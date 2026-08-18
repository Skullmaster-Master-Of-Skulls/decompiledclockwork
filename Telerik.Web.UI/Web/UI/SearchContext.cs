using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000874 RID: 2164
	public class SearchContext : IDisposable
	{
		// Token: 0x17001A1D RID: 6685
		// (get) Token: 0x06004FE2 RID: 20450 RVA: 0x000FA63B File Offset: 0x000F883B
		internal SearchContextControl ContextControl
		{
			[DebuggerStepThrough]
			get
			{
				if (this._contextControl == null)
				{
					this._contextControl = new SearchContextControl();
				}
				return this._contextControl;
			}
		}

		// Token: 0x17001A1E RID: 6686
		// (get) Token: 0x06004FE3 RID: 20451 RVA: 0x000FA656 File Offset: 0x000F8856
		// (set) Token: 0x06004FE4 RID: 20452 RVA: 0x000FA663 File Offset: 0x000F8863
		[Description("SelectedIndex")]
		[SimplePersistenceSetting]
		[Bindable(true)]
		[Browsable(false)]
		[DefaultValue(-1)]
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int SelectedIndex
		{
			get
			{
				return this.ContextControl.SelectedIndex;
			}
			set
			{
				this.ContextControl.SelectedIndex = value;
			}
		}

		// Token: 0x17001A1F RID: 6687
		// (get) Token: 0x06004FE5 RID: 20453 RVA: 0x000FA671 File Offset: 0x000F8871
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(false)]
		[DefaultValue(null)]
		[Category("Behavior")]
		[Browsable(false)]
		public virtual SearchContextItem SelectedItem
		{
			get
			{
				return this.ContextControl.SelectedItem;
			}
		}

		// Token: 0x17001A20 RID: 6688
		// (get) Token: 0x06004FE6 RID: 20454 RVA: 0x000FA67E File Offset: 0x000F887E
		// (set) Token: 0x06004FE7 RID: 20455 RVA: 0x000FA68B File Offset: 0x000F888B
		public object DataSource
		{
			get
			{
				return this.ContextControl.DataSource;
			}
			set
			{
				this.ContextControl.DataSource = value;
			}
		}

		// Token: 0x17001A21 RID: 6689
		// (get) Token: 0x06004FE8 RID: 20456 RVA: 0x000FA699 File Offset: 0x000F8899
		// (set) Token: 0x06004FE9 RID: 20457 RVA: 0x000FA6A6 File Offset: 0x000F88A6
		public string DataSourceID
		{
			get
			{
				return this.ContextControl.DataSourceID;
			}
			set
			{
				this.ContextControl.DataSourceID = value;
			}
		}

		// Token: 0x17001A22 RID: 6690
		// (get) Token: 0x06004FEA RID: 20458 RVA: 0x000FA6B4 File Offset: 0x000F88B4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public SearchContextItemCollection Items
		{
			get
			{
				return this.ContextControl.Items;
			}
		}

		// Token: 0x17001A23 RID: 6691
		// (get) Token: 0x06004FEB RID: 20459 RVA: 0x000FA6C1 File Offset: 0x000F88C1
		// (set) Token: 0x06004FEC RID: 20460 RVA: 0x000FA6CE File Offset: 0x000F88CE
		[DefaultValue("")]
		[Category("Data")]
		public virtual string DataTextField
		{
			get
			{
				return this.ContextControl.DataTextField;
			}
			set
			{
				this.ContextControl.DataTextField = value;
			}
		}

		// Token: 0x17001A24 RID: 6692
		// (get) Token: 0x06004FED RID: 20461 RVA: 0x000FA6DC File Offset: 0x000F88DC
		// (set) Token: 0x06004FEE RID: 20462 RVA: 0x000FA6E9 File Offset: 0x000F88E9
		[DefaultValue("")]
		[Category("Data")]
		public virtual string DataKeyField
		{
			get
			{
				return this.ContextControl.DataKeyField;
			}
			set
			{
				this.ContextControl.DataKeyField = value;
			}
		}

		// Token: 0x17001A25 RID: 6693
		// (get) Token: 0x06004FEF RID: 20463 RVA: 0x000FA6F7 File Offset: 0x000F88F7
		// (set) Token: 0x06004FF0 RID: 20464 RVA: 0x000FA704 File Offset: 0x000F8904
		[DefaultValue("")]
		[Category("Data")]
		public virtual string DataModelID
		{
			get
			{
				return this.ContextControl.DataModelID;
			}
			set
			{
				this.ContextControl.DataModelID = value;
			}
		}

		// Token: 0x17001A26 RID: 6694
		// (get) Token: 0x06004FF1 RID: 20465 RVA: 0x000FA712 File Offset: 0x000F8912
		// (set) Token: 0x06004FF2 RID: 20466 RVA: 0x000FA71F File Offset: 0x000F891F
		[DefaultValue(true)]
		[Category("Behavior")]
		public virtual bool ShowDefaultItem
		{
			get
			{
				return this.ContextControl.ShowDefaultItem;
			}
			set
			{
				this.ContextControl.ShowDefaultItem = value;
			}
		}

		// Token: 0x17001A27 RID: 6695
		// (get) Token: 0x06004FF3 RID: 20467 RVA: 0x000FA72D File Offset: 0x000F892D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Gets the settings(service path and method name)for the web service used to provide items for the search context.")]
		[Category("Behavior")]
		public WebServiceSettings WebServiceSettings
		{
			get
			{
				return this.ContextControl.WebServiceSettings;
			}
		}

		// Token: 0x17001A28 RID: 6696
		// (get) Token: 0x06004FF4 RID: 20468 RVA: 0x000FA73A File Offset: 0x000F893A
		// (set) Token: 0x06004FF5 RID: 20469 RVA: 0x000FA747 File Offset: 0x000F8947
		public virtual short TabIndex
		{
			get
			{
				return this.ContextControl.TabIndex;
			}
			set
			{
				this.ContextControl.TabIndex = value;
			}
		}

		// Token: 0x17001A29 RID: 6697
		// (get) Token: 0x06004FF6 RID: 20470 RVA: 0x000FA755 File Offset: 0x000F8955
		// (set) Token: 0x06004FF7 RID: 20471 RVA: 0x000FA762 File Offset: 0x000F8962
		[TypeConverter(typeof(UnitConverter))]
		[DefaultValue(typeof(Unit), "")]
		[Description("The width of the SearchContext")]
		public Unit Width
		{
			get
			{
				return this.ContextControl.Width;
			}
			set
			{
				this.ContextControl.Width = value;
			}
		}

		// Token: 0x17001A2A RID: 6698
		// (get) Token: 0x06004FF8 RID: 20472 RVA: 0x000FA770 File Offset: 0x000F8970
		// (set) Token: 0x06004FF9 RID: 20473 RVA: 0x000FA77D File Offset: 0x000F897D
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("Css class of the dropdown")]
		public string DropDownCssClass
		{
			get
			{
				return this.ContextControl.DropDownCssClass;
			}
			set
			{
				this.ContextControl.DropDownCssClass = value;
			}
		}

		// Token: 0x17001A2B RID: 6699
		// (get) Token: 0x06004FFA RID: 20474 RVA: 0x000FA78B File Offset: 0x000F898B
		// (set) Token: 0x06004FFB RID: 20475 RVA: 0x000FA798 File Offset: 0x000F8998
		[Description("Css class of the search context element")]
		[DefaultValue("")]
		[Category("Appearance")]
		public string CssClass
		{
			get
			{
				return this.ContextControl.CssClass;
			}
			set
			{
				this.ContextControl.CssClass = value;
			}
		}

		// Token: 0x17001A2C RID: 6700
		// (get) Token: 0x06004FFC RID: 20476 RVA: 0x000FA7A6 File Offset: 0x000F89A6
		// (set) Token: 0x06004FFD RID: 20477 RVA: 0x000FA7B3 File Offset: 0x000F89B3
		public virtual bool Enabled
		{
			get
			{
				return this.ContextControl.Enabled;
			}
			set
			{
				this.ContextControl.Enabled = value;
			}
		}

		// Token: 0x140000CB RID: 203
		// (add) Token: 0x06004FFE RID: 20478 RVA: 0x000FA7C1 File Offset: 0x000F89C1
		// (remove) Token: 0x06004FFF RID: 20479 RVA: 0x000FA7CF File Offset: 0x000F89CF
		public event SearchBoxContextItemEventHandler ItemDataBound
		{
			add
			{
				this.ContextControl.ItemDataBound += value;
			}
			remove
			{
				this.ContextControl.ItemDataBound -= value;
			}
		}

		// Token: 0x17001A2D RID: 6701
		// (get) Token: 0x06005000 RID: 20480 RVA: 0x000FA7DD File Offset: 0x000F89DD
		// (set) Token: 0x06005001 RID: 20481 RVA: 0x000FA7EA File Offset: 0x000F89EA
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[Description("The JavaScript function executed when the RadSearchBox is bound to RadODataDataSource control and a SearchContextItem is being populated.")]
		public string OnClientItemDataBound
		{
			get
			{
				return this.ContextControl.OnClientItemDataBound;
			}
			set
			{
				this.ContextControl.OnClientItemDataBound = value;
			}
		}

		// Token: 0x06005002 RID: 20482 RVA: 0x000FA7F8 File Offset: 0x000F89F8
		public void DataBind()
		{
			if (this._contextControl != null)
			{
				this.ContextControl.DataBind();
			}
		}

		// Token: 0x06005003 RID: 20483 RVA: 0x000FA80D File Offset: 0x000F8A0D
		public void ClearSelection()
		{
			if (this._contextControl != null)
			{
				this.ContextControl.ClearSelection();
			}
		}

		// Token: 0x06005004 RID: 20484 RVA: 0x000FA822 File Offset: 0x000F8A22
		[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005005 RID: 20485 RVA: 0x000FA831 File Offset: 0x000F8A31
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._contextControl != null)
			{
				this._contextControl.Dispose();
				this._contextControl = null;
			}
		}

		// Token: 0x040013DC RID: 5084
		private SearchContextControl _contextControl;
	}
}
