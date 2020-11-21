using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabalhoRondado
{
    class Adapter : Form
    {
        public Adapter()
        {

        }

        //String de conexão com o banco;
        protected string StringConn { get; } = ConfigurationManager.ConnectionStrings["Databasecrud"].ConnectionString;

        public void Salvar(string nome, string telefone, string celular, string email, string endereco, string numero, string bairro, string cep, string cpf){
            using (var conn = new NpgsqlConnection(StringConn))
            {

                string sql = "insert into clientes (nome, telefone, celular, email, endereco, num_end, bairro_end, cep_end, cnpj_cpf) values(@nome, @telefone, @celular, @email, @endereco, @num_end, @bairro_end, @cep_end, @cnpj_cpf)";

                NpgsqlCommand comando = new NpgsqlCommand(sql, conn);


                comando.Parameters.Add("@nome", NpgsqlTypes.NpgsqlDbType.Text).Value = nome;
                comando.Parameters.Add("@telefone", NpgsqlTypes.NpgsqlDbType.Text).Value = telefone;
                comando.Parameters.Add("@celular", NpgsqlTypes.NpgsqlDbType.Text).Value = celular;
                comando.Parameters.Add("@email", NpgsqlTypes.NpgsqlDbType.Text).Value = email;
                comando.Parameters.Add("@endereco", NpgsqlTypes.NpgsqlDbType.Text).Value = endereco;
                comando.Parameters.Add("@num_end", NpgsqlTypes.NpgsqlDbType.Text).Value = numero;
                comando.Parameters.Add("@bairro_end", NpgsqlTypes.NpgsqlDbType.Text).Value = bairro;
                comando.Parameters.Add("@cep_end", NpgsqlTypes.NpgsqlDbType.Text).Value = cep;
                comando.Parameters.Add("@cnpj_cpf", NpgsqlTypes.NpgsqlDbType.Text).Value = cpf;

                try
                {
                    conn.Open();

                    comando.ExecuteNonQuery();

                    MessageBox.Show("Cadastro Efetuado com Sucesso");
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                finally
                {
                    conn.Close();
                }


            }

        }

    }
}
