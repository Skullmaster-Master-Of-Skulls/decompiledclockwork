using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002A3 RID: 675
	internal class ComboBoxDesigner : ControlDesigner
	{
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x000947A8 File Offset: 0x000929A8
		public override IList SnapLines
		{
			get
			{
				ArrayList arrayList = base.SnapLines as ArrayList;
				int num = DesignerUtils.GetTextBaseline(this.Control, ContentAlignment.TopLeft);
				num += 3;
				arrayList.Add(new SnapLine(SnapLineType.Baseline, num, SnapLinePriority.Medium));
				return arrayList;
			}
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x000947E2 File Offset: 0x000929E2
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.propChanged != null)
			{
				((ComboBox)this.Control).StyleChanged -= this.propChanged;
			}
			base.Dispose(disposing);
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x0009480C File Offset: 0x00092A0C
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			base.AutoResizeHandles = true;
			this.propChanged = new EventHandler(this.OnControlPropertyChanged);
			((ComboBox)this.Control).StyleChanged += this.propChanged;
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x00094844 File Offset: 0x00092A44
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			base.InitializeNewComponent(defaultValues);
			((ComboBox)base.Component).FormattingEnabled = true;
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["Text"];
			if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(string) && !propertyDescriptor.IsReadOnly && propertyDescriptor.IsBrowsable)
			{
				propertyDescriptor.SetValue(base.Component, "");
			}
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x000948BA File Offset: 0x00092ABA
		private void OnControlPropertyChanged(object sender, EventArgs e)
		{
			if (base.BehaviorService != null)
			{
				base.BehaviorService.SyncSelection();
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001A01 RID: 6657 RVA: 0x000948D0 File Offset: 0x00092AD0
		public override SelectionRules SelectionRules
		{
			get
			{
				SelectionRules selectionRules = base.SelectionRules;
				object component = base.Component;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)["DropDownStyle"];
				if (propertyDescriptor != null)
				{
					ComboBoxStyle comboBoxStyle = (ComboBoxStyle)propertyDescriptor.GetValue(component);
					if (comboBoxStyle == ComboBoxStyle.DropDown || comboBoxStyle == ComboBoxStyle.DropDownList)
					{
						selectionRules &= ~(SelectionRules.TopSizeable | SelectionRules.BottomSizeable);
					}
				}
				return selectionRules;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x0009491A File Offset: 0x00092B1A
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				if (this._actionLists == null)
				{
					this._actionLists = new DesignerActionListCollection();
					this._actionLists.Add(new ListControlBoundActionList(this));
				}
				return this._actionLists;
			}
		}

		// Token: 0x040015C9 RID: 5577
		private EventHandler propChanged;

		// Token: 0x040015CA RID: 5578
		private DesignerActionListCollection _actionLists;
	}
}
