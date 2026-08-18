using System;
using System.Web;
using System.Web.SessionState;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.ClockWorkServer.Contracts.DTO.ConfidentialityAgreement;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.UI.ClientManager.Web.Core.ConfidentialityAgreement;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.ConfidentialityAgreement
{
	// Token: 0x0200001E RID: 30
	public class StudentConfidentialityAgreementWebClientManager : IStudentConfidentialityAgreementWebClientManager
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00006F9E File Offset: 0x0000519E
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00006FA6 File Offset: 0x000051A6
		private eClockWorkModules Module { get; set; }

		// Token: 0x060000AF RID: 175 RVA: 0x00006FAF File Offset: 0x000051AF
		public StudentConfidentialityAgreementWebClientManager(eClockWorkModules module)
		{
			this.Module = module;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00006FC4 File Offset: 0x000051C4
		public void RecordSignedConfidentialityAgreement(int personId)
		{
			SignedConfidentialityAgreementReq signedConfidentialityAgreementReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SignedConfidentialityAgreementReq>();
			signedConfidentialityAgreementReq.PersonId = personId;
			signedConfidentialityAgreementReq.Module = this.Module;
			ClientServiceFactory.GetClientInstance<IStudentConfidentialityAgreement>().RecordSignedConfidentialityAgreement(signedConfidentialityAgreementReq);
			HttpSessionState session = HttpContext.Current.Session;
			string name = string.Format("{0}_IsConfidentialityAgreementSigningRequired", this.Module);
			session[name] = false;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00007030 File Offset: 0x00005230
		public StudentConfidentialityAgreementDTO LastSignedStudentConfidentialityAgreement(int personId)
		{
			LastStudentConfidentialityAgreementReq lastStudentConfidentialityAgreementReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LastStudentConfidentialityAgreementReq>();
			lastStudentConfidentialityAgreementReq.PersonId = personId;
			lastStudentConfidentialityAgreementReq.Module = this.Module;
			return ClientServiceFactory.GetClientInstance<IStudentConfidentialityAgreement>().LastSignedStudentConfidentialityAgreement(lastStudentConfidentialityAgreementReq).ConfidentialityAgreement;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00007074 File Offset: 0x00005274
		public bool IsConfidentialityAgreementSigningRequired(int pid)
		{
			HttpSessionState session = HttpContext.Current.Session;
			string name = string.Format("{0}_IsConfidentialityAgreementSigningRequired", this.Module);
			object obj = session[name];
			bool flag = obj == null || (obj is bool && (bool)obj);
			bool result;
			if (flag)
			{
				IsConfidentialityAgreementSigningRequiredReq isConfidentialityAgreementSigningRequiredReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<IsConfidentialityAgreementSigningRequiredReq>();
				isConfidentialityAgreementSigningRequiredReq.PersonId = pid;
				isConfidentialityAgreementSigningRequiredReq.Module = this.Module;
				result = ClientServiceFactory.GetClientInstance<IStudentConfidentialityAgreement>().IsConfidentialityAgreementSigningRequired(isConfidentialityAgreementSigningRequiredReq).IsSigningRequired;
			}
			else
			{
				result = (bool)obj;
			}
			return result;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000710C File Offset: 0x0000530C
		public string GetStudentConfidentialityAgreementText(int pid)
		{
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			string key = string.Format("{0}_ConfidentialityAgreementText", this.Module.ToString());
			object obj = clientCache[key];
			bool flag = obj == null || string.IsNullOrEmpty((string)obj);
			string result;
			if (flag)
			{
				GetStudentConfidentialityAgreementTextReq getStudentConfidentialityAgreementTextReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetStudentConfidentialityAgreementTextReq>();
				getStudentConfidentialityAgreementTextReq.PersonId = pid;
				getStudentConfidentialityAgreementTextReq.Module = this.Module;
				GetStudentConfidentialityAgreementTextResp studentConfidentialityAgreementText = ClientServiceFactory.GetClientInstance<IStudentConfidentialityAgreement>().GetStudentConfidentialityAgreementText(getStudentConfidentialityAgreementTextReq);
				clientCache[key] = studentConfidentialityAgreementText.ConfidentialityAgreementText;
				result = studentConfidentialityAgreementText.ConfidentialityAgreementText;
			}
			else
			{
				result = (string)obj;
			}
			return result;
		}
	}
}
