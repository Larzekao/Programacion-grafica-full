using ConsoleApp3;
using System.Collections.Generic;

public class FiguraManager
{
    private Dictionary<string, UncFiguras3D> figuras;
    private Dictionary<string, Escenario> escenarios;

    public FiguraManager()
    {
        figuras = new Dictionary<string, UncFiguras3D>();
        escenarios = new Dictionary<string, Escenario>();
    }

    public void AgregarFigura(string id, UncFiguras3D figura)
    {
        figuras[id] = figura;
    }

    public UncFiguras3D ObtenerFigura(string id)
    {
        return figuras.TryGetValue(id, out var figura) ? figura : null;
    }

    public void AgregarEscenario(string id, Escenario escenario)
    {
        escenarios[id] = escenario;
    }

    public Escenario ObtenerEscenario(string id)
    {
        return escenarios.TryGetValue(id, out var escenario) ? escenario : null;
    }

    public void DibujarEscenarios()
    {
        foreach (var escenario in escenarios.Values)
        {
            escenario.Dibujar();
        }
    }
}
