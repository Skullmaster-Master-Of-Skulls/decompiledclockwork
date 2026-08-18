using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013DB RID: 5083
	internal class FootnoteBody : FObj
	{
		// Token: 0x0600D1F4 RID: 53748 RVA: 0x002E83D5 File Offset: 0x002E65D5
		public new static FObj.Maker GetMaker()
		{
			return new FootnoteBody.Maker();
		}

		// Token: 0x0600D1F5 RID: 53749 RVA: 0x002E83DC File Offset: 0x002E65DC
		public FootnoteBody(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:footnote-body";
			this.areaClass = AreaClass.setAreaClass(AreaClass.XSL_FOOTNOTE);
		}

		// Token: 0x0600D1F6 RID: 53750 RVA: 0x002E8404 File Offset: 0x002E6604
		public override Status Layout(Area area)
		{
			if (this.marker == -1000)
			{
				this.marker = 0;
			}
			BlockArea blockArea = new BlockArea(this.propMgr.GetFontState(area.getFontInfo()), area.getAllocationWidth(), area.spaceLeft(), this.startIndent, this.endIndent, this.textIndent, this.align, this.alignLast, this.lineHeight);
			blockArea.setGeneratedBy(this);
			blockArea.isFirst(true);
			blockArea.setParent(area);
			blockArea.setPage(area.getPage());
			blockArea.start();
			blockArea.setAbsoluteHeight(area.getAbsoluteHeight());
			blockArea.setIDReferences(area.getIDReferences());
			blockArea.setTableCellXOffset(area.getTableCellXOffset());
			int count = this.children.Count;
			for (int i = this.marker; i < count; i++)
			{
				FONode fonode = (FONode)this.children[i];
				Status status;
				Status result = status = fonode.Layout(blockArea);
				if (status.isIncomplete())
				{
					this.ResetMarker();
					return result;
				}
			}
			blockArea.end();
			area.addChild(blockArea);
			area.increaseHeight(blockArea.GetHeight());
			blockArea.isLast(true);
			return new Status(1);
		}

		// Token: 0x04003885 RID: 14469
		private int align;

		// Token: 0x04003886 RID: 14470
		private int alignLast;

		// Token: 0x04003887 RID: 14471
		private int lineHeight;

		// Token: 0x04003888 RID: 14472
		private int startIndent;

		// Token: 0x04003889 RID: 14473
		private int endIndent;

		// Token: 0x0400388A RID: 14474
		private int textIndent;

		// Token: 0x020013DC RID: 5084
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D1F7 RID: 53751 RVA: 0x002E852A File Offset: 0x002E672A
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new FootnoteBody(parent, propertyList);
			}
		}
	}
}
