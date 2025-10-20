namespace CertiPurge.OCR;

class Readers
{
	public static string GetContentFromWordString(string input)
	{
		var seperated = input.Split(' ');

		if (seperated.Length < 7)
		{
			return "";
		}

		string content = string.Join(" ", seperated[6..]);

		if (!content.StartsWith('#'))
		{
			return "";
		}

		content = content[1..];
		return content;
	}
	public static void ReadAQA(string text, out string name, out string course, out string grade, out string centrenum, out string candidate)
	{
		name = string.Empty;
		course = string.Empty;
		grade = string.Empty;
		centrenum = string.Empty;
		candidate = string.Empty;

		string previousLine = string.Empty;

		if (string.IsNullOrWhiteSpace(text))
		{
			return;
		}

		foreach (var line in text.Split("\n"))
		{
			if (string.IsNullOrWhiteSpace(line))
			{
				continue;
			}


			var content = GetContentFromWordString(line);

			if (string.IsNullOrWhiteSpace(content))
			{
				continue;
			}

			if (previousLine.Contains("certify that") && string.IsNullOrWhiteSpace(name))
			{
				var parts = content.Split("date of birth");

				name = parts[0].Trim();
			}
			else if (previousLine.Contains("Subject") && string.IsNullOrWhiteSpace(course))
			{
				if (content.Contains("GRADE"))
				{
					var split = content.Split("GRADE");
					course = split[0].Trim();
					grade = string.Join(" ", split[1..]).Trim();
				}
				else
				{
					course = content;
				}
			}
			else if (content.Contains("GRADE") && string.IsNullOrWhiteSpace(grade))
			{
				grade = string.Join(" ", content.Split(" ")[1..]).Trim();
			}
			else if (content.Contains("CENTRE No.") && string.IsNullOrWhiteSpace(centrenum))
			{
				content = content.Replace("CENTRE No./CANDIDATE No.", "").Trim();

				var parts = content.Split(' ');

				var focus = parts[0].Trim().Split('/');
				centrenum = focus[0].Trim();
				candidate = string.Join("/", focus[1..]).Trim();

				if (candidate.EndsWith("/"))
				{
					candidate = candidate[0..^1];
				}
			}

			previousLine = content;
		}
	}
	
	public static void ReadBTEC(string text, out string name, out string course, out string grade, out string centrenum, out string candidate)
	{
		name = string.Empty;
		course = string.Empty;
		grade = string.Empty;
		centrenum = string.Empty;
		candidate = string.Empty;

		string previousLine = string.Empty;

		if (string.IsNullOrWhiteSpace(text))
		{
			return;
		}

		foreach (var line in text.Split("\n"))
		{
			if (string.IsNullOrWhiteSpace(line))
			{
				continue;
			}


			var content = GetContentFromWordString(line);

			if (string.IsNullOrWhiteSpace(content))
			{
				continue;
			}

			if (previousLine.Contains("awarded to") && string.IsNullOrWhiteSpace(name))
			{
				var parts = content.Split("date of birth");

				name = parts[0].Trim();

				Console.WriteLine($"Extracted name: {name}");
			}
			else if (previousLine.Contains("Diploma") && string.IsNullOrWhiteSpace(course))
			{
				course = content[3..].Trim();
			}
			else if (content.Contains("GRADE") && string.IsNullOrWhiteSpace(grade))
			{
				grade = string.Join("", content.Split("GRADE")[1..]).Trim();
			}
			else if (previousLine.Contains("MORE THAN ONE PAGE") && string.IsNullOrWhiteSpace(centrenum))
			{
				var parts = content.Split(' ');

				centrenum = parts[0].Trim();
				candidate = parts[2].Split(":")[0].Trim();
			}

			previousLine = content;
		}
	}
}