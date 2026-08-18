using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x02000272 RID: 626
	internal class Earley : Parser
	{
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x001048F4 File Offset: 0x00102AF4
		public EarleyParserGrammarDefinition EarleyGrammar
		{
			get
			{
				return this.m_vEarleyParserGrammar;
			}
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x001048FC File Offset: 0x00102AFC
		public Earley(EarleyParserGrammarDefinition grammarDefinition)
		{
			this.m_vEarleyParserGrammar = grammarDefinition;
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0010490C File Offset: 0x00102B0C
		public override ParseNode Parse(string scriptText, List<LexerToken> src)
		{
			if (src.Count < 1)
			{
				return null;
			}
			Earley.EarleyParsingHelper earleyParsingHelper;
			using (new PerformanceTimer("Parse"))
			{
				earleyParsingHelper = new Earley.EarleyParsingHelper(scriptText, src, this.m_vEarleyParserGrammar);
				earleyParsingHelper.Parse();
			}
			ParseNode result;
			using (new PerformanceTimer("Build Parse Tree"))
			{
				result = earleyParsingHelper.BuildParseTree();
			}
			return result;
		}

		// Token: 0x04001B42 RID: 6978
		protected EarleyParserGrammarDefinition m_vEarleyParserGrammar;

		// Token: 0x02000273 RID: 627
		protected class EarleyParsingHelper : Parser.ParsingHelper
		{
			// Token: 0x060018C6 RID: 6342 RVA: 0x0010498C File Offset: 0x00102B8C
			public EarleyParsingHelper(string scriptText, List<LexerToken> lexerTokens, EarleyParserGrammarDefinition grammar) : base(scriptText, lexerTokens)
			{
				this.m_vEarleyParserGrammar = grammar;
				this.m_vIdentifierSymbolIndex = ((OracleSqlEarleyParserGrammarDefinition)this.m_vEarleyParserGrammar).m_vIdentifierSymbolIndex;
				this.m_vIdentifierSymbolIndexMask = (long)this.m_vIdentifierSymbolIndex << 32;
				this.m_vIdentifierSymbolIndexMask1 = this.m_vIdentifierSymbolIndexMask + 4294967296L;
			}

			// Token: 0x060018C7 RID: 6343 RVA: 0x00104A24 File Offset: 0x00102C24
			public void Parse()
			{
				int count = this.m_vLexerTokens.Count;
				HashSet<int>[] vHasPathToFirstSymbolTable = this.m_vEarleyParserGrammar.m_vHasPathToFirstSymbolTable;
				this.m_vWikiMatrix = new Dictionary<int, FlexibleSizeLongArray>[count + 1];
				this.m_vPredictionMatrix = new FlexibleSizeLongArray[count + 1];
				this.m_vActivePositionLexerToken = this.m_vLexerTokens[0];
				this.m_vEarleyParserGrammar.GetTokensInfo(this.m_vActivePositionLexerToken, out this.m_vActivePositionSymbolIndex, out this.m_vActivePositionSymbolCouldBeIdentifier);
				if (count > 1)
				{
					this.m_vActivePositionLookAheadLexerToken = this.m_vLexerTokens[1];
					this.m_vEarleyParserGrammar.GetTokensInfo(this.m_vActivePositionLookAheadLexerToken, out this.m_vActivePositionLookAheadSymbolIndex, out this.m_vActivePositionLookAheadSymbolCouldBeIdentifier);
				}
				else
				{
					this.m_vActivePositionLookAheadLexerToken = null;
					this.m_vActivePositionLookAheadSymbolIndex = -1;
					this.m_vActivePositionLookAheadSymbolCouldBeIdentifier = false;
				}
				foreach (int num in this.m_vEarleyParserGrammar.m_vTopLevelSymbols)
				{
					HashSet<int> hashSet = vHasPathToFirstSymbolTable[num];
					if (hashSet.Contains(this.m_vActivePositionSymbolIndex))
					{
						this.m_vPredictionTuples.Add((long)num << 32 | (long)((ulong)this.m_vActivePositionSymbolIndex));
					}
					if (this.m_vActivePositionSymbolCouldBeIdentifier && hashSet.Contains(this.m_vIdentifierSymbolIndex))
					{
						this.m_vPredictionTuples.Add((long)num << 32 | (long)((ulong)this.m_vIdentifierSymbolIndex));
					}
				}
				this.m_vCurrentMatrixCell = new Dictionary<int, FlexibleSizeLongArray>(16);
				this.m_vWikiMatrix[0] = this.m_vCurrentMatrixCell;
				this.Predict();
				this.m_vPredictionTuples.Clear();
				this.Scan();
				this.m_vInputPosition = 1;
				while (this.m_vInputPosition < count)
				{
					this.m_vActivePositionLexerToken = this.m_vActivePositionLookAheadLexerToken;
					this.m_vActivePositionSymbolIndex = this.m_vActivePositionLookAheadSymbolIndex;
					this.m_vActivePositionSymbolCouldBeIdentifier = this.m_vActivePositionLookAheadSymbolCouldBeIdentifier;
					if (this.m_vInputPosition < count - 1)
					{
						this.m_vActivePositionLookAheadLexerToken = this.m_vLexerTokens[this.m_vInputPosition + 1];
						this.m_vEarleyParserGrammar.GetTokensInfo(this.m_vActivePositionLookAheadLexerToken, out this.m_vActivePositionLookAheadSymbolIndex, out this.m_vActivePositionLookAheadSymbolCouldBeIdentifier);
					}
					else
					{
						this.m_vActivePositionLookAheadLexerToken = null;
						this.m_vActivePositionLookAheadSymbolIndex = -1;
						this.m_vActivePositionLookAheadSymbolCouldBeIdentifier = false;
					}
					this.m_vCurrentMatrixCell = this.m_vWikiMatrix[this.m_vInputPosition];
					this.Complete();
					this.Predict();
					this.m_vStatesToComplete.Clear();
					this.m_vStatesMarkedForCompletion.Clear();
					this.m_vPredictionTuples.Clear();
					this.Scan();
					this.m_vInputPosition++;
				}
				this.m_vCurrentMatrixCell = this.m_vWikiMatrix[this.m_vInputPosition];
				this.Complete();
			}

			// Token: 0x060018C8 RID: 6344 RVA: 0x00104CB4 File Offset: 0x00102EB4
			protected void HandleScanError()
			{
				int num = this.m_vInputPosition - 20;
				if (num < 0)
				{
					num = 0;
				}
				int num2 = this.m_vInputPosition;
				int count = this.m_vLexerTokens.Count;
				int num3 = num + 40;
				if (num3 >= count)
				{
					num3 = count - 1;
				}
				num = this.m_vLexerTokens[num].m_vBegin;
				num3 = this.m_vLexerTokens[num3].m_vEnd;
				num2 = this.m_vLexerTokens[num2].m_vBegin - num;
				string text = this.m_vScriptText.Substring(num, num3 - num);
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < num2; i++)
				{
					char c = text[i];
					if (char.IsControl(c) || char.IsWhiteSpace(c))
					{
						stringBuilder.Append(c);
					}
					else
					{
						stringBuilder.Append(' ');
					}
				}
				stringBuilder.Append('^');
				for (int i = num2 + 1; i < text.Length; i++)
				{
					char c = text[i];
					if (char.IsControl(c) || char.IsWhiteSpace(c))
					{
						stringBuilder.Append(c);
					}
					else
					{
						stringBuilder.Append(' ');
					}
				}
				string text2 = stringBuilder.ToString();
				stringBuilder.Clear();
				char[] separator = new char[]
				{
					'\n'
				};
				string[] array = text.Split(separator);
				string[] array2 = text2.Split(separator);
				stringBuilder.Append(">>>\n");
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(array[i]);
					stringBuilder.Append('\n');
					stringBuilder.Append(array2[i]);
					stringBuilder.Append('\n');
				}
				stringBuilder.Append(">>>\n");
				int[] expectedTokens = this.GetExpectedTokens();
				stringBuilder.Append("Possible follow up tokens are: \n");
				foreach (int num4 in expectedTokens)
				{
					stringBuilder.Append('\t');
					stringBuilder.Append(this.m_vEarleyParserGrammar.m_vAllSymbols[num4]);
					stringBuilder.Append('\n');
				}
				if (expectedTokens.Length > 20)
				{
					stringBuilder.Append("\t...\n");
				}
				text = string.Format(OracleScParserErrorStrings.GetErrorString(ParserExceptionError.NoParseFollowUp), this.m_vActivePositionLexerToken.m_vContent, stringBuilder.ToString());
				throw new ParserException(ParserExceptionType.Parser, ParserExceptionError.NoParseFollowUp, text);
			}

			// Token: 0x060018C9 RID: 6345 RVA: 0x00104F08 File Offset: 0x00103108
			protected int[] GetExpectedTokens()
			{
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				Dictionary<int, FlexibleSizeLongArray> dictionary2 = this.m_vWikiMatrix[this.m_vInputPosition];
				foreach (KeyValuePair<int, FlexibleSizeLongArray> keyValuePair in dictionary2)
				{
					int key = keyValuePair.Key;
					if (key >= 0)
					{
						if (this.m_vEarleyParserGrammar.IsTerminal(key))
						{
							if (dictionary.ContainsKey(key))
							{
								Dictionary<int, int> dictionary3;
								int key2;
								(dictionary3 = dictionary)[key2 = key] = dictionary3[key2] + 1;
							}
							else
							{
								dictionary[key] = 1;
							}
						}
						else
						{
							foreach (int num in this.m_vEarleyParserGrammar.m_vHasPathToFirstSymbolTable[key])
							{
								if (dictionary.ContainsKey(num))
								{
									Dictionary<int, int> dictionary4;
									int key3;
									(dictionary4 = dictionary)[key3 = num] = dictionary4[key3] + 1;
								}
								else
								{
									dictionary[num] = 1;
								}
							}
						}
					}
				}
				int count = dictionary.Count;
				if (count > 0)
				{
					long[] array = new long[count];
					int[] array2 = new int[count];
					int i = 0;
					foreach (KeyValuePair<int, int> keyValuePair2 in dictionary)
					{
						array[i++] = ((long)(-(long)keyValuePair2.Value) << 32) + (long)keyValuePair2.Key;
					}
					Array.Sort<long>(array);
					for (i = 0; i < count; i++)
					{
						array2[i] = (int)(array[i] & (long)((ulong)-1));
					}
					return array2;
				}
				return new int[0];
			}

			// Token: 0x060018CA RID: 6346 RVA: 0x001050D4 File Offset: 0x001032D4
			protected void Predict()
			{
				FlexibleSizeLongArray flexibleSizeLongArray = null;
				FlexibleSizeLongArray y = null;
				Dictionary<long, FlexibleSizeLongArray> vPredictionsTable = this.m_vEarleyParserGrammar.m_vPredictionsTable;
				foreach (long key in this.m_vPredictionTuples)
				{
					if (vPredictionsTable.TryGetValue(key, out y))
					{
						flexibleSizeLongArray = FlexibleSizeLongArray.Merge(flexibleSizeLongArray, y);
					}
				}
				if (flexibleSizeLongArray != null)
				{
					this.m_vPredictionMatrix[this.m_vInputPosition] = flexibleSizeLongArray;
				}
			}

			// Token: 0x060018CB RID: 6347 RVA: 0x00105158 File Offset: 0x00103358
			protected bool Scan()
			{
				FlexibleSizeLongArray flexibleSizeLongArray = null;
				FlexibleSizeLongArray flexibleSizeLongArray2 = null;
				FlexibleSizeLongArray flexibleSizeLongArray3 = null;
				Dictionary<int, FlexibleSizeLongArray> dictionary = new Dictionary<int, FlexibleSizeLongArray>(5);
				FlexibleSizeLongArray flexibleSizeLongArray4;
				this.m_vCurrentMatrixCell.TryGetValue(this.m_vActivePositionSymbolIndex, out flexibleSizeLongArray4);
				FlexibleSizeLongArray flexibleSizeLongArray5;
				if (this.m_vActivePositionSymbolCouldBeIdentifier && this.m_vCurrentMatrixCell.TryGetValue(this.m_vIdentifierSymbolIndex, out flexibleSizeLongArray5))
				{
					flexibleSizeLongArray4 = FlexibleSizeLongArray.Merge(flexibleSizeLongArray4, flexibleSizeLongArray5);
				}
				if (flexibleSizeLongArray4 != null)
				{
					int vContentSize = flexibleSizeLongArray4.m_vContentSize;
					long[] vArray = flexibleSizeLongArray4.m_vArray;
					for (int i = 0; i < vContentSize; i++)
					{
						long num = vArray[i];
						num += 1L;
						int num2 = (int)num;
						int[] vRhs = this.m_vEarleyParserGrammar.m_vRules[num2 >> 12].m_vRhs;
						num2 &= 4095;
						if (num2 >= vRhs.Length)
						{
							this.m_vStatesMarkedForCompletion.Add(num);
							this.m_vStatesToComplete.Enqueue(num);
							flexibleSizeLongArray = FlexibleSizeLongArray.Append(flexibleSizeLongArray, num);
						}
						else
						{
							int num3 = vRhs[num2];
							if (this.m_vEarleyParserGrammar.IsTerminal(num3))
							{
								if (num3 == this.m_vActivePositionLookAheadSymbolIndex)
								{
									flexibleSizeLongArray2 = FlexibleSizeLongArray.Append(flexibleSizeLongArray2, num);
								}
								else if (this.m_vActivePositionLookAheadSymbolCouldBeIdentifier && num3 == this.m_vIdentifierSymbolIndex)
								{
									flexibleSizeLongArray3 = FlexibleSizeLongArray.Append(flexibleSizeLongArray3, num);
								}
							}
							else
							{
								HashSet<int> hashSet = this.m_vEarleyParserGrammar.m_vHasPathToFirstSymbolTable[num3];
								bool flag = false;
								if (hashSet.Contains(this.m_vActivePositionLookAheadSymbolIndex))
								{
									this.m_vPredictionTuples.Add((long)num3 << 32 | (long)((ulong)this.m_vActivePositionLookAheadSymbolIndex));
									flag = true;
								}
								if (this.m_vActivePositionLookAheadSymbolCouldBeIdentifier && hashSet.Contains(this.m_vIdentifierSymbolIndex))
								{
									this.m_vPredictionTuples.Add((long)num3 << 32 | (long)((ulong)this.m_vIdentifierSymbolIndex));
									flag = true;
								}
								if (flag)
								{
									dictionary.TryGetValue(num3, out flexibleSizeLongArray5);
									dictionary[num3] = FlexibleSizeLongArray.Append(flexibleSizeLongArray5, num);
								}
							}
						}
					}
				}
				flexibleSizeLongArray4 = this.m_vPredictionMatrix[this.m_vInputPosition];
				if (flexibleSizeLongArray4 != null)
				{
					int vContentSize = flexibleSizeLongArray4.m_vContentSize;
					long[] vArray2 = flexibleSizeLongArray4.m_vArray;
					long num4 = (long)this.m_vInputPosition << 32;
					long num5 = (long)this.m_vActivePositionSymbolIndex << 32;
					long num6 = num5 + 4294967296L;
					int num7 = Array.BinarySearch<long>(vArray2, 0, vContentSize, num5);
					if (num7 < 0)
					{
						num7 = ~num7;
					}
					for (int j = num7; j < vContentSize; j++)
					{
						long num = vArray2[j];
						if (num >= num6)
						{
							break;
						}
						num = (num4 | (num & (long)((ulong)-1)));
						num += 1L;
						int[] vRhs = this.m_vEarleyParserGrammar.m_vRules[(int)num >> 12].m_vRhs;
						if (1 == vRhs.Length)
						{
							this.m_vStatesMarkedForCompletion.Add(num);
							this.m_vStatesToComplete.Enqueue(num);
							flexibleSizeLongArray = FlexibleSizeLongArray.Insert(flexibleSizeLongArray, num);
						}
						else
						{
							int num3 = vRhs[1];
							if (this.m_vEarleyParserGrammar.IsTerminal(num3))
							{
								if (num3 == this.m_vActivePositionLookAheadSymbolIndex)
								{
									flexibleSizeLongArray2 = FlexibleSizeLongArray.Insert(flexibleSizeLongArray2, num);
								}
								else if (this.m_vActivePositionLookAheadSymbolCouldBeIdentifier && num3 == this.m_vIdentifierSymbolIndex)
								{
									flexibleSizeLongArray3 = FlexibleSizeLongArray.Insert(flexibleSizeLongArray3, num);
								}
							}
							else
							{
								HashSet<int> hashSet = this.m_vEarleyParserGrammar.m_vHasPathToFirstSymbolTable[num3];
								bool flag = false;
								if (hashSet.Contains(this.m_vActivePositionLookAheadSymbolIndex))
								{
									this.m_vPredictionTuples.Add((long)num3 << 32 | (long)((ulong)this.m_vActivePositionLookAheadSymbolIndex));
									flag = true;
								}
								if (this.m_vActivePositionLookAheadSymbolCouldBeIdentifier && hashSet.Contains(this.m_vIdentifierSymbolIndex))
								{
									this.m_vPredictionTuples.Add((long)num3 << 32 | (long)((ulong)this.m_vIdentifierSymbolIndex));
									flag = true;
								}
								if (flag)
								{
									dictionary.TryGetValue(num3, out flexibleSizeLongArray5);
									dictionary[num3] = FlexibleSizeLongArray.Insert(flexibleSizeLongArray5, num);
								}
							}
						}
					}
					if (this.m_vActivePositionSymbolCouldBeIdentifier)
					{
						num7 = Array.BinarySearch<long>(vArray2, 0, vContentSize, this.m_vIdentifierSymbolIndexMask);
						if (num7 < 0)
						{
							num7 = ~num7;
						}
						for (int k = num7; k < vContentSize; k++)
						{
							long num = vArray2[k];
							if (num >= this.m_vIdentifierSymbolIndexMask1)
							{
								break;
							}
							num = (num4 | (num & (long)((ulong)-1)));
							num += 1L;
							int[] vRhs = this.m_vEarleyParserGrammar.m_vRules[(int)num >> 12].m_vRhs;
							if (1 == vRhs.Length)
							{
								this.m_vStatesMarkedForCompletion.Add(num);
								this.m_vStatesToComplete.Enqueue(num);
								flexibleSizeLongArray = FlexibleSizeLongArray.Insert(flexibleSizeLongArray, num);
							}
							else
							{
								int num3 = vRhs[1];
								if (this.m_vEarleyParserGrammar.IsTerminal(num3))
								{
									if (num3 == this.m_vActivePositionLookAheadSymbolIndex)
									{
										flexibleSizeLongArray2 = FlexibleSizeLongArray.Insert(flexibleSizeLongArray2, num);
									}
									else if (this.m_vActivePositionLookAheadSymbolCouldBeIdentifier && num3 == this.m_vIdentifierSymbolIndex)
									{
										flexibleSizeLongArray3 = FlexibleSizeLongArray.Insert(flexibleSizeLongArray3, num);
									}
								}
								else
								{
									HashSet<int> hashSet = this.m_vEarleyParserGrammar.m_vHasPathToFirstSymbolTable[num3];
									bool flag = false;
									if (hashSet.Contains(this.m_vActivePositionLookAheadSymbolIndex))
									{
										this.m_vPredictionTuples.Add((long)num3 << 32 | (long)((ulong)this.m_vActivePositionLookAheadSymbolIndex));
										flag = true;
									}
									if (this.m_vActivePositionLookAheadSymbolCouldBeIdentifier && hashSet.Contains(this.m_vIdentifierSymbolIndex))
									{
										this.m_vPredictionTuples.Add((long)num3 << 32 | (long)((ulong)this.m_vIdentifierSymbolIndex));
										flag = true;
									}
									if (flag)
									{
										dictionary.TryGetValue(num3, out flexibleSizeLongArray5);
										dictionary[num3] = FlexibleSizeLongArray.Insert(flexibleSizeLongArray5, num);
									}
								}
							}
						}
					}
				}
				if (flexibleSizeLongArray != null)
				{
					dictionary[-1] = flexibleSizeLongArray;
				}
				if (flexibleSizeLongArray2 != null)
				{
					dictionary[this.m_vActivePositionLookAheadSymbolIndex] = flexibleSizeLongArray2;
				}
				if (this.m_vActivePositionLookAheadSymbolCouldBeIdentifier && flexibleSizeLongArray3 != null)
				{
					dictionary[this.m_vIdentifierSymbolIndex] = flexibleSizeLongArray3;
				}
				if (dictionary.Count == 0)
				{
					this.HandleScanError();
				}
				this.m_vWikiMatrix[this.m_vInputPosition + 1] = dictionary;
				this.m_vCurrentMatrixCell = dictionary;
				return true;
			}

			// Token: 0x060018CC RID: 6348 RVA: 0x00105704 File Offset: 0x00103904
			protected void Complete()
			{
				ParserRuleTuple[] vRules = this.m_vEarleyParserGrammar.m_vRules;
				HashSet<int>[] vHasPathToFirstSymbolTable = this.m_vEarleyParserGrammar.m_vHasPathToFirstSymbolTable;
				FlexibleSizeLongArray flexibleSizeLongArray = null;
				FlexibleSizeLongArray flexibleSizeLongArray2 = null;
				FlexibleSizeLongArray flexibleSizeLongArray3 = null;
				this.m_vCurrentMatrixCell.TryGetValue(-1, out flexibleSizeLongArray);
				this.m_vCurrentMatrixCell.TryGetValue(this.m_vActivePositionSymbolIndex, out flexibleSizeLongArray2);
				if (this.m_vActivePositionSymbolCouldBeIdentifier)
				{
					this.m_vCurrentMatrixCell.TryGetValue(this.m_vIdentifierSymbolIndex, out flexibleSizeLongArray3);
				}
				int i = this.m_vStatesToComplete.Count;
				while (i > 0)
				{
					long num = this.m_vStatesToComplete.Dequeue();
					i--;
					int num2 = (int)(num >> 32);
					Dictionary<int, FlexibleSizeLongArray> dictionary = this.m_vWikiMatrix[num2];
					int ruleHead = this.m_vEarleyParserGrammar.GetRuleHead(num);
					FlexibleSizeLongArray flexibleSizeLongArray4 = this.m_vPredictionMatrix[num2];
					FlexibleSizeLongArray flexibleSizeLongArray5;
					if (flexibleSizeLongArray4 != null)
					{
						long[] vArray = flexibleSizeLongArray4.m_vArray;
						long num3 = (long)ruleHead << 32;
						long num4 = num3 + 4294967296L;
						int vContentSize = flexibleSizeLongArray4.m_vContentSize;
						int num5 = Array.BinarySearch<long>(vArray, 0, vContentSize, num3);
						if (num5 < 0)
						{
							num5 = ~num5;
						}
						if (num5 < vContentSize && (vArray[num5] & num3) != 0L)
						{
							long num6 = (long)num2 << 32;
							for (int j = num5; j < vContentSize; j++)
							{
								num = vArray[j];
								if (num >= num4)
								{
									break;
								}
								num += 1L;
								int[] vRhs = vRules[(int)num >> 12].m_vRhs;
								num = (num6 | (num & (long)((ulong)-1)));
								if (vRhs.Length == 1)
								{
									if (!this.m_vStatesMarkedForCompletion.Contains(num))
									{
										this.m_vStatesMarkedForCompletion.Add(num);
										this.m_vStatesToComplete.Enqueue(num);
										flexibleSizeLongArray = FlexibleSizeLongArray.Insert(flexibleSizeLongArray, num);
										i++;
									}
								}
								else
								{
									int num7 = vRhs[1];
									if (!this.m_vEarleyParserGrammar.IsTerminal(num7))
									{
										HashSet<int> hashSet = vHasPathToFirstSymbolTable[num7];
										bool flag = false;
										if (hashSet.Contains(this.m_vActivePositionSymbolIndex))
										{
											this.m_vPredictionTuples.Add((long)num7 << 32 | (long)((ulong)this.m_vActivePositionSymbolIndex));
											flag = true;
										}
										if (this.m_vActivePositionSymbolCouldBeIdentifier && hashSet.Contains(this.m_vIdentifierSymbolIndex))
										{
											this.m_vPredictionTuples.Add((long)num7 << 32 | (long)((ulong)this.m_vIdentifierSymbolIndex));
											flag = true;
										}
										if (flag)
										{
											this.m_vCurrentMatrixCell.TryGetValue(num7, out flexibleSizeLongArray5);
											this.m_vCurrentMatrixCell[num7] = FlexibleSizeLongArray.Insert(flexibleSizeLongArray5, num);
										}
									}
									else if (num7 == this.m_vActivePositionSymbolIndex)
									{
										flexibleSizeLongArray2 = FlexibleSizeLongArray.Insert(flexibleSizeLongArray2, num);
									}
									else if (this.m_vActivePositionSymbolCouldBeIdentifier && num7 == this.m_vIdentifierSymbolIndex)
									{
										flexibleSizeLongArray3 = FlexibleSizeLongArray.Insert(flexibleSizeLongArray3, num);
									}
								}
							}
						}
					}
					if (dictionary.TryGetValue(ruleHead, out flexibleSizeLongArray5))
					{
						int vContentSize2 = flexibleSizeLongArray5.m_vContentSize;
						long[] vArray2 = flexibleSizeLongArray5.m_vArray;
						for (int k = 0; k < vContentSize2; k++)
						{
							num = vArray2[k];
							num += 1L;
							int num8 = (int)num;
							int[] vRhs = vRules[num8 >> 12].m_vRhs;
							num8 &= 4095;
							if (num8 >= vRhs.Length)
							{
								if (!this.m_vStatesMarkedForCompletion.Contains(num))
								{
									this.m_vStatesMarkedForCompletion.Add(num);
									this.m_vStatesToComplete.Enqueue(num);
									flexibleSizeLongArray = FlexibleSizeLongArray.Insert(flexibleSizeLongArray, num);
									i++;
								}
							}
							else
							{
								int num7 = vRhs[num8];
								if (!this.m_vEarleyParserGrammar.IsTerminal(num7))
								{
									HashSet<int> hashSet = vHasPathToFirstSymbolTable[num7];
									bool flag = false;
									if (hashSet.Contains(this.m_vActivePositionSymbolIndex))
									{
										this.m_vPredictionTuples.Add((long)num7 << 32 | (long)((ulong)this.m_vActivePositionSymbolIndex));
										flag = true;
									}
									if (this.m_vActivePositionSymbolCouldBeIdentifier && hashSet.Contains(this.m_vIdentifierSymbolIndex))
									{
										this.m_vPredictionTuples.Add((long)num7 << 32 | (long)((ulong)this.m_vIdentifierSymbolIndex));
										flag = true;
									}
									if (flag)
									{
										this.m_vCurrentMatrixCell.TryGetValue(num7, out flexibleSizeLongArray5);
										this.m_vCurrentMatrixCell[num7] = FlexibleSizeLongArray.Insert(flexibleSizeLongArray5, num);
									}
								}
								else if (num7 == this.m_vActivePositionSymbolIndex)
								{
									flexibleSizeLongArray2 = FlexibleSizeLongArray.Insert(flexibleSizeLongArray2, num);
								}
								else if (this.m_vActivePositionSymbolCouldBeIdentifier && num7 == this.m_vIdentifierSymbolIndex)
								{
									flexibleSizeLongArray3 = FlexibleSizeLongArray.Insert(flexibleSizeLongArray3, num);
								}
							}
						}
					}
				}
				if (flexibleSizeLongArray != null)
				{
					this.m_vCurrentMatrixCell[-1] = flexibleSizeLongArray;
				}
				if (flexibleSizeLongArray2 != null)
				{
					this.m_vCurrentMatrixCell[this.m_vActivePositionSymbolIndex] = flexibleSizeLongArray2;
				}
				if (this.m_vActivePositionSymbolCouldBeIdentifier && flexibleSizeLongArray3 != null)
				{
					this.m_vCurrentMatrixCell[this.m_vIdentifierSymbolIndex] = flexibleSizeLongArray3;
				}
			}

			// Token: 0x060018CD RID: 6349 RVA: 0x00105B6C File Offset: 0x00103D6C
			public ParseNode BuildParseTree()
			{
				Queue<EarleyParseTreeBuildState> queue = new Queue<EarleyParseTreeBuildState>(256);
				ParserRuleTuple parserRuleTuple = null;
				ParseNode parseNode = null;
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				ParseNode result;
				try
				{
					int count = this.m_vLexerTokens.Count;
					if (count == 0)
					{
						result = new ParseNode(0, count, -1, -1);
					}
					else
					{
						FlexibleSizeLongArray flexibleSizeLongArray;
						if (!this.m_vWikiMatrix[count].TryGetValue(-1, out flexibleSizeLongArray) || flexibleSizeLongArray.m_vContentSize == 0)
						{
							throw new ParserException(ParserExceptionType.Parser, ParserExceptionError.ParseTreeBuildError, "Parse tree build: no suitable branches");
						}
						long[] vArray = flexibleSizeLongArray.m_vArray;
						int vContentSize = flexibleSizeLongArray.m_vContentSize;
						int[] vSymbolsFlags = this.m_vEarleyParserGrammar.m_vSymbolsFlags;
						for (int i = vContentSize - 1; i >= 0; i--)
						{
							long num4 = vArray[i];
							int num5 = (int)num4 & 4095;
							int num6 = (int)num4 >> 12;
							ParserRuleTuple parserRuleTuple2 = this.m_vEarleyParserGrammar.m_vRules[num6];
							if ((vSymbolsFlags[parserRuleTuple2.m_vHead] & 6) == 2)
							{
								parseNode = new ParseNode(0, count, parserRuleTuple2.m_vHead, parserRuleTuple2.m_vHead);
								parseNode.AppendUsedRule(num6);
								queue.Enqueue(new EarleyParseTreeBuildState(0, count, num6, num5, parserRuleTuple2, parseNode));
								IL_3E7:
								while (queue.Count != 0)
								{
									EarleyParseTreeBuildState earleyParseTreeBuildState = queue.Dequeue();
									parserRuleTuple2 = earleyParseTreeBuildState.m_vRule;
									num5 = earleyParseTreeBuildState.m_vDot;
									num6 = earleyParseTreeBuildState.m_vRuleIdx;
									int num7 = parserRuleTuple2.m_vRhs[num5 - 1];
									int num8 = earleyParseTreeBuildState.m_vEnd - 1;
									int vHead = parserRuleTuple2.m_vHead;
									int num9 = parserRuleTuple2.m_vRhs.Length;
									long value = ((long)earleyParseTreeBuildState.m_vStart << 32) + ((long)earleyParseTreeBuildState.m_vRuleIdx << 12) + (long)earleyParseTreeBuildState.m_vDot - 1L;
									if (this.m_vEarleyParserGrammar.IsTerminal(num7))
									{
										if (num9 == 1)
										{
											earleyParseTreeBuildState.m_vParentNode.PayloadIn = num7;
										}
										else if (num5 == 1)
										{
											earleyParseTreeBuildState.m_vParentNode.AddChild(new ParseNode(num8, earleyParseTreeBuildState.m_vEnd, num7, num7));
										}
										else
										{
											earleyParseTreeBuildState.m_vParentNode.AddChild(new ParseNode(num8, earleyParseTreeBuildState.m_vEnd, num7, num7));
											earleyParseTreeBuildState.m_vEnd = num8;
											earleyParseTreeBuildState.m_vDot = num5 - 1;
											queue.Enqueue(earleyParseTreeBuildState);
										}
									}
									else
									{
										if (!this.m_vWikiMatrix[earleyParseTreeBuildState.m_vEnd].TryGetValue(-1, out flexibleSizeLongArray))
										{
											throw new ParserException(ParserExceptionType.Parser, ParserExceptionError.ParseTreeBuildError, "Parse tree build: completed states not found");
										}
										vArray = flexibleSizeLongArray.m_vArray;
										for (i = flexibleSizeLongArray.m_vContentSize - 1; i >= 0; i--)
										{
											num4 = vArray[i];
											if (num4 >= 0L)
											{
												num2 = (int)num4 >> 12;
												parserRuleTuple = this.m_vEarleyParserGrammar.m_vRules[num2];
												if (parserRuleTuple.m_vHead == num7)
												{
													num3 = (int)(num4 >> 32);
													if (num5 == 1)
													{
														if (num3 != earleyParseTreeBuildState.m_vStart)
														{
															goto IL_2C2;
														}
													}
													else if (this.m_vWikiMatrix[num3].TryGetValue(num7, out flexibleSizeLongArray) && Array.BinarySearch<long>(flexibleSizeLongArray.m_vArray, 0, flexibleSizeLongArray.m_vContentSize, value) < 0)
													{
														goto IL_2C2;
													}
													num = ((int)num4 & 4095);
													vArray[i] = -num4;
													break;
												}
											}
											IL_2C2:;
										}
										if (i == -1)
										{
											throw new ParserException(ParserExceptionType.Parser, ParserExceptionError.ParseTreeBuildError, "Parse tree build: no matching completed state found");
										}
										if (num9 == 1)
										{
											earleyParseTreeBuildState.m_vParentNode.AppendUsedRule(num2);
											earleyParseTreeBuildState.m_vParentNode.PayloadIn = num7;
											earleyParseTreeBuildState.m_vRuleIdx = num2;
											earleyParseTreeBuildState.m_vDot = num;
											earleyParseTreeBuildState.m_vRule = parserRuleTuple;
											queue.Enqueue(earleyParseTreeBuildState);
										}
										else if (num5 == 1)
										{
											ParseNode parseNode2 = new ParseNode(earleyParseTreeBuildState.m_vStart, earleyParseTreeBuildState.m_vEnd, num7, num7);
											parseNode2.AppendUsedRule(num2);
											earleyParseTreeBuildState.m_vParentNode.AddChild(parseNode2);
											earleyParseTreeBuildState.m_vRuleIdx = num2;
											earleyParseTreeBuildState.m_vRule = parserRuleTuple;
											earleyParseTreeBuildState.m_vDot = num;
											earleyParseTreeBuildState.m_vParentNode = parseNode2;
											queue.Enqueue(earleyParseTreeBuildState);
										}
										else
										{
											ParseNode parseNode2 = new ParseNode(num3, earleyParseTreeBuildState.m_vEnd, num7, num7);
											parseNode2.AppendUsedRule(num2);
											earleyParseTreeBuildState.m_vParentNode.AddChild(parseNode2);
											EarleyParseTreeBuildState item = new EarleyParseTreeBuildState(num3, earleyParseTreeBuildState.m_vEnd, num2, num, parserRuleTuple, parseNode2);
											earleyParseTreeBuildState.m_vEnd = num3;
											earleyParseTreeBuildState.m_vDot = num5 - 1;
											queue.Enqueue(earleyParseTreeBuildState);
											queue.Enqueue(item);
										}
									}
								}
								return parseNode;
							}
						}
						goto IL_3E7;
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
				return result;
			}

			// Token: 0x060018CE RID: 6350 RVA: 0x00105F94 File Offset: 0x00104194
			public ParseNode BuildParseTreeRecursive()
			{
				ParseNode result;
				try
				{
					ParseNode parseNode = null;
					int count = this.m_vLexerTokens.Count;
					if (count == 0)
					{
						result = new ParseNode(0, count, -1, -1);
					}
					else
					{
						FlexibleSizeLongArray flexibleSizeLongArray;
						if (!this.m_vWikiMatrix[count].TryGetValue(-1, out flexibleSizeLongArray) || flexibleSizeLongArray.m_vContentSize == 0)
						{
							throw new ParserException(ParserExceptionType.Parser, ParserExceptionError.ParseTreeBuildError, "Parse tree build: no suitable branches");
						}
						long[] vArray = flexibleSizeLongArray.m_vArray;
						int vContentSize = flexibleSizeLongArray.m_vContentSize;
						int[] vSymbolsFlags = this.m_vEarleyParserGrammar.m_vSymbolsFlags;
						for (int i = vContentSize - 1; i >= 0; i--)
						{
							long num = vArray[i];
							int dot = (int)num & 4095;
							int num2 = (int)num >> 12;
							ParserRuleTuple parserRuleTuple = this.m_vEarleyParserGrammar.m_vRules[num2];
							if ((vSymbolsFlags[parserRuleTuple.m_vHead] & 6) == 2)
							{
								parseNode = new ParseNode(0, count, parserRuleTuple.m_vHead, parserRuleTuple.m_vHead);
								parseNode.AppendUsedRule(num2);
								try
								{
									this.BuildParseTree(0, count, num2, dot, parserRuleTuple, parseNode);
								}
								catch (ParserException)
								{
								}
							}
						}
						result = parseNode;
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
				return result;
			}

			// Token: 0x060018CF RID: 6351 RVA: 0x001060B0 File Offset: 0x001042B0
			protected void BuildParseTree(int start, int end, int ruleIdx, int dot, ParserRuleTuple rt, ParseNode parent)
			{
				int num = rt.m_vRhs[dot - 1];
				int dot2 = 0;
				int num2 = 0;
				int num3 = 0;
				ParserRuleTuple parserRuleTuple = null;
				long value = ((long)start << 32) + ((long)ruleIdx << 12) + (long)dot - 1L;
				if (this.m_vEarleyParserGrammar.IsTerminal(num))
				{
					if (rt.m_vRhs.Length == 1)
					{
						parent.PayloadIn = num;
						return;
					}
					if (dot == 1)
					{
						parent.AddChild(new ParseNode(end - 1, end, num, num));
						return;
					}
					parent.AddChild(new ParseNode(end - 1, end, num, num));
					this.BuildParseTree(start, end - 1, ruleIdx, dot - 1, rt, parent);
					return;
				}
				else
				{
					FlexibleSizeLongArray flexibleSizeLongArray;
					if (!this.m_vWikiMatrix[end].TryGetValue(-1, out flexibleSizeLongArray))
					{
						throw new ParserException(ParserExceptionType.Parser, ParserExceptionError.ParseTreeBuildError, "Parse tree build: completed states not found");
					}
					long[] vArray = flexibleSizeLongArray.m_vArray;
					int i;
					for (i = flexibleSizeLongArray.m_vContentSize - 1; i >= 0; i--)
					{
						long num4 = vArray[i];
						if (num4 >= 0L)
						{
							num2 = (int)num4 >> 12;
							parserRuleTuple = this.m_vEarleyParserGrammar.m_vRules[num2];
							if (parserRuleTuple.m_vHead == num)
							{
								num3 = (int)(num4 >> 32);
								if (dot == 1)
								{
									if (num3 != start)
									{
										goto IL_139;
									}
								}
								else if (this.m_vWikiMatrix[num3].TryGetValue(num, out flexibleSizeLongArray) && Array.BinarySearch<long>(flexibleSizeLongArray.m_vArray, 0, flexibleSizeLongArray.m_vContentSize, value) < 0)
								{
									goto IL_139;
								}
								dot2 = ((int)num4 & 4095);
								vArray[i] = -num4;
								break;
							}
						}
						IL_139:;
					}
					if (i == -1)
					{
						throw new ParserException(ParserExceptionType.Parser, ParserExceptionError.ParseTreeBuildError, "Parse tree build: no matching completed state found");
					}
					if (rt.m_vRhs.Length == 1)
					{
						parent.AppendUsedRule(num2);
						parent.PayloadIn = num;
						this.BuildParseTree(start, end, num2, dot2, parserRuleTuple, parent);
						return;
					}
					ParseNode parseNode;
					if (dot == 1)
					{
						parseNode = new ParseNode(start, end, num, num);
						parseNode.AppendUsedRule(num2);
						parent.AddChild(parseNode);
						this.BuildParseTree(start, end, num2, dot2, parserRuleTuple, parseNode);
						return;
					}
					parseNode = new ParseNode(num3, end, num, num);
					parseNode.AppendUsedRule(num2);
					parent.AddChild(parseNode);
					this.BuildParseTree(num3, end, num2, dot2, parserRuleTuple, parseNode);
					this.BuildParseTree(start, num3, ruleIdx, dot - 1, rt, parent);
					return;
				}
			}

			// Token: 0x060018D0 RID: 6352 RVA: 0x001062C4 File Offset: 0x001044C4
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num = 50;
				for (int i = 0; i < this.m_vWikiMatrix.Length; i++)
				{
					Dictionary<int, FlexibleSizeLongArray>.KeyCollection keys = this.m_vWikiMatrix[i].Keys;
					int[] array = new int[keys.Count];
					keys.CopyTo(array, 0);
					Array.Sort<int>(array);
					foreach (int num2 in array)
					{
						long[] vArray = this.m_vWikiMatrix[i][num2].m_vArray;
						stringBuilder.Append('[');
						stringBuilder.Append(i);
						stringBuilder.Append(',');
						stringBuilder.Append(num2);
						stringBuilder.Append(')');
						int num3 = 0;
						while (num3 < vArray.Length && num3 < num + 1)
						{
							long num4 = vArray[num3];
							int num5 = (int)num4;
							int pos = (int)(num4 >> 32) & 65535;
							stringBuilder.Append(this.m_vEarleyParserGrammar.m_vRules[num5].ToString(pos));
							if (num3 == num)
							{
								stringBuilder.Append(" ... " + vArray.Length + " more states");
							}
							num3++;
						}
						stringBuilder.Append('\n');
					}
				}
				return stringBuilder.ToString();
			}

			// Token: 0x04001B43 RID: 6979
			protected EarleyParserGrammarDefinition m_vEarleyParserGrammar;

			// Token: 0x04001B44 RID: 6980
			protected Dictionary<int, FlexibleSizeLongArray>[] m_vWikiMatrix;

			// Token: 0x04001B45 RID: 6981
			protected Dictionary<int, FlexibleSizeLongArray> m_vCurrentMatrixCell;

			// Token: 0x04001B46 RID: 6982
			protected FlexibleSizeLongArray[] m_vPredictionMatrix;

			// Token: 0x04001B47 RID: 6983
			protected int m_vInputPosition;

			// Token: 0x04001B48 RID: 6984
			protected LexerToken m_vActivePositionLexerToken;

			// Token: 0x04001B49 RID: 6985
			protected LexerToken m_vActivePositionLookAheadLexerToken;

			// Token: 0x04001B4A RID: 6986
			protected int m_vActivePositionSymbolIndex = -1;

			// Token: 0x04001B4B RID: 6987
			protected int m_vActivePositionLookAheadSymbolIndex = -1;

			// Token: 0x04001B4C RID: 6988
			protected bool m_vActivePositionSymbolCouldBeIdentifier;

			// Token: 0x04001B4D RID: 6989
			protected bool m_vActivePositionLookAheadSymbolCouldBeIdentifier;

			// Token: 0x04001B4E RID: 6990
			protected HashSet<long> m_vStatesMarkedForCompletion = new HashSet<long>();

			// Token: 0x04001B4F RID: 6991
			protected Queue<long> m_vStatesToComplete = new Queue<long>(256);

			// Token: 0x04001B50 RID: 6992
			protected HashSet<long> m_vPredictionTuples = new HashSet<long>();

			// Token: 0x04001B51 RID: 6993
			protected int m_vIdentifierSymbolIndex;

			// Token: 0x04001B52 RID: 6994
			protected long m_vIdentifierSymbolIndexMask;

			// Token: 0x04001B53 RID: 6995
			protected long m_vIdentifierSymbolIndexMask1;

			// Token: 0x04001B54 RID: 6996
			protected HashSet<int> m_vPredictedSymbols = new HashSet<int>();
		}
	}
}
