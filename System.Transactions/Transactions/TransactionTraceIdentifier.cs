using System;

namespace System.Transactions
{
	// Token: 0x02000074 RID: 116
	internal struct TransactionTraceIdentifier
	{
		// Token: 0x0600033D RID: 829 RVA: 0x00036E94 File Offset: 0x00036294
		public TransactionTraceIdentifier(string transactionIdentifier, int cloneIdentifier)
		{
			this.transactionIdentifier = transactionIdentifier;
			this.cloneIdentifier = cloneIdentifier;
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00036EB4 File Offset: 0x000362B4
		public string TransactionIdentifier
		{
			get
			{
				return this.transactionIdentifier;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00036ED4 File Offset: 0x000362D4
		public int CloneIdentifier
		{
			get
			{
				return this.cloneIdentifier;
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00036EF4 File Offset: 0x000362F4
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00036F14 File Offset: 0x00036314
		public override bool Equals(object objectToCompare)
		{
			if (!(objectToCompare is TransactionTraceIdentifier))
			{
				return false;
			}
			TransactionTraceIdentifier transactionTraceIdentifier = (TransactionTraceIdentifier)objectToCompare;
			return !(transactionTraceIdentifier.TransactionIdentifier != this.TransactionIdentifier) && transactionTraceIdentifier.CloneIdentifier == this.CloneIdentifier;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00036F64 File Offset: 0x00036364
		public static bool operator ==(TransactionTraceIdentifier id1, TransactionTraceIdentifier id2)
		{
			return id1.Equals(id2);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00036F84 File Offset: 0x00036384
		public static bool operator !=(TransactionTraceIdentifier id1, TransactionTraceIdentifier id2)
		{
			return !id1.Equals(id2);
		}

		// Token: 0x0400015E RID: 350
		public static readonly TransactionTraceIdentifier Empty = default(TransactionTraceIdentifier);

		// Token: 0x0400015F RID: 351
		private string transactionIdentifier;

		// Token: 0x04000160 RID: 352
		private int cloneIdentifier;
	}
}
