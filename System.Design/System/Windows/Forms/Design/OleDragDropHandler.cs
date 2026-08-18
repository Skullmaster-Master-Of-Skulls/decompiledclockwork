using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms.Design.Behavior;
using Microsoft.Internal.Performance;

namespace System.Windows.Forms.Design
{
	// Token: 0x020001BA RID: 442
	internal class OleDragDropHandler
	{
		// Token: 0x0600112E RID: 4398 RVA: 0x0005270C File Offset: 0x0005170C
		public OleDragDropHandler(SelectionUIHandler selectionHandler, IServiceProvider serviceProvider, IOleDragClient client)
		{
			this.serviceProvider = serviceProvider;
			this.selectionHandler = selectionHandler;
			this.client = client;
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x0005273F File Offset: 0x0005173F
		public static string DataFormat
		{
			get
			{
				return "CF_XMLCODE";
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x00052746 File Offset: 0x00051746
		public static string ExtraInfoFormat
		{
			get
			{
				return "CF_COMPONENTTYPES";
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x0005274D File Offset: 0x0005174D
		public static string NestedToolboxItemFormat
		{
			get
			{
				return "CF_NESTEDTOOLBOXITEM";
			}
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00052754 File Offset: 0x00051754
		private IComponent GetDragOwnerComponent(IDataObject data)
		{
			if (OleDragDropHandler.currentDrags == null || !OleDragDropHandler.currentDrags.Contains(data))
			{
				return null;
			}
			return OleDragDropHandler.currentDrags[data] as IComponent;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0005277C File Offset: 0x0005177C
		private static void AddCurrentDrag(IDataObject data, IComponent component)
		{
			if (OleDragDropHandler.currentDrags == null)
			{
				OleDragDropHandler.currentDrags = new Hashtable();
			}
			OleDragDropHandler.currentDrags[data] = component;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0005279B File Offset: 0x0005179B
		private static void RemoveCurrentDrag(IDataObject data)
		{
			OleDragDropHandler.currentDrags.Remove(data);
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x000527A8 File Offset: 0x000517A8
		internal IOleDragClient Destination
		{
			get
			{
				return this.client;
			}
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x000527B0 File Offset: 0x000517B0
		protected virtual bool CanDropDataObject(IDataObject dataObj)
		{
			if (dataObj != null)
			{
				if (!(dataObj is OleDragDropHandler.ComponentDataObjectWrapper))
				{
					try
					{
						object data = dataObj.GetData(OleDragDropHandler.DataFormat, false);
						if (data == null)
						{
							return false;
						}
						IDesignerSerializationService designerSerializationService = (IDesignerSerializationService)this.GetService(typeof(IDesignerSerializationService));
						if (designerSerializationService == null)
						{
							return false;
						}
						ICollection collection = designerSerializationService.Deserialize(data);
						if (collection.Count > 0)
						{
							bool flag = true;
							foreach (object obj in collection)
							{
								if (obj is IComponent)
								{
									flag = (flag && this.client.IsDropOk((IComponent)obj));
									if (!flag)
									{
										break;
									}
								}
							}
							return flag;
						}
					}
					catch (Exception ex)
					{
						if (ClientUtils.IsCriticalException(ex))
						{
							throw;
						}
					}
					return false;
				}
				object[] draggingObjects = this.GetDraggingObjects(dataObj, true);
				if (draggingObjects == null)
				{
					return false;
				}
				bool flag2 = true;
				int num = 0;
				while (flag2 && num < draggingObjects.Length)
				{
					flag2 = (flag2 && draggingObjects[num] is IComponent && this.client.IsDropOk((IComponent)draggingObjects[num]));
					num++;
				}
				return flag2;
			}
			return false;
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x000528F8 File Offset: 0x000518F8
		public bool Dragging
		{
			get
			{
				return this.localDrag;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x00052900 File Offset: 0x00051900
		public static bool FreezePainting
		{
			get
			{
				return OleDragDropHandler.freezePainting;
			}
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x00052908 File Offset: 0x00051908
		public IComponent[] CreateTool(ToolboxItem tool, Control parent, int x, int y, int width, int height, bool hasLocation, bool hasSize)
		{
			return this.CreateTool(tool, parent, x, y, width, height, hasLocation, hasSize, null);
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0005292C File Offset: 0x0005192C
		public IComponent[] CreateTool(ToolboxItem tool, Control parent, int x, int y, int width, int height, bool hasLocation, bool hasSize, ToolboxSnapDragDropEventArgs e)
		{
			IToolboxService toolboxService = (IToolboxService)this.GetService(typeof(IToolboxService));
			ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			IComponent[] array = new IComponent[0];
			Cursor value = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			DesignerTransaction designerTransaction = null;
			try
			{
				try
				{
					if (designerHost != null)
					{
						designerTransaction = designerHost.CreateTransaction(SR.GetString("DesignerBatchCreateTool", new object[]
						{
							tool.ToString()
						}));
					}
				}
				catch (CheckoutException ex)
				{
					if (ex == CheckoutException.Canceled)
					{
						return array;
					}
					throw ex;
				}
				try
				{
					try
					{
						if (designerHost != null && this.CurrentlyLocalizing(designerHost.RootComponent))
						{
							IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
							if (iuiservice != null)
							{
								iuiservice.ShowMessage(SR.GetString("LocalizingCannotAdd"));
							}
							array = new IComponent[0];
							return array;
						}
						Hashtable hashtable = new Hashtable();
						if (parent != null)
						{
							hashtable["Parent"] = parent;
						}
						if (parent != null && parent.IsMirrored)
						{
							x += width;
						}
						if (hasLocation)
						{
							hashtable["Location"] = new Point(x, y);
						}
						if (hasSize)
						{
							hashtable["Size"] = new Size(width, height);
						}
						if (e != null)
						{
							hashtable["ToolboxSnapDragDropEventArgs"] = e;
						}
						array = tool.CreateComponents(designerHost, hashtable);
					}
					catch (CheckoutException ex2)
					{
						if (ex2 != CheckoutException.Canceled)
						{
							throw;
						}
						array = new IComponent[0];
					}
					catch (ArgumentException ex3)
					{
						IUIService iuiservice2 = (IUIService)this.GetService(typeof(IUIService));
						if (iuiservice2 != null)
						{
							iuiservice2.ShowError(ex3);
						}
					}
					catch (Exception ex4)
					{
						IUIService iuiservice3 = (IUIService)this.GetService(typeof(IUIService));
						string text = string.Empty;
						if (ex4.InnerException != null)
						{
							text = ex4.InnerException.ToString();
						}
						if (string.IsNullOrEmpty(text))
						{
							text = ex4.ToString();
						}
						if (ex4 is InvalidOperationException)
						{
							text = ex4.Message;
						}
						if (iuiservice3 == null)
						{
							throw;
						}
						iuiservice3.ShowError(ex4, SR.GetString("FailedToCreateComponent", new object[]
						{
							tool.DisplayName,
							text
						}));
					}
					if (array == null)
					{
						array = new IComponent[0];
					}
				}
				finally
				{
					if (toolboxService != null && tool.Equals(toolboxService.GetSelectedToolboxItem(designerHost)))
					{
						toolboxService.SelectedToolboxItemUsed();
					}
				}
			}
			finally
			{
				if (designerTransaction != null)
				{
					designerTransaction.Commit();
				}
				Cursor.Current = value;
			}
			if (selectionService != null && array.Length > 0)
			{
				if (designerHost != null)
				{
					designerHost.Activate();
				}
				ArrayList arrayList = new ArrayList(array);
				for (int i = 0; i < array.Length; i++)
				{
					if (!TypeDescriptor.GetAttributes(array[i]).Contains(DesignTimeVisibleAttribute.Yes))
					{
						arrayList.Remove(array[i]);
					}
				}
				selectionService.SetSelectedComponents(arrayList.ToArray(), SelectionTypes.Replace);
			}
			OleDragDropHandler.codemarkers.CodeMarker(CodeMarkerEvent.perfFXDesignCreateComponentEnd);
			return array;
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00052CBC File Offset: 0x00051CBC
		private bool CurrentlyLocalizing(IComponent rootComponent)
		{
			if (rootComponent != null)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(rootComponent)["Language"];
				if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(CultureInfo))
				{
					CultureInfo cultureInfo = (CultureInfo)propertyDescriptor.GetValue(rootComponent);
					if (!cultureInfo.Equals(CultureInfo.InvariantCulture))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00052D10 File Offset: 0x00051D10
		private void DisableDragDropChildren(ICollection controls, ArrayList allowDropCache)
		{
			foreach (object obj in controls)
			{
				Control control = (Control)obj;
				if (control != null)
				{
					if (control.AllowDrop)
					{
						allowDropCache.Add(control);
						control.AllowDrop = false;
					}
					if (control.HasChildren)
					{
						this.DisableDragDropChildren(control.Controls, allowDropCache);
					}
				}
			}
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00052D8C File Offset: 0x00051D8C
		private Point DrawDragFrames(object[] comps, Point oldOffset, DragDropEffects oldEffect, Point newOffset, DragDropEffects newEffect, bool drawAtNewOffset)
		{
			Rectangle rectangle = Rectangle.Empty;
			Point empty = Point.Empty;
			Control designerControl = this.client.GetDesignerControl();
			if (this.selectionHandler == null)
			{
				return Point.Empty;
			}
			if (comps == null)
			{
				return Point.Empty;
			}
			for (int i = 0; i < comps.Length; i++)
			{
				Control controlForComponent = this.client.GetControlForComponent(comps[i]);
				Color backColor = SystemColors.Control;
				try
				{
					backColor = controlForComponent.BackColor;
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
					}
				}
				bool flag = true;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(comps[i])["Location"];
				if (propertyDescriptor != null)
				{
					flag = propertyDescriptor.IsReadOnly;
				}
				if (!oldOffset.IsEmpty && ((oldEffect & DragDropEffects.Move) == DragDropEffects.None || !flag))
				{
					rectangle = controlForComponent.Bounds;
					if (drawAtNewOffset)
					{
						rectangle.X = oldOffset.X;
						rectangle.Y = oldOffset.Y;
					}
					else
					{
						rectangle.Offset(oldOffset.X, oldOffset.Y);
					}
					rectangle = this.selectionHandler.GetUpdatedRect(controlForComponent.Bounds, rectangle, false);
					this.DrawReversibleFrame(designerControl.Handle, rectangle, backColor);
				}
				if (!newOffset.IsEmpty && ((oldEffect & DragDropEffects.Move) == DragDropEffects.None || !flag))
				{
					rectangle = controlForComponent.Bounds;
					if (drawAtNewOffset)
					{
						rectangle.X = newOffset.X;
						rectangle.Y = newOffset.Y;
					}
					else
					{
						rectangle.Offset(newOffset.X, newOffset.Y);
					}
					rectangle = this.selectionHandler.GetUpdatedRect(controlForComponent.Bounds, rectangle, false);
					this.DrawReversibleFrame(designerControl.Handle, rectangle, backColor);
				}
			}
			return newOffset;
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00052F28 File Offset: 0x00051F28
		private void DrawReversibleFrame(IntPtr handle, Rectangle rectangle, Color backColor)
		{
			if (rectangle.Width == 0)
			{
				rectangle.Width = 5;
			}
			if (rectangle.Height == 0)
			{
				rectangle.Height = 5;
			}
			int nDrawMode;
			Color c;
			if ((double)backColor.GetBrightness() < 0.5)
			{
				nDrawMode = 10;
				c = Color.White;
			}
			else
			{
				nDrawMode = 7;
				c = Color.Black;
			}
			IntPtr dc = UnsafeNativeMethods.GetDC(new HandleRef(null, handle));
			IntPtr intPtr = SafeNativeMethods.CreatePen(NativeMethods.PS_SOLID, 2, ColorTranslator.ToWin32(backColor));
			int nDrawMode2 = SafeNativeMethods.SetROP2(new HandleRef(null, dc), nDrawMode);
			IntPtr handle2 = SafeNativeMethods.SelectObject(new HandleRef(null, dc), new HandleRef(null, UnsafeNativeMethods.GetStockObject(5)));
			IntPtr handle3 = SafeNativeMethods.SelectObject(new HandleRef(null, dc), new HandleRef(null, intPtr));
			SafeNativeMethods.SetBkColor(new HandleRef(null, dc), ColorTranslator.ToWin32(c));
			SafeNativeMethods.Rectangle(new HandleRef(null, dc), rectangle.X, rectangle.Y, rectangle.Right, rectangle.Bottom);
			SafeNativeMethods.SetROP2(new HandleRef(null, dc), nDrawMode2);
			SafeNativeMethods.SelectObject(new HandleRef(null, dc), new HandleRef(null, handle2));
			SafeNativeMethods.SelectObject(new HandleRef(null, dc), new HandleRef(null, handle3));
			if (intPtr != IntPtr.Zero)
			{
				SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
			}
			UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0005307C File Offset: 0x0005207C
		public bool DoBeginDrag(object[] components, SelectionRules rules, int initialX, int initialY)
		{
			if ((rules & SelectionRules.AllSizeable) != SelectionRules.None || Control.MouseButtons == MouseButtons.None)
			{
				return true;
			}
			Control designerControl = this.client.GetDesignerControl();
			this.localDrag = true;
			this.localDragInside = false;
			this.dragComps = components;
			this.dragBase = new Point(initialX, initialY);
			this.localDragOffset = Point.Empty;
			designerControl.PointToClient(new Point(initialX, initialY));
			DragDropEffects dragDropEffects = DragDropEffects.Copy | DragDropEffects.Move;
			for (int i = 0; i < components.Length; i++)
			{
				InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(components[i])[typeof(InheritanceAttribute)];
				if (!inheritanceAttribute.Equals(InheritanceAttribute.NotInherited) && !inheritanceAttribute.Equals(InheritanceAttribute.InheritedReadOnly))
				{
					dragDropEffects &= ~DragDropEffects.Move;
					dragDropEffects |= (DragDropEffects)67108864;
				}
			}
			DataObject data = new OleDragDropHandler.ComponentDataObjectWrapper(new OleDragDropHandler.ComponentDataObject(this.client, this.serviceProvider, components, initialX, initialY));
			NativeMethods.MSG msg = default(NativeMethods.MSG);
			while (NativeMethods.PeekMessage(ref msg, IntPtr.Zero, 15, 15, 1))
			{
				NativeMethods.TranslateMessage(ref msg);
				NativeMethods.DispatchMessage(ref msg);
			}
			bool flag = OleDragDropHandler.freezePainting;
			OleDragDropHandler.AddCurrentDrag(data, this.client.Component);
			ArrayList arrayList = new ArrayList();
			foreach (object obj in components)
			{
				Control control = obj as Control;
				if (control != null && control.HasChildren)
				{
					this.DisableDragDropChildren(control.Controls, arrayList);
				}
			}
			DragDropEffects dragDropEffects2 = DragDropEffects.None;
			IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
			DesignerTransaction designerTransaction = null;
			if (designerHost != null)
			{
				designerTransaction = designerHost.CreateTransaction(SR.GetString("DragDropDragComponents", new object[]
				{
					components.Length
				}));
			}
			try
			{
				dragDropEffects2 = designerControl.DoDragDrop(data, dragDropEffects);
				if (designerTransaction != null)
				{
					designerTransaction.Commit();
				}
			}
			finally
			{
				OleDragDropHandler.RemoveCurrentDrag(data);
				foreach (object obj2 in arrayList)
				{
					Control control2 = (Control)obj2;
					control2.AllowDrop = true;
				}
				OleDragDropHandler.freezePainting = flag;
				if (designerTransaction != null)
				{
					((IDisposable)designerTransaction).Dispose();
				}
			}
			bool flag2 = (dragDropEffects2 & DragDropEffects.Move) != DragDropEffects.None || (dragDropEffects2 & (DragDropEffects)67108864) != DragDropEffects.None;
			bool flag3 = flag2 && this.localDragInside;
			ISelectionUIService selectionUIService = (ISelectionUIService)this.GetService(typeof(ISelectionUIService));
			if (selectionUIService != null && selectionUIService.Dragging)
			{
				selectionUIService.EndDrag(!flag3);
			}
			if (!this.localDragOffset.IsEmpty && dragDropEffects2 != DragDropEffects.None)
			{
				this.DrawDragFrames(this.dragComps, this.localDragOffset, this.localDragEffect, Point.Empty, DragDropEffects.None, false);
			}
			this.localDragOffset = Point.Empty;
			this.dragComps = null;
			this.localDrag = (this.localDragInside = false);
			this.dragBase = Point.Empty;
			return false;
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x00053374 File Offset: 0x00052374
		public void DoEndDrag(object[] components, bool cancel)
		{
			this.dragComps = null;
			this.localDrag = false;
			this.localDragInside = false;
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0005338C File Offset: 0x0005238C
		public void DoOleDragDrop(DragEventArgs de)
		{
			OleDragDropHandler.freezePainting = false;
			if (this.selectionHandler == null)
			{
				de.Effect = DragDropEffects.None;
				return;
			}
			if ((this.localDrag && de.X == this.dragBase.X && de.Y == this.dragBase.Y) || de.AllowedEffect == DragDropEffects.None || (!this.localDrag && !this.dragOk))
			{
				de.Effect = DragDropEffects.None;
				return;
			}
			bool flag = (de.AllowedEffect & (DragDropEffects)67108864) != DragDropEffects.None && this.localDragInside;
			bool flag2 = (de.AllowedEffect & DragDropEffects.Move) != DragDropEffects.None || flag;
			bool flag3 = (de.AllowedEffect & DragDropEffects.Copy) != DragDropEffects.None;
			if ((de.Effect & DragDropEffects.Move) != DragDropEffects.None && !flag2)
			{
				de.Effect = DragDropEffects.Copy;
			}
			if ((de.Effect & DragDropEffects.Copy) != DragDropEffects.None && !flag3)
			{
				de.Effect = DragDropEffects.None;
				return;
			}
			if (flag && (de.Effect & DragDropEffects.Move) != DragDropEffects.None)
			{
				de.Effect |= (DragDropEffects)67108866;
			}
			else if ((de.Effect & DragDropEffects.Copy) != DragDropEffects.None)
			{
				de.Effect = DragDropEffects.Copy;
			}
			if (this.forceDrawFrames || this.localDragInside)
			{
				this.localDragOffset = this.DrawDragFrames(this.dragComps, this.localDragOffset, this.localDragEffect, Point.Empty, DragDropEffects.None, this.forceDrawFrames);
				this.forceDrawFrames = false;
			}
			Cursor value = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				if (this.dragOk || (this.localDragInside && de.Effect == DragDropEffects.Copy))
				{
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					IContainer container = designerHost.RootComponent.Site.Container;
					IDataObject dataObject = de.Data;
					bool flag4 = false;
					object[] array;
					if (dataObject is OleDragDropHandler.ComponentDataObjectWrapper)
					{
						dataObject = ((OleDragDropHandler.ComponentDataObjectWrapper)dataObject).InnerData;
						OleDragDropHandler.ComponentDataObject componentDataObject = (OleDragDropHandler.ComponentDataObject)dataObject;
						IComponent dragOwnerComponent = this.GetDragOwnerComponent(de.Data);
						bool flag5 = dragOwnerComponent == null || this.client.Component == null || dragOwnerComponent.Site.Container != this.client.Component.Site.Container;
						bool flag6 = false;
						if (de.Effect == DragDropEffects.Copy || flag5)
						{
							componentDataObject.Deserialize(this.serviceProvider, (de.Effect & DragDropEffects.Copy) == DragDropEffects.None);
						}
						else
						{
							flag6 = true;
						}
						flag4 = true;
						array = componentDataObject.Components;
						if (flag6)
						{
							array = this.GetTopLevelComponents(array);
						}
					}
					else
					{
						object data = dataObject.GetData(OleDragDropHandler.DataFormat, true);
						if (data == null)
						{
							array = new IComponent[0];
						}
						else
						{
							dataObject = new OleDragDropHandler.ComponentDataObject(this.client, this.serviceProvider, data);
							array = ((OleDragDropHandler.ComponentDataObject)dataObject).Components;
							flag4 = true;
						}
					}
					if (array != null && array.Length > 0)
					{
						DesignerTransaction designerTransaction = null;
						try
						{
							designerTransaction = designerHost.CreateTransaction(SR.GetString("DragDropDropComponents"));
							if (!this.localDrag)
							{
								designerHost.Activate();
							}
							ArrayList arrayList = new ArrayList();
							for (int i = 0; i < array.Length; i++)
							{
								IComponent component = array[i] as IComponent;
								if (component != null)
								{
									try
									{
										string name = null;
										if (component.Site != null)
										{
											name = component.Site.Name;
										}
										Control control = null;
										if (flag4)
										{
											control = this.client.GetDesignerControl();
											NativeMethods.SendMessage(control.Handle, 11, 0, 0);
										}
										Point location = this.client.GetDesignerControl().PointToClient(new Point(de.X, de.Y));
										PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)["TrayLocation"];
										if (propertyDescriptor == null)
										{
											propertyDescriptor = TypeDescriptor.GetProperties(component)["Location"];
										}
										if (propertyDescriptor != null && !propertyDescriptor.IsReadOnly)
										{
											Rectangle dragRect = default(Rectangle);
											Point point = (Point)propertyDescriptor.GetValue(component);
											dragRect.X = location.X + point.X;
											dragRect.Y = location.Y + point.Y;
											dragRect = this.selectionHandler.GetUpdatedRect(Rectangle.Empty, dragRect, false);
										}
										if (!this.client.AddComponent(component, name, false))
										{
											de.Effect = DragDropEffects.None;
										}
										else if (this.client.GetControlForComponent(component) == null)
										{
											flag4 = false;
										}
										if (flag4)
										{
											ParentControlDesigner parentControlDesigner = this.client as ParentControlDesigner;
											if (parentControlDesigner != null)
											{
												Control controlForComponent = this.client.GetControlForComponent(component);
												location = parentControlDesigner.GetSnappedPoint(controlForComponent.Location);
												controlForComponent.Location = location;
											}
										}
										if (control != null)
										{
											NativeMethods.SendMessage(control.Handle, 11, 1, 0);
											control.Invalidate(true);
										}
										if (TypeDescriptor.GetAttributes(component).Contains(DesignTimeVisibleAttribute.Yes))
										{
											arrayList.Add(component);
										}
									}
									catch (CheckoutException ex)
									{
										if (ex == CheckoutException.Canceled)
										{
											break;
										}
										throw;
									}
								}
							}
							if (designerHost != null)
							{
								designerHost.Activate();
							}
							ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
							selectionService.SetSelectedComponents((object[])arrayList.ToArray(typeof(IComponent)), SelectionTypes.Replace);
							this.localDragInside = false;
						}
						finally
						{
							if (designerTransaction != null)
							{
								designerTransaction.Commit();
							}
						}
					}
				}
				if (this.localDragInside)
				{
					ISelectionUIService selectionUIService = (ISelectionUIService)this.GetService(typeof(ISelectionUIService));
					if (selectionUIService != null && selectionUIService.Dragging && flag2)
					{
						Rectangle offset = new Rectangle(de.X - this.dragBase.X, de.Y - this.dragBase.Y, 0, 0);
						selectionUIService.DragMoved(offset);
					}
				}
				this.dragOk = false;
			}
			finally
			{
				Cursor.Current = value;
			}
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0005395C File Offset: 0x0005295C
		public void DoOleDragEnter(DragEventArgs de)
		{
			if (this.localDrag || !this.CanDropDataObject(de.Data) || de.AllowedEffect == DragDropEffects.None)
			{
				if (this.localDrag && de.AllowedEffect != DragDropEffects.None)
				{
					this.localDragInside = true;
					if ((de.KeyState & 8) != 0 && (de.AllowedEffect & DragDropEffects.Copy) != DragDropEffects.None && this.client.CanModifyComponents)
					{
						de.Effect = DragDropEffects.Copy;
					}
					bool flag = (de.AllowedEffect & (DragDropEffects)67108864) != DragDropEffects.None && this.localDragInside;
					if (flag)
					{
						de.Effect |= (DragDropEffects)67108864;
					}
					if ((de.AllowedEffect & DragDropEffects.Move) != DragDropEffects.None)
					{
						de.Effect |= DragDropEffects.Move;
						return;
					}
				}
				else
				{
					de.Effect = DragDropEffects.None;
				}
				return;
			}
			if (!this.client.CanModifyComponents)
			{
				return;
			}
			this.dragOk = true;
			if ((de.KeyState & 8) != 0 && (de.AllowedEffect & DragDropEffects.Copy) != DragDropEffects.None)
			{
				de.Effect = DragDropEffects.Copy;
				return;
			}
			if ((de.AllowedEffect & DragDropEffects.Move) != DragDropEffects.None)
			{
				de.Effect = DragDropEffects.Move;
				return;
			}
			de.Effect = DragDropEffects.None;
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00053A60 File Offset: 0x00052A60
		public void DoOleDragLeave()
		{
			if (this.localDrag || this.forceDrawFrames)
			{
				this.localDragInside = false;
				this.localDragOffset = this.DrawDragFrames(this.dragComps, this.localDragOffset, this.localDragEffect, Point.Empty, DragDropEffects.None, this.forceDrawFrames);
				if (this.forceDrawFrames && this.dragOk)
				{
					this.dragBase = Point.Empty;
					this.dragComps = null;
				}
				this.forceDrawFrames = false;
			}
			this.dragOk = false;
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00053AE0 File Offset: 0x00052AE0
		public void DoOleDragOver(DragEventArgs de)
		{
			if (!this.localDrag && !this.dragOk)
			{
				de.Effect = DragDropEffects.None;
				return;
			}
			bool flag = (de.KeyState & 8) != 0 && (de.AllowedEffect & DragDropEffects.Copy) != DragDropEffects.None && this.client.CanModifyComponents;
			bool flag2 = (de.AllowedEffect & (DragDropEffects)67108864) != DragDropEffects.None && this.localDragInside;
			bool flag3 = (de.AllowedEffect & DragDropEffects.Move) != DragDropEffects.None || flag2;
			if ((flag || flag3) && (this.localDrag || this.forceDrawFrames))
			{
				Point point = Point.Empty;
				Point point2 = this.client.GetDesignerControl().PointToClient(new Point(de.X, de.Y));
				if (this.forceDrawFrames)
				{
					point = point2;
				}
				else
				{
					point = new Point(de.X - this.dragBase.X, de.Y - this.dragBase.Y);
				}
				if (!this.client.GetDesignerControl().ClientRectangle.Contains(point2))
				{
					flag = false;
					flag3 = false;
					point = this.localDragOffset;
				}
				if (point != this.localDragOffset)
				{
					this.DrawDragFrames(this.dragComps, this.localDragOffset, this.localDragEffect, point, de.Effect, this.forceDrawFrames);
					this.localDragOffset = point;
					this.localDragEffect = de.Effect;
				}
			}
			if (flag)
			{
				de.Effect = DragDropEffects.Copy;
			}
			else if (flag3)
			{
				de.Effect = DragDropEffects.Move;
			}
			else
			{
				de.Effect = DragDropEffects.None;
			}
			if (flag2)
			{
				de.Effect |= (DragDropEffects)67108864;
			}
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00053C70 File Offset: 0x00052C70
		public void DoOleGiveFeedback(GiveFeedbackEventArgs e)
		{
			SelectionUIHandler selectionUIHandler = this.selectionHandler;
			e.UseDefaultCursors = ((!this.localDragInside && !this.forceDrawFrames) || (e.Effect & DragDropEffects.Copy) != DragDropEffects.None || e.Effect == DragDropEffects.None);
			if (!e.UseDefaultCursors && this.selectionHandler != null)
			{
				this.selectionHandler.SetCursor();
			}
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00053CCC File Offset: 0x00052CCC
		private object[] GetDraggingObjects(IDataObject dataObj, bool topLevelOnly)
		{
			object[] array = null;
			if (dataObj is OleDragDropHandler.ComponentDataObjectWrapper)
			{
				dataObj = ((OleDragDropHandler.ComponentDataObjectWrapper)dataObj).InnerData;
				OleDragDropHandler.ComponentDataObject componentDataObject = (OleDragDropHandler.ComponentDataObject)dataObj;
				array = componentDataObject.Components;
			}
			if (!topLevelOnly || array == null)
			{
				return array;
			}
			return this.GetTopLevelComponents(array);
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00053D0D File Offset: 0x00052D0D
		public object[] GetDraggingObjects(IDataObject dataObj)
		{
			return this.GetDraggingObjects(dataObj, false);
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00053D17 File Offset: 0x00052D17
		public object[] GetDraggingObjects(DragEventArgs de)
		{
			return this.GetDraggingObjects(de.Data);
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00053D28 File Offset: 0x00052D28
		private object[] GetTopLevelComponents(ICollection comps)
		{
			if (!(comps is IList))
			{
				comps = new ArrayList(comps);
			}
			IList list = (IList)comps;
			ArrayList arrayList = new ArrayList();
			foreach (object obj in list)
			{
				Control control = obj as Control;
				if (control == null && obj != null)
				{
					arrayList.Add(obj);
				}
				else if (control != null && (control.Parent == null || !list.Contains(control.Parent)))
				{
					arrayList.Add(obj);
				}
			}
			return arrayList.ToArray();
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x00053DD4 File Offset: 0x00052DD4
		protected object GetService(Type t)
		{
			return this.serviceProvider.GetService(t);
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00053DE2 File Offset: 0x00052DE2
		protected virtual void OnInitializeComponent(IComponent comp, int x, int y, int width, int height, bool hasLocation, bool hasSize)
		{
		}

		// Token: 0x0400109F RID: 4255
		protected const int AllowLocalMoveOnly = 67108864;

		// Token: 0x040010A0 RID: 4256
		public const string CF_CODE = "CF_XMLCODE";

		// Token: 0x040010A1 RID: 4257
		public const string CF_COMPONENTTYPES = "CF_COMPONENTTYPES";

		// Token: 0x040010A2 RID: 4258
		public const string CF_TOOLBOXITEM = "CF_NESTEDTOOLBOXITEM";

		// Token: 0x040010A3 RID: 4259
		private SelectionUIHandler selectionHandler;

		// Token: 0x040010A4 RID: 4260
		private IServiceProvider serviceProvider;

		// Token: 0x040010A5 RID: 4261
		private IOleDragClient client;

		// Token: 0x040010A6 RID: 4262
		private bool dragOk;

		// Token: 0x040010A7 RID: 4263
		private bool forceDrawFrames;

		// Token: 0x040010A8 RID: 4264
		private bool localDrag;

		// Token: 0x040010A9 RID: 4265
		private bool localDragInside;

		// Token: 0x040010AA RID: 4266
		private Point localDragOffset = Point.Empty;

		// Token: 0x040010AB RID: 4267
		private DragDropEffects localDragEffect;

		// Token: 0x040010AC RID: 4268
		private object[] dragComps;

		// Token: 0x040010AD RID: 4269
		private Point dragBase = Point.Empty;

		// Token: 0x040010AE RID: 4270
		private static bool freezePainting = false;

		// Token: 0x040010AF RID: 4271
		private static Hashtable currentDrags;

		// Token: 0x040010B0 RID: 4272
		private static CodeMarkers codemarkers = CodeMarkers.Instance;

		// Token: 0x020001BB RID: 443
		protected class ComponentDataObjectWrapper : DataObject
		{
			// Token: 0x0600114D RID: 4429 RVA: 0x00053DF6 File Offset: 0x00052DF6
			public ComponentDataObjectWrapper(OleDragDropHandler.ComponentDataObject dataObject) : base(dataObject)
			{
				this.innerData = dataObject;
			}

			// Token: 0x170002CF RID: 719
			// (get) Token: 0x0600114E RID: 4430 RVA: 0x00053E06 File Offset: 0x00052E06
			public OleDragDropHandler.ComponentDataObject InnerData
			{
				get
				{
					return this.innerData;
				}
			}

			// Token: 0x040010B1 RID: 4273
			private OleDragDropHandler.ComponentDataObject innerData;
		}

		// Token: 0x020001BC RID: 444
		protected class ComponentDataObject : IDataObject
		{
			// Token: 0x0600114F RID: 4431 RVA: 0x00053E0E File Offset: 0x00052E0E
			public ComponentDataObject(IOleDragClient dragClient, IServiceProvider sp, object[] comps, int x, int y)
			{
				this.serviceProvider = sp;
				this.components = this.GetComponentList(comps, null, -1);
				this.initialX = x;
				this.initialY = y;
				this.dragClient = dragClient;
			}

			// Token: 0x06001150 RID: 4432 RVA: 0x00053E43 File Offset: 0x00052E43
			public ComponentDataObject(IOleDragClient dragClient, IServiceProvider sp, object serializationData)
			{
				this.serviceProvider = sp;
				this.serializationData = serializationData;
				this.dragClient = dragClient;
			}

			// Token: 0x170002D0 RID: 720
			// (get) Token: 0x06001151 RID: 4433 RVA: 0x00053E60 File Offset: 0x00052E60
			private Stream SerializationStream
			{
				get
				{
					if (this.serializationStream == null && this.Components != null)
					{
						IDesignerSerializationService designerSerializationService = (IDesignerSerializationService)this.serviceProvider.GetService(typeof(IDesignerSerializationService));
						if (designerSerializationService != null)
						{
							object[] array = new object[this.components.Length];
							for (int i = 0; i < this.components.Length; i++)
							{
								array[i] = (IComponent)this.components[i];
							}
							object graph = designerSerializationService.Serialize(array);
							this.serializationStream = new MemoryStream();
							BinaryFormatter binaryFormatter = new BinaryFormatter();
							binaryFormatter.Serialize(this.serializationStream, graph);
							this.serializationStream.Seek(0L, SeekOrigin.Begin);
						}
					}
					return this.serializationStream;
				}
			}

			// Token: 0x170002D1 RID: 721
			// (get) Token: 0x06001152 RID: 4434 RVA: 0x00053F10 File Offset: 0x00052F10
			public object[] Components
			{
				get
				{
					if (this.components == null && (this.serializationStream != null || this.serializationData != null))
					{
						this.Deserialize(null, false);
						if (this.components == null)
						{
							return new object[0];
						}
					}
					return (object[])this.components.Clone();
				}
			}

			// Token: 0x170002D2 RID: 722
			// (get) Token: 0x06001153 RID: 4435 RVA: 0x00053F5C File Offset: 0x00052F5C
			private OleDragDropHandler.CfCodeToolboxItem NestedToolboxItem
			{
				get
				{
					if (this.toolboxitemdata == null)
					{
						this.toolboxitemdata = new OleDragDropHandler.CfCodeToolboxItem(this.GetData(OleDragDropHandler.DataFormat));
					}
					return this.toolboxitemdata;
				}
			}

			// Token: 0x06001154 RID: 4436 RVA: 0x00053F84 File Offset: 0x00052F84
			private object[] GetComponentList(object[] components, ArrayList list, int index)
			{
				if (this.serviceProvider == null)
				{
					return components;
				}
				ISelectionService selectionService = (ISelectionService)this.serviceProvider.GetService(typeof(ISelectionService));
				if (selectionService == null)
				{
					return components;
				}
				ICollection collection;
				if (components == null)
				{
					collection = selectionService.GetSelectedComponents();
				}
				else
				{
					collection = new ArrayList(components);
				}
				IDesignerHost designerHost = (IDesignerHost)this.serviceProvider.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					ArrayList arrayList = new ArrayList();
					foreach (object obj in collection)
					{
						IComponent component = (IComponent)obj;
						arrayList.Add(component);
						this.GetAssociatedComponents(component, designerHost, arrayList);
					}
					collection = arrayList;
				}
				object[] array = new object[collection.Count];
				collection.CopyTo(array, 0);
				return array;
			}

			// Token: 0x06001155 RID: 4437 RVA: 0x00054068 File Offset: 0x00053068
			private void GetAssociatedComponents(IComponent component, IDesignerHost host, ArrayList list)
			{
				ComponentDesigner componentDesigner = host.GetDesigner(component) as ComponentDesigner;
				if (componentDesigner == null)
				{
					return;
				}
				foreach (object obj in componentDesigner.AssociatedComponents)
				{
					IComponent component2 = (IComponent)obj;
					list.Add(component2);
					this.GetAssociatedComponents(component2, host, list);
				}
			}

			// Token: 0x06001156 RID: 4438 RVA: 0x000540DC File Offset: 0x000530DC
			public virtual object GetData(string format)
			{
				return this.GetData(format, false);
			}

			// Token: 0x06001157 RID: 4439 RVA: 0x000540E8 File Offset: 0x000530E8
			public virtual object GetData(string format, bool autoConvert)
			{
				if (format.Equals(OleDragDropHandler.DataFormat))
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					this.SerializationStream.Seek(0L, SeekOrigin.Begin);
					return binaryFormatter.Deserialize(this.SerializationStream);
				}
				if (format.Equals(OleDragDropHandler.NestedToolboxItemFormat))
				{
					this.NestedToolboxItem.SetDisplayName();
					return this.NestedToolboxItem;
				}
				return null;
			}

			// Token: 0x06001158 RID: 4440 RVA: 0x00054144 File Offset: 0x00053144
			public virtual object GetData(Type t)
			{
				return this.GetData(t.FullName);
			}

			// Token: 0x06001159 RID: 4441 RVA: 0x00054152 File Offset: 0x00053152
			public bool GetDataPresent(string format, bool autoConvert)
			{
				return Array.IndexOf<string>(this.GetFormats(), format) != -1;
			}

			// Token: 0x0600115A RID: 4442 RVA: 0x00054166 File Offset: 0x00053166
			public bool GetDataPresent(string format)
			{
				return this.GetDataPresent(format, false);
			}

			// Token: 0x0600115B RID: 4443 RVA: 0x00054170 File Offset: 0x00053170
			public bool GetDataPresent(Type format)
			{
				return this.GetDataPresent(format.FullName, false);
			}

			// Token: 0x0600115C RID: 4444 RVA: 0x0005417F File Offset: 0x0005317F
			public string[] GetFormats(bool autoConvert)
			{
				return this.GetFormats();
			}

			// Token: 0x0600115D RID: 4445 RVA: 0x00054188 File Offset: 0x00053188
			public string[] GetFormats()
			{
				return new string[]
				{
					OleDragDropHandler.NestedToolboxItemFormat,
					OleDragDropHandler.DataFormat,
					DataFormats.Serializable,
					OleDragDropHandler.ExtraInfoFormat
				};
			}

			// Token: 0x0600115E RID: 4446 RVA: 0x000541C0 File Offset: 0x000531C0
			public void Deserialize(IServiceProvider serviceProvider, bool removeCurrentComponents)
			{
				if (serviceProvider == null)
				{
					serviceProvider = this.serviceProvider;
				}
				IDesignerSerializationService designerSerializationService = (IDesignerSerializationService)serviceProvider.GetService(typeof(IDesignerSerializationService));
				IDesignerHost designerHost = null;
				DesignerTransaction designerTransaction = null;
				try
				{
					if (this.serializationData == null)
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						this.serializationData = binaryFormatter.Deserialize(this.SerializationStream);
					}
					if (removeCurrentComponents && this.components != null)
					{
						foreach (IComponent component in this.components)
						{
							if (designerHost == null && component.Site != null)
							{
								designerHost = (IDesignerHost)component.Site.GetService(typeof(IDesignerHost));
								if (designerHost != null)
								{
									designerTransaction = designerHost.CreateTransaction(SR.GetString("DragDropMoveComponents", new object[]
									{
										this.components.Length
									}));
								}
							}
							if (designerHost != null)
							{
								designerHost.DestroyComponent(component);
							}
						}
						this.components = null;
					}
					ICollection collection = designerSerializationService.Deserialize(this.serializationData);
					this.components = new IComponent[collection.Count];
					IEnumerator enumerator = collection.GetEnumerator();
					int num = 0;
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						this.components[num++] = (IComponent)obj;
					}
					ArrayList arrayList = new ArrayList();
					for (int j = 0; j < this.components.Length; j++)
					{
						if (this.components[j] is Control)
						{
							Control control = (Control)this.components[j];
							if (control.Parent == null)
							{
								arrayList.Add(this.components[j]);
							}
						}
						else
						{
							arrayList.Add(this.components[j]);
						}
					}
					this.components = arrayList.ToArray();
				}
				finally
				{
					if (designerTransaction != null)
					{
						designerTransaction.Commit();
					}
				}
			}

			// Token: 0x0600115F RID: 4447 RVA: 0x000543A4 File Offset: 0x000533A4
			public void SetData(string format, bool autoConvert, object data)
			{
				this.SetData(format, data);
			}

			// Token: 0x06001160 RID: 4448 RVA: 0x000543AE File Offset: 0x000533AE
			public void SetData(string format, object data)
			{
				throw new Exception(SR.GetString("DragDropSetDataError"));
			}

			// Token: 0x06001161 RID: 4449 RVA: 0x000543BF File Offset: 0x000533BF
			public void SetData(Type format, object data)
			{
				this.SetData(format.FullName, data);
			}

			// Token: 0x06001162 RID: 4450 RVA: 0x000543CE File Offset: 0x000533CE
			public void SetData(object data)
			{
				this.SetData(data.GetType(), data);
			}

			// Token: 0x040010B2 RID: 4274
			private IServiceProvider serviceProvider;

			// Token: 0x040010B3 RID: 4275
			private object[] components;

			// Token: 0x040010B4 RID: 4276
			private Stream serializationStream;

			// Token: 0x040010B5 RID: 4277
			private object serializationData;

			// Token: 0x040010B6 RID: 4278
			private int initialX;

			// Token: 0x040010B7 RID: 4279
			private int initialY;

			// Token: 0x040010B8 RID: 4280
			private IOleDragClient dragClient;

			// Token: 0x040010B9 RID: 4281
			private OleDragDropHandler.CfCodeToolboxItem toolboxitemdata;
		}

		// Token: 0x020001BD RID: 445
		[Serializable]
		internal class CfCodeToolboxItem : ToolboxItem
		{
			// Token: 0x06001163 RID: 4451 RVA: 0x000543DD File Offset: 0x000533DD
			public CfCodeToolboxItem(object serializationData)
			{
				this.serializationData = serializationData;
			}

			// Token: 0x06001164 RID: 4452 RVA: 0x000543EC File Offset: 0x000533EC
			private CfCodeToolboxItem(SerializationInfo info, StreamingContext context)
			{
				this.Deserialize(info, context);
			}

			// Token: 0x06001165 RID: 4453 RVA: 0x000543FC File Offset: 0x000533FC
			public void SetDisplayName()
			{
				if (!this.displaynameset)
				{
					this.displaynameset = true;
					string str = "Template";
					int num = ++OleDragDropHandler.CfCodeToolboxItem.template;
					base.DisplayName = str + num.ToString(CultureInfo.CurrentCulture);
				}
			}

			// Token: 0x06001166 RID: 4454 RVA: 0x00054442 File Offset: 0x00053442
			protected override void Serialize(SerializationInfo info, StreamingContext context)
			{
				base.Serialize(info, context);
				if (this.serializationData != null)
				{
					info.AddValue("CfCodeToolboxItem.serializationData", this.serializationData);
				}
			}

			// Token: 0x06001167 RID: 4455 RVA: 0x00054468 File Offset: 0x00053468
			protected override void Deserialize(SerializationInfo info, StreamingContext context)
			{
				base.Deserialize(info, context);
				foreach (SerializationEntry serializationEntry in info)
				{
					if (serializationEntry.Name == "CfCodeToolboxItem.serializationData")
					{
						this.serializationData = serializationEntry.Value;
						return;
					}
				}
			}

			// Token: 0x06001168 RID: 4456 RVA: 0x000544B8 File Offset: 0x000534B8
			protected override IComponent[] CreateComponentsCore(IDesignerHost host, IDictionary defaultValues)
			{
				IDesignerSerializationService designerSerializationService = (IDesignerSerializationService)host.GetService(typeof(IDesignerSerializationService));
				if (designerSerializationService == null)
				{
					return null;
				}
				ICollection collection = designerSerializationService.Deserialize(this.serializationData);
				ArrayList arrayList = new ArrayList();
				foreach (object obj in collection)
				{
					if (obj != null && obj is IComponent)
					{
						arrayList.Add(obj);
					}
				}
				IComponent[] array = new IComponent[arrayList.Count];
				arrayList.CopyTo(array, 0);
				ArrayList arrayList2 = null;
				if (defaultValues == null)
				{
					defaultValues = new Hashtable();
				}
				Control control = defaultValues["Parent"] as Control;
				if (control != null)
				{
					ParentControlDesigner parentControlDesigner = host.GetDesigner(control) as ParentControlDesigner;
					if (parentControlDesigner != null)
					{
						Rectangle a = Rectangle.Empty;
						foreach (IComponent component in array)
						{
							Control control2 = component as Control;
							if (control2 != null && control2 != control && control2.Parent == null)
							{
								if (a.IsEmpty)
								{
									a = control2.Bounds;
								}
								else
								{
									a = Rectangle.Union(a, control2.Bounds);
								}
							}
						}
						defaultValues.Remove("Size");
						foreach (IComponent component2 in array)
						{
							Control control3 = component2 as Control;
							Form form = control3 as Form;
							if (control3 != null && (form == null || !form.TopLevel) && control3.Parent == null)
							{
								defaultValues["Offset"] = new Size(control3.Bounds.X - a.X, control3.Bounds.Y - a.Y);
								parentControlDesigner.AddControl(control3, defaultValues);
							}
						}
					}
				}
				ComponentTray componentTray = (ComponentTray)host.GetService(typeof(ComponentTray));
				if (componentTray != null)
				{
					foreach (IComponent comp in array)
					{
						ComponentTray.TrayControl trayControlFromComponent = componentTray.GetTrayControlFromComponent(comp);
						if (trayControlFromComponent != null)
						{
							if (arrayList2 == null)
							{
								arrayList2 = new ArrayList();
							}
							arrayList2.Add(trayControlFromComponent);
						}
					}
					if (arrayList2 != null)
					{
						componentTray.UpdatePastePositions(arrayList2);
					}
				}
				return array;
			}

			// Token: 0x06001169 RID: 4457 RVA: 0x00054718 File Offset: 0x00053718
			protected override IComponent[] CreateComponentsCore(IDesignerHost host)
			{
				return this.CreateComponentsCore(host, null);
			}

			// Token: 0x040010BA RID: 4282
			private object serializationData;

			// Token: 0x040010BB RID: 4283
			private static int template;

			// Token: 0x040010BC RID: 4284
			private bool displaynameset;
		}
	}
}
