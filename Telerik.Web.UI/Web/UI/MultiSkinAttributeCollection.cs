using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000F1D RID: 3869
	public class MultiSkinAttributeCollection
	{
		// Token: 0x17002EBA RID: 11962
		// (get) Token: 0x060093C0 RID: 37824 RVA: 0x00212ABC File Offset: 0x00210CBC
		// (set) Token: 0x060093C1 RID: 37825 RVA: 0x00212AD7 File Offset: 0x00210CD7
		public IList<EmbeddedSkinAttribute> SkinAttributes
		{
			get
			{
				if (this._skinAttributes == null)
				{
					this._skinAttributes = new List<EmbeddedSkinAttribute>();
				}
				return this._skinAttributes;
			}
			set
			{
				this._skinAttributes = value;
			}
		}

		// Token: 0x17002EBB RID: 11963
		// (get) Token: 0x060093C2 RID: 37826 RVA: 0x00212AE0 File Offset: 0x00210CE0
		// (set) Token: 0x060093C3 RID: 37827 RVA: 0x00212AE8 File Offset: 0x00210CE8
		public bool AllSkinsRegistered { get; set; }

		// Token: 0x17002EBC RID: 11964
		// (get) Token: 0x060093C4 RID: 37828 RVA: 0x00212AF1 File Offset: 0x00210CF1
		// (set) Token: 0x060093C5 RID: 37829 RVA: 0x00212AF9 File Offset: 0x00210CF9
		public bool OfficialSkinsRegistered { get; set; }

		// Token: 0x17002EBD RID: 11965
		// (get) Token: 0x060093C6 RID: 37830 RVA: 0x00212B02 File Offset: 0x00210D02
		// (set) Token: 0x060093C7 RID: 37831 RVA: 0x00212B0A File Offset: 0x00210D0A
		public bool CustomSkinsRegistered { get; set; }

		// Token: 0x04002A5E RID: 10846
		private IList<EmbeddedSkinAttribute> _skinAttributes;
	}
}
