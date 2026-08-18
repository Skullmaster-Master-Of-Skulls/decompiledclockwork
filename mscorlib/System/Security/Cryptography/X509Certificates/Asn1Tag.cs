using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008C3 RID: 2243
	internal struct Asn1Tag : IEquatable<Asn1Tag>
	{
		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x060051C1 RID: 20929 RVA: 0x00124EF9 File Offset: 0x00123EF9
		public TagClass TagClass
		{
			get
			{
				return (TagClass)(this._controlFlags & 192);
			}
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x060051C2 RID: 20930 RVA: 0x00124F07 File Offset: 0x00123F07
		public bool IsConstructed
		{
			get
			{
				return (this._controlFlags & 32) != 0;
			}
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x060051C3 RID: 20931 RVA: 0x00124F18 File Offset: 0x00123F18
		public int TagValue
		{
			get
			{
				return this._tagValue;
			}
		}

		// Token: 0x060051C4 RID: 20932 RVA: 0x00124F20 File Offset: 0x00123F20
		private Asn1Tag(byte controlFlags, int tagValue)
		{
			this._controlFlags = (controlFlags & 224);
			this._tagValue = tagValue;
		}

		// Token: 0x060051C5 RID: 20933 RVA: 0x00124F37 File Offset: 0x00123F37
		public Asn1Tag(UniversalTagNumber universalTagNumber, bool isConstructed)
		{
			this = new Asn1Tag(isConstructed ? 32 : 0, (int)universalTagNumber);
			if (universalTagNumber < UniversalTagNumber.EndOfContents || universalTagNumber > UniversalTagNumber.RelativeObjectIdentifierIRI || universalTagNumber == (UniversalTagNumber)15)
			{
				throw new ArgumentOutOfRangeException("universalTagNumber");
			}
		}

		// Token: 0x060051C6 RID: 20934 RVA: 0x00124F64 File Offset: 0x00123F64
		public Asn1Tag(TagClass tagClass, int tagValue, bool isConstructed)
		{
			this = new Asn1Tag((byte)tagClass | (isConstructed ? 32 : 0), tagValue);
			if (tagClass <= TagClass.Application)
			{
				if (tagClass == TagClass.Universal || tagClass == TagClass.Application)
				{
					goto IL_40;
				}
			}
			else if (tagClass == TagClass.ContextSpecific || tagClass == TagClass.Private)
			{
				goto IL_40;
			}
			throw new ArgumentOutOfRangeException("tagClass");
			IL_40:
			if (tagValue < 0)
			{
				throw new ArgumentOutOfRangeException("tagValue");
			}
		}

		// Token: 0x060051C7 RID: 20935 RVA: 0x00124FC0 File Offset: 0x00123FC0
		public Asn1Tag(TagClass tagClass, int tagValue)
		{
			this = new Asn1Tag(tagClass, tagValue, false);
		}

		// Token: 0x060051C8 RID: 20936 RVA: 0x00124FCB File Offset: 0x00123FCB
		public Asn1Tag AsConstructed()
		{
			return new Asn1Tag(this._controlFlags | 32, this.TagValue);
		}

		// Token: 0x060051C9 RID: 20937 RVA: 0x00124FE4 File Offset: 0x00123FE4
		public static bool TryDecode(ReadOnlySpan<byte> source, out Asn1Tag tag, out int bytesConsumed)
		{
			tag = default(Asn1Tag);
			bytesConsumed = 0;
			if (source.IsEmpty)
			{
				return false;
			}
			byte b = source[bytesConsumed];
			bytesConsumed++;
			uint num = (uint)(b & 31);
			if (num == 31U)
			{
				num = 0U;
				while (source.Length > bytesConsumed)
				{
					byte b2 = source[bytesConsumed];
					byte b3 = b2 & 127;
					bytesConsumed++;
					if (num >= 33554432U)
					{
						bytesConsumed = 0;
						return false;
					}
					num <<= 7;
					num |= (uint)b3;
					if (num == 0U)
					{
						bytesConsumed = 0;
						return false;
					}
					if ((b2 & 128) != 128)
					{
						if (num <= 30U)
						{
							bytesConsumed = 0;
							return false;
						}
						if (num > 2147483647U)
						{
							bytesConsumed = 0;
							return false;
						}
						goto IL_99;
					}
				}
				bytesConsumed = 0;
				return false;
			}
			IL_99:
			tag = new Asn1Tag(b, (int)num);
			return true;
		}

		// Token: 0x060051CA RID: 20938 RVA: 0x00125098 File Offset: 0x00124098
		public static Asn1Tag Decode(ReadOnlySpan<byte> source, out int bytesConsumed)
		{
			Asn1Tag result;
			if (Asn1Tag.TryDecode(source, out result, out bytesConsumed))
			{
				return result;
			}
			throw new InvalidOperationException("The provided data does not represent a valid tag.");
		}

		// Token: 0x060051CB RID: 20939 RVA: 0x001250BC File Offset: 0x001240BC
		public bool Equals(Asn1Tag other)
		{
			return this._controlFlags == other._controlFlags && this.TagValue == other.TagValue;
		}

		// Token: 0x060051CC RID: 20940 RVA: 0x001250DE File Offset: 0x001240DE
		public override bool Equals(object obj)
		{
			return obj is Asn1Tag && this.Equals((Asn1Tag)obj);
		}

		// Token: 0x060051CD RID: 20941 RVA: 0x001250F6 File Offset: 0x001240F6
		public override int GetHashCode()
		{
			return (int)this._controlFlags << 24 ^ this.TagValue;
		}

		// Token: 0x060051CE RID: 20942 RVA: 0x00125108 File Offset: 0x00124108
		public static bool operator ==(Asn1Tag left, Asn1Tag right)
		{
			return left.Equals(right);
		}

		// Token: 0x060051CF RID: 20943 RVA: 0x00125112 File Offset: 0x00124112
		public static bool operator !=(Asn1Tag left, Asn1Tag right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060051D0 RID: 20944 RVA: 0x0012511F File Offset: 0x0012411F
		public bool HasSameClassAndValue(Asn1Tag other)
		{
			return this.TagValue == other.TagValue && this.TagClass == other.TagClass;
		}

		// Token: 0x04002A28 RID: 10792
		private const byte ClassMask = 192;

		// Token: 0x04002A29 RID: 10793
		private const byte ConstructedMask = 32;

		// Token: 0x04002A2A RID: 10794
		private const byte ControlMask = 224;

		// Token: 0x04002A2B RID: 10795
		private const byte TagNumberMask = 31;

		// Token: 0x04002A2C RID: 10796
		internal static readonly Asn1Tag EndOfContents = new Asn1Tag(0, 0);

		// Token: 0x04002A2D RID: 10797
		public static readonly Asn1Tag Integer = new Asn1Tag(0, 2);

		// Token: 0x04002A2E RID: 10798
		public static readonly Asn1Tag PrimitiveBitString = new Asn1Tag(0, 3);

		// Token: 0x04002A2F RID: 10799
		public static readonly Asn1Tag ConstructedBitString = new Asn1Tag(32, 3);

		// Token: 0x04002A30 RID: 10800
		public static readonly Asn1Tag PrimitiveOctetString = new Asn1Tag(0, 4);

		// Token: 0x04002A31 RID: 10801
		public static readonly Asn1Tag ConstructedOctetString = new Asn1Tag(32, 4);

		// Token: 0x04002A32 RID: 10802
		public static readonly Asn1Tag Null = new Asn1Tag(0, 5);

		// Token: 0x04002A33 RID: 10803
		public static readonly Asn1Tag ObjectIdentifier = new Asn1Tag(0, 6);

		// Token: 0x04002A34 RID: 10804
		public static readonly Asn1Tag Sequence = new Asn1Tag(32, 16);

		// Token: 0x04002A35 RID: 10805
		public static readonly Asn1Tag SetOf = new Asn1Tag(32, 17);

		// Token: 0x04002A36 RID: 10806
		private readonly byte _controlFlags;

		// Token: 0x04002A37 RID: 10807
		private int _tagValue;
	}
}
