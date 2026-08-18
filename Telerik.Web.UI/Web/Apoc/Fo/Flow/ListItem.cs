using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013E7 RID: 5095
	internal class ListItem : FObj
	{
		// Token: 0x0600D212 RID: 53778 RVA: 0x002E9278 File Offset: 0x002E7478
		public new static FObj.Maker GetMaker()
		{
			return new ListItem.Maker();
		}

		// Token: 0x0600D213 RID: 53779 RVA: 0x002E927F File Offset: 0x002E747F
		public ListItem(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:list-item";
		}

		// Token: 0x0600D214 RID: 53780 RVA: 0x002E9294 File Offset: 0x002E7494
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
				this.spaceBefore = this.properties.GetProperty("space-before.optimum").GetLength().MValue();
				this.spaceAfter = this.properties.GetProperty("space-after.optimum").GetLength().MValue();
				this.id = this.properties.GetProperty("id").GetString();
				area.getIDReferences().CreateID(this.id);
				this.marker = 0;
			}
			if (flag)
			{
				area.end();
			}
			if (this.spaceBefore != 0)
			{
				area.addDisplaySpace(this.spaceBefore);
			}
			this.blockArea = new BlockArea(this.propMgr.GetFontState(area.getFontInfo()), area.getAllocationWidth(), area.spaceLeft(), 0, 0, 0, this.align, this.alignLast, this.lineHeight);
			this.blockArea.setTableCellXOffset(area.getTableCellXOffset());
			this.blockArea.setGeneratedBy(this);
			this.areasGenerated++;
			if (this.areasGenerated == 1)
			{
				this.blockArea.isFirst(true);
			}
			this.blockArea.addLineagePair(this, this.areasGenerated);
			this.blockArea.setParent(area);
			this.blockArea.setPage(area.getPage());
			this.blockArea.start();
			this.blockArea.setAbsoluteHeight(area.getAbsoluteHeight());
			this.blockArea.setIDReferences(area.getIDReferences());
			int count = this.children.Count;
			if (count != 2)
			{
				throw new ApocException("list-item must have exactly two children");
			}
			ListItemLabel listItemLabel = (ListItemLabel)this.children[0];
			ListItemBody listItemBody = (ListItemBody)this.children[1];
			Status result;
			if (this.marker == 0)
			{
				area.getIDReferences().ConfigureID(this.id, area);
				result = listItemLabel.Layout(this.blockArea);
				if (result.isIncomplete())
				{
					return result;
				}
			}
			result = listItemBody.Layout(this.blockArea);
			if (result.isIncomplete())
			{
				this.blockArea.end();
				area.addChild(this.blockArea);
				area.increaseHeight(this.blockArea.GetHeight());
				this.marker = 1;
				return result;
			}
			this.blockArea.end();
			area.addChild(this.blockArea);
			area.increaseHeight(this.blockArea.GetHeight());
			if (this.spaceAfter != 0)
			{
				area.addDisplaySpace(this.spaceAfter);
			}
			if (flag)
			{
				area.start();
			}
			this.blockArea.isLast(true);
			return new Status(1);
		}

		// Token: 0x0600D215 RID: 53781 RVA: 0x002E95D5 File Offset: 0x002E77D5
		public override int GetContentWidth()
		{
			if (this.blockArea != null)
			{
				return this.blockArea.getContentWidth();
			}
			return 0;
		}

		// Token: 0x040038A2 RID: 14498
		private int align;

		// Token: 0x040038A3 RID: 14499
		private int alignLast;

		// Token: 0x040038A4 RID: 14500
		private int lineHeight;

		// Token: 0x040038A5 RID: 14501
		private int spaceBefore;

		// Token: 0x040038A6 RID: 14502
		private int spaceAfter;

		// Token: 0x040038A7 RID: 14503
		private string id;

		// Token: 0x040038A8 RID: 14504
		private BlockArea blockArea;

		// Token: 0x020013E8 RID: 5096
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D216 RID: 53782 RVA: 0x002E95EC File Offset: 0x002E77EC
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new ListItem(parent, propertyList);
			}
		}
	}
}
