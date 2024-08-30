using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using OpenTK.Input;
using OpenTK.Graphics;

namespace ConsoleApp3
{
    public class UnCGraficas : GameWindow
    {
        private float angulox;
        private float anguloy;
        private float Rotar = 1.0f;
        private PlanoCartesiano plano;
        private FiguraManager figuraManager;
        private UncDiccionarioFiguras diccionarioFiguras;
        private string escenarioActual = "escenario1"; // Escenario predeterminado

        public UnCGraficas()
            : base(DisplayDevice.Default.Width, DisplayDevice.Default.Height, GraphicsMode.Default, "Mi Ventana a Pantalla Completa", GameWindowFlags.Fullscreen)
        {
            figuraManager = new FiguraManager();
            diccionarioFiguras = new UncDiccionarioFiguras(figuraManager);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color4.Black);
            GL.Enable(EnableCap.DepthTest);
            plano = new PlanoCartesiano(0.1f, 10);

            // Crear y agregar escenarios
            var escenario1 = new Escenario(Color4.White);
            var escenario2 = new Escenario(Color4.Black);

            figuraManager.AgregarEscenario("escenario1", escenario1);
            figuraManager.AgregarEscenario("escenario2", escenario2);

            // Crear figuras para el escenario 1
            var figuraRectangulo = diccionarioFiguras.CrearRectangulo3D(
                "rectangulo1",
                0.2f,
                0.6f,
                0.2f,
                Color4.Red,
                new UncPunto(0, 0, 0)
            );
            figuraManager.AgregarFigura("rectangulo1", figuraRectangulo);
            escenario1.AgregarFigura(figuraRectangulo);

            // Crear figuras para el escenario 2
            var figuraPiramide = diccionarioFiguras.CrearPiramide3D(
                "piramide1",
                0.5f,
                0.5f,
                1.0f,
                Color4.Blue,
                new UncPunto(0.5f, 0, 0)
            );
            figuraManager.AgregarFigura("piramide1", figuraPiramide);
            escenario2.AgregarFigura(figuraPiramide);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Configuración de la matriz de proyección
            Matrix4 projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), (float)Size.Width / Size.Height, 0.1f, 100.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projectionMatrix);

            // Configuración de la matriz de vista
            Matrix4 viewMatrix = Matrix4.CreateTranslation(0.0f, 0.0f, -5.0f);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref viewMatrix);

            // Aplicar las mismas rotaciones al plano y a las figuras
            GL.PushMatrix();

            // Crear matriz de rotación acumulada para X y Y
            Matrix4 rotationMatrix = Matrix4.CreateRotationX(this.angulox) * Matrix4.CreateRotationY(this.anguloy);
            GL.MultMatrix(ref rotationMatrix);

            plano.Dibujar();

            // Dibujar el escenario actual
            var escenario = figuraManager.ObtenerEscenario(escenarioActual);
            escenario?.Dibujar();

            GL.PopMatrix();

            Context.SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            KeyboardState input = Keyboard.GetState();

            // Salir de la aplicación si se presiona la tecla Escape
            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }

            // Cambiar escenario según la tecla presionada
            if (input.IsKeyDown(Key.W)) // Tecla 1 para el escenario 1
            {
                escenarioActual = "escenario1";
            }
            if (input.IsKeyDown(Key.S)) // Tecla 2 para el escenario 2
            {
                escenarioActual = "escenario2";
            }

            // Ajustar la rotación en el eje X
            if (input.IsKeyDown(Key.Up))
            {
                this.angulox += this.Rotar * (float)e.Time;
            }
            if (input.IsKeyDown(Key.Down))
            {
                this.angulox -= this.Rotar * (float)e.Time;
            }

            // Ajustar la rotación en el eje Y
            if (input.IsKeyDown(Key.Left))
            {
                this.anguloy -= this.Rotar * (float)e.Time;
            }
            if (input.IsKeyDown(Key.Right))
            {
                this.anguloy += this.Rotar * (float)e.Time;
            }
        }
    }
}
