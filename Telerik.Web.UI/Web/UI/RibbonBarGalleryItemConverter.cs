using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000783 RID: 1923
	internal class RibbonBarGalleryItemConverter : JavaScriptConverter
	{
		// Token: 0x060043BF RID: 17343 RVA: 0x000D3E59 File Offset: 0x000D2059
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043C0 RID: 17344 RVA: 0x000D3E60 File Offset: 0x000D2060
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			RibbonBarGalleryItem ribbonBarGalleryItem = (RibbonBarGalleryItem)obj;
			if (!string.IsNullOrEmpty(ribbonBarGalleryItem.CommandArgument))
			{
				dictionary["commandArgument"] = ribbonBarGalleryItem.CommandArgument;
			}
			if (!string.IsNullOrEmpty(ribbonBarGalleryItem.ImageUrl))
			{
				dictionary["imageUrl"] = ribbonBarGalleryItem.ResolveUrl(ribbonBarGalleryItem.ImageUrl);
			}
			if (ribbonBarGalleryItem.Selected)
			{
				dictionary["selected"] = true;
			}
			return dictionary;
		}

		// Token: 0x17001613 RID: 5651
		// (get) Token: 0x060043C1 RID: 17345 RVA: 0x000D3FA4 File Offset: 0x000D21A4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RibbonBarGalleryItem);
				yield break;
			}
		}
	}
}
