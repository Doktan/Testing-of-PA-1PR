import requests
from bs4 import BeautifulSoup
import sys
import os

def main(arg):
    try:
        if os.path.exists('res.txt'):
            os.remove('res.txt')
        url = arg
    except IndexError:
        url = None

    if url is None or url == '' :
        return('INPUT URL!')
    else:
        try:
            r = requests.get(url)
        except requests.exceptions.SSLError:
            return("Inccoret URL")
        except requests.exceptions.MissingSchema:
            return("Inccorect URL. Perhaps you meant http://%s" % url )
        except (requests.exceptions.ConnectionError, requests.exceptions.ConnectTimeout):
            return("URL unavailable!")
        except requests.exceptions.InvalidSchema as e:
            return(e)
            
        soup = BeautifulSoup(r.text, 'html.parser') 

        f = open('res.txt', 'w', encoding='utf-8')
        i = 0
        links = soup.find_all('a')
        while i != 5:
            r = requests.get(links[i].get('href')) #get request to link
            innerSoup = BeautifulSoup(r.text, 'html.parser')
            h1Tags = innerSoup.find_all('h1')
            f.write('Link: ')
            f.write(links[i].get('href'))
            f.write('\n')

            if len(h1Tags) == 0:
                f.write('There is no any h1 tags\n')
            else:
                H1Count = 0
                for tags in h1Tags:
                    f.write('<h1> ')
                    f.write(str(tags.getText()))
                    f.write('\n')
                    H1Count += 1
                    if H1Count != 3:
                        break

            i+=1
            f.write('\n')

        f.close()

        return('Jobs done! Check res.txt')
