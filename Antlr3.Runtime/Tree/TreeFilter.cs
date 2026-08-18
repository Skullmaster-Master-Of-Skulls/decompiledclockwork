using System;
using Antlr.Runtime.Misc;

namespace Antlr.Runtime.Tree
{
	// Token: 0x02000058 RID: 88
	public class TreeFilter : TreeParser
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x0000AA58 File Offset: 0x00008C58
		public TreeFilter(ITreeNodeStream input) : this(input, new RecognizerSharedState())
		{
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000AA66 File Offset: 0x00008C66
		public TreeFilter(ITreeNodeStream input, RecognizerSharedState state) : base(input, state)
		{
			this.originalAdaptor = input.TreeAdaptor;
			this.originalTokenStream = input.TokenStream;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000AA88 File Offset: 0x00008C88
		public virtual void ApplyOnce(object t, Action whichRule)
		{
			if (t == null)
			{
				return;
			}
			try
			{
				this.SetState(new RecognizerSharedState());
				this.SetTreeNodeStream(new CommonTreeNodeStream(this.originalAdaptor, t));
				((CommonTreeNodeStream)this.input).TokenStream = this.originalTokenStream;
				this.BacktrackingLevel = 1;
				whichRule();
				this.BacktrackingLevel = 0;
			}
			catch (RecognitionException)
			{
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000AB28 File Offset: 0x00008D28
		public virtual void Downup(object t)
		{
			TreeVisitor treeVisitor = new TreeVisitor(new CommonTreeAdaptor());
			Func<object, object> preAction = delegate(object o)
			{
				this.ApplyOnce(o, new Action(this.Topdown));
				return o;
			};
			Func<object, object> postAction = delegate(object o)
			{
				this.ApplyOnce(o, new Action(this.Bottomup));
				return o;
			};
			treeVisitor.Visit(t, preAction, postAction);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000AB64 File Offset: 0x00008D64
		protected virtual void Topdown()
		{
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000AB66 File Offset: 0x00008D66
		protected virtual void Bottomup()
		{
		}

		// Token: 0x040000D0 RID: 208
		protected ITokenStream originalTokenStream;

		// Token: 0x040000D1 RID: 209
		protected ITreeAdaptor originalAdaptor;
	}
}
