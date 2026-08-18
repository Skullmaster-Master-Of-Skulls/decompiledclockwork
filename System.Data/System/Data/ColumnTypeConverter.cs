using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data.SqlTypes;
using System.Globalization;
using System.Reflection;

namespace System.Data
{
	// Token: 0x0200005C RID: 92
	internal sealed class ColumnTypeConverter : TypeConverter
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x001E76A8 File Offset: 0x001E6AA8
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x001E76D8 File Offset: 0x001E6AD8
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
					for (int i = 0; i < ColumnTypeConverter.types.Length; i++)
					{
						if (ColumnTypeConverter.types[i].ToString().Equals(value))
						{
							obj = ColumnTypeConverter.types[i];
						}
					}
				}
				if (value is Type || value is string)
				{
					MethodInfo method = typeof(Type).GetMethod("GetType", new Type[]
					{
						typeof(string)
					});
					if (method != null)
					{
						return new InstanceDescriptor(method, new object[]
						{
							((Type)obj).AssemblyQualifiedName
						});
					}
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x001E77D8 File Offset: 0x001E6BD8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertTo(context, sourceType);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x001E7808 File Offset: 0x001E6C08
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value != null && value.GetType() == typeof(string))
			{
				for (int i = 0; i < ColumnTypeConverter.types.Length; i++)
				{
					if (ColumnTypeConverter.types[i].ToString().Equals(value))
					{
						return ColumnTypeConverter.types[i];
					}
				}
				return typeof(string);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x001E7878 File Offset: 0x001E6C78
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.values == null)
			{
				object[] destinationArray;
				if (ColumnTypeConverter.types != null)
				{
					destinationArray = new object[ColumnTypeConverter.types.Length];
					Array.Copy(ColumnTypeConverter.types, destinationArray, ColumnTypeConverter.types.Length);
				}
				else
				{
					destinationArray = null;
				}
				this.values = new TypeConverter.StandardValuesCollection(destinationArray);
			}
			return this.values;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x001E78D8 File Offset: 0x001E6CD8
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x001E78E8 File Offset: 0x001E6CE8
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x040006BA RID: 1722
		private static Type[] types = new Type[]
		{
			typeof(bool),
			typeof(byte),
			typeof(byte[]),
			typeof(char),
			typeof(DateTime),
			typeof(decimal),
			typeof(double),
			typeof(Guid),
			typeof(short),
			typeof(int),
			typeof(long),
			typeof(object),
			typeof(sbyte),
			typeof(float),
			typeof(string),
			typeof(TimeSpan),
			typeof(ushort),
			typeof(uint),
			typeof(ulong),
			typeof(SqlInt16),
			typeof(SqlInt32),
			typeof(SqlInt64),
			typeof(SqlDecimal),
			typeof(SqlSingle),
			typeof(SqlDouble),
			typeof(SqlString),
			typeof(SqlBoolean),
			typeof(SqlBinary),
			typeof(SqlByte),
			typeof(SqlDateTime),
			typeof(SqlGuid),
			typeof(SqlMoney),
			typeof(SqlBytes),
			typeof(SqlChars),
			typeof(SqlXml)
		};

		// Token: 0x040006BB RID: 1723
		private TypeConverter.StandardValuesCollection values;
	}
}
