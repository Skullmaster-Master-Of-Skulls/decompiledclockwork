using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200118B RID: 4491
	[DefaultEvent("SelectedIndexChanged")]
	[DefaultProperty("DataSource")]
	public abstract class GridTableViewBase : CompositeDataBoundControl
	{
		// Token: 0x14000182 RID: 386
		// (add) Token: 0x0600B69A RID: 46746 RVA: 0x00283B46 File Offset: 0x00281D46
		// (remove) Token: 0x0600B69B RID: 46747 RVA: 0x00283B59 File Offset: 0x00281D59
		[Description("Fires when the current selection changes.")]
		[Category("Action")]
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(GridTableViewBase.EventSelectedIndexChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(GridTableViewBase.EventSelectedIndexChanged, value);
			}
		}

		// Token: 0x0600B69D RID: 46749 RVA: 0x00283B78 File Offset: 0x00281D78
		public GridTableViewBase()
		{
		}

		// Token: 0x0600B69E RID: 46750 RVA: 0x00283B8B File Offset: 0x00281D8B
		public GridTableViewBase(RadGrid OwnerGrid)
		{
			this._ownerGrid = OwnerGrid;
		}

		// Token: 0x0600B69F RID: 46751 RVA: 0x00283BA5 File Offset: 0x00281DA5
		internal virtual void Initialize(RadGrid OwnerGrid)
		{
			this._ownerGrid = OwnerGrid;
		}

		// Token: 0x17003B00 RID: 15104
		// (get) Token: 0x0600B6A0 RID: 46752 RVA: 0x00283BAE File Offset: 0x00281DAE
		// (set) Token: 0x0600B6A1 RID: 46753 RVA: 0x00283BB6 File Offset: 0x00281DB6
		[NotifyParentProperty(true)]
		public override short TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				base.TabIndex = value;
			}
		}

		// Token: 0x0600B6A2 RID: 46754 RVA: 0x00283BBF File Offset: 0x00281DBF
		protected override void AddParsedSubObject(object obj)
		{
		}

		// Token: 0x0600B6A3 RID: 46755 RVA: 0x00283BC4 File Offset: 0x00281DC4
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (!this.OwnerGrid.UsesControlState)
			{
				this.LoadControlStateDictionary(array[1]);
			}
		}

		// Token: 0x0600B6A4 RID: 46756 RVA: 0x00283BF8 File Offset: 0x00281DF8
		private void LoadControlStateDictionary(object state)
		{
			Hashtable hashtable = (Hashtable)state;
			foreach (object obj in hashtable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.ControlState[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x0600B6A5 RID: 46757 RVA: 0x00283C68 File Offset: 0x00281E68
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			object value = base.SaveViewState();
			arrayList.Add(value);
			if (!this.OwnerGrid.UsesControlState)
			{
				this.SaveControlStateDictionary(arrayList);
			}
			return (object[])arrayList.ToArray(typeof(object));
		}

		// Token: 0x0600B6A6 RID: 46758 RVA: 0x00283CB3 File Offset: 0x00281EB3
		private void SaveControlStateDictionary(IList state)
		{
			state.Add(this.ControlState);
		}

		// Token: 0x0600B6A7 RID: 46759 RVA: 0x00283CC2 File Offset: 0x00281EC2
		protected override void OnInit(EventArgs e)
		{
			if (this.OwnerGrid.UsesControlState)
			{
				this.RegisterForControlState();
			}
			base.OnInit(e);
		}

		// Token: 0x0600B6A8 RID: 46760
		internal abstract void RegisterForControlState();

		// Token: 0x0600B6A9 RID: 46761 RVA: 0x00283CE0 File Offset: 0x00281EE0
		protected override object SaveControlState()
		{
			if (this.OwnerGrid.UsesControlState)
			{
				ArrayList arrayList = new ArrayList();
				object value = base.SaveControlState();
				arrayList.Add(value);
				this.SaveControlStateDictionary(arrayList);
				return (object[])arrayList.ToArray(typeof(object));
			}
			return base.SaveControlState();
		}

		// Token: 0x0600B6AA RID: 46762 RVA: 0x00283D34 File Offset: 0x00281F34
		protected override void LoadControlState(object savedState)
		{
			if (this.OwnerGrid.UsesControlState)
			{
				object[] array = (object[])savedState;
				base.LoadControlState(array[0]);
				this.LoadControlStateDictionary(array[1]);
				return;
			}
			base.LoadControlState(savedState);
		}

		// Token: 0x17003B01 RID: 15105
		// (get) Token: 0x0600B6AB RID: 46763 RVA: 0x00283D6F File Offset: 0x00281F6F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RadGrid OwnerGrid
		{
			get
			{
				return this._ownerGrid;
			}
		}

		// Token: 0x0600B6AC RID: 46764
		protected abstract void CreateControlHierarchy(bool useDataSource);

		// Token: 0x0600B6AD RID: 46765
		protected abstract void PrepareControlHierarchy();

		// Token: 0x0600B6AE RID: 46766 RVA: 0x00283D77 File Offset: 0x00281F77
		protected override void Render(HtmlTextWriter writer)
		{
			this.PrepareControlHierarchy();
			this.RenderContents(writer);
		}

		// Token: 0x0600B6AF RID: 46767 RVA: 0x00283D88 File Offset: 0x00281F88
		protected override Style CreateControlStyle()
		{
			return new TableStyle(this.ViewState)
			{
				GridLines = GridLines.Both
			};
		}

		// Token: 0x17003B02 RID: 15106
		// (get) Token: 0x0600B6B0 RID: 46768 RVA: 0x00283DA9 File Offset: 0x00281FA9
		// (set) Token: 0x0600B6B1 RID: 46769 RVA: 0x00283DC5 File Offset: 0x00281FC5
		[DefaultValue(-1)]
		[Category("Layout")]
		[NotifyParentProperty(true)]
		[Description("The padding within cells.")]
		[Bindable(true)]
		public virtual int CellPadding
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return -1;
				}
				return ((TableStyle)base.ControlStyle).CellPadding;
			}
			set
			{
				((TableStyle)base.ControlStyle).CellPadding = value;
			}
		}

		// Token: 0x17003B03 RID: 15107
		// (get) Token: 0x0600B6B2 RID: 46770 RVA: 0x00283DD8 File Offset: 0x00281FD8
		// (set) Token: 0x0600B6B3 RID: 46771 RVA: 0x00283DF4 File Offset: 0x00281FF4
		[Description("The spacing between cells.")]
		[DefaultValue(-1)]
		[NotifyParentProperty(true)]
		[Category("Layout")]
		[Bindable(true)]
		public virtual int CellSpacing
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return -1;
				}
				return ((TableStyle)base.ControlStyle).CellSpacing;
			}
			set
			{
				((TableStyle)base.ControlStyle).CellSpacing = value;
			}
		}

		// Token: 0x17003B04 RID: 15108
		// (get) Token: 0x0600B6B4 RID: 46772 RVA: 0x00283E07 File Offset: 0x00282007
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		// Token: 0x17003B05 RID: 15109
		// (get) Token: 0x0600B6B5 RID: 46773 RVA: 0x00283E18 File Offset: 0x00282018
		protected ArrayList DataKeysArray
		{
			get
			{
				object obj = this.ViewState["DataKeys"];
				if (obj == null)
				{
					obj = new ArrayList();
					this.ViewState["DataKeys"] = obj;
				}
				return (ArrayList)obj;
			}
		}

		// Token: 0x17003B06 RID: 15110
		// (get) Token: 0x0600B6B6 RID: 46774 RVA: 0x00283E56 File Offset: 0x00282056
		// (set) Token: 0x0600B6B7 RID: 46775 RVA: 0x00283E72 File Offset: 0x00282072
		[Category("Appearance")]
		[Description("Settings for grid lines between cells.")]
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[DefaultValue(typeof(GridLines), "Both")]
		public virtual GridLines GridLines
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return GridLines.Both;
				}
				return ((TableStyle)base.ControlStyle).GridLines;
			}
			set
			{
				((TableStyle)base.ControlStyle).GridLines = value;
			}
		}

		// Token: 0x17003B07 RID: 15111
		// (get) Token: 0x0600B6B8 RID: 46776 RVA: 0x00283E85 File Offset: 0x00282085
		// (set) Token: 0x0600B6B9 RID: 46777 RVA: 0x00283EA1 File Offset: 0x002820A1
		[Description("The horizontal alignment of the control.")]
		[DefaultValue(0)]
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[Category("Layout")]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return HorizontalAlign.NotSet;
				}
				return ((TableStyle)base.ControlStyle).HorizontalAlign;
			}
			set
			{
				((TableStyle)base.ControlStyle).HorizontalAlign = value;
			}
		}

		// Token: 0x04003053 RID: 12371
		internal const string ItemCountControlStateKey = "_!ItemCount";

		// Token: 0x04003054 RID: 12372
		protected internal Hashtable ControlState = new Hashtable();

		// Token: 0x04003055 RID: 12373
		internal RadGrid _ownerGrid;

		// Token: 0x04003056 RID: 12374
		private static readonly object EventSelectedIndexChanged = new object();
	}
}
