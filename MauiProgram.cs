using Microsoft.Extensions.Logging;

namespace EstoqueAlmoxarifado.Maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

			//Registrar serviços
			builder.Services.AddSingleton<IApiService, ApiService>();
			builder.Services.AddTransient<ItensViewModel>();
			builder.Services.AddTransient<ItensPage>();

		return builder.Build();
	}
}
