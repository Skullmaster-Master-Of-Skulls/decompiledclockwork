using System;

namespace Antlr.Runtime.Tree
{
	// Token: 0x0200004C RID: 76
	public interface ITreeVisitorAction
	{
		// Token: 0x060003A5 RID: 933
		object Pre(object t);

		// Token: 0x060003A6 RID: 934
		object Post(object t);
	}
}
