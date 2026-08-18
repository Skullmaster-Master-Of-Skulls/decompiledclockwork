using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Antlr.Runtime
{
	// Token: 0x02000036 RID: 54
	[DebuggerDisplay("TODO: TokenRewriteStream debugger display")]
	[Serializable]
	public class TokenRewriteStream : CommonTokenStream
	{
		// Token: 0x06000258 RID: 600 RVA: 0x00006E9B File Offset: 0x0000509B
		public TokenRewriteStream()
		{
			this.Init();
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00006EA9 File Offset: 0x000050A9
		protected void Init()
		{
			this.programs = new Dictionary<string, IList<TokenRewriteStream.RewriteOperation>>();
			this.programs["default"] = new List<TokenRewriteStream.RewriteOperation>(100);
			this.lastRewriteTokenIndexes = new Dictionary<string, int>();
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00006ED8 File Offset: 0x000050D8
		public TokenRewriteStream(ITokenSource tokenSource) : base(tokenSource)
		{
			this.Init();
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00006EE7 File Offset: 0x000050E7
		public TokenRewriteStream(ITokenSource tokenSource, int channel) : base(tokenSource, channel)
		{
			this.Init();
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00006EF7 File Offset: 0x000050F7
		public virtual void Rollback(int instructionIndex)
		{
			this.Rollback("default", instructionIndex);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00006F08 File Offset: 0x00005108
		public virtual void Rollback(string programName, int instructionIndex)
		{
			IList<TokenRewriteStream.RewriteOperation> list;
			if (this.programs.TryGetValue(programName, out list) && list != null)
			{
				List<TokenRewriteStream.RewriteOperation> list2 = new List<TokenRewriteStream.RewriteOperation>();
				for (int i = 0; i <= instructionIndex; i++)
				{
					list2.Add(list[i]);
				}
				this.programs[programName] = list2;
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00006F54 File Offset: 0x00005154
		public virtual void DeleteProgram()
		{
			this.DeleteProgram("default");
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00006F61 File Offset: 0x00005161
		public virtual void DeleteProgram(string programName)
		{
			this.Rollback(programName, 0);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00006F6B File Offset: 0x0000516B
		public virtual void InsertAfter(IToken t, object text)
		{
			this.InsertAfter("default", t, text);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00006F7A File Offset: 0x0000517A
		public virtual void InsertAfter(int index, object text)
		{
			this.InsertAfter("default", index, text);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00006F89 File Offset: 0x00005189
		public virtual void InsertAfter(string programName, IToken t, object text)
		{
			this.InsertAfter(programName, t.TokenIndex, text);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00006F99 File Offset: 0x00005199
		public virtual void InsertAfter(string programName, int index, object text)
		{
			this.InsertBefore(programName, index + 1, text);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00006FA6 File Offset: 0x000051A6
		public virtual void InsertBefore(IToken t, object text)
		{
			this.InsertBefore("default", t, text);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00006FB5 File Offset: 0x000051B5
		public virtual void InsertBefore(int index, object text)
		{
			this.InsertBefore("default", index, text);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00006FC4 File Offset: 0x000051C4
		public virtual void InsertBefore(string programName, IToken t, object text)
		{
			this.InsertBefore(programName, t.TokenIndex, text);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00006FD4 File Offset: 0x000051D4
		public virtual void InsertBefore(string programName, int index, object text)
		{
			TokenRewriteStream.RewriteOperation rewriteOperation = new TokenRewriteStream.InsertBeforeOp(this, index, text);
			IList<TokenRewriteStream.RewriteOperation> program = this.GetProgram(programName);
			rewriteOperation.instructionIndex = program.Count;
			program.Add(rewriteOperation);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00007005 File Offset: 0x00005205
		public virtual void Replace(int index, object text)
		{
			this.Replace("default", index, index, text);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00007015 File Offset: 0x00005215
		public virtual void Replace(int from, int to, object text)
		{
			this.Replace("default", from, to, text);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00007025 File Offset: 0x00005225
		public virtual void Replace(IToken indexT, object text)
		{
			this.Replace("default", indexT, indexT, text);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00007035 File Offset: 0x00005235
		public virtual void Replace(IToken from, IToken to, object text)
		{
			this.Replace("default", from, to, text);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00007048 File Offset: 0x00005248
		public virtual void Replace(string programName, int from, int to, object text)
		{
			if (from > to || from < 0 || to < 0 || to >= this._tokens.Count)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"replace: range invalid: ",
					from,
					"..",
					to,
					"(size=",
					this._tokens.Count,
					")"
				}));
			}
			TokenRewriteStream.RewriteOperation rewriteOperation = new TokenRewriteStream.ReplaceOp(this, from, to, text);
			IList<TokenRewriteStream.RewriteOperation> program = this.GetProgram(programName);
			rewriteOperation.instructionIndex = program.Count;
			program.Add(rewriteOperation);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000070ED File Offset: 0x000052ED
		public virtual void Replace(string programName, IToken from, IToken to, object text)
		{
			this.Replace(programName, from.TokenIndex, to.TokenIndex, text);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00007104 File Offset: 0x00005304
		public virtual void Delete(int index)
		{
			this.Delete("default", index, index);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00007113 File Offset: 0x00005313
		public virtual void Delete(int from, int to)
		{
			this.Delete("default", from, to);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00007122 File Offset: 0x00005322
		public virtual void Delete(IToken indexT)
		{
			this.Delete("default", indexT, indexT);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00007131 File Offset: 0x00005331
		public virtual void Delete(IToken from, IToken to)
		{
			this.Delete("default", from, to);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00007140 File Offset: 0x00005340
		public virtual void Delete(string programName, int from, int to)
		{
			this.Replace(programName, from, to, null);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000714C File Offset: 0x0000534C
		public virtual void Delete(string programName, IToken from, IToken to)
		{
			this.Replace(programName, from, to, null);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00007158 File Offset: 0x00005358
		public virtual int GetLastRewriteTokenIndex()
		{
			return this.GetLastRewriteTokenIndex("default");
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00007168 File Offset: 0x00005368
		protected virtual int GetLastRewriteTokenIndex(string programName)
		{
			int result;
			if (this.lastRewriteTokenIndexes.TryGetValue(programName, out result))
			{
				return result;
			}
			return -1;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00007188 File Offset: 0x00005388
		protected virtual void SetLastRewriteTokenIndex(string programName, int i)
		{
			this.lastRewriteTokenIndexes[programName] = i;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00007198 File Offset: 0x00005398
		protected virtual IList<TokenRewriteStream.RewriteOperation> GetProgram(string name)
		{
			IList<TokenRewriteStream.RewriteOperation> list;
			if (!this.programs.TryGetValue(name, out list) || list == null)
			{
				list = this.InitializeProgram(name);
			}
			return list;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000071C4 File Offset: 0x000053C4
		private IList<TokenRewriteStream.RewriteOperation> InitializeProgram(string name)
		{
			IList<TokenRewriteStream.RewriteOperation> list = new List<TokenRewriteStream.RewriteOperation>(100);
			this.programs[name] = list;
			return list;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000071E7 File Offset: 0x000053E7
		public virtual string ToOriginalString()
		{
			this.Fill();
			return this.ToOriginalString(0, this.Count - 1);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00007200 File Offset: 0x00005400
		public virtual string ToOriginalString(int start, int end)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = start;
			while (num >= 0 && num <= end && num < this._tokens.Count)
			{
				if (this.Get(num).Type != -1)
				{
					stringBuilder.Append(this.Get(num).Text);
				}
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00007259 File Offset: 0x00005459
		public override string ToString()
		{
			this.Fill();
			return this.ToString(0, this.Count - 1);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00007270 File Offset: 0x00005470
		public virtual string ToString(string programName)
		{
			this.Fill();
			return this.ToString(programName, 0, this.Count - 1);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00007288 File Offset: 0x00005488
		public override string ToString(int start, int end)
		{
			return this.ToString("default", start, end);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00007298 File Offset: 0x00005498
		public virtual string ToString(string programName, int start, int end)
		{
			IList<TokenRewriteStream.RewriteOperation> list;
			if (!this.programs.TryGetValue(programName, out list))
			{
				list = null;
			}
			if (end > this._tokens.Count - 1)
			{
				end = this._tokens.Count - 1;
			}
			if (start < 0)
			{
				start = 0;
			}
			if (list == null || list.Count == 0)
			{
				return this.ToOriginalString(start, end);
			}
			StringBuilder stringBuilder = new StringBuilder();
			IDictionary<int, TokenRewriteStream.RewriteOperation> dictionary = this.ReduceToSingleOperationPerIndex(list);
			int num = start;
			while (num <= end && num < this._tokens.Count)
			{
				TokenRewriteStream.RewriteOperation rewriteOperation;
				bool flag = dictionary.TryGetValue(num, out rewriteOperation);
				if (flag)
				{
					dictionary.Remove(num);
				}
				if (!flag || rewriteOperation == null)
				{
					IToken token = this._tokens[num];
					if (token.Type != -1)
					{
						stringBuilder.Append(token.Text);
					}
					num++;
				}
				else
				{
					num = rewriteOperation.Execute(stringBuilder);
				}
			}
			if (end == this._tokens.Count - 1)
			{
				foreach (TokenRewriteStream.RewriteOperation rewriteOperation2 in dictionary.Values)
				{
					if (rewriteOperation2.index >= this._tokens.Count - 1)
					{
						stringBuilder.Append(rewriteOperation2.text);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000073E4 File Offset: 0x000055E4
		protected virtual IDictionary<int, TokenRewriteStream.RewriteOperation> ReduceToSingleOperationPerIndex(IList<TokenRewriteStream.RewriteOperation> rewrites)
		{
			for (int i = 0; i < rewrites.Count; i++)
			{
				TokenRewriteStream.RewriteOperation rewriteOperation = rewrites[i];
				if (rewriteOperation != null && rewriteOperation is TokenRewriteStream.ReplaceOp)
				{
					TokenRewriteStream.ReplaceOp replaceOp = (TokenRewriteStream.ReplaceOp)rewrites[i];
					IList<TokenRewriteStream.RewriteOperation> kindOfOps = this.GetKindOfOps(rewrites, typeof(TokenRewriteStream.InsertBeforeOp), i);
					for (int j = 0; j < kindOfOps.Count; j++)
					{
						TokenRewriteStream.InsertBeforeOp insertBeforeOp = (TokenRewriteStream.InsertBeforeOp)kindOfOps[j];
						if (insertBeforeOp.index == replaceOp.index)
						{
							rewrites[insertBeforeOp.instructionIndex] = null;
							replaceOp.text = insertBeforeOp.text.ToString() + ((replaceOp.text != null) ? replaceOp.text.ToString() : string.Empty);
						}
						else if (insertBeforeOp.index > replaceOp.index && insertBeforeOp.index <= replaceOp.lastIndex)
						{
							rewrites[insertBeforeOp.instructionIndex] = null;
						}
					}
					IList<TokenRewriteStream.RewriteOperation> kindOfOps2 = this.GetKindOfOps(rewrites, typeof(TokenRewriteStream.ReplaceOp), i);
					for (int k = 0; k < kindOfOps2.Count; k++)
					{
						TokenRewriteStream.ReplaceOp replaceOp2 = (TokenRewriteStream.ReplaceOp)kindOfOps2[k];
						if (replaceOp2.index >= replaceOp.index && replaceOp2.lastIndex <= replaceOp.lastIndex)
						{
							rewrites[replaceOp2.instructionIndex] = null;
						}
						else
						{
							bool flag = replaceOp2.lastIndex < replaceOp.index || replaceOp2.index > replaceOp.lastIndex;
							bool flag2 = replaceOp2.index == replaceOp.index && replaceOp2.lastIndex == replaceOp.lastIndex;
							if (replaceOp2.text == null && replaceOp.text == null && !flag)
							{
								rewrites[replaceOp2.instructionIndex] = null;
								replaceOp.index = Math.Min(replaceOp2.index, replaceOp.index);
								replaceOp.lastIndex = Math.Max(replaceOp2.lastIndex, replaceOp.lastIndex);
								Console.WriteLine("new rop " + replaceOp);
							}
							else if (!flag && !flag2)
							{
								throw new ArgumentException(string.Concat(new object[]
								{
									"replace op boundaries of ",
									replaceOp,
									" overlap with previous ",
									replaceOp2
								}));
							}
						}
					}
				}
			}
			for (int l = 0; l < rewrites.Count; l++)
			{
				TokenRewriteStream.RewriteOperation rewriteOperation2 = rewrites[l];
				if (rewriteOperation2 != null && rewriteOperation2 is TokenRewriteStream.InsertBeforeOp)
				{
					TokenRewriteStream.InsertBeforeOp insertBeforeOp2 = (TokenRewriteStream.InsertBeforeOp)rewrites[l];
					IList<TokenRewriteStream.RewriteOperation> kindOfOps3 = this.GetKindOfOps(rewrites, typeof(TokenRewriteStream.InsertBeforeOp), l);
					for (int m = 0; m < kindOfOps3.Count; m++)
					{
						TokenRewriteStream.InsertBeforeOp insertBeforeOp3 = (TokenRewriteStream.InsertBeforeOp)kindOfOps3[m];
						if (insertBeforeOp3.index == insertBeforeOp2.index)
						{
							insertBeforeOp2.text = this.CatOpText(insertBeforeOp2.text, insertBeforeOp3.text);
							rewrites[insertBeforeOp3.instructionIndex] = null;
						}
					}
					IList<TokenRewriteStream.RewriteOperation> kindOfOps4 = this.GetKindOfOps(rewrites, typeof(TokenRewriteStream.ReplaceOp), l);
					for (int n = 0; n < kindOfOps4.Count; n++)
					{
						TokenRewriteStream.ReplaceOp replaceOp3 = (TokenRewriteStream.ReplaceOp)kindOfOps4[n];
						if (insertBeforeOp2.index == replaceOp3.index)
						{
							replaceOp3.text = this.CatOpText(insertBeforeOp2.text, replaceOp3.text);
							rewrites[l] = null;
						}
						else if (insertBeforeOp2.index >= replaceOp3.index && insertBeforeOp2.index <= replaceOp3.lastIndex)
						{
							throw new ArgumentException(string.Concat(new object[]
							{
								"insert op ",
								insertBeforeOp2,
								" within boundaries of previous ",
								replaceOp3
							}));
						}
					}
				}
			}
			IDictionary<int, TokenRewriteStream.RewriteOperation> dictionary = new Dictionary<int, TokenRewriteStream.RewriteOperation>();
			for (int num = 0; num < rewrites.Count; num++)
			{
				TokenRewriteStream.RewriteOperation rewriteOperation3 = rewrites[num];
				if (rewriteOperation3 != null)
				{
					TokenRewriteStream.RewriteOperation rewriteOperation4;
					if (dictionary.TryGetValue(rewriteOperation3.index, out rewriteOperation4) && rewriteOperation4 != null)
					{
						throw new Exception("should only be one op per index");
					}
					dictionary[rewriteOperation3.index] = rewriteOperation3;
				}
			}
			return dictionary;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000781F File Offset: 0x00005A1F
		protected virtual string CatOpText(object a, object b)
		{
			return a + b;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00007828 File Offset: 0x00005A28
		protected virtual IList<TokenRewriteStream.RewriteOperation> GetKindOfOps(IList<TokenRewriteStream.RewriteOperation> rewrites, Type kind)
		{
			return this.GetKindOfOps(rewrites, kind, rewrites.Count);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00007838 File Offset: 0x00005A38
		protected virtual IList<TokenRewriteStream.RewriteOperation> GetKindOfOps(IList<TokenRewriteStream.RewriteOperation> rewrites, Type kind, int before)
		{
			IList<TokenRewriteStream.RewriteOperation> list = new List<TokenRewriteStream.RewriteOperation>();
			int num = 0;
			while (num < before && num < rewrites.Count)
			{
				TokenRewriteStream.RewriteOperation rewriteOperation = rewrites[num];
				if (rewriteOperation != null && rewriteOperation.GetType() == kind)
				{
					list.Add(rewriteOperation);
				}
				num++;
			}
			return list;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000787C File Offset: 0x00005A7C
		public virtual string ToDebugString()
		{
			return this.ToDebugString(0, this.Count - 1);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00007890 File Offset: 0x00005A90
		public virtual string ToDebugString(int start, int end)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = start;
			while (num >= 0 && num <= end && num < this._tokens.Count)
			{
				stringBuilder.Append(this.Get(num));
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400007C RID: 124
		public const string DEFAULT_PROGRAM_NAME = "default";

		// Token: 0x0400007D RID: 125
		public const int PROGRAM_INIT_SIZE = 100;

		// Token: 0x0400007E RID: 126
		public const int MIN_TOKEN_INDEX = 0;

		// Token: 0x0400007F RID: 127
		protected IDictionary<string, IList<TokenRewriteStream.RewriteOperation>> programs;

		// Token: 0x04000080 RID: 128
		protected IDictionary<string, int> lastRewriteTokenIndexes;

		// Token: 0x02000037 RID: 55
		protected class RewriteOperation
		{
			// Token: 0x06000285 RID: 645 RVA: 0x000078D5 File Offset: 0x00005AD5
			protected RewriteOperation(TokenRewriteStream stream, int index)
			{
				this.stream = stream;
				this.index = index;
			}

			// Token: 0x06000286 RID: 646 RVA: 0x000078EB File Offset: 0x00005AEB
			protected RewriteOperation(TokenRewriteStream stream, int index, object text)
			{
				this.index = index;
				this.text = text;
				this.stream = stream;
			}

			// Token: 0x06000287 RID: 647 RVA: 0x00007908 File Offset: 0x00005B08
			public virtual int Execute(StringBuilder buf)
			{
				return this.index;
			}

			// Token: 0x06000288 RID: 648 RVA: 0x00007910 File Offset: 0x00005B10
			public override string ToString()
			{
				string text = base.GetType().Name;
				int num = text.IndexOf('$');
				text = text.Substring(num + 1);
				return string.Format("<{0}@{1}:\"{2}\">", text, this.stream._tokens[this.index], this.text);
			}

			// Token: 0x04000081 RID: 129
			public int instructionIndex;

			// Token: 0x04000082 RID: 130
			public int index;

			// Token: 0x04000083 RID: 131
			public object text;

			// Token: 0x04000084 RID: 132
			protected TokenRewriteStream stream;
		}

		// Token: 0x02000038 RID: 56
		private class InsertBeforeOp : TokenRewriteStream.RewriteOperation
		{
			// Token: 0x06000289 RID: 649 RVA: 0x00007963 File Offset: 0x00005B63
			public InsertBeforeOp(TokenRewriteStream stream, int index, object text) : base(stream, index, text)
			{
			}

			// Token: 0x0600028A RID: 650 RVA: 0x00007970 File Offset: 0x00005B70
			public override int Execute(StringBuilder buf)
			{
				buf.Append(this.text);
				if (this.stream._tokens[this.index].Type != -1)
				{
					buf.Append(this.stream._tokens[this.index].Text);
				}
				return this.index + 1;
			}
		}

		// Token: 0x02000039 RID: 57
		private class ReplaceOp : TokenRewriteStream.RewriteOperation
		{
			// Token: 0x0600028B RID: 651 RVA: 0x000079D2 File Offset: 0x00005BD2
			public ReplaceOp(TokenRewriteStream stream, int from, int to, object text) : base(stream, from, text)
			{
				this.lastIndex = to;
			}

			// Token: 0x0600028C RID: 652 RVA: 0x000079E5 File Offset: 0x00005BE5
			public override int Execute(StringBuilder buf)
			{
				if (this.text != null)
				{
					buf.Append(this.text);
				}
				return this.lastIndex + 1;
			}

			// Token: 0x0600028D RID: 653 RVA: 0x00007A04 File Offset: 0x00005C04
			public override string ToString()
			{
				if (this.text == null)
				{
					return string.Format("<DeleteOp@{0}..{1}>", this.stream._tokens[this.index], this.stream._tokens[this.lastIndex]);
				}
				return string.Format("<ReplaceOp@{0}..{1}:\"{2}\">", this.stream._tokens[this.index], this.stream._tokens[this.lastIndex], this.text);
			}

			// Token: 0x04000085 RID: 133
			public int lastIndex;
		}
	}
}
