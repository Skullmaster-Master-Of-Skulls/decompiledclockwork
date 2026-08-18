using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Diagnostics.Design
{
	// Token: 0x02000209 RID: 521
	internal class CounterNameConverter : TypeConverter
	{
		// Token: 0x06001369 RID: 4969 RVA: 0x00010631 File Offset: 0x0000E831
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0006F610 File Offset: 0x0006D810
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return ((string)value).Trim();
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0006F63C File Offset: 0x0006D83C
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
				PerformanceCounter[] counters;
				if (instanceNames.Length == 0)
				{
					counters = performanceCounterCategory.GetCounters();
				}
				else
				{
					counters = performanceCounterCategory.GetCounters(instanceNames[0]);
				}
				string[] array = new string[counters.Length];
				for (int i = 0; i < counters.Length; i++)
				{
					array[i] = counters[i].CounterName;
				}
				Array.Sort(array, Comparer.Default);
				return new TypeConverter.StandardValuesCollection(array);
			}
			catch (Exception)
			{
			}
			return null;
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
