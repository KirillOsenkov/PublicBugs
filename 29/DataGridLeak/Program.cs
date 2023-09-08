using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

class Program
{
    [STAThread]
    static void Main()
    {
        var application = new Application();
        var window = new Window();

        var rootPanel = new DockPanel();

        var buttonPanel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Right
        };

        var items = new ObservableCollection<ChonkyClass>();

        var addButton = new Button 
        {
            Content = "Add",
            Width = 75,
            Height = 23,
            Margin = new Thickness(6)
        };
        addButton.Click += (s, e) =>
        {
            items.Add(new ChonkyClass());
        };

        var clearButton = new Button 
        {
            Content = "Clear",
            Width = 75,
            Height = 23,
            Margin = new Thickness(6)
        };
        clearButton.Click += (s, e) =>
        {
            items.Clear();

            GC();
        };

        var gcButton = new Button
        {
            Content = "GC",
            Width = 75,
            Height = 23,
            Margin = new Thickness(6)
        };
        gcButton.Click += (s, e) =>
        {
            GC();
        };

        var label = new Label();
        var timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromMilliseconds(300);
        timer.Tick += (s, e) =>
        {
            label.Content = $"{Process.GetCurrentProcess().PrivateMemorySize64:n0} bytes";
        };
        timer.Start();

        buttonPanel.Children.Add(label);
        buttonPanel.Children.Add(gcButton);
        buttonPanel.Children.Add(addButton);
        buttonPanel.Children.Add(clearButton);

        var dataGrid = new DataGrid();

        dataGrid.ItemsSource = items;

        // work around a bug where horizontal scrolling would flicker because SelectAll button is shown/hidden
        dataGrid.HeadersVisibility = DataGridHeadersVisibility.Column;

        dataGrid.SelectionMode = DataGridSelectionMode.Extended;

        // to prevent the NewItemPlaceholder to be added
        dataGrid.CanUserAddRows = false;

        rootPanel.Children.Add(buttonPanel);
        rootPanel.Children.Add(dataGrid);
        DockPanel.SetDock(buttonPanel, Dock.Bottom);

        FixDataGridClearingLeak(dataGrid);

        window.Content = rootPanel;

        application.Run(window);
    }

    private static void GC()
    {
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
        System.GC.Collect();
    }

    private static readonly FieldInfo _selectionAnchorField = typeof(DataGrid).GetField("_selectionAnchor", BindingFlags.Instance | BindingFlags.NonPublic);
    private static readonly FieldInfo _focusedInfoField = typeof(ItemsControl).GetField("_focusedInfo", BindingFlags.Instance | BindingFlags.NonPublic);
    private static readonly PropertyInfo FocusedCellProperty = typeof(DataGrid).GetProperty("FocusedCell", BindingFlags.Instance | BindingFlags.NonPublic);

    /// <summary>
    /// https://github.com/dotnet/wpf/issues/6983
    /// </summary>
    public static void FixDataGridClearingLeak(DataGrid dataGrid)
    {
        var items = dataGrid.ItemsSource;
        if (items is IEnumerable)
        {
            var view = CollectionViewSource.GetDefaultView(items);
            view.CollectionChanged += (s, e) =>
            {
                if (view.IsEmpty)
                {
                    _selectionAnchorField.SetValue(dataGrid, null);
                    _focusedInfoField.SetValue(dataGrid, null);
                    FocusedCellProperty.SetValue(dataGrid, null);

                    dataGrid.CurrentItem = null;
                    dataGrid.CurrentCell = default;
                    dataGrid.CurrentColumn = null;
                }
            };
        }
    }

    public class ChonkyClass
    {
        public int[] Array;

        public ChonkyClass()
        {
            Array = new int[100000000];
        }

        public int Id => GetHashCode();
    }
}