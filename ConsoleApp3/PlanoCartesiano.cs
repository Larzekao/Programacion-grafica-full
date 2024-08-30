using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

public class PlanoCartesiano
{
    private float escala; // Escala del plano
    private int cantidadDivisiones; // Cantidad de divisiones en los ejes

    public float Escala
    {
        get { return escala; }
        set { escala = value; }
    }

   
    public PlanoCartesiano(float escalaInicial = 1.0f, int divisiones = 10)
    {
        this.escala = escalaInicial; 
        this.cantidadDivisiones = divisiones; 
    }

  
    public void CambiarEscala(float nuevaEscala)
    {
        this.escala = nuevaEscala;
    }

    
    public void Dibujar()
    {
        GL.LineWidth(3.0f); // Grosor de las líneas principales

        // Dibujar el eje X en rojo
        GL.Color3(1.0f, 0.0f, 0.0f);
        DibujarLinea(-cantidadDivisiones * escala, 0, 0, cantidadDivisiones * escala, 0, 0);

        // Dibujar el eje Y en verde
        GL.Color3(0.0f, 1.0f, 0.0f);
        DibujarLinea(0, -cantidadDivisiones * escala, 0, 0, cantidadDivisiones * escala, 0);

        // Dibujar el eje Z en azul
        GL.Color3(0.0f, 0.0f, 1.0f);
        DibujarLinea(0, 0, -cantidadDivisiones * escala, 0, 0, cantidadDivisiones * escala);

        GL.LineWidth(1.0f); // Restablecer el grosor de línea para las divisiones

        // Dibujar divisiones en los planos XY, XZ y YZ
        GL.Color3(0.5f, 0.5f, 0.5f); // Color gris para las divisiones

        this.DibujarDivXY();
        this.DibujarDivXZ();
        this.DibujarDivYZ();
               
    }
    public void DibujarDivXY() {

        // Dibujar divisiones en el plano XY
        for (int i = -cantidadDivisiones; i <= cantidadDivisiones; i++)
        {
            float valor = i * escala;
            DibujarLinea(valor, -cantidadDivisiones * escala, 0, valor, cantidadDivisiones * escala, 0); // Líneas paralelas al eje Y
            DibujarLinea(-cantidadDivisiones * escala, valor, 0, cantidadDivisiones * escala, valor, 0); // Líneas paralelas al eje X
        }

    }
    public void DibujarDivXZ() {
        // Dibujar divisiones en el plano XZ
        for (int i = -cantidadDivisiones; i <= cantidadDivisiones; i++)
        {
            float valor = i * escala;
            DibujarLinea(valor, 0, -cantidadDivisiones * escala, valor, 0, cantidadDivisiones * escala); // Líneas paralelas al eje Z
            DibujarLinea(-cantidadDivisiones * escala, 0, valor, cantidadDivisiones * escala, 0, valor); // Líneas paralelas al eje X
        }
    }
    public void DibujarDivYZ() {
        // Dibujar divisiones en el plano YZ
        for (int i = -cantidadDivisiones; i <= cantidadDivisiones; i++)
        {
            float valor = i * escala;
            DibujarLinea(0, valor, -cantidadDivisiones * escala, 0, valor, cantidadDivisiones * escala); // Líneas paralelas al eje Z
            DibujarLinea(0, -cantidadDivisiones * escala, valor, 0, cantidadDivisiones * escala, valor); // Líneas paralelas al eje Y
        }
    }
  
    // Método para dibujar una línea desde un punto a otro
    private void DibujarLinea(float x1, float y1, float z1, float x2, float y2, float z2)
    {
        GL.Begin(PrimitiveType.Lines); // Comienza la definición de una línea
        GL.Vertex3(x1, y1, z1); // Primer punto de la línea
        GL.Vertex3(x2, y2, z2); // Segundo punto de la línea
        GL.End(); // Finaliza la definición de la línea
    }
}
