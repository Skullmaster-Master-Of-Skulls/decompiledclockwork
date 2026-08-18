using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ClockWorkLogger;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.ICore;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Caching;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.DynamicForms
{
	// Token: 0x020000FD RID: 253
	public class DynamicFieldManager : IDynamicFieldManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x00041304 File Offset: 0x0003F504
		private IOldUserSettingManager oldUserSettingManager
		{
			get
			{
				IOldUserSettingManager result;
				if ((result = this._oldUserSettingManager) == null)
				{
					result = (this._oldUserSettingManager = new OldUserSettingManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x0004132F File Offset: 0x0003F52F
		// (set) Token: 0x06000A22 RID: 2594 RVA: 0x00041337 File Offset: 0x0003F537
		public OperationContext OpContext { get; set; }

		// Token: 0x06000A23 RID: 2595 RVA: 0x00041340 File Offset: 0x0003F540
		public DynamicFieldManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new DynamicFieldDAO(opContext);
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00041360 File Offset: 0x0003F560
		private string GetUniqueName(IDictionary<string, Type> d, string name)
		{
			string text = name;
			int num = 1;
			while (d.ContainsKey(text) && num < 1000000)
			{
				text = name + "_" + num.ToString();
				num++;
			}
			return text;
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x000413AC File Offset: 0x0003F5AC
		private IDynamicFieldManager dynamicFieldManager
		{
			get
			{
				bool flag = this._dynamicFieldManager == null;
				if (flag)
				{
					this._dynamicFieldManager = new DynamicFieldManager(this.OpContext);
				}
				return this._dynamicFieldManager;
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x000413E4 File Offset: 0x0003F5E4
		private void AddFieldsMigrationInfo(ref DynamicFormMigrationInfo item, TreeNodeCollection<DynamicField> nodes, string currentSectionDescription)
		{
			for (int i = 0; i < nodes.Count; i++)
			{
				TreeNode<DynamicField> treeNode = nodes[i];
				DynamicField value = treeNode.Value;
				string text = value.ControlCaption;
				bool flag = value.ControlCode == eControlCode.PanelStart;
				if (flag)
				{
					int num = i + 1;
					bool flag2 = num < nodes.Count && nodes[num].Value.ControlCode == eControlCode.Label;
					if (flag2)
					{
						text = nodes[num].Value.ControlCaption;
					}
				}
				int num2 = text.IndexOf("~~");
				bool flag3 = num2 > 0;
				if (flag3)
				{
					text = text.Substring(0, num2);
				}
				bool flag4 = treeNode.Nodes.Count > 0;
				if (flag4)
				{
					this.AddFieldsMigrationInfo(ref item, treeNode.Nodes, text);
				}
				else
				{
					bool flag5 = value.ControlCode == eControlCode.Label || value.ControlCode == eControlCode.HorizontalRule || value.ControlCode == eControlCode.BlankSpace || value.ControlCode == eControlCode.ColumnBreak;
					if (!flag5)
					{
						IList<DynamicListItem> listItems = null;
						bool flag6 = value.ControlCode == eControlCode.RadioGroup || value.ControlCode == eControlCode.DropList || value.ControlCode == eControlCode.ListView;
						if (flag6)
						{
							listItems = this.dynamicFieldManager.LoadListItems(value.Setting1);
						}
						DynamicFieldMigrationInfo item2 = new DynamicFieldMigrationInfo
						{
							Caption = (value.IsReadOnly ? "*" : "") + text,
							ControlCode = value.ControlCode,
							ControlId = value.ControlId,
							ListItems = listItems,
							SectionOnForm = currentSectionDescription
						};
						item.Fields.Add(item2);
						currentSectionDescription = "";
					}
				}
			}
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x000415A0 File Offset: 0x0003F7A0
		private void FixFields(ref List<DynamicField> fields)
		{
			bool flag;
			if (fields.Count > 0)
			{
				if (fields.Find((DynamicField f) => f.ControlCode == eControlCode.TabPageStart) != null)
				{
					flag = (fields.Find((DynamicField f) => f.ControlCode == eControlCode.TabPageClose) == null);
					goto IL_5F;
				}
			}
			flag = false;
			IL_5F:
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = fields[fields.Count - 1].ControlCode != eControlCode.TabControlClose;
				if (flag3)
				{
					int num = -1;
					DynamicField item = null;
					for (int i = 0; i < fields.Count; i++)
					{
						DynamicField dynamicField = fields[i];
						bool flag4 = dynamicField.ControlCode == eControlCode.TabControlClose;
						if (flag4)
						{
							item = dynamicField;
							num = i;
							break;
						}
					}
					bool flag5 = num >= 0;
					if (flag5)
					{
						fields.RemoveAt(num);
						fields.Add(item);
					}
				}
				bool flag6 = false;
				int j = 0;
				int num2 = 90000000;
				while (j < fields.Count)
				{
					DynamicField dynamicField2 = fields[j];
					bool flag7 = dynamicField2.ControlCode == eControlCode.TabPageStart;
					if (flag7)
					{
						bool flag8 = flag6;
						if (flag8)
						{
							fields.Insert(j, new DynamicField
							{
								ControlId = num2++,
								ControlCode = eControlCode.TabPageClose,
								ControlCaption = "",
								Args = new Dictionary<string, string>(),
								ControlName = "",
								EnforceMethod = eEnforceType.Optional
							});
							j += 2;
						}
						else
						{
							flag6 = true;
							j++;
						}
					}
					else
					{
						bool flag9 = dynamicField2.ControlCode == eControlCode.TabControlClose;
						if (flag9)
						{
							bool flag10 = flag6;
							if (flag10)
							{
								fields.Insert(j, new DynamicField
								{
									ControlId = num2++,
									ControlCode = eControlCode.TabPageClose,
									ControlCaption = "",
									Args = new Dictionary<string, string>(),
									ControlName = "",
									EnforceMethod = eEnforceType.Optional
								});
								j += 2;
							}
							else
							{
								j++;
							}
						}
						else
						{
							j++;
						}
					}
				}
			}
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x000417CC File Offset: 0x0003F9CC
		public List<DynamicField> LoadFieldsByControlIds(List<int> ControlIds)
		{
			List<DynamicField> result = this.dao.LoadFieldsByControlIds(ControlIds);
			this.FixFields(ref result);
			return result;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x000417F8 File Offset: 0x0003F9F8
		[DebuggerStepThrough]
		public Task<List<DynamicField>> LoadFieldsByControlIdsAsync(List<int> ControlIds)
		{
			DynamicFieldManager.<LoadFieldsByControlIdsAsync>d__16 <LoadFieldsByControlIdsAsync>d__ = new DynamicFieldManager.<LoadFieldsByControlIdsAsync>d__16();
			<LoadFieldsByControlIdsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<DynamicField>>.Create();
			<LoadFieldsByControlIdsAsync>d__.<>4__this = this;
			<LoadFieldsByControlIdsAsync>d__.ControlIds = ControlIds;
			<LoadFieldsByControlIdsAsync>d__.<>1__state = -1;
			<LoadFieldsByControlIdsAsync>d__.<>t__builder.Start<DynamicFieldManager.<LoadFieldsByControlIdsAsync>d__16>(ref <LoadFieldsByControlIdsAsync>d__);
			return <LoadFieldsByControlIdsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00041844 File Offset: 0x0003FA44
		public DynamicField LoadFieldByControlId(int ControlId)
		{
			List<DynamicField> list = this.LoadFieldsByControlIds(new List<int>
			{
				ControlId
			});
			bool flag = list == null || list.Count < 1;
			DynamicField result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = list[0];
			}
			return result;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00041888 File Offset: 0x0003FA88
		[DebuggerStepThrough]
		public Task<DynamicField> LoadFieldByControlIdAsync(int ControlId)
		{
			DynamicFieldManager.<LoadFieldByControlIdAsync>d__18 <LoadFieldByControlIdAsync>d__ = new DynamicFieldManager.<LoadFieldByControlIdAsync>d__18();
			<LoadFieldByControlIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DynamicField>.Create();
			<LoadFieldByControlIdAsync>d__.<>4__this = this;
			<LoadFieldByControlIdAsync>d__.ControlId = ControlId;
			<LoadFieldByControlIdAsync>d__.<>1__state = -1;
			<LoadFieldByControlIdAsync>d__.<>t__builder.Start<DynamicFieldManager.<LoadFieldByControlIdAsync>d__18>(ref <LoadFieldByControlIdAsync>d__);
			return <LoadFieldByControlIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x000418D4 File Offset: 0x0003FAD4
		public DynamicField LoadFieldByUniqueId(Guid uniqueId)
		{
			return this.dao.LoadFieldByUniqueId(uniqueId);
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x000418F4 File Offset: 0x0003FAF4
		public List<DynamicField> LoadFields(DynamicForm Form)
		{
			return this.LoadFields(Form, false);
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00041910 File Offset: 0x0003FB10
		public List<DynamicField> LoadFields(int screenNum, bool IgnoreCache)
		{
			ServerCacheItem key = new ServerCacheItem
			{
				ServerCacheItemType = eServerCacheItemType.uDynamicFormFields,
				SubItemId = screenNum
			};
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			List<DynamicField> list = IgnoreCache ? null : ((List<DynamicField>)cacheStorageManager[key]);
			bool flag = list == null;
			if (flag)
			{
				list = this.dao.LoadFields(screenNum);
				this.FixFields(ref list);
				cacheStorageManager[key] = list;
			}
			return list;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00041980 File Offset: 0x0003FB80
		public List<DynamicField> LoadFields(DynamicForm Form, bool IgnoreCache)
		{
			return this.LoadFields(Form.ScreenNum, IgnoreCache);
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x000419A0 File Offset: 0x0003FBA0
		public Forest<DynamicField> LoadFieldsAsTree(DynamicForm Form, out List<DynamicField> Fields)
		{
			Fields = this.LoadFields(Form);
			ServerCacheItem key = new ServerCacheItem
			{
				ServerCacheItemType = eServerCacheItemType.uDynamicFormFieldsTree,
				SubItemId = Form.ScreenNum
			};
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			Forest<DynamicField> forest = (Forest<DynamicField>)cacheStorageManager[key];
			bool flag = forest == null;
			if (flag)
			{
				bool flag2 = Fields != null;
				if (flag2)
				{
					forest = Fields.FieldListToForest();
					cacheStorageManager.Insert(key, Fields, new TimeSpan(12, 0, 0));
				}
			}
			return forest;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00041A20 File Offset: 0x0003FC20
		public int CreateField(DynamicField Field)
		{
			return this.dao.CreateField(Field);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00041A40 File Offset: 0x0003FC40
		public DynamicField LoadFieldByName(string Name)
		{
			return this.dao.LoadFieldByName(Name);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00041A60 File Offset: 0x0003FC60
		public DynamicField GetEmailField()
		{
			eServerCacheItemType eServerCacheItemType = eServerCacheItemType.uDynamicFieldCid_Email;
			int whoAmI = this.OpContext.WhoAmI;
			OperationContext opContext = this.OpContext;
			IUserDatabaseCacheStorageManager userDatabaseCacheStorageManager = new UserDatabaseCacheStorageManager((opContext != null) ? opContext.TenantId : null);
			DynamicField dynamicField = (DynamicField)userDatabaseCacheStorageManager[whoAmI, eServerCacheItemType];
			bool flag = dynamicField == null;
			if (flag)
			{
				IOldUserSettingManager oldUserSettingManager = this.oldUserSettingManager;
				int settingValue_Int = oldUserSettingManager.GetSettingValue_Int(whoAmI, eSettingCode.SETTING_EmailControlID);
				bool flag2 = settingValue_Int < 1;
				if (flag2)
				{
					dynamicField = this.dao.SearchForField("email", 1);
					bool flag3 = dynamicField == null;
					if (flag3)
					{
						dynamicField = this.dao.SearchForField("school email", 1);
					}
				}
				else
				{
					List<DynamicField> list = this.dao.LoadFieldsByControlIds(new List<int>
					{
						settingValue_Int
					});
					bool flag4 = list != null && list.Count > 0;
					if (flag4)
					{
						dynamicField = list[0];
					}
				}
				bool flag5 = dynamicField != null;
				if (flag5)
				{
					userDatabaseCacheStorageManager[whoAmI, eServerCacheItemType] = dynamicField;
				}
			}
			return dynamicField;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00041B6C File Offset: 0x0003FD6C
		public int CreateList(DynamicListGroup listGroup, IList<DynamicListItem> listItems)
		{
			return this.dao.CreateList(listGroup, listItems);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00041B8C File Offset: 0x0003FD8C
		public List<DynamicListItem> LoadListItems(int LookupGroupId)
		{
			ServerCacheItem key = new ServerCacheItem
			{
				ServerCacheItemType = eServerCacheItemType.uDynamicForm_LookupList,
				SubItemId = LookupGroupId
			};
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			List<DynamicListItem> list = (List<DynamicListItem>)cacheStorageManager[key];
			bool flag = list == null;
			if (flag)
			{
				list = this.dao.LoadListItems(LookupGroupId);
				cacheStorageManager.Insert(key, list, TimeSpan.FromHours(8.0), true);
			}
			return list;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00041BFC File Offset: 0x0003FDFC
		[DebuggerStepThrough]
		public Task<List<DynamicListItem>> LoadListItemsAsync(int LookupGroupId)
		{
			DynamicFieldManager.<LoadListItemsAsync>d__29 <LoadListItemsAsync>d__ = new DynamicFieldManager.<LoadListItemsAsync>d__29();
			<LoadListItemsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<DynamicListItem>>.Create();
			<LoadListItemsAsync>d__.<>4__this = this;
			<LoadListItemsAsync>d__.LookupGroupId = LookupGroupId;
			<LoadListItemsAsync>d__.<>1__state = -1;
			<LoadListItemsAsync>d__.<>t__builder.Start<DynamicFieldManager.<LoadListItemsAsync>d__29>(ref <LoadListItemsAsync>d__);
			return <LoadListItemsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00041C47 File Offset: 0x0003FE47
		public void UpdateFieldName(int ControlId, string NewName)
		{
			this.dao.UpdateFieldName(ControlId, NewName);
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00041C58 File Offset: 0x0003FE58
		public IList<DynamicFormOrGroupOrField> LoadFormsWithControls(bool ExcludeNonDataHoldingControls, params int[] ScreenNumsToExclude)
		{
			return this.dao.LoadFormsWithGroupsAndFields(ExcludeNonDataHoldingControls, ScreenNumsToExclude);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00041C78 File Offset: 0x0003FE78
		public IDictionary<int, ExtendedAccommodationInfo> LoadAccommodationShortCodes(params int[] ControlIds)
		{
			return this.dao.LoadAccommodationShortCodes(ControlIds);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00041C98 File Offset: 0x0003FE98
		public IList<DynamicFormMigrationInfo> LoadDynamicFormMigrationInfo(params int[] ScreenNums)
		{
			List<DynamicFormMigrationInfo> list = new List<DynamicFormMigrationInfo>();
			IDynamicFormManager dynamicFormManager = new DynamicFormManager(this.OpContext);
			foreach (int screenNum in ScreenNums)
			{
				DynamicForm dynamicForm = dynamicFormManager.LoadDynamicFormById(screenNum);
				DynamicFormMigrationInfo item = new DynamicFormMigrationInfo
				{
					ScreenNum = screenNum,
					ScreenName = ((dynamicForm == null) ? "Unknown" : (dynamicForm.Title ?? "NULL")),
					Fields = new List<DynamicFieldMigrationInfo>()
				};
				List<DynamicField> list2;
				Forest<DynamicField> forest = this.dynamicFieldManager.LoadFieldsAsTree(new DynamicForm
				{
					ScreenNum = screenNum
				}, out list2);
				this.AddFieldsMigrationInfo(ref item, forest.Nodes, "");
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00041D60 File Offset: 0x0003FF60
		public IList<int> CreateFields(int ScreenNum, IList<DynamicField> Fields)
		{
			List<int> list = new List<int>();
			foreach (DynamicField dynamicField in Fields)
			{
				int num = this.CreateField(dynamicField);
				bool flag = num > 0;
				if (flag)
				{
					dynamicField.ControlId = num;
					DynamicFieldOnForm fieldOnForm = new DynamicFieldOnForm(dynamicField, ScreenNum);
					int num2 = this.dao.CreateFieldOnForm(fieldOnForm);
					bool flag2 = num2 < 1;
					if (flag2)
					{
						CWLogger.Logger.Error("DynamicFieldManager:CreateField:FailedToReturnDynamicScreenControlId:Cid={0}", num.ToString());
					}
					else
					{
						list.Add(num);
					}
				}
				else
				{
					CWLogger.Logger.Error("DynamicFieldManager:CreateField:FailedToReturnCid");
				}
			}
			return list;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00041E2C File Offset: 0x0004002C
		public DynamicField GetFirstFieldOnFirstPerAppointmentForm(int AppTypeId, eControlCode FieldType)
		{
			return this.dao.GetFirstFieldOnFirstPerAppointmentForm(AppTypeId, FieldType);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00041E4C File Offset: 0x0004004C
		public bool IsListItemSavedSomewhere(int LookupListId)
		{
			return this.dao.IsListItemSavedSomewhere(LookupListId);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00041E6C File Offset: 0x0004006C
		public IList<DynamicListGroup> LoadAllLookupLists()
		{
			return this.dao.LoadAllLookupLists();
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00041E8C File Offset: 0x0004008C
		public IDictionary<string, Type> LoadListViewOrFileListColumns(int ControlId)
		{
			DynamicField dynamicField = this.LoadFieldByControlId(ControlId);
			bool flag = dynamicField == null;
			if (flag)
			{
				throw new InvalidParameterException("DynamicFieldManager:LoadListViewOrFileListColumns:Can't load field with controlid=" + ControlId.ToString());
			}
			eControlCode controlCode = dynamicField.ControlCode;
			List<string> list = (from g in this.LoadListItems(dynamicField.Setting1)
			select g.LookupText).ToList<string>();
			bool flag2 = controlCode == eControlCode.FileList;
			if (flag2)
			{
				list.Add("Filename");
				list.Add("Date");
			}
			else
			{
				bool flag3 = controlCode == eControlCode.ListView || controlCode == eControlCode.DynamicTable;
				if (!flag3)
				{
					throw new InvalidParameterException("DynamicFieldManager:LoadListViewOrFileListColumns:Field is not of supported type; controlcode=" + controlCode.ToString() + "; cid=" + dynamicField.ControlId.ToString());
				}
				list.Add("Date");
			}
			Dictionary<string, Type> dictionary = new Dictionary<string, Type>();
			foreach (string text in list)
			{
				string[] array = text.Split(new char[]
				{
					'`'
				});
				Type type = null;
				bool flag4 = array.Length > 1;
				string name;
				if (flag4)
				{
					name = array[0];
					string text2 = array[1].ToLower();
					string text3 = text2;
					string a = text3;
					if (!(a == ".chk"))
					{
						if (a == ".da2" || a == ".dat" || a == ".dtp")
						{
							type = typeof(DateTime);
						}
					}
					else
					{
						type = typeof(bool);
					}
				}
				else
				{
					name = text;
				}
				bool flag5 = type == null;
				if (flag5)
				{
					type = typeof(string);
				}
				dictionary.Add(this.GetUniqueName(dictionary, name), type);
			}
			return dictionary;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00042094 File Offset: 0x00040294
		public IList<string> GetFieldPossibleValues(int ControlId)
		{
			DynamicField dynamicField = this.LoadFieldByControlId(ControlId);
			eControlCode eControlCode = dynamicField.ControlCode;
			bool flag = eControlCode == eControlCode.RadioGroup && dynamicField.Setting4 != 1;
			if (flag)
			{
				eControlCode = eControlCode.DropList;
			}
			eControlCode eControlCode2 = eControlCode;
			eControlCode eControlCode3 = eControlCode2;
			List<string> list4;
			if (eControlCode3 <= eControlCode.RadioGroup)
			{
				if (eControlCode3 != eControlCode.CheckBox)
				{
					if (eControlCode3 == eControlCode.DropList)
					{
						goto IL_238;
					}
					if (eControlCode3 != eControlCode.RadioGroup)
					{
						goto IL_308;
					}
					IDynamicFormManager dynamicFormManager = new DynamicFormManager(this.OpContext);
					IList<int> list = dynamicFormManager.FindScreensAControlExistsOn(dynamicField.ControlId);
					Dictionary<string, int> dictionary = new Dictionary<string, int>();
					Predicate<DynamicField> <>9__6;
					foreach (int screenNum in list)
					{
						List<DynamicField> list2 = this.dynamicFieldManager.LoadFields(screenNum, true);
						List<DynamicField> list3 = list2;
						Predicate<DynamicField> match;
						if ((match = <>9__6) == null)
						{
							match = (<>9__6 = ((DynamicField g) => g.ControlId == ControlId));
						}
						int num = list3.FindIndex(match);
						bool flag2 = num < 0;
						if (!flag2)
						{
							int i;
							for (i = num + 1; i < list2.Count; i++)
							{
								DynamicField dynamicField2 = list2[i];
								DynamicControlAttribute attribute = dynamicField2.ControlCode.GetAttribute();
								bool flag3 = attribute == null || attribute.IsControlCollectionEnd || attribute.IsControlCollectionStart;
								if (flag3)
								{
									break;
								}
							}
							for (int j = num + 1; j < i; j++)
							{
								string key = list2[j].GetCaptionForDisplay().ToLower();
								bool flag4 = !dictionary.ContainsKey(key);
								if (flag4)
								{
									dictionary.Add(key, list2[j].ControlId);
								}
							}
						}
					}
					list4 = (from g in dictionary
					select g.Key).ToList<string>();
					goto IL_30C;
				}
			}
			else
			{
				if (eControlCode3 == eControlCode.StaffComboBox)
				{
					IPeopleManager peopleManager = new PeopleManager(this.OpContext);
					List<PersonBase> source = peopleManager.LoadGroupMembers(2);
					list4 = (from g in source
					select g.Student_no into h
					where !string.IsNullOrEmpty(h)
					select h).ToList<string>();
					goto IL_30C;
				}
				if (eControlCode3 != eControlCode.AccommodationCheckBox)
				{
					if (eControlCode3 != eControlCode.AccommodationDropList)
					{
						goto IL_308;
					}
					goto IL_238;
				}
			}
			list4 = new List<string>
			{
				"True",
				"False"
			};
			goto IL_30C;
			IL_238:
			List<DynamicListItem> list5 = this.LoadListItems(dynamicField.Setting1);
			List<string> list6;
			if (list5 != null)
			{
				list6 = (from g in list5
				select (g.LookupText ?? "").Trim() into h
				where h.Length > 0
				select h).ToList<string>();
			}
			else
			{
				list6 = null;
			}
			list4 = list6;
			goto IL_30C;
			IL_308:
			list4 = null;
			IL_30C:
			bool flag5 = list4 == null;
			IList<string> result;
			if (flag5)
			{
				result = list4;
			}
			else
			{
				list4.Sort((string g1, string g2) => g1.CompareTo(g2));
				result = list4;
			}
			return result;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00042408 File Offset: 0x00040608
		public IList<int> LoadControlIdsOnForms(bool ignoreCache, params int[] screenNums)
		{
			bool flag = screenNums == null;
			IList<int> result;
			if (flag)
			{
				result = new List<int>();
			}
			else
			{
				bool ignoreCache2 = ignoreCache;
				if (ignoreCache2)
				{
					result = this.dao.LoadControlIdsByForms(screenNums).SelectMany((KeyValuePair<int, IList<int>> g) => g.Value).Distinct<int>().ToList<int>();
				}
				else
				{
					ICacheStorageManager cache = ObjectFactory.Resolve<ICacheStorageManager>();
					List<int> screenNumsFoundInCache = new List<int>();
					Dictionary<int, List<int>> source = screenNums.ToDictionary((int g) => g, delegate(int g)
					{
						bool ignoreCache3 = ignoreCache;
						if (ignoreCache3)
						{
							ServerCacheItem key = new ServerCacheItem
							{
								ServerCacheItemType = eServerCacheItemType.uDynamicFormFields,
								SubItemId = g
							};
							List<DynamicField> list2 = (List<DynamicField>)cache[key];
							bool flag2 = list2 != null;
							if (flag2)
							{
								screenNumsFoundInCache.Add(g);
								return (from h in list2
								select h.ControlId).Distinct<int>().ToList<int>();
							}
						}
						return new List<int>();
					});
					List<int> first = source.SelectMany((KeyValuePair<int, List<int>> g) => g.Value).Distinct<int>().ToList<int>();
					List<int> list = (from g in screenNums
					where !screenNumsFoundInCache.Contains(g)
					select g).ToList<int>();
					List<int> second = this.dao.LoadControlIdsByForms(list.ToArray()).SelectMany((KeyValuePair<int, IList<int>> g) => g.Value).Distinct<int>().ToList<int>();
					result = first.Concat(second).Distinct<int>().ToList<int>();
				}
			}
			return result;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00042570 File Offset: 0x00040770
		[DebuggerStepThrough]
		public Task<IList<int>> LoadControlIdsOnFormsAsync(bool ignoreCache, params int[] screenNums)
		{
			DynamicFieldManager.<LoadControlIdsOnFormsAsync>d__41 <LoadControlIdsOnFormsAsync>d__ = new DynamicFieldManager.<LoadControlIdsOnFormsAsync>d__41();
			<LoadControlIdsOnFormsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<int>>.Create();
			<LoadControlIdsOnFormsAsync>d__.<>4__this = this;
			<LoadControlIdsOnFormsAsync>d__.ignoreCache = ignoreCache;
			<LoadControlIdsOnFormsAsync>d__.screenNums = screenNums;
			<LoadControlIdsOnFormsAsync>d__.<>1__state = -1;
			<LoadControlIdsOnFormsAsync>d__.<>t__builder.Start<DynamicFieldManager.<LoadControlIdsOnFormsAsync>d__41>(ref <LoadControlIdsOnFormsAsync>d__);
			return <LoadControlIdsOnFormsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x040001BC RID: 444
		private IOldUserSettingManager _oldUserSettingManager;

		// Token: 0x040001BD RID: 445
		private IDynamicFieldDAO dao;

		// Token: 0x040001BF RID: 447
		private IDynamicFieldManager _dynamicFieldManager;
	}
}
