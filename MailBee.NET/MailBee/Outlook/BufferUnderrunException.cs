using System;
using System.IO;

namespace MailBee.Outlook
{
	// Token: 0x020005A8 RID: 1448
	[Serializable]
	internal class BufferUnderrunException : IOException
	{
		// Token: 0x060030CF RID: 12495 RVA: 0x000E3FD3 File Offset: 0x000E2FD3
		internal BufferUnderrunException() : base("buffer underrun")
		{
		}
	}
}
