using Microsoft.Win32;
using System.IO;

namespace SegmentFilter.Services;



interface IFileDialogService
{
    bool OpenFileDialog(ref string path, string? filter = null, string? title = null);
    bool SaveFileDilog(ref string path, string? filter = null, string? title = null);
}


class FileDialogService : IFileDialogService
{
    public bool OpenFileDialog(ref string path, string? filter = null, string? title = null)
    {
        var dialog = new OpenFileDialog();

        return ShowFileDilog(dialog, ref path, filter, title);
    }

    public bool SaveFileDilog(ref string path, string? filter = null, string? title = null)
    {
        var dialog = new SaveFileDialog();

        return ShowFileDilog(dialog, ref path, filter, title);
    }

    private static bool ShowFileDilog(FileDialog dialog, ref string path, string? filter, string? title)
    {
        if (filter != null)
            dialog.Filter = filter;

        if (title != null)
            dialog.Title = title;

        if (File.Exists(path))
            dialog.FileName = path;

        else if (Directory.Exists(path))
            dialog.InitialDirectory = path;

        if (dialog.ShowDialog() == true)
        {
            path = dialog.FileName;
            return true;
        }
        else
        {
            path = string.Empty;
            return false;
        }
    }
}
