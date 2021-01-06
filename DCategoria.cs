using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace camadaDados
{
    public class DCategoria
    {
        private int _IDcategoria;
        private string _Nome;
        private string _Descriacao;
        private string _TextoBuscar;      

        public int IDcategoria { get => _IDcategoria; set => _IDcategoria = value; }
        public string Nome { get => _Nome; set => _Nome = value; }
        public string Descriacao { get => _Descriacao; set => _Descriacao = value; }
        public string TextoBuscar { get => _TextoBuscar; set => _TextoBuscar = value; }


        //Construtor vazio
        public DCategoria() { }
        public DCategoria(int idcategoria, string nome, string descricao, string textoBuscar)
        {
            this.IDcategoria = idcategoria;
            this.Nome = nome;
            this.Descriacao = descricao;
            this.TextoBuscar = textoBuscar;

        }

        //metodo inserir
        public string inserir(DCategoria categoria)
        {
            string resp = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon.ConnectionString = conexao.Cn;
                sqlCon.Open();

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "spinserir_categoria";
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdCategoria = new SqlParameter();
                parIdCategoria.ParameterName = "@idcategoria";
                parIdCategoria.SqlDbType = SqlDbType.Int;
                parIdCategoria.Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add(parIdCategoria);

                SqlParameter ParNome = new SqlParameter();
                ParNome.ParameterName = "@nome";
                ParNome.SqlDbType = SqlDbType.VarChar;
                ParNome.Size = 50;
                ParNome.Value = categoria.Nome;
                sqlCmd.Parameters.Add(ParNome);

                SqlParameter ParDescricao = new SqlParameter();
                ParDescricao.ParameterName = "@descricao ";
                ParDescricao.SqlDbType = SqlDbType.VarChar;
                ParDescricao.Size = 256;
                ParDescricao.Value = categoria.Descriacao;
                sqlCmd.Parameters.Add(ParDescricao);

                resp = sqlCmd.ExecuteNonQuery() == 1 ? "OK" : "REGISTRO NÃO INSERIDO NO BANCO";

            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }

            finally
            {
                if(sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return resp;

        }

        //metodo editar
        public string editar(DCategoria categoria)
        {
            string resp = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon.ConnectionString = conexao.Cn;
                sqlCon.Open();

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "speditar_categoria";
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdCategoria = new SqlParameter();
                parIdCategoria.ParameterName = "@idcategoria";
                parIdCategoria.SqlDbType = SqlDbType.Int;
                parIdCategoria.Value = categoria.IDcategoria;
                sqlCmd.Parameters.Add(parIdCategoria);

                SqlParameter ParNome = new SqlParameter();
                ParNome.ParameterName = "@nome";
                ParNome.SqlDbType = SqlDbType.VarChar;
                ParNome.Size = 50;
                ParNome.Value = categoria.Nome;
                sqlCmd.Parameters.Add(ParNome);

                SqlParameter ParDescricao = new SqlParameter();
                ParDescricao.ParameterName = "@descricao ";
                ParDescricao.SqlDbType = SqlDbType.VarChar;
                ParDescricao.Size = 256;
                ParDescricao.Value = categoria.Descriacao;
                sqlCmd.Parameters.Add(ParDescricao);

                resp = sqlCmd.ExecuteNonQuery() == 1 ? "OK" : "A EDÇÃO NÃO FOI FEITA";

            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }

            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return resp;

        }

        //metodo excluir
        public string excluir(DCategoria categoria)
        {
            string resp = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon.ConnectionString = conexao.Cn;
                sqlCon.Open();

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "spdeletar_categoria";
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parIdCategoria = new SqlParameter();
                parIdCategoria.ParameterName = "@idcategoria";
                parIdCategoria.SqlDbType = SqlDbType.Int;
                parIdCategoria.Value = categoria.IDcategoria;
                sqlCmd.Parameters.Add(parIdCategoria);              
                resp = sqlCmd.ExecuteNonQuery() == 1 ? "OK" : "A EXCLUSÃO NÃO FOI FEITA";
            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }

            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }

            return resp;
        }

        //metodo mostrar no DataTable
        public DataTable mostrar()
        {
            DataTable Dt_Resultado = new DataTable("categoria");
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon.ConnectionString = conexao.Cn;
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "spmostrar_categoria";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDat = new SqlDataAdapter(sqlCmd);
                sqlDat.Fill(Dt_Resultado);

            }
            catch(SqlException ex)
            {
                Dt_Resultado = null;
            }

            return Dt_Resultado;

        }

        //metodo inserir no DataTable
        public DataTable buscarNome(DCategoria categoria)
        {
            DataTable Dt_Resultado = new DataTable("categoria");
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon.ConnectionString = conexao.Cn;
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "spbuscar_nome";
                sqlCmd.CommandType = CommandType.StoredProcedure;
               

                SqlParameter ParTextoBuscar = new SqlParameter();
                ParTextoBuscar.ParameterName = "@textobuscar ";
                ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                ParTextoBuscar.Size = 50;
                ParTextoBuscar.Value = categoria.TextoBuscar;
                sqlCmd.Parameters.Add(ParTextoBuscar);

                SqlDataAdapter sqlDat = new SqlDataAdapter(sqlCmd);
                sqlDat.Fill(Dt_Resultado);

            }

            catch (SqlException ex)
            {
                Dt_Resultado = null;

            }

            return Dt_Resultado;

        }
    }
}
