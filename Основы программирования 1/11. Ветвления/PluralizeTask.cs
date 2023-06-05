namespace Pluralize
{
	public static class PluralizeTask
	{
		public static string PluralizeRubles(int count)
		{
            var moreThanTen = GetLastDozen(count) > 10;
			var secondDozen = GetLastDozen(count) <= 20 && moreThanTen;
            if (GetLastDigit(count) == 1 && !secondDozen) return "рубль";
            if ((GetLastDigit(count) <= 4 && GetLastDigit(count) > 0) && !secondDozen) return "рубля";
            return "рублей";
        }
		static int GetLastDigit(int count)
		{
			return count % 10;
        }
		static int GetLastDozen(int count)
		{
			return count % 100;
        }
	}
}