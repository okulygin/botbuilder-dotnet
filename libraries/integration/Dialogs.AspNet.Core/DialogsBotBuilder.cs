using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.Bot.Builder.Integration.AspNet.Core
{

    public class DialogsBotBuilder
    {
        private readonly IServiceCollection _services;

        public DialogsBotBuilder(IServiceCollection services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public IServiceCollection Services => _services;

        public DialogsBotBuilder UseBotState(BotState botState, string statePropertyName = null)
        {
            _services.AddSingleton(botState.CreateProperty<DialogState>(statePropertyName ?? "__DEFAULT_DIALOG_STATE"));

            return this;
        }

        public DialogsBotBuilder UseDialogFactory(IDialogFactory factory)
        {
            _services.AddSingleton(factory);

            return this;
        }

        public DialogsBotBuilder UseDialogFactory(Func<IServiceProvider, IDialogFactory> dialogsFactoryFactory)
        {
            _services.AddSingleton(dialogsFactoryFactory);

            return this;
        }
    }
}
