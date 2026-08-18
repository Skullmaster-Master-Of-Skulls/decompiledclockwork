using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x0200066D RID: 1645
	public class PdfjsProcessingConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003C29 RID: 15401 RVA: 0x000C35A0 File Offset: 0x000C17A0
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			PdfjsProcessing pdfjsProcessing = obj as PdfjsProcessing;
			ExplicitJavaScriptConverter.AddProperty(state, "file", pdfjsProcessing.File, "");
			ExplicitJavaScriptConverter.AddProperty(state, "file", pdfjsProcessing.FileSettings, null);
		}

		// Token: 0x170013D2 RID: 5074
		// (get) Token: 0x06003C2A RID: 15402 RVA: 0x000C35DC File Offset: 0x000C17DC
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(PdfjsProcessing)
				};
			}
		}
	}
}
