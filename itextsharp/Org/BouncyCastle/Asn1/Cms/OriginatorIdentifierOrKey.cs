using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000410 RID: 1040
	public class OriginatorIdentifierOrKey : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x0600235F RID: 9055 RVA: 0x000D9535 File Offset: 0x000D8535
		public OriginatorIdentifierOrKey(IssuerAndSerialNumber id)
		{
			this.id = id;
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x000D9544 File Offset: 0x000D8544
		[Obsolete("Use version taking a 'SubjectKeyIdentifier'")]
		public OriginatorIdentifierOrKey(Asn1OctetString id) : this(new SubjectKeyIdentifier(id))
		{
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x000D9552 File Offset: 0x000D8552
		public OriginatorIdentifierOrKey(SubjectKeyIdentifier id)
		{
			this.id = new DerTaggedObject(false, 0, id);
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x000D9568 File Offset: 0x000D8568
		public OriginatorIdentifierOrKey(OriginatorPublicKey id)
		{
			this.id = new DerTaggedObject(false, 1, id);
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x000D957E File Offset: 0x000D857E
		[Obsolete("Use more specific version")]
		public OriginatorIdentifierOrKey(Asn1Object id)
		{
			this.id = id;
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000D958D File Offset: 0x000D858D
		private OriginatorIdentifierOrKey(Asn1TaggedObject id)
		{
			this.id = id;
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x000D959C File Offset: 0x000D859C
		public static OriginatorIdentifierOrKey GetInstance(Asn1TaggedObject o, bool explicitly)
		{
			if (!explicitly)
			{
				throw new ArgumentException("Can't implicitly tag OriginatorIdentifierOrKey");
			}
			return OriginatorIdentifierOrKey.GetInstance(o.GetObject());
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x000D95B8 File Offset: 0x000D85B8
		public static OriginatorIdentifierOrKey GetInstance(object o)
		{
			if (o == null || o is OriginatorIdentifierOrKey)
			{
				return (OriginatorIdentifierOrKey)o;
			}
			if (o is IssuerAndSerialNumber)
			{
				return new OriginatorIdentifierOrKey((IssuerAndSerialNumber)o);
			}
			if (o is SubjectKeyIdentifier)
			{
				return new OriginatorIdentifierOrKey((SubjectKeyIdentifier)o);
			}
			if (o is OriginatorPublicKey)
			{
				return new OriginatorIdentifierOrKey((OriginatorPublicKey)o);
			}
			if (o is Asn1TaggedObject)
			{
				return new OriginatorIdentifierOrKey((Asn1TaggedObject)o);
			}
			throw new ArgumentException("Invalid OriginatorIdentifierOrKey: " + o.GetType().Name);
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06002367 RID: 9063 RVA: 0x000D9641 File Offset: 0x000D8641
		public Asn1Encodable ID
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06002368 RID: 9064 RVA: 0x000D9649 File Offset: 0x000D8649
		public IssuerAndSerialNumber IssuerAndSerialNumber
		{
			get
			{
				if (this.id is IssuerAndSerialNumber)
				{
					return (IssuerAndSerialNumber)this.id;
				}
				return null;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06002369 RID: 9065 RVA: 0x000D9665 File Offset: 0x000D8665
		public SubjectKeyIdentifier SubjectKeyIdentifier
		{
			get
			{
				if (this.id is Asn1TaggedObject && ((Asn1TaggedObject)this.id).TagNo == 0)
				{
					return SubjectKeyIdentifier.GetInstance((Asn1TaggedObject)this.id, false);
				}
				return null;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x0600236A RID: 9066 RVA: 0x000D9699 File Offset: 0x000D8699
		[Obsolete("Use 'OriginatorPublicKey' property")]
		public OriginatorPublicKey OriginatorKey
		{
			get
			{
				return this.OriginatorPublicKey;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x0600236B RID: 9067 RVA: 0x000D96A1 File Offset: 0x000D86A1
		public OriginatorPublicKey OriginatorPublicKey
		{
			get
			{
				if (this.id is Asn1TaggedObject && ((Asn1TaggedObject)this.id).TagNo == 1)
				{
					return OriginatorPublicKey.GetInstance((Asn1TaggedObject)this.id, false);
				}
				return null;
			}
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x000D96D6 File Offset: 0x000D86D6
		public override Asn1Object ToAsn1Object()
		{
			return this.id.ToAsn1Object();
		}

		// Token: 0x0400187B RID: 6267
		private Asn1Encodable id;
	}
}
