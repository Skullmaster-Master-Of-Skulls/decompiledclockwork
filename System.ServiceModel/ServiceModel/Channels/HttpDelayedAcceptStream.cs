using System;
using System.IO;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000864 RID: 2148
	internal abstract class HttpDelayedAcceptStream : DetectEofStream
	{
		// Token: 0x060050E5 RID: 20709 RVA: 0x00129C05 File Offset: 0x00127E05
		protected HttpDelayedAcceptStream(Stream stream) : base(stream)
		{
		}

		// Token: 0x060050E6 RID: 20710 RVA: 0x00129C0E File Offset: 0x00127E0E
		public bool EnableDelayedAccept(HttpOutput output, bool closeHttpOutput)
		{
			if (base.IsAtEof)
			{
				return false;
			}
			this.closeHttpOutput = closeHttpOutput;
			this.httpOutput = output;
			return true;
		}

		// Token: 0x060050E7 RID: 20711 RVA: 0x00129C29 File Offset: 0x00127E29
		protected override void OnReceivedEof()
		{
			if (this.closeHttpOutput)
			{
				this.CloseHttpOutput();
			}
		}

		// Token: 0x060050E8 RID: 20712 RVA: 0x00129C39 File Offset: 0x00127E39
		public override void Close()
		{
			if (this.closeHttpOutput)
			{
				this.CloseHttpOutput();
			}
			base.Close();
		}

		// Token: 0x060050E9 RID: 20713 RVA: 0x00129C4F File Offset: 0x00127E4F
		private void CloseHttpOutput()
		{
			if (this.httpOutput != null && !this.isHttpOutputClosed)
			{
				this.httpOutput.Close();
				this.isHttpOutputClosed = true;
			}
		}

		// Token: 0x040031E9 RID: 12777
		private HttpOutput httpOutput;

		// Token: 0x040031EA RID: 12778
		private bool isHttpOutputClosed;

		// Token: 0x040031EB RID: 12779
		private bool closeHttpOutput;
	}
}
