using System;
using Antlr.Runtime.Misc;

namespace Antlr.Runtime.Tree
{
	// Token: 0x0200005D RID: 93
	public class TreeVisitor
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x0000B54D File Offset: 0x0000974D
		public TreeVisitor(ITreeAdaptor adaptor)
		{
			this.adaptor = adaptor;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000B55C File Offset: 0x0000975C
		public TreeVisitor() : this(new CommonTreeAdaptor())
		{
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000B56C File Offset: 0x0000976C
		public object Visit(object t, ITreeVisitorAction action)
		{
			bool flag = this.adaptor.IsNil(t);
			if (action != null && !flag)
			{
				t = action.Pre(t);
			}
			for (int i = 0; i < this.adaptor.GetChildCount(t); i++)
			{
				object child = this.adaptor.GetChild(t, i);
				this.Visit(child, action);
			}
			if (action != null && !flag)
			{
				t = action.Post(t);
			}
			return t;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000B5D3 File Offset: 0x000097D3
		public object Visit(object t, Func<object, object> preAction, Func<object, object> postAction)
		{
			return this.Visit(t, new TreeVisitorAction(preAction, postAction));
		}

		// Token: 0x040000F2 RID: 242
		protected ITreeAdaptor adaptor;
	}
}
