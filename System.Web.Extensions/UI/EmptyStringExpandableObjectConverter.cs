using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI
{
	// Token: 0x0200004E RID: 78
	internal sealed class EmptyStringExpandableObjectConverter : ExpandableObjectConverter
	{
		// Token: 0x060002E8 RID: 744 RVA: 0x00011E13 File Offset: 0x00010013
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return string.Empty;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
