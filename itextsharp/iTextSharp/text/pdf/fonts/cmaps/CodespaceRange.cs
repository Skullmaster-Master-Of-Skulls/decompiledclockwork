using System;

namespace iTextSharp.text.pdf.fonts.cmaps
{
	// Token: 0x020001CE RID: 462
	public class CodespaceRange
	{
		// Token: 0x0600120E RID: 4622 RVA: 0x00067CA3 File Offset: 0x00066CA3
		public byte[] GetEnd()
		{
			return this.end;
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x00067CAB File Offset: 0x00066CAB
		public void SetEnd(byte[] endBytes)
		{
			this.end = endBytes;
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x00067CB4 File Offset: 0x00066CB4
		public byte[] GetStart()
		{
			return this.start;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00067CBC File Offset: 0x00066CBC
		public void SetStart(byte[] startBytes)
		{
			this.start = startBytes;
		}

		// Token: 0x04000CB2 RID: 3250
		private byte[] start;

		// Token: 0x04000CB3 RID: 3251
		private byte[] end;
	}
}
