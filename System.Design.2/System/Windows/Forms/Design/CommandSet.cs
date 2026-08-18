using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Design;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002A4 RID: 676
	internal class CommandSet : IDisposable
	{
		// Token: 0x06001A04 RID: 6660 RVA: 0x00094948 File Offset: 0x00092B48
		public CommandSet(ISite site)
		{
			this.site = site;
			this.eventService = (IEventHandlerService)site.GetService(typeof(IEventHandlerService));
			this.eventService.EventHandlerChanged += this.OnEventHandlerChanged;
			IDesignerHost designerHost = (IDesignerHost)site.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				designerHost.Activated += this.UpdateClipboardItems;
			}
			this.statusCommandUI = new StatusCommandUI(site);
			IUIService uiService = site.GetService(typeof(IUIService)) as IUIService;
			CommandSet.CommandSetItem[] array = new CommandSet.CommandSetItem[17];
			array[0] = new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusDelete), new EventHandler(this.OnMenuDelete), StandardCommands.Delete, uiService);
			array[1] = new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusCopy), new EventHandler(this.OnMenuCopy), StandardCommands.Copy, uiService);
			array[2] = new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusCut), new EventHandler(this.OnMenuCut), StandardCommands.Cut, uiService);
			array[3] = new CommandSet.ImmediateCommandSetItem(this, new EventHandler(this.OnStatusPaste), new EventHandler(this.OnMenuPaste), StandardCommands.Paste, uiService);
			array[4] = new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusSelectAll), new EventHandler(this.OnMenuSelectAll), StandardCommands.SelectAll, true, uiService);
			array[5] = new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusAlways), new EventHandler(this.OnMenuDesignerProperties), MenuCommands.DesignerProperties, uiService);
			array[6] = new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusAlways), new EventHandler(this.OnKeyCancel), MenuCommands.KeyCancel, uiService);
			array[7] = new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusAlways), new EventHandler(this.OnKeyCancel), MenuCommands.KeyReverseCancel, uiService);
			array[8] = new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusPrimarySelection), new EventHandler(this.OnKeyDefault), MenuCommands.KeyDefaultAction, true, uiService);
			array[9] = new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusAnySelection), new EventHandler(this.OnKeyMove), MenuCommands.KeyMoveUp, true, uiService);
			CommandSet.CommandSetItem[] array2 = array;
			array2[10] = new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusAnySelection), new EventHandler(this.OnKeyMove), MenuCommands.KeyMoveDown, true, uiService);
			CommandSet.CommandSetItem[] array3 = array2;
			array3[11] = new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusAnySelection), new EventHandler(this.OnKeyMove), MenuCommands.KeyMoveLeft, true, uiService);
			CommandSet.CommandSetItem[] array4 = array3;
			array4[12] = new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusAnySelection), new EventHandler(this.OnKeyMove), MenuCommands.KeyMoveRight, true);
			CommandSet.CommandSetItem[] array5 = array4;
			array5[13] = new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusAnySelection), new EventHandler(this.OnKeyMove), MenuCommands.KeyNudgeUp, true, uiService);
			CommandSet.CommandSetItem[] array6 = array5;
			array6[14] = new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusAnySelection), new EventHandler(this.OnKeyMove), MenuCommands.KeyNudgeDown, true, uiService);
			CommandSet.CommandSetItem[] array7 = array6;
			array7[15] = new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusAnySelection), new EventHandler(this.OnKeyMove), MenuCommands.KeyNudgeLeft, true, uiService);
			CommandSet.CommandSetItem[] array8 = array7;
			array8[16] = new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusAnySelection), new EventHandler(this.OnKeyMove), MenuCommands.KeyNudgeRight, true, uiService);
			this.commandSet = array8;
			this.selectionService = (ISelectionService)site.GetService(typeof(ISelectionService));
			if (this.selectionService != null)
			{
				this.selectionService.SelectionChanged += this.OnSelectionChanged;
			}
			this.menuService = (IMenuCommandService)site.GetService(typeof(IMenuCommandService));
			if (this.menuService != null)
			{
				for (int i = 0; i < this.commandSet.Length; i++)
				{
					this.menuService.AddCommand(this.commandSet[i]);
				}
			}
			IDictionaryService dictionaryService = site.GetService(typeof(IDictionaryService)) as IDictionaryService;
			if (dictionaryService != null)
			{
				dictionaryService.SetValue(typeof(CommandID), new CommandID(new Guid("BA09E2AF-9DF2-4068-B2F0-4C7E5CC19E2F"), 0));
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x00094D62 File Offset: 0x00092F62
		protected BehaviorService BehaviorService
		{
			get
			{
				if (this.behaviorService == null)
				{
					this.behaviorService = (this.GetService(typeof(BehaviorService)) as BehaviorService);
				}
				return this.behaviorService;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x00094D8D File Offset: 0x00092F8D
		protected IMenuCommandService MenuService
		{
			get
			{
				if (this.menuService == null)
				{
					this.menuService = (IMenuCommandService)this.GetService(typeof(IMenuCommandService));
				}
				return this.menuService;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x00094DB8 File Offset: 0x00092FB8
		protected ISelectionService SelectionService
		{
			get
			{
				return this.selectionService;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x00094DC0 File Offset: 0x00092FC0
		protected int SelectionVersion
		{
			get
			{
				return this.selectionVersion;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x00094DC8 File Offset: 0x00092FC8
		protected Timer SnapLineTimer
		{
			get
			{
				if (this.snapLineTimer == null)
				{
					this.snapLineTimer = new Timer();
					this.snapLineTimer.Interval = DesignerUtils.SNAPELINEDELAY;
					this.snapLineTimer.Tick += this.OnSnapLineTimerExpire;
				}
				return this.snapLineTimer;
			}
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x00094E18 File Offset: 0x00093018
		private bool CheckComponentEditor(object obj, bool launchEditor)
		{
			if (obj is IComponent)
			{
				try
				{
					if (!launchEditor)
					{
						return true;
					}
					ComponentEditor componentEditor = (ComponentEditor)TypeDescriptor.GetEditor(obj, typeof(ComponentEditor));
					if (componentEditor == null)
					{
						return false;
					}
					IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
					if (componentChangeService != null)
					{
						try
						{
							componentChangeService.OnComponentChanging(obj, null);
						}
						catch (CheckoutException ex)
						{
							if (ex == CheckoutException.Canceled)
							{
								return false;
							}
							throw ex;
						}
						catch
						{
							throw;
						}
					}
					WindowsFormsComponentEditor windowsFormsComponentEditor = componentEditor as WindowsFormsComponentEditor;
					bool flag;
					if (windowsFormsComponentEditor != null)
					{
						IWin32Window win32Window = null;
						if (obj is IWin32Window)
						{
							win32Window = win32Window;
						}
						flag = windowsFormsComponentEditor.EditComponent(obj, win32Window);
					}
					else
					{
						flag = componentEditor.EditComponent(obj);
					}
					if (flag && componentChangeService != null)
					{
						componentChangeService.OnComponentChanged(obj, null, null, null);
					}
					return true;
				}
				catch (Exception ex2)
				{
					if (ClientUtils.IsCriticalException(ex2))
					{
						throw;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x00094F14 File Offset: 0x00093114
		public virtual void Dispose()
		{
			if (this.menuService != null)
			{
				for (int i = 0; i < this.commandSet.Length; i++)
				{
					this.menuService.RemoveCommand(this.commandSet[i]);
					this.commandSet[i].Dispose();
				}
				this.menuService = null;
			}
			if (this.selectionService != null)
			{
				this.selectionService.SelectionChanged -= this.OnSelectionChanged;
				this.selectionService = null;
			}
			if (this.eventService != null)
			{
				this.eventService.EventHandlerChanged -= this.OnEventHandlerChanged;
				this.eventService = null;
			}
			IDesignerHost designerHost = (IDesignerHost)this.site.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				designerHost.Activated -= this.UpdateClipboardItems;
			}
			if (this.snapLineTimer != null)
			{
				this.snapLineTimer.Stop();
				this.snapLineTimer.Tick -= this.OnSnapLineTimerExpire;
				this.snapLineTimer = null;
			}
			this.EndDragManager();
			this.statusCommandUI = null;
			this.site = null;
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x00095024 File Offset: 0x00093224
		protected void EndDragManager()
		{
			if (this.dragManager != null)
			{
				if (this.snapLineTimer != null)
				{
					this.snapLineTimer.Stop();
				}
				this.dragManager.EraseSnapLines();
				this.dragManager.OnMouseUp();
				this.dragManager = null;
			}
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x00095060 File Offset: 0x00093260
		private object[] FilterSelection(object[] components, SelectionRules selectionRules)
		{
			object[] array = null;
			if (components == null)
			{
				return new object[0];
			}
			if (selectionRules != SelectionRules.None)
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					ArrayList arrayList = new ArrayList();
					foreach (IComponent component in components)
					{
						ControlDesigner controlDesigner = designerHost.GetDesigner(component) as ControlDesigner;
						if (controlDesigner != null && (controlDesigner.SelectionRules & selectionRules) == selectionRules)
						{
							arrayList.Add(component);
						}
					}
					array = arrayList.ToArray();
				}
			}
			if (array != null)
			{
				return array;
			}
			return new object[0];
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x000950F4 File Offset: 0x000932F4
		protected virtual ICollection GetCopySelection()
		{
			ICollection collection = this.SelectionService.GetSelectedComponents();
			bool flag = false;
			object[] array = new object[collection.Count];
			collection.CopyTo(array, 0);
			foreach (object obj in array)
			{
				if (obj is Control)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				this.SortSelection(array, 2);
			}
			collection = array;
			IDesignerHost designerHost = (IDesignerHost)this.site.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj2 in collection)
				{
					IComponent component = (IComponent)obj2;
					arrayList.Add(component);
					this.GetAssociatedComponents(component, designerHost, arrayList);
				}
				collection = arrayList;
			}
			return collection;
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x000951E0 File Offset: 0x000933E0
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
				if (component2.Site != null)
				{
					list.Add(component2);
					this.GetAssociatedComponents(component2, host, list);
				}
			}
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x0009525C File Offset: 0x0009345C
		private Point GetLocation(IComponent comp)
		{
			PropertyDescriptor property = this.GetProperty(comp, "Location");
			if (property != null)
			{
				try
				{
					return (Point)property.GetValue(comp);
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
					}
				}
			}
			return Point.Empty;
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x000952AC File Offset: 0x000934AC
		protected PropertyDescriptor GetProperty(object comp, string propName)
		{
			return TypeDescriptor.GetProperties(comp)[propName];
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x000952BA File Offset: 0x000934BA
		protected virtual object GetService(Type serviceType)
		{
			if (this.site != null)
			{
				return this.site.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x000952D4 File Offset: 0x000934D4
		private Size GetSize(IComponent comp)
		{
			PropertyDescriptor property = this.GetProperty(comp, "Size");
			if (property != null)
			{
				return (Size)property.GetValue(comp);
			}
			return Size.Empty;
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x00095304 File Offset: 0x00093504
		protected virtual void GetSnapInformation(IDesignerHost host, IComponent component, out Size snapSize, out IComponent snapComponent, out PropertyDescriptor snapProperty)
		{
			IComponent rootComponent = host.RootComponent;
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(rootComponent);
			PropertyDescriptor propertyDescriptor = properties["SnapToGrid"];
			if (propertyDescriptor != null && propertyDescriptor.PropertyType != typeof(bool))
			{
				propertyDescriptor = null;
			}
			PropertyDescriptor propertyDescriptor2 = properties["GridSize"];
			if (propertyDescriptor2 != null && propertyDescriptor2.PropertyType != typeof(Size))
			{
				propertyDescriptor2 = null;
			}
			snapComponent = rootComponent;
			snapProperty = propertyDescriptor;
			if (propertyDescriptor2 != null)
			{
				snapSize = (Size)propertyDescriptor2.GetValue(snapComponent);
				return;
			}
			snapSize = Size.Empty;
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x000953A0 File Offset: 0x000935A0
		protected bool CanCheckout(IComponent comp)
		{
			IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				try
				{
					componentChangeService.OnComponentChanging(comp, null);
				}
				catch (CheckoutException ex)
				{
					if (ex == CheckoutException.Canceled)
					{
						return false;
					}
					throw ex;
				}
				return true;
			}
			return true;
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x000953F4 File Offset: 0x000935F4
		private void OnEventHandlerChanged(object sender, EventArgs e)
		{
			this.OnUpdateCommandStatus();
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x000953FC File Offset: 0x000935FC
		private void OnKeyCancel(object sender, EventArgs e)
		{
			this.OnKeyCancel(sender);
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x00095408 File Offset: 0x00093608
		protected virtual bool OnKeyCancel(object sender)
		{
			bool result = false;
			if (this.BehaviorService != null && this.BehaviorService.HasCapture)
			{
				this.BehaviorService.OnLoseCapture();
				result = true;
			}
			else
			{
				IToolboxService toolboxService = (IToolboxService)this.GetService(typeof(IToolboxService));
				if (toolboxService != null && toolboxService.GetSelectedToolboxItem((IDesignerHost)this.GetService(typeof(IDesignerHost))) != null)
				{
					toolboxService.SelectedToolboxItemUsed();
					NativeMethods.POINT point = new NativeMethods.POINT();
					NativeMethods.GetCursorPos(point);
					IntPtr intPtr = NativeMethods.WindowFromPoint(point.x, point.y);
					if (intPtr != IntPtr.Zero)
					{
						NativeMethods.SendMessage(intPtr, 32, intPtr, (IntPtr)1);
					}
					else
					{
						Cursor.Current = Cursors.Default;
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x000954C8 File Offset: 0x000936C8
		protected void OnKeyDefault(object sender, EventArgs e)
		{
			ISelectionService selectionService = this.SelectionService;
			if (selectionService != null)
			{
				IComponent component = selectionService.PrimarySelection as IComponent;
				if (component != null)
				{
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						IDesigner designer = designerHost.GetDesigner(component);
						if (designer != null)
						{
							designer.DoDefaultAction();
						}
					}
				}
			}
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x00095518 File Offset: 0x00093718
		protected virtual void OnKeyMove(object sender, EventArgs e)
		{
			ISelectionService selectionService = this.SelectionService;
			if (selectionService != null)
			{
				IComponent component = selectionService.PrimarySelection as IComponent;
				if (component != null)
				{
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)["Locked"];
						if (propertyDescriptor == null || !(propertyDescriptor.PropertyType == typeof(bool)) || !(bool)propertyDescriptor.GetValue(component))
						{
							CommandID commandID = ((MenuCommand)sender).CommandID;
							bool flag = false;
							int num = 0;
							int num2 = 0;
							if (commandID.Equals(MenuCommands.KeyMoveUp))
							{
								num2 = -1;
							}
							else if (commandID.Equals(MenuCommands.KeyMoveDown))
							{
								num2 = 1;
							}
							else if (commandID.Equals(MenuCommands.KeyMoveLeft))
							{
								num = -1;
							}
							else if (commandID.Equals(MenuCommands.KeyMoveRight))
							{
								num = 1;
							}
							else if (commandID.Equals(MenuCommands.KeyNudgeUp))
							{
								num2 = -1;
								flag = true;
							}
							else if (commandID.Equals(MenuCommands.KeyNudgeDown))
							{
								num2 = 1;
								flag = true;
							}
							else if (commandID.Equals(MenuCommands.KeyNudgeLeft))
							{
								num = -1;
								flag = true;
							}
							else if (commandID.Equals(MenuCommands.KeyNudgeRight))
							{
								num = 1;
								flag = true;
							}
							DesignerTransaction designerTransaction;
							if (selectionService.SelectionCount > 1)
							{
								designerTransaction = designerHost.CreateTransaction(SR.GetString("DragDropMoveComponents", new object[]
								{
									selectionService.SelectionCount
								}));
							}
							else
							{
								designerTransaction = designerHost.CreateTransaction(SR.GetString("DragDropMoveComponent", new object[]
								{
									component.Site.Name
								}));
							}
							try
							{
								if (this.BehaviorService != null)
								{
									Control control = component as Control;
									bool useSnapLines = this.BehaviorService.UseSnapLines;
									if (this.dragManager != null)
									{
										this.EndDragManager();
									}
									if (flag && useSnapLines && control != null)
									{
										ArrayList dragComponents = new ArrayList(selectionService.GetSelectedComponents());
										this.dragManager = new DragAssistanceManager(component.Site, dragComponents);
										Point point = this.dragManager.OffsetToNearestSnapLocation(control, new Point(num, num2));
										num = point.X;
										num2 = point.Y;
										if (control.Parent.IsMirrored)
										{
											num *= -1;
										}
									}
									else if (!flag && !useSnapLines)
									{
										bool flag2 = false;
										Size empty = Size.Empty;
										IComponent component2 = null;
										PropertyDescriptor propertyDescriptor2 = null;
										this.GetSnapInformation(designerHost, component, out empty, out component2, out propertyDescriptor2);
										if (propertyDescriptor2 != null)
										{
											flag2 = (bool)propertyDescriptor2.GetValue(component2);
										}
										if (flag2 && !empty.IsEmpty)
										{
											num *= empty.Width;
											num2 *= empty.Height;
											if (control != null)
											{
												ParentControlDesigner parentControlDesigner = designerHost.GetDesigner(control.Parent) as ParentControlDesigner;
												if (parentControlDesigner != null)
												{
													Point pt = control.Location;
													if (control.Parent.IsMirrored)
													{
														num *= -1;
													}
													pt.Offset(num, num2);
													pt = parentControlDesigner.GetSnappedPoint(pt);
													if (num != 0)
													{
														num = pt.X - control.Location.X;
													}
													if (num2 != 0)
													{
														num2 = pt.Y - control.Location.Y;
													}
												}
											}
										}
										else if (control != null && control.Parent.IsMirrored)
										{
											num *= -1;
										}
									}
									else if (control != null && control.Parent.IsMirrored)
									{
										num *= -1;
									}
									SelectionRules selectionRules = SelectionRules.Moveable | SelectionRules.Visible;
									foreach (object obj in selectionService.GetSelectedComponents())
									{
										IComponent component3 = (IComponent)obj;
										ControlDesigner controlDesigner = designerHost.GetDesigner(component3) as ControlDesigner;
										if (controlDesigner == null || (controlDesigner.SelectionRules & selectionRules) == selectionRules)
										{
											PropertyDescriptor propertyDescriptor3 = TypeDescriptor.GetProperties(component3)["Location"];
											if (propertyDescriptor3 != null)
											{
												Point point2 = (Point)propertyDescriptor3.GetValue(component3);
												point2.Offset(num, num2);
												propertyDescriptor3.SetValue(component3, point2);
											}
											if (component3 == selectionService.PrimarySelection && this.statusCommandUI != null)
											{
												this.statusCommandUI.SetStatusInformation(component3 as Component);
											}
										}
									}
								}
							}
							finally
							{
								if (designerTransaction != null)
								{
									designerTransaction.Commit();
								}
								if (this.dragManager != null)
								{
									this.SnapLineTimer.Start();
									this.dragManager.RenderSnapLinesInternal();
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x000959B0 File Offset: 0x00093BB0
		protected void OnMenuAlignByPrimary(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			CommandID commandID = menuCommand.CommandID;
			Point location = this.GetLocation(this.primarySelection);
			Size size = this.GetSize(this.primarySelection);
			if (this.SelectionService == null)
			{
				return;
			}
			Cursor value = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				ICollection selectedComponents = this.SelectionService.GetSelectedComponents();
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				DesignerTransaction designerTransaction = null;
				try
				{
					if (designerHost != null)
					{
						designerTransaction = designerHost.CreateTransaction(SR.GetString("CommandSetAlignByPrimary", new object[]
						{
							selectedComponents.Count
						}));
					}
					bool flag = true;
					Point point = Point.Empty;
					foreach (object obj in selectedComponents)
					{
						if (obj != this.primarySelection)
						{
							IComponent component = obj as IComponent;
							if (component != null && designerHost != null)
							{
								ControlDesigner controlDesigner = designerHost.GetDesigner(component) as ControlDesigner;
								if (controlDesigner == null)
								{
									continue;
								}
							}
							PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
							PropertyDescriptor propertyDescriptor = properties["Location"];
							PropertyDescriptor propertyDescriptor2 = properties["Size"];
							PropertyDescriptor propertyDescriptor3 = properties["Locked"];
							if ((propertyDescriptor3 == null || !(bool)propertyDescriptor3.GetValue(component)) && propertyDescriptor != null && !propertyDescriptor.IsReadOnly && ((!commandID.Equals(StandardCommands.AlignBottom) && !commandID.Equals(StandardCommands.AlignHorizontalCenters) && !commandID.Equals(StandardCommands.AlignVerticalCenters) && !commandID.Equals(StandardCommands.AlignRight)) || (propertyDescriptor2 != null && !propertyDescriptor2.IsReadOnly)))
							{
								if (commandID.Equals(StandardCommands.AlignBottom))
								{
									point = (Point)propertyDescriptor.GetValue(component);
									Size size2 = (Size)propertyDescriptor2.GetValue(component);
									point.Y = location.Y + size.Height - size2.Height;
								}
								else if (commandID.Equals(StandardCommands.AlignHorizontalCenters))
								{
									point = (Point)propertyDescriptor.GetValue(component);
									Size size3 = (Size)propertyDescriptor2.GetValue(component);
									point.Y = size.Height / 2 + location.Y - size3.Height / 2;
								}
								else if (commandID.Equals(StandardCommands.AlignLeft))
								{
									point = (Point)propertyDescriptor.GetValue(component);
									point.X = location.X;
								}
								else if (commandID.Equals(StandardCommands.AlignRight))
								{
									point = (Point)propertyDescriptor.GetValue(component);
									Size size4 = (Size)propertyDescriptor2.GetValue(component);
									point.X = location.X + size.Width - size4.Width;
								}
								else if (commandID.Equals(StandardCommands.AlignTop))
								{
									point = (Point)propertyDescriptor.GetValue(component);
									point.Y = location.Y;
								}
								else if (commandID.Equals(StandardCommands.AlignVerticalCenters))
								{
									point = (Point)propertyDescriptor.GetValue(component);
									Size size5 = (Size)propertyDescriptor2.GetValue(component);
									point.X = size.Width / 2 + location.X - size5.Width / 2;
								}
								if (flag && !this.CanCheckout(component))
								{
									break;
								}
								flag = false;
								propertyDescriptor.SetValue(component, point);
							}
						}
					}
				}
				finally
				{
					if (designerTransaction != null)
					{
						designerTransaction.Commit();
					}
				}
			}
			finally
			{
				Cursor.Current = value;
			}
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x00095D90 File Offset: 0x00093F90
		protected void OnMenuAlignToGrid(object sender, EventArgs e)
		{
			Size size = Size.Empty;
			Point point = Point.Empty;
			if (this.SelectionService == null)
			{
				return;
			}
			Cursor value = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				ICollection selectedComponents = this.SelectionService.GetSelectedComponents();
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				DesignerTransaction designerTransaction = null;
				try
				{
					if (designerHost != null)
					{
						designerTransaction = designerHost.CreateTransaction(SR.GetString("CommandSetAlignToGrid", new object[]
						{
							selectedComponents.Count
						}));
						Control control = designerHost.RootComponent as Control;
						if (control != null)
						{
							PropertyDescriptor property = this.GetProperty(control, "GridSize");
							if (property != null)
							{
								size = (Size)property.GetValue(control);
							}
							if (property == null || size.IsEmpty)
							{
								return;
							}
						}
					}
					bool flag = true;
					foreach (object obj in selectedComponents)
					{
						PropertyDescriptor property2 = this.GetProperty(obj, "Locked");
						if (property2 == null || !(bool)property2.GetValue(obj))
						{
							IComponent component = obj as IComponent;
							if (component != null && designerHost != null)
							{
								ControlDesigner controlDesigner = designerHost.GetDesigner(component) as ControlDesigner;
								if (controlDesigner == null)
								{
									continue;
								}
							}
							PropertyDescriptor property3 = this.GetProperty(obj, "Location");
							if (property3 != null && !property3.IsReadOnly)
							{
								point = (Point)property3.GetValue(obj);
								int num = point.X % size.Width;
								if (num < size.Width / 2)
								{
									point.X -= num;
								}
								else
								{
									point.X += size.Width - num;
								}
								num = point.Y % size.Height;
								if (num < size.Height / 2)
								{
									point.Y -= num;
								}
								else
								{
									point.Y += size.Height - num;
								}
								if (flag && !this.CanCheckout(component))
								{
									break;
								}
								flag = false;
								property3.SetValue(obj, point);
							}
						}
					}
				}
				finally
				{
					if (designerTransaction != null)
					{
						designerTransaction.Commit();
					}
				}
			}
			finally
			{
				Cursor.Current = value;
			}
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x00096028 File Offset: 0x00094228
		protected void OnMenuCenterSelection(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			CommandID commandID = menuCommand.CommandID;
			if (this.SelectionService == null)
			{
				return;
			}
			Cursor value = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				ICollection selectedComponents = this.SelectionService.GetSelectedComponents();
				Control control = null;
				Size size = Size.Empty;
				Point point = Point.Empty;
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				DesignerTransaction designerTransaction = null;
				try
				{
					if (designerHost != null)
					{
						string @string;
						if (commandID == StandardCommands.CenterHorizontally)
						{
							@string = SR.GetString("WindowsFormsCommandCenterX", new object[]
							{
								selectedComponents.Count
							});
						}
						else
						{
							@string = SR.GetString("WindowsFormsCommandCenterY", new object[]
							{
								selectedComponents.Count
							});
						}
						designerTransaction = designerHost.CreateTransaction(@string);
					}
					int num = int.MaxValue;
					int num2 = int.MaxValue;
					int num3 = int.MinValue;
					int num4 = int.MinValue;
					foreach (object obj in selectedComponents)
					{
						if (obj is Control)
						{
							IComponent component = (IComponent)obj;
							PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
							PropertyDescriptor propertyDescriptor = properties["Location"];
							PropertyDescriptor propertyDescriptor2 = properties["Size"];
							if (propertyDescriptor != null && propertyDescriptor2 != null && !propertyDescriptor.IsReadOnly && !propertyDescriptor2.IsReadOnly)
							{
								PropertyDescriptor propertyDescriptor3 = properties["Locked"];
								if (propertyDescriptor3 == null || !(bool)propertyDescriptor3.GetValue(component))
								{
									size = (Size)propertyDescriptor2.GetValue(component);
									point = (Point)propertyDescriptor.GetValue(component);
									if (control == null)
									{
										control = ((Control)component).Parent;
									}
									if (point.X < num2)
									{
										num2 = point.X;
									}
									if (point.Y < num)
									{
										num = point.Y;
									}
									if (point.X + size.Width > num3)
									{
										num3 = point.X + size.Width;
									}
									if (point.Y + size.Height > num4)
									{
										num4 = point.Y + size.Height;
									}
								}
							}
						}
					}
					if (control != null)
					{
						int num5 = (num2 + num3) / 2;
						int num6 = (num + num4) / 2;
						int num7 = control.ClientSize.Width / 2;
						int num8 = control.ClientSize.Height / 2;
						bool flag = false;
						bool flag2 = false;
						int num9;
						if (num7 >= num5)
						{
							num9 = num7 - num5;
							flag = true;
						}
						else
						{
							num9 = num5 - num7;
						}
						int num10;
						if (num8 >= num6)
						{
							num10 = num8 - num6;
							flag2 = true;
						}
						else
						{
							num10 = num6 - num8;
						}
						bool flag3 = true;
						foreach (object obj2 in selectedComponents)
						{
							if (obj2 is Control)
							{
								IComponent component2 = (IComponent)obj2;
								PropertyDescriptorCollection properties2 = TypeDescriptor.GetProperties(component2);
								PropertyDescriptor propertyDescriptor4 = properties2["Location"];
								if (!propertyDescriptor4.IsReadOnly)
								{
									point = (Point)propertyDescriptor4.GetValue(component2);
									if (commandID == StandardCommands.CenterHorizontally)
									{
										if (flag)
										{
											point.X += num9;
										}
										else
										{
											point.X -= num9;
										}
									}
									else if (commandID == StandardCommands.CenterVertically)
									{
										if (flag2)
										{
											point.Y += num10;
										}
										else
										{
											point.Y -= num10;
										}
									}
									if (flag3 && !this.CanCheckout(component2))
									{
										break;
									}
									flag3 = false;
									propertyDescriptor4.SetValue(component2, point);
								}
							}
						}
					}
				}
				finally
				{
					if (designerTransaction != null)
					{
						designerTransaction.Commit();
					}
				}
			}
			finally
			{
				Cursor.Current = value;
			}
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x00096468 File Offset: 0x00094668
		protected void OnMenuCopy(object sender, EventArgs e)
		{
			if (this.SelectionService == null)
			{
				return;
			}
			Cursor value = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				ICollection objects = this.GetCopySelection();
				objects = this.PrependComponentNames(objects);
				IDesignerSerializationService designerSerializationService = (IDesignerSerializationService)this.GetService(typeof(IDesignerSerializationService));
				if (designerSerializationService != null)
				{
					object graph = designerSerializationService.Serialize(objects);
					MemoryStream memoryStream = new MemoryStream();
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize(memoryStream, graph);
					memoryStream.Seek(0L, SeekOrigin.Begin);
					byte[] buffer = memoryStream.GetBuffer();
					IDataObject dataObject = new DataObject("CF_DESIGNERCOMPONENTS_V2", buffer);
					Clipboard.SetDataObject(dataObject);
				}
				this.UpdateClipboardItems(null, null);
			}
			finally
			{
				Cursor.Current = value;
			}
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x00096520 File Offset: 0x00094720
		protected void OnMenuCut(object sender, EventArgs e)
		{
			if (this.SelectionService == null)
			{
				return;
			}
			Cursor value = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				ICollection collection = this.GetCopySelection();
				int count = collection.Count;
				collection = this.PrependComponentNames(collection);
				IDesignerSerializationService designerSerializationService = (IDesignerSerializationService)this.GetService(typeof(IDesignerSerializationService));
				if (designerSerializationService != null)
				{
					object graph = designerSerializationService.Serialize(collection);
					MemoryStream memoryStream = new MemoryStream();
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize(memoryStream, graph);
					memoryStream.Seek(0L, SeekOrigin.Begin);
					byte[] buffer = memoryStream.GetBuffer();
					IDataObject dataObject = new DataObject("CF_DESIGNERCOMPONENTS_V2", buffer);
					Clipboard.SetDataObject(dataObject);
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					Control control = null;
					if (designerHost != null)
					{
						IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
						DesignerTransaction designerTransaction = null;
						ArrayList arrayList = new ArrayList();
						try
						{
							designerTransaction = designerHost.CreateTransaction(SR.GetString("CommandSetCutMultiple", new object[]
							{
								count
							}));
							this.SelectionService.SetSelectedComponents(new object[0], SelectionTypes.Replace);
							object[] array = new object[collection.Count];
							collection.CopyTo(array, 0);
							foreach (object obj in array)
							{
								IComponent component = obj as IComponent;
								if (obj != designerHost.RootComponent && component != null)
								{
									Control control2 = obj as Control;
									if (control2 != null)
									{
										Control parent = control2.Parent;
										if (parent != null)
										{
											ParentControlDesigner parentControlDesigner = designerHost.GetDesigner(parent) as ParentControlDesigner;
											if (parentControlDesigner != null && !arrayList.Contains(parentControlDesigner))
											{
												parentControlDesigner.SuspendChangingEvents();
												arrayList.Add(parentControlDesigner);
												parentControlDesigner.ForceComponentChanging();
											}
										}
									}
								}
							}
							foreach (object obj2 in array)
							{
								IComponent component2 = obj2 as IComponent;
								if (obj2 != designerHost.RootComponent && component2 != null)
								{
									Control control3 = obj2 as Control;
									if (control == null && control3 != null)
									{
										control = control3.Parent;
									}
									else if (control != null && control3 != null)
									{
										Control control4 = control3;
										if (control4.Parent != control && !control.Contains(control4))
										{
											if (control4 == control || control4.Contains(control))
											{
												control = control4.Parent;
											}
											else
											{
												control = null;
											}
										}
									}
									if (component2 != null)
									{
										ArrayList arrayList2 = new ArrayList();
										this.GetAssociatedComponents(component2, designerHost, arrayList2);
										foreach (object obj3 in arrayList2)
										{
											IComponent component3 = (IComponent)obj3;
											componentChangeService.OnComponentChanging(component3, null);
										}
										designerHost.DestroyComponent(component2);
									}
								}
							}
						}
						finally
						{
							if (designerTransaction != null)
							{
								designerTransaction.Commit();
							}
							foreach (object obj4 in arrayList)
							{
								ParentControlDesigner parentControlDesigner2 = (ParentControlDesigner)obj4;
								if (parentControlDesigner2 != null)
								{
									parentControlDesigner2.ResumeChangingEvents();
								}
							}
						}
						if (control != null)
						{
							this.SelectionService.SetSelectedComponents(new object[]
							{
								control
							}, SelectionTypes.Replace);
						}
						else if (this.SelectionService.PrimarySelection == null)
						{
							this.SelectionService.SetSelectedComponents(new object[]
							{
								designerHost.RootComponent
							}, SelectionTypes.Replace);
						}
					}
				}
			}
			finally
			{
				Cursor.Current = value;
			}
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x000968D8 File Offset: 0x00094AD8
		protected void OnMenuDelete(object sender, EventArgs e)
		{
			Cursor value = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				if (this.site != null)
				{
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					if (this.SelectionService != null)
					{
						if (designerHost != null)
						{
							IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
							ICollection selectedComponents = this.SelectionService.GetSelectedComponents();
							string @string = SR.GetString("CommandSetDelete", new object[]
							{
								selectedComponents.Count
							});
							DesignerTransaction designerTransaction = null;
							IComponent component = null;
							bool flag = false;
							ArrayList arrayList = new ArrayList();
							try
							{
								designerTransaction = designerHost.CreateTransaction(@string);
								this.SelectionService.SetSelectedComponents(new object[0], SelectionTypes.Replace);
								foreach (object obj in selectedComponents)
								{
									IComponent component2 = obj as IComponent;
									if (component2 != null && component2.Site != null)
									{
										Control control = obj as Control;
										if (control != null)
										{
											Control parent = control.Parent;
											if (parent != null)
											{
												ParentControlDesigner parentControlDesigner = designerHost.GetDesigner(parent) as ParentControlDesigner;
												if (parentControlDesigner != null && !arrayList.Contains(parentControlDesigner))
												{
													parentControlDesigner.SuspendChangingEvents();
													arrayList.Add(parentControlDesigner);
													parentControlDesigner.ForceComponentChanging();
												}
											}
										}
									}
								}
								foreach (object obj2 in selectedComponents)
								{
									IComponent component3 = obj2 as IComponent;
									if (component3 != null && component3.Site != null && obj2 != designerHost.RootComponent)
									{
										Control control2 = obj2 as Control;
										if (!flag)
										{
											if (control2 != null)
											{
												component = control2.Parent;
											}
											else
											{
												ITreeDesigner treeDesigner = designerHost.GetDesigner((IComponent)obj2) as ITreeDesigner;
												if (treeDesigner != null)
												{
													IDesigner parent2 = treeDesigner.Parent;
													if (parent2 != null)
													{
														component = parent2.Component;
													}
												}
											}
											flag = (component != null);
										}
										else if (component != null)
										{
											if (control2 != null && component is Control)
											{
												Control control3 = control2;
												Control control4 = (Control)component;
												if (control3.Parent != control4 && !control4.Contains(control3))
												{
													if (control3 == control4 || control3.Contains(control4))
													{
														component = control3.Parent;
													}
													else
													{
														while (control4 != null && !control4.Contains(control3))
														{
															control4 = control4.Parent;
														}
														component = control4;
													}
												}
											}
											else
											{
												ITreeDesigner treeDesigner2 = designerHost.GetDesigner((IComponent)obj2) as ITreeDesigner;
												ITreeDesigner treeDesigner3 = designerHost.GetDesigner(component) as ITreeDesigner;
												if (treeDesigner2 != null && treeDesigner3 != null && treeDesigner2.Parent != treeDesigner3)
												{
													ArrayList arrayList2 = new ArrayList();
													ArrayList arrayList3 = new ArrayList();
													for (treeDesigner2 = (treeDesigner2.Parent as ITreeDesigner); treeDesigner2 != null; treeDesigner2 = (treeDesigner2.Parent as ITreeDesigner))
													{
														arrayList2.Add(treeDesigner2);
													}
													for (treeDesigner3 = (treeDesigner3.Parent as ITreeDesigner); treeDesigner3 != null; treeDesigner3 = (treeDesigner3.Parent as ITreeDesigner))
													{
														arrayList3.Add(treeDesigner3);
													}
													ArrayList arrayList4 = (arrayList2.Count < arrayList3.Count) ? arrayList2 : arrayList3;
													ArrayList arrayList5 = (arrayList4 == arrayList2) ? arrayList3 : arrayList2;
													treeDesigner3 = null;
													if (arrayList4.Count > 0 && arrayList5.Count > 0)
													{
														int num = Math.Max(0, arrayList4.Count - 1);
														int num2 = Math.Max(0, arrayList5.Count - 1);
														while (num >= 0 && num2 >= 0 && arrayList4[num] == arrayList5[num2])
														{
															treeDesigner3 = (ITreeDesigner)arrayList4[num];
															num--;
															num2--;
														}
													}
													if (treeDesigner3 != null)
													{
														component = treeDesigner3.Component;
													}
													else
													{
														component = null;
													}
												}
											}
										}
										ArrayList arrayList6 = new ArrayList();
										this.GetAssociatedComponents((IComponent)obj2, designerHost, arrayList6);
										foreach (object obj3 in arrayList6)
										{
											IComponent component4 = (IComponent)obj3;
											componentChangeService.OnComponentChanging(component4, null);
										}
										designerHost.DestroyComponent((IComponent)obj2);
									}
								}
							}
							finally
							{
								if (designerTransaction != null)
								{
									designerTransaction.Commit();
								}
								foreach (object obj4 in arrayList)
								{
									ParentControlDesigner parentControlDesigner2 = (ParentControlDesigner)obj4;
									if (parentControlDesigner2 != null)
									{
										parentControlDesigner2.ResumeChangingEvents();
									}
								}
							}
							if (component != null && this.SelectionService.PrimarySelection == null)
							{
								ITreeDesigner treeDesigner4 = designerHost.GetDesigner(component) as ITreeDesigner;
								if (treeDesigner4 != null && treeDesigner4.Children != null)
								{
									using (IEnumerator enumerator5 = treeDesigner4.Children.GetEnumerator())
									{
										while (enumerator5.MoveNext())
										{
											object obj5 = enumerator5.Current;
											IDesigner designer = (IDesigner)obj5;
											IComponent component5 = designer.Component;
											if (component5.Site != null)
											{
												component = component5;
												break;
											}
										}
										goto IL_55D;
									}
								}
								if (component is Control)
								{
									Control control5 = (Control)component;
									if (control5.Controls.Count > 0)
									{
										control5 = control5.Controls[0];
										while (control5 != null && control5.Site == null)
										{
											control5 = control5.Parent;
										}
										component = control5;
									}
								}
								IL_55D:
								if (component != null)
								{
									this.SelectionService.SetSelectedComponents(new object[]
									{
										component
									}, SelectionTypes.Replace);
								}
								else
								{
									this.SelectionService.SetSelectedComponents(new object[]
									{
										designerHost.RootComponent
									}, SelectionTypes.Replace);
								}
							}
							else if (this.SelectionService.PrimarySelection == null)
							{
								this.SelectionService.SetSelectedComponents(new object[]
								{
									designerHost.RootComponent
								}, SelectionTypes.Replace);
							}
						}
					}
				}
			}
			finally
			{
				Cursor.Current = value;
			}
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x00096F5C File Offset: 0x0009515C
		protected void OnMenuPaste(object sender, EventArgs e)
		{
			Cursor value = Cursor.Current;
			ArrayList arrayList = new ArrayList();
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				ICollection collection = null;
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					IDataObject dataObject = Clipboard.GetDataObject();
					ICollection collection2 = null;
					bool firstAdd = false;
					ComponentTray componentTray = null;
					int num = 0;
					componentTray = (this.GetService(typeof(ComponentTray)) as ComponentTray);
					num = ((componentTray != null) ? componentTray.Controls.Count : 0);
					object data = dataObject.GetData("CF_DESIGNERCOMPONENTS_V2");
					using (DesignerTransaction designerTransaction = designerHost.CreateTransaction(SR.GetString("CommandSetPaste")))
					{
						byte[] array = data as byte[];
						if (array != null)
						{
							MemoryStream memoryStream = new MemoryStream(array);
							if (memoryStream != null)
							{
								IDesignerSerializationService designerSerializationService = (IDesignerSerializationService)this.GetService(typeof(IDesignerSerializationService));
								if (designerSerializationService != null)
								{
									BinaryFormatter binaryFormatter = new BinaryFormatter();
									memoryStream.Seek(0L, SeekOrigin.Begin);
									object serializationData = binaryFormatter.Deserialize(memoryStream);
									collection2 = designerSerializationService.Deserialize(serializationData);
								}
							}
						}
						else
						{
							IToolboxService toolboxService = (IToolboxService)this.GetService(typeof(IToolboxService));
							if (toolboxService != null && toolboxService.IsSupported(dataObject, designerHost))
							{
								ToolboxItem toolboxItem = toolboxService.DeserializeToolboxItem(dataObject, designerHost);
								if (toolboxItem != null)
								{
									collection2 = toolboxItem.CreateComponents(designerHost);
									firstAdd = true;
								}
							}
						}
						if (collection2 != null && collection2.Count > 0)
						{
							object[] array2 = new object[collection2.Count];
							collection2.CopyTo(array2, 0);
							ArrayList arrayList2 = new ArrayList();
							ArrayList arrayList3 = new ArrayList();
							string[] array3 = null;
							int num2 = 0;
							IDesigner designer = null;
							bool flag = false;
							IComponent rootComponent = designerHost.RootComponent;
							IComponent component = (IComponent)this.SelectionService.PrimarySelection;
							if (component == null)
							{
								component = rootComponent;
							}
							flag = false;
							ITreeDesigner treeDesigner = designerHost.GetDesigner(component) as ITreeDesigner;
							while (!flag && treeDesigner != null)
							{
								if (treeDesigner is IOleDragClient)
								{
									designer = treeDesigner;
									flag = true;
								}
								else
								{
									if (treeDesigner == treeDesigner.Parent)
									{
										break;
									}
									treeDesigner = (treeDesigner.Parent as ITreeDesigner);
								}
							}
							foreach (object obj in collection2)
							{
								string text = null;
								IComponent component2 = obj as IComponent;
								if (obj is IComponent)
								{
									if (array3 != null && num2 < array3.Length)
									{
										text = array3[num2++];
									}
								}
								else
								{
									string[] array4 = obj as string[];
									if (array3 == null && array4 != null)
									{
										array3 = array4;
										num2 = 0;
										continue;
									}
								}
								IEventBindingService eventBindingService = this.GetService(typeof(IEventBindingService)) as IEventBindingService;
								if (eventBindingService != null)
								{
									PropertyDescriptorCollection eventProperties = eventBindingService.GetEventProperties(TypeDescriptor.GetEvents(component2));
									foreach (object obj2 in eventProperties)
									{
										PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj2;
										if (propertyDescriptor != null && !propertyDescriptor.IsReadOnly)
										{
											string text2 = propertyDescriptor.GetValue(component2) as string;
											if (text2 != null)
											{
												propertyDescriptor.SetValue(component2, null);
											}
										}
									}
								}
								if (flag)
								{
									bool flag2 = false;
									if (collection != null)
									{
										foreach (object obj3 in collection)
										{
											Component component3 = (Component)obj3;
											if (component3 == obj as Component)
											{
												flag2 = true;
												break;
											}
										}
									}
									if (!flag2)
									{
										ComponentDesigner componentDesigner = designerHost.GetDesigner(component2) as ComponentDesigner;
										ICollection collection3 = null;
										if (componentDesigner != null)
										{
											collection3 = componentDesigner.AssociatedComponents;
											ComponentDesigner componentDesigner2 = ((ITreeDesigner)componentDesigner).Parent as ComponentDesigner;
											Component component4 = null;
											if (componentDesigner2 != null)
											{
												component4 = (componentDesigner2.Component as Component);
											}
											ArrayList arrayList4 = new ArrayList();
											if (component4 != null && componentDesigner2 != null)
											{
												foreach (object obj4 in componentDesigner2.AssociatedComponents)
												{
													IComponent component5 = (IComponent)obj4;
													arrayList4.Add(component5 as Component);
												}
											}
											if (component4 == null || !arrayList4.Contains(component2))
											{
												if (component4 != null)
												{
													ParentControlDesigner parentControlDesigner = designerHost.GetDesigner(component4) as ParentControlDesigner;
													if (parentControlDesigner != null && !arrayList.Contains(parentControlDesigner))
													{
														parentControlDesigner.SuspendChangingEvents();
														arrayList.Add(parentControlDesigner);
														parentControlDesigner.ForceComponentChanging();
													}
												}
												if (!((IOleDragClient)designer).AddComponent(component2, text, firstAdd))
												{
													collection = collection3;
													return;
												}
												Control controlForComponent = ((IOleDragClient)designer).GetControlForComponent(component2);
												if (controlForComponent != null)
												{
													arrayList3.Add(controlForComponent);
												}
												if (TypeDescriptor.GetAttributes(component2).Contains(DesignTimeVisibleAttribute.Yes) || component2 is ToolStripItem)
												{
													arrayList2.Add(component2);
												}
											}
											else if (arrayList4.Contains(component2) && Array.IndexOf<object>(array2, component4) == -1)
											{
												arrayList2.Add(component2);
											}
											Control control = component2 as Control;
											bool flag3 = false;
											if (control != null && text != null && text.Equals(control.Text))
											{
												flag3 = true;
											}
											if (flag3)
											{
												PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component2);
												PropertyDescriptor propertyDescriptor2 = properties["Name"];
												if (propertyDescriptor2 != null && propertyDescriptor2.PropertyType == typeof(string))
												{
													string text3 = (string)propertyDescriptor2.GetValue(component2);
													if (!text3.Equals(text))
													{
														PropertyDescriptor propertyDescriptor3 = properties["Text"];
														if (propertyDescriptor3 != null && propertyDescriptor3.PropertyType == propertyDescriptor2.PropertyType)
														{
															propertyDescriptor3.SetValue(component2, propertyDescriptor2.GetValue(component2));
														}
													}
												}
											}
										}
									}
								}
							}
							ArrayList arrayList5 = new ArrayList();
							foreach (object obj5 in arrayList3)
							{
								Control control2 = (Control)obj5;
								IDesigner designer2 = designerHost.GetDesigner(control2);
								if (designer2 is ControlDesigner)
								{
									arrayList5.Add(control2);
								}
							}
							if (arrayList5.Count > 0)
							{
								this.UpdatePastePositions(arrayList5);
							}
							if (componentTray == null)
							{
								componentTray = (this.GetService(typeof(ComponentTray)) as ComponentTray);
							}
							if (componentTray != null)
							{
								int num3 = componentTray.Controls.Count - num;
								if (num3 > 0)
								{
									ArrayList arrayList6 = new ArrayList();
									for (int i = 0; i < num3; i++)
									{
										arrayList6.Add(componentTray.Controls[num + i]);
									}
									componentTray.UpdatePastePositions(arrayList6);
								}
							}
							arrayList3.Sort(new CommandSet.TabIndexCompare());
							foreach (object obj6 in arrayList3)
							{
								Control control3 = (Control)obj6;
								this.UpdatePasteTabIndex(control3, control3.Parent);
							}
							this.SelectionService.SetSelectedComponents(arrayList2.ToArray(), SelectionTypes.Replace);
							ParentControlDesigner parentControlDesigner2 = designer as ParentControlDesigner;
							if (parentControlDesigner2 != null && parentControlDesigner2.AllowSetChildIndexOnDrop)
							{
								MenuCommand menuCommand = this.MenuService.FindCommand(StandardCommands.BringToFront);
								if (menuCommand != null)
								{
									menuCommand.Invoke();
								}
							}
							designerTransaction.Commit();
						}
					}
				}
			}
			finally
			{
				Cursor.Current = value;
				foreach (object obj7 in arrayList)
				{
					ParentControlDesigner parentControlDesigner3 = (ParentControlDesigner)obj7;
					if (parentControlDesigner3 != null)
					{
						parentControlDesigner3.ResumeChangingEvents();
					}
				}
			}
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x000977A0 File Offset: 0x000959A0
		protected void OnMenuSelectAll(object sender, EventArgs e)
		{
			Cursor value = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				if (this.site != null)
				{
					if (this.SelectionService != null)
					{
						IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
						if (designerHost != null)
						{
							ComponentCollection components = designerHost.Container.Components;
							object[] array2;
							if (components == null || components.Count == 0)
							{
								object[] array = new IComponent[0];
								array2 = array;
							}
							else
							{
								array2 = new object[components.Count - 1];
								object rootComponent = designerHost.RootComponent;
								int num = 0;
								foreach (object obj in components)
								{
									IComponent component = (IComponent)obj;
									if (rootComponent != component)
									{
										array2[num++] = component;
									}
								}
							}
							this.SelectionService.SetSelectedComponents(array2, SelectionTypes.Replace);
						}
					}
				}
			}
			finally
			{
				Cursor.Current = value;
			}
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x000978AC File Offset: 0x00095AAC
		protected void OnMenuShowGrid(object sender, EventArgs e)
		{
			if (this.site != null)
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					DesignerTransaction designerTransaction = null;
					try
					{
						designerTransaction = designerHost.CreateTransaction();
						IComponent rootComponent = designerHost.RootComponent;
						if (rootComponent != null && rootComponent is Control)
						{
							PropertyDescriptor property = this.GetProperty(rootComponent, "DrawGrid");
							if (property != null)
							{
								bool flag = (bool)property.GetValue(rootComponent);
								property.SetValue(rootComponent, !flag);
								((MenuCommand)sender).Checked = !flag;
							}
						}
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
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x00097950 File Offset: 0x00095B50
		protected void OnMenuSizingCommand(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			CommandID commandID = menuCommand.CommandID;
			if (this.SelectionService == null)
			{
				return;
			}
			Cursor value = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				ICollection selectedComponents = this.SelectionService.GetSelectedComponents();
				object[] array = new object[selectedComponents.Count];
				selectedComponents.CopyTo(array, 0);
				array = this.FilterSelection(array, SelectionRules.Visible);
				object obj = this.SelectionService.PrimarySelection;
				Size size = Size.Empty;
				Size size2 = Size.Empty;
				IComponent component = obj as IComponent;
				if (component != null)
				{
					PropertyDescriptor property = this.GetProperty(component, "Size");
					if (property == null)
					{
						return;
					}
					size = (Size)property.GetValue(component);
				}
				if (obj != null)
				{
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					DesignerTransaction designerTransaction = null;
					try
					{
						if (designerHost != null)
						{
							designerTransaction = designerHost.CreateTransaction(SR.GetString("CommandSetSize", new object[]
							{
								array.Length
							}));
						}
						foreach (object obj2 in array)
						{
							if (!obj2.Equals(obj))
							{
								IComponent component2 = obj2 as IComponent;
								if (component2 != null)
								{
									PropertyDescriptor property2 = this.GetProperty(obj2, "Locked");
									if (property2 == null || !(bool)property2.GetValue(obj2))
									{
										PropertyDescriptor property = this.GetProperty(component2, "Size");
										if (property != null && !property.IsReadOnly)
										{
											size2 = (Size)property.GetValue(component2);
											if (commandID == StandardCommands.SizeToControlHeight || commandID == StandardCommands.SizeToControl)
											{
												size2.Height = size.Height;
											}
											if (commandID == StandardCommands.SizeToControlWidth || commandID == StandardCommands.SizeToControl)
											{
												size2.Width = size.Width;
											}
											property.SetValue(component2, size2);
										}
									}
								}
							}
						}
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
			finally
			{
				Cursor.Current = value;
			}
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x00097B78 File Offset: 0x00095D78
		protected void OnMenuSizeToGrid(object sender, EventArgs e)
		{
			if (this.SelectionService == null)
			{
				return;
			}
			Cursor value = Cursor.Current;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			DesignerTransaction designerTransaction = null;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				ICollection selectedComponents = this.SelectionService.GetSelectedComponents();
				object[] array = new object[selectedComponents.Count];
				selectedComponents.CopyTo(array, 0);
				array = this.FilterSelection(array, SelectionRules.Visible);
				Size size = Size.Empty;
				Point point = Point.Empty;
				Size size2 = Size.Empty;
				if (designerHost != null)
				{
					designerTransaction = designerHost.CreateTransaction(SR.GetString("CommandSetSizeToGrid", new object[]
					{
						array.Length
					}));
					IComponent rootComponent = designerHost.RootComponent;
					if (rootComponent != null && rootComponent is Control)
					{
						PropertyDescriptor property = this.GetProperty(rootComponent, "CurrentGridSize");
						if (property != null)
						{
							size2 = (Size)property.GetValue(rootComponent);
						}
					}
				}
				if (!size2.IsEmpty)
				{
					foreach (object obj in array)
					{
						IComponent component = obj as IComponent;
						if (obj != null)
						{
							PropertyDescriptor property2 = this.GetProperty(component, "Size");
							PropertyDescriptor property3 = this.GetProperty(component, "Location");
							if (property2 != null && property3 != null && !property2.IsReadOnly && !property3.IsReadOnly)
							{
								size = (Size)property2.GetValue(component);
								point = (Point)property3.GetValue(component);
								size.Width = (size.Width + size2.Width / 2) / size2.Width * size2.Width;
								size.Height = (size.Height + size2.Height / 2) / size2.Height * size2.Height;
								point.X = point.X / size2.Width * size2.Width;
								point.Y = point.Y / size2.Height * size2.Height;
								property2.SetValue(component, size);
								property3.SetValue(component, point);
							}
						}
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
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x00097DE0 File Offset: 0x00095FE0
		protected void OnMenuDesignerProperties(object sender, EventArgs e)
		{
			object obj = this.SelectionService.PrimarySelection;
			if (this.CheckComponentEditor(obj, true))
			{
				return;
			}
			IMenuCommandService menuCommandService = (IMenuCommandService)this.GetService(typeof(IMenuCommandService));
			if (menuCommandService != null)
			{
				menuCommandService.GlobalInvoke(StandardCommands.PropertiesWindow);
				return;
			}
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x00097E2C File Offset: 0x0009602C
		protected void OnMenuSnapToGrid(object sender, EventArgs e)
		{
			if (this.site != null)
			{
				IDesignerHost designerHost = (IDesignerHost)this.site.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					DesignerTransaction designerTransaction = null;
					try
					{
						designerTransaction = designerHost.CreateTransaction(SR.GetString("CommandSetPaste", new object[]
						{
							0
						}));
						IComponent rootComponent = designerHost.RootComponent;
						if (rootComponent != null && rootComponent is Control)
						{
							PropertyDescriptor property = this.GetProperty(rootComponent, "SnapToGrid");
							if (property != null)
							{
								bool flag = (bool)property.GetValue(rootComponent);
								property.SetValue(rootComponent, !flag);
								((MenuCommand)sender).Checked = !flag;
							}
						}
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
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00097EF4 File Offset: 0x000960F4
		protected void OnMenuSpacingCommand(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			CommandID commandID = menuCommand.CommandID;
			DesignerTransaction designerTransaction = null;
			if (this.SelectionService == null)
			{
				return;
			}
			Cursor value = Cursor.Current;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				Size size = Size.Empty;
				ICollection selectedComponents = this.SelectionService.GetSelectedComponents();
				object[] array = new object[selectedComponents.Count];
				selectedComponents.CopyTo(array, 0);
				if (designerHost != null)
				{
					designerTransaction = designerHost.CreateTransaction(SR.GetString("CommandSetFormatSpacing", new object[]
					{
						array.Length
					}));
					IComponent rootComponent = designerHost.RootComponent;
					if (rootComponent != null && rootComponent is Control)
					{
						PropertyDescriptor property = this.GetProperty(rootComponent, "CurrentGridSize");
						if (property != null)
						{
							size = (Size)property.GetValue(rootComponent);
						}
					}
				}
				array = this.FilterSelection(array, SelectionRules.Visible);
				int num = 0;
				PropertyDescriptor propertyDescriptor = null;
				PropertyDescriptor propertyDescriptor2 = null;
				PropertyDescriptor propertyDescriptor3 = null;
				PropertyDescriptor propertyDescriptor4 = null;
				Size size2 = Size.Empty;
				Size size3 = Size.Empty;
				Point point = Point.Empty;
				Point point2 = Point.Empty;
				Point point3 = Point.Empty;
				int num2;
				if (commandID == StandardCommands.HorizSpaceConcatenate || commandID == StandardCommands.HorizSpaceDecrease || commandID == StandardCommands.HorizSpaceIncrease || commandID == StandardCommands.HorizSpaceMakeEqual)
				{
					num2 = 0;
				}
				else
				{
					if (commandID != StandardCommands.VertSpaceConcatenate && commandID != StandardCommands.VertSpaceDecrease && commandID != StandardCommands.VertSpaceIncrease && commandID != StandardCommands.VertSpaceMakeEqual)
					{
						throw new ArgumentException(SR.GetString("CommandSetUnknownSpacingCommand"));
					}
					num2 = 1;
				}
				this.SortSelection(array, num2);
				object obj = this.SelectionService.PrimarySelection;
				int num3 = 0;
				if (obj != null)
				{
					num3 = Array.IndexOf<object>(array, obj);
				}
				IComponent component3;
				if (commandID == StandardCommands.HorizSpaceMakeEqual || commandID == StandardCommands.VertSpaceMakeEqual)
				{
					int num4 = 0;
					for (int i = 0; i < array.Length; i++)
					{
						size2 = Size.Empty;
						IComponent component = array[i] as IComponent;
						if (component != null)
						{
							IComponent component2 = component;
							propertyDescriptor = this.GetProperty(component2, "Size");
							if (propertyDescriptor != null)
							{
								size2 = (Size)propertyDescriptor.GetValue(component2);
							}
						}
						if (num2 == 0)
						{
							num4 += size2.Width;
						}
						else
						{
							num4 += size2.Height;
						}
					}
					component3 = null;
					size2 = Size.Empty;
					point = Point.Empty;
					for (int i = 0; i < array.Length; i++)
					{
						IComponent component2 = array[i] as IComponent;
						if (component2 != null)
						{
							if (component3 == null || component2.GetType() != component3.GetType())
							{
								propertyDescriptor = this.GetProperty(component2, "Size");
								propertyDescriptor3 = this.GetProperty(component2, "Location");
							}
							component3 = component2;
							if (propertyDescriptor3 != null)
							{
								point = (Point)propertyDescriptor3.GetValue(component2);
								if (propertyDescriptor != null && !((Size)propertyDescriptor.GetValue(component2)).IsEmpty && !point.IsEmpty)
								{
									break;
								}
							}
						}
					}
					for (int i = array.Length - 1; i >= 0; i--)
					{
						IComponent component2 = array[i] as IComponent;
						if (component2 != null)
						{
							if (component3 == null || component2.GetType() != component3.GetType())
							{
								propertyDescriptor = this.GetProperty(component2, "Size");
								propertyDescriptor3 = this.GetProperty(component2, "Location");
							}
							component3 = component2;
							if (propertyDescriptor3 != null)
							{
								point2 = (Point)propertyDescriptor3.GetValue(component2);
								if (propertyDescriptor != null)
								{
									size3 = (Size)propertyDescriptor.GetValue(component2);
									if (propertyDescriptor != null && propertyDescriptor3 != null)
									{
										break;
									}
								}
							}
						}
					}
					if (propertyDescriptor != null && propertyDescriptor3 != null)
					{
						if (num2 == 0)
						{
							num = (size3.Width + point2.X - point.X - num4) / (array.Length - 1);
						}
						else
						{
							num = (size3.Height + point2.Y - point.Y - num4) / (array.Length - 1);
						}
						if (num < 0)
						{
							num = 0;
						}
					}
				}
				component3 = null;
				if (obj != null)
				{
					PropertyDescriptor property2 = this.GetProperty(obj, "Location");
					if (property2 != null)
					{
						point3 = (Point)property2.GetValue(obj);
					}
				}
				for (int j = 0; j < array.Length; j++)
				{
					IComponent component2 = (IComponent)array[j];
					PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component2);
					PropertyDescriptor propertyDescriptor5 = properties["Locked"];
					if (propertyDescriptor5 == null || !(bool)propertyDescriptor5.GetValue(component2))
					{
						if (component3 == null || component3.GetType() != component2.GetType())
						{
							propertyDescriptor = properties["Size"];
							propertyDescriptor3 = properties["Location"];
						}
						else
						{
							propertyDescriptor = propertyDescriptor2;
							propertyDescriptor3 = propertyDescriptor4;
						}
						if (propertyDescriptor3 != null)
						{
							point = (Point)propertyDescriptor3.GetValue(component2);
							if (propertyDescriptor != null)
							{
								size2 = (Size)propertyDescriptor.GetValue(component2);
								int num5 = Math.Max(0, j - 1);
								component3 = (IComponent)array[num5];
								if (component3.GetType() != component2.GetType())
								{
									propertyDescriptor2 = this.GetProperty(component3, "Size");
									propertyDescriptor4 = this.GetProperty(component3, "Location");
								}
								else
								{
									propertyDescriptor2 = propertyDescriptor;
									propertyDescriptor4 = propertyDescriptor3;
								}
								if (propertyDescriptor4 != null)
								{
									point2 = (Point)propertyDescriptor4.GetValue(component3);
									if (propertyDescriptor2 != null)
									{
										size3 = (Size)propertyDescriptor2.GetValue(component3);
										if (commandID == StandardCommands.HorizSpaceConcatenate && j > 0)
										{
											point.X = point2.X + size3.Width;
										}
										else if (commandID == StandardCommands.HorizSpaceDecrease)
										{
											if (num3 < j)
											{
												point.X -= size.Width * (j - num3);
												if (point.X < point3.X)
												{
													point.X = point3.X;
												}
											}
											else if (num3 > j)
											{
												point.X += size.Width * (num3 - j);
												if (point.X > point3.X)
												{
													point.X = point3.X;
												}
											}
										}
										else if (commandID == StandardCommands.HorizSpaceIncrease)
										{
											if (num3 < j)
											{
												point.X += size.Width * (j - num3);
											}
											else if (num3 > j)
											{
												point.X -= size.Width * (num3 - j);
											}
										}
										else if (commandID == StandardCommands.HorizSpaceMakeEqual && j > 0)
										{
											point.X = point2.X + size3.Width + num;
										}
										else if (commandID == StandardCommands.VertSpaceConcatenate && j > 0)
										{
											point.Y = point2.Y + size3.Height;
										}
										else if (commandID == StandardCommands.VertSpaceDecrease)
										{
											if (num3 < j)
											{
												point.Y -= size.Height * (j - num3);
												if (point.Y < point3.Y)
												{
													point.Y = point3.Y;
												}
											}
											else if (num3 > j)
											{
												point.Y += size.Height * (num3 - j);
												if (point.Y > point3.Y)
												{
													point.Y = point3.Y;
												}
											}
										}
										else if (commandID == StandardCommands.VertSpaceIncrease)
										{
											if (num3 < j)
											{
												point.Y += size.Height * (j - num3);
											}
											else if (num3 > j)
											{
												point.Y -= size.Height * (num3 - j);
											}
										}
										else if (commandID == StandardCommands.VertSpaceMakeEqual && j > 0)
										{
											point.Y = point2.Y + size3.Height + num;
										}
										if (!propertyDescriptor3.IsReadOnly)
										{
											propertyDescriptor3.SetValue(component2, point);
										}
										component3 = component2;
									}
								}
							}
						}
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
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x000986FC File Offset: 0x000968FC
		protected void OnSelectionChanged(object sender, EventArgs e)
		{
			if (this.SelectionService == null)
			{
				return;
			}
			this.selectionVersion++;
			this.selCount = this.SelectionService.SelectionCount;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (this.selCount > 0 && designerHost != null)
			{
				object rootComponent = designerHost.RootComponent;
				if (rootComponent != null && this.SelectionService.GetComponentSelected(rootComponent))
				{
					this.selCount = 0;
				}
			}
			this.primarySelection = (this.SelectionService.PrimarySelection as IComponent);
			this.selectionInherited = false;
			this.controlsOnlySelection = true;
			if (this.selCount > 0)
			{
				ICollection selectedComponents = this.SelectionService.GetSelectedComponents();
				foreach (object obj in selectedComponents)
				{
					if (!(obj is Control))
					{
						this.controlsOnlySelection = false;
					}
					if (!TypeDescriptor.GetAttributes(obj)[typeof(InheritanceAttribute)].Equals(InheritanceAttribute.NotInherited))
					{
						this.selectionInherited = true;
						break;
					}
				}
			}
			this.OnUpdateCommandStatus();
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x0009882C File Offset: 0x00096A2C
		private void OnSnapLineTimerExpire(object sender, EventArgs e)
		{
			Control adornerWindowControl = this.BehaviorService.AdornerWindowControl;
			if (adornerWindowControl != null && adornerWindowControl.IsHandleCreated)
			{
				adornerWindowControl.BeginInvoke(new EventHandler(this.OnSnapLineTimerExpireMarshalled), new object[]
				{
					sender,
					e
				});
			}
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x00098871 File Offset: 0x00096A71
		private void OnSnapLineTimerExpireMarshalled(object sender, EventArgs e)
		{
			this.snapLineTimer.Stop();
			this.EndDragManager();
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00098884 File Offset: 0x00096A84
		protected void OnStatusAlways(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			menuCommand.Enabled = true;
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x000988A0 File Offset: 0x00096AA0
		protected void OnStatusAnySelection(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			menuCommand.Enabled = (this.selCount > 0);
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x000988C4 File Offset: 0x00096AC4
		protected void OnStatusCopy(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			bool enabled = false;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (!this.selectionInherited && designerHost != null && !designerHost.Loading)
			{
				ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
				if (selectionService != null)
				{
					ICollection selectedComponents = selectionService.GetSelectedComponents();
					object rootComponent = designerHost.RootComponent;
					if (!selectionService.GetComponentSelected(rootComponent))
					{
						foreach (object obj in selectedComponents)
						{
							IComponent component = obj as IComponent;
							if (component != null && component.Site != null && component.Site.Container == designerHost.Container)
							{
								enabled = true;
								break;
							}
						}
					}
				}
			}
			menuCommand.Enabled = enabled;
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x000989B8 File Offset: 0x00096BB8
		protected void OnStatusCut(object sender, EventArgs e)
		{
			this.OnStatusDelete(sender, e);
			if (((MenuCommand)sender).Enabled)
			{
				this.OnStatusCopy(sender, e);
			}
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x000989D8 File Offset: 0x00096BD8
		protected void OnStatusDelete(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			if (this.selectionInherited)
			{
				menuCommand.Enabled = false;
				return;
			}
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
				if (selectionService != null)
				{
					ICollection selectedComponents = selectionService.GetSelectedComponents();
					foreach (object obj in selectedComponents)
					{
						IComponent component = obj as IComponent;
						if (component != null && (component.Site == null || (component.Site != null && component.Site.Container != designerHost.Container)))
						{
							menuCommand.Enabled = false;
							return;
						}
					}
				}
			}
			this.OnStatusAnySelection(sender, e);
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x00098AC0 File Offset: 0x00096CC0
		protected void OnStatusPaste(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (this.primarySelection != null && designerHost != null && designerHost.GetDesigner(this.primarySelection) is ParentControlDesigner)
			{
				InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(this.primarySelection)[typeof(InheritanceAttribute)];
				if (inheritanceAttribute.InheritanceLevel == InheritanceLevel.InheritedReadOnly)
				{
					menuCommand.Enabled = false;
					return;
				}
			}
			IDataObject dataObject = Clipboard.GetDataObject();
			bool enabled = false;
			if (dataObject != null)
			{
				if (dataObject.GetDataPresent("CF_DESIGNERCOMPONENTS_V2"))
				{
					enabled = true;
				}
				else
				{
					IToolboxService toolboxService = (IToolboxService)this.GetService(typeof(IToolboxService));
					if (toolboxService != null)
					{
						enabled = ((designerHost != null) ? toolboxService.IsSupported(dataObject, designerHost) : toolboxService.IsToolboxItem(dataObject));
					}
				}
			}
			menuCommand.Enabled = enabled;
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x00098B90 File Offset: 0x00096D90
		private void OnStatusPrimarySelection(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			menuCommand.Enabled = (this.primarySelection != null);
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x00098BB4 File Offset: 0x00096DB4
		protected virtual void OnStatusSelectAll(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			menuCommand.Enabled = (designerHost.Container.Components.Count > 1);
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x00098BF8 File Offset: 0x00096DF8
		protected virtual void OnUpdateCommandStatus()
		{
			for (int i = 0; i < this.commandSet.Length; i++)
			{
				this.commandSet[i].UpdateStatus();
			}
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x00098C28 File Offset: 0x00096E28
		private ICollection PrependComponentNames(ICollection objects)
		{
			object[] array = new object[objects.Count + 1];
			int num = 1;
			ArrayList arrayList = new ArrayList(objects.Count);
			foreach (object obj in objects)
			{
				IComponent component = obj as IComponent;
				if (component != null)
				{
					string value = null;
					if (component.Site != null)
					{
						value = component.Site.Name;
					}
					arrayList.Add(value);
				}
				array[num++] = obj;
			}
			string[] array2 = new string[arrayList.Count];
			arrayList.CopyTo(array2, 0);
			array[0] = array2;
			return array;
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x00098CE8 File Offset: 0x00096EE8
		private void SortSelection(object[] selectedObjects, int nSortBy)
		{
			IComparer comparer;
			switch (nSortBy)
			{
			case 0:
				comparer = new CommandSet.ComponentLeftCompare();
				break;
			case 1:
				comparer = new CommandSet.ComponentTopCompare();
				break;
			case 2:
				comparer = new CommandSet.ControlZOrderCompare();
				break;
			default:
				return;
			}
			Array.Sort(selectedObjects, comparer);
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x00098D28 File Offset: 0x00096F28
		private void UpdateClipboardItems(object s, EventArgs e)
		{
			int num = 0;
			int num2 = 0;
			while (num < 3 && num2 < this.commandSet.Length)
			{
				CommandSet.CommandSetItem commandSetItem = this.commandSet[num2];
				if (commandSetItem.CommandID == StandardCommands.Paste || commandSetItem.CommandID == StandardCommands.Copy || commandSetItem.CommandID == StandardCommands.Cut)
				{
					num++;
					commandSetItem.UpdateStatus();
				}
				num2++;
			}
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x00098D88 File Offset: 0x00096F88
		private void UpdatePastePositions(ArrayList controls)
		{
			if (controls.Count == 0)
			{
				return;
			}
			Control parent = ((Control)controls[0]).Parent;
			Point location = ((Control)controls[0]).Location;
			Point point = location;
			foreach (object obj in controls)
			{
				Control control = (Control)obj;
				Point location2 = control.Location;
				Size size = control.Size;
				if (location.X > location2.X)
				{
					location.X = location2.X;
				}
				if (location.Y > location2.Y)
				{
					location.Y = location2.Y;
				}
				if (point.X < location2.X + size.Width)
				{
					point.X = location2.X + size.Width;
				}
				if (point.Y < location2.Y + size.Height)
				{
					point.Y = location2.Y + size.Height;
				}
			}
			Point pos = new Point(-location.X, -location.Y);
			if (parent != null)
			{
				bool flag = false;
				Size clientSize = parent.ClientSize;
				Size sz = Size.Empty;
				Point point2 = new Point(clientSize.Width / 2, clientSize.Height / 2);
				point2.X -= (point.X - location.X) / 2;
				point2.Y -= (point.Y - location.Y) / 2;
				bool flag2;
				do
				{
					flag2 = false;
					foreach (object obj2 in parent.Controls)
					{
						Control control2 = (Control)obj2;
						Rectangle bounds = control2.Bounds;
						if (controls.Contains(control2))
						{
							if (!control2.Size.Equals(clientSize))
							{
								continue;
							}
							bounds.Offset(pos);
						}
						Control control3 = (Control)controls[0];
						Rectangle bounds2 = control3.Bounds;
						bounds2.Offset(pos);
						bounds2.Offset(point2);
						if (bounds2.Equals(bounds))
						{
							flag2 = true;
							if (sz.IsEmpty)
							{
								IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
								IComponent rootComponent = designerHost.RootComponent;
								if (rootComponent != null && rootComponent is Control)
								{
									PropertyDescriptor property = this.GetProperty(rootComponent, "GridSize");
									if (property != null)
									{
										sz = (Size)property.GetValue(rootComponent);
									}
								}
								if (sz.IsEmpty)
								{
									sz.Width = 8;
									sz.Height = 8;
								}
							}
							point2 += sz;
							int num;
							int num2;
							if (controls.Count > 1)
							{
								num = point2.X + point.X - location.X;
								num2 = point2.Y + point.Y - location.Y;
							}
							else
							{
								num = point2.X + sz.Width;
								num2 = point2.Y + sz.Height;
							}
							if (num <= clientSize.Width && num2 <= clientSize.Height)
							{
								break;
							}
							point2.X = 0;
							point2.Y = 0;
							if (flag)
							{
								flag2 = false;
								break;
							}
							flag = true;
							break;
						}
					}
				}
				while (flag2);
				pos.Offset(point2.X, point2.Y);
			}
			if (parent != null)
			{
				parent.SuspendLayout();
			}
			try
			{
				foreach (object obj3 in controls)
				{
					Control control4 = (Control)obj3;
					Point location3 = control4.Location;
					location3.Offset(pos.X, pos.Y);
					control4.Location = location3;
				}
			}
			finally
			{
				if (parent != null)
				{
					parent.ResumeLayout();
				}
			}
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x000991FC File Offset: 0x000973FC
		private void UpdatePasteTabIndex(Control componentControl, object parentComponent)
		{
			Control control = parentComponent as Control;
			if (control == null || componentControl == null)
			{
				return;
			}
			bool flag = false;
			int tabIndex = componentControl.TabIndex;
			int num = 0;
			foreach (object obj in control.Controls)
			{
				Control control2 = (Control)obj;
				int tabIndex2 = control2.TabIndex;
				if (num <= tabIndex2)
				{
					num = tabIndex2 + 1;
				}
				if (tabIndex2 == tabIndex)
				{
					flag = true;
				}
			}
			if (flag)
			{
				componentControl.TabIndex = num;
			}
		}

		// Token: 0x040015CB RID: 5579
		protected ISite site;

		// Token: 0x040015CC RID: 5580
		private CommandSet.CommandSetItem[] commandSet;

		// Token: 0x040015CD RID: 5581
		private IMenuCommandService menuService;

		// Token: 0x040015CE RID: 5582
		private IEventHandlerService eventService;

		// Token: 0x040015CF RID: 5583
		private ISelectionService selectionService;

		// Token: 0x040015D0 RID: 5584
		protected int selCount;

		// Token: 0x040015D1 RID: 5585
		protected IComponent primarySelection;

		// Token: 0x040015D2 RID: 5586
		private bool selectionInherited;

		// Token: 0x040015D3 RID: 5587
		protected bool controlsOnlySelection;

		// Token: 0x040015D4 RID: 5588
		private int selectionVersion = 1;

		// Token: 0x040015D5 RID: 5589
		private const int SORT_HORIZONTAL = 0;

		// Token: 0x040015D6 RID: 5590
		private const int SORT_VERTICAL = 1;

		// Token: 0x040015D7 RID: 5591
		private const int SORT_ZORDER = 2;

		// Token: 0x040015D8 RID: 5592
		private const string CF_DESIGNER = "CF_DESIGNERCOMPONENTS_V2";

		// Token: 0x040015D9 RID: 5593
		protected DragAssistanceManager dragManager;

		// Token: 0x040015DA RID: 5594
		private Timer snapLineTimer;

		// Token: 0x040015DB RID: 5595
		private BehaviorService behaviorService;

		// Token: 0x040015DC RID: 5596
		private StatusCommandUI statusCommandUI;

		// Token: 0x02000531 RID: 1329
		protected class CommandSetItem : MenuCommand
		{
			// Token: 0x06003073 RID: 12403 RVA: 0x0010A2FA File Offset: 0x001084FA
			public CommandSetItem(CommandSet commandSet, EventHandler statusHandler, EventHandler invokeHandler, CommandID id, IUIService uiService) : this(commandSet, statusHandler, invokeHandler, id, false, uiService)
			{
			}

			// Token: 0x06003074 RID: 12404 RVA: 0x0010A30A File Offset: 0x0010850A
			public CommandSetItem(CommandSet commandSet, EventHandler statusHandler, EventHandler invokeHandler, CommandID id) : this(commandSet, statusHandler, invokeHandler, id, false, null)
			{
			}

			// Token: 0x06003075 RID: 12405 RVA: 0x0010A319 File Offset: 0x00108519
			public CommandSetItem(CommandSet commandSet, EventHandler statusHandler, EventHandler invokeHandler, CommandID id, bool optimizeStatus) : this(commandSet, statusHandler, invokeHandler, id, optimizeStatus, null)
			{
			}

			// Token: 0x06003076 RID: 12406 RVA: 0x0010A32C File Offset: 0x0010852C
			public CommandSetItem(CommandSet commandSet, EventHandler statusHandler, EventHandler invokeHandler, CommandID id, bool optimizeStatus, IUIService uiService) : base(invokeHandler, id)
			{
				this.uiService = uiService;
				this.eventService = commandSet.eventService;
				this.statusHandler = statusHandler;
				if (optimizeStatus && statusHandler != null)
				{
					this.commandSet = commandSet;
					Type typeFromHandle = typeof(CommandSet.CommandSetItem);
					lock (typeFromHandle)
					{
						if (CommandSet.CommandSetItem.commandStatusHash == null)
						{
							CommandSet.CommandSetItem.commandStatusHash = new Hashtable();
						}
					}
					CommandSet.CommandSetItem.StatusState statusState = CommandSet.CommandSetItem.commandStatusHash[statusHandler] as CommandSet.CommandSetItem.StatusState;
					if (statusState == null)
					{
						statusState = new CommandSet.CommandSetItem.StatusState();
						CommandSet.CommandSetItem.commandStatusHash.Add(statusHandler, statusState);
					}
					statusState.refCount++;
				}
			}

			// Token: 0x17000966 RID: 2406
			// (get) Token: 0x06003077 RID: 12407 RVA: 0x0010A3E4 File Offset: 0x001085E4
			private bool CommandStatusValid
			{
				get
				{
					if (this.commandSet != null && CommandSet.CommandSetItem.commandStatusHash.Contains(this.statusHandler))
					{
						CommandSet.CommandSetItem.StatusState statusState = CommandSet.CommandSetItem.commandStatusHash[this.statusHandler] as CommandSet.CommandSetItem.StatusState;
						if (statusState != null && statusState.SelectionVersion == this.commandSet.SelectionVersion)
						{
							return true;
						}
					}
					return false;
				}
			}

			// Token: 0x06003078 RID: 12408 RVA: 0x0010A43C File Offset: 0x0010863C
			private void ApplyCachedStatus()
			{
				if (this.commandSet != null && CommandSet.CommandSetItem.commandStatusHash.Contains(this.statusHandler))
				{
					try
					{
						this.updatingCommand = true;
						CommandSet.CommandSetItem.StatusState statusState = CommandSet.CommandSetItem.commandStatusHash[this.statusHandler] as CommandSet.CommandSetItem.StatusState;
						statusState.ApplyState(this);
					}
					finally
					{
						this.updatingCommand = false;
					}
				}
			}

			// Token: 0x06003079 RID: 12409 RVA: 0x0010A4A4 File Offset: 0x001086A4
			public override void Invoke()
			{
				try
				{
					if (this.eventService != null)
					{
						IMenuStatusHandler menuStatusHandler = (IMenuStatusHandler)this.eventService.GetHandler(typeof(IMenuStatusHandler));
						if (menuStatusHandler != null && menuStatusHandler.OverrideInvoke(this))
						{
							return;
						}
					}
					base.Invoke();
				}
				catch (Exception ex)
				{
					if (this.uiService != null)
					{
						this.uiService.ShowError(ex, SR.GetString("CommandSetError", new object[]
						{
							ex.Message
						}));
					}
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
					}
				}
			}

			// Token: 0x0600307A RID: 12410 RVA: 0x0010A534 File Offset: 0x00108734
			protected override void OnCommandChanged(EventArgs e)
			{
				if (!this.updatingCommand)
				{
					base.OnCommandChanged(e);
				}
			}

			// Token: 0x0600307B RID: 12411 RVA: 0x0010A548 File Offset: 0x00108748
			private void SaveCommandStatus()
			{
				if (this.commandSet != null)
				{
					CommandSet.CommandSetItem.StatusState statusState;
					if (CommandSet.CommandSetItem.commandStatusHash.Contains(this.statusHandler))
					{
						statusState = (CommandSet.CommandSetItem.commandStatusHash[this.statusHandler] as CommandSet.CommandSetItem.StatusState);
					}
					else
					{
						statusState = new CommandSet.CommandSetItem.StatusState();
					}
					statusState.SaveState(this, this.commandSet.SelectionVersion);
				}
			}

			// Token: 0x0600307C RID: 12412 RVA: 0x0010A5A4 File Offset: 0x001087A4
			public void UpdateStatus()
			{
				if (this.eventService != null)
				{
					IMenuStatusHandler menuStatusHandler = (IMenuStatusHandler)this.eventService.GetHandler(typeof(IMenuStatusHandler));
					if (menuStatusHandler != null && menuStatusHandler.OverrideStatus(this))
					{
						return;
					}
				}
				if (this.statusHandler != null)
				{
					if (!this.CommandStatusValid)
					{
						try
						{
							this.statusHandler(this, EventArgs.Empty);
							this.SaveCommandStatus();
							return;
						}
						catch
						{
							return;
						}
					}
					this.ApplyCachedStatus();
				}
			}

			// Token: 0x0600307D RID: 12413 RVA: 0x0010A624 File Offset: 0x00108824
			public virtual void Dispose()
			{
				CommandSet.CommandSetItem.StatusState statusState = CommandSet.CommandSetItem.commandStatusHash[this.statusHandler] as CommandSet.CommandSetItem.StatusState;
				if (statusState != null)
				{
					statusState.refCount--;
					if (statusState.refCount == 0)
					{
						CommandSet.CommandSetItem.commandStatusHash.Remove(this.statusHandler);
					}
				}
			}

			// Token: 0x040020ED RID: 8429
			private EventHandler statusHandler;

			// Token: 0x040020EE RID: 8430
			private IEventHandlerService eventService;

			// Token: 0x040020EF RID: 8431
			private IUIService uiService;

			// Token: 0x040020F0 RID: 8432
			private CommandSet commandSet;

			// Token: 0x040020F1 RID: 8433
			private static Hashtable commandStatusHash;

			// Token: 0x040020F2 RID: 8434
			private bool updatingCommand;

			// Token: 0x020005E9 RID: 1513
			private class StatusState
			{
				// Token: 0x17000A32 RID: 2610
				// (get) Token: 0x060034CC RID: 13516 RVA: 0x0011ED2F File Offset: 0x0011CF2F
				public int SelectionVersion
				{
					get
					{
						return this.selectionVersion;
					}
				}

				// Token: 0x060034CD RID: 13517 RVA: 0x0011ED38 File Offset: 0x0011CF38
				internal void ApplyState(CommandSet.CommandSetItem item)
				{
					item.Enabled = ((this.statusFlags & 1) == 1);
					item.Visible = ((this.statusFlags & 2) == 2);
					item.Checked = ((this.statusFlags & 4) == 4);
					item.Supported = ((this.statusFlags & 8) == 8);
				}

				// Token: 0x060034CE RID: 13518 RVA: 0x0011ED8C File Offset: 0x0011CF8C
				internal void SaveState(CommandSet.CommandSetItem item, int version)
				{
					this.selectionVersion = version;
					this.statusFlags = 0;
					if (item.Enabled)
					{
						this.statusFlags |= 1;
					}
					if (item.Visible)
					{
						this.statusFlags |= 2;
					}
					if (item.Checked)
					{
						this.statusFlags |= 4;
					}
					if (item.Supported)
					{
						this.statusFlags |= 8;
					}
				}

				// Token: 0x04002334 RID: 9012
				private const int Enabled = 1;

				// Token: 0x04002335 RID: 9013
				private const int Visible = 2;

				// Token: 0x04002336 RID: 9014
				private const int Checked = 4;

				// Token: 0x04002337 RID: 9015
				private const int Supported = 8;

				// Token: 0x04002338 RID: 9016
				private const int NeedsUpdate = 16;

				// Token: 0x04002339 RID: 9017
				private int selectionVersion;

				// Token: 0x0400233A RID: 9018
				private int statusFlags = 16;

				// Token: 0x0400233B RID: 9019
				internal int refCount;
			}
		}

		// Token: 0x02000532 RID: 1330
		protected class ImmediateCommandSetItem : CommandSet.CommandSetItem
		{
			// Token: 0x0600307E RID: 12414 RVA: 0x0010A670 File Offset: 0x00108870
			public ImmediateCommandSetItem(CommandSet commandSet, EventHandler statusHandler, EventHandler invokeHandler, CommandID id, IUIService uiService) : base(commandSet, statusHandler, invokeHandler, id, uiService)
			{
			}

			// Token: 0x17000967 RID: 2407
			// (get) Token: 0x0600307F RID: 12415 RVA: 0x0010A67F File Offset: 0x0010887F
			public override int OleStatus
			{
				get
				{
					base.UpdateStatus();
					return base.OleStatus;
				}
			}
		}

		// Token: 0x02000533 RID: 1331
		private class ComponentLeftCompare : IComparer
		{
			// Token: 0x06003080 RID: 12416 RVA: 0x0010A690 File Offset: 0x00108890
			public int Compare(object p, object q)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(p)["Location"];
				PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(q)["Location"];
				Point point = (Point)propertyDescriptor.GetValue(p);
				Point point2 = (Point)propertyDescriptor2.GetValue(q);
				if (point.X == point2.X)
				{
					return point.Y - point2.Y;
				}
				return point.X - point2.X;
			}
		}

		// Token: 0x02000534 RID: 1332
		private class ComponentTopCompare : IComparer
		{
			// Token: 0x06003082 RID: 12418 RVA: 0x0010A708 File Offset: 0x00108908
			public int Compare(object p, object q)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(p)["Location"];
				PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(q)["Location"];
				Point point = (Point)propertyDescriptor.GetValue(p);
				Point point2 = (Point)propertyDescriptor2.GetValue(q);
				if (point.Y == point2.Y)
				{
					return point.X - point2.X;
				}
				return point.Y - point2.Y;
			}
		}

		// Token: 0x02000535 RID: 1333
		private class ControlZOrderCompare : IComparer
		{
			// Token: 0x06003084 RID: 12420 RVA: 0x0010A780 File Offset: 0x00108980
			public int Compare(object p, object q)
			{
				if (p == null)
				{
					return -1;
				}
				if (q == null)
				{
					return 1;
				}
				if (p == q)
				{
					return 0;
				}
				Control control = p as Control;
				Control control2 = q as Control;
				if (control == null || control2 == null)
				{
					return 1;
				}
				if (control.Parent == control2.Parent && control.Parent != null)
				{
					return control.Parent.Controls.GetChildIndex(control) - control.Parent.Controls.GetChildIndex(control2);
				}
				return 1;
			}
		}

		// Token: 0x02000536 RID: 1334
		private class TabIndexCompare : IComparer
		{
			// Token: 0x06003086 RID: 12422 RVA: 0x0010A7F0 File Offset: 0x001089F0
			public int Compare(object p, object q)
			{
				Control control = p as Control;
				Control control2 = q as Control;
				if (control == control2)
				{
					return 0;
				}
				if (control == null)
				{
					return -1;
				}
				if (control2 == null)
				{
					return 1;
				}
				return control.TabIndex - control2.TabIndex;
			}
		}
	}
}
