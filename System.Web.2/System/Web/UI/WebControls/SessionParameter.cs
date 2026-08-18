using System;
using System.ComponentModel;
using System.Data;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004C6 RID: 1222
	[DefaultProperty("SessionField")]
	public class SessionParameter : Parameter
	{
		// Token: 0x06003CBC RID: 15548 RVA: 0x00090DC4 File Offset: 0x0008EFC4
		public SessionParameter()
		{
		}

		// Token: 0x06003CBD RID: 15549 RVA: 0x000C489A File Offset: 0x000C2A9A
		public SessionParameter(string name, string sessionField) : base(name)
		{
			this.SessionField = sessionField;
		}

		// Token: 0x06003CBE RID: 15550 RVA: 0x000C48AA File Offset: 0x000C2AAA
		public SessionParameter(string name, DbType dbType, string sessionField) : base(name, dbType)
		{
			this.SessionField = sessionField;
		}

		// Token: 0x06003CBF RID: 15551 RVA: 0x000C48BB File Offset: 0x000C2ABB
		public SessionParameter(string name, TypeCode type, string sessionField) : base(name, type)
		{
			this.SessionField = sessionField;
		}

		// Token: 0x06003CC0 RID: 15552 RVA: 0x000C48CC File Offset: 0x000C2ACC
		protected SessionParameter(SessionParameter original) : base(original)
		{
			this.SessionField = original.SessionField;
		}

		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x06003CC1 RID: 15553 RVA: 0x000C48E4 File Offset: 0x000C2AE4
		// (set) Token: 0x06003CC2 RID: 15554 RVA: 0x000C4911 File Offset: 0x000C2B11
		[DefaultValue("")]
		[WebCategory("Parameter")]
		[WebSysDescription("SessionParameter_SessionField")]
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

		// Token: 0x06003CC3 RID: 15555 RVA: 0x000C4938 File Offset: 0x000C2B38
		protected override Parameter Clone()
		{
			return new SessionParameter(this);
		}

		// Token: 0x06003CC4 RID: 15556 RVA: 0x000C4940 File Offset: 0x000C2B40
		protected internal override object Evaluate(HttpContext context, Control control)
		{
			if (context == null || context.Session == null)
			{
				return null;
			}
			return context.Session[this.SessionField];
		}
	}
}
