package Calc.Osago;

public class Main {
	
	protected static  GuiMain gui;
	public static void main(String[] args) {
		String name = "Страховой калькулятор. ОСАГО";
		String label = "Заполните данные для рассчета";
		gui = new GuiMain(name,label);
		
	}

}
