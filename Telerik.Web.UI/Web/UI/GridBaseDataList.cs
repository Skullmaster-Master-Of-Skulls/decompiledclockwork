using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000395 RID: 917
	[DefaultProperty("DataSource")]
	[DefaultEvent("SelectedIndexChanged")]
	public abstract class GridBaseDataList : RadCompositeDataBoundControl
	{
		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06001F7E RID: 8062 RVA: 0x00063CCD File Offset: 0x00061ECD
		// (remove) Token: 0x06001F7F RID: 8063 RVA: 0x00063CE0 File Offset: 0x00061EE0
		[Category("Action")]
		[Description("Fires when the current selection changes.")]
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(GridBaseDataList.EventSelectedIndexChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(GridBaseDataList.EventSelectedIndexChanged, value);
			}
		}

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06001F80 RID: 8064 RVA: 0x00063CF3 File Offset: 0x00061EF3
		// (remove) Token: 0x06001F81 RID: 8065 RVA: 0x00063D06 File Offset: 0x00061F06
		[Description("Fires when the current cell selection changes.")]
		[Category("Action")]
		public event EventHandler SelectedCellChanged
		{
			add
			{
				base.Events.AddHandler(GridBaseDataList.EventSelectedCellChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(GridBaseDataList.EventSelectedCellChanged, value);
			}
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x00063D2F File Offset: 0x00061F2F
		public GridBaseDataList()
		{
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06001F84 RID: 8068 RVA: 0x00063D42 File Offset: 0x00061F42
		// (set) Token: 0x06001F85 RID: 8069 RVA: 0x00063D4A File Offset: 0x00061F4A
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

		// Token: 0x06001F86 RID: 8070 RVA: 0x00063D53 File Offset: 0x00061F53
		protected override void AddParsedSubObject(object obj)
		{
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x00063D58 File Offset: 0x00061F58
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (!this.UsesControlState)
			{
				this.LoadControlStateDictionary(array[1]);
			}
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x00063D88 File Offset: 0x00061F88
		private void LoadControlStateDictionary(object state)
		{
			Hashtable hashtable = (Hashtable)state;
			foreach (object obj in hashtable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.ControlState[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x00063DF8 File Offset: 0x00061FF8
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			object value = base.SaveViewState();
			arrayList.Add(value);
			if (!this.UsesControlState)
			{
				this.SaveControlStateDictionary(arrayList);
			}
			return (object[])arrayList.ToArray(typeof(object));
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x00063E3E File Offset: 0x0006203E
		private void SaveControlStateDictionary(IList state)
		{
			state.Add(this.ControlState);
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x00063E4D File Offset: 0x0006204D
		protected override void OnInit(EventArgs e)
		{
			if (this.UsesControlState)
			{
				this.RegisterForControlState();
			}
			base.OnInit(e);
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06001F8C RID: 8076 RVA: 0x00063E64 File Offset: 0x00062064
		internal bool UsesControlState
		{
			get
			{
				return !base.IsViewStateEnabled;
			}
		}

		// Token: 0x06001F8D RID: 8077
		internal abstract void RegisterForControlState();

		// Token: 0x06001F8E RID: 8078 RVA: 0x00063E70 File Offset: 0x00062070
		protected override object SaveControlState()
		{
			if (this.UsesControlState)
			{
				ArrayList arrayList = new ArrayList();
				object value = base.SaveControlState();
				arrayList.Add(value);
				this.SaveControlStateDictionary(arrayList);
				return (object[])arrayList.ToArray(typeof(object));
			}
			return base.SaveControlState();
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x00063EC0 File Offset: 0x000620C0
		protected override void LoadControlState(object savedState)
		{
			if (this.UsesControlState)
			{
				object[] array = (object[])savedState;
				base.LoadControlState(array[0]);
				this.LoadControlStateDictionary(array[1]);
				return;
			}
			base.LoadControlState(savedState);
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x00063EF8 File Offset: 0x000620F8
		public static bool IsBindableType(Type type)
		{
			return type.IsPrimitive || !(type != typeof(string)) || !(type != typeof(DateTime)) || !(type != typeof(TimeSpan)) || !(type != typeof(decimal)) || !(type != typeof(Guid)) || type.IsEnum || (type.IsValueType && type.IsGenericType && type.GetGenericArguments().Length == 1 && GridBaseDataList.IsBindableType(type.GetGenericArguments()[0]));
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x00063FA0 File Offset: 0x000621A0
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[GridBaseDataList.EventSelectedIndexChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x00063FD0 File Offset: 0x000621D0
		protected virtual void OnSelectedCellChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[GridBaseDataList.EventSelectedCellChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001F93 RID: 8083
		protected abstract void PrepareControlHierarchy();

		// Token: 0x06001F94 RID: 8084 RVA: 0x00063FFE File Offset: 0x000621FE
		protected override void Render(HtmlTextWriter writer)
		{
			if (!this.RegisterWithScriptManager)
			{
				this.ControlPreRender();
				this.EnsureChildControls();
				this.RenderScriptsNoScriptManager(writer);
				this.RenderDescriptorsNoScriptManager(writer);
			}
			this.PrepareControlHierarchy();
			this.RenderContents(writer);
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x00064030 File Offset: 0x00062230
		protected override Style CreateControlStyle()
		{
			return new TableStyle(this.ViewState)
			{
				GridLines = GridLines.Both,
				CellSpacing = 0
			};
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06001F96 RID: 8086 RVA: 0x00064058 File Offset: 0x00062258
		// (set) Token: 0x06001F97 RID: 8087 RVA: 0x00064074 File Offset: 0x00062274
		[NotifyParentProperty(true)]
		[DefaultValue(-1)]
		[Description("The padding within cells.")]
		[Bindable(true)]
		[Category("Layout")]
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

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x00064087 File Offset: 0x00062287
		// (set) Token: 0x06001F99 RID: 8089 RVA: 0x000640A3 File Offset: 0x000622A3
		[Description("The spacing between cells.")]
		[Category("Layout")]
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[DefaultValue(0)]
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

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x000640B6 File Offset: 0x000622B6
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06001F9B RID: 8091 RVA: 0x000640C4 File Offset: 0x000622C4
		// (set) Token: 0x06001F9C RID: 8092 RVA: 0x00064103 File Offset: 0x00062303
		[DefaultValue(typeof(GridLines), "None")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[Description("Settings for grid lines between cells.")]
		[Bindable(true)]
		public virtual GridLines GridLines
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return GridLines.Both;
				}
				if (this.gridLinesExplicitlySet)
				{
					return ((TableStyle)base.ControlStyle).GridLines;
				}
				if (!this.EmptySkin())
				{
					return GridLines.None;
				}
				return ((TableStyle)base.ControlStyle).GridLines;
			}
			set
			{
				this.gridLinesExplicitlySet = true;
				((TableStyle)base.ControlStyle).GridLines = value;
			}
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x0006411D File Offset: 0x0006231D
		internal bool EmptySkin()
		{
			return string.IsNullOrEmpty(base.RuntimeSkin);
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06001F9E RID: 8094 RVA: 0x0006412A File Offset: 0x0006232A
		// (set) Token: 0x06001F9F RID: 8095 RVA: 0x00064146 File Offset: 0x00062346
		[Description("The horizontal alignment of the control.")]
		[Category("Layout")]
		[Bindable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(HorizontalAlign), "NotSet")]
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

		// Token: 0x04000813 RID: 2067
		internal const string ItemCountControlStateKey = "_!ItemCount";

		// Token: 0x04000814 RID: 2068
		protected internal Hashtable ControlState = new Hashtable();

		// Token: 0x04000815 RID: 2069
		private bool gridLinesExplicitlySet;

		// Token: 0x04000816 RID: 2070
		private static readonly object EventSelectedIndexChanged = new object();

		// Token: 0x04000817 RID: 2071
		private static readonly object EventSelectedCellChanged = new object();
	}
}
