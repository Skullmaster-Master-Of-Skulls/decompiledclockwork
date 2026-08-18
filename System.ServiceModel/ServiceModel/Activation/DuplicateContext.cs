using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.Activation
{
	// Token: 0x020005BD RID: 1469
	[DataContract]
	[KnownType(typeof(TcpDuplicateContext))]
	[KnownType(typeof(NamedPipeDuplicateContext))]
	internal class DuplicateContext
	{
		// Token: 0x06003972 RID: 14706 RVA: 0x000DE5C8 File Offset: 0x000DC7C8
		protected DuplicateContext(Uri via, byte[] readData)
		{
			this.via = via;
			this.readData = readData;
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06003973 RID: 14707 RVA: 0x000DE5DE File Offset: 0x000DC7DE
		public Uri Via
		{
			get
			{
				return this.via;
			}
		}

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x06003974 RID: 14708 RVA: 0x000DE5E6 File Offset: 0x000DC7E6
		public byte[] ReadData
		{
			get
			{
				return this.readData;
			}
		}

		// Token: 0x040029E6 RID: 10726
		[DataMember]
		private Uri via;

		// Token: 0x040029E7 RID: 10727
		[DataMember]
		private byte[] readData;
	}
}
