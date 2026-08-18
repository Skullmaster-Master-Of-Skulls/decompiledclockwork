using System;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x0200171F RID: 5919
	internal class WrapContext
	{
		// Token: 0x17004608 RID: 17928
		// (get) Token: 0x0600E5EF RID: 58863 RVA: 0x00330EE0 File Offset: 0x0032F0E0
		internal float ContainerWidth
		{
			get
			{
				return this.wrapContainerWidth;
			}
		}

		// Token: 0x17004609 RID: 17929
		// (get) Token: 0x0600E5F0 RID: 58864 RVA: 0x00330EE8 File Offset: 0x0032F0E8
		internal float ContainerHeight
		{
			get
			{
				return this.wrapContainerHeight;
			}
		}

		// Token: 0x1700460A RID: 17930
		// (get) Token: 0x0600E5F1 RID: 58865 RVA: 0x00330EF0 File Offset: 0x0032F0F0
		internal WrapType Type
		{
			get
			{
				return this.wrapType;
			}
		}

		// Token: 0x0600E5F2 RID: 58866 RVA: 0x00330EF8 File Offset: 0x0032F0F8
		internal WrapContext(float width, float height, WrapType type)
		{
			this.wrapContainerWidth = width;
			this.wrapContainerHeight = height;
			this.wrapType = type;
		}

		// Token: 0x0600E5F3 RID: 58867 RVA: 0x00330F15 File Offset: 0x0032F115
		internal WrapContext(Dimensions dimension, WrapType type)
		{
			this.wrapContainerWidth = dimension.Width.PixelValue;
			this.wrapContainerHeight = dimension.Height.PixelValue;
			this.wrapType = type;
		}

		// Token: 0x04004227 RID: 16935
		private float wrapContainerWidth;

		// Token: 0x04004228 RID: 16936
		private float wrapContainerHeight;

		// Token: 0x04004229 RID: 16937
		private WrapType wrapType;
	}
}
