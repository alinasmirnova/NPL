using System;
using NLP.TextAnnotation.Objects;

using System.IO;

using System.Text.RegularExpressions;
using NLP.Thesaurus.YARN;


namespace NLP.TextAnnotation
{
    class Program
    {
        static void Main(string[] args)
        {
            //загрузка текста
            var text = new Text(File.ReadAllText("textSample.txt"));
            
            //выделяем предложения
            
            //составляем тематические узлы

            var chains = text.GetLexicalChains(new ThesaurusOnline());
            //выделяем "главные" предложения
            //составляем аннотацию


        }
    }
}
