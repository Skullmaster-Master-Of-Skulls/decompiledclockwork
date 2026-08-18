using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200005D RID: 93
	internal class CssContext
	{
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0001A5C9 File Offset: 0x000187C9
		public Position Start
		{
			get
			{
				return this.m_start;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0001A5D1 File Offset: 0x000187D1
		public Position End
		{
			get
			{
				return this.m_end;
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001A5D9 File Offset: 0x000187D9
		internal CssContext()
		{
			this.m_start = new Position();
			this.m_end = new Position();
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001A5F7 File Offset: 0x000187F7
		internal CssContext(Position start, Position end)
		{
			this.m_start = start.Clone();
			this.m_end = end.Clone();
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001A617 File Offset: 0x00018817
		public void Advance()
		{
			this.m_start = this.m_end.Clone();
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001A62A File Offset: 0x0001882A
		public CssContext Clone()
		{
			return new CssContext(this.m_start.Clone(), this.m_end.Clone());
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001A647 File Offset: 0x00018847
		public void Reset(int line, int column)
		{
			this.m_start = new Position(line, column);
			this.m_end = new Position(line, column);
		}

		// Token: 0x040001EF RID: 495
		private Position m_start;

		// Token: 0x040001F0 RID: 496
		private Position m_end;
	}
}
