using System;
using System.Xml;

namespace Spire.Doc.Interface
{
	// Token: 0x0200050B RID: 1291
	public interface IXDLSContentReader
	{
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x0600425F RID: 16991
		string TagName { get; }

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06004260 RID: 16992
		XmlNodeType NodeType { get; }

		// Token: 0x06004261 RID: 16993
		string GetAttributeValue(string name);

		// Token: 0x06004262 RID: 16994
		bool ParseElementType(Type enumType, out Enum elementType);

		// Token: 0x06004263 RID: 16995
		bool ReadChildElement(object value);

		// Token: 0x06004264 RID: 16996
		object ReadChildElement(Type type);

		// Token: 0x06004265 RID: 16997
		string ReadChildStringContent();

		// Token: 0x06004266 RID: 16998
		byte[] ReadChildBinaryElement();

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06004267 RID: 16999
		XmlReader InnerReader { get; }

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06004268 RID: 17000
		IXDLSAttributeReader AttributeReader { get; }
	}
}
