using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x02000663 RID: 1635
	public class ExportAsDialogConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003BE8 RID: 15336 RVA: 0x000C2C60 File Offset: 0x000C0E60
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ExportAsDialog exportAsDialog = obj as ExportAsDialog;
			ExplicitJavaScriptConverter.AddProperty(state, "title", exportAsDialog.Title, "Export...");
			ExplicitJavaScriptConverter.AddProperty(state, "defaultFileName", exportAsDialog.DefaultFileName, "Document");
			ExplicitJavaScriptConverter.AddProperty(state, "pdf", exportAsDialog.Pdf, "Portable Document Format (.pdf)");
			ExplicitJavaScriptConverter.AddProperty(state, "png", exportAsDialog.Png, "Portable Network Graphics (.png)");
			ExplicitJavaScriptConverter.AddProperty(state, "svg", exportAsDialog.Svg, "Scalable Vector Graphics (.svg)");
			ExplicitJavaScriptConverter.AddProperty(state, "labels", exportAsDialog.LabelsMessages, null);
		}

		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x06003BE9 RID: 15337 RVA: 0x000C2CF4 File Offset: 0x000C0EF4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ExportAsDialog)
				};
			}
		}
	}
}
