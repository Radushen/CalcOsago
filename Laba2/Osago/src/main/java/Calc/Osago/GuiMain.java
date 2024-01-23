package Calc.Osago;

import java.awt.event.ActionListener;

import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JTextField;

public class GuiMain {
	private JComboBox<String> combo_tarif;
	private JLabel labelTarif1 = new JLabel("Введите коэфицент мощности:");
	private JLabel labelTarif2d = new JLabel("Введите коэфицент мощности:");
	private JLabel labelTarif2n = new JLabel("Введите коэфицент возраста и стажа:");
	private JLabel labelTarif2p = new JLabel("Введите коэфицент бонус-малус:");
	private JLabel TextPower = new JLabel("");
	private JLabel TextPowerInfo1 = new JLabel("");
	private JLabel TextPowerInfo2 = new JLabel("");
	private JLabel TextPowerInfo3 = new JLabel("");
	private JLabel TextPowerInfo4 = new JLabel("");
	private JLabel TextPowerInfo5 = new JLabel("");
	private JLabel TextPowerInfo6 = new JLabel("");
	private JLabel TextInfo = new JLabel("");
	private JLabel TextDevelopers = new JLabel("");
	private JLabel TextTB = new JLabel("");
	private JLabel TextOutput = new JLabel("");
	private JTextField ElectroData = new JTextField(10);
	private JTextField ElectroData1 = new JTextField(10);
	private JTextField ElectroData2 = new JTextField(10);
	private JTextField ElectroData3 = new JTextField(10);
	private JTextField KM = new JTextField(10);
	private String[] transfer = {"Неограниченный", "Ограниченный"};
	private JLabel[] arrayLabel= {labelTarif1,labelTarif2d,labelTarif2n,labelTarif2p};
	private JTextField[] arrayTextField = {ElectroData,ElectroData1,ElectroData2,ElectroData3};
	private JFrame main_GUI;
	private JPanel main_panel;
	public GuiMain(String name, String label) {
	
	main_GUI = new JFrame("Electro");
	main_GUI.setTitle (name);
	main_GUI.setBounds(700,600,900,500);
	main_GUI.setResizable(false); 
	
	main_panel = new JPanel();
	main_panel.setLayout(null);
	main_GUI.add(main_panel);
		
	JLabel laba_info = new JLabel(label);
	laba_info.setBounds(60,0,300,30);
	main_panel.add(laba_info);
	
	TextTB = new JLabel("Вид страхового полиса");
	TextTB.setBounds(30,20,150,30);
	TextTB.setEnabled(true);
	main_panel.add(TextTB);
	
	combo_tarif = new JComboBox<String>(transfer);
	combo_tarif.setBounds(30,45,150,30);
	ActionListener tarif_set = new TarifSet();
	combo_tarif.addActionListener(tarif_set);
	main_panel.add(combo_tarif);
	
	TextPower = new JLabel("Коэффициенты мощности");
	TextPower.setBounds(300,20,300,30);
	TextPower.setEnabled(true);
	main_panel.add(TextPower);
	
	TextPowerInfo1 = new JLabel("До 50 включительно = 0,6");
	TextPowerInfo1.setBounds(300,40,300,30);
	TextPowerInfo1.setEnabled(true);
	main_panel.add(TextPowerInfo1);
	
	TextPowerInfo2 = new JLabel("Свыше 50 до 70 включительно = 1");
	TextPowerInfo2.setBounds(300,60,300,30);
	TextPowerInfo2.setEnabled(true);
	main_panel.add(TextPowerInfo2);
	
	TextPowerInfo3 = new JLabel("Свыше 70 до 100 включительно = 1,1");
	TextPowerInfo3.setBounds(300,80,300,30);
	TextPowerInfo3.setEnabled(true);
	main_panel.add(TextPowerInfo3);
	
	TextPowerInfo4 = new JLabel("Свыше 100 до 120 включительно = 1,2");
	TextPowerInfo4.setBounds(300,100,300,30);
	TextPowerInfo4.setEnabled(true);
	main_panel.add(TextPowerInfo4);
	
	TextPowerInfo5 = new JLabel("Свыше 120 до 150 включительно = 1,4");
	TextPowerInfo5.setBounds(300,120,300,30);
	TextPowerInfo5.setEnabled(true);
	main_panel.add(TextPowerInfo5);
	
	TextPowerInfo6 = new JLabel("Свыше 150 = 1,6");
	TextPowerInfo6.setBounds(300,140,300,30);
	TextPowerInfo6.setEnabled(true);
	main_panel.add(TextPowerInfo6);
	
	TextInfo = new JLabel("Для определения коэффициентов КВС и КБМ воспользуйтесь текстовым файлов");
	TextInfo.setBounds(300,160,700,30);
	TextInfo.setEnabled(true);
	main_panel.add(TextInfo);
	
	TextDevelopers = new JLabel("Разработали Гумеров Р.М. Викулов А.Е. Хамадалина И.И.");
	TextDevelopers.setBounds(300,200,500,30);
	TextDevelopers.setEnabled(true);
	main_panel.add(TextDevelopers);
	
	JLabel labelOutput = new JLabel("Стоимость:");
	labelOutput.setBounds(30,300,250,30);
	main_panel.add(labelOutput);;
	
	TextOutput = new JLabel("");
	TextOutput.setBounds(30,330,250,30);
	TextOutput.setEnabled(false);
	main_panel.add(TextOutput);
	
	JButton button_create = new JButton("Произвести расчет");
	button_create.setBounds(20,400,150,40);
	ActionListener actionCreate = new ListenerCalc();
	button_create.addActionListener(actionCreate);
	main_panel.add(button_create);
	
	JButton button_exit = new JButton("Выход");
	button_exit.setBounds(770,400,100,40);
	ActionListener actionListener = new ListenerButton();
	button_exit.addActionListener(actionListener); 
	main_panel.add(button_exit);
	
	main_GUI.setVisible(true);
	main_GUI.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
	
	}
	
	public JPanel getPanel() {
		return main_panel;
	}
	
	public JLabel[] getLabel() {
		return arrayLabel;
	}
	public JTextField[] getTextField() {
		return arrayTextField;
	}
	
	public String[] getTransfer() {
		return transfer;
	}
	
	public JLabel getLabelOutput() {
		return TextOutput;
	}
	
	public JComboBox<String> getComboTarif() {
		return combo_tarif;
	}

}
