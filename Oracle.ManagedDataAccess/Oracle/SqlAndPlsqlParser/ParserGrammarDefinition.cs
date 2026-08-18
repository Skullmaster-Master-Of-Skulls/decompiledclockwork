using System;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x0200026E RID: 622
	internal class ParserGrammarDefinition
	{
		// Token: 0x060018B7 RID: 6327 RVA: 0x001043D8 File Offset: 0x001025D8
		public ParserGrammarDefinition(Set<RuleTuple> rules)
		{
			if (rules == null)
			{
				throw new ArgumentNullException("The set of rules can't be null!");
			}
			this.InitializeSymbols(rules);
			this.InitializeRulesInformation(rules);
			this.InitializeSpecialWords();
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x00104404 File Offset: 0x00102604
		protected void InitializeSymbols(Set<RuleTuple> rules)
		{
			Set<string> set = new Set<string>();
			int num;
			foreach (RuleTuple ruleTuple in rules)
			{
				string[] vRhs = ruleTuple.m_vRhs;
				num = vRhs.Length;
				string vHead = ruleTuple.m_vHead;
				if (vHead == null || num == 0 || vRhs[0] == null || (num > 1 && vRhs[1] == null))
				{
					throw new ParserException(ParserExceptionType.Grammar, ParserExceptionError.RuleTupleNullSymbols);
				}
				set.Add(vHead);
				foreach (string item in vRhs)
				{
					set.Add(item);
				}
			}
			num = set.Count;
			this.m_vAllSymbols = set.ToArray();
			this.m_vSymbolsFlags = new int[num];
			for (int j = 0; j < num; j++)
			{
				this.m_vSymbolsFlags[j] = 0;
			}
			Array.Sort<string>(this.m_vAllSymbols, StringComparer.Ordinal);
			this.m_vSymbolIndexes = new IntSortedMap<string>(StringComparer.Ordinal);
			this.m_vQuotedSymbolIndexes = new IntSortedMap<string>(StringComparer.InvariantCultureIgnoreCase);
			for (int k = 0; k < num; k++)
			{
				string text = this.m_vAllSymbols[k];
				this.m_vSymbolIndexes[text] = k;
				if (text[0] == '\'' && text[text.Length - 1] == '\'')
				{
					this.m_vQuotedSymbolIndexes[text.Substring(1, text.Length - 2)] = k;
				}
			}
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x00104588 File Offset: 0x00102788
		protected void InitializeRulesInformation(Set<RuleTuple> rules)
		{
			int num = 0;
			this.m_vRules = new ParserRuleTuple[rules.Count];
			this.m_vSortedRulesIndexes = new int[rules.Count];
			foreach (RuleTuple ruleTuple in rules)
			{
				string[] vRhs = ruleTuple.m_vRhs;
				int num2 = this.m_vSymbolIndexes[ruleTuple.m_vHead];
				this.m_vSymbolsFlags[num2] |= 2;
				int[] array = new int[vRhs.Length];
				for (int i = 0; i < vRhs.Length; i++)
				{
					int num3 = this.m_vSymbolIndexes[vRhs[i]];
					array[i] = num3;
					if (num3 != num2)
					{
						this.m_vSymbolsFlags[num3] |= 4;
					}
				}
				this.m_vSortedRulesIndexes[num] = num;
				this.m_vRules[num++] = new ParserRuleTuple(num2, array, this);
			}
			Array.Sort<ParserRuleTuple>(this.m_vRules, ParserRuleTupleBaseComparer.s_vInstance);
			this.m_vBaseSortedRules = (ParserRuleTuple[])this.m_vRules.Clone();
			for (int i = 0; i < this.m_vAllSymbols.Length; i++)
			{
				if ((this.m_vSymbolsFlags[i] & 6) == 4)
				{
					this.m_vSymbolsFlags[i] |= 1;
				}
			}
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x001046FC File Offset: 0x001028FC
		protected virtual void InitializeSpecialWords()
		{
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x00104700 File Offset: 0x00102900
		public bool IsTerminal(int idx)
		{
			return (this.m_vSymbolsFlags[idx] & 1) != 0;
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x00104714 File Offset: 0x00102914
		internal int GetRuleIndex(string head, string[] rhs)
		{
			int h = this.m_vSymbolIndexes[head];
			int[] array = new int[rhs.Length];
			for (int i = 0; i < rhs.Length; i++)
			{
				int num = this.m_vSymbolIndexes[rhs[i]];
				array[i] = num;
			}
			return Array.BinarySearch<ParserRuleTuple>(this.m_vBaseSortedRules, new ParserRuleTuple(h, array, this), ParserRuleTupleBaseComparer.s_vInstance);
		}

		// Token: 0x04001B29 RID: 6953
		public string[] m_vAllSymbols;

		// Token: 0x04001B2A RID: 6954
		public IntSortedMap<string> m_vSymbolIndexes;

		// Token: 0x04001B2B RID: 6955
		public IntSortedMap<string> m_vQuotedSymbolIndexes;

		// Token: 0x04001B2C RID: 6956
		public ParserRuleTuple[] m_vRules;

		// Token: 0x04001B2D RID: 6957
		public ParserRuleTuple[] m_vBaseSortedRules;

		// Token: 0x04001B2E RID: 6958
		public int[] m_vSortedRulesIndexes;

		// Token: 0x04001B2F RID: 6959
		public int[] m_vSymbolsFlags;
	}
}
