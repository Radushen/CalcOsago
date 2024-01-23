package Calc.Osago;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class ListenerButton implements ActionListener {

	@Override
	public void actionPerformed(ActionEvent e) {
		String exitHat="Выход";
		String exitLow="Страховой калькулятор. ОСАГО";
		new Exit(exitHat, exitLow);

	}

}
