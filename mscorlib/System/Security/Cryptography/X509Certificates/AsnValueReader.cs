using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008CC RID: 2252
	internal struct AsnValueReader
	{
		// Token: 0x0600522B RID: 21035 RVA: 0x00127033 File Offset: 0x00126033
		internal AsnValueReader(ReadOnlySpan<byte> span, AsnEncodingRules ruleSet)
		{
			this._span = span;
			this._ruleSet = ruleSet;
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x0600522C RID: 21036 RVA: 0x00127043 File Offset: 0x00126043
		internal bool HasData
		{
			get
			{
				return !this._span.IsEmpty;
			}
		}

		// Token: 0x0600522D RID: 21037 RVA: 0x00127053 File Offset: 0x00126053
		internal void ThrowIfNotEmpty()
		{
			if (!this._span.IsEmpty)
			{
				new AsnReader(AsnValueReader.s_singleByte, this._ruleSet).ThrowIfNotEmpty();
			}
		}

		// Token: 0x0600522E RID: 21038 RVA: 0x0012707C File Offset: 0x0012607C
		internal Asn1Tag PeekTag()
		{
			int num;
			return Asn1Tag.Decode(this._span, out num);
		}

		// Token: 0x0600522F RID: 21039 RVA: 0x00127098 File Offset: 0x00126098
		internal ReadOnlySpan<byte> PeekContentBytes()
		{
			int start;
			int length;
			int num;
			AsnDecoder.ReadEncodedValue(this._span, this._ruleSet, out start, out length, out num);
			return this._span.Slice(start, length);
		}

		// Token: 0x06005230 RID: 21040 RVA: 0x001270CC File Offset: 0x001260CC
		internal ReadOnlySpan<byte> PeekEncodedValue()
		{
			int num;
			int num2;
			int length;
			AsnDecoder.ReadEncodedValue(this._span, this._ruleSet, out num, out num2, out length);
			return this._span.Slice(0, length);
		}

		// Token: 0x06005231 RID: 21041 RVA: 0x00127100 File Offset: 0x00126100
		internal ReadOnlySpan<byte> ReadEncodedValue()
		{
			ReadOnlySpan<byte> result = this.PeekEncodedValue();
			this._span = this._span.Slice(result.Length);
			return result;
		}

		// Token: 0x06005232 RID: 21042 RVA: 0x00127130 File Offset: 0x00126130
		internal bool TryReadInt32(out int value)
		{
			return this.TryReadInt32(out value, null);
		}

		// Token: 0x06005233 RID: 21043 RVA: 0x00127150 File Offset: 0x00126150
		internal bool TryReadInt32(out int value, Asn1Tag? expectedTag)
		{
			int start;
			bool result = AsnDecoder.TryReadInt32(this._span, this._ruleSet, out value, out start, expectedTag);
			this._span = this._span.Slice(start);
			return result;
		}

		// Token: 0x06005234 RID: 21044 RVA: 0x00127188 File Offset: 0x00126188
		internal ReadOnlySpan<byte> ReadIntegerBytes()
		{
			return this.ReadIntegerBytes(null);
		}

		// Token: 0x06005235 RID: 21045 RVA: 0x001271A4 File Offset: 0x001261A4
		internal ReadOnlySpan<byte> ReadIntegerBytes(Asn1Tag? expectedTag)
		{
			int start;
			ReadOnlySpan<byte> result = AsnDecoder.ReadIntegerBytes(this._span, this._ruleSet, out start, expectedTag);
			this._span = this._span.Slice(start);
			return result;
		}

		// Token: 0x06005236 RID: 21046 RVA: 0x001271DC File Offset: 0x001261DC
		internal bool TryReadPrimitiveBitString(out int unusedBitCount, out ReadOnlySpan<byte> value)
		{
			return this.TryReadPrimitiveBitString(out unusedBitCount, out value, null);
		}

		// Token: 0x06005237 RID: 21047 RVA: 0x001271FC File Offset: 0x001261FC
		internal bool TryReadPrimitiveBitString(out int unusedBitCount, out ReadOnlySpan<byte> value, Asn1Tag? expectedTag)
		{
			int start;
			bool result = AsnDecoder.TryReadPrimitiveBitString(this._span, this._ruleSet, out unusedBitCount, out value, out start, expectedTag);
			this._span = this._span.Slice(start);
			return result;
		}

		// Token: 0x06005238 RID: 21048 RVA: 0x00127234 File Offset: 0x00126234
		internal byte[] ReadBitString(out int unusedBitCount)
		{
			return this.ReadBitString(out unusedBitCount, null);
		}

		// Token: 0x06005239 RID: 21049 RVA: 0x00127254 File Offset: 0x00126254
		internal byte[] ReadBitString(out int unusedBitCount, Asn1Tag? expectedTag)
		{
			int start;
			byte[] result = AsnDecoder.ReadBitString(this._span, this._ruleSet, out unusedBitCount, out start, expectedTag);
			this._span = this._span.Slice(start);
			return result;
		}

		// Token: 0x0600523A RID: 21050 RVA: 0x0012728C File Offset: 0x0012628C
		internal bool TryReadPrimitiveOctetString(out ReadOnlySpan<byte> value)
		{
			return this.TryReadPrimitiveOctetString(out value, null);
		}

		// Token: 0x0600523B RID: 21051 RVA: 0x001272AC File Offset: 0x001262AC
		internal bool TryReadPrimitiveOctetString(out ReadOnlySpan<byte> value, Asn1Tag? expectedTag)
		{
			int start;
			bool result = AsnDecoder.TryReadPrimitiveOctetString(this._span, this._ruleSet, out value, out start, expectedTag);
			this._span = this._span.Slice(start);
			return result;
		}

		// Token: 0x0600523C RID: 21052 RVA: 0x001272E4 File Offset: 0x001262E4
		internal byte[] ReadOctetString()
		{
			return this.ReadOctetString(null);
		}

		// Token: 0x0600523D RID: 21053 RVA: 0x00127300 File Offset: 0x00126300
		internal byte[] ReadOctetString(Asn1Tag? expectedTag)
		{
			int start;
			byte[] result = AsnDecoder.ReadOctetString(this._span, this._ruleSet, out start, expectedTag);
			this._span = this._span.Slice(start);
			return result;
		}

		// Token: 0x0600523E RID: 21054 RVA: 0x00127338 File Offset: 0x00126338
		internal byte[] ReadObjectIdentifier()
		{
			return this.ReadObjectIdentifier(null);
		}

		// Token: 0x0600523F RID: 21055 RVA: 0x00127354 File Offset: 0x00126354
		internal byte[] ReadObjectIdentifier(Asn1Tag? expectedTag)
		{
			int start;
			byte[] result = AsnDecoder.ReadObjectIdentifier(this._span, this._ruleSet, out start, expectedTag);
			this._span = this._span.Slice(start);
			return result;
		}

		// Token: 0x06005240 RID: 21056 RVA: 0x0012738C File Offset: 0x0012638C
		internal AsnValueReader ReadSequence()
		{
			return this.ReadSequence(null);
		}

		// Token: 0x06005241 RID: 21057 RVA: 0x001273A8 File Offset: 0x001263A8
		internal AsnValueReader ReadSequence(Asn1Tag? expectedTag)
		{
			int start;
			int length;
			int start2;
			AsnDecoder.ReadSequence(this._span, this._ruleSet, out start, out length, out start2, expectedTag);
			ReadOnlySpan<byte> span = this._span.Slice(start, length);
			this._span = this._span.Slice(start2);
			return new AsnValueReader(span, this._ruleSet);
		}

		// Token: 0x06005242 RID: 21058 RVA: 0x001273FC File Offset: 0x001263FC
		internal AsnValueReader ReadSetOf()
		{
			return this.ReadSetOf(null, false);
		}

		// Token: 0x06005243 RID: 21059 RVA: 0x00127419 File Offset: 0x00126419
		internal AsnValueReader ReadSetOf(Asn1Tag? expectedTag)
		{
			return this.ReadSetOf(expectedTag, false);
		}

		// Token: 0x06005244 RID: 21060 RVA: 0x00127424 File Offset: 0x00126424
		internal AsnValueReader ReadSetOf(Asn1Tag? expectedTag, bool skipSortOrderValidation)
		{
			int start;
			int length;
			int start2;
			AsnDecoder.ReadSetOf(this._span, this._ruleSet, out start, out length, out start2, skipSortOrderValidation, expectedTag);
			ReadOnlySpan<byte> span = this._span.Slice(start, length);
			this._span = this._span.Slice(start2);
			return new AsnValueReader(span, this._ruleSet);
		}

		// Token: 0x04002A55 RID: 10837
		private static readonly byte[] s_singleByte = new byte[1];

		// Token: 0x04002A56 RID: 10838
		private ReadOnlySpan<byte> _span;

		// Token: 0x04002A57 RID: 10839
		private readonly AsnEncodingRules _ruleSet;
	}
}
