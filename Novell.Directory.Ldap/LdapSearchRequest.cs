using System;
using System.Collections;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000044 RID: 68
	public class LdapSearchRequest : LdapMessage
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000DE7C File Offset: 0x0000CE7C
		public virtual string DN
		{
			get
			{
				return this.Asn1Object.RequestDN;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000DE98 File Offset: 0x0000CE98
		public virtual int Scope
		{
			get
			{
				return ((Asn1Enumerated)((RfcSearchRequest)this.Asn1Object.get_Renamed(1)).get_Renamed(1)).intValue();
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000DECC File Offset: 0x0000CECC
		public virtual int Dereference
		{
			get
			{
				return ((Asn1Enumerated)((RfcSearchRequest)this.Asn1Object.get_Renamed(1)).get_Renamed(2)).intValue();
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000DF00 File Offset: 0x0000CF00
		public virtual int MaxResults
		{
			get
			{
				return ((Asn1Integer)((RfcSearchRequest)this.Asn1Object.get_Renamed(1)).get_Renamed(3)).intValue();
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000DF34 File Offset: 0x0000CF34
		public virtual int ServerTimeLimit
		{
			get
			{
				return ((Asn1Integer)((RfcSearchRequest)this.Asn1Object.get_Renamed(1)).get_Renamed(4)).intValue();
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000DF68 File Offset: 0x0000CF68
		public virtual bool TypesOnly
		{
			get
			{
				return ((Asn1Boolean)((RfcSearchRequest)this.Asn1Object.get_Renamed(1)).get_Renamed(5)).booleanValue();
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000DF9C File Offset: 0x0000CF9C
		public virtual string[] Attributes
		{
			get
			{
				RfcAttributeDescriptionList rfcAttributeDescriptionList = (RfcAttributeDescriptionList)((RfcSearchRequest)this.Asn1Object.get_Renamed(1)).get_Renamed(7);
				string[] array = new string[rfcAttributeDescriptionList.size()];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = ((RfcAttributeDescription)rfcAttributeDescriptionList.get_Renamed(i)).stringValue();
				}
				return array;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000DFFC File Offset: 0x0000CFFC
		public virtual string StringFilter
		{
			get
			{
				return this.RfcFilter.filterToString();
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000E018 File Offset: 0x0000D018
		private RfcFilter RfcFilter
		{
			get
			{
				return (RfcFilter)((RfcSearchRequest)this.Asn1Object.get_Renamed(1)).get_Renamed(6);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000E048 File Offset: 0x0000D048
		public virtual IEnumerator SearchFilter
		{
			get
			{
				return this.RfcFilter.getFilterIterator();
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000E064 File Offset: 0x0000D064
		public LdapSearchRequest(string base_Renamed, int scope, string filter, string[] attrs, int dereference, int maxResults, int serverTimeLimit, bool typesOnly, LdapControl[] cont) : base(3, new RfcSearchRequest(new RfcLdapDN(base_Renamed), new Asn1Enumerated(scope), new Asn1Enumerated(dereference), new Asn1Integer(maxResults), new Asn1Integer(serverTimeLimit), new Asn1Boolean(typesOnly), new RfcFilter(filter), new RfcAttributeDescriptionList(attrs)), cont)
		{
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000E0B8 File Offset: 0x0000D0B8
		public LdapSearchRequest(string base_Renamed, int scope, RfcFilter filter, string[] attrs, int dereference, int maxResults, int serverTimeLimit, bool typesOnly, LdapControl[] cont) : base(3, new RfcSearchRequest(new RfcLdapDN(base_Renamed), new Asn1Enumerated(scope), new Asn1Enumerated(dereference), new Asn1Integer(maxResults), new Asn1Integer(serverTimeLimit), new Asn1Boolean(typesOnly), filter, new RfcAttributeDescriptionList(attrs)), cont)
		{
		}

		// Token: 0x0400013D RID: 317
		public const int AND = 0;

		// Token: 0x0400013E RID: 318
		public const int OR = 1;

		// Token: 0x0400013F RID: 319
		public const int NOT = 2;

		// Token: 0x04000140 RID: 320
		public const int EQUALITY_MATCH = 3;

		// Token: 0x04000141 RID: 321
		public const int SUBSTRINGS = 4;

		// Token: 0x04000142 RID: 322
		public const int GREATER_OR_EQUAL = 5;

		// Token: 0x04000143 RID: 323
		public const int LESS_OR_EQUAL = 6;

		// Token: 0x04000144 RID: 324
		public const int PRESENT = 7;

		// Token: 0x04000145 RID: 325
		public const int APPROX_MATCH = 8;

		// Token: 0x04000146 RID: 326
		public const int EXTENSIBLE_MATCH = 9;

		// Token: 0x04000147 RID: 327
		public const int INITIAL = 0;

		// Token: 0x04000148 RID: 328
		public const int ANY = 1;

		// Token: 0x04000149 RID: 329
		public const int FINAL = 2;
	}
}
