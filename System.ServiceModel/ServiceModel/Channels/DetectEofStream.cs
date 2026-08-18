using System;
using System.IO;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000776 RID: 1910
	internal abstract class DetectEofStream : DelegatingStream
	{
		// Token: 0x06004906 RID: 18694 RVA: 0x0010D6C6 File Offset: 0x0010B8C6
		protected DetectEofStream(Stream stream) : base(stream)
		{
			this.isAtEof = false;
		}

		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x06004907 RID: 18695 RVA: 0x0010D6D6 File Offset: 0x0010B8D6
		protected bool IsAtEof
		{
			get
			{
				return this.isAtEof;
			}
		}

		// Token: 0x06004908 RID: 18696 RVA: 0x0010D6E0 File Offset: 0x0010B8E0
		public override int EndRead(IAsyncResult result)
		{
			int num = base.EndRead(result);
			if (num == 0)
			{
				this.ReceivedEof();
			}
			return num;
		}

		// Token: 0x06004909 RID: 18697 RVA: 0x0010D700 File Offset: 0x0010B900
		public override int ReadByte()
		{
			int num = base.ReadByte();
			if (num == -1)
			{
				this.ReceivedEof();
			}
			return num;
		}

		// Token: 0x0600490A RID: 18698 RVA: 0x0010D720 File Offset: 0x0010B920
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.isAtEof)
			{
				return 0;
			}
			int num = base.Read(buffer, offset, count);
			if (num == 0)
			{
				this.ReceivedEof();
			}
			return num;
		}

		// Token: 0x0600490B RID: 18699 RVA: 0x0010D74B File Offset: 0x0010B94B
		private void ReceivedEof()
		{
			if (!this.isAtEof)
			{
				this.isAtEof = true;
				this.OnReceivedEof();
			}
		}

		// Token: 0x0600490C RID: 18700 RVA: 0x0010D762 File Offset: 0x0010B962
		protected virtual void OnReceivedEof()
		{
		}

		// Token: 0x04002E18 RID: 11800
		private bool isAtEof;
	}
}
