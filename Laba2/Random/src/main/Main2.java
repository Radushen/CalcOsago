package main;
import java.util.Random;

public class Main2 {
public static void main(String[] args)
{
Random rand = new Random();

int mas[] = new int[9];

for(int i = 0; i < 9; i++)
{
mas[i] = rand.nextInt(20);
System.out.println(mas[i]);
}
}
}
