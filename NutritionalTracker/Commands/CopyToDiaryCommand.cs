using System;

namespace NutritionalTracker.Commands
{
    public sealed class CopyToDiaryCommand : ICommand
    {
        public byte MealId { get; set; }
        public DateTime DateToCopy { get; set; }
    }
}