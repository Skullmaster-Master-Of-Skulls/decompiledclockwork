using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x02000441 RID: 1089
	internal class WebBrowserUriTypeConverter : UriTypeConverter
	{
		// Token: 0x06004BB5 RID: 19381 RVA: 0x0013AF60 File Offset: 0x00139160
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			Uri uri = base.ConvertFrom(context, culture, value) as Uri;
			if (uri != null && !string.IsNullOrEmpty(uri.OriginalString) && !uri.IsAbsoluteUri)
			{
				try
				{
					uri = new Uri("http://" + uri.OriginalString.Trim());
				}
				catch (UriFormatException)
				{
				}
			}
			return uri;
		}
	}
}
