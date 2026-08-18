using System;

namespace System.Xml.Schema
{
	// Token: 0x02000275 RID: 629
	public sealed class XmlSchemaCompilationSettings
	{
		// Token: 0x06001D30 RID: 7472 RVA: 0x00085C5D File Offset: 0x00084C5D
		public XmlSchemaCompilationSettings()
		{
			this.enableUpaCheck = true;
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001D31 RID: 7473 RVA: 0x00085C6C File Offset: 0x00084C6C
		// (set) Token: 0x06001D32 RID: 7474 RVA: 0x00085C74 File Offset: 0x00084C74
		public bool EnableUpaCheck
		{
			get
			{
				return this.enableUpaCheck;
			}
			set
			{
				this.enableUpaCheck = value;
			}
		}

		// Token: 0x040011CF RID: 4559
		private bool enableUpaCheck;
	}
}
