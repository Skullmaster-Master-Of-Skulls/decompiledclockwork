using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x0200016D RID: 365
	[DefaultEvent("CollectionChanged")]
	[Editor("System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[TypeConverter("System.Windows.Forms.Design.ControlBindingsConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ControlBindingsCollection : BindingsCollection
	{
		// Token: 0x06001309 RID: 4873 RVA: 0x0003D085 File Offset: 0x0003B285
		public ControlBindingsCollection(IBindableComponent control)
		{
			this.control = control;
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x0003D094 File Offset: 0x0003B294
		public IBindableComponent BindableComponent
		{
			get
			{
				return this.control;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x0003D09C File Offset: 0x0003B29C
		public Control Control
		{
			get
			{
				return this.control as Control;
			}
		}

		// Token: 0x1700045C RID: 1116
		public Binding this[string propertyName]
		{
			get
			{
				foreach (object obj in this)
				{
					Binding binding = (Binding)obj;
					if (string.Equals(binding.PropertyName, propertyName, StringComparison.OrdinalIgnoreCase))
					{
						return binding;
					}
				}
				return null;
			}
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0003D110 File Offset: 0x0003B310
		public new void Add(Binding binding)
		{
			base.Add(binding);
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x0003D11C File Offset: 0x0003B31C
		public Binding Add(string propertyName, object dataSource, string dataMember)
		{
			return this.Add(propertyName, dataSource, dataMember, false, this.DefaultDataSourceUpdateMode, null, string.Empty, null);
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0003D140 File Offset: 0x0003B340
		public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled)
		{
			return this.Add(propertyName, dataSource, dataMember, formattingEnabled, this.DefaultDataSourceUpdateMode, null, string.Empty, null);
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x0003D168 File Offset: 0x0003B368
		public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode updateMode)
		{
			return this.Add(propertyName, dataSource, dataMember, formattingEnabled, updateMode, null, string.Empty, null);
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x0003D18C File Offset: 0x0003B38C
		public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode updateMode, object nullValue)
		{
			return this.Add(propertyName, dataSource, dataMember, formattingEnabled, updateMode, nullValue, string.Empty, null);
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x0003D1B0 File Offset: 0x0003B3B0
		public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode updateMode, object nullValue, string formatString)
		{
			return this.Add(propertyName, dataSource, dataMember, formattingEnabled, updateMode, nullValue, formatString, null);
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0003D1D0 File Offset: 0x0003B3D0
		public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode updateMode, object nullValue, string formatString, IFormatProvider formatInfo)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			Binding binding = new Binding(propertyName, dataSource, dataMember, formattingEnabled, updateMode, nullValue, formatString, formatInfo);
			this.Add(binding);
			return binding;
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x0003D208 File Offset: 0x0003B408
		protected override void AddCore(Binding dataBinding)
		{
			if (dataBinding == null)
			{
				throw new ArgumentNullException("dataBinding");
			}
			if (dataBinding.BindableComponent == this.control)
			{
				throw new ArgumentException(SR.GetString("BindingsCollectionAdd1"));
			}
			if (dataBinding.BindableComponent != null)
			{
				throw new ArgumentException(SR.GetString("BindingsCollectionAdd2"));
			}
			dataBinding.SetBindableComponent(this.control);
			base.AddCore(dataBinding);
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0003D26C File Offset: 0x0003B46C
		internal void CheckDuplicates(Binding binding)
		{
			if (binding.PropertyName.Length == 0)
			{
				return;
			}
			for (int i = 0; i < this.Count; i++)
			{
				if (binding != base[i] && base[i].PropertyName.Length > 0 && string.Compare(binding.PropertyName, base[i].PropertyName, false, CultureInfo.InvariantCulture) == 0)
				{
					throw new ArgumentException(SR.GetString("BindingsCollectionDup"), "binding");
				}
			}
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x0003D2EA File Offset: 0x0003B4EA
		public new void Clear()
		{
			base.Clear();
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0003D2F4 File Offset: 0x0003B4F4
		protected override void ClearCore()
		{
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				Binding binding = base[i];
				binding.SetBindableComponent(null);
			}
			base.ClearCore();
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x0003D329 File Offset: 0x0003B529
		// (set) Token: 0x06001319 RID: 4889 RVA: 0x0003D331 File Offset: 0x0003B531
		public DataSourceUpdateMode DefaultDataSourceUpdateMode
		{
			get
			{
				return this.defaultDataSourceUpdateMode;
			}
			set
			{
				this.defaultDataSourceUpdateMode = value;
			}
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0003D33A File Offset: 0x0003B53A
		public new void Remove(Binding binding)
		{
			base.Remove(binding);
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x0003D343 File Offset: 0x0003B543
		public new void RemoveAt(int index)
		{
			base.RemoveAt(index);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0003D34C File Offset: 0x0003B54C
		protected override void RemoveCore(Binding dataBinding)
		{
			if (dataBinding.BindableComponent != this.control)
			{
				throw new ArgumentException(SR.GetString("BindingsCollectionForeign"));
			}
			dataBinding.SetBindableComponent(null);
			base.RemoveCore(dataBinding);
		}

		// Token: 0x0400090E RID: 2318
		internal IBindableComponent control;

		// Token: 0x0400090F RID: 2319
		private DataSourceUpdateMode defaultDataSourceUpdateMode;
	}
}
