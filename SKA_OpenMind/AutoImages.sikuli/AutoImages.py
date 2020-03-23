from sikuli import *
from time import strftime
import os

class Regions():

    regDict = {}
    
    @staticmethod
    def addRegion(name, dreg):
    	Regions.regDict[name] = dreg
    
    @staticmethod
    def getRegion(name):
        return Regions.regDict[name]
    
    #gets the window region of an open application(or None if the no big enough window is found)
    @staticmethod
    def getWindow(appname):
        a=App.focus(appname)
        i=0
        appw=a.window(i)
        while(appw):        
            if appw.w>getW()/2 and appw.h>getH()/2:
                return appw
            i+=1
            appw=a.window(i)
        return None       

    @staticmethod
    def createRegs(appname):
        Regions.addRegion("rall",Region(0,0,getW(),getH()))
        Regions.addRegion("rstarticon",Region(0,getH()-100,100,100))
        
        rwin=Regions.getWindow(appname)
        Regions.addRegion("rwindow",rwin)
        Regions.addRegion("rrow1",Region(rwin.x, rwin.y+220, 325, rwin.h-280))
        Regions.addRegion("rrow2",Region(rwin.x+325, rwin.y+220, 275, rwin.h-280))
        Regions.addRegion("rrow3",Region(rwin.x+600, rwin.y+220, 275, rwin.h-280))
        Regions.addRegion("rrow3ex",Region(rwin.x+600, rwin.y+220, rwin.w-600, rwin.h-280))
        Regions.addRegion("rrows",Region(rwin.x, rwin.y+220, rwin.w, rwin.h-280))
        Regions.addRegion("rtop",Region(rwin.x, rwin.y, rwin.w, 125))
        Regions.addRegion("rmenu",Region(rwin.x, rwin.y+100, rwin.w, 190))
        Regions.addRegion("rbottom",Region(rwin.x, rwin.y+rwin.h-85, rwin.w, 85))
        Regions.addRegion("rmidcol",Region(rwin.x+rwin.w/4, rwin.y, rwin.w/2, rwin.h))
        Regions.addRegion("rmid",Region(rwin.x+rwin.w/4, rwin.y+rwin.h/4, rwin.w/2, rwin.h/2))
                                                
        Regions.pagepoint=Location(rwin.x+10, rwin.y+rwin.h/2)                     
    
class AutoImages():
    
    imgDict = {}

    @staticmethod
    def addImage(name, img):
    	AutoImages.imgDict[name] = img
    
    @staticmethod
    def getImage(name):
    	return AutoImages.imgDict[name]    
    
    @staticmethod
    def loadImages():
        AutoImages.addImage("crmlogged", "FileEditView.png")
    	AutoImages.addImage("citrixLogo", "GtrixDesktop.png")
    	AutoImages.addImage("citrixLogo2", "ECitrixDeskt.png")
    	AutoImages.addImage("citrixCom", "1375861368050.png")
        AutoImages.addImage("citrixClose", "7.png")
    	AutoImages.addImage("start","start.png")
    	AutoImages.addImage("LogOff", "LogOff.png")
    	AutoImages.addImage("programs", "programs.png")
    	AutoImages.addImage("crmaffari", "crmaffari.png")
    	AutoImages.addImage("usernameAffari", Pattern("usernameAffari.png").targetOffset(48,-1))
    	AutoImages.addImage("rememberPasswordAffari", "rememberPasswordAffari.png")
    	AutoImages.addImage("secureIntCon", "secureIntCon.png")
    	AutoImages.addImage("welcomeBack", "welcomeBack.png")
    	AutoImages.addImage("closeLayer", "closeLayer.png")
    	AutoImages.addImage("clienti", "clienti.png")
    	AutoImages.addImage("pivaField", Pattern("pivaField.png").targetOffset(46,0))
    	AutoImages.addImage("codiceFiscaleField", Pattern("codiceFiscaleField.png").targetOffset(49,0))
    	AutoImages.addImage("clientiCerca", "clientiCerca.png")
    	AutoImages.addImage("cfpivaNonEsiste", Pattern("cfpivaNonEsiste.png").targetOffset(-62,22))
    	AutoImages.addImage("naturaCliente", Pattern("naturaCliente.png").targetOffset(56,0))
    	AutoImages.addImage("giuridica", Pattern("NaturaGiurid.png").targetOffset(70,0))
    	AutoImages.addImage("siglaceva", Pattern("SiglaPrcvinc.png").targetOffset(92,0))
    	AutoImages.addImage("piva", Pattern("piva.png").targetOffset(38,0))
    	AutoImages.addImage("partTopSedeLegale", Pattern("partTopSedeLegale.png").targetOffset(69,0))
    	AutoImages.addImage("viaSedeLegale", Pattern("viaSedeLegale.png").similar(0.94).targetOffset(49,0))
    	AutoImages.addImage("civicoSedeLegale", Pattern("civicoSedeLegale.png").targetOffset(59,0))
    	AutoImages.addImage("datiUtente", Pattern("datiUtente.png").targetOffset(0,19))
    	AutoImages.addImage("prefissoContatto", Pattern("prefissoContatto.png").targetOffset(60,0))
    	AutoImages.addImage("accettaDatiUtente", "Accetta Dati Utente.png")
    	AutoImages.addImage("certifica", "certifica.png")
    	AutoImages.addImage("accettaDatiACLI", "Accetta Dati ACLI.png")
    	AutoImages.addImage("indirizziHover", "1372686649747.png")
    	AutoImages.addImage("indirizzi", "Indirizzi-1.png")
    	AutoImages.addImage("indirizziStart", Pattern("Indirizzi.png").targetOffset(-62,53))
    	AutoImages.addImage("nuovo", "nuovo.png")
    	AutoImages.addImage("referenti", "referenti.png")
    	AutoImages.addImage("referentiSel", "Rcfcrcnti.png")
        AutoImages.addImage("acquisizioneConsensi", "acquisizioneConsensi.png")
        AutoImages.addImage("consensoP1mobile", Pattern("consensoP1Mobile.png").targetOffset(120,0))
        AutoImages.addImage("billingAccount", "billingAccount.png")
        AutoImages.addImage("billingSel", "BillingAccou.png")
        AutoImages.addImage("modalitaDiPagamento", Pattern("modalitaDiPagamento.png").targetOffset(-41,22))
        AutoImages.addImage("termScadenzaFattura", Pattern("1373961803186.png").targetOffset(86,0))
        AutoImages.addImage("selectPopUp", "selectPopUp.png")
        AutoImages.addImage("selectPopUp2", Pattern("selectPopUp.png").similar(0.81))
        AutoImages.addImage("refCanale", Pattern("EanalediComu.png").targetOffset(-32,23))
        AutoImages.addImage("refUnchecked", "1375791779675.png")
        AutoImages.addImage("conferma", "Conferma.png")
        AutoImages.addImage("contratti", "Contratti.png")
        AutoImages.addImage("contrattiSel", "Ccntratti.png")
        AutoImages.addImage("ppcfa", Pattern("CFA-1.png").targetOffset(61,1))
        AutoImages.addImage("dataStipula", Pattern("DataStipula.png").targetOffset(-26,21))
        AutoImages.addImage("nrContratto", Pattern("NumeroContra.png").targetOffset(-35,22))
        AutoImages.addImage("opzionePros", Pattern("OpzioneProsp.png").targetOffset(84,0))
        AutoImages.addImage("cognome", Pattern("Cugnome.png").targetOffset(-21,19))
        AutoImages.addImage("tipologiaRef", Pattern("TipologiaRef.png").targetOffset(145,-1))
        AutoImages.addImage("offerte", "Offertc.png")
        AutoImages.addImage("ordini", Pattern("OffcrtcIOrdi.png").targetOffset(30,0))
        AutoImages.addImage("ordiniSel", "Drdini.png")
        AutoImages.addImage("condiceOrdine", Pattern("CcdiceDrdinc.png").targetOffset(-22,22))
        AutoImages.addImage("pisi", Pattern("PI.png").targetOffset(29,-1))
        AutoImages.addImage("nuovoServ", "NuovoServizi.png")
        AutoImages.addImage("esegui", "Esooui.png")
        AutoImages.addImage("clickPopUp", Pattern("OKChiudi.png").targetOffset(-24,0))
        AutoImages.addImage("configura", "Contiuura.png")
        AutoImages.addImage("secyes", Pattern("YeNoMmeInfu.png").targetOffset(-83,2))
        AutoImages.addImage("iniziative", "1375972843758.png")
        AutoImages.addImage("tipovendita", Pattern("llilYJfflILI.png").targetOffset(0,30))
        AutoImages.addImage("aggiungi", "Aggiungi.png")
        AutoImages.addImage("datiSpedizione", "DatiSpedizic.png")
        AutoImages.addImage("attivita", "Attivit.png")
        AutoImages.addImage("documenti", "Dcacumanti.png")
        AutoImages.addImage("allegaFile", "AlleqaFile.png")
        AutoImages.addImage("repozitory", "1376045490551.png")
        AutoImages.addImage("condiceOrdine2", Pattern("CcdiceOrdinc.png").targetOffset(66,-1))
        AutoImages.addImage("invio", "Invic.png")
        AutoImages.addImage("continua", "FIC0nUnua.png")
        AutoImages.addImage("referentiStart", Pattern("Referenti-1.png").targetOffset(-66,53))
        AutoImages.addImage("tiporefStart", Pattern("TipologiaRef-1.png").targetOffset(-103,48))
        AutoImages.addImage("indppStart", Pattern("QIndirizziMi.png").targetOffset(-39,53))
        AutoImages.addImage("refppStart", Pattern("NuovoI.png").targetOffset(-30,50))
        AutoImages.addImage("billppStart", Pattern("iBillingAcco.png").targetOffset(-44,52))
        AutoImages.addImage("modalitaHeader", "McdalitdiPau.png")
        AutoImages.addImage("paese", Pattern("Paese.png").targetOffset(39,0))
        AutoImages.addImage("ricercaABI", "1111.png")
        AutoImages.addImage("rABIdown", Pattern("2AVI.png").targetOffset(0,85))
        AutoImages.addImage("CFA", Pattern("CFA.png").targetOffset(63,0))
        #AutoImages.addImage("consensoI2", Pattern("CcnsensoI2Mo.png").targetOffset(132,0))    #PROLLY NU TREBE
        #AutoImages.addImage("opFail", Pattern("ConssnsoI2Mo.png").targetOffset(128,14))    #PROLLY NU TREBE
        #AutoImages.addImage("capclear", Pattern("CAP.png").targetOffset(40,1))            #DOAR DACA CLIPCLEAR FAIL
        AutoImages.addImage("ciudi", Pattern("OKChiudi-1.png").targetOffset(17,-1))
    
#sv-verifica existenta imagine
def verifyExist(regiune, imagine, timp):
    if exists(AutoImages.imgDict[imagine], timp):
    	return True
    else:
    	return False
    
#sv-verifica existenta imagine in regiune
def verifyExistReg(regiune, imagine, timp):
    if Regions.getRegion(regiune).exists(AutoImages.imgDict[imagine], timp):
    	return True
    else:
    	return False
    	
#sv-ashtepta aparitia sau ERR
def waitImage(regiune, imagine, timp):
    if exists(AutoImages.imgDict[imagine], timp):
    	return "OK"
    else:
    	raise Exception("Cannot find image '" + imagine +"'!")

#sv-ashtepta aparitia in regiune sau ERR
def waitImageReg(regiune, imagine, timp):
    if Regions.getRegion(regiune).exists(AutoImages.imgDict[imagine], timp):
    	return "OK"
    else:
    	raise Exception("Cannot find image '" + imagine +"'!")    
    
#sv-da click sau ERR    
def tryClick(regiune,imagine,timp):
    try:
    	click(wait(AutoImages.imgDict[imagine], timp))
    except:
    	raise Exception("Cannot click image '" + imagine +"'!")
    
#sv-da click in regiune sau ERR
def tryClickReg(regiune,imagine,timp):
    try:
    	click(Regions.getRegion(regiune).wait(AutoImages.imgDict[imagine], timp))
    except:
    	raise Exception("Region '" + regiune + "' Cannot click image '" + imagine +"'!")

#sv-da click sau ERR    
def trydClick(regiune,imagine,timp):
    try:
    	doubleClick(wait(AutoImages.imgDict[imagine], timp))
    except:
    	raise Exception("Cannot click image '" + imagine +"'!")
    
#sv-da click in regiune sau ERR
def trydClickReg(regiune,imagine,timp):
    try:
    	doubleClick(Regions.getRegion(regiune).wait(AutoImages.imgDict[imagine], timp))
    except:
    	raise Exception("Region '" + regiune + "' Cannot click image '" + imagine +"'!")    