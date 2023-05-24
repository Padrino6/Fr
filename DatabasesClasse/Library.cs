using System;
using System.IO;
using System.Data.SQLite;
using APIBibliotheque.Models;

namespace APIBibliotheque.DatabasesClasse
{
    public class Library
    {
        // Chemin du fichier de base de données
        string dbPath = "Library.db";

        public void InitializeDatabase()
        {
            // Vérifier si la base de données existe
            if (!File.Exists(dbPath))
            {
                // Créer une nouvelle base de données si elle n'existe pas
                SQLiteConnection.CreateFile(dbPath);
            }

            // Établir une connexion à la base de données
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();

                // Créer la table des livres si elle n'existe pas
                using (SQLiteCommand command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS books (idL INTEGER PRIMARY KEY AUTOINCREMENT, title TEXT, author TEXT, publication_year INTEGER, number_pages INTEGER, stock INTEGER)", connection))
                {
                    command.ExecuteNonQuery();
                }

                // Créer la table des emprunteurs si elle n'existe pas
                using (SQLiteCommand command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Borrower (BorrowerId INTEGER PRIMARY KEY AUTOINCREMENT, first_name TEXT, last_name TEXT, address_number TEXT, address_street TEXT, address_zip_code TEXT, address_country TEXT, address_land TEXT, phone_number TEXT, email TEXT)", connection))
                {
                    command.ExecuteNonQuery();
                }

                // Créer la table des prêts si elle n'existe pas
                using (SQLiteCommand command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Loan (LoanId INTEGER PRIMARY KEY AUTOINCREMENT, borrowed_date TEXT, return_date TEXT, book_id INTEGER, borrower_id INTEGER, FOREIGN KEY(idL) REFERENCES books(id), FOREIGN KEY(borrower_id) REFERENCES borrower(borrower_id))", connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
