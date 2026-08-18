using System;
using Oracle.ManagedDataAccess.Client;

namespace OracleInternal.BinXml
{
	// Token: 0x02000002 RID: 2
	internal class ObxmlDecodeContext
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		protected ObxmlDecodeContext()
		{
			this.CsxReadMaxChunkSize = (long)ObxmlDecodeStream.DefaultChunkSize;
			this.ChunkingPolicy = CsxDecodeChunkingPolicy.None;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000206C File Offset: 0x0000026C
		internal static ObxmlDecodeContext CreateDefaultContext(OracleConnection dbConnection)
		{
			ObxmlDecodeContext obxmlDecodeContext = new ObxmlDecodeContext();
			obxmlDecodeContext.SetDecodeContext(dbConnection, null, ObxmlTokenManagerContext.CreateDefaultTokenManagerContext(dbConnection), CsxDecodeChunkingPolicy.None, 0L);
			return obxmlDecodeContext;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002094 File Offset: 0x00000294
		internal static ObxmlDecodeContext UpdateContext(ObxmlDecodeContext decodeContext, ObxmlMetaDataRepository metaDataRepository, ObxmlTokenMap tokenMap, CsxDecodeChunkingPolicy chunkingPolicy = CsxDecodeChunkingPolicy.None, long csxReadMaxChunkSize = -1L)
		{
			decodeContext.SetDecodeContext(null, metaDataRepository, tokenMap, chunkingPolicy, csxReadMaxChunkSize);
			return decodeContext;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020A4 File Offset: 0x000002A4
		protected string GetTimeStamp(bool isStart)
		{
			return string.Empty;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020AC File Offset: 0x000002AC
		protected string DecodeTimeStamp
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020B4 File Offset: 0x000002B4
		protected string CacheSizeString
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020BC File Offset: 0x000002BC
		protected string PerformanceCounterString
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020C4 File Offset: 0x000002C4
		internal string ContextId
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020CC File Offset: 0x000002CC
		internal bool IsBusy
		{
			get
			{
				return !this.m_IsDone;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020D8 File Offset: 0x000002D8
		internal bool IsValid
		{
			get
			{
				return this.DbConnection != null && this.TokenMap != null && this.m_MetaDataRepository != null;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020F8 File Offset: 0x000002F8
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002100 File Offset: 0x00000300
		internal OracleConnection DbConnection
		{
			get
			{
				return this.m_DbConnection;
			}
			set
			{
				if (this.m_IsDone)
				{
					this.m_DbConnection = value;
				}
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002114 File Offset: 0x00000314
		// (set) Token: 0x0600000E RID: 14 RVA: 0x0000211C File Offset: 0x0000031C
		internal ObxmlTokenManagerContext TokenMapContext
		{
			get
			{
				return this.m_TokenMapContext;
			}
			set
			{
				this.m_TokenMapContext = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002128 File Offset: 0x00000328
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002164 File Offset: 0x00000364
		internal ObxmlTokenMap TokenMap
		{
			get
			{
				if (this.m_TokenMap == null && this.m_TokenMapContext != null && this.DbConnection != null)
				{
					this.m_TokenMap = ObxmlTokenManager.GetOracleBinXmlTokenManager(this.DbConnection).Open(this.m_TokenMapContext);
				}
				return this.m_TokenMap;
			}
			set
			{
				this.m_TokenMap = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002170 File Offset: 0x00000370
		internal ObxmlMetaDataRepository MetaDataRepository
		{
			get
			{
				return this.m_MetaDataRepository;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002178 File Offset: 0x00000378
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002180 File Offset: 0x00000380
		internal CsxDecodeChunkingPolicy ChunkingPolicy { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000218C File Offset: 0x0000038C
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002194 File Offset: 0x00000394
		internal long CsxReadMaxChunkSize { get; set; }

		// Token: 0x06000016 RID: 22 RVA: 0x000021A0 File Offset: 0x000003A0
		internal void SetDecodeContext(ObxmlDecodeContext decodeContext)
		{
			if (decodeContext != null && this != decodeContext)
			{
				if (decodeContext.TokenMap != null)
				{
					this.TokenMap = decodeContext.TokenMap;
				}
				if (decodeContext.DbConnection != null)
				{
					this.m_DbConnection = decodeContext.DbConnection;
				}
				if (decodeContext.MetaDataRepository != null)
				{
					this.m_MetaDataRepository = decodeContext.MetaDataRepository;
				}
				if (CsxDecodeChunkingPolicy.None != decodeContext.ChunkingPolicy)
				{
					this.ChunkingPolicy = decodeContext.ChunkingPolicy;
				}
				if (decodeContext.CsxReadMaxChunkSize > 0L)
				{
					this.CsxReadMaxChunkSize = decodeContext.CsxReadMaxChunkSize;
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000221C File Offset: 0x0000041C
		protected ObxmlDecodeContext SetDecodeContext(OracleConnection dbConnection, ObxmlMetaDataRepository metaDataRepository, ObxmlTokenMap tokenMap, CsxDecodeChunkingPolicy chunkingPolicy, long csxReadMaxChunkSize)
		{
			if (dbConnection != null)
			{
				this.m_DbConnection = dbConnection;
			}
			if (metaDataRepository != null)
			{
				this.m_MetaDataRepository = metaDataRepository;
			}
			if (tokenMap != null)
			{
				this.TokenMap = tokenMap;
			}
			if (CsxDecodeChunkingPolicy.None != chunkingPolicy)
			{
				this.ChunkingPolicy = chunkingPolicy;
			}
			if (csxReadMaxChunkSize > 0L)
			{
				this.CsxReadMaxChunkSize = csxReadMaxChunkSize;
			}
			return this;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002258 File Offset: 0x00000458
		protected ObxmlDecodeContext SetDecodeContext(OracleConnection dbConnection, ObxmlMetaDataRepository metaDataRepository, ObxmlTokenManagerContext tokenMapContext, CsxDecodeChunkingPolicy chunkingPolicy, long csxReadMaxChunkSize)
		{
			if (dbConnection != null)
			{
				this.m_DbConnection = dbConnection;
			}
			if (metaDataRepository != null)
			{
				this.m_MetaDataRepository = metaDataRepository;
			}
			if (tokenMapContext != null)
			{
				this.TokenMapContext = tokenMapContext;
			}
			if (CsxDecodeChunkingPolicy.None != chunkingPolicy)
			{
				this.ChunkingPolicy = chunkingPolicy;
			}
			if (csxReadMaxChunkSize > 0L)
			{
				this.CsxReadMaxChunkSize = csxReadMaxChunkSize;
			}
			return this;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002294 File Offset: 0x00000494
		internal virtual bool ResetDecodeState()
		{
			this.CsxReadMaxChunkSize = (long)ObxmlDecodeStream.DefaultChunkSize;
			this.ChunkingPolicy = CsxDecodeChunkingPolicy.None;
			return true;
		}

		// Token: 0x04000001 RID: 1
		protected bool m_IsDone;

		// Token: 0x04000002 RID: 2
		protected ObxmlMetaDataRepository m_MetaDataRepository;

		// Token: 0x04000003 RID: 3
		protected ObxmlTokenManagerContext m_TokenMapContext;

		// Token: 0x04000004 RID: 4
		protected ObxmlTokenMap m_TokenMap;

		// Token: 0x04000005 RID: 5
		protected OracleConnection m_DbConnection;
	}
}
