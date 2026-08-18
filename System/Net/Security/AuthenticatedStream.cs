using System;
using System.IO;

namespace System.Net.Security
{
	// Token: 0x02000588 RID: 1416
	public abstract class AuthenticatedStream : Stream
	{
		// Token: 0x06002B90 RID: 11152 RVA: 0x000BCBB8 File Offset: 0x000BBBB8
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

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06002B91 RID: 11153 RVA: 0x000BCC14 File Offset: 0x000BBC14
		public bool LeaveInnerStreamOpen
		{
			get
			{
				return this._LeaveStreamOpen;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06002B92 RID: 11154 RVA: 0x000BCC1C File Offset: 0x000BBC1C
		protected Stream InnerStream
		{
			get
			{
				return this._InnerStream;
			}
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x000BCC24 File Offset: 0x000BBC24
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

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06002B94 RID: 11156
		public abstract bool IsAuthenticated { get; }

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06002B95 RID: 11157
		public abstract bool IsMutuallyAuthenticated { get; }

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06002B96 RID: 11158
		public abstract bool IsEncrypted { get; }

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06002B97 RID: 11159
		public abstract bool IsSigned { get; }

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06002B98 RID: 11160
		public abstract bool IsServer { get; }

		// Token: 0x040029CF RID: 10703
		private Stream _InnerStream;

		// Token: 0x040029D0 RID: 10704
		private bool _LeaveStreamOpen;
	}
}
