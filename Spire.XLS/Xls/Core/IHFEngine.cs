using System;

namespace Spire.Xls.Core
{
	// Token: 0x020005D7 RID: 1495
	public interface IHFEngine : IRichTextString
	{
		// Token: 0x060058C7 RID: 22727
		void Parse(string strText);

		// Token: 0x060058C8 RID: 22728
		string GetHeaderFooterString();
	}
}
