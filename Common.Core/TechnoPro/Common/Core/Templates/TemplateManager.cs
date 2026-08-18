using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.DAO.Impl.Templates;
using TechnoPro.Common.DAO.Templates;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.ICore.Templates;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.Templates;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.Core.Templates
{
	// Token: 0x02000034 RID: 52
	public class TemplateManager : ITemplateManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000B5D4 File Offset: 0x000097D4
		// (set) Token: 0x06000201 RID: 513 RVA: 0x0000B5DC File Offset: 0x000097DC
		private ITemplateDAO dao { get; set; }

		// Token: 0x06000202 RID: 514 RVA: 0x0000B5E5 File Offset: 0x000097E5
		public TemplateManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new TemplateDAO(opContext);
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000B604 File Offset: 0x00009804
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0000B60C File Offset: 0x0000980C
		public OperationContext OpContext { get; set; }

		// Token: 0x06000205 RID: 517 RVA: 0x0000B618 File Offset: 0x00009818
		public Template LoadTemplate(int TemplateId, bool LoadDocumentOrEmail)
		{
			return this.dao.LoadTemplate(TemplateId, LoadDocumentOrEmail);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000B638 File Offset: 0x00009838
		public int CreateNewTemplate(Template Template)
		{
			return this.dao.CreateNewTemplate(Template);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000B656 File Offset: 0x00009856
		public void ReplaceTemplateFile(int TemplateId, BinaryFile File)
		{
			this.dao.ReplaceTemplateFile(TemplateId, File);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000B667 File Offset: 0x00009867
		public void ReplaceTemplateEmail(int TemplateId, TPMailMessage EmailTemplate)
		{
			this.dao.ReplaceTemplateEmail(TemplateId, EmailTemplate);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000B678 File Offset: 0x00009878
		public void ReplaceTemplateEmailBehindDocument(int TemplateId, TPMailMessage EmailTemplate)
		{
			this.dao.ReplaceTemplateEmailBehindDocument(TemplateId, EmailTemplate);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000B689 File Offset: 0x00009889
		public void DeleteTemplate(int TemplateId)
		{
			this.dao.DeleteTemplate(TemplateId);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000B69C File Offset: 0x0000989C
		public TemplateCollection LoadTemplates(string TemplateGroupId, bool LoadDocumentsOrEmails)
		{
			return this.dao.LoadTemplates(TemplateGroupId, LoadDocumentsOrEmails);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000B6BC File Offset: 0x000098BC
		public TemplateCollection LoadAllTemplates(bool LoadDocumentsOrEmails)
		{
			return this.dao.LoadAllTemplates(LoadDocumentsOrEmails);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000B6DC File Offset: 0x000098DC
		public Forest<TemplateOrGroup> LoadAllTemplatesAsForest(bool LoadDocumentsOrEmails)
		{
			TemplateCollection templateCollection = this.LoadAllTemplates(LoadDocumentsOrEmails);
			return templateCollection.ConvertTemplateCollectionToForest();
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000B6FC File Offset: 0x000098FC
		public TemplateGroup LoadTemplateGroupById(string TemplateGroupId)
		{
			IList<TemplateGroup> source = this.LoadAllTemplateGroups();
			return source.FirstOrDefault((TemplateGroup g) => g.TemplateGroupId.Equals(TemplateGroupId, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000B734 File Offset: 0x00009934
		public IList<TemplateGroup> LoadAllTemplateGroups()
		{
			IList<TemplateGroup> list = this.dao.LoadAllTemplateGroups();
			bool flag = list == null;
			if (flag)
			{
				list = new List<TemplateGroup>();
			}
			eTemplateGroupMeaning[] array = (eTemplateGroupMeaning[])Enum.GetValues(typeof(eTemplateGroupMeaning));
			foreach (eTemplateGroupMeaning meaning in array)
			{
				TemplateGroupMeaningAttribute attribute = TemplateGroupMeaningAttribute.GetAttribute(meaning);
				bool flag2 = attribute != null;
				if (flag2)
				{
					string title = attribute.GroupTitle ?? "";
					TemplateGroup templateGroup = list.FirstOrDefault((TemplateGroup g) => g.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
					bool flag3 = templateGroup == null;
					if (flag3)
					{
						list.Add(new TemplateGroup
						{
							TemplateGroupId = title,
							Title = title
						});
					}
				}
			}
			return list;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000B817 File Offset: 0x00009A17
		public void CreateTemplateGroup(TemplateGroup Group)
		{
			this.dao.CreateTemplateGroup(Group);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000B827 File Offset: 0x00009A27
		public void DeleteTemplateGroup(string TemplateGroupId)
		{
			this.dao.DeleteTemplateGroup(TemplateGroupId);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000B838 File Offset: 0x00009A38
		private bool AreDictionariesTheSame(IDictionary<string, string> d1, IDictionary<string, string> d2)
		{
			IDictionary<string, string> dictionary = d1 ?? new Dictionary<string, string>();
			IDictionary<string, string> dictionary2 = d2 ?? new Dictionary<string, string>();
			bool flag = dictionary.Count < 1 && dictionary2.Count < 1;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					bool flag2 = !dictionary2.ContainsKey(keyValuePair.Key);
					if (flag2)
					{
						return false;
					}
					bool flag3 = keyValuePair.Value != dictionary2[keyValuePair.Key];
					if (flag3)
					{
						return false;
					}
				}
				foreach (KeyValuePair<string, string> keyValuePair2 in dictionary2)
				{
					bool flag4 = !dictionary.ContainsKey(keyValuePair2.Key);
					if (flag4)
					{
						return false;
					}
					bool flag5 = keyValuePair2.Value != dictionary[keyValuePair2.Key];
					if (flag5)
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000B97C File Offset: 0x00009B7C
		public void UpdateTemplate(Template Template)
		{
			Template template = this.LoadTemplate(Template.TemplateId, true);
			string a = (Template.Group == null) ? "" : Template.Group.TemplateGroupId;
			string b = (template.Group == null) ? "" : template.Group.TemplateGroupId;
			bool flag = template.TemplateTitle != Template.TemplateTitle || a != b || !this.AreDictionariesTheSame(template.FieldMappings, Template.FieldMappings);
			if (flag)
			{
				this.UpdateTemplateTitleAndGroup(Template.TemplateId, Template.Group.TemplateGroupId, Template.TemplateTitle, Template.FieldMappings);
			}
			byte[] first = (Template.Document == null) ? new byte[0] : Template.Document.ByteArray;
			byte[] second = (template.Document == null) ? new byte[0] : template.Document.ByteArray;
			bool flag2 = !first.SequenceEqual(second);
			if (flag2)
			{
				this.ReplaceTemplateFile(Template.TemplateId, Template.Document);
			}
			string a2 = (Template.EmailTemplate == null) ? "" : Template.EmailTemplate.ToEmailXml();
			string b2 = (template.EmailTemplate == null) ? "" : template.EmailTemplate.ToEmailXml();
			bool flag3 = a2 != b2;
			if (flag3)
			{
				this.ReplaceTemplateEmail(Template.TemplateId, Template.EmailTemplate);
			}
			string a3 = (Template.EmailBehindDocumentTemplate == null) ? "" : Template.EmailBehindDocumentTemplate.ToEmailXml();
			string b3 = (template.EmailBehindDocumentTemplate == null) ? "" : template.EmailBehindDocumentTemplate.ToEmailXml();
			bool flag4 = a3 != b3;
			if (flag4)
			{
				this.ReplaceTemplateEmailBehindDocument(Template.TemplateId, Template.EmailBehindDocumentTemplate);
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000BB3D File Offset: 0x00009D3D
		private void UpdateTemplateTitleAndGroup(int TemplateId, string TemplateGroupId, string TemplateTitle, IDictionary<string, string> fieldMappings)
		{
			this.dao.UpdateTemplateTitleAndGroup(TemplateId, TemplateGroupId, TemplateTitle, fieldMappings);
		}
	}
}
