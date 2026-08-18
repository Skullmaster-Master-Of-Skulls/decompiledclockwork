using System;
using System.Net.Security;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008D4 RID: 2260
	internal static class Msmq
	{
		// Token: 0x06005605 RID: 22021 RVA: 0x0013AC50 File Offset: 0x00138E50
		static Msmq()
		{
			MsmqQueue.GetMsmqInformation(ref Msmq.version, ref Msmq.activeDirectoryEnabled);
			MsmqDiagnostics.MsmqDetected(Msmq.version);
			Version version = Environment.OSVersion.Version;
			if (version.Major == 5 && version.Minor == 1)
			{
				Msmq.xpSendLock = new object();
			}
		}

		// Token: 0x17001502 RID: 5378
		// (get) Token: 0x06005606 RID: 22022 RVA: 0x0013ACCB File Offset: 0x00138ECB
		internal static bool ActiveDirectoryEnabled
		{
			get
			{
				return Msmq.activeDirectoryEnabled;
			}
		}

		// Token: 0x17001503 RID: 5379
		// (get) Token: 0x06005607 RID: 22023 RVA: 0x0013ACD2 File Offset: 0x00138ED2
		internal static Version Version
		{
			get
			{
				return Msmq.version;
			}
		}

		// Token: 0x17001504 RID: 5380
		// (get) Token: 0x06005608 RID: 22024 RVA: 0x0013ACD9 File Offset: 0x00138ED9
		internal static bool IsPerAppDeadLetterQueueSupported
		{
			get
			{
				return Msmq.Version >= Msmq.longhornVersion;
			}
		}

		// Token: 0x17001505 RID: 5381
		// (get) Token: 0x06005609 RID: 22025 RVA: 0x0013ACEA File Offset: 0x00138EEA
		internal static bool IsAdvancedPoisonHandlingSupported
		{
			get
			{
				return Msmq.Version >= Msmq.longhornVersion;
			}
		}

		// Token: 0x17001506 RID: 5382
		// (get) Token: 0x0600560A RID: 22026 RVA: 0x0013ACFB File Offset: 0x00138EFB
		internal static bool IsRejectMessageSupported
		{
			get
			{
				return Msmq.Version >= Msmq.longhornVersion;
			}
		}

		// Token: 0x17001507 RID: 5383
		// (get) Token: 0x0600560B RID: 22027 RVA: 0x0013AD0C File Offset: 0x00138F0C
		internal static bool IsRemoteReceiveContextSupported
		{
			get
			{
				return Msmq.Version >= Msmq.longhornVersion;
			}
		}

		// Token: 0x17001508 RID: 5384
		// (get) Token: 0x0600560C RID: 22028 RVA: 0x0013AD1D File Offset: 0x00138F1D
		internal static UriPrefixTable<ITransportManagerRegistration> StaticTransportManagerTable
		{
			get
			{
				return Msmq.transportManagerTable;
			}
		}

		// Token: 0x0600560D RID: 22029 RVA: 0x0013AD24 File Offset: 0x00138F24
		internal static IPoisonHandlingStrategy CreatePoisonHandler(MsmqReceiveHelper receiver)
		{
			if (!receiver.Transactional)
			{
				return new MsmqNonTransactedPoisonHandler(receiver);
			}
			if (Msmq.Version < Msmq.longhornVersion)
			{
				return new Msmq3PoisonHandler(receiver);
			}
			if (receiver.ListenUri.AbsoluteUri.Contains(";"))
			{
				return new Msmq4SubqueuePoisonHandler(receiver);
			}
			return new Msmq4PoisonHandler(receiver);
		}

		// Token: 0x0600560E RID: 22030 RVA: 0x0013AD7C File Offset: 0x00138F7C
		internal static MsmqQueue CreateMsmqQueue(MsmqReceiveHelper receiver)
		{
			if (!receiver.MsmqReceiveParameters.ReceiveContextSettings.Enabled)
			{
				return new MsmqQueue(receiver.MsmqReceiveParameters.AddressTranslator.UriToFormatName(receiver.ListenUri), 1);
			}
			if (Msmq.Version < Msmq.longhornVersion)
			{
				return new MsmqDefaultLockingQueue(receiver.MsmqReceiveParameters.AddressTranslator.UriToFormatName(receiver.ListenUri), 1);
			}
			return new MsmqSubqueueLockingQueue(receiver.MsmqReceiveParameters.AddressTranslator.UriToFormatName(receiver.ListenUri), receiver.ListenUri.Host, 1);
		}

		// Token: 0x17001509 RID: 5385
		// (get) Token: 0x0600560F RID: 22031 RVA: 0x0013AE10 File Offset: 0x00139010
		internal static SafeLibraryHandle ErrorStrings
		{
			get
			{
				if (Msmq.errorStrings == null)
				{
					object obj = Msmq.staticLock;
					lock (obj)
					{
						if (Msmq.errorStrings == null)
						{
							Msmq.errorStrings = UnsafeNativeMethods.LoadLibraryEx("MQUTIL.DLL", IntPtr.Zero, 2050U);
						}
					}
				}
				return Msmq.errorStrings;
			}
		}

		// Token: 0x06005610 RID: 22032 RVA: 0x0013AE80 File Offset: 0x00139080
		internal static void EnterXPSendLock(out bool lockHeld, ProtectionLevel protectionLevel)
		{
			lockHeld = false;
			if (Msmq.xpSendLock != null && protectionLevel != ProtectionLevel.None)
			{
				Monitor.Enter(Msmq.xpSendLock, ref lockHeld);
			}
		}

		// Token: 0x06005611 RID: 22033 RVA: 0x0013AE9A File Offset: 0x0013909A
		internal static void LeaveXPSendLock()
		{
			Monitor.Exit(Msmq.xpSendLock);
		}

		// Token: 0x04003530 RID: 13616
		private static Version longhornVersion = new Version(4, 0);

		// Token: 0x04003531 RID: 13617
		private static Version version;

		// Token: 0x04003532 RID: 13618
		private static bool activeDirectoryEnabled;

		// Token: 0x04003533 RID: 13619
		private static object xpSendLock = null;

		// Token: 0x04003534 RID: 13620
		private static UriPrefixTable<ITransportManagerRegistration> transportManagerTable = new UriPrefixTable<ITransportManagerRegistration>();

		// Token: 0x04003535 RID: 13621
		private static object staticLock = new object();

		// Token: 0x04003536 RID: 13622
		private static volatile SafeLibraryHandle errorStrings = null;
	}
}
