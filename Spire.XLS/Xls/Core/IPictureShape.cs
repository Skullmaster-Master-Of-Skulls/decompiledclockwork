using System;
using System.Drawing;

namespace Spire.Xls.Core
{
	// Token: 0x02000200 RID: 512
	public interface IPictureShape : IShape
	{
		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06001CEE RID: 7406
		string FileName { get; }

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06001CEF RID: 7407
		Image Picture { get; }

		// Token: 0x06001CF0 RID: 7408
		void Remove(bool removeImage);
	}
}
