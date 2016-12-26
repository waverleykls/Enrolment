namespace WaverleyKls.Enrolment.ViewModels
{
    public interface ICloneable<T> where T : class
    {
        T Clone(bool initialise = true);
    }
}