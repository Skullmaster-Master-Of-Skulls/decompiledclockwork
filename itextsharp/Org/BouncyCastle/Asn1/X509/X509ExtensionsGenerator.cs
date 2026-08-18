using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000256 RID: 598
	public class X509ExtensionsGenerator
	{
		// Token: 0x060016B8 RID: 5816 RVA: 0x000836EE File Offset: 0x000826EE
		public void Reset()
		{
			this.extensions = new Hashtable();
			this.extOrdering = new ArrayList();
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x00083708 File Offset: 0x00082708
		public void AddExtension(DerObjectIdentifier oid, bool critical, Asn1Encodable extValue)
		{
			byte[] derEncoded;
			try
			{
				derEncoded = extValue.GetDerEncoded();
			}
			catch (Exception arg)
			{
				throw new ArgumentException("error encoding value: " + arg);
			}
			this.AddExtension(oid, critical, derEncoded);
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x0008374C File Offset: 0x0008274C
		public void AddExtension(DerObjectIdentifier oid, bool critical, byte[] extValue)
		{
			if (this.extensions.ContainsKey(oid))
			{
				throw new ArgumentException("extension " + oid + " already added");
			}
			this.extOrdering.Add(oid);
			this.extensions.Add(oid, new X509Extension(critical, new DerOctetString(extValue)));
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x000837A2 File Offset: 0x000827A2
		public bool IsEmpty
		{
			get
			{
				return this.extOrdering.Count < 1;
			}
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x000837B2 File Offset: 0x000827B2
		public X509Extensions Generate()
		{
			return new X509Extensions(this.extOrdering, this.extensions);
		}

		// Token: 0x04000FA1 RID: 4001
		private Hashtable extensions = new Hashtable();

		// Token: 0x04000FA2 RID: 4002
		private ArrayList extOrdering = new ArrayList();
	}
}
