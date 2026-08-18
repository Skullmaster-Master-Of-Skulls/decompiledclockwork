using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003F2 RID: 1010
	internal sealed class EmptyStringExpandableObjectConverter : ExpandableObjectConverter
	{
		// Token: 0x060030BD RID: 12477 RVA: 0x0009ECE3 File Offset: 0x0009CEE3
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return string.Empty;
			}
			throw base.GetConvertToException(value, destinationType);
		}
	}
}
