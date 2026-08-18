using System;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002FC RID: 764
	public class OracleLpTextFragment
	{
		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001B58 RID: 7000 RVA: 0x0010D414 File Offset: 0x0010B614
		public virtual OracleLpTextFragment RelativeFragment
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001B59 RID: 7001 RVA: 0x0010D418 File Offset: 0x0010B618
		public string ReferenceText
		{
			get
			{
				return this.m_vReferenceText;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x0010D420 File Offset: 0x0010B620
		public string Fragment
		{
			get
			{
				if (this.m_vFragment == null)
				{
					this.m_vFragment = this.m_vReferenceText.Substring(this.Start, this.m_vLength);
				}
				return this.m_vFragment;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001B5B RID: 7003 RVA: 0x0010D450 File Offset: 0x0010B650
		public int Length
		{
			get
			{
				return this.m_vLength;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x0010D458 File Offset: 0x0010B658
		public virtual int Start
		{
			get
			{
				return this.m_vStart;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001B5D RID: 7005 RVA: 0x0010D460 File Offset: 0x0010B660
		public virtual int RelativeStart
		{
			get
			{
				return this.m_vStart;
			}
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x0010D468 File Offset: 0x0010B668
		internal OracleLpTextFragment(string text, int start, int length)
		{
			this.m_vReferenceText = text;
			this.m_vStart = start;
			this.m_vLength = length;
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x0010D488 File Offset: 0x0010B688
		internal OracleLpRelativeTextFragment GetRelativeTextFragment(int absoluteStart, int length)
		{
			return new OracleLpRelativeTextFragment(this, absoluteStart - this.Start, length);
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x0010D49C File Offset: 0x0010B69C
		internal OracleLpTextFragment GetAbsoluteTextFragment(int relativeStart, int length)
		{
			return new OracleLpTextFragment(this.m_vReferenceText, relativeStart + this.Start, length);
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x0010D4B4 File Offset: 0x0010B6B4
		public override string ToString()
		{
			return this.Fragment;
		}

		// Token: 0x04001D4A RID: 7498
		protected string m_vReferenceText;

		// Token: 0x04001D4B RID: 7499
		protected string m_vFragment;

		// Token: 0x04001D4C RID: 7500
		protected int m_vLength;

		// Token: 0x04001D4D RID: 7501
		protected int m_vStart;
	}
}
