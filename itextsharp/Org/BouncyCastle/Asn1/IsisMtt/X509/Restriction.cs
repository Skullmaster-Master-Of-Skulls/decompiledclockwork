using System;
using Org.BouncyCastle.Asn1.X500;

namespace Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x02000388 RID: 904
	public class Restriction : Asn1Encodable
	{
		// Token: 0x06001F7F RID: 8063 RVA: 0x000BC220 File Offset: 0x000BB220
		public static Restriction GetInstance(object obj)
		{
			if (obj == null || obj is Restriction)
			{
				return (Restriction)obj;
			}
			if (obj is IAsn1String)
			{
				return new Restriction(DirectoryString.GetInstance(obj));
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x000BC272 File Offset: 0x000BB272
		private Restriction(DirectoryString restriction)
		{
			this.restriction = restriction;
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x000BC281 File Offset: 0x000BB281
		public Restriction(string restriction)
		{
			this.restriction = new DirectoryString(restriction);
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001F82 RID: 8066 RVA: 0x000BC295 File Offset: 0x000BB295
		public virtual DirectoryString RestrictionString
		{
			get
			{
				return this.restriction;
			}
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x000BC29D File Offset: 0x000BB29D
		public override Asn1Object ToAsn1Object()
		{
			return this.restriction.ToAsn1Object();
		}

		// Token: 0x040015D6 RID: 5590
		private readonly DirectoryString restriction;
	}
}
