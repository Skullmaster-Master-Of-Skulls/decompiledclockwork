using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Web.Resources;

namespace System.Web.UI
{
	// Token: 0x02000044 RID: 68
	public class AsyncPostBackTrigger : UpdatePanelControlTrigger
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x000110D4 File Offset: 0x0000F2D4
		public AsyncPostBackTrigger()
		{
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000110DC File Offset: 0x0000F2DC
		internal AsyncPostBackTrigger(IScriptManagerInternal scriptManager)
		{
			this._scriptManager = scriptManager;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002AB RID: 683 RVA: 0x000110EB File Offset: 0x0000F2EB
		private static MethodInfo EventHandler
		{
			get
			{
				if (AsyncPostBackTrigger._eventHandler == null)
				{
					AsyncPostBackTrigger._eventHandler = typeof(AsyncPostBackTrigger).GetMethod("OnEvent");
				}
				return AsyncPostBackTrigger._eventHandler;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00011118 File Offset: 0x0000F318
		// (set) Token: 0x060002AD RID: 685 RVA: 0x00011120 File Offset: 0x0000F320
		[TypeConverter("System.Web.UI.Design.AsyncPostBackTriggerControlIDConverter, System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
		public new string ControlID
		{
			get
			{
				return base.ControlID;
			}
			set
			{
				base.ControlID = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00011129 File Offset: 0x0000F329
		// (set) Token: 0x060002AF RID: 687 RVA: 0x0001113F File Offset: 0x0000F33F
		[DefaultValue("")]
		[Category("Behavior")]
		[ResourceDescription("AsyncPostBackTrigger_EventName")]
		[TypeConverter("System.Web.UI.Design.AsyncPostBackTriggerEventNameConverter, System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
		public string EventName
		{
			get
			{
				if (this._eventName == null)
				{
					return string.Empty;
				}
				return this._eventName;
			}
			set
			{
				this._eventName = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00011148 File Offset: 0x0000F348
		internal IScriptManagerInternal ScriptManager
		{
			get
			{
				if (this._scriptManager == null)
				{
					Page page = base.Owner.Page;
					if (page == null)
					{
						throw new InvalidOperationException(AtlasWeb.Common_PageCannotBeNull);
					}
					this._scriptManager = System.Web.UI.ScriptManager.GetCurrent(page);
					if (this._scriptManager == null)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.Common_ScriptManagerRequired, new object[]
						{
							base.Owner.ID
						}));
					}
				}
				return this._scriptManager;
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000111BC File Offset: 0x0000F3BC
		protected internal override void Initialize()
		{
			base.Initialize();
			this._associatedControl = base.FindTargetControl(true);
			this.ScriptManager.RegisterAsyncPostBackControl(this._associatedControl);
			string eventName = this.EventName;
			if (eventName.Length != 0)
			{
				EventInfo @event = this._associatedControl.GetType().GetEvent(eventName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
				if (@event == null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.AsyncPostBackTrigger_CannotFindEvent, new object[]
					{
						eventName,
						this.ControlID,
						base.Owner.ID
					}));
				}
				MethodInfo method = @event.EventHandlerType.GetMethod("Invoke");
				ParameterInfo[] parameters = method.GetParameters();
				if (!method.ReturnType.Equals(typeof(void)) || parameters.Length != 2 || !typeof(EventArgs).IsAssignableFrom(parameters[1].ParameterType))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.AsyncPostBackTrigger_InvalidEvent, new object[]
					{
						eventName,
						this.ControlID,
						base.Owner.ID
					}));
				}
				Delegate handler = Delegate.CreateDelegate(@event.EventHandlerType, this, AsyncPostBackTrigger.EventHandler);
				@event.AddEventHandler(this._associatedControl, handler);
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000112F8 File Offset: 0x0000F4F8
		protected internal override bool HasTriggered()
		{
			if (!string.IsNullOrEmpty(this.EventName))
			{
				return this._eventHandled;
			}
			string asyncPostBackSourceElementID = this.ScriptManager.AsyncPostBackSourceElementID;
			return asyncPostBackSourceElementID == this._associatedControl.UniqueID || asyncPostBackSourceElementID.StartsWith(this._associatedControl.UniqueID + "$", StringComparison.Ordinal);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00011356 File Offset: 0x0000F556
		public void OnEvent(object sender, EventArgs e)
		{
			this._eventHandled = true;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00011360 File Offset: 0x0000F560
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.ControlID))
			{
				return "AsyncPostBack";
			}
			return "AsyncPostBack: " + this.ControlID + (string.IsNullOrEmpty(this.EventName) ? string.Empty : ("." + this.EventName));
		}

		// Token: 0x04000103 RID: 259
		private IScriptManagerInternal _scriptManager;

		// Token: 0x04000104 RID: 260
		private Control _associatedControl;

		// Token: 0x04000105 RID: 261
		private static MethodInfo _eventHandler;

		// Token: 0x04000106 RID: 262
		private bool _eventHandled;

		// Token: 0x04000107 RID: 263
		private string _eventName;
	}
}
