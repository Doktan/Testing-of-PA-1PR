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
        if len(links) == 0:
            f.write('There is no any links in %s' %  url)
        else:
            for link in links:
                error = None
                try:
                    r = requests.get(link.get('href')) #get request to link
                except requests.exceptions.SSLError as e:
                   error = "SSL Error %s\n" % e
                   pass
                except requests.exceptions.MissingSchema as e:
                   error = "Inccorect URL %s\n" % link.get('href')
                   pass
                except (requests.exceptions.ConnectionError, requests.exceptions.ConnectTimeout) as e:
                   error = "URL: %s unavailable!\n" % link.get('href')
                   pass
                except requests.exceptions.InvalidSchema as e:
                    error = e
                    pass
                innerSoup = BeautifulSoup(r.text, 'html.parser')
                h1Tags = innerSoup.find_all('h1')
                f.write('Link: ')
                f.write(str(link.get('href')))
                f.write('\n')
                if error is not None:
                    f.write(error)
                    f.write('\n')
                else:
                    if len(h1Tags) == 0:
                        f.write('There is no any h1 tags\n')
                    else:
                        H1Count = 0
                        for tags in h1Tags:
                            f.write('<h1> ')
                            f.write(str(tags.getText()))
                            f.write('\n')
                            H1Count += 1
                            if H1Count == 3:
                                break
                    f.write('\n')
                i += 1
                if i > 4:
                    break
        f.close()

        return('Jobs done! Check res.txt')

print(main('http://yandex.ru/'))