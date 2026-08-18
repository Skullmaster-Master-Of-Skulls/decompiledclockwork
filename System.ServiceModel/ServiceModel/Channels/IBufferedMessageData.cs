using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009BF RID: 2495
	internal interface IBufferedMessageData
	{
		// Token: 0x1700179D RID: 6045
		// (get) Token: 0x06006209 RID: 25097
		MessageEncoder MessageEncoder { get; }

		// Token: 0x1700179E RID: 6046
		// (get) Token: 0x0600620A RID: 25098
		ArraySegment<byte> Buffer { get; }

		// Token: 0x1700179F RID: 6047
		// (get) Token: 0x0600620B RID: 25099
		XmlDictionaryReaderQuotas Quotas { get; }

		// Token: 0x0600620C RID: 25100
		void Close();

		// Token: 0x0600620D RID: 25101
		void EnableMultipleUsers();

		// Token: 0x0600620E RID: 25102
		XmlDictionaryReader GetMessageReader();

		// Token: 0x0600620F RID: 25103
		void Open();

		// Token: 0x06006210 RID: 25104
		void ReturnMessageState(RecycledMessageState messageState);

		// Token: 0x06006211 RID: 25105
		RecycledMessageState TakeMessageState();
	}
}
