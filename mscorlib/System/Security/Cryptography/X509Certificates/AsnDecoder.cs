using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008C4 RID: 2244
	internal static class AsnDecoder
	{
		// Token: 0x060051D2 RID: 20946 RVA: 0x001251D0 File Offset: 0x001241D0
		public static bool TryReadPrimitiveBitString(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, out int unusedBitCount, out ReadOnlySpan<byte> value, out int bytesConsumed, Asn1Tag? expectedTag)
		{
			int? num;
			int num2;
			int num3;
			ReadOnlySpan<byte> readOnlySpan;
			int num4;
			byte b;
			if (AsnDecoder.TryReadPrimitiveBitStringCore(source, ruleSet, expectedTag ?? Asn1Tag.PrimitiveBitString, out num, out num2, out num3, out readOnlySpan, out num4, out b) && (readOnlySpan.Length == 0 || b == readOnlySpan[readOnlySpan.Length - 1]))
			{
				unusedBitCount = num3;
				value = readOnlySpan;
				bytesConsumed = num4;
				return true;
			}
			unusedBitCount = 0;
			value = default(ReadOnlySpan<byte>);
			bytesConsumed = 0;
			return false;
		}

		// Token: 0x060051D3 RID: 20947 RVA: 0x0012524C File Offset: 0x0012424C
		public static bool TryReadBitString(ReadOnlySpan<byte> source, Span<byte> destination, AsnEncodingRules ruleSet, out int unusedBitCount, out int bytesConsumed, out int bytesWritten, Asn1Tag? expectedTag)
		{
			if (source.Overlaps(destination))
			{
				throw new ArgumentException("The destination buffer overlaps the source buffer.", "destination");
			}
			int? length;
			int num;
			int num2;
			ReadOnlySpan<byte> value;
			int num3;
			byte normalizedLastByte;
			if (AsnDecoder.TryReadPrimitiveBitStringCore(source, ruleSet, expectedTag ?? Asn1Tag.PrimitiveBitString, out length, out num, out num2, out value, out num3, out normalizedLastByte))
			{
				if (value.Length > destination.Length)
				{
					bytesConsumed = 0;
					bytesWritten = 0;
					unusedBitCount = 0;
					return false;
				}
				AsnDecoder.CopyBitStringValue(value, normalizedLastByte, destination);
				bytesWritten = value.Length;
				bytesConsumed = num3;
				unusedBitCount = num2;
				return true;
			}
			else
			{
				int num4;
				int num5;
				if (AsnDecoder.TryCopyConstructedBitStringValue(AsnDecoder.Slice(source, num, length), ruleSet, destination, length == null, out num2, out num4, out num5))
				{
					unusedBitCount = num2;
					bytesConsumed = num + num4;
					bytesWritten = num5;
					return true;
				}
				bytesWritten = (bytesConsumed = (unusedBitCount = 0));
				return false;
			}
		}

		// Token: 0x060051D4 RID: 20948 RVA: 0x0012532C File Offset: 0x0012432C
		public static byte[] ReadBitString(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, out int unusedBitCount, out int bytesConsumed, Asn1Tag? expectedTag)
		{
			int? num;
			int num2;
			int num3;
			ReadOnlySpan<byte> readOnlySpan;
			int num4;
			byte b;
			if (AsnDecoder.TryReadPrimitiveBitStringCore(source, ruleSet, expectedTag ?? Asn1Tag.PrimitiveBitString, out num, out num2, out num3, out readOnlySpan, out num4, out b))
			{
				byte[] array = readOnlySpan.ToArray();
				if (readOnlySpan.Length > 0)
				{
					array[array.Length - 1] = b;
				}
				unusedBitCount = num3;
				bytesConsumed = num4;
				return array;
			}
			int size = num ?? AsnDecoder.SeekEndOfContents(source.Slice(num2), ruleSet);
			byte[] array2 = CryptoPool.Rent(size);
			int num5;
			int num6;
			if (AsnDecoder.TryCopyConstructedBitStringValue(AsnDecoder.Slice(source, num2, num), ruleSet, array2, num == null, out num3, out num5, out num6))
			{
				byte[] result = Utility.GetSpanForArray<byte>(array2, 0, num6).ToArray();
				CryptoPool.Return(array2, num6);
				unusedBitCount = num3;
				bytesConsumed = num2 + num5;
				return result;
			}
			throw new InvalidOperationException("TryCopyConstructedBitStringValue failed with a pre-allocated buffer");
		}

		// Token: 0x060051D5 RID: 20949 RVA: 0x0012541C File Offset: 0x0012441C
		private static void ParsePrimitiveBitStringContents(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, out int unusedBitCount, out ReadOnlySpan<byte> value, out byte normalizedLastByte)
		{
			if (ruleSet == AsnEncodingRules.CER && source.Length > 1000)
			{
				throw new InvalidOperationException("The encoded value is not valid under the selected encoding, but it may be valid under the BER or DER encoding.");
			}
			if (source.Length == 0)
			{
				throw new InvalidOperationException();
			}
			unusedBitCount = (int)source[0];
			if (unusedBitCount > 7)
			{
				throw new InvalidOperationException();
			}
			if (source.Length == 1)
			{
				if (unusedBitCount > 0)
				{
					throw new InvalidOperationException();
				}
				value = ReadOnlySpan<byte>.Empty;
				normalizedLastByte = 0;
				return;
			}
			else
			{
				int num = -1 << unusedBitCount;
				byte b = source[source.Length - 1];
				byte b2 = (byte)((int)b & num);
				if (b2 != b && (ruleSet == AsnEncodingRules.DER || ruleSet == AsnEncodingRules.CER))
				{
					throw new InvalidOperationException("The encoded value is not valid under the selected encoding, but it may be valid under the BER encoding.");
				}
				normalizedLastByte = b2;
				value = source.Slice(1);
				return;
			}
		}

		// Token: 0x060051D6 RID: 20950 RVA: 0x001254DB File Offset: 0x001244DB
		private static void CopyBitStringValue(ReadOnlySpan<byte> value, byte normalizedLastByte, Span<byte> destination)
		{
			if (value.Length == 0)
			{
				return;
			}
			value.CopyTo(destination);
			destination[value.Length - 1] = normalizedLastByte;
		}

		// Token: 0x060051D7 RID: 20951 RVA: 0x00125500 File Offset: 0x00124500
		private static int CountConstructedBitString(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, bool isIndefinite)
		{
			Span<byte> empty = Span<byte>.Empty;
			int num;
			int num2;
			return AsnDecoder.ProcessConstructedBitString(source, ruleSet, empty, null, isIndefinite, out num, out num2);
		}

		// Token: 0x060051D8 RID: 20952 RVA: 0x00125524 File Offset: 0x00124524
		private static void CopyConstructedBitString(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, Span<byte> destination, bool isIndefinite, out int unusedBitCount, out int bytesRead, out int bytesWritten)
		{
			bytesWritten = AsnDecoder.ProcessConstructedBitString(source, ruleSet, destination, new AsnDecoder.BitStringCopyAction(AsnDecoder.CopyBitStringValue), isIndefinite, out unusedBitCount, out bytesRead);
		}

		// Token: 0x060051D9 RID: 20953 RVA: 0x00125550 File Offset: 0x00124550
		private static int ProcessConstructedBitString(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, Span<byte> destination, AsnDecoder.BitStringCopyAction copyAction, bool isIndefinite, out int lastUnusedBitCount, out int bytesRead)
		{
			lastUnusedBitCount = 0;
			bytesRead = 0;
			int num = 1000;
			ReadOnlySpan<byte> readOnlySpan = source;
			Stack stack = null;
			int num2 = 0;
			Asn1Tag asn1Tag = Asn1Tag.ConstructedBitString;
			Span<byte> destination2 = destination;
			for (;;)
			{
				if (!readOnlySpan.IsEmpty)
				{
					int? length;
					int num3;
					asn1Tag = AsnDecoder.ReadTagAndLength(readOnlySpan, ruleSet, out length, out num3);
					if (asn1Tag == Asn1Tag.PrimitiveBitString)
					{
						if (lastUnusedBitCount != 0)
						{
							break;
						}
						if (ruleSet == AsnEncodingRules.CER && num != 1000)
						{
							goto Block_4;
						}
						ReadOnlySpan<byte> source2 = AsnDecoder.Slice(readOnlySpan, num3, length.Value);
						ReadOnlySpan<byte> value;
						byte normalizedLastByte;
						AsnDecoder.ParsePrimitiveBitStringContents(source2, ruleSet, out lastUnusedBitCount, out value, out normalizedLastByte);
						int num4 = num3 + source2.Length;
						readOnlySpan = readOnlySpan.Slice(num4);
						bytesRead += num4;
						num2 += value.Length;
						num = source2.Length;
						if (copyAction != null)
						{
							copyAction(value, normalizedLastByte, destination2);
							destination2 = destination2.Slice(value.Length);
							continue;
						}
						continue;
					}
					else if (asn1Tag == Asn1Tag.EndOfContents && isIndefinite)
					{
						AsnDecoder.ValidateEndOfContents(asn1Tag, length, num3);
						bytesRead += num3;
						if (stack != null && stack.Count > 0)
						{
							AsnDecoder.ParseFrame parseFrame = (AsnDecoder.ParseFrame)stack.Pop();
							readOnlySpan = source.Slice(parseFrame.Offset, parseFrame.Length).Slice(bytesRead);
							bytesRead += parseFrame.BytesRead;
							isIndefinite = parseFrame.Indefinite;
							continue;
						}
					}
					else
					{
						if (!(asn1Tag == Asn1Tag.ConstructedBitString))
						{
							goto IL_1CB;
						}
						if (ruleSet == AsnEncodingRules.CER)
						{
							goto Block_11;
						}
						if (stack == null)
						{
							stack = new Stack();
						}
						int offset;
						if (!source.Overlaps(readOnlySpan, out offset))
						{
							goto Block_13;
						}
						stack.Push(new AsnDecoder.ParseFrame(offset, readOnlySpan.Length, isIndefinite, bytesRead));
						readOnlySpan = AsnDecoder.Slice(readOnlySpan, num3, length);
						bytesRead = num3;
						isIndefinite = (length == null);
						continue;
					}
				}
				if (isIndefinite && asn1Tag != Asn1Tag.EndOfContents)
				{
					goto Block_15;
				}
				if (stack == null || stack.Count <= 0)
				{
					return num2;
				}
				AsnDecoder.ParseFrame parseFrame2 = (AsnDecoder.ParseFrame)stack.Pop();
				readOnlySpan = source.Slice(parseFrame2.Offset, parseFrame2.Length).Slice(bytesRead);
				isIndefinite = parseFrame2.Indefinite;
				bytesRead += parseFrame2.BytesRead;
			}
			throw new InvalidOperationException();
			Block_4:
			throw new InvalidOperationException("The encoded value is not valid under the selected encoding, but it may be valid under the BER or DER encoding.");
			Block_11:
			throw new InvalidOperationException("The encoded value is not valid under the selected encoding, but it may be valid under the BER encoding.");
			Block_13:
			throw new InvalidOperationException();
			IL_1CB:
			throw new InvalidOperationException();
			Block_15:
			throw new InvalidOperationException();
		}

		// Token: 0x060051DA RID: 20954 RVA: 0x001257A8 File Offset: 0x001247A8
		private static bool TryCopyConstructedBitStringValue(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, Span<byte> dest, bool isIndefinite, out int unusedBitCount, out int bytesRead, out int bytesWritten)
		{
			int num = AsnDecoder.CountConstructedBitString(source, ruleSet, isIndefinite);
			if (ruleSet == AsnEncodingRules.CER && num < 1000)
			{
				throw new InvalidOperationException("The encoded value is not valid under the selected encoding, but it may be valid under the BER encoding.");
			}
			if (dest.Length < num)
			{
				unusedBitCount = 0;
				bytesRead = 0;
				bytesWritten = 0;
				return false;
			}
			AsnDecoder.CopyConstructedBitString(source, ruleSet, dest, isIndefinite, out unusedBitCount, out bytesRead, out bytesWritten);
			return true;
		}

		// Token: 0x060051DB RID: 20955 RVA: 0x00125800 File Offset: 0x00124800
		private static bool TryReadPrimitiveBitStringCore(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, Asn1Tag expectedTag, out int? contentsLength, out int headerLength, out int unusedBitCount, out ReadOnlySpan<byte> value, out int bytesConsumed, out byte normalizedLastByte)
		{
			Asn1Tag tag = AsnDecoder.ReadTagAndLength(source, ruleSet, out contentsLength, out headerLength);
			AsnDecoder.CheckExpectedTag(tag, expectedTag, UniversalTagNumber.BitString);
			ReadOnlySpan<byte> source2 = AsnDecoder.Slice(source, headerLength, contentsLength);
			if (!tag.IsConstructed)
			{
				AsnDecoder.ParsePrimitiveBitStringContents(source2, ruleSet, out unusedBitCount, out value, out normalizedLastByte);
				bytesConsumed = headerLength + source2.Length;
				return true;
			}
			if (ruleSet == AsnEncodingRules.DER)
			{
				throw new InvalidOperationException("The encoded value is not valid under the selected encoding, but it may be valid under the BER or CER encoding.");
			}
			unusedBitCount = 0;
			value = default(ReadOnlySpan<byte>);
			normalizedLastByte = 0;
			bytesConsumed = 0;
			return false;
		}

		// Token: 0x060051DC RID: 20956 RVA: 0x0012587C File Offset: 0x0012487C
		public static bool TryReadEncodedValue(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, out Asn1Tag tag, out int contentOffset, out int contentLength, out int bytesConsumed)
		{
			AsnDecoder.CheckEncodingRules(ruleSet);
			Asn1Tag asn1Tag;
			int num;
			int? encodedLength;
			int num2;
			if (Asn1Tag.TryDecode(source, out asn1Tag, out num) && AsnDecoder.TryReadLength(source.Slice(num), ruleSet, out encodedLength, out num2))
			{
				int num3 = num + num2;
				int num4;
				int num5;
				AsnDecoder.LengthValidity lengthValidity = AsnDecoder.ValidateLength(source.Slice(num3), ruleSet, asn1Tag, encodedLength, out num4, out num5);
				if (lengthValidity == AsnDecoder.LengthValidity.Valid)
				{
					tag = asn1Tag;
					contentOffset = num3;
					contentLength = num4;
					bytesConsumed = num3 + num5;
					return true;
				}
			}
			tag = default(Asn1Tag);
			contentOffset = (contentLength = (bytesConsumed = 0));
			return false;
		}

		// Token: 0x060051DD RID: 20957 RVA: 0x00125908 File Offset: 0x00124908
		public static Asn1Tag ReadEncodedValue(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, out int contentOffset, out int contentLength, out int bytesConsumed)
		{
			AsnDecoder.CheckEncodingRules(ruleSet);
			int num;
			Asn1Tag asn1Tag = Asn1Tag.Decode(source, out num);
			int num2;
			int? encodedLength = AsnDecoder.ReadLength(source.Slice(num), ruleSet, out num2);
			int num3 = num + num2;
			int num4;
			int num5;
			AsnDecoder.LengthValidity lengthValidity = AsnDecoder.ValidateLength(source.Slice(num3), ruleSet, asn1Tag, encodedLength, out num4, out num5);
			if (lengthValidity == AsnDecoder.LengthValidity.Valid)
			{
				contentOffset = num3;
				contentLength = num4;
				bytesConsumed = num3 + num5;
				return asn1Tag;
			}
			throw AsnDecoder.GetValidityException(lengthValidity);
		}

		// Token: 0x060051DE RID: 20958 RVA: 0x00125970 File Offset: 0x00124970
		private static ReadOnlySpan<byte> GetPrimitiveContentSpan(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, Asn1Tag expectedTag, UniversalTagNumber tagNumber, out int bytesConsumed)
		{
			AsnDecoder.CheckEncodingRules(ruleSet);
			int num;
			Asn1Tag tag = Asn1Tag.Decode(source, out num);
			int num3;
			int? num2 = AsnDecoder.ReadLength(source.Slice(num), ruleSet, out num3);
			int num4 = num + num3;
			AsnDecoder.CheckExpectedTag(tag, expectedTag, tagNumber);
			if (tag.IsConstructed)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The encoded value uses a constructed encoding, which is invalid for '{0}' values.", new object[]
				{
					tagNumber
				}));
			}
			if (num2 == null)
			{
				throw new InvalidOperationException();
			}
			ReadOnlySpan<byte> result = AsnDecoder.Slice(source, num4, num2.Value);
			bytesConsumed = num4 + result.Length;
			return result;
		}

		// Token: 0x060051DF RID: 20959 RVA: 0x00125A0B File Offset: 0x00124A0B
		private static bool TryReadLength(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, out int? length, out int bytesRead)
		{
			return AsnDecoder.DecodeLength(source, ruleSet, out length, out bytesRead) == AsnDecoder.LengthDecodeStatus.Success;
		}

		// Token: 0x060051E0 RID: 20960 RVA: 0x00125A1C File Offset: 0x00124A1C
		private static int? ReadLength(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, out int bytesConsumed)
		{
			int? result;
			switch (AsnDecoder.DecodeLength(source, ruleSet, out result, out bytesConsumed))
			{
			case AsnDecoder.LengthDecodeStatus.DerIndefinite:
			case AsnDecoder.LengthDecodeStatus.LaxEncodingProhibited:
				throw new InvalidOperationException("The encoded length is not valid under the requested encoding rules, the value may be valid under the BER encoding.");
			case AsnDecoder.LengthDecodeStatus.LengthTooBig:
				throw new InvalidOperationException("The encoded length exceeds the maximum supported by this library (Int32.MaxValue).");
			case AsnDecoder.LengthDecodeStatus.Success:
				return result;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060051E1 RID: 20961 RVA: 0x00125A74 File Offset: 0x00124A74
		private static AsnDecoder.LengthDecodeStatus DecodeLength(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, out int? length, out int bytesRead)
		{
			length = null;
			bytesRead = 0;
			if (source.IsEmpty)
			{
				return AsnDecoder.LengthDecodeStatus.NeedMoreData;
			}
			byte b = source[bytesRead];
			bytesRead++;
			if (b == 128)
			{
				if (ruleSet == AsnEncodingRules.DER)
				{
					bytesRead = 0;
					return AsnDecoder.LengthDecodeStatus.DerIndefinite;
				}
				return AsnDecoder.LengthDecodeStatus.Success;
			}
			else
			{
				if (b < 128)
				{
					length = new int?((int)b);
					return AsnDecoder.LengthDecodeStatus.Success;
				}
				if (b == 255)
				{
					bytesRead = 0;
					return AsnDecoder.LengthDecodeStatus.ReservedValue;
				}
				byte b2 = (byte)((int)b & -129);
				if ((int)(b2 + 1) > source.Length)
				{
					bytesRead = 0;
					return AsnDecoder.LengthDecodeStatus.NeedMoreData;
				}
				bool flag = ruleSet == AsnEncodingRules.DER || ruleSet == AsnEncodingRules.CER;
				if (flag && b2 > 4)
				{
					bytesRead = 0;
					return AsnDecoder.LengthDecodeStatus.LengthTooBig;
				}
				uint num = 0U;
				for (int i = 0; i < (int)b2; i++)
				{
					byte b3 = source[bytesRead];
					bytesRead++;
					if (num == 0U)
					{
						if (flag && b3 == 0)
						{
							bytesRead = 0;
							return AsnDecoder.LengthDecodeStatus.LaxEncodingProhibited;
						}
						if (!flag && b3 != 0 && (int)b2 - i > 4)
						{
							bytesRead = 0;
							return AsnDecoder.LengthDecodeStatus.LengthTooBig;
						}
					}
					num <<= 8;
					num |= (uint)b3;
				}
				if (num > 2147483647U)
				{
					bytesRead = 0;
					return AsnDecoder.LengthDecodeStatus.LengthTooBig;
				}
				if (flag && num < 128U)
				{
					bytesRead = 0;
					return AsnDecoder.LengthDecodeStatus.LaxEncodingProhibited;
				}
				length = new int?((int)num);
				return AsnDecoder.LengthDecodeStatus.Success;
			}
		}

		// Token: 0x060051E2 RID: 20962 RVA: 0x00125B88 File Offset: 0x00124B88
		private static Asn1Tag ReadTagAndLength(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, out int? contentsLength, out int bytesRead)
		{
			int num;
			Asn1Tag result = Asn1Tag.Decode(source, out num);
			int num3;
			int? num2 = AsnDecoder.ReadLength(source.Slice(num), ruleSet, out num3);
			int num4 = num + num3;
			if (result.IsConstructed)
			{
				if (ruleSet == AsnEncodingRules.CER && num2 != null)
				{
					throw AsnDecoder.GetValidityException(AsnDecoder.LengthValidity.CerRequiresIndefinite);
				}
			}
			else if (num2 == null)
			{
				throw AsnDecoder.GetValidityException(AsnDecoder.LengthValidity.PrimitiveEncodingRequiresDefinite);
			}
			bytesRead = num4;
			contentsLength = num2;
			return result;
		}

		// Token: 0x060051E3 RID: 20963 RVA: 0x00125BF0 File Offset: 0x00124BF0
		private static void ValidateEndOfContents(Asn1Tag tag, int? length, int headerLength)
		{
			if (tag.IsConstructed || length != 0 || headerLength != 2)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060051E4 RID: 20964 RVA: 0x00125C2C File Offset: 0x00124C2C
		private static AsnDecoder.LengthValidity ValidateLength(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, Asn1Tag localTag, int? encodedLength, out int actualLength, out int bytesConsumed)
		{
			if (localTag.IsConstructed)
			{
				if (ruleSet == AsnEncodingRules.CER && encodedLength != null)
				{
					actualLength = (bytesConsumed = 0);
					return AsnDecoder.LengthValidity.CerRequiresIndefinite;
				}
			}
			else if (encodedLength == null)
			{
				actualLength = (bytesConsumed = 0);
				return AsnDecoder.LengthValidity.PrimitiveEncodingRequiresDefinite;
			}
			if (encodedLength == null)
			{
				actualLength = AsnDecoder.SeekEndOfContents(source, ruleSet);
				bytesConsumed = actualLength + 2;
				return AsnDecoder.LengthValidity.Valid;
			}
			int value = encodedLength.Value;
			int num = value;
			if (num > source.Length)
			{
				actualLength = (bytesConsumed = 0);
				return AsnDecoder.LengthValidity.LengthExceedsInput;
			}
			actualLength = value;
			bytesConsumed = value;
			return AsnDecoder.LengthValidity.Valid;
		}

		// Token: 0x060051E5 RID: 20965 RVA: 0x00125CB8 File Offset: 0x00124CB8
		private static InvalidOperationException GetValidityException(AsnDecoder.LengthValidity validity)
		{
			switch (validity)
			{
			case AsnDecoder.LengthValidity.CerRequiresIndefinite:
				return new InvalidOperationException("A constructed tag used a definite length encoding, which is invalid for CER data. The input may be encoded with BER or DER.");
			case AsnDecoder.LengthValidity.LengthExceedsInput:
				return new InvalidOperationException("The encoded length exceeds the number of bytes remaining in the input buffer.");
			}
			return new InvalidOperationException();
		}

		// Token: 0x060051E6 RID: 20966 RVA: 0x00125CF8 File Offset: 0x00124CF8
		private static int GetPrimitiveIntegerSize(Type primitiveType)
		{
			if (primitiveType == typeof(byte) || primitiveType == typeof(sbyte))
			{
				return 1;
			}
			if (primitiveType == typeof(short) || primitiveType == typeof(ushort))
			{
				return 2;
			}
			if (primitiveType == typeof(int) || primitiveType == typeof(uint))
			{
				return 4;
			}
			if (primitiveType == typeof(long) || primitiveType == typeof(ulong))
			{
				return 8;
			}
			return 0;
		}

		// Token: 0x060051E7 RID: 20967 RVA: 0x00125D78 File Offset: 0x00124D78
		private static int SeekEndOfContents(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet)
		{
			ReadOnlySpan<byte> source2 = source;
			int num = 0;
			int num2 = 1;
			while (!source2.IsEmpty)
			{
				int? length;
				int num3;
				Asn1Tag asn1Tag = AsnDecoder.ReadTagAndLength(source2, ruleSet, out length, out num3);
				if (asn1Tag == Asn1Tag.EndOfContents)
				{
					AsnDecoder.ValidateEndOfContents(asn1Tag, length, num3);
					num2--;
					if (num2 == 0)
					{
						return num;
					}
				}
				if (length == null)
				{
					num2++;
					source2 = source2.Slice(num3);
					num += num3;
				}
				else
				{
					ReadOnlySpan<byte> readOnlySpan = AsnDecoder.Slice(source2, 0, num3 + length.Value);
					source2 = source2.Slice(readOnlySpan.Length);
					num += readOnlySpan.Length;
				}
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060051E8 RID: 20968 RVA: 0x00125E18 File Offset: 0x00124E18
		private static ReadOnlySpan<byte> SliceAtMost(ReadOnlySpan<byte> source, int longestPermitted)
		{
			int length = Math.Min(longestPermitted, source.Length);
			return source.Slice(0, length);
		}

		// Token: 0x060051E9 RID: 20969 RVA: 0x00125E3C File Offset: 0x00124E3C
		private static ReadOnlySpan<byte> Slice(ReadOnlySpan<byte> source, int offset, int length)
		{
			if (length < 0 || source.Length - offset < length)
			{
				throw new InvalidOperationException("The encoded length exceeds the number of bytes remaining in the input buffer.");
			}
			return source.Slice(offset, length);
		}

		// Token: 0x060051EA RID: 20970 RVA: 0x00125E64 File Offset: 0x00124E64
		private static ReadOnlySpan<byte> Slice(ReadOnlySpan<byte> source, int offset, int? length)
		{
			if (length == null)
			{
				return source.Slice(offset);
			}
			int value = length.Value;
			if (value < 0 || source.Length - offset < value)
			{
				throw new InvalidOperationException("The encoded length exceeds the number of bytes remaining in the input buffer.");
			}
			return source.Slice(offset, value);
		}

		// Token: 0x060051EB RID: 20971 RVA: 0x00125EB0 File Offset: 0x00124EB0
		internal static ReadOnlyMemory<byte> Slice(ReadOnlyMemory<byte> bigger, ReadOnlySpan<byte> smaller)
		{
			if (smaller.IsEmpty)
			{
				return default(ReadOnlyMemory<byte>);
			}
			int start;
			if (bigger.Span.Overlaps(smaller, out start))
			{
				return bigger.Slice(start, smaller.Length);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060051EC RID: 20972 RVA: 0x00125EF9 File Offset: 0x00124EF9
		[Conditional("DEBUG")]
		private static void AssertEncodingRules(AsnEncodingRules ruleSet)
		{
		}

		// Token: 0x060051ED RID: 20973 RVA: 0x00125EFB File Offset: 0x00124EFB
		internal static void CheckEncodingRules(AsnEncodingRules ruleSet)
		{
			if (ruleSet != AsnEncodingRules.BER && ruleSet != AsnEncodingRules.CER && ruleSet != AsnEncodingRules.DER)
			{
				throw new ArgumentOutOfRangeException("ruleSet");
			}
		}

		// Token: 0x060051EE RID: 20974 RVA: 0x00125F14 File Offset: 0x00124F14
		private static void CheckExpectedTag(Asn1Tag tag, Asn1Tag expectedTag, UniversalTagNumber tagNumber)
		{
			if (expectedTag.TagClass == TagClass.Universal && expectedTag.TagValue != (int)tagNumber)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Tags with TagClass Universal must have the appropriate TagValue value for the data type being read or written.", new object[]
				{
					expectedTag
				}));
			}
			if (expectedTag.TagClass != tag.TagClass || expectedTag.TagValue != tag.TagValue)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The provided data is tagged with '{0}' class value '{1}', but it should have been '{2}' class value '{3}'.", new object[]
				{
					tag.TagClass,
					tag.TagValue,
					expectedTag.TagClass,
					expectedTag.TagValue
				}));
			}
		}

		// Token: 0x060051EF RID: 20975 RVA: 0x00125FD4 File Offset: 0x00124FD4
		public static ReadOnlySpan<byte> ReadIntegerBytes(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, out int bytesConsumed, Asn1Tag? expectedTag)
		{
			return AsnDecoder.GetIntegerContents(source, ruleSet, expectedTag ?? Asn1Tag.Integer, UniversalTagNumber.Integer, out bytesConsumed);
		}

		// Token: 0x060051F0 RID: 20976 RVA: 0x00126004 File Offset: 0x00125004
		public static bool TryReadInt32(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, out int value, out int bytesConsumed, Asn1Tag? expectedTag)
		{
			long num;
			if (AsnDecoder.TryReadSignedInteger(source, ruleSet, 4, expectedTag ?? Asn1Tag.Integer, UniversalTagNumber.Integer, out num, out bytesConsumed))
			{
				value = (int)num;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x060051F1 RID: 20977 RVA: 0x00126044 File Offset: 0x00125044
		public static bool TryReadUInt32(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, out uint value, out int bytesConsumed, Asn1Tag? expectedTag)
		{
			ulong num;
			if (AsnDecoder.TryReadUnsignedInteger(source, ruleSet, 4, expectedTag ?? Asn1Tag.Integer, UniversalTagNumber.Integer, out num, out bytesConsumed))
			{
				value = (uint)num;
				return true;
			}
			value = 0U;
			return false;
		}

		// Token: 0x060051F2 RID: 20978 RVA: 0x00126084 File Offset: 0x00125084
		public static bool TryReadInt64(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, out long value, out int bytesConsumed, Asn1Tag? expectedTag)
		{
			return AsnDecoder.TryReadSignedInteger(source, ruleSet, 8, expectedTag ?? Asn1Tag.Integer, UniversalTagNumber.Integer, out value, out bytesConsumed);
		}

		// Token: 0x060051F3 RID: 20979 RVA: 0x001260B8 File Offset: 0x001250B8
		public static bool TryReadUInt64(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, out ulong value, out int bytesConsumed, Asn1Tag? expectedTag)
		{
			return AsnDecoder.TryReadUnsignedInteger(source, ruleSet, 8, expectedTag ?? Asn1Tag.Integer, UniversalTagNumber.Integer, out value, out bytesConsumed);
		}

		// Token: 0x060051F4 RID: 20980 RVA: 0x001260EC File Offset: 0x001250EC
		private static ReadOnlySpan<byte> GetIntegerContents(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, Asn1Tag expectedTag, UniversalTagNumber tagNumber, out int bytesConsumed)
		{
			int num;
			ReadOnlySpan<byte> primitiveContentSpan = AsnDecoder.GetPrimitiveContentSpan(source, ruleSet, expectedTag, tagNumber, out num);
			if (primitiveContentSpan.IsEmpty)
			{
				throw new InvalidOperationException();
			}
			ushort num2;
			if (BinaryPrimitives.TryReadUInt16BigEndian(primitiveContentSpan, out num2))
			{
				ushort num3 = num2 & 65408;
				if (num3 == 0 || num3 == 65408)
				{
					throw new InvalidOperationException();
				}
			}
			bytesConsumed = num;
			return primitiveContentSpan;
		}

		// Token: 0x060051F5 RID: 20981 RVA: 0x00126140 File Offset: 0x00125140
		private static bool TryReadSignedInteger(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, int sizeLimit, Asn1Tag expectedTag, UniversalTagNumber tagNumber, out long value, out int bytesConsumed)
		{
			int num;
			ReadOnlySpan<byte> integerContents = AsnDecoder.GetIntegerContents(source, ruleSet, expectedTag, tagNumber, out num);
			if (integerContents.Length > sizeLimit)
			{
				value = 0L;
				bytesConsumed = 0;
				return false;
			}
			long num2 = ((integerContents[0] & 128) != 0) ? -1L : 0L;
			for (int i = 0; i < integerContents.Length; i++)
			{
				num2 <<= 8;
				num2 |= (long)((ulong)integerContents[i]);
			}
			bytesConsumed = num;
			value = num2;
			return true;
		}

		// Token: 0x060051F6 RID: 20982 RVA: 0x001261C0 File Offset: 0x001251C0
		private static bool TryReadUnsignedInteger(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, int sizeLimit, Asn1Tag expectedTag, UniversalTagNumber tagNumber, out ulong value, out int bytesConsumed)
		{
			int num;
			ReadOnlySpan<byte> readOnlySpan = AsnDecoder.GetIntegerContents(source, ruleSet, expectedTag, tagNumber, out num);
			bool flag = (readOnlySpan[0] & 128) != 0;
			if (flag)
			{
				bytesConsumed = 0;
				value = 0UL;
				return false;
			}
			if (readOnlySpan.Length > 1 && readOnlySpan[0] == 0)
			{
				readOnlySpan = readOnlySpan.Slice(1);
			}
			if (readOnlySpan.Length > sizeLimit)
			{
				bytesConsumed = 0;
				value = 0UL;
				return false;
			}
			ulong num2 = 0UL;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				num2 <<= 8;
				num2 |= (ulong)readOnlySpan[i];
			}
			bytesConsumed = num;
			value = num2;
			return true;
		}

		// Token: 0x060051F7 RID: 20983 RVA: 0x00126264 File Offset: 0x00125264
		public static void ReadNull(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, out int bytesConsumed, Asn1Tag? expectedTag)
		{
			int num;
			if (AsnDecoder.GetPrimitiveContentSpan(source, ruleSet, expectedTag ?? Asn1Tag.Null, UniversalTagNumber.Null, out num).Length != 0)
			{
				throw new InvalidOperationException();
			}
			bytesConsumed = num;
		}

		// Token: 0x060051F8 RID: 20984 RVA: 0x001262A8 File Offset: 0x001252A8
		public static bool TryReadOctetString(ReadOnlySpan<byte> source, Span<byte> destination, AsnEncodingRules ruleSet, out int bytesConsumed, out int bytesWritten, Asn1Tag? expectedTag)
		{
			if (source.Overlaps(destination))
			{
				throw new ArgumentException("The destination buffer overlaps the source buffer.", "destination");
			}
			int? length;
			int num;
			ReadOnlySpan<byte> readOnlySpan;
			int num2;
			if (!AsnDecoder.TryReadPrimitiveOctetStringCore(source, ruleSet, expectedTag ?? Asn1Tag.PrimitiveOctetString, UniversalTagNumber.OctetString, out length, out num, out readOnlySpan, out num2))
			{
				int num3;
				bool flag = AsnDecoder.TryCopyConstructedOctetStringContents(AsnDecoder.Slice(source, num, length), ruleSet, destination, length == null, out num3, out bytesWritten);
				if (flag)
				{
					bytesConsumed = num + num3;
				}
				else
				{
					bytesConsumed = 0;
				}
				return flag;
			}
			if (readOnlySpan.Length > destination.Length)
			{
				bytesWritten = 0;
				bytesConsumed = 0;
				return false;
			}
			readOnlySpan.CopyTo(destination);
			bytesWritten = readOnlySpan.Length;
			bytesConsumed = num2;
			return true;
		}

		// Token: 0x060051F9 RID: 20985 RVA: 0x00126364 File Offset: 0x00125364
		public static byte[] ReadOctetString(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, out int bytesConsumed, Asn1Tag? expectedTag)
		{
			byte[] array = null;
			int num;
			ReadOnlySpan<byte> octetStringContents = AsnDecoder.GetOctetStringContents(source, ruleSet, expectedTag ?? Asn1Tag.PrimitiveOctetString, UniversalTagNumber.OctetString, out num, ref array, default(Span<byte>));
			byte[] result = octetStringContents.ToArray();
			if (array != null)
			{
				CryptoPool.Return(array, octetStringContents.Length);
			}
			bytesConsumed = num;
			return result;
		}

		// Token: 0x060051FA RID: 20986 RVA: 0x001263C0 File Offset: 0x001253C0
		private static bool TryReadPrimitiveOctetStringCore(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, Asn1Tag expectedTag, UniversalTagNumber universalTagNumber, out int? contentLength, out int headerLength, out ReadOnlySpan<byte> contents, out int bytesConsumed)
		{
			Asn1Tag tag = AsnDecoder.ReadTagAndLength(source, ruleSet, out contentLength, out headerLength);
			AsnDecoder.CheckExpectedTag(tag, expectedTag, universalTagNumber);
			ReadOnlySpan<byte> readOnlySpan = AsnDecoder.Slice(source, headerLength, contentLength);
			if (tag.IsConstructed)
			{
				if (ruleSet == AsnEncodingRules.DER)
				{
					throw new InvalidOperationException("The encoded value is not valid under the selected encoding, but it may be valid under the BER or CER encoding.");
				}
				contents = default(ReadOnlySpan<byte>);
				bytesConsumed = 0;
				return false;
			}
			else
			{
				if (ruleSet == AsnEncodingRules.CER && readOnlySpan.Length > 1000)
				{
					throw new InvalidOperationException("The encoded value is not valid under the selected encoding, but it may be valid under the BER or DER encoding.");
				}
				contents = readOnlySpan;
				bytesConsumed = headerLength + readOnlySpan.Length;
				return true;
			}
		}

		// Token: 0x060051FB RID: 20987 RVA: 0x0012644C File Offset: 0x0012544C
		public static bool TryReadPrimitiveOctetString(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, out ReadOnlySpan<byte> value, out int bytesConsumed, Asn1Tag? expectedTag)
		{
			int? num;
			int num2;
			return AsnDecoder.TryReadPrimitiveOctetStringCore(source, ruleSet, expectedTag ?? Asn1Tag.PrimitiveOctetString, UniversalTagNumber.OctetString, out num, out num2, out value, out bytesConsumed);
		}

		// Token: 0x060051FC RID: 20988 RVA: 0x00126484 File Offset: 0x00125484
		private static int CountConstructedOctetString(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, bool isIndefinite)
		{
			int num2;
			int num = AsnDecoder.CopyConstructedOctetString(source, ruleSet, Span<byte>.Empty, false, isIndefinite, out num2);
			if (ruleSet == AsnEncodingRules.CER && num <= 1000)
			{
				throw new InvalidOperationException("The encoded value is not valid under the selected encoding, but it may be valid under the BER encoding.");
			}
			return num;
		}

		// Token: 0x060051FD RID: 20989 RVA: 0x001264BA File Offset: 0x001254BA
		private static void CopyConstructedOctetString(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, Span<byte> destination, bool isIndefinite, out int bytesRead, out int bytesWritten)
		{
			bytesWritten = AsnDecoder.CopyConstructedOctetString(source, ruleSet, destination, true, isIndefinite, out bytesRead);
		}

		// Token: 0x060051FE RID: 20990 RVA: 0x001264CC File Offset: 0x001254CC
		private static int CopyConstructedOctetString(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, Span<byte> destination, bool write, bool isIndefinite, out int bytesRead)
		{
			bytesRead = 0;
			int num = 1000;
			ReadOnlySpan<byte> readOnlySpan = source;
			Stack stack = null;
			int num2 = 0;
			Asn1Tag asn1Tag = Asn1Tag.ConstructedBitString;
			Span<byte> destination2 = destination;
			for (;;)
			{
				if (!readOnlySpan.IsEmpty)
				{
					int? length;
					int num3;
					asn1Tag = AsnDecoder.ReadTagAndLength(readOnlySpan, ruleSet, out length, out num3);
					if (asn1Tag == Asn1Tag.PrimitiveOctetString)
					{
						if (ruleSet == AsnEncodingRules.CER && num != 1000)
						{
							break;
						}
						ReadOnlySpan<byte> readOnlySpan2 = AsnDecoder.Slice(readOnlySpan, num3, length.Value);
						int num4 = num3 + readOnlySpan2.Length;
						readOnlySpan = readOnlySpan.Slice(num4);
						bytesRead += num4;
						num2 += readOnlySpan2.Length;
						num = readOnlySpan2.Length;
						if (ruleSet == AsnEncodingRules.CER && num > 1000)
						{
							goto Block_5;
						}
						if (write)
						{
							readOnlySpan2.CopyTo(destination2);
							destination2 = destination2.Slice(readOnlySpan2.Length);
							continue;
						}
						continue;
					}
					else if (asn1Tag == Asn1Tag.EndOfContents && isIndefinite)
					{
						AsnDecoder.ValidateEndOfContents(asn1Tag, length, num3);
						bytesRead += num3;
						if (stack != null && stack.Count > 0)
						{
							AsnDecoder.ParseFrame parseFrame = (AsnDecoder.ParseFrame)stack.Pop();
							readOnlySpan = source.Slice(parseFrame.Offset, parseFrame.Length).Slice(bytesRead);
							bytesRead += parseFrame.BytesRead;
							isIndefinite = parseFrame.Indefinite;
							continue;
						}
					}
					else
					{
						if (!(asn1Tag == Asn1Tag.ConstructedOctetString))
						{
							goto IL_1C2;
						}
						if (ruleSet == AsnEncodingRules.CER)
						{
							goto Block_12;
						}
						if (stack == null)
						{
							stack = new Stack();
						}
						int offset;
						if (!source.Overlaps(readOnlySpan, out offset))
						{
							goto Block_14;
						}
						stack.Push(new AsnDecoder.ParseFrame(offset, readOnlySpan.Length, isIndefinite, bytesRead));
						readOnlySpan = AsnDecoder.Slice(readOnlySpan, num3, length);
						bytesRead = num3;
						isIndefinite = (length == null);
						continue;
					}
				}
				if (isIndefinite && asn1Tag != Asn1Tag.EndOfContents)
				{
					goto Block_16;
				}
				if (stack == null || stack.Count <= 0)
				{
					return num2;
				}
				AsnDecoder.ParseFrame parseFrame2 = (AsnDecoder.ParseFrame)stack.Pop();
				readOnlySpan = source.Slice(parseFrame2.Offset, parseFrame2.Length).Slice(bytesRead);
				isIndefinite = parseFrame2.Indefinite;
				bytesRead += parseFrame2.BytesRead;
			}
			throw new InvalidOperationException("The encoded value is not valid under the selected encoding, but it may be valid under the BER encoding.");
			Block_5:
			throw new InvalidOperationException("The encoded value is not valid under the selected encoding, but it may be valid under the BER encoding.");
			Block_12:
			throw new InvalidOperationException("The encoded value is not valid under the selected encoding, but it may be valid under the BER encoding.");
			Block_14:
			throw new InvalidOperationException();
			IL_1C2:
			throw new InvalidOperationException();
			Block_16:
			throw new InvalidOperationException();
		}

		// Token: 0x060051FF RID: 20991 RVA: 0x0012671C File Offset: 0x0012571C
		private static bool TryCopyConstructedOctetStringContents(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, Span<byte> dest, bool isIndefinite, out int bytesRead, out int bytesWritten)
		{
			bytesRead = 0;
			int num = AsnDecoder.CountConstructedOctetString(source, ruleSet, isIndefinite);
			if (dest.Length < num)
			{
				bytesWritten = 0;
				return false;
			}
			AsnDecoder.CopyConstructedOctetString(source, ruleSet, dest, isIndefinite, out bytesRead, out bytesWritten);
			return true;
		}

		// Token: 0x06005200 RID: 20992 RVA: 0x00126754 File Offset: 0x00125754
		private static ReadOnlySpan<byte> GetOctetStringContents(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, Asn1Tag expectedTag, UniversalTagNumber universalTagNumber, out int bytesConsumed, ref byte[] rented, Span<byte> tmpSpace)
		{
			int? num;
			int num2;
			ReadOnlySpan<byte> readOnlySpan;
			if (AsnDecoder.TryReadPrimitiveOctetStringCore(source, ruleSet, expectedTag, universalTagNumber, out num, out num2, out readOnlySpan, out bytesConsumed))
			{
				return readOnlySpan;
			}
			readOnlySpan = source.Slice(num2);
			int num3 = num ?? AsnDecoder.SeekEndOfContents(readOnlySpan, ruleSet);
			if (tmpSpace.Length > 0 && num3 > tmpSpace.Length)
			{
				bool isIndefinite = num == null;
				num3 = AsnDecoder.CountConstructedOctetString(readOnlySpan, ruleSet, isIndefinite);
			}
			if (num3 > tmpSpace.Length)
			{
				rented = CryptoPool.Rent(num3);
				tmpSpace = rented;
			}
			int num4;
			int length;
			if (AsnDecoder.TryCopyConstructedOctetStringContents(AsnDecoder.Slice(source, num2, num), ruleSet, tmpSpace, num == null, out num4, out length))
			{
				bytesConsumed = num2 + num4;
				return tmpSpace.Slice(0, length);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06005201 RID: 20993 RVA: 0x00126820 File Offset: 0x00125820
		public static byte[] ReadObjectIdentifier(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, out int bytesConsumed, Asn1Tag? expectedTag)
		{
			int num;
			ReadOnlySpan<byte> primitiveContentSpan = AsnDecoder.GetPrimitiveContentSpan(source, ruleSet, expectedTag ?? Asn1Tag.ObjectIdentifier, UniversalTagNumber.ObjectIdentifier, out num);
			bytesConsumed = num;
			return primitiveContentSpan.ToArray();
		}

		// Token: 0x06005202 RID: 20994 RVA: 0x0012685C File Offset: 0x0012585C
		public static void ReadSequence(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, out int contentOffset, out int contentLength, out int bytesConsumed, Asn1Tag? expectedTag)
		{
			int? num;
			int num2;
			Asn1Tag tag = AsnDecoder.ReadTagAndLength(source, ruleSet, out num, out num2);
			AsnDecoder.CheckExpectedTag(tag, expectedTag ?? Asn1Tag.Sequence, UniversalTagNumber.Sequence);
			if (!tag.IsConstructed)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The encoded value uses a primitive encoding, which is invalid for '{0}' values.", new object[]
				{
					UniversalTagNumber.Sequence
				}));
			}
			if (num == null)
			{
				int num3 = AsnDecoder.SeekEndOfContents(source.Slice(num2), ruleSet);
				contentLength = num3;
				contentOffset = num2;
				bytesConsumed = num3 + num2 + 2;
				return;
			}
			if (num.Value + num2 > source.Length)
			{
				throw AsnDecoder.GetValidityException(AsnDecoder.LengthValidity.LengthExceedsInput);
			}
			contentLength = num.Value;
			contentOffset = num2;
			bytesConsumed = contentLength + num2;
		}

		// Token: 0x06005203 RID: 20995 RVA: 0x00126920 File Offset: 0x00125920
		public static void ReadSetOf(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, out int contentOffset, out int contentLength, out int bytesConsumed, bool skipSortOrderValidation, Asn1Tag? expectedTag)
		{
			int? num;
			int num2;
			Asn1Tag tag = AsnDecoder.ReadTagAndLength(source, ruleSet, out num, out num2);
			AsnDecoder.CheckExpectedTag(tag, expectedTag ?? Asn1Tag.SetOf, UniversalTagNumber.Set);
			if (!tag.IsConstructed)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The encoded value uses a primitive encoding, which is invalid for '{0}' values.", new object[]
				{
					UniversalTagNumber.Set
				}));
			}
			int num3;
			ReadOnlySpan<byte> readOnlySpan;
			if (num != null)
			{
				num3 = 0;
				readOnlySpan = AsnDecoder.Slice(source, num2, num.Value);
			}
			else
			{
				int length = AsnDecoder.SeekEndOfContents(source.Slice(num2), ruleSet);
				readOnlySpan = AsnDecoder.Slice(source, num2, length);
				num3 = 2;
			}
			if (!skipSortOrderValidation && (ruleSet == AsnEncodingRules.DER || ruleSet == AsnEncodingRules.CER))
			{
				ReadOnlySpan<byte> source2 = readOnlySpan;
				ReadOnlySpan<byte> y = default(ReadOnlySpan<byte>);
				while (!source2.IsEmpty)
				{
					int num4;
					int num5;
					int num6;
					AsnDecoder.ReadEncodedValue(source2, ruleSet, out num4, out num5, out num6);
					ReadOnlySpan<byte> readOnlySpan2 = source2.Slice(0, num6);
					source2 = source2.Slice(num6);
					if (SetOfValueComparer.Compare(readOnlySpan2, y) < 0)
					{
						throw new InvalidOperationException("The encoded set is not sorted as required by the current encoding rules. The value may be valid under the BER encoding, or you can ignore the sort validation by specifying skipSortValidation=true.");
					}
					y = readOnlySpan2;
				}
			}
			contentOffset = num2;
			contentLength = readOnlySpan.Length;
			bytesConsumed = num2 + readOnlySpan.Length + num3;
		}

		// Token: 0x04002A38 RID: 10808
		internal const int MaxCERSegmentSize = 1000;

		// Token: 0x04002A39 RID: 10809
		internal const int EndOfContentsEncodedLength = 2;

		// Token: 0x020008C5 RID: 2245
		// (Invoke) Token: 0x06005205 RID: 20997
		private delegate void BitStringCopyAction(ReadOnlySpan<byte> value, byte normalizedLastByte, Span<byte> destination);

		// Token: 0x020008C6 RID: 2246
		private struct ParseFrame
		{
			// Token: 0x06005208 RID: 21000 RVA: 0x00126A47 File Offset: 0x00125A47
			public ParseFrame(int offset, int length, bool indefinite, int bytesRead)
			{
				this._offset = offset;
				this._length = length;
				this._indefinite = indefinite;
				this._bytesRead = bytesRead;
			}

			// Token: 0x17000E2E RID: 3630
			// (get) Token: 0x06005209 RID: 21001 RVA: 0x00126A66 File Offset: 0x00125A66
			public int Offset
			{
				get
				{
					return this._offset;
				}
			}

			// Token: 0x17000E2F RID: 3631
			// (get) Token: 0x0600520A RID: 21002 RVA: 0x00126A6E File Offset: 0x00125A6E
			public int Length
			{
				get
				{
					return this._length;
				}
			}

			// Token: 0x17000E30 RID: 3632
			// (get) Token: 0x0600520B RID: 21003 RVA: 0x00126A76 File Offset: 0x00125A76
			public bool Indefinite
			{
				get
				{
					return this._indefinite;
				}
			}

			// Token: 0x17000E31 RID: 3633
			// (get) Token: 0x0600520C RID: 21004 RVA: 0x00126A7E File Offset: 0x00125A7E
			public int BytesRead
			{
				get
				{
					return this._bytesRead;
				}
			}

			// Token: 0x04002A3A RID: 10810
			private int _offset;

			// Token: 0x04002A3B RID: 10811
			private int _length;

			// Token: 0x04002A3C RID: 10812
			private bool _indefinite;

			// Token: 0x04002A3D RID: 10813
			private int _bytesRead;
		}

		// Token: 0x020008C7 RID: 2247
		private enum LengthDecodeStatus
		{
			// Token: 0x04002A3F RID: 10815
			NeedMoreData,
			// Token: 0x04002A40 RID: 10816
			DerIndefinite,
			// Token: 0x04002A41 RID: 10817
			ReservedValue,
			// Token: 0x04002A42 RID: 10818
			LengthTooBig,
			// Token: 0x04002A43 RID: 10819
			LaxEncodingProhibited,
			// Token: 0x04002A44 RID: 10820
			Success
		}

		// Token: 0x020008C8 RID: 2248
		private enum LengthValidity
		{
			// Token: 0x04002A46 RID: 10822
			CerRequiresIndefinite,
			// Token: 0x04002A47 RID: 10823
			PrimitiveEncodingRequiresDefinite,
			// Token: 0x04002A48 RID: 10824
			LengthExceedsInput,
			// Token: 0x04002A49 RID: 10825
			Valid
		}
	}
}
