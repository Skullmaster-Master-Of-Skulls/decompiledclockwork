using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002B1 RID: 689
	internal static class OracleLpErrorStrings
	{
		// Token: 0x060019C1 RID: 6593 RVA: 0x001098F0 File Offset: 0x00107AF0
		static OracleLpErrorStrings()
		{
			OracleLpErrorStrings.s_vErrorStringDictionary.Add(OracleLpExceptionError.InvalidRuleHeadSymbol, "Invalid rule head symbol");
			OracleLpErrorStrings.s_vErrorStringDictionary.Add(OracleLpExceptionError.InvalidRuleRHSSymbols, "Invalid rule right hand symbols");
			OracleLpErrorStrings.s_vErrorStringDictionary.Add(OracleLpExceptionError.MissingParserSymbol, "None of the parser rules contains the {0} symbol");
			OracleLpErrorStrings.s_vErrorStringDictionary.Add(OracleLpExceptionError.MissingRule, "The parser rules doesn't contain the {0} rule");
			OracleLpErrorStrings.s_vErrorStringDictionary.Add(OracleLpExceptionError.MissingTable_View_Query, "The table, view or query reference named [{0}.{1}] is missing from the SELECT statement.");
			OracleLpErrorStrings.s_vErrorStringDictionary.Add(OracleLpExceptionError.MissingColumnFromReference, "The column [{0}] is missing from the [{1}] object reference.");
			OracleLpErrorStrings.s_vErrorStringDictionary.Add(OracleLpExceptionError.MissingColumnFromAllReferences, "The column [{0}] is missing from all corresponding object references.");
			OracleLpErrorStrings.s_vErrorStringDictionary.Add(OracleLpExceptionError.AmbiguousColumn, "The column [{0}] is ambiguously defined in corresponding object references.");
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x00109988 File Offset: 0x00107B88
		public static string GetErrorString(OracleLpExceptionError item)
		{
			return OracleLpErrorStrings.s_vErrorStringDictionary[item];
		}

		// Token: 0x04001C3B RID: 7227
		private const string c_vInvalidRuleHeadSymbol = "Invalid rule head symbol";

		// Token: 0x04001C3C RID: 7228
		private const string c_vInvalidRuleRHSSymbols = "Invalid rule right hand symbols";

		// Token: 0x04001C3D RID: 7229
		private const string c_vMissingParserSymbol = "None of the parser rules contains the {0} symbol";

		// Token: 0x04001C3E RID: 7230
		private const string c_vMissingRule = "The parser rules doesn't contain the {0} rule";

		// Token: 0x04001C3F RID: 7231
		private const string c_vMissingTable_View_Query = "The table, view or query reference named [{0}.{1}] is missing from the SELECT statement.";

		// Token: 0x04001C40 RID: 7232
		private const string c_vMissingColumnFromReference = "The column [{0}] is missing from the [{1}] object reference.";

		// Token: 0x04001C41 RID: 7233
		private const string c_vMissingColumnFromAllReferences = "The column [{0}] is missing from all corresponding object references.";

		// Token: 0x04001C42 RID: 7234
		private const string c_vAmbiguousColumn = "The column [{0}] is ambiguously defined in corresponding object references.";

		// Token: 0x04001C43 RID: 7235
		private static Dictionary<OracleLpExceptionError, string> s_vErrorStringDictionary = new Dictionary<OracleLpExceptionError, string>();
	}
}
