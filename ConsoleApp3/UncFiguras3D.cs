using ConsoleApp3;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

public abstract class UncFiguras3D
{
    protected List<UncPoligono> caras;

    public UncFiguras3D()
    {
        caras = new List<UncPoligono>();
    }

    public abstract void CrearFigura();

    public void Dibujar()
    {
        foreach (var cara in caras)
        {
            cara.Dibujar();
        }
    }
}
