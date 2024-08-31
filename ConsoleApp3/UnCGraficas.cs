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
        private FiguraManager figuraManager;
        private UncDiccionarioFiguras diccionarioFiguras;
        private string escenarioActual = "escenario1";
        private PlanoCartesiano plano;

        private Matrix4 projectionMatrix;
        private Matrix4 viewMatrix;

        public UnCGraficas()
            : base(1920, 1080, GraphicsMode.Default, "Mi Ventana", GameWindowFlags.Fullscreen)
        {
            VSync = VSyncMode.On;
            figuraManager = new FiguraManager();
            diccionarioFiguras = new UncDiccionarioFiguras(figuraManager);
            plano = new PlanoCartesiano(0.1f, 10);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.Enable(EnableCap.DepthTest);

            // Configuración inicial de matrices
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(45.0f), (float)Width / Height, 0.1f, 100.0f);
            viewMatrix = Matrix4.CreateTranslation(0.0f, 0.0f, -5.0f);

            var escenario1 = new Escenario(Color4.White, plano);
            var escenario2 = new Escenario(Color4.Black, plano);

            figuraManager.AgregarEscenario("escenario1", escenario1);
            figuraManager.AgregarEscenario("escenario2", escenario2);

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

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projectionMatrix);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref viewMatrix);

            GL.PushMatrix();
            if (angulox != 0 || anguloy != 0)
            {
                Matrix4 rotationMatrix = Matrix4.CreateRotationX(angulox) * Matrix4.CreateRotationY(anguloy);
                GL.MultMatrix(ref rotationMatrix);
            }

            var escenario = figuraManager.ObtenerEscenario(escenarioActual);
            escenario?.Dibujar();

            GL.PopMatrix();
            Context.SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(45.0f), (float)Width / Height, 0.1f, 100.0f);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }

            // Cambio de escenario
            if (input.IsKeyDown(Key.W) && escenarioActual != "escenario1")
            {
                escenarioActual = "escenario1";
            }
            if (input.IsKeyDown(Key.S) && escenarioActual != "escenario2")
            {
                escenarioActual = "escenario2";
            }

            // Actualización de ángulos
            float deltaAngulo = Rotar * (float)e.Time;
            if (input.IsKeyDown(Key.Up))
            {
                angulox += deltaAngulo;
            }
            if (input.IsKeyDown(Key.Down))
            {
                angulox -= deltaAngulo;
            }
            if (input.IsKeyDown(Key.Left))
            {
                anguloy -= deltaAngulo;
            }
            if (input.IsKeyDown(Key.Right))
            {
                anguloy += deltaAngulo;
            }
        }
    }
}
