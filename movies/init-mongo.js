db = db.getSiblingDB('moviesdb');
db.createUser(
    {
        user: 'sonal',
        pwd: 'mypassword1!',
        roles: [{ role: 'readWrite', db: 'moviesdb' }]
    }
);
db.createCollection('Movies');  // Create Movies collection

// Add documents to the Movies collection
db.Movies.insertMany([
    {
        "Title": "The Lord of the Rings",
        "Year": 2001,
        "Summary": "A meek Hobbit and eight companions set out on a journey to destroy the One Ring and the Dark Lord Sauron.",
        "Actors": ["Elijah Wood", "Ian McKellen", "Orlando Bloom"]
    },
    {
        "Title": "Harry Potter and the Philosopher's Stone",
        "Year": 2001,
        "Summary": "An orphaned boy enrolls in a school of wizardry, where he learns the truth about himself, his family and the terrible evil that haunts the magical world.",
        "Actors": ["Daniel Radcliffe", "Rupert Grint", "Emma Watson"]
    },
    {
        "Title": "Inception",
        "Year": 2010,
        "Summary": "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.",
        "Actors": ["Leonardo DiCaprio", "Joseph Gordon-Levitt", "Ellen Page"]
    },
    {
        "Title": "The Dark Knight",
        "Year": 2008,
        "Summary": "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
        "Actors": ["Christian Bale", "Heath Ledger", "Aaron Eckhart"]
    },
    {
        "Title": "The Matrix",
        "Year": 1999,
        "Summary": "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.",
        "Actors": ["Keanu Reeves", "Laurence Fishburne", "Carrie-Anne Moss"]
    }
]);