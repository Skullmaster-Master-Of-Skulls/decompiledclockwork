using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x0200140B RID: 5131
	internal class TableCell : FObj
	{
		// Token: 0x0600D288 RID: 53896 RVA: 0x002EB4FD File Offset: 0x002E96FD
		public new static FObj.Maker GetMaker()
		{
			return new TableCell.Maker();
		}

		// Token: 0x0600D289 RID: 53897 RVA: 0x002EB504 File Offset: 0x002E9704
		public TableCell(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:table-cell";
			this.DoSetup();
		}

		// Token: 0x0600D28A RID: 53898 RVA: 0x002EB52D File Offset: 0x002E972D
		public void SetStartOffset(int offset)
		{
			this.startOffset = offset;
		}

		// Token: 0x0600D28B RID: 53899 RVA: 0x002EB536 File Offset: 0x002E9736
		public void SetWidth(int width)
		{
			this.width = width;
		}

		// Token: 0x0600D28C RID: 53900 RVA: 0x002EB53F File Offset: 0x002E973F
		public int GetColumnNumber()
		{
			return this.iColNumber;
		}

		// Token: 0x0600D28D RID: 53901 RVA: 0x002EB547 File Offset: 0x002E9747
		public int GetNumColumnsSpanned()
		{
			return this.numColumnsSpanned;
		}

		// Token: 0x0600D28E RID: 53902 RVA: 0x002EB54F File Offset: 0x002E974F
		public int GetNumRowsSpanned()
		{
			return this.numRowsSpanned;
		}

		// Token: 0x0600D28F RID: 53903 RVA: 0x002EB558 File Offset: 0x002E9758
		public void DoSetup()
		{
			this.propMgr.GetAccessibilityProps();
			this.propMgr.GetAuralProps();
			this.propMgr.GetBorderAndPadding();
			this.propMgr.GetBackgroundProps();
			this.propMgr.GetRelativePositionProps();
			this.iColNumber = this.properties.GetProperty("column-number").GetNumber().IntValue();
			if (this.iColNumber < 0)
			{
				this.iColNumber = 0;
			}
			this.numColumnsSpanned = this.properties.GetProperty("number-columns-spanned").GetNumber().IntValue();
			if (this.numColumnsSpanned < 1)
			{
				this.numColumnsSpanned = 1;
			}
			this.numRowsSpanned = this.properties.GetProperty("number-rows-spanned").GetNumber().IntValue();
			if (this.numRowsSpanned < 1)
			{
				this.numRowsSpanned = 1;
			}
			this.id = this.properties.GetProperty("id").GetString();
			this.bSepBorders = (this.properties.GetProperty("border-collapse").GetEnum() == 68);
			this.CalcBorders(this.propMgr.GetBorderAndPadding());
			this.verticalAlign = this.properties.GetProperty("display-align").GetEnum();
			if (this.verticalAlign == 7)
			{
				this.bRelativeAlign = true;
				this.verticalAlign = this.properties.GetProperty("relative-align").GetEnum();
			}
			else
			{
				this.bRelativeAlign = false;
			}
			this.minCellHeight = this.properties.GetProperty("height").GetLength().MValue();
		}

		// Token: 0x0600D290 RID: 53904 RVA: 0x002EB6EC File Offset: 0x002E98EC
		public override Status Layout(Area area)
		{
			area.getAbsoluteHeight();
			if (this.marker == -1001)
			{
				return new Status(1);
			}
			if (this.marker == -1000)
			{
				area.getIDReferences().CreateID(this.id);
				this.marker = 0;
				this.bDone = false;
			}
			if (this.marker == 0)
			{
				area.getIDReferences().ConfigureID(this.id, area);
			}
			int num = area.spaceLeft() - this.m_borderSeparation;
			this.cellArea = new AreaContainer(this.propMgr.GetFontState(area.getFontInfo()), this.startOffset + this.startAdjust, this.beforeOffset, this.width - this.widthAdjust, num, 61);
			this.cellArea.foCreator = this;
			this.cellArea.setPage(area.getPage());
			this.cellArea.setParent(area);
			this.cellArea.setBorderAndPadding((BorderAndPadding)this.propMgr.GetBorderAndPadding().Clone());
			this.cellArea.setBackground(this.propMgr.GetBackgroundProps());
			this.cellArea.start();
			this.cellArea.setAbsoluteHeight(area.getAbsoluteHeight());
			this.cellArea.setIDReferences(area.getIDReferences());
			this.cellArea.setTableCellXOffset(this.startOffset + this.startAdjust);
			int count = this.children.Count;
			int num2 = this.marker;
			while (!this.bDone && num2 < count)
			{
				FObj fobj = (FObj)this.children[num2];
				fobj.SetIsInTableCell();
				fobj.ForceWidth(this.width);
				this.marker = num2;
				Status status2;
				Status status = status2 = fobj.Layout(this.cellArea);
				if (status2.isIncomplete())
				{
					if (num2 == 0 && status.getCode() == 2)
					{
						return new Status(2);
					}
					area.addChild(this.cellArea);
					return new Status(3);
				}
				else
				{
					area.setMaxHeight(area.getMaxHeight() - num + this.cellArea.getMaxHeight());
					num2++;
				}
			}
			this.bDone = true;
			this.cellArea.end();
			area.addChild(this.cellArea);
			if (this.minCellHeight > this.cellArea.getContentHeight())
			{
				this.cellArea.SetHeight(this.minCellHeight);
			}
			this.height = this.cellArea.GetHeight();
			this.top = this.cellArea.GetCurrentYPosition();
			return new Status(1);
		}

		// Token: 0x0600D291 RID: 53905 RVA: 0x002EB966 File Offset: 0x002E9B66
		public int GetHeight()
		{
			return this.cellArea.GetHeight() + this.m_borderSeparation - this.borderHeight;
		}

		// Token: 0x0600D292 RID: 53906 RVA: 0x002EB984 File Offset: 0x002E9B84
		public void SetRowHeight(int h)
		{
			int num = h - this.GetHeight();
			if (this.bRelativeAlign)
			{
				this.cellArea.increaseHeight(num);
				return;
			}
			if (num > 0)
			{
				BorderAndPadding borderAndPadding = this.cellArea.GetBorderAndPadding();
				int num2 = this.verticalAlign;
				if (num2 == 2)
				{
					borderAndPadding.setPaddingLength(0, borderAndPadding.getPaddingTop(false) + num);
					this.cellArea.shiftYPosition(num);
					return;
				}
				if (num2 != 9)
				{
					if (num2 != 13)
					{
						return;
					}
					this.cellArea.shiftYPosition(num / 2);
					borderAndPadding.setPaddingLength(0, borderAndPadding.getPaddingTop(false) + num / 2);
					borderAndPadding.setPaddingLength(2, borderAndPadding.getPaddingBottom(false) + num - num / 2);
					return;
				}
				else
				{
					borderAndPadding.setPaddingLength(2, borderAndPadding.getPaddingBottom(false) + num);
				}
			}
		}

		// Token: 0x0600D293 RID: 53907 RVA: 0x002EBA3C File Offset: 0x002E9C3C
		private void CalcBorders(BorderAndPadding bp)
		{
			if (this.bSepBorders)
			{
				int num = this.properties.GetProperty("border-separation.inline-progression-direction").GetLength().MValue();
				this.startAdjust = num / 2 + bp.getBorderLeftWidth(false) + bp.getPaddingLeft(false);
				this.widthAdjust = this.startAdjust + num - num / 2 + bp.getBorderRightWidth(false) + bp.getPaddingRight(false);
				this.m_borderSeparation = this.properties.GetProperty("border-separation.block-progression-direction").GetLength().MValue();
				this.beforeOffset = this.m_borderSeparation / 2 + bp.getBorderTopWidth(false) + bp.getPaddingTop(false);
				return;
			}
			int borderLeftWidth = bp.getBorderLeftWidth(false);
			int borderRightWidth = bp.getBorderRightWidth(false);
			int borderTopWidth = bp.getBorderTopWidth(false);
			int borderBottomWidth = bp.getBorderBottomWidth(false);
			this.startAdjust = borderLeftWidth / 2 + bp.getPaddingLeft(false);
			this.widthAdjust = this.startAdjust + borderRightWidth / 2 + bp.getPaddingRight(false);
			this.beforeOffset = borderTopWidth / 2 + bp.getPaddingTop(false);
			this.borderHeight = (borderTopWidth + borderBottomWidth) / 2;
		}

		// Token: 0x040038DB RID: 14555
		private string id;

		// Token: 0x040038DC RID: 14556
		private int numColumnsSpanned;

		// Token: 0x040038DD RID: 14557
		private int numRowsSpanned;

		// Token: 0x040038DE RID: 14558
		private int iColNumber = -1;

		// Token: 0x040038DF RID: 14559
		protected int startOffset;

		// Token: 0x040038E0 RID: 14560
		protected int width;

		// Token: 0x040038E1 RID: 14561
		protected int beforeOffset;

		// Token: 0x040038E2 RID: 14562
		protected int startAdjust;

		// Token: 0x040038E3 RID: 14563
		protected int widthAdjust;

		// Token: 0x040038E4 RID: 14564
		protected int borderHeight;

		// Token: 0x040038E5 RID: 14565
		protected int minCellHeight;

		// Token: 0x040038E6 RID: 14566
		protected int height;

		// Token: 0x040038E7 RID: 14567
		protected int top;

		// Token: 0x040038E8 RID: 14568
		protected int verticalAlign;

		// Token: 0x040038E9 RID: 14569
		protected bool bRelativeAlign;

		// Token: 0x040038EA RID: 14570
		private bool bSepBorders = true;

		// Token: 0x040038EB RID: 14571
		private bool bDone;

		// Token: 0x040038EC RID: 14572
		private int m_borderSeparation;

		// Token: 0x040038ED RID: 14573
		private AreaContainer cellArea;

		// Token: 0x0200140C RID: 5132
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D294 RID: 53908 RVA: 0x002EBB50 File Offset: 0x002E9D50
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new TableCell(parent, propertyList);
			}
		}
	}
}
