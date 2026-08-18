using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020001CD RID: 461
	internal class ContextMenuStripActionList : DesignerActionList
	{
		// Token: 0x060011EA RID: 4586 RVA: 0x000571E9 File Offset: 0x000561E9
		public ContextMenuStripActionList(ToolStripDropDownDesigner designer) : base(designer.Component)
		{
			this._toolStripDropDown = (ToolStripDropDown)designer.Component;
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00057208 File Offset: 0x00056208
		private object GetProperty(string propertyName)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this._toolStripDropDown)[propertyName];
			if (propertyDescriptor != null)
			{
				return propertyDescriptor.GetValue(this._toolStripDropDown);
			}
			return null;
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00057238 File Offset: 0x00056238
		private void ChangeProperty(string propertyName, object value)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this._toolStripDropDown)[propertyName];
			if (propertyDescriptor != null)
			{
				propertyDescriptor.SetValue(this._toolStripDropDown, value);
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x00057267 File Offset: 0x00056267
		// (set) Token: 0x060011EE RID: 4590 RVA: 0x0005726F File Offset: 0x0005626F
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

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x00057281 File Offset: 0x00056281
		// (set) Token: 0x060011F0 RID: 4592 RVA: 0x00057293 File Offset: 0x00056293
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

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x000572AF File Offset: 0x000562AF
		// (set) Token: 0x060011F2 RID: 4594 RVA: 0x000572C1 File Offset: 0x000562C1
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

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x000572DD File Offset: 0x000562DD
		// (set) Token: 0x060011F4 RID: 4596 RVA: 0x000572EF File Offset: 0x000562EF
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

		// Token: 0x060011F5 RID: 4597 RVA: 0x0005730C File Offset: 0x0005630C
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
			designerActionItemCollection.Add(new DesignerActionPropertyItem(SR.GetString("ToolStripActionList_RenderMode"), SR.GetString("ToolStripActionList_RenderMode"), SR.GetString("ToolStripActionList_Layout"), SR.GetString("ToolStripActionList_RenderModeDesc")));
			if (this._toolStripDropDown is ToolStripDropDownMenu)
			{
				designerActionItemCollection.Add(new DesignerActionPropertyItem(SR.GetString("ContextMenuStripActionList_ShowImageMargin"), SR.GetString("ContextMenuStripActionList_ShowImageMargin"), SR.GetString("ToolStripActionList_Layout"), SR.GetString("ContextMenuStripActionList_ShowImageMarginDesc")));
				designerActionItemCollection.Add(new DesignerActionPropertyItem(SR.GetString("ContextMenuStripActionList_ShowCheckMargin"), SR.GetString("ContextMenuStripActionList_ShowCheckMargin"), SR.GetString("ToolStripActionList_Layout"), SR.GetString("ContextMenuStripActionList_ShowCheckMarginDesc")));
			}
			return designerActionItemCollection;
		}

		// Token: 0x040010F5 RID: 4341
		private ToolStripDropDown _toolStripDropDown;

		// Token: 0x040010F6 RID: 4342
		private bool _autoShow;
	}
}
