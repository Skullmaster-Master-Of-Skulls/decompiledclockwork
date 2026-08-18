using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x02000667 RID: 1639
	public class LabelsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003BFC RID: 15356 RVA: 0x000C2EF8 File Offset: 0x000C10F8
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Labels labels = obj as Labels;
			ExplicitJavaScriptConverter.AddProperty(state, "fileName", labels.FileName, "File name");
			ExplicitJavaScriptConverter.AddProperty(state, "saveAsType", labels.SaveAsType, "Save as");
			ExplicitJavaScriptConverter.AddProperty(state, "page", labels.Page, "Page");
		}

		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x06003BFD RID: 15357 RVA: 0x000C2F50 File Offset: 0x000C1150
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Labels)
				};
			}
		}
	}
}
