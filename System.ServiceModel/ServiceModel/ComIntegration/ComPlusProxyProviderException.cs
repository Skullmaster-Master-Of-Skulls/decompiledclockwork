using System;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000221 RID: 545
	[Serializable]
	internal class ComPlusProxyProviderException : Exception
	{
		// Token: 0x06001087 RID: 4231 RVA: 0x0003D1C5 File Offset: 0x0003B3C5
		public ComPlusProxyProviderException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
