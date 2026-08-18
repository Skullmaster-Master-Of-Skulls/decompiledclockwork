using System;
using System.Reflection;
using System.Security.Permissions;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000017 RID: 23
	internal static class X509Helper
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00003F1C File Offset: 0x0000211C
		internal static byte[] VerifyNotPfx(byte[] rawData)
		{
			if (!X509Helper.verifyNotPfxMethodInitialized)
			{
				X509Helper.InitializeNotPfxDelegate();
			}
			if (X509Helper.verifyNotPfxDelegate != null)
			{
				X509Helper.verifyNotPfxDelegate(rawData, "Pkcs12PrivateKeyCheckForSystem.IdentityModel", ref X509Helper.privateKeyCheckSetting);
			}
			return rawData;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003F4C File Offset: 0x0000214C
		[SecuritySafeCritical]
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		private static void InitializeNotPfxDelegate()
		{
			Assembly assembly = typeof(X509Certificate2).Assembly;
			if (assembly != null)
			{
				Type type = assembly.GetType("System.Security.Cryptography.X509Certificates.PrivateKeyEnforcer");
				if (type != null)
				{
					BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.NonPublic;
					Type[] types = new Type[]
					{
						typeof(byte[]),
						typeof(string),
						typeof(int).MakeByRefType()
					};
					MethodInfo method = type.GetMethod("VerifyNotPfx", bindingAttr, null, types, null);
					if (method != null)
					{
						X509Helper.verifyNotPfxDelegate = (X509Helper.VerifyNotPfxDelegate)Delegate.CreateDelegate(typeof(X509Helper.VerifyNotPfxDelegate), method);
					}
				}
			}
			X509Helper.verifyNotPfxMethodInitialized = true;
		}

		// Token: 0x040000A9 RID: 169
		public const string SettingName = "Pkcs12PrivateKeyCheckForSystem.IdentityModel";

		// Token: 0x040000AA RID: 170
		private static int privateKeyCheckSetting;

		// Token: 0x040000AB RID: 171
		private static volatile bool verifyNotPfxMethodInitialized;

		// Token: 0x040000AC RID: 172
		private static X509Helper.VerifyNotPfxDelegate verifyNotPfxDelegate;

		// Token: 0x02000226 RID: 550
		// (Invoke) Token: 0x060011E1 RID: 4577
		private delegate void VerifyNotPfxDelegate(byte[] rawData, string settingName, ref int setting);
	}
}
