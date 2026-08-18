using System;
using System.Data;
using System.IO;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x02000245 RID: 581
	public sealed class OracleBlob : Stream, IDisposable, ICloneable, INullable
	{
		// Token: 0x06001538 RID: 5432 RVA: 0x000E4C10 File Offset: 0x000E2E10
		public OracleBlob(OracleConnection connection) : this(connection, false)
		{
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x000E4C1C File Offset: 0x000E2E1C
		public OracleBlob(OracleConnection connection, bool bCaching)
		{
			this.m_bNotNull = true;
			this.m_lock = new object();
			this.lockBlob = new object();
			base..ctor();
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (connection == null)
				{
					GC.SuppressFinalize(this);
					throw new ArgumentNullException("connection");
				}
				if (ConnectionState.Open != connection.m_connectionState)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_CLOSED, new string[0]));
				}
				this.m_connection = connection;
				this.m_blobImpl = new OracleBlobImpl(connection.m_oracleConnectionImpl, null, bCaching);
				this.m_blobImpl.m_isTemporaryLob = true;
				if (this.m_connection.m_oracleConnectionImpl != null)
				{
					this.m_connection.m_oracleConnectionImpl.RegisterForConnectionClose(this);
				}
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
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x000E4D24 File Offset: 0x000E2F24
		internal OracleBlob(OracleConnection connection, byte[] lobLocator)
		{
			this.m_bNotNull = true;
			this.m_lock = new object();
			this.lockBlob = new object();
			base..ctor();
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_connection = connection;
				bool flag = OracleBlobImpl.IsTemporaryLob(lobLocator);
				if (flag)
				{
					string lobIdString = OracleBlobImpl.GetLobIdString(lobLocator);
					OracleBlobImpl oracleBlobImpl = (OracleBlobImpl)connection.m_oracleConnectionImpl.TemporaryLobReferenceGet(lobIdString);
					if (oracleBlobImpl != null)
					{
						this.m_blobImpl = oracleBlobImpl;
						this.m_blobImpl.AddRef();
					}
					else
					{
						this.m_blobImpl = new OracleBlobImpl(connection.m_oracleConnectionImpl, lobLocator, false);
					}
				}
				else
				{
					this.m_blobImpl = new OracleBlobImpl(connection.m_oracleConnectionImpl, lobLocator, false);
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

		// Token: 0x0600153B RID: 5435 RVA: 0x000E4E3C File Offset: 0x000E303C
		internal OracleBlob(OracleConnection connection, OracleBlobImpl blobImpl)
		{
			this.m_bNotNull = true;
			this.m_lock = new object();
			this.lockBlob = new object();
			base..ctor();
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			this.m_connection = connection;
			this.m_blobImpl = blobImpl;
			if (this.m_connection.m_oracleConnectionImpl != null)
			{
				this.m_connection.m_oracleConnectionImpl.RegisterForConnectionClose(this);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
			}
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x000E4EC8 File Offset: 0x000E30C8
		private OracleBlob(char dummy)
		{
			this.m_bNotNull = true;
			this.m_lock = new object();
			this.lockBlob = new object();
			base..ctor();
			this.m_bNotNull = false;
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x000E4EF4 File Offset: 0x000E30F4
		protected override void Finalize()
		{
			try
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
				}
				try
				{
					this.Dispose(false);
				}
				catch (Exception ex)
				{
					OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				}
				finally
				{
					if (ProviderConfig.m_bTraceLevelPublic)
					{
						Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x000E4F80 File Offset: 0x000E3180
		public override bool CanRead
		{
			get
			{
				return !this.m_bNotNull || !this.m_bClosed;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x000E4F98 File Offset: 0x000E3198
		public override bool CanSeek
		{
			get
			{
				return !this.m_bNotNull || !this.m_bClosed;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x000E4FB0 File Offset: 0x000E31B0
		public override bool CanWrite
		{
			get
			{
				return this.m_bNotNull && !this.m_bClosed;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x000E4FC8 File Offset: 0x000E31C8
		public bool IsInChunkWriteMode
		{
			get
			{
				return this.m_bNotNull && this.m_isInChunkWriteMode;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x000E4FDC File Offset: 0x000E31DC
		public bool IsTemporary
		{
			get
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				return this.m_bNotNull && (this.m_blobImpl.m_isTemporaryLob || this.m_blobImpl.IsTemporaryLob());
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x000E502C File Offset: 0x000E322C
		public override long Length
		{
			get
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (!this.m_bNotNull)
				{
					return 0L;
				}
				if (this.m_blobImpl.m_isTemporaryLob && !this.m_blobImpl.m_doneTempLobCreate)
				{
					return 0L;
				}
				return this.m_blobImpl.GetLength();
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x000E508C File Offset: 0x000E328C
		public int OptimumChunkSize
		{
			get
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (!this.m_bNotNull)
				{
					return 0;
				}
				if (this.m_blobImpl.m_chunkSize != 0)
				{
					return this.m_blobImpl.m_chunkSize;
				}
				return this.GetOptimumChunkSize();
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06001545 RID: 5445 RVA: 0x000E50E0 File Offset: 0x000E32E0
		// (set) Token: 0x06001546 RID: 5446 RVA: 0x000E5114 File Offset: 0x000E3314
		public override long Position
		{
			get
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (!this.m_bNotNull)
				{
					return 0L;
				}
				return this.m_position;
			}
			set
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (this.m_bNotNull)
				{
					if (value < 0L)
					{
						throw new ArgumentOutOfRangeException("Position");
					}
					this.m_position = value;
				}
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x000E5154 File Offset: 0x000E3354
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x000E5160 File Offset: 0x000E3360
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

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x000E5190 File Offset: 0x000E3390
		public bool IsEmpty
		{
			get
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (this.Length != 0L)
				{
					return this.m_blobImpl.m_isEmpty = false;
				}
				return this.m_blobImpl.m_isEmpty = true;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x000E51F4 File Offset: 0x000E33F4
		public byte[] Value
		{
			get
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (this.m_blobImpl.m_isTemporaryLob && !this.m_blobImpl.m_doneTempLobCreate)
				{
					return null;
				}
				long position = this.m_position;
				this.m_position = 0L;
				long length = this.Length;
				int num;
				if (length >= 2147483647L)
				{
					num = int.MaxValue;
				}
				else
				{
					num = (int)length;
				}
				byte[] array = new byte[num];
				this.Read(array, 0, num);
				this.m_position = position;
				return array;
			}
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x000E5290 File Offset: 0x000E3490
		internal byte[] GetLobLocator()
		{
			return this.m_blobImpl.m_lobLocator;
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x000E52A0 File Offset: 0x000E34A0
		internal void SetLobLocator(byte[] lobLocator, bool bTempLob)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				OracleBlobImpl oracleBlobImpl;
				if ((this.m_blobImpl.m_isTemporaryLob && this.m_blobImpl.m_doneTempLobCreate) || this.m_blobImpl.IsTemporaryLob())
				{
					oracleBlobImpl = (OracleBlobImpl)this.m_blobImpl.m_connectionImpl.TemporaryLobReferenceGet(this.m_blobImpl.m_lobId);
					if (oracleBlobImpl != null)
					{
						if (oracleBlobImpl.m_refCount == 1)
						{
							lock (this.m_lock)
							{
								if (oracleBlobImpl.m_refCount == 1)
								{
									this.m_blobImpl.m_connectionImpl.TemporaryLobReferenceRemove(this.m_blobImpl.m_lobId);
								}
								goto IL_BA;
							}
						}
						this.m_blobImpl.RelRef();
					}
					IL_BA:;
				}
				oracleBlobImpl = (OracleBlobImpl)this.m_blobImpl.m_connectionImpl.TemporaryLobReferenceGet(OracleBlobImpl.GetLobIdString(lobLocator));
				if (oracleBlobImpl != null)
				{
					lock (this.m_lock)
					{
						this.m_blobImpl = oracleBlobImpl;
						this.m_blobImpl.AddRef();
						goto IL_157;
					}
				}
				this.m_blobImpl.m_lobId = OracleBlobImpl.GetLobIdString(lobLocator);
				this.m_blobImpl.m_lobLocator = lobLocator;
				this.m_blobImpl.m_isTemporaryLob = bTempLob;
				this.m_blobImpl.m_connectionImpl.TemporaryLobReferenceAdd(this.m_blobImpl.m_lobId, this.m_blobImpl, true);
				IL_157:;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x000E546C File Offset: 0x000E366C
		public bool IsEqual(OracleBlob obj)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (obj == null)
				{
					throw new ArgumentNullException("obj");
				}
				if (!this.m_bNotNull || obj.IsNull)
				{
					if (!this.m_bNotNull && obj.IsNull)
					{
						result = true;
					}
					else
					{
						result = false;
					}
				}
				else
				{
					if (obj.m_connection != this.m_connection)
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_DIFFERENT_CONNECTIONS, new string[0]));
					}
					if ((obj.m_blobImpl.m_isTemporaryLob && !obj.m_blobImpl.m_doneTempLobCreate) || (this.m_blobImpl.m_isTemporaryLob && !this.m_blobImpl.m_doneTempLobCreate))
					{
						result = false;
					}
					else
					{
						if (this.m_command == null)
						{
							this.m_command = new OracleCommand();
						}
						this.m_command.Connection = this.m_connection;
						this.m_command.CommandText = "BEGIN :1 := DBMS_LOB.COMPARE(:LOB_1, :LOB_2); END;";
						int num = 0;
						this.m_command.CommandType = CommandType.Text;
						try
						{
							OracleParameter oracleParameter = new OracleParameter("return_value", OracleDbType.Int32, num, ParameterDirection.ReturnValue);
							oracleParameter.DbType = DbType.Int32;
							this.m_command.Parameters.Add(oracleParameter);
							this.m_command.Parameters.Add("LOB_1", OracleDbType.Blob, this, ParameterDirection.Input);
							this.m_command.Parameters.Add("LOB_2", OracleDbType.Blob, obj, ParameterDirection.Input);
							this.m_command.ExecuteNonQuery();
							num = (int)this.m_command.Parameters[0].Value;
						}
						finally
						{
							this.m_command.Parameters.Clear();
						}
						if (num == 0)
						{
							result = true;
						}
						else
						{
							result = false;
						}
					}
				}
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
			return result;
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x000E56AC File Offset: 0x000E38AC
		public long Erase()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			long result;
			try
			{
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				result = this.Erase(0L, this.Length);
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
			return result;
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x000E5738 File Offset: 0x000E3938
		public long Erase(long offset, long amount)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			long result;
			try
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (offset < 0L)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				if (amount < 0L)
				{
					throw new ArgumentOutOfRangeException("amount");
				}
				if (this.m_blobImpl.m_isTemporaryLob && !this.m_blobImpl.m_doneTempLobCreate)
				{
					result = 0L;
				}
				else
				{
					if (this.m_blobImpl.GetRefCount() > 1)
					{
						this.CreateDeepCopy();
					}
					long num = this.m_blobImpl.Erase(offset + 1L, amount);
					result = num;
				}
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
			return result;
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x000E5838 File Offset: 0x000E3A38
		public override void Flush()
		{
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x000E583C File Offset: 0x000E3A3C
		public long Search(byte[] val, long offset, long nth)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			long result;
			try
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (!this.m_bNotNull)
				{
					result = 0L;
				}
				else
				{
					if (val.Length > 16383)
					{
						throw new ArgumentOutOfRangeException("val");
					}
					if (offset < 0L || offset >= (long)((ulong)-1))
					{
						throw new ArgumentOutOfRangeException("offset");
					}
					if (nth <= 0L || nth >= (long)((ulong)-1))
					{
						throw new ArgumentOutOfRangeException("nth");
					}
					if (this.m_blobImpl.m_isTemporaryLob && !this.m_blobImpl.m_doneTempLobCreate)
					{
						result = 0L;
					}
					else
					{
						long num = 0L;
						offset += 1L;
						if (this.m_command == null)
						{
							this.m_command = new OracleCommand();
						}
						this.m_command.Connection = this.m_connection;
						this.m_command.CommandText = "BEGIN :1 := DBMS_LOB.INSTR(:LOB_LOC, :PATTERN, :OFFSET, :NTH); END;";
						this.m_command.CommandType = CommandType.Text;
						try
						{
							OracleParameter oracleParameter = new OracleParameter("return_value", OracleDbType.Int64, num, ParameterDirection.ReturnValue);
							oracleParameter.DbType = DbType.Int64;
							this.m_command.Parameters.Add(oracleParameter);
							this.m_command.Parameters.Add("current_blob", OracleDbType.Blob, this, ParameterDirection.Input);
							this.m_command.Parameters.Add("pattern", OracleDbType.Raw, val, ParameterDirection.Input);
							this.m_command.Parameters.Add("current_offset", OracleDbType.Int64, offset, ParameterDirection.Input);
							this.m_command.Parameters.Add("occurence", OracleDbType.Int64, nth, ParameterDirection.Input);
							this.m_command.ExecuteNonQuery();
							num = (long)this.m_command.Parameters[0].Value;
						}
						finally
						{
							this.m_command.Parameters.Clear();
						}
						result = num;
					}
				}
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
			return result;
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x000E5A90 File Offset: 0x000E3C90
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			long result;
			try
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (!this.m_bNotNull)
				{
					result = 0L;
				}
				else if (this.m_blobImpl.m_isTemporaryLob && !this.m_blobImpl.m_doneTempLobCreate)
				{
					result = 0L;
				}
				else
				{
					if (origin == SeekOrigin.Begin)
					{
						this.m_position = offset;
					}
					if (origin == SeekOrigin.Current)
					{
						this.m_position += offset;
					}
					if (origin == SeekOrigin.End)
					{
						this.m_position = this.Length + offset;
					}
					result = this.m_position;
				}
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
			return result;
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x000E5B7C File Offset: 0x000E3D7C
		public override void SetLength(long newLength)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (newLength < 0L)
				{
					throw new ArgumentOutOfRangeException("newLength");
				}
				if (!this.m_blobImpl.m_isTemporaryLob || this.m_blobImpl.m_doneTempLobCreate)
				{
					if (this.m_blobImpl.m_refCount > 1)
					{
						this.CreateDeepCopy();
					}
					this.m_blobImpl.SetLength(newLength);
					if (this.m_position > newLength)
					{
						this.Seek(0L, SeekOrigin.End);
					}
					if (newLength == 0L)
					{
						this.m_blobImpl.m_isEmpty = true;
					}
				}
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
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x000E5C88 File Offset: 0x000E3E88
		public void BeginChunkWrite()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (!this.m_isInChunkWriteMode)
				{
					if (this.m_blobImpl.m_refCount > 1)
					{
						this.CreateDeepCopy();
					}
					else if (this.m_blobImpl.m_isTemporaryLob && !this.m_blobImpl.m_doneTempLobCreate)
					{
						this.CreateTempLob();
					}
					this.m_blobImpl.Open();
					this.m_isInChunkWriteMode = true;
				}
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
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x000E5D78 File Offset: 0x000E3F78
		public void EndChunkWrite()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (this.m_isInChunkWriteMode)
				{
					this.m_blobImpl.Close();
					this.m_isInChunkWriteMode = false;
				}
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
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x000E5E30 File Offset: 0x000E4030
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (!this.m_bNotNull)
				{
					result = 0;
				}
				else
				{
					if (offset < 0)
					{
						throw new ArgumentOutOfRangeException("offset");
					}
					if (count < 0)
					{
						throw new ArgumentOutOfRangeException("count");
					}
					if (offset + count > buffer.Length)
					{
						throw new ArgumentOutOfRangeException("count");
					}
					if (count == 0 || (this.m_blobImpl.m_isTemporaryLob && !this.m_blobImpl.m_doneTempLobCreate))
					{
						result = 0;
					}
					else
					{
						long locatorOffset;
						if (this.m_position <= 0L)
						{
							locatorOffset = 1L;
						}
						else
						{
							locatorOffset = this.m_position + 1L;
						}
						long numBytesToRead;
						if (count + offset <= buffer.Length)
						{
							numBytesToRead = (long)count;
						}
						else
						{
							numBytesToRead = (long)(buffer.Length - offset);
						}
						long num = this.m_blobImpl.Read(locatorOffset, numBytesToRead, (long)offset, ref buffer);
						this.m_position += num;
						result = (int)num;
					}
				}
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
			return result;
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x000E5F7C File Offset: 0x000E417C
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (offset < 0)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (offset + count > buffer.Length)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (this.m_blobImpl.m_refCount > 1)
				{
					this.CreateDeepCopy();
				}
				else if (this.m_blobImpl.m_isTemporaryLob && !this.m_blobImpl.m_doneTempLobCreate)
				{
					this.CreateTempLob();
				}
				if (count != 0)
				{
					long locatorOffset;
					if (this.m_position <= 0L)
					{
						locatorOffset = 1L;
					}
					else
					{
						locatorOffset = this.m_position + 1L;
					}
					long numBytesToWrite;
					if (count + offset <= buffer.Length)
					{
						numBytesToWrite = (long)count;
					}
					else
					{
						numBytesToWrite = (long)(buffer.Length - offset);
					}
					long num = this.m_blobImpl.Write(locatorOffset, buffer, (long)offset, numBytesToWrite);
					this.m_position += num;
					if (num != 0L)
					{
						this.m_blobImpl.m_isEmpty = false;
					}
				}
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
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x000E6100 File Offset: 0x000E4300
		public void Append(OracleBlob obj)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (obj == null)
				{
					throw new ArgumentNullException("obj");
				}
				if (obj.IsNull)
				{
					throw new OracleNullValueException();
				}
				if (obj.m_connection != this.m_connection)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_DIFFERENT_CONNECTIONS, new string[0]));
				}
				if (!obj.m_blobImpl.m_isTemporaryLob || obj.m_blobImpl.m_doneTempLobCreate)
				{
					if (this.m_blobImpl.m_refCount > 1)
					{
						this.CreateDeepCopy();
					}
					else if (this.m_blobImpl.m_isTemporaryLob && !this.m_blobImpl.m_doneTempLobCreate)
					{
						this.CreateTempLob();
					}
					this.m_blobImpl.Append(obj.m_blobImpl.m_lobLocator);
				}
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
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x000E6244 File Offset: 0x000E4444
		public void Append(byte[] buffer, int offset, int count)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (buffer.Length != 0 && count != 0)
				{
					if (this.m_blobImpl.m_refCount > 1)
					{
						this.CreateDeepCopy();
					}
					else if (this.m_blobImpl.m_isTemporaryLob && !this.m_blobImpl.m_doneTempLobCreate)
					{
						this.CreateTempLob();
					}
					long position = this.m_position;
					this.Seek(0L, SeekOrigin.End);
					this.Write(buffer, offset, count);
					this.m_position = position;
				}
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
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x000E6344 File Offset: 0x000E4544
		public object Clone()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			object result;
			try
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (!this.m_bNotNull)
				{
					result = OracleBlob.Null;
				}
				else
				{
					if (this.m_blobImpl.m_isTemporaryLob && !this.m_blobImpl.m_doneTempLobCreate)
					{
						this.CreateTempLob();
					}
					OracleBlob oracleBlob = new OracleBlob(this.m_connection, this.m_blobImpl);
					this.m_blobImpl.AddRef();
					oracleBlob.m_position = this.m_position;
					oracleBlob.m_bNotNull = this.m_bNotNull;
					result = oracleBlob;
				}
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
			return result;
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x000E643C File Offset: 0x000E463C
		public override void Close()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			if (!this.m_bNotNull)
			{
				return;
			}
			if (!this.m_bClosed)
			{
				lock (this.lockBlob)
				{
					if (!this.m_bClosed)
					{
						try
						{
							this.m_blobImpl.RelRef();
							if (this.m_connection != null && this.m_isInChunkWriteMode)
							{
								this.EndChunkWrite();
							}
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
							this.m_bClosed = true;
							if (this.m_connection != null && this.m_connection.m_oracleConnectionImpl != null)
							{
								this.m_connection.m_oracleConnectionImpl.DeregisterForConnectionClose(this);
							}
							if (!this.m_bDisposed)
							{
								this.Dispose();
							}
							if (ProviderConfig.m_bTraceLevelPublic)
							{
								Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x000E6564 File Offset: 0x000E4764
		internal void ConnectionClose()
		{
			if (!this.m_bClosed)
			{
				this.Close();
			}
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x000E6574 File Offset: 0x000E4774
		public int Compare(long src_offset, OracleBlob obj, long dst_offset, long amount)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (obj == null)
				{
					throw new ArgumentNullException("obj");
				}
				if (!this.m_bNotNull || obj.IsNull)
				{
					if (this.m_bNotNull || !obj.IsNull)
					{
						throw new OracleNullValueException();
					}
					result = 0;
				}
				else
				{
					if (obj.m_connection != this.m_connection)
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_DIFFERENT_CONNECTIONS, new string[0]));
					}
					if (src_offset < 0L)
					{
						throw new ArgumentOutOfRangeException("src_offset");
					}
					if (dst_offset < 0L)
					{
						throw new ArgumentOutOfRangeException("dst_offset");
					}
					if (amount < 0L)
					{
						throw new ArgumentOutOfRangeException("amount");
					}
					if (obj.m_blobImpl.m_isTemporaryLob && !obj.m_blobImpl.m_doneTempLobCreate && this.m_blobImpl.m_isTemporaryLob && !this.m_blobImpl.m_doneTempLobCreate)
					{
						result = 0;
					}
					else
					{
						if (obj.m_blobImpl.m_isTemporaryLob && !obj.m_blobImpl.m_doneTempLobCreate)
						{
							obj.CreateTempLob();
						}
						if (this.m_blobImpl.m_isTemporaryLob && !this.m_blobImpl.m_doneTempLobCreate)
						{
							this.CreateTempLob();
						}
						int num = -1;
						src_offset += 1L;
						dst_offset += 1L;
						if (this.m_command == null)
						{
							this.m_command = new OracleCommand();
						}
						this.m_command.Connection = this.m_connection;
						this.m_command.CommandText = "BEGIN :1 := DBMS_LOB.COMPARE(:LOB_1, :LOB_2, :AMOUNT, :OFFSET_1, :OFFSET_2); END;";
						this.m_command.CommandType = CommandType.Text;
						try
						{
							OracleParameter oracleParameter = new OracleParameter("return_value", OracleDbType.Int32, num, ParameterDirection.ReturnValue);
							oracleParameter.DbType = DbType.Int32;
							this.m_command.Parameters.Add(oracleParameter);
							this.m_command.Parameters.Add("provided_blob", OracleDbType.Blob, obj, ParameterDirection.Input);
							this.m_command.Parameters.Add("current_blob", OracleDbType.Blob, this, ParameterDirection.Input);
							this.m_command.Parameters.Add("compare_amount", OracleDbType.Int64, amount, ParameterDirection.Input);
							this.m_command.Parameters.Add("src_offset", OracleDbType.Int64, src_offset, ParameterDirection.Input);
							this.m_command.Parameters.Add("dst_offset", OracleDbType.Int64, dst_offset, ParameterDirection.Input);
							this.m_command.ExecuteNonQuery();
							num = (int)this.m_command.Parameters[0].Value;
						}
						finally
						{
							this.m_command.Parameters.Clear();
						}
						result = num;
					}
				}
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
			return result;
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x000E6888 File Offset: 0x000E4A88
		public long CopyTo(OracleBlob obj)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			long result;
			try
			{
				if (obj == null)
				{
					throw new ArgumentNullException("obj");
				}
				if (!this.m_bNotNull || obj.IsNull)
				{
					throw new OracleNullValueException();
				}
				long num = this.CopyTo(0L, obj, 0L, this.Length);
				result = num;
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
			return result;
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x000E6930 File Offset: 0x000E4B30
		public long CopyTo(OracleBlob obj, long dst_offset)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			long result;
			try
			{
				if (obj == null)
				{
					throw new ArgumentNullException("obj");
				}
				if (!this.m_bNotNull || obj.IsNull)
				{
					throw new OracleNullValueException();
				}
				long num = this.CopyTo(0L, obj, dst_offset, this.Length);
				result = num;
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
			return result;
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x000E69D4 File Offset: 0x000E4BD4
		public long CopyTo(long src_offset, OracleBlob obj, long dst_offset, long amount)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			long result;
			try
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (obj == null)
				{
					throw new ArgumentNullException("obj");
				}
				if (!this.m_bNotNull || obj.IsNull)
				{
					throw new OracleNullValueException();
				}
				if (obj.m_connection != this.m_connection)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_DIFFERENT_CONNECTIONS, new string[0]));
				}
				if (src_offset < 0L)
				{
					throw new ArgumentOutOfRangeException("src_offset");
				}
				if (dst_offset < 0L)
				{
					throw new ArgumentOutOfRangeException("dst_offset");
				}
				if (amount < 0L)
				{
					throw new ArgumentOutOfRangeException("amount");
				}
				if (this.m_blobImpl.m_isTemporaryLob && !this.m_blobImpl.m_doneTempLobCreate)
				{
					result = 0L;
				}
				else
				{
					if (this.m_blobImpl.m_refCount > 1)
					{
						obj.CreateDeepCopy();
					}
					else if (obj.m_blobImpl.m_isTemporaryLob && !obj.m_blobImpl.m_doneTempLobCreate)
					{
						obj.CreateTempLob();
					}
					src_offset += 1L;
					dst_offset += 1L;
					result = this.m_blobImpl.CopyTo(obj.m_blobImpl.m_lobLocator, src_offset, dst_offset, amount);
				}
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
			return result;
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x000E6B70 File Offset: 0x000E4D70
		public new void Dispose()
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

		// Token: 0x06001562 RID: 5474 RVA: 0x000E6BFC File Offset: 0x000E4DFC
		protected override void Dispose(bool disposing)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			if (!this.m_bDisposed)
			{
				lock (this.lockBlob)
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
								if (disposing)
								{
									if (this.m_command != null)
									{
										this.m_command.Dispose();
										this.m_command = null;
									}
									this.m_connection = null;
								}
							}
						}
						catch (Exception ex)
						{
							OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
							throw;
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

		// Token: 0x06001563 RID: 5475 RVA: 0x000E6CF0 File Offset: 0x000E4EF0
		internal int GetOptimumChunkSize()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int chunkSize;
			try
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (this.m_blobImpl.m_isTemporaryLob && !this.m_blobImpl.m_doneTempLobCreate)
				{
					this.CreateTempLob();
				}
				this.m_blobImpl.m_chunkSize = (int)this.m_blobImpl.GetChunkSize();
				chunkSize = this.m_blobImpl.m_chunkSize;
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
			return chunkSize;
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x000E6DC0 File Offset: 0x000E4FC0
		internal void CreateTempLob()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (this.m_blobImpl.m_isTemporaryLob && !this.m_blobImpl.m_doneTempLobCreate)
				{
					this.m_blobImpl.CreateTemporaryLob();
					this.m_blobImpl.m_doneTempLobCreate = true;
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

		// Token: 0x06001565 RID: 5477 RVA: 0x000E6E80 File Offset: 0x000E5080
		internal void CreateDeepCopy()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				OracleBlobImpl blobImpl = this.m_blobImpl;
				if (this.IsTemporary)
				{
					this.m_blobImpl = new OracleBlobImpl(this.m_connection.m_oracleConnectionImpl, null, blobImpl.m_caching);
					this.m_blobImpl.m_isTemporaryLob = true;
					this.CreateTempLob();
					blobImpl.CopyTo(this.m_blobImpl.m_lobLocator, 1L, 1L, blobImpl.GetLength());
				}
				else
				{
					byte[] array = new byte[this.m_blobImpl.m_lobLocator.Length];
					Array.Copy(this.m_blobImpl.m_lobLocator, array, array.Length);
					this.m_blobImpl = new OracleBlobImpl(this.m_connection.m_oracleConnectionImpl, array, blobImpl.m_caching);
					this.m_blobImpl.m_isTemporaryLob = false;
				}
				blobImpl.RelRef();
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

		// Token: 0x040019A2 RID: 6562
		public const long MaxSize = 4294967295L;

		// Token: 0x040019A3 RID: 6563
		internal OracleBlobImpl m_blobImpl;

		// Token: 0x040019A4 RID: 6564
		internal OracleConnection m_connection;

		// Token: 0x040019A5 RID: 6565
		private OracleCommand m_command;

		// Token: 0x040019A6 RID: 6566
		private bool m_bNotNull;

		// Token: 0x040019A7 RID: 6567
		private bool m_isInChunkWriteMode;

		// Token: 0x040019A8 RID: 6568
		private long m_position;

		// Token: 0x040019A9 RID: 6569
		internal bool m_bClosed;

		// Token: 0x040019AA RID: 6570
		internal bool m_bDisposed;

		// Token: 0x040019AB RID: 6571
		private object m_lock;

		// Token: 0x040019AC RID: 6572
		private object lockBlob;

		// Token: 0x040019AD RID: 6573
		public new static readonly OracleBlob Null = new OracleBlob('x');
	}
}
