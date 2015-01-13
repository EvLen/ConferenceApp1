using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return new EditConference(uri.ToString().Replace("/","").ConvertToInt32(0));
        }
    }
}
