using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000F57 RID: 3927
	public class SingleTemplateContainer : Panel, INamingContainer
	{
		// Token: 0x060095B8 RID: 38328 RVA: 0x00216E40 File Offset: 0x00215040
		internal SingleTemplateContainer(Control parentRadControl)
		{
			this._parentRadControl = parentRadControl;
		}

		// Token: 0x060095B9 RID: 38329 RVA: 0x00216E4F File Offset: 0x0021504F
		private SingleTemplateContainer()
		{
		}

		// Token: 0x060095BA RID: 38330 RVA: 0x00216E57 File Offset: 0x00215057
		public override bool HasControls()
		{
			return base.Controls.Count > 0;
		}

		// Token: 0x17002F5A RID: 12122
		// (get) Token: 0x060095BB RID: 38331 RVA: 0x00216E67 File Offset: 0x00215067
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		// Token: 0x060095BC RID: 38332 RVA: 0x00216E75 File Offset: 0x00215075
		protected override void OnInit(EventArgs e)
		{
			this.InstantiateTemplate();
			base.OnInit(e);
		}

		// Token: 0x060095BD RID: 38333 RVA: 0x00216E84 File Offset: 0x00215084
		protected override void CreateChildControls()
		{
			this.InstantiateTemplate();
		}

		// Token: 0x17002F5B RID: 12123
		// (get) Token: 0x060095BE RID: 38334 RVA: 0x00216E8C File Offset: 0x0021508C
		// (set) Token: 0x060095BF RID: 38335 RVA: 0x00216E94 File Offset: 0x00215094
		internal ITemplate Template
		{
			get
			{
				return this._template;
			}
			set
			{
				if (this._templateInstantiated && !base.DesignMode)
				{
					throw new InvalidOperationException(string.Format("A template of {0} with ID='{1}' cannot be set after the it has been instantiated or its template container has been created.", this._parentRadControl.GetType().Name, this._parentRadControl.ID));
				}
				this._template = value;
				this.InstantiateTemplate();
			}
		}

		// Token: 0x060095C0 RID: 38336 RVA: 0x00216EE9 File Offset: 0x002150E9
		protected virtual void InstantiateTemplate()
		{
			if (!this._templateInstantiating && !this._templateInstantiated)
			{
				this._templateInstantiating = true;
				if (this._template != null)
				{
					this._template.InstantiateIn(this);
					this._templateInstantiated = true;
				}
				this._templateInstantiating = false;
			}
		}

		// Token: 0x04002ACE RID: 10958
		private ITemplate _template;

		// Token: 0x04002ACF RID: 10959
		private Control _parentRadControl;

		// Token: 0x04002AD0 RID: 10960
		private bool _templateInstantiated;

		// Token: 0x04002AD1 RID: 10961
		private bool _templateInstantiating;
	}
}
