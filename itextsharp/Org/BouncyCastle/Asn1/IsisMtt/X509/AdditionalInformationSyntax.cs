using System;
using Org.BouncyCastle.Asn1.X500;

namespace Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x0200057C RID: 1404
	public class AdditionalInformationSyntax : Asn1Encodable
	{
		// Token: 0x06002FD8 RID: 12248 RVA: 0x0012781C File Offset: 0x0012681C
		public static AdditionalInformationSyntax GetInstance(object obj)
		{
			if (obj == null || obj is AdditionalInformationSyntax)
			{
				return (AdditionalInformationSyntax)obj;
			}
			if (obj is IAsn1String)
			{
				return new AdditionalInformationSyntax(DirectoryString.GetInstance(obj));
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x0012786E File Offset: 0x0012686E
		private AdditionalInformationSyntax(DirectoryString information)
		{
			this.information = information;
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x0012787D File Offset: 0x0012687D
		public AdditionalInformationSyntax(string information)
		{
			this.information = new DirectoryString(information);
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06002FDB RID: 12251 RVA: 0x00127891 File Offset: 0x00126891
		public virtual DirectoryString Information
		{
			get
			{
				return this.information;
			}
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x00127899 File Offset: 0x00126899
		public override Asn1Object ToAsn1Object()
		{
			return this.information.ToAsn1Object();
		}

		// Token: 0x040020E0 RID: 8416
		private readonly DirectoryString information;
	}
}
