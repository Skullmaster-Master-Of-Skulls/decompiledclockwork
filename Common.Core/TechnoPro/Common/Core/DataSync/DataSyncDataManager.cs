using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.Reports;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.DAO.DataSync;
using TechnoPro.Common.DAO.Impl.DataSync;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.DataSync.DataSyncData;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;
using TechnoPro.Common.TextFormat.Adapters;

namespace TechnoPro.Common.Core.DataSync
{
	// Token: 0x0200010F RID: 271
	public class DataSyncDataManager : IDataSyncDataManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000B1C RID: 2844 RVA: 0x00048FD0 File Offset: 0x000471D0
		public DataSyncDataManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00048FE2 File Offset: 0x000471E2
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x00048FEA File Offset: 0x000471EA
		public OperationContext OpContext { get; set; }

		// Token: 0x06000B1F RID: 2847 RVA: 0x00048FF4 File Offset: 0x000471F4
		private bool SaveClockWorkData(int pid, DataSyncDataItemBase item, DataSyncMapperItemBase mapper, DataSyncDataWorkingData workingData)
		{
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
			DynamicField dynamicField = dynamicFieldManager.LoadFieldByControlId(mapper.ControlId);
			object value = null;
			bool flag = true;
			eControlCode controlCode = dynamicField.ControlCode;
			bool flag2 = controlCode == eControlCode.DropList && dynamicField.Setting3 != 0;
			if (flag2)
			{
			}
			eControlCode controlCode2 = dynamicField.ControlCode;
			eControlCode eControlCode = controlCode2;
			if (eControlCode <= eControlCode.RadioGroup)
			{
				switch (eControlCode)
				{
				case eControlCode.TextBox:
				{
					DataSyncDataItemString dataSyncDataItemString = item.ConvertTo<DataSyncDataItemString>();
					bool flag3 = string.IsNullOrEmpty(dataSyncDataItemString.Text);
					if (flag3)
					{
						flag = false;
					}
					else
					{
						value = dataSyncDataItemString.Text;
					}
					goto IL_399;
				}
				case eControlCode.CheckBox:
				{
					DataSyncDataItemBool dataSyncDataItemBool = item.ConvertTo<DataSyncDataItemBool>();
					bool flag4 = dataSyncDataItemBool.Checked == null || !dataSyncDataItemBool.Checked.Value;
					if (flag4)
					{
						flag = false;
					}
					else
					{
						value = dataSyncDataItemBool.Checked.Value;
					}
					goto IL_399;
				}
				case eControlCode.DropList:
				{
					DataSyncDataItemString dataSyncDataItemString2 = item.ConvertTo<DataSyncDataItemString>();
					string dropListText = (dataSyncDataItemString2.Text ?? "").Trim();
					bool flag5 = dropListText.Length < 1;
					if (flag5)
					{
						flag = false;
					}
					else
					{
						List<DynamicListItem> list = workingData.LookupLists.ContainsKey(dynamicField.Setting1) ? workingData.LookupLists[dynamicField.Setting1] : null;
						bool flag6 = list != null;
						if (flag6)
						{
							DynamicListItem dynamicListItem = list.FirstOrDefault((DynamicListItem g) => (g.LookupText ?? "").Equals(dropListText, StringComparison.OrdinalIgnoreCase));
							bool flag7 = dynamicListItem != null;
							if (flag7)
							{
								value = dynamicListItem.LookupListId;
							}
							else
							{
								flag = false;
							}
						}
						else
						{
							flag = false;
						}
					}
					goto IL_399;
				}
				case eControlCode.RadioButton:
				case eControlCode.Label:
					break;
				case eControlCode.Date:
				{
					DataSyncDataItemDateTime dataSyncDataItemDateTime = item.ConvertTo<DataSyncDataItemDateTime>();
					bool flag8 = dataSyncDataItemDateTime.DateTimeValue == null || dataSyncDataItemDateTime.DateTimeValue.Value == DateTime.MinValue;
					if (flag8)
					{
						flag = false;
					}
					else
					{
						value = dataSyncDataItemDateTime.DateTimeValue.Value;
					}
					goto IL_399;
				}
				default:
					if (eControlCode == eControlCode.RadioGroup)
					{
						DataSyncDataItemString dataSyncDataItemString3 = item.ConvertTo<DataSyncDataItemString>();
						string itemRadioText = (dataSyncDataItemString3.Text ?? "").Trim();
						bool flag9 = itemRadioText.Length < 1;
						if (flag9)
						{
							flag = false;
						}
						else
						{
							List<DynamicListItem> list2 = workingData.LookupLists.ContainsKey(dynamicField.Setting1) ? workingData.LookupLists[dynamicField.Setting1] : null;
							bool flag10 = list2 != null;
							if (flag10)
							{
								DynamicListItem dynamicListItem2 = list2.FirstOrDefault((DynamicListItem g) => (g.LookupText ?? "").Equals(itemRadioText, StringComparison.OrdinalIgnoreCase));
								bool flag11 = dynamicListItem2 != null;
								if (flag11)
								{
									value = dynamicListItem2.LookupListId;
								}
								else
								{
									flag = false;
								}
							}
							else
							{
								flag = false;
							}
						}
						goto IL_399;
					}
					break;
				}
			}
			else
			{
				if (eControlCode == eControlCode.Picture)
				{
					DataSyncDataItemBinaryData dataSyncDataItemBinaryData = item.ConvertTo<DataSyncDataItemBinaryData>();
					bool flag12 = dataSyncDataItemBinaryData.BinaryData == null;
					if (flag12)
					{
						flag = false;
					}
					else
					{
						value = dataSyncDataItemBinaryData.BinaryData;
					}
					goto IL_399;
				}
				if (eControlCode == eControlCode.TableControl)
				{
					DataSyncDataItemString dataSyncDataItemString4 = item.ConvertTo<DataSyncDataItemString>();
					string text = (dataSyncDataItemString4.Text ?? "").Trim();
					bool flag13 = text.Length < 1;
					if (flag13)
					{
						flag = false;
					}
					else
					{
						value = text;
					}
					goto IL_399;
				}
				if (eControlCode == eControlCode.RtfTextBox)
				{
					DataSyncDataItemString dataSyncDataItemString5 = item.ConvertTo<DataSyncDataItemString>();
					string text2 = (dataSyncDataItemString5.Text ?? "").Trim();
					bool flag14 = text2.Length < 1;
					if (flag14)
					{
						flag = false;
					}
					else
					{
						value = text2.ConvertPlainTextToRtf();
					}
					goto IL_399;
				}
			}
			throw new NotImplementedException();
			IL_399:
			bool flag15 = !flag;
			bool result;
			if (flag15)
			{
				result = false;
			}
			else
			{
				DynamicData item2 = new DynamicData
				{
					Field = dynamicField,
					Value = value
				};
				IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
				dynamicDataManager.SaveData(new DynamicDataContext
				{
					PrimaryId = pid
				}, new List<DynamicData>
				{
					item2
				}, eDynamicFormType.PerStudent);
				result = true;
			}
			return result;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x000493F8 File Offset: 0x000475F8
		private IList<DataSyncMapperItemBase> LoadMappings(IList<DataSyncDataMappingBasic> basicMappings)
		{
			bool flag = basicMappings == null;
			IList<DataSyncMapperItemBase> result;
			if (flag)
			{
				result = new List<DataSyncMapperItemBase>();
			}
			else
			{
				List<int> controlIds = (from g in basicMappings
				select g.ControlId).ToList<int>();
				IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
				List<DynamicField> source = dynamicFieldManager.LoadFieldsByControlIds(controlIds);
				List<DataSyncMapperItemBase> list = new List<DataSyncMapperItemBase>();
				using (IEnumerator<DataSyncDataMappingBasic> enumerator = basicMappings.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DataSyncDataMappingBasic basicMapping = enumerator.Current;
						DynamicField dynamicField = source.FirstOrDefault((DynamicField g) => g.ControlId == basicMapping.ControlId);
						bool flag2 = dynamicField == null;
						if (!flag2)
						{
							bool flag3 = dynamicField.ControlCode == eControlCode.TableControl;
							if (flag3)
							{
								List<DataSyncMapperItemBase> list2 = list;
								DataSyncMapperItemList dataSyncMapperItemList = new DataSyncMapperItemList();
								dataSyncMapperItemList.ControlId = basicMapping.ControlId;
								dataSyncMapperItemList.ControlCaption = dynamicField.ControlCaption;
								dataSyncMapperItemList.ControlCode = dynamicField.ControlCode;
								dataSyncMapperItemList.ExternalFieldNames = (from g in basicMapping.ExternalColumnNames.Trim().Split(new char[]
								{
									','
								})
								select g.Trim() into h
								where h.Length > 0
								select h).ToArray<string>();
								list2.Add(dataSyncMapperItemList);
							}
							else
							{
								list.Add(new DataSyncMapperItemStandard
								{
									ControlId = basicMapping.ControlId,
									ControlCaption = dynamicField.ControlCaption,
									ControlCode = dynamicField.ControlCode,
									ExternalFieldName = basicMapping.ExternalColumnNames.Trim()
								});
							}
						}
					}
				}
				DynamicField dynamicField2 = dynamicFieldManager.LoadFieldByName("LastDataSync") ?? dynamicFieldManager.LoadFieldByName("Last Data Sync");
				bool flag4 = dynamicField2 != null;
				if (flag4)
				{
					list.Add(new DataSyncMapperItemStandard
					{
						ControlId = dynamicField2.ControlId,
						ControlCaption = dynamicField2.ControlCaption,
						ControlCode = dynamicField2.ControlCode,
						ExternalFieldName = "now"
					});
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00049674 File Offset: 0x00047874
		private bool CheckForNameChange(BasicPerson clockworkStudent, BasicPerson externalStudent)
		{
			string a = (clockworkStudent.FirstName ?? "").Trim();
			string a2 = (clockworkStudent.LastName ?? "").Trim();
			string a3 = (clockworkStudent.MiddleName ?? "").Trim();
			string text = (externalStudent.FirstName ?? "").Trim();
			string text2 = (externalStudent.LastName ?? "").Trim();
			string b = (externalStudent.MiddleName ?? "").Trim();
			bool flag = !string.IsNullOrEmpty(text) && a != text;
			bool flag2 = !string.IsNullOrEmpty(text2) && a2 != text2;
			bool flag3 = a3 != b;
			int personId = clockworkStudent.PersonId;
			return flag || flag2 || flag3;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0004974C File Offset: 0x0004794C
		private IList<DataSyncDataItemJob> MakeAJobPlan(IList<DataSyncDataLoadedItem> externalDataItems, IList<DataSyncDataLoadedItem> clockWorkDataItems)
		{
			List<DataSyncDataItemJob> list = new List<DataSyncDataItemJob>();
			using (IEnumerator<DataSyncDataLoadedItem> enumerator = externalDataItems.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DataSyncDataLoadedItem externalDataItem = enumerator.Current;
					DataSyncDataItemJob dataSyncDataItemJob = new DataSyncDataItemJob
					{
						DataItem = externalDataItem.DataItem,
						MapperItem = externalDataItem.MapperItem
					};
					bool flag = !externalDataItem.DataItem.HasValue;
					if (flag)
					{
						dataSyncDataItemJob.ChangeAction = eDataSyncDataItemChangeStatus.DoNothing;
						list.Add(dataSyncDataItemJob);
					}
					else
					{
						DataSyncDataLoadedItem dataSyncDataLoadedItem = clockWorkDataItems.FirstOrDefault((DataSyncDataLoadedItem g) => g.MapperItem.ControlId == externalDataItem.MapperItem.ControlId);
						bool flag2 = dataSyncDataLoadedItem == null;
						if (flag2)
						{
							dataSyncDataItemJob.ChangeAction = eDataSyncDataItemChangeStatus.Add;
						}
						else
						{
							dataSyncDataItemJob.ChangeAction = (dataSyncDataLoadedItem.DataItem.Equals(externalDataItem.DataItem) ? eDataSyncDataItemChangeStatus.DoNothing : eDataSyncDataItemChangeStatus.Update);
						}
						list.Add(dataSyncDataItemJob);
					}
				}
			}
			return list;
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0004985C File Offset: 0x00047A5C
		private IList<DataSyncDataItemResult> RunDataSyncActions(int pid, IList<DataSyncDataItemJob> jobs, DataSyncDataWorkingData workingData)
		{
			List<DataSyncDataItemResult> list = new List<DataSyncDataItemResult>();
			foreach (DataSyncDataItemJob dataSyncDataItemJob in jobs)
			{
				switch (dataSyncDataItemJob.ChangeAction)
				{
				case eDataSyncDataItemChangeStatus.DoNothing:
					break;
				case eDataSyncDataItemChangeStatus.Update:
					this.SaveClockWorkData(pid, dataSyncDataItemJob.DataItem, dataSyncDataItemJob.MapperItem, workingData);
					list.Add(new DataSyncDataItemResult
					{
						ResultStatus = eDataSyncDataItemStatus.Successful,
						ChangeStatus = dataSyncDataItemJob.ChangeAction,
						ExternalData = dataSyncDataItemJob.DataItem
					});
					break;
				case eDataSyncDataItemChangeStatus.Delete:
					goto IL_D3;
				case eDataSyncDataItemChangeStatus.Add:
					this.SaveClockWorkData(pid, dataSyncDataItemJob.DataItem, dataSyncDataItemJob.MapperItem, workingData);
					list.Add(new DataSyncDataItemResult
					{
						ResultStatus = eDataSyncDataItemStatus.Successful,
						ChangeStatus = dataSyncDataItemJob.ChangeAction,
						ExternalData = dataSyncDataItemJob.DataItem
					});
					break;
				default:
					goto IL_D3;
				}
				continue;
				IL_D3:
				list.Add(new DataSyncDataItemResult
				{
					ResultStatus = eDataSyncDataItemStatus.Failed,
					ChangeStatus = dataSyncDataItemJob.ChangeAction,
					ExternalData = dataSyncDataItemJob.DataItem,
					ResultMessage = "Not supported"
				});
			}
			return list;
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x000499B4 File Offset: 0x00047BB4
		private DataSyncDataManager.DataSyncDataLoadedItemResult GetClockWorkDataValue(DataSyncMapperItemBase mapping, IDynamicDataSerializableItem dataItem)
		{
			eControlCode? eControlCode;
			if (dataItem == null)
			{
				eControlCode = null;
			}
			else
			{
				DynamicField field = dataItem.Field;
				eControlCode = ((field != null) ? new eControlCode?(field.ControlCode) : null);
			}
			eControlCode? eControlCode2 = eControlCode;
			eControlCode valueOrDefault = eControlCode2.GetValueOrDefault();
			eControlCode eControlCode3 = valueOrDefault;
			eControlCode eControlCode4 = eControlCode3;
			if (eControlCode4 <= eControlCode.RadioGroup)
			{
				switch (eControlCode4)
				{
				case eControlCode.TextBox:
				{
					DynamicDataItemTextbox dynamicDataItemTextbox = dataItem as DynamicDataItemTextbox;
					DataSyncDataManager.DataSyncDataLoadedItemResult result;
					if (dynamicDataItemTextbox != null)
					{
						bool wasSuccessful = true;
						DataSyncDataLoadedItem dataItem2;
						if ((dynamicDataItemTextbox.Value ?? "").Trim().Length <= 0)
						{
							dataItem2 = null;
						}
						else
						{
							DataSyncDataLoadedItem dataSyncDataLoadedItem = new DataSyncDataLoadedItem();
							dataSyncDataLoadedItem.MapperItem = mapping;
							dataItem2 = dataSyncDataLoadedItem;
							dataSyncDataLoadedItem.DataItem = new DataSyncDataItemString(dynamicDataItemTextbox.Value);
						}
						result = new DataSyncDataManager.DataSyncDataLoadedItemResult(wasSuccessful, dataItem2);
					}
					else
					{
						result = new DataSyncDataManager.DataSyncDataLoadedItemResult("Expected type is DynamicDataItemTextbox;Actual type is " + dataItem.GetType().ToString());
					}
					return result;
				}
				case eControlCode.CheckBox:
				{
					DynamicDataItemCheckbox dynamicDataItemCheckbox = dataItem as DynamicDataItemCheckbox;
					DataSyncDataManager.DataSyncDataLoadedItemResult result2;
					if (dynamicDataItemCheckbox != null)
					{
						bool wasSuccessful2 = true;
						DynamicDataItemBool value = dynamicDataItemCheckbox.Value;
						DataSyncDataLoadedItem dataItem3;
						if (value == null || !value.IsChecked)
						{
							dataItem3 = null;
						}
						else
						{
							DataSyncDataLoadedItem dataSyncDataLoadedItem2 = new DataSyncDataLoadedItem();
							dataSyncDataLoadedItem2.MapperItem = mapping;
							dataItem3 = dataSyncDataLoadedItem2;
							dataSyncDataLoadedItem2.DataItem = new DataSyncDataItemBool(new bool?(dynamicDataItemCheckbox.Value.IsChecked));
						}
						result2 = new DataSyncDataManager.DataSyncDataLoadedItemResult(wasSuccessful2, dataItem3);
					}
					else
					{
						result2 = new DataSyncDataManager.DataSyncDataLoadedItemResult("Expected type is DynamicDataItemCheckbox;Actual type is " + dataItem.GetType().ToString());
					}
					return result2;
				}
				case eControlCode.DropList:
				{
					bool flag = dataItem.Field.Setting3 == 0;
					if (flag)
					{
						DynamicDataItemDropListGeneral dynamicDataItemDropListGeneral = dataItem as DynamicDataItemDropListGeneral;
						DataSyncDataManager.DataSyncDataLoadedItemResult result3;
						if (dynamicDataItemDropListGeneral != null)
						{
							bool wasSuccessful3 = true;
							DynamicDataItemListItem value2 = dynamicDataItemDropListGeneral.Value;
							DataSyncDataLoadedItem dataItem4;
							if ((((value2 != null) ? value2.Title : null) ?? "").Trim().Length <= 0)
							{
								dataItem4 = null;
							}
							else
							{
								DataSyncDataLoadedItem dataSyncDataLoadedItem3 = new DataSyncDataLoadedItem();
								dataSyncDataLoadedItem3.MapperItem = mapping;
								dataItem4 = dataSyncDataLoadedItem3;
								dataSyncDataLoadedItem3.DataItem = new DataSyncDataItemString(dynamicDataItemDropListGeneral.Value.Title);
							}
							result3 = new DataSyncDataManager.DataSyncDataLoadedItemResult(wasSuccessful3, dataItem4);
						}
						else
						{
							result3 = new DataSyncDataManager.DataSyncDataLoadedItemResult("Expected type is DynamicDataItemDropListGeneral;Actual type is " + dataItem.GetType().ToString());
						}
						return result3;
					}
					DynamicDataItemTextbox dynamicDataItemTextbox2 = dataItem as DynamicDataItemTextbox;
					DataSyncDataManager.DataSyncDataLoadedItemResult result4;
					if (dynamicDataItemTextbox2 != null)
					{
						bool wasSuccessful4 = true;
						DataSyncDataLoadedItem dataItem5;
						if ((dynamicDataItemTextbox2.Value ?? "").Trim().Length <= 0)
						{
							dataItem5 = null;
						}
						else
						{
							DataSyncDataLoadedItem dataSyncDataLoadedItem4 = new DataSyncDataLoadedItem();
							dataSyncDataLoadedItem4.MapperItem = mapping;
							dataItem5 = dataSyncDataLoadedItem4;
							dataSyncDataLoadedItem4.DataItem = new DataSyncDataItemString(dynamicDataItemTextbox2.Value);
						}
						result4 = new DataSyncDataManager.DataSyncDataLoadedItemResult(wasSuccessful4, dataItem5);
					}
					else
					{
						result4 = new DataSyncDataManager.DataSyncDataLoadedItemResult("Expected type is DynamicDataItemTextbox;Actual type is " + dataItem.GetType().ToString());
					}
					return result4;
				}
				case eControlCode.RadioButton:
				case eControlCode.Label:
					break;
				case eControlCode.Date:
				{
					DynamicDataItemDateValue dynamicDataItemDateValue = dataItem as DynamicDataItemDateValue;
					DataSyncDataManager.DataSyncDataLoadedItemResult result5;
					if (dynamicDataItemDateValue != null)
					{
						bool wasSuccessful5 = true;
						DataSyncDataLoadedItem dataItem6;
						if (dynamicDataItemDateValue.Value == null)
						{
							dataItem6 = null;
						}
						else
						{
							DataSyncDataLoadedItem dataSyncDataLoadedItem5 = new DataSyncDataLoadedItem();
							dataSyncDataLoadedItem5.MapperItem = mapping;
							dataItem6 = dataSyncDataLoadedItem5;
							dataSyncDataLoadedItem5.DataItem = new DataSyncDataItemDateTime(new DateTime?(dynamicDataItemDateValue.Value.Value));
						}
						result5 = new DataSyncDataManager.DataSyncDataLoadedItemResult(wasSuccessful5, dataItem6);
					}
					else
					{
						result5 = new DataSyncDataManager.DataSyncDataLoadedItemResult("Expected type is DynamicDataItemDateValue;Actual type is " + dataItem.GetType().ToString());
					}
					return result5;
				}
				default:
					if (eControlCode4 == eControlCode.RadioGroup)
					{
						DynamicDataItemRadioButtonGroup dynamicDataItemRadioButtonGroup = dataItem as DynamicDataItemRadioButtonGroup;
						DataSyncDataManager.DataSyncDataLoadedItemResult result6;
						if (dynamicDataItemRadioButtonGroup != null)
						{
							bool wasSuccessful6 = true;
							DynamicDataItemListItem value3 = dynamicDataItemRadioButtonGroup.Value;
							DataSyncDataLoadedItem dataItem7;
							if ((((value3 != null) ? value3.Title : null) ?? "").Trim().Length <= 0)
							{
								dataItem7 = null;
							}
							else
							{
								DataSyncDataLoadedItem dataSyncDataLoadedItem6 = new DataSyncDataLoadedItem();
								dataSyncDataLoadedItem6.MapperItem = mapping;
								dataItem7 = dataSyncDataLoadedItem6;
								dataSyncDataLoadedItem6.DataItem = new DataSyncDataItemString(dynamicDataItemRadioButtonGroup.Value.Title);
							}
							result6 = new DataSyncDataManager.DataSyncDataLoadedItemResult(wasSuccessful6, dataItem7);
						}
						else
						{
							result6 = new DataSyncDataManager.DataSyncDataLoadedItemResult("Expected type is DynamicDataItemRadioButtonGroup;Actual type is " + dataItem.GetType().ToString());
						}
						return result6;
					}
					break;
				}
			}
			else
			{
				if (eControlCode4 == eControlCode.Picture)
				{
					DynamicDataItemPicture dynamicDataItemPicture = dataItem as DynamicDataItemPicture;
					DataSyncDataManager.DataSyncDataLoadedItemResult result7;
					if (dynamicDataItemPicture != null)
					{
						bool wasSuccessful7 = true;
						DataSyncDataLoadedItem dataItem8;
						if (dynamicDataItemPicture.Value != null && dynamicDataItemPicture.Value.Length >= 1)
						{
							dataItem8 = null;
						}
						else
						{
							DataSyncDataLoadedItem dataSyncDataLoadedItem7 = new DataSyncDataLoadedItem();
							dataSyncDataLoadedItem7.MapperItem = mapping;
							dataItem8 = dataSyncDataLoadedItem7;
							dataSyncDataLoadedItem7.DataItem = new DataSyncDataItemBinaryData(dynamicDataItemPicture.Value);
						}
						result7 = new DataSyncDataManager.DataSyncDataLoadedItemResult(wasSuccessful7, dataItem8);
					}
					else
					{
						result7 = new DataSyncDataManager.DataSyncDataLoadedItemResult("Expected type is DynamicDataItemPicture;Actual type is " + dataItem.GetType().ToString());
					}
					return result7;
				}
				if (eControlCode4 != eControlCode.TableControl)
				{
					if (eControlCode4 == eControlCode.RtfTextBox)
					{
						DynamicDataItemRichTextbox dynamicDataItemRichTextbox = dataItem as DynamicDataItemRichTextbox;
						string text = (((dynamicDataItemRichTextbox != null) ? dynamicDataItemRichTextbox.Value : null) ?? "").Trim();
						DataSyncDataManager.DataSyncDataLoadedItemResult result8;
						if (dynamicDataItemRichTextbox != null)
						{
							bool wasSuccessful8 = true;
							DataSyncDataLoadedItem dataItem9;
							if (text.Length <= 0)
							{
								dataItem9 = null;
							}
							else
							{
								DataSyncDataLoadedItem dataSyncDataLoadedItem8 = new DataSyncDataLoadedItem();
								dataSyncDataLoadedItem8.MapperItem = mapping;
								dataItem9 = dataSyncDataLoadedItem8;
								dataSyncDataLoadedItem8.DataItem = new DataSyncDataItemString(text.ConvertRtfToPlainText());
							}
							result8 = new DataSyncDataManager.DataSyncDataLoadedItemResult(wasSuccessful8, dataItem9);
						}
						else
						{
							result8 = new DataSyncDataManager.DataSyncDataLoadedItemResult("Expected type is DynamicDataItemRichTextbox;Actual type is " + dataItem.GetType().ToString());
						}
						return result8;
					}
				}
				else
				{
					DynamicDataItemListView dynamicDataItemListView = dataItem as DynamicDataItemListView;
					bool flag2 = dynamicDataItemListView == null;
					if (flag2)
					{
						return new DataSyncDataManager.DataSyncDataLoadedItemResult("Expected type is DynamicDataItemListView;Actual type is " + dataItem.GetType().ToString());
					}
					IList<DynamicDataItemListRow> list = dynamicDataItemListView.Value ?? new List<DynamicDataItemListRow>();
					throw new NotImplementedException();
				}
			}
			return new DataSyncDataManager.DataSyncDataLoadedItemResult("ControlCode not supported: " + valueOrDefault.ToString());
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00049EA8 File Offset: 0x000480A8
		private IList<DataSyncDataLoadedItem> LoadClockWorkItems(int pid, IList<DataSyncMapperItemBase> mappings)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
			IDynamicDataManager dynamicDataManager2 = dynamicDataManager;
			DynamicDataContext dynamicDataContext = new DynamicDataContext();
			dynamicDataContext.PrimaryId = pid;
			IList<IDynamicDataSerializableItem> source = dynamicDataManager2.LoadDynamicDataItemsByControlIds(dynamicDataContext, (from g in mappings
			select g.ControlId).Distinct<int>().ToList<int>(), eDynamicFormType.PerStudent);
			List<DataSyncDataLoadedItem> list = new List<DataSyncDataLoadedItem>();
			using (IEnumerator<DataSyncMapperItemBase> enumerator = mappings.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DataSyncMapperItemBase mapping = enumerator.Current;
					IDynamicDataSerializableItem dynamicDataSerializableItem = source.FirstOrDefault((IDynamicDataSerializableItem g) => g.Field.ControlId == mapping.ControlId);
					bool flag = dynamicDataSerializableItem == null;
					if (!flag)
					{
						DataSyncDataManager.DataSyncDataLoadedItemResult clockWorkDataValue = this.GetClockWorkDataValue(mapping, dynamicDataSerializableItem);
						bool flag2 = !clockWorkDataValue.WasSuccessful;
						if (!flag2)
						{
							bool flag3 = clockWorkDataValue.DataSyncItem != null;
							if (flag3)
							{
								list.Add(clockWorkDataValue.DataSyncItem);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00049FC0 File Offset: 0x000481C0
		private BasicPerson LookupStudent(string snum)
		{
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			PersonBase personBase = peopleManager.LoadPersonByStudentNumber(snum);
			bool flag = personBase == null;
			BasicPerson result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new BasicPerson
				{
					FirstName = personBase.FirstName,
					MiddleName = personBase.MiddleName,
					LastName = personBase.LastName,
					PersonId = personBase.PersonId,
					StudentNumber = personBase.Student_no
				};
			}
			return result;
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0004A038 File Offset: 0x00048238
		private BasicPerson ExtractStudent(DataColumnCollection columns, DataRow dr)
		{
			bool flag = !columns.Contains("student_no") || !columns.Contains("firstname") || !columns.Contains("lastname");
			BasicPerson result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new BasicPerson
				{
					StudentNumber = dr["student_no"].ToString().ToUpper().Trim(),
					FirstName = dr["firstname"].ToString().Trim(),
					MiddleName = (columns.Contains("middlename") ? dr["middlename"].ToString().Trim() : string.Empty),
					LastName = dr["lastname"].ToString().Trim()
				};
			}
			return result;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0004A110 File Offset: 0x00048310
		private IList<DataSyncDataLoadedItem> ExtractExternalData(IList<DataSyncMapperItemBase> mappings, DataColumnCollection columns, IList<DataRow> drs)
		{
			List<DataSyncDataLoadedItem> list = new List<DataSyncDataLoadedItem>();
			DataRow dataRow = drs[0];
			foreach (DataSyncMapperItemBase dataSyncMapperItemBase in mappings)
			{
				bool flag = dataSyncMapperItemBase is DataSyncMapperItemStandard;
				if (flag)
				{
					DataSyncMapperItemStandard dataSyncMapperItemStandard = (DataSyncMapperItemStandard)dataSyncMapperItemBase;
					int num = columns.IndexOf(dataSyncMapperItemStandard.ExternalFieldName ?? "");
					bool flag2 = num >= 0;
					if (flag2)
					{
						Type type = columns[num].DataType;
						object obj = dataRow[num];
						bool flag3 = type != typeof(byte[]);
						if (flag3)
						{
							type = typeof(string);
							obj = ((obj is DBNull) ? null : obj.ToString().Trim());
						}
						DataSyncDataItemBase dataItem = DataSyncDataItemFactory.CreateDataSyncDataItem(type, obj);
						list.Add(new DataSyncDataLoadedItem
						{
							DataItem = dataItem,
							MapperItem = dataSyncMapperItemBase
						});
					}
				}
				else
				{
					bool flag4 = dataSyncMapperItemBase is DataSyncMapperItemList;
					if (flag4)
					{
						DataSyncMapperItemList dataSyncMapperItemList = (DataSyncMapperItemList)dataSyncMapperItemBase;
					}
				}
			}
			return list;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0004A254 File Offset: 0x00048454
		private DataSyncDataWorkingData LoadDataSyncWorkingData()
		{
			IDataSyncInfoManager dataSyncInfoManager = new DataSyncInfoManager(this.OpContext);
			DataSyncInfo dataSyncInfo = dataSyncInfoManager.LoadDataSyncInfo();
			IReportManager reportManager = new ReportManager(this.OpContext);
			Report report = reportManager.LoadReport(dataSyncInfo.ImportStudentDataReportId);
			ReportFunction reportFunction;
			if (report == null)
			{
				reportFunction = null;
			}
			else
			{
				List<ReportFunction> functions = report.Functions;
				if (functions == null)
				{
					reportFunction = null;
				}
				else
				{
					reportFunction = functions.FirstOrDefault((ReportFunction g) => g.FunctionCode == eFunctionType.Import_User_Data);
				}
			}
			ReportFunction reportFunction2 = reportFunction;
			string text = ((reportFunction2 != null) ? reportFunction2.GetDefaultFunctionParameter() : null) ?? "";
			List<DataSyncDataMappingBasic> list = new List<DataSyncDataMappingBasic>();
			bool flag = text.Length > 0;
			if (flag)
			{
				using (StringReader stringReader = new StringReader(text))
				{
					string text2;
					while ((text2 = stringReader.ReadLine()) != null)
					{
						int num = text2.IndexOf('=');
						bool flag2 = num > 0 && num < text2.Length - 1;
						if (flag2)
						{
							string s = text2.Substring(0, num).Trim();
							string text3 = text2.Substring(num + 1).Trim();
							int num2;
							bool flag3 = !int.TryParse(s, out num2);
							if (flag3)
							{
								num2 = 0;
							}
							bool flag4 = num2 > 0 && text3.Length > 0;
							if (flag4)
							{
								list.Add(new DataSyncDataMappingBasic
								{
									ControlId = num2,
									ExternalColumnNames = text3
								});
							}
						}
					}
				}
			}
			IList<DataSyncMapperItemBase> list2 = this.LoadMappings(list);
			List<int> controlIds = (from g in list2
			select g.ControlId).Distinct<int>().ToList<int>();
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
			List<DynamicField> list3 = dynamicFieldManager.LoadFieldsByControlIds(controlIds);
			List<DynamicField> source = (from g in list3
			where g.ControlCode == eControlCode.DropList && g.Setting3 == 0
			select g).ToList<DynamicField>();
			List<int> first = (from g in source
			select g.Setting1).Distinct<int>().ToList<int>();
			IEnumerable<DynamicField> source2 = from g in list3
			where g.ControlCode == eControlCode.RadioGroup
			select g;
			List<int> second = (from g in source2
			select g.Setting1).Distinct<int>().ToList<int>();
			List<int> list4 = first.Concat(second).Distinct<int>().ToList<int>();
			Dictionary<int, List<DynamicListItem>> dictionary = new Dictionary<int, List<DynamicListItem>>();
			foreach (int num3 in list4)
			{
				bool flag5 = num3 < 1 || dictionary.ContainsKey(num3);
				if (!flag5)
				{
					List<DynamicListItem> list5 = dynamicFieldManager.LoadListItems(num3);
					bool flag6 = list5 == null;
					if (!flag6)
					{
						dictionary.Add(num3, list5);
					}
				}
			}
			return new DataSyncDataWorkingData
			{
				ImportUserDataReportId = dataSyncInfo.ImportStudentDataReportId,
				Mappings = list2,
				Fields = list3,
				LookupLists = dictionary
			};
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0004A5B0 File Offset: 0x000487B0
		private DataSyncDataResult DataSyncData(string studentNumber, DataTable t, DataSyncDataWorkingData workingData)
		{
			IList<DataSyncMapperItemBase> mappings = workingData.Mappings;
			string snum = (studentNumber ?? "").Trim().ToUpper();
			bool flag = snum.Length < 1;
			if (flag)
			{
				throw new InvalidParameterIdException("Empty student number");
			}
			List<DataRow> list = (from DataRow dr in t.Rows
			where dr["student_no"].ToString().Trim().ToUpper() == snum
			select dr).ToList<DataRow>();
			bool flag2 = list.Count < 1;
			DataSyncDataResult result;
			if (flag2)
			{
				result = new DataSyncDataResult
				{
					ResultStatus = eDataSyncDataStatus.SuccessfulNoData
				};
			}
			else
			{
				DataColumnCollection columns = t.Columns;
				BasicPerson basicPerson = this.ExtractStudent(columns, list[0]);
				bool flag3 = basicPerson == null;
				if (flag3)
				{
					result = new DataSyncDataResult
					{
						ResultStatus = eDataSyncDataStatus.Failed,
						ResultMessage = "Failed to extract student name and number"
					};
				}
				else
				{
					BasicPerson basicPerson2 = this.LookupStudent(snum);
					bool flag4 = basicPerson2 == null || basicPerson2.PersonId < 1;
					if (flag4)
					{
						result = new DataSyncDataResult
						{
							ResultStatus = eDataSyncDataStatus.FailedStudentNotInClockWork
						};
					}
					else
					{
						int personId = basicPerson2.PersonId;
						bool updatedName = this.CheckForNameChange(basicPerson2, basicPerson);
						bool flag5 = !t.Columns.Contains("now");
						if (flag5)
						{
							t.Columns.Add("now", typeof(DateTime));
							DateTime now = DateTime.Now;
							foreach (object obj in t.Rows)
							{
								DataRow dataRow = (DataRow)obj;
								dataRow["now"] = now;
							}
						}
						IList<DataSyncDataLoadedItem> externalDataItems = this.ExtractExternalData(mappings, columns, list);
						IList<DataSyncDataLoadedItem> clockWorkDataItems = this.LoadClockWorkItems(personId, mappings);
						IList<DataSyncDataItemJob> jobs = this.MakeAJobPlan(externalDataItems, clockWorkDataItems);
						IList<DataSyncDataItemResult> itemResults = this.RunDataSyncActions(personId, jobs, workingData);
						result = new DataSyncDataResult
						{
							ResultStatus = eDataSyncDataStatus.Successful,
							ExternalStudentInfo = basicPerson,
							UpdatedName = updatedName,
							ItemResults = itemResults
						};
					}
				}
			}
			return result;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0004A7D8 File Offset: 0x000489D8
		public void DataSyncDataLegacy(string studentNumber)
		{
			this.DataSyncIntakeData(studentNumber, true);
			IDataSyncInfoManager dataSyncInfoManager = new DataSyncInfoManager(this.OpContext);
			DataSyncInfo dataSyncInfo = dataSyncInfoManager.LoadDataSyncInfo();
			int importStudentDataReportId = dataSyncInfo.ImportStudentDataReportId;
			bool flag = importStudentDataReportId < 1;
			if (!flag)
			{
				IReportManager reportManager = new ReportManager(this.OpContext);
				ReportParameter[] parameters = new ReportParameter[]
				{
					new ReportParameter
					{
						Name = "studentno",
						Value = studentNumber
					},
					new ReportParameter
					{
						Name = "student_no",
						Value = studentNumber
					}
				}.ToArray<ReportParameter>();
				reportManager.ExecuteReport2(importStudentDataReportId, parameters);
			}
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0004A870 File Offset: 0x00048A70
		public void DataSyncIntakeData(string studentNumber, bool deleteIntakeEntry = true)
		{
			IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			int settingValue = webSettingManager.GetSettingValue<int>(Setting.INTAKE_FormNum);
			bool flag = settingValue > 0;
			if (flag)
			{
				CWLogger.Logger.Info("DataSyncDataManager.DataSyncIntakeData:Executing import-intake-data");
				try
				{
					IPeopleManager peopleManager = new PeopleManager(this.OpContext);
					PersonBase personBase = (studentNumber.Length > 0) ? peopleManager.LoadPersonByStudentNumber(studentNumber) : null;
					bool flag2 = personBase != null && personBase.PersonId > 0;
					if (flag2)
					{
						IDataSyncInfoDAO dataSyncInfoDAO = new DataSyncInfoDAO(this.OpContext);
						dataSyncInfoDAO.DataSyncIntakeData(personBase.PersonId, personBase.Student_no, settingValue, deleteIntakeEntry);
					}
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Error("DataSyncDataManager.DataSyncIntakeData:ImportIntakeData:Error={0}", ex.ToString());
				}
			}
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0004A948 File Offset: 0x00048B48
		public DataSyncDataResult DataSyncData(string studentNumber, DataTable t)
		{
			DataSyncDataWorkingData workingData = this.LoadDataSyncWorkingData();
			return this.DataSyncData(studentNumber, t, workingData);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0004A96C File Offset: 0x00048B6C
		public BatchDataSyncDataResult DataSyncBatchDataAndCourses(IList<string> studentNumbers)
		{
			DataSyncDataWorkingData dataSyncDataWorkingData = this.LoadDataSyncWorkingData();
			Dictionary<string, eDataSyncDataItemStatus> dictionary = new Dictionary<string, eDataSyncDataItemStatus>();
			IReportManager reportManager = new ReportManager(this.OpContext);
			foreach (string text in studentNumbers)
			{
				ReportParameter[] parameters = new ReportParameter[]
				{
					new ReportParameter
					{
						Name = "studentno",
						Value = text
					},
					new ReportParameter
					{
						Name = "student_no",
						Value = text
					}
				}.ToArray<ReportParameter>();
				RunReportResult runReportResult = reportManager.ExecuteReport2(dataSyncDataWorkingData.ImportUserDataReportId, new eFunctionType[]
				{
					eFunctionType.Import_User_Data
				}.ToList<eFunctionType>(), parameters);
				RunFunctionData primaryData = runReportResult.PrimaryData;
				DataTable t = (primaryData != null) ? primaryData.Table : null;
				DataSyncDataResult dataSyncDataResult = this.DataSyncData(text, t);
				bool flag = dataSyncDataResult.ResultStatus == eDataSyncDataStatus.Successful || dataSyncDataResult.ResultStatus == eDataSyncDataStatus.SuccessfulNoData;
				if (flag)
				{
					dictionary.Add(text, eDataSyncDataItemStatus.Successful);
				}
				else
				{
					dictionary.Add(text, eDataSyncDataItemStatus.Failed);
				}
			}
			return new BatchDataSyncDataResult
			{
				BatchResults = dictionary
			};
		}

		// Token: 0x02000331 RID: 817
		internal class DataSyncDataLoadedItemResult
		{
			// Token: 0x060016AF RID: 5807 RVA: 0x0000672B File Offset: 0x0000492B
			public DataSyncDataLoadedItemResult()
			{
			}

			// Token: 0x060016B0 RID: 5808 RVA: 0x00089855 File Offset: 0x00087A55
			public DataSyncDataLoadedItemResult(bool wasSuccessful, DataSyncDataLoadedItem dataItem)
			{
				this.DataSyncItem = dataItem;
				this.WasSuccessful = true;
			}

			// Token: 0x060016B1 RID: 5809 RVA: 0x0008986F File Offset: 0x00087A6F
			public DataSyncDataLoadedItemResult(string errorMessage)
			{
				this.WasSuccessful = false;
				this.ErrorMessage = errorMessage;
			}

			// Token: 0x1700028A RID: 650
			// (get) Token: 0x060016B2 RID: 5810 RVA: 0x00089889 File Offset: 0x00087A89
			// (set) Token: 0x060016B3 RID: 5811 RVA: 0x00089891 File Offset: 0x00087A91
			public DataSyncDataLoadedItem DataSyncItem { get; set; }

			// Token: 0x1700028B RID: 651
			// (get) Token: 0x060016B4 RID: 5812 RVA: 0x0008989A File Offset: 0x00087A9A
			// (set) Token: 0x060016B5 RID: 5813 RVA: 0x000898A2 File Offset: 0x00087AA2
			public bool WasSuccessful { get; set; }

			// Token: 0x1700028C RID: 652
			// (get) Token: 0x060016B6 RID: 5814 RVA: 0x000898AB File Offset: 0x00087AAB
			// (set) Token: 0x060016B7 RID: 5815 RVA: 0x000898B3 File Offset: 0x00087AB3
			public string ErrorMessage { get; set; }
		}
	}
}
