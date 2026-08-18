using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000152 RID: 338
	internal class ConstantMapping : Mapping
	{
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x0600177D RID: 6013 RVA: 0x000676C1 File Offset: 0x000658C1
		// (set) Token: 0x0600177E RID: 6014 RVA: 0x000676D7 File Offset: 0x000658D7
		internal string XmlName
		{
			get
			{
				if (this.xmlName != null)
				{
					return this.xmlName;
				}
				return string.Empty;
			}
			set
			{
				this.xmlName = value;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x0600177F RID: 6015 RVA: 0x000676E0 File Offset: 0x000658E0
		// (set) Token: 0x06001780 RID: 6016 RVA: 0x000676F6 File Offset: 0x000658F6
		internal string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x000676FF File Offset: 0x000658FF
		// (set) Token: 0x06001782 RID: 6018 RVA: 0x00067707 File Offset: 0x00065907
		internal long Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x04000AE4 RID: 2788
		private string xmlName;

		// Token: 0x04000AE5 RID: 2789
		private string name;

		// Token: 0x04000AE6 RID: 2790
		private long value;
	}
}
