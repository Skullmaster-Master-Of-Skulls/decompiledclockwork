using System;
using System.Collections;
using System.Drawing;
using Telerik.Web.Apoc.Layout.Inline;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015F4 RID: 5620
	internal class LinkSet
	{
		// Token: 0x0600DB1C RID: 56092 RVA: 0x002FF9C1 File Offset: 0x002FDBC1
		public LinkSet(string destination, Area area, int linkType)
		{
			this.destination = destination;
			this.area = area;
			this.linkType = linkType;
		}

		// Token: 0x0600DB1D RID: 56093 RVA: 0x002FF9EC File Offset: 0x002FDBEC
		public void addRect(Rectangle r, LineArea lineArea, InlineArea inlineArea)
		{
			LinkedRectangle linkedRectangle = new LinkedRectangle(r, lineArea, inlineArea);
			linkedRectangle.setY(this.yoffset);
			if (this.yoffset > this.maxY)
			{
				this.maxY = this.yoffset;
			}
			this.rects.Add(linkedRectangle);
		}

		// Token: 0x0600DB1E RID: 56094 RVA: 0x002FFA35 File Offset: 0x002FDC35
		public void setYOffset(int y)
		{
			this.yoffset = y;
		}

		// Token: 0x0600DB1F RID: 56095 RVA: 0x002FFA3E File Offset: 0x002FDC3E
		public void setXOffset(int x)
		{
			this.xoffset = x;
		}

		// Token: 0x0600DB20 RID: 56096 RVA: 0x002FFA47 File Offset: 0x002FDC47
		public void setContentRectangleWidth(int contentRectangleWidth)
		{
			this.contentRectangleWidth = contentRectangleWidth;
		}

		// Token: 0x0600DB21 RID: 56097 RVA: 0x002FFA50 File Offset: 0x002FDC50
		public void applyAreaContainerOffsets(AreaContainer ac, Area area)
		{
			int absoluteHeight = area.getAbsoluteHeight();
			BlockArea blockArea = (BlockArea)area;
			foreach (object obj in this.rects)
			{
				LinkedRectangle linkedRectangle = (LinkedRectangle)obj;
				linkedRectangle.setX(linkedRectangle.getX() + ac.getXPosition() + area.getTableCellXOffset());
				linkedRectangle.setY(ac.GetYPosition() - absoluteHeight + (this.maxY - linkedRectangle.getY()) - blockArea.getHalfLeading());
			}
		}

		// Token: 0x0600DB22 RID: 56098 RVA: 0x002FFAF0 File Offset: 0x002FDCF0
		public void mergeLinks()
		{
			int count = this.rects.Count;
			if (count == 1)
			{
				return;
			}
			LinkedRectangle linkedRectangle = new LinkedRectangle((LinkedRectangle)this.rects[0]);
			ArrayList arrayList = new ArrayList();
			for (int i = 1; i < count; i++)
			{
				LinkedRectangle linkedRectangle2 = (LinkedRectangle)this.rects[i];
				if (linkedRectangle2.getLineArea() == linkedRectangle.getLineArea())
				{
					linkedRectangle.SetWidth(linkedRectangle2.getX() + linkedRectangle2.getWidth() - linkedRectangle.getX());
				}
				else
				{
					arrayList.Add(linkedRectangle);
					linkedRectangle = new LinkedRectangle(linkedRectangle2);
				}
				if (i == count - 1)
				{
					arrayList.Add(linkedRectangle);
				}
			}
			this.rects = arrayList;
		}

		// Token: 0x0600DB23 RID: 56099 RVA: 0x002FFB9C File Offset: 0x002FDD9C
		public void align()
		{
			foreach (object obj in this.rects)
			{
				LinkedRectangle linkedRectangle = (LinkedRectangle)obj;
				linkedRectangle.setX(linkedRectangle.getX() + linkedRectangle.getLineArea().getStartIndent() + linkedRectangle.getInlineArea().getXOffset());
			}
		}

		// Token: 0x0600DB24 RID: 56100 RVA: 0x002FFC14 File Offset: 0x002FDE14
		public string getDest()
		{
			return this.destination;
		}

		// Token: 0x0600DB25 RID: 56101 RVA: 0x002FFC1C File Offset: 0x002FDE1C
		public ArrayList getRects()
		{
			return this.rects;
		}

		// Token: 0x0600DB26 RID: 56102 RVA: 0x002FFC24 File Offset: 0x002FDE24
		public int getEndIndent()
		{
			return this.endIndent;
		}

		// Token: 0x0600DB27 RID: 56103 RVA: 0x002FFC2C File Offset: 0x002FDE2C
		public int getStartIndent()
		{
			return this.startIndent;
		}

		// Token: 0x0600DB28 RID: 56104 RVA: 0x002FFC34 File Offset: 0x002FDE34
		public Area getArea()
		{
			return this.area;
		}

		// Token: 0x0600DB29 RID: 56105 RVA: 0x002FFC3C File Offset: 0x002FDE3C
		public int getLinkType()
		{
			return this.linkType;
		}

		// Token: 0x04003D1D RID: 15645
		public const int INTERNAL = 0;

		// Token: 0x04003D1E RID: 15646
		public const int EXTERNAL = 1;

		// Token: 0x04003D1F RID: 15647
		private string destination;

		// Token: 0x04003D20 RID: 15648
		private ArrayList rects = new ArrayList();

		// Token: 0x04003D21 RID: 15649
		private int xoffset;

		// Token: 0x04003D22 RID: 15650
		private int yoffset;

		// Token: 0x04003D23 RID: 15651
		private int maxY;

		// Token: 0x04003D24 RID: 15652
		protected int startIndent;

		// Token: 0x04003D25 RID: 15653
		protected int endIndent;

		// Token: 0x04003D26 RID: 15654
		private int linkType;

		// Token: 0x04003D27 RID: 15655
		private Area area;

		// Token: 0x04003D28 RID: 15656
		private int contentRectangleWidth;
	}
}
