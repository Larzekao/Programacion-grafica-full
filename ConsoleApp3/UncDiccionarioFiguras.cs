using System.Collections.Generic;
using OpenTK.Graphics;

namespace ConsoleApp3
{
    public class UncDiccionarioFiguras
    {
        private FiguraManager figuraManager;

        public UncDiccionarioFiguras(FiguraManager figuraManager)
        {
            this.figuraManager = figuraManager;
        }

        // Método para crear un rectángulo 3D
        public UncFiguras3D CrearRectangulo3D(
            string id,
            float ancho,
            float alto,
            float profundidad,
            Color4 color,
            UncPunto origen)
        {
            var rectangulo = new UncFiguras3DImplRectangulo(ancho, alto, profundidad, color, origen);
            rectangulo.CrearFigura();
            figuraManager.AgregarFigura(id, rectangulo);
            return rectangulo;  // Devuelve la figura creada
        }

        // Método para crear una pirámide 3D
        public UncFiguras3D CrearPiramide3D(
            string id,
            float ancho,
            float profundidad,
            float altura,
            Color4 color,
            UncPunto origen)
        {
            var piramide = new UncFiguras3DImplPiramide(ancho, profundidad, altura, color, origen);
            piramide.CrearFigura();
            figuraManager.AgregarFigura(id, piramide);
            return piramide;  // Devuelve la figura creada
        }

        // Implementación de la figura 3D para el rectángulo
        private class UncFiguras3DImplRectangulo : UncFiguras3D
        {
            private float ancho;
            private float alto;
            private float profundidad;
            private Color4 color;
            private UncPunto origen;

            public UncFiguras3DImplRectangulo(float ancho, float alto, float profundidad, Color4 color, UncPunto origen)
            {
                this.ancho = ancho;
                this.alto = alto;
                this.profundidad = profundidad;
                this.color = color;
                this.origen = origen;
            }

            public override void CrearFigura()
            {
                float cx = ancho / 2.0f;
                float cy = alto / 2.0f;
                float cz = profundidad / 2.0f;

                // Crear las 6 caras del rectángulo, centrado en el origen
                var carasRectangulo = new List<UncPoligono>
                {
                    new UncPoligono(new List<UncPunto>
                    {
                        new UncPunto(-cx + origen.X, -cy + origen.Y, -cz + origen.Z),
                        new UncPunto(cx + origen.X, -cy + origen.Y, -cz + origen.Z),
                        new UncPunto(cx + origen.X, cy + origen.Y, -cz + origen.Z),
                        new UncPunto(-cx + origen.X, cy + origen.Y, -cz + origen.Z)
                    }, color, origen),
                    new UncPoligono(new List<UncPunto>
                    {
                        new UncPunto(-cx + origen.X, -cy + origen.Y, cz + origen.Z),
                        new UncPunto(cx + origen.X, -cy + origen.Y, cz + origen.Z),
                        new UncPunto(cx + origen.X, cy + origen.Y, cz + origen.Z),
                        new UncPunto(-cx + origen.X, cy + origen.Y, cz + origen.Z)
                    }, color, origen),
                    new UncPoligono(new List<UncPunto>
                    {
                        new UncPunto(-cx + origen.X, -cy + origen.Y, -cz + origen.Z),
                        new UncPunto(-cx + origen.X, -cy + origen.Y, cz + origen.Z),
                        new UncPunto(-cx + origen.X, cy + origen.Y, cz + origen.Z),
                        new UncPunto(-cx + origen.X, cy + origen.Y, -cz + origen.Z)
                    }, color, origen),
                    new UncPoligono(new List<UncPunto>
                    {
                        new UncPunto(cx + origen.X, -cy + origen.Y, -cz + origen.Z),
                        new UncPunto(cx + origen.X, -cy + origen.Y, cz + origen.Z),
                        new UncPunto(cx + origen.X, cy + origen.Y, cz + origen.Z),
                        new UncPunto(cx + origen.X, cy + origen.Y, -cz + origen.Z)
                    }, color, origen),
                    new UncPoligono(new List<UncPunto>
                    {
                        new UncPunto(-cx + origen.X, cy + origen.Y, -cz + origen.Z),
                        new UncPunto(cx + origen.X, cy + origen.Y, -cz + origen.Z),
                        new UncPunto(cx + origen.X, cy + origen.Y, cz + origen.Z),
                        new UncPunto(-cx + origen.X, cy + origen.Y, cz + origen.Z)
                    }, color, origen),
                    new UncPoligono(new List<UncPunto>
                    {
                        new UncPunto(-cx + origen.X, -cy + origen.Y, -cz + origen.Z),
                        new UncPunto(cx + origen.X, -cy + origen.Y, -cz + origen.Z),
                        new UncPunto(cx + origen.X, -cy + origen.Y, cz + origen.Z),
                        new UncPunto(-cx + origen.X, -cy + origen.Y, cz + origen.Z)
                    }, color, origen)
                };

                caras.AddRange(carasRectangulo);
            }
        }

        // Implementación de la figura 3D para la pirámide
        private class UncFiguras3DImplPiramide : UncFiguras3D
        {
            private float ancho;
            private float profundidad;
            private float altura;
            private Color4 color;
            private UncPunto origen;

            public UncFiguras3DImplPiramide(float ancho, float profundidad, float altura, Color4 color, UncPunto origen)
            {
                this.ancho = ancho;
                this.profundidad = profundidad;
                this.altura = altura;
                this.color = color;
                this.origen = origen;
            }

            public override void CrearFigura()
            {
                float cx = ancho / 2.0f;
                float cz = profundidad / 2.0f;
                float cy = altura / 3.0f; // La mitad de la altura desde la base hasta el centro de masa

                // Vértices de la base (centrada en el plano XZ)
                UncPunto v0 = new UncPunto(-cx + origen.X, -cy + origen.Y, -cz + origen.Z);
                UncPunto v1 = new UncPunto(cx + origen.X, -cy + origen.Y, -cz + origen.Z);
                UncPunto v2 = new UncPunto(cx + origen.X, -cy + origen.Y, cz + origen.Z);
                UncPunto v3 = new UncPunto(-cx + origen.X, -cy + origen.Y, cz + origen.Z);

                // Vértice superior (centrado sobre el origen)
                UncPunto v4 = new UncPunto(origen.X, 2 * cy + origen.Y, origen.Z);

                // Crear las 4 caras de la pirámide
                var carasPiramide = new List<UncPoligono>
                {
                    // Base
                    new UncPoligono(new List<UncPunto> { v0, v1, v2, v3 }, color, origen),

                    // Caras laterales
                    new UncPoligono(new List<UncPunto> { v0, v1, v4 }, color, origen),
                    new UncPoligono(new List<UncPunto> { v1, v2, v4 }, color, origen),
                    new UncPoligono(new List<UncPunto> { v2, v3, v4 }, color, origen),
                    new UncPoligono(new List<UncPunto> { v3, v0, v4 }, color, origen)
                };

                caras.AddRange(carasPiramide);
            }
        }
    }
}
