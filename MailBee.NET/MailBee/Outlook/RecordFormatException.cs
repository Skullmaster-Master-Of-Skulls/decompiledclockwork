using System;

namespace MailBee.Outlook
{
	// Token: 0x020005AA RID: 1450
	[Serializable]
	internal class RecordFormatException : RuntimeException
	{
		// Token: 0x060030D1 RID: 12497 RVA: 0x000E3FED File Offset: 0x000E2FED
		public RecordFormatException(string A_0) : base(A_0)
		{
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x000E3FF6 File Offset: 0x000E2FF6
		public RecordFormatException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x000E4000 File Offset: 0x000E3000
		public RecordFormatException(Exception A_0) : base(A_0)
		{
		}
	}
}
