using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000EA1 RID: 3745
	public class GDIOperationsBasedGraphicsCore : IGraphicsCore
	{
		// Token: 0x06008ED1 RID: 36561 RVA: 0x00202B80 File Offset: 0x00200D80
		public Image ChangeOpacity(Image image, double opacity)
		{
			return this.ApplyOperation(image, new OpacityOperation(opacity));
		}

		// Token: 0x06008ED2 RID: 36562 RVA: 0x00202B8F File Offset: 0x00200D8F
		public Image Resize(Image image, Size size)
		{
			return this.ApplyOperation(image, new ResizeOperation(size));
		}

		// Token: 0x06008ED3 RID: 36563 RVA: 0x00202B9E File Offset: 0x00200D9E
		public Image Flip(Image image, FlipDirection direction)
		{
			return this.ApplyOperation(image, new RotateFlipOperation((direction == FlipDirection.Horizontal) ? RotateFlipType.RotateNoneFlipX : ((direction == FlipDirection.Vertical) ? RotateFlipType.Rotate180FlipX : RotateFlipType.Rotate180FlipNone)));
		}

		// Token: 0x06008ED4 RID: 36564 RVA: 0x00202BBA File Offset: 0x00200DBA
		public Image Rotate(Image image, Rotation rotate)
		{
			return this.ApplyOperation(image, new RotateFlipOperation((rotate == Rotation.Rotate90) ? RotateFlipType.Rotate90FlipNone : ((rotate == Rotation.Rotate180) ? RotateFlipType.Rotate180FlipNone : RotateFlipType.Rotate270FlipNone)));
		}

		// Token: 0x06008ED5 RID: 36565 RVA: 0x00202BD6 File Offset: 0x00200DD6
		public Image Crop(Image image, Rectangle rectange)
		{
			return this.ApplyOperation(image, new CropOperation(rectange));
		}

		// Token: 0x06008ED6 RID: 36566 RVA: 0x00202BE5 File Offset: 0x00200DE5
		public Image AddText(Image image, Point position, ImageText text)
		{
			return this.ApplyOperation(image, new AddTextOperation(position, text));
		}

		// Token: 0x06008ED7 RID: 36567 RVA: 0x00202BF5 File Offset: 0x00200DF5
		public Image InsertImage(Image image, Point position, Image imageToInsert)
		{
			return this.ApplyOperation(image, new InsertImageOperation(position, imageToInsert));
		}

		// Token: 0x06008ED8 RID: 36568 RVA: 0x00202C05 File Offset: 0x00200E05
		public Image ConvertTo(Image original, EditableFormat format)
		{
			return this.ApplyOperation(original, new ConvertToOperation((format == EditableFormat.Png) ? ImageFormat.Png : ((format == EditableFormat.Jpg) ? ImageFormat.Jpeg : ((format == EditableFormat.Gif) ? ImageFormat.Gif : ((format == EditableFormat.Bmp) ? ImageFormat.Bmp : original.RawFormat)))));
		}

		// Token: 0x06008ED9 RID: 36569 RVA: 0x00202C44 File Offset: 0x00200E44
		public virtual Image ApplyOperation(Image image, IImageOperation operation)
		{
			return operation.Apply(image);
		}
	}
}
