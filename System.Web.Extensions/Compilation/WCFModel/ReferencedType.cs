using System;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x02000021 RID: 33
	internal class ReferencedType
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00004C14 File Offset: 0x00002E14
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00004C1C File Offset: 0x00002E1C
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

		// Token: 0x04000065 RID: 101
		private string m_TypeName;
	}
}
