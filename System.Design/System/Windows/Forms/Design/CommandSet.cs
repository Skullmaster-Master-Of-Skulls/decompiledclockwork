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
	// Token: 0x020001AD RID: 429
	internal class CommandSet : IDisposable
	{
		// Token: 0x06001068 RID: 4200 RVA: 0x0004ADD4 File Offset: 0x00049DD4
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
			this.commandSet = new CommandSet.CommandSetItem[]
			{
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusDelete), new EventHandler(this.OnMenuDelete), StandardCommands.Delete, uiService),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusCopy), new EventHandler(this.OnMenuCopy), StandardCommands.Copy, uiService),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusCut), new EventHandler(this.OnMenuCut), StandardCommands.Cut, uiService),
				new CommandSet.ImmediateCommandSetItem(this, new EventHandler(this.OnStatusPaste), new EventHandler(this.OnMenuPaste), StandardCommands.Paste, uiService),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusSelectAll), new EventHandler(this.OnMenuSelectAll), StandardCommands.SelectAll, true, uiService),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusAlways), new EventHandler(this.OnMenuDesignerProperties), MenuCommands.DesignerProperties, uiService),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusAlways), new EventHandler(this.OnKeyCancel), MenuCommands.KeyCancel, uiService),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusAlways), new EventHandler(this.OnKeyCancel), MenuCommands.KeyReverseCancel, uiService),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusPrimarySelection), new EventHandler(this.OnKeyDefault), MenuCommands.KeyDefaultAction, true, uiService),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusAnySelection), new EventHandler(this.OnKeyMove), MenuCommands.KeyMoveUp, true, uiService),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusAnySelection), new EventHandler(this.OnKeyMove), MenuCommands.KeyMoveDown, true, uiService),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusAnySelection), new EventHandler(this.OnKeyMove), MenuCommands.KeyMoveLeft, true, uiService),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusAnySelection), new EventHandler(this.OnKeyMove), MenuCommands.KeyMoveRight, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusAnySelection), new EventHandler(this.OnKeyMove), MenuCommands.KeyNudgeUp, true, uiService),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusAnySelection), new EventHandler(this.OnKeyMove), MenuCommands.KeyNudgeDown, true, uiService),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusAnySelection), new EventHandler(this.OnKeyMove), MenuCommands.KeyNudgeLeft, true, uiService),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusAnySelection), new EventHandler(this.OnKeyMove), MenuCommands.KeyNudgeRight, true, uiService)
			};
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

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x0004B203 File Offset: 0x0004A203
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

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x0004B22E File Offset: 0x0004A22E
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

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x0004B259 File Offset: 0x0004A259
		protected ISelectionService SelectionService
		{
			get
			{
				return this.selectionService;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x0004B261 File Offset: 0x0004A261
		protected int SelectionVersion
		{
			get
			{
				return this.selectionVersion;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x0004B26C File Offset: 0x0004A26C
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

		// Token: 0x0600106E RID: 4206 RVA: 0x0004B2BC File Offset: 0x0004A2BC
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
				catch
				{
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0004B3C8 File Offset: 0x0004A3C8
		public virtual void Dispose()
		{
			if (this.menuService != null)
			{
				for (int i = 0; i < this.commandSet.Length; i++)
				{
					this.menuService.RemoveCommand(this.commandSet[i]);
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

		// Token: 0x06001070 RID: 4208 RVA: 0x0004B4CB File Offset: 0x0004A4CB
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

		// Token: 0x06001071 RID: 4209 RVA: 0x0004B508 File Offset: 0x0004A508
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

		// Token: 0x06001072 RID: 4210 RVA: 0x0004B59C File Offset: 0x0004A59C
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

		// Token: 0x06001073 RID: 4211 RVA: 0x0004B68C File Offset: 0x0004A68C
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

		// Token: 0x06001074 RID: 4212 RVA: 0x0004B708 File Offset: 0x0004A708
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
				catch
				{
				}
			}
			return Point.Empty;
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0004B768 File Offset: 0x0004A768
		protected PropertyDescriptor GetProperty(object comp, string propName)
		{
			return TypeDescriptor.GetProperties(comp)[propName];
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0004B776 File Offset: 0x0004A776
		protected virtual object GetService(Type serviceType)
		{
			if (this.site != null)
			{
				return this.site.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0004B790 File Offset: 0x0004A790
		private Size GetSize(IComponent comp)
		{
			PropertyDescriptor property = this.GetProperty(comp, "Size");
			if (property != null)
			{
				return (Size)property.GetValue(comp);
			}
			return Size.Empty;
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0004B7C0 File Offset: 0x0004A7C0
		protected virtual void GetSnapInformation(IDesignerHost host, IComponent component, out Size snapSize, out IComponent snapComponent, out PropertyDescriptor snapProperty)
		{
			IContainer container = component.Site.Container;
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

		// Token: 0x06001079 RID: 4217 RVA: 0x0004B860 File Offset: 0x0004A860
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

		// Token: 0x0600107A RID: 4218 RVA: 0x0004B8B4 File Offset: 0x0004A8B4
		private void OnEventHandlerChanged(object sender, EventArgs e)
		{
			this.OnUpdateCommandStatus();
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0004B8BC File Offset: 0x0004A8BC
		private void OnKeyCancel(object sender, EventArgs e)
		{
			this.OnKeyCancel(sender);
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0004B8C8 File Offset: 0x0004A8C8
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

		// Token: 0x0600107D RID: 4221 RVA: 0x0004B988 File Offset: 0x0004A988
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

		// Token: 0x0600107E RID: 4222 RVA: 0x0004B9D8 File Offset: 0x0004A9D8
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
						if (propertyDescriptor == null || propertyDescriptor.PropertyType != typeof(bool) || !(bool)propertyDescriptor.GetValue(component))
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

		// Token: 0x0600107F RID: 4223 RVA: 0x0004BE74 File Offset: 0x0004AE74
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

		// Token: 0x06001080 RID: 4224 RVA: 0x0004C25C File Offset: 0x0004B25C
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

		// Token: 0x06001081 RID: 4225 RVA: 0x0004C4FC File Offset: 0x0004B4FC
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

		// Token: 0x06001082 RID: 4226 RVA: 0x0004C948 File Offset: 0x0004B948
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

		// Token: 0x06001083 RID: 4227 RVA: 0x0004CA00 File Offset: 0x0004BA00
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

		// Token: 0x06001084 RID: 4228 RVA: 0x0004CDC4 File Offset: 0x0004BDC4
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
										goto IL_567;
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
								IL_567:
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

		// Token: 0x06001085 RID: 4229 RVA: 0x0004D460 File Offset: 0x0004C460
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
							designerHost.GetDesigner(rootComponent);
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
													continue;
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
											else if (arrayList4.Contains(component2) && Array.IndexOf(array2, component4) == -1)
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

		// Token: 0x06001086 RID: 4230 RVA: 0x0004DCA0 File Offset: 0x0004CCA0
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
							object[] array;
							if (components == null || components.Count == 0)
							{
								array = new IComponent[0];
							}
							else
							{
								array = new object[components.Count - 1];
								object rootComponent = designerHost.RootComponent;
								int num = 0;
								foreach (object obj in components)
								{
									IComponent component = (IComponent)obj;
									if (rootComponent != component)
									{
										array[num++] = component;
									}
								}
							}
							this.SelectionService.SetSelectedComponents(array, SelectionTypes.Replace);
						}
					}
				}
			}
			finally
			{
				Cursor.Current = value;
			}
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x0004DDA8 File Offset: 0x0004CDA8
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

		// Token: 0x06001088 RID: 4232 RVA: 0x0004DE4C File Offset: 0x0004CE4C
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

		// Token: 0x06001089 RID: 4233 RVA: 0x0004E07C File Offset: 0x0004D07C
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

		// Token: 0x0600108A RID: 4234 RVA: 0x0004E2E8 File Offset: 0x0004D2E8
		protected void OnMenuDesignerProperties(object sender, EventArgs e)
		{
			object obj = this.SelectionService.PrimarySelection;
			if (this.CheckComponentEditor(obj, true))
			{
				return;
			}
			IMenuCommandService menuCommandService = (IMenuCommandService)this.GetService(typeof(IMenuCommandService));
			if (menuCommandService == null || menuCommandService.GlobalInvoke(StandardCommands.PropertiesWindow))
			{
			}
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0004E334 File Offset: 0x0004D334
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

		// Token: 0x0600108C RID: 4236 RVA: 0x0004E404 File Offset: 0x0004D404
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

		// Token: 0x0600108D RID: 4237 RVA: 0x0004EBF8 File Offset: 0x0004DBF8
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

		// Token: 0x0600108E RID: 4238 RVA: 0x0004ED28 File Offset: 0x0004DD28
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

		// Token: 0x0600108F RID: 4239 RVA: 0x0004ED6F File Offset: 0x0004DD6F
		private void OnSnapLineTimerExpireMarshalled(object sender, EventArgs e)
		{
			this.snapLineTimer.Stop();
			this.EndDragManager();
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0004ED84 File Offset: 0x0004DD84
		protected void OnStatusAlways(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			menuCommand.Enabled = true;
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0004EDA0 File Offset: 0x0004DDA0
		protected void OnStatusAnySelection(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			menuCommand.Enabled = (this.selCount > 0);
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0004EDC4 File Offset: 0x0004DDC4
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

		// Token: 0x06001093 RID: 4243 RVA: 0x0004EEB8 File Offset: 0x0004DEB8
		protected void OnStatusCut(object sender, EventArgs e)
		{
			this.OnStatusDelete(sender, e);
			if (((MenuCommand)sender).Enabled)
			{
				this.OnStatusCopy(sender, e);
			}
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0004EED8 File Offset: 0x0004DED8
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

		// Token: 0x06001095 RID: 4245 RVA: 0x0004EFC0 File Offset: 0x0004DFC0
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

		// Token: 0x06001096 RID: 4246 RVA: 0x0004F094 File Offset: 0x0004E094
		private void OnStatusPrimarySelection(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			menuCommand.Enabled = (this.primarySelection != null);
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0004F0BC File Offset: 0x0004E0BC
		protected virtual void OnStatusSelectAll(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			menuCommand.Enabled = (designerHost.Container.Components.Count > 1);
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0004F100 File Offset: 0x0004E100
		protected virtual void OnUpdateCommandStatus()
		{
			for (int i = 0; i < this.commandSet.Length; i++)
			{
				this.commandSet[i].UpdateStatus();
			}
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0004F130 File Offset: 0x0004E130
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

		// Token: 0x0600109A RID: 4250 RVA: 0x0004F1F0 File Offset: 0x0004E1F0
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

		// Token: 0x0600109B RID: 4251 RVA: 0x0004F234 File Offset: 0x0004E234
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

		// Token: 0x0600109C RID: 4252 RVA: 0x0004F294 File Offset: 0x0004E294
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

		// Token: 0x0600109D RID: 4253 RVA: 0x0004F708 File Offset: 0x0004E708
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

		// Token: 0x0400105E RID: 4190
		private const int SORT_HORIZONTAL = 0;

		// Token: 0x0400105F RID: 4191
		private const int SORT_VERTICAL = 1;

		// Token: 0x04001060 RID: 4192
		private const int SORT_ZORDER = 2;

		// Token: 0x04001061 RID: 4193
		private const string CF_DESIGNER = "CF_DESIGNERCOMPONENTS_V2";

		// Token: 0x04001062 RID: 4194
		protected ISite site;

		// Token: 0x04001063 RID: 4195
		private CommandSet.CommandSetItem[] commandSet;

		// Token: 0x04001064 RID: 4196
		private IMenuCommandService menuService;

		// Token: 0x04001065 RID: 4197
		private IEventHandlerService eventService;

		// Token: 0x04001066 RID: 4198
		private ISelectionService selectionService;

		// Token: 0x04001067 RID: 4199
		protected int selCount;

		// Token: 0x04001068 RID: 4200
		protected IComponent primarySelection;

		// Token: 0x04001069 RID: 4201
		private bool selectionInherited;

		// Token: 0x0400106A RID: 4202
		protected bool controlsOnlySelection;

		// Token: 0x0400106B RID: 4203
		private int selectionVersion = 1;

		// Token: 0x0400106C RID: 4204
		protected DragAssistanceManager dragManager;

		// Token: 0x0400106D RID: 4205
		private Timer snapLineTimer;

		// Token: 0x0400106E RID: 4206
		private BehaviorService behaviorService;

		// Token: 0x0400106F RID: 4207
		private StatusCommandUI statusCommandUI;

		// Token: 0x020001AE RID: 430
		protected class CommandSetItem : MenuCommand
		{
			// Token: 0x0600109E RID: 4254 RVA: 0x0004F7A0 File Offset: 0x0004E7A0
			public CommandSetItem(CommandSet commandSet, EventHandler statusHandler, EventHandler invokeHandler, CommandID id, IUIService uiService) : this(commandSet, statusHandler, invokeHandler, id, false, uiService)
			{
			}

			// Token: 0x0600109F RID: 4255 RVA: 0x0004F7B0 File Offset: 0x0004E7B0
			public CommandSetItem(CommandSet commandSet, EventHandler statusHandler, EventHandler invokeHandler, CommandID id) : this(commandSet, statusHandler, invokeHandler, id, false, null)
			{
			}

			// Token: 0x060010A0 RID: 4256 RVA: 0x0004F7BF File Offset: 0x0004E7BF
			public CommandSetItem(CommandSet commandSet, EventHandler statusHandler, EventHandler invokeHandler, CommandID id, bool optimizeStatus) : this(commandSet, statusHandler, invokeHandler, id, optimizeStatus, null)
			{
			}

			// Token: 0x060010A1 RID: 4257 RVA: 0x0004F7D0 File Offset: 0x0004E7D0
			public CommandSetItem(CommandSet commandSet, EventHandler statusHandler, EventHandler invokeHandler, CommandID id, bool optimizeStatus, IUIService uiService) : base(invokeHandler, id)
			{
				this.uiService = uiService;
				this.eventService = commandSet.eventService;
				this.statusHandler = statusHandler;
				if (optimizeStatus && statusHandler != null)
				{
					this.commandSet = commandSet;
					lock (typeof(CommandSet.CommandSetItem))
					{
						if (CommandSet.CommandSetItem.commandStatusHash == null)
						{
							CommandSet.CommandSetItem.commandStatusHash = new Hashtable();
						}
					}
					if (!CommandSet.CommandSetItem.commandStatusHash.Contains(statusHandler))
					{
						CommandSet.CommandSetItem.commandStatusHash.Add(statusHandler, new CommandSet.CommandSetItem.StatusState());
					}
				}
			}

			// Token: 0x170002B7 RID: 695
			// (get) Token: 0x060010A2 RID: 4258 RVA: 0x0004F868 File Offset: 0x0004E868
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

			// Token: 0x060010A3 RID: 4259 RVA: 0x0004F8C0 File Offset: 0x0004E8C0
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

			// Token: 0x060010A4 RID: 4260 RVA: 0x0004F928 File Offset: 0x0004E928
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
				catch
				{
				}
			}

			// Token: 0x060010A5 RID: 4261 RVA: 0x0004F9CC File Offset: 0x0004E9CC
			protected override void OnCommandChanged(EventArgs e)
			{
				if (!this.updatingCommand)
				{
					base.OnCommandChanged(e);
				}
			}

			// Token: 0x060010A6 RID: 4262 RVA: 0x0004F9E0 File Offset: 0x0004E9E0
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

			// Token: 0x060010A7 RID: 4263 RVA: 0x0004FA3C File Offset: 0x0004EA3C
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

			// Token: 0x04001070 RID: 4208
			private EventHandler statusHandler;

			// Token: 0x04001071 RID: 4209
			private IEventHandlerService eventService;

			// Token: 0x04001072 RID: 4210
			private IUIService uiService;

			// Token: 0x04001073 RID: 4211
			private CommandSet commandSet;

			// Token: 0x04001074 RID: 4212
			private static Hashtable commandStatusHash;

			// Token: 0x04001075 RID: 4213
			private bool updatingCommand;

			// Token: 0x020001AF RID: 431
			private class StatusState
			{
				// Token: 0x170002B8 RID: 696
				// (get) Token: 0x060010A8 RID: 4264 RVA: 0x0004FABC File Offset: 0x0004EABC
				public int SelectionVersion
				{
					get
					{
						return this.selectionVersion;
					}
				}

				// Token: 0x060010A9 RID: 4265 RVA: 0x0004FAC4 File Offset: 0x0004EAC4
				internal void ApplyState(CommandSet.CommandSetItem item)
				{
					item.Enabled = ((this.statusFlags & 1) == 1);
					item.Visible = ((this.statusFlags & 2) == 2);
					item.Checked = ((this.statusFlags & 4) == 4);
					item.Supported = ((this.statusFlags & 8) == 8);
				}

				// Token: 0x060010AA RID: 4266 RVA: 0x0004FB18 File Offset: 0x0004EB18
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

				// Token: 0x04001076 RID: 4214
				private const int Enabled = 1;

				// Token: 0x04001077 RID: 4215
				private const int Visible = 2;

				// Token: 0x04001078 RID: 4216
				private const int Checked = 4;

				// Token: 0x04001079 RID: 4217
				private const int Supported = 8;

				// Token: 0x0400107A RID: 4218
				private const int NeedsUpdate = 16;

				// Token: 0x0400107B RID: 4219
				private int selectionVersion;

				// Token: 0x0400107C RID: 4220
				private int statusFlags = 16;
			}
		}

		// Token: 0x020001B0 RID: 432
		protected class ImmediateCommandSetItem : CommandSet.CommandSetItem
		{
			// Token: 0x060010AC RID: 4268 RVA: 0x0004FB9B File Offset: 0x0004EB9B
			public ImmediateCommandSetItem(CommandSet commandSet, EventHandler statusHandler, EventHandler invokeHandler, CommandID id, IUIService uiService) : base(commandSet, statusHandler, invokeHandler, id, uiService)
			{
			}

			// Token: 0x170002B9 RID: 697
			// (get) Token: 0x060010AD RID: 4269 RVA: 0x0004FBAA File Offset: 0x0004EBAA
			public override int OleStatus
			{
				get
				{
					base.UpdateStatus();
					return base.OleStatus;
				}
			}
		}

		// Token: 0x020001B1 RID: 433
		private class ComponentLeftCompare : IComparer
		{
			// Token: 0x060010AE RID: 4270 RVA: 0x0004FBB8 File Offset: 0x0004EBB8
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

		// Token: 0x020001B2 RID: 434
		private class ComponentTopCompare : IComparer
		{
			// Token: 0x060010B0 RID: 4272 RVA: 0x0004FC38 File Offset: 0x0004EC38
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

		// Token: 0x020001B3 RID: 435
		private class ControlZOrderCompare : IComparer
		{
			// Token: 0x060010B2 RID: 4274 RVA: 0x0004FCB8 File Offset: 0x0004ECB8
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

		// Token: 0x020001B4 RID: 436
		private class TabIndexCompare : IComparer
		{
			// Token: 0x060010B4 RID: 4276 RVA: 0x0004FD30 File Offset: 0x0004ED30
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
