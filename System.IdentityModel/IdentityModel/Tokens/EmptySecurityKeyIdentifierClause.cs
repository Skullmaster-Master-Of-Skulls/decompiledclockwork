using System;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000119 RID: 281
	public class EmptySecurityKeyIdentifierClause : SecurityKeyIdentifierClause
	{
		// Token: 0x060007AF RID: 1967 RVA: 0x0002090C File Offset: 0x0001EB0C
		public EmptySecurityKeyIdentifierClause() : this(null)
		{
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00020915 File Offset: 0x0001EB15
		public EmptySecurityKeyIdentifierClause(object context) : base(typeof(EmptySecurityKeyIdentifierClause).ToString())
		{
			this._context = context;
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x00020933 File Offset: 0x0001EB33
		public object Context
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x04000AD5 RID: 2773
		private object _context;
	}
}
