using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;

namespace System.ServiceModel.Activation
{
	// Token: 0x020005C2 RID: 1474
	[DataContract]
	[KnownType(typeof(IPEndPoint))]
	internal class TcpDuplicateContext : DuplicateContext
	{
		// Token: 0x06003982 RID: 14722 RVA: 0x000DE6D9 File Offset: 0x000DC8D9
		public TcpDuplicateContext(SocketInformation socketInformation, Uri via, byte[] readData) : base(via, readData)
		{
			this.socketInformation = socketInformation;
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x06003983 RID: 14723 RVA: 0x000DE6EA File Offset: 0x000DC8EA
		public SocketInformation SocketInformation
		{
			get
			{
				return this.socketInformation;
			}
		}

		// Token: 0x040029EC RID: 10732
		[DataMember]
		private SocketInformation socketInformation;
	}
}
