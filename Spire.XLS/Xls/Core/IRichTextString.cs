using System;
using Spire.Xls.Core.Interfaces;

namespace Spire.Xls.Core
{
	// Token: 0x02000108 RID: 264
	public interface IRichTextString : IExcelApplication, IOptimizedUpdate
	{
		// Token: 0x06000BF3 RID: 3059
		IFont GetFont(int index);

		// Token: 0x06000BF4 RID: 3060
		void SetFont(int iStartPos, int iEndPos, IFont font);

		// Token: 0x06000BF5 RID: 3061
		void ClearFormatting();

		// Token: 0x06000BF6 RID: 3062
		void Clear();

		// Token: 0x06000BF7 RID: 3063
		void Append(string text, IFont font);

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000BF8 RID: 3064
		// (set) Token: 0x06000BF9 RID: 3065
		string Text { get; set; }

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000BFA RID: 3066
		string RtfText { get; }

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000BFB RID: 3067
		bool IsFormatted { get; }
	}
}
