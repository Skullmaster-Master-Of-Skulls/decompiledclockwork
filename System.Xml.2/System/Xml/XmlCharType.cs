using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace System.Xml
{
	// Token: 0x02000079 RID: 121
	internal struct XmlCharType
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000FC18 File Offset: 0x0000DE18
		private static object StaticLock
		{
			get
			{
				if (XmlCharType.s_Lock == null)
				{
					object value = new object();
					Interlocked.CompareExchange<object>(ref XmlCharType.s_Lock, value, null);
				}
				return XmlCharType.s_Lock;
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000FC44 File Offset: 0x0000DE44
		private unsafe static void InitInstance()
		{
			object staticLock = XmlCharType.StaticLock;
			lock (staticLock)
			{
				if (XmlCharType.s_CharProperties == null)
				{
					UnmanagedMemoryStream unmanagedMemoryStream = (UnmanagedMemoryStream)Assembly.GetExecutingAssembly().GetManifestResourceStream("XmlCharType.bin");
					byte* positionPointer = unmanagedMemoryStream.PositionPointer;
					Thread.MemoryBarrier();
					XmlCharType.s_CharProperties = positionPointer;
				}
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000FCB4 File Offset: 0x0000DEB4
		private unsafe XmlCharType(byte* charProperties)
		{
			this.charProperties = charProperties;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000FCBD File Offset: 0x0000DEBD
		public static XmlCharType Instance
		{
			get
			{
				if (XmlCharType.s_CharProperties == null)
				{
					XmlCharType.InitInstance();
				}
				return new XmlCharType(XmlCharType.s_CharProperties);
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000FCDB File Offset: 0x0000DEDB
		public unsafe bool IsWhiteSpace(char ch)
		{
			return (this.charProperties[ch] & 1) > 0;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000FCEB File Offset: 0x0000DEEB
		public bool IsExtender(char ch)
		{
			return ch == '·';
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000FCF5 File Offset: 0x0000DEF5
		public unsafe bool IsNCNameSingleChar(char ch)
		{
			return (this.charProperties[ch] & 8) > 0;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000FD05 File Offset: 0x0000DF05
		public unsafe bool IsStartNCNameSingleChar(char ch)
		{
			return (this.charProperties[ch] & 4) > 0;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000FD15 File Offset: 0x0000DF15
		public bool IsNameSingleChar(char ch)
		{
			return this.IsNCNameSingleChar(ch) || ch == ':';
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000FD27 File Offset: 0x0000DF27
		public bool IsStartNameSingleChar(char ch)
		{
			return this.IsStartNCNameSingleChar(ch) || ch == ':';
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000FD39 File Offset: 0x0000DF39
		public unsafe bool IsCharData(char ch)
		{
			return (this.charProperties[ch] & 16) > 0;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000FD4A File Offset: 0x0000DF4A
		public bool IsPubidChar(char ch)
		{
			return ch < '\u0080' && ((int)"␀\0ﾻ꿿￿蟿￾߿"[(int)(ch >> 4)] & 1 << (int)(ch & '\u000f')) != 0;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000FD70 File Offset: 0x0000DF70
		internal unsafe bool IsTextChar(char ch)
		{
			return (this.charProperties[ch] & 64) > 0;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000FD81 File Offset: 0x0000DF81
		internal unsafe bool IsAttributeValueChar(char ch)
		{
			return (this.charProperties[ch] & 128) > 0;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000FD95 File Offset: 0x0000DF95
		public unsafe bool IsLetter(char ch)
		{
			return (this.charProperties[ch] & 2) > 0;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000FDA5 File Offset: 0x0000DFA5
		public unsafe bool IsNCNameCharXml4e(char ch)
		{
			return (this.charProperties[ch] & 32) > 0;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000FDB6 File Offset: 0x0000DFB6
		public bool IsStartNCNameCharXml4e(char ch)
		{
			return this.IsLetter(ch) || ch == '_';
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000FDC8 File Offset: 0x0000DFC8
		public bool IsNameCharXml4e(char ch)
		{
			return this.IsNCNameCharXml4e(ch) || ch == ':';
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000FDDA File Offset: 0x0000DFDA
		public bool IsStartNameCharXml4e(char ch)
		{
			return this.IsStartNCNameCharXml4e(ch) || ch == ':';
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000FDEC File Offset: 0x0000DFEC
		public static bool IsDigit(char ch)
		{
			return XmlCharType.InRange((int)ch, 48, 57);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000FDF8 File Offset: 0x0000DFF8
		public static bool IsHexDigit(char ch)
		{
			return XmlCharType.InRange((int)ch, 48, 57) || XmlCharType.InRange((int)ch, 97, 102) || XmlCharType.InRange((int)ch, 65, 70);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000FE1E File Offset: 0x0000E01E
		internal static bool IsHighSurrogate(int ch)
		{
			return XmlCharType.InRange(ch, 55296, 56319);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000FE30 File Offset: 0x0000E030
		internal static bool IsLowSurrogate(int ch)
		{
			return XmlCharType.InRange(ch, 56320, 57343);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000FE42 File Offset: 0x0000E042
		internal static bool IsSurrogate(int ch)
		{
			return XmlCharType.InRange(ch, 55296, 57343);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000FE54 File Offset: 0x0000E054
		internal static int CombineSurrogateChar(int lowChar, int highChar)
		{
			return lowChar - 56320 | (highChar - 55296 << 10) + 65536;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000FE70 File Offset: 0x0000E070
		internal static void SplitSurrogateChar(int combinedChar, out char lowChar, out char highChar)
		{
			int num = combinedChar - 65536;
			lowChar = (char)(56320 + num % 1024);
			highChar = (char)(55296 + num / 1024);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000FEA5 File Offset: 0x0000E0A5
		internal bool IsOnlyWhitespace(string str)
		{
			return this.IsOnlyWhitespaceWithPos(str) == -1;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000FEB4 File Offset: 0x0000E0B4
		internal unsafe int IsOnlyWhitespaceWithPos(string str)
		{
			if (str != null)
			{
				for (int i = 0; i < str.Length; i++)
				{
					if ((this.charProperties[str[i]] & 1) == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000FEEC File Offset: 0x0000E0EC
		internal unsafe int IsOnlyCharData(string str)
		{
			if (str != null)
			{
				for (int i = 0; i < str.Length; i++)
				{
					if ((this.charProperties[str[i]] & 16) == 0)
					{
						if (i + 1 >= str.Length || !XmlCharType.IsHighSurrogate((int)str[i]) || !XmlCharType.IsLowSurrogate((int)str[i + 1]))
						{
							return i;
						}
						i++;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000FF54 File Offset: 0x0000E154
		internal static bool IsOnlyDigits(string str, int startPos, int len)
		{
			for (int i = startPos; i < startPos + len; i++)
			{
				if (!XmlCharType.IsDigit(str[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000FF80 File Offset: 0x0000E180
		internal static bool IsOnlyDigits(char[] chars, int startPos, int len)
		{
			for (int i = startPos; i < startPos + len; i++)
			{
				if (!XmlCharType.IsDigit(chars[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000FFA8 File Offset: 0x0000E1A8
		internal int IsPublicId(string str)
		{
			if (str != null)
			{
				for (int i = 0; i < str.Length; i++)
				{
					if (!this.IsPubidChar(str[i]))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000FFDB File Offset: 0x0000E1DB
		private static bool InRange(int value, int start, int end)
		{
			return value - start <= end - start;
		}

		// Token: 0x040001CC RID: 460
		internal const int SurHighStart = 55296;

		// Token: 0x040001CD RID: 461
		internal const int SurHighEnd = 56319;

		// Token: 0x040001CE RID: 462
		internal const int SurLowStart = 56320;

		// Token: 0x040001CF RID: 463
		internal const int SurLowEnd = 57343;

		// Token: 0x040001D0 RID: 464
		internal const int SurMask = 64512;

		// Token: 0x040001D1 RID: 465
		internal const int fWhitespace = 1;

		// Token: 0x040001D2 RID: 466
		internal const int fLetter = 2;

		// Token: 0x040001D3 RID: 467
		internal const int fNCStartNameSC = 4;

		// Token: 0x040001D4 RID: 468
		internal const int fNCNameSC = 8;

		// Token: 0x040001D5 RID: 469
		internal const int fCharData = 16;

		// Token: 0x040001D6 RID: 470
		internal const int fNCNameXml4e = 32;

		// Token: 0x040001D7 RID: 471
		internal const int fText = 64;

		// Token: 0x040001D8 RID: 472
		internal const int fAttrValue = 128;

		// Token: 0x040001D9 RID: 473
		private const string s_PublicIdBitmap = "␀\0ﾻ꿿￿蟿￾߿";

		// Token: 0x040001DA RID: 474
		private const uint CharPropertiesSize = 65536U;

		// Token: 0x040001DB RID: 475
		private static object s_Lock;

		// Token: 0x040001DC RID: 476
		private unsafe static volatile byte* s_CharProperties;

		// Token: 0x040001DD RID: 477
		internal unsafe byte* charProperties;
	}
}
