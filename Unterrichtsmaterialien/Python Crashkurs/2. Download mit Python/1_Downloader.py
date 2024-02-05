import requests

#Lade die komplette Datei in den Zwischenspeicher:
res = requests.get('http://christopher-schwerdt.de/sherlock.txt')

#Zeige erste 256 Zeichen von res:
print(res.text[:256])
