using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace UdpExample;

// A custom class mapping to an Oracle user defined type.
// Provided all required implementations by ODP.NET developer guide to represent the Oracle UDT as custom type.
// The custom class must implement IOracleCustomType and INullable interfaces.
// Note: Any Oracle UDT name must be uppercase.
public class Article : IOracleCustomType, INullable
{
    // A private member indicating whether this object is null.
    private bool ObjectIsNull;

    // The OracleObjectMapping attribute is required to map .NET custom type member to Oracle object attribute.
    [OracleObjectMapping("ARTICLE_ID")]
    public int ArticleId { get; set; }
    [OracleObjectMapping("ARTICLE_NAME")]
    public string ArticleName { get; set; }
    /*[OracleObjectMapping("UPDATE_DATE")]
    public DateTime UpdateDate { get; set; }*/

    // Implementation of interface IOracleCustomType method FromCustomObject.
    // Set Oracle object attribute values from .NET custom type object.
    public void FromCustomObject(OracleConnection con, object pUdt)
    {
        OracleUdt.SetValue(con, pUdt, "ARTICLE_ID", ArticleId);
        OracleUdt.SetValue(con, pUdt, "ARTICLE_NAME", ArticleName);
    }

    // Implementation of interface IOracleCustomType method ToCustomObject.
    // Set .NET custom type object members from Oracle object attributes.
    public void ToCustomObject(OracleConnection con, object pUdt)
    {
        ArticleId = (int)OracleUdt.GetValue(con, pUdt, "ARTICLE_ID");
        ArticleName = (string)OracleUdt.GetValue(con, pUdt, "ARTICLE_NAME");
       // UpdateDate = (DateTime)OracleUdt.GetValue(con, pUdt, "UPDATE_DATE");
    }

    // A property of interface INullable. Indicate whether the custom type object is null.
    public bool IsNull
    {
        get { return ObjectIsNull; }
    }

    // Static null property is required to return a null UDT.
    public static Article Null
    {
        get
        {
            Article obj = new Article();
            obj.ObjectIsNull = true;
            return obj;
        }
    }
}

// A custom type factory class is required to create an instance of a custom type representing an Oracle object type.
// The custom type factory class must implement IOralceCustomTypeFactory class.
// The OracleCustomTypeMapping attribute is required to indicate the Oracle UDT for this factory class.
[OracleCustomTypeMapping("TORGI_TIMBER_TOBE.ARTICLES_TYP")]
public class ArticleFactory : IOracleCustomTypeFactory
{
    // Implementation of interface IOracleCustomTypeFactory method CreateObject.
    // Return a new .NET custom type object representing an Oracle UDT object.
    public IOracleCustomType CreateObject()
    {
        return new Article();
    }
}