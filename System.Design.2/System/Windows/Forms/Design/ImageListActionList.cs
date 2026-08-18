using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002F4 RID: 756
	internal class ImageListActionList : DesignerActionList
	{
		// Token: 0x06001E2C RID: 7724 RVA: 0x000B6D07 File Offset: 0x000B4F07
		public ImageListActionList(ImageListDesigner designer) : base(designer.Component)
		{
			this._designer = designer;
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x000B6D1C File Offset: 0x000B4F1C
		public void ChooseImages()
		{
			EditorServiceContext.EditValue(this._designer, base.Component, "Images");
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001E2E RID: 7726 RVA: 0x000B6D35 File Offset: 0x000B4F35
		// (set) Token: 0x06001E2F RID: 7727 RVA: 0x000B6D47 File Offset: 0x000B4F47
		public ColorDepth ColorDepth
		{
			get
			{
				return ((ImageList)base.Component).ColorDepth;
			}
			set
			{
				TypeDescriptor.GetProperties(base.Component)["ColorDepth"].SetValue(base.Component, value);
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001E30 RID: 7728 RVA: 0x000B6D6F File Offset: 0x000B4F6F
		// (set) Token: 0x06001E31 RID: 7729 RVA: 0x000B6D81 File Offset: 0x000B4F81
		public Size ImageSize
		{
			get
			{
				return ((ImageList)base.Component).ImageSize;
			}
			set
			{
				TypeDescriptor.GetProperties(base.Component)["ImageSize"].SetValue(base.Component, value);
			}
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x000B6DAC File Offset: 0x000B4FAC
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			return new DesignerActionItemCollection
			{
				new DesignerActionPropertyItem("ImageSize", SR.GetString("ImageListActionListImageSizeDisplayName"), SR.GetString("PropertiesCategoryName"), SR.GetString("ImageListActionListImageSizeDescription")),
				new DesignerActionPropertyItem("ColorDepth", SR.GetString("ImageListActionListColorDepthDisplayName"), SR.GetString("PropertiesCategoryName"), SR.GetString("ImageListActionListColorDepthDescription")),
				new DesignerActionMethodItem(this, "ChooseImages", SR.GetString("ImageListActionListChooseImagesDisplayName"), SR.GetString("LinksCategoryName"), SR.GetString("ImageListActionListChooseImagesDescription"), true)
			};
		}

		// Token: 0x040017C5 RID: 6085
		private ImageListDesigner _designer;
	}
}
