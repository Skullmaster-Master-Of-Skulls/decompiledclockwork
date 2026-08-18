using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Web.Services.Description;
using System.Xml.Serialization;
using ClockWorkLogger;
using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Security.Tokens;

namespace TechnoPro.Common.WebServices.Helpers
{
	// Token: 0x02000002 RID: 2
	public class WsProxy
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static object CallWebService(string webServiceAsmxUrl, string serviceName, string methodName, string protocolName_nullToAutoSet, object[] args, string username, string password)
		{
			CWLogger.Logger.Trace("WsProxy:CallWebService:Starting:Url={0}:serviceName={1}:methodName={2}", webServiceAsmxUrl, serviceName, methodName);
			object result;
			try
			{
				ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true));
				ServiceDescription serviceDescription = ServiceDescription.Read(new WebClient().OpenRead(webServiceAsmxUrl));
				ServiceDescriptionImporter serviceDescriptionImporter = new ServiceDescriptionImporter();
				serviceDescriptionImporter.ProtocolName = ((protocolName_nullToAutoSet == null || protocolName_nullToAutoSet.Trim().Length < 1) ? "Soap12" : protocolName_nullToAutoSet);
				serviceDescriptionImporter.AddServiceDescription(serviceDescription, null, null);
				serviceDescriptionImporter.Style = ServiceDescriptionImportStyle.Client;
				serviceDescriptionImporter.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties;
				CodeNamespace codeNamespace = new CodeNamespace();
				CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
				codeCompileUnit.Namespaces.Add(codeNamespace);
				if (serviceDescriptionImporter.Import(codeNamespace, codeCompileUnit) == (ServiceDescriptionImportWarnings)0)
				{
					CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("CSharp");
					CompilerParameters options = new CompilerParameters(new string[]
					{
						"System.dll",
						"System.Web.Services.dll",
						"System.Web.dll",
						"System.Xml.dll",
						"System.Data.dll"
					});
					CompilerResults compilerResults = codeDomProvider.CompileAssemblyFromDom(options, new CodeCompileUnit[]
					{
						codeCompileUnit
					});
					if (compilerResults.Errors.Count > 0)
					{
						StringBuilder stringBuilder = new StringBuilder();
						foreach (object obj in compilerResults.Errors)
						{
							CompilerError compilerError = (CompilerError)obj;
							stringBuilder.Append("Error=" + compilerError.ErrorText);
							stringBuilder.AppendLine();
						}
						throw new Exception("Compile Error Occured calling webservice. Errors=" + stringBuilder.ToString());
					}
					object obj2 = compilerResults.CompiledAssembly.CreateInstance(serviceName);
					if (!string.IsNullOrEmpty(username))
					{
						try
						{
							Type type = obj2.GetType();
							UsernameToken token = new UsernameToken(username, password, PasswordOption.SendPlainText);
							SoapContext soapContext = (SoapContext)type.GetProperty("RequestSoapContext", BindingFlags.GetProperty).GetValue(obj2, null);
							soapContext.Security.Timestamp.TtlInSeconds = 60L;
							soapContext.Security.Tokens.Add(token);
						}
						catch (Exception ex)
						{
							throw new Exception("SetCredentialsError: " + ex.ToString());
						}
					}
					result = obj2.GetType().GetMethod(methodName).Invoke(obj2, args);
				}
				else
				{
					result = null;
				}
			}
			catch (Exception ex2)
			{
				CWLogger.Logger.Error("Common.WebServices:WsProxy:CallWebService:err={0}", ex2.ToString());
				result = null;
			}
			return result;
		}
	}
}
