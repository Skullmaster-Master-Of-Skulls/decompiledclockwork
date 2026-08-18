using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using ClockWorkLogger;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.Core.Templates;
using TechnoPro.Common.DAO.Entity.Accommodations;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.ICore.MailMerging;
using TechnoPro.Common.ICore.Templates;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities.DataTableMailMerging;
using TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Output;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.Templates;
using TechnoPro.Common.Public.Entities.TPMailMan;
using TechnoPro.Common.TextFormat.Adapters;

namespace TechnoPro.Common.Core.MailMerging
{
	// Token: 0x020000C9 RID: 201
	public class MailMergingEmailManager : IMailMergingEmailManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x00029654 File Offset: 0x00027854
		private ITemplateManager templateManager
		{
			get
			{
				ITemplateManager result;
				if ((result = this.tm) == null)
				{
					result = (this.tm = new TemplateManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x00029680 File Offset: 0x00027880
		private MailMergingManager mailMergingManager
		{
			get
			{
				MailMergingManager result;
				if ((result = this.mm) == null)
				{
					result = (this.mm = new MailMergingManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x000296AC File Offset: 0x000278AC
		private MailMergingManager _mailMergingManager
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

		// Token: 0x0600072F RID: 1839 RVA: 0x000296D7 File Offset: 0x000278D7
		public MailMergingEmailManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x000296E9 File Offset: 0x000278E9
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x000296F1 File Offset: 0x000278F1
		public OperationContext OpContext { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x000296FC File Offset: 0x000278FC
		private AccommodationsDAO accommodationsDao
		{
			get
			{
				AccommodationsDAO result;
				if ((result = this.adao) == null)
				{
					result = (this.adao = new AccommodationsDAO(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00029728 File Offset: 0x00027928
		public static MailMergeContextWithCustomDictionary GetMailMergeContextWithCustomDictionaryFromDataRow(DataRow dr, IList<string> colNames)
		{
			MailMergeContext context = new MailMergeContext();
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			MailMergeContextWithCustomDictionary result = new MailMergeContextWithCustomDictionary
			{
				Context = context,
				CustomDictionary = new MailMergeCustomDictionary
				{
					Args = dictionary
				}
			};
			foreach (string text in colNames)
			{
				MailMergingEmailManager.UpdateContextFromDataRow(ref context, dr, text);
				bool flag = !dictionary.ContainsKey(text);
				if (flag)
				{
					dictionary.Add(text, (dr[text] is DBNull) ? "" : dr[text].ToString());
				}
			}
			return result;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x000297EC File Offset: 0x000279EC
		private static int GetIntFromDataRowCell(DataRow dr, string colName)
		{
			bool flag = dr[colName] is DBNull;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				object obj = dr[colName];
				bool flag2 = obj is int;
				if (flag2)
				{
					result = (int)obj;
				}
				else
				{
					string s = obj.ToString();
					int num;
					result = (int.TryParse(s, out num) ? num : 0);
				}
			}
			return result;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00029850 File Offset: 0x00027A50
		private static void UpdateContextFromDataRow(ref MailMergeContext context, DataRow dr, string colName)
		{
			string text = colName.ToLower().Trim();
			string text2 = text;
			string text3 = text2;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text3);
			if (num <= 2309556255U)
			{
				if (num <= 1170819617U)
				{
					if (num != 142317981U)
					{
						if (num != 541136106U)
						{
							if (num != 1170819617U)
							{
								return;
							}
							if (!(text3 == "appid"))
							{
								return;
							}
						}
						else
						{
							if (!(text3 == "lucourseid"))
							{
								return;
							}
							context.LuCourseId = Math.Max(MailMergingEmailManager.GetIntFromDataRowCell(dr, colName), context.LuCourseId);
							return;
						}
					}
					else
					{
						if (!(text3 == "perdateid"))
						{
							return;
						}
						goto IL_2BC;
					}
				}
				else if (num != 1718176901U)
				{
					if (num != 2205048873U)
					{
						if (num != 2309556255U)
						{
							return;
						}
						if (!(text3 == "examid"))
						{
							return;
						}
						context.ExamId = Math.Max(MailMergingEmailManager.GetIntFromDataRowCell(dr, colName), context.ExamId);
						return;
					}
					else
					{
						if (!(text3 == "infopcid"))
						{
							return;
						}
						goto IL_2BC;
					}
				}
				else
				{
					if (!(text3 == "catalogid"))
					{
						return;
					}
					context.CatalogId = Math.Max(MailMergingEmailManager.GetIntFromDataRowCell(dr, colName), context.CatalogId);
					return;
				}
			}
			else if (num <= 3555319223U)
			{
				if (num != 2729553814U)
				{
					if (num != 2939753504U)
					{
						if (num != 3555319223U)
						{
							return;
						}
						if (!(text3 == "appointmentid"))
						{
							return;
						}
					}
					else
					{
						if (!(text3 == "infoid"))
						{
							return;
						}
						goto IL_2BC;
					}
				}
				else
				{
					if (!(text3 == "loanid"))
					{
						return;
					}
					context.LoanId = Math.Max(MailMergingEmailManager.GetIntFromDataRowCell(dr, colName), context.LoanId);
					return;
				}
			}
			else if (num != 3882419735U)
			{
				if (num != 4097006500U)
				{
					if (num != 4098703229U)
					{
						return;
					}
					if (!(text3 == "personid"))
					{
						return;
					}
					context.PersonId = Math.Max(MailMergingEmailManager.GetIntFromDataRowCell(dr, colName), context.PersonId);
					return;
				}
				else
				{
					if (!(text3 == "serviceproviderid"))
					{
						return;
					}
					context.ServiceProviderId = Math.Max(MailMergingEmailManager.GetIntFromDataRowCell(dr, colName), context.ServiceProviderId);
					return;
				}
			}
			else
			{
				if (!(text3 == "instructorid"))
				{
					return;
				}
				context.InstructorId = Math.Max(MailMergingEmailManager.GetIntFromDataRowCell(dr, colName), context.InstructorId);
				return;
			}
			context.AppointmentId = Math.Max(MailMergingEmailManager.GetIntFromDataRowCell(dr, colName), context.AppointmentId);
			return;
			IL_2BC:
			context.PerDateId = Math.Max(MailMergingEmailManager.GetIntFromDataRowCell(dr, colName), context.PerDateId);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00029B38 File Offset: 0x00027D38
		public MailMergeCodesWithTemplate ExtractUniqueCodes(string TemplateXml)
		{
			TPMailMessage tpmailMessage = TemplateXml.EmailFromXml();
			bool flag = tpmailMessage == null;
			if (flag)
			{
				tpmailMessage = new TPMailMessage();
			}
			List<MailMergeCode> codes = new List<MailMergeCode>();
			Regex regex = new Regex("#~[^#~]*~#");
			this.ExtractUniqueCodes(tpmailMessage.Body ?? "", regex, ref codes);
			this.ExtractUniqueCodes(tpmailMessage.BodyHtml ?? "", regex, ref codes);
			this.ExtractUniqueCodes((tpmailMessage.From == null) ? "" : (tpmailMessage.From.EmailAddress ?? ""), regex, ref codes);
			this.ExtractUniqueCodes(tpmailMessage.Subject ?? "", regex, ref codes);
			this.ExtractUniqueCodes(tpmailMessage.To, regex, ref codes);
			this.ExtractUniqueCodes(tpmailMessage.Cc, regex, ref codes);
			this.ExtractUniqueCodes(tpmailMessage.Bcc, regex, ref codes);
			bool flag2 = tpmailMessage.Attachments != null && tpmailMessage.Attachments.Count > 0;
			if (flag2)
			{
				foreach (TPMailAttachment tpmailAttachment in tpmailMessage.Attachments)
				{
					bool flag3 = !string.IsNullOrEmpty((tpmailAttachment != null) ? tpmailAttachment.FileNameForDisplay : null);
					if (flag3)
					{
						this.ExtractUniqueCodes(tpmailAttachment.FileNameForDisplay, regex, ref codes);
					}
				}
			}
			return new MailMergeCodesWithTemplate
			{
				Codes = codes,
				Template = new Template
				{
					EmailTemplate = tpmailMessage
				}
			};
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00029CCC File Offset: 0x00027ECC
		private void ExtractUniqueCodes(IList<TPMailAddress> addresses, Regex regex, ref List<MailMergeCode> codes)
		{
			bool flag = addresses == null;
			if (!flag)
			{
				foreach (TPMailAddress tpmailAddress in addresses)
				{
					bool flag2 = !string.IsNullOrEmpty((tpmailAddress != null) ? tpmailAddress.EmailAddress : null);
					if (flag2)
					{
						this.ExtractUniqueCodes(tpmailAddress.EmailAddress, regex, ref codes);
					}
				}
			}
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00029D44 File Offset: 0x00027F44
		private void ExtractUniqueCodes(string s, Regex regex, ref List<MailMergeCode> codes)
		{
			bool flag = string.IsNullOrEmpty(s);
			if (!flag)
			{
				string text = s.DecodeHtml();
				bool flag2 = text.IndexOf("#~", StringComparison.Ordinal) < 0 && text.IndexOf("#<", StringComparison.Ordinal) >= 0 && text.IndexOf(">#", StringComparison.Ordinal) >= 0;
				if (flag2)
				{
					text = Regex.Replace(text, "#<(?'code'[^#<]*)>#", "#~${code}~#");
				}
				MatchCollection mc = regex.Matches(text);
				IList<MailMergeCode> collection = this._mailMergingManager.ExtractUniqueCodes(this._mailMergingManager.ConvertMatchCollectionToStringList(mc), null);
				codes.AddRange(collection);
			}
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00029DDC File Offset: 0x00027FDC
		public MailMergeCodesWithTemplate ExtractUniqueCodes(int TemplateId)
		{
			Template template = this.templateManager.LoadTemplate(TemplateId, true);
			bool flag = template.EmailTemplate != null;
			MailMergeCodesWithTemplate mailMergeCodesWithTemplate;
			if (flag)
			{
				string templateXml = template.EmailTemplate.ToEmailXml();
				mailMergeCodesWithTemplate = this.ExtractUniqueCodes(templateXml);
			}
			else
			{
				mailMergeCodesWithTemplate = new MailMergeCodesWithTemplate
				{
					Codes = new List<MailMergeCode>(),
					Template = null
				};
			}
			bool flag2 = (mailMergeCodesWithTemplate.Codes == null || mailMergeCodesWithTemplate.Codes.Count < 1) && template.EmailBehindDocumentTemplate != null;
			if (flag2)
			{
				string templateXml2 = template.EmailBehindDocumentTemplate.ToEmailXml();
				mailMergeCodesWithTemplate = this.ExtractUniqueCodes(templateXml2);
			}
			bool flag3 = mailMergeCodesWithTemplate.Template != null;
			if (flag3)
			{
				mailMergeCodesWithTemplate.Template.TemplateId = TemplateId;
			}
			mailMergeCodesWithTemplate.Template = template;
			return mailMergeCodesWithTemplate;
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00029EA4 File Offset: 0x000280A4
		public MailMergeCodesWithTemplate ExtractUniqueCodes(Setting WebSettingEmailXmlTemplate)
		{
			SettingManager settingManager = new SettingManager(this.OpContext);
			string settingValue = settingManager.GetSettingValue<string>(WebSettingEmailXmlTemplate);
			MailMergeCodesWithTemplate mailMergeCodesWithTemplate = this.ExtractUniqueCodes(settingValue);
			mailMergeCodesWithTemplate.Template = new Template
			{
				TemplateType = eTemplateType.EmailTemplate,
				EmailTemplate = settingValue.EmailFromXml()
			};
			return mailMergeCodesWithTemplate;
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00029EF4 File Offset: 0x000280F4
		public TPMailMessage OutputFile(MailMergeCodesWithTemplate EmailCodes)
		{
			bool flag = EmailCodes.Template == null;
			TPMailMessage tpmailMessage;
			if (flag)
			{
				tpmailMessage = new TPMailMessage();
			}
			else
			{
				bool flag2 = EmailCodes.Template.EmailBehindDocumentTemplate != null;
				if (flag2)
				{
					tpmailMessage = EmailCodes.Template.EmailBehindDocumentTemplate.Clone();
				}
				else
				{
					bool flag3 = EmailCodes.Template.EmailTemplate != null;
					if (flag3)
					{
						tpmailMessage = EmailCodes.Template.EmailTemplate.Clone();
					}
					else
					{
						tpmailMessage = new TPMailMessage();
					}
				}
			}
			return this.OutputFile(EmailCodes, tpmailMessage.BodyType != eEmailBodyType.Html);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00029F80 File Offset: 0x00028180
		public TPMailMessage OutputFile(MailMergeCodesWithTemplate EmailCodes, bool isPlainText)
		{
			bool flag = EmailCodes.Template == null;
			TPMailMessage tpmailMessage;
			if (flag)
			{
				tpmailMessage = new TPMailMessage();
			}
			else
			{
				bool flag2 = EmailCodes.Template.EmailBehindDocumentTemplate != null;
				if (flag2)
				{
					tpmailMessage = EmailCodes.Template.EmailBehindDocumentTemplate.Clone();
				}
				else
				{
					bool flag3 = EmailCodes.Template.EmailTemplate != null;
					if (flag3)
					{
						tpmailMessage = EmailCodes.Template.EmailTemplate.Clone();
					}
					else
					{
						tpmailMessage = new TPMailMessage();
					}
				}
			}
			bool flag4 = this.EmailHasXmlMailMergeCodes(tpmailMessage);
			string str = flag4 ? "#~" : "#<";
			string str2 = flag4 ? "~#" : ">#";
			TempCache tempCache = new TempCache();
			foreach (MailMergeCode mailMergeCode in EmailCodes.Codes)
			{
				string mailMergeValue = this.GetMailMergeValue(mailMergeCode, tempCache, isPlainText);
				string oldVal = str + mailMergeCode.OriginalCode + str2;
				this.ReplaceTextInEmail(ref tpmailMessage, oldVal, mailMergeValue);
			}
			tpmailMessage.Subject = (tpmailMessage.Subject ?? "").DecodeHtml();
			tpmailMessage.Body = (tpmailMessage.Body ?? "").DecodeHtml();
			return tpmailMessage;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0002A0D4 File Offset: 0x000282D4
		private bool EmailHasXmlMailMergeCodes(TPMailMessage email)
		{
			bool flag = email == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = MailMergingEmailManager.StringHasXmlMailMergeCodes(email.Body);
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = MailMergingEmailManager.StringHasXmlMailMergeCodes(email.BodyHtml);
					if (flag3)
					{
						result = true;
					}
					else
					{
						bool flag4 = MailMergingEmailManager.StringHasXmlMailMergeCodes(email.Subject);
						if (flag4)
						{
							result = true;
						}
						else
						{
							bool flag5 = email.From != null && MailMergingEmailManager.StringHasXmlMailMergeCodes(email.From.EmailAddress);
							if (flag5)
							{
								result = true;
							}
							else
							{
								bool flag6 = this.AddressListHasXmlMailMergeCodes(email.To);
								if (flag6)
								{
									result = true;
								}
								else
								{
									bool flag7 = this.AddressListHasXmlMailMergeCodes(email.Cc);
									if (flag7)
									{
										result = true;
									}
									else
									{
										bool flag8 = this.AddressListHasXmlMailMergeCodes(email.Bcc);
										result = flag8;
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0002A1A0 File Offset: 0x000283A0
		private bool AddressListHasXmlMailMergeCodes(IList<TPMailAddress> addresses)
		{
			object obj;
			if (addresses == null)
			{
				obj = null;
			}
			else
			{
				obj = addresses.FirstOrDefault((TPMailAddress g) => MailMergingEmailManager.StringHasXmlMailMergeCodes(g.EmailAddress));
			}
			return obj != null;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0002A1E0 File Offset: 0x000283E0
		private static bool StringHasXmlMailMergeCodes(string s)
		{
			return !string.IsNullOrEmpty(s) && s.IndexOf("#~") >= 0;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0002A210 File Offset: 0x00028410
		private void ReplaceTextInEmail(ref TPMailMessage email, string oldVal, string newVal)
		{
			TPMailAddress from = email.From;
			bool flag = !string.IsNullOrEmpty((from != null) ? from.EmailAddress : null);
			if (flag)
			{
				email.From.EmailAddress = email.From.EmailAddress.Replace(oldVal, newVal);
			}
			this.ReplaceTextInAddressList(email.To, oldVal, newVal);
			this.ReplaceTextInAddressList(email.Cc, oldVal, newVal);
			this.ReplaceTextInAddressList(email.Bcc, oldVal, newVal);
			bool flag2 = !string.IsNullOrEmpty(email.Subject);
			if (flag2)
			{
				email.Subject = email.Subject.Replace(oldVal, newVal);
			}
			bool flag3 = !string.IsNullOrEmpty(email.Body);
			if (flag3)
			{
				email.Body = email.Body.Replace(oldVal, newVal);
			}
			bool flag4 = !string.IsNullOrEmpty(email.BodyHtml);
			if (flag4)
			{
				email.BodyHtml = email.BodyHtml.Replace(oldVal, newVal);
			}
			bool flag5 = email.Attachments != null;
			if (flag5)
			{
				foreach (TPMailAttachment tpmailAttachment in email.Attachments)
				{
					bool flag6 = !string.IsNullOrEmpty((tpmailAttachment != null) ? tpmailAttachment.FileNameForDisplay : null);
					if (flag6)
					{
						tpmailAttachment.FileNameForDisplay = tpmailAttachment.FileNameForDisplay.Replace(oldVal, newVal);
					}
				}
			}
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0002A390 File Offset: 0x00028590
		private void ReplaceTextInAddressList(IList<TPMailAddress> addresses, string oldVal, string newVal)
		{
			bool flag = addresses == null;
			if (!flag)
			{
				foreach (TPMailAddress tpmailAddress in addresses)
				{
					bool flag2 = !string.IsNullOrEmpty((tpmailAddress != null) ? tpmailAddress.EmailAddress : null);
					if (flag2)
					{
						tpmailAddress.EmailAddress = tpmailAddress.EmailAddress.Replace(oldVal, newVal);
					}
				}
			}
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0002A40C File Offset: 0x0002860C
		private string GetMailMergeValue(MailMergeCode code, TempCache tempCache, bool isPlainText)
		{
			bool flag = !isPlainText;
			bool flag2 = code == null || code.MailMergeValueIsNull;
			string result;
			if (flag2)
			{
				result = "";
			}
			else
			{
				bool mailMergeValueIsNull = code.MailMergeValueIsNull;
				string str;
				if (mailMergeValueIsNull)
				{
					str = (code.DefaultValue ?? string.Empty);
				}
				else
				{
					bool flag3 = code.IsOfType<MailMergeValueDynamicData>();
					if (flag3)
					{
						IList<DynamicData> mailMergeValues = code.GetMailMergeValues<MailMergeValueDynamicData, DynamicData>(null);
						bool flag4 = mailMergeValues != null && (mailMergeValues[0].Field.ControlCode == eControlCode.Label || mailMergeValues[0].Field.ControlCode == eControlCode.Picture);
						if (flag4)
						{
							str = "";
						}
						else
						{
							string text = "";
							bool flag5 = code.ValueFormat != null;
							if (flag5)
							{
								bool flag6 = mailMergeValues == null || mailMergeValues.Count == 1;
								if (flag6)
								{
									text = mailMergeValues[0].GetString();
								}
								else
								{
									StringBuilder stringBuilder = new StringBuilder();
									eValueFormatType valueFormatType = code.ValueFormat.ValueFormatType;
									eValueFormatType eValueFormatType = valueFormatType;
									if (eValueFormatType != eValueFormatType.CommaSeparatedList)
									{
										bool flag7 = code.ValueFormat.ValueFormatType == eValueFormatType.DefaultToStringFormat && mailMergeValues.Count == 1;
										string str2;
										if (flag7)
										{
											str2 = "";
										}
										else
										{
											str2 = "* ";
										}
										foreach (DynamicData dynamicData in mailMergeValues)
										{
											bool flag8 = stringBuilder.Length > 0;
											if (flag8)
											{
												stringBuilder.Append("\r\n");
											}
											bool flag9 = dynamicData.Field.ControlCode == eControlCode.RtfTextBox;
											if (flag9)
											{
												dynamicData.Value = dynamicData.Value.ToString().ConvertRtfToPlainText();
											}
											stringBuilder.Append(str2 + dynamicData.GetStringWithCaption());
										}
										text = stringBuilder.ToString();
									}
									else
									{
										foreach (DynamicData dynamicData2 in mailMergeValues)
										{
											bool flag10 = stringBuilder.Length > 0;
											if (flag10)
											{
												stringBuilder.Append(", ");
											}
											bool flag11 = dynamicData2.Field.ControlCode == eControlCode.RtfTextBox;
											if (flag11)
											{
												dynamicData2.Value = dynamicData2.Value.ToString().ConvertRtfToPlainText();
											}
											stringBuilder.Append(dynamicData2.GetStringWithCaption());
										}
										text = stringBuilder.ToString();
									}
								}
							}
							str = text;
						}
					}
					else
					{
						bool flag12 = code.IsOfType<MailMergeValueBool>();
						if (flag12)
						{
							str = (code.GetFirstMailMergeValue<MailMergeValueBool, bool>(false) ? "Yes" : "No");
						}
						else
						{
							bool flag13 = code.IsOfType<MailMergeValueDateTime>() || code.IsOfType<MailMergeValueDateTimeNullable>();
							if (flag13)
							{
								bool flag14 = code.IsOfType<MailMergeValueDateTime>();
								DateTime d;
								if (flag14)
								{
									d = code.GetFirstMailMergeValue<MailMergeValueDateTime, DateTime>(DateTime.MinValue);
								}
								else
								{
									d = (code.GetFirstMailMergeValue<MailMergeValueDateTimeNullable, DateTime?>(null) ?? DateTime.MinValue);
								}
								bool flag15 = d != DateTime.MinValue;
								if (flag15)
								{
									string text2 = null;
									MailMergeValueFormat valueFormat = code.ValueFormat;
									bool flag16 = !string.IsNullOrEmpty((valueFormat != null) ? valueFormat.CustomFormat : null);
									if (flag16)
									{
										try
										{
											text2 = d.ToString(code.ValueFormat.CustomFormat);
										}
										catch (Exception ex)
										{
											CWLogger.Logger.Error("Can't format date string: {0}:{1}", code.ValueFormat.CustomFormat, ex.ToString());
										}
									}
									str = (text2 ?? d.ToString("MMMM d, yyyy"));
								}
								else
								{
									str = string.Empty;
								}
							}
							else
							{
								bool flag17 = code.IsOfType<MailMergeValueAccommodationData>();
								if (flag17)
								{
									IList<AccommodationData> mailMergeValues2 = code.GetMailMergeValues<MailMergeValueAccommodationData, AccommodationData>(null);
									bool flag18 = mailMergeValues2 != null;
									if (flag18)
									{
										string mailMergeCode = code.Name.ToLower().Trim();
										bool flag19 = code.ValueFormat != null && code.ValueFormat.ValueFormatType == eValueFormatType.NumberedList;
										AccommodationListFormattingInfoDAO formattingInfo;
										string listCounterName;
										if (flag19)
										{
											formattingInfo = new AccommodationListFormattingInfoDAO
											{
												itemFooter = "",
												itemHeader = "",
												itemNewline = (flag ? "<br />" : "\r\n"),
												itemPre = "{ctr}. ",
												itemPost = "",
												emptyListString = "None."
											};
											listCounterName = code.ValueFormat.CustomFormat;
										}
										else
										{
											bool flag20 = flag;
											if (flag20)
											{
												formattingInfo = new AccommodationListFormattingInfoDAO
												{
													itemFooter = "</ul>",
													itemHeader = "<ul>",
													itemNewline = "",
													itemPre = "<li>",
													itemPost = "</li>",
													emptyListString = "None."
												};
												listCounterName = null;
											}
											else
											{
												formattingInfo = new AccommodationListFormattingInfoDAO
												{
													itemFooter = "",
													itemHeader = "",
													itemNewline = "\r\n",
													itemPre = "* ",
													itemPost = "",
													emptyListString = "None."
												};
												listCounterName = null;
											}
										}
										str = this.accommodationsDao.GetAccommodationsListString(mailMergeValues2.ToList<AccommodationData>(), mailMergeCode, formattingInfo, tempCache, listCounterName);
									}
									else
									{
										str = "";
									}
								}
								else
								{
									bool flag21 = code.IsOfType<MailMergeValueString>();
									if (flag21)
									{
										bool flag22 = code.IsValueAList();
										if (flag22)
										{
											IList<string> mailMergeValues3 = code.GetMailMergeValues<MailMergeValueString, string>(string.Empty);
											bool flag23 = mailMergeValues3 != null;
											if (flag23)
											{
												string text3 = "";
												bool flag24 = code.ValueFormat != null;
												if (flag24)
												{
													eValueFormatType valueFormatType2 = code.ValueFormat.ValueFormatType;
													eValueFormatType eValueFormatType2 = valueFormatType2;
													if (eValueFormatType2 != eValueFormatType.BulletedList)
													{
														text3 = string.Join(", ", mailMergeValues3.ToArray<string>());
													}
													else
													{
														text3 = string.Join("\r\n", mailMergeValues3.ToArray<string>());
													}
												}
												str = text3;
											}
											else
											{
												str = string.Empty;
											}
										}
										else
										{
											str = code.GetFirstMailMergeValue<MailMergeValueString, string>(string.Empty);
										}
									}
									else
									{
										str = code.GetFirstMailMergeValue<MailMergeValueString, string>(string.Empty);
									}
								}
							}
						}
					}
				}
				result = SecurityElement.Escape(str);
			}
			return result;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0002AA5C File Offset: 0x00028C5C
		public TPMailMessage MailMerge(MailMergeContextWithCustomDictionary ContextWithCustomDictionary, string TemplateXml)
		{
			MailMergeCodesWithTemplate mailMergeCodesWithTemplate = this.ExtractUniqueCodes(TemplateXml);
			IList<MailMergeCode> source = this.mailMergingManager.LookupCodeValues(ContextWithCustomDictionary, mailMergeCodesWithTemplate.Codes);
			mailMergeCodesWithTemplate.Codes = source.ToList<MailMergeCode>();
			return this.OutputFile(mailMergeCodesWithTemplate);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0002AAA0 File Offset: 0x00028CA0
		public TPMailMessage MailMerge(MailMergeContextWithCustomDictionary ContextWithCustomDictionary, int TemplateId)
		{
			MailMergeCodesWithTemplate mailMergeCodesWithTemplate = this.ExtractUniqueCodes(TemplateId);
			IList<MailMergeCode> source = this.mailMergingManager.LookupCodeValues(ContextWithCustomDictionary, mailMergeCodesWithTemplate.Codes);
			mailMergeCodesWithTemplate.Codes = source.ToList<MailMergeCode>();
			return this.OutputFile(mailMergeCodesWithTemplate);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0002AAE4 File Offset: 0x00028CE4
		public TPMailMessage MailMerge(MailMergeContextWithCustomDictionary ContextWithCustomDictionary, Setting WebSettingEmailXmlTemplate)
		{
			SettingManager settingManager = new SettingManager(this.OpContext);
			string settingValue = settingManager.GetSettingValue<string>(WebSettingEmailXmlTemplate);
			MailMergeCodesWithTemplate mailMergeCodesWithTemplate = this.ExtractUniqueCodes(settingValue);
			IList<MailMergeCode> source = this.mailMergingManager.LookupCodeValues(ContextWithCustomDictionary, mailMergeCodesWithTemplate.Codes);
			mailMergeCodesWithTemplate.Codes = source.ToList<MailMergeCode>();
			return this.OutputFile(mailMergeCodesWithTemplate);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0002AB3C File Offset: 0x00028D3C
		public IDictionary<int, TPMailMessage> MailMergeAccommodationLetterCoursesEmail(IList<int> LuCourseIds, MailMergeContextWithCustomDictionary ContextWithCustomDictionary, int TemplateId)
		{
			Dictionary<int, TPMailMessage> dictionary = new Dictionary<int, TPMailMessage>();
			MailMergeCodesWithTemplate mailMergeCodesWithTemplate = this.ExtractUniqueCodes(TemplateId);
			List<List<MailMergeCode>> list = new List<List<MailMergeCode>>();
			foreach (int num in LuCourseIds)
			{
				MailMergeCodesWithTemplate mailMergeCodesWithTemplate2 = mailMergeCodesWithTemplate.Clone();
				bool flag = !dictionary.ContainsKey(num);
				if (flag)
				{
					List<MailMergeCode> list2 = new List<MailMergeCode>();
					foreach (MailMergeCode mailMergeCode in mailMergeCodesWithTemplate2.Codes)
					{
						MailMergeCode mailMergeCode2 = new MailMergeCode(mailMergeCode);
						mailMergeCode2.SetMailMergeValueDirectly(mailMergeCode.GetMailMergeValuesDirectly());
						list2.Add(mailMergeCode2);
					}
					ContextWithCustomDictionary.Context.LuCourseId = num;
					IList<MailMergeCode> source = this.mailMergingManager.LookupCodeValues(ContextWithCustomDictionary, list2);
					mailMergeCodesWithTemplate2.Codes = source.ToList<MailMergeCode>();
					TPMailMessage value = this.OutputFile(mailMergeCodesWithTemplate2);
					dictionary.Add(num, value);
				}
			}
			return dictionary;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0002AC70 File Offset: 0x00028E70
		public TPMailMessage MailMergeAccommodationSingleLetterEmail(IList<int> LuCourseIds, MailMergeContextWithCustomDictionary ContextWithCustomDictionary, int TemplateId)
		{
			MailMergeCodesWithTemplate mailMergeCodesWithTemplate = this.ExtractUniqueCodes(TemplateId);
			IList<MailMergeCode> source = this.mailMergingManager.LookupCodeValues(ContextWithCustomDictionary, mailMergeCodesWithTemplate.Codes);
			mailMergeCodesWithTemplate.Codes = source.ToList<MailMergeCode>();
			return this.OutputFile(mailMergeCodesWithTemplate);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0002ACB4 File Offset: 0x00028EB4
		public IDictionary<MailMergeContext, TPMailMessage> MailMerge(IList<MailMergeContextWithCustomDictionary> ContextsWithCustomDictionaries, string TemplateXml)
		{
			Dictionary<MailMergeContext, TPMailMessage> dictionary = new Dictionary<MailMergeContext, TPMailMessage>();
			foreach (MailMergeContextWithCustomDictionary mailMergeContextWithCustomDictionary in ContextsWithCustomDictionaries)
			{
				TPMailMessage value = this.MailMerge(mailMergeContextWithCustomDictionary, TemplateXml);
				bool flag = !dictionary.ContainsKey(mailMergeContextWithCustomDictionary.Context);
				if (flag)
				{
					dictionary.Add(mailMergeContextWithCustomDictionary.Context, value);
				}
			}
			return dictionary;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0002AD38 File Offset: 0x00028F38
		public IDictionary<MailMergeContext, TPMailMessage> MailMerge(IList<MailMergeContextWithCustomDictionary> ContextsWithCustomDictionaries, int TemplateId)
		{
			Dictionary<MailMergeContext, TPMailMessage> dictionary = new Dictionary<MailMergeContext, TPMailMessage>();
			foreach (MailMergeContextWithCustomDictionary mailMergeContextWithCustomDictionary in ContextsWithCustomDictionaries)
			{
				TPMailMessage value = this.MailMerge(mailMergeContextWithCustomDictionary, TemplateId);
				bool flag = !dictionary.ContainsKey(mailMergeContextWithCustomDictionary.Context);
				if (flag)
				{
					dictionary.Add(mailMergeContextWithCustomDictionary.Context, value);
				}
			}
			return dictionary;
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0002ADB8 File Offset: 0x00028FB8
		public IDictionary<MailMergeContext, TPMailMessage> MailMerge(IList<MailMergeContextWithCustomDictionary> ContextsWithCustomDictionaries, Setting WebSettingExmailXmlTemplate)
		{
			Dictionary<MailMergeContext, TPMailMessage> dictionary = new Dictionary<MailMergeContext, TPMailMessage>();
			foreach (MailMergeContextWithCustomDictionary mailMergeContextWithCustomDictionary in ContextsWithCustomDictionaries)
			{
				TPMailMessage value = this.MailMerge(mailMergeContextWithCustomDictionary, WebSettingExmailXmlTemplate);
				bool flag = !dictionary.ContainsKey(mailMergeContextWithCustomDictionary.Context);
				if (flag)
				{
					dictionary.Add(mailMergeContextWithCustomDictionary.Context, value);
				}
			}
			return dictionary;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0002AE38 File Offset: 0x00029038
		public IList<MailMergedEmailWithOriginalRowAndDictionary> MailMergeAndReturnOriginalDataRows(DataTable t, string TemplateXml)
		{
			List<string> colNames = (from DataColumn dc in t.Columns
			select dc.ColumnName).ToList<string>();
			List<MailMergedEmailWithOriginalRowAndDictionary> list = new List<MailMergedEmailWithOriginalRowAndDictionary>();
			foreach (object obj in t.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				try
				{
					MailMergeContextWithCustomDictionary mailMergeContextWithCustomDictionaryFromDataRow = MailMergingEmailManager.GetMailMergeContextWithCustomDictionaryFromDataRow(dataRow, colNames);
					TPMailMessage mergedEmail = this.MailMerge(mailMergeContextWithCustomDictionaryFromDataRow, TemplateXml);
					list.Add(new MailMergedEmailWithOriginalRowAndDictionary
					{
						ContextWithCustomDictionary = mailMergeContextWithCustomDictionaryFromDataRow,
						MergedEmail = mergedEmail,
						OriginalRows = new DataRow[]
						{
							dataRow
						}
					});
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Error("Common.Core.MailMerging.MailMergingEmailManager:MailMergeAndReturnOriginalDataRows:Collect:err={0}", ex.ToString());
				}
			}
			return list;
		}

		// Token: 0x04000157 RID: 343
		private ITemplateManager tm;

		// Token: 0x04000158 RID: 344
		private MailMergingManager mm;

		// Token: 0x04000159 RID: 345
		private MailMergingManager _mm;

		// Token: 0x0400015B RID: 347
		private AccommodationsDAO adao;
	}
}
