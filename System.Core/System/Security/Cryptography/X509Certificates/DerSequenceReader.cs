using System;
using System.Globalization;
using System.Numerics;
using System.Text;
using System.Threading;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000123 RID: 291
	internal class DerSequenceReader
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x00021ED3 File Offset: 0x000200D3
		// (set) Token: 0x0600097F RID: 2431 RVA: 0x00021EDB File Offset: 0x000200DB
		internal int ContentLength { get; private set; }

		// Token: 0x06000980 RID: 2432 RVA: 0x00021EE4 File Offset: 0x000200E4
		private DerSequenceReader(bool startAtPayload, byte[] data, int offset, int length)
		{
			this._data = data;
			this._position = offset;
			this._end = offset + length;
			this.ContentLength = length;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00021F0C File Offset: 0x0002010C
		internal DerSequenceReader(byte[] data) : this(data, 0, data.Length)
		{
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00021F19 File Offset: 0x00020119
		internal DerSequenceReader(byte[] data, int offset, int length) : this(DerSequenceReader.DerTag.Sequence, data, offset, length)
		{
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00021F28 File Offset: 0x00020128
		private DerSequenceReader(DerSequenceReader.DerTag tagToEat, byte[] data, int offset, int length)
		{
			if (offset < 0 || length < 2 || length > data.Length - offset)
			{
				throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
			}
			this._data = data;
			this._end = offset + length;
			this._position = offset;
			this.EatTag(tagToEat);
			int num = this.EatLength();
			this.ContentLength = num;
			this._end = this._position + num;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00021F98 File Offset: 0x00020198
		internal static DerSequenceReader CreateForPayload(byte[] payload)
		{
			return new DerSequenceReader(true, payload, 0, payload.Length);
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00021FA5 File Offset: 0x000201A5
		internal bool HasData
		{
			get
			{
				return this._position < this._end;
			}
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00021FB8 File Offset: 0x000201B8
		internal byte PeekTag()
		{
			if (!this.HasData)
			{
				throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
			}
			byte b = this._data[this._position];
			if ((b & 31) == 31)
			{
				throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
			}
			return b;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00022004 File Offset: 0x00020204
		internal bool HasTag(DerSequenceReader.DerTag expectedTag)
		{
			return this.HasTag((byte)expectedTag);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0002200D File Offset: 0x0002020D
		internal bool HasTag(byte expectedTag)
		{
			return this.HasData && this._data[this._position] == expectedTag;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0002202C File Offset: 0x0002022C
		internal void SkipValue()
		{
			this.EatTag((DerSequenceReader.DerTag)this.PeekTag());
			int num = this.EatLength();
			this._position += num;
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0002205C File Offset: 0x0002025C
		internal void ValidateAndSkipDerValue()
		{
			byte b = this.PeekTag();
			if ((b & 192) == 0)
			{
				if (b == 0 || b == 15)
				{
					throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
				}
				bool flag = false;
				int num = (int)(b & 31);
				if (num <= 11)
				{
					if (num != 8 && num != 11)
					{
						goto IL_53;
					}
				}
				else if (num - 16 > 1 && num != 29)
				{
					goto IL_53;
				}
				flag = true;
				IL_53:
				bool flag2 = (b & 32) == 32;
				if (flag != flag2)
				{
					throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
				}
			}
			this.EatTag((DerSequenceReader.DerTag)b);
			int num2 = this.EatLength();
			if (num2 > 0 && (b & 32) == 32)
			{
				DerSequenceReader derSequenceReader = new DerSequenceReader(true, this._data, this._position, this._end - this._position);
				while (derSequenceReader.HasData)
				{
					derSequenceReader.ValidateAndSkipDerValue();
				}
			}
			this._position += num2;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00022134 File Offset: 0x00020334
		internal byte[] ReadNextEncodedValue()
		{
			this.PeekTag();
			int num2;
			int num = DerSequenceReader.ScanContentLength(this._data, this._position + 1, this._end, out num2);
			int num3 = 1 + num2 + num;
			byte[] array = new byte[num3];
			Buffer.BlockCopy(this._data, this._position, array, 0, num3);
			this._position += num3;
			return array;
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00022194 File Offset: 0x00020394
		internal bool ReadBoolean()
		{
			this.EatTag(DerSequenceReader.DerTag.Boolean);
			int num = this.EatLength();
			if (num != 1)
			{
				throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
			}
			bool result = this._data[this._position] > 0;
			this._position += num;
			return result;
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x000221E4 File Offset: 0x000203E4
		internal int ReadInteger()
		{
			byte[] array = this.ReadIntegerBytes();
			Array.Reverse(array);
			BigInteger value = new BigInteger(array);
			return (int)value;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0002220C File Offset: 0x0002040C
		internal byte[] ReadIntegerBytes()
		{
			this.EatTag(DerSequenceReader.DerTag.Integer);
			return this.ReadContentAsBytes();
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0002221C File Offset: 0x0002041C
		internal byte[] ReadBitString()
		{
			this.EatTag(DerSequenceReader.DerTag.BitString);
			int num = this.EatLength();
			if (num < 1)
			{
				throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
			}
			byte b = this._data[this._position];
			if (b > 7)
			{
				throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
			}
			num--;
			this._position++;
			byte[] array = new byte[num];
			Buffer.BlockCopy(this._data, this._position, array, 0, num);
			this._position += num;
			return array;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x000222A9 File Offset: 0x000204A9
		internal byte[] ReadOctetString()
		{
			this.EatTag(DerSequenceReader.DerTag.OctetString);
			return this.ReadContentAsBytes();
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x000222B8 File Offset: 0x000204B8
		internal string ReadOidAsString()
		{
			this.EatTag(DerSequenceReader.DerTag.ObjectIdentifier);
			int num = this.EatLength();
			if (num < 1)
			{
				throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
			}
			StringBuilder stringBuilder = new StringBuilder(num * 4);
			byte b = this._data[this._position];
			byte value = b / 40;
			byte value2 = b % 40;
			stringBuilder.Append(value);
			stringBuilder.Append('.');
			stringBuilder.Append(value2);
			bool flag = true;
			BigInteger bigInteger = new BigInteger(0);
			for (int i = 1; i < num; i++)
			{
				byte b2 = this._data[this._position + i];
				byte b3 = b2 & 127;
				if (flag)
				{
					stringBuilder.Append('.');
					flag = false;
				}
				bigInteger <<= 7;
				bigInteger += b3;
				if (b2 == b3)
				{
					stringBuilder.Append(bigInteger);
					bigInteger = 0;
					flag = true;
				}
			}
			this._position += num;
			return stringBuilder.ToString();
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x000223B7 File Offset: 0x000205B7
		internal Oid ReadOid()
		{
			return new Oid(this.ReadOidAsString());
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x000223C4 File Offset: 0x000205C4
		internal string ReadUtf8String()
		{
			this.EatTag(DerSequenceReader.DerTag.UTF8String);
			int num = this.EatLength();
			string @string = Encoding.UTF8.GetString(this._data, this._position, num);
			this._position += num;
			return DerSequenceReader.TrimTrailingNulls(@string);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0002240C File Offset: 0x0002060C
		private DerSequenceReader ReadCollectionWithTag(DerSequenceReader.DerTag expected)
		{
			DerSequenceReader.CheckTag(expected, this._data, this._position);
			int num2;
			int num = DerSequenceReader.ScanContentLength(this._data, this._position + 1, this._end, out num2);
			int num3 = 1 + num2 + num;
			DerSequenceReader result = new DerSequenceReader(expected, this._data, this._position, num3);
			this._position += num3;
			return result;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00022470 File Offset: 0x00020670
		internal DerSequenceReader ReadSequence()
		{
			return this.ReadCollectionWithTag(DerSequenceReader.DerTag.Sequence);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0002247A File Offset: 0x0002067A
		internal DerSequenceReader ReadSet()
		{
			return this.ReadCollectionWithTag(DerSequenceReader.DerTag.Set);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00022484 File Offset: 0x00020684
		internal string ReadPrintableString()
		{
			this.EatTag(DerSequenceReader.DerTag.PrintableString);
			int num = this.EatLength();
			string @string = Encoding.ASCII.GetString(this._data, this._position, num);
			this._position += num;
			return DerSequenceReader.TrimTrailingNulls(@string);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x000224CC File Offset: 0x000206CC
		internal string ReadIA5String()
		{
			this.EatTag(DerSequenceReader.DerTag.IA5String);
			int num = this.EatLength();
			string @string = Encoding.ASCII.GetString(this._data, this._position, num);
			this._position += num;
			return DerSequenceReader.TrimTrailingNulls(@string);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00022514 File Offset: 0x00020714
		internal DateTime ReadX509Date()
		{
			byte b = this.PeekTag();
			DerSequenceReader.DerTag derTag = (DerSequenceReader.DerTag)b;
			if (derTag == DerSequenceReader.DerTag.UTCTime)
			{
				return this.ReadUtcTime();
			}
			if (derTag != DerSequenceReader.DerTag.GeneralizedTime)
			{
				throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
			}
			return this.ReadGeneralizedTime();
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00022553 File Offset: 0x00020753
		internal DateTime ReadUtcTime()
		{
			return this.ReadTime(DerSequenceReader.DerTag.UTCTime, "yyMMddHHmmss'Z'");
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00022562 File Offset: 0x00020762
		internal DateTime ReadGeneralizedTime()
		{
			return this.ReadTime(DerSequenceReader.DerTag.GeneralizedTime, "yyyyMMddHHmmss'Z'");
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00022574 File Offset: 0x00020774
		internal string ReadBMPString()
		{
			this.EatTag(DerSequenceReader.DerTag.BMPString);
			int num = this.EatLength();
			string @string = Encoding.BigEndianUnicode.GetString(this._data, this._position, num);
			this._position += num;
			return DerSequenceReader.TrimTrailingNulls(@string);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x000225BC File Offset: 0x000207BC
		private static string TrimTrailingNulls(string value)
		{
			if (value != null && value.Length > 0)
			{
				int num = value.Length;
				while (num > 0 && value[num - 1] == '\0')
				{
					num--;
				}
				if (num != value.Length)
				{
					return value.Substring(0, num);
				}
			}
			return value;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00022604 File Offset: 0x00020804
		private DateTime ReadTime(DerSequenceReader.DerTag timeTag, string formatString)
		{
			this.EatTag(timeTag);
			int num = this.EatLength();
			string @string = Encoding.ASCII.GetString(this._data, this._position, num);
			this._position += num;
			DateTimeFormatInfo provider = LazyInitializer.EnsureInitialized<DateTimeFormatInfo>(ref DerSequenceReader.s_validityDateTimeFormatInfo, delegate()
			{
				DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)CultureInfo.InvariantCulture.DateTimeFormat.Clone();
				dateTimeFormatInfo.Calendar.TwoDigitYearMax = 2049;
				return dateTimeFormatInfo;
			});
			DateTime result;
			if (!DateTime.TryParseExact(@string, formatString, provider, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out result))
			{
				throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
			}
			return result;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00022690 File Offset: 0x00020890
		private byte[] ReadContentAsBytes()
		{
			int num = this.EatLength();
			byte[] array = new byte[num];
			Buffer.BlockCopy(this._data, this._position, array, 0, num);
			this._position += num;
			return array;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x000226CE File Offset: 0x000208CE
		private void EatTag(DerSequenceReader.DerTag expected)
		{
			if (!this.HasData)
			{
				throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
			}
			DerSequenceReader.CheckTag(expected, this._data, this._position);
			this._position++;
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00022708 File Offset: 0x00020908
		private static void CheckTag(DerSequenceReader.DerTag expected, byte[] data, int position)
		{
			if (position >= data.Length)
			{
				throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
			}
			byte b = data[position];
			byte b2 = b & 31;
			if (b2 == 31)
			{
				throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
			}
			if ((b & 128) != 0)
			{
				return;
			}
			byte b3 = (byte)(expected & (DerSequenceReader.DerTag)31);
			if (b3 != b2)
			{
				throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00022770 File Offset: 0x00020970
		private int EatLength()
		{
			int num;
			int result = DerSequenceReader.ScanContentLength(this._data, this._position, this._end, out num);
			this._position += num;
			return result;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x000227A8 File Offset: 0x000209A8
		private static int ScanContentLength(byte[] data, int offset, int end, out int bytesConsumed)
		{
			if (offset >= end)
			{
				throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
			}
			byte b = data[offset];
			if (b < 128)
			{
				bytesConsumed = 1;
				if ((int)b > end - offset - bytesConsumed)
				{
					throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
				}
				return (int)b;
			}
			else
			{
				int num = (int)(b & 127);
				if (num > 4)
				{
					throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
				}
				bytesConsumed = 1 + num;
				if (bytesConsumed > end - offset)
				{
					throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
				}
				if (bytesConsumed == 1)
				{
					throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
				}
				int num2 = offset + bytesConsumed;
				int num3 = 0;
				for (int i = offset + 1; i < num2; i++)
				{
					num3 <<= 8;
					num3 |= (int)data[i];
				}
				if (num3 < 0)
				{
					throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
				}
				if (num3 > end - offset - bytesConsumed)
				{
					throw new CryptographicException(SR.GetString("Cryptography_Der_Invalid_Encoding"));
				}
				return num3;
			}
		}

		// Token: 0x04000701 RID: 1793
		internal const byte ContextSpecificTagFlag = 128;

		// Token: 0x04000702 RID: 1794
		internal const byte ConstructedFlag = 32;

		// Token: 0x04000703 RID: 1795
		internal const byte ContextSpecificConstructedTag0 = 160;

		// Token: 0x04000704 RID: 1796
		internal const byte ContextSpecificConstructedTag1 = 161;

		// Token: 0x04000705 RID: 1797
		internal const byte ContextSpecificConstructedTag2 = 162;

		// Token: 0x04000706 RID: 1798
		internal const byte ContextSpecificConstructedTag3 = 163;

		// Token: 0x04000707 RID: 1799
		internal const byte ConstructedSequence = 48;

		// Token: 0x04000708 RID: 1800
		internal const byte TagClassMask = 192;

		// Token: 0x04000709 RID: 1801
		internal const byte TagNumberMask = 31;

		// Token: 0x0400070A RID: 1802
		internal static DateTimeFormatInfo s_validityDateTimeFormatInfo;

		// Token: 0x0400070B RID: 1803
		private readonly byte[] _data;

		// Token: 0x0400070C RID: 1804
		private readonly int _end;

		// Token: 0x0400070D RID: 1805
		private int _position;

		// Token: 0x02000356 RID: 854
		internal enum DerTag : byte
		{
			// Token: 0x04000F2C RID: 3884
			Boolean = 1,
			// Token: 0x04000F2D RID: 3885
			Integer,
			// Token: 0x04000F2E RID: 3886
			BitString,
			// Token: 0x04000F2F RID: 3887
			OctetString,
			// Token: 0x04000F30 RID: 3888
			Null,
			// Token: 0x04000F31 RID: 3889
			ObjectIdentifier,
			// Token: 0x04000F32 RID: 3890
			UTF8String = 12,
			// Token: 0x04000F33 RID: 3891
			Sequence = 16,
			// Token: 0x04000F34 RID: 3892
			Set,
			// Token: 0x04000F35 RID: 3893
			PrintableString = 19,
			// Token: 0x04000F36 RID: 3894
			T61String,
			// Token: 0x04000F37 RID: 3895
			IA5String = 22,
			// Token: 0x04000F38 RID: 3896
			UTCTime,
			// Token: 0x04000F39 RID: 3897
			GeneralizedTime,
			// Token: 0x04000F3A RID: 3898
			BMPString = 30
		}
	}
}
