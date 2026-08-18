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
	// Token: 0x0200031B RID: 795
	internal class OleDragDropHandler
	{
		// Token: 0x06001F43 RID: 8003 RVA: 0x000BBE0A File Offset: 0x000BA00A
		public OleDragDropHandler(SelectionUIHandler selectionHandler, IServiceProvider serviceProvider, IOleDragClient client)
		{
			this.serviceProvider = serviceProvider;
			this.selectionHandler = selectionHandler;
			this.client = client;
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001F44 RID: 8004 RVA: 0x000BBE3D File Offset: 0x000BA03D
		public static string DataFormat
		{
			get
			{
				return "CF_XMLCODE";
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001F45 RID: 8005 RVA: 0x000BBE44 File Offset: 0x000BA044
		public static string ExtraInfoFormat
		{
			get
			{
				return "CF_COMPONENTTYPES";
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001F46 RID: 8006 RVA: 0x000BBE4B File Offset: 0x000BA04B
		public static string NestedToolboxItemFormat
		{
			get
			{
				return "CF_NESTEDTOOLBOXITEM";
			}
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x000BBE52 File Offset: 0x000BA052
		private IComponent GetDragOwnerComponent(IDataObject data)
		{
			if (OleDragDropHandler.currentDrags == null || !OleDragDropHandler.currentDrags.Contains(data))
			{
				return null;
			}
			return OleDragDropHandler.currentDrags[data] as IComponent;
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x000BBE7A File Offset: 0x000BA07A
		private static void AddCurrentDrag(IDataObject data, IComponent component)
		{
			if (OleDragDropHandler.currentDrags == null)
			{
				OleDragDropHandler.currentDrags = new Hashtable();
			}
			OleDragDropHandler.currentDrags[data] = component;
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x000BBE99 File Offset: 0x000BA099
		private static void RemoveCurrentDrag(IDataObject data)
		{
			OleDragDropHandler.currentDrags.Remove(data);
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001F4A RID: 8010 RVA: 0x000BBEA6 File Offset: 0x000BA0A6
		internal IOleDragClient Destination
		{
			get
			{
				return this.client;
			}
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x000BBEB0 File Offset: 0x000BA0B0
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

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x000BBFF8 File Offset: 0x000BA1F8
		public bool Dragging
		{
			get
			{
				return this.localDrag;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x000BC000 File Offset: 0x000BA200
		public static bool FreezePainting
		{
			get
			{
				return OleDragDropHandler.freezePainting;
			}
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x000BC008 File Offset: 0x000BA208
		public IComponent[] CreateTool(ToolboxItem tool, Control parent, int x, int y, int width, int height, bool hasLocation, bool hasSize)
		{
			return this.CreateTool(tool, parent, x, y, width, height, hasLocation, hasSize, null);
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x000BC02C File Offset: 0x000BA22C
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
			if (selectionService != null && array.Length != 0)
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
			OleDragDropHandler.codemarkers.CodeMarker(7501);
			return array;
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x000BC3B0 File Offset: 0x000BA5B0
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

		// Token: 0x06001F51 RID: 8017 RVA: 0x000BC408 File Offset: 0x000BA608
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

		// Token: 0x06001F52 RID: 8018 RVA: 0x000BC484 File Offset: 0x000BA684
		private Point DrawDragFrames(object[] comps, Point oldOffset, DragDropEffects oldEffect, Point newOffset, DragDropEffects newEffect, bool drawAtNewOffset)
		{
			Rectangle rectangle = Rectangle.Empty;
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

		// Token: 0x06001F53 RID: 8019 RVA: 0x000BC61C File Offset: 0x000BA81C
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

		// Token: 0x06001F54 RID: 8020 RVA: 0x000BC770 File Offset: 0x000BA970
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
			bool flag2 = (dragDropEffects2 & DragDropEffects.Move) != DragDropEffects.None || (dragDropEffects2 & (DragDropEffects)67108864) > DragDropEffects.None;
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

		// Token: 0x06001F55 RID: 8021 RVA: 0x000BCA64 File Offset: 0x000BAC64
		public void DoEndDrag(object[] components, bool cancel)
		{
			this.dragComps = null;
			this.localDrag = false;
			this.localDragInside = false;
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x000BCA7C File Offset: 0x000BAC7C
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
			bool flag2 = (de.AllowedEffect & DragDropEffects.Move) > DragDropEffects.None || flag;
			bool flag3 = (de.AllowedEffect & DragDropEffects.Copy) > DragDropEffects.None;
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
							object[] array2 = new IComponent[0];
							array = array2;
						}
						else
						{
							dataObject = new OleDragDropHandler.ComponentDataObject(this.client, this.serviceProvider, data);
							array = ((OleDragDropHandler.ComponentDataObject)dataObject).Components;
							flag4 = true;
						}
					}
					if (array != null && array.Length != 0)
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
					if (selectionUIService != null && (selectionUIService.Dragging && flag2))
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

		// Token: 0x06001F57 RID: 8023 RVA: 0x000BD04C File Offset: 0x000BB24C
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

		// Token: 0x06001F58 RID: 8024 RVA: 0x000BD150 File Offset: 0x000BB350
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

		// Token: 0x06001F59 RID: 8025 RVA: 0x000BD1D0 File Offset: 0x000BB3D0
		public void DoOleDragOver(DragEventArgs de)
		{
			if (!this.localDrag && !this.dragOk)
			{
				de.Effect = DragDropEffects.None;
				return;
			}
			bool flag = (de.KeyState & 8) != 0 && (de.AllowedEffect & DragDropEffects.Copy) != DragDropEffects.None && this.client.CanModifyComponents;
			bool flag2 = (de.AllowedEffect & (DragDropEffects)67108864) != DragDropEffects.None && this.localDragInside;
			bool flag3 = (de.AllowedEffect & DragDropEffects.Move) > DragDropEffects.None || flag2;
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

		// Token: 0x06001F5A RID: 8026 RVA: 0x000BD360 File Offset: 0x000BB560
		public void DoOleGiveFeedback(GiveFeedbackEventArgs e)
		{
			SelectionUIHandler selectionUIHandler = this.selectionHandler;
			e.UseDefaultCursors = ((!this.localDragInside && !this.forceDrawFrames) || (e.Effect & DragDropEffects.Copy) != DragDropEffects.None || e.Effect == DragDropEffects.None);
			if (!e.UseDefaultCursors && this.selectionHandler != null)
			{
				this.selectionHandler.SetCursor();
			}
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x000BD3BC File Offset: 0x000BB5BC
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

		// Token: 0x06001F5C RID: 8028 RVA: 0x000BD3FD File Offset: 0x000BB5FD
		public object[] GetDraggingObjects(IDataObject dataObj)
		{
			return this.GetDraggingObjects(dataObj, false);
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x000BD407 File Offset: 0x000BB607
		public object[] GetDraggingObjects(DragEventArgs de)
		{
			return this.GetDraggingObjects(de.Data);
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x000BD418 File Offset: 0x000BB618
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

		// Token: 0x06001F5F RID: 8031 RVA: 0x000BD4C4 File Offset: 0x000BB6C4
		protected object GetService(Type t)
		{
			return this.serviceProvider.GetService(t);
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void OnInitializeComponent(IComponent comp, int x, int y, int width, int height, bool hasLocation, bool hasSize)
		{
		}

		// Token: 0x04001857 RID: 6231
		protected const int AllowLocalMoveOnly = 67108864;

		// Token: 0x04001858 RID: 6232
		private SelectionUIHandler selectionHandler;

		// Token: 0x04001859 RID: 6233
		private IServiceProvider serviceProvider;

		// Token: 0x0400185A RID: 6234
		private IOleDragClient client;

		// Token: 0x0400185B RID: 6235
		private bool dragOk;

		// Token: 0x0400185C RID: 6236
		private bool forceDrawFrames;

		// Token: 0x0400185D RID: 6237
		private bool localDrag;

		// Token: 0x0400185E RID: 6238
		private bool localDragInside;

		// Token: 0x0400185F RID: 6239
		private Point localDragOffset = Point.Empty;

		// Token: 0x04001860 RID: 6240
		private DragDropEffects localDragEffect;

		// Token: 0x04001861 RID: 6241
		private object[] dragComps;

		// Token: 0x04001862 RID: 6242
		private Point dragBase = Point.Empty;

		// Token: 0x04001863 RID: 6243
		private static bool freezePainting = false;

		// Token: 0x04001864 RID: 6244
		private static Hashtable currentDrags;

		// Token: 0x04001865 RID: 6245
		private static CodeMarkers codemarkers = CodeMarkers.Instance;

		// Token: 0x04001866 RID: 6246
		public const string CF_CODE = "CF_XMLCODE";

		// Token: 0x04001867 RID: 6247
		public const string CF_COMPONENTTYPES = "CF_COMPONENTTYPES";

		// Token: 0x04001868 RID: 6248
		public const string CF_TOOLBOXITEM = "CF_NESTEDTOOLBOXITEM";

		// Token: 0x02000584 RID: 1412
		protected class ComponentDataObjectWrapper : DataObject
		{
			// Token: 0x0600327F RID: 12927 RVA: 0x00111337 File Offset: 0x0010F537
			public ComponentDataObjectWrapper(OleDragDropHandler.ComponentDataObject dataObject) : base(dataObject)
			{
				this.innerData = dataObject;
			}

			// Token: 0x170009EF RID: 2543
			// (get) Token: 0x06003280 RID: 12928 RVA: 0x00111347 File Offset: 0x0010F547
			public OleDragDropHandler.ComponentDataObject InnerData
			{
				get
				{
					return this.innerData;
				}
			}

			// Token: 0x040021A4 RID: 8612
			private OleDragDropHandler.ComponentDataObject innerData;
		}

		// Token: 0x02000585 RID: 1413
		protected class ComponentDataObject : IDataObject
		{
			// Token: 0x06003281 RID: 12929 RVA: 0x0011134F File Offset: 0x0010F54F
			public ComponentDataObject(IOleDragClient dragClient, IServiceProvider sp, object[] comps, int x, int y)
			{
				this.serviceProvider = sp;
				this.components = this.GetComponentList(comps, null, -1);
				this.initialX = x;
				this.initialY = y;
				this.dragClient = dragClient;
			}

			// Token: 0x06003282 RID: 12930 RVA: 0x00111384 File Offset: 0x0010F584
			public ComponentDataObject(IOleDragClient dragClient, IServiceProvider sp, object serializationData)
			{
				this.serviceProvider = sp;
				this.serializationData = serializationData;
				this.dragClient = dragClient;
			}

			// Token: 0x170009F0 RID: 2544
			// (get) Token: 0x06003283 RID: 12931 RVA: 0x001113A4 File Offset: 0x0010F5A4
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

			// Token: 0x170009F1 RID: 2545
			// (get) Token: 0x06003284 RID: 12932 RVA: 0x00111458 File Offset: 0x0010F658
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

			// Token: 0x170009F2 RID: 2546
			// (get) Token: 0x06003285 RID: 12933 RVA: 0x001114A4 File Offset: 0x0010F6A4
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

			// Token: 0x06003286 RID: 12934 RVA: 0x001114CC File Offset: 0x0010F6CC
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

			// Token: 0x06003287 RID: 12935 RVA: 0x001115B0 File Offset: 0x0010F7B0
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

			// Token: 0x06003288 RID: 12936 RVA: 0x00111624 File Offset: 0x0010F824
			public virtual object GetData(string format)
			{
				return this.GetData(format, false);
			}

			// Token: 0x06003289 RID: 12937 RVA: 0x00111630 File Offset: 0x0010F830
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

			// Token: 0x0600328A RID: 12938 RVA: 0x0011168C File Offset: 0x0010F88C
			public virtual object GetData(Type t)
			{
				return this.GetData(t.FullName);
			}

			// Token: 0x0600328B RID: 12939 RVA: 0x0011169A File Offset: 0x0010F89A
			public bool GetDataPresent(string format, bool autoConvert)
			{
				return Array.IndexOf<string>(this.GetFormats(), format) != -1;
			}

			// Token: 0x0600328C RID: 12940 RVA: 0x001116AE File Offset: 0x0010F8AE
			public bool GetDataPresent(string format)
			{
				return this.GetDataPresent(format, false);
			}

			// Token: 0x0600328D RID: 12941 RVA: 0x001116B8 File Offset: 0x0010F8B8
			public bool GetDataPresent(Type format)
			{
				return this.GetDataPresent(format.FullName, false);
			}

			// Token: 0x0600328E RID: 12942 RVA: 0x001116C7 File Offset: 0x0010F8C7
			public string[] GetFormats(bool autoConvert)
			{
				return this.GetFormats();
			}

			// Token: 0x0600328F RID: 12943 RVA: 0x001116CF File Offset: 0x0010F8CF
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

			// Token: 0x06003290 RID: 12944 RVA: 0x001116F8 File Offset: 0x0010F8F8
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
					object[] array2 = new IComponent[collection.Count];
					this.components = array2;
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

			// Token: 0x06003291 RID: 12945 RVA: 0x001118DC File Offset: 0x0010FADC
			public void SetData(string format, bool autoConvert, object data)
			{
				this.SetData(format, data);
			}

			// Token: 0x06003292 RID: 12946 RVA: 0x001118E6 File Offset: 0x0010FAE6
			public void SetData(string format, object data)
			{
				throw new Exception(SR.GetString("DragDropSetDataError"));
			}

			// Token: 0x06003293 RID: 12947 RVA: 0x001118F7 File Offset: 0x0010FAF7
			public void SetData(Type format, object data)
			{
				this.SetData(format.FullName, data);
			}

			// Token: 0x06003294 RID: 12948 RVA: 0x00111906 File Offset: 0x0010FB06
			public void SetData(object data)
			{
				this.SetData(data.GetType(), data);
			}

			// Token: 0x040021A5 RID: 8613
			private IServiceProvider serviceProvider;

			// Token: 0x040021A6 RID: 8614
			private object[] components;

			// Token: 0x040021A7 RID: 8615
			private Stream serializationStream;

			// Token: 0x040021A8 RID: 8616
			private object serializationData;

			// Token: 0x040021A9 RID: 8617
			private int initialX;

			// Token: 0x040021AA RID: 8618
			private int initialY;

			// Token: 0x040021AB RID: 8619
			private IOleDragClient dragClient;

			// Token: 0x040021AC RID: 8620
			private OleDragDropHandler.CfCodeToolboxItem toolboxitemdata;
		}

		// Token: 0x02000586 RID: 1414
		[Serializable]
		internal class CfCodeToolboxItem : ToolboxItem
		{
			// Token: 0x06003295 RID: 12949 RVA: 0x00111915 File Offset: 0x0010FB15
			public CfCodeToolboxItem(object serializationData)
			{
				this.serializationData = serializationData;
			}

			// Token: 0x06003296 RID: 12950 RVA: 0x0008C0A7 File Offset: 0x0008A2A7
			private CfCodeToolboxItem(SerializationInfo info, StreamingContext context)
			{
				this.Deserialize(info, context);
			}

			// Token: 0x06003297 RID: 12951 RVA: 0x00111924 File Offset: 0x0010FB24
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

			// Token: 0x06003298 RID: 12952 RVA: 0x0011196A File Offset: 0x0010FB6A
			protected override void Serialize(SerializationInfo info, StreamingContext context)
			{
				base.Serialize(info, context);
				if (this.serializationData != null)
				{
					info.AddValue("CfCodeToolboxItem.serializationData", this.serializationData);
				}
			}

			// Token: 0x06003299 RID: 12953 RVA: 0x00111990 File Offset: 0x0010FB90
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

			// Token: 0x0600329A RID: 12954 RVA: 0x001119E0 File Offset: 0x0010FBE0
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

			// Token: 0x0600329B RID: 12955 RVA: 0x00111C40 File Offset: 0x0010FE40
			protected override IComponent[] CreateComponentsCore(IDesignerHost host)
			{
				return this.CreateComponentsCore(host, null);
			}

			// Token: 0x040021AD RID: 8621
			private object serializationData;

			// Token: 0x040021AE RID: 8622
			private static int template;

			// Token: 0x040021AF RID: 8623
			private bool displaynameset;
		}
	}
}
