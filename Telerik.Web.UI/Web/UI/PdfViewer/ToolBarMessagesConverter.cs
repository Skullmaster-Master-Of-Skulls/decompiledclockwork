using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x02000657 RID: 1623
	public class ToolBarMessagesConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003B9A RID: 15258 RVA: 0x000C214C File Offset: 0x000C034C
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ToolBarMessages toolBarMessages = obj as ToolBarMessages;
			ExplicitJavaScriptConverter.AddProperty(state, "open", toolBarMessages.Open, "Open");
			ExplicitJavaScriptConverter.AddProperty(state, "exportAs", toolBarMessages.ExportAs, "Export");
			ExplicitJavaScriptConverter.AddProperty(state, "download", toolBarMessages.Download, "Download");
			ExplicitJavaScriptConverter.AddProperty(state, "pager", toolBarMessages.PagerMessages, null);
		}

		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x06003B9B RID: 15259 RVA: 0x000C21B4 File Offset: 0x000C03B4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ToolBarMessages)
				};
			}
		}
	}
}
