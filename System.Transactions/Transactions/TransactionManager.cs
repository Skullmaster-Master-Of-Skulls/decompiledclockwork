using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Threading;
using System.Transactions.Configuration;
using System.Transactions.Diagnostics;
using System.Transactions.Oletx;

namespace System.Transactions
{
	// Token: 0x02000069 RID: 105
	public static class TransactionManager
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060002DE RID: 734 RVA: 0x00034194 File Offset: 0x00033594
		// (remove) Token: 0x060002DF RID: 735 RVA: 0x00034204 File Offset: 0x00033604
		public static event TransactionStartedEventHandler DistributedTransactionStarted
		{
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			add
			{
				if (!TransactionManager._platformValidated)
				{
					TransactionManager.ValidatePlatform();
				}
				lock (TransactionManager.ClassSyncObject)
				{
					TransactionManager.distributedTransactionStartedDelegate = (TransactionStartedEventHandler)Delegate.Combine(TransactionManager.distributedTransactionStartedDelegate, value);
					if (value != null)
					{
						TransactionManager.ProcessExistingTransactions(value);
					}
				}
			}
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			remove
			{
				if (!TransactionManager._platformValidated)
				{
					TransactionManager.ValidatePlatform();
				}
				lock (TransactionManager.ClassSyncObject)
				{
					TransactionManager.distributedTransactionStartedDelegate = (TransactionStartedEventHandler)Delegate.Remove(TransactionManager.distributedTransactionStartedDelegate, value);
				}
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00034264 File Offset: 0x00033664
		internal static void ProcessExistingTransactions(TransactionStartedEventHandler eventHandler)
		{
			lock (TransactionManager.PromotedTransactionTable)
			{
				foreach (object obj2 in TransactionManager.PromotedTransactionTable)
				{
					WeakReference weakReference = (WeakReference)((DictionaryEntry)obj2).Value;
					Transaction transaction = (Transaction)weakReference.Target;
					if (transaction != null)
					{
						TransactionEventArgs transactionEventArgs = new TransactionEventArgs();
						transactionEventArgs.transaction = transaction.InternalClone();
						eventHandler(transactionEventArgs.transaction, transactionEventArgs);
					}
				}
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00034334 File Offset: 0x00033734
		internal static void FireDistributedTransactionStarted(Transaction transaction)
		{
			TransactionStartedEventHandler transactionStartedEventHandler = null;
			lock (TransactionManager.ClassSyncObject)
			{
				transactionStartedEventHandler = TransactionManager.distributedTransactionStartedDelegate;
			}
			if (transactionStartedEventHandler != null)
			{
				TransactionEventArgs transactionEventArgs = new TransactionEventArgs();
				transactionEventArgs.transaction = transaction.InternalClone();
				transactionStartedEventHandler(transactionEventArgs.transaction, transactionEventArgs);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x000343A4 File Offset: 0x000337A4
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x000343C4 File Offset: 0x000337C4
		public static HostCurrentTransactionCallback HostCurrentCallback
		{
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			get
			{
				if (!TransactionManager._platformValidated)
				{
					TransactionManager.ValidatePlatform();
				}
				return TransactionManager.currentDelegate;
			}
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			set
			{
				if (!TransactionManager._platformValidated)
				{
					TransactionManager.ValidatePlatform();
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				lock (TransactionManager.ClassSyncObject)
				{
					if (TransactionManager.currentDelegateSet)
					{
						throw new InvalidOperationException(SR.GetString("CurrentDelegateSet"));
					}
					TransactionManager.currentDelegateSet = true;
				}
				TransactionManager.currentDelegate = value;
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00034444 File Offset: 0x00033844
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static Enlistment Reenlist(Guid resourceManagerIdentifier, byte[] recoveryInformation, IEnlistmentNotification enlistmentNotification)
		{
			if (resourceManagerIdentifier == Guid.Empty)
			{
				throw new ArgumentException(SR.GetString("BadResourceManagerId"), "resourceManagerIdentifier");
			}
			if (recoveryInformation == null)
			{
				throw new ArgumentNullException("recoveryInformation");
			}
			if (enlistmentNotification == null)
			{
				throw new ArgumentNullException("enlistmentNotification");
			}
			if (!TransactionManager._platformValidated)
			{
				TransactionManager.ValidatePlatform();
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionManager.Reenlist");
			}
			if (DiagnosticTrace.Information)
			{
				ReenlistTraceRecord.Trace(SR.GetString("TraceSourceBase"), resourceManagerIdentifier);
			}
			MemoryStream memoryStream = new MemoryStream(recoveryInformation);
			string nodeName = null;
			byte[] recoveryInformation2 = null;
			try
			{
				BinaryReader binaryReader = new BinaryReader(memoryStream);
				int num = binaryReader.ReadInt32();
				if (num != 1)
				{
					if (DiagnosticTrace.Error)
					{
						TransactionExceptionTraceRecord.Trace(SR.GetString("TraceSourceBase"), SR.GetString("UnrecognizedRecoveryInformation"));
					}
					throw new ArgumentException(SR.GetString("UnrecognizedRecoveryInformation"), "recoveryInformation");
				}
				nodeName = binaryReader.ReadString();
				recoveryInformation2 = binaryReader.ReadBytes(recoveryInformation.Length - checked((int)memoryStream.Position));
			}
			catch (EndOfStreamException innerException)
			{
				if (DiagnosticTrace.Error)
				{
					TransactionExceptionTraceRecord.Trace(SR.GetString("TraceSourceBase"), SR.GetString("UnrecognizedRecoveryInformation"));
				}
				throw new ArgumentException(SR.GetString("UnrecognizedRecoveryInformation"), "recoveryInformation", innerException);
			}
			catch (FormatException innerException2)
			{
				if (DiagnosticTrace.Error)
				{
					TransactionExceptionTraceRecord.Trace(SR.GetString("TraceSourceBase"), SR.GetString("UnrecognizedRecoveryInformation"));
				}
				throw new ArgumentException(SR.GetString("UnrecognizedRecoveryInformation"), "recoveryInformation", innerException2);
			}
			finally
			{
				memoryStream.Close();
			}
			OletxTransactionManager oletxTransactionManager = TransactionManager.CheckTransactionManager(nodeName);
			object syncRoot = new object();
			Enlistment enlistment = new Enlistment(enlistmentNotification, syncRoot);
			EnlistmentState._EnlistmentStatePromoted.EnterState(enlistment.InternalEnlistment);
			enlistment.InternalEnlistment.PromotedEnlistment = oletxTransactionManager.ReenlistTransaction(resourceManagerIdentifier, recoveryInformation2, (RecoveringInternalEnlistment)enlistment.InternalEnlistment);
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionManager.Reenlist");
			}
			return enlistment;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00034684 File Offset: 0x00033A84
		private static OletxTransactionManager CheckTransactionManager(string nodeName)
		{
			OletxTransactionManager oletxTransactionManager = TransactionManager.DistributedTransactionManager;
			if ((oletxTransactionManager.NodeName != null || (nodeName != null && nodeName.Length != 0)) && (oletxTransactionManager.NodeName == null || !oletxTransactionManager.NodeName.Equals(nodeName)))
			{
				throw new ArgumentException(SR.GetString("InvalidRecoveryInformation"), "recoveryInformation");
			}
			return oletxTransactionManager;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000346E4 File Offset: 0x00033AE4
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static void RecoveryComplete(Guid resourceManagerIdentifier)
		{
			if (resourceManagerIdentifier == Guid.Empty)
			{
				throw new ArgumentException(SR.GetString("BadResourceManagerId"), "resourceManagerIdentifier");
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionManager.RecoveryComplete");
			}
			if (DiagnosticTrace.Information)
			{
				RecoveryCompleteTraceRecord.Trace(SR.GetString("TraceSourceBase"), resourceManagerIdentifier);
			}
			TransactionManager.DistributedTransactionManager.ResourceManagerRecoveryComplete(resourceManagerIdentifier);
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionManager.RecoveryComplete");
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00034774 File Offset: 0x00033B74
		private static object ClassSyncObject
		{
			get
			{
				if (TransactionManager.classSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref TransactionManager.classSyncObject, value, null);
				}
				return TransactionManager.classSyncObject;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x000347A4 File Offset: 0x00033BA4
		internal static IsolationLevel DefaultIsolationLevel
		{
			get
			{
				if (DiagnosticTrace.Verbose)
				{
					MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionManager.get_DefaultIsolationLevel");
					MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionManager.get_DefaultIsolationLevel");
				}
				return IsolationLevel.Serializable;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x000347E4 File Offset: 0x00033BE4
		private static System.Transactions.Configuration.DefaultSettingsSection DefaultSettings
		{
			get
			{
				if (TransactionManager.defaultSettings == null)
				{
					TransactionManager.defaultSettings = System.Transactions.Configuration.DefaultSettingsSection.GetSection();
				}
				return TransactionManager.defaultSettings;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00034814 File Offset: 0x00033C14
		private static System.Transactions.Configuration.MachineSettingsSection MachineSettings
		{
			get
			{
				if (TransactionManager.machineSettings == null)
				{
					TransactionManager.machineSettings = System.Transactions.Configuration.MachineSettingsSection.GetSection();
				}
				return TransactionManager.machineSettings;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00034844 File Offset: 0x00033C44
		public static TimeSpan DefaultTimeout
		{
			get
			{
				if (!TransactionManager._platformValidated)
				{
					TransactionManager.ValidatePlatform();
				}
				if (DiagnosticTrace.Verbose)
				{
					MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionManager.get_DefaultTimeout");
				}
				if (!TransactionManager._defaultTimeoutValidated)
				{
					TransactionManager._defaultTimeout = TransactionManager.ValidateTimeout(TransactionManager.DefaultSettings.Timeout);
					if (TransactionManager._defaultTimeout != TransactionManager.DefaultSettings.Timeout && DiagnosticTrace.Warning)
					{
						ConfiguredDefaultTimeoutAdjustedTraceRecord.Trace(SR.GetString("TraceSourceBase"));
					}
					TransactionManager._defaultTimeoutValidated = true;
				}
				if (DiagnosticTrace.Verbose)
				{
					MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionManager.get_DefaultTimeout");
				}
				return TransactionManager._defaultTimeout;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002EC RID: 748 RVA: 0x000348F4 File Offset: 0x00033CF4
		public static TimeSpan MaximumTimeout
		{
			get
			{
				if (!TransactionManager._platformValidated)
				{
					TransactionManager.ValidatePlatform();
				}
				if (DiagnosticTrace.Verbose)
				{
					MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionManager.get_DefaultMaximumTimeout");
				}
				if (!TransactionManager._cachedMaxTimeout)
				{
					lock (TransactionManager.ClassSyncObject)
					{
						if (!TransactionManager._cachedMaxTimeout)
						{
							TimeSpan maxTimeout = TransactionManager.MachineSettings.MaxTimeout;
							Thread.MemoryBarrier();
							TransactionManager._maximumTimeout = maxTimeout;
							TransactionManager._cachedMaxTimeout = true;
						}
					}
				}
				if (DiagnosticTrace.Verbose)
				{
					MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionManager.get_DefaultMaximumTimeout");
				}
				return TransactionManager._maximumTimeout;
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000349A4 File Offset: 0x00033DA4
		internal static byte[] GetRecoveryInformation(string startupInfo, byte[] resourceManagerRecoveryInformation)
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionManager.GetRecoveryInformation");
			}
			MemoryStream memoryStream = new MemoryStream();
			byte[] result = null;
			try
			{
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(1);
				if (startupInfo != null)
				{
					binaryWriter.Write(startupInfo);
				}
				else
				{
					binaryWriter.Write("");
				}
				binaryWriter.Write(resourceManagerRecoveryInformation);
				binaryWriter.Flush();
				result = memoryStream.ToArray();
			}
			finally
			{
				memoryStream.Close();
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionManager.GetRecoveryInformation");
			}
			return result;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00034A54 File Offset: 0x00033E54
		internal static byte[] ConvertToByteArray(object thingToConvert)
		{
			MemoryStream memoryStream = new MemoryStream();
			byte[] array = null;
			try
			{
				IFormatter formatter = new BinaryFormatter();
				formatter.Serialize(memoryStream, thingToConvert);
				array = new byte[memoryStream.Length];
				memoryStream.Position = 0L;
				memoryStream.Read(array, 0, Convert.ToInt32(memoryStream.Length, CultureInfo.InvariantCulture));
			}
			finally
			{
				memoryStream.Close();
			}
			return array;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00034AD4 File Offset: 0x00033ED4
		internal static void ValidateIsolationLevel(IsolationLevel transactionIsolationLevel)
		{
			switch (transactionIsolationLevel)
			{
			case IsolationLevel.Serializable:
			case IsolationLevel.RepeatableRead:
			case IsolationLevel.ReadCommitted:
			case IsolationLevel.ReadUncommitted:
			case IsolationLevel.Snapshot:
			case IsolationLevel.Chaos:
			case IsolationLevel.Unspecified:
				return;
			default:
				throw new ArgumentOutOfRangeException("transactionIsolationLevel");
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00034B14 File Offset: 0x00033F14
		internal static TimeSpan ValidateTimeout(TimeSpan transactionTimeout)
		{
			if (transactionTimeout < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("transactionTimeout");
			}
			if (TransactionManager.MaximumTimeout != TimeSpan.Zero && (transactionTimeout > TransactionManager.MaximumTimeout || transactionTimeout == TimeSpan.Zero))
			{
				return TransactionManager.MaximumTimeout;
			}
			return transactionTimeout;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00034B74 File Offset: 0x00033F74
		internal static Transaction FindPromotedTransaction(Guid transactionIdentifier)
		{
			Hashtable hashtable = TransactionManager.PromotedTransactionTable;
			WeakReference weakReference = (WeakReference)hashtable[transactionIdentifier];
			if (weakReference != null)
			{
				Transaction transaction = weakReference.Target as Transaction;
				if (null != transaction)
				{
					return transaction.InternalClone();
				}
				lock (hashtable)
				{
					hashtable.Remove(transactionIdentifier);
				}
			}
			return null;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00034BF4 File Offset: 0x00033FF4
		internal static Transaction FindOrCreatePromotedTransaction(Guid transactionIdentifier, OletxTransaction oletx)
		{
			Transaction transaction = null;
			Hashtable hashtable = TransactionManager.PromotedTransactionTable;
			lock (hashtable)
			{
				WeakReference weakReference = (WeakReference)hashtable[transactionIdentifier];
				if (weakReference != null)
				{
					transaction = (weakReference.Target as Transaction);
					if (null != transaction)
					{
						oletx.Dispose();
						return transaction.InternalClone();
					}
					lock (hashtable)
					{
						hashtable.Remove(transactionIdentifier);
					}
				}
				transaction = new Transaction(oletx);
				transaction.internalTransaction.finalizedObject = new FinalizedObject(transaction.internalTransaction, oletx.Identifier);
				weakReference = new WeakReference(transaction, false);
				hashtable[oletx.Identifier] = weakReference;
			}
			oletx.savedLtmPromotedTransaction = transaction;
			TransactionManager.FireDistributedTransactionStarted(transaction);
			return transaction;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00034D04 File Offset: 0x00034104
		internal static Hashtable PromotedTransactionTable
		{
			get
			{
				if (TransactionManager.promotedTransactionTable == null)
				{
					lock (TransactionManager.ClassSyncObject)
					{
						if (TransactionManager.promotedTransactionTable == null)
						{
							Hashtable hashtable = new Hashtable(100);
							Thread.MemoryBarrier();
							TransactionManager.promotedTransactionTable = hashtable;
						}
					}
				}
				return TransactionManager.promotedTransactionTable;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00034D74 File Offset: 0x00034174
		internal static TransactionTable TransactionTable
		{
			get
			{
				if (TransactionManager.transactionTable == null)
				{
					lock (TransactionManager.ClassSyncObject)
					{
						if (TransactionManager.transactionTable == null)
						{
							TransactionTable transactionTable = new TransactionTable();
							Thread.MemoryBarrier();
							TransactionManager.transactionTable = transactionTable;
						}
					}
				}
				return TransactionManager.transactionTable;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00034DE4 File Offset: 0x000341E4
		internal static OletxTransactionManager DistributedTransactionManager
		{
			get
			{
				if (TransactionManager.distributedTransactionManager == null)
				{
					lock (TransactionManager.ClassSyncObject)
					{
						if (TransactionManager.distributedTransactionManager == null)
						{
							OletxTransactionManager oletxTransactionManager = new OletxTransactionManager(TransactionManager.DefaultSettings.DistributedTransactionManagerName);
							Thread.MemoryBarrier();
							TransactionManager.distributedTransactionManager = oletxTransactionManager;
						}
					}
				}
				return TransactionManager.distributedTransactionManager;
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00034E54 File Offset: 0x00034254
		internal static void ValidatePlatform()
		{
			if (PlatformID.Win32NT != Environment.OSVersion.Platform)
			{
				throw new PlatformNotSupportedException(SR.GetString("OnlySupportedOnWinNT"));
			}
			TransactionManager._platformValidated = true;
		}

		// Token: 0x04000117 RID: 279
		private const int recoveryInformationVersion1 = 1;

		// Token: 0x04000118 RID: 280
		private const int currentRecoveryVersion = 1;

		// Token: 0x04000119 RID: 281
		internal static bool _platformValidated;

		// Token: 0x0400011A RID: 282
		private static Hashtable promotedTransactionTable;

		// Token: 0x0400011B RID: 283
		private static TransactionTable transactionTable;

		// Token: 0x0400011C RID: 284
		private static TransactionStartedEventHandler distributedTransactionStartedDelegate;

		// Token: 0x0400011D RID: 285
		internal static HostCurrentTransactionCallback currentDelegate;

		// Token: 0x0400011E RID: 286
		internal static bool currentDelegateSet;

		// Token: 0x0400011F RID: 287
		private static object classSyncObject;

		// Token: 0x04000120 RID: 288
		private static System.Transactions.Configuration.DefaultSettingsSection defaultSettings;

		// Token: 0x04000121 RID: 289
		private static System.Transactions.Configuration.MachineSettingsSection machineSettings;

		// Token: 0x04000122 RID: 290
		private static bool _defaultTimeoutValidated;

		// Token: 0x04000123 RID: 291
		private static TimeSpan _defaultTimeout;

		// Token: 0x04000124 RID: 292
		private static bool _cachedMaxTimeout;

		// Token: 0x04000125 RID: 293
		private static TimeSpan _maximumTimeout;

		// Token: 0x04000126 RID: 294
		internal static OletxTransactionManager distributedTransactionManager;
	}
}
