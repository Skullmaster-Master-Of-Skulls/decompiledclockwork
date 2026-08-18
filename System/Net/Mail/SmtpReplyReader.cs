using System;

namespace System.Net.Mail
{
	// Token: 0x020006D5 RID: 1749
	internal class SmtpReplyReader
	{
		// Token: 0x06003602 RID: 13826 RVA: 0x000E6741 File Offset: 0x000E5741
		internal SmtpReplyReader(SmtpReplyReaderFactory reader)
		{
			this.reader = reader;
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x000E6750 File Offset: 0x000E5750
		internal IAsyncResult BeginReadLines(AsyncCallback callback, object state)
		{
			return this.reader.BeginReadLines(this, callback, state);
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x000E6760 File Offset: 0x000E5760
		internal IAsyncResult BeginReadLine(AsyncCallback callback, object state)
		{
			return this.reader.BeginReadLine(this, callback, state);
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x000E6770 File Offset: 0x000E5770
		public void Close()
		{
			this.reader.Close(this);
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x000E677E File Offset: 0x000E577E
		internal LineInfo[] EndReadLines(IAsyncResult result)
		{
			return this.reader.EndReadLines(result);
		}

		// Token: 0x06003607 RID: 13831 RVA: 0x000E678C File Offset: 0x000E578C
		internal LineInfo EndReadLine(IAsyncResult result)
		{
			return this.reader.EndReadLine(result);
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x000E679A File Offset: 0x000E579A
		internal LineInfo[] ReadLines()
		{
			return this.reader.ReadLines(this);
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x000E67A8 File Offset: 0x000E57A8
		internal LineInfo ReadLine()
		{
			return this.reader.ReadLine(this);
		}

		// Token: 0x04003127 RID: 12583
		private SmtpReplyReaderFactory reader;
	}
}
