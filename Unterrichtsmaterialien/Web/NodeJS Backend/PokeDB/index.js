const express = require('express');
const session = require('express-session');
const { createDemoData } = require('./demodata');
const app = express();
const PORT = 3000;


app.use(express.static(__dirname + '/public'));
app.use(session({
        secret: 'gotta catch them all',
        resave: false,
        saveUninitialized: false,
        cookie: { maxAge: 60000 } //session Timeout of 60 Seconds
}));

const { PrismaClient } = require("@prisma/client");
const prisma = new PrismaClient();

//Beispielabfrage:
//app.get("/", async(req, res) => {
        //const users= await prisma.user.findMany();
        //console.log(users);//Gibts alle Daten der Tabelle User aus
//})

var bodyParser = require('body-parser')
app.use(bodyParser.urlencoded({ extended: false }))

//Set EJS as the view engine
app.set('view engine','ejs');

//createDemoData();
//Routes
app.get('/', (req,res) => {
        const sessionData= req.session;
        res.render('login');
});
app.get('/index',async (req,res) => {
        const sessionData= req.session;
        const isLoggedIn =req.session.isLoggedIn;
        const username = req.session.username;
        if(isLoggedIn){
                try {
                        // Hole die Pokemon des aktuell angemeldeten Benutzers (aus der Session)
                        const userId = req.session.user_id;
                        const userWithPokemons = await prisma.user.findUnique({
                                where: {
                                        user_id: userId,
                                },
                                include: {
                                        pokemons: {
                                                include: {
                                                        pokemon: true,  // Die Pokemon-Details
                                                },
                                        },
                                },
                        });

                        // Wenn der Benutzer Pokémon hat, gib sie an das Template weiter
                        const pokemons = userWithPokemons ? userWithPokemons.pokemons.map(up => up.pokemon) : [];

                        // Render die Seite und übergebe die Pokémon
                        res.render('index', { username,pokemons });
                } catch (error) {
                        console.error('Fehler beim Abrufen der Pokémon:', error);
                        res.status(500).send('Fehler beim Abrufen der Pokémon.');
                }
        } else {
                // Falls der Benutzer nicht eingeloggt ist, leite ihn zur Login-Seite weiter
                res.render('login');
        }

});
app.post('/login', async (req, res) => {
       //Zugriff auf Postdaten:
        const{username,password }  = req.body;
        console.log(username + " " + password);
        try {
                var user = await prisma.user.findFirstOrThrow({
                        where: {username: username}
                });
                //Login erfolgreich. Redirect Hauptseite
                req.session.isLoggedIn = true;
                req.session.username = username;
                req.session.user_id = user.user_id ;
                console.log('Login erfolgreich!')
                res.redirect('index');
        } catch(error){
                //Login nicht erfolgreich. Zurück zum Login
                console.log(error);
                res.render('login');

        }

})
app.get('/logout', (req,res) => {
        req.session.destroy((err) => {
                if(err){
                        console.log(err);
                } else {
                        res.render('login');
                }
        });
})

//Start server
app.listen(PORT, () => {
        console.log(`Gotta catch em all on http://localhost:${PORT}`);
})