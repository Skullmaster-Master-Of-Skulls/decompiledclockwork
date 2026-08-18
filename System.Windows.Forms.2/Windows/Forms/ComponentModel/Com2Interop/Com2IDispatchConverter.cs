using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x0200049C RID: 1180
	internal class Com2IDispatchConverter : Com2ExtendedTypeConverter
	{
		// Token: 0x06004E9E RID: 20126 RVA: 0x00143878 File Offset: 0x00141A78
		public Com2IDispatchConverter(Com2PropertyDescriptor propDesc, bool allowExpand, TypeConverter baseConverter) : base(baseConverter)
		{
			this.propDesc = propDesc;
			this.allowExpand = allowExpand;
		}

		// Token: 0x06004E9F RID: 20127 RVA: 0x0014388F File Offset: 0x00141A8F
		public Com2IDispatchConverter(Com2PropertyDescriptor propDesc, bool allowExpand) : base(propDesc.PropertyType)
		{
			this.propDesc = propDesc;
			this.allowExpand = allowExpand;
		}

		// Token: 0x06004EA0 RID: 20128 RVA: 0x00011A20 File Offset: 0x0000FC20
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return false;
		}

		// Token: 0x06004EA1 RID: 20129 RVA: 0x001438AB File Offset: 0x00141AAB
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string);
		}

		// Token: 0x06004EA2 RID: 20130 RVA: 0x001438C0 File Offset: 0x00141AC0
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (!(destinationType == typeof(string)))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (value == null)
			{
				return Com2IDispatchConverter.none;
			}
			string text = ComNativeDescriptor.Instance.GetName(value);
			if (text == null || text.Length == 0)
			{
				text = ComNativeDescriptor.Instance.GetClassName(value);
			}
			if (text == null)
			{
				return "(Object)";
			}
			return text;
		}

		// Token: 0x06004EA3 RID: 20131 RVA: 0x00143922 File Offset: 0x00141B22
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(value, attributes);
		}

		// Token: 0x06004EA4 RID: 20132 RVA: 0x0014392B File Offset: 0x00141B2B
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return this.allowExpand;
		}

		// Token: 0x06004EA5 RID: 20133 RVA: 0x00011A20 File Offset: 0x0000FC20
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x04003415 RID: 13333
		private Com2PropertyDescriptor propDesc;

		// Token: 0x04003416 RID: 13334
		protected static readonly string none = SR.GetString("toStringNone");

		// Token: 0x04003417 RID: 13335
		private bool allowExpand;
	}
}
