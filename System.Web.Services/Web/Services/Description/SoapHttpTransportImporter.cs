using System;
using System.CodeDom;
using System.Web.Services.Protocols;

namespace System.Web.Services.Description
{
	// Token: 0x02000120 RID: 288
	internal class SoapHttpTransportImporter : SoapTransportImporter
	{
		// Token: 0x060008C8 RID: 2248 RVA: 0x00040FE3 File Offset: 0x0003FFE3
		public override bool IsSupportedTransport(string transport)
		{
			return transport == "http://schemas.xmlsoap.org/soap/http";
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00040FF0 File Offset: 0x0003FFF0
		public override void ImportClass()
		{
			SoapAddressBinding soapAddressBinding = (base.ImportContext.Port == null) ? null : ((SoapAddressBinding)base.ImportContext.Port.Extensions.Find(typeof(SoapAddressBinding)));
			if (base.ImportContext.Style == ServiceDescriptionImportStyle.Client)
			{
				base.ImportContext.CodeTypeDeclaration.BaseTypes.Add(typeof(SoapHttpClientProtocol).FullName);
				CodeConstructor codeConstructor = WebCodeGenerator.AddConstructor(base.ImportContext.CodeTypeDeclaration, new string[0], new string[0], null, CodeFlags.IsPublic);
				codeConstructor.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), true));
				bool flag = true;
				if (base.ImportContext is Soap12ProtocolImporter)
				{
					flag = false;
					CodeTypeReferenceExpression targetObject = new CodeTypeReferenceExpression(typeof(SoapProtocolVersion));
					CodeFieldReferenceExpression right = new CodeFieldReferenceExpression(targetObject, Enum.Format(typeof(SoapProtocolVersion), SoapProtocolVersion.Soap12, "G"));
					CodePropertyReferenceExpression left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "SoapVersion");
					CodeAssignStatement value = new CodeAssignStatement(left, right);
					codeConstructor.Statements.Add(value);
				}
				ServiceDescription serviceDescription = base.ImportContext.Binding.ServiceDescription;
				string url = (soapAddressBinding != null) ? soapAddressBinding.Location : null;
				string appSettingUrlKey = serviceDescription.AppSettingUrlKey;
				string appSettingBaseUrl = serviceDescription.AppSettingBaseUrl;
				ProtocolImporterUtil.GenerateConstructorStatements(codeConstructor, url, appSettingUrlKey, appSettingBaseUrl, flag && !base.ImportContext.IsEncodedBinding);
				return;
			}
			if (base.ImportContext.Style == ServiceDescriptionImportStyle.Server)
			{
				base.ImportContext.CodeTypeDeclaration.BaseTypes.Add(typeof(WebService).FullName);
			}
		}
	}
}
