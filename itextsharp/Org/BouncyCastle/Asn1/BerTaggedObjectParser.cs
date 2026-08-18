using System;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020001C0 RID: 448
	public class BerTaggedObjectParser : Asn1TaggedObjectParser, IAsn1Convertible
	{
		// Token: 0x060010DD RID: 4317 RVA: 0x0005F918 File Offset: 0x0005E918
		internal BerTaggedObjectParser(int baseTag, int tagNumber, Stream contentStream)
		{
			if (!contentStream.CanRead)
			{
				throw new ArgumentException("Expected stream to be readable", "contentStream");
			}
			this._baseTag = baseTag;
			this._tagNumber = tagNumber;
			this._contentStream = contentStream;
			this._indefiniteLength = (contentStream is IndefiniteLengthInputStream);
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x0005F967 File Offset: 0x0005E967
		public bool IsConstructed
		{
			get
			{
				return (this._baseTag & 32) != 0;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x0005F978 File Offset: 0x0005E978
		public int TagNo
		{
			get
			{
				return this._tagNumber;
			}
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0005F980 File Offset: 0x0005E980
		public IAsn1Convertible GetObjectParser(int tag, bool isExplicit)
		{
			if (isExplicit)
			{
				return new Asn1StreamParser(this._contentStream).ReadObject();
			}
			if (tag != 4)
			{
				switch (tag)
				{
				case 16:
					if (this._indefiniteLength)
					{
						return new BerSequenceParser(new Asn1StreamParser(this._contentStream));
					}
					return new DerSequenceParser(new Asn1StreamParser(this._contentStream));
				case 17:
					if (this._indefiniteLength)
					{
						return new BerSetParser(new Asn1StreamParser(this._contentStream));
					}
					return new DerSetParser(new Asn1StreamParser(this._contentStream));
				default:
					throw Platform.CreateNotImplementedException("implicit tagging");
				}
			}
			else
			{
				if (this._indefiniteLength || this.IsConstructed)
				{
					return new BerOctetStringParser(new Asn1StreamParser(this._contentStream));
				}
				return new DerOctetStringParser((DefiniteLengthInputStream)this._contentStream);
			}
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0005FA50 File Offset: 0x0005EA50
		private Asn1EncodableVector rLoadVector(Stream inStream)
		{
			Asn1EncodableVector result;
			try
			{
				result = new Asn1StreamParser(inStream).ReadVector();
			}
			catch (IOException ex)
			{
				throw new InvalidOperationException(ex.Message, ex);
			}
			return result;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0005FA8C File Offset: 0x0005EA8C
		public Asn1Object ToAsn1Object()
		{
			if (this._indefiniteLength)
			{
				Asn1EncodableVector asn1EncodableVector = this.rLoadVector(this._contentStream);
				if (asn1EncodableVector.Count != 1)
				{
					return new BerTaggedObject(false, this._tagNumber, BerSequence.FromVector(asn1EncodableVector));
				}
				return new BerTaggedObject(true, this._tagNumber, asn1EncodableVector[0]);
			}
			else
			{
				if (!this.IsConstructed)
				{
					Asn1Object result;
					try
					{
						DefiniteLengthInputStream definiteLengthInputStream = (DefiniteLengthInputStream)this._contentStream;
						result = new DerTaggedObject(false, this._tagNumber, new DerOctetString(definiteLengthInputStream.ToArray()));
					}
					catch (IOException ex)
					{
						throw new InvalidOperationException(ex.Message, ex);
					}
					return result;
				}
				Asn1EncodableVector asn1EncodableVector2 = this.rLoadVector(this._contentStream);
				if (asn1EncodableVector2.Count != 1)
				{
					return new DerTaggedObject(false, this._tagNumber, DerSequence.FromVector(asn1EncodableVector2));
				}
				return new DerTaggedObject(true, this._tagNumber, asn1EncodableVector2[0]);
			}
		}

		// Token: 0x04000C39 RID: 3129
		private int _baseTag;

		// Token: 0x04000C3A RID: 3130
		private int _tagNumber;

		// Token: 0x04000C3B RID: 3131
		private Stream _contentStream;

		// Token: 0x04000C3C RID: 3132
		private bool _indefiniteLength;
	}
}
