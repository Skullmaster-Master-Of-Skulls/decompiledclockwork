using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data.SqlTypes;
using System.Globalization;
using System.Reflection;

namespace System.Data
{
	// Token: 0x02000094 RID: 148
	internal sealed class ColumnTypeConverter : TypeConverter
	{
		// Token: 0x060007D0 RID: 2000 RVA: 0x000563A0 File Offset: 0x000557A0
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x000563CC File Offset: 0x000557CC
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

		// Token: 0x060007D2 RID: 2002 RVA: 0x000564CC File Offset: 0x000558CC
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertTo(context, sourceType);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x000564F8 File Offset: 0x000558F8
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

		// Token: 0x060007D4 RID: 2004 RVA: 0x00056564 File Offset: 0x00055964
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

		// Token: 0x060007D5 RID: 2005 RVA: 0x000565B8 File Offset: 0x000559B8
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x000565C8 File Offset: 0x000559C8
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x040002BE RID: 702
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

		// Token: 0x040002BF RID: 703
		private TypeConverter.StandardValuesCollection values;
	}
}
