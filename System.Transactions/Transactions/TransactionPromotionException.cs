using System;
using System.Runtime.Serialization;

namespace System.Transactions
{
	// Token: 0x02000064 RID: 100
	[Serializable]
	public class TransactionPromotionException : TransactionException
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x00033654 File Offset: 0x00032A54
		public TransactionPromotionException() : this(SR.GetString("PromotionFailed"))
		{
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00033674 File Offset: 0x00032A74
		public TransactionPromotionException(string message) : base(message)
		{
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00033694 File Offset: 0x00032A94
		public TransactionPromotionException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000336B4 File Offset: 0x00032AB4
		protected TransactionPromotionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
