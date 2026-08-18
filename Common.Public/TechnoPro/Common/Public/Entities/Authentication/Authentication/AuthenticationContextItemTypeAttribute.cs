using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnoPro.Common.Public.Entities.Authentication.Authentication
{
	// Token: 0x0200049E RID: 1182
	public class AuthenticationContextItemTypeAttribute : Attribute
	{
		// Token: 0x0600239E RID: 9118 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public AuthenticationContextItemTypeAttribute()
		{
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x00027051 File Offset: 0x00025251
		public AuthenticationContextItemTypeAttribute(string title, string description)
		{
			this.Title = title;
			this.Description = description;
			this.Parameters = new AuthenticationContextItemParameter[0];
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x00027078 File Offset: 0x00025278
		public AuthenticationContextItemTypeAttribute(string title, string description, params AuthenticationContextItemParameter[] parameters)
		{
			this.Title = title;
			this.Description = description;
			this.Parameters = (parameters ?? new AuthenticationContextItemParameter[0]);
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x000270A4 File Offset: 0x000252A4
		public AuthenticationContextItemTypeAttribute(string title, string description, eAuthenticationContextItemParameter[] requiredParameters, eAuthenticationContextItemParameter[] optionalParameters)
		{
			this.Title = title;
			this.Description = description;
			List<AuthenticationContextItemParameter> list = (from g in requiredParameters ?? new eAuthenticationContextItemParameter[0]
			select new AuthenticationContextItemParameter(g, true)).ToList<AuthenticationContextItemParameter>();
			list.AddRange(from g in optionalParameters ?? new eAuthenticationContextItemParameter[0]
			select new AuthenticationContextItemParameter(g, false));
			this.Parameters = list.ToArray();
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x060023A2 RID: 9122 RVA: 0x00027142 File Offset: 0x00025342
		// (set) Token: 0x060023A3 RID: 9123 RVA: 0x0002714A File Offset: 0x0002534A
		public string Title { get; set; }

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x060023A4 RID: 9124 RVA: 0x00027153 File Offset: 0x00025353
		// (set) Token: 0x060023A5 RID: 9125 RVA: 0x0002715B File Offset: 0x0002535B
		public string Description { get; set; }

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x060023A6 RID: 9126 RVA: 0x00027164 File Offset: 0x00025364
		// (set) Token: 0x060023A7 RID: 9127 RVA: 0x0002716C File Offset: 0x0002536C
		public AuthenticationContextItemParameter[] Parameters { get; set; }

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x060023A8 RID: 9128 RVA: 0x00027175 File Offset: 0x00025375
		// (set) Token: 0x060023A9 RID: 9129 RVA: 0x0002717D File Offset: 0x0002537D
		public bool IsHidden { get; set; }
	}
}
