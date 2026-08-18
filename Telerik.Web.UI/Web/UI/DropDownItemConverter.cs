using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Telerik.Web.UI.AutoCompleteBox;

namespace Telerik.Web.UI
{
	// Token: 0x020009B3 RID: 2483
	internal class DropDownItemConverter : JavaScriptConverter
	{
		// Token: 0x06005F1A RID: 24346 RVA: 0x001222A8 File Offset: 0x001204A8
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005F1B RID: 24347 RVA: 0x001222B0 File Offset: 0x001204B0
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			DropDownItem dropDownItem = obj as DropDownItem;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (!string.IsNullOrEmpty(dropDownItem.Text))
			{
				dictionary.Add("text", dropDownItem.Text);
			}
			if (!string.IsNullOrEmpty(dropDownItem.Value))
			{
				dictionary.Add("value", dropDownItem.Value);
			}
			return dictionary;
		}

		// Token: 0x17001F62 RID: 8034
		// (get) Token: 0x06005F1C RID: 24348 RVA: 0x001223D4 File Offset: 0x001205D4
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
