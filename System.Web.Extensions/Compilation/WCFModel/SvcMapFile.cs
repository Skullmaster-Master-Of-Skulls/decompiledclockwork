using System;
using System.Collections.Generic;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x02000023 RID: 35
	internal class SvcMapFile : MapFile
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000588C File Offset: 0x00003A8C
		public SvcMapFileImpl Impl
		{
			get
			{
				return this._impl;
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005894 File Offset: 0x00003A94
		public SvcMapFile()
		{
			this._impl = new SvcMapFileImpl();
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000058A7 File Offset: 0x00003AA7
		public SvcMapFile(SvcMapFileImpl impl)
		{
			this._impl = impl;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000160 RID: 352 RVA: 0x000058B6 File Offset: 0x00003AB6
		// (set) Token: 0x06000161 RID: 353 RVA: 0x000058C3 File Offset: 0x00003AC3
		public override string ID
		{
			get
			{
				return this._impl.ID;
			}
			set
			{
				this._impl.ID = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000058D1 File Offset: 0x00003AD1
		public override List<MetadataSource> MetadataSourceList
		{
			get
			{
				return this._impl.MetadataSourceList;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000058DE File Offset: 0x00003ADE
		public override List<MetadataFile> MetadataList
		{
			get
			{
				return this._impl.MetadataList;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000058EB File Offset: 0x00003AEB
		public override List<ExtensionFile> Extensions
		{
			get
			{
				return this._impl.Extensions;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000058F8 File Offset: 0x00003AF8
		public ClientOptions ClientOptions
		{
			get
			{
				return this._impl.ClientOptions;
			}
		}

		// Token: 0x0400006B RID: 107
		private SvcMapFileImpl _impl;
	}
}
