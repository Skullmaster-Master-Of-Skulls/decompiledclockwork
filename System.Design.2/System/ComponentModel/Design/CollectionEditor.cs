using System;
using System.Collections;
using System.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.VisualStyles;

namespace System.ComponentModel.Design
{
	// Token: 0x02000198 RID: 408
	public class CollectionEditor : UITypeEditor
	{
		// Token: 0x06000ED7 RID: 3799 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void CancelChanges()
		{
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0005611A File Offset: 0x0005431A
		public CollectionEditor(Type type)
		{
			this.type = type;
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x00056129 File Offset: 0x00054329
		protected Type CollectionItemType
		{
			get
			{
				if (this.collectionItemType == null)
				{
					this.collectionItemType = this.CreateCollectionItemType();
				}
				return this.collectionItemType;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x0005614B File Offset: 0x0005434B
		protected Type CollectionType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x00056153 File Offset: 0x00054353
		protected ITypeDescriptorContext Context
		{
			get
			{
				return this.currentContext;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x0005615B File Offset: 0x0005435B
		protected Type[] NewItemTypes
		{
			get
			{
				if (this.newItemTypes == null)
				{
					this.newItemTypes = this.CreateNewItemTypes();
				}
				return this.newItemTypes;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x00028BA3 File Offset: 0x00026DA3
		protected virtual string HelpTopic
		{
			get
			{
				return "net.ComponentModel.CollectionEditor";
			}
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x00056178 File Offset: 0x00054378
		protected virtual bool CanRemoveInstance(object value)
		{
			IComponent component = value as IComponent;
			if (component != null)
			{
				InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(component)[typeof(InheritanceAttribute)];
				if (inheritanceAttribute != null && inheritanceAttribute.InheritanceLevel != InheritanceLevel.NotInherited)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected virtual bool CanSelectMultipleInstances()
		{
			return true;
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x000561B9 File Offset: 0x000543B9
		protected virtual CollectionEditor.CollectionForm CreateCollectionForm()
		{
			return new CollectionEditor.CollectionEditorCollectionForm(this);
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x000561C1 File Offset: 0x000543C1
		protected virtual object CreateInstance(Type itemType)
		{
			return CollectionEditor.CreateInstance(itemType, (IDesignerHost)this.GetService(typeof(IDesignerHost)), null);
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x000561E0 File Offset: 0x000543E0
		protected virtual IList GetObjectsFromInstance(object instance)
		{
			return new ArrayList
			{
				instance
			};
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x000561FC File Offset: 0x000543FC
		internal static object CreateInstance(Type itemType, IDesignerHost host, string name)
		{
			object obj = null;
			if (typeof(IComponent).IsAssignableFrom(itemType) && host != null)
			{
				obj = host.CreateComponent(itemType, name);
				if (host != null)
				{
					IComponentInitializer componentInitializer = host.GetDesigner((IComponent)obj) as IComponentInitializer;
					if (componentInitializer != null)
					{
						componentInitializer.InitializeNewComponent(null);
					}
				}
			}
			if (obj == null)
			{
				obj = TypeDescriptor.CreateInstance(host, itemType, null, null);
			}
			return obj;
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00056258 File Offset: 0x00054458
		protected virtual string GetDisplayText(object value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(value)["Name"];
			string text;
			if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(string))
			{
				text = (string)propertyDescriptor.GetValue(value);
				if (text != null && text.Length > 0)
				{
					return text;
				}
			}
			propertyDescriptor = TypeDescriptor.GetDefaultProperty(this.CollectionType);
			if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(string))
			{
				text = (string)propertyDescriptor.GetValue(value);
				if (text != null && text.Length > 0)
				{
					return text;
				}
			}
			text = TypeDescriptor.GetConverter(value).ConvertToString(value);
			if (text == null || text.Length == 0)
			{
				text = value.GetType().Name;
			}
			return text;
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0005631C File Offset: 0x0005451C
		protected virtual Type CreateCollectionItemType()
		{
			PropertyInfo[] properties = TypeDescriptor.GetReflectionType(this.CollectionType).GetProperties(BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < properties.Length; i++)
			{
				if (properties[i].Name.Equals("Item") || properties[i].Name.Equals("Items"))
				{
					return properties[i].PropertyType;
				}
			}
			return typeof(object);
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x00056385 File Offset: 0x00054585
		protected virtual Type[] CreateNewItemTypes()
		{
			return new Type[]
			{
				this.CollectionItemType
			};
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00056398 File Offset: 0x00054598
		protected virtual void DestroyInstance(object instance)
		{
			IComponent component = instance as IComponent;
			if (component == null)
			{
				IDisposable disposable = instance as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
				return;
			}
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				designerHost.DestroyComponent(component);
				return;
			}
			component.Dispose();
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x000563E8 File Offset: 0x000545E8
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService != null)
				{
					this.currentContext = context;
					CollectionEditor.CollectionForm collectionForm = DpiHelper.CreateInstanceInSystemAwareContext<CollectionEditor.CollectionForm>(() => this.CreateCollectionForm());
					ITypeDescriptorContext typeDescriptorContext = this.currentContext;
					collectionForm.EditValue = value;
					this.ignoreChangingEvents = false;
					this.ignoreChangedEvents = false;
					DesignerTransaction designerTransaction = null;
					bool flag = true;
					IComponentChangeService componentChangeService = null;
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					try
					{
						try
						{
							if (designerHost != null)
							{
								designerTransaction = designerHost.CreateTransaction(SR.GetString("CollectionEditorUndoBatchDesc", new object[]
								{
									this.CollectionItemType.Name
								}));
							}
						}
						catch (CheckoutException ex)
						{
							if (ex == CheckoutException.Canceled)
							{
								return value;
							}
							throw ex;
						}
						componentChangeService = ((designerHost != null) ? ((IComponentChangeService)designerHost.GetService(typeof(IComponentChangeService))) : null);
						if (componentChangeService != null)
						{
							componentChangeService.ComponentChanged += this.OnComponentChanged;
							componentChangeService.ComponentChanging += this.OnComponentChanging;
						}
						if (collectionForm.ShowEditorDialog(windowsFormsEditorService) == DialogResult.OK)
						{
							value = collectionForm.EditValue;
						}
						else
						{
							flag = false;
						}
					}
					finally
					{
						collectionForm.EditValue = null;
						this.currentContext = typeDescriptorContext;
						if (designerTransaction != null)
						{
							if (flag)
							{
								designerTransaction.Commit();
							}
							else
							{
								designerTransaction.Cancel();
							}
						}
						if (componentChangeService != null)
						{
							componentChangeService.ComponentChanged -= this.OnComponentChanged;
							componentChangeService.ComponentChanging -= this.OnComponentChanging;
						}
						collectionForm.Dispose();
					}
					return value;
				}
			}
			return value;
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00009D4C File Offset: 0x00007F4C
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00056584 File Offset: 0x00054784
		private bool IsAnyObjectInheritedReadOnly(object[] items)
		{
			IInheritanceService inheritanceService = null;
			bool flag = false;
			foreach (object obj in items)
			{
				IComponent component = obj as IComponent;
				if (component != null && component.Site == null)
				{
					if (!flag)
					{
						flag = true;
						if (this.Context != null)
						{
							inheritanceService = (IInheritanceService)this.Context.GetService(typeof(IInheritanceService));
						}
					}
					if (inheritanceService != null && inheritanceService.GetInheritanceAttribute(component).Equals(InheritanceAttribute.InheritedReadOnly))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x00056604 File Offset: 0x00054804
		protected virtual object[] GetItems(object editValue)
		{
			if (editValue != null && editValue is ICollection)
			{
				ArrayList arrayList = new ArrayList();
				ICollection collection = (ICollection)editValue;
				foreach (object value in collection)
				{
					arrayList.Add(value);
				}
				object[] array = new object[arrayList.Count];
				arrayList.CopyTo(array, 0);
				return array;
			}
			return new object[0];
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00056690 File Offset: 0x00054890
		protected object GetService(Type serviceType)
		{
			if (this.Context != null)
			{
				return this.Context.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x000566A8 File Offset: 0x000548A8
		private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			if (!this.ignoreChangedEvents && sender != this.Context.Instance)
			{
				this.ignoreChangedEvents = true;
				this.Context.OnComponentChanged();
			}
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x000566D2 File Offset: 0x000548D2
		private void OnComponentChanging(object sender, ComponentChangingEventArgs e)
		{
			if (!this.ignoreChangingEvents && sender != this.Context.Instance)
			{
				this.ignoreChangingEvents = true;
				this.Context.OnComponentChanging();
			}
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x00003937 File Offset: 0x00001B37
		internal virtual void OnItemRemoving(object item)
		{
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00056700 File Offset: 0x00054900
		protected virtual object SetItems(object editValue, object[] value)
		{
			if (editValue != null && editValue is IList)
			{
				IList list = (IList)editValue;
				list.Clear();
				for (int i = 0; i < value.Length; i++)
				{
					list.Add(value[i]);
				}
			}
			return editValue;
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00056740 File Offset: 0x00054940
		protected virtual void ShowHelp()
		{
			IHelpService helpService = this.GetService(typeof(IHelpService)) as IHelpService;
			if (helpService != null)
			{
				helpService.ShowHelpFromKeyword(this.HelpTopic);
			}
		}

		// Token: 0x040008D5 RID: 2261
		private Type type;

		// Token: 0x040008D6 RID: 2262
		private Type collectionItemType;

		// Token: 0x040008D7 RID: 2263
		private Type[] newItemTypes;

		// Token: 0x040008D8 RID: 2264
		private ITypeDescriptorContext currentContext;

		// Token: 0x040008D9 RID: 2265
		private bool ignoreChangedEvents;

		// Token: 0x040008DA RID: 2266
		private bool ignoreChangingEvents;

		// Token: 0x0200047C RID: 1148
		internal class SplitButton : Button
		{
			// Token: 0x06002A3D RID: 10813 RVA: 0x000FD90C File Offset: 0x000FBB0C
			public SplitButton()
			{
				if (!CollectionEditor.SplitButton.isScalingInitialized)
				{
					if (DpiHelper.IsScalingRequired)
					{
						CollectionEditor.SplitButton.offset2X = DpiHelper.LogicalToDeviceUnitsX(2);
						CollectionEditor.SplitButton.offset2Y = DpiHelper.LogicalToDeviceUnitsY(2);
					}
					CollectionEditor.SplitButton.isScalingInitialized = true;
				}
			}

			// Token: 0x170008F4 RID: 2292
			// (set) Token: 0x06002A3E RID: 10814 RVA: 0x000FD93E File Offset: 0x000FBB3E
			public bool ShowSplit
			{
				set
				{
					if (value != this.showSplit)
					{
						this.showSplit = value;
						base.Invalidate();
					}
				}
			}

			// Token: 0x170008F5 RID: 2293
			// (get) Token: 0x06002A3F RID: 10815 RVA: 0x000FD956 File Offset: 0x000FBB56
			// (set) Token: 0x06002A40 RID: 10816 RVA: 0x000FD95E File Offset: 0x000FBB5E
			private PushButtonState State
			{
				get
				{
					return this._state;
				}
				set
				{
					if (!this._state.Equals(value))
					{
						this._state = value;
						base.Invalidate();
					}
				}
			}

			// Token: 0x06002A41 RID: 10817 RVA: 0x000FD988 File Offset: 0x000FBB88
			public override Size GetPreferredSize(Size proposedSize)
			{
				Size preferredSize = base.GetPreferredSize(proposedSize);
				if (this.showSplit && !string.IsNullOrEmpty(this.Text) && TextRenderer.MeasureText(this.Text, this.Font).Width + 14 > preferredSize.Width)
				{
					return preferredSize + new Size(14, 0);
				}
				return preferredSize;
			}

			// Token: 0x06002A42 RID: 10818 RVA: 0x000FD9E7 File Offset: 0x000FBBE7
			protected override bool IsInputKey(Keys keyData)
			{
				return (keyData.Equals(Keys.Down) && this.showSplit) || base.IsInputKey(keyData);
			}

			// Token: 0x06002A43 RID: 10819 RVA: 0x000FDA10 File Offset: 0x000FBC10
			protected override void OnGotFocus(EventArgs e)
			{
				if (!this.showSplit)
				{
					base.OnGotFocus(e);
					return;
				}
				if (!this.State.Equals(PushButtonState.Pressed) && !this.State.Equals(PushButtonState.Disabled))
				{
					this.State = PushButtonState.Default;
				}
			}

			// Token: 0x06002A44 RID: 10820 RVA: 0x000FDA6C File Offset: 0x000FBC6C
			protected override void OnKeyDown(KeyEventArgs kevent)
			{
				if (kevent.KeyCode.Equals(Keys.Down) && this.showSplit)
				{
					this.ShowContextMenuStrip();
					return;
				}
				base.OnKeyDown(kevent);
			}

			// Token: 0x06002A45 RID: 10821 RVA: 0x000FDAAC File Offset: 0x000FBCAC
			protected override void OnLostFocus(EventArgs e)
			{
				if (!this.showSplit)
				{
					base.OnLostFocus(e);
					return;
				}
				if (!this.State.Equals(PushButtonState.Pressed) && !this.State.Equals(PushButtonState.Disabled))
				{
					this.State = PushButtonState.Normal;
				}
			}

			// Token: 0x06002A46 RID: 10822 RVA: 0x000FDB08 File Offset: 0x000FBD08
			protected override void OnMouseDown(MouseEventArgs e)
			{
				if (!this.showSplit)
				{
					base.OnMouseDown(e);
					return;
				}
				if (this.dropDownRectangle.Contains(e.Location))
				{
					this.ShowContextMenuStrip();
					return;
				}
				this.State = PushButtonState.Pressed;
			}

			// Token: 0x06002A47 RID: 10823 RVA: 0x000FDB3C File Offset: 0x000FBD3C
			protected override void OnMouseEnter(EventArgs e)
			{
				if (!this.showSplit)
				{
					base.OnMouseEnter(e);
					return;
				}
				if (!this.State.Equals(PushButtonState.Pressed) && !this.State.Equals(PushButtonState.Disabled))
				{
					this.State = PushButtonState.Hot;
				}
			}

			// Token: 0x06002A48 RID: 10824 RVA: 0x000FDB98 File Offset: 0x000FBD98
			protected override void OnMouseLeave(EventArgs e)
			{
				if (!this.showSplit)
				{
					base.OnMouseLeave(e);
					return;
				}
				if (!this.State.Equals(PushButtonState.Pressed) && !this.State.Equals(PushButtonState.Disabled))
				{
					if (this.Focused)
					{
						this.State = PushButtonState.Default;
						return;
					}
					this.State = PushButtonState.Normal;
				}
			}

			// Token: 0x06002A49 RID: 10825 RVA: 0x000FDC04 File Offset: 0x000FBE04
			protected override void OnMouseUp(MouseEventArgs mevent)
			{
				if (!this.showSplit)
				{
					base.OnMouseUp(mevent);
					return;
				}
				if (this.ContextMenuStrip == null || !this.ContextMenuStrip.Visible)
				{
					this.SetButtonDrawState();
					if (base.Bounds.Contains(base.Parent.PointToClient(Cursor.Position)) && !this.dropDownRectangle.Contains(mevent.Location))
					{
						this.OnClick(new EventArgs());
					}
				}
			}

			// Token: 0x06002A4A RID: 10826 RVA: 0x000FDC7C File Offset: 0x000FBE7C
			protected override void OnPaint(PaintEventArgs pevent)
			{
				base.OnPaint(pevent);
				if (!this.showSplit)
				{
					return;
				}
				Graphics graphics = pevent.Graphics;
				Rectangle bounds = new Rectangle(0, 0, base.Width, base.Height);
				TextFormatFlags textFormatFlags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
				ButtonRenderer.DrawButton(graphics, bounds, this.State);
				this.dropDownRectangle = new Rectangle(bounds.Right - 14 - 1, 4, 14, bounds.Height - 8);
				if (this.RightToLeft == RightToLeft.Yes)
				{
					this.dropDownRectangle.X = bounds.Left + 1;
					graphics.DrawLine(SystemPens.ButtonHighlight, bounds.Left + 14, 4, bounds.Left + 14, bounds.Bottom - 4);
					graphics.DrawLine(SystemPens.ButtonHighlight, bounds.Left + 14 + 1, 4, bounds.Left + 14 + 1, bounds.Bottom - 4);
					bounds.Offset(14, 0);
					bounds.Width -= 14;
				}
				else
				{
					graphics.DrawLine(SystemPens.ButtonHighlight, bounds.Right - 14, 4, bounds.Right - 14, bounds.Bottom - 4);
					graphics.DrawLine(SystemPens.ButtonHighlight, bounds.Right - 14 - 1, 4, bounds.Right - 14 - 1, bounds.Bottom - 4);
					bounds.Width -= 14;
				}
				this.PaintArrow(graphics, this.dropDownRectangle);
				if (!base.UseMnemonic)
				{
					textFormatFlags |= TextFormatFlags.NoPrefix;
				}
				else if (!this.ShowKeyboardCues)
				{
					textFormatFlags |= TextFormatFlags.HidePrefix;
				}
				if (!string.IsNullOrEmpty(this.Text))
				{
					TextRenderer.DrawText(graphics, this.Text, this.Font, bounds, SystemColors.ControlText, textFormatFlags);
				}
				if (this.Focused)
				{
					bounds.Inflate(-4, -4);
				}
			}

			// Token: 0x06002A4B RID: 10827 RVA: 0x000FDE4C File Offset: 0x000FC04C
			private void PaintArrow(Graphics g, Rectangle dropDownRect)
			{
				Point point = new Point(Convert.ToInt32(dropDownRect.Left + dropDownRect.Width / 2), Convert.ToInt32(dropDownRect.Top + dropDownRect.Height / 2));
				point.X += dropDownRect.Width % 2;
				Point[] points = new Point[]
				{
					new Point(point.X - CollectionEditor.SplitButton.offset2X, point.Y - 1),
					new Point(point.X + CollectionEditor.SplitButton.offset2X + 1, point.Y - 1),
					new Point(point.X, point.Y + CollectionEditor.SplitButton.offset2Y)
				};
				g.FillPolygon(SystemBrushes.ControlText, points);
			}

			// Token: 0x06002A4C RID: 10828 RVA: 0x000FDF1C File Offset: 0x000FC11C
			private void ShowContextMenuStrip()
			{
				this.State = PushButtonState.Pressed;
				if (this.ContextMenuStrip != null)
				{
					this.ContextMenuStrip.Closed += this.ContextMenuStrip_Closed;
					this.ContextMenuStrip.Show(this, 0, base.Height);
				}
			}

			// Token: 0x06002A4D RID: 10829 RVA: 0x000FDF58 File Offset: 0x000FC158
			private void ContextMenuStrip_Closed(object sender, ToolStripDropDownClosedEventArgs e)
			{
				ContextMenuStrip contextMenuStrip = sender as ContextMenuStrip;
				if (contextMenuStrip != null)
				{
					contextMenuStrip.Closed -= this.ContextMenuStrip_Closed;
				}
				this.SetButtonDrawState();
			}

			// Token: 0x06002A4E RID: 10830 RVA: 0x000FDF88 File Offset: 0x000FC188
			private void SetButtonDrawState()
			{
				if (base.Bounds.Contains(base.Parent.PointToClient(Cursor.Position)))
				{
					this.State = PushButtonState.Hot;
					return;
				}
				if (this.Focused)
				{
					this.State = PushButtonState.Default;
					return;
				}
				this.State = PushButtonState.Normal;
			}

			// Token: 0x04001DA8 RID: 7592
			private PushButtonState _state;

			// Token: 0x04001DA9 RID: 7593
			private const int pushButtonWidth = 14;

			// Token: 0x04001DAA RID: 7594
			private Rectangle dropDownRectangle;

			// Token: 0x04001DAB RID: 7595
			private bool showSplit;

			// Token: 0x04001DAC RID: 7596
			private static bool isScalingInitialized = false;

			// Token: 0x04001DAD RID: 7597
			private const int OFFSET_2PIXELS = 2;

			// Token: 0x04001DAE RID: 7598
			private static int offset2X = 2;

			// Token: 0x04001DAF RID: 7599
			private static int offset2Y = 2;
		}

		// Token: 0x0200047D RID: 1149
		private class CollectionEditorCollectionForm : CollectionEditor.CollectionForm
		{
			// Token: 0x06002A50 RID: 10832 RVA: 0x000FDFE8 File Offset: 0x000FC1E8
			public CollectionEditorCollectionForm(CollectionEditor editor) : base(editor)
			{
				this.editor = editor;
				this.InitializeComponent();
				if (DpiHelper.IsScalingRequired)
				{
					DpiHelper.ScaleButtonImageLogicalToDevice(this.downButton);
					DpiHelper.ScaleButtonImageLogicalToDevice(this.upButton);
				}
				this.Text = SR.GetString("CollectionEditorCaption", new object[]
				{
					base.CollectionItemType.Name
				});
				this.HookEvents();
				Type[] newItemTypes = base.NewItemTypes;
				if (newItemTypes.Length > 1)
				{
					EventHandler handler = new EventHandler(this.AddDownMenu_click);
					this.addButton.ShowSplit = true;
					this.addDownMenu = new ContextMenuStrip();
					this.addButton.ContextMenuStrip = this.addDownMenu;
					for (int i = 0; i < newItemTypes.Length; i++)
					{
						this.addDownMenu.Items.Add(new CollectionEditor.CollectionEditorCollectionForm.TypeMenuItem(newItemTypes[i], handler));
					}
				}
				this.AdjustListBoxItemHeight();
			}

			// Token: 0x170008F6 RID: 2294
			// (get) Token: 0x06002A51 RID: 10833 RVA: 0x000FE0C0 File Offset: 0x000FC2C0
			private bool IsImmutable
			{
				get
				{
					foreach (object obj in this.listbox.SelectedItems)
					{
						CollectionEditor.CollectionEditorCollectionForm.ListItem listItem = (CollectionEditor.CollectionEditorCollectionForm.ListItem)obj;
						Type type = listItem.Value.GetType();
						if (!TypeDescriptor.GetConverter(type).GetCreateInstanceSupported())
						{
							foreach (object obj2 in TypeDescriptor.GetProperties(type))
							{
								PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj2;
								if (!propertyDescriptor.IsReadOnly)
								{
									return false;
								}
							}
						}
					}
					return true;
				}
			}

			// Token: 0x06002A52 RID: 10834 RVA: 0x000FE18C File Offset: 0x000FC38C
			private void AddButton_click(object sender, EventArgs e)
			{
				this.PerformAdd();
			}

			// Token: 0x06002A53 RID: 10835 RVA: 0x000FE194 File Offset: 0x000FC394
			private void AddDownMenu_click(object sender, EventArgs e)
			{
				if (sender is CollectionEditor.CollectionEditorCollectionForm.TypeMenuItem)
				{
					CollectionEditor.CollectionEditorCollectionForm.TypeMenuItem typeMenuItem = (CollectionEditor.CollectionEditorCollectionForm.TypeMenuItem)sender;
					this.CreateAndAddInstance(typeMenuItem.ItemType);
				}
			}

			// Token: 0x06002A54 RID: 10836 RVA: 0x000FE1BC File Offset: 0x000FC3BC
			private void AddItems(IList instances)
			{
				if (this.createdItems == null)
				{
					this.createdItems = new ArrayList();
				}
				this.listbox.BeginUpdate();
				try
				{
					foreach (object obj in instances)
					{
						if (obj != null)
						{
							this.dirty = true;
							this.createdItems.Add(obj);
							CollectionEditor.CollectionEditorCollectionForm.ListItem item = new CollectionEditor.CollectionEditorCollectionForm.ListItem(this.editor, obj);
							this.listbox.Items.Add(item);
						}
					}
				}
				finally
				{
					this.listbox.EndUpdate();
				}
				if (instances.Count == 1)
				{
					this.UpdateItemWidths(this.listbox.Items[this.listbox.Items.Count - 1] as CollectionEditor.CollectionEditorCollectionForm.ListItem);
				}
				else
				{
					this.UpdateItemWidths(null);
				}
				this.SuspendEnabledUpdates();
				try
				{
					this.listbox.ClearSelected();
					this.listbox.SelectedIndex = this.listbox.Items.Count - 1;
					object[] array = new object[this.listbox.Items.Count];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = ((CollectionEditor.CollectionEditorCollectionForm.ListItem)this.listbox.Items[i]).Value;
					}
					base.Items = array;
					if (this.listbox.Items.Count > 0 && this.listbox.SelectedIndex != this.listbox.Items.Count - 1)
					{
						this.listbox.ClearSelected();
						this.listbox.SelectedIndex = this.listbox.Items.Count - 1;
					}
				}
				finally
				{
					this.ResumeEnabledUpdates(true);
				}
			}

			// Token: 0x06002A55 RID: 10837 RVA: 0x000FE3A0 File Offset: 0x000FC5A0
			private void AdjustListBoxItemHeight()
			{
				this.listbox.ItemHeight = this.Font.Height + SystemInformation.BorderSize.Width * 2;
			}

			// Token: 0x06002A56 RID: 10838 RVA: 0x000FE3D3 File Offset: 0x000FC5D3
			private bool AllowRemoveInstance(object value)
			{
				return (this.createdItems != null && this.createdItems.Contains(value)) || base.CanRemoveInstance(value);
			}

			// Token: 0x06002A57 RID: 10839 RVA: 0x000FE3F4 File Offset: 0x000FC5F4
			private int CalcItemWidth(Graphics g, CollectionEditor.CollectionEditorCollectionForm.ListItem item)
			{
				int num = this.listbox.Items.Count;
				if (num < 2)
				{
					num = 2;
				}
				SizeF sizeF = g.MeasureString(num.ToString(CultureInfo.CurrentCulture), this.listbox.Font);
				int num2 = (int)(Math.Log((double)(num - 1)) / CollectionEditor.CollectionEditorCollectionForm.LOG10) + 1;
				int num3 = 4 + num2 * (this.Font.Height / 2);
				num3 = Math.Max(num3, (int)Math.Ceiling((double)sizeF.Width));
				num3 += SystemInformation.BorderSize.Width * 4;
				SizeF sizeF2 = g.MeasureString(this.GetDisplayText(item), this.listbox.Font);
				int num4 = 0;
				if (item.Editor != null && item.Editor.GetPaintValueSupported())
				{
					num4 = 21;
				}
				return (int)Math.Ceiling((double)sizeF2.Width) + num3 + num4 + SystemInformation.BorderSize.Width * 4;
			}

			// Token: 0x06002A58 RID: 10840 RVA: 0x000FE4E0 File Offset: 0x000FC6E0
			private void CancelButton_click(object sender, EventArgs e)
			{
				try
				{
					this.editor.CancelChanges();
					if (this.CollectionEditable && this.dirty)
					{
						this.dirty = false;
						this.listbox.Items.Clear();
						if (this.createdItems != null)
						{
							object[] array = this.createdItems.ToArray();
							if (array.Length != 0 && array[0] is IComponent && ((IComponent)array[0]).Site != null)
							{
								return;
							}
							for (int i = 0; i < array.Length; i++)
							{
								base.DestroyInstance(array[i]);
							}
							this.createdItems.Clear();
						}
						if (this.removedItems != null)
						{
							this.removedItems.Clear();
						}
						if (this.originalItems != null && this.originalItems.Count > 0)
						{
							object[] array2 = new object[this.originalItems.Count];
							for (int j = 0; j < this.originalItems.Count; j++)
							{
								array2[j] = this.originalItems[j];
							}
							base.Items = array2;
							this.originalItems.Clear();
						}
						else
						{
							base.Items = new object[0];
						}
					}
				}
				catch (Exception e2)
				{
					base.DialogResult = DialogResult.None;
					this.DisplayError(e2);
				}
			}

			// Token: 0x06002A59 RID: 10841 RVA: 0x000FE62C File Offset: 0x000FC82C
			private void CreateAndAddInstance(Type type)
			{
				try
				{
					object instance = base.CreateInstance(type);
					IList objectsFromInstance = this.editor.GetObjectsFromInstance(instance);
					if (objectsFromInstance != null)
					{
						this.AddItems(objectsFromInstance);
					}
				}
				catch (Exception e)
				{
					this.DisplayError(e);
				}
			}

			// Token: 0x06002A5A RID: 10842 RVA: 0x000FE674 File Offset: 0x000FC874
			private void DownButton_click(object sender, EventArgs e)
			{
				try
				{
					this.SuspendEnabledUpdates();
					this.dirty = true;
					int selectedIndex = this.listbox.SelectedIndex;
					if (selectedIndex != this.listbox.Items.Count - 1)
					{
						int topIndex = this.listbox.TopIndex;
						object value = this.listbox.Items[selectedIndex];
						this.listbox.Items[selectedIndex] = this.listbox.Items[selectedIndex + 1];
						this.listbox.Items[selectedIndex + 1] = value;
						if (topIndex < this.listbox.Items.Count - 1)
						{
							this.listbox.TopIndex = topIndex + 1;
						}
						this.listbox.ClearSelected();
						this.listbox.SelectedIndex = selectedIndex + 1;
						Control control = (Control)sender;
						if (control.Enabled)
						{
							control.Focus();
						}
					}
				}
				finally
				{
					this.ResumeEnabledUpdates(true);
				}
			}

			// Token: 0x06002A5B RID: 10843 RVA: 0x000FE778 File Offset: 0x000FC978
			private void CollectionEditor_HelpButtonClicked(object sender, CancelEventArgs e)
			{
				e.Cancel = true;
				this.editor.ShowHelp();
			}

			// Token: 0x06002A5C RID: 10844 RVA: 0x000FE78C File Offset: 0x000FC98C
			private void Form_HelpRequested(object sender, HelpEventArgs e)
			{
				this.editor.ShowHelp();
			}

			// Token: 0x06002A5D RID: 10845 RVA: 0x000FE799 File Offset: 0x000FC999
			private string GetDisplayText(CollectionEditor.CollectionEditorCollectionForm.ListItem item)
			{
				if (item != null)
				{
					return item.ToString();
				}
				return string.Empty;
			}

			// Token: 0x06002A5E RID: 10846 RVA: 0x000FE7AC File Offset: 0x000FC9AC
			private void HookEvents()
			{
				this.listbox.KeyDown += this.Listbox_keyDown;
				this.listbox.DrawItem += this.Listbox_drawItem;
				this.listbox.SelectedIndexChanged += this.Listbox_selectedIndexChanged;
				this.listbox.HandleCreated += this.Listbox_handleCreated;
				this.upButton.Click += this.UpButton_click;
				this.downButton.Click += this.DownButton_click;
				this.propertyBrowser.PropertyValueChanged += this.PropertyGrid_propertyValueChanged;
				this.addButton.Click += this.AddButton_click;
				this.removeButton.Click += this.RemoveButton_click;
				this.okButton.Click += this.OKButton_click;
				this.cancelButton.Click += this.CancelButton_click;
				base.HelpButtonClicked += this.CollectionEditor_HelpButtonClicked;
				base.HelpRequested += this.Form_HelpRequested;
				base.Shown += this.Form_Shown;
			}

			// Token: 0x06002A5F RID: 10847 RVA: 0x000FE8EC File Offset: 0x000FCAEC
			private void InitializeComponent()
			{
				ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(CollectionEditor));
				this.membersLabel = new Label();
				this.listbox = new CollectionEditor.FilterListBox();
				this.upButton = new Button();
				this.downButton = new Button();
				this.propertiesLabel = new Label();
				this.propertyBrowser = new VsPropertyGrid(base.Context);
				this.addButton = new CollectionEditor.SplitButton();
				this.removeButton = new Button();
				this.okButton = new Button();
				this.cancelButton = new Button();
				this.okCancelTableLayoutPanel = new TableLayoutPanel();
				this.overArchingTableLayoutPanel = new TableLayoutPanel();
				this.addRemoveTableLayoutPanel = new TableLayoutPanel();
				this.okCancelTableLayoutPanel.SuspendLayout();
				this.overArchingTableLayoutPanel.SuspendLayout();
				this.addRemoveTableLayoutPanel.SuspendLayout();
				base.SuspendLayout();
				componentResourceManager.ApplyResources(this.membersLabel, "membersLabel");
				this.membersLabel.Margin = new Padding(0, 0, 3, 3);
				this.membersLabel.Name = "membersLabel";
				componentResourceManager.ApplyResources(this.listbox, "listbox");
				this.listbox.SelectionMode = (this.CanSelectMultipleInstances() ? SelectionMode.MultiExtended : SelectionMode.One);
				this.listbox.DrawMode = DrawMode.OwnerDrawFixed;
				this.listbox.FormattingEnabled = true;
				this.listbox.Margin = new Padding(0, 3, 3, 3);
				this.listbox.Name = "listbox";
				this.overArchingTableLayoutPanel.SetRowSpan(this.listbox, 2);
				componentResourceManager.ApplyResources(this.upButton, "upButton");
				this.upButton.Name = "upButton";
				componentResourceManager.ApplyResources(this.downButton, "downButton");
				this.downButton.Name = "downButton";
				componentResourceManager.ApplyResources(this.propertiesLabel, "propertiesLabel");
				this.propertiesLabel.AutoEllipsis = true;
				this.propertiesLabel.Margin = new Padding(0, 0, 3, 3);
				this.propertiesLabel.Name = "propertiesLabel";
				componentResourceManager.ApplyResources(this.propertyBrowser, "propertyBrowser");
				this.propertyBrowser.CommandsVisibleIfAvailable = false;
				this.propertyBrowser.Margin = new Padding(3, 3, 0, 3);
				this.propertyBrowser.Name = "propertyBrowser";
				this.overArchingTableLayoutPanel.SetRowSpan(this.propertyBrowser, 3);
				componentResourceManager.ApplyResources(this.addButton, "addButton");
				this.addButton.Margin = new Padding(0, 3, 3, 3);
				this.addButton.Name = "addButton";
				componentResourceManager.ApplyResources(this.removeButton, "removeButton");
				this.removeButton.Margin = new Padding(3, 3, 0, 3);
				this.removeButton.Name = "removeButton";
				componentResourceManager.ApplyResources(this.okButton, "okButton");
				this.okButton.DialogResult = DialogResult.OK;
				this.okButton.Margin = new Padding(0, 3, 3, 0);
				this.okButton.Name = "okButton";
				componentResourceManager.ApplyResources(this.cancelButton, "cancelButton");
				this.cancelButton.DialogResult = DialogResult.Cancel;
				this.cancelButton.Margin = new Padding(3, 3, 0, 0);
				this.cancelButton.Name = "cancelButton";
				componentResourceManager.ApplyResources(this.okCancelTableLayoutPanel, "okCancelTableLayoutPanel");
				this.overArchingTableLayoutPanel.SetColumnSpan(this.okCancelTableLayoutPanel, 3);
				this.okCancelTableLayoutPanel.Controls.Add(this.okButton, 0, 0);
				this.okCancelTableLayoutPanel.Controls.Add(this.cancelButton, 1, 0);
				this.okCancelTableLayoutPanel.Margin = new Padding(3, 3, 0, 0);
				this.okCancelTableLayoutPanel.Name = "okCancelTableLayoutPanel";
				componentResourceManager.ApplyResources(this.overArchingTableLayoutPanel, "overArchingTableLayoutPanel");
				this.overArchingTableLayoutPanel.Controls.Add(this.downButton, 1, 2);
				this.overArchingTableLayoutPanel.Controls.Add(this.addRemoveTableLayoutPanel, 0, 3);
				this.overArchingTableLayoutPanel.Controls.Add(this.propertiesLabel, 2, 0);
				this.overArchingTableLayoutPanel.Controls.Add(this.membersLabel, 0, 0);
				this.overArchingTableLayoutPanel.Controls.Add(this.listbox, 0, 1);
				this.overArchingTableLayoutPanel.Controls.Add(this.propertyBrowser, 2, 1);
				this.overArchingTableLayoutPanel.Controls.Add(this.okCancelTableLayoutPanel, 0, 4);
				this.overArchingTableLayoutPanel.Controls.Add(this.upButton, 1, 1);
				this.overArchingTableLayoutPanel.Name = "overArchingTableLayoutPanel";
				componentResourceManager.ApplyResources(this.addRemoveTableLayoutPanel, "addRemoveTableLayoutPanel");
				this.addRemoveTableLayoutPanel.Controls.Add(this.addButton, 0, 0);
				this.addRemoveTableLayoutPanel.Controls.Add(this.removeButton, 2, 0);
				this.addRemoveTableLayoutPanel.Margin = new Padding(0, 3, 3, 3);
				this.addRemoveTableLayoutPanel.Name = "addRemoveTableLayoutPanel";
				base.AcceptButton = this.okButton;
				componentResourceManager.ApplyResources(this, "$this");
				base.AutoScaleMode = AutoScaleMode.Font;
				base.CancelButton = this.cancelButton;
				base.Controls.Add(this.overArchingTableLayoutPanel);
				base.HelpButton = true;
				base.MaximizeBox = false;
				base.MinimizeBox = false;
				base.Name = "CollectionEditor";
				base.ShowIcon = false;
				base.ShowInTaskbar = false;
				this.okCancelTableLayoutPanel.ResumeLayout(false);
				this.okCancelTableLayoutPanel.PerformLayout();
				this.overArchingTableLayoutPanel.ResumeLayout(false);
				this.overArchingTableLayoutPanel.PerformLayout();
				this.addRemoveTableLayoutPanel.ResumeLayout(false);
				this.addRemoveTableLayoutPanel.PerformLayout();
				base.ResumeLayout(false);
			}

			// Token: 0x06002A60 RID: 10848 RVA: 0x000FEEA8 File Offset: 0x000FD0A8
			private void UpdateItemWidths(CollectionEditor.CollectionEditorCollectionForm.ListItem item)
			{
				if (!this.listbox.IsHandleCreated)
				{
					return;
				}
				using (Graphics graphics = this.listbox.CreateGraphics())
				{
					int horizontalExtent = this.listbox.HorizontalExtent;
					if (item != null)
					{
						int num = this.CalcItemWidth(graphics, item);
						if (num > horizontalExtent)
						{
							this.listbox.HorizontalExtent = num;
						}
					}
					else
					{
						int num2 = 0;
						foreach (object obj in this.listbox.Items)
						{
							CollectionEditor.CollectionEditorCollectionForm.ListItem item2 = (CollectionEditor.CollectionEditorCollectionForm.ListItem)obj;
							int num3 = this.CalcItemWidth(graphics, item2);
							if (num3 > num2)
							{
								num2 = num3;
							}
						}
						this.listbox.HorizontalExtent = num2;
					}
				}
			}

			// Token: 0x06002A61 RID: 10849 RVA: 0x000FEF84 File Offset: 0x000FD184
			private void Listbox_drawItem(object sender, DrawItemEventArgs e)
			{
				if (e.Index != -1)
				{
					CollectionEditor.CollectionEditorCollectionForm.ListItem listItem = (CollectionEditor.CollectionEditorCollectionForm.ListItem)this.listbox.Items[e.Index];
					Graphics graphics = e.Graphics;
					int count = this.listbox.Items.Count;
					int num = (count > 1) ? (count - 1) : count;
					SizeF sizeF = graphics.MeasureString(num.ToString(CultureInfo.CurrentCulture), this.listbox.Font);
					int num2 = (int)(Math.Log((double)num) / CollectionEditor.CollectionEditorCollectionForm.LOG10) + 1;
					int num3 = 4 + num2 * (this.Font.Height / 2);
					num3 = Math.Max(num3, (int)Math.Ceiling((double)sizeF.Width));
					num3 += SystemInformation.BorderSize.Width * 4;
					Rectangle rectangle = new Rectangle(e.Bounds.X, e.Bounds.Y, num3, e.Bounds.Height);
					ControlPaint.DrawButton(graphics, rectangle, ButtonState.Normal);
					rectangle.Inflate(-SystemInformation.BorderSize.Width * 2, -SystemInformation.BorderSize.Height * 2);
					int num4 = num3;
					Color color = SystemColors.Window;
					Color color2 = SystemColors.WindowText;
					if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
					{
						color = SystemColors.Highlight;
						color2 = SystemColors.HighlightText;
					}
					Rectangle rectangle2 = new Rectangle(e.Bounds.X + num4, e.Bounds.Y, e.Bounds.Width - num4, e.Bounds.Height);
					graphics.FillRectangle(new SolidBrush(color), rectangle2);
					if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
					{
						ControlPaint.DrawFocusRectangle(graphics, rectangle2);
					}
					num4 += 2;
					if (listItem.Editor != null && listItem.Editor.GetPaintValueSupported())
					{
						Rectangle rectangle3 = new Rectangle(e.Bounds.X + num4, e.Bounds.Y + 1, 20, e.Bounds.Height - 3);
						graphics.DrawRectangle(SystemPens.ControlText, rectangle3.X, rectangle3.Y, rectangle3.Width - 1, rectangle3.Height - 1);
						rectangle3.Inflate(-1, -1);
						listItem.Editor.PaintValue(listItem.Value, graphics, rectangle3);
						num4 += 27;
					}
					using (StringFormat stringFormat = new StringFormat())
					{
						stringFormat.Alignment = StringAlignment.Center;
						graphics.DrawString(e.Index.ToString(CultureInfo.CurrentCulture), this.Font, SystemBrushes.ControlText, new Rectangle(e.Bounds.X, e.Bounds.Y, num3, e.Bounds.Height), stringFormat);
					}
					Brush brush = new SolidBrush(color2);
					string displayText = this.GetDisplayText(listItem);
					try
					{
						graphics.DrawString(displayText, this.Font, brush, new Rectangle(e.Bounds.X + num4, e.Bounds.Y, e.Bounds.Width - num4, e.Bounds.Height));
					}
					finally
					{
						if (brush != null)
						{
							brush.Dispose();
						}
					}
					int num5 = num4 + (int)graphics.MeasureString(displayText, this.Font).Width;
					if (num5 > e.Bounds.Width && this.listbox.HorizontalExtent < num5)
					{
						this.listbox.HorizontalExtent = num5;
					}
				}
			}

			// Token: 0x06002A62 RID: 10850 RVA: 0x000FF35C File Offset: 0x000FD55C
			private void Listbox_keyDown(object sender, KeyEventArgs kevent)
			{
				Keys keyData = kevent.KeyData;
				if (keyData != Keys.Insert)
				{
					if (keyData == Keys.Delete)
					{
						this.PerformRemove();
						return;
					}
				}
				else
				{
					this.PerformAdd();
				}
			}

			// Token: 0x06002A63 RID: 10851 RVA: 0x000FF387 File Offset: 0x000FD587
			private void Listbox_selectedIndexChanged(object sender, EventArgs e)
			{
				this.UpdateEnabled();
			}

			// Token: 0x06002A64 RID: 10852 RVA: 0x000FF38F File Offset: 0x000FD58F
			private void Listbox_handleCreated(object sender, EventArgs e)
			{
				this.UpdateItemWidths(null);
			}

			// Token: 0x06002A65 RID: 10853 RVA: 0x000FF398 File Offset: 0x000FD598
			private void OKButton_click(object sender, EventArgs e)
			{
				try
				{
					if (!this.dirty || !this.CollectionEditable)
					{
						this.dirty = false;
						base.DialogResult = DialogResult.Cancel;
					}
					else
					{
						if (this.dirty)
						{
							object[] array = new object[this.listbox.Items.Count];
							for (int i = 0; i < array.Length; i++)
							{
								array[i] = ((CollectionEditor.CollectionEditorCollectionForm.ListItem)this.listbox.Items[i]).Value;
							}
							base.Items = array;
						}
						if (this.removedItems != null && this.dirty)
						{
							object[] array2 = this.removedItems.ToArray();
							for (int j = 0; j < array2.Length; j++)
							{
								base.DestroyInstance(array2[j]);
							}
							this.removedItems.Clear();
						}
						if (this.createdItems != null)
						{
							this.createdItems.Clear();
						}
						if (this.originalItems != null)
						{
							this.originalItems.Clear();
						}
						this.listbox.Items.Clear();
						this.dirty = false;
					}
				}
				catch (Exception e2)
				{
					base.DialogResult = DialogResult.None;
					this.DisplayError(e2);
				}
			}

			// Token: 0x06002A66 RID: 10854 RVA: 0x000FF4BC File Offset: 0x000FD6BC
			private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
			{
				if (!this.dirty && this.originalItems != null)
				{
					foreach (object obj in this.originalItems)
					{
						if (obj == e.Component)
						{
							this.dirty = true;
							break;
						}
					}
				}
			}

			// Token: 0x06002A67 RID: 10855 RVA: 0x000FF52C File Offset: 0x000FD72C
			protected override void OnEditValueChanged()
			{
				if (!base.Visible)
				{
					return;
				}
				if (this.originalItems == null)
				{
					this.originalItems = new ArrayList();
				}
				this.originalItems.Clear();
				this.listbox.Items.Clear();
				this.propertyBrowser.Site = new CollectionEditor.PropertyGridSite(base.Context, this.propertyBrowser);
				if (base.EditValue != null)
				{
					this.SuspendEnabledUpdates();
					try
					{
						object[] items = base.Items;
						for (int i = 0; i < items.Length; i++)
						{
							this.listbox.Items.Add(new CollectionEditor.CollectionEditorCollectionForm.ListItem(this.editor, items[i]));
							this.originalItems.Add(items[i]);
						}
						if (this.listbox.Items.Count > 0)
						{
							this.listbox.SelectedIndex = 0;
						}
						goto IL_D3;
					}
					finally
					{
						this.ResumeEnabledUpdates(true);
					}
				}
				this.UpdateEnabled();
				IL_D3:
				this.AdjustListBoxItemHeight();
				this.UpdateItemWidths(null);
			}

			// Token: 0x06002A68 RID: 10856 RVA: 0x000FF62C File Offset: 0x000FD82C
			protected override void OnFontChanged(EventArgs e)
			{
				base.OnFontChanged(e);
				this.AdjustListBoxItemHeight();
			}

			// Token: 0x06002A69 RID: 10857 RVA: 0x000FF63B File Offset: 0x000FD83B
			private void PerformAdd()
			{
				this.CreateAndAddInstance(base.NewItemTypes[0]);
			}

			// Token: 0x06002A6A RID: 10858 RVA: 0x000FF64C File Offset: 0x000FD84C
			private void PerformRemove()
			{
				int selectedIndex = this.listbox.SelectedIndex;
				if (selectedIndex != -1)
				{
					this.SuspendEnabledUpdates();
					try
					{
						if (this.listbox.SelectedItems.Count > 1)
						{
							ArrayList arrayList = new ArrayList(this.listbox.SelectedItems);
							using (IEnumerator enumerator = arrayList.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj = enumerator.Current;
									CollectionEditor.CollectionEditorCollectionForm.ListItem item = (CollectionEditor.CollectionEditorCollectionForm.ListItem)obj;
									this.RemoveInternal(item);
								}
								goto IL_8D;
							}
						}
						this.RemoveInternal((CollectionEditor.CollectionEditorCollectionForm.ListItem)this.listbox.SelectedItem);
						IL_8D:
						if (selectedIndex < this.listbox.Items.Count)
						{
							this.listbox.SelectedIndex = selectedIndex;
						}
						else if (this.listbox.Items.Count > 0)
						{
							this.listbox.SelectedIndex = this.listbox.Items.Count - 1;
						}
					}
					finally
					{
						this.ResumeEnabledUpdates(true);
					}
				}
			}

			// Token: 0x06002A6B RID: 10859 RVA: 0x000FF760 File Offset: 0x000FD960
			private void PropertyGrid_propertyValueChanged(object sender, PropertyValueChangedEventArgs e)
			{
				this.dirty = true;
				this.SuspendEnabledUpdates();
				try
				{
					int selectedIndex = this.listbox.SelectedIndex;
					if (selectedIndex >= 0)
					{
						this.listbox.RefreshItem(this.listbox.SelectedIndex);
					}
				}
				finally
				{
					this.ResumeEnabledUpdates(false);
				}
				this.UpdateItemWidths(null);
				this.listbox.Invalidate();
				this.propertiesLabel.Text = SR.GetString("CollectionEditorProperties", new object[]
				{
					this.GetDisplayText((CollectionEditor.CollectionEditorCollectionForm.ListItem)this.listbox.SelectedItem)
				});
			}

			// Token: 0x06002A6C RID: 10860 RVA: 0x000FF800 File Offset: 0x000FDA00
			private void RemoveInternal(CollectionEditor.CollectionEditorCollectionForm.ListItem item)
			{
				if (item != null)
				{
					this.editor.OnItemRemoving(item.Value);
					this.dirty = true;
					if (this.createdItems != null && this.createdItems.Contains(item.Value))
					{
						base.DestroyInstance(item.Value);
						this.createdItems.Remove(item.Value);
						this.listbox.Items.Remove(item);
					}
					else
					{
						try
						{
							if (!base.CanRemoveInstance(item.Value))
							{
								throw new Exception(SR.GetString("CollectionEditorCantRemoveItem", new object[]
								{
									this.GetDisplayText(item)
								}));
							}
							if (this.removedItems == null)
							{
								this.removedItems = new ArrayList();
							}
							this.removedItems.Add(item.Value);
							this.listbox.Items.Remove(item);
						}
						catch (Exception e)
						{
							this.DisplayError(e);
						}
					}
					this.UpdateItemWidths(null);
				}
			}

			// Token: 0x06002A6D RID: 10861 RVA: 0x000FF900 File Offset: 0x000FDB00
			private void RemoveButton_click(object sender, EventArgs e)
			{
				this.PerformRemove();
				Control control = (Control)sender;
				if (control.Enabled)
				{
					control.Focus();
				}
			}

			// Token: 0x06002A6E RID: 10862 RVA: 0x000FF929 File Offset: 0x000FDB29
			private void ResumeEnabledUpdates(bool updateNow)
			{
				this.suspendEnabledCount--;
				if (updateNow)
				{
					this.UpdateEnabled();
					return;
				}
				base.BeginInvoke(new MethodInvoker(this.UpdateEnabled));
			}

			// Token: 0x06002A6F RID: 10863 RVA: 0x000FF956 File Offset: 0x000FDB56
			private void SuspendEnabledUpdates()
			{
				this.suspendEnabledCount++;
			}

			// Token: 0x06002A70 RID: 10864 RVA: 0x000FF968 File Offset: 0x000FDB68
			protected internal override DialogResult ShowEditorDialog(IWindowsFormsEditorService edSvc)
			{
				IComponentChangeService componentChangeService = null;
				DialogResult result = DialogResult.OK;
				try
				{
					componentChangeService = (IComponentChangeService)this.editor.Context.GetService(typeof(IComponentChangeService));
					if (componentChangeService != null)
					{
						componentChangeService.ComponentChanged += this.OnComponentChanged;
					}
					base.ActiveControl = this.listbox;
					result = base.ShowEditorDialog(edSvc);
				}
				finally
				{
					if (componentChangeService != null)
					{
						componentChangeService.ComponentChanged -= this.OnComponentChanged;
					}
				}
				return result;
			}

			// Token: 0x06002A71 RID: 10865 RVA: 0x000FF9EC File Offset: 0x000FDBEC
			private void UpButton_click(object sender, EventArgs e)
			{
				int selectedIndex = this.listbox.SelectedIndex;
				if (selectedIndex == 0)
				{
					return;
				}
				this.dirty = true;
				try
				{
					this.SuspendEnabledUpdates();
					int topIndex = this.listbox.TopIndex;
					object value = this.listbox.Items[selectedIndex];
					this.listbox.Items[selectedIndex] = this.listbox.Items[selectedIndex - 1];
					this.listbox.Items[selectedIndex - 1] = value;
					if (topIndex > 0)
					{
						this.listbox.TopIndex = topIndex - 1;
					}
					this.listbox.ClearSelected();
					this.listbox.SelectedIndex = selectedIndex - 1;
					Control control = (Control)sender;
					if (control.Enabled)
					{
						control.Focus();
					}
				}
				finally
				{
					this.ResumeEnabledUpdates(true);
				}
			}

			// Token: 0x06002A72 RID: 10866 RVA: 0x000FFAC8 File Offset: 0x000FDCC8
			private void UpdateEnabled()
			{
				if (this.suspendEnabledCount > 0)
				{
					return;
				}
				bool flag = this.listbox.SelectedItem != null && this.CollectionEditable;
				this.removeButton.Enabled = (flag && this.AllowRemoveInstance(((CollectionEditor.CollectionEditorCollectionForm.ListItem)this.listbox.SelectedItem).Value));
				this.upButton.Enabled = (flag && this.listbox.Items.Count > 1);
				this.downButton.Enabled = (flag && this.listbox.Items.Count > 1);
				this.propertyBrowser.Enabled = flag;
				this.addButton.Enabled = this.CollectionEditable;
				if (this.listbox.SelectedItem == null)
				{
					this.propertiesLabel.Text = SR.GetString("CollectionEditorPropertiesNone");
					this.propertyBrowser.SelectedObject = null;
					return;
				}
				object[] array;
				if (this.IsImmutable)
				{
					array = new object[]
					{
						new CollectionEditor.CollectionEditorCollectionForm.SelectionWrapper(base.CollectionType, base.CollectionItemType, this.listbox, this.listbox.SelectedItems)
					};
				}
				else
				{
					array = new object[this.listbox.SelectedItems.Count];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = ((CollectionEditor.CollectionEditorCollectionForm.ListItem)this.listbox.SelectedItems[i]).Value;
					}
				}
				int count = this.listbox.SelectedItems.Count;
				if (count == 1 || count == -1)
				{
					this.propertiesLabel.Text = SR.GetString("CollectionEditorProperties", new object[]
					{
						this.GetDisplayText((CollectionEditor.CollectionEditorCollectionForm.ListItem)this.listbox.SelectedItem)
					});
				}
				else
				{
					this.propertiesLabel.Text = SR.GetString("CollectionEditorPropertiesMultiSelect");
				}
				if (this.editor.IsAnyObjectInheritedReadOnly(array))
				{
					this.propertyBrowser.SelectedObjects = null;
					this.propertyBrowser.Enabled = false;
					this.removeButton.Enabled = false;
					this.upButton.Enabled = false;
					this.downButton.Enabled = false;
					this.propertiesLabel.Text = SR.GetString("CollectionEditorInheritedReadOnlySelection");
					return;
				}
				this.propertyBrowser.Enabled = true;
				this.propertyBrowser.SelectedObjects = array;
			}

			// Token: 0x06002A73 RID: 10867 RVA: 0x000FFD0D File Offset: 0x000FDF0D
			private void Form_Shown(object sender, EventArgs e)
			{
				this.OnEditValueChanged();
			}

			// Token: 0x04001DB0 RID: 7600
			private const int TEXT_INDENT = 1;

			// Token: 0x04001DB1 RID: 7601
			private const int PAINT_WIDTH = 20;

			// Token: 0x04001DB2 RID: 7602
			private const int PAINT_INDENT = 26;

			// Token: 0x04001DB3 RID: 7603
			private static readonly double LOG10 = Math.Log(10.0);

			// Token: 0x04001DB4 RID: 7604
			private ArrayList createdItems;

			// Token: 0x04001DB5 RID: 7605
			private ArrayList removedItems;

			// Token: 0x04001DB6 RID: 7606
			private ArrayList originalItems;

			// Token: 0x04001DB7 RID: 7607
			private CollectionEditor editor;

			// Token: 0x04001DB8 RID: 7608
			private CollectionEditor.FilterListBox listbox;

			// Token: 0x04001DB9 RID: 7609
			private CollectionEditor.SplitButton addButton;

			// Token: 0x04001DBA RID: 7610
			private Button removeButton;

			// Token: 0x04001DBB RID: 7611
			private Button cancelButton;

			// Token: 0x04001DBC RID: 7612
			private Button okButton;

			// Token: 0x04001DBD RID: 7613
			private Button downButton;

			// Token: 0x04001DBE RID: 7614
			private Button upButton;

			// Token: 0x04001DBF RID: 7615
			private VsPropertyGrid propertyBrowser;

			// Token: 0x04001DC0 RID: 7616
			private Label membersLabel;

			// Token: 0x04001DC1 RID: 7617
			private Label propertiesLabel;

			// Token: 0x04001DC2 RID: 7618
			private ContextMenuStrip addDownMenu;

			// Token: 0x04001DC3 RID: 7619
			private TableLayoutPanel okCancelTableLayoutPanel;

			// Token: 0x04001DC4 RID: 7620
			private TableLayoutPanel overArchingTableLayoutPanel;

			// Token: 0x04001DC5 RID: 7621
			private TableLayoutPanel addRemoveTableLayoutPanel;

			// Token: 0x04001DC6 RID: 7622
			private int suspendEnabledCount;

			// Token: 0x04001DC7 RID: 7623
			private bool dirty;

			// Token: 0x020005CB RID: 1483
			private class SelectionWrapper : PropertyDescriptor, ICustomTypeDescriptor
			{
				// Token: 0x06003414 RID: 13332 RVA: 0x0011C470 File Offset: 0x0011A670
				public SelectionWrapper(Type collectionType, Type collectionItemType, Control control, ICollection collection) : base("Value", new Attribute[]
				{
					new CategoryAttribute(collectionItemType.Name)
				})
				{
					this.collectionType = collectionType;
					this.collectionItemType = collectionItemType;
					this.control = control;
					this.collection = collection;
					this.properties = new PropertyDescriptorCollection(new PropertyDescriptor[]
					{
						this
					});
					this.value = this;
					foreach (object obj in collection)
					{
						CollectionEditor.CollectionEditorCollectionForm.ListItem listItem = (CollectionEditor.CollectionEditorCollectionForm.ListItem)obj;
						if (this.value == this)
						{
							this.value = listItem.Value;
						}
						else
						{
							object obj2 = listItem.Value;
							if (this.value != null)
							{
								if (obj2 == null)
								{
									this.value = null;
									break;
								}
								if (!this.value.Equals(obj2))
								{
									this.value = null;
									break;
								}
							}
							else if (obj2 != null)
							{
								this.value = null;
								break;
							}
						}
					}
				}

				// Token: 0x17000A1A RID: 2586
				// (get) Token: 0x06003415 RID: 13333 RVA: 0x0011C56C File Offset: 0x0011A76C
				public override Type ComponentType
				{
					get
					{
						return this.collectionType;
					}
				}

				// Token: 0x17000A1B RID: 2587
				// (get) Token: 0x06003416 RID: 13334 RVA: 0x0000445B File Offset: 0x0000265B
				public override bool IsReadOnly
				{
					get
					{
						return false;
					}
				}

				// Token: 0x17000A1C RID: 2588
				// (get) Token: 0x06003417 RID: 13335 RVA: 0x0011C574 File Offset: 0x0011A774
				public override Type PropertyType
				{
					get
					{
						return this.collectionItemType;
					}
				}

				// Token: 0x06003418 RID: 13336 RVA: 0x0000445B File Offset: 0x0000265B
				public override bool CanResetValue(object component)
				{
					return false;
				}

				// Token: 0x06003419 RID: 13337 RVA: 0x0011C57C File Offset: 0x0011A77C
				public override object GetValue(object component)
				{
					return this.value;
				}

				// Token: 0x0600341A RID: 13338 RVA: 0x00003937 File Offset: 0x00001B37
				public override void ResetValue(object component)
				{
				}

				// Token: 0x0600341B RID: 13339 RVA: 0x0011C584 File Offset: 0x0011A784
				public override void SetValue(object component, object value)
				{
					this.value = value;
					foreach (object obj in this.collection)
					{
						CollectionEditor.CollectionEditorCollectionForm.ListItem listItem = (CollectionEditor.CollectionEditorCollectionForm.ListItem)obj;
						listItem.Value = value;
					}
					this.control.Invalidate();
					this.OnValueChanged(component, EventArgs.Empty);
				}

				// Token: 0x0600341C RID: 13340 RVA: 0x0000445B File Offset: 0x0000265B
				public override bool ShouldSerializeValue(object component)
				{
					return false;
				}

				// Token: 0x0600341D RID: 13341 RVA: 0x0011C5FC File Offset: 0x0011A7FC
				AttributeCollection ICustomTypeDescriptor.GetAttributes()
				{
					return TypeDescriptor.GetAttributes(this.collectionItemType);
				}

				// Token: 0x0600341E RID: 13342 RVA: 0x0011C609 File Offset: 0x0011A809
				string ICustomTypeDescriptor.GetClassName()
				{
					return this.collectionItemType.Name;
				}

				// Token: 0x0600341F RID: 13343 RVA: 0x00003598 File Offset: 0x00001798
				string ICustomTypeDescriptor.GetComponentName()
				{
					return null;
				}

				// Token: 0x06003420 RID: 13344 RVA: 0x00003598 File Offset: 0x00001798
				TypeConverter ICustomTypeDescriptor.GetConverter()
				{
					return null;
				}

				// Token: 0x06003421 RID: 13345 RVA: 0x00003598 File Offset: 0x00001798
				EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
				{
					return null;
				}

				// Token: 0x06003422 RID: 13346 RVA: 0x0000CA50 File Offset: 0x0000AC50
				PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
				{
					return this;
				}

				// Token: 0x06003423 RID: 13347 RVA: 0x00003598 File Offset: 0x00001798
				object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
				{
					return null;
				}

				// Token: 0x06003424 RID: 13348 RVA: 0x0011C616 File Offset: 0x0011A816
				EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
				{
					return EventDescriptorCollection.Empty;
				}

				// Token: 0x06003425 RID: 13349 RVA: 0x0011C616 File Offset: 0x0011A816
				EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
				{
					return EventDescriptorCollection.Empty;
				}

				// Token: 0x06003426 RID: 13350 RVA: 0x0011C61D File Offset: 0x0011A81D
				PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
				{
					return this.properties;
				}

				// Token: 0x06003427 RID: 13351 RVA: 0x0011C61D File Offset: 0x0011A81D
				PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
				{
					return this.properties;
				}

				// Token: 0x06003428 RID: 13352 RVA: 0x0000CA50 File Offset: 0x0000AC50
				object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
				{
					return this;
				}

				// Token: 0x040022DA RID: 8922
				private Type collectionType;

				// Token: 0x040022DB RID: 8923
				private Type collectionItemType;

				// Token: 0x040022DC RID: 8924
				private Control control;

				// Token: 0x040022DD RID: 8925
				private ICollection collection;

				// Token: 0x040022DE RID: 8926
				private PropertyDescriptorCollection properties;

				// Token: 0x040022DF RID: 8927
				private object value;
			}

			// Token: 0x020005CC RID: 1484
			private class ListItem
			{
				// Token: 0x06003429 RID: 13353 RVA: 0x0011C625 File Offset: 0x0011A825
				public ListItem(CollectionEditor parentCollectionEditor, object value)
				{
					this.value = value;
					this.parentCollectionEditor = parentCollectionEditor;
				}

				// Token: 0x0600342A RID: 13354 RVA: 0x0011C63B File Offset: 0x0011A83B
				public override string ToString()
				{
					return this.parentCollectionEditor.GetDisplayText(this.value);
				}

				// Token: 0x17000A1D RID: 2589
				// (get) Token: 0x0600342B RID: 13355 RVA: 0x0011C650 File Offset: 0x0011A850
				public UITypeEditor Editor
				{
					get
					{
						if (this.uiTypeEditor == null)
						{
							this.uiTypeEditor = TypeDescriptor.GetEditor(this.value, typeof(UITypeEditor));
							if (this.uiTypeEditor == null)
							{
								this.uiTypeEditor = this;
							}
						}
						if (this.uiTypeEditor != this)
						{
							return (UITypeEditor)this.uiTypeEditor;
						}
						return null;
					}
				}

				// Token: 0x17000A1E RID: 2590
				// (get) Token: 0x0600342C RID: 13356 RVA: 0x0011C6A5 File Offset: 0x0011A8A5
				// (set) Token: 0x0600342D RID: 13357 RVA: 0x0011C6AD File Offset: 0x0011A8AD
				public object Value
				{
					get
					{
						return this.value;
					}
					set
					{
						this.uiTypeEditor = null;
						this.value = value;
					}
				}

				// Token: 0x040022E0 RID: 8928
				private object value;

				// Token: 0x040022E1 RID: 8929
				private object uiTypeEditor;

				// Token: 0x040022E2 RID: 8930
				private CollectionEditor parentCollectionEditor;
			}

			// Token: 0x020005CD RID: 1485
			private class TypeMenuItem : ToolStripMenuItem
			{
				// Token: 0x0600342E RID: 13358 RVA: 0x0011C6BD File Offset: 0x0011A8BD
				public TypeMenuItem(Type itemType, EventHandler handler) : base(itemType.Name, null, handler)
				{
					this.itemType = itemType;
				}

				// Token: 0x17000A1F RID: 2591
				// (get) Token: 0x0600342F RID: 13359 RVA: 0x0011C6D4 File Offset: 0x0011A8D4
				public Type ItemType
				{
					get
					{
						return this.itemType;
					}
				}

				// Token: 0x040022E3 RID: 8931
				private Type itemType;
			}
		}

		// Token: 0x0200047E RID: 1150
		internal class FilterListBox : ListBox
		{
			// Token: 0x170008F7 RID: 2295
			// (get) Token: 0x06002A75 RID: 10869 RVA: 0x000FFD2C File Offset: 0x000FDF2C
			private PropertyGrid PropertyGrid
			{
				get
				{
					if (this.grid == null)
					{
						foreach (object obj in base.Parent.Controls)
						{
							Control control = (Control)obj;
							if (control is PropertyGrid)
							{
								this.grid = (PropertyGrid)control;
								break;
							}
						}
					}
					return this.grid;
				}
			}

			// Token: 0x06002A76 RID: 10870 RVA: 0x000FFDA8 File Offset: 0x000FDFA8
			public new void RefreshItem(int index)
			{
				base.RefreshItem(index);
			}

			// Token: 0x06002A77 RID: 10871 RVA: 0x000FFDB4 File Offset: 0x000FDFB4
			protected override void WndProc(ref Message m)
			{
				int msg = m.Msg;
				if (msg != 256)
				{
					if (msg == 258)
					{
						if ((Control.ModifierKeys & (Keys.Control | Keys.Alt)) == Keys.None && this.PropertyGrid != null)
						{
							this.PropertyGrid.Focus();
							UnsafeNativeMethods.SetFocus(new HandleRef(this.PropertyGrid, this.PropertyGrid.Handle));
							Application.DoEvents();
							if (this.PropertyGrid.Focused || this.PropertyGrid.ContainsFocus)
							{
								IntPtr focus = UnsafeNativeMethods.GetFocus();
								NativeMethods.SendMessage(focus, 256, this.lastKeyDown.WParam, this.lastKeyDown.LParam);
								NativeMethods.SendMessage(focus, 258, m.WParam, m.LParam);
								return;
							}
						}
					}
				}
				else
				{
					this.lastKeyDown = m;
					if ((int)((long)m.WParam) == 229 && this.PropertyGrid != null)
					{
						this.PropertyGrid.Focus();
						UnsafeNativeMethods.SetFocus(new HandleRef(this.PropertyGrid, this.PropertyGrid.Handle));
						Application.DoEvents();
						if (this.PropertyGrid.Focused || this.PropertyGrid.ContainsFocus)
						{
							NativeMethods.SendMessage(UnsafeNativeMethods.GetFocus(), 256, this.lastKeyDown.WParam, this.lastKeyDown.LParam);
						}
					}
				}
				base.WndProc(ref m);
			}

			// Token: 0x04001DC8 RID: 7624
			private PropertyGrid grid;

			// Token: 0x04001DC9 RID: 7625
			private Message lastKeyDown;
		}

		// Token: 0x0200047F RID: 1151
		protected abstract class CollectionForm : Form
		{
			// Token: 0x06002A79 RID: 10873 RVA: 0x000FFF34 File Offset: 0x000FE134
			public CollectionForm(CollectionEditor editor)
			{
				this.editor = editor;
			}

			// Token: 0x170008F8 RID: 2296
			// (get) Token: 0x06002A7A RID: 10874 RVA: 0x000FFF43 File Offset: 0x000FE143
			protected Type CollectionItemType
			{
				get
				{
					return this.editor.CollectionItemType;
				}
			}

			// Token: 0x170008F9 RID: 2297
			// (get) Token: 0x06002A7B RID: 10875 RVA: 0x000FFF50 File Offset: 0x000FE150
			protected Type CollectionType
			{
				get
				{
					return this.editor.CollectionType;
				}
			}

			// Token: 0x170008FA RID: 2298
			// (get) Token: 0x06002A7C RID: 10876 RVA: 0x000FFF60 File Offset: 0x000FE160
			// (set) Token: 0x06002A7D RID: 10877 RVA: 0x000FFFB7 File Offset: 0x000FE1B7
			internal virtual bool CollectionEditable
			{
				get
				{
					if (this.editableState != 0)
					{
						return this.editableState == 1;
					}
					bool flag = typeof(IList).IsAssignableFrom(this.editor.CollectionType);
					if (flag)
					{
						IList list = this.EditValue as IList;
						if (list != null)
						{
							return !list.IsReadOnly;
						}
					}
					return flag;
				}
				set
				{
					if (value)
					{
						this.editableState = 1;
						return;
					}
					this.editableState = 2;
				}
			}

			// Token: 0x170008FB RID: 2299
			// (get) Token: 0x06002A7E RID: 10878 RVA: 0x000FFFCB File Offset: 0x000FE1CB
			protected ITypeDescriptorContext Context
			{
				get
				{
					return this.editor.Context;
				}
			}

			// Token: 0x170008FC RID: 2300
			// (get) Token: 0x06002A7F RID: 10879 RVA: 0x000FFFD8 File Offset: 0x000FE1D8
			// (set) Token: 0x06002A80 RID: 10880 RVA: 0x000FFFE0 File Offset: 0x000FE1E0
			public object EditValue
			{
				get
				{
					return this.value;
				}
				set
				{
					this.value = value;
					this.OnEditValueChanged();
				}
			}

			// Token: 0x170008FD RID: 2301
			// (get) Token: 0x06002A81 RID: 10881 RVA: 0x000FFFEF File Offset: 0x000FE1EF
			// (set) Token: 0x06002A82 RID: 10882 RVA: 0x00100004 File Offset: 0x000FE204
			protected object[] Items
			{
				get
				{
					return this.editor.GetItems(this.EditValue);
				}
				set
				{
					bool flag = false;
					try
					{
						flag = this.Context.OnComponentChanging();
					}
					catch (Exception ex)
					{
						if (ClientUtils.IsCriticalException(ex))
						{
							throw;
						}
						this.DisplayError(ex);
					}
					if (flag)
					{
						object obj = this.editor.SetItems(this.EditValue, value);
						if (obj != this.EditValue)
						{
							this.EditValue = obj;
						}
						this.Context.OnComponentChanged();
					}
				}
			}

			// Token: 0x170008FE RID: 2302
			// (get) Token: 0x06002A83 RID: 10883 RVA: 0x00100078 File Offset: 0x000FE278
			protected Type[] NewItemTypes
			{
				get
				{
					return this.editor.NewItemTypes;
				}
			}

			// Token: 0x06002A84 RID: 10884 RVA: 0x00100085 File Offset: 0x000FE285
			protected bool CanRemoveInstance(object value)
			{
				return this.editor.CanRemoveInstance(value);
			}

			// Token: 0x06002A85 RID: 10885 RVA: 0x00100093 File Offset: 0x000FE293
			protected virtual bool CanSelectMultipleInstances()
			{
				return this.editor.CanSelectMultipleInstances();
			}

			// Token: 0x06002A86 RID: 10886 RVA: 0x001000A0 File Offset: 0x000FE2A0
			protected object CreateInstance(Type itemType)
			{
				return this.editor.CreateInstance(itemType);
			}

			// Token: 0x06002A87 RID: 10887 RVA: 0x001000AE File Offset: 0x000FE2AE
			protected void DestroyInstance(object instance)
			{
				this.editor.DestroyInstance(instance);
			}

			// Token: 0x06002A88 RID: 10888 RVA: 0x001000BC File Offset: 0x000FE2BC
			protected virtual void DisplayError(Exception e)
			{
				IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
				if (iuiservice != null)
				{
					iuiservice.ShowError(e);
					return;
				}
				string text = e.Message;
				if (text == null || text.Length == 0)
				{
					text = e.ToString();
				}
				RTLAwareMessageBox.Show(null, text, null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
			}

			// Token: 0x06002A89 RID: 10889 RVA: 0x00100111 File Offset: 0x000FE311
			protected override object GetService(Type serviceType)
			{
				return this.editor.GetService(serviceType);
			}

			// Token: 0x06002A8A RID: 10890 RVA: 0x0010011F File Offset: 0x000FE31F
			protected internal virtual DialogResult ShowEditorDialog(IWindowsFormsEditorService edSvc)
			{
				return edSvc.ShowDialog(this);
			}

			// Token: 0x06002A8B RID: 10891
			protected abstract void OnEditValueChanged();

			// Token: 0x04001DCA RID: 7626
			private CollectionEditor editor;

			// Token: 0x04001DCB RID: 7627
			private object value;

			// Token: 0x04001DCC RID: 7628
			private short editableState;

			// Token: 0x04001DCD RID: 7629
			private const short EditableDynamic = 0;

			// Token: 0x04001DCE RID: 7630
			private const short EditableYes = 1;

			// Token: 0x04001DCF RID: 7631
			private const short EditableNo = 2;
		}

		// Token: 0x02000480 RID: 1152
		internal class PropertyGridSite : ISite, IServiceProvider
		{
			// Token: 0x06002A8C RID: 10892 RVA: 0x00100128 File Offset: 0x000FE328
			public PropertyGridSite(IServiceProvider sp, IComponent comp)
			{
				this.sp = sp;
				this.comp = comp;
			}

			// Token: 0x170008FF RID: 2303
			// (get) Token: 0x06002A8D RID: 10893 RVA: 0x0010013E File Offset: 0x000FE33E
			public IComponent Component
			{
				get
				{
					return this.comp;
				}
			}

			// Token: 0x17000900 RID: 2304
			// (get) Token: 0x06002A8E RID: 10894 RVA: 0x00003598 File Offset: 0x00001798
			public IContainer Container
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000901 RID: 2305
			// (get) Token: 0x06002A8F RID: 10895 RVA: 0x0000445B File Offset: 0x0000265B
			public bool DesignMode
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000902 RID: 2306
			// (get) Token: 0x06002A90 RID: 10896 RVA: 0x00003598 File Offset: 0x00001798
			// (set) Token: 0x06002A91 RID: 10897 RVA: 0x00003937 File Offset: 0x00001B37
			public string Name
			{
				get
				{
					return null;
				}
				set
				{
				}
			}

			// Token: 0x06002A92 RID: 10898 RVA: 0x00100148 File Offset: 0x000FE348
			public object GetService(Type t)
			{
				if (!this.inGetService && this.sp != null)
				{
					try
					{
						this.inGetService = true;
						return this.sp.GetService(t);
					}
					finally
					{
						this.inGetService = false;
					}
				}
				return null;
			}

			// Token: 0x04001DD0 RID: 7632
			private IServiceProvider sp;

			// Token: 0x04001DD1 RID: 7633
			private IComponent comp;

			// Token: 0x04001DD2 RID: 7634
			private bool inGetService;
		}
	}
}
