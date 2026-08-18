using System;

namespace MailBee.Outlook
{
	// Token: 0x020005A9 RID: 1449
	[Serializable]
	internal class BufferUnderflowException : RuntimeException
	{
		// Token: 0x060030D0 RID: 12496 RVA: 0x000E3FE0 File Offset: 0x000E2FE0
		public BufferUnderflowException() : base("Buffer Underflow")
		{
		}
	}
}
