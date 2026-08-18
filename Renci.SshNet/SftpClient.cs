using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Renci.SshNet.Abstractions;
using Renci.SshNet.Common;
using Renci.SshNet.Sftp;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet
{
	// Token: 0x0200002A RID: 42
	public class SftpClient : BaseClient
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000AD9F File Offset: 0x00008F9F
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x0000ADAD File Offset: 0x00008FAD
		public TimeSpan OperationTimeout
		{
			get
			{
				base.CheckDisposed();
				return this._operationTimeout;
			}
			set
			{
				base.CheckDisposed();
				this._operationTimeout = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000ADBC File Offset: 0x00008FBC
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000ADCA File Offset: 0x00008FCA
		public uint BufferSize
		{
			get
			{
				base.CheckDisposed();
				return this._bufferSize;
			}
			set
			{
				base.CheckDisposed();
				this._bufferSize = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000ADD9 File Offset: 0x00008FD9
		public string WorkingDirectory
		{
			get
			{
				base.CheckDisposed();
				if (this._sftpSession == null)
				{
					throw new SshConnectionException("Client not connected.");
				}
				return this._sftpSession.WorkingDirectory;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000ADFF File Offset: 0x00008FFF
		public int ProtocolVersion
		{
			get
			{
				base.CheckDisposed();
				if (this._sftpSession == null)
				{
					throw new SshConnectionException("Client not connected.");
				}
				return (int)this._sftpSession.ProtocolVersion;
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000AE25 File Offset: 0x00009025
		public SftpClient(ConnectionInfo connectionInfo) : this(connectionInfo, false)
		{
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000AE2F File Offset: 0x0000902F
		public SftpClient(string host, int port, string username, string password) : this(new PasswordConnectionInfo(host, port, username, password), true)
		{
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000AE42 File Offset: 0x00009042
		public SftpClient(string host, string username, string password) : this(host, ConnectionInfo.DefaultPort, username, password)
		{
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000AE52 File Offset: 0x00009052
		public SftpClient(string host, int port, string username, params PrivateKeyFile[] keyFiles) : this(new PrivateKeyConnectionInfo(host, port, username, keyFiles), true)
		{
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000AE65 File Offset: 0x00009065
		public SftpClient(string host, string username, params PrivateKeyFile[] keyFiles) : this(host, ConnectionInfo.DefaultPort, username, keyFiles)
		{
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000AE75 File Offset: 0x00009075
		private SftpClient(ConnectionInfo connectionInfo, bool ownsConnectionInfo) : this(connectionInfo, ownsConnectionInfo, new ServiceFactory())
		{
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000AE84 File Offset: 0x00009084
		internal SftpClient(ConnectionInfo connectionInfo, bool ownsConnectionInfo, IServiceFactory serviceFactory) : base(connectionInfo, ownsConnectionInfo, serviceFactory)
		{
			this.OperationTimeout = Renci.SshNet.Session.InfiniteTimeSpan;
			this.BufferSize = 32768U;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000AEA5 File Offset: 0x000090A5
		public void ChangeDirectory(string path)
		{
			base.CheckDisposed();
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (this._sftpSession == null)
			{
				throw new SshConnectionException("Client not connected.");
			}
			this._sftpSession.ChangeDirectory(path);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000AEDA File Offset: 0x000090DA
		public void ChangePermissions(string path, short mode)
		{
			this.Get(path).SetPermissions(mode);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000AEEC File Offset: 0x000090EC
		public void CreateDirectory(string path)
		{
			base.CheckDisposed();
			if (path.IsNullOrWhiteSpace())
			{
				throw new ArgumentException(path);
			}
			if (this._sftpSession == null)
			{
				throw new SshConnectionException("Client not connected.");
			}
			string canonicalPath = this._sftpSession.GetCanonicalPath(path);
			this._sftpSession.RequestMkDir(canonicalPath);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000AF3C File Offset: 0x0000913C
		public void DeleteDirectory(string path)
		{
			base.CheckDisposed();
			if (path.IsNullOrWhiteSpace())
			{
				throw new ArgumentException("path");
			}
			if (this._sftpSession == null)
			{
				throw new SshConnectionException("Client not connected.");
			}
			string canonicalPath = this._sftpSession.GetCanonicalPath(path);
			this._sftpSession.RequestRmDir(canonicalPath);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000AF90 File Offset: 0x00009190
		public void DeleteFile(string path)
		{
			base.CheckDisposed();
			if (path.IsNullOrWhiteSpace())
			{
				throw new ArgumentException("path");
			}
			if (this._sftpSession == null)
			{
				throw new SshConnectionException("Client not connected.");
			}
			string canonicalPath = this._sftpSession.GetCanonicalPath(path);
			this._sftpSession.RequestRemove(canonicalPath);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000AFE2 File Offset: 0x000091E2
		public void RenameFile(string oldPath, string newPath)
		{
			this.RenameFile(oldPath, newPath, false);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000AFF0 File Offset: 0x000091F0
		public void RenameFile(string oldPath, string newPath, bool isPosix)
		{
			base.CheckDisposed();
			if (oldPath == null)
			{
				throw new ArgumentNullException("oldPath");
			}
			if (newPath == null)
			{
				throw new ArgumentNullException("newPath");
			}
			if (this._sftpSession == null)
			{
				throw new SshConnectionException("Client not connected.");
			}
			string canonicalPath = this._sftpSession.GetCanonicalPath(oldPath);
			string canonicalPath2 = this._sftpSession.GetCanonicalPath(newPath);
			if (isPosix)
			{
				this._sftpSession.RequestPosixRename(canonicalPath, canonicalPath2);
				return;
			}
			this._sftpSession.RequestRename(canonicalPath, canonicalPath2);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000B06C File Offset: 0x0000926C
		public void SymbolicLink(string path, string linkPath)
		{
			base.CheckDisposed();
			if (path.IsNullOrWhiteSpace())
			{
				throw new ArgumentException("path");
			}
			if (linkPath.IsNullOrWhiteSpace())
			{
				throw new ArgumentException("linkPath");
			}
			if (this._sftpSession == null)
			{
				throw new SshConnectionException("Client not connected.");
			}
			string canonicalPath = this._sftpSession.GetCanonicalPath(path);
			string canonicalPath2 = this._sftpSession.GetCanonicalPath(linkPath);
			this._sftpSession.RequestSymLink(canonicalPath, canonicalPath2);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000B0DF File Offset: 0x000092DF
		public IEnumerable<SftpFile> ListDirectory(string path, Action<int> listCallback = null)
		{
			base.CheckDisposed();
			return this.InternalListDirectory(path, listCallback);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000B0F0 File Offset: 0x000092F0
		public IAsyncResult BeginListDirectory(string path, AsyncCallback asyncCallback, object state, Action<int> listCallback = null)
		{
			base.CheckDisposed();
			SftpListDirectoryAsyncResult asyncResult = new SftpListDirectoryAsyncResult(asyncCallback, state);
			Action<int> <>9__1;
			ThreadAbstraction.ExecuteThread(delegate
			{
				try
				{
					SftpClient <>4__this = this;
					string path2 = path;
					Action<int> listCallback2;
					if ((listCallback2 = <>9__1) == null)
					{
						listCallback2 = (<>9__1 = delegate(int count)
						{
							asyncResult.Update(count);
							if (listCallback != null)
							{
								listCallback(count);
							}
						});
					}
					IEnumerable<SftpFile> result = <>4__this.InternalListDirectory(path2, listCallback2);
					asyncResult.SetAsCompleted(result, false);
				}
				catch (Exception exception)
				{
					asyncResult.SetAsCompleted(exception, false);
				}
			});
			return asyncResult;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000B144 File Offset: 0x00009344
		public IEnumerable<SftpFile> EndListDirectory(IAsyncResult asyncResult)
		{
			SftpListDirectoryAsyncResult sftpListDirectoryAsyncResult = asyncResult as SftpListDirectoryAsyncResult;
			if (sftpListDirectoryAsyncResult == null || sftpListDirectoryAsyncResult.EndInvokeCalled)
			{
				throw new ArgumentException("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndExecute was called multiple times with the same IAsyncResult.");
			}
			return sftpListDirectoryAsyncResult.EndInvoke();
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000B174 File Offset: 0x00009374
		public SftpFile Get(string path)
		{
			base.CheckDisposed();
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (this._sftpSession == null)
			{
				throw new SshConnectionException("Client not connected.");
			}
			string canonicalPath = this._sftpSession.GetCanonicalPath(path);
			SftpFileAttributes attributes = this._sftpSession.RequestLStat(canonicalPath);
			return new SftpFile(this._sftpSession, canonicalPath, attributes);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000B1D0 File Offset: 0x000093D0
		public bool Exists(string path)
		{
			base.CheckDisposed();
			if (path.IsNullOrWhiteSpace())
			{
				throw new ArgumentException("path");
			}
			if (this._sftpSession == null)
			{
				throw new SshConnectionException("Client not connected.");
			}
			string canonicalPath = this._sftpSession.GetCanonicalPath(path);
			bool result;
			try
			{
				this._sftpSession.RequestLStat(canonicalPath);
				result = true;
			}
			catch (SftpPathNotFoundException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000B240 File Offset: 0x00009440
		public void DownloadFile(string path, Stream output, Action<ulong> downloadCallback = null)
		{
			base.CheckDisposed();
			this.InternalDownloadFile(path, output, null, downloadCallback);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000B252 File Offset: 0x00009452
		public IAsyncResult BeginDownloadFile(string path, Stream output)
		{
			return this.BeginDownloadFile(path, output, null, null, null);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000B25F File Offset: 0x0000945F
		public IAsyncResult BeginDownloadFile(string path, Stream output, AsyncCallback asyncCallback)
		{
			return this.BeginDownloadFile(path, output, asyncCallback, null, null);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000B26C File Offset: 0x0000946C
		public IAsyncResult BeginDownloadFile(string path, Stream output, AsyncCallback asyncCallback, object state, Action<ulong> downloadCallback = null)
		{
			base.CheckDisposed();
			if (path.IsNullOrWhiteSpace())
			{
				throw new ArgumentException("path");
			}
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			SftpDownloadAsyncResult asyncResult = new SftpDownloadAsyncResult(asyncCallback, state);
			Action<ulong> <>9__1;
			ThreadAbstraction.ExecuteThread(delegate
			{
				try
				{
					SftpClient <>4__this = this;
					string path2 = path;
					Stream output2 = output;
					SftpDownloadAsyncResult asyncResult = asyncResult;
					Action<ulong> downloadCallback2;
					if ((downloadCallback2 = <>9__1) == null)
					{
						downloadCallback2 = (<>9__1 = delegate(ulong offset)
						{
							asyncResult.Update(offset);
							if (downloadCallback != null)
							{
								downloadCallback(offset);
							}
						});
					}
					<>4__this.InternalDownloadFile(path2, output2, asyncResult, downloadCallback2);
					asyncResult.SetAsCompleted(null, false);
				}
				catch (Exception exception)
				{
					SftpDownloadAsyncResult asyncResult;
					asyncResult.SetAsCompleted(exception, false);
				}
			});
			return asyncResult;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000B2F0 File Offset: 0x000094F0
		public void EndDownloadFile(IAsyncResult asyncResult)
		{
			SftpDownloadAsyncResult sftpDownloadAsyncResult = asyncResult as SftpDownloadAsyncResult;
			if (sftpDownloadAsyncResult == null || sftpDownloadAsyncResult.EndInvokeCalled)
			{
				throw new ArgumentException("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndExecute was called multiple times with the same IAsyncResult.");
			}
			sftpDownloadAsyncResult.EndInvoke();
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000B320 File Offset: 0x00009520
		public void UploadFile(Stream input, string path, Action<ulong> uploadCallback = null)
		{
			this.UploadFile(input, path, true, uploadCallback);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000B32C File Offset: 0x0000952C
		public void UploadFile(Stream input, string path, bool canOverride, Action<ulong> uploadCallback = null)
		{
			base.CheckDisposed();
			Flags flags = Flags.Write | Flags.Truncate;
			if (canOverride)
			{
				flags |= Flags.CreateNewOrOpen;
			}
			else
			{
				flags |= Flags.CreateNew;
			}
			this.InternalUploadFile(input, path, flags, null, uploadCallback);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000B35C File Offset: 0x0000955C
		public IAsyncResult BeginUploadFile(Stream input, string path)
		{
			return this.BeginUploadFile(input, path, true, null, null, null);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000B36A File Offset: 0x0000956A
		public IAsyncResult BeginUploadFile(Stream input, string path, AsyncCallback asyncCallback)
		{
			return this.BeginUploadFile(input, path, true, asyncCallback, null, null);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000B378 File Offset: 0x00009578
		public IAsyncResult BeginUploadFile(Stream input, string path, AsyncCallback asyncCallback, object state, Action<ulong> uploadCallback = null)
		{
			return this.BeginUploadFile(input, path, true, asyncCallback, state, uploadCallback);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000B388 File Offset: 0x00009588
		public IAsyncResult BeginUploadFile(Stream input, string path, bool canOverride, AsyncCallback asyncCallback, object state, Action<ulong> uploadCallback = null)
		{
			base.CheckDisposed();
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (path.IsNullOrWhiteSpace())
			{
				throw new ArgumentException("path");
			}
			Flags flags = Flags.Write | Flags.Truncate;
			if (canOverride)
			{
				flags |= Flags.CreateNewOrOpen;
			}
			else
			{
				flags |= Flags.CreateNew;
			}
			SftpUploadAsyncResult asyncResult = new SftpUploadAsyncResult(asyncCallback, state);
			Action<ulong> <>9__1;
			ThreadAbstraction.ExecuteThread(delegate
			{
				try
				{
					SftpClient <>4__this = this;
					Stream input2 = input;
					string path2 = path;
					Flags flags = flags;
					SftpUploadAsyncResult asyncResult = asyncResult;
					Action<ulong> uploadCallback2;
					if ((uploadCallback2 = <>9__1) == null)
					{
						uploadCallback2 = (<>9__1 = delegate(ulong offset)
						{
							asyncResult.Update(offset);
							if (uploadCallback != null)
							{
								uploadCallback(offset);
							}
						});
					}
					<>4__this.InternalUploadFile(input2, path2, flags, asyncResult, uploadCallback2);
					asyncResult.SetAsCompleted(null, false);
				}
				catch (Exception exception)
				{
					SftpUploadAsyncResult asyncResult;
					asyncResult.SetAsCompleted(exception, false);
				}
			});
			return asyncResult;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000B43C File Offset: 0x0000963C
		public void EndUploadFile(IAsyncResult asyncResult)
		{
			SftpUploadAsyncResult sftpUploadAsyncResult = asyncResult as SftpUploadAsyncResult;
			if (sftpUploadAsyncResult == null || sftpUploadAsyncResult.EndInvokeCalled)
			{
				throw new ArgumentException("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndExecute was called multiple times with the same IAsyncResult.");
			}
			sftpUploadAsyncResult.EndInvoke();
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000B46C File Offset: 0x0000966C
		public SftpFileSytemInformation GetStatus(string path)
		{
			base.CheckDisposed();
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (this._sftpSession == null)
			{
				throw new SshConnectionException("Client not connected.");
			}
			string canonicalPath = this._sftpSession.GetCanonicalPath(path);
			return this._sftpSession.RequestStatVfs(canonicalPath, false);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000B4BC File Offset: 0x000096BC
		public void AppendAllLines(string path, IEnumerable<string> contents)
		{
			base.CheckDisposed();
			if (contents == null)
			{
				throw new ArgumentNullException("contents");
			}
			using (StreamWriter streamWriter = this.AppendText(path))
			{
				foreach (string value in contents)
				{
					streamWriter.WriteLine(value);
				}
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000B538 File Offset: 0x00009738
		public void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
		{
			base.CheckDisposed();
			if (contents == null)
			{
				throw new ArgumentNullException("contents");
			}
			using (StreamWriter streamWriter = this.AppendText(path, encoding))
			{
				foreach (string value in contents)
				{
					streamWriter.WriteLine(value);
				}
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000B5B4 File Offset: 0x000097B4
		public void AppendAllText(string path, string contents)
		{
			using (StreamWriter streamWriter = this.AppendText(path))
			{
				streamWriter.Write(contents);
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000B5EC File Offset: 0x000097EC
		public void AppendAllText(string path, string contents, Encoding encoding)
		{
			using (StreamWriter streamWriter = this.AppendText(path, encoding))
			{
				streamWriter.Write(contents);
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000B628 File Offset: 0x00009828
		public StreamWriter AppendText(string path)
		{
			return this.AppendText(path, Encoding.UTF8);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000B636 File Offset: 0x00009836
		public StreamWriter AppendText(string path, Encoding encoding)
		{
			base.CheckDisposed();
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			return new StreamWriter(new SftpFileStream(this._sftpSession, path, FileMode.Append, FileAccess.Write, (int)this._bufferSize), encoding);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000B666 File Offset: 0x00009866
		public SftpFileStream Create(string path)
		{
			base.CheckDisposed();
			return new SftpFileStream(this._sftpSession, path, FileMode.Create, FileAccess.ReadWrite, (int)this._bufferSize);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000B682 File Offset: 0x00009882
		public SftpFileStream Create(string path, int bufferSize)
		{
			base.CheckDisposed();
			return new SftpFileStream(this._sftpSession, path, FileMode.Create, FileAccess.ReadWrite, bufferSize);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000B699 File Offset: 0x00009899
		public StreamWriter CreateText(string path)
		{
			return this.CreateText(path, Encoding.UTF8);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000B6A7 File Offset: 0x000098A7
		public StreamWriter CreateText(string path, Encoding encoding)
		{
			base.CheckDisposed();
			return new StreamWriter(this.OpenWrite(path), encoding);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000B6BC File Offset: 0x000098BC
		public void Delete(string path)
		{
			this.Get(path).Delete();
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000B6CA File Offset: 0x000098CA
		public DateTime GetLastAccessTime(string path)
		{
			return this.Get(path).LastAccessTime;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000B6D8 File Offset: 0x000098D8
		public DateTime GetLastAccessTimeUtc(string path)
		{
			return this.GetLastAccessTime(path).ToUniversalTime();
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000B6F4 File Offset: 0x000098F4
		public DateTime GetLastWriteTime(string path)
		{
			return this.Get(path).LastWriteTime;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000B704 File Offset: 0x00009904
		public DateTime GetLastWriteTimeUtc(string path)
		{
			return this.GetLastWriteTime(path).ToUniversalTime();
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000B720 File Offset: 0x00009920
		public SftpFileStream Open(string path, FileMode mode)
		{
			return this.Open(path, mode, FileAccess.ReadWrite);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000B72B File Offset: 0x0000992B
		public SftpFileStream Open(string path, FileMode mode, FileAccess access)
		{
			base.CheckDisposed();
			return new SftpFileStream(this._sftpSession, path, mode, access, (int)this._bufferSize);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000B747 File Offset: 0x00009947
		public SftpFileStream OpenRead(string path)
		{
			return this.Open(path, FileMode.Open, FileAccess.Read);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000B752 File Offset: 0x00009952
		public StreamReader OpenText(string path)
		{
			return new StreamReader(this.OpenRead(path), Encoding.UTF8);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000B765 File Offset: 0x00009965
		public SftpFileStream OpenWrite(string path)
		{
			base.CheckDisposed();
			return new SftpFileStream(this._sftpSession, path, FileMode.OpenOrCreate, FileAccess.Write, (int)this._bufferSize);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000B784 File Offset: 0x00009984
		public byte[] ReadAllBytes(string path)
		{
			byte[] result;
			using (SftpFileStream sftpFileStream = this.OpenRead(path))
			{
				byte[] array = new byte[sftpFileStream.Length];
				sftpFileStream.Read(array, 0, array.Length);
				result = array;
			}
			return result;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000B7D4 File Offset: 0x000099D4
		public string[] ReadAllLines(string path)
		{
			return this.ReadAllLines(path, Encoding.UTF8);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000B7E4 File Offset: 0x000099E4
		public string[] ReadAllLines(string path, Encoding encoding)
		{
			List<string> list = new List<string>();
			using (StreamReader streamReader = new StreamReader(this.OpenRead(path), encoding))
			{
				while (!streamReader.EndOfStream)
				{
					list.Add(streamReader.ReadLine());
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000B840 File Offset: 0x00009A40
		public string ReadAllText(string path)
		{
			return this.ReadAllText(path, Encoding.UTF8);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000B850 File Offset: 0x00009A50
		public string ReadAllText(string path, Encoding encoding)
		{
			string result;
			using (StreamReader streamReader = new StreamReader(this.OpenRead(path), encoding))
			{
				result = streamReader.ReadToEnd();
			}
			return result;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000B890 File Offset: 0x00009A90
		public IEnumerable<string> ReadLines(string path)
		{
			return this.ReadAllLines(path);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000B899 File Offset: 0x00009A99
		public IEnumerable<string> ReadLines(string path, Encoding encoding)
		{
			return this.ReadAllLines(path, encoding);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000B8A3 File Offset: 0x00009AA3
		[Obsolete("Note: This method currently throws NotImplementedException because it has not yet been implemented.")]
		public void SetLastAccessTime(string path, DateTime lastAccessTime)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000B8A3 File Offset: 0x00009AA3
		[Obsolete("Note: This method currently throws NotImplementedException because it has not yet been implemented.")]
		public void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000B8A3 File Offset: 0x00009AA3
		[Obsolete("Note: This method currently throws NotImplementedException because it has not yet been implemented.")]
		public void SetLastWriteTime(string path, DateTime lastWriteTime)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000B8A3 File Offset: 0x00009AA3
		[Obsolete("Note: This method currently throws NotImplementedException because it has not yet been implemented.")]
		public void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000B8AC File Offset: 0x00009AAC
		public void WriteAllBytes(string path, byte[] bytes)
		{
			using (SftpFileStream sftpFileStream = this.OpenWrite(path))
			{
				sftpFileStream.Write(bytes, 0, bytes.Length);
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000B8E8 File Offset: 0x00009AE8
		public void WriteAllLines(string path, IEnumerable<string> contents)
		{
			this.WriteAllLines(path, contents, Encoding.UTF8);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000B8F7 File Offset: 0x00009AF7
		public void WriteAllLines(string path, string[] contents)
		{
			this.WriteAllLines(path, contents, Encoding.UTF8);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000B908 File Offset: 0x00009B08
		public void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
		{
			using (StreamWriter streamWriter = this.CreateText(path, encoding))
			{
				foreach (string value in contents)
				{
					streamWriter.WriteLine(value);
				}
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000B970 File Offset: 0x00009B70
		public void WriteAllLines(string path, string[] contents, Encoding encoding)
		{
			using (StreamWriter streamWriter = this.CreateText(path, encoding))
			{
				foreach (string value in contents)
				{
					streamWriter.WriteLine(value);
				}
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000B9C0 File Offset: 0x00009BC0
		public void WriteAllText(string path, string contents)
		{
			using (StreamWriter streamWriter = this.CreateText(path))
			{
				streamWriter.Write(contents);
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000B9F8 File Offset: 0x00009BF8
		public void WriteAllText(string path, string contents, Encoding encoding)
		{
			using (StreamWriter streamWriter = this.CreateText(path, encoding))
			{
				streamWriter.Write(contents);
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000BA34 File Offset: 0x00009C34
		public SftpFileAttributes GetAttributes(string path)
		{
			base.CheckDisposed();
			if (this._sftpSession == null)
			{
				throw new SshConnectionException("Client not connected.");
			}
			string canonicalPath = this._sftpSession.GetCanonicalPath(path);
			return this._sftpSession.RequestLStat(canonicalPath);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000BA74 File Offset: 0x00009C74
		public void SetAttributes(string path, SftpFileAttributes fileAttributes)
		{
			base.CheckDisposed();
			if (this._sftpSession == null)
			{
				throw new SshConnectionException("Client not connected.");
			}
			string canonicalPath = this._sftpSession.GetCanonicalPath(path);
			this._sftpSession.RequestSetStat(canonicalPath, fileAttributes);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000BAB4 File Offset: 0x00009CB4
		public IEnumerable<FileInfo> SynchronizeDirectories(string sourcePath, string destinationPath, string searchPattern)
		{
			if (sourcePath == null)
			{
				throw new ArgumentNullException("sourcePath");
			}
			if (destinationPath.IsNullOrWhiteSpace())
			{
				throw new ArgumentException("destinationPath");
			}
			return this.InternalSynchronizeDirectories(sourcePath, destinationPath, searchPattern, null);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000BAE4 File Offset: 0x00009CE4
		public IAsyncResult BeginSynchronizeDirectories(string sourcePath, string destinationPath, string searchPattern, AsyncCallback asyncCallback, object state)
		{
			if (sourcePath == null)
			{
				throw new ArgumentNullException("sourcePath");
			}
			if (destinationPath.IsNullOrWhiteSpace())
			{
				throw new ArgumentException("destDir");
			}
			SftpSynchronizeDirectoriesAsyncResult asyncResult = new SftpSynchronizeDirectoriesAsyncResult(asyncCallback, state);
			ThreadAbstraction.ExecuteThread(delegate
			{
				try
				{
					IEnumerable<FileInfo> result = this.InternalSynchronizeDirectories(sourcePath, destinationPath, searchPattern, asyncResult);
					asyncResult.SetAsCompleted(result, false);
				}
				catch (Exception exception)
				{
					asyncResult.SetAsCompleted(exception, false);
				}
			});
			return asyncResult;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000BB64 File Offset: 0x00009D64
		public IEnumerable<FileInfo> EndSynchronizeDirectories(IAsyncResult asyncResult)
		{
			SftpSynchronizeDirectoriesAsyncResult sftpSynchronizeDirectoriesAsyncResult = asyncResult as SftpSynchronizeDirectoriesAsyncResult;
			if (sftpSynchronizeDirectoriesAsyncResult == null || sftpSynchronizeDirectoriesAsyncResult.EndInvokeCalled)
			{
				throw new ArgumentException("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndExecute was called multiple times with the same IAsyncResult.");
			}
			return sftpSynchronizeDirectoriesAsyncResult.EndInvoke();
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000BB94 File Offset: 0x00009D94
		private IEnumerable<FileInfo> InternalSynchronizeDirectories(string sourcePath, string destinationPath, string searchPattern, SftpSynchronizeDirectoriesAsyncResult asynchResult)
		{
			if (!Directory.Exists(sourcePath))
			{
				throw new FileNotFoundException(string.Format("Source directory not found: {0}", sourcePath));
			}
			List<FileInfo> list = new List<FileInfo>();
			List<FileInfo> list2 = FileSystemAbstraction.EnumerateFiles(new DirectoryInfo(sourcePath), searchPattern).ToList<FileInfo>();
			if (list2.Count == 0)
			{
				return list;
			}
			IEnumerable<SftpFile> enumerable = this.InternalListDirectory(destinationPath, null);
			Dictionary<string, SftpFile> dictionary = new Dictionary<string, SftpFile>();
			foreach (SftpFile sftpFile in enumerable)
			{
				if (!sftpFile.IsDirectory)
				{
					dictionary.Add(sftpFile.Name, sftpFile);
				}
			}
			foreach (FileInfo fileInfo in list2)
			{
				bool flag = !dictionary.ContainsKey(fileInfo.Name);
				if (!flag)
				{
					SftpFile sftpFile2 = dictionary[fileInfo.Name];
					flag = (fileInfo.Length != sftpFile2.Length);
				}
				if (flag)
				{
					string text = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
					{
						destinationPath,
						fileInfo.Name
					});
					try
					{
						using (FileStream fileStream = File.OpenRead(fileInfo.FullName))
						{
							this.InternalUploadFile(fileStream, text, Flags.Write | Flags.CreateNewOrOpen | Flags.Truncate, null, null);
						}
						list.Add(fileInfo);
						if (asynchResult != null)
						{
							asynchResult.Update(list.Count);
						}
					}
					catch (Exception innerException)
					{
						throw new Exception(string.Format("Failed to upload {0} to {1}", fileInfo.FullName, text), innerException);
					}
				}
			}
			return list;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000BD58 File Offset: 0x00009F58
		private IEnumerable<SftpFile> InternalListDirectory(string path, Action<int> listCallback)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (this._sftpSession == null)
			{
				throw new SshConnectionException("Client not connected.");
			}
			string canonicalPath = this._sftpSession.GetCanonicalPath(path);
			byte[] handle = this._sftpSession.RequestOpenDir(canonicalPath, false);
			string basePath = canonicalPath;
			if (!basePath.EndsWith("/"))
			{
				basePath = string.Format("{0}/", canonicalPath);
			}
			List<SftpFile> result = new List<SftpFile>();
			for (KeyValuePair<string, SftpFileAttributes>[] source = this._sftpSession.RequestReadDir(handle); source != null; source = this._sftpSession.RequestReadDir(handle))
			{
				result.AddRange(from f in source
				select new SftpFile(this._sftpSession, string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[]
				{
					basePath,
					f.Key
				}), f.Value));
				if (listCallback != null)
				{
					ThreadAbstraction.ExecuteThread(delegate
					{
						listCallback(result.Count);
					});
				}
			}
			this._sftpSession.RequestClose(handle);
			return result;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000BE54 File Offset: 0x0000A054
		private void InternalDownloadFile(string path, Stream output, SftpDownloadAsyncResult asyncResult, Action<ulong> downloadCallback)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (path.IsNullOrWhiteSpace())
			{
				throw new ArgumentException("path");
			}
			if (this._sftpSession == null)
			{
				throw new SshConnectionException("Client not connected.");
			}
			string canonicalPath = this._sftpSession.GetCanonicalPath(path);
			byte[] handle = this._sftpSession.RequestOpen(canonicalPath, Flags.Read, false);
			ulong num = 0UL;
			uint length = this._sftpSession.CalculateOptimalReadLength(this._bufferSize);
			byte[] array = this._sftpSession.RequestRead(handle, num, length);
			while (array.Length != 0 && (asyncResult == null || !asyncResult.IsDownloadCanceled))
			{
				output.Write(array, 0, array.Length);
				output.Flush();
				num += (ulong)((long)array.Length);
				if (downloadCallback != null)
				{
					ulong downloadOffset = num;
					ThreadAbstraction.ExecuteThread(delegate
					{
						downloadCallback(downloadOffset);
					});
				}
				array = this._sftpSession.RequestRead(handle, num, length);
			}
			this._sftpSession.RequestClose(handle);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000BF5C File Offset: 0x0000A15C
		private void InternalUploadFile(Stream input, string path, Flags flags, SftpUploadAsyncResult asyncResult, Action<ulong> uploadCallback)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (path.IsNullOrWhiteSpace())
			{
				throw new ArgumentException("path");
			}
			if (this._sftpSession == null)
			{
				throw new SshConnectionException("Client not connected.");
			}
			string canonicalPath = this._sftpSession.GetCanonicalPath(path);
			byte[] handle = this._sftpSession.RequestOpen(canonicalPath, flags, false);
			ulong num = 0UL;
			byte[] array = new byte[this._sftpSession.CalculateOptimalWriteLength(this._bufferSize, handle)];
			int num2 = input.Read(array, 0, array.Length);
			int expectedResponses = 0;
			AutoResetEvent responseReceivedWaitHandle = new AutoResetEvent(false);
			while (asyncResult == null || !asyncResult.IsUploadCanceled)
			{
				if (num2 > 0)
				{
					ulong writtenBytes = num + (ulong)((long)num2);
					Action <>9__1;
					this._sftpSession.RequestWrite(handle, num, array, num2, null, delegate(SftpStatusResponse s)
					{
						if (s.StatusCode == StatusCodes.Ok)
						{
							Interlocked.Decrement(ref expectedResponses);
							responseReceivedWaitHandle.Set();
							if (uploadCallback != null)
							{
								Action action;
								if ((action = <>9__1) == null)
								{
									action = (<>9__1 = delegate()
									{
										uploadCallback(writtenBytes);
									});
								}
								ThreadAbstraction.ExecuteThread(action);
							}
						}
					});
					Interlocked.Increment(ref expectedResponses);
					num += (ulong)((long)num2);
					num2 = input.Read(array, 0, array.Length);
				}
				else if (expectedResponses > 0)
				{
					this._sftpSession.WaitOnHandle(responseReceivedWaitHandle, this.OperationTimeout);
				}
				if (expectedResponses <= 0 && num2 <= 0)
				{
					break;
				}
			}
			this._sftpSession.RequestClose(handle);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000C0BD File Offset: 0x0000A2BD
		protected override void OnConnected()
		{
			base.OnConnected();
			this._sftpSession = base.ServiceFactory.CreateSftpSession(base.Session, this.OperationTimeout, base.ConnectionInfo.Encoding);
			this._sftpSession.Connect();
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000C0F8 File Offset: 0x0000A2F8
		protected override void OnDisconnecting()
		{
			base.OnDisconnecting();
			if (this._sftpSession != null)
			{
				this._sftpSession.Disconnect();
				this._sftpSession.Dispose();
				this._sftpSession = null;
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000C125 File Offset: 0x0000A325
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && this._sftpSession != null)
			{
				this._sftpSession.Dispose();
				this._sftpSession = null;
			}
		}

		// Token: 0x040000DD RID: 221
		private ISftpSession _sftpSession;

		// Token: 0x040000DE RID: 222
		private TimeSpan _operationTimeout;

		// Token: 0x040000DF RID: 223
		private uint _bufferSize;
	}
}
