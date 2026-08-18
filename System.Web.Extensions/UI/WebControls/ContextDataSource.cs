using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200008C RID: 140
	public abstract class ContextDataSource : QueryableDataSource
	{
		// Token: 0x060005FA RID: 1530 RVA: 0x0001AFD2 File Offset: 0x000191D2
		internal ContextDataSource(IPage page) : base(page)
		{
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001AFDB File Offset: 0x000191DB
		internal ContextDataSource(ContextDataSourceView view) : base(view)
		{
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001AFE4 File Offset: 0x000191E4
		protected ContextDataSource()
		{
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0001AFEC File Offset: 0x000191EC
		private ContextDataSourceView View
		{
			get
			{
				if (this._view == null)
				{
					this._view = (ContextDataSourceView)this.GetView("DefaultView");
				}
				return this._view;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0001B012 File Offset: 0x00019212
		// (set) Token: 0x060005FF RID: 1535 RVA: 0x0001B01F File Offset: 0x0001921F
		public virtual string ContextTypeName
		{
			get
			{
				return this.View.ContextTypeName;
			}
			set
			{
				this.View.ContextTypeName = value;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0001B02D File Offset: 0x0001922D
		// (set) Token: 0x06000601 RID: 1537 RVA: 0x0001B03A File Offset: 0x0001923A
		protected string EntitySetName
		{
			get
			{
				return this.View.EntitySetName;
			}
			set
			{
				this.View.EntitySetName = value;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0001B048 File Offset: 0x00019248
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x0001B055 File Offset: 0x00019255
		public virtual string EntityTypeName
		{
			get
			{
				return this.View.EntityTypeName;
			}
			set
			{
				this.View.EntityTypeName = value;
			}
		}

		// Token: 0x04000229 RID: 553
		private ContextDataSourceView _view;
	}
}
