package Calc.Osago;

import javax.swing.JTextField;

public final class TwoTarif extends Calc {

	private double indication1, indication2, indication3,sum=0,baseTarif=7400;
	
	protected TwoTarif(double indication1,double indication2,double indication3) {
		this.indication1=indication1;
		this.indication2=indication2;
		this.indication3=indication3;
	}
	
	@Override
	public void getMultiAll() {
		JTextField[] arrayTextField = Main.gui.getTextField();
		setBaseTarif(Double.parseDouble(arrayTextField[1].getText()));
		setBaseTarif(Double.parseDouble(arrayTextField[2].getText()));
		setBaseTarif(Double.parseDouble(arrayTextField[3].getText()));
		sum=baseTarif*indication1*indication2*indication3*1;
	}
	
	public double getSum() {
		return sum;
	}
	
	public String toString() {
		
		return String.valueOf(getSum())+ " руб.";
	}
	
}