﻿#pragma checksum "..\..\..\UserControls\UCNavigationButtons.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "81DC1806525F21238580D2A674383849"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
using WMDB;


namespace WMDB.UserControls {
    
    
    /// <summary>
    /// UCNavigationButtons
    /// </summary>
    public partial class UCNavigationButtons : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\UserControls\UCNavigationButtons.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid NavigationGrid;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\UserControls\UCNavigationButtons.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblHeading;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\UserControls\UCNavigationButtons.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid NavigationButonsGrid;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\UserControls\UCNavigationButtons.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid BtnHomeGrid;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\UserControls\UCNavigationButtons.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnHome;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\UserControls\UCNavigationButtons.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRefresh;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\UserControls\UCNavigationButtons.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnViewQuery;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\UserControls\UCNavigationButtons.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCopyQuery;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WMDB;component/usercontrols/ucnavigationbuttons.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\UCNavigationButtons.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.NavigationGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.lblHeading = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.NavigationButonsGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.BtnHomeGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.btnHome = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\UserControls\UCNavigationButtons.xaml"
            this.btnHome.Click += new System.Windows.RoutedEventHandler(this.btnHome_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnRefresh = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\UserControls\UCNavigationButtons.xaml"
            this.btnRefresh.Click += new System.Windows.RoutedEventHandler(this.btnRefresh_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnViewQuery = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\UserControls\UCNavigationButtons.xaml"
            this.btnViewQuery.Click += new System.Windows.RoutedEventHandler(this.btnViewQuery_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnCopyQuery = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\UserControls\UCNavigationButtons.xaml"
            this.btnCopyQuery.Click += new System.Windows.RoutedEventHandler(this.btnCopyQuery_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

