using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors
{
	// Token: 0x0200031F RID: 799
	public abstract class OracleMbRuleProcessorTableDictionary<T>
	{
		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001D30 RID: 7472 RVA: 0x0011ECB0 File Offset: 0x0011CEB0
		public Dictionary<string, T> RuleProcessorTableDictionary
		{
			get
			{
				return this.m_vRuleProcessorTableDictionary;
			}
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x0011ECB8 File Offset: 0x0011CEB8
		public OracleMbRuleProcessorTableDictionary()
		{
			this.m_vRuleProcessorTableDictionary = new Dictionary<string, T>();
		}

		// Token: 0x04001D7E RID: 7550
		protected readonly Dictionary<string, T> m_vRuleProcessorTableDictionary;
	}
}
