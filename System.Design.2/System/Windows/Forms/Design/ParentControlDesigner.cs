using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200031D RID: 797
	public class ParentControlDesigner : ControlDesigner, IOleDragClient
	{
		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001F66 RID: 8038 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected virtual bool AllowControlLasso
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected virtual bool AllowGenericDragBox
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001F68 RID: 8040 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected internal virtual bool AllowSetChildIndexOnDrop
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected internal virtual bool CanAddComponent(IComponent component)
		{
			return true;
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001F6A RID: 8042 RVA: 0x000BD5EE File Offset: 0x000BB7EE
		private Size CurrentGridSize
		{
			get
			{
				return this.GridSize;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001F6B RID: 8043 RVA: 0x000BD5F6 File Offset: 0x000BB7F6
		protected virtual Point DefaultControlLocation
		{
			get
			{
				return new Point(0, 0);
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001F6C RID: 8044 RVA: 0x000BD5FF File Offset: 0x000BB7FF
		private bool DefaultUseSnapLines
		{
			get
			{
				if (this.checkSnapLineSetting)
				{
					this.checkSnapLineSetting = false;
					this.defaultUseSnapLines = DesignerUtils.UseSnapLines(base.Component.Site);
				}
				return this.defaultUseSnapLines;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001F6D RID: 8045 RVA: 0x000BD62C File Offset: 0x000BB82C
		// (set) Token: 0x06001F6E RID: 8046 RVA: 0x000BD698 File Offset: 0x000BB898
		protected virtual bool DrawGrid
		{
			get
			{
				if (this.DefaultUseSnapLines)
				{
					return false;
				}
				if (this.getDefaultDrawGrid)
				{
					this.drawGrid = true;
					ParentControlDesigner parentControlDesignerOfParent = this.GetParentControlDesignerOfParent();
					if (parentControlDesignerOfParent != null)
					{
						this.drawGrid = parentControlDesignerOfParent.DrawGrid;
					}
					else
					{
						object optionValue = DesignerUtils.GetOptionValue(this.ServiceProvider, "ShowGrid");
						if (optionValue is bool)
						{
							this.drawGrid = (bool)optionValue;
						}
					}
				}
				return this.drawGrid;
			}
			set
			{
				if (value != this.drawGrid)
				{
					if (this.parentCanSetDrawGrid)
					{
						this.parentCanSetDrawGrid = false;
					}
					if (this.getDefaultDrawGrid)
					{
						this.getDefaultDrawGrid = false;
					}
					this.drawGrid = value;
					Control control = this.Control;
					if (control != null)
					{
						control.Invalidate(true);
					}
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						foreach (object obj in this.Control.Controls)
						{
							Control component = (Control)obj;
							ParentControlDesigner parentControlDesigner = designerHost.GetDesigner(component) as ParentControlDesigner;
							if (parentControlDesigner != null)
							{
								parentControlDesigner.DrawGridOfParentChanged(this.drawGrid);
							}
						}
					}
				}
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001F6F RID: 8047 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected override bool EnableDragRect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001F70 RID: 8048 RVA: 0x000BD5EE File Offset: 0x000BB7EE
		internal Size ParentGridSize
		{
			get
			{
				return this.GridSize;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001F71 RID: 8049 RVA: 0x000BD76C File Offset: 0x000BB96C
		// (set) Token: 0x06001F72 RID: 8050 RVA: 0x000BD7D4 File Offset: 0x000BB9D4
		protected Size GridSize
		{
			get
			{
				if (this.getDefaultGridSize)
				{
					this.gridSize = new Size(8, 8);
					ParentControlDesigner parentControlDesignerOfParent = this.GetParentControlDesignerOfParent();
					if (parentControlDesignerOfParent != null)
					{
						this.gridSize = parentControlDesignerOfParent.GridSize;
					}
					else
					{
						object optionValue = DesignerUtils.GetOptionValue(this.ServiceProvider, "GridSize");
						if (optionValue is Size)
						{
							this.gridSize = (Size)optionValue;
						}
					}
				}
				return this.gridSize;
			}
			set
			{
				if (this.parentCanSetGridSize)
				{
					this.parentCanSetGridSize = false;
				}
				if (this.getDefaultGridSize)
				{
					this.getDefaultGridSize = false;
				}
				if (value.Width < 2 || value.Height < 2 || value.Width > 200 || value.Height > 200)
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
					{
						"GridSize",
						value.ToString()
					}));
				}
				this.gridSize = value;
				Control control = this.Control;
				if (control != null)
				{
					control.Invalidate(true);
				}
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					foreach (object obj in this.Control.Controls)
					{
						Control component = (Control)obj;
						ParentControlDesigner parentControlDesigner = designerHost.GetDesigner(component) as ParentControlDesigner;
						if (parentControlDesigner != null)
						{
							parentControlDesigner.GridSizeOfParentChanged(this.gridSize);
						}
					}
				}
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001F73 RID: 8051 RVA: 0x000BD8FC File Offset: 0x000BBAFC
		protected ToolboxItem MouseDragTool
		{
			get
			{
				return this.mouseDragTool;
			}
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x000BD904 File Offset: 0x000BBB04
		protected virtual Control GetParentForComponent(IComponent component)
		{
			return this.Control;
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x000BD90C File Offset: 0x000BBB0C
		protected void AddPaddingSnapLines(ref ArrayList snapLines)
		{
			if (snapLines == null)
			{
				snapLines = new ArrayList(4);
			}
			Point offsetToClientArea = base.GetOffsetToClientArea();
			Rectangle displayRectangle = this.Control.DisplayRectangle;
			displayRectangle.X += offsetToClientArea.X;
			displayRectangle.Y += offsetToClientArea.Y;
			snapLines.Add(new SnapLine(SnapLineType.Vertical, displayRectangle.Left, "Padding.Left", SnapLinePriority.Always));
			snapLines.Add(new SnapLine(SnapLineType.Vertical, displayRectangle.Right, "Padding.Right", SnapLinePriority.Always));
			snapLines.Add(new SnapLine(SnapLineType.Horizontal, displayRectangle.Top, "Padding.Top", SnapLinePriority.Always));
			snapLines.Add(new SnapLine(SnapLineType.Horizontal, displayRectangle.Bottom, "Padding.Bottom", SnapLinePriority.Always));
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001F76 RID: 8054 RVA: 0x000BD9D0 File Offset: 0x000BBBD0
		public override IList SnapLines
		{
			get
			{
				ArrayList arrayList = base.SnapLines as ArrayList;
				if (arrayList == null)
				{
					arrayList = new ArrayList(4);
				}
				this.AddPaddingSnapLines(ref arrayList);
				return arrayList;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001F77 RID: 8055 RVA: 0x000BD9FC File Offset: 0x000BBBFC
		private IServiceProvider ServiceProvider
		{
			get
			{
				if (base.Component != null)
				{
					return base.Component.Site;
				}
				return null;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001F78 RID: 8056 RVA: 0x000BDA14 File Offset: 0x000BBC14
		// (set) Token: 0x06001F79 RID: 8057 RVA: 0x000BDA80 File Offset: 0x000BBC80
		private bool SnapToGrid
		{
			get
			{
				if (this.DefaultUseSnapLines)
				{
					return false;
				}
				if (this.getDefaultGridSnap)
				{
					this.gridSnap = true;
					ParentControlDesigner parentControlDesignerOfParent = this.GetParentControlDesignerOfParent();
					if (parentControlDesignerOfParent != null)
					{
						this.gridSnap = parentControlDesignerOfParent.SnapToGrid;
					}
					else
					{
						object optionValue = DesignerUtils.GetOptionValue(this.ServiceProvider, "SnapToGrid");
						if (optionValue != null && optionValue is bool)
						{
							this.gridSnap = (bool)optionValue;
						}
					}
				}
				return this.gridSnap;
			}
			set
			{
				if (this.gridSnap != value)
				{
					if (this.parentCanSetGridSnap)
					{
						this.parentCanSetGridSnap = false;
					}
					if (this.getDefaultGridSnap)
					{
						this.getDefaultGridSnap = false;
					}
					this.gridSnap = value;
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						foreach (object obj in this.Control.Controls)
						{
							Control component = (Control)obj;
							ParentControlDesigner parentControlDesigner = designerHost.GetDesigner(component) as ParentControlDesigner;
							if (parentControlDesigner != null)
							{
								parentControlDesigner.GridSnapOfParentChanged(this.gridSnap);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x000BDB40 File Offset: 0x000BBD40
		internal virtual void AddChildControl(Control newChild)
		{
			if (newChild.Left == 0 && newChild.Top == 0 && newChild.Width >= this.Control.Width && newChild.Height >= this.Control.Height)
			{
				Point location = newChild.Location;
				location.Offset(this.GridSize.Width, this.GridSize.Height);
				newChild.Location = location;
			}
			this.Control.Controls.Add(newChild);
			this.Control.Controls.SetChildIndex(newChild, 0);
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x000BDBD8 File Offset: 0x000BBDD8
		internal void AddControl(Control newChild, IDictionary defaultValues)
		{
			Point p = Point.Empty;
			Size size = Size.Empty;
			Size size2 = new Size(0, 0);
			bool flag = defaultValues != null && defaultValues.Contains("Location");
			bool flag2 = defaultValues != null && defaultValues.Contains("Size");
			if (flag)
			{
				p = (Point)defaultValues["Location"];
			}
			if (flag2)
			{
				size = (Size)defaultValues["Size"];
			}
			if (defaultValues != null && defaultValues.Contains("Offset"))
			{
				size2 = (Size)defaultValues["Offset"];
			}
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null && newChild != null && !this.Control.Contains(newChild) && designerHost.GetDesigner(newChild) is ControlDesigner && (!(newChild is Form) || !((Form)newChild).TopLevel))
			{
				Rectangle rectangle = default(Rectangle);
				if (flag)
				{
					p = this.Control.PointToClient(p);
					rectangle.X = p.X;
					rectangle.Y = p.Y;
				}
				else
				{
					ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
					object primarySelection = selectionService.PrimarySelection;
					Control control = null;
					if (primarySelection != null)
					{
						control = ((IOleDragClient)this).GetControlForComponent(primarySelection);
					}
					if (control != null && control.Site == null)
					{
						control = null;
					}
					if (primarySelection == base.Component || control == null)
					{
						rectangle.X = this.DefaultControlLocation.X;
						rectangle.Y = this.DefaultControlLocation.Y;
					}
					else
					{
						rectangle.X = control.Location.X + this.GridSize.Width;
						rectangle.Y = control.Location.Y + this.GridSize.Height;
					}
				}
				if (flag2)
				{
					rectangle.Width = size.Width;
					rectangle.Height = size.Height;
				}
				else
				{
					rectangle.Size = this.GetDefaultSize(newChild);
				}
				if (!flag2 && !flag)
				{
					Rectangle rectangle2 = this.GetAdjustedSnapLocation(Rectangle.Empty, rectangle);
					rectangle2 = this.GetControlStackLocation(rectangle2);
					rectangle = rectangle2;
				}
				else
				{
					rectangle = this.GetAdjustedSnapLocation(Rectangle.Empty, rectangle);
				}
				rectangle.X += size2.Width;
				rectangle.Y += size2.Height;
				if (defaultValues != null && defaultValues.Contains("ToolboxSnapDragDropEventArgs"))
				{
					ToolboxSnapDragDropEventArgs e = defaultValues["ToolboxSnapDragDropEventArgs"] as ToolboxSnapDragDropEventArgs;
					Rectangle boundsFromToolboxSnapDragDropInfo = DesignerUtils.GetBoundsFromToolboxSnapDragDropInfo(e, rectangle, this.Control.IsMirrored);
					Control control2 = designerHost.RootComponent as Control;
					if (control2 != null && boundsFromToolboxSnapDragDropInfo.IntersectsWith(control2.ClientRectangle))
					{
						rectangle = boundsFromToolboxSnapDragDropInfo;
					}
				}
				PropertyDescriptor member = TypeDescriptor.GetProperties(this.Control)["Controls"];
				if (this.componentChangeSvc != null)
				{
					this.componentChangeSvc.OnComponentChanging(this.Control, member);
				}
				this.AddChildControl(newChild);
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(newChild);
				if (properties != null)
				{
					PropertyDescriptor propertyDescriptor = properties["Size"];
					if (propertyDescriptor != null)
					{
						propertyDescriptor.SetValue(newChild, new Size(rectangle.Width, rectangle.Height));
					}
					Point point = new Point(rectangle.X, rectangle.Y);
					ScrollableControl scrollableControl = newChild.Parent as ScrollableControl;
					if (scrollableControl != null)
					{
						Point autoScrollPosition = scrollableControl.AutoScrollPosition;
						point.Offset(-autoScrollPosition.X, -autoScrollPosition.Y);
					}
					propertyDescriptor = properties["Location"];
					if (propertyDescriptor != null)
					{
						propertyDescriptor.SetValue(newChild, point);
					}
				}
				if (this.componentChangeSvc != null)
				{
					this.componentChangeSvc.OnComponentChanged(this.Control, member, this.Control.Controls, this.Control.Controls);
				}
				newChild.Update();
			}
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x000BDFD4 File Offset: 0x000BC1D4
		private void AddChildComponents(IComponent component, IContainer container, IDesignerHost host)
		{
			Control control = this.GetControl(component);
			if (control != null)
			{
				Control control2 = control;
				Control[] array = new Control[control2.Controls.Count];
				control2.Controls.CopyTo(array, 0);
				for (int i = 0; i < array.Length; i++)
				{
					ISite site = ((IComponent)array[i]).Site;
					if (site != null)
					{
						string text = site.Name;
						if (container.Components[text] != null)
						{
							text = null;
						}
						IContainer container2 = site.Container;
						if (container2 != null)
						{
							container2.Remove(array[i]);
						}
						if (text != null)
						{
							container.Add(array[i], text);
						}
						else
						{
							container.Add(array[i]);
						}
						if (array[i].Parent != control2)
						{
							control2.Controls.Add(array[i]);
						}
						else
						{
							int childIndex = control2.Controls.GetChildIndex(array[i]);
							control2.Controls.Remove(array[i]);
							control2.Controls.Add(array[i]);
							control2.Controls.SetChildIndex(array[i], childIndex);
						}
						IComponentInitializer componentInitializer = host.GetDesigner(component) as IComponentInitializer;
						if (componentInitializer != null)
						{
							componentInitializer.InitializeExistingComponent(null);
						}
						this.AddChildComponents(array[i], container, host);
					}
				}
			}
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x000BE10C File Offset: 0x000BC30C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.OnMouseDragEnd(this.mouseDragBase == ControlDesigner.InvalidPoint);
				base.EnableDragDrop(false);
				if (this.Control is ScrollableControl)
				{
					((ScrollableControl)this.Control).Scroll -= this.OnScroll;
				}
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					this.componentChangeSvc.ComponentRemoving -= this.OnComponentRemoving;
					this.componentChangeSvc.ComponentRemoved -= this.OnComponentRemoved;
					this.componentChangeSvc = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x000BE1C0 File Offset: 0x000BC3C0
		private void DrawGridOfParentChanged(bool drawGrid)
		{
			if (this.parentCanSetDrawGrid)
			{
				bool flag = this.getDefaultDrawGrid;
				this.DrawGrid = drawGrid;
				this.parentCanSetDrawGrid = true;
				this.getDefaultDrawGrid = flag;
			}
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x000BE1F4 File Offset: 0x000BC3F4
		private void GridSizeOfParentChanged(Size gridSize)
		{
			if (this.parentCanSetGridSize)
			{
				bool flag = this.getDefaultGridSize;
				this.GridSize = gridSize;
				this.parentCanSetGridSize = true;
				this.getDefaultGridSize = flag;
			}
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x000BE228 File Offset: 0x000BC428
		private void GridSnapOfParentChanged(bool gridSnap)
		{
			if (this.parentCanSetGridSnap)
			{
				bool flag = this.getDefaultGridSnap;
				this.SnapToGrid = gridSnap;
				this.parentCanSetGridSnap = true;
				this.getDefaultGridSnap = flag;
			}
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x000BE259 File Offset: 0x000BC459
		protected static void InvokeCreateTool(ParentControlDesigner toInvoke, ToolboxItem tool)
		{
			toInvoke.CreateTool(tool);
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x000BE262 File Offset: 0x000BC462
		public virtual bool CanParent(ControlDesigner controlDesigner)
		{
			return this.CanParent(controlDesigner.Control);
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x000BE270 File Offset: 0x000BC470
		public virtual bool CanParent(Control control)
		{
			return !control.Contains(this.Control);
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x000BE281 File Offset: 0x000BC481
		protected void CreateTool(ToolboxItem tool)
		{
			this.CreateToolCore(tool, 0, 0, 0, 0, false, false);
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x000BE291 File Offset: 0x000BC491
		protected void CreateTool(ToolboxItem tool, Point location)
		{
			this.CreateToolCore(tool, location.X, location.Y, 0, 0, true, false);
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x000BE2AD File Offset: 0x000BC4AD
		protected void CreateTool(ToolboxItem tool, Rectangle bounds)
		{
			this.CreateToolCore(tool, bounds.X, bounds.Y, bounds.Width, bounds.Height, true, true);
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x000BE2D8 File Offset: 0x000BC4D8
		protected virtual IComponent[] CreateToolCore(ToolboxItem tool, int x, int y, int width, int height, bool hasLocation, bool hasSize)
		{
			IComponent[] result = null;
			try
			{
				result = this.GetOleDragHandler().CreateTool(tool, this.Control, x, y, width, height, hasLocation, hasSize, this.toolboxSnapDragDropEventArgs);
			}
			finally
			{
				this.toolboxSnapDragDropEventArgs = null;
			}
			return result;
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x000BE328 File Offset: 0x000BC528
		private SnapLine[] GenerateNewToolSnapLines(Rectangle r)
		{
			return new SnapLine[]
			{
				new SnapLine(SnapLineType.Left, r.Right),
				new SnapLine(SnapLineType.Right, r.Right),
				new SnapLine(SnapLineType.Bottom, r.Bottom),
				new SnapLine(SnapLineType.Top, r.Bottom)
			};
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x000BE37C File Offset: 0x000BC57C
		internal object[] GetComponentsInRect(Rectangle value, bool screenCoords, bool containRect)
		{
			ArrayList arrayList = new ArrayList();
			Rectangle rect = screenCoords ? this.Control.RectangleToClient(value) : value;
			IContainer container = base.Component.Site.Container;
			Control control = this.Control;
			int count = control.Controls.Count;
			for (int i = 0; i < count; i++)
			{
				Control control2 = control.Controls[i];
				Rectangle bounds = control2.Bounds;
				container = DesignerUtils.CheckForNestedContainer(container);
				if (control2.Visible && ((containRect && rect.Contains(bounds)) || (!containRect && bounds.IntersectsWith(rect))) && control2.Site != null && control2.Site.Container == container)
				{
					arrayList.Add(control2);
				}
			}
			return arrayList.ToArray();
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x000BE444 File Offset: 0x000BC644
		protected Control GetControl(object component)
		{
			IComponent component2 = component as IComponent;
			if (component2 != null)
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					ControlDesigner controlDesigner = designerHost.GetDesigner(component2) as ControlDesigner;
					if (controlDesigner != null)
					{
						return controlDesigner.Control;
					}
				}
			}
			return null;
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x000BE48C File Offset: 0x000BC68C
		private Rectangle GetControlStackLocation(Rectangle centeredLocation)
		{
			Control control = this.Control;
			int height = control.ClientSize.Height;
			int width = control.ClientSize.Width;
			if (centeredLocation.Bottom >= height || centeredLocation.Right >= width)
			{
				centeredLocation.X = this.DefaultControlLocation.X;
				centeredLocation.Y = this.DefaultControlLocation.Y;
			}
			return centeredLocation;
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x000BE500 File Offset: 0x000BC700
		private Size GetDefaultSize(IComponent component)
		{
			Size size = Size.Empty;
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)["AutoSize"];
			if (propertyDescriptor != null && !propertyDescriptor.Attributes.Contains(DesignerSerializationVisibilityAttribute.Hidden) && !propertyDescriptor.Attributes.Contains(BrowsableAttribute.No))
			{
				bool flag = (bool)propertyDescriptor.GetValue(component);
				if (flag)
				{
					propertyDescriptor = TypeDescriptor.GetProperties(component)["PreferredSize"];
					if (propertyDescriptor != null)
					{
						size = (Size)propertyDescriptor.GetValue(component);
						if (size != Size.Empty)
						{
							return size;
						}
					}
				}
			}
			propertyDescriptor = TypeDescriptor.GetProperties(component)["Size"];
			if (propertyDescriptor != null)
			{
				size = (Size)propertyDescriptor.GetValue(component);
				if (size.Width > 0 && size.Height > 0)
				{
					return size;
				}
				DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)propertyDescriptor.Attributes[typeof(DefaultValueAttribute)];
				if (defaultValueAttribute != null)
				{
					return (Size)defaultValueAttribute.Value;
				}
			}
			return new Size(75, 23);
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x000BE5F8 File Offset: 0x000BC7F8
		protected override ControlBodyGlyph GetControlGlyph(GlyphSelectionType selectionType)
		{
			this.OnSetCursor();
			Rectangle rectangle = base.BehaviorService.ControlRectInAdornerWindow(this.Control);
			Control parent = this.Control.Parent;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (parent != null && designerHost != null && designerHost.RootComponent != base.Component)
			{
				Rectangle a = base.BehaviorService.ControlRectInAdornerWindow(parent);
				Rectangle bounds = Rectangle.Intersect(a, rectangle);
				if (selectionType == GlyphSelectionType.NotSelected)
				{
					if (!bounds.IsEmpty && !a.Contains(rectangle))
					{
						return new ControlBodyGlyph(bounds, Cursor.Current, this.Control, this);
					}
					if (bounds.IsEmpty)
					{
						return null;
					}
				}
			}
			return new ControlBodyGlyph(rectangle, Cursor.Current, this.Control, this);
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x000BE6B0 File Offset: 0x000BC8B0
		public override GlyphCollection GetGlyphs(GlyphSelectionType selectionType)
		{
			GlyphCollection glyphs = base.GetGlyphs(selectionType);
			if ((this.SelectionRules & SelectionRules.Moveable) != SelectionRules.None && this.InheritanceAttribute != InheritanceAttribute.InheritedReadOnly && selectionType != GlyphSelectionType.NotSelected)
			{
				Point location = base.BehaviorService.ControlToAdornerWindow((Control)base.Component);
				Rectangle containerBounds = new Rectangle(location, ((Control)base.Component).Size);
				int num = (int)((double)DesignerUtils.CONTAINERGRABHANDLESIZE * 0.5);
				if (containerBounds.Width < 2 * DesignerUtils.CONTAINERGRABHANDLESIZE)
				{
					num = -1 * num;
				}
				ContainerSelectorBehavior behavior = new ContainerSelectorBehavior((Control)base.Component, base.Component.Site, true);
				ContainerSelectorGlyph value = new ContainerSelectorGlyph(containerBounds, DesignerUtils.CONTAINERGRABHANDLESIZE, num, behavior);
				glyphs.Insert(0, value);
			}
			return glyphs;
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x000BE779 File Offset: 0x000BC979
		internal OleDragDropHandler GetOleDragHandler()
		{
			if (this.oleDragDropHandler == null)
			{
				this.oleDragDropHandler = new OleDragDropHandler(null, (IServiceProvider)this.GetService(typeof(IDesignerHost)), this);
			}
			return this.oleDragDropHandler;
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x000BE7AC File Offset: 0x000BC9AC
		private ParentControlDesigner GetParentControlDesignerOfParent()
		{
			Control parent = this.Control.Parent;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (parent != null && designerHost != null)
			{
				return designerHost.GetDesigner(parent) as ParentControlDesigner;
			}
			return null;
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x000BE7F0 File Offset: 0x000BC9F0
		private Rectangle GetAdjustedSnapLocation(Rectangle originalRect, Rectangle dragRect)
		{
			Rectangle updatedRect = this.GetUpdatedRect(originalRect, dragRect, true);
			updatedRect.Width = dragRect.Width;
			updatedRect.Height = dragRect.Height;
			Point defaultControlLocation = this.DefaultControlLocation;
			if (updatedRect.X < defaultControlLocation.X)
			{
				updatedRect.X = defaultControlLocation.X;
			}
			if (updatedRect.Y < defaultControlLocation.Y)
			{
				updatedRect.Y = defaultControlLocation.Y;
			}
			return updatedRect;
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x000BE868 File Offset: 0x000BCA68
		internal Point GetSnappedPoint(Point pt)
		{
			Rectangle updatedRect = this.GetUpdatedRect(Rectangle.Empty, new Rectangle(pt.X, pt.Y, 0, 0), false);
			return new Point(updatedRect.X, updatedRect.Y);
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x000BE8AA File Offset: 0x000BCAAA
		internal Rectangle GetSnappedRect(Rectangle originalRect, Rectangle dragRect, bool updateSize)
		{
			return this.GetUpdatedRect(originalRect, dragRect, updateSize);
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x000BE8B8 File Offset: 0x000BCAB8
		protected Rectangle GetUpdatedRect(Rectangle originalRect, Rectangle dragRect, bool updateSize)
		{
			Rectangle result = Rectangle.Empty;
			if (this.SnapToGrid)
			{
				Size size = this.GridSize;
				Point point = new Point(size.Width / 2, size.Height / 2);
				result = dragRect;
				result.X = originalRect.X;
				result.Y = originalRect.Y;
				if (dragRect.X != originalRect.X)
				{
					result.X = dragRect.X / size.Width * size.Width;
					if (dragRect.X - result.X > point.X)
					{
						result.X += size.Width;
					}
				}
				if (dragRect.Y != originalRect.Y)
				{
					result.Y = dragRect.Y / size.Height * size.Height;
					if (dragRect.Y - result.Y > point.Y)
					{
						result.Y += size.Height;
					}
				}
				if (updateSize)
				{
					result.Width = (dragRect.X + dragRect.Width) / size.Width * size.Width - result.X;
					result.Height = (dragRect.Y + dragRect.Height) / size.Height * size.Height - result.Y;
					if (result.Width < size.Width)
					{
						result.Width = size.Width;
					}
					if (result.Height < size.Height)
					{
						result.Height = size.Height;
					}
				}
			}
			else
			{
				result = dragRect;
			}
			return result;
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x000BEA70 File Offset: 0x000BCC70
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			if (this.Control is ScrollableControl)
			{
				((ScrollableControl)this.Control).Scroll += this.OnScroll;
			}
			base.EnableDragDrop(true);
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				this.componentChangeSvc = (IComponentChangeService)designerHost.GetService(typeof(IComponentChangeService));
				if (this.componentChangeSvc != null)
				{
					this.componentChangeSvc.ComponentRemoving += this.OnComponentRemoving;
					this.componentChangeSvc.ComponentRemoved += this.OnComponentRemoved;
				}
			}
			this.statusCommandUI = new StatusCommandUI(component.Site);
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x000BEB30 File Offset: 0x000BCD30
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			base.InitializeNewComponent(defaultValues);
			if (!this.AllowControlLasso)
			{
				return;
			}
			if (defaultValues != null && defaultValues["Size"] != null && defaultValues["Location"] != null && defaultValues["Parent"] != null)
			{
				Rectangle value = new Rectangle((Point)defaultValues["Location"], (Size)defaultValues["Size"]);
				IComponent component = defaultValues["Parent"] as IComponent;
				if (component == null)
				{
					return;
				}
				IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost == null)
				{
					return;
				}
				ParentControlDesigner parentControlDesigner = designerHost.GetDesigner(component) as ParentControlDesigner;
				if (parentControlDesigner == null)
				{
					return;
				}
				object[] componentsInRect = parentControlDesigner.GetComponentsInRect(value, true, true);
				if (componentsInRect == null || componentsInRect.Length == 0)
				{
					return;
				}
				ArrayList arrayList = new ArrayList(componentsInRect);
				if (arrayList.Contains(this.Control))
				{
					arrayList.Remove(this.Control);
				}
				this.ReParentControls(this.Control, arrayList, SR.GetString("ParentControlDesignerLassoShortcutRedo", new object[]
				{
					this.Control.Site.Name
				}), designerHost);
			}
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x000BEC58 File Offset: 0x000BCE58
		private bool IsOptionDefault(string optionName, object value)
		{
			IDesignerOptionService designerOptionService = (IDesignerOptionService)this.GetService(typeof(IDesignerOptionService));
			object obj = null;
			if (designerOptionService == null)
			{
				if (optionName.Equals("ShowGrid"))
				{
					obj = true;
				}
				else if (optionName.Equals("SnapToGrid"))
				{
					obj = true;
				}
				else if (optionName.Equals("GridSize"))
				{
					obj = new Size(8, 8);
				}
			}
			else
			{
				obj = DesignerUtils.GetOptionValue(this.ServiceProvider, optionName);
			}
			if (obj != null)
			{
				return obj.Equals(value);
			}
			return value == null;
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x000BECE4 File Offset: 0x000BCEE4
		private void OnComponentRemoving(object sender, ComponentEventArgs e)
		{
			Control control = e.Component as Control;
			if (control != null && control.Parent != null && control.Parent == this.Control)
			{
				this.pendingRemoveControl = control;
				if (this.suspendChanging == 0)
				{
					this.componentChangeSvc.OnComponentChanging(this.Control, TypeDescriptor.GetProperties(this.Control)["Controls"]);
				}
			}
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x000BED4B File Offset: 0x000BCF4B
		private void OnComponentRemoved(object sender, ComponentEventArgs e)
		{
			if (e.Component == this.pendingRemoveControl)
			{
				this.pendingRemoveControl = null;
				this.componentChangeSvc.OnComponentChanged(this.Control, TypeDescriptor.GetProperties(this.Control)["Controls"], null, null);
			}
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x000BED8A File Offset: 0x000BCF8A
		internal void SuspendChangingEvents()
		{
			this.suspendChanging++;
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x000BED9A File Offset: 0x000BCF9A
		internal void ResumeChangingEvents()
		{
			this.suspendChanging--;
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x000BEDAA File Offset: 0x000BCFAA
		internal void ForceComponentChanging()
		{
			this.componentChangeSvc.OnComponentChanging(this.Control, TypeDescriptor.GetProperties(this.Control)["Controls"]);
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x000BEDD4 File Offset: 0x000BCFD4
		protected override void OnDragComplete(DragEventArgs de)
		{
			DropSourceBehavior.BehaviorDataObject behaviorDataObject = de.Data as DropSourceBehavior.BehaviorDataObject;
			if (behaviorDataObject != null)
			{
				behaviorDataObject.CleanupDrag();
			}
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x000BEDF8 File Offset: 0x000BCFF8
		protected override void OnDragDrop(DragEventArgs de)
		{
			if (de is ToolboxSnapDragDropEventArgs)
			{
				this.toolboxSnapDragDropEventArgs = (de as ToolboxSnapDragDropEventArgs);
			}
			DropSourceBehavior.BehaviorDataObject behaviorDataObject = de.Data as DropSourceBehavior.BehaviorDataObject;
			if (behaviorDataObject != null)
			{
				behaviorDataObject.Target = base.Component;
				behaviorDataObject.EndDragDrop(this.AllowSetChildIndexOnDrop);
				this.OnDragComplete(de);
			}
			else if (this.mouseDragTool == null && behaviorDataObject == null)
			{
				OleDragDropHandler oleDragHandler = this.GetOleDragHandler();
				if (oleDragHandler != null)
				{
					IOleDragClient destination = oleDragHandler.Destination;
					if (destination != null && destination.Component != null && destination.Component.Site != null)
					{
						IContainer container = destination.Component.Site.Container;
						if (container != null)
						{
							object[] draggingObjects = oleDragHandler.GetDraggingObjects(de);
							for (int i = 0; i < draggingObjects.Length; i++)
							{
								IComponent component = draggingObjects[i] as IComponent;
								container.Add(component);
							}
						}
					}
				}
			}
			if (this.mouseDragTool != null)
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					designerHost.Activate();
				}
				try
				{
					if (base.BehaviorService != null)
					{
						base.BehaviorService.EndDragNotification();
					}
					this.CreateTool(this.mouseDragTool, new Point(de.X, de.Y));
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
					}
					base.DisplayError(ex);
				}
				this.mouseDragTool = null;
				return;
			}
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x000BEF50 File Offset: 0x000BD150
		protected override void OnDragEnter(DragEventArgs de)
		{
			bool flag = false;
			DropSourceBehavior.BehaviorDataObject behaviorDataObject = null;
			DropSourceBehavior.BehaviorDataObject behaviorDataObject2 = de.Data as DropSourceBehavior.BehaviorDataObject;
			if (behaviorDataObject2 != null)
			{
				behaviorDataObject = behaviorDataObject2;
				behaviorDataObject.Target = base.Component;
				de.Effect = ((Control.ModifierKeys == Keys.Control) ? DragDropEffects.Copy : DragDropEffects.Move);
				flag = !behaviorDataObject2.Source.Equals(base.Component);
			}
			IMenuCommandService menuCommandService = (IMenuCommandService)this.GetService(typeof(IMenuCommandService));
			if (menuCommandService != null)
			{
				MenuCommand menuCommand = menuCommandService.FindCommand(StandardCommands.TabOrder);
				if (menuCommand != null && menuCommand.Checked)
				{
					de.Effect = DragDropEffects.None;
					return;
				}
			}
			object[] array;
			if (behaviorDataObject != null && behaviorDataObject.DragComponents != null)
			{
				array = new object[behaviorDataObject.DragComponents.Count];
				behaviorDataObject.DragComponents.CopyTo(array, 0);
			}
			else
			{
				OleDragDropHandler oleDragHandler = this.GetOleDragHandler();
				array = oleDragHandler.GetDraggingObjects(de);
			}
			Control control = null;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				DocumentDesigner documentDesigner = designerHost.GetDesigner(designerHost.RootComponent) as DocumentDesigner;
				if (documentDesigner != null && !documentDesigner.CanDropComponents(de))
				{
					de.Effect = DragDropEffects.None;
					return;
				}
			}
			if (array != null)
			{
				if (behaviorDataObject2 == null)
				{
					flag = true;
				}
				for (int i = 0; i < array.Length; i++)
				{
					IComponent component = array[i] as IComponent;
					if (designerHost != null && component != null)
					{
						if (flag)
						{
							InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(component)[typeof(InheritanceAttribute)];
							if (inheritanceAttribute != null && !inheritanceAttribute.Equals(InheritanceAttribute.NotInherited) && !inheritanceAttribute.Equals(InheritanceAttribute.InheritedReadOnly))
							{
								de.Effect = DragDropEffects.None;
								return;
							}
						}
						object designer = designerHost.GetDesigner(component);
						if (designer is IOleDragClient)
						{
							control = ((IOleDragClient)this).GetControlForComponent(array[i]);
						}
						Control control2 = array[i] as Control;
						if (control == null && control2 != null)
						{
							control = control2;
						}
						if (control != null)
						{
							if (this.InheritanceAttribute == InheritanceAttribute.InheritedReadOnly && control.Parent != this.Control)
							{
								de.Effect = DragDropEffects.None;
								return;
							}
							if (!((IOleDragClient)this).IsDropOk(component))
							{
								de.Effect = DragDropEffects.None;
								return;
							}
						}
					}
				}
				if (behaviorDataObject2 == null)
				{
					this.PerformDragEnter(de, designerHost);
				}
			}
			if (this.toolboxService == null)
			{
				this.toolboxService = (IToolboxService)this.GetService(typeof(IToolboxService));
			}
			if (this.toolboxService != null && array == null)
			{
				this.mouseDragTool = this.toolboxService.DeserializeToolboxItem(de.Data, designerHost);
				if (this.mouseDragTool != null && base.BehaviorService != null && base.BehaviorService.UseSnapLines)
				{
					if (this.toolboxItemSnapLineBehavior == null)
					{
						this.toolboxItemSnapLineBehavior = new ToolboxItemSnapLineBehavior(base.Component.Site, base.BehaviorService, this, this.AllowGenericDragBox);
					}
					if (!this.toolboxItemSnapLineBehavior.IsPushed)
					{
						base.BehaviorService.PushBehavior(this.toolboxItemSnapLineBehavior);
						this.toolboxItemSnapLineBehavior.IsPushed = true;
					}
				}
				if (this.mouseDragTool != null)
				{
					this.PerformDragEnter(de, designerHost);
				}
				if (this.toolboxItemSnapLineBehavior != null)
				{
					this.toolboxItemSnapLineBehavior.OnBeginDrag();
				}
			}
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x000BF25C File Offset: 0x000BD45C
		private void PerformDragEnter(DragEventArgs de, IDesignerHost host)
		{
			if (host != null)
			{
				host.Activate();
			}
			if ((de.AllowedEffect & DragDropEffects.Move) != DragDropEffects.None)
			{
				de.Effect = DragDropEffects.Move;
			}
			else
			{
				de.Effect = DragDropEffects.Copy;
			}
			if (this.InheritanceAttribute == InheritanceAttribute.InheritedReadOnly)
			{
				de.Effect = DragDropEffects.None;
				return;
			}
			ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
			if (selectionService != null)
			{
				selectionService.SetSelectedComponents(new object[]
				{
					base.Component
				}, SelectionTypes.Replace);
			}
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x000BF2D0 File Offset: 0x000BD4D0
		protected override void OnDragLeave(EventArgs e)
		{
			if (this.toolboxItemSnapLineBehavior != null && this.toolboxItemSnapLineBehavior.IsPushed)
			{
				base.BehaviorService.PopBehavior(this.toolboxItemSnapLineBehavior);
				this.toolboxItemSnapLineBehavior.IsPushed = false;
			}
			this.mouseDragTool = null;
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x000BF30C File Offset: 0x000BD50C
		protected override void OnDragOver(DragEventArgs de)
		{
			DropSourceBehavior.BehaviorDataObject behaviorDataObject = de.Data as DropSourceBehavior.BehaviorDataObject;
			if (behaviorDataObject != null)
			{
				behaviorDataObject.Target = base.Component;
				de.Effect = ((Control.ModifierKeys == Keys.Control) ? DragDropEffects.Copy : DragDropEffects.Move);
			}
			IMenuCommandService menuCommandService = (IMenuCommandService)this.GetService(typeof(IMenuCommandService));
			if (menuCommandService != null)
			{
				MenuCommand menuCommand = menuCommandService.FindCommand(StandardCommands.TabOrder);
				if (menuCommand != null && menuCommand.Checked)
				{
					de.Effect = DragDropEffects.None;
					return;
				}
			}
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				DocumentDesigner documentDesigner = designerHost.GetDesigner(designerHost.RootComponent) as DocumentDesigner;
				if (documentDesigner != null && !documentDesigner.CanDropComponents(de))
				{
					de.Effect = DragDropEffects.None;
					return;
				}
			}
			if (this.mouseDragTool != null)
			{
				de.Effect = DragDropEffects.Copy;
				return;
			}
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x000BF3D4 File Offset: 0x000BD5D4
		private static int FrameWidth(FrameStyle style)
		{
			if (style != FrameStyle.Dashed)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x000BF3DC File Offset: 0x000BD5DC
		protected override void OnMouseDragBegin(int x, int y)
		{
			Control control = this.Control;
			if (!this.InheritanceAttribute.Equals(InheritanceAttribute.InheritedReadOnly))
			{
				if (this.toolboxService == null)
				{
					this.toolboxService = (IToolboxService)this.GetService(typeof(IToolboxService));
				}
				if (this.toolboxService != null)
				{
					this.mouseDragTool = this.toolboxService.GetSelectedToolboxItem((IDesignerHost)this.GetService(typeof(IDesignerHost)));
				}
			}
			control.Capture = true;
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			NativeMethods.GetWindowRect(control.Handle, ref rect);
			Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
			this.mouseDragFrame = ((this.mouseDragTool == null) ? FrameStyle.Dashed : FrameStyle.Thick);
			this.mouseDragBase = new Point(x, y);
			ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
			if (selectionService != null)
			{
				selectionService.SetSelectedComponents(new object[]
				{
					base.Component
				}, SelectionTypes.Click);
			}
			IEventHandlerService eventHandlerService = (IEventHandlerService)this.GetService(typeof(IEventHandlerService));
			if (eventHandlerService != null && this.escapeHandler == null)
			{
				this.escapeHandler = new ParentControlDesigner.EscapeHandler(this);
				eventHandlerService.PushHandler(this.escapeHandler);
			}
			this.adornerWindowToScreenOffset = base.BehaviorService.AdornerWindowToScreen();
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x000BF528 File Offset: 0x000BD728
		protected override void OnMouseDragEnd(bool cancel)
		{
			if (this.mouseDragBase == ControlDesigner.InvalidPoint)
			{
				base.OnMouseDragEnd(cancel);
				return;
			}
			Rectangle rectangle = this.mouseDragOffset;
			ToolboxItem toolboxItem = this.mouseDragTool;
			Point location = this.mouseDragBase;
			this.mouseDragOffset = Rectangle.Empty;
			this.mouseDragBase = ControlDesigner.InvalidPoint;
			this.mouseDragTool = null;
			this.Control.Capture = false;
			Cursor.Clip = Rectangle.Empty;
			if (!rectangle.IsEmpty && this.graphics != null)
			{
				Rectangle rectangle2 = new Rectangle(rectangle.X - this.adornerWindowToScreenOffset.X, rectangle.Y - this.adornerWindowToScreenOffset.Y, rectangle.Width, rectangle.Height);
				int num = ParentControlDesigner.FrameWidth(this.mouseDragFrame);
				this.graphics.SetClip(rectangle2);
				using (Region region = new Region(rectangle2))
				{
					region.Exclude(Rectangle.Inflate(rectangle2, -num, -num));
					base.BehaviorService.Invalidate(region);
				}
				this.graphics.ResetClip();
			}
			if (this.graphics != null)
			{
				this.graphics.Dispose();
				this.graphics = null;
			}
			if (this.dragManager != null)
			{
				this.dragManager.OnMouseUp();
				this.dragManager = null;
			}
			IEventHandlerService eventHandlerService = (IEventHandlerService)this.GetService(typeof(IEventHandlerService));
			if (eventHandlerService != null && this.escapeHandler != null)
			{
				eventHandlerService.PopHandler(this.escapeHandler);
				this.escapeHandler = null;
			}
			if (this.statusCommandUI != null && !rectangle.IsEmpty)
			{
				NativeMethods.POINT point = new NativeMethods.POINT(location.X, location.Y);
				NativeMethods.MapWindowPoints(IntPtr.Zero, this.Control.Handle, point, 1);
				if (this.statusCommandUI != null)
				{
					this.statusCommandUI.SetStatusInformation(new Rectangle(point.x, point.y, rectangle.Width, rectangle.Height));
				}
			}
			if (rectangle.IsEmpty && !cancel)
			{
				if (toolboxItem != null)
				{
					try
					{
						this.CreateTool(toolboxItem, location);
						if (this.toolboxService != null)
						{
							this.toolboxService.SelectedToolboxItemUsed();
						}
					}
					catch (Exception ex)
					{
						if (ClientUtils.IsCriticalException(ex))
						{
							throw;
						}
						base.DisplayError(ex);
					}
				}
				return;
			}
			if (cancel)
			{
				return;
			}
			if (toolboxItem != null)
			{
				try
				{
					Size size = new Size(DesignerUtils.MinDragSize.Width * 2, DesignerUtils.MinDragSize.Height * 2);
					if (rectangle.Width < size.Width)
					{
						rectangle.Width = size.Width;
					}
					if (rectangle.Height < size.Height)
					{
						rectangle.Height = size.Height;
					}
					this.CreateTool(toolboxItem, rectangle);
					if (this.toolboxService != null)
					{
						this.toolboxService.SelectedToolboxItemUsed();
					}
					return;
				}
				catch (Exception ex2)
				{
					if (ClientUtils.IsCriticalException(ex2))
					{
						throw;
					}
					base.DisplayError(ex2);
					return;
				}
			}
			ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
			if (selectionService != null)
			{
				object[] componentsInRect = this.GetComponentsInRect(rectangle, true, false);
				if (componentsInRect.Length != 0)
				{
					selectionService.SetSelectedComponents(componentsInRect);
				}
			}
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x000BF868 File Offset: 0x000BDA68
		protected override void OnMouseDragMove(int x, int y)
		{
			if (this.toolboxItemSnapLineBehavior != null && this.toolboxItemSnapLineBehavior.IsPushed)
			{
				base.BehaviorService.PopBehavior(this.toolboxItemSnapLineBehavior);
				this.toolboxItemSnapLineBehavior.IsPushed = false;
			}
			if (this.GetOleDragHandler().Dragging || this.mouseDragBase == ControlDesigner.InvalidPoint)
			{
				return;
			}
			Rectangle rect = this.mouseDragOffset;
			this.mouseDragOffset.X = this.mouseDragBase.X;
			this.mouseDragOffset.Y = this.mouseDragBase.Y;
			this.mouseDragOffset.Width = x - this.mouseDragBase.X;
			this.mouseDragOffset.Height = y - this.mouseDragBase.Y;
			if (this.dragManager == null && this.ParticipatesWithSnapLines && this.mouseDragTool != null && base.BehaviorService.UseSnapLines)
			{
				this.dragManager = new DragAssistanceManager(base.Component.Site);
			}
			if (this.dragManager != null)
			{
				Rectangle rectangle = new Rectangle(this.mouseDragBase.X - this.adornerWindowToScreenOffset.X, this.mouseDragBase.Y - this.adornerWindowToScreenOffset.Y, x - this.mouseDragBase.X, y - this.mouseDragBase.Y);
				Point point = this.dragManager.OnMouseMove(rectangle, this.GenerateNewToolSnapLines(rectangle));
				this.mouseDragOffset.Width = this.mouseDragOffset.Width + point.X;
				this.mouseDragOffset.Height = this.mouseDragOffset.Height + point.Y;
				this.dragManager.RenderSnapLinesInternal();
			}
			if (this.mouseDragOffset.Width < 0)
			{
				this.mouseDragOffset.X = this.mouseDragOffset.X + this.mouseDragOffset.Width;
				this.mouseDragOffset.Width = -this.mouseDragOffset.Width;
			}
			if (this.mouseDragOffset.Height < 0)
			{
				this.mouseDragOffset.Y = this.mouseDragOffset.Y + this.mouseDragOffset.Height;
				this.mouseDragOffset.Height = -this.mouseDragOffset.Height;
			}
			if (this.mouseDragTool != null)
			{
				this.mouseDragOffset = this.Control.RectangleToClient(this.mouseDragOffset);
				this.mouseDragOffset = this.GetUpdatedRect(Rectangle.Empty, this.mouseDragOffset, true);
				this.mouseDragOffset = this.Control.RectangleToScreen(this.mouseDragOffset);
			}
			if (this.graphics == null)
			{
				this.graphics = base.BehaviorService.AdornerWindowGraphics;
			}
			if (!this.mouseDragOffset.IsEmpty && this.graphics != null)
			{
				Rectangle rect2 = new Rectangle(this.mouseDragOffset.X - this.adornerWindowToScreenOffset.X, this.mouseDragOffset.Y - this.adornerWindowToScreenOffset.Y, this.mouseDragOffset.Width, this.mouseDragOffset.Height);
				using (Region region = new Region(rect2))
				{
					int num = ParentControlDesigner.FrameWidth(this.mouseDragFrame);
					region.Exclude(Rectangle.Inflate(rect2, -num, -num));
					if (!rect.IsEmpty)
					{
						rect.X -= this.adornerWindowToScreenOffset.X;
						rect.Y -= this.adornerWindowToScreenOffset.Y;
						using (Region region2 = new Region(rect))
						{
							region2.Exclude(Rectangle.Inflate(rect, -num, -num));
							base.BehaviorService.Invalidate(region2);
						}
					}
					DesignerUtils.DrawFrame(this.graphics, region, this.mouseDragFrame, this.Control.BackColor);
				}
			}
			if (this.statusCommandUI != null)
			{
				NativeMethods.POINT point2 = new NativeMethods.POINT(this.mouseDragOffset.X, this.mouseDragOffset.Y);
				NativeMethods.MapWindowPoints(IntPtr.Zero, this.Control.Handle, point2, 1);
				if (this.statusCommandUI != null)
				{
					this.statusCommandUI.SetStatusInformation(new Rectangle(point2.x, point2.y, this.mouseDragOffset.Width, this.mouseDragOffset.Height));
				}
			}
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x000BFCC0 File Offset: 0x000BDEC0
		protected override void OnPaintAdornments(PaintEventArgs pe)
		{
			if (this.DrawGrid)
			{
				Control control = this.Control;
				Rectangle displayRectangle = this.Control.DisplayRectangle;
				Rectangle clientRectangle = this.Control.ClientRectangle;
				Rectangle area = new Rectangle(Math.Min(displayRectangle.X, clientRectangle.X), Math.Min(displayRectangle.Y, clientRectangle.Y), Math.Max(displayRectangle.Width, clientRectangle.Width), Math.Max(displayRectangle.Height, clientRectangle.Height));
				float num = (float)area.X;
				float num2 = (float)area.Y;
				pe.Graphics.TranslateTransform(num, num2);
				area.X = (area.Y = 0);
				int num3 = area.Width;
				area.Width = num3 + 1;
				num3 = area.Height;
				area.Height = num3 + 1;
				ControlPaint.DrawGrid(pe.Graphics, area, this.GridSize, control.BackColor);
				pe.Graphics.TranslateTransform(-num, -num2);
			}
			base.OnPaintAdornments(pe);
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x000BFDD7 File Offset: 0x000BDFD7
		private void OnScroll(object sender, ScrollEventArgs se)
		{
			base.BehaviorService.Invalidate(base.BehaviorService.ControlRectInAdornerWindow(this.Control));
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x000BFDF8 File Offset: 0x000BDFF8
		protected override void OnSetCursor()
		{
			if (this.toolboxService == null)
			{
				this.toolboxService = (IToolboxService)this.GetService(typeof(IToolboxService));
			}
			try
			{
				if (this.toolboxService == null || !this.toolboxService.SetCursor() || this.InheritanceAttribute.Equals(InheritanceAttribute.InheritedReadOnly))
				{
					Cursor.Current = Cursors.Default;
				}
			}
			catch
			{
				Cursor.Current = Cursors.Default;
			}
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x000BFE7C File Offset: 0x000BE07C
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			if (!this.DefaultUseSnapLines)
			{
				properties["DrawGrid"] = TypeDescriptor.CreateProperty(typeof(ParentControlDesigner), "DrawGrid", typeof(bool), new Attribute[]
				{
					BrowsableAttribute.Yes,
					DesignOnlyAttribute.Yes,
					new SRDescriptionAttribute("ParentControlDesignerDrawGridDescr"),
					CategoryAttribute.Design
				});
				properties["SnapToGrid"] = TypeDescriptor.CreateProperty(typeof(ParentControlDesigner), "SnapToGrid", typeof(bool), new Attribute[]
				{
					BrowsableAttribute.Yes,
					DesignOnlyAttribute.Yes,
					new SRDescriptionAttribute("ParentControlDesignerSnapToGridDescr"),
					CategoryAttribute.Design
				});
				properties["GridSize"] = TypeDescriptor.CreateProperty(typeof(ParentControlDesigner), "GridSize", typeof(Size), new Attribute[]
				{
					BrowsableAttribute.Yes,
					new SRDescriptionAttribute("ParentControlDesignerGridSizeDescr"),
					DesignOnlyAttribute.Yes,
					CategoryAttribute.Design
				});
			}
			properties["CurrentGridSize"] = TypeDescriptor.CreateProperty(typeof(ParentControlDesigner), "CurrentGridSize", typeof(Size), new Attribute[]
			{
				BrowsableAttribute.No,
				DesignerSerializationVisibilityAttribute.Hidden
			});
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x000BFFD8 File Offset: 0x000BE1D8
		private void ReParentControls(Control newParent, ArrayList controls, string transactionName, IDesignerHost host)
		{
			using (DesignerTransaction designerTransaction = host.CreateTransaction(transactionName))
			{
				IComponentChangeService componentChangeService = this.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
				PropertyDescriptor member = TypeDescriptor.GetProperties(newParent)["Controls"];
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(newParent)["Location"];
				Point point = Point.Empty;
				if (propertyDescriptor != null)
				{
					point = (Point)propertyDescriptor.GetValue(newParent);
				}
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanging(newParent, member);
				}
				foreach (object obj in controls)
				{
					Control control = obj as Control;
					Control parent = control.Parent;
					Point point2 = Point.Empty;
					InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(control)[typeof(InheritanceAttribute)];
					if (inheritanceAttribute == null || inheritanceAttribute != InheritanceAttribute.InheritedReadOnly)
					{
						PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(control)["Location"];
						if (propertyDescriptor2 != null)
						{
							point2 = (Point)propertyDescriptor2.GetValue(control);
						}
						if (parent != null)
						{
							if (componentChangeService != null)
							{
								componentChangeService.OnComponentChanging(parent, member);
							}
							parent.Controls.Remove(control);
						}
						newParent.Controls.Add(control);
						Point empty = Point.Empty;
						if (parent != null)
						{
							if (parent.Controls.Contains(newParent))
							{
								empty = new Point(point2.X - point.X, point2.Y - point.Y);
							}
							else
							{
								Point point3 = (Point)propertyDescriptor2.GetValue(parent);
								empty = new Point(point2.X + point3.X, point2.Y + point3.Y);
							}
						}
						propertyDescriptor2.SetValue(control, empty);
						if (componentChangeService != null && parent != null)
						{
							componentChangeService.OnComponentChanged(parent, member, null, null);
						}
					}
				}
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanged(newParent, member, null, null);
				}
				designerTransaction.Commit();
			}
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x000C0208 File Offset: 0x000BE408
		private bool ShouldSerializeDrawGrid()
		{
			ParentControlDesigner parentControlDesignerOfParent = this.GetParentControlDesignerOfParent();
			if (parentControlDesignerOfParent != null)
			{
				return this.DrawGrid != parentControlDesignerOfParent.DrawGrid;
			}
			return !this.IsOptionDefault("ShowGrid", this.DrawGrid);
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x000C024C File Offset: 0x000BE44C
		private bool ShouldSerializeSnapToGrid()
		{
			ParentControlDesigner parentControlDesignerOfParent = this.GetParentControlDesignerOfParent();
			if (parentControlDesignerOfParent != null)
			{
				return this.SnapToGrid != parentControlDesignerOfParent.SnapToGrid;
			}
			return !this.IsOptionDefault("SnapToGrid", this.SnapToGrid);
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x000C0290 File Offset: 0x000BE490
		private bool ShouldSerializeGridSize()
		{
			ParentControlDesigner parentControlDesignerOfParent = this.GetParentControlDesignerOfParent();
			if (parentControlDesignerOfParent != null)
			{
				return !this.GridSize.Equals(parentControlDesignerOfParent.GridSize);
			}
			return !this.IsOptionDefault("GridSize", this.GridSize);
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x000C02E4 File Offset: 0x000BE4E4
		private void ResetGridSize()
		{
			this.getDefaultGridSize = true;
			this.parentCanSetGridSize = true;
			Control control = this.Control;
			if (control != null)
			{
				control.Invalidate(true);
			}
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x000C0310 File Offset: 0x000BE510
		private void ResetDrawGrid()
		{
			this.getDefaultDrawGrid = true;
			this.parentCanSetDrawGrid = true;
			Control control = this.Control;
			if (control != null)
			{
				control.Invalidate(true);
			}
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x000C033C File Offset: 0x000BE53C
		private void ResetSnapToGrid()
		{
			this.getDefaultGridSnap = true;
			this.parentCanSetGridSnap = true;
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001FB2 RID: 8114 RVA: 0x000C034C File Offset: 0x000BE54C
		IComponent IOleDragClient.Component
		{
			get
			{
				return base.Component;
			}
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x000C0354 File Offset: 0x000BE554
		bool IOleDragClient.AddComponent(IComponent component, string name, bool firstAdd)
		{
			IContainer container = DesignerUtils.CheckForNestedContainer(base.Component.Site.Container);
			bool flag = true;
			IContainer container2 = null;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (!firstAdd)
			{
				if (component.Site != null)
				{
					container2 = component.Site.Container;
					flag = (container != container2);
					if (flag)
					{
						container2.Remove(component);
					}
				}
				if (flag)
				{
					if (name != null && container.Components[name] != null)
					{
						name = null;
					}
					if (name != null)
					{
						container.Add(component, name);
					}
					else
					{
						container.Add(component);
					}
				}
			}
			if (!((IOleDragClient)this).IsDropOk(component))
			{
				try
				{
					IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
					string @string = SR.GetString("DesignerCantParentType", new object[]
					{
						component.GetType().Name,
						base.Component.GetType().Name
					});
					if (iuiservice != null)
					{
						iuiservice.ShowError(@string);
					}
					else
					{
						RTLAwareMessageBox.Show(null, @string, null, MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
					}
					return false;
				}
				finally
				{
					if (flag)
					{
						container.Remove(component);
						if (container2 != null)
						{
							container2.Add(component);
						}
					}
					else
					{
						container.Remove(component);
					}
				}
			}
			if (!this.CanAddComponent(component))
			{
				return false;
			}
			Control control = this.GetControl(component);
			if (control != null)
			{
				Control parentForComponent = this.GetParentForComponent(component);
				Form form = control as Form;
				if (form == null || !form.TopLevel)
				{
					if (control.Parent != parentForComponent)
					{
						PropertyDescriptor member = TypeDescriptor.GetProperties(parentForComponent)["Controls"];
						if (control.Parent != null)
						{
							Control parent = control.Parent;
							if (this.componentChangeSvc != null)
							{
								this.componentChangeSvc.OnComponentChanging(parent, member);
							}
							parent.Controls.Remove(control);
							if (this.componentChangeSvc != null)
							{
								this.componentChangeSvc.OnComponentChanged(parent, member, parent.Controls, parent.Controls);
							}
						}
						if (this.suspendChanging == 0 && this.componentChangeSvc != null)
						{
							this.componentChangeSvc.OnComponentChanging(parentForComponent, member);
						}
						parentForComponent.Controls.Add(control);
						if (this.componentChangeSvc != null)
						{
							this.componentChangeSvc.OnComponentChanged(parentForComponent, member, parentForComponent.Controls, parentForComponent.Controls);
						}
					}
					else
					{
						int childIndex = parentForComponent.Controls.GetChildIndex(control);
						parentForComponent.Controls.Remove(control);
						parentForComponent.Controls.Add(control);
						parentForComponent.Controls.SetChildIndex(control, childIndex);
					}
				}
				control.Invalidate(true);
			}
			if (designerHost != null && flag)
			{
				IComponentInitializer componentInitializer = designerHost.GetDesigner(component) as IComponentInitializer;
				if (componentInitializer != null)
				{
					componentInitializer.InitializeExistingComponent(null);
				}
				this.AddChildComponents(component, container, designerHost);
			}
			return true;
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001FB4 RID: 8116 RVA: 0x000C0610 File Offset: 0x000BE810
		bool IOleDragClient.CanModifyComponents
		{
			get
			{
				return !this.InheritanceAttribute.Equals(InheritanceAttribute.InheritedReadOnly);
			}
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x000C0628 File Offset: 0x000BE828
		bool IOleDragClient.IsDropOk(IComponent component)
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				IDesigner designer = designerHost.GetDesigner(component);
				bool flag = false;
				if (designer == null)
				{
					designer = TypeDescriptor.CreateDesigner(component, typeof(IDesigner));
					ControlDesigner controlDesigner = designer as ControlDesigner;
					if (controlDesigner != null)
					{
						controlDesigner.ForceVisible = false;
					}
					designer.Initialize(component);
					flag = true;
				}
				try
				{
					ComponentDesigner componentDesigner = designer as ComponentDesigner;
					if (componentDesigner != null)
					{
						if (!componentDesigner.CanBeAssociatedWith(this))
						{
							return false;
						}
						ControlDesigner controlDesigner2 = componentDesigner as ControlDesigner;
						if (controlDesigner2 != null)
						{
							return this.CanParent(controlDesigner2);
						}
					}
				}
				finally
				{
					if (flag)
					{
						designer.Dispose();
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x000BD904 File Offset: 0x000BBB04
		Control IOleDragClient.GetDesignerControl()
		{
			return this.Control;
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x000C06DC File Offset: 0x000BE8DC
		Control IOleDragClient.GetControlForComponent(object component)
		{
			return this.GetControl(component);
		}

		// Token: 0x04001869 RID: 6249
		private static BooleanSwitch StepControls = new BooleanSwitch("StepControls", "ParentControlDesigner: step added controls");

		// Token: 0x0400186A RID: 6250
		private Point mouseDragBase = ControlDesigner.InvalidPoint;

		// Token: 0x0400186B RID: 6251
		private Rectangle mouseDragOffset = Rectangle.Empty;

		// Token: 0x0400186C RID: 6252
		private ToolboxItem mouseDragTool;

		// Token: 0x0400186D RID: 6253
		private FrameStyle mouseDragFrame;

		// Token: 0x0400186E RID: 6254
		private OleDragDropHandler oleDragDropHandler;

		// Token: 0x0400186F RID: 6255
		private ParentControlDesigner.EscapeHandler escapeHandler;

		// Token: 0x04001870 RID: 6256
		private Control pendingRemoveControl;

		// Token: 0x04001871 RID: 6257
		private IComponentChangeService componentChangeSvc;

		// Token: 0x04001872 RID: 6258
		private DragAssistanceManager dragManager;

		// Token: 0x04001873 RID: 6259
		private ToolboxSnapDragDropEventArgs toolboxSnapDragDropEventArgs;

		// Token: 0x04001874 RID: 6260
		private ToolboxItemSnapLineBehavior toolboxItemSnapLineBehavior;

		// Token: 0x04001875 RID: 6261
		private Graphics graphics;

		// Token: 0x04001876 RID: 6262
		private IToolboxService toolboxService;

		// Token: 0x04001877 RID: 6263
		private const int minGridSize = 2;

		// Token: 0x04001878 RID: 6264
		private const int maxGridSize = 200;

		// Token: 0x04001879 RID: 6265
		private Point adornerWindowToScreenOffset;

		// Token: 0x0400187A RID: 6266
		private bool checkSnapLineSetting = true;

		// Token: 0x0400187B RID: 6267
		private bool defaultUseSnapLines;

		// Token: 0x0400187C RID: 6268
		private bool gridSnap = true;

		// Token: 0x0400187D RID: 6269
		private Size gridSize = Size.Empty;

		// Token: 0x0400187E RID: 6270
		private bool drawGrid = true;

		// Token: 0x0400187F RID: 6271
		private bool parentCanSetDrawGrid = true;

		// Token: 0x04001880 RID: 6272
		private bool parentCanSetGridSize = true;

		// Token: 0x04001881 RID: 6273
		private bool parentCanSetGridSnap = true;

		// Token: 0x04001882 RID: 6274
		private bool getDefaultDrawGrid = true;

		// Token: 0x04001883 RID: 6275
		private bool getDefaultGridSize = true;

		// Token: 0x04001884 RID: 6276
		private bool getDefaultGridSnap = true;

		// Token: 0x04001885 RID: 6277
		private StatusCommandUI statusCommandUI;

		// Token: 0x04001886 RID: 6278
		private int suspendChanging;

		// Token: 0x02000587 RID: 1415
		private class EscapeHandler : IMenuStatusHandler
		{
			// Token: 0x0600329C RID: 12956 RVA: 0x00111C4A File Offset: 0x0010FE4A
			public EscapeHandler(ParentControlDesigner designer)
			{
				this.designer = designer;
			}

			// Token: 0x0600329D RID: 12957 RVA: 0x00111C59 File Offset: 0x0010FE59
			public bool OverrideInvoke(MenuCommand cmd)
			{
				if (cmd.CommandID.Equals(MenuCommands.KeyCancel))
				{
					this.designer.OnMouseDragEnd(true);
					return true;
				}
				return false;
			}

			// Token: 0x0600329E RID: 12958 RVA: 0x00111C7C File Offset: 0x0010FE7C
			public bool OverrideStatus(MenuCommand cmd)
			{
				if (cmd.CommandID.Equals(MenuCommands.KeyCancel))
				{
					cmd.Enabled = true;
				}
				else
				{
					cmd.Enabled = false;
				}
				return true;
			}

			// Token: 0x040021B0 RID: 8624
			private ParentControlDesigner designer;
		}
	}
}
