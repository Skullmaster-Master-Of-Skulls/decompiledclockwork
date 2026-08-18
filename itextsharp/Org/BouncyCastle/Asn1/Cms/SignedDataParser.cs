using System;
using System.IO;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x020001BD RID: 445
	public class SignedDataParser
	{
		// Token: 0x060010C0 RID: 4288 RVA: 0x0005F408 File Offset: 0x0005E408
		public static SignedDataParser GetInstance(object o)
		{
			if (o is Asn1Sequence)
			{
				return new SignedDataParser(((Asn1Sequence)o).Parser);
			}
			if (o is Asn1SequenceParser)
			{
				return new SignedDataParser((Asn1SequenceParser)o);
			}
			throw new IOException("unknown object encountered: " + o.GetType().Name);
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0005F45C File Offset: 0x0005E45C
		public SignedDataParser(Asn1SequenceParser seq)
		{
			this._seq = seq;
			this._version = (DerInteger)seq.ReadObject();
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x0005F47C File Offset: 0x0005E47C
		public DerInteger Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0005F484 File Offset: 0x0005E484
		public Asn1SetParser GetDigestAlgorithms()
		{
			return (Asn1SetParser)this._seq.ReadObject();
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0005F496 File Offset: 0x0005E496
		public ContentInfoParser GetEncapContentInfo()
		{
			return new ContentInfoParser((Asn1SequenceParser)this._seq.ReadObject());
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0005F4B0 File Offset: 0x0005E4B0
		public Asn1SetParser GetCertificates()
		{
			this._certsCalled = true;
			this._nextObject = this._seq.ReadObject();
			if (this._nextObject is Asn1TaggedObjectParser && ((Asn1TaggedObjectParser)this._nextObject).TagNo == 0)
			{
				Asn1SetParser result = (Asn1SetParser)((Asn1TaggedObjectParser)this._nextObject).GetObjectParser(17, false);
				this._nextObject = null;
				return result;
			}
			return null;
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0005F518 File Offset: 0x0005E518
		public Asn1SetParser GetCrls()
		{
			if (!this._certsCalled)
			{
				throw new IOException("GetCerts() has not been called.");
			}
			this._crlsCalled = true;
			if (this._nextObject == null)
			{
				this._nextObject = this._seq.ReadObject();
			}
			if (this._nextObject is Asn1TaggedObjectParser && ((Asn1TaggedObjectParser)this._nextObject).TagNo == 1)
			{
				Asn1SetParser result = (Asn1SetParser)((Asn1TaggedObjectParser)this._nextObject).GetObjectParser(17, false);
				this._nextObject = null;
				return result;
			}
			return null;
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0005F59C File Offset: 0x0005E59C
		public Asn1SetParser GetSignerInfos()
		{
			if (!this._certsCalled || !this._crlsCalled)
			{
				throw new IOException("GetCerts() and/or GetCrls() has not been called.");
			}
			if (this._nextObject == null)
			{
				this._nextObject = this._seq.ReadObject();
			}
			return (Asn1SetParser)this._nextObject;
		}

		// Token: 0x04000C32 RID: 3122
		private Asn1SequenceParser _seq;

		// Token: 0x04000C33 RID: 3123
		private DerInteger _version;

		// Token: 0x04000C34 RID: 3124
		private object _nextObject;

		// Token: 0x04000C35 RID: 3125
		private bool _certsCalled;

		// Token: 0x04000C36 RID: 3126
		private bool _crlsCalled;
	}
}
