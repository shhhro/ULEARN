using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace linq_slideviews;
public class ParsingTask
{
	public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
	{
		IDictionary<int, SlideRecord> slides = new Dictionary<int, SlideRecord>();

		return lines.Skip(1)
			.Select(line => line.Split(';', StringSplitOptions.RemoveEmptyEntries))
			.Where(line => line.Length == 3 &&
					int.TryParse(line[0], out int id) &&
					Enum.TryParse(line[1], true, out SlideType _type))
			.Select(line =>
			{
				Enum.TryParse(line[1], true, out SlideType slideType);
				return new SlideRecord(int.Parse(line[0]), slideType, line[2]);
			})
			.ToDictionary(record => record.SlideId);
	}

    public static IEnumerable<VisitRecord> ParseVisitRecords(
        IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
    {
        var visitRecords = new List<VisitRecord>();

        foreach (var line in lines.Skip(1))
        {
            var exceptionMessage = $"Wrong line [{line}]";
            var visitInformation = line.Split(';', StringSplitOptions.RemoveEmptyEntries);

            if (visitInformation.Length != 4
                || !int.TryParse(visitInformation[0], out int userId)
                || !int.TryParse(visitInformation[1], out int slideId)
                || !slides.ContainsKey(slideId)
                || !DateTime.TryParse($"{visitInformation[2]} {visitInformation[3]}",
                out DateTime dateTime))
                throw new FormatException(exceptionMessage);
            var visitRecord = new VisitRecord(userId, slideId, dateTime,
                slides[slideId].SlideType);
            visitRecords.Add(visitRecord);
        }
        return visitRecords.ToArray();
    }
}
