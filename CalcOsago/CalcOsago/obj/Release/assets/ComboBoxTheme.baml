   M S B A M L   `   `   `  ���� R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ComboBoxNormalBorderBrush  �� Color ! ComboBoxNormalBackgroundBrush # ComboBoxDisabledForegroundBrush # ComboBoxDisabledBackgroundBrush  ComboBoxDisabledBorderBrush  ComboBoxIsMouseOver  ComboBoxTextColor  ComboBoxDropDownBackground �� 
TargetType   ComboBoxToggleButtonTemplate � ColumnDefinitions �� CornerRadius �� Triggers P� Property P� Value �� 
TargetName �� Value	 �� Property
 �� 
TargetType � ToolTip �� ElementName �� Path J� Effect3   -System.Windows.Media.Effects.DropShadowEffect    
BlurRadius    	Direction    RenderingBias    ShadowDepth    Color �� 	ClickMode d� 	IsChecked �� RelativeSource �� Mode J� IsHitTestVisible �� SelectionBoxItem �� SelectionBoxItemTemplate �� 
IsReadOnly �� 
IsReadOnly � AllowsTransparency �� IsDropDownOpen J� SnapsToDevicePixels  � ActualWidth! �� MaxDropDownHeight-  'System.Windows.Input.KeyboardNavigation"   DirectionalNavigation�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      %
  &	        &	      &	 &     &	 9     &	 N     &	 ^     &	 q     &	 �     &	 �     7   7  7  7  7  7  7  7  7  '�� R    7  7  7  7   7  7  �� $  #5F897D���� $  #AADBCD���� $  #5F897D���� $  	#FF95bfb3���� $
  #888���� $  #95bfb3���� $  #4C6E64���� $  #99DBC8���� " d�.R��  �� �� $��20 �.+��� $��Border��$��2��8��  $ 5��$��
1, 1, 1, 1q�8�� �� $��ButtonBorder��$��1��$��
1, 1, 1, 1q����#444$ 5��$��
0, 0, 0, 0q�8�� F� $��Arrow��$��1��=����        �    �� � 	= ��[ � z �� � z     � 	=  	= 	$��Center=����#5F897D$��Center=� P�  �@J�IsMouseOver . . ��� $ ButtonBorder��8  	 � ��P�  �@d�	IsChecked$
 TrueY�. ��� $ ButtonBorder��8  	 � ���� $ Arrow��	 � �� �	#FF8D979EP�  � {� .  . ��� $ Border��8  	 � ���� $ ButtonBorder��8  	 � ���� $ ButtonBorder��8  	 � ���� 8  	 � ���� $ Arrow��	 � �� �#999�� "
 ��.��� 	 �@J�SnapsToDevicePixels . �� 	 �@�OverridesDefaultStyle . �� 	 � ��$
 Auto=��� 	 � ��$
 Auto=��� 	 � �� . �� 	 � ��8   �� 	 �@�FocusVisualStyle Z� �� 	 �@=�ErrorTemplate �� .R�]� ��. .+��� $��Right=����#FF0066$��14(�$
��Bold"� �� $ adornerPlaceholder��$8 2AdornedElement.(Validation.Errors)[0].ErrorContent��� $ 5�����#FF6600$��2q�    $ 10Q�$	 -90Q�$ Quality=�$ 0Q�$ #FF6600��._��� �� 	 � �� �� " ��.R�� .+�d� $��ToggleButton��$��2��$ Press=�|�.  8��  �� $ IsDropDownOpen� �� *TemplatedParent+$ TwoWay=��� $��ContentSite��$��5, 3, 23, 3q� .  $
��Left=�$��Center=�#��z #��z #��z���� $��PART_EditableTextBox��$��3, 3, 23, 3q�# z $y�Hidden=�
������ $
��Left=�$��Center=�|�. .������ " ��.R��� $��PART_ContentHost��|�.  .��� $��Popup��$��Bottom=�|�.   . #��z $��Slide=�.��� $��DropDown�� . #��z  #��z! .+��� $��DropDownBorder��8�� $��
0, 1, 0, 0q�$ 3��$��1,1,1,1q�8�� �� $��4q� . .���� $" 	Contained=� P�  �@J�IsMouseOver . . ��� 8  	 � ��P�  �@��HasItems .  . ��� $ DropDownBorder��	 � ��$ 95��P�  � {� .  . ��� 8  	 � ��P�  �@��
IsGrouping . . ��� 	 � �� .  P�  �@��
IsEditable . . ��� 	 �@ 	IsTabStop .  �� $ PART_EditableTextBox��	 � y�$ Visible=��� $ ContentSite��	 � y�$ Hidden=�