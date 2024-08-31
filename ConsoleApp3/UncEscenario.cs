using System.Collections.Generic;
using OpenTK.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ConsoleApp3
{
    public class Escenario
    {
        private List<UncFiguras3D> figuras;
        private PlanoCartesiano plano; // Añadido para el plano cartesiano
        public Color4 FondoColor { get; set; }

        public Escenario(Color4 fondoColor, PlanoCartesiano planoCartesiano)
        {
            figuras = new List<UncFiguras3D>();
            FondoColor = fondoColor;
            plano = planoCartesiano; // Inicializa el plano cartesiano
        }

        public void AgregarFigura(UncFiguras3D figura)
        {
            figuras.Add(figura);
        }

        public void Dibujar()
        {
            // Configura el fondo del escenario
            GL.ClearColor(FondoColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Dibuja el plano cartesiano
            plano.Dibujar();

            // Dibuja todas las figuras en el escenario
            foreach (var figura in figuras)
            {
                figura.Dibujar(); // Asume que la clase UncFiguras3D tiene un método Dibujar
            }
        }
    }
}
