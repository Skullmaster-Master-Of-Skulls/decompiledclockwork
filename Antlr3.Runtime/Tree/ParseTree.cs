using System;
using System.Collections.Generic;
using System.Text;

namespace Antlr.Runtime.Tree
{
	// Token: 0x0200004E RID: 78
	[Serializable]
	public class ParseTree : BaseTree
	{
		// Token: 0x060003AA RID: 938 RVA: 0x0000A1E6 File Offset: 0x000083E6
		public ParseTree(object label)
		{
			this.payload = label;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000A1F5 File Offset: 0x000083F5
		// (set) Token: 0x060003AC RID: 940 RVA: 0x0000A1FD File Offset: 0x000083FD
		public override string Text
		{
			get
			{
				return this.ToString();
			}
			set
			{
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000A1FF File Offset: 0x000083FF
		// (set) Token: 0x060003AE RID: 942 RVA: 0x0000A202 File Offset: 0x00008402
		public override int TokenStartIndex
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000A204 File Offset: 0x00008404
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x0000A207 File Offset: 0x00008407
		public override int TokenStopIndex
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000A209 File Offset: 0x00008409
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x0000A20C File Offset: 0x0000840C
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

		// Token: 0x060003B3 RID: 947 RVA: 0x0000A20E File Offset: 0x0000840E
		public override ITree DupNode()
		{
			return null;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000A214 File Offset: 0x00008414
		public override string ToString()
		{
			if (!(this.payload is IToken))
			{
				return this.payload.ToString();
			}
			IToken token = (IToken)this.payload;
			if (token.Type == -1)
			{
				return "<EOF>";
			}
			return token.Text;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000A25C File Offset: 0x0000845C
		public virtual string ToStringWithHiddenTokens()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.hiddenTokens != null)
			{
				for (int i = 0; i < this.hiddenTokens.Count; i++)
				{
					IToken token = this.hiddenTokens[i];
					stringBuilder.Append(token.Text);
				}
			}
			string text = this.ToString();
			if (!text.Equals("<EOF>"))
			{
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000A2CC File Offset: 0x000084CC
		public virtual string ToInputString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToStringLeaves(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000A2EC File Offset: 0x000084EC
		protected virtual void ToStringLeaves(StringBuilder buf)
		{
			if (this.payload is IToken)
			{
				buf.Append(this.ToStringWithHiddenTokens());
				return;
			}
			int num = 0;
			while (this.Children != null && num < this.Children.Count)
			{
				ParseTree parseTree = (ParseTree)this.Children[num];
				parseTree.ToStringLeaves(buf);
				num++;
			}
		}

		// Token: 0x040000BF RID: 191
		public object payload;

		// Token: 0x040000C0 RID: 192
		public List<IToken> hiddenTokens;
	}
}
