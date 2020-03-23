from sikuli import *
import os
import csv
import re
import string
from time import strftime
from datetime import datetime
import shutil
myPath = os.path.abspath(os.path.join(getBundlePath(), os.pardir))
#192.168.20.1/CLUJ/2013/TIM/SETTEMBRE/11.09.2013/visure
#anca.palie    4uk.palie
    
import MiscFunctions
reload(MiscFunctions)
from MiscFunctions import *

usernameCRM = "8110000085100"
passwordCRM = "!GENTILISSIMALORI30"

regionsFile = myPath + "\\Regions.csv"
logDir = myPath + "\\Log\\" + strftime("Logger_%d_%m_%Y") + "\\"
logFis = logDir + "log.txt"
outDir = myPath + "\\Output\\"
outFis = outDir + "output.txt"
bkDir = myPath + "\\Back-up\\"
inFis = myPath + "input.xls"

#global variables
regionList = []
prefixTelItalia = [10,11,121,122,123,124,125,131,141,142,143,144,15,161,163,165,166,171,172,173,174,175,182,183,184,185,187,19,2,30,31,321,322,323,324,331,332,334,341,342,343,344,345,346,35,362,363,364,365,369,371,372,373,374,375,376,377,381,382,383,384,385,386,39,40,41,0421,0422,0423,0424,425,426,427,428,429,431,432,433,434,435,436,437,438,439,442,444,445,45,461,462,463,464,465,471,472,473,474,481,49,50,51,521,522,523,524,525,532,533,534,535,536,541,542,543,544,545,546,547,549,55,564,566,571,572,573,574,575,577,578,583,584,585,586,587,588,59,6,70,71,721,722,73,731,732,733,734,735,736,737,74,742,743,744,746,75,761,763,765,766,769,771,773,774,775,776,781,782,783,784,785,789,79,80,81,82,823,824,825,827,828,831,832,833,835,836,85,861,862,863,864,865,871,872,873,874,875,881,882,883,884,885,89,90,91,921,922,923,924,925,931,932,933,934,935,941,942,95,961,962,963,964,965,966,967,968,971,972,973,974,975,976,981,982,983,984,985,99,800]
siglaProvinceDict = getProvinceDict()
socDict = getSocDict()
inputData = {}
cfData = {}
CFA="888201412656"

#sv-maximizeaza citrix
def citrixMax():
    if exists(AutoImages.imgDict["citrixLogo"], 2):
        doubleClick(AutoImages.imgDict["citrixLogo"]); wait(2)
    elif exists(AutoImages.imgDict["citrixLogo2"], 2):
        doubleClick(AutoImages.imgDict["citrixLogo2"]); wait(2)
    else:
        print "Problem at Max"

#sv-minimizeaza citrix
def citrixMin():
    if exists(AutoImages.imgDict["citrixCom"],2):
        hover(AutoImages.imgDict["citrixCom"]); wait(0.5)        
        click(AutoImages.imgDict["citrixClose"]); wait(1)
    else:
        print "Problem at Min"

#sv-creeaza diz/fis ZC (dak nu e deja)
def MakeFiles():
    if not os.path.exists(logDir):
        os.makedirs(logDir)
    if not os.path.exists(logFis):
        fo = open(logFis, "wb")
        fo.close()
    if not os.path.exists(inFis):
        pass
        #writeToLog("Could not find input file. Exit...")
    if not os.path.exists(outDir):
        os.makedirs(outDir)
    if not os.path.exists(outFis):
        fo = open(outFis, "wb")
        fo.close()
    if not os.path.exists(bkDir):
        os.makedirs(bkDir)        
        
#sv-capture la ecran si copie fisimg in dir logZC
def CaptureScreen():
    img = Screen(0).capture(Screen(0).getBounds())
    shutil.copy2(img, logDir+strftime("[%d_%m_%Y]Captura Exceptie -- Time[%H_%M_%S].png"))

#sv-scrie mesaj in fis logZC
def writeToLog(mesaj):
    myfile = file(logFis, "a")
    myfile.write(strftime("[%H:%M:%S]") + " - " + mesaj +"\n")
    myfile.close()

#sv-scrie mesaj in fis output
def writeToOutput(mesaj):
    myfile = file(outFis, "a")
    myfile.write(mesaj +"\n")
    myfile.close()

#sv-scrie datele dupa procesari
def printData():
    print "ID-----------------------"
    for entry in inputData.items():
        print "ID",entry,"\n"
    print "CFD-----------------------"
    for entry in cfData.items():
        print "CFD",entry,"\n"
    print "-----------------------"

#sv-click pe "start", apoi "programs", apoi "crmaffari"
def OpenCRM():
    if not verifyExistReg("rmenu", "crmlogged", 5):
        tryClickReg("rbottom", "start", 10)
        tryClickReg("rrow1", "programs", 10)
        tryClickReg("rwindow", "crmaffari", 10)
        LoginCRM()

#sv-click "usernameAffari", scrie user/pass, asteapta "rememberPasswordAffari"-Enter, apoi "secureIntCon"-Alt+Y-Enter, click "closeLayer", asteapta "welcomeBack"
def LoginCRM():
    tryClickReg("rwindow", "usernameAffari", 15)
    type(usernameCRM + Key.TAB + passwordCRM + Key.ENTER); wait(3)
    check = verifyExistReg("rwindow", "rememberPasswordAffari", 10)
    type(Key.ENTER); wait(2)
    check = verifyExistReg("rwindow", "secureIntCon", 10); wait(3)
    type('Y', KeyModifier.ALT); wait(3)
    type(Key.ENTER); wait(2)
    tryClickReg("rwindow", "closeLayer", 10)
    wait(AutoImages.imgDict["welcomeBack"], 20)

#sv-Ctrl+Shift+x-tab-tab-Enter-tab-tab-Enter
def LogOffCRM():
    type('x' , KeyModifier.CTRL + KeyModifier.SHIFT); wait(1)
    type(Key.TAB + Key.TAB + Key.ENTER); wait(1)
    type(Key.TAB + Key.TAB + Key.ENTER)

#sv-return prefix telFisso(pe baza de lista prefixTelItalia)
def getPrefixTelFisso(telFisso):
    i = 1
    while(True):
        if i > len(telFisso):
            type(Key.PAGE_UP)
            return "0";
        if (int(telFisso[0:i]) in prefixTelItalia):
            return "0"+telFisso[0:i]
        i+= 1
        
#sv-ia din cod_fis info si populeaza cfData[] cu info
def reverseCF():
    global cfData
    
    cod_fis=inputData["cf"]
    year = '19' + cod_fis[6:8]
    month = getCfMonth(cod_fis[8])
    day = cod_fis[9:11]
    if int(day) < 41:
        cfData["sex"] = "M"
    else:
        cfData["sex"] = "F"
        day=str(int(day)-40)
    cfData["birthday"] = str(day) + '/' + str(month) + '/' + str(year)
    cfData["codice_catastale"] = cod_fis[11:15]
    try:
        if cfData["codice_catastale"].startswith("Z"):
            xlsreader = getXLSRows(myPath + "\\statiEsteri.xls", 0, -1)
            for serow in xlsreader:
                if cfData["codice_catastale"]==serow[4]:
                    cfData["luogoDiNascita"]=serow[3].replace("SAN", "S.")
                    cfData["sigla"]="EE"
                    cfData["provincia"] = "EE"
                    break
        else:
            fin=open(myPath + "\\Regions.csv", 'rb')
            reader = csv.reader(fin, delimiter=';')
            for row in reader:
                if cfData["codice_catastale"]==row[2]:
                    cfData["luogoDiNascita"]=row[0].replace("SAN", "S.")
                    cfData["sigla"]=row[1]
                    cfData["provincia"] = siglaProvinceDict[cfData["sigla"]]
                    break
    except KeyError:
        raise Exception("Could not get provincia, Error at finding sigla!")                
    except:
        raise Exception("Error searching Regions.csv")
    finally:
        if not cfData["codice_catastale"].startswith("Z"):
            fin.close()

#sv-proceseaza date din fis de input(un singur row) si populeaza inputData/cfData
def processInputRow(row):
    global inputData   

    inputData["listaProvenienza"] = row[0]
    inputData["dataContratto"]=row[1]
    inputData["cc"]=row[2].strip()
    inputData["piva"] = row[3].strip()
    while (len(inputData["piva"]) < 11):
        inputData["piva"] = "0" + inputData["piva"]
    inputData["ragioneSociale"] = row[4].strip()
    inputData["telFisso"] = row[5].strip()
    inputData["prefissoTelFisso"] = getPrefixTelFisso(inputData["telFisso"])
    inputData["telFisso"] = inputData["telFisso"][3:len(inputData["telFisso"])]
    inputData["celular"] = row[6].strip()
    inputData["prefissoCelular"] = inputData["celular"][0:3]
    inputData["celular"] = inputData["celular"][3:len(inputData["celular"])]
    inputData["titolare"] = row[7].strip()
    inputData["cf"] = row[8].strip()
    splited = inputData["titolare"].split(" ")
    if len(splited[0]) < 4:
        splited[0] = splited[0] + " " + splited[1]
    if (inputData["cf"][0] in splited[0]) and (inputData["cf"][1] in splited[0]) and (inputData["cf"][2] in splited[0]):
        inputData["cognome"] = splited[0].strip().upper()
        inputData["nome"] = inputData["titolare"][len(splited[0]):len(inputData["titolare"])].strip().upper()
    if (inputData["cf"][0] in splited[len(splited)-1]) and (inputData["cf"][1] in splited[len(splited)-1]) and (inputData["cf"][2] in splited[len(splited)-1]):
        inputData["cognome"] = splited[len(splited)-1].strip().upper()
        inputData["nome"] = inputData["titolare"][0:len(inputData["titolare"])-len(inputData["cognome"])].strip().upper()
    documento = row[9].strip()
    nrs=re.findall(r'[0-9]+',documento.strip().split("_")[2].split(":")[1].strip())
    inputData["dataRilascioDocumento"] = "%s/%s/%d"%(nrs[0],nrs[1],int(nrs[2])-10)#data din CI - 10 years
    inputData["luogoRilascio"] = documento.strip().split("_")[1].split(":")[1].strip()
    nDoc = documento.strip().split("_")[0].strip().split(":")[1].strip().split(" ")
    inputData["numeroDocumento"] = nDoc[len(nDoc)-1].strip()
    if inputData["numeroDocumento"][len(inputData["numeroDocumento"])-2].isdigit() and inputData["numeroDocumento"][len(inputData["numeroDocumento"])-1].isdigit()==False:
        inputData["tipoDocumento"] = "PATENTE"
    elif inputData["numeroDocumento"][1].isdigit() and inputData["numeroDocumento"][len(inputData["numeroDocumento"])-1].isdigit():
        inputData["tipoDocumento"] = "PASSAPORTO"
    else:
        inputData["tipoDocumento"] = "CARTA DI IDENTITA"
    inputData["mail"] = row[12].strip()
    inputData["tipovendita"]=row[14].strip()   
    indirizzo = row[13].strip()
    inputData["cap"] = indirizzo.strip().split("_")[3].strip()
    inputData["provinciaIndirizzo"] = indirizzo.strip().split("_")[2].strip()
    inputData["comuneSedeLegale2"] = indirizzo.strip().split("_")[1].strip()
    inputData["siglai"] = siglaProvinceDict.keys()[siglaProvinceDict.values().index(inputData["provinciaIndirizzo"])]    
    street = indirizzo.strip().split("_")[0].strip()     
    inputData["civicoSedeLegale"] = street.split(" ")[len(street.split(" "))-1].strip()
    if(inputData["civicoSedeLegale"]==""):
        inputData["civicoSedeLegale"]="S.N.C."
    else:
        street = street.replace(inputData["civicoSedeLegale"], "")
    inputData["partTopSedeLegale"] = street.split(" ")[0].strip()
    inputData["viaSedeLegale"]  = street.replace(inputData["partTopSedeLegale"], "").strip()
    inputData["modalitaPagamento"] = row[18]
    inputData["ivr"]=row[19]
    
    reverseCF()
    if "DITTA" in inputData["listaProvenienza"].upper():
        inputData["giuridica"]="ditta"
    else:
        for socItem in socDict.items():
            if socItem[0] in inputData["ragioneSociale"]:
                inputData["giuridica"]=socItem[1]
                return
        raise Exception("Could not find forma giuridica!")

def startClienti():
    global inputData
    
    tryClickReg("rmenu", "clienti", 5); wait(2)
    if inputData["giuridica"]=="ditta":
    	tryClickReg("rrow1", "codiceFiscaleField", 10)
    	type(inputData["cf"]); wait(0.5)
    else:
    	tryClickReg("rrow1", "pivaField", 10)
    	type(inputData["piva"]); wait(0,5)
    tryClickReg("rrow1", "clientiCerca", 10)

    #verify if is new
    if verifyExistReg("rmid", "cfpivaNonEsiste", 5):
        print "Intrarea Nu Exista!"
        tryClickReg("rmid", "cfpivaNonEsiste", 3)
    else:
        print "Intrarea Deja Exista!"
        return    #TO DO LATER !!!
    
#sv-certificazione-nu se poate intoarce la asta dupa click pe "accettaDatiUtente"
def certificazione():
    global inputData, cfData

    if inputData["giuridica"]!="ditta":
        tryClickReg("rrow2", "giuridica", 5); wait(0.5)
        DeleteAndType(inputData["giuridica"]+Key.TAB); wait(0.5)
        tab(-4, 0); wait(0.5)
        type(inputData["ragioneSociale"]); wait(0.5)
        tryClickReg("rrow1", "siglaceva", 5); wait(0.5)
        type(inputData["siglai"]+Key.TAB*2); wait(0.5)
    else:
        tryClickReg("rrow2", "naturaCliente", 10)
        type(Key.DOWN + Key.UP + Key.ENTER); wait(0.5)
        tryClickReg("rrow1", "piva", 10)
        type(inputData["piva"] + Key.TAB); wait(0.5)
        type(inputData["ragioneSociale"] + Key.TAB*2); wait(0.5)
        type(inputData["cf"] + Key.TAB); wait(0.3)
        type("DATO NON PRESE" + Key.TAB*2); wait(2)
        type(inputData["nome"] + Key.TAB*3); wait(0.5)
        type(inputData["cognome"] + Key.TAB*3); wait(0.5)
        type(cfData["sex"] + Key.TAB*2); wait(0.5)
        type(inputData["tipoDocumento"] + Key.TAB); wait(0.5)
        type(cfData["birthday"] + Key.TAB*2); wait(0.5)
        type(inputData["numeroDocumento"] +  Key.TAB); wait(0.5)
        type(cfData["provincia"] + Key.TAB*2); wait(2)
        type(inputData["dataRilascioDocumento"] + Key.TAB); wait(1)
        type(cfData["luogoDiNascita"] + Key.TAB*2); wait(2)
        type(inputData["luogoRilascio"] + Key.TAB); wait(1)
        type(inputData["siglai"] + Key.TAB*2); wait(1)
        
    type(inputData["comuneSedeLegale2"] + Key.TAB); wait(5)
    tryClickReg("rrow2", "partTopSedeLegale", 5)
    type(inputData["partTopSedeLegale"]); wait(1)
    tryClickReg("rrow2", "viaSedeLegale", 5)
    type(inputData["viaSedeLegale"] ); wait(1)
    tryClickReg("rrow2", "civicoSedeLegale", 5)
    type(inputData["civicoSedeLegale"]); wait(0.5)
    tryClickReg("rrow1", "datiUtente", 5)
    type(Key.PAGE_DOWN*2); wait(3)
    tryClickReg("rrow1", "prefissoContatto", 5)
    type(inputData["prefissoTelFisso"] + Key.TAB*2); wait(1)
    type(inputData["prefissoCelular"] + Key.TAB); wait(1)
    type(inputData["telFisso"] + Key.TAB*2); wait(1)
    type(inputData["celular"] + Key.TAB); wait(1)
    type(inputData["mail"].split("@")[0])
    type("@")
    #type(r";", KeyModifier.CTRL + KeyModifier.ALT)
    type(inputData["mail"].split("@")[1]); wait(1.5); tab(1)

    popup("B4 CERTIFICA")
    tryClickReg("rmenu", "accettaDatiUtente", 5); wait(15)
    tryClickReg("rmenu", "certifica", 5); wait(15)
    tryClickReg("rmenu", "certifica", 5); wait(15)
    tryClickReg("rmenu", "accettaDatiACLI", 5); wait(15)

#sv-anagrafica
def indirizzo():
    global inputData, cfData   

    click(Regions.pagepoint); type(Key.PAGE_DOWN); wait(1)
    if not verifyExistReg("rrows", "indirizziHover", 5):
        tryClickReg("rrows", "indirizzi", 5); wait(2)

    equals = verifyLineValues([inputData["provinciaIndirizzo"], inputData["siglai"], inputData["comuneSedeLegale2"],
                 inputData["partTopSedeLegale"], inputData["viaSedeLegale"], inputData["civicoSedeLegale"]],
            [1, 1, 1, 4, 1, 1, 7], "indirizziStart")

    if equals == False:
        tryClickReg("rrow1", "nuovo", 5); wait(1)
        type(Key.TAB + inputData["provinciaIndirizzo"] + Key.TAB*2); wait(1)
        type(inputData["comuneSedeLegale2"] + Key.TAB*4); wait(1)
        type(inputData["partTopSedeLegale"] + Key.TAB); wait(1)
        type(inputData["viaSedeLegale"]  + Key.TAB); wait(1)
        type(inputData["civicoSedeLegale"] + Key.TAB); wait(1)
        type(Key.ENTER); wait(5)
        type(Key.ENTER); wait(25)

def referenti():
    if not verifyExistReg("rrows", "referentiSel", 5):
        tryClickReg("rrow1", "referenti", 5); wait(2)
    click(Regions.pagepoint); type(Key.PAGE_UP); wait(1)
    tryClickReg("rrow1", "referentiStart", 5); wait(0.5)
    tryClickReg("rrow1", "referentiStart", 5); wait(0.5);
    GetTextSel(); tab(1)
    if GetTextSel()=="":
        tryClickReg("rrow1", "nuovo", 5); wait(1)
        if inputData["giuridica"]!="ditta":
            type(inputData["cf"]+Key.TAB); wait(0.5)
            type(inputData["nome"]+Key.TAB); wait(0.5)
            type(inputData["cognome"]+Key.TAB); wait(0.5)
            type(cfData["birthday"]+Key.TAB); wait(0.5)
            type(cfData["sigla"]+Key.TAB); wait(0.5)
            type(cfData["luogoDiNascita"]+Key.TAB); wait(1.5)
            type(cfData["sex"]+Key.TAB); wait(0.3); tab(11); wait(1)
            type(inputData["tipoDocumento"]+Key.TAB); wait(0.5)
            type(inputData["numeroDocumento"]+Key.TAB); wait(1)
            type(inputData["luogoRilascio"]+Key.TAB); wait(0.5)
            type(inputData["dataRilascioDocumento"]+Key.TAB*2); wait(0.5)
        else:
            tab(23, 0)
        if cfData["sex"] == "M":
            type("SIG.")
        else:
            type("SIG.RA")
        wait(1)
        type(Key.TAB); wait(1)
        if verifyExistReg("rrows", "refUnchecked", 5):
            tryClickReg("rrows", "refUnchecked", 5); wait(1)
        type(Key.TAB); wait(1)
        type("TUTTO" + Key.TAB*4); wait(2)
        tryClickReg("rrows", "refCanale", 5); wait(1)
        DeleteAndType("CARTACEO"); wait(1)
        type(Key.TAB*3)
        if verifyExistReg("rrows", "refUnchecked", 5):
            tryClickReg("rrows", "refUnchecked", 5); wait(1)

    #TIPOLOGIA REFERENTI
    tryClickReg("rrows", "tiporefStart", 5); wait(0.5)
    tryClickReg("rrows", "tiporefStart", 5); wait(0.5)
    tab(1); wait(3)
    listTipo = ["MANDATARIO", "MASTER", "REFERENTE PER CAMPAGNA"]

    while(True):
        tab(4,0); wait(1)
        t=GetText()
        print "TIPU--"+t
        if t in listTipo:
            listTipo.remove(t)
        print "LISTA--",listTipo
        tab(11,0); wait(2)
        if(GetText()==""):
            break
    for tipo in listTipo:
        tryClickReg("rrow3", "nuovo", 5); wait(1)
        type(inputData["prefissoCelular"] + Key.TAB); wait(1)
        type(inputData["celular"] + Key.TAB*2); wait(1)
        type(inputData["mail"].split("@")[0])
        type("@")
        #type(r";", KeyModifier.CTRL + KeyModifier.ALT)
        type(inputData["mail"].split("@")[1]); wait(0.5)
        type(Key.TAB + tipo + Key.TAB*5); wait(1)
        type(inputData["prefissoTelFisso"] + Key.TAB); wait(1)
        type(inputData["telFisso"] + Key.TAB)

    click(Regions.pagepoint); type(Key.PAGE_UP); wait(1)
    tryClickReg("rrows","acquisizioneConsensi",10)
    tryClickReg("rrows", "consensoP1mobile", 10)
    DeleteAndType("NO"); wait(0.3); tab(1, 0.5)
    type(usernameCRM); wait(0.3); tab(2, 0.5)
    
    #type("SI"); wait(0.3); tab(1, 0.5)    PROLLY NU TREBE
    #type(usernameCRM); wait(0.3)
    #click(Regions.pagepoint); type(Key.PAGE_DOWN); wait(1)
    #tryClickReg("rrow1", "consensoI2", 5)
    #DeleteAndType("SI"); wait(0.3); tab(1, 0.5)
    #type(usernameCRM); wait(0.3); tab(1, 0.5)
    #click(Regions.pagepoint); type(Key.PAGE_UP); wait(1)

def selectModalita():
    selects = Regions.getRegion("rrow1").findAll(AutoImages.imgDict["selectPopUp2"])
    sorted_selects = sorted(selects, key=lambda m:m.y)
    for modalitappi in range(1,3):
        equals = verifyLineValues([inputData["comuneSedeLegale2"],
                    inputData["partTopSedeLegale"]+ " " +inputData["viaSedeLegale"]+ " " +inputData["civicoSedeLegale"]]
                , [1, 7, 0], "indppStart", sorted_selects[modalitappi], "capclear")
    click(sorted_selects[0]); wait(0.5)
    tab(6); type("CAB"+Key.TAB); wait(0.5)
    type(inputData["modalitaPagamento"][10:15]+Key.ENTER); wait(1)
    if verifyExist("rwindow", "ricercaABI", 5):
        wait(2)
        org=find(AutoImages.imgDict["ricercaABI"]).getCenter()
        r1=Region(org.x-26, org.y+23, 56, 23)
        while(len(r1.text())>1):
            r1.highlight(1)
            print "TEXT " + r1.text()
            if r1.text()==inputData["modalitaPagamento"][5:10]:
                doubleClick(r1); return True
            r1=Region(r1.x, r1.y+22, 56, 23)
        if not verifyExistReg("rwindow", "rABIdown", 5):
            return False
        r1=Region(r1.x, r1.y-22, 56, 23)
        click(r1)
        prev="Nothing"
        while(prev!=r1.text()):
            print "TEXT " + r1.text()
            if r1.text()==inputData["modalitaPagamento"][5:10]:
                doubleClick(r1); return True
            prev=r1.text()
            tryClick("rwindow", "rABIdown", 5); wait(0.5)
        return False

def billing():
    global CFA
    
    if not verifyExistReg("rrows", "billingSel", 5):
        tryClickReg("rrow2", "billingAccount", 5); wait(2)
    tryClickReg("rrow1", "nuovo", 5); wait(5)

    click(Regions.pagepoint); type(Key.PAGE_DOWN*2+Key.UP); wait(1)
    tryClickReg("rrow1", "CFA", 5); wait(0.2)
    CFA=GetText()
    tryClickReg("rrow3ex", "termScadenzaFattura", 5); wait(0.2)
    DeleteAndType("30")
    selects = Regions.getRegion("rrow3ex").findAll(AutoImages.imgDict["selectPopUp"])
    sorted_selects = sorted(selects, key=lambda m:m.y)
    for billinppi in range(0,2):
        verifyLineValues([inputData["comuneSedeLegale2"],
                    inputData["partTopSedeLegale"]+ " " +inputData["viaSedeLegale"]+ " " +inputData["civicoSedeLegale"]]
                , [1, 7, 0], "indppStart", sorted_selects[billinppi], "capclear")
    click(Regions.pagepoint); type(Key.PAGE_UP); wait(1)
    
    if inputData["modalitaPagamento"]!="BOLLETTINO":
        tryClickReg("rrow3", "modalitaDiPagamento", 5); wait(0.5)
        DeleteAndType("Domi"); tab(1); wait(1)
        tryClickReg("rrow1", "modalitaHeader", 5); wait(2)
        click(Regions.pagepoint); type(Key.PAGE_DOWN*2); wait(1)
        tryClickReg("rrow1", "paese", 5); wait(0.5)
        type(inputData["modalitaPagamento"][0:2]+ Key.TAB); wait(0.5)
        type(inputData["modalitaPagamento"][2:4]+ Key.TAB); wait(0.5)
        type(inputData["modalitaPagamento"][4]+ Key.TAB*5); wait(1)
        type(inputData["modalitaPagamento"][15:len(inputData["modalitaPagamento"])]+ Key.TAB*4); wait(1)
        type(inputData["cf"]+ Key.TAB*9); wait(1)
        type(inputData["cf"]); wait(0.5)
        if selectModalita()==False:
            raise Exception("Could not find ABI from modalitapagamento(text search)!")
        click(Regions.pagepoint); type(Key.PAGE_UP*2); wait(1)
    popup("B4 CONFERMA")
    tryClickReg("rrow1", "conferma", 5); wait(3)

#sv-contratti cannot go back
def contratti():
    global CFA

    if not verifyExistReg("rrows", "contrattiSel", 5):
        tryClickReg("rrow2", "contratti", 5); wait(1)
    tryClickReg("rrow1", "nuovo", 5); wait(3)
    tryClickReg("rrow2", "dataStipula", 5); wait(1)
    DeleteAndType(inputData["dataContratto"][0:10]+Key.TAB); wait(1)
    tryClickReg("rrow1", "nrContratto", 5); wait(1.5)
    tryClickReg("rrows", "opzionePros", 5); wait(1)
    DeleteAndType("Fattura Totale"+Key.TAB*12); wait(1)
    #type(usernameCRM+Key.TAB); wait(1)    PROLLY NU TREBE
    #type("24"+Key.TAB*8); wait(1)
    #type(usernameCRM+Key.TAB*4); wait(1)
    #tryClickReg("rrows", "opFail", 5)
    #DeleteAndType(usernameCRM+Key.TAB); wait(1)
    #click(Regions.pagepoint); type(Key.RIGHT*3); wait(0.3)
    if verifyExistReg("rrow3ex", "ppcfa", 5):
        popup("B4 CFA")
        ppcfa=Regions.getRegion("rrow3ex").find(AutoImages.imgDict["ppcfa"]); wait(1)
        equals = verifyLineValues([CFA], [0, 8], "billppStart", ppcfa)

    click(Regions.pagepoint); type(Key.PAGE_DOWN); wait(1)
    tryClickReg("rrow1", "nuovo", 5); wait(2)
    tryClickReg("rrow1", "cognome", 5); wait(0.2)
    pp=Regions.getRegion("rrow1").find(AutoImages.imgDict["selectPopUp"]); wait(1)
    verifyLineValues([inputData["cf"]], [3, 14], "refppStart", pp)
    
    click(Regions.pagepoint); type(Key.PAGE_DOWN); wait(1)
    for i in range(11, 28, 8):
        tryClickReg("rrow1", "tipologiaRef", 5); wait(2)
        tryClickReg("rrow3", "selectPopUp", 5); wait(0.5)
        type(Key.TAB*i); wait(0.5)
        type(Key.ENTER); wait(1)
    click(Regions.pagepoint); type(Key.PAGE_UP); wait(1)
    tryClickReg("rrow1", "indirizzi", 5); wait(1)
    click(Regions.pagepoint); type(Key.PAGE_DOWN); wait(1)

    selects = Regions.getRegion("rrow2").nearby().findAll(AutoImages.imgDict["selectPopUp2"])
    sorted_selects = sorted(selects, key=lambda m:m.y)
    for ctri in range(0,4):
        click(sorted_selects[ctri]); wait(1)
        if(ctri%2==0):
            verifyLineValues([inputData["comuneSedeLegale2"],
                        inputData["partTopSedeLegale"]+ " " +inputData["viaSedeLegale"]+ " " +inputData["civicoSedeLegale"]]
                    , [1, 7, 0], "indppStart", sorted_selects[ctri], "capclear")
        else:
            verifyLineValues([inputData["cf"], inputData["nome"]], [0, 1, 25], "refppStart",
                    sorted_selects[ctri], "capclear")
    selects = Regions.getRegion("rrow2").right().findAll(AutoImages.imgDict["selectPopUp2"])
    sorted_selects = sorted(selects, key=lambda m:m.y)
    for ctri2 in range(0,2):
        verifyLineValues([inputData["cf"], inputData["nome"]], [0, 1, 25], "refppStart",
                 sorted_selects[ctri2], "capclear")

    click(Regions.pagepoint); type(Key.UP*2); wait(1)
    tryClickReg("rrows", "offerte", 5); wait(1)
    tryClickReg("rrow1", "nuovo", 5); wait(2)
    if len(getTipoVendita(inputData["tipovendita"]))>9:
        type("OFF_08_00000079"+Key.TAB); wait(7)
    else:
        type("OFF_08_00000058"+Key.TAB); wait(7)
    type(Key.TAB*7+"CD"+Key.TAB); wait(1)
    click(Regions.pagepoint); type(Key.PAGE_UP*2); wait(1)

    popup("B4 CONFERMA")
    tryClickReg("rrows", "conferma", 5); wait(4)
    click(Regions.pagepoint); type(Key.PAGE_DOWN); wait(1)    

def ordini():#TIM TUTTO BUSINESS
    if not verifyExistReg("rrows", "ordiniSel", 5):
        tryClickReg("rrows", "ordini", 5); wait(1)
    tryClickReg("rrow1", "nuovo", 5); wait(5)
    DeleteAndType("LOTTO"+Key.TAB); wait(1)
    type(Key.TAB*2+inputData["dataContratto"][0:10]+Key.TAB); wait(1)
    tryClickReg("rrow1", "condiceOrdine", 5); wait(2)
    tryClickReg("rrow3ex", "pisi", 5); wait(0.5)
    DeleteAndType("SI"+Key.TAB); wait(1)
    click(Regions.pagepoint); type(Key.PAGE_DOWN); wait(1)
    tryClickReg("rrow1", "nuovoServ", 5); wait(2)
    type(Key.TAB*7+"RICARI"); wait(0.2)
    tryClickReg("rwindow", "esegui", 5); wait(1)
    tryClickReg("rrow3ex", "clickPopUp", 5); wait(5)
    tryClickReg("rrows", "configura", 30); wait(5)
    type(Key.ENTER); wait(1)
    if verifyExistReg("rmid", "secyes", 5):
        tryClickReg("rmid", "secyes", 3); wait(2)

    tryClickReg("rrow1", "iniziative", 5); wait(3)
    tryClickReg("rrow1", "tipovendita", 5); wait(1)
    type(getTipoVendita(inputData["tipovendita"])+Key.ENTER); wait(0.5)
    type(Key.TAB+"1")
    popup("NEED TO DO DIS U!")
    tryClickReg("rrow2", "aggiungi", 5); wait(1)
    tryClickReg("rrow3ex", "conferma", 5); wait(5)

    click(Regions.pagepoint); type(Key.PAGE_DOWN); wait(1)
    tryClickReg("rrow1", "datiSpedizione", 5); wait(1)
    click(Regions.pagepoint); type(Key.PAGE_DOWN); wait(1)
    selects = Regions.getRegion("rrow2").findAll(AutoImages.imgDict["selectPopUp2"])
    sorted_selects = sorted(selects, key=lambda m:m.y)
    verifyLineValues([inputData["comuneSedeLegale2"],
                inputData["partTopSedeLegale"]+ " " +inputData["viaSedeLegale"]+ " " +inputData["civicoSedeLegale"]]
            , [1, 7, 0], "indppStart", sorted_selects[0], "capclear")
    verifyLineValues([inputData["cf"]], [3, 14], "refppStart", sorted_selects[1], "capclear")
    DeleteAndType(inputData["prefissoTelFisso"]+inputData["telFisso"]+r"/"+inputData["prefissoCelular"]+inputData["celular"]+Key.TAB)
    #DeleteAndType(inputData["prefissoTelFisso"]+inputData["telFisso"]+r"&"+inputData["prefissoCelular"]+inputData["celular"]+Key.TAB)

    tryClickReg("rrow1", "attivita", 5); wait(1.5)
    tryClickReg("rrow1", "nuovo", 5); wait(2)
    type("VERBAL ORDERING"+Key.TAB); wait(2)
    type(Key.TAB*5); wait(0.5)
    type("IVR "+inputData["ivr"]+Key.TAB); wait(1)
    tab(-6)
    type("Completata"+Key.TAB); wait(1)

    if not ftpCopyPDF(inputData["ragioneSociale"], inputData["cc"], inputData["dataContratto"]):
        if inputData["giuridica"]!="ditta":
            raise Exception("Cannot find PDF for forma giuridica!")
    else:
        tryClickReg("rrows", "documenti", 5); wait(1)
        tryClickReg("rrow1", "allegaFile", 5); wait(1)
        tryClickReg("rwindow", "repozitory", 7); wait(0.5)
        type(Key.TAB*7+Key.DOWN+Key.UP); wait(0.5)
        while(True):
            type(Key.TAB)
            t=GetText()
            if t=="":
                type(Key.ESC)
                raise Exception("Cannot find File in Repozitory!")
            if inputData["ragioneSociale"] in t:
                type(Key.ENTER*2)
                break
            else:
                type(Key.TAB*7+Key.DOWN); wait(0.5)
        wait(3); type(Key.TAB*6); wait(1)
        type("VISURA CAMERALE"+Key.ENTER); wait(2)
        
    click(Regions.pagepoint); type(Key.PAGE_UP*2); wait(1)
    trydClickReg("rrow1", "condiceOrdine2", 5); wait(0.5)
    cdiceOrdine=GetText(); wait(0.5)
    return cdiceOrdine

def finishInvio():
    tryClickReg("rrow1", "invio", 5); wait(10)
    tryClickReg("rrow1", "continua", 5); wait(2)