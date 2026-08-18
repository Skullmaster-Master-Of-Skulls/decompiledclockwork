using System;
using System.ComponentModel;
using System.Globalization;

namespace Telerik.Web.UI
{
	// Token: 0x020012EC RID: 4844
	public class RecurrenceRuleConverter : TypeConverter
	{
		// Token: 0x0600CB58 RID: 52056 RVA: 0x002D7383 File Offset: 0x002D5583
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600CB59 RID: 52057 RVA: 0x002D73A4 File Offset: 0x002D55A4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				throw new InvalidOperationException("Cannot convert from null value.");
			}
			string text = value as string;
			if (!string.IsNullOrEmpty(text))
			{
				RecurrenceRule result;
				RecurrenceRule.TryParse(text, out result);
				return result;
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x0600CB5A RID: 52058 RVA: 0x002D73E2 File Offset: 0x002D55E2
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600CB5B RID: 52059 RVA: 0x002D7400 File Offset: 0x002D5600
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				RecurrenceRule recurrenceRule = value as RecurrenceRule;
				if (recurrenceRule != null)
				{
					return recurrenceRule.ToString();
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x0600CB5C RID: 52060 RVA: 0x002D7444 File Offset: 0x002D5644
		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			string text = value as string;
			if (!string.IsNullOrEmpty(text))
			{
				RecurrenceRule o;
				RecurrenceRule.TryParse(text, out o);
				return o != null;
			}
			return base.IsValid(context, value);
		}
	}
}
