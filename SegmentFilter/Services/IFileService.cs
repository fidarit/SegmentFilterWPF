namespace SegmentFilter.Services;


interface IFileService<T> where T : class
{
    /// <exception cref="FormatException">Файл неправильно заполнен</exception>
    Task<T?> ReadFileAsync(string path);

    Task SaveToFileAsync(T value, string path);
}
