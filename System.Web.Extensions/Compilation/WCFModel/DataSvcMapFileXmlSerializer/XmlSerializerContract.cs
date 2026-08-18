using System;
using System.Collections;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel.DataSvcMapFileXmlSerializer
{
	// Token: 0x02000032 RID: 50
	internal class XmlSerializerContract : XmlSerializerImplementation
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000CF56 File Offset: 0x0000B156
		public override XmlSerializationReader Reader
		{
			get
			{
				return new XmlSerializationReaderDataSvcMapFileImpl();
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000CF5D File Offset: 0x0000B15D
		public override XmlSerializationWriter Writer
		{
			get
			{
				return new XmlSerializationWriterDataSvcMapFileImpl();
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000CF9C File Offset: 0x0000B19C
		public override Hashtable ReadMethods
		{
			get
			{
				if (this.readMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["System.Web.Compilation.WCFModel.DataSvcMapFileImpl:urn:schemas-microsoft-com:xml-dataservicemap:ReferenceGroup:True:"] = "Read9_ReferenceGroup";
					if (this.readMethods == null)
					{
						this.readMethods = hashtable;
					}
				}
				return this.readMethods;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000CFDC File Offset: 0x0000B1DC
		public override Hashtable WriteMethods
		{
			get
			{
				if (this.writeMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["System.Web.Compilation.WCFModel.DataSvcMapFileImpl:urn:schemas-microsoft-com:xml-dataservicemap:ReferenceGroup:True:"] = "Write9_ReferenceGroup";
					if (this.writeMethods == null)
					{
						this.writeMethods = hashtable;
					}
				}
				return this.writeMethods;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000D01C File Offset: 0x0000B21C
		public override Hashtable TypedSerializers
		{
			get
			{
				if (this.typedSerializers == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add("System.Web.Compilation.WCFModel.DataSvcMapFileImpl:urn:schemas-microsoft-com:xml-dataservicemap:ReferenceGroup:True:", new DataSvcMapFileImplSerializer());
					if (this.typedSerializers == null)
					{
						this.typedSerializers = hashtable;
					}
				}
				return this.typedSerializers;
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000D05C File Offset: 0x0000B25C
		public override bool CanSerialize(Type type)
		{
			return type == typeof(DataSvcMapFileImpl);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000D073 File Offset: 0x0000B273
		public override XmlSerializer GetSerializer(Type type)
		{
			if (type == typeof(DataSvcMapFileImpl))
			{
				return new DataSvcMapFileImplSerializer();
			}
			return null;
		}

		// Token: 0x040000D3 RID: 211
		private Hashtable readMethods;

		// Token: 0x040000D4 RID: 212
		private Hashtable writeMethods;

		// Token: 0x040000D5 RID: 213
		private Hashtable typedSerializers;
	}
}
