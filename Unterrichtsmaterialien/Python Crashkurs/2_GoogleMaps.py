import webbrowser,sys

if len(sys.argv) > 1:
    address = ' '.join(sys.argv[1:])
webbrowser.open("https://google.de/maps/place/" + address)
