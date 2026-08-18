using System;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x020003F7 RID: 1015
	internal interface IImageEditorCacheHandler
	{
		// Token: 0x06002546 RID: 9542
		bool IsCustomDownloadOperation(string downloadKey);

		// Token: 0x06002547 RID: 9543
		bool IsDownloadedFromImageProvider(string downloadKey);

		// Token: 0x06002548 RID: 9544
		bool IsDownloadedFromCanvas(string downloadKey);

		// Token: 0x06002549 RID: 9545
		string Encrypt(string input);

		// Token: 0x0600254A RID: 9546
		string Decrypt(string input);
	}
}
