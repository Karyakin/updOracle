using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace UdpExample;

// A custom class mapping to an Oracle collection type.
public class ArticleList : IOracleCustomType, INullable
{
    // The OracleArrayMapping attribute is required to map .NET class member to Oracle collection type.
    [OracleArrayMapping()]
    public Article[] objArticles;

    // A private member indicating whether this object is null.
    private bool ObjectIsNull;

    // Implementation of interface IOracleCustomType method FromCustomObject.
    // Set Oracle collection value from .NET custom type member with OracleArrayMapping attribute.
    public void FromCustomObject(OracleConnection con, object pUdt)
    {
        OracleUdt.SetValue(con, pUdt, 0, objArticles);
    }

    // Implementation of interface IOracleCustomType method ToCustomObject.
    // Set .NET custom type member with OracleArrayMapping attribute from Oracle collection value.
    public void ToCustomObject(OracleConnection con, object pUdt)
    {
        objArticles = (Article[])OracleUdt.GetValue(con, pUdt, 0);
    }

    // A property of interface INullable. Indicate whether the custom type object is null.
    public bool IsNull
    {
        get { return ObjectIsNull; }
    }

    // Static null property is required to return a null UDT.
    public static ArticleList Null
    {
        get
        {
            ArticleList obj = new ArticleList();
            obj.ObjectIsNull = true;
            return obj;
        }
    }
}

// A custom type factory class is required to crate an instance of a custom type representing an Oracle collection type.
// The custom type factory class must implement IOralceCustomTypeFactory and IOracleArrayTypeFactory class.
// The OracleCustomTypeMapping attribute is required to indicate the Oracle UDT for this factory class.
[OracleCustomTypeMapping("TORGI_TIMBER_TOBE.ARTICLES_LIST_TYP")]
public class ArticleListFactory : IOracleCustomTypeFactory, IOracleArrayTypeFactory
{
    // Implementation of interface IOracleCustomTypeFactory method CreateObject.
    // Return a new .NET custom type object representing an Oracle UDT collection object.
    public IOracleCustomType CreateObject()
    {
        return new ArticleList();
    }

    // Implementation of interface IOracleArrayTypeFactory method CreateArray to return a new array.
    public Array CreateArray(int numElems)
    {
        return new Article[numElems];
    }

    // Implementation of interface IOracleArrayTypeFactory method CreateStatusArray to return a new OracleUdtStatus array.
    public Array CreateStatusArray(int numElems)
    {
        return null;
    }
}