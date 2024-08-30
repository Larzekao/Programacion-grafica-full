using OpenTK;

public class UncPunto
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public UncPunto(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public void Trasladar(float dx, float dy, float dz)
    {
        X += dx;
        Y += dy;
        Z += dz;
    }

    public void RotarX(float angulo, UncPunto origen)
    {
        float rad = MathHelper.DegreesToRadians(angulo);
        float cos = (float)Math.Cos(rad);
        float sin = (float)Math.Sin(rad);

        float y = Y - origen.Y;
        float z = Z - origen.Z;

        Y = origen.Y + y * cos - z * sin;
        Z = origen.Z + y * sin + z * cos;
    }

    public void RotarY(float angulo, UncPunto origen)
    {
        float rad = MathHelper.DegreesToRadians(angulo);
        float cos = (float)Math.Cos(rad);
        float sin = (float)Math.Sin(rad);

        float x = X - origen.X;
        float z = Z - origen.Z;

        X = origen.X + x * cos + z * sin;
        Z = origen.Z - x * sin + z * cos;
    }

    public void RotarZ(float angulo, UncPunto origen)
    {
        float rad = MathHelper.DegreesToRadians(angulo);
        float cos = (float)Math.Cos(rad);
        float sin = (float)Math.Sin(rad);

        float x = X - origen.X;
        float y = Y - origen.Y;

        X = origen.X + x * cos - y * sin;
        Y = origen.Y + x * sin + y * cos;
    }

    public void Escalar(float factorX, float factorY, float factorZ, UncPunto origen)
    {
        X = origen.X + (X - origen.X) * factorX;
        Y = origen.Y + (Y - origen.Y) * factorY;
        Z = origen.Z + (Z - origen.Z) * factorZ;
    }

    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }
}
