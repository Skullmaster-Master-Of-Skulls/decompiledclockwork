using System;
using System.Globalization;

namespace System.IdentityModel.Metadata
{
	// Token: 0x020000FB RID: 251
	public class LocalizedName : LocalizedEntry
	{
		// Token: 0x060006BB RID: 1723 RVA: 0x0001AB92 File Offset: 0x00018D92
		public LocalizedName() : this(null, null)
		{
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001AB9C File Offset: 0x00018D9C
		public LocalizedName(string name, CultureInfo language) : base(language)
		{
			this.name = name;
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0001ABAC File Offset: 0x00018DAC
		// (set) Token: 0x060006BE RID: 1726 RVA: 0x0001ABB4 File Offset: 0x00018DB4
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x04000A7B RID: 2683
		private string name;
	}
}
