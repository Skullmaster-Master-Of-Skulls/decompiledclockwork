using System;
using System.Security.Permissions;
using Internal.Cryptography;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x020000DC RID: 220
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class AesCng : Aes, ICngSymmetricAlgorithm
	{
		// Token: 0x060006B4 RID: 1716 RVA: 0x00015FA7 File Offset: 0x000141A7
		public AesCng()
		{
			this._core = new CngSymmetricAlgorithmCore(this);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00015FBB File Offset: 0x000141BB
		public AesCng(string keyName) : this(keyName, CngProvider.MicrosoftSoftwareKeyStorageProvider)
		{
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00015FC9 File Offset: 0x000141C9
		public AesCng(string keyName, CngProvider provider) : this(keyName, provider, CngKeyOpenOptions.None)
		{
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00015FD4 File Offset: 0x000141D4
		public AesCng(string keyName, CngProvider provider, CngKeyOpenOptions openOptions)
		{
			this._core = new CngSymmetricAlgorithmCore(this, keyName, provider, openOptions);
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00015FEB File Offset: 0x000141EB
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x00015FF8 File Offset: 0x000141F8
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

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00016006 File Offset: 0x00014206
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x0001600E File Offset: 0x0001420E
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

		// Token: 0x060006BC RID: 1724 RVA: 0x0001601D File Offset: 0x0001421D
		public override ICryptoTransform CreateDecryptor()
		{
			return this._core.CreateDecryptor();
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001602A File Offset: 0x0001422A
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return this._core.CreateDecryptor(rgbKey, rgbIV);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00016039 File Offset: 0x00014239
		public override ICryptoTransform CreateEncryptor()
		{
			return this._core.CreateEncryptor();
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00016046 File Offset: 0x00014246
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return this._core.CreateEncryptor(rgbKey, rgbIV);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00016055 File Offset: 0x00014255
		public override void GenerateKey()
		{
			this._core.GenerateKey();
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00016062 File Offset: 0x00014262
		public override void GenerateIV()
		{
			this._core.GenerateIV();
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001606F File Offset: 0x0001426F
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00016078 File Offset: 0x00014278
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x00016080 File Offset: 0x00014280
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

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00016089 File Offset: 0x00014289
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x00016091 File Offset: 0x00014291
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

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001609A File Offset: 0x0001429A
		bool ICngSymmetricAlgorithm.IsWeakKey(byte[] key)
		{
			return false;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001609D File Offset: 0x0001429D
		[SecurityCritical]
		SafeBCryptAlgorithmHandle ICngSymmetricAlgorithm.GetEphemeralModeHandle()
		{
			return BCryptNative.AesBCryptModes.GetSharedHandle(this.Mode);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x000160AA File Offset: 0x000142AA
		string ICngSymmetricAlgorithm.GetNCryptAlgorithmIdentifier()
		{
			return "AES";
		}

		// Token: 0x040005D2 RID: 1490
		private CngSymmetricAlgorithmCore _core;
	}
}
