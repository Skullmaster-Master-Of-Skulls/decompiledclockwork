using System;
using System.IO;
using System.Web.Compilation.WCFModel.DataSvcMapFileXmlSerializer;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x02000011 RID: 17
	internal class DataSvcMapFileLoader : MapFileLoader
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00003923 File Offset: 0x00001B23
		public DataSvcMapFileLoader(string mapFilePath)
		{
			this._mapFilePath = mapFilePath;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003932 File Offset: 0x00001B32
		protected override string MapFileName
		{
			get
			{
				return this._mapFilePath;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000393A File Offset: 0x00001B3A
		protected override MapFile Wrap(object mapFileImpl)
		{
			if (!(mapFileImpl is DataSvcMapFileImpl))
			{
				return null;
			}
			return new DataSvcMapFile((DataSvcMapFileImpl)mapFileImpl);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003951 File Offset: 0x00001B51
		protected override object Unwrap(MapFile mapFile)
		{
			if (!(mapFile is DataSvcMapFile))
			{
				return null;
			}
			return ((DataSvcMapFile)mapFile).Impl;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003968 File Offset: 0x00001B68
		protected override XmlSchemaSet GetMapFileSchemaSet()
		{
			if (this._mapFileSchemaSet == null)
			{
				this._mapFileSchemaSet = new XmlSchemaSet();
				using (Stream manifestResourceStream = typeof(DataSvcMapFileImpl).Assembly.GetManifestResourceStream(typeof(DataSvcMapFileImpl), "Schema.DataServiceMapSchema.xsd"))
				{
					this._mapFileSchemaSet.Add(XmlSchema.Read(manifestResourceStream, null));
				}
			}
			return this._mapFileSchemaSet;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000039E4 File Offset: 0x00001BE4
		protected override XmlSerializer GetMapFileSerializer()
		{
			if (this._mapFileSerializer == null)
			{
				this._mapFileSerializer = new DataSvcMapFileImplSerializer();
			}
			return this._mapFileSerializer;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000039FF File Offset: 0x00001BFF
		protected override TextReader GetMapFileReader()
		{
			return File.OpenText(this._mapFilePath);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003A0C File Offset: 0x00001C0C
		protected override byte[] ReadMetadataFile(string name)
		{
			return File.ReadAllBytes(Path.Combine(Path.GetDirectoryName(this._mapFilePath), name));
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003A0C File Offset: 0x00001C0C
		protected override byte[] ReadExtensionFile(string name)
		{
			return File.ReadAllBytes(Path.Combine(Path.GetDirectoryName(this._mapFilePath), name));
		}

		// Token: 0x0400003D RID: 61
		private string _mapFilePath;

		// Token: 0x0400003E RID: 62
		private XmlSchemaSet _mapFileSchemaSet;

		// Token: 0x0400003F RID: 63
		private XmlSerializer _mapFileSerializer;
	}
}
