using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ServiceModel.PeerResolvers;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A09 RID: 2569
	internal class PeerExceptionHelper
	{
		// Token: 0x060065BE RID: 26046 RVA: 0x0017B3B5 File Offset: 0x001795B5
		internal static void ThrowInvalidOperation_InsufficientCryptoSupport(Exception innerException)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InsufficientCryptoSupport"), innerException));
		}

		// Token: 0x060065BF RID: 26047 RVA: 0x0017B3D1 File Offset: 0x001795D1
		internal static void ThrowArgument_InsufficientCredentials(string property)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("InsufficientCredentials", new object[]
			{
				property
			})));
		}

		// Token: 0x060065C0 RID: 26048 RVA: 0x0017B3F6 File Offset: 0x001795F6
		internal static void ThrowArgumentOutOfRange_InvalidTransportCredentialType(int value)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("CredentialType", value, SR.GetString("ValueMustBeInRange", new object[]
			{
				PeerTransportCredentialType.Password,
				PeerTransportCredentialType.Certificate
			})));
		}

		// Token: 0x060065C1 RID: 26049 RVA: 0x0017B434 File Offset: 0x00179634
		internal static void ThrowArgumentOutOfRange_InvalidSecurityMode(int value)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("Mode", value, SR.GetString("ValueMustBeInRange", new object[]
			{
				SecurityMode.None,
				SecurityMode.TransportWithMessageCredential
			})));
		}

		// Token: 0x060065C2 RID: 26050 RVA: 0x0017B472 File Offset: 0x00179672
		internal static void ThrowInvalidOperation_UnexpectedSecurityTokensDuringHandshake()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnexpectedSecurityTokensDuringHandshake")));
		}

		// Token: 0x060065C3 RID: 26051 RVA: 0x0017B48D File Offset: 0x0017968D
		internal static void ThrowArgument_PnrpAddressesExceedLimit()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("PnrpAddressesExceedLimit")));
		}

		// Token: 0x060065C4 RID: 26052 RVA: 0x0017B4A8 File Offset: 0x001796A8
		internal static void ThrowInvalidOperation_PnrpNoClouds()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PnrpNoClouds")));
		}

		// Token: 0x060065C5 RID: 26053 RVA: 0x0017B4C3 File Offset: 0x001796C3
		internal static void ThrowInvalidOperation_PnrpAddressesUnsupported()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PnrpAddressesUnsupported")));
		}

		// Token: 0x060065C6 RID: 26054 RVA: 0x0017B4DE File Offset: 0x001796DE
		internal static void ThrowArgument_InsufficientResolverSettings()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("InsufficientResolverSettings")));
		}

		// Token: 0x060065C7 RID: 26055 RVA: 0x0017B4F9 File Offset: 0x001796F9
		internal static void ThrowArgument_MustOverrideInitialize()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MustOverrideInitialize")));
		}

		// Token: 0x060065C8 RID: 26056 RVA: 0x0017B514 File Offset: 0x00179714
		internal static void ThrowArgument_InvalidResolverMode(PeerResolverMode mode)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("InvalidResolverMode", new object[]
			{
				mode
			})));
		}

		// Token: 0x060065C9 RID: 26057 RVA: 0x0017B53E File Offset: 0x0017973E
		internal static void ThrowInvalidOperation_NotValidWhenOpen(string operation)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NotValidWhenOpen", new object[]
			{
				operation
			})));
		}

		// Token: 0x060065CA RID: 26058 RVA: 0x0017B563 File Offset: 0x00179763
		internal static void ThrowInvalidOperation_NotValidWhenClosed(string operation)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NotValidWhenClosed", new object[]
			{
				operation
			})));
		}

		// Token: 0x060065CB RID: 26059 RVA: 0x0017B588 File Offset: 0x00179788
		internal static void ThrowInvalidOperation_DuplicatePeerRegistration(string servicepath)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("DuplicatePeerRegistration", new object[]
			{
				servicepath
			})));
		}

		// Token: 0x060065CC RID: 26060 RVA: 0x0017B5AD File Offset: 0x001797AD
		internal static void ThrowPnrpError(int errorCode, string cloud)
		{
			PeerExceptionHelper.ThrowPnrpError(errorCode, cloud, true);
		}

		// Token: 0x060065CD RID: 26061 RVA: 0x0017B5B7 File Offset: 0x001797B7
		internal static void ThrowPnrpError(int errorCode, string cloud, bool trace)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelper(new PnrpPeerResolver.PnrpException(errorCode, cloud), trace ? TraceEventType.Error : TraceEventType.Information);
		}

		// Token: 0x060065CE RID: 26062 RVA: 0x0017B5D1 File Offset: 0x001797D1
		internal static void ThrowInvalidOperation_PeerConflictingPeerNodeSettings(string propertyName)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PeerConflictingPeerNodeSettings", new object[]
			{
				propertyName
			})));
		}

		// Token: 0x060065CF RID: 26063 RVA: 0x0017B5F6 File Offset: 0x001797F6
		internal static void ThrowInvalidOperation_PeerCertGenFailure(Exception innerException)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PeerCertGenFailure"), innerException));
		}

		// Token: 0x060065D0 RID: 26064 RVA: 0x0017B612 File Offset: 0x00179812
		internal static void ThrowInvalidOperation_ConflictingHeader(string headerName)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PeerConflictingHeader", new object[]
			{
				headerName,
				"http://schemas.microsoft.com/net/2006/05/peer"
			})));
		}

		// Token: 0x060065D1 RID: 26065 RVA: 0x0017B63F File Offset: 0x0017983F
		public static Exception GetLastException()
		{
			return new Win32Exception(Marshal.GetLastWin32Error());
		}
	}
}
