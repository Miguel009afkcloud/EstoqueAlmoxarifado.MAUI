public class BaseViewModel : InotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChaged([CallerMemberName] string? nome = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));

    private bool _isBusy;
    private bool IsBusy
    {
        get => _isBusy;
        set { _isBusy = value; OnPropertyChaged(); OnPropertyChaged(nameof(NaoEstaOcupado)); }
    }
    public bool NaoEstaOcupado => !IsBusy; !
}