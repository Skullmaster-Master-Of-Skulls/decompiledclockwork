using System;
using System.Collections.Specialized;
using System.IO;

namespace System.Net.Mime
{
	// Token: 0x02000681 RID: 1665
	internal abstract class BaseWriter
	{
		// Token: 0x06003391 RID: 13201
		internal abstract IAsyncResult BeginGetContentStream(AsyncCallback callback, object state);

		// Token: 0x06003392 RID: 13202
		internal abstract Stream EndGetContentStream(IAsyncResult result);

		// Token: 0x06003393 RID: 13203
		internal abstract Stream GetContentStream();

		// Token: 0x06003394 RID: 13204
		internal abstract void WriteHeader(string name, string value);

		// Token: 0x06003395 RID: 13205
		internal abstract void WriteHeaders(NameValueCollection headers);

		// Token: 0x06003396 RID: 13206
		internal abstract void Close();
	}
}
