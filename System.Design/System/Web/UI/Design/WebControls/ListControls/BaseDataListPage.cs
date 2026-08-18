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
	// Token: 0x0200050A RID: 1290
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal abstract class BaseDataListPage : ComponentEditorPage
	{
		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06002E07 RID: 11783
		protected abstract string HelpKeyword { get; }

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06002E08 RID: 11784 RVA: 0x00104FF2 File Offset: 0x00103FF2
		protected bool IsDataGridMode
		{
			get
			{
				return this.dataGridMode;
			}
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x00104FFC File Offset: 0x00103FFC
		protected BaseDataList GetBaseControl()
		{
			IComponent selectedComponent = base.GetSelectedComponent();
			return (BaseDataList)selectedComponent;
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x00105018 File Offset: 0x00104018
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

		// Token: 0x06002E0B RID: 11787 RVA: 0x00105060 File Offset: 0x00104060
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

		// Token: 0x06002E0C RID: 11788 RVA: 0x001050A8 File Offset: 0x001040A8
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

		// Token: 0x06002E0D RID: 11789 RVA: 0x001050E8 File Offset: 0x001040E8
		public override bool SupportsHelp()
		{
			return true;
		}

		// Token: 0x04001F52 RID: 8018
		private bool dataGridMode;

		// Token: 0x0200050B RID: 1291
		protected class DataSourceItem
		{
			// Token: 0x06002E0F RID: 11791 RVA: 0x001050F3 File Offset: 0x001040F3
			public DataSourceItem(string dataSourceName, IEnumerable runtimeDataSource)
			{
				this.runtimeDataSource = runtimeDataSource;
				this.dataSourceName = dataSourceName;
			}

			// Token: 0x170008B4 RID: 2228
			// (get) Token: 0x06002E10 RID: 11792 RVA: 0x0010510C File Offset: 0x0010410C
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

			// Token: 0x170008B5 RID: 2229
			// (get) Token: 0x06002E11 RID: 11793 RVA: 0x00105151 File Offset: 0x00104151
			public virtual bool HasDataMembers
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170008B6 RID: 2230
			// (get) Token: 0x06002E12 RID: 11794 RVA: 0x00105154 File Offset: 0x00104154
			public string Name
			{
				get
				{
					return this.dataSourceName;
				}
			}

			// Token: 0x170008B7 RID: 2231
			// (get) Token: 0x06002E13 RID: 11795 RVA: 0x0010515C File Offset: 0x0010415C
			protected virtual object RuntimeComponent
			{
				get
				{
					return this.runtimeDataSource;
				}
			}

			// Token: 0x170008B8 RID: 2232
			// (get) Token: 0x06002E14 RID: 11796 RVA: 0x00105164 File Offset: 0x00104164
			protected virtual IEnumerable RuntimeDataSource
			{
				get
				{
					return this.runtimeDataSource;
				}
			}

			// Token: 0x06002E15 RID: 11797 RVA: 0x0010516C File Offset: 0x0010416C
			protected void ClearFields()
			{
				this.dataFields = null;
			}

			// Token: 0x06002E16 RID: 11798 RVA: 0x00105175 File Offset: 0x00104175
			public override string ToString()
			{
				return this.Name;
			}

			// Token: 0x04001F53 RID: 8019
			private IEnumerable runtimeDataSource;

			// Token: 0x04001F54 RID: 8020
			private string dataSourceName;

			// Token: 0x04001F55 RID: 8021
			private PropertyDescriptorCollection dataFields;
		}

		// Token: 0x0200050C RID: 1292
		protected class ListSourceDataSourceItem : BaseDataListPage.DataSourceItem
		{
			// Token: 0x06002E17 RID: 11799 RVA: 0x0010517D File Offset: 0x0010417D
			public ListSourceDataSourceItem(string dataSourceName, IListSource runtimeListSource) : base(dataSourceName, null)
			{
				this.runtimeListSource = runtimeListSource;
			}

			// Token: 0x170008B9 RID: 2233
			// (get) Token: 0x06002E18 RID: 11800 RVA: 0x0010518E File Offset: 0x0010418E
			// (set) Token: 0x06002E19 RID: 11801 RVA: 0x00105196 File Offset: 0x00104196
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

			// Token: 0x170008BA RID: 2234
			// (get) Token: 0x06002E1A RID: 11802 RVA: 0x001051A5 File Offset: 0x001041A5
			public override bool HasDataMembers
			{
				get
				{
					return this.runtimeListSource.ContainsListCollection;
				}
			}

			// Token: 0x170008BB RID: 2235
			// (get) Token: 0x06002E1B RID: 11803 RVA: 0x001051B2 File Offset: 0x001041B2
			protected override object RuntimeComponent
			{
				get
				{
					return this.runtimeListSource;
				}
			}

			// Token: 0x170008BC RID: 2236
			// (get) Token: 0x06002E1C RID: 11804 RVA: 0x001051BA File Offset: 0x001041BA
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

			// Token: 0x04001F56 RID: 8022
			private IListSource runtimeListSource;

			// Token: 0x04001F57 RID: 8023
			private string currentDataMember;
		}
	}
}
