using Microsoft.Data.Sqlite;
using System;
using System.IO;

namespace SistemaDePagamentos_Strategy.Data
{
    public static class DataBase
    {
        private static readonly string caminhoBanco;

        static DataBase()
        {
            var pasta = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "SistemaPagamentos"
            );

            if (!Directory.Exists(pasta))
                Directory.CreateDirectory(pasta);

            caminhoBanco = Path.Combine(pasta, "pagamentos.db");

            if (!File.Exists(caminhoBanco))
                CriarBanco();
        }

        public static SqliteConnection GetConnection()
        {
            return new SqliteConnection($"Data Source={caminhoBanco}");
        }

        private static void CriarBanco()
        {
            using var conn = GetConnection();
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Pagamento (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    tipo TEXT NOT NULL,
                    valor REAL NOT NULL,
                    valor_final REAL NOT NULL,
                    data TEXT NOT NULL
                );
            ";

            cmd.ExecuteNonQuery();
        }
    }
}