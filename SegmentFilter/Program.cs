﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SegmentFilter.Views;

namespace SegmentFilter;


public class Program
{
    [STAThread]
    public static void Main()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<App>();
                services.AddSingleton<MainWindow>();
            })
            .Build();

        var app = host.Services.GetService<App>();
        app?.Run();
    }
}
