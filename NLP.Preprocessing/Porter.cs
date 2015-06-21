using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NLP.Preprocessing
{
    public class Porter
    {
        private static string rvPattern = "([аеиоуыэюя])(.*)";
        private static string rPattern = "([аеиоуыэюя])([^аеиоуыэюя])(.*)";

        private static string perfGerundPattern1 = "(ив|ивши|ившись|ыв|ывши|ывшись)$";
        private static string perfGerundPattern2 = "([ая](в|вши|вшись))$";
        private static string removePerfGerundPattern2 = "(в|вши|вшись)$";
 
        private static string reflexivePattern = "(с[яь])$";  
        private static string adjectivePattern = "(ее|ие|ые|ое|ими|ыми|ей|ий|ый|ой|ем|им|ым|ом|его|ого|ему|ому|их|ых|ую|юю|ая|яя|ою|ею)$";  

        private static string participlePattern1 = "(ивш|ывш|ующ)";
        private static string participlePattern2 = "([ая])(ем|нн|вш|ющ|щ)";
        private static string removeParticiplePattern2 = "(ем|нн|вш|ющ|щ)";  

        private static string verbPattern1 = "(ила|ыла|ена|ейте|уйте|ите|или|ыли|ей|уй|ил|ыл|им|ым|ен|ило|ыло|ено|ят|ует|уют|ит|ыт|ены|ить|ыть|ишь|ую|ю)$";
        private static string verbPattern2 = "([ая](ла|на|ете|йте|ли|й|л|ем|н|ло|но|ет|ют|ны|ть|ешь|нно))$";
        private static string removeVerbPattern2 = "(ла|на|ете|йте|ли|й|л|ем|н|ло|но|ет|ют|ны|ть|ешь|нно)$";

        private static string adjectivalPattern1 = participlePattern1 + "?" + adjectivePattern;
        private static string adjectivalPattern2 = participlePattern2 + "?" + adjectivePattern;
        private static string removeAdjectivalPattern2 = removeParticiplePattern2 + "?" + adjectivePattern;

        private static string nounPattern = "(а|ев|ов|ие|ье|е|иями|ями|ами|еи|ии|и|ией|ей|ой|ий|й|иям|ям|ием|ем|ам|ом|о|у|ах|иях|ях|ы|ь|ию|ью|ю|ия|ья|я)$";  
        private static string derivationalPattern = "ость?$";  
        private static string superlativePattern = "(ейше|ейш)$";  
        private static string iPattern = "и$";  
        private static string pPattern = "ь$";  
        private static string nnPattern = "нн$";  

        public static Word Stem(string full)
        {
            var word = new Word()
            {
                Full = full,
                PartOfSpeach = PartOfSpeach.Unhnown
            };

            //рекомендуется заменять ё на е перед началом работы, так как она редко употребляется
            var stemmed = Regex.Replace(full, "ё", "e");

            //RV - часть слова после первой гласной или пустой строке, если в нем нет гласных
            var rv = Regex.Match(stemmed, rvPattern).Groups[2].Value;
            //R1 - часть слова после первой согласной, следующей за гласной или пустая строка
            //R2 - часть R1 после первой согласной, следующей за гласной или пустая строка
            
            //далее работаем с RV
            //step 1: ищем окончание perspective gerund. Если нашли - удаляем и переходим к шагу 2. Иначе - пытаемся удалить reflexive ending, а затем - (1) an ADJECTIVAL, (2) a VERB or (3) a NOUN
            
            Regex replaceReg;
            if (Regex.IsMatch(rv, perfGerundPattern1))
            {
                replaceReg = new Regex(perfGerundPattern1);
                stemmed = replaceReg.Replace(stemmed, "");
                rv = replaceReg.Replace(rv, "");
                word.PartOfSpeach = PartOfSpeach.PerfectGerund;
            }
            else if (Regex.IsMatch(rv, perfGerundPattern2))
            {
                replaceReg = new Regex(removePerfGerundPattern2);
                stemmed = replaceReg.Replace(stemmed, "");
                rv = replaceReg.Replace(rv, "");
                word.PartOfSpeach = PartOfSpeach.PerfectGerund;
            }
            else
            {
                if (Regex.IsMatch(rv, reflexivePattern))
                {
                    replaceReg = new Regex(reflexivePattern);
                    stemmed = replaceReg.Replace(stemmed, "");
                    rv = replaceReg.Replace(rv, "");
                }

                replaceReg = null;
                int replaceLength = 0;

                if (Regex.IsMatch(rv, adjectivalPattern1))
                {
                    replaceReg = new Regex(adjectivalPattern1);
                    replaceLength = replaceReg.Match(rv).Length;
                    word.PartOfSpeach = PartOfSpeach.Adjective;
                }
                if (Regex.IsMatch(rv, adjectivalPattern2) && replaceLength <= Regex.Match(rv, adjectivalPattern2).Length)
                {
                    replaceReg = new Regex(removeAdjectivalPattern2);
                    replaceLength = replaceReg.Match(rv).Length;
                    word.PartOfSpeach = PartOfSpeach.Adjective;
                }
                if (Regex.IsMatch(rv, verbPattern1) && replaceLength <= Regex.Match(rv, verbPattern1).Length)
                {
                    replaceReg = new Regex(verbPattern1);
                    replaceLength = replaceReg.Match(rv).Length;
                    word.PartOfSpeach = PartOfSpeach.Verb;
                }
                if (Regex.IsMatch(rv, verbPattern2) && replaceLength <= Regex.Match(rv, verbPattern2).Length)
                {
                    replaceReg = new Regex(removeVerbPattern2);
                    replaceLength = replaceReg.Match(rv).Length;
                    word.PartOfSpeach = PartOfSpeach.Verb;
                }
                if (Regex.IsMatch(rv, nounPattern) && replaceLength <= Regex.Match(rv, nounPattern).Length)
                {
                    replaceReg = new Regex(nounPattern);
                    replaceLength = replaceReg.Match(rv).Length;
                    word.PartOfSpeach = PartOfSpeach.Noun;
                }
                if (replaceReg != null)
                {
                    stemmed = stemmed.Substring(0, stemmed.Length - replaceLength);
                    rv = rv.Substring(0, rv.Length - replaceLength);
                }
            }

            //step 2: удаляем "и" вконце
            if (Regex.IsMatch(rv, iPattern))
            {
                replaceReg = new Regex(iPattern);
                stemmed = replaceReg.Replace(stemmed, "");
                rv = replaceReg.Replace(rv, "");
            }

            //step 3: Ищем derivational в R2 и удаляем
            if (Regex.IsMatch(stemmed, rPattern + rPattern + derivationalPattern))
            {
                replaceReg = new Regex(derivationalPattern);
                stemmed = replaceReg.Replace(stemmed, "");
                rv = replaceReg.Replace(rv, "");
            }

            //step 4: Удаляем вторую н вконце, удаляем SUPERLATIVE ending или удаляем мягкий знак
            if (Regex.IsMatch(rv, pPattern))
            {
                replaceReg = new Regex(pPattern);
                stemmed = replaceReg.Replace(stemmed, "");
                rv = replaceReg.Replace(rv, "");
            }
            if (Regex.IsMatch(rv, superlativePattern))
            {
                replaceReg = new Regex(superlativePattern);
                stemmed = replaceReg.Replace(stemmed, "");
                rv = replaceReg.Replace(rv, "");
            }
            if (Regex.IsMatch(rv, nnPattern))
            {
                replaceReg = new Regex(nnPattern);
                stemmed = replaceReg.Replace(stemmed, "н");
            }
            word.Stemmed = stemmed;
            return word;
        }
    }
}
