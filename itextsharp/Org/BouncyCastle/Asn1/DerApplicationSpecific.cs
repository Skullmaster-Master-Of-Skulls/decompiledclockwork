using System;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200048F RID: 1167
	public class DerApplicationSpecific : Asn1Object
	{
		// Token: 0x0600277F RID: 10111 RVA: 0x000EE171 File Offset: 0x000ED171
		internal DerApplicationSpecific(bool isConstructed, int tag, byte[] octets)
		{
			this.isConstructed = isConstructed;
			this.tag = tag;
			this.octets = octets;
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x000EE18E File Offset: 0x000ED18E
		public DerApplicationSpecific(int tag, byte[] octets) : this(false, tag, octets)
		{
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x000EE199 File Offset: 0x000ED199
		public DerApplicationSpecific(int tag, Asn1Encodable obj) : this(true, tag, obj)
		{
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x000EE1A4 File Offset: 0x000ED1A4
		public DerApplicationSpecific(bool isExplicit, int tag, Asn1Encodable obj)
		{
			byte[] derEncoded = obj.GetDerEncoded();
			this.isConstructed = isExplicit;
			this.tag = tag;
			if (isExplicit)
			{
				this.octets = derEncoded;
				return;
			}
			int lengthOfLength = this.GetLengthOfLength(derEncoded);
			byte[] array = new byte[derEncoded.Length - lengthOfLength];
			Array.Copy(derEncoded, lengthOfLength, array, 0, array.Length);
			this.octets = array;
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x000EE200 File Offset: 0x000ED200
		public DerApplicationSpecific(int tagNo, Asn1EncodableVector vec)
		{
			this.tag = tagNo;
			this.isConstructed = true;
			MemoryStream memoryStream = new MemoryStream();
			for (int num = 0; num != vec.Count; num++)
			{
				try
				{
					byte[] encoded = vec[num].GetEncoded();
					memoryStream.Write(encoded, 0, encoded.Length);
				}
				catch (IOException innerException)
				{
					throw new InvalidOperationException("malformed object", innerException);
				}
			}
			this.octets = memoryStream.ToArray();
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x000EE27C File Offset: 0x000ED27C
		private int GetLengthOfLength(byte[] data)
		{
			int num = 2;
			while ((data[num - 1] & 128) != 0)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x000EE29F File Offset: 0x000ED29F
		public bool IsConstructed()
		{
			return this.isConstructed;
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x000EE2A7 File Offset: 0x000ED2A7
		public byte[] GetContents()
		{
			return this.octets;
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06002787 RID: 10119 RVA: 0x000EE2AF File Offset: 0x000ED2AF
		public int ApplicationTag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x000EE2B7 File Offset: 0x000ED2B7
		public Asn1Object GetObject()
		{
			return Asn1Object.FromByteArray(this.GetContents());
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x000EE2C4 File Offset: 0x000ED2C4
		public Asn1Object GetObject(int derTagNo)
		{
			if (derTagNo >= 31)
			{
				throw new IOException("unsupported tag number");
			}
			byte[] encoded = base.GetEncoded();
			byte[] array = this.ReplaceTagNumber(derTagNo, encoded);
			if ((encoded[0] & 32) != 0)
			{
				byte[] array2 = array;
				int num = 0;
				array2[num] |= 32;
			}
			return Asn1Object.FromByteArray(array);
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x000EE318 File Offset: 0x000ED318
		internal override void Encode(DerOutputStream derOut)
		{
			int num = 64;
			if (this.isConstructed)
			{
				num |= 32;
			}
			derOut.WriteEncoded(num, this.tag, this.octets);
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x000EE348 File Offset: 0x000ED348
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerApplicationSpecific derApplicationSpecific = asn1Object as DerApplicationSpecific;
			return derApplicationSpecific != null && (this.isConstructed == derApplicationSpecific.isConstructed && this.tag == derApplicationSpecific.tag) && Arrays.AreEqual(this.octets, derApplicationSpecific.octets);
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x000EE390 File Offset: 0x000ED390
		protected override int Asn1GetHashCode()
		{
			return this.isConstructed.GetHashCode() ^ this.tag.GetHashCode() ^ Arrays.GetHashCode(this.octets);
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x000EE3C8 File Offset: 0x000ED3C8
		private byte[] ReplaceTagNumber(int newTag, byte[] input)
		{
			int num = (int)(input[0] & 31);
			int num2 = 1;
			if (num == 31)
			{
				num = 0;
				int num3 = (int)(input[num2++] & byte.MaxValue);
				if ((num3 & 127) == 0)
				{
					throw new InvalidOperationException("corrupted stream - invalid high tag number found");
				}
				while (num3 >= 0 && (num3 & 128) != 0)
				{
					num |= (num3 & 127);
					num <<= 7;
					num3 = (int)(input[num2++] & byte.MaxValue);
				}
				num |= (num3 & 127);
			}
			byte[] array = new byte[input.Length - num2 + 1];
			Array.Copy(input, num2, array, 1, array.Length - 1);
			array[0] = (byte)newTag;
			return array;
		}

		// Token: 0x04001B2D RID: 6957
		private readonly bool isConstructed;

		// Token: 0x04001B2E RID: 6958
		private readonly int tag;

		// Token: 0x04001B2F RID: 6959
		private readonly byte[] octets;
	}
}
