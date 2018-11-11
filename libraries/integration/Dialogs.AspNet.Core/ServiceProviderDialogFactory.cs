using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.Bot.Builder.Integration.AspNet.Core
{
    internal delegate Dialog DialogFactory(IServiceProvider serviceProvider, string dialogId);
    
    internal sealed class ServiceProviderDialogFactory : IDialogFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDictionary<string, DialogFactory> _dialogFactories;

        public ServiceProviderDialogFactory(IServiceProvider serviceProvider, IDictionary<string, DialogFactory> dialogFactories)
        {
            _serviceProvider = serviceProvider;
            _dialogFactories = dialogFactories ?? throw new ArgumentNullException(nameof(dialogFactories));
        }

        public Dialog GetDialog(string dialogId)
        {
            if (string.IsNullOrEmpty(dialogId))
            {
                throw new ArgumentException("Expected non-null/empty value.", dialogId);
            }

            if (!_dialogFactories.TryGetValue(dialogId, out var dialogFactory))
            {
                return default(Dialog);
            }

            return dialogFactory(_serviceProvider, dialogId);
        }
    }
}
