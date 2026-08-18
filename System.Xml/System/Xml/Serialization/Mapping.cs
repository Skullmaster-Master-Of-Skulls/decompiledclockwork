using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002C5 RID: 709
	internal abstract class Mapping
	{
		// Token: 0x060021A6 RID: 8614 RVA: 0x0009F1E3 File Offset: 0x0009E1E3
		internal Mapping()
		{
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060021A7 RID: 8615 RVA: 0x0009F1EB File Offset: 0x0009E1EB
		// (set) Token: 0x060021A8 RID: 8616 RVA: 0x0009F1F3 File Offset: 0x0009E1F3
		internal bool IsSoap
		{
			get
			{
				return this.isSoap;
			}
			set
			{
				this.isSoap = value;
			}
		}

		// Token: 0x0400146E RID: 5230
		private bool isSoap;
	}
}
