using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Negocios;
using ObjetoTranferenciaDTO;

namespace Apresentacao
{
    public partial class FrmClienteSelecionar : Form
    {
        public FrmClienteSelecionar()
        {
            InitializeComponent();
            dataGridViewPrincipal.AutoGenerateColumns = false;
        }

        private void pesquisarGrid()
        {
            ClienteNegocios clienteNegocios = new ClienteNegocios();
            ClienteCollection clienteCollection = clienteNegocios.consultarPorNome(textBoxPesquisa.Text);

            dataGridViewPrincipal.DataSource = null;
            dataGridViewPrincipal.DataSource = clienteCollection;
            dataGridViewPrincipal.Update();
            dataGridViewPrincipal.Refresh();
        }

        private void buttonPesquisar_Click(object sender, EventArgs e)
        {
            pesquisarGrid();
        }

        private void buttonFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            if (dataGridViewPrincipal.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum Cliente selecionado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult resultado = MessageBox.Show("Tem certeza que deseja Excluir Cliente?", "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.No)
            {
                return;
            }
            
            //Pegar o cliente selecionado
            Cliente clienteSelect = (dataGridViewPrincipal.SelectedRows[0].DataBoundItem) as Cliente;

            //Intânciar a regra de negócio
            ClienteNegocios clienteNegocios = new ClienteNegocios();
            string retorno = clienteNegocios.excluir(clienteSelect);

            //Verifica excluir com sucesso
            try
            {
                int idcliente = Convert.ToInt32(retorno);
                MessageBox.Show("Cliente excluido com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridViewPrincipal.ClearSelection();
                pesquisarGrid();
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível excluir. Detalhes:" + retorno, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonInserir_Click(object sender, EventArgs e)
        {
            FrmClienteCadastrar frmClienteCadastrar = new FrmClienteCadastrar(AcaoNaTela.Inserir, null);
            
            if (frmClienteCadastrar.ShowDialog() == DialogResult.Yes)
            {
                pesquisarGrid();
            }
        }

        private void buttonAlterar_Click(object sender, EventArgs e)
        {
            if (dataGridViewPrincipal.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum Cliente selecionado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            //Pegar o cliente selecionado
            Cliente clienteSelect = (dataGridViewPrincipal.SelectedRows[0].DataBoundItem) as Cliente;

            //Intânciar a formulário de cadastro
            FrmClienteCadastrar frmClienteCadastrar = new FrmClienteCadastrar(AcaoNaTela.Alterar, clienteSelect);
            if (frmClienteCadastrar.ShowDialog() == DialogResult.Yes)
            {
                pesquisarGrid();
            }
        }

        private void buttonConsultar_Click(object sender, EventArgs e)
        {
            if (dataGridViewPrincipal.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum Cliente selecionado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Pegar o cliente selecionado
            Cliente clienteSelect = (dataGridViewPrincipal.SelectedRows[0].DataBoundItem) as Cliente;

            FrmClienteCadastrar frmClienteCadastrar = new FrmClienteCadastrar(AcaoNaTela.Consultar, clienteSelect);
            frmClienteCadastrar.ShowDialog();
        }
    }
}
