using System;
using TechnoPro.Common.Public.Entities.CustomForms.Data;

namespace TechnoPro.Common.Public.Entities.CustomForms.Field
{
	// Token: 0x02000420 RID: 1056
	public class CustomControlTypeAttribute : Attribute
	{
		// Token: 0x0600201F RID: 8223 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public CustomControlTypeAttribute()
		{
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x000246ED File Offset: 0x000228ED
		public CustomControlTypeAttribute(string controlCode, string title, params eCustomDataPrimitiveType[] supportedPrimitives)
		{
			this.ControlCode = controlCode;
			this.SupportedPrimitives = supportedPrimitives;
			this.Title = title;
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06002021 RID: 8225 RVA: 0x0002470F File Offset: 0x0002290F
		// (set) Token: 0x06002022 RID: 8226 RVA: 0x00024717 File Offset: 0x00022917
		public bool IsHidden { get; set; }

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x00024720 File Offset: 0x00022920
		// (set) Token: 0x06002024 RID: 8228 RVA: 0x00024728 File Offset: 0x00022928
		public eCustomDataPrimitiveType[] SupportedPrimitives { get; set; }

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x06002025 RID: 8229 RVA: 0x00024731 File Offset: 0x00022931
		// (set) Token: 0x06002026 RID: 8230 RVA: 0x00024739 File Offset: 0x00022939
		public string ControlCode { get; set; }

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x06002027 RID: 8231 RVA: 0x00024742 File Offset: 0x00022942
		// (set) Token: 0x06002028 RID: 8232 RVA: 0x0002474A File Offset: 0x0002294A
		public string Title { get; set; }
	}
}
