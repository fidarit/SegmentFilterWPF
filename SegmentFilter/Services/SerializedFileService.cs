using System.IO;
using System.Text.Json;

namespace SegmentFilter.Services;


class SerializedFileService<T> : IFileService<T> where T : class
{
    public async Task<T?> ReadFileAsync(string path)
    {
        using var stream = File.OpenRead(path);

        return await JsonSerializer.DeserializeAsync<T>(stream);
    }

    public async Task SaveToFileAsync(T value, string path)
    {
        using var stream = File.OpenWrite(path);

        await JsonSerializer.SerializeAsync(stream, value);
    }
}
