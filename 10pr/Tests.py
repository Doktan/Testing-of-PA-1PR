import unittest
from main import GetUnread

# You should input your auth data here
Login = ''
Password = '' 

class VkTest(unittest.TestCase):

    def test_noInput(self):
        self.assertEqual(GetUnread('       ','123'),'You should enter password and login!')
        self.assertEqual(GetUnread('123','        '),'You should enter password and login!')
        self.assertEqual(GetUnread('',''),'You should enter password and login!')

    def test_invalidInput(self):
        self.assertEqual(GetUnread('123@123.123','123'),'Wrong Login or Password')

    def test_allRight(self):
        res = GetUnread(Login,Password)
        print(res)
        self.assertEqual(res,res)

if __name__ == "__main__":
    unittest.main()