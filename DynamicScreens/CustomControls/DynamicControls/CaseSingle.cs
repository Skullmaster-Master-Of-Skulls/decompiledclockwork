using System;
using System.Collections.Generic;
using System.Data;

namespace DynamicScreens.CustomControls.DynamicControls
{
	// Token: 0x02000059 RID: 89
	public class CaseSingle
	{
		// Token: 0x060004C5 RID: 1221 RVA: 0x0003FDE6 File Offset: 0x0003EDE6
		public CaseSingle(int infoPcPid, DataTable casePeople)
		{
			this.infoPcPid = infoPcPid;
			this.casePeople = casePeople;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0003FE00 File Offset: 0x0003EE00
		public int InfoPcPid
		{
			get
			{
				return this.infoPcPid;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0003FE18 File Offset: 0x0003EE18
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x0003FE30 File Offset: 0x0003EE30
		public DataTable CasePeople
		{
			get
			{
				return this.casePeople;
			}
			set
			{
				this.casePeople = value;
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0003FE3C File Offset: 0x0003EE3C
		public static CaseSingle FindCaseSingle(List<CaseSingle> cases, int infoPcPid)
		{
			foreach (CaseSingle caseSingle in cases)
			{
				if (caseSingle.InfoPcPid == infoPcPid)
				{
					return caseSingle;
				}
			}
			return null;
		}

		// Token: 0x04000364 RID: 868
		private int infoPcPid;

		// Token: 0x04000365 RID: 869
		private DataTable casePeople;
	}
}
