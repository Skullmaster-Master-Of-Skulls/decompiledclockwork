using System;
using System.Runtime.Serialization;

namespace WCFExtrasPlus.Wsdl.Documentation
{
	// Token: 0x02000017 RID: 23
	[DataContract(Namespace = "XmlCommentsExporter.Annotation")]
	internal class Annotation
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00004630 File Offset: 0x00002830
		public Annotation(string s)
		{
			this.Text = s;
		}

		// Token: 0x0400001F RID: 31
		public const string AnnotationNamespace = "XmlCommentsExporter.Annotation";

		// Token: 0x04000020 RID: 32
		[DataMember]
		public string Text;
	}
}
