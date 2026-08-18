using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200088C RID: 2188
	public sealed class HttpMessageSettings : IEquatable<HttpMessageSettings>
	{
		// Token: 0x17001481 RID: 5249
		// (get) Token: 0x0600531E RID: 21278 RVA: 0x001324AC File Offset: 0x001306AC
		// (set) Token: 0x0600531F RID: 21279 RVA: 0x001324B4 File Offset: 0x001306B4
		public bool HttpMessagesSupported { get; set; }

		// Token: 0x06005320 RID: 21280 RVA: 0x001324BD File Offset: 0x001306BD
		public bool Equals(HttpMessageSettings other)
		{
			return other != null && other.HttpMessagesSupported == this.HttpMessagesSupported;
		}
	}
}
