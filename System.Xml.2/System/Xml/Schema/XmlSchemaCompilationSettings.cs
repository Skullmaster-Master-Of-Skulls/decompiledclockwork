using System;

namespace System.Xml.Schema
{
	// Token: 0x020002AA RID: 682
	public sealed class XmlSchemaCompilationSettings
	{
		// Token: 0x060027C9 RID: 10185 RVA: 0x000D1D69 File Offset: 0x000CFF69
		public XmlSchemaCompilationSettings()
		{
			this.enableUpaCheck = true;
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x060027CA RID: 10186 RVA: 0x000D1D78 File Offset: 0x000CFF78
		// (set) Token: 0x060027CB RID: 10187 RVA: 0x000D1D80 File Offset: 0x000CFF80
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

		// Token: 0x04001145 RID: 4421
		private bool enableUpaCheck;
	}
}
