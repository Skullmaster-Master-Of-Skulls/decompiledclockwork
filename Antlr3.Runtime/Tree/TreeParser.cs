using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Antlr.Runtime.Tree
{
	// Token: 0x02000057 RID: 87
	public class TreeParser : BaseRecognizer
	{
		// Token: 0x060003ED RID: 1005 RVA: 0x0000A7CB File Offset: 0x000089CB
		public TreeParser(ITreeNodeStream input)
		{
			this.input = input;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000A7DA File Offset: 0x000089DA
		public TreeParser(ITreeNodeStream input, RecognizerSharedState state) : base(state)
		{
			this.input = input;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000A7EA File Offset: 0x000089EA
		public override void Reset()
		{
			base.Reset();
			if (this.input != null)
			{
				this.input.Seek(0);
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000A806 File Offset: 0x00008A06
		public virtual void SetTreeNodeStream(ITreeNodeStream input)
		{
			this.input = input;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000A80F File Offset: 0x00008A0F
		public virtual ITreeNodeStream GetTreeNodeStream()
		{
			return this.input;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000A817 File Offset: 0x00008A17
		public override string SourceName
		{
			get
			{
				return this.input.SourceName;
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000A824 File Offset: 0x00008A24
		protected override object GetCurrentInputSymbol(IIntStream input)
		{
			return ((ITreeNodeStream)input).LT(1);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000A834 File Offset: 0x00008A34
		protected override object GetMissingSymbol(IIntStream input, RecognitionException e, int expectedTokenType, BitSet follow)
		{
			string text = "<missing " + this.TokenNames[expectedTokenType] + ">";
			ITreeAdaptor treeAdaptor = ((ITreeNodeStream)e.Input).TreeAdaptor;
			return treeAdaptor.Create(new CommonToken(expectedTokenType, text));
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000A878 File Offset: 0x00008A78
		public override void MatchAny(IIntStream ignore)
		{
			this.state.errorRecovery = false;
			this.state.failed = false;
			this.input.Consume();
			int num = this.input.LA(1);
			if (num == 2)
			{
				this.input.Consume();
				int i = 1;
				while (i > 0)
				{
					switch (this.input.LA(1))
					{
					case -1:
						return;
					case 2:
						i++;
						break;
					case 3:
						i--;
						break;
					}
					this.input.Consume();
				}
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000A90F File Offset: 0x00008B0F
		protected override object RecoverFromMismatchedToken(IIntStream input, int ttype, BitSet follow)
		{
			throw new MismatchedTreeNodeException(ttype, (ITreeNodeStream)input);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000A920 File Offset: 0x00008B20
		public override string GetErrorHeader(RecognitionException e)
		{
			return string.Concat(new object[]
			{
				this.GrammarFileName,
				": node from ",
				e.ApproximateLineInfo ? "after " : "",
				"line ",
				e.Line,
				":",
				e.CharPositionInLine
			});
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000A990 File Offset: 0x00008B90
		public override string GetErrorMessage(RecognitionException e, string[] tokenNames)
		{
			if (this != null)
			{
				ITreeAdaptor treeAdaptor = ((ITreeNodeStream)e.Input).TreeAdaptor;
				e.Token = treeAdaptor.GetToken(e.Node);
				if (e.Token == null)
				{
					e.Token = new CommonToken(treeAdaptor.GetType(e.Node), treeAdaptor.GetText(e.Node));
				}
			}
			return base.GetErrorMessage(e, tokenNames);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000A9F6 File Offset: 0x00008BF6
		[Conditional("ANTLR_TRACE")]
		public virtual void TraceIn(string ruleName, int ruleIndex)
		{
			base.TraceIn(ruleName, ruleIndex, this.input.LT(1));
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000AA0C File Offset: 0x00008C0C
		[Conditional("ANTLR_TRACE")]
		public virtual void TraceOut(string ruleName, int ruleIndex)
		{
			base.TraceOut(ruleName, ruleIndex, this.input.LT(1));
		}

		// Token: 0x040000C9 RID: 201
		public const int DOWN = 2;

		// Token: 0x040000CA RID: 202
		public const int UP = 3;

		// Token: 0x040000CB RID: 203
		private static string dotdot = ".*[^.]\\.\\.[^.].*";

		// Token: 0x040000CC RID: 204
		private static string doubleEtc = ".*\\.\\.\\.\\s+\\.\\.\\..*";

		// Token: 0x040000CD RID: 205
		private static Regex dotdotPattern = new Regex(TreeParser.dotdot, RegexOptions.Compiled);

		// Token: 0x040000CE RID: 206
		private static Regex doubleEtcPattern = new Regex(TreeParser.doubleEtc, RegexOptions.Compiled);

		// Token: 0x040000CF RID: 207
		protected ITreeNodeStream input;
	}
}
