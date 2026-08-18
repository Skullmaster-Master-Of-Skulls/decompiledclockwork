using System;
using System.Xml.Linq;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020001A4 RID: 420
	internal class VersionedModel
	{
		// Token: 0x06000E44 RID: 3652 RVA: 0x0003EE31 File Offset: 0x0003D031
		public VersionedModel(XDocument model, string version = null)
		{
			this._model = model;
			this._version = version;
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x0003EE47 File Offset: 0x0003D047
		public XDocument Model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x0003EE4F File Offset: 0x0003D04F
		public string Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x040003D1 RID: 977
		private readonly XDocument _model;

		// Token: 0x040003D2 RID: 978
		private readonly string _version;
	}
}
