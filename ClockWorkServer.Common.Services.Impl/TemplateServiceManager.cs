using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Templates;
using TechnoPro.Common.Core.Mappers.Files;
using TechnoPro.Common.Core.Mappers.Templates;
using TechnoPro.Common.Core.Mappers.TPMailMan;
using TechnoPro.Common.Core.Templates;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.ICore.Templates;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Templates;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000096 RID: 150
	public class TemplateServiceManager : ITemplate, IService
	{
		// Token: 0x06000559 RID: 1369 RVA: 0x00018E04 File Offset: 0x00017004
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00018E18 File Offset: 0x00017018
		public LoadTemplateResp LoadTemplate(LoadTemplateReq Request)
		{
			ITemplateManager templateManager = new TemplateManager(Request.GetOperationContext());
			Template template = templateManager.LoadTemplate(Request.TemplateId, Request.LoadDocumentOrEmail);
			return new LoadTemplateResp
			{
				Template = ((template == null) ? null : template.ToDTO())
			};
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00018E64 File Offset: 0x00017064
		public CreateNewTemplateResp CreateNewTemplate(CreateNewTemplateReq Request)
		{
			ITemplateManager templateManager = new TemplateManager(Request.GetOperationContext());
			int templateId = templateManager.CreateNewTemplate(Request.Template.ToDomainObject());
			return new CreateNewTemplateResp
			{
				TemplateId = templateId
			};
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00018EA4 File Offset: 0x000170A4
		public void ReplaceTemplateFile(ReplaceTemplateFileReq Request)
		{
			ITemplateManager templateManager = new TemplateManager(Request.GetOperationContext());
			templateManager.ReplaceTemplateFile(Request.TemplateId, (Request.File == null) ? null : Request.File.ToDomainObject());
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00018EE4 File Offset: 0x000170E4
		public void ReplaceTemplateEmail(ReplaceTemplateEmailReq Request)
		{
			ITemplateManager templateManager = new TemplateManager(Request.GetOperationContext());
			templateManager.ReplaceTemplateEmail(Request.TemplateId, (Request.EmailTemplate == null) ? null : Request.EmailTemplate.ToDomainObject());
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00018F24 File Offset: 0x00017124
		public void ReplaceTemplateEmailBehindDocument(ReplaceTemplateEmailBehindDocumentReq Request)
		{
			ITemplateManager templateManager = new TemplateManager(Request.GetOperationContext());
			templateManager.ReplaceTemplateEmailBehindDocument(Request.TemplateId, (Request.EmailTemplate == null) ? null : Request.EmailTemplate.ToDomainObject());
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00018F64 File Offset: 0x00017164
		public void DeleteTemplate(DeleteTemplateReq Request)
		{
			ITemplateManager templateManager = new TemplateManager(Request.GetOperationContext());
			templateManager.DeleteTemplate(Request.TemplateId);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00018F8C File Offset: 0x0001718C
		public LoadTemplatesResp LoadTemplates(LoadTemplatesReq Request)
		{
			ITemplateManager templateManager = new TemplateManager(Request.GetOperationContext());
			TemplateCollection templateCollection = templateManager.LoadTemplates(Request.TemplateGroupId, Request.LoadDocumentsOrEmails);
			return new LoadTemplatesResp
			{
				TemplateCollection = ((templateCollection == null) ? null : templateCollection.ToDTO())
			};
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00018FD8 File Offset: 0x000171D8
		public LoadAllTemplatesResp LoadAllTemplates(LoadAllTemplatesReq Request)
		{
			ITemplateManager templateManager = new TemplateManager(Request.GetOperationContext());
			TemplateCollection templateCollection = templateManager.LoadAllTemplates(Request.LoadDocumentsOrEmails);
			return new LoadAllTemplatesResp
			{
				TemplateCollection = ((templateCollection == null) ? null : templateCollection.ToDTO())
			};
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001901C File Offset: 0x0001721C
		public LoadAllTemplatesAsForestResp LoadAllTemplatesAsForest(LoadAllTemplatesAsForestReq Request)
		{
			ITemplateManager templateManager = new TemplateManager(Request.GetOperationContext());
			Forest<TemplateOrGroup> forest = templateManager.LoadAllTemplatesAsForest(Request.LoadDocumentsOrEmails);
			return new LoadAllTemplatesAsForestResp
			{
				Forest = ((forest == null) ? null : forest.ToDTO())
			};
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00019060 File Offset: 0x00017260
		public LoadTemplateGroupByIdResp LoadTemplateGroupById(LoadTemplateGroupByIdReq Request)
		{
			ITemplateManager templateManager = new TemplateManager(Request.GetOperationContext());
			TemplateGroup templateGroup = templateManager.LoadTemplateGroupById(Request.TemplateGroupId);
			return new LoadTemplateGroupByIdResp
			{
				TemplateGroup = ((templateGroup == null) ? null : templateGroup.ToDTO())
			};
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000190A4 File Offset: 0x000172A4
		public void CreateTemplateGroup(CreateTemplateGroupReq Request)
		{
			ITemplateManager templateManager = new TemplateManager(Request.GetOperationContext());
			templateManager.CreateTemplateGroup(Request.TemplateGroup.ToDomainObject());
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000190D0 File Offset: 0x000172D0
		public void DeleteTemplateGroup(DeleteTemplateGroupReq Request)
		{
			ITemplateManager templateManager = new TemplateManager(Request.GetOperationContext());
			templateManager.DeleteTemplateGroup(Request.TemplateGroupId);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000190F8 File Offset: 0x000172F8
		public LoadAllTemplateGroupsResp LoadAllTemplateGroups(LoadAllTemplateGroupsReq Request)
		{
			ITemplateManager templateManager = new TemplateManager(Request.GetOperationContext());
			IList<TemplateGroup> list = templateManager.LoadAllTemplateGroups();
			LoadAllTemplateGroupsResp loadAllTemplateGroupsResp = new LoadAllTemplateGroupsResp();
			IList<TemplateGroupDTO> templateGroups;
			if (list != null)
			{
				templateGroups = list.ToList<TemplateGroup>().ConvertAll<TemplateGroupDTO>((TemplateGroup g) => g.ToDTO());
			}
			else
			{
				templateGroups = null;
			}
			loadAllTemplateGroupsResp.TemplateGroups = templateGroups;
			return loadAllTemplateGroupsResp;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001915C File Offset: 0x0001735C
		public void UpdateTemplate(UpdateTemplateReq Request)
		{
			ITemplateManager templateManager = new TemplateManager(Request.GetOperationContext());
			templateManager.UpdateTemplate(Request.Template.ToDomainObject());
		}
	}
}
