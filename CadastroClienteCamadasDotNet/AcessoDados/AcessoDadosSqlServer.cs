using AcessoDados.Properties;
using System;
using System.Data;
using System.Data.SqlClient;
//Namespace´s que contém as classes que manipulam dados

namespace AcessoDados
{
    public class AcessoDadosSqlServer
    {
        //Criar a conexão
        private SqlConnection CriarConexao()
        {
            return new SqlConnection(Settings.Default.stringConexao);
        }

        //Parâmetros que vão para banco
        private SqlParameterCollection sqlParameterCollection = new SqlCommand().Parameters;
        public void linparParametros()
        {
            sqlParameterCollection.Clear();
        }

        public void adicionarParemetros(string nomeParametro, object valorParametro)
        {
            sqlParameterCollection.Add(new SqlParameter(nomeParametro, valorParametro));
        }

        //Persistência - Isert, Alter, Delet
        public object executarManipulacao(CommandType commandType, string nomeStoredProcedureOuTextoSql)
        {
            try
            {
                //Executar o comando
                return cliarConexaoSqlCommand(commandType, nomeStoredProcedureOuTextoSql).ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Consultar registros do banco
        public DataTable execultarConsulta(CommandType commandType, string nomeStoredProcedureOuTextoSql)
        {
            try
            {
                //Cliar um adaptador
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(
                    cliarConexaoSqlCommand(commandType, nomeStoredProcedureOuTextoSql));
                //DataTable = Tabela de dados vazia
                DataTable dataTable = new DataTable();
                //Mandar o comando ir buscar os dados e o adaptador preencher o datatable
                sqlDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //private criar conexão
        private SqlCommand cliarConexaoSqlCommand(CommandType commandType, string nomeStoredProcedureOuTextoSql)
        {
            try
            {
                //Criar a conexão
                SqlConnection sqlConnection = CriarConexao();
                //Abrir conexão
                sqlConnection.Open();
                //Cliar comando que vai levar a informação ao banco
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //Colocar os comandos (o que vai trafegar na conexão)
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;
                sqlCommand.CommandTimeout = 7200;

                //Adicionar os parâmentros no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                return sqlCommand;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
