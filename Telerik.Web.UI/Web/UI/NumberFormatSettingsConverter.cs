using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020012C0 RID: 4800
	internal class NumberFormatSettingsConverter : JavaScriptConverter
	{
		// Token: 0x0600C941 RID: 51521 RVA: 0x002CDE58 File Offset: 0x002CC058
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		// Token: 0x0600C942 RID: 51522 RVA: 0x002CDE64 File Offset: 0x002CC064
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			NumberFormatSettings numberFormatSettings = obj as NumberFormatSettings;
			if (numberFormatSettings == null)
			{
				throw new ArgumentException("Can serialize only NumberFormatSettings objects.");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("DecimalDigits", numberFormatSettings.DecimalDigits);
			dictionary.Add("DecimalSeparator", numberFormatSettings.DecimalSeparator);
			dictionary.Add("CultureNativeDecimalSeparator", numberFormatSettings.CultureNativeDecimalSeparator);
			dictionary.Add("GroupSeparator", numberFormatSettings.GroupSeparator);
			dictionary.Add("GroupSizes", numberFormatSettings.GroupSizes);
			dictionary.Add("NegativePattern", numberFormatSettings.NegativePattern);
			dictionary.Add("NegativeSign", numberFormatSettings.NegativeSign);
			dictionary.Add("PositivePattern", numberFormatSettings.PositivePattern);
			if (numberFormatSettings.ZeroPattern != numberFormatSettings.PositivePattern)
			{
				dictionary.Add("ZeroPattern", numberFormatSettings.ZeroPattern);
			}
			dictionary.Add("AllowRounding", numberFormatSettings.AllowRounding);
			dictionary.Add("KeepNotRoundedValue", numberFormatSettings.KeepNotRoundedValue);
			dictionary.Add("KeepTrailingZerosOnFocus", numberFormatSettings.KeepTrailingZerosOnFocus);
			dictionary.Add("NumericPlaceHolder", numberFormatSettings.NumericPlaceHolder);
			return dictionary;
		}

		// Token: 0x17004115 RID: 16661
		// (get) Token: 0x0600C943 RID: 51523 RVA: 0x002CDF98 File Offset: 0x002CC198
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(NumberFormatSettings)
				};
			}
		}
	}
}
