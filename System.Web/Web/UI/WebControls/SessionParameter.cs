using System;
using System.ComponentModel;
using System.Data;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200063D RID: 1597
	[DefaultProperty("SessionField")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class SessionParameter : Parameter
	{
		// Token: 0x06004EC3 RID: 20163 RVA: 0x0013E233 File Offset: 0x0013D233
		public SessionParameter()
		{
		}

		// Token: 0x06004EC4 RID: 20164 RVA: 0x0013E23B File Offset: 0x0013D23B
		public SessionParameter(string name, string sessionField) : base(name)
		{
			this.SessionField = sessionField;
		}

		// Token: 0x06004EC5 RID: 20165 RVA: 0x0013E24B File Offset: 0x0013D24B
		public SessionParameter(string name, DbType dbType, string sessionField) : base(name, dbType)
		{
			this.SessionField = sessionField;
		}

		// Token: 0x06004EC6 RID: 20166 RVA: 0x0013E25C File Offset: 0x0013D25C
		public SessionParameter(string name, TypeCode type, string sessionField) : base(name, type)
		{
			this.SessionField = sessionField;
		}

		// Token: 0x06004EC7 RID: 20167 RVA: 0x0013E26D File Offset: 0x0013D26D
		protected SessionParameter(SessionParameter original) : base(original)
		{
			this.SessionField = original.SessionField;
		}

		// Token: 0x170013EC RID: 5100
		// (get) Token: 0x06004EC8 RID: 20168 RVA: 0x0013E284 File Offset: 0x0013D284
		// (set) Token: 0x06004EC9 RID: 20169 RVA: 0x0013E2B1 File Offset: 0x0013D2B1
		[DefaultValue("")]
		[WebSysDescription("SessionParameter_SessionField")]
		[WebCategory("Parameter")]
		public string SessionField
		{
			get
			{
				object obj = base.ViewState["SessionField"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				if (this.SessionField != value)
				{
					base.ViewState["SessionField"] = value;
					base.OnParameterChanged();
				}
			}
		}

		// Token: 0x06004ECA RID: 20170 RVA: 0x0013E2D8 File Offset: 0x0013D2D8
		protected override Parameter Clone()
		{
			return new SessionParameter(this);
		}

		// Token: 0x06004ECB RID: 20171 RVA: 0x0013E2E0 File Offset: 0x0013D2E0
		protected override object Evaluate(HttpContext context, Control control)
		{
			if (context == null || context.Session == null)
			{
				return null;
			}
			return context.Session[this.SessionField];
		}
	}
}
