using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x02000275 RID: 629
	internal class EarleyParserGrammarDefinition : ParserGrammarDefinition
	{
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x00106408 File Offset: 0x00104608
		public virtual OracleMbEarleyRulesPriorityDescriptor[] RulesPriorityDescriptors
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x0010640C File Offset: 0x0010460C
		public EarleyParserGrammarDefinition(Set<RuleTuple> rules) : base(rules)
		{
			this.PrioritizeRules();
			this.BuildHasPathToFirstSymbolTable();
			this.BuildPredictionsTable();
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x00106428 File Offset: 0x00104628
		protected void PrioritizeRules()
		{
			Map<int, List<int>> map = new Map<int, List<int>>();
			for (int i = 0; i < this.m_vRules.Length; i++)
			{
				ParserRuleTuple parserRuleTuple = this.m_vRules[i];
				int vHead = parserRuleTuple.m_vHead;
				List<int> list;
				if (!map.TryGetValue(vHead, out list))
				{
					list = new List<int>();
					map[vHead] = list;
				}
				list.Add(i);
			}
			this.PrioritizeRules(map);
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x0010648C File Offset: 0x0010468C
		protected void PrioritizeRules(Map<int, List<int>> headToRules)
		{
			OracleMbEarleyRulesPriorityDescriptor[] rulesPriorityDescriptors = this.RulesPriorityDescriptors;
			if (rulesPriorityDescriptors == null)
			{
				return;
			}
			foreach (OracleMbEarleyRulesPriorityDescriptor oracleMbEarleyRulesPriorityDescriptor in rulesPriorityDescriptors)
			{
				List<int> list;
				if (headToRules.TryGetValue(this.m_vSymbolIndexes[oracleMbEarleyRulesPriorityDescriptor.m_vHeadSymbol], out list) && oracleMbEarleyRulesPriorityDescriptor.m_vFirstRHSSymbols != null && oracleMbEarleyRulesPriorityDescriptor.m_vFirstRHSSymbols.Length > 0)
				{
					int num = 0;
					int num2 = oracleMbEarleyRulesPriorityDescriptor.m_vFirstRHSSymbols.Length;
					int[] array2 = new int[num2];
					foreach (string key in oracleMbEarleyRulesPriorityDescriptor.m_vFirstRHSSymbols)
					{
						array2[num++] = this.m_vSymbolIndexes[key];
					}
					int[] rules = list.ToArray();
					this.SortRulesOnPriority(rules, array2);
				}
			}
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x00106560 File Offset: 0x00104760
		protected void SortRulesOnPriority(int[] rules, int[] firstRHSSymbolsPriority)
		{
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			int num = firstRHSSymbolsPriority.Length;
			foreach (int num2 in rules)
			{
				ParserRuleTuple parserRuleTuple = this.m_vRules[num2];
				for (int j = 0; j < num; j++)
				{
					if (parserRuleTuple.m_vRhs[0] == firstRHSSymbolsPriority[j])
					{
						list.Add(j);
						list2.Add(num2);
						break;
					}
				}
			}
			int count = list2.Count;
			ParserRuleTuple[] array = new ParserRuleTuple[count];
			int[] array2 = list2.ToArray();
			int[] keys = list.ToArray();
			Array.Sort<int, int>(keys, array2);
			for (int k = 0; k < count; k++)
			{
				array[k] = this.m_vRules[array2[k]];
			}
			int num3 = 0;
			foreach (int num4 in list2)
			{
				this.m_vRules[num4] = array[num3];
				this.m_vSortedRulesIndexes[array2[num3]] = num4;
				num3++;
			}
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x0010667C File Offset: 0x0010487C
		protected void BuildHasPathToFirstSymbolTable()
		{
			int num = this.m_vAllSymbols.Length;
			this.m_vTopLevelSymbols = new List<int>();
			this.m_vHasPathToFirstSymbolTable = new HashSet<int>[num];
			this.m_vHeadToRulesTable = new List<int>[num];
			for (int i = 0; i < this.m_vRules.Length; i++)
			{
				ParserRuleTuple parserRuleTuple = this.m_vRules[i];
				int vHead = parserRuleTuple.m_vHead;
				List<int> list = this.m_vHeadToRulesTable[vHead];
				if (list == null)
				{
					list = new List<int>(1);
					this.m_vHeadToRulesTable[vHead] = list;
				}
				list.Add(i);
			}
			for (int j = 0; j < num; j++)
			{
				if ((this.m_vSymbolsFlags[j] & 6) == 2)
				{
					this.m_vTopLevelSymbols.Add(j);
				}
			}
			int num2 = this.m_vAllSymbols.Length;
			bool[] visited = new bool[num2];
			bool[] visiting = new bool[num2];
			bool[] array = new bool[num2];
			for (int k = 0; k < num2; k++)
			{
				this.BuildHasPathToFirstSymbolTable(k, visited, visiting, array);
			}
			long num3 = 0L;
			long num4 = 0L;
			for (;;)
			{
				foreach (HashSet<int> hashSet in this.m_vHasPathToFirstSymbolTable)
				{
					num4 += (long)hashSet.Count;
				}
				if (num4 == num3)
				{
					break;
				}
				num3 = num4;
				num4 = 0L;
				for (int m = 0; m < num2; m++)
				{
					if (!array[m])
					{
						this.CompleteBuildHasPathToFirstSymbolTable(m, array);
					}
				}
			}
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x001067DC File Offset: 0x001049DC
		protected void BuildHasPathToFirstSymbolTable(int symbol, bool[] visited, bool[] visiting, bool[] completed)
		{
			visiting[symbol] = true;
			HashSet<int> hashSet;
			if (base.IsTerminal(symbol))
			{
				hashSet = new HashSet<int>();
				hashSet.Add(symbol);
				this.m_vHasPathToFirstSymbolTable[symbol] = hashSet;
				visiting[symbol] = false;
				visited[symbol] = true;
				completed[symbol] = true;
				return;
			}
			HashSet<int> hashSet2 = new HashSet<int>();
			bool flag = false;
			hashSet = this.m_vHasPathToFirstSymbolTable[symbol];
			if (hashSet == null)
			{
				hashSet = new HashSet<int>();
				this.m_vHasPathToFirstSymbolTable[symbol] = hashSet;
			}
			foreach (int num in this.m_vHeadToRulesTable[symbol])
			{
				int num2 = this.m_vRules[num].m_vRhs[0];
				if (symbol != num2 && !hashSet2.Contains(num2))
				{
					HashSet<int> other;
					if (visited[num2])
					{
						other = this.m_vHasPathToFirstSymbolTable[num2];
					}
					else
					{
						if (visiting[num2])
						{
							flag = true;
							hashSet2.Add(num2);
							continue;
						}
						this.BuildHasPathToFirstSymbolTable(num2, visited, visiting, completed);
						other = this.m_vHasPathToFirstSymbolTable[num2];
					}
					if (!completed[num2])
					{
						flag = true;
					}
					hashSet.UnionWith(other);
					hashSet2.Add(num2);
				}
			}
			visiting[symbol] = false;
			visited[symbol] = true;
			if (!flag)
			{
				completed[symbol] = true;
			}
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x00106904 File Offset: 0x00104B04
		protected void CompleteBuildHasPathToFirstSymbolTable(int symbol, bool[] completed)
		{
			HashSet<int> hashSet = new HashSet<int>();
			HashSet<int> hashSet2 = this.m_vHasPathToFirstSymbolTable[symbol];
			if (hashSet2 == null)
			{
				hashSet2 = new HashSet<int>();
				this.m_vHasPathToFirstSymbolTable[symbol] = hashSet2;
			}
			foreach (int num in this.m_vHeadToRulesTable[symbol])
			{
				int num2 = this.m_vRules[num].m_vRhs[0];
				if (symbol != num2 && !completed[num2] && !hashSet.Contains(num2))
				{
					HashSet<int> other = this.m_vHasPathToFirstSymbolTable[num2];
					hashSet2.UnionWith(other);
					hashSet.Add(num2);
				}
			}
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x001069B0 File Offset: 0x00104BB0
		protected bool NonTerminalHasPathToTerminal(int nonTerminal, int terminal)
		{
			HashSet<int> hashSet = this.m_vHasPathToFirstSymbolTable[nonTerminal];
			return hashSet.Contains(terminal);
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x001069D0 File Offset: 0x00104BD0
		protected void BuildPredictionsTable()
		{
			this.m_vPredictionsTable = new Dictionary<long, FlexibleSizeLongArray>();
			int num = this.m_vAllSymbols.Length;
			bool[] array = new bool[num];
			bool[] visiting = new bool[num];
			bool[] array2 = new bool[num];
			for (int i = 0; i < num; i++)
			{
				if (base.IsTerminal(i))
				{
					array[i] = true;
					array2[i] = true;
				}
				else
				{
					this.BuildPredictionsTable(i, array, visiting, array2);
				}
			}
			long num2 = 0L;
			long num3 = 0L;
			for (;;)
			{
				foreach (KeyValuePair<long, FlexibleSizeLongArray> keyValuePair in this.m_vPredictionsTable)
				{
					num3 += (long)keyValuePair.Value.m_vContentSize;
				}
				if (num3 == num2)
				{
					break;
				}
				num2 = num3;
				num3 = 0L;
				for (int j = 0; j < num; j++)
				{
					if (!array2[j])
					{
						this.CompleteBuildPredictionsTable(j, array2);
					}
				}
			}
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x00106AC4 File Offset: 0x00104CC4
		protected void BuildPredictionsTable(int symbol, bool[] visited, bool[] visiting, bool[] completed)
		{
			HashSet<int> hashSet = this.m_vHasPathToFirstSymbolTable[symbol];
			long num = (long)symbol << 32;
			bool flag = false;
			HashSet<int> hashSet2 = new HashSet<int>();
			visiting[symbol] = true;
			foreach (int num2 in hashSet)
			{
				long key = num | (long)((ulong)num2);
				FlexibleSizeLongArray flexibleSizeLongArray;
				this.m_vPredictionsTable.TryGetValue(key, out flexibleSizeLongArray);
				hashSet2.Clear();
				foreach (int num3 in this.m_vHeadToRulesTable[symbol])
				{
					int[] vRhs = this.m_vRules[num3].m_vRhs;
					int num4 = vRhs[0];
					if (base.IsTerminal(num4))
					{
						if (num4 == num2)
						{
							flexibleSizeLongArray = FlexibleSizeLongArray.Insert(flexibleSizeLongArray, this.MakeMatrixCellElem(num3, 0));
						}
					}
					else if (symbol == num4)
					{
						flexibleSizeLongArray = FlexibleSizeLongArray.Insert(flexibleSizeLongArray, this.MakeMatrixCellElem(num3, 0));
					}
					else if (this.NonTerminalHasPathToTerminal(num4, num2))
					{
						if (!hashSet2.Contains(num4))
						{
							hashSet2.Add(num4);
							if (visiting[num4])
							{
								flag = true;
								flexibleSizeLongArray = FlexibleSizeLongArray.Insert(flexibleSizeLongArray, this.MakeMatrixCellElem(num3, 0));
								continue;
							}
							if (!visited[num4])
							{
								this.BuildPredictionsTable(num4, visited, visiting, completed);
							}
							if (!completed[num4])
							{
								flag = true;
							}
							long key2 = (long)num4 << 32 | (long)((ulong)num2);
							FlexibleSizeLongArray y;
							if (this.m_vPredictionsTable.TryGetValue(key2, out y))
							{
								flexibleSizeLongArray = FlexibleSizeLongArray.Merge(flexibleSizeLongArray, y);
							}
						}
						flexibleSizeLongArray = FlexibleSizeLongArray.Insert(flexibleSizeLongArray, this.MakeMatrixCellElem(num3, 0));
					}
				}
				this.m_vPredictionsTable[key] = flexibleSizeLongArray;
			}
			visiting[symbol] = false;
			visited[symbol] = true;
			if (!flag)
			{
				completed[symbol] = true;
			}
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x00106CBC File Offset: 0x00104EBC
		protected void CompleteBuildPredictionsTable(int symbol, bool[] completed)
		{
			HashSet<int> hashSet = this.m_vHasPathToFirstSymbolTable[symbol];
			long num = (long)symbol << 32;
			HashSet<int> hashSet2 = new HashSet<int>();
			foreach (int num2 in hashSet)
			{
				long key = num | (long)((ulong)num2);
				FlexibleSizeLongArray flexibleSizeLongArray;
				this.m_vPredictionsTable.TryGetValue(key, out flexibleSizeLongArray);
				hashSet2.Clear();
				foreach (int num3 in this.m_vHeadToRulesTable[symbol])
				{
					int[] vRhs = this.m_vRules[num3].m_vRhs;
					int num4 = vRhs[0];
					if (symbol != num4 && !completed[num4] && !hashSet2.Contains(num4))
					{
						hashSet2.Add(num4);
						if (this.NonTerminalHasPathToTerminal(num4, num2))
						{
							long key2 = (long)num4 << 32 | (long)((ulong)num2);
							FlexibleSizeLongArray y;
							if (this.m_vPredictionsTable.TryGetValue(key2, out y))
							{
								flexibleSizeLongArray = FlexibleSizeLongArray.Merge(flexibleSizeLongArray, y);
							}
						}
					}
				}
				this.m_vPredictionsTable[key] = flexibleSizeLongArray;
			}
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x00106DF8 File Offset: 0x00104FF8
		public long MakeMatrixCellElem(int ruleIdx, int position)
		{
			long num = -1L;
			int[] vRhs = this.m_vRules[ruleIdx].m_vRhs;
			if (position < vRhs.Length)
			{
				num = (long)vRhs[position];
			}
			return (long)ruleIdx << 12 | (long)((ulong)position) | num << 32;
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x00106E30 File Offset: 0x00105030
		public virtual void GetTokensInfo(LexerToken token, out int symbolIndex, out bool symbolCouldBeIdentifier)
		{
			symbolCouldBeIdentifier = false;
			switch (token.m_vType)
			{
			case Token.OPERATION:
				symbolIndex = this.m_vQuotedSymbolIndexes[token.m_vContent];
				return;
			case Token.IDENTIFIER:
				symbolIndex = this.m_vQuotedSymbolIndexes[token.m_vContent];
				if (symbolIndex < 0 || (this.m_vSymbolsFlags[symbolIndex] & 32) == 0)
				{
					symbolCouldBeIdentifier = true;
					return;
				}
				break;
			default:
				symbolIndex = -1;
				break;
			}
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x00106E9C File Offset: 0x0010509C
		public virtual bool LookaheadOK(int lookAheadSymbol, int symbolIndex, bool symbolCouldBeIdentifier)
		{
			if (lookAheadSymbol < 0 || symbolIndex < 0)
			{
				return true;
			}
			if (!base.IsTerminal(lookAheadSymbol))
			{
				return this.CanBePrediction(lookAheadSymbol, symbolIndex, symbolCouldBeIdentifier);
			}
			return lookAheadSymbol == symbolIndex;
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x00106EC0 File Offset: 0x001050C0
		public virtual bool CanBePrediction(int symbol, int firstSymbol, bool firstSymbolCouldBeIdentifier)
		{
			HashSet<int> hashSet = this.m_vHasPathToFirstSymbolTable[symbol];
			return hashSet.Contains(firstSymbol);
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x00106EE0 File Offset: 0x001050E0
		public int GetNextSymbol(long state)
		{
			int num = (int)state;
			int[] vRhs = this.m_vRules[num >> 12].m_vRhs;
			num &= 4095;
			if (num >= vRhs.Length)
			{
				return -1;
			}
			return vRhs[num];
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x00106F14 File Offset: 0x00105114
		public int GetRuleHead(long state)
		{
			int num = (int)state >> 12;
			return this.m_vRules[num].m_vHead;
		}

		// Token: 0x04001B57 RID: 6999
		public List<int> m_vTopLevelSymbols;

		// Token: 0x04001B58 RID: 7000
		public HashSet<int>[] m_vHasPathToFirstSymbolTable;

		// Token: 0x04001B59 RID: 7001
		public List<int>[] m_vHeadToRulesTable;

		// Token: 0x04001B5A RID: 7002
		public Dictionary<long, FlexibleSizeLongArray> m_vPredictionsTable;
	}
}
