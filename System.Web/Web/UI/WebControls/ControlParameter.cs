using System;
using System.ComponentModel;
using System.Data;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200050A RID: 1290
	[DefaultProperty("ControlID")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ControlParameter : Parameter
	{
		// Token: 0x06003EF0 RID: 16112 RVA: 0x00105BCD File Offset: 0x00104BCD
		public ControlParameter()
		{
		}

		// Token: 0x06003EF1 RID: 16113 RVA: 0x00105BD5 File Offset: 0x00104BD5
		public ControlParameter(string name, string controlID) : base(name)
		{
			this.ControlID = controlID;
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x00105BE5 File Offset: 0x00104BE5
		public ControlParameter(string name, string controlID, string propertyName) : base(name)
		{
			this.ControlID = controlID;
			this.PropertyName = propertyName;
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x00105BFC File Offset: 0x00104BFC
		public ControlParameter(string name, DbType dbType, string controlID, string propertyName) : base(name, dbType)
		{
			this.ControlID = controlID;
			this.PropertyName = propertyName;
		}

		// Token: 0x06003EF4 RID: 16116 RVA: 0x00105C15 File Offset: 0x00104C15
		public ControlParameter(string name, TypeCode type, string controlID, string propertyName) : base(name, type)
		{
			this.ControlID = controlID;
			this.PropertyName = propertyName;
		}

		// Token: 0x06003EF5 RID: 16117 RVA: 0x00105C2E File Offset: 0x00104C2E
		protected ControlParameter(ControlParameter original) : base(original)
		{
			this.ControlID = original.ControlID;
			this.PropertyName = original.PropertyName;
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06003EF6 RID: 16118 RVA: 0x00105C50 File Offset: 0x00104C50
		// (set) Token: 0x06003EF7 RID: 16119 RVA: 0x00105C7D File Offset: 0x00104C7D
		[WebCategory("Control")]
		[WebSysDescription("ControlParameter_ControlID")]
		[RefreshProperties(RefreshProperties.All)]
		[TypeConverter(typeof(ControlIDConverter))]
		[DefaultValue("")]
		[IDReferenceProperty]
		public string ControlID
		{
			get
			{
				object obj = base.ViewState["ControlID"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				if (this.ControlID != value)
				{
					base.ViewState["ControlID"] = value;
					base.OnParameterChanged();
				}
			}
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06003EF8 RID: 16120 RVA: 0x00105CA4 File Offset: 0x00104CA4
		// (set) Token: 0x06003EF9 RID: 16121 RVA: 0x00105CD1 File Offset: 0x00104CD1
		[DefaultValue("")]
		[WebCategory("Control")]
		[WebSysDescription("ControlParameter_PropertyName")]
		[TypeConverter(typeof(ControlPropertyNameConverter))]
		public string PropertyName
		{
			get
			{
				object obj = base.ViewState["PropertyName"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				if (this.PropertyName != value)
				{
					base.ViewState["PropertyName"] = value;
					base.OnParameterChanged();
				}
			}
		}

		// Token: 0x06003EFA RID: 16122 RVA: 0x00105CF8 File Offset: 0x00104CF8
		protected override Parameter Clone()
		{
			return new ControlParameter(this);
		}

		// Token: 0x06003EFB RID: 16123 RVA: 0x00105D00 File Offset: 0x00104D00
		protected override object Evaluate(HttpContext context, Control control)
		{
			if (control == null)
			{
				return null;
			}
			string controlID = this.ControlID;
			string text = this.PropertyName;
			if (controlID.Length == 0)
			{
				throw new ArgumentException(SR.GetString("ControlParameter_ControlIDNotSpecified", new object[]
				{
					base.Name
				}));
			}
			Control control2 = DataBoundControlHelper.FindControl(control, controlID);
			if (control2 == null)
			{
				throw new InvalidOperationException(SR.GetString("ControlParameter_CouldNotFindControl", new object[]
				{
					controlID,
					base.Name
				}));
			}
			ControlValuePropertyAttribute controlValuePropertyAttribute = (ControlValuePropertyAttribute)TypeDescriptor.GetAttributes(control2)[typeof(ControlValuePropertyAttribute)];
			if (text.Length == 0)
			{
				if (controlValuePropertyAttribute == null || string.IsNullOrEmpty(controlValuePropertyAttribute.Name))
				{
					throw new InvalidOperationException(SR.GetString("ControlParameter_PropertyNameNotSpecified", new object[]
					{
						controlID,
						base.Name
					}));
				}
				text = controlValuePropertyAttribute.Name;
			}
			object obj = DataBinder.Eval(control2, text);
			if (controlValuePropertyAttribute != null && string.Equals(controlValuePropertyAttribute.Name, text, StringComparison.OrdinalIgnoreCase) && controlValuePropertyAttribute.DefaultValue != null && controlValuePropertyAttribute.DefaultValue.Equals(obj))
			{
				return null;
			}
			return obj;
		}
	}
}
