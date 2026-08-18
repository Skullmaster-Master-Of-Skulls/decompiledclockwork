using System;
using System.Runtime.CompilerServices;

namespace System.IdentityModel
{
	// Token: 0x0200004D RID: 77
	internal static class LocalAppContextSwitches
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000BA3D File Offset: 0x00009C3D
		public static bool EnableCachedEmptyDefaultAuthorizationContext
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.IdentityModel.EnableCachedEmptyDefaultAuthorizationContext", ref LocalAppContextSwitches.enableCachedEmptyDefaultAuthorizationContext);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000BA4E File Offset: 0x00009C4E
		public static bool DisableMultipleDNSEntriesInSANCertificate
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.IdentityModel.DisableMultipleDNSEntriesInSANCertificate", ref LocalAppContextSwitches.disableMultipleDNSEntriesInSANCertificate);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000BA5F File Offset: 0x00009C5F
		public static bool DisableUpdatingRsaProviderType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.IdentityModel.DisableUpdatingRsaProviderType", ref LocalAppContextSwitches.disableUpdatingRsaProviderType);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000BA70 File Offset: 0x00009C70
		public static bool DisableCngCertificates
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.IdentityModel.DisableCngCertificates", ref LocalAppContextSwitches.disableCngCertificatesString);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000BA81 File Offset: 0x00009C81
		public static bool ProcessMultipleSecurityKeyIdentifierClauses
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.IdentityModel.ProcessMultipleSecurityKeyIdentifierClauses", ref LocalAppContextSwitches.processMultipleSecurityKeyIdentifierClauses);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000BA92 File Offset: 0x00009C92
		public static bool ReturnMultipleSecurityKeyIdentifierClauses
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.IdentityModel.ReturnMultipleSecurityKeyIdentifierClauses", ref LocalAppContextSwitches.returnMultipleSecurityKeyIdentifierClauses);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000BAA3 File Offset: 0x00009CA3
		public static bool PassUnfilteredAlgorithmsToCryptoConfig
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.IdentityModel.PassUnfilteredAlgorithmsToCryptoConfig", ref LocalAppContextSwitches.passUnfilteredAlgorithmsToCryptoConfig);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000BAB4 File Offset: 0x00009CB4
		public static bool AllowUnlimitedXmlTransforms
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.IdentityModel.AllowUnlimitedXmlTransforms", ref LocalAppContextSwitches.allowUnlimitedXmlTransforms);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000BAC5 File Offset: 0x00009CC5
		public static bool AllowUnlimitedXmlReferences
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.IdentityModel.AllowUnlimitedXmlReferences", ref LocalAppContextSwitches.allowUnlimitedXmlReferences);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000BAD6 File Offset: 0x00009CD6
		public static bool AllowUnlimitedXmlRecursion
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.IdentityModel.AllowUnlimitedXmlRecursion", ref LocalAppContextSwitches.allowUnlimitedXmlRecursion);
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000BAE7 File Offset: 0x00009CE7
		public static void SetDefaultsLessOrEqual_452()
		{
			LocalAppContext.DefineSwitchDefault("Switch.System.IdentityModel.EnableCachedEmptyDefaultAuthorizationContext", true);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000BAF4 File Offset: 0x00009CF4
		public static void SetDefaultsLessOrEqual_46()
		{
			LocalAppContext.DefineSwitchDefault("Switch.System.IdentityModel.DisableMultipleDNSEntriesInSANCertificate", true);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000BB01 File Offset: 0x00009D01
		public static void SetDefaultsLessOrEqual_462()
		{
			LocalAppContext.DefineSwitchDefault("Switch.System.IdentityModel.DisableCngCertificates", true);
		}

		// Token: 0x0400029B RID: 667
		private const string EnableCachedEmptyDefaultAuthorizationContextString = "Switch.System.IdentityModel.EnableCachedEmptyDefaultAuthorizationContext";

		// Token: 0x0400029C RID: 668
		private const string DisableMultipleDNSEntriesInSANCertificateString = "Switch.System.IdentityModel.DisableMultipleDNSEntriesInSANCertificate";

		// Token: 0x0400029D RID: 669
		private const string DisableUpdatingRsaProviderTypeString = "Switch.System.IdentityModel.DisableUpdatingRsaProviderType";

		// Token: 0x0400029E RID: 670
		private const string DisableCngCertificatesString = "Switch.System.IdentityModel.DisableCngCertificates";

		// Token: 0x0400029F RID: 671
		private const string ProcessMultipleSecurityKeyIdentifierClausesString = "Switch.System.IdentityModel.ProcessMultipleSecurityKeyIdentifierClauses";

		// Token: 0x040002A0 RID: 672
		private const string ReturnMultipleSecurityKeyIdentifierClausesString = "Switch.System.IdentityModel.ReturnMultipleSecurityKeyIdentifierClauses";

		// Token: 0x040002A1 RID: 673
		private const string PassUnfilteredAlgorithmsToCryptoConfigString = "Switch.System.IdentityModel.PassUnfilteredAlgorithmsToCryptoConfig";

		// Token: 0x040002A2 RID: 674
		private const string AllowUnlimitedXmlTransformsString = "Switch.System.IdentityModel.AllowUnlimitedXmlTransforms";

		// Token: 0x040002A3 RID: 675
		private const string AllowUnlimitedXmlReferencesString = "Switch.System.IdentityModel.AllowUnlimitedXmlReferences";

		// Token: 0x040002A4 RID: 676
		private const string AllowUnlimitedXmlRecursionString = "Switch.System.IdentityModel.AllowUnlimitedXmlRecursion";

		// Token: 0x040002A5 RID: 677
		private static int enableCachedEmptyDefaultAuthorizationContext;

		// Token: 0x040002A6 RID: 678
		private static int disableMultipleDNSEntriesInSANCertificate;

		// Token: 0x040002A7 RID: 679
		private static int disableUpdatingRsaProviderType;

		// Token: 0x040002A8 RID: 680
		private static int disableCngCertificatesString;

		// Token: 0x040002A9 RID: 681
		private static int processMultipleSecurityKeyIdentifierClauses;

		// Token: 0x040002AA RID: 682
		private static int returnMultipleSecurityKeyIdentifierClauses;

		// Token: 0x040002AB RID: 683
		private static int passUnfilteredAlgorithmsToCryptoConfig;

		// Token: 0x040002AC RID: 684
		private static int allowUnlimitedXmlTransforms;

		// Token: 0x040002AD RID: 685
		private static int allowUnlimitedXmlReferences;

		// Token: 0x040002AE RID: 686
		private static int allowUnlimitedXmlRecursion;
	}
}
