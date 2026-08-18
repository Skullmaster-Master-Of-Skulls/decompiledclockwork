using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace Telerik.Web.UI
{
	// Token: 0x020010AC RID: 4268
	internal class GridDataTypeConverter : TypeConverter
	{
		// Token: 0x0600ADED RID: 44525 RVA: 0x00257D08 File Offset: 0x00255F08
		static GridDataTypeConverter()
		{
			Type[] array = new Type[]
			{
				typeof(bool),
				typeof(byte),
				typeof(char),
				typeof(DateTime),
				typeof(decimal),
				typeof(double),
				typeof(short),
				typeof(int),
				typeof(long),
				typeof(sbyte),
				typeof(float),
				typeof(string),
				typeof(TimeSpan),
				typeof(ushort),
				typeof(uint),
				typeof(ulong),
				typeof(Guid)
			};
			GridDataTypeConverter.types = array;
		}

		// Token: 0x1700383A RID: 14394
		// (get) Token: 0x0600ADEE RID: 44526 RVA: 0x00257E0A File Offset: 0x0025600A
		internal static ArrayList SupportedTypes
		{
			get
			{
				return new ArrayList(GridDataTypeConverter.types);
			}
		}

		// Token: 0x0600ADEF RID: 44527 RVA: 0x00257E16 File Offset: 0x00256016
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertTo(context, sourceType);
		}

		// Token: 0x0600ADF0 RID: 44528 RVA: 0x00257E34 File Offset: 0x00256034
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600ADF1 RID: 44529 RVA: 0x00257E54 File Offset: 0x00256054
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null || value.GetType() != typeof(string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			for (int i = 0; i < GridDataTypeConverter.types.Length; i++)
			{
				if (GridDataTypeConverter.types[i].ToString().Equals(value.ToString(), StringComparison.InvariantCultureIgnoreCase))
				{
					return GridDataTypeConverter.types[i];
				}
			}
			return typeof(string);
		}

		// Token: 0x0600ADF2 RID: 44530 RVA: 0x00257EC4 File Offset: 0x002560C4
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
					for (int i = 0; i < GridDataTypeConverter.types.Length; i++)
					{
						if (GridDataTypeConverter.types[i].ToString().Equals(value.ToString(), StringComparison.InvariantCultureIgnoreCase))
						{
							obj = GridDataTypeConverter.types[i];
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
						object[] array2 = new object[]
						{
							((Type)obj).AssemblyQualifiedName
						};
						return Activator.CreateInstance(typeof(InstanceDescriptor), new object[]
						{
							method,
							array2
						});
					}
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x0600ADF3 RID: 44531 RVA: 0x00257FF8 File Offset: 0x002561F8
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.values == null)
			{
				object[] destinationArray;
				if (GridDataTypeConverter.types != null)
				{
					destinationArray = new object[GridDataTypeConverter.types.Length];
					Array.Copy(GridDataTypeConverter.types, destinationArray, GridDataTypeConverter.types.Length);
				}
				else
				{
					destinationArray = null;
				}
				this.values = new TypeConverter.StandardValuesCollection(destinationArray);
			}
			return this.values;
		}

		// Token: 0x0600ADF4 RID: 44532 RVA: 0x00258049 File Offset: 0x00256249
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0600ADF5 RID: 44533 RVA: 0x0025804C File Offset: 0x0025624C
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x04002DF1 RID: 11761
		private static Type[] types;

		// Token: 0x04002DF2 RID: 11762
		private TypeConverter.StandardValuesCollection values;
	}
}
