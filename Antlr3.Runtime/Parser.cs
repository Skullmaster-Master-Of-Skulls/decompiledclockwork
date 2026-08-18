using System;
using System.Diagnostics;

namespace Antlr.Runtime
{
	// Token: 0x02000032 RID: 50
	public class Parser : BaseRecognizer
	{
		// Token: 0x06000248 RID: 584 RVA: 0x00006C38 File Offset: 0x00004E38
		public Parser(ITokenStream input)
		{
			this.TokenStream = input;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00006C47 File Offset: 0x00004E47
		public Parser(ITokenStream input, RecognizerSharedState state) : base(state)
		{
			this.input = input;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00006C57 File Offset: 0x00004E57
		public override void Reset()
		{
			base.Reset();
			if (this.input != null)
			{
				this.input.Seek(0);
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00006C73 File Offset: 0x00004E73
		protected override object GetCurrentInputSymbol(IIntStream input)
		{
			return ((ITokenStream)input).LT(1);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00006C84 File Offset: 0x00004E84
		protected override object GetMissingSymbol(IIntStream input, RecognitionException e, int expectedTokenType, BitSet follow)
		{
			string text;
			if (expectedTokenType == -1)
			{
				text = "<missing EOF>";
			}
			else
			{
				text = "<missing " + this.TokenNames[expectedTokenType] + ">";
			}
			CommonToken commonToken = new CommonToken(expectedTokenType, text);
			IToken token = ((ITokenStream)input).LT(1);
			if (token.Type == -1)
			{
				token = ((ITokenStream)input).LT(-1);
			}
			commonToken.Line = token.Line;
			commonToken.CharPositionInLine = token.CharPositionInLine;
			commonToken.Channel = 0;
			commonToken.InputStream = token.InputStream;
			return commonToken;
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00006D0E File Offset: 0x00004F0E
		// (set) Token: 0x0600024E RID: 590 RVA: 0x00006D16 File Offset: 0x00004F16
		public virtual ITokenStream TokenStream
		{
			get
			{
				return this.input;
			}
			set
			{
				this.input = null;
				this.Reset();
				this.input = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00006D2C File Offset: 0x00004F2C
		public override string SourceName
		{
			get
			{
				return this.input.SourceName;
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00006D39 File Offset: 0x00004F39
		[Conditional("ANTLR_TRACE")]
		public virtual void TraceIn(string ruleName, int ruleIndex)
		{
			base.TraceIn(ruleName, ruleIndex, this.input.LT(1));
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00006D4F File Offset: 0x00004F4F
		[Conditional("ANTLR_TRACE")]
		public virtual void TraceOut(string ruleName, int ruleIndex)
		{
			base.TraceOut(ruleName, ruleIndex, this.input.LT(1));
		}

		// Token: 0x04000069 RID: 105
		public ITokenStream input;
	}
}
