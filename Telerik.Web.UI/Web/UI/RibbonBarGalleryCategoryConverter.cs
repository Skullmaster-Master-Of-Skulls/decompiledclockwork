using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000784 RID: 1924
	internal class RibbonBarGalleryCategoryConverter : JavaScriptConverter
	{
		// Token: 0x060043C3 RID: 17347 RVA: 0x000D3FC9 File Offset: 0x000D21C9
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043C4 RID: 17348 RVA: 0x000D3FD0 File Offset: 0x000D21D0
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			RibbonBarGalleryCategory ribbonBarGalleryCategory = (RibbonBarGalleryCategory)obj;
			if (ribbonBarGalleryCategory.Items.Count > 0)
			{
				dictionary["itemData"] = ribbonBarGalleryCategory.Items;
			}
			return dictionary;
		}

		// Token: 0x17001614 RID: 5652
		// (get) Token: 0x060043C5 RID: 17349 RVA: 0x000D40D8 File Offset: 0x000D22D8
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RibbonBarGalleryCategory);
				yield break;
			}
		}
	}
}
