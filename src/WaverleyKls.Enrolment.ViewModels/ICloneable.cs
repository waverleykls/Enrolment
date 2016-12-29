namespace WaverleyKls.Enrolment.ViewModels
{
    /// <summary>
    /// This provides interfaces to a class that can clone its current instance.
    /// </summary>
    /// <typeparam name="T">Type of class.</typeparam>
    public interface ICloneable<T> where T : class
    {
        /// <summary>
        /// Clones the current instance.
        /// </summary>
        /// <param name="initialise">Value that indicates whether to initialise the cloned object or not.</param>
        /// <returns>Returns the instance cloned.</returns>
        T Clone(bool initialise = true);
    }
}