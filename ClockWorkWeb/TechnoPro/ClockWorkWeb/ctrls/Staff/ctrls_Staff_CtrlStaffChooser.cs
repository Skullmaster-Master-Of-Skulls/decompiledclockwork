using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.Core.People;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.UI.Web.Entity.Web.EventArgs;

namespace TechnoPro.ClockWorkWeb.ctrls.Staff
{
	// Token: 0x0200013A RID: 314
	public class ctrls_Staff_CtrlStaffChooser : UserControl
	{
		// Token: 0x06000965 RID: 2405 RVA: 0x00042E3C File Offset: 0x0004103C
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000966 RID: 2406 RVA: 0x00042E60 File Offset: 0x00041060
		// (remove) Token: 0x06000967 RID: 2407 RVA: 0x00042E98 File Offset: 0x00041098
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<SelectedIdEventArgs> OnSelectedPersonIdChanged;

		// Token: 0x06000968 RID: 2408 RVA: 0x00042ECD File Offset: 0x000410CD
		private void FireOnSelectedPersonIdChanged(int pid)
		{
			EventHandler<SelectedIdEventArgs> onSelectedPersonIdChanged = this.OnSelectedPersonIdChanged;
			if (onSelectedPersonIdChanged != null)
			{
				onSelectedPersonIdChanged(this, new SelectedIdEventArgs
				{
					Id = pid
				});
			}
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00042EF0 File Offset: 0x000410F0
		protected void cmb_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			int selectedPid = this.SelectedPid;
			bool flag = selectedPid > 0;
			if (flag)
			{
				this.FireOnSelectedPersonIdChanged(selectedPid);
			}
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00042F18 File Offset: 0x00041118
		public void SetSelectedPid(int pid)
		{
			this.cmb.SelectedIndex = -1;
			string b = pid.ToString();
			foreach (object obj in this.cmb.Items)
			{
				ListItem listItem = (ListItem)obj;
				bool flag = listItem.Value == b;
				if (flag)
				{
					listItem.Selected = true;
					break;
				}
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00042FA8 File Offset: 0x000411A8
		public int SelectedPid
		{
			get
			{
				string selectedValue = this.cmb.SelectedValue;
				int num;
				bool flag = !string.IsNullOrEmpty(selectedValue) && int.TryParse(selectedValue, out num) && num > 0;
				int result;
				if (flag)
				{
					result = num;
				}
				else
				{
					result = 0;
				}
				return result;
			}
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00042FE8 File Offset: 0x000411E8
		public new void Init(IList<int> staffGroupIdsToLoad)
		{
			IPeopleClientManager peopleClientManager = new PeopleClientManager();
			List<ctrls_Staff_CtrlStaffChooser.StaffWrapper> list = peopleClientManager.LoadGroupMembers(staffGroupIdsToLoad.ToArray<int>()).ToList<PersonBaseDTO>().ConvertAll<ctrls_Staff_CtrlStaffChooser.StaffWrapper>((PersonBaseDTO g) => new ctrls_Staff_CtrlStaffChooser.StaffWrapper(g));
			BindingList<ctrls_Staff_CtrlStaffChooser.StaffWrapper> dataSource = new BindingList<ctrls_Staff_CtrlStaffChooser.StaffWrapper>(list);
			this.cmb.DataSource = dataSource;
			this.cmb.DataValueField = "PersonId";
			this.cmb.DataTextField = "Title";
			this.cmb.DataBind();
		}

		// Token: 0x0400075F RID: 1887
		protected DropDownList cmb;

		// Token: 0x0200024B RID: 587
		internal class StaffWrapper : WrapperBase<PersonBaseDTO>
		{
			// Token: 0x06000F0A RID: 3850 RVA: 0x00051017 File Offset: 0x0004F217
			public StaffWrapper()
			{
			}

			// Token: 0x06000F0B RID: 3851 RVA: 0x00051021 File Offset: 0x0004F221
			public StaffWrapper(PersonBaseDTO p) : base(p)
			{
			}

			// Token: 0x17000357 RID: 855
			// (get) Token: 0x06000F0C RID: 3852 RVA: 0x0005102C File Offset: 0x0004F22C
			public int PersonId
			{
				get
				{
					return (base.Item == null) ? 0 : base.Item.PersonId;
				}
			}

			// Token: 0x17000358 RID: 856
			// (get) Token: 0x06000F0D RID: 3853 RVA: 0x00051054 File Offset: 0x0004F254
			public string Title
			{
				get
				{
					return (base.Item == null) ? "" : base.Item.GetName();
				}
			}
		}
	}
}
