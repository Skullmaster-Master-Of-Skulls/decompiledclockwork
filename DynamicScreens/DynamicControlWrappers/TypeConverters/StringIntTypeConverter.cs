using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace DynamicScreens.DynamicControlWrappers.TypeConverters
{
	// Token: 0x0200005C RID: 92
	public class StringIntTypeConverter : TypeConverter
	{
		// Token: 0x060004E0 RID: 1248 RVA: 0x000408F7 File Offset: 0x0003F8F7
		public StringIntTypeConverter()
		{
			this.values = new ArrayList();
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00040914 File Offset: 0x0003F914
		private void SetValues(ITypeDescriptorContext context)
		{
			if (context.Instance is DynamicControlWrapper_Base)
			{
				DynamicControlWrapper_Base dynamicControlWrapper_Base = (DynamicControlWrapper_Base)context.Instance;
				this.helperClass = (DynamicControlWrapper_HelperClass)dynamicControlWrapper_Base.dynamicControl.Tag;
				foreach (object obj in this.helperClass.ListGroups)
				{
					DynamicListGroup dynamicListGroup = (DynamicListGroup)obj;
					StringIntValue value = new StringIntValue(dynamicListGroup.LookupGroupId, dynamicListGroup.Description);
					this.values.Add(value);
				}
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000409DC File Offset: 0x0003F9DC
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x000409FC File Offset: 0x0003F9FC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			DynamicControl dynamicControl;
			if (context.Instance is object[])
			{
				object[] array = (object[])context.Instance;
				dynamicControl = ((DynamicControlWrapper_Base)array[0]).dynamicControl;
			}
			else
			{
				dynamicControl = ((DynamicControlWrapper_Base)context.Instance).dynamicControl;
			}
			if (value is string)
			{
				string text = (string)value;
				if (text.Length < 1)
				{
					return null;
				}
				foreach (object obj in this.values)
				{
					StringIntValue stringIntValue = (StringIntValue)obj;
					if (stringIntValue.Equals(text))
					{
						if (dynamicControl.IsComboBox)
						{
							dynamicControl.Setting1 = stringIntValue.IntValue;
						}
						else
						{
							dynamicControl.Setting1 = stringIntValue.IntValue;
						}
					}
				}
			}
			return dynamicControl;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00040B2C File Offset: 0x0003FB2C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00040B4C File Offset: 0x0003FB4C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			object result;
			if (value is StringIntValue)
			{
				StringIntValue stringIntValue = (StringIntValue)value;
				result = stringIntValue.ToString();
			}
			else if (context.Instance is object[])
			{
				object[] array = (object[])context.Instance;
				if (array.Length > 0)
				{
					object obj = array[0];
					DynamicControl dynamicControl = ((DynamicControlWrapper_Base)obj).dynamicControl;
					result = this.ConvertTo2(dynamicControl);
				}
				else
				{
					result = "{none}";
				}
			}
			else
			{
				DynamicControl dynamicControl = ((DynamicControlWrapper_Base)context.Instance).dynamicControl;
				result = this.ConvertTo2(dynamicControl);
			}
			return result;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00040BF8 File Offset: 0x0003FBF8
		private object ConvertTo2(DynamicControl dc)
		{
			int setting;
			if (dc.IsComboBox)
			{
				setting = dc.Setting1;
			}
			else
			{
				setting = dc.Setting1;
			}
			object result;
			if (this.helperClass != null)
			{
				DynamicListGroup dynamicListGroup = this.helperClass.ListGroups.FindListGroup(setting);
				if (dynamicListGroup != null)
				{
					result = dynamicListGroup.Description;
				}
				else
				{
					result = "{none}";
				}
			}
			else
			{
				result = "{not initialized}";
			}
			return result;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00040C68 File Offset: 0x0003FC68
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00040C7C File Offset: 0x0003FC7C
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.values.Count < 1)
			{
				this.SetValues(context);
			}
			return new TypeConverter.StandardValuesCollection(this.values);
		}

		// Token: 0x04000370 RID: 880
		private ArrayList values;

		// Token: 0x04000371 RID: 881
		private DynamicControlWrapper_HelperClass helperClass = null;
	}
}
