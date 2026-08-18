using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200054E RID: 1358
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class EnumConverter : TypeConverter
	{
		// Token: 0x060032FE RID: 13054 RVA: 0x000E2F19 File Offset: 0x000E1119
		public EnumConverter(Type type)
		{
			this.type = type;
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x060032FF RID: 13055 RVA: 0x000E2F28 File Offset: 0x000E1128
		protected Type EnumType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06003300 RID: 13056 RVA: 0x000E2F30 File Offset: 0x000E1130
		// (set) Token: 0x06003301 RID: 13057 RVA: 0x000E2F38 File Offset: 0x000E1138
		protected TypeConverter.StandardValuesCollection Values
		{
			get
			{
				return this.values;
			}
			set
			{
				this.values = value;
			}
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x000E2F41 File Offset: 0x000E1141
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || sourceType == typeof(Enum[]) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06003303 RID: 13059 RVA: 0x000E2F71 File Offset: 0x000E1171
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || destinationType == typeof(Enum[]) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06003304 RID: 13060 RVA: 0x000E2FA1 File Offset: 0x000E11A1
		protected virtual IComparer Comparer
		{
			get
			{
				return InvariantComparer.Default;
			}
		}

		// Token: 0x06003305 RID: 13061 RVA: 0x000E2FA8 File Offset: 0x000E11A8
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				try
				{
					string text = (string)value;
					if (text.IndexOf(',') != -1)
					{
						long num = 0L;
						string[] array = text.Split(new char[]
						{
							','
						});
						foreach (string value2 in array)
						{
							num |= Convert.ToInt64((Enum)Enum.Parse(this.type, value2, true), culture);
						}
						return Enum.ToObject(this.type, num);
					}
					return Enum.Parse(this.type, text, true);
				}
				catch (Exception innerException)
				{
					throw new FormatException(SR.GetString("ConvertInvalidPrimitive", new object[]
					{
						(string)value,
						this.type.Name
					}), innerException);
				}
			}
			if (value is Enum[])
			{
				long num2 = 0L;
				foreach (Enum value3 in (Enum[])value)
				{
					num2 |= Convert.ToInt64(value3, culture);
				}
				return Enum.ToObject(this.type, num2);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x000E30DC File Offset: 0x000E12DC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value != null)
			{
				Type underlyingType = Enum.GetUnderlyingType(this.type);
				if (value is IConvertible && value.GetType() != underlyingType)
				{
					value = ((IConvertible)value).ToType(underlyingType, culture);
				}
				if (!this.type.IsDefined(typeof(FlagsAttribute), false) && !Enum.IsDefined(this.type, value))
				{
					throw new ArgumentException(SR.GetString("EnumConverterInvalidValue", new object[]
					{
						value.ToString(),
						this.type.Name
					}));
				}
				return Enum.Format(this.type, value, "G");
			}
			else
			{
				if (destinationType == typeof(InstanceDescriptor) && value != null)
				{
					string text = base.ConvertToInvariantString(context, value);
					if (this.type.IsDefined(typeof(FlagsAttribute), false) && text.IndexOf(',') != -1)
					{
						Type underlyingType2 = Enum.GetUnderlyingType(this.type);
						if (value is IConvertible)
						{
							object obj = ((IConvertible)value).ToType(underlyingType2, culture);
							MethodInfo method = typeof(Enum).GetMethod("ToObject", new Type[]
							{
								typeof(Type),
								underlyingType2
							});
							if (method != null)
							{
								return new InstanceDescriptor(method, new object[]
								{
									this.type,
									obj
								});
							}
						}
					}
					else
					{
						FieldInfo field = this.type.GetField(text);
						if (field != null)
						{
							return new InstanceDescriptor(field, null);
						}
					}
				}
				if (!(destinationType == typeof(Enum[])) || value == null)
				{
					return base.ConvertTo(context, culture, value, destinationType);
				}
				if (this.type.IsDefined(typeof(FlagsAttribute), false))
				{
					List<Enum> list = new List<Enum>();
					Array array = Enum.GetValues(this.type);
					long[] array2 = new long[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array2[i] = Convert.ToInt64((Enum)array.GetValue(i), culture);
					}
					long num = Convert.ToInt64((Enum)value, culture);
					bool flag = true;
					while (flag)
					{
						flag = false;
						foreach (long num2 in array2)
						{
							if ((num2 != 0L && (num2 & num) == num2) || num2 == num)
							{
								list.Add((Enum)Enum.ToObject(this.type, num2));
								flag = true;
								num &= ~num2;
								break;
							}
						}
						if (num == 0L)
						{
							break;
						}
					}
					if (!flag && num != 0L)
					{
						list.Add((Enum)Enum.ToObject(this.type, num));
					}
					return list.ToArray();
				}
				return new Enum[]
				{
					(Enum)Enum.ToObject(this.type, value)
				};
			}
		}

		// Token: 0x06003307 RID: 13063 RVA: 0x000E33E0 File Offset: 0x000E15E0
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.values == null)
			{
				Type reflectionType = TypeDescriptor.GetReflectionType(this.type);
				if (reflectionType == null)
				{
					reflectionType = this.type;
				}
				FieldInfo[] fields = reflectionType.GetFields(BindingFlags.Static | BindingFlags.Public);
				ArrayList arrayList = null;
				if (fields != null && fields.Length != 0)
				{
					arrayList = new ArrayList(fields.Length);
				}
				if (arrayList != null)
				{
					foreach (FieldInfo fieldInfo in fields)
					{
						BrowsableAttribute browsableAttribute = null;
						foreach (Attribute attribute in fieldInfo.GetCustomAttributes(typeof(BrowsableAttribute), false))
						{
							browsableAttribute = (attribute as BrowsableAttribute);
						}
						if (browsableAttribute == null || browsableAttribute.Browsable)
						{
							object obj = null;
							try
							{
								if (fieldInfo.Name != null)
								{
									obj = Enum.Parse(this.type, fieldInfo.Name);
								}
							}
							catch (ArgumentException)
							{
							}
							if (obj != null)
							{
								arrayList.Add(obj);
							}
						}
					}
					IComparer comparer = this.Comparer;
					if (comparer != null)
					{
						arrayList.Sort(comparer);
					}
				}
				Array array2 = (arrayList != null) ? arrayList.ToArray() : null;
				this.values = new TypeConverter.StandardValuesCollection(array2);
			}
			return this.values;
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x000E351C File Offset: 0x000E171C
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return !this.type.IsDefined(typeof(FlagsAttribute), false);
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x000E3537 File Offset: 0x000E1737
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x000E353A File Offset: 0x000E173A
		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			return Enum.IsDefined(this.type, value);
		}

		// Token: 0x040029AF RID: 10671
		private TypeConverter.StandardValuesCollection values;

		// Token: 0x040029B0 RID: 10672
		private Type type;
	}
}
