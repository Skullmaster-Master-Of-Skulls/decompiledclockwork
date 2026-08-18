using System;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002FD RID: 765
	internal class OracleLpRelativeTextFragment : OracleLpTextFragment
	{
		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001B62 RID: 7010 RVA: 0x0010D4BC File Offset: 0x0010B6BC
		public override OracleLpTextFragment RelativeFragment
		{
			get
			{
				return this.m_vRelativeFragment;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x0010D4C4 File Offset: 0x0010B6C4
		public override int Start
		{
			get
			{
				return this.m_vAbsoluteStart;
			}
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x0010D4CC File Offset: 0x0010B6CC
		public OracleLpRelativeTextFragment(OracleLpTextFragment fragment, int relativeStart, int length) : base(null, relativeStart, length)
		{
			this.m_vRelativeFragment = fragment;
			this.m_vStart = relativeStart;
			this.m_vLength = length;
			this.m_vAbsoluteStart = relativeStart + fragment.Start;
			this.m_vReferenceText = fragment.ReferenceText;
		}

		// Token: 0x04001D4E RID: 7502
		protected OracleLpTextFragment m_vRelativeFragment;

		// Token: 0x04001D4F RID: 7503
		protected int m_vAbsoluteStart;
	}
}
