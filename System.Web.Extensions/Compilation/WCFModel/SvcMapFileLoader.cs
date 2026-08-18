using System;
using System.IO;
using System.Web.Compilation.WCFModel.SvcMapFileXmlSerializer;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x02000025 RID: 37
	internal class SvcMapFileLoader : MapFileLoader
	{
		// Token: 0x0600016E RID: 366 RVA: 0x000059D6 File Offset: 0x00003BD6
		public SvcMapFileLoader(string mapFilePath)
		{
			this._mapFilePath = mapFilePath;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000059E5 File Offset: 0x00003BE5
		protected override string MapFileName
		{
			get
			{
				return this._mapFilePath;
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000059ED File Offset: 0x00003BED
		protected override MapFile Wrap(object mapFileImpl)
		{
			if (!(mapFileImpl is SvcMapFileImpl))
			{
				return null;
			}
			return new SvcMapFile((SvcMapFileImpl)mapFileImpl);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005A04 File Offset: 0x00003C04
		protected override object Unwrap(MapFile mapFile)
		{
			if (!(mapFile is SvcMapFile))
			{
				return null;
			}
			return ((SvcMapFile)mapFile).Impl;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005A1C File Offset: 0x00003C1C
		protected override XmlSchemaSet GetMapFileSchemaSet()
		{
			if (this._mapFileSchemaSet == null)
			{
				this._mapFileSchemaSet = new XmlSchemaSet();
				using (Stream manifestResourceStream = typeof(SvcMapFileImpl).Assembly.GetManifestResourceStream(typeof(SvcMapFileImpl), "Schema.ServiceMapSchema.xsd"))
				{
					this._mapFileSchemaSet.Add(XmlSchema.Read(manifestResourceStream, null));
				}
			}
			return this._mapFileSchemaSet;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005A98 File Offset: 0x00003C98
		protected override XmlSerializer GetMapFileSerializer()
		{
			if (this._mapFileSerializer == null)
			{
				this._mapFileSerializer = new SvcMapFileImplSerializer();
			}
			return this._mapFileSerializer;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005AB3 File Offset: 0x00003CB3
		protected override TextReader GetMapFileReader()
		{
			return File.OpenText(this._mapFilePath);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005AC0 File Offset: 0x00003CC0
		protected override byte[] ReadMetadataFile(string name)
		{
			return File.ReadAllBytes(Path.Combine(Path.GetDirectoryName(this._mapFilePath), name));
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005AC0 File Offset: 0x00003CC0
		protected override byte[] ReadExtensionFile(string name)
		{
			return File.ReadAllBytes(Path.Combine(Path.GetDirectoryName(this._mapFilePath), name));
		}

		// Token: 0x04000072 RID: 114
		private string _mapFilePath;

		// Token: 0x04000073 RID: 115
		private XmlSchemaSet _mapFileSchemaSet;

		// Token: 0x04000074 RID: 116
		private XmlSerializer _mapFileSerializer;
	}
}
