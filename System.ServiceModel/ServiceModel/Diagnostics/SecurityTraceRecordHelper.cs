using System;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime.Diagnostics;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A90 RID: 2704
	internal static class SecurityTraceRecordHelper
	{
		// Token: 0x06006AC1 RID: 27329 RVA: 0x0018E07B File Offset: 0x0018C27B
		internal static void TraceRemovedCachedServiceToken<T>(IssuanceTokenProviderBase<T> provider, SecurityToken serviceToken) where T : IssuanceTokenProviderState
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458761, SR.GetString("TraceCodeIssuanceTokenProviderRemovedCachedToken"), new SecurityTraceRecordHelper.IssuanceProviderTraceRecord<T>(provider, serviceToken));
			}
		}

		// Token: 0x06006AC2 RID: 27330 RVA: 0x0018E0A0 File Offset: 0x0018C2A0
		internal static void TraceUsingCachedServiceToken<T>(IssuanceTokenProviderBase<T> provider, SecurityToken serviceToken, EndpointAddress target) where T : IssuanceTokenProviderState
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458762, SR.GetString("TraceCodeIssuanceTokenProviderUsingCachedToken"), new SecurityTraceRecordHelper.IssuanceProviderTraceRecord<T>(provider, serviceToken, target));
			}
		}

		// Token: 0x06006AC3 RID: 27331 RVA: 0x0018E0C6 File Offset: 0x0018C2C6
		internal static void TraceBeginSecurityNegotiation<T>(IssuanceTokenProviderBase<T> provider, EndpointAddress target) where T : IssuanceTokenProviderState
		{
			if (TD.SecurityNegotiationStartIsEnabled())
			{
				TD.SecurityNegotiationStart(provider.EventTraceActivity);
			}
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458763, SR.GetString("TraceCodeIssuanceTokenProviderBeginSecurityNegotiation"), new SecurityTraceRecordHelper.IssuanceProviderTraceRecord<T>(provider, target));
			}
		}

		// Token: 0x06006AC4 RID: 27332 RVA: 0x0018E0FD File Offset: 0x0018C2FD
		internal static void TraceEndSecurityNegotiation<T>(IssuanceTokenProviderBase<T> provider, SecurityToken serviceToken, EndpointAddress target) where T : IssuanceTokenProviderState
		{
			if (TD.SecurityNegotiationStopIsEnabled())
			{
				TD.SecurityNegotiationStop(provider.EventTraceActivity);
			}
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458764, SR.GetString("TraceCodeIssuanceTokenProviderEndSecurityNegotiation"), new SecurityTraceRecordHelper.IssuanceProviderTraceRecord<T>(provider, serviceToken, target));
			}
		}

		// Token: 0x06006AC5 RID: 27333 RVA: 0x0018E135 File Offset: 0x0018C335
		internal static void TraceRedirectApplied<T>(IssuanceTokenProviderBase<T> provider, EndpointAddress newTarget, EndpointAddress oldTarget) where T : IssuanceTokenProviderState
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458765, SR.GetString("TraceCodeIssuanceTokenProviderRedirectApplied"), new SecurityTraceRecordHelper.IssuanceProviderTraceRecord<T>(provider, newTarget, oldTarget));
			}
		}

		// Token: 0x06006AC6 RID: 27334 RVA: 0x0018E15B File Offset: 0x0018C35B
		internal static void TraceClientServiceTokenCacheFull<T>(IssuanceTokenProviderBase<T> provider, int cacheSize) where T : IssuanceTokenProviderState
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458766, SR.GetString("TraceCodeIssuanceTokenProviderServiceTokenCacheFull"), new SecurityTraceRecordHelper.IssuanceProviderTraceRecord<T>(provider, cacheSize));
			}
		}

		// Token: 0x06006AC7 RID: 27335 RVA: 0x0018E180 File Offset: 0x0018C380
		internal static void TraceClientSpnego(WindowsSspiNegotiation windowsNegotiation)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458784, SR.GetString("TraceCodeSpnegoClientNegotiationCompleted"), new SecurityTraceRecordHelper.WindowsSspiNegotiationTraceRecord(windowsNegotiation));
			}
		}

		// Token: 0x06006AC8 RID: 27336 RVA: 0x0018E1A4 File Offset: 0x0018C3A4
		internal static void TraceServiceSpnego(WindowsSspiNegotiation windowsNegotiation)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458785, SR.GetString("TraceCodeSpnegoServiceNegotiationCompleted"), new SecurityTraceRecordHelper.WindowsSspiNegotiationTraceRecord(windowsNegotiation));
			}
		}

		// Token: 0x06006AC9 RID: 27337 RVA: 0x0018E1C8 File Offset: 0x0018C3C8
		internal static void TraceClientOutgoingSpnego(WindowsSspiNegotiation windowsNegotiation)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458786, SR.GetString("TraceCodeSpnegoClientNegotiation"), new SecurityTraceRecordHelper.WindowsSspiNegotiationTraceRecord(windowsNegotiation));
			}
		}

		// Token: 0x06006ACA RID: 27338 RVA: 0x0018E1EC File Offset: 0x0018C3EC
		internal static void TraceServiceOutgoingSpnego(WindowsSspiNegotiation windowsNegotiation)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458787, SR.GetString("TraceCodeSpnegoServiceNegotiation"), new SecurityTraceRecordHelper.WindowsSspiNegotiationTraceRecord(windowsNegotiation));
			}
		}

		// Token: 0x06006ACB RID: 27339 RVA: 0x0018E210 File Offset: 0x0018C410
		internal static void TraceNegotiationTokenAuthenticatorAttached<T>(NegotiationTokenAuthenticator<T> authenticator, IChannelListener transportChannelListener) where T : NegotiationTokenAuthenticatorState
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458788, SR.GetString("TraceCodeNegotiationAuthenticatorAttached"), new SecurityTraceRecordHelper.NegotiationAuthenticatorTraceRecord<T>(authenticator, transportChannelListener));
			}
		}

		// Token: 0x06006ACC RID: 27340 RVA: 0x0018E238 File Offset: 0x0018C438
		internal static void TraceServiceSecurityNegotiationCompleted<T>(Message message, NegotiationTokenAuthenticator<T> authenticator, SecurityContextSecurityToken serviceToken) where T : NegotiationTokenAuthenticatorState
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458789, SR.GetString("TraceCodeServiceSecurityNegotiationCompleted"), new SecurityTraceRecordHelper.NegotiationAuthenticatorTraceRecord<T>(authenticator, serviceToken));
			}
			if (TD.ServiceSecurityNegotiationCompletedIsEnabled())
			{
				EventTraceActivity eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(message);
				TD.ServiceSecurityNegotiationCompleted(eventTraceActivity);
			}
		}

		// Token: 0x06006ACD RID: 27341 RVA: 0x0018E27C File Offset: 0x0018C47C
		internal static void TraceServiceSecurityNegotiationFailure<T>(EventTraceActivity eventTraceActivity, NegotiationTokenAuthenticator<T> authenticator, Exception e) where T : NegotiationTokenAuthenticatorState
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 458760, SR.GetString("TraceCodeSecurityNegotiationProcessingFailure"), new SecurityTraceRecordHelper.NegotiationAuthenticatorTraceRecord<T>(authenticator, e));
			}
			if (TD.SecurityNegotiationProcessingFailureIsEnabled())
			{
				TD.SecurityNegotiationProcessingFailure(eventTraceActivity);
			}
		}

		// Token: 0x06006ACE RID: 27342 RVA: 0x0018E2AE File Offset: 0x0018C4AE
		internal static void TraceSecurityContextTokenCacheFull(int capacity, int pruningAmount)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458790, SR.GetString("TraceCodeSecurityContextTokenCacheFull"), new SecurityTraceRecordHelper.SecurityContextTokenCacheTraceRecord(capacity, pruningAmount));
			}
		}

		// Token: 0x06006ACF RID: 27343 RVA: 0x0018E2D3 File Offset: 0x0018C4D3
		internal static void TraceIdentityVerificationSuccess(EventTraceActivity eventTraceActivity, EndpointIdentity identity, Claim claim, Type identityVerifier)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458753, SR.GetString("TraceCodeSecurityIdentityVerificationSuccess"), new SecurityTraceRecordHelper.IdentityVerificationSuccessTraceRecord(identity, claim, identityVerifier));
			}
			if (TD.SecurityIdentityVerificationSuccessIsEnabled())
			{
				TD.SecurityIdentityVerificationSuccess(eventTraceActivity);
			}
		}

		// Token: 0x06006AD0 RID: 27344 RVA: 0x0018E306 File Offset: 0x0018C506
		internal static void TraceIdentityVerificationFailure(EndpointIdentity identity, AuthorizationContext authContext, Type identityVerifier)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458754, SR.GetString("TraceCodeSecurityIdentityVerificationFailure"), new SecurityTraceRecordHelper.IdentityVerificationFailureTraceRecord(identity, authContext, identityVerifier));
			}
		}

		// Token: 0x06006AD1 RID: 27345 RVA: 0x0018E32C File Offset: 0x0018C52C
		internal static void TraceIdentityDeterminationSuccess(EndpointAddress epr, EndpointIdentity identity, Type identityVerifier)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458755, SR.GetString("TraceCodeSecurityIdentityDeterminationSuccess"), new SecurityTraceRecordHelper.IdentityDeterminationSuccessTraceRecord(epr, identity, identityVerifier));
			}
		}

		// Token: 0x06006AD2 RID: 27346 RVA: 0x0018E352 File Offset: 0x0018C552
		internal static void TraceIdentityDeterminationFailure(EndpointAddress epr, Type identityVerifier)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458756, SR.GetString("TraceCodeSecurityIdentityDeterminationFailure"), new SecurityTraceRecordHelper.IdentityDeterminationFailureTraceRecord(epr, identityVerifier));
			}
		}

		// Token: 0x06006AD3 RID: 27347 RVA: 0x0018E377 File Offset: 0x0018C577
		internal static void TraceIdentityHostNameNormalizationFailure(EndpointAddress epr, Type identityVerifier, Exception e)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458757, SR.GetString("TraceCodeSecurityIdentityHostNameNormalizationFailure"), new SecurityTraceRecordHelper.IdentityHostNameNormalizationFailureTraceRecord(epr, identityVerifier, e));
			}
		}

		// Token: 0x06006AD4 RID: 27348 RVA: 0x0018E39D File Offset: 0x0018C59D
		internal static void TraceExportChannelBindingEntry()
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458791, SR.GetString("TraceCodeExportSecurityChannelBindingEntry"), null);
			}
		}

		// Token: 0x06006AD5 RID: 27349 RVA: 0x0018E3BC File Offset: 0x0018C5BC
		internal static void TraceExportChannelBindingExit()
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458792, SR.GetString("TraceCodeExportSecurityChannelBindingExit"));
			}
		}

		// Token: 0x06006AD6 RID: 27350 RVA: 0x0018E3DA File Offset: 0x0018C5DA
		internal static void TraceImportChannelBindingEntry()
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458793, SR.GetString("TraceCodeImportSecurityChannelBindingEntry"), null);
			}
		}

		// Token: 0x06006AD7 RID: 27351 RVA: 0x0018E3F9 File Offset: 0x0018C5F9
		internal static void TraceImportChannelBindingExit()
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458794, SR.GetString("TraceCodeImportSecurityChannelBindingExit"));
			}
		}

		// Token: 0x06006AD8 RID: 27352 RVA: 0x0018E417 File Offset: 0x0018C617
		internal static void TraceTokenProviderOpened(EventTraceActivity eventTraceActivity, SecurityTokenProvider provider)
		{
			if (TD.SecurityTokenProviderOpenedIsEnabled())
			{
				TD.SecurityTokenProviderOpened(eventTraceActivity);
			}
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458795, SR.GetString("TraceCodeSecurityTokenProviderOpened"), new SecurityTraceRecordHelper.TokenProviderTraceRecord(provider));
			}
		}

		// Token: 0x06006AD9 RID: 27353 RVA: 0x0018E448 File Offset: 0x0018C648
		internal static void TraceTokenProviderClosed(SecurityTokenProvider provider)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458796, SR.GetString("TraceCodeSecurityTokenProviderClosed"), new SecurityTraceRecordHelper.TokenProviderTraceRecord(provider));
			}
		}

		// Token: 0x06006ADA RID: 27354 RVA: 0x0018E46C File Offset: 0x0018C66C
		internal static void TraceTokenAuthenticatorOpened(SecurityTokenAuthenticator authenticator)
		{
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				TraceUtility.TraceEvent(TraceEventType.Verbose, 458797, SR.GetString("TraceCodeSecurityTokenAuthenticatorOpened"), new SecurityTraceRecordHelper.TokenAuthenticatorTraceRecord(authenticator));
			}
		}

		// Token: 0x06006ADB RID: 27355 RVA: 0x0018E491 File Offset: 0x0018C691
		internal static void TraceTokenAuthenticatorClosed(SecurityTokenAuthenticator authenticator)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458798, SR.GetString("TraceCodeSecurityTokenAuthenticatorClosed"), new SecurityTraceRecordHelper.TokenAuthenticatorTraceRecord(authenticator));
			}
		}

		// Token: 0x06006ADC RID: 27356 RVA: 0x0018E4B8 File Offset: 0x0018C6B8
		internal static void TraceOutgoingMessageSecured(SecurityProtocol binding, Message message)
		{
			if (TD.OutgoingMessageSecuredIsEnabled())
			{
				EventTraceActivity eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(message);
				TD.OutgoingMessageSecured(eventTraceActivity);
			}
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458799, SR.GetString("TraceCodeSecurityBindingOutgoingMessageSecured"), new SecurityTraceRecordHelper.MessageSecurityTraceRecord(binding, message), null, null, message);
			}
		}

		// Token: 0x06006ADD RID: 27357 RVA: 0x0018E500 File Offset: 0x0018C700
		internal static void TraceIncomingMessageVerified(SecurityProtocol binding, Message message)
		{
			if (TD.IncomingMessageVerifiedIsEnabled())
			{
				EventTraceActivity eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(message);
				TD.IncomingMessageVerified(eventTraceActivity);
			}
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458800, SR.GetString("TraceCodeSecurityBindingIncomingMessageVerified"), new SecurityTraceRecordHelper.MessageSecurityTraceRecord(binding, message), null, null, message);
			}
		}

		// Token: 0x06006ADE RID: 27358 RVA: 0x0018E547 File Offset: 0x0018C747
		internal static void TraceSecureOutgoingMessageFailure(SecurityProtocol binding, Message message)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 458801, SR.GetString("TraceCodeSecurityBindingSecureOutgoingMessageFailure"), new SecurityTraceRecordHelper.MessageSecurityTraceRecord(binding, message), null, null, message);
			}
		}

		// Token: 0x06006ADF RID: 27359 RVA: 0x0018E56F File Offset: 0x0018C76F
		internal static void TraceVerifyIncomingMessageFailure(SecurityProtocol binding, Message message)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 458802, SR.GetString("TraceCodeSecurityBindingVerifyIncomingMessageFailure"), new SecurityTraceRecordHelper.MessageSecurityTraceRecord(binding, message), null, null, message);
			}
		}

		// Token: 0x06006AE0 RID: 27360 RVA: 0x0018E597 File Offset: 0x0018C797
		internal static void TraceSpnToSidMappingFailure(string spn, Exception e)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 458803, SR.GetString("TraceCodeSecuritySpnToSidMappingFailure"), new SecurityTraceRecordHelper.SpnToSidMappingTraceRecord(spn, e));
			}
		}

		// Token: 0x06006AE1 RID: 27361 RVA: 0x0018E5BC File Offset: 0x0018C7BC
		internal static void TraceSessionRedirectApplied(EndpointAddress previousTarget, EndpointAddress newTarget, GenericXmlSecurityToken sessionToken)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458804, SR.GetString("TraceCodeSecuritySessionRedirectApplied"), new SecurityTraceRecordHelper.SessionRedirectAppliedTraceRecord(previousTarget, newTarget, sessionToken));
			}
		}

		// Token: 0x06006AE2 RID: 27362 RVA: 0x0018E5E2 File Offset: 0x0018C7E2
		internal static void TraceCloseMessageSent(SecurityToken sessionToken, EndpointAddress remoteTarget)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458805, SR.GetString("TraceCodeSecurityClientSessionCloseSent"), new SecurityTraceRecordHelper.ClientSessionTraceRecord(sessionToken, null, remoteTarget));
			}
		}

		// Token: 0x06006AE3 RID: 27363 RVA: 0x0018E608 File Offset: 0x0018C808
		internal static void TraceCloseResponseMessageSent(SecurityToken sessionToken, EndpointAddress remoteTarget)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458806, SR.GetString("TraceCodeSecurityClientSessionCloseResponseSent"), new SecurityTraceRecordHelper.ClientSessionTraceRecord(sessionToken, null, remoteTarget));
			}
		}

		// Token: 0x06006AE4 RID: 27364 RVA: 0x0018E62E File Offset: 0x0018C82E
		internal static void TraceCloseMessageReceived(SecurityToken sessionToken, EndpointAddress remoteTarget)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458807, SR.GetString("TraceCodeSecurityClientSessionCloseMessageReceived"), new SecurityTraceRecordHelper.ClientSessionTraceRecord(sessionToken, null, remoteTarget));
			}
		}

		// Token: 0x06006AE5 RID: 27365 RVA: 0x0018E654 File Offset: 0x0018C854
		internal static void TraceSessionKeyRenewalFault(SecurityToken sessionToken, EndpointAddress remoteTarget)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 458808, SR.GetString("TraceCodeSecuritySessionKeyRenewalFaultReceived"), new SecurityTraceRecordHelper.ClientSessionTraceRecord(sessionToken, null, remoteTarget));
			}
		}

		// Token: 0x06006AE6 RID: 27366 RVA: 0x0018E67A File Offset: 0x0018C87A
		internal static void TraceRemoteSessionAbortedFault(SecurityToken sessionToken, EndpointAddress remoteTarget)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 458809, SR.GetString("TraceCodeSecuritySessionAbortedFaultReceived"), new SecurityTraceRecordHelper.ClientSessionTraceRecord(sessionToken, null, remoteTarget));
			}
		}

		// Token: 0x06006AE7 RID: 27367 RVA: 0x0018E6A0 File Offset: 0x0018C8A0
		internal static void TraceCloseResponseReceived(SecurityToken sessionToken, EndpointAddress remoteTarget)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458810, SR.GetString("TraceCodeSecuritySessionClosedResponseReceived"), new SecurityTraceRecordHelper.ClientSessionTraceRecord(sessionToken, null, remoteTarget));
			}
		}

		// Token: 0x06006AE8 RID: 27368 RVA: 0x0018E6C6 File Offset: 0x0018C8C6
		internal static void TracePreviousSessionKeyDiscarded(SecurityToken previousSessionToken, SecurityToken currentSessionToken, EndpointAddress remoteAddress)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458811, SR.GetString("TraceCodeSecurityClientSessionPreviousKeyDiscarded"), new SecurityTraceRecordHelper.ClientSessionTraceRecord(currentSessionToken, previousSessionToken, remoteAddress));
			}
		}

		// Token: 0x06006AE9 RID: 27369 RVA: 0x0018E6EC File Offset: 0x0018C8EC
		internal static void TraceSessionKeyRenewed(SecurityToken newSessionToken, SecurityToken currentSessionToken, EndpointAddress remoteAddress)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458812, SR.GetString("TraceCodeSecurityClientSessionKeyRenewed"), new SecurityTraceRecordHelper.ClientSessionTraceRecord(newSessionToken, currentSessionToken, remoteAddress));
			}
		}

		// Token: 0x06006AEA RID: 27370 RVA: 0x0018E712 File Offset: 0x0018C912
		internal static void TracePendingSessionAdded(UniqueId sessionId, Uri listenAddress)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458813, SR.GetString("TraceCodeSecurityPendingServerSessionAdded"), new SecurityTraceRecordHelper.ServerSessionTraceRecord(sessionId, listenAddress));
			}
		}

		// Token: 0x06006AEB RID: 27371 RVA: 0x0018E737 File Offset: 0x0018C937
		internal static void TracePendingSessionClosed(UniqueId sessionId, Uri listenAddress)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 458814, SR.GetString("TraceCodeSecurityPendingServerSessionClosed"), new SecurityTraceRecordHelper.ServerSessionTraceRecord(sessionId, listenAddress));
			}
		}

		// Token: 0x06006AEC RID: 27372 RVA: 0x0018E75C File Offset: 0x0018C95C
		internal static void TracePendingSessionActivated(UniqueId sessionId, Uri listenAddress)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458815, SR.GetString("TraceCodeSecurityPendingServerSessionActivated"), new SecurityTraceRecordHelper.ServerSessionTraceRecord(sessionId, listenAddress));
			}
		}

		// Token: 0x06006AED RID: 27373 RVA: 0x0018E781 File Offset: 0x0018C981
		internal static void TraceActiveSessionRemoved(UniqueId sessionId, Uri listenAddress)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458816, SR.GetString("TraceCodeSecurityActiveServerSessionRemoved"), new SecurityTraceRecordHelper.ServerSessionTraceRecord(sessionId, listenAddress));
			}
		}

		// Token: 0x06006AEE RID: 27374 RVA: 0x0018E7A6 File Offset: 0x0018C9A6
		internal static void TraceNewServerSessionKeyIssued(SecurityContextSecurityToken newToken, SecurityContextSecurityToken supportingToken, Uri listenAddress)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458817, SR.GetString("TraceCodeSecurityNewServerSessionKeyIssued"), new SecurityTraceRecordHelper.ServerSessionTraceRecord(newToken, supportingToken, listenAddress));
			}
		}

		// Token: 0x06006AEF RID: 27375 RVA: 0x0018E7CC File Offset: 0x0018C9CC
		internal static void TraceInactiveSessionFaulted(SecurityContextSecurityToken sessionToken, Uri listenAddress)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 458818, SR.GetString("TraceCodeSecurityInactiveSessionFaulted"), new SecurityTraceRecordHelper.ServerSessionTraceRecord(sessionToken, null, listenAddress));
			}
		}

		// Token: 0x06006AF0 RID: 27376 RVA: 0x0018E7F2 File Offset: 0x0018C9F2
		internal static void TraceServerSessionKeyUpdated(SecurityContextSecurityToken sessionToken, Uri listenAddress)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458819, SR.GetString("TraceCodeSecurityServerSessionKeyUpdated"), new SecurityTraceRecordHelper.ServerSessionTraceRecord(sessionToken, null, listenAddress));
			}
		}

		// Token: 0x06006AF1 RID: 27377 RVA: 0x0018E818 File Offset: 0x0018CA18
		internal static void TraceServerSessionCloseReceived(SecurityContextSecurityToken sessionToken, Uri listenAddress)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458820, SR.GetString("TraceCodeSecurityServerSessionCloseReceived"), new SecurityTraceRecordHelper.ServerSessionTraceRecord(sessionToken, null, listenAddress));
			}
		}

		// Token: 0x06006AF2 RID: 27378 RVA: 0x0018E83E File Offset: 0x0018CA3E
		internal static void TraceServerSessionCloseResponseReceived(SecurityContextSecurityToken sessionToken, Uri listenAddress)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458825, SR.GetString("TraceCodeSecurityServerSessionCloseResponseReceived"), new SecurityTraceRecordHelper.ServerSessionTraceRecord(sessionToken, null, listenAddress));
			}
		}

		// Token: 0x06006AF3 RID: 27379 RVA: 0x0018E864 File Offset: 0x0018CA64
		internal static void TraceSessionRenewalFaultSent(SecurityContextSecurityToken sessionToken, Uri listenAddress, Message message)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 458821, SR.GetString("TraceCodeSecurityServerSessionRenewalFaultSent"), new SecurityTraceRecordHelper.ServerSessionTraceRecord(sessionToken, message, listenAddress), null, null, message);
			}
		}

		// Token: 0x06006AF4 RID: 27380 RVA: 0x0018E88D File Offset: 0x0018CA8D
		internal static void TraceSessionAbortedFaultSent(SecurityContextSecurityToken sessionToken, Uri listenAddress)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 458822, SR.GetString("TraceCodeSecurityServerSessionAbortedFaultSent"), new SecurityTraceRecordHelper.ServerSessionTraceRecord(sessionToken, null, listenAddress));
			}
		}

		// Token: 0x06006AF5 RID: 27381 RVA: 0x0018E8B3 File Offset: 0x0018CAB3
		internal static void TraceSessionClosedResponseSent(SecurityContextSecurityToken sessionToken, Uri listenAddress)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458823, SR.GetString("TraceCodeSecuritySessionCloseResponseSent"), new SecurityTraceRecordHelper.ServerSessionTraceRecord(sessionToken, null, listenAddress));
			}
		}

		// Token: 0x06006AF6 RID: 27382 RVA: 0x0018E8D9 File Offset: 0x0018CAD9
		internal static void TraceSessionClosedSent(SecurityContextSecurityToken sessionToken, Uri listenAddress)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458824, SR.GetString("TraceCodeSecuritySessionServerCloseSent"), new SecurityTraceRecordHelper.ServerSessionTraceRecord(sessionToken, null, listenAddress));
			}
		}

		// Token: 0x06006AF7 RID: 27383 RVA: 0x0018E8FF File Offset: 0x0018CAFF
		internal static void TraceRenewFaultSendFailure(SecurityContextSecurityToken sessionToken, Uri listenAddress, Exception e)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 458826, SR.GetString("TraceCodeSecuritySessionRenewFaultSendFailure"), new SecurityTraceRecordHelper.ServerSessionTraceRecord(sessionToken, listenAddress), e);
			}
		}

		// Token: 0x06006AF8 RID: 27384 RVA: 0x0018E925 File Offset: 0x0018CB25
		internal static void TraceSessionAbortedFaultSendFailure(SecurityContextSecurityToken sessionToken, Uri listenAddress, Exception e)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 458827, SR.GetString("TraceCodeSecuritySessionAbortedFaultSendFailure"), new SecurityTraceRecordHelper.ServerSessionTraceRecord(sessionToken, listenAddress), e);
			}
		}

		// Token: 0x06006AF9 RID: 27385 RVA: 0x0018E94B File Offset: 0x0018CB4B
		internal static void TraceSessionClosedResponseSendFailure(SecurityContextSecurityToken sessionToken, Uri listenAddress, Exception e)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 458828, SR.GetString("TraceCodeSecuritySessionClosedResponseSendFailure"), new SecurityTraceRecordHelper.ServerSessionTraceRecord(sessionToken, listenAddress), e);
			}
		}

		// Token: 0x06006AFA RID: 27386 RVA: 0x0018E971 File Offset: 0x0018CB71
		internal static void TraceSessionCloseSendFailure(SecurityContextSecurityToken sessionToken, Uri listenAddress, Exception e)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 458829, SR.GetString("TraceCodeSecuritySessionServerCloseSendFailure"), new SecurityTraceRecordHelper.ServerSessionTraceRecord(sessionToken, listenAddress), e);
			}
		}

		// Token: 0x06006AFB RID: 27387 RVA: 0x0018E997 File Offset: 0x0018CB97
		internal static void TraceBeginSecuritySessionOperation(SecuritySessionOperation operation, EndpointAddress target, SecurityToken currentToken)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458830, SR.GetString("TraceCodeSecuritySessionRequestorStartOperation"), new SecurityTraceRecordHelper.SessionRequestorTraceRecord(operation, currentToken, null, target));
			}
		}

		// Token: 0x06006AFC RID: 27388 RVA: 0x0018E9BE File Offset: 0x0018CBBE
		internal static void TraceSecuritySessionOperationSuccess(SecuritySessionOperation operation, EndpointAddress target, SecurityToken currentToken, SecurityToken issuedToken)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458831, SR.GetString("TraceCodeSecuritySessionRequestorOperationSuccess"), new SecurityTraceRecordHelper.SessionRequestorTraceRecord(operation, currentToken, issuedToken, target));
			}
		}

		// Token: 0x06006AFD RID: 27389 RVA: 0x0018E9E5 File Offset: 0x0018CBE5
		internal static void TraceSecuritySessionOperationFailure(SecuritySessionOperation operation, EndpointAddress target, SecurityToken currentToken, Exception e)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 458832, SR.GetString("TraceCodeSecuritySessionRequestorOperationFailure"), new SecurityTraceRecordHelper.SessionRequestorTraceRecord(operation, currentToken, e, target));
			}
		}

		// Token: 0x06006AFE RID: 27390 RVA: 0x0018EA0C File Offset: 0x0018CC0C
		internal static void TraceServerSessionOperationException(SecuritySessionOperation operation, Exception e, Uri listenAddress)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 458833, SR.GetString("TraceCodeSecuritySessionResponderOperationFailure"), new SecurityTraceRecordHelper.SessionResponderTraceRecord(operation, e, listenAddress));
			}
		}

		// Token: 0x06006AFF RID: 27391 RVA: 0x0018EA32 File Offset: 0x0018CC32
		internal static void TraceImpersonationSucceeded(EventTraceActivity eventTraceActivity, DispatchOperationRuntime operation)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458758, SR.GetString("TraceCodeSecurityImpersonationSuccess"), new SecurityTraceRecordHelper.ImpersonationTraceRecord(operation));
			}
			if (TD.SecurityImpersonationSuccessIsEnabled())
			{
				TD.SecurityImpersonationSuccess(eventTraceActivity);
			}
		}

		// Token: 0x06006B00 RID: 27392 RVA: 0x0018EA63 File Offset: 0x0018CC63
		internal static void TraceImpersonationFailed(EventTraceActivity eventTraceActivity, DispatchOperationRuntime operation, Exception e)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 458759, SR.GetString("TraceCodeSecurityImpersonationFailure"), new SecurityTraceRecordHelper.ImpersonationTraceRecord(operation), e);
			}
			if (TD.SecurityImpersonationFailureIsEnabled())
			{
				TD.SecurityImpersonationFailure(eventTraceActivity);
			}
		}

		// Token: 0x06006B01 RID: 27393 RVA: 0x0018EA98 File Offset: 0x0018CC98
		private static void WritePossibleGenericXmlToken(XmlWriter writer, string startElement, SecurityToken token)
		{
			if (writer == null)
			{
				return;
			}
			writer.WriteStartElement(startElement);
			GenericXmlSecurityToken genericXmlSecurityToken = token as GenericXmlSecurityToken;
			if (genericXmlSecurityToken != null)
			{
				SecurityTraceRecordHelper.WriteGenericXmlToken(writer, genericXmlSecurityToken);
			}
			else if (token != null)
			{
				writer.WriteElementString("TokenType", token.GetType().ToString());
			}
			writer.WriteEndElement();
		}

		// Token: 0x06006B02 RID: 27394 RVA: 0x0018EAE4 File Offset: 0x0018CCE4
		private static void WriteGenericXmlToken(XmlWriter xml, SecurityToken sessiontoken)
		{
			if (xml == null || sessiontoken == null)
			{
				return;
			}
			xml.WriteElementString("SessionTokenType", sessiontoken.GetType().ToString());
			xml.WriteElementString("ValidFrom", XmlConvert.ToString(sessiontoken.ValidFrom, XmlDateTimeSerializationMode.Utc));
			xml.WriteElementString("ValidTo", XmlConvert.ToString(sessiontoken.ValidTo, XmlDateTimeSerializationMode.Utc));
			GenericXmlSecurityToken genericXmlSecurityToken = sessiontoken as GenericXmlSecurityToken;
			if (genericXmlSecurityToken != null)
			{
				if (genericXmlSecurityToken.InternalTokenReference != null)
				{
					xml.WriteElementString("InternalTokenReference", genericXmlSecurityToken.InternalTokenReference.ToString());
				}
				if (genericXmlSecurityToken.ExternalTokenReference != null)
				{
					xml.WriteElementString("ExternalTokenReference", genericXmlSecurityToken.ExternalTokenReference.ToString());
				}
				xml.WriteElementString("IssuedTokenElementName", genericXmlSecurityToken.TokenXml.LocalName);
				xml.WriteElementString("IssuedTokenElementNamespace", genericXmlSecurityToken.TokenXml.NamespaceURI);
			}
		}

		// Token: 0x06006B03 RID: 27395 RVA: 0x0018EBAE File Offset: 0x0018CDAE
		private static void WriteSecurityContextToken(XmlWriter xml, SecurityContextSecurityToken token)
		{
			xml.WriteElementString("ContextId", token.ContextId.ToString());
			if (token.KeyGeneration != null)
			{
				xml.WriteElementString("KeyGeneration", token.KeyGeneration.ToString());
			}
		}

		// Token: 0x06006B04 RID: 27396 RVA: 0x0018EBEC File Offset: 0x0018CDEC
		internal static void WriteClaim(XmlWriter xml, Claim claim)
		{
			if (xml == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("xml");
			}
			if (claim != null)
			{
				xml.WriteStartElement("Claim");
				if (DiagnosticUtility.DiagnosticTrace != null && DiagnosticUtility.DiagnosticTrace.TraceSource != null && DiagnosticUtility.DiagnosticTrace.ShouldLogPii)
				{
					xml.WriteElementString("ClaimType", claim.ClaimType);
					xml.WriteElementString("Right", claim.Right);
					if (claim.Resource != null)
					{
						xml.WriteElementString("ResourceType", claim.Resource.GetType().ToString());
					}
					else
					{
						xml.WriteElementString("Resource", "null");
					}
				}
				else
				{
					xml.WriteString(claim.GetType().AssemblyQualifiedName);
				}
				xml.WriteEndElement();
			}
		}

		// Token: 0x02000EAB RID: 3755
		private class SessionResponderTraceRecord : SecurityTraceRecord
		{
			// Token: 0x06008439 RID: 33849 RVA: 0x001E8A0F File Offset: 0x001E6C0F
			public SessionResponderTraceRecord(SecuritySessionOperation operation, Exception e, Uri listenAddress) : base("SecuritySession")
			{
				this.operation = operation;
				this.e = e;
				this.listenAddress = listenAddress;
			}

			// Token: 0x0600843A RID: 33850 RVA: 0x001E8A34 File Offset: 0x001E6C34
			internal override void WriteTo(XmlWriter xml)
			{
				if (xml == null)
				{
					return;
				}
				xml.WriteElementString("Operation", this.operation.ToString());
				if (this.e != null)
				{
					xml.WriteElementString("Exception", this.e.ToString());
				}
				if (this.listenAddress != null)
				{
					xml.WriteElementString("ListenAddress", this.listenAddress.ToString());
				}
			}

			// Token: 0x04004C3C RID: 19516
			private SecuritySessionOperation operation;

			// Token: 0x04004C3D RID: 19517
			private Exception e;

			// Token: 0x04004C3E RID: 19518
			private Uri listenAddress;
		}

		// Token: 0x02000EAC RID: 3756
		private class SessionRequestorTraceRecord : SecurityTraceRecord
		{
			// Token: 0x0600843B RID: 33851 RVA: 0x001E8AA3 File Offset: 0x001E6CA3
			public SessionRequestorTraceRecord(SecuritySessionOperation operation, SecurityToken currentToken, SecurityToken issuedToken, EndpointAddress target) : base("SecuritySession")
			{
				this.operation = operation;
				this.currentToken = currentToken;
				this.issuedToken = issuedToken;
				this.target = target;
			}

			// Token: 0x0600843C RID: 33852 RVA: 0x001E8ACD File Offset: 0x001E6CCD
			public SessionRequestorTraceRecord(SecuritySessionOperation operation, SecurityToken currentToken, Exception e, EndpointAddress target) : base("SecuritySession")
			{
				this.operation = operation;
				this.currentToken = currentToken;
				this.e = e;
				this.target = target;
			}

			// Token: 0x0600843D RID: 33853 RVA: 0x001E8AF8 File Offset: 0x001E6CF8
			internal override void WriteTo(XmlWriter xml)
			{
				if (xml == null)
				{
					return;
				}
				xml.WriteElementString("Operation", this.operation.ToString());
				if (this.currentToken != null)
				{
					SecurityTraceRecordHelper.WritePossibleGenericXmlToken(xml, "SupportingToken", this.currentToken);
				}
				if (this.issuedToken != null)
				{
					SecurityTraceRecordHelper.WritePossibleGenericXmlToken(xml, "IssuedToken", this.issuedToken);
				}
				if (this.e != null)
				{
					xml.WriteElementString("Exception", this.e.ToString());
				}
				if (this.target != null)
				{
					xml.WriteElementString("RemoteAddress", this.target.ToString());
				}
			}

			// Token: 0x04004C3F RID: 19519
			private SecuritySessionOperation operation;

			// Token: 0x04004C40 RID: 19520
			private SecurityToken currentToken;

			// Token: 0x04004C41 RID: 19521
			private SecurityToken issuedToken;

			// Token: 0x04004C42 RID: 19522
			private EndpointAddress target;

			// Token: 0x04004C43 RID: 19523
			private Exception e;
		}

		// Token: 0x02000EAD RID: 3757
		private class ServerSessionTraceRecord : SecurityTraceRecord
		{
			// Token: 0x0600843E RID: 33854 RVA: 0x001E8B99 File Offset: 0x001E6D99
			public ServerSessionTraceRecord(SecurityContextSecurityToken currentSessionToken, SecurityContextSecurityToken newSessionToken, Uri listenAddress) : base("SecuritySession")
			{
				this.currentSessionToken = currentSessionToken;
				this.newSessionToken = newSessionToken;
				this.listenAddress = listenAddress;
			}

			// Token: 0x0600843F RID: 33855 RVA: 0x001E8BBB File Offset: 0x001E6DBB
			public ServerSessionTraceRecord(SecurityContextSecurityToken currentSessionToken, Message message, Uri listenAddress) : base("SecuritySession")
			{
				this.currentSessionToken = currentSessionToken;
				this.message = message;
				this.listenAddress = listenAddress;
			}

			// Token: 0x06008440 RID: 33856 RVA: 0x001E8BDD File Offset: 0x001E6DDD
			public ServerSessionTraceRecord(SecurityContextSecurityToken currentSessionToken, Uri listenAddress) : base("SecuritySession")
			{
				this.currentSessionToken = currentSessionToken;
				this.listenAddress = listenAddress;
			}

			// Token: 0x06008441 RID: 33857 RVA: 0x001E8BF8 File Offset: 0x001E6DF8
			public ServerSessionTraceRecord(UniqueId sessionId, Uri listenAddress) : base("SecuritySession")
			{
				this.sessionId = sessionId;
				this.listenAddress = listenAddress;
			}

			// Token: 0x06008442 RID: 33858 RVA: 0x001E8C14 File Offset: 0x001E6E14
			internal override void WriteTo(XmlWriter xml)
			{
				if (xml == null)
				{
					return;
				}
				if (this.currentSessionToken != null)
				{
					xml.WriteStartElement("CurrentSessionToken");
					SecurityTraceRecordHelper.WriteSecurityContextToken(xml, this.currentSessionToken);
					xml.WriteEndElement();
				}
				if (this.newSessionToken != null)
				{
					xml.WriteStartElement("NewSessionToken");
					SecurityTraceRecordHelper.WriteSecurityContextToken(xml, this.newSessionToken);
					xml.WriteEndElement();
				}
				if (this.sessionId != null)
				{
					XmlHelper.WriteElementStringAsUniqueId(xml, "SessionId", this.sessionId);
				}
				if (this.message != null)
				{
					xml.WriteElementString("MessageAction", this.message.Headers.Action);
				}
				if (this.listenAddress != null)
				{
					xml.WriteElementString("ListenAddress", this.listenAddress.ToString());
				}
			}

			// Token: 0x04004C44 RID: 19524
			private SecurityContextSecurityToken currentSessionToken;

			// Token: 0x04004C45 RID: 19525
			private SecurityContextSecurityToken newSessionToken;

			// Token: 0x04004C46 RID: 19526
			private UniqueId sessionId;

			// Token: 0x04004C47 RID: 19527
			private Message message;

			// Token: 0x04004C48 RID: 19528
			private Uri listenAddress;
		}

		// Token: 0x02000EAE RID: 3758
		private class ClientSessionTraceRecord : SecurityTraceRecord
		{
			// Token: 0x06008443 RID: 33859 RVA: 0x001E8CD5 File Offset: 0x001E6ED5
			public ClientSessionTraceRecord(SecurityToken currentSessionToken, SecurityToken previousSessionToken, EndpointAddress remoteAddress) : base("SecuritySession")
			{
				this.currentSessionToken = currentSessionToken;
				this.previousSessionToken = previousSessionToken;
				this.remoteAddress = remoteAddress;
			}

			// Token: 0x06008444 RID: 33860 RVA: 0x001E8CF8 File Offset: 0x001E6EF8
			internal override void WriteTo(XmlWriter xml)
			{
				if (xml == null)
				{
					return;
				}
				if (this.remoteAddress != null)
				{
					xml.WriteElementString("RemoteAddress", this.remoteAddress.ToString());
				}
				if (this.currentSessionToken != null)
				{
					xml.WriteStartElement("CurrentSessionToken");
					SecurityTraceRecordHelper.WriteGenericXmlToken(xml, this.currentSessionToken);
					xml.WriteEndElement();
				}
				if (this.previousSessionToken != null)
				{
					xml.WriteStartElement("PreviousSessionToken");
					SecurityTraceRecordHelper.WriteGenericXmlToken(xml, this.previousSessionToken);
					xml.WriteEndElement();
				}
			}

			// Token: 0x04004C49 RID: 19529
			private SecurityToken currentSessionToken;

			// Token: 0x04004C4A RID: 19530
			private SecurityToken previousSessionToken;

			// Token: 0x04004C4B RID: 19531
			private EndpointAddress remoteAddress;
		}

		// Token: 0x02000EAF RID: 3759
		private class SessionRedirectAppliedTraceRecord : SecurityTraceRecord
		{
			// Token: 0x06008445 RID: 33861 RVA: 0x001E8D77 File Offset: 0x001E6F77
			public SessionRedirectAppliedTraceRecord(EndpointAddress previousTarget, EndpointAddress newTarget, GenericXmlSecurityToken sessionToken) : base("SecuritySession")
			{
				this.previousTarget = previousTarget;
				this.newTarget = newTarget;
				this.sessionToken = sessionToken;
			}

			// Token: 0x06008446 RID: 33862 RVA: 0x001E8D9C File Offset: 0x001E6F9C
			internal override void WriteTo(XmlWriter xml)
			{
				if (xml == null)
				{
					return;
				}
				if (this.previousTarget != null)
				{
					xml.WriteElementString("OriginalRemoteAddress", this.previousTarget.ToString());
				}
				if (this.newTarget != null)
				{
					xml.WriteElementString("NewRemoteAddress", this.newTarget.ToString());
				}
				if (this.sessionToken != null)
				{
					xml.WriteStartElement("SessionToken");
					SecurityTraceRecordHelper.WriteGenericXmlToken(xml, this.sessionToken);
					xml.WriteEndElement();
				}
			}

			// Token: 0x04004C4C RID: 19532
			private EndpointAddress previousTarget;

			// Token: 0x04004C4D RID: 19533
			private EndpointAddress newTarget;

			// Token: 0x04004C4E RID: 19534
			private GenericXmlSecurityToken sessionToken;
		}

		// Token: 0x02000EB0 RID: 3760
		private class SpnToSidMappingTraceRecord : SecurityTraceRecord
		{
			// Token: 0x06008447 RID: 33863 RVA: 0x001E8E1A File Offset: 0x001E701A
			public SpnToSidMappingTraceRecord(string spn, Exception e) : base("SecurityIdentity")
			{
				this.spn = spn;
				this.e = e;
			}

			// Token: 0x06008448 RID: 33864 RVA: 0x001E8E35 File Offset: 0x001E7035
			internal override void WriteTo(XmlWriter xml)
			{
				if (xml == null)
				{
					return;
				}
				if (this.spn != null)
				{
					xml.WriteElementString("ServicePrincipalName", this.spn);
				}
				if (this.e != null)
				{
					xml.WriteElementString("Exception", this.e.ToString());
				}
			}

			// Token: 0x04004C4F RID: 19535
			private string spn;

			// Token: 0x04004C50 RID: 19536
			private Exception e;
		}

		// Token: 0x02000EB1 RID: 3761
		private class MessageSecurityTraceRecord : SecurityTraceRecord
		{
			// Token: 0x06008449 RID: 33865 RVA: 0x001E8E72 File Offset: 0x001E7072
			public MessageSecurityTraceRecord(SecurityProtocol binding, Message message) : base("SecurityProtocol")
			{
				this.binding = binding;
				this.message = message;
			}

			// Token: 0x0600844A RID: 33866 RVA: 0x001E8E90 File Offset: 0x001E7090
			internal override void WriteTo(XmlWriter xml)
			{
				if (xml == null)
				{
					return;
				}
				if (this.binding != null)
				{
					xml.WriteElementString("SecurityProtocol", this.binding.ToString());
				}
				if (this.message != null)
				{
					string action = this.message.Headers.Action;
					Uri to = this.message.Headers.To;
					EndpointAddress replyTo = this.message.Headers.ReplyTo;
					UniqueId messageId = this.message.Headers.MessageId;
					if (!string.IsNullOrEmpty(action))
					{
						xml.WriteElementString("Action", action);
					}
					if (to != null)
					{
						xml.WriteElementString("To", to.AbsoluteUri);
					}
					if (replyTo != null)
					{
						replyTo.WriteTo(this.message.Version.Addressing, xml);
					}
					if (messageId != null)
					{
						xml.WriteElementString("MessageId", messageId.ToString());
						return;
					}
				}
				else
				{
					xml.WriteElementString("Message", "null");
				}
			}

			// Token: 0x04004C51 RID: 19537
			private SecurityProtocol binding;

			// Token: 0x04004C52 RID: 19538
			private Message message;
		}

		// Token: 0x02000EB2 RID: 3762
		private class TokenProviderTraceRecord : SecurityTraceRecord
		{
			// Token: 0x0600844B RID: 33867 RVA: 0x001E8F87 File Offset: 0x001E7187
			public TokenProviderTraceRecord(SecurityTokenProvider provider) : base("SecurityTokenProvider")
			{
				this.provider = provider;
			}

			// Token: 0x0600844C RID: 33868 RVA: 0x001E8F9B File Offset: 0x001E719B
			internal override void WriteTo(XmlWriter xml)
			{
				if (xml == null)
				{
					return;
				}
				if (this.provider != null)
				{
					xml.WriteElementString("SecurityTokenProvider", this.provider.ToString());
				}
			}

			// Token: 0x04004C53 RID: 19539
			private SecurityTokenProvider provider;
		}

		// Token: 0x02000EB3 RID: 3763
		private class TokenAuthenticatorTraceRecord : SecurityTraceRecord
		{
			// Token: 0x0600844D RID: 33869 RVA: 0x001E8FBF File Offset: 0x001E71BF
			public TokenAuthenticatorTraceRecord(SecurityTokenAuthenticator authenticator) : base("SecurityTokenAuthenticator")
			{
				this.authenticator = authenticator;
			}

			// Token: 0x0600844E RID: 33870 RVA: 0x001E8FD3 File Offset: 0x001E71D3
			internal override void WriteTo(XmlWriter xml)
			{
				if (xml == null)
				{
					return;
				}
				if (this.authenticator != null)
				{
					xml.WriteElementString("SecurityTokenAuthenticator", this.authenticator.ToString());
				}
			}

			// Token: 0x04004C54 RID: 19540
			private SecurityTokenAuthenticator authenticator;
		}

		// Token: 0x02000EB4 RID: 3764
		private class SecurityContextTokenCacheTraceRecord : SecurityTraceRecord
		{
			// Token: 0x0600844F RID: 33871 RVA: 0x001E8FF7 File Offset: 0x001E71F7
			public SecurityContextTokenCacheTraceRecord(int capacity, int pruningAmount) : base("ServiceSecurityNegotiation")
			{
				this.capacity = capacity;
				this.pruningAmount = pruningAmount;
			}

			// Token: 0x06008450 RID: 33872 RVA: 0x001E9012 File Offset: 0x001E7212
			internal override void WriteTo(XmlWriter xml)
			{
				if (xml == null)
				{
					return;
				}
				xml.WriteElementString("Capacity", this.capacity.ToString(NumberFormatInfo.InvariantInfo));
				xml.WriteElementString("PruningAmount", this.pruningAmount.ToString(NumberFormatInfo.InvariantInfo));
			}

			// Token: 0x04004C55 RID: 19541
			private int capacity;

			// Token: 0x04004C56 RID: 19542
			private int pruningAmount;
		}

		// Token: 0x02000EB5 RID: 3765
		private class NegotiationAuthenticatorTraceRecord<T> : SecurityTraceRecord where T : NegotiationTokenAuthenticatorState
		{
			// Token: 0x06008451 RID: 33873 RVA: 0x001E904E File Offset: 0x001E724E
			public NegotiationAuthenticatorTraceRecord(NegotiationTokenAuthenticator<T> authenticator, IChannelListener transportChannelListener) : base("NegotiationTokenAuthenticator")
			{
				this.authenticator = authenticator;
				this.transportChannelListener = transportChannelListener;
			}

			// Token: 0x06008452 RID: 33874 RVA: 0x001E9069 File Offset: 0x001E7269
			public NegotiationAuthenticatorTraceRecord(NegotiationTokenAuthenticator<T> authenticator, Exception e) : base("NegotiationTokenAuthenticator")
			{
				this.authenticator = authenticator;
				this.e = e;
			}

			// Token: 0x06008453 RID: 33875 RVA: 0x001E9084 File Offset: 0x001E7284
			public NegotiationAuthenticatorTraceRecord(NegotiationTokenAuthenticator<T> authenticator, SecurityContextSecurityToken serviceToken) : base("NegotiationTokenAuthenticator")
			{
				this.authenticator = authenticator;
				this.serviceToken = serviceToken;
			}

			// Token: 0x06008454 RID: 33876 RVA: 0x001E90A0 File Offset: 0x001E72A0
			internal override void WriteTo(XmlWriter xml)
			{
				if (xml == null)
				{
					return;
				}
				if (this.authenticator != null)
				{
					xml.WriteElementString("NegotiationTokenAuthenticator", base.XmlEncode(this.authenticator.ToString()));
				}
				if (this.authenticator != null && this.authenticator.ListenUri != null)
				{
					xml.WriteElementString("AuthenticatorListenUri", this.authenticator.ListenUri.AbsoluteUri);
				}
				if (this.serviceToken != null)
				{
					xml.WriteStartElement("SecurityContextSecurityToken");
					SecurityTraceRecordHelper.WriteSecurityContextToken(xml, this.serviceToken);
					xml.WriteEndElement();
				}
				if (this.transportChannelListener != null)
				{
					xml.WriteElementString("TransportChannelListener", base.XmlEncode(this.transportChannelListener.ToString()));
					if (this.transportChannelListener.Uri != null)
					{
						xml.WriteElementString("ListenUri", this.transportChannelListener.Uri.AbsoluteUri);
					}
				}
				if (this.e != null)
				{
					xml.WriteElementString("Exception", base.XmlEncode(this.e.ToString()));
				}
			}

			// Token: 0x04004C57 RID: 19543
			private NegotiationTokenAuthenticator<T> authenticator;

			// Token: 0x04004C58 RID: 19544
			private IChannelListener transportChannelListener;

			// Token: 0x04004C59 RID: 19545
			private SecurityContextSecurityToken serviceToken;

			// Token: 0x04004C5A RID: 19546
			private Exception e;
		}

		// Token: 0x02000EB6 RID: 3766
		private class IdentityVerificationSuccessTraceRecord : SecurityTraceRecord
		{
			// Token: 0x06008455 RID: 33877 RVA: 0x001E91A6 File Offset: 0x001E73A6
			public IdentityVerificationSuccessTraceRecord(EndpointIdentity identity, Claim claim, Type identityVerifier) : base("ServiceIdentityVerification")
			{
				this.identity = identity;
				this.claim = claim;
				this.identityVerifier = identityVerifier;
			}

			// Token: 0x06008456 RID: 33878 RVA: 0x001E91C8 File Offset: 0x001E73C8
			internal override void WriteTo(XmlWriter xml)
			{
				if (xml == null)
				{
					return;
				}
				XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateDictionaryWriter(xml);
				if (this.identityVerifier != null)
				{
					xml.WriteElementString("IdentityVerifierType", this.identityVerifier.ToString());
				}
				if (this.identity != null)
				{
					this.identity.WriteTo(xmlDictionaryWriter);
				}
				if (this.claim != null)
				{
					SecurityTraceRecordHelper.WriteClaim(xmlDictionaryWriter, this.claim);
				}
			}

			// Token: 0x04004C5B RID: 19547
			private EndpointIdentity identity;

			// Token: 0x04004C5C RID: 19548
			private Claim claim;

			// Token: 0x04004C5D RID: 19549
			private Type identityVerifier;
		}

		// Token: 0x02000EB7 RID: 3767
		private class IdentityVerificationFailureTraceRecord : SecurityTraceRecord
		{
			// Token: 0x06008457 RID: 33879 RVA: 0x001E922C File Offset: 0x001E742C
			public IdentityVerificationFailureTraceRecord(EndpointIdentity identity, AuthorizationContext authContext, Type identityVerifier) : base("ServiceIdentityVerification")
			{
				this.identity = identity;
				this.authContext = authContext;
				this.identityVerifier = identityVerifier;
			}

			// Token: 0x06008458 RID: 33880 RVA: 0x001E9250 File Offset: 0x001E7450
			internal override void WriteTo(XmlWriter xml)
			{
				if (xml == null)
				{
					return;
				}
				XmlDictionaryWriter writer = XmlDictionaryWriter.CreateDictionaryWriter(xml);
				if (this.identityVerifier != null)
				{
					xml.WriteElementString("IdentityVerifierType", this.identityVerifier.ToString());
				}
				if (this.identity != null)
				{
					this.identity.WriteTo(writer);
				}
				if (this.authContext != null)
				{
					for (int i = 0; i < this.authContext.ClaimSets.Count; i++)
					{
						ClaimSet claimSet = this.authContext.ClaimSets[i];
						if (this.authContext.ClaimSets[i] != null)
						{
							for (int j = 0; j < claimSet.Count; j++)
							{
								Claim claim = claimSet[j];
								if (claimSet[j] != null)
								{
									xml.WriteStartElement("Claim");
									if (claim.ClaimType != null)
									{
										xml.WriteElementString("ClaimType", claim.ClaimType);
									}
									else
									{
										xml.WriteElementString("ClaimType", "null");
									}
									if (claim.Right != null)
									{
										xml.WriteElementString("Right", claim.Right);
									}
									else
									{
										xml.WriteElementString("Right", "null");
									}
									if (claim.Resource != null)
									{
										xml.WriteElementString("ResourceType", claim.Resource.GetType().ToString());
									}
									else
									{
										xml.WriteElementString("Resource", "null");
									}
									xml.WriteEndElement();
								}
							}
						}
					}
				}
			}

			// Token: 0x04004C5E RID: 19550
			private EndpointIdentity identity;

			// Token: 0x04004C5F RID: 19551
			private AuthorizationContext authContext;

			// Token: 0x04004C60 RID: 19552
			private Type identityVerifier;
		}

		// Token: 0x02000EB8 RID: 3768
		private class IdentityDeterminationSuccessTraceRecord : SecurityTraceRecord
		{
			// Token: 0x06008459 RID: 33881 RVA: 0x001E93C2 File Offset: 0x001E75C2
			public IdentityDeterminationSuccessTraceRecord(EndpointAddress epr, EndpointIdentity identity, Type identityVerifier) : base("ServiceIdentityDetermination")
			{
				this.identity = identity;
				this.epr = epr;
				this.identityVerifier = identityVerifier;
			}

			// Token: 0x0600845A RID: 33882 RVA: 0x001E93E4 File Offset: 0x001E75E4
			internal override void WriteTo(XmlWriter xml)
			{
				if (xml == null)
				{
					return;
				}
				if (this.identityVerifier != null)
				{
					xml.WriteElementString("IdentityVerifierType", this.identityVerifier.ToString());
				}
				if (this.identity != null)
				{
					this.identity.WriteTo(XmlDictionaryWriter.CreateDictionaryWriter(xml));
				}
				if (this.epr != null)
				{
					this.epr.WriteTo(AddressingVersion.WSAddressing10, xml);
				}
			}

			// Token: 0x04004C61 RID: 19553
			private EndpointIdentity identity;

			// Token: 0x04004C62 RID: 19554
			private EndpointAddress epr;

			// Token: 0x04004C63 RID: 19555
			private Type identityVerifier;
		}

		// Token: 0x02000EB9 RID: 3769
		private class IdentityDeterminationFailureTraceRecord : SecurityTraceRecord
		{
			// Token: 0x0600845B RID: 33883 RVA: 0x001E9451 File Offset: 0x001E7651
			public IdentityDeterminationFailureTraceRecord(EndpointAddress epr, Type identityVerifier) : base("ServiceIdentityDetermination")
			{
				this.epr = epr;
				this.identityVerifier = identityVerifier;
			}

			// Token: 0x0600845C RID: 33884 RVA: 0x001E946C File Offset: 0x001E766C
			internal override void WriteTo(XmlWriter xml)
			{
				if (xml == null)
				{
					return;
				}
				if (this.identityVerifier != null)
				{
					xml.WriteElementString("IdentityVerifierType", this.identityVerifier.ToString());
				}
				if (this.epr != null)
				{
					this.epr.WriteTo(AddressingVersion.WSAddressing10, xml);
				}
			}

			// Token: 0x04004C64 RID: 19556
			private Type identityVerifier;

			// Token: 0x04004C65 RID: 19557
			private EndpointAddress epr;
		}

		// Token: 0x02000EBA RID: 3770
		private class IdentityHostNameNormalizationFailureTraceRecord : SecurityTraceRecord
		{
			// Token: 0x0600845D RID: 33885 RVA: 0x001E94C0 File Offset: 0x001E76C0
			public IdentityHostNameNormalizationFailureTraceRecord(EndpointAddress epr, Type identityVerifier, Exception e) : base("ServiceIdentityDetermination")
			{
				this.epr = epr;
				this.identityVerifier = identityVerifier;
				this.e = e;
			}

			// Token: 0x0600845E RID: 33886 RVA: 0x001E94E4 File Offset: 0x001E76E4
			internal override void WriteTo(XmlWriter xml)
			{
				if (xml == null)
				{
					return;
				}
				if (this.identityVerifier != null)
				{
					xml.WriteElementString("IdentityVerifierType", this.identityVerifier.ToString());
				}
				if (this.epr != null)
				{
					this.epr.WriteTo(AddressingVersion.WSAddressing10, xml);
				}
				if (this.e != null)
				{
					xml.WriteElementString("Exception", this.e.ToString());
				}
			}

			// Token: 0x04004C66 RID: 19558
			private Type identityVerifier;

			// Token: 0x04004C67 RID: 19559
			private Exception e;

			// Token: 0x04004C68 RID: 19560
			private EndpointAddress epr;
		}

		// Token: 0x02000EBB RID: 3771
		private class IssuanceProviderTraceRecord<T> : SecurityTraceRecord where T : IssuanceTokenProviderState
		{
			// Token: 0x0600845F RID: 33887 RVA: 0x001E9556 File Offset: 0x001E7756
			public IssuanceProviderTraceRecord(IssuanceTokenProviderBase<T> provider, SecurityToken serviceToken) : this(provider, serviceToken, null)
			{
			}

			// Token: 0x06008460 RID: 33888 RVA: 0x001E9561 File Offset: 0x001E7761
			public IssuanceProviderTraceRecord(IssuanceTokenProviderBase<T> provider, EndpointAddress target) : this(provider, null, target)
			{
			}

			// Token: 0x06008461 RID: 33889 RVA: 0x001E956C File Offset: 0x001E776C
			public IssuanceProviderTraceRecord(IssuanceTokenProviderBase<T> provider, SecurityToken serviceToken, EndpointAddress target) : base("ClientSecurityNegotiation")
			{
				this.provider = provider;
				this.serviceToken = serviceToken;
				this.target = target;
			}

			// Token: 0x06008462 RID: 33890 RVA: 0x001E958E File Offset: 0x001E778E
			public IssuanceProviderTraceRecord(IssuanceTokenProviderBase<T> provider, EndpointAddress newTarget, EndpointAddress oldTarget) : base("ClientSecurityNegotiation")
			{
				this.provider = provider;
				this.newTarget = newTarget;
				this.target = oldTarget;
			}

			// Token: 0x06008463 RID: 33891 RVA: 0x001E95B0 File Offset: 0x001E77B0
			public IssuanceProviderTraceRecord(IssuanceTokenProviderBase<T> provider, int cacheSize) : base("ClientSecurityNegotiation")
			{
				this.provider = provider;
				this.cacheSize = cacheSize;
			}

			// Token: 0x06008464 RID: 33892 RVA: 0x001E95CC File Offset: 0x001E77CC
			internal override void WriteTo(XmlWriter xml)
			{
				if (xml == null)
				{
					return;
				}
				if (this.provider != null)
				{
					xml.WriteElementString("IssuanceTokenProvider", this.provider.ToString());
				}
				if (this.serviceToken != null)
				{
					SecurityTraceRecordHelper.WritePossibleGenericXmlToken(xml, "ServiceToken", this.serviceToken);
				}
				if (this.target != null)
				{
					xml.WriteStartElement("Target");
					this.target.WriteTo(AddressingVersion.WSAddressing10, xml);
					xml.WriteEndElement();
				}
				if (this.newTarget != null)
				{
					xml.WriteStartElement("PinnedTarget");
					this.newTarget.WriteTo(AddressingVersion.WSAddressing10, xml);
					xml.WriteEndElement();
				}
				if (this.cacheSize != 0)
				{
					xml.WriteElementString("CacheSize", this.cacheSize.ToString(NumberFormatInfo.InvariantInfo));
				}
			}

			// Token: 0x04004C69 RID: 19561
			private IssuanceTokenProviderBase<T> provider;

			// Token: 0x04004C6A RID: 19562
			private EndpointAddress target;

			// Token: 0x04004C6B RID: 19563
			private EndpointAddress newTarget;

			// Token: 0x04004C6C RID: 19564
			private SecurityToken serviceToken;

			// Token: 0x04004C6D RID: 19565
			private int cacheSize;
		}

		// Token: 0x02000EBC RID: 3772
		private class WindowsSspiNegotiationTraceRecord : SecurityTraceRecord
		{
			// Token: 0x06008465 RID: 33893 RVA: 0x001E9697 File Offset: 0x001E7897
			public WindowsSspiNegotiationTraceRecord(WindowsSspiNegotiation windowsNegotiation) : base("SpnegoSecurityNegotiation")
			{
				this.windowsNegotiation = windowsNegotiation;
			}

			// Token: 0x06008466 RID: 33894 RVA: 0x001E96AC File Offset: 0x001E78AC
			internal override void WriteTo(XmlWriter xml)
			{
				if (xml == null)
				{
					return;
				}
				if (this.windowsNegotiation != null)
				{
					xml.WriteElementString("Protocol", this.windowsNegotiation.ProtocolName);
					xml.WriteElementString("ServicePrincipalName", this.windowsNegotiation.ServicePrincipalName);
					xml.WriteElementString("MutualAuthentication", this.windowsNegotiation.IsMutualAuthFlag.ToString());
					if (this.windowsNegotiation.IsIdentifyFlag)
					{
						xml.WriteElementString("ImpersonationLevel", "Identify");
						return;
					}
					if (this.windowsNegotiation.IsDelegationFlag)
					{
						xml.WriteElementString("ImpersonationLevel", "Delegate");
						return;
					}
					xml.WriteElementString("ImpersonationLevel", "Impersonate");
				}
			}

			// Token: 0x04004C6E RID: 19566
			private WindowsSspiNegotiation windowsNegotiation;
		}

		// Token: 0x02000EBD RID: 3773
		private class ImpersonationTraceRecord : SecurityTraceRecord
		{
			// Token: 0x06008467 RID: 33895 RVA: 0x001E975E File Offset: 0x001E795E
			internal ImpersonationTraceRecord(DispatchOperationRuntime operation) : base("SecurityImpersonation")
			{
				this.operation = operation;
			}

			// Token: 0x06008468 RID: 33896 RVA: 0x001E9772 File Offset: 0x001E7972
			internal override void WriteTo(XmlWriter xml)
			{
				if (xml == null)
				{
					return;
				}
				if (this.operation != null)
				{
					xml.WriteElementString("OperationAction", this.operation.Action);
					xml.WriteElementString("OperationName", this.operation.Name);
				}
			}

			// Token: 0x04004C6F RID: 19567
			private DispatchOperationRuntime operation;
		}
	}
}
