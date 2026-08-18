using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001906 RID: 6406
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridNestedViewSettings : ObjectWithState
	{
		// Token: 0x0600F865 RID: 63589 RVA: 0x003823C9 File Offset: 0x003805C9
		public GridNestedViewSettings(StateBag OwnerStateBag, GridTableView OwnerTableView) : base("nvsettings_", OwnerStateBag)
		{
			this.owner = OwnerTableView;
		}

		// Token: 0x17004B0D RID: 19213
		// (get) Token: 0x0600F866 RID: 63590 RVA: 0x003823E0 File Offset: 0x003805E0
		// (set) Token: 0x0600F867 RID: 63591 RVA: 0x0038240D File Offset: 0x0038060D
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[DefaultValue("")]
		public string DataSourceID
		{
			get
			{
				object obj = base.ViewState["DataSourceID"];
				if (obj == null)
				{
					obj = "";
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["DataSourceID"] = value;
			}
		}

		// Token: 0x17004B0E RID: 19214
		// (get) Token: 0x0600F868 RID: 63592 RVA: 0x00382420 File Offset: 0x00380620
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public GridTableViewRelation ParentTableRelation
		{
			get
			{
				if (this._parentTableRelation == null)
				{
					this._parentTableRelation = new GridTableViewRelation();
				}
				return this._parentTableRelation;
			}
		}

		// Token: 0x040046C2 RID: 18114
		private readonly GridTableView owner;

		// Token: 0x040046C3 RID: 18115
		private GridTableViewRelation _parentTableRelation;
	}
}
