using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x0200065F RID: 1631
	public class DialogsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003BC9 RID: 15305 RVA: 0x000C284C File Offset: 0x000C0A4C
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Dialogs dialogs = obj as Dialogs;
			ExplicitJavaScriptConverter.AddProperty(state, "exportAsDialog", dialogs.ExportAsDialogMessages, null);
			ExplicitJavaScriptConverter.AddProperty(state, "okText", dialogs.OkText, "OK");
			ExplicitJavaScriptConverter.AddProperty(state, "save", dialogs.Save, "Save");
			ExplicitJavaScriptConverter.AddProperty(state, "cancel", dialogs.Cancel, "Cancel");
		}

		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x06003BCA RID: 15306 RVA: 0x000C28B4 File Offset: 0x000C0AB4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Dialogs)
				};
			}
		}
	}
}
