   M S B A M L   `   `   `  ����D  @CryptoHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CryptoHub	CryptoHub  5   	   C&clr-namespace:CryptoHub.MVVM.ViewModelCryptoHub.MVVM.ViewModel  5   	       CryptoHub.MainWindow   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:CryptoHub     5   	   <	viewModel&clr-namespace:CryptoHub.MVVM.ViewModel     5   	     � Title$  
MainWindow��5	   	   $	��800��5
   	   $
��1200��6    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	   
������  � AllowsTransparency$
 True��5   	    � DataContext 5      ,  &CryptoHub.MVVM.ViewModel.MainViewModel  5   
   6$   .��5      �� ���#DA0037 �� CornerRadius$ 20��5      ._�5   
   �  � ColumnDefinitions 5      �� 5      $	��200 �6#   �� 5       � RowDefinitions 5      �� 5      $��75 �6    �� 5      .+�5      �� $��	CryptoHUB��6   $��Center=�5      $
��Left=�5       
�������$��22(�5"      $��20,0,0,0q�5#      $��/Fonts/#Poppins)�5$      67   �� 5&      $��1��6   .+�5'      	� $��Market��6   $��50��5(      
�������$��14(�5*         MenuButtonTheme#��[  5+       d� 	IsChecked$
 TrueY�5,      ��5-      �� *MarketViewCommand+	� 5/      $��Wallet��6   $��50��50      
�������$��14(�52      #��[  53      ��54      �� *WalletViewCommand+	� 56      $��
Crypto Bot��6   $��50��57      
�������$��14(�59      #��[  5:      ��5;      �� *CryptoBotViewCommand+	� 5=      $��
Watch List��6   $��50��5>      
�������$��14(�5@      #��[  5A      ��5B      �� *WatchListViewCommand+5C      �� 5E      $��1��6   $��Right=�6)   	 �� Orientation$	 
Horizontal=�6E   $��0 0 10 0q�6^   .+�5F      �� -   5        $ 5��5F      ���#DA0037$	��942��5H      $��73��5I      $��
10 0 30 30q�5J      6�   �� 5K      -   5        $��
QuitButton��5K      $
��Quit��6+     MuhStyle#��[ 6:   $��3q�6\   6�   5L      �� 5N      $��1��6   $��1��5O      $��10 10 30 30q�5P      ��5Q      �� *CurrentView+6?   5S      5T      5U      