using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008F1 RID: 2289
	internal static class BinaryPrimitives
	{
		// Token: 0x0600530F RID: 21263 RVA: 0x0012B4C2 File Offset: 0x0012A4C2
		public static bool TryReadUInt16BigEndian(ReadOnlySpan<byte> bytes, out ushort value)
		{
			if (bytes.Length < 2)
			{
				value = 0;
				return false;
			}
			value = (ushort)((int)bytes[1] | (int)bytes[0] << 8);
			return true;
		}

		// Token: 0x06005310 RID: 21264 RVA: 0x0012B4EA File Offset: 0x0012A4EA
		public static short ReadInt16BigEndian(ReadOnlySpan<byte> bytes)
		{
			return (short)((int)bytes[1] | (int)bytes[0] << 8);
		}
	}
}
