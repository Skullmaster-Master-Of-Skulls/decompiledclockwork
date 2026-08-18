using System;
using System.Collections;
using System.ComponentModel;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200037C RID: 892
	[Designer("System.Web.UI.Design.WebControls.BaseDataBoundControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("DataSourceID")]
	public abstract class BaseDataBoundControl : WebControl
	{
		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x0600291C RID: 10524 RVA: 0x000852FE File Offset: 0x000834FE
		// (set) Token: 0x0600291D RID: 10525 RVA: 0x00085306 File Offset: 0x00083506
		[Bindable(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Themeable(false)]
		[WebCategory("Data")]
		[WebSysDescription("BaseDataBoundControl_DataSource")]
		public virtual object DataSource
		{
			get
			{
				return this._dataSource;
			}
			set
			{
				if (value != null)
				{
					this.ValidateDataSource(value);
				}
				this._dataSource = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x0600291E RID: 10526 RVA: 0x00085320 File Offset: 0x00083520
		// (set) Token: 0x0600291F RID: 10527 RVA: 0x0008534D File Offset: 0x0008354D
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Data")]
		[WebSysDescription("BaseDataBoundControl_DataSourceID")]
		public virtual string DataSourceID
		{
			get
			{
				object obj = this.ViewState["DataSourceID"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(this.DataSourceID))
				{
					this._requiresBindToNull = true;
				}
				this.ViewState["DataSourceID"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06002920 RID: 10528 RVA: 0x00085382 File Offset: 0x00083582
		protected bool Initialized
		{
			get
			{
				return this._inited;
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06002921 RID: 10529 RVA: 0x00007722 File Offset: 0x00005922
		protected virtual bool IsUsingModelBinders
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06002922 RID: 10530 RVA: 0x0008538A File Offset: 0x0008358A
		protected bool IsBoundUsingDataSourceID
		{
			get
			{
				return this.DataSourceID.Length > 0;
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06002923 RID: 10531 RVA: 0x0008539A File Offset: 0x0008359A
		protected internal bool IsDataBindingAutomatic
		{
			get
			{
				return this.IsBoundUsingDataSourceID || this.IsUsingModelBinders;
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06002924 RID: 10532 RVA: 0x000853AC File Offset: 0x000835AC
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return this.RenderingCompatibility < VersionUtil.Framework40;
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06002925 RID: 10533 RVA: 0x000853BE File Offset: 0x000835BE
		// (set) Token: 0x06002926 RID: 10534 RVA: 0x000853C6 File Offset: 0x000835C6
		protected bool RequiresDataBinding
		{
			get
			{
				return this._requiresDataBinding;
			}
			set
			{
				if (value && this._preRendered && this.IsDataBindingAutomatic && this.Page != null && !this.Page.IsCallback)
				{
					this._requiresDataBinding = true;
					this.EnsureDataBound();
					return;
				}
				this._requiresDataBinding = value;
			}
		}

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06002927 RID: 10535 RVA: 0x00085405 File Offset: 0x00083605
		// (remove) Token: 0x06002928 RID: 10536 RVA: 0x00085418 File Offset: 0x00083618
		[WebCategory("Data")]
		[WebSysDescription("BaseDataBoundControl_OnDataBound")]
		public event EventHandler DataBound
		{
			add
			{
				base.Events.AddHandler(BaseDataBoundControl.EventDataBound, value);
			}
			remove
			{
				base.Events.RemoveHandler(BaseDataBoundControl.EventDataBound, value);
			}
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x0008542B File Offset: 0x0008362B
		protected void ConfirmInitState()
		{
			this._inited = true;
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x00085434 File Offset: 0x00083634
		public override void DataBind()
		{
			if (base.DesignMode)
			{
				IDictionary designModeState = this.GetDesignModeState();
				if ((designModeState == null || designModeState["EnableDesignTimeDataBinding"] == null) && base.Site == null)
				{
					return;
				}
			}
			this.PerformSelect();
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x00085470 File Offset: 0x00083670
		protected virtual void EnsureDataBound()
		{
			try
			{
				this._throwOnDataPropertyChange = true;
				if (this.RequiresDataBinding && (this.IsDataBindingAutomatic || this._requiresBindToNull))
				{
					this.DataBind();
					this._requiresBindToNull = false;
				}
			}
			finally
			{
				this._throwOnDataPropertyChange = false;
			}
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x000854C4 File Offset: 0x000836C4
		internal void InternalEnsureDataBound()
		{
			this.EnsureDataBound();
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x000854CC File Offset: 0x000836CC
		protected virtual void OnDataBound(EventArgs e)
		{
			EventHandler eventHandler = base.Events[BaseDataBoundControl.EventDataBound] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x000854FA File Offset: 0x000836FA
		protected virtual void OnDataPropertyChanged()
		{
			if (this._throwOnDataPropertyChange)
			{
				throw new HttpException(SR.GetString("DataBoundControl_InvalidDataPropertyChange", new object[]
				{
					this.ID
				}));
			}
			if (this._inited)
			{
				this.RequiresDataBinding = true;
			}
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x00085534 File Offset: 0x00083734
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.Page != null)
			{
				this.Page.PreLoad += this.OnPagePreLoad;
				if (!base.IsViewStateEnabled && this.Page.IsPostBack)
				{
					this.RequiresDataBinding = true;
				}
			}
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x00085584 File Offset: 0x00083784
		protected virtual void OnPagePreLoad(object sender, EventArgs e)
		{
			this._inited = true;
			if (this.Page != null)
			{
				this.Page.PreLoad -= this.OnPagePreLoad;
			}
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x000855AD File Offset: 0x000837AD
		protected internal override void OnPreRender(EventArgs e)
		{
			this._preRendered = true;
			this.EnsureDataBound();
			base.OnPreRender(e);
		}

		// Token: 0x06002932 RID: 10546
		protected abstract void PerformSelect();

		// Token: 0x06002933 RID: 10547
		protected abstract void ValidateDataSource(object dataSource);

		// Token: 0x04001E47 RID: 7751
		private static readonly object EventDataBound = new object();

		// Token: 0x04001E48 RID: 7752
		private object _dataSource;

		// Token: 0x04001E49 RID: 7753
		private bool _requiresDataBinding;

		// Token: 0x04001E4A RID: 7754
		private bool _inited;

		// Token: 0x04001E4B RID: 7755
		private bool _preRendered;

		// Token: 0x04001E4C RID: 7756
		private bool _requiresBindToNull;

		// Token: 0x04001E4D RID: 7757
		private bool _throwOnDataPropertyChange;
	}
}
