using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;


Console.WriteLine("Azure App Configuration Article Demo\n");


//Retrieve the Connection String from Azure App Configuration Resource
const string connectionString = "{Azure_App_ConnectionString}";


var configuration = new ConfigurationBuilder()
            .AddAzureAppConfiguration(options =>
            {
                options.Connect(connectionString).UseFeatureFlags();
            }).Build();


var services = new ServiceCollection();


services.AddSingleton<IConfiguration>(configuration).AddFeatureManagement();


using (var serviceProvider = services.BuildServiceProvider())
{
    var featureManager = serviceProvider.GetRequiredService<IFeatureManager>();


    //read great day feature
    if (await featureManager.IsEnabledAsync("TogglePreviewFeature"))
    {
        Console.WriteLine("Preview Feature Enabled!!!");
    }
    else
    {
        Console.WriteLine("Preview Feature Disabled!");
    }
};