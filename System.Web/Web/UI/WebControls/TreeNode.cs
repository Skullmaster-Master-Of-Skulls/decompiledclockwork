using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000668 RID: 1640
	[ParseChildren(true, "ChildNodes")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TreeNode : IStateManager, ICloneable
	{
		// Token: 0x06005011 RID: 20497 RVA: 0x00141344 File Offset: 0x00140344
		public TreeNode()
		{
			this._selectDesired = 0;
		}

		// Token: 0x06005012 RID: 20498 RVA: 0x0014135B File Offset: 0x0014035B
		protected internal TreeNode(TreeView owner, bool isRoot) : this()
		{
			this._owner = owner;
			this._isRoot = isRoot;
		}

		// Token: 0x06005013 RID: 20499 RVA: 0x00141371 File Offset: 0x00140371
		public TreeNode(string text) : this(text, null, null, null, null)
		{
		}

		// Token: 0x06005014 RID: 20500 RVA: 0x0014137E File Offset: 0x0014037E
		public TreeNode(string text, string value) : this(text, value, null, null, null)
		{
		}

		// Token: 0x06005015 RID: 20501 RVA: 0x0014138B File Offset: 0x0014038B
		public TreeNode(string text, string value, string imageUrl) : this(text, value, imageUrl, null, null)
		{
		}

		// Token: 0x06005016 RID: 20502 RVA: 0x00141398 File Offset: 0x00140398
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

		// Token: 0x17001445 RID: 5189
		// (get) Token: 0x06005017 RID: 20503 RVA: 0x001413F0 File Offset: 0x001403F0
		// (set) Token: 0x06005018 RID: 20504 RVA: 0x00141419 File Offset: 0x00140419
		[WebSysDescription("TreeNode_Checked")]
		[DefaultValue(false)]
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

		// Token: 0x17001446 RID: 5190
		// (get) Token: 0x06005019 RID: 20505 RVA: 0x00141437 File Offset: 0x00140437
		internal bool CheckedSet
		{
			get
			{
				return this.ViewState["Checked"] != null;
			}
		}

		// Token: 0x17001447 RID: 5191
		// (get) Token: 0x0600501A RID: 20506 RVA: 0x00141450 File Offset: 0x00140450
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool DataBound
		{
			get
			{
				object obj = this.ViewState["DataBound"];
				return obj != null && (bool)obj;
			}
		}

		// Token: 0x17001448 RID: 5192
		// (get) Token: 0x0600501B RID: 20507 RVA: 0x00141479 File Offset: 0x00140479
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[DefaultValue(null)]
		[Browsable(false)]
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

		// Token: 0x17001449 RID: 5193
		// (get) Token: 0x0600501C RID: 20508 RVA: 0x00141498 File Offset: 0x00140498
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[DefaultValue("")]
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

		// Token: 0x1700144A RID: 5194
		// (get) Token: 0x0600501D RID: 20509 RVA: 0x001414C8 File Offset: 0x001404C8
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

		// Token: 0x1700144B RID: 5195
		// (get) Token: 0x0600501E RID: 20510 RVA: 0x0014153C File Offset: 0x0014053C
		// (set) Token: 0x0600501F RID: 20511 RVA: 0x00141570 File Offset: 0x00140570
		[WebSysDescription("TreeNode_Expanded")]
		[DefaultValue(typeof(bool?), "")]
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
				bool valueOrDefault = flag.GetValueOrDefault();
				bool? flag2 = expanded;
				if (valueOrDefault != flag2.GetValueOrDefault() || flag != null != (flag2 != null))
				{
					if (this._owner != null && this._owner.DesignMode)
					{
						return;
					}
					if (value == true)
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
					else if (value == false && expanded == true && this.ChildNodes.Count > 0 && this._owner != null)
					{
						this._owner.RaiseTreeNodeCollapsed(this);
					}
				}
			}
		}

		// Token: 0x1700144C RID: 5196
		// (get) Token: 0x06005020 RID: 20512 RVA: 0x00141689 File Offset: 0x00140689
		[Browsable(false)]
		[DefaultValue(null)]
		public object DataItem
		{
			get
			{
				return this._dataItem;
			}
		}

		// Token: 0x1700144D RID: 5197
		// (get) Token: 0x06005021 RID: 20513 RVA: 0x00141694 File Offset: 0x00140694
		// (set) Token: 0x06005022 RID: 20514 RVA: 0x001416C1 File Offset: 0x001406C1
		[Localizable(true)]
		[DefaultValue("")]
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

		// Token: 0x1700144E RID: 5198
		// (get) Token: 0x06005023 RID: 20515 RVA: 0x001416D4 File Offset: 0x001406D4
		// (set) Token: 0x06005024 RID: 20516 RVA: 0x00141701 File Offset: 0x00140701
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

		// Token: 0x1700144F RID: 5199
		// (get) Token: 0x06005025 RID: 20517 RVA: 0x00141714 File Offset: 0x00140714
		// (set) Token: 0x06005026 RID: 20518 RVA: 0x0014171C File Offset: 0x0014071C
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

		// Token: 0x17001450 RID: 5200
		// (get) Token: 0x06005027 RID: 20519 RVA: 0x00141728 File Offset: 0x00140728
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

		// Token: 0x17001451 RID: 5201
		// (get) Token: 0x06005028 RID: 20520 RVA: 0x001417D8 File Offset: 0x001407D8
		// (set) Token: 0x06005029 RID: 20521 RVA: 0x00141805 File Offset: 0x00140805
		[UrlProperty]
		[WebSysDescription("TreeNode_NavigateUrl")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
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

		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x0600502A RID: 20522 RVA: 0x00141818 File Offset: 0x00140818
		internal TreeView Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17001453 RID: 5203
		// (get) Token: 0x0600502B RID: 20523 RVA: 0x00141820 File Offset: 0x00140820
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

		// Token: 0x17001454 RID: 5204
		// (get) Token: 0x0600502C RID: 20524 RVA: 0x00141840 File Offset: 0x00140840
		// (set) Token: 0x0600502D RID: 20525 RVA: 0x00141869 File Offset: 0x00140869
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

		// Token: 0x17001455 RID: 5205
		// (get) Token: 0x0600502E RID: 20526 RVA: 0x00141884 File Offset: 0x00140884
		// (set) Token: 0x0600502F RID: 20527 RVA: 0x001418B0 File Offset: 0x001408B0
		[WebSysDescription("TreeNode_PopulateOnDemand")]
		[DefaultValue(false)]
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
				if (value && this.Expanded == true)
				{
					this.Expanded = null;
				}
			}
		}

		// Token: 0x17001456 RID: 5206
		// (get) Token: 0x06005030 RID: 20528 RVA: 0x00141904 File Offset: 0x00140904
		// (set) Token: 0x06005031 RID: 20529 RVA: 0x0014192D File Offset: 0x0014092D
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

		// Token: 0x17001457 RID: 5207
		// (get) Token: 0x06005032 RID: 20530 RVA: 0x00141948 File Offset: 0x00140948
		// (set) Token: 0x06005033 RID: 20531 RVA: 0x00141971 File Offset: 0x00140971
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

		// Token: 0x17001458 RID: 5208
		// (get) Token: 0x06005034 RID: 20532 RVA: 0x0014198C File Offset: 0x0014098C
		// (set) Token: 0x06005035 RID: 20533 RVA: 0x001419B8 File Offset: 0x001409B8
		[WebSysDescription("TreeNode_Selected")]
		[DefaultValue(false)]
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

		// Token: 0x17001459 RID: 5209
		// (get) Token: 0x06005036 RID: 20534 RVA: 0x00141A0C File Offset: 0x00140A0C
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

		// Token: 0x1700145A RID: 5210
		// (get) Token: 0x06005037 RID: 20535 RVA: 0x00141A40 File Offset: 0x00140A40
		// (set) Token: 0x06005038 RID: 20536 RVA: 0x00141A71 File Offset: 0x00140A71
		[WebSysDescription("TreeNode_ShowCheckBox")]
		[DefaultValue(typeof(bool?), "")]
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

		// Token: 0x1700145B RID: 5211
		// (get) Token: 0x06005039 RID: 20537 RVA: 0x00141A8C File Offset: 0x00140A8C
		// (set) Token: 0x0600503A RID: 20538 RVA: 0x00141AB9 File Offset: 0x00140AB9
		[WebSysDescription("TreeNode_Target")]
		[DefaultValue("")]
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

		// Token: 0x1700145C RID: 5212
		// (get) Token: 0x0600503B RID: 20539 RVA: 0x00141ACC File Offset: 0x00140ACC
		// (set) Token: 0x0600503C RID: 20540 RVA: 0x00141B12 File Offset: 0x00140B12
		[DefaultValue("")]
		[WebSysDescription("TreeNode_Text")]
		[Localizable(true)]
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

		// Token: 0x1700145D RID: 5213
		// (get) Token: 0x0600503D RID: 20541 RVA: 0x00141B28 File Offset: 0x00140B28
		// (set) Token: 0x0600503E RID: 20542 RVA: 0x00141B55 File Offset: 0x00140B55
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

		// Token: 0x1700145E RID: 5214
		// (get) Token: 0x0600503F RID: 20543 RVA: 0x00141B68 File Offset: 0x00140B68
		// (set) Token: 0x06005040 RID: 20544 RVA: 0x00141BAE File Offset: 0x00140BAE
		[DefaultValue("")]
		[WebSysDescription("TreeNode_Value")]
		[Localizable(true)]
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

		// Token: 0x1700145F RID: 5215
		// (get) Token: 0x06005041 RID: 20545 RVA: 0x00141BC8 File Offset: 0x00140BC8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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
					this._valuePath = ((valuePath.Length == 0 && this._parent.Depth == -1) ? this.Value : (valuePath + this._owner.PathSeparator + this.Value));
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

		// Token: 0x17001460 RID: 5216
		// (get) Token: 0x06005042 RID: 20546 RVA: 0x00141CB2 File Offset: 0x00140CB2
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

		// Token: 0x06005043 RID: 20547 RVA: 0x00141CE0 File Offset: 0x00140CE0
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

		// Token: 0x06005044 RID: 20548 RVA: 0x00141D44 File Offset: 0x00140D44
		public void Collapse()
		{
			this.Expanded = new bool?(false);
		}

		// Token: 0x06005045 RID: 20549 RVA: 0x00141D52 File Offset: 0x00140D52
		public void CollapseAll()
		{
			this.SetExpandedRecursive(false);
		}

		// Token: 0x06005046 RID: 20550 RVA: 0x00141D5B File Offset: 0x00140D5B
		public void Expand()
		{
			this.Expanded = new bool?(true);
		}

		// Token: 0x06005047 RID: 20551 RVA: 0x00141D69 File Offset: 0x00140D69
		public void ExpandAll()
		{
			this.SetExpandedRecursive(true);
		}

		// Token: 0x06005048 RID: 20552 RVA: 0x00141D72 File Offset: 0x00140D72
		internal TreeNode GetParentInternal()
		{
			return this._parent;
		}

		// Token: 0x06005049 RID: 20553 RVA: 0x00141D7C File Offset: 0x00140D7C
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
				result = string.Concat(new object[]
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
					this.DataBound ? 't' : 'f',
					"','",
					Util.QuoteJScriptString(this.DataPath, true),
					"','",
					this._parentIsLast,
					"')"
				});
			}
			else
			{
				result = string.Concat(new object[]
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
					this.DataBound ? 't' : 'f',
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

		// Token: 0x0600504A RID: 20554 RVA: 0x00142079 File Offset: 0x00141079
		internal bool GetEffectiveShowCheckBox()
		{
			return this.GetEffectiveShowCheckBox(this.GetTreeNodeType());
		}

		// Token: 0x0600504B RID: 20555 RVA: 0x00142088 File Offset: 0x00141088
		private bool GetEffectiveShowCheckBox(TreeNodeTypes type)
		{
			return this.ShowCheckBox == true || (!(this.ShowCheckBox == false) && (this._owner.ShowCheckBoxes & type) != TreeNodeTypes.None);
		}

		// Token: 0x0600504C RID: 20556 RVA: 0x001420E4 File Offset: 0x001410E4
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

		// Token: 0x0600504D RID: 20557 RVA: 0x00142200 File Offset: 0x00141200
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

		// Token: 0x0600504E RID: 20558 RVA: 0x00142244 File Offset: 0x00141244
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

		// Token: 0x0600504F RID: 20559 RVA: 0x001422B5 File Offset: 0x001412B5
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

		// Token: 0x06005050 RID: 20560 RVA: 0x001422E8 File Offset: 0x001412E8
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
			bool flag2 = this.Expanded == true;
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
			bool flag3 = (this.PopulateOnDemand || this.ChildNodes.Count > 0) && this._owner.ShowExpandCollapse;
			string text2 = string.Empty;
			string lineType = " ";
			string text3 = string.Empty;
			if (flag)
			{
				if (flag3)
				{
					if (flag2)
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
			else if (flag3)
			{
				if (flag2)
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
				if (flag3)
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
					}
					else
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Alt, string.Empty);
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
							else if (!this._owner.CustomExpandCollapseHandlerExists && flag3)
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
				else if (flag3)
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
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Alt, string.Empty);
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
				bool flag4;
				string cssClassName2 = this._owner.GetCssClassName(this, true, out flag4);
				if (cssClassName2.Trim().Length > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClassName2);
					if (flag4)
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
					if (!flag2)
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
				if (flag2)
				{
					this.RenderChildNodes(writer, depth, isLast, enabled);
				}
			}
		}

		// Token: 0x06005051 RID: 20561 RVA: 0x001433F4 File Offset: 0x001423F4
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

		// Token: 0x06005052 RID: 20562 RVA: 0x00143503 File Offset: 0x00142503
		protected virtual void RenderPostText(HtmlTextWriter writer)
		{
		}

		// Token: 0x06005053 RID: 20563 RVA: 0x00143505 File Offset: 0x00142505
		protected virtual void RenderPreText(HtmlTextWriter writer)
		{
		}

		// Token: 0x06005054 RID: 20564 RVA: 0x00143508 File Offset: 0x00142508
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

		// Token: 0x06005055 RID: 20565 RVA: 0x00143570 File Offset: 0x00142570
		public void Select()
		{
			this.Selected = true;
		}

		// Token: 0x06005056 RID: 20566 RVA: 0x00143579 File Offset: 0x00142579
		internal void SetDataBound(bool dataBound)
		{
			this.ViewState["DataBound"] = dataBound;
		}

		// Token: 0x06005057 RID: 20567 RVA: 0x00143594 File Offset: 0x00142594
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

		// Token: 0x06005058 RID: 20568 RVA: 0x001435E3 File Offset: 0x001425E3
		internal void SetDataItem(object dataItem)
		{
			this._dataItem = dataItem;
		}

		// Token: 0x06005059 RID: 20569 RVA: 0x001435EC File Offset: 0x001425EC
		internal void SetDataPath(string dataPath)
		{
			this.ViewState["DataPath"] = dataPath;
		}

		// Token: 0x0600505A RID: 20570 RVA: 0x001435FF File Offset: 0x001425FF
		internal void SetDirty()
		{
			this.ViewState.SetDirty(true);
			if (this.ChildNodes.Count > 0)
			{
				this.ChildNodes.SetDirty();
			}
		}

		// Token: 0x0600505B RID: 20571 RVA: 0x00143628 File Offset: 0x00142628
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

		// Token: 0x0600505C RID: 20572 RVA: 0x0014372C File Offset: 0x0014272C
		internal void SetParent(TreeNode parent)
		{
			this._parent = parent;
			this.SetPath(null);
		}

		// Token: 0x0600505D RID: 20573 RVA: 0x0014373C File Offset: 0x0014273C
		internal void SetPath(string newPath)
		{
			this._internalValuePath = newPath;
			this._depth = -2;
		}

		// Token: 0x0600505E RID: 20574 RVA: 0x0014374D File Offset: 0x0014274D
		internal void SetSelected(bool value)
		{
			this.ViewState["Selected"] = value;
			if (this._owner == null)
			{
				this._selectDesired = (value ? 1 : -1);
			}
		}

		// Token: 0x0600505F RID: 20575 RVA: 0x0014377C File Offset: 0x0014277C
		public void ToggleExpandState()
		{
			this.Expanded = new bool?(!(this.Expanded == true));
		}

		// Token: 0x17001461 RID: 5217
		// (get) Token: 0x06005060 RID: 20576 RVA: 0x001437B1 File Offset: 0x001427B1
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x17001462 RID: 5218
		// (get) Token: 0x06005061 RID: 20577 RVA: 0x001437B9 File Offset: 0x001427B9
		protected bool IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x06005062 RID: 20578 RVA: 0x001437C1 File Offset: 0x001427C1
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x06005063 RID: 20579 RVA: 0x001437CC File Offset: 0x001427CC
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

		// Token: 0x06005064 RID: 20580 RVA: 0x0014380F File Offset: 0x0014280F
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06005065 RID: 20581 RVA: 0x00143818 File Offset: 0x00142818
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

		// Token: 0x06005066 RID: 20582 RVA: 0x00143865 File Offset: 0x00142865
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x06005067 RID: 20583 RVA: 0x0014386D File Offset: 0x0014286D
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

		// Token: 0x06005068 RID: 20584 RVA: 0x0014389C File Offset: 0x0014289C
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06005069 RID: 20585 RVA: 0x001438A4 File Offset: 0x001428A4
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

		// Token: 0x04002D12 RID: 11538
		private bool _isTrackingViewState;

		// Token: 0x04002D13 RID: 11539
		private StateBag _viewState;

		// Token: 0x04002D14 RID: 11540
		private TreeNodeCollection _childNodes;

		// Token: 0x04002D15 RID: 11541
		private TreeView _owner;

		// Token: 0x04002D16 RID: 11542
		private TreeNode _parent;

		// Token: 0x04002D17 RID: 11543
		private bool _populateDesired;

		// Token: 0x04002D18 RID: 11544
		private int _selectDesired;

		// Token: 0x04002D19 RID: 11545
		private bool _modifyCheckedNodes;

		// Token: 0x04002D1A RID: 11546
		private string _parentIsLast;

		// Token: 0x04002D1B RID: 11547
		private string _toggleNodeAttributeValue;

		// Token: 0x04002D1C RID: 11548
		private object _dataItem;

		// Token: 0x04002D1D RID: 11549
		private int _index;

		// Token: 0x04002D1E RID: 11550
		private string _valuePath;

		// Token: 0x04002D1F RID: 11551
		private string _internalValuePath;

		// Token: 0x04002D20 RID: 11552
		private int _depth = -2;

		// Token: 0x04002D21 RID: 11553
		private bool _isRoot;
	}
}
