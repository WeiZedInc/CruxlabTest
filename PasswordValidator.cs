namespace CruxlabTest
{
	internal class PasswordValidator
	{
		readonly string _FILEPATH;
		List<string> _textLinesList;
		int _validPasswordsCounter;

		public PasswordValidator(string filePath)
		{
			_FILEPATH = filePath;
			_validPasswordsCounter = 0;
			_textLinesList = new();

			ReadFile();
		}

		public void ShowValidCount() => Console.WriteLine($"Input file contains {_validPasswordsCounter} valid passwords.");

		void ReadFile() // reading from input file
		{
			try
			{
				string line;
				using (StreamReader sr = new StreamReader(_FILEPATH))
				{
					while ((line = sr.ReadLine()) != null)
					{
						if (!string.IsNullOrWhiteSpace(line))
							_textLinesList.Add(line);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error at ReadFile() " + ex.Message);
			}
		}

		public void Validate() // looping through lines to find matches with our conditions and counting them up
		{
			try
			{
				char charToMatch;
				int counter = 0;
				(int min, int max) condition;

				foreach (string line in _textLinesList)
				{
					charToMatch = line[0];
					condition = FindCondition(line);

					foreach (char c in line)
					{
						if (charToMatch == c)
							counter++;
					}
					if (counter >= condition.min && counter <= condition.max)
						_validPasswordsCounter++;

					counter = 0;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error at Validate() " + ex.Message);
			}
		}

		(int min, int max) FindCondition(string line) // looking for our condition Ex. 2-3; 1-5 etc
		{
			try
			{
				string substring = string.Empty;
				int startIndex = 2; // condition starts at 2-nd index
				int endIndex = line.IndexOf(':', startIndex);

				if (startIndex != -1 && endIndex != -1)
					substring = line.Substring(startIndex, endIndex - startIndex);

				string[] conditions = substring.Split('-');

				return (int.Parse(conditions[0]), int.Parse(conditions[1]));
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error at FindCondition() " + ex.Message);
				return (0, 0);
			}
		}
	}
}
