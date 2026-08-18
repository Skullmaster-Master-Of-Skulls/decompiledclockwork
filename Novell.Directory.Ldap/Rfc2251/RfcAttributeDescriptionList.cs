using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000C2 RID: 194
	public class RfcAttributeDescriptionList : Asn1SequenceOf
	{
		// Token: 0x060004F7 RID: 1271 RVA: 0x00017A40 File Offset: 0x00016A40
		public RfcAttributeDescriptionList(int size) : base(size)
		{
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00017A58 File Offset: 0x00016A58
		public RfcAttributeDescriptionList(string[] attrs) : base((attrs == null) ? 0 : attrs.Length)
		{
			if (attrs != null)
			{
				for (int i = 0; i < attrs.Length; i++)
				{
					base.add(new RfcAttributeDescription(attrs[i]));
				}
			}
		}
	}
}
