using System;
using System.IO;

namespace log4net.Util
{
	// Token: 0x02000113 RID: 275
	public class ProtectCloseTextWriter : TextWriterAdapter
	{
		// Token: 0x0600080E RID: 2062 RVA: 0x00018F88 File Offset: 0x00017188
		public ProtectCloseTextWriter(TextWriter writer) : base(writer)
		{
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00018F91 File Offset: 0x00017191
		public void Attach(TextWriter writer)
		{
			base.Writer = writer;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00018F9A File Offset: 0x0001719A
		public override void Close()
		{
		}
	}
}
