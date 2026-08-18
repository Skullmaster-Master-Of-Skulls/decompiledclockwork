using System;
using System.Data.Common;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Data.OracleClient
{
	// Token: 0x0200004A RID: 74
	public sealed class OracleBFile : Stream, ICloneable, INullable, IDisposable
	{
		// Token: 0x06000221 RID: 545 RVA: 0x0005CC64 File Offset: 0x0005C064
		internal OracleBFile()
		{
			this._lob = OracleLob.Null;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0005CC84 File Offset: 0x0005C084
		internal OracleBFile(OciLobLocator lobLocator)
		{
			this._lob = new OracleLob(lobLocator);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0005CCA4 File Offset: 0x0005C0A4
		internal OracleBFile(OracleBFile bfile)
		{
			this._lob = (OracleLob)bfile._lob.Clone();
			this._fileName = bfile._fileName;
			this._directoryAlias = bfile._directoryAlias;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0005CCF4 File Offset: 0x0005C0F4
		public override bool CanRead
		{
			get
			{
				return this.IsNull || !this.IsDisposed;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0005CD14 File Offset: 0x0005C114
		public override bool CanSeek
		{
			get
			{
				return this.IsNull || !this.IsDisposed;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0005CD34 File Offset: 0x0005C134
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0005CD44 File Offset: 0x0005C144
		public OracleConnection Connection
		{
			get
			{
				this.AssertInternalLobIsValid();
				return this._lob.Connection;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0005CD64 File Offset: 0x0005C164
		internal OciHandle Descriptor
		{
			get
			{
				return this.LobLocator.Descriptor;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0005CD84 File Offset: 0x0005C184
		public string DirectoryName
		{
			get
			{
				this.AssertInternalLobIsValid();
				if (this.IsNull)
				{
					return string.Empty;
				}
				if (this._directoryAlias == null)
				{
					this.GetNames();
				}
				return this._directoryAlias;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0005CDC4 File Offset: 0x0005C1C4
		public bool FileExists
		{
			get
			{
				this.AssertInternalLobIsValid();
				if (this.IsNull)
				{
					return false;
				}
				this._lob.AssertConnectionIsOpen();
				int num2;
				int num = TracedNativeMethods.OCILobFileExists(this.ServiceContextHandle, this.ErrorHandle, this.Descriptor, out num2);
				if (num != 0)
				{
					this.Connection.CheckError(this.ErrorHandle, num);
				}
				return num2 != 0;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0005CE24 File Offset: 0x0005C224
		public string FileName
		{
			get
			{
				this.AssertInternalLobIsValid();
				if (this.IsNull)
				{
					return string.Empty;
				}
				if (this._fileName == null)
				{
					this.GetNames();
				}
				return this._fileName;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0005CE64 File Offset: 0x0005C264
		internal OciErrorHandle ErrorHandle
		{
			get
			{
				return this._lob.ErrorHandle;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0005CE84 File Offset: 0x0005C284
		private bool IsDisposed
		{
			get
			{
				return null == this._lob;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0005CEA4 File Offset: 0x0005C2A4
		public bool IsNull
		{
			get
			{
				return OracleLob.Null == this._lob;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0005CEC4 File Offset: 0x0005C2C4
		public override long Length
		{
			get
			{
				this.AssertInternalLobIsValid();
				if (this.IsNull)
				{
					return 0L;
				}
				return this._lob.Length;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0005CEF4 File Offset: 0x0005C2F4
		internal OciLobLocator LobLocator
		{
			get
			{
				return this._lob.LobLocator;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0005CF14 File Offset: 0x0005C314
		// (set) Token: 0x06000232 RID: 562 RVA: 0x0005CF44 File Offset: 0x0005C344
		public override long Position
		{
			get
			{
				this.AssertInternalLobIsValid();
				if (this.IsNull)
				{
					return 0L;
				}
				return this._lob.Position;
			}
			set
			{
				this.AssertInternalLobIsValid();
				if (!this.IsNull)
				{
					this._lob.Position = value;
				}
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0005CF74 File Offset: 0x0005C374
		internal OciServiceContextHandle ServiceContextHandle
		{
			get
			{
				return this._lob.ServiceContextHandle;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0005CF94 File Offset: 0x0005C394
		public object Value
		{
			get
			{
				this.AssertInternalLobIsValid();
				if (this.IsNull)
				{
					return DBNull.Value;
				}
				this.EnsureLobIsOpened();
				return this._lob.Value;
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0005CFD4 File Offset: 0x0005C3D4
		internal void AssertInternalLobIsValid()
		{
			if (this.IsDisposed)
			{
				throw ADP.ObjectDisposed("OracleBFile");
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0005CFF4 File Offset: 0x0005C3F4
		public object Clone()
		{
			return new OracleBFile(this);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0005D014 File Offset: 0x0005C414
		public long CopyTo(OracleLob destination)
		{
			return this.CopyTo(0L, destination, 0L, this.Length);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0005D034 File Offset: 0x0005C434
		public long CopyTo(OracleLob destination, long destinationOffset)
		{
			return this.CopyTo(0L, destination, destinationOffset, this.Length);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0005D054 File Offset: 0x0005C454
		public long CopyTo(long sourceOffset, OracleLob destination, long destinationOffset, long amount)
		{
			this.AssertInternalLobIsValid();
			if (destination == null)
			{
				throw ADP.ArgumentNull("destination");
			}
			if (destination.IsNull)
			{
				throw ADP.LobWriteInvalidOnNull();
			}
			if (this._lob.IsNull)
			{
				return 0L;
			}
			this._lob.AssertConnectionIsOpen();
			this._lob.AssertAmountIsValid(amount, "amount");
			this._lob.AssertAmountIsValid(sourceOffset, "sourceOffset");
			this._lob.AssertAmountIsValid(destinationOffset, "destinationOffset");
			this._lob.AssertTransactionExists();
			long num = Math.Min(this.Length - sourceOffset, amount);
			long num2 = destinationOffset + 1L;
			long num3 = sourceOffset + 1L;
			if (0L >= num)
			{
				return 0L;
			}
			int num4 = TracedNativeMethods.OCILobLoadFromFile(this.ServiceContextHandle, this.ErrorHandle, destination.Descriptor, this.Descriptor, (uint)num, (uint)num2, (uint)num3);
			if (num4 != 0)
			{
				this.Connection.CheckError(this.ErrorHandle, num4);
			}
			return num;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0005D144 File Offset: 0x0005C544
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				OracleLob lob = this._lob;
				if (lob != null)
				{
					lob.Close();
				}
			}
			this._lob = null;
			this._fileName = null;
			this._directoryAlias = null;
			base.Dispose(disposing);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0005D184 File Offset: 0x0005C584
		private void EnsureLobIsOpened()
		{
			this.LobLocator.Open(OracleLobOpenMode.ReadOnly);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0005D1A4 File Offset: 0x0005C5A4
		public override void Flush()
		{
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0005D1B4 File Offset: 0x0005C5B4
		internal void GetNames()
		{
			this._lob.AssertConnectionIsOpen();
			short num = this.Connection.EnvironmentHandle.IsUnicode ? 2 : 1;
			ushort num2;
			int offset;
			ushort num3;
			checked
			{
				num2 = (ushort)(30 * num);
				offset = (int)num2;
				num3 = (ushort)(255 * num);
			}
			NativeBuffer scratchBuffer = this.Connection.GetScratchBuffer((int)(num2 + num3));
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				scratchBuffer.DangerousAddRef(ref flag);
				int num4 = TracedNativeMethods.OCILobFileGetName(this.Connection.EnvironmentHandle, this.ErrorHandle, this.Descriptor, scratchBuffer.DangerousGetDataPtr(), ref num2, scratchBuffer.DangerousGetDataPtr(offset), ref num3);
				if (num4 != 0)
				{
					this.Connection.CheckError(this.ErrorHandle, num4);
				}
				this._directoryAlias = this.Connection.GetString(scratchBuffer.ReadBytes(0, (int)num2));
				this._fileName = this.Connection.GetString(scratchBuffer.ReadBytes(offset, (int)num3));
			}
			finally
			{
				if (flag)
				{
					scratchBuffer.DangerousRelease();
				}
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0005D2C4 File Offset: 0x0005C6C4
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.AssertInternalLobIsValid();
			if (!this.IsNull)
			{
				this.EnsureLobIsOpened();
			}
			return this._lob.Read(buffer, offset, count);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0005D304 File Offset: 0x0005C704
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.AssertInternalLobIsValid();
			return this._lob.Seek(offset, origin);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0005D334 File Offset: 0x0005C734
		public void SetFileName(string directory, string file)
		{
			this.AssertInternalLobIsValid();
			if (!this.IsNull)
			{
				this._lob.AssertConnectionIsOpen();
				this._lob.AssertTransactionExists();
				OciFileDescriptor ociFileDescriptor = (OciFileDescriptor)this.LobLocator.Descriptor;
				if (ociFileDescriptor != null)
				{
					this.LobLocator.ForceClose();
					int num = TracedNativeMethods.OCILobFileSetName(this.Connection.EnvironmentHandle, this.ErrorHandle, ociFileDescriptor, directory, file);
					if (num != 0)
					{
						this.Connection.CheckError(this.ErrorHandle, num);
					}
					this.LobLocator.ForceOpen();
					this._fileName = null;
					this._directoryAlias = null;
					try
					{
						this._lob.Position = 0L;
					}
					catch (Exception e)
					{
						if (!ADP.IsCatchableExceptionType(e))
						{
							throw;
						}
					}
				}
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0005D414 File Offset: 0x0005C814
		public override void SetLength(long value)
		{
			this.AssertInternalLobIsValid();
			throw ADP.ReadOnlyLob();
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0005D434 File Offset: 0x0005C834
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.AssertInternalLobIsValid();
			throw ADP.ReadOnlyLob();
		}

		// Token: 0x0400032B RID: 811
		private const short MaxDirectoryAliasChars = 30;

		// Token: 0x0400032C RID: 812
		private const short MaxFileAliasChars = 255;

		// Token: 0x0400032D RID: 813
		private OracleLob _lob;

		// Token: 0x0400032E RID: 814
		private string _fileName;

		// Token: 0x0400032F RID: 815
		private string _directoryAlias;

		// Token: 0x04000330 RID: 816
		public new static readonly OracleBFile Null = new OracleBFile();
	}
}
