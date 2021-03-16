import pywinauto, time
from pywinauto.application import Application
from pywinauto.keyboard import send_keys
import unittest
import os.path

app = Application().Start('TestingPr3.exe')
time.sleep(1)

app = Application(backend="uia").connect(title=u"Вариант 10")
main_dlg = app.window(title='Вариант 10')
InputInFile = main_dlg.window(auto_id="InputInFile")

Selector = main_dlg.ComboBox
Selector.click_input()
#Selector.child_window(title="Записать в файл").click_input()
Selector.type_keys("{DOWN}""{DOWN}""{ENTER}")

#time.sleep(1)
ArrayText = main_dlg.window(auto_id="OutputWindow")
resArray = ArrayText.legacy_properties()["Value"]
#main_dlg.print_control_identifiers()

#main_dlg.print_control_identifiers()
#print("resArray = ", resArray)

Selector.click_input()
Selector.type_keys("{UP}""{ENTER}")

FileName = main_dlg.window(auto_id="FileName")
FileName.type_keys("test")



InputInFile.click_input()

resFile = ArrayText.legacy_properties()["Value"]
#print(resFile)

#main_dlg.print_control_identifiers()

class UItest(unittest.TestCase):
    def testArray(self):
        self.assertEqual(resArray,"Содержимое массива:\nПривет, мир\nп\nаа\n11 тест\n")
    def testFile(self):
        self.assertEqual(resFile,"Содержимое файла test.txt:\nПривет, мир\nп\nаа\n11 тест\n")
    def testSelectorTest(self):
        self.assertEqual(Selector.item_count(),2)
        app.window(title='Вариант 10').close()
    def testFileCreation(self):
        self.assertTrue(os.path.isfile("test.txt"))
    

unittest.main()

