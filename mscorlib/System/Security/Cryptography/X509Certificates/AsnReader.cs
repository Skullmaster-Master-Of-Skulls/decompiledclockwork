using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008C9 RID: 2249
	internal class AsnReader
	{
		// Token: 0x0600520D RID: 21005 RVA: 0x00126A88 File Offset: 0x00125A88
		public bool TryReadPrimitiveBitString(out int unusedBitCount, out ReadOnlyMemory<byte> value, Asn1Tag? expectedTag)
		{
			ReadOnlySpan<byte> smaller;
			int start;
			bool flag = AsnDecoder.TryReadPrimitiveBitString(this._data.Span, this.RuleSet, out unusedBitCount, out smaller, out start, expectedTag);
			if (flag)
			{
				value = AsnDecoder.Slice(this._data, smaller);
				this._data = this._data.Slice(start);
			}
			else
			{
				value = default(ReadOnlyMemory<byte>);
			}
			return flag;
		}

		// Token: 0x0600520E RID: 21006 RVA: 0x00126AE4 File Offset: 0x00125AE4
		public bool TryReadBitString(Span<byte> destination, out int unusedBitCount, out int bytesWritten, Asn1Tag? expectedTag)
		{
			int start;
			bool flag = AsnDecoder.TryReadBitString(this._data.Span, destination, this.RuleSet, out unusedBitCount, out start, out bytesWritten, expectedTag);
			if (flag)
			{
				this._data = this._data.Slice(start);
			}
			return flag;
		}

		// Token: 0x0600520F RID: 21007 RVA: 0x00126B28 File Offset: 0x00125B28
		public byte[] ReadBitString(out int unusedBitCount, Asn1Tag? expectedTag)
		{
			int start;
			byte[] result = AsnDecoder.ReadBitString(this._data.Span, this.RuleSet, out unusedBitCount, out start, expectedTag);
			this._data = this._data.Slice(start);
			return result;
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x06005210 RID: 21008 RVA: 0x00126B63 File Offset: 0x00125B63
		public AsnEncodingRules RuleSet
		{
			get
			{
				return this._ruleSet;
			}
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06005211 RID: 21009 RVA: 0x00126B6B File Offset: 0x00125B6B
		public bool HasData
		{
			get
			{
				return !this._data.IsEmpty;
			}
		}

		// Token: 0x06005212 RID: 21010 RVA: 0x00126B7B File Offset: 0x00125B7B
		public AsnReader(ReadOnlyMemory<byte> data, AsnEncodingRules ruleSet, AsnReaderOptions options)
		{
			AsnDecoder.CheckEncodingRules(ruleSet);
			this._data = data;
			this._ruleSet = ruleSet;
			this._options = options;
		}

		// Token: 0x06005213 RID: 21011 RVA: 0x00126BA0 File Offset: 0x00125BA0
		public AsnReader(ReadOnlyMemory<byte> data, AsnEncodingRules ruleSet) : this(data, ruleSet, default(AsnReaderOptions))
		{
		}

		// Token: 0x06005214 RID: 21012 RVA: 0x00126BBE File Offset: 0x00125BBE
		public void ThrowIfNotEmpty()
		{
			if (this.HasData)
			{
				throw new InvalidOperationException("The last expected value has been read, but the reader still has pending data. This value may be from a newer schema, or is corrupt.");
			}
		}

		// Token: 0x06005215 RID: 21013 RVA: 0x00126BD4 File Offset: 0x00125BD4
		public Asn1Tag PeekTag()
		{
			int num;
			return Asn1Tag.Decode(this._data.Span, out num);
		}

		// Token: 0x06005216 RID: 21014 RVA: 0x00126BF4 File Offset: 0x00125BF4
		public ReadOnlyMemory<byte> PeekEncodedValue()
		{
			int num;
			int num2;
			int length;
			AsnDecoder.ReadEncodedValue(this._data.Span, this.RuleSet, out num, out num2, out length);
			return this._data.Slice(0, length);
		}

		// Token: 0x06005217 RID: 21015 RVA: 0x00126C2C File Offset: 0x00125C2C
		public ReadOnlyMemory<byte> PeekContentBytes()
		{
			int start;
			int length;
			int num;
			AsnDecoder.ReadEncodedValue(this._data.Span, this.RuleSet, out start, out length, out num);
			return this._data.Slice(start, length);
		}

		// Token: 0x06005218 RID: 21016 RVA: 0x00126C64 File Offset: 0x00125C64
		public ReadOnlyMemory<byte> ReadEncodedValue()
		{
			ReadOnlyMemory<byte> result = this.PeekEncodedValue();
			this._data = this._data.Slice(result.Length);
			return result;
		}

		// Token: 0x06005219 RID: 21017 RVA: 0x00126C91 File Offset: 0x00125C91
		private AsnReader CloneAtSlice(int start, int length)
		{
			return new AsnReader(this._data.Slice(start, length), this.RuleSet, this._options);
		}

		// Token: 0x0600521A RID: 21018 RVA: 0x00126CB4 File Offset: 0x00125CB4
		public ReadOnlyMemory<byte> ReadIntegerBytes(Asn1Tag? expectedTag)
		{
			int start;
			ReadOnlySpan<byte> smaller = AsnDecoder.ReadIntegerBytes(this._data.Span, this.RuleSet, out start, expectedTag);
			ReadOnlyMemory<byte> result = AsnDecoder.Slice(this._data, smaller);
			this._data = this._data.Slice(start);
			return result;
		}

		// Token: 0x0600521B RID: 21019 RVA: 0x00126CFC File Offset: 0x00125CFC
		public bool TryReadInt32(out int value, Asn1Tag? expectedTag)
		{
			int start;
			bool result = AsnDecoder.TryReadInt32(this._data.Span, this.RuleSet, out value, out start, expectedTag);
			this._data = this._data.Slice(start);
			return result;
		}

		// Token: 0x0600521C RID: 21020 RVA: 0x00126D38 File Offset: 0x00125D38
		public bool TryReadUInt32(out uint value, Asn1Tag? expectedTag)
		{
			int start;
			bool result = AsnDecoder.TryReadUInt32(this._data.Span, this.RuleSet, out value, out start, expectedTag);
			this._data = this._data.Slice(start);
			return result;
		}

		// Token: 0x0600521D RID: 21021 RVA: 0x00126D74 File Offset: 0x00125D74
		public bool TryReadInt64(out long value, Asn1Tag? expectedTag)
		{
			int start;
			bool result = AsnDecoder.TryReadInt64(this._data.Span, this.RuleSet, out value, out start, expectedTag);
			this._data = this._data.Slice(start);
			return result;
		}

		// Token: 0x0600521E RID: 21022 RVA: 0x00126DB0 File Offset: 0x00125DB0
		public bool TryReadUInt64(out ulong value, Asn1Tag? expectedTag)
		{
			int start;
			bool result = AsnDecoder.TryReadUInt64(this._data.Span, this.RuleSet, out value, out start, expectedTag);
			this._data = this._data.Slice(start);
			return result;
		}

		// Token: 0x0600521F RID: 21023 RVA: 0x00126DEC File Offset: 0x00125DEC
		public void ReadNull(Asn1Tag? expectedTag)
		{
			int start;
			AsnDecoder.ReadNull(this._data.Span, this.RuleSet, out start, expectedTag);
			this._data = this._data.Slice(start);
		}

		// Token: 0x06005220 RID: 21024 RVA: 0x00126E24 File Offset: 0x00125E24
		public bool TryReadOctetString(Span<byte> destination, out int bytesWritten, Asn1Tag? expectedTag)
		{
			int start;
			bool flag = AsnDecoder.TryReadOctetString(this._data.Span, destination, this.RuleSet, out start, out bytesWritten, expectedTag);
			if (flag)
			{
				this._data = this._data.Slice(start);
			}
			return flag;
		}

		// Token: 0x06005221 RID: 21025 RVA: 0x00126E64 File Offset: 0x00125E64
		public byte[] ReadOctetString(Asn1Tag? expectedTag)
		{
			int start;
			byte[] result = AsnDecoder.ReadOctetString(this._data.Span, this.RuleSet, out start, expectedTag);
			this._data = this._data.Slice(start);
			return result;
		}

		// Token: 0x06005222 RID: 21026 RVA: 0x00126EA0 File Offset: 0x00125EA0
		public bool TryReadPrimitiveOctetString(out ReadOnlyMemory<byte> contents, Asn1Tag? expectedTag)
		{
			ReadOnlySpan<byte> smaller;
			int start;
			bool flag = AsnDecoder.TryReadPrimitiveOctetString(this._data.Span, this.RuleSet, out smaller, out start, expectedTag);
			if (flag)
			{
				contents = AsnDecoder.Slice(this._data, smaller);
				this._data = this._data.Slice(start);
			}
			else
			{
				contents = default(ReadOnlyMemory<byte>);
			}
			return flag;
		}

		// Token: 0x06005223 RID: 21027 RVA: 0x00126EFC File Offset: 0x00125EFC
		public byte[] ReadObjectIdentifier(Asn1Tag? expectedTag)
		{
			int start;
			byte[] result = AsnDecoder.ReadObjectIdentifier(this._data.Span, this.RuleSet, out start, expectedTag);
			this._data = this._data.Slice(start);
			return result;
		}

		// Token: 0x06005224 RID: 21028 RVA: 0x00126F38 File Offset: 0x00125F38
		public AsnReader ReadSequence(Asn1Tag? expectedTag)
		{
			int start;
			int length;
			int start2;
			AsnDecoder.ReadSequence(this._data.Span, this.RuleSet, out start, out length, out start2, expectedTag);
			AsnReader result = this.CloneAtSlice(start, length);
			this._data = this._data.Slice(start2);
			return result;
		}

		// Token: 0x06005225 RID: 21029 RVA: 0x00126F80 File Offset: 0x00125F80
		public AsnReader ReadSetOf(Asn1Tag? expectedTag)
		{
			return this.ReadSetOf(this._options.SkipSetSortOrderVerification, expectedTag);
		}

		// Token: 0x06005226 RID: 21030 RVA: 0x00126FA4 File Offset: 0x00125FA4
		public AsnReader ReadSetOf(bool skipSortOrderValidation, Asn1Tag? expectedTag)
		{
			int start;
			int length;
			int start2;
			AsnDecoder.ReadSetOf(this._data.Span, this.RuleSet, out start, out length, out start2, skipSortOrderValidation, expectedTag);
			AsnReader result = this.CloneAtSlice(start, length);
			this._data = this._data.Slice(start2);
			return result;
		}

		// Token: 0x04002A4A RID: 10826
		internal const int MaxCERSegmentSize = 1000;

		// Token: 0x04002A4B RID: 10827
		private ReadOnlyMemory<byte> _data;

		// Token: 0x04002A4C RID: 10828
		private readonly AsnReaderOptions _options;

		// Token: 0x04002A4D RID: 10829
		private AsnEncodingRules _ruleSet;
	}
}
