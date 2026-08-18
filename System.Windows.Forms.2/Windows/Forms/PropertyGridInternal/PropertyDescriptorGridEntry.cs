using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x02000513 RID: 1299
	internal class PropertyDescriptorGridEntry : GridEntry
	{
		// Token: 0x060054FF RID: 21759 RVA: 0x001645A0 File Offset: 0x001627A0
		internal PropertyDescriptorGridEntry(PropertyGrid ownerGrid, GridEntry peParent, bool hide) : base(ownerGrid, peParent)
		{
			this.activeXHide = hide;
		}

		// Token: 0x06005500 RID: 21760 RVA: 0x001645BC File Offset: 0x001627BC
		internal PropertyDescriptorGridEntry(PropertyGrid ownerGrid, GridEntry peParent, PropertyDescriptor propInfo, bool hide) : base(ownerGrid, peParent)
		{
			this.activeXHide = hide;
			this.Initialize(propInfo);
		}

		// Token: 0x17001462 RID: 5218
		// (get) Token: 0x06005501 RID: 21761 RVA: 0x001645E0 File Offset: 0x001627E0
		public override bool AllowMerge
		{
			get
			{
				MergablePropertyAttribute mergablePropertyAttribute = (MergablePropertyAttribute)this.propertyInfo.Attributes[typeof(MergablePropertyAttribute)];
				return mergablePropertyAttribute == null || mergablePropertyAttribute.IsDefaultAttribute();
			}
		}

		// Token: 0x17001463 RID: 5219
		// (get) Token: 0x06005502 RID: 21762 RVA: 0x00164618 File Offset: 0x00162818
		internal override AttributeCollection Attributes
		{
			get
			{
				return this.propertyInfo.Attributes;
			}
		}

		// Token: 0x17001464 RID: 5220
		// (get) Token: 0x06005503 RID: 21763 RVA: 0x00164628 File Offset: 0x00162828
		public override string HelpKeyword
		{
			get
			{
				if (this.helpKeyword == null)
				{
					object valueOwner = this.GetValueOwner();
					if (valueOwner == null)
					{
						return null;
					}
					HelpKeywordAttribute helpKeywordAttribute = (HelpKeywordAttribute)this.propertyInfo.Attributes[typeof(HelpKeywordAttribute)];
					if (helpKeywordAttribute != null && !helpKeywordAttribute.IsDefaultAttribute())
					{
						return helpKeywordAttribute.HelpKeyword;
					}
					if (this is ImmutablePropertyDescriptorGridEntry)
					{
						this.helpKeyword = this.PropertyName;
						GridEntry gridEntry = this;
						while (gridEntry.ParentGridEntry != null)
						{
							gridEntry = gridEntry.ParentGridEntry;
							if (gridEntry.PropertyValue == valueOwner || (valueOwner.GetType().IsValueType && valueOwner.GetType() == gridEntry.PropertyValue.GetType()))
							{
								this.helpKeyword = gridEntry.PropertyName + "." + this.helpKeyword;
								break;
							}
						}
					}
					else
					{
						Type type = this.propertyInfo.ComponentType;
						string str;
						if (type.IsCOMObject)
						{
							str = TypeDescriptor.GetClassName(valueOwner);
						}
						else
						{
							Type type2 = valueOwner.GetType();
							if (!type.IsPublic || !type.IsAssignableFrom(type2))
							{
								PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(type2)[this.PropertyName];
								if (propertyDescriptor != null)
								{
									type = propertyDescriptor.ComponentType;
								}
								else
								{
									type = null;
								}
							}
							if (type == null)
							{
								str = TypeDescriptor.GetClassName(valueOwner);
							}
							else
							{
								str = type.FullName;
							}
						}
						this.helpKeyword = str + "." + this.propertyInfo.Name;
					}
				}
				return this.helpKeyword;
			}
		}

		// Token: 0x17001465 RID: 5221
		// (get) Token: 0x06005504 RID: 21764 RVA: 0x0016479D File Offset: 0x0016299D
		internal override string LabelToolTipText
		{
			get
			{
				if (this.toolTipText == null)
				{
					return base.LabelToolTipText;
				}
				return this.toolTipText;
			}
		}

		// Token: 0x17001466 RID: 5222
		// (get) Token: 0x06005505 RID: 21765 RVA: 0x0015FB60 File Offset: 0x0015DD60
		internal override string HelpKeywordInternal
		{
			get
			{
				return this.PropertyLabel;
			}
		}

		// Token: 0x17001467 RID: 5223
		// (get) Token: 0x06005506 RID: 21766 RVA: 0x001647B4 File Offset: 0x001629B4
		internal override bool Enumerable
		{
			get
			{
				return base.Enumerable && !this.IsPropertyReadOnly;
			}
		}

		// Token: 0x17001468 RID: 5224
		// (get) Token: 0x06005507 RID: 21767 RVA: 0x001647C9 File Offset: 0x001629C9
		internal virtual bool IsPropertyReadOnly
		{
			get
			{
				return this.propertyInfo.IsReadOnly;
			}
		}

		// Token: 0x17001469 RID: 5225
		// (get) Token: 0x06005508 RID: 21768 RVA: 0x001647D6 File Offset: 0x001629D6
		public override bool IsValueEditable
		{
			get
			{
				return this.exceptionConverter == null && !this.IsPropertyReadOnly && base.IsValueEditable;
			}
		}

		// Token: 0x1700146A RID: 5226
		// (get) Token: 0x06005509 RID: 21769 RVA: 0x001647F0 File Offset: 0x001629F0
		public override bool NeedsDropDownButton
		{
			get
			{
				return base.NeedsDropDownButton && !this.IsPropertyReadOnly;
			}
		}

		// Token: 0x1700146B RID: 5227
		// (get) Token: 0x0600550A RID: 21770 RVA: 0x00164808 File Offset: 0x00162A08
		internal bool ParensAroundName
		{
			get
			{
				if (255 == this.parensAroundName)
				{
					if (((ParenthesizePropertyNameAttribute)this.propertyInfo.Attributes[typeof(ParenthesizePropertyNameAttribute)]).NeedParenthesis)
					{
						this.parensAroundName = 1;
					}
					else
					{
						this.parensAroundName = 0;
					}
				}
				return this.parensAroundName == 1;
			}
		}

		// Token: 0x1700146C RID: 5228
		// (get) Token: 0x0600550B RID: 21771 RVA: 0x00164864 File Offset: 0x00162A64
		public override string PropertyCategory
		{
			get
			{
				string text = this.propertyInfo.Category;
				if (text == null || text.Length == 0)
				{
					text = base.PropertyCategory;
				}
				return text;
			}
		}

		// Token: 0x1700146D RID: 5229
		// (get) Token: 0x0600550C RID: 21772 RVA: 0x00164890 File Offset: 0x00162A90
		public override PropertyDescriptor PropertyDescriptor
		{
			get
			{
				return this.propertyInfo;
			}
		}

		// Token: 0x1700146E RID: 5230
		// (get) Token: 0x0600550D RID: 21773 RVA: 0x00164898 File Offset: 0x00162A98
		public override string PropertyDescription
		{
			get
			{
				return this.propertyInfo.Description;
			}
		}

		// Token: 0x1700146F RID: 5231
		// (get) Token: 0x0600550E RID: 21774 RVA: 0x001648A8 File Offset: 0x00162AA8
		public override string PropertyLabel
		{
			get
			{
				string text = this.propertyInfo.DisplayName;
				if (this.ParensAroundName)
				{
					text = "(" + text + ")";
				}
				return text;
			}
		}

		// Token: 0x17001470 RID: 5232
		// (get) Token: 0x0600550F RID: 21775 RVA: 0x001648DB File Offset: 0x00162ADB
		public override string PropertyName
		{
			get
			{
				if (this.propertyInfo != null)
				{
					return this.propertyInfo.Name;
				}
				return null;
			}
		}

		// Token: 0x17001471 RID: 5233
		// (get) Token: 0x06005510 RID: 21776 RVA: 0x001648F2 File Offset: 0x00162AF2
		public override Type PropertyType
		{
			get
			{
				return this.propertyInfo.PropertyType;
			}
		}

		// Token: 0x17001472 RID: 5234
		// (get) Token: 0x06005511 RID: 21777 RVA: 0x00164900 File Offset: 0x00162B00
		// (set) Token: 0x06005512 RID: 21778 RVA: 0x00164960 File Offset: 0x00162B60
		public override object PropertyValue
		{
			get
			{
				object result;
				try
				{
					object propertyValueCore = this.GetPropertyValueCore(this.GetValueOwner());
					if (this.exceptionConverter != null)
					{
						this.SetFlagsAndExceptionInfo(0, null, null);
					}
					result = propertyValueCore;
				}
				catch (Exception ex)
				{
					if (this.exceptionConverter == null)
					{
						this.SetFlagsAndExceptionInfo(0, new PropertyDescriptorGridEntry.ExceptionConverter(), new PropertyDescriptorGridEntry.ExceptionEditor());
					}
					result = ex;
				}
				return result;
			}
			set
			{
				this.SetPropertyValue(this.GetValueOwner(), value, false, null);
			}
		}

		// Token: 0x17001473 RID: 5235
		// (get) Token: 0x06005513 RID: 21779 RVA: 0x00164972 File Offset: 0x00162B72
		private IPropertyValueUIService PropertyValueUIService
		{
			get
			{
				if (!this.pvSvcChecked && this.pvSvc == null)
				{
					this.pvSvc = (IPropertyValueUIService)this.GetService(typeof(IPropertyValueUIService));
					this.pvSvcChecked = true;
				}
				return this.pvSvc;
			}
		}

		// Token: 0x06005514 RID: 21780 RVA: 0x001649AC File Offset: 0x00162BAC
		private void SetFlagsAndExceptionInfo(int flags, PropertyDescriptorGridEntry.ExceptionConverter converter, PropertyDescriptorGridEntry.ExceptionEditor editor)
		{
			this.Flags = flags;
			this.exceptionConverter = converter;
			this.exceptionEditor = editor;
		}

		// Token: 0x17001474 RID: 5236
		// (get) Token: 0x06005515 RID: 21781 RVA: 0x001649C4 File Offset: 0x00162BC4
		public override bool ShouldRenderReadOnly
		{
			get
			{
				if (base.ForceReadOnly || this.forceRenderReadOnly)
				{
					return true;
				}
				if (this.propertyInfo.IsReadOnly && !this.readOnlyVerified && this.GetFlagSet(128))
				{
					Type propertyType = this.PropertyType;
					if (propertyType != null && (propertyType.IsArray || propertyType.IsValueType || propertyType.IsPrimitive))
					{
						this.SetFlag(128, false);
						this.SetFlag(256, true);
						this.forceRenderReadOnly = true;
					}
				}
				this.readOnlyVerified = true;
				return base.ShouldRenderReadOnly && !this.isSerializeContentsProp && !base.NeedsCustomEditorButton;
			}
		}

		// Token: 0x17001475 RID: 5237
		// (get) Token: 0x06005516 RID: 21782 RVA: 0x00164A6E File Offset: 0x00162C6E
		internal override TypeConverter TypeConverter
		{
			get
			{
				if (this.exceptionConverter != null)
				{
					return this.exceptionConverter;
				}
				if (this.converter == null)
				{
					this.converter = this.propertyInfo.Converter;
				}
				return base.TypeConverter;
			}
		}

		// Token: 0x17001476 RID: 5238
		// (get) Token: 0x06005517 RID: 21783 RVA: 0x00164A9E File Offset: 0x00162C9E
		internal override UITypeEditor UITypeEditor
		{
			get
			{
				if (this.exceptionEditor != null)
				{
					return this.exceptionEditor;
				}
				this.editor = (UITypeEditor)this.propertyInfo.GetEditor(typeof(UITypeEditor));
				return base.UITypeEditor;
			}
		}

		// Token: 0x06005518 RID: 21784 RVA: 0x00164AD8 File Offset: 0x00162CD8
		internal override void EditPropertyValue(PropertyGridView iva)
		{
			base.EditPropertyValue(iva);
			if (!this.IsValueEditable)
			{
				RefreshPropertiesAttribute refreshPropertiesAttribute = (RefreshPropertiesAttribute)this.propertyInfo.Attributes[typeof(RefreshPropertiesAttribute)];
				if (refreshPropertiesAttribute != null && !refreshPropertiesAttribute.RefreshProperties.Equals(RefreshProperties.None))
				{
					this.GridEntryHost.Refresh(refreshPropertiesAttribute != null && refreshPropertiesAttribute.Equals(RefreshPropertiesAttribute.All));
				}
			}
		}

		// Token: 0x06005519 RID: 21785 RVA: 0x00164B50 File Offset: 0x00162D50
		internal override Point GetLabelToolTipLocation(int mouseX, int mouseY)
		{
			if (this.pvUIItems != null)
			{
				for (int i = 0; i < this.pvUIItems.Length; i++)
				{
					if (this.uiItemRects[i].Contains(mouseX, this.GridEntryHost.GetGridEntryHeight() / 2))
					{
						this.toolTipText = this.pvUIItems[i].ToolTip;
						return new Point(mouseX, mouseY);
					}
				}
			}
			this.toolTipText = null;
			return base.GetLabelToolTipLocation(mouseX, mouseY);
		}

		// Token: 0x0600551A RID: 21786 RVA: 0x00164BC4 File Offset: 0x00162DC4
		protected object GetPropertyValueCore(object target)
		{
			if (this.propertyInfo == null)
			{
				return null;
			}
			if (target is ICustomTypeDescriptor)
			{
				target = ((ICustomTypeDescriptor)target).GetPropertyOwner(this.propertyInfo);
			}
			object value;
			try
			{
				value = this.propertyInfo.GetValue(target);
			}
			catch
			{
				throw;
			}
			return value;
		}

		// Token: 0x0600551B RID: 21787 RVA: 0x00164C1C File Offset: 0x00162E1C
		protected void Initialize(PropertyDescriptor propInfo)
		{
			this.propertyInfo = propInfo;
			this.isSerializeContentsProp = (this.propertyInfo.SerializationVisibility == DesignerSerializationVisibility.Content);
			if (!this.activeXHide && this.IsPropertyReadOnly)
			{
				this.SetFlag(1, false);
			}
			if (this.isSerializeContentsProp && this.TypeConverter.GetPropertiesSupported())
			{
				this.SetFlag(131072, true);
			}
		}

		// Token: 0x0600551C RID: 21788 RVA: 0x00164C80 File Offset: 0x00162E80
		protected virtual void NotifyParentChange(GridEntry ge)
		{
			while (ge != null && ge is PropertyDescriptorGridEntry && ((PropertyDescriptorGridEntry)ge).propertyInfo.Attributes.Contains(NotifyParentPropertyAttribute.Yes))
			{
				object valueOwner = ge.GetValueOwner();
				bool isValueType = valueOwner.GetType().IsValueType;
				while ((!(ge is PropertyDescriptorGridEntry) || isValueType) ? valueOwner.Equals(ge.GetValueOwner()) : (valueOwner == ge.GetValueOwner()))
				{
					ge = ge.ParentGridEntry;
					if (ge == null)
					{
						break;
					}
				}
				if (ge != null)
				{
					valueOwner = ge.GetValueOwner();
					IComponentChangeService componentChangeService = this.ComponentChangeService;
					if (componentChangeService != null)
					{
						componentChangeService.OnComponentChanging(valueOwner, ((PropertyDescriptorGridEntry)ge).propertyInfo);
						componentChangeService.OnComponentChanged(valueOwner, ((PropertyDescriptorGridEntry)ge).propertyInfo, null, null);
					}
					ge.ClearCachedValues(false);
					PropertyGridView gridEntryHost = this.GridEntryHost;
					if (gridEntryHost != null)
					{
						gridEntryHost.InvalidateGridEntryValue(ge);
					}
				}
			}
		}

		// Token: 0x0600551D RID: 21789 RVA: 0x00164D58 File Offset: 0x00162F58
		internal override bool NotifyValueGivenParent(object obj, int type)
		{
			if (obj is ICustomTypeDescriptor)
			{
				obj = ((ICustomTypeDescriptor)obj).GetPropertyOwner(this.propertyInfo);
			}
			switch (type)
			{
			case 1:
				this.SetPropertyValue(obj, null, true, SR.GetString("PropertyGridResetValue", new object[]
				{
					this.PropertyName
				}));
				if (this.pvUIItems != null)
				{
					for (int i = 0; i < this.pvUIItems.Length; i++)
					{
						this.pvUIItems[i].Reset();
					}
				}
				this.pvUIItems = null;
				return false;
			case 2:
				try
				{
					return this.propertyInfo.CanResetValue(obj) || (this.pvUIItems != null && this.pvUIItems.Length != 0);
				}
				catch
				{
					if (this.exceptionConverter == null)
					{
						this.Flags = 0;
						this.exceptionConverter = new PropertyDescriptorGridEntry.ExceptionConverter();
						this.exceptionEditor = new PropertyDescriptorGridEntry.ExceptionEditor();
					}
					return false;
				}
				break;
			case 3:
			case 5:
				goto IL_124;
			case 4:
				break;
			default:
				return false;
			}
			try
			{
				return this.propertyInfo.ShouldSerializeValue(obj);
			}
			catch
			{
				if (this.exceptionConverter == null)
				{
					this.Flags = 0;
					this.exceptionConverter = new PropertyDescriptorGridEntry.ExceptionConverter();
					this.exceptionEditor = new PropertyDescriptorGridEntry.ExceptionEditor();
				}
				return false;
			}
			IL_124:
			if (this.eventBindings == null)
			{
				this.eventBindings = (IEventBindingService)this.GetService(typeof(IEventBindingService));
			}
			if (this.eventBindings != null)
			{
				EventDescriptor @event = this.eventBindings.GetEvent(this.propertyInfo);
				if (@event != null)
				{
					return this.ViewEvent(obj, null, null, true);
				}
			}
			return false;
		}

		// Token: 0x0600551E RID: 21790 RVA: 0x00164EF4 File Offset: 0x001630F4
		public override void OnComponentChanged()
		{
			base.OnComponentChanged();
			this.NotifyParentChange(this);
		}

		// Token: 0x0600551F RID: 21791 RVA: 0x00164F04 File Offset: 0x00163104
		public override bool OnMouseClick(int x, int y, int count, MouseButtons button)
		{
			if (this.pvUIItems != null && count == 2 && (button & MouseButtons.Left) == MouseButtons.Left)
			{
				for (int i = 0; i < this.pvUIItems.Length; i++)
				{
					if (this.uiItemRects[i].Contains(x, this.GridEntryHost.GetGridEntryHeight() / 2))
					{
						this.pvUIItems[i].InvokeHandler(this, this.propertyInfo, this.pvUIItems[i]);
						return true;
					}
				}
			}
			return base.OnMouseClick(x, y, count, button);
		}

		// Token: 0x06005520 RID: 21792 RVA: 0x00164F90 File Offset: 0x00163190
		public override void PaintLabel(Graphics g, Rectangle rect, Rectangle clipRect, bool selected, bool paintFullLabel)
		{
			base.PaintLabel(g, rect, clipRect, selected, paintFullLabel);
			IPropertyValueUIService propertyValueUIService = this.PropertyValueUIService;
			if (propertyValueUIService == null)
			{
				return;
			}
			this.pvUIItems = propertyValueUIService.GetPropertyUIValueItems(this, this.propertyInfo);
			if (this.pvUIItems != null)
			{
				if (this.uiItemRects == null || this.uiItemRects.Length != this.pvUIItems.Length)
				{
					this.uiItemRects = new Rectangle[this.pvUIItems.Length];
				}
				if (!PropertyDescriptorGridEntry.isScalingInitialized)
				{
					if (DpiHelper.IsScalingRequired)
					{
						PropertyDescriptorGridEntry.scaledImageSizeX = DpiHelper.LogicalToDeviceUnitsX(8);
						PropertyDescriptorGridEntry.scaledImageSizeY = DpiHelper.LogicalToDeviceUnitsY(8);
					}
					PropertyDescriptorGridEntry.isScalingInitialized = true;
				}
				for (int i = 0; i < this.pvUIItems.Length; i++)
				{
					this.uiItemRects[i] = new Rectangle(rect.Right - (PropertyDescriptorGridEntry.scaledImageSizeX + 1) * (i + 1), (rect.Height - PropertyDescriptorGridEntry.scaledImageSizeY) / 2, PropertyDescriptorGridEntry.scaledImageSizeX, PropertyDescriptorGridEntry.scaledImageSizeY);
					g.DrawImage(this.pvUIItems[i].Image, this.uiItemRects[i]);
				}
				this.GridEntryHost.LabelPaintMargin = (PropertyDescriptorGridEntry.scaledImageSizeX + 1) * this.pvUIItems.Length;
			}
		}

		// Token: 0x06005521 RID: 21793 RVA: 0x001650B4 File Offset: 0x001632B4
		private object SetPropertyValue(object obj, object objVal, bool reset, string undoText)
		{
			DesignerTransaction designerTransaction = null;
			try
			{
				object propertyValueCore = this.GetPropertyValueCore(obj);
				if (objVal != null && objVal.Equals(propertyValueCore))
				{
					return objVal;
				}
				base.ClearCachedValues();
				IDesignerHost designerHost = this.DesignerHost;
				if (designerHost != null)
				{
					string description = (undoText == null) ? SR.GetString("PropertyGridSetValue", new object[]
					{
						this.propertyInfo.Name
					}) : undoText;
					designerTransaction = designerHost.CreateTransaction(description);
				}
				bool flag = !(obj is IComponent) || ((IComponent)obj).Site == null;
				if (flag)
				{
					try
					{
						if (this.ComponentChangeService != null)
						{
							this.ComponentChangeService.OnComponentChanging(obj, this.propertyInfo);
						}
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return propertyValueCore;
						}
						throw ex;
					}
				}
				bool internalExpanded = this.InternalExpanded;
				int oldCount = -1;
				if (internalExpanded)
				{
					oldCount = base.ChildCount;
				}
				RefreshPropertiesAttribute refreshPropertiesAttribute = (RefreshPropertiesAttribute)this.propertyInfo.Attributes[typeof(RefreshPropertiesAttribute)];
				bool flag2 = internalExpanded || (refreshPropertiesAttribute != null && !refreshPropertiesAttribute.RefreshProperties.Equals(RefreshProperties.None));
				if (flag2)
				{
					this.DisposeChildren();
				}
				EventDescriptor eventDescriptor = null;
				if (obj != null && objVal is string)
				{
					if (this.eventBindings == null)
					{
						this.eventBindings = (IEventBindingService)this.GetService(typeof(IEventBindingService));
					}
					if (this.eventBindings != null)
					{
						eventDescriptor = this.eventBindings.GetEvent(this.propertyInfo);
					}
					if (eventDescriptor == null)
					{
						object component = obj;
						if (this.propertyInfo is MergePropertyDescriptor && obj is Array)
						{
							Array array = obj as Array;
							if (array.Length > 0)
							{
								component = array.GetValue(0);
							}
						}
						eventDescriptor = TypeDescriptor.GetEvents(component)[this.propertyInfo.Name];
					}
				}
				bool flag3 = false;
				try
				{
					if (reset)
					{
						this.propertyInfo.ResetValue(obj);
					}
					else if (eventDescriptor != null)
					{
						this.ViewEvent(obj, (string)objVal, eventDescriptor, false);
					}
					else
					{
						this.SetPropertyValueCore(obj, objVal, true);
					}
					flag3 = true;
					if (flag && this.ComponentChangeService != null)
					{
						this.ComponentChangeService.OnComponentChanged(obj, this.propertyInfo, null, objVal);
					}
					this.NotifyParentChange(this);
				}
				finally
				{
					if (flag2 && this.GridEntryHost != null)
					{
						base.RecreateChildren(oldCount);
						if (flag3)
						{
							this.GridEntryHost.Refresh(refreshPropertiesAttribute != null && refreshPropertiesAttribute.Equals(RefreshPropertiesAttribute.All));
						}
					}
				}
			}
			catch (CheckoutException ex2)
			{
				if (designerTransaction != null)
				{
					designerTransaction.Cancel();
					designerTransaction = null;
				}
				if (ex2 == CheckoutException.Canceled)
				{
					return null;
				}
				throw;
			}
			catch
			{
				if (designerTransaction != null)
				{
					designerTransaction.Cancel();
					designerTransaction = null;
				}
				throw;
			}
			finally
			{
				if (designerTransaction != null)
				{
					designerTransaction.Commit();
				}
			}
			return obj;
		}

		// Token: 0x06005522 RID: 21794 RVA: 0x001653D0 File Offset: 0x001635D0
		protected void SetPropertyValueCore(object obj, object value, bool doUndo)
		{
			if (this.propertyInfo == null)
			{
				return;
			}
			Cursor value2 = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				object obj2 = obj;
				if (obj2 is ICustomTypeDescriptor)
				{
					obj2 = ((ICustomTypeDescriptor)obj2).GetPropertyOwner(this.propertyInfo);
				}
				bool flag = false;
				if (this.ParentGridEntry != null)
				{
					Type propertyType = this.ParentGridEntry.PropertyType;
					flag = (propertyType.IsValueType || propertyType.IsArray);
				}
				if (obj2 != null)
				{
					this.propertyInfo.SetValue(obj2, value);
					if (flag)
					{
						GridEntry parentGridEntry = this.ParentGridEntry;
						if (parentGridEntry != null && parentGridEntry.IsValueEditable)
						{
							parentGridEntry.PropertyValue = obj;
						}
					}
				}
			}
			finally
			{
				Cursor.Current = value2;
			}
		}

		// Token: 0x06005523 RID: 21795 RVA: 0x00165484 File Offset: 0x00163684
		protected bool ViewEvent(object obj, string newHandler, EventDescriptor eventdesc, bool alwaysNavigate)
		{
			object propertyValueCore = this.GetPropertyValueCore(obj);
			string text = propertyValueCore as string;
			if (text == null && propertyValueCore != null && this.TypeConverter != null && this.TypeConverter.CanConvertTo(typeof(string)))
			{
				text = this.TypeConverter.ConvertToString(propertyValueCore);
			}
			if (newHandler == null && !string.IsNullOrEmpty(text))
			{
				newHandler = text;
			}
			else if (text == newHandler && !string.IsNullOrEmpty(newHandler))
			{
				return true;
			}
			IComponent component = obj as IComponent;
			if (component == null && this.propertyInfo is MergePropertyDescriptor)
			{
				Array array = obj as Array;
				if (array != null && array.Length > 0)
				{
					component = (array.GetValue(0) as IComponent);
				}
			}
			if (component == null)
			{
				return false;
			}
			if (this.propertyInfo.IsReadOnly)
			{
				return false;
			}
			if (eventdesc == null)
			{
				if (this.eventBindings == null)
				{
					this.eventBindings = (IEventBindingService)this.GetService(typeof(IEventBindingService));
				}
				if (this.eventBindings != null)
				{
					eventdesc = this.eventBindings.GetEvent(this.propertyInfo);
				}
			}
			IDesignerHost designerHost = this.DesignerHost;
			DesignerTransaction designerTransaction = null;
			try
			{
				if (eventdesc.EventType == null)
				{
					return false;
				}
				if (designerHost != null)
				{
					string str = (component.Site != null) ? component.Site.Name : component.GetType().Name;
					designerTransaction = this.DesignerHost.CreateTransaction(SR.GetString("WindowsFormsSetEvent", new object[]
					{
						str + "." + this.PropertyName
					}));
				}
				if (this.eventBindings == null)
				{
					ISite site = component.Site;
					if (site != null)
					{
						this.eventBindings = (IEventBindingService)site.GetService(typeof(IEventBindingService));
					}
				}
				if (newHandler == null && this.eventBindings != null)
				{
					newHandler = this.eventBindings.CreateUniqueMethodName(component, eventdesc);
				}
				if (newHandler != null)
				{
					if (this.eventBindings != null)
					{
						bool flag = false;
						foreach (object obj2 in this.eventBindings.GetCompatibleMethods(eventdesc))
						{
							string b = (string)obj2;
							if (newHandler == b)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							alwaysNavigate = true;
						}
					}
					try
					{
						this.propertyInfo.SetValue(obj, newHandler);
					}
					catch (InvalidOperationException ex)
					{
						if (designerTransaction != null)
						{
							designerTransaction.Cancel();
							designerTransaction = null;
						}
						if (this.GridEntryHost != null && this.GridEntryHost != null)
						{
							PropertyGridView gridEntryHost = this.GridEntryHost;
							gridEntryHost.ShowInvalidMessage(newHandler, obj, ex);
						}
						return false;
					}
				}
				if (alwaysNavigate && this.eventBindings != null)
				{
					PropertyDescriptorGridEntry.targetBindingService = this.eventBindings;
					PropertyDescriptorGridEntry.targetComponent = component;
					PropertyDescriptorGridEntry.targetEventdesc = eventdesc;
					Application.Idle += PropertyDescriptorGridEntry.ShowCodeIdle;
				}
			}
			catch
			{
				if (designerTransaction != null)
				{
					designerTransaction.Cancel();
					designerTransaction = null;
				}
				throw;
			}
			finally
			{
				if (designerTransaction != null)
				{
					designerTransaction.Commit();
				}
			}
			return true;
		}

		// Token: 0x06005524 RID: 21796 RVA: 0x001657B4 File Offset: 0x001639B4
		private static void ShowCodeIdle(object sender, EventArgs e)
		{
			Application.Idle -= PropertyDescriptorGridEntry.ShowCodeIdle;
			if (PropertyDescriptorGridEntry.targetBindingService != null)
			{
				PropertyDescriptorGridEntry.targetBindingService.ShowCode(PropertyDescriptorGridEntry.targetComponent, PropertyDescriptorGridEntry.targetEventdesc);
				PropertyDescriptorGridEntry.targetBindingService = null;
				PropertyDescriptorGridEntry.targetComponent = null;
				PropertyDescriptorGridEntry.targetEventdesc = null;
			}
		}

		// Token: 0x06005525 RID: 21797 RVA: 0x00165800 File Offset: 0x00163A00
		protected override GridEntry.GridEntryAccessibleObject GetAccessibilityObject()
		{
			if (AccessibilityImprovements.Level2)
			{
				return new PropertyDescriptorGridEntry.PropertyDescriptorGridEntryAccessibleObject(this);
			}
			return base.GetAccessibilityObject();
		}

		// Token: 0x04003732 RID: 14130
		internal PropertyDescriptor propertyInfo;

		// Token: 0x04003733 RID: 14131
		private TypeConverter exceptionConverter;

		// Token: 0x04003734 RID: 14132
		private UITypeEditor exceptionEditor;

		// Token: 0x04003735 RID: 14133
		private bool isSerializeContentsProp;

		// Token: 0x04003736 RID: 14134
		private byte parensAroundName = byte.MaxValue;

		// Token: 0x04003737 RID: 14135
		private IPropertyValueUIService pvSvc;

		// Token: 0x04003738 RID: 14136
		protected IEventBindingService eventBindings;

		// Token: 0x04003739 RID: 14137
		private bool pvSvcChecked;

		// Token: 0x0400373A RID: 14138
		private PropertyValueUIItem[] pvUIItems;

		// Token: 0x0400373B RID: 14139
		private Rectangle[] uiItemRects;

		// Token: 0x0400373C RID: 14140
		private bool readOnlyVerified;

		// Token: 0x0400373D RID: 14141
		private bool forceRenderReadOnly;

		// Token: 0x0400373E RID: 14142
		private string helpKeyword;

		// Token: 0x0400373F RID: 14143
		private string toolTipText;

		// Token: 0x04003740 RID: 14144
		private bool activeXHide;

		// Token: 0x04003741 RID: 14145
		private static int scaledImageSizeX = 8;

		// Token: 0x04003742 RID: 14146
		private static int scaledImageSizeY = 8;

		// Token: 0x04003743 RID: 14147
		private static bool isScalingInitialized = false;

		// Token: 0x04003744 RID: 14148
		private const int IMAGE_SIZE = 8;

		// Token: 0x04003745 RID: 14149
		private const byte ParensAroundNameUnknown = 255;

		// Token: 0x04003746 RID: 14150
		private const byte ParensAroundNameNo = 0;

		// Token: 0x04003747 RID: 14151
		private const byte ParensAroundNameYes = 1;

		// Token: 0x04003748 RID: 14152
		private static IEventBindingService targetBindingService;

		// Token: 0x04003749 RID: 14153
		private static IComponent targetComponent;

		// Token: 0x0400374A RID: 14154
		private static EventDescriptor targetEventdesc;

		// Token: 0x02000895 RID: 2197
		[ComVisible(true)]
		internal class PropertyDescriptorGridEntryAccessibleObject : GridEntry.GridEntryAccessibleObject
		{
			// Token: 0x0600724D RID: 29261 RVA: 0x001A2542 File Offset: 0x001A0742
			public PropertyDescriptorGridEntryAccessibleObject(PropertyDescriptorGridEntry owner) : base(owner)
			{
			}

			// Token: 0x0600724E RID: 29262 RVA: 0x001A379B File Offset: 0x001A199B
			internal override bool IsIAccessibleExSupported()
			{
				return !base.IsOwnerGridEntryCleared();
			}

			// Token: 0x0600724F RID: 29263 RVA: 0x001A37A8 File Offset: 0x001A19A8
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (base.IsOwnerGridEntryCleared())
				{
					return null;
				}
				if (AccessibilityImprovements.Level3)
				{
					switch (direction)
					{
					case UnsafeNativeMethods.NavigateDirection.NextSibling:
					{
						if (AccessibilityImprovements.Level5)
						{
							GridEntry parentGridEntry = this.owner.ParentGridEntry;
							PropertyDescriptorGridEntry.PropertyDescriptorGridEntryAccessibleObject propertyDescriptorGridEntryAccessibleObject = ((parentGridEntry != null) ? parentGridEntry.AccessibilityObject : null) as PropertyDescriptorGridEntry.PropertyDescriptorGridEntryAccessibleObject;
							if (propertyDescriptorGridEntryAccessibleObject != null)
							{
								return propertyDescriptorGridEntryAccessibleObject.GetNextChildFragment(this);
							}
						}
						PropertyGridView.PropertyGridViewAccessibleObject propertyGridViewAccessibleObject = (PropertyGridView.PropertyGridViewAccessibleObject)this.Parent;
						PropertyGridView propertyGridView = propertyGridViewAccessibleObject.Owner as PropertyGridView;
						bool flag = false;
						return propertyGridViewAccessibleObject.GetNextGridEntry(this.owner, propertyGridView.TopLevelGridEntries, out flag);
					}
					case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
					{
						if (AccessibilityImprovements.Level5)
						{
							GridEntry parentGridEntry2 = this.owner.ParentGridEntry;
							PropertyDescriptorGridEntry.PropertyDescriptorGridEntryAccessibleObject propertyDescriptorGridEntryAccessibleObject2 = ((parentGridEntry2 != null) ? parentGridEntry2.AccessibilityObject : null) as PropertyDescriptorGridEntry.PropertyDescriptorGridEntryAccessibleObject;
							if (propertyDescriptorGridEntryAccessibleObject2 != null)
							{
								return propertyDescriptorGridEntryAccessibleObject2.GetPreviousChildFragment(this);
							}
						}
						PropertyGridView.PropertyGridViewAccessibleObject propertyGridViewAccessibleObject = (PropertyGridView.PropertyGridViewAccessibleObject)this.Parent;
						PropertyGridView propertyGridView = propertyGridViewAccessibleObject.Owner as PropertyGridView;
						bool flag = false;
						return propertyGridViewAccessibleObject.GetPreviousGridEntry(this.owner, propertyGridView.TopLevelGridEntries, out flag);
					}
					case UnsafeNativeMethods.NavigateDirection.FirstChild:
						return this.GetFirstChild();
					case UnsafeNativeMethods.NavigateDirection.LastChild:
						return this.GetLastChild();
					}
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x06007250 RID: 29264 RVA: 0x001A38BC File Offset: 0x001A1ABC
			private UnsafeNativeMethods.IRawElementProviderFragment GetFirstChild()
			{
				if (AccessibilityImprovements.Level5)
				{
					if (this.GetChildFragmentCount() <= 0)
					{
						return null;
					}
					return this.GetChildFragment(0);
				}
				else
				{
					if (this.owner == null)
					{
						return null;
					}
					if (this.owner.ChildCount > 0)
					{
						return this.owner.Children.GetEntry(0).AccessibilityObject;
					}
					PropertyGridView propertyGridView = this.GetPropertyGridView();
					if (propertyGridView == null)
					{
						return null;
					}
					GridEntry selectedGridEntry = propertyGridView.SelectedGridEntry;
					if (this.owner != selectedGridEntry)
					{
						return null;
					}
					if (selectedGridEntry.Enumerable)
					{
						return propertyGridView.DropDownListBoxAccessibleObject;
					}
					return propertyGridView.EditAccessibleObject;
				}
			}

			// Token: 0x06007251 RID: 29265 RVA: 0x001A3948 File Offset: 0x001A1B48
			private UnsafeNativeMethods.IRawElementProviderFragment GetLastChild()
			{
				if (AccessibilityImprovements.Level5)
				{
					int childFragmentCount = this.GetChildFragmentCount();
					if (childFragmentCount <= 0)
					{
						return null;
					}
					return this.GetChildFragment(childFragmentCount - 1);
				}
				else
				{
					if (this.owner == null)
					{
						return null;
					}
					if (this.owner.ChildCount > 0)
					{
						return this.owner.Children.GetEntry(this.owner.ChildCount - 1).AccessibilityObject;
					}
					PropertyGridView propertyGridView = this.GetPropertyGridView();
					if (propertyGridView == null)
					{
						return null;
					}
					GridEntry selectedGridEntry = propertyGridView.SelectedGridEntry;
					if (this.owner != selectedGridEntry)
					{
						return null;
					}
					if (selectedGridEntry.Enumerable && propertyGridView.DropDownButton.Visible)
					{
						return propertyGridView.DropDownButton.AccessibilityObject;
					}
					return propertyGridView.EditAccessibleObject;
				}
			}

			// Token: 0x06007252 RID: 29266 RVA: 0x001A39F4 File Offset: 0x001A1BF4
			internal UnsafeNativeMethods.IRawElementProviderFragment GetNextChildFragment(UnsafeNativeMethods.IRawElementProviderFragment child)
			{
				int childFragmentIndex = this.GetChildFragmentIndex(child);
				int num = this.GetChildFragmentCount() - 1;
				if (childFragmentIndex == -1 || childFragmentIndex == num)
				{
					return null;
				}
				return this.GetChildFragment(childFragmentIndex + 1);
			}

			// Token: 0x06007253 RID: 29267 RVA: 0x001A3A28 File Offset: 0x001A1C28
			internal UnsafeNativeMethods.IRawElementProviderFragment GetPreviousChildFragment(UnsafeNativeMethods.IRawElementProviderFragment child)
			{
				int childFragmentIndex = this.GetChildFragmentIndex(child);
				if (childFragmentIndex == -1 || childFragmentIndex == 0)
				{
					return null;
				}
				return this.GetChildFragment(childFragmentIndex - 1);
			}

			// Token: 0x06007254 RID: 29268 RVA: 0x001A3A50 File Offset: 0x001A1C50
			private UnsafeNativeMethods.IRawElementProviderFragment GetChildFragment(int index)
			{
				if (base.IsOwnerGridEntryCleared())
				{
					return null;
				}
				PropertyGridView propertyGridView = this.GetPropertyGridView();
				if (propertyGridView != null && propertyGridView.SelectedGridEntry == this.owner)
				{
					if (propertyGridView.DropDownVisible && propertyGridView.DropDownControlHolder.Component == propertyGridView.DropDownListBox)
					{
						if (index == 0)
						{
							return propertyGridView.DropDownListBoxAccessibleObject;
						}
						index--;
					}
					if (index == 0)
					{
						return propertyGridView.EditAccessibleObject;
					}
					index--;
					if (propertyGridView.DropDownButton.Visible)
					{
						if (index == 0)
						{
							return propertyGridView.DropDownButton.AccessibilityObject;
						}
						index--;
					}
					else if (propertyGridView.DialogButton.Visible)
					{
						if (index == 0)
						{
							return propertyGridView.DialogButton.AccessibilityObject;
						}
						index--;
					}
				}
				if (this.owner.ChildCount > 0 && this.owner.Expanded && index < this.owner.ChildCount)
				{
					return this.owner.Children.GetEntry(index).AccessibilityObject;
				}
				return null;
			}

			// Token: 0x06007255 RID: 29269 RVA: 0x001A3B40 File Offset: 0x001A1D40
			private int GetChildFragmentCount()
			{
				if (base.IsOwnerGridEntryCleared())
				{
					return 0;
				}
				int num = 0;
				PropertyGridView propertyGridView = this.GetPropertyGridView();
				if (propertyGridView != null && propertyGridView.SelectedGridEntry == this.owner)
				{
					if (propertyGridView.DropDownVisible && propertyGridView.DropDownControlHolder.Component == propertyGridView.DropDownListBox)
					{
						num++;
					}
					num++;
					if (propertyGridView.DropDownButton.Visible)
					{
						num++;
					}
					else if (propertyGridView.DialogButton.Visible)
					{
						num++;
					}
				}
				if (this.owner.Expanded)
				{
					num += this.owner.ChildCount;
				}
				return num;
			}

			// Token: 0x06007256 RID: 29270 RVA: 0x001A3BD4 File Offset: 0x001A1DD4
			private int GetChildFragmentIndex(UnsafeNativeMethods.IRawElementProviderFragment child)
			{
				if (base.IsOwnerGridEntryCleared())
				{
					return -1;
				}
				int num = 0;
				PropertyGridView propertyGridView = this.GetPropertyGridView();
				if (propertyGridView != null && propertyGridView.SelectedGridEntry == this.owner)
				{
					if (propertyGridView.DropDownVisible && propertyGridView.DropDownControlHolder.Component == propertyGridView.DropDownListBox)
					{
						if (child == propertyGridView.DropDownListBoxAccessibleObject)
						{
							return num;
						}
						num++;
					}
					if (child == propertyGridView.EditAccessibleObject)
					{
						return num;
					}
					num++;
					if (propertyGridView.DropDownButton.Visible)
					{
						if (child == propertyGridView.DropDownButton.AccessibilityObject)
						{
							return num;
						}
						num++;
					}
					else if (propertyGridView.DialogButton.Visible)
					{
						if (child == propertyGridView.DialogButton.AccessibilityObject)
						{
							return num;
						}
						num++;
					}
				}
				if (this.owner.ChildCount > 0 && this.owner.Expanded)
				{
					foreach (object obj in this.owner.Children)
					{
						if (child == ((GridEntry)obj).AccessibilityObject)
						{
							return num;
						}
						num++;
					}
					return -1;
				}
				return -1;
			}

			// Token: 0x06007257 RID: 29271 RVA: 0x001A3D04 File Offset: 0x001A1F04
			private PropertyGridView GetPropertyGridView()
			{
				PropertyGridView.PropertyGridViewAccessibleObject propertyGridViewAccessibleObject = this.Parent as PropertyGridView.PropertyGridViewAccessibleObject;
				if (propertyGridViewAccessibleObject == null)
				{
					return null;
				}
				return propertyGridViewAccessibleObject.Owner as PropertyGridView;
			}

			// Token: 0x06007258 RID: 29272 RVA: 0x001A3D2D File Offset: 0x001A1F2D
			internal override bool IsPatternSupported(int patternId)
			{
				return !base.IsOwnerGridEntryCleared() && ((AccessibilityImprovements.Level3 && patternId == 10002) || (patternId == 10005 && this.owner.Enumerable) || base.IsPatternSupported(patternId));
			}

			// Token: 0x06007259 RID: 29273 RVA: 0x001A3D66 File Offset: 0x001A1F66
			internal override void Expand()
			{
				if (this.ExpandCollapseState == UnsafeNativeMethods.ExpandCollapseState.Collapsed)
				{
					this.ExpandOrCollapse();
				}
			}

			// Token: 0x0600725A RID: 29274 RVA: 0x001A3D76 File Offset: 0x001A1F76
			internal override void Collapse()
			{
				if (this.ExpandCollapseState == UnsafeNativeMethods.ExpandCollapseState.Expanded)
				{
					this.ExpandOrCollapse();
				}
			}

			// Token: 0x0600725B RID: 29275 RVA: 0x001A3D88 File Offset: 0x001A1F88
			private void ExpandOrCollapse()
			{
				PropertyGridView propertyGridView = this.GetPropertyGridView();
				if (propertyGridView == null)
				{
					return;
				}
				int rowFromGridEntryInternal = propertyGridView.GetRowFromGridEntryInternal(this.owner);
				if (rowFromGridEntryInternal != -1)
				{
					propertyGridView.PopupDialog(rowFromGridEntryInternal);
				}
			}

			// Token: 0x17001913 RID: 6419
			// (get) Token: 0x0600725C RID: 29276 RVA: 0x001A3DB8 File Offset: 0x001A1FB8
			internal override UnsafeNativeMethods.ExpandCollapseState ExpandCollapseState
			{
				get
				{
					if (base.IsOwnerGridEntryCleared())
					{
						return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
					}
					PropertyGridView propertyGridView = this.GetPropertyGridView();
					if (propertyGridView == null)
					{
						return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
					}
					if (this.owner == propertyGridView.SelectedGridEntry && ((AccessibilityImprovements.Level4 && this.owner != null && this.owner.InternalExpanded) || propertyGridView.DropDownVisible))
					{
						return UnsafeNativeMethods.ExpandCollapseState.Expanded;
					}
					return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
				}
			}

			// Token: 0x0600725D RID: 29277 RVA: 0x001A3E10 File Offset: 0x001A2010
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30010)
				{
					return !base.IsOwnerGridEntryCleared() && !((PropertyDescriptorGridEntry)this.owner).IsPropertyReadOnly;
				}
				if (AccessibilityImprovements.Level3)
				{
					if (propertyID == 30100)
					{
						return string.Empty;
					}
					if (propertyID == 30043)
					{
						return true;
					}
				}
				return base.GetPropertyValue(propertyID);
			}
		}

		// Token: 0x02000896 RID: 2198
		private class ExceptionConverter : TypeConverter
		{
			// Token: 0x0600725E RID: 29278 RVA: 0x001A3E74 File Offset: 0x001A2074
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (!(destinationType == typeof(string)))
				{
					throw base.GetConvertToException(value, destinationType);
				}
				if (value is Exception)
				{
					Exception ex = (Exception)value;
					if (ex.InnerException != null)
					{
						ex = ex.InnerException;
					}
					return ex.Message;
				}
				return null;
			}
		}

		// Token: 0x02000897 RID: 2199
		private class ExceptionEditor : UITypeEditor
		{
			// Token: 0x06007260 RID: 29280 RVA: 0x001A3EC4 File Offset: 0x001A20C4
			public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
			{
				Exception ex = value as Exception;
				if (ex != null)
				{
					IUIService iuiservice = null;
					if (context != null)
					{
						iuiservice = (IUIService)context.GetService(typeof(IUIService));
					}
					if (iuiservice != null)
					{
						iuiservice.ShowError(ex);
					}
					else
					{
						string text = ex.Message;
						if (text == null || text.Length == 0)
						{
							text = ex.ToString();
						}
						RTLAwareMessageBox.Show(null, text, SR.GetString("PropertyGridExceptionInfo"), MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
					}
				}
				return value;
			}

			// Token: 0x06007261 RID: 29281 RVA: 0x0001627D File Offset: 0x0001447D
			public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
			{
				return UITypeEditorEditStyle.Modal;
			}
		}
	}
}
