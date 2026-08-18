using System;
using System.Data;
using System.IO;
using System.Xml;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x02000256 RID: 598
	public sealed class OracleXmlType : IDisposable, ICloneable, INullable
	{
		// Token: 0x0600181D RID: 6173 RVA: 0x000FD5C0 File Offset: 0x000FB7C0
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

		// Token: 0x0600181E RID: 6174 RVA: 0x000FD64C File Offset: 0x000FB84C
		public OracleXmlType(OracleConnection con, string xmlData)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
				{
					" (ENTRY) OracleXmlType::OracleXmlType(con, string)\n"
				});
			}
			if (con == null)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentNullException("con", null);
			}
			if (ConnectionState.Open != con.m_connectionState)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_CLOSED, new string[0]));
			}
			if (xmlData == null)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentNullException("xmlData");
			}
			if (xmlData.Length == 0)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ODP_INVALID_VALUE, new string[]
				{
					"xmlData"
				}), "xmlData");
			}
			this.m_connection = con;
			this.m_xmlTypeImpl = new OracleXmlTypeImpl(this.m_connection.m_oracleConnectionImpl, TypeOfXmlType.String, TypeOfXmlData.String, xmlData);
			this.m_xmlTypeImpl.GetXmlDocument(true, true);
			if (this.m_connection.m_oracleConnectionImpl != null)
			{
				this.m_connection.m_oracleConnectionImpl.RegisterForConnectionClose(this);
			}
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[]
				{
					" (ENTRY) OracleXmlType::OracleXmlType(con, string)\n"
				});
			}
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x000FD798 File Offset: 0x000FB998
		public OracleXmlType(OracleClob clob)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
				{
					" (ENTRY) OracleXmlType::OracleXmlType(clob)\n"
				});
			}
			if (clob == null)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentNullException("clob");
			}
			if (clob.m_connection == null)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ODP_INVALID_VALUE, new string[]
				{
					"clob"
				}));
			}
			if (ConnectionState.Open != clob.m_connection.m_connectionState)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_CLOSED, new string[0]));
			}
			this.m_connection = clob.Connection;
			if (clob.IsEmpty)
			{
				this.m_bIsEmpty = true;
			}
			this.m_xmlTypeImpl = new OracleXmlTypeImpl(this.m_connection.m_oracleConnectionImpl, TypeOfXmlType.Clob, TypeOfXmlData.Clob, clob);
			this.m_xmlTypeImpl.GetXmlDocument(true, true);
			if (this.m_connection.m_oracleConnectionImpl != null)
			{
				this.m_connection.m_oracleConnectionImpl.RegisterForConnectionClose(this);
			}
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[]
				{
					" (ENTRY) OracleXmlType::OracleXmlType(clob)\n"
				});
			}
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x000FD8E4 File Offset: 0x000FBAE4
		public OracleXmlType(OracleConnection con, XmlReader reader)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
				{
					" (ENTRY) OracleXmlType::OracleXmlType(con, xmlreader)\n"
				});
			}
			if (con == null)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentNullException("con", null);
			}
			if (ConnectionState.Open != con.m_connectionState)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_CLOSED, new string[0]));
			}
			if (reader == null)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentNullException("reader");
			}
			string text = DotNetXmlImpl.ConvertXmlReaderToString(reader);
			if (string.IsNullOrEmpty(text))
			{
				GC.SuppressFinalize(this);
				throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ODP_INVALID_VALUE, new string[]
				{
					"reader"
				}), "reader");
			}
			this.m_connection = con;
			this.m_xmlTypeImpl = new OracleXmlTypeImpl(this.m_connection.m_oracleConnectionImpl, TypeOfXmlType.String, TypeOfXmlData.String, text);
			if (this.m_connection.m_oracleConnectionImpl != null)
			{
				this.m_connection.m_oracleConnectionImpl.RegisterForConnectionClose(this);
			}
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[]
				{
					" (ENTRY) OracleXmlType::OracleXmlType(con, xmlreader)\n"
				});
			}
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x000FDA2C File Offset: 0x000FBC2C
		public OracleXmlType(OracleConnection con, XmlDocument domDoc)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
				{
					" (ENTRY) OracleXmlType::OracleXmlType(con, xmldocument)\n"
				});
			}
			if (con == null)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentNullException("con");
			}
			if (ConnectionState.Open != con.m_connectionState)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_CLOSED, new string[0]));
			}
			if (domDoc == null)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentNullException("domDoc");
			}
			string text = DotNetXmlImpl.ConvertXmlDocToString(domDoc);
			if (string.IsNullOrEmpty(text))
			{
				GC.SuppressFinalize(this);
				throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ODP_INVALID_VALUE, new string[]
				{
					"domDoc"
				}), "domDoc");
			}
			this.m_connection = con;
			this.m_xmlTypeImpl = new OracleXmlTypeImpl(this.m_connection.m_oracleConnectionImpl, TypeOfXmlType.String, TypeOfXmlData.String, text);
			if (this.m_connection.m_oracleConnectionImpl != null)
			{
				this.m_connection.m_oracleConnectionImpl.RegisterForConnectionClose(this);
			}
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[]
				{
					" (ENTRY) OracleXmlType::OracleXmlType(con, xmldocument)\n"
				});
			}
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x000FDB70 File Offset: 0x000FBD70
		internal OracleXmlType(OracleConnection conn, OracleXmlTypeImpl xmlTypeImpl)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[]
				{
					" (ENTRY) OracleXmlType::OracleXmlType(conn, OracleXmlTypeImpl)\n"
				});
			}
			this.m_connection = conn;
			this.m_xmlTypeImpl = xmlTypeImpl;
			this.m_xmlTypeImpl.Initialize(this.m_connection);
			if (this.m_xmlTypeImpl.IsEmptyXmlTypeData())
			{
				this.m_bIsEmpty = true;
			}
			if (this.m_connection.m_oracleConnectionImpl != null)
			{
				this.m_connection.m_oracleConnectionImpl.RegisterForConnectionClose(this);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[]
				{
					" (EXIT) OracleXmlType::OracleXmlType(conn, OracleXmlTypeImpl)\n"
				});
			}
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x000FDC40 File Offset: 0x000FBE40
		internal OracleXmlType(OracleConnection con) : this(con, new OracleXmlTypeImpl(con.m_oracleConnectionImpl, TypeOfXmlType.String, TypeOfXmlData.String, ""))
		{
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x000FDC5C File Offset: 0x000FBE5C
		internal OracleXmlType(OracleXmlType orgXmlType)
		{
			this.m_connection = orgXmlType.m_connection;
			this.m_xmlTypeImpl = orgXmlType.m_xmlTypeImpl.Clone();
			this.m_bPopluateSchema = orgXmlType.m_bPopluateSchema;
			if (this.m_bPopluateSchema)
			{
				this.m_bIsSchemaBased = orgXmlType.m_bIsSchemaBased;
				this.m_schemaURL = orgXmlType.m_schemaURL;
				this.m_schemaClob = orgXmlType.m_schemaClob;
			}
			this.m_bIsEmpty = orgXmlType.m_bIsEmpty;
			this.m_bGotRootElement = orgXmlType.m_bGotRootElement;
			if (this.m_bGotRootElement)
			{
				this.m_rootElement = orgXmlType.m_rootElement;
			}
			if (this.m_connection != null && this.m_connection.m_oracleConnectionImpl != null)
			{
				this.m_connection.m_oracleConnectionImpl.RegisterForConnectionClose(this);
			}
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x000FDD40 File Offset: 0x000FBF40
		internal OracleXmlType(OracleConnection con, string xmlData, bool bThrowException)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[]
				{
					" (ENTRY) OracleXmlType::OracleXmlType(con, string)\n"
				});
			}
			if (con == null)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentNullException("con", null);
			}
			if (ConnectionState.Open != con.m_connectionState)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_CLOSED, new string[0]));
			}
			if (xmlData == null)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentNullException("xmlData");
			}
			if (xmlData.Length == 0)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ODP_INVALID_VALUE, new string[]
				{
					"xmlData"
				}), "xmlData");
			}
			this.m_connection = con;
			this.m_xmlTypeImpl = new OracleXmlTypeImpl(this.m_connection.m_oracleConnectionImpl, TypeOfXmlType.String, TypeOfXmlData.String, xmlData);
			if (this.m_connection.m_oracleConnectionImpl != null)
			{
				this.m_connection.m_oracleConnectionImpl.RegisterForConnectionClose(this);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[]
				{
					" (ENTRY) OracleXmlType::OracleXmlType(con, string)\n"
				});
			}
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x000FDE80 File Offset: 0x000FC080
		private OracleXmlType()
		{
			this.m_bNotNull = false;
			this.m_bIsEmpty = true;
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001827 RID: 6183 RVA: 0x000FDEC0 File Offset: 0x000FC0C0
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

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x000FDEF0 File Offset: 0x000FC0F0
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
				return this.m_bIsEmpty;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001829 RID: 6185 RVA: 0x000FDF24 File Offset: 0x000FC124
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x000FDF30 File Offset: 0x000FC130
		public bool IsSchemaBased
		{
			get
			{
				bool result;
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
					if (this.m_bIsEmpty)
					{
						this.m_bPopluateSchema = true;
						result = (this.m_bIsSchemaBased = false);
					}
					else
					{
						if (!this.m_xmlTypeImpl.m_bGotSchemaInfo)
						{
							this.m_xmlTypeImpl.GetXmlDocument(true, false);
						}
						string text;
						result = this.GetSchemaInfo(false, out text);
					}
				}
				catch (Exception ex)
				{
					OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
					throw;
				}
				return result;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x000FDFD0 File Offset: 0x000FC1D0
		public bool IsFragment
		{
			get
			{
				bool result;
				try
				{
					if (this.m_bClosed)
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
					}
					if (!this.m_bNotNull)
					{
						result = false;
					}
					else if (this.m_bIsEmpty)
					{
						result = false;
					}
					else if (this.m_xmlTypeImpl.m_bGotFragmentProp)
					{
						result = this.m_xmlTypeImpl.m_bIsFragment;
					}
					else
					{
						this.m_xmlTypeImpl.GetXmlDocument(true, false);
						result = this.m_xmlTypeImpl.m_bIsFragment;
					}
				}
				catch (Exception ex)
				{
					OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
					throw;
				}
				return result;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x0600182C RID: 6188 RVA: 0x000FE06C File Offset: 0x000FC26C
		public string RootElement
		{
			get
			{
				string result;
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
					if (this.m_bGotRootElement)
					{
						result = this.m_rootElement;
					}
					else if (this.m_bIsEmpty)
					{
						this.m_bGotRootElement = true;
						result = (this.m_rootElement = string.Empty);
					}
					else if (this.m_xmlTypeImpl.m_bIsFragment)
					{
						this.m_bGotRootElement = true;
						result = (this.m_rootElement = string.Empty);
					}
					else
					{
						XmlDocument xmlDocument = this.m_xmlTypeImpl.GetXmlDocument(true, false);
						this.m_rootElement = DotNetXmlImpl.GetRootElement(xmlDocument);
						this.m_bGotRootElement = true;
						result = this.m_rootElement;
					}
				}
				catch (Exception ex)
				{
					OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
					throw;
				}
				return result;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x000FE14C File Offset: 0x000FC34C
		public OracleXmlType Schema
		{
			get
			{
				OracleXmlType result;
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
					if (this.m_bIsEmpty)
					{
						this.m_bPopluateSchema = true;
						result = new OracleXmlType(this.m_connection);
					}
					else
					{
						if (!this.m_xmlTypeImpl.m_bGotSchemaInfo)
						{
							this.m_xmlTypeImpl.GetXmlDocument(true, false);
						}
						string text;
						this.GetSchemaInfo(true, out text);
						if (this.m_bIsSchemaBased && text != null)
						{
							result = new OracleXmlType(this.m_connection, text);
						}
						else
						{
							result = new OracleXmlType(this.m_connection);
						}
					}
				}
				catch (Exception ex)
				{
					OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
					throw;
				}
				return result;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x0600182E RID: 6190 RVA: 0x000FE210 File Offset: 0x000FC410
		public string SchemaUrl
		{
			get
			{
				string result;
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
					if (this.m_bIsEmpty)
					{
						this.m_bPopluateSchema = true;
						result = string.Empty;
					}
					else
					{
						if (!this.m_xmlTypeImpl.m_bGotSchemaInfo)
						{
							this.m_xmlTypeImpl.GetXmlDocument(true, false);
						}
						string text;
						if (this.GetSchemaInfo(false, out text))
						{
							result = this.m_schemaURL;
						}
						else
						{
							result = string.Empty;
						}
					}
				}
				catch (Exception ex)
				{
					OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
					throw;
				}
				return result;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x000FE2B8 File Offset: 0x000FC4B8
		public string Value
		{
			get
			{
				string @string;
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
					@string = this.m_xmlTypeImpl.GetString();
				}
				catch (Exception ex)
				{
					OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
					throw;
				}
				return @string;
			}
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x000FE320 File Offset: 0x000FC520
		public void Dispose()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
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

		// Token: 0x06001831 RID: 6193 RVA: 0x000FE39C File Offset: 0x000FC59C
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
					result = OracleXmlType.Null;
				}
				else
				{
					result = new OracleXmlType(this);
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

		// Token: 0x06001832 RID: 6194 RVA: 0x000FE440 File Offset: 0x000FC640
		public OracleXmlType Extract(string xpathExpr, string nsMap)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
				{
					"OracleXmlType::Extract(xpath, nsmap)"
				});
			}
			OracleXmlType result;
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
				if (string.IsNullOrEmpty(xpathExpr))
				{
					throw new ArgumentNullException("xpathExpr");
				}
				if (this.m_bIsEmpty)
				{
					result = new OracleXmlType(this.m_connection);
				}
				else
				{
					XmlDocument xmlDocument = this.m_xmlTypeImpl.GetXmlDocument(true, false);
					string text;
					if (this.m_xmlTypeImpl.m_xmlDocFragmentInternal != null)
					{
						text = DotNetXmlImpl.Extract(xmlDocument, "/" + xpathExpr, nsMap);
					}
					else
					{
						text = DotNetXmlImpl.Extract(xmlDocument, xpathExpr, nsMap);
					}
					if (!string.IsNullOrEmpty(text))
					{
						OracleXmlType oracleXmlType = new OracleXmlType(this.m_connection, text, false);
						oracleXmlType.m_xmlTypeImpl.m_bIsFragment = true;
						oracleXmlType.m_xmlTypeImpl.m_bGotFragmentProp = true;
						oracleXmlType.m_rootElement = string.Empty;
						this.m_bGotRootElement = true;
						result = oracleXmlType;
					}
					else
					{
						result = new OracleXmlType(this.m_connection);
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
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[]
					{
						"OracleXmlType::Extract(xpath, nsmap)"
					});
				}
			}
			return result;
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x000FE5B0 File Offset: 0x000FC7B0
		public OracleXmlType Extract(string xpathExpr, XmlNamespaceManager nsMgr)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
				{
					"OracleXmlType::Extract(xpath, nsmgr)"
				});
			}
			OracleXmlType result;
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
				if (string.IsNullOrEmpty(xpathExpr))
				{
					throw new ArgumentNullException("xpathExpr");
				}
				if (this.m_bIsEmpty)
				{
					result = new OracleXmlType(this.m_connection);
				}
				else
				{
					XmlDocument xmlDocument = this.m_xmlTypeImpl.GetXmlDocument(true, false);
					string text;
					if (this.m_xmlTypeImpl.m_xmlDocFragmentInternal != null)
					{
						text = DotNetXmlImpl.Extract(xmlDocument, "/" + xpathExpr, nsMgr);
					}
					else
					{
						text = DotNetXmlImpl.Extract(xmlDocument, xpathExpr, nsMgr);
					}
					if (!string.IsNullOrEmpty(text))
					{
						OracleXmlType oracleXmlType = new OracleXmlType(this.m_connection, text, false);
						oracleXmlType.m_xmlTypeImpl.m_bIsFragment = true;
						oracleXmlType.m_xmlTypeImpl.m_bGotFragmentProp = true;
						oracleXmlType.m_rootElement = string.Empty;
						this.m_bGotRootElement = true;
						result = oracleXmlType;
					}
					else
					{
						result = new OracleXmlType(this.m_connection);
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
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[]
					{
						"OracleXmlType::Extract(xpath, nsmgr)"
					});
				}
			}
			return result;
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x000FE720 File Offset: 0x000FC920
		public OracleXmlStream GetStream()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleXmlStream result;
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
				result = new OracleXmlStream(this);
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

		// Token: 0x06001835 RID: 6197 RVA: 0x000FE7C0 File Offset: 0x000FC9C0
		public XmlDocument GetXmlDocument()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			XmlDocument result;
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
				if (this.m_bIsEmpty)
				{
					result = new XmlDocument();
				}
				else
				{
					string value = this.Value;
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.PreserveWhitespace = true;
					xmlDocument.LoadXml(value);
					result = xmlDocument;
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

		// Token: 0x06001836 RID: 6198 RVA: 0x000FE888 File Offset: 0x000FCA88
		public XmlReader GetXmlReader()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			XmlReader result;
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
				if (this.m_bIsEmpty)
				{
					result = XmlReader.Create(new StringReader(""));
				}
				else
				{
					string value = this.Value;
					TextReader input = new StringReader(value);
					XmlReader xmlReader = new XmlTextReader(input);
					result = xmlReader;
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

		// Token: 0x06001837 RID: 6199 RVA: 0x000FE95C File Offset: 0x000FCB5C
		public bool IsExists(string xpathExpr, string nsMap)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
				{
					"OracleXmlType::IsExists(xpath, nsmap)"
				});
			}
			bool result;
			try
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (!this.m_bNotNull)
				{
					result = false;
				}
				else
				{
					if (string.IsNullOrEmpty(xpathExpr))
					{
						throw new ArgumentNullException("xpathExpr");
					}
					if (this.m_bIsEmpty)
					{
						result = false;
					}
					else
					{
						XmlDocument xmlDocument = this.m_xmlTypeImpl.GetXmlDocument(true, false);
						if (this.m_xmlTypeImpl.m_xmlDocFragmentInternal != null)
						{
							result = DotNetXmlImpl.IsExists(xmlDocument, "/" + xpathExpr, nsMap);
						}
						else
						{
							result = DotNetXmlImpl.IsExists(xmlDocument, xpathExpr, nsMap);
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
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[]
					{
						"OracleXmlType::IsExists(xpath, nsmap)"
					});
				}
			}
			return result;
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x000FEA68 File Offset: 0x000FCC68
		public bool IsExists(string xpathExpr, XmlNamespaceManager nsMgr)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
				{
					"OracleXmlType::IsExists(xpath, nsmgr)"
				});
			}
			bool result;
			try
			{
				if (this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (!this.m_bNotNull)
				{
					result = false;
				}
				else
				{
					if (string.IsNullOrEmpty(xpathExpr))
					{
						throw new ArgumentNullException("xpathExpr", null);
					}
					if (this.m_bIsEmpty)
					{
						result = false;
					}
					else
					{
						XmlDocument xmlDocument = this.m_xmlTypeImpl.GetXmlDocument(true, false);
						if (this.m_xmlTypeImpl.m_xmlDocFragmentInternal != null)
						{
							result = DotNetXmlImpl.IsExists(xmlDocument, "/" + xpathExpr, nsMgr);
						}
						else
						{
							result = DotNetXmlImpl.IsExists(xmlDocument, xpathExpr, nsMgr);
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
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[]
					{
						"OracleXmlType::IsExists(xpath, nsmgr)"
					});
				}
			}
			return result;
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x000FEB74 File Offset: 0x000FCD74
		public OracleXmlType Transform(OracleXmlType xsldoc, string paramMap)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
				{
					"OracleXmlType::Transform(xmltypexsldoc, paramMap)"
				});
			}
			OracleXmlType result;
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
				if (xsldoc == null || xsldoc.IsNull)
				{
					throw new ArgumentNullException("xsldoc");
				}
				if (this.m_bIsEmpty)
				{
					result = new OracleXmlType(this.m_connection);
				}
				else
				{
					XmlDocument xmlDocument = this.m_xmlTypeImpl.GetXmlDocument(true, false);
					string text = DotNetXmlImpl.Transform(xsldoc, xmlDocument, paramMap);
					if (!string.IsNullOrEmpty(text))
					{
						result = new OracleXmlType(this.m_connection, text, false);
					}
					else
					{
						result = new OracleXmlType(this.m_connection);
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
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[]
					{
						"OracleXmlType::Transform(xmltypexsldoc, paramMap)"
					});
				}
			}
			return result;
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x000FEC90 File Offset: 0x000FCE90
		public OracleXmlType Transform(string xsldoc, string paramMap)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
				{
					"OracleXmlType::Transform(xsldoc, paramMap)"
				});
			}
			OracleXmlType result;
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
				if (string.IsNullOrEmpty(xsldoc))
				{
					throw new ArgumentNullException("xsldoc");
				}
				if (this.m_bIsEmpty)
				{
					result = new OracleXmlType(this.m_connection);
				}
				else
				{
					XmlDocument xmlDocument = this.m_xmlTypeImpl.GetXmlDocument(true, false);
					string text = DotNetXmlImpl.Transform(xsldoc, xmlDocument, paramMap);
					if (!string.IsNullOrEmpty(text))
					{
						result = new OracleXmlType(this.m_connection, text, false);
					}
					else
					{
						result = new OracleXmlType(this.m_connection);
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
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[]
					{
						"OracleXmlType::Transform(xsldoc, paramMap)"
					});
				}
			}
			return result;
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x000FEDA8 File Offset: 0x000FCFA8
		public void Update(string xpathExpr, string nsMap, OracleXmlType val)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
				{
					"OracleXmlType::Update(xpathexpr, nsmap, xmltypeval)"
				});
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
				if (string.IsNullOrEmpty(xpathExpr))
				{
					throw new ArgumentNullException("xpathExpr");
				}
				if (val == null)
				{
					throw new ArgumentNullException("val");
				}
				if (string.IsNullOrEmpty(nsMap))
				{
					nsMap = null;
				}
				XmlDocument xmlDocument = this.m_xmlTypeImpl.GetXmlDocument(true, false);
				bool bOriginalFragmentValue;
				bool bValueIsFragment;
				if (this.m_xmlTypeImpl.m_xmlDocFragmentInternal != null)
				{
					bValueIsFragment = (bOriginalFragmentValue = true);
					DotNetXmlImpl.Update(xmlDocument, "/" + xpathExpr, nsMap, val.Value, ref bValueIsFragment);
				}
				else
				{
					bValueIsFragment = (bOriginalFragmentValue = false);
					DotNetXmlImpl.Update(xmlDocument, xpathExpr, nsMap, val.Value, ref bValueIsFragment);
				}
				this.m_xmlTypeImpl.Invalidate(bOriginalFragmentValue, bValueIsFragment, ref this.m_bGotRootElement);
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
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[]
					{
						"OracleXmlType::Update(xpathexpr, nsmap, xmltypeval)"
					});
				}
			}
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x000FEEEC File Offset: 0x000FD0EC
		public void Update(string xpathExpr, XmlNamespaceManager nsMgr, OracleXmlType val)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
				{
					"OracleXmlType::Update(xpathexpr, nsMgr, xmltypeval)"
				});
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
				if (string.IsNullOrEmpty(xpathExpr))
				{
					throw new ArgumentNullException("xpathExpr");
				}
				if (val == null)
				{
					throw new ArgumentNullException("val");
				}
				XmlDocument xmlDocument = this.m_xmlTypeImpl.GetXmlDocument(true, false);
				bool bOriginalFragmentValue;
				bool bValueIsFragment;
				if (this.m_xmlTypeImpl.m_xmlDocFragmentInternal != null)
				{
					bValueIsFragment = (bOriginalFragmentValue = true);
					DotNetXmlImpl.Update(xmlDocument, "/" + xpathExpr, nsMgr, val.Value, ref bValueIsFragment);
				}
				else
				{
					bValueIsFragment = (bOriginalFragmentValue = false);
					DotNetXmlImpl.Update(xmlDocument, xpathExpr, nsMgr, val.Value, ref bValueIsFragment);
				}
				this.m_xmlTypeImpl.Invalidate(bOriginalFragmentValue, bValueIsFragment, ref this.m_bGotRootElement);
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
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[]
					{
						"OracleXmlType::Update(xpathexpr, nsMgr, xmltypeval)"
					});
				}
			}
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x000FF024 File Offset: 0x000FD224
		public void Update(string xpathExpr, string nsMap, string val)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
				{
					"OracleXmlType::Update(xpathexpr, nsmap, stringval)"
				});
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
				if (string.IsNullOrEmpty(xpathExpr))
				{
					throw new ArgumentNullException("xpathExpr");
				}
				if (val == null)
				{
					throw new ArgumentNullException("val");
				}
				if (string.IsNullOrEmpty(nsMap))
				{
					nsMap = null;
				}
				XmlDocument xmlDocument = this.m_xmlTypeImpl.GetXmlDocument(true, false);
				bool bOriginalFragmentValue;
				bool bValueIsFragment;
				if (this.m_xmlTypeImpl.m_xmlDocFragmentInternal != null)
				{
					bValueIsFragment = (bOriginalFragmentValue = true);
					DotNetXmlImpl.Update(xmlDocument, "/" + xpathExpr, nsMap, val, ref bValueIsFragment);
				}
				else
				{
					bValueIsFragment = (bOriginalFragmentValue = false);
					DotNetXmlImpl.Update(xmlDocument, xpathExpr, nsMap, val, ref bValueIsFragment);
				}
				this.m_xmlTypeImpl.Invalidate(bOriginalFragmentValue, bValueIsFragment, ref this.m_bGotRootElement);
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
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[]
					{
						"OracleXmlType::Update(xpathexpr, nsmap, stringval)"
					});
				}
			}
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x000FF15C File Offset: 0x000FD35C
		public void Update(string xpathExpr, XmlNamespaceManager nsMgr, string val)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
				{
					"OracleXmlType::Update(xpathexpr, nsMgr, xmltypeval)"
				});
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
				if (string.IsNullOrEmpty(xpathExpr))
				{
					throw new ArgumentNullException("xpathExpr");
				}
				if (val == null)
				{
					throw new ArgumentNullException("val");
				}
				XmlDocument xmlDocument = this.m_xmlTypeImpl.GetXmlDocument(true, false);
				bool bOriginalFragmentValue;
				bool bValueIsFragment;
				if (this.m_xmlTypeImpl.m_xmlDocFragmentInternal != null)
				{
					bValueIsFragment = (bOriginalFragmentValue = true);
					DotNetXmlImpl.Update(xmlDocument, "/" + xpathExpr, nsMgr, val, ref bValueIsFragment);
				}
				else
				{
					bValueIsFragment = (bOriginalFragmentValue = false);
					DotNetXmlImpl.Update(xmlDocument, xpathExpr, nsMgr, val, ref bValueIsFragment);
				}
				this.m_xmlTypeImpl.Invalidate(bOriginalFragmentValue, bValueIsFragment, ref this.m_bGotRootElement);
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
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[]
					{
						"OracleXmlType::Update(xpathexpr, nsMgr, xmltypeval)"
					});
				}
			}
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x000FF28C File Offset: 0x000FD48C
		public bool Validate(string schemaUrl)
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
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (string.IsNullOrEmpty(schemaUrl))
				{
					throw new ArgumentNullException("schemaUrl");
				}
				if (this.m_bIsEmpty)
				{
					result = false;
				}
				else
				{
					bool flag = false;
					XmlSchemaPool xmlSchemaPool = null;
					OracleClob oracleClob = null;
					string text = null;
					byte[] id = null;
					if (this.m_connection.m_oracleConnectionImpl != null && this.m_connection.m_oracleConnectionImpl.m_pm != null && this.m_connection.m_oracleConnectionImpl.m_pm.m_dictXmlSchemaPool != null)
					{
						lock (this.m_connection.m_oracleConnectionImpl.m_pm.m_dictXmlSchemaPoolLock)
						{
							if (this.m_connection.m_oracleConnectionImpl.m_pm.m_dictXmlSchemaPool.ContainsKey(this.m_connection.m_oracleConnectionImpl.ServiceName))
							{
								xmlSchemaPool = this.m_connection.m_oracleConnectionImpl.m_pm.m_dictXmlSchemaPool[this.m_connection.m_oracleConnectionImpl.ServiceName];
							}
							else
							{
								xmlSchemaPool = new XmlSchemaPool(200);
								this.m_connection.m_oracleConnectionImpl.m_pm.m_dictXmlSchemaPool[this.m_connection.m_oracleConnectionImpl.ServiceName] = xmlSchemaPool;
							}
						}
					}
					if (xmlSchemaPool != null && xmlSchemaPool.Contains(schemaUrl) && xmlSchemaPool[schemaUrl] != null)
					{
						CachedSchemaWithId cachedSchemaWithId = xmlSchemaPool[schemaUrl];
						text = cachedSchemaWithId.schemaInfo;
					}
					else
					{
						if (this.m_command == null)
						{
							this.m_command = new OracleCommand("", this.m_connection);
						}
						OraXmlImpl.GetSchema(this.m_command, this, schemaUrl, out oracleClob, out id);
						if (oracleClob != null && !oracleClob.IsNull && xmlSchemaPool != null)
						{
							text = oracleClob.Value;
							xmlSchemaPool[id] = new CachedSchemaWithUrl(schemaUrl, text);
							xmlSchemaPool[schemaUrl] = new CachedSchemaWithId(id, text);
						}
					}
					if (text != null)
					{
						DotNetXmlValidator dotNetXmlValidator = new DotNetXmlValidator(text);
						XmlReader xmlReader = this.m_xmlTypeImpl.GetXmlReader(dotNetXmlValidator.m_readerSettings);
						flag = dotNetXmlValidator.Validate(xmlReader);
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

		// Token: 0x06001840 RID: 6208 RVA: 0x000FF53C File Offset: 0x000FD73C
		internal void Dispose(bool disposing)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			if (!this.m_bClosed)
			{
				lock (this.lockXmlType)
				{
					try
					{
						if (this.m_bNotNull)
						{
							if (!this.m_bClosed)
							{
								this.Close();
							}
							if (disposing)
							{
								if (this.m_command != null)
								{
									this.m_command.Dispose();
								}
								this.m_command = null;
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
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
						}
					}
				}
			}
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x000FF61C File Offset: 0x000FD81C
		internal void Close()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			if (!this.m_bClosed)
			{
				lock (this.lockXmlType)
				{
					try
					{
						if (this.m_bNotNull)
						{
							if (!this.m_bClosed)
							{
								this.m_xmlTypeImpl.Dispose();
								if (this.m_schemaClob != null)
								{
									this.m_schemaClob.Dispose();
									this.m_schemaClob = null;
								}
								this.m_bClosed = true;
							}
						}
					}
					catch (Exception ex)
					{
						OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
					}
					finally
					{
						if (this.m_connection != null && this.m_connection.m_oracleConnectionImpl != null)
						{
							this.m_connection.m_oracleConnectionImpl.DeregisterForConnectionClose(this);
						}
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
						}
					}
				}
			}
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x000FF728 File Offset: 0x000FD928
		internal void Set(OracleConnection conn, OraXmlTypeHeader xmlTypeHeader, OraXmlTypeData xmlTypeData)
		{
			if (!this.m_bNotNull)
			{
				return;
			}
			this.m_xmlTypeImpl.Dispose();
			if (this.m_schemaClob != null)
			{
				this.m_schemaClob.Dispose();
			}
			this.m_bPopluateSchema = false;
			this.m_bIsSchemaBased = false;
			this.m_schemaURL = string.Empty;
			this.m_schemaClob = null;
			this.m_bGotRootElement = false;
			this.m_rootElement = string.Empty;
			this.m_xmlTypeImpl.Set(conn.m_oracleConnectionImpl, xmlTypeHeader, xmlTypeData);
			this.m_xmlTypeImpl.Initialize(conn);
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x000FF7B0 File Offset: 0x000FD9B0
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
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x000FF82C File Offset: 0x000FDA2C
		internal bool GetSchemaInfo(bool bGetSchemaValue, out string xmlSchemaValue)
		{
			xmlSchemaValue = null;
			if (this.m_bPopluateSchema && !bGetSchemaValue)
			{
				return this.m_bIsSchemaBased;
			}
			try
			{
				string text = null;
				byte[] array = null;
				bool flag;
				this.m_xmlTypeImpl.GetXmlSchemaProp(out text, out array, out flag);
				if (array != null || !string.IsNullOrEmpty(text))
				{
					text.Trim();
					if (flag)
					{
						string[] array2 = text.Split(new char[]
						{
							' ',
							'\n',
							'\t',
							'\r'
						});
						if (array2.Length >= 2)
						{
							text = array2[1];
						}
					}
				}
				if (array != null || !string.IsNullOrEmpty(text))
				{
					this.m_bIsSchemaBased = true;
				}
				else
				{
					this.m_bIsSchemaBased = false;
				}
				if (this.m_bIsSchemaBased)
				{
					XmlSchemaPool xmlSchemaPool = null;
					if (this.m_connection.m_oracleConnectionImpl != null && this.m_connection.m_oracleConnectionImpl.m_pm != null && this.m_connection.m_oracleConnectionImpl.m_pm.m_dictXmlSchemaPool != null)
					{
						lock (this.m_connection.m_oracleConnectionImpl.m_pm.m_dictXmlSchemaPoolLock)
						{
							if (this.m_connection.m_oracleConnectionImpl.m_pm.m_dictXmlSchemaPool.ContainsKey(this.m_connection.m_oracleConnectionImpl.ServiceName))
							{
								xmlSchemaPool = this.m_connection.m_oracleConnectionImpl.m_pm.m_dictXmlSchemaPool[this.m_connection.m_oracleConnectionImpl.ServiceName];
							}
							else
							{
								xmlSchemaPool = new XmlSchemaPool(200);
								this.m_connection.m_oracleConnectionImpl.m_pm.m_dictXmlSchemaPool[this.m_connection.m_oracleConnectionImpl.ServiceName] = xmlSchemaPool;
							}
						}
					}
					if (array != null && xmlSchemaPool != null && xmlSchemaPool.Contains(array) && xmlSchemaPool[array] != null)
					{
						CachedSchemaWithUrl cachedSchemaWithUrl = xmlSchemaPool[array];
						xmlSchemaValue = cachedSchemaWithUrl.schemaInfo;
						this.m_schemaURL = cachedSchemaWithUrl.schemaUrl;
						this.m_schemaID = array;
					}
					else if (text != null && xmlSchemaPool != null && xmlSchemaPool.Contains(text) && xmlSchemaPool[text] != null)
					{
						CachedSchemaWithId cachedSchemaWithId = xmlSchemaPool[text];
						xmlSchemaValue = cachedSchemaWithId.schemaInfo;
						this.m_schemaURL = text;
						this.m_schemaID = cachedSchemaWithId.schemaId;
					}
					else
					{
						if (this.m_schemaClob == null)
						{
							if (this.m_command == null)
							{
								this.m_command = new OracleCommand("", this.m_connection);
							}
							if (array != null)
							{
								OraXmlImpl.GetSchema(this.m_command, this, array, out this.m_schemaClob, out text);
							}
							else
							{
								OraXmlImpl.GetSchema(this.m_command, this, text, out this.m_schemaClob, out array);
							}
						}
						if (this.m_schemaClob != null && !this.m_schemaClob.IsNull && xmlSchemaPool != null)
						{
							if (bGetSchemaValue)
							{
								xmlSchemaValue = this.m_schemaClob.Value;
								xmlSchemaPool[array] = new CachedSchemaWithUrl(text, xmlSchemaValue);
								xmlSchemaPool[text] = new CachedSchemaWithId(array, xmlSchemaValue);
							}
							else
							{
								xmlSchemaPool[array] = null;
								xmlSchemaPool[text] = null;
							}
							this.m_schemaURL = text;
							this.m_schemaID = array;
						}
					}
				}
				this.m_bPopluateSchema = true;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
			}
			finally
			{
				this.m_bPopluateSchema = true;
			}
			return this.m_bIsSchemaBased;
		}

		// Token: 0x04001A66 RID: 6758
		internal OracleConnection m_connection;

		// Token: 0x04001A67 RID: 6759
		private bool m_bNotNull = true;

		// Token: 0x04001A68 RID: 6760
		private bool m_bClosed;

		// Token: 0x04001A69 RID: 6761
		private bool m_bPopluateSchema;

		// Token: 0x04001A6A RID: 6762
		private bool m_bIsSchemaBased;

		// Token: 0x04001A6B RID: 6763
		private bool m_bIsEmpty;

		// Token: 0x04001A6C RID: 6764
		private string m_schemaURL = string.Empty;

		// Token: 0x04001A6D RID: 6765
		private OracleClob m_schemaClob;

		// Token: 0x04001A6E RID: 6766
		private byte[] m_schemaID;

		// Token: 0x04001A6F RID: 6767
		private bool m_bGotRootElement;

		// Token: 0x04001A70 RID: 6768
		private string m_rootElement = string.Empty;

		// Token: 0x04001A71 RID: 6769
		private object lockXmlType = new object();

		// Token: 0x04001A72 RID: 6770
		internal OracleXmlTypeImpl m_xmlTypeImpl;

		// Token: 0x04001A73 RID: 6771
		internal OracleCommand m_command;

		// Token: 0x04001A74 RID: 6772
		public static readonly OracleXmlType Null = new OracleXmlType();
	}
}
