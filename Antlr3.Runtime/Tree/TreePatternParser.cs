using System;

namespace Antlr.Runtime.Tree
{
	// Token: 0x0200005B RID: 91
	public class TreePatternParser
	{
		// Token: 0x0600040E RID: 1038 RVA: 0x0000B068 File Offset: 0x00009268
		public TreePatternParser(TreePatternLexer tokenizer, TreeWizard wizard, ITreeAdaptor adaptor)
		{
			this.tokenizer = tokenizer;
			this.wizard = wizard;
			this.adaptor = adaptor;
			this.ttype = tokenizer.NextToken();
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000B094 File Offset: 0x00009294
		public virtual object Pattern()
		{
			if (this.ttype == 1)
			{
				return this.ParseTree();
			}
			if (this.ttype != 3)
			{
				return null;
			}
			object result = this.ParseNode();
			if (this.ttype == -1)
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000B0D0 File Offset: 0x000092D0
		public virtual object ParseTree()
		{
			if (this.ttype != 1)
			{
				throw new InvalidOperationException("No beginning.");
			}
			this.ttype = this.tokenizer.NextToken();
			object obj = this.ParseNode();
			if (obj == null)
			{
				return null;
			}
			while (this.ttype == 1 || this.ttype == 3 || this.ttype == 5 || this.ttype == 7)
			{
				if (this.ttype == 1)
				{
					object child = this.ParseTree();
					this.adaptor.AddChild(obj, child);
				}
				else
				{
					object obj2 = this.ParseNode();
					if (obj2 == null)
					{
						return null;
					}
					this.adaptor.AddChild(obj, obj2);
				}
			}
			if (this.ttype != 2)
			{
				throw new InvalidOperationException("No end.");
			}
			this.ttype = this.tokenizer.NextToken();
			return obj;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000B190 File Offset: 0x00009390
		public virtual object ParseNode()
		{
			string text = null;
			if (this.ttype == 5)
			{
				this.ttype = this.tokenizer.NextToken();
				if (this.ttype != 3)
				{
					return null;
				}
				text = this.tokenizer.sval.ToString();
				this.ttype = this.tokenizer.NextToken();
				if (this.ttype != 6)
				{
					return null;
				}
				this.ttype = this.tokenizer.NextToken();
			}
			if (this.ttype == 7)
			{
				this.ttype = this.tokenizer.NextToken();
				IToken payload = new CommonToken(0, ".");
				TreeWizard.TreePattern treePattern = new TreeWizard.WildcardTreePattern(payload);
				if (text != null)
				{
					treePattern.label = text;
				}
				return treePattern;
			}
			if (this.ttype != 3)
			{
				return null;
			}
			string text2 = this.tokenizer.sval.ToString();
			this.ttype = this.tokenizer.NextToken();
			if (text2.Equals("nil"))
			{
				return this.adaptor.Nil();
			}
			string text3 = text2;
			string text4 = null;
			if (this.ttype == 4)
			{
				text4 = this.tokenizer.sval.ToString();
				text3 = text4;
				this.ttype = this.tokenizer.NextToken();
			}
			int tokenType = this.wizard.GetTokenType(text2);
			if (tokenType == 0)
			{
				return null;
			}
			object obj = this.adaptor.Create(tokenType, text3);
			if (text != null && obj.GetType() == typeof(TreeWizard.TreePattern))
			{
				((TreeWizard.TreePattern)obj).label = text;
			}
			if (text4 != null && obj.GetType() == typeof(TreeWizard.TreePattern))
			{
				((TreeWizard.TreePattern)obj).hasTextArg = true;
			}
			return obj;
		}

		// Token: 0x040000E9 RID: 233
		protected TreePatternLexer tokenizer;

		// Token: 0x040000EA RID: 234
		protected int ttype;

		// Token: 0x040000EB RID: 235
		protected TreeWizard wizard;

		// Token: 0x040000EC RID: 236
		protected ITreeAdaptor adaptor;
	}
}
