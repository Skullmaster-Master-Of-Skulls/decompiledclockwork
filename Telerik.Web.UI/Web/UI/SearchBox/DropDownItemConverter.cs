using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.SearchBox
{
	// Token: 0x02000EE6 RID: 3814
	internal class DropDownItemConverter : JavaScriptConverter
	{
		// Token: 0x060090CC RID: 37068 RVA: 0x00209C5F File Offset: 0x00207E5F
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060090CD RID: 37069 RVA: 0x00209C68 File Offset: 0x00207E68
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			DropDownItem dropDownItem = (DropDownItem)obj;
			if (!string.IsNullOrEmpty(dropDownItem.Value))
			{
				dictionary.Add("value", dropDownItem.Value);
			}
			if (!string.IsNullOrEmpty(dropDownItem.DisplayText))
			{
				dictionary.Add("text", dropDownItem.DisplayText);
			}
			if (dropDownItem._DataItem.Keys.Count > 0)
			{
				dictionary.Add("dataItem", dropDownItem._DataItem);
			}
			return dictionary;
		}

		// Token: 0x17002DDB RID: 11739
		// (get) Token: 0x060090CE RID: 37070 RVA: 0x00209DB0 File Offset: 0x00207FB0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(DropDownItem);
				yield break;
			}
		}
	}
}
