using System;

namespace System.Net.Mail
{
	// Token: 0x02000293 RID: 659
	internal class SmtpReplyReader
	{
		// Token: 0x06001891 RID: 6289 RVA: 0x0007CD5D File Offset: 0x0007AF5D
		internal SmtpReplyReader(SmtpReplyReaderFactory reader)
		{
			this.reader = reader;
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x0007CD6C File Offset: 0x0007AF6C
		internal IAsyncResult BeginReadLines(AsyncCallback callback, object state)
		{
			return this.reader.BeginReadLines(this, callback, state);
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x0007CD7C File Offset: 0x0007AF7C
		internal IAsyncResult BeginReadLine(AsyncCallback callback, object state)
		{
			return this.reader.BeginReadLine(this, callback, state);
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x0007CD8C File Offset: 0x0007AF8C
		public void Close()
		{
			this.reader.Close(this);
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x0007CD9A File Offset: 0x0007AF9A
		internal LineInfo[] EndReadLines(IAsyncResult result)
		{
			return this.reader.EndReadLines(result);
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x0007CDA8 File Offset: 0x0007AFA8
		internal LineInfo EndReadLine(IAsyncResult result)
		{
			return this.reader.EndReadLine(result);
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x0007CDB6 File Offset: 0x0007AFB6
		internal LineInfo[] ReadLines()
		{
			return this.reader.ReadLines(this);
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x0007CDC4 File Offset: 0x0007AFC4
		internal LineInfo ReadLine()
		{
			return this.reader.ReadLine(this);
		}

		// Token: 0x0400186B RID: 6251
		private SmtpReplyReaderFactory reader;
	}
}
