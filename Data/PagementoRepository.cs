using SistemaDePagamentos_Strategy.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace SistemaDePagamentos_Strategy.Data
{
    public class PagamentoRepository
    {
        public void Inserir(Pagamento pagamento)
        {
            using var conn = DataBase.GetConnection();
            conn.Open();

            var cmd = new SqliteCommand(@"
                INSERT INTO Pagamento
                (tipo, valor, valor_final, data)
                VALUES (@tipo, @valor, @valorFinal, @data)
            ", conn);

            cmd.Parameters.AddWithValue("@tipo", pagamento.Tipo);
            cmd.Parameters.AddWithValue("@valor", pagamento.Valor);
            cmd.Parameters.AddWithValue("@valorFinal", pagamento.ValorFinal);
            cmd.Parameters.AddWithValue("@data", pagamento.Data);

            cmd.ExecuteNonQuery();
        }
        
        public List<Pagamento> Listar()
        {
            var lista = new List<Pagamento>();

            using var conn = DataBase.GetConnection();
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT
                    tipo,
                    valor,
                    valor_final,
                    data
                FROM Pagamento
            ";

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Pagamento
                {
                    Tipo = reader.GetString(0),
                    Valor = reader.GetDouble(1),
                    ValorFinal = reader.GetDouble(2),
                    Data = reader.GetString(3)
                });
            }

            return lista;
        }
    }
}