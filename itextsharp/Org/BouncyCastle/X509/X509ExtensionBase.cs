using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000003 RID: 3
	public abstract class X509ExtensionBase : IX509Extension
	{
		// Token: 0x06000005 RID: 5
		protected abstract X509Extensions GetX509Extensions();

		// Token: 0x06000006 RID: 6 RVA: 0x000020D0 File Offset: 0x000010D0
		protected virtual ISet GetExtensionOids(bool critical)
		{
			X509Extensions x509Extensions = this.GetX509Extensions();
			if (x509Extensions != null)
			{
				HashSet hashSet = new HashSet();
				foreach (object obj in x509Extensions.ExtensionOids)
				{
					DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)obj;
					X509Extension extension = x509Extensions.GetExtension(derObjectIdentifier);
					if (extension.IsCritical == critical)
					{
						hashSet.Add(derObjectIdentifier.Id);
					}
				}
				return hashSet;
			}
			return null;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000215C File Offset: 0x0000115C
		public virtual ISet GetNonCriticalExtensionOids()
		{
			return this.GetExtensionOids(false);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002165 File Offset: 0x00001165
		public virtual ISet GetCriticalExtensionOids()
		{
			return this.GetExtensionOids(true);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000216E File Offset: 0x0000116E
		[Obsolete("Use version taking a DerObjectIdentifier instead")]
		public Asn1OctetString GetExtensionValue(string oid)
		{
			return this.GetExtensionValue(new DerObjectIdentifier(oid));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000217C File Offset: 0x0000117C
		public virtual Asn1OctetString GetExtensionValue(DerObjectIdentifier oid)
		{
			X509Extensions x509Extensions = this.GetX509Extensions();
			if (x509Extensions != null)
			{
				X509Extension extension = x509Extensions.GetExtension(oid);
				if (extension != null)
				{
					return extension.Value;
				}
			}
			return null;
		}
	}
}
