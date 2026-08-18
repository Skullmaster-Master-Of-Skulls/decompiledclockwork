using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x0200065D RID: 1629
	public class DefaultPageSizeConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003BB9 RID: 15289 RVA: 0x000C2628 File Offset: 0x000C0828
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			DefaultPageSize defaultPageSize = obj as DefaultPageSize;
			ExplicitJavaScriptConverter.AddProperty(state, "width", defaultPageSize.Width, 794.0);
			ExplicitJavaScriptConverter.AddProperty(state, "height", defaultPageSize.Height, 1123.0);
		}

		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x06003BBA RID: 15290 RVA: 0x000C2684 File Offset: 0x000C0884
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(DefaultPageSize)
				};
			}
		}
	}
}
