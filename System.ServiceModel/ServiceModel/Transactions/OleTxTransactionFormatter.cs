using System;
using System.Runtime.InteropServices;
using System.ServiceModel.Channels;
using System.Transactions;

namespace System.ServiceModel.Transactions
{
	// Token: 0x020001AE RID: 430
	internal class OleTxTransactionFormatter : TransactionFormatter
	{
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x00032CBA File Offset: 0x00030EBA
		public override MessageHeader EmptyTransactionHeader
		{
			get
			{
				return OleTxTransactionFormatter.emptyTransactionHeader;
			}
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00032CC4 File Offset: 0x00030EC4
		public override void WriteTransaction(Transaction transaction, Message message)
		{
			byte[] transmitterPropagationToken = TransactionInterop.GetTransmitterPropagationToken(transaction);
			WsatExtendedInformation wsatInfo;
			if (!TransactionCache<Transaction, WsatExtendedInformation>.Find(transaction, out wsatInfo))
			{
				uint timeoutFromTransaction = OleTxTransactionFormatter.GetTimeoutFromTransaction(transaction);
				wsatInfo = ((timeoutFromTransaction != 0U) ? new WsatExtendedInformation(null, timeoutFromTransaction) : null);
			}
			OleTxTransactionHeader header = new OleTxTransactionHeader(transmitterPropagationToken, wsatInfo);
			message.Headers.Add(header);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00032D0C File Offset: 0x00030F0C
		public override TransactionInfo ReadTransaction(Message message)
		{
			OleTxTransactionHeader oleTxTransactionHeader = OleTxTransactionHeader.ReadFrom(message);
			if (oleTxTransactionHeader == null)
			{
				return null;
			}
			return new OleTxTransactionInfo(oleTxTransactionHeader);
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00032D2C File Offset: 0x00030F2C
		public static uint GetTimeoutFromTransaction(Transaction transaction)
		{
			IDtcTransaction dtcTransaction = TransactionInterop.GetDtcTransaction(transaction);
			OleTxTransactionFormatter.ITransactionOptions transactionOptions = (OleTxTransactionFormatter.ITransactionOptions)dtcTransaction;
			OleTxTransactionFormatter.XACTOPT xactopt;
			transactionOptions.GetOptions(out xactopt);
			return xactopt.ulTimeout;
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00032D58 File Offset: 0x00030F58
		public static void GetTransactionAttributes(Transaction transaction, out uint timeout, out IsolationFlags isoFlags, out string description)
		{
			IDtcTransaction dtcTransaction = TransactionInterop.GetDtcTransaction(transaction);
			OleTxTransactionFormatter.ITransactionOptions transactionOptions = (OleTxTransactionFormatter.ITransactionOptions)dtcTransaction;
			OleTxTransactionFormatter.ISaneDtcTransaction saneDtcTransaction = (OleTxTransactionFormatter.ISaneDtcTransaction)dtcTransaction;
			OleTxTransactionFormatter.XACTOPT xactopt;
			transactionOptions.GetOptions(out xactopt);
			timeout = xactopt.ulTimeout;
			description = xactopt.szDescription;
			OleTxTransactionFormatter.XACTTRANSINFO xacttransinfo;
			saneDtcTransaction.GetTransactionInfo(out xacttransinfo);
			isoFlags = xacttransinfo.isoFlags;
		}

		// Token: 0x0400173A RID: 5946
		private static OleTxTransactionHeader emptyTransactionHeader = new OleTxTransactionHeader(null, null);

		// Token: 0x02000AF8 RID: 2808
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		private struct XACTOPT
		{
			// Token: 0x04003F57 RID: 16215
			public uint ulTimeout;

			// Token: 0x04003F58 RID: 16216
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
			public string szDescription;
		}

		// Token: 0x02000AF9 RID: 2809
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		private struct XACTTRANSINFO
		{
			// Token: 0x04003F59 RID: 16217
			public Guid uow;

			// Token: 0x04003F5A RID: 16218
			public IsolationLevel isoLevel;

			// Token: 0x04003F5B RID: 16219
			public IsolationFlags isoFlags;

			// Token: 0x04003F5C RID: 16220
			public uint grfTCSupported;

			// Token: 0x04003F5D RID: 16221
			public uint grfRMSupported;

			// Token: 0x04003F5E RID: 16222
			public uint grfTCSupportedRetaining;

			// Token: 0x04003F5F RID: 16223
			public uint grfRMSupportedRetaining;
		}

		// Token: 0x02000AFA RID: 2810
		[Guid("3A6AD9E0-23B9-11cf-AD60-00AA00A74CCD")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		private interface ITransactionOptions
		{
			// Token: 0x06006F33 RID: 28467
			void SetOptions([In] ref OleTxTransactionFormatter.XACTOPT pOptions);

			// Token: 0x06006F34 RID: 28468
			void GetOptions(out OleTxTransactionFormatter.XACTOPT pOptions);
		}

		// Token: 0x02000AFB RID: 2811
		[Guid("0fb15084-af41-11ce-bd2b-204c4f4f5020")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		private interface ISaneDtcTransaction
		{
			// Token: 0x06006F35 RID: 28469
			void Abort(IntPtr reason, int retaining, int async);

			// Token: 0x06006F36 RID: 28470
			void Commit(int retaining, int commitType, int reserved);

			// Token: 0x06006F37 RID: 28471
			void GetTransactionInfo(out OleTxTransactionFormatter.XACTTRANSINFO transactionInformation);
		}
	}
}
