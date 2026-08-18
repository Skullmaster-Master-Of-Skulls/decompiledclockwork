using System;

namespace Antlr.Runtime
{
	// Token: 0x02000002 RID: 2
	public interface IIntStream
	{
		// Token: 0x06000001 RID: 1
		void Consume();

		// Token: 0x06000002 RID: 2
		int LA(int i);

		// Token: 0x06000003 RID: 3
		int Mark();

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4
		int Index { get; }

		// Token: 0x06000005 RID: 5
		void Rewind(int marker);

		// Token: 0x06000006 RID: 6
		void Rewind();

		// Token: 0x06000007 RID: 7
		void Release(int marker);

		// Token: 0x06000008 RID: 8
		void Seek(int index);

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9
		int Count { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10
		string SourceName { get; }
	}
}
