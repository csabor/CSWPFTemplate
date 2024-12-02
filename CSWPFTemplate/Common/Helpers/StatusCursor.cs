namespace CSWPFTemplate.Common.Helpers
{
    internal class StatusCursor : IDisposable
    {
        public StatusCursor(System.Windows.Input.Cursor cursor)
        {
            System.Windows.Input.Mouse.OverrideCursor = cursor;
        }

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    System.Windows.Input.Mouse.OverrideCursor = null;
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
