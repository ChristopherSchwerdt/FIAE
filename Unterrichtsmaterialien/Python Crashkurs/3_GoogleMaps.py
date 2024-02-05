# Dieses Python-Programm öffnet Google-Maps mit der per Kommandozeilenparameter 
# übergebenen Adresse oder alternativ per Zwischenablage (STRG+C)

import webbrowser,sys,pyperclip

#Wenn etwas per Kommandozeilenparameter übergeben worden ist:
if len(sys.argv) > 1:
    # ... alle Parameter aus dem Array in einen String zusammenfassen.
    address = ' '.join(sys.argv[1:])
else:
    #Ansonsten nimm die Adresse aus der Zwischenablage(STRG+C).
    address = pyperclip.paste()
    
webbrowser.open("https://google.de/maps/place/" + address)
