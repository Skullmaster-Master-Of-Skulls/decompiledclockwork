using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.DynamicForms
{
	// Token: 0x02000055 RID: 85
	public class DynamicFieldRestClientManager : BearerTokenRestProxy<IDynamicFieldClientManager>, IDynamicFieldClientManager, IWebService
	{
		// Token: 0x0600033F RID: 831 RVA: 0x0000A3FB File Offset: 0x000085FB
		public DynamicFieldRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000A405 File Offset: 0x00008605
		public DynamicFieldRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000A410 File Offset: 0x00008610
		public Forest<DynamicFieldDTO> LoadFieldsAsTree(DynamicFormDTO Form, out List<DynamicFieldDTO> Fields)
		{
			LoadFieldsAsTreeReq loadFieldsAsTreeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadFieldsAsTreeReq>();
			loadFieldsAsTreeReq.Form = Form;
			LoadFieldsAsTreeResp loadFieldsAsTreeResp = base.Post<LoadFieldsAsTreeReq, LoadFieldsAsTreeResp>(loadFieldsAsTreeReq, "dynamicfield/loadfieldsastree");
			Fields = loadFieldsAsTreeResp.Fields;
			return loadFieldsAsTreeResp.Tree;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000A44C File Offset: 0x0000864C
		public DynamicFieldDTO LoadFieldByControlId(int ControlId)
		{
			IList<DynamicFieldDTO> list = this.LoadFieldsByControlIds(new List<int>
			{
				ControlId
			});
			if (list != null && list.Count >= 1)
			{
				return list[0];
			}
			return null;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000A481 File Offset: 0x00008681
		public IList<DynamicFieldDTO> LoadFieldsByControlIds(List<int> ControlIds)
		{
			return base.GetMany<DynamicFieldDTO>(string.Format("dynamicfield/controlids/{0}", ControlIds.CommaSeparatedValuesWithoutSpace<int>()), true);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000A49A File Offset: 0x0000869A
		public IList<DynamicFieldDTO> LoadFieldsByForm(DynamicFormDTO Form)
		{
			return this.LoadFieldsByForm(Form, false);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000A4A4 File Offset: 0x000086A4
		public IList<DynamicFieldDTO> LoadFieldsByFormId(int screenNum, bool ignoreCache = true)
		{
			return base.GetMany<DynamicFieldDTO>(string.Format("dynamicfield/loadfields/screennum/{0}", screenNum), true);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000A4C0 File Offset: 0x000086C0
		public IList<DynamicFieldDTO> LoadFieldsByForm(DynamicFormDTO Form, bool IgnoreCache)
		{
			LoadFieldsByFormReq loadFieldsByFormReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadFieldsByFormReq>();
			loadFieldsByFormReq.Form = Form;
			loadFieldsByFormReq.IgnoreCache = IgnoreCache;
			return base.Post<LoadFieldsByFormReq, IList<DynamicFieldDTO>>(loadFieldsByFormReq, "dynamicfield/loadfields");
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000A4F2 File Offset: 0x000086F2
		public DynamicFieldDTO LoadFieldByName(string Name)
		{
			return base.Get<DynamicFieldDTO>(string.Format("dynamicfield/fieldname/{0}", Name), true);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000A506 File Offset: 0x00008706
		public int CreateField(DynamicFieldDTO Field)
		{
			return base.Post<DynamicFieldDTO, int>(Field, "dynamicfield");
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000A514 File Offset: 0x00008714
		public IList<DynamicListItemDTO> LoadListItems(int LookupGroupId)
		{
			return base.GetMany<DynamicListItemDTO>(string.Format("dynamicfield/listitems/lookupgroupid/{0}", LookupGroupId), true);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000A52D File Offset: 0x0000872D
		public IList<DynamicFormOrGroupOrFieldDTO> LoadFormsWithControls2(bool ExcludeNonDataHoldingControls, params int[] ScreenNumsToExclude)
		{
			return base.GetMany<DynamicFormOrGroupOrFieldDTO>(string.Format("dynamicfield/formswithcontrols/v2/screennumstoexclude/{0}?excludenondataholdingcontrols={1}", ScreenNumsToExclude.CommaSeparatedValuesWithoutSpace<int>(), ExcludeNonDataHoldingControls), true);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000A54C File Offset: 0x0000874C
		public bool IsListItemSavedSomewhere(int LookupListId)
		{
			return base.Get<bool>(string.Format("dynamicfield/islistitemsaved/lookuplistid/{0}", LookupListId), true);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000A565 File Offset: 0x00008765
		public IList<DynamicListGroupDTO> LoadAllLookupLists()
		{
			return base.GetMany<DynamicListGroupDTO>("dynamicfield/alllookuplists", true);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000A573 File Offset: 0x00008773
		public IList<string> GetFieldPossibleValues(int ControlId)
		{
			return base.GetMany<string>(string.Format("dynamicfield/fieldposiblevalues/controlid/{0}", ControlId), true);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000A58C File Offset: 0x0000878C
		public int CreateList(DynamicListGroupDTO group, IList<DynamicListItemDTO> items)
		{
			CreateListReq createListReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateListReq>();
			createListReq.Group = group;
			createListReq.ListItems = items;
			return base.Post<CreateListReq, int>(createListReq, "dynamicfield/createlist");
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000A5C0 File Offset: 0x000087C0
		public IList<int> CreateFields(int ScreenNum, IList<DynamicFieldDTO> fields)
		{
			CreateFieldsReq createFieldsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateFieldsReq>();
			createFieldsReq.ScreenNum = ScreenNum;
			createFieldsReq.Fields = fields;
			return base.Post<CreateFieldsReq, IList<int>>(createFieldsReq, "dynamicfield/createfields");
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000A5F2 File Offset: 0x000087F2
		public IList<int> LoadControlIdsOnForms(bool ignoreCache, params int[] screenNums)
		{
			return base.GetMany<int>(string.Format("dynamicfield/controlidsonforms/screennums/{0}?ignorecache={1}", screenNums.CommaSeparatedValuesWithoutSpace<int>(), ignoreCache), true);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000A611 File Offset: 0x00008811
		public DynamicFieldDTO GetEmailField()
		{
			return base.Get<DynamicFieldDTO>("dynamicfield/emailfield", true);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000A620 File Offset: 0x00008820
		public IList<PersonBaseDTO> LoadStaffFromStaffDropList(DynamicFieldDTO staffDropListField)
		{
			if (((staffDropListField != null) ? staffDropListField.ControlCode : eControlCode.Unknown) != eControlCode.StaffComboBox)
			{
				throw new Exception(("Control is not a staff drop list: " + ((staffDropListField != null) ? staffDropListField.ControlCode.ToString() : null)) ?? "");
			}
			int num = (staffDropListField != null) ? staffDropListField.Setting1 : 0;
			if (num < 1)
			{
				num = 2;
			}
			List<PersonBaseDTO> list = ObjectFactory.Resolve<IPersonBaseClientManager>().LoadGroupMembers(num).ToList<PersonBaseDTO>();
			list.Sort(delegate(PersonBaseDTO g1, PersonBaseDTO g2)
			{
				int num2 = (g1.LastName ?? "").CompareTo(g2.LastName ?? "");
				if (num2 != 0)
				{
					return num2;
				}
				return (g1.FirstName ?? "").CompareTo(g2.LastName ?? "");
			});
			return list;
		}
	}
}
