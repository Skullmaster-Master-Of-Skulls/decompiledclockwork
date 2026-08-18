using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors
{
	// Token: 0x020002DC RID: 732
	internal class OracleMbParserContextBase<T, U> where T : Parser
	{
		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001A9B RID: 6811 RVA: 0x0010B1F0 File Offset: 0x001093F0
		public T Parser
		{
			get
			{
				return this.m_vParser;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001A9C RID: 6812 RVA: 0x0010B1F8 File Offset: 0x001093F8
		// (set) Token: 0x06001A9D RID: 6813 RVA: 0x0010B200 File Offset: 0x00109400
		public string Script
		{
			get
			{
				return this.m_vScriptText;
			}
			set
			{
				this.m_vScriptText = value;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001A9E RID: 6814 RVA: 0x0010B20C File Offset: 0x0010940C
		// (set) Token: 0x06001A9F RID: 6815 RVA: 0x0010B214 File Offset: 0x00109414
		public List<LexerToken> Tokens
		{
			get
			{
				return this.m_vTokens;
			}
			set
			{
				this.m_vTokens = value;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x0010B220 File Offset: 0x00109420
		// (set) Token: 0x06001AA1 RID: 6817 RVA: 0x0010B228 File Offset: 0x00109428
		public ParseNode RootParseNode
		{
			get
			{
				return this.m_vRootParseNode;
			}
			set
			{
				this.m_vRootParseNode = value;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x0010B234 File Offset: 0x00109434
		// (set) Token: 0x06001AA3 RID: 6819 RVA: 0x0010B23C File Offset: 0x0010943C
		public ParseNode CurrentParseNode
		{
			get
			{
				return this.m_vCurrentParseNode;
			}
			set
			{
				this.m_vCurrentParseNode = value;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x0010B248 File Offset: 0x00109448
		// (set) Token: 0x06001AA5 RID: 6821 RVA: 0x0010B250 File Offset: 0x00109450
		public int CurrentRuleIndex
		{
			get
			{
				return this.m_vCurrentRuleIndex;
			}
			set
			{
				this.m_vCurrentRuleIndex = value;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001AA6 RID: 6822 RVA: 0x0010B25C File Offset: 0x0010945C
		// (set) Token: 0x06001AA7 RID: 6823 RVA: 0x0010B264 File Offset: 0x00109464
		public U RuleProcessorTable
		{
			get
			{
				return this.m_vRuleProcessorTable;
			}
			set
			{
				this.m_vRuleProcessorTable = value;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x0010B270 File Offset: 0x00109470
		// (set) Token: 0x06001AA9 RID: 6825 RVA: 0x0010B278 File Offset: 0x00109478
		public List<string> ActiveViewColumnAliases
		{
			get
			{
				return this.m_vActiveViewColumnAliases;
			}
			set
			{
				this.m_vActiveViewColumnAliases = value;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001AAA RID: 6826 RVA: 0x0010B284 File Offset: 0x00109484
		public OracleMbParserPropertiesBag PropertiesBag
		{
			get
			{
				return this.m_vPropertiesBag;
			}
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x0010B28C File Offset: 0x0010948C
		public OracleMbParserContextBase(T parser, OracleMbRuleProcessorTableDictionary<U> ruleProcessorTableDictionary)
		{
			this.m_vParser = parser;
			this.m_vRuleProcessorTableDictionary = ruleProcessorTableDictionary.RuleProcessorTableDictionary;
			this.m_vRuleProcessorTable = this.GetRuleProcessorTable("ODPCommands");
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x0010B2C4 File Offset: 0x001094C4
		public string GetStatementBetweenGivenSrcAndTgtTokenIdx(int srcTokenIdx, int tgtTokenIdx)
		{
			if (srcTokenIdx < 0 || tgtTokenIdx < 0 || srcTokenIdx > tgtTokenIdx)
			{
				return string.Empty;
			}
			int vBegin = this.Tokens[srcTokenIdx].m_vBegin;
			int length = this.Tokens[tgtTokenIdx].m_vEnd - vBegin;
			return this.Script.Substring(vBegin, length);
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x0010B318 File Offset: 0x00109518
		public U GetRuleProcessorTable(string name)
		{
			U result;
			if (!this.m_vRuleProcessorTableDictionary.TryGetValue(name, out result))
			{
				return this.m_vRuleProcessorTableDictionary["empty"];
			}
			return result;
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x0010B348 File Offset: 0x00109548
		public virtual void Clear()
		{
			this.m_vPropertiesBag.Clear();
			this.m_vCurrentRuleIndex = 0;
			this.m_vCurrentParseNode = null;
			this.m_vRootParseNode = null;
			this.m_vTokens = null;
			this.m_vScriptText = null;
		}

		// Token: 0x04001CBA RID: 7354
		protected readonly Dictionary<string, U> m_vRuleProcessorTableDictionary;

		// Token: 0x04001CBB RID: 7355
		protected readonly T m_vParser;

		// Token: 0x04001CBC RID: 7356
		protected string m_vScriptText;

		// Token: 0x04001CBD RID: 7357
		protected List<LexerToken> m_vTokens;

		// Token: 0x04001CBE RID: 7358
		protected ParseNode m_vRootParseNode;

		// Token: 0x04001CBF RID: 7359
		protected ParseNode m_vCurrentParseNode;

		// Token: 0x04001CC0 RID: 7360
		protected int m_vCurrentRuleIndex;

		// Token: 0x04001CC1 RID: 7361
		protected U m_vRuleProcessorTable;

		// Token: 0x04001CC2 RID: 7362
		protected List<string> m_vActiveViewColumnAliases;

		// Token: 0x04001CC3 RID: 7363
		protected OracleMbParserPropertiesBag m_vPropertiesBag = new OracleMbParserPropertiesBag();
	}
}
