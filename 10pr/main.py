from selenium import webdriver
from selenium.webdriver.common.keys import Keys
import unittest
import time
import json

def GetUnread(login, password):
    driver = webdriver.Chrome()
    driver.get('http://vk.com')
    input_email = driver.find_element_by_id('index_email')
    input_password = driver.find_element_by_id('index_pass')
    confirm = driver.find_element_by_id('index_login_button')
    if login is None or login.replace(" ", "") == '' or password is None or password.replace(" ", "") == '':
        return('You should enter password and login!')
    else:
        input_email.send_keys(login)
        input_password.send_keys(password)
        time.sleep(1)
        confirm.click()
        time.sleep(2)
        if driver.title == 'Вход | ВКонтакте' or driver.find_elements_by_id("recaptcha0"):
            driver.quit()
            return('Wrong Login or Password')
        else:
            driver.get('https://vk.com/im')
            time.sleep(1.5)
            AllUnreads = driver.find_elements_by_css_selector("div[class='nim-dialog--unread _im_dialog_unread_ct']")
            count = 0
            for unread in AllUnreads:
                try:
                    count += int(unread.text)
                except ValueError:
                    pass
            driver.get('https://vk.com/dev/messages.getConversations?params[offset]=0&params[count]=200&params[filter]=unread&params[extended]=0&params[v]=5.130')
            driver.find_element_by_css_selector("button[id='dev_req_run_btn']").click()
            time.sleep(1)

            resString = driver.find_element_by_css_selector("div[id='dev_result']").text

            vkApiCount = 0
            test = json.loads(resString, strict=False)
            for key in test["response"]["items"]:
               vkApiCount += int(key["conversation"]["unread_count"])
            driver.quit()
            return(count, vkApiCount)
