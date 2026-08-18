using System;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace OracleInternal.BinXml
{
	// Token: 0x02000029 RID: 41
	internal class ObxmlProcessor
	{
		// Token: 0x06000233 RID: 563 RVA: 0x0000C9E4 File Offset: 0x0000ABE4
		internal ObxmlProcessor()
		{
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
		internal ObxmlDecodeStream GetDecodeStream(OracleConnection conn, OracleBlob csxBlob)
		{
			if (conn == null && csxBlob == null)
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.InvalidArguments, null, ObxmlOpcode.OpcodeIds.None));
			}
			if (conn == null)
			{
				conn = csxBlob.Connection;
			}
			if (this.m_DecodeContext != null)
			{
				lock (this.m_DecodeContextLock)
				{
					if (this.m_DecodeContext != null)
					{
						ObxmlDecodeStream obxmlDecodeStream = (ObxmlDecodeStream)this.m_DecodeContext;
						obxmlDecodeStream.ResetRequestObject(this.m_DecodeContext, csxBlob).DbConnection = conn;
						this.m_DecodeContext = null;
						return obxmlDecodeStream;
					}
				}
			}
			ObxmlTokenManager oracleBinXmlTokenManager = ObxmlTokenManager.GetOracleBinXmlTokenManager(conn);
			ObxmlDecodeContext obxmlDecodeContext = ObxmlDecodeContext.CreateDefaultContext(conn);
			ObxmlMetaDataRepository defaultMetaDataRepository = oracleBinXmlTokenManager.GetDefaultMetaDataRepository(true, obxmlDecodeContext);
			ObxmlTokenMap tokenMap = oracleBinXmlTokenManager.Open(obxmlDecodeContext.TokenMapContext);
			ObxmlDecodeContext.UpdateContext(obxmlDecodeContext, defaultMetaDataRepository, tokenMap, CsxDecodeChunkingPolicy.None, -1L);
			return new ObxmlDecodeStream(obxmlDecodeContext, csxBlob);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000CAE0 File Offset: 0x0000ACE0
		internal void CloseDecodeStream(ObxmlDecodeContext decodeContext)
		{
			if (decodeContext == null)
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.InvalidArguments, null, ObxmlOpcode.OpcodeIds.None));
			}
			if (this.m_DecodeContext == null)
			{
				lock (this.m_DecodeContextLock)
				{
					if (this.m_DecodeContext == null)
					{
						this.m_DecodeContext = decodeContext;
						this.m_DecodeContext.ResetDecodeState();
						return;
					}
				}
			}
			((ObxmlDecodeStream)decodeContext).Dispose();
		}

		// Token: 0x040002DB RID: 731
		private ObxmlDecodeContext m_DecodeContext;

		// Token: 0x040002DC RID: 732
		private object m_DecodeContextLock = new object();
	}
}
