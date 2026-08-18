using System;

namespace Telerik.Web.UI
{
	// Token: 0x020000A6 RID: 166
	public interface IRadImagePersister
	{
		// Token: 0x0600067A RID: 1658
		string GenerateBinaryImageUrl(string imageHandlerUrl);

		// Token: 0x0600067B RID: 1659
		void SaveImage(byte[] image);

		// Token: 0x0600067C RID: 1660
		BinaryImageDataContainer LoadImage();
	}
}
