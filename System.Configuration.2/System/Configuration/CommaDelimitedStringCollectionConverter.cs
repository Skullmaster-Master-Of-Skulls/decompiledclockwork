using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Configuration
{
	// Token: 0x02000018 RID: 24
	public sealed class CommaDelimitedStringCollectionConverter : ConfigurationConverterBase
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00008DCC File Offset: 0x00006FCC
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			base.ValidateType(value, typeof(CommaDelimitedStringCollection));
			CommaDelimitedStringCollection commaDelimitedStringCollection = value as CommaDelimitedStringCollection;
			if (commaDelimitedStringCollection != null)
			{
				return commaDelimitedStringCollection.ToString();
			}
			return null;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00008DFC File Offset: 0x00006FFC
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			CommaDelimitedStringCollection commaDelimitedStringCollection = new CommaDelimitedStringCollection();
			commaDelimitedStringCollection.FromString((string)data);
			return commaDelimitedStringCollection;
		}
	}
}
