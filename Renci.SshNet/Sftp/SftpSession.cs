using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Renci.SshNet.Common;
using Renci.SshNet.Sftp.Requests;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp
{
	// Token: 0x0200003B RID: 59
	internal class SftpSession : SubsystemSession, ISftpSession, ISubsystemSession, IDisposable
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00010848 File Offset: 0x0000EA48
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x00010850 File Offset: 0x0000EA50
		public string WorkingDirectory { get; private set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00010859 File Offset: 0x0000EA59
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x00010861 File Offset: 0x0000EA61
		public uint ProtocolVersion { get; private set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0001086A File Offset: 0x0000EA6A
		public uint NextRequestId
		{
			get
			{
				return (uint)Interlocked.Increment(ref this._requestId);
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00010878 File Offset: 0x0000EA78
		public SftpSession(ISession session, TimeSpan operationTimeout, Encoding encoding) : base(session, "sftp", operationTimeout, encoding)
		{
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000108B0 File Offset: 0x0000EAB0
		public void ChangeDirectory(string path)
		{
			string canonicalPath = this.GetCanonicalPath(path);
			byte[] handle = this.RequestOpenDir(canonicalPath, false);
			this.RequestClose(handle);
			this.WorkingDirectory = canonicalPath;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000108DC File Offset: 0x0000EADC
		internal void SendMessage(SftpMessage sftpMessage)
		{
			byte[] bytes = sftpMessage.GetBytes();
			base.SendData(bytes);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000108F8 File Offset: 0x0000EAF8
		public string GetCanonicalPath(string path)
		{
			string fullRemotePath = this.GetFullRemotePath(path);
			string text = string.Empty;
			KeyValuePair<string, SftpFileAttributes>[] array = this.RequestRealPath(fullRemotePath, true);
			if (array != null)
			{
				text = array.First<KeyValuePair<string, SftpFileAttributes>>().Key;
			}
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			if (fullRemotePath.EndsWith("/.", StringComparison.OrdinalIgnoreCase) || fullRemotePath.EndsWith("/..", StringComparison.OrdinalIgnoreCase) || fullRemotePath.Equals("/", StringComparison.OrdinalIgnoreCase) || fullRemotePath.IndexOf('/') < 0)
			{
				return fullRemotePath;
			}
			string[] array2 = fullRemotePath.Split(new char[]
			{
				'/'
			});
			string text2 = string.Join("/", array2, 0, array2.Length - 1);
			if (string.IsNullOrEmpty(text2))
			{
				text2 = "/";
			}
			array = this.RequestRealPath(text2, true);
			if (array != null)
			{
				text = array.First<KeyValuePair<string, SftpFileAttributes>>().Key;
			}
			if (string.IsNullOrEmpty(text))
			{
				return fullRemotePath;
			}
			string text3 = string.Empty;
			if (text[text.Length - 1] != '/')
			{
				text3 = "/";
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", new object[]
			{
				text,
				text3,
				array2[array2.Length - 1]
			});
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00010A14 File Offset: 0x0000EC14
		internal string GetFullRemotePath(string path)
		{
			string result = path;
			if (!string.IsNullOrEmpty(path) && path[0] != '/' && this.WorkingDirectory != null)
			{
				if (this.WorkingDirectory[this.WorkingDirectory.Length - 1] == '/')
				{
					result = string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[]
					{
						this.WorkingDirectory,
						path
					});
				}
				else
				{
					result = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
					{
						this.WorkingDirectory,
						path
					});
				}
			}
			return result;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00010AA4 File Offset: 0x0000ECA4
		protected override void OnChannelOpen()
		{
			this.SendMessage(new SftpInitRequest(3U));
			base.WaitOnHandle(this._sftpVersionConfirmed, base.OperationTimeout);
			if (this.ProtocolVersion > 3U || this.ProtocolVersion < 0U)
			{
				throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, "Server SFTP version {0} is not supported.", new object[]
				{
					this.ProtocolVersion
				}));
			}
			this.WorkingDirectory = this.RequestRealPath(".", false).First<KeyValuePair<string, SftpFileAttributes>>().Key;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00010B2C File Offset: 0x0000ED2C
		protected override void OnDataReceived(byte[] data)
		{
			this._data.AddRange(data);
			while (this._data.Count > 5)
			{
				int num = (int)this._data[0] << 24 | (int)this._data[1] << 16 | (int)this._data[2] << 8 | (int)this._data[3];
				if (this._data.Count < num + 4)
				{
					break;
				}
				int num2 = num + 4;
				byte[] array = new byte[num2];
				this._data.CopyTo(0, array, 0, num2);
				this._data.RemoveRange(0, num2);
				SftpMessage sftpMessage = SftpMessage.Load(this.ProtocolVersion, array, base.Encoding);
				try
				{
					SftpVersionResponse sftpVersionResponse = sftpMessage as SftpVersionResponse;
					if (sftpVersionResponse != null)
					{
						this.ProtocolVersion = sftpVersionResponse.Version;
						this._supportedExtensions = sftpVersionResponse.Extentions;
						this._sftpVersionConfirmed.Set();
					}
					else
					{
						this.HandleResponse(sftpMessage as SftpResponse);
					}
				}
				catch (Exception error)
				{
					base.RaiseError(error);
					break;
				}
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00010C44 File Offset: 0x0000EE44
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && this._sftpVersionConfirmed != null)
			{
				this._sftpVersionConfirmed.Dispose();
				this._sftpVersionConfirmed = null;
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00010C6C File Offset: 0x0000EE6C
		private void SendRequest(SftpRequest request)
		{
			Dictionary<uint, SftpRequest> requests = this._requests;
			lock (requests)
			{
				this._requests.Add(request.RequestId, request);
			}
			this.SendMessage(request);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00010CC0 File Offset: 0x0000EEC0
		public byte[] RequestOpen(string path, Flags flags, bool nullOnError = false)
		{
			SftpSession.<>c__DisplayClass26_0 CS$<>8__locals1 = new SftpSession.<>c__DisplayClass26_0();
			CS$<>8__locals1.handle = null;
			CS$<>8__locals1.exception = null;
			using (AutoResetEvent wait = new AutoResetEvent(false))
			{
				SftpOpenRequest request = new SftpOpenRequest(this.ProtocolVersion, this.NextRequestId, path, base.Encoding, flags, delegate(SftpHandleResponse response)
				{
					CS$<>8__locals1.handle = response.Handle;
					wait.Set();
				}, delegate(SftpStatusResponse response)
				{
					CS$<>8__locals1.exception = SftpSession.GetSftpException(response);
					wait.Set();
				});
				this.SendRequest(request);
				base.WaitOnHandle(wait, base.OperationTimeout);
			}
			if (!nullOnError && CS$<>8__locals1.exception != null)
			{
				throw CS$<>8__locals1.exception;
			}
			return CS$<>8__locals1.handle;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00010D84 File Offset: 0x0000EF84
		public void RequestClose(byte[] handle)
		{
			SftpSession.<>c__DisplayClass27_0 CS$<>8__locals1 = new SftpSession.<>c__DisplayClass27_0();
			CS$<>8__locals1.exception = null;
			using (AutoResetEvent wait = new AutoResetEvent(false))
			{
				SftpCloseRequest request = new SftpCloseRequest(this.ProtocolVersion, this.NextRequestId, handle, delegate(SftpStatusResponse response)
				{
					CS$<>8__locals1.exception = SftpSession.GetSftpException(response);
					wait.Set();
				});
				this.SendRequest(request);
				base.WaitOnHandle(wait, base.OperationTimeout);
			}
			if (CS$<>8__locals1.exception != null)
			{
				throw CS$<>8__locals1.exception;
			}
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00010E24 File Offset: 0x0000F024
		public byte[] RequestRead(byte[] handle, ulong offset, uint length)
		{
			SftpSession.<>c__DisplayClass28_0 CS$<>8__locals1 = new SftpSession.<>c__DisplayClass28_0();
			CS$<>8__locals1.exception = null;
			CS$<>8__locals1.data = null;
			using (AutoResetEvent wait = new AutoResetEvent(false))
			{
				SftpReadRequest request = new SftpReadRequest(this.ProtocolVersion, this.NextRequestId, handle, offset, length, delegate(SftpDataResponse response)
				{
					CS$<>8__locals1.data = response.Data;
					wait.Set();
				}, delegate(SftpStatusResponse response)
				{
					if (response.StatusCode != StatusCodes.Eof)
					{
						CS$<>8__locals1.exception = SftpSession.GetSftpException(response);
					}
					CS$<>8__locals1.data = Array<byte>.Empty;
					wait.Set();
				});
				this.SendRequest(request);
				base.WaitOnHandle(wait, base.OperationTimeout);
			}
			if (CS$<>8__locals1.exception != null)
			{
				throw CS$<>8__locals1.exception;
			}
			return CS$<>8__locals1.data;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00010EE0 File Offset: 0x0000F0E0
		public void RequestWrite(byte[] handle, ulong offset, byte[] data, int length, AutoResetEvent wait, Action<SftpStatusResponse> writeCompleted = null)
		{
			SshException exception = null;
			SftpWriteRequest request = new SftpWriteRequest(this.ProtocolVersion, this.NextRequestId, handle, offset, data, length, delegate(SftpStatusResponse response)
			{
				if (writeCompleted != null)
				{
					writeCompleted(response);
				}
				exception = SftpSession.GetSftpException(response);
				if (wait != null)
				{
					wait.Set();
				}
			});
			this.SendRequest(request);
			if (wait != null)
			{
				base.WaitOnHandle(wait, base.OperationTimeout);
			}
			if (exception != null)
			{
				throw exception;
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00010F60 File Offset: 0x0000F160
		public SftpFileAttributes RequestLStat(string path)
		{
			SftpSession.<>c__DisplayClass30_0 CS$<>8__locals1 = new SftpSession.<>c__DisplayClass30_0();
			CS$<>8__locals1.exception = null;
			CS$<>8__locals1.attributes = null;
			using (AutoResetEvent wait = new AutoResetEvent(false))
			{
				SftpLStatRequest request = new SftpLStatRequest(this.ProtocolVersion, this.NextRequestId, path, base.Encoding, delegate(SftpAttrsResponse response)
				{
					CS$<>8__locals1.attributes = response.Attributes;
					wait.Set();
				}, delegate(SftpStatusResponse response)
				{
					CS$<>8__locals1.exception = SftpSession.GetSftpException(response);
					wait.Set();
				});
				this.SendRequest(request);
				base.WaitOnHandle(wait, base.OperationTimeout);
			}
			if (CS$<>8__locals1.exception != null)
			{
				throw CS$<>8__locals1.exception;
			}
			return CS$<>8__locals1.attributes;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00011020 File Offset: 0x0000F220
		public SftpFileAttributes RequestFStat(byte[] handle)
		{
			SftpSession.<>c__DisplayClass31_0 CS$<>8__locals1 = new SftpSession.<>c__DisplayClass31_0();
			CS$<>8__locals1.exception = null;
			CS$<>8__locals1.attributes = null;
			using (AutoResetEvent wait = new AutoResetEvent(false))
			{
				SftpFStatRequest request = new SftpFStatRequest(this.ProtocolVersion, this.NextRequestId, handle, delegate(SftpAttrsResponse response)
				{
					CS$<>8__locals1.attributes = response.Attributes;
					wait.Set();
				}, delegate(SftpStatusResponse response)
				{
					CS$<>8__locals1.exception = SftpSession.GetSftpException(response);
					wait.Set();
				});
				this.SendRequest(request);
				base.WaitOnHandle(wait, base.OperationTimeout);
			}
			if (CS$<>8__locals1.exception != null)
			{
				throw CS$<>8__locals1.exception;
			}
			return CS$<>8__locals1.attributes;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x000110DC File Offset: 0x0000F2DC
		public void RequestSetStat(string path, SftpFileAttributes attributes)
		{
			SftpSession.<>c__DisplayClass32_0 CS$<>8__locals1 = new SftpSession.<>c__DisplayClass32_0();
			CS$<>8__locals1.exception = null;
			using (AutoResetEvent wait = new AutoResetEvent(false))
			{
				SftpSetStatRequest request = new SftpSetStatRequest(this.ProtocolVersion, this.NextRequestId, path, base.Encoding, attributes, delegate(SftpStatusResponse response)
				{
					CS$<>8__locals1.exception = SftpSession.GetSftpException(response);
					wait.Set();
				});
				this.SendRequest(request);
				base.WaitOnHandle(wait, base.OperationTimeout);
			}
			if (CS$<>8__locals1.exception != null)
			{
				throw CS$<>8__locals1.exception;
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00011184 File Offset: 0x0000F384
		public void RequestFSetStat(byte[] handle, SftpFileAttributes attributes)
		{
			SftpSession.<>c__DisplayClass33_0 CS$<>8__locals1 = new SftpSession.<>c__DisplayClass33_0();
			CS$<>8__locals1.exception = null;
			using (AutoResetEvent wait = new AutoResetEvent(false))
			{
				SftpFSetStatRequest request = new SftpFSetStatRequest(this.ProtocolVersion, this.NextRequestId, handle, attributes, delegate(SftpStatusResponse response)
				{
					CS$<>8__locals1.exception = SftpSession.GetSftpException(response);
					wait.Set();
				});
				this.SendRequest(request);
				base.WaitOnHandle(wait, base.OperationTimeout);
			}
			if (CS$<>8__locals1.exception != null)
			{
				throw CS$<>8__locals1.exception;
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00011228 File Offset: 0x0000F428
		public byte[] RequestOpenDir(string path, bool nullOnError = false)
		{
			SftpSession.<>c__DisplayClass34_0 CS$<>8__locals1 = new SftpSession.<>c__DisplayClass34_0();
			CS$<>8__locals1.exception = null;
			CS$<>8__locals1.handle = null;
			using (AutoResetEvent wait = new AutoResetEvent(false))
			{
				SftpOpenDirRequest request = new SftpOpenDirRequest(this.ProtocolVersion, this.NextRequestId, path, base.Encoding, delegate(SftpHandleResponse response)
				{
					CS$<>8__locals1.handle = response.Handle;
					wait.Set();
				}, delegate(SftpStatusResponse response)
				{
					CS$<>8__locals1.exception = SftpSession.GetSftpException(response);
					wait.Set();
				});
				this.SendRequest(request);
				base.WaitOnHandle(wait, base.OperationTimeout);
			}
			if (!nullOnError && CS$<>8__locals1.exception != null)
			{
				throw CS$<>8__locals1.exception;
			}
			return CS$<>8__locals1.handle;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x000112EC File Offset: 0x0000F4EC
		public KeyValuePair<string, SftpFileAttributes>[] RequestReadDir(byte[] handle)
		{
			SftpSession.<>c__DisplayClass35_0 CS$<>8__locals1 = new SftpSession.<>c__DisplayClass35_0();
			CS$<>8__locals1.exception = null;
			CS$<>8__locals1.result = null;
			using (AutoResetEvent wait = new AutoResetEvent(false))
			{
				SftpReadDirRequest request = new SftpReadDirRequest(this.ProtocolVersion, this.NextRequestId, handle, delegate(SftpNameResponse response)
				{
					CS$<>8__locals1.result = response.Files;
					wait.Set();
				}, delegate(SftpStatusResponse response)
				{
					if (response.StatusCode != StatusCodes.Eof)
					{
						CS$<>8__locals1.exception = SftpSession.GetSftpException(response);
					}
					wait.Set();
				});
				this.SendRequest(request);
				base.WaitOnHandle(wait, base.OperationTimeout);
			}
			if (CS$<>8__locals1.exception != null)
			{
				throw CS$<>8__locals1.exception;
			}
			return CS$<>8__locals1.result;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000113A8 File Offset: 0x0000F5A8
		public void RequestRemove(string path)
		{
			SftpSession.<>c__DisplayClass36_0 CS$<>8__locals1 = new SftpSession.<>c__DisplayClass36_0();
			CS$<>8__locals1.exception = null;
			using (AutoResetEvent wait = new AutoResetEvent(false))
			{
				SftpRemoveRequest request = new SftpRemoveRequest(this.ProtocolVersion, this.NextRequestId, path, base.Encoding, delegate(SftpStatusResponse response)
				{
					CS$<>8__locals1.exception = SftpSession.GetSftpException(response);
					wait.Set();
				});
				this.SendRequest(request);
				base.WaitOnHandle(wait, base.OperationTimeout);
			}
			if (CS$<>8__locals1.exception != null)
			{
				throw CS$<>8__locals1.exception;
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00011450 File Offset: 0x0000F650
		public void RequestMkDir(string path)
		{
			SftpSession.<>c__DisplayClass37_0 CS$<>8__locals1 = new SftpSession.<>c__DisplayClass37_0();
			CS$<>8__locals1.exception = null;
			using (AutoResetEvent wait = new AutoResetEvent(false))
			{
				SftpMkDirRequest request = new SftpMkDirRequest(this.ProtocolVersion, this.NextRequestId, path, base.Encoding, delegate(SftpStatusResponse response)
				{
					CS$<>8__locals1.exception = SftpSession.GetSftpException(response);
					wait.Set();
				});
				this.SendRequest(request);
				base.WaitOnHandle(wait, base.OperationTimeout);
			}
			if (CS$<>8__locals1.exception != null)
			{
				throw CS$<>8__locals1.exception;
			}
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x000114F8 File Offset: 0x0000F6F8
		public void RequestRmDir(string path)
		{
			SftpSession.<>c__DisplayClass38_0 CS$<>8__locals1 = new SftpSession.<>c__DisplayClass38_0();
			CS$<>8__locals1.exception = null;
			using (AutoResetEvent wait = new AutoResetEvent(false))
			{
				SftpRmDirRequest request = new SftpRmDirRequest(this.ProtocolVersion, this.NextRequestId, path, base.Encoding, delegate(SftpStatusResponse response)
				{
					CS$<>8__locals1.exception = SftpSession.GetSftpException(response);
					wait.Set();
				});
				this.SendRequest(request);
				base.WaitOnHandle(wait, base.OperationTimeout);
			}
			if (CS$<>8__locals1.exception != null)
			{
				throw CS$<>8__locals1.exception;
			}
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x000115A0 File Offset: 0x0000F7A0
		internal KeyValuePair<string, SftpFileAttributes>[] RequestRealPath(string path, bool nullOnError = false)
		{
			SftpSession.<>c__DisplayClass39_0 CS$<>8__locals1 = new SftpSession.<>c__DisplayClass39_0();
			CS$<>8__locals1.exception = null;
			CS$<>8__locals1.result = null;
			using (AutoResetEvent wait = new AutoResetEvent(false))
			{
				SftpRealPathRequest request = new SftpRealPathRequest(this.ProtocolVersion, this.NextRequestId, path, base.Encoding, delegate(SftpNameResponse response)
				{
					CS$<>8__locals1.result = response.Files;
					wait.Set();
				}, delegate(SftpStatusResponse response)
				{
					CS$<>8__locals1.exception = SftpSession.GetSftpException(response);
					wait.Set();
				});
				this.SendRequest(request);
				base.WaitOnHandle(wait, base.OperationTimeout);
			}
			if (!nullOnError && CS$<>8__locals1.exception != null)
			{
				throw CS$<>8__locals1.exception;
			}
			return CS$<>8__locals1.result;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00011664 File Offset: 0x0000F864
		internal SftpFileAttributes RequestStat(string path, bool nullOnError = false)
		{
			SftpSession.<>c__DisplayClass40_0 CS$<>8__locals1 = new SftpSession.<>c__DisplayClass40_0();
			CS$<>8__locals1.exception = null;
			CS$<>8__locals1.attributes = null;
			using (AutoResetEvent wait = new AutoResetEvent(false))
			{
				SftpStatRequest request = new SftpStatRequest(this.ProtocolVersion, this.NextRequestId, path, base.Encoding, delegate(SftpAttrsResponse response)
				{
					CS$<>8__locals1.attributes = response.Attributes;
					wait.Set();
				}, delegate(SftpStatusResponse response)
				{
					CS$<>8__locals1.exception = SftpSession.GetSftpException(response);
					wait.Set();
				});
				this.SendRequest(request);
				base.WaitOnHandle(wait, base.OperationTimeout);
			}
			if (!nullOnError && CS$<>8__locals1.exception != null)
			{
				throw CS$<>8__locals1.exception;
			}
			return CS$<>8__locals1.attributes;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00011728 File Offset: 0x0000F928
		public void RequestRename(string oldPath, string newPath)
		{
			SftpSession.<>c__DisplayClass41_0 CS$<>8__locals1 = new SftpSession.<>c__DisplayClass41_0();
			if (this.ProtocolVersion < 2U)
			{
				throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, "SSH_FXP_RENAME operation is not supported in {0} version that server operates in.", new object[]
				{
					this.ProtocolVersion
				}));
			}
			CS$<>8__locals1.exception = null;
			using (AutoResetEvent wait = new AutoResetEvent(false))
			{
				SftpRenameRequest request = new SftpRenameRequest(this.ProtocolVersion, this.NextRequestId, oldPath, newPath, base.Encoding, delegate(SftpStatusResponse response)
				{
					CS$<>8__locals1.exception = SftpSession.GetSftpException(response);
					wait.Set();
				});
				this.SendRequest(request);
				base.WaitOnHandle(wait, base.OperationTimeout);
			}
			if (CS$<>8__locals1.exception != null)
			{
				throw CS$<>8__locals1.exception;
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00011804 File Offset: 0x0000FA04
		internal KeyValuePair<string, SftpFileAttributes>[] RequestReadLink(string path, bool nullOnError = false)
		{
			SftpSession.<>c__DisplayClass42_0 CS$<>8__locals1 = new SftpSession.<>c__DisplayClass42_0();
			if (this.ProtocolVersion < 3U)
			{
				throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, "SSH_FXP_READLINK operation is not supported in {0} version that server operates in.", new object[]
				{
					this.ProtocolVersion
				}));
			}
			CS$<>8__locals1.exception = null;
			CS$<>8__locals1.result = null;
			using (AutoResetEvent wait = new AutoResetEvent(false))
			{
				SftpReadLinkRequest request = new SftpReadLinkRequest(this.ProtocolVersion, this.NextRequestId, path, base.Encoding, delegate(SftpNameResponse response)
				{
					CS$<>8__locals1.result = response.Files;
					wait.Set();
				}, delegate(SftpStatusResponse response)
				{
					CS$<>8__locals1.exception = SftpSession.GetSftpException(response);
					wait.Set();
				});
				this.SendRequest(request);
				base.WaitOnHandle(wait, base.OperationTimeout);
			}
			if (!nullOnError && CS$<>8__locals1.exception != null)
			{
				throw CS$<>8__locals1.exception;
			}
			return CS$<>8__locals1.result;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x000118F8 File Offset: 0x0000FAF8
		public void RequestSymLink(string linkpath, string targetpath)
		{
			SftpSession.<>c__DisplayClass43_0 CS$<>8__locals1 = new SftpSession.<>c__DisplayClass43_0();
			if (this.ProtocolVersion < 3U)
			{
				throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, "SSH_FXP_SYMLINK operation is not supported in {0} version that server operates in.", new object[]
				{
					this.ProtocolVersion
				}));
			}
			CS$<>8__locals1.exception = null;
			using (AutoResetEvent wait = new AutoResetEvent(false))
			{
				SftpSymLinkRequest request = new SftpSymLinkRequest(this.ProtocolVersion, this.NextRequestId, linkpath, targetpath, base.Encoding, delegate(SftpStatusResponse response)
				{
					CS$<>8__locals1.exception = SftpSession.GetSftpException(response);
					wait.Set();
				});
				this.SendRequest(request);
				base.WaitOnHandle(wait, base.OperationTimeout);
			}
			if (CS$<>8__locals1.exception != null)
			{
				throw CS$<>8__locals1.exception;
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000119D4 File Offset: 0x0000FBD4
		public void RequestPosixRename(string oldPath, string newPath)
		{
			SftpSession.<>c__DisplayClass44_0 CS$<>8__locals1 = new SftpSession.<>c__DisplayClass44_0();
			if (this.ProtocolVersion < 3U)
			{
				throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, "SSH_FXP_EXTENDED operation is not supported in {0} version that server operates in.", new object[]
				{
					this.ProtocolVersion
				}));
			}
			CS$<>8__locals1.exception = null;
			using (AutoResetEvent wait = new AutoResetEvent(false))
			{
				PosixRenameRequest posixRenameRequest = new PosixRenameRequest(this.ProtocolVersion, this.NextRequestId, oldPath, newPath, base.Encoding, delegate(SftpStatusResponse response)
				{
					CS$<>8__locals1.exception = SftpSession.GetSftpException(response);
					wait.Set();
				});
				if (!this._supportedExtensions.ContainsKey(posixRenameRequest.Name))
				{
					throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, "Extension method {0} currently not supported by the server.", new object[]
					{
						posixRenameRequest.Name
					}));
				}
				this.SendRequest(posixRenameRequest);
				base.WaitOnHandle(wait, base.OperationTimeout);
			}
			if (CS$<>8__locals1.exception != null)
			{
				throw CS$<>8__locals1.exception;
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00011AE4 File Offset: 0x0000FCE4
		public SftpFileSytemInformation RequestStatVfs(string path, bool nullOnError = false)
		{
			SftpSession.<>c__DisplayClass45_0 CS$<>8__locals1 = new SftpSession.<>c__DisplayClass45_0();
			if (this.ProtocolVersion < 3U)
			{
				throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, "SSH_FXP_EXTENDED operation is not supported in {0} version that server operates in.", new object[]
				{
					this.ProtocolVersion
				}));
			}
			CS$<>8__locals1.exception = null;
			CS$<>8__locals1.information = null;
			using (AutoResetEvent wait = new AutoResetEvent(false))
			{
				StatVfsRequest statVfsRequest = new StatVfsRequest(this.ProtocolVersion, this.NextRequestId, path, base.Encoding, delegate(SftpExtendedReplyResponse response)
				{
					CS$<>8__locals1.information = response.GetReply<StatVfsReplyInfo>().Information;
					wait.Set();
				}, delegate(SftpStatusResponse response)
				{
					CS$<>8__locals1.exception = SftpSession.GetSftpException(response);
					wait.Set();
				});
				if (!this._supportedExtensions.ContainsKey(statVfsRequest.Name))
				{
					throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, "Extension method {0} currently not supported by the server.", new object[]
					{
						statVfsRequest.Name
					}));
				}
				this.SendRequest(statVfsRequest);
				base.WaitOnHandle(wait, base.OperationTimeout);
			}
			if (!nullOnError && CS$<>8__locals1.exception != null)
			{
				throw CS$<>8__locals1.exception;
			}
			return CS$<>8__locals1.information;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00011C10 File Offset: 0x0000FE10
		internal SftpFileSytemInformation RequestFStatVfs(byte[] handle, bool nullOnError = false)
		{
			SftpSession.<>c__DisplayClass46_0 CS$<>8__locals1 = new SftpSession.<>c__DisplayClass46_0();
			if (this.ProtocolVersion < 3U)
			{
				throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, "SSH_FXP_EXTENDED operation is not supported in {0} version that server operates in.", new object[]
				{
					this.ProtocolVersion
				}));
			}
			CS$<>8__locals1.exception = null;
			CS$<>8__locals1.information = null;
			using (AutoResetEvent wait = new AutoResetEvent(false))
			{
				FStatVfsRequest fstatVfsRequest = new FStatVfsRequest(this.ProtocolVersion, this.NextRequestId, handle, delegate(SftpExtendedReplyResponse response)
				{
					CS$<>8__locals1.information = response.GetReply<StatVfsReplyInfo>().Information;
					wait.Set();
				}, delegate(SftpStatusResponse response)
				{
					CS$<>8__locals1.exception = SftpSession.GetSftpException(response);
					wait.Set();
				});
				if (!this._supportedExtensions.ContainsKey(fstatVfsRequest.Name))
				{
					throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, "Extension method {0} currently not supported by the server.", new object[]
					{
						fstatVfsRequest.Name
					}));
				}
				this.SendRequest(fstatVfsRequest);
				base.WaitOnHandle(wait, base.OperationTimeout);
			}
			if (!nullOnError && CS$<>8__locals1.exception != null)
			{
				throw CS$<>8__locals1.exception;
			}
			return CS$<>8__locals1.information;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00011D38 File Offset: 0x0000FF38
		internal void HardLink(string oldPath, string newPath)
		{
			SftpSession.<>c__DisplayClass47_0 CS$<>8__locals1 = new SftpSession.<>c__DisplayClass47_0();
			if (this.ProtocolVersion < 3U)
			{
				throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, "SSH_FXP_EXTENDED operation is not supported in {0} version that server operates in.", new object[]
				{
					this.ProtocolVersion
				}));
			}
			CS$<>8__locals1.exception = null;
			using (AutoResetEvent wait = new AutoResetEvent(false))
			{
				HardLinkRequest hardLinkRequest = new HardLinkRequest(this.ProtocolVersion, this.NextRequestId, oldPath, newPath, delegate(SftpStatusResponse response)
				{
					CS$<>8__locals1.exception = SftpSession.GetSftpException(response);
					wait.Set();
				});
				if (!this._supportedExtensions.ContainsKey(hardLinkRequest.Name))
				{
					throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, "Extension method {0} currently not supported by the server.", new object[]
					{
						hardLinkRequest.Name
					}));
				}
				this.SendRequest(hardLinkRequest);
				base.WaitOnHandle(wait, base.OperationTimeout);
			}
			if (CS$<>8__locals1.exception != null)
			{
				throw CS$<>8__locals1.exception;
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00011E44 File Offset: 0x00010044
		public uint CalculateOptimalReadLength(uint bufferSize)
		{
			uint localPacketSize = base.Channel.LocalPacketSize;
			return Math.Min(bufferSize, localPacketSize) - 13U;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00011E68 File Offset: 0x00010068
		public uint CalculateOptimalWriteLength(uint bufferSize, byte[] handle)
		{
			uint num = (uint)(25 + handle.Length);
			uint remotePacketSize = base.Channel.RemotePacketSize;
			return Math.Min(bufferSize, remotePacketSize) - num;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00011E94 File Offset: 0x00010094
		private static SshException GetSftpException(SftpStatusResponse response)
		{
			if (response.StatusCode == StatusCodes.Ok)
			{
				return null;
			}
			if (response.StatusCode == StatusCodes.PermissionDenied)
			{
				return new SftpPermissionDeniedException(response.ErrorMessage);
			}
			if (response.StatusCode == StatusCodes.NoSuchFile)
			{
				return new SftpPathNotFoundException(response.ErrorMessage);
			}
			return new SshException(response.ErrorMessage);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00011EE0 File Offset: 0x000100E0
		private void HandleResponse(SftpResponse response)
		{
			Dictionary<uint, SftpRequest> requests = this._requests;
			SftpRequest sftpRequest;
			lock (requests)
			{
				this._requests.TryGetValue(response.ResponseId, out sftpRequest);
				if (sftpRequest != null)
				{
					this._requests.Remove(response.ResponseId);
				}
			}
			if (sftpRequest == null)
			{
				throw new InvalidOperationException("Invalid response.");
			}
			sftpRequest.Complete(response);
		}

		// Token: 0x040001AB RID: 427
		private const int MaximumSupportedVersion = 3;

		// Token: 0x040001AC RID: 428
		private const int MinimumSupportedVersion = 0;

		// Token: 0x040001AD RID: 429
		private readonly Dictionary<uint, SftpRequest> _requests = new Dictionary<uint, SftpRequest>();

		// Token: 0x040001AE RID: 430
		private readonly List<byte> _data = new List<byte>(32768);

		// Token: 0x040001AF RID: 431
		private EventWaitHandle _sftpVersionConfirmed = new AutoResetEvent(false);

		// Token: 0x040001B0 RID: 432
		private IDictionary<string, string> _supportedExtensions;

		// Token: 0x040001B3 RID: 435
		private long _requestId;
	}
}
