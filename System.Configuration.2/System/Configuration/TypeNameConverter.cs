using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Configuration
{
	// Token: 0x0200009B RID: 155
	public sealed class TypeNameConverter : ConfigurationConverterBase
	{
		// Token: 0x0600061A RID: 1562 RVA: 0x0001CFE8 File Offset: 0x0001B1E8
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			if (!(value is Type))
			{
				base.ValidateType(value, typeof(Type));
			}
			string result = null;
			if (value != null)
			{
				result = ((Type)value).AssemblyQualifiedName;
			}
			return result;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001D020 File Offset: 0x0001B220
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			Type typeWithReflectionPermission = TypeUtil.GetTypeWithReflectionPermission((string)data, false);
			if (typeWithReflectionPermission == null)
			{
				throw new ArgumentException(SR.GetString("Type_cannot_be_resolved", new object[]
				{
					(string)data
				}));
			}
			return typeWithReflectionPermission;
		}
	}
}
