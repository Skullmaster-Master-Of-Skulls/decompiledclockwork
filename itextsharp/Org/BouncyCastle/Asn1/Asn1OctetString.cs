using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200031D RID: 797
	public abstract class Asn1OctetString : Asn1Object, Asn1OctetStringParser, IAsn1Convertible
	{
		// Token: 0x06001CF1 RID: 7409 RVA: 0x000AC2B4 File Offset: 0x000AB2B4
		public static Asn1OctetString GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return Asn1OctetString.GetInstance(obj.GetObject());
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x000AC2C4 File Offset: 0x000AB2C4
		public static Asn1OctetString GetInstance(object obj)
		{
			if (obj == null || obj is Asn1OctetString)
			{
				return (Asn1OctetString)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return Asn1OctetString.GetInstance(((Asn1TaggedObject)obj).GetObject());
			}
			if (obj is Asn1Sequence)
			{
				ArrayList arrayList = new ArrayList();
				foreach (object value in ((Asn1Sequence)obj))
				{
					arrayList.Add(value);
				}
				return new BerOctetString(arrayList);
			}
			throw new ArgumentException("illegal object in GetInstance: " + obj.GetType().Name);
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x000AC374 File Offset: 0x000AB374
		internal Asn1OctetString(byte[] str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.str = str;
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x000AC394 File Offset: 0x000AB394
		internal Asn1OctetString(Asn1Encodable obj)
		{
			try
			{
				this.str = obj.GetEncoded("DER");
			}
			catch (IOException ex)
			{
				throw new ArgumentException("Error processing object : " + ex.ToString());
			}
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x000AC3E4 File Offset: 0x000AB3E4
		public Stream GetOctetStream()
		{
			return new MemoryStream(this.str, false);
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001CF6 RID: 7414 RVA: 0x000AC3F2 File Offset: 0x000AB3F2
		public Asn1OctetStringParser Parser
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x000AC3F5 File Offset: 0x000AB3F5
		public virtual byte[] GetOctets()
		{
			return this.str;
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x000AC3FD File Offset: 0x000AB3FD
		protected override int Asn1GetHashCode()
		{
			return Arrays.GetHashCode(this.GetOctets());
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x000AC40C File Offset: 0x000AB40C
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerOctetString derOctetString = asn1Object as DerOctetString;
			return derOctetString != null && Arrays.AreEqual(this.GetOctets(), derOctetString.GetOctets());
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x000AC436 File Offset: 0x000AB436
		public override string ToString()
		{
			return "#" + Hex.ToHexString(this.str);
		}

		// Token: 0x040013F4 RID: 5108
		internal byte[] str;
	}
}
