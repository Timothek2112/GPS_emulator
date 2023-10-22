using GMap.NET;
using GMap.NET.WindowsPresentation;
using RitDrillingMonitoringWPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RitDrillingMonitoringWPF.Utils
{
    internal class Marker : GMapMarker
    {
        BitmapImage _pinSrcImage;
        
        public Marker(PointLatLng pos) : base(pos)
        {
            Shape = CreatePinImage(this);
        }

        Image CreatePinImage(GMapMarker marker)
        {
            Image img = new Image
            {
                Tag = marker,
                Width = 32,
                Height = 32
            };

            if (_pinSrcImage == null)
            {
                _pinSrcImage = new BitmapImage(new Uri("pack://application:,,,/Resources/pin.png", UriKind.Absolute));
                _pinSrcImage.Freeze();
            }
            img.Source = _pinSrcImage;
            marker.Offset = new Point(-img.Width / 2, -img.Height / 2);
            return img;
        }

        public async void SendMessage()
        {
            var socket = new Socket("ws://109.174.29.40:6686/ws/1");
            await socket.Open();
            var gpgga = GpsSerializer.CreateGPGGA(this);
            await socket.Send(gpgga.ToString());
            await socket.Close();
        }
    }
}
