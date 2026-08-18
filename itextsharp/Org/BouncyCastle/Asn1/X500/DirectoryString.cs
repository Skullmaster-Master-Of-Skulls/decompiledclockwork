using System;

namespace Org.BouncyCastle.Asn1.X500
{
	// Token: 0x02000564 RID: 1380
	public class DirectoryString : Asn1Encodable, IAsn1Choice, IAsn1String
	{
		// Token: 0x06002F6B RID: 12139 RVA: 0x001263D8 File Offset: 0x001253D8
		public static DirectoryString GetInstance(object obj)
		{
			if (obj is DirectoryString)
			{
				return (DirectoryString)obj;
			}
			if (obj is DerStringBase && (obj is DerT61String || obj is DerPrintableString || obj is DerUniversalString || obj is DerUtf8String || obj is DerBmpString))
			{
				return new DirectoryString((DerStringBase)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x0012644F File Offset: 0x0012544F
		public static DirectoryString GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			if (!isExplicit)
			{
				throw new ArgumentException("choice item must be explicitly tagged");
			}
			return DirectoryString.GetInstance(obj.GetObject());
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x0012646A File Offset: 0x0012546A
		private DirectoryString(DerStringBase str)
		{
			this.str = str;
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x00126479 File Offset: 0x00125479
		public DirectoryString(string str)
		{
			this.str = new DerUtf8String(str);
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x0012648D File Offset: 0x0012548D
		public string GetString()
		{
			return this.str.GetString();
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x0012649A File Offset: 0x0012549A
		public override Asn1Object ToAsn1Object()
		{
			return this.str.ToAsn1Object();
		}

		// Token: 0x040020AC RID: 8364
		private readonly DerStringBase str;
	}
}
