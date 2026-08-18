using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200005E RID: 94
	internal class Position
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0001A663 File Offset: 0x00018863
		public int Line
		{
			get
			{
				return this.m_line;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0001A66B File Offset: 0x0001886B
		public int Char
		{
			get
			{
				return this.m_char;
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001A673 File Offset: 0x00018873
		public Position()
		{
			this.m_line = 1;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001A682 File Offset: 0x00018882
		public Position(int line, int character)
		{
			this.m_line = line;
			this.m_char = character;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001A698 File Offset: 0x00018898
		public void NextLine()
		{
			this.m_line++;
			this.m_char = 0;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001A6AF File Offset: 0x000188AF
		public void NextChar()
		{
			this.m_char++;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001A6BF File Offset: 0x000188BF
		public void PreviousChar()
		{
			this.m_char--;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001A6CF File Offset: 0x000188CF
		public Position Clone()
		{
			return new Position(this.m_line, this.m_char);
		}

		// Token: 0x040001F1 RID: 497
		private int m_line;

		// Token: 0x040001F2 RID: 498
		private int m_char;
	}
}
