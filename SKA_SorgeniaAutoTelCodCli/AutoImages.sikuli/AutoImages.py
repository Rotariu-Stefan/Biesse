from sikuli import *
from time import strftime
import os

class Regions():
    rall=Region(0,0,getW(),getH())
    rstarticon=Region(0,getH()-100,100,100)
    
    #create app regions
    @staticmethod
    def createRegs(appname):
        rwin=getWindow(appname)
        Regions.rwindow=rwin
        Regions.rrow1=Region(rwin.x, rwin.y+220, 325, rwin.h-280)
        Regions.pagepoint=Location(rwin.x+10, rwin.y+rwin.h/2)
    
    #afiseaza regiunile
    @staticmethod
    def showRegs():
        for i in Regions.__dict__.items():
            if(i[0].startswith("r")):
                i[1].highlight(2)
                print i[0], "---->", i[1]
    
class AutoImages():
    
    imgDict = {}
    
    @staticmethod
    def loadImages():
        AutoImages.addImage("enterSorgenia", Pattern("ScrgeniaCall.png").similar(0.35))
        AutoImages.addImage("ieSorgenia", "ViS0rgenlaCa.png")
        AutoImages.addImage("home", "HOMEI1IRABen.png")
        AutoImages.addImage("clienti", Pattern("clientiimg.png").similar(0.81).targetOffset(20,0))
        AutoImages.addImage("tutticlienti", Pattern("Tuttiiclient.png").targetOffset(0,200))
        AutoImages.addImage("clientisel", Pattern("clientisel.png").similar(0.81).targetOffset(25,0))
        AutoImages.addImage("telfis", Pattern("TelefonoFiss.png").targetOffset(86,0))
        AutoImages.addImage("telcel", Pattern("QTelefunoCel.png").targetOffset(98,2))
        AutoImages.addImage("telass", Pattern("Numtelefonic.png").targetOffset(114,0))
        AutoImages.addImage("ragione", Pattern("ragiosoc.png").targetOffset(0,23))
        AutoImages.addImage("codcli", Pattern("codcli.png").targetOffset(69,3))
        AutoImages.addImage("datacre", "datajos.png")
        AutoImages.addImage("menu", Pattern("menupoint.png").targetOffset(0,30))
        AutoImages.addImage("contatti", "contatti.png")
        AutoImages.addImage("contattisel", "contattisel.png")
        AutoImages.addImage("condice", Pattern("condice.png").targetOffset(-23,23))
        AutoImages.addImage("menu2", Pattern("menu2.png").targetOffset(-29,46))
    
    @staticmethod
    def addImage(name, img):
    	AutoImages.imgDict[name] = img
    
    @staticmethod
    def getImage(name):
    	return AutoImages.imgDict[name]

def findImage(regiune, imagine, timp):
    imreg=wait(AutoImages.imgDict[imagine], timp)
    if(imreg==None):
        raise Exception("Exceptie la cautare pe imaginea -> " + imagine)
    else:
        return imreg
    
#sv-verifica existenta imagine
def verifyExist(regiune, imagine, timp):
    if exists(AutoImages.imgDict[imagine], timp):
    	return True
    else:
    	return False
    
#sv-verifica existenta imagine in regiune
def verifyExistReg(regiune, imagine, timp):
    if getattr(Regions, regiune).exists(AutoImages.imgDict[imagine], timp):
    	return True
    else:
    	return False
    	
#sv-ashtepta aparitia
def waitImage(regiune, imagine, timp):
    if exists(AutoImages.imgDict[imagine], timp):
    	return "OK"
    else:
    	raise Exception("Cannot find image '" + imagine +"'!")
    
#sv-da click sau ERR    
def clickImage(regiune, imagine, timp):
    try:
    	click(wait(AutoImages.imgDict[imagine], timp))
    except:
    	raise Exception("Cannot click image '" + imagine +"'!")
    
#sv-da click in regiune sau ERR
def clickImageReg(regiune, imagine, timp):
    try:
    	click(getattr(Regions, regiune).wait(AutoImages.imgDict[imagine], timp))
    except:
    	raise Exception("Region '" + regiune + "' Cannot click image '" + imagine +"'!")

#sv-da dublu click sau ERR    
def dclickImage(regiune, imagine, timp):
    try:
    	doubleClick(wait(AutoImages.imgDict[imagine], timp))
    except:
    	raise Exception("Cannot double click image '" + imagine +"'!")
    
#sv-da dublu click in regiune sau ERR
def dclickImageReg(regiune, imagine, timp):
    try:
    	doubleClick(getattr(Regions, regiune).wait(AutoImages.imgDict[imagine], timp))
    except:
    	raise Exception("Region '" + regiune + "' Cannot double click image '" + imagine +"'!")    