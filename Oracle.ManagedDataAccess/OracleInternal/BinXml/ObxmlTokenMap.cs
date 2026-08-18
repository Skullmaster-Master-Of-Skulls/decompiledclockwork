using System;
using System.Collections;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.BinXml
{
	// Token: 0x0200002E RID: 46
	internal class ObxmlTokenMap
	{
		// Token: 0x06000263 RID: 611 RVA: 0x0000D0D0 File Offset: 0x0000B2D0
		static ObxmlTokenMap()
		{
			ObxmlTokenMap.m_NsTokenList.Add(ObxmlTokenMap.CreateObxmlNamespaceToken(1UL, "http://www.w3.org/XML/1998/namespace"));
			ObxmlTokenMap.m_NsTokenList.Add(ObxmlTokenMap.CreateObxmlNamespaceToken(2UL, "http://www.w3.org/XML/2000/xmlns/"));
			ObxmlTokenMap.m_NsTokenList.Add(ObxmlTokenMap.CreateObxmlNamespaceToken(3UL, "http://www.w3.org/2001/XMLSchema-instance"));
			ObxmlTokenMap.m_NsTokenList.Add(ObxmlTokenMap.CreateObxmlNamespaceToken(4UL, "http://www.w3.org/2001/XMLSchema"));
			ObxmlTokenMap.m_NsTokenList.Add(ObxmlTokenMap.CreateObxmlNamespaceToken(5UL, "http://xmlns.oracle.com/2004/csx"));
			ObxmlTokenMap.m_NsTokenList.Add(ObxmlTokenMap.CreateObxmlNamespaceToken(6UL, "http://xmlns.oracle.com/xdb"));
			ObxmlTokenMap.m_NsTokenList.Add(ObxmlTokenMap.CreateObxmlNamespaceToken(7UL, ""));
			ObxmlTokenMap.m_NsTokenList.Add(ObxmlTokenMap.CreateObxmlNamespaceToken(8UL, "http://www.w3.org/2001/XInclude"));
			ObxmlTokenMap.m_AttrTokenList.Add(ObxmlTokenMap.CreateObxmlQNameToken(16UL, 1UL, "space", true));
			ObxmlTokenMap.m_AttrTokenList.Add(ObxmlTokenMap.CreateObxmlQNameToken(17UL, 1UL, "lang", true));
			ObxmlTokenMap.m_AttrTokenList.Add(ObxmlTokenMap.CreateObxmlQNameToken(18UL, 3UL, "type", true));
			ObxmlTokenMap.m_AttrTokenList.Add(ObxmlTokenMap.CreateObxmlQNameToken(19UL, 3UL, "nil", true));
			ObxmlTokenMap.m_AttrTokenList.Add(ObxmlTokenMap.CreateObxmlQNameToken(20UL, 3UL, "schemaLocation", true));
			ObxmlTokenMap.m_AttrTokenList.Add(ObxmlTokenMap.CreateObxmlQNameToken(21UL, 3UL, "noNamespaceSchemaLocation", true));
			ObxmlTokenMap.m_AttrTokenList.Add(ObxmlTokenMap.CreateObxmlQNameToken(22UL, 2UL, "xmlns", true));
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000D268 File Offset: 0x0000B468
		internal ObxmlTokenMap()
		{
			this.Init();
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000D28C File Offset: 0x0000B48C
		internal ObxmlTokenMap(ObxmlTokenManagerContext context, TokenTypes tokenType)
		{
			this.m_DefaultTokenType = tokenType;
			this.PartitionId = context.PartitionId;
			this.m_tmContext = context;
			this.Init();
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000D2C8 File Offset: 0x0000B4C8
		internal string TokenMapSizeString
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000D2D0 File Offset: 0x0000B4D0
		internal void Clear()
		{
			if (this.m_ElementAttributeTokens != null)
			{
				this.m_ElementAttributeTokens.Purge();
			}
			if (this.m_NamespaceTokens != null)
			{
				this.m_NamespaceTokens.Purge();
			}
			if (this.m_NamespaceUriTokens != null)
			{
				this.m_NamespaceUriTokens.Clear();
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000D30C File Offset: 0x0000B50C
		// (set) Token: 0x06000269 RID: 617 RVA: 0x0000D314 File Offset: 0x0000B514
		internal string PartitionId { get; set; }

		// Token: 0x0600026A RID: 618 RVA: 0x0000D320 File Offset: 0x0000B520
		internal ObxmlTokenManagerContext GetTokenMgrContext()
		{
			return this.m_tmContext;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000D328 File Offset: 0x0000B528
		internal ulong NSIDNULL
		{
			get
			{
				return ObxmlTokenMap.m_Nsidnull;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000D330 File Offset: 0x0000B530
		internal ulong NSIDXI
		{
			get
			{
				return ObxmlTokenMap.m_Nsidxi;
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000D338 File Offset: 0x0000B538
		internal bool Init()
		{
			if (this.m_bInit)
			{
				return true;
			}
			if (this.m_ElementAttributeTokens == null)
			{
				this.m_ElementAttributeTokens = new CacheWithLRUList<ulong, ObxmlToken>(ObxmlTokenManager.MaxTokenPoolEntries);
			}
			if (this.m_NamespaceTokens == null)
			{
				this.m_NamespaceTokens = new CacheWithLRUList<ulong, ObxmlToken>(ObxmlTokenManager.MaxTokenPoolEntries);
			}
			this.m_ElementAttributeTokens.Put(16UL, ObxmlTokenMap.m_AttrTokenList[0], false);
			this.m_ElementAttributeTokens.Put(17UL, ObxmlTokenMap.m_AttrTokenList[1], false);
			this.m_ElementAttributeTokens.Put(18UL, ObxmlTokenMap.m_AttrTokenList[2], false);
			this.m_ElementAttributeTokens.Put(19UL, ObxmlTokenMap.m_AttrTokenList[3], false);
			this.m_ElementAttributeTokens.Put(20UL, ObxmlTokenMap.m_AttrTokenList[4], false);
			this.m_ElementAttributeTokens.Put(21UL, ObxmlTokenMap.m_AttrTokenList[5], false);
			this.m_ElementAttributeTokens.Put(22UL, ObxmlTokenMap.m_AttrTokenList[6], false);
			this.m_NamespaceTokens.Put(1UL, ObxmlTokenMap.m_NsTokenList[0], false);
			this.m_NamespaceTokens.Put(2UL, ObxmlTokenMap.m_NsTokenList[1], false);
			this.m_NamespaceTokens.Put(3UL, ObxmlTokenMap.m_NsTokenList[2], false);
			this.m_NamespaceTokens.Put(4UL, ObxmlTokenMap.m_NsTokenList[3], false);
			this.m_NamespaceTokens.Put(5UL, ObxmlTokenMap.m_NsTokenList[4], false);
			this.m_NamespaceTokens.Put(6UL, ObxmlTokenMap.m_NsTokenList[5], false);
			this.m_NamespaceTokens.Put(7UL, ObxmlTokenMap.m_NsTokenList[6], false);
			this.m_NamespaceTokens.Put(8UL, ObxmlTokenMap.m_NsTokenList[7], false);
			this.m_NamespaceUriTokens[ObxmlTokenMap.m_NsTokenList[0].TokenName] = ObxmlTokenMap.m_NsTokenList[0];
			this.m_NamespaceUriTokens[ObxmlTokenMap.m_NsTokenList[1].TokenName] = ObxmlTokenMap.m_NsTokenList[1];
			this.m_NamespaceUriTokens[ObxmlTokenMap.m_NsTokenList[2].TokenName] = ObxmlTokenMap.m_NsTokenList[2];
			this.m_NamespaceUriTokens[ObxmlTokenMap.m_NsTokenList[3].TokenName] = ObxmlTokenMap.m_NsTokenList[3];
			this.m_NamespaceUriTokens[ObxmlTokenMap.m_NsTokenList[4].TokenName] = ObxmlTokenMap.m_NsTokenList[4];
			this.m_NamespaceUriTokens[ObxmlTokenMap.m_NsTokenList[5].TokenName] = ObxmlTokenMap.m_NsTokenList[5];
			this.m_NamespaceUriTokens[ObxmlTokenMap.m_NsTokenList[6].TokenName] = ObxmlTokenMap.m_NsTokenList[6];
			this.m_NamespaceUriTokens[ObxmlTokenMap.m_NsTokenList[7].TokenName] = ObxmlTokenMap.m_NsTokenList[7];
			ulong namespaceTokenId = this.GetNamespaceTokenId(null);
			ObxmlToken obxmlToken = this.m_NamespaceTokens.Get(namespaceTokenId);
			ObxmlTokenMap.m_Nsidnull = obxmlToken.TokenId;
			namespaceTokenId = this.GetNamespaceTokenId("http://www.w3.org/2001/XInclude");
			obxmlToken = this.m_NamespaceTokens.Get(namespaceTokenId);
			ObxmlTokenMap.m_Nsidxi = obxmlToken.TokenId;
			return this.m_bInit = true;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000D68C File Offset: 0x0000B88C
		internal string GetPartitionId()
		{
			return this.PartitionId;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000D694 File Offset: 0x0000B894
		// (set) Token: 0x06000270 RID: 624 RVA: 0x0000D69C File Offset: 0x0000B89C
		internal bool IsActive { get; set; }

		// Token: 0x06000271 RID: 625 RVA: 0x0000D6A8 File Offset: 0x0000B8A8
		internal void SetToken(ObxmlToken token)
		{
			if (token == null)
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.TokenInvalid, null, ObxmlOpcode.OpcodeIds.None));
			}
			switch (token.TokenType)
			{
			case TokenTypes.NamespaceToken:
				if (!this.m_NamespaceTokens.ContainsKey(token.TokenId))
				{
					this.RegisterNamespace(token);
					return;
				}
				return;
			case TokenTypes.AttributeToken:
				if (!this.m_ElementAttributeTokens.ContainsKey(token.TokenId))
				{
					this.m_ElementAttributeTokens.Put(token.TokenId, token, false);
					return;
				}
				return;
			case TokenTypes.ElementToken:
				if (!this.m_ElementAttributeTokens.ContainsKey(token.TokenId))
				{
					this.m_ElementAttributeTokens.Put(token.TokenId, token, false);
					return;
				}
				return;
			}
			throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.TokenInvalid, null, ObxmlOpcode.OpcodeIds.None));
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000D780 File Offset: 0x0000B980
		internal ObxmlToken PopulateNamespaceUri(ObxmlDecodeContext decodeContext, ObxmlToken token)
		{
			if (token != null && token.NamespaceId == ObxmlTokenMap.m_Nsidnull)
			{
				token.NamespaceId = 0UL;
			}
			if (token.NamespaceId != 0UL && token.Uri == null)
			{
				ObxmlToken namespaceToken = this.GetNamespaceToken(decodeContext, token.NamespaceId, TokenTypes.NamespaceToken);
				token.Uri = ((namespaceToken != null) ? namespaceToken.TokenName : null);
			}
			return token;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000D7DC File Offset: 0x0000B9DC
		internal string GetTokenNameForDefaultTokenType(ObxmlDecodeContext decodeContext, ulong tokenId)
		{
			try
			{
				ObxmlToken tokenForDefaultTokenType = this.GetTokenForDefaultTokenType(decodeContext, tokenId);
				if (tokenForDefaultTokenType != null)
				{
					return tokenForDefaultTokenType.TokenName;
				}
			}
			catch (Exception)
			{
			}
			return null;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000D818 File Offset: 0x0000BA18
		internal ObxmlToken ConvertNSIDToNullNameSpaceId(ObxmlToken token, bool tryBothIds = false)
		{
			if (token != null)
			{
				if (tryBothIds && token.TokenType == TokenTypes.NamespaceToken && token.TokenId == ObxmlTokenMap.m_Nsidnull)
				{
					token.TokenId = 0UL;
				}
				if (token.NamespaceId == ObxmlTokenMap.m_Nsidnull)
				{
					token.NamespaceId = 0UL;
				}
			}
			return token;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000D854 File Offset: 0x0000BA54
		internal ObxmlToken GetTokenForDefaultTokenType(ObxmlDecodeContext decodeContext, ulong tokenId)
		{
			return this.GetToken(decodeContext, tokenId, this.m_DefaultTokenType, true);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000D868 File Offset: 0x0000BA68
		internal ObxmlToken GetToken(ObxmlDecodeContext decodeContext, ulong tokenId, TokenTypes tokenType, bool tryRepository = true)
		{
			ObxmlToken obxmlToken = null;
			try
			{
				switch (tokenType)
				{
				case TokenTypes.NamespaceToken:
					obxmlToken = this.m_NamespaceTokens.Get(tokenId);
					break;
				case TokenTypes.AttributeToken:
					obxmlToken = this.m_ElementAttributeTokens.Get(tokenId);
					break;
				case TokenTypes.ElementToken:
					obxmlToken = this.m_ElementAttributeTokens.Get(tokenId);
					break;
				}
			}
			catch
			{
			}
			if (obxmlToken == null && tryRepository)
			{
				decodeContext.MetaDataRepository.GetTokenSet(decodeContext, tokenId, tokenType, null, null, true);
				return this.GetToken(decodeContext, tokenId, tokenType, false);
			}
			this.ConvertNSIDToNullNameSpaceId(obxmlToken, false);
			return obxmlToken;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000D900 File Offset: 0x0000BB00
		internal static bool TryGetValue(CacheWithLRUList<ulong, ObxmlToken> tokenCache, ulong key, out ObxmlToken value)
		{
			if (tokenCache.ContainsKey(key))
			{
				value = tokenCache.Get(key);
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000D91C File Offset: 0x0000BB1C
		internal ObxmlToken GetToken(ObxmlDecodeContext decodeContext, ulong tokenId, bool tryRepository)
		{
			try
			{
				ObxmlToken obxmlToken = null;
				if (ObxmlTokenMap.TryGetValue(this.m_ElementAttributeTokens, tokenId, out obxmlToken) && obxmlToken != null)
				{
					this.ConvertNSIDToNullNameSpaceId(obxmlToken, false);
					return obxmlToken;
				}
				if (ObxmlTokenMap.TryGetValue(this.m_NamespaceTokens, tokenId, out obxmlToken) && obxmlToken != null)
				{
					this.ConvertNSIDToNullNameSpaceId(obxmlToken, false);
					return obxmlToken;
				}
				if (tryRepository)
				{
					decodeContext.MetaDataRepository.GetTokenSet(decodeContext, tokenId, TokenTypes.ElementToken, null, null, true);
					obxmlToken = this.GetToken(decodeContext, tokenId, false);
					if (obxmlToken == null)
					{
						decodeContext.MetaDataRepository.GetTokenSet(decodeContext, tokenId, TokenTypes.NamespaceToken, null, null, true);
						return this.GetToken(decodeContext, tokenId, false);
					}
					if (obxmlToken != null)
					{
						this.ConvertNSIDToNullNameSpaceId(obxmlToken, false);
						return obxmlToken;
					}
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000D9D4 File Offset: 0x0000BBD4
		internal string GetTokenName(ObxmlDecodeContext decodeContext, ulong tokenId, TokenTypes tokenType)
		{
			try
			{
				ObxmlToken token = this.GetToken(decodeContext, tokenId, tokenType, true);
				if (token != null)
				{
					return token.TokenName;
				}
				return null;
			}
			catch (Exception)
			{
			}
			return null;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000DA14 File Offset: 0x0000BC14
		internal string GetTokenName(ObxmlDecodeContext decodeContext, ulong tokenId)
		{
			try
			{
				ObxmlToken token = this.GetToken(decodeContext, tokenId, true);
				if (token != null)
				{
					return token.TokenName;
				}
				return null;
			}
			catch (Exception)
			{
			}
			return null;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000DA54 File Offset: 0x0000BC54
		internal void RegisterNamespace(ObxmlToken token)
		{
			if (!this.m_NamespaceTokens.ContainsKey(token.TokenId))
			{
				this.m_NamespaceTokens.Put(token.TokenId, token, false);
			}
			if (!this.m_NamespaceUriTokens.ContainsKey(token.TokenName))
			{
				this.m_NamespaceUriTokens.Add(token.TokenName, token);
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000DAB0 File Offset: 0x0000BCB0
		internal string GetNamespaceUri(ObxmlDecodeContext decodeContext, ulong nsid, out ObxmlToken token)
		{
			token = null;
			string result = string.Empty;
			if (nsid == 0UL)
			{
				return result;
			}
			token = this.GetNamespaceToken(decodeContext, nsid, TokenTypes.NamespaceToken);
			if (token != null)
			{
				result = token.TokenName;
			}
			return result;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000DAE8 File Offset: 0x0000BCE8
		internal ObxmlToken GetNamespaceToken(ObxmlDecodeContext decodeContext, ulong nsid, TokenTypes tokenType = TokenTypes.NamespaceToken)
		{
			return this.GetToken(decodeContext, nsid, tokenType, true);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000DAF4 File Offset: 0x0000BCF4
		internal static ObxmlToken CreateObxmlToken(ulong tokenId, string tokenName, TokenTypes tokenType = TokenTypes.None)
		{
			if (tokenType == TokenTypes.None)
			{
				tokenType = TokenTypes.ElementToken;
			}
			ObxmlToken obxmlToken = new ObxmlToken(tokenId, tokenName, tokenType);
			if (obxmlToken != null && obxmlToken.NamespaceId == ObxmlTokenMap.m_Nsidnull)
			{
				obxmlToken.NamespaceId = 0UL;
			}
			return obxmlToken;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000DB2C File Offset: 0x0000BD2C
		internal ObxmlToken CreateToken(ulong tokenId, string tokenName, TokenTypes tokenType = TokenTypes.None)
		{
			ObxmlToken obxmlToken = ObxmlTokenMap.CreateObxmlToken(tokenId, tokenName, tokenType);
			this.SetToken(obxmlToken);
			return obxmlToken;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000DB4C File Offset: 0x0000BD4C
		internal static ObxmlToken CreateObxmlNamespaceToken(ulong tokenId, string namespaceURI)
		{
			return ObxmlTokenMap.CreateObxmlToken(tokenId, namespaceURI, TokenTypes.NamespaceToken);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000DB58 File Offset: 0x0000BD58
		internal ObxmlToken CreateNamespaceToken(ulong tokenId, string namespaceURI)
		{
			ObxmlToken obxmlToken = ObxmlTokenMap.CreateObxmlNamespaceToken(tokenId, namespaceURI);
			this.SetToken(obxmlToken);
			return obxmlToken;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000DB78 File Offset: 0x0000BD78
		internal static ObxmlToken CreateObxmlQNameToken(ulong tokenId, ulong namespaceId, string localName, bool isAttribute)
		{
			ObxmlToken obxmlToken = ObxmlTokenMap.CreateObxmlToken(tokenId, localName, isAttribute ? TokenTypes.AttributeToken : TokenTypes.ElementToken);
			if (namespaceId == ObxmlTokenMap.m_Nsidnull)
			{
				obxmlToken.NamespaceId = 0UL;
			}
			else
			{
				obxmlToken.NamespaceId = namespaceId;
			}
			return obxmlToken;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000DBB0 File Offset: 0x0000BDB0
		internal ObxmlToken CreateQNameToken(ulong tokenId, ulong namespaceId, string localName, bool isAttribute)
		{
			ObxmlToken obxmlToken = ObxmlTokenMap.CreateObxmlQNameToken(tokenId, namespaceId, localName, isAttribute);
			this.SetToken(obxmlToken);
			return obxmlToken;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000DBD0 File Offset: 0x0000BDD0
		internal ObxmlToken PutNamespaceNonSchema(string namespaceURI)
		{
			if (namespaceURI == null)
			{
				return ObxmlTokenMap.m_NsTokenList[6];
			}
			ObxmlToken obxmlToken = (ObxmlToken)this.m_NamespaceUriTokens[namespaceURI];
			if (obxmlToken != null)
			{
				return obxmlToken;
			}
			throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.NotImplemented, null, ObxmlOpcode.OpcodeIds.None));
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000DC24 File Offset: 0x0000BE24
		internal ulong GetNamespaceTokenId(string namespaceURI)
		{
			ObxmlToken obxmlToken = null;
			if (!string.IsNullOrEmpty(namespaceURI))
			{
				obxmlToken = (ObxmlToken)this.m_NamespaceUriTokens[namespaceURI];
			}
			if (obxmlToken == null)
			{
				obxmlToken = this.PutNamespaceNonSchema(namespaceURI);
			}
			return obxmlToken.TokenId;
		}

		// Token: 0x040002F2 RID: 754
		private static ulong m_Nsidnull = 0UL;

		// Token: 0x040002F3 RID: 755
		private static ulong m_Nsidxi = 0UL;

		// Token: 0x040002F4 RID: 756
		internal static List<ObxmlToken> m_NsTokenList = new List<ObxmlToken>(8);

		// Token: 0x040002F5 RID: 757
		internal static List<ObxmlToken> m_AttrTokenList = new List<ObxmlToken>(7);

		// Token: 0x040002F6 RID: 758
		private TokenTypes m_DefaultTokenType = TokenTypes.ElementToken;

		// Token: 0x040002F7 RID: 759
		private bool m_bInit;

		// Token: 0x040002F8 RID: 760
		private ObxmlTokenManagerContext m_tmContext;

		// Token: 0x040002F9 RID: 761
		private CacheWithLRUList<ulong, ObxmlToken> m_ElementAttributeTokens;

		// Token: 0x040002FA RID: 762
		private CacheWithLRUList<ulong, ObxmlToken> m_NamespaceTokens;

		// Token: 0x040002FB RID: 763
		private Hashtable m_NamespaceUriTokens = new Hashtable();
	}
}
