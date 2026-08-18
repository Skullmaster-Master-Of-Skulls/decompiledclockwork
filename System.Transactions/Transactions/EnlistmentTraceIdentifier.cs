using System;

namespace System.Transactions
{
	// Token: 0x02000042 RID: 66
	internal struct EnlistmentTraceIdentifier
	{
		// Token: 0x060001EE RID: 494 RVA: 0x00030DD4 File Offset: 0x000301D4
		public EnlistmentTraceIdentifier(Guid resourceManagerIdentifier, TransactionTraceIdentifier transactionTraceId, int enlistmentIdentifier)
		{
			this.resourceManagerIdentifier = resourceManagerIdentifier;
			this.transactionTraceIdentifier = transactionTraceId;
			this.enlistmentIdentifier = enlistmentIdentifier;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00030E04 File Offset: 0x00030204
		public Guid ResourceManagerIdentifier
		{
			get
			{
				return this.resourceManagerIdentifier;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00030E24 File Offset: 0x00030224
		public TransactionTraceIdentifier TransactionTraceId
		{
			get
			{
				return this.transactionTraceIdentifier;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00030E44 File Offset: 0x00030244
		public int EnlistmentIdentifier
		{
			get
			{
				return this.enlistmentIdentifier;
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00030E64 File Offset: 0x00030264
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00030E84 File Offset: 0x00030284
		public override bool Equals(object objectToCompare)
		{
			if (!(objectToCompare is EnlistmentTraceIdentifier))
			{
				return false;
			}
			EnlistmentTraceIdentifier enlistmentTraceIdentifier = (EnlistmentTraceIdentifier)objectToCompare;
			return !(enlistmentTraceIdentifier.ResourceManagerIdentifier != this.ResourceManagerIdentifier) && !(enlistmentTraceIdentifier.TransactionTraceId != this.TransactionTraceId) && enlistmentTraceIdentifier.EnlistmentIdentifier == this.EnlistmentIdentifier;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00030EE4 File Offset: 0x000302E4
		public static bool operator ==(EnlistmentTraceIdentifier id1, EnlistmentTraceIdentifier id2)
		{
			return id1.Equals(id2);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00030F04 File Offset: 0x00030304
		public static bool operator !=(EnlistmentTraceIdentifier id1, EnlistmentTraceIdentifier id2)
		{
			return !id1.Equals(id2);
		}

		// Token: 0x040000F2 RID: 242
		public static readonly EnlistmentTraceIdentifier Empty = default(EnlistmentTraceIdentifier);

		// Token: 0x040000F3 RID: 243
		private Guid resourceManagerIdentifier;

		// Token: 0x040000F4 RID: 244
		private TransactionTraceIdentifier transactionTraceIdentifier;

		// Token: 0x040000F5 RID: 245
		private int enlistmentIdentifier;
	}
}
