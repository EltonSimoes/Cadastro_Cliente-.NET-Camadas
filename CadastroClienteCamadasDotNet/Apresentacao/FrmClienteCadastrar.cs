using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ObjetoTranferenciaDTO;
using Negocios;

namespace Apresentacao
{
    public partial class FrmClienteCadastrar : Form
    {
        private Enum acaoNaTelaSelect;

        public FrmClienteCadastrar(AcaoNaTela acaoNaTela, Cliente cliente)
        {
            InitializeComponent();

            this.acaoNaTelaSelect = acaoNaTela;

            if (acaoNaTela.Equals(AcaoNaTela.Inserir))
            {
                this.Text = "Inserir Cliente";
            }
            else if (acaoNaTela.Equals(AcaoNaTela.Alterar))
            {
                this.Text = "Alterar Cliente";
                carregarTela(cliente);
            }
            else if (acaoNaTela.Equals(AcaoNaTela.Consultar))
            {
                this.Text = "Consultar Cliente";
                carregarTela(cliente);
                desbilitarCampos();
            }
        }

        private void desbilitarCampos()
        {
            //Desabilitar campos da tela
            textBoxNome.ReadOnly = true;
            textBoxNome.TabStop = false;

            dateTPDataNascimento.Enabled = false;

            radioSexoMasculino.Enabled = false;
            radioSexoFeminino.Enabled = false;

            textBoxLimiteCompra.ReadOnly = true;
            textBoxLimiteCompra.TabStop = false;

            btnSalvar.Visible = false;
            btnCancelar.Text = "Fechar";
            btnCancelar.Focus();
        }

        private void carregarTela(Cliente cliente)
        {
            //Carregar tela cadastro
            textBoxCodigo.Text = cliente.IdCliente.ToString();
            textBoxNome.Text = cliente.Nome.ToString();
            dateTPDataNascimento.Value = cliente.DataNascimento;
            if (cliente.Sexo)
                radioSexoMasculino.Checked = true;
            else
                radioSexoFeminino.Checked = false;
            textBoxLimiteCompra.Text = cliente.LimiteCompra.ToString();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (acaoNaTelaSelect.Equals(AcaoNaTela.Inserir))
            {
                try
                {
                    ClienteNegocios clienteNegocios = new ClienteNegocios();
                    int idCliente = Convert.ToInt32(clienteNegocios.inserir(carregaCliente()));

                    MessageBox.Show("Clinte inserido com sucesso! Código: " + idCliente.ToString(), "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.Yes;
                }
                catch (Exception)
                {
                    MessageBox.Show("Não foi possivel inserir Cliente!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.No;
                }
            }
            else if (acaoNaTelaSelect.Equals(AcaoNaTela.Alterar))
            {
                try
                {
                    ClienteNegocios clienteNegocios = new ClienteNegocios();
                    int idCliente = Convert.ToInt32(clienteNegocios.alterar(carregaCliente()));

                    MessageBox.Show("Clinte alterado com sucesso! Código: " + idCliente.ToString(), "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.Yes;
                }
                catch (Exception)
                {
                    MessageBox.Show("Não foi possivel alterar Cliente!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.No;
                }
            }
        }

        private Cliente carregaCliente()
        {
            Cliente cliente = new Cliente();
            if (textBoxCodigo.Text != "")
            {
                cliente.IdCliente = Convert.ToInt32(textBoxCodigo.Text);
            }
            cliente.Nome = textBoxNome.Text;
            cliente.DataNascimento = dateTPDataNascimento.Value;
            if (radioSexoMasculino.Checked == true)
                cliente.Sexo = true;
            else
                cliente.Sexo = false;
            cliente.LimiteCompra = Convert.ToDecimal(textBoxLimiteCompra.Text);

            return cliente;
        }

    }
}
