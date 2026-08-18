using System;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x020001F7 RID: 503
	public class Lifetime
	{
		// Token: 0x060010B0 RID: 4272 RVA: 0x0004733E File Offset: 0x0004553E
		public Lifetime(DateTime created, DateTime expires) : this(new DateTime?(created), new DateTime?(expires))
		{
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00047354 File Offset: 0x00045554
		public Lifetime(DateTime? created, DateTime? expires)
		{
			if (created != null && expires != null && expires.Value <= created.Value)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("ID2000")));
			}
			this._created = DateTimeUtil.ToUniversalTime(created);
			this._expires = DateTimeUtil.ToUniversalTime(expires);
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060010B2 RID: 4274 RVA: 0x000473C0 File Offset: 0x000455C0
		// (set) Token: 0x060010B3 RID: 4275 RVA: 0x000473C8 File Offset: 0x000455C8
		public DateTime? Created
		{
			get
			{
				return this._created;
			}
			set
			{
				this._created = value;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x000473D1 File Offset: 0x000455D1
		// (set) Token: 0x060010B5 RID: 4277 RVA: 0x000473D9 File Offset: 0x000455D9
		public DateTime? Expires
		{
			get
			{
				return this._expires;
			}
			set
			{
				this._expires = value;
			}
		}

		// Token: 0x04000E72 RID: 3698
		private DateTime? _created;

		// Token: 0x04000E73 RID: 3699
		private DateTime? _expires;
	}
}
