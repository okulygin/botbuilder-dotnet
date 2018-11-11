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

    public static class DialogsBotBuilderExtensions
    {
        public static DialogsBotBuilder UseDefaultDialogFactory(this DialogsBotBuilder builder, Action<DefaultDialogFactoryBuilder> configure)
        {
            var defaultDialogFactoryBuilder = new DefaultDialogFactoryBuilder();

            configure(defaultDialogFactoryBuilder);

            var dialogFactories = defaultDialogFactoryBuilder.DialogFactories;

            builder.Services.AddTransient<IDialogFactory, ServiceProviderDialogFactory>(sp => new ServiceProviderDialogFactory(sp, dialogFactories));

            return builder;
        }

        public static DialogsBotBuilder UseDialogSet(this DialogsBotBuilder builder, DialogSet dialogSet) =>
            builder.UseDialogFactory(dialogSet);
    }
}
