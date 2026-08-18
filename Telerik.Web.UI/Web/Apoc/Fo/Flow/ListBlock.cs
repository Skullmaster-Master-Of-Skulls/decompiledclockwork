using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013E5 RID: 5093
	internal class ListBlock : FObj
	{
		// Token: 0x0600D20D RID: 53773 RVA: 0x002E8EC8 File Offset: 0x002E70C8
		public new static FObj.Maker GetMaker()
		{
			return new ListBlock.Maker();
		}

		// Token: 0x0600D20E RID: 53774 RVA: 0x002E8ECF File Offset: 0x002E70CF
		public ListBlock(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:list-block";
		}

		// Token: 0x0600D20F RID: 53775 RVA: 0x002E8EE4 File Offset: 0x002E70E4
		public override Status Layout(Area area)
		{
			bool flag = area is BlockArea;
			if (this.marker == -1000)
			{
				this.propMgr.GetAccessibilityProps();
				this.propMgr.GetAuralProps();
				this.propMgr.GetBorderAndPadding();
				this.propMgr.GetBackgroundProps();
				this.propMgr.GetMarginProps();
				this.propMgr.GetRelativePositionProps();
				this.align = this.properties.GetProperty("text-align").GetEnum();
				this.alignLast = this.properties.GetProperty("text-align-last").GetEnum();
				this.lineHeight = this.properties.GetProperty("line-height").GetLength().MValue();
				this.startIndent = this.properties.GetProperty("start-indent").GetLength().MValue();
				this.endIndent = this.properties.GetProperty("end-indent").GetLength().MValue();
				this.spaceBefore = this.properties.GetProperty("space-before.optimum").GetLength().MValue();
				this.spaceAfter = this.properties.GetProperty("space-after.optimum").GetLength().MValue();
				this.marker = 0;
				if (flag)
				{
					area.end();
				}
				if (this.spaceBefore != 0)
				{
					area.addDisplaySpace(this.spaceBefore);
				}
				if (this.isInTableCell)
				{
					this.startIndent += this.forcedStartOffset;
					this.endIndent += area.getAllocationWidth() - this.forcedWidth - this.forcedStartOffset;
				}
				string @string = this.properties.GetProperty("id").GetString();
				area.getIDReferences().InitializeID(@string, area);
			}
			BlockArea blockArea = new BlockArea(this.propMgr.GetFontState(area.getFontInfo()), area.getAllocationWidth(), area.spaceLeft(), this.startIndent, this.endIndent, 0, this.align, this.alignLast, this.lineHeight);
			blockArea.setTableCellXOffset(area.getTableCellXOffset());
			blockArea.setGeneratedBy(this);
			this.areasGenerated++;
			if (this.areasGenerated == 1)
			{
				blockArea.isFirst(true);
			}
			blockArea.addLineagePair(this, this.areasGenerated);
			blockArea.setParent(area);
			blockArea.setPage(area.getPage());
			blockArea.setBackground(this.propMgr.GetBackgroundProps());
			blockArea.start();
			blockArea.setAbsoluteHeight(area.getAbsoluteHeight());
			blockArea.setIDReferences(area.getIDReferences());
			int count = this.children.Count;
			for (int i = this.marker; i < count; i++)
			{
				if (!(this.children[i] is ListItem))
				{
					ApocDriver.ActiveDriver.FireApocError("Children of list-blocks must be list-items");
					return new Status(1);
				}
				ListItem listItem = (ListItem)this.children[i];
				Status status;
				Status result = status = listItem.Layout(blockArea);
				if (status.isIncomplete())
				{
					if (result.getCode() == 2 && i > 0)
					{
						result = new Status(3);
					}
					this.marker = i;
					blockArea.end();
					area.addChild(blockArea);
					area.increaseHeight(blockArea.GetHeight());
					return result;
				}
			}
			blockArea.end();
			area.addChild(blockArea);
			area.increaseHeight(blockArea.GetHeight());
			if (this.spaceAfter != 0)
			{
				area.addDisplaySpace(this.spaceAfter);
			}
			if (flag)
			{
				area.start();
			}
			blockArea.isLast(true);
			return new Status(1);
		}

		// Token: 0x0400389B RID: 14491
		private int align;

		// Token: 0x0400389C RID: 14492
		private int alignLast;

		// Token: 0x0400389D RID: 14493
		private int lineHeight;

		// Token: 0x0400389E RID: 14494
		private int startIndent;

		// Token: 0x0400389F RID: 14495
		private int endIndent;

		// Token: 0x040038A0 RID: 14496
		private int spaceBefore;

		// Token: 0x040038A1 RID: 14497
		private int spaceAfter;

		// Token: 0x020013E6 RID: 5094
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D210 RID: 53776 RVA: 0x002E9267 File Offset: 0x002E7467
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new ListBlock(parent, propertyList);
			}
		}
	}
}
