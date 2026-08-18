using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002AB RID: 683
	internal class ContextMenuStripActionList : DesignerActionList
	{
		// Token: 0x06001AB4 RID: 6836 RVA: 0x0009C248 File Offset: 0x0009A448
		public ContextMenuStripActionList(ToolStripDropDownDesigner designer) : base(designer.Component)
		{
			this._toolStripDropDown = (ToolStripDropDown)designer.Component;
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x0009C268 File Offset: 0x0009A468
		private object GetProperty(string propertyName)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this._toolStripDropDown)[propertyName];
			if (propertyDescriptor != null)
			{
				return propertyDescriptor.GetValue(this._toolStripDropDown);
			}
			return null;
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x0009C298 File Offset: 0x0009A498
		private void ChangeProperty(string propertyName, object value)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this._toolStripDropDown)[propertyName];
			if (propertyDescriptor != null)
			{
				propertyDescriptor.SetValue(this._toolStripDropDown, value);
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x0009C2C7 File Offset: 0x0009A4C7
		// (set) Token: 0x06001AB8 RID: 6840 RVA: 0x0009C2CF File Offset: 0x0009A4CF
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

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x0009C2E1 File Offset: 0x0009A4E1
		// (set) Token: 0x06001ABA RID: 6842 RVA: 0x0009C2F3 File Offset: 0x0009A4F3
		public bool ShowImageMargin
		{
			get
			{
				return (bool)this.GetProperty("ShowImageMargin");
			}
			set
			{
				if (value != this.ShowImageMargin)
				{
					this.ChangeProperty("ShowImageMargin", value);
				}
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001ABB RID: 6843 RVA: 0x0009C30F File Offset: 0x0009A50F
		// (set) Token: 0x06001ABC RID: 6844 RVA: 0x0009C321 File Offset: 0x0009A521
		public bool ShowCheckMargin
		{
			get
			{
				return (bool)this.GetProperty("ShowCheckMargin");
			}
			set
			{
				if (value != this.ShowCheckMargin)
				{
					this.ChangeProperty("ShowCheckMargin", value);
				}
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x0009C33D File Offset: 0x0009A53D
		// (set) Token: 0x06001ABE RID: 6846 RVA: 0x0009C34F File Offset: 0x0009A54F
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

		// Token: 0x06001ABF RID: 6847 RVA: 0x0009C36C File Offset: 0x0009A56C
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
			designerActionItemCollection.Add(new DesignerActionPropertyItem("RenderMode", SR.GetString("ToolStripActionList_RenderMode"), SR.GetString("ToolStripActionList_Layout"), SR.GetString("ToolStripActionList_RenderModeDesc")));
			if (this._toolStripDropDown is ToolStripDropDownMenu)
			{
				designerActionItemCollection.Add(new DesignerActionPropertyItem("ShowImageMargin", SR.GetString("ContextMenuStripActionList_ShowImageMargin"), SR.GetString("ToolStripActionList_Layout"), SR.GetString("ContextMenuStripActionList_ShowImageMarginDesc")));
				designerActionItemCollection.Add(new DesignerActionPropertyItem("ShowCheckMargin", SR.GetString("ContextMenuStripActionList_ShowCheckMargin"), SR.GetString("ToolStripActionList_Layout"), SR.GetString("ContextMenuStripActionList_ShowCheckMarginDesc")));
			}
			return designerActionItemCollection;
		}

		// Token: 0x0400160C RID: 5644
		private ToolStripDropDown _toolStripDropDown;

		// Token: 0x0400160D RID: 5645
		private bool _autoShow;
	}
}
