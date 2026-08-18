using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WCFExtrasPlus.Wsdl.Documentation
{
	// Token: 0x02000018 RID: 24
	[DataContract(Namespace = "XmlCommentsExporter.Annotation")]
	internal class EnumAnnotation
	{
		// Token: 0x04000021 RID: 33
		[DataMember]
		public string EnumText;

		// Token: 0x04000022 RID: 34
		[DataMember]
		public Dictionary<string, string> Members = new Dictionary<string, string>();
	}
}
