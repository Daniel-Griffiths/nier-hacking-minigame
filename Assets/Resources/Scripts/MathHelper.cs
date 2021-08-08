public static class MathHelper
{
    public static float clampNegative(float number)
    {  
        return number < 0f ? number : 0f;
    }

    public static float clampPositive(float number)
    {  
        return number > 0f ? number : 0f;
    }    
}