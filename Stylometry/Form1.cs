using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Stylometry
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateAuthorsList();
        }

        private void CreateAuthorsList()
        {
            List<Author> authors = ProcessCorpus("Corpus.csv");
            StartButton.Text = authors[0].Text;
        }

        private List<Author> ProcessCorpus(string path)
        {
            return File.ReadAllLines(path).
                Skip(1).
                Where(row => row.Length > 0).
                Select(Author.ParseRow).ToList();
        }

        private void StartButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}
