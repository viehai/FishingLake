   M S B A M L   `   `   `  ����G  CFishing_Lake, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Fishing_LakeFishing_Lake  5   	       Fishing_Lake.DetailWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Fishing_Lake     5   	     � Title$  Pond Details��5   	   $	��550��6   $	��900��6+    � WindowStartupLocation$ CenterScreen=�67   ���#e0f7fa � 
ResizeMode$ NoResize=�6q   .��5	      ��  �� CornerRadius$ 10��6   ���#0288d1$��2q�65   $��10q�6I    �� Padding$ 20q�6U   ._�5
   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��* �6    �� 5      $
��Auto �6    .+�5      �� $%��🛠 Create / Edit Fishing Pond��6   $��24(�5      $
��Bold"�6&   ���#0288d1$��0,0,0,20q�5      � 5      $��1��6    � ColumnDefinitions 5      �� 5      $��* �6'   �� 5      $��* �6'   .+�5      �� $��0��6   $��0,0,10,0q�6-   .+�5      �� $��
Pond Name:��6   �� 5      -   5         ��Name$ txtName��5      $��30��6/   �� 5      $��	Location:��6   �� 5       -   5        $ txtLocation��5       $��30��63   �� 5"      $��	Capacity:��6   �� 5#      -   5        $ txtCapacity��5#      $��30��63   �� 5%      $��Description:��6   �� 5&      -   5        $ txtDescription��5&      $��60��66    �� TextWrapping$
 Wrap=�6B   	 �� AcceptsReturn$
	 True��6V   5'      �� 5)      $��1��6   $��10,0,0,0q�6-   .+�5,      �� $��Fish Species in Pond:��6   &   System.Windows.Controls.DataGrid  5-      -   5        
  Name$
 
dgFishList��5-         AutoGenerateColumns$ False��63   $	��180��6O      CanUserAddRows$
 True��6\      Columns 5.      4  .System.Windows.Controls.DataGridComboBoxColumn  5/      ,  &System.Windows.Controls.DataGridColumn   Header$ Fish Species��65      DisplayMemberPath$
 Name��50   5      SelectedValuePath$ Id��6N      SelectedValueBinding 5/   K   �� *FishId+   ItemsSource 51      ��  �� RelativeSource ��  �� AncestorType
 Window �� Path$ fishSpecies�54      57      58      �� 5:      $��2��6    �� Orientation$ 
Horizontal=�6&   $��Right=�6?   $��0,20,0,0q�6[   .+�5;      �� -   5        $��💾 Save Pond��5;      $	��120��60   $��0,0,10,0q�6<   ���#0288d1
�������6v   �� 5<      -   5        $��	❌ Close��5<      $��80��6+   66   5=      5>      5?      5@      