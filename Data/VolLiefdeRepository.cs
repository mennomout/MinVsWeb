namespace Data;

public class VolLiefdeRepository
{
    // New C# 12 language feature: Collection expressions.
    private Dictionary<string, string> _quotes = new()
    {
        ["Petri"] = "Zal ik het eerlijk zeggen? Ik kom hier aan en ik ben meteen he-le-maal niet blij!",
        ["Walter"] = "Vind je het goed als ik morgen antwoord? Want ik zit nu best wel met m'n verkoudheid.",
        ["Olof"] = "Ja, helemaal prima. Niks mis mee!",
        ["Suus"] = "Sick! Super stoked! Chill! Classic!",
        ["Ingrid"] = "Ik voel f*ck you, ik voel pissig, ik voel boosheid!",
        ["Martijn"] = "Two berehap satsos, two frikandel en one frikandel spesjal.",
        ["Anne"] = "Ik heb last van mijn zonnevlecht.",
        ["Debbie"] = "Daar gaat die snollebak!",
        ["iedereen"] = "Spiritualiteit / spieruwaliteit / spierewaalteit"
    };

    // Other collection expression examples:
    private static readonly string[] WinterVolLiefdeParticipants = ["Benjamin", "Edith", "Guido", "Marco", "Saul"];
    private static readonly List<string> BenBVolLiefde2023Participants = ["Bram", "Debbie", "Joy", "Leendert", "Marian", "Martijn", "Petri", "Walter"];
    private static readonly List<string> BenBVolLiefde2022Participants = ["Natasja", "Denise", "Hans B.", "Ted", "Astrid", "Martijn", "Richard", "Hans V."];
    private static readonly List<string> BenBVolLiefde2021Participants = ["Debbie", "Bert", "Jacob", "Caroline", "Vincent", "Roxanne"];
    private static readonly Dictionary<int, List<string>> AllCandidates = new()
    {
        // One part of the new collection expressions is the spread element.
        // The "..WinterVolLiefdeParticipants" spread element can be combined with indivitual elements like "Olof".
        [2023] = [.. WinterVolLiefdeParticipants, .. BenBVolLiefde2023Participants, "Olof"],
        [2022] = BenBVolLiefde2022Participants,
        [2021] = BenBVolLiefde2021Participants
    };

    public string? Get(string part) => _quotes.FirstOrDefault(x => string.Equals(part, x.Key, StringComparison.OrdinalIgnoreCase)).Value;
    public List<KeyValuePair<string, string>> GetAll() => _quotes.ToList();
    public void Delete(string name) => _quotes.Remove(name);
    public void Add(string name, QuoteModel model) => _quotes.Add(name, model.Quote);
    public bool ContainsQuoteBy(string key) => _quotes.ContainsKey(key);

    // New C# 12 language feature: Pass by readonly reference:
    public int GetOlofAge(ref readonly Olof2013Statistics olof) => olof.Age;
}

public readonly struct Olof2013Statistics
{
    public Olof2013Statistics()
    {
        Age = 75;
        FavoriteColor = "blue";
        IsSomethingWrong = false; // "Daar is niets mis mee"
        LaughCounter = int.MaxValue;
    }

    public int Age { get; init; }
    public string FavoriteColor { get; init; }
    public bool IsSomethingWrong { get; init; }
    public int LaughCounter { get; init; }
}