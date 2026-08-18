using System;
using System.Drawing;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000EA0 RID: 3744
	public interface IGraphicsCore
	{
		// Token: 0x06008EC9 RID: 36553
		Image ChangeOpacity(Image original, double opacity);

		// Token: 0x06008ECA RID: 36554
		Image Resize(Image original, Size size);

		// Token: 0x06008ECB RID: 36555
		Image Flip(Image original, FlipDirection direction);

		// Token: 0x06008ECC RID: 36556
		Image Rotate(Image original, Rotation rotate);

		// Token: 0x06008ECD RID: 36557
		Image Crop(Image original, Rectangle rectange);

		// Token: 0x06008ECE RID: 36558
		Image AddText(Image original, Point position, ImageText text);

		// Token: 0x06008ECF RID: 36559
		Image InsertImage(Image original, Point position, Image imageToInsert);

		// Token: 0x06008ED0 RID: 36560
		Image ConvertTo(Image original, EditableFormat format);
	}
}
