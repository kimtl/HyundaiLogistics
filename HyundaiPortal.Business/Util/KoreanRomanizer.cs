using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HyundaiPortal.Business.Util
{
    public class KoreanRomanizer
    {

        private bool capitalizeOnFirstLetter = true;

        private bool useHyphenWhenVowelConfused = true;

        private static String[] chosungs = { "g", "kk", "n", "d", "tt", "r", "m", "b", "pp", "s", "ss", "", "j", "jj", "ch", "k", "t", "p", "h" };

        private static String[] jungsungs = { "a", "ae", "ya", "yae", "eo", "e", "yeo", "ye", "o", "wa", "wae", "oe", "yo", "u", "wo", "we", "wi", "yu", "eu", "ui", "i" };

        public const int CHOSUNG_ㄱ = 0;

        public const int CHOSUNG_ㄲ = 1;

        public const int CHOSUNG_ㄴ = 2;

        public const int CHOSUNG_ㄷ = 3;

        public const int CHOSUNG_ㄸ = 4;

        public const int CHOSUNG_ㄹ = 5;

        public const int CHOSUNG_ㅁ = 6;

        public const int CHOSUNG_ㅂ = 7;

        public const int CHOSUNG_ㅃ = 8;

        public const int CHOSUNG_ㅅ = 9;

        public const int CHOSUNG_ㅆ = 10;

        public const int CHOSUNG_ㅇ = 11;

        public const int CHOSUNG_ㅈ = 12;

        public const int CHOSUNG_ㅉ = 13;

        public const int CHOSUNG_ㅊ = 14;

        public const int CHOSUNG_ㅋ = 15;

        public const int CHOSUNG_ㅌ = 16;

        public const int CHOSUNG_ㅍ = 17;

        public const int CHOSUNG_ㅎ = 18;

        public const int JUNGSUNG_ㅏ = 0;

        public const int JUNGSUNG_ㅐ = 1;

        public const int JUNGSUNG_ㅑ = 2;

        public const int JUNGSUNG_ㅒ = 3;

        public const int JUNGSUNG_ㅓ = 4;

        public const int JUNGSUNG_ㅔ = 5;

        public const int JUNGSUNG_ㅕ = 6;

        public const int JUNGSUNG_ㅖ = 7;

        public const int JUNGSUNG_ㅗ = 8;

        public const int JUNGSUNG_ㅘ = 9;

        public const int JUNGSUNG_ㅙ = 10;

        public const int JUNGSUNG_ㅚ = 11;

        public const int JUNGSUNG_ㅛ = 12;

        public const int JUNGSUNG_ㅜ = 13;

        public const int JUNGSUNG_ㅝ = 14;

        public const int JUNGSUNG_ㅞ = 15;

        public const int JUNGSUNG_ㅟ = 16;

        public const int JUNGSUNG_ㅠ = 17;

        public const int JUNGSUNG_ㅡ = 18;

        public const int JUNGSUNG_ㅢ = 19;

        public const int JUNGSUNG_ㅣ = 20;

        public const int JONGSUNG_NONE = 0;

        public const int JONGSUNG_ㄱ = 1;

        public const int JONGSUNG_ㄲ = 2;

        public const int JONGSUNG_ㄳ = 3;

        public const int JONGSUNG_ㄴ = 4;

        public const int JONGSUNG_ㄵ = 5;

        public const int JONGSUNG_ㄶ = 6;

        public const int JONGSUNG_ㄷ = 7;

        public const int JONGSUNG_ㄹ = 8;

        public const int JONGSUNG_ㄺ = 9;

        public const int JONGSUNG_ㄻ = 10;

        public const int JONGSUNG_ㄼ = 11;

        public const int JONGSUNG_ㄽ = 12;

        public const int JONGSUNG_ㄾ = 13;

        public const int JONGSUNG_ㄿ = 14;

        public const int JONGSUNG_ㅀ = 15;

        public const int JONGSUNG_ㅁ = 16;

        public const int JONGSUNG_ㅂ = 17;

        public const int JONGSUNG_ㅄ = 18;

        public const int JONGSUNG_ㅅ = 19;

        public const int JONGSUNG_ㅆ = 20;

        public const int JONGSUNG_ㅇ = 21;

        public const int JONGSUNG_ㅈ = 22;

        public const int JONGSUNG_ㅊ = 23;

        public const int JONGSUNG_ㅋ = 24;

        public const int JONGSUNG_ㅌ = 25;

        public const int JONGSUNG_ㅍ = 26;

        public const int JONGSUNG_ㅎ = 27;

        public void setCapitalizeOnFirstLetter(bool capitalizeOnFirstLetter)
        {

            this.capitalizeOnFirstLetter = capitalizeOnFirstLetter;

        }

        public void setUseHyphenWhenVowelConfused(bool useHyphenWhenVowelConfused)
        {

            this.useHyphenWhenVowelConfused = useHyphenWhenVowelConfused;

        }

        public string romanize(string str)
        {

            if (str == null)
            {

                throw new Exception();

            }

            int length = str.Length;

            bool isFirstLetter = true;

            bool skipNextChosung = false;

            StringBuilder buffer = new StringBuilder(length * 3);

            for (int i = 0; i < length; i++)
            {

                char character = str[i];

                if (character < 0xAC00 || character > 0xD7A3)
                {

                    buffer.Append(character);

                    isFirstLetter = true;

                    continue;

                }

                character -= (char)0xAC00;

                if (!skipNextChosung)
                {

                    String chosung = chosungs[character / (21 * 28)];

                    if (capitalizeOnFirstLetter && isFirstLetter && chosung.Length > 0)
                    {

                        chosung = chosung[0].ToString().ToUpper() + chosung.Substring(1);

                        isFirstLetter = false;

                    }

                    buffer.Append(chosung);

                }

                skipNextChosung = false;

                string jungsung = jungsungs[character % (21 * 28) / 28];

                if (capitalizeOnFirstLetter && isFirstLetter && jungsung.Length > 0)
                {

                    jungsung = jungsung[0].ToString().ToUpper() + jungsung.Substring(1);

                    isFirstLetter = false;

                }

                buffer.Append(jungsung);

                int nextChosung = -1;

                int nextJungsung = -1;

                if (i < length - 1)
                {

                    char nextCharacter = str[i + 1];

                    if (nextCharacter >= 0xAC00 && nextCharacter <= 0xD7A3)
                    {

                        nextChosung = (nextCharacter - 0xAC00) / (21 * 28);

                        nextJungsung = (nextCharacter - 0xAC00) % (21 * 28) / 28;

                    }

                }

                int jongsung = character % 28;

                if (useHyphenWhenVowelConfused && jongsung == JONGSUNG_NONE && nextChosung == CHOSUNG_ㅇ)
                {

                    char nextJungsungChar = jungsungs[nextJungsung][0];

                    bool useHyphen = false;

                    switch (jungsung[jungsung.Length - 1])
                    {

                        case 'a':

                            switch (nextJungsungChar)
                            {

                                case 'a':

                                case 'e':

                                    useHyphen = true;

                                    break;

                            }

                            break;

                        case 'e':

                            switch (nextJungsungChar)
                            {

                                case 'a':

                                case 'e':

                                case 'o':

                                case 'u':

                                    useHyphen = true;

                                    break;

                            }

                            break;

                    }

                    if (useHyphen)
                    {

                        buffer.Append("-");

                    }

                }

                skipNextChosung = false;

                switch (jongsung)
                {

                    case JONGSUNG_ㄱ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄲ:

                            case CHOSUNG_ㅋ:

                                break;

                            case CHOSUNG_ㄴ:

                            case CHOSUNG_ㅁ:

                                buffer.Append("ng");

                                break;

                            case CHOSUNG_ㄹ:

                                buffer.Append("ngn");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅇ:

                                buffer.Append("g");

                                break;

                            default:

                                buffer.Append("k");

                                break;

                        }

                        break;

                    case JONGSUNG_ㄲ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄲ:

                            case CHOSUNG_ㅋ:

                                break;

                            case CHOSUNG_ㄱ:

                                buffer.Append("kg");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㄴ:

                            case CHOSUNG_ㅁ:

                                buffer.Append("ng");

                                break;

                            case CHOSUNG_ㄹ:

                                buffer.Append("ngn");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅇ:

                                buffer.Append("kk");

                                break;

                            case CHOSUNG_ㅎ:

                                buffer.Append("k");

                                skipNextChosung = true;

                                break;

                            default:

                                buffer.Append("k");

                                break;

                        }

                        break;

                    case JONGSUNG_ㄳ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄲ:

                            case CHOSUNG_ㅋ:

                                break;

                            case CHOSUNG_ㄴ:

                            case CHOSUNG_ㅁ:

                                buffer.Append("ng");

                                break;

                            case CHOSUNG_ㄹ:

                                buffer.Append("ngn");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅇ:

                                buffer.Append("ks");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅎ:

                                buffer.Append("k");

                                skipNextChosung = true;

                                break;

                            default:

                                buffer.Append("k");

                                break;

                        }

                        break;

                    case JONGSUNG_ㄴ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄱ:

                                buffer.Append("n");

                                if (useHyphenWhenVowelConfused)
                                {

                                    buffer.Append("-");

                                }

                                buffer.Append("g");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㄹ:

                                buffer.Append("ll");

                                skipNextChosung = true;

                                break;

                            default:

                                buffer.Append("n");

                                break;

                        }

                        break;

                    case JONGSUNG_ㄵ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄱ:

                                buffer.Append("n");

                                if (useHyphenWhenVowelConfused)
                                {

                                    buffer.Append("-");

                                }

                                buffer.Append("g");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㄷ:

                                buffer.Append("ntt");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㄹ:

                                buffer.Append("ll");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅇ:

                            case CHOSUNG_ㅎ:

                                skipNextChosung = true;
                                buffer.Append("nj");
                                break;

                            case CHOSUNG_ㅈ:

                                buffer.Append("nj");

                                break;

                            default:

                                buffer.Append("n");

                                break;

                        }

                        break;

                    case JONGSUNG_ㄶ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄱ:

                                buffer.Append("n");

                                if (useHyphenWhenVowelConfused)
                                {

                                    buffer.Append("-");

                                }

                                buffer.Append("g");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㄹ:

                                buffer.Append("ll");

                                skipNextChosung = true;

                                break;

                            default:

                                buffer.Append("n");
                                break;
                        }

                        break;

                    case JONGSUNG_ㄷ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄸ:

                                break;

                            case CHOSUNG_ㄴ:

                            case CHOSUNG_ㅁ:

                                buffer.Append("n");

                                break;

                            case CHOSUNG_ㄹ:

                                buffer.Append("nn");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅇ:

                                switch (nextJungsung)
                                {

                                    case JUNGSUNG_ㅑ:

                                    case JUNGSUNG_ㅒ:

                                    case JUNGSUNG_ㅕ:

                                    case JUNGSUNG_ㅖ:

                                    case JUNGSUNG_ㅛ:

                                    case JUNGSUNG_ㅠ:

                                    case JUNGSUNG_ㅣ:

                                        buffer.Append("j");

                                        break;

                                    default:

                                        buffer.Append("d");

                                        break;

                                }

                                break;

                            case CHOSUNG_ㅌ:

                                buffer.Append("t");

                                if (useHyphenWhenVowelConfused)
                                {

                                    buffer.Append("-");

                                }

                                break;

                            case CHOSUNG_ㅎ:

                                buffer.Append("t");

                                skipNextChosung = true;

                                break;

                            default:

                                buffer.Append("t");
                                break;
                        }

                        break;

                    case JONGSUNG_ㄹ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄴ:

                            case CHOSUNG_ㄹ:

                                buffer.Append("ll");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅎ:

                                skipNextChosung = true;
                                buffer.Append("r");
                                break;

                            case CHOSUNG_ㅇ:

                                buffer.Append("r");

                                break;

                            default:

                                buffer.Append("l");
                                break;
                        }

                        break;

                    case JONGSUNG_ㄺ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄱ:

                            case CHOSUNG_ㄲ:

                                buffer.Append("lkk");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㄴ:

                                buffer.Append("ll");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅂ:

                                buffer.Append("lpp");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅃ:

                            case CHOSUNG_ㅊ:

                            case CHOSUNG_ㅅ:

                            case CHOSUNG_ㅋ:

                            case CHOSUNG_ㅌ:

                            case CHOSUNG_ㅍ:

                            case CHOSUNG_ㄸ:

                                buffer.Append("l");

                                break;

                            case CHOSUNG_ㄷ:

                                buffer.Append("ltt");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㄹ:

                                buffer.Append("ngn");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅁ:

                                buffer.Append("ng");

                                break;

                            case CHOSUNG_ㅆ:

                                buffer.Append("lss");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅇ:

                                buffer.Append("lg");

                                break;

                            case CHOSUNG_ㅈ:

                            case CHOSUNG_ㅉ:

                                buffer.Append("ljj");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅎ:

                                buffer.Append("lk");

                                skipNextChosung = true;

                                break;

                            default:

                                buffer.Append("k");

                                break;

                        }

                        break;

                    case JONGSUNG_ㄻ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄱ:

                                buffer.Append("mkk");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㄷ:

                                buffer.Append("mtt");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㄹ:

                                buffer.Append("ll");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅁ:

                                buffer.Append("l");

                                break;

                            case CHOSUNG_ㅂ:

                                buffer.Append("mpp");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅅ:

                                buffer.Append("mss");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅇ:

                                buffer.Append("lm");

                                break;

                            case CHOSUNG_ㅈ:

                                buffer.Append("mjj");

                                skipNextChosung = true;

                                break;

                            default:

                                buffer.Append("m");
                                break;
                        }

                        break;

                    case JONGSUNG_ㄼ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄱ:

                                buffer.Append("lkk");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㄴ:

                                buffer.Append("m");

                                break;

                            case CHOSUNG_ㅎ:

                                skipNextChosung = true;
                                buffer.Append("p");
                                break;

                            case CHOSUNG_ㄷ:

                            case CHOSUNG_ㅂ:

                            case CHOSUNG_ㄸ:

                            case CHOSUNG_ㅅ:

                            case CHOSUNG_ㅆ:

                            case CHOSUNG_ㅈ:

                            case CHOSUNG_ㅉ:

                            case CHOSUNG_ㅊ:

                            case CHOSUNG_ㅋ:

                            case CHOSUNG_ㅌ:

                                buffer.Append("p");

                                break;

                            case CHOSUNG_ㄹ:

                                buffer.Append("mn");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅃ:

                                break;

                            case CHOSUNG_ㅇ:

                                buffer.Append("lb");

                                skipNextChosung = true;

                                break;

                            default:

                                buffer.Append("l");

                                break;

                        }

                        break;

                    case JONGSUNG_ㄽ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄱ:

                                buffer.Append("lkk");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㄷ:

                                buffer.Append("ltt");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㄹ:

                                buffer.Append("ll");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅂ:

                                buffer.Append("lpp");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅇ:

                            case CHOSUNG_ㅎ:

                                buffer.Append("ls");

                                skipNextChosung = true;

                                break;

                            default:

                                buffer.Append("l");

                                break;

                        }

                        break;

                    case JONGSUNG_ㄾ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄱ:

                                buffer.Append("lkk");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㄹ:

                                buffer.Append("ll");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅂ:

                                buffer.Append("lp");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㄷ:

                            case CHOSUNG_ㅇ:

                            case CHOSUNG_ㅎ:

                                buffer.Append("lt");

                                skipNextChosung = true;

                                break;

                            default:

                                buffer.Append("l");

                                break;

                        }

                        break;

                    case JONGSUNG_ㄿ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄱ:

                                buffer.Append("lkk");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㄴ:

                                buffer.Append("m");

                                break;

                            case CHOSUNG_ㄷ:

                            case CHOSUNG_ㅂ:

                            case CHOSUNG_ㄸ:

                            case CHOSUNG_ㅅ:

                            case CHOSUNG_ㅆ:

                            case CHOSUNG_ㅈ:

                            case CHOSUNG_ㅉ:

                            case CHOSUNG_ㅊ:

                            case CHOSUNG_ㅋ:

                            case CHOSUNG_ㅌ:

                            case CHOSUNG_ㅎ:

                                buffer.Append("p");

                                break;

                            case CHOSUNG_ㄹ:

                                buffer.Append("mn");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅃ:

                            case CHOSUNG_ㅍ:

                                break;

                            case CHOSUNG_ㅇ:

                                buffer.Append("lp");

                                skipNextChosung = true;

                                break;

                            default:

                                buffer.Append("l");

                                break;

                        }

                        break;

                    case JONGSUNG_ㅀ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄱ:

                                buffer.Append("lk");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㄴ:

                            case CHOSUNG_ㄹ:

                                buffer.Append("ll");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅎ:

                                skipNextChosung = true;
                                buffer.Append("r");
                                break;

                            case CHOSUNG_ㅇ:

                                buffer.Append("r");

                                break;

                            default:

                                buffer.Append("l");

                                break;

                        }

                        break;

                    case JONGSUNG_ㅁ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄷ:

                                buffer.Append("mtt");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㄹ:

                                buffer.Append("mn");

                                skipNextChosung = true;

                                break;

                            default:

                                buffer.Append("m");

                                break;

                        }

                        break;

                    case JONGSUNG_ㅂ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄴ:

                            case CHOSUNG_ㄹ:

                                buffer.Append("mn");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅁ:

                                buffer.Append("m");

                                break;

                            case CHOSUNG_ㅃ:

                                break;

                            case CHOSUNG_ㅇ:

                                buffer.Append("b");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅎ:

                                buffer.Append("p");

                                break;

                            case CHOSUNG_ㅍ:

                                buffer.Append("p");

                                if (useHyphenWhenVowelConfused)
                                {

                                    buffer.Append("-");

                                }

                                break;

                            default:

                                buffer.Append("p");

                                break;

                        }

                        break;

                    case JONGSUNG_ㅄ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄹ:

                                buffer.Append("mn");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㄴ:

                            case CHOSUNG_ㅁ:

                                buffer.Append("m");

                                break;

                            case CHOSUNG_ㅃ:

                                break;

                            case CHOSUNG_ㅇ:

                                buffer.Append("ps");

                                skipNextChosung = true;

                                break;

                            default:

                                buffer.Append("p");

                                break;

                        }

                        break;

                    case JONGSUNG_ㅅ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄴ:

                            case CHOSUNG_ㅁ:

                                buffer.Append("n");

                                break;

                            case CHOSUNG_ㄸ:

                                break;

                            case CHOSUNG_ㄹ:

                                buffer.Append("nn");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅇ:

                                buffer.Append("s");

                                break;

                            case CHOSUNG_ㅌ:

                            case CHOSUNG_ㅎ:

                                buffer.Append("t");

                                if (useHyphenWhenVowelConfused)
                                {

                                    buffer.Append("-");

                                }

                                break;

                            default:

                                buffer.Append("t");

                                break;

                        }

                        break;

                    case JONGSUNG_ㅆ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄴ:

                            case CHOSUNG_ㅁ:

                                buffer.Append("n");

                                break;

                            case CHOSUNG_ㄸ:

                                break;

                            case CHOSUNG_ㄹ:

                                buffer.Append("nn");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅇ:

                                buffer.Append("tss");

                                break;

                            case CHOSUNG_ㅌ:

                            case CHOSUNG_ㅎ:

                                buffer.Append("t");

                                if (useHyphenWhenVowelConfused)
                                {

                                    buffer.Append("-");

                                }

                                break;

                            default:

                                buffer.Append("t");

                                break;

                        }

                        break;

                    case JONGSUNG_ㅇ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄹ:

                                buffer.Append("ngn");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅇ:

                                buffer.Append("ng");

                                if (useHyphenWhenVowelConfused)
                                {

                                    buffer.Append("-");

                                }

                                break;

                            default:

                                buffer.Append("ng");

                                break;

                        }

                        break;

                    case JONGSUNG_ㅈ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄸ:

                                break;

                            case CHOSUNG_ㄴ:

                            case CHOSUNG_ㅁ:

                                buffer.Append("n");

                                break;

                            case CHOSUNG_ㅊ:

                                buffer.Append("t");

                                break;

                            case CHOSUNG_ㄹ:

                                buffer.Append("nn");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅇ:

                                buffer.Append("j");

                                break;

                            case CHOSUNG_ㅌ:

                            case CHOSUNG_ㅎ:

                                switch (nextJungsung)
                                {

                                    case JUNGSUNG_ㅑ:

                                    case JUNGSUNG_ㅒ:

                                    case JUNGSUNG_ㅕ:

                                    case JUNGSUNG_ㅖ:

                                    case JUNGSUNG_ㅛ:

                                    case JUNGSUNG_ㅠ:

                                    case JUNGSUNG_ㅣ:

                                        buffer.Append("ch");

                                        skipNextChosung = true;

                                        break;

                                    default:

                                        buffer.Append("t");

                                        if (useHyphenWhenVowelConfused)
                                        {

                                            buffer.Append("-");

                                        }

                                        break;

                                }

                                break;

                            default:

                                buffer.Append("t");

                                break;

                        }

                        break;

                    case JONGSUNG_ㅊ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄸ:

                                break;

                            case CHOSUNG_ㄴ:

                            case CHOSUNG_ㅁ:

                                buffer.Append("n");

                                break;

                            case CHOSUNG_ㄹ:

                                buffer.Append("nn");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅇ:

                                buffer.Append("ch");

                                break;

                            case CHOSUNG_ㅌ:

                            case CHOSUNG_ㅎ:

                                buffer.Append("t");

                                if (useHyphenWhenVowelConfused)
                                {

                                    buffer.Append("-");

                                }

                                break;

                            default:

                                buffer.Append("t");

                                break;

                        }

                        break;

                    case JONGSUNG_ㅋ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄲ:

                                break;

                            case CHOSUNG_ㄴ:

                            case CHOSUNG_ㅁ:

                                buffer.Append("ng");

                                break;

                            case CHOSUNG_ㄹ:

                                buffer.Append("ngn");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅎ:

                                buffer.Append("k");

                                skipNextChosung = true;

                                break;

                            default:

                                buffer.Append("k");

                                break;

                        }

                        break;

                    case JONGSUNG_ㅌ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄸ:

                                break;

                            case CHOSUNG_ㄴ:

                            case CHOSUNG_ㅁ:

                                buffer.Append("n");

                                break;

                            case CHOSUNG_ㄹ:

                                buffer.Append("ll");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅇ:

                                switch (nextJungsung)
                                {

                                    case JUNGSUNG_ㅑ:

                                    case JUNGSUNG_ㅒ:

                                    case JUNGSUNG_ㅕ:

                                    case JUNGSUNG_ㅖ:

                                    case JUNGSUNG_ㅛ:

                                    case JUNGSUNG_ㅠ:

                                    case JUNGSUNG_ㅣ:

                                        buffer.Append("ch");

                                        break;

                                    default:

                                        buffer.Append("t");
                                        break;
                                }

                                break;

                            case CHOSUNG_ㅌ:

                                buffer.Append("t");

                                if (useHyphenWhenVowelConfused)
                                {

                                    buffer.Append("-");

                                }

                                break;

                            case CHOSUNG_ㅎ:

                                buffer.Append("t");

                                skipNextChosung = true;

                                break;

                            default:

                                buffer.Append("t");

                                break;

                        }

                        break;

                    case JONGSUNG_ㅍ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㄹ:

                                buffer.Append("mn");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅁ:

                                buffer.Append("m");

                                break;

                            case CHOSUNG_ㅃ:

                                break;

                            case CHOSUNG_ㅇ:

                                buffer.Append("p");

                                skipNextChosung = true;

                                break;

                            default:

                                buffer.Append("p");

                                break;

                        }

                        break;

                    case JONGSUNG_ㅎ:

                        switch (nextChosung)
                        {

                            case CHOSUNG_ㅇ:

                            case CHOSUNG_ㅎ:

                                break;

                            case CHOSUNG_ㄱ:

                                buffer.Append("k");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㄷ:

                                buffer.Append("t");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㄴ:

                            case CHOSUNG_ㅁ:

                                buffer.Append("n");

                                break;

                            case CHOSUNG_ㄹ:

                                buffer.Append("nn");

                                skipNextChosung = true;

                                break;

                            case CHOSUNG_ㅈ:

                                buffer.Append("ch");

                                skipNextChosung = true;

                                break;

                            default:

                                buffer.Append("t");

                                break;

                        }

                        break;

                }

            }

            return buffer.ToString();

        }
    }
}
