package Calc.Osago;

public final class Result extends Calc {

	private double sum=0;
	
	protected Result(double indication1) {
		sum=mul(indication1,sum);
	}
	
	public double getSum() {
		return sum;
	}
	
	public String toString() {
		
		return String.valueOf(getSum())+ " руб.";
	}
	

}
