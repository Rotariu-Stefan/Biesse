from sikuli import *

class AutoRegions():

	regDict = {}
	
	def __init__(self):
		self.regDict = {}
		self.addRegion("regScreen", Region(Screen(0).getBounds()))
		self.addRegion("regUP", Region(Region(5,9,1013,190)))
		self.addRegion("regUPdinamic", Region(0,0,Screen(0).getW(),Screen(0).getH()/4))
		self.addRegion("regAttivaInfo", Region(Region(5,117,1001,149)))
		self.addRegion("regAttivaInfodinamic", Region(0,0,Screen(0).getW(),Screen(0).getH()/3))
		self.addRegion("SV", Region(0, Screen(0).getH()/2, Screen(0).getW()/5, Screen(0).getH()/3))
		
	def addRegion(self, name, img):
		self.regDict[name] = img
		
	def getRegion(self, name):
		return self.regDict[name]