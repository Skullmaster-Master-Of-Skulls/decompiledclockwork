using System;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200019D RID: 413
	public class TransactionRow
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x0003E68A File Offset: 0x0003C88A
		// (set) Token: 0x06000E10 RID: 3600 RVA: 0x0003E692 File Offset: 0x0003C892
		public Guid Id { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x0003E69B File Offset: 0x0003C89B
		// (set) Token: 0x06000E12 RID: 3602 RVA: 0x0003E6A3 File Offset: 0x0003C8A3
		public DateTime CreationTime { get; set; }

		// Token: 0x06000E13 RID: 3603 RVA: 0x0003E6AC File Offset: 0x0003C8AC
		public override bool Equals(object obj)
		{
			TransactionRow transactionRow = obj as TransactionRow;
			return transactionRow != null && this.Id == transactionRow.Id;
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0003E6D8 File Offset: 0x0003C8D8
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}
	}
}
