package main;

import static org.junit.Assert.assertArrayEquals;
import org.junit.Test;

public class Main3Test {
    @Test
    public void testBubbleSorter()
    {
        ArrayBubble array = new ArrayBubble(5);
        array.into(163);
        array.into(300);
        array.into(184);
        array.into(191);
        array.into(174);

        long[] expected = {163, 174, 184, 191, 300};
        array.bubbleSorter();
        assertArrayEquals(expected, array.getArray());
    }
}