using System;
using System.IO;

namespace System.Web
{
	// Token: 0x02000005 RID: 5
	internal abstract class HttpResponseInternalBase : HttpResponseBase
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public virtual TextWriter SwitchWriter(TextWriter writer)
		{
			throw new NotImplementedException();
		}
	}
}
