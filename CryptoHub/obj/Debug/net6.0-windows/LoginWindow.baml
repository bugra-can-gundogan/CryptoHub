   M S B A M L   `   `   `  ����D  @CryptoHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CryptoHub	CryptoHub  5   	       CryptoHub.LoginWindow   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:CryptoHub     5   	     � AllowsTransparency$
  True��5   	   
������  � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5	   	    � ShowInTaskbar$
 True��5
   	    � WindowStartupLocation$ CenterScreen=�5   	    � Title$ LoginWindow��5   	   $	��350��6   $	��450��6*   .��5      ��  �� CornerRadius$ 20��6    �� Padding$ 5q�6   ���#DA0037._�5   
   � .+�5      ��  �� Orientation$ Vertical=�6   .+�5      ]� $��0,10,5,0q�6   .+�5      �� 
�������$��Right=�60   $��30��6L   $��30��6W   $ 15��6c   ._�5      �� -   5        $��CloseTheApp��5   !   $��16(�64   $��X��6B   $��30��6N   $��30��6Y   $��0q�6e   
������ 6�   5      5      �� 5      $��	CryptoHUB��6   $��Center=�5      $��Center=�5      
�������$��32(�5      $��0,5,0,0q�5      $��/Fonts/#Poppins)�5      67   ]� 5      $	��340��6   $��0,50,0,0q�6(   .+�5      �� $��16(�6   $��API KEY:��6*   
�������$��SemiBold"�6P   6h   G� 5      -   5        	 G�Name$	 
api_key_pb��5   z   $��16(�6"   $��Center=�60   $��Right=�6R   $	��230��6n   6�   5       ]� 5!      $	��340��6   $
��0,10q�6(   .+�5"      �� $��16(�6   $��API SECRET:��6*   
�������$��SemiBold"�6S   6k   G� 5#      -   5        $	 api_scret_pb��5#   z   $��16(�6"   $��Center=�60   $��Right=�6R   $	��230��6n   6�   5$      �� 5%      -   5        $��borderOfLogin��5%      $��0,30,0,0q�6.   
�������$	��125��6S   $ 15��6_   ._�5&      �� -   5        $��loginButton��5&      $��26(�60   $��Login��6>   $	��120��6N   $��0q�6Z   
������ $��/Fonts/#Poppins)�6�   6�   5'      5)      5*      5+      5-      