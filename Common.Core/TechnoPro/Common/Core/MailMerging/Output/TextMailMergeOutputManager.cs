using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClockWorkLogger;
using TechnoPro.Common.Core.Templates;
using TechnoPro.Common.ICore.MailMerging.Output;
using TechnoPro.Common.ICore.Templates;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Output;
using TechnoPro.Common.Public.Entities.Templates;

namespace TechnoPro.Common.Core.MailMerging.Output
{
	// Token: 0x020000CF RID: 207
	public class TextMailMergeOutputManager : IMailMergeOutputManager, IBaseOperationContext<MailMergeOutputOperationContext>
	{
		// Token: 0x060007BB RID: 1979 RVA: 0x0003646D File Offset: 0x0003466D
		public TextMailMergeOutputManager(MailMergeOutputOperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0003647F File Offset: 0x0003467F
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x00036487 File Offset: 0x00034687
		public MailMergeOutputOperationContext OpContext { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x00036490 File Offset: 0x00034690
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x00036498 File Offset: 0x00034698
		protected IList<StringBuilder> TextDocuments { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x000364A1 File Offset: 0x000346A1
		// (set) Token: 0x060007C1 RID: 1985 RVA: 0x000364A9 File Offset: 0x000346A9
		protected StringBuilder CurrentTextDocument { get; set; }

		// Token: 0x060007C2 RID: 1986 RVA: 0x000364B4 File Offset: 0x000346B4
		public object OutputMailMergeCodes()
		{
			this.InitializeDocument();
			TempCache tempCache = new TempCache();
			for (int i = 0; i < this.OpContext.CodeLists.Count; i++)
			{
				IList<MailMergeCode> list = this.OpContext.CodeLists[i];
				bool flag = i > 0;
				if (flag)
				{
					this.OutputPageBreak();
				}
				tempCache.ClearNonGlobalItems();
				foreach (MailMergeCode mailMergeCode in list)
				{
					MailMergeValueFormat mailMergeValueFormat = mailMergeCode.ValueFormat;
					bool flag2 = mailMergeValueFormat == null;
					if (flag2)
					{
						mailMergeValueFormat = new MailMergeValueFormat
						{
							ValueFormatType = eValueFormatType.DefaultToStringFormat,
							CustomFormat = ""
						};
					}
					else
					{
						bool flag3 = mailMergeValueFormat.CustomFormat == null;
						if (flag3)
						{
							mailMergeValueFormat.CustomFormat = "";
						}
					}
					this.OutputData(mailMergeCode, mailMergeValueFormat, tempCache);
				}
			}
			return this.CloseDocument();
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x000365C8 File Offset: 0x000347C8
		protected virtual void OutputData(MailMergeCode code, MailMergeValueFormat valueFormat, TempCache tempCache)
		{
			bool flag = valueFormat != null && valueFormat.ValueFormatType == eValueFormatType.InsertedMailMergeDocument;
			if (flag)
			{
				string customFormat = valueFormat.CustomFormat;
				int num;
				bool flag2 = customFormat.Length < 1 || !int.TryParse(customFormat, out num);
				if (flag2)
				{
					num = 0;
				}
				bool flag3 = num > 0;
				if (flag3)
				{
					ITemplateManager templateManager = new TemplateManager(this.OpContext);
					Template template = templateManager.LoadTemplate(num, true);
					bool flag4 = template != null && template.Document != null && template.Document.ByteArray != null;
					if (flag4)
					{
						try
						{
							this.OutputString(code, Encoding.UTF8.GetString(template.Document.ByteArray), null);
						}
						catch (Exception ex)
						{
							CWLogger.Logger.Error("Common.Core.MailMerging.Output.TextMailMergeOutputManager:OutputData:InsertedMailMergeDocument:err={0}", ex.ToString());
							this.OutputNull(code, null);
						}
					}
				}
			}
			else
			{
				bool mailMergeValueIsNull = code.MailMergeValueIsNull;
				if (mailMergeValueIsNull)
				{
					bool flag5 = code.DefaultValue == null;
					if (flag5)
					{
						this.OutputNull(code, valueFormat);
					}
					else
					{
						this.OutputString(code, code.DefaultValue, valueFormat);
					}
				}
				else
				{
					bool flag6 = code.IsOfType<MailMergeValueString>();
					if (flag6)
					{
						bool flag7 = code.IsValueAList();
						if (flag7)
						{
							this.OutputStringList(code, code.GetMailMergeValues<MailMergeValueString, string>(""), tempCache, valueFormat);
						}
						else
						{
							this.OutputString(code, code.GetFirstMailMergeValue<MailMergeValueString, string>(""), valueFormat);
						}
					}
					else
					{
						bool flag8 = code.IsOfType<MailMergeValueBool>();
						if (flag8)
						{
							this.OutputBoolean(code, code.GetFirstMailMergeValue<MailMergeValueBool, bool>(false), valueFormat);
						}
						else
						{
							bool flag9 = code.IsOfType<MailMergeValueInt>();
							if (flag9)
							{
								this.OutputInt(code, code.GetFirstMailMergeValue<MailMergeValueInt, int>(0), valueFormat);
							}
							else
							{
								bool flag10 = code.IsOfType<MailMergeValueDateTime>();
								if (flag10)
								{
									this.OutputDateTime(code, code.GetFirstMailMergeValue<MailMergeValueDateTime, DateTime>(DateTime.MinValue), valueFormat);
								}
								else
								{
									bool flag11 = code.IsOfType<MailMergeValueDateTimeNullable>();
									if (flag11)
									{
										DateTime? firstMailMergeValue = code.GetFirstMailMergeValue<MailMergeValueDateTimeNullable, DateTime?>(null);
										bool flag12 = firstMailMergeValue != null;
										if (flag12)
										{
											this.OutputDateTime(code, firstMailMergeValue.Value, valueFormat);
										}
									}
									else
									{
										bool flag13 = code.IsOfType<MailMergeValueDynamicData>();
										if (flag13)
										{
											IList<DynamicData> mailMergeValues = code.GetMailMergeValues<MailMergeValueDynamicData, DynamicData>(null);
											bool flag14 = mailMergeValues.Count < 1;
											if (flag14)
											{
												this.OutputString(code, "", null);
											}
											else
											{
												bool flag15 = mailMergeValues[0].Value is byte[] && (mailMergeValues[0].Field.ControlCode == eControlCode.Label || mailMergeValues[0].Field.ControlCode == eControlCode.Picture);
												if (flag15)
												{
													this.OutputImage(code, (byte[])mailMergeValues[0].Value, valueFormat);
												}
												else
												{
													this.OutputDataList(code, mailMergeValues, tempCache, valueFormat);
												}
											}
										}
										else
										{
											bool flag16 = code.IsOfType<MailMergeValueAccommodationData>();
											if (flag16)
											{
												IList<AccommodationData> mailMergeValues2 = code.GetMailMergeValues<MailMergeValueAccommodationData, AccommodationData>(null);
												bool flag17 = mailMergeValues2.Count < 1;
												if (flag17)
												{
													this.OutputString(code, (code.Args.ContainsKey("nonone") && code.Args["nonone"] == "1") ? "" : "none", null);
												}
												else
												{
													DynamicData data = mailMergeValues2[0].Data;
													bool flag18 = data.Value != null && data.Value is byte[] && (data.Field.ControlCode == eControlCode.Label || data.Field.ControlCode == eControlCode.Picture);
													if (flag18)
													{
														this.OutputImage(code, (byte[])data.Value, valueFormat);
													}
													else
													{
														this.OutputDataList(code, mailMergeValues2.Select(delegate(AccommodationData g)
														{
															string text = (g.Detail == null || g.Detail.LongDescription == null) ? "" : g.Detail.LongDescription.Trim();
															bool flag19 = text.Length > 0;
															if (flag19)
															{
																g.Data.Field.ControlCaption = text;
															}
															return g.Data;
														}).ToList<DynamicData>(), tempCache, valueFormat);
													}
												}
											}
											else
											{
												this.OutputString(code, "", valueFormat);
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x000369D0 File Offset: 0x00034BD0
		protected virtual void InitializeDocument()
		{
			this.TextDocuments = new List<StringBuilder>();
			this.CurrentTextDocument = new StringBuilder(string.Copy(this.OpContext.Template.Template));
			this.TextDocuments.Add(this.CurrentTextDocument);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00036A20 File Offset: 0x00034C20
		protected virtual object CloseDocument()
		{
			return this.TextDocuments.ToList<StringBuilder>().ConvertAll<string>((StringBuilder i) => i.ToString());
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00036A61 File Offset: 0x00034C61
		protected virtual void OutputPageBreak()
		{
			this.CurrentTextDocument = new StringBuilder(string.Copy(this.OpContext.Template.Template));
			this.TextDocuments.Add(this.CurrentTextDocument);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00036A98 File Offset: 0x00034C98
		private string GetNullString()
		{
			return "";
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00036AAF File Offset: 0x00034CAF
		protected virtual void OutputNull(MailMergeCode code, MailMergeValueFormat valueFormat)
		{
			this.OutputString(code, this.GetNullString(), valueFormat);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00036AC4 File Offset: 0x00034CC4
		protected virtual void OutputString(MailMergeCode code, string text, MailMergeValueFormat valueFormat = null)
		{
			bool flag = valueFormat == null;
			if (flag)
			{
				valueFormat = MailMergeValueFormat.DefaultMailMergeValueFormat;
			}
			this.CurrentTextDocument.Replace(string.Format("#<{0}>#", code.OriginalCode), text);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00036AFE File Offset: 0x00034CFE
		protected virtual void OutputImage(MailMergeCode code, byte[] imageData, MailMergeValueFormat valueFormat)
		{
			this.OutputNull(code, valueFormat);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00036B0C File Offset: 0x00034D0C
		protected virtual void OutputDataList(MailMergeCode code, IList<DynamicData> list0, TempCache tempCache, MailMergeValueFormat valueFormat = null)
		{
			List<string> list = new List<string>();
			StringBuilder currentTextDocument = this.CurrentTextDocument;
			List<DynamicData> list2 = list0.ToList<DynamicData>();
			list2.Sort((DynamicData g1, DynamicData g2) => g1.Field.ControlId.CompareTo(g2.Field.ControlId));
			int j;
			for (int i = 0; i < list2.Count; i = j)
			{
				DynamicData dynamicData = list2[i];
				for (j = i + 1; j < list2.Count; j++)
				{
					DynamicData dynamicData2 = list2[j];
					bool flag = dynamicData2.Field.ControlId != dynamicData.Field.ControlId;
					if (flag)
					{
						break;
					}
				}
				this.CurrentTextDocument = new StringBuilder("#<" + code.OriginalCode + ">#");
				bool flag2 = j == i + 1;
				if (flag2)
				{
					this.OutputDataItem(code, dynamicData, valueFormat);
				}
				else
				{
					List<DynamicData> list3 = new List<DynamicData>();
					for (int k = i; k < j; k++)
					{
						list3.Add(list2[k]);
					}
					string dataItemPreFormattedOutput = this.GetDataItemPreFormattedOutput((from g in list3
					where g != null
					select g).ToArray<DynamicData>());
					this.OutputString(code, dataItemPreFormattedOutput, valueFormat);
				}
				list.Add(this.CurrentTextDocument.ToString());
			}
			this.CurrentTextDocument = currentTextDocument;
			this.OutputStringList(code, list, tempCache, valueFormat);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00036C9C File Offset: 0x00034E9C
		private string GetDataItemPreFormattedOutput(params DynamicData[] dataItems)
		{
			bool flag = dataItems == null || dataItems.Length < 1 || dataItems[0] == null;
			string result;
			if (flag)
			{
				result = this.GetNullString();
			}
			else
			{
				eControlCode controlCode = dataItems[0].Field.ControlCode;
				bool flag2 = controlCode == eControlCode.CheckBox || controlCode == eControlCode.MyCheckBox || controlCode == eControlCode.AccommodationCheckBox;
				if (flag2)
				{
					result = (dataItems[0].Field.ControlCaption ?? "");
				}
				else
				{
					bool flag3 = controlCode == eControlCode.MultiCheckBoxText || controlCode == eControlCode.MultiCheckBoxDropList;
					if (flag3)
					{
						DynamicData dynamicData = dataItems.FirstOrDefault((DynamicData g) => g.Value != null && !(g.Value is int));
						bool flag4 = dynamicData != null;
						if (flag4)
						{
							result = dynamicData.GetStringWithCaption();
						}
						else
						{
							result = dataItems[0].Field.GetCaptionForDisplay() + ": " + string.Join(" ", (from g in dataItems
							select g.GetString()).ToArray<string>());
						}
					}
					else
					{
						result = dataItems[0].GetStringWithCaption();
					}
				}
			}
			return result;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00036DC2 File Offset: 0x00034FC2
		protected virtual void OutputDataItem(MailMergeCode code, DynamicData dataItem, MailMergeValueFormat valueFormat = null)
		{
			this.OutputString(code, this.GetDataItemPreFormattedOutput(new DynamicData[]
			{
				dataItem
			}), valueFormat);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00036DE0 File Offset: 0x00034FE0
		protected virtual void OutputStringList(MailMergeCode code, IList<string> list, TempCache tempCache, MailMergeValueFormat valueFormat = null)
		{
			bool flag = valueFormat == null;
			if (flag)
			{
				valueFormat = MailMergeValueFormat.DefaultMailMergeValueFormat;
			}
			list = (from f in list
			where f != null && f.Trim().Length > 0
			select f).ToList<string>();
			eValueFormatType valueFormatType = valueFormat.ValueFormatType;
			eValueFormatType eValueFormatType = valueFormatType;
			if (eValueFormatType != eValueFormatType.BulletedList)
			{
				if (eValueFormatType != eValueFormatType.NumberedList)
				{
					this.OutputString(code, string.Join(", ", list.ToArray<string>()), null);
				}
				else
				{
					bool flag2 = !string.IsNullOrEmpty(code.ValueFormat.CustomFormat);
					TempCacheObject tempCacheObject;
					if (flag2)
					{
						tempCacheObject = (tempCache.ContainsKey(code.ValueFormat.CustomFormat) ? tempCache[code.ValueFormat.CustomFormat] : tempCache.AddLocalItem(code.ValueFormat.CustomFormat, 1));
					}
					else
					{
						tempCacheObject = null;
					}
					int ctr = (tempCacheObject == null) ? 1 : ((int)tempCacheObject.Object);
					string text = string.Join("\r\n", list.Select(delegate(string g)
					{
						int ctr = ctr;
						ctr++;
						return ctr.ToString() + ". " + g;
					}).ToArray<string>());
					bool flag3 = tempCacheObject != null;
					if (flag3)
					{
						tempCacheObject.Object = ctr;
					}
					this.OutputString(code, text, null);
				}
			}
			else
			{
				this.OutputString(code, string.Join(Environment.NewLine, list.ToList<string>().ConvertAll<string>((string f) => "* " + f).ToArray()), null);
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00036F6D File Offset: 0x0003516D
		protected virtual void OutputInt(MailMergeCode code, int intVal, MailMergeValueFormat valueFormat)
		{
			this.OutputString(code, intVal.ToString(), null);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00036F80 File Offset: 0x00035180
		protected virtual void OutputBoolean(MailMergeCode code, bool boolVal, MailMergeValueFormat valueFormat)
		{
			eValueFormatType valueFormatType = valueFormat.ValueFormatType;
			eValueFormatType eValueFormatType = valueFormatType;
			if (eValueFormatType != eValueFormatType.BooleanYesNo)
			{
				if (eValueFormatType != eValueFormatType.BooleanTrueFalse)
				{
					this.OutputString(code, boolVal.ToString(), null);
				}
				else
				{
					this.OutputString(code, boolVal ? "True" : "False", null);
				}
			}
			else
			{
				this.OutputString(code, boolVal ? "Yes" : "No", null);
			}
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00036FEC File Offset: 0x000351EC
		protected virtual void OutputDateTime(MailMergeCode code, DateTime dateTime, MailMergeValueFormat valueFormat)
		{
			switch (valueFormat.ValueFormatType)
			{
			case eValueFormatType.CustomFormat:
				this.OutputString(code, dateTime.ToString(valueFormat.CustomFormat), null);
				break;
			case eValueFormatType.DateSmall:
				this.OutputString(code, dateTime.ToShortDateString(), null);
				break;
			case eValueFormatType.DateLarge:
				this.OutputString(code, dateTime.ToLongDateString(), null);
				break;
			case eValueFormatType.TimeAmPm:
				this.OutputString(code, dateTime.ToString("h:mm tt"), null);
				break;
			case eValueFormatType.TimeMilitary:
				this.OutputString(code, dateTime.ToString("H:mm"), null);
				break;
			default:
				this.OutputString(code, dateTime.ToString(), null);
				break;
			}
		}
	}
}
