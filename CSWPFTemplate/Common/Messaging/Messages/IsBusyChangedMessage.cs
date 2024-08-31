using CommunityToolkit.Mvvm.Messaging.Messages;

namespace CSWPFTemplate.Common.Messaging.Messages
{
    internal class IsBusyChangedMessage(bool value) : ValueChangedMessage<bool>(value)
    {
    }
}
