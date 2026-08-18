using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens;

namespace System.IdentityModel.Metadata
{
	// Token: 0x020000F7 RID: 247
	public class KeyDescriptor
	{
		// Token: 0x060006AE RID: 1710 RVA: 0x0001AAF4 File Offset: 0x00018CF4
		public KeyDescriptor() : this(null)
		{
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001AAFD File Offset: 0x00018CFD
		public KeyDescriptor(SecurityKeyIdentifier ski)
		{
			this._ski = ski;
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0001AB17 File Offset: 0x00018D17
		// (set) Token: 0x060006B1 RID: 1713 RVA: 0x0001AB1F File Offset: 0x00018D1F
		public SecurityKeyIdentifier KeyInfo
		{
			get
			{
				return this._ski;
			}
			set
			{
				this._ski = value;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0001AB28 File Offset: 0x00018D28
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x0001AB30 File Offset: 0x00018D30
		public KeyType Use
		{
			get
			{
				return this._use;
			}
			set
			{
				this._use = value;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0001AB39 File Offset: 0x00018D39
		public ICollection<EncryptionMethod> EncryptionMethods
		{
			get
			{
				return this._encryptionMethods;
			}
		}

		// Token: 0x04000A73 RID: 2675
		private SecurityKeyIdentifier _ski;

		// Token: 0x04000A74 RID: 2676
		private KeyType _use;

		// Token: 0x04000A75 RID: 2677
		private Collection<EncryptionMethod> _encryptionMethods = new Collection<EncryptionMethod>();
	}
}
