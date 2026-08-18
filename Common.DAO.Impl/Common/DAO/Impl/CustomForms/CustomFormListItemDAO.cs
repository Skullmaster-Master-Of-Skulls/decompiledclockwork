using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Databases;
using TechnoPro.Common.DAO.CustomForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.Common.DAO.Impl.CustomForms
{
	// Token: 0x02000101 RID: 257
	public class CustomFormListItemDAO : ICustomFormListItemDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000756 RID: 1878 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		public CustomFormListItemDAO()
		{
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0004B488 File Offset: 0x00049688
		public CustomFormListItemDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0004B49A File Offset: 0x0004969A
		// (set) Token: 0x06000759 RID: 1881 RVA: 0x0004B4A2 File Offset: 0x000496A2
		public OperationContext OpContext { get; set; }

		// Token: 0x0600075A RID: 1882 RVA: 0x0004B4AC File Offset: 0x000496AC
		private CustomListItem GetCustomListItemFromRecord(IDataRecord record)
		{
			return new CustomListItem
			{
				ListItemId = (Guid)record["CustomListItemId"],
				ItemCaption = ((record["ItemCaption"] is DBNull) ? "" : ((string)record["ItemCaption"])),
				OrderNum = ((record["OrderNum"] is DBNull) ? 0 : ((int)record["OrderNum"]))
			};
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0004B538 File Offset: 0x00049738
		[DebuggerStepThrough]
		public Task<IList<CustomListItem>> LoadListItemsByGroupIdAsync(Guid customListItemGroupId)
		{
			CustomFormListItemDAO.<LoadListItemsByGroupIdAsync>d__7 <LoadListItemsByGroupIdAsync>d__ = new CustomFormListItemDAO.<LoadListItemsByGroupIdAsync>d__7();
			<LoadListItemsByGroupIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<CustomListItem>>.Create();
			<LoadListItemsByGroupIdAsync>d__.<>4__this = this;
			<LoadListItemsByGroupIdAsync>d__.customListItemGroupId = customListItemGroupId;
			<LoadListItemsByGroupIdAsync>d__.<>1__state = -1;
			<LoadListItemsByGroupIdAsync>d__.<>t__builder.Start<CustomFormListItemDAO.<LoadListItemsByGroupIdAsync>d__7>(ref <LoadListItemsByGroupIdAsync>d__);
			return <LoadListItemsByGroupIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0004B584 File Offset: 0x00049784
		public IList<CustomListItem> LoadListItemsByGroupId(Guid customListItemGroupId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@groupid", DbType.Guid, customListItemGroupId)
			};
			IList<CustomListItem> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT cli.CustomListItemId,cli.ItemCaption,cli.OrderNum FROM CustomListItem cli WHERE cli.CustomListGroupId=@groupid ORDER BY cli.OrderNum,cli.ItemCaption", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<CustomListItem> list = new List<CustomListItem>();
					while (dataReader.Read())
					{
						CustomListItem customListItemFromRecord = this.GetCustomListItemFromRecord(dataReader);
						bool flag2 = customListItemFromRecord != null;
						if (flag2)
						{
							list.Add(customListItemFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0004B63C File Offset: 0x0004983C
		[DebuggerStepThrough]
		public Task<CustomListItem> LoadListItemByListItemIdAsync(Guid listItemId)
		{
			CustomFormListItemDAO.<LoadListItemByListItemIdAsync>d__9 <LoadListItemByListItemIdAsync>d__ = new CustomFormListItemDAO.<LoadListItemByListItemIdAsync>d__9();
			<LoadListItemByListItemIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CustomListItem>.Create();
			<LoadListItemByListItemIdAsync>d__.<>4__this = this;
			<LoadListItemByListItemIdAsync>d__.listItemId = listItemId;
			<LoadListItemByListItemIdAsync>d__.<>1__state = -1;
			<LoadListItemByListItemIdAsync>d__.<>t__builder.Start<CustomFormListItemDAO.<LoadListItemByListItemIdAsync>d__9>(ref <LoadListItemByListItemIdAsync>d__);
			return <LoadListItemByListItemIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0004B688 File Offset: 0x00049888
		public CustomListItem LoadListItemByListItemId(Guid listItemId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@id", DbType.Guid, listItemId)
			};
			CustomListItem result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT cli.CustomListItemId,cli.ItemCaption,cli.OrderNum FROM CustomListItem cli WHERE cli.CustomListItemId=@id", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetCustomListItemFromRecord(dataReader);
				}
			}
			return result;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0004B71C File Offset: 0x0004991C
		[DebuggerStepThrough]
		public Task<Guid> CreateListGroupAsync(CustomListItemGroup group)
		{
			CustomFormListItemDAO.<CreateListGroupAsync>d__11 <CreateListGroupAsync>d__ = new CustomFormListItemDAO.<CreateListGroupAsync>d__11();
			<CreateListGroupAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Guid>.Create();
			<CreateListGroupAsync>d__.<>4__this = this;
			<CreateListGroupAsync>d__.group = group;
			<CreateListGroupAsync>d__.<>1__state = -1;
			<CreateListGroupAsync>d__.<>t__builder.Start<CustomFormListItemDAO.<CreateListGroupAsync>d__11>(ref <CreateListGroupAsync>d__);
			return <CreateListGroupAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0004B768 File Offset: 0x00049968
		[DebuggerStepThrough]
		public Task<Guid> CreateListItemAsync(Guid customListItemGroupId, CustomListItem item)
		{
			CustomFormListItemDAO.<CreateListItemAsync>d__12 <CreateListItemAsync>d__ = new CustomFormListItemDAO.<CreateListItemAsync>d__12();
			<CreateListItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Guid>.Create();
			<CreateListItemAsync>d__.<>4__this = this;
			<CreateListItemAsync>d__.customListItemGroupId = customListItemGroupId;
			<CreateListItemAsync>d__.item = item;
			<CreateListItemAsync>d__.<>1__state = -1;
			<CreateListItemAsync>d__.<>t__builder.Start<CustomFormListItemDAO.<CreateListItemAsync>d__12>(ref <CreateListItemAsync>d__);
			return <CreateListItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0004B7BC File Offset: 0x000499BC
		[DebuggerStepThrough]
		public Task UpdateListItemAsync(CustomListItem item)
		{
			CustomFormListItemDAO.<UpdateListItemAsync>d__13 <UpdateListItemAsync>d__ = new CustomFormListItemDAO.<UpdateListItemAsync>d__13();
			<UpdateListItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateListItemAsync>d__.<>4__this = this;
			<UpdateListItemAsync>d__.item = item;
			<UpdateListItemAsync>d__.<>1__state = -1;
			<UpdateListItemAsync>d__.<>t__builder.Start<CustomFormListItemDAO.<UpdateListItemAsync>d__13>(ref <UpdateListItemAsync>d__);
			return <UpdateListItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0004B808 File Offset: 0x00049A08
		[DebuggerStepThrough]
		public Task UpdateListItemGroupAsync(CustomListItemGroup group)
		{
			CustomFormListItemDAO.<UpdateListItemGroupAsync>d__14 <UpdateListItemGroupAsync>d__ = new CustomFormListItemDAO.<UpdateListItemGroupAsync>d__14();
			<UpdateListItemGroupAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateListItemGroupAsync>d__.<>4__this = this;
			<UpdateListItemGroupAsync>d__.group = group;
			<UpdateListItemGroupAsync>d__.<>1__state = -1;
			<UpdateListItemGroupAsync>d__.<>t__builder.Start<CustomFormListItemDAO.<UpdateListItemGroupAsync>d__14>(ref <UpdateListItemGroupAsync>d__);
			return <UpdateListItemGroupAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0004B854 File Offset: 0x00049A54
		[DebuggerStepThrough]
		public Task EnableOrDisableListItemAsync(Guid CustomListItemId, bool enable)
		{
			CustomFormListItemDAO.<EnableOrDisableListItemAsync>d__15 <EnableOrDisableListItemAsync>d__ = new CustomFormListItemDAO.<EnableOrDisableListItemAsync>d__15();
			<EnableOrDisableListItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<EnableOrDisableListItemAsync>d__.<>4__this = this;
			<EnableOrDisableListItemAsync>d__.CustomListItemId = CustomListItemId;
			<EnableOrDisableListItemAsync>d__.enable = enable;
			<EnableOrDisableListItemAsync>d__.<>1__state = -1;
			<EnableOrDisableListItemAsync>d__.<>t__builder.Start<CustomFormListItemDAO.<EnableOrDisableListItemAsync>d__15>(ref <EnableOrDisableListItemAsync>d__);
			return <EnableOrDisableListItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0004B8A8 File Offset: 0x00049AA8
		[DebuggerStepThrough]
		public Task EnableOrDisableListItemGroupAsync(Guid customListItemGroupId, bool enable)
		{
			CustomFormListItemDAO.<EnableOrDisableListItemGroupAsync>d__16 <EnableOrDisableListItemGroupAsync>d__ = new CustomFormListItemDAO.<EnableOrDisableListItemGroupAsync>d__16();
			<EnableOrDisableListItemGroupAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<EnableOrDisableListItemGroupAsync>d__.<>4__this = this;
			<EnableOrDisableListItemGroupAsync>d__.customListItemGroupId = customListItemGroupId;
			<EnableOrDisableListItemGroupAsync>d__.enable = enable;
			<EnableOrDisableListItemGroupAsync>d__.<>1__state = -1;
			<EnableOrDisableListItemGroupAsync>d__.<>t__builder.Start<CustomFormListItemDAO.<EnableOrDisableListItemGroupAsync>d__16>(ref <EnableOrDisableListItemGroupAsync>d__);
			return <EnableOrDisableListItemGroupAsync>d__.<>t__builder.Task;
		}
	}
}
