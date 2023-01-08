using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;


Console.WriteLine("Azure App Configuration Article Demo\n");


//Retrieve the Connection String from Azure App Configuration Resource
const string connectionString = "Endpoint=https://app-config-blog-usage.azconfig.io;Id=Wnxq-l0-s0:yDGmY+NIW6MuFJktMRaz;Secret=66JVN5RTtuKcVH+Mn28CK97kGzmNNCSC3LlEN62aNiY=";


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