// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Microsoft.Bot.Builder.Dialogs
{
    public interface IDialogFactory
    {
        Dialog GetDialog(string dialogId);
    }
}
