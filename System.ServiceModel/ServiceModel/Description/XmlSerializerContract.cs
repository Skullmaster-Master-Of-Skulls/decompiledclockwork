using System;
using System.Collections;
using System.Xml.Serialization;

namespace System.ServiceModel.Description
{
	// Token: 0x020003EE RID: 1006
	internal class XmlSerializerContract : XmlSerializerImplementation
	{
		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x060025D9 RID: 9689 RVA: 0x00089231 File Offset: 0x00087431
		public override XmlSerializationReader Reader
		{
			get
			{
				return new XmlSerializationReaderMetadataSet();
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x060025DA RID: 9690 RVA: 0x00089238 File Offset: 0x00087438
		public override XmlSerializationWriter Writer
		{
			get
			{
				return new XmlSerializationWriterMetadataSet();
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x060025DB RID: 9691 RVA: 0x00089240 File Offset: 0x00087440
		public override Hashtable ReadMethods
		{
			get
			{
				if (this.readMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["System.ServiceModel.Description.MetadataSet:http://schemas.xmlsoap.org/ws/2004/09/mex:Metadata:True:"] = "Read68_Metadata";
					if (this.readMethods == null)
					{
						this.readMethods = hashtable;
					}
				}
				return this.readMethods;
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x060025DC RID: 9692 RVA: 0x00089280 File Offset: 0x00087480
		public override Hashtable WriteMethods
		{
			get
			{
				if (this.writeMethods == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable["System.ServiceModel.Description.MetadataSet:http://schemas.xmlsoap.org/ws/2004/09/mex:Metadata:True:"] = "Write68_Metadata";
					if (this.writeMethods == null)
					{
						this.writeMethods = hashtable;
					}
				}
				return this.writeMethods;
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060025DD RID: 9693 RVA: 0x000892C0 File Offset: 0x000874C0
		public override Hashtable TypedSerializers
		{
			get
			{
				if (this.typedSerializers == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add("System.ServiceModel.Description.MetadataSet:http://schemas.xmlsoap.org/ws/2004/09/mex:Metadata:True:", new MetadataSetSerializer());
					if (this.typedSerializers == null)
					{
						this.typedSerializers = hashtable;
					}
				}
				return this.typedSerializers;
			}
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x00089300 File Offset: 0x00087500
		public override bool CanSerialize(Type type)
		{
			return type == typeof(MetadataSet);
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x00089317 File Offset: 0x00087517
		public override XmlSerializer GetSerializer(Type type)
		{
			if (type == typeof(MetadataSet))
			{
				return new MetadataSetSerializer();
			}
			return null;
		}

		// Token: 0x04002167 RID: 8551
		private Hashtable readMethods;

		// Token: 0x04002168 RID: 8552
		private Hashtable writeMethods;

		// Token: 0x04002169 RID: 8553
		private Hashtable typedSerializers;
	}
}
