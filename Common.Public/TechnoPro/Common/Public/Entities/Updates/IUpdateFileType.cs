using System;

namespace TechnoPro.Common.Public.Entities.Updates
{
	// Token: 0x02000142 RID: 322
	public interface IUpdateFileType
	{
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060007A9 RID: 1961
		eUpdateFileTypes UpdateFileType { get; }

		// Token: 0x060007AA RID: 1962
		string GetFilenamePattern(int addSize = 0);

		// Token: 0x060007AB RID: 1963
		Version GetFileVersion(string fn);

		// Token: 0x060007AC RID: 1964
		bool IsHotFix(string fn);

		// Token: 0x060007AD RID: 1965
		int GetAddressSize(string fn);

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060007AE RID: 1966
		string Extension { get; }
	}
}
