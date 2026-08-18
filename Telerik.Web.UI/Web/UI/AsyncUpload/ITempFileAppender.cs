using System;

namespace Telerik.Web.UI.AsyncUpload
{
	// Token: 0x02000135 RID: 309
	public interface ITempFileAppender
	{
		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000CCB RID: 3275
		long AppendedContentLength { get; }

		// Token: 0x06000CCC RID: 3276
		void AppendTo(string fullPath);
	}
}
