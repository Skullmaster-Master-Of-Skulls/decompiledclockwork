using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020002CE RID: 718
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[LookupBindingProperties("DataSource", "DisplayMember", "ValueMember", "SelectedValue")]
	public abstract class ListControl : Control
	{
		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06002C6E RID: 11374 RVA: 0x000C7C16 File Offset: 0x000C5E16
		// (set) Token: 0x06002C6F RID: 11375 RVA: 0x000C7C20 File Offset: 0x000C5E20
		[SRCategory("CatData")]
		[DefaultValue(null)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[AttributeProvider(typeof(IListSource))]
		[SRDescription("ListControlDataSourceDescr")]
		public object DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				if (value != null && !(value is IList) && !(value is IListSource))
				{
					throw new ArgumentException(SR.GetString("BadDataSourceForComplexBinding"));
				}
				if (this.dataSource == value)
				{
					return;
				}
				try
				{
					this.SetDataConnection(value, this.displayMember, false);
				}
				catch
				{
					this.DisplayMember = "";
				}
				if (value == null)
				{
					this.DisplayMember = "";
				}
			}
		}

		// Token: 0x14000200 RID: 512
		// (add) Token: 0x06002C70 RID: 11376 RVA: 0x000C7C98 File Offset: 0x000C5E98
		// (remove) Token: 0x06002C71 RID: 11377 RVA: 0x000C7CAB File Offset: 0x000C5EAB
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ListControlOnDataSourceChangedDescr")]
		public event EventHandler DataSourceChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.EVENT_DATASOURCECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.EVENT_DATASOURCECHANGED, value);
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06002C72 RID: 11378 RVA: 0x000C7CBE File Offset: 0x000C5EBE
		protected CurrencyManager DataManager
		{
			get
			{
				return this.dataManager;
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06002C73 RID: 11379 RVA: 0x000C7CC6 File Offset: 0x000C5EC6
		// (set) Token: 0x06002C74 RID: 11380 RVA: 0x000C7CD4 File Offset: 0x000C5ED4
		[SRCategory("CatData")]
		[DefaultValue("")]
		[TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRDescription("ListControlDisplayMemberDescr")]
		public string DisplayMember
		{
			get
			{
				return this.displayMember.BindingMember;
			}
			set
			{
				BindingMemberInfo bindingMemberInfo = this.displayMember;
				try
				{
					this.SetDataConnection(this.dataSource, new BindingMemberInfo(value), false);
				}
				catch
				{
					this.displayMember = bindingMemberInfo;
				}
			}
		}

		// Token: 0x14000201 RID: 513
		// (add) Token: 0x06002C75 RID: 11381 RVA: 0x000C7D18 File Offset: 0x000C5F18
		// (remove) Token: 0x06002C76 RID: 11382 RVA: 0x000C7D2B File Offset: 0x000C5F2B
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ListControlOnDisplayMemberChangedDescr")]
		public event EventHandler DisplayMemberChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.EVENT_DISPLAYMEMBERCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.EVENT_DISPLAYMEMBERCHANGED, value);
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06002C77 RID: 11383 RVA: 0x000C7D40 File Offset: 0x000C5F40
		private TypeConverter DisplayMemberConverter
		{
			get
			{
				if (this.displayMemberConverter == null && this.DataManager != null)
				{
					BindingMemberInfo bindingMemberInfo = this.displayMember;
					PropertyDescriptorCollection itemProperties = this.DataManager.GetItemProperties();
					if (itemProperties != null)
					{
						PropertyDescriptor propertyDescriptor = itemProperties.Find(this.displayMember.BindingField, true);
						if (propertyDescriptor != null)
						{
							this.displayMemberConverter = propertyDescriptor.Converter;
						}
					}
				}
				return this.displayMemberConverter;
			}
		}

		// Token: 0x14000202 RID: 514
		// (add) Token: 0x06002C78 RID: 11384 RVA: 0x000C7D9B File Offset: 0x000C5F9B
		// (remove) Token: 0x06002C79 RID: 11385 RVA: 0x000C7DB4 File Offset: 0x000C5FB4
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ListControlFormatDescr")]
		public event ListControlConvertEventHandler Format
		{
			add
			{
				base.Events.AddHandler(ListControl.EVENT_FORMAT, value);
				this.RefreshItems();
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.EVENT_FORMAT, value);
				this.RefreshItems();
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06002C7A RID: 11386 RVA: 0x000C7DCD File Offset: 0x000C5FCD
		// (set) Token: 0x06002C7B RID: 11387 RVA: 0x000C7DD5 File Offset: 0x000C5FD5
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DefaultValue(null)]
		public IFormatProvider FormatInfo
		{
			get
			{
				return this.formatInfo;
			}
			set
			{
				if (value != this.formatInfo)
				{
					this.formatInfo = value;
					this.RefreshItems();
					this.OnFormatInfoChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000203 RID: 515
		// (add) Token: 0x06002C7C RID: 11388 RVA: 0x000C7DF8 File Offset: 0x000C5FF8
		// (remove) Token: 0x06002C7D RID: 11389 RVA: 0x000C7E0B File Offset: 0x000C600B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ListControlFormatInfoChangedDescr")]
		public event EventHandler FormatInfoChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.EVENT_FORMATINFOCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.EVENT_FORMATINFOCHANGED, value);
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06002C7E RID: 11390 RVA: 0x000C7E1E File Offset: 0x000C601E
		// (set) Token: 0x06002C7F RID: 11391 RVA: 0x000C7E26 File Offset: 0x000C6026
		[DefaultValue("")]
		[SRDescription("ListControlFormatStringDescr")]
		[Editor("System.Windows.Forms.Design.FormatStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
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
					this.RefreshItems();
					this.OnFormatStringChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000204 RID: 516
		// (add) Token: 0x06002C80 RID: 11392 RVA: 0x000C7E58 File Offset: 0x000C6058
		// (remove) Token: 0x06002C81 RID: 11393 RVA: 0x000C7E6B File Offset: 0x000C606B
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ListControlFormatStringChangedDescr")]
		public event EventHandler FormatStringChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.EVENT_FORMATSTRINGCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.EVENT_FORMATSTRINGCHANGED, value);
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06002C82 RID: 11394 RVA: 0x000C7E7E File Offset: 0x000C607E
		// (set) Token: 0x06002C83 RID: 11395 RVA: 0x000C7E86 File Offset: 0x000C6086
		[DefaultValue(false)]
		[SRDescription("ListControlFormattingEnabledDescr")]
		public bool FormattingEnabled
		{
			get
			{
				return this.formattingEnabled;
			}
			set
			{
				if (value != this.formattingEnabled)
				{
					this.formattingEnabled = value;
					this.RefreshItems();
					this.OnFormattingEnabledChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000205 RID: 517
		// (add) Token: 0x06002C84 RID: 11396 RVA: 0x000C7EA9 File Offset: 0x000C60A9
		// (remove) Token: 0x06002C85 RID: 11397 RVA: 0x000C7EBC File Offset: 0x000C60BC
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ListControlFormattingEnabledChangedDescr")]
		public event EventHandler FormattingEnabledChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.EVENT_FORMATTINGENABLEDCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.EVENT_FORMATTINGENABLEDCHANGED, value);
			}
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x000C7ED0 File Offset: 0x000C60D0
		private bool BindingMemberInfoInDataManager(BindingMemberInfo bindingMemberInfo)
		{
			if (this.dataManager == null)
			{
				return false;
			}
			PropertyDescriptorCollection itemProperties = this.dataManager.GetItemProperties();
			int count = itemProperties.Count;
			for (int i = 0; i < count; i++)
			{
				if (!typeof(IList).IsAssignableFrom(itemProperties[i].PropertyType) && itemProperties[i].Name.Equals(bindingMemberInfo.BindingField))
				{
					return true;
				}
			}
			for (int j = 0; j < count; j++)
			{
				if (!typeof(IList).IsAssignableFrom(itemProperties[j].PropertyType) && string.Compare(itemProperties[j].Name, bindingMemberInfo.BindingField, true, CultureInfo.CurrentCulture) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06002C87 RID: 11399 RVA: 0x000C7F8B File Offset: 0x000C618B
		// (set) Token: 0x06002C88 RID: 11400 RVA: 0x000C7F98 File Offset: 0x000C6198
		[SRCategory("CatData")]
		[DefaultValue("")]
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRDescription("ListControlValueMemberDescr")]
		public string ValueMember
		{
			get
			{
				return this.valueMember.BindingMember;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				BindingMemberInfo bindingMemberInfo = new BindingMemberInfo(value);
				BindingMemberInfo bindingMemberInfo2 = this.valueMember;
				if (!bindingMemberInfo.Equals(this.valueMember))
				{
					if (this.DisplayMember.Length == 0)
					{
						this.SetDataConnection(this.DataSource, bindingMemberInfo, false);
					}
					if (this.dataManager != null && value != null && value.Length != 0 && !this.BindingMemberInfoInDataManager(bindingMemberInfo))
					{
						throw new ArgumentException(SR.GetString("ListControlWrongValueMember"), "value");
					}
					this.valueMember = bindingMemberInfo;
					this.OnValueMemberChanged(EventArgs.Empty);
					this.OnSelectedValueChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000206 RID: 518
		// (add) Token: 0x06002C89 RID: 11401 RVA: 0x000C8041 File Offset: 0x000C6241
		// (remove) Token: 0x06002C8A RID: 11402 RVA: 0x000C8054 File Offset: 0x000C6254
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ListControlOnValueMemberChangedDescr")]
		public event EventHandler ValueMemberChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.EVENT_VALUEMEMBERCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.EVENT_VALUEMEMBERCHANGED, value);
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06002C8B RID: 11403 RVA: 0x00013062 File Offset: 0x00011262
		protected virtual bool AllowSelection
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06002C8C RID: 11404
		// (set) Token: 0x06002C8D RID: 11405
		public abstract int SelectedIndex { get; set; }

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06002C8E RID: 11406 RVA: 0x000C8068 File Offset: 0x000C6268
		// (set) Token: 0x06002C8F RID: 11407 RVA: 0x000C80B0 File Offset: 0x000C62B0
		[SRCategory("CatData")]
		[DefaultValue(null)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListControlSelectedValueDescr")]
		[Bindable(true)]
		public object SelectedValue
		{
			get
			{
				if (this.SelectedIndex != -1 && this.dataManager != null)
				{
					object item = this.dataManager[this.SelectedIndex];
					return this.FilterItemOnProperty(item, this.valueMember.BindingField);
				}
				return null;
			}
			set
			{
				if (this.dataManager != null)
				{
					string bindingField = this.valueMember.BindingField;
					if (string.IsNullOrEmpty(bindingField))
					{
						throw new InvalidOperationException(SR.GetString("ListControlEmptyValueMemberInSettingSelectedValue"));
					}
					PropertyDescriptorCollection itemProperties = this.dataManager.GetItemProperties();
					PropertyDescriptor property = itemProperties.Find(bindingField, true);
					int selectedIndex = this.dataManager.Find(property, value, true);
					this.SelectedIndex = selectedIndex;
				}
			}
		}

		// Token: 0x14000207 RID: 519
		// (add) Token: 0x06002C90 RID: 11408 RVA: 0x000C8114 File Offset: 0x000C6314
		// (remove) Token: 0x06002C91 RID: 11409 RVA: 0x000C8127 File Offset: 0x000C6327
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ListControlOnSelectedValueChangedDescr")]
		public event EventHandler SelectedValueChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.EVENT_SELECTEDVALUECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.EVENT_SELECTEDVALUECHANGED, value);
			}
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x000C813A File Offset: 0x000C633A
		private void DataManager_PositionChanged(object sender, EventArgs e)
		{
			if (this.dataManager != null && this.AllowSelection)
			{
				this.SelectedIndex = this.dataManager.Position;
			}
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x000C8160 File Offset: 0x000C6360
		private void DataManager_ItemChanged(object sender, ItemChangedEventArgs e)
		{
			if (this.dataManager != null)
			{
				if (e.Index == -1)
				{
					this.SetItemsCore(this.dataManager.List);
					if (this.AllowSelection)
					{
						this.SelectedIndex = this.dataManager.Position;
						return;
					}
				}
				else
				{
					this.SetItemCore(e.Index, this.dataManager[e.Index]);
				}
			}
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x000C81C6 File Offset: 0x000C63C6
		protected object FilterItemOnProperty(object item)
		{
			return this.FilterItemOnProperty(item, this.displayMember.BindingField);
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x000C81DC File Offset: 0x000C63DC
		protected object FilterItemOnProperty(object item, string field)
		{
			if (item != null && field.Length > 0)
			{
				try
				{
					PropertyDescriptor propertyDescriptor;
					if (this.dataManager != null)
					{
						propertyDescriptor = this.dataManager.GetItemProperties().Find(field, true);
					}
					else
					{
						propertyDescriptor = TypeDescriptor.GetProperties(item).Find(field, true);
					}
					if (propertyDescriptor != null)
					{
						item = propertyDescriptor.GetValue(item);
					}
				}
				catch
				{
				}
			}
			return item;
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06002C96 RID: 11414 RVA: 0x000C8244 File Offset: 0x000C6444
		internal bool BindingFieldEmpty
		{
			get
			{
				return this.displayMember.BindingField.Length <= 0;
			}
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x000C825C File Offset: 0x000C645C
		internal int FindStringInternal(string str, IList items, int startIndex, bool exact)
		{
			return this.FindStringInternal(str, items, startIndex, exact, true);
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x000C826C File Offset: 0x000C646C
		internal int FindStringInternal(string str, IList items, int startIndex, bool exact, bool ignorecase)
		{
			if (str == null || items == null)
			{
				return -1;
			}
			if (startIndex < -1 || startIndex >= items.Count)
			{
				return -1;
			}
			int length = str.Length;
			int i = 0;
			int num = (startIndex + 1) % items.Count;
			while (i < items.Count)
			{
				i++;
				bool flag;
				if (exact)
				{
					flag = (string.Compare(str, this.GetItemText(items[num]), ignorecase, CultureInfo.CurrentCulture) == 0);
				}
				else
				{
					flag = (string.Compare(str, 0, this.GetItemText(items[num]), 0, length, ignorecase, CultureInfo.CurrentCulture) == 0);
				}
				if (flag)
				{
					return num;
				}
				num = (num + 1) % items.Count;
			}
			return -1;
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x000C830C File Offset: 0x000C650C
		public string GetItemText(object item)
		{
			if (!this.formattingEnabled)
			{
				if (item == null)
				{
					return string.Empty;
				}
				item = this.FilterItemOnProperty(item, this.displayMember.BindingField);
				if (item == null)
				{
					return "";
				}
				return Convert.ToString(item, CultureInfo.CurrentCulture);
			}
			else
			{
				object obj = this.FilterItemOnProperty(item, this.displayMember.BindingField);
				ListControlConvertEventArgs listControlConvertEventArgs = new ListControlConvertEventArgs(obj, typeof(string), item);
				this.OnFormat(listControlConvertEventArgs);
				if (listControlConvertEventArgs.Value != item && listControlConvertEventArgs.Value is string)
				{
					return (string)listControlConvertEventArgs.Value;
				}
				if (ListControl.stringTypeConverter == null)
				{
					ListControl.stringTypeConverter = TypeDescriptor.GetConverter(typeof(string));
				}
				string result;
				try
				{
					result = (string)Formatter.FormatObject(obj, typeof(string), this.DisplayMemberConverter, ListControl.stringTypeConverter, this.formatString, this.formatInfo, null, DBNull.Value);
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
					result = ((obj != null) ? Convert.ToString(item, CultureInfo.CurrentCulture) : "");
				}
				return result;
			}
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x000C8428 File Offset: 0x000C6628
		protected override bool IsInputKey(Keys keyData)
		{
			if ((keyData & Keys.Alt) == Keys.Alt)
			{
				return false;
			}
			Keys keys = keyData & Keys.KeyCode;
			return keys - Keys.Prior <= 3 || base.IsInputKey(keyData);
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x000C845D File Offset: 0x000C665D
		protected override void OnBindingContextChanged(EventArgs e)
		{
			this.SetDataConnection(this.dataSource, this.displayMember, true);
			base.OnBindingContextChanged(e);
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x000C847C File Offset: 0x000C667C
		protected virtual void OnDataSourceChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[ListControl.EVENT_DATASOURCECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x000C84AC File Offset: 0x000C66AC
		protected virtual void OnDisplayMemberChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[ListControl.EVENT_DISPLAYMEMBERCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x000C84DC File Offset: 0x000C66DC
		protected virtual void OnFormat(ListControlConvertEventArgs e)
		{
			ListControlConvertEventHandler listControlConvertEventHandler = base.Events[ListControl.EVENT_FORMAT] as ListControlConvertEventHandler;
			if (listControlConvertEventHandler != null)
			{
				listControlConvertEventHandler(this, e);
			}
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x000C850C File Offset: 0x000C670C
		protected virtual void OnFormatInfoChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[ListControl.EVENT_FORMATINFOCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x000C853C File Offset: 0x000C673C
		protected virtual void OnFormatStringChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[ListControl.EVENT_FORMATSTRINGCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x000C856C File Offset: 0x000C676C
		protected virtual void OnFormattingEnabledChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[ListControl.EVENT_FORMATTINGENABLEDCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x000C859A File Offset: 0x000C679A
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			this.OnSelectedValueChanged(EventArgs.Empty);
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x000C85A8 File Offset: 0x000C67A8
		protected virtual void OnValueMemberChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[ListControl.EVENT_VALUEMEMBERCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000C85D8 File Offset: 0x000C67D8
		protected virtual void OnSelectedValueChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[ListControl.EVENT_SELECTEDVALUECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002CA5 RID: 11429
		protected abstract void RefreshItem(int index);

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void RefreshItems()
		{
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x000C8606 File Offset: 0x000C6806
		private void DataSourceDisposed(object sender, EventArgs e)
		{
			this.SetDataConnection(null, new BindingMemberInfo(""), true);
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x000C861C File Offset: 0x000C681C
		private void DataSourceInitialized(object sender, EventArgs e)
		{
			ISupportInitializeNotification supportInitializeNotification = this.dataSource as ISupportInitializeNotification;
			this.SetDataConnection(this.dataSource, this.displayMember, true);
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x000C8648 File Offset: 0x000C6848
		private void SetDataConnection(object newDataSource, BindingMemberInfo newDisplayMember, bool force)
		{
			bool flag = this.dataSource != newDataSource;
			bool flag2 = !this.displayMember.Equals(newDisplayMember);
			if (this.inSetDataConnection)
			{
				return;
			}
			try
			{
				if (force || flag || flag2)
				{
					this.inSetDataConnection = true;
					IList list = (this.DataManager != null) ? this.DataManager.List : null;
					bool flag3 = this.DataManager == null;
					this.UnwireDataSource();
					this.dataSource = newDataSource;
					this.displayMember = newDisplayMember;
					this.WireDataSource();
					if (this.isDataSourceInitialized)
					{
						CurrencyManager currencyManager = null;
						if (newDataSource != null && this.BindingContext != null && newDataSource != Convert.DBNull)
						{
							currencyManager = (CurrencyManager)this.BindingContext[newDataSource, newDisplayMember.BindingPath];
						}
						if (this.dataManager != currencyManager)
						{
							if (this.dataManager != null)
							{
								this.dataManager.ItemChanged -= this.DataManager_ItemChanged;
								this.dataManager.PositionChanged -= this.DataManager_PositionChanged;
							}
							this.dataManager = currencyManager;
							if (this.dataManager != null)
							{
								this.dataManager.ItemChanged += this.DataManager_ItemChanged;
								this.dataManager.PositionChanged += this.DataManager_PositionChanged;
							}
						}
						if (this.dataManager != null && (flag2 || flag) && this.displayMember.BindingMember != null && this.displayMember.BindingMember.Length != 0 && !this.BindingMemberInfoInDataManager(this.displayMember))
						{
							throw new ArgumentException(SR.GetString("ListControlWrongDisplayMember"), "newDisplayMember");
						}
						if (this.dataManager != null && (flag || flag2 || force) && (flag2 || (force && (list != this.dataManager.List || flag3))))
						{
							this.DataManager_ItemChanged(this.dataManager, new ItemChangedEventArgs(-1));
						}
					}
					this.displayMemberConverter = null;
				}
				if (flag)
				{
					this.OnDataSourceChanged(EventArgs.Empty);
				}
				if (flag2)
				{
					this.OnDisplayMemberChanged(EventArgs.Empty);
				}
			}
			finally
			{
				this.inSetDataConnection = false;
			}
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x000C8860 File Offset: 0x000C6A60
		private void UnwireDataSource()
		{
			if (this.dataSource is IComponent)
			{
				((IComponent)this.dataSource).Disposed -= this.DataSourceDisposed;
			}
			ISupportInitializeNotification supportInitializeNotification = this.dataSource as ISupportInitializeNotification;
			if (supportInitializeNotification != null && this.isDataSourceInitEventHooked)
			{
				supportInitializeNotification.Initialized -= this.DataSourceInitialized;
				this.isDataSourceInitEventHooked = false;
			}
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x000C88C8 File Offset: 0x000C6AC8
		private void WireDataSource()
		{
			if (this.dataSource is IComponent)
			{
				((IComponent)this.dataSource).Disposed += this.DataSourceDisposed;
			}
			ISupportInitializeNotification supportInitializeNotification = this.dataSource as ISupportInitializeNotification;
			if (supportInitializeNotification != null && !supportInitializeNotification.IsInitialized)
			{
				supportInitializeNotification.Initialized += this.DataSourceInitialized;
				this.isDataSourceInitEventHooked = true;
				this.isDataSourceInitialized = false;
				return;
			}
			this.isDataSourceInitialized = true;
		}

		// Token: 0x06002CAC RID: 11436
		protected abstract void SetItemsCore(IList items);

		// Token: 0x06002CAD RID: 11437 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void SetItemCore(int index, object value)
		{
		}

		// Token: 0x04001286 RID: 4742
		private static readonly object EVENT_DATASOURCECHANGED = new object();

		// Token: 0x04001287 RID: 4743
		private static readonly object EVENT_DISPLAYMEMBERCHANGED = new object();

		// Token: 0x04001288 RID: 4744
		private static readonly object EVENT_VALUEMEMBERCHANGED = new object();

		// Token: 0x04001289 RID: 4745
		private static readonly object EVENT_SELECTEDVALUECHANGED = new object();

		// Token: 0x0400128A RID: 4746
		private static readonly object EVENT_FORMATINFOCHANGED = new object();

		// Token: 0x0400128B RID: 4747
		private static readonly object EVENT_FORMATSTRINGCHANGED = new object();

		// Token: 0x0400128C RID: 4748
		private static readonly object EVENT_FORMATTINGENABLEDCHANGED = new object();

		// Token: 0x0400128D RID: 4749
		private object dataSource;

		// Token: 0x0400128E RID: 4750
		private CurrencyManager dataManager;

		// Token: 0x0400128F RID: 4751
		private BindingMemberInfo displayMember;

		// Token: 0x04001290 RID: 4752
		private BindingMemberInfo valueMember;

		// Token: 0x04001291 RID: 4753
		private string formatString = string.Empty;

		// Token: 0x04001292 RID: 4754
		private IFormatProvider formatInfo;

		// Token: 0x04001293 RID: 4755
		private bool formattingEnabled;

		// Token: 0x04001294 RID: 4756
		private static readonly object EVENT_FORMAT = new object();

		// Token: 0x04001295 RID: 4757
		private TypeConverter displayMemberConverter;

		// Token: 0x04001296 RID: 4758
		private static TypeConverter stringTypeConverter = null;

		// Token: 0x04001297 RID: 4759
		private bool isDataSourceInitialized;

		// Token: 0x04001298 RID: 4760
		private bool isDataSourceInitEventHooked;

		// Token: 0x04001299 RID: 4761
		private bool inSetDataConnection;
	}
}
