using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x0200066B RID: 1643
	public class PagerConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003C1D RID: 15389 RVA: 0x000C33C0 File Offset: 0x000C15C0
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Pager pager = obj as Pager;
			ExplicitJavaScriptConverter.AddProperty(state, "first", pager.First, "Go to the first page");
			ExplicitJavaScriptConverter.AddProperty(state, "previous", pager.Previous, "Go to the previous page");
			ExplicitJavaScriptConverter.AddProperty(state, "next", pager.Next, "Go to the next page");
			ExplicitJavaScriptConverter.AddProperty(state, "last", pager.Last, "Go to the last page");
			ExplicitJavaScriptConverter.AddProperty(state, "of", pager.Of, " of {0} ");
			ExplicitJavaScriptConverter.AddProperty(state, "page", pager.Page, "page");
			ExplicitJavaScriptConverter.AddProperty(state, "pages", pager.Pages, "pages");
		}

		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x06003C1E RID: 15390 RVA: 0x000C3470 File Offset: 0x000C1670
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Pager)
				};
			}
		}
	}
}
