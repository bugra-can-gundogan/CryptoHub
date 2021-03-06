#pragma checksum "..\..\..\..\..\MVVM\View\WalletView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "726B29EC7C0E09FC6ED21FE50B97221E196F8A96"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CryptoHub.MVVM.View;
using LiveCharts.Wpf;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace CryptoHub.MVVM.View {
    
    
    /// <summary>
    /// WalletView
    /// </summary>
    public partial class WalletView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\..\..\MVVM\View\WalletView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel WalletMainPanel;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\..\MVVM\View\WalletView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LiveCharts.Wpf.PieChart Chart;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\..\MVVM\View\WalletView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label walletTotalValLbl;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\..\MVVM\View\WalletView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label DailyPNRLbl;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\..\MVVM\View\WalletView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LastMonthLbl;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\..\MVVM\View\WalletView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label YearPNRLbl;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\..\MVVM\View\WalletView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid AllCoinsDataGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CryptoHub;component/mvvm/view/walletview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\MVVM\View\WalletView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.WalletMainPanel = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 2:
            this.Chart = ((LiveCharts.Wpf.PieChart)(target));
            return;
            case 3:
            this.walletTotalValLbl = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.DailyPNRLbl = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.LastMonthLbl = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.YearPNRLbl = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.AllCoinsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

