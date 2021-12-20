namespace ShipAbstractComponents.ports
{
    public interface IOutputPortListener
    {
        void OnSend();
        void OnConnection(OutputPort from,InputPort to);
    }
}