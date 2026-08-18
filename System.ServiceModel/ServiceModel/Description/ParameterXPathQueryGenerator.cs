using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace System.ServiceModel.Description
{
	// Token: 0x020003DC RID: 988
	public static class ParameterXPathQueryGenerator
	{
		// Token: 0x06002532 RID: 9522 RVA: 0x000855AC File Offset: 0x000837AC
		public static string CreateFromDataContractSerializer(XName serviceContractName, string operationName, string parameterName, bool isReply, Type type, MemberInfo[] pathToMember, out XmlNamespaceManager namespaces)
		{
			if (type == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("type"));
			}
			if (pathToMember == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("pathToMember"));
			}
			if (operationName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("operationName"));
			}
			if (serviceContractName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("serviceContractName"));
			}
			if (isReply)
			{
				operationName += "Response";
			}
			StringBuilder stringBuilder = new StringBuilder("/xgSc:" + operationName);
			stringBuilder.Append("/xgSc:" + parameterName);
			string result = XPathQueryGenerator.CreateFromDataContractSerializer(type, pathToMember, stringBuilder, out namespaces);
			string text = serviceContractName.NamespaceName;
			if (string.IsNullOrEmpty(text))
			{
				text = "http://tempuri.org/";
			}
			namespaces.AddNamespace("xgSc", text);
			return result;
		}

		// Token: 0x040020B8 RID: 8376
		private const string XPathSeparator = "/";

		// Token: 0x040020B9 RID: 8377
		private const string NsSeparator = ":";

		// Token: 0x040020BA RID: 8378
		private const string ServiceContractPrefix = "xgSc";
	}
}
