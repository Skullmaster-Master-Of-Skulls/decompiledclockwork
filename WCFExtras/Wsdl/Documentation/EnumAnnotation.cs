using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WCFExtras.Wsdl.Documentation
{
	// Token: 0x02000022 RID: 34
	[DataContract(Namespace = "XmlCommentsExporter.Annotation")]
	internal class EnumAnnotation
	{
		// Token: 0x04000032 RID: 50
		[DataMember]
		public string EnumText;

		// Token: 0x04000033 RID: 51
		[DataMember]
		public Dictionary<string, string> Members = new Dictionary<string, string>();
	}
}
