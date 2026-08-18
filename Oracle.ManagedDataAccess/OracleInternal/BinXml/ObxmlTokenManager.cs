using System;
using System.Collections;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.ConnectionPool;

namespace OracleInternal.BinXml
{
	// Token: 0x0200002A RID: 42
	internal class ObxmlTokenManager : IDisposable
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000CB6C File Offset: 0x0000AD6C
		internal static bool ClientDecodeEnabled
		{
			get
			{
				return ConfigBaseClass.m_XMLTypeClientSideDecoding;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000CB74 File Offset: 0x0000AD74
		internal static int MaxTokenPoolEntries
		{
			get
			{
				if (ObxmlTokenManager.m_MaxTokenPoolEntries != 0)
				{
					return ObxmlTokenManager.m_MaxTokenPoolEntries;
				}
				return ObxmlTokenManager.m_MaxTokenPoolEntries = ConfigBaseClass.m_XMLTypeMaxCacheEntries;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000CB90 File Offset: 0x0000AD90
		internal static int MaxTokenPoolSize
		{
			get
			{
				if (ObxmlTokenManager.m_MaxTokenPoolSize != 0)
				{
					return ObxmlTokenManager.m_MaxTokenPoolSize;
				}
				return ObxmlTokenManager.m_MaxTokenPoolSize = ConfigBaseClass.m_XMLTypeMaxCacheSize;
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000CBAC File Offset: 0x0000ADAC
		internal ObxmlTokenManager(OraclePoolManager parent)
		{
			if (parent == null)
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.InvalidArguments, null, ObxmlOpcode.OpcodeIds.None));
			}
			this.m_parent = parent;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000CC0C File Offset: 0x0000AE0C
		internal static ObxmlTokenManager GetOracleBinXmlTokenManager(ObxmlDecodeContext decodeContext)
		{
			if (decodeContext == null)
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.InvalidArguments, null, ObxmlOpcode.OpcodeIds.None));
			}
			return ObxmlTokenManager.GetOracleBinXmlTokenManager(decodeContext.DbConnection);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000CC3C File Offset: 0x0000AE3C
		internal static ObxmlTokenManager GetOracleBinXmlTokenManager(OracleConnection dbConnection)
		{
			if (dbConnection == null)
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.InvalidArguments, null, ObxmlOpcode.OpcodeIds.None));
			}
			return dbConnection.m_oracleConnectionImpl.m_pm.m_xmlTokenManager;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000CC70 File Offset: 0x0000AE70
		internal ObxmlMetaDataRepository GetDefaultMetaDataRepository(bool bDisposeMetaDataRepository, ObxmlDecodeContext decodeContext = null)
		{
			this.m_bOwnsMetaDataRepository = bDisposeMetaDataRepository;
			return this.m_DefaultMetaDataRepository;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000CC80 File Offset: 0x0000AE80
		internal ObxmlTokenMap Open(ObxmlTokenManagerContext tmContext)
		{
			ObxmlTokenMap obxmlTokenMap = null;
			if (tmContext == null || !tmContext.IsValid)
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.TokenCacheCorrupt, null, ObxmlOpcode.OpcodeIds.None));
			}
			lock (this.m_tokenMapLock)
			{
				if ((obxmlTokenMap = this.GetMap(tmContext.PartitionId)) != null)
				{
					return obxmlTokenMap;
				}
				obxmlTokenMap = new ObxmlTokenMap(tmContext, TokenTypes.None);
				this.SetMap(ref obxmlTokenMap, obxmlTokenMap.PartitionId);
			}
			return obxmlTokenMap;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000CD14 File Offset: 0x0000AF14
		internal int Count
		{
			get
			{
				return this.m_TokenMapHash.Count;
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000CD24 File Offset: 0x0000AF24
		internal ObxmlTokenMap Open(string partitionId)
		{
			return null;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000CD28 File Offset: 0x0000AF28
		internal void Close(ObxmlTokenMap tokenMap)
		{
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000CD2C File Offset: 0x0000AF2C
		internal void Close(string partitionId)
		{
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000CD30 File Offset: 0x0000AF30
		internal void Clear(ObxmlTokenMap tokenMap)
		{
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000CD34 File Offset: 0x0000AF34
		internal void Clear(string partitionId)
		{
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000CD38 File Offset: 0x0000AF38
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000CD48 File Offset: 0x0000AF48
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_TokenMapHash != null)
				{
					this.m_TokenMapHash.Clear();
					this.m_TokenMapHash = null;
				}
				if (this.m_bOwnsMetaDataRepository && this.m_DefaultMetaDataRepository != null)
				{
					this.m_DefaultMetaDataRepository.Dispose();
					this.m_DefaultMetaDataRepository = null;
				}
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000CD94 File Offset: 0x0000AF94
		internal void PurgeTokenMaps(List<string> lruPartitionsList, int thresHold)
		{
			lock (this.m_tokenMapLock)
			{
				if (lruPartitionsList == null && thresHold == -1)
				{
					foreach (object obj in this.m_TokenMapHash)
					{
						ObxmlTokenMap obxmlTokenMap = (ObxmlTokenMap)obj;
						obxmlTokenMap.Clear();
					}
				}
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000CE20 File Offset: 0x0000B020
		private void SetMap(ref ObxmlTokenMap tokenMap, string partitionId)
		{
			if (!this.m_TokenMapHash.Contains(partitionId))
			{
				this.m_TokenMapHash.Add(partitionId, tokenMap);
				return;
			}
			tokenMap = (ObxmlTokenMap)this.m_TokenMapHash[partitionId];
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000CE54 File Offset: 0x0000B054
		private ObxmlTokenMap GetMap(string partitionId)
		{
			try
			{
				return (ObxmlTokenMap)this.m_TokenMapHash[partitionId];
			}
			catch (Exception)
			{
			}
			return null;
		}

		// Token: 0x040002DD RID: 733
		private ObxmlMetaDataRepository m_DefaultMetaDataRepository = new ObxmlMetaDataRepository();

		// Token: 0x040002DE RID: 734
		private bool m_bOwnsMetaDataRepository;

		// Token: 0x040002DF RID: 735
		private Hashtable m_TokenMapHash = new Hashtable();

		// Token: 0x040002E0 RID: 736
		private static int m_MaxTokenPoolEntries;

		// Token: 0x040002E1 RID: 737
		private static int m_MaxTokenPoolSize;

		// Token: 0x040002E2 RID: 738
		private OraclePoolManager m_parent;

		// Token: 0x040002E3 RID: 739
		private object m_tokenMapLock = new object();
	}
}
