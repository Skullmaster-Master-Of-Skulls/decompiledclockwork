using System;

namespace OracleInternal.BinXml
{
	// Token: 0x02000010 RID: 16
	internal class ObxmlSectionHeader : ObxmlStateObject
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000345C File Offset: 0x0000165C
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003464 File Offset: 0x00001664
		internal byte Version { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003470 File Offset: 0x00001670
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003478 File Offset: 0x00001678
		internal byte Flags { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003484 File Offset: 0x00001684
		// (set) Token: 0x0600008A RID: 138 RVA: 0x0000348C File Offset: 0x0000168C
		internal byte[] DocId { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003498 File Offset: 0x00001698
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000034A0 File Offset: 0x000016A0
		internal byte[] Rguid { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000034AC File Offset: 0x000016AC
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000034B4 File Offset: 0x000016B4
		internal byte[] PathId { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000034C0 File Offset: 0x000016C0
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000034C8 File Offset: 0x000016C8
		internal bool BigEflt { get; set; }

		// Token: 0x06000091 RID: 145 RVA: 0x000034D4 File Offset: 0x000016D4
		internal ObxmlSectionHeader()
		{
			this.ClearStateObject();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000034E4 File Offset: 0x000016E4
		internal override void ClearStateObject()
		{
			this.Version = 0;
			this.Flags = 0;
			this.DocId = null;
			this.Rguid = null;
			this.PathId = null;
			this.BigEflt = false;
		}
	}
}
