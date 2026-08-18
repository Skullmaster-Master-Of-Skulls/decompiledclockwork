using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000352 RID: 850
	internal class ToolStripItemCustomMenuItemCollection : CustomMenuItemCollection
	{
		// Token: 0x060021AA RID: 8618 RVA: 0x000CD12F File Offset: 0x000CB32F
		public ToolStripItemCustomMenuItemCollection(IServiceProvider provider, Component currentItem)
		{
			this.serviceProvider = provider;
			this.currentItem = (currentItem as ToolStripItem);
			this.PopulateList();
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x060021AB RID: 8619 RVA: 0x000CD150 File Offset: 0x000CB350
		private ToolStrip ParentTool
		{
			get
			{
				return this.currentItem.Owner;
			}
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x000CD160 File Offset: 0x000CB360
		private ToolStripMenuItem CreatePropertyBasedItem(string text, string propertyName, string imageName)
		{
			ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(text);
			bool flag = this.IsPropertyBrowsable(propertyName);
			toolStripMenuItem.Visible = flag;
			if (flag)
			{
				if (!string.IsNullOrEmpty(imageName))
				{
					toolStripMenuItem.Image = new Bitmap(BitmapSelector.GetResourceStream(typeof(ToolStripMenuItem), imageName));
					toolStripMenuItem.ImageTransparentColor = Color.Magenta;
				}
				IUIService iuiservice = this.serviceProvider.GetService(typeof(IUIService)) as IUIService;
				if (iuiservice != null)
				{
					toolStripMenuItem.DropDown.Renderer = (ToolStripProfessionalRenderer)iuiservice.Styles["VsRenderer"];
					toolStripMenuItem.DropDown.Font = (Font)iuiservice.Styles["DialogFont"];
				}
			}
			return toolStripMenuItem;
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x000CD218 File Offset: 0x000CB418
		private ToolStripMenuItem CreateEnumValueItem(string propertyName, string name, object value)
		{
			ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(name);
			toolStripMenuItem.Tag = new ToolStripItemCustomMenuItemCollection.EnumValueDescription(propertyName, value);
			toolStripMenuItem.Click += this.OnEnumValueChanged;
			return toolStripMenuItem;
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x000CD24C File Offset: 0x000CB44C
		private ToolStripMenuItem CreateBooleanItem(string text, string propertyName)
		{
			ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(text);
			bool visible = this.IsPropertyBrowsable(propertyName);
			toolStripMenuItem.Visible = visible;
			toolStripMenuItem.Tag = propertyName;
			toolStripMenuItem.CheckOnClick = true;
			toolStripMenuItem.Click += this.OnBooleanValueChanged;
			return toolStripMenuItem;
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x000CD290 File Offset: 0x000CB490
		private void PopulateList()
		{
			ToolStripItem toolStripItem = this.currentItem;
			if (!(toolStripItem is ToolStripControlHost) && !(toolStripItem is ToolStripSeparator))
			{
				this.imageToolStripMenuItem = new ToolStripMenuItem();
				this.imageToolStripMenuItem.Text = SR.GetString("ToolStripItemContextMenuSetImage");
				this.imageToolStripMenuItem.Image = new Bitmap(typeof(ToolStripMenuItem), "image.bmp");
				this.imageToolStripMenuItem.ImageTransparentColor = Color.Magenta;
				this.imageToolStripMenuItem.Click += this.OnImageToolStripMenuItemClick;
				this.enabledToolStripMenuItem = this.CreateBooleanItem("E&nabled", "Enabled");
				base.AddRange(new ToolStripItem[]
				{
					this.imageToolStripMenuItem,
					this.enabledToolStripMenuItem
				});
				if (toolStripItem is ToolStripMenuItem)
				{
					this.checkedToolStripMenuItem = this.CreateBooleanItem("C&hecked", "Checked");
					this.showShortcutKeysToolStripMenuItem = this.CreateBooleanItem("ShowShortcut&Keys", "ShowShortcutKeys");
					base.AddRange(new ToolStripItem[]
					{
						this.checkedToolStripMenuItem,
						this.showShortcutKeysToolStripMenuItem
					});
				}
				else
				{
					if (toolStripItem is ToolStripLabel)
					{
						this.isLinkToolStripMenuItem = this.CreateBooleanItem("IsLin&k", "IsLink");
						base.Add(this.isLinkToolStripMenuItem);
					}
					if (toolStripItem is ToolStripStatusLabel)
					{
						this.springToolStripMenuItem = this.CreateBooleanItem("Sprin&g", "Spring");
						base.Add(this.springToolStripMenuItem);
					}
					this.leftToolStripMenuItem = this.CreateEnumValueItem("Alignment", "Left", ToolStripItemAlignment.Left);
					this.rightToolStripMenuItem = this.CreateEnumValueItem("Alignment", "Right", ToolStripItemAlignment.Right);
					this.noneStyleToolStripMenuItem = this.CreateEnumValueItem("DisplayStyle", "None", ToolStripItemDisplayStyle.None);
					this.textStyleToolStripMenuItem = this.CreateEnumValueItem("DisplayStyle", "Text", ToolStripItemDisplayStyle.Text);
					this.imageStyleToolStripMenuItem = this.CreateEnumValueItem("DisplayStyle", "Image", ToolStripItemDisplayStyle.Image);
					this.imageTextStyleToolStripMenuItem = this.CreateEnumValueItem("DisplayStyle", "ImageAndText", ToolStripItemDisplayStyle.ImageAndText);
					this.alignmentToolStripMenuItem = this.CreatePropertyBasedItem("Ali&gnment", "Alignment", "alignment.bmp");
					this.alignmentToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
					{
						this.leftToolStripMenuItem,
						this.rightToolStripMenuItem
					});
					this.displayStyleToolStripMenuItem = this.CreatePropertyBasedItem("Displa&yStyle", "DisplayStyle", "displaystyle.bmp");
					this.displayStyleToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
					{
						this.noneStyleToolStripMenuItem,
						this.textStyleToolStripMenuItem,
						this.imageStyleToolStripMenuItem,
						this.imageTextStyleToolStripMenuItem
					});
					IUIService iuiservice = this.serviceProvider.GetService(typeof(IUIService)) as IUIService;
					if (iuiservice != null)
					{
						ToolStripProfessionalRenderer renderer = (ToolStripProfessionalRenderer)iuiservice.Styles["VsRenderer"];
						this.alignmentToolStripMenuItem.DropDown.Renderer = renderer;
						this.displayStyleToolStripMenuItem.DropDown.Renderer = renderer;
						Font font = (Font)iuiservice.Styles["DialogFont"];
						this.alignmentToolStripMenuItem.DropDown.Font = font;
						this.displayStyleToolStripMenuItem.DropDown.Font = font;
						object obj = iuiservice.Styles["VsColorPanelText"];
						if (obj is Color)
						{
							Color foreColor = (Color)obj;
							this.alignmentToolStripMenuItem.DropDown.ForeColor = foreColor;
							this.displayStyleToolStripMenuItem.DropDown.ForeColor = foreColor;
						}
					}
					base.AddRange(new ToolStripItem[]
					{
						this.alignmentToolStripMenuItem,
						this.displayStyleToolStripMenuItem
					});
				}
				this.toolStripSeparator1 = new ToolStripSeparator();
				base.Add(this.toolStripSeparator1);
			}
			this.convertToolStripMenuItem = new ToolStripMenuItem();
			this.convertToolStripMenuItem.Text = SR.GetString("ToolStripItemContextMenuConvertTo");
			this.convertToolStripMenuItem.DropDown = ToolStripDesignerUtils.GetNewItemDropDown(this.ParentTool, this.currentItem, new EventHandler(this.AddNewItemClick), true, this.serviceProvider, true);
			this.insertToolStripMenuItem = new ToolStripMenuItem();
			this.insertToolStripMenuItem.Text = SR.GetString("ToolStripItemContextMenuInsert");
			this.insertToolStripMenuItem.DropDown = ToolStripDesignerUtils.GetNewItemDropDown(this.ParentTool, this.currentItem, new EventHandler(this.AddNewItemClick), false, this.serviceProvider, true);
			base.AddRange(new ToolStripItem[]
			{
				this.convertToolStripMenuItem,
				this.insertToolStripMenuItem
			});
			if (this.currentItem is ToolStripDropDownItem)
			{
				IDesignerHost designerHost = (IDesignerHost)this.serviceProvider.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					ToolStripItemDesigner toolStripItemDesigner = designerHost.GetDesigner(this.currentItem) as ToolStripItemDesigner;
					if (toolStripItemDesigner != null)
					{
						this.verbManager = new CollectionEditVerbManager(SR.GetString("ToolStripDropDownItemCollectionEditorVerb"), toolStripItemDesigner, TypeDescriptor.GetProperties(this.currentItem)["DropDownItems"], false);
						this.editItemsToolStripMenuItem = new ToolStripMenuItem();
						this.editItemsToolStripMenuItem.Text = SR.GetString("ToolStripDropDownItemCollectionEditorVerb");
						this.editItemsToolStripMenuItem.Click += this.OnEditItemsMenuItemClick;
						this.editItemsToolStripMenuItem.Image = new Bitmap(BitmapSelector.GetResourceStream(typeof(ToolStripMenuItem), "editdropdownlist.bmp"));
						this.editItemsToolStripMenuItem.ImageTransparentColor = Color.Magenta;
						base.Add(this.editItemsToolStripMenuItem);
					}
				}
			}
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x000CD801 File Offset: 0x000CBA01
		private void OnEditItemsMenuItemClick(object sender, EventArgs e)
		{
			if (this.verbManager != null)
			{
				this.verbManager.EditItemsVerb.Invoke();
			}
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x000CD81C File Offset: 0x000CBA1C
		private void OnImageToolStripMenuItemClick(object sender, EventArgs e)
		{
			IDesignerHost designerHost = (IDesignerHost)this.serviceProvider.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				ToolStripItemDesigner toolStripItemDesigner = designerHost.GetDesigner(this.currentItem) as ToolStripItemDesigner;
				if (toolStripItemDesigner != null)
				{
					try
					{
						EditorServiceContext.EditValue(toolStripItemDesigner, this.currentItem, "Image");
					}
					catch (InvalidOperationException ex)
					{
						IUIService iuiservice = (IUIService)this.serviceProvider.GetService(typeof(IUIService));
						iuiservice.ShowError(ex.Message);
					}
				}
			}
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x000CD8AC File Offset: 0x000CBAAC
		private void OnBooleanValueChanged(object sender, EventArgs e)
		{
			ToolStripItem toolStripItem = sender as ToolStripItem;
			if (toolStripItem != null)
			{
				string text = toolStripItem.Tag as string;
				if (text != null)
				{
					bool flag = (bool)this.GetProperty(text);
					this.ChangeProperty(text, !flag);
				}
			}
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x000CD8F0 File Offset: 0x000CBAF0
		private void OnEnumValueChanged(object sender, EventArgs e)
		{
			ToolStripItem toolStripItem = sender as ToolStripItem;
			if (toolStripItem != null)
			{
				ToolStripItemCustomMenuItemCollection.EnumValueDescription enumValueDescription = toolStripItem.Tag as ToolStripItemCustomMenuItemCollection.EnumValueDescription;
				if (enumValueDescription != null && !string.IsNullOrEmpty(enumValueDescription.PropertyName))
				{
					this.ChangeProperty(enumValueDescription.PropertyName, enumValueDescription.Value);
				}
			}
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x000CD938 File Offset: 0x000CBB38
		private void AddNewItemClick(object sender, EventArgs e)
		{
			ItemTypeToolStripMenuItem itemTypeToolStripMenuItem = (ItemTypeToolStripMenuItem)sender;
			Type itemType = itemTypeToolStripMenuItem.ItemType;
			if (itemTypeToolStripMenuItem.ConvertTo)
			{
				this.MorphToolStripItem(itemType);
				return;
			}
			this.InsertItem(itemType);
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x000CD96C File Offset: 0x000CBB6C
		private void MorphToolStripItem(Type t)
		{
			if (t != this.currentItem.GetType())
			{
				IDesignerHost designerHost = (IDesignerHost)this.serviceProvider.GetService(typeof(IDesignerHost));
				ToolStripItemDesigner toolStripItemDesigner = (ToolStripItemDesigner)designerHost.GetDesigner(this.currentItem);
				toolStripItemDesigner.MorphCurrentItem(t);
			}
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x000CD9C4 File Offset: 0x000CBBC4
		private void InsertItem(Type t)
		{
			ToolStripMenuItem toolStripMenuItem = this.currentItem as ToolStripMenuItem;
			if (toolStripMenuItem != null)
			{
				this.InsertMenuItem(t);
				return;
			}
			this.InsertStripItem(t);
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x000CD9F0 File Offset: 0x000CBBF0
		private void InsertStripItem(Type t)
		{
			StatusStrip statusStrip = this.ParentTool as StatusStrip;
			if (statusStrip != null)
			{
				this.InsertIntoStatusStrip(statusStrip, t);
				return;
			}
			this.InsertToolStripItem(t);
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x000CDA1C File Offset: 0x000CBC1C
		private void InsertMenuItem(Type t)
		{
			MenuStrip menuStrip = this.ParentTool as MenuStrip;
			if (menuStrip != null)
			{
				this.InsertIntoMainMenu(menuStrip, t);
				return;
			}
			this.InsertIntoDropDown((ToolStripDropDown)this.currentItem.Owner, t);
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x000CDA58 File Offset: 0x000CBC58
		private void TryCancelTransaction(ref DesignerTransaction transaction)
		{
			if (transaction != null)
			{
				try
				{
					transaction.Cancel();
					transaction = null;
				}
				catch
				{
				}
			}
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x000CDA88 File Offset: 0x000CBC88
		private void InsertIntoDropDown(ToolStripDropDown parent, Type t)
		{
			IDesignerHost designerHost = (IDesignerHost)this.serviceProvider.GetService(typeof(IDesignerHost));
			int num = parent.Items.IndexOf(this.currentItem);
			if (parent != null)
			{
				ToolStripDropDownItem toolStripDropDownItem = parent.OwnerItem as ToolStripDropDownItem;
				if (toolStripDropDownItem != null && (toolStripDropDownItem.DropDownDirection == ToolStripDropDownDirection.AboveLeft || toolStripDropDownItem.DropDownDirection == ToolStripDropDownDirection.AboveRight))
				{
					num++;
				}
			}
			DesignerTransaction designerTransaction = designerHost.CreateTransaction(SR.GetString("ToolStripAddingItem"));
			try
			{
				IComponent component = designerHost.CreateComponent(t);
				IDesigner designer = designerHost.GetDesigner(component);
				if (designer is ComponentDesigner)
				{
					((ComponentDesigner)designer).InitializeNewComponent(null);
				}
				parent.Items.Insert(num, (ToolStripItem)component);
				ISelectionService selectionService = (ISelectionService)this.serviceProvider.GetService(typeof(ISelectionService));
				if (selectionService != null)
				{
					selectionService.SetSelectedComponents(new object[]
					{
						component
					}, SelectionTypes.Replace);
				}
			}
			catch (Exception ex)
			{
				if (parent != null && parent.OwnerItem != null && parent.OwnerItem.Owner != null)
				{
					ToolStripDesigner toolStripDesigner = designerHost.GetDesigner(parent.OwnerItem.Owner) as ToolStripDesigner;
					if (toolStripDesigner != null)
					{
						toolStripDesigner.CancelPendingMenuItemTransaction();
					}
				}
				this.TryCancelTransaction(ref designerTransaction);
				if (ClientUtils.IsCriticalException(ex))
				{
					throw;
				}
			}
			finally
			{
				if (designerTransaction != null)
				{
					designerTransaction.Commit();
					designerTransaction = null;
				}
			}
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x000CDBE8 File Offset: 0x000CBDE8
		private void InsertIntoMainMenu(MenuStrip parent, Type t)
		{
			IDesignerHost designerHost = (IDesignerHost)this.serviceProvider.GetService(typeof(IDesignerHost));
			int index = parent.Items.IndexOf(this.currentItem);
			DesignerTransaction designerTransaction = designerHost.CreateTransaction(SR.GetString("ToolStripAddingItem"));
			try
			{
				IComponent component = designerHost.CreateComponent(t);
				IDesigner designer = designerHost.GetDesigner(component);
				if (designer is ComponentDesigner)
				{
					((ComponentDesigner)designer).InitializeNewComponent(null);
				}
				parent.Items.Insert(index, (ToolStripItem)component);
				ISelectionService selectionService = (ISelectionService)this.serviceProvider.GetService(typeof(ISelectionService));
				if (selectionService != null)
				{
					selectionService.SetSelectedComponents(new object[]
					{
						component
					}, SelectionTypes.Replace);
				}
			}
			catch (Exception ex)
			{
				this.TryCancelTransaction(ref designerTransaction);
				if (ClientUtils.IsCriticalException(ex))
				{
					throw;
				}
			}
			finally
			{
				if (designerTransaction != null)
				{
					designerTransaction.Commit();
					designerTransaction = null;
				}
			}
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x000CDCE4 File Offset: 0x000CBEE4
		private void InsertIntoStatusStrip(StatusStrip parent, Type t)
		{
			IDesignerHost designerHost = (IDesignerHost)this.serviceProvider.GetService(typeof(IDesignerHost));
			int index = parent.Items.IndexOf(this.currentItem);
			DesignerTransaction designerTransaction = designerHost.CreateTransaction(SR.GetString("ToolStripAddingItem"));
			try
			{
				IComponent component = designerHost.CreateComponent(t);
				IDesigner designer = designerHost.GetDesigner(component);
				if (designer is ComponentDesigner)
				{
					((ComponentDesigner)designer).InitializeNewComponent(null);
				}
				parent.Items.Insert(index, (ToolStripItem)component);
				ISelectionService selectionService = (ISelectionService)this.serviceProvider.GetService(typeof(ISelectionService));
				if (selectionService != null)
				{
					selectionService.SetSelectedComponents(new object[]
					{
						component
					}, SelectionTypes.Replace);
				}
			}
			catch (Exception ex)
			{
				this.TryCancelTransaction(ref designerTransaction);
				if (ClientUtils.IsCriticalException(ex))
				{
					throw;
				}
			}
			finally
			{
				if (designerTransaction != null)
				{
					designerTransaction.Commit();
					designerTransaction = null;
				}
			}
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x000CDDE0 File Offset: 0x000CBFE0
		private void InsertToolStripItem(Type t)
		{
			IDesignerHost designerHost = (IDesignerHost)this.serviceProvider.GetService(typeof(IDesignerHost));
			ToolStrip parentTool = this.ParentTool;
			int index = parentTool.Items.IndexOf(this.currentItem);
			DesignerTransaction designerTransaction = designerHost.CreateTransaction(SR.GetString("ToolStripAddingItem"));
			try
			{
				IComponent component = designerHost.CreateComponent(t);
				IDesigner designer = designerHost.GetDesigner(component);
				if (designer is ComponentDesigner)
				{
					((ComponentDesigner)designer).InitializeNewComponent(null);
				}
				if (component is ToolStripButton || component is ToolStripSplitButton || component is ToolStripDropDownButton)
				{
					Image value = null;
					try
					{
						value = new Bitmap(typeof(ToolStripButton), "blank.bmp");
					}
					catch (Exception ex)
					{
						if (ClientUtils.IsCriticalException(ex))
						{
							throw;
						}
					}
					this.ChangeProperty(component, "Image", value);
					this.ChangeProperty(component, "DisplayStyle", ToolStripItemDisplayStyle.Image);
					this.ChangeProperty(component, "ImageTransparentColor", Color.Magenta);
				}
				parentTool.Items.Insert(index, (ToolStripItem)component);
				ISelectionService selectionService = (ISelectionService)this.serviceProvider.GetService(typeof(ISelectionService));
				if (selectionService != null)
				{
					selectionService.SetSelectedComponents(new object[]
					{
						component
					}, SelectionTypes.Replace);
				}
			}
			catch (Exception ex2)
			{
				if (designerTransaction != null)
				{
					designerTransaction.Cancel();
					designerTransaction = null;
				}
				if (ClientUtils.IsCriticalException(ex2))
				{
					throw;
				}
			}
			finally
			{
				if (designerTransaction != null)
				{
					designerTransaction.Commit();
					designerTransaction = null;
				}
			}
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x000CDF9C File Offset: 0x000CC19C
		private bool IsPropertyBrowsable(string propertyName)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.currentItem)[propertyName];
			if (propertyDescriptor != null)
			{
				BrowsableAttribute browsableAttribute = propertyDescriptor.Attributes[typeof(BrowsableAttribute)] as BrowsableAttribute;
				if (browsableAttribute != null)
				{
					return browsableAttribute.Browsable;
				}
			}
			return true;
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x000CDFE4 File Offset: 0x000CC1E4
		private object GetProperty(string propertyName)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.currentItem)[propertyName];
			if (propertyDescriptor != null)
			{
				return propertyDescriptor.GetValue(this.currentItem);
			}
			return null;
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x000CE014 File Offset: 0x000CC214
		protected void ChangeProperty(string propertyName, object value)
		{
			this.ChangeProperty(this.currentItem, propertyName, value);
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x000CE024 File Offset: 0x000CC224
		protected void ChangeProperty(IComponent target, string propertyName, object value)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(target)[propertyName];
			try
			{
				if (propertyDescriptor != null)
				{
					propertyDescriptor.SetValue(target, value);
				}
			}
			catch (InvalidOperationException ex)
			{
				IUIService iuiservice = (IUIService)this.serviceProvider.GetService(typeof(IUIService));
				iuiservice.ShowError(ex.Message);
			}
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x000CE088 File Offset: 0x000CC288
		private void RefreshAlignment()
		{
			ToolStripItemAlignment toolStripItemAlignment = (ToolStripItemAlignment)this.GetProperty("Alignment");
			this.leftToolStripMenuItem.Checked = (toolStripItemAlignment == ToolStripItemAlignment.Left);
			this.rightToolStripMenuItem.Checked = (toolStripItemAlignment == ToolStripItemAlignment.Right);
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x000CE0CC File Offset: 0x000CC2CC
		private void RefreshDisplayStyle()
		{
			ToolStripItemDisplayStyle toolStripItemDisplayStyle = (ToolStripItemDisplayStyle)this.GetProperty("DisplayStyle");
			this.noneStyleToolStripMenuItem.Checked = (toolStripItemDisplayStyle == ToolStripItemDisplayStyle.None);
			this.textStyleToolStripMenuItem.Checked = (toolStripItemDisplayStyle == ToolStripItemDisplayStyle.Text);
			this.imageStyleToolStripMenuItem.Checked = (toolStripItemDisplayStyle == ToolStripItemDisplayStyle.Image);
			this.imageTextStyleToolStripMenuItem.Checked = (toolStripItemDisplayStyle == ToolStripItemDisplayStyle.ImageAndText);
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x000CE138 File Offset: 0x000CC338
		public override void RefreshItems()
		{
			base.RefreshItems();
			ToolStripItem toolStripItem = this.currentItem;
			if (!(toolStripItem is ToolStripControlHost) && !(toolStripItem is ToolStripSeparator))
			{
				this.enabledToolStripMenuItem.Checked = (bool)this.GetProperty("Enabled");
				if (toolStripItem is ToolStripMenuItem)
				{
					this.checkedToolStripMenuItem.Checked = (bool)this.GetProperty("Checked");
					this.showShortcutKeysToolStripMenuItem.Checked = (bool)this.GetProperty("ShowShortcutKeys");
					return;
				}
				if (toolStripItem is ToolStripLabel)
				{
					this.isLinkToolStripMenuItem.Checked = (bool)this.GetProperty("IsLink");
				}
				this.RefreshAlignment();
				this.RefreshDisplayStyle();
			}
		}

		// Token: 0x04001950 RID: 6480
		private ToolStripItem currentItem;

		// Token: 0x04001951 RID: 6481
		private IServiceProvider serviceProvider;

		// Token: 0x04001952 RID: 6482
		private ToolStripMenuItem imageToolStripMenuItem;

		// Token: 0x04001953 RID: 6483
		private ToolStripMenuItem enabledToolStripMenuItem;

		// Token: 0x04001954 RID: 6484
		private ToolStripMenuItem isLinkToolStripMenuItem;

		// Token: 0x04001955 RID: 6485
		private ToolStripMenuItem springToolStripMenuItem;

		// Token: 0x04001956 RID: 6486
		private ToolStripMenuItem checkedToolStripMenuItem;

		// Token: 0x04001957 RID: 6487
		private ToolStripMenuItem showShortcutKeysToolStripMenuItem;

		// Token: 0x04001958 RID: 6488
		private ToolStripMenuItem alignmentToolStripMenuItem;

		// Token: 0x04001959 RID: 6489
		private ToolStripMenuItem displayStyleToolStripMenuItem;

		// Token: 0x0400195A RID: 6490
		private ToolStripSeparator toolStripSeparator1;

		// Token: 0x0400195B RID: 6491
		private ToolStripMenuItem convertToolStripMenuItem;

		// Token: 0x0400195C RID: 6492
		private ToolStripMenuItem insertToolStripMenuItem;

		// Token: 0x0400195D RID: 6493
		private ToolStripMenuItem leftToolStripMenuItem;

		// Token: 0x0400195E RID: 6494
		private ToolStripMenuItem rightToolStripMenuItem;

		// Token: 0x0400195F RID: 6495
		private ToolStripMenuItem noneStyleToolStripMenuItem;

		// Token: 0x04001960 RID: 6496
		private ToolStripMenuItem textStyleToolStripMenuItem;

		// Token: 0x04001961 RID: 6497
		private ToolStripMenuItem imageStyleToolStripMenuItem;

		// Token: 0x04001962 RID: 6498
		private ToolStripMenuItem imageTextStyleToolStripMenuItem;

		// Token: 0x04001963 RID: 6499
		private ToolStripMenuItem editItemsToolStripMenuItem;

		// Token: 0x04001964 RID: 6500
		private CollectionEditVerbManager verbManager;

		// Token: 0x02000597 RID: 1431
		private class EnumValueDescription
		{
			// Token: 0x06003343 RID: 13123 RVA: 0x001182D0 File Offset: 0x001164D0
			public EnumValueDescription(string propertyName, object value)
			{
				this.PropertyName = propertyName;
				this.Value = value;
			}

			// Token: 0x04002250 RID: 8784
			public string PropertyName;

			// Token: 0x04002251 RID: 8785
			public object Value;
		}
	}
}
