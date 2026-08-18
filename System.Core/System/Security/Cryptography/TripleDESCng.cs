using System;
using System.Security.Permissions;
using Internal.Cryptography;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200011C RID: 284
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class TripleDESCng : TripleDES, ICngSymmetricAlgorithm
	{
		// Token: 0x06000908 RID: 2312 RVA: 0x0001F25B File Offset: 0x0001D45B
		public TripleDESCng()
		{
			this.SetLegalKeySizesValue();
			this._core = new CngSymmetricAlgorithmCore(this);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0001F275 File Offset: 0x0001D475
		public TripleDESCng(string keyName) : this(keyName, CngProvider.MicrosoftSoftwareKeyStorageProvider)
		{
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0001F283 File Offset: 0x0001D483
		public TripleDESCng(string keyName, CngProvider provider) : this(keyName, provider, CngKeyOpenOptions.None)
		{
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0001F28E File Offset: 0x0001D48E
		public TripleDESCng(string keyName, CngProvider provider, CngKeyOpenOptions openOptions)
		{
			this.SetLegalKeySizesValue();
			this._core = new CngSymmetricAlgorithmCore(this, keyName, provider, openOptions);
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x0001F2AB File Offset: 0x0001D4AB
		// (set) Token: 0x0600090D RID: 2317 RVA: 0x0001F2B8 File Offset: 0x0001D4B8
		public override byte[] Key
		{
			get
			{
				return this._core.GetKeyIfExportable();
			}
			set
			{
				this._core.SetKey(value);
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x0001F2C6 File Offset: 0x0001D4C6
		// (set) Token: 0x0600090F RID: 2319 RVA: 0x0001F2CE File Offset: 0x0001D4CE
		public override int KeySize
		{
			get
			{
				return base.KeySize;
			}
			set
			{
				this._core.SetKeySize(value, this);
			}
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0001F2DD File Offset: 0x0001D4DD
		public override ICryptoTransform CreateDecryptor()
		{
			return this._core.CreateDecryptor();
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0001F2EA File Offset: 0x0001D4EA
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return this._core.CreateDecryptor(rgbKey, rgbIV);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0001F2F9 File Offset: 0x0001D4F9
		public override ICryptoTransform CreateEncryptor()
		{
			return this._core.CreateEncryptor();
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0001F306 File Offset: 0x0001D506
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return this._core.CreateEncryptor(rgbKey, rgbIV);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0001F315 File Offset: 0x0001D515
		public override void GenerateKey()
		{
			this._core.GenerateKey();
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0001F322 File Offset: 0x0001D522
		public override void GenerateIV()
		{
			this._core.GenerateIV();
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0001F32F File Offset: 0x0001D52F
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0001F338 File Offset: 0x0001D538
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x0001F340 File Offset: 0x0001D540
		byte[] ICngSymmetricAlgorithm.BaseKey
		{
			get
			{
				return base.Key;
			}
			set
			{
				base.Key = value;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0001F349 File Offset: 0x0001D549
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x0001F351 File Offset: 0x0001D551
		int ICngSymmetricAlgorithm.BaseKeySize
		{
			get
			{
				return base.KeySize;
			}
			set
			{
				base.KeySize = value;
			}
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0001F35A File Offset: 0x0001D55A
		bool ICngSymmetricAlgorithm.IsWeakKey(byte[] key)
		{
			return TripleDES.IsWeakKey(key);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0001F362 File Offset: 0x0001D562
		[SecurityCritical]
		SafeBCryptAlgorithmHandle ICngSymmetricAlgorithm.GetEphemeralModeHandle()
		{
			return BCryptNative.TripleDesBCryptModes.GetSharedHandle(this.Mode);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0001F36F File Offset: 0x0001D56F
		string ICngSymmetricAlgorithm.GetNCryptAlgorithmIdentifier()
		{
			return "3DES";
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0001F376 File Offset: 0x0001D576
		private void SetLegalKeySizesValue()
		{
			this.LegalKeySizesValue = new KeySizes[]
			{
				new KeySizes(192, 192, 0)
			};
		}

		// Token: 0x040006EB RID: 1771
		private CngSymmetricAlgorithmCore _core;
	}
}
