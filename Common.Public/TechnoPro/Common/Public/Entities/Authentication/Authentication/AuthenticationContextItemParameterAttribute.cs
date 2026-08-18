using System;

namespace TechnoPro.Common.Public.Entities.Authentication.Authentication
{
	// Token: 0x020004A1 RID: 1185
	public class AuthenticationContextItemParameterAttribute : Attribute
	{
		// Token: 0x060023B0 RID: 9136 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public AuthenticationContextItemParameterAttribute()
		{
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x000271C2 File Offset: 0x000253C2
		public AuthenticationContextItemParameterAttribute(string argName, string title, string description, string editor = null)
		{
			this.ArgName = argName;
			this.Title = title;
			this.Description = description;
			this.Editor = editor;
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x000271ED File Offset: 0x000253ED
		public AuthenticationContextItemParameterAttribute(string argName, bool isHidden)
		{
			this.ArgName = argName;
			this.IsHidden = isHidden;
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x060023B3 RID: 9139 RVA: 0x00027207 File Offset: 0x00025407
		// (set) Token: 0x060023B4 RID: 9140 RVA: 0x0002720F File Offset: 0x0002540F
		public string ArgName { get; set; }

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x060023B5 RID: 9141 RVA: 0x00027218 File Offset: 0x00025418
		// (set) Token: 0x060023B6 RID: 9142 RVA: 0x00027220 File Offset: 0x00025420
		public bool IsHidden { get; set; }

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x060023B7 RID: 9143 RVA: 0x00027229 File Offset: 0x00025429
		// (set) Token: 0x060023B8 RID: 9144 RVA: 0x00027231 File Offset: 0x00025431
		public string Title { get; set; }

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x060023B9 RID: 9145 RVA: 0x0002723A File Offset: 0x0002543A
		// (set) Token: 0x060023BA RID: 9146 RVA: 0x00027242 File Offset: 0x00025442
		public string Description { get; set; }

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x060023BB RID: 9147 RVA: 0x0002724B File Offset: 0x0002544B
		// (set) Token: 0x060023BC RID: 9148 RVA: 0x00027253 File Offset: 0x00025453
		public string Editor { get; set; }
	}
}
