using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000984 RID: 2436
	[Serializable]
	public class RedirectionLocation
	{
		// Token: 0x06005E49 RID: 24137 RVA: 0x0015D108 File Offset: 0x0015B308
		private RedirectionLocation()
		{
		}

		// Token: 0x06005E4A RID: 24138 RVA: 0x0015D110 File Offset: 0x0015B310
		public RedirectionLocation(Uri address)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			if (!address.IsAbsoluteUri)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("address", SR.GetString("UriMustBeAbsolute"));
			}
			this.Address = address;
		}

		// Token: 0x170016A0 RID: 5792
		// (get) Token: 0x06005E4B RID: 24139 RVA: 0x0015D165 File Offset: 0x0015B365
		// (set) Token: 0x06005E4C RID: 24140 RVA: 0x0015D16D File Offset: 0x0015B36D
		public Uri Address { get; private set; }
	}
}
