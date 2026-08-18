using System;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001FA RID: 506
	[DataContract(Name = "ComPlusDllHostInitializerAddingHost")]
	internal class ComPlusDllHostInitializerAddingHostSchema : ComPlusDllHostInitializerSchema
	{
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x00038EF3 File Offset: 0x000370F3
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusDllHostInitializerAddingHostTraceRecord";
			}
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x00038EFA File Offset: 0x000370FA
		internal override void WriteTo(XmlWriter xmlWriter)
		{
			ComPlusTraceRecord.SerializeRecord(xmlWriter, this);
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x00038F04 File Offset: 0x00037104
		public ComPlusDllHostInitializerAddingHostSchema(Guid appid, Guid clsid, string behaviorConfiguration, string serviceType, string address, string bindingConfiguration, string bindingName, string bindingNamespace, string bindingSectionName, string contractType) : base(appid)
		{
			this.clsid = clsid;
			this.behaviorConfiguration = behaviorConfiguration;
			this.serviceType = serviceType;
			this.address = address;
			this.bindingConfiguration = bindingConfiguration;
			this.bindingName = bindingName;
			this.bindingNamespace = bindingNamespace;
			this.bindingSectionName = bindingSectionName;
			this.contractType = contractType;
		}

		// Token: 0x040017EB RID: 6123
		private const string schemaId = "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusDllHostInitializerAddingHostTraceRecord";

		// Token: 0x040017EC RID: 6124
		[DataMember(Name = "clsid")]
		private Guid clsid;

		// Token: 0x040017ED RID: 6125
		[DataMember(Name = "BehaviorConfiguration")]
		private string behaviorConfiguration;

		// Token: 0x040017EE RID: 6126
		[DataMember(Name = "ServiceType")]
		private string serviceType;

		// Token: 0x040017EF RID: 6127
		[DataMember(Name = "Address")]
		private string address;

		// Token: 0x040017F0 RID: 6128
		[DataMember(Name = "BindingConfiguration")]
		private string bindingConfiguration;

		// Token: 0x040017F1 RID: 6129
		[DataMember(Name = "BindingName")]
		private string bindingName;

		// Token: 0x040017F2 RID: 6130
		[DataMember(Name = "BindingNamespace")]
		private string bindingNamespace;

		// Token: 0x040017F3 RID: 6131
		[DataMember(Name = "BindingSectionName")]
		private string bindingSectionName;

		// Token: 0x040017F4 RID: 6132
		[DataMember(Name = "ContractType")]
		private string contractType;
	}
}
