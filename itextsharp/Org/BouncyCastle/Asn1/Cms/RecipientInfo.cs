using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x020001BE RID: 446
	public class RecipientInfo : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x060010C8 RID: 4296 RVA: 0x0005F5E8 File Offset: 0x0005E5E8
		public RecipientInfo(KeyTransRecipientInfo info)
		{
			this.info = info;
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0005F5F7 File Offset: 0x0005E5F7
		public RecipientInfo(KeyAgreeRecipientInfo info)
		{
			this.info = new DerTaggedObject(false, 1, info);
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0005F60D File Offset: 0x0005E60D
		public RecipientInfo(KekRecipientInfo info)
		{
			this.info = new DerTaggedObject(false, 2, info);
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0005F623 File Offset: 0x0005E623
		public RecipientInfo(PasswordRecipientInfo info)
		{
			this.info = new DerTaggedObject(false, 3, info);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0005F639 File Offset: 0x0005E639
		public RecipientInfo(OtherRecipientInfo info)
		{
			this.info = new DerTaggedObject(false, 4, info);
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0005F64F File Offset: 0x0005E64F
		public RecipientInfo(Asn1Object info)
		{
			this.info = info;
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x0005F660 File Offset: 0x0005E660
		public static RecipientInfo GetInstance(object o)
		{
			if (o == null || o is RecipientInfo)
			{
				return (RecipientInfo)o;
			}
			if (o is Asn1Sequence)
			{
				return new RecipientInfo((Asn1Sequence)o);
			}
			if (o is Asn1TaggedObject)
			{
				return new RecipientInfo((Asn1TaggedObject)o);
			}
			throw new ArgumentException("unknown object in factory: " + o.GetType().Name);
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x0005F6C4 File Offset: 0x0005E6C4
		public DerInteger Version
		{
			get
			{
				if (!(this.info is Asn1TaggedObject))
				{
					return KeyTransRecipientInfo.GetInstance(this.info).Version;
				}
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)this.info;
				switch (asn1TaggedObject.TagNo)
				{
				case 1:
					return KeyAgreeRecipientInfo.GetInstance(asn1TaggedObject, false).Version;
				case 2:
					return this.GetKekInfo(asn1TaggedObject).Version;
				case 3:
					return PasswordRecipientInfo.GetInstance(asn1TaggedObject, false).Version;
				case 4:
					return new DerInteger(0);
				default:
					throw new InvalidOperationException("unknown tag");
				}
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x0005F754 File Offset: 0x0005E754
		public bool IsTagged
		{
			get
			{
				return this.info is Asn1TaggedObject;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x0005F764 File Offset: 0x0005E764
		public Asn1Encodable Info
		{
			get
			{
				if (!(this.info is Asn1TaggedObject))
				{
					return KeyTransRecipientInfo.GetInstance(this.info);
				}
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)this.info;
				switch (asn1TaggedObject.TagNo)
				{
				case 1:
					return KeyAgreeRecipientInfo.GetInstance(asn1TaggedObject, false);
				case 2:
					return this.GetKekInfo(asn1TaggedObject);
				case 3:
					return PasswordRecipientInfo.GetInstance(asn1TaggedObject, false);
				case 4:
					return OtherRecipientInfo.GetInstance(asn1TaggedObject, false);
				default:
					throw new InvalidOperationException("unknown tag");
				}
			}
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0005F7E1 File Offset: 0x0005E7E1
		private KekRecipientInfo GetKekInfo(Asn1TaggedObject o)
		{
			return KekRecipientInfo.GetInstance(o, o.IsExplicit());
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0005F7EF File Offset: 0x0005E7EF
		public override Asn1Object ToAsn1Object()
		{
			return this.info.ToAsn1Object();
		}

		// Token: 0x04000C37 RID: 3127
		internal Asn1Encodable info;
	}
}
