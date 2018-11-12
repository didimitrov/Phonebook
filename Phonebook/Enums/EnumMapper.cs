using System;

namespace Phonebook.Enums
{
    public static class EnumMapper
    {
        public static MenuOption ReadMenuOption(string input)
        {
            if (!Enum.TryParse(input, out MenuOption menuOption))
            {
                menuOption = MenuOption.unknown;
            }

            return menuOption;
        }

        public static AnswerOption ReadContinueOption(string input)
        { 
            if (!Enum.TryParse(input, out AnswerOption answer))
            {
                answer = AnswerOption.unknown;
            }

            return answer;
        }

        public static SearchOption ReadSearchOption(string input)
        {
            if (!Enum.TryParse(input, out SearchOption searchOption))
            {
                searchOption = SearchOption.unknown;
            }

            return searchOption;
        }
    }
}
