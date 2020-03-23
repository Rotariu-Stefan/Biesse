from sikuli import *
import sys

print "Program started!"
wait(1)
print "Working..."
wait(3)
if len(sys.argv)>1:
    print "From ",sys.argv[1]
    print "To ",sys.argv[2]
    print "Err ",sys.argv[3]
wait(1)
print "Done!"