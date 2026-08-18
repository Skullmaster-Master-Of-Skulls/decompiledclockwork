using System;
using System.ServiceModel.Description;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A60 RID: 2656
	internal interface ITransactionChannel
	{
		// Token: 0x060068ED RID: 26861
		void WriteTransactionDataToMessage(Message message, MessageDirection direction);

		// Token: 0x060068EE RID: 26862
		void ReadTransactionDataFromMessage(Message message, MessageDirection direction);

		// Token: 0x060068EF RID: 26863
		void ReadIssuedTokens(Message message, MessageDirection direction);

		// Token: 0x060068F0 RID: 26864
		void WriteIssuedTokens(Message message, MessageDirection direction);
	}
}
