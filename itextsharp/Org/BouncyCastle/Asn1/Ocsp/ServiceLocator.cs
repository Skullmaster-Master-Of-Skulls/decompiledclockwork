using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x02000209 RID: 521
	public class ServiceLocator : Asn1Encodable
	{
		// Token: 0x060013FA RID: 5114 RVA: 0x00072BE2 File Offset: 0x00071BE2
		public static ServiceLocator GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return ServiceLocator.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x00072BF0 File Offset: 0x00071BF0
		public static ServiceLocator GetInstance(object obj)
		{
			if (obj == null || obj is ServiceLocator)
			{
				return (ServiceLocator)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ServiceLocator((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x00072C42 File Offset: 0x00071C42
		public ServiceLocator(X509Name issuer) : this(issuer, null)
		{
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00072C4C File Offset: 0x00071C4C
		public ServiceLocator(X509Name issuer, Asn1Object locator)
		{
			if (issuer == null)
			{
				throw new ArgumentNullException("issuer");
			}
			this.issuer = issuer;
			this.locator = locator;
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x00072C70 File Offset: 0x00071C70
		private ServiceLocator(Asn1Sequence seq)
		{
			this.issuer = X509Name.GetInstance(seq[0]);
			if (seq.Count > 1)
			{
				this.locator = seq[1].ToAsn1Object();
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x00072CA5 File Offset: 0x00071CA5
		public X509Name Issuer
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x00072CAD File Offset: 0x00071CAD
		public Asn1Object Locator
		{
			get
			{
				return this.locator;
			}
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x00072CB8 File Offset: 0x00071CB8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.issuer
			});
			if (this.locator != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.locator
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000DCD RID: 3533
		private readonly X509Name issuer;

		// Token: 0x04000DCE RID: 3534
		private readonly Asn1Object locator;
	}
}
