from selenium import webdriver
from selenium.webdriver.common.keys import Keys
import unittest

class SmokeTests(unittest.TestCase):

    def setUp(self):
        self.driver = webdriver.Chrome()
        self.driver.get("http://free-generator.ru/names.html")

    def test_search_in_python_org(self):
        driver = self.driver
        self.assertIn("Генератор случайных Имен Фамилий Отчеств онлайн БЕСПЛАТНО - free.generator.ru", driver.title)

    def test_ButtonsEnabled(self):
        driver = self.driver
        upperCheckBoxes = driver.find_elements_by_name('pol')
        for button in upperCheckBoxes:
            self.assertTrue(button.is_enabled())
        
        generalCheckBoxes = driver.find_elements_by_css_selector("input[type='checkbox']")
        for el in generalCheckBoxes:
            self.assertTrue(el.is_enabled())

        button = driver.find_element_by_css_selector("button[class='btn btn-success']")
        assert button.text == 'Сгенерировать'
        self.assertTrue(button.is_enabled())

    def tearDown(self):
        self.driver.close()

if __name__ == "__main__":
    unittest.main()
    