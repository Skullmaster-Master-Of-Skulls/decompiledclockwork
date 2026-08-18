using System;
using System.Runtime.Serialization;

namespace WCFExtras.Wsdl.Documentation
{
	// Token: 0x02000021 RID: 33
	[DataContract(Namespace = "XmlCommentsExporter.Annotation")]
	internal class Annotation
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x000061B8 File Offset: 0x000043B8
		public Annotation(string s)
		{
			this.Text = s;
		}

		// Token: 0x04000030 RID: 48
		public const string AnnotationNamespace = "XmlCommentsExporter.Annotation";

		// Token: 0x04000031 RID: 49
		[DataMember]
		public string Text;
	}
}
