using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Claims;

namespace System.Web.Security
{
	// Token: 0x020005E3 RID: 1507
	[ComVisible(false)]
	[Serializable]
	public class FormsIdentity : ClaimsIdentity
	{
		// Token: 0x17001669 RID: 5737
		// (get) Token: 0x06004C18 RID: 19480 RVA: 0x001040F0 File Offset: 0x001022F0
		public override string Name
		{
			get
			{
				return this._Ticket.Name;
			}
		}

		// Token: 0x1700166A RID: 5738
		// (get) Token: 0x06004C19 RID: 19481 RVA: 0x001040FD File Offset: 0x001022FD
		public override string AuthenticationType
		{
			get
			{
				return "Forms";
			}
		}

		// Token: 0x1700166B RID: 5739
		// (get) Token: 0x06004C1A RID: 19482 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool IsAuthenticated
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700166C RID: 5740
		// (get) Token: 0x06004C1B RID: 19483 RVA: 0x00104104 File Offset: 0x00102304
		public FormsAuthenticationTicket Ticket
		{
			get
			{
				return this._Ticket;
			}
		}

		// Token: 0x1700166D RID: 5741
		// (get) Token: 0x06004C1C RID: 19484 RVA: 0x0010410C File Offset: 0x0010230C
		public override IEnumerable<Claim> Claims
		{
			get
			{
				return base.Claims;
			}
		}

		// Token: 0x06004C1D RID: 19485 RVA: 0x00104114 File Offset: 0x00102314
		public FormsIdentity(FormsAuthenticationTicket ticket)
		{
			if (ticket == null)
			{
				throw new ArgumentNullException("ticket");
			}
			this._Ticket = ticket;
			this.AddNameClaim();
		}

		// Token: 0x06004C1E RID: 19486 RVA: 0x00104137 File Offset: 0x00102337
		protected FormsIdentity(FormsIdentity identity) : base(identity)
		{
			this._Ticket = identity._Ticket;
		}

		// Token: 0x06004C1F RID: 19487 RVA: 0x0010414C File Offset: 0x0010234C
		public override ClaimsIdentity Clone()
		{
			return new FormsIdentity(this);
		}

		// Token: 0x06004C20 RID: 19488 RVA: 0x00104154 File Offset: 0x00102354
		[OnDeserialized]
		private void OnDeserializedMethod(StreamingContext context)
		{
			bool flag = false;
			using (IEnumerator<Claim> enumerator = base.Claims.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					Claim claim = enumerator.Current;
					flag = true;
				}
			}
			if (!flag)
			{
				this.AddNameClaim();
			}
		}

		// Token: 0x06004C21 RID: 19489 RVA: 0x001041AC File Offset: 0x001023AC
		[SecuritySafeCritical]
		private void AddNameClaim()
		{
			if (this._Ticket != null && this._Ticket.Name != null)
			{
				base.AddClaim(new Claim(base.NameClaimType, this._Ticket.Name, "http://www.w3.org/2001/XMLSchema#string", "Forms", "Forms", this));
			}
		}

		// Token: 0x040028F6 RID: 10486
		private FormsAuthenticationTicket _Ticket;
	}
}
