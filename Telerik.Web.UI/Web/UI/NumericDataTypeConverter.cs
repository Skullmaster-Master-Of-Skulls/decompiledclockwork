using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace Telerik.Web.UI
{
	// Token: 0x02001907 RID: 6407
	internal class NumericDataTypeConverter : TypeConverter
	{
		// Token: 0x17004B0F RID: 19215
		// (get) Token: 0x0600F86A RID: 63594 RVA: 0x00382447 File Offset: 0x00380647
		internal static ArrayList SupportedTypes
		{
			get
			{
				return new ArrayList(NumericDataTypeConverter.types);
			}
		}

		// Token: 0x0600F86B RID: 63595 RVA: 0x00382453 File Offset: 0x00380653
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertTo(context, sourceType);
		}

		// Token: 0x0600F86C RID: 63596 RVA: 0x00382471 File Offset: 0x00380671
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600F86D RID: 63597 RVA: 0x00382490 File Offset: 0x00380690
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null || value.GetType() != typeof(string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			for (int i = 0; i < NumericDataTypeConverter.types.Length; i++)
			{
				if (NumericDataTypeConverter.types[i].ToString().Equals(value))
				{
					return NumericDataTypeConverter.types[i];
				}
			}
			return typeof(string);
		}

		// Token: 0x0600F86E RID: 63598 RVA: 0x003824FC File Offset: 0x003806FC
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string))
			{
				if (value == null)
				{
					return string.Empty;
				}
				value.ToString();
			}
			if (value != null && destinationType == typeof(InstanceDescriptor))
			{
				object obj = value;
				if (value is string)
				{
					for (int i = 0; i < NumericDataTypeConverter.types.Length; i++)
					{
						if (NumericDataTypeConverter.types[i].ToString().Equals(value))
						{
							obj = NumericDataTypeConverter.types[i];
						}
					}
				}
				if (value is Type || value is string)
				{
					Type[] array = new Type[]
					{
						typeof(string)
					};
					MethodInfo method = typeof(Type).GetMethod("GetType", array);
					if (method != null)
					{
						object[] arguments = new object[]
						{
							((Type)obj).AssemblyQualifiedName
						};
						return new InstanceDescriptor(method, arguments);
					}
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x0600F86F RID: 63599 RVA: 0x0038260C File Offset: 0x0038080C
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.values == null)
			{
				object[] destinationArray;
				if (NumericDataTypeConverter.types != null)
				{
					destinationArray = new object[NumericDataTypeConverter.types.Length];
					Array.Copy(NumericDataTypeConverter.types, destinationArray, NumericDataTypeConverter.types.Length);
				}
				else
				{
					destinationArray = null;
				}
				this.values = new TypeConverter.StandardValuesCollection(destinationArray);
			}
			return this.values;
		}

		// Token: 0x0600F870 RID: 63600 RVA: 0x0038265D File Offset: 0x0038085D
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0600F871 RID: 63601 RVA: 0x00382660 File Offset: 0x00380860
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x040046C4 RID: 18116
		private static Type[] types = InputUtil.GetNumericSupportedTypes();

		// Token: 0x040046C5 RID: 18117
		private TypeConverter.StandardValuesCollection values;
	}
}
