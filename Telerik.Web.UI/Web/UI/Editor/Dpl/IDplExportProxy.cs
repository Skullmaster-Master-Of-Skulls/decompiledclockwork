using System;
using System.Reflection;

namespace Telerik.Web.UI.Editor.Dpl
{
	// Token: 0x02000285 RID: 645
	public interface IDplExportProxy
	{
		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001702 RID: 5890
		Assembly DocumentsFlow { get; }

		// Token: 0x06001703 RID: 5891
		object CreateHtmlFormatProvider();

		// Token: 0x06001704 RID: 5892
		object ConvertHtmlToRadFlowDocument(string editorContent);

		// Token: 0x06001705 RID: 5893
		string ExportToDocx(object radFlowDocument);

		// Token: 0x06001706 RID: 5894
		string ExportToRtf(object radFlowDocument);

		// Token: 0x06001707 RID: 5895
		string ValidateHtmlForExport(string html);

		// Token: 0x06001708 RID: 5896
		void SetPageHeader(object radFlowDocument, string pageHeader, decimal headerFontSizeInPoints);

		// Token: 0x06001709 RID: 5897
		void SetDefaultFont(object radFlowDocument, string defaultFontName, decimal defaultFontSizeInPoints);
	}
}
