using System;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000209 RID: 521
	[DataContract(Name = "ComPlusMexChannelBuilder")]
	internal class ComPlusMexChannelBuilderSchema : TraceRecord
	{
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x0003941D File Offset: 0x0003761D
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusMexChannelBuilderTraceRecord";
			}
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x00039424 File Offset: 0x00037624
		internal override void WriteTo(XmlWriter xmlWriter)
		{
			ComPlusTraceRecord.SerializeRecord(xmlWriter, this);
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0003942D File Offset: 0x0003762D
		public ComPlusMexChannelBuilderSchema(string contract, string contractNamespace, string binding, string bindingNamespace, string address)
		{
			this.contract = contract;
			this.binding = binding;
			this.contractNamespace = contractNamespace;
			this.bindingNamespace = bindingNamespace;
			this.address = address;
		}

		// Token: 0x0400183B RID: 6203
		private const string schemaId = "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusMexChannelBuilderTraceRecord";

		// Token: 0x0400183C RID: 6204
		[DataMember(Name = "Contract")]
		private string contract;

		// Token: 0x0400183D RID: 6205
		[DataMember(Name = "contractNamespace")]
		private string contractNamespace;

		// Token: 0x0400183E RID: 6206
		[DataMember(Name = "bindingNamespace")]
		private string bindingNamespace;

		// Token: 0x0400183F RID: 6207
		[DataMember(Name = "Binding")]
		private string binding;

		// Token: 0x04001840 RID: 6208
		[DataMember(Name = "Address")]
		private string address;
	}
}
