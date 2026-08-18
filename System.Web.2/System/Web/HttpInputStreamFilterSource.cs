using System;

namespace System.Web
{
	// Token: 0x020000A3 RID: 163
	internal class HttpInputStreamFilterSource : HttpInputStream
	{
		// Token: 0x06000A50 RID: 2640 RVA: 0x00017C83 File Offset: 0x00015E83
		internal HttpInputStreamFilterSource() : base(null, 0, 0)
		{
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00017C8E File Offset: 0x00015E8E
		internal void SetContent(HttpRawUploadedContent data)
		{
			if (data != null)
			{
				base.Init(data, 0, data.Length);
				return;
			}
			base.Uninit();
		}
	}
}
