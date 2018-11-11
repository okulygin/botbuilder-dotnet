using System;
using Microsoft.Bot.Builder.Dialogs;

namespace Microsoft.Bot.Builder.Integration.AspNet.Core
{
    public static class DefaultDialogFactoryBuilderExtensions
    {
        public static DefaultDialogFactoryBuilder AddRootDialog(this DefaultDialogFactoryBuilder builder, Type dialogType, params object[] additionalDialogArguments) =>
            builder.AddDialog(DialogsBot.DefaultRootDialogId, dialogType, additionalDialogArguments);

        public static DefaultDialogFactoryBuilder AddRootDialog(this DefaultDialogFactoryBuilder builder, Type dialogType) =>
            builder.AddDialog(DialogsBot.DefaultRootDialogId, dialogType, additionalDialogArguments: null);

        public static DefaultDialogFactoryBuilder AddRootDialog<TDialog>(this DefaultDialogFactoryBuilder builder)
            where TDialog : Dialog =>
            builder.AddDialog(DialogsBot.DefaultRootDialogId, typeof(TDialog), additionalDialogArguments: null);

        public static DefaultDialogFactoryBuilder AddRootDialog<TDialog>(this DefaultDialogFactoryBuilder builder, params object[] additionalDialogArguments)
            where TDialog : Dialog =>
            builder.AddDialog(DialogsBot.DefaultRootDialogId, typeof(TDialog), additionalDialogArguments);

        public static DefaultDialogFactoryBuilder AddRootDialog(this DefaultDialogFactoryBuilder builder, Func<IServiceProvider, Dialog> dialogFactory) =>
            builder.AddDialog(DialogsBot.DefaultRootDialogId, dialogFactory);

        public static DefaultDialogFactoryBuilder AddDialog<TDialog>(this DefaultDialogFactoryBuilder builder, string dialogId, params object[] additionalDialogArguments)
            where TDialog : Dialog =>
            builder.AddDialog(dialogId, typeof(TDialog), additionalDialogArguments);

        public static DefaultDialogFactoryBuilder AddDialog<TDialog>(this DefaultDialogFactoryBuilder builder, string dialogId)
            where TDialog : Dialog =>
            builder.AddDialog(dialogId, typeof(TDialog), additionalDialogArguments: null);

        public static DefaultDialogFactoryBuilder AddPrompt<TPrompt, TPromptValue>(this DefaultDialogFactoryBuilder builder, string promptId)
            where TPrompt : Prompt<TPromptValue> =>
            builder.AddDialog<TPrompt>(promptId, additionalDialogArguments: null);

        public static DefaultDialogFactoryBuilder AddPrompt<TPrompt, TPromptValue>(this DefaultDialogFactoryBuilder builder, string promptId, PromptValidator<TPromptValue> promptValidator)
            where TPrompt : Prompt<TPromptValue> =>
            builder.AddDialog<TPrompt>(promptId, additionalDialogArguments: promptValidator);

        public static DefaultDialogFactoryBuilder AddPrompt<TPromptValue>(this DefaultDialogFactoryBuilder builder, Prompt<TPromptValue> prompt) =>
            builder.AddDialog(prompt);
    }
}
