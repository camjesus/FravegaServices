namespace Domain.Core.Exceptions
{
    public class FechaInicioMayorFechaFinException : UserException
    {
        public FechaInicioMayorFechaFinException()
            : base("La fecha de inicio no puede se mayor a fecha fin")
        { }
    }
}