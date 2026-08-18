using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000575 RID: 1397
	internal sealed class EmptyStringExpandableObjectConverter : ExpandableObjectConverter
	{
		// Token: 0x06004498 RID: 17560 RVA: 0x0011A1B7 File Offset: 0x001191B7
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
