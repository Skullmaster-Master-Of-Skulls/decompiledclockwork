using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x02000031 RID: 49
	public class OracleParameterTypeConverter : TypeConverter
	{
		// Token: 0x06000298 RID: 664 RVA: 0x0000F248 File Offset: 0x0000D448
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000F268 File Offset: 0x0000D468
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			try
			{
				if (value != null && destinationType == typeof(InstanceDescriptor))
				{
					ConstructorInfo constructor = value.GetType().GetConstructor(new Type[0]);
					return new InstanceDescriptor(constructor, new object[0], false);
				}
			}
			catch
			{
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
