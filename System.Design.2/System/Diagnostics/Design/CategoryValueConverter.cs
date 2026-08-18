using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Diagnostics.Design
{
	// Token: 0x02000207 RID: 519
	internal class CategoryValueConverter : TypeConverter
	{
		// Token: 0x06001361 RID: 4961 RVA: 0x00010631 File Offset: 0x0000E831
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0006F438 File Offset: 0x0006D638
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return ((string)value).Trim();
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x0006F464 File Offset: 0x0006D664
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			PerformanceCounter performanceCounter = (context == null) ? null : (context.Instance as PerformanceCounter);
			string text = ".";
			if (performanceCounter != null)
			{
				text = performanceCounter.MachineName;
			}
			if (text == this.previousMachineName)
			{
				return this.values;
			}
			this.previousMachineName = text;
			try
			{
				PerformanceCounter.CloseSharedResources();
				PerformanceCounterCategory[] categories = PerformanceCounterCategory.GetCategories(text);
				string[] array = new string[categories.Length];
				for (int i = 0; i < categories.Length; i++)
				{
					array[i] = categories[i].CategoryName;
				}
				Array.Sort(array, Comparer.Default);
				this.values = new TypeConverter.StandardValuesCollection(array);
			}
			catch (Exception)
			{
				this.values = null;
			}
			return this.values;
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x04000A7A RID: 2682
		private TypeConverter.StandardValuesCollection values;

		// Token: 0x04000A7B RID: 2683
		private string previousMachineName;
	}
}
