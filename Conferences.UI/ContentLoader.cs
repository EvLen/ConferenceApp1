using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conferences.UI.Pages;
using Conferences.UI.Pages.Controls;
using FirstFloor.ModernUI.Windows;

namespace Conferences.UI
{
    public class ContentLoader
        : DefaultContentLoader
    {
        /// <summary>
        /// Loads the content from specified uri.
        /// </summary>
        /// <param name="uri">The content uri</param>
        /// <returns>The loaded content.</returns>
        protected override object LoadContent(Uri uri)
        {
            var url = uri.ToString().ToLower();
            if (url.Contains("home")) return new Home();
            if (url.Contains("rooms"))
                return new EditRoom(url.Replace("/rooms?","").ConvertToInt32(0));//this should be ur room control
            if (url.Contains("users"))
                return new EditConference(url.Replace("/users?", "").ConvertToInt32(0));//this should be ur user control

            return new EditConference(uri.ToString().Replace("/","").ConvertToInt32(0));

            if (url.Contains("days"))
                return new EditDay(url.Replace("/days?", "").ConvertToInt32(0));
        }
    }
}
