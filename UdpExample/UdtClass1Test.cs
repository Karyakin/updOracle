namespace UdpExample;

public class UdtClass1Test
{
    public void InsertArticleRecordTest()
    {
        UdtClass1 target = new UdtClass1();
        Article a = new Article() { ArticleId = 1, ArticleName = "article one"};
        bool actual;
        actual = target.InsertArticleRecord(a);
    }

    public void GetArticleRecordTest()
    {
        UdtClass1 target = new UdtClass1();
        int intArticleId = 1;
        Article actual;
        actual = target.GetArticleRecord(intArticleId);
    }

    public void InsertArticleRecordsTest()
    {
        UdtClass1 target = new UdtClass1();
        ArticleList al = new ArticleList();
        Article[] articles = new Article[2]
        {
            new Article() { ArticleId = 7, ArticleName = "article two1" },
            new Article() { ArticleId = 8, ArticleName = "article three1" }
        };
        al.objArticles = articles;
        bool actual;
        actual = target.InsertArticleRecords(al);
    }

    public void GetAllArticlesTest()
    {
        UdtClass1 target = new UdtClass1();
        ArticleList actual;
        actual = target.GetAllArticles();
    }
}