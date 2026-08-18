using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.Configuration
{
	// Token: 0x02000743 RID: 1859
	[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
	internal class RemoteWebConfigurationHostStream : Stream
	{
		// Token: 0x0600598C RID: 22924 RVA: 0x00138FF8 File Offset: 0x001371F8
		internal RemoteWebConfigurationHostStream(bool streamForWrite, string serverName, string streamName, string templateStreamName, string username, string domain, string password, WindowsIdentity identity)
		{
			this._Server = serverName;
			this._FileName = streamName;
			this._TemplateFileName = templateStreamName;
			this._Username = username;
			this._Domain = domain;
			this._Password = password;
			this._Identity = identity;
			this._streamForWrite = streamForWrite;
		}

		// Token: 0x0600598D RID: 22925 RVA: 0x00139048 File Offset: 0x00137248
		private void Init()
		{
			if (this._MemoryStream != null)
			{
				return;
			}
			byte[] array = null;
			WindowsImpersonationContext windowsImpersonationContext = null;
			try
			{
				if (this._Identity != null)
				{
					windowsImpersonationContext = this._Identity.Impersonate();
				}
				try
				{
					IRemoteWebConfigurationHostServer remoteWebConfigurationHostServer = RemoteWebConfigurationHost.CreateRemoteObject(this._Server, this._Username, this._Domain, this._Password);
					try
					{
						array = remoteWebConfigurationHostServer.GetData(this._FileName, this._streamForWrite, out this._ReadTime);
					}
					finally
					{
						while (Marshal.ReleaseComObject(remoteWebConfigurationHostServer) > 0)
						{
						}
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					if (windowsImpersonationContext != null)
					{
						windowsImpersonationContext.Undo();
					}
				}
			}
			catch
			{
				throw;
			}
			if (array == null || array.Length < 1)
			{
				this._MemoryStream = new MemoryStream();
				return;
			}
			this._MemoryStream = new MemoryStream(array.Length);
			this._MemoryStream.Write(array, 0, array.Length);
			this._MemoryStream.Position = 0L;
		}

		// Token: 0x170019E9 RID: 6633
		// (get) Token: 0x0600598E RID: 22926 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019EA RID: 6634
		// (get) Token: 0x0600598F RID: 22927 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019EB RID: 6635
		// (get) Token: 0x06005990 RID: 22928 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019EC RID: 6636
		// (get) Token: 0x06005991 RID: 22929 RVA: 0x00139144 File Offset: 0x00137344
		public override long Length
		{
			get
			{
				this.Init();
				return this._MemoryStream.Length;
			}
		}

		// Token: 0x170019ED RID: 6637
		// (get) Token: 0x06005992 RID: 22930 RVA: 0x00139157 File Offset: 0x00137357
		// (set) Token: 0x06005993 RID: 22931 RVA: 0x0013916A File Offset: 0x0013736A
		public override long Position
		{
			get
			{
				this.Init();
				return this._MemoryStream.Position;
			}
			set
			{
				this.Init();
				this._MemoryStream.Position = value;
			}
		}

		// Token: 0x06005994 RID: 22932 RVA: 0x0013917E File Offset: 0x0013737E
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.Init();
			return this._MemoryStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06005995 RID: 22933 RVA: 0x00139198 File Offset: 0x00137398
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this._IsDirty = true;
			this.Init();
			if ((long)(offset + count) > this._MemoryStream.Length)
			{
				this._MemoryStream.SetLength((long)(offset + count));
			}
			return this._MemoryStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06005996 RID: 22934 RVA: 0x001391E4 File Offset: 0x001373E4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this._MemoryStream != null)
				{
					this.Flush();
					this._MemoryStream.Close();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06005997 RID: 22935 RVA: 0x00139228 File Offset: 0x00137428
		public override ObjRef CreateObjRef(Type requestedType)
		{
			throw new RemotingException();
		}

		// Token: 0x06005998 RID: 22936 RVA: 0x0013922F File Offset: 0x0013742F
		public override int EndRead(IAsyncResult asyncResult)
		{
			this.Init();
			return this._MemoryStream.EndRead(asyncResult);
		}

		// Token: 0x06005999 RID: 22937 RVA: 0x00139243 File Offset: 0x00137443
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.Init();
			this._MemoryStream.EndWrite(asyncResult);
		}

		// Token: 0x0600599A RID: 22938 RVA: 0x00006164 File Offset: 0x00004364
		public override void Flush()
		{
		}

		// Token: 0x0600599B RID: 22939 RVA: 0x00139258 File Offset: 0x00137458
		internal void FlushForWriteCompleted()
		{
			if (this._IsDirty && this._MemoryStream != null)
			{
				WindowsImpersonationContext windowsImpersonationContext = null;
				try
				{
					if (this._Identity != null)
					{
						windowsImpersonationContext = this._Identity.Impersonate();
					}
					try
					{
						IRemoteWebConfigurationHostServer remoteWebConfigurationHostServer = RemoteWebConfigurationHost.CreateRemoteObject(this._Server, this._Username, this._Domain, this._Password);
						try
						{
							remoteWebConfigurationHostServer.WriteData(this._FileName, this._TemplateFileName, this._MemoryStream.ToArray(), ref this._ReadTime);
						}
						catch
						{
							throw;
						}
						finally
						{
							while (Marshal.ReleaseComObject(remoteWebConfigurationHostServer) > 0)
							{
							}
						}
					}
					catch
					{
						throw;
					}
					finally
					{
						if (windowsImpersonationContext != null)
						{
							windowsImpersonationContext.Undo();
						}
					}
				}
				catch
				{
					throw;
				}
				this._MemoryStream.Flush();
				this._IsDirty = false;
			}
		}

		// Token: 0x0600599C RID: 22940 RVA: 0x0013934C File Offset: 0x0013754C
		public override object InitializeLifetimeService()
		{
			this.Init();
			return this._MemoryStream.InitializeLifetimeService();
		}

		// Token: 0x0600599D RID: 22941 RVA: 0x0013935F File Offset: 0x0013755F
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.Init();
			return this._MemoryStream.Read(buffer, offset, count);
		}

		// Token: 0x0600599E RID: 22942 RVA: 0x00139375 File Offset: 0x00137575
		public override int ReadByte()
		{
			this.Init();
			return this._MemoryStream.ReadByte();
		}

		// Token: 0x0600599F RID: 22943 RVA: 0x00139388 File Offset: 0x00137588
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.Init();
			return this._MemoryStream.Seek(offset, origin);
		}

		// Token: 0x060059A0 RID: 22944 RVA: 0x0013939D File Offset: 0x0013759D
		public override void SetLength(long val)
		{
			this._IsDirty = true;
			this.Init();
			this._MemoryStream.SetLength(val);
		}

		// Token: 0x060059A1 RID: 22945 RVA: 0x001393B8 File Offset: 0x001375B8
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._IsDirty = true;
			this.Init();
			if ((long)(offset + count) > this._MemoryStream.Length)
			{
				this._MemoryStream.SetLength((long)(offset + count));
			}
			this._MemoryStream.Write(buffer, offset, count);
		}

		// Token: 0x060059A2 RID: 22946 RVA: 0x001393F5 File Offset: 0x001375F5
		public override void WriteByte(byte val)
		{
			this._IsDirty = true;
			this.Init();
			this._MemoryStream.WriteByte(val);
		}

		// Token: 0x04002F74 RID: 12148
		private string _FileName;

		// Token: 0x04002F75 RID: 12149
		private string _TemplateFileName;

		// Token: 0x04002F76 RID: 12150
		private string _Server;

		// Token: 0x04002F77 RID: 12151
		private MemoryStream _MemoryStream;

		// Token: 0x04002F78 RID: 12152
		private bool _IsDirty;

		// Token: 0x04002F79 RID: 12153
		private long _ReadTime;

		// Token: 0x04002F7A RID: 12154
		private WindowsIdentity _Identity;

		// Token: 0x04002F7B RID: 12155
		private string _Username;

		// Token: 0x04002F7C RID: 12156
		private string _Domain;

		// Token: 0x04002F7D RID: 12157
		private string _Password;

		// Token: 0x04002F7E RID: 12158
		private bool _streamForWrite;
	}
}
