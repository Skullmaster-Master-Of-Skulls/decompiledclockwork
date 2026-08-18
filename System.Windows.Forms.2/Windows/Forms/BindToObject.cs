using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x0200013D RID: 317
	internal class BindToObject
	{
		// Token: 0x06000C41 RID: 3137 RVA: 0x00023235 File Offset: 0x00021435
		private void PropValueChanged(object sender, EventArgs e)
		{
			if (this.bindingManager != null)
			{
				this.bindingManager.OnCurrentChanged(EventArgs.Empty);
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x00023250 File Offset: 0x00021450
		private bool IsDataSourceInitialized
		{
			get
			{
				if (this.dataSourceInitialized)
				{
					return true;
				}
				ISupportInitializeNotification supportInitializeNotification = this.dataSource as ISupportInitializeNotification;
				if (supportInitializeNotification == null || supportInitializeNotification.IsInitialized)
				{
					this.dataSourceInitialized = true;
					return true;
				}
				if (this.waitingOnDataSource)
				{
					return false;
				}
				supportInitializeNotification.Initialized += this.DataSource_Initialized;
				this.waitingOnDataSource = true;
				return false;
			}
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x000232AB File Offset: 0x000214AB
		internal BindToObject(Binding owner, object dataSource, string dataMember)
		{
			this.owner = owner;
			this.dataSource = dataSource;
			this.dataMember = new BindingMemberInfo(dataMember);
			this.CheckBinding();
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x000232E0 File Offset: 0x000214E0
		private void DataSource_Initialized(object sender, EventArgs e)
		{
			ISupportInitializeNotification supportInitializeNotification = this.dataSource as ISupportInitializeNotification;
			if (supportInitializeNotification != null)
			{
				supportInitializeNotification.Initialized -= this.DataSource_Initialized;
			}
			this.waitingOnDataSource = false;
			this.dataSourceInitialized = true;
			this.CheckBinding();
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00023324 File Offset: 0x00021524
		internal void SetBindingManagerBase(BindingManagerBase lManager)
		{
			if (this.bindingManager == lManager)
			{
				return;
			}
			if (this.bindingManager != null && this.fieldInfo != null && this.bindingManager.IsBinding && !(this.bindingManager is CurrencyManager))
			{
				this.fieldInfo.RemoveValueChanged(this.bindingManager.Current, new EventHandler(this.PropValueChanged));
				this.fieldInfo = null;
			}
			this.bindingManager = lManager;
			this.CheckBinding();
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x0002339B File Offset: 0x0002159B
		internal string DataErrorText
		{
			get
			{
				return this.errorText;
			}
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x000233A4 File Offset: 0x000215A4
		private string GetErrorText(object value)
		{
			IDataErrorInfo dataErrorInfo = value as IDataErrorInfo;
			string text = string.Empty;
			if (dataErrorInfo != null)
			{
				if (this.fieldInfo == null)
				{
					text = dataErrorInfo.Error;
				}
				else
				{
					text = dataErrorInfo[this.fieldInfo.Name];
				}
			}
			return text ?? string.Empty;
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x000233F0 File Offset: 0x000215F0
		internal object GetValue()
		{
			object obj = this.bindingManager.Current;
			this.errorText = this.GetErrorText(obj);
			if (this.fieldInfo != null)
			{
				obj = this.fieldInfo.GetValue(obj);
			}
			return obj;
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x0002342C File Offset: 0x0002162C
		internal Type BindToType
		{
			get
			{
				if (this.dataMember.BindingField.Length == 0)
				{
					Type type = this.bindingManager.BindType;
					if (typeof(Array).IsAssignableFrom(type))
					{
						type = type.GetElementType();
					}
					return type;
				}
				if (this.fieldInfo != null)
				{
					return this.fieldInfo.PropertyType;
				}
				return null;
			}
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00023488 File Offset: 0x00021688
		internal void SetValue(object value)
		{
			object obj = null;
			if (this.fieldInfo != null)
			{
				obj = this.bindingManager.Current;
				if (obj is IEditableObject)
				{
					((IEditableObject)obj).BeginEdit();
				}
				if (!this.fieldInfo.IsReadOnly)
				{
					this.fieldInfo.SetValue(obj, value);
				}
			}
			else
			{
				CurrencyManager currencyManager = this.bindingManager as CurrencyManager;
				if (currencyManager != null)
				{
					currencyManager[currencyManager.Position] = value;
					obj = value;
				}
			}
			this.errorText = this.GetErrorText(obj);
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x00023505 File Offset: 0x00021705
		internal BindingMemberInfo BindingMemberInfo
		{
			get
			{
				return this.dataMember;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x0002350D File Offset: 0x0002170D
		internal object DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x00023515 File Offset: 0x00021715
		internal PropertyDescriptor FieldInfo
		{
			get
			{
				return this.fieldInfo;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x0002351D File Offset: 0x0002171D
		internal BindingManagerBase BindingManagerBase
		{
			get
			{
				return this.bindingManager;
			}
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00023528 File Offset: 0x00021728
		internal void CheckBinding()
		{
			if (this.owner != null && this.owner.BindableComponent != null && this.owner.ControlAtDesignTime())
			{
				return;
			}
			if (this.owner.BindingManagerBase != null && this.fieldInfo != null && this.owner.BindingManagerBase.IsBinding && !(this.owner.BindingManagerBase is CurrencyManager))
			{
				this.fieldInfo.RemoveValueChanged(this.owner.BindingManagerBase.Current, new EventHandler(this.PropValueChanged));
			}
			if (this.owner != null && this.owner.BindingManagerBase != null && this.owner.BindableComponent != null && this.owner.ComponentCreated && this.IsDataSourceInitialized)
			{
				string bindingField = this.dataMember.BindingField;
				this.fieldInfo = this.owner.BindingManagerBase.GetItemProperties().Find(bindingField, true);
				if (this.owner.BindingManagerBase.DataSource != null && this.fieldInfo == null && bindingField.Length > 0)
				{
					throw new ArgumentException(SR.GetString("ListBindingBindField", new object[]
					{
						bindingField
					}), "dataMember");
				}
				if (this.fieldInfo != null && this.owner.BindingManagerBase.IsBinding && !(this.owner.BindingManagerBase is CurrencyManager))
				{
					this.fieldInfo.AddValueChanged(this.owner.BindingManagerBase.Current, new EventHandler(this.PropValueChanged));
					return;
				}
			}
			else
			{
				this.fieldInfo = null;
			}
		}

		// Token: 0x04000704 RID: 1796
		private PropertyDescriptor fieldInfo;

		// Token: 0x04000705 RID: 1797
		private BindingMemberInfo dataMember;

		// Token: 0x04000706 RID: 1798
		private object dataSource;

		// Token: 0x04000707 RID: 1799
		private BindingManagerBase bindingManager;

		// Token: 0x04000708 RID: 1800
		private Binding owner;

		// Token: 0x04000709 RID: 1801
		private string errorText = string.Empty;

		// Token: 0x0400070A RID: 1802
		private bool dataSourceInitialized;

		// Token: 0x0400070B RID: 1803
		private bool waitingOnDataSource;
	}
}
