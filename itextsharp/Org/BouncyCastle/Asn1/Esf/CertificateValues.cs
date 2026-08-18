using System;
using System.Collections;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x020005BE RID: 1470
	public class CertificateValues : Asn1Encodable
	{
		// Token: 0x06003285 RID: 12933 RVA: 0x001393AC File Offset: 0x001383AC
		public static CertificateValues GetInstance(object obj)
		{
			if (obj == null || obj is CertificateValues)
			{
				return (CertificateValues)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertificateValues((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CertificateValues' factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x00139400 File Offset: 0x00138400
		private CertificateValues(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			foreach (object obj in seq)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				X509CertificateStructure.GetInstance(asn1Encodable.ToAsn1Object());
			}
			this.certificates = seq;
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x00139474 File Offset: 0x00138474
		public CertificateValues(params X509CertificateStructure[] certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			this.certificates = new DerSequence(certificates);
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x00139498 File Offset: 0x00138498
		public CertificateValues(IEnumerable certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			if (!CollectionUtilities.CheckElementsAreOfType(certificates, typeof(X509CertificateStructure)))
			{
				throw new ArgumentException("Must contain only 'X509CertificateStructure' objects", "certificates");
			}
			this.certificates = new DerSequence(Asn1EncodableVector.FromEnumerable(certificates));
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x001394EC File Offset: 0x001384EC
		public X509CertificateStructure[] GetCertificates()
		{
			X509CertificateStructure[] array = new X509CertificateStructure[this.certificates.Count];
			for (int i = 0; i < this.certificates.Count; i++)
			{
				array[i] = X509CertificateStructure.GetInstance(this.certificates[i]);
			}
			return array;
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x00139535 File Offset: 0x00138535
		public override Asn1Object ToAsn1Object()
		{
			return this.certificates;
		}

		// Token: 0x0400228B RID: 8843
		private readonly Asn1Sequence certificates;
	}
}
