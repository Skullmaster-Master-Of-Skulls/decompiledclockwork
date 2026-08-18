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
	// Token: 0x02000226 RID: 550
	public class ParentControlDesigner : ControlDesigner, IOleDragClient
	{
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x00069501 File Offset: 0x00068501
		protected virtual bool AllowControlLasso
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x00069504 File Offset: 0x00068504
		protected virtual bool AllowGenericDragBox
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x00069507 File Offset: 0x00068507
		protected internal virtual bool AllowSetChildIndexOnDrop
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x0006950A File Offset: 0x0006850A
		private Size CurrentGridSize
		{
			get
			{
				return this.GridSize;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x00069512 File Offset: 0x00068512
		protected virtual Point DefaultControlLocation
		{
			get
			{
				return new Point(0, 0);
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x0006951B File Offset: 0x0006851B
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

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x00069548 File Offset: 0x00068548
		// (set) Token: 0x060014A6 RID: 5286 RVA: 0x000695B4 File Offset: 0x000685B4
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

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x0006968C File Offset: 0x0006868C
		protected override bool EnableDragRect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x0006968F File Offset: 0x0006868F
		internal Size ParentGridSize
		{
			get
			{
				return this.GridSize;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x00069698 File Offset: 0x00068698
		// (set) Token: 0x060014AA RID: 5290 RVA: 0x00069700 File Offset: 0x00068700
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

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x00069830 File Offset: 0x00068830
		protected ToolboxItem MouseDragTool
		{
			get
			{
				return this.mouseDragTool;
			}
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00069838 File Offset: 0x00068838
		protected virtual Control GetParentForComponent(IComponent component)
		{
			return this.Control;
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x00069840 File Offset: 0x00068840
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

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x00069904 File Offset: 0x00068904
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

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x00069930 File Offset: 0x00068930
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

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x00069948 File Offset: 0x00068948
		// (set) Token: 0x060014B1 RID: 5297 RVA: 0x000699B4 File Offset: 0x000689B4
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

		// Token: 0x060014B2 RID: 5298 RVA: 0x00069A74 File Offset: 0x00068A74
		internal static int DetermineTopChildIndex(Control parent)
		{
			int i;
			for (i = 0; i < parent.Controls.Count - 1; i++)
			{
				Control control = parent.Controls[i];
				if (control.Site != null)
				{
					InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(control)[typeof(InheritanceAttribute)];
					InheritanceLevel inheritanceLevel = InheritanceLevel.NotInherited;
					if (inheritanceAttribute != null)
					{
						inheritanceLevel = inheritanceAttribute.InheritanceLevel;
					}
					if (inheritanceLevel == InheritanceLevel.NotInherited)
					{
						break;
					}
				}
			}
			return i;
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x00069ADC File Offset: 0x00068ADC
		internal virtual void AddChildControl(Control newChild)
		{
			if (newChild.Left == 0 && newChild.Top == 0 && newChild.Width >= this.Control.Width && newChild.Height >= this.Control.Height)
			{
				Point location = newChild.Location;
				location.Offset(this.GridSize.Width, this.GridSize.Height);
				newChild.Location = location;
			}
			this.Control.Controls.Add(newChild);
			int newIndex = ParentControlDesigner.DetermineTopChildIndex(this.Control);
			this.Control.Controls.SetChildIndex(newChild, newIndex);
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x00069B80 File Offset: 0x00068B80
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

		// Token: 0x060014B5 RID: 5301 RVA: 0x00069F7C File Offset: 0x00068F7C
		private void AddChildComponents(IComponent component, IContainer container, IDesignerHost host)
		{
			Control control = this.GetControl(component);
			if (control != null)
			{
				Control control2 = control;
				Control[] array = new Control[control2.Controls.Count];
				control2.Controls.CopyTo(array, 0);
				host.GetDesigner(component);
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

		// Token: 0x060014B6 RID: 5302 RVA: 0x0006A0BC File Offset: 0x000690BC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.OnMouseDragEnd(false);
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

		// Token: 0x060014B7 RID: 5303 RVA: 0x0006A15C File Offset: 0x0006915C
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

		// Token: 0x060014B8 RID: 5304 RVA: 0x0006A190 File Offset: 0x00069190
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

		// Token: 0x060014B9 RID: 5305 RVA: 0x0006A1C4 File Offset: 0x000691C4
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

		// Token: 0x060014BA RID: 5306 RVA: 0x0006A1F5 File Offset: 0x000691F5
		protected static void InvokeCreateTool(ParentControlDesigner toInvoke, ToolboxItem tool)
		{
			toInvoke.CreateTool(tool);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0006A1FE File Offset: 0x000691FE
		public virtual bool CanParent(ControlDesigner controlDesigner)
		{
			return this.CanParent(controlDesigner.Control);
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0006A20C File Offset: 0x0006920C
		public virtual bool CanParent(Control control)
		{
			return !control.Contains(this.Control);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0006A21D File Offset: 0x0006921D
		protected void CreateTool(ToolboxItem tool)
		{
			this.CreateToolCore(tool, 0, 0, 0, 0, false, false);
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0006A22D File Offset: 0x0006922D
		protected void CreateTool(ToolboxItem tool, Point location)
		{
			this.CreateToolCore(tool, location.X, location.Y, 0, 0, true, false);
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0006A249 File Offset: 0x00069249
		protected void CreateTool(ToolboxItem tool, Rectangle bounds)
		{
			this.CreateToolCore(tool, bounds.X, bounds.Y, bounds.Width, bounds.Height, true, true);
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0006A274 File Offset: 0x00069274
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

		// Token: 0x060014C1 RID: 5313 RVA: 0x0006A2C4 File Offset: 0x000692C4
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

		// Token: 0x060014C2 RID: 5314 RVA: 0x0006A31C File Offset: 0x0006931C
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

		// Token: 0x060014C3 RID: 5315 RVA: 0x0006A3E4 File Offset: 0x000693E4
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

		// Token: 0x060014C4 RID: 5316 RVA: 0x0006A42C File Offset: 0x0006942C
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

		// Token: 0x060014C5 RID: 5317 RVA: 0x0006A4A0 File Offset: 0x000694A0
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

		// Token: 0x060014C6 RID: 5318 RVA: 0x0006A598 File Offset: 0x00069598
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

		// Token: 0x060014C7 RID: 5319 RVA: 0x0006A650 File Offset: 0x00069650
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

		// Token: 0x060014C8 RID: 5320 RVA: 0x0006A719 File Offset: 0x00069719
		internal OleDragDropHandler GetOleDragHandler()
		{
			if (this.oleDragDropHandler == null)
			{
				this.oleDragDropHandler = new OleDragDropHandler(null, (IServiceProvider)this.GetService(typeof(IDesignerHost)), this);
			}
			return this.oleDragDropHandler;
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0006A74C File Offset: 0x0006974C
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

		// Token: 0x060014CA RID: 5322 RVA: 0x0006A790 File Offset: 0x00069790
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

		// Token: 0x060014CB RID: 5323 RVA: 0x0006A808 File Offset: 0x00069808
		internal Point GetSnappedPoint(Point pt)
		{
			Rectangle updatedRect = this.GetUpdatedRect(Rectangle.Empty, new Rectangle(pt.X, pt.Y, 0, 0), false);
			return new Point(updatedRect.X, updatedRect.Y);
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0006A84A File Offset: 0x0006984A
		internal Rectangle GetSnappedRect(Rectangle originalRect, Rectangle dragRect, bool updateSize)
		{
			return this.GetUpdatedRect(originalRect, dragRect, updateSize);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0006A858 File Offset: 0x00069858
		protected Rectangle GetUpdatedRect(Rectangle originalRect, Rectangle dragRect, bool updateSize)
		{
			Rectangle result = Rectangle.Empty;
			if (this.SnapToGrid)
			{
				Size size = this.GridSize;
				Point point = new Point(size.Width / 2, size.Height / 2);
				result = dragRect;
				int y = dragRect.Y;
				int height = dragRect.Height;
				int x = dragRect.X;
				int width = dragRect.Width;
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

		// Token: 0x060014CE RID: 5326 RVA: 0x0006AA30 File Offset: 0x00069A30
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

		// Token: 0x060014CF RID: 5327 RVA: 0x0006AAF0 File Offset: 0x00069AF0
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

		// Token: 0x060014D0 RID: 5328 RVA: 0x0006AC1C File Offset: 0x00069C1C
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

		// Token: 0x060014D1 RID: 5329 RVA: 0x0006ACA8 File Offset: 0x00069CA8
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

		// Token: 0x060014D2 RID: 5330 RVA: 0x0006AD0F File Offset: 0x00069D0F
		private void OnComponentRemoved(object sender, ComponentEventArgs e)
		{
			if (e.Component == this.pendingRemoveControl)
			{
				this.pendingRemoveControl = null;
				this.componentChangeSvc.OnComponentChanged(this.Control, TypeDescriptor.GetProperties(this.Control)["Controls"], null, null);
			}
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x0006AD4E File Offset: 0x00069D4E
		internal void SuspendChangingEvents()
		{
			this.suspendChanging++;
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0006AD5E File Offset: 0x00069D5E
		internal void ResumeChangingEvents()
		{
			this.suspendChanging--;
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0006AD6E File Offset: 0x00069D6E
		internal void ForceComponentChanging()
		{
			this.componentChangeSvc.OnComponentChanging(this.Control, TypeDescriptor.GetProperties(this.Control)["Controls"]);
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0006AD98 File Offset: 0x00069D98
		protected override void OnDragComplete(DragEventArgs de)
		{
			DropSourceBehavior.BehaviorDataObject behaviorDataObject = de.Data as DropSourceBehavior.BehaviorDataObject;
			if (behaviorDataObject != null)
			{
				behaviorDataObject.CleanupDrag();
			}
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0006ADBC File Offset: 0x00069DBC
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
			}
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x0006AF10 File Offset: 0x00069F10
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

		// Token: 0x060014D9 RID: 5337 RVA: 0x0006B21C File Offset: 0x0006A21C
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

		// Token: 0x060014DA RID: 5338 RVA: 0x0006B292 File Offset: 0x0006A292
		protected override void OnDragLeave(EventArgs e)
		{
			if (this.toolboxItemSnapLineBehavior != null && this.toolboxItemSnapLineBehavior.IsPushed)
			{
				base.BehaviorService.PopBehavior(this.toolboxItemSnapLineBehavior);
				this.toolboxItemSnapLineBehavior.IsPushed = false;
			}
			this.mouseDragTool = null;
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0006B2D0 File Offset: 0x0006A2D0
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
			}
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0006B397 File Offset: 0x0006A397
		private static int FrameWidth(FrameStyle style)
		{
			if (style != FrameStyle.Dashed)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0006B3A0 File Offset: 0x0006A3A0
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

		// Token: 0x060014DE RID: 5342 RVA: 0x0006B4F4 File Offset: 0x0006A4F4
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
				if (componentsInRect.Length > 0)
				{
					selectionService.SetSelectedComponents(componentsInRect);
				}
			}
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0006B834 File Offset: 0x0006A834
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

		// Token: 0x060014E0 RID: 5344 RVA: 0x0006BC8C File Offset: 0x0006AC8C
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
				area.Width++;
				area.Height++;
				ControlPaint.DrawGrid(pe.Graphics, area, this.GridSize, control.BackColor);
				pe.Graphics.TranslateTransform(-num, -num2);
			}
			base.OnPaintAdornments(pe);
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0006BD9B File Offset: 0x0006AD9B
		private void OnScroll(object sender, ScrollEventArgs se)
		{
			base.BehaviorService.Invalidate(base.BehaviorService.ControlRectInAdornerWindow(this.Control));
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0006BDBC File Offset: 0x0006ADBC
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

		// Token: 0x060014E3 RID: 5347 RVA: 0x0006BE3C File Offset: 0x0006AE3C
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

		// Token: 0x060014E4 RID: 5348 RVA: 0x0006BFA0 File Offset: 0x0006AFA0
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

		// Token: 0x060014E5 RID: 5349 RVA: 0x0006C1D0 File Offset: 0x0006B1D0
		private bool ShouldSerializeDrawGrid()
		{
			ParentControlDesigner parentControlDesignerOfParent = this.GetParentControlDesignerOfParent();
			if (parentControlDesignerOfParent != null)
			{
				return this.DrawGrid != parentControlDesignerOfParent.DrawGrid;
			}
			return !this.IsOptionDefault("ShowGrid", this.DrawGrid);
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0006C214 File Offset: 0x0006B214
		private bool ShouldSerializeSnapToGrid()
		{
			ParentControlDesigner parentControlDesignerOfParent = this.GetParentControlDesignerOfParent();
			if (parentControlDesignerOfParent != null)
			{
				return this.SnapToGrid != parentControlDesignerOfParent.SnapToGrid;
			}
			return !this.IsOptionDefault("SnapToGrid", this.SnapToGrid);
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x0006C258 File Offset: 0x0006B258
		private bool ShouldSerializeGridSize()
		{
			ParentControlDesigner parentControlDesignerOfParent = this.GetParentControlDesignerOfParent();
			if (parentControlDesignerOfParent != null)
			{
				return !this.GridSize.Equals(parentControlDesignerOfParent.GridSize);
			}
			return !this.IsOptionDefault("GridSize", this.GridSize);
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0006C2AC File Offset: 0x0006B2AC
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

		// Token: 0x060014E9 RID: 5353 RVA: 0x0006C2D8 File Offset: 0x0006B2D8
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

		// Token: 0x060014EA RID: 5354 RVA: 0x0006C304 File Offset: 0x0006B304
		private void ResetSnapToGrid()
		{
			this.getDefaultGridSnap = true;
			this.parentCanSetGridSnap = true;
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x0006C314 File Offset: 0x0006B314
		IComponent IOleDragClient.Component
		{
			get
			{
				return base.Component;
			}
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x0006C31C File Offset: 0x0006B31C
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
					IDesignerHost designerHost2 = (IDesignerHost)component.Site.GetService(typeof(IDesignerHost));
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

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x0006C5EC File Offset: 0x0006B5EC
		bool IOleDragClient.CanModifyComponents
		{
			get
			{
				return !this.InheritanceAttribute.Equals(InheritanceAttribute.InheritedReadOnly);
			}
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0006C604 File Offset: 0x0006B604
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

		// Token: 0x060014EF RID: 5359 RVA: 0x0006C6B8 File Offset: 0x0006B6B8
		Control IOleDragClient.GetDesignerControl()
		{
			return this.Control;
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0006C6C0 File Offset: 0x0006B6C0
		Control IOleDragClient.GetControlForComponent(object component)
		{
			return this.GetControl(component);
		}

		// Token: 0x0400123D RID: 4669
		private const int minGridSize = 2;

		// Token: 0x0400123E RID: 4670
		private const int maxGridSize = 200;

		// Token: 0x0400123F RID: 4671
		private static BooleanSwitch StepControls = new BooleanSwitch("StepControls", "ParentControlDesigner: step added controls");

		// Token: 0x04001240 RID: 4672
		private Point mouseDragBase = ControlDesigner.InvalidPoint;

		// Token: 0x04001241 RID: 4673
		private Rectangle mouseDragOffset = Rectangle.Empty;

		// Token: 0x04001242 RID: 4674
		private ToolboxItem mouseDragTool;

		// Token: 0x04001243 RID: 4675
		private FrameStyle mouseDragFrame;

		// Token: 0x04001244 RID: 4676
		private OleDragDropHandler oleDragDropHandler;

		// Token: 0x04001245 RID: 4677
		private ParentControlDesigner.EscapeHandler escapeHandler;

		// Token: 0x04001246 RID: 4678
		private Control pendingRemoveControl;

		// Token: 0x04001247 RID: 4679
		private IComponentChangeService componentChangeSvc;

		// Token: 0x04001248 RID: 4680
		private DragAssistanceManager dragManager;

		// Token: 0x04001249 RID: 4681
		private ToolboxSnapDragDropEventArgs toolboxSnapDragDropEventArgs;

		// Token: 0x0400124A RID: 4682
		private ToolboxItemSnapLineBehavior toolboxItemSnapLineBehavior;

		// Token: 0x0400124B RID: 4683
		private Graphics graphics;

		// Token: 0x0400124C RID: 4684
		private IToolboxService toolboxService;

		// Token: 0x0400124D RID: 4685
		private Point adornerWindowToScreenOffset;

		// Token: 0x0400124E RID: 4686
		private bool checkSnapLineSetting = true;

		// Token: 0x0400124F RID: 4687
		private bool defaultUseSnapLines;

		// Token: 0x04001250 RID: 4688
		private bool gridSnap = true;

		// Token: 0x04001251 RID: 4689
		private Size gridSize = Size.Empty;

		// Token: 0x04001252 RID: 4690
		private bool drawGrid = true;

		// Token: 0x04001253 RID: 4691
		private bool parentCanSetDrawGrid = true;

		// Token: 0x04001254 RID: 4692
		private bool parentCanSetGridSize = true;

		// Token: 0x04001255 RID: 4693
		private bool parentCanSetGridSnap = true;

		// Token: 0x04001256 RID: 4694
		private bool getDefaultDrawGrid = true;

		// Token: 0x04001257 RID: 4695
		private bool getDefaultGridSize = true;

		// Token: 0x04001258 RID: 4696
		private bool getDefaultGridSnap = true;

		// Token: 0x04001259 RID: 4697
		private StatusCommandUI statusCommandUI;

		// Token: 0x0400125A RID: 4698
		private int suspendChanging;

		// Token: 0x02000228 RID: 552
		private class EscapeHandler : IMenuStatusHandler
		{
			// Token: 0x060014F5 RID: 5365 RVA: 0x0006C753 File Offset: 0x0006B753
			public EscapeHandler(ParentControlDesigner designer)
			{
				this.designer = designer;
			}

			// Token: 0x060014F6 RID: 5366 RVA: 0x0006C762 File Offset: 0x0006B762
			public bool OverrideInvoke(MenuCommand cmd)
			{
				if (cmd.CommandID.Equals(MenuCommands.KeyCancel))
				{
					this.designer.OnMouseDragEnd(true);
					return true;
				}
				return false;
			}

			// Token: 0x060014F7 RID: 5367 RVA: 0x0006C785 File Offset: 0x0006B785
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

			// Token: 0x0400125B RID: 4699
			private ParentControlDesigner designer;
		}
	}
}
