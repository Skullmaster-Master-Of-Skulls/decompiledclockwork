using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Diagnostics.Design
{
	// Token: 0x0200020A RID: 522
	internal class InstanceNameConverter : TypeConverter
	{
		// Token: 0x0600136E RID: 4974 RVA: 0x00010631 File Offset: 0x0000E831
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0006F704 File Offset: 0x0006D904
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return ((string)value).Trim();
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0006F730 File Offset: 0x0006D930
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			PerformanceCounter performanceCounter = (context == null) ? null : (context.Instance as PerformanceCounter);
			string machineName = ".";
			string categoryName = string.Empty;
			if (performanceCounter != null)
			{
				machineName = performanceCounter.MachineName;
				categoryName = performanceCounter.CategoryName;
			}
			try
			{
				PerformanceCounterCategory performanceCounterCategory = new PerformanceCounterCategory(categoryName, machineName);
				string[] instanceNames = performanceCounterCategory.GetInstanceNames();
				Array.Sort(instanceNames, Comparer.Default);
				return new TypeConverter.StandardValuesCollection(instanceNames);
			}
			catch (Exception)
			{
			}
			return null;
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
