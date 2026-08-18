using System;
using System.Data;
using System.IO;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x02000255 RID: 597
	public sealed class OracleXmlStream : Stream, ICloneable
	{
		// Token: 0x06001808 RID: 6152 RVA: 0x000FCC4C File Offset: 0x000FAE4C
		public OracleXmlStream(OracleXmlType xmlType)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.m_xmlType = xmlType;
			OracleConnection connection = xmlType.m_connection;
			if (connection == null || connection.m_connectionState != ConnectionState.Open)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException();
			}
			if (connection.m_connectionState != ConnectionState.Open)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_CLOSED, new string[0]));
			}
			try
			{
				this.m_connection = xmlType.m_connection;
				this.m_xmlStreamImpl = new OracleXmlStreamImpl(this.m_connection.m_oracleConnectionImpl, xmlType.m_xmlTypeImpl);
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

		// Token: 0x06001809 RID: 6153 RVA: 0x000FCD6C File Offset: 0x000FAF6C
		private void Initialize(OracleXmlType xmlType)
		{
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x000FCD70 File Offset: 0x000FAF70
		public override bool CanRead
		{
			get
			{
				return !this.m_bClosed;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x000FCD80 File Offset: 0x000FAF80
		public override bool CanSeek
		{
			get
			{
				return !this.m_bClosed;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x000FCD90 File Offset: 0x000FAF90
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x000FCD94 File Offset: 0x000FAF94
		public OracleConnection Connection
		{
			get
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				return this.m_connection;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x000FCDBC File Offset: 0x000FAFBC
		public override long Length
		{
			get
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				long length;
				try
				{
					length = this.m_xmlStreamImpl.GetLength();
				}
				catch (Exception ex)
				{
					OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
					throw;
				}
				return length;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x000FCE18 File Offset: 0x000FB018
		public string Value
		{
			get
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				string value;
				try
				{
					value = this.m_xmlStreamImpl.GetValue();
				}
				catch (Exception ex)
				{
					OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
					throw;
				}
				return value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x000FCE74 File Offset: 0x000FB074
		// (set) Token: 0x06001811 RID: 6161 RVA: 0x000FCE9C File Offset: 0x000FB09C
		public override long Position
		{
			get
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				return this.m_position;
			}
			set
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				this.m_position = value;
			}
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x000FCEC4 File Offset: 0x000FB0C4
		public object Clone()
		{
			if (this.m_bClosed)
			{
				throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
			}
			return new OracleXmlStream(this.m_xmlType)
			{
				m_position = this.m_position
			};
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x000FCF0C File Offset: 0x000FB10C
		public override void Close()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.XML, new string[]
				{
					"OracleXmlStream::Close"
				});
			}
			if (!this.m_bClosed)
			{
				lock (this.lockXmlStream)
				{
					try
					{
						if (!this.m_bClosed)
						{
							this.m_xmlStreamImpl.Dispose();
							this.m_position = 0L;
							this.m_length = 0L;
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
						this.m_connection = null;
						if (ProviderConfig.m_bTraceLevelPublic)
						{
							Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
						}
					}
				}
			}
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x000FD028 File Offset: 0x000FB228
		public new void Dispose()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			if (!this.m_bClosed)
			{
				lock (this.lockXmlStream)
				{
					try
					{
						if (!this.m_bClosed)
						{
							this.Close();
						}
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
			}
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x000FD0D8 File Offset: 0x000FB2D8
		public override void Flush()
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
				throw new NotSupportedException(null, null);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
					goto IL_56;
				}
				goto IL_56;
				IL_56:;
			}
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x000FD14C File Offset: 0x000FB34C
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
				if (count == 0)
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
					result = this.m_xmlStreamImpl.Read(buffer, offset, count, ref this.m_position);
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

		// Token: 0x06001817 RID: 6167 RVA: 0x000FD224 File Offset: 0x000FB424
		public int Read(char[] buffer, int offset, int count)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
				{
					"OracleXmlStream::Read(char[], int, int)"
				});
			}
			int result;
			try
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (count == 0)
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
					if (this.m_position % 2L != 0L)
					{
						throw new ArgumentOutOfRangeException(null, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.EVEN_VALUE_PARAM_REQUIRED, new string[0]));
					}
					result = this.m_xmlStreamImpl.Read(buffer, offset, count, ref this.m_position);
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

		// Token: 0x06001818 RID: 6168 RVA: 0x000FD330 File Offset: 0x000FB530
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			long position;
			try
			{
				if (this.m_bClosed)
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
				position = this.m_position;
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
			return position;
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x000FD3F0 File Offset: 0x000FB5F0
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
				throw new NotSupportedException(null, null);
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
					goto IL_66;
				}
				goto IL_66;
				IL_66:;
			}
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x000FD480 File Offset: 0x000FB680
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
				throw new NotSupportedException(null, null);
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
					goto IL_66;
				}
				goto IL_66;
				IL_66:;
			}
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x000FD510 File Offset: 0x000FB710
		~OracleXmlStream()
		{
			this.Dispose();
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x000FD53C File Offset: 0x000FB73C
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
				this.m_bClosed = true;
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

		// Token: 0x04001A5F RID: 6751
		private bool m_bClosed;

		// Token: 0x04001A60 RID: 6752
		private long m_length;

		// Token: 0x04001A61 RID: 6753
		private long m_position;

		// Token: 0x04001A62 RID: 6754
		private OracleConnection m_connection;

		// Token: 0x04001A63 RID: 6755
		private OracleXmlType m_xmlType;

		// Token: 0x04001A64 RID: 6756
		internal OracleXmlStreamImpl m_xmlStreamImpl;

		// Token: 0x04001A65 RID: 6757
		private object lockXmlStream = new object();
	}
}
