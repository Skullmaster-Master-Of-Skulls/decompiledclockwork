using System;
using System.IdentityModel.Tokens;

namespace System.IdentityModel.Metadata
{
	// Token: 0x020000FD RID: 253
	public abstract class MetadataBase
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0001ABE8 File Offset: 0x00018DE8
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x0001ABF0 File Offset: 0x00018DF0
		public SigningCredentials SigningCredentials
		{
			get
			{
				return this._signingCredentials;
			}
			set
			{
				this._signingCredentials = value;
			}
		}

		// Token: 0x04000A7D RID: 2685
		private SigningCredentials _signingCredentials;
	}
}
