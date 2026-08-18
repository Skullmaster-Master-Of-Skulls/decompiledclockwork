using System;
using System.Text;
using System.Threading;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000016 RID: 22
	public sealed class ZipConstants
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00005C20 File Offset: 0x00004C20
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00005C27 File Offset: 0x00004C27
		public static int DefaultCodePage
		{
			get
			{
				return ZipConstants.defaultCodePage;
			}
			set
			{
				if (value < 0 || value > 65535 || value == 1 || value == 2 || value == 3 || value == 42)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				ZipConstants.defaultCodePage = value;
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005C57 File Offset: 0x00004C57
		public static string ConvertToString(byte[] data, int count)
		{
			if (data == null)
			{
				return string.Empty;
			}
			return Encoding.GetEncoding(ZipConstants.DefaultCodePage).GetString(data, 0, count);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005C74 File Offset: 0x00004C74
		public static string ConvertToString(byte[] data)
		{
			if (data == null)
			{
				return string.Empty;
			}
			return ZipConstants.ConvertToString(data, data.Length);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005C88 File Offset: 0x00004C88
		public static string ConvertToStringExt(int flags, byte[] data, int count)
		{
			if (data == null)
			{
				return string.Empty;
			}
			if ((flags & 2048) != 0)
			{
				return Encoding.UTF8.GetString(data, 0, count);
			}
			return ZipConstants.ConvertToString(data, count);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005CB1 File Offset: 0x00004CB1
		public static string ConvertToStringExt(int flags, byte[] data)
		{
			if (data == null)
			{
				return string.Empty;
			}
			if ((flags & 2048) != 0)
			{
				return Encoding.UTF8.GetString(data, 0, data.Length);
			}
			return ZipConstants.ConvertToString(data, data.Length);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005CDE File Offset: 0x00004CDE
		public static byte[] ConvertToArray(string str)
		{
			if (str == null)
			{
				return new byte[0];
			}
			return Encoding.GetEncoding(ZipConstants.DefaultCodePage).GetBytes(str);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005CFA File Offset: 0x00004CFA
		public static byte[] ConvertToArray(int flags, string str)
		{
			if (str == null)
			{
				return new byte[0];
			}
			if ((flags & 2048) != 0)
			{
				return Encoding.UTF8.GetBytes(str);
			}
			return ZipConstants.ConvertToArray(str);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005D21 File Offset: 0x00004D21
		private ZipConstants()
		{
		}

		// Token: 0x0400009B RID: 155
		public const int VersionMadeBy = 51;

		// Token: 0x0400009C RID: 156
		[Obsolete("Use VersionMadeBy instead")]
		public const int VERSION_MADE_BY = 51;

		// Token: 0x0400009D RID: 157
		public const int VersionStrongEncryption = 50;

		// Token: 0x0400009E RID: 158
		[Obsolete("Use VersionStrongEncryption instead")]
		public const int VERSION_STRONG_ENCRYPTION = 50;

		// Token: 0x0400009F RID: 159
		public const int VERSION_AES = 51;

		// Token: 0x040000A0 RID: 160
		public const int VersionZip64 = 45;

		// Token: 0x040000A1 RID: 161
		public const int LocalHeaderBaseSize = 30;

		// Token: 0x040000A2 RID: 162
		[Obsolete("Use LocalHeaderBaseSize instead")]
		public const int LOCHDR = 30;

		// Token: 0x040000A3 RID: 163
		public const int Zip64DataDescriptorSize = 24;

		// Token: 0x040000A4 RID: 164
		public const int DataDescriptorSize = 16;

		// Token: 0x040000A5 RID: 165
		[Obsolete("Use DataDescriptorSize instead")]
		public const int EXTHDR = 16;

		// Token: 0x040000A6 RID: 166
		public const int CentralHeaderBaseSize = 46;

		// Token: 0x040000A7 RID: 167
		[Obsolete("Use CentralHeaderBaseSize instead")]
		public const int CENHDR = 46;

		// Token: 0x040000A8 RID: 168
		public const int EndOfCentralRecordBaseSize = 22;

		// Token: 0x040000A9 RID: 169
		[Obsolete("Use EndOfCentralRecordBaseSize instead")]
		public const int ENDHDR = 22;

		// Token: 0x040000AA RID: 170
		public const int CryptoHeaderSize = 12;

		// Token: 0x040000AB RID: 171
		[Obsolete("Use CryptoHeaderSize instead")]
		public const int CRYPTO_HEADER_SIZE = 12;

		// Token: 0x040000AC RID: 172
		public const int LocalHeaderSignature = 67324752;

		// Token: 0x040000AD RID: 173
		[Obsolete("Use LocalHeaderSignature instead")]
		public const int LOCSIG = 67324752;

		// Token: 0x040000AE RID: 174
		public const int SpanningSignature = 134695760;

		// Token: 0x040000AF RID: 175
		[Obsolete("Use SpanningSignature instead")]
		public const int SPANNINGSIG = 134695760;

		// Token: 0x040000B0 RID: 176
		public const int SpanningTempSignature = 808471376;

		// Token: 0x040000B1 RID: 177
		[Obsolete("Use SpanningTempSignature instead")]
		public const int SPANTEMPSIG = 808471376;

		// Token: 0x040000B2 RID: 178
		public const int DataDescriptorSignature = 134695760;

		// Token: 0x040000B3 RID: 179
		[Obsolete("Use DataDescriptorSignature instead")]
		public const int EXTSIG = 134695760;

		// Token: 0x040000B4 RID: 180
		[Obsolete("Use CentralHeaderSignature instead")]
		public const int CENSIG = 33639248;

		// Token: 0x040000B5 RID: 181
		public const int CentralHeaderSignature = 33639248;

		// Token: 0x040000B6 RID: 182
		public const int Zip64CentralFileHeaderSignature = 101075792;

		// Token: 0x040000B7 RID: 183
		[Obsolete("Use Zip64CentralFileHeaderSignature instead")]
		public const int CENSIG64 = 101075792;

		// Token: 0x040000B8 RID: 184
		public const int Zip64CentralDirLocatorSignature = 117853008;

		// Token: 0x040000B9 RID: 185
		public const int ArchiveExtraDataSignature = 117853008;

		// Token: 0x040000BA RID: 186
		public const int CentralHeaderDigitalSignature = 84233040;

		// Token: 0x040000BB RID: 187
		[Obsolete("Use CentralHeaderDigitalSignaure instead")]
		public const int CENDIGITALSIG = 84233040;

		// Token: 0x040000BC RID: 188
		public const int EndOfCentralDirectorySignature = 101010256;

		// Token: 0x040000BD RID: 189
		[Obsolete("Use EndOfCentralDirectorySignature instead")]
		public const int ENDSIG = 101010256;

		// Token: 0x040000BE RID: 190
		private static int defaultCodePage = (Thread.CurrentThread.CurrentCulture.TextInfo.OEMCodePage == 1 || Thread.CurrentThread.CurrentCulture.TextInfo.OEMCodePage == 2 || Thread.CurrentThread.CurrentCulture.TextInfo.OEMCodePage == 3 || Thread.CurrentThread.CurrentCulture.TextInfo.OEMCodePage == 42) ? 437 : Thread.CurrentThread.CurrentCulture.TextInfo.OEMCodePage;
	}
}
