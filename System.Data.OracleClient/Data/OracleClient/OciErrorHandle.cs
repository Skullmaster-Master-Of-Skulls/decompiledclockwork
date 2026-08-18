using System;

namespace System.Data.OracleClient
{
	// Token: 0x0200003A RID: 58
	internal sealed class OciErrorHandle : OciHandle
	{
		// Token: 0x060001FD RID: 509 RVA: 0x0005C634 File Offset: 0x0005BA34
		internal OciErrorHandle(OciHandle parent) : base(parent, OCI.HTYPE.OCI_HTYPE_ERROR)
		{
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0005C654 File Offset: 0x0005BA54
		// (set) Token: 0x060001FF RID: 511 RVA: 0x0005C674 File Offset: 0x0005BA74
		internal bool ConnectionIsBroken
		{
			get
			{
				return this._connectionIsBroken;
			}
			set
			{
				this._connectionIsBroken = value;
			}
		}

		// Token: 0x04000324 RID: 804
		private bool _connectionIsBroken;
	}
}
