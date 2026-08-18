using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using Databases;
using Newtonsoft.Json;
using TechnoPro.Common.DAO.Email;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.Impl.Email;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.Impl.Properties;
using TechnoPro.Common.DAO.Templates;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.Templates;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.DAO.Impl.Templates
{
	// Token: 0x02000037 RID: 55
	public class TemplateDAO : ITemplateDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000148 RID: 328 RVA: 0x00009ED8 File Offset: 0x000080D8
		public TemplateDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00009EEA File Offset: 0x000080EA
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00009EF2 File Offset: 0x000080F2
		public OperationContext OpContext { get; set; }

		// Token: 0x0600014B RID: 331 RVA: 0x00009EFC File Offset: 0x000080FC
		private void MergeTemplateCollections(ref TemplateCollection tc, TemplateCollection tproTemplates)
		{
			IList<TemplateGroup> tcGroups = tc.Groups;
			IEnumerable<TemplateGroup> enumerable;
			if (tproTemplates.Groups != null)
			{
				enumerable = from h in tproTemplates.Groups
				where tcGroups.FirstOrDefault((TemplateGroup g) => !string.IsNullOrEmpty(g.TemplateGroupId) && !string.IsNullOrEmpty(h.TemplateGroupId) && g.TemplateGroupId.Equals(h.TemplateGroupId, StringComparison.OrdinalIgnoreCase)) == null
				select h;
			}
			else
			{
				IEnumerable<TemplateGroup> enumerable2 = new List<TemplateGroup>();
				enumerable = enumerable2;
			}
			IEnumerable<TemplateGroup> enumerable3 = enumerable;
			foreach (TemplateGroup item in enumerable3)
			{
				tc.Groups.Add(item);
			}
			bool flag = tproTemplates.Templates != null;
			if (flag)
			{
				foreach (Template item2 in tproTemplates.Templates)
				{
					tc.Templates.Add(item2);
				}
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00009FE8 File Offset: 0x000081E8
		private string GetTproTemplatesXml()
		{
			string text = "";
			bool flag = !string.IsNullOrEmpty(text);
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				result = (Resources.TproTemplates ?? "");
			}
			return result;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000A020 File Offset: 0x00008220
		private TemplateCollection LoadAllTproTemplates(bool LoadDocumentsOrEmails)
		{
			string tproTemplatesXml = this.GetTproTemplatesXml();
			return tproTemplatesXml.TemplatesFromXml(LoadDocumentsOrEmails);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000A040 File Offset: 0x00008240
		private Template LoadTproTemplateById(int TemplateId, bool LoadDocumentsOrEmails)
		{
			TemplateCollection templateCollection = this.LoadAllTproTemplates(LoadDocumentsOrEmails);
			bool flag = templateCollection == null || templateCollection.Templates == null;
			Template result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = templateCollection.Templates.FirstOrDefault((Template g) => g.TemplateId > 0 && g.TemplateId == TemplateId);
			}
			return result;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000A098 File Offset: 0x00008298
		private string ConvertEmailAddressesToString(IList<TPMailAddress> addresses)
		{
			bool flag = addresses == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				List<string> list = new List<string>();
				foreach (TPMailAddress tpmailAddress in addresses)
				{
					string a = (tpmailAddress.EmailAddress ?? "").Trim();
					bool flag2 = a.Length > 0 && list.FirstOrDefault((string g) => g.Equals(a, StringComparison.OrdinalIgnoreCase)) == null;
					if (flag2)
					{
						list.Add(a);
					}
				}
				result = string.Join(",", list.ToArray());
			}
			return result;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000A16C File Offset: 0x0000836C
		private string ConvertEmailAttachmentsToString(IList<TPMailAttachment> attachments)
		{
			bool flag = attachments == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = attachments.GetXmlFromAttachmentsList();
			}
			return result;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000A194 File Offset: 0x00008394
		private IDictionary<string, TemplateGroupMeaningAttribute> allTemplateGroupMeanings
		{
			get
			{
				bool flag = this._allTemplateGroupMeanings == null;
				if (flag)
				{
					List<eTemplateGroupMeaning> list = ((eTemplateGroupMeaning[])Enum.GetValues(typeof(eTemplateGroupMeaning))).ToList<eTemplateGroupMeaning>();
					this._allTemplateGroupMeanings = new Dictionary<string, TemplateGroupMeaningAttribute>();
					foreach (eTemplateGroupMeaning eTemplateGroupMeaning in list)
					{
						string key = eTemplateGroupMeaning.ToString().ToLower();
						bool flag2 = eTemplateGroupMeaning != eTemplateGroupMeaning.Unknown && !this._allTemplateGroupMeanings.ContainsKey(key);
						if (flag2)
						{
							this._allTemplateGroupMeanings.Add(key, TemplateGroupMeaningAttribute.GetAttribute(eTemplateGroupMeaning));
						}
					}
				}
				return this._allTemplateGroupMeanings;
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000A268 File Offset: 0x00008468
		private Template GetTemplateFromReader(IDataReader reader)
		{
			bool flag = reader == null;
			Template result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (reader["templateid"] is DBNull) ? 0 : ((int)reader["templateid"]);
				bool flag2 = num < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					string value = (reader["errorifmissingcodes"] is DBNull) ? "" : ((string)reader["errorifmissingcodes"]).Trim();
					Template template = new Template
					{
						TemplateId = num,
						TemplateTitle = reader["title"].ToString(),
						Document = this.GetTemplateDocumentFromReader(reader),
						EmailTemplate = this.GetEmailTemplateFromReader(reader),
						EmailBehindDocumentTemplate = this.GetEmailBehindDocumentTemplateFromReader(reader),
						Group = this.GetTemplateGroupFromRecord(reader),
						FieldMappings = (string.IsNullOrEmpty(value) ? null : JsonConvert.DeserializeObject<IDictionary<string, string>>(value))
					};
					bool flag3 = !(reader["isemailtemplate"] is DBNull) && (bool)reader["isemailtemplate"];
					bool flag4 = template.Document != null && !flag3;
					if (flag4)
					{
						template.TemplateType = eTemplateType.DocumentTemplate;
					}
					else
					{
						bool flag5 = template.EmailTemplate != null;
						if (flag5)
						{
							template.Document = null;
							template.TemplateType = eTemplateType.EmailTemplate;
						}
					}
					result = template;
				}
			}
			return result;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000A3D4 File Offset: 0x000085D4
		private TemplateGroup GetTemplateGroupFromGroupId(string TemplateGroupId)
		{
			bool flag = TemplateGroupId.Length < 1;
			TemplateGroup result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string key = TemplateGroupId.ToLower().Trim();
				bool flag2 = !this.allTemplateGroupMeanings.ContainsKey(key);
				if (flag2)
				{
					result = new TemplateGroup
					{
						TemplateGroupId = TemplateGroupId,
						Title = TemplateGroupId
					};
				}
				else
				{
					TemplateGroupMeaningAttribute templateGroupMeaningAttribute = this.allTemplateGroupMeanings[key];
					result = new TemplateGroup
					{
						TemplateGroupId = TemplateGroupId,
						Title = templateGroupMeaningAttribute.GroupTitle
					};
				}
			}
			return result;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000A458 File Offset: 0x00008658
		private TemplateGroup GetTemplateGroupFromRecord(IDataReader reader)
		{
			bool flag = reader == null;
			TemplateGroup result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string templateGroupId = reader["grp"].ToString().Trim();
				result = this.GetTemplateGroupFromGroupId(templateGroupId);
			}
			return result;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000A494 File Offset: 0x00008694
		private TPMailMessage GetEmailTemplateFromReader(IDataReader reader)
		{
			string text = reader["emailtemplate"].ToString().Trim();
			bool flag = text.Length < 1;
			TPMailMessage result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = text.EmailFromXml();
			}
			return result;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000A4D4 File Offset: 0x000086D4
		private TPMailMessage GetEmailBehindDocumentTemplateFromReader(IDataReader reader)
		{
			string text = reader["ebody"].ToString().Trim();
			bool flag = text.StartsWith("<email>", StringComparison.OrdinalIgnoreCase);
			TPMailMessage result;
			if (flag)
			{
				result = text.EmailFromXml();
			}
			else
			{
				string text2 = reader["eto"].ToString().Trim();
				string text3 = reader["ecc"].ToString().Trim();
				string text4 = reader["ebcc"].ToString().Trim();
				string text5 = reader["eattachments"].ToString().Trim();
				string text6 = reader.ContainsColumn("blankreplacements") ? reader["blankreplacements"].ToString().Trim() : "";
				string text7 = reader["warningifmissingcodes"].ToString().Trim();
				text = text.DecodeHtml();
				bool flag2 = text.Length < 1 && text2.Length < 1 && text3.Length < 1 && text4.Length < 1 && text5.Length < 1 && text7.Length < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					bool flag3 = PeopleDAO.ReaderContainsColumn(reader, "bodytype") || reader["bodytype"] is DBNull;
					eEmailBodyType bodyType;
					if (flag3)
					{
						int num = (int)reader["bodytype"];
						bodyType = (eEmailBodyType)(Enum.IsDefined(typeof(eEmailBodyType), num) ? num : 0);
					}
					else
					{
						bodyType = eEmailBodyType.Unknown;
					}
					int num2 = (int)reader["MessageDeliveryMethod"];
					TPMailMessage tpmailMessage = new TPMailMessage();
					tpmailMessage.IsActive = true;
					tpmailMessage.To = text2.Split(new char[]
					{
						','
					}, StringSplitOptions.RemoveEmptyEntries).ToList<string>().ConvertAll<TPMailAddress>((string g) => new TPMailAddress
					{
						EmailAddress = g
					});
					tpmailMessage.Cc = text3.Split(new char[]
					{
						','
					}, StringSplitOptions.RemoveEmptyEntries).ToList<string>().ConvertAll<TPMailAddress>((string g) => new TPMailAddress
					{
						EmailAddress = g
					});
					tpmailMessage.Bcc = text4.Split(new char[]
					{
						','
					}, StringSplitOptions.RemoveEmptyEntries).ToList<string>().ConvertAll<TPMailAddress>((string g) => new TPMailAddress
					{
						EmailAddress = g
					});
					tpmailMessage.Attachments = text7.GetAttachmentsFromXmlString().ToList<TPMailAttachment>();
					tpmailMessage.Body = text;
					tpmailMessage.BodyHtml = text;
					tpmailMessage.BodyType = bodyType;
					tpmailMessage.Subject = text5;
					TPMailMessage tpmailMessage2 = tpmailMessage;
					object from;
					if (text6.Length <= 0)
					{
						from = null;
					}
					else
					{
						(from = new TPMailAddress()).EmailAddress = text6;
					}
					tpmailMessage2.From = from;
					tpmailMessage.DeliveryMethod = (Enum.IsDefined(typeof(eTPMessageDeliveryMethod), num2) ? ((eTPMessageDeliveryMethod)Enum.Parse(typeof(eTPMessageDeliveryMethod), num2.ToString())) : eTPMessageDeliveryMethod.Unknown);
					result = tpmailMessage;
				}
			}
			return result;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000A7F4 File Offset: 0x000089F4
		private BinaryFile GetTemplateDocumentFromReader(IDataReader reader)
		{
			bool flag = reader == null;
			BinaryFile result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string text = reader["filename"].ToString();
				byte[] array = (reader["binarycontent"] is DBNull) ? null : ((byte[])reader["binarycontent"]);
				bool flag2 = text.Length < 1 && (array == null || array.Count<byte>() < 1);
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = new BinaryFile
					{
						FileName = text,
						ByteArray = array
					};
				}
			}
			return result;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000A884 File Offset: 0x00008A84
		public Template LoadTemplate(int TemplateId, bool LoadDocumentOrEmail)
		{
			bool flag = Template.IsTemplateIdTproTemplate(TemplateId);
			Template result;
			if (flag)
			{
				result = this.LoadTproTemplateById(TemplateId, LoadDocumentOrEmail);
			}
			else
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@tid", DbType.Int32, TemplateId),
					databaseLayer.GetParameter("@includefilecontents", DbType.Boolean, LoadDocumentOrEmail)
				};
				using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("DECLARE @cd char(1)\r\nSET @cd='_'\r\n\r\nSELECT et.templateid,et.efrom,\r\nCASE WHEN CHARINDEX(@cd,efrom,1) > 0\r\nTHEN\r\nSUBSTRING( efrom,1,CHARINDEX(@cd,efrom,1)-1)\r\nELSE '' END AS grp,\r\nCASE WHEN CHARINDEX(@cd,efrom,1) > 0 THEN\r\nSUBSTRING( efrom,CHARINDEX(@cd,efrom,1)+1,LEN(efrom)-CHARINDEX(@cd,efrom,1))\r\nELSE efrom END AS title,\r\nCAST(ebody AS varchar(max)) AS EmailBehindTemplate,\r\nebodypdf AS filename,\r\nebt.EmailTemplate,COALESCE(ebt.IsEmailTemplate,CAST(0 AS bit)) AS IsEmailTemplate,\r\nCASE WHEN NOT emisc IS NULL AND NOT CAST(emisc AS varchar(max))='' THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS HasContent,\r\nCASE WHEN @includefilecontents=1 AND NOT emisc IS NULL AND NOT CAST(emisc AS varchar(max))='' \r\nTHEN CAST('<data>' + CAST(emisc AS varchar(max)) + '</data>' AS xml).value('(data)[1]', 'varbinary(max)')\r\nELSE NULL END AS BinaryContent,\r\nblankreplacements,datecreated,\r\neto,ecc,ebcc,eattachments,ebody,warningifmissingcodes,bodytype,messagedeliverymethod,errorifmissingcodes\r\nFROM emailtemplates et LEFT JOIN EmailBasedTemplates ebt ON ebt.templateid=et.templateid\r\nWHERE et.templateid=@tid\r\nORDER BY grp,title", parameters))
				{
					bool flag2 = dataReader != null && dataReader.Read();
					if (flag2)
					{
						Template templateFromReader = this.GetTemplateFromReader(dataReader);
						bool flag3 = templateFromReader != null;
						if (flag3)
						{
							return templateFromReader;
						}
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000A95C File Offset: 0x00008B5C
		public int CreateNewTemplate(Template Template)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			string text = (Template.Group == null) ? "" : Template.Group.TemplateGroupId;
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@templateid", DbType.Int32, 0),
				databaseLayer.GetParameter("title", DbType.String, text + ((text.Length > 0) ? "_" : "") + (Template.TemplateTitle ?? "")),
				databaseLayer.GetParameter("@fieldmappings", DbType.String, (Template.FieldMappings == null || Template.FieldMappings.Count < 1) ? "" : JsonConvert.SerializeObject(Template.FieldMappings))
			};
			databaseLayer.ExecuteNonQuery("IF NOT EXISTS(SELECT templateid FROM emailtemplates WHERE efrom=@title)\r\nBEGIN\r\n    INSERT INTO emailtemplates (efrom,eto,ecc,ebcc,eattachments,ebody,emisc,blankreplacements,errorifmissingcodes) VALUES (@title,'','','','','','','',@fieldmappings)\r\n    SET @templateid = CAST(SCOPE_IDENTITY() as int)\r\nEND\r\nELSE\r\nBEGIN\r\n    SET @templateid = (SELECT TOP 1 templateid FROM emailtemplates WHERE efrom=@title)\r\nEND", array);
			int num = (int)array[0].Value;
			bool flag = Template.TemplateType == eTemplateType.DocumentTemplate;
			if (flag)
			{
				bool flag2 = Template.Document != null && Template.Document.ByteArray != null && Template.Document.ByteArray.Count<byte>() > 0;
				if (flag2)
				{
					this.ReplaceTemplateFile(num, Template.Document);
				}
				bool flag3 = Template.EmailBehindDocumentTemplate != null;
				if (flag3)
				{
					this.ReplaceTemplateEmailBehindDocument(num, Template.EmailBehindDocumentTemplate);
				}
			}
			else
			{
				bool flag4 = Template.TemplateType == eTemplateType.EmailTemplate;
				if (flag4)
				{
					bool flag5 = Template.EmailTemplate != null;
					if (flag5)
					{
						this.ReplaceTemplateEmail(num, Template.EmailTemplate);
					}
				}
			}
			return num;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000AAE8 File Offset: 0x00008CE8
		public void ReplaceTemplateFile(int TemplateId, BinaryFile File)
		{
			bool flag = File == null;
			if (flag)
			{
				this.ClearTemplateFile(TemplateId);
			}
			else
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@tid", DbType.Int32, TemplateId),
					databaseLayer.GetParameter("@filename", DbType.String, Path.GetFileName(File.FileName)),
					databaseLayer.GetParameter("@bb", DbType.String, Convert.ToBase64String(File.ByteArray))
				};
				databaseLayer.ExecuteNonQuery("UPDATE emailtemplates SET emisc=@bb,ebodypdf=@filename WHERE templateid=@tid", parameters);
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000AB84 File Offset: 0x00008D84
		public void ClearTemplateFile(int TemplateId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@tid", DbType.Int32, TemplateId)
			};
			databaseLayer.ExecuteNonQuery("UPDATE emailtemplates SET ebodypdf='',emisc='' WHERE templateid=@tid", parameters);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000ABD8 File Offset: 0x00008DD8
		public void ClearTemplateEmail(int TemplateId)
		{
			this.RemoveFileAttachments(TemplateId, true, false);
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@tid", DbType.Int32, TemplateId)
			};
			databaseLayer.ExecuteNonQuery("UPDATE emailtemplates SET ebodypdf='',emisc='' WHERE templateid=@tid", parameters);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000AC34 File Offset: 0x00008E34
		private void RemoveFileAttachments(int TemplateId, bool removeEmailAttachments, bool removeEmailBehindAttachments)
		{
			Template template = this.LoadTemplate(TemplateId, false);
			bool flag = template == null;
			if (!flag)
			{
				List<TPMailAttachment> list = (!removeEmailAttachments || template.EmailTemplate == null) ? new List<TPMailAttachment>() : (template.EmailTemplate.Attachments ?? new List<TPMailAttachment>());
				bool flag2 = removeEmailBehindAttachments && template.EmailBehindDocumentTemplate != null && template.EmailBehindDocumentTemplate.Attachments != null;
				if (flag2)
				{
					list.AddRange(template.EmailBehindDocumentTemplate.Attachments);
				}
				IEmailAttachmentDAO emailAttachmentDAO = new EmailAttachmentDAO(this.OpContext);
				foreach (TPMailAttachment tpmailAttachment in list)
				{
					bool flag3 = tpmailAttachment.FileAttachmentId > 0;
					if (flag3)
					{
						emailAttachmentDAO.DeleteAttachment(tpmailAttachment.FileAttachmentId);
					}
				}
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000AD20 File Offset: 0x00008F20
		public void ClearTemplateEmailBehindDocument(int TemplateId)
		{
			this.RemoveFileAttachments(TemplateId, false, true);
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@tid", DbType.Int32, TemplateId)
			};
			databaseLayer.ExecuteNonQuery("UPDATE emailtemplates SET eto='',ecc='',ebcc='',eattachments='',ebody='',warningifmissingcodes='',bodytype=0,blankreplacements='' WHERE templateid=@tid", parameters);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000AD7C File Offset: 0x00008F7C
		public void ReplaceTemplateEmail(int TemplateId, TPMailMessage EmailTemplate)
		{
			bool flag = EmailTemplate == null;
			if (flag)
			{
				this.ClearTemplateEmail(TemplateId);
			}
			else
			{
				bool flag2 = EmailTemplate.Attachments != null;
				if (flag2)
				{
					IEmailAttachmentDAO emailAttachmentDAO = new EmailAttachmentDAO(this.OpContext);
					foreach (TPMailAttachment tpmailAttachment in EmailTemplate.Attachments)
					{
						bool flag3 = tpmailAttachment.FileAttachmentId < 1 && tpmailAttachment.FileBytes != null && tpmailAttachment.FileBytes.Count<byte>() > 0;
						if (flag3)
						{
							tpmailAttachment.FileAttachmentId = emailAttachmentDAO.CreateAttachment(tpmailAttachment);
						}
					}
				}
				BinaryFile file = new BinaryFile
				{
					FileName = "email.xml",
					ByteArray = Encoding.UTF8.GetBytes(EmailTemplate.ToEmailXml())
				};
				this.ReplaceTemplateFile(TemplateId, file);
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000AE70 File Offset: 0x00009070
		public void ReplaceTemplateEmailBehindDocument(int TemplateId, TPMailMessage EmailTemplate)
		{
			bool flag = EmailTemplate == null;
			if (flag)
			{
				this.ClearTemplateEmailBehindDocument(TemplateId);
			}
			else
			{
				eEmailBodyType bodyType = EmailTemplate.BodyType;
				eEmailBodyType eEmailBodyType = bodyType;
				string value;
				if (eEmailBodyType - eEmailBodyType.PlainText > 1)
				{
					value = ((!string.IsNullOrEmpty(EmailTemplate.Body)) ? EmailTemplate.Body : (EmailTemplate.BodyHtml ?? ""));
				}
				else
				{
					value = (EmailTemplate.Body ?? "");
				}
				bool flag2 = EmailTemplate.Attachments != null;
				if (flag2)
				{
					IEmailAttachmentDAO emailAttachmentDAO = new EmailAttachmentDAO(this.OpContext);
					foreach (TPMailAttachment tpmailAttachment in EmailTemplate.Attachments)
					{
						bool flag3 = tpmailAttachment.FileAttachmentId < 1 && tpmailAttachment.FileBytes != null && tpmailAttachment.FileBytes.Count<byte>() > 0;
						if (flag3)
						{
							tpmailAttachment.FileAttachmentId = emailAttachmentDAO.CreateAttachment(tpmailAttachment);
						}
					}
				}
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@tid", DbType.Int32, TemplateId),
					databaseLayer.GetParameter("@eto", DbType.String, this.ConvertEmailAddressesToString(EmailTemplate.To)),
					databaseLayer.GetParameter("@ecc", DbType.String, this.ConvertEmailAddressesToString(EmailTemplate.Cc)),
					databaseLayer.GetParameter("@ebcc", DbType.String, this.ConvertEmailAddressesToString(EmailTemplate.Bcc)),
					databaseLayer.GetParameter("@eattach", DbType.String, this.ConvertEmailAttachmentsToString(EmailTemplate.Attachments)),
					databaseLayer.GetParameter("@esubject", DbType.String, EmailTemplate.Subject ?? ""),
					databaseLayer.GetParameter("@ebody", DbType.String, value),
					databaseLayer.GetParameter("@bodytype", DbType.Int32, (int)EmailTemplate.BodyType),
					databaseLayer.GetParameter("@blankreplacements", DbType.String, (EmailTemplate.From == null || EmailTemplate.From.EmailAddress == null) ? "" : EmailTemplate.From.EmailAddress),
					databaseLayer.GetParameter("@deliverymethod", DbType.Int32, (int)EmailTemplate.DeliveryMethod)
				};
				databaseLayer.ExecuteNonQuery("UPDATE emailtemplates SET eto=@eto,ecc=@ecc,ebcc=@ebcc,warningifmissingcodes=@eattach,eattachments=@esubject,ebody=@ebody,bodytype=@bodytype,blankreplacements=@blankreplacements,MessageDeliveryMethod=@deliverymethod WHERE templateid=@tid", parameters);
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000B0D8 File Offset: 0x000092D8
		public void DeleteTemplate(int TemplateId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@tid", DbType.Int32, TemplateId)
			};
			databaseLayer.ExecuteNonQuery("DELETE FROM emailtemplates WHERE templateid=@tid", parameters);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000B12C File Offset: 0x0000932C
		public TemplateCollection LoadTemplates(string TemplateGroupId, bool LoadDocumentsOrEmails)
		{
			bool flag = TemplateGroupId.EndsWith("_") && TemplateGroupId.Length > 0;
			if (flag)
			{
				TemplateGroupId = TemplateGroupId.Substring(0, TemplateGroupId.Length - 1);
			}
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@groupid", DbType.String, TemplateGroupId ?? ""),
				databaseLayer.GetParameter("@includefilecontents", DbType.Boolean, LoadDocumentsOrEmails)
			};
			TemplateCollection templateCollection;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("DECLARE @cd char(1)\r\nSET @cd='_'\r\n\r\nSELECT y.* \r\nFROM\r\n(\r\nSELECT et.templateid,et.efrom,\r\nCASE WHEN CHARINDEX(@cd,efrom,1) > 0\r\nTHEN\r\nSUBSTRING( efrom,1,CHARINDEX(@cd,efrom,1)-1)\r\nELSE '' END AS grp,\r\nCASE WHEN CHARINDEX(@cd,efrom,1) > 0 THEN\r\nSUBSTRING( efrom,CHARINDEX(@cd,efrom,1)+1,LEN(efrom)-CHARINDEX(@cd,efrom,1))\r\nELSE efrom END AS title,\r\nCAST(ebody AS varchar(max)) AS EmailBehindTemplate,\r\nebodypdf AS filename,\r\nebt.EmailTemplate,COALESCE(ebt.IsEmailTemplate,CAST(0 AS bit)) AS IsEmailTemplate,\r\nCASE WHEN NOT emisc IS NULL AND NOT CAST(emisc AS varchar(max))='' THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS HasContent,\r\nCASE WHEN @includefilecontents=1 AND NOT emisc IS NULL AND NOT CAST(emisc AS varchar(max))='' \r\nTHEN CAST('<data>' + CAST(emisc AS varchar(max)) + '</data>' AS xml).value('(data)[1]', 'varbinary(max)')\r\nELSE NULL END AS BinaryContent,\r\nblankreplacements,datecreated,\r\neto,ecc,ebcc,eattachments,ebody,warningifmissingcodes,bodytype,messagedeliverymethod,errorifmissingcodes\r\nFROM emailtemplates et LEFT JOIN EmailBasedTemplates ebt ON ebt.templateid=et.templateid\r\nWHERE et.isactive=1\r\n) y WHERE y.grp=@groupid\r\nORDER BY y.grp,y.title", parameters))
			{
				bool flag2 = dataReader == null;
				if (flag2)
				{
					return null;
				}
				List<Template> list = new List<Template>();
				while (dataReader.Read())
				{
					Template templateFromReader = this.GetTemplateFromReader(dataReader);
					bool flag3 = templateFromReader != null;
					if (flag3)
					{
						list.Add(templateFromReader);
					}
				}
				templateCollection = new TemplateCollection
				{
					Groups = new List<TemplateGroup>(),
					Templates = list
				};
				foreach (Template template in list)
				{
					TemplateGroup group = template.Group;
					bool flag4 = group != null && templateCollection.Groups.FirstOrDefault((TemplateGroup g) => g.TemplateGroupId.Equals(group.TemplateGroupId, StringComparison.OrdinalIgnoreCase)) != null;
					if (flag4)
					{
						templateCollection.Groups.Add(group);
					}
				}
				IList<TemplateGroup> list2 = this.LoadAllTemplateGroups();
				using (IEnumerator<TemplateGroup> enumerator2 = list2.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						TemplateGroup eg = enumerator2.Current;
						bool flag5 = templateCollection.Groups.FirstOrDefault((TemplateGroup g) => g.TemplateGroupId.Equals(eg.TemplateGroupId, StringComparison.OrdinalIgnoreCase)) == null;
						if (flag5)
						{
							templateCollection.Groups.Add(eg);
						}
					}
				}
			}
			TemplateCollection templateCollection2 = this.LoadAllTproTemplates(LoadDocumentsOrEmails);
			bool flag6 = templateCollection2.Templates != null;
			if (flag6)
			{
				templateCollection2.Templates = (from g in templateCollection2.Templates
				where (g.TemplateGroupId ?? "").Equals(TemplateGroupId ?? "")
				select g).ToList<Template>();
			}
			this.MergeTemplateCollections(ref templateCollection, templateCollection2);
			List<TemplateGroup> list3 = templateCollection.Groups.ToList<TemplateGroup>();
			list3.Sort((TemplateGroup g1, TemplateGroup g2) => (g1.Title ?? "").CompareTo(g2.Title ?? ""));
			templateCollection.Groups = list3;
			return templateCollection;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000B440 File Offset: 0x00009640
		public TemplateCollection LoadAllTemplates(bool LoadDocumentsOrEmails)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@includefilecontents", DbType.Boolean, LoadDocumentsOrEmails)
			};
			TemplateCollection templateCollection;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("DECLARE @cd char(1)\r\nSET @cd='_'\r\n\r\nSELECT y.* \r\nFROM\r\n(\r\nSELECT et.templateid,et.efrom,\r\nCASE WHEN CHARINDEX(@cd,efrom,1) > 0\r\nTHEN\r\nSUBSTRING( efrom,1,CHARINDEX(@cd,efrom,1)-1)\r\nELSE '' END AS grp,\r\nCASE WHEN CHARINDEX(@cd,efrom,1) > 0 THEN\r\nSUBSTRING( efrom,CHARINDEX(@cd,efrom,1)+1,LEN(efrom)-CHARINDEX(@cd,efrom,1))\r\nELSE efrom END AS title,\r\nCAST(ebody AS varchar(max)) AS EmailBehindTemplate,\r\nebodypdf AS filename,\r\nebt.EmailTemplate,COALESCE(ebt.IsEmailTemplate,CAST(0 AS bit)) AS IsEmailTemplate,\r\nCASE WHEN NOT emisc IS NULL AND NOT CAST(emisc AS nvarchar(max))='' THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS HasContent,\r\nCASE WHEN @includefilecontents=1 AND NOT emisc IS NULL AND NOT CAST(emisc AS nvarchar(max))='' \r\nTHEN CAST('<data>' + CAST(emisc AS nvarchar(max)) + '</data>' AS xml).value('(data)[1]', 'varbinary(max)')\r\nELSE NULL END AS BinaryContent,\r\nblankreplacements,datecreated,\r\neto,ecc,ebcc,eattachments,ebody,warningifmissingcodes,bodytype,messagedeliverymethod,errorifmissingcodes\r\nFROM emailtemplates et LEFT JOIN EmailBasedTemplates ebt ON ebt.templateid=et.templateid\r\nWHERE et.isactive=1\r\n) y \r\nORDER BY y.grp,y.title", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					return null;
				}
				List<Template> list = new List<Template>();
				while (dataReader.Read())
				{
					Template templateFromReader = this.GetTemplateFromReader(dataReader);
					bool flag2 = templateFromReader != null;
					if (flag2)
					{
						list.Add(templateFromReader);
					}
				}
				templateCollection = new TemplateCollection
				{
					Groups = new List<TemplateGroup>(),
					Templates = list
				};
				foreach (Template template in list)
				{
					TemplateGroup group = template.Group;
					bool flag3 = group != null && templateCollection.Groups.FirstOrDefault((TemplateGroup g) => g.TemplateGroupId.Equals(group.TemplateGroupId, StringComparison.OrdinalIgnoreCase)) != null;
					if (flag3)
					{
						templateCollection.Groups.Add(group);
					}
				}
				IList<TemplateGroup> list2 = this.LoadAllTemplateGroups();
				using (IEnumerator<TemplateGroup> enumerator2 = list2.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						TemplateGroup eg = enumerator2.Current;
						bool flag4 = templateCollection.Groups.FirstOrDefault((TemplateGroup g) => g.TemplateGroupId.Equals(eg.TemplateGroupId, StringComparison.OrdinalIgnoreCase)) == null;
						if (flag4)
						{
							templateCollection.Groups.Add(eg);
						}
					}
				}
			}
			TemplateCollection tproTemplates = this.LoadAllTproTemplates(LoadDocumentsOrEmails);
			this.MergeTemplateCollections(ref templateCollection, tproTemplates);
			List<TemplateGroup> list3 = templateCollection.Groups.ToList<TemplateGroup>();
			list3.Sort((TemplateGroup g1, TemplateGroup g2) => (g1.Title ?? "").CompareTo(g2.Title ?? ""));
			templateCollection.Groups = list3;
			return templateCollection;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000B6A8 File Offset: 0x000098A8
		public IList<TemplateGroup> LoadAllTemplateGroups()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<TemplateGroup> items;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("DECLARE @cd char(1)\r\nSET @cd='_'\r\n\r\nSELECT DISTINCT x.* FROM\r\n(\r\nSELECT CASE WHEN CHARINDEX(@cd,efrom,1) > 0\r\nTHEN\r\nSUBSTRING( efrom,1,CHARINDEX(@cd,efrom,1)-1)\r\nELSE '' END AS grp,'' AS grptitle\r\nFROM emailtemplates et \r\nWHERE et.isactive=1\r\n\r\nUNION\r\n\r\nSELECT templategroupname AS grp,templategrouptitle AS grptitle \r\nFROM emailtemplategroups \r\nWHERE isactive=1\r\n) x\r\n\r\nORDER BY x.grp"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					return null;
				}
				items = new List<TemplateGroup>();
				while (dataReader.Read())
				{
					TemplateGroup templateGroupFromRecord = this.GetTemplateGroupFromRecord(dataReader);
					bool flag2 = templateGroupFromRecord != null;
					if (flag2)
					{
						items.Add(templateGroupFromRecord);
					}
				}
			}
			TemplateCollection templateCollection = this.LoadAllTproTemplates(false);
			bool flag3 = templateCollection.Groups != null;
			if (flag3)
			{
				IEnumerable<TemplateGroup> collection = from g in templateCollection.Groups
				where !string.IsNullOrEmpty(g.TemplateGroupId) && items.FirstOrDefault((TemplateGroup h) => !string.IsNullOrEmpty(h.TemplateGroupId) && h.TemplateGroupId.Equals(g.TemplateGroupId, StringComparison.OrdinalIgnoreCase)) == null
				select g;
				items.AddRange(collection);
			}
			return items;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000B79C File Offset: 0x0000999C
		public string CreateTemplateGroup(TemplateGroup Group)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@newgid", DbType.String, 256),
				databaseLayer.GetParameter("@gid", DbType.String, Group.TemplateGroupId),
				databaseLayer.GetParameter("@title", DbType.String, (!string.IsNullOrEmpty(Group.Title)) ? Group.Title : Group.TemplateGroupId)
			};
			databaseLayer.ExecuteNonQuery("IF NOT @gid IS NULL AND NOT @gid='' AND NOT EXISTS(SELECT templategroupname FROM emailtemplategroups WHERE templategroupname=@gid)\r\nBEGIN\r\n    INSERT INTO emailtemplategroups (templategroupname,TemplateGroupTitle,isactive) VALUES (@gid,@title,1);\r\nEND\r\n\r\nSET @newgid=@gid", array);
			return array[0].Value.ToString().Trim();
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000B844 File Offset: 0x00009A44
		public void DeleteTemplateGroup(string TemplateGroupId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@gid", DbType.String, TemplateGroupId)
			};
			databaseLayer.ExecuteNonQuery("DELETE FROM emailtemplategroups WHERE templategroupname=@gid", parameters);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000B890 File Offset: 0x00009A90
		public void UpdateTemplateTitleAndGroup(int TemplateId, string TemplateGroupId, string TemplateTitle, IDictionary<string, string> fieldMappings)
		{
			TemplateGroupId = ((TemplateGroupId == null) ? "" : TemplateGroupId.Trim());
			bool flag = string.IsNullOrEmpty(TemplateTitle);
			if (flag)
			{
				TemplateTitle = "template";
			}
			bool flag2 = TemplateGroupId.EndsWith("_") && TemplateGroupId.Length > 1;
			if (flag2)
			{
				TemplateGroupId = TemplateGroupId.Substring(0, TemplateGroupId.Length - 1);
			}
			string value = (TemplateGroupId + ((!string.IsNullOrEmpty(TemplateGroupId)) ? "_" : "") + TemplateTitle.Trim()).Trim();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@tid", DbType.Int32, TemplateId),
				databaseLayer.GetParameter("@title", DbType.String, value),
				databaseLayer.GetParameter("@fieldmappings", DbType.String, (fieldMappings == null) ? "" : JsonConvert.SerializeObject(fieldMappings))
			};
			databaseLayer.ExecuteNonQuery("UPDATE emailtemplates SET efrom=@title,errorifmissingcodes=@fieldmappings WHERE templateid=@tid", parameters);
		}

		// Token: 0x0400008D RID: 141
		private IDictionary<string, TemplateGroupMeaningAttribute> _allTemplateGroupMeanings;
	}
}
