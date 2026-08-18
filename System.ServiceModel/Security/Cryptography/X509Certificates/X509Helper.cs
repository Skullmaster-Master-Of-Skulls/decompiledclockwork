using System;
using System.Reflection;
using System.Security.Permissions;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x0200001C RID: 28
	internal static class X509Helper
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x0000716C File Offset: 0x0000536C
		internal static byte[] VerifyNotPfx(byte[] rawData)
		{
			if (!X509Helper.verifyNotPfxMethodInitialized)
			{
				X509Helper.InitializeNotPfxDelegate();
			}
			if (X509Helper.verifyNotPfxDelegate != null)
			{
				X509Helper.verifyNotPfxDelegate(rawData, "Pkcs12PrivateKeyCheckForSystem.ServiceModel", ref X509Helper.privateKeyCheckSetting);
			}
			return rawData;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000719C File Offset: 0x0000539C
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

		// Token: 0x040000BB RID: 187
		public const string SettingName = "Pkcs12PrivateKeyCheckForSystem.ServiceModel";

		// Token: 0x040000BC RID: 188
		private static int privateKeyCheckSetting;

		// Token: 0x040000BD RID: 189
		private static volatile bool verifyNotPfxMethodInitialized;

		// Token: 0x040000BE RID: 190
		private static X509Helper.VerifyNotPfxDelegate verifyNotPfxDelegate;

		// Token: 0x02000AC8 RID: 2760
		// (Invoke) Token: 0x06006E34 RID: 28212
		private delegate void VerifyNotPfxDelegate(byte[] rawData, string settingName, ref int setting);
	}
}
