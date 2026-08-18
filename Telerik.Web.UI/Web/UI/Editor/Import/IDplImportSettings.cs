using System;

namespace Telerik.Web.UI.Editor.Import
{
	// Token: 0x020002A0 RID: 672
	public interface IDplImportSettings
	{
		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060017C1 RID: 6081
		// (set) Token: 0x060017C2 RID: 6082
		DocumentLevel DocumentLevel { get; set; }

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x060017C3 RID: 6083
		// (set) Token: 0x060017C4 RID: 6084
		StylesMode StylesMode { get; set; }

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x060017C5 RID: 6085
		// (set) Token: 0x060017C6 RID: 6086
		string StylesFilePath { get; set; }

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x060017C7 RID: 6087
		// (set) Token: 0x060017C8 RID: 6088
		string StylesSourcePath { get; set; }

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x060017C9 RID: 6089
		// (set) Token: 0x060017CA RID: 6090
		ImagesMode ImagesMode { get; set; }

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x060017CB RID: 6091
		// (set) Token: 0x060017CC RID: 6092
		string ImagesFolderPath { get; set; }

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x060017CD RID: 6093
		// (set) Token: 0x060017CE RID: 6094
		string ImagesSourceBasePath { get; set; }
	}
}
