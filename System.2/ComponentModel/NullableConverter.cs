using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000594 RID: 1428
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class NullableConverter : TypeConverter
	{
		// Token: 0x06003510 RID: 13584 RVA: 0x000E78F4 File Offset: 0x000E5AF4
		public NullableConverter(Type type)
		{
			this.nullableType = type;
			this.simpleType = Nullable.GetUnderlyingType(type);
			if (this.simpleType == null)
			{
				throw new ArgumentException(SR.GetString("NullableConverterBadCtorArg"), "type");
			}
			this.simpleTypeConverter = TypeDescriptor.GetConverter(this.simpleType);
		}

		// Token: 0x06003511 RID: 13585 RVA: 0x000E794E File Offset: 0x000E5B4E
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == this.simpleType)
			{
				return true;
			}
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.CanConvertFrom(context, sourceType);
			}
			return base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06003512 RID: 13586 RVA: 0x000E7980 File Offset: 0x000E5B80
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null || value.GetType() == this.simpleType)
			{
				return value;
			}
			if (value is string && string.IsNullOrEmpty(value as string))
			{
				return null;
			}
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.ConvertFrom(context, culture, value);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06003513 RID: 13587 RVA: 0x000E79E0 File Offset: 0x000E5BE0
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == this.simpleType)
			{
				return true;
			}
			if (destinationType == typeof(InstanceDescriptor))
			{
				return true;
			}
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.CanConvertTo(context, destinationType);
			}
			return base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x000E7A30 File Offset: 0x000E5C30
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == this.simpleType && this.nullableType.IsInstanceOfType(value))
			{
				return value;
			}
			if (destinationType == typeof(InstanceDescriptor))
			{
				ConstructorInfo constructor = this.nullableType.GetConstructor(new Type[]
				{
					this.simpleType
				});
				return new InstanceDescriptor(constructor, new object[]
				{
					value
				}, true);
			}
			if (value == null)
			{
				if (destinationType == typeof(string))
				{
					return string.Empty;
				}
			}
			else if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.ConvertTo(context, culture, value, destinationType);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06003515 RID: 13589 RVA: 0x000E7AF4 File Offset: 0x000E5CF4
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.CreateInstance(context, propertyValues);
			}
			return base.CreateInstance(context, propertyValues);
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x000E7B21 File Offset: 0x000E5D21
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.GetCreateInstanceSupported(context);
			}
			return base.GetCreateInstanceSupported(context);
		}

		// Token: 0x06003517 RID: 13591 RVA: 0x000E7B40 File Offset: 0x000E5D40
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.GetProperties(context, value, attributes);
			}
			return base.GetProperties(context, value, attributes);
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x000E7B6F File Offset: 0x000E5D6F
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.GetPropertiesSupported(context);
			}
			return base.GetPropertiesSupported(context);
		}

		// Token: 0x06003519 RID: 13593 RVA: 0x000E7B90 File Offset: 0x000E5D90
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.simpleTypeConverter != null)
			{
				TypeConverter.StandardValuesCollection standardValues = this.simpleTypeConverter.GetStandardValues(context);
				if (this.GetStandardValuesSupported(context) && standardValues != null)
				{
					object[] array = new object[standardValues.Count + 1];
					int num = 0;
					array[num++] = null;
					foreach (object obj in standardValues)
					{
						array[num++] = obj;
					}
					return new TypeConverter.StandardValuesCollection(array);
				}
			}
			return base.GetStandardValues(context);
		}

		// Token: 0x0600351A RID: 13594 RVA: 0x000E7C2C File Offset: 0x000E5E2C
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.GetStandardValuesExclusive(context);
			}
			return base.GetStandardValuesExclusive(context);
		}

		// Token: 0x0600351B RID: 13595 RVA: 0x000E7C4A File Offset: 0x000E5E4A
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.GetStandardValuesSupported(context);
			}
			return base.GetStandardValuesSupported(context);
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x000E7C68 File Offset: 0x000E5E68
		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			if (this.simpleTypeConverter != null)
			{
				return value == null || this.simpleTypeConverter.IsValid(context, value);
			}
			return base.IsValid(context, value);
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x0600351D RID: 13597 RVA: 0x000E7C9A File Offset: 0x000E5E9A
		public Type NullableType
		{
			get
			{
				return this.nullableType;
			}
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x0600351E RID: 13598 RVA: 0x000E7CA2 File Offset: 0x000E5EA2
		public Type UnderlyingType
		{
			get
			{
				return this.simpleType;
			}
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x0600351F RID: 13599 RVA: 0x000E7CAA File Offset: 0x000E5EAA
		public TypeConverter UnderlyingTypeConverter
		{
			get
			{
				return this.simpleTypeConverter;
			}
		}

		// Token: 0x04002A39 RID: 10809
		private Type nullableType;

		// Token: 0x04002A3A RID: 10810
		private Type simpleType;

		// Token: 0x04002A3B RID: 10811
		private TypeConverter simpleTypeConverter;
	}
}
