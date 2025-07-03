namespace BiblioPfe.helpers
{
    public static class Parsers
    {
        public static DateTime TimeStampToDateTime(int TimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(TimeStamp).ToLocalTime();
            return dateTime;
        }
        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search, StringComparison.Ordinal);
            return pos < 0 ? text : string.Concat(text[..pos], replace, text.AsSpan(pos + search.Length));
        }
    }
}
