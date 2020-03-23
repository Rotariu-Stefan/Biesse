from sikuli import *
from time import strftime
import os
myPath = os.path.dirname(getBundlePath())
if not myPath in sys.path: sys.path.append(myPath)
from AutoRegions import *

myRegions = AutoRegions()
class AutoImages():
    def __init__(self):
    	self.imgDict = {}
    	self.addImage("noVPNconn", "noVPNconn.png")
        self.addImage("crmLoginPic", "1374055587447.png")
        self.addImage("siebelVers", "siebelVers.png")
        self.addImage("welcomeBack", "welcomeBack.png")
        self.addImage("clienti1", "1374058318055.png")
        self.addImage("clienti1hover", "1374058337045.png")
        self.addImage("clienti2", Pattern("clienti2.png").targetOffset(40,0))
        self.addImage("clienti2hover", Pattern("1374065003166.png").targetOffset(39,-1))
        self.addImage("cerca", "1374066571789.png")
        self.addImage("codfis", Pattern("1374067793034.png").targetOffset(1,24))
        self.addImage("attivita", "attivita.png")
        self.addImage("tel", Pattern("telefono.png").targetOffset(33,0))
        self.addImage("cel", Pattern("cellulare.png").targetOffset(32,0))
        self.addImage("mail", Pattern("mail.png").targetOffset(26,0))
        self.addImage("ieSiebel", "ESiebelEnerg.png")
        
    def addImage(self, name, img):
    	self.imgDict[name] = img

    def getImage(self, name):
    	return self.imgDict[name]

    def findImage(self, regiune, imagine, timp):
        im=myRegions.regDict[regiune].wait(self.imgDict[imagine], timp)
        if(im==None):
            raise Exception("Exceptie la cautare pe imaginea -> " + imagine)
        else:
            return im
    
    def waitImage(self, regiune, imagine, timp):
        if myRegions.regDict[regiune].exists(self.imgDict[imagine], timp)==None:
            return "Nu am gasit imaginea" + imagine
        else:
            return "OK"
    
    def clickImage(self,regiune,imagine,timp):
        try:
            click(myRegions.regDict[regiune].wait(self.imgDict[imagine], timp))
        except:
            raise Exception("Exceptie la click pe imaginea -> " + imagine)

    def dclickImage(self, regiune, imagine, timp):
        try:
            doubleClick(myRegions.regDict["regAttivaInfodinamic"].wait(myImages.imgDict["tel"],30))
        except:
            raise Exception("Exceptie la dublu click pe imaginea -> " + imagine)