using System;
using System.IO;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000834 RID: 2100
	public abstract class StreamUpgradeAcceptor
	{
		// Token: 0x06004E76 RID: 20086
		public abstract bool CanUpgrade(string contentType);

		// Token: 0x06004E77 RID: 20087 RVA: 0x0011E47E File Offset: 0x0011C67E
		public virtual Stream AcceptUpgrade(Stream stream)
		{
			return this.EndAcceptUpgrade(this.BeginAcceptUpgrade(stream, null, null));
		}

		// Token: 0x06004E78 RID: 20088
		public abstract IAsyncResult BeginAcceptUpgrade(Stream stream, AsyncCallback callback, object state);

		// Token: 0x06004E79 RID: 20089
		public abstract Stream EndAcceptUpgrade(IAsyncResult result);
	}
}
