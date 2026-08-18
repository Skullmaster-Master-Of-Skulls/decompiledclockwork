using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Security.Permissions;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x02000150 RID: 336
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ToolZoneDesigner : WebZoneDesigner
	{
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x0004B434 File Offset: 0x00049634
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

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0004B464 File Offset: 0x00049664
		// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x0004B48D File Offset: 0x0004968D
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

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0004B4B4 File Offset: 0x000496B4
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(ToolZone));
			base.Initialize(component);
		}

		// Token: 0x0200045F RID: 1119
		private class ToolZoneDesignerActionList : DesignerActionList
		{
			// Token: 0x06002988 RID: 10632 RVA: 0x000FAB2F File Offset: 0x000F8D2F
			public ToolZoneDesignerActionList(ToolZoneDesigner parent) : base(parent.Component)
			{
				this._parent = parent;
			}

			// Token: 0x170008C8 RID: 2248
			// (get) Token: 0x06002989 RID: 10633 RVA: 0x00003B0F File Offset: 0x00001D0F
			// (set) Token: 0x0600298A RID: 10634 RVA: 0x00003937 File Offset: 0x00001B37
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

			// Token: 0x170008C9 RID: 2249
			// (get) Token: 0x0600298B RID: 10635 RVA: 0x000FAB44 File Offset: 0x000F8D44
			// (set) Token: 0x0600298C RID: 10636 RVA: 0x000FAB51 File Offset: 0x000F8D51
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

			// Token: 0x0600298D RID: 10637 RVA: 0x000FAB60 File Offset: 0x000F8D60
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				return new DesignerActionItemCollection
				{
					new DesignerActionPropertyItem("ViewInBrowseMode", SR.GetString("ToolZoneDesigner_ViewInBrowseMode"), string.Empty, SR.GetString("ToolZoneDesigner_ViewInBrowseModeDesc"))
					{
						ShowInSourceView = false
					}
				};
			}

			// Token: 0x04001D50 RID: 7504
			private ToolZoneDesigner _parent;
		}
	}
}
