using System;
using System.Collections.Generic;
using Oracle.SqlAndPlsqlParser.LocalParsing;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors
{
	// Token: 0x020002DE RID: 734
	internal class OracleMbEarleyRuleMultiProcessorTable
	{
		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x0010B3B8 File Offset: 0x001095B8
		public Dictionary<int, List<OracleMbEarleyRuleMultiProcessorToken>> RuleProcessors
		{
			get
			{
				return this.m_vRuleProcessors;
			}
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x0010B3C0 File Offset: 0x001095C0
		public OracleMbEarleyRuleMultiProcessorTable(EarleyParserGrammarDefinition grammar, OracleMbEarleyRuleMultiProcessorAddItem[] ruleProcessorItems)
		{
			if (grammar == null || ruleProcessorItems == null)
			{
				throw new ArgumentNullException("The parameters of the processor table constructor cannot be null.");
			}
			this.m_vEarleyGrammar = grammar;
			this.m_vRuleProcessorItems = ruleProcessorItems;
			this.Initialize();
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x0010B3F8 File Offset: 0x001095F8
		public virtual void Initialize()
		{
			foreach (OracleMbEarleyRuleMultiProcessorAddItem oracleMbEarleyRuleMultiProcessorAddItem in this.m_vRuleProcessorItems)
			{
				this.AddRuleProcessor(oracleMbEarleyRuleMultiProcessorAddItem.m_vHeadSymbol, oracleMbEarleyRuleMultiProcessorAddItem.m_vRHSSymbols, oracleMbEarleyRuleMultiProcessorAddItem.m_vRuleProcessor, oracleMbEarleyRuleMultiProcessorAddItem.m_vAddType);
			}
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x0010B44C File Offset: 0x0010964C
		public bool AddRuleProcessor(string headSymbol, string[] rhsSymbols, OracleMbEarleyRuleMultiProcessorDelegate ruleProcessor, OracleMbEarleyRuleMultiProcessorAddType addType)
		{
			if (headSymbol == null)
			{
				throw new OracleLpException(OracleLpExceptionType.RuleMultiProcessor, OracleLpExceptionError.InvalidRuleHeadSymbol, string.Format(OracleLpErrorStrings.GetErrorString(OracleLpExceptionError.InvalidRuleHeadSymbol), headSymbol));
			}
			if (rhsSymbols == null)
			{
				throw new OracleLpException(OracleLpExceptionType.RuleMultiProcessor, OracleLpExceptionError.InvalidRuleRHSSymbols, OracleLpErrorStrings.GetErrorString(OracleLpExceptionError.InvalidRuleRHSSymbols));
			}
			int num = this.m_vEarleyGrammar.m_vSymbolIndexes[headSymbol];
			if (num == -1)
			{
				throw new OracleLpException(OracleLpExceptionType.RuleMultiProcessor, OracleLpExceptionError.MissingParserSymbol, string.Format(OracleLpErrorStrings.GetErrorString(OracleLpExceptionError.MissingParserSymbol), headSymbol));
			}
			int h = num;
			int num2 = rhsSymbols.Length;
			int[] array = new int[num2];
			int i;
			for (i = 0; i < num2; i++)
			{
				string text = rhsSymbols[i];
				if (text == null)
				{
					array[i] = -1;
				}
				else
				{
					num = this.m_vEarleyGrammar.m_vSymbolIndexes[text];
					if (num == -1)
					{
						throw new OracleLpException(OracleLpExceptionType.RuleMultiProcessor, OracleLpExceptionError.MissingParserSymbol, string.Format(OracleLpErrorStrings.GetErrorString(OracleLpExceptionError.MissingParserSymbol), text));
					}
					array[i] = num;
				}
			}
			ParserRuleTuple[] vBaseSortedRules = this.m_vEarleyGrammar.m_vBaseSortedRules;
			ParserRuleTuple parserRuleTuple;
			switch (addType)
			{
			case OracleMbEarleyRuleMultiProcessorAddType.SpecificRule:
			{
				parserRuleTuple = new ParserRuleTuple(h, array, this.m_vEarleyGrammar);
				num = Array.BinarySearch<ParserRuleTuple>(vBaseSortedRules, parserRuleTuple, ParserRuleTupleBaseComparer.s_vInstance);
				if (num < 0)
				{
					throw new OracleLpException(OracleLpExceptionType.RuleMultiProcessor, OracleLpExceptionError.MissingRule, string.Format(OracleLpErrorStrings.GetErrorString(OracleLpExceptionError.MissingRule), parserRuleTuple));
				}
				int key = this.m_vEarleyGrammar.m_vSortedRulesIndexes[num];
				List<OracleMbEarleyRuleMultiProcessorToken> list;
				if (this.m_vRuleProcessors.ContainsKey(key))
				{
					list = this.m_vRuleProcessors[key];
				}
				else
				{
					list = new List<OracleMbEarleyRuleMultiProcessorToken>();
					this.m_vRuleProcessors[key] = list;
				}
				list.Add(new OracleMbEarleyRuleMultiProcessorToken(ruleProcessor, 0, num2));
				int j = num;
				while (j > 0)
				{
					j--;
					if (ParserRuleTupleBaseComparer.s_vInstance.Compare(parserRuleTuple, vBaseSortedRules[j]) != 0)
					{
						break;
					}
					key = this.m_vEarleyGrammar.m_vSortedRulesIndexes[j];
					if (this.m_vRuleProcessors.ContainsKey(key))
					{
						list = this.m_vRuleProcessors[key];
					}
					else
					{
						list = new List<OracleMbEarleyRuleMultiProcessorToken>();
						this.m_vRuleProcessors[key] = list;
					}
					list.Add(new OracleMbEarleyRuleMultiProcessorToken(ruleProcessor, 0, num2));
				}
				int num3 = vBaseSortedRules.Length - 1;
				j = num;
				while (j < num3)
				{
					j++;
					if (ParserRuleTupleBaseComparer.s_vInstance.Compare(parserRuleTuple, vBaseSortedRules[j]) != 0)
					{
						break;
					}
					key = this.m_vEarleyGrammar.m_vSortedRulesIndexes[j];
					if (this.m_vRuleProcessors.ContainsKey(key))
					{
						list = this.m_vRuleProcessors[key];
					}
					else
					{
						list = new List<OracleMbEarleyRuleMultiProcessorToken>();
						this.m_vRuleProcessors[key] = list;
					}
					list.Add(new OracleMbEarleyRuleMultiProcessorToken(ruleProcessor, 0, num2));
				}
				return true;
			}
			case OracleMbEarleyRuleMultiProcessorAddType.AllMatchingRules:
				parserRuleTuple = new ParserRuleTuple(h, new int[0], this.m_vEarleyGrammar);
				num = Array.BinarySearch<ParserRuleTuple>(vBaseSortedRules, parserRuleTuple, ParserRuleTupleBaseComparer.s_vInstance);
				if (num < 0)
				{
					num = ~num;
				}
				parserRuleTuple = new ParserRuleTuple(h, array, this.m_vEarleyGrammar);
				for (i = num; i < vBaseSortedRules.Length; i++)
				{
					if (vBaseSortedRules[i].m_vBaseHead != parserRuleTuple.m_vBaseHead)
					{
						break;
					}
					int[] vBaseRhs = vBaseSortedRules[i].m_vBaseRhs;
					if (vBaseRhs.Length == num2)
					{
						int k;
						for (k = 0; k < num2; k++)
						{
							int num4 = parserRuleTuple.m_vBaseRhs[k];
							if (num4 != -1 && num4 != vBaseRhs[k])
							{
								break;
							}
						}
						if (k == num2)
						{
							num = this.m_vEarleyGrammar.m_vSortedRulesIndexes[i];
							List<OracleMbEarleyRuleMultiProcessorToken> list;
							if (this.m_vRuleProcessors.ContainsKey(num))
							{
								list = this.m_vRuleProcessors[num];
							}
							else
							{
								list = new List<OracleMbEarleyRuleMultiProcessorToken>();
								this.m_vRuleProcessors[num] = list;
							}
							list.Add(new OracleMbEarleyRuleMultiProcessorToken(ruleProcessor, 0, num2));
						}
					}
				}
				return true;
			case OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith:
				parserRuleTuple = new ParserRuleTuple(h, new int[0], this.m_vEarleyGrammar);
				num = Array.BinarySearch<ParserRuleTuple>(vBaseSortedRules, parserRuleTuple, ParserRuleTupleBaseComparer.s_vInstance);
				if (num < 0)
				{
					num = ~num;
				}
				parserRuleTuple = new ParserRuleTuple(h, array, this.m_vEarleyGrammar);
				for (i = num; i < vBaseSortedRules.Length; i++)
				{
					if (vBaseSortedRules[i].m_vBaseHead != parserRuleTuple.m_vBaseHead)
					{
						break;
					}
					int[] vBaseRhs = vBaseSortedRules[i].m_vBaseRhs;
					if (vBaseRhs.Length >= num2)
					{
						int k;
						for (k = 0; k < num2; k++)
						{
							int num4 = parserRuleTuple.m_vBaseRhs[k];
							if (num4 != -1 && num4 != vBaseRhs[k])
							{
								break;
							}
						}
						if (k == num2)
						{
							num = this.m_vEarleyGrammar.m_vSortedRulesIndexes[i];
							List<OracleMbEarleyRuleMultiProcessorToken> list;
							if (this.m_vRuleProcessors.ContainsKey(num))
							{
								list = this.m_vRuleProcessors[num];
							}
							else
							{
								list = new List<OracleMbEarleyRuleMultiProcessorToken>();
								this.m_vRuleProcessors[num] = list;
							}
							list.Add(new OracleMbEarleyRuleMultiProcessorToken(ruleProcessor, 0, num2));
						}
					}
				}
				return true;
			case OracleMbEarleyRuleMultiProcessorAddType.AllRulesEndingWith:
				parserRuleTuple = new ParserRuleTuple(h, new int[0], this.m_vEarleyGrammar);
				num = Array.BinarySearch<ParserRuleTuple>(vBaseSortedRules, parserRuleTuple, ParserRuleTupleBaseComparer.s_vInstance);
				if (num < 0)
				{
					num = ~num;
				}
				parserRuleTuple = new ParserRuleTuple(h, array, this.m_vEarleyGrammar);
				for (i = num; i < vBaseSortedRules.Length; i++)
				{
					if (vBaseSortedRules[i].m_vBaseHead != parserRuleTuple.m_vBaseHead)
					{
						break;
					}
					int[] vBaseRhs = vBaseSortedRules[i].m_vBaseRhs;
					int num3 = vBaseRhs.Length;
					if (num3 >= num2)
					{
						num3 -= num2;
						int k;
						for (k = 0; k < num2; k++)
						{
							int num4 = parserRuleTuple.m_vBaseRhs[k];
							if (num4 != -1 && num4 != vBaseRhs[num3 + k])
							{
								break;
							}
						}
						if (k == num2)
						{
							num = this.m_vEarleyGrammar.m_vSortedRulesIndexes[i];
							List<OracleMbEarleyRuleMultiProcessorToken> list;
							if (this.m_vRuleProcessors.ContainsKey(num))
							{
								list = this.m_vRuleProcessors[num];
							}
							else
							{
								list = new List<OracleMbEarleyRuleMultiProcessorToken>();
								this.m_vRuleProcessors[num] = list;
							}
							list.Add(new OracleMbEarleyRuleMultiProcessorToken(ruleProcessor, num3, num2));
						}
					}
				}
				return true;
			case OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules:
				parserRuleTuple = new ParserRuleTuple(h, new int[0], this.m_vEarleyGrammar);
				num = Array.BinarySearch<ParserRuleTuple>(vBaseSortedRules, parserRuleTuple, ParserRuleTupleBaseComparer.s_vInstance);
				if (num < 0)
				{
					num = ~num;
				}
				parserRuleTuple = new ParserRuleTuple(h, array, this.m_vEarleyGrammar);
				for (i = num; i < vBaseSortedRules.Length; i++)
				{
					if (vBaseSortedRules[i].m_vBaseHead != parserRuleTuple.m_vBaseHead)
					{
						break;
					}
					int[] vBaseRhs = vBaseSortedRules[i].m_vBaseRhs;
					if (vBaseRhs.Length >= num2)
					{
						int num5 = vBaseRhs.Length - num2;
						for (int l = 0; l <= num5; l++)
						{
							int k;
							for (k = 0; k < num2; k++)
							{
								int num4 = parserRuleTuple.m_vBaseRhs[k];
								if (num4 != -1 && num4 != vBaseRhs[k + l])
								{
									break;
								}
							}
							if (k == num2)
							{
								num = this.m_vEarleyGrammar.m_vSortedRulesIndexes[i];
								List<OracleMbEarleyRuleMultiProcessorToken> list;
								if (this.m_vRuleProcessors.ContainsKey(num))
								{
									list = this.m_vRuleProcessors[num];
								}
								else
								{
									list = new List<OracleMbEarleyRuleMultiProcessorToken>();
									this.m_vRuleProcessors[num] = list;
								}
								list.Add(new OracleMbEarleyRuleMultiProcessorToken(ruleProcessor, l, num2));
								break;
							}
						}
					}
				}
				return true;
			}
			parserRuleTuple = new ParserRuleTuple(h, new int[0], this.m_vEarleyGrammar);
			num = Array.BinarySearch<ParserRuleTuple>(vBaseSortedRules, parserRuleTuple, ParserRuleTupleBaseComparer.s_vInstance);
			if (num < 0)
			{
				num = ~num;
			}
			i = num;
			while (i < vBaseSortedRules.Length && vBaseSortedRules[i].m_vBaseHead == parserRuleTuple.m_vBaseHead)
			{
				num = this.m_vEarleyGrammar.m_vSortedRulesIndexes[i];
				List<OracleMbEarleyRuleMultiProcessorToken> list;
				if (this.m_vRuleProcessors.ContainsKey(num))
				{
					list = this.m_vRuleProcessors[num];
				}
				else
				{
					list = new List<OracleMbEarleyRuleMultiProcessorToken>();
					this.m_vRuleProcessors[num] = list;
				}
				list.Add(new OracleMbEarleyRuleMultiProcessorToken(ruleProcessor, 0, -1));
				i++;
			}
			return true;
		}

		// Token: 0x04001CC4 RID: 7364
		protected EarleyParserGrammarDefinition m_vEarleyGrammar;

		// Token: 0x04001CC5 RID: 7365
		protected OracleMbEarleyRuleMultiProcessorAddItem[] m_vRuleProcessorItems;

		// Token: 0x04001CC6 RID: 7366
		protected Dictionary<int, List<OracleMbEarleyRuleMultiProcessorToken>> m_vRuleProcessors = new Dictionary<int, List<OracleMbEarleyRuleMultiProcessorToken>>();
	}
}
