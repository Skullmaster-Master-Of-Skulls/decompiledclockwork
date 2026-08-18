using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Diagnostics;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x02000391 RID: 913
	internal class TableLayoutPanelBehavior : Behavior
	{
		// Token: 0x06002541 RID: 9537 RVA: 0x000E8FDC File Offset: 0x000E71DC
		internal TableLayoutPanelBehavior(TableLayoutPanel panel, TableLayoutPanelDesigner designer, IServiceProvider serviceProvider)
		{
			this.table = panel;
			this.designer = designer;
			this.serviceProvider = serviceProvider;
			this.behaviorService = (serviceProvider.GetService(typeof(BehaviorService)) as BehaviorService);
			if (this.behaviorService == null)
			{
				return;
			}
			this.pushedBehavior = false;
			this.lastMouseLoc = Point.Empty;
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x000E903C File Offset: 0x000E723C
		private void FinishResize()
		{
			this.pushedBehavior = false;
			this.behaviorService.PopBehavior(this);
			this.lastMouseLoc = Point.Empty;
			this.styles = null;
			IComponentChangeService componentChangeService = this.serviceProvider.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
			if (componentChangeService != null && this.changedProp != null)
			{
				componentChangeService.OnComponentChanged(this.table, this.changedProp, null, null);
				this.changedProp = null;
			}
			SelectionManager selectionManager = this.serviceProvider.GetService(typeof(SelectionManager)) as SelectionManager;
			if (selectionManager != null)
			{
				selectionManager.Refresh();
			}
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x000E90D4 File Offset: 0x000E72D4
		public override void OnLoseCapture(Glyph g, EventArgs e)
		{
			if (this.pushedBehavior)
			{
				this.FinishResize();
				if (this.resizeTransaction != null)
				{
					DesignerTransaction designerTransaction = this.resizeTransaction;
					this.resizeTransaction = null;
					using (designerTransaction)
					{
						designerTransaction.Cancel();
					}
				}
			}
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x000E912C File Offset: 0x000E732C
		public override bool OnMouseDown(Glyph g, MouseButtons button, Point mouseLoc)
		{
			if (button == MouseButtons.Left && g is TableLayoutPanelResizeGlyph)
			{
				this.tableGlyph = (g as TableLayoutPanelResizeGlyph);
				ISelectionService selectionService = this.serviceProvider.GetService(typeof(ISelectionService)) as ISelectionService;
				if (selectionService != null)
				{
					selectionService.SetSelectedComponents(new object[]
					{
						this.designer.Component
					}, SelectionTypes.Click);
				}
				bool flag = this.tableGlyph.Type == TableLayoutPanelResizeGlyph.TableLayoutResizeType.Column;
				this.lastMouseLoc = mouseLoc;
				this.resizeProp = TypeDescriptor.GetProperties(this.tableGlyph.Style)[flag ? "Width" : "Height"];
				IComponentChangeService componentChangeService = this.serviceProvider.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
				if (componentChangeService != null)
				{
					this.changedProp = TypeDescriptor.GetProperties(this.table)[flag ? "ColumnStyles" : "RowStyles"];
					int[] widths = flag ? this.table.GetColumnWidths() : this.table.GetRowHeights();
					if (this.changedProp != null)
					{
						this.GetActiveStyleCollection(flag);
						if (this.styles != null && this.CanResizeStyle(widths))
						{
							IDesignerHost designerHost = this.serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
							if (designerHost != null)
							{
								this.resizeTransaction = designerHost.CreateTransaction(SR.GetString("TableLayoutPanelRowColResize", new object[]
								{
									flag ? "Column" : "Row",
									this.designer.Control.Site.Name
								}));
							}
							try
							{
								int startIndex = this.styles.IndexOf(this.tableGlyph.Style);
								this.rightStyle.index = this.IndexOfNextStealableStyle(true, startIndex, widths);
								this.rightStyle.style = (TableLayoutStyle)this.styles[this.rightStyle.index];
								this.rightStyle.styleProp = TypeDescriptor.GetProperties(this.rightStyle.style)[flag ? "Width" : "Height"];
								this.leftStyle.index = this.IndexOfNextStealableStyle(false, startIndex, widths);
								this.leftStyle.style = (TableLayoutStyle)this.styles[this.leftStyle.index];
								this.leftStyle.styleProp = TypeDescriptor.GetProperties(this.leftStyle.style)[flag ? "Width" : "Height"];
								componentChangeService.OnComponentChanging(this.table, this.changedProp);
								goto IL_2C3;
							}
							catch (CheckoutException obj)
							{
								if (CheckoutException.Canceled.Equals(obj) && this.resizeTransaction != null && !this.resizeTransaction.Canceled)
								{
									this.resizeTransaction.Cancel();
								}
								throw;
							}
						}
						return false;
					}
				}
				IL_2C3:
				this.behaviorService.PushCaptureBehavior(this);
				this.pushedBehavior = true;
			}
			return false;
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x000E942C File Offset: 0x000E762C
		private void GetActiveStyleCollection(bool isColumn)
		{
			if ((this.styles == null || isColumn != this.currentColumnStyles) && this.table != null)
			{
				this.styles = new ArrayList(this.changedProp.GetValue(this.table) as TableLayoutStyleCollection);
				this.currentColumnStyles = isColumn;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06002546 RID: 9542 RVA: 0x000E947C File Offset: 0x000E767C
		private bool ColumnResize
		{
			get
			{
				bool result = false;
				if (this.tableGlyph != null)
				{
					result = (this.tableGlyph.Type == TableLayoutPanelResizeGlyph.TableLayoutResizeType.Column);
				}
				return result;
			}
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x000E94A4 File Offset: 0x000E76A4
		private bool CanResizeStyle(int[] widths)
		{
			int num = ((IList)this.styles).IndexOf(this.tableGlyph.Style);
			if (num > -1 && num != this.styles.Count)
			{
				bool flag = this.IndexOfNextStealableStyle(true, num, widths) != -1;
				bool flag2 = this.IndexOfNextStealableStyle(false, num, widths) != -1;
				return flag && flag2;
			}
			return false;
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x000E9508 File Offset: 0x000E7708
		private int IndexOfNextStealableStyle(bool forward, int startIndex, int[] widths)
		{
			int result = -1;
			if (this.styles != null)
			{
				if (forward)
				{
					for (int i = startIndex + 1; i < this.styles.Count; i++)
					{
						if (i >= widths.Length)
						{
							break;
						}
						if (((TableLayoutStyle)this.styles[i]).SizeType != SizeType.AutoSize && widths[i] >= DesignerUtils.MINUMUMSTYLESIZEDRAG)
						{
							result = i;
							break;
						}
					}
				}
				else if (startIndex < widths.Length)
				{
					for (int j = startIndex; j >= 0; j--)
					{
						if (((TableLayoutStyle)this.styles[j]).SizeType != SizeType.AutoSize && widths[j] >= DesignerUtils.MINUMUMSTYLESIZEDRAG)
						{
							result = j;
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x000E95A4 File Offset: 0x000E77A4
		public override bool OnMouseMove(Glyph g, MouseButtons button, Point mouseLoc)
		{
			if (this.pushedBehavior)
			{
				bool columnResize = this.ColumnResize;
				this.GetActiveStyleCollection(columnResize);
				if (this.styles != null)
				{
					int index = this.rightStyle.index;
					int index2 = this.leftStyle.index;
					int num = columnResize ? (mouseLoc.X - this.lastMouseLoc.X) : (mouseLoc.Y - this.lastMouseLoc.Y);
					if (columnResize && this.table.RightToLeft == RightToLeft.Yes)
					{
						num *= -1;
					}
					if (num == 0)
					{
						return false;
					}
					int[] array = columnResize ? this.table.GetColumnWidths() : this.table.GetRowHeights();
					int[] array2 = array.Clone() as int[];
					array2[index] -= num;
					array2[index2] += num;
					if (array2[index] < DesignerUtils.MINUMUMSTYLESIZEDRAG || array2[index2] < DesignerUtils.MINUMUMSTYLESIZEDRAG)
					{
						return false;
					}
					this.table.SuspendLayout();
					int num2 = 0;
					if (((TableLayoutStyle)this.styles[index]).SizeType == SizeType.Absolute && ((TableLayoutStyle)this.styles[index2]).SizeType == SizeType.Absolute)
					{
						float num3 = (float)array2[index];
						float num4 = (float)this.rightStyle.styleProp.GetValue(this.rightStyle.style);
						if (num4 != (float)array[index])
						{
							num3 = Math.Max(num4 - (float)num, (float)DesignerUtils.MINUMUMSTYLESIZEDRAG);
						}
						float num5 = (float)array2[index2];
						float num6 = (float)this.leftStyle.styleProp.GetValue(this.leftStyle.style);
						if (num6 != (float)array[index2])
						{
							num5 = Math.Max(num6 + (float)num, (float)DesignerUtils.MINUMUMSTYLESIZEDRAG);
						}
						this.rightStyle.styleProp.SetValue(this.rightStyle.style, num3);
						this.leftStyle.styleProp.SetValue(this.leftStyle.style, num5);
					}
					else if (((TableLayoutStyle)this.styles[index]).SizeType == SizeType.Percent && ((TableLayoutStyle)this.styles[index2]).SizeType == SizeType.Percent)
					{
						for (int i = 0; i < this.styles.Count; i++)
						{
							if (((TableLayoutStyle)this.styles[i]).SizeType == SizeType.Percent)
							{
								num2 += array[i];
							}
						}
						for (int j = 0; j < 2; j++)
						{
							int num7 = (j == 0) ? index : index2;
							float num8 = (float)array2[num7] * 100f / (float)num2;
							PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.styles[num7])[columnResize ? "Width" : "Height"];
							if (propertyDescriptor != null)
							{
								propertyDescriptor.SetValue(this.styles[num7], num8);
							}
						}
					}
					else
					{
						int num9 = (((TableLayoutStyle)this.styles[index]).SizeType == SizeType.Absolute) ? index : index2;
						PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(this.styles[num9])[columnResize ? "Width" : "Height"];
						if (propertyDescriptor2 != null)
						{
							float num10 = (float)array2[num9];
							float num11 = (float)propertyDescriptor2.GetValue(this.styles[num9]);
							if (num11 != (float)array[num9])
							{
								num10 = Math.Max((num9 == index) ? (num11 - (float)num) : (num11 + (float)num), (float)DesignerUtils.MINUMUMSTYLESIZEDRAG);
							}
							propertyDescriptor2.SetValue(this.styles[num9], num10);
						}
					}
					this.table.ResumeLayout(true);
					bool flag = true;
					int[] array3 = columnResize ? this.table.GetColumnWidths() : this.table.GetRowHeights();
					for (int k = 0; k < array3.Length; k++)
					{
						if (array3[k] == array[k] && array2[k] != array[k])
						{
							flag = false;
						}
					}
					if (flag)
					{
						this.lastMouseLoc = mouseLoc;
					}
				}
				else
				{
					this.lastMouseLoc = mouseLoc;
				}
			}
			return false;
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x000E99B8 File Offset: 0x000E7BB8
		public override bool OnMouseUp(Glyph g, MouseButtons button)
		{
			if (this.pushedBehavior)
			{
				this.FinishResize();
				if (this.resizeTransaction != null)
				{
					DesignerTransaction designerTransaction = this.resizeTransaction;
					this.resizeTransaction = null;
					using (designerTransaction)
					{
						designerTransaction.Commit();
					}
					this.resizeProp = null;
				}
			}
			return false;
		}

		// Token: 0x04001B26 RID: 6950
		private TableLayoutPanelDesigner designer;

		// Token: 0x04001B27 RID: 6951
		private Point lastMouseLoc;

		// Token: 0x04001B28 RID: 6952
		private bool pushedBehavior;

		// Token: 0x04001B29 RID: 6953
		private BehaviorService behaviorService;

		// Token: 0x04001B2A RID: 6954
		private IServiceProvider serviceProvider;

		// Token: 0x04001B2B RID: 6955
		private TableLayoutPanelResizeGlyph tableGlyph;

		// Token: 0x04001B2C RID: 6956
		private DesignerTransaction resizeTransaction;

		// Token: 0x04001B2D RID: 6957
		private PropertyDescriptor resizeProp;

		// Token: 0x04001B2E RID: 6958
		private PropertyDescriptor changedProp;

		// Token: 0x04001B2F RID: 6959
		private TableLayoutPanel table;

		// Token: 0x04001B30 RID: 6960
		private TableLayoutPanelBehavior.StyleHelper rightStyle;

		// Token: 0x04001B31 RID: 6961
		private TableLayoutPanelBehavior.StyleHelper leftStyle;

		// Token: 0x04001B32 RID: 6962
		private ArrayList styles;

		// Token: 0x04001B33 RID: 6963
		private bool currentColumnStyles;

		// Token: 0x04001B34 RID: 6964
		private static readonly TraceSwitch tlpResizeSwitch;

		// Token: 0x020005AA RID: 1450
		internal struct StyleHelper
		{
			// Token: 0x040022AA RID: 8874
			public int index;

			// Token: 0x040022AB RID: 8875
			public PropertyDescriptor styleProp;

			// Token: 0x040022AC RID: 8876
			public TableLayoutStyle style;
		}
	}
}
