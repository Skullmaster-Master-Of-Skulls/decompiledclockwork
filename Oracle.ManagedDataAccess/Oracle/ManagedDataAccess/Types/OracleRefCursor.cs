using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x0200024F RID: 591
	public sealed class OracleRefCursor : MarshalByRefObject, IDisposable, INullable
	{
		// Token: 0x060016E2 RID: 5858 RVA: 0x000F3C64 File Offset: 0x000F1E64
		private OracleRefCursor()
		{
			this.m_bNotNull = false;
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x000F3C9C File Offset: 0x000F1E9C
		public OracleConnection Connection
		{
			get
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (!this.m_bNotNull)
				{
					return null;
				}
				return this.m_connection;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x000F3D20 File Offset: 0x000F1F20
		// (set) Token: 0x060016E4 RID: 5860 RVA: 0x000F3CCC File Offset: 0x000F1ECC
		public long FetchSize
		{
			get
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				return this.m_fetchSize;
			}
			set
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (value <= 0L)
				{
					throw new ArgumentException();
				}
				if (this.m_cachedReader != null)
				{
					this.m_cachedReader.FetchSize = value;
				}
				this.m_fetchSize = value;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x000F3D48 File Offset: 0x000F1F48
		public long RowSize
		{
			get
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (this.m_refCursorImpl == null || this.m_refCursorImpl.m_sqlMetaData == null)
				{
					return 0L;
				}
				return (long)this.m_refCursorImpl.m_sqlMetaData.m_maxRowSize + (long)this.m_refCursorImpl.m_sqlMetaData.m_numOfLOBColumns * Math.Max(86L, 86L + this.m_clientInitialLOBFS) + (long)this.m_refCursorImpl.m_sqlMetaData.m_numOfLONGColumns * Math.Max(2L, this.m_initialLongFS) + (long)(this.m_refCursorImpl.m_sqlMetaData.m_numOfBFileColumns * 86);
			}
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x000F3DF8 File Offset: 0x000F1FF8
		internal OracleRefCursor(OracleConnection connection, OracleRefCursorImpl refCursorImpl, OracleIntervalDS sessionTimeZone, string commandText, string paramPosOrName, long initialLongFS, long clientInitialLobFS, long internalInitialLOBFS, long[] scnFromExecution, bool bImplicitRefCursor = false)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_connection = connection;
				this.m_sessionTimeZone = sessionTimeZone;
				this.m_initialLongFS = initialLongFS;
				this.m_clientInitialLOBFS = clientInitialLobFS;
				this.m_internalInitialLOBFS = internalInitialLOBFS;
				this.m_snapshotSCN = scnFromExecution;
				this.m_refCursorImpl = refCursorImpl;
				bool flag = false;
				int num = -1;
				try
				{
					num = int.Parse(paramPosOrName);
					flag = true;
				}
				catch (Exception)
				{
				}
				ConfigBaseClass.StoredProcedureInfo storedProcInfo = ConfigBaseClass.GetInstance(true).GetStoredProcInfo(commandText);
				if (storedProcInfo != null)
				{
					List<RefCursorInfo> list;
					if (bImplicitRefCursor)
					{
						list = storedProcInfo.m_implicitlyRetRefCursors;
					}
					else
					{
						list = storedProcInfo.m_refCursors;
					}
					foreach (RefCursorInfo refCursorInfo in list)
					{
						if (flag)
						{
							if (refCursorInfo.position == num)
							{
								this.m_refCursorInfo = refCursorInfo;
							}
						}
						else if (refCursorInfo.name == paramPosOrName)
						{
							this.m_refCursorInfo = refCursorInfo;
						}
					}
				}
				if (this.m_connection.m_oracleConnectionImpl != null)
				{
					this.m_connection.m_oracleConnectionImpl.RegisterForConnectionClose(this);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x000F3F98 File Offset: 0x000F2198
		public OracleDataReader GetDataReader()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDataReader dataReader;
			try
			{
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				dataReader = this.GetDataReader(false);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return dataReader;
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x000F401C File Offset: 0x000F221C
		[MethodImpl(MethodImplOptions.Synchronized)]
		internal OracleDataReader GetDataReader(bool fillRequest)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleDataReader result;
			try
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (this.m_bReaderGivenToRefCursor)
				{
					if (!fillRequest || this.m_cachedReader == null)
					{
						throw new InvalidOperationException();
					}
					result = this.m_cachedReader;
				}
				else
				{
					OracleDataReaderImpl initializedDataReaderImpl = this.m_connection.m_oracleConnectionImpl.GetInitializedDataReaderImpl(this.m_refCursorImpl.m_accessors, this.m_refCursorImpl.m_sqlMetaData, this.m_refCursorImpl.m_cursorId, 0, null, this.m_sessionTimeZone, this.m_initialLongFS, this.m_clientInitialLOBFS, this.m_internalInitialLOBFS, this.m_snapshotSCN, false, false);
					initializedDataReaderImpl.m_bHasMoreRowsInDB = true;
					initializedDataReaderImpl.m_bForRefCursor = true;
					OracleDataReader oracleDataReader = new OracleDataReader(initializedDataReaderImpl, this.m_connection, this.m_fetchSize, this.m_clientInitialLOBFS, this.m_internalInitialLOBFS, (int)this.m_initialLongFS, -1, string.Empty, SqlStatementType.PLSQL, CommandBehavior.Default);
					oracleDataReader.RefCursor = this;
					if (fillRequest)
					{
						this.m_cachedReader = oracleDataReader;
					}
					else
					{
						this.m_cachedReader = null;
					}
					this.m_bReaderGivenToRefCursor = true;
					result = oracleDataReader;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x000F4198 File Offset: 0x000F2398
		internal void Close()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			if (!this.m_bNotNull)
			{
				return;
			}
			if (!this.m_bClosed)
			{
				lock (this.lockRefCursor)
				{
					if (!this.m_bClosed)
					{
						try
						{
							if (this.m_connection != null)
							{
								if (this.m_cachedReader != null && !this.m_cachedReader.IsClosed)
								{
									lock (this.m_syncObj)
									{
										if (this.m_cachedReader != null)
										{
											this.m_cachedReader.Close();
											this.m_cachedReader = null;
										}
										goto IL_C2;
									}
								}
								if (!this.m_bReaderGivenToRefCursor)
								{
									this.m_connection.m_oracleConnectionImpl.AddCursorIdToBeClosed((long)this.m_refCursorImpl.m_cursorId);
								}
							}
							IL_C2:;
						}
						catch (Exception ex)
						{
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
								{
									ex.ToString()
								});
							}
						}
						finally
						{
							this.m_bClosed = true;
							if (this.m_connection != null)
							{
								if (this.m_connection.m_oracleConnectionImpl != null)
								{
									this.m_connection.m_oracleConnectionImpl.DeregisterForConnectionClose(this);
								}
								this.m_connection = null;
							}
							if (!this.m_bDisposed)
							{
								this.Dispose();
							}
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
							}
						}
					}
				}
			}
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x000F4360 File Offset: 0x000F2560
		internal void ConnectionClose()
		{
			if (!this.m_bClosed)
			{
				this.Close();
			}
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x000F4370 File Offset: 0x000F2570
		public void Dispose()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.Dispose(true);
			}
			catch (Exception ex)
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Error, new string[]
					{
						ex.ToString()
					});
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x000F43FC File Offset: 0x000F25FC
		private void Dispose(bool disposing)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			if (!this.m_bDisposed)
			{
				lock (this.lockRefCursor)
				{
					if (!this.m_bDisposed)
					{
						try
						{
							if (this.m_bNotNull)
							{
								this.m_bDisposed = true;
								if (!this.m_bClosed)
								{
									this.Close();
								}
							}
						}
						catch (Exception ex)
						{
							OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
						}
						finally
						{
							GC.SuppressFinalize(this);
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
							}
						}
					}
				}
			}
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x000F44C8 File Offset: 0x000F26C8
		protected override void Finalize()
		{
			try
			{
				this.Dispose(false);
			}
			catch (Exception ex)
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Error, new string[]
					{
						ex.Message
					});
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x000F452C File Offset: 0x000F272C
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x04001A1A RID: 6682
		internal const int MAX_SIZE = 16;

		// Token: 0x04001A1B RID: 6683
		internal OracleRefCursorImpl m_refCursorImpl;

		// Token: 0x04001A1C RID: 6684
		internal OracleConnection m_connection;

		// Token: 0x04001A1D RID: 6685
		private OracleDataReader m_cachedReader;

		// Token: 0x04001A1E RID: 6686
		private long m_fetchSize = 65535L;

		// Token: 0x04001A1F RID: 6687
		private bool m_bNotNull = true;

		// Token: 0x04001A20 RID: 6688
		private bool m_bReaderGivenToRefCursor;

		// Token: 0x04001A21 RID: 6689
		private long m_initialLongFS;

		// Token: 0x04001A22 RID: 6690
		private long m_clientInitialLOBFS;

		// Token: 0x04001A23 RID: 6691
		private long m_internalInitialLOBFS;

		// Token: 0x04001A24 RID: 6692
		internal long[] m_snapshotSCN;

		// Token: 0x04001A25 RID: 6693
		internal OracleIntervalDS m_sessionTimeZone;

		// Token: 0x04001A26 RID: 6694
		internal bool m_bClosed;

		// Token: 0x04001A27 RID: 6695
		internal bool m_bDisposed;

		// Token: 0x04001A28 RID: 6696
		private object m_syncObj = new object();

		// Token: 0x04001A29 RID: 6697
		private object lockRefCursor = new object();

		// Token: 0x04001A2A RID: 6698
		internal RefCursorInfo m_refCursorInfo;

		// Token: 0x04001A2B RID: 6699
		public static readonly OracleRefCursor Null = new OracleRefCursor();
	}
}
