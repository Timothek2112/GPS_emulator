using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using RitDrillingMonitoringWPF.Services;
using RitDrillingMonitoringWPF.Utils;

namespace RitDrillingMonitoringWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Marker _currentElement;

        public MainWindow()
        {
            InitializeComponent();
            MapControlDrill.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            MapControlDrill.MinZoom = 2;
            MapControlDrill.MaxZoom = 16;
            MapControlDrill.Zoom = 11;
            MapControlDrill.Position = new PointLatLng(55.008111, 82.921768);
            MapControlDrill.MouseWheelZoomType = MouseWheelZoomType.MousePositionWithoutCenter;
            MapControlDrill.CanDragMap = true;
            MapControlDrill.DragButton = MouseButton.Right;
            MapControlDrill.ShowCenter = false;
            MapControlDrill.ShowTileGridLines = false;
            var converter = TypeDescriptor.GetConverter(typeof(Geometry));
            infoColumn.Width = new GridLength(0);
            var polygon = new GMapPolygon(new List<PointLatLng>()

            {
                new PointLatLng(54.971402, 82.873281),
                new PointLatLng(54.994728, 82.869857),
                new PointLatLng(54.994858, 82.875244),
                new PointLatLng(54.983081, 82.896064),
                new PointLatLng(54.972556, 82.897647)
            })
            {
                Shape = new Path()
                {
                    Stroke = Brushes.DarkBlue,
                    StrokeThickness = 1.5,
                    Effect = null,
                    Fill = Brushes.DarkBlue,
                    Opacity = 0.25

                }
            };

            MapControlDrill.Markers.Add(polygon);
            AddMaker(new PointLatLng(54.978770, 82.882599));

        }

        private void AddMaker(PointLatLng pt)
        {
            GMapMarker marker = new Marker(pt);
            MapControlDrill.Markers.Add(marker);
        }

        private HitTestResultBehavior HitTestCallback(HitTestResult result)
        {
            Image image = result.VisualHit as Image;
            if (image != null)
            {
                _currentElement = image.Tag as Marker;
                //OpenInfoPanel();
                var task = new Task(() => OpenInfoPanel()); ;
                task.Start();
                return HitTestResultBehavior.Stop;
            }
            return HitTestResultBehavior.Continue;
        }

        private void MapControlDrill_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_currentElement == null)
            {
                Point pt = e.GetPosition(MapControlDrill);
                PointLatLng point = MapControlDrill.FromLocalToLatLng((int)pt.X, (int)pt.Y);
                PointHitTestParameters parameters = new PointHitTestParameters(pt);
                VisualTreeHelper.HitTest(MapControlDrill, null, HitTestCallback, parameters);
            }
        }

        private void MapControlDrill_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed
                && _currentElement != null)
            {
                Point pt = e.GetPosition(MapControlDrill);
                PointLatLng point = MapControlDrill.FromLocalToLatLng((int)pt.X, (int)pt.Y);
                _currentElement.Position = point;
            }
        }

        private void MapControlDrill_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(_currentElement != null)
            {
                _currentElement.SendMessage();
            }
            _currentElement = null;
        }

        private async Task OpenInfoPanel()
        {
            while(infoColumn.Width.Value < 200)
            {
                infoColumn.Width = new GridLength(infoColumn.Width.Value * (1 - 0.1) + 300 * 0.1);
            }
        }
    }
}
