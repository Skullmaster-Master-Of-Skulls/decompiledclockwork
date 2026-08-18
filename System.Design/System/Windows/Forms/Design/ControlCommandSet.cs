using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x020001D3 RID: 467
	internal class ControlCommandSet : CommandSet
	{
		// Token: 0x0600120D RID: 4621 RVA: 0x00057EA8 File Offset: 0x00056EA8
		public ControlCommandSet(ISite site) : base(site)
		{
			this.statusCommandUI = new StatusCommandUI(site);
			this.commandSet = new CommandSet.CommandSetItem[]
			{
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusMultiSelectPrimary), new EventHandler(base.OnMenuAlignByPrimary), StandardCommands.AlignLeft, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusMultiSelectPrimary), new EventHandler(base.OnMenuAlignByPrimary), StandardCommands.AlignTop, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusControlsOnlySelectionAndGrid), new EventHandler(base.OnMenuAlignToGrid), StandardCommands.AlignToGrid, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusMultiSelectPrimary), new EventHandler(base.OnMenuAlignByPrimary), StandardCommands.AlignBottom, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusMultiSelectPrimary), new EventHandler(base.OnMenuAlignByPrimary), StandardCommands.AlignHorizontalCenters, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusMultiSelectPrimary), new EventHandler(base.OnMenuAlignByPrimary), StandardCommands.AlignRight, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusMultiSelectPrimary), new EventHandler(base.OnMenuAlignByPrimary), StandardCommands.AlignVerticalCenters, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusControlsOnlySelection), new EventHandler(base.OnMenuCenterSelection), StandardCommands.CenterHorizontally, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusControlsOnlySelection), new EventHandler(base.OnMenuCenterSelection), StandardCommands.CenterVertically, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusMultiSelectNonContained), new EventHandler(base.OnMenuSpacingCommand), StandardCommands.HorizSpaceConcatenate, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusMultiSelectNonContained), new EventHandler(base.OnMenuSpacingCommand), StandardCommands.HorizSpaceDecrease, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusMultiSelectNonContained), new EventHandler(base.OnMenuSpacingCommand), StandardCommands.HorizSpaceIncrease, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusMultiSelectNonContained), new EventHandler(base.OnMenuSpacingCommand), StandardCommands.HorizSpaceMakeEqual, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusMultiSelectNonContained), new EventHandler(base.OnMenuSpacingCommand), StandardCommands.VertSpaceConcatenate, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusMultiSelectNonContained), new EventHandler(base.OnMenuSpacingCommand), StandardCommands.VertSpaceDecrease, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusMultiSelectNonContained), new EventHandler(base.OnMenuSpacingCommand), StandardCommands.VertSpaceIncrease, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusMultiSelectNonContained), new EventHandler(base.OnMenuSpacingCommand), StandardCommands.VertSpaceMakeEqual, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusMultiSelectPrimary), new EventHandler(base.OnMenuSizingCommand), StandardCommands.SizeToControl, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusMultiSelectPrimary), new EventHandler(base.OnMenuSizingCommand), StandardCommands.SizeToControlWidth, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusMultiSelectPrimary), new EventHandler(base.OnMenuSizingCommand), StandardCommands.SizeToControlHeight, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusControlsOnlySelectionAndGrid), new EventHandler(base.OnMenuSizeToGrid), StandardCommands.SizeToGrid, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusZOrder), new EventHandler(this.OnMenuZOrderSelection), StandardCommands.BringToFront, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusZOrder), new EventHandler(this.OnMenuZOrderSelection), StandardCommands.SendToBack, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusShowGrid), new EventHandler(base.OnMenuShowGrid), StandardCommands.ShowGrid, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusSnapToGrid), new EventHandler(base.OnMenuSnapToGrid), StandardCommands.SnapToGrid, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusAnyControls), new EventHandler(this.OnMenuTabOrder), StandardCommands.TabOrder, true),
				new CommandSet.CommandSetItem(this, new EventHandler(this.OnStatusLockControls), new EventHandler(this.OnMenuLockControls), StandardCommands.LockControls, true),
				new CommandSet.CommandSetItem(this, new EventHandler(base.OnStatusAlways), new EventHandler(this.OnKeySize), MenuCommands.KeySizeWidthIncrease),
				new CommandSet.CommandSetItem(this, new EventHandler(base.OnStatusAlways), new EventHandler(this.OnKeySize), MenuCommands.KeySizeHeightIncrease),
				new CommandSet.CommandSetItem(this, new EventHandler(base.OnStatusAlways), new EventHandler(this.OnKeySize), MenuCommands.KeySizeWidthDecrease),
				new CommandSet.CommandSetItem(this, new EventHandler(base.OnStatusAlways), new EventHandler(this.OnKeySize), MenuCommands.KeySizeHeightDecrease),
				new CommandSet.CommandSetItem(this, new EventHandler(base.OnStatusAlways), new EventHandler(this.OnKeySize), MenuCommands.KeyNudgeWidthIncrease),
				new CommandSet.CommandSetItem(this, new EventHandler(base.OnStatusAlways), new EventHandler(this.OnKeySize), MenuCommands.KeyNudgeHeightIncrease),
				new CommandSet.CommandSetItem(this, new EventHandler(base.OnStatusAlways), new EventHandler(this.OnKeySize), MenuCommands.KeyNudgeWidthDecrease),
				new CommandSet.CommandSetItem(this, new EventHandler(base.OnStatusAlways), new EventHandler(this.OnKeySize), MenuCommands.KeyNudgeHeightDecrease),
				new CommandSet.CommandSetItem(this, new EventHandler(base.OnStatusAlways), new EventHandler(this.OnKeySelect), MenuCommands.KeySelectNext),
				new CommandSet.CommandSetItem(this, new EventHandler(base.OnStatusAlways), new EventHandler(this.OnKeySelect), MenuCommands.KeySelectPrevious)
			};
			if (base.MenuService != null)
			{
				for (int i = 0; i < this.commandSet.Length; i++)
				{
					base.MenuService.AddCommand(this.commandSet[i]);
				}
			}
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				Control control = designerHost.RootComponent as Control;
				if (control != null)
				{
					this.baseControl = control;
				}
			}
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x000584EC File Offset: 0x000574EC
		private bool CheckSelectionParenting()
		{
			ICollection selectedComponents = base.SelectionService.GetSelectedComponents();
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				IComponent rootComponent = designerHost.RootComponent;
			}
			Hashtable hashtable = new Hashtable(selectedComponents.Count);
			foreach (object obj in selectedComponents)
			{
				Control control = obj as Control;
				if (control == null || control.Site == null)
				{
					return false;
				}
				hashtable.Add(obj, obj);
			}
			Control control2 = null;
			foreach (object obj2 in selectedComponents)
			{
				Control control3 = obj2 as Control;
				if (control3 == null || control3.Site == null)
				{
					return false;
				}
				for (Control parent = control3.Parent; parent != null; parent = parent.Parent)
				{
					if (parent != control2)
					{
						object obj3 = hashtable[parent];
						if (obj3 != null && obj3 != obj2)
						{
							return false;
						}
					}
				}
				control2 = control3.Parent;
			}
			return true;
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0005863C File Offset: 0x0005763C
		public override void Dispose()
		{
			if (base.MenuService != null)
			{
				for (int i = 0; i < this.commandSet.Length; i++)
				{
					base.MenuService.RemoveCommand(this.commandSet[i]);
				}
			}
			if (this.tabOrder != null)
			{
				this.tabOrder.Dispose();
				this.tabOrder = null;
			}
			this.statusCommandUI = null;
			base.Dispose();
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x000586A0 File Offset: 0x000576A0
		protected override void GetSnapInformation(IDesignerHost host, IComponent component, out Size snapSize, out IComponent snapComponent, out PropertyDescriptor snapProperty)
		{
			IComponent component2 = null;
			IContainer container = component.Site.Container;
			PropertyDescriptor propertyDescriptor = null;
			PropertyDescriptor propertyDescriptor2 = null;
			Control control = component as Control;
			PropertyDescriptorCollection properties;
			if (control != null)
			{
				Control parent = control.Parent;
				while (parent != null && component2 == null)
				{
					properties = TypeDescriptor.GetProperties(parent);
					propertyDescriptor2 = properties["SnapToGrid"];
					if (propertyDescriptor2 != null)
					{
						if (propertyDescriptor2.PropertyType == typeof(bool) && parent.Site != null && parent.Site.Container == container)
						{
							component2 = parent;
						}
						else
						{
							propertyDescriptor2 = null;
						}
					}
					parent = parent.Parent;
				}
			}
			if (component2 == null)
			{
				component2 = host.RootComponent;
			}
			properties = TypeDescriptor.GetProperties(component2);
			if (propertyDescriptor2 == null)
			{
				propertyDescriptor2 = properties["SnapToGrid"];
				if (propertyDescriptor2 != null && propertyDescriptor2.PropertyType != typeof(bool))
				{
					propertyDescriptor2 = null;
				}
			}
			if (propertyDescriptor == null)
			{
				propertyDescriptor = properties["GridSize"];
				if (propertyDescriptor != null && propertyDescriptor.PropertyType != typeof(Size))
				{
					propertyDescriptor = null;
				}
			}
			snapComponent = component2;
			snapProperty = propertyDescriptor2;
			if (propertyDescriptor != null)
			{
				snapSize = (Size)propertyDescriptor.GetValue(snapComponent);
				return;
			}
			snapSize = Size.Empty;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x000587BC File Offset: 0x000577BC
		protected override bool OnKeyCancel(object sender)
		{
			if (!base.OnKeyCancel(sender))
			{
				MenuCommand menuCommand = (MenuCommand)sender;
				bool backwards = menuCommand.CommandID.Equals(MenuCommands.KeyReverseCancel);
				this.RotateParentSelection(backwards);
				return true;
			}
			return false;
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x000587F4 File Offset: 0x000577F4
		private ArrayList GenerateSnapLines(SelectionRules rules, Control primaryControl, int directionOffsetX, int directionOffsetY)
		{
			ArrayList arrayList = new ArrayList(2);
			Point point = base.BehaviorService.ControlToAdornerWindow(primaryControl);
			bool flag = primaryControl.Parent != null && primaryControl.Parent.IsMirrored;
			if (directionOffsetX != 0)
			{
				if (!flag)
				{
					if ((rules & SelectionRules.RightSizeable) != SelectionRules.None)
					{
						arrayList.Add(new SnapLine(SnapLineType.Right, point.X + primaryControl.Width - 1));
						arrayList.Add(new SnapLine(SnapLineType.Vertical, point.X + primaryControl.Width + primaryControl.Margin.Right, "Margin.Right", SnapLinePriority.Always));
					}
				}
				else if ((rules & SelectionRules.LeftSizeable) != SelectionRules.None)
				{
					arrayList.Add(new SnapLine(SnapLineType.Left, point.X));
					arrayList.Add(new SnapLine(SnapLineType.Vertical, point.X - primaryControl.Margin.Left, "Margin.Left", SnapLinePriority.Always));
				}
			}
			if (directionOffsetY != 0 && (rules & SelectionRules.BottomSizeable) != SelectionRules.None)
			{
				arrayList.Add(new SnapLine(SnapLineType.Bottom, point.Y + primaryControl.Height - 1));
				arrayList.Add(new SnapLine(SnapLineType.Horizontal, point.Y + primaryControl.Height + primaryControl.Margin.Bottom, "Margin.Bottom", SnapLinePriority.Always));
			}
			return arrayList;
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00058928 File Offset: 0x00057928
		protected void OnKeySize(object sender, EventArgs e)
		{
			ISelectionService selectionService = base.SelectionService;
			if (selectionService != null)
			{
				IComponent component = selectionService.PrimarySelection as IComponent;
				if (component != null)
				{
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						ControlDesigner controlDesigner = designerHost.GetDesigner(component) as ControlDesigner;
						if (controlDesigner != null && (controlDesigner.SelectionRules & SelectionRules.Locked) == SelectionRules.None)
						{
							bool flag = false;
							PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)["Dock"];
							if (propertyDescriptor != null)
							{
								DockStyle dockStyle = (DockStyle)propertyDescriptor.GetValue(component);
								flag = (dockStyle == DockStyle.Bottom || dockStyle == DockStyle.Right);
							}
							SelectionRules selectionRules = SelectionRules.Visible;
							CommandID commandID = ((MenuCommand)sender).CommandID;
							bool flag2 = false;
							int num = 0;
							int num2 = 0;
							bool flag3 = false;
							if (commandID.Equals(MenuCommands.KeySizeHeightDecrease))
							{
								num2 = (flag ? 1 : -1);
								selectionRules |= SelectionRules.BottomSizeable;
							}
							else if (commandID.Equals(MenuCommands.KeySizeHeightIncrease))
							{
								num2 = (flag ? -1 : 1);
								selectionRules |= SelectionRules.BottomSizeable;
							}
							else if (commandID.Equals(MenuCommands.KeySizeWidthDecrease))
							{
								num = (flag ? 1 : -1);
								selectionRules |= SelectionRules.RightSizeable;
							}
							else if (commandID.Equals(MenuCommands.KeySizeWidthIncrease))
							{
								num = (flag ? -1 : 1);
								selectionRules |= SelectionRules.RightSizeable;
							}
							else if (commandID.Equals(MenuCommands.KeyNudgeHeightDecrease))
							{
								num2 = -1;
								flag2 = true;
								selectionRules |= SelectionRules.BottomSizeable;
							}
							else if (commandID.Equals(MenuCommands.KeyNudgeHeightIncrease))
							{
								num2 = 1;
								flag2 = true;
								selectionRules |= SelectionRules.BottomSizeable;
							}
							else if (commandID.Equals(MenuCommands.KeyNudgeWidthDecrease))
							{
								num = -1;
								flag2 = true;
								selectionRules |= SelectionRules.RightSizeable;
							}
							else if (commandID.Equals(MenuCommands.KeyNudgeWidthIncrease))
							{
								num = 1;
								flag2 = true;
								selectionRules |= SelectionRules.RightSizeable;
							}
							DesignerTransaction designerTransaction = null;
							if (selectionService.SelectionCount > 1)
							{
								designerTransaction = designerHost.CreateTransaction(SR.GetString("DragDropSizeComponents", new object[]
								{
									selectionService.SelectionCount
								}));
							}
							else
							{
								designerTransaction = designerHost.CreateTransaction(SR.GetString("DragDropSizeComponent", new object[]
								{
									component.Site.Name
								}));
							}
							try
							{
								if (base.BehaviorService != null)
								{
									Control control = component as Control;
									bool useSnapLines = base.BehaviorService.UseSnapLines;
									if (this.dragManager != null)
									{
										base.EndDragManager();
									}
									if (flag2 && useSnapLines)
									{
										ArrayList dragComponents = new ArrayList(selectionService.GetSelectedComponents());
										this.dragManager = new DragAssistanceManager(controlDesigner.Component.Site, dragComponents);
										ArrayList targetSnaplines = this.GenerateSnapLines(controlDesigner.SelectionRules, control, num, num2);
										Point point = this.dragManager.OffsetToNearestSnapLocation(control, targetSnaplines, new Point(num, num2));
										Size sz = control.Size;
										sz += new Size(point.X, point.Y);
										if (sz.Width <= 0 || sz.Height <= 0)
										{
											num = 0;
											num2 = 0;
											base.EndDragManager();
										}
										else
										{
											num = point.X;
											num2 = point.Y;
										}
										if (control.Parent.IsMirrored)
										{
											num *= -1;
										}
									}
									else if (!flag2 && !useSnapLines)
									{
										bool flag4 = false;
										Size empty = Size.Empty;
										IComponent component2 = null;
										PropertyDescriptor propertyDescriptor2 = null;
										this.GetSnapInformation(designerHost, component, out empty, out component2, out propertyDescriptor2);
										if (propertyDescriptor2 != null)
										{
											flag4 = (bool)propertyDescriptor2.GetValue(component2);
										}
										if (flag4 && !empty.IsEmpty)
										{
											ParentControlDesigner parentControlDesigner = designerHost.GetDesigner(control.Parent) as ParentControlDesigner;
											if (parentControlDesigner != null)
											{
												num *= empty.Width;
												num2 *= empty.Height;
												if (control.Parent.IsMirrored)
												{
													num *= -1;
												}
												Rectangle dragRect = new Rectangle(control.Location.X, control.Location.Y, control.Width + num, control.Height + num2);
												Rectangle snappedRect = parentControlDesigner.GetSnappedRect(control.Bounds, dragRect, true);
												if (num != 0)
												{
													num = snappedRect.Width - control.Width;
												}
												if (num2 != 0)
												{
													num2 = snappedRect.Height - control.Height;
												}
											}
										}
										else
										{
											flag3 = true;
											if (control.Parent.IsMirrored)
											{
												num *= -1;
											}
										}
									}
									else
									{
										flag3 = true;
										if (control.Parent.IsMirrored)
										{
											num *= -1;
										}
									}
									foreach (object obj in selectionService.GetSelectedComponents())
									{
										IComponent component3 = (IComponent)obj;
										controlDesigner = (designerHost.GetDesigner(component3) as ControlDesigner);
										if (controlDesigner == null || (controlDesigner.SelectionRules & selectionRules) == selectionRules)
										{
											Control control2 = component3 as Control;
											if (control2 != null)
											{
												int num3 = num2;
												if (flag3)
												{
													PropertyDescriptor propertyDescriptor3 = TypeDescriptor.GetProperties(component3)["IntegralHeight"];
													if (propertyDescriptor3 != null)
													{
														object value = propertyDescriptor3.GetValue(component3);
														if (value is bool && (bool)value)
														{
															PropertyDescriptor propertyDescriptor4 = TypeDescriptor.GetProperties(component3)["ItemHeight"];
															if (propertyDescriptor4 != null)
															{
																num3 *= (int)propertyDescriptor4.GetValue(component3);
															}
														}
													}
												}
												PropertyDescriptor propertyDescriptor5 = TypeDescriptor.GetProperties(component3)["Size"];
												if (propertyDescriptor5 != null)
												{
													Size size = (Size)propertyDescriptor5.GetValue(component3);
													size += new Size(num, num3);
													propertyDescriptor5.SetValue(component3, size);
												}
											}
											if (control2 == selectionService.PrimarySelection && this.statusCommandUI != null)
											{
												this.statusCommandUI.SetStatusInformation(control2);
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
									base.SnapLineTimer.Start();
									this.dragManager.RenderSnapLinesInternal();
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00058F30 File Offset: 0x00057F30
		protected void OnKeySelect(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			bool backwards = menuCommand.CommandID.Equals(MenuCommands.KeySelectPrevious);
			this.RotateTabSelection(backwards);
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00058F5C File Offset: 0x00057F5C
		protected override void OnKeyMove(object sender, EventArgs e)
		{
			base.OnKeyMove(sender, e);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00058F68 File Offset: 0x00057F68
		protected void OnMenuLockControls(object sender, EventArgs e)
		{
			Cursor value = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					ComponentCollection components = designerHost.Container.Components;
					if (components != null && components.Count > 0)
					{
						DesignerTransaction designerTransaction = null;
						try
						{
							designerTransaction = designerHost.CreateTransaction(SR.GetString("CommandSetLockControls", new object[]
							{
								components.Count
							}));
							MenuCommand menuCommand = (MenuCommand)sender;
							bool flag = !menuCommand.Checked;
							bool flag2 = true;
							foreach (object obj in components)
							{
								IComponent component = (IComponent)obj;
								PropertyDescriptor property = base.GetProperty(component, "Locked");
								if (property != null && !property.IsReadOnly)
								{
									if (flag2 && !base.CanCheckout(component))
									{
										return;
									}
									flag2 = false;
									property.SetValue(component, flag);
								}
							}
							menuCommand.Checked = flag;
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
			finally
			{
				Cursor.Current = value;
			}
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x000590E4 File Offset: 0x000580E4
		private void OnMenuTabOrder(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			if (menuCommand.Checked)
			{
				if (this.tabOrder != null)
				{
					this.tabOrder.Dispose();
					this.tabOrder = null;
				}
				menuCommand.Checked = false;
				return;
			}
			ISelectionService selectionService = base.SelectionService;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null && selectionService != null)
			{
				object rootComponent = designerHost.RootComponent;
				if (rootComponent != null)
				{
					selectionService.SetSelectedComponents(new object[]
					{
						rootComponent
					}, SelectionTypes.Replace);
				}
			}
			this.tabOrder = new TabOrder((IDesignerHost)this.GetService(typeof(IDesignerHost)));
			menuCommand.Checked = true;
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0005918C File Offset: 0x0005818C
		private void OnMenuZOrderSelection(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			CommandID commandID = menuCommand.CommandID;
			if (base.SelectionService == null)
			{
				return;
			}
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			Cursor value = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				DesignerTransaction designerTransaction = null;
				try
				{
					ICollection selectedComponents = base.SelectionService.GetSelectedComponents();
					object[] array = new object[selectedComponents.Count];
					selectedComponents.CopyTo(array, 0);
					string @string;
					if (commandID == StandardCommands.BringToFront)
					{
						@string = SR.GetString("CommandSetBringToFront", new object[]
						{
							array.Length
						});
					}
					else
					{
						@string = SR.GetString("CommandSetSendToBack", new object[]
						{
							array.Length
						});
					}
					Array.Sort(array, new ControlCommandSet.ControlComparer());
					designerTransaction = designerHost.CreateTransaction(@string);
					if (array.Length > 0)
					{
						int num = array.Length;
						for (int i = num - 1; i >= 0; i--)
						{
							Control control = array[i] as Control;
							IComponent component = array[i] as IComponent;
							if (component != null)
							{
								INestedSite nestedSite = component.Site as INestedSite;
								if (nestedSite != null)
								{
									INestedContainer nestedContainer = nestedSite.Container as INestedContainer;
									if (nestedContainer != null)
									{
										control = (nestedContainer.Owner as Control);
										array[i] = control;
									}
								}
							}
							if (control != null)
							{
								Control parent = control.Parent;
								if (parent != null)
								{
									if (componentChangeService != null)
									{
										try
										{
											if (!arrayList2.Contains(parent))
											{
												PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(parent)["Controls"];
												if (propertyDescriptor != null)
												{
													arrayList2.Add(parent);
													componentChangeService.OnComponentChanging(parent, propertyDescriptor);
												}
											}
										}
										catch (CheckoutException ex)
										{
											if (ex == CheckoutException.Canceled)
											{
												if (designerTransaction != null)
												{
													designerTransaction.Cancel();
												}
												return;
											}
											throw ex;
										}
									}
									if (!arrayList.Contains(parent))
									{
										arrayList.Add(parent);
										parent.SuspendLayout();
									}
								}
							}
						}
						for (int j = num - 1; j >= 0; j--)
						{
							if (commandID == StandardCommands.BringToFront)
							{
								Control control2 = array[num - j - 1] as Control;
								if (control2 != null)
								{
									control2.BringToFront();
								}
							}
							else if (commandID == StandardCommands.SendToBack)
							{
								Control control3 = array[j] as Control;
								if (control3 != null)
								{
									control3.SendToBack();
								}
							}
						}
					}
				}
				finally
				{
					if (designerTransaction != null && !designerTransaction.Canceled)
					{
						foreach (object obj in arrayList2)
						{
							Control component2 = (Control)obj;
							PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(component2)["Controls"];
							if (componentChangeService != null && propertyDescriptor2 != null)
							{
								componentChangeService.OnComponentChanged(component2, propertyDescriptor2, null, null);
							}
						}
						foreach (object obj2 in arrayList)
						{
							Control control4 = (Control)obj2;
							control4.ResumeLayout();
						}
						designerTransaction.Commit();
					}
				}
			}
			finally
			{
				Cursor.Current = value;
			}
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00059530 File Offset: 0x00058530
		protected void OnStatusAnyControls(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			bool enabled = false;
			if (this.baseControl != null && this.baseControl.Controls.Count > 0)
			{
				enabled = true;
			}
			menuCommand.Enabled = enabled;
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0005956C File Offset: 0x0005856C
		protected void OnStatusControlsOnlySelection(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			menuCommand.Enabled = (this.selCount > 0 && this.controlsOnlySelection);
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00059598 File Offset: 0x00058598
		protected void OnStatusControlsOnlySelectionAndGrid(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			menuCommand.Enabled = (this.selCount > 0 && this.controlsOnlySelection && !base.BehaviorService.UseSnapLines);
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x000595D4 File Offset: 0x000585D4
		protected void OnStatusLockControls(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			if (this.baseControl == null)
			{
				menuCommand.Enabled = false;
				return;
			}
			menuCommand.Enabled = this.controlsOnlySelection;
			menuCommand.Checked = false;
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.baseControl)["Locked"];
			if (propertyDescriptor != null && (bool)propertyDescriptor.GetValue(this.baseControl))
			{
				menuCommand.Checked = true;
				return;
			}
			IDesignerHost designerHost = (IDesignerHost)this.site.GetService(typeof(IDesignerHost));
			if (designerHost == null)
			{
				return;
			}
			ComponentDesigner componentDesigner = designerHost.GetDesigner(this.baseControl) as ComponentDesigner;
			foreach (object component in componentDesigner.AssociatedComponents)
			{
				propertyDescriptor = TypeDescriptor.GetProperties(component)["Locked"];
				if (propertyDescriptor != null && (bool)propertyDescriptor.GetValue(component))
				{
					menuCommand.Checked = true;
					break;
				}
			}
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x000596E8 File Offset: 0x000586E8
		protected void OnStatusMultiSelect(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			menuCommand.Enabled = (this.controlsOnlySelection && this.selCount > 1);
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00059718 File Offset: 0x00058718
		protected void OnStatusMultiSelectPrimary(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			menuCommand.Enabled = (this.controlsOnlySelection && this.selCount > 1 && this.primarySelection != null);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00059754 File Offset: 0x00058754
		private void OnStatusMultiSelectNonContained(object sender, EventArgs e)
		{
			this.OnStatusMultiSelect(sender, e);
			MenuCommand menuCommand = (MenuCommand)sender;
			if (menuCommand.Enabled)
			{
				menuCommand.Enabled = this.CheckSelectionParenting();
			}
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00059784 File Offset: 0x00058784
		protected void OnStatusShowGrid(object sender, EventArgs e)
		{
			if (this.site != null)
			{
				IDesignerHost designerHost = (IDesignerHost)this.site.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					IComponent rootComponent = designerHost.RootComponent;
					if (rootComponent != null && rootComponent is Control)
					{
						PropertyDescriptor property = base.GetProperty(rootComponent, "DrawGrid");
						if (property != null)
						{
							bool @checked = (bool)property.GetValue(rootComponent);
							MenuCommand menuCommand = (MenuCommand)sender;
							menuCommand.Enabled = true;
							menuCommand.Checked = @checked;
						}
					}
				}
			}
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00059800 File Offset: 0x00058800
		protected void OnStatusSnapToGrid(object sender, EventArgs e)
		{
			if (this.site != null)
			{
				IDesignerHost designerHost = (IDesignerHost)this.site.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					IComponent rootComponent = designerHost.RootComponent;
					if (rootComponent != null && rootComponent is Control)
					{
						PropertyDescriptor property = base.GetProperty(rootComponent, "SnapToGrid");
						if (property != null)
						{
							bool @checked = (bool)property.GetValue(rootComponent);
							MenuCommand menuCommand = (MenuCommand)sender;
							menuCommand.Enabled = this.controlsOnlySelection;
							menuCommand.Checked = @checked;
						}
					}
				}
			}
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00059880 File Offset: 0x00058880
		private void OnStatusZOrder(object sender, EventArgs e)
		{
			MenuCommand menuCommand = (MenuCommand)sender;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				ComponentCollection components = designerHost.Container.Components;
				object rootComponent = designerHost.RootComponent;
				bool flag = components != null && components.Count > 2 && this.controlsOnlySelection;
				if (flag)
				{
					if (base.SelectionService == null)
					{
						return;
					}
					ICollection selectedComponents = base.SelectionService.GetSelectedComponents();
					flag = false;
					foreach (object obj in selectedComponents)
					{
						if (obj is Control && !TypeDescriptor.GetAttributes(obj)[typeof(InheritanceAttribute)].Equals(InheritanceAttribute.InheritedReadOnly))
						{
							flag = true;
						}
						if (obj == rootComponent)
						{
							flag = false;
							break;
						}
					}
				}
				menuCommand.Enabled = flag;
				return;
			}
			menuCommand.Enabled = false;
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00059988 File Offset: 0x00058988
		protected override void OnUpdateCommandStatus()
		{
			for (int i = 0; i < this.commandSet.Length; i++)
			{
				this.commandSet[i].UpdateStatus();
			}
			base.OnUpdateCommandStatus();
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x000599BC File Offset: 0x000589BC
		private void RotateParentSelection(bool backwards)
		{
			object obj = null;
			ISelectionService selectionService = base.SelectionService;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (selectionService == null || designerHost == null || !(designerHost.RootComponent is Control))
			{
				return;
			}
			IContainer container = designerHost.Container;
			Control control = selectionService.PrimarySelection as Control;
			Control control2;
			if (control != null)
			{
				control2 = control;
			}
			else
			{
				control2 = (Control)designerHost.RootComponent;
			}
			if (backwards)
			{
				if (control2 != null)
				{
					if (control2.Controls.Count > 0)
					{
						obj = control2.Controls[0];
					}
					else
					{
						obj = control2;
					}
				}
			}
			else if (control2 != null)
			{
				obj = control2.Parent;
				Control control3 = obj as Control;
				IContainer container2 = null;
				if (control3 != null && control3.Site != null)
				{
					container2 = DesignerUtils.CheckForNestedContainer(control3.Site.Container);
				}
				if (control3 == null || control3.Site == null || container2 != container)
				{
					obj = control2;
				}
			}
			selectionService.SetSelectedComponents(new object[]
			{
				obj
			}, SelectionTypes.Replace);
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00059AB0 File Offset: 0x00058AB0
		private void RotateTabSelection(bool backwards)
		{
			object obj = null;
			ISelectionService selectionService = base.SelectionService;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (selectionService == null || designerHost == null || !(designerHost.RootComponent is Control))
			{
				return;
			}
			IContainer container = designerHost.Container;
			Control control = (Control)designerHost.RootComponent;
			object primarySelection = selectionService.PrimarySelection;
			Control control2 = primarySelection as Control;
			if (obj == null && control2 != null)
			{
				if (!control.Contains(control2))
				{
					if (control != primarySelection)
					{
						goto IL_9E;
					}
				}
				while ((control2 = this.GetNextControlInTab(control, control2, !backwards)) != null && (control2.Site == null || control2.Site.Container != control2.Container))
				{
				}
				obj = control2;
			}
			IL_9E:
			if (obj == null)
			{
				ComponentTray componentTray = (ComponentTray)this.GetService(typeof(ComponentTray));
				if (componentTray != null)
				{
					obj = componentTray.GetNextComponent((IComponent)primarySelection, !backwards);
					if (obj != null)
					{
						IComponent component = obj as IComponent;
						ControlDesigner controlDesigner = designerHost.GetDesigner(component) as ControlDesigner;
						while (controlDesigner != null)
						{
							component = componentTray.GetNextComponent(component, !backwards);
							if (component != null)
							{
								controlDesigner = (designerHost.GetDesigner(component) as ControlDesigner);
							}
							else
							{
								controlDesigner = null;
							}
						}
					}
				}
				if (obj == null)
				{
					obj = control;
				}
			}
			selectionService.SetSelectedComponents(new object[]
			{
				obj
			}, SelectionTypes.Replace);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00059BF0 File Offset: 0x00058BF0
		private Control GetNextControlInTab(Control basectl, Control ctl, bool forward)
		{
			if (forward)
			{
				Control.ControlCollection controls = ctl.Controls;
				if (controls != null && controls.Count > 0)
				{
					Control control = null;
					for (int i = 0; i < controls.Count; i++)
					{
						if (control == null || control.TabIndex > controls[i].TabIndex)
						{
							control = controls[i];
						}
					}
					return control;
				}
				while (ctl != basectl)
				{
					int tabIndex = ctl.TabIndex;
					bool flag = false;
					Control control2 = null;
					Control parent = ctl.Parent;
					int num = 0;
					Control.ControlCollection controls2 = parent.Controls;
					if (controls2 != null)
					{
						num = controls2.Count;
					}
					for (int j = 0; j < num; j++)
					{
						if (controls2[j] != ctl)
						{
							if (controls2[j].TabIndex >= tabIndex && (control2 == null || control2.TabIndex > controls2[j].TabIndex) && (controls2[j].TabIndex != tabIndex || flag))
							{
								control2 = controls2[j];
							}
						}
						else
						{
							flag = true;
						}
					}
					if (control2 != null)
					{
						return control2;
					}
					ctl = ctl.Parent;
				}
			}
			else
			{
				if (ctl != basectl)
				{
					int tabIndex2 = ctl.TabIndex;
					bool flag2 = false;
					Control control3 = null;
					Control parent2 = ctl.Parent;
					int num2 = 0;
					Control.ControlCollection controls3 = parent2.Controls;
					if (controls3 != null)
					{
						num2 = controls3.Count;
					}
					for (int k = num2 - 1; k >= 0; k--)
					{
						if (controls3[k] != ctl)
						{
							if (controls3[k].TabIndex <= tabIndex2 && (control3 == null || control3.TabIndex < controls3[k].TabIndex) && (controls3[k].TabIndex != tabIndex2 || flag2))
							{
								control3 = controls3[k];
							}
						}
						else
						{
							flag2 = true;
						}
					}
					if (control3 != null)
					{
						ctl = control3;
					}
					else
					{
						if (parent2 == basectl)
						{
							return null;
						}
						return parent2;
					}
				}
				Control.ControlCollection controls4 = ctl.Controls;
				while (controls4 != null && controls4.Count > 0)
				{
					Control control4 = null;
					for (int l = controls4.Count - 1; l >= 0; l--)
					{
						if (control4 == null || control4.TabIndex < controls4[l].TabIndex)
						{
							control4 = controls4[l];
						}
					}
					ctl = control4;
					controls4 = ctl.Controls;
				}
			}
			if (ctl != basectl)
			{
				return ctl;
			}
			return null;
		}

		// Token: 0x040010FC RID: 4348
		private CommandSet.CommandSetItem[] commandSet;

		// Token: 0x040010FD RID: 4349
		private TabOrder tabOrder;

		// Token: 0x040010FE RID: 4350
		private Control baseControl;

		// Token: 0x040010FF RID: 4351
		private StatusCommandUI statusCommandUI;

		// Token: 0x020001D4 RID: 468
		private class ControlComparer : IComparer
		{
			// Token: 0x06001227 RID: 4647 RVA: 0x00059E30 File Offset: 0x00058E30
			public int Compare(object x, object y)
			{
				if (x == y)
				{
					return 0;
				}
				Control control = x as Control;
				Control control2 = y as Control;
				if (control != null && control2 != null)
				{
					if (control.Parent == control2.Parent)
					{
						Control parent = control.Parent;
						if (parent == null)
						{
							return 0;
						}
						if (parent.Controls.GetChildIndex(control) > parent.Controls.GetChildIndex(control2))
						{
							return -1;
						}
						return 1;
					}
					else
					{
						if (control.Parent == null || control.Contains(control2))
						{
							return 1;
						}
						if (control2.Parent == null || control2.Contains(control))
						{
							return -1;
						}
						return (int)control.Parent.Handle - (int)control2.Parent.Handle;
					}
				}
				else
				{
					if (control2 != null)
					{
						return -1;
					}
					if (control != null)
					{
						return 1;
					}
					return 0;
				}
			}
		}
	}
}
