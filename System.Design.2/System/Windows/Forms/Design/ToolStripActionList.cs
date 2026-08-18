using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200034B RID: 843
	internal class ToolStripActionList : DesignerActionList
	{
		// Token: 0x0600214F RID: 8527 RVA: 0x000CB624 File Offset: 0x000C9824
		public ToolStripActionList(ToolStripDesigner designer) : base(designer.Component)
		{
			this._toolStrip = (ToolStrip)designer.Component;
			this.designer = designer;
			this.changeParentVerb = new ChangeToolStripParentVerb(SR.GetString("ToolStripDesignerEmbedVerb"), designer);
			if (!(this._toolStrip is StatusStrip))
			{
				this.standardItemsVerb = new StandardMenuStripVerb(SR.GetString("ToolStripDesignerStandardItemsVerb"), designer);
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06002150 RID: 8528 RVA: 0x000CB690 File Offset: 0x000C9890
		private bool CanAddItems
		{
			get
			{
				InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(this._toolStrip)[typeof(InheritanceAttribute)];
				return inheritanceAttribute == null || inheritanceAttribute.InheritanceLevel == InheritanceLevel.NotInherited;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06002151 RID: 8529 RVA: 0x000CB6CC File Offset: 0x000C98CC
		private bool IsReadOnly
		{
			get
			{
				InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(this._toolStrip)[typeof(InheritanceAttribute)];
				return inheritanceAttribute == null || inheritanceAttribute.InheritanceLevel == InheritanceLevel.InheritedReadOnly;
			}
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x000CB708 File Offset: 0x000C9908
		private object GetProperty(string propertyName)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this._toolStrip)[propertyName];
			if (propertyDescriptor != null)
			{
				return propertyDescriptor.GetValue(this._toolStrip);
			}
			return null;
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x000CB738 File Offset: 0x000C9938
		private void ChangeProperty(string propertyName, object value)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this._toolStrip)[propertyName];
			if (propertyDescriptor != null)
			{
				propertyDescriptor.SetValue(this._toolStrip, value);
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06002154 RID: 8532 RVA: 0x000CB767 File Offset: 0x000C9967
		// (set) Token: 0x06002155 RID: 8533 RVA: 0x000CB76F File Offset: 0x000C996F
		public override bool AutoShow
		{
			get
			{
				return this._autoShow;
			}
			set
			{
				if (this._autoShow != value)
				{
					this._autoShow = value;
				}
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06002156 RID: 8534 RVA: 0x000CB781 File Offset: 0x000C9981
		// (set) Token: 0x06002157 RID: 8535 RVA: 0x000CB793 File Offset: 0x000C9993
		public DockStyle Dock
		{
			get
			{
				return (DockStyle)this.GetProperty("Dock");
			}
			set
			{
				if (value != this.Dock)
				{
					this.ChangeProperty("Dock", value);
				}
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x000CB7AF File Offset: 0x000C99AF
		// (set) Token: 0x06002159 RID: 8537 RVA: 0x000CB7C1 File Offset: 0x000C99C1
		public ToolStripRenderMode RenderMode
		{
			get
			{
				return (ToolStripRenderMode)this.GetProperty("RenderMode");
			}
			set
			{
				if (value != this.RenderMode)
				{
					this.ChangeProperty("RenderMode", value);
				}
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x0600215A RID: 8538 RVA: 0x000CB7DD File Offset: 0x000C99DD
		// (set) Token: 0x0600215B RID: 8539 RVA: 0x000CB7EF File Offset: 0x000C99EF
		public ToolStripGripStyle GripStyle
		{
			get
			{
				return (ToolStripGripStyle)this.GetProperty("GripStyle");
			}
			set
			{
				if (value != this.GripStyle)
				{
					this.ChangeProperty("GripStyle", value);
				}
			}
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x000CB80C File Offset: 0x000C9A0C
		private void InvokeEmbedVerb()
		{
			DesignerActionUIService designerActionUIService = (DesignerActionUIService)this._toolStrip.Site.GetService(typeof(DesignerActionUIService));
			if (designerActionUIService != null)
			{
				designerActionUIService.HideUI(this._toolStrip);
			}
			this.changeParentVerb.ChangeParent();
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x000CB853 File Offset: 0x000C9A53
		private void InvokeInsertStandardItemsVerb()
		{
			this.standardItemsVerb.InsertItems();
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x000CB860 File Offset: 0x000C9A60
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
			if (!this.IsReadOnly)
			{
				designerActionItemCollection.Add(new DesignerActionMethodItem(this, "InvokeEmbedVerb", SR.GetString("ToolStripDesignerEmbedVerb"), "", SR.GetString("ToolStripDesignerEmbedVerbDesc"), true));
			}
			if (this.CanAddItems)
			{
				if (!(this._toolStrip is StatusStrip))
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "InvokeInsertStandardItemsVerb", SR.GetString("ToolStripDesignerStandardItemsVerb"), "", SR.GetString("ToolStripDesignerStandardItemsVerbDesc"), true));
				}
				designerActionItemCollection.Add(new DesignerActionPropertyItem("RenderMode", SR.GetString("ToolStripActionList_RenderMode"), SR.GetString("ToolStripActionList_Layout"), SR.GetString("ToolStripActionList_RenderModeDesc")));
			}
			if (!(this._toolStrip.Parent is ToolStripPanel))
			{
				designerActionItemCollection.Add(new DesignerActionPropertyItem("Dock", SR.GetString("ToolStripActionList_Dock"), SR.GetString("ToolStripActionList_Layout"), SR.GetString("ToolStripActionList_DockDesc")));
			}
			if (!(this._toolStrip is StatusStrip))
			{
				designerActionItemCollection.Add(new DesignerActionPropertyItem("GripStyle", SR.GetString("ToolStripActionList_GripStyle"), SR.GetString("ToolStripActionList_Layout"), SR.GetString("ToolStripActionList_GripStyleDesc")));
			}
			return designerActionItemCollection;
		}

		// Token: 0x04001932 RID: 6450
		private ToolStrip _toolStrip;

		// Token: 0x04001933 RID: 6451
		private bool _autoShow;

		// Token: 0x04001934 RID: 6452
		private ToolStripDesigner designer;

		// Token: 0x04001935 RID: 6453
		private ChangeToolStripParentVerb changeParentVerb;

		// Token: 0x04001936 RID: 6454
		private StandardMenuStripVerb standardItemsVerb;
	}
}
