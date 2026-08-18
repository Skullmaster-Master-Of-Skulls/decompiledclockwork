using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000332 RID: 818
	internal class SplitterPanelDesigner : PanelDesigner
	{
		// Token: 0x06002053 RID: 8275 RVA: 0x000C40A5 File Offset: 0x000C22A5
		public override bool CanBeParentedTo(IDesigner parentDesigner)
		{
			return parentDesigner is SplitContainerDesigner;
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06002054 RID: 8276 RVA: 0x000C40B0 File Offset: 0x000C22B0
		protected override InheritanceAttribute InheritanceAttribute
		{
			get
			{
				if (this.splitterPanel != null && this.splitterPanel.Parent != null)
				{
					return (InheritanceAttribute)TypeDescriptor.GetAttributes(this.splitterPanel.Parent)[typeof(InheritanceAttribute)];
				}
				return base.InheritanceAttribute;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06002055 RID: 8277 RVA: 0x000C40FD File Offset: 0x000C22FD
		// (set) Token: 0x06002056 RID: 8278 RVA: 0x000C4105 File Offset: 0x000C2305
		internal bool Selected
		{
			get
			{
				return this.selected;
			}
			set
			{
				this.selected = value;
				if (this.selected)
				{
					this.DrawSelectedBorder();
					return;
				}
				this.EraseBorder();
			}
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x000C4123 File Offset: 0x000C2323
		protected override void OnDragEnter(DragEventArgs de)
		{
			if (this.InheritanceAttribute == InheritanceAttribute.InheritedReadOnly)
			{
				de.Effect = DragDropEffects.None;
				return;
			}
			base.OnDragEnter(de);
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x000C4141 File Offset: 0x000C2341
		protected override void OnDragOver(DragEventArgs de)
		{
			if (this.InheritanceAttribute == InheritanceAttribute.InheritedReadOnly)
			{
				de.Effect = DragDropEffects.None;
				return;
			}
			base.OnDragOver(de);
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x000C415F File Offset: 0x000C235F
		protected override void OnDragLeave(EventArgs e)
		{
			if (this.InheritanceAttribute == InheritanceAttribute.InheritedReadOnly)
			{
				return;
			}
			base.OnDragLeave(e);
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x000C4176 File Offset: 0x000C2376
		protected override void OnDragDrop(DragEventArgs de)
		{
			if (this.InheritanceAttribute == InheritanceAttribute.InheritedReadOnly)
			{
				de.Effect = DragDropEffects.None;
				return;
			}
			base.OnDragDrop(de);
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x000C4194 File Offset: 0x000C2394
		protected override void OnMouseHover()
		{
			if (this.splitContainerDesigner != null)
			{
				this.splitContainerDesigner.SplitterPanelHover();
			}
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x000C41AC File Offset: 0x000C23AC
		protected override void Dispose(bool disposing)
		{
			IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.ComponentChanged -= this.OnComponentChanged;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x000C41EC File Offset: 0x000C23EC
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.splitterPanel = (SplitterPanel)component;
			this.designerHost = (IDesignerHost)component.Site.GetService(typeof(IDesignerHost));
			this.splitContainerDesigner = (SplitContainerDesigner)this.designerHost.GetDesigner(this.splitterPanel.Parent);
			IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.ComponentChanged += this.OnComponentChanged;
			}
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)["Locked"];
			if (propertyDescriptor != null && this.splitterPanel.Parent is SplitContainer)
			{
				propertyDescriptor.SetValue(component, true);
			}
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x000C42AC File Offset: 0x000C24AC
		private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			if (this.splitterPanel.Parent == null)
			{
				return;
			}
			if (this.splitterPanel.Controls.Count == 0)
			{
				Graphics graphics = this.splitterPanel.CreateGraphics();
				this.DrawWaterMark(graphics);
				graphics.Dispose();
				return;
			}
			this.splitterPanel.Invalidate();
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x000C4300 File Offset: 0x000C2500
		internal void DrawSelectedBorder()
		{
			Control control = this.Control;
			Rectangle clientRectangle = control.ClientRectangle;
			using (Graphics graphics = control.CreateGraphics())
			{
				Color color;
				if ((double)control.BackColor.GetBrightness() < 0.5)
				{
					color = ControlPaint.Light(control.BackColor);
				}
				else
				{
					color = ControlPaint.Dark(control.BackColor);
				}
				using (Pen pen = new Pen(color))
				{
					pen.DashStyle = DashStyle.Dash;
					clientRectangle.Inflate(-4, -4);
					graphics.DrawRectangle(pen, clientRectangle);
				}
			}
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x000C43B0 File Offset: 0x000C25B0
		internal void EraseBorder()
		{
			Control control = this.Control;
			Rectangle clientRectangle = control.ClientRectangle;
			Graphics graphics = control.CreateGraphics();
			Color backColor = control.BackColor;
			Pen pen = new Pen(backColor);
			pen.DashStyle = DashStyle.Dash;
			clientRectangle.Inflate(-4, -4);
			graphics.DrawRectangle(pen, clientRectangle);
			pen.Dispose();
			graphics.Dispose();
			control.Invalidate();
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x000C4410 File Offset: 0x000C2610
		internal void DrawWaterMark(Graphics g)
		{
			Control control = this.Control;
			Rectangle clientRectangle = control.ClientRectangle;
			string name = control.Name;
			using (Font font = new Font("Arial", 8f))
			{
				int x = clientRectangle.Width / 2 - (int)g.MeasureString(name, font).Width / 2;
				int y = clientRectangle.Height / 2;
				Color foreColor = Color.Black;
				IUIService iuiservice = this.GetService(typeof(IUIService)) as IUIService;
				if (iuiservice != null && iuiservice.Styles["SmartTagText"] is Color)
				{
					foreColor = (Color)iuiservice.Styles["SmartTagText"];
				}
				TextRenderer.DrawText(g, name, font, new Point(x, y), foreColor, TextFormatFlags.Default);
			}
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x000C44F0 File Offset: 0x000C26F0
		protected override void OnPaintAdornments(PaintEventArgs pe)
		{
			base.OnPaintAdornments(pe);
			if (this.splitterPanel.BorderStyle == BorderStyle.None)
			{
				this.DrawBorder(pe.Graphics);
			}
			if (this.Selected)
			{
				this.DrawSelectedBorder();
			}
			if (this.splitterPanel.Controls.Count == 0)
			{
				this.DrawWaterMark(pe.Graphics);
			}
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x000C454C File Offset: 0x000C274C
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			properties.Remove("Modifiers");
			properties.Remove("Locked");
			properties.Remove("GenerateMember");
			foreach (object obj in properties)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)dictionaryEntry.Value;
				if (propertyDescriptor.Name.Equals("Name") && propertyDescriptor.DesignTimeOnly)
				{
					properties[dictionaryEntry.Key] = TypeDescriptor.CreateProperty(propertyDescriptor.ComponentType, propertyDescriptor, new Attribute[]
					{
						BrowsableAttribute.No,
						DesignerSerializationVisibilityAttribute.Hidden
					});
					break;
				}
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06002064 RID: 8292 RVA: 0x000C461C File Offset: 0x000C281C
		public override IList SnapLines
		{
			get
			{
				ArrayList result = null;
				base.AddPaddingSnapLines(ref result);
				return result;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06002065 RID: 8293 RVA: 0x000C4634 File Offset: 0x000C2834
		public override SelectionRules SelectionRules
		{
			get
			{
				SelectionRules result = SelectionRules.None;
				Control control = this.Control;
				if (control.Parent is SplitContainer)
				{
					result = SelectionRules.Locked;
				}
				return result;
			}
		}

		// Token: 0x040018D8 RID: 6360
		private IDesignerHost designerHost;

		// Token: 0x040018D9 RID: 6361
		private SplitContainerDesigner splitContainerDesigner;

		// Token: 0x040018DA RID: 6362
		private SplitterPanel splitterPanel;

		// Token: 0x040018DB RID: 6363
		private bool selected;
	}
}
