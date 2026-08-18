using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x02000665 RID: 1637
	public class FileConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003BF1 RID: 15345 RVA: 0x000C2DB4 File Offset: 0x000C0FB4
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			File file = obj as File;
			ExplicitJavaScriptConverter.AddProperty(state, "data", file.Data, "");
			ExplicitJavaScriptConverter.AddProperty(state, "url", file.Url, "");
		}

		// Token: 0x170013BA RID: 5050
		// (get) Token: 0x06003BF2 RID: 15346 RVA: 0x000C2DF4 File Offset: 0x000C0FF4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(File)
				};
			}
		}
	}
}
