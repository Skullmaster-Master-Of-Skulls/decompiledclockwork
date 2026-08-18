using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004FA RID: 1274
	[ParseChildren(true, "ChildNodes")]
	public class TreeNode : IStateManager, ICloneable
	{
		// Token: 0x06003F61 RID: 16225 RVA: 0x000CBA8E File Offset: 0x000C9C8E
		public TreeNode()
		{
			this._selectDesired = 0;
		}

		// Token: 0x06003F62 RID: 16226 RVA: 0x000CBAA5 File Offset: 0x000C9CA5
		protected internal TreeNode(TreeView owner, bool isRoot) : this()
		{
			this._owner = owner;
			this._isRoot = isRoot;
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x000CBABB File Offset: 0x000C9CBB
		public TreeNode(string text) : this(text, null, null, null, null)
		{
		}

		// Token: 0x06003F64 RID: 16228 RVA: 0x000CBAC8 File Offset: 0x000C9CC8
		public TreeNode(string text, string value) : this(text, value, null, null, null)
		{
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x000CBAD5 File Offset: 0x000C9CD5
		public TreeNode(string text, string value, string imageUrl) : this(text, value, imageUrl, null, null)
		{
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x000CBAE4 File Offset: 0x000C9CE4
		public TreeNode(string text, string value, string imageUrl, string navigateUrl, string target) : this()
		{
			if (text != null)
			{
				this.Text = text;
			}
			if (value != null)
			{
				this.Value = value;
			}
			if (!string.IsNullOrEmpty(imageUrl))
			{
				this.ImageUrl = imageUrl;
			}
			if (!string.IsNullOrEmpty(navigateUrl))
			{
				this.NavigateUrl = navigateUrl;
			}
			if (!string.IsNullOrEmpty(target))
			{
				this.Target = target;
			}
		}

		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x06003F67 RID: 16231 RVA: 0x000CBB3C File Offset: 0x000C9D3C
		// (set) Token: 0x06003F68 RID: 16232 RVA: 0x000CBB65 File Offset: 0x000C9D65
		[DefaultValue(false)]
		[WebSysDescription("TreeNode_Checked")]
		public bool Checked
		{
			get
			{
				object obj = this.ViewState["Checked"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["Checked"] = value;
				this.NotifyOwnerChecked();
			}
		}

		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x06003F69 RID: 16233 RVA: 0x000CBB83 File Offset: 0x000C9D83
		internal bool CheckedSet
		{
			get
			{
				return this.ViewState["Checked"] != null;
			}
		}

		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x06003F6A RID: 16234 RVA: 0x000CBB98 File Offset: 0x000C9D98
		[Browsable(false)]
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool DataBound
		{
			get
			{
				object obj = this.ViewState["DataBound"];
				return obj != null && (bool)obj;
			}
		}

		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x06003F6B RID: 16235 RVA: 0x000CBBC1 File Offset: 0x000C9DC1
		[Browsable(false)]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public TreeNodeCollection ChildNodes
		{
			get
			{
				if (this._childNodes == null)
				{
					this._childNodes = new TreeNodeCollection(this);
				}
				return this._childNodes;
			}
		}

		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x06003F6C RID: 16236 RVA: 0x000CBBE0 File Offset: 0x000C9DE0
		[Browsable(false)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DataPath
		{
			get
			{
				string text = (string)this.ViewState["DataPath"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
		}

		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x06003F6D RID: 16237 RVA: 0x000CBC10 File Offset: 0x000C9E10
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Depth
		{
			get
			{
				if (this._depth == -2)
				{
					if (this._isRoot)
					{
						return -1;
					}
					if (this.Parent != null)
					{
						this._depth = this.Parent.Depth + 1;
					}
					else
					{
						if (this._owner == null)
						{
							return 0;
						}
						this._depth = this.InternalValuePath.Split(new char[]
						{
							'\\'
						}).Length - 1;
					}
				}
				return this._depth;
			}
		}

		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x06003F6E RID: 16238 RVA: 0x000CBC80 File Offset: 0x000C9E80
		// (set) Token: 0x06003F6F RID: 16239 RVA: 0x000CBCB4 File Offset: 0x000C9EB4
		[DefaultValue(typeof(bool?), "")]
		[WebSysDescription("TreeNode_Expanded")]
		public bool? Expanded
		{
			get
			{
				object obj = this.ViewState["Expanded"];
				if (obj == null)
				{
					return null;
				}
				return (bool?)obj;
			}
			set
			{
				bool? expanded = this.Expanded;
				this.ViewState["Expanded"] = value;
				bool? flag = value;
				bool? flag2 = expanded;
				if (!(flag.GetValueOrDefault() == flag2.GetValueOrDefault() & flag != null == (flag2 != null)))
				{
					if (this._owner != null && this._owner.DesignMode)
					{
						return;
					}
					flag2 = value;
					bool flag3 = true;
					if (flag2.GetValueOrDefault() == flag3 & flag2 != null)
					{
						if (this.PopulateOnDemand)
						{
							if (this._owner == null)
							{
								this._populateDesired = true;
							}
							else if (!this._owner.LoadingNodeState)
							{
								this.Populate();
							}
						}
						if (this._owner != null)
						{
							this._owner.RaiseTreeNodeExpanded(this);
							return;
						}
					}
					else
					{
						flag2 = value;
						flag3 = false;
						if (flag2.GetValueOrDefault() == flag3 & flag2 != null)
						{
							flag2 = expanded;
							flag3 = true;
							if ((flag2.GetValueOrDefault() == flag3 & flag2 != null) && this.ChildNodes.Count > 0 && this._owner != null)
							{
								this._owner.RaiseTreeNodeCollapsed(this);
							}
						}
					}
				}
			}
		}

		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x06003F70 RID: 16240 RVA: 0x000CBDC9 File Offset: 0x000C9FC9
		[Browsable(false)]
		[DefaultValue(null)]
		public object DataItem
		{
			get
			{
				return this._dataItem;
			}
		}

		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x06003F71 RID: 16241 RVA: 0x000CBDD4 File Offset: 0x000C9FD4
		// (set) Token: 0x06003F72 RID: 16242 RVA: 0x000CBE01 File Offset: 0x000CA001
		[DefaultValue("")]
		[Localizable(true)]
		[WebSysDescription("TreeNode_ImageToolTip")]
		public string ImageToolTip
		{
			get
			{
				string text = (string)this.ViewState["ImageToolTip"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["ImageToolTip"] = value;
			}
		}

		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x06003F73 RID: 16243 RVA: 0x000CBE14 File Offset: 0x000CA014
		// (set) Token: 0x06003F74 RID: 16244 RVA: 0x000CBE41 File Offset: 0x000CA041
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("TreeNode_ImageUrl")]
		public string ImageUrl
		{
			get
			{
				string text = (string)this.ViewState["ImageUrl"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x06003F75 RID: 16245 RVA: 0x000CBE54 File Offset: 0x000CA054
		// (set) Token: 0x06003F76 RID: 16246 RVA: 0x000CBE5C File Offset: 0x000CA05C
		internal int Index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x06003F77 RID: 16247 RVA: 0x000CBE68 File Offset: 0x000CA068
		internal string InternalValuePath
		{
			get
			{
				if (this._internalValuePath != null)
				{
					return this._internalValuePath;
				}
				if (this._parent != null)
				{
					List<string> list = new List<string>();
					list.Add(TreeView.Escape(this.Value));
					TreeNode parent = this._parent;
					while (parent != null && !parent._isRoot)
					{
						if (parent._internalValuePath != null)
						{
							list.Add(parent._internalValuePath);
							break;
						}
						list.Add(TreeView.Escape(parent.Value));
						parent = parent._parent;
					}
					list.Reverse();
					this._internalValuePath = string.Join('\\'.ToString(), list.ToArray());
					return this._internalValuePath;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x06003F78 RID: 16248 RVA: 0x000CBF18 File Offset: 0x000CA118
		// (set) Token: 0x06003F79 RID: 16249 RVA: 0x000CBF45 File Offset: 0x000CA145
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("TreeNode_NavigateUrl")]
		public string NavigateUrl
		{
			get
			{
				string text = (string)this.ViewState["NavigateUrl"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x06003F7A RID: 16250 RVA: 0x000CBF58 File Offset: 0x000CA158
		internal TreeView Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x1700128F RID: 4751
		// (get) Token: 0x06003F7B RID: 16251 RVA: 0x000CBF60 File Offset: 0x000CA160
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TreeNode Parent
		{
			get
			{
				if (this._parent == null || this._parent._isRoot)
				{
					return null;
				}
				return this._parent;
			}
		}

		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x06003F7C RID: 16252 RVA: 0x000CBF80 File Offset: 0x000CA180
		// (set) Token: 0x06003F7D RID: 16253 RVA: 0x000CBFA9 File Offset: 0x000CA1A9
		internal bool Populated
		{
			get
			{
				object obj = this.ViewState["Populated"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["Populated"] = value;
			}
		}

		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x06003F7E RID: 16254 RVA: 0x000CBFC4 File Offset: 0x000CA1C4
		// (set) Token: 0x06003F7F RID: 16255 RVA: 0x000CBFF0 File Offset: 0x000CA1F0
		[DefaultValue(false)]
		[WebSysDescription("TreeNode_PopulateOnDemand")]
		public bool PopulateOnDemand
		{
			get
			{
				object obj = this.ViewState["PopulateOnDemand"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["PopulateOnDemand"] = value;
				if (value)
				{
					bool? expanded = this.Expanded;
					bool flag = true;
					if (expanded.GetValueOrDefault() == flag & expanded != null)
					{
						this.Expanded = null;
					}
				}
			}
		}

		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x06003F80 RID: 16256 RVA: 0x000CC044 File Offset: 0x000CA244
		// (set) Token: 0x06003F81 RID: 16257 RVA: 0x000CC06D File Offset: 0x000CA26D
		[DefaultValue(false)]
		internal bool PreserveChecked
		{
			get
			{
				object obj = this.ViewState["PreserveChecked"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["PreserveChecked"] = value;
			}
		}

		// Token: 0x17001293 RID: 4755
		// (get) Token: 0x06003F82 RID: 16258 RVA: 0x000CC088 File Offset: 0x000CA288
		// (set) Token: 0x06003F83 RID: 16259 RVA: 0x000CC0B1 File Offset: 0x000CA2B1
		[DefaultValue(TreeNodeSelectAction.Select)]
		[WebSysDescription("TreeNode_SelectAction")]
		public TreeNodeSelectAction SelectAction
		{
			get
			{
				object obj = this.ViewState["SelectAction"];
				if (obj == null)
				{
					return TreeNodeSelectAction.Select;
				}
				return (TreeNodeSelectAction)obj;
			}
			set
			{
				this.ViewState["SelectAction"] = value;
			}
		}

		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x06003F84 RID: 16260 RVA: 0x000CC0CC File Offset: 0x000CA2CC
		// (set) Token: 0x06003F85 RID: 16261 RVA: 0x000CC0F8 File Offset: 0x000CA2F8
		[DefaultValue(false)]
		[WebSysDescription("TreeNode_Selected")]
		public bool Selected
		{
			get
			{
				object obj = this.ViewState["Selected"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.SetSelected(value);
				if (this._owner == null)
				{
					this._selectDesired = (value ? 1 : -1);
					return;
				}
				if (value)
				{
					this._owner.SetSelectedNode(this);
					return;
				}
				if (this == this._owner.SelectedNode)
				{
					this._owner.SetSelectedNode(null);
				}
			}
		}

		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x06003F86 RID: 16262 RVA: 0x000CC14C File Offset: 0x000CA34C
		internal string SelectID
		{
			get
			{
				if (this._owner.ShowExpandCollapse)
				{
					return this._owner.CreateNodeTextId(this.Index);
				}
				return this._owner.CreateNodeId(this.Index);
			}
		}

		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x06003F87 RID: 16263 RVA: 0x000CC180 File Offset: 0x000CA380
		// (set) Token: 0x06003F88 RID: 16264 RVA: 0x000CC1B1 File Offset: 0x000CA3B1
		[DefaultValue(typeof(bool?), "")]
		[WebSysDescription("TreeNode_ShowCheckBox")]
		public bool? ShowCheckBox
		{
			get
			{
				object obj = this.ViewState["ShowCheckBox"];
				if (obj == null)
				{
					return null;
				}
				return (bool?)obj;
			}
			set
			{
				this.ViewState["ShowCheckBox"] = value;
			}
		}

		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x06003F89 RID: 16265 RVA: 0x000CC1CC File Offset: 0x000CA3CC
		// (set) Token: 0x06003F8A RID: 16266 RVA: 0x000CC1F9 File Offset: 0x000CA3F9
		[DefaultValue("")]
		[WebSysDescription("TreeNode_Target")]
		public string Target
		{
			get
			{
				string text = (string)this.ViewState["Target"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x06003F8B RID: 16267 RVA: 0x000CC20C File Offset: 0x000CA40C
		// (set) Token: 0x06003F8C RID: 16268 RVA: 0x000CC252 File Offset: 0x000CA452
		[DefaultValue("")]
		[Localizable(true)]
		[WebSysDescription("TreeNode_Text")]
		public string Text
		{
			get
			{
				string text = (string)this.ViewState["Text"];
				if (text == null)
				{
					text = (string)this.ViewState["Value"];
					if (text == null)
					{
						return string.Empty;
					}
				}
				return text;
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x06003F8D RID: 16269 RVA: 0x000CC268 File Offset: 0x000CA468
		// (set) Token: 0x06003F8E RID: 16270 RVA: 0x000CC295 File Offset: 0x000CA495
		[DefaultValue("")]
		[Localizable(true)]
		[WebSysDescription("TreeNode_ToolTip")]
		public string ToolTip
		{
			get
			{
				string text = (string)this.ViewState["ToolTip"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x06003F8F RID: 16271 RVA: 0x000CC2A8 File Offset: 0x000CA4A8
		// (set) Token: 0x06003F90 RID: 16272 RVA: 0x000CC2EE File Offset: 0x000CA4EE
		[DefaultValue("")]
		[Localizable(true)]
		[WebSysDescription("TreeNode_Value")]
		public string Value
		{
			get
			{
				string text = (string)this.ViewState["Value"];
				if (text == null)
				{
					text = (string)this.ViewState["Text"];
					if (text == null)
					{
						return string.Empty;
					}
				}
				return text;
			}
			set
			{
				this.ViewState["Value"] = value;
				this.ResetValuePathRecursive();
			}
		}

		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x06003F91 RID: 16273 RVA: 0x000CC308 File Offset: 0x000CA508
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ValuePath
		{
			get
			{
				if (this._valuePath != null)
				{
					return this._valuePath;
				}
				if (this._parent != null)
				{
					string valuePath = this._parent.ValuePath;
					this._valuePath = ((valuePath.Length == 0 && this._parent.Depth == -1) ? this.Value : (valuePath + this._owner.PathSeparator.ToString() + this.Value));
					return this._valuePath;
				}
				if (this.Owner != null && !string.IsNullOrEmpty(this.InternalValuePath))
				{
					string[] array = this.InternalValuePath.Split(new char[]
					{
						'\\'
					});
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = TreeView.UnEscape(array[i]);
					}
					this._valuePath = string.Join(this.Owner.PathSeparator.ToString(), array);
					return this._valuePath;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x06003F92 RID: 16274 RVA: 0x000CC3F2 File Offset: 0x000CA5F2
		private StateBag ViewState
		{
			get
			{
				if (this._viewState == null)
				{
					this._viewState = new StateBag();
					if (this._isTrackingViewState)
					{
						((IStateManager)this._viewState).TrackViewState();
					}
				}
				return this._viewState;
			}
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x000CC420 File Offset: 0x000CA620
		private void ApplyAttributeList(HtmlTextWriter writer, ArrayList list)
		{
			for (int i = 0; i < list.Count; i += 2)
			{
				object obj = list[i];
				if (obj is string)
				{
					writer.AddAttribute((string)obj, (string)list[i + 1]);
				}
				else
				{
					writer.AddAttribute((HtmlTextWriterAttribute)obj, (string)list[i + 1]);
				}
			}
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x000CC484 File Offset: 0x000CA684
		public void Collapse()
		{
			this.Expanded = new bool?(false);
		}

		// Token: 0x06003F95 RID: 16277 RVA: 0x000CC492 File Offset: 0x000CA692
		public void CollapseAll()
		{
			this.SetExpandedRecursive(false);
		}

		// Token: 0x06003F96 RID: 16278 RVA: 0x000CC49B File Offset: 0x000CA69B
		public void Expand()
		{
			this.Expanded = new bool?(true);
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x000CC4A9 File Offset: 0x000CA6A9
		public void ExpandAll()
		{
			this.SetExpandedRecursive(true);
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x000CC4B2 File Offset: 0x000CA6B2
		internal TreeNode GetParentInternal()
		{
			return this._parent;
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x000CC4BC File Offset: 0x000CA6BC
		private string GetPopulateNodeAttribute(HtmlTextWriter writer, string myId, string selectId, string selectImageId, string lineType, int depth, bool[] isLast)
		{
			string result = string.Empty;
			if (this._parentIsLast == null)
			{
				char[] array = new char[depth + 1];
				for (int i = 0; i < depth + 1; i++)
				{
					if (isLast[i])
					{
						array[i] = 't';
					}
					else
					{
						array[i] = 'f';
					}
				}
				this._parentIsLast = new string(array);
			}
			string text = this.Index.ToString(CultureInfo.InvariantCulture);
			if (this._owner.IsNotIE)
			{
				result = string.Concat(new string[]
				{
					"javascript:TreeView_PopulateNode(",
					this._owner.ClientDataObjectID,
					",",
					text,
					",document.getElementById('",
					myId,
					"'),document.getElementById('",
					selectId,
					"'),",
					(selectImageId.Length == 0) ? "null" : ("document.getElementById('" + selectImageId + "')"),
					",'",
					lineType,
					"','",
					Util.QuoteJScriptString(this.Text, true),
					"','",
					Util.QuoteJScriptString(this.InternalValuePath, true),
					"','",
					(this.DataBound ? 't' : 'f').ToString(),
					"','",
					Util.QuoteJScriptString(this.DataPath, true),
					"','",
					this._parentIsLast,
					"')"
				});
			}
			else
			{
				result = string.Concat(new string[]
				{
					"javascript:TreeView_PopulateNode(",
					this._owner.ClientDataObjectID,
					",",
					text,
					",",
					myId,
					",",
					selectId,
					",",
					(selectImageId.Length == 0) ? "null" : selectImageId,
					",'",
					lineType,
					"','",
					Util.QuoteJScriptString(this.Text, true),
					"','",
					Util.QuoteJScriptString(this.InternalValuePath, true),
					"','",
					(this.DataBound ? 't' : 'f').ToString(),
					"','",
					Util.QuoteJScriptString(this.DataPath, true),
					"','",
					this._parentIsLast,
					"')"
				});
			}
			if (this._owner.Page != null)
			{
				this._owner.Page.ClientScript.RegisterForEventValidation(this._owner.UniqueID, text + this.Text + this.InternalValuePath + this.DataPath);
			}
			return result;
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x000CC78B File Offset: 0x000CA98B
		internal bool GetEffectiveShowCheckBox()
		{
			return this.GetEffectiveShowCheckBox(this.GetTreeNodeType());
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x000CC79C File Offset: 0x000CA99C
		private bool GetEffectiveShowCheckBox(TreeNodeTypes type)
		{
			bool? showCheckBox = this.ShowCheckBox;
			bool flag = true;
			if (showCheckBox.GetValueOrDefault() == flag & showCheckBox != null)
			{
				return true;
			}
			showCheckBox = this.ShowCheckBox;
			flag = false;
			return !(showCheckBox.GetValueOrDefault() == flag & showCheckBox != null) && (this._owner.ShowCheckBoxes & type) > TreeNodeTypes.None;
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x000CC7F8 File Offset: 0x000CA9F8
		private string GetToggleNodeAttributeValue(string myId, string lineType)
		{
			if (this._toggleNodeAttributeValue == null)
			{
				if (this._owner.IsNotIE)
				{
					this._toggleNodeAttributeValue = string.Concat(new string[]
					{
						"javascript:TreeView_ToggleNode(",
						this._owner.ClientDataObjectID,
						",",
						this.Index.ToString(CultureInfo.InvariantCulture),
						",document.getElementById('",
						myId,
						"'),'",
						lineType,
						"',document.getElementById('",
						myId,
						"Nodes'))"
					});
				}
				else
				{
					this._toggleNodeAttributeValue = string.Concat(new string[]
					{
						"javascript:TreeView_ToggleNode(",
						this._owner.ClientDataObjectID,
						",",
						this.Index.ToString(CultureInfo.InvariantCulture),
						",",
						myId,
						",'",
						lineType,
						"',",
						myId,
						"Nodes)"
					});
				}
			}
			return this._toggleNodeAttributeValue;
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x000CC910 File Offset: 0x000CAB10
		private TreeNodeTypes GetTreeNodeType()
		{
			TreeNodeTypes result = TreeNodeTypes.Leaf;
			if (this.Depth == 0 && this.ChildNodes.Count > 0)
			{
				result = TreeNodeTypes.Root;
			}
			else if (this.ChildNodes.Count > 0 || this.PopulateOnDemand)
			{
				result = TreeNodeTypes.Parent;
			}
			return result;
		}

		// Token: 0x06003F9E RID: 16286 RVA: 0x000CC954 File Offset: 0x000CAB54
		private void NotifyOwnerChecked()
		{
			if (this._owner == null)
			{
				this._modifyCheckedNodes = true;
				return;
			}
			object obj = this.ViewState["Checked"];
			if (obj != null && (bool)obj)
			{
				TreeNodeCollection checkedNodes = this._owner.CheckedNodes;
				if (!checkedNodes.Contains(this))
				{
					this._owner.CheckedNodes.Add(this);
					return;
				}
			}
			else
			{
				this._owner.CheckedNodes.Remove(this);
			}
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x000CC9C5 File Offset: 0x000CABC5
		internal void Populate()
		{
			if (!this.Populated && this.ChildNodes.Count == 0)
			{
				if (this._owner != null)
				{
					this._owner.PopulateNode(this);
					return;
				}
				this._populateDesired = true;
			}
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x000CC9F8 File Offset: 0x000CABF8
		internal void Render(HtmlTextWriter writer, int position, bool[] isLast, bool enabled)
		{
			string text = string.Empty;
			text = this._owner.CreateNodeId(this.Index);
			int depth = this.Depth;
			bool flag = false;
			if (depth > -1)
			{
				flag = isLast[depth];
			}
			bool? expanded = this.Expanded;
			bool flag2 = true;
			bool flag3 = expanded.GetValueOrDefault() == flag2 & expanded != null;
			TreeNodeStyle style = this._owner.GetStyle(this);
			writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
			writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0");
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			if (style != null && !style.NodeSpacing.IsEmpty && (depth != 0 || position != 0))
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, style.NodeSpacing.ToString(CultureInfo.InvariantCulture));
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			if (depth > 0)
			{
				for (int i = 0; i < depth; i++)
				{
					if (writer is Html32TextWriter)
					{
						writer.RenderBeginTag(HtmlTextWriterTag.Td);
						writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this._owner.NodeIndent.ToString(CultureInfo.InvariantCulture) + "px");
						writer.RenderBeginTag(HtmlTextWriterTag.Table);
						writer.RenderBeginTag(HtmlTextWriterTag.Tr);
						writer.RenderBeginTag(HtmlTextWriterTag.Td);
						if (this._owner.ShowLines && !isLast[i])
						{
							writer.AddAttribute(HtmlTextWriterAttribute.Src, this._owner.GetImageUrl(6));
							writer.AddAttribute(HtmlTextWriterAttribute.Alt, string.Empty);
							writer.RenderBeginTag(HtmlTextWriterTag.Img);
							writer.RenderEndTag();
						}
						writer.RenderEndTag();
						writer.RenderEndTag();
						writer.RenderEndTag();
						writer.RenderEndTag();
					}
					else
					{
						writer.RenderBeginTag(HtmlTextWriterTag.Td);
						writer.Write("<div style=\"width:" + this._owner.NodeIndent.ToString(CultureInfo.InvariantCulture) + "px;height:1px\">");
						if (this._owner.ShowLines && !isLast[i])
						{
							writer.AddAttribute(HtmlTextWriterAttribute.Src, this._owner.GetImageUrl(6));
							writer.AddAttribute(HtmlTextWriterAttribute.Alt, string.Empty);
							writer.RenderBeginTag(HtmlTextWriterTag.Img);
							writer.RenderEndTag();
						}
						writer.Write("</div>");
						writer.RenderEndTag();
					}
				}
			}
			bool flag4 = (this.PopulateOnDemand || this.ChildNodes.Count > 0) && this._owner.ShowExpandCollapse;
			string text2 = string.Empty;
			string lineType = " ";
			string text3 = string.Empty;
			if (flag)
			{
				if (flag4)
				{
					if (flag3)
					{
						if (this._owner.ShowLines)
						{
							if (depth == 0)
							{
								if (position == 0)
								{
									lineType = "-";
									text2 = this._owner.GetImageUrl(18);
									text3 = this._owner.CollapseImageToolTip;
								}
								else
								{
									lineType = "l";
									text2 = this._owner.GetImageUrl(15);
									text3 = this._owner.CollapseImageToolTip;
								}
							}
							else
							{
								lineType = "l";
								text2 = this._owner.GetImageUrl(15);
								text3 = this._owner.CollapseImageToolTip;
							}
						}
						else
						{
							text2 = this._owner.GetImageUrl(5);
							text3 = this._owner.CollapseImageToolTip;
						}
					}
					else if (this._owner.ShowLines)
					{
						if (depth == 0)
						{
							if (position == 0)
							{
								lineType = "-";
								text2 = this._owner.GetImageUrl(17);
								text3 = this._owner.ExpandImageToolTip;
							}
							else
							{
								lineType = "l";
								text2 = this._owner.GetImageUrl(14);
								text3 = this._owner.ExpandImageToolTip;
							}
						}
						else
						{
							lineType = "l";
							text2 = this._owner.GetImageUrl(14);
							text3 = this._owner.ExpandImageToolTip;
						}
					}
					else
					{
						text2 = this._owner.GetImageUrl(4);
						text3 = this._owner.ExpandImageToolTip;
					}
				}
				else if (this._owner.ShowLines)
				{
					if (depth == 0)
					{
						if (position == 0)
						{
							lineType = "-";
							text2 = this._owner.GetImageUrl(16);
						}
						else
						{
							lineType = "l";
							text2 = this._owner.GetImageUrl(13);
						}
					}
					else
					{
						lineType = "l";
						text2 = this._owner.GetImageUrl(13);
					}
				}
				else if (this._owner.ShowExpandCollapse)
				{
					text2 = this._owner.GetImageUrl(3);
				}
			}
			else if (flag4)
			{
				if (flag3)
				{
					if (this._owner.ShowLines)
					{
						if (depth == 0)
						{
							if (position == 0)
							{
								lineType = "r";
								text2 = this._owner.GetImageUrl(9);
								text3 = this._owner.CollapseImageToolTip;
							}
							else
							{
								lineType = "t";
								text2 = this._owner.GetImageUrl(12);
								text3 = this._owner.CollapseImageToolTip;
							}
						}
						else
						{
							lineType = "t";
							text2 = this._owner.GetImageUrl(12);
							text3 = this._owner.CollapseImageToolTip;
						}
					}
					else
					{
						text2 = this._owner.GetImageUrl(5);
						text3 = this._owner.CollapseImageToolTip;
					}
				}
				else if (this._owner.ShowLines)
				{
					if (depth == 0)
					{
						if (position == 0)
						{
							lineType = "r";
							text2 = this._owner.GetImageUrl(8);
							text3 = this._owner.ExpandImageToolTip;
						}
						else
						{
							lineType = "t";
							text2 = this._owner.GetImageUrl(11);
							text3 = this._owner.ExpandImageToolTip;
						}
					}
					else
					{
						lineType = "t";
						text2 = this._owner.GetImageUrl(11);
						text3 = this._owner.ExpandImageToolTip;
					}
				}
				else
				{
					text2 = this._owner.GetImageUrl(4);
					text3 = this._owner.ExpandImageToolTip;
				}
			}
			else if (this._owner.ShowLines)
			{
				if (depth == 0)
				{
					if (position == 0)
					{
						lineType = "r";
						text2 = this._owner.GetImageUrl(7);
					}
					else
					{
						lineType = "t";
						text2 = this._owner.GetImageUrl(10);
					}
				}
				else
				{
					lineType = "t";
					text2 = this._owner.GetImageUrl(10);
				}
			}
			else if (this._owner.ShowExpandCollapse)
			{
				text2 = this._owner.GetImageUrl(3);
			}
			TreeNodeTypes treeNodeType = this.GetTreeNodeType();
			string text4 = string.Empty;
			if (this.ImageUrl.Length > 0)
			{
				text4 = this._owner.ResolveClientUrl(this.ImageUrl);
			}
			else if (depth < this._owner.LevelStyles.Count && this._owner.LevelStyles[depth] != null && style.ImageUrl.Length > 0)
			{
				text4 = this._owner.GetLevelImageUrl(depth);
			}
			else
			{
				switch (treeNodeType)
				{
				case TreeNodeTypes.Root:
					text4 = this._owner.GetImageUrl(0);
					break;
				case TreeNodeTypes.Parent:
					text4 = this._owner.GetImageUrl(1);
					break;
				case TreeNodeTypes.Leaf:
					text4 = this._owner.GetImageUrl(2);
					break;
				}
			}
			string text5 = string.Empty;
			if (text4.Length > 0)
			{
				text5 = this.SelectID + "i";
			}
			if (text2.Length > 0)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				if (flag4)
				{
					if (this._owner.RenderClientScript && !this._owner.CustomExpandCollapseHandlerExists)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Id, text);
						if (this.PopulateOnDemand)
						{
							if (this._owner.PopulateNodesFromClient)
							{
								if (this.ChildNodes.Count != 0)
								{
									throw new InvalidOperationException(SR.GetString("TreeView_PopulateOnlyEmptyNodes", new object[]
									{
										this._owner.ID
									}));
								}
								writer.AddAttribute(HtmlTextWriterAttribute.Href, this.GetPopulateNodeAttribute(writer, text, this.SelectID, text5, lineType, depth, isLast));
							}
							else
							{
								string value = "javascript:0";
								if (this._owner.Page != null)
								{
									value = this._owner.Page.ClientScript.GetPostBackClientHyperlink(this._owner, "t" + this.InternalValuePath, true, true);
								}
								writer.AddAttribute(HtmlTextWriterAttribute.Href, value);
							}
						}
						else
						{
							writer.AddAttribute(HtmlTextWriterAttribute.Href, this.GetToggleNodeAttributeValue(text, lineType));
						}
					}
					else
					{
						string value2 = "javascript:0";
						if (this._owner.Page != null)
						{
							value2 = this._owner.Page.ClientScript.GetPostBackClientHyperlink(this._owner, "t" + this.InternalValuePath, true);
						}
						writer.AddAttribute(HtmlTextWriterAttribute.Href, value2);
					}
					if (enabled)
					{
						writer.RenderBeginTag(HtmlTextWriterTag.A);
					}
					writer.AddAttribute(HtmlTextWriterAttribute.Src, text2);
					writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0");
					if (text3.Length > 0)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Alt, string.Format(CultureInfo.CurrentCulture, text3, new object[]
						{
							this.Text
						}));
						if (BinaryCompatibility.Current.TargetsAtLeastFramework461)
						{
							writer.AddAttribute(HtmlTextWriterAttribute.Title, string.Format(CultureInfo.CurrentCulture, text3, new object[]
							{
								this.Text
							}));
						}
					}
					else
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Alt, string.Empty);
						if (BinaryCompatibility.Current.TargetsAtLeastFramework461)
						{
							writer.AddAttribute(HtmlTextWriterAttribute.Title, string.Empty);
						}
					}
					writer.RenderBeginTag(HtmlTextWriterTag.Img);
					writer.RenderEndTag();
					if (enabled)
					{
						writer.RenderEndTag();
					}
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Src, text2);
					writer.AddAttribute(HtmlTextWriterAttribute.Alt, string.Empty);
					writer.RenderBeginTag(HtmlTextWriterTag.Img);
					writer.RenderEndTag();
				}
				writer.RenderEndTag();
			}
			ArrayList arrayList = new ArrayList();
			if (this.NavigateUrl.Length > 0)
			{
				arrayList.Add(HtmlTextWriterAttribute.Href);
				arrayList.Add(this._owner.ResolveClientUrl(this.NavigateUrl));
				string text6 = this.ViewState["Target"] as string;
				if (text6 == null)
				{
					text6 = this._owner.Target;
				}
				if (text6.Length > 0)
				{
					arrayList.Add(HtmlTextWriterAttribute.Target);
					arrayList.Add(text6);
					if (this._owner.RenderClientScript)
					{
						string text7 = string.Empty;
						if ((this._owner.Page != null && this._owner.Page.SupportsStyleSheets && this.SelectAction == TreeNodeSelectAction.Select) || this.SelectAction == TreeNodeSelectAction.SelectExpand)
						{
							text7 = Util.MergeScript(text7, string.Concat(new string[]
							{
								"TreeView_SelectNode(",
								this._owner.ClientDataObjectID,
								", this,'",
								this.SelectID,
								"');"
							}));
						}
						if (this.SelectAction == TreeNodeSelectAction.Expand || this.SelectAction == TreeNodeSelectAction.SelectExpand)
						{
							if (this.PopulateOnDemand)
							{
								text7 = Util.MergeScript(text7, this._owner.Page.ClientScript.GetPostBackClientHyperlink(this._owner, "t" + this.InternalValuePath, true, true));
							}
							else if (!this._owner.CustomExpandCollapseHandlerExists && flag4)
							{
								text7 = Util.MergeScript(text7, this.GetToggleNodeAttributeValue(text, lineType));
							}
						}
						if (text7.Length != 0)
						{
							arrayList.Add("onclick");
							arrayList.Add(text7);
						}
					}
				}
			}
			else if (this._owner.RenderClientScript && this.SelectAction == TreeNodeSelectAction.Expand && !this._owner.CustomExpandCollapseHandlerExists)
			{
				if (this.PopulateOnDemand)
				{
					if (this._owner.PopulateNodesFromClient)
					{
						arrayList.Add(HtmlTextWriterAttribute.Href);
						arrayList.Add(this.GetPopulateNodeAttribute(writer, text, this.SelectID, text5, lineType, depth, isLast));
					}
					else
					{
						arrayList.Add(HtmlTextWriterAttribute.Href);
						string value3 = "javascript:0";
						if (this._owner.Page != null)
						{
							value3 = this._owner.Page.ClientScript.GetPostBackClientHyperlink(this._owner, "t" + this.InternalValuePath, true, true);
						}
						arrayList.Add(value3);
					}
				}
				else if (flag4)
				{
					arrayList.Add(HtmlTextWriterAttribute.Href);
					arrayList.Add(this.GetToggleNodeAttributeValue(text, lineType));
				}
			}
			else if (this.SelectAction != TreeNodeSelectAction.None)
			{
				arrayList.Add(HtmlTextWriterAttribute.Href);
				if (this._owner.Page != null)
				{
					string postBackClientHyperlink = this._owner.Page.ClientScript.GetPostBackClientHyperlink(this._owner, "s" + this.InternalValuePath, true, true);
					arrayList.Add(postBackClientHyperlink);
					if (this._owner.RenderClientScript)
					{
						arrayList.Add("onclick");
						arrayList.Add(string.Concat(new string[]
						{
							"TreeView_SelectNode(",
							this._owner.ClientDataObjectID,
							", this,'",
							this.SelectID,
							"');"
						}));
					}
				}
				else
				{
					arrayList.Add("javascript:0");
				}
			}
			if (this.ToolTip.Length > 0)
			{
				arrayList.Add(HtmlTextWriterAttribute.Title);
				arrayList.Add(this.ToolTip);
			}
			if (text4.Length > 0)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				this.ApplyAttributeList(writer, arrayList);
				writer.AddAttribute(HtmlTextWriterAttribute.Id, text5);
				if (enabled && this.SelectAction != TreeNodeSelectAction.None)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, "-1");
					writer.RenderBeginTag(HtmlTextWriterTag.A);
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Src, text4);
				writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0");
				if (this.ImageToolTip.Length > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Alt, this.ImageToolTip);
					if (BinaryCompatibility.Current.TargetsAtLeastFramework461)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Title, this.ImageToolTip);
					}
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Alt, string.Empty);
					if (BinaryCompatibility.Current.TargetsAtLeastFramework461)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Title, string.Empty);
					}
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				if (enabled && this.SelectAction != TreeNodeSelectAction.None)
				{
					writer.RenderEndTag();
				}
				writer.RenderEndTag();
			}
			if (!this._owner.NodeWrap)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
			}
			if (this._owner.Page != null && this._owner.Page.SupportsStyleSheets)
			{
				string cssClassName = this._owner.GetCssClassName(this, false);
				if (cssClassName.Trim().Length > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClassName);
				}
			}
			else if (style != null)
			{
				style.AddAttributesToRender(writer);
			}
			if (this._owner.EnableHover && this.SelectAction != TreeNodeSelectAction.None)
			{
				writer.AddAttribute("onmouseover", "TreeView_HoverNode(" + this._owner.ClientDataObjectID + ", this)");
				writer.AddAttribute("onmouseout", "TreeView_UnhoverNode(this)");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			if (this.GetEffectiveShowCheckBox(treeNodeType))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
				string value4 = text + "CheckBox";
				writer.AddAttribute(HtmlTextWriterAttribute.Name, value4);
				writer.AddAttribute(HtmlTextWriterAttribute.Id, value4);
				if (this.Checked)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
				}
				if (!enabled)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
					if (!this._owner.Enabled && this._owner.RenderingCompatibility >= VersionUtil.Framework40 && !string.IsNullOrEmpty(WebControl.DisabledCssClass))
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Class, WebControl.DisabledCssClass);
					}
				}
				if (this.ToolTip.Length > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Title, this.ToolTip);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Input);
				writer.RenderEndTag();
			}
			this.RenderPreText(writer);
			if (this._owner.Page != null && this._owner.Page.SupportsStyleSheets)
			{
				bool flag5;
				string cssClassName2 = this._owner.GetCssClassName(this, true, out flag5);
				if (cssClassName2.Trim().Length > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClassName2);
					if (flag5)
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "none");
						writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "1em");
					}
				}
			}
			else if (style != null)
			{
				style.HyperLinkStyle.AddAttributesToRender(writer);
			}
			this.ApplyAttributeList(writer, arrayList);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.SelectID);
			if (this.SelectAction == TreeNodeSelectAction.None || !enabled)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write(this.Text);
				writer.RenderEndTag();
			}
			else
			{
				if (!this._owner.AccessKeyRendered && this._owner.AccessKey.Length != 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, this._owner.AccessKey, true);
					this._owner.AccessKeyRendered = true;
				}
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.Write(this.Text);
				writer.RenderEndTag();
			}
			this.RenderPostText(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
			if (style != null && !style.NodeSpacing.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, style.NodeSpacing.ToString(CultureInfo.InvariantCulture));
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
			if (this.ChildNodes.Count > 0)
			{
				if (isLast.Length < depth + 2)
				{
					bool[] array = new bool[depth + 5];
					Array.Copy(isLast, 0, array, 0, isLast.Length);
					isLast = array;
				}
				if (this._owner.RenderClientScript)
				{
					if (!flag3)
					{
						writer.AddStyleAttribute("display", "none");
					}
					else
					{
						writer.AddStyleAttribute("display", "block");
					}
					writer.AddAttribute(HtmlTextWriterAttribute.Id, text + "Nodes");
					writer.RenderBeginTag(HtmlTextWriterTag.Div);
					this.RenderChildNodes(writer, depth, isLast, enabled);
					writer.RenderEndTag();
					return;
				}
				if (flag3)
				{
					this.RenderChildNodes(writer, depth, isLast, enabled);
				}
			}
		}

		// Token: 0x06003FA1 RID: 16289 RVA: 0x000CDBA4 File Offset: 0x000CBDA4
		internal void RenderChildNodes(HtmlTextWriter writer, int depth, bool[] isLast, bool enabled)
		{
			TreeNodeStyle style = this._owner.GetStyle(this);
			if (!style.ChildNodesPadding.IsEmpty)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Height, style.ChildNodesPadding.ToString(CultureInfo.InvariantCulture));
				writer.RenderBeginTag(HtmlTextWriterTag.Table);
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			for (int i = 0; i < this.ChildNodes.Count; i++)
			{
				TreeNode treeNode = this.ChildNodes[i];
				isLast[depth + 1] = (i == this.ChildNodes.Count - 1);
				treeNode.Render(writer, i, isLast, enabled);
			}
			if (!isLast[depth] && !style.ChildNodesPadding.IsEmpty)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Height, style.ChildNodesPadding.ToString(CultureInfo.InvariantCulture));
				writer.RenderBeginTag(HtmlTextWriterTag.Table);
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void RenderPostText(HtmlTextWriter writer)
		{
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void RenderPreText(HtmlTextWriter writer)
		{
		}

		// Token: 0x06003FA4 RID: 16292 RVA: 0x000CDCB0 File Offset: 0x000CBEB0
		internal void ResetValuePathRecursive()
		{
			if (this._valuePath != null)
			{
				this._valuePath = null;
				foreach (object obj in this.ChildNodes)
				{
					TreeNode treeNode = (TreeNode)obj;
					treeNode.ResetValuePathRecursive();
				}
			}
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x000CDD18 File Offset: 0x000CBF18
		public void Select()
		{
			this.Selected = true;
		}

		// Token: 0x06003FA6 RID: 16294 RVA: 0x000CDD21 File Offset: 0x000CBF21
		internal void SetDataBound(bool dataBound)
		{
			this.ViewState["DataBound"] = dataBound;
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x000CDD3C File Offset: 0x000CBF3C
		private void SetExpandedRecursive(bool value)
		{
			this.Expanded = new bool?(value);
			if (this.ChildNodes.Count > 0)
			{
				for (int i = 0; i < this.ChildNodes.Count; i++)
				{
					this.ChildNodes[i].SetExpandedRecursive(value);
				}
			}
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x000CDD8B File Offset: 0x000CBF8B
		internal void SetDataItem(object dataItem)
		{
			this._dataItem = dataItem;
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x000CDD94 File Offset: 0x000CBF94
		internal void SetDataPath(string dataPath)
		{
			this.ViewState["DataPath"] = dataPath;
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x000CDDA7 File Offset: 0x000CBFA7
		internal void SetDirty()
		{
			this.ViewState.SetDirty(true);
			if (this.ChildNodes.Count > 0)
			{
				this.ChildNodes.SetDirty();
			}
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x000CDDD0 File Offset: 0x000CBFD0
		internal void SetOwner(TreeView owner)
		{
			this._owner = owner;
			if (this._selectDesired == 1)
			{
				this._selectDesired = 0;
				this.Selected = true;
			}
			else if (this._selectDesired == -1)
			{
				this._selectDesired = 0;
				this.Selected = false;
			}
			if (this._populateDesired)
			{
				this._populateDesired = false;
				this.Populate();
			}
			if (this._modifyCheckedNodes && this._owner != null)
			{
				this._modifyCheckedNodes = false;
				if (this.Checked)
				{
					TreeNodeCollection checkedNodes = this._owner.CheckedNodes;
					if (!checkedNodes.Contains(this))
					{
						this._owner.CheckedNodes.Add(this);
					}
				}
				else
				{
					this._owner.CheckedNodes.Remove(this);
				}
			}
			foreach (object obj in this.ChildNodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				treeNode.SetOwner(this._owner);
			}
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x000CDED4 File Offset: 0x000CC0D4
		internal void SetParent(TreeNode parent)
		{
			this._parent = parent;
			this.SetPath(null);
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x000CDEE4 File Offset: 0x000CC0E4
		internal void SetPath(string newPath)
		{
			this._internalValuePath = newPath;
			this._depth = -2;
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x000CDEF5 File Offset: 0x000CC0F5
		internal void SetSelected(bool value)
		{
			this.ViewState["Selected"] = value;
			if (this._owner == null)
			{
				this._selectDesired = (value ? 1 : -1);
			}
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x000CDF24 File Offset: 0x000CC124
		public void ToggleExpandState()
		{
			bool? expanded = this.Expanded;
			bool flag = true;
			this.Expanded = new bool?(!(expanded.GetValueOrDefault() == flag & expanded != null));
		}

		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x06003FB0 RID: 16304 RVA: 0x000CDF5A File Offset: 0x000CC15A
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x06003FB1 RID: 16305 RVA: 0x000CDF62 File Offset: 0x000CC162
		protected bool IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x06003FB2 RID: 16306 RVA: 0x000CDF6A File Offset: 0x000CC16A
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x06003FB3 RID: 16307 RVA: 0x000CDF74 File Offset: 0x000CC174
		protected virtual void LoadViewState(object state)
		{
			object[] array = (object[])state;
			if (array != null)
			{
				if (array[0] != null)
				{
					((IStateManager)this.ViewState).LoadViewState(array[0]);
					this.NotifyOwnerChecked();
				}
				if (array[1] != null)
				{
					((IStateManager)this.ChildNodes).LoadViewState(array[1]);
				}
			}
		}

		// Token: 0x06003FB4 RID: 16308 RVA: 0x000CDFB7 File Offset: 0x000CC1B7
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06003FB5 RID: 16309 RVA: 0x000CDFC0 File Offset: 0x000CC1C0
		protected virtual object SaveViewState()
		{
			object[] array = new object[2];
			if (this._viewState != null)
			{
				array[0] = ((IStateManager)this._viewState).SaveViewState();
			}
			if (this._childNodes != null)
			{
				array[1] = ((IStateManager)this._childNodes).SaveViewState();
			}
			if (array[0] == null && array[1] == null)
			{
				return null;
			}
			return array;
		}

		// Token: 0x06003FB6 RID: 16310 RVA: 0x000CE00D File Offset: 0x000CC20D
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x000CE015 File Offset: 0x000CC215
		protected void TrackViewState()
		{
			this._isTrackingViewState = true;
			if (this._viewState != null)
			{
				((IStateManager)this._viewState).TrackViewState();
			}
			if (this._childNodes != null)
			{
				((IStateManager)this._childNodes).TrackViewState();
			}
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x000CE044 File Offset: 0x000CC244
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x000CE04C File Offset: 0x000CC24C
		protected virtual object Clone()
		{
			TreeNode treeNode = new TreeNode();
			treeNode.Checked = this.Checked;
			treeNode.Expanded = this.Expanded;
			treeNode.ImageUrl = this.ImageUrl;
			treeNode.ImageToolTip = this.ImageToolTip;
			treeNode.NavigateUrl = this.NavigateUrl;
			treeNode.PopulateOnDemand = this.PopulateOnDemand;
			treeNode.SelectAction = this.SelectAction;
			treeNode.Selected = this.Selected;
			if (this.ViewState["ShowCheckBox"] != null)
			{
				treeNode.ShowCheckBox = this.ShowCheckBox;
			}
			treeNode.Target = this.Target;
			treeNode.Text = this.Text;
			treeNode.ToolTip = this.ToolTip;
			treeNode.Value = this.Value;
			return treeNode;
		}

		// Token: 0x04002452 RID: 9298
		private bool _isTrackingViewState;

		// Token: 0x04002453 RID: 9299
		private StateBag _viewState;

		// Token: 0x04002454 RID: 9300
		private TreeNodeCollection _childNodes;

		// Token: 0x04002455 RID: 9301
		private TreeView _owner;

		// Token: 0x04002456 RID: 9302
		private TreeNode _parent;

		// Token: 0x04002457 RID: 9303
		private bool _populateDesired;

		// Token: 0x04002458 RID: 9304
		private int _selectDesired;

		// Token: 0x04002459 RID: 9305
		private bool _modifyCheckedNodes;

		// Token: 0x0400245A RID: 9306
		private string _parentIsLast;

		// Token: 0x0400245B RID: 9307
		private string _toggleNodeAttributeValue;

		// Token: 0x0400245C RID: 9308
		private object _dataItem;

		// Token: 0x0400245D RID: 9309
		private int _index;

		// Token: 0x0400245E RID: 9310
		private string _valuePath;

		// Token: 0x0400245F RID: 9311
		private string _internalValuePath;

		// Token: 0x04002460 RID: 9312
		private int _depth = -2;

		// Token: 0x04002461 RID: 9313
		private bool _isRoot;
	}
}
