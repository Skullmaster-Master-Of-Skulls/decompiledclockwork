using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace System.Web.Razor.Resources
{
	// Token: 0x02000094 RID: 148
	[DebuggerNonUserCode]
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	internal class RazorResources
	{
		// Token: 0x0600062C RID: 1580 RVA: 0x0001787B File Offset: 0x00015A7B
		internal RazorResources()
		{
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x00017884 File Offset: 0x00015A84
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(RazorResources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("System.Web.Razor.Resources.RazorResources", typeof(RazorResources).Assembly);
					RazorResources.resourceMan = resourceManager;
				}
				return RazorResources.resourceMan;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x000178C3 File Offset: 0x00015AC3
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x000178CA File Offset: 0x00015ACA
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return RazorResources.resourceCulture;
			}
			set
			{
				RazorResources.resourceCulture = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x000178D2 File Offset: 0x00015AD2
		internal static string ActiveParser_Must_Be_Code_Or_Markup_Parser
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ActiveParser_Must_Be_Code_Or_Markup_Parser", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x000178E8 File Offset: 0x00015AE8
		internal static string Block_Type_Not_Specified
		{
			get
			{
				return RazorResources.ResourceManager.GetString("Block_Type_Not_Specified", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x000178FE File Offset: 0x00015AFE
		internal static string BlockName_Code
		{
			get
			{
				return RazorResources.ResourceManager.GetString("BlockName_Code", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00017914 File Offset: 0x00015B14
		internal static string BlockName_ExplicitExpression
		{
			get
			{
				return RazorResources.ResourceManager.GetString("BlockName_ExplicitExpression", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001792A File Offset: 0x00015B2A
		internal static string CancelBacktrack_Must_Be_Called_Within_Lookahead
		{
			get
			{
				return RazorResources.ResourceManager.GetString("CancelBacktrack_Must_Be_Called_Within_Lookahead", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00017940 File Offset: 0x00015B40
		internal static string CreateCodeWriter_NoCodeWriter
		{
			get
			{
				return RazorResources.ResourceManager.GetString("CreateCodeWriter_NoCodeWriter", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00017956 File Offset: 0x00015B56
		internal static string CSharpSymbol_CharacterLiteral
		{
			get
			{
				return RazorResources.ResourceManager.GetString("CSharpSymbol_CharacterLiteral", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0001796C File Offset: 0x00015B6C
		internal static string CSharpSymbol_Comment
		{
			get
			{
				return RazorResources.ResourceManager.GetString("CSharpSymbol_Comment", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00017982 File Offset: 0x00015B82
		internal static string CSharpSymbol_Identifier
		{
			get
			{
				return RazorResources.ResourceManager.GetString("CSharpSymbol_Identifier", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00017998 File Offset: 0x00015B98
		internal static string CSharpSymbol_IntegerLiteral
		{
			get
			{
				return RazorResources.ResourceManager.GetString("CSharpSymbol_IntegerLiteral", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x000179AE File Offset: 0x00015BAE
		internal static string CSharpSymbol_Keyword
		{
			get
			{
				return RazorResources.ResourceManager.GetString("CSharpSymbol_Keyword", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x000179C4 File Offset: 0x00015BC4
		internal static string CSharpSymbol_Newline
		{
			get
			{
				return RazorResources.ResourceManager.GetString("CSharpSymbol_Newline", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x000179DA File Offset: 0x00015BDA
		internal static string CSharpSymbol_RealLiteral
		{
			get
			{
				return RazorResources.ResourceManager.GetString("CSharpSymbol_RealLiteral", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x000179F0 File Offset: 0x00015BF0
		internal static string CSharpSymbol_StringLiteral
		{
			get
			{
				return RazorResources.ResourceManager.GetString("CSharpSymbol_StringLiteral", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00017A06 File Offset: 0x00015C06
		internal static string CSharpSymbol_Whitespace
		{
			get
			{
				return RazorResources.ResourceManager.GetString("CSharpSymbol_Whitespace", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x00017A1C File Offset: 0x00015C1C
		internal static string EndBlock_Called_Without_Matching_StartBlock
		{
			get
			{
				return RazorResources.ResourceManager.GetString("EndBlock_Called_Without_Matching_StartBlock", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00017A32 File Offset: 0x00015C32
		internal static string ErrorComponent_Character
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ErrorComponent_Character", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x00017A48 File Offset: 0x00015C48
		internal static string ErrorComponent_EndOfFile
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ErrorComponent_EndOfFile", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00017A5E File Offset: 0x00015C5E
		internal static string ErrorComponent_Newline
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ErrorComponent_Newline", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x00017A74 File Offset: 0x00015C74
		internal static string ErrorComponent_Whitespace
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ErrorComponent_Whitespace", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00017A8A File Offset: 0x00015C8A
		internal static string HtmlSymbol_NewLine
		{
			get
			{
				return RazorResources.ResourceManager.GetString("HtmlSymbol_NewLine", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x00017AA0 File Offset: 0x00015CA0
		internal static string HtmlSymbol_RazorComment
		{
			get
			{
				return RazorResources.ResourceManager.GetString("HtmlSymbol_RazorComment", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00017AB6 File Offset: 0x00015CB6
		internal static string HtmlSymbol_Text
		{
			get
			{
				return RazorResources.ResourceManager.GetString("HtmlSymbol_Text", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x00017ACC File Offset: 0x00015CCC
		internal static string HtmlSymbol_WhiteSpace
		{
			get
			{
				return RazorResources.ResourceManager.GetString("HtmlSymbol_WhiteSpace", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x00017AE2 File Offset: 0x00015CE2
		internal static string Language_Does_Not_Support_RazorComment
		{
			get
			{
				return RazorResources.ResourceManager.GetString("Language_Does_Not_Support_RazorComment", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x00017AF8 File Offset: 0x00015CF8
		internal static string ParseError_AtInCode_Must_Be_Followed_By_Colon_Paren_Or_Identifier_Start
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_AtInCode_Must_Be_Followed_By_Colon_Paren_Or_Identifier_Start", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00017B0E File Offset: 0x00015D0E
		internal static string ParseError_BlockComment_Not_Terminated
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_BlockComment_Not_Terminated", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x00017B24 File Offset: 0x00015D24
		internal static string ParseError_BlockNotTerminated
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_BlockNotTerminated", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x00017B3A File Offset: 0x00015D3A
		internal static string ParseError_Expected_CloseBracket_Before_EOF
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_Expected_CloseBracket_Before_EOF", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x00017B50 File Offset: 0x00015D50
		internal static string ParseError_Expected_EndOfBlock_Before_EOF
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_Expected_EndOfBlock_Before_EOF", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x00017B66 File Offset: 0x00015D66
		internal static string ParseError_Expected_X
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_Expected_X", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x00017B7C File Offset: 0x00015D7C
		internal static string ParseError_Helpers_Cannot_Be_Nested
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_Helpers_Cannot_Be_Nested", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00017B92 File Offset: 0x00015D92
		internal static string ParseError_InheritsKeyword_Must_Be_Followed_By_TypeName
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_InheritsKeyword_Must_Be_Followed_By_TypeName", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x00017BA8 File Offset: 0x00015DA8
		internal static string ParseError_InlineMarkup_Blocks_Cannot_Be_Nested
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_InlineMarkup_Blocks_Cannot_Be_Nested", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x00017BBE File Offset: 0x00015DBE
		internal static string ParseError_InvalidOptionValue
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_InvalidOptionValue", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x00017BD4 File Offset: 0x00015DD4
		internal static string ParseError_MarkupBlock_Must_Start_With_Tag
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_MarkupBlock_Must_Start_With_Tag", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x00017BEA File Offset: 0x00015DEA
		internal static string ParseError_MissingCharAfterHelperName
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_MissingCharAfterHelperName", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x00017C00 File Offset: 0x00015E00
		internal static string ParseError_MissingCharAfterHelperParameters
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_MissingCharAfterHelperParameters", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x00017C16 File Offset: 0x00015E16
		internal static string ParseError_MissingEndTag
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_MissingEndTag", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x00017C2C File Offset: 0x00015E2C
		internal static string ParseError_MissingOpenBraceAfterSection
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_MissingOpenBraceAfterSection", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x00017C42 File Offset: 0x00015E42
		internal static string ParseError_NamespaceImportAndTypeAlias_Cannot_Exist_Within_CodeBlock
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_NamespaceImportAndTypeAlias_Cannot_Exist_Within_CodeBlock", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x00017C58 File Offset: 0x00015E58
		internal static string ParseError_NamespaceOrTypeAliasExpected
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_NamespaceOrTypeAliasExpected", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x00017C6E File Offset: 0x00015E6E
		internal static string ParseError_OuterTagMissingName
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_OuterTagMissingName", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00017C84 File Offset: 0x00015E84
		internal static string ParseError_RazorComment_Not_Terminated
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_RazorComment_Not_Terminated", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00017C9A File Offset: 0x00015E9A
		internal static string ParseError_ReservedWord
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_ReservedWord", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00017CB0 File Offset: 0x00015EB0
		internal static string ParseError_Sections_Cannot_Be_Nested
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_Sections_Cannot_Be_Nested", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x00017CC6 File Offset: 0x00015EC6
		internal static string ParseError_SingleLine_ControlFlowStatements_Not_Allowed
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_SingleLine_ControlFlowStatements_Not_Allowed", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00017CDC File Offset: 0x00015EDC
		internal static string ParseError_TextTagCannotContainAttributes
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_TextTagCannotContainAttributes", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00017CF2 File Offset: 0x00015EF2
		internal static string ParseError_Unexpected
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_Unexpected", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00017D08 File Offset: 0x00015F08
		internal static string ParseError_Unexpected_Character_At_Helper_Name_Start
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_Unexpected_Character_At_Helper_Name_Start", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x00017D1E File Offset: 0x00015F1E
		internal static string ParseError_Unexpected_Character_At_Section_Name_Start
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_Unexpected_Character_At_Section_Name_Start", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x00017D34 File Offset: 0x00015F34
		internal static string ParseError_Unexpected_Character_At_Start_Of_CodeBlock_CS
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_Unexpected_Character_At_Start_Of_CodeBlock_CS", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x00017D4A File Offset: 0x00015F4A
		internal static string ParseError_Unexpected_Character_At_Start_Of_CodeBlock_VB
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_Unexpected_Character_At_Start_Of_CodeBlock_VB", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x00017D60 File Offset: 0x00015F60
		internal static string ParseError_Unexpected_EndOfFile_At_Start_Of_CodeBlock
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_Unexpected_EndOfFile_At_Start_Of_CodeBlock", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x00017D76 File Offset: 0x00015F76
		internal static string ParseError_Unexpected_Keyword_After_At
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_Unexpected_Keyword_After_At", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x00017D8C File Offset: 0x00015F8C
		internal static string ParseError_Unexpected_Nested_CodeBlock
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_Unexpected_Nested_CodeBlock", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x00017DA2 File Offset: 0x00015FA2
		internal static string ParseError_Unexpected_WhiteSpace_At_Start_Of_CodeBlock_CS
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_Unexpected_WhiteSpace_At_Start_Of_CodeBlock_CS", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x00017DB8 File Offset: 0x00015FB8
		internal static string ParseError_Unexpected_WhiteSpace_At_Start_Of_CodeBlock_VB
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_Unexpected_WhiteSpace_At_Start_Of_CodeBlock_VB", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x00017DCE File Offset: 0x00015FCE
		internal static string ParseError_UnexpectedEndTag
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_UnexpectedEndTag", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00017DE4 File Offset: 0x00015FE4
		internal static string ParseError_UnfinishedTag
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_UnfinishedTag", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x00017DFA File Offset: 0x00015FFA
		internal static string ParseError_UnknownOption
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_UnknownOption", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x00017E10 File Offset: 0x00016010
		internal static string ParseError_Unterminated_String_Literal
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_Unterminated_String_Literal", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x00017E26 File Offset: 0x00016026
		internal static string ParseError_UnterminatedHelperParameterList
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParseError_UnterminatedHelperParameterList", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00017E3C File Offset: 0x0001603C
		internal static string Parser_Context_Not_Set
		{
			get
			{
				return RazorResources.ResourceManager.GetString("Parser_Context_Not_Set", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x00017E52 File Offset: 0x00016052
		internal static string ParserContext_CannotCompleteTree_NoRootBlock
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParserContext_CannotCompleteTree_NoRootBlock", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x00017E68 File Offset: 0x00016068
		internal static string ParserContext_CannotCompleteTree_OutstandingBlocks
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParserContext_CannotCompleteTree_OutstandingBlocks", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x00017E7E File Offset: 0x0001607E
		internal static string ParserContext_NoCurrentBlock
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParserContext_NoCurrentBlock", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00017E94 File Offset: 0x00016094
		internal static string ParserContext_ParseComplete
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParserContext_ParseComplete", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00017EAA File Offset: 0x000160AA
		internal static string ParserEror_SessionDirectiveMissingValue
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParserEror_SessionDirectiveMissingValue", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x00017EC0 File Offset: 0x000160C0
		internal static string ParserIsNotAMarkupParser
		{
			get
			{
				return RazorResources.ResourceManager.GetString("ParserIsNotAMarkupParser", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00017ED6 File Offset: 0x000160D6
		internal static string SectionExample_CS
		{
			get
			{
				return RazorResources.ResourceManager.GetString("SectionExample_CS", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x00017EEC File Offset: 0x000160EC
		internal static string SectionExample_VB
		{
			get
			{
				return RazorResources.ResourceManager.GetString("SectionExample_VB", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00017F02 File Offset: 0x00016102
		internal static string Structure_Member_CannotBeNull
		{
			get
			{
				return RazorResources.ResourceManager.GetString("Structure_Member_CannotBeNull", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x00017F18 File Offset: 0x00016118
		internal static string Symbol_Unknown
		{
			get
			{
				return RazorResources.ResourceManager.GetString("Symbol_Unknown", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x00017F2E File Offset: 0x0001612E
		internal static string Tokenizer_CannotResumeSymbolUnlessIsPrevious
		{
			get
			{
				return RazorResources.ResourceManager.GetString("Tokenizer_CannotResumeSymbolUnlessIsPrevious", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00017F44 File Offset: 0x00016144
		internal static string TokenizerView_CannotPutBack
		{
			get
			{
				return RazorResources.ResourceManager.GetString("TokenizerView_CannotPutBack", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00017F5A File Offset: 0x0001615A
		internal static string Trace_BackgroundThreadShutdown
		{
			get
			{
				return RazorResources.ResourceManager.GetString("Trace_BackgroundThreadShutdown", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x00017F70 File Offset: 0x00016170
		internal static string Trace_BackgroundThreadStart
		{
			get
			{
				return RazorResources.ResourceManager.GetString("Trace_BackgroundThreadStart", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x00017F86 File Offset: 0x00016186
		internal static string Trace_ChangesArrived
		{
			get
			{
				return RazorResources.ResourceManager.GetString("Trace_ChangesArrived", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x00017F9C File Offset: 0x0001619C
		internal static string Trace_ChangesDiscarded
		{
			get
			{
				return RazorResources.ResourceManager.GetString("Trace_ChangesDiscarded", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x00017FB2 File Offset: 0x000161B2
		internal static string Trace_CollectedDiscardedChanges
		{
			get
			{
				return RazorResources.ResourceManager.GetString("Trace_CollectedDiscardedChanges", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x00017FC8 File Offset: 0x000161C8
		internal static string Trace_Disabled
		{
			get
			{
				return RazorResources.ResourceManager.GetString("Trace_Disabled", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x00017FDE File Offset: 0x000161DE
		internal static string Trace_EditorProcessedChange
		{
			get
			{
				return RazorResources.ResourceManager.GetString("Trace_EditorProcessedChange", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x00017FF4 File Offset: 0x000161F4
		internal static string Trace_EditorReceivedChange
		{
			get
			{
				return RazorResources.ResourceManager.GetString("Trace_EditorReceivedChange", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0001800A File Offset: 0x0001620A
		internal static string Trace_Enabled
		{
			get
			{
				return RazorResources.ResourceManager.GetString("Trace_Enabled", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00018020 File Offset: 0x00016220
		internal static string Trace_Format
		{
			get
			{
				return RazorResources.ResourceManager.GetString("Trace_Format", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x00018036 File Offset: 0x00016236
		internal static string Trace_NoChangesArrived
		{
			get
			{
				return RazorResources.ResourceManager.GetString("Trace_NoChangesArrived", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0001804C File Offset: 0x0001624C
		internal static string Trace_ParseComplete
		{
			get
			{
				return RazorResources.ResourceManager.GetString("Trace_ParseComplete", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x00018062 File Offset: 0x00016262
		internal static string Trace_QueuingParse
		{
			get
			{
				return RazorResources.ResourceManager.GetString("Trace_QueuingParse", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00018078 File Offset: 0x00016278
		internal static string Trace_Startup
		{
			get
			{
				return RazorResources.ResourceManager.GetString("Trace_Startup", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0001808E File Offset: 0x0001628E
		internal static string Trace_TreesCompared
		{
			get
			{
				return RazorResources.ResourceManager.GetString("Trace_TreesCompared", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x000180A4 File Offset: 0x000162A4
		internal static string VBSymbol_CharacterLiteral
		{
			get
			{
				return RazorResources.ResourceManager.GetString("VBSymbol_CharacterLiteral", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x000180BA File Offset: 0x000162BA
		internal static string VBSymbol_Comment
		{
			get
			{
				return RazorResources.ResourceManager.GetString("VBSymbol_Comment", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x000180D0 File Offset: 0x000162D0
		internal static string VBSymbol_DateLiteral
		{
			get
			{
				return RazorResources.ResourceManager.GetString("VBSymbol_DateLiteral", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x000180E6 File Offset: 0x000162E6
		internal static string VBSymbol_FloatingPointLiteral
		{
			get
			{
				return RazorResources.ResourceManager.GetString("VBSymbol_FloatingPointLiteral", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x000180FC File Offset: 0x000162FC
		internal static string VBSymbol_Identifier
		{
			get
			{
				return RazorResources.ResourceManager.GetString("VBSymbol_Identifier", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x00018112 File Offset: 0x00016312
		internal static string VBSymbol_IntegerLiteral
		{
			get
			{
				return RazorResources.ResourceManager.GetString("VBSymbol_IntegerLiteral", RazorResources.resourceCulture);
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00018128 File Offset: 0x00016328
		internal static string VBSymbol_Keyword
		{
			get
			{
				return RazorResources.ResourceManager.GetString("VBSymbol_Keyword", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x0001813E File Offset: 0x0001633E
		internal static string VBSymbol_NewLine
		{
			get
			{
				return RazorResources.ResourceManager.GetString("VBSymbol_NewLine", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00018154 File Offset: 0x00016354
		internal static string VBSymbol_RazorComment
		{
			get
			{
				return RazorResources.ResourceManager.GetString("VBSymbol_RazorComment", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x0001816A File Offset: 0x0001636A
		internal static string VBSymbol_StringLiteral
		{
			get
			{
				return RazorResources.ResourceManager.GetString("VBSymbol_StringLiteral", RazorResources.resourceCulture);
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00018180 File Offset: 0x00016380
		internal static string VBSymbol_WhiteSpace
		{
			get
			{
				return RazorResources.ResourceManager.GetString("VBSymbol_WhiteSpace", RazorResources.resourceCulture);
			}
		}

		// Token: 0x0400032D RID: 813
		private static ResourceManager resourceMan;

		// Token: 0x0400032E RID: 814
		private static CultureInfo resourceCulture;
	}
}
