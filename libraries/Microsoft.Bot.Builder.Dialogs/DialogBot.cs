// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Bot.Builder.Dialogs
{
    public class DialogBot<TDialogSet> : IBot
        where TDialogSet : DialogSet
    {
        public static readonly string DefaultRootDialogId = "root";

        private readonly TDialogSet _dialogSet;
        private readonly string _rootDialogId;

        public DialogBot(IStatePropertyAccessor<DialogState> dialogStatePropertyAccessor) : this(dialogStatePropertyAccessor, DefaultRootDialogId)
        {
        }

        public DialogBot(IStatePropertyAccessor<DialogState> dialogStatePropertyAccessor, string rootDialogId)
        {
            if (dialogStatePropertyAccessor == null)
            {
                throw new ArgumentNullException(nameof(dialogStatePropertyAccessor));
            }

            if (string.IsNullOrEmpty(rootDialogId))
            {
                throw new ArgumentNullException(nameof(rootDialogId));
            }

            _dialogSet = Activator.CreateInstance(typeof(TDialogSet), dialogStatePropertyAccessor) as TDialogSet;

            _rootDialogId = rootDialogId;
        }

        public DialogBot(TDialogSet dialogSet) : this(dialogSet, DefaultRootDialogId)
        {
        }

        public DialogBot(TDialogSet dialogSet, string rootDialogId)
        {
            _dialogSet = dialogSet ?? throw new ArgumentException(nameof(dialogSet));

            if (string.IsNullOrEmpty(rootDialogId))
            {
                throw new ArgumentNullException(nameof(rootDialogId));
            }

            _rootDialogId = rootDialogId;
        }

        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            var dialogContext = await _dialogSet.CreateContextAsync(turnContext, cancellationToken).ConfigureAwait(false);
            var dialogTurnResult = default(DialogTurnResult);

            if (dialogContext.ActiveDialog != null)
            {
                dialogTurnResult = await dialogContext.ContinueDialogAsync(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                dialogTurnResult = await dialogContext.BeginDialogAsync(_rootDialogId, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
