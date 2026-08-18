using System;
using System.Reflection;
using System.Transactions;

namespace System.Data.SqlClient
{
	// Token: 0x02000205 RID: 517
	internal static class SysTxForGlobalTransactions
	{
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060020F6 RID: 8438 RVA: 0x000DE440 File Offset: 0x000DD840
		public static MethodInfo EnlistPromotableSinglePhase
		{
			get
			{
				return SysTxForGlobalTransactions._enlistPromotableSinglePhase.Value;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060020F7 RID: 8439 RVA: 0x000DE458 File Offset: 0x000DD858
		public static MethodInfo SetDistributedTransactionIdentifier
		{
			get
			{
				return SysTxForGlobalTransactions._setDistributedTransactionIdentifier.Value;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060020F8 RID: 8440 RVA: 0x000DE470 File Offset: 0x000DD870
		public static MethodInfo GetPromotedToken
		{
			get
			{
				return SysTxForGlobalTransactions._getPromotedToken.Value;
			}
		}

		// Token: 0x040011F9 RID: 4601
		private static readonly Lazy<MethodInfo> _enlistPromotableSinglePhase = new Lazy<MethodInfo>(() => typeof(Transaction).GetMethod("EnlistPromotableSinglePhase", new Type[]
		{
			typeof(IPromotableSinglePhaseNotification),
			typeof(Guid)
		}));

		// Token: 0x040011FA RID: 4602
		private static readonly Lazy<MethodInfo> _setDistributedTransactionIdentifier = new Lazy<MethodInfo>(() => typeof(Transaction).GetMethod("SetDistributedTransactionIdentifier", new Type[]
		{
			typeof(IPromotableSinglePhaseNotification),
			typeof(Guid)
		}));

		// Token: 0x040011FB RID: 4603
		private static readonly Lazy<MethodInfo> _getPromotedToken = new Lazy<MethodInfo>(() => typeof(Transaction).GetMethod("GetPromotedToken"));
	}
}
