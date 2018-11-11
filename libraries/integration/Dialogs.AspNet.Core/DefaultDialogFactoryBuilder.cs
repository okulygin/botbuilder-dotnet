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
    public sealed class DefaultDialogFactoryBuilder
    {
        private readonly Dictionary<string, DialogFactory> _dialogFactories = new Dictionary<string, DialogFactory>();

        public DefaultDialogFactoryBuilder()
        {
        }

        internal Dictionary<string, DialogFactory> DialogFactories => _dialogFactories;

        public DefaultDialogFactoryBuilder AddDialog(Dialog dialog)
        {
            if (dialog == null)
            {
                throw new ArgumentNullException(nameof(dialog));
            }

            if (string.IsNullOrEmpty(dialog.Id))
            {
                throw new ArgumentException($"Singleton {nameof(Dialog)} instances must have their {nameof(Dialog.Id)} properties set to a non-null/empty value.");
            }

            _dialogFactories.Add(
                dialog.Id,
                (sp, dialogId) => dialog);

            return this;
        }

        public DefaultDialogFactoryBuilder AddDialog(string dialogId, Type dialogType, params object[] additionalDialogArguments)
        {
            if (string.IsNullOrEmpty(dialogId))
            {
                throw new ArgumentException("Expected non-null/empty value.", nameof(dialogId));
            }

            if (dialogType == null)
            {
                throw new ArgumentNullException(nameof(dialogType));
            }

            if (!typeof(Dialog).IsAssignableFrom(dialogType))
            {
                throw new ArgumentException($"Type must be a subtype of {nameof(Dialog)}.", nameof(dialogType));
            }

            if (additionalDialogArguments != null && additionalDialogArguments.Length > 0)
            {
                var objectFactory = ActivatorUtilities.CreateFactory(dialogType, additionalDialogArguments?.Select(o => o.GetType()).ToArray() ?? Type.EmptyTypes);

                _dialogFactories.Add(dialogId, (sp, did) =>
                {
                    var dialog = objectFactory(sp, additionalDialogArguments) as Dialog;

                    dialog.Id = did;

                    return dialog;
                });
            }
            else
            {
                _dialogFactories.Add(dialogId, (sp, did) =>
                {
                    var dialog = ActivatorUtilities.GetServiceOrCreateInstance(sp, dialogType) as Dialog;

                    dialog.Id = did;

                    return dialog;
                });
            }

            return this;
        }

        public DefaultDialogFactoryBuilder AddDialog(string dialogId, Func<IServiceProvider, Dialog> dialogFactory)
        {
            if (string.IsNullOrEmpty(dialogId))
            {
                throw new ArgumentException("Expected non-null/empty value.", nameof(dialogId));
            }

            if (dialogFactory == null)
            {
                throw new ArgumentNullException(nameof(dialogFactory));
            }

            _dialogFactories.Add(dialogId, (sp, did) =>
            {
                var dialog = dialogFactory(sp);

                dialog.Id = did;

                return dialog;
            });

            return this;
        }
    }
}
