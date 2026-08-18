using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x02000277 RID: 631
	internal class OracleSqlEarley : Earley
	{
		// Token: 0x060018E7 RID: 6375 RVA: 0x00106F78 File Offset: 0x00105178
		public OracleSqlEarley(OracleSqlEarleyParserGrammarDefinition grammar) : base(grammar)
		{
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x00106F84 File Offset: 0x00105184
		public string TestParse(string s)
		{
			List<LexerToken> src;
			using (new PerformanceTimer("Lexing"))
			{
				src = LexerToken.Parse(s);
			}
			string result;
			try
			{
				using (new PerformanceTimer("Parsing"))
				{
					ParseNode parseNode = this.Parse(s, src);
					if (parseNode != null)
					{
						result = parseNode.ToString(base.EarleyGrammar);
					}
					else
					{
						result = "Parse failed";
					}
				}
			}
			catch (ParserException ex)
			{
				result = ex.ToString();
			}
			return result;
		}
	}
}
