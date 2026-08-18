using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using WebGrease.Configuration;
using WebGrease.Css.Ast;

namespace WebGrease.Css
{
	// Token: 0x02000146 RID: 326
	[GeneratedCode("ANTLR", "3.3.1.7705")]
	[CLSCompliant(false)]
	public class CssParser : Parser
	{
		// Token: 0x06001301 RID: 4865 RVA: 0x0006557C File Offset: 0x0006377C
		public static StyleSheetNode Parse(IWebGreaseContext context, string cssContent, bool shouldLogDiagnostics = true)
		{
			return CssParser.ParseStyleSheet(context, cssContent, shouldLogDiagnostics);
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00065588 File Offset: 0x00063788
		public static StyleSheetNode Parse(FileInfo cssFile, bool shouldLogDiagnostics = true)
		{
			string fullName = cssFile.FullName;
			Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "Parsing {0} ", new object[]
			{
				fullName
			}));
			return CssParser.ParseStyleSheet(new WebGreaseContext(new WebGreaseConfiguration(), null, null, null, null, null, null), File.ReadAllText(fullName), shouldLogDiagnostics);
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x000655D8 File Offset: 0x000637D8
		public override void ReportError(RecognitionException e)
		{
			if (e != null)
			{
				this._exceptions.Add(e);
				base.ReportError(e);
			}
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x000656A0 File Offset: 0x000638A0
		private static StyleSheetNode ParseStyleSheet(IWebGreaseContext context, string cssContent, bool shouldLogDiagnostics)
		{
			CssLexer tokenSource = new CssLexer(new ANTLRStringStream(cssContent));
			CommonTokenStream input = new CommonTokenStream(tokenSource);
			CssParser parser = new CssParser(input);
			CommonTree commonTree = context.SectionedAction(new string[]
			{
				"CssParser",
				"Antlr"
			}).Execute<CommonTree>(delegate()
			{
				if (shouldLogDiagnostics)
				{
					TextWriterTraceListener textWriterTraceListener = Trace.Listeners.OfType<TextWriterTraceListener>().FirstOrDefault<TextWriterTraceListener>();
					if (textWriterTraceListener != null)
					{
						parser.TraceDestination = textWriterTraceListener.Writer;
					}
				}
				CssParser.main_return main_return = parser.main();
				return main_return.Tree as CommonTree;
			});
			if (commonTree != null)
			{
				return context.SectionedAction(new string[]
				{
					"CssParser",
					"CreateObjects"
				}).Execute<StyleSheetNode>(delegate()
				{
					if (shouldLogDiagnostics)
					{
						CssParser.LogDiagnostics(cssContent, commonTree);
					}
					if (parser.NumberOfSyntaxErrors > 0)
					{
						throw new AggregateException("Syntax errors found.", parser._exceptions);
					}
					return CommonTreeTransformer.CreateStyleSheetNode(commonTree);
				});
			}
			return null;
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00065764 File Offset: 0x00063964
		private static void LogDiagnostics(string css, CommonTree commonTree)
		{
			Trace.WriteLine("Input Css:");
			Trace.WriteLine("____________________________________________________");
			Trace.WriteLine(css);
			Trace.WriteLine("____________________________________________________");
			Trace.WriteLine("Css String Tree:");
			Trace.WriteLine("____________________________________________________");
			Trace.WriteLine(commonTree.ToStringTree());
			Trace.WriteLine("____________________________________________________");
			Trace.WriteLine("Css Common Tree:");
			Trace.WriteLine("____________________________________________________");
			CssParser.LogTree(commonTree);
			Trace.WriteLine("____________________________________________________");
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x000657E4 File Offset: 0x000639E4
		private static void LogTree(CommonTree tree)
		{
			Stack<Tuple<int, CommonTree>> stack = new Stack<Tuple<int, CommonTree>>();
			stack.Push(new Tuple<int, CommonTree>(0, tree));
			while (stack.Count > 0)
			{
				Tuple<int, CommonTree> tuple = stack.Pop();
				int item = tuple.Item1;
				CommonTree item2 = tuple.Item2;
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < item; i++)
				{
					stringBuilder.Append("---");
				}
				Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[]
				{
					stringBuilder,
					item2
				}));
				IList<ITree> children = item2.Children;
				if (children != null)
				{
					foreach (CommonTree item3 in children.OfType<CommonTree>().Reverse<CommonTree>())
					{
						stack.Push(new Tuple<int, CommonTree>(item + 1, item3));
					}
				}
			}
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x000658D8 File Offset: 0x00063AD8
		private CommonToken GetWhitespaceToken()
		{
			if (this.input.Index > 0)
			{
				IToken token = this.input.Get(this.input.Index - 1);
				if (token != null && token.Type == 105 && token.Text != null && string.IsNullOrWhiteSpace(token.Text))
				{
					return new CommonToken(190, token.Text.Length.ToString());
				}
			}
			return new CommonToken(190, "0");
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0006595B File Offset: 0x00063B5B
		private static CommonToken TrimMsieExpression(string text)
		{
			if (text.EndsWith(";"))
			{
				text = text.TrimEnd(CssParser._semicolon);
			}
			return new CommonToken(54, text);
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x0006597F File Offset: 0x00063B7F
		public CssParser(ITokenStream input) : this(input, new RecognizerSharedState())
		{
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00065990 File Offset: 0x00063B90
		public CssParser(ITokenStream input, RecognizerSharedState state) : base(input, state)
		{
			ITreeAdaptor treeAdaptor = null;
			this.TreeAdaptor = (treeAdaptor ?? new CommonTreeAdaptor());
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x000659C2 File Offset: 0x00063BC2
		// (set) Token: 0x0600130C RID: 4876 RVA: 0x000659CA File Offset: 0x00063BCA
		public ITreeAdaptor TreeAdaptor
		{
			get
			{
				return this.adaptor;
			}
			set
			{
				this.adaptor = value;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x000659D3 File Offset: 0x00063BD3
		public override string[] TokenNames
		{
			get
			{
				return CssParser.tokenNames;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x000659DA File Offset: 0x00063BDA
		public override string GrammarFileName
		{
			get
			{
				return "Css\\CssParser.g3";
			}
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x000659E4 File Offset: 0x00063BE4
		[GrammarRule("main")]
		public CssParser.main_return main()
		{
			CssParser.main_return main_return = new CssParser.main_return(this);
			main_return.Start = (CommonToken)this.input.LT(1);
			try
			{
				object obj = this.adaptor.Nil();
				base.PushFollow(CssParser.Follow._styleSheet_in_main653);
				CssParser.styleSheet_return styleSheet_return = this.styleSheet();
				base.PopFollow();
				if (this.state.failed)
				{
					return main_return;
				}
				if (this.state.backtracking == 0)
				{
					this.adaptor.AddChild(obj, styleSheet_return.Tree);
				}
				CommonToken payload = (CommonToken)this.Match(this.input, -1, CssParser.Follow._EOF_in_main659);
				if (this.state.failed)
				{
					return main_return;
				}
				if (this.state.backtracking == 0)
				{
					object child = this.adaptor.Create(payload);
					this.adaptor.AddChild(obj, child);
				}
				main_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					main_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(main_return.Tree, main_return.Start, main_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				main_return.Tree = this.adaptor.ErrorNode(this.input, main_return.Start, this.input.LT(-1), ex);
			}
			return main_return;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00065B80 File Offset: 0x00063D80
		[GrammarRule("styleSheet")]
		private CssParser.styleSheet_return styleSheet()
		{
			CssParser.styleSheet_return styleSheet_return = new CssParser.styleSheet_return(this);
			styleSheet_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token CHARSET_SYM");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token STRING");
			RewriteRuleTokenStream rewriteRuleTokenStream3 = new RewriteRuleTokenStream(this.adaptor, "token SEMICOLON");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule styleimport");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream2 = new RewriteRuleSubtreeStream(this.adaptor, "rule namespace");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream3 = new RewriteRuleSubtreeStream(this.adaptor, "rule styleSheetRulesOrComment");
			try
			{
				int num = 2;
				int num2 = this.input.LA(1);
				if (num2 == 11)
				{
					num = 1;
				}
				int num3 = num;
				if (num3 == 1)
				{
					CommonToken el = (CommonToken)this.Match(this.input, 11, CssParser.Follow._CHARSET_SYM_in_styleSheet683);
					if (this.state.failed)
					{
						return styleSheet_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream.Add(el);
					}
					CommonToken el2 = (CommonToken)this.Match(this.input, 85, CssParser.Follow._STRING_in_styleSheet685);
					if (this.state.failed)
					{
						return styleSheet_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream2.Add(el2);
					}
					CommonToken el3 = (CommonToken)this.Match(this.input, 79, CssParser.Follow._SEMICOLON_in_styleSheet687);
					if (this.state.failed)
					{
						return styleSheet_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream3.Add(el3);
					}
				}
				for (;;)
				{
					int num4 = 2;
					int num5 = this.input.LA(1);
					if (num5 == 44)
					{
						num4 = 1;
					}
					int num6 = num4;
					if (num6 != 1)
					{
						break;
					}
					base.PushFollow(CssParser.Follow._styleimport_in_styleSheet691);
					CssParser.styleimport_return styleimport_return = this.styleimport();
					base.PopFollow();
					if (this.state.failed)
					{
						goto Block_13;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream.Add(styleimport_return.Tree);
					}
				}
				for (;;)
				{
					int num7 = 2;
					int num8 = this.input.LA(1);
					if (num8 == 58)
					{
						num7 = 1;
					}
					int num9 = num7;
					if (num9 != 1)
					{
						break;
					}
					base.PushFollow(CssParser.Follow._namespace_in_styleSheet694);
					CssParser.namespace_return namespace_return = this.@namespace();
					base.PopFollow();
					if (this.state.failed)
					{
						goto Block_17;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream2.Add(namespace_return.Tree);
					}
				}
				for (;;)
				{
					int num10 = 2;
					int num11 = this.input.LA(1);
					if (num11 == 7 || (num11 >= 14 && num11 <= 15) || (num11 == 24 || num11 == 38 || (num11 >= 41 && num11 <= 42)) || num11 == 47 || num11 == 52 || num11 == 68 || num11 == 70 || num11 == 76 || num11 == 82 || num11 == 84 || num11 == 104)
					{
						num10 = 1;
					}
					int num12 = num10;
					if (num12 != 1)
					{
						goto IL_330;
					}
					base.PushFollow(CssParser.Follow._styleSheetRulesOrComment_in_styleSheet697);
					CssParser.styleSheetRulesOrComment_return styleSheetRulesOrComment_return = this.styleSheetRulesOrComment();
					base.PopFollow();
					if (this.state.failed)
					{
						break;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream3.Add(styleSheetRulesOrComment_return.Tree);
					}
				}
				return styleSheet_return;
				IL_330:
				if (this.state.backtracking == 0)
				{
					styleSheet_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (styleSheet_return != null) ? styleSheet_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(180, "STYLESHEET"), obj2);
					if (rewriteRuleTokenStream2.HasNext)
					{
						object obj3 = this.adaptor.Nil();
						obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(116, "CHARSET"), obj3);
						object obj4 = this.adaptor.Nil();
						obj4 = this.adaptor.BecomeRoot(this.adaptor.Create(179, "STRINGBASEDVALUE"), obj4);
						this.adaptor.AddChild(obj4, rewriteRuleTokenStream2.NextNode());
						this.adaptor.AddChild(obj3, obj4);
						this.adaptor.AddChild(obj2, obj3);
					}
					rewriteRuleTokenStream2.Reset();
					if (rewriteRuleSubtreeStream.HasNext)
					{
						object obj5 = this.adaptor.Nil();
						obj5 = this.adaptor.BecomeRoot(this.adaptor.Create(140, "IMPORTS"), obj5);
						while (rewriteRuleSubtreeStream.HasNext)
						{
							this.adaptor.AddChild(obj5, rewriteRuleSubtreeStream.NextTree());
						}
						rewriteRuleSubtreeStream.Reset();
						this.adaptor.AddChild(obj2, obj5);
					}
					rewriteRuleSubtreeStream.Reset();
					if (rewriteRuleSubtreeStream2.HasNext)
					{
						object obj6 = this.adaptor.Nil();
						obj6 = this.adaptor.BecomeRoot(this.adaptor.Create(155, "NAMESPACES"), obj6);
						while (rewriteRuleSubtreeStream2.HasNext)
						{
							this.adaptor.AddChild(obj6, rewriteRuleSubtreeStream2.NextTree());
						}
						rewriteRuleSubtreeStream2.Reset();
						this.adaptor.AddChild(obj2, obj6);
					}
					rewriteRuleSubtreeStream2.Reset();
					while (rewriteRuleSubtreeStream3.HasNext)
					{
						this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream3.NextTree());
					}
					rewriteRuleSubtreeStream3.Reset();
					this.adaptor.AddChild(obj, obj2);
					styleSheet_return.Tree = obj;
				}
				styleSheet_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					styleSheet_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(styleSheet_return.Tree, styleSheet_return.Start, styleSheet_return.Stop);
				}
				return styleSheet_return;
				Block_17:
				return styleSheet_return;
				Block_13:
				return styleSheet_return;
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				styleSheet_return.Tree = this.adaptor.ErrorNode(this.input, styleSheet_return.Start, this.input.LT(-1), ex);
			}
			return styleSheet_return;
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x000661B4 File Offset: 0x000643B4
		[GrammarRule("styleSheetRulesOrComment")]
		private CssParser.styleSheetRulesOrComment_return styleSheetRulesOrComment()
		{
			CssParser.styleSheetRulesOrComment_return styleSheetRulesOrComment_return = new CssParser.styleSheetRulesOrComment_return(this);
			styleSheetRulesOrComment_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			try
			{
				int num = this.input.LA(1);
				int num2;
				if (num == 42)
				{
					num2 = 1;
				}
				else if (num == 7 || (num >= 14 && num <= 15) || num == 24 || num == 38 || num == 41 || num == 47 || num == 52 || num == 68 || num == 70 || num == 76 || num == 82 || num == 84 || num == 104)
				{
					num2 = 2;
				}
				else
				{
					if (this.state.backtracking > 0)
					{
						this.state.failed = true;
						return styleSheetRulesOrComment_return;
					}
					NoViableAltException ex = new NoViableAltException("", 5, 0, this.input);
					throw ex;
				}
				switch (num2)
				{
				case 1:
				{
					obj = this.adaptor.Nil();
					CommonToken payload = (CommonToken)this.Match(this.input, 42, CssParser.Follow._IMPORTANT_COMMENTS_in_styleSheetRulesOrComment756);
					if (this.state.failed)
					{
						return styleSheetRulesOrComment_return;
					}
					if (this.state.backtracking == 0)
					{
						object child = this.adaptor.Create(payload);
						this.adaptor.AddChild(obj, child);
					}
					break;
				}
				case 2:
				{
					obj = this.adaptor.Nil();
					base.PushFollow(CssParser.Follow._styleSheetrules_in_styleSheetRulesOrComment764);
					CssParser.styleSheetrules_return styleSheetrules_return = this.styleSheetrules();
					base.PopFollow();
					if (this.state.failed)
					{
						return styleSheetRulesOrComment_return;
					}
					if (this.state.backtracking == 0)
					{
						this.adaptor.AddChild(obj, styleSheetrules_return.Tree);
					}
					break;
				}
				}
				styleSheetRulesOrComment_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					styleSheetRulesOrComment_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(styleSheetRulesOrComment_return.Tree, styleSheetRulesOrComment_return.Start, styleSheetRulesOrComment_return.Stop);
				}
			}
			catch (RecognitionException ex2)
			{
				this.ReportError(ex2);
				this.Recover(this.input, ex2);
				styleSheetRulesOrComment_return.Tree = this.adaptor.ErrorNode(this.input, styleSheetRulesOrComment_return.Start, this.input.LT(-1), ex2);
			}
			return styleSheetRulesOrComment_return;
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x0006642C File Offset: 0x0006462C
		[GrammarRule("styleimport")]
		private CssParser.styleimport_return styleimport()
		{
			CssParser.styleimport_return styleimport_return = new CssParser.styleimport_return(this);
			styleimport_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token IMPORT_SYM");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token SEMICOLON");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule stringoruri");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream2 = new RewriteRuleSubtreeStream(this.adaptor, "rule media_query_list");
			try
			{
				CommonToken el = (CommonToken)this.Match(this.input, 44, CssParser.Follow._IMPORT_SYM_in_styleimport784);
				if (this.state.failed)
				{
					return styleimport_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream.Add(el);
				}
				base.PushFollow(CssParser.Follow._stringoruri_in_styleimport786);
				CssParser.stringoruri_return stringoruri_return = this.stringoruri();
				base.PopFollow();
				if (this.state.failed)
				{
					return styleimport_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleSubtreeStream.Add(stringoruri_return.Tree);
				}
				int num = 2;
				int num2 = this.input.LA(1);
				if (num2 == 12 || num2 == 41 || num2 == 63 || num2 == 66)
				{
					num = 1;
				}
				int num3 = num;
				if (num3 == 1)
				{
					base.PushFollow(CssParser.Follow._media_query_list_in_styleimport788);
					CssParser.media_query_list_return media_query_list_return = this.media_query_list();
					base.PopFollow();
					if (this.state.failed)
					{
						return styleimport_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream2.Add(media_query_list_return.Tree);
					}
				}
				CommonToken el2 = (CommonToken)this.Match(this.input, 79, CssParser.Follow._SEMICOLON_in_styleimport791);
				if (this.state.failed)
				{
					return styleimport_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream2.Add(el2);
				}
				if (this.state.backtracking == 0)
				{
					styleimport_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (styleimport_return != null) ? styleimport_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(138, "IMPORT"), obj2);
					this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream.NextTree());
					if (rewriteRuleSubtreeStream2.HasNext)
					{
						this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream2.NextTree());
					}
					rewriteRuleSubtreeStream2.Reset();
					this.adaptor.AddChild(obj, obj2);
					styleimport_return.Tree = obj;
				}
				styleimport_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					styleimport_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(styleimport_return.Tree, styleimport_return.Start, styleimport_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				styleimport_return.Tree = this.adaptor.ErrorNode(this.input, styleimport_return.Start, this.input.LT(-1), ex);
			}
			return styleimport_return;
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x00066770 File Offset: 0x00064970
		[GrammarRule("namespace")]
		private CssParser.namespace_return @namespace()
		{
			CssParser.namespace_return namespace_return = new CssParser.namespace_return(this);
			namespace_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token NAMESPACE_SYM");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token SEMICOLON");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule namespace_prefix");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream2 = new RewriteRuleSubtreeStream(this.adaptor, "rule stringoruri");
			try
			{
				CommonToken el = (CommonToken)this.Match(this.input, 58, CssParser.Follow._NAMESPACE_SYM_in_namespace826);
				if (this.state.failed)
				{
					return namespace_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream.Add(el);
				}
				int num = 2;
				int num2 = this.input.LA(1);
				if (num2 == 41)
				{
					num = 1;
				}
				int num3 = num;
				if (num3 == 1)
				{
					base.PushFollow(CssParser.Follow._namespace_prefix_in_namespace828);
					CssParser.namespace_prefix_return namespace_prefix_return = this.namespace_prefix();
					base.PopFollow();
					if (this.state.failed)
					{
						return namespace_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream.Add(namespace_prefix_return.Tree);
					}
				}
				base.PushFollow(CssParser.Follow._stringoruri_in_namespace831);
				CssParser.stringoruri_return stringoruri_return = this.stringoruri();
				base.PopFollow();
				if (this.state.failed)
				{
					return namespace_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleSubtreeStream2.Add(stringoruri_return.Tree);
				}
				CommonToken el2 = (CommonToken)this.Match(this.input, 79, CssParser.Follow._SEMICOLON_in_namespace833);
				if (this.state.failed)
				{
					return namespace_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream2.Add(el2);
				}
				if (this.state.backtracking == 0)
				{
					namespace_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (namespace_return != null) ? namespace_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(154, "NAMESPACE"), obj2);
					if (rewriteRuleSubtreeStream.HasNext)
					{
						this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream.NextTree());
					}
					rewriteRuleSubtreeStream.Reset();
					this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream2.NextTree());
					this.adaptor.AddChild(obj, obj2);
					namespace_return.Tree = obj;
				}
				namespace_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					namespace_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(namespace_return.Tree, namespace_return.Start, namespace_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				namespace_return.Tree = this.adaptor.ErrorNode(this.input, namespace_return.Start, this.input.LT(-1), ex);
			}
			return namespace_return;
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00066AA4 File Offset: 0x00064CA4
		[GrammarRule("namespace_prefix")]
		private CssParser.namespace_prefix_return namespace_prefix()
		{
			CssParser.namespace_prefix_return namespace_prefix_return = new CssParser.namespace_prefix_return(this);
			namespace_prefix_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token IDENT");
			try
			{
				CommonToken el = (CommonToken)this.Match(this.input, 41, CssParser.Follow._IDENT_in_namespace_prefix865);
				if (this.state.failed)
				{
					return namespace_prefix_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream.Add(el);
				}
				if (this.state.backtracking == 0)
				{
					namespace_prefix_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (namespace_prefix_return != null) ? namespace_prefix_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(156, "NAMESPACE_PREFIX"), obj2);
					this.adaptor.AddChild(obj2, rewriteRuleTokenStream.NextNode());
					this.adaptor.AddChild(obj, obj2);
					namespace_prefix_return.Tree = obj;
				}
				namespace_prefix_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					namespace_prefix_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(namespace_prefix_return.Tree, namespace_prefix_return.Start, namespace_prefix_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				namespace_prefix_return.Tree = this.adaptor.ErrorNode(this.input, namespace_prefix_return.Start, this.input.LT(-1), ex);
			}
			return namespace_prefix_return;
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x00066C7C File Offset: 0x00064E7C
		[GrammarRule("wg_dpi")]
		private CssParser.wg_dpi_return wg_dpi()
		{
			CssParser.wg_dpi_return wg_dpi_return = new CssParser.wg_dpi_return(this);
			wg_dpi_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token WG_DPI_SYM");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token NUMBER");
			RewriteRuleTokenStream rewriteRuleTokenStream3 = new RewriteRuleTokenStream(this.adaptor, "token SEMICOLON");
			try
			{
				CommonToken el = (CommonToken)this.Match(this.input, 104, CssParser.Follow._WG_DPI_SYM_in_wg_dpi894);
				if (this.state.failed)
				{
					return wg_dpi_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream.Add(el);
				}
				CommonToken el2 = (CommonToken)this.Match(this.input, 64, CssParser.Follow._NUMBER_in_wg_dpi896);
				if (this.state.failed)
				{
					return wg_dpi_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream2.Add(el2);
				}
				CommonToken el3 = (CommonToken)this.Match(this.input, 79, CssParser.Follow._SEMICOLON_in_wg_dpi898);
				if (this.state.failed)
				{
					return wg_dpi_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream3.Add(el3);
				}
				if (this.state.backtracking == 0)
				{
					wg_dpi_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (wg_dpi_return != null) ? wg_dpi_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(189, "WG_DPI"), obj2);
					this.adaptor.AddChild(obj2, rewriteRuleTokenStream2.NextNode());
					this.adaptor.AddChild(obj, obj2);
					wg_dpi_return.Tree = obj;
				}
				wg_dpi_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					wg_dpi_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(wg_dpi_return.Tree, wg_dpi_return.Start, wg_dpi_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				wg_dpi_return.Tree = this.adaptor.ErrorNode(this.input, wg_dpi_return.Start, this.input.LT(-1), ex);
			}
			return wg_dpi_return;
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00066F08 File Offset: 0x00065108
		[GrammarRule("media")]
		private CssParser.media_return media()
		{
			CssParser.media_return media_return = new CssParser.media_return(this);
			media_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token MEDIA_SYM");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token CURLY_BEGIN");
			RewriteRuleTokenStream rewriteRuleTokenStream3 = new RewriteRuleTokenStream(this.adaptor, "token CURLY_END");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule media_query_list");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream2 = new RewriteRuleSubtreeStream(this.adaptor, "rule ruleset");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream3 = new RewriteRuleSubtreeStream(this.adaptor, "rule page");
			try
			{
				CommonToken el = (CommonToken)this.Match(this.input, 52, CssParser.Follow._MEDIA_SYM_in_media930);
				if (this.state.failed)
				{
					return media_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream.Add(el);
				}
				int num = 2;
				int num2 = this.input.LA(1);
				if (num2 == 12 || num2 == 41 || num2 == 63 || num2 == 66)
				{
					num = 1;
				}
				int num3 = num;
				if (num3 == 1)
				{
					base.PushFollow(CssParser.Follow._media_query_list_in_media932);
					CssParser.media_query_list_return media_query_list_return = this.media_query_list();
					base.PopFollow();
					if (this.state.failed)
					{
						return media_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream.Add(media_query_list_return.Tree);
					}
				}
				CommonToken el2 = (CommonToken)this.Match(this.input, 18, CssParser.Follow._CURLY_BEGIN_in_media935);
				if (this.state.failed)
				{
					return media_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream2.Add(el2);
				}
				for (;;)
				{
					int num4 = 3;
					int num5 = this.input.LA(1);
					if (num5 == 7 || (num5 >= 14 && num5 <= 15) || num5 == 38 || num5 == 41 || num5 == 70 || num5 == 76 || num5 == 82 || num5 == 84)
					{
						num4 = 1;
					}
					else if (num5 == 68)
					{
						num4 = 2;
					}
					switch (num4)
					{
					case 1:
					{
						base.PushFollow(CssParser.Follow._ruleset_in_media939);
						CssParser.ruleset_return ruleset_return = this.ruleset();
						base.PopFollow();
						if (this.state.failed)
						{
							goto Block_22;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleSubtreeStream2.Add(ruleset_return.Tree);
							continue;
						}
						continue;
					}
					case 2:
					{
						base.PushFollow(CssParser.Follow._page_in_media943);
						CssParser.page_return page_return = this.page();
						base.PopFollow();
						if (this.state.failed)
						{
							goto Block_24;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleSubtreeStream3.Add(page_return.Tree);
							continue;
						}
						continue;
					}
					}
					break;
				}
				CommonToken el3 = (CommonToken)this.Match(this.input, 19, CssParser.Follow._CURLY_END_in_media948);
				if (this.state.failed)
				{
					return media_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream3.Add(el3);
				}
				if (this.state.backtracking == 0)
				{
					media_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (media_return != null) ? media_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(147, "MEDIA"), obj2);
					if (rewriteRuleSubtreeStream.HasNext)
					{
						this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream.NextTree());
					}
					rewriteRuleSubtreeStream.Reset();
					if (rewriteRuleSubtreeStream2.HasNext)
					{
						object obj3 = this.adaptor.Nil();
						obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(172, "RULESETS"), obj3);
						while (rewriteRuleSubtreeStream2.HasNext)
						{
							this.adaptor.AddChild(obj3, rewriteRuleSubtreeStream2.NextTree());
						}
						rewriteRuleSubtreeStream2.Reset();
						this.adaptor.AddChild(obj2, obj3);
					}
					rewriteRuleSubtreeStream2.Reset();
					if (rewriteRuleSubtreeStream3.HasNext)
					{
						object obj4 = this.adaptor.Nil();
						obj4 = this.adaptor.BecomeRoot(this.adaptor.Create(163, "PAGE"), obj4);
						while (rewriteRuleSubtreeStream3.HasNext)
						{
							this.adaptor.AddChild(obj4, rewriteRuleSubtreeStream3.NextTree());
						}
						rewriteRuleSubtreeStream3.Reset();
						this.adaptor.AddChild(obj2, obj4);
					}
					rewriteRuleSubtreeStream3.Reset();
					this.adaptor.AddChild(obj, obj2);
					media_return.Tree = obj;
				}
				media_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					media_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(media_return.Tree, media_return.Start, media_return.Stop);
				}
				return media_return;
				Block_22:
				return media_return;
				Block_24:
				return media_return;
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				media_return.Tree = this.adaptor.ErrorNode(this.input, media_return.Start, this.input.LT(-1), ex);
			}
			return media_return;
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0006745C File Offset: 0x0006565C
		[GrammarRule("media_query_list")]
		private CssParser.media_query_list_return media_query_list()
		{
			CssParser.media_query_list_return media_query_list_return = new CssParser.media_query_list_return(this);
			media_query_list_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token COMMA");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule media_query");
			try
			{
				base.PushFollow(CssParser.Follow._media_query_in_media_query_list997);
				CssParser.media_query_return media_query_return = this.media_query();
				base.PopFollow();
				if (this.state.failed)
				{
					return media_query_list_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleSubtreeStream.Add(media_query_return.Tree);
				}
				for (;;)
				{
					int num = 2;
					int num2 = this.input.LA(1);
					if (num2 == 16)
					{
						num = 1;
					}
					int num3 = num;
					if (num3 != 1)
					{
						goto IL_14C;
					}
					CommonToken el = (CommonToken)this.Match(this.input, 16, CssParser.Follow._COMMA_in_media_query_list1000);
					if (this.state.failed)
					{
						break;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream.Add(el);
					}
					base.PushFollow(CssParser.Follow._media_query_in_media_query_list1002);
					CssParser.media_query_return media_query_return2 = this.media_query();
					base.PopFollow();
					if (this.state.failed)
					{
						goto Block_9;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream.Add(media_query_return2.Tree);
					}
				}
				return media_query_list_return;
				Block_9:
				return media_query_list_return;
				IL_14C:
				if (this.state.backtracking == 0)
				{
					media_query_list_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (media_query_list_return != null) ? media_query_list_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(152, "MEDIA_QUERY_LIST"), obj2);
					while (rewriteRuleSubtreeStream.HasNext)
					{
						this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream.NextTree());
					}
					rewriteRuleSubtreeStream.Reset();
					this.adaptor.AddChild(obj, obj2);
					media_query_list_return.Tree = obj;
				}
				media_query_list_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					media_query_list_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(media_query_list_return.Tree, media_query_list_return.Start, media_query_list_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				media_query_list_return.Tree = this.adaptor.ErrorNode(this.input, media_query_list_return.Start, this.input.LT(-1), ex);
			}
			return media_query_list_return;
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0006771C File Offset: 0x0006591C
		[GrammarRule("media_query")]
		private CssParser.media_query_return media_query()
		{
			CssParser.media_query_return media_query_return = new CssParser.media_query_return(this);
			media_query_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token ONLY");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token NOT");
			RewriteRuleTokenStream rewriteRuleTokenStream3 = new RewriteRuleTokenStream(this.adaptor, "token AND");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule media_type");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream2 = new RewriteRuleSubtreeStream(this.adaptor, "rule media_expression");
			try
			{
				int num = this.input.LA(1);
				int num2;
				if (num == 41 || num == 63 || num == 66)
				{
					num2 = 1;
				}
				else if (num == 12)
				{
					num2 = 2;
				}
				else
				{
					if (this.state.backtracking > 0)
					{
						this.state.failed = true;
						return media_query_return;
					}
					NoViableAltException ex = new NoViableAltException("", 14, 0, this.input);
					throw ex;
				}
				switch (num2)
				{
				case 1:
				{
					int num3 = 3;
					int num4 = this.input.LA(1);
					if (num4 == 66)
					{
						num3 = 1;
					}
					else if (num4 == 63)
					{
						num3 = 2;
					}
					switch (num3)
					{
					case 1:
					{
						CommonToken el = (CommonToken)this.Match(this.input, 66, CssParser.Follow._ONLY_in_media_query1036);
						if (this.state.failed)
						{
							return media_query_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream.Add(el);
						}
						break;
					}
					case 2:
					{
						CommonToken el2 = (CommonToken)this.Match(this.input, 63, CssParser.Follow._NOT_in_media_query1040);
						if (this.state.failed)
						{
							return media_query_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream2.Add(el2);
						}
						break;
					}
					}
					base.PushFollow(CssParser.Follow._media_type_in_media_query1044);
					CssParser.media_type_return media_type_return = this.media_type();
					base.PopFollow();
					if (this.state.failed)
					{
						return media_query_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream.Add(media_type_return.Tree);
					}
					for (;;)
					{
						int num5 = 2;
						int num6 = this.input.LA(1);
						if (num6 == 5)
						{
							num5 = 1;
						}
						int num7 = num5;
						if (num7 != 1)
						{
							goto IL_2E1;
						}
						CommonToken el3 = (CommonToken)this.Match(this.input, 5, CssParser.Follow._AND_in_media_query1047);
						if (this.state.failed)
						{
							break;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream3.Add(el3);
						}
						base.PushFollow(CssParser.Follow._media_expression_in_media_query1049);
						CssParser.media_expression_return media_expression_return = this.media_expression();
						base.PopFollow();
						if (this.state.failed)
						{
							goto Block_21;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleSubtreeStream2.Add(media_expression_return.Tree);
						}
					}
					return media_query_return;
					Block_21:
					return media_query_return;
					IL_2E1:
					if (this.state.backtracking == 0)
					{
						media_query_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (media_query_return != null) ? media_query_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj2 = this.adaptor.Nil();
						obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(151, "MEDIA_QUERY"), obj2);
						if (rewriteRuleTokenStream.HasNext)
						{
							object obj3 = this.adaptor.Nil();
							obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(161, "ONLY_TEXT"), obj3);
							this.adaptor.AddChild(obj3, rewriteRuleTokenStream.NextNode());
							this.adaptor.AddChild(obj2, obj3);
						}
						rewriteRuleTokenStream.Reset();
						if (rewriteRuleTokenStream2.HasNext)
						{
							object obj4 = this.adaptor.Nil();
							obj4 = this.adaptor.BecomeRoot(this.adaptor.Create(159, "NOT_TEXT"), obj4);
							this.adaptor.AddChild(obj4, rewriteRuleTokenStream2.NextNode());
							this.adaptor.AddChild(obj2, obj4);
						}
						rewriteRuleTokenStream2.Reset();
						this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream.NextTree());
						if (rewriteRuleSubtreeStream2.HasNext)
						{
							object obj5 = this.adaptor.Nil();
							obj5 = this.adaptor.BecomeRoot(this.adaptor.Create(149, "MEDIA_EXPRESSIONS"), obj5);
							while (rewriteRuleSubtreeStream2.HasNext)
							{
								this.adaptor.AddChild(obj5, rewriteRuleSubtreeStream2.NextTree());
							}
							rewriteRuleSubtreeStream2.Reset();
							this.adaptor.AddChild(obj2, obj5);
						}
						rewriteRuleSubtreeStream2.Reset();
						this.adaptor.AddChild(obj, obj2);
						media_query_return.Tree = obj;
					}
					break;
				}
				case 2:
				{
					base.PushFollow(CssParser.Follow._media_expression_in_media_query1087);
					CssParser.media_expression_return media_expression_return2 = this.media_expression();
					base.PopFollow();
					if (this.state.failed)
					{
						return media_query_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream2.Add(media_expression_return2.Tree);
					}
					for (;;)
					{
						int num8 = 2;
						int num9 = this.input.LA(1);
						if (num9 == 5)
						{
							num8 = 1;
						}
						int num10 = num8;
						if (num10 != 1)
						{
							goto IL_5C1;
						}
						CommonToken el4 = (CommonToken)this.Match(this.input, 5, CssParser.Follow._AND_in_media_query1090);
						if (this.state.failed)
						{
							break;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream3.Add(el4);
						}
						base.PushFollow(CssParser.Follow._media_expression_in_media_query1092);
						CssParser.media_expression_return media_expression_return3 = this.media_expression();
						base.PopFollow();
						if (this.state.failed)
						{
							goto Block_35;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleSubtreeStream2.Add(media_expression_return3.Tree);
						}
					}
					return media_query_return;
					Block_35:
					return media_query_return;
					IL_5C1:
					if (this.state.backtracking == 0)
					{
						media_query_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (media_query_return != null) ? media_query_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj6 = this.adaptor.Nil();
						obj6 = this.adaptor.BecomeRoot(this.adaptor.Create(151, "MEDIA_QUERY"), obj6);
						object obj7 = this.adaptor.Nil();
						obj7 = this.adaptor.BecomeRoot(this.adaptor.Create(149, "MEDIA_EXPRESSIONS"), obj7);
						while (rewriteRuleSubtreeStream2.HasNext)
						{
							this.adaptor.AddChild(obj7, rewriteRuleSubtreeStream2.NextTree());
						}
						rewriteRuleSubtreeStream2.Reset();
						this.adaptor.AddChild(obj6, obj7);
						this.adaptor.AddChild(obj, obj6);
						media_query_return.Tree = obj;
					}
					break;
				}
				}
				media_query_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					media_query_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(media_query_return.Tree, media_query_return.Start, media_query_return.Stop);
				}
			}
			catch (RecognitionException ex2)
			{
				this.ReportError(ex2);
				this.Recover(this.input, ex2);
				media_query_return.Tree = this.adaptor.ErrorNode(this.input, media_query_return.Start, this.input.LT(-1), ex2);
			}
			return media_query_return;
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00067E90 File Offset: 0x00066090
		[GrammarRule("media_type")]
		private CssParser.media_type_return media_type()
		{
			CssParser.media_type_return media_type_return = new CssParser.media_type_return(this);
			media_type_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token IDENT");
			try
			{
				CommonToken el = (CommonToken)this.Match(this.input, 41, CssParser.Follow._IDENT_in_media_type1122);
				if (this.state.failed)
				{
					return media_type_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream.Add(el);
				}
				if (this.state.backtracking == 0)
				{
					media_type_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (media_type_return != null) ? media_type_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(153, "MEDIA_TYPE"), obj2);
					this.adaptor.AddChild(obj2, rewriteRuleTokenStream.NextNode());
					this.adaptor.AddChild(obj, obj2);
					media_type_return.Tree = obj;
				}
				media_type_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					media_type_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(media_type_return.Tree, media_type_return.Start, media_type_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				media_type_return.Tree = this.adaptor.ErrorNode(this.input, media_type_return.Start, this.input.LT(-1), ex);
			}
			return media_type_return;
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00068068 File Offset: 0x00066268
		[GrammarRule("media_expression")]
		private CssParser.media_expression_return media_expression()
		{
			CssParser.media_expression_return media_expression_return = new CssParser.media_expression_return(this);
			media_expression_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token CIRCLE_BEGIN");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token COLON");
			RewriteRuleTokenStream rewriteRuleTokenStream3 = new RewriteRuleTokenStream(this.adaptor, "token CIRCLE_END");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule media_feature");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream2 = new RewriteRuleSubtreeStream(this.adaptor, "rule expr");
			try
			{
				CommonToken el = (CommonToken)this.Match(this.input, 12, CssParser.Follow._CIRCLE_BEGIN_in_media_expression1145);
				if (this.state.failed)
				{
					return media_expression_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream.Add(el);
				}
				base.PushFollow(CssParser.Follow._media_feature_in_media_expression1147);
				CssParser.media_feature_return media_feature_return = this.media_feature();
				base.PopFollow();
				if (this.state.failed)
				{
					return media_expression_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleSubtreeStream.Add(media_feature_return.Tree);
				}
				int num = 2;
				int num2 = this.input.LA(1);
				if (num2 == 15)
				{
					num = 1;
				}
				int num3 = num;
				if (num3 == 1)
				{
					CommonToken el2 = (CommonToken)this.Match(this.input, 15, CssParser.Follow._COLON_in_media_expression1150);
					if (this.state.failed)
					{
						return media_expression_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream2.Add(el2);
					}
					base.PushFollow(CssParser.Follow._expr_in_media_expression1152);
					CssParser.expr_return expr_return = this.expr();
					base.PopFollow();
					if (this.state.failed)
					{
						return media_expression_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream2.Add(expr_return.Tree);
					}
				}
				CommonToken el3 = (CommonToken)this.Match(this.input, 13, CssParser.Follow._CIRCLE_END_in_media_expression1156);
				if (this.state.failed)
				{
					return media_expression_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream3.Add(el3);
				}
				if (this.state.backtracking == 0)
				{
					media_expression_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (media_expression_return != null) ? media_expression_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(148, "MEDIA_EXPRESSION"), obj2);
					this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream.NextTree());
					if (rewriteRuleSubtreeStream2.HasNext)
					{
						this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream2.NextTree());
					}
					rewriteRuleSubtreeStream2.Reset();
					this.adaptor.AddChild(obj, obj2);
					media_expression_return.Tree = obj;
				}
				media_expression_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					media_expression_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(media_expression_return.Tree, media_expression_return.Start, media_expression_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				media_expression_return.Tree = this.adaptor.ErrorNode(this.input, media_expression_return.Start, this.input.LT(-1), ex);
			}
			return media_expression_return;
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x000683F8 File Offset: 0x000665F8
		[GrammarRule("media_feature")]
		private CssParser.media_feature_return media_feature()
		{
			CssParser.media_feature_return media_feature_return = new CssParser.media_feature_return(this);
			media_feature_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token IDENT");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token REPLACEMENTTOKEN");
			try
			{
				int num = this.input.LA(1);
				int num2;
				if (num == 41)
				{
					num2 = 1;
				}
				else if (num == 76)
				{
					num2 = 2;
				}
				else
				{
					if (this.state.backtracking > 0)
					{
						this.state.failed = true;
						return media_feature_return;
					}
					NoViableAltException ex = new NoViableAltException("", 16, 0, this.input);
					throw ex;
				}
				switch (num2)
				{
				case 1:
				{
					CommonToken el = (CommonToken)this.Match(this.input, 41, CssParser.Follow._IDENT_in_media_feature1183);
					if (this.state.failed)
					{
						return media_feature_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream.Add(el);
					}
					if (this.state.backtracking == 0)
					{
						media_feature_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (media_feature_return != null) ? media_feature_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj2 = this.adaptor.Nil();
						obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(150, "MEDIA_FEATURE"), obj2);
						this.adaptor.AddChild(obj2, rewriteRuleTokenStream.NextNode());
						this.adaptor.AddChild(obj, obj2);
						media_feature_return.Tree = obj;
					}
					break;
				}
				case 2:
				{
					CommonToken el2 = (CommonToken)this.Match(this.input, 76, CssParser.Follow._REPLACEMENTTOKEN_in_media_feature1197);
					if (this.state.failed)
					{
						return media_feature_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream2.Add(el2);
					}
					if (this.state.backtracking == 0)
					{
						media_feature_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (media_feature_return != null) ? media_feature_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj3 = this.adaptor.Nil();
						obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(150, "MEDIA_FEATURE"), obj3);
						this.adaptor.AddChild(obj3, rewriteRuleTokenStream2.NextNode());
						this.adaptor.AddChild(obj, obj3);
						media_feature_return.Tree = obj;
					}
					break;
				}
				}
				media_feature_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					media_feature_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(media_feature_return.Tree, media_feature_return.Start, media_feature_return.Stop);
				}
			}
			catch (RecognitionException ex2)
			{
				this.ReportError(ex2);
				this.Recover(this.input, ex2);
				media_feature_return.Tree = this.adaptor.ErrorNode(this.input, media_feature_return.Start, this.input.LT(-1), ex2);
			}
			return media_feature_return;
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00068744 File Offset: 0x00066944
		[GrammarRule("page")]
		private CssParser.page_return page()
		{
			CssParser.page_return page_return = new CssParser.page_return(this);
			page_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token PAGE_SYM");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token CURLY_BEGIN");
			RewriteRuleTokenStream rewriteRuleTokenStream3 = new RewriteRuleTokenStream(this.adaptor, "token SEMICOLON");
			RewriteRuleTokenStream rewriteRuleTokenStream4 = new RewriteRuleTokenStream(this.adaptor, "token CURLY_END");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule pseudo_page");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream2 = new RewriteRuleSubtreeStream(this.adaptor, "rule declaration");
			try
			{
				CommonToken el = (CommonToken)this.Match(this.input, 68, CssParser.Follow._PAGE_SYM_in_page1224);
				if (this.state.failed)
				{
					return page_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream.Add(el);
				}
				int num = 2;
				int num2 = this.input.LA(1);
				if (num2 == 15)
				{
					num = 1;
				}
				int num3 = num;
				if (num3 == 1)
				{
					base.PushFollow(CssParser.Follow._pseudo_page_in_page1226);
					CssParser.pseudo_page_return pseudo_page_return = this.pseudo_page();
					base.PopFollow();
					if (this.state.failed)
					{
						return page_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream.Add(pseudo_page_return.Tree);
					}
				}
				CommonToken el2 = (CommonToken)this.Match(this.input, 18, CssParser.Follow._CURLY_BEGIN_in_page1229);
				if (this.state.failed)
				{
					return page_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream2.Add(el2);
				}
				for (;;)
				{
					int num4 = 2;
					int num5 = this.input.LA(1);
					if ((num5 >= 41 && num5 <= 42) || num5 == 76 || num5 == 84)
					{
						num4 = 1;
					}
					int num6 = num4;
					if (num6 != 1)
					{
						goto IL_282;
					}
					base.PushFollow(CssParser.Follow._declaration_in_page1232);
					CssParser.declaration_return declaration_return = this.declaration();
					base.PopFollow();
					if (this.state.failed)
					{
						break;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream2.Add(declaration_return.Tree);
					}
					int num7 = 2;
					int num8 = this.input.LA(1);
					if (num8 == 79)
					{
						num7 = 1;
					}
					int num9 = num7;
					if (num9 == 1)
					{
						CommonToken el3 = (CommonToken)this.Match(this.input, 79, CssParser.Follow._SEMICOLON_in_page1234);
						if (this.state.failed)
						{
							goto Block_18;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream3.Add(el3);
						}
					}
				}
				return page_return;
				Block_18:
				return page_return;
				IL_282:
				CommonToken el4 = (CommonToken)this.Match(this.input, 19, CssParser.Follow._CURLY_END_in_page1239);
				if (this.state.failed)
				{
					return page_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream4.Add(el4);
				}
				if (this.state.backtracking == 0)
				{
					page_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (page_return != null) ? page_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(163, "PAGE"), obj2);
					if (rewriteRuleSubtreeStream.HasNext)
					{
						this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream.NextTree());
					}
					rewriteRuleSubtreeStream.Reset();
					if (rewriteRuleSubtreeStream2.HasNext)
					{
						object obj3 = this.adaptor.Nil();
						obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(123, "DECLARATIONS"), obj3);
						while (rewriteRuleSubtreeStream2.HasNext)
						{
							this.adaptor.AddChild(obj3, rewriteRuleSubtreeStream2.NextTree());
						}
						rewriteRuleSubtreeStream2.Reset();
						this.adaptor.AddChild(obj2, obj3);
					}
					rewriteRuleSubtreeStream2.Reset();
					this.adaptor.AddChild(obj, obj2);
					page_return.Tree = obj;
				}
				page_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					page_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(page_return.Tree, page_return.Start, page_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				page_return.Tree = this.adaptor.ErrorNode(this.input, page_return.Start, this.input.LT(-1), ex);
			}
			return page_return;
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00068BF0 File Offset: 0x00066DF0
		[GrammarRule("pseudo_page")]
		private CssParser.pseudo_page_return pseudo_page()
		{
			CssParser.pseudo_page_return pseudo_page_return = new CssParser.pseudo_page_return(this);
			pseudo_page_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token COLON");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token IDENT");
			try
			{
				CommonToken el = (CommonToken)this.Match(this.input, 15, CssParser.Follow._COLON_in_pseudo_page1280);
				if (this.state.failed)
				{
					return pseudo_page_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream.Add(el);
				}
				CommonToken el2 = (CommonToken)this.Match(this.input, 41, CssParser.Follow._IDENT_in_pseudo_page1282);
				if (this.state.failed)
				{
					return pseudo_page_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream2.Add(el2);
				}
				if (this.state.backtracking == 0)
				{
					pseudo_page_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (pseudo_page_return != null) ? pseudo_page_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(167, "PSEUDO_PAGE"), obj2);
					this.adaptor.AddChild(obj2, rewriteRuleTokenStream.NextNode());
					this.adaptor.AddChild(obj2, rewriteRuleTokenStream2.NextNode());
					this.adaptor.AddChild(obj, obj2);
					pseudo_page_return.Tree = obj;
				}
				pseudo_page_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					pseudo_page_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(pseudo_page_return.Tree, pseudo_page_return.Start, pseudo_page_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				pseudo_page_return.Tree = this.adaptor.ErrorNode(this.input, pseudo_page_return.Start, this.input.LT(-1), ex);
			}
			return pseudo_page_return;
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00068E34 File Offset: 0x00067034
		[GrammarRule("operator")]
		private CssParser.operator_return @operator()
		{
			CssParser.operator_return operator_return = new CssParser.operator_return(this);
			operator_return.Start = (CommonToken)this.input.LT(1);
			try
			{
				object obj = this.adaptor.Nil();
				CommonToken payload = (CommonToken)this.input.LT(1);
				if (this.input.LA(1) == 16 || this.input.LA(1) == 28 || this.input.LA(1) == 31 || this.input.LA(1) == 84)
				{
					this.input.Consume();
					if (this.state.backtracking == 0)
					{
						this.adaptor.AddChild(obj, this.adaptor.Create(payload));
					}
					this.state.errorRecovery = false;
					this.state.failed = false;
					operator_return.Stop = (CommonToken)this.input.LT(-1);
					if (this.state.backtracking == 0)
					{
						operator_return.Tree = this.adaptor.RulePostProcessing(obj);
						this.adaptor.SetTokenBoundaries(operator_return.Tree, operator_return.Start, operator_return.Stop);
					}
				}
				else
				{
					if (this.state.backtracking > 0)
					{
						this.state.failed = true;
						return operator_return;
					}
					MismatchedSetException ex = new MismatchedSetException(null, this.input);
					throw ex;
				}
			}
			catch (RecognitionException ex2)
			{
				this.ReportError(ex2);
				this.Recover(this.input, ex2);
				operator_return.Tree = this.adaptor.ErrorNode(this.input, operator_return.Start, this.input.LT(-1), ex2);
			}
			return operator_return;
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00068FF8 File Offset: 0x000671F8
		[GrammarRule("unary_operator")]
		private CssParser.unary_operator_return unary_operator()
		{
			CssParser.unary_operator_return unary_operator_return = new CssParser.unary_operator_return(this);
			unary_operator_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token MINUS");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token PLUS");
			try
			{
				int num = this.input.LA(1);
				int num2;
				if (num == 53)
				{
					num2 = 1;
				}
				else if (num == 71)
				{
					num2 = 2;
				}
				else
				{
					if (this.state.backtracking > 0)
					{
						this.state.failed = true;
						return unary_operator_return;
					}
					NoViableAltException ex = new NoViableAltException("", 20, 0, this.input);
					throw ex;
				}
				switch (num2)
				{
				case 1:
				{
					CommonToken el = (CommonToken)this.Match(this.input, 53, CssParser.Follow._MINUS_in_unary_operator1349);
					if (this.state.failed)
					{
						return unary_operator_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream.Add(el);
					}
					if (this.state.backtracking == 0)
					{
						unary_operator_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (unary_operator_return != null) ? unary_operator_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj2 = this.adaptor.Nil();
						obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(185, "UNARY"), obj2);
						this.adaptor.AddChild(obj2, rewriteRuleTokenStream.NextNode());
						this.adaptor.AddChild(obj, obj2);
						unary_operator_return.Tree = obj;
					}
					break;
				}
				case 2:
				{
					CommonToken el2 = (CommonToken)this.Match(this.input, 71, CssParser.Follow._PLUS_in_unary_operator1365);
					if (this.state.failed)
					{
						return unary_operator_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream2.Add(el2);
					}
					if (this.state.backtracking == 0)
					{
						unary_operator_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (unary_operator_return != null) ? unary_operator_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj3 = this.adaptor.Nil();
						obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(185, "UNARY"), obj3);
						this.adaptor.AddChild(obj3, rewriteRuleTokenStream2.NextNode());
						this.adaptor.AddChild(obj, obj3);
						unary_operator_return.Tree = obj;
					}
					break;
				}
				}
				unary_operator_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					unary_operator_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(unary_operator_return.Tree, unary_operator_return.Start, unary_operator_return.Stop);
				}
			}
			catch (RecognitionException ex2)
			{
				this.ReportError(ex2);
				this.Recover(this.input, ex2);
				unary_operator_return.Tree = this.adaptor.ErrorNode(this.input, unary_operator_return.Start, this.input.LT(-1), ex2);
			}
			return unary_operator_return;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00069344 File Offset: 0x00067544
		[GrammarRule("property")]
		private CssParser.property_return property()
		{
			CssParser.property_return property_return = new CssParser.property_return(this);
			property_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token STAR");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token IDENT");
			RewriteRuleTokenStream rewriteRuleTokenStream3 = new RewriteRuleTokenStream(this.adaptor, "token IMPORTANT_COMMENTS");
			RewriteRuleTokenStream rewriteRuleTokenStream4 = new RewriteRuleTokenStream(this.adaptor, "token REPLACEMENTTOKEN");
			try
			{
				int num = this.input.LA(1);
				int num2;
				if (num == 41 || num == 84)
				{
					num2 = 1;
				}
				else if (num == 76)
				{
					num2 = 2;
				}
				else
				{
					if (this.state.backtracking > 0)
					{
						this.state.failed = true;
						return property_return;
					}
					NoViableAltException ex = new NoViableAltException("", 23, 0, this.input);
					throw ex;
				}
				switch (num2)
				{
				case 1:
				{
					int num3 = 2;
					int num4 = this.input.LA(1);
					if (num4 == 84)
					{
						num3 = 1;
					}
					int num5 = num3;
					if (num5 == 1)
					{
						CommonToken el = (CommonToken)this.Match(this.input, 84, CssParser.Follow._STAR_in_property1394);
						if (this.state.failed)
						{
							return property_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream.Add(el);
						}
					}
					CommonToken el2 = (CommonToken)this.Match(this.input, 41, CssParser.Follow._IDENT_in_property1398);
					if (this.state.failed)
					{
						return property_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream2.Add(el2);
					}
					for (;;)
					{
						int num6 = 2;
						int num7 = this.input.LA(1);
						if (num7 == 42)
						{
							num6 = 1;
						}
						int num8 = num6;
						if (num8 != 1)
						{
							goto IL_206;
						}
						CommonToken el3 = (CommonToken)this.Match(this.input, 42, CssParser.Follow._IMPORTANT_COMMENTS_in_property1400);
						if (this.state.failed)
						{
							break;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream3.Add(el3);
						}
					}
					return property_return;
					IL_206:
					if (this.state.backtracking == 0)
					{
						property_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (property_return != null) ? property_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj2 = this.adaptor.Nil();
						obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(164, "PROPERTY"), obj2);
						if (rewriteRuleTokenStream.HasNext)
						{
							this.adaptor.AddChild(obj2, rewriteRuleTokenStream.NextNode());
						}
						rewriteRuleTokenStream.Reset();
						this.adaptor.AddChild(obj2, rewriteRuleTokenStream2.NextNode());
						while (rewriteRuleTokenStream3.HasNext)
						{
							this.adaptor.AddChild(obj2, rewriteRuleTokenStream3.NextNode());
						}
						rewriteRuleTokenStream3.Reset();
						this.adaptor.AddChild(obj, obj2);
						property_return.Tree = obj;
					}
					break;
				}
				case 2:
				{
					CommonToken el4 = (CommonToken)this.Match(this.input, 76, CssParser.Follow._REPLACEMENTTOKEN_in_property1424);
					if (this.state.failed)
					{
						return property_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream4.Add(el4);
					}
					if (this.state.backtracking == 0)
					{
						property_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (property_return != null) ? property_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj3 = this.adaptor.Nil();
						obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(164, "PROPERTY"), obj3);
						this.adaptor.AddChild(obj3, rewriteRuleTokenStream4.NextNode());
						this.adaptor.AddChild(obj, obj3);
						property_return.Tree = obj;
					}
					break;
				}
				}
				property_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					property_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(property_return.Tree, property_return.Start, property_return.Stop);
				}
			}
			catch (RecognitionException ex2)
			{
				this.ReportError(ex2);
				this.Recover(this.input, ex2);
				property_return.Tree = this.adaptor.ErrorNode(this.input, property_return.Start, this.input.LT(-1), ex2);
			}
			return property_return;
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x000697DC File Offset: 0x000679DC
		[GrammarRule("ruleset")]
		private CssParser.ruleset_return ruleset()
		{
			CssParser.ruleset_return ruleset_return = new CssParser.ruleset_return(this);
			ruleset_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token CURLY_BEGIN");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token SEMICOLON");
			RewriteRuleTokenStream rewriteRuleTokenStream3 = new RewriteRuleTokenStream(this.adaptor, "token IMPORTANT_COMMENTS");
			RewriteRuleTokenStream rewriteRuleTokenStream4 = new RewriteRuleTokenStream(this.adaptor, "token CURLY_END");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule selectors_group");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream2 = new RewriteRuleSubtreeStream(this.adaptor, "rule declaration");
			try
			{
				base.PushFollow(CssParser.Follow._selectors_group_in_ruleset1454);
				CssParser.selectors_group_return selectors_group_return = this.selectors_group();
				base.PopFollow();
				if (this.state.failed)
				{
					return ruleset_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleSubtreeStream.Add(selectors_group_return.Tree);
				}
				CommonToken el = (CommonToken)this.Match(this.input, 18, CssParser.Follow._CURLY_BEGIN_in_ruleset1460);
				if (this.state.failed)
				{
					return ruleset_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream.Add(el);
				}
				for (;;)
				{
					int num = 2;
					try
					{
						num = this.dfa25.Predict(this.input);
					}
					catch (NoViableAltException)
					{
						throw;
					}
					int num2 = num;
					if (num2 != 1)
					{
						break;
					}
					base.PushFollow(CssParser.Follow._declaration_in_ruleset1467);
					CssParser.declaration_return declaration_return = this.declaration();
					base.PopFollow();
					if (this.state.failed)
					{
						goto Block_9;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream2.Add(declaration_return.Tree);
					}
					int num3 = 2;
					int num4 = this.input.LA(1);
					if (num4 == 79)
					{
						num3 = 1;
					}
					int num5 = num3;
					if (num5 == 1)
					{
						CommonToken el2 = (CommonToken)this.Match(this.input, 79, CssParser.Follow._SEMICOLON_in_ruleset1469);
						if (this.state.failed)
						{
							goto Block_13;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream2.Add(el2);
						}
					}
				}
				for (;;)
				{
					int num6 = 2;
					int num7 = this.input.LA(1);
					if (num7 == 42)
					{
						num6 = 1;
					}
					int num8 = num6;
					if (num8 != 1)
					{
						goto IL_273;
					}
					CommonToken el3 = (CommonToken)this.Match(this.input, 42, CssParser.Follow._IMPORTANT_COMMENTS_in_ruleset1475);
					if (this.state.failed)
					{
						break;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream3.Add(el3);
					}
				}
				return ruleset_return;
				IL_273:
				CommonToken el4 = (CommonToken)this.Match(this.input, 19, CssParser.Follow._CURLY_END_in_ruleset1482);
				if (this.state.failed)
				{
					return ruleset_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream4.Add(el4);
				}
				if (this.state.backtracking == 0)
				{
					ruleset_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (ruleset_return != null) ? ruleset_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(171, "RULESET"), obj2);
					this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream.NextTree());
					if (rewriteRuleSubtreeStream2.HasNext)
					{
						object obj3 = this.adaptor.Nil();
						obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(123, "DECLARATIONS"), obj3);
						while (rewriteRuleSubtreeStream2.HasNext)
						{
							this.adaptor.AddChild(obj3, rewriteRuleSubtreeStream2.NextTree());
						}
						rewriteRuleSubtreeStream2.Reset();
						this.adaptor.AddChild(obj2, obj3);
					}
					rewriteRuleSubtreeStream2.Reset();
					while (rewriteRuleTokenStream3.HasNext)
					{
						this.adaptor.AddChild(obj2, rewriteRuleTokenStream3.NextNode());
					}
					rewriteRuleTokenStream3.Reset();
					this.adaptor.AddChild(obj, obj2);
					ruleset_return.Tree = obj;
				}
				ruleset_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					ruleset_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(ruleset_return.Tree, ruleset_return.Start, ruleset_return.Stop);
				}
				return ruleset_return;
				Block_9:
				return ruleset_return;
				Block_13:
				return ruleset_return;
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				ruleset_return.Tree = this.adaptor.ErrorNode(this.input, ruleset_return.Start, this.input.LT(-1), ex);
			}
			return ruleset_return;
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x00069CA8 File Offset: 0x00067EA8
		[GrammarRule("selectors_group")]
		private CssParser.selectors_group_return selectors_group()
		{
			CssParser.selectors_group_return selectors_group_return = new CssParser.selectors_group_return(this);
			selectors_group_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token COMMA");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule selector");
			try
			{
				base.PushFollow(CssParser.Follow._selector_in_selectors_group1523);
				CssParser.selector_return selector_return = this.selector();
				base.PopFollow();
				if (this.state.failed)
				{
					return selectors_group_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleSubtreeStream.Add(selector_return.Tree);
				}
				for (;;)
				{
					int num = 2;
					int num2 = this.input.LA(1);
					if (num2 == 16)
					{
						num = 1;
					}
					int num3 = num;
					if (num3 != 1)
					{
						goto IL_14C;
					}
					CommonToken el = (CommonToken)this.Match(this.input, 16, CssParser.Follow._COMMA_in_selectors_group1526);
					if (this.state.failed)
					{
						break;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream.Add(el);
					}
					base.PushFollow(CssParser.Follow._selector_in_selectors_group1528);
					CssParser.selector_return selector_return2 = this.selector();
					base.PopFollow();
					if (this.state.failed)
					{
						goto Block_9;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream.Add(selector_return2.Tree);
					}
				}
				return selectors_group_return;
				Block_9:
				return selectors_group_return;
				IL_14C:
				if (this.state.backtracking == 0)
				{
					selectors_group_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (selectors_group_return != null) ? selectors_group_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(174, "SELECTORS_GROUP"), obj2);
					while (rewriteRuleSubtreeStream.HasNext)
					{
						this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream.NextTree());
					}
					rewriteRuleSubtreeStream.Reset();
					this.adaptor.AddChild(obj, obj2);
					selectors_group_return.Tree = obj;
				}
				selectors_group_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					selectors_group_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(selectors_group_return.Tree, selectors_group_return.Start, selectors_group_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				selectors_group_return.Tree = this.adaptor.ErrorNode(this.input, selectors_group_return.Start, this.input.LT(-1), ex);
			}
			return selectors_group_return;
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00069F68 File Offset: 0x00068168
		[GrammarRule("selector")]
		private CssParser.selector_return selector()
		{
			CssParser.selector_return selector_return = new CssParser.selector_return(this);
			selector_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule simple_selector_sequence");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream2 = new RewriteRuleSubtreeStream(this.adaptor, "rule combinator_simple_selector_sequence");
			try
			{
				base.PushFollow(CssParser.Follow._simple_selector_sequence_in_selector1559);
				CssParser.simple_selector_sequence_return simple_selector_sequence_return = this.simple_selector_sequence();
				base.PopFollow();
				if (this.state.failed)
				{
					return selector_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleSubtreeStream.Add(simple_selector_sequence_return.Tree);
				}
				for (;;)
				{
					int num = 2;
					int num2 = this.input.LA(1);
					if (num2 == 7 || (num2 >= 14 && num2 <= 15) || (num2 == 35 || num2 == 38 || num2 == 41 || (num2 >= 70 && num2 <= 71)) || num2 == 76 || num2 == 82 || num2 == 84 || num2 == 89 || num2 == 105)
					{
						num = 1;
					}
					int num3 = num;
					if (num3 != 1)
					{
						goto IL_148;
					}
					base.PushFollow(CssParser.Follow._combinator_simple_selector_sequence_in_selector1562);
					CssParser.combinator_simple_selector_sequence_return combinator_simple_selector_sequence_return = this.combinator_simple_selector_sequence();
					base.PopFollow();
					if (this.state.failed)
					{
						break;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream2.Add(combinator_simple_selector_sequence_return.Tree);
					}
				}
				return selector_return;
				IL_148:
				if (this.state.backtracking == 0)
				{
					selector_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (selector_return != null) ? selector_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(173, "SELECTOR"), obj2);
					this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream.NextTree());
					if (rewriteRuleSubtreeStream2.HasNext)
					{
						object obj3 = this.adaptor.Nil();
						obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(121, "COMBINATOR_SIMPLE_SELECTOR_SEQUENCES"), obj3);
						while (rewriteRuleSubtreeStream2.HasNext)
						{
							this.adaptor.AddChild(obj3, rewriteRuleSubtreeStream2.NextTree());
						}
						rewriteRuleSubtreeStream2.Reset();
						this.adaptor.AddChild(obj2, obj3);
					}
					rewriteRuleSubtreeStream2.Reset();
					this.adaptor.AddChild(obj, obj2);
					selector_return.Tree = obj;
				}
				selector_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					selector_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(selector_return.Tree, selector_return.Start, selector_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				selector_return.Tree = this.adaptor.ErrorNode(this.input, selector_return.Start, this.input.LT(-1), ex);
			}
			return selector_return;
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0006A284 File Offset: 0x00068484
		[GrammarRule("combinator_simple_selector_sequence")]
		private CssParser.combinator_simple_selector_sequence_return combinator_simple_selector_sequence()
		{
			CssParser.combinator_simple_selector_sequence_return combinator_simple_selector_sequence_return = new CssParser.combinator_simple_selector_sequence_return(this);
			combinator_simple_selector_sequence_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule combinator");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream2 = new RewriteRuleSubtreeStream(this.adaptor, "rule simple_selector_sequence");
			try
			{
				base.PushFollow(CssParser.Follow._combinator_in_combinator_simple_selector_sequence1601);
				CssParser.combinator_return combinator_return = this.combinator();
				base.PopFollow();
				if (this.state.failed)
				{
					return combinator_simple_selector_sequence_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleSubtreeStream.Add(combinator_return.Tree);
				}
				base.PushFollow(CssParser.Follow._simple_selector_sequence_in_combinator_simple_selector_sequence1603);
				CssParser.simple_selector_sequence_return simple_selector_sequence_return = this.simple_selector_sequence();
				base.PopFollow();
				if (this.state.failed)
				{
					return combinator_simple_selector_sequence_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleSubtreeStream2.Add(simple_selector_sequence_return.Tree);
				}
				if (this.state.backtracking == 0)
				{
					combinator_simple_selector_sequence_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (combinator_simple_selector_sequence_return != null) ? combinator_simple_selector_sequence_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(120, "COMBINATOR_SIMPLE_SELECTOR"), obj2);
					this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream.NextTree());
					this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream2.NextTree());
					this.adaptor.AddChild(obj, obj2);
					combinator_simple_selector_sequence_return.Tree = obj;
				}
				combinator_simple_selector_sequence_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					combinator_simple_selector_sequence_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(combinator_simple_selector_sequence_return.Tree, combinator_simple_selector_sequence_return.Start, combinator_simple_selector_sequence_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				combinator_simple_selector_sequence_return.Tree = this.adaptor.ErrorNode(this.input, combinator_simple_selector_sequence_return.Start, this.input.LT(-1), ex);
			}
			return combinator_simple_selector_sequence_return;
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0006A4CC File Offset: 0x000686CC
		[GrammarRule("combinator")]
		private CssParser.combinator_return combinator()
		{
			CssParser.combinator_return combinator_return = new CssParser.combinator_return(this);
			combinator_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			CommonToken commonToken = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token PLUS");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token GREATER");
			RewriteRuleTokenStream rewriteRuleTokenStream3 = new RewriteRuleTokenStream(this.adaptor, "token TILDE");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule whitespace");
			try
			{
				int num = this.input.LA(1);
				int num2;
				if (num == 35 || num == 71 || num == 89)
				{
					num2 = 1;
				}
				else if (num == 7 || (num >= 14 && num <= 15) || num == 38 || num == 41 || num == 70 || num == 76 || num == 82 || num == 84 || num == 105)
				{
					num2 = 2;
				}
				else
				{
					if (this.state.backtracking > 0)
					{
						this.state.failed = true;
						return combinator_return;
					}
					NoViableAltException ex = new NoViableAltException("", 30, 0, this.input);
					throw ex;
				}
				switch (num2)
				{
				case 1:
				{
					int num3 = this.input.LA(1);
					int num4;
					if (num3 != 35)
					{
						if (num3 != 71)
						{
							if (num3 != 89)
							{
								if (this.state.backtracking > 0)
								{
									this.state.failed = true;
									return combinator_return;
								}
								NoViableAltException ex2 = new NoViableAltException("", 29, 0, this.input);
								throw ex2;
							}
							else
							{
								num4 = 3;
							}
						}
						else
						{
							num4 = 1;
						}
					}
					else
					{
						num4 = 2;
					}
					switch (num4)
					{
					case 1:
						commonToken = (CommonToken)this.Match(this.input, 71, CssParser.Follow._PLUS_in_combinator1644);
						if (this.state.failed)
						{
							return combinator_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream.Add(commonToken);
						}
						break;
					case 2:
						commonToken = (CommonToken)this.Match(this.input, 35, CssParser.Follow._GREATER_in_combinator1655);
						if (this.state.failed)
						{
							return combinator_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream2.Add(commonToken);
						}
						break;
					case 3:
						commonToken = (CommonToken)this.Match(this.input, 89, CssParser.Follow._TILDE_in_combinator1666);
						if (this.state.failed)
						{
							return combinator_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream3.Add(commonToken);
						}
						break;
					}
					if (this.state.backtracking == 0)
					{
						combinator_return.Tree = obj;
						RewriteRuleTokenStream rewriteRuleTokenStream4 = new RewriteRuleTokenStream(this.adaptor, "token combinatorValue", commonToken);
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (combinator_return != null) ? combinator_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj2 = this.adaptor.Nil();
						obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(119, "COMBINATOR"), obj2);
						this.adaptor.AddChild(obj2, rewriteRuleTokenStream4.NextNode());
						this.adaptor.AddChild(obj, obj2);
						combinator_return.Tree = obj;
					}
					break;
				}
				case 2:
				{
					base.PushFollow(CssParser.Follow._whitespace_in_combinator1687);
					CssParser.whitespace_return whitespace_return = this.whitespace();
					base.PopFollow();
					if (this.state.failed)
					{
						return combinator_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream.Add(whitespace_return.Tree);
					}
					if (this.state.backtracking == 0)
					{
						combinator_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (combinator_return != null) ? combinator_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj3 = this.adaptor.Nil();
						obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(119, "COMBINATOR"), obj3);
						this.adaptor.AddChild(obj3, rewriteRuleSubtreeStream.NextTree());
						this.adaptor.AddChild(obj, obj3);
						combinator_return.Tree = obj;
					}
					break;
				}
				}
				combinator_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					combinator_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(combinator_return.Tree, combinator_return.Start, combinator_return.Stop);
				}
			}
			catch (RecognitionException ex3)
			{
				this.ReportError(ex3);
				this.Recover(this.input, ex3);
				combinator_return.Tree = this.adaptor.ErrorNode(this.input, combinator_return.Start, this.input.LT(-1), ex3);
			}
			return combinator_return;
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0006A9A8 File Offset: 0x00068BA8
		[GrammarRule("whitespace")]
		private CssParser.whitespace_return whitespace()
		{
			CssParser.whitespace_return whitespace_return = new CssParser.whitespace_return(this);
			whitespace_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			CommonToken oneElement = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token WS");
			try
			{
				int num = 2;
				int num2 = this.input.LA(1);
				if (num2 == 105)
				{
					this.input.LA(2);
					if (this.EvaluatePredicate(new Action(this.synpred1_CssParser_fragment)))
					{
						num = 1;
					}
				}
				int num3 = num;
				if (num3 == 1)
				{
					CommonToken el = (CommonToken)this.Match(this.input, 105, CssParser.Follow._WS_in_whitespace1728);
					if (this.state.failed)
					{
						return whitespace_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream.Add(el);
					}
				}
				if (this.state.backtracking == 0)
				{
					oneElement = this.GetWhitespaceToken();
				}
				if (this.state.backtracking == 0)
				{
					whitespace_return.Tree = obj;
					RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token ws", oneElement);
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (whitespace_return != null) ? whitespace_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(190, "WHITESPACE"), obj2);
					this.adaptor.AddChild(obj2, rewriteRuleTokenStream2.NextNode());
					this.adaptor.AddChild(obj, obj2);
					whitespace_return.Tree = obj;
				}
				whitespace_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					whitespace_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(whitespace_return.Tree, whitespace_return.Start, whitespace_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				whitespace_return.Tree = this.adaptor.ErrorNode(this.input, whitespace_return.Start, this.input.LT(-1), ex);
			}
			return whitespace_return;
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0006ABF0 File Offset: 0x00068DF0
		[GrammarRule("simple_selector_sequence")]
		private CssParser.simple_selector_sequence_return simple_selector_sequence()
		{
			CssParser.simple_selector_sequence_return simple_selector_sequence_return = new CssParser.simple_selector_sequence_return(this);
			simple_selector_sequence_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule universal");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream2 = new RewriteRuleSubtreeStream(this.adaptor, "rule type_selector");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream3 = new RewriteRuleSubtreeStream(this.adaptor, "rule whitespace");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream4 = new RewriteRuleSubtreeStream(this.adaptor, "rule hashclassatnameattribpseudonegation");
			try
			{
				int num = this.input.LA(1);
				int num2;
				if (num == 41 || num == 70 || num == 84)
				{
					num2 = 1;
				}
				else if (num == 76 && this.EvaluatePredicate(new Action(this.synpred5_CssParser_fragment)))
				{
					num2 = 2;
				}
				else if (num == 38 && this.EvaluatePredicate(new Action(this.synpred5_CssParser_fragment)))
				{
					num2 = 2;
				}
				else if (num == 14 && this.EvaluatePredicate(new Action(this.synpred5_CssParser_fragment)))
				{
					num2 = 2;
				}
				else if (num == 7 && this.EvaluatePredicate(new Action(this.synpred5_CssParser_fragment)))
				{
					num2 = 2;
				}
				else if (num == 82 && this.EvaluatePredicate(new Action(this.synpred5_CssParser_fragment)))
				{
					num2 = 2;
				}
				else if (num == 15 && this.EvaluatePredicate(new Action(this.synpred5_CssParser_fragment)))
				{
					num2 = 2;
				}
				else
				{
					if (this.state.backtracking > 0)
					{
						this.state.failed = true;
						return simple_selector_sequence_return;
					}
					NoViableAltException ex = new NoViableAltException("", 34, 0, this.input);
					throw ex;
				}
				switch (num2)
				{
				case 1:
				{
					int num3 = this.input.LA(1);
					int num4;
					if (num3 != 41)
					{
						if (num3 != 70)
						{
							if (num3 != 84)
							{
								if (this.state.backtracking > 0)
								{
									this.state.failed = true;
									return simple_selector_sequence_return;
								}
								NoViableAltException ex2 = new NoViableAltException("", 32, 0, this.input);
								throw ex2;
							}
							else
							{
								this.input.LA(2);
								if (this.EvaluatePredicate(new Action(this.synpred2_CssParser_fragment)))
								{
									num4 = 1;
								}
								else if (this.EvaluatePredicate(new Action(this.synpred3_CssParser_fragment)))
								{
									num4 = 2;
								}
								else
								{
									if (this.state.backtracking > 0)
									{
										this.state.failed = true;
										return simple_selector_sequence_return;
									}
									NoViableAltException ex3 = new NoViableAltException("", 32, 2, this.input);
									throw ex3;
								}
							}
						}
						else
						{
							int num5 = this.input.LA(2);
							if (num5 == 84)
							{
								this.input.LA(3);
								if (this.EvaluatePredicate(new Action(this.synpred2_CssParser_fragment)))
								{
									num4 = 1;
								}
								else if (this.EvaluatePredicate(new Action(this.synpred3_CssParser_fragment)))
								{
									num4 = 2;
								}
								else
								{
									if (this.state.backtracking > 0)
									{
										this.state.failed = true;
										return simple_selector_sequence_return;
									}
									NoViableAltException ex4 = new NoViableAltException("", 32, 6, this.input);
									throw ex4;
								}
							}
							else if (num5 == 41 && this.EvaluatePredicate(new Action(this.synpred3_CssParser_fragment)))
							{
								num4 = 2;
							}
							else
							{
								if (this.state.backtracking > 0)
								{
									this.state.failed = true;
									return simple_selector_sequence_return;
								}
								NoViableAltException ex5 = new NoViableAltException("", 32, 3, this.input);
								throw ex5;
							}
						}
					}
					else
					{
						this.input.LA(2);
						if (this.EvaluatePredicate(new Action(this.synpred2_CssParser_fragment)))
						{
							num4 = 1;
						}
						else if (this.EvaluatePredicate(new Action(this.synpred3_CssParser_fragment)))
						{
							num4 = 2;
						}
						else
						{
							if (this.state.backtracking > 0)
							{
								this.state.failed = true;
								return simple_selector_sequence_return;
							}
							NoViableAltException ex6 = new NoViableAltException("", 32, 1, this.input);
							throw ex6;
						}
					}
					switch (num4)
					{
					case 1:
					{
						base.PushFollow(CssParser.Follow._universal_in_simple_selector_sequence1783);
						CssParser.universal_return universal_return = this.universal();
						base.PopFollow();
						if (this.state.failed)
						{
							return simple_selector_sequence_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleSubtreeStream.Add(universal_return.Tree);
						}
						break;
					}
					case 2:
					{
						base.PushFollow(CssParser.Follow._type_selector_in_simple_selector_sequence1793);
						CssParser.type_selector_return type_selector_return = this.type_selector();
						base.PopFollow();
						if (this.state.failed)
						{
							return simple_selector_sequence_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleSubtreeStream2.Add(type_selector_return.Tree);
						}
						break;
					}
					}
					base.PushFollow(CssParser.Follow._whitespace_in_simple_selector_sequence1797);
					CssParser.whitespace_return whitespace_return = this.whitespace();
					base.PopFollow();
					if (this.state.failed)
					{
						return simple_selector_sequence_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream3.Add(whitespace_return.Tree);
					}
					int num6 = 2;
					try
					{
						num6 = this.dfa33.Predict(this.input);
					}
					catch (NoViableAltException)
					{
						throw;
					}
					int num7 = num6;
					if (num7 == 1)
					{
						base.PushFollow(CssParser.Follow._hashclassatnameattribpseudonegation_in_simple_selector_sequence1806);
						CssParser.hashclassatnameattribpseudonegation_return hashclassatnameattribpseudonegation_return = this.hashclassatnameattribpseudonegation();
						base.PopFollow();
						if (this.state.failed)
						{
							return simple_selector_sequence_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleSubtreeStream4.Add(hashclassatnameattribpseudonegation_return.Tree);
						}
					}
					if (this.state.backtracking == 0)
					{
						simple_selector_sequence_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (simple_selector_sequence_return != null) ? simple_selector_sequence_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj2 = this.adaptor.Nil();
						obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(177, "SIMPLE_SELECTOR_SEQUENCE"), obj2);
						if (rewriteRuleSubtreeStream2.HasNext)
						{
							this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream2.NextTree());
						}
						rewriteRuleSubtreeStream2.Reset();
						if (rewriteRuleSubtreeStream.HasNext)
						{
							this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream.NextTree());
						}
						rewriteRuleSubtreeStream.Reset();
						if (rewriteRuleSubtreeStream3.HasNext)
						{
							this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream3.NextTree());
						}
						rewriteRuleSubtreeStream3.Reset();
						if (rewriteRuleSubtreeStream4.HasNext)
						{
							object obj3 = this.adaptor.Nil();
							obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(134, "HASHCLASSATNAMEATTRIBPSEUDONEGATIONNODES"), obj3);
							this.adaptor.AddChild(obj3, rewriteRuleSubtreeStream4.NextTree());
							this.adaptor.AddChild(obj2, obj3);
						}
						rewriteRuleSubtreeStream4.Reset();
						this.adaptor.AddChild(obj, obj2);
						simple_selector_sequence_return.Tree = obj;
					}
					break;
				}
				case 2:
				{
					base.PushFollow(CssParser.Follow._hashclassatnameattribpseudonegation_in_simple_selector_sequence1848);
					CssParser.hashclassatnameattribpseudonegation_return hashclassatnameattribpseudonegation_return2 = this.hashclassatnameattribpseudonegation();
					base.PopFollow();
					if (this.state.failed)
					{
						return simple_selector_sequence_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream4.Add(hashclassatnameattribpseudonegation_return2.Tree);
					}
					if (this.state.backtracking == 0)
					{
						simple_selector_sequence_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (simple_selector_sequence_return != null) ? simple_selector_sequence_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj4 = this.adaptor.Nil();
						obj4 = this.adaptor.BecomeRoot(this.adaptor.Create(177, "SIMPLE_SELECTOR_SEQUENCE"), obj4);
						object obj5 = this.adaptor.Nil();
						obj5 = this.adaptor.BecomeRoot(this.adaptor.Create(134, "HASHCLASSATNAMEATTRIBPSEUDONEGATIONNODES"), obj5);
						this.adaptor.AddChild(obj5, rewriteRuleSubtreeStream4.NextTree());
						this.adaptor.AddChild(obj4, obj5);
						this.adaptor.AddChild(obj, obj4);
						simple_selector_sequence_return.Tree = obj;
					}
					break;
				}
				}
				simple_selector_sequence_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					simple_selector_sequence_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(simple_selector_sequence_return.Tree, simple_selector_sequence_return.Start, simple_selector_sequence_return.Stop);
				}
			}
			catch (RecognitionException ex7)
			{
				this.ReportError(ex7);
				this.Recover(this.input, ex7);
				simple_selector_sequence_return.Tree = this.adaptor.ErrorNode(this.input, simple_selector_sequence_return.Start, this.input.LT(-1), ex7);
			}
			return simple_selector_sequence_return;
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0006B4B8 File Offset: 0x000696B8
		[GrammarRule("hashclassatnameattribpseudonegation")]
		private CssParser.hashclassatnameattribpseudonegation_return hashclassatnameattribpseudonegation()
		{
			CssParser.hashclassatnameattribpseudonegation_return hashclassatnameattribpseudonegation_return = new CssParser.hashclassatnameattribpseudonegation_return(this);
			hashclassatnameattribpseudonegation_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token REPLACEMENTTOKEN");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule hash");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream2 = new RewriteRuleSubtreeStream(this.adaptor, "rule class");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream3 = new RewriteRuleSubtreeStream(this.adaptor, "rule atname");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream4 = new RewriteRuleSubtreeStream(this.adaptor, "rule attrib");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream5 = new RewriteRuleSubtreeStream(this.adaptor, "rule pseudo");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream6 = new RewriteRuleSubtreeStream(this.adaptor, "rule negation");
			try
			{
				int num = this.input.LA(1);
				int num2;
				if (num <= 15)
				{
					if (num == 7)
					{
						num2 = 4;
						goto IL_1D6;
					}
					switch (num)
					{
					case 14:
						num2 = 3;
						goto IL_1D6;
					case 15:
					{
						int num3 = this.input.LA(2);
						if (num3 == 63)
						{
							num2 = 7;
							goto IL_1D6;
						}
						if (num3 == 15 || num3 == 33 || num3 == 41 || num3 == 55 || num3 == 91)
						{
							num2 = 6;
							goto IL_1D6;
						}
						if (this.state.backtracking > 0)
						{
							this.state.failed = true;
							return hashclassatnameattribpseudonegation_return;
						}
						NoViableAltException ex = new NoViableAltException("", 35, 6, this.input);
						throw ex;
					}
					}
				}
				else
				{
					if (num == 38)
					{
						num2 = 2;
						goto IL_1D6;
					}
					if (num == 76)
					{
						num2 = 1;
						goto IL_1D6;
					}
					if (num == 82)
					{
						num2 = 5;
						goto IL_1D6;
					}
				}
				if (this.state.backtracking > 0)
				{
					this.state.failed = true;
					return hashclassatnameattribpseudonegation_return;
				}
				NoViableAltException ex2 = new NoViableAltException("", 35, 0, this.input);
				throw ex2;
				IL_1D6:
				switch (num2)
				{
				case 1:
				{
					CommonToken el = (CommonToken)this.Match(this.input, 76, CssParser.Follow._REPLACEMENTTOKEN_in_hashclassatnameattribpseudonegation1878);
					if (this.state.failed)
					{
						return hashclassatnameattribpseudonegation_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream.Add(el);
					}
					if (this.state.backtracking == 0)
					{
						hashclassatnameattribpseudonegation_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (hashclassatnameattribpseudonegation_return != null) ? hashclassatnameattribpseudonegation_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj2 = this.adaptor.Nil();
						obj2 = this.adaptor.BecomeRoot(rewriteRuleTokenStream.NextNode(), obj2);
						object obj3 = this.adaptor.Nil();
						obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(170, "REPLACEMENTTOKENIDENTIFIER"), obj3);
						this.adaptor.AddChild(obj3, rewriteRuleTokenStream.NextNode());
						this.adaptor.AddChild(obj2, obj3);
						this.adaptor.AddChild(obj, obj2);
						hashclassatnameattribpseudonegation_return.Tree = obj;
					}
					break;
				}
				case 2:
				{
					base.PushFollow(CssParser.Follow._hash_in_hashclassatnameattribpseudonegation1902);
					CssParser.hash_return hash_return = this.hash();
					base.PopFollow();
					if (this.state.failed)
					{
						return hashclassatnameattribpseudonegation_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream.Add(hash_return.Tree);
					}
					if (this.state.backtracking == 0)
					{
						hashclassatnameattribpseudonegation_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (hashclassatnameattribpseudonegation_return != null) ? hashclassatnameattribpseudonegation_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj4 = this.adaptor.Nil();
						obj4 = this.adaptor.BecomeRoot(this.adaptor.Create(133, "HASHCLASSATNAMEATTRIBPSEUDONEGATION"), obj4);
						this.adaptor.AddChild(obj4, rewriteRuleSubtreeStream.NextTree());
						this.adaptor.AddChild(obj, obj4);
						hashclassatnameattribpseudonegation_return.Tree = obj;
					}
					break;
				}
				case 3:
				{
					base.PushFollow(CssParser.Follow._class_in_hashclassatnameattribpseudonegation1922);
					CssParser.class_return class_return = this.@class();
					base.PopFollow();
					if (this.state.failed)
					{
						return hashclassatnameattribpseudonegation_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream2.Add(class_return.Tree);
					}
					if (this.state.backtracking == 0)
					{
						hashclassatnameattribpseudonegation_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (hashclassatnameattribpseudonegation_return != null) ? hashclassatnameattribpseudonegation_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj5 = this.adaptor.Nil();
						obj5 = this.adaptor.BecomeRoot(this.adaptor.Create(133, "HASHCLASSATNAMEATTRIBPSEUDONEGATION"), obj5);
						this.adaptor.AddChild(obj5, rewriteRuleSubtreeStream2.NextTree());
						this.adaptor.AddChild(obj, obj5);
						hashclassatnameattribpseudonegation_return.Tree = obj;
					}
					break;
				}
				case 4:
				{
					base.PushFollow(CssParser.Follow._atname_in_hashclassatnameattribpseudonegation1942);
					CssParser.atname_return atname_return = this.atname();
					base.PopFollow();
					if (this.state.failed)
					{
						return hashclassatnameattribpseudonegation_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream3.Add(atname_return.Tree);
					}
					if (this.state.backtracking == 0)
					{
						hashclassatnameattribpseudonegation_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (hashclassatnameattribpseudonegation_return != null) ? hashclassatnameattribpseudonegation_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj6 = this.adaptor.Nil();
						obj6 = this.adaptor.BecomeRoot(this.adaptor.Create(133, "HASHCLASSATNAMEATTRIBPSEUDONEGATION"), obj6);
						this.adaptor.AddChild(obj6, rewriteRuleSubtreeStream3.NextTree());
						this.adaptor.AddChild(obj, obj6);
						hashclassatnameattribpseudonegation_return.Tree = obj;
					}
					break;
				}
				case 5:
				{
					base.PushFollow(CssParser.Follow._attrib_in_hashclassatnameattribpseudonegation1962);
					CssParser.attrib_return attrib_return = this.attrib();
					base.PopFollow();
					if (this.state.failed)
					{
						return hashclassatnameattribpseudonegation_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream4.Add(attrib_return.Tree);
					}
					if (this.state.backtracking == 0)
					{
						hashclassatnameattribpseudonegation_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (hashclassatnameattribpseudonegation_return != null) ? hashclassatnameattribpseudonegation_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj7 = this.adaptor.Nil();
						obj7 = this.adaptor.BecomeRoot(this.adaptor.Create(133, "HASHCLASSATNAMEATTRIBPSEUDONEGATION"), obj7);
						this.adaptor.AddChild(obj7, rewriteRuleSubtreeStream4.NextTree());
						this.adaptor.AddChild(obj, obj7);
						hashclassatnameattribpseudonegation_return.Tree = obj;
					}
					break;
				}
				case 6:
				{
					base.PushFollow(CssParser.Follow._pseudo_in_hashclassatnameattribpseudonegation1982);
					CssParser.pseudo_return pseudo_return = this.pseudo();
					base.PopFollow();
					if (this.state.failed)
					{
						return hashclassatnameattribpseudonegation_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream5.Add(pseudo_return.Tree);
					}
					if (this.state.backtracking == 0)
					{
						hashclassatnameattribpseudonegation_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (hashclassatnameattribpseudonegation_return != null) ? hashclassatnameattribpseudonegation_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj8 = this.adaptor.Nil();
						obj8 = this.adaptor.BecomeRoot(this.adaptor.Create(133, "HASHCLASSATNAMEATTRIBPSEUDONEGATION"), obj8);
						this.adaptor.AddChild(obj8, rewriteRuleSubtreeStream5.NextTree());
						this.adaptor.AddChild(obj, obj8);
						hashclassatnameattribpseudonegation_return.Tree = obj;
					}
					break;
				}
				case 7:
				{
					base.PushFollow(CssParser.Follow._negation_in_hashclassatnameattribpseudonegation2002);
					CssParser.negation_return negation_return = this.negation();
					base.PopFollow();
					if (this.state.failed)
					{
						return hashclassatnameattribpseudonegation_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream6.Add(negation_return.Tree);
					}
					if (this.state.backtracking == 0)
					{
						hashclassatnameattribpseudonegation_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (hashclassatnameattribpseudonegation_return != null) ? hashclassatnameattribpseudonegation_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj9 = this.adaptor.Nil();
						obj9 = this.adaptor.BecomeRoot(this.adaptor.Create(133, "HASHCLASSATNAMEATTRIBPSEUDONEGATION"), obj9);
						this.adaptor.AddChild(obj9, rewriteRuleSubtreeStream6.NextTree());
						this.adaptor.AddChild(obj, obj9);
						hashclassatnameattribpseudonegation_return.Tree = obj;
					}
					break;
				}
				}
				hashclassatnameattribpseudonegation_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					hashclassatnameattribpseudonegation_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(hashclassatnameattribpseudonegation_return.Tree, hashclassatnameattribpseudonegation_return.Start, hashclassatnameattribpseudonegation_return.Stop);
				}
			}
			catch (RecognitionException ex3)
			{
				this.ReportError(ex3);
				this.Recover(this.input, ex3);
				hashclassatnameattribpseudonegation_return.Tree = this.adaptor.ErrorNode(this.input, hashclassatnameattribpseudonegation_return.Start, this.input.LT(-1), ex3);
			}
			return hashclassatnameattribpseudonegation_return;
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0006BE00 File Offset: 0x0006A000
		[GrammarRule("type_selector")]
		private CssParser.type_selector_return type_selector()
		{
			CssParser.type_selector_return type_selector_return = new CssParser.type_selector_return(this);
			type_selector_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule selector_namespace_prefix");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream2 = new RewriteRuleSubtreeStream(this.adaptor, "rule element_name");
			try
			{
				int num = 2;
				int num2 = this.input.LA(1);
				if (num2 == 41)
				{
					this.input.LA(2);
					if (this.EvaluatePredicate(new Action(this.synpred6_CssParser_fragment)))
					{
						num = 1;
					}
				}
				else if (num2 == 84)
				{
					this.input.LA(2);
					if (this.EvaluatePredicate(new Action(this.synpred6_CssParser_fragment)))
					{
						num = 1;
					}
				}
				else if (num2 == 70 && this.EvaluatePredicate(new Action(this.synpred6_CssParser_fragment)))
				{
					num = 1;
				}
				int num3 = num;
				if (num3 == 1)
				{
					base.PushFollow(CssParser.Follow._selector_namespace_prefix_in_type_selector2047);
					CssParser.selector_namespace_prefix_return selector_namespace_prefix_return = this.selector_namespace_prefix();
					base.PopFollow();
					if (this.state.failed)
					{
						return type_selector_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream.Add(selector_namespace_prefix_return.Tree);
					}
				}
				base.PushFollow(CssParser.Follow._element_name_in_type_selector2051);
				CssParser.element_name_return element_name_return = this.element_name();
				base.PopFollow();
				if (this.state.failed)
				{
					return type_selector_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleSubtreeStream2.Add(element_name_return.Tree);
				}
				if (this.state.backtracking == 0)
				{
					type_selector_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (type_selector_return != null) ? type_selector_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(184, "TYPE_SELECTOR"), obj2);
					if (rewriteRuleSubtreeStream.HasNext)
					{
						this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream.NextTree());
					}
					rewriteRuleSubtreeStream.Reset();
					this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream2.NextTree());
					this.adaptor.AddChild(obj, obj2);
					type_selector_return.Tree = obj;
				}
				type_selector_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					type_selector_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(type_selector_return.Tree, type_selector_return.Start, type_selector_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				type_selector_return.Tree = this.adaptor.ErrorNode(this.input, type_selector_return.Start, this.input.LT(-1), ex);
			}
			return type_selector_return;
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0006C0EC File Offset: 0x0006A2EC
		[GrammarRule("selector_namespace_prefix")]
		private CssParser.selector_namespace_prefix_return selector_namespace_prefix()
		{
			CssParser.selector_namespace_prefix_return selector_namespace_prefix_return = new CssParser.selector_namespace_prefix_return(this);
			selector_namespace_prefix_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token PIPE");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule element_name");
			try
			{
				int num = 2;
				int num2 = this.input.LA(1);
				if (num2 == 41 || num2 == 84)
				{
					num = 1;
				}
				int num3 = num;
				if (num3 == 1)
				{
					base.PushFollow(CssParser.Follow._element_name_in_selector_namespace_prefix2085);
					CssParser.element_name_return element_name_return = this.element_name();
					base.PopFollow();
					if (this.state.failed)
					{
						return selector_namespace_prefix_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream.Add(element_name_return.Tree);
					}
				}
				CommonToken el = (CommonToken)this.Match(this.input, 70, CssParser.Follow._PIPE_in_selector_namespace_prefix2088);
				if (this.state.failed)
				{
					return selector_namespace_prefix_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream.Add(el);
				}
				if (this.state.backtracking == 0)
				{
					selector_namespace_prefix_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (selector_namespace_prefix_return != null) ? selector_namespace_prefix_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(176, "SELECTOR_NAMESPACE_PREFIX"), obj2);
					if (rewriteRuleSubtreeStream.HasNext)
					{
						this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream.NextTree());
					}
					rewriteRuleSubtreeStream.Reset();
					this.adaptor.AddChild(obj, obj2);
					selector_namespace_prefix_return.Tree = obj;
				}
				selector_namespace_prefix_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					selector_namespace_prefix_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(selector_namespace_prefix_return.Tree, selector_namespace_prefix_return.Start, selector_namespace_prefix_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				selector_namespace_prefix_return.Tree = this.adaptor.ErrorNode(this.input, selector_namespace_prefix_return.Start, this.input.LT(-1), ex);
			}
			return selector_namespace_prefix_return;
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0006C358 File Offset: 0x0006A558
		[GrammarRule("element_name")]
		private CssParser.element_name_return element_name()
		{
			CssParser.element_name_return element_name_return = new CssParser.element_name_return(this);
			element_name_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token IDENT");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token STAR");
			try
			{
				int num = this.input.LA(1);
				int num2;
				if (num == 41)
				{
					num2 = 1;
				}
				else if (num == 84)
				{
					num2 = 2;
				}
				else
				{
					if (this.state.backtracking > 0)
					{
						this.state.failed = true;
						return element_name_return;
					}
					NoViableAltException ex = new NoViableAltException("", 38, 0, this.input);
					throw ex;
				}
				switch (num2)
				{
				case 1:
				{
					CommonToken el = (CommonToken)this.Match(this.input, 41, CssParser.Follow._IDENT_in_element_name2117);
					if (this.state.failed)
					{
						return element_name_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream.Add(el);
					}
					if (this.state.backtracking == 0)
					{
						element_name_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (element_name_return != null) ? element_name_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj2 = this.adaptor.Nil();
						obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(127, "ELEMENT_NAME"), obj2);
						this.adaptor.AddChild(obj2, rewriteRuleTokenStream.NextNode());
						this.adaptor.AddChild(obj, obj2);
						element_name_return.Tree = obj;
					}
					break;
				}
				case 2:
				{
					CommonToken el2 = (CommonToken)this.Match(this.input, 84, CssParser.Follow._STAR_in_element_name2137);
					if (this.state.failed)
					{
						return element_name_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream2.Add(el2);
					}
					if (this.state.backtracking == 0)
					{
						element_name_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (element_name_return != null) ? element_name_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj3 = this.adaptor.Nil();
						obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(127, "ELEMENT_NAME"), obj3);
						this.adaptor.AddChild(obj3, rewriteRuleTokenStream2.NextNode());
						this.adaptor.AddChild(obj, obj3);
						element_name_return.Tree = obj;
					}
					break;
				}
				}
				element_name_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					element_name_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(element_name_return.Tree, element_name_return.Start, element_name_return.Stop);
				}
			}
			catch (RecognitionException ex2)
			{
				this.ReportError(ex2);
				this.Recover(this.input, ex2);
				element_name_return.Tree = this.adaptor.ErrorNode(this.input, element_name_return.Start, this.input.LT(-1), ex2);
			}
			return element_name_return;
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x0006C69C File Offset: 0x0006A89C
		[GrammarRule("universal")]
		private CssParser.universal_return universal()
		{
			CssParser.universal_return universal_return = new CssParser.universal_return(this);
			universal_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token STAR");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule selector_namespace_prefix");
			try
			{
				int num = 2;
				int num2 = this.input.LA(1);
				if (num2 == 41 && this.EvaluatePredicate(new Action(this.synpred7_CssParser_fragment)))
				{
					num = 1;
				}
				else if (num2 == 84)
				{
					this.input.LA(2);
					if (this.EvaluatePredicate(new Action(this.synpred7_CssParser_fragment)))
					{
						num = 1;
					}
				}
				else if (num2 == 70 && this.EvaluatePredicate(new Action(this.synpred7_CssParser_fragment)))
				{
					num = 1;
				}
				int num3 = num;
				if (num3 == 1)
				{
					base.PushFollow(CssParser.Follow._selector_namespace_prefix_in_universal2174);
					CssParser.selector_namespace_prefix_return selector_namespace_prefix_return = this.selector_namespace_prefix();
					base.PopFollow();
					if (this.state.failed)
					{
						return universal_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream.Add(selector_namespace_prefix_return.Tree);
					}
				}
				CommonToken el = (CommonToken)this.Match(this.input, 84, CssParser.Follow._STAR_in_universal2178);
				if (this.state.failed)
				{
					return universal_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream.Add(el);
				}
				if (this.state.backtracking == 0)
				{
					universal_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (universal_return != null) ? universal_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(186, "UNIVERSAL"), obj2);
					if (rewriteRuleSubtreeStream.HasNext)
					{
						this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream.NextTree());
					}
					rewriteRuleSubtreeStream.Reset();
					this.adaptor.AddChild(obj, obj2);
					universal_return.Tree = obj;
				}
				universal_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					universal_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(universal_return.Tree, universal_return.Start, universal_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				universal_return.Tree = this.adaptor.ErrorNode(this.input, universal_return.Start, this.input.LT(-1), ex);
			}
			return universal_return;
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0006C964 File Offset: 0x0006AB64
		[GrammarRule("class")]
		private CssParser.class_return @class()
		{
			CssParser.class_return class_return = new CssParser.class_return(this);
			class_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token CLASS_IDENT");
			try
			{
				CommonToken el = (CommonToken)this.Match(this.input, 14, CssParser.Follow._CLASS_IDENT_in_class2207);
				if (this.state.failed)
				{
					return class_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream.Add(el);
				}
				if (this.state.backtracking == 0)
				{
					class_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (class_return != null) ? class_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(117, "CLASSIDENTIFIER"), obj2);
					this.adaptor.AddChild(obj2, rewriteRuleTokenStream.NextNode());
					this.adaptor.AddChild(obj, obj2);
					class_return.Tree = obj;
				}
				class_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					class_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(class_return.Tree, class_return.Start, class_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				class_return.Tree = this.adaptor.ErrorNode(this.input, class_return.Start, this.input.LT(-1), ex);
			}
			return class_return;
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0006CB38 File Offset: 0x0006AD38
		[GrammarRule("attrib")]
		private CssParser.attrib_return attrib()
		{
			CssParser.attrib_return attrib_return = new CssParser.attrib_return(this);
			attrib_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			CommonToken commonToken = null;
			CommonToken commonToken2 = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token SQUARE_BEGIN");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token IDENT");
			RewriteRuleTokenStream rewriteRuleTokenStream3 = new RewriteRuleTokenStream(this.adaptor, "token PREFIXMATCH");
			RewriteRuleTokenStream rewriteRuleTokenStream4 = new RewriteRuleTokenStream(this.adaptor, "token SUFFIXMATCH");
			RewriteRuleTokenStream rewriteRuleTokenStream5 = new RewriteRuleTokenStream(this.adaptor, "token SUBSTRINGMATCH");
			RewriteRuleTokenStream rewriteRuleTokenStream6 = new RewriteRuleTokenStream(this.adaptor, "token EQUALS");
			RewriteRuleTokenStream rewriteRuleTokenStream7 = new RewriteRuleTokenStream(this.adaptor, "token INCLUDES");
			RewriteRuleTokenStream rewriteRuleTokenStream8 = new RewriteRuleTokenStream(this.adaptor, "token DASHMATCH");
			RewriteRuleTokenStream rewriteRuleTokenStream9 = new RewriteRuleTokenStream(this.adaptor, "token STRING");
			RewriteRuleTokenStream rewriteRuleTokenStream10 = new RewriteRuleTokenStream(this.adaptor, "token SQUARE_END");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule selector_namespace_prefix");
			try
			{
				CommonToken el = (CommonToken)this.Match(this.input, 82, CssParser.Follow._SQUARE_BEGIN_in_attrib2246);
				if (this.state.failed)
				{
					return attrib_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream.Add(el);
				}
				int num = 2;
				int num2 = this.input.LA(1);
				if (num2 == 41)
				{
					int num3 = this.input.LA(2);
					if (num3 == 70)
					{
						num = 1;
					}
				}
				else if (num2 == 70 || num2 == 84)
				{
					num = 1;
				}
				int num4 = num;
				if (num4 == 1)
				{
					base.PushFollow(CssParser.Follow._selector_namespace_prefix_in_attrib2257);
					CssParser.selector_namespace_prefix_return selector_namespace_prefix_return = this.selector_namespace_prefix();
					base.PopFollow();
					if (this.state.failed)
					{
						return attrib_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream.Add(selector_namespace_prefix_return.Tree);
					}
				}
				CommonToken commonToken3 = (CommonToken)this.Match(this.input, 41, CssParser.Follow._IDENT_in_attrib2262);
				if (this.state.failed)
				{
					return attrib_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream2.Add(commonToken3);
				}
				int num5 = 2;
				int num6 = this.input.LA(1);
				if (num6 == 21 || num6 == 28 || num6 == 45 || num6 == 72 || (num6 >= 86 && num6 <= 87))
				{
					num5 = 1;
				}
				int num7 = num5;
				if (num7 == 1)
				{
					int num8 = this.input.LA(1);
					int num9;
					if (num8 <= 28)
					{
						if (num8 == 21)
						{
							num9 = 6;
							goto IL_2F3;
						}
						if (num8 == 28)
						{
							num9 = 4;
							goto IL_2F3;
						}
					}
					else
					{
						if (num8 == 45)
						{
							num9 = 5;
							goto IL_2F3;
						}
						if (num8 == 72)
						{
							num9 = 1;
							goto IL_2F3;
						}
						switch (num8)
						{
						case 86:
							num9 = 3;
							goto IL_2F3;
						case 87:
							num9 = 2;
							goto IL_2F3;
						}
					}
					if (this.state.backtracking > 0)
					{
						this.state.failed = true;
						return attrib_return;
					}
					NoViableAltException ex = new NoViableAltException("", 41, 0, this.input);
					throw ex;
					IL_2F3:
					switch (num9)
					{
					case 1:
						commonToken = (CommonToken)this.Match(this.input, 72, CssParser.Follow._PREFIXMATCH_in_attrib2289);
						if (this.state.failed)
						{
							return attrib_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream3.Add(commonToken);
						}
						break;
					case 2:
						commonToken = (CommonToken)this.Match(this.input, 87, CssParser.Follow._SUFFIXMATCH_in_attrib2293);
						if (this.state.failed)
						{
							return attrib_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream4.Add(commonToken);
						}
						break;
					case 3:
						commonToken = (CommonToken)this.Match(this.input, 86, CssParser.Follow._SUBSTRINGMATCH_in_attrib2297);
						if (this.state.failed)
						{
							return attrib_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream5.Add(commonToken);
						}
						break;
					case 4:
						commonToken = (CommonToken)this.Match(this.input, 28, CssParser.Follow._EQUALS_in_attrib2301);
						if (this.state.failed)
						{
							return attrib_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream6.Add(commonToken);
						}
						break;
					case 5:
						commonToken = (CommonToken)this.Match(this.input, 45, CssParser.Follow._INCLUDES_in_attrib2305);
						if (this.state.failed)
						{
							return attrib_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream7.Add(commonToken);
						}
						break;
					case 6:
						commonToken = (CommonToken)this.Match(this.input, 21, CssParser.Follow._DASHMATCH_in_attrib2309);
						if (this.state.failed)
						{
							return attrib_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream8.Add(commonToken);
						}
						break;
					}
					int num10 = this.input.LA(1);
					int num11;
					if (num10 == 41)
					{
						num11 = 1;
					}
					else if (num10 == 85)
					{
						num11 = 2;
					}
					else
					{
						if (this.state.backtracking > 0)
						{
							this.state.failed = true;
							return attrib_return;
						}
						NoViableAltException ex2 = new NoViableAltException("", 42, 0, this.input);
						throw ex2;
					}
					switch (num11)
					{
					case 1:
						commonToken2 = (CommonToken)this.Match(this.input, 41, CssParser.Follow._IDENT_in_attrib2327);
						if (this.state.failed)
						{
							return attrib_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream2.Add(commonToken2);
						}
						break;
					case 2:
					{
						CommonToken el2 = (CommonToken)this.Match(this.input, 85, CssParser.Follow._STRING_in_attrib2329);
						if (this.state.failed)
						{
							return attrib_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream9.Add(el2);
						}
						break;
					}
					}
				}
				CommonToken el3 = (CommonToken)this.Match(this.input, 83, CssParser.Follow._SQUARE_END_in_attrib2347);
				if (this.state.failed)
				{
					return attrib_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream10.Add(el3);
				}
				if (this.state.backtracking == 0)
				{
					attrib_return.Tree = obj;
					RewriteRuleTokenStream rewriteRuleTokenStream11 = new RewriteRuleTokenStream(this.adaptor, "token attributeName", commonToken3);
					RewriteRuleTokenStream rewriteRuleTokenStream12 = new RewriteRuleTokenStream(this.adaptor, "token attributeOperator", commonToken);
					RewriteRuleTokenStream rewriteRuleTokenStream13 = new RewriteRuleTokenStream(this.adaptor, "token attribvalue", commonToken2);
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (attrib_return != null) ? attrib_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(111, "ATTRIBIDENTIFIER"), obj2);
					if (rewriteRuleSubtreeStream.HasNext)
					{
						this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream.NextTree());
					}
					rewriteRuleSubtreeStream.Reset();
					object obj3 = this.adaptor.Nil();
					obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(112, "ATTRIBNAME"), obj3);
					this.adaptor.AddChild(obj3, rewriteRuleTokenStream11.NextNode());
					this.adaptor.AddChild(obj2, obj3);
					if (rewriteRuleTokenStream12.HasNext || rewriteRuleTokenStream13.HasNext || rewriteRuleTokenStream9.HasNext)
					{
						object obj4 = this.adaptor.Nil();
						obj4 = this.adaptor.BecomeRoot(this.adaptor.Create(114, "ATTRIBOPERATORVALUE"), obj4);
						object obj5 = this.adaptor.Nil();
						obj5 = this.adaptor.BecomeRoot(this.adaptor.Create(113, "ATTRIBOPERATOR"), obj5);
						this.adaptor.AddChild(obj5, rewriteRuleTokenStream12.NextNode());
						this.adaptor.AddChild(obj4, obj5);
						object obj6 = this.adaptor.Nil();
						obj6 = this.adaptor.BecomeRoot(this.adaptor.Create(115, "ATTRIBVALUE"), obj6);
						if (rewriteRuleTokenStream13.HasNext)
						{
							this.adaptor.AddChild(obj6, rewriteRuleTokenStream13.NextNode());
						}
						rewriteRuleTokenStream13.Reset();
						if (rewriteRuleTokenStream9.HasNext)
						{
							object obj7 = this.adaptor.Nil();
							obj7 = this.adaptor.BecomeRoot(this.adaptor.Create(179, "STRINGBASEDVALUE"), obj7);
							this.adaptor.AddChild(obj7, rewriteRuleTokenStream9.NextNode());
							this.adaptor.AddChild(obj6, obj7);
						}
						rewriteRuleTokenStream9.Reset();
						this.adaptor.AddChild(obj4, obj6);
						this.adaptor.AddChild(obj2, obj4);
					}
					rewriteRuleTokenStream12.Reset();
					rewriteRuleTokenStream13.Reset();
					rewriteRuleTokenStream9.Reset();
					this.adaptor.AddChild(obj, obj2);
					attrib_return.Tree = obj;
				}
				attrib_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					attrib_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(attrib_return.Tree, attrib_return.Start, attrib_return.Stop);
				}
			}
			catch (RecognitionException ex3)
			{
				this.ReportError(ex3);
				this.Recover(this.input, ex3);
				attrib_return.Tree = this.adaptor.ErrorNode(this.input, attrib_return.Start, this.input.LT(-1), ex3);
			}
			return attrib_return;
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0006D4D4 File Offset: 0x0006B6D4
		[GrammarRule("pseudo")]
		private CssParser.pseudo_return pseudo()
		{
			CssParser.pseudo_return pseudo_return = new CssParser.pseudo_return(this);
			pseudo_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			CommonToken commonToken = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token COLON");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token IDENT");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule functional_pseudo");
			try
			{
				int num = this.input.LA(1);
				if (num == 15)
				{
					int num2 = this.input.LA(2);
					int num5;
					if (num2 <= 33)
					{
						if (num2 != 15)
						{
							if (num2 != 33)
							{
								goto IL_29E;
							}
						}
						else
						{
							int num3 = this.input.LA(3);
							if (num3 == 41)
							{
								int num4 = this.input.LA(4);
								if (num4 == 12)
								{
									num5 = 2;
									goto IL_312;
								}
								if (num4 == -1 || num4 == 7 || (num4 >= 13 && num4 <= 16) || (num4 == 18 || num4 == 35 || num4 == 38 || num4 == 41 || (num4 >= 70 && num4 <= 71)) || num4 == 76 || num4 == 82 || num4 == 84 || num4 == 89 || num4 == 105)
								{
									num5 = 1;
									goto IL_312;
								}
								if (this.state.backtracking > 0)
								{
									this.state.failed = true;
									return pseudo_return;
								}
								NoViableAltException ex = new NoViableAltException("", 46, 3, this.input);
								throw ex;
							}
							else
							{
								if (num3 == 33 || num3 == 55 || num3 == 91)
								{
									num5 = 2;
									goto IL_312;
								}
								if (this.state.backtracking > 0)
								{
									this.state.failed = true;
									return pseudo_return;
								}
								NoViableAltException ex2 = new NoViableAltException("", 46, 2, this.input);
								throw ex2;
							}
						}
					}
					else if (num2 != 41)
					{
						if (num2 != 55 && num2 != 91)
						{
							goto IL_29E;
						}
					}
					else
					{
						int num6 = this.input.LA(3);
						if (num6 == 12)
						{
							num5 = 2;
							goto IL_312;
						}
						if (num6 == -1 || num6 == 7 || (num6 >= 13 && num6 <= 16) || (num6 == 18 || num6 == 35 || num6 == 38 || num6 == 41 || (num6 >= 70 && num6 <= 71)) || num6 == 76 || num6 == 82 || num6 == 84 || num6 == 89 || num6 == 105)
						{
							num5 = 1;
							goto IL_312;
						}
						if (this.state.backtracking > 0)
						{
							this.state.failed = true;
							return pseudo_return;
						}
						NoViableAltException ex3 = new NoViableAltException("", 46, 3, this.input);
						throw ex3;
					}
					num5 = 2;
					goto IL_312;
					IL_29E:
					if (this.state.backtracking > 0)
					{
						this.state.failed = true;
						return pseudo_return;
					}
					NoViableAltException ex4 = new NoViableAltException("", 46, 1, this.input);
					throw ex4;
					IL_312:
					switch (num5)
					{
					case 1:
					{
						CommonToken commonToken2 = (CommonToken)this.Match(this.input, 15, CssParser.Follow._COLON_in_pseudo2420);
						if (this.state.failed)
						{
							return pseudo_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream.Add(commonToken2);
						}
						int num7 = 2;
						int num8 = this.input.LA(1);
						if (num8 == 15)
						{
							num7 = 1;
						}
						int num9 = num7;
						if (num9 == 1)
						{
							commonToken = (CommonToken)this.Match(this.input, 15, CssParser.Follow._COLON_in_pseudo2424);
							if (this.state.failed)
							{
								return pseudo_return;
							}
							if (this.state.backtracking == 0)
							{
								rewriteRuleTokenStream.Add(commonToken);
							}
						}
						CommonToken commonToken3 = (CommonToken)this.Match(this.input, 41, CssParser.Follow._IDENT_in_pseudo2429);
						if (this.state.failed)
						{
							return pseudo_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream2.Add(commonToken3);
						}
						if (this.state.backtracking == 0)
						{
							pseudo_return.Tree = obj;
							RewriteRuleTokenStream rewriteRuleTokenStream3 = new RewriteRuleTokenStream(this.adaptor, "token c1", commonToken2);
							RewriteRuleTokenStream rewriteRuleTokenStream4 = new RewriteRuleTokenStream(this.adaptor, "token c2", commonToken);
							RewriteRuleTokenStream rewriteRuleTokenStream5 = new RewriteRuleTokenStream(this.adaptor, "token pseudoName", commonToken3);
							new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (pseudo_return != null) ? pseudo_return.Tree : null);
							obj = this.adaptor.Nil();
							object obj2 = this.adaptor.Nil();
							obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(165, "PSEUDOIDENTIFIER"), obj2);
							object obj3 = this.adaptor.Nil();
							obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(118, "COLONS"), obj3);
							this.adaptor.AddChild(obj3, rewriteRuleTokenStream3.NextNode());
							if (rewriteRuleTokenStream4.HasNext)
							{
								this.adaptor.AddChild(obj3, rewriteRuleTokenStream4.NextNode());
							}
							rewriteRuleTokenStream4.Reset();
							this.adaptor.AddChild(obj2, obj3);
							object obj4 = this.adaptor.Nil();
							obj4 = this.adaptor.BecomeRoot(this.adaptor.Create(166, "PSEUDONAME"), obj4);
							this.adaptor.AddChild(obj4, rewriteRuleTokenStream5.NextNode());
							this.adaptor.AddChild(obj2, obj4);
							this.adaptor.AddChild(obj, obj2);
							pseudo_return.Tree = obj;
						}
						break;
					}
					case 2:
					{
						CommonToken commonToken2 = (CommonToken)this.Match(this.input, 15, CssParser.Follow._COLON_in_pseudo2467);
						if (this.state.failed)
						{
							return pseudo_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream.Add(commonToken2);
						}
						int num10 = 2;
						int num11 = this.input.LA(1);
						if (num11 == 15)
						{
							num10 = 1;
						}
						int num12 = num10;
						if (num12 == 1)
						{
							commonToken = (CommonToken)this.Match(this.input, 15, CssParser.Follow._COLON_in_pseudo2471);
							if (this.state.failed)
							{
								return pseudo_return;
							}
							if (this.state.backtracking == 0)
							{
								rewriteRuleTokenStream.Add(commonToken);
							}
						}
						base.PushFollow(CssParser.Follow._functional_pseudo_in_pseudo2474);
						CssParser.functional_pseudo_return functional_pseudo_return = this.functional_pseudo();
						base.PopFollow();
						if (this.state.failed)
						{
							return pseudo_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleSubtreeStream.Add(functional_pseudo_return.Tree);
						}
						if (this.state.backtracking == 0)
						{
							pseudo_return.Tree = obj;
							RewriteRuleTokenStream rewriteRuleTokenStream6 = new RewriteRuleTokenStream(this.adaptor, "token c1", commonToken2);
							RewriteRuleTokenStream rewriteRuleTokenStream7 = new RewriteRuleTokenStream(this.adaptor, "token c2", commonToken);
							new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (pseudo_return != null) ? pseudo_return.Tree : null);
							obj = this.adaptor.Nil();
							object obj5 = this.adaptor.Nil();
							obj5 = this.adaptor.BecomeRoot(this.adaptor.Create(165, "PSEUDOIDENTIFIER"), obj5);
							object obj6 = this.adaptor.Nil();
							obj6 = this.adaptor.BecomeRoot(this.adaptor.Create(118, "COLONS"), obj6);
							this.adaptor.AddChild(obj6, rewriteRuleTokenStream6.NextNode());
							if (rewriteRuleTokenStream7.HasNext)
							{
								this.adaptor.AddChild(obj6, rewriteRuleTokenStream7.NextNode());
							}
							rewriteRuleTokenStream7.Reset();
							this.adaptor.AddChild(obj5, obj6);
							this.adaptor.AddChild(obj5, rewriteRuleSubtreeStream.NextTree());
							this.adaptor.AddChild(obj, obj5);
							pseudo_return.Tree = obj;
						}
						break;
					}
					}
					pseudo_return.Stop = (CommonToken)this.input.LT(-1);
					if (this.state.backtracking == 0)
					{
						pseudo_return.Tree = this.adaptor.RulePostProcessing(obj);
						this.adaptor.SetTokenBoundaries(pseudo_return.Tree, pseudo_return.Start, pseudo_return.Stop);
					}
				}
				else
				{
					if (this.state.backtracking > 0)
					{
						this.state.failed = true;
						return pseudo_return;
					}
					NoViableAltException ex5 = new NoViableAltException("", 46, 0, this.input);
					throw ex5;
				}
			}
			catch (RecognitionException ex6)
			{
				this.ReportError(ex6);
				this.Recover(this.input, ex6);
				pseudo_return.Tree = this.adaptor.ErrorNode(this.input, pseudo_return.Start, this.input.LT(-1), ex6);
			}
			return pseudo_return;
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0006DD6C File Offset: 0x0006BF6C
		[GrammarRule("functional_pseudo")]
		private CssParser.functional_pseudo_return functional_pseudo()
		{
			CssParser.functional_pseudo_return functional_pseudo_return = new CssParser.functional_pseudo_return(this);
			functional_pseudo_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token CIRCLE_END");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule beginfunc");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream2 = new RewriteRuleSubtreeStream(this.adaptor, "rule selectorexpression");
			try
			{
				base.PushFollow(CssParser.Follow._beginfunc_in_functional_pseudo2515);
				CssParser.beginfunc_return beginfunc_return = this.beginfunc();
				base.PopFollow();
				if (this.state.failed)
				{
					return functional_pseudo_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleSubtreeStream.Add(beginfunc_return.Tree);
				}
				base.PushFollow(CssParser.Follow._selectorexpression_in_functional_pseudo2517);
				CssParser.selectorexpression_return selectorexpression_return = this.selectorexpression();
				base.PopFollow();
				if (this.state.failed)
				{
					return functional_pseudo_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleSubtreeStream2.Add(selectorexpression_return.Tree);
				}
				CommonToken el = (CommonToken)this.Match(this.input, 13, CssParser.Follow._CIRCLE_END_in_functional_pseudo2519);
				if (this.state.failed)
				{
					return functional_pseudo_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream.Add(el);
				}
				if (this.state.backtracking == 0)
				{
					functional_pseudo_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (functional_pseudo_return != null) ? functional_pseudo_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(129, "FUNCTIONAL_PSEUDO"), obj2);
					this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream.NextTree());
					object obj3 = this.adaptor.Nil();
					obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(175, "SELECTOR_EXPRESSION"), obj3);
					this.adaptor.AddChild(obj3, rewriteRuleSubtreeStream2.NextTree());
					this.adaptor.AddChild(obj2, obj3);
					this.adaptor.AddChild(obj, obj2);
					functional_pseudo_return.Tree = obj;
				}
				functional_pseudo_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					functional_pseudo_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(functional_pseudo_return.Tree, functional_pseudo_return.Start, functional_pseudo_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				functional_pseudo_return.Tree = this.adaptor.ErrorNode(this.input, functional_pseudo_return.Start, this.input.LT(-1), ex);
			}
			return functional_pseudo_return;
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0006E054 File Offset: 0x0006C254
		[GrammarRule("selectorexpression")]
		private CssParser.selectorexpression_return selectorexpression()
		{
			CssParser.selectorexpression_return selectorexpression_return = new CssParser.selectorexpression_return(this);
			selectorexpression_return.Start = (CommonToken)this.input.LT(1);
			try
			{
				object obj = this.adaptor.Nil();
				int num = 0;
				for (;;)
				{
					int num2 = 2;
					int num3 = this.input.LA(1);
					if (num3 == 23 || num3 == 41 || num3 == 53 || num3 == 64 || num3 == 71 || num3 == 85 || num3 == 168)
					{
						num2 = 1;
					}
					int num4 = num2;
					if (num4 != 1)
					{
						goto IL_17F;
					}
					CommonToken payload = (CommonToken)this.input.LT(1);
					if (this.input.LA(1) != 23 && this.input.LA(1) != 41 && this.input.LA(1) != 53 && this.input.LA(1) != 64 && this.input.LA(1) != 71 && this.input.LA(1) != 85 && this.input.LA(1) != 168)
					{
						break;
					}
					this.input.Consume();
					if (this.state.backtracking == 0)
					{
						this.adaptor.AddChild(obj, this.adaptor.Create(payload));
					}
					this.state.errorRecovery = false;
					this.state.failed = false;
					num++;
				}
				if (this.state.backtracking > 0)
				{
					this.state.failed = true;
					return selectorexpression_return;
				}
				MismatchedSetException ex = new MismatchedSetException(null, this.input);
				throw ex;
				IL_17F:
				if (num < 1)
				{
					if (this.state.backtracking > 0)
					{
						this.state.failed = true;
						return selectorexpression_return;
					}
					EarlyExitException ex2 = new EarlyExitException(47, this.input);
					throw ex2;
				}
				else
				{
					selectorexpression_return.Stop = (CommonToken)this.input.LT(-1);
					if (this.state.backtracking == 0)
					{
						selectorexpression_return.Tree = this.adaptor.RulePostProcessing(obj);
						this.adaptor.SetTokenBoundaries(selectorexpression_return.Tree, selectorexpression_return.Start, selectorexpression_return.Stop);
					}
				}
			}
			catch (RecognitionException ex3)
			{
				this.ReportError(ex3);
				this.Recover(this.input, ex3);
				selectorexpression_return.Tree = this.adaptor.ErrorNode(this.input, selectorexpression_return.Start, this.input.LT(-1), ex3);
			}
			return selectorexpression_return;
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0006E2DC File Offset: 0x0006C4DC
		[GrammarRule("negation")]
		private CssParser.negation_return negation()
		{
			CssParser.negation_return negation_return = new CssParser.negation_return(this);
			negation_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token COLON");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token NOT");
			RewriteRuleTokenStream rewriteRuleTokenStream3 = new RewriteRuleTokenStream(this.adaptor, "token CIRCLE_BEGIN");
			RewriteRuleTokenStream rewriteRuleTokenStream4 = new RewriteRuleTokenStream(this.adaptor, "token CIRCLE_END");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule negation_arg");
			try
			{
				CommonToken el = (CommonToken)this.Match(this.input, 15, CssParser.Follow._COLON_in_negation2594);
				if (this.state.failed)
				{
					return negation_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream.Add(el);
				}
				CommonToken el2 = (CommonToken)this.Match(this.input, 63, CssParser.Follow._NOT_in_negation2596);
				if (this.state.failed)
				{
					return negation_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream2.Add(el2);
				}
				CommonToken el3 = (CommonToken)this.Match(this.input, 12, CssParser.Follow._CIRCLE_BEGIN_in_negation2598);
				if (this.state.failed)
				{
					return negation_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream3.Add(el3);
				}
				base.PushFollow(CssParser.Follow._negation_arg_in_negation2601);
				CssParser.negation_arg_return negation_arg_return = this.negation_arg();
				base.PopFollow();
				if (this.state.failed)
				{
					return negation_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleSubtreeStream.Add(negation_arg_return.Tree);
				}
				CommonToken el4 = (CommonToken)this.Match(this.input, 13, CssParser.Follow._CIRCLE_END_in_negation2603);
				if (this.state.failed)
				{
					return negation_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream4.Add(el4);
				}
				if (this.state.backtracking == 0)
				{
					negation_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (negation_return != null) ? negation_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(157, "NEGATIONIDENTIFIER"), obj2);
					object obj3 = this.adaptor.Nil();
					obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(158, "NEGATION_ARG"), obj3);
					this.adaptor.AddChild(obj3, rewriteRuleSubtreeStream.NextTree());
					this.adaptor.AddChild(obj2, obj3);
					this.adaptor.AddChild(obj, obj2);
					negation_return.Tree = obj;
				}
				negation_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					negation_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(negation_return.Tree, negation_return.Start, negation_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				negation_return.Tree = this.adaptor.ErrorNode(this.input, negation_return.Start, this.input.LT(-1), ex);
			}
			return negation_return;
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0006E660 File Offset: 0x0006C860
		[GrammarRule("negation_arg")]
		private CssParser.negation_arg_return negation_arg()
		{
			CssParser.negation_arg_return negation_arg_return = new CssParser.negation_arg_return(this);
			negation_arg_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			try
			{
				int num = 6;
				try
				{
					num = this.dfa48.Predict(this.input);
				}
				catch (NoViableAltException)
				{
					throw;
				}
				switch (num)
				{
				case 1:
				{
					obj = this.adaptor.Nil();
					base.PushFollow(CssParser.Follow._universal_in_negation_arg2640);
					CssParser.universal_return universal_return = this.universal();
					base.PopFollow();
					if (this.state.failed)
					{
						return negation_arg_return;
					}
					if (this.state.backtracking == 0)
					{
						this.adaptor.AddChild(obj, universal_return.Tree);
					}
					break;
				}
				case 2:
				{
					obj = this.adaptor.Nil();
					base.PushFollow(CssParser.Follow._type_selector_in_negation_arg2643);
					CssParser.type_selector_return type_selector_return = this.type_selector();
					base.PopFollow();
					if (this.state.failed)
					{
						return negation_arg_return;
					}
					if (this.state.backtracking == 0)
					{
						this.adaptor.AddChild(obj, type_selector_return.Tree);
					}
					break;
				}
				case 3:
				{
					obj = this.adaptor.Nil();
					base.PushFollow(CssParser.Follow._hash_in_negation_arg2645);
					CssParser.hash_return hash_return = this.hash();
					base.PopFollow();
					if (this.state.failed)
					{
						return negation_arg_return;
					}
					if (this.state.backtracking == 0)
					{
						this.adaptor.AddChild(obj, hash_return.Tree);
					}
					break;
				}
				case 4:
				{
					obj = this.adaptor.Nil();
					base.PushFollow(CssParser.Follow._class_in_negation_arg2647);
					CssParser.class_return class_return = this.@class();
					base.PopFollow();
					if (this.state.failed)
					{
						return negation_arg_return;
					}
					if (this.state.backtracking == 0)
					{
						this.adaptor.AddChild(obj, class_return.Tree);
					}
					break;
				}
				case 5:
				{
					obj = this.adaptor.Nil();
					base.PushFollow(CssParser.Follow._attrib_in_negation_arg2649);
					CssParser.attrib_return attrib_return = this.attrib();
					base.PopFollow();
					if (this.state.failed)
					{
						return negation_arg_return;
					}
					if (this.state.backtracking == 0)
					{
						this.adaptor.AddChild(obj, attrib_return.Tree);
					}
					break;
				}
				case 6:
				{
					obj = this.adaptor.Nil();
					base.PushFollow(CssParser.Follow._pseudo_in_negation_arg2651);
					CssParser.pseudo_return pseudo_return = this.pseudo();
					base.PopFollow();
					if (this.state.failed)
					{
						return negation_arg_return;
					}
					if (this.state.backtracking == 0)
					{
						this.adaptor.AddChild(obj, pseudo_return.Tree);
					}
					break;
				}
				}
				negation_arg_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					negation_arg_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(negation_arg_return.Tree, negation_arg_return.Start, negation_arg_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				negation_arg_return.Tree = this.adaptor.ErrorNode(this.input, negation_arg_return.Start, this.input.LT(-1), ex);
			}
			return negation_arg_return;
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0006E9F0 File Offset: 0x0006CBF0
		[GrammarRule("atname")]
		private CssParser.atname_return atname()
		{
			CssParser.atname_return atname_return = new CssParser.atname_return(this);
			atname_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token AT_NAME");
			try
			{
				CommonToken el = (CommonToken)this.Match(this.input, 7, CssParser.Follow._AT_NAME_in_atname2666);
				if (this.state.failed)
				{
					return atname_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream.Add(el);
				}
				if (this.state.backtracking == 0)
				{
					atname_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (atname_return != null) ? atname_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(110, "ATIDENTIFIER"), obj2);
					this.adaptor.AddChild(obj2, rewriteRuleTokenStream.NextNode());
					this.adaptor.AddChild(obj, obj2);
					atname_return.Tree = obj;
				}
				atname_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					atname_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(atname_return.Tree, atname_return.Start, atname_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				atname_return.Tree = this.adaptor.ErrorNode(this.input, atname_return.Start, this.input.LT(-1), ex);
			}
			return atname_return;
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0006EBC4 File Offset: 0x0006CDC4
		[GrammarRule("declaration")]
		private CssParser.declaration_return declaration()
		{
			CssParser.declaration_return declaration_return = new CssParser.declaration_return(this);
			declaration_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token IMPORTANT_COMMENTS");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token COLON");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule property");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream2 = new RewriteRuleSubtreeStream(this.adaptor, "rule expr");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream3 = new RewriteRuleSubtreeStream(this.adaptor, "rule prio");
			try
			{
				for (;;)
				{
					int num = 2;
					int num2 = this.input.LA(1);
					if (num2 == 42)
					{
						num = 1;
					}
					int num3 = num;
					if (num3 != 1)
					{
						goto IL_EF;
					}
					CommonToken el = (CommonToken)this.Match(this.input, 42, CssParser.Follow._IMPORTANT_COMMENTS_in_declaration2698);
					if (this.state.failed)
					{
						break;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream.Add(el);
					}
				}
				return declaration_return;
				IL_EF:
				base.PushFollow(CssParser.Follow._property_in_declaration2701);
				CssParser.property_return property_return = this.property();
				base.PopFollow();
				if (this.state.failed)
				{
					return declaration_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleSubtreeStream.Add(property_return.Tree);
				}
				CommonToken el2 = (CommonToken)this.Match(this.input, 15, CssParser.Follow._COLON_in_declaration2703);
				if (this.state.failed)
				{
					return declaration_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream2.Add(el2);
				}
				base.PushFollow(CssParser.Follow._expr_in_declaration2705);
				CssParser.expr_return expr_return = this.expr();
				base.PopFollow();
				if (this.state.failed)
				{
					return declaration_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleSubtreeStream2.Add(expr_return.Tree);
				}
				int num4 = 2;
				int num5 = this.input.LA(1);
				if (num5 == 43)
				{
					num4 = 1;
				}
				int num6 = num4;
				if (num6 == 1)
				{
					base.PushFollow(CssParser.Follow._prio_in_declaration2707);
					CssParser.prio_return prio_return = this.prio();
					base.PopFollow();
					if (this.state.failed)
					{
						return declaration_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream3.Add(prio_return.Tree);
					}
				}
				if (this.state.backtracking == 0)
				{
					declaration_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (declaration_return != null) ? declaration_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(122, "DECLARATION"), obj2);
					while (rewriteRuleTokenStream.HasNext)
					{
						this.adaptor.AddChild(obj2, rewriteRuleTokenStream.NextNode());
					}
					rewriteRuleTokenStream.Reset();
					this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream.NextTree());
					this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream2.NextTree());
					if (rewriteRuleSubtreeStream3.HasNext)
					{
						this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream3.NextTree());
					}
					rewriteRuleSubtreeStream3.Reset();
					this.adaptor.AddChild(obj, obj2);
					declaration_return.Tree = obj;
				}
				declaration_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					declaration_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(declaration_return.Tree, declaration_return.Start, declaration_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				declaration_return.Tree = this.adaptor.ErrorNode(this.input, declaration_return.Start, this.input.LT(-1), ex);
			}
			return declaration_return;
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0006EFB0 File Offset: 0x0006D1B0
		[GrammarRule("stringoruri")]
		private CssParser.stringoruri_return stringoruri()
		{
			CssParser.stringoruri_return stringoruri_return = new CssParser.stringoruri_return(this);
			stringoruri_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token STRING");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token URI");
			try
			{
				int num = this.input.LA(1);
				int num2;
				if (num == 85)
				{
					num2 = 1;
				}
				else if (num == 99)
				{
					num2 = 2;
				}
				else
				{
					if (this.state.backtracking > 0)
					{
						this.state.failed = true;
						return stringoruri_return;
					}
					NoViableAltException ex = new NoViableAltException("", 51, 0, this.input);
					throw ex;
				}
				switch (num2)
				{
				case 1:
				{
					CommonToken el = (CommonToken)this.Match(this.input, 85, CssParser.Follow._STRING_in_stringoruri2747);
					if (this.state.failed)
					{
						return stringoruri_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream.Add(el);
					}
					if (this.state.backtracking == 0)
					{
						stringoruri_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (stringoruri_return != null) ? stringoruri_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj2 = this.adaptor.Nil();
						obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(179, "STRINGBASEDVALUE"), obj2);
						this.adaptor.AddChild(obj2, rewriteRuleTokenStream.NextNode());
						this.adaptor.AddChild(obj, obj2);
						stringoruri_return.Tree = obj;
					}
					break;
				}
				case 2:
				{
					CommonToken el2 = (CommonToken)this.Match(this.input, 99, CssParser.Follow._URI_in_stringoruri2767);
					if (this.state.failed)
					{
						return stringoruri_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream2.Add(el2);
					}
					if (this.state.backtracking == 0)
					{
						stringoruri_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (stringoruri_return != null) ? stringoruri_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj3 = this.adaptor.Nil();
						obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(187, "URIBASEDVALUE"), obj3);
						this.adaptor.AddChild(obj3, rewriteRuleTokenStream2.NextNode());
						this.adaptor.AddChild(obj, obj3);
						stringoruri_return.Tree = obj;
					}
					break;
				}
				}
				stringoruri_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					stringoruri_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(stringoruri_return.Tree, stringoruri_return.Start, stringoruri_return.Stop);
				}
			}
			catch (RecognitionException ex2)
			{
				this.ReportError(ex2);
				this.Recover(this.input, ex2);
				stringoruri_return.Tree = this.adaptor.ErrorNode(this.input, stringoruri_return.Start, this.input.LT(-1), ex2);
			}
			return stringoruri_return;
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0006F2FC File Offset: 0x0006D4FC
		[GrammarRule("styleSheetrules")]
		private CssParser.styleSheetrules_return styleSheetrules()
		{
			CssParser.styleSheetrules_return styleSheetrules_return = new CssParser.styleSheetrules_return(this);
			styleSheetrules_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			try
			{
				int num = this.input.LA(1);
				int num2;
				if (num <= 41)
				{
					if (num <= 15)
					{
						if (num != 7)
						{
							switch (num)
							{
							case 14:
							case 15:
								break;
							default:
								goto IL_E9;
							}
						}
					}
					else
					{
						if (num == 24)
						{
							num2 = 5;
							goto IL_123;
						}
						if (num != 38 && num != 41)
						{
							goto IL_E9;
						}
					}
				}
				else if (num <= 70)
				{
					if (num == 47)
					{
						num2 = 4;
						goto IL_123;
					}
					if (num == 52)
					{
						num2 = 2;
						goto IL_123;
					}
					switch (num)
					{
					case 68:
						num2 = 3;
						goto IL_123;
					case 69:
						goto IL_E9;
					case 70:
						break;
					default:
						goto IL_E9;
					}
				}
				else if (num != 76)
				{
					switch (num)
					{
					case 82:
					case 84:
						break;
					case 83:
						goto IL_E9;
					default:
						if (num != 104)
						{
							goto IL_E9;
						}
						num2 = 6;
						goto IL_123;
					}
				}
				num2 = 1;
				goto IL_123;
				IL_E9:
				if (this.state.backtracking > 0)
				{
					this.state.failed = true;
					return styleSheetrules_return;
				}
				NoViableAltException ex = new NoViableAltException("", 52, 0, this.input);
				throw ex;
				IL_123:
				switch (num2)
				{
				case 1:
				{
					obj = this.adaptor.Nil();
					base.PushFollow(CssParser.Follow._ruleset_in_styleSheetrules2796);
					CssParser.ruleset_return ruleset_return = this.ruleset();
					base.PopFollow();
					if (this.state.failed)
					{
						return styleSheetrules_return;
					}
					if (this.state.backtracking == 0)
					{
						this.adaptor.AddChild(obj, ruleset_return.Tree);
					}
					break;
				}
				case 2:
				{
					obj = this.adaptor.Nil();
					base.PushFollow(CssParser.Follow._media_in_styleSheetrules2798);
					CssParser.media_return media_return = this.media();
					base.PopFollow();
					if (this.state.failed)
					{
						return styleSheetrules_return;
					}
					if (this.state.backtracking == 0)
					{
						this.adaptor.AddChild(obj, media_return.Tree);
					}
					break;
				}
				case 3:
				{
					obj = this.adaptor.Nil();
					base.PushFollow(CssParser.Follow._page_in_styleSheetrules2800);
					CssParser.page_return page_return = this.page();
					base.PopFollow();
					if (this.state.failed)
					{
						return styleSheetrules_return;
					}
					if (this.state.backtracking == 0)
					{
						this.adaptor.AddChild(obj, page_return.Tree);
					}
					break;
				}
				case 4:
				{
					obj = this.adaptor.Nil();
					base.PushFollow(CssParser.Follow._keyframes_in_styleSheetrules2802);
					CssParser.keyframes_return keyframes_return = this.keyframes();
					base.PopFollow();
					if (this.state.failed)
					{
						return styleSheetrules_return;
					}
					if (this.state.backtracking == 0)
					{
						this.adaptor.AddChild(obj, keyframes_return.Tree);
					}
					break;
				}
				case 5:
				{
					obj = this.adaptor.Nil();
					base.PushFollow(CssParser.Follow._document_in_styleSheetrules2804);
					CssParser.document_return document_return = this.document();
					base.PopFollow();
					if (this.state.failed)
					{
						return styleSheetrules_return;
					}
					if (this.state.backtracking == 0)
					{
						this.adaptor.AddChild(obj, document_return.Tree);
					}
					break;
				}
				case 6:
				{
					obj = this.adaptor.Nil();
					base.PushFollow(CssParser.Follow._wg_dpi_in_styleSheetrules2806);
					CssParser.wg_dpi_return wg_dpi_return = this.wg_dpi();
					base.PopFollow();
					if (this.state.failed)
					{
						return styleSheetrules_return;
					}
					if (this.state.backtracking == 0)
					{
						this.adaptor.AddChild(obj, wg_dpi_return.Tree);
					}
					break;
				}
				}
				styleSheetrules_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					styleSheetrules_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(styleSheetrules_return.Tree, styleSheetrules_return.Start, styleSheetrules_return.Stop);
				}
			}
			catch (RecognitionException ex2)
			{
				this.ReportError(ex2);
				this.Recover(this.input, ex2);
				styleSheetrules_return.Tree = this.adaptor.ErrorNode(this.input, styleSheetrules_return.Start, this.input.LT(-1), ex2);
			}
			return styleSheetrules_return;
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0006F74C File Offset: 0x0006D94C
		[GrammarRule("prio")]
		private CssParser.prio_return prio()
		{
			CssParser.prio_return prio_return = new CssParser.prio_return(this);
			prio_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token IMPORTANT_SYM");
			try
			{
				CommonToken el = (CommonToken)this.Match(this.input, 43, CssParser.Follow._IMPORTANT_SYM_in_prio2826);
				if (this.state.failed)
				{
					return prio_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream.Add(el);
				}
				if (this.state.backtracking == 0)
				{
					prio_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (prio_return != null) ? prio_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(139, "IMPORTANT"), obj2);
					this.adaptor.AddChild(obj2, rewriteRuleTokenStream.NextNode());
					this.adaptor.AddChild(obj, obj2);
					prio_return.Tree = obj;
				}
				prio_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					prio_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(prio_return.Tree, prio_return.Start, prio_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				prio_return.Tree = this.adaptor.ErrorNode(this.input, prio_return.Start, this.input.LT(-1), ex);
			}
			return prio_return;
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0006F924 File Offset: 0x0006DB24
		[GrammarRule("expr")]
		private CssParser.expr_return expr()
		{
			CssParser.expr_return expr_return = new CssParser.expr_return(this);
			expr_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token IMPORTANT_COMMENTS");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule term");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream2 = new RewriteRuleSubtreeStream(this.adaptor, "rule termwithoperator");
			try
			{
				for (;;)
				{
					int num = 2;
					int num2 = this.input.LA(1);
					if (num2 == 42)
					{
						num = 1;
					}
					int num3 = num;
					if (num3 != 1)
					{
						goto IL_C5;
					}
					CommonToken el = (CommonToken)this.Match(this.input, 42, CssParser.Follow._IMPORTANT_COMMENTS_in_expr2856);
					if (this.state.failed)
					{
						break;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream.Add(el);
					}
				}
				return expr_return;
				IL_C5:
				base.PushFollow(CssParser.Follow._term_in_expr2859);
				CssParser.term_return term_return = this.term();
				base.PopFollow();
				if (this.state.failed)
				{
					return expr_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleSubtreeStream.Add(term_return.Tree);
				}
				for (;;)
				{
					int num4 = 2;
					try
					{
						num4 = this.dfa54.Predict(this.input);
					}
					catch (NoViableAltException)
					{
						throw;
					}
					int num5 = num4;
					if (num5 != 1)
					{
						goto IL_17B;
					}
					base.PushFollow(CssParser.Follow._termwithoperator_in_expr2862);
					CssParser.termwithoperator_return termwithoperator_return = this.termwithoperator();
					base.PopFollow();
					if (this.state.failed)
					{
						break;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream2.Add(termwithoperator_return.Tree);
					}
				}
				return expr_return;
				IL_17B:
				if (this.state.backtracking == 0)
				{
					expr_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (expr_return != null) ? expr_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(128, "EXPR"), obj2);
					while (rewriteRuleTokenStream.HasNext)
					{
						this.adaptor.AddChild(obj2, rewriteRuleTokenStream.NextNode());
					}
					rewriteRuleTokenStream.Reset();
					this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream.NextTree());
					if (rewriteRuleSubtreeStream2.HasNext)
					{
						object obj3 = this.adaptor.Nil();
						obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(183, "TERMWITHOPERATORS"), obj3);
						while (rewriteRuleSubtreeStream2.HasNext)
						{
							this.adaptor.AddChild(obj3, rewriteRuleSubtreeStream2.NextTree());
						}
						rewriteRuleSubtreeStream2.Reset();
						this.adaptor.AddChild(obj2, obj3);
					}
					rewriteRuleSubtreeStream2.Reset();
					this.adaptor.AddChild(obj, obj2);
					expr_return.Tree = obj;
				}
				expr_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					expr_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(expr_return.Tree, expr_return.Start, expr_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				expr_return.Tree = this.adaptor.ErrorNode(this.input, expr_return.Start, this.input.LT(-1), ex);
			}
			return expr_return;
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0006FCB4 File Offset: 0x0006DEB4
		[GrammarRule("termwithoperator")]
		private CssParser.termwithoperator_return termwithoperator()
		{
			CssParser.termwithoperator_return termwithoperator_return = new CssParser.termwithoperator_return(this);
			termwithoperator_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule operator");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream2 = new RewriteRuleSubtreeStream(this.adaptor, "rule term");
			try
			{
				int num = 2;
				int num2 = this.input.LA(1);
				if (num2 == 16 || num2 == 28 || num2 == 31 || num2 == 84)
				{
					num = 1;
				}
				int num3 = num;
				if (num3 == 1)
				{
					base.PushFollow(CssParser.Follow._operator_in_termwithoperator2902);
					CssParser.operator_return operator_return = this.@operator();
					base.PopFollow();
					if (this.state.failed)
					{
						return termwithoperator_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream.Add(operator_return.Tree);
					}
				}
				base.PushFollow(CssParser.Follow._term_in_termwithoperator2905);
				CssParser.term_return term_return = this.term();
				base.PopFollow();
				if (this.state.failed)
				{
					return termwithoperator_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleSubtreeStream2.Add(term_return.Tree);
				}
				if (this.state.backtracking == 0)
				{
					termwithoperator_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (termwithoperator_return != null) ? termwithoperator_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(182, "TERMWITHOPERATOR"), obj2);
					if (rewriteRuleSubtreeStream.HasNext)
					{
						object obj3 = this.adaptor.Nil();
						obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(162, "OPERATOR"), obj3);
						this.adaptor.AddChild(obj3, rewriteRuleSubtreeStream.NextTree());
						this.adaptor.AddChild(obj2, obj3);
					}
					rewriteRuleSubtreeStream.Reset();
					this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream2.NextTree());
					this.adaptor.AddChild(obj, obj2);
					termwithoperator_return.Tree = obj;
				}
				termwithoperator_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					termwithoperator_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(termwithoperator_return.Tree, termwithoperator_return.Start, termwithoperator_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				termwithoperator_return.Tree = this.adaptor.ErrorNode(this.input, termwithoperator_return.Start, this.input.LT(-1), ex);
			}
			return termwithoperator_return;
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0006FF84 File Offset: 0x0006E184
		[GrammarRule("term")]
		private CssParser.term_return term()
		{
			CssParser.term_return term_return = new CssParser.term_return(this);
			term_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			CommonToken commonToken = null;
			CommonToken oneElement = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token NUMBER");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token PERCENTAGE");
			RewriteRuleTokenStream rewriteRuleTokenStream3 = new RewriteRuleTokenStream(this.adaptor, "token LENGTH");
			RewriteRuleTokenStream rewriteRuleTokenStream4 = new RewriteRuleTokenStream(this.adaptor, "token RELATIVELENGTH");
			RewriteRuleTokenStream rewriteRuleTokenStream5 = new RewriteRuleTokenStream(this.adaptor, "token ANGLE");
			RewriteRuleTokenStream rewriteRuleTokenStream6 = new RewriteRuleTokenStream(this.adaptor, "token TIME");
			RewriteRuleTokenStream rewriteRuleTokenStream7 = new RewriteRuleTokenStream(this.adaptor, "token FREQ");
			RewriteRuleTokenStream rewriteRuleTokenStream8 = new RewriteRuleTokenStream(this.adaptor, "token RESOLUTION");
			RewriteRuleTokenStream rewriteRuleTokenStream9 = new RewriteRuleTokenStream(this.adaptor, "token SPEECH");
			RewriteRuleTokenStream rewriteRuleTokenStream10 = new RewriteRuleTokenStream(this.adaptor, "token IMPORTANT_COMMENTS");
			RewriteRuleTokenStream rewriteRuleTokenStream11 = new RewriteRuleTokenStream(this.adaptor, "token URI");
			RewriteRuleTokenStream rewriteRuleTokenStream12 = new RewriteRuleTokenStream(this.adaptor, "token MSIE_EXPRESSION");
			RewriteRuleTokenStream rewriteRuleTokenStream13 = new RewriteRuleTokenStream(this.adaptor, "token IDENT");
			RewriteRuleTokenStream rewriteRuleTokenStream14 = new RewriteRuleTokenStream(this.adaptor, "token STRING");
			RewriteRuleTokenStream rewriteRuleTokenStream15 = new RewriteRuleTokenStream(this.adaptor, "token REPLACEMENTTOKEN");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule unary_operator");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream2 = new RewriteRuleSubtreeStream(this.adaptor, "rule hash");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream3 = new RewriteRuleSubtreeStream(this.adaptor, "rule function");
			try
			{
				int num = 8;
				try
				{
					num = this.dfa65.Predict(this.input);
				}
				catch (NoViableAltException)
				{
					throw;
				}
				switch (num)
				{
				case 1:
				{
					int num2 = 2;
					int num3 = this.input.LA(1);
					if (num3 == 53 || num3 == 71)
					{
						num2 = 1;
					}
					int num4 = num2;
					if (num4 == 1)
					{
						base.PushFollow(CssParser.Follow._unary_operator_in_term2943);
						CssParser.unary_operator_return unary_operator_return = this.unary_operator();
						base.PopFollow();
						if (this.state.failed)
						{
							return term_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleSubtreeStream.Add(unary_operator_return.Tree);
						}
					}
					num4 = this.input.LA(1);
					int num5;
					if (num4 <= 64)
					{
						if (num4 <= 32)
						{
							if (num4 == 6)
							{
								num5 = 5;
								goto IL_327;
							}
							if (num4 == 32)
							{
								num5 = 7;
								goto IL_327;
							}
						}
						else
						{
							if (num4 == 49)
							{
								num5 = 3;
								goto IL_327;
							}
							if (num4 == 64)
							{
								num5 = 1;
								goto IL_327;
							}
						}
					}
					else if (num4 <= 77)
					{
						if (num4 == 69)
						{
							num5 = 2;
							goto IL_327;
						}
						switch (num4)
						{
						case 75:
							num5 = 4;
							goto IL_327;
						case 77:
							num5 = 8;
							goto IL_327;
						}
					}
					else
					{
						if (num4 == 81)
						{
							num5 = 9;
							goto IL_327;
						}
						if (num4 == 90)
						{
							num5 = 6;
							goto IL_327;
						}
					}
					if (this.state.backtracking > 0)
					{
						this.state.failed = true;
						return term_return;
					}
					NoViableAltException ex = new NoViableAltException("", 57, 0, this.input);
					throw ex;
					IL_327:
					switch (num5)
					{
					case 1:
						commonToken = (CommonToken)this.Match(this.input, 64, CssParser.Follow._NUMBER_in_term2951);
						if (this.state.failed)
						{
							return term_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream.Add(commonToken);
						}
						break;
					case 2:
						commonToken = (CommonToken)this.Match(this.input, 69, CssParser.Follow._PERCENTAGE_in_term2959);
						if (this.state.failed)
						{
							return term_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream2.Add(commonToken);
						}
						break;
					case 3:
						commonToken = (CommonToken)this.Match(this.input, 49, CssParser.Follow._LENGTH_in_term2967);
						if (this.state.failed)
						{
							return term_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream3.Add(commonToken);
						}
						break;
					case 4:
						commonToken = (CommonToken)this.Match(this.input, 75, CssParser.Follow._RELATIVELENGTH_in_term2975);
						if (this.state.failed)
						{
							return term_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream4.Add(commonToken);
						}
						break;
					case 5:
						commonToken = (CommonToken)this.Match(this.input, 6, CssParser.Follow._ANGLE_in_term2983);
						if (this.state.failed)
						{
							return term_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream5.Add(commonToken);
						}
						break;
					case 6:
						commonToken = (CommonToken)this.Match(this.input, 90, CssParser.Follow._TIME_in_term2991);
						if (this.state.failed)
						{
							return term_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream6.Add(commonToken);
						}
						break;
					case 7:
						commonToken = (CommonToken)this.Match(this.input, 32, CssParser.Follow._FREQ_in_term2999);
						if (this.state.failed)
						{
							return term_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream7.Add(commonToken);
						}
						break;
					case 8:
						commonToken = (CommonToken)this.Match(this.input, 77, CssParser.Follow._RESOLUTION_in_term3007);
						if (this.state.failed)
						{
							return term_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream8.Add(commonToken);
						}
						break;
					case 9:
						commonToken = (CommonToken)this.Match(this.input, 81, CssParser.Follow._SPEECH_in_term3015);
						if (this.state.failed)
						{
							return term_return;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream9.Add(commonToken);
						}
						break;
					}
					for (;;)
					{
						int num6 = 2;
						int num7 = this.input.LA(1);
						if (num7 == 42)
						{
							num6 = 1;
						}
						num4 = num6;
						if (num4 != 1)
						{
							goto IL_65B;
						}
						CommonToken el = (CommonToken)this.Match(this.input, 42, CssParser.Follow._IMPORTANT_COMMENTS_in_term3020);
						if (this.state.failed)
						{
							break;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream10.Add(el);
						}
					}
					return term_return;
					IL_65B:
					if (this.state.backtracking == 0)
					{
						term_return.Tree = obj;
						RewriteRuleTokenStream rewriteRuleTokenStream16 = new RewriteRuleTokenStream(this.adaptor, "token t", commonToken);
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (term_return != null) ? term_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj2 = this.adaptor.Nil();
						obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(181, "TERM"), obj2);
						if (rewriteRuleSubtreeStream.HasNext)
						{
							this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream.NextTree());
						}
						rewriteRuleSubtreeStream.Reset();
						object obj3 = this.adaptor.Nil();
						obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(160, "NUMBERBASEDVALUE"), obj3);
						this.adaptor.AddChild(obj3, rewriteRuleTokenStream16.NextNode());
						this.adaptor.AddChild(obj2, obj3);
						while (rewriteRuleTokenStream10.HasNext)
						{
							this.adaptor.AddChild(obj2, rewriteRuleTokenStream10.NextNode());
						}
						rewriteRuleTokenStream10.Reset();
						this.adaptor.AddChild(obj, obj2);
						term_return.Tree = obj;
					}
					break;
				}
				case 2:
				{
					CommonToken el2 = (CommonToken)this.Match(this.input, 99, CssParser.Follow._URI_in_term3052);
					if (this.state.failed)
					{
						return term_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream11.Add(el2);
					}
					for (;;)
					{
						int num8 = 2;
						int num9 = this.input.LA(1);
						if (num9 == 42)
						{
							num8 = 1;
						}
						int num4 = num8;
						if (num4 != 1)
						{
							goto IL_846;
						}
						CommonToken el3 = (CommonToken)this.Match(this.input, 42, CssParser.Follow._IMPORTANT_COMMENTS_in_term3054);
						if (this.state.failed)
						{
							break;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream10.Add(el3);
						}
					}
					return term_return;
					IL_846:
					if (this.state.backtracking == 0)
					{
						term_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (term_return != null) ? term_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj4 = this.adaptor.Nil();
						obj4 = this.adaptor.BecomeRoot(this.adaptor.Create(181, "TERM"), obj4);
						object obj5 = this.adaptor.Nil();
						obj5 = this.adaptor.BecomeRoot(this.adaptor.Create(187, "URIBASEDVALUE"), obj5);
						this.adaptor.AddChild(obj5, rewriteRuleTokenStream11.NextNode());
						this.adaptor.AddChild(obj4, obj5);
						while (rewriteRuleTokenStream10.HasNext)
						{
							this.adaptor.AddChild(obj4, rewriteRuleTokenStream10.NextNode());
						}
						rewriteRuleTokenStream10.Reset();
						this.adaptor.AddChild(obj, obj4);
						term_return.Tree = obj;
					}
					break;
				}
				case 3:
				{
					CommonToken commonToken2 = (CommonToken)this.Match(this.input, 54, CssParser.Follow._MSIE_EXPRESSION_in_term3088);
					if (this.state.failed)
					{
						return term_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream12.Add(commonToken2);
					}
					if (this.state.backtracking == 0)
					{
						oneElement = CssParser.TrimMsieExpression((commonToken2 != null) ? commonToken2.Text : null);
					}
					for (;;)
					{
						int num10 = 2;
						int num11 = this.input.LA(1);
						if (num11 == 42)
						{
							num10 = 1;
						}
						int num4 = num10;
						if (num4 != 1)
						{
							goto IL_A1B;
						}
						CommonToken el4 = (CommonToken)this.Match(this.input, 42, CssParser.Follow._IMPORTANT_COMMENTS_in_term3093);
						if (this.state.failed)
						{
							break;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream10.Add(el4);
						}
					}
					return term_return;
					IL_A1B:
					if (this.state.backtracking == 0)
					{
						term_return.Tree = obj;
						RewriteRuleTokenStream rewriteRuleTokenStream17 = new RewriteRuleTokenStream(this.adaptor, "token exp", oneElement);
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (term_return != null) ? term_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj6 = this.adaptor.Nil();
						obj6 = this.adaptor.BecomeRoot(this.adaptor.Create(181, "TERM"), obj6);
						object obj7 = this.adaptor.Nil();
						obj7 = this.adaptor.BecomeRoot(this.adaptor.Create(179, "STRINGBASEDVALUE"), obj7);
						this.adaptor.AddChild(obj7, rewriteRuleTokenStream17.NextNode());
						this.adaptor.AddChild(obj6, obj7);
						while (rewriteRuleTokenStream10.HasNext)
						{
							this.adaptor.AddChild(obj6, rewriteRuleTokenStream10.NextNode());
						}
						rewriteRuleTokenStream10.Reset();
						this.adaptor.AddChild(obj, obj6);
						term_return.Tree = obj;
					}
					break;
				}
				case 4:
				{
					CommonToken el5 = (CommonToken)this.Match(this.input, 41, CssParser.Follow._IDENT_in_term3122);
					if (this.state.failed)
					{
						return term_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream13.Add(el5);
					}
					for (;;)
					{
						int num12 = 2;
						int num13 = this.input.LA(1);
						if (num13 == 42)
						{
							num12 = 1;
						}
						int num4 = num12;
						if (num4 != 1)
						{
							goto IL_BE2;
						}
						CommonToken el6 = (CommonToken)this.Match(this.input, 42, CssParser.Follow._IMPORTANT_COMMENTS_in_term3124);
						if (this.state.failed)
						{
							break;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream10.Add(el6);
						}
					}
					return term_return;
					IL_BE2:
					if (this.state.backtracking == 0)
					{
						term_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (term_return != null) ? term_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj8 = this.adaptor.Nil();
						obj8 = this.adaptor.BecomeRoot(this.adaptor.Create(181, "TERM"), obj8);
						object obj9 = this.adaptor.Nil();
						obj9 = this.adaptor.BecomeRoot(this.adaptor.Create(137, "IDENTBASEDVALUE"), obj9);
						this.adaptor.AddChild(obj9, rewriteRuleTokenStream13.NextNode());
						this.adaptor.AddChild(obj8, obj9);
						while (rewriteRuleTokenStream10.HasNext)
						{
							this.adaptor.AddChild(obj8, rewriteRuleTokenStream10.NextNode());
						}
						rewriteRuleTokenStream10.Reset();
						this.adaptor.AddChild(obj, obj8);
						term_return.Tree = obj;
					}
					break;
				}
				case 5:
				{
					CommonToken el7 = (CommonToken)this.Match(this.input, 85, CssParser.Follow._STRING_in_term3152);
					if (this.state.failed)
					{
						return term_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream14.Add(el7);
					}
					for (;;)
					{
						int num14 = 2;
						int num15 = this.input.LA(1);
						if (num15 == 42)
						{
							num14 = 1;
						}
						int num4 = num14;
						if (num4 != 1)
						{
							goto IL_D96;
						}
						CommonToken el8 = (CommonToken)this.Match(this.input, 42, CssParser.Follow._IMPORTANT_COMMENTS_in_term3154);
						if (this.state.failed)
						{
							break;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream10.Add(el8);
						}
					}
					return term_return;
					IL_D96:
					if (this.state.backtracking == 0)
					{
						term_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (term_return != null) ? term_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj10 = this.adaptor.Nil();
						obj10 = this.adaptor.BecomeRoot(this.adaptor.Create(181, "TERM"), obj10);
						object obj11 = this.adaptor.Nil();
						obj11 = this.adaptor.BecomeRoot(this.adaptor.Create(179, "STRINGBASEDVALUE"), obj11);
						this.adaptor.AddChild(obj11, rewriteRuleTokenStream14.NextNode());
						this.adaptor.AddChild(obj10, obj11);
						while (rewriteRuleTokenStream10.HasNext)
						{
							this.adaptor.AddChild(obj10, rewriteRuleTokenStream10.NextNode());
						}
						rewriteRuleTokenStream10.Reset();
						this.adaptor.AddChild(obj, obj10);
						term_return.Tree = obj;
					}
					break;
				}
				case 6:
				{
					base.PushFollow(CssParser.Follow._hash_in_term3182);
					CssParser.hash_return hash_return = this.hash();
					base.PopFollow();
					if (this.state.failed)
					{
						return term_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream2.Add(hash_return.Tree);
					}
					for (;;)
					{
						int num16 = 2;
						int num17 = this.input.LA(1);
						if (num17 == 42)
						{
							num16 = 1;
						}
						int num4 = num16;
						if (num4 != 1)
						{
							goto IL_F4E;
						}
						CommonToken el9 = (CommonToken)this.Match(this.input, 42, CssParser.Follow._IMPORTANT_COMMENTS_in_term3184);
						if (this.state.failed)
						{
							break;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream10.Add(el9);
						}
					}
					return term_return;
					IL_F4E:
					if (this.state.backtracking == 0)
					{
						term_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (term_return != null) ? term_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj12 = this.adaptor.Nil();
						obj12 = this.adaptor.BecomeRoot(this.adaptor.Create(181, "TERM"), obj12);
						object obj13 = this.adaptor.Nil();
						obj13 = this.adaptor.BecomeRoot(this.adaptor.Create(136, "HEXBASEDVALUE"), obj13);
						this.adaptor.AddChild(obj13, rewriteRuleSubtreeStream2.NextTree());
						this.adaptor.AddChild(obj12, obj13);
						while (rewriteRuleTokenStream10.HasNext)
						{
							this.adaptor.AddChild(obj12, rewriteRuleTokenStream10.NextNode());
						}
						rewriteRuleTokenStream10.Reset();
						this.adaptor.AddChild(obj, obj12);
						term_return.Tree = obj;
					}
					break;
				}
				case 7:
				{
					CommonToken el10 = (CommonToken)this.Match(this.input, 76, CssParser.Follow._REPLACEMENTTOKEN_in_term3209);
					if (this.state.failed)
					{
						return term_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream15.Add(el10);
					}
					if (this.state.backtracking == 0)
					{
						term_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (term_return != null) ? term_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj14 = this.adaptor.Nil();
						obj14 = this.adaptor.BecomeRoot(this.adaptor.Create(181, "TERM"), obj14);
						object obj15 = this.adaptor.Nil();
						obj15 = this.adaptor.BecomeRoot(this.adaptor.Create(169, "REPLACEMENTTOKENBASEDVALUE"), obj15);
						this.adaptor.AddChild(obj15, rewriteRuleTokenStream15.NextNode());
						this.adaptor.AddChild(obj14, obj15);
						this.adaptor.AddChild(obj, obj14);
						term_return.Tree = obj;
					}
					break;
				}
				case 8:
				{
					base.PushFollow(CssParser.Follow._function_in_term3233);
					CssParser.function_return function_return = this.function();
					base.PopFollow();
					if (this.state.failed)
					{
						return term_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream3.Add(function_return.Tree);
					}
					for (;;)
					{
						int num18 = 2;
						int num19 = this.input.LA(1);
						if (num19 == 42)
						{
							num18 = 1;
						}
						int num4 = num18;
						if (num4 != 1)
						{
							goto IL_122A;
						}
						CommonToken el11 = (CommonToken)this.Match(this.input, 42, CssParser.Follow._IMPORTANT_COMMENTS_in_term3235);
						if (this.state.failed)
						{
							break;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream10.Add(el11);
						}
					}
					return term_return;
					IL_122A:
					if (this.state.backtracking == 0)
					{
						term_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (term_return != null) ? term_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj16 = this.adaptor.Nil();
						obj16 = this.adaptor.BecomeRoot(this.adaptor.Create(181, "TERM"), obj16);
						this.adaptor.AddChild(obj16, rewriteRuleSubtreeStream3.NextTree());
						while (rewriteRuleTokenStream10.HasNext)
						{
							this.adaptor.AddChild(obj16, rewriteRuleTokenStream10.NextNode());
						}
						rewriteRuleTokenStream10.Reset();
						this.adaptor.AddChild(obj, obj16);
						term_return.Tree = obj;
					}
					break;
				}
				}
				term_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					term_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(term_return.Tree, term_return.Start, term_return.Stop);
				}
			}
			catch (RecognitionException ex2)
			{
				this.ReportError(ex2);
				this.Recover(this.input, ex2);
				term_return.Tree = this.adaptor.ErrorNode(this.input, term_return.Start, this.input.LT(-1), ex2);
			}
			return term_return;
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x00071350 File Offset: 0x0006F550
		[GrammarRule("hash")]
		private CssParser.hash_return hash()
		{
			CssParser.hash_return hash_return = new CssParser.hash_return(this);
			hash_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token HASH_IDENT");
			try
			{
				CommonToken el = (CommonToken)this.Match(this.input, 38, CssParser.Follow._HASH_IDENT_in_hash3268);
				if (this.state.failed)
				{
					return hash_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream.Add(el);
				}
				if (this.state.backtracking == 0)
				{
					hash_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (hash_return != null) ? hash_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(135, "HASHIDENTIFIER"), obj2);
					this.adaptor.AddChild(obj2, rewriteRuleTokenStream.NextNode());
					this.adaptor.AddChild(obj, obj2);
					hash_return.Tree = obj;
				}
				hash_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					hash_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(hash_return.Tree, hash_return.Start, hash_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				hash_return.Tree = this.adaptor.ErrorNode(this.input, hash_return.Start, this.input.LT(-1), ex);
			}
			return hash_return;
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00071528 File Offset: 0x0006F728
		[GrammarRule("function")]
		private CssParser.function_return function()
		{
			CssParser.function_return function_return = new CssParser.function_return(this);
			function_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token CIRCLE_END");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule beginfunc");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream2 = new RewriteRuleSubtreeStream(this.adaptor, "rule expr");
			try
			{
				base.PushFollow(CssParser.Follow._beginfunc_in_function3300);
				CssParser.beginfunc_return beginfunc_return = this.beginfunc();
				base.PopFollow();
				if (this.state.failed)
				{
					return function_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleSubtreeStream.Add(beginfunc_return.Tree);
				}
				int num = 2;
				int num2 = this.input.LA(1);
				if (num2 == 6 || (num2 >= 32 && num2 <= 33) || (num2 == 38 || (num2 >= 41 && num2 <= 42)) || (num2 == 49 || (num2 >= 53 && num2 <= 55)) || (num2 == 64 || num2 == 69 || num2 == 71 || (num2 >= 75 && num2 <= 77)) || (num2 == 81 || num2 == 85 || (num2 >= 90 && num2 <= 91)) || num2 == 99)
				{
					num = 1;
				}
				int num3 = num;
				if (num3 == 1)
				{
					base.PushFollow(CssParser.Follow._expr_in_function3302);
					CssParser.expr_return expr_return = this.expr();
					base.PopFollow();
					if (this.state.failed)
					{
						return function_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream2.Add(expr_return.Tree);
					}
				}
				CommonToken el = (CommonToken)this.Match(this.input, 13, CssParser.Follow._CIRCLE_END_in_function3305);
				if (this.state.failed)
				{
					return function_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream.Add(el);
				}
				if (this.state.backtracking == 0)
				{
					function_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (function_return != null) ? function_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(130, "FUNCTIONBASEDVALUE"), obj2);
					this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream.NextTree());
					if (rewriteRuleSubtreeStream2.HasNext)
					{
						this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream2.NextTree());
					}
					rewriteRuleSubtreeStream2.Reset();
					this.adaptor.AddChild(obj, obj2);
					function_return.Tree = obj;
				}
				function_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					function_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(function_return.Tree, function_return.Start, function_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				function_return.Tree = this.adaptor.ErrorNode(this.input, function_return.Start, this.input.LT(-1), ex);
			}
			return function_return;
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0007186C File Offset: 0x0006FA6C
		[GrammarRule("beginfunc")]
		private CssParser.beginfunc_return beginfunc()
		{
			CssParser.beginfunc_return beginfunc_return = new CssParser.beginfunc_return(this);
			beginfunc_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token IDENT");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token CIRCLE_BEGIN");
			RewriteRuleTokenStream rewriteRuleTokenStream3 = new RewriteRuleTokenStream(this.adaptor, "token FROM");
			RewriteRuleTokenStream rewriteRuleTokenStream4 = new RewriteRuleTokenStream(this.adaptor, "token TO");
			RewriteRuleTokenStream rewriteRuleTokenStream5 = new RewriteRuleTokenStream(this.adaptor, "token MSIE_IMAGE_TRANSFORM");
			try
			{
				int num = this.input.LA(1);
				int num2;
				if (num <= 41)
				{
					if (num == 33)
					{
						num2 = 2;
						goto IL_111;
					}
					if (num == 41)
					{
						num2 = 1;
						goto IL_111;
					}
				}
				else
				{
					if (num == 55)
					{
						num2 = 4;
						goto IL_111;
					}
					if (num == 91)
					{
						num2 = 3;
						goto IL_111;
					}
				}
				if (this.state.backtracking > 0)
				{
					this.state.failed = true;
					return beginfunc_return;
				}
				NoViableAltException ex = new NoViableAltException("", 67, 0, this.input);
				throw ex;
				IL_111:
				switch (num2)
				{
				case 1:
				{
					CommonToken el = (CommonToken)this.Match(this.input, 41, CssParser.Follow._IDENT_in_beginfunc3337);
					if (this.state.failed)
					{
						return beginfunc_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream.Add(el);
					}
					CommonToken el2 = (CommonToken)this.Match(this.input, 12, CssParser.Follow._CIRCLE_BEGIN_in_beginfunc3339);
					if (this.state.failed)
					{
						return beginfunc_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream2.Add(el2);
					}
					if (this.state.backtracking == 0)
					{
						beginfunc_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (beginfunc_return != null) ? beginfunc_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj2 = this.adaptor.Nil();
						obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(131, "FUNCTIONNAME"), obj2);
						this.adaptor.AddChild(obj2, rewriteRuleTokenStream.NextNode());
						this.adaptor.AddChild(obj, obj2);
						beginfunc_return.Tree = obj;
					}
					break;
				}
				case 2:
				{
					CommonToken el3 = (CommonToken)this.Match(this.input, 33, CssParser.Follow._FROM_in_beginfunc3361);
					if (this.state.failed)
					{
						return beginfunc_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream3.Add(el3);
					}
					CommonToken el4 = (CommonToken)this.Match(this.input, 12, CssParser.Follow._CIRCLE_BEGIN_in_beginfunc3363);
					if (this.state.failed)
					{
						return beginfunc_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream2.Add(el4);
					}
					if (this.state.backtracking == 0)
					{
						beginfunc_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (beginfunc_return != null) ? beginfunc_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj3 = this.adaptor.Nil();
						obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(131, "FUNCTIONNAME"), obj3);
						this.adaptor.AddChild(obj3, rewriteRuleTokenStream3.NextNode());
						this.adaptor.AddChild(obj, obj3);
						beginfunc_return.Tree = obj;
					}
					break;
				}
				case 3:
				{
					CommonToken el5 = (CommonToken)this.Match(this.input, 91, CssParser.Follow._TO_in_beginfunc3383);
					if (this.state.failed)
					{
						return beginfunc_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream4.Add(el5);
					}
					CommonToken el6 = (CommonToken)this.Match(this.input, 12, CssParser.Follow._CIRCLE_BEGIN_in_beginfunc3385);
					if (this.state.failed)
					{
						return beginfunc_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream2.Add(el6);
					}
					if (this.state.backtracking == 0)
					{
						beginfunc_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (beginfunc_return != null) ? beginfunc_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj4 = this.adaptor.Nil();
						obj4 = this.adaptor.BecomeRoot(this.adaptor.Create(131, "FUNCTIONNAME"), obj4);
						this.adaptor.AddChild(obj4, rewriteRuleTokenStream4.NextNode());
						this.adaptor.AddChild(obj, obj4);
						beginfunc_return.Tree = obj;
					}
					break;
				}
				case 4:
				{
					CommonToken el7 = (CommonToken)this.Match(this.input, 55, CssParser.Follow._MSIE_IMAGE_TRANSFORM_in_beginfunc3406);
					if (this.state.failed)
					{
						return beginfunc_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream5.Add(el7);
					}
					CommonToken el8 = (CommonToken)this.Match(this.input, 12, CssParser.Follow._CIRCLE_BEGIN_in_beginfunc3408);
					if (this.state.failed)
					{
						return beginfunc_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream2.Add(el8);
					}
					if (this.state.backtracking == 0)
					{
						beginfunc_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (beginfunc_return != null) ? beginfunc_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj5 = this.adaptor.Nil();
						obj5 = this.adaptor.BecomeRoot(this.adaptor.Create(131, "FUNCTIONNAME"), obj5);
						this.adaptor.AddChild(obj5, rewriteRuleTokenStream5.NextNode());
						this.adaptor.AddChild(obj, obj5);
						beginfunc_return.Tree = obj;
					}
					break;
				}
				}
				beginfunc_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					beginfunc_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(beginfunc_return.Tree, beginfunc_return.Start, beginfunc_return.Stop);
				}
			}
			catch (RecognitionException ex2)
			{
				this.ReportError(ex2);
				this.Recover(this.input, ex2);
				beginfunc_return.Tree = this.adaptor.ErrorNode(this.input, beginfunc_return.Start, this.input.LT(-1), ex2);
			}
			return beginfunc_return;
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x00071F04 File Offset: 0x00070104
		[GrammarRule("keyframes")]
		private CssParser.keyframes_return keyframes()
		{
			CssParser.keyframes_return keyframes_return = new CssParser.keyframes_return(this);
			keyframes_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token KEYFRAMES_SYM");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token IDENT");
			RewriteRuleTokenStream rewriteRuleTokenStream3 = new RewriteRuleTokenStream(this.adaptor, "token STRING");
			RewriteRuleTokenStream rewriteRuleTokenStream4 = new RewriteRuleTokenStream(this.adaptor, "token CURLY_BEGIN");
			RewriteRuleTokenStream rewriteRuleTokenStream5 = new RewriteRuleTokenStream(this.adaptor, "token CURLY_END");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule keyframes_block");
			try
			{
				CommonToken el = (CommonToken)this.Match(this.input, 47, CssParser.Follow._KEYFRAMES_SYM_in_keyframes3438);
				if (this.state.failed)
				{
					return keyframes_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream.Add(el);
				}
				int num = this.input.LA(1);
				int num2;
				if (num == 41)
				{
					num2 = 1;
				}
				else if (num == 85)
				{
					num2 = 2;
				}
				else
				{
					if (this.state.backtracking > 0)
					{
						this.state.failed = true;
						return keyframes_return;
					}
					NoViableAltException ex = new NoViableAltException("", 68, 0, this.input);
					throw ex;
				}
				switch (num2)
				{
				case 1:
				{
					CommonToken el2 = (CommonToken)this.Match(this.input, 41, CssParser.Follow._IDENT_in_keyframes3441);
					if (this.state.failed)
					{
						return keyframes_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream2.Add(el2);
					}
					break;
				}
				case 2:
				{
					CommonToken el3 = (CommonToken)this.Match(this.input, 85, CssParser.Follow._STRING_in_keyframes3443);
					if (this.state.failed)
					{
						return keyframes_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream3.Add(el3);
					}
					break;
				}
				}
				CommonToken el4 = (CommonToken)this.Match(this.input, 18, CssParser.Follow._CURLY_BEGIN_in_keyframes3446);
				if (this.state.failed)
				{
					return keyframes_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream4.Add(el4);
				}
				for (;;)
				{
					int num3 = 2;
					int num4 = this.input.LA(1);
					if (num4 == 33 || num4 == 69 || num4 == 91)
					{
						num3 = 1;
					}
					int num5 = num3;
					if (num5 != 1)
					{
						goto IL_2A3;
					}
					base.PushFollow(CssParser.Follow._keyframes_block_in_keyframes3448);
					CssParser.keyframes_block_return keyframes_block_return = this.keyframes_block();
					base.PopFollow();
					if (this.state.failed)
					{
						break;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream.Add(keyframes_block_return.Tree);
					}
				}
				return keyframes_return;
				IL_2A3:
				CommonToken el5 = (CommonToken)this.Match(this.input, 19, CssParser.Follow._CURLY_END_in_keyframes3451);
				if (this.state.failed)
				{
					return keyframes_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream5.Add(el5);
				}
				if (this.state.backtracking == 0)
				{
					keyframes_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (keyframes_return != null) ? keyframes_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(141, "KEYFRAMES"), obj2);
					object obj3 = this.adaptor.Nil();
					obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(146, "KEYFRAMES_SYMBOL"), obj3);
					this.adaptor.AddChild(obj3, rewriteRuleTokenStream.NextNode());
					this.adaptor.AddChild(obj2, obj3);
					if (rewriteRuleTokenStream2.HasNext)
					{
						object obj4 = this.adaptor.Nil();
						obj4 = this.adaptor.BecomeRoot(this.adaptor.Create(137, "IDENTBASEDVALUE"), obj4);
						this.adaptor.AddChild(obj4, rewriteRuleTokenStream2.NextNode());
						this.adaptor.AddChild(obj2, obj4);
					}
					rewriteRuleTokenStream2.Reset();
					if (rewriteRuleTokenStream3.HasNext)
					{
						object obj5 = this.adaptor.Nil();
						obj5 = this.adaptor.BecomeRoot(this.adaptor.Create(179, "STRINGBASEDVALUE"), obj5);
						this.adaptor.AddChild(obj5, rewriteRuleTokenStream3.NextNode());
						this.adaptor.AddChild(obj2, obj5);
					}
					rewriteRuleTokenStream3.Reset();
					if (rewriteRuleSubtreeStream.HasNext)
					{
						object obj6 = this.adaptor.Nil();
						obj6 = this.adaptor.BecomeRoot(this.adaptor.Create(143, "KEYFRAMES_BLOCKS"), obj6);
						while (rewriteRuleSubtreeStream.HasNext)
						{
							this.adaptor.AddChild(obj6, rewriteRuleSubtreeStream.NextTree());
						}
						rewriteRuleSubtreeStream.Reset();
						this.adaptor.AddChild(obj2, obj6);
					}
					rewriteRuleSubtreeStream.Reset();
					this.adaptor.AddChild(obj, obj2);
					keyframes_return.Tree = obj;
				}
				keyframes_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					keyframes_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(keyframes_return.Tree, keyframes_return.Start, keyframes_return.Stop);
				}
			}
			catch (RecognitionException ex2)
			{
				this.ReportError(ex2);
				this.Recover(this.input, ex2);
				keyframes_return.Tree = this.adaptor.ErrorNode(this.input, keyframes_return.Start, this.input.LT(-1), ex2);
			}
			return keyframes_return;
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x000724CC File Offset: 0x000706CC
		[GrammarRule("keyframes_block")]
		private CssParser.keyframes_block_return keyframes_block()
		{
			CssParser.keyframes_block_return keyframes_block_return = new CssParser.keyframes_block_return(this);
			keyframes_block_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token CURLY_BEGIN");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token SEMICOLON");
			RewriteRuleTokenStream rewriteRuleTokenStream3 = new RewriteRuleTokenStream(this.adaptor, "token CURLY_END");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule keyframes_selectors");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream2 = new RewriteRuleSubtreeStream(this.adaptor, "rule declaration");
			try
			{
				base.PushFollow(CssParser.Follow._keyframes_selectors_in_keyframes_block3507);
				CssParser.keyframes_selectors_return keyframes_selectors_return = this.keyframes_selectors();
				base.PopFollow();
				if (this.state.failed)
				{
					return keyframes_block_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleSubtreeStream.Add(keyframes_selectors_return.Tree);
				}
				CommonToken el = (CommonToken)this.Match(this.input, 18, CssParser.Follow._CURLY_BEGIN_in_keyframes_block3509);
				if (this.state.failed)
				{
					return keyframes_block_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream.Add(el);
				}
				for (;;)
				{
					int num = 2;
					int num2 = this.input.LA(1);
					if ((num2 >= 41 && num2 <= 42) || num2 == 76 || num2 == 84)
					{
						num = 1;
					}
					int num3 = num;
					if (num3 != 1)
					{
						goto IL_205;
					}
					base.PushFollow(CssParser.Follow._declaration_in_keyframes_block3512);
					CssParser.declaration_return declaration_return = this.declaration();
					base.PopFollow();
					if (this.state.failed)
					{
						break;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream2.Add(declaration_return.Tree);
					}
					int num4 = 2;
					int num5 = this.input.LA(1);
					if (num5 == 79)
					{
						num4 = 1;
					}
					int num6 = num4;
					if (num6 == 1)
					{
						CommonToken el2 = (CommonToken)this.Match(this.input, 79, CssParser.Follow._SEMICOLON_in_keyframes_block3514);
						if (this.state.failed)
						{
							goto Block_14;
						}
						if (this.state.backtracking == 0)
						{
							rewriteRuleTokenStream2.Add(el2);
						}
					}
				}
				return keyframes_block_return;
				Block_14:
				return keyframes_block_return;
				IL_205:
				CommonToken el3 = (CommonToken)this.Match(this.input, 19, CssParser.Follow._CURLY_END_in_keyframes_block3519);
				if (this.state.failed)
				{
					return keyframes_block_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream3.Add(el3);
				}
				if (this.state.backtracking == 0)
				{
					keyframes_block_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (keyframes_block_return != null) ? keyframes_block_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(142, "KEYFRAMES_BLOCK"), obj2);
					object obj3 = this.adaptor.Nil();
					obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(145, "KEYFRAMES_SELECTORS"), obj3);
					this.adaptor.AddChild(obj3, rewriteRuleSubtreeStream.NextTree());
					this.adaptor.AddChild(obj2, obj3);
					if (rewriteRuleSubtreeStream2.HasNext)
					{
						object obj4 = this.adaptor.Nil();
						obj4 = this.adaptor.BecomeRoot(this.adaptor.Create(123, "DECLARATIONS"), obj4);
						while (rewriteRuleSubtreeStream2.HasNext)
						{
							this.adaptor.AddChild(obj4, rewriteRuleSubtreeStream2.NextTree());
						}
						rewriteRuleSubtreeStream2.Reset();
						this.adaptor.AddChild(obj2, obj4);
					}
					rewriteRuleSubtreeStream2.Reset();
					this.adaptor.AddChild(obj, obj2);
					keyframes_block_return.Tree = obj;
				}
				keyframes_block_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					keyframes_block_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(keyframes_block_return.Tree, keyframes_block_return.Start, keyframes_block_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				keyframes_block_return.Tree = this.adaptor.ErrorNode(this.input, keyframes_block_return.Start, this.input.LT(-1), ex);
			}
			return keyframes_block_return;
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0007292C File Offset: 0x00070B2C
		[GrammarRule("keyframes_selectors")]
		private CssParser.keyframes_selectors_return keyframes_selectors()
		{
			CssParser.keyframes_selectors_return keyframes_selectors_return = new CssParser.keyframes_selectors_return(this);
			keyframes_selectors_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token COMMA");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule keyframes_selector");
			try
			{
				base.PushFollow(CssParser.Follow._keyframes_selector_in_keyframes_selectors3561);
				CssParser.keyframes_selector_return keyframes_selector_return = this.keyframes_selector();
				base.PopFollow();
				if (this.state.failed)
				{
					return keyframes_selectors_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleSubtreeStream.Add(keyframes_selector_return.Tree);
				}
				for (;;)
				{
					int num = 2;
					int num2 = this.input.LA(1);
					if (num2 == 16)
					{
						num = 1;
					}
					int num3 = num;
					if (num3 != 1)
					{
						goto IL_14C;
					}
					CommonToken el = (CommonToken)this.Match(this.input, 16, CssParser.Follow._COMMA_in_keyframes_selectors3564);
					if (this.state.failed)
					{
						break;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream.Add(el);
					}
					base.PushFollow(CssParser.Follow._keyframes_selector_in_keyframes_selectors3566);
					CssParser.keyframes_selector_return keyframes_selector_return2 = this.keyframes_selector();
					base.PopFollow();
					if (this.state.failed)
					{
						goto Block_9;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream.Add(keyframes_selector_return2.Tree);
					}
				}
				return keyframes_selectors_return;
				Block_9:
				return keyframes_selectors_return;
				IL_14C:
				if (this.state.backtracking == 0)
				{
					keyframes_selectors_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (keyframes_selectors_return != null) ? keyframes_selectors_return.Tree : null);
					obj = this.adaptor.Nil();
					while (rewriteRuleSubtreeStream.HasNext)
					{
						object obj2 = this.adaptor.Nil();
						obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(144, "KEYFRAMES_SELECTOR"), obj2);
						this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream.NextTree());
						this.adaptor.AddChild(obj, obj2);
					}
					rewriteRuleSubtreeStream.Reset();
					keyframes_selectors_return.Tree = obj;
				}
				keyframes_selectors_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					keyframes_selectors_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(keyframes_selectors_return.Tree, keyframes_selectors_return.Start, keyframes_selectors_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				keyframes_selectors_return.Tree = this.adaptor.ErrorNode(this.input, keyframes_selectors_return.Start, this.input.LT(-1), ex);
			}
			return keyframes_selectors_return;
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00072BEC File Offset: 0x00070DEC
		[GrammarRule("keyframes_selector")]
		private CssParser.keyframes_selector_return keyframes_selector()
		{
			CssParser.keyframes_selector_return keyframes_selector_return = new CssParser.keyframes_selector_return(this);
			keyframes_selector_return.Start = (CommonToken)this.input.LT(1);
			try
			{
				object obj = this.adaptor.Nil();
				CommonToken payload = (CommonToken)this.input.LT(1);
				if (this.input.LA(1) == 33 || this.input.LA(1) == 69 || this.input.LA(1) == 91)
				{
					this.input.Consume();
					if (this.state.backtracking == 0)
					{
						this.adaptor.AddChild(obj, this.adaptor.Create(payload));
					}
					this.state.errorRecovery = false;
					this.state.failed = false;
					keyframes_selector_return.Stop = (CommonToken)this.input.LT(-1);
					if (this.state.backtracking == 0)
					{
						keyframes_selector_return.Tree = this.adaptor.RulePostProcessing(obj);
						this.adaptor.SetTokenBoundaries(keyframes_selector_return.Tree, keyframes_selector_return.Start, keyframes_selector_return.Stop);
					}
				}
				else
				{
					if (this.state.backtracking > 0)
					{
						this.state.failed = true;
						return keyframes_selector_return;
					}
					MismatchedSetException ex = new MismatchedSetException(null, this.input);
					throw ex;
				}
			}
			catch (RecognitionException ex2)
			{
				this.ReportError(ex2);
				this.Recover(this.input, ex2);
				keyframes_selector_return.Tree = this.adaptor.ErrorNode(this.input, keyframes_selector_return.Start, this.input.LT(-1), ex2);
			}
			return keyframes_selector_return;
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00072DA0 File Offset: 0x00070FA0
		[GrammarRule("document")]
		private CssParser.document_return document()
		{
			CssParser.document_return document_return = new CssParser.document_return(this);
			document_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token DOCUMENT_SYM");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token S");
			RewriteRuleTokenStream rewriteRuleTokenStream3 = new RewriteRuleTokenStream(this.adaptor, "token CURLY_BEGIN");
			RewriteRuleTokenStream rewriteRuleTokenStream4 = new RewriteRuleTokenStream(this.adaptor, "token CURLY_END");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream = new RewriteRuleSubtreeStream(this.adaptor, "rule document_match_function");
			RewriteRuleSubtreeStream rewriteRuleSubtreeStream2 = new RewriteRuleSubtreeStream(this.adaptor, "rule ruleset");
			try
			{
				CommonToken el = (CommonToken)this.Match(this.input, 24, CssParser.Follow._DOCUMENT_SYM_in_document3619);
				if (this.state.failed)
				{
					return document_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream.Add(el);
				}
				for (;;)
				{
					int num = 2;
					int num2 = this.input.LA(1);
					if (num2 == 78)
					{
						num = 1;
					}
					int num3 = num;
					if (num3 != 1)
					{
						goto IL_14A;
					}
					CommonToken el2 = (CommonToken)this.Match(this.input, 78, CssParser.Follow._S_in_document3621);
					if (this.state.failed)
					{
						break;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream2.Add(el2);
					}
				}
				return document_return;
				IL_14A:
				base.PushFollow(CssParser.Follow._document_match_function_in_document3624);
				CssParser.document_match_function_return document_match_function_return = this.document_match_function();
				base.PopFollow();
				if (this.state.failed)
				{
					return document_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleSubtreeStream.Add(document_match_function_return.Tree);
				}
				for (;;)
				{
					int num4 = 2;
					int num5 = this.input.LA(1);
					if (num5 == 78)
					{
						num4 = 1;
					}
					int num6 = num4;
					if (num6 != 1)
					{
						goto IL_1FD;
					}
					CommonToken el3 = (CommonToken)this.Match(this.input, 78, CssParser.Follow._S_in_document3626);
					if (this.state.failed)
					{
						break;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream2.Add(el3);
					}
				}
				return document_return;
				IL_1FD:
				CommonToken el4 = (CommonToken)this.Match(this.input, 18, CssParser.Follow._CURLY_BEGIN_in_document3629);
				if (this.state.failed)
				{
					return document_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream3.Add(el4);
				}
				for (;;)
				{
					int num7 = 2;
					int num8 = this.input.LA(1);
					if (num8 == 7 || (num8 >= 14 && num8 <= 15) || num8 == 38 || num8 == 41 || num8 == 70 || num8 == 76 || num8 == 82 || num8 == 84)
					{
						num7 = 1;
					}
					int num9 = num7;
					if (num9 != 1)
					{
						goto IL_2E5;
					}
					base.PushFollow(CssParser.Follow._ruleset_in_document3631);
					CssParser.ruleset_return ruleset_return = this.ruleset();
					base.PopFollow();
					if (this.state.failed)
					{
						break;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleSubtreeStream2.Add(ruleset_return.Tree);
					}
				}
				return document_return;
				IL_2E5:
				CommonToken el5 = (CommonToken)this.Match(this.input, 19, CssParser.Follow._CURLY_END_in_document3634);
				if (this.state.failed)
				{
					return document_return;
				}
				if (this.state.backtracking == 0)
				{
					rewriteRuleTokenStream4.Add(el5);
				}
				if (this.state.backtracking == 0)
				{
					document_return.Tree = obj;
					new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (document_return != null) ? document_return.Tree : null);
					obj = this.adaptor.Nil();
					object obj2 = this.adaptor.Nil();
					obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(124, "DOCUMENT"), obj2);
					object obj3 = this.adaptor.Nil();
					obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(126, "DOCUMENT_SYMBOL"), obj3);
					this.adaptor.AddChild(obj3, rewriteRuleTokenStream.NextNode());
					this.adaptor.AddChild(obj2, obj3);
					this.adaptor.AddChild(obj2, rewriteRuleSubtreeStream.NextTree());
					if (rewriteRuleSubtreeStream2.HasNext)
					{
						object obj4 = this.adaptor.Nil();
						obj4 = this.adaptor.BecomeRoot(this.adaptor.Create(172, "RULESETS"), obj4);
						while (rewriteRuleSubtreeStream2.HasNext)
						{
							this.adaptor.AddChild(obj4, rewriteRuleSubtreeStream2.NextTree());
						}
						rewriteRuleSubtreeStream2.Reset();
						this.adaptor.AddChild(obj2, obj4);
					}
					rewriteRuleSubtreeStream2.Reset();
					this.adaptor.AddChild(obj, obj2);
					document_return.Tree = obj;
				}
				document_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					document_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(document_return.Tree, document_return.Start, document_return.Stop);
				}
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				document_return.Tree = this.adaptor.ErrorNode(this.input, document_return.Start, this.input.LT(-1), ex);
			}
			return document_return;
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x000732F0 File Offset: 0x000714F0
		[GrammarRule("document_match_function")]
		private CssParser.document_match_function_return document_match_function()
		{
			CssParser.document_match_function_return document_match_function_return = new CssParser.document_match_function_return(this);
			document_match_function_return.Start = (CommonToken)this.input.LT(1);
			object obj = null;
			RewriteRuleTokenStream rewriteRuleTokenStream = new RewriteRuleTokenStream(this.adaptor, "token URI");
			RewriteRuleTokenStream rewriteRuleTokenStream2 = new RewriteRuleTokenStream(this.adaptor, "token URLPREFIX_FUNCTION");
			RewriteRuleTokenStream rewriteRuleTokenStream3 = new RewriteRuleTokenStream(this.adaptor, "token DOMAIN_FUNCTION");
			RewriteRuleTokenStream rewriteRuleTokenStream4 = new RewriteRuleTokenStream(this.adaptor, "token REGEXP_FUNCTION");
			try
			{
				int num = this.input.LA(1);
				int num2;
				if (num != 25)
				{
					if (num != 74)
					{
						switch (num)
						{
						case 99:
							num2 = 1;
							goto IL_F5;
						case 101:
							num2 = 2;
							goto IL_F5;
						}
						if (this.state.backtracking > 0)
						{
							this.state.failed = true;
							return document_match_function_return;
						}
						NoViableAltException ex = new NoViableAltException("", 76, 0, this.input);
						throw ex;
					}
					else
					{
						num2 = 4;
					}
				}
				else
				{
					num2 = 3;
				}
				IL_F5:
				switch (num2)
				{
				case 1:
				{
					CommonToken el = (CommonToken)this.Match(this.input, 99, CssParser.Follow._URI_in_document_match_function3678);
					if (this.state.failed)
					{
						return document_match_function_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream.Add(el);
					}
					if (this.state.backtracking == 0)
					{
						document_match_function_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (document_match_function_return != null) ? document_match_function_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj2 = this.adaptor.Nil();
						obj2 = this.adaptor.BecomeRoot(this.adaptor.Create(125, "DOCUMENT_MATCHNAME"), obj2);
						this.adaptor.AddChild(obj2, rewriteRuleTokenStream.NextNode());
						this.adaptor.AddChild(obj, obj2);
						document_match_function_return.Tree = obj;
					}
					break;
				}
				case 2:
				{
					CommonToken el2 = (CommonToken)this.Match(this.input, 101, CssParser.Follow._URLPREFIX_FUNCTION_in_document_match_function3699);
					if (this.state.failed)
					{
						return document_match_function_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream2.Add(el2);
					}
					if (this.state.backtracking == 0)
					{
						document_match_function_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (document_match_function_return != null) ? document_match_function_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj3 = this.adaptor.Nil();
						obj3 = this.adaptor.BecomeRoot(this.adaptor.Create(125, "DOCUMENT_MATCHNAME"), obj3);
						this.adaptor.AddChild(obj3, rewriteRuleTokenStream2.NextNode());
						this.adaptor.AddChild(obj, obj3);
						document_match_function_return.Tree = obj;
					}
					break;
				}
				case 3:
				{
					CommonToken el3 = (CommonToken)this.Match(this.input, 25, CssParser.Follow._DOMAIN_FUNCTION_in_document_match_function3720);
					if (this.state.failed)
					{
						return document_match_function_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream3.Add(el3);
					}
					if (this.state.backtracking == 0)
					{
						document_match_function_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (document_match_function_return != null) ? document_match_function_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj4 = this.adaptor.Nil();
						obj4 = this.adaptor.BecomeRoot(this.adaptor.Create(125, "DOCUMENT_MATCHNAME"), obj4);
						this.adaptor.AddChild(obj4, rewriteRuleTokenStream3.NextNode());
						this.adaptor.AddChild(obj, obj4);
						document_match_function_return.Tree = obj;
					}
					break;
				}
				case 4:
				{
					CommonToken el4 = (CommonToken)this.Match(this.input, 74, CssParser.Follow._REGEXP_FUNCTION_in_document_match_function3740);
					if (this.state.failed)
					{
						return document_match_function_return;
					}
					if (this.state.backtracking == 0)
					{
						rewriteRuleTokenStream4.Add(el4);
					}
					if (this.state.backtracking == 0)
					{
						document_match_function_return.Tree = obj;
						new RewriteRuleSubtreeStream(this.adaptor, "rule retval", (document_match_function_return != null) ? document_match_function_return.Tree : null);
						obj = this.adaptor.Nil();
						object obj5 = this.adaptor.Nil();
						obj5 = this.adaptor.BecomeRoot(this.adaptor.Create(125, "DOCUMENT_MATCHNAME"), obj5);
						this.adaptor.AddChild(obj5, rewriteRuleTokenStream4.NextNode());
						this.adaptor.AddChild(obj, obj5);
						document_match_function_return.Tree = obj;
					}
					break;
				}
				}
				document_match_function_return.Stop = (CommonToken)this.input.LT(-1);
				if (this.state.backtracking == 0)
				{
					document_match_function_return.Tree = this.adaptor.RulePostProcessing(obj);
					this.adaptor.SetTokenBoundaries(document_match_function_return.Tree, document_match_function_return.Start, document_match_function_return.Stop);
				}
			}
			catch (RecognitionException ex2)
			{
				this.ReportError(ex2);
				this.Recover(this.input, ex2);
				document_match_function_return.Tree = this.adaptor.ErrorNode(this.input, document_match_function_return.Start, this.input.LT(-1), ex2);
			}
			return document_match_function_return;
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0007384C File Offset: 0x00071A4C
		public void synpred1_CssParser_fragment()
		{
			this.Match(this.input, 105, CssParser.Follow._WS_in_synpred1_CssParser1723);
			if (this.state.failed)
			{
			}
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x00073871 File Offset: 0x00071A71
		public void synpred2_CssParser_fragment()
		{
			base.PushFollow(CssParser.Follow._universal_in_synpred2_CssParser1778);
			this.universal();
			base.PopFollow();
			if (this.state.failed)
			{
			}
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0007389A File Offset: 0x00071A9A
		public void synpred3_CssParser_fragment()
		{
			base.PushFollow(CssParser.Follow._type_selector_in_synpred3_CssParser1788);
			this.type_selector();
			base.PopFollow();
			if (this.state.failed)
			{
			}
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x000738C3 File Offset: 0x00071AC3
		public void synpred4_CssParser_fragment()
		{
			base.PushFollow(CssParser.Follow._hashclassatnameattribpseudonegation_in_synpred4_CssParser1801);
			this.hashclassatnameattribpseudonegation();
			base.PopFollow();
			if (this.state.failed)
			{
			}
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x000738EC File Offset: 0x00071AEC
		public void synpred5_CssParser_fragment()
		{
			base.PushFollow(CssParser.Follow._hashclassatnameattribpseudonegation_in_synpred5_CssParser1843);
			this.hashclassatnameattribpseudonegation();
			base.PopFollow();
			if (this.state.failed)
			{
			}
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x00073915 File Offset: 0x00071B15
		public void synpred6_CssParser_fragment()
		{
			base.PushFollow(CssParser.Follow._selector_namespace_prefix_in_synpred6_CssParser2042);
			this.selector_namespace_prefix();
			base.PopFollow();
			if (this.state.failed)
			{
			}
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0007393E File Offset: 0x00071B3E
		public void synpred7_CssParser_fragment()
		{
			base.PushFollow(CssParser.Follow._selector_namespace_prefix_in_synpred7_CssParser2169);
			this.selector_namespace_prefix();
			base.PopFollow();
			if (this.state.failed)
			{
			}
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00073967 File Offset: 0x00071B67
		public void synpred8_CssParser_fragment()
		{
			base.PushFollow(CssParser.Follow._universal_in_synpred8_CssParser2635);
			this.universal();
			base.PopFollow();
			if (this.state.failed)
			{
			}
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00073990 File Offset: 0x00071B90
		private bool EvaluatePredicate(Action fragment)
		{
			this.state.backtracking++;
			int marker = this.input.Mark();
			try
			{
				fragment();
			}
			catch (RecognitionException arg)
			{
				Console.Error.WriteLine("impossible: " + arg);
			}
			bool result = !this.state.failed;
			this.input.Rewind(marker);
			this.state.backtracking--;
			this.state.failed = false;
			return result;
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00073A2C File Offset: 0x00071C2C
		protected override void InitDFAs()
		{
			base.InitDFAs();
			this.dfa25 = new CssParser.DFA25(this);
			this.dfa33 = new CssParser.DFA33(this, new SpecialStateTransitionHandler(this.SpecialStateTransition33));
			this.dfa48 = new CssParser.DFA48(this, new SpecialStateTransitionHandler(this.SpecialStateTransition48));
			this.dfa54 = new CssParser.DFA54(this);
			this.dfa65 = new CssParser.DFA65(this);
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00073A94 File Offset: 0x00071C94
		private int SpecialStateTransition33(DFA dfa, int s, IIntStream _input)
		{
			ITokenStream tokenStream = (ITokenStream)_input;
			int stateNumber = s;
			switch (s)
			{
			case 0:
			{
				tokenStream.LA(1);
				int index = tokenStream.Index;
				tokenStream.Rewind();
				s = -1;
				if (this.EvaluatePredicate(new Action(this.synpred4_CssParser_fragment)))
				{
					s = 8;
				}
				else
				{
					s = 7;
				}
				tokenStream.Seek(index);
				if (s >= 0)
				{
					return s;
				}
				break;
			}
			case 1:
			{
				tokenStream.LA(1);
				int index2 = tokenStream.Index;
				tokenStream.Rewind();
				s = -1;
				if (this.EvaluatePredicate(new Action(this.synpred4_CssParser_fragment)))
				{
					s = 8;
				}
				else
				{
					s = 7;
				}
				tokenStream.Seek(index2);
				if (s >= 0)
				{
					return s;
				}
				break;
			}
			case 2:
			{
				tokenStream.LA(1);
				int index3 = tokenStream.Index;
				tokenStream.Rewind();
				s = -1;
				if (this.EvaluatePredicate(new Action(this.synpred4_CssParser_fragment)))
				{
					s = 8;
				}
				else
				{
					s = 7;
				}
				tokenStream.Seek(index3);
				if (s >= 0)
				{
					return s;
				}
				break;
			}
			case 3:
			{
				tokenStream.LA(1);
				int index4 = tokenStream.Index;
				tokenStream.Rewind();
				s = -1;
				if (this.EvaluatePredicate(new Action(this.synpred4_CssParser_fragment)))
				{
					s = 8;
				}
				else
				{
					s = 7;
				}
				tokenStream.Seek(index4);
				if (s >= 0)
				{
					return s;
				}
				break;
			}
			case 4:
			{
				tokenStream.LA(1);
				int index5 = tokenStream.Index;
				tokenStream.Rewind();
				s = -1;
				if (this.EvaluatePredicate(new Action(this.synpred4_CssParser_fragment)))
				{
					s = 8;
				}
				else
				{
					s = 7;
				}
				tokenStream.Seek(index5);
				if (s >= 0)
				{
					return s;
				}
				break;
			}
			case 5:
			{
				tokenStream.LA(1);
				int index6 = tokenStream.Index;
				tokenStream.Rewind();
				s = -1;
				if (this.EvaluatePredicate(new Action(this.synpred4_CssParser_fragment)))
				{
					s = 8;
				}
				else
				{
					s = 7;
				}
				tokenStream.Seek(index6);
				if (s >= 0)
				{
					return s;
				}
				break;
			}
			case 6:
			{
				tokenStream.LA(1);
				int index7 = tokenStream.Index;
				tokenStream.Rewind();
				s = -1;
				if (this.EvaluatePredicate(new Action(this.synpred4_CssParser_fragment)))
				{
					s = 8;
				}
				else
				{
					s = 7;
				}
				tokenStream.Seek(index7);
				if (s >= 0)
				{
					return s;
				}
				break;
			}
			case 7:
			{
				tokenStream.LA(1);
				int index8 = tokenStream.Index;
				tokenStream.Rewind();
				s = -1;
				if (this.EvaluatePredicate(new Action(this.synpred4_CssParser_fragment)))
				{
					s = 8;
				}
				else
				{
					s = 7;
				}
				tokenStream.Seek(index8);
				if (s >= 0)
				{
					return s;
				}
				break;
			}
			}
			if (this.state.backtracking > 0)
			{
				this.state.failed = true;
				return -1;
			}
			NoViableAltException ex = new NoViableAltException(dfa.Description, 33, stateNumber, tokenStream);
			dfa.Error(ex);
			throw ex;
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00073D38 File Offset: 0x00071F38
		private int SpecialStateTransition48(DFA dfa, int s, IIntStream _input)
		{
			ITokenStream tokenStream = (ITokenStream)_input;
			int stateNumber = s;
			switch (s)
			{
			case 0:
			{
				tokenStream.LA(1);
				int index = tokenStream.Index;
				tokenStream.Rewind();
				s = -1;
				if (this.EvaluatePredicate(new Action(this.synpred8_CssParser_fragment)))
				{
					s = 9;
				}
				else
				{
					s = 8;
				}
				tokenStream.Seek(index);
				if (s >= 0)
				{
					return s;
				}
				break;
			}
			case 1:
			{
				tokenStream.LA(1);
				int index2 = tokenStream.Index;
				tokenStream.Rewind();
				s = -1;
				if (this.EvaluatePredicate(new Action(this.synpred8_CssParser_fragment)))
				{
					s = 9;
				}
				else
				{
					s = 8;
				}
				tokenStream.Seek(index2);
				if (s >= 0)
				{
					return s;
				}
				break;
			}
			}
			if (this.state.backtracking > 0)
			{
				this.state.failed = true;
				return -1;
			}
			NoViableAltException ex = new NoViableAltException(dfa.Description, 48, stateNumber, tokenStream);
			dfa.Error(ex);
			throw ex;
		}

		// Token: 0x040008AC RID: 2220
		public const int EOF = -1;

		// Token: 0x040008AD RID: 2221
		public const int A = 4;

		// Token: 0x040008AE RID: 2222
		public const int AND = 5;

		// Token: 0x040008AF RID: 2223
		public const int ANGLE = 6;

		// Token: 0x040008B0 RID: 2224
		public const int AT_NAME = 7;

		// Token: 0x040008B1 RID: 2225
		public const int B = 8;

		// Token: 0x040008B2 RID: 2226
		public const int BACKWARD_SLASH = 9;

		// Token: 0x040008B3 RID: 2227
		public const int C = 10;

		// Token: 0x040008B4 RID: 2228
		public const int CHARSET_SYM = 11;

		// Token: 0x040008B5 RID: 2229
		public const int CIRCLE_BEGIN = 12;

		// Token: 0x040008B6 RID: 2230
		public const int CIRCLE_END = 13;

		// Token: 0x040008B7 RID: 2231
		public const int CLASS_IDENT = 14;

		// Token: 0x040008B8 RID: 2232
		public const int COLON = 15;

		// Token: 0x040008B9 RID: 2233
		public const int COMMA = 16;

		// Token: 0x040008BA RID: 2234
		public const int COMMENTS = 17;

		// Token: 0x040008BB RID: 2235
		public const int CURLY_BEGIN = 18;

		// Token: 0x040008BC RID: 2236
		public const int CURLY_END = 19;

		// Token: 0x040008BD RID: 2237
		public const int D = 20;

		// Token: 0x040008BE RID: 2238
		public const int DASHMATCH = 21;

		// Token: 0x040008BF RID: 2239
		public const int DIGITS = 22;

		// Token: 0x040008C0 RID: 2240
		public const int DIMENSION = 23;

		// Token: 0x040008C1 RID: 2241
		public const int DOCUMENT_SYM = 24;

		// Token: 0x040008C2 RID: 2242
		public const int DOMAIN_FUNCTION = 25;

		// Token: 0x040008C3 RID: 2243
		public const int E = 26;

		// Token: 0x040008C4 RID: 2244
		public const int EMPTY_COMMENT = 27;

		// Token: 0x040008C5 RID: 2245
		public const int EQUALS = 28;

		// Token: 0x040008C6 RID: 2246
		public const int ESCAPE = 29;

		// Token: 0x040008C7 RID: 2247
		public const int F = 30;

		// Token: 0x040008C8 RID: 2248
		public const int FORWARD_SLASH = 31;

		// Token: 0x040008C9 RID: 2249
		public const int FREQ = 32;

		// Token: 0x040008CA RID: 2250
		public const int FROM = 33;

		// Token: 0x040008CB RID: 2251
		public const int G = 34;

		// Token: 0x040008CC RID: 2252
		public const int GREATER = 35;

		// Token: 0x040008CD RID: 2253
		public const int H = 36;

		// Token: 0x040008CE RID: 2254
		public const int HASH = 37;

		// Token: 0x040008CF RID: 2255
		public const int HASH_IDENT = 38;

		// Token: 0x040008D0 RID: 2256
		public const int HEXDIGIT = 39;

		// Token: 0x040008D1 RID: 2257
		public const int I = 40;

		// Token: 0x040008D2 RID: 2258
		public const int IDENT = 41;

		// Token: 0x040008D3 RID: 2259
		public const int IMPORTANT_COMMENTS = 42;

		// Token: 0x040008D4 RID: 2260
		public const int IMPORTANT_SYM = 43;

		// Token: 0x040008D5 RID: 2261
		public const int IMPORT_SYM = 44;

		// Token: 0x040008D6 RID: 2262
		public const int INCLUDES = 45;

		// Token: 0x040008D7 RID: 2263
		public const int K = 46;

		// Token: 0x040008D8 RID: 2264
		public const int KEYFRAMES_SYM = 47;

		// Token: 0x040008D9 RID: 2265
		public const int L = 48;

		// Token: 0x040008DA RID: 2266
		public const int LENGTH = 49;

		// Token: 0x040008DB RID: 2267
		public const int LETTER = 50;

		// Token: 0x040008DC RID: 2268
		public const int M = 51;

		// Token: 0x040008DD RID: 2269
		public const int MEDIA_SYM = 52;

		// Token: 0x040008DE RID: 2270
		public const int MINUS = 53;

		// Token: 0x040008DF RID: 2271
		public const int MSIE_EXPRESSION = 54;

		// Token: 0x040008E0 RID: 2272
		public const int MSIE_IMAGE_TRANSFORM = 55;

		// Token: 0x040008E1 RID: 2273
		public const int N = 56;

		// Token: 0x040008E2 RID: 2274
		public const int NAME = 57;

		// Token: 0x040008E3 RID: 2275
		public const int NAMESPACE_SYM = 58;

		// Token: 0x040008E4 RID: 2276
		public const int NL = 59;

		// Token: 0x040008E5 RID: 2277
		public const int NMCHAR = 60;

		// Token: 0x040008E6 RID: 2278
		public const int NMSTART = 61;

		// Token: 0x040008E7 RID: 2279
		public const int NONASCII = 62;

		// Token: 0x040008E8 RID: 2280
		public const int NOT = 63;

		// Token: 0x040008E9 RID: 2281
		public const int NUMBER = 64;

		// Token: 0x040008EA RID: 2282
		public const int O = 65;

		// Token: 0x040008EB RID: 2283
		public const int ONLY = 66;

		// Token: 0x040008EC RID: 2284
		public const int P = 67;

		// Token: 0x040008ED RID: 2285
		public const int PAGE_SYM = 68;

		// Token: 0x040008EE RID: 2286
		public const int PERCENTAGE = 69;

		// Token: 0x040008EF RID: 2287
		public const int PIPE = 70;

		// Token: 0x040008F0 RID: 2288
		public const int PLUS = 71;

		// Token: 0x040008F1 RID: 2289
		public const int PREFIXMATCH = 72;

		// Token: 0x040008F2 RID: 2290
		public const int R = 73;

		// Token: 0x040008F3 RID: 2291
		public const int REGEXP_FUNCTION = 74;

		// Token: 0x040008F4 RID: 2292
		public const int RELATIVELENGTH = 75;

		// Token: 0x040008F5 RID: 2293
		public const int REPLACEMENTTOKEN = 76;

		// Token: 0x040008F6 RID: 2294
		public const int RESOLUTION = 77;

		// Token: 0x040008F7 RID: 2295
		public const int S = 78;

		// Token: 0x040008F8 RID: 2296
		public const int SEMICOLON = 79;

		// Token: 0x040008F9 RID: 2297
		public const int SPACE_AFTER_UNICODE = 80;

		// Token: 0x040008FA RID: 2298
		public const int SPEECH = 81;

		// Token: 0x040008FB RID: 2299
		public const int SQUARE_BEGIN = 82;

		// Token: 0x040008FC RID: 2300
		public const int SQUARE_END = 83;

		// Token: 0x040008FD RID: 2301
		public const int STAR = 84;

		// Token: 0x040008FE RID: 2302
		public const int STRING = 85;

		// Token: 0x040008FF RID: 2303
		public const int SUBSTRINGMATCH = 86;

		// Token: 0x04000900 RID: 2304
		public const int SUFFIXMATCH = 87;

		// Token: 0x04000901 RID: 2305
		public const int T = 88;

		// Token: 0x04000902 RID: 2306
		public const int TILDE = 89;

		// Token: 0x04000903 RID: 2307
		public const int TIME = 90;

		// Token: 0x04000904 RID: 2308
		public const int TO = 91;

		// Token: 0x04000905 RID: 2309
		public const int U = 92;

		// Token: 0x04000906 RID: 2310
		public const int UNICODE = 93;

		// Token: 0x04000907 RID: 2311
		public const int UNICODE_ESCAPE_HACK = 94;

		// Token: 0x04000908 RID: 2312
		public const int UNICODE_NULLTERM = 95;

		// Token: 0x04000909 RID: 2313
		public const int UNICODE_RANGE = 96;

		// Token: 0x0400090A RID: 2314
		public const int UNICODE_TAB = 97;

		// Token: 0x0400090B RID: 2315
		public const int UNICODE_ZEROS = 98;

		// Token: 0x0400090C RID: 2316
		public const int URI = 99;

		// Token: 0x0400090D RID: 2317
		public const int URL = 100;

		// Token: 0x0400090E RID: 2318
		public const int URLPREFIX_FUNCTION = 101;

		// Token: 0x0400090F RID: 2319
		public const int V = 102;

		// Token: 0x04000910 RID: 2320
		public const int W = 103;

		// Token: 0x04000911 RID: 2321
		public const int WG_DPI_SYM = 104;

		// Token: 0x04000912 RID: 2322
		public const int WS = 105;

		// Token: 0x04000913 RID: 2323
		public const int WS_FRAGMENT = 106;

		// Token: 0x04000914 RID: 2324
		public const int X = 107;

		// Token: 0x04000915 RID: 2325
		public const int Y = 108;

		// Token: 0x04000916 RID: 2326
		public const int Z = 109;

		// Token: 0x04000917 RID: 2327
		public const int ATIDENTIFIER = 110;

		// Token: 0x04000918 RID: 2328
		public const int ATTRIBIDENTIFIER = 111;

		// Token: 0x04000919 RID: 2329
		public const int ATTRIBNAME = 112;

		// Token: 0x0400091A RID: 2330
		public const int ATTRIBOPERATOR = 113;

		// Token: 0x0400091B RID: 2331
		public const int ATTRIBOPERATORVALUE = 114;

		// Token: 0x0400091C RID: 2332
		public const int ATTRIBVALUE = 115;

		// Token: 0x0400091D RID: 2333
		public const int CHARSET = 116;

		// Token: 0x0400091E RID: 2334
		public const int CLASSIDENTIFIER = 117;

		// Token: 0x0400091F RID: 2335
		public const int COLONS = 118;

		// Token: 0x04000920 RID: 2336
		public const int COMBINATOR = 119;

		// Token: 0x04000921 RID: 2337
		public const int COMBINATOR_SIMPLE_SELECTOR = 120;

		// Token: 0x04000922 RID: 2338
		public const int COMBINATOR_SIMPLE_SELECTOR_SEQUENCES = 121;

		// Token: 0x04000923 RID: 2339
		public const int DECLARATION = 122;

		// Token: 0x04000924 RID: 2340
		public const int DECLARATIONS = 123;

		// Token: 0x04000925 RID: 2341
		public const int DOCUMENT = 124;

		// Token: 0x04000926 RID: 2342
		public const int DOCUMENT_MATCHNAME = 125;

		// Token: 0x04000927 RID: 2343
		public const int DOCUMENT_SYMBOL = 126;

		// Token: 0x04000928 RID: 2344
		public const int ELEMENT_NAME = 127;

		// Token: 0x04000929 RID: 2345
		public const int EXPR = 128;

		// Token: 0x0400092A RID: 2346
		public const int FUNCTIONAL_PSEUDO = 129;

		// Token: 0x0400092B RID: 2347
		public const int FUNCTIONBASEDVALUE = 130;

		// Token: 0x0400092C RID: 2348
		public const int FUNCTIONNAME = 131;

		// Token: 0x0400092D RID: 2349
		public const int FUNCTIONPARAM = 132;

		// Token: 0x0400092E RID: 2350
		public const int HASHCLASSATNAMEATTRIBPSEUDONEGATION = 133;

		// Token: 0x0400092F RID: 2351
		public const int HASHCLASSATNAMEATTRIBPSEUDONEGATIONNODES = 134;

		// Token: 0x04000930 RID: 2352
		public const int HASHIDENTIFIER = 135;

		// Token: 0x04000931 RID: 2353
		public const int HEXBASEDVALUE = 136;

		// Token: 0x04000932 RID: 2354
		public const int IDENTBASEDVALUE = 137;

		// Token: 0x04000933 RID: 2355
		public const int IMPORT = 138;

		// Token: 0x04000934 RID: 2356
		public const int IMPORTANT = 139;

		// Token: 0x04000935 RID: 2357
		public const int IMPORTS = 140;

		// Token: 0x04000936 RID: 2358
		public const int KEYFRAMES = 141;

		// Token: 0x04000937 RID: 2359
		public const int KEYFRAMES_BLOCK = 142;

		// Token: 0x04000938 RID: 2360
		public const int KEYFRAMES_BLOCKS = 143;

		// Token: 0x04000939 RID: 2361
		public const int KEYFRAMES_SELECTOR = 144;

		// Token: 0x0400093A RID: 2362
		public const int KEYFRAMES_SELECTORS = 145;

		// Token: 0x0400093B RID: 2363
		public const int KEYFRAMES_SYMBOL = 146;

		// Token: 0x0400093C RID: 2364
		public const int MEDIA = 147;

		// Token: 0x0400093D RID: 2365
		public const int MEDIA_EXPRESSION = 148;

		// Token: 0x0400093E RID: 2366
		public const int MEDIA_EXPRESSIONS = 149;

		// Token: 0x0400093F RID: 2367
		public const int MEDIA_FEATURE = 150;

		// Token: 0x04000940 RID: 2368
		public const int MEDIA_QUERY = 151;

		// Token: 0x04000941 RID: 2369
		public const int MEDIA_QUERY_LIST = 152;

		// Token: 0x04000942 RID: 2370
		public const int MEDIA_TYPE = 153;

		// Token: 0x04000943 RID: 2371
		public const int NAMESPACE = 154;

		// Token: 0x04000944 RID: 2372
		public const int NAMESPACES = 155;

		// Token: 0x04000945 RID: 2373
		public const int NAMESPACE_PREFIX = 156;

		// Token: 0x04000946 RID: 2374
		public const int NEGATIONIDENTIFIER = 157;

		// Token: 0x04000947 RID: 2375
		public const int NEGATION_ARG = 158;

		// Token: 0x04000948 RID: 2376
		public const int NOT_TEXT = 159;

		// Token: 0x04000949 RID: 2377
		public const int NUMBERBASEDVALUE = 160;

		// Token: 0x0400094A RID: 2378
		public const int ONLY_TEXT = 161;

		// Token: 0x0400094B RID: 2379
		public const int OPERATOR = 162;

		// Token: 0x0400094C RID: 2380
		public const int PAGE = 163;

		// Token: 0x0400094D RID: 2381
		public const int PROPERTY = 164;

		// Token: 0x0400094E RID: 2382
		public const int PSEUDOIDENTIFIER = 165;

		// Token: 0x0400094F RID: 2383
		public const int PSEUDONAME = 166;

		// Token: 0x04000950 RID: 2384
		public const int PSEUDO_PAGE = 167;

		// Token: 0x04000951 RID: 2385
		public const int REPLACEMENT = 168;

		// Token: 0x04000952 RID: 2386
		public const int REPLACEMENTTOKENBASEDVALUE = 169;

		// Token: 0x04000953 RID: 2387
		public const int REPLACEMENTTOKENIDENTIFIER = 170;

		// Token: 0x04000954 RID: 2388
		public const int RULESET = 171;

		// Token: 0x04000955 RID: 2389
		public const int RULESETS = 172;

		// Token: 0x04000956 RID: 2390
		public const int SELECTOR = 173;

		// Token: 0x04000957 RID: 2391
		public const int SELECTORS_GROUP = 174;

		// Token: 0x04000958 RID: 2392
		public const int SELECTOR_EXPRESSION = 175;

		// Token: 0x04000959 RID: 2393
		public const int SELECTOR_NAMESPACE_PREFIX = 176;

		// Token: 0x0400095A RID: 2394
		public const int SIMPLE_SELECTOR_SEQUENCE = 177;

		// Token: 0x0400095B RID: 2395
		public const int STAR_TEXT = 178;

		// Token: 0x0400095C RID: 2396
		public const int STRINGBASEDVALUE = 179;

		// Token: 0x0400095D RID: 2397
		public const int STYLESHEET = 180;

		// Token: 0x0400095E RID: 2398
		public const int TERM = 181;

		// Token: 0x0400095F RID: 2399
		public const int TERMWITHOPERATOR = 182;

		// Token: 0x04000960 RID: 2400
		public const int TERMWITHOPERATORS = 183;

		// Token: 0x04000961 RID: 2401
		public const int TYPE_SELECTOR = 184;

		// Token: 0x04000962 RID: 2402
		public const int UNARY = 185;

		// Token: 0x04000963 RID: 2403
		public const int UNIVERSAL = 186;

		// Token: 0x04000964 RID: 2404
		public const int URIBASEDVALUE = 187;

		// Token: 0x04000965 RID: 2405
		public const int URIHASH = 188;

		// Token: 0x04000966 RID: 2406
		public const int WG_DPI = 189;

		// Token: 0x04000967 RID: 2407
		public const int WHITESPACE = 190;

		// Token: 0x04000968 RID: 2408
		private readonly IList<Exception> _exceptions = new List<Exception>();

		// Token: 0x04000969 RID: 2409
		private static char[] _semicolon = new char[]
		{
			';'
		};

		// Token: 0x0400096A RID: 2410
		internal static readonly string[] tokenNames = new string[]
		{
			"<invalid>",
			"<EOR>",
			"<DOWN>",
			"<UP>",
			"A",
			"AND",
			"ANGLE",
			"AT_NAME",
			"B",
			"BACKWARD_SLASH",
			"C",
			"CHARSET_SYM",
			"CIRCLE_BEGIN",
			"CIRCLE_END",
			"CLASS_IDENT",
			"COLON",
			"COMMA",
			"COMMENTS",
			"CURLY_BEGIN",
			"CURLY_END",
			"D",
			"DASHMATCH",
			"DIGITS",
			"DIMENSION",
			"DOCUMENT_SYM",
			"DOMAIN_FUNCTION",
			"E",
			"EMPTY_COMMENT",
			"EQUALS",
			"ESCAPE",
			"F",
			"FORWARD_SLASH",
			"FREQ",
			"FROM",
			"G",
			"GREATER",
			"H",
			"HASH",
			"HASH_IDENT",
			"HEXDIGIT",
			"I",
			"IDENT",
			"IMPORTANT_COMMENTS",
			"IMPORTANT_SYM",
			"IMPORT_SYM",
			"INCLUDES",
			"K",
			"KEYFRAMES_SYM",
			"L",
			"LENGTH",
			"LETTER",
			"M",
			"MEDIA_SYM",
			"MINUS",
			"MSIE_EXPRESSION",
			"MSIE_IMAGE_TRANSFORM",
			"N",
			"NAME",
			"NAMESPACE_SYM",
			"NL",
			"NMCHAR",
			"NMSTART",
			"NONASCII",
			"NOT",
			"NUMBER",
			"O",
			"ONLY",
			"P",
			"PAGE_SYM",
			"PERCENTAGE",
			"PIPE",
			"PLUS",
			"PREFIXMATCH",
			"R",
			"REGEXP_FUNCTION",
			"RELATIVELENGTH",
			"REPLACEMENTTOKEN",
			"RESOLUTION",
			"S",
			"SEMICOLON",
			"SPACE_AFTER_UNICODE",
			"SPEECH",
			"SQUARE_BEGIN",
			"SQUARE_END",
			"STAR",
			"STRING",
			"SUBSTRINGMATCH",
			"SUFFIXMATCH",
			"T",
			"TILDE",
			"TIME",
			"TO",
			"U",
			"UNICODE",
			"UNICODE_ESCAPE_HACK",
			"UNICODE_NULLTERM",
			"UNICODE_RANGE",
			"UNICODE_TAB",
			"UNICODE_ZEROS",
			"URI",
			"URL",
			"URLPREFIX_FUNCTION",
			"V",
			"W",
			"WG_DPI_SYM",
			"WS",
			"WS_FRAGMENT",
			"X",
			"Y",
			"Z",
			"ATIDENTIFIER",
			"ATTRIBIDENTIFIER",
			"ATTRIBNAME",
			"ATTRIBOPERATOR",
			"ATTRIBOPERATORVALUE",
			"ATTRIBVALUE",
			"CHARSET",
			"CLASSIDENTIFIER",
			"COLONS",
			"COMBINATOR",
			"COMBINATOR_SIMPLE_SELECTOR",
			"COMBINATOR_SIMPLE_SELECTOR_SEQUENCES",
			"DECLARATION",
			"DECLARATIONS",
			"DOCUMENT",
			"DOCUMENT_MATCHNAME",
			"DOCUMENT_SYMBOL",
			"ELEMENT_NAME",
			"EXPR",
			"FUNCTIONAL_PSEUDO",
			"FUNCTIONBASEDVALUE",
			"FUNCTIONNAME",
			"FUNCTIONPARAM",
			"HASHCLASSATNAMEATTRIBPSEUDONEGATION",
			"HASHCLASSATNAMEATTRIBPSEUDONEGATIONNODES",
			"HASHIDENTIFIER",
			"HEXBASEDVALUE",
			"IDENTBASEDVALUE",
			"IMPORT",
			"IMPORTANT",
			"IMPORTS",
			"KEYFRAMES",
			"KEYFRAMES_BLOCK",
			"KEYFRAMES_BLOCKS",
			"KEYFRAMES_SELECTOR",
			"KEYFRAMES_SELECTORS",
			"KEYFRAMES_SYMBOL",
			"MEDIA",
			"MEDIA_EXPRESSION",
			"MEDIA_EXPRESSIONS",
			"MEDIA_FEATURE",
			"MEDIA_QUERY",
			"MEDIA_QUERY_LIST",
			"MEDIA_TYPE",
			"NAMESPACE",
			"NAMESPACES",
			"NAMESPACE_PREFIX",
			"NEGATIONIDENTIFIER",
			"NEGATION_ARG",
			"NOT_TEXT",
			"NUMBERBASEDVALUE",
			"ONLY_TEXT",
			"OPERATOR",
			"PAGE",
			"PROPERTY",
			"PSEUDOIDENTIFIER",
			"PSEUDONAME",
			"PSEUDO_PAGE",
			"REPLACEMENT",
			"REPLACEMENTTOKENBASEDVALUE",
			"REPLACEMENTTOKENIDENTIFIER",
			"RULESET",
			"RULESETS",
			"SELECTOR",
			"SELECTORS_GROUP",
			"SELECTOR_EXPRESSION",
			"SELECTOR_NAMESPACE_PREFIX",
			"SIMPLE_SELECTOR_SEQUENCE",
			"STAR_TEXT",
			"STRINGBASEDVALUE",
			"STYLESHEET",
			"TERM",
			"TERMWITHOPERATOR",
			"TERMWITHOPERATORS",
			"TYPE_SELECTOR",
			"UNARY",
			"UNIVERSAL",
			"URIBASEDVALUE",
			"URIHASH",
			"WG_DPI",
			"WHITESPACE"
		};

		// Token: 0x0400096B RID: 2411
		private ITreeAdaptor adaptor;

		// Token: 0x0400096C RID: 2412
		private CssParser.DFA25 dfa25;

		// Token: 0x0400096D RID: 2413
		private CssParser.DFA33 dfa33;

		// Token: 0x0400096E RID: 2414
		private CssParser.DFA48 dfa48;

		// Token: 0x0400096F RID: 2415
		private CssParser.DFA54 dfa54;

		// Token: 0x04000970 RID: 2416
		private CssParser.DFA65 dfa65;

		// Token: 0x02000147 RID: 327
		public sealed class main_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x17000498 RID: 1176
			// (get) Token: 0x06001352 RID: 4946 RVA: 0x000745BB File Offset: 0x000727BB
			// (set) Token: 0x06001353 RID: 4947 RVA: 0x000745C3 File Offset: 0x000727C3
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x17000499 RID: 1177
			// (get) Token: 0x06001354 RID: 4948 RVA: 0x000745CC File Offset: 0x000727CC
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x06001355 RID: 4949 RVA: 0x000745D4 File Offset: 0x000727D4
			public main_return(CssParser grammar)
			{
			}

			// Token: 0x04000971 RID: 2417
			private object _tree;
		}

		// Token: 0x02000148 RID: 328
		private sealed class styleSheet_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x1700049A RID: 1178
			// (get) Token: 0x06001356 RID: 4950 RVA: 0x000745DC File Offset: 0x000727DC
			// (set) Token: 0x06001357 RID: 4951 RVA: 0x000745E4 File Offset: 0x000727E4
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x1700049B RID: 1179
			// (get) Token: 0x06001358 RID: 4952 RVA: 0x000745ED File Offset: 0x000727ED
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x06001359 RID: 4953 RVA: 0x000745F5 File Offset: 0x000727F5
			public styleSheet_return(CssParser grammar)
			{
			}

			// Token: 0x04000972 RID: 2418
			private object _tree;
		}

		// Token: 0x02000149 RID: 329
		private sealed class styleSheetRulesOrComment_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x1700049C RID: 1180
			// (get) Token: 0x0600135A RID: 4954 RVA: 0x000745FD File Offset: 0x000727FD
			// (set) Token: 0x0600135B RID: 4955 RVA: 0x00074605 File Offset: 0x00072805
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x1700049D RID: 1181
			// (get) Token: 0x0600135C RID: 4956 RVA: 0x0007460E File Offset: 0x0007280E
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x0600135D RID: 4957 RVA: 0x00074616 File Offset: 0x00072816
			public styleSheetRulesOrComment_return(CssParser grammar)
			{
			}

			// Token: 0x04000973 RID: 2419
			private object _tree;
		}

		// Token: 0x0200014A RID: 330
		private sealed class styleimport_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x1700049E RID: 1182
			// (get) Token: 0x0600135E RID: 4958 RVA: 0x0007461E File Offset: 0x0007281E
			// (set) Token: 0x0600135F RID: 4959 RVA: 0x00074626 File Offset: 0x00072826
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x1700049F RID: 1183
			// (get) Token: 0x06001360 RID: 4960 RVA: 0x0007462F File Offset: 0x0007282F
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x06001361 RID: 4961 RVA: 0x00074637 File Offset: 0x00072837
			public styleimport_return(CssParser grammar)
			{
			}

			// Token: 0x04000974 RID: 2420
			private object _tree;
		}

		// Token: 0x0200014B RID: 331
		private sealed class namespace_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004A0 RID: 1184
			// (get) Token: 0x06001362 RID: 4962 RVA: 0x0007463F File Offset: 0x0007283F
			// (set) Token: 0x06001363 RID: 4963 RVA: 0x00074647 File Offset: 0x00072847
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004A1 RID: 1185
			// (get) Token: 0x06001364 RID: 4964 RVA: 0x00074650 File Offset: 0x00072850
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x06001365 RID: 4965 RVA: 0x00074658 File Offset: 0x00072858
			public namespace_return(CssParser grammar)
			{
			}

			// Token: 0x04000975 RID: 2421
			private object _tree;
		}

		// Token: 0x0200014C RID: 332
		private sealed class namespace_prefix_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004A2 RID: 1186
			// (get) Token: 0x06001366 RID: 4966 RVA: 0x00074660 File Offset: 0x00072860
			// (set) Token: 0x06001367 RID: 4967 RVA: 0x00074668 File Offset: 0x00072868
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004A3 RID: 1187
			// (get) Token: 0x06001368 RID: 4968 RVA: 0x00074671 File Offset: 0x00072871
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x06001369 RID: 4969 RVA: 0x00074679 File Offset: 0x00072879
			public namespace_prefix_return(CssParser grammar)
			{
			}

			// Token: 0x04000976 RID: 2422
			private object _tree;
		}

		// Token: 0x0200014D RID: 333
		private sealed class wg_dpi_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004A4 RID: 1188
			// (get) Token: 0x0600136A RID: 4970 RVA: 0x00074681 File Offset: 0x00072881
			// (set) Token: 0x0600136B RID: 4971 RVA: 0x00074689 File Offset: 0x00072889
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004A5 RID: 1189
			// (get) Token: 0x0600136C RID: 4972 RVA: 0x00074692 File Offset: 0x00072892
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x0600136D RID: 4973 RVA: 0x0007469A File Offset: 0x0007289A
			public wg_dpi_return(CssParser grammar)
			{
			}

			// Token: 0x04000977 RID: 2423
			private object _tree;
		}

		// Token: 0x0200014E RID: 334
		private sealed class media_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004A6 RID: 1190
			// (get) Token: 0x0600136E RID: 4974 RVA: 0x000746A2 File Offset: 0x000728A2
			// (set) Token: 0x0600136F RID: 4975 RVA: 0x000746AA File Offset: 0x000728AA
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004A7 RID: 1191
			// (get) Token: 0x06001370 RID: 4976 RVA: 0x000746B3 File Offset: 0x000728B3
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x06001371 RID: 4977 RVA: 0x000746BB File Offset: 0x000728BB
			public media_return(CssParser grammar)
			{
			}

			// Token: 0x04000978 RID: 2424
			private object _tree;
		}

		// Token: 0x0200014F RID: 335
		private sealed class media_query_list_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004A8 RID: 1192
			// (get) Token: 0x06001372 RID: 4978 RVA: 0x000746C3 File Offset: 0x000728C3
			// (set) Token: 0x06001373 RID: 4979 RVA: 0x000746CB File Offset: 0x000728CB
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004A9 RID: 1193
			// (get) Token: 0x06001374 RID: 4980 RVA: 0x000746D4 File Offset: 0x000728D4
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x06001375 RID: 4981 RVA: 0x000746DC File Offset: 0x000728DC
			public media_query_list_return(CssParser grammar)
			{
			}

			// Token: 0x04000979 RID: 2425
			private object _tree;
		}

		// Token: 0x02000150 RID: 336
		private sealed class media_query_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004AA RID: 1194
			// (get) Token: 0x06001376 RID: 4982 RVA: 0x000746E4 File Offset: 0x000728E4
			// (set) Token: 0x06001377 RID: 4983 RVA: 0x000746EC File Offset: 0x000728EC
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004AB RID: 1195
			// (get) Token: 0x06001378 RID: 4984 RVA: 0x000746F5 File Offset: 0x000728F5
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x06001379 RID: 4985 RVA: 0x000746FD File Offset: 0x000728FD
			public media_query_return(CssParser grammar)
			{
			}

			// Token: 0x0400097A RID: 2426
			private object _tree;
		}

		// Token: 0x02000151 RID: 337
		private sealed class media_type_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004AC RID: 1196
			// (get) Token: 0x0600137A RID: 4986 RVA: 0x00074705 File Offset: 0x00072905
			// (set) Token: 0x0600137B RID: 4987 RVA: 0x0007470D File Offset: 0x0007290D
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004AD RID: 1197
			// (get) Token: 0x0600137C RID: 4988 RVA: 0x00074716 File Offset: 0x00072916
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x0600137D RID: 4989 RVA: 0x0007471E File Offset: 0x0007291E
			public media_type_return(CssParser grammar)
			{
			}

			// Token: 0x0400097B RID: 2427
			private object _tree;
		}

		// Token: 0x02000152 RID: 338
		private sealed class media_expression_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004AE RID: 1198
			// (get) Token: 0x0600137E RID: 4990 RVA: 0x00074726 File Offset: 0x00072926
			// (set) Token: 0x0600137F RID: 4991 RVA: 0x0007472E File Offset: 0x0007292E
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004AF RID: 1199
			// (get) Token: 0x06001380 RID: 4992 RVA: 0x00074737 File Offset: 0x00072937
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x06001381 RID: 4993 RVA: 0x0007473F File Offset: 0x0007293F
			public media_expression_return(CssParser grammar)
			{
			}

			// Token: 0x0400097C RID: 2428
			private object _tree;
		}

		// Token: 0x02000153 RID: 339
		private sealed class media_feature_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004B0 RID: 1200
			// (get) Token: 0x06001382 RID: 4994 RVA: 0x00074747 File Offset: 0x00072947
			// (set) Token: 0x06001383 RID: 4995 RVA: 0x0007474F File Offset: 0x0007294F
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004B1 RID: 1201
			// (get) Token: 0x06001384 RID: 4996 RVA: 0x00074758 File Offset: 0x00072958
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x06001385 RID: 4997 RVA: 0x00074760 File Offset: 0x00072960
			public media_feature_return(CssParser grammar)
			{
			}

			// Token: 0x0400097D RID: 2429
			private object _tree;
		}

		// Token: 0x02000154 RID: 340
		private sealed class page_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004B2 RID: 1202
			// (get) Token: 0x06001386 RID: 4998 RVA: 0x00074768 File Offset: 0x00072968
			// (set) Token: 0x06001387 RID: 4999 RVA: 0x00074770 File Offset: 0x00072970
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004B3 RID: 1203
			// (get) Token: 0x06001388 RID: 5000 RVA: 0x00074779 File Offset: 0x00072979
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x06001389 RID: 5001 RVA: 0x00074781 File Offset: 0x00072981
			public page_return(CssParser grammar)
			{
			}

			// Token: 0x0400097E RID: 2430
			private object _tree;
		}

		// Token: 0x02000155 RID: 341
		private sealed class pseudo_page_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004B4 RID: 1204
			// (get) Token: 0x0600138A RID: 5002 RVA: 0x00074789 File Offset: 0x00072989
			// (set) Token: 0x0600138B RID: 5003 RVA: 0x00074791 File Offset: 0x00072991
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004B5 RID: 1205
			// (get) Token: 0x0600138C RID: 5004 RVA: 0x0007479A File Offset: 0x0007299A
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x0600138D RID: 5005 RVA: 0x000747A2 File Offset: 0x000729A2
			public pseudo_page_return(CssParser grammar)
			{
			}

			// Token: 0x0400097F RID: 2431
			private object _tree;
		}

		// Token: 0x02000156 RID: 342
		private sealed class operator_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004B6 RID: 1206
			// (get) Token: 0x0600138E RID: 5006 RVA: 0x000747AA File Offset: 0x000729AA
			// (set) Token: 0x0600138F RID: 5007 RVA: 0x000747B2 File Offset: 0x000729B2
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004B7 RID: 1207
			// (get) Token: 0x06001390 RID: 5008 RVA: 0x000747BB File Offset: 0x000729BB
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x06001391 RID: 5009 RVA: 0x000747C3 File Offset: 0x000729C3
			public operator_return(CssParser grammar)
			{
			}

			// Token: 0x04000980 RID: 2432
			private object _tree;
		}

		// Token: 0x02000157 RID: 343
		private sealed class unary_operator_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004B8 RID: 1208
			// (get) Token: 0x06001392 RID: 5010 RVA: 0x000747CB File Offset: 0x000729CB
			// (set) Token: 0x06001393 RID: 5011 RVA: 0x000747D3 File Offset: 0x000729D3
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004B9 RID: 1209
			// (get) Token: 0x06001394 RID: 5012 RVA: 0x000747DC File Offset: 0x000729DC
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x06001395 RID: 5013 RVA: 0x000747E4 File Offset: 0x000729E4
			public unary_operator_return(CssParser grammar)
			{
			}

			// Token: 0x04000981 RID: 2433
			private object _tree;
		}

		// Token: 0x02000158 RID: 344
		private sealed class property_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004BA RID: 1210
			// (get) Token: 0x06001396 RID: 5014 RVA: 0x000747EC File Offset: 0x000729EC
			// (set) Token: 0x06001397 RID: 5015 RVA: 0x000747F4 File Offset: 0x000729F4
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004BB RID: 1211
			// (get) Token: 0x06001398 RID: 5016 RVA: 0x000747FD File Offset: 0x000729FD
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x06001399 RID: 5017 RVA: 0x00074805 File Offset: 0x00072A05
			public property_return(CssParser grammar)
			{
			}

			// Token: 0x04000982 RID: 2434
			private object _tree;
		}

		// Token: 0x02000159 RID: 345
		private sealed class ruleset_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004BC RID: 1212
			// (get) Token: 0x0600139A RID: 5018 RVA: 0x0007480D File Offset: 0x00072A0D
			// (set) Token: 0x0600139B RID: 5019 RVA: 0x00074815 File Offset: 0x00072A15
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004BD RID: 1213
			// (get) Token: 0x0600139C RID: 5020 RVA: 0x0007481E File Offset: 0x00072A1E
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x0600139D RID: 5021 RVA: 0x00074826 File Offset: 0x00072A26
			public ruleset_return(CssParser grammar)
			{
			}

			// Token: 0x04000983 RID: 2435
			private object _tree;
		}

		// Token: 0x0200015A RID: 346
		private sealed class selectors_group_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004BE RID: 1214
			// (get) Token: 0x0600139E RID: 5022 RVA: 0x0007482E File Offset: 0x00072A2E
			// (set) Token: 0x0600139F RID: 5023 RVA: 0x00074836 File Offset: 0x00072A36
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004BF RID: 1215
			// (get) Token: 0x060013A0 RID: 5024 RVA: 0x0007483F File Offset: 0x00072A3F
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013A1 RID: 5025 RVA: 0x00074847 File Offset: 0x00072A47
			public selectors_group_return(CssParser grammar)
			{
			}

			// Token: 0x04000984 RID: 2436
			private object _tree;
		}

		// Token: 0x0200015B RID: 347
		private sealed class selector_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004C0 RID: 1216
			// (get) Token: 0x060013A2 RID: 5026 RVA: 0x0007484F File Offset: 0x00072A4F
			// (set) Token: 0x060013A3 RID: 5027 RVA: 0x00074857 File Offset: 0x00072A57
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004C1 RID: 1217
			// (get) Token: 0x060013A4 RID: 5028 RVA: 0x00074860 File Offset: 0x00072A60
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013A5 RID: 5029 RVA: 0x00074868 File Offset: 0x00072A68
			public selector_return(CssParser grammar)
			{
			}

			// Token: 0x04000985 RID: 2437
			private object _tree;
		}

		// Token: 0x0200015C RID: 348
		private sealed class combinator_simple_selector_sequence_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004C2 RID: 1218
			// (get) Token: 0x060013A6 RID: 5030 RVA: 0x00074870 File Offset: 0x00072A70
			// (set) Token: 0x060013A7 RID: 5031 RVA: 0x00074878 File Offset: 0x00072A78
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004C3 RID: 1219
			// (get) Token: 0x060013A8 RID: 5032 RVA: 0x00074881 File Offset: 0x00072A81
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013A9 RID: 5033 RVA: 0x00074889 File Offset: 0x00072A89
			public combinator_simple_selector_sequence_return(CssParser grammar)
			{
			}

			// Token: 0x04000986 RID: 2438
			private object _tree;
		}

		// Token: 0x0200015D RID: 349
		private sealed class combinator_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004C4 RID: 1220
			// (get) Token: 0x060013AA RID: 5034 RVA: 0x00074891 File Offset: 0x00072A91
			// (set) Token: 0x060013AB RID: 5035 RVA: 0x00074899 File Offset: 0x00072A99
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004C5 RID: 1221
			// (get) Token: 0x060013AC RID: 5036 RVA: 0x000748A2 File Offset: 0x00072AA2
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013AD RID: 5037 RVA: 0x000748AA File Offset: 0x00072AAA
			public combinator_return(CssParser grammar)
			{
			}

			// Token: 0x04000987 RID: 2439
			private object _tree;
		}

		// Token: 0x0200015E RID: 350
		private sealed class whitespace_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004C6 RID: 1222
			// (get) Token: 0x060013AE RID: 5038 RVA: 0x000748B2 File Offset: 0x00072AB2
			// (set) Token: 0x060013AF RID: 5039 RVA: 0x000748BA File Offset: 0x00072ABA
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004C7 RID: 1223
			// (get) Token: 0x060013B0 RID: 5040 RVA: 0x000748C3 File Offset: 0x00072AC3
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013B1 RID: 5041 RVA: 0x000748CB File Offset: 0x00072ACB
			public whitespace_return(CssParser grammar)
			{
			}

			// Token: 0x04000988 RID: 2440
			private object _tree;
		}

		// Token: 0x0200015F RID: 351
		private sealed class simple_selector_sequence_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004C8 RID: 1224
			// (get) Token: 0x060013B2 RID: 5042 RVA: 0x000748D3 File Offset: 0x00072AD3
			// (set) Token: 0x060013B3 RID: 5043 RVA: 0x000748DB File Offset: 0x00072ADB
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004C9 RID: 1225
			// (get) Token: 0x060013B4 RID: 5044 RVA: 0x000748E4 File Offset: 0x00072AE4
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013B5 RID: 5045 RVA: 0x000748EC File Offset: 0x00072AEC
			public simple_selector_sequence_return(CssParser grammar)
			{
			}

			// Token: 0x04000989 RID: 2441
			private object _tree;
		}

		// Token: 0x02000160 RID: 352
		private sealed class hashclassatnameattribpseudonegation_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004CA RID: 1226
			// (get) Token: 0x060013B6 RID: 5046 RVA: 0x000748F4 File Offset: 0x00072AF4
			// (set) Token: 0x060013B7 RID: 5047 RVA: 0x000748FC File Offset: 0x00072AFC
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004CB RID: 1227
			// (get) Token: 0x060013B8 RID: 5048 RVA: 0x00074905 File Offset: 0x00072B05
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013B9 RID: 5049 RVA: 0x0007490D File Offset: 0x00072B0D
			public hashclassatnameattribpseudonegation_return(CssParser grammar)
			{
			}

			// Token: 0x0400098A RID: 2442
			private object _tree;
		}

		// Token: 0x02000161 RID: 353
		private sealed class type_selector_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004CC RID: 1228
			// (get) Token: 0x060013BA RID: 5050 RVA: 0x00074915 File Offset: 0x00072B15
			// (set) Token: 0x060013BB RID: 5051 RVA: 0x0007491D File Offset: 0x00072B1D
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004CD RID: 1229
			// (get) Token: 0x060013BC RID: 5052 RVA: 0x00074926 File Offset: 0x00072B26
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013BD RID: 5053 RVA: 0x0007492E File Offset: 0x00072B2E
			public type_selector_return(CssParser grammar)
			{
			}

			// Token: 0x0400098B RID: 2443
			private object _tree;
		}

		// Token: 0x02000162 RID: 354
		private sealed class selector_namespace_prefix_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004CE RID: 1230
			// (get) Token: 0x060013BE RID: 5054 RVA: 0x00074936 File Offset: 0x00072B36
			// (set) Token: 0x060013BF RID: 5055 RVA: 0x0007493E File Offset: 0x00072B3E
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004CF RID: 1231
			// (get) Token: 0x060013C0 RID: 5056 RVA: 0x00074947 File Offset: 0x00072B47
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013C1 RID: 5057 RVA: 0x0007494F File Offset: 0x00072B4F
			public selector_namespace_prefix_return(CssParser grammar)
			{
			}

			// Token: 0x0400098C RID: 2444
			private object _tree;
		}

		// Token: 0x02000163 RID: 355
		private sealed class element_name_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004D0 RID: 1232
			// (get) Token: 0x060013C2 RID: 5058 RVA: 0x00074957 File Offset: 0x00072B57
			// (set) Token: 0x060013C3 RID: 5059 RVA: 0x0007495F File Offset: 0x00072B5F
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004D1 RID: 1233
			// (get) Token: 0x060013C4 RID: 5060 RVA: 0x00074968 File Offset: 0x00072B68
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013C5 RID: 5061 RVA: 0x00074970 File Offset: 0x00072B70
			public element_name_return(CssParser grammar)
			{
			}

			// Token: 0x0400098D RID: 2445
			private object _tree;
		}

		// Token: 0x02000164 RID: 356
		private sealed class universal_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004D2 RID: 1234
			// (get) Token: 0x060013C6 RID: 5062 RVA: 0x00074978 File Offset: 0x00072B78
			// (set) Token: 0x060013C7 RID: 5063 RVA: 0x00074980 File Offset: 0x00072B80
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004D3 RID: 1235
			// (get) Token: 0x060013C8 RID: 5064 RVA: 0x00074989 File Offset: 0x00072B89
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013C9 RID: 5065 RVA: 0x00074991 File Offset: 0x00072B91
			public universal_return(CssParser grammar)
			{
			}

			// Token: 0x0400098E RID: 2446
			private object _tree;
		}

		// Token: 0x02000165 RID: 357
		private sealed class class_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004D4 RID: 1236
			// (get) Token: 0x060013CA RID: 5066 RVA: 0x00074999 File Offset: 0x00072B99
			// (set) Token: 0x060013CB RID: 5067 RVA: 0x000749A1 File Offset: 0x00072BA1
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004D5 RID: 1237
			// (get) Token: 0x060013CC RID: 5068 RVA: 0x000749AA File Offset: 0x00072BAA
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013CD RID: 5069 RVA: 0x000749B2 File Offset: 0x00072BB2
			public class_return(CssParser grammar)
			{
			}

			// Token: 0x0400098F RID: 2447
			private object _tree;
		}

		// Token: 0x02000166 RID: 358
		private sealed class attrib_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004D6 RID: 1238
			// (get) Token: 0x060013CE RID: 5070 RVA: 0x000749BA File Offset: 0x00072BBA
			// (set) Token: 0x060013CF RID: 5071 RVA: 0x000749C2 File Offset: 0x00072BC2
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004D7 RID: 1239
			// (get) Token: 0x060013D0 RID: 5072 RVA: 0x000749CB File Offset: 0x00072BCB
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013D1 RID: 5073 RVA: 0x000749D3 File Offset: 0x00072BD3
			public attrib_return(CssParser grammar)
			{
			}

			// Token: 0x04000990 RID: 2448
			private object _tree;
		}

		// Token: 0x02000167 RID: 359
		private sealed class pseudo_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004D8 RID: 1240
			// (get) Token: 0x060013D2 RID: 5074 RVA: 0x000749DB File Offset: 0x00072BDB
			// (set) Token: 0x060013D3 RID: 5075 RVA: 0x000749E3 File Offset: 0x00072BE3
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004D9 RID: 1241
			// (get) Token: 0x060013D4 RID: 5076 RVA: 0x000749EC File Offset: 0x00072BEC
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013D5 RID: 5077 RVA: 0x000749F4 File Offset: 0x00072BF4
			public pseudo_return(CssParser grammar)
			{
			}

			// Token: 0x04000991 RID: 2449
			private object _tree;
		}

		// Token: 0x02000168 RID: 360
		private sealed class functional_pseudo_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004DA RID: 1242
			// (get) Token: 0x060013D6 RID: 5078 RVA: 0x000749FC File Offset: 0x00072BFC
			// (set) Token: 0x060013D7 RID: 5079 RVA: 0x00074A04 File Offset: 0x00072C04
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004DB RID: 1243
			// (get) Token: 0x060013D8 RID: 5080 RVA: 0x00074A0D File Offset: 0x00072C0D
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013D9 RID: 5081 RVA: 0x00074A15 File Offset: 0x00072C15
			public functional_pseudo_return(CssParser grammar)
			{
			}

			// Token: 0x04000992 RID: 2450
			private object _tree;
		}

		// Token: 0x02000169 RID: 361
		private sealed class selectorexpression_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004DC RID: 1244
			// (get) Token: 0x060013DA RID: 5082 RVA: 0x00074A1D File Offset: 0x00072C1D
			// (set) Token: 0x060013DB RID: 5083 RVA: 0x00074A25 File Offset: 0x00072C25
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004DD RID: 1245
			// (get) Token: 0x060013DC RID: 5084 RVA: 0x00074A2E File Offset: 0x00072C2E
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013DD RID: 5085 RVA: 0x00074A36 File Offset: 0x00072C36
			public selectorexpression_return(CssParser grammar)
			{
			}

			// Token: 0x04000993 RID: 2451
			private object _tree;
		}

		// Token: 0x0200016A RID: 362
		private sealed class negation_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004DE RID: 1246
			// (get) Token: 0x060013DE RID: 5086 RVA: 0x00074A3E File Offset: 0x00072C3E
			// (set) Token: 0x060013DF RID: 5087 RVA: 0x00074A46 File Offset: 0x00072C46
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004DF RID: 1247
			// (get) Token: 0x060013E0 RID: 5088 RVA: 0x00074A4F File Offset: 0x00072C4F
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013E1 RID: 5089 RVA: 0x00074A57 File Offset: 0x00072C57
			public negation_return(CssParser grammar)
			{
			}

			// Token: 0x04000994 RID: 2452
			private object _tree;
		}

		// Token: 0x0200016B RID: 363
		private sealed class negation_arg_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004E0 RID: 1248
			// (get) Token: 0x060013E2 RID: 5090 RVA: 0x00074A5F File Offset: 0x00072C5F
			// (set) Token: 0x060013E3 RID: 5091 RVA: 0x00074A67 File Offset: 0x00072C67
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004E1 RID: 1249
			// (get) Token: 0x060013E4 RID: 5092 RVA: 0x00074A70 File Offset: 0x00072C70
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013E5 RID: 5093 RVA: 0x00074A78 File Offset: 0x00072C78
			public negation_arg_return(CssParser grammar)
			{
			}

			// Token: 0x04000995 RID: 2453
			private object _tree;
		}

		// Token: 0x0200016C RID: 364
		private sealed class atname_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004E2 RID: 1250
			// (get) Token: 0x060013E6 RID: 5094 RVA: 0x00074A80 File Offset: 0x00072C80
			// (set) Token: 0x060013E7 RID: 5095 RVA: 0x00074A88 File Offset: 0x00072C88
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004E3 RID: 1251
			// (get) Token: 0x060013E8 RID: 5096 RVA: 0x00074A91 File Offset: 0x00072C91
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013E9 RID: 5097 RVA: 0x00074A99 File Offset: 0x00072C99
			public atname_return(CssParser grammar)
			{
			}

			// Token: 0x04000996 RID: 2454
			private object _tree;
		}

		// Token: 0x0200016D RID: 365
		private sealed class declaration_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004E4 RID: 1252
			// (get) Token: 0x060013EA RID: 5098 RVA: 0x00074AA1 File Offset: 0x00072CA1
			// (set) Token: 0x060013EB RID: 5099 RVA: 0x00074AA9 File Offset: 0x00072CA9
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004E5 RID: 1253
			// (get) Token: 0x060013EC RID: 5100 RVA: 0x00074AB2 File Offset: 0x00072CB2
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013ED RID: 5101 RVA: 0x00074ABA File Offset: 0x00072CBA
			public declaration_return(CssParser grammar)
			{
			}

			// Token: 0x04000997 RID: 2455
			private object _tree;
		}

		// Token: 0x0200016E RID: 366
		private sealed class stringoruri_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004E6 RID: 1254
			// (get) Token: 0x060013EE RID: 5102 RVA: 0x00074AC2 File Offset: 0x00072CC2
			// (set) Token: 0x060013EF RID: 5103 RVA: 0x00074ACA File Offset: 0x00072CCA
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004E7 RID: 1255
			// (get) Token: 0x060013F0 RID: 5104 RVA: 0x00074AD3 File Offset: 0x00072CD3
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013F1 RID: 5105 RVA: 0x00074ADB File Offset: 0x00072CDB
			public stringoruri_return(CssParser grammar)
			{
			}

			// Token: 0x04000998 RID: 2456
			private object _tree;
		}

		// Token: 0x0200016F RID: 367
		private sealed class styleSheetrules_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004E8 RID: 1256
			// (get) Token: 0x060013F2 RID: 5106 RVA: 0x00074AE3 File Offset: 0x00072CE3
			// (set) Token: 0x060013F3 RID: 5107 RVA: 0x00074AEB File Offset: 0x00072CEB
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004E9 RID: 1257
			// (get) Token: 0x060013F4 RID: 5108 RVA: 0x00074AF4 File Offset: 0x00072CF4
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013F5 RID: 5109 RVA: 0x00074AFC File Offset: 0x00072CFC
			public styleSheetrules_return(CssParser grammar)
			{
			}

			// Token: 0x04000999 RID: 2457
			private object _tree;
		}

		// Token: 0x02000170 RID: 368
		private sealed class prio_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004EA RID: 1258
			// (get) Token: 0x060013F6 RID: 5110 RVA: 0x00074B04 File Offset: 0x00072D04
			// (set) Token: 0x060013F7 RID: 5111 RVA: 0x00074B0C File Offset: 0x00072D0C
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004EB RID: 1259
			// (get) Token: 0x060013F8 RID: 5112 RVA: 0x00074B15 File Offset: 0x00072D15
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013F9 RID: 5113 RVA: 0x00074B1D File Offset: 0x00072D1D
			public prio_return(CssParser grammar)
			{
			}

			// Token: 0x0400099A RID: 2458
			private object _tree;
		}

		// Token: 0x02000171 RID: 369
		private sealed class expr_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004EC RID: 1260
			// (get) Token: 0x060013FA RID: 5114 RVA: 0x00074B25 File Offset: 0x00072D25
			// (set) Token: 0x060013FB RID: 5115 RVA: 0x00074B2D File Offset: 0x00072D2D
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004ED RID: 1261
			// (get) Token: 0x060013FC RID: 5116 RVA: 0x00074B36 File Offset: 0x00072D36
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x060013FD RID: 5117 RVA: 0x00074B3E File Offset: 0x00072D3E
			public expr_return(CssParser grammar)
			{
			}

			// Token: 0x0400099B RID: 2459
			private object _tree;
		}

		// Token: 0x02000172 RID: 370
		private sealed class termwithoperator_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004EE RID: 1262
			// (get) Token: 0x060013FE RID: 5118 RVA: 0x00074B46 File Offset: 0x00072D46
			// (set) Token: 0x060013FF RID: 5119 RVA: 0x00074B4E File Offset: 0x00072D4E
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004EF RID: 1263
			// (get) Token: 0x06001400 RID: 5120 RVA: 0x00074B57 File Offset: 0x00072D57
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x06001401 RID: 5121 RVA: 0x00074B5F File Offset: 0x00072D5F
			public termwithoperator_return(CssParser grammar)
			{
			}

			// Token: 0x0400099C RID: 2460
			private object _tree;
		}

		// Token: 0x02000173 RID: 371
		private sealed class term_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004F0 RID: 1264
			// (get) Token: 0x06001402 RID: 5122 RVA: 0x00074B67 File Offset: 0x00072D67
			// (set) Token: 0x06001403 RID: 5123 RVA: 0x00074B6F File Offset: 0x00072D6F
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004F1 RID: 1265
			// (get) Token: 0x06001404 RID: 5124 RVA: 0x00074B78 File Offset: 0x00072D78
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x06001405 RID: 5125 RVA: 0x00074B80 File Offset: 0x00072D80
			public term_return(CssParser grammar)
			{
			}

			// Token: 0x0400099D RID: 2461
			private object _tree;
		}

		// Token: 0x02000174 RID: 372
		private sealed class hash_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004F2 RID: 1266
			// (get) Token: 0x06001406 RID: 5126 RVA: 0x00074B88 File Offset: 0x00072D88
			// (set) Token: 0x06001407 RID: 5127 RVA: 0x00074B90 File Offset: 0x00072D90
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004F3 RID: 1267
			// (get) Token: 0x06001408 RID: 5128 RVA: 0x00074B99 File Offset: 0x00072D99
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x06001409 RID: 5129 RVA: 0x00074BA1 File Offset: 0x00072DA1
			public hash_return(CssParser grammar)
			{
			}

			// Token: 0x0400099E RID: 2462
			private object _tree;
		}

		// Token: 0x02000175 RID: 373
		private sealed class function_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004F4 RID: 1268
			// (get) Token: 0x0600140A RID: 5130 RVA: 0x00074BA9 File Offset: 0x00072DA9
			// (set) Token: 0x0600140B RID: 5131 RVA: 0x00074BB1 File Offset: 0x00072DB1
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004F5 RID: 1269
			// (get) Token: 0x0600140C RID: 5132 RVA: 0x00074BBA File Offset: 0x00072DBA
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x0600140D RID: 5133 RVA: 0x00074BC2 File Offset: 0x00072DC2
			public function_return(CssParser grammar)
			{
			}

			// Token: 0x0400099F RID: 2463
			private object _tree;
		}

		// Token: 0x02000176 RID: 374
		private sealed class beginfunc_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004F6 RID: 1270
			// (get) Token: 0x0600140E RID: 5134 RVA: 0x00074BCA File Offset: 0x00072DCA
			// (set) Token: 0x0600140F RID: 5135 RVA: 0x00074BD2 File Offset: 0x00072DD2
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004F7 RID: 1271
			// (get) Token: 0x06001410 RID: 5136 RVA: 0x00074BDB File Offset: 0x00072DDB
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x06001411 RID: 5137 RVA: 0x00074BE3 File Offset: 0x00072DE3
			public beginfunc_return(CssParser grammar)
			{
			}

			// Token: 0x040009A0 RID: 2464
			private object _tree;
		}

		// Token: 0x02000177 RID: 375
		private sealed class keyframes_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004F8 RID: 1272
			// (get) Token: 0x06001412 RID: 5138 RVA: 0x00074BEB File Offset: 0x00072DEB
			// (set) Token: 0x06001413 RID: 5139 RVA: 0x00074BF3 File Offset: 0x00072DF3
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004F9 RID: 1273
			// (get) Token: 0x06001414 RID: 5140 RVA: 0x00074BFC File Offset: 0x00072DFC
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x06001415 RID: 5141 RVA: 0x00074C04 File Offset: 0x00072E04
			public keyframes_return(CssParser grammar)
			{
			}

			// Token: 0x040009A1 RID: 2465
			private object _tree;
		}

		// Token: 0x02000178 RID: 376
		private sealed class keyframes_block_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004FA RID: 1274
			// (get) Token: 0x06001416 RID: 5142 RVA: 0x00074C0C File Offset: 0x00072E0C
			// (set) Token: 0x06001417 RID: 5143 RVA: 0x00074C14 File Offset: 0x00072E14
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004FB RID: 1275
			// (get) Token: 0x06001418 RID: 5144 RVA: 0x00074C1D File Offset: 0x00072E1D
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x06001419 RID: 5145 RVA: 0x00074C25 File Offset: 0x00072E25
			public keyframes_block_return(CssParser grammar)
			{
			}

			// Token: 0x040009A2 RID: 2466
			private object _tree;
		}

		// Token: 0x02000179 RID: 377
		private sealed class keyframes_selectors_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004FC RID: 1276
			// (get) Token: 0x0600141A RID: 5146 RVA: 0x00074C2D File Offset: 0x00072E2D
			// (set) Token: 0x0600141B RID: 5147 RVA: 0x00074C35 File Offset: 0x00072E35
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004FD RID: 1277
			// (get) Token: 0x0600141C RID: 5148 RVA: 0x00074C3E File Offset: 0x00072E3E
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x0600141D RID: 5149 RVA: 0x00074C46 File Offset: 0x00072E46
			public keyframes_selectors_return(CssParser grammar)
			{
			}

			// Token: 0x040009A3 RID: 2467
			private object _tree;
		}

		// Token: 0x0200017A RID: 378
		private sealed class keyframes_selector_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x170004FE RID: 1278
			// (get) Token: 0x0600141E RID: 5150 RVA: 0x00074C4E File Offset: 0x00072E4E
			// (set) Token: 0x0600141F RID: 5151 RVA: 0x00074C56 File Offset: 0x00072E56
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x170004FF RID: 1279
			// (get) Token: 0x06001420 RID: 5152 RVA: 0x00074C5F File Offset: 0x00072E5F
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x06001421 RID: 5153 RVA: 0x00074C67 File Offset: 0x00072E67
			public keyframes_selector_return(CssParser grammar)
			{
			}

			// Token: 0x040009A4 RID: 2468
			private object _tree;
		}

		// Token: 0x0200017B RID: 379
		private sealed class document_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x17000500 RID: 1280
			// (get) Token: 0x06001422 RID: 5154 RVA: 0x00074C6F File Offset: 0x00072E6F
			// (set) Token: 0x06001423 RID: 5155 RVA: 0x00074C77 File Offset: 0x00072E77
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x17000501 RID: 1281
			// (get) Token: 0x06001424 RID: 5156 RVA: 0x00074C80 File Offset: 0x00072E80
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x06001425 RID: 5157 RVA: 0x00074C88 File Offset: 0x00072E88
			public document_return(CssParser grammar)
			{
			}

			// Token: 0x040009A5 RID: 2469
			private object _tree;
		}

		// Token: 0x0200017C RID: 380
		private sealed class document_match_function_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<object>, IAstRuleReturnScope, IRuleReturnScope
		{
			// Token: 0x17000502 RID: 1282
			// (get) Token: 0x06001426 RID: 5158 RVA: 0x00074C90 File Offset: 0x00072E90
			// (set) Token: 0x06001427 RID: 5159 RVA: 0x00074C98 File Offset: 0x00072E98
			public object Tree
			{
				get
				{
					return this._tree;
				}
				set
				{
					this._tree = value;
				}
			}

			// Token: 0x17000503 RID: 1283
			// (get) Token: 0x06001428 RID: 5160 RVA: 0x00074CA1 File Offset: 0x00072EA1
			object IAstRuleReturnScope.Tree
			{
				get
				{
					return this.Tree;
				}
			}

			// Token: 0x06001429 RID: 5161 RVA: 0x00074CA9 File Offset: 0x00072EA9
			public document_match_function_return(CssParser grammar)
			{
			}

			// Token: 0x040009A6 RID: 2470
			private object _tree;
		}

		// Token: 0x0200017D RID: 381
		private class DFA25 : DFA
		{
			// Token: 0x0600142A RID: 5162 RVA: 0x00074CB4 File Offset: 0x00072EB4
			static DFA25()
			{
				int num = CssParser.DFA25.DFA25_transitionS.Length;
				CssParser.DFA25.DFA25_transition = new short[num][];
				for (int i = 0; i < num; i++)
				{
					CssParser.DFA25.DFA25_transition[i] = DFA.UnpackEncodedString(CssParser.DFA25.DFA25_transitionS[i]);
				}
			}

			// Token: 0x0600142B RID: 5163 RVA: 0x00074D7C File Offset: 0x00072F7C
			public DFA25(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 25;
				this.eot = CssParser.DFA25.DFA25_eot;
				this.eof = CssParser.DFA25.DFA25_eof;
				this.min = CssParser.DFA25.DFA25_min;
				this.max = CssParser.DFA25.DFA25_max;
				this.accept = CssParser.DFA25.DFA25_accept;
				this.special = CssParser.DFA25.DFA25_special;
				this.transition = CssParser.DFA25.DFA25_transition;
			}

			// Token: 0x17000504 RID: 1284
			// (get) Token: 0x0600142C RID: 5164 RVA: 0x00074DEB File Offset: 0x00072FEB
			public override string Description
			{
				get
				{
					return "()* loopback of 266:5: ( declaration ( SEMICOLON )? )*";
				}
			}

			// Token: 0x0600142D RID: 5165 RVA: 0x00074DF2 File Offset: 0x00072FF2
			public override void Error(NoViableAltException nvae)
			{
			}

			// Token: 0x040009A7 RID: 2471
			private const string DFA25_eotS = "\u0004￿";

			// Token: 0x040009A8 RID: 2472
			private const string DFA25_eofS = "\u0004￿";

			// Token: 0x040009A9 RID: 2473
			private const string DFA25_minS = "\u0002\u0013\u0002￿";

			// Token: 0x040009AA RID: 2474
			private const string DFA25_maxS = "\u0002T\u0002￿";

			// Token: 0x040009AB RID: 2475
			private const string DFA25_acceptS = "\u0002￿\u0001\u0002\u0001\u0001";

			// Token: 0x040009AC RID: 2476
			private const string DFA25_specialS = "\u0004￿}>";

			// Token: 0x040009AD RID: 2477
			private static readonly string[] DFA25_transitionS = new string[]
			{
				"\u0001\u0002\u0015￿\u0001\u0003\u0001\u0001!￿\u0001\u0003\a￿\u0001\u0003",
				"\u0001\u0002\u0015￿\u0001\u0003\u0001\u0001!￿\u0001\u0003\a￿\u0001\u0003",
				"",
				""
			};

			// Token: 0x040009AE RID: 2478
			private static readonly short[] DFA25_eot = DFA.UnpackEncodedString("\u0004￿");

			// Token: 0x040009AF RID: 2479
			private static readonly short[] DFA25_eof = DFA.UnpackEncodedString("\u0004￿");

			// Token: 0x040009B0 RID: 2480
			private static readonly char[] DFA25_min = DFA.UnpackEncodedStringToUnsignedChars("\u0002\u0013\u0002￿");

			// Token: 0x040009B1 RID: 2481
			private static readonly char[] DFA25_max = DFA.UnpackEncodedStringToUnsignedChars("\u0002T\u0002￿");

			// Token: 0x040009B2 RID: 2482
			private static readonly short[] DFA25_accept = DFA.UnpackEncodedString("\u0002￿\u0001\u0002\u0001\u0001");

			// Token: 0x040009B3 RID: 2483
			private static readonly short[] DFA25_special = DFA.UnpackEncodedString("\u0004￿}>");

			// Token: 0x040009B4 RID: 2484
			private static readonly short[][] DFA25_transition;
		}

		// Token: 0x0200017E RID: 382
		private class DFA33 : DFA
		{
			// Token: 0x0600142E RID: 5166 RVA: 0x00074DF4 File Offset: 0x00072FF4
			static DFA33()
			{
				int num = CssParser.DFA33.DFA33_transitionS.Length;
				CssParser.DFA33.DFA33_transition = new short[num][];
				for (int i = 0; i < num; i++)
				{
					CssParser.DFA33.DFA33_transition[i] = DFA.UnpackEncodedString(CssParser.DFA33.DFA33_transitionS[i]);
				}
			}

			// Token: 0x0600142F RID: 5167 RVA: 0x000750F8 File Offset: 0x000732F8
			public DFA33(BaseRecognizer recognizer, SpecialStateTransitionHandler specialStateTransition) : base(specialStateTransition)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 33;
				this.eot = CssParser.DFA33.DFA33_eot;
				this.eof = CssParser.DFA33.DFA33_eof;
				this.min = CssParser.DFA33.DFA33_min;
				this.max = CssParser.DFA33.DFA33_max;
				this.accept = CssParser.DFA33.DFA33_accept;
				this.special = CssParser.DFA33.DFA33_special;
				this.transition = CssParser.DFA33.DFA33_transition;
			}

			// Token: 0x17000505 RID: 1285
			// (get) Token: 0x06001430 RID: 5168 RVA: 0x00075168 File Offset: 0x00073368
			public override string Description
			{
				get
				{
					return "323:82: ( ( hashclassatnameattribpseudonegation )=> hashclassatnameattribpseudonegation )?";
				}
			}

			// Token: 0x06001431 RID: 5169 RVA: 0x0007516F File Offset: 0x0007336F
			public override void Error(NoViableAltException nvae)
			{
			}

			// Token: 0x040009B5 RID: 2485
			private const string DFA33_eotS = "D￿";

			// Token: 0x040009B6 RID: 2486
			private const string DFA33_eofS = "D￿";

			// Token: 0x040009B7 RID: 2487
			private const string DFA33_minS = "\u0001\a\u0004\0\u0001)\u0001\u000f\u0002￿\u0001\u0015\u0001F\u0001)\u0001\f\u0001!\u0001\0\u0003\f\u0006)\u0001\0\u0001\u0015\u0001\u000e\u0003\u0017\u0002S\u0002\r\u0001)\u0002\r\u0001)\u0001\u000f\u0001\r\u0001\0\u0002\r\u0001\u0015\u0001F\u0001)\u0001!\u0004\f\u0001\0\u0006)\u0001\r\u0001\u0015\u0004\u0017\u0002S\u0002\r";

			// Token: 0x040009B8 RID: 2488
			private const string DFA33_maxS = "\u0001i\u0004\0\u0001T\u0001[\u0002￿\u0001W\u0001F\u0001)\u0001\f\u0001[\u0001\0\u0003\f\u0006U\u0001\0\u0001W\u0001T\u0003¨\u0002S\u0002F\u0001T\u0002\r\u0001T\u0001[\u0001¨\u0001\0\u0002\r\u0001W\u0001F\u0001)\u0001[\u0001\r\u0003\f\u0001\0\u0006U\u0001\r\u0001W\u0004¨\u0002S\u0001¨\u0001\r";

			// Token: 0x040009B9 RID: 2489
			private const string DFA33_acceptS = "\a￿\u0001\u0002\u0001\u0001;￿";

			// Token: 0x040009BA RID: 2490
			private const string DFA33_specialS = "\u0001￿\u0001\0\u0001\u0001\u0001\u0002\u0001\u0003\t￿\u0001\u0004\t￿\u0001\u0005\u000f￿\u0001\u0006\n￿\u0001\a\u0010￿}>";

			// Token: 0x040009BB RID: 2491
			private static readonly string[] DFA33_transitionS = new string[]
			{
				"\u0001\u0004\u0006￿\u0001\u0003\u0001\u0006\u0001\a\u0001￿\u0001\a\u0010￿\u0001\a\u0002￿\u0001\u0002\u0002￿\u0001\a\u001c￿\u0002\a\u0004￿\u0001\u0001\u0005￿\u0001\u0005\u0001￿\u0001\a\u0004￿\u0001\a\u000f￿\u0001\a",
				"\u0001￿",
				"\u0001￿",
				"\u0001￿",
				"\u0001￿",
				"\u0001\t\u001c￿\u0001\v\r￿\u0001\n",
				"\u0001\r\u0011￿\u0001\u000f\a￿\u0001\u000e\r￿\u0001\u0011\a￿\u0001\f\u001b￿\u0001\u0010",
				"",
				"",
				"\u0001\u0017\u0006￿\u0001\u0015\u0010￿\u0001\u0016\u0018￿\u0001\v\u0001￿\u0001\u0012\n￿\u0001\u0018\u0002￿\u0001\u0014\u0001\u0013",
				"\u0001\v",
				"\u0001\u0019",
				"\u0001\u001a",
				"\u0001\u000f\a￿\u0001\u000e\r￿\u0001\u0011#￿\u0001\u0010",
				"\u0001￿",
				"\u0001\u001b",
				"\u0001\u001c",
				"\u0001\u001d",
				"\u0001\u001e+￿\u0001\u001f",
				"\u0001\u001e+￿\u0001\u001f",
				"\u0001\u001e+￿\u0001\u001f",
				"\u0001\u001e+￿\u0001\u001f",
				"\u0001\u001e+￿\u0001\u001f",
				"\u0001\u001e+￿\u0001\u001f",
				"\u0001￿",
				"\u0001\u0017\u0006￿\u0001\u0015\u0010￿\u0001\u0016\u001a￿\u0001\u0012\n￿\u0001\u0018\u0002￿\u0001\u0014\u0001\u0013",
				"\u0001$\u0001&\u0016￿\u0001#\u0002￿\u0001 \u001c￿\u0001\"\v￿\u0001%\u0001￿\u0001!",
				"\u0001'\u0011￿\u0001'\v￿\u0001'\n￿\u0001'\u0006￿\u0001'\r￿\u0001'R￿\u0001'",
				"\u0001'\u0011￿\u0001'\v￿\u0001'\n￿\u0001'\u0006￿\u0001'\r￿\u0001'R￿\u0001'",
				"\u0001'\u0011￿\u0001'\v￿\u0001'\n￿\u0001'\u0006￿\u0001'\r￿\u0001'R￿\u0001'",
				"\u0001\u0018",
				"\u0001\u0018",
				"\u0001(8￿\u0001\"",
				"\u0001(8￿\u0001\"",
				"\u0001**￿\u0001)",
				"\u0001(",
				"\u0001(",
				"\u0001+\u001c￿\u0001-\r￿\u0001,",
				"\u0001.\u0011￿\u00010\a￿\u0001/\r￿\u00012#￿\u00011",
				"\u00013\t￿\u0001'\u0011￿\u0001'\v￿\u0001'\n￿\u0001'\u0006￿\u0001'\r￿\u0001'R￿\u0001'",
				"\u0001￿",
				"\u0001(",
				"\u0001(",
				"\u00019\u0006￿\u00017\u0010￿\u00018\u0018￿\u0001-\u0001￿\u00014\n￿\u0001:\u0002￿\u00016\u00015",
				"\u0001-",
				"\u0001;",
				"\u00010\a￿\u0001/\r￿\u00012#￿\u00011",
				"\u0001<\u0001(",
				"\u0001=",
				"\u0001>",
				"\u0001?",
				"\u0001￿",
				"\u0001@+￿\u0001A",
				"\u0001@+￿\u0001A",
				"\u0001@+￿\u0001A",
				"\u0001@+￿\u0001A",
				"\u0001@+￿\u0001A",
				"\u0001@+￿\u0001A",
				"\u0001(",
				"\u00019\u0006￿\u00017\u0010￿\u00018\u001a￿\u00014\n￿\u0001:\u0002￿\u00016\u00015",
				"\u0001B\u0011￿\u0001B\v￿\u0001B\n￿\u0001B\u0006￿\u0001B\r￿\u0001BR￿\u0001B",
				"\u0001B\u0011￿\u0001B\v￿\u0001B\n￿\u0001B\u0006￿\u0001B\r￿\u0001BR￿\u0001B",
				"\u0001B\u0011￿\u0001B\v￿\u0001B\n￿\u0001B\u0006￿\u0001B\r￿\u0001BR￿\u0001B",
				"\u0001B\u0011￿\u0001B\v￿\u0001B\n￿\u0001B\u0006￿\u0001B\r￿\u0001BR￿\u0001B",
				"\u0001:",
				"\u0001:",
				"\u0001C\t￿\u0001B\u0011￿\u0001B\v￿\u0001B\n￿\u0001B\u0006￿\u0001B\r￿\u0001BR￿\u0001B",
				"\u0001("
			};

			// Token: 0x040009BC RID: 2492
			private static readonly short[] DFA33_eot = DFA.UnpackEncodedString("D￿");

			// Token: 0x040009BD RID: 2493
			private static readonly short[] DFA33_eof = DFA.UnpackEncodedString("D￿");

			// Token: 0x040009BE RID: 2494
			private static readonly char[] DFA33_min = DFA.UnpackEncodedStringToUnsignedChars("\u0001\a\u0004\0\u0001)\u0001\u000f\u0002￿\u0001\u0015\u0001F\u0001)\u0001\f\u0001!\u0001\0\u0003\f\u0006)\u0001\0\u0001\u0015\u0001\u000e\u0003\u0017\u0002S\u0002\r\u0001)\u0002\r\u0001)\u0001\u000f\u0001\r\u0001\0\u0002\r\u0001\u0015\u0001F\u0001)\u0001!\u0004\f\u0001\0\u0006)\u0001\r\u0001\u0015\u0004\u0017\u0002S\u0002\r");

			// Token: 0x040009BF RID: 2495
			private static readonly char[] DFA33_max = DFA.UnpackEncodedStringToUnsignedChars("\u0001i\u0004\0\u0001T\u0001[\u0002￿\u0001W\u0001F\u0001)\u0001\f\u0001[\u0001\0\u0003\f\u0006U\u0001\0\u0001W\u0001T\u0003¨\u0002S\u0002F\u0001T\u0002\r\u0001T\u0001[\u0001¨\u0001\0\u0002\r\u0001W\u0001F\u0001)\u0001[\u0001\r\u0003\f\u0001\0\u0006U\u0001\r\u0001W\u0004¨\u0002S\u0001¨\u0001\r");

			// Token: 0x040009C0 RID: 2496
			private static readonly short[] DFA33_accept = DFA.UnpackEncodedString("\a￿\u0001\u0002\u0001\u0001;￿");

			// Token: 0x040009C1 RID: 2497
			private static readonly short[] DFA33_special = DFA.UnpackEncodedString("\u0001￿\u0001\0\u0001\u0001\u0001\u0002\u0001\u0003\t￿\u0001\u0004\t￿\u0001\u0005\u000f￿\u0001\u0006\n￿\u0001\a\u0010￿}>");

			// Token: 0x040009C2 RID: 2498
			private static readonly short[][] DFA33_transition;
		}

		// Token: 0x0200017F RID: 383
		private class DFA48 : DFA
		{
			// Token: 0x06001432 RID: 5170 RVA: 0x00075174 File Offset: 0x00073374
			static DFA48()
			{
				int num = CssParser.DFA48.DFA48_transitionS.Length;
				CssParser.DFA48.DFA48_transition = new short[num][];
				for (int i = 0; i < num; i++)
				{
					CssParser.DFA48.DFA48_transition[i] = DFA.UnpackEncodedString(CssParser.DFA48.DFA48_transitionS[i]);
				}
			}

			// Token: 0x06001433 RID: 5171 RVA: 0x00075278 File Offset: 0x00073478
			public DFA48(BaseRecognizer recognizer, SpecialStateTransitionHandler specialStateTransition) : base(specialStateTransition)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 48;
				this.eot = CssParser.DFA48.DFA48_eot;
				this.eof = CssParser.DFA48.DFA48_eof;
				this.min = CssParser.DFA48.DFA48_min;
				this.max = CssParser.DFA48.DFA48_max;
				this.accept = CssParser.DFA48.DFA48_accept;
				this.special = CssParser.DFA48.DFA48_special;
				this.transition = CssParser.DFA48.DFA48_transition;
			}

			// Token: 0x17000506 RID: 1286
			// (get) Token: 0x06001434 RID: 5172 RVA: 0x000752E8 File Offset: 0x000734E8
			public override string Description
			{
				get
				{
					return "460:1: negation_arg : ( ( ( universal )=> universal ) | type_selector | hash | class | attrib | pseudo );";
				}
			}

			// Token: 0x06001435 RID: 5173 RVA: 0x000752EF File Offset: 0x000734EF
			public override void Error(NoViableAltException nvae)
			{
			}

			// Token: 0x040009C3 RID: 2499
			private const string DFA48_eotS = "\v￿";

			// Token: 0x040009C4 RID: 2500
			private const string DFA48_eofS = "\v￿";

			// Token: 0x040009C5 RID: 2501
			private const string DFA48_minS = "\u0001\u000e\u0001\r\u0001\0\u0001)\u0006￿\u0001\0";

			// Token: 0x040009C6 RID: 2502
			private const string DFA48_maxS = "\u0001T\u0001F\u0001\0\u0001T\u0006￿\u0001\0";

			// Token: 0x040009C7 RID: 2503
			private const string DFA48_acceptS = "\u0004￿\u0001\u0003\u0001\u0004\u0001\u0005\u0001\u0006\u0001\u0002\u0001\u0001\u0001￿";

			// Token: 0x040009C8 RID: 2504
			private const string DFA48_specialS = "\u0002￿\u0001\0\a￿\u0001\u0001}>";

			// Token: 0x040009C9 RID: 2505
			private static readonly string[] DFA48_transitionS = new string[]
			{
				"\u0001\u0005\u0001\a\u0016￿\u0001\u0004\u0002￿\u0001\u0001\u001c￿\u0001\u0003\v￿\u0001\u0006\u0001￿\u0001\u0002",
				"\u0001\b8￿\u0001\u0003",
				"\u0001￿",
				"\u0001\b*￿\u0001\n",
				"",
				"",
				"",
				"",
				"",
				"",
				"\u0001￿"
			};

			// Token: 0x040009CA RID: 2506
			private static readonly short[] DFA48_eot = DFA.UnpackEncodedString("\v￿");

			// Token: 0x040009CB RID: 2507
			private static readonly short[] DFA48_eof = DFA.UnpackEncodedString("\v￿");

			// Token: 0x040009CC RID: 2508
			private static readonly char[] DFA48_min = DFA.UnpackEncodedStringToUnsignedChars("\u0001\u000e\u0001\r\u0001\0\u0001)\u0006￿\u0001\0");

			// Token: 0x040009CD RID: 2509
			private static readonly char[] DFA48_max = DFA.UnpackEncodedStringToUnsignedChars("\u0001T\u0001F\u0001\0\u0001T\u0006￿\u0001\0");

			// Token: 0x040009CE RID: 2510
			private static readonly short[] DFA48_accept = DFA.UnpackEncodedString("\u0004￿\u0001\u0003\u0001\u0004\u0001\u0005\u0001\u0006\u0001\u0002\u0001\u0001\u0001￿");

			// Token: 0x040009CF RID: 2511
			private static readonly short[] DFA48_special = DFA.UnpackEncodedString("\u0002￿\u0001\0\a￿\u0001\u0001}>");

			// Token: 0x040009D0 RID: 2512
			private static readonly short[][] DFA48_transition;
		}

		// Token: 0x02000180 RID: 384
		private class DFA54 : DFA
		{
			// Token: 0x06001436 RID: 5174 RVA: 0x000752F4 File Offset: 0x000734F4
			static DFA54()
			{
				int num = CssParser.DFA54.DFA54_transitionS.Length;
				CssParser.DFA54.DFA54_transition = new short[num][];
				for (int i = 0; i < num; i++)
				{
					CssParser.DFA54.DFA54_transition[i] = DFA.UnpackEncodedString(CssParser.DFA54.DFA54_transitionS[i]);
				}
			}

			// Token: 0x06001437 RID: 5175 RVA: 0x000753DC File Offset: 0x000735DC
			public DFA54(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 54;
				this.eot = CssParser.DFA54.DFA54_eot;
				this.eof = CssParser.DFA54.DFA54_eof;
				this.min = CssParser.DFA54.DFA54_min;
				this.max = CssParser.DFA54.DFA54_max;
				this.accept = CssParser.DFA54.DFA54_accept;
				this.special = CssParser.DFA54.DFA54_special;
				this.transition = CssParser.DFA54.DFA54_transition;
			}

			// Token: 0x17000507 RID: 1287
			// (get) Token: 0x06001438 RID: 5176 RVA: 0x0007544B File Offset: 0x0007364B
			public override string Description
			{
				get
				{
					return "()* loopback of 500:29: ( termwithoperator )*";
				}
			}

			// Token: 0x06001439 RID: 5177 RVA: 0x00075452 File Offset: 0x00073652
			public override void Error(NoViableAltException nvae)
			{
			}

			// Token: 0x040009D1 RID: 2513
			private const string DFA54_eotS = "\b￿";

			// Token: 0x040009D2 RID: 2514
			private const string DFA54_eofS = "\b￿";

			// Token: 0x040009D3 RID: 2515
			private const string DFA54_minS = "\u0001\u0006\u0001￿\u0003\u0006\u0001￿\u0002\u0006";

			// Token: 0x040009D4 RID: 2516
			private const string DFA54_maxS = "\u0001c\u0001￿\u0003c\u0001￿\u0002c";

			// Token: 0x040009D5 RID: 2517
			private const string DFA54_acceptS = "\u0001￿\u0001\u0002\u0003￿\u0001\u0001\u0002￿";

			// Token: 0x040009D6 RID: 2518
			private const string DFA54_specialS = "\b￿}>";

			// Token: 0x040009D7 RID: 2519
			private static readonly string[] DFA54_transitionS = new string[]
			{
				"\u0001\u0005\u0006￿\u0001\u0001\u0002￿\u0001\u0005\u0002￿\u0001\u0001\b￿\u0001\u0005\u0002￿\u0003\u0005\u0004￿\u0001\u0005\u0002￿\u0001\u0003\u0002\u0001\u0005￿\u0001\u0005\u0003￿\u0003\u0005\b￿\u0001\u0005\u0004￿\u0001\u0005\u0001￿\u0001\u0005\u0003￿\u0001\u0005\u0001\u0004\u0001\u0005\u0001￿\u0001\u0001\u0001￿\u0001\u0005\u0002￿\u0001\u0002\u0001\u0005\u0004￿\u0002\u0005\a￿\u0001\u0005",
				"",
				"\u0001\u0005\u0019￿\u0002\u0005\u0004￿\u0001\u0005\u0002￿\u0001\u0003\a￿\u0001\u0005\u0003￿\u0003\u0005\b￿\u0001\u0005\u0004￿\u0001\u0005\u0001￿\u0001\u0005\u0003￿\u0003\u0005\u0003￿\u0001\u0005\u0003￿\u0001\u0005\u0004￿\u0002\u0005\a￿\u0001\u0005",
				"\u0001\u0005\u0005￿\u0002\u0005\u0001￿\u0001\u0001\u0001\u0005\u0002￿\u0001\u0005\b￿\u0001\u0005\u0002￿\u0003\u0005\u0004￿\u0001\u0005\u0002￿\u0001\u0005\u0001\u0006\u0001\u0005\u0005￿\u0001\u0005\u0003￿\u0003\u0005\b￿\u0001\u0005\u0004￿\u0001\u0005\u0001￿\u0001\u0005\u0003￿\u0003\u0005\u0001￿\u0001\u0005\u0001￿\u0001\u0005\u0002￿\u0002\u0005\u0004￿\u0002\u0005\a￿\u0001\u0005",
				"\u0001\u0005\u0006￿\u0001\u0005\u0001￿\u0001\u0001\u0001\u0005\u0002￿\u0001\u0005\b￿\u0001\u0005\u0002￿\u0003\u0005\u0004￿\u0001\u0005\u0002￿\u0003\u0005\u0005￿\u0001\u0005\u0003￿\u0003\u0005\b￿\u0001\u0005\u0004￿\u0001\u0005\u0001￿\u0001\u0005\u0003￿\u0003\u0005\u0001￿\u0001\u0005\u0001￿\u0001\u0005\u0002￿\u0002\u0005\u0004￿\u0002\u0005\a￿\u0001\u0005",
				"",
				"\u0001\u0005\u0006￿\u0001\u0005\u0001￿\u0001\u0001\u0001\u0005\u0002￿\u0001\u0005\b￿\u0001\u0005\u0002￿\u0003\u0005\u0004￿\u0001\u0005\u0002￿\u0001\u0005\u0001\a\u0001\u0005\u0005￿\u0001\u0005\u0003￿\u0003\u0005\b￿\u0001\u0005\u0004￿\u0001\u0005\u0001￿\u0001\u0005\u0003￿\u0003\u0005\u0001￿\u0001\u0005\u0001￿\u0001\u0005\u0002￿\u0002\u0005\u0004￿\u0002\u0005\a￿\u0001\u0005",
				"\u0001\u0005\u0006￿\u0001\u0005\u0001￿\u0001\u0001\u0001\u0005\u0002￿\u0001\u0005\b￿\u0001\u0005\u0002￿\u0003\u0005\u0004￿\u0001\u0005\u0002￿\u0001\u0005\u0001\a\u0001\u0005\u0005￿\u0001\u0005\u0003￿\u0003\u0005\b￿\u0001\u0005\u0004￿\u0001\u0005\u0001￿\u0001\u0005\u0003￿\u0003\u0005\u0001￿\u0001\u0005\u0001￿\u0001\u0005\u0002￿\u0002\u0005\u0004￿\u0002\u0005\a￿\u0001\u0005"
			};

			// Token: 0x040009D8 RID: 2520
			private static readonly short[] DFA54_eot = DFA.UnpackEncodedString("\b￿");

			// Token: 0x040009D9 RID: 2521
			private static readonly short[] DFA54_eof = DFA.UnpackEncodedString("\b￿");

			// Token: 0x040009DA RID: 2522
			private static readonly char[] DFA54_min = DFA.UnpackEncodedStringToUnsignedChars("\u0001\u0006\u0001￿\u0003\u0006\u0001￿\u0002\u0006");

			// Token: 0x040009DB RID: 2523
			private static readonly char[] DFA54_max = DFA.UnpackEncodedStringToUnsignedChars("\u0001c\u0001￿\u0003c\u0001￿\u0002c");

			// Token: 0x040009DC RID: 2524
			private static readonly short[] DFA54_accept = DFA.UnpackEncodedString("\u0001￿\u0001\u0002\u0003￿\u0001\u0001\u0002￿");

			// Token: 0x040009DD RID: 2525
			private static readonly short[] DFA54_special = DFA.UnpackEncodedString("\b￿}>");

			// Token: 0x040009DE RID: 2526
			private static readonly short[][] DFA54_transition;
		}

		// Token: 0x02000181 RID: 385
		private class DFA65 : DFA
		{
			// Token: 0x0600143A RID: 5178 RVA: 0x00075454 File Offset: 0x00073654
			static DFA65()
			{
				int num = CssParser.DFA65.DFA65_transitionS.Length;
				CssParser.DFA65.DFA65_transition = new short[num][];
				for (int i = 0; i < num; i++)
				{
					CssParser.DFA65.DFA65_transition[i] = DFA.UnpackEncodedString(CssParser.DFA65.DFA65_transitionS[i]);
				}
			}

			// Token: 0x0600143B RID: 5179 RVA: 0x0007554C File Offset: 0x0007374C
			public DFA65(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 65;
				this.eot = CssParser.DFA65.DFA65_eot;
				this.eof = CssParser.DFA65.DFA65_eof;
				this.min = CssParser.DFA65.DFA65_min;
				this.max = CssParser.DFA65.DFA65_max;
				this.accept = CssParser.DFA65.DFA65_accept;
				this.special = CssParser.DFA65.DFA65_special;
				this.transition = CssParser.DFA65.DFA65_transition;
			}

			// Token: 0x17000508 RID: 1288
			// (get) Token: 0x0600143C RID: 5180 RVA: 0x000755BB File Offset: 0x000737BB
			public override string Description
			{
				get
				{
					return "509:1: term : ( ( ( unary_operator )? (t= NUMBER |t= PERCENTAGE |t= LENGTH |t= RELATIVELENGTH |t= ANGLE |t= TIME |t= FREQ |t= RESOLUTION |t= SPEECH ) ) ( IMPORTANT_COMMENTS )* -> ^( TERM ( unary_operator )? ^( NUMBERBASEDVALUE $t) ( IMPORTANT_COMMENTS )* ) | URI ( IMPORTANT_COMMENTS )* -> ^( TERM ^( URIBASEDVALUE URI ) ( IMPORTANT_COMMENTS )* ) | (exp= ( MSIE_EXPRESSION ) ) ( IMPORTANT_COMMENTS )* -> ^( TERM ^( STRINGBASEDVALUE $exp) ( IMPORTANT_COMMENTS )* ) | IDENT ( IMPORTANT_COMMENTS )* -> ^( TERM ^( IDENTBASEDVALUE IDENT ) ( IMPORTANT_COMMENTS )* ) | STRING ( IMPORTANT_COMMENTS )* -> ^( TERM ^( STRINGBASEDVALUE STRING ) ( IMPORTANT_COMMENTS )* ) | hash ( IMPORTANT_COMMENTS )* -> ^( TERM ^( HEXBASEDVALUE hash ) ( IMPORTANT_COMMENTS )* ) | REPLACEMENTTOKEN -> ^( TERM ^( REPLACEMENTTOKENBASEDVALUE REPLACEMENTTOKEN ) ) | function ( IMPORTANT_COMMENTS )* -> ^( TERM function ( IMPORTANT_COMMENTS )* ) );";
				}
			}

			// Token: 0x0600143D RID: 5181 RVA: 0x000755C2 File Offset: 0x000737C2
			public override void Error(NoViableAltException nvae)
			{
			}

			// Token: 0x040009DF RID: 2527
			private const string DFA65_eotS = "\n￿";

			// Token: 0x040009E0 RID: 2528
			private const string DFA65_eofS = "\n￿";

			// Token: 0x040009E1 RID: 2529
			private const string DFA65_minS = "\u0001\u0006\u0003￿\u0001\u0006\u0005￿";

			// Token: 0x040009E2 RID: 2530
			private const string DFA65_maxS = "\u0001c\u0003￿\u0001c\u0005￿";

			// Token: 0x040009E3 RID: 2531
			private const string DFA65_acceptS = "\u0001￿\u0001\u0001\u0001\u0002\u0001\u0003\u0001￿\u0001\u0005\u0001\u0006\u0001\a\u0001\b\u0001\u0004";

			// Token: 0x040009E4 RID: 2532
			private const string DFA65_specialS = "\n￿}>";

			// Token: 0x040009E5 RID: 2533
			private static readonly string[] DFA65_transitionS = new string[]
			{
				"\u0001\u0001\u0019￿\u0001\u0001\u0001\b\u0004￿\u0001\u0006\u0002￿\u0001\u0004\a￿\u0001\u0001\u0003￿\u0001\u0001\u0001\u0003\u0001\b\b￿\u0001\u0001\u0004￿\u0001\u0001\u0001￿\u0001\u0001\u0003￿\u0001\u0001\u0001\a\u0001\u0001\u0003￿\u0001\u0001\u0003￿\u0001\u0005\u0004￿\u0001\u0001\u0001\b\a￿\u0001\u0002",
				"",
				"",
				"",
				"\u0001\t\u0005￿\u0001\b\u0001\t\u0002￿\u0001\t\u0002￿\u0001\t\b￿\u0001\t\u0002￿\u0003\t\u0004￿\u0001\t\u0002￿\u0003\t\u0005￿\u0001\t\u0003￿\u0003\t\b￿\u0001\t\u0004￿\u0001\t\u0001￿\u0001\t\u0003￿\u0003\t\u0001￿\u0001\t\u0001￿\u0001\t\u0002￿\u0002\t\u0004￿\u0002\t\a￿\u0001\t",
				"",
				"",
				"",
				"",
				""
			};

			// Token: 0x040009E6 RID: 2534
			private static readonly short[] DFA65_eot = DFA.UnpackEncodedString("\n￿");

			// Token: 0x040009E7 RID: 2535
			private static readonly short[] DFA65_eof = DFA.UnpackEncodedString("\n￿");

			// Token: 0x040009E8 RID: 2536
			private static readonly char[] DFA65_min = DFA.UnpackEncodedStringToUnsignedChars("\u0001\u0006\u0003￿\u0001\u0006\u0005￿");

			// Token: 0x040009E9 RID: 2537
			private static readonly char[] DFA65_max = DFA.UnpackEncodedStringToUnsignedChars("\u0001c\u0003￿\u0001c\u0005￿");

			// Token: 0x040009EA RID: 2538
			private static readonly short[] DFA65_accept = DFA.UnpackEncodedString("\u0001￿\u0001\u0001\u0001\u0002\u0001\u0003\u0001￿\u0001\u0005\u0001\u0006\u0001\a\u0001\b\u0001\u0004");

			// Token: 0x040009EB RID: 2539
			private static readonly short[] DFA65_special = DFA.UnpackEncodedString("\n￿}>");

			// Token: 0x040009EC RID: 2540
			private static readonly short[][] DFA65_transition;
		}

		// Token: 0x02000182 RID: 386
		private static class Follow
		{
			// Token: 0x0600143E RID: 5182 RVA: 0x000755F8 File Offset: 0x000737F8
			// Note: this type is marked as 'beforefieldinit'.
			static Follow()
			{
				ulong[] bits = new ulong[1];
				CssParser.Follow._styleSheet_in_main653 = new BitSet(bits);
				CssParser.Follow._EOF_in_main659 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._CHARSET_SYM_in_styleSheet683 = new BitSet(new ulong[]
				{
					0UL,
					2097152UL
				});
				CssParser.Follow._STRING_in_styleSheet685 = new BitSet(new ulong[]
				{
					0UL,
					32768UL
				});
				CssParser.Follow._SEMICOLON_in_styleSheet687 = new BitSet(new ulong[]
				{
					292899177417982082UL,
					1099512942672UL
				});
				CssParser.Follow._styleimport_in_styleSheet691 = new BitSet(new ulong[]
				{
					292899177417982082UL,
					1099512942672UL
				});
				CssParser.Follow._namespace_in_styleSheet694 = new BitSet(new ulong[]
				{
					292881585231937666UL,
					1099512942672UL
				});
				CssParser.Follow._styleSheetRulesOrComment_in_styleSheet697 = new BitSet(new ulong[]
				{
					4651209080225922UL,
					1099512942672UL
				});
				CssParser.Follow._IMPORTANT_COMMENTS_in_styleSheetRulesOrComment756 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._styleSheetrules_in_styleSheetRulesOrComment764 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._IMPORT_SYM_in_styleimport784 = new BitSet(new ulong[]
				{
					0UL,
					34361835520UL
				});
				CssParser.Follow._stringoruri_in_styleimport786 = new BitSet(new ulong[]
				{
					9223374235878035456UL,
					32772UL
				});
				CssParser.Follow._media_query_list_in_styleimport788 = new BitSet(new ulong[]
				{
					0UL,
					32768UL
				});
				CssParser.Follow._SEMICOLON_in_styleimport791 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._NAMESPACE_SYM_in_namespace826 = new BitSet(new ulong[]
				{
					2199023255552UL,
					34361835520UL
				});
				CssParser.Follow._namespace_prefix_in_namespace828 = new BitSet(new ulong[]
				{
					0UL,
					34361835520UL
				});
				CssParser.Follow._stringoruri_in_namespace831 = new BitSet(new ulong[]
				{
					0UL,
					32768UL
				});
				CssParser.Follow._SEMICOLON_in_namespace833 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._IDENT_in_namespace_prefix865 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._WG_DPI_SYM_in_wg_dpi894 = new BitSet(new ulong[]
				{
					0UL,
					1UL
				});
				CssParser.Follow._NUMBER_in_wg_dpi896 = new BitSet(new ulong[]
				{
					0UL,
					32768UL
				});
				CssParser.Follow._SEMICOLON_in_wg_dpi898 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._MEDIA_SYM_in_media930 = new BitSet(new ulong[]
				{
					9223374235878297600UL,
					4UL
				});
				CssParser.Follow._media_query_list_in_media932 = new BitSet(new ulong[]
				{
					262144UL
				});
				CssParser.Follow._CURLY_BEGIN_in_media935 = new BitSet(new ulong[]
				{
					2473901736064UL,
					1314896UL
				});
				CssParser.Follow._ruleset_in_media939 = new BitSet(new ulong[]
				{
					2473901736064UL,
					1314896UL
				});
				CssParser.Follow._page_in_media943 = new BitSet(new ulong[]
				{
					2473901736064UL,
					1314896UL
				});
				CssParser.Follow._CURLY_END_in_media948 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._media_query_in_media_query_list997 = new BitSet(new ulong[]
				{
					65538UL
				});
				CssParser.Follow._COMMA_in_media_query_list1000 = new BitSet(new ulong[]
				{
					9223374235878035456UL,
					4UL
				});
				CssParser.Follow._media_query_in_media_query_list1002 = new BitSet(new ulong[]
				{
					65538UL
				});
				CssParser.Follow._ONLY_in_media_query1036 = new BitSet(new ulong[]
				{
					9223374235878031360UL,
					4UL
				});
				CssParser.Follow._NOT_in_media_query1040 = new BitSet(new ulong[]
				{
					9223374235878031360UL,
					4UL
				});
				CssParser.Follow._media_type_in_media_query1044 = new BitSet(new ulong[]
				{
					34UL
				});
				CssParser.Follow._AND_in_media_query1047 = new BitSet(new ulong[]
				{
					9223374235878035456UL,
					4UL
				});
				CssParser.Follow._media_expression_in_media_query1049 = new BitSet(new ulong[]
				{
					34UL
				});
				CssParser.Follow._media_expression_in_media_query1087 = new BitSet(new ulong[]
				{
					34UL
				});
				CssParser.Follow._AND_in_media_query1090 = new BitSet(new ulong[]
				{
					9223374235878035456UL,
					4UL
				});
				CssParser.Follow._media_expression_in_media_query1092 = new BitSet(new ulong[]
				{
					34UL
				});
				CssParser.Follow._IDENT_in_media_type1122 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._CIRCLE_BEGIN_in_media_expression1145 = new BitSet(new ulong[]
				{
					2199023255552UL,
					4096UL
				});
				CssParser.Follow._media_feature_in_media_expression1147 = new BitSet(new ulong[]
				{
					40960UL
				});
				CssParser.Follow._COLON_in_media_expression1150 = new BitSet(new ulong[]
				{
					63620229569183808UL,
					34563307681UL
				});
				CssParser.Follow._expr_in_media_expression1152 = new BitSet(new ulong[]
				{
					8192UL
				});
				CssParser.Follow._CIRCLE_END_in_media_expression1156 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._IDENT_in_media_feature1183 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._REPLACEMENTTOKEN_in_media_feature1197 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._PAGE_SYM_in_page1224 = new BitSet(new ulong[]
				{
					294912UL
				});
				CssParser.Follow._pseudo_page_in_page1226 = new BitSet(new ulong[]
				{
					262144UL
				});
				CssParser.Follow._CURLY_BEGIN_in_page1229 = new BitSet(new ulong[]
				{
					6597070290944UL,
					1052672UL
				});
				CssParser.Follow._declaration_in_page1232 = new BitSet(new ulong[]
				{
					6597070290944UL,
					1085440UL
				});
				CssParser.Follow._SEMICOLON_in_page1234 = new BitSet(new ulong[]
				{
					6597070290944UL,
					1052672UL
				});
				CssParser.Follow._CURLY_END_in_page1239 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._COLON_in_pseudo_page1280 = new BitSet(new ulong[]
				{
					2199023255552UL
				});
				CssParser.Follow._IDENT_in_pseudo_page1282 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._set_in_operator1314 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._MINUS_in_unary_operator1349 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._PLUS_in_unary_operator1365 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._STAR_in_property1394 = new BitSet(new ulong[]
				{
					2199023255552UL
				});
				CssParser.Follow._IDENT_in_property1398 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._IMPORTANT_COMMENTS_in_property1400 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._REPLACEMENTTOKEN_in_property1424 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._selectors_group_in_ruleset1454 = new BitSet(new ulong[]
				{
					262144UL
				});
				CssParser.Follow._CURLY_BEGIN_in_ruleset1460 = new BitSet(new ulong[]
				{
					6597070290944UL,
					1052672UL
				});
				CssParser.Follow._declaration_in_ruleset1467 = new BitSet(new ulong[]
				{
					6597070290944UL,
					1085440UL
				});
				CssParser.Follow._SEMICOLON_in_ruleset1469 = new BitSet(new ulong[]
				{
					6597070290944UL,
					1052672UL
				});
				CssParser.Follow._IMPORTANT_COMMENTS_in_ruleset1475 = new BitSet(new ulong[]
				{
					4398047035392UL
				});
				CssParser.Follow._CURLY_END_in_ruleset1482 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._selector_in_selectors_group1523 = new BitSet(new ulong[]
				{
					65538UL
				});
				CssParser.Follow._COMMA_in_selectors_group1526 = new BitSet(new ulong[]
				{
					2473901211776UL,
					1314880UL
				});
				CssParser.Follow._selector_in_selectors_group1528 = new BitSet(new ulong[]
				{
					65538UL
				});
				CssParser.Follow._simple_selector_sequence_in_selector1559 = new BitSet(new ulong[]
				{
					34359738370UL,
					2199056810112UL
				});
				CssParser.Follow._combinator_simple_selector_sequence_in_selector1562 = new BitSet(new ulong[]
				{
					34359738370UL,
					2199056810112UL
				});
				CssParser.Follow._combinator_in_combinator_simple_selector_sequence1601 = new BitSet(new ulong[]
				{
					2473901211776UL,
					1314880UL
				});
				CssParser.Follow._simple_selector_sequence_in_combinator_simple_selector_sequence1603 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._PLUS_in_combinator1644 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._GREATER_in_combinator1655 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._TILDE_in_combinator1666 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._whitespace_in_combinator1687 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._WS_in_whitespace1728 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._universal_in_simple_selector_sequence1783 = new BitSet(new ulong[]
				{
					2473901211776UL,
					2199024570432UL
				});
				CssParser.Follow._type_selector_in_simple_selector_sequence1793 = new BitSet(new ulong[]
				{
					2473901211776UL,
					2199024570432UL
				});
				CssParser.Follow._whitespace_in_simple_selector_sequence1797 = new BitSet(new ulong[]
				{
					2473901211778UL,
					1314880UL
				});
				CssParser.Follow._hashclassatnameattribpseudonegation_in_simple_selector_sequence1806 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._hashclassatnameattribpseudonegation_in_simple_selector_sequence1848 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._REPLACEMENTTOKEN_in_hashclassatnameattribpseudonegation1878 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._hash_in_hashclassatnameattribpseudonegation1902 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._class_in_hashclassatnameattribpseudonegation1922 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._atname_in_hashclassatnameattribpseudonegation1942 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._attrib_in_hashclassatnameattribpseudonegation1962 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._pseudo_in_hashclassatnameattribpseudonegation1982 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._negation_in_hashclassatnameattribpseudonegation2002 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._selector_namespace_prefix_in_type_selector2047 = new BitSet(new ulong[]
				{
					2199023255552UL,
					1048576UL
				});
				CssParser.Follow._element_name_in_type_selector2051 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._element_name_in_selector_namespace_prefix2085 = new BitSet(new ulong[]
				{
					0UL,
					64UL
				});
				CssParser.Follow._PIPE_in_selector_namespace_prefix2088 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._IDENT_in_element_name2117 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._STAR_in_element_name2137 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._selector_namespace_prefix_in_universal2174 = new BitSet(new ulong[]
				{
					0UL,
					1048576UL
				});
				CssParser.Follow._STAR_in_universal2178 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._CLASS_IDENT_in_class2207 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._SQUARE_BEGIN_in_attrib2246 = new BitSet(new ulong[]
				{
					2199023255552UL,
					1048640UL
				});
				CssParser.Follow._selector_namespace_prefix_in_attrib2257 = new BitSet(new ulong[]
				{
					2199023255552UL
				});
				CssParser.Follow._IDENT_in_attrib2262 = new BitSet(new ulong[]
				{
					35184642621440UL,
					13107456UL
				});
				CssParser.Follow._PREFIXMATCH_in_attrib2289 = new BitSet(new ulong[]
				{
					2199023255552UL,
					2097152UL
				});
				CssParser.Follow._SUFFIXMATCH_in_attrib2293 = new BitSet(new ulong[]
				{
					2199023255552UL,
					2097152UL
				});
				CssParser.Follow._SUBSTRINGMATCH_in_attrib2297 = new BitSet(new ulong[]
				{
					2199023255552UL,
					2097152UL
				});
				CssParser.Follow._EQUALS_in_attrib2301 = new BitSet(new ulong[]
				{
					2199023255552UL,
					2097152UL
				});
				CssParser.Follow._INCLUDES_in_attrib2305 = new BitSet(new ulong[]
				{
					2199023255552UL,
					2097152UL
				});
				CssParser.Follow._DASHMATCH_in_attrib2309 = new BitSet(new ulong[]
				{
					2199023255552UL,
					2097152UL
				});
				CssParser.Follow._IDENT_in_attrib2327 = new BitSet(new ulong[]
				{
					0UL,
					524288UL
				});
				CssParser.Follow._STRING_in_attrib2329 = new BitSet(new ulong[]
				{
					0UL,
					524288UL
				});
				CssParser.Follow._SQUARE_END_in_attrib2347 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._COLON_in_pseudo2420 = new BitSet(new ulong[]
				{
					2199023288320UL
				});
				CssParser.Follow._COLON_in_pseudo2424 = new BitSet(new ulong[]
				{
					2199023255552UL
				});
				CssParser.Follow._IDENT_in_pseudo2429 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._COLON_in_pseudo2467 = new BitSet(new ulong[]
				{
					63620229569216576UL,
					34563307681UL
				});
				CssParser.Follow._COLON_in_pseudo2471 = new BitSet(new ulong[]
				{
					63620229569216576UL,
					34563307681UL
				});
				CssParser.Follow._functional_pseudo_in_pseudo2474 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._beginfunc_in_functional_pseudo2515 = new BitSet(new ulong[]
				{
					9009398286385152UL,
					2097281UL,
					1099511627776UL
				});
				CssParser.Follow._selectorexpression_in_functional_pseudo2517 = new BitSet(new ulong[]
				{
					8192UL
				});
				CssParser.Follow._CIRCLE_END_in_functional_pseudo2519 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._set_in_selectorexpression2561 = new BitSet(new ulong[]
				{
					9009398286385154UL,
					2097281UL,
					1099511627776UL
				});
				CssParser.Follow._COLON_in_negation2594 = new BitSet(new ulong[]
				{
					9223372036854775808UL
				});
				CssParser.Follow._NOT_in_negation2596 = new BitSet(new ulong[]
				{
					4096UL
				});
				CssParser.Follow._CIRCLE_BEGIN_in_negation2598 = new BitSet(new ulong[]
				{
					2473901211648UL,
					1310784UL
				});
				CssParser.Follow._negation_arg_in_negation2601 = new BitSet(new ulong[]
				{
					8192UL
				});
				CssParser.Follow._CIRCLE_END_in_negation2603 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._universal_in_negation_arg2640 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._type_selector_in_negation_arg2643 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._hash_in_negation_arg2645 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._class_in_negation_arg2647 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._attrib_in_negation_arg2649 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._pseudo_in_negation_arg2651 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._AT_NAME_in_atname2666 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._IMPORTANT_COMMENTS_in_declaration2698 = new BitSet(new ulong[]
				{
					6597069766656UL,
					1052672UL
				});
				CssParser.Follow._property_in_declaration2701 = new BitSet(new ulong[]
				{
					32768UL
				});
				CssParser.Follow._COLON_in_declaration2703 = new BitSet(new ulong[]
				{
					63620229569183808UL,
					34563307681UL
				});
				CssParser.Follow._expr_in_declaration2705 = new BitSet(new ulong[]
				{
					8796093022210UL
				});
				CssParser.Follow._prio_in_declaration2707 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._STRING_in_stringoruri2747 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._URI_in_stringoruri2767 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._ruleset_in_styleSheetrules2796 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._media_in_styleSheetrules2798 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._page_in_styleSheetrules2800 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._keyframes_in_styleSheetrules2802 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._document_in_styleSheetrules2804 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._wg_dpi_in_styleSheetrules2806 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._IMPORTANT_SYM_in_prio2826 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._IMPORTANT_COMMENTS_in_expr2856 = new BitSet(new ulong[]
				{
					63620229569183808UL,
					34563307681UL
				});
				CssParser.Follow._term_in_expr2859 = new BitSet(new ulong[]
				{
					63620231985168450UL,
					34564356257UL
				});
				CssParser.Follow._termwithoperator_in_expr2862 = new BitSet(new ulong[]
				{
					63620231985168450UL,
					34564356257UL
				});
				CssParser.Follow._operator_in_termwithoperator2902 = new BitSet(new ulong[]
				{
					63620229569183808UL,
					34563307681UL
				});
				CssParser.Follow._term_in_termwithoperator2905 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._unary_operator_in_term2943 = new BitSet(new ulong[]
				{
					562954248388672UL,
					67250209UL
				});
				CssParser.Follow._NUMBER_in_term2951 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._PERCENTAGE_in_term2959 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._LENGTH_in_term2967 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._RELATIVELENGTH_in_term2975 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._ANGLE_in_term2983 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._TIME_in_term2991 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._FREQ_in_term2999 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._RESOLUTION_in_term3007 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._SPEECH_in_term3015 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._IMPORTANT_COMMENTS_in_term3020 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._URI_in_term3052 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._IMPORTANT_COMMENTS_in_term3054 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._MSIE_EXPRESSION_in_term3088 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._IMPORTANT_COMMENTS_in_term3093 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._IDENT_in_term3122 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._IMPORTANT_COMMENTS_in_term3124 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._STRING_in_term3152 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._IMPORTANT_COMMENTS_in_term3154 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._hash_in_term3182 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._IMPORTANT_COMMENTS_in_term3184 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._REPLACEMENTTOKEN_in_term3209 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._function_in_term3233 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._IMPORTANT_COMMENTS_in_term3235 = new BitSet(new ulong[]
				{
					4398046511106UL
				});
				CssParser.Follow._HASH_IDENT_in_hash3268 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._beginfunc_in_function3300 = new BitSet(new ulong[]
				{
					63620229569192000UL,
					34563307681UL
				});
				CssParser.Follow._expr_in_function3302 = new BitSet(new ulong[]
				{
					8192UL
				});
				CssParser.Follow._CIRCLE_END_in_function3305 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._IDENT_in_beginfunc3337 = new BitSet(new ulong[]
				{
					4096UL
				});
				CssParser.Follow._CIRCLE_BEGIN_in_beginfunc3339 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._FROM_in_beginfunc3361 = new BitSet(new ulong[]
				{
					4096UL
				});
				CssParser.Follow._CIRCLE_BEGIN_in_beginfunc3363 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._TO_in_beginfunc3383 = new BitSet(new ulong[]
				{
					4096UL
				});
				CssParser.Follow._CIRCLE_BEGIN_in_beginfunc3385 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._MSIE_IMAGE_TRANSFORM_in_beginfunc3406 = new BitSet(new ulong[]
				{
					4096UL
				});
				CssParser.Follow._CIRCLE_BEGIN_in_beginfunc3408 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._KEYFRAMES_SYM_in_keyframes3438 = new BitSet(new ulong[]
				{
					2199023255552UL,
					2097152UL
				});
				CssParser.Follow._IDENT_in_keyframes3441 = new BitSet(new ulong[]
				{
					262144UL
				});
				CssParser.Follow._STRING_in_keyframes3443 = new BitSet(new ulong[]
				{
					262144UL
				});
				CssParser.Follow._CURLY_BEGIN_in_keyframes3446 = new BitSet(new ulong[]
				{
					8590458880UL,
					134217760UL
				});
				CssParser.Follow._keyframes_block_in_keyframes3448 = new BitSet(new ulong[]
				{
					8590458880UL,
					134217760UL
				});
				CssParser.Follow._CURLY_END_in_keyframes3451 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._keyframes_selectors_in_keyframes_block3507 = new BitSet(new ulong[]
				{
					262144UL
				});
				CssParser.Follow._CURLY_BEGIN_in_keyframes_block3509 = new BitSet(new ulong[]
				{
					6597070290944UL,
					1052672UL
				});
				CssParser.Follow._declaration_in_keyframes_block3512 = new BitSet(new ulong[]
				{
					6597070290944UL,
					1085440UL
				});
				CssParser.Follow._SEMICOLON_in_keyframes_block3514 = new BitSet(new ulong[]
				{
					6597070290944UL,
					1052672UL
				});
				CssParser.Follow._CURLY_END_in_keyframes_block3519 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._keyframes_selector_in_keyframes_selectors3561 = new BitSet(new ulong[]
				{
					65538UL
				});
				CssParser.Follow._COMMA_in_keyframes_selectors3564 = new BitSet(new ulong[]
				{
					8589934592UL,
					134217760UL
				});
				CssParser.Follow._keyframes_selector_in_keyframes_selectors3566 = new BitSet(new ulong[]
				{
					65538UL
				});
				CssParser.Follow._set_in_keyframes_selector3596 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._DOCUMENT_SYM_in_document3619 = new BitSet(new ulong[]
				{
					33554432UL,
					171798709248UL
				});
				CssParser.Follow._S_in_document3621 = new BitSet(new ulong[]
				{
					33554432UL,
					171798709248UL
				});
				CssParser.Follow._document_match_function_in_document3624 = new BitSet(new ulong[]
				{
					262144UL,
					16384UL
				});
				CssParser.Follow._S_in_document3626 = new BitSet(new ulong[]
				{
					262144UL,
					16384UL
				});
				CssParser.Follow._CURLY_BEGIN_in_document3629 = new BitSet(new ulong[]
				{
					2473901736064UL,
					1314880UL
				});
				CssParser.Follow._ruleset_in_document3631 = new BitSet(new ulong[]
				{
					2473901736064UL,
					1314880UL
				});
				CssParser.Follow._CURLY_END_in_document3634 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._URI_in_document_match_function3678 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._URLPREFIX_FUNCTION_in_document_match_function3699 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._DOMAIN_FUNCTION_in_document_match_function3720 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._REGEXP_FUNCTION_in_document_match_function3740 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._WS_in_synpred1_CssParser1723 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._universal_in_synpred2_CssParser1778 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._type_selector_in_synpred3_CssParser1788 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._hashclassatnameattribpseudonegation_in_synpred4_CssParser1801 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._hashclassatnameattribpseudonegation_in_synpred5_CssParser1843 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._selector_namespace_prefix_in_synpred6_CssParser2042 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._selector_namespace_prefix_in_synpred7_CssParser2169 = new BitSet(new ulong[]
				{
					2UL
				});
				CssParser.Follow._universal_in_synpred8_CssParser2635 = new BitSet(new ulong[]
				{
					2UL
				});
			}

			// Token: 0x040009ED RID: 2541
			public static readonly BitSet _styleSheet_in_main653;

			// Token: 0x040009EE RID: 2542
			public static readonly BitSet _EOF_in_main659;

			// Token: 0x040009EF RID: 2543
			public static readonly BitSet _CHARSET_SYM_in_styleSheet683;

			// Token: 0x040009F0 RID: 2544
			public static readonly BitSet _STRING_in_styleSheet685;

			// Token: 0x040009F1 RID: 2545
			public static readonly BitSet _SEMICOLON_in_styleSheet687;

			// Token: 0x040009F2 RID: 2546
			public static readonly BitSet _styleimport_in_styleSheet691;

			// Token: 0x040009F3 RID: 2547
			public static readonly BitSet _namespace_in_styleSheet694;

			// Token: 0x040009F4 RID: 2548
			public static readonly BitSet _styleSheetRulesOrComment_in_styleSheet697;

			// Token: 0x040009F5 RID: 2549
			public static readonly BitSet _IMPORTANT_COMMENTS_in_styleSheetRulesOrComment756;

			// Token: 0x040009F6 RID: 2550
			public static readonly BitSet _styleSheetrules_in_styleSheetRulesOrComment764;

			// Token: 0x040009F7 RID: 2551
			public static readonly BitSet _IMPORT_SYM_in_styleimport784;

			// Token: 0x040009F8 RID: 2552
			public static readonly BitSet _stringoruri_in_styleimport786;

			// Token: 0x040009F9 RID: 2553
			public static readonly BitSet _media_query_list_in_styleimport788;

			// Token: 0x040009FA RID: 2554
			public static readonly BitSet _SEMICOLON_in_styleimport791;

			// Token: 0x040009FB RID: 2555
			public static readonly BitSet _NAMESPACE_SYM_in_namespace826;

			// Token: 0x040009FC RID: 2556
			public static readonly BitSet _namespace_prefix_in_namespace828;

			// Token: 0x040009FD RID: 2557
			public static readonly BitSet _stringoruri_in_namespace831;

			// Token: 0x040009FE RID: 2558
			public static readonly BitSet _SEMICOLON_in_namespace833;

			// Token: 0x040009FF RID: 2559
			public static readonly BitSet _IDENT_in_namespace_prefix865;

			// Token: 0x04000A00 RID: 2560
			public static readonly BitSet _WG_DPI_SYM_in_wg_dpi894;

			// Token: 0x04000A01 RID: 2561
			public static readonly BitSet _NUMBER_in_wg_dpi896;

			// Token: 0x04000A02 RID: 2562
			public static readonly BitSet _SEMICOLON_in_wg_dpi898;

			// Token: 0x04000A03 RID: 2563
			public static readonly BitSet _MEDIA_SYM_in_media930;

			// Token: 0x04000A04 RID: 2564
			public static readonly BitSet _media_query_list_in_media932;

			// Token: 0x04000A05 RID: 2565
			public static readonly BitSet _CURLY_BEGIN_in_media935;

			// Token: 0x04000A06 RID: 2566
			public static readonly BitSet _ruleset_in_media939;

			// Token: 0x04000A07 RID: 2567
			public static readonly BitSet _page_in_media943;

			// Token: 0x04000A08 RID: 2568
			public static readonly BitSet _CURLY_END_in_media948;

			// Token: 0x04000A09 RID: 2569
			public static readonly BitSet _media_query_in_media_query_list997;

			// Token: 0x04000A0A RID: 2570
			public static readonly BitSet _COMMA_in_media_query_list1000;

			// Token: 0x04000A0B RID: 2571
			public static readonly BitSet _media_query_in_media_query_list1002;

			// Token: 0x04000A0C RID: 2572
			public static readonly BitSet _ONLY_in_media_query1036;

			// Token: 0x04000A0D RID: 2573
			public static readonly BitSet _NOT_in_media_query1040;

			// Token: 0x04000A0E RID: 2574
			public static readonly BitSet _media_type_in_media_query1044;

			// Token: 0x04000A0F RID: 2575
			public static readonly BitSet _AND_in_media_query1047;

			// Token: 0x04000A10 RID: 2576
			public static readonly BitSet _media_expression_in_media_query1049;

			// Token: 0x04000A11 RID: 2577
			public static readonly BitSet _media_expression_in_media_query1087;

			// Token: 0x04000A12 RID: 2578
			public static readonly BitSet _AND_in_media_query1090;

			// Token: 0x04000A13 RID: 2579
			public static readonly BitSet _media_expression_in_media_query1092;

			// Token: 0x04000A14 RID: 2580
			public static readonly BitSet _IDENT_in_media_type1122;

			// Token: 0x04000A15 RID: 2581
			public static readonly BitSet _CIRCLE_BEGIN_in_media_expression1145;

			// Token: 0x04000A16 RID: 2582
			public static readonly BitSet _media_feature_in_media_expression1147;

			// Token: 0x04000A17 RID: 2583
			public static readonly BitSet _COLON_in_media_expression1150;

			// Token: 0x04000A18 RID: 2584
			public static readonly BitSet _expr_in_media_expression1152;

			// Token: 0x04000A19 RID: 2585
			public static readonly BitSet _CIRCLE_END_in_media_expression1156;

			// Token: 0x04000A1A RID: 2586
			public static readonly BitSet _IDENT_in_media_feature1183;

			// Token: 0x04000A1B RID: 2587
			public static readonly BitSet _REPLACEMENTTOKEN_in_media_feature1197;

			// Token: 0x04000A1C RID: 2588
			public static readonly BitSet _PAGE_SYM_in_page1224;

			// Token: 0x04000A1D RID: 2589
			public static readonly BitSet _pseudo_page_in_page1226;

			// Token: 0x04000A1E RID: 2590
			public static readonly BitSet _CURLY_BEGIN_in_page1229;

			// Token: 0x04000A1F RID: 2591
			public static readonly BitSet _declaration_in_page1232;

			// Token: 0x04000A20 RID: 2592
			public static readonly BitSet _SEMICOLON_in_page1234;

			// Token: 0x04000A21 RID: 2593
			public static readonly BitSet _CURLY_END_in_page1239;

			// Token: 0x04000A22 RID: 2594
			public static readonly BitSet _COLON_in_pseudo_page1280;

			// Token: 0x04000A23 RID: 2595
			public static readonly BitSet _IDENT_in_pseudo_page1282;

			// Token: 0x04000A24 RID: 2596
			public static readonly BitSet _set_in_operator1314;

			// Token: 0x04000A25 RID: 2597
			public static readonly BitSet _MINUS_in_unary_operator1349;

			// Token: 0x04000A26 RID: 2598
			public static readonly BitSet _PLUS_in_unary_operator1365;

			// Token: 0x04000A27 RID: 2599
			public static readonly BitSet _STAR_in_property1394;

			// Token: 0x04000A28 RID: 2600
			public static readonly BitSet _IDENT_in_property1398;

			// Token: 0x04000A29 RID: 2601
			public static readonly BitSet _IMPORTANT_COMMENTS_in_property1400;

			// Token: 0x04000A2A RID: 2602
			public static readonly BitSet _REPLACEMENTTOKEN_in_property1424;

			// Token: 0x04000A2B RID: 2603
			public static readonly BitSet _selectors_group_in_ruleset1454;

			// Token: 0x04000A2C RID: 2604
			public static readonly BitSet _CURLY_BEGIN_in_ruleset1460;

			// Token: 0x04000A2D RID: 2605
			public static readonly BitSet _declaration_in_ruleset1467;

			// Token: 0x04000A2E RID: 2606
			public static readonly BitSet _SEMICOLON_in_ruleset1469;

			// Token: 0x04000A2F RID: 2607
			public static readonly BitSet _IMPORTANT_COMMENTS_in_ruleset1475;

			// Token: 0x04000A30 RID: 2608
			public static readonly BitSet _CURLY_END_in_ruleset1482;

			// Token: 0x04000A31 RID: 2609
			public static readonly BitSet _selector_in_selectors_group1523;

			// Token: 0x04000A32 RID: 2610
			public static readonly BitSet _COMMA_in_selectors_group1526;

			// Token: 0x04000A33 RID: 2611
			public static readonly BitSet _selector_in_selectors_group1528;

			// Token: 0x04000A34 RID: 2612
			public static readonly BitSet _simple_selector_sequence_in_selector1559;

			// Token: 0x04000A35 RID: 2613
			public static readonly BitSet _combinator_simple_selector_sequence_in_selector1562;

			// Token: 0x04000A36 RID: 2614
			public static readonly BitSet _combinator_in_combinator_simple_selector_sequence1601;

			// Token: 0x04000A37 RID: 2615
			public static readonly BitSet _simple_selector_sequence_in_combinator_simple_selector_sequence1603;

			// Token: 0x04000A38 RID: 2616
			public static readonly BitSet _PLUS_in_combinator1644;

			// Token: 0x04000A39 RID: 2617
			public static readonly BitSet _GREATER_in_combinator1655;

			// Token: 0x04000A3A RID: 2618
			public static readonly BitSet _TILDE_in_combinator1666;

			// Token: 0x04000A3B RID: 2619
			public static readonly BitSet _whitespace_in_combinator1687;

			// Token: 0x04000A3C RID: 2620
			public static readonly BitSet _WS_in_whitespace1728;

			// Token: 0x04000A3D RID: 2621
			public static readonly BitSet _universal_in_simple_selector_sequence1783;

			// Token: 0x04000A3E RID: 2622
			public static readonly BitSet _type_selector_in_simple_selector_sequence1793;

			// Token: 0x04000A3F RID: 2623
			public static readonly BitSet _whitespace_in_simple_selector_sequence1797;

			// Token: 0x04000A40 RID: 2624
			public static readonly BitSet _hashclassatnameattribpseudonegation_in_simple_selector_sequence1806;

			// Token: 0x04000A41 RID: 2625
			public static readonly BitSet _hashclassatnameattribpseudonegation_in_simple_selector_sequence1848;

			// Token: 0x04000A42 RID: 2626
			public static readonly BitSet _REPLACEMENTTOKEN_in_hashclassatnameattribpseudonegation1878;

			// Token: 0x04000A43 RID: 2627
			public static readonly BitSet _hash_in_hashclassatnameattribpseudonegation1902;

			// Token: 0x04000A44 RID: 2628
			public static readonly BitSet _class_in_hashclassatnameattribpseudonegation1922;

			// Token: 0x04000A45 RID: 2629
			public static readonly BitSet _atname_in_hashclassatnameattribpseudonegation1942;

			// Token: 0x04000A46 RID: 2630
			public static readonly BitSet _attrib_in_hashclassatnameattribpseudonegation1962;

			// Token: 0x04000A47 RID: 2631
			public static readonly BitSet _pseudo_in_hashclassatnameattribpseudonegation1982;

			// Token: 0x04000A48 RID: 2632
			public static readonly BitSet _negation_in_hashclassatnameattribpseudonegation2002;

			// Token: 0x04000A49 RID: 2633
			public static readonly BitSet _selector_namespace_prefix_in_type_selector2047;

			// Token: 0x04000A4A RID: 2634
			public static readonly BitSet _element_name_in_type_selector2051;

			// Token: 0x04000A4B RID: 2635
			public static readonly BitSet _element_name_in_selector_namespace_prefix2085;

			// Token: 0x04000A4C RID: 2636
			public static readonly BitSet _PIPE_in_selector_namespace_prefix2088;

			// Token: 0x04000A4D RID: 2637
			public static readonly BitSet _IDENT_in_element_name2117;

			// Token: 0x04000A4E RID: 2638
			public static readonly BitSet _STAR_in_element_name2137;

			// Token: 0x04000A4F RID: 2639
			public static readonly BitSet _selector_namespace_prefix_in_universal2174;

			// Token: 0x04000A50 RID: 2640
			public static readonly BitSet _STAR_in_universal2178;

			// Token: 0x04000A51 RID: 2641
			public static readonly BitSet _CLASS_IDENT_in_class2207;

			// Token: 0x04000A52 RID: 2642
			public static readonly BitSet _SQUARE_BEGIN_in_attrib2246;

			// Token: 0x04000A53 RID: 2643
			public static readonly BitSet _selector_namespace_prefix_in_attrib2257;

			// Token: 0x04000A54 RID: 2644
			public static readonly BitSet _IDENT_in_attrib2262;

			// Token: 0x04000A55 RID: 2645
			public static readonly BitSet _PREFIXMATCH_in_attrib2289;

			// Token: 0x04000A56 RID: 2646
			public static readonly BitSet _SUFFIXMATCH_in_attrib2293;

			// Token: 0x04000A57 RID: 2647
			public static readonly BitSet _SUBSTRINGMATCH_in_attrib2297;

			// Token: 0x04000A58 RID: 2648
			public static readonly BitSet _EQUALS_in_attrib2301;

			// Token: 0x04000A59 RID: 2649
			public static readonly BitSet _INCLUDES_in_attrib2305;

			// Token: 0x04000A5A RID: 2650
			public static readonly BitSet _DASHMATCH_in_attrib2309;

			// Token: 0x04000A5B RID: 2651
			public static readonly BitSet _IDENT_in_attrib2327;

			// Token: 0x04000A5C RID: 2652
			public static readonly BitSet _STRING_in_attrib2329;

			// Token: 0x04000A5D RID: 2653
			public static readonly BitSet _SQUARE_END_in_attrib2347;

			// Token: 0x04000A5E RID: 2654
			public static readonly BitSet _COLON_in_pseudo2420;

			// Token: 0x04000A5F RID: 2655
			public static readonly BitSet _COLON_in_pseudo2424;

			// Token: 0x04000A60 RID: 2656
			public static readonly BitSet _IDENT_in_pseudo2429;

			// Token: 0x04000A61 RID: 2657
			public static readonly BitSet _COLON_in_pseudo2467;

			// Token: 0x04000A62 RID: 2658
			public static readonly BitSet _COLON_in_pseudo2471;

			// Token: 0x04000A63 RID: 2659
			public static readonly BitSet _functional_pseudo_in_pseudo2474;

			// Token: 0x04000A64 RID: 2660
			public static readonly BitSet _beginfunc_in_functional_pseudo2515;

			// Token: 0x04000A65 RID: 2661
			public static readonly BitSet _selectorexpression_in_functional_pseudo2517;

			// Token: 0x04000A66 RID: 2662
			public static readonly BitSet _CIRCLE_END_in_functional_pseudo2519;

			// Token: 0x04000A67 RID: 2663
			public static readonly BitSet _set_in_selectorexpression2561;

			// Token: 0x04000A68 RID: 2664
			public static readonly BitSet _COLON_in_negation2594;

			// Token: 0x04000A69 RID: 2665
			public static readonly BitSet _NOT_in_negation2596;

			// Token: 0x04000A6A RID: 2666
			public static readonly BitSet _CIRCLE_BEGIN_in_negation2598;

			// Token: 0x04000A6B RID: 2667
			public static readonly BitSet _negation_arg_in_negation2601;

			// Token: 0x04000A6C RID: 2668
			public static readonly BitSet _CIRCLE_END_in_negation2603;

			// Token: 0x04000A6D RID: 2669
			public static readonly BitSet _universal_in_negation_arg2640;

			// Token: 0x04000A6E RID: 2670
			public static readonly BitSet _type_selector_in_negation_arg2643;

			// Token: 0x04000A6F RID: 2671
			public static readonly BitSet _hash_in_negation_arg2645;

			// Token: 0x04000A70 RID: 2672
			public static readonly BitSet _class_in_negation_arg2647;

			// Token: 0x04000A71 RID: 2673
			public static readonly BitSet _attrib_in_negation_arg2649;

			// Token: 0x04000A72 RID: 2674
			public static readonly BitSet _pseudo_in_negation_arg2651;

			// Token: 0x04000A73 RID: 2675
			public static readonly BitSet _AT_NAME_in_atname2666;

			// Token: 0x04000A74 RID: 2676
			public static readonly BitSet _IMPORTANT_COMMENTS_in_declaration2698;

			// Token: 0x04000A75 RID: 2677
			public static readonly BitSet _property_in_declaration2701;

			// Token: 0x04000A76 RID: 2678
			public static readonly BitSet _COLON_in_declaration2703;

			// Token: 0x04000A77 RID: 2679
			public static readonly BitSet _expr_in_declaration2705;

			// Token: 0x04000A78 RID: 2680
			public static readonly BitSet _prio_in_declaration2707;

			// Token: 0x04000A79 RID: 2681
			public static readonly BitSet _STRING_in_stringoruri2747;

			// Token: 0x04000A7A RID: 2682
			public static readonly BitSet _URI_in_stringoruri2767;

			// Token: 0x04000A7B RID: 2683
			public static readonly BitSet _ruleset_in_styleSheetrules2796;

			// Token: 0x04000A7C RID: 2684
			public static readonly BitSet _media_in_styleSheetrules2798;

			// Token: 0x04000A7D RID: 2685
			public static readonly BitSet _page_in_styleSheetrules2800;

			// Token: 0x04000A7E RID: 2686
			public static readonly BitSet _keyframes_in_styleSheetrules2802;

			// Token: 0x04000A7F RID: 2687
			public static readonly BitSet _document_in_styleSheetrules2804;

			// Token: 0x04000A80 RID: 2688
			public static readonly BitSet _wg_dpi_in_styleSheetrules2806;

			// Token: 0x04000A81 RID: 2689
			public static readonly BitSet _IMPORTANT_SYM_in_prio2826;

			// Token: 0x04000A82 RID: 2690
			public static readonly BitSet _IMPORTANT_COMMENTS_in_expr2856;

			// Token: 0x04000A83 RID: 2691
			public static readonly BitSet _term_in_expr2859;

			// Token: 0x04000A84 RID: 2692
			public static readonly BitSet _termwithoperator_in_expr2862;

			// Token: 0x04000A85 RID: 2693
			public static readonly BitSet _operator_in_termwithoperator2902;

			// Token: 0x04000A86 RID: 2694
			public static readonly BitSet _term_in_termwithoperator2905;

			// Token: 0x04000A87 RID: 2695
			public static readonly BitSet _unary_operator_in_term2943;

			// Token: 0x04000A88 RID: 2696
			public static readonly BitSet _NUMBER_in_term2951;

			// Token: 0x04000A89 RID: 2697
			public static readonly BitSet _PERCENTAGE_in_term2959;

			// Token: 0x04000A8A RID: 2698
			public static readonly BitSet _LENGTH_in_term2967;

			// Token: 0x04000A8B RID: 2699
			public static readonly BitSet _RELATIVELENGTH_in_term2975;

			// Token: 0x04000A8C RID: 2700
			public static readonly BitSet _ANGLE_in_term2983;

			// Token: 0x04000A8D RID: 2701
			public static readonly BitSet _TIME_in_term2991;

			// Token: 0x04000A8E RID: 2702
			public static readonly BitSet _FREQ_in_term2999;

			// Token: 0x04000A8F RID: 2703
			public static readonly BitSet _RESOLUTION_in_term3007;

			// Token: 0x04000A90 RID: 2704
			public static readonly BitSet _SPEECH_in_term3015;

			// Token: 0x04000A91 RID: 2705
			public static readonly BitSet _IMPORTANT_COMMENTS_in_term3020;

			// Token: 0x04000A92 RID: 2706
			public static readonly BitSet _URI_in_term3052;

			// Token: 0x04000A93 RID: 2707
			public static readonly BitSet _IMPORTANT_COMMENTS_in_term3054;

			// Token: 0x04000A94 RID: 2708
			public static readonly BitSet _MSIE_EXPRESSION_in_term3088;

			// Token: 0x04000A95 RID: 2709
			public static readonly BitSet _IMPORTANT_COMMENTS_in_term3093;

			// Token: 0x04000A96 RID: 2710
			public static readonly BitSet _IDENT_in_term3122;

			// Token: 0x04000A97 RID: 2711
			public static readonly BitSet _IMPORTANT_COMMENTS_in_term3124;

			// Token: 0x04000A98 RID: 2712
			public static readonly BitSet _STRING_in_term3152;

			// Token: 0x04000A99 RID: 2713
			public static readonly BitSet _IMPORTANT_COMMENTS_in_term3154;

			// Token: 0x04000A9A RID: 2714
			public static readonly BitSet _hash_in_term3182;

			// Token: 0x04000A9B RID: 2715
			public static readonly BitSet _IMPORTANT_COMMENTS_in_term3184;

			// Token: 0x04000A9C RID: 2716
			public static readonly BitSet _REPLACEMENTTOKEN_in_term3209;

			// Token: 0x04000A9D RID: 2717
			public static readonly BitSet _function_in_term3233;

			// Token: 0x04000A9E RID: 2718
			public static readonly BitSet _IMPORTANT_COMMENTS_in_term3235;

			// Token: 0x04000A9F RID: 2719
			public static readonly BitSet _HASH_IDENT_in_hash3268;

			// Token: 0x04000AA0 RID: 2720
			public static readonly BitSet _beginfunc_in_function3300;

			// Token: 0x04000AA1 RID: 2721
			public static readonly BitSet _expr_in_function3302;

			// Token: 0x04000AA2 RID: 2722
			public static readonly BitSet _CIRCLE_END_in_function3305;

			// Token: 0x04000AA3 RID: 2723
			public static readonly BitSet _IDENT_in_beginfunc3337;

			// Token: 0x04000AA4 RID: 2724
			public static readonly BitSet _CIRCLE_BEGIN_in_beginfunc3339;

			// Token: 0x04000AA5 RID: 2725
			public static readonly BitSet _FROM_in_beginfunc3361;

			// Token: 0x04000AA6 RID: 2726
			public static readonly BitSet _CIRCLE_BEGIN_in_beginfunc3363;

			// Token: 0x04000AA7 RID: 2727
			public static readonly BitSet _TO_in_beginfunc3383;

			// Token: 0x04000AA8 RID: 2728
			public static readonly BitSet _CIRCLE_BEGIN_in_beginfunc3385;

			// Token: 0x04000AA9 RID: 2729
			public static readonly BitSet _MSIE_IMAGE_TRANSFORM_in_beginfunc3406;

			// Token: 0x04000AAA RID: 2730
			public static readonly BitSet _CIRCLE_BEGIN_in_beginfunc3408;

			// Token: 0x04000AAB RID: 2731
			public static readonly BitSet _KEYFRAMES_SYM_in_keyframes3438;

			// Token: 0x04000AAC RID: 2732
			public static readonly BitSet _IDENT_in_keyframes3441;

			// Token: 0x04000AAD RID: 2733
			public static readonly BitSet _STRING_in_keyframes3443;

			// Token: 0x04000AAE RID: 2734
			public static readonly BitSet _CURLY_BEGIN_in_keyframes3446;

			// Token: 0x04000AAF RID: 2735
			public static readonly BitSet _keyframes_block_in_keyframes3448;

			// Token: 0x04000AB0 RID: 2736
			public static readonly BitSet _CURLY_END_in_keyframes3451;

			// Token: 0x04000AB1 RID: 2737
			public static readonly BitSet _keyframes_selectors_in_keyframes_block3507;

			// Token: 0x04000AB2 RID: 2738
			public static readonly BitSet _CURLY_BEGIN_in_keyframes_block3509;

			// Token: 0x04000AB3 RID: 2739
			public static readonly BitSet _declaration_in_keyframes_block3512;

			// Token: 0x04000AB4 RID: 2740
			public static readonly BitSet _SEMICOLON_in_keyframes_block3514;

			// Token: 0x04000AB5 RID: 2741
			public static readonly BitSet _CURLY_END_in_keyframes_block3519;

			// Token: 0x04000AB6 RID: 2742
			public static readonly BitSet _keyframes_selector_in_keyframes_selectors3561;

			// Token: 0x04000AB7 RID: 2743
			public static readonly BitSet _COMMA_in_keyframes_selectors3564;

			// Token: 0x04000AB8 RID: 2744
			public static readonly BitSet _keyframes_selector_in_keyframes_selectors3566;

			// Token: 0x04000AB9 RID: 2745
			public static readonly BitSet _set_in_keyframes_selector3596;

			// Token: 0x04000ABA RID: 2746
			public static readonly BitSet _DOCUMENT_SYM_in_document3619;

			// Token: 0x04000ABB RID: 2747
			public static readonly BitSet _S_in_document3621;

			// Token: 0x04000ABC RID: 2748
			public static readonly BitSet _document_match_function_in_document3624;

			// Token: 0x04000ABD RID: 2749
			public static readonly BitSet _S_in_document3626;

			// Token: 0x04000ABE RID: 2750
			public static readonly BitSet _CURLY_BEGIN_in_document3629;

			// Token: 0x04000ABF RID: 2751
			public static readonly BitSet _ruleset_in_document3631;

			// Token: 0x04000AC0 RID: 2752
			public static readonly BitSet _CURLY_END_in_document3634;

			// Token: 0x04000AC1 RID: 2753
			public static readonly BitSet _URI_in_document_match_function3678;

			// Token: 0x04000AC2 RID: 2754
			public static readonly BitSet _URLPREFIX_FUNCTION_in_document_match_function3699;

			// Token: 0x04000AC3 RID: 2755
			public static readonly BitSet _DOMAIN_FUNCTION_in_document_match_function3720;

			// Token: 0x04000AC4 RID: 2756
			public static readonly BitSet _REGEXP_FUNCTION_in_document_match_function3740;

			// Token: 0x04000AC5 RID: 2757
			public static readonly BitSet _WS_in_synpred1_CssParser1723;

			// Token: 0x04000AC6 RID: 2758
			public static readonly BitSet _universal_in_synpred2_CssParser1778;

			// Token: 0x04000AC7 RID: 2759
			public static readonly BitSet _type_selector_in_synpred3_CssParser1788;

			// Token: 0x04000AC8 RID: 2760
			public static readonly BitSet _hashclassatnameattribpseudonegation_in_synpred4_CssParser1801;

			// Token: 0x04000AC9 RID: 2761
			public static readonly BitSet _hashclassatnameattribpseudonegation_in_synpred5_CssParser1843;

			// Token: 0x04000ACA RID: 2762
			public static readonly BitSet _selector_namespace_prefix_in_synpred6_CssParser2042;

			// Token: 0x04000ACB RID: 2763
			public static readonly BitSet _selector_namespace_prefix_in_synpred7_CssParser2169;

			// Token: 0x04000ACC RID: 2764
			public static readonly BitSet _universal_in_synpred8_CssParser2635;
		}
	}
}
