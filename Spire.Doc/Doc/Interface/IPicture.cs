using System;
using System.Drawing;
using Spire.Doc.Documents;

namespace Spire.Doc.Interface
{
	// Token: 0x020004FF RID: 1279
	public interface IPicture : IParagraphBase
	{
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06004216 RID: 16918
		// (set) Token: 0x06004217 RID: 16919
		float Height { get; set; }

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06004218 RID: 16920
		// (set) Token: 0x06004219 RID: 16921
		float Width { get; set; }

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x0600421A RID: 16922
		// (set) Token: 0x0600421B RID: 16923
		float HeightScale { get; set; }

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x0600421C RID: 16924
		// (set) Token: 0x0600421D RID: 16925
		float WidthScale { get; set; }

		// Token: 0x0600421E RID: 16926
		void LoadImage(Image imageStream);

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x0600421F RID: 16927
		Image Image { get; }

		// Token: 0x06004220 RID: 16928
		void LoadImage(byte[] imageBytes);

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06004221 RID: 16929
		byte[] ImageBytes { get; }

		// Token: 0x06004222 RID: 16930
		IParagraph AddCaption(string name, CaptionNumberingFormat format, CaptionPosition captionPosition);

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06004223 RID: 16931
		// (set) Token: 0x06004224 RID: 16932
		HorizontalOrigin HorizontalOrigin { get; set; }

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06004225 RID: 16933
		// (set) Token: 0x06004226 RID: 16934
		VerticalOrigin VerticalOrigin { get; set; }

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06004227 RID: 16935
		// (set) Token: 0x06004228 RID: 16936
		float HorizontalPosition { get; set; }

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06004229 RID: 16937
		// (set) Token: 0x0600422A RID: 16938
		float VerticalPosition { get; set; }

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x0600422B RID: 16939
		// (set) Token: 0x0600422C RID: 16940
		TextWrappingStyle TextWrappingStyle { get; set; }

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x0600422D RID: 16941
		// (set) Token: 0x0600422E RID: 16942
		TextWrappingType TextWrappingType { get; set; }

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x0600422F RID: 16943
		// (set) Token: 0x06004230 RID: 16944
		ShapeHorizontalAlignment HorizontalAlignment { get; set; }

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06004231 RID: 16945
		// (set) Token: 0x06004232 RID: 16946
		ShapeVerticalAlignment VerticalAlignment { get; set; }

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06004233 RID: 16947
		// (set) Token: 0x06004234 RID: 16948
		string AlternativeText { get; set; }

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06004235 RID: 16949
		// (set) Token: 0x06004236 RID: 16950
		string Title { get; set; }

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06004237 RID: 16951
		// (set) Token: 0x06004238 RID: 16952
		bool IsUnderText { get; set; }
	}
}
