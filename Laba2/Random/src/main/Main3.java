package main;

import static org.junit.Assert.fail;

import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;

class ArrayBubble{
	
    private long[] a; // Ссылка на массив
    private int elems; // Количество элементов в массиве

    public ArrayBubble(int max) // Конструктор класса
    {
        a = new long[max]; // Создание массива размером max
        elems = 0; // При создании массив содержит 0 элементов
    }

    public void into(long value) // Метод вставки элемента в массив
    {   
        a[elems] = value; //Вставка value в массив a
        elems++; // Размер массива увеличивается
    }

    public void printer() // Метод вывода массива в консоль
    {
        for (int i = 0; i < elems; i++) // Для каждого элемента в массиве
        {    
            System.out.print(a[i] + " "); // Вывести в консоль
            System.out.println(""); // С новой строки
        }
        
        System.out.println("----Окончание вывода массива----");
    }

    private void toSwap(int first, int second) //метод меняет местами пару чисел массива
    {
        long dummy = a[first]; // Во временную переменную помещаем первый элемент
        a[first] = a[second]; // На место первого ставим второй элемент
        a[second] = dummy; // Вместо второго элемента пишем первый из временной памяти
    }

    public void bubbleSorter() // Метод пузырьковой сортировки
    {     
        for (int out = elems - 1; out >= 1; out--) // Внешний цикл
        {  
            for (int in = 0; in < out; in++) // Внутренний цикл
            {       
                if(a[in] > a[in + 1]) toSwap(in, in + 1); // Если порядок элементов нарушен вызвать метод, меняющий местами
            }
        }
    }
        
    public long[] getArray() 
    {
            return a;
    }
}

public class Main3 {
    public static void main(String[] args) {
        ArrayBubble array = new ArrayBubble(5); //Создаем массив array на 5 элементов

        array.into(163); // Заполняем массив
        array.into(300);
        array.into(184);
        array.into(191);
        array.into(174);

        array.printer(); // Выводим элементы до сортировки
        array.bubbleSorter(); // Используем пузырьковую сортировку
        array.printer(); // Снова выводим отсортированный список
    }
}