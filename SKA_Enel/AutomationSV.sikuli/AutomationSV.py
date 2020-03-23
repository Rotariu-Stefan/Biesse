from sikuli import *
import os
import csv
from time import strftime
from datetime import datetime
import shutil
import AutoImages
reload(AutoImages)
from AutoImages import *
import AutoRegions
reload(AutoRegions)
from AutoRegions import *

myImages = AutoImages()
myRegions = AutoRegions()

director = "C:\\BSS\\EnelSiebel\\SV\\Log\\" + strftime("Logger_%d_%m_%Y") + "\\"
fisier = director + "Log.txt"


def MakeFiles():
    if not os.path.exists(director):
        os.makedirs(director)
    if not os.path.exists(fisier):
        fl = open(fisier, "wb")
        fl.close()
    if not os.path.exists("C:\\BSS\\EnelSiebel\\SV\\Output\\siebel_outputSV.csv"):
        fo = open("C:\\BSS\\EnelSiebel\\SV\\Output\\siebel_outputSV.csv", "wb")
        fo.close()

def CaptureScreen():
    img = Screen(0).capture(Screen(0).getBounds())
    shutil.copy2(img, director+strftime("[%d_%m_%Y]Capture Exception Time[%H_%M_%S].png"))
    
def writeToLog(mesaj):
    myfile = file(fisier, "a")
    myfile.write(strftime("[%H:%M:%S]") + " - " + mesaj + "\n")
    myfile.close()

def writeToOutput(row):
    with open('C:\\BSS\\EnelSiebel\\SV\\Output\\siebel_outputSV.csv', 'ab') as fout:
        writer = csv.writer(fout, delimiter='\t')
        writer.writerow(row)

def GetText():    
    type('c', KeyModifier.CTRL); wait(0.1)
    text=Env.getClipboard()
    type(Key.RIGHT); paste("")
    return text

def pastesv(str):
    paste(str); wait(0.1)
    paste("")

def vpnVerify():
    while(myImages.waitImage("regScreen", "noVPNconn", 5)=="OK"):
        writeToLog("No VPN Connection!")
        popup("No VPN Connection! Connect manually to VPN and after that press OK!")
        type(Key.F5); wait(25)

def noRowsSV():
    rnr=0
    cfo=myImages.findImage("regUPdinamic", "codfis", 5).getCenter(); wait(0.3)
    cfr=Region(cfo.x-45,cfo.y+16,109,15)
    if len(cfr.text())>2:
        rnr+=1
    while(True):    
        cfr=cfr.offset(Location(0, 22))
        if len(cfr.text())>2:
            rnr+=1
        else:
            break
    return rnr

def GoToRowSV(lineNr):
    cfo=myImages.findImage("regUPdinamic", "codfis", 5).getCenter(); wait(0.3)
    click(Location(cfo.x, cfo.y+16+22*(lineNr-1))); wait(2)

def telSwitch2(oData):
    dt=[]
    if not len(oData[3].strip())>1:
        if not len(oData[4].strip())>1:
            dt.append(oData)
        else:
            if oData[4].startswith("'3"):
                dt.append([oData[0], oData[1], oData[2], "", oData[4], oData[5], oData[6]])
            else:
                dt.append([oData[0], oData[1], oData[2], oData[4], "", oData[5], oData[6]])
    else:
        if not len(oData[4].strip())>1:
            if oData[3].startswith("'3"):
                dt.append([oData[0], oData[1], oData[2], "", oData[3], oData[5], oData[6]])
            else:
                dt.append([oData[0], oData[1], oData[2], oData[3], "", oData[5], oData[6]])
        else:
            dt.append(oData)
    return dt
    
def telSwitch(oData):
    dt=[];    tel=[];    cel=[]
    for i in range(3, 5):
        if oData[i].startswith("'3"):
            cel.append(oData[i])
        elif len(oData[i].strip())>1:
            tel.append(oData[i])
    if len(tel)>1:
        dt.append([oData[0], oData[1], oData[2], tel[0], "", oData[5], oData[6]])
        dt.append([oData[0], oData[1], oData[2], tel[1], "", oData[5], "OK -tel2"])
    elif len(cel)>1:
        dt.append([oData[0], oData[1], oData[2], "", cel[0], oData[5], oData[6]])
        dt.append([oData[0], oData[1], oData[2], "", cel[1], oData[5], "OK -cel2"])
    else:
        if(len(tel)==0):
            tel.append("")
        if(len(cel)==0):
            cel.append("")
        dt.append([oData[0], oData[1], oData[2], tel[0], cel[0], oData[5], oData[6]])
    return dt
    
def loadCRM():
    try:
        writeToLog("CRM loading...")
        if not App.focus("Siebel Energy"):
            browsercrm  = App.open("C:\Program Files (x86)\Internet Explorer\iexplore.exe http://crm-ml.risorse.enel/eenergy_enu/start.swe"); wait(1)
        if myImages.waitImage("regScreen", "crmLoginPic", 10) == "OK":
            type("RISORSE\AE23032" + Key.TAB + "AvTa6Y$g" + Key.ENTER); wait(10)
        if myImages.waitImage("regScreen", "ieSiebel", 3) == "OK":
            type(Key.F11)
        if myImages.waitImage("regScreen", "siebelVers", 3) == "OK":
            type(Key.ENTER)
        if myImages.waitImage("regScreen", "welcomeBack", 3) == "OK":
            myImages.clickImage("regUPdinamic", "clienti1", 3)
            if myImages.waitImage("regUPdinamic", "clienti2", 3) == "OK":
                myImages.clickImage("regUPdinamic", "clienti2", 3)
        if myImages.waitImage("regUPdinamic", "clienti2hover", 5) != "OK":
            raise Exception("Cannot find starting place (clienti2 page)!")
        writeToLog("CRM loaded successfully.")
    except Exception, exp:
        writeToLog("@loading CRM -- " + exp.message)
        exit(1)
    except:
        writeToLog("@loading CRM -- Unknown!")
        exit(1)

prevData=['cf', 'tel', 'cel', 'mail']
def procRow(row):
    global prevData, lastClip
    
    if len(row)<3 or row[2]==None or len(row[2].strip())==0:
        writeToOutput([row[0], row[1], "???", "", "", "", "CodFis_PIVA cannot be empty ! Please check it manually !"])
        return
    
    oData=['empty', 'empty', 'empty', 'empty', 'empty', 'empty', 'empty']
    oData[0]=row[0].strip()
    oData[1]=row[1].strip()
    oData[2]=row[2].strip()

    myImages.clickImage("regUPdinamic", "clienti2hover", 5); wait(1.2)
    myImages.clickImage("regUPdinamic", "cerca", 5); wait(1.2)
    type(Key.TAB*2)
    if oData[2][0].isdigit():
        while(len(oData[2])<11):
            oData[2] = "0" + oData[2]
    pastesv(oData[2]); type(Key.ENTER); wait(1)
    
    randuri = noRowsSV()    #CHRSDR66P08E884P pb

    for i in range (1, randuri+1):
        if randuri>1:
            GoToRowSV(i)        
        myImages.clickImage("SV", "attivita", 5)
        dclickImage("regAttivaInfodinamic", "tel",30); wait(0.2)
        oData[3]="'"+GetText()
        dclickImage("regAttivaInfodinamic", "cel",30); wait(0.2)
        oData[4]="'"+GetText()
        if oData[3]==oData[4]:
            oData[4]=""
        dclickImage("regAttivaInfodinamic", "mail",30); wait(0.2)
        oData[5]=GetText()
        if i<randuri:
            myImages.clickImage("regUPdinamic", "clienti2hover", 5); wait(1)

        if prevData[1:4]==oData[3:6] and prevData[0]!=oData[2]:
            raise Exception("Data carried over from last contract!")
        prevData=oData[2:6]

        oData[6]="OK"        
        for oRow in telSwitch2(oData):
            writeToOutput(oRow)

tErr=0
def Automation(startRow=0, finalRow=-1, errMax=50):
    global tErr

    writeToLog("Automation started!")
    with open('C:\\BSS\\EnelSiebel\\SV\\inputFULL.csv', 'rb') as fin:
        reader = csv.reader(fin, delimiter=',')
        otest=open('C:\\BSS\\EnelSiebel\\SV\\Output\\siebel_outputSV.csv', 'rb')
        
        oreader = csv.reader(otest, delimiter='\t')        
        rowSofar = 0
        startCT = ""
        for row in oreader:
            rowSofar += 1
            break        
        if rowSofar==0:
            writeToOutput(["Contratto", "Cliente", "CodFiscale_PIVA", "Telefono", "Cellulare", "E-mail", "Esito"])
            if(startRow==-1):
                startRow==0
        else:
            if startRow==-1:
                for row in oreader:
                    startCT=row[0]
        otest.close()

        nrow=0
        outputpassed=False
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
            tries=0
            while(True):
                try:
                    procRow(row)
                    break
                except Exception, exp:                        
                    tErr+=1
                    vpnVerify()
                    if(tries>3):
                        CaptureScreen()
                        writeToLog("@Contratto = " + row[0] + " (CF = " + row[2].strip() + ") --" + exp.message + " -ABORT")
                        writeToOutput([row[0], row[1], row[2], "", "", "", "Could not get data! Please check manually!"])
                        break
                    writeToLog("@Contratto = " + row[0] + " (CF = " + row[2].strip() + ") --" + exp.message + " -retry("+ str(tries+1) + ")...")
                except:
                    tErr+=1
                    vpnVerify()
                    if(tries>3):
                        CaptureScreen()
                        writeToLog("@Contratto = " + row[0] + " (CF = " + row[2].strip() + ") -- Unknown exception!" + " -ABORT")
                        writeToOutput([row[0], row[1], row[2], "", "", "", "Could not get data! Please check manually!"])
                        break
                    writeToLog("@Contratto = " + row[0] + " (CF = " + row[2].strip() + ") -- Unknown exception!" + " -retry("+ str(tries+1) + ")...")
                tries+=1
    writeToLog("Automation finished!")

MakeFiles()
writeToLog("------------------------------")
loadCRM()
#220->280 nashpa
Automation(-1, -1, 10)
writeToLog("------------------------------")

print "====================SUCCESS!!!======================"