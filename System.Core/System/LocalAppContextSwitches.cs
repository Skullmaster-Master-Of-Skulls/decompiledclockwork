using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000024 RID: 36
	internal static class LocalAppContextSwitches
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000039B4 File Offset: 0x00001BB4
		public static bool DontReliablyClonePrivateKey
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Security.Cryptography.X509Certificates.RSACertificateExtensions.DontReliablyClonePrivateKey", ref LocalAppContextSwitches._dontReliablyClonePrivateKeyName);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000039C5 File Offset: 0x00001BC5
		public static bool UseLegacyPublicKeyBehavior
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Security.Cryptography.X509Certificates.ECDsaCertificateExtensions.UseLegacyPublicKeyBehavior", ref LocalAppContextSwitches._useLegacyPublicKeyBehavior);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600010A RID: 266 RVA: 0x000039D6 File Offset: 0x00001BD6
		public static bool AesCryptoServiceProviderDontCorrectlyResetDecryptor
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Security.Cryptography.AesCryptoServiceProvider.DontCorrectlyResetDecryptor", ref LocalAppContextSwitches._aesCryptoServiceProviderDontCorrectlyResetDecryptorName);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000039E7 File Offset: 0x00001BE7
		public static bool SymmetricCngAlwaysUseNCrypt
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Security.Cryptography.SymmetricCng.AlwaysUseNCrypt", ref LocalAppContextSwitches._symmetricCngAlwaysUseNCryptName);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000039F8 File Offset: 0x00001BF8
		public static bool UseLegacyFipsThrow
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue(LocalAppContextSwitches.SwitchCryptographyUseLegacyFipsThrow, ref LocalAppContextSwitches._useLegacyFipsThrow);
			}
		}

		// Token: 0x040000D5 RID: 213
		internal const string DontReliablyClonePrivateKeyStr = "Switch.System.Security.Cryptography.X509Certificates.RSACertificateExtensions.DontReliablyClonePrivateKey";

		// Token: 0x040000D6 RID: 214
		private static int _dontReliablyClonePrivateKeyName;

		// Token: 0x040000D7 RID: 215
		internal const string UseLegacyPublicKeyBehaviorStr = "Switch.System.Security.Cryptography.X509Certificates.ECDsaCertificateExtensions.UseLegacyPublicKeyBehavior";

		// Token: 0x040000D8 RID: 216
		private static int _useLegacyPublicKeyBehavior;

		// Token: 0x040000D9 RID: 217
		internal const string AesCryptoServiceProviderDontCorrectlyResetDecryptorStr = "Switch.System.Security.Cryptography.AesCryptoServiceProvider.DontCorrectlyResetDecryptor";

		// Token: 0x040000DA RID: 218
		private static int _aesCryptoServiceProviderDontCorrectlyResetDecryptorName;

		// Token: 0x040000DB RID: 219
		internal const string SymmetricCngAlwaysUseNCryptStr = "Switch.System.Security.Cryptography.SymmetricCng.AlwaysUseNCrypt";

		// Token: 0x040000DC RID: 220
		private static int _symmetricCngAlwaysUseNCryptName;

		// Token: 0x040000DD RID: 221
		internal static readonly string SwitchCryptographyUseLegacyFipsThrow = "Switch.System.Security.Cryptography.UseLegacyFipsThrow";

		// Token: 0x040000DE RID: 222
		private static int _useLegacyFipsThrow;
	}
}
