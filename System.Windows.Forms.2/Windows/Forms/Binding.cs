using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000130 RID: 304
	[TypeConverter(typeof(ListBindingConverter))]
	public class Binding
	{
		// Token: 0x06000ABB RID: 2747 RVA: 0x0001E610 File Offset: 0x0001C810
		public Binding(string propertyName, object dataSource, string dataMember) : this(propertyName, dataSource, dataMember, false, DataSourceUpdateMode.OnValidation, null, string.Empty, null)
		{
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0001E630 File Offset: 0x0001C830
		public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled) : this(propertyName, dataSource, dataMember, formattingEnabled, DataSourceUpdateMode.OnValidation, null, string.Empty, null)
		{
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0001E650 File Offset: 0x0001C850
		public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode) : this(propertyName, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode, null, string.Empty, null)
		{
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0001E674 File Offset: 0x0001C874
		public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue) : this(propertyName, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode, nullValue, string.Empty, null)
		{
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0001E698 File Offset: 0x0001C898
		public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue, string formatString) : this(propertyName, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode, nullValue, formatString, null)
		{
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0001E6B8 File Offset: 0x0001C8B8
		public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue, string formatString, IFormatProvider formatInfo)
		{
			this.propertyName = "";
			this.formatString = string.Empty;
			this.dsNullValue = Formatter.GetDefaultDataSourceNullValue(null);
			base..ctor();
			this.bindToObject = new BindToObject(this, dataSource, dataMember);
			this.propertyName = propertyName;
			this.formattingEnabled = formattingEnabled;
			this.formatString = formatString;
			this.nullValue = nullValue;
			this.formatInfo = formatInfo;
			this.formattingEnabled = formattingEnabled;
			this.dataSourceUpdateMode = dataSourceUpdateMode;
			this.CheckBinding();
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0001E738 File Offset: 0x0001C938
		private Binding()
		{
			this.propertyName = "";
			this.formatString = string.Empty;
			this.dsNullValue = Formatter.GetDefaultDataSourceNullValue(null);
			base..ctor();
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0001E762 File Offset: 0x0001C962
		internal BindToObject BindToObject
		{
			get
			{
				return this.bindToObject;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0001E76A File Offset: 0x0001C96A
		public object DataSource
		{
			get
			{
				return this.bindToObject.DataSource;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x0001E777 File Offset: 0x0001C977
		public BindingMemberInfo BindingMemberInfo
		{
			get
			{
				return this.bindToObject.BindingMemberInfo;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0001E784 File Offset: 0x0001C984
		[DefaultValue(null)]
		public IBindableComponent BindableComponent
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return this.control;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0001E78C File Offset: 0x0001C98C
		[DefaultValue(null)]
		public Control Control
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return this.control as Control;
			}
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0001E79C File Offset: 0x0001C99C
		internal static bool IsComponentCreated(IBindableComponent component)
		{
			Control control = component as Control;
			return control == null || control.Created;
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x0001E7BB File Offset: 0x0001C9BB
		internal bool ComponentCreated
		{
			get
			{
				return Binding.IsComponentCreated(this.control);
			}
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0001E7C8 File Offset: 0x0001C9C8
		private void FormLoaded(object sender, EventArgs e)
		{
			this.CheckBinding();
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0001E7D0 File Offset: 0x0001C9D0
		internal void SetBindableComponent(IBindableComponent value)
		{
			if (this.control != value)
			{
				IBindableComponent bindableComponent = this.control;
				this.BindTarget(false);
				this.control = value;
				this.BindTarget(true);
				try
				{
					this.CheckBinding();
				}
				catch
				{
					this.BindTarget(false);
					this.control = bindableComponent;
					this.BindTarget(true);
					throw;
				}
				BindingContext.UpdateBinding((this.control != null && Binding.IsComponentCreated(this.control)) ? this.control.BindingContext : null, this);
				Form form = value as Form;
				if (form != null)
				{
					form.Load += this.FormLoaded;
				}
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0001E87C File Offset: 0x0001CA7C
		public bool IsBinding
		{
			get
			{
				return this.bound;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0001E884 File Offset: 0x0001CA84
		public BindingManagerBase BindingManagerBase
		{
			get
			{
				return this.bindingManagerBase;
			}
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0001E88C File Offset: 0x0001CA8C
		internal void SetListManager(BindingManagerBase bindingManagerBase)
		{
			if (this.bindingManagerBase is CurrencyManager)
			{
				((CurrencyManager)this.bindingManagerBase).MetaDataChanged -= this.binding_MetaDataChanged;
			}
			this.bindingManagerBase = bindingManagerBase;
			if (this.bindingManagerBase is CurrencyManager)
			{
				((CurrencyManager)this.bindingManagerBase).MetaDataChanged += this.binding_MetaDataChanged;
			}
			this.BindToObject.SetBindingManagerBase(bindingManagerBase);
			this.CheckBinding();
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x0001E904 File Offset: 0x0001CB04
		[DefaultValue("")]
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06000ACF RID: 2767 RVA: 0x0001E90C File Offset: 0x0001CB0C
		// (remove) Token: 0x06000AD0 RID: 2768 RVA: 0x0001E925 File Offset: 0x0001CB25
		public event BindingCompleteEventHandler BindingComplete
		{
			add
			{
				this.onComplete = (BindingCompleteEventHandler)Delegate.Combine(this.onComplete, value);
			}
			remove
			{
				this.onComplete = (BindingCompleteEventHandler)Delegate.Remove(this.onComplete, value);
			}
		}

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06000AD1 RID: 2769 RVA: 0x0001E93E File Offset: 0x0001CB3E
		// (remove) Token: 0x06000AD2 RID: 2770 RVA: 0x0001E957 File Offset: 0x0001CB57
		public event ConvertEventHandler Parse
		{
			add
			{
				this.onParse = (ConvertEventHandler)Delegate.Combine(this.onParse, value);
			}
			remove
			{
				this.onParse = (ConvertEventHandler)Delegate.Remove(this.onParse, value);
			}
		}

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06000AD3 RID: 2771 RVA: 0x0001E970 File Offset: 0x0001CB70
		// (remove) Token: 0x06000AD4 RID: 2772 RVA: 0x0001E989 File Offset: 0x0001CB89
		public event ConvertEventHandler Format
		{
			add
			{
				this.onFormat = (ConvertEventHandler)Delegate.Combine(this.onFormat, value);
			}
			remove
			{
				this.onFormat = (ConvertEventHandler)Delegate.Remove(this.onFormat, value);
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x0001E9A2 File Offset: 0x0001CBA2
		// (set) Token: 0x06000AD6 RID: 2774 RVA: 0x0001E9AA File Offset: 0x0001CBAA
		[DefaultValue(false)]
		public bool FormattingEnabled
		{
			get
			{
				return this.formattingEnabled;
			}
			set
			{
				if (this.formattingEnabled != value)
				{
					this.formattingEnabled = value;
					if (this.IsBinding)
					{
						this.PushData();
					}
				}
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x0001E9CB File Offset: 0x0001CBCB
		// (set) Token: 0x06000AD8 RID: 2776 RVA: 0x0001E9D3 File Offset: 0x0001CBD3
		[DefaultValue(null)]
		public IFormatProvider FormatInfo
		{
			get
			{
				return this.formatInfo;
			}
			set
			{
				if (this.formatInfo != value)
				{
					this.formatInfo = value;
					if (this.IsBinding)
					{
						this.PushData();
					}
				}
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		// (set) Token: 0x06000ADA RID: 2778 RVA: 0x0001E9FC File Offset: 0x0001CBFC
		public string FormatString
		{
			get
			{
				return this.formatString;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (!value.Equals(this.formatString))
				{
					this.formatString = value;
					if (this.IsBinding)
					{
						this.PushData();
					}
				}
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0001EA2C File Offset: 0x0001CC2C
		// (set) Token: 0x06000ADC RID: 2780 RVA: 0x0001EA34 File Offset: 0x0001CC34
		public object NullValue
		{
			get
			{
				return this.nullValue;
			}
			set
			{
				if (!object.Equals(this.nullValue, value))
				{
					this.nullValue = value;
					if (this.IsBinding && Formatter.IsNullData(this.bindToObject.GetValue(), this.dsNullValue))
					{
						this.PushData();
					}
				}
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0001EA72 File Offset: 0x0001CC72
		// (set) Token: 0x06000ADE RID: 2782 RVA: 0x0001EA7C File Offset: 0x0001CC7C
		public object DataSourceNullValue
		{
			get
			{
				return this.dsNullValue;
			}
			set
			{
				if (!object.Equals(this.dsNullValue, value))
				{
					object dataSourceNullValue = this.dsNullValue;
					this.dsNullValue = value;
					this.dsNullValueSet = true;
					if (this.IsBinding)
					{
						object value2 = this.bindToObject.GetValue();
						if (Formatter.IsNullData(value2, dataSourceNullValue))
						{
							this.WriteValue();
						}
						if (Formatter.IsNullData(value2, value))
						{
							this.ReadValue();
						}
					}
				}
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x0001EADE File Offset: 0x0001CCDE
		// (set) Token: 0x06000AE0 RID: 2784 RVA: 0x0001EAE6 File Offset: 0x0001CCE6
		[DefaultValue(ControlUpdateMode.OnPropertyChanged)]
		public ControlUpdateMode ControlUpdateMode
		{
			get
			{
				return this.controlUpdateMode;
			}
			set
			{
				if (this.controlUpdateMode != value)
				{
					this.controlUpdateMode = value;
					if (this.IsBinding)
					{
						this.PushData();
					}
				}
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0001EB07 File Offset: 0x0001CD07
		// (set) Token: 0x06000AE2 RID: 2786 RVA: 0x0001EB0F File Offset: 0x0001CD0F
		[DefaultValue(DataSourceUpdateMode.OnValidation)]
		public DataSourceUpdateMode DataSourceUpdateMode
		{
			get
			{
				return this.dataSourceUpdateMode;
			}
			set
			{
				if (this.dataSourceUpdateMode != value)
				{
					this.dataSourceUpdateMode = value;
				}
			}
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0001EB24 File Offset: 0x0001CD24
		private void BindTarget(bool bind)
		{
			if (bind)
			{
				if (this.IsBinding)
				{
					if (this.propInfo != null && this.control != null)
					{
						EventHandler handler = new EventHandler(this.Target_PropertyChanged);
						this.propInfo.AddValueChanged(this.control, handler);
					}
					if (this.validateInfo != null)
					{
						CancelEventHandler value = new CancelEventHandler(this.Target_Validate);
						this.validateInfo.AddEventHandler(this.control, value);
						return;
					}
				}
			}
			else
			{
				if (this.propInfo != null && this.control != null)
				{
					EventHandler handler2 = new EventHandler(this.Target_PropertyChanged);
					this.propInfo.RemoveValueChanged(this.control, handler2);
				}
				if (this.validateInfo != null)
				{
					CancelEventHandler value2 = new CancelEventHandler(this.Target_Validate);
					this.validateInfo.RemoveEventHandler(this.control, value2);
				}
			}
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0001E7C8 File Offset: 0x0001C9C8
		private void binding_MetaDataChanged(object sender, EventArgs e)
		{
			this.CheckBinding();
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0001EBEC File Offset: 0x0001CDEC
		private void CheckBinding()
		{
			this.bindToObject.CheckBinding();
			if (this.control != null && this.propertyName.Length > 0)
			{
				this.control.DataBindings.CheckDuplicates(this);
				Type type = this.control.GetType();
				string b = this.propertyName + "IsNull";
				PropertyDescriptor propertyDescriptor = null;
				PropertyDescriptor propertyDescriptor2 = null;
				InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(this.control)[typeof(InheritanceAttribute)];
				PropertyDescriptorCollection properties;
				if (inheritanceAttribute != null && inheritanceAttribute.InheritanceLevel != InheritanceLevel.NotInherited)
				{
					properties = TypeDescriptor.GetProperties(type);
				}
				else
				{
					properties = TypeDescriptor.GetProperties(this.control);
				}
				for (int i = 0; i < properties.Count; i++)
				{
					if (propertyDescriptor == null && string.Equals(properties[i].Name, this.propertyName, StringComparison.OrdinalIgnoreCase))
					{
						propertyDescriptor = properties[i];
						if (propertyDescriptor2 != null)
						{
							break;
						}
					}
					if (propertyDescriptor2 == null && string.Equals(properties[i].Name, b, StringComparison.OrdinalIgnoreCase))
					{
						propertyDescriptor2 = properties[i];
						if (propertyDescriptor != null)
						{
							break;
						}
					}
				}
				if (propertyDescriptor == null)
				{
					throw new ArgumentException(SR.GetString("ListBindingBindProperty", new object[]
					{
						this.propertyName
					}), "PropertyName");
				}
				if (propertyDescriptor.IsReadOnly && this.controlUpdateMode != ControlUpdateMode.Never)
				{
					throw new ArgumentException(SR.GetString("ListBindingBindPropertyReadOnly", new object[]
					{
						this.propertyName
					}), "PropertyName");
				}
				this.propInfo = propertyDescriptor;
				Type propertyType = this.propInfo.PropertyType;
				this.propInfoConverter = this.propInfo.Converter;
				if (propertyDescriptor2 != null && propertyDescriptor2.PropertyType == typeof(bool) && !propertyDescriptor2.IsReadOnly)
				{
					this.propIsNullInfo = propertyDescriptor2;
				}
				EventDescriptor eventDescriptor = null;
				string b2 = "Validating";
				EventDescriptorCollection events = TypeDescriptor.GetEvents(this.control);
				for (int j = 0; j < events.Count; j++)
				{
					if (eventDescriptor == null && string.Equals(events[j].Name, b2, StringComparison.OrdinalIgnoreCase))
					{
						eventDescriptor = events[j];
						break;
					}
				}
				this.validateInfo = eventDescriptor;
			}
			else
			{
				this.propInfo = null;
				this.validateInfo = null;
			}
			this.UpdateIsBinding();
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0001EE28 File Offset: 0x0001D028
		internal bool ControlAtDesignTime()
		{
			IComponent component = this.control;
			if (component == null)
			{
				return false;
			}
			ISite site = component.Site;
			return site != null && site.DesignMode;
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0001EE53 File Offset: 0x0001D053
		private object GetDataSourceNullValue(Type type)
		{
			if (!this.dsNullValueSet)
			{
				return Formatter.GetDefaultDataSourceNullValue(type);
			}
			return this.dsNullValue;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0001EE6C File Offset: 0x0001D06C
		private object GetPropValue()
		{
			bool flag = false;
			if (this.propIsNullInfo != null)
			{
				flag = (bool)this.propIsNullInfo.GetValue(this.control);
			}
			object obj;
			if (flag)
			{
				obj = this.DataSourceNullValue;
			}
			else
			{
				obj = this.propInfo.GetValue(this.control);
				if (obj == null)
				{
					obj = this.DataSourceNullValue;
				}
			}
			return obj;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0001EEC4 File Offset: 0x0001D0C4
		private BindingCompleteEventArgs CreateBindingCompleteEventArgs(BindingCompleteContext context, Exception ex)
		{
			bool cancel = false;
			string text = string.Empty;
			BindingCompleteState state = BindingCompleteState.Success;
			if (ex != null)
			{
				text = ex.Message;
				state = BindingCompleteState.Exception;
				cancel = true;
			}
			else
			{
				text = this.BindToObject.DataErrorText;
				if (!string.IsNullOrEmpty(text))
				{
					state = BindingCompleteState.DataError;
				}
			}
			return new BindingCompleteEventArgs(this, state, context, text, ex, cancel);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0001EF0C File Offset: 0x0001D10C
		protected virtual void OnBindingComplete(BindingCompleteEventArgs e)
		{
			if (!this.inOnBindingComplete)
			{
				try
				{
					this.inOnBindingComplete = true;
					if (this.onComplete != null)
					{
						this.onComplete(this, e);
					}
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
					e.Cancel = true;
				}
				finally
				{
					this.inOnBindingComplete = false;
				}
			}
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0001EF78 File Offset: 0x0001D178
		protected virtual void OnParse(ConvertEventArgs cevent)
		{
			if (this.onParse != null)
			{
				this.onParse(this, cevent);
			}
			if (!this.formattingEnabled && !(cevent.Value is DBNull) && cevent.Value != null && cevent.DesiredType != null && !cevent.DesiredType.IsInstanceOfType(cevent.Value) && cevent.Value is IConvertible)
			{
				cevent.Value = Convert.ChangeType(cevent.Value, cevent.DesiredType, CultureInfo.CurrentCulture);
			}
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0001F004 File Offset: 0x0001D204
		protected virtual void OnFormat(ConvertEventArgs cevent)
		{
			if (this.onFormat != null)
			{
				this.onFormat(this, cevent);
			}
			if (!this.formattingEnabled && !(cevent.Value is DBNull) && cevent.DesiredType != null && !cevent.DesiredType.IsInstanceOfType(cevent.Value) && cevent.Value is IConvertible)
			{
				cevent.Value = Convert.ChangeType(cevent.Value, cevent.DesiredType, CultureInfo.CurrentCulture);
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0001F088 File Offset: 0x0001D288
		private object ParseObject(object value)
		{
			Type bindToType = this.bindToObject.BindToType;
			if (this.formattingEnabled)
			{
				ConvertEventArgs convertEventArgs = new ConvertEventArgs(value, bindToType);
				this.OnParse(convertEventArgs);
				object value2 = convertEventArgs.Value;
				if (!object.Equals(value, value2))
				{
					return value2;
				}
				TypeConverter targetConverter = null;
				if (this.bindToObject.FieldInfo != null)
				{
					targetConverter = this.bindToObject.FieldInfo.Converter;
				}
				return Formatter.ParseObject(value, bindToType, (value == null) ? this.propInfo.PropertyType : value.GetType(), targetConverter, this.propInfoConverter, this.formatInfo, this.nullValue, this.GetDataSourceNullValue(bindToType));
			}
			else
			{
				ConvertEventArgs convertEventArgs2 = new ConvertEventArgs(value, bindToType);
				this.OnParse(convertEventArgs2);
				if (convertEventArgs2.Value != null && (convertEventArgs2.Value.GetType().IsSubclassOf(bindToType) || convertEventArgs2.Value.GetType() == bindToType || convertEventArgs2.Value is DBNull))
				{
					return convertEventArgs2.Value;
				}
				TypeConverter converter = TypeDescriptor.GetConverter((value != null) ? value.GetType() : typeof(object));
				if (converter != null && converter.CanConvertTo(bindToType))
				{
					return converter.ConvertTo(value, bindToType);
				}
				if (value is IConvertible)
				{
					object obj = Convert.ChangeType(value, bindToType, CultureInfo.CurrentCulture);
					if (obj != null && (obj.GetType().IsSubclassOf(bindToType) || obj.GetType() == bindToType))
					{
						return obj;
					}
				}
				return null;
			}
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0001F1EC File Offset: 0x0001D3EC
		private object FormatObject(object value)
		{
			if (this.ControlAtDesignTime())
			{
				return value;
			}
			Type propertyType = this.propInfo.PropertyType;
			if (this.formattingEnabled)
			{
				ConvertEventArgs convertEventArgs = new ConvertEventArgs(value, propertyType);
				this.OnFormat(convertEventArgs);
				if (convertEventArgs.Value != value)
				{
					return convertEventArgs.Value;
				}
				TypeConverter sourceConverter = null;
				if (this.bindToObject.FieldInfo != null)
				{
					sourceConverter = this.bindToObject.FieldInfo.Converter;
				}
				return Formatter.FormatObject(value, propertyType, sourceConverter, this.propInfoConverter, this.formatString, this.formatInfo, this.nullValue, this.dsNullValue);
			}
			else
			{
				ConvertEventArgs convertEventArgs2 = new ConvertEventArgs(value, propertyType);
				this.OnFormat(convertEventArgs2);
				object obj = convertEventArgs2.Value;
				if (propertyType == typeof(object))
				{
					return value;
				}
				if (obj != null && (obj.GetType().IsSubclassOf(propertyType) || obj.GetType() == propertyType))
				{
					return obj;
				}
				TypeConverter converter = TypeDescriptor.GetConverter((value != null) ? value.GetType() : typeof(object));
				if (converter != null && converter.CanConvertTo(propertyType))
				{
					return converter.ConvertTo(value, propertyType);
				}
				if (value is IConvertible)
				{
					obj = Convert.ChangeType(value, propertyType, CultureInfo.CurrentCulture);
					if (obj != null && (obj.GetType().IsSubclassOf(propertyType) || obj.GetType() == propertyType))
					{
						return obj;
					}
				}
				throw new FormatException(SR.GetString("ListBindingFormatFailed"));
			}
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0001F34F File Offset: 0x0001D54F
		internal bool PullData()
		{
			return this.PullData(true, false);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0001F359 File Offset: 0x0001D559
		internal bool PullData(bool reformat)
		{
			return this.PullData(reformat, false);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0001F364 File Offset: 0x0001D564
		internal bool PullData(bool reformat, bool force)
		{
			if (this.ControlUpdateMode == ControlUpdateMode.Never)
			{
				reformat = false;
			}
			bool flag = false;
			object obj = null;
			Exception ex = null;
			if (!this.IsBinding)
			{
				return false;
			}
			if (!force)
			{
				if (this.propInfo.SupportsChangeEvents && !this.modified)
				{
					return false;
				}
				if (this.DataSourceUpdateMode == DataSourceUpdateMode.Never)
				{
					return false;
				}
			}
			if (this.inPushOrPull && this.formattingEnabled)
			{
				return false;
			}
			this.inPushOrPull = true;
			object propValue = this.GetPropValue();
			try
			{
				obj = this.ParseObject(propValue);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			try
			{
				if (ex != null || (!this.FormattingEnabled && obj == null))
				{
					flag = true;
					obj = this.bindToObject.GetValue();
				}
				if (reformat && (!this.FormattingEnabled || !flag))
				{
					object obj2 = this.FormatObject(obj);
					if (force || !this.FormattingEnabled || !object.Equals(obj2, propValue))
					{
						this.SetPropValue(obj2);
					}
				}
				if (!flag)
				{
					this.bindToObject.SetValue(obj);
				}
			}
			catch (Exception ex3)
			{
				ex = ex3;
				if (!this.FormattingEnabled)
				{
					throw;
				}
			}
			finally
			{
				this.inPushOrPull = false;
			}
			if (this.FormattingEnabled)
			{
				BindingCompleteEventArgs bindingCompleteEventArgs = this.CreateBindingCompleteEventArgs(BindingCompleteContext.DataSourceUpdate, ex);
				this.OnBindingComplete(bindingCompleteEventArgs);
				if (bindingCompleteEventArgs.BindingCompleteState == BindingCompleteState.Success && !bindingCompleteEventArgs.Cancel)
				{
					this.modified = false;
				}
				return bindingCompleteEventArgs.Cancel;
			}
			this.modified = false;
			return false;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0001F4C8 File Offset: 0x0001D6C8
		internal bool PushData()
		{
			return this.PushData(false);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0001F4D4 File Offset: 0x0001D6D4
		internal bool PushData(bool force)
		{
			Exception ex = null;
			if (!force && this.ControlUpdateMode == ControlUpdateMode.Never)
			{
				return false;
			}
			if (this.inPushOrPull && this.formattingEnabled)
			{
				return false;
			}
			this.inPushOrPull = true;
			try
			{
				if (this.IsBinding)
				{
					object value = this.bindToObject.GetValue();
					object propValue = this.FormatObject(value);
					this.SetPropValue(propValue);
					this.modified = false;
				}
				else
				{
					this.SetPropValue(null);
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
				if (!this.FormattingEnabled)
				{
					throw;
				}
			}
			finally
			{
				this.inPushOrPull = false;
			}
			if (this.FormattingEnabled)
			{
				BindingCompleteEventArgs bindingCompleteEventArgs = this.CreateBindingCompleteEventArgs(BindingCompleteContext.ControlUpdate, ex);
				this.OnBindingComplete(bindingCompleteEventArgs);
				return bindingCompleteEventArgs.Cancel;
			}
			return false;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0001F598 File Offset: 0x0001D798
		public void ReadValue()
		{
			this.PushData(true);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0001F5A2 File Offset: 0x0001D7A2
		public void WriteValue()
		{
			this.PullData(true, true);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0001F5B0 File Offset: 0x0001D7B0
		private void SetPropValue(object value)
		{
			if (this.ControlAtDesignTime())
			{
				return;
			}
			this.inSetPropValue = true;
			try
			{
				bool flag = value == null || Formatter.IsNullData(value, this.DataSourceNullValue);
				if (flag)
				{
					if (this.propIsNullInfo != null)
					{
						this.propIsNullInfo.SetValue(this.control, true);
					}
					else if (this.propInfo.PropertyType == typeof(object))
					{
						this.propInfo.SetValue(this.control, this.DataSourceNullValue);
					}
					else
					{
						this.propInfo.SetValue(this.control, null);
					}
				}
				else
				{
					this.propInfo.SetValue(this.control, value);
				}
			}
			finally
			{
				this.inSetPropValue = false;
			}
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0001F67C File Offset: 0x0001D87C
		private bool ShouldSerializeFormatString()
		{
			return this.formatString != null && this.formatString.Length > 0;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0001F696 File Offset: 0x0001D896
		private bool ShouldSerializeNullValue()
		{
			return this.nullValue != null;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0001F6A1 File Offset: 0x0001D8A1
		private bool ShouldSerializeDataSourceNullValue()
		{
			return this.dsNullValueSet && this.dsNullValue != Formatter.GetDefaultDataSourceNullValue(null);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0001F6BE File Offset: 0x0001D8BE
		private void Target_PropertyChanged(object sender, EventArgs e)
		{
			if (this.inSetPropValue)
			{
				return;
			}
			if (this.IsBinding)
			{
				this.modified = true;
				if (this.DataSourceUpdateMode == DataSourceUpdateMode.OnPropertyChanged)
				{
					this.PullData(false);
					this.modified = true;
				}
			}
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0001F6F0 File Offset: 0x0001D8F0
		private void Target_Validate(object sender, CancelEventArgs e)
		{
			try
			{
				if (this.PullData(true))
				{
					e.Cancel = true;
				}
			}
			catch
			{
				e.Cancel = true;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x0001F72C File Offset: 0x0001D92C
		internal bool IsBindable
		{
			get
			{
				return this.control != null && this.propertyName.Length > 0 && this.bindToObject.DataSource != null && this.bindingManagerBase != null;
			}
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0001F75C File Offset: 0x0001D95C
		internal void UpdateIsBinding()
		{
			bool flag = this.IsBindable && this.ComponentCreated && this.bindingManagerBase.IsBinding;
			if (this.bound != flag)
			{
				this.bound = flag;
				this.BindTarget(flag);
				if (this.bound)
				{
					if (this.controlUpdateMode == ControlUpdateMode.Never)
					{
						this.PullData(false, true);
						return;
					}
					this.PushData();
				}
			}
		}

		// Token: 0x0400069F RID: 1695
		private IBindableComponent control;

		// Token: 0x040006A0 RID: 1696
		private BindingManagerBase bindingManagerBase;

		// Token: 0x040006A1 RID: 1697
		private BindToObject bindToObject;

		// Token: 0x040006A2 RID: 1698
		private string propertyName;

		// Token: 0x040006A3 RID: 1699
		private PropertyDescriptor propInfo;

		// Token: 0x040006A4 RID: 1700
		private PropertyDescriptor propIsNullInfo;

		// Token: 0x040006A5 RID: 1701
		private EventDescriptor validateInfo;

		// Token: 0x040006A6 RID: 1702
		private TypeConverter propInfoConverter;

		// Token: 0x040006A7 RID: 1703
		private bool formattingEnabled;

		// Token: 0x040006A8 RID: 1704
		private bool bound;

		// Token: 0x040006A9 RID: 1705
		private bool modified;

		// Token: 0x040006AA RID: 1706
		private bool inSetPropValue;

		// Token: 0x040006AB RID: 1707
		private bool inPushOrPull;

		// Token: 0x040006AC RID: 1708
		private bool inOnBindingComplete;

		// Token: 0x040006AD RID: 1709
		private string formatString;

		// Token: 0x040006AE RID: 1710
		private IFormatProvider formatInfo;

		// Token: 0x040006AF RID: 1711
		private object nullValue;

		// Token: 0x040006B0 RID: 1712
		private object dsNullValue;

		// Token: 0x040006B1 RID: 1713
		private bool dsNullValueSet;

		// Token: 0x040006B2 RID: 1714
		private ConvertEventHandler onParse;

		// Token: 0x040006B3 RID: 1715
		private ConvertEventHandler onFormat;

		// Token: 0x040006B4 RID: 1716
		private ControlUpdateMode controlUpdateMode;

		// Token: 0x040006B5 RID: 1717
		private DataSourceUpdateMode dataSourceUpdateMode;

		// Token: 0x040006B6 RID: 1718
		private BindingCompleteEventHandler onComplete;
	}
}
