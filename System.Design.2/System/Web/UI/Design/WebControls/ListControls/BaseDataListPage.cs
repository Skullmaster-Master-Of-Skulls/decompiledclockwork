using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Security.Permissions;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Web.UI.Design.WebControls.ListControls
{
	// Token: 0x02000157 RID: 343
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal abstract class BaseDataListPage : ComponentEditorPage
	{
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000BF4 RID: 3060
		protected abstract string HelpKeyword { get; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0004B83E File Offset: 0x00049A3E
		protected bool IsDataGridMode
		{
			get
			{
				return this.dataGridMode;
			}
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0004B848 File Offset: 0x00049A48
		protected BaseDataList GetBaseControl()
		{
			IComponent selectedComponent = base.GetSelectedComponent();
			return (BaseDataList)selectedComponent;
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0004B864 File Offset: 0x00049A64
		protected BaseDataListDesigner GetBaseDesigner()
		{
			BaseDataListDesigner result = null;
			IComponent selectedComponent = base.GetSelectedComponent();
			ISite site = selectedComponent.Site;
			IDesignerHost designerHost = (IDesignerHost)site.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				object designer = designerHost.GetDesigner(selectedComponent);
				result = (BaseDataListDesigner)designer;
			}
			return result;
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0004B8AC File Offset: 0x00049AAC
		public override void SetComponent(IComponent component)
		{
			base.SetComponent(component);
			this.dataGridMode = (this.GetBaseControl() is System.Web.UI.WebControls.DataGrid);
			string @string = SR.GetString("RTL");
			if (!string.Equals(@string, "RTL_False", StringComparison.Ordinal))
			{
				this.RightToLeft = RightToLeft.Yes;
			}
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0004B8F4 File Offset: 0x00049AF4
		public override void ShowHelp()
		{
			IComponent selectedComponent = base.GetSelectedComponent();
			ISite site = selectedComponent.Site;
			IHelpService helpService = (IHelpService)site.GetService(typeof(IHelpService));
			if (helpService != null)
			{
				helpService.ShowHelpFromKeyword(this.HelpKeyword);
			}
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool SupportsHelp()
		{
			return true;
		}

		// Token: 0x04000719 RID: 1817
		private bool dataGridMode;

		// Token: 0x02000463 RID: 1123
		protected class DataSourceItem
		{
			// Token: 0x06002995 RID: 10645 RVA: 0x000FAC0F File Offset: 0x000F8E0F
			public DataSourceItem(string dataSourceName, IEnumerable runtimeDataSource)
			{
				this.runtimeDataSource = runtimeDataSource;
				this.dataSourceName = dataSourceName;
			}

			// Token: 0x170008CA RID: 2250
			// (get) Token: 0x06002996 RID: 10646 RVA: 0x000FAC28 File Offset: 0x000F8E28
			public PropertyDescriptorCollection Fields
			{
				get
				{
					if (this.dataFields == null)
					{
						IEnumerable enumerable = this.RuntimeDataSource;
						if (enumerable != null)
						{
							this.dataFields = DesignTimeData.GetDataFields(enumerable);
						}
					}
					if (this.dataFields == null)
					{
						this.dataFields = new PropertyDescriptorCollection(null);
					}
					return this.dataFields;
				}
			}

			// Token: 0x170008CB RID: 2251
			// (get) Token: 0x06002997 RID: 10647 RVA: 0x0000445B File Offset: 0x0000265B
			public virtual bool HasDataMembers
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170008CC RID: 2252
			// (get) Token: 0x06002998 RID: 10648 RVA: 0x000FAC6D File Offset: 0x000F8E6D
			public string Name
			{
				get
				{
					return this.dataSourceName;
				}
			}

			// Token: 0x170008CD RID: 2253
			// (get) Token: 0x06002999 RID: 10649 RVA: 0x000FAC75 File Offset: 0x000F8E75
			protected virtual object RuntimeComponent
			{
				get
				{
					return this.runtimeDataSource;
				}
			}

			// Token: 0x170008CE RID: 2254
			// (get) Token: 0x0600299A RID: 10650 RVA: 0x000FAC75 File Offset: 0x000F8E75
			protected virtual IEnumerable RuntimeDataSource
			{
				get
				{
					return this.runtimeDataSource;
				}
			}

			// Token: 0x0600299B RID: 10651 RVA: 0x000FAC7D File Offset: 0x000F8E7D
			protected void ClearFields()
			{
				this.dataFields = null;
			}

			// Token: 0x0600299C RID: 10652 RVA: 0x000FAC86 File Offset: 0x000F8E86
			public override string ToString()
			{
				return this.Name;
			}

			// Token: 0x04001D54 RID: 7508
			private IEnumerable runtimeDataSource;

			// Token: 0x04001D55 RID: 7509
			private string dataSourceName;

			// Token: 0x04001D56 RID: 7510
			private PropertyDescriptorCollection dataFields;
		}

		// Token: 0x02000464 RID: 1124
		protected class ListSourceDataSourceItem : BaseDataListPage.DataSourceItem
		{
			// Token: 0x0600299D RID: 10653 RVA: 0x000FAC8E File Offset: 0x000F8E8E
			public ListSourceDataSourceItem(string dataSourceName, IListSource runtimeListSource) : base(dataSourceName, null)
			{
				this.runtimeListSource = runtimeListSource;
			}

			// Token: 0x170008CF RID: 2255
			// (get) Token: 0x0600299E RID: 10654 RVA: 0x000FAC9F File Offset: 0x000F8E9F
			// (set) Token: 0x0600299F RID: 10655 RVA: 0x000FACA7 File Offset: 0x000F8EA7
			public string CurrentDataMember
			{
				get
				{
					return this.currentDataMember;
				}
				set
				{
					this.currentDataMember = value;
					base.ClearFields();
				}
			}

			// Token: 0x170008D0 RID: 2256
			// (get) Token: 0x060029A0 RID: 10656 RVA: 0x000FACB6 File Offset: 0x000F8EB6
			public override bool HasDataMembers
			{
				get
				{
					return this.runtimeListSource.ContainsListCollection;
				}
			}

			// Token: 0x170008D1 RID: 2257
			// (get) Token: 0x060029A1 RID: 10657 RVA: 0x000FACC3 File Offset: 0x000F8EC3
			protected override object RuntimeComponent
			{
				get
				{
					return this.runtimeListSource;
				}
			}

			// Token: 0x170008D2 RID: 2258
			// (get) Token: 0x060029A2 RID: 10658 RVA: 0x000FACCB File Offset: 0x000F8ECB
			protected override IEnumerable RuntimeDataSource
			{
				get
				{
					if (this.HasDataMembers)
					{
						return DesignTimeData.GetDataMember(this.runtimeListSource, this.currentDataMember);
					}
					return this.runtimeListSource.GetList();
				}
			}

			// Token: 0x04001D57 RID: 7511
			private IListSource runtimeListSource;

			// Token: 0x04001D58 RID: 7512
			private string currentDataMember;
		}
	}
}
