using System;
using Antlr.Runtime.Misc;

namespace Antlr.Runtime.Tree
{
	// Token: 0x0200005C RID: 92
	public class TreeRewriter : TreeParser
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x0000B325 File Offset: 0x00009525
		public TreeRewriter(ITreeNodeStream input) : this(input, new RecognizerSharedState())
		{
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000B344 File Offset: 0x00009544
		public TreeRewriter(ITreeNodeStream input, RecognizerSharedState state) : base(input, state)
		{
			this.originalAdaptor = input.TreeAdaptor;
			this.originalTokenStream = input.TokenStream;
			this.topdown_func = (() => this.Topdown());
			this.bottomup_func = (() => this.Bottomup());
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000B3A4 File Offset: 0x000095A4
		public virtual object ApplyOnce(object t, Func<IAstRuleReturnScope> whichRule)
		{
			if (t == null)
			{
				return null;
			}
			try
			{
				this.SetState(new RecognizerSharedState());
				this.SetTreeNodeStream(new CommonTreeNodeStream(this.originalAdaptor, t));
				((CommonTreeNodeStream)this.input).TokenStream = this.originalTokenStream;
				this.BacktrackingLevel = 1;
				IAstRuleReturnScope astRuleReturnScope = whichRule();
				this.BacktrackingLevel = 0;
				if (this.Failed)
				{
					return t;
				}
				if (this.showTransformations && astRuleReturnScope != null && !t.Equals(astRuleReturnScope.Tree) && astRuleReturnScope.Tree != null)
				{
					this.ReportTransformation(t, astRuleReturnScope.Tree);
				}
				if (astRuleReturnScope != null && astRuleReturnScope.Tree != null)
				{
					return astRuleReturnScope.Tree;
				}
				return t;
			}
			catch (RecognitionException)
			{
			}
			return t;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000B468 File Offset: 0x00009668
		public virtual object ApplyRepeatedly(object t, Func<IAstRuleReturnScope> whichRule)
		{
			bool flag = true;
			while (flag)
			{
				object obj = this.ApplyOnce(t, whichRule);
				flag = !t.Equals(obj);
				t = obj;
			}
			return t;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000B494 File Offset: 0x00009694
		public virtual object Downup(object t)
		{
			return this.Downup(t, false);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000B4BC File Offset: 0x000096BC
		public virtual object Downup(object t, bool showTransformations)
		{
			this.showTransformations = showTransformations;
			TreeVisitor treeVisitor = new TreeVisitor(new CommonTreeAdaptor());
			t = treeVisitor.Visit(t, (object o) => this.ApplyOnce(o, this.topdown_func), (object o) => this.ApplyRepeatedly(o, this.bottomup_func));
			return t;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000B4FD File Offset: 0x000096FD
		protected virtual IAstRuleReturnScope Topdown()
		{
			return null;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000B500 File Offset: 0x00009700
		protected virtual IAstRuleReturnScope Bottomup()
		{
			return null;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000B504 File Offset: 0x00009704
		protected virtual void ReportTransformation(object oldTree, object newTree)
		{
			ITree tree = oldTree as ITree;
			ITree tree2 = newTree as ITree;
			string arg = (tree != null) ? tree.ToStringTree() : "??";
			string arg2 = (tree2 != null) ? tree2.ToStringTree() : "??";
			Console.WriteLine("{0} -> {1}", arg, arg2);
		}

		// Token: 0x040000ED RID: 237
		protected bool showTransformations;

		// Token: 0x040000EE RID: 238
		protected ITokenStream originalTokenStream;

		// Token: 0x040000EF RID: 239
		protected ITreeAdaptor originalAdaptor;

		// Token: 0x040000F0 RID: 240
		private Func<IAstRuleReturnScope> topdown_func;

		// Token: 0x040000F1 RID: 241
		private Func<IAstRuleReturnScope> bottomup_func;
	}
}
