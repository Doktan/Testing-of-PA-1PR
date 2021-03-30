import unittest
from main import main
import os

class TestMain(unittest.TestCase):
    def test_noArg(self):
        self.assertEqual(main(None),'INPUT URL!')
    def test_MissingSchema(self):
        self.assertEqual(main('yandex.ru'),'Inccorect URL. Perhaps you meant http://yandex.ru')
    def test_ConnectionError(self):
        self.assertEqual(main('http://5.5'),'URL unavailable!')
    def test_JobsDone(self):
        self.assertEqual(main('http://yandex.ru'),'Jobs done! Check res.txt')
        self.assertTrue(os.path.exists('res.txt'),True)
        f = open('res.txt')
        line = f.readline()
        lineCount = 1
        while line:
            lineCount += 1
            line = f.readline()
        f.close()
        self.assertEqual(lineCount, 16)

if __name__ == "__main__":
  unittest.main()