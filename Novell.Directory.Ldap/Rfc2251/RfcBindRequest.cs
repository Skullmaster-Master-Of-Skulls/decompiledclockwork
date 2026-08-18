using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000C8 RID: 200
	public class RfcBindRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00017BB4 File Offset: 0x00016BB4
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x00017BD4 File Offset: 0x00016BD4
		public virtual Asn1Integer Version
		{
			get
			{
				return (Asn1Integer)base.get_Renamed(0);
			}
			set
			{
				base.set_Renamed(0, value);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00017BEC File Offset: 0x00016BEC
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x00017C0C File Offset: 0x00016C0C
		public virtual RfcLdapDN Name
		{
			get
			{
				return (RfcLdapDN)base.get_Renamed(1);
			}
			set
			{
				base.set_Renamed(1, value);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00017C24 File Offset: 0x00016C24
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x00017C44 File Offset: 0x00016C44
		public virtual RfcAuthenticationChoice AuthenticationChoice
		{
			get
			{
				return (RfcAuthenticationChoice)base.get_Renamed(2);
			}
			set
			{
				base.set_Renamed(2, value);
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00017C5C File Offset: 0x00016C5C
		public RfcBindRequest(Asn1Integer version, RfcLdapDN name, RfcAuthenticationChoice auth) : base(3)
		{
			base.add(version);
			base.add(name);
			base.add(auth);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00017C88 File Offset: 0x00016C88
		[CLSCompliant(false)]
		public RfcBindRequest(int version, string dn, string mechanism, sbyte[] credentials) : this(new Asn1Integer(version), new RfcLdapDN(dn), new RfcAuthenticationChoice(mechanism, credentials))
		{
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00017CB0 File Offset: 0x00016CB0
		internal RfcBindRequest(Asn1Object[] origRequest, string base_Renamed) : base(origRequest, origRequest.Length)
		{
			if (base_Renamed != null)
			{
				base.set_Renamed(1, new RfcLdapDN(base_Renamed));
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00017CDC File Offset: 0x00016CDC
		public override Asn1Identifier getIdentifier()
		{
			return RfcBindRequest.ID;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00017CF4 File Offset: 0x00016CF4
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcBindRequest(base.toArray(), base_Renamed);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00017D14 File Offset: 0x00016D14
		public string getRequestDN()
		{
			return ((RfcLdapDN)base.get_Renamed(1)).stringValue();
		}

		// Token: 0x040003FB RID: 1019
		private static readonly Asn1Identifier ID = new Asn1Identifier(1, true, 0);
	}
}
