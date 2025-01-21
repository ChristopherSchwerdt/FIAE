const { PrismaClient } = require('@prisma/client');
const prisma = new PrismaClient();

async function createDemoData() {
    try {
        // Erstelle Demo-Benutzer
        const user1 = await prisma.user.create({
            data: {
                username: 'AshKetchum',
                password: 'pikachu123', // Achtung: Passwörter sollten in der echten Anwendung verschlüsselt gespeichert werden
            },
        });

        const user2 = await prisma.user.create({
            data: {
                username: 'Misty',
                password: 'waterpower',
            },
        });

        // Erstelle Demo-Pokemon
        const pokemon1 = await prisma.pokemon.create({
            data: {
                name: 'Pikachu',
                number: 25,
                type: 'Elektro',
                attacks: 'Donnerblitz, Ruckzuckhieb, Eisenschweif, Agilität',
            },
        });

        const pokemon2 = await prisma.pokemon.create({
            data: {
                name: 'Schiggy',
                number: 7,
                type: 'Wasser',
                attacks: 'Tackle, Rutenschlag, Aquaknarre,Aquawelle',
            },
        });

        const pokemon3 = await prisma.pokemon.create({
            data: {
                name: 'Bisasam',
                number: 1,
                type: 'Pflanze, Gift',
                attacks: 'Rasierblatt, Solarstrahl, Schlafpuder, Egelsamen',
            },
        });

        // Erstelle Kreuztabelleneinträge (User <-> Pokemon)
        await prisma.userPokemon.create({
            data: {
                user_id: user1.user_id,
                pokemon_id: pokemon1.id,  // Ash hat Pikachu
            },
        });

        await prisma.userPokemon.create({
            data: {
                user_id: user2.user_id,
                pokemon_id: pokemon2.id,  // Misty hat Squirtle
            },
        });

        await prisma.userPokemon.create({
            data: {
                user_id: user2.user_id,
                pokemon_id: pokemon3.id,  // Misty hat Bulbasaur
            },
        });

        console.log('Demo-Daten erfolgreich erstellt!');
    } catch (error) {
        console.error('Fehler beim Erstellen der Demo-Daten:', error);
    } finally {
        await prisma.$disconnect();
    }
}

module.exports = { createDemoData };