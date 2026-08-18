using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Web.Services.Description;
using System.Xml.Serialization;
using ClockWorkLogger;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x0200002B RID: 43
	public class WsProxy
	{
		// Token: 0x060002CA RID: 714 RVA: 0x0002A7E8 File Offset: 0x000289E8
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static object CallWebService(string webServiceAsmxUrl, string serviceName, string methodName, string protocolName_nullToAutoSet, object[] args)
		{
			int num = 0;
			object result;
			try
			{
				num = 1;
				ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true));
				WebClient webClient = new WebClient();
				num = 5;
				Stream stream = webClient.OpenRead(webServiceAsmxUrl + "?wsdl");
				num = 10;
				ServiceDescription serviceDescription = ServiceDescription.Read(stream);
				num = 20;
				ServiceDescriptionImporter serviceDescriptionImporter = new ServiceDescriptionImporter();
				serviceDescriptionImporter.ProtocolName = ((protocolName_nullToAutoSet == null || protocolName_nullToAutoSet.Trim().Length < 1) ? "Soap12" : protocolName_nullToAutoSet);
				serviceDescriptionImporter.AddServiceDescription(serviceDescription, null, null);
				num = 30;
				serviceDescriptionImporter.Style = ServiceDescriptionImportStyle.Client;
				serviceDescriptionImporter.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties;
				num = 40;
				CodeNamespace codeNamespace = new CodeNamespace();
				CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
				codeCompileUnit.Namespaces.Add(codeNamespace);
				num = 50;
				ServiceDescriptionImportWarnings serviceDescriptionImportWarnings = serviceDescriptionImporter.Import(codeNamespace, codeCompileUnit);
				num = 60;
				bool flag = serviceDescriptionImportWarnings == (ServiceDescriptionImportWarnings)0;
				if (flag)
				{
					CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("CSharp");
					num = 70;
					string[] assemblyNames = new string[]
					{
						"System.dll",
						"System.Web.Services.dll",
						"System.Web.dll",
						"System.Xml.dll",
						"System.Data.dll"
					};
					CompilerParameters options = new CompilerParameters(assemblyNames);
					CompilerResults compilerResults = codeDomProvider.CompileAssemblyFromDom(options, new CodeCompileUnit[]
					{
						codeCompileUnit
					});
					num = 80;
					bool flag2 = compilerResults.Errors.Count > 0;
					if (flag2)
					{
						foreach (object obj in compilerResults.Errors)
						{
							CompilerError compilerError = (CompilerError)obj;
						}
						throw new Exception("Compile Error Occured calling webservice. Check Debug ouput window.");
					}
					num = 90;
					object obj2 = compilerResults.CompiledAssembly.CreateInstance(serviceName);
					num = 100;
					MethodInfo method = obj2.GetType().GetMethod(methodName);
					num = 110;
					result = method.Invoke(obj2, args);
				}
				else
				{
					num = 9999;
					result = null;
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.DAO.Reports.Impl.Legacy.Entity.WsProxy:err={0}: {1}", ex.ToString(), num.ToString());
				result = null;
			}
			return result;
		}
	}
}
