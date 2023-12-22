using Microsoft.Extensions.Logging;
using Rollbar;
using Rollbar.NetPlatformExtensions;

namespace RollbarMauiApp
{
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

#if DEBUG
    		builder.Logging.AddDebug();
           
#endif


            // STEP 3 - Setup Rollbar Infrastructure
            ConfigureRollbarInfrastructure();

            // STEP 4 - Add Rollbar logger with the log level filter accordingly configured
            builder.Services.AddRollbarLogger(loggerOptions =>
            {
                loggerOptions.Filter =
                  (loggerName, loglevel) => loglevel >= LogLevel.Trace;
            });

            return builder.Build();
        }


        // STEP 3 - Setup Rollbar Infrastructure:
        static void ConfigureRollbarInfrastructure()
        {
            RollbarInfrastructureConfig config = new RollbarInfrastructureConfig(
              "YOUR_ACCESS_TOKEN",
              "development"
            );
            RollbarDataSecurityOptions dataSecurityOptions = new RollbarDataSecurityOptions();
            dataSecurityOptions.ScrubFields = new string[]
            {
      "url",
      "method",
            };
            config.RollbarLoggerConfig.RollbarDataSecurityOptions.Reconfigure(dataSecurityOptions);

            RollbarInfrastructure.Instance.Init(config);
        }

    }
}
