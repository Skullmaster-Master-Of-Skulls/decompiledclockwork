using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000EE7 RID: 3815
	internal class SearchBoxButtonConverter : JavaScriptConverter
	{
		// Token: 0x060090D0 RID: 37072 RVA: 0x00209DD5 File Offset: 0x00207FD5
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060090D1 RID: 37073 RVA: 0x00209DDC File Offset: 0x00207FDC
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			SearchBoxButton searchBoxButton = (SearchBoxButton)obj;
			if (!string.IsNullOrEmpty(searchBoxButton.ImageUrl))
			{
				dictionary.Add("imageUrl", searchBoxButton.ResolveClientUrl(searchBoxButton.ImageUrl));
			}
			if (!string.IsNullOrEmpty(searchBoxButton.CssClass))
			{
				dictionary.Add("cssClass", searchBoxButton.CssClass);
			}
			if (!string.IsNullOrEmpty(searchBoxButton.CommandName))
			{
				dictionary.Add("commandName", searchBoxButton.CommandName);
			}
			if (!string.IsNullOrEmpty(searchBoxButton.CommandArgument))
			{
				dictionary.Add("commandArgument", searchBoxButton.CommandArgument);
			}
			dictionary.Add("position", searchBoxButton.Position);
			return dictionary;
		}

		// Token: 0x17002DDC RID: 11740
		// (get) Token: 0x060090D2 RID: 37074 RVA: 0x00209F58 File Offset: 0x00208158
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(SearchBoxButton);
				yield break;
			}
		}
	}
}
