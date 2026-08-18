using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.Editor;

namespace Telerik.Web.UI
{
	// Token: 0x020012A4 RID: 4772
	[ParseChildren(true, "Tools")]
	public class EditorToolGroup : StateManager, IAttributeAccessor
	{
		// Token: 0x17004096 RID: 16534
		// (get) Token: 0x0600C7E7 RID: 51175 RVA: 0x002C8B78 File Offset: 0x002C6D78
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

		// Token: 0x17004097 RID: 16535
		// (get) Token: 0x0600C7E8 RID: 51176 RVA: 0x002C8BA7 File Offset: 0x002C6DA7
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

		// Token: 0x0600C7E9 RID: 51177 RVA: 0x002C8BC8 File Offset: 0x002C6DC8
		string IAttributeAccessor.GetAttribute(string key)
		{
			if (this._attributeState == null)
			{
				return null;
			}
			return this.Attributes[key];
		}

		// Token: 0x0600C7EA RID: 51178 RVA: 0x002C8BE0 File Offset: 0x002C6DE0
		void IAttributeAccessor.SetAttribute(string key, string value)
		{
			this.Attributes[key] = value;
		}

		// Token: 0x0600C7EB RID: 51179 RVA: 0x002C8BF0 File Offset: 0x002C6DF0
		public List<EditorToolBase> GetAllTools()
		{
			List<EditorToolBase> list = new List<EditorToolBase>();
			foreach (object obj in this.Tools)
			{
				EditorToolBase editorToolBase = (EditorToolBase)obj;
				list.Add(editorToolBase);
				EditorToolStrip editorToolStrip = editorToolBase as EditorToolStrip;
				if (editorToolStrip != null)
				{
					foreach (object obj2 in editorToolStrip.Tools)
					{
						EditorToolBase item = (EditorToolBase)obj2;
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x0600C7EC RID: 51180 RVA: 0x002C8CB0 File Offset: 0x002C6EB0
		public EditorTool FindTool(string name)
		{
			List<EditorToolBase> allTools = this.GetAllTools();
			foreach (EditorToolBase editorToolBase in allTools)
			{
				EditorTool editorTool = editorToolBase as EditorTool;
				if (editorTool != null && name.Equals(editorTool.Name, StringComparison.OrdinalIgnoreCase))
				{
					return editorTool;
				}
			}
			return null;
		}

		// Token: 0x0600C7ED RID: 51181 RVA: 0x002C8D20 File Offset: 0x002C6F20
		public bool Contains(string name)
		{
			return this.FindTool(name) != null;
		}

		// Token: 0x17004098 RID: 16536
		// (get) Token: 0x0600C7EE RID: 51182 RVA: 0x002C8D2F File Offset: 0x002C6F2F
		// (set) Token: 0x0600C7EF RID: 51183 RVA: 0x002C8D5E File Offset: 0x002C6F5E
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public virtual string Tag
		{
			get
			{
				if (base.ViewState["Tag"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["Tag"];
			}
			set
			{
				base.ViewState["Tag"] = value;
			}
		}

		// Token: 0x17004099 RID: 16537
		// (get) Token: 0x0600C7F0 RID: 51184 RVA: 0x002C8D71 File Offset: 0x002C6F71
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public EditorToolBaseCollection Tools
		{
			get
			{
				if (this._tools == null)
				{
					this._tools = new EditorToolBaseCollection();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._tools).TrackViewState();
					}
				}
				return this._tools;
			}
		}

		// Token: 0x1700409A RID: 16538
		// (get) Token: 0x0600C7F1 RID: 51185 RVA: 0x002C8D9F File Offset: 0x002C6F9F
		// (set) Token: 0x0600C7F2 RID: 51186 RVA: 0x002C8DBF File Offset: 0x002C6FBF
		[DefaultValue("")]
		public string Tab
		{
			get
			{
				return (string)(base.ViewState["Tab"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Tab"] = value;
			}
		}

		// Token: 0x1700409B RID: 16539
		// (get) Token: 0x0600C7F3 RID: 51187 RVA: 0x002C8DD2 File Offset: 0x002C6FD2
		// (set) Token: 0x0600C7F4 RID: 51188 RVA: 0x002C8DF2 File Offset: 0x002C6FF2
		[DefaultValue("")]
		public string Context
		{
			get
			{
				return (string)(base.ViewState["Context"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Context"] = value;
			}
		}

		// Token: 0x0600C7F5 RID: 51189 RVA: 0x002C8E08 File Offset: 0x002C7008
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.Tools).LoadViewState(array[1]);
			if (array.Length > 1 && array[2] != null)
			{
				((IStateManager)this.AttributeState).LoadViewState(array[2]);
			}
		}

		// Token: 0x0600C7F6 RID: 51190 RVA: 0x002C8E4C File Offset: 0x002C704C
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Tools).SaveViewState(),
				(this._attributeState == null) ? null : ((IStateManager)this._attributeState).SaveViewState()
			};
		}

		// Token: 0x0600C7F7 RID: 51191 RVA: 0x002C8E93 File Offset: 0x002C7093
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Tools).TrackViewState();
		}

		// Token: 0x0600C7F8 RID: 51192 RVA: 0x002C8EA6 File Offset: 0x002C70A6
		internal override void SetDirty()
		{
			base.SetDirty();
			this.Tools.SetDirty();
			if (this._attributeState != null)
			{
				this._attributeState.SetDirty(true);
			}
		}

		// Token: 0x040034A3 RID: 13475
		private Telerik.Web.UI.Editor.AttributeCollection _attributes;

		// Token: 0x040034A4 RID: 13476
		private StateBag _attributeState;

		// Token: 0x040034A5 RID: 13477
		private EditorToolBaseCollection _tools;
	}
}
