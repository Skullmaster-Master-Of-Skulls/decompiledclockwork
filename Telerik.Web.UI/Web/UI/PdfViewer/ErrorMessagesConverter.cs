using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x02000661 RID: 1633
	public class ErrorMessagesConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003BD4 RID: 15316 RVA: 0x000C29B8 File Offset: 0x000C0BB8
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ErrorMessages errorMessages = obj as ErrorMessages;
			ExplicitJavaScriptConverter.AddProperty(state, "notSupported", errorMessages.NotSupported, "Only pdf files allowed.");
			ExplicitJavaScriptConverter.AddProperty(state, "parseError", errorMessages.ParseError, "PDF file fails to process.");
			ExplicitJavaScriptConverter.AddProperty(state, "notFound", errorMessages.NotFound, "File is not found.");
		}

		// Token: 0x170013AE RID: 5038
		// (get) Token: 0x06003BD5 RID: 15317 RVA: 0x000C2A10 File Offset: 0x000C0C10
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ErrorMessages)
				};
			}
		}
	}
}
