using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x02000669 RID: 1641
	public class MessagesConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003C0A RID: 15370 RVA: 0x000C313C File Offset: 0x000C133C
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Messages messages = obj as Messages;
			ExplicitJavaScriptConverter.AddProperty(state, "defaultFileName", messages.DefaultFileName, "Document");
			ExplicitJavaScriptConverter.AddProperty(state, "toolbar", messages.ToolBarMessages, null);
			ExplicitJavaScriptConverter.AddProperty(state, "errorMessages", messages.ErrorMessages, null);
			ExplicitJavaScriptConverter.AddProperty(state, "dialogs", messages.DialogsMessages, null);
		}

		// Token: 0x170013C5 RID: 5061
		// (get) Token: 0x06003C0B RID: 15371 RVA: 0x000C319C File Offset: 0x000C139C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Messages)
				};
			}
		}
	}
}
