from sikuli import *
import Automation
reload(Automation)
from Automation import *
myPath = os.path.abspath(os.path.join(getBundlePath(), os.pardir))

def Automation(startRow=1, finalRow=-1, errMax=50):
    global tErr

    writeToLog("Automation started!")
    reader = getXLSRows(inFis, 0, -1)

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
    if startRow==-1:
        outputpassed=False
    else:
        outputpassed=True
        
    nrow=0
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
        if errMax!=-1 and tErr>errMax:
            writeToLog("Too many errors, assuming aplication failure!")
            break

        for tries in range(0,5):
            try:
                processInputRow(row)
                certificatione()
                indirizzo()
                billing()
                contratti()
                ordini()
                completeXLS(inFis, rowNr, condiceOrdine)
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