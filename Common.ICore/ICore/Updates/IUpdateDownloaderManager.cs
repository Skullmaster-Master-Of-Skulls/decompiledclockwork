using System;
using System.Collections.Generic;

namespace TechnoPro.Common.ICore.Updates
{
	// Token: 0x0200001C RID: 28
	public interface IUpdateDownloaderManager
	{
		// Token: 0x060000B0 RID: 176
		void GetNewUpdates();

		// Token: 0x060000B1 RID: 177
		void GetRecoveryFiles();

		// Token: 0x060000B2 RID: 178
		IList<string> GetAllUpdatingSystemClientPrivateFolderPath();
	}
}
