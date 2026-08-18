using System;
using System.Collections.Generic;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200020D RID: 525
	[DataContract(Name = "ComPlusDispatchMethodSchema")]
	internal class ComPlusDispatchMethodSchema : TraceRecord
	{
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x00039519 File Offset: 0x00037719
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusDispatchMethodTraceRecord";
			}
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00039520 File Offset: 0x00037720
		internal override void WriteTo(XmlWriter xmlWriter)
		{
			ComPlusTraceRecord.SerializeRecord(xmlWriter, this);
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00039529 File Offset: 0x00037729
		public ComPlusDispatchMethodSchema(string name, List<DispatchProxy.ParamInfo> paramList, DispatchProxy.ParamInfo returnValue)
		{
			this.name = name;
			this.paramList = paramList;
			this.returnValue = returnValue;
		}

		// Token: 0x0400184C RID: 6220
		private const string schemaId = "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusDispatchMethodTraceRecord";

		// Token: 0x0400184D RID: 6221
		[DataMember(Name = "Name")]
		private string name;

		// Token: 0x0400184E RID: 6222
		[DataMember(Name = "ParameterInfo")]
		private List<DispatchProxy.ParamInfo> paramList;

		// Token: 0x0400184F RID: 6223
		[DataMember(Name = "ReturnValueInfo")]
		private DispatchProxy.ParamInfo returnValue;
	}
}
