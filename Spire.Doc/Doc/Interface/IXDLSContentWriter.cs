using System;
using System.Xml;

namespace Spire.Doc.Interface
{
	// Token: 0x02000369 RID: 873
	public interface IXDLSContentWriter
	{
		// Token: 0x0600310A RID: 12554
		void WriteChildBinaryElement(string name, byte[] value);

		// Token: 0x0600310B RID: 12555
		void WriteChildStringElement(string name, string value);

		// Token: 0x0600310C RID: 12556
		void WriteChildElement(string name, object value);

		// Token: 0x0600310D RID: 12557
		void WriteChildRefElement(string name, int refToElement);

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x0600310E RID: 12558
		XmlWriter InnerWriter { get; }
	}
}
