using System;

namespace Antlr.Runtime.Tree
{
	// Token: 0x02000043 RID: 67
	public interface ITreeNodeStream : IIntStream
	{
		// Token: 0x1700009A RID: 154
		object this[int i]
		{
			get;
		}

		// Token: 0x0600032C RID: 812
		object LT(int k);

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600032D RID: 813
		object TreeSource { get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600032E RID: 814
		ITokenStream TokenStream { get; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600032F RID: 815
		ITreeAdaptor TreeAdaptor { get; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000330 RID: 816
		// (set) Token: 0x06000331 RID: 817
		bool UniqueNavigationNodes { get; set; }

		// Token: 0x06000332 RID: 818
		string ToString(object start, object stop);

		// Token: 0x06000333 RID: 819
		void ReplaceChildren(object parent, int startChildIndex, int stopChildIndex, object t);
	}
}
