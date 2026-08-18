using System;
using System.ComponentModel;

namespace System.Configuration
{
	// Token: 0x02000023 RID: 35
	public abstract class ConfigurationConverterBase : TypeConverter
	{
		// Token: 0x0600014B RID: 331 RVA: 0x0000995D File Offset: 0x00007B5D
		public override bool CanConvertTo(ITypeDescriptorContext ctx, Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000995D File Offset: 0x00007B5D
		public override bool CanConvertFrom(ITypeDescriptorContext ctx, Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000996F File Offset: 0x00007B6F
		internal void ValidateType(object value, Type expected)
		{
			if (value != null && value.GetType() != expected)
			{
				throw new ArgumentException(SR.GetString("Converter_unsupported_value_type", new object[]
				{
					expected.Name
				}));
			}
		}
	}
}
