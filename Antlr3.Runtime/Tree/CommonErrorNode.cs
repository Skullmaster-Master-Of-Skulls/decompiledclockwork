using System;

namespace Antlr.Runtime.Tree
{
	// Token: 0x02000047 RID: 71
	[Serializable]
	public class CommonErrorNode : CommonTree
	{
		// Token: 0x06000379 RID: 889 RVA: 0x00009338 File Offset: 0x00007538
		public CommonErrorNode(ITokenStream input, IToken start, IToken stop, RecognitionException e)
		{
			if (stop == null || (stop.TokenIndex < start.TokenIndex && stop.Type != -1))
			{
				stop = start;
			}
			this.input = input;
			this.start = start;
			this.stop = stop;
			this.trappedException = e;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00009385 File Offset: 0x00007585
		public override bool IsNil
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00009388 File Offset: 0x00007588
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000941E File Offset: 0x0000761E
		public override string Text
		{
			get
			{
				string result;
				if (this.start != null)
				{
					int tokenIndex = this.start.TokenIndex;
					int num = this.stop.TokenIndex;
					if (this.stop.Type == -1)
					{
						num = ((ITokenStream)this.input).Count;
					}
					result = ((ITokenStream)this.input).ToString(tokenIndex, num);
				}
				else if (this.start is ITree)
				{
					result = ((ITreeNodeStream)this.input).ToString(this.start, this.stop);
				}
				else
				{
					result = "<unknown>";
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00009420 File Offset: 0x00007620
		// (set) Token: 0x0600037E RID: 894 RVA: 0x00009423 File Offset: 0x00007623
		public override int Type
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00009428 File Offset: 0x00007628
		public override string ToString()
		{
			if (this.trappedException is MissingTokenException)
			{
				return "<missing type: " + ((MissingTokenException)this.trappedException).MissingType + ">";
			}
			if (this.trappedException is UnwantedTokenException)
			{
				return string.Concat(new object[]
				{
					"<extraneous: ",
					((UnwantedTokenException)this.trappedException).UnexpectedToken,
					", resync=",
					this.Text,
					">"
				});
			}
			if (this.trappedException is MismatchedTokenException)
			{
				return string.Concat(new object[]
				{
					"<mismatched token: ",
					this.trappedException.Token,
					", resync=",
					this.Text,
					">"
				});
			}
			if (this.trappedException is NoViableAltException)
			{
				return string.Concat(new object[]
				{
					"<unexpected: ",
					this.trappedException.Token,
					", resync=",
					this.Text,
					">"
				});
			}
			return "<error: " + this.Text + ">";
		}

		// Token: 0x040000A9 RID: 169
		public IIntStream input;

		// Token: 0x040000AA RID: 170
		public IToken start;

		// Token: 0x040000AB RID: 171
		public IToken stop;

		// Token: 0x040000AC RID: 172
		public RecognitionException trappedException;
	}
}
