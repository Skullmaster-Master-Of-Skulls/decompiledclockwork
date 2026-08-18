using System;
using System.Collections;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel.SvcMapFileXmlSerializer
{
	// Token: 0x0200002D RID: 45
	internal class XmlSerializerContract : XmlSerializerImplementation
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000B4D1 File Offset: 0x000096D1
		public override XmlSerializationReader Reader
		{
			get
			{
				return new XmlSerializationReaderSvcMapFileImpl();
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000B4D8 File Offset: 0x000096D8
		public override XmlSerializationWriter Writer
		{
			get
			{
				return new XmlSerializationWriterSvcMapFileImpl();
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000B51C File Offset: 0x0000971C
		public override Hashtable ReadMethods
		{
			get
			{
				if (this.readMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["System.Web.Compilation.WCFModel.SvcMapFileImpl:urn:schemas-microsoft-com:xml-wcfservicemap:ReferenceGroup:True:"] = "Read16_ReferenceGroup";
					if (this.readMethods == null)
					{
						this.readMethods = hashtable;
					}
				}
				return this.readMethods;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000B55C File Offset: 0x0000975C
		public override Hashtable WriteMethods
		{
			get
			{
				if (this.writeMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["System.Web.Compilation.WCFModel.SvcMapFileImpl:urn:schemas-microsoft-com:xml-wcfservicemap:ReferenceGroup:True:"] = "Write16_ReferenceGroup";
					if (this.writeMethods == null)
					{
						this.writeMethods = hashtable;
					}
				}
				return this.writeMethods;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000B59C File Offset: 0x0000979C
		public override Hashtable TypedSerializers
		{
			get
			{
				if (this.typedSerializers == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add("System.Web.Compilation.WCFModel.SvcMapFileImpl:urn:schemas-microsoft-com:xml-wcfservicemap:ReferenceGroup:True:", new SvcMapFileImplSerializer());
					if (this.typedSerializers == null)
					{
						this.typedSerializers = hashtable;
					}
				}
				return this.typedSerializers;
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000B5DC File Offset: 0x000097DC
		public override bool CanSerialize(Type type)
		{
			return type == typeof(SvcMapFileImpl);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000B5F3 File Offset: 0x000097F3
		public override XmlSerializer GetSerializer(Type type)
		{
			if (type == typeof(SvcMapFileImpl))
			{
				return new SvcMapFileImplSerializer();
			}
			return null;
		}

		// Token: 0x040000B9 RID: 185
		private Hashtable readMethods;

		// Token: 0x040000BA RID: 186
		private Hashtable writeMethods;

		// Token: 0x040000BB RID: 187
		private Hashtable typedSerializers;
	}
}
