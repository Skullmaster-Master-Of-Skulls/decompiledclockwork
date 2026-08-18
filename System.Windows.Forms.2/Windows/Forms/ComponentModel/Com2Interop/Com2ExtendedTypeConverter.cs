using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x02000499 RID: 1177
	internal class Com2ExtendedTypeConverter : TypeConverter
	{
		// Token: 0x06004E84 RID: 20100 RVA: 0x00143393 File Offset: 0x00141593
		public Com2ExtendedTypeConverter(TypeConverter innerConverter)
		{
			this.innerConverter = innerConverter;
		}

		// Token: 0x06004E85 RID: 20101 RVA: 0x001433A2 File Offset: 0x001415A2
		public Com2ExtendedTypeConverter(Type baseType)
		{
			this.innerConverter = TypeDescriptor.GetConverter(baseType);
		}

		// Token: 0x1700134B RID: 4939
		// (get) Token: 0x06004E86 RID: 20102 RVA: 0x001433B6 File Offset: 0x001415B6
		public TypeConverter InnerConverter
		{
			get
			{
				return this.innerConverter;
			}
		}

		// Token: 0x06004E87 RID: 20103 RVA: 0x001433C0 File Offset: 0x001415C0
		public TypeConverter GetWrappedConverter(Type t)
		{
			for (TypeConverter typeConverter = this.innerConverter; typeConverter != null; typeConverter = ((Com2ExtendedTypeConverter)typeConverter).InnerConverter)
			{
				if (t.IsInstanceOfType(typeConverter))
				{
					return typeConverter;
				}
				if (!(typeConverter is Com2ExtendedTypeConverter))
				{
					break;
				}
			}
			return null;
		}

		// Token: 0x06004E88 RID: 20104 RVA: 0x001433F9 File Offset: 0x001415F9
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.CanConvertFrom(context, sourceType);
			}
			return base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06004E89 RID: 20105 RVA: 0x00143419 File Offset: 0x00141619
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.CanConvertTo(context, destinationType);
			}
			return base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06004E8A RID: 20106 RVA: 0x00143439 File Offset: 0x00141639
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.ConvertFrom(context, culture, value);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06004E8B RID: 20107 RVA: 0x0014345B File Offset: 0x0014165B
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.ConvertTo(context, culture, value, destinationType);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06004E8C RID: 20108 RVA: 0x00143481 File Offset: 0x00141681
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.CreateInstance(context, propertyValues);
			}
			return base.CreateInstance(context, propertyValues);
		}

		// Token: 0x06004E8D RID: 20109 RVA: 0x001434A1 File Offset: 0x001416A1
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.GetCreateInstanceSupported(context);
			}
			return base.GetCreateInstanceSupported(context);
		}

		// Token: 0x06004E8E RID: 20110 RVA: 0x001434BF File Offset: 0x001416BF
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.GetProperties(context, value, attributes);
			}
			return base.GetProperties(context, value, attributes);
		}

		// Token: 0x06004E8F RID: 20111 RVA: 0x001434E1 File Offset: 0x001416E1
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.GetPropertiesSupported(context);
			}
			return base.GetPropertiesSupported(context);
		}

		// Token: 0x06004E90 RID: 20112 RVA: 0x001434FF File Offset: 0x001416FF
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.GetStandardValues(context);
			}
			return base.GetStandardValues(context);
		}

		// Token: 0x06004E91 RID: 20113 RVA: 0x0014351D File Offset: 0x0014171D
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.GetStandardValuesExclusive(context);
			}
			return base.GetStandardValuesExclusive(context);
		}

		// Token: 0x06004E92 RID: 20114 RVA: 0x0014353B File Offset: 0x0014173B
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.GetStandardValuesSupported(context);
			}
			return base.GetStandardValuesSupported(context);
		}

		// Token: 0x06004E93 RID: 20115 RVA: 0x00143559 File Offset: 0x00141759
		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.IsValid(context, value);
			}
			return base.IsValid(context, value);
		}

		// Token: 0x04003412 RID: 13330
		private TypeConverter innerConverter;
	}
}
