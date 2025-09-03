using UnityEngine;
using SQLite;
using System.IO;

public class DatabaseManager : MonoBehaviour
{
    public static SQLiteConnection db;

    void Awake()
    {
        var path = Path.Combine(Application.persistentDataPath, "game.db");
        db = new SQLiteConnection(path);
        db.CreateTable<User>();
        db.CreateTable<Score>();

        // ensure one admin exists
        if (db.Table<User>().Where(u => u.IsAdmin).Count() == 0)
        {
            db.Insert(new User
            {
                Email = "ron81@gmail.com",
                Password = "888888",
                IsAdmin = true
            });
        }
    }
}
