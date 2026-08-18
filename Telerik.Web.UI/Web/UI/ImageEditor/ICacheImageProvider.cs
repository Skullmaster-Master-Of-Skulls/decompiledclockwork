using System;
using System.Drawing;
using System.Web;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000E8C RID: 3724
	public interface ICacheImageProvider
	{
		// Token: 0x06008D26 RID: 36134
		string Store(EditableImage image);

		// Token: 0x06008D27 RID: 36135
		EditableImage Retrieve(string key);

		// Token: 0x06008D28 RID: 36136
		Image LoadImage(string imageUrl, string physicalPath, HttpContext context);

		// Token: 0x06008D29 RID: 36137
		string SaveImage(EditableImage editableImage, string physicalPath, string imageUrl, bool overwrite);

		// Token: 0x06008D2A RID: 36138
		void ClearImages();

		// Token: 0x06008D2B RID: 36139
		void ClearImages(string imageKey);

		// Token: 0x17002C8C RID: 11404
		// (get) Token: 0x06008D2C RID: 36140
		// (set) Token: 0x06008D2D RID: 36141
		ImageStorage Storage { get; set; }

		// Token: 0x17002C8D RID: 11405
		// (get) Token: 0x06008D2E RID: 36142
		// (set) Token: 0x06008D2F RID: 36143
		string ImageStorageKey { get; set; }
	}
}
