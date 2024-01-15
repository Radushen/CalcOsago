package main;

public class Main1 
{
    public static void main(String... args)
    {
        final int min = 20; // Минимальное число для диапазона
        final int max = 90; // Максимальное число для диапазона
        final int rnd = rnd(min, max);

        System.out.println("Псевдослучайное целое число: " + rnd);
    }

    public static int rnd(int min, int max) // Метод получения псевдослучайного целого числа от min до max (включая max)
    {
        max -= min;
        return (int) (Math.random() * ++max) + min;
    }
}