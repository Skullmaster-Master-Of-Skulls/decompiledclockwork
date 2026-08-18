using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.Editor;

namespace Telerik.Web.UI
{
	// Token: 0x0200108E RID: 4238
	public class EditorModule : StateManager, IAttributeAccessor
	{
		// Token: 0x0600AC51 RID: 44113 RVA: 0x00250213 File Offset: 0x0024E413
		public EditorModule()
		{
		}

		// Token: 0x0600AC52 RID: 44114 RVA: 0x0025021B File Offset: 0x0024E41B
		public EditorModule(string name, string scriptFile) : this(name, scriptFile, true, true)
		{
		}

		// Token: 0x0600AC53 RID: 44115 RVA: 0x00250227 File Offset: 0x0024E427
		public EditorModule(string name, string scriptFile, bool visible, bool enabled)
		{
			this.Name = name;
			this.ScriptFile = scriptFile;
			this.Visible = visible;
			this.Enabled = enabled;
		}

		// Token: 0x170037B3 RID: 14259
		// (get) Token: 0x0600AC54 RID: 44116 RVA: 0x0025024C File Offset: 0x0024E44C
		// (set) Token: 0x0600AC55 RID: 44117 RVA: 0x0025027B File Offset: 0x0024E47B
		[DefaultValue("")]
		public string ScriptFile
		{
			get
			{
				if (base.ViewState["ScriptFile"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["ScriptFile"];
			}
			set
			{
				base.ViewState["ScriptFile"] = value;
			}
		}

		// Token: 0x170037B4 RID: 14260
		// (get) Token: 0x0600AC56 RID: 44118 RVA: 0x0025028E File Offset: 0x0024E48E
		// (set) Token: 0x0600AC57 RID: 44119 RVA: 0x002502BD File Offset: 0x0024E4BD
		[TypeConverter("Telerik.Web.Design.EditorModuleNameTypeConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
		public string Name
		{
			get
			{
				if (base.ViewState["Name"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["Name"];
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}

		// Token: 0x170037B5 RID: 14261
		// (get) Token: 0x0600AC58 RID: 44120 RVA: 0x002502D0 File Offset: 0x0024E4D0
		// (set) Token: 0x0600AC59 RID: 44121 RVA: 0x002502FB File Offset: 0x0024E4FB
		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				return base.ViewState["Enabled"] == null || (bool)base.ViewState["Enabled"];
			}
			set
			{
				base.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x170037B6 RID: 14262
		// (get) Token: 0x0600AC5A RID: 44122 RVA: 0x00250313 File Offset: 0x0024E513
		// (set) Token: 0x0600AC5B RID: 44123 RVA: 0x0025033E File Offset: 0x0024E53E
		[DefaultValue(true)]
		public bool Visible
		{
			get
			{
				return base.ViewState["Visible"] == null || (bool)base.ViewState["Visible"];
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x170037B7 RID: 14263
		// (get) Token: 0x0600AC5C RID: 44124 RVA: 0x00250356 File Offset: 0x0024E556
		public virtual Telerik.Web.UI.Editor.AttributeCollection Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new Telerik.Web.UI.Editor.AttributeCollection(this.AttributeState);
				}
				return this._attributes;
			}
		}

		// Token: 0x0600AC5D RID: 44125 RVA: 0x00250377 File Offset: 0x0024E577
		protected override object SaveViewState()
		{
			return new Pair(base.SaveViewState(), (this._attributeState == null) ? null : ((IStateManager)this._attributeState).SaveViewState());
		}

		// Token: 0x0600AC5E RID: 44126 RVA: 0x0025039C File Offset: 0x0024E59C
		protected override void LoadViewState(object state)
		{
			Pair pair = (Pair)state;
			base.LoadViewState(pair.First);
			if (pair.Second != null)
			{
				((IStateManager)this.AttributeState).LoadViewState(pair.Second);
			}
		}

		// Token: 0x0600AC5F RID: 44127 RVA: 0x002503D5 File Offset: 0x0024E5D5
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._attributeState != null)
			{
				((IStateManager)this._attributeState).TrackViewState();
			}
		}

		// Token: 0x0600AC60 RID: 44128 RVA: 0x002503F0 File Offset: 0x0024E5F0
		internal override void SetDirty()
		{
			base.SetDirty();
			if (this._attributeState != null)
			{
				this._attributeState.SetDirty(true);
			}
		}

		// Token: 0x0600AC61 RID: 44129 RVA: 0x0025040C File Offset: 0x0024E60C
		string IAttributeAccessor.GetAttribute(string key)
		{
			if (this._attributeState == null)
			{
				return null;
			}
			return this.Attributes[key];
		}

		// Token: 0x0600AC62 RID: 44130 RVA: 0x00250424 File Offset: 0x0024E624
		void IAttributeAccessor.SetAttribute(string key, string value)
		{
			this.Attributes[key] = value;
		}

		// Token: 0x170037B8 RID: 14264
		// (get) Token: 0x0600AC63 RID: 44131 RVA: 0x00250433 File Offset: 0x0024E633
		private StateBag AttributeState
		{
			get
			{
				if (this._attributeState == null)
				{
					this._attributeState = new StateBag(true);
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._attributeState).TrackViewState();
					}
				}
				return this._attributeState;
			}
		}

		// Token: 0x04002DBE RID: 11710
		private Telerik.Web.UI.Editor.AttributeCollection _attributes;

		// Token: 0x04002DBF RID: 11711
		private StateBag _attributeState;
	}
}
