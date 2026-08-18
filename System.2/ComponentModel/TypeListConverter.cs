using System;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005B7 RID: 1463
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class TypeListConverter : TypeConverter
	{
		// Token: 0x060036F0 RID: 14064 RVA: 0x000EF8A9 File Offset: 0x000EDAA9
		protected TypeListConverter(Type[] types)
		{
			this.types = types;
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x000EF8B8 File Offset: 0x000EDAB8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x000EF8D6 File Offset: 0x000EDAD6
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x000EF8F4 File Offset: 0x000EDAF4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				foreach (Type type in this.types)
				{
					if (value.Equals(type.FullName))
					{
						return type;
					}
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060036F4 RID: 14068 RVA: 0x000EF93C File Offset: 0x000EDB3C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(string)))
			{
				if (destinationType == typeof(InstanceDescriptor) && value is Type)
				{
					MethodInfo method = typeof(Type).GetMethod("GetType", new Type[]
					{
						typeof(string)
					});
					if (method != null)
					{
						return new InstanceDescriptor(method, new object[]
						{
							((Type)value).AssemblyQualifiedName
						});
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (value == null)
			{
				return SR.GetString("toStringNone");
			}
			return ((Type)value).FullName;
		}

		// Token: 0x060036F5 RID: 14069 RVA: 0x000EFA00 File Offset: 0x000EDC00
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.values == null)
			{
				object[] destinationArray;
				if (this.types != null)
				{
					destinationArray = new object[this.types.Length];
					Array.Copy(this.types, destinationArray, this.types.Length);
				}
				else
				{
					destinationArray = null;
				}
				this.values = new TypeConverter.StandardValuesCollection(destinationArray);
			}
			return this.values;
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x000EFA55 File Offset: 0x000EDC55
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x000EFA58 File Offset: 0x000EDC58
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x04002AC1 RID: 10945
		private Type[] types;

		// Token: 0x04002AC2 RID: 10946
		private TypeConverter.StandardValuesCollection values;
	}
}
