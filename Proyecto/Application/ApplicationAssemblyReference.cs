using System.Reflection;

namespace Application;
/// <summary>
/// Esta clase se utiliza para obtener la referencia a la asamblea de la aplicación.
/// </summary>
public class ApplicationAssemblyReference
{
    // Esta propiedad estática se utiliza para obtener la referencia a la asamblea de la aplicación.
    internal static readonly Assembly Assembly = typeof(ApplicationAssemblyReference).Assembly;
}