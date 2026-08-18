using System;

namespace Telerik.Charting
{
	// Token: 0x0200171C RID: 5916
	internal class ChartWord
	{
		// Token: 0x17004602 RID: 17922
		// (get) Token: 0x0600E5DE RID: 58846 RVA: 0x00330D5D File Offset: 0x0032EF5D
		// (set) Token: 0x0600E5DF RID: 58847 RVA: 0x00330D65 File Offset: 0x0032EF65
		internal ChartWordCollection Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x17004603 RID: 17923
		// (get) Token: 0x0600E5E0 RID: 58848 RVA: 0x00330D6E File Offset: 0x0032EF6E
		internal float Width
		{
			get
			{
				return this.width;
			}
		}

		// Token: 0x17004604 RID: 17924
		// (get) Token: 0x0600E5E1 RID: 58849 RVA: 0x00330D76 File Offset: 0x0032EF76
		internal string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x0600E5E2 RID: 58850 RVA: 0x00330D7E File Offset: 0x0032EF7E
		internal ChartWord()
		{
			this.width = 0f;
			this.text = "";
		}

		// Token: 0x0600E5E3 RID: 58851 RVA: 0x00330D9C File Offset: 0x0032EF9C
		internal ChartWord(string text, float width) : this()
		{
			this.text = text;
			this.width = width;
		}

		// Token: 0x0600E5E4 RID: 58852 RVA: 0x00330DB4 File Offset: 0x0032EFB4
		internal ChartWord Clone()
		{
			return (ChartWord)base.MemberwiseClone();
		}

		// Token: 0x0400421E RID: 16926
		private float width;

		// Token: 0x0400421F RID: 16927
		private string text;

		// Token: 0x04004220 RID: 16928
		private ChartWordCollection parent;
	}
}
