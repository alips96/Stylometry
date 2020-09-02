using System;

namespace Stylometry
{
    public class Author
    {
        public int Id { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Topic { get; set; }
        public string Sign { get; set; }
        public string Date { get; set; }
        public string Text { get; set; }

        internal static Author ParseRow(string row)
        {
            string[] column = TrimRow(ref row);

            return new Author()
            {
                Id = Convert.ToInt32(column[0]),
                Gender = column[1],
                Age = Convert.ToInt32(column[2]),
                Topic = column[3],
                Sign = column[4],
                Date = GetBlogDate(column),
                Text = GetAuthorText(column)
            };
        }

        private static string[] TrimRow(ref string row)
        {
            row = row.Replace("\"\"", "");
            row = row.Trim(new char[] { '"' });
            string[] column = row.Split(',');
            return column;
        }

        private static string GetBlogDate(string[] column)
        {
            string date = column[5] + "/" + column[6] + "/" + column[7];
            return date;
        }

        private static string GetAuthorText(string[] column)
        {
            string text = null;

            for (int i = 8; i < column.Length; i++)
            {
                text += column[i];
            }

            text = text.Trim(); //remove emty spaces
            return text;
        }
    }
}
