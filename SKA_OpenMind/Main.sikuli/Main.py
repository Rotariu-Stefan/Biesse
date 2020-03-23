from sikuli import *
import sys
import os
import Automation
reload(Automation)
from Automation import *
Settings.ActionLogs=False
Settings.InfoLogs=False
Settings.DebugLogs=False
CheckNumLock()
CheckCapsLock()
myPath = os.path.abspath(os.path.join(getBundlePath(), os.pardir))

MakeFiles()
AutoImages.loadImages()
#citrixMax()
Regions.createRegs("Desktop Viewer")

OpenCRM()

reader = getXLSRows(myPath + "\\TIM BSS.xls", 6, 6)
processInputRow(reader[0])
printData()

startClienti()
popup("BEFORE CERT"); wait(5)
certificazione()
popup("BEFORE INDI"); wait(5)
indirizzo()
popup("BEFORE REF"); wait(5)
referenti()
#popup("BEFORE BILL"); wait(5)
#billing()
#popup("BEFORE CONT"); wait(5)
#contratti()
#popup("BEFORE ORD"); wait(5)
#condiceOrdine=ordini()
#print condiceOrdine
#completeXLS(myPath + "\\TIM CCD.xls", 6, condiceOrdine)

#----finishInvio()

#LogOffCRM()

#citrixMin()
print "===========================!!!SUCCESS!!!==========================="    