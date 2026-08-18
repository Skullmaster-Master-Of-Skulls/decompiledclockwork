using System;
using System.IO;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020003E0 RID: 992
	public interface CmsProcessable
	{
		// Token: 0x0600228B RID: 8843
		Stream Read();

		// Token: 0x0600228C RID: 8844
		void Write(Stream outStream);

		// Token: 0x0600228D RID: 8845
		object GetContent();
	}
}
