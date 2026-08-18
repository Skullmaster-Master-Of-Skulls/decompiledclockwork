using System;
using System.Security.Cryptography;

namespace System.Web.SessionState
{
	// Token: 0x02000129 RID: 297
	internal static class SessionId
	{
		// Token: 0x060011CA RID: 4554 RVA: 0x00031FA4 File Offset: 0x000301A4
		static SessionId()
		{
			for (int i = SessionId.s_encoding.Length - 1; i >= 0; i--)
			{
				char c = SessionId.s_encoding[i];
				SessionId.s_legalchars[(int)c] = true;
			}
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00031FFC File Offset: 0x000301FC
		internal static bool IsLegit(string s)
		{
			if (s == null || s.Length != 24)
			{
				return false;
			}
			bool result;
			try
			{
				int num = 24;
				while (--num >= 0)
				{
					char c = s[num];
					if (!SessionId.s_legalchars[(int)c])
					{
						return false;
					}
				}
				result = true;
			}
			catch (IndexOutOfRangeException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00032058 File Offset: 0x00030258
		private static string Encode(byte[] buffer)
		{
			char[] array = new char[24];
			int num = 0;
			for (int i = 0; i < 15; i += 5)
			{
				int num2 = (int)buffer[i] | (int)buffer[i + 1] << 8 | (int)buffer[i + 2] << 16 | (int)buffer[i + 3] << 24;
				int num3 = num2 & 31;
				array[num++] = SessionId.s_encoding[num3];
				num3 = (num2 >> 5 & 31);
				array[num++] = SessionId.s_encoding[num3];
				num3 = (num2 >> 10 & 31);
				array[num++] = SessionId.s_encoding[num3];
				num3 = (num2 >> 15 & 31);
				array[num++] = SessionId.s_encoding[num3];
				num3 = (num2 >> 20 & 31);
				array[num++] = SessionId.s_encoding[num3];
				num3 = (num2 >> 25 & 31);
				array[num++] = SessionId.s_encoding[num3];
				num2 = ((num2 >> 30 & 3) | (int)buffer[i + 4] << 2);
				num3 = (num2 & 31);
				array[num++] = SessionId.s_encoding[num3];
				num3 = (num2 >> 5 & 31);
				array[num++] = SessionId.s_encoding[num3];
			}
			return new string(array);
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00032168 File Offset: 0x00030368
		internal static string Create(ref RandomNumberGenerator randgen)
		{
			if (randgen == null)
			{
				randgen = new RNGCryptoServiceProvider();
			}
			byte[] array = new byte[15];
			randgen.GetBytes(array);
			return SessionId.Encode(array);
		}

		// Token: 0x0400140F RID: 5135
		internal const int NUM_CHARS_IN_ENCODING = 32;

		// Token: 0x04001410 RID: 5136
		internal const int ENCODING_BITS_PER_CHAR = 5;

		// Token: 0x04001411 RID: 5137
		internal const int ID_LENGTH_BITS = 120;

		// Token: 0x04001412 RID: 5138
		internal const int ID_LENGTH_BYTES = 15;

		// Token: 0x04001413 RID: 5139
		internal const int ID_LENGTH_CHARS = 24;

		// Token: 0x04001414 RID: 5140
		private static char[] s_encoding = new char[]
		{
			'a',
			'b',
			'c',
			'd',
			'e',
			'f',
			'g',
			'h',
			'i',
			'j',
			'k',
			'l',
			'm',
			'n',
			'o',
			'p',
			'q',
			'r',
			's',
			't',
			'u',
			'v',
			'w',
			'x',
			'y',
			'z',
			'0',
			'1',
			'2',
			'3',
			'4',
			'5'
		};

		// Token: 0x04001415 RID: 5141
		private static bool[] s_legalchars = new bool[128];
	}
}
