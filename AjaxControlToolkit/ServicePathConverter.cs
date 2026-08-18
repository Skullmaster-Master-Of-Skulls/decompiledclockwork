using System;
using System.ComponentModel;
using System.Globalization;
using System.Web;

namespace AjaxControlToolkit
{
	// Token: 0x02000099 RID: 153
	public class ServicePathConverter : StringConverter
	{
		// Token: 0x060004C3 RID: 1219 RVA: 0x0000D30C File Offset: 0x0000B50C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				string value2 = (string)value;
				if (string.IsNullOrEmpty(value2))
				{
					HttpContext httpContext = HttpContext.Current;
					if (httpContext != null)
					{
						return httpContext.Request.FilePath;
					}
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
