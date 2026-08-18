using System;
using System.Net.Security;
using System.Runtime.CompilerServices;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008FE RID: 2302
	internal static class MsmqVerifier
	{
		// Token: 0x060057E0 RID: 22496 RVA: 0x00142C94 File Offset: 0x00140E94
		internal static void VerifySender<TChannel>(MsmqChannelFactoryBase<TChannel> factory)
		{
			if (!factory.Durable && factory.ExactlyOnce)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqNoAssurancesForVolatile")));
			}
			MsmqChannelFactory<TChannel> msmqChannelFactory = factory as MsmqChannelFactory<TChannel>;
			if (msmqChannelFactory != null && msmqChannelFactory.UseActiveDirectory && msmqChannelFactory.QueueTransferProtocol != QueueTransferProtocol.Native)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqActiveDirectoryRequiresNativeTransfer")));
			}
			bool? useActiveDirectory = null;
			if (msmqChannelFactory != null)
			{
				useActiveDirectory = new bool?(msmqChannelFactory.UseActiveDirectory);
			}
			MsmqVerifier.VerifySecurity(factory.MsmqTransportSecurity, useActiveDirectory);
			if (null != factory.CustomDeadLetterQueue)
			{
				if (DeadLetterQueue.Custom != factory.DeadLetterQueue)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqPerAppDLQRequiresCustom")));
				}
				if (!Msmq.IsPerAppDeadLetterQueueSupported)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqPerAppDLQRequiresMsmq4")));
				}
				if (!factory.ExactlyOnce)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqPerAppDLQRequiresExactlyOnce")));
				}
				string formatName = MsmqUri.NetMsmqAddressTranslator.UriToFormatName(factory.CustomDeadLetterQueue);
				if (!MsmqQueue.IsWriteable(formatName))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqDLQNotWriteable")));
				}
				bool flag;
				if (!MsmqQueue.TryGetIsTransactional(formatName, out flag) || !flag)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqTransactedDLQExpected")));
				}
			}
			if (null == factory.CustomDeadLetterQueue && DeadLetterQueue.Custom == factory.DeadLetterQueue)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqCustomRequiresPerAppDLQ")));
			}
			if (MsmqAuthenticationMode.Certificate == factory.MsmqTransportSecurity.MsmqAuthenticationMode)
			{
				MsmqVerifier.EnsureSecurityTokenManagerPresent<TChannel>(factory);
			}
		}

		// Token: 0x060057E1 RID: 22497 RVA: 0x00142E3C File Offset: 0x0014103C
		internal static void VerifyReceiver(MsmqReceiveParameters receiveParameters, Uri listenUri)
		{
			if (!receiveParameters.Durable && receiveParameters.ExactlyOnce)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqNoAssurancesForVolatile")));
			}
			if (receiveParameters.ReceiveContextSettings.Enabled && !receiveParameters.ExactlyOnce)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqExactlyOnceNeededForReceiveContext")));
			}
			MsmqVerifier.VerifySecurity(receiveParameters.TransportSecurity, null);
			string text = receiveParameters.AddressTranslator.UriToFormatName(listenUri);
			if (receiveParameters.ReceiveContextSettings.Enabled && text.Contains(";"))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqReceiveContextSubqueuesNotSupported")));
			}
			MsmqException innerException;
			if (!MsmqQueue.IsReadable(text, out innerException))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqQueueNotReadable"), innerException));
			}
			bool flag = false;
			bool flag2;
			flag = MsmqQueue.TryGetIsTransactional(text, out flag2);
			try
			{
				if (!flag && receiveParameters is MsmqTransportReceiveParameters)
				{
					flag = MsmqQueue.TryGetIsTransactional(MsmqUri.ActiveDirectoryAddressTranslator.UriToFormatName(listenUri), out flag2);
				}
			}
			catch (MsmqException ex)
			{
				MsmqDiagnostics.ExpectedException(ex);
			}
			if (flag)
			{
				if (!receiveParameters.ExactlyOnce && flag2)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqNonTransactionalQueueNeeded")));
				}
				if (receiveParameters.ExactlyOnce && !flag2)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqTransactionalQueueNeeded")));
				}
			}
			if (receiveParameters.ExactlyOnce)
			{
				if (Msmq.IsAdvancedPoisonHandlingSupported)
				{
					if (text.Contains(";"))
					{
						if (ReceiveErrorHandling.Move == receiveParameters.ReceiveErrorHandling)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqNoMoveForSubqueues")));
						}
					}
					else if (!MsmqQueue.IsMoveable(text + ";retry"))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqDirectFormatNameRequiredForPoison")));
					}
				}
				else if (ReceiveErrorHandling.Reject == receiveParameters.ReceiveErrorHandling || ReceiveErrorHandling.Move == receiveParameters.ReceiveErrorHandling)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqAdvancedPoisonHandlingRequired")));
				}
			}
		}

		// Token: 0x060057E2 RID: 22498 RVA: 0x0014304C File Offset: 0x0014124C
		private static void VerifySecurity(MsmqTransportSecurity security, bool? useActiveDirectory)
		{
			if (security.MsmqAuthenticationMode == MsmqAuthenticationMode.WindowsDomain && !Msmq.ActiveDirectoryEnabled)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqWindowsAuthnRequiresAD")));
			}
			if (security.MsmqAuthenticationMode == MsmqAuthenticationMode.None && security.MsmqProtectionLevel != ProtectionLevel.None)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqAuthNoneRequiresProtectionNone")));
			}
			if (security.MsmqAuthenticationMode == MsmqAuthenticationMode.Certificate && security.MsmqProtectionLevel == ProtectionLevel.None)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqAuthCertificateRequiresProtectionSign")));
			}
			if (security.MsmqAuthenticationMode == MsmqAuthenticationMode.WindowsDomain && security.MsmqProtectionLevel == ProtectionLevel.None)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqAuthWindowsRequiresProtectionNotNone")));
			}
			if (security.MsmqProtectionLevel == ProtectionLevel.EncryptAndSign && useActiveDirectory != null && !useActiveDirectory.Value)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqEncryptRequiresUseAD")));
			}
		}

		// Token: 0x060057E3 RID: 22499 RVA: 0x00143138 File Offset: 0x00141338
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void EnsureSecurityTokenManagerPresent<TChannel>(MsmqChannelFactoryBase<TChannel> factory)
		{
			if (factory.SecurityTokenManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqTokenProviderNeededForCertificates")));
			}
		}
	}
}
