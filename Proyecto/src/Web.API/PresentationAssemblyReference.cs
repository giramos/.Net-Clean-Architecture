using System.Reflection;

namespace Web.API;
/// <summary>
/// Esta clase se utiliza para obtener la referencia a la asamblea de presentación.
/// </summary>
public class PresentationAssemblyReference
{
    // Esta propiedad estática se utiliza para obtener la referencia a la asamblea de presentación.
    internal static readonly Assembly Assembly = typeof(PresentationAssemblyReference).Assembly;

}