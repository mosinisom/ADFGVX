using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Services
{
  // шифр ADFGVX с русским алфавитом
  public class CipherService
  {
    Random random = new Random();
    private readonly int MaxRnd = russianWords.Length - 1;
    private readonly char[] letters = { 'a', 'd', 'f', 'g', 'v', 'x' };

    private readonly char[,] tableADFGVX =
    {
      { 'а', 'б', 'в', 'г', 'д', 'е' },
      { 'ё', 'ж', 'з', 'и', 'й', 'к' },
      { 'л', 'м', 'н', 'о', 'п', 'р' },
      { 'с', 'т', 'у', 'ф', 'х', 'ц' },
      { 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь' },
      { 'э', 'ю', 'я', ' ', '.', ',' }
    };

    private readonly static string[] russianWords =
    {
      "автомобиль", "бабушка", "вода", "город", "дом", "еж", "жара", "зима", "игра", "йогурт",
      "книга", "лиса", "море", "ночь", "облако", "птица", "река", "солнце", "тигр", "улица",
      "флаг", "хлеб", "цветок", "чай", "школа", "яблоко", "автобус", "банан", "велосипед", "груша",
      "друг", "ежевика", "животное", "зуб", "игрушка", "йога", "картина", "лампа", "машина", "носок",
      "обезьяна", "печенье", "работа", "собака", "телефон", "учебник", "фрукт", "хомяк", "цвет",
      "шарик", "яблоня", "артист", "буква", "вино", "гриб", "дождь", "ежик", "жизнь", "зебра",
      "игла", "йод", "картофель", "лист", "молоко", "новость", "окно", "память", "роман", "снег",
      "трава", "ученик", "фен", "хоккей", "часы", "шарф", "язык", "альбом", "береза", "вино",
      "городок", "доска", "единица", "жук", "звонок", "идея", "йогурт", "кабан", "лук", "мебель",
      "ночлег", "орел", "палец", "рука", "сапог", "туча", "учитель", "фея", "химия", "чайка",
      "шапка", "якорь", "антенна", "берег", "весна", "гитара", "дерево", "ежик", "журнал", "золото",
      "интернет", "кабинет", "лента", "маска", "новый", "олень", "печь", "роза", "сыр", "тропа",
      "улей", "фонарь", "холм", "цирк", "шоколад", "ясень", "авиатор", "блокнот", "великан", "голос",
      "день", "ежегодник", "жена", "знак", "изюм", "команда", "лебедь", "мишка", "нос", "ответ",
      "перо", "ромашка", "стол", "трактор", "угол", "ферма", "хитрость", "черепаха", "штука", "яблонька",
      "апельсин", "бархат", "ваза", "горка", "деревня", "ежевика", "желание", "задание", "изба", "конь",
      "лапша", "мост", "небо", "оазис", "письмо", "роса", "стекло", "традиция", "уверенность", "флот",
      "художник", "цель", "чемодан", "школяр", "ягода", "ангел", "батарея", "вера", "голова", "древо",
      "еженедельник", "жемчуг", "зонтик", "капуста", "линейка", "мыло", "окорок", "песня", "рыба", "свет",
      "тур", "ученость", "флагман", "хвост", "цветник", "час", "щенок", "экран", "юбка", "автоматика"
    };

    private List<char> ReplaceLetters(string text)
    {
      var result = new List<char>();
      foreach (var letter in text)
      {
        for (int i = 0; i < 6; i++)
        {
          for (int j = 0; j < 6; j++)
          {
            if (tableADFGVX[i, j] == letter)
            {
              result.Add(letters[j]);
              result.Add(letters[i]);
            }
          }
        }
      }
      return result;
    }

    private List<char> TransposeLetters(string key, List<char> replacedLetters)
    {
      key = key.Replace(" ", "");

      int numRows = (int)Math.Ceiling((double)replacedLetters.Count / key.Length);
      char[,] grid = new char[numRows + 1, key.Length];

      for (int i = 0; i < key.Length; i++)
      {
        grid[0, i] = key[i];
      }

      int index = 0;
      for (int row = 1; row <= numRows; row++)
      {
        for (int col = 0; col < key.Length; col++)
        {
          if (index < replacedLetters.Count)
          {
            grid[row, col] = replacedLetters[index++];
          }
          else
          {
            grid[row, col] = ' ';
          }
        }
      }

      var columns = new List<(char letter, int index)>();
      for (int i = 0; i < key.Length; i++)
      {
        columns.Add((grid[0, i], i));
      }

      columns.Sort((a, b) => a.letter.CompareTo(b.letter));

      var result = new List<char>();
      foreach (var column in columns)
      {
        for (int row = 1; row <= numRows; row++)
        {
          result.Add(grid[row, column.index]);
        }
      }

      return result;
    }

    public string Encrypt(string text, string key)
    {
      var replacedLetters = ReplaceLetters(text);
      var transposedLetters = TransposeLetters(key, replacedLetters);
      var result = string.Join("", transposedLetters);
      return result;
    }

    public string Decrypt(string text, string key)
    {
        return "Decrypted text";
    }



    public string GenerateKey()
    {
      string firstWord = russianWords[random.Next(MaxRnd)];
      string secondWord = russianWords[random.Next(MaxRnd)];
      string thirdWord = russianWords[random.Next(MaxRnd)];

      return $"{firstWord} {secondWord} {thirdWord}";
    }
  }
}