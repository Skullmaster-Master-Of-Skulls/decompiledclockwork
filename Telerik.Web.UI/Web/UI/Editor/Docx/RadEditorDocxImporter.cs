using System;
using Telerik.Web.UI.Editor.Import;

namespace Telerik.Web.UI.Editor.Docx
{
	// Token: 0x020002A7 RID: 679
	internal class RadEditorDocxImporter : RadEditorDplImport
	{
		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x0004FC4A File Offset: 0x0004DE4A
		protected override string FormatProviderType
		{
			get
			{
				return "Telerik.Windows.Documents.Flow.FormatProviders.Docx.DocxFormatProvider";
			}
		}
	}
}
