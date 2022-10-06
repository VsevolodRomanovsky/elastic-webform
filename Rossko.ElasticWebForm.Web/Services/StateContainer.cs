namespace Rossko.ElasticWebForm.Web.Services
{
    public class StateContainer
    {
        private long? counterState;

        public long Property
        {
            get => counterState ?? default(long);
            set
            {
                counterState = value;
                NotifyStateChanged();
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
