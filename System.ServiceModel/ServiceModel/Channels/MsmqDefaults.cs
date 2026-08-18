using System;
using System.Net.Security;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200078D RID: 1933
	internal static class MsmqDefaults
	{
		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x0600499C RID: 18844 RVA: 0x0010EBCC File Offset: 0x0010CDCC
		internal static MsmqSecureHashAlgorithm MsmqSecureHashAlgorithm
		{
			get
			{
				if (!LocalAppContextSwitches.UseSha1InMsmqEncryptionAlgorithm)
				{
					return MsmqSecureHashAlgorithm.Sha256;
				}
				return MsmqSecureHashAlgorithm.Sha1;
			}
		}

		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x0600499D RID: 18845 RVA: 0x0010EBD8 File Offset: 0x0010CDD8
		internal static TimeSpan RetryCycleDelay
		{
			get
			{
				return TimeSpanHelper.FromMinutes(30, "00:30:00");
			}
		}

		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x0600499E RID: 18846 RVA: 0x0010EBE6 File Offset: 0x0010CDE6
		internal static TimeSpan TimeToLive
		{
			get
			{
				return TimeSpanHelper.FromDays(1, "1.00:00:00");
			}
		}

		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x0600499F RID: 18847 RVA: 0x0010EBF3 File Offset: 0x0010CDF3
		internal static TimeSpan ValidityDuration
		{
			get
			{
				return TimeSpanHelper.FromMinutes(5, "00:05:00");
			}
		}

		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x060049A0 RID: 18848 RVA: 0x0010EC00 File Offset: 0x0010CE00
		internal static SecurityAlgorithmSuite MessageSecurityAlgorithmSuite
		{
			get
			{
				return SecurityAlgorithmSuite.Default;
			}
		}

		// Token: 0x04002E57 RID: 11863
		internal const MessageCredentialType DefaultClientCredentialType = MessageCredentialType.Windows;

		// Token: 0x04002E58 RID: 11864
		internal const Uri CustomDeadLetterQueue = null;

		// Token: 0x04002E59 RID: 11865
		internal const DeadLetterQueue DeadLetterQueue = DeadLetterQueue.System;

		// Token: 0x04002E5A RID: 11866
		internal const bool Durable = true;

		// Token: 0x04002E5B RID: 11867
		internal const bool ExactlyOnce = true;

		// Token: 0x04002E5C RID: 11868
		internal const bool ReceiveContextEnabled = true;

		// Token: 0x04002E5D RID: 11869
		internal const int MaxRetryCycles = 2;

		// Token: 0x04002E5E RID: 11870
		internal const int MaxPoolSize = 8;

		// Token: 0x04002E5F RID: 11871
		internal const MsmqAuthenticationMode MsmqAuthenticationMode = MsmqAuthenticationMode.WindowsDomain;

		// Token: 0x04002E60 RID: 11872
		internal const MsmqEncryptionAlgorithm MsmqEncryptionAlgorithm = MsmqEncryptionAlgorithm.RC4Stream;

		// Token: 0x04002E61 RID: 11873
		internal const MsmqSecureHashAlgorithm DefaultMsmqSecureHashAlgorithm = MsmqSecureHashAlgorithm.Sha256;

		// Token: 0x04002E62 RID: 11874
		internal const ProtectionLevel MsmqProtectionLevel = ProtectionLevel.Sign;

		// Token: 0x04002E63 RID: 11875
		internal const ReceiveErrorHandling ReceiveErrorHandling = ReceiveErrorHandling.Fault;

		// Token: 0x04002E64 RID: 11876
		internal const int ReceiveRetryCount = 5;

		// Token: 0x04002E65 RID: 11877
		internal const QueueTransferProtocol QueueTransferProtocol = QueueTransferProtocol.Native;

		// Token: 0x04002E66 RID: 11878
		internal const string RetryCycleDelayString = "00:30:00";

		// Token: 0x04002E67 RID: 11879
		internal const string TimeToLiveString = "1.00:00:00";

		// Token: 0x04002E68 RID: 11880
		internal const bool UseActiveDirectory = false;

		// Token: 0x04002E69 RID: 11881
		internal const bool UseSourceJournal = false;

		// Token: 0x04002E6A RID: 11882
		internal const bool UseMsmqTracing = false;

		// Token: 0x04002E6B RID: 11883
		internal const string ValidityDurationString = "00:05:00";
	}
}
