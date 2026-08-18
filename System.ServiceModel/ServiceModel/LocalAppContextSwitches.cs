using System;
using System.Runtime.CompilerServices;

namespace System.ServiceModel
{
	// Token: 0x02000030 RID: 48
	internal static class LocalAppContextSwitches
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00008A8F File Offset: 0x00006C8F
		public static bool AlwaysTryCreateNamedPipeInGlobalNamespace
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.ServiceModel.AlwaysTryCreateNamedPipeInGlobalNamespace", ref LocalAppContextSwitches.alwaysTryCreateNamedPipeInGlobalNamespace);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00008AA0 File Offset: 0x00006CA0
		public static bool DontEnableSystemDefaultTlsVersions
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.ServiceModel.DontEnableSystemDefaultTlsVersions", ref LocalAppContextSwitches.dontEnableSystemDefaultTlsVersions);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00008AB1 File Offset: 0x00006CB1
		public static bool UseSha1InMsmqEncryptionAlgorithm
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.ServiceModel.UseSha1InMsmqEncryptionAlgorithm", ref LocalAppContextSwitches.useSha1InMsmqEncryptionAlgorithm);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00008AC2 File Offset: 0x00006CC2
		public static bool DisableAddressHeaderCollectionValidation
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.ServiceModel.DisableAddressHeaderCollectionValidation", ref LocalAppContextSwitches.disableAddressHeaderCollectionValidation);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00008AD3 File Offset: 0x00006CD3
		public static bool UseSha1InPipeConnectionGetHashAlgorithm
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.ServiceModel.UseSha1InPipeConnectionGetHashAlgorithm", ref LocalAppContextSwitches.useSha1InPipeConnectionGetHashAlgorithm);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00008AE4 File Offset: 0x00006CE4
		public static bool DisableExplicitConnectionCloseHeader
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.ServiceModel.DisableExplicitConnectionCloseHeader", ref LocalAppContextSwitches.disableExplicitConnectionCloseHeader);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00008AF5 File Offset: 0x00006CF5
		public static bool AllowUnsignedToHeader
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.ServiceModel.AllowUnsignedToHeader", ref LocalAppContextSwitches.allowUnsignedToHeader);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00008B06 File Offset: 0x00006D06
		public static bool DisableCngCertificates
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.ServiceModel.DisableCngCertificates", ref LocalAppContextSwitches.disableCngCertificates);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00008B17 File Offset: 0x00006D17
		public static bool DisableUsingServicePointManagerSecurityProtocols
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.ServiceModel.DisableUsingServicePointManagerSecurityProtocols", ref LocalAppContextSwitches.disableUsingServicePointManagerSecurityProtocols);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00008B28 File Offset: 0x00006D28
		public static bool AllowMultipleStandardSoapHeaders
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.ServiceModel.AllowMultipleStandardSoapHeaders", ref LocalAppContextSwitches.allowMultipleStandardSoapHeaders);
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00008B39 File Offset: 0x00006D39
		public static void SetDefaultsLessOrEqual_452()
		{
			LocalAppContext.DefineSwitchDefault("Switch.System.ServiceModel.DisableExplicitConnectionCloseHeader", true);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00008B46 File Offset: 0x00006D46
		public static void SetDefaultsLessOrEqual_461()
		{
			LocalAppContext.DefineSwitchDefault("Switch.System.ServiceModel.DisableCngCertificates", true);
		}

		// Token: 0x0400018E RID: 398
		internal const string DisableExplicitConnectionCloseHeaderString = "Switch.System.ServiceModel.DisableExplicitConnectionCloseHeader";

		// Token: 0x0400018F RID: 399
		internal const string AllowUnsignedToHeaderString = "Switch.System.ServiceModel.AllowUnsignedToHeader";

		// Token: 0x04000190 RID: 400
		internal const string DisableCngCertificatesString = "Switch.System.ServiceModel.DisableCngCertificates";

		// Token: 0x04000191 RID: 401
		internal const string DisableUsingServicePointManagerSecurityProtocolsString = "Switch.System.ServiceModel.DisableUsingServicePointManagerSecurityProtocols";

		// Token: 0x04000192 RID: 402
		internal const string UseSha1InPipeConnectionGetHashAlgorithmString = "Switch.System.ServiceModel.UseSha1InPipeConnectionGetHashAlgorithm";

		// Token: 0x04000193 RID: 403
		internal const string DisableAddressHeaderCollectionValidationString = "Switch.System.ServiceModel.DisableAddressHeaderCollectionValidation";

		// Token: 0x04000194 RID: 404
		internal const string UseSha1InMsmqEncryptionAlgorithmString = "Switch.System.ServiceModel.UseSha1InMsmqEncryptionAlgorithm";

		// Token: 0x04000195 RID: 405
		internal const string DontEnableSystemDefaultTlsVersionsString = "Switch.System.ServiceModel.DontEnableSystemDefaultTlsVersions";

		// Token: 0x04000196 RID: 406
		internal const string AlwaysTryCreateNamedPipeInGlobalNamespaceString = "Switch.System.ServiceModel.AlwaysTryCreateNamedPipeInGlobalNamespace";

		// Token: 0x04000197 RID: 407
		internal const string AllowMultipleStandardSoapHeadersString = "Switch.System.ServiceModel.AllowMultipleStandardSoapHeaders";

		// Token: 0x04000198 RID: 408
		private static int disableExplicitConnectionCloseHeader;

		// Token: 0x04000199 RID: 409
		private static int allowUnsignedToHeader;

		// Token: 0x0400019A RID: 410
		private static int disableCngCertificates;

		// Token: 0x0400019B RID: 411
		private static int disableUsingServicePointManagerSecurityProtocols;

		// Token: 0x0400019C RID: 412
		private static int useSha1InPipeConnectionGetHashAlgorithm;

		// Token: 0x0400019D RID: 413
		private static int disableAddressHeaderCollectionValidation;

		// Token: 0x0400019E RID: 414
		private static int useSha1InMsmqEncryptionAlgorithm;

		// Token: 0x0400019F RID: 415
		private static int dontEnableSystemDefaultTlsVersions;

		// Token: 0x040001A0 RID: 416
		private static int alwaysTryCreateNamedPipeInGlobalNamespace;

		// Token: 0x040001A1 RID: 417
		private static int allowMultipleStandardSoapHeaders;
	}
}
