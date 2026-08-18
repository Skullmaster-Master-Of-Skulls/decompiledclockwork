using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200032D RID: 813
	internal abstract class SelectionUIHandler
	{
		// Token: 0x06001FEA RID: 8170 RVA: 0x000C13BC File Offset: 0x000BF5BC
		public virtual bool BeginDrag(object[] components, SelectionRules rules, int initialX, int initialY)
		{
			this.dragOffset = default(Rectangle);
			this.originalCoords = null;
			this.rules = rules;
			this.dragControls = new Control[components.Length];
			for (int i = 0; i < components.Length; i++)
			{
				this.dragControls[i] = this.GetControl((IComponent)components[i]);
			}
			bool flag = false;
			IComponent component = this.GetComponent();
			for (int j = 0; j < components.Length; j++)
			{
				if (components[j] == component)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				Control control = this.GetControl();
				Size currentSnapSize = this.GetCurrentSnapSize();
				Rectangle rectangle = control.RectangleToScreen(control.ClientRectangle);
				rectangle.Inflate(currentSnapSize.Width, currentSnapSize.Height);
				ScrollableControl scrollableControl = this.GetControl() as ScrollableControl;
				if (scrollableControl != null && scrollableControl.AutoScroll)
				{
					Rectangle virtualScreen = SystemInformation.VirtualScreen;
					rectangle.Width = virtualScreen.Width;
					rectangle.Height = virtualScreen.Height;
				}
			}
			return true;
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x000C14AC File Offset: 0x000BF6AC
		private void CancelControlMove(Control[] controls, SelectionUIHandler.BoundsInfo[] bounds)
		{
			Rectangle bounds2 = default(Rectangle);
			for (int i = 0; i < controls.Length; i++)
			{
				Control parent = controls[i].Parent;
				if (parent != null)
				{
					parent.SuspendLayout();
				}
				bounds2.X = bounds[i].X;
				bounds2.Y = bounds[i].Y;
				bounds2.Width = bounds[i].Width;
				bounds2.Height = bounds[i].Height;
				controls[i].Bounds = bounds2;
			}
			for (int j = 0; j < controls.Length; j++)
			{
				Control parent2 = controls[j].Parent;
				if (parent2 != null)
				{
					parent2.ResumeLayout();
				}
			}
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x000C1549 File Offset: 0x000BF749
		public virtual void DragMoved(object[] components, Rectangle offset)
		{
			this.dragOffset = offset;
			this.MoveControls(components, false, false);
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x000C155C File Offset: 0x000BF75C
		public virtual void EndDrag(object[] components, bool cancel)
		{
			try
			{
				this.MoveControls(components, cancel, true);
			}
			catch (CheckoutException ex)
			{
				if (ex != CheckoutException.Canceled)
				{
					throw ex;
				}
				this.MoveControls(components, true, false);
			}
		}

		// Token: 0x06001FEE RID: 8174
		protected abstract IComponent GetComponent();

		// Token: 0x06001FEF RID: 8175
		protected abstract Control GetControl();

		// Token: 0x06001FF0 RID: 8176
		protected abstract Control GetControl(IComponent component);

		// Token: 0x06001FF1 RID: 8177
		protected abstract Size GetCurrentSnapSize();

		// Token: 0x06001FF2 RID: 8178
		protected abstract object GetService(Type serviceType);

		// Token: 0x06001FF3 RID: 8179
		protected abstract bool GetShouldSnapToGrid();

		// Token: 0x06001FF4 RID: 8180
		public abstract Rectangle GetUpdatedRect(Rectangle orignalRect, Rectangle dragRect, bool updateSize);

		// Token: 0x06001FF5 RID: 8181 RVA: 0x000C159C File Offset: 0x000BF79C
		private void MoveControls(object[] components, bool cancel, bool finalMove)
		{
			Control[] array = this.dragControls;
			Rectangle rectangle = this.dragOffset;
			SelectionUIHandler.BoundsInfo[] array2 = this.originalCoords;
			Point point = default(Point);
			if (finalMove)
			{
				Cursor.Clip = Rectangle.Empty;
				this.dragOffset = Rectangle.Empty;
				this.dragControls = null;
				this.originalCoords = null;
			}
			if (rectangle.IsEmpty)
			{
				return;
			}
			if (finalMove && rectangle.X == 0 && rectangle.Y == 0 && rectangle.Width == 0 && rectangle.Height == 0)
			{
				return;
			}
			if (cancel)
			{
				this.CancelControlMove(array, array2);
				return;
			}
			if (this.originalCoords == null && !finalMove)
			{
				this.originalCoords = new SelectionUIHandler.BoundsInfo[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.originalCoords[i] = new SelectionUIHandler.BoundsInfo(array[i]);
				}
				array2 = this.originalCoords;
			}
			for (int j = 0; j < array.Length; j++)
			{
				Control parent = array[j].Parent;
				if (parent != null)
				{
					parent.SuspendLayout();
				}
				SelectionUIHandler.BoundsInfo boundsInfo = array2[j];
				point.X = boundsInfo.lastRequestedX;
				point.Y = boundsInfo.lastRequestedY;
				if (!finalMove)
				{
					boundsInfo.lastRequestedX += rectangle.X;
					boundsInfo.lastRequestedY += rectangle.Y;
					boundsInfo.lastRequestedWidth += rectangle.Width;
					boundsInfo.lastRequestedHeight += rectangle.Height;
				}
				int x = boundsInfo.lastRequestedX;
				int y = boundsInfo.lastRequestedY;
				int num = boundsInfo.lastRequestedWidth;
				int num2 = boundsInfo.lastRequestedHeight;
				Rectangle bounds = array[j].Bounds;
				if ((this.rules & SelectionRules.Moveable) == SelectionRules.None)
				{
					Size currentSnapSize;
					if (this.GetShouldSnapToGrid())
					{
						currentSnapSize = this.GetCurrentSnapSize();
					}
					else
					{
						currentSnapSize = new Size(1, 1);
					}
					if (num < currentSnapSize.Width)
					{
						num = currentSnapSize.Width;
						x = bounds.X;
					}
					if (num2 < currentSnapSize.Height)
					{
						num2 = currentSnapSize.Height;
						y = bounds.Y;
					}
				}
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (array[j] == designerHost.RootComponent)
				{
					x = 0;
					y = 0;
				}
				Rectangle updatedRect = this.GetUpdatedRect(bounds, new Rectangle(x, y, num, num2), true);
				Rectangle rectangle2 = bounds;
				if ((this.rules & SelectionRules.Moveable) != SelectionRules.None)
				{
					rectangle2.X = updatedRect.X;
					rectangle2.Y = updatedRect.Y;
				}
				else
				{
					if ((this.rules & SelectionRules.TopSizeable) != SelectionRules.None)
					{
						rectangle2.Y = updatedRect.Y;
						rectangle2.Height = updatedRect.Height;
					}
					if ((this.rules & SelectionRules.BottomSizeable) != SelectionRules.None)
					{
						rectangle2.Height = updatedRect.Height;
					}
					if ((this.rules & SelectionRules.LeftSizeable) != SelectionRules.None)
					{
						rectangle2.X = updatedRect.X;
						rectangle2.Width = updatedRect.Width;
					}
					if ((this.rules & SelectionRules.RightSizeable) != SelectionRules.None)
					{
						rectangle2.Width = updatedRect.Width;
					}
				}
				bool flag = rectangle.X != 0 || rectangle.Y != 0;
				bool flag2 = rectangle.Width != 0 || rectangle.Height != 0;
				if (flag && flag2)
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(components[j])["Bounds"];
					if (propertyDescriptor != null && !propertyDescriptor.IsReadOnly)
					{
						if (finalMove)
						{
							object component = components[j];
							propertyDescriptor.SetValue(component, rectangle2);
						}
						else
						{
							array[j].Bounds = rectangle2;
						}
						flag2 = (flag = false);
					}
				}
				if (flag)
				{
					point.X = rectangle2.X;
					point.Y = rectangle2.Y;
					PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(components[j])["TrayLocation"];
					if (propertyDescriptor2 != null && !propertyDescriptor2.IsReadOnly)
					{
						propertyDescriptor2.SetValue(components[j], point);
					}
					else
					{
						PropertyDescriptor propertyDescriptor3 = TypeDescriptor.GetProperties(components[j])["Left"];
						PropertyDescriptor propertyDescriptor4 = TypeDescriptor.GetProperties(components[j])["Top"];
						if (propertyDescriptor4 != null && !propertyDescriptor4.IsReadOnly)
						{
							if (finalMove)
							{
								object component2 = components[j];
								propertyDescriptor4.SetValue(component2, point.Y);
							}
							else
							{
								array[j].Top = point.Y;
							}
						}
						if (propertyDescriptor3 != null && !propertyDescriptor3.IsReadOnly)
						{
							if (finalMove)
							{
								object component3 = components[j];
								propertyDescriptor3.SetValue(component3, point.X);
							}
							else
							{
								array[j].Left = point.X;
							}
						}
						if (propertyDescriptor3 == null || propertyDescriptor4 == null)
						{
							PropertyDescriptor propertyDescriptor5 = TypeDescriptor.GetProperties(components[j])["Location"];
							if (propertyDescriptor5 != null && !propertyDescriptor5.IsReadOnly)
							{
								propertyDescriptor5.SetValue(components[j], point);
							}
						}
					}
				}
				if (flag2)
				{
					Size size = new Size(Math.Max(3, rectangle2.Width), Math.Max(3, rectangle2.Height));
					PropertyDescriptor propertyDescriptor6 = TypeDescriptor.GetProperties(components[j])["Width"];
					PropertyDescriptor propertyDescriptor7 = TypeDescriptor.GetProperties(components[j])["Height"];
					if (propertyDescriptor6 != null && !propertyDescriptor6.IsReadOnly && size.Width != (int)propertyDescriptor6.GetValue(components[j]))
					{
						if (finalMove)
						{
							object component4 = components[j];
							propertyDescriptor6.SetValue(component4, size);
						}
						else
						{
							array[j].Width = size.Width;
						}
					}
					if (propertyDescriptor7 != null && !propertyDescriptor7.IsReadOnly && size.Height != (int)propertyDescriptor7.GetValue(components[j]))
					{
						if (finalMove)
						{
							object component5 = components[j];
							propertyDescriptor7.SetValue(component5, size);
						}
						else
						{
							array[j].Height = size.Height;
						}
					}
				}
			}
			for (int k = 0; k < array.Length; k++)
			{
				Control parent2 = array[k].Parent;
				if (parent2 != null)
				{
					parent2.ResumeLayout();
					parent2.Update();
				}
				array[k].Update();
			}
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x000C1B80 File Offset: 0x000BFD80
		public bool QueryBeginDrag(object[] components, SelectionRules rules, int initialX, int initialY)
		{
			IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				try
				{
					if (components != null && components.Length != 0)
					{
						foreach (object component in components)
						{
							componentChangeService.OnComponentChanging(component, TypeDescriptor.GetProperties(component)["Location"]);
							PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)["Size"];
							if (propertyDescriptor != null && propertyDescriptor.Attributes.Contains(DesignerSerializationVisibilityAttribute.Hidden))
							{
								propertyDescriptor = TypeDescriptor.GetProperties(component)["ClientSize"];
							}
							componentChangeService.OnComponentChanging(component, propertyDescriptor);
						}
					}
					else
					{
						componentChangeService.OnComponentChanging(this.GetComponent(), null);
					}
				}
				catch (CheckoutException ex)
				{
					if (ex == CheckoutException.Canceled)
					{
						return false;
					}
					throw ex;
				}
				catch (InvalidOperationException)
				{
					return false;
				}
			}
			return components != null && components.Length != 0;
		}

		// Token: 0x06001FF7 RID: 8183
		public abstract void SetCursor();

		// Token: 0x06001FF8 RID: 8184 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void OleDragEnter(DragEventArgs de)
		{
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void OleDragDrop(DragEventArgs de)
		{
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void OleDragOver(DragEventArgs de)
		{
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void OleDragLeave()
		{
		}

		// Token: 0x040018AA RID: 6314
		private Rectangle dragOffset = Rectangle.Empty;

		// Token: 0x040018AB RID: 6315
		private Control[] dragControls;

		// Token: 0x040018AC RID: 6316
		private SelectionUIHandler.BoundsInfo[] originalCoords;

		// Token: 0x040018AD RID: 6317
		private SelectionRules rules;

		// Token: 0x040018AE RID: 6318
		private const int MinControlWidth = 3;

		// Token: 0x040018AF RID: 6319
		private const int MinControlHeight = 3;

		// Token: 0x02000589 RID: 1417
		private class BoundsInfo
		{
			// Token: 0x060032A0 RID: 12960 RVA: 0x00111CB8 File Offset: 0x0010FEB8
			public BoundsInfo(Control control)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(control)["Size"];
				PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(control)["Location"];
				Size size;
				if (propertyDescriptor != null)
				{
					size = (Size)propertyDescriptor.GetValue(control);
				}
				else
				{
					size = control.Size;
				}
				Point point;
				if (propertyDescriptor2 != null)
				{
					point = (Point)propertyDescriptor2.GetValue(control);
				}
				else
				{
					point = control.Location;
				}
				this.X = point.X;
				this.Y = point.Y;
				this.Width = size.Width;
				this.Height = size.Height;
				this.lastRequestedX = this.X;
				this.lastRequestedY = this.Y;
				this.lastRequestedWidth = this.Width;
				this.lastRequestedHeight = this.Height;
			}

			// Token: 0x060032A1 RID: 12961 RVA: 0x00111DA0 File Offset: 0x0010FFA0
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"{X=",
					this.X.ToString(CultureInfo.CurrentCulture),
					", Y=",
					this.Y.ToString(CultureInfo.CurrentCulture),
					", Width=",
					this.Width.ToString(CultureInfo.CurrentCulture),
					", Height=",
					this.Height.ToString(CultureInfo.CurrentCulture),
					"}"
				});
			}

			// Token: 0x040021B4 RID: 8628
			public int X;

			// Token: 0x040021B5 RID: 8629
			public int Y;

			// Token: 0x040021B6 RID: 8630
			public int Width;

			// Token: 0x040021B7 RID: 8631
			public int Height;

			// Token: 0x040021B8 RID: 8632
			public int lastRequestedX = -1;

			// Token: 0x040021B9 RID: 8633
			public int lastRequestedY = -1;

			// Token: 0x040021BA RID: 8634
			public int lastRequestedWidth = -1;

			// Token: 0x040021BB RID: 8635
			public int lastRequestedHeight = -1;
		}
	}
}
