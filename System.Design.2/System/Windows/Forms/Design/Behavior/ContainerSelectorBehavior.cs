using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x02000378 RID: 888
	internal sealed class ContainerSelectorBehavior : Behavior
	{
		// Token: 0x06002484 RID: 9348 RVA: 0x000E1EEB File Offset: 0x000E00EB
		internal ContainerSelectorBehavior(Control containerControl, IServiceProvider serviceProvider)
		{
			this.Init(containerControl, serviceProvider);
			this.setInitialDragPoint = false;
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x000E1F02 File Offset: 0x000E0102
		internal ContainerSelectorBehavior(Control containerControl, IServiceProvider serviceProvider, bool setInitialDragPoint)
		{
			this.Init(containerControl, serviceProvider);
			this.setInitialDragPoint = setInitialDragPoint;
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x000E1F1C File Offset: 0x000E011C
		private void Init(Control containerControl, IServiceProvider serviceProvider)
		{
			this.behaviorService = (BehaviorService)serviceProvider.GetService(typeof(BehaviorService));
			if (this.behaviorService == null)
			{
				return;
			}
			this.containerControl = containerControl;
			this.serviceProvider = serviceProvider;
			this.initialDragPoint = Point.Empty;
			this.okToMove = false;
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06002487 RID: 9351 RVA: 0x000E1F6D File Offset: 0x000E016D
		public Control ContainerControl
		{
			get
			{
				return this.containerControl;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06002488 RID: 9352 RVA: 0x000E1F75 File Offset: 0x000E0175
		// (set) Token: 0x06002489 RID: 9353 RVA: 0x000E1F7D File Offset: 0x000E017D
		public bool OkToMove
		{
			get
			{
				return this.okToMove;
			}
			set
			{
				this.okToMove = value;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x0600248A RID: 9354 RVA: 0x000E1F86 File Offset: 0x000E0186
		// (set) Token: 0x0600248B RID: 9355 RVA: 0x000E1F8E File Offset: 0x000E018E
		public Point InitialDragPoint
		{
			get
			{
				return this.initialDragPoint;
			}
			set
			{
				this.initialDragPoint = value;
			}
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x000E1F98 File Offset: 0x000E0198
		public override bool OnMouseDown(Glyph g, MouseButtons button, Point mouseLoc)
		{
			if (button == MouseButtons.Left)
			{
				ISelectionService selectionService = (ISelectionService)this.serviceProvider.GetService(typeof(ISelectionService));
				if (selectionService != null && !this.containerControl.Equals(selectionService.PrimarySelection as Control))
				{
					selectionService.SetSelectedComponents(new object[]
					{
						this.containerControl
					}, SelectionTypes.Click | SelectionTypes.Toggle);
					ContainerSelectorGlyph containerSelectorGlyph = g as ContainerSelectorGlyph;
					if (containerSelectorGlyph == null)
					{
						return false;
					}
					using (BehaviorServiceAdornerCollectionEnumerator enumerator = this.behaviorService.Adorners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Adorner adorner = enumerator.Current;
							foreach (object obj in adorner.Glyphs)
							{
								Glyph glyph = (Glyph)obj;
								ContainerSelectorGlyph containerSelectorGlyph2 = glyph as ContainerSelectorGlyph;
								if (containerSelectorGlyph2 != null && !containerSelectorGlyph2.Equals(containerSelectorGlyph))
								{
									ContainerSelectorBehavior containerSelectorBehavior = containerSelectorGlyph2.RelatedBehavior as ContainerSelectorBehavior;
									ContainerSelectorBehavior containerSelectorBehavior2 = containerSelectorGlyph.RelatedBehavior as ContainerSelectorBehavior;
									if (containerSelectorBehavior != null && containerSelectorBehavior2 != null && containerSelectorBehavior2.ContainerControl.Equals(containerSelectorBehavior.ContainerControl))
									{
										containerSelectorBehavior.OkToMove = true;
										containerSelectorBehavior.InitialDragPoint = this.DetermineInitialDragPoint(mouseLoc);
										break;
									}
								}
							}
						}
						return false;
					}
				}
				this.InitialDragPoint = this.DetermineInitialDragPoint(mouseLoc);
				this.OkToMove = true;
			}
			return false;
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x000E2124 File Offset: 0x000E0324
		private Point DetermineInitialDragPoint(Point mouseLoc)
		{
			if (this.setInitialDragPoint)
			{
				Point point = this.behaviorService.ControlToAdornerWindow(this.containerControl);
				point = this.behaviorService.AdornerWindowPointToScreen(point);
				Cursor.Position = point;
				return point;
			}
			return mouseLoc;
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x000E2164 File Offset: 0x000E0364
		public override bool OnMouseMove(Glyph g, MouseButtons button, Point mouseLoc)
		{
			if (button == MouseButtons.Left && this.OkToMove)
			{
				if (this.InitialDragPoint == Point.Empty)
				{
					this.InitialDragPoint = this.DetermineInitialDragPoint(mouseLoc);
				}
				Size size = new Size(Math.Abs(mouseLoc.X - this.InitialDragPoint.X), Math.Abs(mouseLoc.Y - this.InitialDragPoint.Y));
				if (size.Width >= DesignerUtils.MinDragSize.Width / 2 || size.Height >= DesignerUtils.MinDragSize.Height / 2)
				{
					Point initialMouseLocation = this.behaviorService.AdornerWindowToScreen();
					initialMouseLocation.Offset(mouseLoc.X, mouseLoc.Y);
					this.StartDragOperation(initialMouseLocation);
				}
			}
			return false;
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x000E223C File Offset: 0x000E043C
		public override bool OnMouseUp(Glyph g, MouseButtons button)
		{
			this.InitialDragPoint = Point.Empty;
			this.OkToMove = false;
			return false;
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x000E2254 File Offset: 0x000E0454
		private void StartDragOperation(Point initialMouseLocation)
		{
			ISelectionService selectionService = (ISelectionService)this.serviceProvider.GetService(typeof(ISelectionService));
			IDesignerHost designerHost = (IDesignerHost)this.serviceProvider.GetService(typeof(IDesignerHost));
			if (selectionService == null || designerHost == null)
			{
				return;
			}
			Control parent = this.containerControl.Parent;
			ArrayList arrayList = new ArrayList();
			ICollection selectedComponents = selectionService.GetSelectedComponents();
			foreach (object obj in selectedComponents)
			{
				IComponent component = (IComponent)obj;
				Control control = component as Control;
				if (control != null && control.Parent != null && control.Parent.Equals(parent))
				{
					ControlDesigner controlDesigner = designerHost.GetDesigner(control) as ControlDesigner;
					if (controlDesigner != null && (controlDesigner.SelectionRules & SelectionRules.Moveable) != SelectionRules.None)
					{
						arrayList.Add(control);
					}
				}
			}
			if (arrayList.Count > 0)
			{
				Point point;
				if (this.setInitialDragPoint)
				{
					point = this.behaviorService.ControlToAdornerWindow(this.containerControl);
					point = this.behaviorService.AdornerWindowPointToScreen(point);
				}
				else
				{
					point = initialMouseLocation;
				}
				DropSourceBehavior dropSourceBehavior = new DropSourceBehavior(arrayList, this.containerControl.Parent, point);
				try
				{
					this.behaviorService.DoDragDrop(dropSourceBehavior);
				}
				finally
				{
					this.OkToMove = false;
					this.InitialDragPoint = Point.Empty;
				}
			}
		}

		// Token: 0x04001A70 RID: 6768
		private Control containerControl;

		// Token: 0x04001A71 RID: 6769
		private IServiceProvider serviceProvider;

		// Token: 0x04001A72 RID: 6770
		private BehaviorService behaviorService;

		// Token: 0x04001A73 RID: 6771
		private bool okToMove;

		// Token: 0x04001A74 RID: 6772
		private Point initialDragPoint;

		// Token: 0x04001A75 RID: 6773
		private bool setInitialDragPoint;
	}
}
