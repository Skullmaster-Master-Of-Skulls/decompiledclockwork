using System;
using System.Diagnostics;
using System.IO;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007E8 RID: 2024
	internal sealed class MessageEnd : IStreamable
	{
		// Token: 0x06004792 RID: 18322 RVA: 0x000F54C5 File Offset: 0x000F44C5
		internal MessageEnd()
		{
		}

		// Token: 0x06004793 RID: 18323 RVA: 0x000F54CD File Offset: 0x000F44CD
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(11);
		}

		// Token: 0x06004794 RID: 18324 RVA: 0x000F54D7 File Offset: 0x000F44D7
		public void Read(__BinaryParser input)
		{
		}

		// Token: 0x06004795 RID: 18325 RVA: 0x000F54D9 File Offset: 0x000F44D9
		public void Dump()
		{
		}

		// Token: 0x06004796 RID: 18326 RVA: 0x000F54DB File Offset: 0x000F44DB
		public void Dump(Stream sout)
		{
		}

		// Token: 0x06004797 RID: 18327 RVA: 0x000F54DD File Offset: 0x000F44DD
		[Conditional("_LOGGING")]
		private void DumpInternal(Stream sout)
		{
			if (BCLDebug.CheckEnabled("BINARY") && sout != null && sout.CanSeek)
			{
				long length = sout.Length;
			}
		}
	}
}
