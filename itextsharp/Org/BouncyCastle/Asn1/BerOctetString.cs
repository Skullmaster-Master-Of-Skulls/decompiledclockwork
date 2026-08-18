using System;
using System.Collections;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200031F RID: 799
	public class BerOctetString : DerOctetString, IEnumerable
	{
		// Token: 0x06001CFF RID: 7423 RVA: 0x000AC47C File Offset: 0x000AB47C
		private static byte[] ToBytes(IEnumerable octs)
		{
			MemoryStream memoryStream = new MemoryStream();
			foreach (object obj in octs)
			{
				DerOctetString derOctetString = (DerOctetString)obj;
				byte[] octets = derOctetString.GetOctets();
				memoryStream.Write(octets, 0, octets.Length);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x000AC4EC File Offset: 0x000AB4EC
		public BerOctetString(byte[] str) : base(str)
		{
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x000AC4F5 File Offset: 0x000AB4F5
		public BerOctetString(IEnumerable octets) : base(BerOctetString.ToBytes(octets))
		{
			this.octs = octets;
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x000AC50A File Offset: 0x000AB50A
		public BerOctetString(Asn1Object obj) : base(obj)
		{
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x000AC513 File Offset: 0x000AB513
		public BerOctetString(Asn1Encodable obj) : base(obj.ToAsn1Object())
		{
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x000AC521 File Offset: 0x000AB521
		public override byte[] GetOctets()
		{
			return this.str;
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x000AC529 File Offset: 0x000AB529
		public IEnumerator GetEnumerator()
		{
			if (this.octs == null)
			{
				return this.GenerateOcts().GetEnumerator();
			}
			return this.octs.GetEnumerator();
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x000AC54A File Offset: 0x000AB54A
		[Obsolete("Use GetEnumerator() instead")]
		public IEnumerator GetObjects()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x000AC554 File Offset: 0x000AB554
		private ArrayList GenerateOcts()
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < this.str.Length; i += 1000)
			{
				int num = Math.Min(this.str.Length, i + 1000);
				byte[] array = new byte[num - i];
				Array.Copy(this.str, i, array, 0, array.Length);
				arrayList.Add(new DerOctetString(array));
			}
			return arrayList;
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x000AC5BC File Offset: 0x000AB5BC
		internal override void Encode(DerOutputStream derOut)
		{
			if (derOut is Asn1OutputStream || derOut is BerOutputStream)
			{
				derOut.WriteByte(36);
				derOut.WriteByte(128);
				foreach (object obj in this)
				{
					DerOctetString obj2 = (DerOctetString)obj;
					derOut.WriteObject(obj2);
				}
				derOut.WriteByte(0);
				derOut.WriteByte(0);
				return;
			}
			base.Encode(derOut);
		}

		// Token: 0x040013F5 RID: 5109
		private const int MaxLength = 1000;

		// Token: 0x040013F6 RID: 5110
		private readonly IEnumerable octs;
	}
}
