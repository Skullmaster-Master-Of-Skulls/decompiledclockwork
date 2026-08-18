using System;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015FB RID: 5627
	internal class TextState
	{
		// Token: 0x0600DB6A RID: 56170 RVA: 0x003001E8 File Offset: 0x002FE3E8
		public bool getUnderlined()
		{
			return this.underlined;
		}

		// Token: 0x0600DB6B RID: 56171 RVA: 0x003001F0 File Offset: 0x002FE3F0
		public void setUnderlined(bool ul)
		{
			this.underlined = ul;
		}

		// Token: 0x0600DB6C RID: 56172 RVA: 0x003001F9 File Offset: 0x002FE3F9
		public bool getOverlined()
		{
			return this.overlined;
		}

		// Token: 0x0600DB6D RID: 56173 RVA: 0x00300201 File Offset: 0x002FE401
		public void setOverlined(bool ol)
		{
			this.overlined = ol;
		}

		// Token: 0x0600DB6E RID: 56174 RVA: 0x0030020A File Offset: 0x002FE40A
		public bool getLineThrough()
		{
			return this.linethrough;
		}

		// Token: 0x0600DB6F RID: 56175 RVA: 0x00300212 File Offset: 0x002FE412
		public void setLineThrough(bool lt)
		{
			this.linethrough = lt;
		}

		// Token: 0x04003D5C RID: 15708
		protected bool underlined;

		// Token: 0x04003D5D RID: 15709
		protected bool overlined;

		// Token: 0x04003D5E RID: 15710
		protected bool linethrough;
	}
}
