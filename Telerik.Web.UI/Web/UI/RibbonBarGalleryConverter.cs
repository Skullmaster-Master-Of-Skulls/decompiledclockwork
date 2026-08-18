using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000785 RID: 1925
	internal class RibbonBarGalleryConverter : JavaScriptConverter
	{
		// Token: 0x060043C7 RID: 17351 RVA: 0x000D40FD File Offset: 0x000D22FD
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043C8 RID: 17352 RVA: 0x000D4104 File Offset: 0x000D2304
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			RibbonBarGallery ribbonBarGallery = (RibbonBarGallery)obj;
			if (!string.IsNullOrEmpty(ribbonBarGallery.CommandName))
			{
				dictionary["commandName"] = ribbonBarGallery.CommandName;
			}
			if (ribbonBarGallery.Categories.Count > 0)
			{
				dictionary["categoryData"] = ribbonBarGallery.Categories;
			}
			if (ribbonBarGallery.ItemHeight != Unit.Empty)
			{
				dictionary["itemHeight"] = ribbonBarGallery.ItemHeight.Value;
			}
			if (ribbonBarGallery.ItemWidth != Unit.Empty)
			{
				dictionary["itemWidth"] = ribbonBarGallery.ItemWidth.Value;
			}
			if (ribbonBarGallery.Columns != 5)
			{
				dictionary["columns"] = ribbonBarGallery.Columns;
			}
			if (ribbonBarGallery.ExpandedColumns != 5)
			{
				dictionary["expandedColumns"] = ribbonBarGallery.ExpandedColumns;
			}
			if (ribbonBarGallery.ExpandedHeight != Unit.Empty)
			{
				dictionary["expandedHeight"] = ribbonBarGallery.ExpandedHeight.Value;
			}
			return dictionary;
		}

		// Token: 0x17001615 RID: 5653
		// (get) Token: 0x060043C9 RID: 17353 RVA: 0x000D42F8 File Offset: 0x000D24F8
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RibbonBarGallery);
				yield break;
			}
		}
	}
}
