﻿#pragma checksum "..\..\..\..\..\MVVM\View\CryptoBotView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B055BE6AB880AF5985838DEEEDF0BA85CDE928E6"
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
    /// CryptoBotView
    /// </summary>
    public partial class CryptoBotView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\..\..\MVVM\View\CryptoBotView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ValueAllowedByUserLBL;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\..\MVVM\View\CryptoBotView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ParityWantedToTradeLBL;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\..\MVVM\View\CryptoBotView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button runBotBtn;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\..\MVVM\View\CryptoBotView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button stopBotBtn;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\..\MVVM\View\CryptoBotView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ActivityDataGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/CryptoHub;component/mvvm/view/cryptobotview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\MVVM\View\CryptoBotView.xaml"
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
            this.ValueAllowedByUserLBL = ((System.Windows.Controls.TextBox)(target));
            
            #line 13 "..\..\..\..\..\MVVM\View\CryptoBotView.xaml"
            this.ValueAllowedByUserLBL.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ParityWantedToTradeLBL = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.runBotBtn = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\..\..\MVVM\View\CryptoBotView.xaml"
            this.runBotBtn.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.stopBotBtn = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\..\..\MVVM\View\CryptoBotView.xaml"
            this.stopBotBtn.Click += new System.Windows.RoutedEventHandler(this.stopBotBtn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ActivityDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

