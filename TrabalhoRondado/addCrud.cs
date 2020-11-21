using LinqToDB;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabalhoRondado
{
    public partial class addCrud : Form
    {
        String nome;
        String telefone;
        String celular;
        String email;
        String endereco;
        String numero;
        String bairro;
        String cep;
        String cpfcnpj;

        Adapter adapter = new Adapter(); //adapter class instance

        //String de conexão com o banco
        protected string StringConn { get; } = ConfigurationManager.ConnectionStrings["Databasecrud"].ConnectionString; // Database connection string PostgreSQL

        public addCrud() // button to release all lines
        {
            InitializeComponent(); //Inicializa janela

            //Deixa caixas de texto desabilitadas
            txt_id.Enabled = false;
            txt_nome.Enabled = false;
            txt_telefone.Enabled = false;
            txt_celular.Enabled = false;
            txt_cnpjcpf.Enabled = false;
            txt_email.Enabled = false;
            txt_endereco.Enabled = false;
            txt_numero.Enabled = false;
            txt_cep.Enabled = false;
            txt_bairro.Enabled = false;
            txt_pesquisanome.Enabled = false;

        }
       

        private void btn_add_Click(object sender, EventArgs e) //add button
        {
            //Habilita Caixas de escrita
            txt_id.Enabled = false;
            txt_nome.Enabled = true;
            txt_telefone.Enabled = true;
            txt_celular.Enabled = true;
            txt_cnpjcpf.Enabled = true;
            txt_email.Enabled = true;
            txt_endereco.Enabled = true;
            txt_numero.Enabled = true;
            txt_cep.Enabled = true;
            txt_bairro.Enabled = true;
            txt_pesquisanome.Enabled = true;

            //Limpa os campos
            txt_id.Clear();
            txt_nome.Clear();
            txt_telefone.Clear();
            txt_celular.Clear();
            txt_email.Clear();
            txt_endereco.Clear();
            txt_numero.Clear();
            txt_bairro.Clear();
            txt_cep.Clear();
            txt_cnpjcpf.Clear();
        }

        private void btn_salvar_Click(object sender, EventArgs e) //save button using adapter class
        {
                nome = txt_nome.Text;
                telefone = txt_telefone.Text;
                celular = txt_celular.Text;
                email = txt_email.Text;
                endereco = txt_endereco.Text;
                numero = txt_numero.Text;
                bairro = txt_bairro.Text;
                cep = txt_cep.Text;
                cpfcnpj = txt_cnpjcpf.Text;

                adapter.Salvar(nome, telefone, celular, email, endereco, numero, bairro, cep, cpfcnpj);

                txt_pesquisanome.Enabled = true;

                txt_id.Clear();
                txt_nome.Clear();
                txt_telefone.Clear();
                txt_celular.Clear();
                txt_email.Clear();
                txt_endereco.Clear();
                txt_numero.Clear();
                txt_bairro.Clear();
                txt_cep.Clear();
                txt_cnpjcpf.Clear();

        }


        private void btn_buscar_Click(object sender, EventArgs e) // seek button
        {
            //metodo de conexão com o banco
            using (var conn = new NpgsqlConnection(StringConn))
            {

                string sql = "SELECT * FROM clientes WHERE nome=@pesquisanome";

                NpgsqlCommand comando = new NpgsqlCommand(sql, conn);

                comando.Parameters.Add("@pesquisanome", NpgsqlTypes.NpgsqlDbType.Text).Value = txt_pesquisanome.Text;

                try
                {
                    if(txt_pesquisanome.Text == string.Empty)
                    {
                        MessageBox.Show("Você não digitou um nome.");
                    }

                    conn.Open();

                    NpgsqlDataReader dr = comando.ExecuteReader();

                    if(dr.HasRows == false)
                    {
                        throw new Exception("Este nome não está cadastrado");
                    }

                    dr.Read();

                    txt_id.Text = Convert.ToString(dr["id"]);
                    txt_nome.Text = Convert.ToString(dr["nome"]);
                    txt_telefone.Text = Convert.ToString(dr["telefone"]);
                    txt_celular.Text = Convert.ToString(dr["celular"]);
                    txt_email.Text = Convert.ToString(dr["email"]);
                    txt_endereco.Text = Convert.ToString(dr["endereco"]);
                    txt_numero.Text = Convert.ToString(dr["num_end"]);
                    txt_bairro.Text = Convert.ToString(dr["bairro_end"]);
                    txt_cep.Text = Convert.ToString(dr["cep_end"]);
                    txt_cnpjcpf.Text = Convert.ToString(dr["cnpj_cpf"]);

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                finally
                {
                    conn.Close();
                }

                txt_id.Enabled = false;
                txt_pesquisanome.Clear();

            }
        }

        private void btn_editar_Click(object sender, EventArgs e) //seek button
        {
            using (var conn = new NpgsqlConnection(StringConn))
            {

                string sql = "UPDATE clientes SET nome=@nome, telefone=@telefone, celular=@celular, email=@email, endereco=@endereco, num_end=@num_end, bairro_end=@bairro_end, cep_end=@cep_end, cnpj_cpf=@cnpj_cpf WHERE id=@id;";

                NpgsqlCommand comando = new NpgsqlCommand(sql, conn);

                comando.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = txt_id.Text;
                comando.Parameters.Add("@nome", NpgsqlTypes.NpgsqlDbType.Text).Value = txt_nome.Text;
                comando.Parameters.Add("@telefone", NpgsqlTypes.NpgsqlDbType.Text).Value = txt_telefone.Text;
                comando.Parameters.Add("@celular", NpgsqlTypes.NpgsqlDbType.Text).Value = txt_celular.Text;
                comando.Parameters.Add("@email", NpgsqlTypes.NpgsqlDbType.Text).Value = txt_email.Text;
                comando.Parameters.Add("@endereco", NpgsqlTypes.NpgsqlDbType.Text).Value = txt_endereco.Text;
                comando.Parameters.Add("@num_end", NpgsqlTypes.NpgsqlDbType.Text).Value = txt_numero.Text;
                comando.Parameters.Add("@bairro_end", NpgsqlTypes.NpgsqlDbType.Text).Value = txt_bairro.Text;
                comando.Parameters.Add("@cep_end", NpgsqlTypes.NpgsqlDbType.Text).Value = txt_cep.Text;
                comando.Parameters.Add("@cnpj_cpf", NpgsqlTypes.NpgsqlDbType.Text).Value = txt_cnpjcpf.Text;

                try
                {
                    conn.Open();

                    comando.Parameters[0].Value = Convert.ToInt32(txt_id.Text); // converting string to integer

                    comando.ExecuteNonQuery();

                    MessageBox.Show("Cadastro Alterado com Sucesso");
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                finally
                {
                    conn.Close();
                }

                txt_id.Clear();
                txt_nome.Clear();
                txt_telefone.Clear();
                txt_celular.Clear();
                txt_email.Clear();
                txt_endereco.Clear();
                txt_numero.Clear();
                txt_bairro.Clear();
                txt_cep.Clear();
                txt_cnpjcpf.Clear();
            }
        }

        private void btn_excluir_Click(object sender, EventArgs e) // delete button
        {
            using (var conn = new NpgsqlConnection(StringConn))
            {

                string sql = "DELETE FROM clientes WHERE nome = @nome;";

                NpgsqlCommand comando = new NpgsqlCommand(sql, conn);

                comando.Parameters.Add("@nome", NpgsqlTypes.NpgsqlDbType.Text).Value = txt_nome.Text;

                try
                {
                    conn.Open();

                    comando.ExecuteNonQuery();

                    MessageBox.Show("Exclusão de Cadastro feita com Sucesso");
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                finally
                {
                    conn.Close();
                }

                txt_id.Clear();
                txt_nome.Clear();
                txt_telefone.Clear();
                txt_celular.Clear();
                txt_email.Clear();
                txt_endereco.Clear();
                txt_numero.Clear();
                txt_bairro.Clear();
                txt_cep.Clear();
                txt_cnpjcpf.Clear();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void addCrud_Load(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
