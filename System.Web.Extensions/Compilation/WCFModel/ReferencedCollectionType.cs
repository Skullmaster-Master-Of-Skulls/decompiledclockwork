using System;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x02000020 RID: 32
	internal class ReferencedCollectionType
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00004BF2 File Offset: 0x00002DF2
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00004BFA File Offset: 0x00002DFA
		[XmlAttribute]
		public string TypeName
		{
			get
			{
				return this.m_TypeName;
			}
			set
			{
				this.m_TypeName = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00004C03 File Offset: 0x00002E03
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00004C0B File Offset: 0x00002E0B
		[XmlAttribute]
		public ReferencedCollectionType.CollectionCategory Category
		{
			get
			{
				return this.m_Category;
			}
			set
			{
				this.m_Category = value;
			}
		}

		// Token: 0x04000063 RID: 99
		private string m_TypeName;

		// Token: 0x04000064 RID: 100
		private ReferencedCollectionType.CollectionCategory m_Category;

		// Token: 0x0200012F RID: 303
		public enum CollectionCategory
		{
			// Token: 0x04000472 RID: 1138
			[XmlEnum(Name = "Unknown")]
			Unknown,
			// Token: 0x04000473 RID: 1139
			[XmlEnum(Name = "List")]
			List,
			// Token: 0x04000474 RID: 1140
			[XmlEnum(Name = "Dictionary")]
			Dictionary
		}
	}
}
