using System;
using System.IO;
using Org.BouncyCastle.Asn1.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020000B6 RID: 182
	public class Asn1InputStream : FilterStream
	{
		// Token: 0x060005AE RID: 1454 RVA: 0x0001D064 File Offset: 0x0001C064
		public Asn1InputStream(Stream inputStream) : this(inputStream, int.MaxValue)
		{
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001D072 File Offset: 0x0001C072
		public Asn1InputStream(Stream inputStream, int limit) : base(inputStream)
		{
			this.limit = limit;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001D082 File Offset: 0x0001C082
		public Asn1InputStream(byte[] input) : this(new MemoryStream(input, false), input.Length)
		{
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0001D094 File Offset: 0x0001C094
		internal Asn1EncodableVector BuildEncodableVector()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			Asn1Object asn1Object;
			while ((asn1Object = this.ReadObject()) != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					asn1Object
				});
			}
			return asn1EncodableVector;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0001D0CC File Offset: 0x0001C0CC
		internal virtual Asn1EncodableVector BuildDerEncodableVector(DefiniteLengthInputStream dIn)
		{
			return new Asn1InputStream(dIn).BuildEncodableVector();
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001D0D9 File Offset: 0x0001C0D9
		internal virtual DerSequence CreateDerSequence(DefiniteLengthInputStream dIn)
		{
			return DerSequence.FromVector(this.BuildDerEncodableVector(dIn));
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0001D0E7 File Offset: 0x0001C0E7
		internal virtual DerSet CreateDerSet(DefiniteLengthInputStream dIn)
		{
			return DerSet.FromVector(this.BuildDerEncodableVector(dIn), false);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001D0F8 File Offset: 0x0001C0F8
		public Asn1Object ReadObject()
		{
			int num = this.ReadByte();
			if (num <= 0)
			{
				if (num == 0)
				{
					throw new IOException("unexpected end-of-contents marker");
				}
				return null;
			}
			else
			{
				int num2 = Asn1InputStream.ReadTagNumber(this, num);
				bool flag = (num & 32) != 0;
				int num3 = Asn1InputStream.ReadLength(this, this.limit);
				if (num3 < 0)
				{
					if (!flag)
					{
						throw new IOException("indefinite length primitive encoding encountered");
					}
					IndefiniteLengthInputStream indefiniteLengthInputStream = new IndefiniteLengthInputStream(this);
					if ((num & 64) != 0)
					{
						Asn1StreamParser parser = new Asn1StreamParser(indefiniteLengthInputStream, this.limit);
						return new BerApplicationSpecificParser(num2, parser).ToAsn1Object();
					}
					if ((num & 128) != 0)
					{
						return new BerTaggedObjectParser(num, num2, indefiniteLengthInputStream).ToAsn1Object();
					}
					Asn1StreamParser parser2 = new Asn1StreamParser(indefiniteLengthInputStream, this.limit);
					int num4 = num2;
					if (num4 == 4)
					{
						return new BerOctetStringParser(parser2).ToAsn1Object();
					}
					if (num4 == 8)
					{
						return new DerExternalParser(parser2).ToAsn1Object();
					}
					switch (num4)
					{
					case 16:
						return new BerSequenceParser(parser2).ToAsn1Object();
					case 17:
						return new BerSetParser(parser2).ToAsn1Object();
					default:
						throw new IOException("unknown BER object encountered");
					}
				}
				else
				{
					DefiniteLengthInputStream definiteLengthInputStream = new DefiniteLengthInputStream(this, num3);
					if ((num & 64) != 0)
					{
						return new DerApplicationSpecific(flag, num2, definiteLengthInputStream.ToArray());
					}
					if ((num & 128) != 0)
					{
						return new BerTaggedObjectParser(num, num2, definiteLengthInputStream).ToAsn1Object();
					}
					if (!flag)
					{
						return Asn1InputStream.CreatePrimitiveDerObject(num2, definiteLengthInputStream.ToArray());
					}
					int num5 = num2;
					if (num5 == 4)
					{
						return new BerOctetString(this.BuildDerEncodableVector(definiteLengthInputStream));
					}
					if (num5 == 8)
					{
						return new DerExternal(this.BuildDerEncodableVector(definiteLengthInputStream));
					}
					switch (num5)
					{
					case 16:
						return this.CreateDerSequence(definiteLengthInputStream);
					case 17:
						return this.CreateDerSet(definiteLengthInputStream);
					default:
						return new DerUnknownTag(true, num2, definiteLengthInputStream.ToArray());
					}
				}
			}
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0001D2AC File Offset: 0x0001C2AC
		internal static int ReadTagNumber(Stream s, int tag)
		{
			int num = tag & 31;
			if (num == 31)
			{
				num = 0;
				int num2 = s.ReadByte();
				if ((num2 & 127) == 0)
				{
					throw new IOException("Corrupted stream - invalid high tag number found");
				}
				while (num2 >= 0 && (num2 & 128) != 0)
				{
					num |= (num2 & 127);
					num <<= 7;
					num2 = s.ReadByte();
				}
				if (num2 < 0)
				{
					throw new EndOfStreamException("EOF found inside tag value.");
				}
				num |= (num2 & 127);
			}
			return num;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001D314 File Offset: 0x0001C314
		internal static int ReadLength(Stream s, int limit)
		{
			int num = s.ReadByte();
			if (num < 0)
			{
				throw new EndOfStreamException("EOF found when length expected");
			}
			if (num == 128)
			{
				return -1;
			}
			if (num > 127)
			{
				int num2 = num & 127;
				if (num2 > 4)
				{
					throw new IOException("DER length more than 4 bytes: " + num2);
				}
				num = 0;
				for (int i = 0; i < num2; i++)
				{
					int num3 = s.ReadByte();
					if (num3 < 0)
					{
						throw new EndOfStreamException("EOF found reading length");
					}
					num = (num << 8) + num3;
				}
				if (num < 0)
				{
					throw new IOException("Corrupted stream - negative length found");
				}
				if (num >= limit)
				{
					throw new IOException("Corrupted stream - out of bounds length found");
				}
			}
			return num;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001D3B0 File Offset: 0x0001C3B0
		internal static Asn1Object CreatePrimitiveDerObject(int tagNo, byte[] bytes)
		{
			switch (tagNo)
			{
			case 1:
				return new DerBoolean(bytes);
			case 2:
				return new DerInteger(bytes);
			case 3:
			{
				int padBits = (int)bytes[0];
				byte[] array = new byte[bytes.Length - 1];
				Array.Copy(bytes, 1, array, 0, bytes.Length - 1);
				return new DerBitString(array, padBits);
			}
			case 4:
				return new DerOctetString(bytes);
			case 5:
				return DerNull.Instance;
			case 6:
				return new DerObjectIdentifier(bytes);
			case 10:
				return new DerEnumerated(bytes);
			case 12:
				return new DerUtf8String(bytes);
			case 18:
				return new DerNumericString(bytes);
			case 19:
				return new DerPrintableString(bytes);
			case 20:
				return new DerT61String(bytes);
			case 22:
				return new DerIA5String(bytes);
			case 23:
				return new DerUtcTime(bytes);
			case 24:
				return new DerGeneralizedTime(bytes);
			case 26:
				return new DerVisibleString(bytes);
			case 27:
				return new DerGeneralString(bytes);
			case 28:
				return new DerUniversalString(bytes);
			case 30:
				return new DerBmpString(bytes);
			}
			return new DerUnknownTag(false, tagNo, bytes);
		}

		// Token: 0x040002BD RID: 701
		private readonly int limit;
	}
}
