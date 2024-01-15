package main;

import org.junit.Test;
import static org.junit.Assert.*;

public class Main1Test {

    @Test
    public void testRndInRange() {
        int min = 20;
        int max = 90;

        // Repeat the test multiple times for randomness
        for (int i = 0; i < 1000; i++) 
        {
            int rnd = Main1.rnd(min, max);

            assertTrue("Полученное значение " + rnd + " выходит за минимальный диапазон.", rnd >= min);
            assertTrue("Полученное значение " + rnd + " выходит за максимальный диапазон.", rnd <= max);
            
            if (i == 999) 
            {
                System.out.println("Тест прошёл успешно!");
            }
        }
    }
}