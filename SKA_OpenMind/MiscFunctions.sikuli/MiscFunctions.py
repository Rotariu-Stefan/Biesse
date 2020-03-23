from sikuli import *
from time import strftime
from xlwt import Workbook
from xlrd import *
from xlutils.copy import copy
from java.awt.datatransfer import StringSelection
from java.awt.datatransfer import Clipboard
from java.awt import Toolkit
import pickle
from ftplib import FTP
import AutoImages
reload(AutoImages)
from AutoImages import *
myPath = os.path.abspath(os.path.join(getBundlePath(), os.pardir))

def tab(tabNr, timeBetween=0.5):
    if tabNr<0:
        tabNr=abs(tabNr)
        for i in range(0, tabNr):
            type(Key.TAB, KeyModifier.SHIFT); wait(timeBetween)
    else:    
        for i in range(0, tabNr):
            type(Key.TAB); wait(timeBetween)
            
#sv-return tot textu Home->End
def GetText():
    type(Key.HOME); wait(0.2); type(Key.END, KeyModifier.SHIFT); wait(0.2)
    return GetTextSel()

#sv-return tot textu selectat deja
def GetTextSel():
    type('c', KeyModifier.CTRL); wait(0.2)
    text=Env.getClipboard(); wait(0.2)
    toolkit = Toolkit.getDefaultToolkit()
    toolkit.getSystemClipboard().setContents(StringSelection(""), None)
    return text

#sv-sterge tot din locu asta si scrie <modalitate>
def DeleteAndType(dttext):
    type(Key.HOME)
    type(Key.END, KeyModifier.SHIFT); wait(0.2)
    type(Key.DELETE); wait(0.2)
    type(dttext); wait(1)     

def CheckNumLock():
    stateNumLock = Env.isLockOn(Key.NUM_LOCK)
    if stateNumLock == True:
        type(Key.NUM_LOCK)

def CheckCapsLock():
    stateCapsLock = Env.isLockOn(Key.CAPS_LOCK)
    if stateCapsLock == True:
        type(Key.CAPS_LOCK) 

#sv-return nr lunii(sau 13)
def getCfMonth(x):
    return {        
        'A': '01',        'B': '02',        'C': '03',        'D': '04',        'E': '05',
        'H': '06',        'L': '07',        'M': '08',        'P': '09',        'R': '10',
        'S': '11',        'T': '12'        }.get(x, '13')
def getMonthName(mNr):
    return {"01":"gennaio", "02":"febbraio", "03":"marzo", "04":"aprile", "05":"maggio",
            "06":"giugno", "07":"luglio", "08":"agosto", "09":"settembre", "10":"ottobre",
            "11":"novembre", "12":"dicembre"}.get(mNr, None)

def setProvinceDict():
    provList={}
    sigla=""
    reg=""
    
    click(Pattern("SigladellaPr.png").targetOffset(-25,22)); wait(0.3)
    for i in range(0,120):
        sigla=GetText()
        while(sigla==""):
            sigla=GetText(); wait(0.1)
        tab(2); wait(0.2)
        reg=GetText()
        while(reg==""):
            reg=GetText(); wait(0.1)
        provList[str(sigla).strip()]=str(reg).strip()
        tab(-2); wait(0.2)
        click(Pattern("OKAnnulla.png").targetOffset(37,-21)); wait(0.3)    
    pickle.dump(provList, open(myPath + "\\provDict.p", 'wb'))

def getProvinceDict():
    return pickle.load(open(myPath + "\\provDict.p", 'rb'))

def getSocDict():
    return pickle.load(open(myPath + "\\socDict.p", 'rb'))

def createSocList():
    fout=open(myPath + "\\socList.csv", 'ab')
    fout.close()
    prevsoc="asfdwsrgw"
    
    while(True):
        click(Pattern("ECcdiccLotto.png").targetOffset(-70,0)); wait(0.3)
        type(Key.DOWN + Key.ENTER); wait(0.3)
        click(Pattern("EICcdiccLott.png").targetOffset(-109,0)); wait(0.3)
        valsoc=GetText()
        if valsoc==prevsoc:
            break
        else:
            prevsoc=valsoc
        fout=open(myPath + "\\socList.csv", 'ab')
        writer = csv.writer(fout, delimiter='\t')
        writer.writerow([valsoc])
        fout.close()
        
def ftpCopyPDF(filename, cc, dtc):
    isfile=False
    visuraPath=(r"/"+cc+r"/"+dtc[6:10]+r"/TIM/"+getMonthName(dtc[3:5]).upper()
            +r"/"+dtc.replace(r"/", r".")+r"/VISURE/")
    repozitoryPath='C:\\DEALER-AR\\DDI\\RepositoryAttach\\'
    lfile=open(repozitoryPath+filename+r".pdf", 'wb')
    ftp = FTP("192.168.20.1")    
    try:
        ftp.login("anca.palie", "4uk.palie")
        ftp.cwd(visuraPath)
        ftp.retrbinary('RETR '+filename+r".pdf", lfile.write)
        isfile=True
    except:        
        print "PDF was not found !"
    finally:
        lfile.close()
        if isfile==False:
            os.remove(repozitoryPath+filename+r".pdf")
        ftp.quit()
        return isfile
    
def verifyLineValues(lineValue, lineTabs, startImg, ppClickPoint=None):#, clearImg=None):    #DOAR DACA CLIPCLEAR FAIL
    for itry in range(0, 5):
        if ppClickPoint==None:
            equals = verifyLines(lineValue, lineTabs, startImg)
            if equals:
                break
            else:
                if itry>=4:
                    raise Exception("Corect data could not be found!")
        else:
            click(ppClickPoint); wait(1)
            equals = verifyLines(lineValue, lineTabs, startImg)
            if equals: 
                type(Key.ENTER); wait(1)
                break
            else:
                tryClickReg("rwindow", "ciudi", 5); wait(1)
                #if clearImg!=None:    #DOAR DACA CLIPCLEAR FAIL
                #    trydClickReg("rrows", clearImg, 5); wait(0.5); GetTextSel(); wait(0.2)                
                if itry>=4:
                    raise Exception("Corect data could not be found!")

def verifyLines(lineValue, lineTabs, startImg):
    tryClickReg("rwindow", startImg, 5); wait(0.5)
    tryClickReg("rwindow", startImg, 5); wait(0.5); tab(1); wait(0.5)
    prev="empty" 
    while(True):
        equals=True
        breaktabs=0
        tab(lineTabs[0], 0); wait(1)
        selectedValue=GetText()
        if prev == selectedValue:
            break
        print selectedValue+" 1-- "+lineValue[0]
        if  selectedValue != lineValue[0]:
            equals = False
            for ibreaktabs0 in range(1, len(lineTabs)-1):
                breaktabs+=lineTabs[ibreaktabs0]
            tab(breaktabs, 0); wait(1)
        else:
            for lineValCount in range(1, len(lineValue)):
                tab(lineTabs[lineValCount], 0); wait(1)
                selectedValue=GetText()
                print selectedValue+" -- "+lineValue[lineValCount]
                if selectedValue != lineValue[lineValCount]:
                    equals = False
                    for ibreaktabs in range(lineValCount+1, len(lineTabs)-1):
                        breaktabs+=lineTabs[ibreaktabs]
                    tab(breaktabs, 0); wait(1)
                    break
        if equals == True:
            return True
        tab(lineTabs[len(lineTabs)-1],0); wait(1) 
        prev=GetText()
        tab(1); wait(1.5)
    return False

def getTipoVendita(text):
    if "TIM SENZA PROBLEMI SUPER" in text:
        return "ttt"
    elif "TIM SENZA PROBLEMI PIU" in text:
        return "tt"
    elif ("TIM SENZA PROBLEMI RICARICABILE" in text) or ("TIM SENZA PROBLEMI" in text):
        return "t"
    elif "TIM VALORE 400" in text:
        return "tttttttt"    
    elif "TIM VALORE 250" in text:
        return "ttttttt"
    elif "TIM VALORE 150" in text:
        return "tttttt"
    elif "TIM VALORE NEW" in text:
        return "tttt"
    elif "TIM VALORE" in text:
        if "AZIENDA" in text:
            return "tttttttttttttttt"
        else:
            return "ttttttttt"
    elif "TIM TUTTO SMALL" in text:
        return "tttttttttt"
    elif "TIM TUTTO MEDIUM" in text:
        return "ttttttttttt"
    elif "TIM TUTTO LARGE" in text:
        return "tttttttttttt"
    elif "TIM TUTTO SENZA LIMITI" in text:
        if "INSIEME" in text:
            return "tttttttttttttt"
        else:
            return "ttttttttttttt"
    else:
        raise Exception("Tipo vendita not recognized!")

#sv-returneaza o lista de rows(de la fromRow la toRow) cu valori(stringuri) din un Xls(filename)
def getXLSRows(filename, fromRow=0, toRow=-1):
    wb=open_workbook(filename)
    s=wb.sheet_by_index(0)
    if toRow==-1 and fromRow<=s.nrows and fromRow>=0:
        toRow=s.nrows-1
    elif fromRow<0 or fromRow>toRow or toRow>s.nrows:
        raise "Invalid Row indexes!"
    rows=[]
    for rnr in range(fromRow, toRow+1):
        row=[]
        for cnr in range(s.ncols):
            cl=s.cell(rnr ,cnr)
            if cl.ctype==XL_CELL_NUMBER:
                row.append(str(int(cl.value)))
            elif cl.ctype==XL_CELL_DATE:
                dt = list(xldate_as_tuple(cl.value, wb.datemode))
                for i in range(0,6):
                    dt[i]=str(dt[i])
                    if len(dt[i])==1:
                        dt[i]='0'+dt[i]
                dtstr='%s/%s/%s %s:%s:%s'%(dt[2],dt[1],dt[0][2:4],dt[3],dt[4],dt[5])
                row.append(dtstr)
            else:
                row.append(str(cl.value))
        rows.append(row)
    return rows

#sv-completeaza fis de intrare cu noi date
def completeXLS(filename, row, value):
    rb = open_workbook(filename, formatting_info=True)
    wb = copy(rb)
    ws = wb.get_sheet(0)

    ws.write(row, 20, "OK")
    ws.write(row, 21, value)
    ws.write(row, 22, "APPROVATO")
    ws.write(row, 23, strftime('%d/%m/%Y'))

    wb.save(filename)

def pastesv(str):
    paste(str); wait(0.1)
    toolkit = Toolkit.getDefaultToolkit()
    toolkit.getSystemClipboard().setContents(StringSelection(""), None)