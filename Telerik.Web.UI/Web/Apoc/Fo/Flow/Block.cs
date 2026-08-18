using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013CD RID: 5069
	internal class Block : FObjMixed
	{
		// Token: 0x0600D1C0 RID: 53696 RVA: 0x002E6AD4 File Offset: 0x002E4CD4
		public new static FObj.Maker GetMaker()
		{
			return new Block.Maker();
		}

		// Token: 0x0600D1C1 RID: 53697 RVA: 0x002E6ADC File Offset: 0x002E4CDC
		public Block(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:block";
			string name;
			switch (name = parent.GetName())
			{
			case "fo:basic-link":
			case "fo:block":
			case "fo:block-container":
			case "fo:float":
			case "fo:flow":
			case "fo:footnote-body":
			case "fo:inline":
			case "fo:inline-container":
			case "fo:list-item-body":
			case "fo:list-item-label":
			case "fo:marker":
			case "fo:multi-case":
			case "fo:static-content":
			case "fo:table-caption":
			case "fo:table-cell":
			case "fo:wrapper":
				this.span = this.properties.GetProperty("span").GetEnum();
				this.ts = this.propMgr.getTextDecoration(parent);
				return;
			}
			throw new ApocException("fo:block must be child of fo:basic-link, fo:block, fo:block-container, fo:float, fo:flow, fo:footnote-body, fo:inline, fo:inline-container, fo:list-item-body, fo:list-item-label, fo:marker, fo:multi-case, fo:static-content, fo:table-caption, fo:table-cell or fo:wrapper not " + parent.GetName());
		}

		// Token: 0x0600D1C2 RID: 53698 RVA: 0x002E6C84 File Offset: 0x002E4E84
		public override Status Layout(Area area)
		{
			if (this.marker == -1001)
			{
				return new Status(1);
			}
			bool flag = area is BlockArea;
			if (this.marker == -1000)
			{
				this.propMgr.GetAccessibilityProps();
				this.propMgr.GetAuralProps();
				this.propMgr.GetBorderAndPadding();
				this.propMgr.GetBackgroundProps();
				this.propMgr.GetHyphenationProps();
				this.propMgr.GetMarginProps();
				this.propMgr.GetRelativePositionProps();
				this.align = this.properties.GetProperty("text-align").GetEnum();
				this.alignLast = this.properties.GetProperty("text-align-last").GetEnum();
				this.breakAfter = this.properties.GetProperty("break-after").GetEnum();
				this.lineHeight = this.properties.GetProperty("line-height").GetLength().MValue();
				this.startIndent = this.properties.GetProperty("start-indent").GetLength().MValue();
				this.endIndent = this.properties.GetProperty("end-indent").GetLength().MValue();
				this.spaceBefore = this.properties.GetProperty("space-before.optimum").GetLength().MValue();
				this.spaceAfter = this.properties.GetProperty("space-after.optimum").GetLength().MValue();
				this.textIndent = this.properties.GetProperty("text-indent").GetLength().MValue();
				this.keepWithNext = this.properties.GetProperty("keep-with-next").GetEnum();
				this.blockWidows = this.properties.GetProperty("widows").GetNumber().IntValue();
				this.blockOrphans = this.properties.GetProperty("orphans").GetNumber().IntValue();
				this.id = this.properties.GetProperty("id").GetString();
				if (flag)
				{
					area.end();
				}
				if (area.getIDReferences() != null)
				{
					area.getIDReferences().CreateID(this.id);
				}
				this.marker = 0;
				int num = this.propMgr.CheckBreakBefore(area);
				if (num != 1)
				{
					return new Status(num);
				}
				int count = this.children.Count;
				for (int i = 0; i < count; i++)
				{
					FONode fonode = (FONode)this.children[i];
					FOText fotext = fonode as FOText;
					if (fotext == null)
					{
						fonode.SetWidows(this.blockWidows);
						break;
					}
					if (fotext.willCreateArea())
					{
						fonode.SetWidows(this.blockWidows);
						break;
					}
					this.children.RemoveAt(i);
					count = this.children.Count;
					i--;
				}
				for (int j = count - 1; j >= 0; j--)
				{
					FONode fonode2 = (FONode)this.children[j];
					FOText fotext2 = fonode2 as FOText;
					if (fotext2 == null)
					{
						fonode2.SetOrphans(this.blockOrphans);
						break;
					}
					if (fotext2.willCreateArea())
					{
						fonode2.SetOrphans(this.blockOrphans);
						break;
					}
				}
			}
			if (this.spaceBefore != 0 && this.marker == 0)
			{
				area.addDisplaySpace(this.spaceBefore);
			}
			if (this.anythingLaidOut)
			{
				this.textIndent = 0;
			}
			if (this.marker == 0 && area.getIDReferences() != null)
			{
				area.getIDReferences().ConfigureID(this.id, area);
			}
			int num2 = area.spaceLeft();
			BlockArea blockArea = new BlockArea(this.propMgr.GetFontState(area.getFontInfo()), area.getAllocationWidth(), area.spaceLeft(), this.startIndent, this.endIndent, this.textIndent, this.align, this.alignLast, this.lineHeight);
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
			blockArea.setBorderAndPadding(this.propMgr.GetBorderAndPadding());
			blockArea.setHyphenation(this.propMgr.GetHyphenationProps());
			blockArea.start();
			blockArea.setAbsoluteHeight(area.getAbsoluteHeight());
			blockArea.setIDReferences(area.getIDReferences());
			blockArea.setTableCellXOffset(area.getTableCellXOffset());
			int k = this.marker;
			while (k < this.children.Count)
			{
				FONode fonode3 = (FONode)this.children[k];
				Status status;
				Status result = status = fonode3.Layout(blockArea);
				if (status.isIncomplete())
				{
					this.marker = k;
					if (result.getCode() != 2)
					{
						area.addChild(blockArea);
						area.setMaxHeight(area.getMaxHeight() - num2 + blockArea.getMaxHeight());
						area.increaseHeight(blockArea.GetHeight());
						this.anythingLaidOut = true;
						return result;
					}
					if (k != 0)
					{
						result = new Status(3);
						area.addChild(blockArea);
						area.setMaxHeight(area.getMaxHeight() - num2 + blockArea.getMaxHeight());
						area.increaseHeight(blockArea.GetHeight());
						this.anythingLaidOut = true;
						return result;
					}
					this.anythingLaidOut = false;
					return result;
				}
				else
				{
					this.anythingLaidOut = true;
					k++;
				}
			}
			blockArea.end();
			area.setMaxHeight(area.getMaxHeight() - num2 + blockArea.getMaxHeight());
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
			this.areaHeight = blockArea.GetHeight();
			this.contentWidth = blockArea.getContentWidth();
			int num3 = this.propMgr.CheckBreakAfter(area);
			if (num3 != 1)
			{
				this.marker = -1001;
				return new Status(num3);
			}
			if (this.keepWithNext != 0)
			{
				return new Status(8);
			}
			blockArea.isLast(true);
			return new Status(1);
		}

		// Token: 0x0600D1C3 RID: 53699 RVA: 0x002E728B File Offset: 0x002E548B
		public int GetAreaHeight()
		{
			return this.areaHeight;
		}

		// Token: 0x0600D1C4 RID: 53700 RVA: 0x002E7293 File Offset: 0x002E5493
		public override int GetContentWidth()
		{
			return this.contentWidth;
		}

		// Token: 0x0600D1C5 RID: 53701 RVA: 0x002E729B File Offset: 0x002E549B
		public int GetSpan()
		{
			return this.span;
		}

		// Token: 0x0600D1C6 RID: 53702 RVA: 0x002E72A3 File Offset: 0x002E54A3
		public override void ResetMarker()
		{
			this.anythingLaidOut = false;
			base.ResetMarker();
		}

		// Token: 0x04003858 RID: 14424
		private int align;

		// Token: 0x04003859 RID: 14425
		private int alignLast;

		// Token: 0x0400385A RID: 14426
		private int breakAfter;

		// Token: 0x0400385B RID: 14427
		private int lineHeight;

		// Token: 0x0400385C RID: 14428
		private int startIndent;

		// Token: 0x0400385D RID: 14429
		private int endIndent;

		// Token: 0x0400385E RID: 14430
		private int spaceBefore;

		// Token: 0x0400385F RID: 14431
		private int spaceAfter;

		// Token: 0x04003860 RID: 14432
		private int textIndent;

		// Token: 0x04003861 RID: 14433
		private int keepWithNext;

		// Token: 0x04003862 RID: 14434
		private int blockWidows;

		// Token: 0x04003863 RID: 14435
		private int blockOrphans;

		// Token: 0x04003864 RID: 14436
		private int areaHeight;

		// Token: 0x04003865 RID: 14437
		private int contentWidth;

		// Token: 0x04003866 RID: 14438
		private string id;

		// Token: 0x04003867 RID: 14439
		private int span;

		// Token: 0x04003868 RID: 14440
		private bool anythingLaidOut;

		// Token: 0x020013CE RID: 5070
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D1C7 RID: 53703 RVA: 0x002E72B2 File Offset: 0x002E54B2
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new Block(parent, propertyList);
			}
		}
	}
}
