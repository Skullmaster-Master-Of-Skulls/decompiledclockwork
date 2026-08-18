using System;
using System.Collections.Generic;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x0200003E RID: 62
	public class IdentityResult
	{
		// Token: 0x0600011C RID: 284 RVA: 0x00006AC4 File Offset: 0x00004CC4
		public IdentityResult(params string[] errors) : this((IEnumerable<string>)errors)
		{
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006AD4 File Offset: 0x00004CD4
		public IdentityResult(IEnumerable<string> errors)
		{
			if (errors == null)
			{
				errors = new string[]
				{
					Resources.DefaultError
				};
			}
			this.Succeeded = false;
			this.Errors = errors;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006B0A File Offset: 0x00004D0A
		protected IdentityResult(bool success)
		{
			this.Succeeded = success;
			this.Errors = new string[0];
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00006B25 File Offset: 0x00004D25
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00006B2D File Offset: 0x00004D2D
		public bool Succeeded { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00006B36 File Offset: 0x00004D36
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00006B3E File Offset: 0x00004D3E
		public IEnumerable<string> Errors { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00006B47 File Offset: 0x00004D47
		public static IdentityResult Success
		{
			get
			{
				return IdentityResult._success;
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006B4E File Offset: 0x00004D4E
		public static IdentityResult Failed(params string[] errors)
		{
			return new IdentityResult(errors);
		}

		// Token: 0x0400002D RID: 45
		private static readonly IdentityResult _success = new IdentityResult(true);
	}
}
