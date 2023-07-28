namespace Discoteque.Data.Models
{
    /// <summary>
    /// publico clase nombre tipoGenerico lo declara quien sea que lo declara donde TId es de un tipo struc es para generar data
    /// esto es reflexion anonia
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public class BaseEntity<TId> where TId : struct
    {
        public TId Id { get; set;}
    }
}
