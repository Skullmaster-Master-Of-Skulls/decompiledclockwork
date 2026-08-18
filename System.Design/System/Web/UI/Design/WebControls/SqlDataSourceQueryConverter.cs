using System;
using System.ComponentModel;
using System.Design;
using System.Globalization;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020004D7 RID: 1239
	internal class SqlDataSourceQueryConverter : TypeConverter
	{
		// Token: 0x06002C88 RID: 11400 RVA: 0x000FAB8B File Offset: 0x000F9B8B
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return SR.GetString("SqlDataSourceQueryConverter_Text");
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x000FABB1 File Offset: 0x000F9BB1
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return null;
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x000FABB4 File Offset: 0x000F9BB4
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}
	}
}
