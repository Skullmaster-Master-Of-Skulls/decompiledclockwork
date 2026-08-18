using System;
using System.ComponentModel;
using System.Data;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003A6 RID: 934
	[DefaultProperty("ControlID")]
	public class ControlParameter : Parameter
	{
		// Token: 0x06002C73 RID: 11379 RVA: 0x00090DC4 File Offset: 0x0008EFC4
		public ControlParameter()
		{
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x00090DCC File Offset: 0x0008EFCC
		public ControlParameter(string name, string controlID) : base(name)
		{
			this.ControlID = controlID;
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x00090DDC File Offset: 0x0008EFDC
		public ControlParameter(string name, string controlID, string propertyName) : base(name)
		{
			this.ControlID = controlID;
			this.PropertyName = propertyName;
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x00090DF3 File Offset: 0x0008EFF3
		public ControlParameter(string name, DbType dbType, string controlID, string propertyName) : base(name, dbType)
		{
			this.ControlID = controlID;
			this.PropertyName = propertyName;
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x00090E0C File Offset: 0x0008F00C
		public ControlParameter(string name, TypeCode type, string controlID, string propertyName) : base(name, type)
		{
			this.ControlID = controlID;
			this.PropertyName = propertyName;
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x00090E25 File Offset: 0x0008F025
		protected ControlParameter(ControlParameter original) : base(original)
		{
			this.ControlID = original.ControlID;
			this.PropertyName = original.PropertyName;
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06002C79 RID: 11385 RVA: 0x00090E48 File Offset: 0x0008F048
		// (set) Token: 0x06002C7A RID: 11386 RVA: 0x00090E75 File Offset: 0x0008F075
		[DefaultValue("")]
		[IDReferenceProperty]
		[RefreshProperties(RefreshProperties.All)]
		[TypeConverter(typeof(ControlIDConverter))]
		[WebCategory("Control")]
		[WebSysDescription("ControlParameter_ControlID")]
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

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06002C7B RID: 11387 RVA: 0x00090E9C File Offset: 0x0008F09C
		// (set) Token: 0x06002C7C RID: 11388 RVA: 0x00090EC9 File Offset: 0x0008F0C9
		[DefaultValue("")]
		[TypeConverter(typeof(ControlPropertyNameConverter))]
		[WebCategory("Control")]
		[WebSysDescription("ControlParameter_PropertyName")]
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

		// Token: 0x06002C7D RID: 11389 RVA: 0x00090EF0 File Offset: 0x0008F0F0
		protected override Parameter Clone()
		{
			return new ControlParameter(this);
		}

		// Token: 0x06002C7E RID: 11390 RVA: 0x00090EF8 File Offset: 0x0008F0F8
		protected internal override object Evaluate(HttpContext context, Control control)
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
