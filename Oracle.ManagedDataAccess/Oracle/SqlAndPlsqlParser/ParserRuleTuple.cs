using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x0200027C RID: 636
	internal class ParserRuleTuple : RuleTupleBase<int>
	{
		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x00107ACC File Offset: 0x00105CCC
		public ParserGrammarDefinition ParserGrammar
		{
			get
			{
				return this.m_vParserGrammar;
			}
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x00107AD4 File Offset: 0x00105CD4
		static ParserRuleTuple()
		{
			RuleTupleBase<int>.s_vNullHeadHashCode = 0.GetHashCode();
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x00107AF0 File Offset: 0x00105CF0
		public ParserRuleTuple(int h, List<int> r, ParserGrammarDefinition parserGrammar) : base(h, r)
		{
			this.m_vParserGrammar = parserGrammar;
			this.GetBaseSymbols();
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x00107B10 File Offset: 0x00105D10
		public ParserRuleTuple(int h, int[] r, ParserGrammarDefinition parserGrammar) : base(h, r)
		{
			this.m_vParserGrammar = parserGrammar;
			this.GetBaseSymbols();
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00107B30 File Offset: 0x00105D30
		protected void GetBaseSymbols()
		{
			string text = this.m_vParserGrammar.m_vAllSymbols[this.m_vHead];
			int num = text.IndexOf('[');
			if (num >= 0)
			{
				text = text.Substring(0, num);
				this.m_vBaseHead = this.m_vParserGrammar.m_vSymbolIndexes[text];
			}
			else
			{
				this.m_vBaseHead = this.m_vHead;
			}
			if (this.m_vRhs == null)
			{
				return;
			}
			int num2 = this.m_vRhs.Length;
			this.m_vBaseRhs = new int[num2];
			for (int i = 0; i < num2; i++)
			{
				int num3 = this.m_vRhs[i];
				if (num3 < 0)
				{
					this.m_vBaseRhs[i] = num3;
				}
				else
				{
					text = this.m_vParserGrammar.m_vAllSymbols[num3];
					num = text.IndexOf('[');
					if (num >= 0)
					{
						text = text.Substring(0, num);
						this.m_vBaseRhs[i] = this.m_vParserGrammar.m_vSymbolIndexes[text];
					}
					else
					{
						this.m_vBaseRhs[i] = num3;
					}
				}
			}
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x00107C20 File Offset: 0x00105E20
		public override int CompareTo(RuleTupleBase<int> src)
		{
			ParserRuleTuple parserRuleTuple = src as ParserRuleTuple;
			if (parserRuleTuple == null)
			{
				throw new ParserException(ParserExceptionType.Grammar, ParserExceptionError.MismatchedTuplesComparison);
			}
			if (this.m_vParserGrammar != parserRuleTuple.m_vParserGrammar)
			{
				throw new ParserException(ParserExceptionType.Grammar, ParserExceptionError.DifferentGrammarsTuples);
			}
			int num = this.m_vHead - parserRuleTuple.m_vHead;
			if (num == 0)
			{
				int[] vRhs = parserRuleTuple.m_vRhs;
				if (this.m_vRhs == null)
				{
					if (vRhs != null)
					{
						num = -1;
					}
				}
				else if (vRhs == null)
				{
					num = 1;
				}
				else
				{
					int num2 = this.m_vRhs.Length;
					num = num2 - vRhs.Length;
					int num3 = 0;
					while (num == 0 && num3 < num2)
					{
						num = this.m_vRhs[num3] - vRhs[num3];
						num3++;
					}
				}
			}
			return num;
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x00107CB8 File Offset: 0x00105EB8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			if (this.m_vHead >= 0)
			{
				string value = this.m_vParserGrammar.m_vAllSymbols[this.m_vHead];
				stringBuilder.Append(value);
				stringBuilder.Append(':');
			}
			foreach (int num in this.m_vRhs)
			{
				stringBuilder.Append(' ');
				if (num >= 0)
				{
					string value = this.m_vParserGrammar.m_vAllSymbols[num];
					stringBuilder.Append(value);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00107D44 File Offset: 0x00105F44
		public virtual string ToString(int pos)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			if (this.m_vHead >= 0)
			{
				string value = this.m_vParserGrammar.m_vAllSymbols[this.m_vHead];
				stringBuilder.Append(value);
				stringBuilder.Append(':');
			}
			int num = this.m_vRhs.Length;
			for (int i = 0; i < num; i++)
			{
				stringBuilder.Append(' ');
				int num2 = this.m_vRhs[i];
				if (num2 >= 0)
				{
					if (i == pos)
					{
						stringBuilder.Append('!');
					}
					string value = this.m_vParserGrammar.m_vAllSymbols[num2];
					stringBuilder.Append(value);
				}
			}
			if (num == pos)
			{
				stringBuilder.Append('!');
			}
			stringBuilder.Append(';');
			return stringBuilder.ToString();
		}

		// Token: 0x04001B71 RID: 7025
		public int m_vBaseHead = -1;

		// Token: 0x04001B72 RID: 7026
		public int[] m_vBaseRhs;

		// Token: 0x04001B73 RID: 7027
		protected ParserGrammarDefinition m_vParserGrammar;
	}
}
