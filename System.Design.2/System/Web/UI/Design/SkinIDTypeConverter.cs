using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.Design
{
	// Token: 0x02000069 RID: 105
	public class SkinIDTypeConverter : TypeConverter
	{
		// Token: 0x06000312 RID: 786 RVA: 0x00010631 File Offset: 0x0000E831
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0001064F File Offset: 0x0000E84F
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return value;
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00010664 File Offset: 0x0000E864
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destType)
		{
			return destType == typeof(string) || base.CanConvertTo(context, destType);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00010682 File Offset: 0x0000E882
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is string)
			{
				return value;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0001069C File Offset: 0x0000E89C
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (context == null)
			{
				return new TypeConverter.StandardValuesCollection(new ArrayList());
			}
			Control control = context.Instance as Control;
			ArrayList arrayList = new ArrayList();
			if (control != null && control.Site != null)
			{
				IThemeResolutionService themeResolutionService = (IThemeResolutionService)control.Site.GetService(typeof(IThemeResolutionService));
				ThemeProvider stylesheetThemeProvider = themeResolutionService.GetStylesheetThemeProvider();
				ThemeProvider themeProvider = themeResolutionService.GetThemeProvider();
				if (stylesheetThemeProvider != null)
				{
					arrayList.AddRange(stylesheetThemeProvider.GetSkinsForControl(control.GetType()));
					arrayList.Remove(string.Empty);
				}
				if (themeProvider != null)
				{
					ICollection skinsForControl = themeProvider.GetSkinsForControl(control.GetType());
					foreach (object obj in skinsForControl)
					{
						string text = (string)obj;
						if (!arrayList.Contains(text))
						{
							arrayList.Add(text);
						}
					}
					arrayList.Remove(string.Empty);
				}
				arrayList.Sort();
			}
			return new TypeConverter.StandardValuesCollection(arrayList);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000107AC File Offset: 0x0000E9AC
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			ThemeProvider themeProvider = null;
			if (context != null)
			{
				Control control = context.Instance as Control;
				if (control != null && control.Site != null)
				{
					IThemeResolutionService themeResolutionService = (IThemeResolutionService)control.Site.GetService(typeof(IThemeResolutionService));
					if (themeResolutionService != null)
					{
						themeProvider = themeResolutionService.GetThemeProvider();
						if (themeProvider == null)
						{
							themeProvider = themeResolutionService.GetStylesheetThemeProvider();
						}
					}
				}
			}
			return themeProvider != null;
		}
	}
}
