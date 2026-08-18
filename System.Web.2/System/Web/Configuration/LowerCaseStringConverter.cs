using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.Configuration
{
	// Token: 0x02000711 RID: 1809
	public sealed class LowerCaseStringConverter : TypeConverter
	{
		// Token: 0x06005717 RID: 22295 RVA: 0x00130421 File Offset: 0x0012E621
		public override bool CanConvertTo(ITypeDescriptorContext ctx, Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x06005718 RID: 22296 RVA: 0x00130421 File Offset: 0x0012E621
		public override bool CanConvertFrom(ITypeDescriptorContext ctx, Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x06005719 RID: 22297 RVA: 0x00130433 File Offset: 0x0012E633
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			if (value == null)
			{
				return string.Empty;
			}
			return ((string)value).ToLower(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600571A RID: 22298 RVA: 0x0013044E File Offset: 0x0012E64E
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			return ((string)data).ToLower(CultureInfo.InvariantCulture);
		}
	}
}
