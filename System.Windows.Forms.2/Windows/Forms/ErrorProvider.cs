using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Internal;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Internal;

namespace System.Windows.Forms
{
	// Token: 0x0200024D RID: 589
	[ProvideProperty("IconPadding", typeof(Control))]
	[ProvideProperty("IconAlignment", typeof(Control))]
	[ProvideProperty("Error", typeof(Control))]
	[ToolboxItemFilter("System.Windows.Forms")]
	[ComplexBindingProperties("DataSource", "DataMember")]
	[SRDescription("DescriptionErrorProvider")]
	public class ErrorProvider : Component, IExtenderProvider, ISupportInitialize
	{
		// Token: 0x0600251B RID: 9499 RVA: 0x000AD80C File Offset: 0x000ABA0C
		public ErrorProvider()
		{
			this.icon = ErrorProvider.DefaultIcon;
			this.blinkRate = 250;
			this.blinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;
			this.currentChanged = new EventHandler(this.ErrorManager_CurrentChanged);
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x000AD876 File Offset: 0x000ABA76
		public ErrorProvider(ContainerControl parentControl) : this()
		{
			this.parentControl = parentControl;
			this.propChangedEvent = new EventHandler(this.ParentControl_BindingContextChanged);
			parentControl.BindingContextChanged += this.propChangedEvent;
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x000AD8A3 File Offset: 0x000ABAA3
		public ErrorProvider(IContainer container) : this()
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			container.Add(this);
		}

		// Token: 0x17000889 RID: 2185
		// (set) Token: 0x0600251E RID: 9502 RVA: 0x000AD8C0 File Offset: 0x000ABAC0
		public override ISite Site
		{
			set
			{
				base.Site = value;
				if (value == null)
				{
					return;
				}
				IDesignerHost designerHost = value.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost != null)
				{
					IComponent rootComponent = designerHost.RootComponent;
					if (rootComponent is ContainerControl)
					{
						this.ContainerControl = (ContainerControl)rootComponent;
					}
				}
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x0600251F RID: 9503 RVA: 0x000AD90C File Offset: 0x000ABB0C
		// (set) Token: 0x06002520 RID: 9504 RVA: 0x000AD920 File Offset: 0x000ABB20
		[SRCategory("CatBehavior")]
		[DefaultValue(ErrorBlinkStyle.BlinkIfDifferentError)]
		[SRDescription("ErrorProviderBlinkStyleDescr")]
		public ErrorBlinkStyle BlinkStyle
		{
			get
			{
				if (this.blinkRate == 0)
				{
					return ErrorBlinkStyle.NeverBlink;
				}
				return this.blinkStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ErrorBlinkStyle));
				}
				if (this.blinkRate == 0)
				{
					value = ErrorBlinkStyle.NeverBlink;
				}
				if (this.blinkStyle == value)
				{
					return;
				}
				if (value == ErrorBlinkStyle.AlwaysBlink)
				{
					this.showIcon = true;
					this.blinkStyle = ErrorBlinkStyle.AlwaysBlink;
					using (IEnumerator enumerator = this.windows.Values.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							ErrorProvider.ErrorWindow errorWindow = (ErrorProvider.ErrorWindow)obj;
							errorWindow.StartBlinking();
						}
						return;
					}
				}
				if (this.blinkStyle == ErrorBlinkStyle.AlwaysBlink)
				{
					this.blinkStyle = value;
					using (IEnumerator enumerator2 = this.windows.Values.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							ErrorProvider.ErrorWindow errorWindow2 = (ErrorProvider.ErrorWindow)obj2;
							errorWindow2.StopBlinking();
						}
						return;
					}
				}
				this.blinkStyle = value;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06002521 RID: 9505 RVA: 0x000ADA30 File Offset: 0x000ABC30
		// (set) Token: 0x06002522 RID: 9506 RVA: 0x000ADA38 File Offset: 0x000ABC38
		[DefaultValue(null)]
		[SRCategory("CatData")]
		[SRDescription("ErrorProviderContainerControlDescr")]
		public ContainerControl ContainerControl
		{
			[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
			[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
			get
			{
				return this.parentControl;
			}
			set
			{
				if (this.parentControl != value)
				{
					if (this.parentControl != null)
					{
						this.parentControl.BindingContextChanged -= this.propChangedEvent;
					}
					this.parentControl = value;
					if (this.parentControl != null)
					{
						this.parentControl.BindingContextChanged += this.propChangedEvent;
					}
					this.Set_ErrorManager(this.DataSource, this.DataMember, true);
				}
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06002523 RID: 9507 RVA: 0x000ADA9A File Offset: 0x000ABC9A
		// (set) Token: 0x06002524 RID: 9508 RVA: 0x000ADAA2 File Offset: 0x000ABCA2
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("ControlRightToLeftDescr")]
		public virtual bool RightToLeft
		{
			get
			{
				return this.rightToLeft;
			}
			set
			{
				if (value != this.rightToLeft)
				{
					this.rightToLeft = value;
					this.OnRightToLeftChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000199 RID: 409
		// (add) Token: 0x06002525 RID: 9509 RVA: 0x000ADABF File Offset: 0x000ABCBF
		// (remove) Token: 0x06002526 RID: 9510 RVA: 0x000ADAD8 File Offset: 0x000ABCD8
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnRightToLeftChangedDescr")]
		public event EventHandler RightToLeftChanged
		{
			add
			{
				this.onRightToLeftChanged = (EventHandler)Delegate.Combine(this.onRightToLeftChanged, value);
			}
			remove
			{
				this.onRightToLeftChanged = (EventHandler)Delegate.Remove(this.onRightToLeftChanged, value);
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06002527 RID: 9511 RVA: 0x000ADAF1 File Offset: 0x000ABCF1
		// (set) Token: 0x06002528 RID: 9512 RVA: 0x000ADAF9 File Offset: 0x000ABCF9
		[SRCategory("CatData")]
		[Localizable(false)]
		[Bindable(true)]
		[SRDescription("ControlTagDescr")]
		[DefaultValue(null)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.userData;
			}
			set
			{
				this.userData = value;
			}
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x000ADB04 File Offset: 0x000ABD04
		private void Set_ErrorManager(object newDataSource, string newDataMember, bool force)
		{
			if (this.inSetErrorManager)
			{
				return;
			}
			this.inSetErrorManager = true;
			try
			{
				bool flag = this.DataSource != newDataSource;
				bool flag2 = this.DataMember != newDataMember;
				if (flag || flag2 || force)
				{
					this.dataSource = newDataSource;
					this.dataMember = newDataMember;
					if (this.initializing)
					{
						this.setErrorManagerOnEndInit = true;
					}
					else
					{
						this.UnwireEvents(this.errorManager);
						if (this.parentControl != null && this.dataSource != null && this.parentControl.BindingContext != null)
						{
							this.errorManager = this.parentControl.BindingContext[this.dataSource, this.dataMember];
						}
						else
						{
							this.errorManager = null;
						}
						this.WireEvents(this.errorManager);
						if (this.errorManager != null)
						{
							this.UpdateBinding();
						}
					}
				}
			}
			finally
			{
				this.inSetErrorManager = false;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x0600252A RID: 9514 RVA: 0x000ADBF0 File Offset: 0x000ABDF0
		// (set) Token: 0x0600252B RID: 9515 RVA: 0x000ADBF8 File Offset: 0x000ABDF8
		[DefaultValue(null)]
		[SRCategory("CatData")]
		[AttributeProvider(typeof(IListSource))]
		[SRDescription("ErrorProviderDataSourceDescr")]
		public object DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				if (this.parentControl != null && value != null && !string.IsNullOrEmpty(this.dataMember))
				{
					try
					{
						this.errorManager = this.parentControl.BindingContext[value, this.dataMember];
					}
					catch (ArgumentException)
					{
						this.dataMember = "";
					}
				}
				this.Set_ErrorManager(value, this.DataMember, false);
			}
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x000ADC68 File Offset: 0x000ABE68
		private bool ShouldSerializeDataSource()
		{
			return this.dataSource != null;
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x0600252D RID: 9517 RVA: 0x000ADC73 File Offset: 0x000ABE73
		// (set) Token: 0x0600252E RID: 9518 RVA: 0x000ADC7B File Offset: 0x000ABE7B
		[DefaultValue(null)]
		[SRCategory("CatData")]
		[Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRDescription("ErrorProviderDataMemberDescr")]
		public string DataMember
		{
			get
			{
				return this.dataMember;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				this.Set_ErrorManager(this.DataSource, value, false);
			}
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x000ADC95 File Offset: 0x000ABE95
		private bool ShouldSerializeDataMember()
		{
			return this.dataMember != null && this.dataMember.Length != 0;
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x000ADCAF File Offset: 0x000ABEAF
		public void BindToDataAndErrors(object newDataSource, string newDataMember)
		{
			this.Set_ErrorManager(newDataSource, newDataMember, false);
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x000ADCBC File Offset: 0x000ABEBC
		private void WireEvents(BindingManagerBase listManager)
		{
			if (listManager != null)
			{
				listManager.CurrentChanged += this.currentChanged;
				listManager.BindingComplete += this.ErrorManager_BindingComplete;
				CurrencyManager currencyManager = listManager as CurrencyManager;
				if (currencyManager != null)
				{
					currencyManager.ItemChanged += this.ErrorManager_ItemChanged;
					currencyManager.Bindings.CollectionChanged += this.ErrorManager_BindingsChanged;
				}
			}
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x000ADD20 File Offset: 0x000ABF20
		private void UnwireEvents(BindingManagerBase listManager)
		{
			if (listManager != null)
			{
				listManager.CurrentChanged -= this.currentChanged;
				listManager.BindingComplete -= this.ErrorManager_BindingComplete;
				CurrencyManager currencyManager = listManager as CurrencyManager;
				if (currencyManager != null)
				{
					currencyManager.ItemChanged -= this.ErrorManager_ItemChanged;
					currencyManager.Bindings.CollectionChanged -= this.ErrorManager_BindingsChanged;
				}
			}
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x000ADD84 File Offset: 0x000ABF84
		private void ErrorManager_BindingComplete(object sender, BindingCompleteEventArgs e)
		{
			Binding binding = e.Binding;
			if (binding != null && binding.Control != null)
			{
				this.SetError(binding.Control, (e.ErrorText == null) ? string.Empty : e.ErrorText);
			}
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x000ADDC4 File Offset: 0x000ABFC4
		private void ErrorManager_BindingsChanged(object sender, CollectionChangeEventArgs e)
		{
			this.ErrorManager_CurrentChanged(this.errorManager, e);
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x000ADDD3 File Offset: 0x000ABFD3
		private void ParentControl_BindingContextChanged(object sender, EventArgs e)
		{
			this.Set_ErrorManager(this.DataSource, this.DataMember, true);
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x000ADDE8 File Offset: 0x000ABFE8
		public void UpdateBinding()
		{
			this.ErrorManager_CurrentChanged(this.errorManager, EventArgs.Empty);
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x000ADDFC File Offset: 0x000ABFFC
		private void ErrorManager_ItemChanged(object sender, ItemChangedEventArgs e)
		{
			BindingsCollection bindings = this.errorManager.Bindings;
			int count = bindings.Count;
			if (e.Index == -1 && this.errorManager.Count == 0)
			{
				for (int i = 0; i < count; i++)
				{
					if (bindings[i].Control != null)
					{
						this.SetError(bindings[i].Control, "");
					}
				}
				return;
			}
			this.ErrorManager_CurrentChanged(sender, e);
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x000ADE6C File Offset: 0x000AC06C
		private void ErrorManager_CurrentChanged(object sender, EventArgs e)
		{
			if (this.errorManager.Count == 0)
			{
				return;
			}
			object obj = this.errorManager.Current;
			if (!(obj is IDataErrorInfo))
			{
				return;
			}
			BindingsCollection bindings = this.errorManager.Bindings;
			int count = bindings.Count;
			foreach (object obj2 in this.items.Values)
			{
				ErrorProvider.ControlItem controlItem = (ErrorProvider.ControlItem)obj2;
				controlItem.BlinkPhase = 0;
			}
			Hashtable hashtable = new Hashtable(count);
			for (int i = 0; i < count; i++)
			{
				if (bindings[i].Control != null)
				{
					BindToObject bindToObject = bindings[i].BindToObject;
					string text = ((IDataErrorInfo)obj)[bindToObject.BindingMemberInfo.BindingField];
					if (text == null)
					{
						text = "";
					}
					string text2 = "";
					if (hashtable.Contains(bindings[i].Control))
					{
						text2 = (string)hashtable[bindings[i].Control];
					}
					if (string.IsNullOrEmpty(text2))
					{
						text2 = text;
					}
					else
					{
						text2 = text2 + "\r\n" + text;
					}
					hashtable[bindings[i].Control] = text2;
				}
			}
			foreach (object obj3 in hashtable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
				this.SetError((Control)dictionaryEntry.Key, (string)dictionaryEntry.Value);
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06002539 RID: 9529 RVA: 0x000AE018 File Offset: 0x000AC218
		// (set) Token: 0x0600253A RID: 9530 RVA: 0x000AE020 File Offset: 0x000AC220
		[SRCategory("CatBehavior")]
		[DefaultValue(250)]
		[SRDescription("ErrorProviderBlinkRateDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public int BlinkRate
		{
			get
			{
				return this.blinkRate;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("BlinkRate", value, SR.GetString("BlinkRateMustBeZeroOrMore"));
				}
				this.blinkRate = value;
				if (this.blinkRate == 0)
				{
					this.BlinkStyle = ErrorBlinkStyle.NeverBlink;
				}
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x0600253B RID: 9531 RVA: 0x000AE058 File Offset: 0x000AC258
		private static Icon DefaultIcon
		{
			get
			{
				if (ErrorProvider.defaultIcon == null)
				{
					Type typeFromHandle = typeof(ErrorProvider);
					lock (typeFromHandle)
					{
						if (ErrorProvider.defaultIcon == null)
						{
							ErrorProvider.defaultIcon = new Icon(typeof(ErrorProvider), "Error.ico");
						}
					}
				}
				return ErrorProvider.defaultIcon;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x0600253C RID: 9532 RVA: 0x000AE0C4 File Offset: 0x000AC2C4
		// (set) Token: 0x0600253D RID: 9533 RVA: 0x000AE0CC File Offset: 0x000AC2CC
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ErrorProviderIconDescr")]
		public Icon Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.icon = value;
				this.DisposeRegion();
				ErrorProvider.ErrorWindow[] array = new ErrorProvider.ErrorWindow[this.windows.Values.Count];
				this.windows.Values.CopyTo(array, 0);
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Update(false);
				}
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x0600253E RID: 9534 RVA: 0x000AE133 File Offset: 0x000AC333
		internal ErrorProvider.IconRegion Region
		{
			get
			{
				if (this.region == null)
				{
					this.region = new ErrorProvider.IconRegion(this.Icon);
				}
				return this.region;
			}
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x000AE154 File Offset: 0x000AC354
		void ISupportInitialize.BeginInit()
		{
			this.initializing = true;
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x000AE15D File Offset: 0x000AC35D
		private void EndInitCore()
		{
			this.initializing = false;
			if (this.setErrorManagerOnEndInit)
			{
				this.setErrorManagerOnEndInit = false;
				this.Set_ErrorManager(this.DataSource, this.DataMember, true);
			}
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x000AE188 File Offset: 0x000AC388
		void ISupportInitialize.EndInit()
		{
			ISupportInitializeNotification supportInitializeNotification = this.DataSource as ISupportInitializeNotification;
			if (supportInitializeNotification != null && !supportInitializeNotification.IsInitialized)
			{
				supportInitializeNotification.Initialized += this.DataSource_Initialized;
				return;
			}
			this.EndInitCore();
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x000AE1C8 File Offset: 0x000AC3C8
		private void DataSource_Initialized(object sender, EventArgs e)
		{
			ISupportInitializeNotification supportInitializeNotification = this.DataSource as ISupportInitializeNotification;
			if (supportInitializeNotification != null)
			{
				supportInitializeNotification.Initialized -= this.DataSource_Initialized;
			}
			this.EndInitCore();
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x000AE1FC File Offset: 0x000AC3FC
		public void Clear()
		{
			ErrorProvider.ErrorWindow[] array = new ErrorProvider.ErrorWindow[this.windows.Values.Count];
			this.windows.Values.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Dispose();
			}
			this.windows.Clear();
			foreach (object obj in this.items.Values)
			{
				ErrorProvider.ControlItem controlItem = (ErrorProvider.ControlItem)obj;
				if (controlItem != null)
				{
					controlItem.Dispose();
				}
			}
			this.items.Clear();
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x000AE2B4 File Offset: 0x000AC4B4
		public bool CanExtend(object extendee)
		{
			return extendee is Control && !(extendee is Form) && !(extendee is ToolBar);
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x000AE2D4 File Offset: 0x000AC4D4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Clear();
				this.DisposeRegion();
				this.UnwireEvents(this.errorManager);
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x000AE2F8 File Offset: 0x000AC4F8
		private void DisposeRegion()
		{
			if (this.region != null)
			{
				this.region.Dispose();
				this.region = null;
			}
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x000AE314 File Offset: 0x000AC514
		private ErrorProvider.ControlItem EnsureControlItem(Control control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			ErrorProvider.ControlItem controlItem = (ErrorProvider.ControlItem)this.items[control];
			if (controlItem == null)
			{
				int value = this.itemIdCounter + 1;
				this.itemIdCounter = value;
				controlItem = new ErrorProvider.ControlItem(this, control, (IntPtr)value);
				this.items[control] = controlItem;
			}
			return controlItem;
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x000AE370 File Offset: 0x000AC570
		internal ErrorProvider.ErrorWindow EnsureErrorWindow(Control parent)
		{
			ErrorProvider.ErrorWindow errorWindow = (ErrorProvider.ErrorWindow)this.windows[parent];
			if (errorWindow == null)
			{
				errorWindow = new ErrorProvider.ErrorWindow(this, parent);
				this.windows[parent] = errorWindow;
			}
			return errorWindow;
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x000AE3A8 File Offset: 0x000AC5A8
		[DefaultValue("")]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ErrorProviderErrorDescr")]
		public string GetError(Control control)
		{
			return this.EnsureControlItem(control).Error;
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x000AE3B6 File Offset: 0x000AC5B6
		[DefaultValue(ErrorIconAlignment.MiddleRight)]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ErrorProviderIconAlignmentDescr")]
		public ErrorIconAlignment GetIconAlignment(Control control)
		{
			return this.EnsureControlItem(control).IconAlignment;
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x000AE3C4 File Offset: 0x000AC5C4
		[DefaultValue(0)]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ErrorProviderIconPaddingDescr")]
		public int GetIconPadding(Control control)
		{
			return this.EnsureControlItem(control).IconPadding;
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x000AE3D2 File Offset: 0x000AC5D2
		private void ResetIcon()
		{
			this.Icon = ErrorProvider.DefaultIcon;
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x000AE3E0 File Offset: 0x000AC5E0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnRightToLeftChanged(EventArgs e)
		{
			foreach (object obj in this.windows.Values)
			{
				ErrorProvider.ErrorWindow errorWindow = (ErrorProvider.ErrorWindow)obj;
				errorWindow.Update(false);
			}
			if (this.onRightToLeftChanged != null)
			{
				this.onRightToLeftChanged(this, e);
			}
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x000AE454 File Offset: 0x000AC654
		public void SetError(Control control, string value)
		{
			this.EnsureControlItem(control).Error = value;
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x000AE463 File Offset: 0x000AC663
		public void SetIconAlignment(Control control, ErrorIconAlignment value)
		{
			this.EnsureControlItem(control).IconAlignment = value;
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x000AE472 File Offset: 0x000AC672
		public void SetIconPadding(Control control, int padding)
		{
			this.EnsureControlItem(control).IconPadding = padding;
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x000AE481 File Offset: 0x000AC681
		private bool ShouldSerializeIcon()
		{
			return this.Icon != ErrorProvider.DefaultIcon;
		}

		// Token: 0x04000F7A RID: 3962
		private Hashtable items = new Hashtable();

		// Token: 0x04000F7B RID: 3963
		private Hashtable windows = new Hashtable();

		// Token: 0x04000F7C RID: 3964
		private Icon icon = ErrorProvider.DefaultIcon;

		// Token: 0x04000F7D RID: 3965
		private ErrorProvider.IconRegion region;

		// Token: 0x04000F7E RID: 3966
		private int itemIdCounter;

		// Token: 0x04000F7F RID: 3967
		private int blinkRate;

		// Token: 0x04000F80 RID: 3968
		private ErrorBlinkStyle blinkStyle;

		// Token: 0x04000F81 RID: 3969
		private bool showIcon = true;

		// Token: 0x04000F82 RID: 3970
		private bool inSetErrorManager;

		// Token: 0x04000F83 RID: 3971
		private bool setErrorManagerOnEndInit;

		// Token: 0x04000F84 RID: 3972
		private bool initializing;

		// Token: 0x04000F85 RID: 3973
		[ThreadStatic]
		private static Icon defaultIcon;

		// Token: 0x04000F86 RID: 3974
		private const int defaultBlinkRate = 250;

		// Token: 0x04000F87 RID: 3975
		private const ErrorBlinkStyle defaultBlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;

		// Token: 0x04000F88 RID: 3976
		private const ErrorIconAlignment defaultIconAlignment = ErrorIconAlignment.MiddleRight;

		// Token: 0x04000F89 RID: 3977
		private ContainerControl parentControl;

		// Token: 0x04000F8A RID: 3978
		private object dataSource;

		// Token: 0x04000F8B RID: 3979
		private string dataMember;

		// Token: 0x04000F8C RID: 3980
		private BindingManagerBase errorManager;

		// Token: 0x04000F8D RID: 3981
		private EventHandler currentChanged;

		// Token: 0x04000F8E RID: 3982
		private EventHandler propChangedEvent;

		// Token: 0x04000F8F RID: 3983
		private EventHandler onRightToLeftChanged;

		// Token: 0x04000F90 RID: 3984
		private bool rightToLeft;

		// Token: 0x04000F91 RID: 3985
		private object userData;

		// Token: 0x02000689 RID: 1673
		internal class ErrorWindow : NativeWindow
		{
			// Token: 0x06006742 RID: 26434 RVA: 0x00182704 File Offset: 0x00180904
			public ErrorWindow(ErrorProvider provider, Control parent)
			{
				this.provider = provider;
				this.parent = parent;
			}

			// Token: 0x06006743 RID: 26435 RVA: 0x00182758 File Offset: 0x00180958
			public void Add(ErrorProvider.ControlItem item)
			{
				this.items.Add(item);
				if (!this.EnsureCreated())
				{
					return;
				}
				NativeMethods.TOOLINFO_T toolinfo_T = new NativeMethods.TOOLINFO_T();
				toolinfo_T.cbSize = Marshal.SizeOf(toolinfo_T);
				toolinfo_T.hwnd = base.Handle;
				toolinfo_T.uId = item.Id;
				toolinfo_T.lpszText = item.Error;
				toolinfo_T.uFlags = 16;
				UnsafeNativeMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), NativeMethods.TTM_ADDTOOL, 0, toolinfo_T);
				this.Update(false);
			}

			// Token: 0x06006744 RID: 26436 RVA: 0x001827E3 File Offset: 0x001809E3
			public void Dispose()
			{
				this.EnsureDestroyed();
			}

			// Token: 0x06006745 RID: 26437 RVA: 0x001827EC File Offset: 0x001809EC
			private bool EnsureCreated()
			{
				if (base.Handle == IntPtr.Zero)
				{
					if (!this.parent.IsHandleCreated)
					{
						return false;
					}
					this.CreateHandle(new CreateParams
					{
						Caption = string.Empty,
						Style = 1342177280,
						ClassStyle = 8,
						X = 0,
						Y = 0,
						Width = 0,
						Height = 0,
						Parent = this.parent.Handle
					});
					NativeMethods.INITCOMMONCONTROLSEX initcommoncontrolsex = new NativeMethods.INITCOMMONCONTROLSEX();
					initcommoncontrolsex.dwICC = 8;
					initcommoncontrolsex.dwSize = Marshal.SizeOf(initcommoncontrolsex);
					SafeNativeMethods.InitCommonControlsEx(initcommoncontrolsex);
					CreateParams createParams = new CreateParams();
					createParams.Parent = base.Handle;
					createParams.ClassName = "tooltips_class32";
					createParams.Style = 1;
					this.tipWindow = new NativeWindow();
					this.tipWindow.CreateHandle(createParams);
					UnsafeNativeMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), 1048, 0, SystemInformation.MaxWindowTrackSize.Width);
					SafeNativeMethods.SetWindowPos(new HandleRef(this.tipWindow, this.tipWindow.Handle), NativeMethods.HWND_TOP, 0, 0, 0, 0, 19);
					UnsafeNativeMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), 1027, 3, 0);
				}
				return true;
			}

			// Token: 0x06006746 RID: 26438 RVA: 0x0018294C File Offset: 0x00180B4C
			private void EnsureDestroyed()
			{
				if (this.timer != null)
				{
					this.timer.Dispose();
					this.timer = null;
				}
				if (this.tipWindow != null)
				{
					this.tipWindow.DestroyHandle();
					this.tipWindow = null;
				}
				SafeNativeMethods.SetWindowPos(new HandleRef(this, base.Handle), NativeMethods.HWND_TOP, this.windowBounds.X, this.windowBounds.Y, this.windowBounds.Width, this.windowBounds.Height, 131);
				if (this.parent != null)
				{
					this.parent.Invalidate(true);
				}
				this.DestroyHandle();
				if (this.mirrordc != null)
				{
					this.mirrordc.Dispose();
				}
			}

			// Token: 0x06006747 RID: 26439 RVA: 0x00182A04 File Offset: 0x00180C04
			private void CreateMirrorDC(IntPtr hdc, int originOffset)
			{
				this.mirrordc = DeviceContext.FromHdc(hdc);
				if (this.parent.IsMirrored && this.mirrordc != null)
				{
					this.mirrordc.SaveHdc();
					this.mirrordcExtent = this.mirrordc.ViewportExtent;
					this.mirrordcOrigin = this.mirrordc.ViewportOrigin;
					this.mirrordcMode = this.mirrordc.SetMapMode(DeviceContextMapMode.Anisotropic);
					this.mirrordc.ViewportExtent = new Size(-this.mirrordcExtent.Width, this.mirrordcExtent.Height);
					this.mirrordc.ViewportOrigin = new Point(this.mirrordcOrigin.X + originOffset, this.mirrordcOrigin.Y);
				}
			}

			// Token: 0x06006748 RID: 26440 RVA: 0x00182AC8 File Offset: 0x00180CC8
			private void RestoreMirrorDC()
			{
				if (this.parent.IsMirrored && this.mirrordc != null)
				{
					this.mirrordc.ViewportExtent = this.mirrordcExtent;
					this.mirrordc.ViewportOrigin = this.mirrordcOrigin;
					this.mirrordc.SetMapMode(this.mirrordcMode);
					this.mirrordc.RestoreHdc();
					this.mirrordc.Dispose();
				}
				this.mirrordc = null;
				this.mirrordcExtent = Size.Empty;
				this.mirrordcOrigin = Point.Empty;
				this.mirrordcMode = DeviceContextMapMode.Text;
			}

			// Token: 0x06006749 RID: 26441 RVA: 0x00182B58 File Offset: 0x00180D58
			private void OnPaint(ref Message m)
			{
				NativeMethods.PAINTSTRUCT paintstruct = default(NativeMethods.PAINTSTRUCT);
				IntPtr hdc = UnsafeNativeMethods.BeginPaint(new HandleRef(this, base.Handle), ref paintstruct);
				try
				{
					this.CreateMirrorDC(hdc, this.windowBounds.Width - 1);
					try
					{
						for (int i = 0; i < this.items.Count; i++)
						{
							ErrorProvider.ControlItem controlItem = (ErrorProvider.ControlItem)this.items[i];
							Rectangle iconBounds = controlItem.GetIconBounds(this.provider.Region.Size);
							SafeNativeMethods.DrawIconEx(new HandleRef(this, this.mirrordc.Hdc), iconBounds.X - this.windowBounds.X, iconBounds.Y - this.windowBounds.Y, new HandleRef(this.provider.Region, this.provider.Region.IconHandle), iconBounds.Width, iconBounds.Height, 0, NativeMethods.NullHandleRef, 3);
						}
					}
					finally
					{
						this.RestoreMirrorDC();
					}
				}
				finally
				{
					UnsafeNativeMethods.EndPaint(new HandleRef(this, base.Handle), ref paintstruct);
				}
			}

			// Token: 0x0600674A RID: 26442 RVA: 0x0003BADD File Offset: 0x00039CDD
			protected override void OnThreadException(Exception e)
			{
				Application.OnThreadException(e);
			}

			// Token: 0x0600674B RID: 26443 RVA: 0x00182C88 File Offset: 0x00180E88
			private void OnTimer(object sender, EventArgs e)
			{
				int num = 0;
				for (int i = 0; i < this.items.Count; i++)
				{
					num += ((ErrorProvider.ControlItem)this.items[i]).BlinkPhase;
				}
				if (num == 0 && this.provider.BlinkStyle != ErrorBlinkStyle.AlwaysBlink)
				{
					this.timer.Stop();
				}
				this.Update(true);
			}

			// Token: 0x0600674C RID: 26444 RVA: 0x00182CEC File Offset: 0x00180EEC
			private void OnToolTipVisibilityChanging(IntPtr id, bool toolTipShown)
			{
				for (int i = 0; i < this.items.Count; i++)
				{
					if (((ErrorProvider.ControlItem)this.items[i]).Id == id)
					{
						((ErrorProvider.ControlItem)this.items[i]).ToolTipShown = toolTipShown;
					}
				}
			}

			// Token: 0x0600674D RID: 26445 RVA: 0x00182D44 File Offset: 0x00180F44
			public void Remove(ErrorProvider.ControlItem item)
			{
				this.items.Remove(item);
				if (this.tipWindow != null)
				{
					NativeMethods.TOOLINFO_T toolinfo_T = new NativeMethods.TOOLINFO_T();
					toolinfo_T.cbSize = Marshal.SizeOf(toolinfo_T);
					toolinfo_T.hwnd = base.Handle;
					toolinfo_T.uId = item.Id;
					UnsafeNativeMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), NativeMethods.TTM_DELTOOL, 0, toolinfo_T);
				}
				if (this.items.Count == 0)
				{
					this.EnsureDestroyed();
					return;
				}
				this.Update(false);
			}

			// Token: 0x0600674E RID: 26446 RVA: 0x00182DD0 File Offset: 0x00180FD0
			internal void StartBlinking()
			{
				if (this.timer == null)
				{
					this.timer = new Timer();
					this.timer.Tick += this.OnTimer;
				}
				this.timer.Interval = this.provider.BlinkRate;
				this.timer.Start();
				this.Update(false);
			}

			// Token: 0x0600674F RID: 26447 RVA: 0x00182E2F File Offset: 0x0018102F
			internal void StopBlinking()
			{
				if (this.timer != null)
				{
					this.timer.Stop();
				}
				this.Update(false);
			}

			// Token: 0x06006750 RID: 26448 RVA: 0x00182E4C File Offset: 0x0018104C
			public void Update(bool timerCaused)
			{
				ErrorProvider.IconRegion region = this.provider.Region;
				Size size = region.Size;
				this.windowBounds = Rectangle.Empty;
				for (int i = 0; i < this.items.Count; i++)
				{
					ErrorProvider.ControlItem controlItem = (ErrorProvider.ControlItem)this.items[i];
					Rectangle iconBounds = controlItem.GetIconBounds(size);
					if (this.windowBounds.IsEmpty)
					{
						this.windowBounds = iconBounds;
					}
					else
					{
						this.windowBounds = Rectangle.Union(this.windowBounds, iconBounds);
					}
				}
				Region region2 = new Region(new Rectangle(0, 0, 0, 0));
				IntPtr intPtr = IntPtr.Zero;
				try
				{
					for (int j = 0; j < this.items.Count; j++)
					{
						ErrorProvider.ControlItem controlItem2 = (ErrorProvider.ControlItem)this.items[j];
						Rectangle iconBounds2 = controlItem2.GetIconBounds(size);
						iconBounds2.X -= this.windowBounds.X;
						iconBounds2.Y -= this.windowBounds.Y;
						bool flag = true;
						if (!controlItem2.ToolTipShown)
						{
							switch (this.provider.BlinkStyle)
							{
							case ErrorBlinkStyle.BlinkIfDifferentError:
								flag = (controlItem2.BlinkPhase == 0 || (controlItem2.BlinkPhase > 0 && (controlItem2.BlinkPhase & 1) == (j & 1)));
								break;
							case ErrorBlinkStyle.AlwaysBlink:
								flag = ((j & 1) == 0 == this.provider.showIcon);
								break;
							}
						}
						if (flag)
						{
							region.Region.Translate(iconBounds2.X, iconBounds2.Y);
							region2.Union(region.Region);
							region.Region.Translate(-iconBounds2.X, -iconBounds2.Y);
						}
						if (this.tipWindow != null)
						{
							NativeMethods.TOOLINFO_T toolinfo_T = new NativeMethods.TOOLINFO_T();
							toolinfo_T.cbSize = Marshal.SizeOf(toolinfo_T);
							toolinfo_T.hwnd = base.Handle;
							toolinfo_T.uId = controlItem2.Id;
							toolinfo_T.lpszText = controlItem2.Error;
							toolinfo_T.rect = NativeMethods.RECT.FromXYWH(iconBounds2.X, iconBounds2.Y, iconBounds2.Width, iconBounds2.Height);
							toolinfo_T.uFlags = 16;
							if (this.provider.RightToLeft)
							{
								toolinfo_T.uFlags |= 4;
							}
							UnsafeNativeMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), NativeMethods.TTM_SETTOOLINFO, 0, toolinfo_T);
						}
						if (timerCaused && controlItem2.BlinkPhase > 0)
						{
							ErrorProvider.ControlItem controlItem3 = controlItem2;
							int blinkPhase = controlItem3.BlinkPhase;
							controlItem3.BlinkPhase = blinkPhase - 1;
						}
					}
					if (timerCaused)
					{
						this.provider.showIcon = !this.provider.showIcon;
					}
					DeviceContext deviceContext = null;
					using (DeviceContext deviceContext = DeviceContext.FromHwnd(base.Handle))
					{
						this.CreateMirrorDC(deviceContext.Hdc, this.windowBounds.Width);
						Graphics graphics = Graphics.FromHdcInternal(this.mirrordc.Hdc);
						try
						{
							intPtr = region2.GetHrgn(graphics);
							System.Internal.HandleCollector.Add(intPtr, NativeMethods.CommonHandles.GDI);
						}
						finally
						{
							graphics.Dispose();
							this.RestoreMirrorDC();
						}
						if (UnsafeNativeMethods.SetWindowRgn(new HandleRef(this, base.Handle), new HandleRef(region2, intPtr), true) != 0)
						{
							intPtr = IntPtr.Zero;
						}
					}
				}
				finally
				{
					region2.Dispose();
					if (intPtr != IntPtr.Zero)
					{
						SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
					}
				}
				SafeNativeMethods.SetWindowPos(new HandleRef(this, base.Handle), NativeMethods.HWND_TOP, this.windowBounds.X, this.windowBounds.Y, this.windowBounds.Width, this.windowBounds.Height, 16);
				SafeNativeMethods.InvalidateRect(new HandleRef(this, base.Handle), null, false);
			}

			// Token: 0x06006751 RID: 26449 RVA: 0x0018325C File Offset: 0x0018145C
			protected override void WndProc(ref Message m)
			{
				int msg = m.Msg;
				if (msg != 15)
				{
					if (msg != 20)
					{
						if (msg == 78)
						{
							NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
							if (nmhdr.code == -521 || nmhdr.code == -522)
							{
								this.OnToolTipVisibilityChanging(nmhdr.idFrom, nmhdr.code == -521);
								return;
							}
						}
						else
						{
							base.WndProc(ref m);
						}
					}
					return;
				}
				this.OnPaint(ref m);
			}

			// Token: 0x04003A91 RID: 14993
			private ArrayList items = new ArrayList();

			// Token: 0x04003A92 RID: 14994
			private Control parent;

			// Token: 0x04003A93 RID: 14995
			private ErrorProvider provider;

			// Token: 0x04003A94 RID: 14996
			private Rectangle windowBounds = Rectangle.Empty;

			// Token: 0x04003A95 RID: 14997
			private Timer timer;

			// Token: 0x04003A96 RID: 14998
			private NativeWindow tipWindow;

			// Token: 0x04003A97 RID: 14999
			private DeviceContext mirrordc;

			// Token: 0x04003A98 RID: 15000
			private Size mirrordcExtent = Size.Empty;

			// Token: 0x04003A99 RID: 15001
			private Point mirrordcOrigin = Point.Empty;

			// Token: 0x04003A9A RID: 15002
			private DeviceContextMapMode mirrordcMode = DeviceContextMapMode.Text;
		}

		// Token: 0x0200068A RID: 1674
		internal class ControlItem
		{
			// Token: 0x06006752 RID: 26450 RVA: 0x001832D8 File Offset: 0x001814D8
			public ControlItem(ErrorProvider provider, Control control, IntPtr id)
			{
				this.toolTipShown = false;
				this.iconAlignment = ErrorIconAlignment.MiddleRight;
				this.error = string.Empty;
				this.id = id;
				this.control = control;
				this.provider = provider;
				this.control.HandleCreated += this.OnCreateHandle;
				this.control.HandleDestroyed += this.OnDestroyHandle;
				this.control.LocationChanged += this.OnBoundsChanged;
				this.control.SizeChanged += this.OnBoundsChanged;
				this.control.VisibleChanged += this.OnParentVisibleChanged;
				this.control.ParentChanged += this.OnParentVisibleChanged;
			}

			// Token: 0x06006753 RID: 26451 RVA: 0x001833A4 File Offset: 0x001815A4
			public void Dispose()
			{
				if (this.control != null)
				{
					this.control.HandleCreated -= this.OnCreateHandle;
					this.control.HandleDestroyed -= this.OnDestroyHandle;
					this.control.LocationChanged -= this.OnBoundsChanged;
					this.control.SizeChanged -= this.OnBoundsChanged;
					this.control.VisibleChanged -= this.OnParentVisibleChanged;
					this.control.ParentChanged -= this.OnParentVisibleChanged;
				}
				this.error = string.Empty;
			}

			// Token: 0x17001680 RID: 5760
			// (get) Token: 0x06006754 RID: 26452 RVA: 0x00183451 File Offset: 0x00181651
			public IntPtr Id
			{
				get
				{
					return this.id;
				}
			}

			// Token: 0x17001681 RID: 5761
			// (get) Token: 0x06006755 RID: 26453 RVA: 0x00183459 File Offset: 0x00181659
			// (set) Token: 0x06006756 RID: 26454 RVA: 0x00183461 File Offset: 0x00181661
			public int BlinkPhase
			{
				get
				{
					return this.blinkPhase;
				}
				set
				{
					this.blinkPhase = value;
				}
			}

			// Token: 0x17001682 RID: 5762
			// (get) Token: 0x06006757 RID: 26455 RVA: 0x0018346A File Offset: 0x0018166A
			// (set) Token: 0x06006758 RID: 26456 RVA: 0x00183472 File Offset: 0x00181672
			public int IconPadding
			{
				get
				{
					return this.iconPadding;
				}
				set
				{
					if (this.iconPadding != value)
					{
						this.iconPadding = value;
						this.UpdateWindow();
					}
				}
			}

			// Token: 0x17001683 RID: 5763
			// (get) Token: 0x06006759 RID: 26457 RVA: 0x0018348A File Offset: 0x0018168A
			// (set) Token: 0x0600675A RID: 26458 RVA: 0x00183494 File Offset: 0x00181694
			public string Error
			{
				get
				{
					return this.error;
				}
				set
				{
					if (value == null)
					{
						value = "";
					}
					if (this.error.Equals(value) && this.provider.BlinkStyle != ErrorBlinkStyle.AlwaysBlink)
					{
						return;
					}
					bool flag = this.error.Length == 0;
					this.error = value;
					if (value.Length == 0)
					{
						this.RemoveFromWindow();
						return;
					}
					if (flag)
					{
						this.AddToWindow();
						return;
					}
					if (this.provider.BlinkStyle != ErrorBlinkStyle.NeverBlink)
					{
						this.StartBlinking();
						return;
					}
					this.UpdateWindow();
				}
			}

			// Token: 0x17001684 RID: 5764
			// (get) Token: 0x0600675B RID: 26459 RVA: 0x00183512 File Offset: 0x00181712
			// (set) Token: 0x0600675C RID: 26460 RVA: 0x0018351A File Offset: 0x0018171A
			public ErrorIconAlignment IconAlignment
			{
				get
				{
					return this.iconAlignment;
				}
				set
				{
					if (this.iconAlignment != value)
					{
						if (!ClientUtils.IsEnumValid(value, (int)value, 0, 5))
						{
							throw new InvalidEnumArgumentException("value", (int)value, typeof(ErrorIconAlignment));
						}
						this.iconAlignment = value;
						this.UpdateWindow();
					}
				}
			}

			// Token: 0x17001685 RID: 5765
			// (get) Token: 0x0600675D RID: 26461 RVA: 0x00183558 File Offset: 0x00181758
			// (set) Token: 0x0600675E RID: 26462 RVA: 0x00183560 File Offset: 0x00181760
			public bool ToolTipShown
			{
				get
				{
					return this.toolTipShown;
				}
				set
				{
					this.toolTipShown = value;
				}
			}

			// Token: 0x0600675F RID: 26463 RVA: 0x00183569 File Offset: 0x00181769
			internal ErrorIconAlignment RTLTranslateIconAlignment(ErrorIconAlignment align)
			{
				if (!this.provider.RightToLeft)
				{
					return align;
				}
				switch (align)
				{
				case ErrorIconAlignment.TopLeft:
					return ErrorIconAlignment.TopRight;
				case ErrorIconAlignment.TopRight:
					return ErrorIconAlignment.TopLeft;
				case ErrorIconAlignment.MiddleLeft:
					return ErrorIconAlignment.MiddleRight;
				case ErrorIconAlignment.MiddleRight:
					return ErrorIconAlignment.MiddleLeft;
				case ErrorIconAlignment.BottomLeft:
					return ErrorIconAlignment.BottomRight;
				case ErrorIconAlignment.BottomRight:
					return ErrorIconAlignment.BottomLeft;
				default:
					return align;
				}
			}

			// Token: 0x06006760 RID: 26464 RVA: 0x001835A8 File Offset: 0x001817A8
			internal Rectangle GetIconBounds(Size size)
			{
				int x = 0;
				int y = 0;
				switch (this.RTLTranslateIconAlignment(this.IconAlignment))
				{
				case ErrorIconAlignment.TopLeft:
				case ErrorIconAlignment.MiddleLeft:
				case ErrorIconAlignment.BottomLeft:
					x = this.control.Left - size.Width - this.iconPadding;
					break;
				case ErrorIconAlignment.TopRight:
				case ErrorIconAlignment.MiddleRight:
				case ErrorIconAlignment.BottomRight:
					x = this.control.Right + this.iconPadding;
					break;
				}
				switch (this.IconAlignment)
				{
				case ErrorIconAlignment.TopLeft:
				case ErrorIconAlignment.TopRight:
					y = this.control.Top;
					break;
				case ErrorIconAlignment.MiddleLeft:
				case ErrorIconAlignment.MiddleRight:
					y = this.control.Top + (this.control.Height - size.Height) / 2;
					break;
				case ErrorIconAlignment.BottomLeft:
				case ErrorIconAlignment.BottomRight:
					y = this.control.Bottom - size.Height;
					break;
				}
				return new Rectangle(x, y, size.Width, size.Height);
			}

			// Token: 0x06006761 RID: 26465 RVA: 0x00183698 File Offset: 0x00181898
			private void UpdateWindow()
			{
				if (this.window != null)
				{
					this.window.Update(false);
				}
			}

			// Token: 0x06006762 RID: 26466 RVA: 0x001836AE File Offset: 0x001818AE
			private void StartBlinking()
			{
				if (this.window != null)
				{
					this.BlinkPhase = 10;
					this.window.StartBlinking();
				}
			}

			// Token: 0x06006763 RID: 26467 RVA: 0x001836CC File Offset: 0x001818CC
			private void AddToWindow()
			{
				if (this.window == null && (this.control.Created || this.control.RecreatingHandle) && this.control.Visible && this.control.ParentInternal != null && this.error.Length > 0)
				{
					this.window = this.provider.EnsureErrorWindow(this.control.ParentInternal);
					this.window.Add(this);
					if (this.provider.BlinkStyle != ErrorBlinkStyle.NeverBlink)
					{
						this.StartBlinking();
					}
				}
			}

			// Token: 0x06006764 RID: 26468 RVA: 0x0018375F File Offset: 0x0018195F
			private void RemoveFromWindow()
			{
				if (this.window != null)
				{
					this.window.Remove(this);
					this.window = null;
				}
			}

			// Token: 0x06006765 RID: 26469 RVA: 0x0018377C File Offset: 0x0018197C
			private void OnBoundsChanged(object sender, EventArgs e)
			{
				this.UpdateWindow();
			}

			// Token: 0x06006766 RID: 26470 RVA: 0x00183784 File Offset: 0x00181984
			private void OnParentVisibleChanged(object sender, EventArgs e)
			{
				this.BlinkPhase = 0;
				this.RemoveFromWindow();
				this.AddToWindow();
			}

			// Token: 0x06006767 RID: 26471 RVA: 0x00183799 File Offset: 0x00181999
			private void OnCreateHandle(object sender, EventArgs e)
			{
				this.AddToWindow();
			}

			// Token: 0x06006768 RID: 26472 RVA: 0x001837A1 File Offset: 0x001819A1
			private void OnDestroyHandle(object sender, EventArgs e)
			{
				this.RemoveFromWindow();
			}

			// Token: 0x04003A9B RID: 15003
			private string error;

			// Token: 0x04003A9C RID: 15004
			private Control control;

			// Token: 0x04003A9D RID: 15005
			private ErrorProvider.ErrorWindow window;

			// Token: 0x04003A9E RID: 15006
			private ErrorProvider provider;

			// Token: 0x04003A9F RID: 15007
			private int blinkPhase;

			// Token: 0x04003AA0 RID: 15008
			private IntPtr id;

			// Token: 0x04003AA1 RID: 15009
			private int iconPadding;

			// Token: 0x04003AA2 RID: 15010
			private bool toolTipShown;

			// Token: 0x04003AA3 RID: 15011
			private ErrorIconAlignment iconAlignment;

			// Token: 0x04003AA4 RID: 15012
			private const int startingBlinkPhase = 10;
		}

		// Token: 0x0200068B RID: 1675
		internal class IconRegion
		{
			// Token: 0x06006769 RID: 26473 RVA: 0x001837A9 File Offset: 0x001819A9
			public IconRegion(Icon icon)
			{
				this.icon = new Icon(icon, 16, 16);
			}

			// Token: 0x17001686 RID: 5766
			// (get) Token: 0x0600676A RID: 26474 RVA: 0x001837C1 File Offset: 0x001819C1
			public IntPtr IconHandle
			{
				get
				{
					return this.icon.Handle;
				}
			}

			// Token: 0x17001687 RID: 5767
			// (get) Token: 0x0600676B RID: 26475 RVA: 0x001837D0 File Offset: 0x001819D0
			public Region Region
			{
				get
				{
					if (this.region == null)
					{
						this.region = new Region(new Rectangle(0, 0, 0, 0));
						IntPtr intPtr = IntPtr.Zero;
						try
						{
							Size size = this.icon.Size;
							Bitmap bitmap = this.icon.ToBitmap();
							bitmap.MakeTransparent();
							intPtr = ControlPaint.CreateHBitmapTransparencyMask(bitmap);
							bitmap.Dispose();
							int num = 16;
							int num2 = 2 * ((size.Width + 15) / num);
							byte[] array = new byte[num2 * size.Height];
							SafeNativeMethods.GetBitmapBits(new HandleRef(null, intPtr), array.Length, array);
							for (int i = 0; i < size.Height; i++)
							{
								for (int j = 0; j < size.Width; j++)
								{
									if (((int)array[i * num2 + j / 8] & 1 << 7 - j % 8) == 0)
									{
										this.region.Union(new Rectangle(j, i, 1, 1));
									}
								}
							}
							this.region.Intersect(new Rectangle(0, 0, size.Width, size.Height));
						}
						finally
						{
							if (intPtr != IntPtr.Zero)
							{
								SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
							}
						}
					}
					return this.region;
				}
			}

			// Token: 0x17001688 RID: 5768
			// (get) Token: 0x0600676C RID: 26476 RVA: 0x00183914 File Offset: 0x00181B14
			public Size Size
			{
				get
				{
					return this.icon.Size;
				}
			}

			// Token: 0x0600676D RID: 26477 RVA: 0x00183921 File Offset: 0x00181B21
			public void Dispose()
			{
				if (this.region != null)
				{
					this.region.Dispose();
					this.region = null;
				}
				this.icon.Dispose();
			}

			// Token: 0x04003AA5 RID: 15013
			private Region region;

			// Token: 0x04003AA6 RID: 15014
			private Icon icon;
		}
	}
}
