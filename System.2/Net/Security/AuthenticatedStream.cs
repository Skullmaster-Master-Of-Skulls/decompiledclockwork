using System;
using System.IO;

namespace System.Net.Security
{
	// Token: 0x02000354 RID: 852
	public abstract class AuthenticatedStream : Stream
	{
		// Token: 0x06001E94 RID: 7828 RVA: 0x0008FEF8 File Offset: 0x0008E0F8
		protected AuthenticatedStream(Stream innerStream, bool leaveInnerStreamOpen)
		{
			if (innerStream == null || innerStream == Stream.Null)
			{
				throw new ArgumentNullException("innerStream");
			}
			if (!innerStream.CanRead || !innerStream.CanWrite)
			{
				throw new ArgumentException(SR.GetString("net_io_must_be_rw_stream"), "innerStream");
			}
			this._InnerStream = innerStream;
			this._LeaveStreamOpen = leaveInnerStreamOpen;
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06001E95 RID: 7829 RVA: 0x0008FF54 File Offset: 0x0008E154
		public bool LeaveInnerStreamOpen
		{
			get
			{
				return this._LeaveStreamOpen;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06001E96 RID: 7830 RVA: 0x0008FF5C File Offset: 0x0008E15C
		protected Stream InnerStream
		{
			get
			{
				return this._InnerStream;
			}
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x0008FF64 File Offset: 0x0008E164
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this._LeaveStreamOpen)
					{
						this._InnerStream.Flush();
					}
					else
					{
						this._InnerStream.Close();
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06001E98 RID: 7832
		public abstract bool IsAuthenticated { get; }

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001E99 RID: 7833
		public abstract bool IsMutuallyAuthenticated { get; }

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001E9A RID: 7834
		public abstract bool IsEncrypted { get; }

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001E9B RID: 7835
		public abstract bool IsSigned { get; }

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06001E9C RID: 7836
		public abstract bool IsServer { get; }

		// Token: 0x04001CF1 RID: 7409
		private Stream _InnerStream;

		// Token: 0x04001CF2 RID: 7410
		private bool _LeaveStreamOpen;
	}
}
