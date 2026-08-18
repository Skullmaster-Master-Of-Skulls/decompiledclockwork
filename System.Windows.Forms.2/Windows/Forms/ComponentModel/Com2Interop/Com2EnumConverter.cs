using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x02000497 RID: 1175
	internal class Com2EnumConverter : TypeConverter
	{
		// Token: 0x06004E76 RID: 20086 RVA: 0x00143254 File Offset: 0x00141454
		public Com2EnumConverter(Com2Enum enumObj)
		{
			this.com2Enum = enumObj;
		}

		// Token: 0x06004E77 RID: 20087 RVA: 0x000C24B8 File Offset: 0x000C06B8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06004E78 RID: 20088 RVA: 0x00143263 File Offset: 0x00141463
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destType)
		{
			return base.CanConvertTo(context, destType) || destType.IsEnum;
		}

		// Token: 0x06004E79 RID: 20089 RVA: 0x00143277 File Offset: 0x00141477
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return this.com2Enum.FromString((string)value);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06004E7A RID: 20090 RVA: 0x0014329C File Offset: 0x0014149C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value != null)
			{
				string text = this.com2Enum.ToString(value);
				if (text != null)
				{
					return text;
				}
				return "";
			}
			else
			{
				if (destinationType.IsEnum)
				{
					return Enum.ToObject(destinationType, value);
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}

		// Token: 0x06004E7B RID: 20091 RVA: 0x0014330C File Offset: 0x0014150C
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.values == null)
			{
				object[] array = this.com2Enum.Values;
				if (array != null)
				{
					this.values = new TypeConverter.StandardValuesCollection(array);
				}
			}
			return this.values;
		}

		// Token: 0x06004E7C RID: 20092 RVA: 0x00143342 File Offset: 0x00141542
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return this.com2Enum.IsStrictEnum;
		}

		// Token: 0x06004E7D RID: 20093 RVA: 0x00013062 File Offset: 0x00011262
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06004E7E RID: 20094 RVA: 0x00143350 File Offset: 0x00141550
		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			string text = this.com2Enum.ToString(value);
			return text != null && text.Length > 0;
		}

		// Token: 0x06004E7F RID: 20095 RVA: 0x00143378 File Offset: 0x00141578
		public void RefreshValues()
		{
			this.values = null;
		}

		// Token: 0x04003410 RID: 13328
		internal readonly Com2Enum com2Enum;

		// Token: 0x04003411 RID: 13329
		private TypeConverter.StandardValuesCollection values;
	}
}
