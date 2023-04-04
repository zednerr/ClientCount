using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;

namespace ClientCount.Models
{
    public class ToastView
    {
        private readonly string _text;
        public ToastView(string text) {
        _text = text;
        }
        public ToastOptions ToastOptions() {
            var messageOptions = new MessageOptions
            {
                Foreground =Color.Blue,
                Font = Font.SystemFontOfSize(16),                
                Message = _text
            };
            var options = new ToastOptions
            {
                MessageOptions = messageOptions,
                Duration = TimeSpan.FromMilliseconds(3000),
                BackgroundColor = Color.White,
                CornerRadius=10,
                IsRtl = false
            };
            return options;
        }

    }
}
