namespace Kingfisher.Provider.SingleInstance
{
    public class SingleInstanceEventArgs
    {
        public SingleInstanceEventArgs(string[] args)
        {
            Args = args;
        }

        public string[] Args { get; }
    }
}