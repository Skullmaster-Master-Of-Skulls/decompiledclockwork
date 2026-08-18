using System;
using System.Text;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000563 RID: 1379
	public class AuthorityInformationAccess : Asn1Encodable
	{
		// Token: 0x06002F64 RID: 12132 RVA: 0x00126230 File Offset: 0x00125230
		public static AuthorityInformationAccess GetInstance(object obj)
		{
			if (obj is AuthorityInformationAccess)
			{
				return (AuthorityInformationAccess)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AuthorityInformationAccess((Asn1Sequence)obj);
			}
			if (obj is X509Extension)
			{
				return AuthorityInformationAccess.GetInstance(X509Extension.ConvertValueToObject((X509Extension)obj));
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x00126298 File Offset: 0x00125298
		private AuthorityInformationAccess(Asn1Sequence seq)
		{
			if (seq.Count < 1)
			{
				throw new ArgumentException("sequence may not be empty");
			}
			this.descriptions = new AccessDescription[seq.Count];
			for (int i = 0; i < seq.Count; i++)
			{
				this.descriptions[i] = AccessDescription.GetInstance(seq[i]);
			}
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x001262F8 File Offset: 0x001252F8
		[Obsolete("Use version taking an AccessDescription instead")]
		public AuthorityInformationAccess(DerObjectIdentifier oid, GeneralName location)
		{
			this.descriptions = new AccessDescription[]
			{
				new AccessDescription(oid, location)
			};
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x00126324 File Offset: 0x00125324
		public AuthorityInformationAccess(AccessDescription description)
		{
			this.descriptions = new AccessDescription[]
			{
				description
			};
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x00126349 File Offset: 0x00125349
		public AccessDescription[] GetAccessDescriptions()
		{
			return (AccessDescription[])this.descriptions.Clone();
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x0012635B File Offset: 0x0012535B
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(this.descriptions);
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x00126368 File Offset: 0x00125368
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Platform.NewLine;
			stringBuilder.Append("AuthorityInformationAccess:");
			stringBuilder.Append(newLine);
			foreach (AccessDescription value in this.descriptions)
			{
				stringBuilder.Append("    ");
				stringBuilder.Append(value);
				stringBuilder.Append(newLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040020AB RID: 8363
		private readonly AccessDescription[] descriptions;
	}
}
