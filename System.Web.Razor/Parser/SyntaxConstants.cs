using System;

namespace System.Web.Razor.Parser
{
	// Token: 0x02000045 RID: 69
	public static class SyntaxConstants
	{
		// Token: 0x040000BD RID: 189
		public static readonly string TextTagName = "text";

		// Token: 0x040000BE RID: 190
		public static readonly char TransitionCharacter = '@';

		// Token: 0x040000BF RID: 191
		public static readonly string TransitionString = "@";

		// Token: 0x040000C0 RID: 192
		public static readonly string StartCommentSequence = "@*";

		// Token: 0x040000C1 RID: 193
		public static readonly string EndCommentSequence = "*@";

		// Token: 0x02000046 RID: 70
		public static class CSharp
		{
			// Token: 0x040000C2 RID: 194
			public static readonly int UsingKeywordLength = 5;

			// Token: 0x040000C3 RID: 195
			public static readonly string InheritsKeyword = "inherits";

			// Token: 0x040000C4 RID: 196
			public static readonly string FunctionsKeyword = "functions";

			// Token: 0x040000C5 RID: 197
			public static readonly string SectionKeyword = "section";

			// Token: 0x040000C6 RID: 198
			public static readonly string HelperKeyword = "helper";

			// Token: 0x040000C7 RID: 199
			public static readonly string ElseIfKeyword = "else if";

			// Token: 0x040000C8 RID: 200
			public static readonly string NamespaceKeyword = "namespace";

			// Token: 0x040000C9 RID: 201
			public static readonly string ClassKeyword = "class";

			// Token: 0x040000CA RID: 202
			public static readonly string LayoutKeyword = "layout";

			// Token: 0x040000CB RID: 203
			public static readonly string SessionStateKeyword = "sessionstate";
		}

		// Token: 0x02000047 RID: 71
		public static class VB
		{
			// Token: 0x040000CC RID: 204
			public static readonly int ImportsKeywordLength = 7;

			// Token: 0x040000CD RID: 205
			public static readonly string EndKeyword = "End";

			// Token: 0x040000CE RID: 206
			public static readonly string CodeKeyword = "Code";

			// Token: 0x040000CF RID: 207
			public static readonly string FunctionsKeyword = "Functions";

			// Token: 0x040000D0 RID: 208
			public static readonly string SectionKeyword = "Section";

			// Token: 0x040000D1 RID: 209
			public static readonly string StrictKeyword = "Strict";

			// Token: 0x040000D2 RID: 210
			public static readonly string ExplicitKeyword = "Explicit";

			// Token: 0x040000D3 RID: 211
			public static readonly string OffKeyword = "Off";

			// Token: 0x040000D4 RID: 212
			public static readonly string HelperKeyword = "Helper";

			// Token: 0x040000D5 RID: 213
			public static readonly string SelectCaseKeyword = "Select Case";

			// Token: 0x040000D6 RID: 214
			public static readonly string LayoutKeyword = "Layout";

			// Token: 0x040000D7 RID: 215
			public static readonly string EndCodeKeyword = "End Code";

			// Token: 0x040000D8 RID: 216
			public static readonly string EndHelperKeyword = "End Helper";

			// Token: 0x040000D9 RID: 217
			public static readonly string EndFunctionsKeyword = "End Functions";

			// Token: 0x040000DA RID: 218
			public static readonly string EndSectionKeyword = "End Section";

			// Token: 0x040000DB RID: 219
			public static readonly string SessionStateKeyword = "SessionState";
		}
	}
}
