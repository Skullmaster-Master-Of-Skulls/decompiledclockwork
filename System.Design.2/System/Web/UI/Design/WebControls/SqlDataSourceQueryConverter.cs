using System;
using System.ComponentModel;
using System.Design;
using System.Globalization;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000115 RID: 277
	internal class SqlDataSourceQueryConverter : TypeConverter
	{
		// Token: 0x06000A1C RID: 2588 RVA: 0x0003F8A1 File Offset: 0x0003DAA1
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return SR.GetString("SqlDataSourceQueryConverter_Text");
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00003598 File Offset: 0x00001798
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return null;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}
	}
}
