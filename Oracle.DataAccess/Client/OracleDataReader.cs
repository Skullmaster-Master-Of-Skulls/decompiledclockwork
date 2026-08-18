using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml;
using Oracle.DataAccess.Types;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200000F RID: 15
	public sealed class OracleDataReader : DbDataReader
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002940 File Offset: 0x00001940
		static OracleDataReader()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002958 File Offset: 0x00001958
		public unsafe int InitialLONGFetchSize
		{
			get
			{
				if (this.m_closed)
				{
					throw new InvalidOperationException();
				}
				return this.m_pOpoDacValCtx->InitialLongFS;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002973 File Offset: 0x00001973
		public unsafe int InitialLOBFetchSize
		{
			get
			{
				if (this.m_closed)
				{
					throw new InvalidOperationException();
				}
				return this.m_pOpoDacValCtx->InitialLobFS;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000298E File Offset: 0x0000198E
		public override int Depth
		{
			get
			{
				if (this.m_closed)
				{
					throw new InvalidOperationException();
				}
				return 0;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000029C8 File Offset: 0x000019C8
		// (set) Token: 0x06000038 RID: 56 RVA: 0x0000299F File Offset: 0x0000199F
		public long FetchSize
		{
			get
			{
				if (this.m_closed)
				{
					throw new InvalidOperationException();
				}
				return this.m_fetchSize;
			}
			set
			{
				if (this.m_closed)
				{
					throw new InvalidOperationException();
				}
				if (value <= 0L)
				{
					throw new ArgumentException();
				}
				this.m_fetchSize = value;
				this.m_bFetchSizePropertySet = true;
			}
		}

		// Token: 0x17000008 RID: 8
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000029DE File Offset: 0x000019DE
		internal long FetchSizeInRows
		{
			set
			{
				if (this.m_closed)
				{
					throw new InvalidOperationException();
				}
				if (value <= 0L)
				{
					throw new ArgumentException();
				}
				this.m_fetchSize = value * this.m_rowSize;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002A07 File Offset: 0x00001A07
		public override int FieldCount
		{
			get
			{
				if (this.m_closed)
				{
					throw new InvalidOperationException();
				}
				if (this.m_pOpoMetValCtx != null)
				{
					return this.m_fieldCount;
				}
				return 0;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002A29 File Offset: 0x00001A29
		public override bool IsClosed
		{
			get
			{
				return this.m_closed;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002A31 File Offset: 0x00001A31
		public override int RecordsAffected
		{
			get
			{
				return this.m_recordsAffected;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002A3C File Offset: 0x00001A3C
		public unsafe override bool HasRows
		{
			get
			{
				if (this.m_closed)
				{
					throw new InvalidOperationException();
				}
				if (!this.m_hasRows && !this.m_doneReadOne && this.Read() && this.m_currentClientRow == 1)
				{
					this.m_currentClientRow = 0;
					this.m_pOpoDacValCtx->CurrentClientRow = 0;
					this.m_bHasRowsCalledBeforeRead = true;
				}
				return this.m_hasRows;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002A98 File Offset: 0x00001A98
		public long RowSize
		{
			get
			{
				return this.m_rowSize;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002AA0 File Offset: 0x00001AA0
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002AB3 File Offset: 0x00001AB3
		internal IntPtr SqlCtx
		{
			get
			{
				return this.m_opsSqlCtx[0];
			}
			set
			{
				this.m_opsSqlCtx[0] = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002AC7 File Offset: 0x00001AC7
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002ACF File Offset: 0x00001ACF
		internal int FreeSqlCtx
		{
			get
			{
				return this.m_freeOpsSqlCtx;
			}
			set
			{
				this.m_freeOpsSqlCtx = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002AD8 File Offset: 0x00001AD8
		public override int VisibleFieldCount
		{
			get
			{
				if (this.m_closed)
				{
					throw new InvalidOperationException();
				}
				if (this.m_pOpoMetValCtx != null)
				{
					return this.m_fieldCount;
				}
				return 0;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002AFA File Offset: 0x00001AFA
		public unsafe int HiddenFieldCount
		{
			get
			{
				if (this.m_closed)
				{
					throw new InvalidOperationException();
				}
				if (this.m_pOpoMetValCtx != null)
				{
					return (int)this.m_pOpoMetValCtx->NoOfHiddenCols;
				}
				return 0;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002B21 File Offset: 0x00001B21
		internal int CurrentRow
		{
			get
			{
				if (this.m_closed)
				{
					throw new InvalidOperationException();
				}
				return this.m_currentClientRow;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002B37 File Offset: 0x00001B37
		internal bool IsEOF
		{
			get
			{
				if (this.m_closed)
				{
					throw new InvalidOperationException();
				}
				return this.m_bEOF;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002B4D File Offset: 0x00001B4D
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002B64 File Offset: 0x00001B64
		internal bool IsFillReader
		{
			get
			{
				if (this.m_closed)
				{
					throw new InvalidOperationException();
				}
				return this.m_fillReader;
			}
			set
			{
				if (this.m_closed)
				{
					throw new InvalidOperationException();
				}
				this.m_fillReader = value;
				if (this.m_fillReader)
				{
					this.m_dataTableList = new ArrayList();
					DataTable minSchemaTable = this.GetMinSchemaTable();
					if (minSchemaTable != null)
					{
						this.m_dataTableList.Add(minSchemaTable);
					}
				}
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002BB0 File Offset: 0x00001BB0
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002BC6 File Offset: 0x00001BC6
		internal OracleRefCursor RefCursor
		{
			get
			{
				if (this.m_closed)
				{
					throw new InvalidOperationException();
				}
				return this.m_refCursor;
			}
			set
			{
				if (this.m_closed)
				{
					throw new InvalidOperationException();
				}
				this.m_refCursor = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002BDD File Offset: 0x00001BDD
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002BE5 File Offset: 0x00001BE5
		internal Hashtable SafeMapping
		{
			get
			{
				return this.m_safeMapping;
			}
			set
			{
				this.m_safeMapping = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002BEE File Offset: 0x00001BEE
		internal ArrayList SchemaTables
		{
			get
			{
				if (this.m_closed)
				{
					throw new InvalidOperationException();
				}
				return this.m_dataTableList;
			}
		}

		// Token: 0x17000018 RID: 24
		public override object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		// Token: 0x17000019 RID: 25
		public override object this[string columnName]
		{
			get
			{
				return this[this.GetOrdinal(columnName)];
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002C1C File Offset: 0x00001C1C
		public override void Close()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::Close()\n"
				});
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
			this.m_closed = true;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::Close()\n"
				});
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002C77 File Offset: 0x00001C77
		public new void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002C88 File Offset: 0x00001C88
		public override bool GetBoolean(int i)
		{
			if (!this.m_isFromEF)
			{
				throw new NotSupportedException();
			}
			object value = this.GetValue(i);
			Type type = value.GetType();
			if (type == typeof(bool))
			{
				return (bool)value;
			}
			return !(type == typeof(DBNull)) && (decimal)value > 0m;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002CF8 File Offset: 0x00001CF8
		public unsafe override byte GetByte(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetByte()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (oraType != OraType.ORA_NUMBER)
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			this.m_pOpoDacValCtx->Type = 103;
			long num2 = this.m_fetchArrayLocation + this.m_rowLocation + (long)((ulong)this.m_colIndOffset[i]);
			short num3 = *(UIntPtr)num2;
			if (num3 == -1)
			{
				throw new InvalidCastException(OpoErrResManager.GetErrorMesg(ErrRes.DR_NULL_COL_DATA, new string[0]));
			}
			long num4 = this.m_fetchArrayLocation + this.m_rowLocation;
			IntPtr pBuffer = (IntPtr)(num4 + (long)((ulong)this.m_colOffset[i]));
			this.m_pOpoDacValCtx->pBuffer = pBuffer;
			this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
			try
			{
				num = OpsDac.GetType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx, 1);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					if (this.m_pOpoDacValCtx->Type == -1)
					{
						if (num == 22053 || num == 22054)
						{
							throw new OverflowException(OracleTypeException.GetTypeMsg(num, new object[0]));
						}
						throw new OracleTypeException(num, new object[0]);
					}
					else
					{
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetByte()\n"
				});
			}
			return *(byte*)this.m_pOpoDacValCtx->pValCtx;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F08 File Offset: 0x00001F08
		public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this.GetBytesInternal(i, fieldOffset, buffer, bufferOffset, length, true);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002F18 File Offset: 0x00001F18
		internal unsafe long GetBytesInternal(int i, long fieldOffset, byte[] buffer, int bufferOffset, int length, bool bThrowException)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetBytes()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			if (buffer != null)
			{
				this.CheckParameters(buffer.Length, bufferOffset, length);
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (this.m_external && oraType != OraType.ORA_LONGRAW && oraType != OraType.ORA_RAW && oraType != OraType.ORA_OCIBLobLocator && oraType != OraType.ORA_OCIBFileLocator)
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->RetDataLen = 0;
			if (buffer != null)
			{
				GCHandle gchandle = default(GCHandle);
				if (length == 0 || fieldOffset < 0L)
				{
					return 0L;
				}
				this.m_pOpoDacValCtx->BufLen = length;
				this.m_pOpoDacValCtx->FieldOffset = fieldOffset;
				bool flag;
				if ((oraType == OraType.ORA_LONGRAW && this.m_pOpoDacValCtx->InitialLongFS != -1 && (long)this.m_pOpoDacValCtx->BufLen + this.m_pOpoDacValCtx->FieldOffset > (long)this.m_pOpoDacValCtx->InitialLongFS) || (oraType == OraType.ORA_OCIBLobLocator && this.m_pOpoDacValCtx->InitialLobFS == 0) || (oraType == OraType.ORA_OCIBLobLocator && this.m_pOpoDacValCtx->InitialLobFS != -1 && (long)this.m_pOpoDacValCtx->BufLen + this.m_pOpoDacValCtx->FieldOffset > (long)this.m_pOpoDacValCtx->InitialLobFS) || oraType == OraType.ORA_OCIBFileLocator)
				{
					flag = false;
					gchandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
					long num2 = (long)gchandle.AddrOfPinnedObject();
					num2 += (long)bufferOffset;
					this.m_pOpoDacValCtx->pBuffer = (IntPtr)num2;
				}
				else
				{
					flag = true;
					this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
				}
				try
				{
					num = OpsDac.GetType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx, 0);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					num = ErrRes.INT_ERR;
					throw;
				}
				finally
				{
					if (num != ErrRes.INT_ERR && (oraType == OraType.ORA_LONGRAW || (oraType == OraType.ORA_OCIBLobLocator && this.m_pOpoDacValCtx->InitialLobFS > 0 && !this.m_isDBVer10gR2OrHigher)) && !this.m_pkFetched)
					{
						this.UpdateMetaDataPool();
					}
					if (!flag && gchandle.IsAllocated)
					{
						gchandle.Free();
					}
				}
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
				if (this.m_pOpoDacValCtx->Indicator == -1)
				{
					if (!bThrowException)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.Trace(1U, new string[]
							{
								" (EXIT) OracleDataReader::GetBytes()\n"
							});
						}
						return -1L;
					}
					throw new InvalidCastException(OpoErrResManager.GetErrorMesg(ErrRes.DR_NULL_COL_DATA, new string[0]));
				}
				else if (flag)
				{
					if (length < this.m_pOpoDacValCtx->RetDataLen)
					{
						this.m_pOpoDacValCtx->RetDataLen = length;
					}
					if (this.m_pOpoDacValCtx->RetDataLen > 0)
					{
						Marshal.Copy(this.m_pOpoDacValCtx->pBuffer, buffer, bufferOffset, this.m_pOpoDacValCtx->RetDataLen);
					}
				}
			}
			else
			{
				this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
				this.m_pOpoDacValCtx->BufLen = 0;
				this.m_pOpoDacValCtx->FieldOffset = 0L;
				try
				{
					num = OpsDac.GetLen(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx);
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
					throw;
				}
				if ((oraType == OraType.ORA_LONGRAW || (oraType == OraType.ORA_OCIBLobLocator && this.m_pOpoDacValCtx->InitialLobFS > 0 && !this.m_isDBVer10gR2OrHigher)) && !this.m_pkFetched)
				{
					this.UpdateMetaDataPool();
				}
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (this.m_pOpoDacValCtx->Indicator != -1)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::GetBytes()\n"
					});
				}
				return (long)this.m_pOpoDacValCtx->RetDataLen;
			}
			if (!bThrowException)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT) OracleDataReader::GetBytes()\n"
					});
				}
				return -1L;
			}
			throw new InvalidCastException(OpoErrResManager.GetErrorMesg(ErrRes.DR_NULL_COL_DATA, new string[0]));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000337C File Offset: 0x0000237C
		public override char GetChar(int i)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003384 File Offset: 0x00002384
		public unsafe override long GetChars(int i, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			bool flag = false;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetChars()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			if (buffer != null)
			{
				this.CheckParameters(buffer.Length, bufferOffset, length);
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (this.m_external && oraType != OraType.ORA_CHAR && oraType != OraType.ORA_CHARN && oraType != OraType.ORA_LONG && oraType != OraType.ORA_OCIRowid && oraType != OraType.ORA_OCICLobLocator)
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->RetDataLen = 0;
			if (buffer != null)
			{
				if (length == 0 || fieldOffset < 0L)
				{
					return 0L;
				}
				this.m_pOpoDacValCtx->BufLen = length * 2;
				this.m_pOpoDacValCtx->FieldOffset = fieldOffset * 2L;
				GCHandle gchandle = default(GCHandle);
				bool flag2;
				if ((oraType == OraType.ORA_LONG && this.m_pOpoDacValCtx->InitialLongFS != -1 && ((long)this.m_pOpoDacValCtx->BufLen + this.m_pOpoDacValCtx->FieldOffset) / 2L > (long)this.m_pOpoDacValCtx->InitialLongFS) || (oraType == OraType.ORA_OCICLobLocator && this.m_pOpoDacValCtx->InitialLobFS == 0) || (oraType == OraType.ORA_OCICLobLocator && this.m_pOpoDacValCtx->InitialLobFS != -1 && ((long)this.m_pOpoDacValCtx->BufLen + this.m_pOpoDacValCtx->FieldOffset) / 2L > (long)this.m_pOpoDacValCtx->InitialLobFS))
				{
					flag2 = false;
					gchandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
					long num2 = (long)gchandle.AddrOfPinnedObject();
					num2 += (long)(bufferOffset * 2);
					this.m_pOpoDacValCtx->pBuffer = (IntPtr)num2;
				}
				else
				{
					flag2 = true;
					this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
					if (oraType != OraType.ORA_LONG && oraType != OraType.ORA_OCICLobLocator)
					{
						long num3 = this.m_fetchArrayLocation + this.m_rowLocation + (long)((ulong)this.m_colIndOffset[i]);
						short num4 = *(UIntPtr)num3;
						if (num4 == -1)
						{
							throw new InvalidCastException(OpoErrResManager.GetErrorMesg(ErrRes.DR_NULL_COL_DATA, new string[0]));
						}
						long num5 = this.m_fetchArrayLocation + this.m_rowLocation;
						num3 = num5 + (long)((ulong)this.m_colLenOffset[i]);
						IntPtr pBuffer = (IntPtr)(num5 + (long)((ulong)this.m_colOffset[i]) + this.m_pOpoDacValCtx->FieldOffset);
						this.m_pOpoDacValCtx->pBuffer = pBuffer;
						this.m_pOpoDacValCtx->RetDataLen = (int)(*(UIntPtr)num3);
						if (this.m_pOpoDacValCtx->FieldOffset > 0L)
						{
							this.m_pOpoDacValCtx->RetDataLen -= (int)this.m_pOpoDacValCtx->FieldOffset;
						}
						flag = true;
					}
				}
				try
				{
					if (!flag)
					{
						num = OpsDac.GetType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx, 0);
					}
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					num = ErrRes.INT_ERR;
					throw;
				}
				finally
				{
					if (num != ErrRes.INT_ERR && (oraType == OraType.ORA_LONG || (oraType == OraType.ORA_OCICLobLocator && this.m_pOpoDacValCtx->InitialLobFS > 0 && !this.m_isDBVer10gR2OrHigher)) && !this.m_pkFetched)
					{
						this.UpdateMetaDataPool();
					}
					if (!flag2 && gchandle.IsAllocated)
					{
						gchandle.Free();
					}
				}
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
				if (this.m_pOpoDacValCtx->Indicator == -1)
				{
					throw new InvalidCastException(OpoErrResManager.GetErrorMesg(ErrRes.DR_NULL_COL_DATA, new string[0]));
				}
				this.m_pOpoDacValCtx->RetDataLen /= 2;
				if (flag2)
				{
					bool flag3 = true;
					if (this.m_pOpoMetValCtx->pColMetaVal[i].UCS2Character == 0)
					{
						flag3 = false;
					}
					if (length < this.m_pOpoDacValCtx->RetDataLen)
					{
						this.m_pOpoDacValCtx->RetDataLen = length;
					}
					if (this.m_pOpoDacValCtx->RetDataLen > 0)
					{
						if (flag3)
						{
							Marshal.Copy(this.m_pOpoDacValCtx->pBuffer, buffer, bufferOffset, this.m_pOpoDacValCtx->RetDataLen);
						}
						else
						{
							string value = OracleString.GetValue(this.m_pOpoDacValCtx->pBuffer, this.m_pOpoDacValCtx->RetDataLen, flag3);
							value.CopyTo(0, buffer, bufferOffset, this.m_pOpoDacValCtx->RetDataLen);
						}
					}
				}
			}
			else
			{
				this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
				this.m_pOpoDacValCtx->BufLen = 0;
				this.m_pOpoDacValCtx->FieldOffset = fieldOffset;
				try
				{
					num = OpsDac.GetLen(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx);
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
					throw;
				}
				if ((oraType == OraType.ORA_LONG || (oraType == OraType.ORA_OCICLobLocator && this.m_pOpoDacValCtx->InitialLobFS > 0 && !this.m_isDBVer10gR2OrHigher)) && !this.m_pkFetched)
				{
					this.UpdateMetaDataPool();
				}
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
				if (this.m_pOpoDacValCtx->Indicator == -1)
				{
					throw new InvalidCastException(OpoErrResManager.GetErrorMesg(ErrRes.DR_NULL_COL_DATA, new string[0]));
				}
				this.m_pOpoDacValCtx->RetDataLen /= 2;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetChars()\n"
				});
			}
			return (long)this.m_pOpoDacValCtx->RetDataLen;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003900 File Offset: 0x00002900
		public new DbDataReader GetData(int i)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003908 File Offset: 0x00002908
		public override string GetDataTypeName(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetDataTypeName()\n"
				});
			}
			if (this.m_closed)
			{
				throw new InvalidOperationException();
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetDataTypeName()\n"
				});
			}
			return this.m_oracleDbType[i].ToString();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003970 File Offset: 0x00002970
		public unsafe override DateTime GetDateTime(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetDateTime()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (this.m_external && oraType != OraType.ORA_TIMESTAMP && oraType != OraType.ORA_TIMESTAMP_LTZ && oraType != OraType.ORA_TIMESTAMP_TZ && oraType != OraType.ORA_DATE)
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			long num2 = this.m_fetchArrayLocation + this.m_rowLocation + (long)((ulong)this.m_colIndOffset[i]);
			short num3 = *(UIntPtr)num2;
			if (num3 == -1)
			{
				throw new InvalidCastException(OpoErrResManager.GetErrorMesg(ErrRes.DR_NULL_COL_DATA, new string[0]));
			}
			long num4 = this.m_fetchArrayLocation + this.m_rowLocation;
			IntPtr pBuffer = (IntPtr)(num4 + (long)((ulong)this.m_colOffset[i]));
			this.m_pOpoDacValCtx->pBuffer = pBuffer;
			this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
			try
			{
				num = OpsDac.GetType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx, 1);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			OraType oraType2 = oraType;
			DateTime result;
			if (oraType2 != OraType.ORA_DATE)
			{
				switch (oraType2)
				{
				case OraType.ORA_TIMESTAMP:
					result = DateTimeConv.GetDateTime((OpoTSValCtx*)this.m_pOpoDacValCtx->pValCtx, OracleDbType.TimeStamp, false);
					break;
				case OraType.ORA_TIMESTAMP_TZ:
					result = DateTimeConv.GetDateTime((OpoTSValCtx*)this.m_pOpoDacValCtx->pValCtx, OracleDbType.TimeStampTZ, false);
					break;
				default:
					if (oraType2 != OraType.ORA_TIMESTAMP_LTZ)
					{
						result = default(DateTime);
					}
					else
					{
						result = DateTimeConv.GetDateTime((OpoTSValCtx*)this.m_pOpoDacValCtx->pValCtx, OracleDbType.TimeStampLTZ, false);
					}
					break;
				}
			}
			else
			{
				byte* ptr = (byte*)((void*)this.m_pOpoDacValCtx->pBuffer);
				int year = (int)((*ptr - 100) * 100 + (ptr[1] - 100));
				result = new DateTime(year, (int)ptr[2], (int)ptr[3], (int)(ptr[4] - 1), (int)(ptr[5] - 1), (int)(ptr[6] - 1));
				this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetDateTime()\n"
				});
			}
			return result;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003C30 File Offset: 0x00002C30
		public unsafe override decimal GetDecimal(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetDecimal()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			OraType oraType = this.m_oraType[i];
			if (this.m_isFromEF)
			{
				if (oraType != OraType.ORA_NUMBER && oraType != OraType.ORA_INTERVAL_YM && oraType != OraType.ORA_INTERVAL_DS)
				{
					throw new InvalidCastException();
				}
				if (oraType == OraType.ORA_INTERVAL_DS)
				{
					return (decimal)this.GetTimeSpan(i).TotalSeconds;
				}
				if (oraType == OraType.ORA_INTERVAL_YM)
				{
					object value = this.GetValue(i);
					return (decimal)value;
				}
			}
			else if (oraType != OraType.ORA_NUMBER)
			{
				throw new InvalidCastException();
			}
			long num = this.m_fetchArrayLocation + this.m_rowLocation + (long)((ulong)this.m_colIndOffset[i]);
			short num2 = *(UIntPtr)num;
			if (num2 == -1)
			{
				throw new InvalidCastException(OpoErrResManager.GetErrorMesg(ErrRes.DR_NULL_COL_DATA, new string[0]));
			}
			long num3 = this.m_fetchArrayLocation + this.m_rowLocation;
			IntPtr numCtx = (IntPtr)(num3 + (long)((ulong)this.m_colOffset[i]));
			return DecimalConv.GetDecimal(numCtx);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003D74 File Offset: 0x00002D74
		public unsafe override double GetDouble(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetDouble()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (oraType != OraType.ORA_NUMBER && oraType != OraType.ORA_IBDOUBLE)
			{
				throw new InvalidCastException();
			}
			int precision = (int)this.m_pOpoMetValCtx->pColMetaVal[i].Precision;
			if (!this.m_isFromEF && precision >= 16 && oraType != OraType.ORA_IBDOUBLE)
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			this.m_pOpoDacValCtx->Type = 108;
			long num2 = this.m_fetchArrayLocation + this.m_rowLocation + (long)((ulong)this.m_colIndOffset[i]);
			short num3 = *(UIntPtr)num2;
			if (num3 == -1)
			{
				throw new InvalidCastException(OpoErrResManager.GetErrorMesg(ErrRes.DR_NULL_COL_DATA, new string[0]));
			}
			long num4 = this.m_fetchArrayLocation + this.m_rowLocation;
			IntPtr pBuffer = (IntPtr)(num4 + (long)((ulong)this.m_colOffset[i]));
			this.m_pOpoDacValCtx->pBuffer = pBuffer;
			try
			{
				num = OpsDac.GetType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx, 1);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					if (this.m_pOpoDacValCtx->Type == -1)
					{
						if (num == 22053 || num == 22054)
						{
							throw new OverflowException(OracleTypeException.GetTypeMsg(num, new object[0]));
						}
						throw new OracleTypeException(num, new object[0]);
					}
					else
					{
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetDouble()\n"
				});
			}
			return *(double*)this.m_pOpoDacValCtx->pValCtx;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003FAC File Offset: 0x00002FAC
		public unsafe override Type GetFieldType(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetFieldType()\n"
				});
			}
			if (this.m_closed)
			{
				throw new InvalidOperationException();
			}
			if (i >= this.m_fieldCount || i < 0)
			{
				throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
			}
			OraType oraType = this.m_oraType[i];
			if (oraType == OraType.ORA_NDT && this.m_pOpoMetValCtx->pColMetaVal[i].bIsXmlType == 0)
			{
				OracleUdtDescriptor cachedOracleUdtDescriptor = this.GetCachedOracleUdtDescriptor(i);
				if (cachedOracleUdtDescriptor.m_bSetOracleDbType)
				{
					OracleDbType oraDbType = cachedOracleUdtDescriptor.m_oraDbType;
				}
				else
				{
					OracleDbType oracleDbType = cachedOracleUdtDescriptor.OracleDbType;
				}
				if (cachedOracleUdtDescriptor.OracleDbType != OracleDbType.XmlType)
				{
					if (OracleConnection.s_bIsOdtConnection)
					{
						return typeof(object);
					}
					object factory = OracleUdt.GetFactory(cachedOracleUdtDescriptor);
					if (factory is IOracleCustomTypeFactory)
					{
						return ((IOracleCustomTypeFactory)factory).CreateObject().GetType();
					}
					if (factory is IOracleArrayTypeFactory)
					{
						return ((IOracleArrayTypeFactory)factory).CreateArray(0).GetType();
					}
					return factory.GetType();
				}
			}
			Type type = (Type)OracleTypeMapper.m_OraToNET[oraType];
			if (this.m_safeMapping != null && this.m_safeMapping.Count > 0 && this.IsCorruptible(oraType))
			{
				Type type2 = (Type)this.m_safeMapping[this.m_colMetaRef[i].pColAlias];
				if (type2 == null)
				{
					type2 = (Type)this.m_safeMapping["*"];
				}
				if (type2 != null)
				{
					type = type2;
				}
			}
			if (type == typeof(decimal))
			{
				int scale = (int)this.m_pOpoMetValCtx->pColMetaVal[i].Scale;
				int precision = (int)this.m_pOpoMetValCtx->pColMetaVal[i].Precision;
				if (scale <= 0 && precision - scale < 5)
				{
					type = typeof(short);
				}
				else if (scale <= 0 && precision - scale < 10)
				{
					type = typeof(int);
				}
				else if (scale <= 0 && precision - scale < 19)
				{
					type = typeof(long);
				}
				else if (precision < 8 && ((scale <= 0 && precision - scale <= 38) || (scale > 0 && scale <= 44)))
				{
					type = typeof(float);
				}
				else if (precision < 16)
				{
					type = typeof(double);
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetFieldType()\n"
				});
			}
			return type;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004230 File Offset: 0x00003230
		public override Guid GetGuid(int i)
		{
			if (this.m_isFromEF)
			{
				object value = this.GetValue(i);
				return (Guid)value;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000425C File Offset: 0x0000325C
		public unsafe override short GetInt16(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetInt16()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (oraType != OraType.ORA_NUMBER)
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			this.m_pOpoDacValCtx->Type = 111;
			long num2 = this.m_fetchArrayLocation + this.m_rowLocation + (long)((ulong)this.m_colIndOffset[i]);
			short num3 = *(UIntPtr)num2;
			if (num3 == -1)
			{
				throw new InvalidCastException(OpoErrResManager.GetErrorMesg(ErrRes.DR_NULL_COL_DATA, new string[0]));
			}
			long num4 = this.m_fetchArrayLocation + this.m_rowLocation;
			IntPtr pBuffer = (IntPtr)(num4 + (long)((ulong)this.m_colOffset[i]));
			this.m_pOpoDacValCtx->pBuffer = pBuffer;
			try
			{
				num = OpsDac.GetType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx, 1);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					if (this.m_pOpoDacValCtx->Type == -1)
					{
						if (num == 22053 || num == 22054)
						{
							throw new OverflowException(OracleTypeException.GetTypeMsg(num, new object[0]));
						}
						throw new OracleTypeException(num, new object[0]);
					}
					else
					{
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetInt16()\n"
				});
			}
			return *(short*)this.m_pOpoDacValCtx->pValCtx;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000445C File Offset: 0x0000345C
		public unsafe override int GetInt32(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetInt32()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (oraType != OraType.ORA_NUMBER)
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			this.m_pOpoDacValCtx->Type = 112;
			long num2 = this.m_fetchArrayLocation + this.m_rowLocation + (long)this.m_colIndOffset[i];
			short num3 = *(UIntPtr)num2;
			if (num3 == -1)
			{
				throw new InvalidCastException(OpoErrResManager.GetErrorMesg(ErrRes.DR_NULL_COL_DATA, new string[0]));
			}
			long num4 = this.m_fetchArrayLocation + this.m_rowLocation;
			IntPtr pBuffer = (IntPtr)(num4 + (long)((ulong)this.m_colOffset[i]));
			this.m_pOpoDacValCtx->pBuffer = pBuffer;
			try
			{
				num = OpsDac.GetType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx, 1);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					if (this.m_pOpoDacValCtx->Type == -1)
					{
						if (num == 22053 || num == 22054)
						{
							throw new OverflowException(OracleTypeException.GetTypeMsg(num, new object[0]));
						}
						throw new OracleTypeException(num, new object[0]);
					}
					else
					{
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetInt32()\n"
				});
			}
			return *(int*)this.m_pOpoDacValCtx->pValCtx;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000465C File Offset: 0x0000365C
		public unsafe override long GetInt64(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetInt64()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (oraType != OraType.ORA_NUMBER && oraType != OraType.ORA_INTERVAL_YM)
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			this.m_pOpoDacValCtx->Type = 113;
			long num2 = this.m_fetchArrayLocation + this.m_rowLocation + (long)((ulong)this.m_colIndOffset[i]);
			short num3 = *(UIntPtr)num2;
			if (num3 == -1)
			{
				throw new InvalidCastException(OpoErrResManager.GetErrorMesg(ErrRes.DR_NULL_COL_DATA, new string[0]));
			}
			long num4 = this.m_fetchArrayLocation + this.m_rowLocation;
			IntPtr pBuffer = (IntPtr)(num4 + (long)((ulong)this.m_colOffset[i]));
			this.m_pOpoDacValCtx->pBuffer = pBuffer;
			try
			{
				num = OpsDac.GetType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx, 1);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					if (this.m_pOpoDacValCtx->Type == -1)
					{
						if (num == 22053 || num == 22054)
						{
							throw new OverflowException(OracleTypeException.GetTypeMsg(num, new object[0]));
						}
						throw new OracleTypeException(num, new object[0]);
					}
					else
					{
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
				}
			}
			if (oraType == OraType.ORA_INTERVAL_YM)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::GetInt64()\n"
					});
				}
				return LongConv.GetLong((OpoITLValCtx*)this.m_pOpoDacValCtx->pValCtx, OracleDbType.IntervalYM);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetInt64()\n"
				});
			}
			return *(long*)this.m_pOpoDacValCtx->pValCtx;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000048A0 File Offset: 0x000038A0
		public unsafe override float GetFloat(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetFloat()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (oraType != OraType.ORA_NUMBER && oraType != OraType.ORA_IBFLOAT)
			{
				throw new InvalidCastException();
			}
			int scale = (int)this.m_pOpoMetValCtx->pColMetaVal[i].Scale;
			int precision = (int)this.m_pOpoMetValCtx->pColMetaVal[i].Precision;
			if (!this.m_isFromEF && (precision >= 8 || ((scale > 0 || precision - scale > 38) && (scale <= 0 || scale > 44))) && oraType != OraType.ORA_IBFLOAT)
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			this.m_pOpoDacValCtx->Type = 108;
			long num2 = this.m_fetchArrayLocation + this.m_rowLocation + (long)((ulong)this.m_colIndOffset[i]);
			short num3 = *(UIntPtr)num2;
			if (num3 == -1)
			{
				throw new InvalidCastException(OpoErrResManager.GetErrorMesg(ErrRes.DR_NULL_COL_DATA, new string[0]));
			}
			long num4 = this.m_fetchArrayLocation + this.m_rowLocation;
			IntPtr pBuffer = (IntPtr)(num4 + (long)((ulong)this.m_colOffset[i]));
			this.m_pOpoDacValCtx->pBuffer = pBuffer;
			try
			{
				num = OpsDac.GetType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx, 1);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					if (this.m_pOpoDacValCtx->Type == -1)
					{
						if (num == 22053 || num == 22054)
						{
							throw new OverflowException(OracleTypeException.GetTypeMsg(num, new object[0]));
						}
						throw new OracleTypeException(num, new object[0]);
					}
					else
					{
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetFloat()\n"
				});
			}
			if (oraType != OraType.ORA_IBFLOAT)
			{
				return (float)(*(double*)this.m_pOpoDacValCtx->pValCtx);
			}
			return *(float*)this.m_pOpoDacValCtx->pValCtx;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004B1C File Offset: 0x00003B1C
		public override string GetName(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetName()\n"
				});
			}
			if (this.m_closed)
			{
				throw new InvalidOperationException();
			}
			if (i >= this.m_fieldCount || i < 0)
			{
				throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
			}
			if (this.m_pOpoMetValCtx != null && this.m_colMetaRef == null)
			{
				this.GetColMetaRef(false, false);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetName()\n"
				});
			}
			return this.m_colMetaRef[i].pColAlias;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004BBC File Offset: 0x00003BBC
		public OracleBinary GetOracleBinary(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetOracleBinary()\n"
				});
			}
			byte[] array = null;
			long num;
			if ((num = this.GetBytesInternal(i, 0L, array, 0, 0, false)) < 0L)
			{
				return OracleBinary.Null;
			}
			array = new byte[num];
			int num2 = 0;
			while (num > 2147483647L)
			{
				if (this.GetBytesInternal(i, (long)num2, array, num2, 2147483647, false) < 0L)
				{
					return OracleBinary.Null;
				}
				num2 += int.MaxValue;
				num -= 2147483647L;
			}
			if (this.GetBytesInternal(i, (long)num2, array, num2, (int)num, false) < 0L)
			{
				return OracleBinary.Null;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetOracleBinary()\n"
				});
			}
			return new OracleBinary(array);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004C84 File Offset: 0x00003C84
		public unsafe OracleBlob GetOracleBlob(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetOracleBlob()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (oraType != OraType.ORA_OCIBLobLocator || this.m_pOpoDacValCtx->InitialLobFS == -1 || (this.m_pOpoDacValCtx->InitialLobFS > 0 && !this.m_isDBVer10gR2OrHigher))
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			try
			{
				num = OpsDac.GetOraType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (this.m_pOpoDacValCtx->Indicator == -1)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::GetOracleBlob()\n"
					});
				}
				return OracleBlob.Null;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetOracleBlob()\n"
				});
			}
			return new OracleBlob(this.m_connection, this.m_pOpoDacValCtx->pBuffer, false, false);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004E44 File Offset: 0x00003E44
		public unsafe OracleBlob GetOracleBlobForUpdate(int i, int wait)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetOracleBlobForUpdate()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (oraType != OraType.ORA_OCIBLobLocator || this.m_pOpoDacValCtx->InitialLobFS == -1 || (this.m_pOpoDacValCtx->InitialLobFS > 0 && !this.m_isDBVer10gR2OrHigher))
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			this.m_pOpoDacValCtx->ForUpdate = 1;
			this.m_pOpoDacValCtx->Wait = wait;
			try
			{
				num = OpsDac.GetOraType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (!this.m_pkFetched)
			{
				this.UpdateMetaDataPool();
			}
			if (num != 0)
			{
				if (num == ErrRes.DAC_PK_REQUIRED)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.DAC_PK_REQUIRED, new string[0]));
				}
				OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
			}
			if (this.m_pOpoDacValCtx->Indicator == -1)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::GetOracleBlobForUpdate()\n"
					});
				}
				return OracleBlob.Null;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetOracleBlobForUpdate()\n"
				});
			}
			return new OracleBlob(this.m_connection, this.m_pOpoDacValCtx->pBuffer, false, false);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00005038 File Offset: 0x00004038
		public OracleBlob GetOracleBlobForUpdate(int i)
		{
			return this.GetOracleBlobForUpdate(i, -1);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00005044 File Offset: 0x00004044
		public unsafe OracleBFile GetOracleBFile(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetOracleBFile()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (oraType != OraType.ORA_OCIBFileLocator)
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			try
			{
				num = OpsDac.GetOraType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (this.m_pOpoDacValCtx->Indicator == -1)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::GetOracleBFile()\n"
					});
				}
				return OracleBFile.Null;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetOracleBFile()\n"
				});
			}
			return new OracleBFile(this.m_connection, this.m_pOpoDacValCtx->pBuffer);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000051DC File Offset: 0x000041DC
		public unsafe OracleClob GetOracleClob(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetOracleClob()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (oraType != OraType.ORA_OCICLobLocator || this.m_pOpoDacValCtx->InitialLobFS == -1 || (this.m_pOpoDacValCtx->InitialLobFS > 0 && !this.m_isDBVer10gR2OrHigher))
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			try
			{
				num = OpsDac.GetOraType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (this.m_pOpoDacValCtx->Indicator == -1)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::GetOracleClob()\n"
					});
				}
				return OracleClob.Null;
			}
			bool bNClob = true;
			if (this.m_pOpoMetValCtx->pColMetaVal[i].UCS2Character == 0)
			{
				bNClob = false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetOracleClob()\n"
				});
			}
			return new OracleClob(this.m_connection, this.m_pOpoDacValCtx->pBuffer, false, bNClob, false);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000053C0 File Offset: 0x000043C0
		public unsafe OracleClob GetOracleClobForUpdate(int i, int wait)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetOracleClobForUpdate()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (oraType != OraType.ORA_OCICLobLocator || this.m_pOpoDacValCtx->InitialLobFS == -1 || (this.m_pOpoDacValCtx->InitialLobFS > 0 && !this.m_isDBVer10gR2OrHigher))
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			this.m_pOpoDacValCtx->ForUpdate = 1;
			this.m_pOpoDacValCtx->Wait = wait;
			try
			{
				num = OpsDac.GetOraType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (!this.m_pkFetched)
			{
				this.UpdateMetaDataPool();
			}
			if (num != 0)
			{
				if (num == ErrRes.DAC_PK_REQUIRED)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.DAC_PK_REQUIRED, new string[0]));
				}
				OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
			}
			if (this.m_pOpoDacValCtx->Indicator == -1)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::GetOracleClobForUpdate()\n"
					});
				}
				return OracleClob.Null;
			}
			bool bNClob = true;
			if (this.m_pOpoMetValCtx->pColMetaVal[i].UCS2Character == 0)
			{
				bNClob = false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetOracleClobForUpdate()\n"
				});
			}
			return new OracleClob(this.m_connection, this.m_pOpoDacValCtx->pBuffer, false, bNClob, false);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000055D8 File Offset: 0x000045D8
		public OracleClob GetOracleClobForUpdate(int i)
		{
			return this.GetOracleClobForUpdate(i, -1);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000055E4 File Offset: 0x000045E4
		public unsafe OracleDate GetOracleDate(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetOracleDate()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (oraType != OraType.ORA_DATE)
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			try
			{
				num = OpsDac.GetOraType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (this.m_pOpoDacValCtx->Indicator == -1)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::GetOracleDate()\n"
					});
				}
				return OracleDate.Null;
			}
			OracleDate result = new OracleDate((OpoDatValCtx*)this.m_pOpoDacValCtx->pValCtx);
			this.m_pOpoDacValCtx->pValCtx = null;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetOracleDate()\n"
				});
			}
			return result;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000578C File Offset: 0x0000478C
		public unsafe OracleDecimal GetOracleDecimal(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetOracleDecimal()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (oraType != OraType.ORA_NUMBER && oraType != OraType.ORA_IBFLOAT && oraType != OraType.ORA_IBDOUBLE)
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			try
			{
				num = OpsDac.GetOraType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (this.m_pOpoDacValCtx->Indicator == -1)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::GetOracleDecimal()\n"
					});
				}
				return OracleDecimal.Null;
			}
			OracleDecimal result;
			if (OraType.ORA_NUMBER == oraType)
			{
				result = new OracleDecimal((IntPtr)this.m_pOpoDacValCtx->pValCtx, false);
			}
			else if (OraType.ORA_IBFLOAT == oraType)
			{
				result = new OracleDecimal(*(float*)this.m_pOpoDacValCtx->pValCtx);
			}
			else
			{
				result = new OracleDecimal(*(double*)this.m_pOpoDacValCtx->pValCtx);
			}
			this.m_pOpoDacValCtx->pValCtx = null;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetOracleDecimal()\n"
				});
			}
			return result;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00005974 File Offset: 0x00004974
		public unsafe OracleIntervalDS GetOracleIntervalDS(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetOracleIntervalDS()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (oraType != OraType.ORA_INTERVAL_DS)
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			try
			{
				num = OpsDac.GetOraType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (this.m_pOpoDacValCtx->Indicator == -1)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::GetOracleIntervalDS()\n"
					});
				}
				return OracleIntervalDS.Null;
			}
			OracleIntervalDS result = new OracleIntervalDS((OpoITLValCtx*)this.m_pOpoDacValCtx->pValCtx, (int)this.m_pOpoMetValCtx->pColMetaVal[i].Precision, (int)this.m_pOpoMetValCtx->pColMetaVal[i].Scale);
			this.m_pOpoDacValCtx->pValCtx = null;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetOracleIntervalDS()\n"
				});
			}
			return result;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00005B50 File Offset: 0x00004B50
		public unsafe OracleIntervalYM GetOracleIntervalYM(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetOracleIntervalYM()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (oraType != OraType.ORA_INTERVAL_YM)
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			try
			{
				num = OpsDac.GetOraType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (this.m_pOpoDacValCtx->Indicator == -1)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::GetOracleIntervalYM()\n"
					});
				}
				return OracleIntervalYM.Null;
			}
			OracleIntervalYM result = new OracleIntervalYM((OpoITLValCtx*)this.m_pOpoDacValCtx->pValCtx, (int)this.m_pOpoMetValCtx->pColMetaVal[i].Precision);
			this.m_pOpoDacValCtx->pValCtx = null;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetOracleIntervalYM()\n"
				});
			}
			return result;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005D14 File Offset: 0x00004D14
		public unsafe OracleRef GetOracleRef(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetOracleRef()\n"
				});
			}
			if (this.m_closed || this.m_bBOF || this.m_bEOF)
			{
				throw new InvalidOperationException();
			}
			if (i >= this.m_fieldCount || i < 0)
			{
				throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (oraType != OraType.ORA_OCIRef)
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			OracleUdtDescriptor cachedOracleUdtDescriptor = this.GetCachedOracleUdtDescriptor(i);
			try
			{
				num = OpsDac.GetOraType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (this.m_pOpoDacValCtx->Indicator == -1)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::GetOracleRef()\n"
					});
				}
				return OracleRef.Null;
			}
			if (!this.m_fillReader && this.m_pOpoDacValCtx->Indicator == 0 && this.m_pOpoDacValCtx->pBuffer == IntPtr.Zero)
			{
				OracleRef oracleRef = (OracleRef)this.m_currentRowUdtCache[i];
				oracleRef = (OracleRef)oracleRef.Clone();
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::GetOracleRef()\n"
					});
				}
				return oracleRef;
			}
			OpoUdtCtx opoUdtCtx = new OpoUdtCtx(this.m_opsConCtx, IntPtr.Zero, this.m_pOpoDacValCtx->pBuffer, this.m_pOpoDacValCtx->pUdtNullStruct);
			OracleRef oracleRef2;
			if (this.m_pOpoMetValCtx->pColMetaVal[i].bIsFinalType == 0)
			{
				oracleRef2 = new OracleRef(this.m_connection, opoUdtCtx);
			}
			else
			{
				oracleRef2 = new OracleRef(cachedOracleUdtDescriptor, opoUdtCtx);
			}
			if (!this.m_fillReader)
			{
				this.m_currentRowUdtCache[i] = oracleRef2;
			}
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetOracleRef()\n"
				});
			}
			return oracleRef2;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005F88 File Offset: 0x00004F88
		public unsafe OracleString GetOracleString(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetOracleString()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (this.m_external && oraType != OraType.ORA_CHAR && oraType != OraType.ORA_CHARN && oraType != OraType.ORA_LONG && oraType != OraType.ORA_OCIRowid && oraType != OraType.ORA_OCICLobLocator && oraType != OraType.ORA_NDT && oraType != OraType.ORA_OCIRef)
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
			if (oraType == OraType.ORA_NDT || oraType == OraType.ORA_OCIRef)
			{
				if (this.m_pOpoMetValCtx->pColMetaVal[i].bIsXmlType == 1)
				{
					if (this.m_opsXmlTypeCtx == null)
					{
						this.m_opsXmlTypeCtx = new IntPtr[this.m_fieldCount];
					}
					else if (this.m_opsXmlTypeCtx[i] != IntPtr.Zero)
					{
						IntPtr zero = IntPtr.Zero;
						int count = 0;
						OracleString result = null;
						try
						{
							num = OpsXmlStream.GetValueBuffer(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsXmlTypeCtx[i], ref zero, ref count);
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
							throw;
						}
						if (num == 0)
						{
							result = new OracleString(zero, count, true);
							try
							{
								num = OpsXmlStream.FreeValueBuffer(ref zero);
							}
							catch (Exception ex2)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex2);
								}
							}
						}
						if (num != 0)
						{
							OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
						}
						OraTrace.Trace(1U, new string[]
						{
							" (EXIT)  OracleDataReader::GetOracleString()\n"
						});
						return result;
					}
				}
				else
				{
					if (this.IsDBNull(i))
					{
						return OracleString.Null;
					}
					return new OracleString(this.GetString(i));
				}
			}
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			string text = null;
			GCHandle gchandle = default(GCHandle);
			bool flag;
			if (oraType != OraType.ORA_LONG || this.InitialLONGFetchSize == -1)
			{
				if (oraType != OraType.ORA_OCICLobLocator || this.InitialLOBFetchSize == -1)
				{
					flag = true;
					this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
					this.m_pOpoDacValCtx->BufLen = 0;
					goto IL_3FB;
				}
			}
			try
			{
				num = OpsDac.GetLen(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx);
			}
			catch (Exception ex3)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex3);
				}
				throw;
			}
			if ((oraType == OraType.ORA_LONG || (oraType == OraType.ORA_OCICLobLocator && this.m_pOpoDacValCtx->InitialLobFS > 0 && !this.m_isDBVer10gR2OrHigher)) && !this.m_pkFetched)
			{
				this.UpdateMetaDataPool();
			}
			if (num != 0)
			{
				OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
			}
			if (this.m_pOpoDacValCtx->Indicator == -1)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::GetOralceString()\n"
					});
				}
				return OracleString.Null;
			}
			if (((oraType != OraType.ORA_LONG || this.m_pOpoDacValCtx->RetDataLen / 2 > this.m_pOpoDacValCtx->InitialLongFS) && (oraType != OraType.ORA_OCICLobLocator || this.m_pOpoDacValCtx->InitialLobFS <= 0 || this.m_pOpoDacValCtx->RetDataLen / 2 > this.m_pOpoDacValCtx->InitialLobFS)) || (oraType == OraType.ORA_OCICLobLocator && this.m_pOpoDacValCtx->InitialLobFS > 0 && this.m_pOpoDacValCtx->RetDataLen / 2 > this.m_pOpoDacValCtx->InitialLobFS && this.m_isDBVer10gR2OrHigher))
			{
				flag = false;
				text = new string('\0', this.m_pOpoDacValCtx->RetDataLen / 2);
				gchandle = GCHandle.Alloc(text, GCHandleType.Pinned);
				this.m_pOpoDacValCtx->pBuffer = gchandle.AddrOfPinnedObject();
				this.m_pOpoDacValCtx->BufLen = this.m_pOpoDacValCtx->RetDataLen;
			}
			else
			{
				flag = true;
				this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
				this.m_pOpoDacValCtx->BufLen = this.m_pOpoDacValCtx->RetDataLen;
			}
			IL_3FB:
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			try
			{
				num = OpsDac.GetType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx, 0);
			}
			catch (Exception ex4)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex4);
				}
				num = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num != ErrRes.INT_ERR && (oraType == OraType.ORA_LONG || (oraType == OraType.ORA_OCICLobLocator && this.m_pOpoDacValCtx->InitialLobFS > 0 && !this.m_isDBVer10gR2OrHigher)) && !this.m_pkFetched)
				{
					this.UpdateMetaDataPool();
				}
				if (!flag && gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			if (num != 0)
			{
				OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
			}
			if (this.m_pOpoDacValCtx->Indicator == -1)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::GetOralceString()\n"
					});
				}
				return OracleString.Null;
			}
			if (oraType == OraType.ORA_NDT)
			{
				if (this.m_pOpoMetValCtx->pColMetaVal[i].bIsXmlType == 1)
				{
					IntPtr intPtr = (IntPtr)this.m_pOpoDacValCtx->pValCtx;
					IntPtr zero2 = IntPtr.Zero;
					int count2 = 0;
					OracleString result2 = null;
					this.m_opsXmlTypeCtx[i] = intPtr;
					this.m_pOpoDacValCtx->pValCtx = null;
					try
					{
						num = OpsXmlStream.GetValueBuffer(this.m_opsConCtx, this.m_opsErrCtx, intPtr, ref zero2, ref count2);
					}
					catch (Exception ex5)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex5);
						}
						throw;
					}
					if (num == 0)
					{
						result2 = new OracleString(zero2, count2, true);
						try
						{
							num = OpsXmlStream.FreeValueBuffer(ref zero2);
						}
						catch (Exception ex6)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex6);
							}
						}
					}
					if (num != 0)
					{
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::GetOracleString()\n"
					});
					return result2;
				}
				return new OracleString(this.GetString(i));
			}
			else
			{
				this.m_pOpoDacValCtx->RetDataLen /= 2;
				if (flag)
				{
					bool isUnicode = true;
					if (this.m_pOpoMetValCtx->pColMetaVal[i].UCS2Character == 0)
					{
						isUnicode = false;
					}
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(1U, new string[]
						{
							" (EXIT)  OracleDataReader::GetString()\n"
						});
					}
					return new OracleString(this.m_pOpoDacValCtx->pBuffer, this.m_pOpoDacValCtx->RetDataLen, isUnicode);
				}
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::GetOralceString()\n"
					});
				}
				return new OracleString(text);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000666C File Offset: 0x0000566C
		public unsafe OracleTimeStamp GetOracleTimeStamp(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetOracleTimeStamp()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (oraType != OraType.ORA_TIMESTAMP)
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			try
			{
				num = OpsDac.GetOraType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (this.m_pOpoDacValCtx->Indicator == -1)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::GetOracleTimeStamp()\n"
					});
				}
				return OracleTimeStamp.Null;
			}
			OracleTimeStamp result = new OracleTimeStamp((OpoTSValCtx*)this.m_pOpoDacValCtx->pValCtx, (int)this.m_pOpoMetValCtx->pColMetaVal[i].Scale);
			this.m_pOpoDacValCtx->pValCtx = null;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetOracleTimeStamp()\n"
				});
			}
			return result;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00006830 File Offset: 0x00005830
		public unsafe OracleTimeStampLTZ GetOracleTimeStampLTZ(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetOracleTimeStampLTZ()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (oraType != OraType.ORA_TIMESTAMP_LTZ)
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			try
			{
				num = OpsDac.GetOraType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (this.m_pOpoDacValCtx->Indicator == -1)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::GetOracleTimeStampLTZ()\n"
					});
				}
				return OracleTimeStampLTZ.Null;
			}
			OracleTimeStampLTZ result = new OracleTimeStampLTZ((OpoTSValCtx*)this.m_pOpoDacValCtx->pValCtx, (int)this.m_pOpoMetValCtx->pColMetaVal[i].Scale);
			this.m_pOpoDacValCtx->pValCtx = null;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetOracleTimeStampLTZ()\n"
				});
			}
			return result;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000069F4 File Offset: 0x000059F4
		public unsafe OracleTimeStampTZ GetOracleTimeStampTZ(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetOracleTimeStampTZ()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (oraType != OraType.ORA_TIMESTAMP_TZ)
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			try
			{
				num = OpsDac.GetOraType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (this.m_pOpoDacValCtx->Indicator == -1)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::GetOracleTimeStampTZ()\n"
					});
				}
				return OracleTimeStampTZ.Null;
			}
			OracleTimeStampTZ result = new OracleTimeStampTZ((OpoTSValCtx*)this.m_pOpoDacValCtx->pValCtx, (int)this.m_pOpoMetValCtx->pColMetaVal[i].Scale);
			this.m_pOpoDacValCtx->pValCtx = null;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetOracleTimeStampTZ()\n"
				});
			}
			return result;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00006BB8 File Offset: 0x00005BB8
		public unsafe OracleXmlType GetOracleXmlType(int i)
		{
			OraTrace.Trace(1U, new string[]
			{
				" (ENTRY) OracleDataReader::GetOracleXmlType()\n"
			});
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (oraType != OraType.ORA_NDT)
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
			if (this.m_opsXmlTypeCtx == null)
			{
				this.m_opsXmlTypeCtx = new IntPtr[this.m_fieldCount];
			}
			else if (this.m_opsXmlTypeCtx[i] != IntPtr.Zero)
			{
				return new OracleXmlType(this.m_connection, this.m_opsXmlTypeCtx[i], true);
			}
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			try
			{
				num = OpsDac.GetOraType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (this.m_pOpoDacValCtx->Indicator == -1)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetOracleXmlType()\n"
				});
				return OracleXmlType.Null;
			}
			OracleXmlType result = new OracleXmlType(this.m_connection, (IntPtr)this.m_pOpoDacValCtx->pValCtx, true);
			this.m_opsXmlTypeCtx[i] = (IntPtr)this.m_pOpoDacValCtx->pValCtx;
			this.m_pOpoDacValCtx->pValCtx = null;
			OraTrace.Trace(1U, new string[]
			{
				" (EXIT)  OracleDataReader::GetOracleXmlType()\n"
			});
			return result;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00006DCC File Offset: 0x00005DCC
		public unsafe object GetOracleValue(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetOracleValue()\n"
				});
			}
			object result = null;
			OraType oraType = (OraType)this.m_pOpoMetValCtx->pColMetaVal[i].OraType;
			try
			{
				this.m_external = false;
				OraType oraType2 = oraType;
				if (oraType2 <= OraType.ORA_LONGRAW)
				{
					if (oraType2 <= OraType.ORA_LONG)
					{
						switch (oraType2)
						{
						case OraType.ORA_CHARN:
							break;
						case OraType.ORA_NUMBER:
							result = this.GetOracleDecimal(i);
							goto IL_264;
						default:
							if (oraType2 != OraType.ORA_LONG)
							{
								goto IL_264;
							}
							break;
						}
					}
					else
					{
						if (oraType2 == OraType.ORA_DATE)
						{
							result = this.GetOracleDate(i);
							goto IL_264;
						}
						switch (oraType2)
						{
						case OraType.ORA_RAW:
						case OraType.ORA_LONGRAW:
							result = this.GetOracleBinary(i);
							goto IL_264;
						default:
							goto IL_264;
						}
					}
				}
				else if (oraType2 <= OraType.ORA_OCIBFileLocator)
				{
					if (oraType2 != OraType.ORA_CHAR)
					{
						switch (oraType2)
						{
						case OraType.ORA_IBFLOAT:
						case OraType.ORA_IBDOUBLE:
							result = this.GetOracleDecimal(i);
							goto IL_264;
						case (OraType)102:
						case (OraType)103:
						case (OraType)105:
						case (OraType)106:
						case (OraType)107:
						case (OraType)109:
						case (OraType)111:
							goto IL_264;
						case OraType.ORA_OCIRowid:
							break;
						case OraType.ORA_NDT:
							if (this.m_pOpoMetValCtx->pColMetaVal[i].bIsXmlType != 1)
							{
								result = this.GetCustomObject(i);
								goto IL_264;
							}
							result = this.GetOracleXmlType(i);
							goto IL_264;
						case OraType.ORA_OCIRef:
							result = this.GetOracleRef(i);
							goto IL_264;
						case OraType.ORA_OCICLobLocator:
							if (this.InitialLOBFetchSize == 0 || (this.InitialLOBFetchSize != -1 && this.m_isDBVer10gR2OrHigher))
							{
								result = this.GetOracleClob(i);
								goto IL_264;
							}
							result = this.GetOracleString(i);
							goto IL_264;
						case OraType.ORA_OCIBLobLocator:
							if (this.InitialLOBFetchSize == 0 || (this.InitialLOBFetchSize != -1 && this.m_isDBVer10gR2OrHigher))
							{
								result = this.GetOracleBlob(i);
								goto IL_264;
							}
							result = this.GetOracleBinary(i);
							goto IL_264;
						case OraType.ORA_OCIBFileLocator:
							result = this.GetOracleBFile(i);
							goto IL_264;
						default:
							goto IL_264;
						}
					}
				}
				else
				{
					switch (oraType2)
					{
					case OraType.ORA_TIMESTAMP:
						result = this.GetOracleTimeStamp(i);
						goto IL_264;
					case OraType.ORA_TIMESTAMP_TZ:
						result = this.GetOracleTimeStampTZ(i);
						goto IL_264;
					case OraType.ORA_INTERVAL_YM:
						result = this.GetOracleIntervalYM(i);
						goto IL_264;
					case OraType.ORA_INTERVAL_DS:
						result = this.GetOracleIntervalDS(i);
						goto IL_264;
					default:
						if (oraType2 != OraType.ORA_TIMESTAMP_LTZ)
						{
							goto IL_264;
						}
						result = this.GetOracleTimeStampLTZ(i);
						goto IL_264;
					}
				}
				result = this.GetOracleString(i);
				IL_264:;
			}
			finally
			{
				this.m_external = true;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetOracleValue()\n"
				});
			}
			return result;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00007084 File Offset: 0x00006084
		public int GetOracleValues(object[] values)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetOracleValues()\n"
				});
			}
			if (this.m_closed || this.m_bBOF || this.m_bEOF)
			{
				throw new InvalidOperationException();
			}
			int num = values.Length;
			int num2 = 0;
			if (num < this.m_fieldCount)
			{
				num2 = num;
			}
			else
			{
				num2 = this.m_fieldCount;
			}
			try
			{
				this.m_external = false;
				for (int i = 0; i < num2; i++)
				{
					values[i] = this.GetOracleValue(i);
				}
			}
			finally
			{
				this.m_external = true;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetOracleValues()\n"
				});
			}
			return num2;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00007140 File Offset: 0x00006140
		public override int GetOrdinal(string name)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetOrdinal()\n"
				});
			}
			if (this.m_closed)
			{
				throw new InvalidOperationException();
			}
			if (this.m_pOpoMetValCtx != null && this.m_colMetaRef == null)
			{
				this.GetColMetaRef(false, false);
			}
			for (int i = 0; i < this.m_fieldCount; i++)
			{
				if (name.Equals(this.m_colMetaRef[i].pColAlias))
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(1U, new string[]
						{
							" (EXIT)  OracleDataReader::GetOrdinal()\n"
						});
					}
					return i;
				}
			}
			for (int j = 0; j < this.m_fieldCount; j++)
			{
				if (name.ToUpper().Equals(this.m_colMetaRef[j].pColAlias.ToUpper()))
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(1U, new string[]
						{
							" (EXIT)  OracleDataReader::GetOrdinal()\n"
						});
					}
					return j;
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetOrdinal()\n"
				});
			}
			throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_NAME, new string[0]));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00007260 File Offset: 0x00006260
		public unsafe override DataTable GetSchemaTable()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetSchemaTable()\n"
				});
			}
			if (this.m_closed)
			{
				throw new InvalidOperationException();
			}
			if (this.m_noMoreResults)
			{
				return null;
			}
			if (this.m_pOpoMetValCtx == null)
			{
				return null;
			}
			DataTable dataTable = null;
			if (this.m_dataTable == null)
			{
				int num = 0;
				bool bLocalParsed = false;
				try
				{
					if (this.m_pOpoSqlValCtx->CommandType == 1)
					{
						if (this.m_pOpoMetValCtx->bStmtParsed != 1)
						{
							num = OpsMet.GetSchemaMetaData(this.m_opsConCtx, this.m_opsErrCtx, this.m_pOpoMetValCtx, this.m_pOpoSqlValCtx->AddRowid, this.m_pOpoSqlValCtx->AddToStmtCache);
							bLocalParsed = true;
						}
						if ((this.m_commandBehavior & CommandBehavior.KeyInfo) == CommandBehavior.KeyInfo && this.m_pOpoMetValCtx->bPkFetched != 1)
						{
							num = OpsMet.GetPrimaryKey(this.m_opsConCtx, this.m_opsErrCtx, this.m_pOpoMetValCtx, 1, this.m_pOpoDacValCtx->AddRowid, this.m_pOpoDacValCtx->AddToStmtCache);
							this.UpdateMetaDataPool();
						}
					}
					else
					{
						if (this.m_refCursor != null)
						{
							RefCursorInfo refCursorInfo = this.m_refCursor.m_refCursorInfo;
							if (refCursorInfo != null)
							{
								dataTable = refCursorInfo.columnInfo;
							}
						}
						else
						{
							StoredProcedureInfo storedProcInfo = RegAndConfigRdr.GetStoredProcInfo(this.m_storedProcName);
							if (storedProcInfo != null && storedProcInfo.refCursors.Count > 0)
							{
								dataTable = ((RefCursorInfo)storedProcInfo.refCursors[this.m_currentResultIndex]).columnInfo;
							}
						}
						if (dataTable != null)
						{
							DataTable dataTable2 = dataTable.Copy();
							dataTable2.Columns.Remove("NativeDataType");
							dataTable2.Columns.Remove("ProviderDBType");
							dataTable2.Columns.Remove("ObjectName");
							dataTable2.AcceptChanges();
							return dataTable2;
						}
					}
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				finally
				{
					if (num != 0)
					{
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
				}
				this.GetColMetaRef(true, bLocalParsed);
				this.m_dataTable = new DataTable("SchemaTable");
				this.m_dataTable.MinimumCapacity = this.m_fieldCount;
				this.m_dataTable.Columns.Add("ColumnName", typeof(string));
				this.m_dataTable.Columns.Add("ColumnOrdinal", typeof(int));
				this.m_dataTable.Columns.Add("ColumnSize", typeof(int));
				this.m_dataTable.Columns.Add("NumericPrecision", typeof(short));
				this.m_dataTable.Columns.Add("NumericScale", typeof(short));
				this.m_dataTable.Columns.Add("IsUnique", typeof(bool));
				this.m_dataTable.Columns.Add("IsKey", typeof(bool));
				this.m_dataTable.Columns.Add("IsRowID", typeof(bool));
				this.m_dataTable.Columns.Add("BaseColumnName", typeof(string));
				this.m_dataTable.Columns.Add("BaseSchemaName", typeof(string));
				this.m_dataTable.Columns.Add("BaseTableName", typeof(string));
				this.m_dataTable.Columns.Add("DataType", typeof(Type));
				this.m_dataTable.Columns.Add("ProviderType", typeof(OracleDbType));
				this.m_dataTable.Columns.Add("AllowDBNull", typeof(bool));
				this.m_dataTable.Columns.Add("IsAliased", typeof(bool));
				this.m_dataTable.Columns.Add("IsByteSemantic", typeof(bool));
				this.m_dataTable.Columns.Add("IsExpression", typeof(bool));
				this.m_dataTable.Columns.Add("IsHidden", typeof(bool));
				this.m_dataTable.Columns.Add("IsReadOnly", typeof(bool));
				this.m_dataTable.Columns.Add("IsLong", typeof(bool));
				this.m_dataTable.Columns.Add("UdtTypeName", typeof(string));
				int i = 0;
				while (i < this.m_fieldCount)
				{
					DataRow dataRow = this.m_dataTable.NewRow();
					dataRow[0] = this.m_colMetaRef[i].pColAlias;
					dataRow[1] = i;
					dataRow[7] = false;
					dataRow[19] = false;
					if (this.m_pOpoMetValCtx->pColMetaVal[i].NullOK == 1)
					{
						dataRow[13] = true;
					}
					else
					{
						dataRow[13] = false;
					}
					OraType oraType = (OraType)this.m_pOpoMetValCtx->pColMetaVal[i].OraType;
					OraType oraType2 = oraType;
					if (oraType2 <= OraType.ORA_LONGRAW)
					{
						if (oraType2 == OraType.ORA_CHARN)
						{
							goto IL_6F6;
						}
						switch (oraType2)
						{
						case OraType.ORA_LONG:
							goto IL_6B9;
						case OraType.ORA_VARCHAR:
							goto IL_6F6;
						case (OraType)10:
						case OraType.ORA_ROWID:
							goto IL_7BF;
						case OraType.ORA_DATE:
							dataRow[2] = 7;
							break;
						default:
							if (oraType2 != OraType.ORA_LONGRAW)
							{
								goto IL_7BF;
							}
							goto IL_6B9;
						}
					}
					else if (oraType2 <= OraType.ORA_OCIBFileLocator)
					{
						if (oraType2 == OraType.ORA_CHAR)
						{
							goto IL_6F6;
						}
						switch (oraType2)
						{
						case OraType.ORA_OCIRowid:
							dataRow[2] = 18;
							dataRow[7] = true;
							if (!this.m_connection.IsDBVer10gR2OrHigher && this.m_colMetaRef[i].pColName.ToString().Equals("ROWID"))
							{
								dataRow[13] = false;
							}
							break;
						case (OraType)105:
						case (OraType)106:
						case (OraType)107:
						case (OraType)109:
						case (OraType)111:
							goto IL_7BF;
						case OraType.ORA_NDT:
							dataRow[2] = int.MaxValue;
							break;
						case OraType.ORA_OCIRef:
							dataRow[2] = 256;
							break;
						case OraType.ORA_OCICLobLocator:
						case OraType.ORA_OCIBLobLocator:
						case OraType.ORA_OCIBFileLocator:
							goto IL_6B9;
						default:
							goto IL_7BF;
						}
					}
					else
					{
						switch (oraType2)
						{
						case OraType.ORA_TIMESTAMP:
						case OraType.ORA_INTERVAL_DS:
							break;
						case OraType.ORA_TIMESTAMP_TZ:
							dataRow[2] = 13;
							goto IL_7E7;
						case OraType.ORA_INTERVAL_YM:
							dataRow[2] = 5;
							goto IL_7E7;
						default:
							if (oraType2 != OraType.ORA_TIMESTAMP_LTZ)
							{
								goto IL_7BF;
							}
							break;
						}
						dataRow[2] = 11;
					}
					IL_7E7:
					if (oraType == OraType.ORA_NUMBER || oraType == OraType.ORA_INTERVAL_DS || oraType == OraType.ORA_INTERVAL_YM)
					{
						dataRow[3] = this.m_pOpoMetValCtx->pColMetaVal[i].Precision;
					}
					if (oraType == OraType.ORA_NUMBER || oraType == OraType.ORA_INTERVAL_DS || oraType == OraType.ORA_TIMESTAMP || oraType == OraType.ORA_TIMESTAMP_LTZ || oraType == OraType.ORA_TIMESTAMP_TZ)
					{
						dataRow[4] = this.m_pOpoMetValCtx->pColMetaVal[i].Scale;
					}
					if ((this.m_commandBehavior & CommandBehavior.KeyInfo) == CommandBehavior.KeyInfo)
					{
						if (this.m_pOpoMetValCtx->pColMetaVal[i].bIsUnique == 1)
						{
							dataRow[5] = true;
						}
						else
						{
							dataRow[5] = false;
						}
						if (this.m_pOpoMetValCtx->pColMetaVal[i].bIsKeyColumn == 1)
						{
							dataRow[6] = true;
						}
						else
						{
							dataRow[6] = false;
						}
					}
					dataRow[8] = this.m_colMetaRef[i].pColName;
					dataRow[9] = this.m_colMetaRef[i].pSchemaName;
					dataRow[10] = this.m_colMetaRef[i].pTabName;
					if (this.m_returnPSTypes)
					{
						dataRow[11] = this.GetProviderSpecificFieldType(i);
					}
					else
					{
						dataRow[11] = this.GetFieldType(i);
					}
					if (this.IsCorruptible(oraType) && (Type)dataRow[11] == typeof(string))
					{
						dataRow[2] = -1;
					}
					dataRow[12] = this.m_oracleDbType[i];
					if (this.m_pOpoSqlValCtx->CommandType != 8 && this.m_pOpoSqlValCtx->CommandType != 9)
					{
						string a = null;
						string b = null;
						if (this.m_colMetaRef[i].pColName != null)
						{
							a = this.m_colMetaRef[i].pColName;
						}
						if (this.m_colMetaRef[i].pColAlias != null)
						{
							b = this.m_colMetaRef[i].pColAlias;
						}
						if (a != b)
						{
							dataRow[14] = true;
						}
						else
						{
							dataRow[14] = false;
						}
						if (this.m_pOpoMetValCtx->pColMetaVal[i].bIsExpression == 1)
						{
							dataRow[16] = true;
						}
						else
						{
							dataRow[16] = false;
						}
					}
					if (this.m_pOpoMetValCtx->pColMetaVal[i].bIsHiddenCol == 1)
					{
						dataRow[17] = true;
					}
					else
					{
						dataRow[17] = false;
					}
					if (this.m_pOpoMetValCtx->pColMetaVal[i].Updatable == 1 || this.m_pOpoSqlValCtx->CommandType == 8 || (this.m_pOpoSqlValCtx->CommandType == 9 && !(bool)dataRow[7]))
					{
						dataRow[18] = false;
					}
					else
					{
						dataRow[18] = true;
					}
					if ((oraType == OraType.ORA_NDT && this.m_pOpoMetValCtx->pColMetaVal[i].bIsXmlType == 0) || oraType == OraType.ORA_OCIRef)
					{
						dataRow[20] = this.GetCachedOracleUdtDescriptor(i).UdtTypeName;
					}
					this.m_dataTable.Rows.Add(dataRow);
					i++;
					continue;
					IL_6B9:
					dataRow[2] = int.MaxValue;
					dataRow[19] = true;
					goto IL_7E7;
					IL_6F6:
					if (oraType == OraType.ORA_CHARN)
					{
						dataRow[2] = this.m_pOpoMetValCtx->pColMetaVal[i].Size;
					}
					else
					{
						dataRow[2] = this.m_pOpoMetValCtx->pColMetaVal[i].Size;
					}
					if (this.m_pOpoMetValCtx->pColMetaVal[i].bIsByteSemantic == 1)
					{
						dataRow[15] = true;
						goto IL_7E7;
					}
					if (this.m_pOpoMetValCtx->pColMetaVal[i].CharSetForm != 2)
					{
						dataRow[15] = false;
						goto IL_7E7;
					}
					goto IL_7E7;
					IL_7BF:
					dataRow[2] = this.m_pOpoMetValCtx->pColMetaVal[i].Size;
					goto IL_7E7;
				}
				this.m_dataTable.AcceptChanges();
			}
			DataTable result = this.m_dataTable.Copy();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetSchemaTable()\n"
				});
			}
			return result;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00007E74 File Offset: 0x00006E74
		public unsafe override string GetString(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetString()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (this.m_external && oraType != OraType.ORA_CHAR && oraType != OraType.ORA_CHARN && oraType != OraType.ORA_LONG && oraType != OraType.ORA_OCIRowid && oraType != OraType.ORA_OCICLobLocator && (oraType != OraType.ORA_NDT || this.m_pOpoMetValCtx->pColMetaVal[i].bIsXmlType == 0) && oraType != OraType.ORA_OCIRef)
			{
				throw new InvalidCastException();
			}
			bool flag = true;
			if (this.m_pOpoMetValCtx->pColMetaVal[i].UCS2Character == 0)
			{
				flag = false;
			}
			long num2 = this.m_fetchArrayLocation + this.m_rowLocation + (long)((ulong)this.m_colIndOffset[i]);
			short num3 = *(UIntPtr)num2;
			if (num3 == -1)
			{
				throw new InvalidCastException(OpoErrResManager.GetErrorMesg(ErrRes.DR_NULL_COL_DATA, new string[0]));
			}
			if (oraType == OraType.ORA_CHAR || oraType == OraType.ORA_CHARN || oraType == OraType.ORA_OCIRowid)
			{
				long num4 = this.m_fetchArrayLocation + this.m_rowLocation;
				num2 = num4 + (long)((ulong)this.m_colLenOffset[i]);
				ushort num5 = *(UIntPtr)num2;
				IntPtr intPtr = (IntPtr)(num4 + (long)((ulong)this.m_colOffset[i]));
				if (!flag)
				{
					return OracleString.GetValue(intPtr, (int)num5, false);
				}
				if (this.m_connection.ConnectionType == OracleConnectionType.TimesTen && this.m_pOpoMetValCtx->pColMetaVal[i].Define.Type == 94)
				{
					int num6 = *(UIntPtr)num2;
					intPtr = (IntPtr)(num4 + (long)((ulong)this.m_colOffset[i]) + 4L);
					return Marshal.PtrToStringUni(intPtr, num6 / 2);
				}
				if (this.m_connection.ConnectionType == OracleConnectionType.TimesTen && this.m_pOpoMetValCtx->pColMetaVal[i].Define.Type == 5)
				{
					return Marshal.PtrToStringUni(intPtr);
				}
				return Marshal.PtrToStringUni(intPtr, (int)(num5 / 2));
			}
			else
			{
				this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
				if (oraType == OraType.ORA_NDT && this.m_pOpoMetValCtx->pColMetaVal[i].bIsXmlType == 1)
				{
					if (this.m_opsXmlTypeCtx == null)
					{
						this.m_opsXmlTypeCtx = new IntPtr[this.m_fieldCount];
					}
					else if (this.m_opsXmlTypeCtx[i] != IntPtr.Zero)
					{
						IntPtr zero = IntPtr.Zero;
						int len = 0;
						string result = null;
						try
						{
							num = OpsXmlStream.GetValueBuffer(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsXmlTypeCtx[i], ref zero, ref len);
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
							throw;
						}
						if (num == 0)
						{
							result = Marshal.PtrToStringUni(zero, len);
							try
							{
								num = OpsXmlStream.FreeValueBuffer(ref zero);
							}
							catch (Exception ex2)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex2);
								}
							}
						}
						if (num != 0)
						{
							OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
						}
						OraTrace.Trace(1U, new string[]
						{
							" (EXIT)  OracleDataReader::GetString()\n"
						});
						return result;
					}
				}
				this.m_pOpoDacValCtx->Ordinal = i;
				this.m_pOpoDacValCtx->FieldOffset = 0L;
				this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
				string text = null;
				GCHandle gchandle = default(GCHandle);
				bool flag2;
				if (oraType != OraType.ORA_LONG || this.InitialLONGFetchSize == -1)
				{
					if (oraType != OraType.ORA_OCICLobLocator || this.InitialLOBFetchSize == -1)
					{
						flag2 = true;
						this.m_pOpoDacValCtx->BufLen = 0;
						goto IL_51E;
					}
				}
				try
				{
					num = OpsDac.GetLen(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx);
				}
				catch (Exception ex3)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex3);
					}
					throw;
				}
				if ((oraType == OraType.ORA_LONG || (oraType == OraType.ORA_OCICLobLocator && this.m_pOpoDacValCtx->InitialLobFS > 0 && !this.m_isDBVer10gR2OrHigher)) && !this.m_pkFetched)
				{
					this.UpdateMetaDataPool();
				}
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
				if (this.m_pOpoDacValCtx->Indicator == -1)
				{
					throw new InvalidCastException(OpoErrResManager.GetErrorMesg(ErrRes.DR_NULL_COL_DATA, new string[0]));
				}
				if (((oraType != OraType.ORA_LONG || this.m_pOpoDacValCtx->RetDataLen / 2 > this.m_pOpoDacValCtx->InitialLongFS) && (oraType != OraType.ORA_OCICLobLocator || this.m_pOpoDacValCtx->InitialLobFS <= 0 || this.m_pOpoDacValCtx->RetDataLen / 2 > this.m_pOpoDacValCtx->InitialLobFS)) || (oraType == OraType.ORA_OCICLobLocator && this.m_pOpoDacValCtx->InitialLobFS > 0 && this.m_pOpoDacValCtx->RetDataLen / 2 > this.m_pOpoDacValCtx->InitialLobFS && this.m_isDBVer10gR2OrHigher))
				{
					flag2 = false;
					text = new string('\0', this.m_pOpoDacValCtx->RetDataLen / 2);
					gchandle = GCHandle.Alloc(text, GCHandleType.Pinned);
					this.m_pOpoDacValCtx->pBuffer = gchandle.AddrOfPinnedObject();
					this.m_pOpoDacValCtx->BufLen = this.m_pOpoDacValCtx->RetDataLen;
				}
				else
				{
					flag2 = true;
					this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
					this.m_pOpoDacValCtx->BufLen = this.m_pOpoDacValCtx->RetDataLen;
				}
				IL_51E:
				OpoUdtCtx opoUdtCtx = null;
				if (oraType == OraType.ORA_NDT && this.m_pOpoMetValCtx->pColMetaVal[i].bIsXmlType == 0)
				{
					return this.GetCustomObject(i).ToString();
				}
				if (opoUdtCtx == null)
				{
					try
					{
						num = OpsDac.GetType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx, 0);
					}
					catch (Exception ex4)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex4);
						}
						num = ErrRes.INT_ERR;
						throw;
					}
					finally
					{
						if (num != ErrRes.INT_ERR && (oraType == OraType.ORA_LONG || (oraType == OraType.ORA_OCICLobLocator && this.m_pOpoDacValCtx->InitialLobFS > 0 && !this.m_isDBVer10gR2OrHigher)) && !this.m_pkFetched)
						{
							this.UpdateMetaDataPool();
						}
						if (!flag2 && gchandle.IsAllocated)
						{
							gchandle.Free();
						}
					}
				}
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
				if (this.m_pOpoDacValCtx->Indicator == -1)
				{
					throw new InvalidCastException(OpoErrResManager.GetErrorMesg(ErrRes.DR_NULL_COL_DATA, new string[0]));
				}
				if (oraType == OraType.ORA_NDT || oraType == OraType.ORA_OCIRef)
				{
					if (this.m_pOpoMetValCtx->pColMetaVal[i].bIsXmlType == 1)
					{
						IntPtr intPtr2 = (IntPtr)this.m_pOpoDacValCtx->pValCtx;
						IntPtr zero2 = IntPtr.Zero;
						int len2 = 0;
						string result2 = null;
						this.m_opsXmlTypeCtx[i] = intPtr2;
						this.m_pOpoDacValCtx->pValCtx = null;
						try
						{
							num = OpsXmlStream.GetValueBuffer(this.m_opsConCtx, this.m_opsErrCtx, intPtr2, ref zero2, ref len2);
						}
						catch (Exception ex5)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex5);
							}
							throw;
						}
						if (num == 0)
						{
							result2 = Marshal.PtrToStringUni(zero2, len2);
							try
							{
								num = OpsXmlStream.FreeValueBuffer(ref zero2);
							}
							catch (Exception ex6)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex6);
								}
							}
						}
						if (num != 0)
						{
							OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
						}
						OraTrace.Trace(1U, new string[]
						{
							" (EXIT)  OracleDataReader::GetString()\n"
						});
						return result2;
					}
					OracleUdtDescriptor cachedOracleUdtDescriptor = this.GetCachedOracleUdtDescriptor(i);
					if (oraType == OraType.ORA_OCIRef)
					{
						OracleRef oracleRef;
						if (!this.m_fillReader && this.m_pOpoDacValCtx->Indicator == 0 && this.m_pOpoDacValCtx->pBuffer == IntPtr.Zero)
						{
							oracleRef = (OracleRef)this.m_currentRowUdtCache[i];
						}
						else
						{
							if (opoUdtCtx == null)
							{
								opoUdtCtx = new OpoUdtCtx(this.m_opsConCtx, IntPtr.Zero, this.m_pOpoDacValCtx->pBuffer, this.m_pOpoDacValCtx->pUdtNullStruct);
							}
							if (this.m_pOpoMetValCtx->pColMetaVal[i].bIsFinalType == 0)
							{
								oracleRef = new OracleRef(this.m_connection, opoUdtCtx);
							}
							else
							{
								oracleRef = new OracleRef(cachedOracleUdtDescriptor, opoUdtCtx);
							}
							if (!this.m_fillReader)
							{
								this.m_currentRowUdtCache[i] = oracleRef;
								oracleRef.m_bNotRefByApp = true;
							}
						}
						string value = oracleRef.Value;
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.Trace(1U, new string[]
							{
								" (EXIT)  OracleDataReader::GetString()\n"
							});
						}
						return value;
					}
				}
				this.m_pOpoDacValCtx->RetDataLen /= 2;
				if (!flag2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(1U, new string[]
						{
							" (EXIT)  OracleDataReader::GetString()\n"
						});
					}
					return text;
				}
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::GetString()\n"
					});
				}
				if (!flag)
				{
					return OracleString.GetValue(this.m_pOpoDacValCtx->pBuffer, this.m_pOpoDacValCtx->RetDataLen, false);
				}
				if (this.m_pOpoDacValCtx->pBuffer != IntPtr.Zero)
				{
					return Marshal.PtrToStringUni(this.m_pOpoDacValCtx->pBuffer, this.m_pOpoDacValCtx->RetDataLen);
				}
				return null;
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00008794 File Offset: 0x00007794
		public unsafe TimeSpan GetTimeSpan(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetTimeSpan()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			if (oraType != OraType.ORA_INTERVAL_DS)
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			long num2 = this.m_fetchArrayLocation + this.m_rowLocation + (long)((ulong)this.m_colIndOffset[i]);
			short num3 = *(UIntPtr)num2;
			if (num3 == -1)
			{
				throw new InvalidCastException(OpoErrResManager.GetErrorMesg(ErrRes.DR_NULL_COL_DATA, new string[0]));
			}
			long num4 = this.m_fetchArrayLocation + this.m_rowLocation;
			IntPtr pBuffer = (IntPtr)(num4 + (long)((ulong)this.m_colOffset[i]));
			this.m_pOpoDacValCtx->pBuffer = pBuffer;
			this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
			try
			{
				num = OpsDac.GetType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx, 1);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetTimeSpan()\n"
				});
			}
			return TimeSpanConv.GetTimeSpan((OpoITLValCtx*)this.m_pOpoDacValCtx->pValCtx, OracleDbType.IntervalDS);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00008964 File Offset: 0x00007964
		public unsafe override object GetValue(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetValue()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			OraType oraType = this.m_oraType[i];
			object obj = null;
			try
			{
				this.m_external = false;
				if (oraType == OraType.ORA_OCIRef || oraType == OraType.ORA_NDT || oraType == OraType.ORA_NDT)
				{
					if (this.IsDBNull(i))
					{
						return DBNull.Value;
					}
				}
				else
				{
					long num = this.m_fetchArrayLocation + this.m_rowLocation + (long)((ulong)this.m_colIndOffset[i]);
					short num2 = *(UIntPtr)num;
					if (num2 == -1)
					{
						return DBNull.Value;
					}
				}
				OraType oraType2 = oraType;
				if (oraType2 <= OraType.ORA_LONGRAW)
				{
					if (oraType2 <= OraType.ORA_LONG)
					{
						switch (oraType2)
						{
						case OraType.ORA_CHARN:
							break;
						case OraType.ORA_NUMBER:
							if (this.m_safeMapping != null && this.m_safeMapping.Count > 0)
							{
								Type left = (Type)this.m_safeMapping[this.m_colMetaRef[i].pColAlias];
								if (left == null)
								{
									left = (Type)this.m_safeMapping["*"];
								}
								if (left == typeof(string))
								{
									obj = this.GetOracleDecimal(i).ToString();
								}
								else if (left == typeof(byte[]))
								{
									obj = this.GetOracleDecimal(i).BinData;
								}
							}
							if (obj != null)
							{
								goto IL_6CA;
							}
							switch (this.m_dotNetNumericAccessor[i])
							{
							case DotNetNumericAccessor.GetInt16:
								obj = this.GetInt16(i);
								goto IL_6CA;
							case DotNetNumericAccessor.GetInt32:
								obj = this.GetInt32(i);
								goto IL_6CA;
							case DotNetNumericAccessor.GetInt64:
								obj = this.GetInt64(i);
								goto IL_6CA;
							case DotNetNumericAccessor.GetFloat:
								obj = this.GetFloat(i);
								goto IL_6CA;
							case DotNetNumericAccessor.GetDouble:
								obj = this.GetDouble(i);
								goto IL_6CA;
							case DotNetNumericAccessor.GetDecimal:
								obj = this.GetDecimal(i);
								goto IL_6CA;
							default:
								goto IL_6CA;
							}
							break;
						default:
							if (oraType2 != OraType.ORA_LONG)
							{
								goto IL_6CA;
							}
							break;
						}
					}
					else
					{
						if (oraType2 == OraType.ORA_DATE)
						{
							goto IL_397;
						}
						switch (oraType2)
						{
						case OraType.ORA_RAW:
						case OraType.ORA_LONGRAW:
							goto IL_367;
						default:
							goto IL_6CA;
						}
					}
				}
				else if (oraType2 <= OraType.ORA_OCIBFileLocator)
				{
					if (oraType2 != OraType.ORA_CHAR)
					{
						switch (oraType2)
						{
						case OraType.ORA_IBFLOAT:
							obj = this.GetFloat(i);
							goto IL_6CA;
						case OraType.ORA_IBDOUBLE:
							obj = this.GetDouble(i);
							goto IL_6CA;
						case (OraType)102:
						case (OraType)103:
						case (OraType)105:
						case (OraType)106:
						case (OraType)107:
						case (OraType)109:
						case (OraType)111:
							goto IL_6CA;
						case OraType.ORA_OCIRowid:
						case OraType.ORA_OCICLobLocator:
							break;
						case OraType.ORA_NDT:
							if ((oraType == OraType.ORA_NDT && this.m_pOpoMetValCtx->pColMetaVal[i].bIsXmlType != 1) || oraType == OraType.ORA_OCIRef)
							{
								obj = this.GetCustomObject(i);
								goto IL_6CA;
							}
							obj = this.GetString(i);
							goto IL_6CA;
						case OraType.ORA_OCIRef:
							obj = this.GetString(i);
							goto IL_6CA;
						case OraType.ORA_OCIBLobLocator:
						case OraType.ORA_OCIBFileLocator:
							goto IL_367;
						default:
							goto IL_6CA;
						}
					}
				}
				else
				{
					switch (oraType2)
					{
					case OraType.ORA_TIMESTAMP:
					case OraType.ORA_TIMESTAMP_TZ:
						goto IL_397;
					case OraType.ORA_INTERVAL_YM:
						obj = this.GetInt64(i);
						goto IL_6CA;
					case OraType.ORA_INTERVAL_DS:
						if (this.m_safeMapping != null && this.m_safeMapping.Count > 0)
						{
							Type left2 = (Type)this.m_safeMapping[this.m_colMetaRef[i].pColAlias];
							if (left2 == null)
							{
								left2 = (Type)this.m_safeMapping["*"];
							}
							if (left2 == typeof(string))
							{
								obj = this.GetOracleIntervalDS(i).ToString();
							}
							else if (left2 == typeof(byte[]))
							{
								obj = this.GetOracleIntervalDS(i).BinData;
							}
						}
						if (obj == null)
						{
							obj = this.GetTimeSpan(i);
							goto IL_6CA;
						}
						goto IL_6CA;
					default:
						if (oraType2 != OraType.ORA_TIMESTAMP_LTZ)
						{
							goto IL_6CA;
						}
						goto IL_397;
					}
				}
				obj = this.GetString(i);
				goto IL_6CA;
				IL_367:
				long bytes = this.GetBytes(i, 0L, null, 0, 0);
				byte[] array = new byte[bytes];
				this.GetBytes(i, 0L, array, 0, (int)bytes);
				obj = array;
				goto IL_6CA;
				IL_397:
				if (this.m_isFromEF && this.m_expectedColumnTypes != null && this.m_expectedColumnTypes[i].ClrEquivalentType == typeof(DateTimeOffset))
				{
					OracleTimeStampTZ oracleTimeStampTZ;
					if (oraType == OraType.ORA_TIMESTAMP_TZ)
					{
						oracleTimeStampTZ = this.GetOracleTimeStampTZ(i);
					}
					else if (oraType == OraType.ORA_TIMESTAMP_LTZ)
					{
						oracleTimeStampTZ = this.GetOracleTimeStampLTZ(i).ToOracleTimeStampTZ();
					}
					else if (oraType == OraType.ORA_TIMESTAMP)
					{
						oracleTimeStampTZ = this.GetOracleTimeStamp(i).ToOracleTimeStampTZ();
					}
					else
					{
						oracleTimeStampTZ = this.GetOracleDate(i).ToOracleTimeStamp().ToOracleTimeStampTZ();
					}
					return new DateTimeOffset(oracleTimeStampTZ.Value, oracleTimeStampTZ.GetTimeZoneOffset());
				}
				if (this.m_safeMapping != null && this.m_safeMapping.Count > 0)
				{
					Type left3 = (Type)this.m_safeMapping[this.m_colMetaRef[i].pColAlias];
					if (left3 == null)
					{
						left3 = (Type)this.m_safeMapping["*"];
					}
					if (left3 == typeof(string))
					{
						OraType oraType3 = oraType;
						if (oraType3 != OraType.ORA_DATE)
						{
							switch (oraType3)
							{
							case OraType.ORA_TIMESTAMP:
								obj = this.GetOracleTimeStamp(i).ToString();
								break;
							case OraType.ORA_TIMESTAMP_TZ:
								obj = this.GetOracleTimeStampTZ(i).ToString();
								break;
							default:
								if (oraType3 == OraType.ORA_TIMESTAMP_LTZ)
								{
									obj = this.GetOracleTimeStampLTZ(i).ToString();
								}
								break;
							}
						}
						else
						{
							obj = this.GetOracleDate(i).ToString();
						}
					}
					else if (left3 == typeof(byte[]))
					{
						OraType oraType4 = oraType;
						if (oraType4 != OraType.ORA_DATE)
						{
							switch (oraType4)
							{
							case OraType.ORA_TIMESTAMP:
								obj = this.GetOracleTimeStamp(i).BinData;
								break;
							case OraType.ORA_TIMESTAMP_TZ:
								obj = this.GetOracleTimeStampTZ(i).BinData;
								break;
							default:
								if (oraType4 == OraType.ORA_TIMESTAMP_LTZ)
								{
									obj = this.GetOracleTimeStampLTZ(i).BinData;
								}
								break;
							}
						}
						else
						{
							obj = this.GetOracleDate(i).BinData;
						}
					}
				}
				if (obj == null)
				{
					obj = this.GetDateTime(i);
				}
				IL_6CA:;
			}
			finally
			{
				this.m_external = true;
			}
			if (this.m_isFromEF && this.m_expectedColumnTypes != null && obj.GetType() != this.m_expectedColumnTypes[i].ClrEquivalentType)
			{
				obj = this.ChangeType(obj, this.m_expectedColumnTypes[i].ClrEquivalentType);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetValue()\n"
				});
			}
			return obj;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000090C4 File Offset: 0x000080C4
		public unsafe override int GetValues(object[] values)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetValues()\n"
				});
			}
			if (this.m_closed || this.m_bBOF || this.m_bEOF)
			{
				throw new InvalidOperationException();
			}
			int num = values.Length;
			int num2 = 0;
			IntPtr intPtr = IntPtr.Zero;
			if (num < this.m_fieldCount)
			{
				num2 = num;
			}
			else
			{
				num2 = this.m_fieldCount;
			}
			if (this.IsFillReader)
			{
				if (!(this.m_pColumnsDataBuffer == IntPtr.Zero))
				{
					goto IL_B2;
				}
			}
			try
			{
				this.m_external = false;
				for (int i = 0; i < num2; i++)
				{
					values[i] = this.GetValue(i);
				}
				goto IL_DB4;
			}
			finally
			{
				this.m_external = true;
			}
			try
			{
				IL_B2:
				this.m_external = false;
				int j = 0;
				while (j < num2)
				{
					OraType oraType = this.m_oraType[j];
					object obj = null;
					long num3;
					long num4;
					short num5;
					if (oraType == OraType.ORA_OCIRef || oraType == OraType.ORA_NDT || oraType == OraType.ORA_NDT)
					{
						if (!this.IsDBNull(j))
						{
							goto IL_12C;
						}
						values[j] = DBNull.Value;
					}
					else
					{
						num3 = this.m_fetchArrayLocation + this.m_rowLocation;
						num4 = num3 + (long)((ulong)this.m_colIndOffset[j]);
						num5 = *(UIntPtr)num4;
						if (num5 != -1)
						{
							goto IL_12C;
						}
						values[j] = DBNull.Value;
					}
					IL_D9C:
					j++;
					continue;
					IL_12C:
					OraType oraType2 = oraType;
					if (oraType2 <= OraType.ORA_LONGRAW)
					{
						if (oraType2 <= OraType.ORA_LONG)
						{
							switch (oraType2)
							{
							case OraType.ORA_CHARN:
								break;
							case OraType.ORA_NUMBER:
								if (this.m_safeMapping != null && this.m_safeMapping.Count > 0)
								{
									Type left = (Type)this.m_safeMapping[this.m_colMetaRef[j].pColAlias];
									if (left == null)
									{
										left = (Type)this.m_safeMapping["*"];
									}
									if (left == typeof(string))
									{
										obj = this.GetOracleDecimal(j).ToString();
									}
									else if (left == typeof(byte[]))
									{
										obj = this.GetOracleDecimal(j).BinData;
									}
								}
								if (obj != null)
								{
									values[j] = obj;
									goto IL_D9C;
								}
								switch (this.m_dotNetNumericAccessor[j])
								{
								case DotNetNumericAccessor.GetInt16:
									num4 = this.m_fetchArrayLocation + this.m_rowLocation + (long)((ulong)this.m_colIndOffset[j]);
									if (*(UIntPtr)num4 == -1)
									{
										values[j] = DBNull.Value;
										goto IL_D9C;
									}
									values[j] = *(short*)((byte*)((byte*)((void*)this.m_pColumnsDataBuffer) + this.m_colDatOffset[j]) + (long)(this.m_currentClientRow - 1) % this.m_pOpoDacValCtx->FetchSize * (long)((ulong)this.m_colDatSize[j]));
									goto IL_D9C;
								case DotNetNumericAccessor.GetInt32:
									num4 = this.m_fetchArrayLocation + this.m_rowLocation + (long)((ulong)this.m_colIndOffset[j]);
									if (*(UIntPtr)num4 == -1)
									{
										values[j] = DBNull.Value;
										goto IL_D9C;
									}
									values[j] = *(int*)((byte*)((byte*)((void*)this.m_pColumnsDataBuffer) + this.m_colDatOffset[j]) + (long)(this.m_currentClientRow - 1) % this.m_pOpoDacValCtx->FetchSize * (long)((ulong)this.m_colDatSize[j]));
									goto IL_D9C;
								case DotNetNumericAccessor.GetInt64:
									num4 = this.m_fetchArrayLocation + this.m_rowLocation + (long)((ulong)this.m_colIndOffset[j]);
									if (*(UIntPtr)num4 == -1)
									{
										values[j] = DBNull.Value;
										goto IL_D9C;
									}
									values[j] = *(long*)((byte*)((byte*)((void*)this.m_pColumnsDataBuffer) + this.m_colDatOffset[j]) + (long)(this.m_currentClientRow - 1) % this.m_pOpoDacValCtx->FetchSize * (long)((ulong)this.m_colDatSize[j]));
									goto IL_D9C;
								case DotNetNumericAccessor.GetFloat:
									num4 = this.m_fetchArrayLocation + this.m_rowLocation + (long)((ulong)this.m_colIndOffset[j]);
									if (*(UIntPtr)num4 == -1)
									{
										values[j] = DBNull.Value;
										goto IL_D9C;
									}
									values[j] = (float)(*(double*)((byte*)((byte*)((void*)this.m_pColumnsDataBuffer) + this.m_colDatOffset[j]) + (long)(this.m_currentClientRow - 1) % this.m_pOpoDacValCtx->FetchSize * (long)((ulong)this.m_colDatSize[j])));
									goto IL_D9C;
								case DotNetNumericAccessor.GetDouble:
									num4 = this.m_fetchArrayLocation + this.m_rowLocation + (long)((ulong)this.m_colIndOffset[j]);
									if (*(UIntPtr)num4 == -1)
									{
										values[j] = DBNull.Value;
										goto IL_D9C;
									}
									values[j] = *(double*)((byte*)((byte*)((void*)this.m_pColumnsDataBuffer) + this.m_colDatOffset[j]) + (long)(this.m_currentClientRow - 1) % this.m_pOpoDacValCtx->FetchSize * (long)((ulong)this.m_colDatSize[j]));
									goto IL_D9C;
								case DotNetNumericAccessor.GetDecimal:
									num3 = this.m_fetchArrayLocation + this.m_rowLocation;
									num4 = num3 + (long)((ulong)this.m_colIndOffset[j]);
									num5 = *(UIntPtr)num4;
									if (num5 == -1)
									{
										values[j] = DBNull.Value;
										goto IL_D9C;
									}
									intPtr = (IntPtr)(num3 + (long)((ulong)this.m_colOffset[j]));
									values[j] = DecimalConv.GetDecimal(intPtr);
									goto IL_D9C;
								default:
									goto IL_D9C;
								}
								break;
							default:
								if (oraType2 != OraType.ORA_LONG)
								{
									goto IL_D9C;
								}
								goto IL_26C;
							}
						}
						else
						{
							if (oraType2 == OraType.ORA_DATE)
							{
								goto IL_7BA;
							}
							switch (oraType2)
							{
							case OraType.ORA_RAW:
							case OraType.ORA_LONGRAW:
								goto IL_752;
							default:
								goto IL_D9C;
							}
						}
					}
					else if (oraType2 <= OraType.ORA_OCIBFileLocator)
					{
						if (oraType2 != OraType.ORA_CHAR)
						{
							switch (oraType2)
							{
							case OraType.ORA_IBFLOAT:
								num3 = this.m_fetchArrayLocation + this.m_rowLocation;
								num4 = num3 + (long)((ulong)this.m_colIndOffset[j]);
								num5 = *(UIntPtr)num4;
								if (num5 == -1)
								{
									values[j] = DBNull.Value;
									goto IL_D9C;
								}
								intPtr = (IntPtr)(num3 + (long)((ulong)this.m_colOffset[j]));
								values[j] = *(float*)((void*)intPtr);
								goto IL_D9C;
							case OraType.ORA_IBDOUBLE:
								num3 = this.m_fetchArrayLocation + this.m_rowLocation;
								num4 = num3 + (long)((ulong)this.m_colIndOffset[j]);
								num5 = *(UIntPtr)num4;
								if (num5 == -1)
								{
									values[j] = DBNull.Value;
									goto IL_D9C;
								}
								intPtr = (IntPtr)(num3 + (long)((ulong)this.m_colOffset[j]));
								values[j] = *(double*)((void*)intPtr);
								goto IL_D9C;
							case (OraType)102:
							case (OraType)103:
							case (OraType)105:
							case (OraType)106:
							case (OraType)107:
							case (OraType)109:
							case (OraType)111:
								goto IL_D9C;
							case OraType.ORA_OCIRowid:
								break;
							case OraType.ORA_NDT:
								if ((oraType == OraType.ORA_NDT && this.m_pOpoMetValCtx->pColMetaVal[j].bIsXmlType != 1) || oraType == OraType.ORA_OCIRef)
								{
									values[j] = this.GetCustomObject(j);
									goto IL_D9C;
								}
								values[j] = this.GetString(j);
								goto IL_D9C;
							case OraType.ORA_OCIRef:
								values[j] = this.GetString(j);
								goto IL_D9C;
							case OraType.ORA_OCICLobLocator:
								goto IL_26C;
							case OraType.ORA_OCIBLobLocator:
							case OraType.ORA_OCIBFileLocator:
								goto IL_752;
							default:
								goto IL_D9C;
							}
						}
					}
					else
					{
						switch (oraType2)
						{
						case OraType.ORA_TIMESTAMP:
						case OraType.ORA_TIMESTAMP_TZ:
							goto IL_7BA;
						case OraType.ORA_INTERVAL_YM:
						{
							num3 = this.m_fetchArrayLocation + this.m_rowLocation;
							num4 = num3 + (long)((ulong)this.m_colIndOffset[j]);
							num5 = *(UIntPtr)num4;
							if (num5 == -1)
							{
								values[j] = DBNull.Value;
								goto IL_D9C;
							}
							long* ptr = (long*)((byte*)((byte*)((void*)this.m_pColumnsDataBuffer) + this.m_colDatOffset[j]) + (long)(this.m_currentClientRow - 1) % this.m_pOpoDacValCtx->FetchSize * (long)((ulong)this.m_colDatSize[j]));
							values[j] = *ptr;
							goto IL_D9C;
						}
						case OraType.ORA_INTERVAL_DS:
						{
							if (this.m_safeMapping != null && this.m_safeMapping.Count > 0)
							{
								Type left2 = (Type)this.m_safeMapping[this.m_colMetaRef[j].pColAlias];
								if (left2 == null)
								{
									left2 = (Type)this.m_safeMapping["*"];
								}
								if (left2 == typeof(string))
								{
									obj = this.GetOracleIntervalDS(j).ToString();
								}
								else if (left2 == typeof(byte[]))
								{
									obj = this.GetOracleIntervalDS(j).BinData;
								}
							}
							if (obj != null)
							{
								values[j] = obj;
								goto IL_D9C;
							}
							num3 = this.m_fetchArrayLocation + this.m_rowLocation;
							num4 = num3 + (long)((ulong)this.m_colIndOffset[j]);
							num5 = *(UIntPtr)num4;
							if (num5 == -1)
							{
								values[j] = DBNull.Value;
								goto IL_D9C;
							}
							intPtr = (IntPtr)(num3 + (long)((ulong)this.m_colOffset[j]));
							IDSCtx* ptr2 = (IDSCtx*)((byte*)((byte*)((void*)this.m_pColumnsDataBuffer) + this.m_colDatOffset[j]) + (long)(this.m_currentClientRow - 1) % this.m_pOpoDacValCtx->FetchSize * (long)((ulong)this.m_colDatSize[j]));
							if (Math.Abs(ptr2->m_fSeconds) % 100 > 0)
							{
								throw new OracleTypeException(ErrRes.TYP_GETDOTNETTYPE_FAIL, new object[0]);
							}
							decimal num6 = ptr2->m_days * 864000000000m + (long)ptr2->m_hours * 36000000000L + (long)ptr2->m_minutes * 600000000L + (long)ptr2->m_seconds * 10000000L + ptr2->m_fSeconds * 0.01m;
							if (num6 < -9223372036854775808m || num6 > 9223372036854775807m)
							{
								throw new OracleTypeException(ErrRes.TYP_GETDOTNETTYPE_FAIL, new object[0]);
							}
							values[j] = new TimeSpan((long)num6);
							goto IL_D9C;
						}
						default:
							if (oraType2 != OraType.ORA_TIMESTAMP_LTZ)
							{
								goto IL_D9C;
							}
							goto IL_7BA;
						}
					}
					num3 = this.m_fetchArrayLocation + this.m_rowLocation;
					num4 = num3 + (long)((ulong)this.m_colIndOffset[j]);
					num5 = *(UIntPtr)num4;
					if (num5 == -1)
					{
						values[j] = DBNull.Value;
						goto IL_D9C;
					}
					num4 = num3 + (long)((ulong)this.m_colLenOffset[j]);
					short num7 = *(UIntPtr)num4;
					IntPtr ptr3 = (IntPtr)(num3 + (long)((ulong)this.m_colOffset[j]));
					values[j] = Marshal.PtrToStringUni(ptr3, (int)(num7 / 2));
					goto IL_D9C;
					IL_26C:
					num3 = this.m_fetchArrayLocation + this.m_rowLocation;
					num4 = num3 + (long)((ulong)this.m_colIndOffset[j]);
					num5 = *(UIntPtr)num4;
					if (num5 == -1)
					{
						values[j] = DBNull.Value;
						goto IL_D9C;
					}
					values[j] = this.GetString(j);
					goto IL_D9C;
					IL_752:
					num3 = this.m_fetchArrayLocation + this.m_rowLocation;
					num4 = num3 + (long)((ulong)this.m_colIndOffset[j]);
					num5 = *(UIntPtr)num4;
					if (num5 == -1)
					{
						values[j] = DBNull.Value;
						goto IL_D9C;
					}
					long bytes = this.GetBytes(j, 0L, null, 0, 0);
					byte[] array = new byte[bytes];
					this.GetBytes(j, 0L, array, 0, (int)bytes);
					values[j] = array;
					goto IL_D9C;
					IL_7BA:
					if (this.m_safeMapping != null && this.m_safeMapping.Count > 0)
					{
						Type left3 = (Type)this.m_safeMapping[this.m_colMetaRef[j].pColAlias];
						if (left3 == null)
						{
							left3 = (Type)this.m_safeMapping["*"];
						}
						if (left3 == typeof(string))
						{
							OraType oraType3 = oraType;
							if (oraType3 != OraType.ORA_DATE)
							{
								switch (oraType3)
								{
								case OraType.ORA_TIMESTAMP:
									obj = this.GetOracleTimeStamp(j).ToString();
									break;
								case OraType.ORA_TIMESTAMP_TZ:
									obj = this.GetOracleTimeStampTZ(j).ToString();
									break;
								default:
									if (oraType3 == OraType.ORA_TIMESTAMP_LTZ)
									{
										obj = this.GetOracleTimeStampLTZ(j).ToString();
									}
									break;
								}
							}
							else
							{
								obj = this.GetOracleDate(j).ToString();
							}
						}
						else if (left3 == typeof(byte[]))
						{
							OraType oraType4 = oraType;
							if (oraType4 != OraType.ORA_DATE)
							{
								switch (oraType4)
								{
								case OraType.ORA_TIMESTAMP:
									obj = this.GetOracleTimeStamp(j).BinData;
									break;
								case OraType.ORA_TIMESTAMP_TZ:
									obj = this.GetOracleTimeStampTZ(j).BinData;
									break;
								default:
									if (oraType4 == OraType.ORA_TIMESTAMP_LTZ)
									{
										obj = this.GetOracleTimeStampLTZ(j).BinData;
									}
									break;
								}
							}
							else
							{
								obj = this.GetOracleDate(j).BinData;
							}
						}
					}
					if (obj != null)
					{
						values[j] = obj;
						goto IL_D9C;
					}
					num3 = this.m_fetchArrayLocation + this.m_rowLocation;
					num4 = num3 + (long)((ulong)this.m_colIndOffset[j]);
					num5 = *(UIntPtr)num4;
					if (num5 == -1)
					{
						values[j] = DBNull.Value;
						goto IL_D9C;
					}
					OraType oraType5 = oraType;
					if (oraType5 == OraType.ORA_DATE)
					{
						intPtr = (IntPtr)(num3 + (long)((ulong)this.m_colOffset[j]));
						byte* ptr4 = (byte*)((void*)intPtr);
						int year = (int)((*ptr4 - 100) * 100 + (ptr4[1] - 100));
						values[j] = new DateTime(year, (int)ptr4[2], (int)ptr4[3], (int)(ptr4[4] - 1), (int)(ptr4[5] - 1), (int)(ptr4[6] - 1));
						goto IL_D9C;
					}
					switch (oraType5)
					{
					case OraType.ORA_TIMESTAMP:
					case OraType.ORA_TIMESTAMP_TZ:
						break;
					default:
						if (oraType5 != OraType.ORA_TIMESTAMP_LTZ)
						{
							goto IL_D9C;
						}
						break;
					}
					OpoDatValCtx* ptr5 = (OpoDatValCtx*)((byte*)((byte*)((void*)this.m_pColumnsDataBuffer) + this.m_colDatOffset[j]) + (long)(this.m_currentClientRow - 1) % this.m_pOpoDacValCtx->FetchSize * (long)((ulong)this.m_colDatSize[j]));
					DateTime dateTime = new DateTime((int)ptr5->m_year, (int)ptr5->m_month, (int)ptr5->m_day, (int)ptr5->m_hour, (int)ptr5->m_minute, (int)ptr5->m_second);
					if (ptr5->m_fSecond > 0)
					{
						values[j] = dateTime.AddTicks((long)(ptr5->m_fSecond / 100));
						goto IL_D9C;
					}
					values[j] = dateTime;
					goto IL_D9C;
				}
			}
			finally
			{
				this.m_external = true;
			}
			IL_DB4:
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetValues()\n"
				});
			}
			return num2;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00009EDC File Offset: 0x00008EDC
		public unsafe XmlReader GetXmlReader(int i)
		{
			OraTrace.Trace(1U, new string[]
			{
				" (ENTRY) OracleDataReader::GetXmlReader()\n"
			});
			if (this.m_closed || this.m_bBOF || this.m_bEOF)
			{
				throw new InvalidOperationException();
			}
			string s = null;
			if (i >= this.m_fieldCount || i < 0)
			{
				throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
			}
			int num = 0;
			OraType oraType = this.m_oraType[i];
			this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
			if (oraType != OraType.ORA_NDT || this.m_pOpoMetValCtx->pColMetaVal[i].bIsXmlType != 1)
			{
				throw new InvalidCastException();
			}
			if (this.m_opsXmlTypeCtx == null)
			{
				this.m_opsXmlTypeCtx = new IntPtr[this.m_fieldCount];
			}
			if (this.m_opsXmlTypeCtx[i] != IntPtr.Zero)
			{
				IntPtr zero = IntPtr.Zero;
				int len = 0;
				try
				{
					num = OpsXmlStream.GetValueBuffer(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsXmlTypeCtx[i], ref zero, ref len);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				if (num == 0)
				{
					s = Marshal.PtrToStringUni(zero, len);
					try
					{
						num = OpsXmlStream.FreeValueBuffer(ref zero);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
					}
				}
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			else
			{
				this.m_pOpoDacValCtx->Ordinal = i;
				this.m_pOpoDacValCtx->FieldOffset = 0L;
				this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
				this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
				this.m_pOpoDacValCtx->BufLen = 0;
				this.m_pOpoDacValCtx->FieldOffset = 0L;
				try
				{
					num = OpsDac.GetType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx, 0);
				}
				catch (Exception ex3)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex3);
					}
					throw;
				}
				finally
				{
					if (num != 0)
					{
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
				}
				if (this.m_pOpoDacValCtx->Indicator == -1)
				{
					throw new InvalidCastException(OpoErrResManager.GetErrorMesg(ErrRes.DR_NULL_COL_DATA, new string[0]));
				}
				IntPtr intPtr = (IntPtr)this.m_pOpoDacValCtx->pValCtx;
				this.m_pOpoDacValCtx->pValCtx = null;
				IntPtr zero2 = IntPtr.Zero;
				int len2 = 0;
				this.m_opsXmlTypeCtx[i] = intPtr;
				try
				{
					num = OpsXmlStream.GetValueBuffer(this.m_opsConCtx, this.m_opsErrCtx, intPtr, ref zero2, ref len2);
				}
				catch (Exception ex4)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex4);
					}
					throw;
				}
				if (num == 0)
				{
					s = Marshal.PtrToStringUni(zero2, len2);
					try
					{
						num = OpsXmlStream.FreeValueBuffer(ref zero2);
					}
					catch (Exception ex5)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex5);
						}
					}
				}
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			TextReader input = new StringReader(s);
			XmlReader result = new XmlTextReader(input);
			OraTrace.Trace(1U, new string[]
			{
				" (EXIT)  OracleDataReader::GetXmlReader()\n"
			});
			return result;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000A234 File Offset: 0x00009234
		public unsafe override bool IsDBNull(int i)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::IsDBNull()\n"
				});
			}
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			OraType oraType = this.m_oraType[i];
			if (oraType != OraType.ORA_OCIRef && oraType != OraType.ORA_NDT && oraType != OraType.ORA_NDT)
			{
				long num = this.m_fetchArrayLocation + this.m_rowLocation + (long)((ulong)this.m_colIndOffset[i]);
				short num2 = *(UIntPtr)num;
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::IsDBNull()\n"
					});
				}
				return num2 == -1;
			}
			int num3 = 0;
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
			try
			{
				num3 = OpsDac.GetInd(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num3 != 0)
				{
					OracleException.HandleError(num3, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (this.m_pOpoDacValCtx->Indicator == -1)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::IsDBNull()\n"
					});
				}
				return true;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::IsDBNull()\n"
				});
			}
			return false;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000A3E4 File Offset: 0x000093E4
		public unsafe override bool NextResult()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::NextResult()\n"
				});
			}
			if (this.m_closed)
			{
				throw new InvalidOperationException();
			}
			int num = 0;
			if (this.m_refCursor != null && !this.m_fillReader)
			{
				this.m_refCursor.m_state = OraRefCursorState.Closed;
			}
			if (this.m_currentResultIndex == this.m_resultCount - 1 || this.m_opsSqlCtx == null || (this.m_commandBehavior & CommandBehavior.SingleResult) == CommandBehavior.SingleResult)
			{
				if (!this.m_noMoreResults && !this.m_fillReader)
				{
					if (this.m_pOpoSqlValCtx != null && this.m_pOpoSqlValCtx->bPooledFetchArray == 1)
					{
						this.m_fetchArrayPooler.PutFetchArray((IntPtr)this.m_fetchArrayLocation);
						this.m_fetchArrayLocation = 0L;
						this.m_pOpoSqlValCtx->FetchArrayLocation = IntPtr.Zero;
					}
					if (this.m_opsSqlCtx == null)
					{
						try
						{
							OpsDac.Dispose(this.m_opsConCtx, this.m_opsErrCtx, IntPtr.Zero, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx, this.m_pOpoSqlValCtx, 1);
							this.m_opsErrCtx = IntPtr.Zero;
							goto IL_3A9;
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
							goto IL_3A9;
						}
					}
					try
					{
						if (this.m_freeOpsSqlCtx == 1)
						{
							OpsDac.Dispose(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsSqlCtx[this.m_currentResultIndex], this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx, this.m_pOpoSqlValCtx, 1);
						}
						else
						{
							OpsDac.Dispose(this.m_opsConCtx, this.m_opsErrCtx, IntPtr.Zero, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx, this.m_pOpoSqlValCtx, 1);
						}
						this.m_opsErrCtx = IntPtr.Zero;
						this.m_opsSqlCtx[this.m_currentResultIndex] = IntPtr.Zero;
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
					}
					if (this.m_opsSqlCtx[this.m_currentResultIndex] != IntPtr.Zero)
					{
						try
						{
							if (this.m_freeOpsSqlCtx == 1)
							{
								if (this.m_pOpoSqlValCtx->CommandType == 1)
								{
									if (this.m_pOpoSqlValCtx->AddToStmtCache == 0)
									{
										OpsSql.FreeCtx(ref this.m_opsSqlCtx[this.m_currentResultIndex], this.m_opsErrCtx, 0);
									}
									else
									{
										OpsSql.FreeCtx(ref this.m_opsSqlCtx[this.m_currentResultIndex], this.m_opsErrCtx, 1);
									}
								}
								else
								{
									OpsSql.FreeCtx(ref this.m_opsSqlCtx[this.m_currentResultIndex], this.m_opsErrCtx, 0);
								}
							}
						}
						catch (Exception ex3)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex3);
							}
						}
						this.m_opsSqlCtx[this.m_currentResultIndex] = IntPtr.Zero;
					}
					if ((this.m_commandBehavior & CommandBehavior.SingleResult) == CommandBehavior.SingleResult)
					{
						int i = 0;
						this.m_currentResultIndex++;
						for (i = this.m_currentResultIndex; i < this.m_resultCount; i++)
						{
							if (this.m_opsSqlCtx[i] != IntPtr.Zero)
							{
								try
								{
									if (this.m_pOpoSqlValCtx->CommandType == 1)
									{
										if (this.m_pOpoSqlValCtx->AddToStmtCache == 0)
										{
											OpsSql.FreeCtx(ref this.m_opsSqlCtx[i], this.m_opsErrCtx, 0);
										}
										else
										{
											OpsSql.FreeCtx(ref this.m_opsSqlCtx[i], this.m_opsErrCtx, 1);
										}
									}
									else
									{
										OpsSql.FreeCtx(ref this.m_opsSqlCtx[i], this.m_opsErrCtx, 0);
									}
								}
								catch (Exception ex4)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex4);
									}
								}
								this.m_opsSqlCtx[i] = IntPtr.Zero;
							}
						}
					}
					IL_3A9:
					if (this.m_opsErrCtx != IntPtr.Zero)
					{
						try
						{
							OpsErr.FreeCtx(ref this.m_opsErrCtx);
						}
						catch (Exception ex5)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex5);
							}
						}
						this.m_opsErrCtx = IntPtr.Zero;
					}
					this.m_pOpoSqlValCtx = null;
					this.m_pOpoDacValCtx = null;
					this.m_pOpoMetValCtx = null;
					this.m_pkFetched = false;
					this.m_doneMarshalAndStoreMetaData = false;
					this.m_bBOF = true;
					this.m_bEOF = true;
					this.m_bLastFetch = true;
					this.m_bSetLastFetch = true;
					this.m_opsErrCtx = IntPtr.Zero;
					this.m_opsDacCtx = IntPtr.Zero;
					this.m_colMetaRef = null;
					this.m_noMoreResults = true;
					this.m_metaData = null;
					this.m_expectedColumnTypes = null;
					this.m_isFromEF = false;
					this.m_colDatOffset = null;
					this.m_colDatSize = null;
					if (this.m_currentRowUdtCache != null)
					{
						for (int j = 0; j < this.m_currentRowUdtCache.Length; j++)
						{
							if (this.m_currentRowUdtCache[j] != null)
							{
								OracleRef oracleRef = this.m_currentRowUdtCache[j] as OracleRef;
								if (oracleRef != null && oracleRef.m_bNotRefByApp)
								{
									oracleRef.Dispose();
								}
								this.m_currentRowUdtCache[j] = null;
							}
						}
						this.m_currentRowUdtCache = null;
					}
					this.m_udtDescriptorCache = null;
				}
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::NextResult()\n"
					});
				}
				return false;
			}
			if (this.m_pOpoSqlValCtx != null && this.m_pOpoSqlValCtx->bPooledFetchArray == 1)
			{
				this.m_fetchArrayPooler.PutFetchArray((IntPtr)this.m_fetchArrayLocation);
				this.m_fetchArrayLocation = 0L;
				this.m_pOpoSqlValCtx->FetchArrayLocation = IntPtr.Zero;
			}
			try
			{
				num = OpsDac.NextResult(this.m_connection.m_opoConCtx.opsConCtx, this.m_opsErrCtx, this.m_opsSqlCtx, this.m_opsDacCtx, this.m_pOpoSqlValCtx, ref this.m_pOpoMetValCtx, this.m_pOpoDacValCtx);
			}
			catch (Exception ex6)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex6);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			this.m_recordCount = this.m_pOpoDacValCtx->RecordCount;
			this.m_opsSqlCtx[this.m_currentResultIndex] = IntPtr.Zero;
			this.m_metaData = new MetaData(this.m_pOpoMetValCtx, this.m_addRowid);
			try
			{
				OpsMet.AddRef(this.m_pOpoMetValCtx);
			}
			catch (Exception ex7)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex7);
				}
				throw;
			}
			if (this.m_pOpoMetValCtx != null)
			{
				this.m_rowSize = (long)this.m_pOpoMetValCtx->pColMetaVal[this.m_pOpoMetValCtx->NoOfCols - 1].Offset;
				this.m_fieldCount = (int)(this.m_pOpoMetValCtx->NoOfCols - this.m_pOpoMetValCtx->NoOfHiddenCols);
			}
			this.m_currentResultIndex++;
			this.m_pOpoDacValCtx->ResultsetIndex = this.m_currentResultIndex;
			this.m_pOpoDacValCtx->InitialLongFS = this.m_pOpoSqlValCtx->InitialLongFS;
			this.m_pOpoDacValCtx->InitialLobFS = this.m_pOpoSqlValCtx->InitialLobFS;
			this.m_opsDacCtx = IntPtr.Zero;
			this.m_bBOF = true;
			this.m_bEOF = false;
			this.m_bLastFetch = false;
			this.m_bSetLastFetch = false;
			this.m_colMetaRef = null;
			this.m_doneReadOne = false;
			this.m_hasRows = false;
			this.m_bHasRowsCalledBeforeRead = false;
			this.m_expectedColumnTypes = null;
			this.m_isFromEF = false;
			this.m_colDatOffset = null;
			this.m_colDatSize = null;
			this.m_bHasUdtType = false;
			if (this.m_pOpoMetValCtx != null)
			{
				this.m_bHasUdtType = (this.m_pOpoMetValCtx->bHasUdtType == 1);
				this.m_colOffset = new uint[this.m_fieldCount];
				this.m_colIndOffset = new uint[this.m_fieldCount];
				this.m_colLenOffset = new uint[this.m_fieldCount];
				this.m_oraType = new OraType[this.m_fieldCount];
				this.m_oracleDbType = new OracleDbType[this.m_fieldCount];
				this.m_dotNetNumericAccessor = new DotNetNumericAccessor[this.m_fieldCount];
				for (int k = 0; k < this.m_fieldCount; k++)
				{
					this.m_colOffset[k] = 0U;
					if (k > 0)
					{
						this.m_colOffset[k] = this.m_pOpoMetValCtx->pColMetaVal[k - 1].Offset;
					}
					this.m_colIndOffset[k] = this.m_colOffset[k] + this.m_pOpoMetValCtx->pColMetaVal[k].Define.Length;
					this.m_colLenOffset[k] = this.m_colIndOffset[k] + 2U;
					this.m_oraType[k] = (OraType)this.m_pOpoMetValCtx->pColMetaVal[k].OraType;
					this.m_oracleDbType[k] = this.GetOraDbTypeEx(k);
					int scale = (int)this.m_pOpoMetValCtx->pColMetaVal[k].Scale;
					int precision = (int)this.m_pOpoMetValCtx->pColMetaVal[k].Precision;
					if (scale <= 0 && precision - scale < 5)
					{
						this.m_dotNetNumericAccessor[k] = DotNetNumericAccessor.GetInt16;
					}
					else if (scale <= 0 && precision - scale < 10)
					{
						this.m_dotNetNumericAccessor[k] = DotNetNumericAccessor.GetInt32;
					}
					else if (scale <= 0 && precision - scale < 19)
					{
						this.m_dotNetNumericAccessor[k] = DotNetNumericAccessor.GetInt64;
					}
					else if (precision < 8 && ((scale <= 0 && precision - scale <= 38) || (scale > 0 && scale <= 44)))
					{
						this.m_dotNetNumericAccessor[k] = DotNetNumericAccessor.GetFloat;
					}
					else if (precision < 16)
					{
						this.m_dotNetNumericAccessor[k] = DotNetNumericAccessor.GetDouble;
					}
					else
					{
						this.m_dotNetNumericAccessor[k] = DotNetNumericAccessor.GetDecimal;
					}
				}
				this.m_pColumnsDataBuffer = IntPtr.Zero;
				this.m_currentClientRow = 0;
			}
			if (this.m_opsSqlCtx == null || this.m_pOpoMetValCtx == null || (this.m_commandBehavior & CommandBehavior.SchemaOnly) == CommandBehavior.SchemaOnly)
			{
				this.m_bSetLastFetch = true;
			}
			this.m_bCmdBehaviorSingleRow = ((this.m_commandBehavior & CommandBehavior.SingleRow) == CommandBehavior.SingleRow);
			if (this.m_currentRowUdtCache != null)
			{
				for (int l = 0; l < this.m_currentRowUdtCache.Length; l++)
				{
					if (this.m_currentRowUdtCache[l] != null)
					{
						OracleRef oracleRef2 = this.m_currentRowUdtCache[l] as OracleRef;
						if (oracleRef2 != null && oracleRef2.m_bNotRefByApp)
						{
							oracleRef2.Dispose();
						}
						this.m_currentRowUdtCache[l] = null;
					}
				}
				this.m_currentRowUdtCache = null;
			}
			this.m_udtDescriptorCache = null;
			if (this.m_dataTable != null)
			{
				this.m_dataTable.Dispose();
				this.m_dataTable = null;
			}
			if (this.m_fillReader)
			{
				DataTable minSchemaTable = this.GetMinSchemaTable();
				if (minSchemaTable != null)
				{
					this.m_dataTableList.Add(minSchemaTable);
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::NextResult()\n"
				});
			}
			return true;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000AE98 File Offset: 0x00009E98
		internal void GetDataOffsetsAndSizes(uint[] colDatOffset, uint[] colDatSize, int rows)
		{
			int i = 0;
			while (i < this.m_fieldCount)
			{
				int num = 0;
				OracleDbType oracleDbType = this.m_oracleDbType[i];
				switch (oracleDbType)
				{
				case OracleDbType.Double:
				case OracleDbType.Single:
					goto IL_79;
				case OracleDbType.Long:
				case OracleDbType.LongRaw:
				case OracleDbType.NClob:
				case OracleDbType.NChar:
				case (OracleDbType)118:
				case OracleDbType.NVarchar2:
				case OracleDbType.Raw:
				case OracleDbType.RefCursor:
					break;
				case OracleDbType.Int16:
					num = 2;
					break;
				case OracleDbType.Int32:
					num = 4;
					break;
				case OracleDbType.Int64:
					num = 8;
					break;
				case OracleDbType.IntervalDS:
					num = 20;
					break;
				case OracleDbType.IntervalYM:
					num = 8;
					break;
				case OracleDbType.TimeStamp:
				case OracleDbType.TimeStampLTZ:
				case OracleDbType.TimeStampTZ:
					num = 20;
					break;
				default:
					switch (oracleDbType)
					{
					case OracleDbType.BinaryDouble:
						goto IL_79;
					case OracleDbType.BinaryFloat:
						num = 4;
						break;
					}
					break;
				}
				IL_99:
				colDatOffset[i + 1] = (uint)((ulong)this.m_colDatOffset[i] + (ulong)((long)(num * rows)));
				colDatSize[i] = (uint)num;
				i++;
				continue;
				IL_79:
				num = 8;
				goto IL_99;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000AF68 File Offset: 0x00009F68
		public unsafe override bool Read()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::Read()\n"
				});
			}
			if (this.m_closed)
			{
				throw new InvalidOperationException();
			}
			if (this.m_bHasRowsCalledBeforeRead && this.m_currentClientRow == 0)
			{
				this.m_currentClientRow = 1;
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::Read()\n"
					});
				}
				return true;
			}
			int num = 0;
			if (this.m_pOpoMetValCtx != null && this.m_bHasUdtType)
			{
				if (this.m_currentRowUdtCache == null)
				{
					if (!this.m_fillReader)
					{
						this.m_currentRowUdtCache = new object[(int)this.m_pOpoMetValCtx->NoOfCols];
					}
					if (this.m_pOpoMetValCtx->bUdtInfoFetched == 0)
					{
						for (int i = 0; i < (int)this.m_pOpoMetValCtx->NoOfCols; i++)
						{
							OraType oraType = (OraType)this.m_pOpoMetValCtx->pColMetaVal[i].OraType;
							if ((oraType == OraType.ORA_NDT && this.m_pOpoMetValCtx->pColMetaVal[i].bIsXmlType == 0) || oraType == OraType.ORA_OCIRef)
							{
								OracleUdtDescriptor cachedOracleUdtDescriptor = this.GetCachedOracleUdtDescriptor(i);
								this.m_pOpoMetValCtx->pColMetaVal[i].pOpsDscCtx = cachedOracleUdtDescriptor.m_opsDscCtx;
								this.m_pOpoMetValCtx->pColMetaVal[i].ociTypeCode = (int)cachedOracleUdtDescriptor.GetUdtTypeCode();
								OracleDbType oracleDbType;
								if (cachedOracleUdtDescriptor.m_bSetOracleDbType)
								{
									oracleDbType = cachedOracleUdtDescriptor.m_oraDbType;
								}
								else
								{
									oracleDbType = cachedOracleUdtDescriptor.OracleDbType;
								}
								if (oracleDbType != OracleDbType.Array && oraType == OraType.ORA_OCIRef)
								{
									this.m_pOpoMetValCtx->pColMetaVal[i].ociTypeCode = 110;
								}
								this.m_pOpoMetValCtx->pColMetaVal[i].bIsFinalType = (int)cachedOracleUdtDescriptor.m_pOpoDscValCtx->bIsFinalType;
							}
						}
						this.m_pOpoMetValCtx->bUdtInfoFetched = 1;
					}
					if (this.m_pOpoDacValCtx->pOpoUdtValCtx == null)
					{
						try
						{
							OpsUdt.AllocValCtx(out this.m_pOpoDacValCtx->pOpoUdtValCtx, (int)this.m_pOpoMetValCtx->NoOfCols);
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
							throw;
						}
						finally
						{
							if (num != 0)
							{
								OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
							}
						}
					}
				}
				if (!this.m_fillReader)
				{
					for (int j = 0; j < (int)this.m_pOpoMetValCtx->NoOfCols; j++)
					{
						OracleRef oracleRef = this.m_currentRowUdtCache[j] as OracleRef;
						if (oracleRef != null && oracleRef.m_bNotRefByApp)
						{
							oracleRef.Dispose();
						}
						this.m_currentRowUdtCache[j] = null;
					}
				}
			}
			if (this.m_bSetLastFetch || (this.m_bCmdBehaviorSingleRow && this.m_currentClientRow > 0))
			{
				this.m_bLastFetch = true;
				this.m_bEOF = true;
				if (this.m_refCursor != null)
				{
					this.m_refCursor.m_state = OraRefCursorState.Closed;
				}
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDataReader::Read()\n"
					});
				}
				return false;
			}
			if (this.m_opsXmlTypeCtx != null)
			{
				try
				{
					int num2 = this.m_opsXmlTypeCtx.Length;
					for (int k = 0; k < num2; k++)
					{
						if (this.m_opsXmlTypeCtx[k] != IntPtr.Zero)
						{
							OpsXmlType.RelRef(this.m_opsConCtx, this.m_opsErrCtx, ref this.m_opsXmlTypeCtx[k], 1);
						}
					}
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
				}
				this.m_opsXmlTypeCtx = null;
			}
			if (this.m_currentClientRow == this.m_recordCount)
			{
				if (this.m_bLastFetch)
				{
					this.m_bEOF = true;
					if (this.m_refCursor != null)
					{
						this.m_refCursor.m_state = OraRefCursorState.Closed;
					}
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(1U, new string[]
						{
							" (EXIT)  OracleDataReader::Read()\n"
						});
					}
					return false;
				}
				if (this.m_currentClientRow == 0)
				{
					this.m_pOpoDacValCtx->FetchSize = this.m_fetchSize;
					if (this.m_fetchSize < this.m_rowSize || this.m_rowSize == 0L)
					{
						this.m_pOpoDacValCtx->FetchSize = 1L;
						this.m_pOpoSqlValCtx->FetchSize = 1L;
					}
					else
					{
						this.m_pOpoSqlValCtx->FetchSize = (this.m_pOpoDacValCtx->FetchSize = this.m_fetchSize / this.m_rowSize);
					}
					if (this.m_pOpoDacValCtx->FetchSize >= 25L && this.m_fetchSize != 131072L && this.m_bFetchSizePropertySet)
					{
						this.m_pOpoDacValCtx->FetchSize += 1L;
						this.m_pOpoSqlValCtx->FetchSize += 1L;
					}
				}
				this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
				try
				{
					num = OpsDac.Read(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsSqlCtx[this.m_currentResultIndex], ref this.m_opsDacCtx, this.m_pOpoSqlValCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx);
				}
				catch (Exception ex3)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex3);
					}
					throw;
				}
				finally
				{
					this.m_doneReadOne = true;
					if (num != 0)
					{
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this, true);
					}
				}
				this.m_recordCount = this.m_pOpoDacValCtx->RecordCount;
				if (this.m_currentClientRow == 0)
				{
					this.m_fetchArrayLocation = (long)this.m_pOpoSqlValCtx->FetchArrayLocation;
				}
				if ((long)this.m_recordCount < (long)this.m_currentClientRow + this.m_pOpoDacValCtx->FetchSize)
				{
					this.m_bLastFetch = true;
					if (this.m_recordCount == this.m_currentClientRow)
					{
						this.m_bEOF = true;
						if (this.m_refCursor != null)
						{
							this.m_refCursor.m_state = OraRefCursorState.Closed;
						}
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.Trace(1U, new string[]
							{
								" (EXIT)  OracleDataReader::Read()\n"
							});
						}
						return false;
					}
				}
				this.m_bBOF = false;
				this.m_rowLocation = 0L;
				if (!this.m_fillReader)
				{
					goto IL_82B;
				}
				try
				{
					try
					{
						if (this.m_fillReader && this.m_colDatOffset == null)
						{
							this.m_colDatOffset = new uint[this.m_fieldCount + 1];
							this.m_colDatSize = new uint[this.m_fieldCount];
							int num3 = (int)((long)this.m_pOpoDacValCtx->RecordCount % this.m_pOpoDacValCtx->FetchSize);
							if (num3 == 0)
							{
								num3 = (int)this.m_pOpoDacValCtx->FetchSize;
							}
							this.GetDataOffsetsAndSizes(this.m_colDatOffset, this.m_colDatSize, num3);
						}
						num = OpsDac.GetColumnValues(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx, this.m_oracleDbType, ref this.m_pColumnsDataBuffer, this.m_fetchArrayLocation, this.m_rowSize, this.m_colOffset, this.m_colIndOffset, this.m_colLenOffset, this.m_colDatOffset, this.m_colDatSize);
					}
					catch (Exception ex4)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex4);
						}
						throw;
					}
					goto IL_82B;
				}
				finally
				{
					if (num == 22053 || num == 22054)
					{
						throw new OverflowException(OracleTypeException.GetTypeMsg(num, new object[0]));
					}
					if (num != 0)
					{
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
				}
			}
			if (this.m_currentClientRow == 0)
			{
				this.m_bBOF = false;
				this.m_rowLocation = 0L;
				if (!this.m_fillReader)
				{
					goto IL_82B;
				}
				try
				{
					try
					{
						if (this.m_fillReader && this.m_colDatOffset == null)
						{
							this.m_colDatOffset = new uint[this.m_fieldCount + 1];
							this.m_colDatSize = new uint[this.m_fieldCount];
							int num4 = (int)((long)this.m_pOpoDacValCtx->RecordCount % this.m_pOpoDacValCtx->FetchSize);
							if (num4 == 0)
							{
								num4 = (int)this.m_pOpoDacValCtx->FetchSize;
							}
							this.GetDataOffsetsAndSizes(this.m_colDatOffset, this.m_colDatSize, num4);
						}
						num = OpsDac.GetColumnValues(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx, this.m_oracleDbType, ref this.m_pColumnsDataBuffer, this.m_fetchArrayLocation, this.m_rowSize, this.m_colOffset, this.m_colIndOffset, this.m_colLenOffset, this.m_colDatOffset, this.m_colDatSize);
					}
					catch (Exception ex5)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex5);
						}
						throw;
					}
					goto IL_82B;
				}
				finally
				{
					if (num == 22053 || num == 22054)
					{
						throw new OverflowException(OracleTypeException.GetTypeMsg(num, new object[0]));
					}
					if (num != 0)
					{
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
				}
			}
			this.m_rowLocation += this.m_rowSize;
			IL_82B:
			this.m_pOpoDacValCtx->CurrentClientRow++;
			this.m_currentClientRow++;
			this.m_hasRows = true;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::Read()\n"
				});
			}
			return true;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000B85C File Offset: 0x0000A85C
		private void CheckParameters(int bufferLength, int bufferOffset, int length)
		{
			if (bufferOffset < 0 || bufferOffset > bufferLength)
			{
				throw new ArgumentOutOfRangeException("bufferOffset");
			}
			if (bufferOffset + length > bufferLength)
			{
				throw new ArgumentOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_DATA_REQ, new string[0]));
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000B898 File Offset: 0x0000A898
		protected unsafe override void Dispose(bool disposing)
		{
			int num = 1;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::Dispose()\n"
				});
			}
			if (!this.m_disposed)
			{
				if (this.m_pOpoSqlValCtx != null && this.m_pOpoSqlValCtx->bPooledFetchArray == 1)
				{
					this.m_fetchArrayPooler.PutFetchArray((IntPtr)this.m_fetchArrayLocation);
				}
				lock (this.m_disposeSyncObj)
				{
					if (!this.m_disposed)
					{
						try
						{
							if (this.m_connection != null)
							{
								lock (this.m_connection.m_DataReaderList.SyncRoot)
								{
									this.m_connection.m_DataReaderList.Remove(this);
								}
							}
						}
						catch
						{
						}
						try
						{
							if (OracleConnection.IsAvailable && this.m_connection != null && this.m_connection.m_extProcEnv != null)
							{
								Monitor.Enter(this.m_connection.m_extProcEnv);
								if (!this.m_connection.m_extProcEnv.m_status)
								{
									num = 0;
								}
							}
							if (this.m_opsXmlTypeCtx != null)
							{
								try
								{
									int num2 = this.m_opsXmlTypeCtx.Length;
									for (int i = 0; i < num2; i++)
									{
										if (this.m_opsXmlTypeCtx[i] != IntPtr.Zero)
										{
											OpsXmlType.RelRef(this.m_opsConCtx, this.m_opsErrCtx, ref this.m_opsXmlTypeCtx[i], num);
											this.m_opsXmlTypeCtx[i] = IntPtr.Zero;
										}
									}
								}
								catch (Exception ex)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex);
									}
								}
								this.m_opsXmlTypeCtx = null;
							}
							if (this.m_connection.m_opoConCtx.m_bSelfTuning && !this.m_connection.m_disposed && null != this.m_pOpoSqlValCtx && 1 == this.m_pOpoSqlValCtx->AddToStmtCache && this.m_commandText != null && !OracleTuningAgent.bHighMemoryAlertFlag)
							{
								this.m_connection.AcceptStatementData(this.m_commandText);
							}
							if (!this.m_noMoreResults)
							{
								if (this.m_opsSqlCtx == null)
								{
									try
									{
										OpsDac.Dispose(this.m_opsConCtx, this.m_opsErrCtx, IntPtr.Zero, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx, this.m_pOpoSqlValCtx, num);
										this.m_opsErrCtx = IntPtr.Zero;
										goto IL_4D0;
									}
									catch (Exception ex2)
									{
										if (OraTrace.m_TraceLevel != 0U)
										{
											OraTrace.TraceExceptionInfo(ex2);
										}
										goto IL_4D0;
									}
								}
								try
								{
									if (this.m_freeOpsSqlCtx == 1)
									{
										OpsDac.Dispose(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsSqlCtx[this.m_currentResultIndex], this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx, this.m_pOpoSqlValCtx, num);
									}
									else
									{
										OpsDac.Dispose(this.m_opsConCtx, this.m_opsErrCtx, IntPtr.Zero, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx, this.m_pOpoSqlValCtx, num);
									}
									this.m_opsErrCtx = IntPtr.Zero;
									this.m_opsSqlCtx[this.m_currentResultIndex] = IntPtr.Zero;
								}
								catch (Exception ex3)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex3);
									}
								}
								try
								{
									if (this.m_opsSqlCtx[this.m_currentResultIndex] != IntPtr.Zero)
									{
										try
										{
											if (this.m_freeOpsSqlCtx == 1 && num == 1)
											{
												if (this.m_pOpoSqlValCtx->CommandType == 1)
												{
													if (this.m_pOpoSqlValCtx->AddToStmtCache == 0)
													{
														OpsSql.FreeCtx(ref this.m_opsSqlCtx[this.m_currentResultIndex], this.m_opsErrCtx, 0);
													}
													else
													{
														OpsSql.FreeCtx(ref this.m_opsSqlCtx[this.m_currentResultIndex], this.m_opsErrCtx, 1);
													}
												}
												else
												{
													OpsSql.FreeCtx(ref this.m_opsSqlCtx[this.m_currentResultIndex], this.m_opsErrCtx, 0);
												}
											}
										}
										catch (Exception ex4)
										{
											if (OraTrace.m_TraceLevel != 0U)
											{
												OraTrace.TraceExceptionInfo(ex4);
											}
										}
										this.m_opsSqlCtx[this.m_currentResultIndex] = IntPtr.Zero;
									}
									int j = 0;
									this.m_currentResultIndex++;
									for (j = this.m_currentResultIndex; j < this.m_resultCount; j++)
									{
										if (this.m_opsSqlCtx[j] != IntPtr.Zero)
										{
											try
											{
												if (num == 1)
												{
													if (this.m_pOpoSqlValCtx->CommandType == 1)
													{
														if (this.m_pOpoSqlValCtx->AddToStmtCache == 0)
														{
															OpsSql.FreeCtx(ref this.m_opsSqlCtx[j], this.m_opsErrCtx, 0);
														}
														else
														{
															OpsSql.FreeCtx(ref this.m_opsSqlCtx[j], this.m_opsErrCtx, 1);
														}
													}
													else
													{
														OpsSql.FreeCtx(ref this.m_opsSqlCtx[j], this.m_opsErrCtx, 0);
													}
												}
											}
											catch (Exception ex5)
											{
												if (OraTrace.m_TraceLevel != 0U)
												{
													OraTrace.TraceExceptionInfo(ex5);
												}
											}
											this.m_opsSqlCtx[j] = IntPtr.Zero;
										}
									}
								}
								catch
								{
								}
								IL_4D0:
								if (this.m_opsErrCtx != IntPtr.Zero)
								{
									try
									{
										if (num == 1)
										{
											OpsErr.FreeCtx(ref this.m_opsErrCtx);
										}
									}
									catch (Exception ex6)
									{
										if (OraTrace.m_TraceLevel != 0U)
										{
											OraTrace.TraceExceptionInfo(ex6);
										}
									}
									this.m_opsErrCtx = IntPtr.Zero;
								}
								this.m_pOpoSqlValCtx = null;
								this.m_pOpoDacValCtx = null;
								this.m_pOpoMetValCtx = null;
								this.m_opsErrCtx = IntPtr.Zero;
								this.m_opsDacCtx = IntPtr.Zero;
								this.m_noMoreResults = true;
							}
						}
						finally
						{
							if (OracleConnection.IsAvailable && this.m_connection != null && this.m_connection.m_extProcEnv != null)
							{
								Monitor.Exit(this.m_connection.m_extProcEnv);
							}
						}
						try
						{
							OpsCon.RelRef(ref this.m_opsConCtx);
						}
						catch (Exception ex7)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex7);
							}
						}
						if (this.m_refCursor != null)
						{
							try
							{
								this.m_refCursor.m_state = OraRefCursorState.Closed;
							}
							catch
							{
							}
						}
						if ((this.m_commandBehavior & CommandBehavior.CloseConnection) == CommandBehavior.CloseConnection)
						{
							try
							{
								this.m_connection.Close();
							}
							catch
							{
							}
						}
						if (disposing)
						{
							this.m_closed = true;
							this.m_connection = null;
							this.m_opsSqlCtx = null;
							this.m_colMetaRef = null;
							this.m_safeMapping = null;
							this.m_pkFetched = false;
							this.m_doneMarshalAndStoreMetaData = false;
							this.m_expectedColumnTypes = null;
							if (this.m_currentRowUdtCache != null)
							{
								for (int k = 0; k < this.m_currentRowUdtCache.Length; k++)
								{
									if (this.m_currentRowUdtCache[k] != null)
									{
										OracleRef oracleRef = this.m_currentRowUdtCache[k] as OracleRef;
										if (oracleRef != null && oracleRef.m_bNotRefByApp)
										{
											oracleRef.Dispose();
										}
										this.m_currentRowUdtCache[k] = null;
									}
								}
								this.m_currentRowUdtCache = null;
							}
							this.m_udtDescriptorCache = null;
							if (this.m_dataTable != null)
							{
								try
								{
									this.m_dataTable.Dispose();
								}
								catch
								{
								}
								this.m_dataTable = null;
							}
							this.m_dataTableList = null;
							this.m_commandText = null;
							this.m_metaData = null;
						}
						this.m_disposed = true;
					}
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::Dispose()\n"
				});
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000C0EC File Offset: 0x0000B0EC
		private unsafe void GetColMetaRef(bool bFetchMoreMetaIfRequired, bool bLocalParsed)
		{
			int num = 0;
			int num2 = 0;
			bool flag = bLocalParsed;
			if (this.m_pOpoMetValCtx == null || this.m_pOpoMetValCtx->pOpoMetRefCtx == IntPtr.Zero)
			{
				return;
			}
			num2 = (int)this.m_pOpoMetValCtx->NoOfCols;
			if (num2 == 0)
			{
				return;
			}
			try
			{
				if (bFetchMoreMetaIfRequired && this.m_pOpoMetValCtx->bStmtParsed != 1 && this.m_pOpoSqlValCtx->CommandType == 1)
				{
					num = OpsMet.GetSchemaMetaData(this.m_opsConCtx, this.m_opsErrCtx, this.m_pOpoMetValCtx, this.m_pOpoSqlValCtx->AddRowid, this.m_pOpoSqlValCtx->AddToStmtCache);
					flag = true;
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (this.m_metaData != null && this.m_colMetaRef == null)
			{
				if (!this.m_addRowid)
				{
					this.m_colMetaRef = this.m_metaData.m_colMetaRef;
				}
				else
				{
					this.m_colMetaRef = this.m_metaData.m_colMetaRefWRowid;
				}
			}
			if (this.m_colMetaRef != null && !flag)
			{
				return;
			}
			this.m_colMetaRef = new ColMetaRef[num2];
			OpoMetRefCtx opoMetRefCtx = new OpoMetRefCtx();
			Marshal.PtrToStructure(this.m_pOpoMetValCtx->pOpoMetRefCtx, opoMetRefCtx);
			IntPtr intPtr = opoMetRefCtx.pColMetaRef;
			int num3 = Marshal.SizeOf(typeof(ColMetaRef));
			for (int i = 0; i < num2; i++)
			{
				this.m_colMetaRef[i] = new ColMetaRef();
				Marshal.PtrToStructure(intPtr, this.m_colMetaRef[i]);
				intPtr = (IntPtr)((long)intPtr + (long)num3);
			}
			if (this.m_metaData != null)
			{
				if (!this.m_addRowid)
				{
					this.m_metaData.m_colMetaRef = this.m_colMetaRef;
				}
				else
				{
					this.m_metaData.m_colMetaRefWRowid = this.m_colMetaRef;
				}
			}
			if (bLocalParsed)
			{
				this.m_doneMarshalAndStoreMetaData = true;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000C2CC File Offset: 0x0000B2CC
		private OracleDbType GetOraDbType(int i)
		{
			return this.m_oracleDbType[i];
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000C2D8 File Offset: 0x0000B2D8
		private unsafe OracleDbType GetOraDbTypeEx(int i)
		{
			if (i >= this.m_fieldCount || i < 0)
			{
				throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
			}
			bool flag = true;
			if (this.m_pOpoMetValCtx->pColMetaVal[i].CharSetForm != 2)
			{
				flag = false;
			}
			OraType oraType = this.m_oraType[i];
			if (oraType != OraType.ORA_NDT || this.m_pOpoMetValCtx->pColMetaVal[i].bIsXmlType != 0)
			{
				OracleDbType oracleDbType = (OracleDbType)OracleTypeMapper.m_OraToOraDb[oraType];
				OracleDbType oracleDbType2 = oracleDbType;
				switch (oracleDbType2)
				{
				case OracleDbType.Char:
					if (flag)
					{
						oracleDbType = OracleDbType.NChar;
					}
					break;
				case OracleDbType.Clob:
					if (flag)
					{
						oracleDbType = OracleDbType.NClob;
					}
					break;
				case OracleDbType.Date:
					break;
				case OracleDbType.Decimal:
				{
					int scale = (int)this.m_pOpoMetValCtx->pColMetaVal[i].Scale;
					int precision = (int)this.m_pOpoMetValCtx->pColMetaVal[i].Precision;
					if (scale <= 0 && precision - scale < 5)
					{
						oracleDbType = OracleDbType.Int16;
					}
					else if (scale <= 0 && precision - scale < 10)
					{
						oracleDbType = OracleDbType.Int32;
					}
					else if (scale <= 0 && precision - scale < 19)
					{
						oracleDbType = OracleDbType.Int64;
					}
					else if (precision < 8 && ((scale <= 0 && precision - scale <= 38) || (scale > 0 && scale <= 44)))
					{
						oracleDbType = OracleDbType.Single;
					}
					else if (precision < 16)
					{
						oracleDbType = OracleDbType.Double;
					}
					break;
				}
				default:
					if (oracleDbType2 == OracleDbType.Varchar2)
					{
						if (flag)
						{
							oracleDbType = OracleDbType.NVarchar2;
						}
					}
					break;
				}
				return oracleDbType;
			}
			OracleUdtDescriptor cachedOracleUdtDescriptor = this.GetCachedOracleUdtDescriptor(i);
			if (cachedOracleUdtDescriptor.m_bSetOracleDbType)
			{
				return cachedOracleUdtDescriptor.m_oraDbType;
			}
			return cachedOracleUdtDescriptor.OracleDbType;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000C478 File Offset: 0x0000B478
		public override IEnumerator GetEnumerator()
		{
			bool closeReader = false;
			if ((this.m_commandBehavior & CommandBehavior.CloseConnection) == CommandBehavior.CloseConnection)
			{
				closeReader = true;
			}
			return new DbEnumerator(this, closeReader);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000C4A0 File Offset: 0x0000B4A0
		public override Type GetProviderSpecificFieldType(int ordinal)
		{
			Type result = null;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetProviderSpecificFieldType()\n"
				});
			}
			switch (this.m_oracleDbType[ordinal])
			{
			case OracleDbType.BFile:
				result = ODPType.OraBFile;
				break;
			case OracleDbType.Blob:
				result = ODPType.OraBlob;
				break;
			case OracleDbType.Byte:
			case OracleDbType.Decimal:
			case OracleDbType.Double:
			case OracleDbType.Int16:
			case OracleDbType.Int32:
			case OracleDbType.Int64:
			case OracleDbType.Single:
			case OracleDbType.BinaryDouble:
			case OracleDbType.BinaryFloat:
				result = ODPType.OraDecimal;
				break;
			case OracleDbType.Char:
			case OracleDbType.Long:
			case OracleDbType.NChar:
			case OracleDbType.NVarchar2:
			case OracleDbType.Varchar2:
				result = ODPType.OraString;
				break;
			case OracleDbType.Clob:
			case OracleDbType.NClob:
				result = ODPType.OraClob;
				break;
			case OracleDbType.Date:
				result = ODPType.OraDate;
				break;
			case OracleDbType.LongRaw:
			case OracleDbType.Raw:
				result = ODPType.OraBinary;
				break;
			case OracleDbType.IntervalDS:
				result = ODPType.OraIntervalDS;
				break;
			case OracleDbType.IntervalYM:
				result = ODPType.OraIntervalYM;
				break;
			case OracleDbType.RefCursor:
				result = ODPType.OraRefCursor;
				break;
			case OracleDbType.TimeStamp:
				result = ODPType.OraTimeStamp;
				break;
			case OracleDbType.TimeStampLTZ:
				result = ODPType.OraTimeStampLTZ;
				break;
			case OracleDbType.TimeStampTZ:
				result = ODPType.OraTimeStampTZ;
				break;
			case OracleDbType.XmlType:
				result = ODPType.OraXmlType;
				break;
			case OracleDbType.Array:
			case OracleDbType.Object:
				result = this.GetFieldType(ordinal);
				break;
			case OracleDbType.Ref:
				result = ODPType.OraRef;
				break;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetProviderSpecificFieldType()\n"
				});
			}
			return result;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000C608 File Offset: 0x0000B608
		public override object GetProviderSpecificValue(int ordinal)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetProviderSpecificValue()\n"
				});
			}
			object oracleValue = this.GetOracleValue(ordinal);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetProviderSpecificValue()\n"
				});
			}
			return oracleValue;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000C658 File Offset: 0x0000B658
		public override int GetProviderSpecificValues(object[] values)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDataReader::GetProviderSpecificValues()\n"
				});
			}
			int oracleValues = this.GetOracleValues(values);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDataReader::GetProviderSpecificValues()\n"
				});
			}
			return oracleValues;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000C6A8 File Offset: 0x0000B6A8
		private bool IsCorruptible(OraType oraType)
		{
			if (oraType <= OraType.ORA_DATE)
			{
				if (oraType != OraType.ORA_NUMBER && oraType != OraType.ORA_DATE)
				{
					return false;
				}
			}
			else
			{
				switch (oraType)
				{
				case OraType.ORA_TIMESTAMP:
				case OraType.ORA_TIMESTAMP_TZ:
				case OraType.ORA_INTERVAL_DS:
					break;
				case OraType.ORA_INTERVAL_YM:
					return false;
				default:
					if (oraType != OraType.ORA_TIMESTAMP_LTZ)
					{
						return false;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000C6F0 File Offset: 0x0000B6F0
		private unsafe OracleUdtDescriptor GetCachedOracleUdtDescriptor(int index)
		{
			if (this.m_udtDescriptorCache == null)
			{
				this.m_udtDescriptorCache = new OracleUdtDescriptor[(int)this.m_pOpoMetValCtx->NoOfCols];
			}
			if (this.m_udtDescriptorCache[index] == null)
			{
				if (this.m_pOpoMetValCtx != null && this.m_colMetaRef == null)
				{
					this.GetColMetaRef(false, false);
				}
				if (this.m_colMetaRef[index].pUdtTypeName != null)
				{
					this.m_udtDescriptorCache[index] = OracleUdtDescriptor.GetOracleUdtDescriptor(this.m_connection, this.m_colMetaRef[index].pUdtSchemaName, this.m_colMetaRef[index].pUdtTypeName);
					if (this.m_udtDescriptorCache[index] != null)
					{
						this.m_udtDescriptorCache[index].GetMetaDataTable();
					}
				}
			}
			return this.m_udtDescriptorCache[index];
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000C79C File Offset: 0x0000B79C
		internal unsafe object GetCustomObject(int i)
		{
			if (this.m_external)
			{
				if (this.m_closed || this.m_bBOF || this.m_bEOF)
				{
					throw new InvalidOperationException();
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
			}
			int num = 0;
			OraType oraType = (OraType)this.m_pOpoMetValCtx->pColMetaVal[i].OraType;
			if (oraType != OraType.ORA_NDT)
			{
				throw new InvalidCastException();
			}
			this.m_pOpoDacValCtx->CurrentClientRow = this.m_currentClientRow;
			this.m_pOpoDacValCtx->Ordinal = i;
			this.m_pOpoDacValCtx->FieldOffset = 0L;
			this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
			OracleUdtDescriptor oracleUdtDescriptor;
			if (this.m_pOpoMetValCtx->pColMetaVal[i].bIsFinalType == 0)
			{
				try
				{
					num = OpsDac.GetType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx, 0);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				finally
				{
					if (num != 0)
					{
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
				}
				if (this.m_pOpoDacValCtx->Indicator == 0)
				{
					bool flag;
					oracleUdtDescriptor = OracleUdtDescriptor.GetOracleUdtDescriptor(this.m_connection, this.m_pOpoDacValCtx->pTDO, false, out flag);
					if (flag)
					{
						try
						{
							OpsDsc.UnpinTDO(this.m_opsConCtx, this.m_pOpoDacValCtx->pTDO);
						}
						catch (Exception ex2)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex2);
							}
						}
					}
					this.m_pOpoDacValCtx->pTDO = IntPtr.Zero;
				}
				else
				{
					oracleUdtDescriptor = this.GetCachedOracleUdtDescriptor(i);
				}
			}
			else
			{
				oracleUdtDescriptor = this.GetCachedOracleUdtDescriptor(i);
			}
			if (OracleConnection.s_bIsOdtConnection)
			{
				this.m_pOpoDacValCtx->pOpoUdtValCtx[i].bIsOdtConnection = 1;
				if (this.m_pOpoMetValCtx->pColMetaVal[i].bIsFinalType == 0)
				{
					oracleUdtDescriptor.GetMetaDataTable();
				}
			}
			else
			{
				this.m_pOpoDacValCtx->pOpoUdtValCtx[i].bIsOdtConnection = 0;
				if (oracleUdtDescriptor.m_customTypeFactory == null)
				{
					object factory = OracleUdt.GetFactory(oracleUdtDescriptor);
					if (factory != null)
					{
						oracleUdtDescriptor.DescribeCustomType(factory);
					}
				}
			}
			this.m_pOpoDacValCtx->pOpoUdtValCtx[i].pTDO = oracleUdtDescriptor.m_opsDscCtx;
			this.m_pOpoDacValCtx->pOpoUdtValCtx[i].pOpsErrCtx = this.m_opsErrCtx;
			this.m_pOpoDacValCtx->pOpoUdtValCtx[i].pOpoDscValCtx = oracleUdtDescriptor.m_pOpoDscValCtx;
			this.m_pOpoDacValCtx->pOpoUdtValCtx[i].ppRefTDO = this.m_pOpoDacValCtx->ppRefTDO;
			try
			{
				if (this.m_pOpoMetValCtx->pColMetaVal[i].bIsFinalType == 1)
				{
					num = OpsDac.GetType(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDacCtx, this.m_pOpoMetValCtx, this.m_pOpoDacValCtx, 0);
				}
				else if (this.m_pOpoDacValCtx->Indicator == 0)
				{
					this.m_pOpoDacValCtx->pOpoUdtValCtx[i].pUDT = this.m_pOpoDacValCtx->pBuffer;
					this.m_pOpoDacValCtx->pOpoUdtValCtx[i].pNullStruct = this.m_pOpoDacValCtx->pUdtNullStruct;
					num = OpsUdt.GetObj(this.m_opsConCtx, this.m_pOpoDacValCtx->pOpoUdtValCtx + i);
				}
			}
			catch (Exception ex3)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex3);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			object obj;
			if (this.m_pOpoDacValCtx->Indicator != -1)
			{
				this.m_pOpoDacValCtx->pBuffer = IntPtr.Zero;
				if (OracleConnection.s_bIsOdtConnection)
				{
					if (oracleUdtDescriptor.OracleDbType == OracleDbType.Object)
					{
						OracleUdtWrapper oracleUdtWrapper = new OracleUdtWrapper();
						oracleUdtWrapper.m_udtData = new object[oracleUdtDescriptor.AttributeCount];
						oracleUdtWrapper.m_udtStatusArray = new OracleUdtStatus[oracleUdtDescriptor.AttributeCount];
						for (int j = 0; j < oracleUdtDescriptor.AttributeCount; j++)
						{
							OciTypeCode typeCode = (OciTypeCode)oracleUdtDescriptor.m_pOpoDscValCtx->pAttrMetaVals[j].TypeCode;
							if (typeCode == OciTypeCode.OBJECT || typeCode == OciTypeCode.NAMEDCOLLECTION)
							{
								OracleUdtWrapper oracleUdtWrapper2 = new OracleUdtWrapper();
								oracleUdtWrapper2.m_udtDescriptor = oracleUdtDescriptor.GetObjAttrUdtDescriptor(j);
								((object[])oracleUdtWrapper.m_udtData)[j] = oracleUdtWrapper2;
								if (this.m_pOpoDacValCtx->pOpoUdtValCtx[i].pOpoUdtValCtx[j].bIsNull == 1)
								{
									oracleUdtWrapper.m_udtStatusArray[j] = OracleUdtStatus.Null;
								}
								else
								{
									oracleUdtWrapper.m_udtStatusArray[j] = OracleUdtStatus.NotNull;
								}
							}
							else
							{
								object value = OracleUdt.GetValue(this.m_connection, (IntPtr)((void*)(this.m_pOpoDacValCtx->pOpoUdtValCtx + i)), j);
								((object[])oracleUdtWrapper.m_udtData)[j] = value;
								if (value == null || value == DBNull.Value || (value is INullable && ((INullable)value).IsNull))
								{
									oracleUdtWrapper.m_udtStatusArray[j] = OracleUdtStatus.Null;
								}
								else
								{
									oracleUdtWrapper.m_udtStatusArray[j] = OracleUdtStatus.NotNull;
								}
							}
						}
						oracleUdtWrapper.m_udtDescriptor = oracleUdtDescriptor;
						obj = oracleUdtWrapper;
					}
					else
					{
						OracleUdtWrapper oracleUdtWrapper3 = new OracleUdtWrapper();
						int numOfArrElems = this.m_pOpoDacValCtx->pOpoUdtValCtx[i].NumOfArrElems;
						oracleUdtWrapper3.m_udtData = new object[numOfArrElems];
						oracleUdtWrapper3.m_udtStatusArray = new OracleUdtStatus[numOfArrElems];
						OciTypeCode typeCode2 = (OciTypeCode)oracleUdtDescriptor.m_pOpoDscValCtx->pAttrMetaVals->TypeCode;
						for (int k = 0; k < numOfArrElems; k++)
						{
							if (typeCode2 == OciTypeCode.OBJECT || typeCode2 == OciTypeCode.NAMEDCOLLECTION)
							{
								OracleUdtWrapper oracleUdtWrapper4 = new OracleUdtWrapper();
								oracleUdtWrapper4.m_udtDescriptor = oracleUdtDescriptor.GetArrElemUdtDescriptor();
								((object[])oracleUdtWrapper3.m_udtData)[k] = oracleUdtWrapper4;
								if (this.m_pOpoDacValCtx->pOpoUdtValCtx[i].pOpoUdtValCtx[k].bIsNull == 1)
								{
									oracleUdtWrapper3.m_udtStatusArray[k] = OracleUdtStatus.Null;
								}
								else
								{
									oracleUdtWrapper3.m_udtStatusArray[k] = OracleUdtStatus.NotNull;
								}
							}
							else
							{
								OracleUdtStatus oracleUdtStatus;
								object obj2;
								object data = OracleUdt.GetData(this.m_connection, (IntPtr)((void*)(this.m_pOpoDacValCtx->pOpoUdtValCtx + i)), k, out oracleUdtStatus, out obj2);
								((object[])oracleUdtWrapper3.m_udtData)[k] = data;
								if (data == null || data == DBNull.Value || (data is INullable && ((INullable)data).IsNull))
								{
									oracleUdtWrapper3.m_udtStatusArray[k] = OracleUdtStatus.Null;
								}
								else
								{
									oracleUdtWrapper3.m_udtStatusArray[k] = OracleUdtStatus.NotNull;
								}
							}
						}
						oracleUdtWrapper3.m_udtDescriptor = oracleUdtDescriptor;
						obj = oracleUdtWrapper3;
					}
				}
				else
				{
					object customTypeFactory = oracleUdtDescriptor.m_customTypeFactory;
					if (oracleUdtDescriptor.m_pOpoDscValCtx->bIsArrayType == 0)
					{
						obj = ((IOracleCustomTypeFactory)oracleUdtDescriptor.m_customTypeFactory).CreateObject();
						((IOracleCustomType)obj).ToCustomObject(this.m_connection, (IntPtr)((void*)(this.m_pOpoDacValCtx->pOpoUdtValCtx + i)));
					}
					else
					{
						this.m_pOpoDacValCtx->pOpoUdtValCtx[i].bIgnoreElemStatus = 1;
						OracleUdtStatus oracleUdtStatus2;
						object obj3;
						obj = OracleUdt.GetArrData(this.m_connection, (IntPtr)((void*)(this.m_pOpoDacValCtx->pOpoUdtValCtx + i)), out oracleUdtStatus2, out obj3);
					}
				}
				GC.KeepAlive(oracleUdtDescriptor);
				return obj;
			}
			object customTypeFactory2 = oracleUdtDescriptor.m_customTypeFactory;
			if (oracleUdtDescriptor.m_pOpoDscValCtx->bIsArrayType != 0 || !(customTypeFactory2 is IOracleCustomTypeFactory))
			{
				return null;
			}
			obj = ((IOracleCustomTypeFactory)oracleUdtDescriptor.m_customTypeFactory).CreateObject();
			Type type = obj.GetType();
			PropertyInfo property = type.GetProperty("Null");
			if (property != null)
			{
				return property.GetValue(null, null);
			}
			throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(-2902, new string[]
			{
				"'" + type.FullName + "'",
				"'Null'"
			}));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000CFC0 File Offset: 0x0000BFC0
		internal unsafe DataTable GetMinSchemaTable()
		{
			if (this.m_pOpoMetValCtx == null)
			{
				return null;
			}
			DataTable dataTable = null;
			if (this.m_pOpoSqlValCtx->CommandType != 1)
			{
				if (this.m_refCursor != null)
				{
					RefCursorInfo refCursorInfo = this.m_refCursor.m_refCursorInfo;
					if (refCursorInfo != null)
					{
						dataTable = refCursorInfo.columnInfo;
					}
				}
				else
				{
					StoredProcedureInfo storedProcInfo = RegAndConfigRdr.GetStoredProcInfo(this.m_storedProcName);
					if (storedProcInfo != null && storedProcInfo.refCursors.Count > 0)
					{
						RefCursorInfo refCursorInfo2 = (RefCursorInfo)storedProcInfo.refCursors[this.m_currentResultIndex];
						if (refCursorInfo2 != null)
						{
							dataTable = refCursorInfo2.columnInfo;
						}
					}
				}
			}
			DataTable dataTable2 = new DataTable("MinSchemaTable");
			this.GetColMetaRef(true, false);
			dataTable2.MinimumCapacity = this.m_fieldCount;
			if (this.m_pOpoSqlValCtx->CommandType != 1)
			{
				dataTable2.ExtendedProperties["REFCursorName"] = ((this.m_currentResultIndex == 0) ? "REFCursor" : ("REFCursor" + this.m_currentResultIndex));
			}
			dataTable2.Columns.Add("ColumnName", typeof(string));
			dataTable2.Columns.Add("BaseColumnName", typeof(string));
			dataTable2.Columns.Add("BaseTableName", typeof(string));
			dataTable2.Columns.Add("OraDbType", typeof(OracleDbType));
			dataTable2.Columns.Add("BaseSchemaName", typeof(string));
			dataTable2.Columns.Add("UdtTypeName", typeof(string));
			for (int i = 0; i < this.m_fieldCount; i++)
			{
				DataRow dataRow = dataTable2.NewRow();
				if (this.m_pOpoSqlValCtx->CommandType != 1 && dataTable != null)
				{
					dataRow[0] = dataTable.Rows[i]["ColumnName"];
					dataRow[1] = dataTable.Rows[i]["BaseColumnName"];
					dataRow[2] = dataTable.Rows[i]["BaseTableName"];
					dataRow[3] = dataTable.Rows[i]["ProviderType"];
					dataRow[4] = dataTable.Rows[i]["BaseSchemaName"];
					dataRow[5] = dataTable.Rows[i]["UdtTypeName"];
				}
				else
				{
					dataRow[0] = this.m_colMetaRef[i].pColAlias;
					dataRow[1] = this.m_colMetaRef[i].pColName;
					dataRow[2] = this.m_colMetaRef[i].pTabName;
					dataRow[3] = this.m_oracleDbType[i];
					dataRow[4] = this.m_colMetaRef[i].pSchemaName;
					OraType oraType = (OraType)this.m_pOpoMetValCtx->pColMetaVal[i].OraType;
					if ((oraType == OraType.ORA_NDT && this.m_pOpoMetValCtx->pColMetaVal[i].bIsXmlType == 0) || oraType == OraType.ORA_OCIRef)
					{
						dataRow[5] = this.GetCachedOracleUdtDescriptor(i).UdtTypeName;
					}
				}
				dataTable2.Rows.Add(dataRow);
			}
			dataTable2.AcceptChanges();
			return dataTable2;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000D328 File Offset: 0x0000C328
		internal unsafe OracleDataReader(OracleConnection connection, IntPtr[] opsSqlCtx, IntPtr opsDacCtx, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, OpoDacValCtx* pOpoDacValCtx, MetaData metaData, int resultCount, CommandBehavior commandBehavior, Hashtable safeMapping, string commandText, int freeOpsSqlCtx, bool bFetchSizePropertySet)
		{
			int num = 0;
			this.m_bBOF = true;
			this.m_external = true;
			this.m_safeMapping = safeMapping;
			this.m_bFetchSizePropertySet = bFetchSizePropertySet;
			this.m_resultCount = resultCount;
			this.m_commandBehavior = commandBehavior;
			this.m_connection = connection;
			this.m_fetchArrayPooler = connection.m_opoConCtx.m_fetchArrayPooler;
			this.m_conSignature = this.m_connection.m_conSignature;
			this.m_opsSqlCtx = opsSqlCtx;
			this.m_opsDacCtx = opsDacCtx;
			this.m_opsErrCtx = opsErrCtx;
			this.m_pOpoSqlValCtx = pOpoSqlValCtx;
			this.m_fetchSize = this.m_pOpoSqlValCtx->FetchSize;
			this.m_commandText = commandText;
			this.m_freeOpsSqlCtx = freeOpsSqlCtx;
			if (this.m_pOpoSqlValCtx->AddRowid == 1)
			{
				this.m_addRowid = true;
			}
			if (this.m_pOpoSqlValCtx->CommandType == 4 || this.m_pOpoSqlValCtx->CommandType == 2 || this.m_pOpoSqlValCtx->CommandType == 3)
			{
				this.m_recordsAffected = this.m_pOpoSqlValCtx->RowsAffected;
			}
			else
			{
				this.m_recordsAffected = -1;
			}
			this.m_opsConCtx = this.m_connection.m_opoConCtx.opsConCtx;
			if (this.m_opsConCtx == IntPtr.Zero)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			try
			{
				int num2 = OpsCon.AddRef(this.m_opsConCtx);
				if (num2 <= 1)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				GC.SuppressFinalize(this);
				throw;
			}
			if (this.m_opsErrCtx == IntPtr.Zero)
			{
				try
				{
					num = OpsErr.AllocCtx(ref this.m_opsErrCtx, this.m_opsConCtx);
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
					num = ErrRes.INT_ERR;
					GC.SuppressFinalize(this);
					throw;
				}
				finally
				{
					if (num != 0)
					{
						try
						{
							OpsCon.RelRef(ref this.m_opsConCtx);
						}
						catch (Exception ex3)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex3);
							}
						}
						if (num != ErrRes.INT_ERR)
						{
							GC.SuppressFinalize(this);
							OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
						}
					}
				}
			}
			this.m_metaData = metaData;
			if (this.m_metaData != null)
			{
				if (!this.m_addRowid)
				{
					this.m_pOpoMetValCtx = this.m_metaData.m_pOpoMetValCtx;
				}
				else
				{
					this.m_pOpoMetValCtx = this.m_metaData.m_pOpoMetValCtxWRowid;
				}
				try
				{
					OpsMet.AddRef(this.m_pOpoMetValCtx);
				}
				catch (Exception ex4)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex4);
					}
					num = ErrRes.INT_ERR;
					GC.SuppressFinalize(this);
					throw;
				}
				finally
				{
					if (num != 0)
					{
						try
						{
							OpsCon.RelRef(ref this.m_opsConCtx);
						}
						catch (Exception ex5)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex5);
							}
						}
						if (num != ErrRes.INT_ERR)
						{
							GC.SuppressFinalize(this);
							OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
						}
					}
				}
			}
			if (this.m_opsSqlCtx != null && this.m_pOpoMetValCtx == null && (this.m_pOpoSqlValCtx->CommandType == 8 || this.m_pOpoSqlValCtx->CommandType == 9) && this.m_opsSqlCtx[this.m_currentResultIndex] != IntPtr.Zero)
			{
				try
				{
					num = OpsMet.GetValCtx(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsSqlCtx[this.m_currentResultIndex], this.m_pOpoSqlValCtx, ref this.m_pOpoMetValCtx);
				}
				catch (Exception ex6)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex6);
					}
					num = ErrRes.INT_ERR;
					throw;
				}
				finally
				{
					if (num != 0)
					{
						try
						{
							OpsCon.RelRef(ref this.m_opsConCtx);
						}
						catch (Exception ex7)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex7);
							}
						}
						try
						{
							OpsMet.RelRef(this.m_pOpoMetValCtx);
						}
						catch (Exception ex8)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex8);
							}
						}
						if (num != ErrRes.INT_ERR)
						{
							OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
						}
					}
				}
			}
			if (this.m_pOpoMetValCtx != null)
			{
				this.m_rowSize = (long)this.m_pOpoMetValCtx->pColMetaVal[this.m_pOpoMetValCtx->NoOfCols - 1].Offset;
				this.m_fieldCount = (int)(this.m_pOpoMetValCtx->NoOfCols - this.m_pOpoMetValCtx->NoOfHiddenCols);
				this.m_bHasUdtType = (this.m_pOpoMetValCtx->bHasUdtType == 1);
			}
			try
			{
				if (this.m_opsDacCtx != IntPtr.Zero && pOpoDacValCtx != null)
				{
					this.m_pOpoDacValCtx = pOpoDacValCtx;
				}
				else
				{
					num = OpsDac.AllocValCtx(ref this.m_pOpoDacValCtx);
				}
			}
			catch (Exception ex9)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex9);
				}
				num = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num != 0)
				{
					try
					{
						OpsCon.RelRef(ref this.m_opsConCtx);
					}
					catch (Exception ex10)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex10);
						}
					}
					try
					{
						OpsMet.RelRef(this.m_pOpoMetValCtx);
					}
					catch (Exception ex11)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex11);
						}
					}
					if (num != ErrRes.INT_ERR)
					{
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
				}
			}
			this.m_pOpoDacValCtx->InitialLongFS = this.m_pOpoSqlValCtx->InitialLongFS;
			this.m_pOpoDacValCtx->InitialLobFS = this.m_pOpoSqlValCtx->InitialLobFS;
			this.m_pOpoDacValCtx->ResultsetIndex = this.m_currentResultIndex;
			this.m_pOpoDacValCtx->pSnapShot = pOpoSqlValCtx->pSnapShot;
			pOpoSqlValCtx->pSnapShot = IntPtr.Zero;
			this.m_pOpoDacValCtx->AddRowid = this.m_pOpoSqlValCtx->AddRowid;
			this.m_pOpoDacValCtx->AddToStmtCache = this.m_pOpoSqlValCtx->AddToStmtCache;
			this.m_isDBVer10gR2OrHigher = this.m_connection.IsDBVer10gR2OrHigher;
			if (this.m_metaData != null && this.m_metaData.m_dotNetNumericAccessor != null && this.m_metaData.m_rowSize == this.m_rowSize)
			{
				this.m_colOffset = this.m_metaData.m_colOffset;
				this.m_colIndOffset = this.m_metaData.m_colIndOffset;
				this.m_colLenOffset = this.m_metaData.m_colLenOffset;
				this.m_oraType = this.m_metaData.m_oraType;
				this.m_oracleDbType = this.m_metaData.m_oracleDbType;
				this.m_dotNetNumericAccessor = this.m_metaData.m_dotNetNumericAccessor;
			}
			else
			{
				this.m_colOffset = new uint[this.m_fieldCount];
				this.m_colIndOffset = new uint[this.m_fieldCount];
				this.m_colLenOffset = new uint[this.m_fieldCount];
				this.m_oraType = new OraType[this.m_fieldCount];
				this.m_oracleDbType = new OracleDbType[this.m_fieldCount];
				this.m_dotNetNumericAccessor = new DotNetNumericAccessor[this.m_fieldCount];
				for (int i = 0; i < this.m_fieldCount; i++)
				{
					this.m_colOffset[i] = 0U;
					if (i > 0)
					{
						this.m_colOffset[i] = this.m_pOpoMetValCtx->pColMetaVal[i - 1].Offset;
					}
					this.m_colIndOffset[i] = this.m_colOffset[i] + this.m_pOpoMetValCtx->pColMetaVal[i].Define.Length;
					this.m_colLenOffset[i] = this.m_colIndOffset[i] + 2U;
					this.m_oraType[i] = (OraType)this.m_pOpoMetValCtx->pColMetaVal[i].OraType;
					this.m_oracleDbType[i] = this.GetOraDbTypeEx(i);
					int scale = (int)this.m_pOpoMetValCtx->pColMetaVal[i].Scale;
					int precision = (int)this.m_pOpoMetValCtx->pColMetaVal[i].Precision;
					if (scale <= 0 && precision - scale < 5)
					{
						this.m_dotNetNumericAccessor[i] = DotNetNumericAccessor.GetInt16;
					}
					else if (scale <= 0 && precision - scale < 10)
					{
						this.m_dotNetNumericAccessor[i] = DotNetNumericAccessor.GetInt32;
					}
					else if (scale <= 0 && precision - scale < 19)
					{
						this.m_dotNetNumericAccessor[i] = DotNetNumericAccessor.GetInt64;
					}
					else if (precision < 8 && ((scale <= 0 && precision - scale <= 38) || (scale > 0 && scale <= 44)))
					{
						this.m_dotNetNumericAccessor[i] = DotNetNumericAccessor.GetFloat;
					}
					else if (precision < 16)
					{
						this.m_dotNetNumericAccessor[i] = DotNetNumericAccessor.GetDouble;
					}
					else
					{
						this.m_dotNetNumericAccessor[i] = DotNetNumericAccessor.GetDecimal;
					}
				}
				if (this.m_metaData != null && this.m_metaData.m_dotNetNumericAccessor == null)
				{
					lock (this.m_metaData)
					{
						if (this.m_metaData.m_dotNetNumericAccessor == null)
						{
							this.m_metaData.m_rowSize = this.m_rowSize;
							this.m_metaData.m_colOffset = this.m_colOffset;
							this.m_metaData.m_colIndOffset = this.m_colIndOffset;
							this.m_metaData.m_colLenOffset = this.m_colLenOffset;
							this.m_metaData.m_oraType = this.m_oraType;
							this.m_metaData.m_oracleDbType = this.m_oracleDbType;
							this.m_metaData.m_dotNetNumericAccessor = this.m_dotNetNumericAccessor;
						}
					}
				}
			}
			this.m_currentClientRow = 0;
			this.m_recordCount = this.m_pOpoDacValCtx->RecordCount;
			this.m_fetchArrayLocation = (long)this.m_pOpoSqlValCtx->FetchArrayLocation;
			if (this.m_opsSqlCtx == null || this.m_pOpoMetValCtx == null || (this.m_commandBehavior & CommandBehavior.SchemaOnly) == CommandBehavior.SchemaOnly)
			{
				this.m_bSetLastFetch = true;
			}
			this.m_bCmdBehaviorSingleRow = ((this.m_commandBehavior & CommandBehavior.SingleRow) == CommandBehavior.SingleRow);
			lock (this.m_connection.m_DataReaderList.SyncRoot)
			{
				this.m_connection.m_DataReaderList.Add(this);
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000DDAC File Offset: 0x0000CDAC
		internal unsafe void UpdateMetaDataPool()
		{
			if (this.m_pOpoMetValCtx == null || this.m_pOpoMetValCtx->bPkFetched == 0)
			{
				return;
			}
			if (this.m_pOpoSqlValCtx->CommandType != 1 || this.m_commandText == null)
			{
				return;
			}
			if (this.m_pOpoMetValCtx->bStmtParsed == 1 && !this.m_doneMarshalAndStoreMetaData)
			{
				this.GetColMetaRef(false, true);
			}
			if (this.m_connection.m_opoConCtx.metaPool == 0)
			{
				return;
			}
			if ((this.m_pOpoDacValCtx->InitialLongFS != 0 || this.m_pOpoDacValCtx->InitialLobFS != 0) && (!this.m_isFromEF || this.m_pOpoDacValCtx->InitialLongFS > 0 || this.m_pOpoDacValCtx->InitialLobFS > 0))
			{
				return;
			}
			MetaData metaData = this.m_connection.m_opoConCtx.m_conPooler.Get(this.m_commandText) as MetaData;
			bool flag = false;
			if (metaData == null)
			{
				metaData = this.m_metaData;
				flag = true;
			}
			OpoMetValCtx* ptr;
			if (!this.m_addRowid)
			{
				ptr = metaData.m_pOpoMetValCtx;
			}
			else
			{
				ptr = metaData.m_pOpoMetValCtxWRowid;
			}
			if (flag)
			{
				ptr->bPooled = 1;
				this.m_connection.m_opoConCtx.m_conPooler.Put(this.m_commandText, metaData);
			}
			else if (ptr->bPkFetched != 1)
			{
				if (this.m_pOpoMetValCtx->bPkPresent == 1 || this.m_pOpoMetValCtx->bRowidPresent == 1)
				{
					int noOfCols = (int)this.m_pOpoMetValCtx->NoOfCols;
					for (int i = 0; i < noOfCols; i++)
					{
						ptr->pColMetaVal[i].bIsKeyColumn = this.m_pOpoMetValCtx->pColMetaVal[i].bIsKeyColumn;
						ptr->pColMetaVal[i].bIsUnique = this.m_pOpoMetValCtx->pColMetaVal[i].bIsUnique;
					}
					ptr->bRowidPresent = this.m_pOpoMetValCtx->bRowidPresent;
					ptr->bPkPresent = this.m_pOpoMetValCtx->bPkPresent;
				}
				ptr->bPkFetched = 1;
			}
			ptr = null;
			this.m_pkFetched = true;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000DFA8 File Offset: 0x0000CFA8
		internal object ChangeType(object sourceValue, Type targetType)
		{
			if (sourceValue is byte[] && targetType == typeof(Guid))
			{
				return new Guid((byte[])sourceValue);
			}
			if (sourceValue is TimeSpan && targetType == typeof(decimal))
			{
				return (decimal)((TimeSpan)sourceValue).TotalSeconds;
			}
			return Convert.ChangeType(sourceValue, targetType, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000E020 File Offset: 0x0000D020
		~OracleDataReader()
		{
			this.Dispose(false);
		}

		// Token: 0x04000031 RID: 49
		private static int POOLED_CACHE_ARRAY_SIZE = 131072;

		// Token: 0x04000032 RID: 50
		private int m_freeOpsSqlCtx;

		// Token: 0x04000033 RID: 51
		private IntPtr m_opsConCtx;

		// Token: 0x04000034 RID: 52
		private IntPtr m_opsErrCtx;

		// Token: 0x04000035 RID: 53
		private IntPtr[] m_opsSqlCtx;

		// Token: 0x04000036 RID: 54
		internal IntPtr m_opsDacCtx;

		// Token: 0x04000037 RID: 55
		private ColMetaRef[] m_colMetaRef;

		// Token: 0x04000038 RID: 56
		private unsafe OpoSqlValCtx* m_pOpoSqlValCtx;

		// Token: 0x04000039 RID: 57
		internal uint[] m_colOffset;

		// Token: 0x0400003A RID: 58
		internal uint[] m_colIndOffset;

		// Token: 0x0400003B RID: 59
		internal uint[] m_colLenOffset;

		// Token: 0x0400003C RID: 60
		internal uint[] m_colDatOffset;

		// Token: 0x0400003D RID: 61
		internal uint[] m_colDatSize;

		// Token: 0x0400003E RID: 62
		internal long m_fetchArrayLocation;

		// Token: 0x0400003F RID: 63
		internal int m_currentClientRow;

		// Token: 0x04000040 RID: 64
		private long m_rowLocation;

		// Token: 0x04000041 RID: 65
		private int m_recordCount;

		// Token: 0x04000042 RID: 66
		private OraType[] m_oraType;

		// Token: 0x04000043 RID: 67
		private OracleDbType[] m_oracleDbType;

		// Token: 0x04000044 RID: 68
		private IntPtr m_pColumnsDataBuffer = IntPtr.Zero;

		// Token: 0x04000045 RID: 69
		private DotNetNumericAccessor[] m_dotNetNumericAccessor;

		// Token: 0x04000046 RID: 70
		private object m_disposeSyncObj = new object();

		// Token: 0x04000047 RID: 71
		private MetaData m_metaData;

		// Token: 0x04000048 RID: 72
		internal unsafe OpoMetValCtx* m_pOpoMetValCtx;

		// Token: 0x04000049 RID: 73
		internal bool m_bHasUdtType;

		// Token: 0x0400004A RID: 74
		internal unsafe OpoDacValCtx* m_pOpoDacValCtx;

		// Token: 0x0400004B RID: 75
		internal bool m_bCmdBehaviorSingleRow;

		// Token: 0x0400004C RID: 76
		private int m_recordsAffected;

		// Token: 0x0400004D RID: 77
		internal long m_fetchSize;

		// Token: 0x0400004E RID: 78
		private int m_currentResultIndex;

		// Token: 0x0400004F RID: 79
		private int m_resultCount;

		// Token: 0x04000050 RID: 80
		private bool m_closed;

		// Token: 0x04000051 RID: 81
		private bool m_disposed;

		// Token: 0x04000052 RID: 82
		private bool m_bBOF;

		// Token: 0x04000053 RID: 83
		private bool m_bLastFetch;

		// Token: 0x04000054 RID: 84
		private bool m_bSetLastFetch;

		// Token: 0x04000055 RID: 85
		private bool m_bEOF;

		// Token: 0x04000056 RID: 86
		private bool m_fillReader;

		// Token: 0x04000057 RID: 87
		private OracleConnection m_connection;

		// Token: 0x04000058 RID: 88
		private CommandBehavior m_commandBehavior;

		// Token: 0x04000059 RID: 89
		private DataTable m_dataTable;

		// Token: 0x0400005A RID: 90
		private ArrayList m_dataTableList;

		// Token: 0x0400005B RID: 91
		private bool m_noMoreResults;

		// Token: 0x0400005C RID: 92
		private int m_conSignature;

		// Token: 0x0400005D RID: 93
		private OracleRefCursor m_refCursor;

		// Token: 0x0400005E RID: 94
		private Hashtable m_safeMapping;

		// Token: 0x0400005F RID: 95
		private string m_commandText;

		// Token: 0x04000060 RID: 96
		private bool m_pkFetched;

		// Token: 0x04000061 RID: 97
		private bool m_doneMarshalAndStoreMetaData;

		// Token: 0x04000062 RID: 98
		private bool m_addRowid;

		// Token: 0x04000063 RID: 99
		private long m_rowSize;

		// Token: 0x04000064 RID: 100
		private IntPtr[] m_opsXmlTypeCtx;

		// Token: 0x04000065 RID: 101
		private bool m_hasRows;

		// Token: 0x04000066 RID: 102
		private bool m_doneReadOne;

		// Token: 0x04000067 RID: 103
		private bool m_bHasRowsCalledBeforeRead;

		// Token: 0x04000068 RID: 104
		private int m_fieldCount;

		// Token: 0x04000069 RID: 105
		private bool m_external;

		// Token: 0x0400006A RID: 106
		private bool m_isDBVer10gR2OrHigher;

		// Token: 0x0400006B RID: 107
		private bool m_bFetchSizePropertySet;

		// Token: 0x0400006C RID: 108
		private object[] m_currentRowUdtCache;

		// Token: 0x0400006D RID: 109
		private OracleUdtDescriptor[] m_udtDescriptorCache;

		// Token: 0x0400006E RID: 110
		internal bool m_returnPSTypes;

		// Token: 0x0400006F RID: 111
		private FetchArrayPooler m_fetchArrayPooler;

		// Token: 0x04000070 RID: 112
		internal string m_storedProcName;

		// Token: 0x04000071 RID: 113
		internal PrimitiveType[] m_expectedColumnTypes;

		// Token: 0x04000072 RID: 114
		internal bool m_isFromEF;
	}
}
