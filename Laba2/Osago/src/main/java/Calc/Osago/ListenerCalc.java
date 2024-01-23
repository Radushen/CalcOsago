package Calc.Osago;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JComboBox;
import javax.swing.JLabel;
import javax.swing.JTextField;

public class ListenerCalc implements ActionListener {
	
	@Override
	public void actionPerformed(ActionEvent e) {
		
		JComboBox<String> combo = Main.gui.getComboTarif();
		String item = (String)combo.getSelectedItem();
		String[] transfer = Main.gui.getTransfer();
		JTextField[] arrayTextField = Main.gui.getTextField();
		JLabel Output = Main.gui.getLabelOutput();

		
		if (item==transfer[0]) {
			if (!"double x =0.611.11.21.41.6.\b".contains(arrayTextField[0].getText())||arrayTextField[0].getText().isEmpty()) {arrayTextField[0].setText("0");}
			Result tarif1 = new Result(Double.parseDouble(arrayTextField[0].getText()));
			Output.setText(tarif1.toString());
			
		}
		
		if (item==transfer[1]) {
			if (!"0.611.11.21.41.6.\b".contains(arrayTextField[1].getText())||arrayTextField[1].getText().isEmpty()) {arrayTextField[1].setText("0");}
			if (!"1.431.361.350.910.900.890.880.831.461.461.401.390.930.920.910.900.861.501.441.430.960.950.940.930.911.541.471.461.000.970.950.940.91.561.501.481.051.041.010.970.951.721.601.541.091.081.071.021,881.721.711.131.101.092.271.921.841.651.62.\b".contains(arrayTextField[2].getText())||arrayTextField[2].getText().isEmpty()) {arrayTextField[2].setText("0");}
			if (!"3.922.942.251.761.1710.910.830.780.740.680.630.570.520.46.\b".contains(arrayTextField[3].getText())||arrayTextField[3].getText().isEmpty()) {arrayTextField[3].setText("0");}
			TwoTarif tarif2 = new TwoTarif(Double.parseDouble(arrayTextField[1].getText()),Double.parseDouble(arrayTextField[2].getText()),Double.parseDouble(arrayTextField[3].getText()));
			tarif2.getMultiAll();
			Output.setText(tarif2.toString());
			
			
		}
		
		
		
		
	}

}
