namespace NotifyApp.Communication
{
    public interface ICommunicationHub
    {
        void SendMessage(ICommunicationParticipant participant, object message);
        void Subscribe<MessageType>(ICommunicationParticipant caller);
    }
}