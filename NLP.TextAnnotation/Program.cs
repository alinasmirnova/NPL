using System;
using NLP.TextAnnotation.Objects;

using System.IO;

using System.Text.RegularExpressions;


namespace NLP.TextAnnotation
{
    class Program
    {
        static void Main(string[] args)
        {
            //загрузка текста
            var text = new Text(File.ReadAllText("textSample.txt"));
            
            //выделяем предложения
            var sentenses = text.GetSentenses();

            var words = text.GetWords();
            foreach (var word in words)
            {
                Console.WriteLine(word);
            }
            Console.ReadLine();
            //составляем тематические узлы
            //выделяем "главные" предложения
            //составляем аннотацию


        }
    }
}
