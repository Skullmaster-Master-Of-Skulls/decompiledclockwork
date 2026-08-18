using System;
using System.IO;

namespace System.Net.Http.Internal
{
	// Token: 0x0200003A RID: 58
	internal class NonClosingDelegatingStream : DelegatingStream
	{
		// Token: 0x060001DC RID: 476 RVA: 0x00007F0F File Offset: 0x0000610F
		public NonClosingDelegatingStream(Stream innerStream) : base(innerStream)
		{
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00007F18 File Offset: 0x00006118
		public override void Close()
		{
		}
	}
}
