using System;
using Antlr.Runtime.Misc;

namespace Antlr.Runtime.Tree
{
	// Token: 0x0200004D RID: 77
	public class TreeVisitorAction : ITreeVisitorAction
	{
		// Token: 0x060003A7 RID: 935 RVA: 0x0000A1A0 File Offset: 0x000083A0
		public TreeVisitorAction(Func<object, object> preAction, Func<object, object> postAction)
		{
			this._preAction = preAction;
			this._postAction = postAction;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000A1B6 File Offset: 0x000083B6
		public object Pre(object t)
		{
			if (this._preAction != null)
			{
				return this._preAction(t);
			}
			return t;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000A1CE File Offset: 0x000083CE
		public object Post(object t)
		{
			if (this._postAction != null)
			{
				return this._postAction(t);
			}
			return t;
		}

		// Token: 0x040000BD RID: 189
		private readonly Func<object, object> _preAction;

		// Token: 0x040000BE RID: 190
		private readonly Func<object, object> _postAction;
	}
}
