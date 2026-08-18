using System;
using System.Data.Common;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Data.OracleClient
{
	// Token: 0x0200006C RID: 108
	public sealed class OracleLob : Stream, ICloneable, IDisposable, INullable
	{
		// Token: 0x0600051A RID: 1306 RVA: 0x0006B164 File Offset: 0x0006A564
		internal OracleLob()
		{
			this._isNull = true;
			this._lobType = OracleType.Blob;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0006B194 File Offset: 0x0006A594
		internal OracleLob(OciLobLocator lobLocator)
		{
			this._lobLocator = lobLocator.Clone();
			this._lobType = this._lobLocator.LobType;
			this._charsetForm = ((OracleType.NClob == this._lobType) ? OCI.CHARSETFORM.SQLCS_NCHAR : OCI.CHARSETFORM.SQLCS_IMPLICIT);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0006B1E4 File Offset: 0x0006A5E4
		internal OracleLob(OracleLob lob)
		{
			this._lobLocator = lob._lobLocator.Clone();
			this._lobType = lob._lobLocator.LobType;
			this._charsetForm = lob._charsetForm;
			this._currentPosition = lob._currentPosition;
			this._isTemporaryState = lob._isTemporaryState;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0006B244 File Offset: 0x0006A644
		internal OracleLob(OracleConnection connection, OracleType oracleType)
		{
			this._lobLocator = new OciLobLocator(connection, oracleType);
			this._lobType = oracleType;
			this._charsetForm = ((OracleType.NClob == this._lobType) ? OCI.CHARSETFORM.SQLCS_NCHAR : OCI.CHARSETFORM.SQLCS_IMPLICIT);
			this._isTemporaryState = 1;
			OCI.LOB_TYPE lobtype = (OracleType.Blob == oracleType) ? OCI.LOB_TYPE.OCI_TEMP_BLOB : OCI.LOB_TYPE.OCI_TEMP_CLOB;
			int num = TracedNativeMethods.OCILobCreateTemporary(connection.ServiceContextHandle, connection.ErrorHandle, this._lobLocator.Descriptor, 0, this._charsetForm, lobtype, 0, OCI.DURATION.OCI_DURATION_BEGIN);
			if (num != 0)
			{
				connection.CheckError(this.ErrorHandle, num);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0006B2D4 File Offset: 0x0006A6D4
		public override bool CanRead
		{
			get
			{
				return this.IsNull || !this.IsDisposed;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x0006B2F4 File Offset: 0x0006A6F4
		public override bool CanSeek
		{
			get
			{
				return this.IsNull || !this.IsDisposed;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0006B314 File Offset: 0x0006A714
		public override bool CanWrite
		{
			get
			{
				bool result = OracleType.BFile != this._lobType;
				if (!this.IsNull)
				{
					result = !this.IsDisposed;
				}
				return result;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0006B344 File Offset: 0x0006A744
		public int ChunkSize
		{
			get
			{
				this.AssertObjectNotDisposed();
				if (this.IsNull)
				{
					return 0;
				}
				this.AssertConnectionIsOpen();
				uint result = 0U;
				int num = TracedNativeMethods.OCILobGetChunkSize(this.ServiceContextHandle, this.ErrorHandle, this.Descriptor, out result);
				if (num != 0)
				{
					this.Connection.CheckError(this.ErrorHandle, num);
				}
				return (int)result;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0006B3A4 File Offset: 0x0006A7A4
		public OracleConnection Connection
		{
			get
			{
				this.AssertObjectNotDisposed();
				OciLobLocator lobLocator = this.LobLocator;
				if (lobLocator == null)
				{
					return null;
				}
				return lobLocator.Connection;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x0006B3D4 File Offset: 0x0006A7D4
		private bool ConnectionIsClosed
		{
			get
			{
				return this.LobLocator == null || this.LobLocator.ConnectionIsClosed;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0006B404 File Offset: 0x0006A804
		private uint CurrentOraclePosition
		{
			get
			{
				return (uint)this.AdjustOffsetToOracle(this._currentPosition) + 1U;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0006B424 File Offset: 0x0006A824
		internal OciHandle Descriptor
		{
			get
			{
				return this.LobLocator.Descriptor;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0006B444 File Offset: 0x0006A844
		internal OciErrorHandle ErrorHandle
		{
			get
			{
				return this.LobLocator.ErrorHandle;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0006B464 File Offset: 0x0006A864
		public bool IsBatched
		{
			get
			{
				if (this.IsNull || this.IsDisposed || this.ConnectionIsClosed)
				{
					return false;
				}
				int num2;
				int num = TracedNativeMethods.OCILobIsOpen(this.ServiceContextHandle, this.ErrorHandle, this.Descriptor, out num2);
				if (num != 0)
				{
					this.Connection.CheckError(this.ErrorHandle, num);
				}
				return num2 != 0;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0006B4C4 File Offset: 0x0006A8C4
		private bool IsCharacterLob
		{
			get
			{
				return OracleType.Clob == this._lobType || OracleType.NClob == this._lobType;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0006B4F4 File Offset: 0x0006A8F4
		private bool IsDisposed
		{
			get
			{
				return !this._isNull && null == this.LobLocator;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0006B514 File Offset: 0x0006A914
		public bool IsNull
		{
			get
			{
				return this._isNull;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0006B534 File Offset: 0x0006A934
		public bool IsTemporary
		{
			get
			{
				this.AssertObjectNotDisposed();
				if (this.IsNull)
				{
					return false;
				}
				this.AssertConnectionIsOpen();
				if (this._isTemporaryState == 0)
				{
					int num2;
					int num = TracedNativeMethods.OCILobIsTemporary(this.Connection.EnvironmentHandle, this.ErrorHandle, this.Descriptor, out num2);
					if (num != 0)
					{
						this.Connection.CheckError(this.ErrorHandle, num);
					}
					this._isTemporaryState = ((num2 != 0) ? 1 : 2);
				}
				return 1 == this._isTemporaryState;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x0006B5B4 File Offset: 0x0006A9B4
		internal OciLobLocator LobLocator
		{
			get
			{
				return this._lobLocator;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0006B5D4 File Offset: 0x0006A9D4
		public OracleType LobType
		{
			get
			{
				return this._lobType;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0006B5F4 File Offset: 0x0006A9F4
		public override long Length
		{
			get
			{
				this.AssertObjectNotDisposed();
				if (this.IsNull)
				{
					return 0L;
				}
				this.AssertConnectionIsOpen();
				uint num2;
				int num = TracedNativeMethods.OCILobGetLength(this.ServiceContextHandle, this.ErrorHandle, this.Descriptor, out num2);
				if (num != 0)
				{
					this.Connection.CheckError(this.ErrorHandle, num);
				}
				return this.AdjustOracleToOffset((long)((ulong)num2));
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0006B654 File Offset: 0x0006AA54
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x0006B684 File Offset: 0x0006AA84
		public override long Position
		{
			get
			{
				this.AssertObjectNotDisposed();
				if (this.IsNull)
				{
					return 0L;
				}
				this.AssertConnectionIsOpen();
				return this._currentPosition;
			}
			set
			{
				if (!this.IsNull)
				{
					this.Seek(value, SeekOrigin.Begin);
				}
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0006B6A4 File Offset: 0x0006AAA4
		internal OciServiceContextHandle ServiceContextHandle
		{
			get
			{
				return this.LobLocator.ServiceContextHandle;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0006B6C4 File Offset: 0x0006AAC4
		public object Value
		{
			get
			{
				this.AssertObjectNotDisposed();
				if (this.IsNull)
				{
					return DBNull.Value;
				}
				long currentPosition = this._currentPosition;
				int num = (int)this.Length;
				bool flag = OracleType.Blob == this._lobType || OracleType.BFile == this._lobType;
				if (num != 0)
				{
					string result;
					try
					{
						this.Seek(0L, SeekOrigin.Begin);
						if (flag)
						{
							byte[] array = new byte[num];
							this.Read(array, 0, num);
							return array;
						}
						try
						{
							StreamReader streamReader = new StreamReader(this, Encoding.Unicode);
							result = streamReader.ReadToEnd();
						}
						finally
						{
						}
					}
					finally
					{
						this._currentPosition = currentPosition;
					}
					return result;
				}
				if (flag)
				{
					return new byte[0];
				}
				return string.Empty;
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0006B7A4 File Offset: 0x0006ABA4
		internal int AdjustOffsetToOracle(int amount)
		{
			return this.IsCharacterLob ? (amount / 2) : amount;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0006B7C4 File Offset: 0x0006ABC4
		internal long AdjustOffsetToOracle(long amount)
		{
			return this.IsCharacterLob ? (amount / 2L) : amount;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0006B7E4 File Offset: 0x0006ABE4
		internal int AdjustOracleToOffset(int amount)
		{
			return this.IsCharacterLob ? checked(amount * 2) : amount;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0006B804 File Offset: 0x0006AC04
		internal long AdjustOracleToOffset(long amount)
		{
			return this.IsCharacterLob ? checked(amount * 2L) : amount;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0006B824 File Offset: 0x0006AC24
		internal void AssertAmountIsEven(long amount, string argName)
		{
			if (this.IsCharacterLob && 1L == (amount & 1L))
			{
				throw ADP.LobAmountMustBeEven(argName);
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0006B854 File Offset: 0x0006AC54
		internal void AssertAmountIsValidOddOK(long amount, string argName)
		{
			if (amount < 0L || amount >= (long)((ulong)-1))
			{
				throw ADP.LobAmountExceeded(argName);
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0006B874 File Offset: 0x0006AC74
		internal void AssertAmountIsValid(long amount, string argName)
		{
			this.AssertAmountIsValidOddOK(amount, argName);
			this.AssertAmountIsEven(amount, argName);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0006B894 File Offset: 0x0006AC94
		internal void AssertConnectionIsOpen()
		{
			if (this.ConnectionIsClosed)
			{
				throw ADP.ClosedConnectionError();
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0006B8B4 File Offset: 0x0006ACB4
		internal void AssertObjectNotDisposed()
		{
			if (this.IsDisposed)
			{
				throw ADP.ObjectDisposed("OracleLob");
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0006B8D4 File Offset: 0x0006ACD4
		internal void AssertPositionIsValid()
		{
			if (this.IsCharacterLob && 1L == (this._currentPosition & 1L))
			{
				throw ADP.LobPositionMustBeEven();
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0006B904 File Offset: 0x0006AD04
		internal void AssertTransactionExists()
		{
			if (!this.Connection.HasTransaction)
			{
				throw ADP.LobWriteRequiresTransaction();
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0006B924 File Offset: 0x0006AD24
		public void Append(OracleLob source)
		{
			if (source == null)
			{
				throw ADP.ArgumentNull("source");
			}
			this.AssertObjectNotDisposed();
			source.AssertObjectNotDisposed();
			if (this.IsNull)
			{
				throw ADP.LobWriteInvalidOnNull();
			}
			if (!source.IsNull)
			{
				this.AssertConnectionIsOpen();
				int num = TracedNativeMethods.OCILobAppend(this.ServiceContextHandle, this.ErrorHandle, this.Descriptor, source.Descriptor);
				if (num != 0)
				{
					this.Connection.CheckError(this.ErrorHandle, num);
				}
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0006B9A4 File Offset: 0x0006ADA4
		public void BeginBatch()
		{
			this.BeginBatch(OracleLobOpenMode.ReadOnly);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0006B9C4 File Offset: 0x0006ADC4
		public void BeginBatch(OracleLobOpenMode mode)
		{
			this.AssertObjectNotDisposed();
			if (!this.IsNull)
			{
				this.AssertConnectionIsOpen();
				this.LobLocator.Open(mode);
			}
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0006B9F4 File Offset: 0x0006ADF4
		public object Clone()
		{
			this.AssertObjectNotDisposed();
			if (this.IsNull)
			{
				return OracleLob.Null;
			}
			this.AssertConnectionIsOpen();
			return new OracleLob(this);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0006BA24 File Offset: 0x0006AE24
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && !this.IsNull && !this.ConnectionIsClosed)
				{
					this.Flush();
					OciLobLocator.SafeDispose(ref this._lobLocator);
					this._lobLocator = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0006BA84 File Offset: 0x0006AE84
		public long CopyTo(OracleLob destination)
		{
			return this.CopyTo(0L, destination, 0L, this.Length);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0006BAA4 File Offset: 0x0006AEA4
		public long CopyTo(OracleLob destination, long destinationOffset)
		{
			return this.CopyTo(0L, destination, destinationOffset, this.Length);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0006BAC4 File Offset: 0x0006AEC4
		public long CopyTo(long sourceOffset, OracleLob destination, long destinationOffset, long amount)
		{
			if (destination == null)
			{
				throw ADP.ArgumentNull("destination");
			}
			this.AssertObjectNotDisposed();
			destination.AssertObjectNotDisposed();
			this.AssertAmountIsValid(amount, "amount");
			this.AssertAmountIsValid(sourceOffset, "sourceOffset");
			this.AssertAmountIsValid(destinationOffset, "destinationOffset");
			if (destination.IsNull)
			{
				throw ADP.LobWriteInvalidOnNull();
			}
			if (this.IsNull)
			{
				return 0L;
			}
			this.AssertConnectionIsOpen();
			this.AssertTransactionExists();
			long num = this.AdjustOffsetToOracle(Math.Min(this.Length - sourceOffset, amount));
			long num2 = this.AdjustOffsetToOracle(destinationOffset) + 1L;
			long num3 = this.AdjustOffsetToOracle(sourceOffset) + 1L;
			if (0L >= num)
			{
				return 0L;
			}
			int num4 = TracedNativeMethods.OCILobCopy(this.ServiceContextHandle, this.ErrorHandle, destination.Descriptor, this.Descriptor, (uint)num, (uint)num2, (uint)num3);
			if (num4 != 0)
			{
				this.Connection.CheckError(this.ErrorHandle, num4);
			}
			return this.AdjustOracleToOffset(num);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0006BBB4 File Offset: 0x0006AFB4
		public void EndBatch()
		{
			this.AssertObjectNotDisposed();
			if (!this.IsNull)
			{
				this.AssertConnectionIsOpen();
				this.LobLocator.ForceClose();
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0006BBE4 File Offset: 0x0006AFE4
		public long Erase()
		{
			return this.Erase(0L, this.Length);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0006BC04 File Offset: 0x0006B004
		public long Erase(long offset, long amount)
		{
			this.AssertObjectNotDisposed();
			if (this.IsNull)
			{
				throw ADP.LobWriteInvalidOnNull();
			}
			this.AssertAmountIsValid(amount, "amount");
			this.AssertAmountIsEven(offset, "offset");
			this.AssertPositionIsValid();
			this.AssertConnectionIsOpen();
			this.AssertTransactionExists();
			if (offset < 0L || offset >= (long)((ulong)-1))
			{
				return 0L;
			}
			uint num = (uint)this.AdjustOffsetToOracle(amount);
			uint offset2 = (uint)this.AdjustOffsetToOracle(offset) + 1U;
			int num2 = TracedNativeMethods.OCILobErase(this.ServiceContextHandle, this.ErrorHandle, this.Descriptor, ref num, offset2);
			if (num2 != 0)
			{
				this.Connection.CheckError(this.ErrorHandle, num2);
			}
			return this.AdjustOracleToOffset((long)((ulong)num));
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0006BCB4 File Offset: 0x0006B0B4
		internal void Free()
		{
			int num = TracedNativeMethods.OCILobFreeTemporary(this._lobLocator.ServiceContextHandle, this._lobLocator.ErrorHandle, this._lobLocator.Descriptor);
			if (num != 0)
			{
				this._lobLocator.Connection.CheckError(this.ErrorHandle, num);
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0006BD04 File Offset: 0x0006B104
		public override void Flush()
		{
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0006BD14 File Offset: 0x0006B114
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.AssertObjectNotDisposed();
			if (count < 0)
			{
				throw ADP.MustBePositive("count");
			}
			if (offset < 0)
			{
				throw ADP.MustBePositive("offset");
			}
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			if ((long)buffer.Length < (long)offset + (long)count)
			{
				throw ADP.BufferExceeded("count");
			}
			if (this.IsNull || count == 0)
			{
				return 0;
			}
			this.AssertConnectionIsOpen();
			this.AssertAmountIsValidOddOK((long)offset, "offset");
			this.AssertAmountIsValidOddOK((long)count, "count");
			uint num = (uint)this._currentPosition;
			int num2 = 0;
			byte[] array = buffer;
			int num3 = offset;
			int num4 = count;
			if (this.IsCharacterLob)
			{
				num2 = (int)(num & 1U);
				int num5 = offset & 1;
				int num6 = count & 1;
				num /= 2U;
				if (1 == num5 || 1 == num2 || 1 == num6)
				{
					num3 = 0;
					num4 = count + num6 + 2 * num2;
					array = new byte[num4];
				}
			}
			ushort csid = this.IsCharacterLob ? 1000 : 0;
			int num7 = 0;
			int num8 = this.AdjustOffsetToOracle(num4);
			GCHandle gchandle = default(GCHandle);
			try
			{
				gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
				IntPtr bufp = new IntPtr((long)gchandle.AddrOfPinnedObject() + (long)num3);
				num7 = TracedNativeMethods.OCILobRead(this.ServiceContextHandle, this.ErrorHandle, this.Descriptor, ref num8, num + 1U, bufp, checked((uint)num4), csid, this._charsetForm);
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			if (99 == num7)
			{
				num7 = 0;
			}
			if (100 == num7)
			{
				return 0;
			}
			if (num7 != 0)
			{
				this.Connection.CheckError(this.ErrorHandle, num7);
			}
			num8 = this.AdjustOracleToOffset(num8);
			if (array != buffer)
			{
				if (num8 >= count)
				{
					num8 = count;
				}
				else
				{
					num8 -= num2;
				}
				Buffer.BlockCopy(array, num2, buffer, offset, num8);
				array = null;
			}
			this._currentPosition += (long)num8;
			return num8;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0006BEF4 File Offset: 0x0006B2F4
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.AssertObjectNotDisposed();
			if (this.IsNull)
			{
				return 0L;
			}
			long length = this.Length;
			long num;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = offset;
				break;
			case SeekOrigin.Current:
				num = this._currentPosition + offset;
				break;
			case SeekOrigin.End:
				num = length + offset;
				break;
			default:
				throw ADP.InvalidSeekOrigin(origin);
			}
			if (num < 0L || num > length)
			{
				throw ADP.SeekBeyondEnd("offset");
			}
			this._currentPosition = num;
			return this._currentPosition;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0006BF74 File Offset: 0x0006B374
		public override void SetLength(long value)
		{
			this.AssertObjectNotDisposed();
			if (this.IsNull)
			{
				throw ADP.LobWriteInvalidOnNull();
			}
			this.AssertConnectionIsOpen();
			this.AssertAmountIsValid(value, "value");
			this.AssertTransactionExists();
			uint newlen = (uint)this.AdjustOffsetToOracle(value);
			int num = TracedNativeMethods.OCILobTrim(this.ServiceContextHandle, this.ErrorHandle, this.Descriptor, newlen);
			if (num != 0)
			{
				this.Connection.CheckError(this.ErrorHandle, num);
			}
			this._currentPosition = Math.Min(this._currentPosition, value);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0006C004 File Offset: 0x0006B404
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.AssertObjectNotDisposed();
			this.AssertConnectionIsOpen();
			if (count < 0)
			{
				throw ADP.MustBePositive("count");
			}
			if (offset < 0)
			{
				throw ADP.MustBePositive("offset");
			}
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			if ((long)buffer.Length < (long)offset + (long)count)
			{
				throw ADP.BufferExceeded("count");
			}
			this.AssertTransactionExists();
			if (this.IsNull)
			{
				throw ADP.LobWriteInvalidOnNull();
			}
			this.AssertAmountIsValid((long)offset, "offset");
			this.AssertAmountIsValid((long)count, "count");
			this.AssertPositionIsValid();
			OCI.CHARSETFORM charsetForm = this._charsetForm;
			ushort csid = this.IsCharacterLob ? 1000 : 0;
			int num = this.AdjustOffsetToOracle(count);
			int num2 = 0;
			if (num == 0)
			{
				return;
			}
			GCHandle gchandle = default(GCHandle);
			try
			{
				gchandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
				IntPtr bufp = new IntPtr((long)gchandle.AddrOfPinnedObject() + (long)offset);
				num2 = TracedNativeMethods.OCILobWrite(this.ServiceContextHandle, this.ErrorHandle, this.Descriptor, ref num, this.CurrentOraclePosition, bufp, (uint)count, 0, csid, charsetForm);
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			if (num2 != 0)
			{
				this.Connection.CheckError(this.ErrorHandle, num2);
			}
			num = this.AdjustOracleToOffset(num);
			this._currentPosition += (long)num;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0006C164 File Offset: 0x0006B564
		public override void WriteByte(byte value)
		{
			if (OracleType.Clob == this._lobType || OracleType.NClob == this._lobType)
			{
				throw ADP.WriteByteForBinaryLobsOnly();
			}
			base.WriteByte(value);
		}

		// Token: 0x04000456 RID: 1110
		private const byte x_IsTemporaryUnknown = 0;

		// Token: 0x04000457 RID: 1111
		private const byte x_IsTemporary = 1;

		// Token: 0x04000458 RID: 1112
		private const byte x_IsNotTemporary = 2;

		// Token: 0x04000459 RID: 1113
		private bool _isNull;

		// Token: 0x0400045A RID: 1114
		private OciLobLocator _lobLocator;

		// Token: 0x0400045B RID: 1115
		private OracleType _lobType;

		// Token: 0x0400045C RID: 1116
		private OCI.CHARSETFORM _charsetForm;

		// Token: 0x0400045D RID: 1117
		private long _currentPosition;

		// Token: 0x0400045E RID: 1118
		private byte _isTemporaryState;

		// Token: 0x0400045F RID: 1119
		public new static readonly OracleLob Null = new OracleLob();
	}
}
