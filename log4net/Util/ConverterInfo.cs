using System;

namespace log4net.Util
{
	// Token: 0x020000F2 RID: 242
	public sealed class ConverterInfo
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00015A97 File Offset: 0x00013C97
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x00015A9F File Offset: 0x00013C9F
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00015AA8 File Offset: 0x00013CA8
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x00015AB0 File Offset: 0x00013CB0
		public Type Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00015AB9 File Offset: 0x00013CB9
		public void AddProperty(PropertyEntry entry)
		{
			this.properties[entry.Key] = entry.Value;
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x00015AD2 File Offset: 0x00013CD2
		public PropertiesDictionary Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x0400029F RID: 671
		private string m_name;

		// Token: 0x040002A0 RID: 672
		private Type m_type;

		// Token: 0x040002A1 RID: 673
		private readonly PropertiesDictionary properties = new PropertiesDictionary();
	}
}
