using System;

namespace System.ServiceModel
{
	// Token: 0x02000143 RID: 323
	internal static class NetHttpMessageEncodingHelper
	{
		// Token: 0x060008F6 RID: 2294 RVA: 0x000240FD File Offset: 0x000222FD
		internal static bool IsDefined(NetHttpMessageEncoding value)
		{
			return value == NetHttpMessageEncoding.Binary || value == NetHttpMessageEncoding.Text || value == NetHttpMessageEncoding.Mtom;
		}
	}
}
