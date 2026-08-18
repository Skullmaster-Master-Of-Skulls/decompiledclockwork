using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Xml;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000065 RID: 101
	public sealed class OracleXmlType : IDisposable, INullable
	{
		// Token: 0x060004B7 RID: 1207 RVA: 0x00036AD7 File Offset: 0x00035AD7
		static OracleXmlType()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00036AEF File Offset: 0x00035AEF
		internal OracleXmlType(OracleConnection con) : this(con, string.Empty, string.Empty)
		{
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00036B04 File Offset: 0x00035B04
		internal unsafe OracleXmlType(OracleConnection con, string rootElement, string schemaUrl)
		{
			this.m_bFreeOciXmlType = 1;
			this.m_bNotNull = true;
			base..ctor();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlType::OracleXmlType(con, string, schemaUrl)\n"
				});
			}
			int num = 0;
			this.m_bFreeOciXmlType = 1;
			if (con == null)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentNullException("con", null);
			}
			this.m_opsConCtx = con.m_opoConCtx.opsConCtx;
			if (this.m_opsConCtx == IntPtr.Zero)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			this.m_opoXmlTypeRefCtx = new OpoXmlTypeRefCtx();
			this.m_opoXmlTypeRefCtx.rootElement = rootElement;
			this.m_opoXmlTypeRefCtx.schemaUrl = schemaUrl;
			try
			{
				num = OpsXmlType.AllocXmlTypeCtxEmpty(this.m_opsConCtx, ref this.m_opsXmlTypeCtx, ref this.m_opsErrCtx, ref this.m_pOpoXmlTypeValCtx, this.m_opoXmlTypeRefCtx);
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
				if (num != 0)
				{
					GC.SuppressFinalize(this);
					throw new OracleTypeException(num, new object[]
					{
						"schemaurl"
					});
				}
			}
			this.m_pOpoXmlTypeValCtx->isFragment = 2;
			this.m_pOpoXmlTypeValCtx->isSchemaBased = 2;
			this.m_pOpoXmlTypeValCtx->isEmpty = 2;
			this.m_connection = con;
			this.m_conSignature = con.m_conSignature;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleXmlType::OracleXmlType(con, string, schemaUrl)\n"
				});
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00036C94 File Offset: 0x00035C94
		public OracleXmlType(OracleClob clob)
		{
			this.m_bFreeOciXmlType = 1;
			this.m_bNotNull = true;
			base..ctor();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlType::OracleXmlType(clob)\n"
				});
			}
			if (clob == null)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentNullException("clob", null);
			}
			if (clob.m_connection == null)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException();
			}
			this.m_bFreeOciXmlType = 1;
			this.Initialize(clob.m_connection, null, clob, 2);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleXmlType::OracleXmlType(clob)\n"
				});
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00036D30 File Offset: 0x00035D30
		public OracleXmlType(OracleConnection con, string xmlData)
		{
			this.m_bFreeOciXmlType = 1;
			this.m_bNotNull = true;
			base..ctor();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlType::OracleXmlType(con, string)\n"
				});
			}
			if (con == null)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentNullException("con", null);
			}
			if (xmlData == null)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentNullException("xmlData", null);
			}
			if (xmlData.Length == 0)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentException(string.Empty, "xmlData");
			}
			this.m_bFreeOciXmlType = 1;
			this.Initialize(con, xmlData, null, 1);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleXmlType::OracleXmlType(con, string)\n"
				});
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00036DE4 File Offset: 0x00035DE4
		internal OracleXmlType(string xmlData) : this(OracleConnection.GetInternalConnection(), xmlData)
		{
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00036DF4 File Offset: 0x00035DF4
		public OracleXmlType(OracleConnection con, XmlReader reader)
		{
			this.m_bFreeOciXmlType = 1;
			this.m_bNotNull = true;
			base..ctor();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlType::OracleXmlType(con, xmlreader)\n"
				});
			}
			if (con == null)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentNullException("con", null);
			}
			if (reader == null)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentNullException("reader", null);
			}
			this.m_bFreeOciXmlType = 1;
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			xmlDocument.Load(reader);
			string outerXml = xmlDocument.OuterXml;
			if (outerXml == null || outerXml.Length == 0)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentException(string.Empty, "reader");
			}
			this.Initialize(con, outerXml, null, 1);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleXmlType::OracleXmlType(con, xmlreader)\n"
				});
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00036ECC File Offset: 0x00035ECC
		public OracleXmlType(OracleConnection con, XmlDocument domDoc)
		{
			this.m_bFreeOciXmlType = 1;
			this.m_bNotNull = true;
			base..ctor();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlType::OracleXmlType(con, xmldocument)\n"
				});
			}
			if (con == null)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentNullException("con", null);
			}
			if (domDoc == null)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentNullException("domDoc", null);
			}
			string outerXml = domDoc.OuterXml;
			if (outerXml == null || outerXml.Length == 0)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentException(string.Empty, "domDoc");
			}
			this.m_bFreeOciXmlType = 1;
			this.Initialize(con, outerXml, null, 1);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleXmlType::OracleXmlType(con, xmldocument)\n"
				});
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00036F8C File Offset: 0x00035F8C
		internal unsafe OracleXmlType(OracleConnection con, IntPtr pOpsXmlTypeCtx, bool flag)
		{
			this.m_bFreeOciXmlType = 1;
			this.m_bNotNull = true;
			base..ctor();
			int num = 0;
			if (con == null)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException();
			}
			this.m_opsConCtx = con.m_opoConCtx.opsConCtx;
			if (this.m_opsConCtx == IntPtr.Zero)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			this.m_bFreeOciXmlType = 1;
			this.m_opoXmlTypeRefCtx = new OpoXmlTypeRefCtx();
			try
			{
				num = OpsXmlType.AllocCtx(this.m_opsConCtx, ref this.m_opsErrCtx, ref this.m_pOpoXmlTypeValCtx);
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
				if (num != 0)
				{
					GC.SuppressFinalize(this);
					throw new OracleTypeException(num, new object[]
					{
						"xml"
					});
				}
			}
			this.m_pOpoXmlTypeValCtx->isFragment = 2;
			this.m_pOpoXmlTypeValCtx->isSchemaBased = 2;
			this.m_pOpoXmlTypeValCtx->isEmpty = 2;
			this.m_opsXmlTypeCtx = pOpsXmlTypeCtx;
			if (flag)
			{
				try
				{
					OpsXmlType.AddRef(pOpsXmlTypeCtx);
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
					GC.SuppressFinalize(this);
					throw;
				}
			}
			this.m_connection = con;
			this.m_conSignature = con.m_conSignature;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000370EC File Offset: 0x000360EC
		internal OracleXmlType(IntPtr pOciXmlType, bool addRef, int allocOciXmlType) : this(OracleConnection.GetInternalConnection(), pOciXmlType, addRef, allocOciXmlType)
		{
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000370FC File Offset: 0x000360FC
		private OracleXmlType()
		{
			this.m_bFreeOciXmlType = 1;
			this.m_bNotNull = true;
			base..ctor();
			this.m_bNotNull = false;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0003711C File Offset: 0x0003611C
		internal unsafe OracleXmlType(OracleConnection con, IntPtr pOciXmlType, bool addRef, int allocOciXmlType)
		{
			this.m_bFreeOciXmlType = 1;
			this.m_bNotNull = true;
			base..ctor();
			int num = 0;
			if (con == null)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException();
			}
			this.m_opsConCtx = con.m_opoConCtx.opsConCtx;
			if (this.m_opsConCtx == IntPtr.Zero)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			this.m_bFreeOciXmlType = allocOciXmlType;
			this.m_opoXmlTypeRefCtx = new OpoXmlTypeRefCtx();
			try
			{
				num = OpsXmlType.AllocCtx(this.m_opsConCtx, ref this.m_opsErrCtx, ref this.m_pOpoXmlTypeValCtx);
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
				if (num != 0)
				{
					GC.SuppressFinalize(this);
					throw new OracleTypeException(num, new object[]
					{
						"xml"
					});
				}
			}
			this.m_pOpoXmlTypeValCtx->isFragment = 2;
			this.m_pOpoXmlTypeValCtx->isSchemaBased = 2;
			this.m_pOpoXmlTypeValCtx->isEmpty = 2;
			try
			{
				num = OpsXmlType.AllocNewCtx(this.m_opsConCtx, ref this.m_opsXmlTypeCtx, pOciXmlType, allocOciXmlType);
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					GC.SuppressFinalize(this);
					throw new OracleTypeException(num, new object[]
					{
						"xml"
					});
				}
			}
			if (addRef)
			{
				try
				{
					OpsXmlType.AddRef(this.m_opsXmlTypeCtx);
				}
				catch (Exception ex3)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex3);
					}
					GC.SuppressFinalize(this);
					throw;
				}
			}
			this.m_connection = con;
			this.m_conSignature = con.m_conSignature;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x000372E4 File Offset: 0x000362E4
		public void Dispose()
		{
			bool flag = true;
			if (!this.m_bNotNull)
			{
				return;
			}
			if (this.m_doneDispose)
			{
				return;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlType::Dispose()\n"
				});
			}
			try
			{
				if (OracleConnection.IsAvailable && this.m_connection != null && this.m_connection.m_extProcEnv != null)
				{
					Monitor.Enter(this.m_connection.m_extProcEnv);
					flag = this.m_connection.m_extProcEnv.m_status;
				}
				if (this.m_opoXmlTypeRefCtx != null && this.m_opoXmlTypeRefCtx.schema_opsXmlTypeCtx != IntPtr.Zero)
				{
					try
					{
						OpsXmlType.RelRef(this.m_opsConCtx, this.m_opsErrCtx, ref this.m_opoXmlTypeRefCtx.schema_opsXmlTypeCtx, flag ? this.m_bFreeOciXmlType : 0);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
					}
					this.m_opoXmlTypeRefCtx.schema_opsXmlTypeCtx = IntPtr.Zero;
				}
				try
				{
					if (this.m_opoXmlTypeRefCtx != null)
					{
						this.m_opoXmlTypeRefCtx.rootElement = null;
						this.m_opoXmlTypeRefCtx.schemaUrl = null;
					}
				}
				catch
				{
				}
				this.m_value = null;
				try
				{
					OpsXmlType.RelRef(this.m_opsConCtx, this.m_opsErrCtx, ref this.m_opsXmlTypeCtx, flag ? this.m_bFreeOciXmlType : 0);
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
				}
				try
				{
					OpsXmlType.FreeCtx(ref this.m_opsConCtx, ref this.m_opsErrCtx, ref this.m_pOpoXmlTypeValCtx, flag ? 1 : 0);
				}
				catch (Exception ex3)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex3);
					}
				}
			}
			catch
			{
			}
			finally
			{
				if (OracleConnection.IsAvailable && this.m_connection != null && this.m_connection.m_extProcEnv != null)
				{
					Monitor.Exit(this.m_connection.m_extProcEnv);
				}
			}
			this.m_connection = null;
			this.m_opsErrCtx = IntPtr.Zero;
			this.m_opsXmlTypeCtx = IntPtr.Zero;
			this.m_pOpoXmlTypeValCtx = null;
			this.m_doneDispose = true;
			try
			{
				GC.SuppressFinalize(this);
			}
			catch
			{
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleXmlType::Dispose()\n"
				});
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00037598 File Offset: 0x00036598
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x000375A4 File Offset: 0x000365A4
		public OracleConnection Connection
		{
			get
			{
				if (this.m_doneDispose)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (!this.m_bNotNull)
				{
					return null;
				}
				if (this.m_connection.m_internalUse)
				{
					throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.CLR_NOTSUPPORTED_INTERNAL_CONN, new string[0]));
				}
				return this.m_connection;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00037600 File Offset: 0x00036600
		public unsafe bool IsEmpty
		{
			get
			{
				if (this.m_doneDispose)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (1 == this.m_pOpoXmlTypeValCtx->isEmpty)
				{
					return true;
				}
				if (this.m_pOpoXmlTypeValCtx->isEmpty == 0)
				{
					return false;
				}
				string value = this.Value;
				if (value == null)
				{
					this.HandleError(-1, this.m_connection, this.m_opsErrCtx, this);
				}
				if (value.Length == 0)
				{
					this.m_pOpoXmlTypeValCtx->isEmpty = 1;
					return true;
				}
				this.m_pOpoXmlTypeValCtx->isEmpty = 0;
				return false;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00037694 File Offset: 0x00036694
		public unsafe bool IsSchemaBased
		{
			get
			{
				int num = 0;
				if (this.m_doneDispose)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (1 == this.m_pOpoXmlTypeValCtx->isSchemaBased)
				{
					return true;
				}
				if (this.m_pOpoXmlTypeValCtx->isSchemaBased == 0)
				{
					return false;
				}
				if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
				if (this.m_connection.m_conSignature != this.m_conSignature)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
				}
				try
				{
					num = OpsXmlType.IsSchemaBased(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsXmlTypeCtx, ref this.m_pOpoXmlTypeValCtx);
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
						this.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
				}
				return 1 == this.m_pOpoXmlTypeValCtx->isSchemaBased;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x000377BC File Offset: 0x000367BC
		public unsafe bool IsFragment
		{
			get
			{
				if (this.m_doneDispose)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
				if (this.m_connection.m_conSignature != this.m_conSignature)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
				}
				try
				{
					OpsXmlType.IsFragment(this.m_opsXmlTypeCtx, ref this.m_pOpoXmlTypeValCtx);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				return this.m_pOpoXmlTypeValCtx->isFragment == 1;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00037890 File Offset: 0x00036890
		public string RootElement
		{
			get
			{
				if (this.m_doneDispose)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (!this.m_doneGetSchema)
				{
					this.GetSchemaFromOPS();
				}
				return this.m_opoXmlTypeRefCtx.rootElement;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x000378E0 File Offset: 0x000368E0
		public OracleXmlType Schema
		{
			get
			{
				if (this.m_doneDispose)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (!this.m_doneGetSchema)
				{
					this.GetSchemaFromOPS();
					if (this.m_opoXmlTypeRefCtx.schema_opsXmlTypeCtx == IntPtr.Zero)
					{
						return new OracleXmlType(this.m_connection);
					}
					return new OracleXmlType(this.m_connection, this.m_opoXmlTypeRefCtx.schema_opsXmlTypeCtx, true);
				}
				else
				{
					if (this.m_opoXmlTypeRefCtx.schema_opsXmlTypeCtx == IntPtr.Zero)
					{
						return new OracleXmlType(this.m_connection);
					}
					return new OracleXmlType(this.m_connection, this.m_opoXmlTypeRefCtx.schema_opsXmlTypeCtx, true);
				}
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00037998 File Offset: 0x00036998
		public string SchemaUrl
		{
			get
			{
				if (this.m_doneDispose)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (!this.m_doneGetSchema)
				{
					this.GetSchemaFromOPS();
				}
				return this.m_opoXmlTypeRefCtx.schemaUrl;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x000379E8 File Offset: 0x000369E8
		public string Value
		{
			get
			{
				if (this.m_doneDispose)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (this.m_value == null)
				{
					IntPtr zero = IntPtr.Zero;
					int num = 0;
					int num2 = 0;
					if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
					{
						throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
					}
					if (this.m_connection.m_conSignature != this.m_conSignature)
					{
						throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
					}
					try
					{
						num2 = OpsXmlStream.GetValueBuffer(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsXmlTypeCtx, ref zero, ref num);
						if (num2 == 0)
						{
							if (num > 0 && zero != IntPtr.Zero)
							{
								this.m_value = Marshal.PtrToStringUni(zero, num);
								num2 = OpsXmlStream.FreeValueBuffer(ref zero);
							}
							else
							{
								this.m_value = string.Empty;
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
						if (num2 != 0)
						{
							this.HandleError(num2, this.m_connection, this.m_opsErrCtx, this);
						}
					}
				}
				return this.m_value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00037B2C File Offset: 0x00036B2C
		internal IntPtr OpsXmlTypeCtx
		{
			get
			{
				if (this.m_doneDispose)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				return this.m_opsXmlTypeCtx;
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00037B50 File Offset: 0x00036B50
		public object Clone()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlType::Clone()\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT) OracleXmlType::Clone()\n"
					});
				}
				return OracleXmlType.Null;
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_conSignature != this.m_connection.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			OracleXmlType result = new OracleXmlType(this.m_connection, this.m_opsXmlTypeCtx, true);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleXmlType::Clone()\n"
				});
			}
			return result;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00037C4A File Offset: 0x00036C4A
		internal void KeepOciXmlType()
		{
			this.m_bFreeOciXmlType = 0;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00037C54 File Offset: 0x00036C54
		internal int GetOCIXMLType(out IntPtr ociXMLType)
		{
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			int result = 0;
			ociXMLType = IntPtr.Zero;
			try
			{
				result = OpsXmlType.GetOCIXMLType(this.OpsXmlTypeCtx, ref ociXMLType);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00037CAC File Offset: 0x00036CAC
		public OracleXmlType Extract(string xpathExpr, string nsMap)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlType::Extract(xpath, nsmap)\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT) OracleXmlType::Extract(xpath, nsmap)\n"
					});
				}
				return null;
			}
			int num = 0;
			IntPtr zero = IntPtr.Zero;
			if (xpathExpr == null || xpathExpr.Length == 0)
			{
				throw new ArgumentNullException("xpathExpr", null);
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			try
			{
				num = OpsXmlType.Extract(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsXmlTypeCtx, xpathExpr, nsMap, ref zero);
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
					this.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (IntPtr.Zero != zero)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT) OracleXmlType::Extract(xpath, nsmap)\n"
					});
				}
				return new OracleXmlType(this.m_connection, zero, false);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleXmlType::Extract(xpath, nsmap)\n"
				});
			}
			return new OracleXmlType(this.m_connection);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00037E60 File Offset: 0x00036E60
		public OracleXmlType Extract(string xpathExpr, XmlNamespaceManager nsMgr)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlType::Extract(xpath, nsmgr)\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT) OracleXmlType::Extract(xpath, nsmgr)\n"
					});
				}
				return null;
			}
			string nsMap = this.NsMgrToString(nsMgr);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleXmlType::Extract(xpath, nsmgr)\n"
				});
			}
			return this.Extract(xpathExpr, nsMap);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00037EF8 File Offset: 0x00036EF8
		public OracleXmlStream GetStream()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlType::GetStream()\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_conSignature != this.m_connection.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleXmlType::GetStream()\n"
				});
			}
			return new OracleXmlStream(this.m_connection, this.m_opsXmlTypeCtx);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00037FD4 File Offset: 0x00036FD4
		public XmlDocument GetXmlDocument()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlType::GetXmlDocument()\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			string value = this.Value;
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			xmlDocument.LoadXml(value);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleXmlType::GetXmlDocument()\n"
				});
			}
			return xmlDocument;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00038060 File Offset: 0x00037060
		public XmlReader GetXmlReader()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlType::GetXmlReader()\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			string value = this.Value;
			TextReader input = new StringReader(value);
			XmlReader result = new XmlTextReader(input);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleXmlType::GetXmlReader()\n"
				});
			}
			return result;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x000380EC File Offset: 0x000370EC
		public bool IsExists(string xpathExpr, string nsMap)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlType::IsExists(xpath, nsmap)\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT) OracleXmlType::IsExists(xpath, nsmap)\n"
					});
				}
				return false;
			}
			if (xpathExpr == null || xpathExpr.Length == 0)
			{
				throw new ArgumentNullException("xpathExpr", null);
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			int num = 0;
			int num2 = 0;
			try
			{
				num = OpsXmlType.Exists(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsXmlTypeCtx, xpathExpr, nsMap, ref num2);
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
					this.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleXmlType::IsExists(xpath, nsmap)\n"
				});
			}
			return num2 == 1;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0003825C File Offset: 0x0003725C
		public bool IsExists(string xpathExpr, XmlNamespaceManager nsMgr)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlType::IsExists(xpath, nsmgr)\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT) OracleXmlType::IsExists(xpath, nsmgr)\n"
					});
				}
				return false;
			}
			string nsMap = this.NsMgrToString(nsMgr);
			bool result = this.IsExists(xpathExpr, nsMap);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleXmlType::IsExists(xpath, nsmgr)\n"
				});
			}
			return result;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000382F8 File Offset: 0x000372F8
		public OracleXmlType Transform(OracleXmlType xsldoc, string paramMap)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlType::Transform(xmltypexsldoc, paramMap)\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			if (xsldoc == null)
			{
				throw new ArgumentNullException("xsldoc", null);
			}
			int num = 0;
			IntPtr pBuffer = IntPtr.Zero;
			IntPtr zero = IntPtr.Zero;
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			pBuffer = xsldoc.OpsXmlTypeCtx;
			try
			{
				num = OpsXmlType.Transform(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsXmlTypeCtx, pBuffer, 4, paramMap, ref zero);
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
					this.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleXmlType::Transform(xmltypexsldoc, paramMap)\n"
				});
			}
			if (IntPtr.Zero == zero)
			{
				return new OracleXmlType(this.m_connection);
			}
			return new OracleXmlType(this.m_connection, zero, false);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00038478 File Offset: 0x00037478
		public OracleXmlType Transform(string xsldoc, string paramMap)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlType::Transform(stringxsldoc, paramMap)\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			if (xsldoc == null || xsldoc.Length == 0)
			{
				throw new ArgumentNullException("xsldoc", null);
			}
			int num = 0;
			IntPtr zero = IntPtr.Zero;
			GCHandle gchandle = GCHandle.Alloc(xsldoc, GCHandleType.Pinned);
			IntPtr pBuffer = gchandle.AddrOfPinnedObject();
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			try
			{
				num = OpsXmlType.Transform(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsXmlTypeCtx, pBuffer, 1, paramMap, ref zero);
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
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				if (num != 0)
				{
					this.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleXmlType::Transform(stringxsldoc, paramMap)\n"
				});
			}
			if (IntPtr.Zero == zero)
			{
				return new OracleXmlType(this.m_connection);
			}
			return new OracleXmlType(this.m_connection, zero, false);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00038614 File Offset: 0x00037614
		public unsafe void Update(string xpathExpr, string nsMap, OracleXmlType val)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlType::Update(xpathexpr, nsmap, xmltypeval)\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			if (xpathExpr == null || xpathExpr.Length == 0 || val == null)
			{
				throw new ArgumentNullException("xpathExpr or val", null);
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			if (nsMap != null && nsMap.Length == 0)
			{
				nsMap = null;
			}
			int num = 0;
			IntPtr zero = IntPtr.Zero;
			try
			{
				num = OpsXmlType.Copy(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsXmlTypeCtx, ref zero);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				this.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
			}
			try
			{
				OpsXmlType.RelRef(this.m_opsConCtx, this.m_opsErrCtx, ref this.m_opsXmlTypeCtx, this.m_bFreeOciXmlType);
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
			}
			this.m_opsXmlTypeCtx = zero;
			this.m_value = null;
			try
			{
				num = OpsXmlType.UpdateFromXmlType(this.m_opsConCtx, this.m_opsErrCtx, zero, xpathExpr, nsMap, val.OpsXmlTypeCtx);
			}
			catch (Exception ex3)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex3);
				}
				throw;
			}
			if (num != 0)
			{
				this.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
			}
			this.m_pOpoXmlTypeValCtx->isFragment = 2;
			this.m_pOpoXmlTypeValCtx->isSchemaBased = 2;
			this.m_pOpoXmlTypeValCtx->isEmpty = 2;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleXmlType::Update(xpathexpr, nsmap, xmltypeval)\n"
				});
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00038828 File Offset: 0x00037828
		public void Update(string xpathExpr, XmlNamespaceManager nsMgr, OracleXmlType val)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlType::Update(xpathexpr, nsmgr, xmltypeval)\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			string nsMap = this.NsMgrToString(nsMgr);
			this.Update(xpathExpr, nsMap, val);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleXmlType::Update(xpathexpr, nsmgr, xmltypeval)\n"
				});
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x000388A8 File Offset: 0x000378A8
		public unsafe void Update(string xpathExpr, string nsMap, string val)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlType::Update(xpathexpr, nsmap, stringval)\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			if (xpathExpr == null || xpathExpr.Length == 0)
			{
				throw new ArgumentNullException("xpathExpr", null);
			}
			if (nsMap != null && nsMap.Length == 0)
			{
				nsMap = null;
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			int num = 0;
			IntPtr zero = IntPtr.Zero;
			try
			{
				num = OpsXmlType.Copy(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsXmlTypeCtx, ref zero);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				this.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
			}
			try
			{
				OpsXmlType.RelRef(this.m_opsConCtx, this.m_opsErrCtx, ref this.m_opsXmlTypeCtx, this.m_bFreeOciXmlType);
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
			}
			this.m_opsXmlTypeCtx = zero;
			this.m_value = null;
			try
			{
				num = OpsXmlType.UpdateFromString(this.m_opsConCtx, this.m_opsErrCtx, zero, xpathExpr, nsMap, val);
			}
			catch (Exception ex3)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex3);
				}
				throw;
			}
			if (num != 0)
			{
				this.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
			}
			this.m_pOpoXmlTypeValCtx->isFragment = 2;
			this.m_pOpoXmlTypeValCtx->isSchemaBased = 2;
			this.m_pOpoXmlTypeValCtx->isEmpty = 2;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleXmlType::Update(xpathexpr, nsmap, stringval)\n"
				});
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00038AB4 File Offset: 0x00037AB4
		public void Update(string xpathExpr, XmlNamespaceManager nsMgr, string val)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlType::Update(xpathexpr, nsmgr, stringval)\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			string nsMap = this.NsMgrToString(nsMgr);
			this.Update(xpathExpr, nsMap, val);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleXmlType::Update(xpathexpr, nsmgr, stringval)\n"
				});
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00038B34 File Offset: 0x00037B34
		public bool Validate(string schemaUrl)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlType::Validate(schemaurl)\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			if (schemaUrl == null || schemaUrl.Length == 0)
			{
				throw new ArgumentNullException("schemaUrl", null);
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			int num = 0;
			int num2 = 0;
			try
			{
				num = OpsXmlType.Validate(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsXmlTypeCtx, schemaUrl, ref num2);
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
					this.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleXmlType::Validate(schemaurl)\n"
				});
			}
			return 1 == num2;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00038C88 File Offset: 0x00037C88
		~OracleXmlType()
		{
			this.Dispose();
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00038CB4 File Offset: 0x00037CB4
		private unsafe void Initialize(OracleConnection con, string xmlData, OracleClob clob, int flag)
		{
			int num = 0;
			IntPtr pBuffer = IntPtr.Zero;
			GCHandle gchandle = default(GCHandle);
			this.m_opsConCtx = con.m_opoConCtx.opsConCtx;
			if (this.m_opsConCtx == IntPtr.Zero)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (1 == flag)
			{
				gchandle = GCHandle.Alloc(xmlData, GCHandleType.Pinned);
				pBuffer = gchandle.AddrOfPinnedObject();
			}
			else
			{
				if (con.m_conSignature != clob.m_connection.m_conSignature)
				{
					GC.SuppressFinalize(this);
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
				}
				pBuffer = clob.LobCtx;
			}
			this.m_opoXmlTypeRefCtx = new OpoXmlTypeRefCtx();
			try
			{
				num = OpsXmlType.AllocXmlTypeCtx(this.m_opsConCtx, ref this.m_opsXmlTypeCtx, ref this.m_opsErrCtx, ref this.m_pOpoXmlTypeValCtx, pBuffer, flag);
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
			finally
			{
				if (1 == flag && gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				if (num != 0)
				{
					GC.SuppressFinalize(this);
					throw new OracleTypeException(num, new object[]
					{
						"xml"
					});
				}
			}
			this.m_pOpoXmlTypeValCtx->isFragment = 2;
			this.m_pOpoXmlTypeValCtx->isSchemaBased = 2;
			this.m_pOpoXmlTypeValCtx->isEmpty = 2;
			this.m_connection = con;
			this.m_conSignature = con.m_conSignature;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00038E30 File Offset: 0x00037E30
		private void GetSchemaFromOPS()
		{
			if (this.m_doneGetSchema)
			{
				return;
			}
			this.m_doneGetSchema = true;
			int num = 0;
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			try
			{
				num = OpsXmlType.GetSchema(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsXmlTypeCtx, ref this.m_opoXmlTypeRefCtx);
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
					this.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (this.m_opoXmlTypeRefCtx.rootElement == null)
			{
				this.m_opoXmlTypeRefCtx.rootElement = string.Empty;
			}
			if (this.m_opoXmlTypeRefCtx.schemaUrl == null)
			{
				this.m_opoXmlTypeRefCtx.schemaUrl = string.Empty;
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00038F4C File Offset: 0x00037F4C
		private void HandleError(int errCode, OracleConnection conn, IntPtr opsErrCtx, object src)
		{
			if (IntPtr.Zero == opsErrCtx)
			{
				string dataSrc = (conn != null) ? conn.DataSource : string.Empty;
				throw new OracleException(errCode, dataSrc, string.Empty, string.Empty);
			}
			OracleException.HandleError(errCode, conn, opsErrCtx, src);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00038F94 File Offset: 0x00037F94
		private string NsMgrToString(XmlNamespaceManager nsMgr)
		{
			string text = null;
			if (nsMgr != null)
			{
				text = string.Empty;
				foreach (object obj in nsMgr)
				{
					string text2 = (string)obj;
					string text3 = nsMgr.LookupNamespace(text2);
					if ((text2 != null && text2.Length != 0) || (text3 != null && text3.Length != 0))
					{
						StringBuilder stringBuilder = new StringBuilder(text, 1024);
						if (text != null && text.Length != 0)
						{
							stringBuilder.Append(' ');
						}
						stringBuilder.Append("xmlns:");
						stringBuilder.Append(text2);
						stringBuilder.Append('=');
						stringBuilder.Append(text3);
						text = stringBuilder.ToString();
					}
				}
			}
			return text;
		}

		// Token: 0x04000335 RID: 821
		private const int FALSE = 0;

		// Token: 0x04000336 RID: 822
		private const int TRUE = 1;

		// Token: 0x04000337 RID: 823
		private const int UNKNOWN = 2;

		// Token: 0x04000338 RID: 824
		internal int m_bFreeOciXmlType;

		// Token: 0x04000339 RID: 825
		private IntPtr m_opsErrCtx;

		// Token: 0x0400033A RID: 826
		private IntPtr m_opsConCtx;

		// Token: 0x0400033B RID: 827
		private IntPtr m_opsXmlTypeCtx;

		// Token: 0x0400033C RID: 828
		internal OracleConnection m_connection;

		// Token: 0x0400033D RID: 829
		internal int m_conSignature;

		// Token: 0x0400033E RID: 830
		private OpoXmlTypeRefCtx m_opoXmlTypeRefCtx;

		// Token: 0x0400033F RID: 831
		private unsafe OpoXmlTypeValCtx* m_pOpoXmlTypeValCtx;

		// Token: 0x04000340 RID: 832
		private bool m_doneGetSchema;

		// Token: 0x04000341 RID: 833
		private bool m_doneDispose;

		// Token: 0x04000342 RID: 834
		private string m_value;

		// Token: 0x04000343 RID: 835
		private bool m_bNotNull;

		// Token: 0x04000344 RID: 836
		public static readonly OracleXmlType Null = new OracleXmlType();
	}
}
