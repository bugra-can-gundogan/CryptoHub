   M S B A M L   `   `   `  ����D  @CryptoHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:CryptoHub.MVVM.ViewCryptoHub.MVVM.View  5      U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 6>   $    CryptoHub.MVVM.View.MarketView   5      W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:CryptoHub.MVVM.View     5      Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     6>   .��5	      � ���#171717  � RowDefinitions  5
   
   �� 5      $��75 �6   �� 5      �� 5      .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   .+�5      ��  �� CornerRadius$ 10��6   ���#EDEDED$	��200��5      $��40��5      $��15 0 0 0q�5      ._�5      � .+�5      �� -   5        $��m_Search_Box��5      $	��200��5      $��40��5      $��Center=�5      $
��Left=�5      $��5q�5      $��Search for parity...��5      
������ ���#171717$��/Fonts/#Poppins)�5       $��0q�5!      5"   {   5#      5$      ]� 5%      $	��680��6   .+�5&      �� -   5        $��CurrentPriceLabel��5&   J   ���#EDEDED$��/Fonts/#Poppins)�6-   $��Center=�6c   $	��---��6�   $��15 0 0 0q�6�   6�   �� 5'      -   5        $��
dHighLabel��5'   -   ���#EDEDED$��Center=�6?   $��Daily High:---��6a   6|   �� 5(      -   5        $��	dLowLabel��5(   -   ���#EDEDED$��Center=�6>   $��Daily Low:---��6`   6z   �� 5)      -   5        $��Add to Wacth List��5)         MuhStyle#��[  65   $��Right=�6W   6s   5*      5,      #  LiveCharts.Wpf.CartesianChart  5-   
   $��1��6   &   LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$
 None=�6N      Series 6*   �� *SeriesCollection+   AxisY 5/        LiveCharts.Wpf.Axis     Title$ Price(USDT)��6      
Foreground �#EDEDED   LabelFormatter 6D   �� *
YFormatter+6l   	   AxisX	 52        $
 Date��6    �#EDEDED
   Labels
 6=   �� *Labels+6Y   54      ]� 55   
   $��2��6   $	��600��6!   .+�56      �� $ Vertical=�6   $��	10,50,0,0q�60   .+�57      ]� $	��0 3q�6   $	��200��6+   .+�58      �� ���#EDEDED$"��Trade Type:           MARKET��61   6Z   59      ]� 5:      $	��0 3q�6   $	��200��6+   .+�5;      �� ���#EDEDED$��Price(USD):��61   6I   �� 5<      -   5        $��PriceTextBoxMarket��5<   5   
������� �� 
IsReadOnly$
 True��6P   $	��100��6b   $��Right=�6n   6�   5=      ]� 5>      $	��0 3q�6   $	��200��6+   .+�5?      �� ���#EDEDED$��Amount:��61   6E   �� 5@      -   5        $��AmountTextBoxMarket��5@   5   
�������$
 True��6P   $	��100��6c   $��Right=�6o   6�   5A      ]� 5B      $	��0 3q�6   $	��200��6+   .+�5C      �� ���#EDEDED$��Total(USD):��61   6I   �� 5D      -   5        $��TotalTextBoxMarket��5D      $	��100��6\   $��Right=�6h   6�   5E      ]� 5F      $
��0 10q�6   $	��200��6,   .+�5G      �� -	   5        $��MarketBUYBtn��5G      $	��BUY��61   $
��Left=�6?   $��Center=�6Z   $��60��6~   $��30��6�   $��18(�6�   $��Gill Sans MT)�6�   $��0q�6�   ���#519872���#EDEDED6  �� 5H      -
   5        $��MarketSELLBtn��5H      $
��SELL��62   $��Right=�6A   $��Center=�6]   $��60��6�   $��30��6�   $��18(�6�   $��Gill Sans MT)�6�   $��0q�6�   ���#DA0037���#EDEDED6  5I      5J      5K      5M      5O      