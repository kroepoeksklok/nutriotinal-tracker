namespace NutritionalTracker.Commands
{
    public interface ICommandProcessor
    {
        void Process(ICommand command);
    }
}