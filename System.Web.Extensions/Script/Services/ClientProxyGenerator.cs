using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;

namespace System.Web.Script.Services
{
	// Token: 0x020000FD RID: 253
	internal abstract class ClientProxyGenerator
	{
		// Token: 0x06000D6A RID: 3434 RVA: 0x0002D87C File Offset: 0x0002BA7C
		internal string GetClientProxyScript(WebServiceData webServiceData)
		{
			if (webServiceData.MethodDatas.Count == 0)
			{
				return null;
			}
			this._builder = new StringBuilder();
			if (this._debugMode)
			{
				this._docCommentCache = new Dictionary<string, string>();
			}
			this.GenerateConstructor(webServiceData);
			this.GeneratePrototype(webServiceData);
			this.GenerateRegisterClass(webServiceData);
			this.GenerateStaticInstance(webServiceData);
			this.GenerateStaticMethods(webServiceData);
			this.GenerateClientTypeProxies(webServiceData);
			this.GenerateEnumTypeProxies(webServiceData.EnumTypes);
			return this._builder.ToString();
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0002D8F8 File Offset: 0x0002BAF8
		protected void GenerateRegisterClass(WebServiceData webServiceData)
		{
			string proxyTypeName = this.GetProxyTypeName(webServiceData);
			this._builder.Append(proxyTypeName).Append(".registerClass('").Append(proxyTypeName).Append("',Sys.Net.WebServiceProxy);\r\n");
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0002D934 File Offset: 0x0002BB34
		protected virtual void GenerateConstructor(WebServiceData webServiceData)
		{
			this.GenerateTypeDeclaration(webServiceData, false);
			this._builder.Append("function() {\r\n");
			this._builder.Append(this.GetProxyTypeName(webServiceData)).Append(".initializeBase(this);\r\n");
			this.GenerateFields();
			this._builder.Append("}\r\n");
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0002D990 File Offset: 0x0002BB90
		protected virtual void GeneratePrototype(WebServiceData webServiceData)
		{
			this.GenerateTypeDeclaration(webServiceData, true);
			this._builder.Append("{\r\n");
			this._builder.Append("_get_path:function() {\r\n var p = this.get_path();\r\n if (p) return p;\r\n else return ");
			this._builder.Append(this.GetProxyTypeName(webServiceData)).Append("._staticInstance.get_path();},\r\n");
			bool flag = true;
			foreach (WebServiceMethodData methodData in webServiceData.MethodDatas)
			{
				if (!flag)
				{
					this._builder.Append(",\r\n");
				}
				flag = false;
				this.GenerateWebMethodProxy(methodData);
			}
			this._builder.Append("}\r\n");
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0002DA50 File Offset: 0x0002BC50
		protected virtual void GenerateTypeDeclaration(WebServiceData webServiceData, bool genClass)
		{
			this.AppendClientTypeDeclaration(webServiceData.TypeData.TypeNamespace, webServiceData.TypeData.TypeName, genClass, true);
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0002DA70 File Offset: 0x0002BC70
		protected void GenerateFields()
		{
			this._builder.Append("this._timeout = 0;\r\n");
			this._builder.Append("this._userContext = null;\r\n");
			this._builder.Append("this._succeeded = null;\r\n");
			this._builder.Append("this._failed = null;\r\n");
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x000032F4 File Offset: 0x000014F4
		protected virtual void GenerateMethods()
		{
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0002DAC4 File Offset: 0x0002BCC4
		protected void GenerateStaticMethods(WebServiceData webServiceData)
		{
			string proxyTypeName = this.GetProxyTypeName(webServiceData);
			foreach (WebServiceMethodData webServiceMethodData in webServiceData.MethodDatas)
			{
				string methodName = webServiceMethodData.MethodName;
				this._builder.Append(proxyTypeName).Append('.').Append(methodName).Append("= function(");
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = true;
				foreach (WebServiceParameterData webServiceParameterData in webServiceMethodData.ParameterDatas)
				{
					if (!flag)
					{
						stringBuilder.Append(',');
					}
					else
					{
						flag = false;
					}
					stringBuilder.Append(webServiceParameterData.ParameterName);
				}
				if (!flag)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append("onSuccess,onFailed,userContext");
				this._builder.Append(stringBuilder.ToString()).Append(") {");
				if (this._debugMode)
				{
					this._builder.Append("\r\n");
					this._builder.Append(this._docCommentCache[methodName]);
				}
				this._builder.Append(proxyTypeName).Append("._staticInstance.").Append(methodName).Append('(');
				this._builder.Append(stringBuilder.ToString()).Append("); }\r\n");
			}
		}

		// Token: 0x06000D72 RID: 3442
		protected abstract string GetProxyPath();

		// Token: 0x06000D73 RID: 3443 RVA: 0x0001B314 File Offset: 0x00019514
		protected virtual string GetJsonpCallbackParameterName()
		{
			return null;
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0001359B File Offset: 0x0001179B
		protected virtual bool GetSupportsJsonp()
		{
			return false;
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0002DC6C File Offset: 0x0002BE6C
		protected void GenerateStaticInstance(WebServiceData data)
		{
			string proxyTypeName = this.GetProxyTypeName(data);
			this._builder.Append(proxyTypeName).Append("._staticInstance = new ").Append(proxyTypeName).Append("();\r\n");
			if (this._debugMode)
			{
				this._builder.Append(proxyTypeName).Append(".set_path = function(value) {\r\n");
				this._builder.Append(proxyTypeName).Append("._staticInstance.set_path(value); }\r\n");
				this._builder.Append(proxyTypeName).Append(".get_path = function() { \r\n/// <value type=\"String\" mayBeNull=\"true\">The service url.</value>\r\nreturn ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.get_path();}\r\n");
				this._builder.Append(proxyTypeName).Append(".set_timeout = function(value) {\r\n");
				this._builder.Append(proxyTypeName).Append("._staticInstance.set_timeout(value); }\r\n");
				this._builder.Append(proxyTypeName).Append(".get_timeout = function() { \r\n/// <value type=\"Number\">The service timeout.</value>\r\nreturn ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.get_timeout(); }\r\n");
				this._builder.Append(proxyTypeName).Append(".set_defaultUserContext = function(value) { \r\n");
				this._builder.Append(proxyTypeName).Append("._staticInstance.set_defaultUserContext(value); }\r\n");
				this._builder.Append(proxyTypeName).Append(".get_defaultUserContext = function() { \r\n/// <value mayBeNull=\"true\">The service default user context.</value>\r\nreturn ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.get_defaultUserContext(); }\r\n");
				this._builder.Append(proxyTypeName).Append(".set_defaultSucceededCallback = function(value) { \r\n ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.set_defaultSucceededCallback(value); }\r\n");
				this._builder.Append(proxyTypeName).Append(".get_defaultSucceededCallback = function() { \r\n/// <value type=\"Function\" mayBeNull=\"true\">The service default succeeded callback.</value>\r\nreturn ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.get_defaultSucceededCallback(); }\r\n");
				this._builder.Append(proxyTypeName).Append(".set_defaultFailedCallback = function(value) { \r\n");
				this._builder.Append(proxyTypeName).Append("._staticInstance.set_defaultFailedCallback(value); }\r\n");
				this._builder.Append(proxyTypeName).Append(".get_defaultFailedCallback = function() { \r\n/// <value type=\"Function\" mayBeNull=\"true\">The service default failed callback.</value>\r\nreturn ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.get_defaultFailedCallback(); }\r\n");
				this._builder.Append(proxyTypeName).Append(".set_enableJsonp = function(value) { ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.set_enableJsonp(value); }\r\n");
				this._builder.Append(proxyTypeName).Append(".get_enableJsonp = function() { \r\n/// <value type=\"Boolean\">Specifies whether the service supports JSONP for cross domain calling.</value>\r\nreturn ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.get_enableJsonp(); }\r\n");
				this._builder.Append(proxyTypeName).Append(".set_jsonpCallbackParameter = function(value) { ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.set_jsonpCallbackParameter(value); }\r\n");
				this._builder.Append(proxyTypeName).Append(".get_jsonpCallbackParameter = function() { \r\n/// <value type=\"String\">Specifies the parameter name that contains the callback function name for a JSONP request.</value>\r\nreturn ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.get_jsonpCallbackParameter(); }\r\n");
			}
			else
			{
				this._builder.Append(proxyTypeName).Append(".set_path = function(value) { ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.set_path(value); }\r\n");
				this._builder.Append(proxyTypeName).Append(".get_path = function() { return ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.get_path(); }\r\n");
				this._builder.Append(proxyTypeName).Append(".set_timeout = function(value) { ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.set_timeout(value); }\r\n");
				this._builder.Append(proxyTypeName).Append(".get_timeout = function() { return ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.get_timeout(); }\r\n");
				this._builder.Append(proxyTypeName).Append(".set_defaultUserContext = function(value) { ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.set_defaultUserContext(value); }\r\n");
				this._builder.Append(proxyTypeName).Append(".get_defaultUserContext = function() { return ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.get_defaultUserContext(); }\r\n");
				this._builder.Append(proxyTypeName).Append(".set_defaultSucceededCallback = function(value) { ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.set_defaultSucceededCallback(value); }\r\n");
				this._builder.Append(proxyTypeName).Append(".get_defaultSucceededCallback = function() { return ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.get_defaultSucceededCallback(); }\r\n");
				this._builder.Append(proxyTypeName).Append(".set_defaultFailedCallback = function(value) { ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.set_defaultFailedCallback(value); }\r\n");
				this._builder.Append(proxyTypeName).Append(".get_defaultFailedCallback = function() { return ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.get_defaultFailedCallback(); }\r\n");
				this._builder.Append(proxyTypeName).Append(".set_enableJsonp = function(value) { ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.set_enableJsonp(value); }\r\n");
				this._builder.Append(proxyTypeName).Append(".get_enableJsonp = function() { return ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.get_enableJsonp(); }\r\n");
				this._builder.Append(proxyTypeName).Append(".set_jsonpCallbackParameter = function(value) { ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.set_jsonpCallbackParameter(value); }\r\n");
				this._builder.Append(proxyTypeName).Append(".get_jsonpCallbackParameter = function() { return ");
				this._builder.Append(proxyTypeName).Append("._staticInstance.get_jsonpCallbackParameter(); }\r\n");
			}
			string text = this.GetProxyPath();
			if (!string.IsNullOrEmpty(text) && (text.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || text.StartsWith("https://", StringComparison.OrdinalIgnoreCase)))
			{
				int startIndex = text.IndexOf("://", StringComparison.OrdinalIgnoreCase) + "://".Length;
				int num = text.IndexOf("/", startIndex, StringComparison.OrdinalIgnoreCase);
				if (num != -1)
				{
					text = text.Substring(0, num) + HttpUtility.UrlPathEncode(text.Substring(num));
				}
			}
			else
			{
				text = HttpUtility.UrlPathEncode(text);
			}
			this._builder.Append(proxyTypeName).Append(".set_path(\"").Append(text).Append("\");\r\n");
			if (this.GetSupportsJsonp())
			{
				this._builder.Append(proxyTypeName).Append(".set_enableJsonp(true);\r\n");
				string jsonpCallbackParameterName = this.GetJsonpCallbackParameterName();
				if (!string.IsNullOrEmpty(jsonpCallbackParameterName) && !jsonpCallbackParameterName.Equals("callback", StringComparison.Ordinal))
				{
					this._builder.Append(proxyTypeName).Append(".set_jsonpCallbackParameter(").Append(JavaScriptSerializer.SerializeInternal(jsonpCallbackParameterName)).Append(");\r\n");
				}
			}
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0002E2CC File Offset: 0x0002C4CC
		private void BuildArgsDictionary(WebServiceMethodData methodData, StringBuilder args, StringBuilder argsDict, StringBuilder docComments)
		{
			argsDict.Append('{');
			foreach (WebServiceParameterData webServiceParameterData in methodData.ParameterDatas)
			{
				string parameterName = webServiceParameterData.ParameterName;
				if (docComments != null)
				{
					docComments.Append("/// <param name=\"").Append(parameterName).Append("\"");
					Type type = ServicesUtilities.UnwrapNullableType(webServiceParameterData.ParameterType);
					string clientTypeNamespace = this.GetClientTypeNamespace(ServicesUtilities.GetClientTypeFromServerType(methodData.Owner, type));
					if (!string.IsNullOrEmpty(clientTypeNamespace))
					{
						docComments.Append(" type=\"").Append(clientTypeNamespace).Append("\"");
					}
					docComments.Append(">").Append(type.FullName).Append("</param>\r\n");
				}
				if (args.Length > 0)
				{
					args.Append(',');
					argsDict.Append(',');
				}
				args.Append(parameterName);
				argsDict.Append(parameterName).Append(':').Append(parameterName);
			}
			if (docComments != null)
			{
				docComments.Append(ClientProxyGenerator.DebugXmlComments);
			}
			argsDict.Append("}");
			if (args.Length > 0)
			{
				args.Append(',');
			}
			args.Append("succeededCallback, failedCallback, userContext");
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0002E428 File Offset: 0x0002C628
		private void GenerateWebMethodProxy(WebServiceMethodData methodData)
		{
			string methodName = methodData.MethodName;
			string proxyTypeName = this.GetProxyTypeName(methodData.Owner);
			string value = methodData.UseGet ? "true" : "false";
			this._builder.Append(methodName).Append(':');
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = null;
			string value2 = null;
			if (this._debugMode)
			{
				stringBuilder3 = new StringBuilder();
			}
			this.BuildArgsDictionary(methodData, stringBuilder, stringBuilder2, stringBuilder3);
			if (this._debugMode)
			{
				value2 = stringBuilder3.ToString();
				this._docCommentCache[methodName] = value2;
			}
			this._builder.Append("function(").Append(stringBuilder.ToString()).Append(") {\r\n");
			if (this._debugMode)
			{
				this._builder.Append(value2);
			}
			this._builder.Append("return this._invoke(this._get_path(), ");
			this._builder.Append("'").Append(methodName).Append("',");
			this._builder.Append(value).Append(',');
			this._builder.Append(stringBuilder2.ToString()).Append(",succeededCallback,failedCallback,userContext); }");
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0002E560 File Offset: 0x0002C760
		private void GenerateClientTypeProxies(WebServiceData data)
		{
			bool flag = true;
			foreach (WebServiceTypeData webServiceTypeData in data.ClientTypes)
			{
				if (flag)
				{
					this._builder.Append("var gtc = Sys.Net.WebServiceProxy._generateTypedConstructor;\r\n");
					flag = false;
				}
				string typeStringRepresentation = data.GetTypeStringRepresentation(webServiceTypeData);
				string clientTypeNamespace = this.GetClientTypeNamespace(webServiceTypeData.TypeName);
				string clientTypeName = ServicesUtilities.GetClientTypeName(clientTypeNamespace);
				string clientTypeNamespace2 = this.GetClientTypeNamespace(webServiceTypeData.TypeNamespace);
				this.EnsureNamespace(webServiceTypeData.TypeNamespace);
				this.EnsureObjectGraph(clientTypeNamespace2, clientTypeName);
				this._builder.Append("if (typeof(").Append(clientTypeName).Append(") === 'undefined') {\r\n");
				this.AppendClientTypeDeclaration(clientTypeNamespace2, clientTypeNamespace, false, false);
				this._builder.Append("gtc(\"");
				this._builder.Append(typeStringRepresentation);
				this._builder.Append("\");\r\n");
				this._builder.Append(clientTypeName).Append(".registerClass('").Append(clientTypeName).Append("');\r\n}\r\n");
			}
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0002E690 File Offset: 0x0002C890
		private void GenerateEnumTypeProxies(IEnumerable<WebServiceEnumData> enumTypes)
		{
			foreach (WebServiceEnumData webServiceEnumData in enumTypes)
			{
				this.EnsureNamespace(webServiceEnumData.TypeNamespace);
				string clientTypeNamespace = this.GetClientTypeNamespace(webServiceEnumData.TypeName);
				string clientTypeName = ServicesUtilities.GetClientTypeName(clientTypeNamespace);
				string[] names = webServiceEnumData.Names;
				long[] values = webServiceEnumData.Values;
				this.EnsureObjectGraph(this.GetClientTypeNamespace(webServiceEnumData.TypeNamespace), clientTypeName);
				this._builder.Append("if (typeof(").Append(clientTypeName).Append(") === 'undefined') {\r\n");
				if (clientTypeName.IndexOf('.') == -1)
				{
					this._builder.Append("var ");
				}
				this._builder.Append(clientTypeName).Append(" = function() { throw Error.invalidOperation(); }\r\n");
				this._builder.Append(clientTypeName).Append(".prototype = {");
				for (int i = 0; i < names.Length; i++)
				{
					if (i > 0)
					{
						this._builder.Append(',');
					}
					this._builder.Append(names[i]);
					this._builder.Append(": ");
					if (webServiceEnumData.IsULong)
					{
						this._builder.Append((ulong)values[i]);
					}
					else
					{
						this._builder.Append(values[i]);
					}
				}
				this._builder.Append("}\r\n");
				this._builder.Append(clientTypeName).Append(".registerEnum('").Append(clientTypeName).Append('\'');
				this._builder.Append(", true);\r\n}\r\n");
			}
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0002B1E4 File Offset: 0x000293E4
		protected virtual string GetClientTypeNamespace(string ns)
		{
			return ns;
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0002E84C File Offset: 0x0002CA4C
		private void AppendClientTypeDeclaration(string ns, string typeName, bool genClass, bool ensureNS)
		{
			string clientTypeNamespace = this.GetClientTypeNamespace(ServicesUtilities.GetClientTypeName(typeName));
			if (!string.IsNullOrEmpty(ns))
			{
				if (ensureNS)
				{
					this.EnsureNamespace(ns);
				}
			}
			else if (!genClass && !clientTypeNamespace.Contains("."))
			{
				this._builder.Append("var ");
			}
			this._builder.Append(clientTypeNamespace);
			if (genClass)
			{
				this._builder.Append(".prototype");
			}
			this._builder.Append('=');
			this._ensuredObjectParts[clientTypeNamespace] = null;
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0002E8D8 File Offset: 0x0002CAD8
		protected virtual string GetProxyTypeName(WebServiceData data)
		{
			return ServicesUtilities.GetClientTypeName(data.TypeData.TypeName);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0002E8EC File Offset: 0x0002CAEC
		private void EnsureNamespace(string ns)
		{
			ns = this.GetClientTypeNamespace(ns);
			if (string.IsNullOrEmpty(ns))
			{
				return;
			}
			if (!this._registeredNamespaces.Contains(ns))
			{
				this._builder.Append("Type.registerNamespace('").Append(ns).Append("');\r\n");
				this._registeredNamespaces[ns] = null;
			}
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0002E948 File Offset: 0x0002CB48
		private void EnsureObjectGraph(string namespacePart, string typeName)
		{
			int startIndex = 0;
			bool flag = true;
			if (!string.IsNullOrEmpty(namespacePart))
			{
				int num = typeName.IndexOf(namespacePart + ".", StringComparison.Ordinal);
				if (num > -1)
				{
					startIndex = num + namespacePart.Length + 1;
					flag = false;
				}
			}
			for (int i = typeName.IndexOf('.', startIndex); i > -1; i = typeName.IndexOf('.', i + 1))
			{
				string text = typeName.Substring(0, i);
				if (!this._registeredNamespaces.Contains(text) && !this._ensuredObjectParts.Contains(text))
				{
					this._ensuredObjectParts[text] = null;
					this._builder.Append("if (typeof(" + text + ") === \"undefined\") {\r\n   ");
					if (flag)
					{
						this._builder.Append("var ");
						flag = false;
					}
					this._builder.Append(text + " = {};\r\n}\r\n");
				}
			}
		}

		// Token: 0x040003C6 RID: 966
		private static string DebugXmlComments = "/// <param name=\"succeededCallback\" type=\"Function\" optional=\"true\" mayBeNull=\"true\"></param>\r\n/// <param name=\"failedCallback\" type=\"Function\" optional=\"true\" mayBeNull=\"true\"></param>\r\n/// <param name=\"userContext\" optional=\"true\" mayBeNull=\"true\"></param>\r\n";

		// Token: 0x040003C7 RID: 967
		private Hashtable _registeredNamespaces = new Hashtable();

		// Token: 0x040003C8 RID: 968
		private Hashtable _ensuredObjectParts = new Hashtable();

		// Token: 0x040003C9 RID: 969
		protected StringBuilder _builder;

		// Token: 0x040003CA RID: 970
		protected bool _debugMode;

		// Token: 0x040003CB RID: 971
		private Dictionary<string, string> _docCommentCache;
	}
}
