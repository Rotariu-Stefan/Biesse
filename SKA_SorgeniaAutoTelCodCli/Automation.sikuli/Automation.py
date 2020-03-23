from sikuli import *
import os
import csv
import shutil
from time import strftime
from datetime import datetime

import AutoImages
reload(AutoImages)
from AutoImages import *
pagepoint=Location(7, Screen(0).getH()/2)
pagejos=Location(17, (Screen(0).getH()/4)*3)

logDir = "D:\\Automatizare Sorgenia\\SorgeniaAutoTelCodCli\\Log\\" + strftime("Logger_%d_%m_%Y") + "\\"
logFis = logDir + "log.txt"
outDir = "D:\\Automatizare Sorgenia\\Fisiere de iesire\\"
outFis = outDir + "codcli2_output.csv"

def MakeFiles():
    if not os.path.exists(logDir):
        os.makedirs(logDir)
    if not os.path.exists(logFis):
        fl = open(logFis, "wb")
        fl.close()
    if not os.path.exists(outDir):
        os.makedirs(outDir)
    if not os.path.exists(outFis):
        fl = open(outFis, "wb")
        fl.close()
        writeToOutput(["Cod Cli", "Data Affido", "Cod Fis", "PIVA", "Tel Fisso", "Tel Celulare", "Tel Associati", "Tel Contatti"])
        
def CaptureScreen():
    img = Screen(0).capture(Screen(0).getBounds())
    shutil.copy2(img, logDir+strftime("[%d_%m_%Y]Capture Exception Time[%H_%M_%S].png"))
    
def writeToLog(mesaj):
    myfile = file(logFis, "a")
    myfile.write(strftime("[%H:%M:%S]") + " - " + mesaj + "\n")
    myfile.close()

def writeToOutput(row):
    with open(outFis, 'ab') as fout:
        writer = csv.writer(fout, delimiter='\t')
        writer.writerow(row)

def CheckNumLock():
    stateNumLock = Env.isLockOn(Key.NUM_LOCK)
    if stateNumLock == True:
        type(Key.NUM_LOCK)        

def GetText():
    type(Key.HOME); wait(0.1); type(Key.END, KeyModifier.SHIFT); wait(0.1)
    return GetTextSel()

def GetTextSel():
    type('c', KeyModifier.CTRL); wait(0.1)
    text=Env.getClipboard()
    type(Key.LEFT); paste("")
    return text

def pastesv(str):
    paste(str); wait(0.1)
    paste("")        

def loadCRM():
    writeToLog("CRM loading...")
    
    if not App.focus("Sorgenia Call Center"):
        App.open("C:\\Program Files\\Internet Explorer\\iexplore.exe http://10.1.168.1/sorgenia_ita/start.swe"); wait(8)
    if verifyExist("etc", "enterSorgenia", 5):
        type("ruggericinzia"+Key.TAB+"firespa2" + Key.ENTER); wait(10)
    if verifyExist("etc", "ieSorgenia", 10):
        type(Key.F11); wait(5) 
    if verifyExist("etc", "clienti", 5):
        clickImage("etc", "clienti", 3); wait(8)
    elif verifyExist("etc", "clientisel", 5):
        clickImage("etc", "clientisel", 3); wait(8)        

    writeToLog("CRM loaded successfully.")

tErr=0
def Automation(startRow=1, finalRow=-1, errMax=50):
    global tErr
    writeToLog("Automation started!")
    
    with open('D:\\Automatizare Sorgenia\\SorgeniaAutoTelCodCli\\input.csv', 'rb') as fin:
        reader = csv.reader(fin, delimiter=',')

        startCT="StartingRow"
        if startRow==-1:
            otest=open(outFis, 'rb')        
            oreader = csv.reader(otest, delimiter='\t')        
            try:
                oreader.next()
                startCT=oreader.next()[0]
                for row in oreader:
                    startCT=row[0]
            except:
                startRow=1
            otest.close()

        nrow=0
        if startRow==-1:
            outputpassed=False
        else:
            outputpassed=True
        for row in reader:
            nrow+=1
            if startRow==-1:
                if outputpassed==False:
                    if row[0]==startCT:
                        outputpassed=True
                    continue
            elif nrow<startRow:
                continue                        
            if finalRow!=-1 and nrow>finalRow:
                break
            if tErr>errMax:
                writeToLog("Too many errors, assuming aplication failure!")
                break

            for tries in range(0,5):
                try:
                    procRow(row)
                    break
                except Exception, exp:
                    tErr+=1
                    if(tries>3):
                        CaptureScreen()
                        writeToLog("@CodCli = " + row[0] + "--" + exp.message + " -ABORT!")
                        writeToOutput([row[0], row[2], row[4], row[5], "?", "?", "?", "Could not get data!"])
                    else:
                        writeToLog("@CodCli = " + row[0] + "--" + exp.message + " -retry("+ str(tries+1) + ")...")
                except:
                    tErr+=1
                    if(tries>3):
                        CaptureScreen()
                        writeToLog("@CodCli = " + row[0] + "--" + "Unknown error" + " -ABORT!")
                        writeToOutput([row[0], row[2], row[4], row[5], "?", "?", "?", "Could not get data!"])
                    else:
                        writeToLog("@CodCli = " + row[0] + "--" + "Unknown error" + " -retry("+ str(tries+1) + ")...")
        if outputpassed==False:
            pass

    writeToLog("Automation finished!")
    
def procRow(iRow):
    oData=[iRow[0], iRow[2], iRow[4], iRow[5]]

    clickImage("etc", "clientisel", 3); wait(4)
    #clickImage("etc", "tutticlienti", 5); wait(2)
    type("q", KeyModifier.ALT); wait(2)        
    if len(iRow[0].split("_"))<2:
        return
    type(iRow[0].split("_")[1] + Key.ENTER); wait(4)
    clickImage("etc", "ragione", 5); wait(6)

    allphones=[]
    dclickImage("etc.", "telfis", 10); wait(0.3)
    allphones.append("'"+GetTextSel().strip())
    dclickImage("etc.", "telcel", 10); wait(0.3)
    allphones.append("'"+GetTextSel().strip())
    dclickImage("etc.", "telass", 10); wait(0.3)
    allphones.append("'"+GetTextSel().strip())
    
    click(pagepoint); wait(0.3); type(Key.PAGE_DOWN*2); wait(4)
    if verifyExist("etc", "contattisel", 5):
        clickImage("etc", "contatti", 5); wait(4)
    clickImage("etc", "menu2", 5); wait(0.5); tab(1); wait(0.5)
    if GetText()=="":
        return
    while(True):     
        tab(4); wait(0.2)
        aux="'"+GetTextSel().strip()
        if (not aux in allphones) and (aux!="'"):
            allphones.append(aux)
        tab(10); wait(0.2)
        aux=GetTextSel()
        tab(1); wait(1)
        if GetText()==aux:
            break
            
    oData+=allphones
    oData.append("OK")
    writeToOutput(oData)

def tab(tabNr):
    if tabNr<0:
        tabNr=abs(tabNr)
        for i in range(0, tabNr):
            type(Key.TAB, KeyModifier.SHIFT); wait(0.1)
    else:    
        for i in range(0, tabNr):
            type(Key.TAB); wait(0.1)    

CheckNumLock()
MakeFiles()
writeToLog("------------------------------")
AutoImages.loadImages()
loadCRM()
Automation(-1, 300, 50)
writeToLog("------------------------------")

type(Key.F11); wait(1)
print "=====================!!!SUCCESS!!!====================="