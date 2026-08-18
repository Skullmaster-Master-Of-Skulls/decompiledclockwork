using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200014C RID: 332
	internal abstract class Mapping
	{
		// Token: 0x06001752 RID: 5970 RVA: 0x000674B1 File Offset: 0x000656B1
		internal Mapping()
		{
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x000674B9 File Offset: 0x000656B9
		protected Mapping(Mapping mapping)
		{
			this.isSoap = mapping.isSoap;
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x000674CD File Offset: 0x000656CD
		// (set) Token: 0x06001755 RID: 5973 RVA: 0x000674D5 File Offset: 0x000656D5
		internal bool IsSoap
		{
			get
			{
				return this.isSoap;
			}
			set
			{
				this.isSoap = value;
			}
		}

		// Token: 0x04000AD4 RID: 2772
		private bool isSoap;
	}
}
