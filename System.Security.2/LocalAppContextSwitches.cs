using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000005 RID: 5
	internal static class LocalAppContextSwitches
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static bool XmlUseInsecureHashAlgorithms
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue(LocalAppContextSwitches.SwitchXmlUseInsecureHashAlgorithms, ref LocalAppContextSwitches._xmlUseInsecureHashAlgorithms);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002061 File Offset: 0x00000261
		public static bool SignedXmlUseLegacyCertificatePrivateKey
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue(LocalAppContextSwitches.SwitchSignedXmlUseLegacyCertificatePrivateKey, ref LocalAppContextSwitches._signedXmlUseLegacyCertificatePrivateKey);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002072 File Offset: 0x00000272
		public static bool CmsUseInsecureHashAlgorithms
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue(LocalAppContextSwitches.SwitchCmsUseInsecureHashAlgorithms, ref LocalAppContextSwitches._cmsUseInsecureHashAlgorithms);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002083 File Offset: 0x00000283
		public static bool EnvelopedCmsUseLegacyDefaultAlgorithm
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue(LocalAppContextSwitches.SwitchEnvelopedCmsUseLegacyDefaultAlgorithm, ref LocalAppContextSwitches._envelopedCmsUseLegacyDefaultAlgorithm);
			}
		}

		// Token: 0x04000050 RID: 80
		private static int _xmlUseInsecureHashAlgorithms;

		// Token: 0x04000051 RID: 81
		internal static readonly string SwitchXmlUseInsecureHashAlgorithms = "Switch.System.Security.Cryptography.Xml.UseInsecureHashAlgorithms";

		// Token: 0x04000052 RID: 82
		private static int _signedXmlUseLegacyCertificatePrivateKey;

		// Token: 0x04000053 RID: 83
		internal static readonly string SwitchSignedXmlUseLegacyCertificatePrivateKey = "Switch.System.Security.Cryptography.Xml.SignedXmlUseLegacyCertificatePrivateKey";

		// Token: 0x04000054 RID: 84
		private static int _cmsUseInsecureHashAlgorithms;

		// Token: 0x04000055 RID: 85
		internal static readonly string SwitchCmsUseInsecureHashAlgorithms = "Switch.System.Security.Cryptography.Pkcs.UseInsecureHashAlgorithms";

		// Token: 0x04000056 RID: 86
		private static int _envelopedCmsUseLegacyDefaultAlgorithm;

		// Token: 0x04000057 RID: 87
		internal static readonly string SwitchEnvelopedCmsUseLegacyDefaultAlgorithm = "Switch.System.Security.Cryptography.Pkcs.EnvelopedCmsUseLegacyDefaultAlgorithm";
	}
}
