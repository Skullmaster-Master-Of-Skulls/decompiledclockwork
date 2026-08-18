using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200045C RID: 1116
	internal class DropDownTreeEntryConverter : JavaScriptConverter
	{
		// Token: 0x0600285C RID: 10332 RVA: 0x00082F93 File Offset: 0x00081193
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			return null;
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x00082F98 File Offset: 0x00081198
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			DropDownTreeEntry dropDownTreeEntry = obj as DropDownTreeEntry;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("text", dropDownTreeEntry.Text);
			if (!string.IsNullOrEmpty(dropDownTreeEntry.Value))
			{
				dictionary.Add("value", dropDownTreeEntry.Value);
			}
			if (!string.IsNullOrEmpty(dropDownTreeEntry.FullPath))
			{
				dictionary.Add("fullPath", dropDownTreeEntry.FullPath);
			}
			return dictionary;
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x0600285E RID: 10334 RVA: 0x000830CC File Offset: 0x000812CC
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(DropDownTreeEntry);
				yield break;
			}
		}
	}
}
