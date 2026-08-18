using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Spreadsheet
{
	// Token: 0x0200089D RID: 2205
	internal class LocalizationConverter : JavaScriptConverter
	{
		// Token: 0x06005209 RID: 21001 RVA: 0x000FF7E3 File Offset: 0x000FD9E3
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600520A RID: 21002 RVA: 0x000FF7EC File Offset: 0x000FD9EC
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			SpreadsheetStrings spreadsheetStrings = (SpreadsheetStrings)obj;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
			Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
			Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
			foreach (PropertyInfo propertyInfo in spreadsheetStrings.GetType().GetProperties())
			{
				string text = (string)propertyInfo.GetValue(spreadsheetStrings, null);
				DefaultValueAttribute defaultValueAttribute = propertyInfo.GetCustomAttributes(typeof(DefaultValueAttribute), false).First<object>() as DefaultValueAttribute;
				CategoryAttribute categoryAttribute = propertyInfo.GetCustomAttributes(typeof(CategoryAttribute), false).First<object>() as CategoryAttribute;
				if (!string.Equals(defaultValueAttribute.Value.ToString(), text, StringComparison.InvariantCulture))
				{
					object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(ClientPropertyNameAttribute), false);
					if (customAttributes.Count<object>() > 0)
					{
						ClientPropertyNameAttribute clientPropertyNameAttribute = customAttributes.First<object>() as ClientPropertyNameAttribute;
						if (categoryAttribute.Category == "MessageDialog")
						{
							dictionary2.Add(clientPropertyNameAttribute.PropertyName, text);
						}
						else if (categoryAttribute.Category == "Hyperlink")
						{
							dictionary3.Add(clientPropertyNameAttribute.PropertyName, text);
						}
						else if (categoryAttribute.Category == "Validation")
						{
							dictionary4.Add(clientPropertyNameAttribute.PropertyName, text);
						}
						else if (categoryAttribute.Category == "ConfirmationDialog")
						{
							dictionary5.Add(clientPropertyNameAttribute.PropertyName, text);
						}
					}
				}
			}
			if (dictionary2.Count > 0)
			{
				dictionary.Add("messageDialog", dictionary2);
			}
			if (dictionary3.Count > 0)
			{
				dictionary.Add("hyperlinkDialog", dictionary3);
			}
			if (dictionary4.Count > 0)
			{
				dictionary.Add("valiationDialog", dictionary4);
			}
			if (dictionary5.Count > 0)
			{
				dictionary.Add("confirmationDialog", dictionary5);
			}
			return dictionary;
		}

		// Token: 0x17001ADF RID: 6879
		// (get) Token: 0x0600520B RID: 21003 RVA: 0x000FF9CC File Offset: 0x000FDBCC
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(SpreadsheetStrings)
				};
			}
		}
	}
}
