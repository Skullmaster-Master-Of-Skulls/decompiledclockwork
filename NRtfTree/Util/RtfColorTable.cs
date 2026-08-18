using System;
using System.Collections.Generic;
using System.Drawing;

namespace Net.Sgoliver.NRtfTree.Util
{
	// Token: 0x02000008 RID: 8
	public class RtfColorTable
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00003E5E File Offset: 0x0000205E
		public RtfColorTable()
		{
			this.colors = new List<int>();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003E71 File Offset: 0x00002071
		public void AddColor(Color color)
		{
			this.colors.Add(color.ToArgb());
		}

		// Token: 0x1700003A RID: 58
		public Color this[int index]
		{
			get
			{
				return Color.FromArgb(this.colors[index]);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003E98 File Offset: 0x00002098
		public int Count
		{
			get
			{
				return this.colors.Count;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003EA5 File Offset: 0x000020A5
		public int IndexOf(Color color)
		{
			return this.colors.IndexOf(color.ToArgb());
		}

		// Token: 0x0400002C RID: 44
		private List<int> colors;
	}
}
