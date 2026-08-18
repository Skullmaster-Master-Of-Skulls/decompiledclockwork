using System;

namespace Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x0200025F RID: 607
	public class ContentHints : Asn1Encodable
	{
		// Token: 0x060016FB RID: 5883 RVA: 0x00084D5C File Offset: 0x00083D5C
		public static ContentHints GetInstance(object o)
		{
			if (o == null || o is ContentHints)
			{
				return (ContentHints)o;
			}
			if (o is Asn1Sequence)
			{
				return new ContentHints((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in 'ContentHints' factory : " + o.GetType().Name + ".");
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00084DB0 File Offset: 0x00083DB0
		private ContentHints(Asn1Sequence seq)
		{
			IAsn1Convertible asn1Convertible = seq[0];
			if (asn1Convertible.ToAsn1Object() is DerUtf8String)
			{
				this.contentDescription = DerUtf8String.GetInstance(asn1Convertible);
				this.contentType = DerObjectIdentifier.GetInstance(seq[1]);
				return;
			}
			this.contentType = DerObjectIdentifier.GetInstance(seq[0]);
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00084E09 File Offset: 0x00083E09
		public ContentHints(DerObjectIdentifier contentType)
		{
			this.contentType = contentType;
			this.contentDescription = null;
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x00084E1F File Offset: 0x00083E1F
		public ContentHints(DerObjectIdentifier contentType, DerUtf8String contentDescription)
		{
			this.contentType = contentType;
			this.contentDescription = contentDescription;
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x00084E35 File Offset: 0x00083E35
		public DerObjectIdentifier ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x00084E3D File Offset: 0x00083E3D
		public DerUtf8String ContentDescription
		{
			get
			{
				return this.contentDescription;
			}
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x00084E48 File Offset: 0x00083E48
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.contentDescription != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.contentDescription
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.contentType
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000FC4 RID: 4036
		private readonly DerUtf8String contentDescription;

		// Token: 0x04000FC5 RID: 4037
		private readonly DerObjectIdentifier contentType;
	}
}
