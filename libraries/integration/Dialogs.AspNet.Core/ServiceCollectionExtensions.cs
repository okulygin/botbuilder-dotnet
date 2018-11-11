using System;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Bot.Builder.Integration.AspNet.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDialogsBot(this IServiceCollection services, Action<DialogsBotBuilder> build, Action<BotFrameworkOptions> configure = null) =>
            services.AddDialogsBot<DialogsBot>(build, configure);

        public static IServiceCollection AddDialogsBot<TDialogsBot>(this IServiceCollection services, Action<DialogsBotBuilder> build, Action<BotFrameworkOptions> configure = null)
            where TDialogsBot : DialogsBot
        {
            build(new DialogsBotBuilder(services));

            return services.AddBot<TDialogsBot>(configure);
        }
    }
}
