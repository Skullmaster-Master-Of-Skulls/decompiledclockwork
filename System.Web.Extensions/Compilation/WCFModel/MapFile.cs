using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x02000018 RID: 24
	internal abstract class MapFile
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00003CD0 File Offset: 0x00001ED0
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00003CF3 File Offset: 0x00001EF3
		public IEnumerable<ProxyGenerationError> LoadErrors
		{
			get
			{
				if (this._loadErrors == null)
				{
					return Enumerable.Empty<ProxyGenerationError>();
				}
				return this._loadErrors;
			}
			internal set
			{
				this._loadErrors = new List<ProxyGenerationError>(value);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000DB RID: 219
		// (set) Token: 0x060000DC RID: 220
		public abstract string ID { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000DD RID: 221
		public abstract List<MetadataSource> MetadataSourceList { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000DE RID: 222
		public abstract List<MetadataFile> MetadataList { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000DF RID: 223
		public abstract List<ExtensionFile> Extensions { get; }

		// Token: 0x0400004A RID: 74
		private List<ProxyGenerationError> _loadErrors;
	}
}
