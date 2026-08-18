using System;
using Telerik.Web.Apoc.DataTypes;
using Telerik.Web.Apoc.Image;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013D3 RID: 5075
	internal class ExternalGraphic : FObj
	{
		// Token: 0x0600D1D6 RID: 53718 RVA: 0x002E783D File Offset: 0x002E5A3D
		public ExternalGraphic(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:external-graphic";
		}

		// Token: 0x0600D1D7 RID: 53719 RVA: 0x002E7854 File Offset: 0x002E5A54
		public override Status Layout(Area area)
		{
			if (this.marker == -1000)
			{
				this.propMgr.GetAccessibilityProps();
				this.propMgr.GetAuralProps();
				this.propMgr.GetBorderAndPadding();
				this.propMgr.GetBackgroundProps();
				this.propMgr.GetMarginInlineProps();
				this.propMgr.GetRelativePositionProps();
				this.align = this.properties.GetProperty("text-align").GetEnum();
				this.startIndent = this.properties.GetProperty("start-indent").GetLength().MValue();
				this.endIndent = this.properties.GetProperty("end-indent").GetLength().MValue();
				this.spaceBefore = this.properties.GetProperty("space-before.optimum").GetLength().MValue();
				this.spaceAfter = this.properties.GetProperty("space-after.optimum").GetLength().MValue();
				this.width = this.properties.GetProperty("width").GetLength().MValue();
				this.height = this.properties.GetProperty("height").GetLength().MValue();
				this.src = this.properties.GetProperty("src").GetString();
				this.id = this.properties.GetProperty("id").GetString();
				area.getIDReferences().CreateID(this.id);
				this.marker = 0;
			}
			try
			{
				ApocImage apocImage = ApocImageFactory.Make(this.src);
				if (this.width == 0 || this.height == 0)
				{
					double num = (double)apocImage.Width;
					double num2 = (double)apocImage.Height;
					if (this.width == 0 && this.height == 0)
					{
						this.width = (int)(num * 1000.0);
						this.height = (int)(num2 * 1000.0);
					}
					else if (this.height == 0)
					{
						this.height = (int)(num2 * (double)this.width / num);
					}
					else if (this.width == 0)
					{
						this.width = (int)(num * (double)this.height / num2);
					}
				}
				double num3 = (double)this.width / (double)this.height;
				Length length = this.properties.GetProperty("max-width").GetLength();
				Length length2 = this.properties.GetProperty("max-height").GetLength();
				if (length != null && this.width > length.MValue())
				{
					this.width = length.MValue();
					this.height = (int)((double)this.width / num3);
				}
				if (length2 != null && this.height > length2.MValue())
				{
					this.height = length2.MValue();
					this.width = (int)(num3 * (double)this.height);
				}
				int num4 = area.getAllocationWidth() - this.startIndent - this.endIndent;
				int num5 = area.getPage().getBody().getMaxHeight() - this.spaceBefore;
				if (this.height > num5)
				{
					this.height = num5;
					this.width = (int)(num3 * (double)this.height);
				}
				if (this.width > num4)
				{
					this.width = num4;
					this.height = (int)((double)this.width / num3);
				}
				if (area.spaceLeft() < this.height + this.spaceBefore)
				{
					this.height = area.spaceLeft();
				}
				this.imageArea = new ImageArea(this.propMgr.GetFontState(area.getFontInfo()), apocImage, area.getAllocationWidth(), this.width, this.height, this.startIndent, this.endIndent, this.align);
				if (this.spaceBefore != 0 && this.marker == 0)
				{
					area.addDisplaySpace(this.spaceBefore);
				}
				if (this.marker == 0)
				{
					area.getIDReferences().ConfigureID(this.id, area);
				}
				this.imageArea.start();
				this.imageArea.end();
				if (this.spaceAfter != 0)
				{
					area.addDisplaySpace(this.spaceAfter);
				}
				if (this.breakBefore == 58 || this.spaceBefore + this.imageArea.GetHeight() > area.spaceLeft())
				{
					return new Status(4);
				}
				if (this.breakBefore == 55)
				{
					return new Status(6);
				}
				if (this.breakBefore == 26)
				{
					return new Status(5);
				}
				BlockArea blockArea = area as BlockArea;
				if (blockArea != null)
				{
					LineArea lineArea = blockArea.getCurrentLineArea();
					if (lineArea == null)
					{
						return new Status(2);
					}
					lineArea.addPending();
					if (this.imageArea.getContentWidth() > lineArea.getRemainingWidth())
					{
						lineArea = blockArea.createNextLineArea();
						if (lineArea == null)
						{
							return new Status(2);
						}
					}
					lineArea.addInlineArea(this.imageArea, this.GetLinkSet());
				}
				else
				{
					area.addChild(this.imageArea);
					area.increaseHeight(this.imageArea.getContentHeight());
				}
				this.imageArea.setPage(area.getPage());
				if (this.breakAfter == 58)
				{
					this.marker = -1001;
					return new Status(4);
				}
				if (this.breakAfter == 55)
				{
					this.marker = -1001;
					return new Status(6);
				}
				if (this.breakAfter == 26)
				{
					this.marker = -1001;
					return new Status(5);
				}
			}
			catch (ApocImageException ex)
			{
				ApocDriver.ActiveDriver.FireApocError("Error while creating area : " + ex.Message);
			}
			return new Status(1);
		}

		// Token: 0x0600D1D8 RID: 53720 RVA: 0x002E7DF0 File Offset: 0x002E5FF0
		public new static FObj.Maker GetMaker()
		{
			return new ExternalGraphic.Maker();
		}

		// Token: 0x04003874 RID: 14452
		private int breakAfter;

		// Token: 0x04003875 RID: 14453
		private int breakBefore;

		// Token: 0x04003876 RID: 14454
		private int align;

		// Token: 0x04003877 RID: 14455
		private int startIndent;

		// Token: 0x04003878 RID: 14456
		private int endIndent;

		// Token: 0x04003879 RID: 14457
		private int spaceBefore;

		// Token: 0x0400387A RID: 14458
		private int spaceAfter;

		// Token: 0x0400387B RID: 14459
		private string src;

		// Token: 0x0400387C RID: 14460
		private int height;

		// Token: 0x0400387D RID: 14461
		private int width;

		// Token: 0x0400387E RID: 14462
		private string id;

		// Token: 0x0400387F RID: 14463
		private ImageArea imageArea;

		// Token: 0x020013D4 RID: 5076
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D1D9 RID: 53721 RVA: 0x002E7DF7 File Offset: 0x002E5FF7
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new ExternalGraphic(parent, propertyList);
			}
		}
	}
}
