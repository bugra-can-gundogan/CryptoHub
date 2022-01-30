# CryptoHub
An application created with the aim of making it easier to control and observe your cryptocurrency investments, also implemented a trader bot. Use it with your own risk.

Changelog:

30.01.2022 - Build v0.2 - Buğra Gündoğan & Raanem Dahbi

  - User Interface overhaul
  - CryptoBot implementation, CryptoBot is now a python program openned from WPF as a process
  - Coins can now be added to the watch list
  - Coins that are watched are added to the database with the DatabaseRelation class.
  - Every single function in BinanceRelation is now a Task with a result. UI freeze is solved.

  Issues to be addressed:
  - Color selection for UI
  - The database should be changed with something like SharedPreferences in Android dev. Right now MSSQL is an overkill.
  - Python path should either be fetched from user via input or incorporated into the program in some way.
  - Hover state colors for bottons.
  - Quit button functionality.
  - Login screen for user to enter their API_key and API_secret.
  - Login screen should be the startup window.



29.01.2022 - Buğra Gündoğan Build v0.1

- Fixed some errors with charts.
- Removed CryptoBot Tab.
- Removed some informative labels in the wallet tab.
- Implemented the datagrid in the wallet tab.
- The datagrid holds all owned coins, their price, their total value, and the volume held by the user.

  Note: This will be the first build. I removed the CB Tab and labels in wallet tab because those tabs will require us to make more calls to the API. First we will need to address this freezing issue. Also CryptoBot seems to be more difficult to build in C# because of the TA library. Next build will feature the CryptoBot, a new User Interface and a fluent user interface.


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