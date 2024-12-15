using SegmentFilter.Models;
using System.Globalization;
using System.IO;

namespace SegmentFilter.Services;


class PlainFileService : IFileService<List<Shape>>
{
    private readonly NumberFormatInfo numberFormatInfo = new()
    {
        NumberDecimalSeparator = ",",
    };

    public async Task<List<Shape>?> ReadFileAsync(string path)
    {
        using var stream = File.OpenRead(path);
        using var reader = new StreamReader(stream);

        var shape = new Shape();
        var result = new List<Shape>();

        for (int lineNumber = 1; !reader.EndOfStream; lineNumber++)
        {
            var line = await reader.ReadLineAsync();

            if (string.IsNullOrWhiteSpace(line))
            {
                if (shape != null && shape.Points.Count > 0)
                    result.Add(shape);

                shape = new();
                continue;
            }

            var scalarTexts = line.Split(' ', '\t');
            if (scalarTexts.Length != 2)
                throw new FormatException($"Строка {lineNumber} имеет неправильный формат");

            var x = double.Parse(scalarTexts[0], numberFormatInfo);
            var y = double.Parse(scalarTexts[1], numberFormatInfo);

            shape.Points.Add(new(x, y));
        }

        return result;
    }

    public async Task SaveToFileAsync(List<Shape> value, string path)
    {
        using var stream = File.OpenWrite(path);
        using var writer = new StreamWriter(stream);

        foreach (var shape in value)
        {
            foreach (var point in shape.Points)
            {
                await writer.WriteAsync(point.X.ToString(numberFormatInfo));
                await writer.WriteAsync(' ');
                await writer.WriteAsync(point.Y.ToString(numberFormatInfo));
                await writer.WriteLineAsync();
            }

            if (shape.Points.Count > 0)
                await writer.WriteLineAsync();
        }
    }
}
