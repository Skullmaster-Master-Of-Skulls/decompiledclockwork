using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009D9 RID: 2521
	internal static class BinaryFormatParser
	{
		// Token: 0x060063AE RID: 25518 RVA: 0x00174295 File Offset: 0x00172495
		public static bool IsSessionKey(int value)
		{
			return (value & 1) != 0;
		}

		// Token: 0x060063AF RID: 25519 RVA: 0x0017429D File Offset: 0x0017249D
		public static int GetSessionKey(int value)
		{
			return value / 2;
		}

		// Token: 0x060063B0 RID: 25520 RVA: 0x001742A2 File Offset: 0x001724A2
		public static int GetStaticKey(int value)
		{
			return value / 2;
		}

		// Token: 0x060063B1 RID: 25521 RVA: 0x001742A8 File Offset: 0x001724A8
		public static int ParseInt32(byte[] buffer, int offset, int size)
		{
			switch (size)
			{
			case 1:
				return (int)buffer[offset];
			case 2:
				return (int)(buffer[offset] & 127) + ((int)buffer[offset + 1] << 7);
			case 3:
				return (int)(buffer[offset] & 127) + ((int)(buffer[offset + 1] & 127) << 7) + ((int)buffer[offset + 2] << 14);
			case 4:
				return (int)(buffer[offset] & 127) + ((int)(buffer[offset + 1] & 127) << 7) + ((int)(buffer[offset + 2] & 127) << 14) + ((int)buffer[offset + 3] << 21);
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("size", size, SR.GetString("ValueMustBeInRange", new object[]
				{
					1,
					4
				})));
			}
		}

		// Token: 0x060063B2 RID: 25522 RVA: 0x00174360 File Offset: 0x00172560
		public static int ParseKey(byte[] buffer, int offset, int size)
		{
			return BinaryFormatParser.ParseInt32(buffer, offset, size);
		}

		// Token: 0x060063B3 RID: 25523 RVA: 0x0017436A File Offset: 0x0017256A
		public static UniqueId ParseUniqueID(byte[] buffer, int offset, int size)
		{
			return new UniqueId(buffer, offset);
		}

		// Token: 0x060063B4 RID: 25524 RVA: 0x00174374 File Offset: 0x00172574
		public static int MatchBytes(byte[] buffer, int offset, int size, byte[] buffer2)
		{
			if (size < buffer2.Length)
			{
				return 0;
			}
			int num = offset;
			int i = 0;
			while (i < buffer2.Length)
			{
				if (buffer2[i] != buffer[num])
				{
					return 0;
				}
				i++;
				num++;
			}
			return buffer2.Length;
		}

		// Token: 0x060063B5 RID: 25525 RVA: 0x001743AC File Offset: 0x001725AC
		public static bool MatchAttributeNode(byte[] buffer, int offset, int size)
		{
			if (size < 1)
			{
				return false;
			}
			XmlBinaryNodeType xmlBinaryNodeType = (XmlBinaryNodeType)buffer[offset];
			return xmlBinaryNodeType >= XmlBinaryNodeType.MinAttribute && xmlBinaryNodeType <= XmlBinaryNodeType.DictionaryAttribute;
		}

		// Token: 0x060063B6 RID: 25526 RVA: 0x001743D0 File Offset: 0x001725D0
		public static int MatchKey(byte[] buffer, int offset, int size)
		{
			return BinaryFormatParser.MatchInt32(buffer, offset, size);
		}

		// Token: 0x060063B7 RID: 25527 RVA: 0x001743DC File Offset: 0x001725DC
		public static int MatchInt32(byte[] buffer, int offset, int size)
		{
			if (size > 0 && (buffer[offset] & 128) == 0)
			{
				return 1;
			}
			if (size > 1 && (buffer[offset + 1] & 128) == 0)
			{
				return 2;
			}
			if (size > 2 && (buffer[offset + 2] & 128) == 0)
			{
				return 3;
			}
			if (size > 3 && (buffer[offset + 3] & 128) == 0)
			{
				return 4;
			}
			return 0;
		}

		// Token: 0x060063B8 RID: 25528 RVA: 0x00174434 File Offset: 0x00172634
		public static int MatchUniqueID(byte[] buffer, int offset, int size)
		{
			if (size < 16)
			{
				return 0;
			}
			return 16;
		}
	}
}
