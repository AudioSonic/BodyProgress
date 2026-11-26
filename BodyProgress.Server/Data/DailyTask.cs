namespace BodyProgress.Server.Data
{
    public class DailyTask
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public bool NutritionCompleted { get; set; } = false;
        public bool CardioCompleted { get; set; } = false;
        public bool StrengthTrainingCompleted { get; set; } = false;
    }
}
