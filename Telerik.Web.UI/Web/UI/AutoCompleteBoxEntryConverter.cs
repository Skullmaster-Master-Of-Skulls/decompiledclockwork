using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020009AD RID: 2477
	internal class AutoCompleteBoxEntryConverter : JavaScriptConverter
	{
		// Token: 0x06005EFF RID: 24319 RVA: 0x00121EF4 File Offset: 0x001200F4
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005F00 RID: 24320 RVA: 0x00121EFC File Offset: 0x001200FC
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			AutoCompleteBoxEntry autoCompleteBoxEntry = obj as AutoCompleteBoxEntry;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("text", autoCompleteBoxEntry.Text);
			if (!string.IsNullOrEmpty(autoCompleteBoxEntry.Value))
			{
				dictionary.Add("value", autoCompleteBoxEntry.Value);
			}
			if (autoCompleteBoxEntry.Attributes.Count > 0)
			{
				dictionary.Add("attributes", autoCompleteBoxEntry.Attributes);
			}
			return dictionary;
		}

		// Token: 0x17001F5A RID: 8026
		// (get) Token: 0x06005F01 RID: 24321 RVA: 0x00122034 File Offset: 0x00120234
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(AutoCompleteBoxEntry);
				yield break;
			}
		}
	}
}
