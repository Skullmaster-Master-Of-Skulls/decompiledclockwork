using System;
using System.Data;
using System.IO;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x02000247 RID: 583
	public sealed class OracleClob : Stream, IDisposable, ICloneable, INullable
	{
		// Token: 0x0600159A RID: 5530 RVA: 0x000E775C File Offset: 0x000E595C
		public OracleClob(OracleConnection con) : this(con, false, false)
		{
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x000E7768 File Offset: 0x000E5968
		public OracleClob(OracleConnection connection, bool bCaching, bool bNClob)
		{
			this.m_bNotNull = true;
			this.m_lock = new object();
			this.lockClob = new object();
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
				this.m_clobImpl = new OracleClobImpl(connection.m_oracleConnectionImpl, null, bNClob, bCaching);
				this.m_clobImpl.m_isTemporaryLob = true;
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

		// Token: 0x0600159C RID: 5532 RVA: 0x000E7874 File Offset: 0x000E5A74
		internal OracleClob(OracleConnection connection, byte[] lobLocator, bool bNClob, bool bCaching)
		{
			this.m_bNotNull = true;
			this.m_lock = new object();
			this.lockClob = new object();
			base..ctor();
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_connection = connection;
				bool flag = OracleClobImpl.IsTemporaryLob(lobLocator);
				if (flag)
				{
					string lobIdString = OracleClobImpl.GetLobIdString(lobLocator);
					OracleClobImpl oracleClobImpl = (OracleClobImpl)connection.m_oracleConnectionImpl.TemporaryLobReferenceGet(lobIdString);
					if (oracleClobImpl != null)
					{
						this.m_clobImpl = oracleClobImpl;
						this.m_clobImpl.AddRef();
					}
					else
					{
						this.m_clobImpl = new OracleClobImpl(connection.m_oracleConnectionImpl, lobLocator, bNClob, bCaching);
					}
				}
				else
				{
					this.m_clobImpl = new OracleClobImpl(connection.m_oracleConnectionImpl, lobLocator, bNClob, bCaching);
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

		// Token: 0x0600159D RID: 5533 RVA: 0x000E7990 File Offset: 0x000E5B90
		internal OracleClob(OracleConnection connection, OracleClobImpl clobImpl)
		{
			this.m_bNotNull = true;
			this.m_lock = new object();
			this.lockClob = new object();
			base..ctor();
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_connection = connection;
				this.m_clobImpl = clobImpl;
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

		// Token: 0x0600159E RID: 5534 RVA: 0x000E7A50 File Offset: 0x000E5C50
		private OracleClob()
		{
			this.m_bNotNull = true;
			this.m_lock = new object();
			this.lockClob = new object();
			base..ctor();
			this.m_bNotNull = false;
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x000E7A7C File Offset: 0x000E5C7C
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

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x000E7B08 File Offset: 0x000E5D08
		public override bool CanRead
		{
			get
			{
				return !this.m_bNotNull || !this.m_bClosed;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x000E7B20 File Offset: 0x000E5D20
		public override bool CanSeek
		{
			get
			{
				return !this.m_bNotNull || !this.m_bClosed;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x000E7B38 File Offset: 0x000E5D38
		public override bool CanWrite
		{
			get
			{
				return this.m_bNotNull && !this.m_bClosed;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x000E7B50 File Offset: 0x000E5D50
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
				if (this.m_clobImpl.m_isTemporaryLob && !this.m_clobImpl.m_doneTempLobCreate)
				{
					return 0L;
				}
				return this.m_clobImpl.GetLength() * 2L;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x000E7BB4 File Offset: 0x000E5DB4
		// (set) Token: 0x060015A5 RID: 5541 RVA: 0x000E7BE8 File Offset: 0x000E5DE8
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

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x000E7C28 File Offset: 0x000E5E28
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x000E7C34 File Offset: 0x000E5E34
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
				if (this.m_clobImpl.m_optimumChunkSize != 0)
				{
					return this.m_clobImpl.m_optimumChunkSize;
				}
				return this.GetOptimumChunkSize();
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x000E7C88 File Offset: 0x000E5E88
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

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x000E7CB8 File Offset: 0x000E5EB8
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
					return this.m_clobImpl.m_isEmpty = false;
				}
				return this.m_clobImpl.m_isEmpty = true;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x000E7D1C File Offset: 0x000E5F1C
		public bool IsNClob
		{
			get
			{
				return this.m_clobImpl.m_isNClob;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x000E7D2C File Offset: 0x000E5F2C
		public bool IsInChunkWriteMode
		{
			get
			{
				return this.m_bNotNull && this.m_isInChunkWriteMode;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x000E7D40 File Offset: 0x000E5F40
		public bool IsTemporary
		{
			get
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				return this.m_bNotNull && (this.m_clobImpl.m_isTemporaryLob || this.m_clobImpl.IsTemporaryLob());
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x000E7D90 File Offset: 0x000E5F90
		public string Value
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
				if (this.m_clobImpl.m_isTemporaryLob && !this.m_clobImpl.m_doneTempLobCreate)
				{
					return string.Empty;
				}
				long position = this.m_position;
				this.m_position = 0L;
				long num = this.Length / 2L;
				int num2;
				if (num >= 2147483647L)
				{
					num2 = int.MaxValue;
				}
				else
				{
					num2 = (int)num;
				}
				char[] value = new char[num2];
				this.m_clobImpl.Read(1L, (long)num2, 0L, ref value);
				string result = new string(value);
				this.m_position = position;
				return result;
			}
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x000E7E44 File Offset: 0x000E6044
		internal byte[] GetLobLocator()
		{
			return this.m_clobImpl.m_lobLocator;
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x000E7E54 File Offset: 0x000E6054
		internal void SetLobLocator(byte[] lobLocator, bool bTempLob)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				OracleClobImpl oracleClobImpl;
				if ((this.m_clobImpl.m_isTemporaryLob && this.m_clobImpl.m_doneTempLobCreate) || this.m_clobImpl.IsTemporaryLob())
				{
					oracleClobImpl = (OracleClobImpl)this.m_clobImpl.m_connectionImpl.TemporaryLobReferenceGet(this.m_clobImpl.m_lobId);
					if (oracleClobImpl != null)
					{
						if (oracleClobImpl.m_refCount == 1)
						{
							lock (this.m_lock)
							{
								if (oracleClobImpl.m_refCount == 1)
								{
									this.m_clobImpl.m_connectionImpl.TemporaryLobReferenceRemove(this.m_clobImpl.m_lobId);
								}
								goto IL_BA;
							}
						}
						this.m_clobImpl.RelRef();
					}
					IL_BA:;
				}
				oracleClobImpl = (OracleClobImpl)this.m_clobImpl.m_connectionImpl.TemporaryLobReferenceGet(OracleClobImpl.GetLobIdString(lobLocator));
				if (oracleClobImpl != null)
				{
					lock (this.m_lock)
					{
						this.m_clobImpl = oracleClobImpl;
						this.m_clobImpl.AddRef();
						goto IL_157;
					}
				}
				this.m_clobImpl.m_lobId = OracleClobImpl.GetLobIdString(lobLocator);
				this.m_clobImpl.m_lobLocator = lobLocator;
				this.m_clobImpl.m_isTemporaryLob = bTempLob;
				this.m_clobImpl.m_connectionImpl.TemporaryLobReferenceAdd(this.m_clobImpl.m_lobId, this.m_clobImpl, true);
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

		// Token: 0x060015B0 RID: 5552 RVA: 0x000E8020 File Offset: 0x000E6220
		public override void Flush()
		{
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x000E8024 File Offset: 0x000E6224
		public bool IsEqual(OracleClob obj)
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
					if ((obj.m_clobImpl.m_isTemporaryLob && !obj.m_clobImpl.m_doneTempLobCreate) || (this.m_clobImpl.m_isTemporaryLob && !this.m_clobImpl.m_doneTempLobCreate))
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
							this.m_command.Parameters.Add("LOB_1", OracleDbType.Clob, this, ParameterDirection.Input);
							this.m_command.Parameters.Add("LOB_2", OracleDbType.Clob, obj, ParameterDirection.Input);
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

		// Token: 0x060015B2 RID: 5554 RVA: 0x000E8264 File Offset: 0x000E6464
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
					if (offset < 0L || offset >= (long)((ulong)-1))
					{
						throw new ArgumentOutOfRangeException("offset");
					}
					if (nth <= 0L || nth >= (long)((ulong)-1))
					{
						throw new ArgumentOutOfRangeException("nth");
					}
					if (this.m_clobImpl.m_isTemporaryLob && !this.m_clobImpl.m_doneTempLobCreate)
					{
						result = 0L;
					}
					else
					{
						OracleString oracleString = new OracleString(val, true);
						int num;
						if (oracleString.Length > 0)
						{
							num = oracleString.Length;
						}
						else
						{
							num = 1;
						}
						char[] array = new char[num];
						if (oracleString.Length > 0)
						{
							oracleString.Value.CopyTo(0, array, 0, oracleString.Length);
						}
						if (array.Length * 2 > 16383)
						{
							throw new ArgumentOutOfRangeException("val");
						}
						long num2 = 0L;
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
							OracleParameter oracleParameter = new OracleParameter("return_value", OracleDbType.Int64, num2, ParameterDirection.ReturnValue);
							oracleParameter.DbType = DbType.Int64;
							this.m_command.Parameters.Add(oracleParameter);
							OracleDbType dbType;
							if (this.IsNClob)
							{
								dbType = OracleDbType.NClob;
							}
							else
							{
								dbType = OracleDbType.Clob;
							}
							this.m_command.Parameters.Add("this_clob_or_nclob", dbType, this, ParameterDirection.Input);
							this.m_command.Parameters.Add("pattern", OracleDbType.Varchar2, array, ParameterDirection.Input);
							this.m_command.Parameters.Add("this_offset", OracleDbType.Int64, offset, ParameterDirection.Input);
							this.m_command.Parameters.Add("occurrence", OracleDbType.Int64, nth, ParameterDirection.Input);
							this.m_command.ExecuteNonQuery();
							num2 = (long)this.m_command.Parameters[0].Value;
						}
						finally
						{
							this.m_command.Parameters.Clear();
						}
						result = num2;
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

		// Token: 0x060015B3 RID: 5555 RVA: 0x000E851C File Offset: 0x000E671C
		public long Search(char[] val, long offset, long nth)
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
					if (val.Length * 2 > 16383)
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
					if (this.m_clobImpl.m_isTemporaryLob && !this.m_clobImpl.m_doneTempLobCreate)
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
							OracleDbType dbType;
							if (this.IsNClob)
							{
								dbType = OracleDbType.NClob;
							}
							else
							{
								dbType = OracleDbType.Clob;
							}
							this.m_command.Parameters.Add("this_clob_or_nclob", dbType, this, ParameterDirection.Input);
							OracleDbType dbType2;
							if (this.IsNClob)
							{
								dbType2 = OracleDbType.NVarchar2;
							}
							else
							{
								dbType2 = OracleDbType.Varchar2;
							}
							this.m_command.Parameters.Add("pattern", dbType2, val, ParameterDirection.Input);
							this.m_command.Parameters.Add("this_offset", OracleDbType.Int64, offset, ParameterDirection.Input);
							this.m_command.Parameters.Add("occurrence", OracleDbType.Int64, nth, ParameterDirection.Input);
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

		// Token: 0x060015B4 RID: 5556 RVA: 0x000E8794 File Offset: 0x000E6994
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
				else if (this.m_clobImpl.m_isTemporaryLob && !this.m_clobImpl.m_doneTempLobCreate)
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

		// Token: 0x060015B5 RID: 5557 RVA: 0x000E8880 File Offset: 0x000E6A80
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
				if (!this.m_clobImpl.m_isTemporaryLob || this.m_clobImpl.m_doneTempLobCreate)
				{
					if (this.m_clobImpl.m_refCount > 1)
					{
						this.CreateDeepCopy();
					}
					this.m_clobImpl.SetLength(newLength);
					if (this.m_position > newLength * 2L)
					{
						this.Seek(0L, SeekOrigin.End);
					}
					if (newLength == 0L)
					{
						this.m_clobImpl.m_isEmpty = true;
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

		// Token: 0x060015B6 RID: 5558 RVA: 0x000E8990 File Offset: 0x000E6B90
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
					if (count == 0 || (this.m_clobImpl.m_isTemporaryLob && !this.m_clobImpl.m_doneTempLobCreate))
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
							locatorOffset = this.m_position / 2L + 1L;
						}
						long numChars;
						if (count + offset <= buffer.Length)
						{
							numChars = (long)(count / 2);
						}
						else
						{
							numChars = (long)((buffer.Length - offset) / 2);
						}
						long num = this.m_clobImpl.Read(this.m_position, locatorOffset, numChars, (long)offset, ref buffer);
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

		// Token: 0x060015B7 RID: 5559 RVA: 0x000E8AE8 File Offset: 0x000E6CE8
		public int Read(char[] buffer, int offset, int count)
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
					if (this.m_position % 2L != 0L)
					{
						throw new ArgumentOutOfRangeException(null, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.EVEN_VALUE_PARAM_REQUIRED, new string[0]));
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
					if (count == 0 || (this.m_clobImpl.m_isTemporaryLob && !this.m_clobImpl.m_doneTempLobCreate))
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
							locatorOffset = this.m_position / 2L + 1L;
						}
						long numCharsToRead;
						if (count + offset <= buffer.Length)
						{
							numCharsToRead = (long)count;
						}
						else
						{
							numCharsToRead = (long)(buffer.Length - offset);
						}
						long num = this.m_clobImpl.Read(locatorOffset, numCharsToRead, (long)offset, ref buffer);
						this.m_position += num * 2L;
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

		// Token: 0x060015B8 RID: 5560 RVA: 0x000E8C74 File Offset: 0x000E6E74
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
				if (offset % 2 != 0 || count % 2 != 0 || this.m_position % 2L != 0L)
				{
					throw new ArgumentOutOfRangeException(null, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.EVEN_VALUE_PARAM_REQUIRED, new string[0]));
				}
				if (this.m_clobImpl.m_refCount > 1)
				{
					this.CreateDeepCopy();
				}
				else if (this.m_clobImpl.m_isTemporaryLob && !this.m_clobImpl.m_doneTempLobCreate)
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
						locatorOffset = this.m_position / 2L + 1L;
					}
					long num;
					if (count + offset <= buffer.Length)
					{
						num = (long)count;
					}
					else
					{
						num = (long)(buffer.Length - offset);
					}
					long num2 = this.m_clobImpl.Write(locatorOffset, this.m_clobImpl.m_isNClob, buffer, offset, (int)num);
					this.m_position += num2 * 2L;
					if (num2 != 0L)
					{
						this.m_clobImpl.m_isEmpty = false;
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

		// Token: 0x060015B9 RID: 5561 RVA: 0x000E8E38 File Offset: 0x000E7038
		public void Write(char[] buffer, int offset, int count)
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
				if (this.m_position % 2L != 0L)
				{
					throw new ArgumentOutOfRangeException(null, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.EVEN_VALUE_PARAM_REQUIRED, new string[0]));
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
				if (this.m_clobImpl.m_refCount > 1)
				{
					this.CreateDeepCopy();
				}
				else if (this.m_clobImpl.m_isTemporaryLob && !this.m_clobImpl.m_doneTempLobCreate)
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
						locatorOffset = this.m_position / 2L + 1L;
					}
					long numCharsToWrite;
					if (count + offset <= buffer.Length)
					{
						numCharsToWrite = (long)count;
					}
					else
					{
						numCharsToWrite = (long)(buffer.Length - offset);
					}
					long num = this.m_clobImpl.Write(locatorOffset, this.m_clobImpl.m_isNClob, buffer, (long)offset, numCharsToWrite);
					this.m_position += num * 2L;
					if (num != 0L)
					{
						this.m_clobImpl.m_isEmpty = false;
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

		// Token: 0x060015BA RID: 5562 RVA: 0x000E8FF4 File Offset: 0x000E71F4
		public void Append(OracleClob obj)
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
				if (!obj.m_clobImpl.m_isTemporaryLob || obj.m_clobImpl.m_doneTempLobCreate)
				{
					if (this.m_clobImpl.m_refCount > 1)
					{
						this.CreateDeepCopy();
					}
					else if (this.m_clobImpl.m_isTemporaryLob && !this.m_clobImpl.m_doneTempLobCreate)
					{
						this.CreateTempLob();
					}
					this.m_clobImpl.Append(obj.m_clobImpl.m_lobLocator);
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

		// Token: 0x060015BB RID: 5563 RVA: 0x000E9138 File Offset: 0x000E7338
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
					if (offset % 2 != 0 || count % 2 != 0)
					{
						throw new ArgumentOutOfRangeException(null, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.EVEN_VALUE_PARAM_REQUIRED, new string[0]));
					}
					if (this.m_clobImpl.m_refCount > 1)
					{
						this.CreateDeepCopy();
					}
					else if (this.m_clobImpl.m_isTemporaryLob && !this.m_clobImpl.m_doneTempLobCreate)
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

		// Token: 0x060015BC RID: 5564 RVA: 0x000E9258 File Offset: 0x000E7458
		public void Append(char[] buffer, int offset, int count)
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
					if (this.m_clobImpl.m_refCount > 1)
					{
						this.CreateDeepCopy();
					}
					else if (this.m_clobImpl.m_isTemporaryLob && !this.m_clobImpl.m_doneTempLobCreate)
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

		// Token: 0x060015BD RID: 5565 RVA: 0x000E9358 File Offset: 0x000E7558
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
					if (this.m_clobImpl.m_refCount > 1)
					{
						this.CreateDeepCopy();
					}
					else if (this.m_clobImpl.m_isTemporaryLob && !this.m_clobImpl.m_doneTempLobCreate)
					{
						this.CreateTempLob();
					}
					this.m_clobImpl.Open();
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

		// Token: 0x060015BE RID: 5566 RVA: 0x000E9448 File Offset: 0x000E7648
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
					this.m_clobImpl.Close();
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

		// Token: 0x060015BF RID: 5567 RVA: 0x000E9500 File Offset: 0x000E7700
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
				result = this.Erase(0L, this.Length / 2L);
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

		// Token: 0x060015C0 RID: 5568 RVA: 0x000E9590 File Offset: 0x000E7790
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
				if (this.m_clobImpl.m_isTemporaryLob && !this.m_clobImpl.m_doneTempLobCreate)
				{
					result = 0L;
				}
				else
				{
					if (offset < 0L)
					{
						throw new ArgumentOutOfRangeException("offset");
					}
					if (amount < 0L)
					{
						throw new ArgumentOutOfRangeException("amount");
					}
					if (this.m_clobImpl.m_refCount > 1)
					{
						this.CreateDeepCopy();
					}
					long num = this.m_clobImpl.Erase(offset + 1L, amount);
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

		// Token: 0x060015C1 RID: 5569 RVA: 0x000E9690 File Offset: 0x000E7890
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
					result = OracleClob.Null;
				}
				else
				{
					if (this.m_clobImpl.m_isTemporaryLob && !this.m_clobImpl.m_doneTempLobCreate)
					{
						this.CreateTempLob();
					}
					OracleClob oracleClob = new OracleClob(this.m_connection, this.m_clobImpl);
					this.m_clobImpl.AddRef();
					oracleClob.m_position = this.m_position;
					oracleClob.m_bNotNull = this.m_bNotNull;
					result = oracleClob;
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

		// Token: 0x060015C2 RID: 5570 RVA: 0x000E9788 File Offset: 0x000E7988
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
				lock (this.lockClob)
				{
					if (!this.m_bClosed)
					{
						try
						{
							this.m_clobImpl.RelRef();
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

		// Token: 0x060015C3 RID: 5571 RVA: 0x000E98B0 File Offset: 0x000E7AB0
		internal void ConnectionClose()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (!this.m_bClosed)
				{
					this.Close();
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

		// Token: 0x060015C4 RID: 5572 RVA: 0x000E992C File Offset: 0x000E7B2C
		public int Compare(long src_offset, OracleClob obj, long dst_offset, long amount)
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
					if (obj.m_clobImpl.m_isTemporaryLob && !obj.m_clobImpl.m_doneTempLobCreate && this.m_clobImpl.m_isTemporaryLob && !this.m_clobImpl.m_doneTempLobCreate)
					{
						result = 0;
					}
					else
					{
						if (obj.m_clobImpl.m_isTemporaryLob && !obj.m_clobImpl.m_doneTempLobCreate)
						{
							obj.CreateTempLob();
						}
						if (this.m_clobImpl.m_isTemporaryLob && !this.m_clobImpl.m_doneTempLobCreate)
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
						OracleParameter oracleParameter = new OracleParameter("return_value", OracleDbType.Int32, num, ParameterDirection.ReturnValue);
						oracleParameter.DbType = DbType.Int32;
						this.m_command.Parameters.Add(oracleParameter);
						OracleDbType dbType;
						if (obj.IsNClob)
						{
							dbType = OracleDbType.NClob;
						}
						else
						{
							dbType = OracleDbType.Clob;
						}
						try
						{
							this.m_command.Parameters.Add("provided_clob", dbType, obj, ParameterDirection.Input);
							OracleDbType dbType2;
							if (this.IsNClob)
							{
								dbType2 = OracleDbType.NClob;
							}
							else
							{
								dbType2 = OracleDbType.Clob;
							}
							this.m_command.Parameters.Add("current_clob", dbType2, this, ParameterDirection.Input);
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

		// Token: 0x060015C5 RID: 5573 RVA: 0x000E9C64 File Offset: 0x000E7E64
		public long CopyTo(OracleClob obj)
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
					throw new ArgumentNullException();
				}
				if (!this.m_bNotNull || obj.IsNull)
				{
					throw new OracleNullValueException();
				}
				if (this.m_clobImpl.m_isTemporaryLob && !this.m_clobImpl.m_doneTempLobCreate)
				{
					result = 0L;
				}
				else
				{
					result = this.CopyTo(0L, obj, 0L, this.Length / 2L);
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

		// Token: 0x060015C6 RID: 5574 RVA: 0x000E9D44 File Offset: 0x000E7F44
		public long CopyTo(OracleClob obj, long dst_offset)
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
				if (this.m_clobImpl.m_isTemporaryLob && !this.m_clobImpl.m_doneTempLobCreate)
				{
					result = 0L;
				}
				else
				{
					result = this.CopyTo(0L, obj, dst_offset, this.Length / 2L);
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

		// Token: 0x060015C7 RID: 5575 RVA: 0x000E9E28 File Offset: 0x000E8028
		public long CopyTo(long src_offset, OracleClob obj, long dst_offset, long amount)
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
				if (this.m_clobImpl.m_isTemporaryLob && !this.m_clobImpl.m_doneTempLobCreate)
				{
					result = 0L;
				}
				else
				{
					if (obj.m_clobImpl.m_refCount > 1)
					{
						this.CreateDeepCopy();
					}
					else if (obj.m_clobImpl.m_isTemporaryLob && !obj.m_clobImpl.m_doneTempLobCreate)
					{
						obj.CreateTempLob();
					}
					src_offset += 1L;
					dst_offset += 1L;
					result = this.m_clobImpl.CopyTo(obj.m_clobImpl.m_lobLocator, src_offset, dst_offset, amount);
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

		// Token: 0x060015C8 RID: 5576 RVA: 0x000E9FC4 File Offset: 0x000E81C4
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

		// Token: 0x060015C9 RID: 5577 RVA: 0x000EA050 File Offset: 0x000E8250
		protected override void Dispose(bool disposing)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			if (!this.m_bDisposed)
			{
				lock (this.lockClob)
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

		// Token: 0x060015CA RID: 5578 RVA: 0x000EA144 File Offset: 0x000E8344
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
				if (this.m_clobImpl.m_isTemporaryLob && !this.m_clobImpl.m_doneTempLobCreate)
				{
					this.m_clobImpl.CreateTemporaryLob();
					this.m_clobImpl.m_doneTempLobCreate = true;
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

		// Token: 0x060015CB RID: 5579 RVA: 0x000EA204 File Offset: 0x000E8404
		internal int GetOptimumChunkSize()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int optimumChunkSize;
			try
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (this.m_clobImpl.m_isTemporaryLob && !this.m_clobImpl.m_doneTempLobCreate)
				{
					this.CreateTempLob();
				}
				this.m_clobImpl.m_optimumChunkSize = (int)this.m_clobImpl.GetChunkSize() * 2;
				optimumChunkSize = this.m_clobImpl.m_optimumChunkSize;
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
			return optimumChunkSize;
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x000EA2D4 File Offset: 0x000E84D4
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
				OracleClobImpl clobImpl = this.m_clobImpl;
				if (this.IsTemporary)
				{
					this.m_clobImpl = new OracleClobImpl(this.m_connection.m_oracleConnectionImpl, null, clobImpl.m_isNClob, clobImpl.m_bCache);
					this.m_clobImpl.m_isTemporaryLob = true;
					this.CreateTempLob();
					clobImpl.CopyTo(this.m_clobImpl.m_lobLocator, 1L, 1L, clobImpl.GetLength());
				}
				else
				{
					byte[] array = new byte[this.m_clobImpl.m_lobLocator.Length];
					Array.Copy(this.m_clobImpl.m_lobLocator, array, array.Length);
					this.m_clobImpl = new OracleClobImpl(this.m_connection.m_oracleConnectionImpl, array, clobImpl.m_isNClob, clobImpl.m_bCache);
					this.m_clobImpl.m_isTemporaryLob = false;
				}
				clobImpl.RelRef();
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

		// Token: 0x040019B6 RID: 6582
		public const long MaxSize = 4294967295L;

		// Token: 0x040019B7 RID: 6583
		internal OracleClobImpl m_clobImpl;

		// Token: 0x040019B8 RID: 6584
		internal OracleConnection m_connection;

		// Token: 0x040019B9 RID: 6585
		private OracleCommand m_command;

		// Token: 0x040019BA RID: 6586
		private bool m_bNotNull;

		// Token: 0x040019BB RID: 6587
		private bool m_isInChunkWriteMode;

		// Token: 0x040019BC RID: 6588
		private long m_position;

		// Token: 0x040019BD RID: 6589
		internal bool m_bClosed;

		// Token: 0x040019BE RID: 6590
		internal bool m_bDisposed;

		// Token: 0x040019BF RID: 6591
		private object m_lock;

		// Token: 0x040019C0 RID: 6592
		private object lockClob;

		// Token: 0x040019C1 RID: 6593
		public new static readonly OracleClob Null = new OracleClob();
	}
}
