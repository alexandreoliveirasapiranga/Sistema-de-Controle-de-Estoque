using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using camadaDados;
using System.Data.SqlClient;
using System.Data;
namespace camadaNegocio
{
    public class NCategoria
    {
        //método inserir
        public static string Inserir(string nome, string descricao)
        {            
            DCategoria obj = new camadaDados.DCategoria();
            obj.Nome = nome;
            obj.Descriacao = descricao;
            return obj.inserir(obj);
        }

        //método editar
        public static string Editar(int idcategoria, string nome, string descricao)
        {
            DCategoria obj = new camadaDados.DCategoria();
            obj.Nome = nome;
            obj.Descriacao = descricao;
            return obj.inserir(obj);
        }
        
        //método excluir
        public static string Excluir(int idcategoria)
        {
            DCategoria obj = new camadaDados.DCategoria();
            obj.IDcategoria = idcategoria;
            return obj.excluir(obj);
        }

        //metodo mostrar
        public static DataTable Mostrar()
        {
            return new DCategoria().mostrar();

        }

        public static DataTable buscarNome(string textoBuscar)
        {
            DCategoria obj = new DCategoria();
            obj.TextoBuscar = textoBuscar;
            return obj.buscarNome(obj);

        }
                


    }
}














