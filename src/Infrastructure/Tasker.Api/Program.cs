

using Tasker.Api.Common.Extensions;
using Tasker.Api.Common.HostedServices;
using Tasker.Discord;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddConfiguration(builder.Configuration)
        .AddLogging(builder.Logging)
        .AddApplication()
        .AddWeb()
        .AddDiscordClient(builder.Configuration)
        .AddHostedService<DiscordBotService>();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.Run();
}