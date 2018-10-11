using System;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Bot.Builder.Integration.AspNet.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDialogBot<TDialogSet>(this IServiceCollection services, IStatePropertyAccessor<DialogState> dialogStatePropertyAccessor, Action<BotFrameworkOptions> configureAction = null)
            where TDialogSet : DialogSet
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddBot(new DialogBot<TDialogSet>(dialogStatePropertyAccessor), configureAction);

            return services;
        }
    }
}
