using AcessoDados;
using ObjetoTranferenciaDTO;
using System;
using System.Data;

namespace Negocios
{
    public class ClienteNegocios
    {
        AcessoDadosSqlServer acessoDadosSqlServer = new AcessoDadosSqlServer();

        public string inserir(Cliente cliente)
        {
            try
            {
                acessoDadosSqlServer.linparParametros();
                acessoDadosSqlServer.adicionarParemetros("@Nome", cliente.Nome);
                acessoDadosSqlServer.adicionarParemetros("@DataNascimento", cliente.DataNascimento);
                acessoDadosSqlServer.adicionarParemetros("@Sexo", cliente.Sexo);
                acessoDadosSqlServer.adicionarParemetros("@LimiteCompra", cliente.LimiteCompra);
                return acessoDadosSqlServer.executarManipulacao(CommandType.StoredProcedure, "uspClienteInserir").ToString();
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public string alterar(Cliente cliente)
        {
            try
            {
                acessoDadosSqlServer.linparParametros();
                acessoDadosSqlServer.adicionarParemetros("@IdCliente", cliente.IdCliente);
                acessoDadosSqlServer.adicionarParemetros("@Nome", cliente.Nome);
                acessoDadosSqlServer.adicionarParemetros("@DataNascimento", cliente.DataNascimento);
                acessoDadosSqlServer.adicionarParemetros("@Sexo", cliente.Sexo);
                acessoDadosSqlServer.adicionarParemetros("@LimiteCompra", cliente.LimiteCompra);
                return acessoDadosSqlServer.executarManipulacao(CommandType.StoredProcedure, "uspClienteAlterar").ToString();
            }
            catch (Exception exeption)
            {
                return exeption.Message;
            }
        }

        public string excluir(Cliente cliente)
        {
            try
            {
                acessoDadosSqlServer.linparParametros();
                acessoDadosSqlServer.adicionarParemetros("@IdCliente", cliente.IdCliente);
                //acessoDadosSqlServer.executarManipulacao(CommandType.Text, "Delete From tblCliente Where IdCleinte = @idCliente").ToString();
                return acessoDadosSqlServer.executarManipulacao(CommandType.StoredProcedure, "uspClienteExcluir").ToString();
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public ClienteCollection consultarPorNome(string nome)
        {
            try
            {
                ClienteCollection clienteCollection = new ClienteCollection();
                acessoDadosSqlServer.linparParametros();
                acessoDadosSqlServer.adicionarParemetros("@Nome", nome);

                DataTable dataTableCliente = acessoDadosSqlServer.execultarConsulta(CommandType.StoredProcedure, "uspClienteConsultarPorNome");

                foreach (DataRow row in dataTableCliente.Rows)
                {
                    Cliente cliente = new Cliente();
                    cliente.IdCliente = Convert.ToInt32(row["IdCliente"]);
                    cliente.Nome = Convert.ToString(row["Nome"]);
                    cliente.DataNascimento = Convert.ToDateTime(row["DataNascimento"]);
                    cliente.Sexo = Convert.ToBoolean(row["Sexo"]);
                    cliente.LimiteCompra = Convert.ToDecimal(row["LimiteCompra"]);

                    clienteCollection.Add(cliente);
                }
                return clienteCollection;
            }
            catch (Exception exception)
            {
                throw new Exception("Não foi possivel encontrar cliente por nome! Detalhes: " + exception.Message);
            }
        }

        public ClienteCollection consultarPorId(int id)
        {
            try
            {
                ClienteCollection clienteCollection = new ClienteCollection();
                acessoDadosSqlServer.linparParametros();
                acessoDadosSqlServer.adicionarParemetros("@IdCliente", idCliente);

                DataTable dataTableCliente = acessoDadosSqlServer.execultarConsulta(CommandType.StoredProcedure, "sqlClienteConsultarPorId");

                foreach (DataRow row in dataTableCliente.Rows)
                {
                    Cliente cliente = new Cliente();
                    cliente.IdCliente = Convert.ToInt32(row["IdCliente"]);
                    cliente.Nome = Convert.ToString(row["Nome"]);
                    cliente.DataNascimento = Convert.ToDateTime(row["DataNascimento"]);
                    cliente.Sexo = Convert.ToBoolean(row["Sexo"]);
                    cliente.LimiteCompra = Convert.ToDecimal(row["LimiteCompra"]);

                    clienteCollection.Add(cliente);
                }
                return clienteCollection;
            }
            catch (Exception exception)
            {
                throw new Exception("Não foi possivel encontrar cliente por Id! Detalhes: " + exception.Message);
            }
        }

        public object idCliente { get; set; }
    }
}
