using System;
using System.Drawing;
using Telerik.Web.Apoc.Layout.Inline;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015F3 RID: 5619
	internal class LinkedRectangle
	{
		// Token: 0x0600DB0C RID: 56076 RVA: 0x002FF8D9 File Offset: 0x002FDAD9
		public LinkedRectangle(Rectangle link, LineArea lineArea, InlineArea inlineArea)
		{
			this.link = link;
			this.lineArea = lineArea;
			this.inlineArea = inlineArea;
		}

		// Token: 0x0600DB0D RID: 56077 RVA: 0x002FF8F6 File Offset: 0x002FDAF6
		public LinkedRectangle(LinkedRectangle lr)
		{
			this.link = lr.getRectangle();
			this.lineArea = lr.getLineArea();
			this.inlineArea = lr.getInlineArea();
		}

		// Token: 0x0600DB0E RID: 56078 RVA: 0x002FF922 File Offset: 0x002FDB22
		public void setRectangle(Rectangle link)
		{
			this.link = link;
		}

		// Token: 0x0600DB0F RID: 56079 RVA: 0x002FF92B File Offset: 0x002FDB2B
		public Rectangle getRectangle()
		{
			return this.link;
		}

		// Token: 0x0600DB10 RID: 56080 RVA: 0x002FF933 File Offset: 0x002FDB33
		public LineArea getLineArea()
		{
			return this.lineArea;
		}

		// Token: 0x0600DB11 RID: 56081 RVA: 0x002FF93B File Offset: 0x002FDB3B
		public void setLineArea(LineArea lineArea)
		{
			this.lineArea = lineArea;
		}

		// Token: 0x0600DB12 RID: 56082 RVA: 0x002FF944 File Offset: 0x002FDB44
		public InlineArea getInlineArea()
		{
			return this.inlineArea;
		}

		// Token: 0x0600DB13 RID: 56083 RVA: 0x002FF94C File Offset: 0x002FDB4C
		public void setLineArea(InlineArea inlineArea)
		{
			this.inlineArea = inlineArea;
		}

		// Token: 0x0600DB14 RID: 56084 RVA: 0x002FF955 File Offset: 0x002FDB55
		public void setX(int x)
		{
			this.link.X = x;
		}

		// Token: 0x0600DB15 RID: 56085 RVA: 0x002FF963 File Offset: 0x002FDB63
		public void setY(int y)
		{
			this.link.Y = y;
		}

		// Token: 0x0600DB16 RID: 56086 RVA: 0x002FF971 File Offset: 0x002FDB71
		public void SetWidth(int width)
		{
			this.link.Width = width;
		}

		// Token: 0x0600DB17 RID: 56087 RVA: 0x002FF97F File Offset: 0x002FDB7F
		public void SetHeight(int height)
		{
			this.link.Height = height;
		}

		// Token: 0x0600DB18 RID: 56088 RVA: 0x002FF98D File Offset: 0x002FDB8D
		public int getX()
		{
			return this.link.X;
		}

		// Token: 0x0600DB19 RID: 56089 RVA: 0x002FF99A File Offset: 0x002FDB9A
		public int getY()
		{
			return this.link.Y;
		}

		// Token: 0x0600DB1A RID: 56090 RVA: 0x002FF9A7 File Offset: 0x002FDBA7
		public int getWidth()
		{
			return this.link.Width;
		}

		// Token: 0x0600DB1B RID: 56091 RVA: 0x002FF9B4 File Offset: 0x002FDBB4
		public int GetHeight()
		{
			return this.link.Height;
		}

		// Token: 0x04003D1A RID: 15642
		protected Rectangle link;

		// Token: 0x04003D1B RID: 15643
		protected LineArea lineArea;

		// Token: 0x04003D1C RID: 15644
		protected InlineArea inlineArea;
	}
}
