using SegmentFilter.Services;

namespace SegmentFilter.Tests;


public class PlainFileServiceTests
{
    private readonly PlainFileService service = new();

    private const string GoodFileName = @"Resources\FileToImport.txt";
    private const string BadFileName = @"Resources\BadFileToImport.txt";

    [Fact]
    public async Task ReadFile()
    {
        var result = await service.ReadFileAsync(GoodFileName);

        Assert.NotNull(result);
        Assert.NotEmpty(result);

        Assert.NotEmpty(result[0].Points);
    }

    [Fact]
    public async Task ReadBadFile()
    {
        await Assert.ThrowsAsync<FormatException>(() => service.ReadFileAsync(BadFileName));
    }
}