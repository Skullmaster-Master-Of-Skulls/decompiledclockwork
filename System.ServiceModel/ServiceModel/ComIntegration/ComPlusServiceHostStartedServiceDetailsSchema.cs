using System;
using System.Runtime.Serialization;
using System.Web.Services.Description;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001F7 RID: 503
	[DataContract(Name = "ComPlusServiceHostStartedServiceDetails")]
	internal class ComPlusServiceHostStartedServiceDetailsSchema : ComPlusServiceHostSchema
	{
		// Token: 0x06000FD5 RID: 4053 RVA: 0x00038E4B File Offset: 0x0003704B
		public ComPlusServiceHostStartedServiceDetailsSchema(Guid appid, Guid clsid, ServiceDescription wsdl) : base(appid, clsid)
		{
			this.wsdlWrapper = new WsdlWrapper(wsdl);
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00038E61 File Offset: 0x00037061
		internal override string EventId
		{
			get
			{
				return base.BuildEventId("ComPlusServiceHostStartedServiceDetails");
			}
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00038E6E File Offset: 0x0003706E
		internal override void WriteTo(XmlWriter xmlWriter)
		{
			ComPlusTraceRecord.SerializeRecord(xmlWriter, this);
		}

		// Token: 0x040017E5 RID: 6117
		[DataMember(Name = "ServiceDescription")]
		private WsdlWrapper wsdlWrapper;
	}
}
