using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200039E RID: 926
	public abstract class CompositeDataBoundControl : DataBoundControl, INamingContainer
	{
		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06002C44 RID: 11332 RVA: 0x0009073B File Offset: 0x0008E93B
		protected override bool IsUsingModelBinders
		{
			get
			{
				return !string.IsNullOrEmpty(this.SelectMethod) || !string.IsNullOrEmpty(this.UpdateMethod) || !string.IsNullOrEmpty(this.DeleteMethod) || !string.IsNullOrEmpty(this.InsertMethod);
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06002C45 RID: 11333 RVA: 0x00090774 File Offset: 0x0008E974
		// (set) Token: 0x06002C46 RID: 11334 RVA: 0x00090785 File Offset: 0x0008E985
		protected internal string UpdateMethod
		{
			get
			{
				return this._updateMethod ?? string.Empty;
			}
			set
			{
				if (!string.Equals(this._updateMethod, value, StringComparison.OrdinalIgnoreCase))
				{
					this._updateMethod = value;
					this.OnDataPropertyChanged();
				}
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06002C47 RID: 11335 RVA: 0x000907A3 File Offset: 0x0008E9A3
		// (set) Token: 0x06002C48 RID: 11336 RVA: 0x000907B4 File Offset: 0x0008E9B4
		protected internal string DeleteMethod
		{
			get
			{
				return this._deleteMethod ?? string.Empty;
			}
			set
			{
				if (!string.Equals(this._deleteMethod, value, StringComparison.OrdinalIgnoreCase))
				{
					this._deleteMethod = value;
					this.OnDataPropertyChanged();
				}
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06002C49 RID: 11337 RVA: 0x000907D2 File Offset: 0x0008E9D2
		// (set) Token: 0x06002C4A RID: 11338 RVA: 0x000907E3 File Offset: 0x0008E9E3
		protected internal string InsertMethod
		{
			get
			{
				return this._insertMethod ?? string.Empty;
			}
			set
			{
				if (!string.Equals(this._insertMethod, value, StringComparison.OrdinalIgnoreCase))
				{
					this._insertMethod = value;
					this.OnDataPropertyChanged();
				}
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06002C4B RID: 11339 RVA: 0x000856CA File Offset: 0x000838CA
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x00090804 File Offset: 0x0008EA04
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			object obj = this.ViewState["_!ItemCount"];
			if (obj == null && base.RequiresDataBinding)
			{
				this.EnsureDataBound();
			}
			if (obj != null && (int)obj != -1)
			{
				DummyDataSource dataSource = new DummyDataSource((int)obj);
				this.CreateChildControls(dataSource, false);
				base.ClearChildViewState();
			}
		}

		// Token: 0x06002C4D RID: 11341
		protected abstract int CreateChildControls(IEnumerable dataSource, bool dataBinding);

		// Token: 0x06002C4E RID: 11342 RVA: 0x00090868 File Offset: 0x0008EA68
		protected internal override void PerformDataBinding(IEnumerable data)
		{
			base.PerformDataBinding(data);
			this.Controls.Clear();
			base.ClearChildViewState();
			this.TrackViewState();
			int num = this.CreateChildControls(data, true);
			base.ChildControlsCreated = true;
			this.ViewState["_!ItemCount"] = num;
		}

		// Token: 0x04001F2C RID: 7980
		internal const string ItemCountViewStateKey = "_!ItemCount";

		// Token: 0x04001F2D RID: 7981
		private string _updateMethod;

		// Token: 0x04001F2E RID: 7982
		private string _insertMethod;

		// Token: 0x04001F2F RID: 7983
		private string _deleteMethod;
	}
}
