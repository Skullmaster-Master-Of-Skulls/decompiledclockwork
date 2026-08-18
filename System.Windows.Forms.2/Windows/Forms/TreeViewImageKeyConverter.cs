using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x02000422 RID: 1058
	public class TreeViewImageKeyConverter : ImageKeyConverter
	{
		// Token: 0x060049DC RID: 18908 RVA: 0x00137254 File Offset: 0x00135454
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value == null)
			{
				return SR.GetString("toStringDefault");
			}
			string text = value as string;
			if (text != null && text.Length == 0)
			{
				return SR.GetString("toStringDefault");
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
