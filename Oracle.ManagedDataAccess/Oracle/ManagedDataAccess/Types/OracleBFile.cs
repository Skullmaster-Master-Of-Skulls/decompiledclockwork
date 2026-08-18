using System;
using System.Data;
using System.IO;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x02000243 RID: 579
	public sealed class OracleBFile : Stream, IDisposable, ICloneable, INullable
	{
		// Token: 0x060014EA RID: 5354 RVA: 0x000E1FB0 File Offset: 0x000E01B0
		public OracleBFile(OracleConnection con) : this(con, string.Empty, string.Empty)
		{
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x000E1FC4 File Offset: 0x000E01C4
		public OracleBFile(OracleConnection connection, string directoryName, string fileName)
		{
			this.m_bNotNull = true;
			this.lockBFile = new object();
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
				this.m_directoryName = directoryName;
				this.m_fileName = fileName;
				this.m_bfileImpl = new OracleBFileImpl(connection.m_oracleConnectionImpl, null);
				this.m_isTemporaryLob = false;
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

		// Token: 0x060014EC RID: 5356 RVA: 0x000E20CC File Offset: 0x000E02CC
		internal OracleBFile(OracleConnection connection, byte[] lobLocator)
		{
			this.m_bNotNull = true;
			this.lockBFile = new object();
			base..ctor();
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_connection = connection;
				this.m_directoryName = null;
				this.m_fileName = null;
				this.m_bfileImpl = new OracleBFileImpl(connection.m_oracleConnectionImpl, lobLocator);
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

		// Token: 0x060014ED RID: 5357 RVA: 0x000E2198 File Offset: 0x000E0398
		private OracleBFile()
		{
			this.m_bNotNull = true;
			this.lockBFile = new object();
			base..ctor();
			this.m_bNotNull = false;
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x000E21BC File Offset: 0x000E03BC
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

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x000E2248 File Offset: 0x000E0448
		public override bool CanRead
		{
			get
			{
				return !this.m_bNotNull || !this.m_bClosed;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x000E2260 File Offset: 0x000E0460
		public override bool CanSeek
		{
			get
			{
				return !this.m_bNotNull || !this.m_bClosed;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x000E2278 File Offset: 0x000E0478
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x000E227C File Offset: 0x000E047C
		public override long Length
		{
			get
			{
				if (!this.m_bNotNull)
				{
					return 0L;
				}
				if (this.m_bClosed || !this.m_isFileOpen)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				this.m_length = this.m_bfileImpl.GetLength();
				return this.m_length;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x000E22D4 File Offset: 0x000E04D4
		public byte[] Value
		{
			get
			{
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (this.m_bClosed || !this.m_isFileOpen)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
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

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x000E235C File Offset: 0x000E055C
		// (set) Token: 0x060014F5 RID: 5365 RVA: 0x000E2398 File Offset: 0x000E0598
		public override long Position
		{
			get
			{
				if (!this.m_bNotNull)
				{
					return 0L;
				}
				if (this.m_bClosed || !this.m_isFileOpen)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				return this.m_position;
			}
			set
			{
				if (!this.m_bNotNull)
				{
					return;
				}
				if (this.m_bClosed || !this.m_isFileOpen)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("Position");
				}
				this.m_position = value;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x000E23EC File Offset: 0x000E05EC
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x000E23F8 File Offset: 0x000E05F8
		public OracleConnection Connection
		{
			get
			{
				if (!this.m_bNotNull)
				{
					return null;
				}
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				return this.m_connection;
			}
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x000E2428 File Offset: 0x000E0628
		public override void Flush()
		{
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x000E242C File Offset: 0x000E062C
		// (set) Token: 0x060014FA RID: 5370 RVA: 0x000E247C File Offset: 0x000E067C
		public string DirectoryName
		{
			get
			{
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (this.m_directoryName == null)
				{
					this.GetDFNames();
				}
				return this.m_directoryName;
			}
			set
			{
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (this.IsOpen)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.LOB_BFILE_ALREADY_OPEN, new string[0]));
				}
				this.m_directoryName = value;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x000E24DC File Offset: 0x000E06DC
		public bool FileExists
		{
			get
			{
				if (!this.m_bNotNull)
				{
					return false;
				}
				if (this.m_bClosed || !this.m_isFileOpen)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				this.m_fileExists = this.m_bfileImpl.FileExists();
				return this.m_fileExists;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x000E2530 File Offset: 0x000E0730
		// (set) Token: 0x060014FD RID: 5373 RVA: 0x000E2580 File Offset: 0x000E0780
		public string FileName
		{
			get
			{
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (this.m_fileName == null)
				{
					this.GetDFNames();
				}
				return this.m_fileName;
			}
			set
			{
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (this.IsOpen)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.LOB_BFILE_ALREADY_OPEN, new string[0]));
				}
				this.m_fileName = value;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x000E25E0 File Offset: 0x000E07E0
		public bool IsEmpty
		{
			get
			{
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (this.m_bClosed || !this.m_isFileOpen)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (this.Length != 0L)
				{
					return this.m_isEmpty = false;
				}
				return this.m_isEmpty = true;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x000E2640 File Offset: 0x000E0840
		public bool IsOpen
		{
			get
			{
				if (!this.m_bNotNull)
				{
					return false;
				}
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				return this.m_isFileOpen;
			}
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x000E2670 File Offset: 0x000E0870
		public long Search(byte[] val, long offset, long nth)
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
					result = 0L;
				}
				else
				{
					if (this.m_bClosed || !this.m_isFileOpen)
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
					}
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
						this.m_command.Parameters.Add("current_bfile", OracleDbType.BFile, this, ParameterDirection.Input);
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

		// Token: 0x06001501 RID: 5377 RVA: 0x000E28A8 File Offset: 0x000E0AA8
		internal byte[] GetValue()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				bool flag = false;
				if (!this.m_isFileOpen)
				{
					this.OpenFile();
					flag = true;
				}
				byte[] array = null;
				try
				{
					array = this.Value;
				}
				finally
				{
					if (flag)
					{
						this.CloseFile();
					}
				}
				result = array;
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

		// Token: 0x06001502 RID: 5378 RVA: 0x000E294C File Offset: 0x000E0B4C
		internal byte[] GetLobLocator()
		{
			return this.m_bfileImpl.m_lobLocator;
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x000E295C File Offset: 0x000E0B5C
		internal void SetLobLocator(byte[] lobLocator, bool bTempLob)
		{
			this.m_bfileImpl.m_lobLocator = lobLocator;
			this.m_directoryName = null;
			this.m_fileName = null;
			this.m_isTemporaryLob = bTempLob;
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x000E2980 File Offset: 0x000E0B80
		public override long Seek(long offset, SeekOrigin origin)
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
					result = 0L;
				}
				else
				{
					if (this.m_bClosed || !this.m_isFileOpen)
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
					}
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

		// Token: 0x06001505 RID: 5381 RVA: 0x000E2A58 File Offset: 0x000E0C58
		public override void SetLength(long newLength)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x000E2A60 File Offset: 0x000E0C60
		public bool IsEqual(OracleBFile obj)
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
				if (this.m_bNotNull && !this.m_isFileOpen)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (obj == null)
				{
					throw new ArgumentNullException();
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
					if (!obj.m_isFileOpen)
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
					}
					if (obj.m_connection != this.m_connection)
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_DIFFERENT_CONNECTIONS, new string[0]));
					}
					bool flag = true;
					byte[] lobLocator = this.m_bfileImpl.m_lobLocator;
					byte[] lobLocator2 = obj.m_bfileImpl.m_lobLocator;
					if (lobLocator.Length != lobLocator2.Length)
					{
						flag = false;
					}
					else
					{
						int num = lobLocator.Length;
						for (int i = 0; i < num; i++)
						{
							if (i == 10)
							{
								i++;
							}
							else if (lobLocator[i] != lobLocator2[i])
							{
								flag = false;
								break;
							}
						}
					}
					result = flag;
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

		// Token: 0x06001507 RID: 5383 RVA: 0x000E2C00 File Offset: 0x000E0E00
		public void OpenFile()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (!this.IsOpen)
				{
					this.SetDFNames();
					this.m_bfileImpl.OpenFile();
					this.m_isFileOpen = true;
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

		// Token: 0x06001508 RID: 5384 RVA: 0x000E2CC0 File Offset: 0x000E0EC0
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				if (!this.m_bNotNull)
				{
					result = 0;
				}
				else
				{
					if (this.m_bClosed || !this.m_isFileOpen)
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
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
					if (count == 0)
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
						long num = this.m_bfileImpl.Read(locatorOffset, numBytesToRead, (long)offset, ref buffer);
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

		// Token: 0x06001509 RID: 5385 RVA: 0x000E2DF8 File Offset: 0x000E0FF8
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x000E2E00 File Offset: 0x000E1000
		public void CloseFile()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (this.m_isFileOpen)
				{
					this.m_bfileImpl.CloseFile();
					this.m_isFileOpen = false;
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

		// Token: 0x0600150B RID: 5387 RVA: 0x000E2EB8 File Offset: 0x000E10B8
		public int Compare(long src_offset, OracleBFile obj, long dst_offset, long amount)
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
				if (this.m_bNotNull && !this.m_isFileOpen)
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
					if (!obj.m_isFileOpen)
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
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
					int num = 0;
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
						this.m_command.Parameters.Add("provided_bfile", OracleDbType.BFile, obj, ParameterDirection.Input);
						this.m_command.Parameters.Add("current_bfile", OracleDbType.BFile, this, ParameterDirection.Input);
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

		// Token: 0x0600150C RID: 5388 RVA: 0x000E3194 File Offset: 0x000E1394
		public long CopyTo(OracleBlob obj)
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
				if (this.m_bClosed || !this.m_isFileOpen)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (obj == null)
				{
					throw new ArgumentNullException("obj");
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

		// Token: 0x0600150D RID: 5389 RVA: 0x000E3258 File Offset: 0x000E1458
		public long CopyTo(OracleBlob obj, long dst_offset)
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
				if (this.m_bClosed || !this.m_isFileOpen)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (obj == null)
				{
					throw new ArgumentNullException("obj");
				}
				if (obj.IsNull)
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

		// Token: 0x0600150E RID: 5390 RVA: 0x000E3328 File Offset: 0x000E1528
		public long CopyTo(OracleClob obj)
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
				if (this.m_bClosed || !this.m_isFileOpen)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (obj == null)
				{
					throw new ArgumentNullException("obj");
				}
				if (obj.IsNull)
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

		// Token: 0x0600150F RID: 5391 RVA: 0x000E33FC File Offset: 0x000E15FC
		public long CopyTo(OracleClob obj, long dst_offset)
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
				if (this.m_bClosed || !this.m_isFileOpen)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (obj == null)
				{
					throw new ArgumentNullException("obj");
				}
				if (obj.IsNull)
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

		// Token: 0x06001510 RID: 5392 RVA: 0x000E34CC File Offset: 0x000E16CC
		public long CopyTo(long src_offset, OracleBlob obj, long dst_offset, long amount)
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
				if (this.m_bClosed || !this.m_isFileOpen)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (obj == null)
				{
					throw new ArgumentNullException("obj");
				}
				if (obj.IsNull)
				{
					throw new OracleNullValueException();
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
				if (obj.m_connection != this.m_connection)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_DIFFERENT_CONNECTIONS, new string[0]));
				}
				if (obj.m_blobImpl.m_isTemporaryLob && !obj.m_blobImpl.m_doneTempLobCreate)
				{
					obj.CreateTempLob();
				}
				src_offset += 1L;
				dst_offset += 1L;
				result = this.m_bfileImpl.CopyTo(obj.m_blobImpl.m_lobLocator, src_offset, dst_offset, amount);
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

		// Token: 0x06001511 RID: 5393 RVA: 0x000E3628 File Offset: 0x000E1828
		public long CopyTo(long src_offset, OracleClob obj, long dst_offset, long amount)
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
				if (this.m_bClosed || !this.m_isFileOpen)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (obj == null)
				{
					throw new ArgumentNullException("obj");
				}
				if (obj.IsNull)
				{
					throw new OracleNullValueException();
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
				if (obj.m_connection != this.m_connection)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_DIFFERENT_CONNECTIONS, new string[0]));
				}
				if (obj.m_clobImpl.m_isTemporaryLob && !obj.m_clobImpl.m_doneTempLobCreate)
				{
					obj.CreateTempLob();
				}
				src_offset += 1L;
				dst_offset += 1L;
				result = this.m_bfileImpl.CopyTo(obj.m_clobImpl.m_lobLocator, src_offset, dst_offset, amount);
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

		// Token: 0x06001512 RID: 5394 RVA: 0x000E3784 File Offset: 0x000E1984
		public object Clone()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			object result;
			try
			{
				if (!this.m_bNotNull)
				{
					result = OracleBFile.Null;
				}
				else
				{
					if (this.m_bClosed)
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
					}
					byte[] array = null;
					if (this.m_bfileImpl.m_lobLocator != null)
					{
						array = new byte[this.m_bfileImpl.m_lobLocator.Length];
						Array.Copy(this.m_bfileImpl.m_lobLocator, array, array.Length);
					}
					result = new OracleBFile(this.m_connection, array)
					{
						m_directoryName = this.m_directoryName,
						m_fileName = this.m_fileName,
						m_fileExists = this.m_fileExists,
						m_position = this.m_position,
						m_bNotNull = this.m_bNotNull
					};
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

		// Token: 0x06001513 RID: 5395 RVA: 0x000E38A4 File Offset: 0x000E1AA4
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
				lock (this.lockBFile)
				{
					if (!this.m_bClosed)
					{
						try
						{
							if (this.m_connection != null && this.m_isFileOpen)
							{
								this.m_bfileImpl.CloseFile();
								this.m_isFileOpen = false;
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

		// Token: 0x06001514 RID: 5396 RVA: 0x000E39D0 File Offset: 0x000E1BD0
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

		// Token: 0x06001515 RID: 5397 RVA: 0x000E3A4C File Offset: 0x000E1C4C
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

		// Token: 0x06001516 RID: 5398 RVA: 0x000E3AD8 File Offset: 0x000E1CD8
		protected override void Dispose(bool disposing)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			if (!this.m_bDisposed)
			{
				lock (this.lockBFile)
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

		// Token: 0x06001517 RID: 5399 RVA: 0x000E3BCC File Offset: 0x000E1DCC
		internal void GetDFNames()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				bool flag = false;
				if (!this.m_isFileOpen)
				{
					this.m_bfileImpl.OpenFile();
					flag = true;
				}
				if (this.m_bfileImpl.m_lobLocator != null)
				{
					if (this.m_command == null)
					{
						this.m_command = new OracleCommand();
					}
					this.m_command.Connection = this.m_connection;
					this.m_command.CommandText = "BEGIN DBMS_LOB.FILEGETNAME(:LOB_1, :DIR_ALIAS, :FILENAME); END;";
					this.m_command.CommandType = CommandType.Text;
					try
					{
						this.m_command.Parameters.Add("provided_bfile", OracleDbType.BFile, this, ParameterDirection.Input);
						this.m_command.Parameters.Add("dir_alias", OracleDbType.Varchar2, ParameterDirection.Output);
						this.m_command.Parameters[1].Size = 30;
						this.m_command.Parameters.Add("filename", OracleDbType.Varchar2, ParameterDirection.Output);
						this.m_command.Parameters[2].Size = 255;
						this.m_command.ExecuteNonQuery();
						this.m_directoryName = ((OracleString)this.m_command.Parameters[1].Value).Value;
						this.m_fileName = ((OracleString)this.m_command.Parameters[2].Value).Value;
					}
					finally
					{
						this.m_command.Parameters.Clear();
						if (flag)
						{
							this.m_bfileImpl.CloseFile();
						}
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
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x000E3DD4 File Offset: 0x000E1FD4
		internal void SetDFNames()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_directoryName == null || this.m_fileName == null)
				{
					this.GetDFNames();
				}
				if (this.m_directoryName != null && this.m_directoryName.Length != 0 && this.m_fileName != null && this.m_fileName.Length != 0)
				{
					this.m_bfileImpl.SetDirFileName(this.m_directoryName, this.m_fileName);
					this.m_fileExists = true;
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

		// Token: 0x0400198E RID: 6542
		public const long MaxSize = 4294967295L;

		// Token: 0x0400198F RID: 6543
		internal OracleBFileImpl m_bfileImpl;

		// Token: 0x04001990 RID: 6544
		internal OracleConnection m_connection;

		// Token: 0x04001991 RID: 6545
		internal bool m_isTemporaryLob;

		// Token: 0x04001992 RID: 6546
		private OracleCommand m_command;

		// Token: 0x04001993 RID: 6547
		private string m_directoryName;

		// Token: 0x04001994 RID: 6548
		private bool m_fileExists;

		// Token: 0x04001995 RID: 6549
		private string m_fileName;

		// Token: 0x04001996 RID: 6550
		private long m_length;

		// Token: 0x04001997 RID: 6551
		private long m_position;

		// Token: 0x04001998 RID: 6552
		internal bool m_isEmpty;

		// Token: 0x04001999 RID: 6553
		private bool m_isFileOpen;

		// Token: 0x0400199A RID: 6554
		private bool m_bNotNull;

		// Token: 0x0400199B RID: 6555
		private bool m_bClosed;

		// Token: 0x0400199C RID: 6556
		private bool m_bDisposed;

		// Token: 0x0400199D RID: 6557
		private object lockBFile;

		// Token: 0x0400199E RID: 6558
		public new static readonly OracleBFile Null = new OracleBFile();
	}
}
