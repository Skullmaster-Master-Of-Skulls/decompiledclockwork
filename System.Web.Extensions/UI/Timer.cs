using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Web.Resources;

namespace System.Web.UI
{
	// Token: 0x02000083 RID: 131
	[DefaultEvent("Tick")]
	[DefaultProperty("Interval")]
	[Designer("System.Web.UI.Design.TimerDesigner, System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[NonVisualControl]
	[ToolboxBitmap(typeof(EmbeddedResourceFinder), "System.Web.Resources.Timer.bmp")]
	[SupportsEventValidation]
	public class Timer : Control, IPostBackEventHandler, IScriptControl
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0001A038 File Offset: 0x00018238
		private IPage IPage
		{
			get
			{
				if (this._page == null)
				{
					Page page = this.Page;
					if (page == null)
					{
						throw new InvalidOperationException(AtlasWeb.Common_PageCannotBeNull);
					}
					this._page = new PageWrapper(page);
				}
				return this._page;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0001A074 File Offset: 0x00018274
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x0001A0A0 File Offset: 0x000182A0
		[ResourceDescription("Timer_TimerEnable")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				object obj = this.ViewState["Enabled"];
				return obj == null || (bool)obj;
			}
			set
			{
				if (!this._stateDirty && base.IsTrackingViewState)
				{
					object obj = this.ViewState["Enabled"];
					this._stateDirty = (obj == null || value != (bool)obj);
				}
				this.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0001A0FC File Offset: 0x000182FC
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x0001A12C File Offset: 0x0001832C
		[ResourceDescription("Timer_TimerInterval")]
		[Category("Behavior")]
		[DefaultValue(60000)]
		public int Interval
		{
			get
			{
				object obj = this.ViewState["Interval"];
				if (obj == null)
				{
					return 60000;
				}
				return (int)obj;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value", AtlasWeb.Timer_IntervalMustBeGreaterThanZero);
				}
				if (!this._stateDirty && base.IsTrackingViewState)
				{
					object obj = this.ViewState["Interval"];
					this._stateDirty = (obj == null || value != (int)obj);
				}
				this.ViewState["Interval"] = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0001A19C File Offset: 0x0001839C
		internal ScriptManager ScriptManager
		{
			get
			{
				if (this._scriptManager == null)
				{
					Page page = this.Page;
					if (page == null)
					{
						throw new InvalidOperationException(AtlasWeb.Common_PageCannotBeNull);
					}
					this._scriptManager = ScriptManager.GetCurrent(page);
					if (this._scriptManager == null)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.Common_ScriptManagerRequired, new object[]
						{
							this.ID
						}));
					}
				}
				return this._scriptManager;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00011F1F File Offset: 0x0001011F
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x00002058 File Offset: 0x00000258
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600059D RID: 1437 RVA: 0x0001A204 File Offset: 0x00018404
		// (remove) Token: 0x0600059E RID: 1438 RVA: 0x0001A217 File Offset: 0x00018417
		[ResourceDescription("Timer_TimerTick")]
		[Category("Action")]
		public event EventHandler<EventArgs> Tick
		{
			add
			{
				base.Events.AddHandler(Timer.TickEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Timer.TickEventKey, value);
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0001A22C File Offset: 0x0001842C
		private string GetJsonState()
		{
			return string.Concat(new string[]
			{
				"[",
				this.Enabled ? "true" : "false",
				",",
				this.Interval.ToString(CultureInfo.InvariantCulture),
				"]"
			});
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001A289 File Offset: 0x00018489
		protected virtual IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			ScriptComponentDescriptor scriptComponentDescriptor = new ScriptControlDescriptor("Sys.UI._Timer", this.ClientID);
			scriptComponentDescriptor.AddProperty("interval", this.Interval);
			scriptComponentDescriptor.AddProperty("enabled", this.Enabled);
			scriptComponentDescriptor.AddProperty("uniqueID", this.UniqueID);
			yield return scriptComponentDescriptor;
			yield break;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001A299 File Offset: 0x00018499
		protected virtual IEnumerable<ScriptReference> GetScriptReferences()
		{
			yield return new ScriptReference("MicrosoftAjaxTimer.js", Assembly.GetAssembly(typeof(Timer)).FullName);
			yield break;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001A2A2 File Offset: 0x000184A2
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0001A2AB File Offset: 0x000184AB
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			return this.GetScriptDescriptors();
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0001A2B3 File Offset: 0x000184B3
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			return this.GetScriptReferences();
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001A2BC File Offset: 0x000184BC
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.ScriptManager.RegisterScriptControl<Timer>(this);
			if (this._stateDirty && this.ScriptManager.IsInAsyncPostBack)
			{
				this._stateDirty = false;
				this.ScriptManager.RegisterDataItem(this, this.GetJsonState(), true);
			}
			this.IPage.ClientScript.GetPostBackEventReference(new PostBackOptions(this, string.Empty));
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001A328 File Offset: 0x00018528
		protected virtual void OnTick(EventArgs e)
		{
			EventHandler<EventArgs> eventHandler = (EventHandler<EventArgs>)base.Events[Timer.TickEventKey];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001A356 File Offset: 0x00018556
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			if (this.Enabled)
			{
				this.OnTick(EventArgs.Empty);
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001A36C File Offset: 0x0001856C
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.IPage.VerifyRenderingInServerForm(this);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			writer.AddStyleAttribute(HtmlTextWriterStyle.Visibility, "hidden");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
			if (!base.DesignMode)
			{
				this.ScriptManager.RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x04000204 RID: 516
		private static readonly object TickEventKey = new object();

		// Token: 0x04000205 RID: 517
		private bool _stateDirty;

		// Token: 0x04000206 RID: 518
		private new IPage _page;

		// Token: 0x04000207 RID: 519
		private ScriptManager _scriptManager;
	}
}
