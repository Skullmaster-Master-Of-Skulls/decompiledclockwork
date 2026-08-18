using System;
using System.Collections;
using Org.BouncyCastle.Crypto;

namespace Org.BouncyCastle.Pkcs
{
	// Token: 0x0200046B RID: 1131
	public class AsymmetricKeyEntry : Pkcs12Entry
	{
		// Token: 0x0600269F RID: 9887 RVA: 0x000EA318 File Offset: 0x000E9318
		public AsymmetricKeyEntry(AsymmetricKeyParameter key) : base(new Hashtable())
		{
			this.key = key;
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x000EA32C File Offset: 0x000E932C
		public AsymmetricKeyEntry(AsymmetricKeyParameter key, Hashtable attributes) : base(attributes)
		{
			this.key = key;
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060026A1 RID: 9889 RVA: 0x000EA33C File Offset: 0x000E933C
		public AsymmetricKeyParameter Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x000EA344 File Offset: 0x000E9344
		public override bool Equals(object obj)
		{
			AsymmetricKeyEntry asymmetricKeyEntry = obj as AsymmetricKeyEntry;
			return asymmetricKeyEntry != null && this.key.Equals(asymmetricKeyEntry.key);
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x000EA36E File Offset: 0x000E936E
		public override int GetHashCode()
		{
			return ~this.key.GetHashCode();
		}

		// Token: 0x04001AB0 RID: 6832
		private readonly AsymmetricKeyParameter key;
	}
}
