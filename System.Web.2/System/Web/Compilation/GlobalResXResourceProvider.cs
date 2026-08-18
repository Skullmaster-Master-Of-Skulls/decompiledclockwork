using System;
using System.Resources;

namespace System.Web.Compilation
{
	// Token: 0x02000848 RID: 2120
	internal class GlobalResXResourceProvider : BaseResXResourceProvider
	{
		// Token: 0x060064B2 RID: 25778 RVA: 0x00160BD9 File Offset: 0x0015EDD9
		internal GlobalResXResourceProvider(string classKey)
		{
			this._classKey = classKey;
		}

		// Token: 0x060064B3 RID: 25779 RVA: 0x00160BE8 File Offset: 0x0015EDE8
		protected override ResourceManager CreateResourceManager()
		{
			string baseName = "Resources." + this._classKey;
			if (BuildManager.AppResourcesAssembly == null)
			{
				return null;
			}
			return new ResourceManager(baseName, BuildManager.AppResourcesAssembly)
			{
				IgnoreCase = true
			};
		}

		// Token: 0x17001C5D RID: 7261
		// (get) Token: 0x060064B4 RID: 25780 RVA: 0x00010D64 File Offset: 0x0000EF64
		public override IResourceReader ResourceReader
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x040033F6 RID: 13302
		private string _classKey;
	}
}
