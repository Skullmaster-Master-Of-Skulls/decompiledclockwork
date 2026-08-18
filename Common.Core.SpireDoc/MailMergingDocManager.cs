using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Core.AppointmentsPointOfContact;
using TechnoPro.Common.Core.CourseRegistrations;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.LookupCourses;
using TechnoPro.Common.Core.MailMerging;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.Core.Templates;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.DynamicQueries;
using TechnoPro.Common.DAO.Impl.DynamicQueries;
using TechnoPro.Common.DAO.Impl.MailMerging;
using TechnoPro.Common.DAO.MailMerging;
using TechnoPro.Common.DAO.SpireDoc.Impl;
using TechnoPro.Common.ICore.AppointmentsPointOfContact;
using TechnoPro.Common.ICore.CourseRegistrations;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.ICore.MailMerging;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.ICore.Templates;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentsPointOfContact;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.Accommodations;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities.DocumentForPrint;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Output;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.Templates;
using TechnoPro.Common.Public.Entities.TPMailMan;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;

namespace TechnoPro.Common.Core.SpireDoc
{
	// Token: 0x02000004 RID: 4
	public class MailMergingDocManager : IMailMergingDocManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000027F8 File Offset: 0x000009F8
		private MailMergingDocManager mailMergingDocManager
		{
			get
			{
				bool flag = this._mdm == null;
				if (flag)
				{
					this._mdm = new MailMergingDocManager(this.OpContext);
				}
				return this._mdm;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002830 File Offset: 0x00000A30
		private MailMergingManager MailMergingManager
		{
			get
			{
				MailMergingManager result;
				if ((result = this._mm) == null)
				{
					result = (this._mm = new MailMergingManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000285B File Offset: 0x00000A5B
		public MailMergingDocManager()
		{
			this._mailMergingDocDao = new MailMergingDocDAO(this.OpContext);
			this._templateManager = new TemplateManager(this.OpContext);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002887 File Offset: 0x00000A87
		public MailMergingDocManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this._mailMergingDocDao = new MailMergingDocDAO(opContext);
			this._templateManager = new TemplateManager(opContext);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000028B4 File Offset: 0x00000AB4
		private int? GetTemplateIdFromOverrideSql(int pid, IList<int> lucids, string sql)
		{
			bool flag = string.IsNullOrEmpty(sql);
			int? result;
			if (flag)
			{
				result = new int?(0);
			}
			else
			{
				IDynamicQueryDAO dynamicQueryDAO = new DynamicQueryDAO(this.OpContext);
				string text = sql.Replace("@pid", pid.ToString()).Replace("@lucid", (lucids != null && lucids.Count > 0) ? lucids[0].ToString() : "0");
				string oldValue = "@lucids";
				string newValue;
				if (lucids != null && lucids.Count >= 1)
				{
					newValue = "'" + string.Join(",", (from g in lucids
					select g.ToString()).ToArray<string>()) + "'";
				}
				else
				{
					newValue = "''";
				}
				string sql2 = text.Replace(oldValue, newValue);
				result = dynamicQueryDAO.LoadInt(sql2);
			}
			return result;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002990 File Offset: 0x00000B90
		private int FigureOutWhichTemplateToUse(AccommodationLetterGenerateContext GenerateContext)
		{
			IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			eAccommodationLetterGenerationType letterType = GenerateContext.LetterType;
			eAccommodationLetterGenerationType eAccommodationLetterGenerationType = letterType;
			int result;
			if (eAccommodationLetterGenerationType != eAccommodationLetterGenerationType.ProfLetter)
			{
				bool flag = GenerateContext.OutputType == eAccommodationLetterGenerationOutputType.Html;
				if (flag)
				{
					CWLogger.Logger.Warn("Common.Core.SpireDoc.MailMergingDocManager.AutoMailMergeAccommodationLetters:Unsupported output type of html on {0}; using pdf template instead", GenerateContext.LetterType.ToString());
				}
				string settingValue = webSettingManager.GetSettingValue<string>(Setting.ACCOMMODATIONS_TemplateChooserForStudent_OverrideSql);
				bool flag2 = !string.IsNullOrEmpty(settingValue);
				if (flag2)
				{
					int? templateIdFromOverrideSql = this.GetTemplateIdFromOverrideSql(GenerateContext.StudentPersonId, GenerateContext.LuCourseIds, settingValue);
					bool flag3 = templateIdFromOverrideSql != null;
					if (flag3)
					{
						return templateIdFromOverrideSql.Value;
					}
				}
				bool flag4 = GenerateContext.PreferredTemplateId > 0;
				if (flag4)
				{
					result = GenerateContext.PreferredTemplateId;
				}
				else
				{
					result = webSettingManager.GetSettingValue<int>(Setting.ACCOMMODATIONS_LetterTemplateId);
				}
			}
			else
			{
				bool flag5 = GenerateContext.OutputType == eAccommodationLetterGenerationOutputType.Html;
				if (flag5)
				{
					string settingValue2 = webSettingManager.GetSettingValue<string>(Setting.ACCOMMODATIONS_TemplateChooserForInstructorHtml_OverrideSql);
					bool flag6 = !string.IsNullOrEmpty(settingValue2);
					if (flag6)
					{
						int? templateIdFromOverrideSql2 = this.GetTemplateIdFromOverrideSql(GenerateContext.StudentPersonId, GenerateContext.LuCourseIds, settingValue2);
						bool flag7 = templateIdFromOverrideSql2 != null;
						if (flag7)
						{
							return templateIdFromOverrideSql2.Value;
						}
					}
					result = webSettingManager.GetSettingValue<int>(Setting.INSTRUCTOR_AccommodationLetterHTMLTemplateId);
				}
				else
				{
					string settingValue3 = webSettingManager.GetSettingValue<string>(Setting.ACCOMMODATIONS_TemplateChooserForInstructor_OverrideSql);
					bool flag8 = !string.IsNullOrEmpty(settingValue3);
					if (flag8)
					{
						int? templateIdFromOverrideSql3 = this.GetTemplateIdFromOverrideSql(GenerateContext.StudentPersonId, GenerateContext.LuCourseIds, settingValue3);
						bool flag9 = templateIdFromOverrideSql3 != null;
						if (flag9)
						{
							return templateIdFromOverrideSql3.Value;
						}
					}
					bool flag10 = GenerateContext.PreferredTemplateId > 0;
					if (flag10)
					{
						result = GenerateContext.PreferredTemplateId;
					}
					else
					{
						result = webSettingManager.GetSettingValue<int>(Setting.INSTRUCTOR_AccommodationLetterTemplateId);
					}
				}
			}
			return result;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002B58 File Offset: 0x00000D58
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002B60 File Offset: 0x00000D60
		public OperationContext OpContext { get; set; }

		// Token: 0x0600001A RID: 26 RVA: 0x00002B6C File Offset: 0x00000D6C
		public MailMergeCodesWithTemplate ExtractUniqueCodes(BinaryFile WordFile, IDictionary<string, string> fieldMappings = null)
		{
			MailMergingDocManager.TemplateFileInfo templateFileInfo = new MailMergingDocManager.TemplateFileInfo(WordFile);
			IList<string> list;
			if (WordFile != null)
			{
				list = this._mailMergingDocDao.ExtractUniqueCodes(templateFileInfo.FileBytes, templateFileInfo.FileType, templateFileInfo.IsLicensed);
			}
			else
			{
				IList<string> list2 = new List<string>();
				list = list2;
			}
			IList<string> list3 = list;
			MailMergingManager mailMergingManager = this.MailMergingManager;
			bool flag = list3 == null;
			if (flag)
			{
				list3 = new List<string>();
			}
			IList<MailMergeCode> list4 = mailMergingManager.ExtractUniqueCodes(list3.ToList<string>(), fieldMappings);
			return new MailMergeCodesWithTemplate
			{
				Codes = ((list4 != null) ? list4.ToList<MailMergeCode>() : null),
				Template = new Template
				{
					Document = WordFile
				}
			};
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002C04 File Offset: 0x00000E04
		public MailMergeCodesWithTemplate ExtractUniqueCodes(int TemplateId)
		{
			bool flag = TemplateId < 1;
			MailMergeCodesWithTemplate result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Template template = this._templateManager.LoadTemplate(TemplateId, true);
				MailMergingDocManager mailMergingDocManager = this.mailMergingDocManager;
				result = mailMergingDocManager.ExtractUniqueCodes(template.Document, template.FieldMappings);
			}
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002C4C File Offset: 0x00000E4C
		public BinaryFile OutputFile(BinaryFile Template, List<List<MailMergeCode>> MultipleCodes, eFileFormat OutputFileFormat)
		{
			MailMergingDocManager.TemplateFileInfo templateFileInfo = new MailMergingDocManager.TemplateFileInfo(Template);
			return this._mailMergingDocDao.OutputFile(templateFileInfo.FileBytes, templateFileInfo.FileType, templateFileInfo.FileName, templateFileInfo.IsLicensed, MultipleCodes, OutputFileFormat);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002C8C File Offset: 0x00000E8C
		public BinaryFile OutputFileMailingLabels(BinaryFile Template, List<List<MailMergeCode>> MultipleCodes, eFileFormat OutputFileFormat)
		{
			return this._mailMergingDocDao.OutputFileMailingLabels(Template, MultipleCodes, OutputFileFormat);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002CAC File Offset: 0x00000EAC
		public BinaryFile OutputFile(MailMergeCodesWithTemplate Codes, eFileFormat OutputFileFormat)
		{
			BinaryFile bf;
			if (Codes == null)
			{
				bf = null;
			}
			else
			{
				Template template = Codes.Template;
				bf = ((template != null) ? template.Document : null);
			}
			MailMergingDocManager.TemplateFileInfo templateFileInfo = new MailMergingDocManager.TemplateFileInfo(bf);
			List<MailMergeCode> list = (Codes != null) ? Codes.Codes : null;
			List<List<MailMergeCode>> list2 = new List<List<MailMergeCode>>();
			bool flag = list != null;
			if (flag)
			{
				list2.Add(list);
			}
			return this._mailMergingDocDao.OutputFile(templateFileInfo.FileBytes, templateFileInfo.FileType, templateFileInfo.FileName, templateFileInfo.IsLicensed, list2, OutputFileFormat);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002D28 File Offset: 0x00000F28
		public BinaryFile MailMerge(MailMergeContextWithCustomDictionary ContextWithCustomDictionary, eFileFormat OutputFileFormat, BinaryFile WordFile)
		{
			MailMergeCodesWithTemplate mailMergeCodesWithTemplate = this.ExtractUniqueCodes(WordFile, null);
			IList<MailMergeCode> source = this.MailMergingManager.LookupCodeValues(ContextWithCustomDictionary, mailMergeCodesWithTemplate.Codes);
			mailMergeCodesWithTemplate.Codes = source.ToList<MailMergeCode>();
			return this.OutputFile(mailMergeCodesWithTemplate, OutputFileFormat);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002D6C File Offset: 0x00000F6C
		public BinaryFile MailMerge(MailMergeContextWithCustomDictionary ContextWithCustomDictionary, eFileFormat OutputFileFormat, int TemplateId)
		{
			MailMergeCodesWithTemplate mailMergeCodesWithTemplate = this.ExtractUniqueCodes(TemplateId);
			IList<MailMergeCode> source = this.MailMergingManager.LookupCodeValues(ContextWithCustomDictionary, mailMergeCodesWithTemplate.Codes);
			mailMergeCodesWithTemplate.Codes = source.ToList<MailMergeCode>();
			return this.OutputFile(mailMergeCodesWithTemplate, OutputFileFormat);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public TPMailMessage MailMergeAccommodationSingleEmailWithLetterAsAttachment(IList<int> LuCourseIds, MailMergeContextWithCustomDictionary ContextWithCustomDictionary, eFileFormat OutputFileFormat, int TemplateId)
		{
			MailMergingEmailManager mailMergingEmailManager = new MailMergingEmailManager(this.OpContext);
			TPMailMessage tpmailMessage = mailMergingEmailManager.MailMergeAccommodationSingleLetterEmail(LuCourseIds, ContextWithCustomDictionary, TemplateId);
			BinaryFile binaryFile = this.MailMergeAccommodationSingleLetter(LuCourseIds, ContextWithCustomDictionary, OutputFileFormat, TemplateId);
			bool flag = tpmailMessage.Attachments == null;
			if (flag)
			{
				tpmailMessage.Attachments = new List<TPMailAttachment>();
			}
			List<TPMailAttachment> attachments = tpmailMessage.Attachments;
			bool flag2 = binaryFile != null;
			if (flag2)
			{
				this.AddAttachment(new TPMailAttachment
				{
					FileBytes = binaryFile.ByteArray,
					FileNameForDisplay = binaryFile.FileName
				}, ref attachments);
			}
			return tpmailMessage;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002E40 File Offset: 0x00001040
		private void AddAttachment(TPMailAttachment attachment, ref List<TPMailAttachment> Attachments)
		{
			List<TPMailAttachment> list = new List<TPMailAttachment>
			{
				attachment
			};
			list.AddRange(Attachments.Where(delegate(TPMailAttachment a)
			{
				bool flag = a.FileBytes == null;
				if (flag)
				{
					a.FileBytes = new byte[0];
				}
				return !string.IsNullOrEmpty(a.FileNameForDisplay);
			}));
			Attachments.Clear();
			Attachments.AddRange(list);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002E9C File Offset: 0x0000109C
		public IDictionary<int, TPMailMessage> MailMergeAccommodationEmailsWithLettersAsAttachments(IList<int> LuCourseIds, MailMergeContextWithCustomDictionary ContextWithCustomDictionary, eFileFormat OutputFileFormat, int TemplateId)
		{
			MailMergingEmailManager mailMergingEmailManager = new MailMergingEmailManager(this.OpContext);
			IDictionary<int, TPMailMessage> dictionary = mailMergingEmailManager.MailMergeAccommodationLetterCoursesEmail(LuCourseIds, ContextWithCustomDictionary, TemplateId);
			Dictionary<int, TPMailMessage> dictionary2 = new Dictionary<int, TPMailMessage>();
			foreach (KeyValuePair<int, TPMailMessage> keyValuePair in dictionary)
			{
				int key = keyValuePair.Key;
				ContextWithCustomDictionary.Context.LuCourseId = key;
				BinaryFile binaryFile = this.MailMergeAccommodationSingleLetter(LuCourseIds, ContextWithCustomDictionary, OutputFileFormat, TemplateId);
				TPMailMessage value = keyValuePair.Value;
				bool flag = value.Attachments == null;
				if (flag)
				{
					value.Attachments = new List<TPMailAttachment>();
				}
				List<TPMailAttachment> attachments = value.Attachments;
				this.AddAttachment(new TPMailAttachment
				{
					FileBytes = binaryFile.ByteArray,
					FileNameForDisplay = binaryFile.FileName
				}, ref attachments);
				dictionary2.Add(key, value);
			}
			return dictionary2;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002F98 File Offset: 0x00001198
		public BinaryFile MailMergeAccommodationLetter(IList<int> LuCourseIds, MailMergeContextWithCustomDictionary ContextWithCustomDictionary, eFileFormat OutputFileFormat, int TemplateId)
		{
			MailMergeCodesWithTemplate mailMergeCodesWithTemplate = this.ExtractUniqueCodes(TemplateId);
			List<List<MailMergeCode>> list = new List<List<MailMergeCode>>();
			foreach (int luCourseId in LuCourseIds)
			{
				List<MailMergeCode> list2 = new List<MailMergeCode>();
				foreach (MailMergeCode mailMergeCode in mailMergeCodesWithTemplate.Codes)
				{
					MailMergeCode mailMergeCode2 = new MailMergeCode(mailMergeCode);
					mailMergeCode2.SetMailMergeValueDirectly(mailMergeCode.GetMailMergeValuesDirectly());
					list2.Add(mailMergeCode2);
				}
				ContextWithCustomDictionary.Context.LuCourseId = luCourseId;
				IList<MailMergeCode> source = this.MailMergingManager.LookupCodeValues(ContextWithCustomDictionary, list2);
				list.Add(source.ToList<MailMergeCode>());
			}
			return this.OutputFile(mailMergeCodesWithTemplate.Template.Document, list, OutputFileFormat);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000030A4 File Offset: 0x000012A4
		public BinaryFile MailMergeAccommodationSingleLetter(IList<int> LuCourseIds, MailMergeContextWithCustomDictionary ContextWithCustomDictionary, eFileFormat OutputFileFormat, int TemplateId)
		{
			MailMergeCodesWithTemplate mailMergeCodesWithTemplate = this.ExtractUniqueCodes(TemplateId);
			List<List<MailMergeCode>> list = new List<List<MailMergeCode>>();
			IList<MailMergeCode> source = this.MailMergingManager.LookupCodeValues(ContextWithCustomDictionary, mailMergeCodesWithTemplate.Codes);
			list.Add(source.ToList<MailMergeCode>());
			return this.OutputFile(mailMergeCodesWithTemplate.Template.Document, list, OutputFileFormat);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000030F8 File Offset: 0x000012F8
		public BinaryFile MailMergeMultipleItemsToOneDocument(IList<MailMergeContextWithCustomDictionary> MailMergeContextsWithCustomDictionaries, eFileFormat OutputFileFormat, int TemplateId)
		{
			MailMergeCodesWithTemplate mailMergeCodesWithTemplate = this.ExtractUniqueCodes(TemplateId);
			List<List<MailMergeCode>> list = new List<List<MailMergeCode>>();
			foreach (MailMergeContextWithCustomDictionary contextWithCustomDictionary in MailMergeContextsWithCustomDictionaries)
			{
				List<MailMergeCode> list2 = new List<MailMergeCode>();
				foreach (MailMergeCode mailMergeCode in mailMergeCodesWithTemplate.Codes)
				{
					MailMergeCode mailMergeCode2 = new MailMergeCode(mailMergeCode);
					mailMergeCode2.SetMailMergeValueDirectly(mailMergeCode.GetMailMergeValuesDirectly());
					list2.Add(mailMergeCode2);
				}
				IList<MailMergeCode> source = this.MailMergingManager.LookupCodeValues(contextWithCustomDictionary, list2);
				list.Add(source.ToList<MailMergeCode>());
			}
			return this.OutputFile(mailMergeCodesWithTemplate.Template.Document, list, OutputFileFormat);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000031F4 File Offset: 0x000013F4
		public BinaryFile MailMergeExamSheets(IList<MailMergeContextWithCustomDictionary> MailMergeContextsWithCustomDictionaries, eFileFormat OutputFileFormat, int TemplateId)
		{
			MailMergeCodesWithTemplate mailMergeCodesWithTemplate = this.ExtractUniqueCodes(TemplateId);
			List<List<MailMergeCode>> list = new List<List<MailMergeCode>>();
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			string text = oldUserSettingManager.GetSettingValue_String(this.OpContext.WhoAmI, eSettingCode.SETTING_Tests_DateFormat, false) ?? "";
			bool flag = text.Trim().Length < 1;
			if (flag)
			{
				text = null;
			}
			string text2 = oldUserSettingManager.GetSettingValue_String(this.OpContext.WhoAmI, eSettingCode.SETTING_Tests_TimeFormat, false) ?? "";
			bool flag2 = text2.Trim().Length < 1;
			if (flag2)
			{
				text2 = null;
			}
			foreach (MailMergeContextWithCustomDictionary mailMergeContextWithCustomDictionary in MailMergeContextsWithCustomDictionaries)
			{
				mailMergeContextWithCustomDictionary.Context.DefaultDateFormat = text;
				mailMergeContextWithCustomDictionary.Context.DefaultTimeFormat = text2;
				List<MailMergeCode> list2 = new List<MailMergeCode>();
				foreach (MailMergeCode mailMergeCode in mailMergeCodesWithTemplate.Codes)
				{
					MailMergeCode mailMergeCode2 = new MailMergeCode(mailMergeCode);
					mailMergeCode2.SetMailMergeValueDirectly(mailMergeCode.GetMailMergeValuesDirectly());
					list2.Add(mailMergeCode2);
				}
				IList<MailMergeCode> source = this.MailMergingManager.LookupCodeValues(mailMergeContextWithCustomDictionary, list2);
				list.Add(source.ToList<MailMergeCode>());
			}
			return this.OutputFile(mailMergeCodesWithTemplate.Template.Document, list, OutputFileFormat);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003390 File Offset: 0x00001590
		public BinaryFile MailMergeMailingLabels(IList<MailMergeContextWithCustomDictionary> MailMergeContextsWithCustomDictionaries, eFileFormat OutputFileFormat, int TemplateId)
		{
			MailMergeCodesWithTemplate mailMergeCodesWithTemplate = this.ExtractUniqueCodes(TemplateId);
			List<List<MailMergeCode>> list = new List<List<MailMergeCode>>();
			foreach (MailMergeContextWithCustomDictionary contextWithCustomDictionary in MailMergeContextsWithCustomDictionaries)
			{
				List<MailMergeCode> list2 = new List<MailMergeCode>();
				foreach (MailMergeCode mailMergeCode in mailMergeCodesWithTemplate.Codes)
				{
					MailMergeCode mailMergeCode2 = new MailMergeCode
					{
						Args = mailMergeCode.Args,
						Name = mailMergeCode.Name,
						OriginalCode = mailMergeCode.OriginalCode,
						ValueFormat = mailMergeCode.ValueFormat
					};
					mailMergeCode2.SetMailMergeValueDirectly(mailMergeCode.GetMailMergeValuesDirectly());
					list2.Add(mailMergeCode2);
				}
				IList<MailMergeCode> source = this.MailMergingManager.LookupCodeValues(contextWithCustomDictionary, list2);
				list.Add(source.ToList<MailMergeCode>());
			}
			return this.OutputFileMailingLabels(mailMergeCodesWithTemplate.Template.Document, list, OutputFileFormat);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000034C4 File Offset: 0x000016C4
		public BinaryFile GenerateDocumentFromPrintCodes(IList<DocumentPrintItem> PrintItems, string FileName, eFileFormat OutputFormat)
		{
			return this._mailMergingDocDao.GenerateDocumentFromPrintCodes(PrintItems, FileName, OutputFormat);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000034E4 File Offset: 0x000016E4
		public BinaryFile AutoMailMergeAccommodationLetter(AccommodationLetterGenerateContext GenerateContext)
		{
			int templateId = this.FigureOutWhichTemplateToUse(GenerateContext);
			eAccommodationLetterGenerationOutputType outputType = GenerateContext.OutputType;
			eAccommodationLetterGenerationOutputType eAccommodationLetterGenerationOutputType = outputType;
			eFileFormat outputFileFormat;
			if (eAccommodationLetterGenerationOutputType != eAccommodationLetterGenerationOutputType.Html)
			{
				outputFileFormat = eFileFormat.PDF;
			}
			else
			{
				outputFileFormat = eFileFormat.Html;
			}
			List<int> luCourseIds = GenerateContext.LuCourseIds.ToList<int>();
			MailMergeContextWithCustomDictionary contextWithCustomDictionary = new MailMergeContextWithCustomDictionary
			{
				Context = new MailMergeContext
				{
					PersonId = GenerateContext.StudentPersonId,
					LuCourseIds = luCourseIds
				},
				CustomDictionary = new MailMergeCustomDictionary
				{
					Args = new Dictionary<string, string>()
				}
			};
			BinaryFile binaryFile = this.MailMergeAccommodationLetter(GenerateContext.LuCourseIds, contextWithCustomDictionary, outputFileFormat, templateId);
			bool flag = binaryFile == null;
			BinaryFile result;
			if (flag)
			{
				result = null;
			}
			else
			{
				OperationContext opContext = new OperationContext
				{
					WhoAmI = this.OpContext.WhoAmI
				};
				ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(opContext);
				switch (GenerateContext.WhoGeneratingFor)
				{
				case eAccommodationLetterGenerationForWhom.ForStudent:
				{
					courseRegistrationManager.SetStudentLastViewedLetters(GenerateContext.StudentPersonId, GenerateContext.LuCourseIds, new DateTime?(DateTime.Now));
					ILookupCourseManager lookupCourseManager = new LookupCourseManager(this.OpContext);
					IList<LookupCourse> source = lookupCourseManager.LoadCoursesByIds(luCourseIds);
					string plainTextMessage = "Student downloaded accommodation letter for: " + string.Join(", ", (from g in source
					select g.GetCourseDescription()).ToArray<string>());
					IPointOfContactManager pointOfContactManager = new PointOfContactManager(this.OpContext);
					pointOfContactManager.CreatePointOfContactFromMessage(ePointOfContactContext.AutomaticSystemCreated, GenerateContext.StudentPersonId, plainTextMessage);
					break;
				}
				case eAccommodationLetterGenerationForWhom.ForInstructor:
					courseRegistrationManager.SetProfLastViewedLetters(GenerateContext.StudentPersonId, GenerateContext.LuCourseIds, new DateTime?(DateTime.Now));
					break;
				case eAccommodationLetterGenerationForWhom.ForStaff:
				{
					IDynamicDataManager dynamicDataManager = new DynamicDataManager(opContext);
					dynamicDataManager.StoreFileInDocuments("Accommodations letter " + DateTime.Now.ToString("yyyy-MM-dd H:_mm"), "", binaryFile, GenerateContext.StudentPersonId, 1000);
					break;
				}
				}
				result = binaryFile;
			}
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000036D0 File Offset: 0x000018D0
		private int GetFileListCidToStoreSignatureButtonDocumentIn(int overrideFileListCid, int[] fileListCidsOnLocalForm)
		{
			bool flag = overrideFileListCid > 0;
			int result;
			if (flag)
			{
				result = overrideFileListCid;
			}
			else
			{
				IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
				int settingValue_Int = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_DocumentsControlId);
				bool flag2 = settingValue_Int > 0;
				if (flag2)
				{
					result = settingValue_Int;
				}
				else
				{
					bool flag3 = fileListCidsOnLocalForm != null && fileListCidsOnLocalForm.Length != 0;
					if (flag3)
					{
						result = fileListCidsOnLocalForm[0];
					}
					else
					{
						result = 0;
					}
				}
			}
			return result;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003738 File Offset: 0x00001938
		public int MailMergeAndStoreSignatureButtonFileInDocuments(int StudentPersonId, MailMergeCustomDictionary CustomArgs, int TemplateId, eFileFormat OutputFormat, int OverrideFileListCid, string Title, IDictionary<int, string> ModifiedPerStudentFileLists, int[] FileListCidsOnLocalForm, out int[] FileListCidsFileWasStoredIn)
		{
			int fileListCidToStoreSignatureButtonDocumentIn = this.GetFileListCidToStoreSignatureButtonDocumentIn(OverrideFileListCid, FileListCidsOnLocalForm);
			bool flag = fileListCidToStoreSignatureButtonDocumentIn < 1;
			if (flag)
			{
				throw new InvalidParameterException("Can't find a file list to store the merged document on.");
			}
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
			DynamicField field = (fileListCidToStoreSignatureButtonDocumentIn > 0) ? dynamicFieldManager.LoadFieldByControlId(fileListCidToStoreSignatureButtonDocumentIn) : null;
			FileListCidsFileWasStoredIn = new int[]
			{
				fileListCidToStoreSignatureButtonDocumentIn
			};
			MailMergeContextWithCustomDictionary contextWithCustomDictionary = new MailMergeContextWithCustomDictionary
			{
				Context = new MailMergeContext
				{
					PersonId = StudentPersonId
				},
				CustomDictionary = (CustomArgs ?? new MailMergeCustomDictionary())
			};
			BinaryFile binaryFile = this.MailMerge(contextWithCustomDictionary, OutputFormat, TemplateId);
			binaryFile.FileName = (Title ?? "") + Path.GetExtension(binaryFile.FileName);
			string text = (fileListCidToStoreSignatureButtonDocumentIn > 0 && ModifiedPerStudentFileLists != null && ModifiedPerStudentFileLists.ContainsKey(fileListCidToStoreSignatureButtonDocumentIn)) ? ModifiedPerStudentFileLists[fileListCidToStoreSignatureButtonDocumentIn] : null;
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
			bool flag2 = text != null;
			if (flag2)
			{
				dynamicDataManager.SaveData(new DynamicDataContext
				{
					PrimaryId = StudentPersonId
				}, new List<DynamicData>
				{
					new DynamicData
					{
						Field = field,
						Value = text
					}
				}, eDynamicFormType.PerStudent);
			}
			return dynamicDataManager.StoreFileInDocuments(Title, "", binaryFile, StudentPersonId, fileListCidToStoreSignatureButtonDocumentIn);
		}

		// Token: 0x04000004 RID: 4
		private readonly IMailMergingDocDAO _mailMergingDocDao;

		// Token: 0x04000005 RID: 5
		private readonly ITemplateManager _templateManager;

		// Token: 0x04000006 RID: 6
		private MailMergingDocManager _mdm;

		// Token: 0x04000007 RID: 7
		private MailMergingManager _mm;

		// Token: 0x02000005 RID: 5
		internal class TemplateFileInfo
		{
			// Token: 0x0600002D RID: 45 RVA: 0x0000279B File Offset: 0x0000099B
			public TemplateFileInfo()
			{
			}

			// Token: 0x0600002E RID: 46 RVA: 0x00003874 File Offset: 0x00001A74
			public TemplateFileInfo(BinaryFile bf)
			{
				bool flag = bf == null;
				if (!flag)
				{
					this.FileBytes = bf.ByteArray;
					this.FileName = bf.FileName;
					this.FileType = (string.IsNullOrWhiteSpace(bf.FileName) ? eAllowedExtensionGroup.Unknown : bf.FileName.GetAllowedExtensionGroupForFilename());
					AllowedExtensionGroupAttribute attribute = this.FileType.GetAttribute<AllowedExtensionGroupAttribute>();
					string text = (attribute != null) ? attribute.ClockWorkLicenseKey : null;
					bool flag2 = !string.IsNullOrWhiteSpace(text);
					if (flag2)
					{
						LicensingManager licensingManager = new LicensingManager();
						DateTime? dateTime;
						ProductLicenseState productState = licensingManager.GetProductState(text, out dateTime);
						this.IsLicensed = (productState == ProductLicenseState.Licensed);
					}
					else
					{
						this.IsLicensed = true;
					}
				}
			}

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600002F RID: 47 RVA: 0x00003928 File Offset: 0x00001B28
			// (set) Token: 0x06000030 RID: 48 RVA: 0x00003930 File Offset: 0x00001B30
			public byte[] FileBytes { get; set; }

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000031 RID: 49 RVA: 0x00003939 File Offset: 0x00001B39
			// (set) Token: 0x06000032 RID: 50 RVA: 0x00003941 File Offset: 0x00001B41
			public string FileName { get; set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000033 RID: 51 RVA: 0x0000394A File Offset: 0x00001B4A
			// (set) Token: 0x06000034 RID: 52 RVA: 0x00003952 File Offset: 0x00001B52
			public eAllowedExtensionGroup FileType { get; set; }

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000035 RID: 53 RVA: 0x0000395B File Offset: 0x00001B5B
			// (set) Token: 0x06000036 RID: 54 RVA: 0x00003963 File Offset: 0x00001B63
			public bool IsLicensed { get; set; }
		}
	}
}
