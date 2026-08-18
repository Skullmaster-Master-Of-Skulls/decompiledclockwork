using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000780 RID: 1920
	internal class RibbonBarApplicationMenuItemBaseConverter : JavaScriptConverter
	{
		// Token: 0x060043B6 RID: 17334 RVA: 0x000D3A97 File Offset: 0x000D1C97
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x000D3AA0 File Offset: 0x000D1CA0
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			RibbonBarApplicationMenuItemBase ribbonBarApplicationMenuItemBase = obj as RibbonBarApplicationMenuItemBase;
			if (!ribbonBarApplicationMenuItemBase.Enabled)
			{
				dictionary["enabled"] = false;
			}
			if (!string.IsNullOrEmpty(ribbonBarApplicationMenuItemBase.ImageUrl))
			{
				dictionary["imageUrl"] = ribbonBarApplicationMenuItemBase.ResolveUrl(ribbonBarApplicationMenuItemBase.ImageUrl);
			}
			if (!string.IsNullOrEmpty(ribbonBarApplicationMenuItemBase.CommandName))
			{
				dictionary["commandName"] = ribbonBarApplicationMenuItemBase.ResolveClientUrl(ribbonBarApplicationMenuItemBase.CommandName);
			}
			if (!string.IsNullOrEmpty(ribbonBarApplicationMenuItemBase.CommandArgument))
			{
				dictionary["commandArgument"] = ribbonBarApplicationMenuItemBase.ResolveClientUrl(ribbonBarApplicationMenuItemBase.CommandArgument);
			}
			return dictionary;
		}

		// Token: 0x17001610 RID: 5648
		// (get) Token: 0x060043B8 RID: 17336 RVA: 0x000D3C0C File Offset: 0x000D1E0C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RibbonBarApplicationMenuItemBase);
				yield break;
			}
		}
	}
}
