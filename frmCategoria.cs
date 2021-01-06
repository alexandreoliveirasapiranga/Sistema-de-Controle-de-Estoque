using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using camadaNegocio;

namespace CamadaApresentacao
{
    public partial class frmCategoria : Form
    {
        private bool eNovo = false;
        private bool eEditar = false;

        public frmCategoria()
        {
            InitializeComponent();            
        }
        //Mostrar menssagem de confirmação
        public void menssagemOk(string mensagem)
        {
            MessageBox.Show(mensagem, "Sistema Comércio", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Mensgem de erro
        public void menssagemErro(string mensagem)
        {
            MessageBox.Show(mensagem, "Sistema Comércio", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        //Limpar campos
        private void limpar()
        {
            this.txtNome.Text = string.Empty;
            this.txtIdCategoria.Text = string.Empty;
            this.txtDescrição.Text = string.Empty;            
        }

        //Habilitar os textbox
        private void habilitar(bool valor)
        {
            this.txtNome.ReadOnly = !valor;
            this.txtDescrição.ReadOnly =!valor;
            this.txtIdCategoria.ReadOnly =!valor;
        }

        //Habilitar os butões
        private void botoes()
        {
            if (this.eNovo || this.eEditar)
            {
                this.habilitar(true);
                this.btnNovo.Enabled = (false);
                this.btnSalvar.Enabled = (true);
                this.btnEditar.Enabled = (false);
                this.btnCancelar.Enabled = (true);
            }
            else
            {
                this.habilitar(false);
                this.btnNovo.Enabled = (true);
                this.btnSalvar.Enabled = (false);
                this.btnEditar.Enabled = (true);
                this.btnCancelar.Enabled = (false);
            }
            
        }

        //Ocultar colunas
        private void ocultarColunas()
        {
            this.dataLista.Columns[0].Visible = false;
            //this.dataLista.Columns[1].Visible = false;

        }

        //Mostrar no dataGridView
        private void Mostrar()
        {
            this.dataLista.DataSource = NCategoria.Mostrar();
            this.ocultarColunas();
            lblTotal.Text = "Total de registros" + Convert.ToString(dataLista.Rows.Count);


        }

        //buscar pelo nome
        private void BuscarNome()
        {
            this.dataLista.DataSource = NCategoria.buscarNome(this.txtBuscar.Text);
            this.ocultarColunas();
            lblTotal.Text = "Total de registros" + Convert.ToString(dataLista.Rows.Count);


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataLista.Columns["Deletar"].Index)
            {

                DataGridViewCheckBoxCell chkDeletar = (DataGridViewCheckBoxCell)dataLista.Rows[e.RowIndex].Cells["Deletar"];
                chkDeletar.Value = !Convert.ToBoolean(chkDeletar.Value);

            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmCategoria_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Mostrar();
            this.habilitar(false);
            this.botoes();

        }

        private void btnBusacar_Click(object sender, EventArgs e)
        {
            this.BuscarNome();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.BuscarNome();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            this.eNovo = true;
            this.eEditar = false;
            this.habilitar(true);
            this.botoes();
            this.limpar();
            this.txtNome.Focus();
            this.txtIdCategoria.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                string resp = "";
                if (this.txtNome.Text == string.Empty)
                {
                    menssagemErro("Preencha os campos");
                    errorIcon.SetError(txtNome, "insira um nome");
                }

                else
                {
                    if (this.eNovo)
                    {
                        resp = NCategoria.Inserir(this.txtNome.Text.Trim().ToUpper(), this.txtDescrição.Text.Trim());
                    }
                    else
                    {
                        resp = NCategoria.Editar(Convert.ToInt32(this.txtIdCategoria.Text),
                        this.txtNome.Text.Trim().ToUpper(),this.txtDescrição.Text.Trim());
                    }

                    if (resp.Equals("OK"))
                    {
                        if(this.eNovo)
                        {
                            this.menssagemOk("Registro salvo com sucesso");
                        }
                        else
                        {
                            this.menssagemOk("Registro editado com sucesso");
                        }
                    }

                    else
                    {
                        this.eNovo = false;
                        this.eEditar = false;
                        this.botoes();
                        this.limpar();
                        this.Mostrar();

                    }
                }                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dataLista_DoubleClick(object sender, EventArgs e)
        {
            this.txtIdCategoria.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["idcategoria"].Value);
            this.txtNome.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["nome"].Value);
            this.txtDescrição.Text = Convert.ToString(this.dataLista.CurrentRow.Cells["descricao"].Value);
            this.tabControl1.SelectedIndex = 1;

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.txtIdCategoria.Text.Equals(""))
            {
                this.menssagemErro("Selecione um registro");
            }
            else
            {
                this.eEditar = true;
                this.botoes();
                this.habilitar(true);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.eNovo = false;
            this.eEditar = false;
            this.botoes();
            this.habilitar(false);
            this.limpar();
        }

        private void chkDeletar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDeletar.Checked)
            {
                this.dataLista.Columns[0].Visible = true;

            }
            else
            {

                this.dataLista.Columns[0].Visible = false;

            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcao;
                Opcao = MessageBox.Show("Realmente deseja apagar os registros","Sistema Comércio",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcao == DialogResult.OK)
                {
                    string Codigo;
                    string Resp = "";

                    foreach(DataGridViewRow row in dataLista.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToString(row.Cells[1].Value);
                            Resp = NCategoria.Excluir(Convert.ToInt32(Codigo));
                            if (Resp.Equals("OK"))
                            {
                                this.menssagemOk("Registro com sucesso");

                            }
                            else
                            {

                                this.menssagemErro(Resp);
                            }

                        }

                        this.Mostrar();
                    }

                }

            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
    }
}
