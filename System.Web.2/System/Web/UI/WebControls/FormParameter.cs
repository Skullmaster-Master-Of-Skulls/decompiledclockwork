using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003FD RID: 1021
	[DefaultProperty("FormField")]
	public class FormParameter : Parameter
	{
		// Token: 0x06003130 RID: 12592 RVA: 0x00090DC4 File Offset: 0x0008EFC4
		public FormParameter()
		{
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x000A045B File Offset: 0x0009E65B
		public FormParameter(string name, string formField) : base(name)
		{
			this.FormField = formField;
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x000A046B File Offset: 0x0009E66B
		public FormParameter(string name, DbType dbType, string formField) : base(name, dbType)
		{
			this.FormField = formField;
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x000A047C File Offset: 0x0009E67C
		public FormParameter(string name, TypeCode type, string formField) : base(name, type)
		{
			this.FormField = formField;
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x000A048D File Offset: 0x0009E68D
		protected FormParameter(FormParameter original) : base(original)
		{
			this.FormField = original.FormField;
			this.ValidateInput = original.ValidateInput;
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x06003135 RID: 12597 RVA: 0x000A04B0 File Offset: 0x0009E6B0
		// (set) Token: 0x06003136 RID: 12598 RVA: 0x000A04DD File Offset: 0x0009E6DD
		[DefaultValue("")]
		[WebCategory("Parameter")]
		[WebSysDescription("FormParameter_FormField")]
		public string FormField
		{
			get
			{
				object obj = base.ViewState["FormField"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				if (this.FormField != value)
				{
					base.ViewState["FormField"] = value;
					base.OnParameterChanged();
				}
			}
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x000A0504 File Offset: 0x0009E704
		protected override Parameter Clone()
		{
			return new FormParameter(this);
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x000A050C File Offset: 0x0009E70C
		protected internal override object Evaluate(HttpContext context, Control control)
		{
			if (context == null || context.Request == null)
			{
				return null;
			}
			NameValueCollection nameValueCollection = this.ValidateInput ? context.Request.Form : context.Request.Unvalidated.Form;
			return nameValueCollection[this.FormField];
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x06003139 RID: 12601 RVA: 0x000A0558 File Offset: 0x0009E758
		// (set) Token: 0x0600313A RID: 12602 RVA: 0x000A0581 File Offset: 0x0009E781
		[WebCategory("Behavior")]
		[WebSysDescription("Parameter_ValidateInput")]
		[DefaultValue(true)]
		public bool ValidateInput
		{
			get
			{
				object obj = base.ViewState["ValidateInput"];
				return obj == null || (bool)obj;
			}
			set
			{
				if (this.ValidateInput != value)
				{
					base.ViewState["ValidateInput"] = value;
					base.OnParameterChanged();
				}
			}
		}
	}
}
