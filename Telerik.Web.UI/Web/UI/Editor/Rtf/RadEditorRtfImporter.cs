using System;
using Telerik.Web.UI.Editor.Import;

namespace Telerik.Web.UI.Editor.Rtf
{
	// Token: 0x0200184B RID: 6219
	internal class RadEditorRtfImporter : RadEditorDplImport
	{
		// Token: 0x170048EE RID: 18670
		// (get) Token: 0x0600F17F RID: 61823 RVA: 0x0036E22E File Offset: 0x0036C42E
		protected override string FormatProviderType
		{
			get
			{
				return "Telerik.Windows.Documents.Flow.FormatProviders.Rtf.RtfFormatProvider";
			}
		}
	}
}
