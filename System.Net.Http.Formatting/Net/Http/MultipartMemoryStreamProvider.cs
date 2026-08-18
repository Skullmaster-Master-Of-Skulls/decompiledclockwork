using System;
using System.IO;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000071 RID: 113
	public class MultipartMemoryStreamProvider : MultipartStreamProvider
	{
		// Token: 0x060003BF RID: 959 RVA: 0x0000FAE6 File Offset: 0x0000DCE6
		public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
		{
			if (parent == null)
			{
				throw Error.ArgumentNull("parent");
			}
			if (headers == null)
			{
				throw Error.ArgumentNull("headers");
			}
			return new MemoryStream();
		}
	}
}
