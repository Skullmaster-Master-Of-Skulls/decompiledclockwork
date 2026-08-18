using System;

namespace OracleInternal.BinXml
{
	// Token: 0x02000014 RID: 20
	internal class DTDElementInfo : ObxmlStateObject
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000037A4 File Offset: 0x000019A4
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x000037AC File Offset: 0x000019AC
		internal string ElementName { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000037B8 File Offset: 0x000019B8
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x000037C0 File Offset: 0x000019C0
		internal string ContentSpec { get; set; }

		// Token: 0x060000BA RID: 186 RVA: 0x000037CC File Offset: 0x000019CC
		internal DTDElementInfo()
		{
			this.ClearStateObject();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000037DC File Offset: 0x000019DC
		internal override void ClearStateObject()
		{
			this.ElementName = null;
			this.ContentSpec = null;
		}
	}
}
