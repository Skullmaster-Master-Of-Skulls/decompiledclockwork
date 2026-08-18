using System;
using System.Transactions;

namespace System.ServiceModel.Transactions
{
	// Token: 0x020001B9 RID: 441
	internal class WsatExtendedInformation
	{
		// Token: 0x06000E71 RID: 3697 RVA: 0x00033D87 File Offset: 0x00031F87
		public WsatExtendedInformation(string identifier, uint timeout)
		{
			this.identifier = identifier;
			this.timeout = timeout;
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x00033D9D File Offset: 0x00031F9D
		public string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x00033DA5 File Offset: 0x00031FA5
		public uint Timeout
		{
			get
			{
				return this.timeout;
			}
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00033DB0 File Offset: 0x00031FB0
		public void TryCache(Transaction tx)
		{
			Guid distributedIdentifier = tx.TransactionInformation.DistributedIdentifier;
			string value = WsatExtendedInformation.IsNativeIdentifier(this.identifier, distributedIdentifier) ? null : this.identifier;
			if (!string.IsNullOrEmpty(value) || this.timeout != 0U)
			{
				WsatExtendedInformationCache.Cache(tx, new WsatExtendedInformation(value, this.timeout));
			}
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x00033E05 File Offset: 0x00032005
		public static string CreateNativeIdentifier(Guid transactionId)
		{
			return "urn:uuid:" + transactionId.ToString("D");
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00033E1D File Offset: 0x0003201D
		public static bool IsNativeIdentifier(string identifier, Guid transactionId)
		{
			return string.Compare(identifier, WsatExtendedInformation.CreateNativeIdentifier(transactionId), StringComparison.Ordinal) == 0;
		}

		// Token: 0x0400175C RID: 5980
		private string identifier;

		// Token: 0x0400175D RID: 5981
		private uint timeout;

		// Token: 0x0400175E RID: 5982
		public const string UuidScheme = "urn:uuid:";
	}
}
