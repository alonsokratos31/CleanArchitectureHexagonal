
IRepository<string> beer = new BeerData();
beer.Add("Cristal");
beer.Add("Pilsen");
var reportGeneratorBeer = new BeerReportGenerate(beer);
var report = new Report();
var data = reportGeneratorBeer.Generate();
report.SaveReport(reportGeneratorBeer, "report.txt");


void Show(IReportShow report)
{
    report.Show();
}

public interface IReportGenerator
{
    public string Generate();

}

public interface IReportShow
{
    public void Show();

}

public interface IRepository<T>
{
    public void Add(T item);

    public List<T> Get();
}


public class BeerData :  IRepository<string>
{
    protected List<string> _beers;

    public BeerData()
    {
        _beers = new List<string>();
    }

    public virtual void Add(string beer)
        => _beers.Add(beer);

    public List<string> Get()
        => _beers;
}

public class LimitedBeerData
{
    private IRepository<string> _beerData;
    private int _limit;
    private int _count = 0;

    public LimitedBeerData(int limit, IRepository<string> beerData)
    {
        _limit = limit;
        _beerData = beerData;
    }

    public void AddBeer(string beer)
    {
        if(_count >= _limit)
        {
            throw new InvalidOperationException("Limite de cerbezas alcanzado");
        }
        _beerData.Add(beer);
        _count++;
    }

    public List<string> Get()
    {
        return _beerData.Get();
    }
}

public class BeerReportGenerate: IReportGenerator, IReportShow
{
    private IRepository<string> _beerData;
    private List<string> _beers;
    public BeerReportGenerate(IRepository<string> beerData)
    {
        _beerData = beerData;
    }
    public string Generate()
    {
        string data = "";
        foreach (var beer in _beers)
        {
            data += " Cerveza: " + beer + Environment.NewLine;
        }
        return data;
    }

    public void Show()
    {
        foreach (var beer in _beers)
        {
            Console.WriteLine(" Cerveza: " + beer);
        }
    }
}


public class BeerReportGenerateHTML : IReportGenerator
{
    private IRepository<string> _beerData;
    private List<string> _beers;
    public BeerReportGenerateHTML(IRepository<string> beerData)
    {
        _beerData = beerData;
    }
    public string Generate()
    {
        string data = "";
        foreach (var beer in _beers)
        {
            data += " Cerveza: " + beer + Environment.NewLine;
        }
        return data;
    }
}


public class Report
{
    public void SaveReport(IReportGenerator reportGenerator, string filePath)
    {
        using(var writer = new StreamWriter(filePath))
        {
            string data = reportGenerator.Generate();
            writer.WriteLine(data);
        }
    }
}