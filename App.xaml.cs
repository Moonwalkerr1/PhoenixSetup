using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoenixSetup.Service.Settings;
using System.IO;
using System.Windows;

namespace PhoenixSetup
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		#region Data
		#region Property
		public static IServiceProvider? ServiceProvider { get; private set; }
		public static IConfiguration? Configuration { get; private set; }
		#endregion
		#endregion

		#region Ovveride
		protected override void OnStartup(StartupEventArgs e)
		{
			var services = new ServiceCollection();

			Configuration = Configuration = BuildConfiguration();

			services.Configure<InstallLinksSettings>(Configuration.GetSection(nameof(InstallLinksSettings)));

			ConfigureSettings(services);
			ConfigureServices(services);
			ConfigureViews(services);

			ServiceProvider = services.BuildServiceProvider();

			var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
			mainWindow.Show();
		}
		#endregion

		#region Private
		private IConfiguration BuildConfiguration()
		{
			return new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();
		}

		private void ConfigureServices(IServiceCollection services)
		{
		}

		private void ConfigureViews(IServiceCollection services)
		{
			services.AddSingleton<MainWindow>();
		}

		private void ConfigureSettings(IServiceCollection services)
		{
			services.Configure<InstallLinksSettings>(Configuration!.GetSection(nameof(InstallLinksSettings)));
		}
		#endregion
	}
}
