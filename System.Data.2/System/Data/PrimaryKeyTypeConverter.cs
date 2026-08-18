using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Data
{
	// Token: 0x02000117 RID: 279
	internal sealed class PrimaryKeyTypeConverter : ReferenceConverter
	{
		// Token: 0x060010F1 RID: 4337 RVA: 0x00083214 File Offset: 0x00082614
		public PrimaryKeyTypeConverter() : base(typeof(DataColumn[]))
		{
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00083234 File Offset: 0x00082634
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00083244 File Offset: 0x00082644
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00083270 File Offset: 0x00082670
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
