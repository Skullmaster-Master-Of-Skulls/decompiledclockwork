using System;
using System.Globalization;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Description
{
	// Token: 0x02000421 RID: 1057
	internal static class NamingHelper
	{
		// Token: 0x06002883 RID: 10371 RVA: 0x00097DA8 File Offset: 0x00095FA8
		internal static string CombineUriStrings(string baseUri, string path)
		{
			if (Uri.IsWellFormedUriString(path, UriKind.Absolute) || path == string.Empty)
			{
				return path;
			}
			if (baseUri.EndsWith("/", StringComparison.Ordinal))
			{
				return baseUri + (path.StartsWith("/", StringComparison.Ordinal) ? path.Substring(1) : path);
			}
			return baseUri + (path.StartsWith("/", StringComparison.Ordinal) ? path : ("/" + path));
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x00097E1C File Offset: 0x0009601C
		internal static string TypeName(Type t)
		{
			if (t.IsGenericType || t.ContainsGenericParameters)
			{
				Type[] genericArguments = t.GetGenericArguments();
				int num = t.Name.IndexOf('`');
				string text = (num > 0) ? t.Name.Substring(0, num) : t.Name;
				text += "Of";
				for (int i = 0; i < genericArguments.Length; i++)
				{
					text = text + "_" + NamingHelper.TypeName(genericArguments[i]);
				}
				return text;
			}
			if (t.IsArray)
			{
				return "ArrayOf" + NamingHelper.TypeName(t.GetElementType());
			}
			return t.Name;
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x00097EBC File Offset: 0x000960BC
		internal static XmlQualifiedName GetContractName(Type contractType, string name, string ns)
		{
			XmlName xmlName = new XmlName(name ?? NamingHelper.TypeName(contractType));
			if (ns == null)
			{
				ns = "http://tempuri.org/";
			}
			return new XmlQualifiedName(xmlName.EncodedName, ns);
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x00097EF0 File Offset: 0x000960F0
		internal static XmlName GetOperationName(string logicalMethodName, string name)
		{
			return new XmlName(string.IsNullOrEmpty(name) ? logicalMethodName : name);
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x00097F04 File Offset: 0x00096104
		internal static string GetMessageAction(OperationDescription operation, bool isResponse)
		{
			ContractDescription declaringContract = operation.DeclaringContract;
			XmlQualifiedName contractName = new XmlQualifiedName(declaringContract.Name, declaringContract.Namespace);
			return NamingHelper.GetMessageAction(contractName, operation.CodeName, null, isResponse);
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x00097F38 File Offset: 0x00096138
		internal static string GetMessageAction(XmlQualifiedName contractName, string opname, string action, bool isResponse)
		{
			if (action != null)
			{
				return action;
			}
			StringBuilder stringBuilder = new StringBuilder(64);
			if (string.IsNullOrEmpty(contractName.Namespace))
			{
				stringBuilder.Append("urn:");
			}
			else
			{
				stringBuilder.Append(contractName.Namespace);
				if (!contractName.Namespace.EndsWith("/", StringComparison.Ordinal))
				{
					stringBuilder.Append('/');
				}
			}
			stringBuilder.Append(contractName.Name);
			stringBuilder.Append('/');
			action = (isResponse ? (opname + "Response") : opname);
			return NamingHelper.CombineUriStrings(stringBuilder.ToString(), action);
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x00097FCC File Offset: 0x000961CC
		internal static string GetUniqueName(string baseName, NamingHelper.DoesNameExist doesNameExist, object nameCollection)
		{
			for (int i = 0; i < 2147483647; i++)
			{
				string text = (i > 0) ? (baseName + i.ToString()) : baseName;
				if (!doesNameExist(text, nameCollection))
				{
					return text;
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Cannot generate unique name for name {0}", new object[]
			{
				baseName
			})));
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x00098034 File Offset: 0x00096234
		internal static void CheckUriProperty(string ns, string propName)
		{
			Uri uri;
			if (!Uri.TryCreate(ns, UriKind.RelativeOrAbsolute, out uri))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SFXUnvalidNamespaceValue", new object[]
				{
					ns,
					propName
				}));
			}
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x00098070 File Offset: 0x00096270
		internal static void CheckUriParameter(string ns, string paramName)
		{
			Uri uri;
			if (!Uri.TryCreate(ns, UriKind.RelativeOrAbsolute, out uri))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(paramName, SR.GetString("SFXUnvalidNamespaceParam", new object[]
				{
					ns
				}));
			}
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x000980A8 File Offset: 0x000962A8
		internal static string XmlName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return name;
			}
			if (NamingHelper.IsAsciiLocalName(name))
			{
				return name;
			}
			if (NamingHelper.IsValidNCName(name))
			{
				return name;
			}
			return XmlConvert.EncodeLocalName(name);
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x000980CE File Offset: 0x000962CE
		internal static string CodeName(string name)
		{
			return XmlConvert.DecodeName(name);
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x000980D6 File Offset: 0x000962D6
		private static bool IsAlpha(char ch)
		{
			return (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z');
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x000980F3 File Offset: 0x000962F3
		private static bool IsDigit(char ch)
		{
			return ch >= '0' && ch <= '9';
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x00098104 File Offset: 0x00096304
		private static bool IsAsciiLocalName(string localName)
		{
			if (!NamingHelper.IsAlpha(localName[0]))
			{
				return false;
			}
			for (int i = 1; i < localName.Length; i++)
			{
				char ch = localName[i];
				if (!NamingHelper.IsAlpha(ch) && !NamingHelper.IsDigit(ch))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x00098150 File Offset: 0x00096350
		internal static bool IsValidNCName(string name)
		{
			bool result;
			try
			{
				XmlConvert.VerifyNCName(name);
				result = true;
			}
			catch (XmlException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x04002247 RID: 8775
		internal const string DefaultNamespace = "http://tempuri.org/";

		// Token: 0x04002248 RID: 8776
		internal const string DefaultServiceName = "service";

		// Token: 0x04002249 RID: 8777
		internal const string MSNamespace = "http://schemas.microsoft.com/2005/07/ServiceModel";

		// Token: 0x02000BDC RID: 3036
		// (Invoke) Token: 0x06007544 RID: 30020
		internal delegate bool DoesNameExist(string name, object nameCollection);
	}
}
