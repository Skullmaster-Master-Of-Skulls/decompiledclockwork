using System;
using System.IO;
using System.Reflection;
using Telerik.Web.UI.Editor.Import;

namespace Telerik.Web.UI.Editor.Dpl
{
	// Token: 0x02000287 RID: 647
	public interface IDplImportProxy
	{
		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001714 RID: 5908
		Assembly DocumentsFlow { get; }

		// Token: 0x06001715 RID: 5909
		object CreateHtmlFormatProvider();

		// Token: 0x06001716 RID: 5910
		string ConvertRadFlowDocumentToHtml(object radFlowDocument, object htmlFormatProvider);

		// Token: 0x06001717 RID: 5911
		object ConvertStreamToRadFlowDocument(Stream stream, string formatProviderType);

		// Token: 0x06001718 RID: 5912
		void ApplyImportSettings(object htmlFormatProvider, IDplImportSettings settings);
	}
}
