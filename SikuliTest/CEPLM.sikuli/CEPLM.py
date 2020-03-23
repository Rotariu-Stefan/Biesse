from sikuli import *
Settings.ActionLogs=False
Settings.InfoLogs=False
Settings.DebugLogs=False
myPath = os.path.abspath(os.path.join(getBundlePath(), os.pardir))
import csv
import sys
import pyPdf

def pdfGetGiuridica(path):
      content = ""
      pdf = pyPdf.PdfFileReader(file(path, "rb"))
      for i in range(0, pdf.getNumPages()):
          content += pdf.getPage(i).extractText()
      fi1=content.find("Formagiuridica")
      fi2=content.find("Codici", fi1)
      return content[fi1+14:fi2]
      #return content.encode("UTF-8","ignore") - all text

def pdfGetPiva(path):
      content = ""
      pdf = pyPdf.PdfFileReader(file(path, "rb"))
      for i in range(0, pdf.getNumPages()):
          content += pdf.getPage(i).extractText()
      fi1=content.find("PartitaIva")
      fi2=content.find("CCIAA", fi1)
      return content[fi1+10:fi2]    

print pdfGetGiuridica(r"D:\SKTest\BORDON LINO.pdf")
print pdfGetPiva(r"D:\SKTest\BORDON LINO.pdf")