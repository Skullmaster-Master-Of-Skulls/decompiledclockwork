using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200029F RID: 671
	public class TlsUtilities
	{
		// Token: 0x06001946 RID: 6470 RVA: 0x00093B67 File Offset: 0x00092B67
		internal static void WriteUint8(short i, Stream os)
		{
			os.WriteByte((byte)i);
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00093B71 File Offset: 0x00092B71
		internal static void WriteUint8(short i, byte[] buf, int offset)
		{
			buf[offset] = (byte)i;
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x00093B78 File Offset: 0x00092B78
		internal static void WriteUint16(int i, Stream os)
		{
			os.WriteByte((byte)(i >> 8));
			os.WriteByte((byte)i);
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x00093B8C File Offset: 0x00092B8C
		internal static void WriteUint16(int i, byte[] buf, int offset)
		{
			buf[offset] = (byte)(i >> 8);
			buf[offset + 1] = (byte)i;
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x00093B9C File Offset: 0x00092B9C
		internal static void WriteUint24(int i, Stream os)
		{
			os.WriteByte((byte)(i >> 16));
			os.WriteByte((byte)(i >> 8));
			os.WriteByte((byte)i);
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x00093BBB File Offset: 0x00092BBB
		internal static void WriteUint24(int i, byte[] buf, int offset)
		{
			buf[offset] = (byte)(i >> 16);
			buf[offset + 1] = (byte)(i >> 8);
			buf[offset + 2] = (byte)i;
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x00093BD8 File Offset: 0x00092BD8
		internal static void WriteUint64(long i, Stream os)
		{
			os.WriteByte((byte)(i >> 56));
			os.WriteByte((byte)(i >> 48));
			os.WriteByte((byte)(i >> 40));
			os.WriteByte((byte)(i >> 32));
			os.WriteByte((byte)(i >> 24));
			os.WriteByte((byte)(i >> 16));
			os.WriteByte((byte)(i >> 8));
			os.WriteByte((byte)i);
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x00093C3C File Offset: 0x00092C3C
		internal static void WriteUint64(long i, byte[] buf, int offset)
		{
			buf[offset] = (byte)(i >> 56);
			buf[offset + 1] = (byte)(i >> 48);
			buf[offset + 2] = (byte)(i >> 40);
			buf[offset + 3] = (byte)(i >> 32);
			buf[offset + 4] = (byte)(i >> 24);
			buf[offset + 5] = (byte)(i >> 16);
			buf[offset + 6] = (byte)(i >> 8);
			buf[offset + 7] = (byte)i;
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x00093C93 File Offset: 0x00092C93
		internal static void WriteOpaque8(byte[] buf, Stream os)
		{
			TlsUtilities.WriteUint8((short)buf.Length, os);
			os.Write(buf, 0, buf.Length);
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x00093CAA File Offset: 0x00092CAA
		internal static void WriteOpaque16(byte[] buf, Stream os)
		{
			TlsUtilities.WriteUint16(buf.Length, os);
			os.Write(buf, 0, buf.Length);
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x00093CC0 File Offset: 0x00092CC0
		internal static void WriteOpaque24(byte[] buf, Stream os)
		{
			TlsUtilities.WriteUint24(buf.Length, os);
			os.Write(buf, 0, buf.Length);
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x00093CD8 File Offset: 0x00092CD8
		internal static short ReadUint8(Stream inStr)
		{
			int num = inStr.ReadByte();
			if (num < 0)
			{
				throw new EndOfStreamException();
			}
			return (short)num;
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x00093CF8 File Offset: 0x00092CF8
		internal static int ReadUint16(Stream inStr)
		{
			int num = inStr.ReadByte();
			int num2 = inStr.ReadByte();
			if ((num | num2) < 0)
			{
				throw new EndOfStreamException();
			}
			return num << 8 | num2;
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x00093D24 File Offset: 0x00092D24
		internal static int ReadUint24(Stream inStr)
		{
			int num = inStr.ReadByte();
			int num2 = inStr.ReadByte();
			int num3 = inStr.ReadByte();
			if ((num | num2 | num3) < 0)
			{
				throw new EndOfStreamException();
			}
			return num << 16 | num2 << 8 | num3;
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x00093D5E File Offset: 0x00092D5E
		internal static void ReadFully(byte[] buf, Stream inStr)
		{
			if (Streams.ReadFully(inStr, buf, 0, buf.Length) < buf.Length)
			{
				throw new EndOfStreamException();
			}
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x00093D78 File Offset: 0x00092D78
		internal static byte[] ReadOpaque8(Stream inStr)
		{
			short num = TlsUtilities.ReadUint8(inStr);
			byte[] array = new byte[(int)num];
			TlsUtilities.ReadFully(array, inStr);
			return array;
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x00093D9C File Offset: 0x00092D9C
		internal static byte[] ReadOpaque16(Stream inStr)
		{
			int num = TlsUtilities.ReadUint16(inStr);
			byte[] array = new byte[num];
			TlsUtilities.ReadFully(array, inStr);
			return array;
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x00093DBF File Offset: 0x00092DBF
		internal static void CheckVersion(byte[] readVersion, TlsProtocolHandler handler)
		{
			if (readVersion[0] != 3 || readVersion[1] != 1)
			{
				handler.FailWithError(2, 70);
			}
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x00093DD8 File Offset: 0x00092DD8
		internal static void CheckVersion(Stream inStr, TlsProtocolHandler handler)
		{
			int num = inStr.ReadByte();
			int num2 = inStr.ReadByte();
			if (num != 3 || num2 != 1)
			{
				handler.FailWithError(2, 70);
			}
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x00093E04 File Offset: 0x00092E04
		internal static void WriteVersion(Stream os)
		{
			os.WriteByte(3);
			os.WriteByte(1);
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x00093E14 File Offset: 0x00092E14
		internal static void WriteVersion(byte[] buf, int offset)
		{
			buf[offset] = 3;
			buf[offset + 1] = 1;
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x00093E20 File Offset: 0x00092E20
		private static void hmac_hash(IDigest digest, byte[] secret, byte[] seed, byte[] output)
		{
			HMac hmac = new HMac(digest);
			KeyParameter parameters = new KeyParameter(secret);
			byte[] array = seed;
			int digestSize = digest.GetDigestSize();
			int num = (output.Length + digestSize - 1) / digestSize;
			byte[] array2 = new byte[hmac.GetMacSize()];
			byte[] array3 = new byte[hmac.GetMacSize()];
			for (int i = 0; i < num; i++)
			{
				hmac.Init(parameters);
				hmac.BlockUpdate(array, 0, array.Length);
				hmac.DoFinal(array2, 0);
				array = array2;
				hmac.Init(parameters);
				hmac.BlockUpdate(array, 0, array.Length);
				hmac.BlockUpdate(seed, 0, seed.Length);
				hmac.DoFinal(array3, 0);
				Array.Copy(array3, 0, output, digestSize * i, Math.Min(digestSize, output.Length - digestSize * i));
			}
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x00093EDC File Offset: 0x00092EDC
		internal static void PRF(byte[] secret, string asciiLabel, byte[] seed, byte[] buf)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(asciiLabel);
			int num = (secret.Length + 1) / 2;
			byte[] array = new byte[num];
			byte[] array2 = new byte[num];
			Array.Copy(secret, 0, array, 0, num);
			Array.Copy(secret, secret.Length - num, array2, 0, num);
			byte[] array3 = new byte[bytes.Length + seed.Length];
			Array.Copy(bytes, 0, array3, 0, bytes.Length);
			Array.Copy(seed, 0, array3, bytes.Length, seed.Length);
			byte[] array4 = new byte[buf.Length];
			TlsUtilities.hmac_hash(new MD5Digest(), array, array3, array4);
			TlsUtilities.hmac_hash(new Sha1Digest(), array2, array3, buf);
			for (int i = 0; i < buf.Length; i++)
			{
				int num2 = i;
				buf[num2] ^= array4[i];
			}
		}
	}
}
