using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007D4 RID: 2004
	internal class ConnectionMessageProperty
	{
		// Token: 0x06004BA8 RID: 19368 RVA: 0x001144B6 File Offset: 0x001126B6
		public ConnectionMessageProperty(IConnection connection)
		{
			this.connection = connection;
		}

		// Token: 0x170012FD RID: 4861
		// (get) Token: 0x06004BA9 RID: 19369 RVA: 0x001144C5 File Offset: 0x001126C5
		public static string Name
		{
			get
			{
				return "iconnection";
			}
		}

		// Token: 0x170012FE RID: 4862
		// (get) Token: 0x06004BAA RID: 19370 RVA: 0x001144CC File Offset: 0x001126CC
		public IConnection Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x04002F53 RID: 12115
		private IConnection connection;
	}
}
