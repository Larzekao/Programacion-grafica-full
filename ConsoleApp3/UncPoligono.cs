using System.Collections.Generic;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace ConsoleApp3
{
    public class UncPoligono
    {
        public List<UncPunto> Vertices { get; private set; }
        private Color4 color;
        private UncPunto origen;

        public UncPoligono(List<UncPunto> vertices, Color4 color = default, UncPunto origen = null)
        {
            this.Vertices = vertices;
            this.color = color == default ? Color4.White : color; // Por defecto blanco
            this.origen = origen ?? new UncPunto(0, 0, 0); // Por defecto en el origen
        }

        public void Dibujar()
        {
            GL.Begin(PrimitiveType.Polygon);
            GL.Color4(color);

            foreach (var vertice in Vertices)
            {
                GL.Vertex3(vertice.X, vertice.Y, vertice.Z);
            }

            GL.End();
        }

        public void Trasladar(float dx, float dy, float dz)
        {
            foreach (var vertice in Vertices)
            {
                vertice.Trasladar(dx, dy, dz);
            }
        }

        public void RotarX(float angulo, UncPunto origen)
        {
            foreach (var vertice in Vertices)
            {
                vertice.RotarX(angulo, origen);
            }
        }

        public void RotarY(float angulo, UncPunto origen)
        {
            foreach (var vertice in Vertices)
            {
                vertice.RotarY(angulo, origen);
            }
        }

        public void RotarZ(float angulo, UncPunto origen)
        {
            foreach (var vertice in Vertices)
            {
                vertice.RotarZ(angulo, origen);
            }
        }

        public void Escalar(float factorX, float factorY, float factorZ, UncPunto origen)
        {
            foreach (var vertice in Vertices)
            {
                vertice.Escalar(factorX, factorY, factorZ, origen);
            }
        }

        public override string ToString()
        {
            string resultado = "Polígono con vértices en: ";
            foreach (var vertice in Vertices)
            {
                resultado += vertice.ToString() + " ";
            }
            return resultado;
        }
    }
}
