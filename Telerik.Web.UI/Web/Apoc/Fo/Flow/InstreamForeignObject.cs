using System;
using Telerik.Web.Apoc.Layout;
using Telerik.Web.Apoc.Layout.Inline;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013E1 RID: 5089
	internal class InstreamForeignObject : FObj
	{
		// Token: 0x0600D202 RID: 53762 RVA: 0x002E8621 File Offset: 0x002E6821
		public new static FObj.Maker GetMaker()
		{
			return new InstreamForeignObject.Maker();
		}

		// Token: 0x0600D203 RID: 53763 RVA: 0x002E8628 File Offset: 0x002E6828
		public InstreamForeignObject(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:instream-foreign-object";
		}

		// Token: 0x0600D204 RID: 53764 RVA: 0x002E8640 File Offset: 0x002E6840
		public override Status Layout(Area area)
		{
			if (this.marker == -1001)
			{
				return new Status(1);
			}
			if (this.marker == -1000)
			{
				this.propMgr.GetAccessibilityProps();
				this.propMgr.GetAuralProps();
				this.propMgr.GetBorderAndPadding();
				this.propMgr.GetBackgroundProps();
				this.propMgr.GetMarginInlineProps();
				this.propMgr.GetRelativePositionProps();
				string @string = this.properties.GetProperty("id").GetString();
				int @enum = this.properties.GetProperty("text-align").GetEnum();
				int enum2 = this.properties.GetProperty("vertical-align").GetEnum();
				int enum3 = this.properties.GetProperty("overflow").GetEnum();
				this.breakBefore = this.properties.GetProperty("break-before").GetEnum();
				this.breakAfter = this.properties.GetProperty("break-after").GetEnum();
				this.width = this.properties.GetProperty("width").GetLength().MValue();
				this.height = this.properties.GetProperty("height").GetLength().MValue();
				this.contwidth = this.properties.GetProperty("content-width").GetLength().MValue();
				this.contheight = this.properties.GetProperty("content-height").GetLength().MValue();
				this.wauto = this.properties.GetProperty("width").GetLength().IsAuto();
				this.hauto = this.properties.GetProperty("height").GetLength().IsAuto();
				this.cwauto = this.properties.GetProperty("content-width").GetLength().IsAuto();
				this.chauto = this.properties.GetProperty("content-height").GetLength().IsAuto();
				this.startIndent = this.properties.GetProperty("start-indent").GetLength().MValue();
				this.endIndent = this.properties.GetProperty("end-indent").GetLength().MValue();
				this.spaceBefore = this.properties.GetProperty("space-before.optimum").GetLength().MValue();
				this.spaceAfter = this.properties.GetProperty("space-after.optimum").GetLength().MValue();
				this.scaling = this.properties.GetProperty("scaling").GetEnum();
				area.getIDReferences().CreateID(@string);
				if (this.areaCurrent == null)
				{
					this.areaCurrent = new ForeignObjectArea(this.propMgr.GetFontState(area.getFontInfo()), area.getAllocationWidth());
					this.areaCurrent.start();
					this.areaCurrent.SetWidth(this.width);
					this.areaCurrent.SetHeight(this.height);
					this.areaCurrent.SetContentWidth(this.contwidth);
					this.areaCurrent.setContentHeight(this.contheight);
					this.areaCurrent.setScaling(this.scaling);
					this.areaCurrent.setAlign(@enum);
					this.areaCurrent.setVerticalAlign(enum2);
					this.areaCurrent.setOverflow(enum3);
					this.areaCurrent.setSizeAuto(this.wauto, this.hauto);
					this.areaCurrent.setContentSizeAuto(this.cwauto, this.chauto);
					this.areaCurrent.setPage(area.getPage());
					int count = this.children.Count;
					if (count > 1)
					{
						throw new ApocException("Only one child element is allowed in an instream-foreign-object");
					}
					if (this.children.Count > 0)
					{
						FONode fonode = (FONode)this.children[0];
						Status status;
						Status result = status = fonode.Layout(this.areaCurrent);
						if (status.isIncomplete())
						{
							return result;
						}
						this.areaCurrent.end();
					}
				}
				this.marker = 0;
				if (this.breakBefore == 58 || this.spaceBefore + this.areaCurrent.getEffectiveHeight() > area.spaceLeft())
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
			}
			if (this.areaCurrent == null)
			{
				return new Status(1);
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
				if (this.areaCurrent.getEffectiveWidth() > lineArea.getRemainingWidth())
				{
					lineArea = blockArea.createNextLineArea();
					if (lineArea == null)
					{
						return new Status(2);
					}
				}
				lineArea.addInlineArea(this.areaCurrent, this.GetLinkSet());
			}
			else
			{
				area.addChild(this.areaCurrent);
				area.increaseHeight(this.areaCurrent.getEffectiveHeight());
			}
			if (this.isInTableCell)
			{
				this.startIndent += this.forcedStartOffset;
			}
			this.areaCurrent.setStartIndent(this.startIndent);
			int num = this.spaceBefore;
			this.areaCurrent.setPage(area.getPage());
			int num2 = this.spaceAfter;
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
			this.areaCurrent = null;
			return new Status(1);
		}

		// Token: 0x0400388B RID: 14475
		private int breakBefore;

		// Token: 0x0400388C RID: 14476
		private int breakAfter;

		// Token: 0x0400388D RID: 14477
		private int scaling;

		// Token: 0x0400388E RID: 14478
		private int width;

		// Token: 0x0400388F RID: 14479
		private int height;

		// Token: 0x04003890 RID: 14480
		private int contwidth;

		// Token: 0x04003891 RID: 14481
		private int contheight;

		// Token: 0x04003892 RID: 14482
		private bool wauto;

		// Token: 0x04003893 RID: 14483
		private bool hauto;

		// Token: 0x04003894 RID: 14484
		private bool cwauto;

		// Token: 0x04003895 RID: 14485
		private bool chauto;

		// Token: 0x04003896 RID: 14486
		private int spaceBefore;

		// Token: 0x04003897 RID: 14487
		private int spaceAfter;

		// Token: 0x04003898 RID: 14488
		private int startIndent;

		// Token: 0x04003899 RID: 14489
		private int endIndent;

		// Token: 0x0400389A RID: 14490
		private ForeignObjectArea areaCurrent;

		// Token: 0x020013E2 RID: 5090
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D205 RID: 53765 RVA: 0x002E8BD5 File Offset: 0x002E6DD5
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new InstreamForeignObject(parent, propertyList);
			}
		}
	}
}
