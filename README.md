# CryptoHub
An application created with the aim of making it easier to control and observe your cryptocurrency investments, also implemented a trader bot. Use it with your own risk.

Changelog:

29.01.2022 - Raanem Dahbi - 2

- Implemented the chart for wallet.
- Wallet chart gets the all assets and their prices, and how much the user owns, and draws the graph.
- Implemented necessary functions for wallet chart in mainwindow.xaml.cs and BinanceRelation class.

  Note: Wallet chart doesn't get updated for now. It should be updated just like in the market. But right now updating it freezes the User Interface too much. 


29.01.2022 - Raanem Dahbi

- Implemented the chart. It should now update every three seconds. 
- !Below three seconds gives the API a timeout!
- There is a stutter while updating and generating the coins dictionary
- There can be adjustments on updating the yFormatter variable, coins with little value look bad on the graph sometimes.
- In the timer_tick function I also update the price textbox below to give the most updated price.

29.01.2022 bugra-gundogan

- Created the BinanceRelation class which handles the following functions:
          - Purchase of an asset
          - Selling of an asset
          - Getting the current price of the asset
          - Getting the highest price the asset reached in that day
          - Getting the lowest price the asset reached in that day
          - Getting the every asset that is tradable with Tether USD
- Purchasing and selling is now available

  Note: The graph still is not implemented. Building the graph and updating it in an interval is a priority.

28.01.2022 - Raanem Dahbi

- Implemented the if else statements to check if the user enters something wrong
- Implemented necessary event where the graph will be updated
- Implemented necessary event to update the labels
- Implemented necessary event to buy or sell the asset
- Implemented the event that changes amount text box according to the total textbox and current price of the asset

  Note: Could not implement the parts of the events listed above where it requires to have an API Key. Those parts are commented as NOT IMPLEMENTED in the code.

26.01.2022 bugra-gundogan

- Created the user interface
- Implemented LiveCharts Package
- Implemented Binance.Net Package
- Created the necessary functions for user control