using System;
using System.IO;
using System.Runtime.Serialization;

namespace System.ServiceModel.Activation
{
	// Token: 0x020005C8 RID: 1480
	[DataContract]
	internal class ListenerChannelContext
	{
		// Token: 0x0600399D RID: 14749 RVA: 0x000DEAAC File Offset: 0x000DCCAC
		internal ListenerChannelContext(string appKey, int listenerChannelId, Guid token)
		{
			this.appKey = appKey;
			this.listenerChannelId = listenerChannelId;
			this.token = token;
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x0600399E RID: 14750 RVA: 0x000DEAC9 File Offset: 0x000DCCC9
		internal string AppKey
		{
			get
			{
				return this.appKey;
			}
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x0600399F RID: 14751 RVA: 0x000DEAD1 File Offset: 0x000DCCD1
		internal int ListenerChannelId
		{
			get
			{
				return this.listenerChannelId;
			}
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x060039A0 RID: 14752 RVA: 0x000DEAD9 File Offset: 0x000DCCD9
		internal Guid Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x000DEAE4 File Offset: 0x000DCCE4
		public static ListenerChannelContext Hydrate(byte[] blob)
		{
			ListenerChannelContext result;
			using (MemoryStream memoryStream = new MemoryStream(blob))
			{
				DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(ListenerChannelContext));
				result = (ListenerChannelContext)dataContractSerializer.ReadObject(memoryStream);
			}
			return result;
		}

		// Token: 0x060039A2 RID: 14754 RVA: 0x000DEB34 File Offset: 0x000DCD34
		public byte[] Dehydrate()
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(ListenerChannelContext));
				dataContractSerializer.WriteObject(memoryStream, this);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x040029F5 RID: 10741
		[DataMember]
		private string appKey;

		// Token: 0x040029F6 RID: 10742
		[DataMember]
		private int listenerChannelId;

		// Token: 0x040029F7 RID: 10743
		[DataMember]
		private Guid token;
	}
}
