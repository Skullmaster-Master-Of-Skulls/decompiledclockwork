using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.SearchBox
{
	// Token: 0x0200086B RID: 2155
	internal class SearchContextItemConverter : JavaScriptConverter
	{
		// Token: 0x06004F22 RID: 20258 RVA: 0x000F801F File Offset: 0x000F621F
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004F23 RID: 20259 RVA: 0x000F8028 File Offset: 0x000F6228
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			SearchContextItem searchContextItem = (SearchContextItem)obj;
			if (!string.IsNullOrEmpty(searchContextItem.Text))
			{
				dictionary.Add("text", searchContextItem.Text);
			}
			if (!string.IsNullOrEmpty(searchContextItem.Key))
			{
				dictionary.Add("key", searchContextItem.Key);
			}
			if (!string.IsNullOrEmpty(searchContextItem.ImageUrl))
			{
				dictionary.Add("imageUrl", searchContextItem.ResolveClientUrl(searchContextItem.ImageUrl));
			}
			if (searchContextItem.Selected)
			{
				dictionary.Add("selected", true);
			}
			return dictionary;
		}

		// Token: 0x170019D9 RID: 6617
		// (get) Token: 0x06004F24 RID: 20260 RVA: 0x000F8188 File Offset: 0x000F6388
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(SearchContextItem);
				yield break;
			}
		}
	}
}
