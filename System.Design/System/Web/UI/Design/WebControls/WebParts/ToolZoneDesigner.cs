using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Security.Permissions;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x02000531 RID: 1329
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ToolZoneDesigner : WebZoneDesigner
	{
		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06002F26 RID: 12070 RVA: 0x0010D918 File Offset: 0x0010C918
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				designerActionListCollection.Add(new ToolZoneDesigner.ToolZoneDesignerActionList(this));
				return designerActionListCollection;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06002F27 RID: 12071 RVA: 0x0010D948 File Offset: 0x0010C948
		// (set) Token: 0x06002F28 RID: 12072 RVA: 0x0010D971 File Offset: 0x0010C971
		private protected bool ViewInBrowseMode
		{
			protected get
			{
				object obj = base.DesignerState["ViewInBrowseMode"];
				return obj != null && (bool)obj;
			}
			private set
			{
				if (value != this.ViewInBrowseMode)
				{
					base.DesignerState["ViewInBrowseMode"] = value;
					this.UpdateDesignTimeHtml();
				}
			}
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x0010D998 File Offset: 0x0010C998
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(ToolZone));
			base.Initialize(component);
		}

		// Token: 0x02000532 RID: 1330
		private class ToolZoneDesignerActionList : DesignerActionList
		{
			// Token: 0x06002F2B RID: 12075 RVA: 0x0010D9B9 File Offset: 0x0010C9B9
			public ToolZoneDesignerActionList(ToolZoneDesigner parent) : base(parent.Component)
			{
				this._parent = parent;
			}

			// Token: 0x170008E9 RID: 2281
			// (get) Token: 0x06002F2C RID: 12076 RVA: 0x0010D9CE File Offset: 0x0010C9CE
			// (set) Token: 0x06002F2D RID: 12077 RVA: 0x0010D9D1 File Offset: 0x0010C9D1
			public override bool AutoShow
			{
				get
				{
					return true;
				}
				set
				{
				}
			}

			// Token: 0x170008EA RID: 2282
			// (get) Token: 0x06002F2E RID: 12078 RVA: 0x0010D9D3 File Offset: 0x0010C9D3
			// (set) Token: 0x06002F2F RID: 12079 RVA: 0x0010D9E0 File Offset: 0x0010C9E0
			public bool ViewInBrowseMode
			{
				get
				{
					return this._parent.ViewInBrowseMode;
				}
				set
				{
					this._parent.ViewInBrowseMode = value;
				}
			}

			// Token: 0x06002F30 RID: 12080 RVA: 0x0010D9F0 File Offset: 0x0010C9F0
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				return new DesignerActionItemCollection
				{
					new DesignerActionPropertyItem("ViewInBrowseMode", SR.GetString("ToolZoneDesigner_ViewInBrowseMode"), string.Empty, SR.GetString("ToolZoneDesigner_ViewInBrowseModeDesc"))
				};
			}

			// Token: 0x0400202E RID: 8238
			private ToolZoneDesigner _parent;
		}
	}
}
