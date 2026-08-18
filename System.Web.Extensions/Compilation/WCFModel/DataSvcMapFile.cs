using System;
using System.Collections.Generic;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x0200000F RID: 15
	internal class DataSvcMapFile : MapFile
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000037F3 File Offset: 0x000019F3
		public DataSvcMapFileImpl Impl
		{
			get
			{
				return this._impl;
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000037FB File Offset: 0x000019FB
		public DataSvcMapFile()
		{
			this._impl = new DataSvcMapFileImpl();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000380E File Offset: 0x00001A0E
		public DataSvcMapFile(DataSvcMapFileImpl impl)
		{
			this._impl = impl;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000381D File Offset: 0x00001A1D
		// (set) Token: 0x0600009F RID: 159 RVA: 0x0000382A File Offset: 0x00001A2A
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

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003838 File Offset: 0x00001A38
		public override List<MetadataSource> MetadataSourceList
		{
			get
			{
				return this._impl.MetadataSourceList;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003845 File Offset: 0x00001A45
		public override List<MetadataFile> MetadataList
		{
			get
			{
				return this._impl.MetadataList;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003852 File Offset: 0x00001A52
		public override List<ExtensionFile> Extensions
		{
			get
			{
				return this._impl.Extensions;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000385F File Offset: 0x00001A5F
		public List<Parameter> Parameters
		{
			get
			{
				return this._impl.Parameters;
			}
		}

		// Token: 0x04000036 RID: 54
		private DataSvcMapFileImpl _impl;
	}
}
