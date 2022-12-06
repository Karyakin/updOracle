using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace UdpExample;

public class UdtClass1
{
    private string strConStr = "Data Source=192.168.5.26:1521/HETA19;Persist Security Info=True;User ID=vinty;Password=1;";

    public bool InsertArticleRecord(Article a)
    {
        OracleConnection cn = new OracleConnection(strConStr);
        cn.Open();
        OracleCommand cmd = new OracleCommand();
        cmd.CommandText = "add_article";
        cmd.Connection = cn;
        cmd.CommandType = CommandType.StoredProcedure;
        OracleParameter p = new OracleParameter();
        p.OracleDbType = OracleDbType.Object;
        p.Direction = ParameterDirection.Input;
        p.Value = a;
        p.UdtTypeName = "YOUR_SCHEMA_NAME.ARTICLES_TYP";
        cmd.Parameters.Add(p);
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        cn.Close();
        cn.Dispose();
        return true;
    }

    public Article GetArticleRecord(int intArticleId)
    {
        Article a;
        OracleConnection cn = new OracleConnection(strConStr);
        cn.Open();
        OracleCommand cmd = new OracleCommand();
        cmd.CommandText = "get_article";
        cmd.Connection = cn;
        cmd.CommandType = CommandType.StoredProcedure;
        OracleParameter pRet = new OracleParameter();
        pRet.OracleDbType = OracleDbType.Object;
        pRet.Direction = ParameterDirection.ReturnValue;
        pRet.UdtTypeName = "YOUR_SCHEMA_NAME.ARTICLES_TYP";
        cmd.Parameters.Add(pRet);
        OracleParameter pIn = new OracleParameter();
        pIn.OracleDbType = OracleDbType.Int32;
        pIn.Direction = ParameterDirection.Input;
        pIn.Value = intArticleId;
        cmd.Parameters.Add(pIn);
        cmd.ExecuteNonQuery();
        a = (Article)cmd.Parameters[0].Value;
        cmd.Dispose();
        cn.Close();
        cn.Dispose();
        return a;
    }

    public bool InsertArticleRecords(ArticleList al)
    {
        try
        {
            OracleConnection cn = new OracleConnection(strConStr);
            cn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "TORGI_TIMBER_TOBE.PCKG_API_TORGI_PRE_TIMBER.PRC_POST_ARTICLES";
            cmd.Connection = cn;
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p = new OracleParameter();
            p.OracleDbType = OracleDbType.Array;
            p.Direction = ParameterDirection.Input;
            p.Value = al;
            p.UdtTypeName = "TORGI_TIMBER_TOBE.ARTICLES_LIST_TYP";
            cmd.Parameters.Add(p);
            
            var outResult = new OracleParameter
            {
                ParameterName = "v_RESULT",
                OracleDbType = OracleDbType.Int32,
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outResult);
            var outErrMsg = new OracleParameter
            {
                ParameterName = "v_ERR_MSG",
                OracleDbType = OracleDbType.Varchar2,
                Size = 1000,
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outErrMsg);
            
            
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cn.Close();
            cn.Dispose();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public ArticleList GetAllArticles()
    {
        OracleConnection cn = new OracleConnection(strConStr);
        ArticleList al;
        cn.Open();
        OracleCommand cmd = new OracleCommand();
        cmd.CommandText = "get_all_articles";
        cmd.Connection = cn;
        cmd.CommandType = CommandType.StoredProcedure;
        OracleParameter pRet = new OracleParameter();
        pRet.OracleDbType = OracleDbType.Array;
        pRet.Direction = ParameterDirection.ReturnValue;
        pRet.UdtTypeName = "YOUR_SCHEMA_NAME.ARTICLES_LIST_TYP";
        cmd.Parameters.Add(pRet);
        cmd.ExecuteNonQuery();
        al = (ArticleList)cmd.Parameters[0].Value;
        cmd.Dispose();
        cn.Close();
        cn.Dispose();
        return al;
    }
}