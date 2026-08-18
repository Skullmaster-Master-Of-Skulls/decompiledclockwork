using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020000CD RID: 205
	internal sealed class PrimaryKeyTypeConverter : ReferenceConverter
	{
		// Token: 0x06000CC6 RID: 3270 RVA: 0x00212238 File Offset: 0x00211638
		public PrimaryKeyTypeConverter() : base(typeof(DataColumn[]))
		{
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00212258 File Offset: 0x00211658
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00212268 File Offset: 0x00211668
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00212298 File Offset: 0x00211698
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string))
			{
				return new DataColumn[0].GetType().Name;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
