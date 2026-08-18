using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.Resources;

namespace System.Web.UI
{
	// Token: 0x02000060 RID: 96
	public class PostBackTrigger : UpdatePanelControlTrigger
	{
		// Token: 0x0600038B RID: 907 RVA: 0x000110D4 File Offset: 0x0000F2D4
		public PostBackTrigger()
		{
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000134F0 File Offset: 0x000116F0
		internal PostBackTrigger(IScriptManagerInternal scriptManager)
		{
			this._scriptManager = scriptManager;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00011118 File Offset: 0x0000F318
		// (set) Token: 0x0600038E RID: 910 RVA: 0x00011120 File Offset: 0x0000F320
		[TypeConverter("System.Web.UI.Design.PostBackTriggerControlIDConverter, System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
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

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00013500 File Offset: 0x00011700
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

		// Token: 0x06000390 RID: 912 RVA: 0x00013574 File Offset: 0x00011774
		protected internal override void Initialize()
		{
			base.Initialize();
			Control control = base.FindTargetControl(false);
			this.ScriptManager.RegisterPostBackControl(control);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0001359B File Offset: 0x0001179B
		protected internal override bool HasTriggered()
		{
			return false;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001359E File Offset: 0x0001179E
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.ControlID))
			{
				return "PostBack";
			}
			return "PostBack: " + this.ControlID;
		}

		// Token: 0x0400014F RID: 335
		private IScriptManagerInternal _scriptManager;
	}
}
