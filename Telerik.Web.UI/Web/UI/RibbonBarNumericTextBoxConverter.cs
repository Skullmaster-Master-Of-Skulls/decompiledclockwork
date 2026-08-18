using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000E35 RID: 3637
	internal class RibbonBarNumericTextBoxConverter : JavaScriptConverter
	{
		// Token: 0x06008994 RID: 35220 RVA: 0x001F5EF9 File Offset: 0x001F40F9
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008995 RID: 35221 RVA: 0x001F5F00 File Offset: 0x001F4100
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			RibbonBarNumericTextBox ribbonBarNumericTextBox = (RibbonBarNumericTextBox)obj;
			if (!string.IsNullOrEmpty(ribbonBarNumericTextBox.Text))
			{
				dictionary["value"] = ribbonBarNumericTextBox.Text;
			}
			if (!string.IsNullOrEmpty(ribbonBarNumericTextBox.Prefix))
			{
				dictionary["prefix"] = ribbonBarNumericTextBox.Prefix;
			}
			if (!string.IsNullOrEmpty(ribbonBarNumericTextBox.Suffix))
			{
				dictionary["suffix"] = ribbonBarNumericTextBox.Suffix;
			}
			if (!string.IsNullOrEmpty(ribbonBarNumericTextBox.ToolTip))
			{
				dictionary["toolTip"] = ribbonBarNumericTextBox.ToolTip;
			}
			dictionary["step"] = ribbonBarNumericTextBox.Step.ToString();
			return dictionary;
		}

		// Token: 0x17002B8B RID: 11147
		// (get) Token: 0x06008996 RID: 35222 RVA: 0x001F6078 File Offset: 0x001F4278
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RibbonBarNumericTextBox);
				yield break;
			}
		}
	}
}
