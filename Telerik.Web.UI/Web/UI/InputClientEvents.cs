using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020012AE RID: 4782
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class InputClientEvents
	{
		// Token: 0x0600C813 RID: 51219 RVA: 0x002C964D File Offset: 0x002C784D
		internal static string ToLower(Match m)
		{
			return m.ToString().ToLower();
		}

		// Token: 0x0600C814 RID: 51220 RVA: 0x002C965A File Offset: 0x002C785A
		public InputClientEvents(StateBag viewStateOwner)
		{
			this._viewStateOwner = new InputStateBag("input_events_", viewStateOwner);
		}

		// Token: 0x170040A2 RID: 16546
		// (get) Token: 0x0600C815 RID: 51221 RVA: 0x002C9673 File Offset: 0x002C7873
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public InputStateBag ViewState
		{
			get
			{
				return this._viewStateOwner;
			}
		}

		// Token: 0x0600C816 RID: 51222 RVA: 0x002C967B File Offset: 0x002C787B
		public override string ToString()
		{
			return "";
		}

		// Token: 0x0600C817 RID: 51223 RVA: 0x002C9684 File Offset: 0x002C7884
		internal void DescribeEvents(IScriptDescriptor descriptor)
		{
			string[] array = new string[]
			{
				"Blur",
				"ButtonClick",
				"Disable",
				"Enable",
				"EnumerationChanged",
				"Error",
				"Focus",
				"KeyPress",
				"Load",
				"MouseOut",
				"MouseOver",
				"MoveDown",
				"MoveUp",
				"ValueChanging",
				"ValueChanged"
			};
			foreach (string text in array)
			{
				string text2 = (string)DataBinder.GetPropertyValue(this, string.Format("On{0}", text));
				if (!string.IsNullOrEmpty(text2))
				{
					descriptor.AddEvent(Regex.Replace(text, "^[A-Z]", new MatchEvaluator(InputClientEvents.ToLower)), text2);
				}
			}
		}

		// Token: 0x170040A3 RID: 16547
		// (get) Token: 0x0600C818 RID: 51224 RVA: 0x002C9774 File Offset: 0x002C7974
		// (set) Token: 0x0600C819 RID: 51225 RVA: 0x002C97A1 File Offset: 0x002C79A1
		[NotifyParentProperty(true)]
		[Category("Client-side events")]
		[Description("The client event will be fired when incorrect value is entered in the input and the validation fails.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		public virtual string OnError
		{
			get
			{
				object obj = this.ViewState["OnError"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["OnError"] = value;
			}
		}

		// Token: 0x170040A4 RID: 16548
		// (get) Token: 0x0600C81A RID: 51226 RVA: 0x002C97B4 File Offset: 0x002C79B4
		// (set) Token: 0x0600C81B RID: 51227 RVA: 0x002C97E1 File Offset: 0x002C79E1
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("The client event will be fired when the input controls is clicked.")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string OnButtonClick
		{
			get
			{
				object obj = this.ViewState["OnButtonClick"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["OnButtonClick"] = value;
			}
		}

		// Token: 0x170040A5 RID: 16549
		// (get) Token: 0x0600C81C RID: 51228 RVA: 0x002C97F4 File Offset: 0x002C79F4
		// (set) Token: 0x0600C81D RID: 51229 RVA: 0x002C9821 File Offset: 0x002C7A21
		[Description("The client event is called when the input loads.")]
		[NotifyParentProperty(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		public virtual string OnLoad
		{
			get
			{
				object obj = this.ViewState["OnLoad"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["OnLoad"] = value;
			}
		}

		// Token: 0x170040A6 RID: 16550
		// (get) Token: 0x0600C81E RID: 51230 RVA: 0x002C9834 File Offset: 0x002C7A34
		// (set) Token: 0x0600C81F RID: 51231 RVA: 0x002C9861 File Offset: 0x002C7A61
		[Category("Client-side events")]
		[Description("The client side event will be fired when the user mouse leaves the input control.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string OnMouseOut
		{
			get
			{
				object obj = this.ViewState["OnMouseOut"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["OnMouseOut"] = value;
			}
		}

		// Token: 0x170040A7 RID: 16551
		// (get) Token: 0x0600C820 RID: 51232 RVA: 0x002C9874 File Offset: 0x002C7A74
		// (set) Token: 0x0600C821 RID: 51233 RVA: 0x002C98A1 File Offset: 0x002C7AA1
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The client side event which will be fired when the user mouse enters the input control area.")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnMouseOver
		{
			get
			{
				object obj = this.ViewState["OnMouseOver"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["OnMouseOver"] = value;
			}
		}

		// Token: 0x170040A8 RID: 16552
		// (get) Token: 0x0600C822 RID: 51234 RVA: 0x002C98B4 File Offset: 0x002C7AB4
		// (set) Token: 0x0600C823 RID: 51235 RVA: 0x002C98E1 File Offset: 0x002C7AE1
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The client side event which will be fired when a focus to the input control is given.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnFocus
		{
			get
			{
				object obj = this.ViewState["OnFocus"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["OnFocus"] = value;
			}
		}

		// Token: 0x170040A9 RID: 16553
		// (get) Token: 0x0600C824 RID: 51236 RVA: 0x002C98F4 File Offset: 0x002C7AF4
		// (set) Token: 0x0600C825 RID: 51237 RVA: 0x002C9921 File Offset: 0x002C7B21
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("The client side event which will be fired when the input contol loses focus.")]
		public virtual string OnBlur
		{
			get
			{
				object obj = this.ViewState["OnBlur"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["OnBlur"] = value;
			}
		}

		// Token: 0x170040AA RID: 16554
		// (get) Token: 0x0600C826 RID: 51238 RVA: 0x002C9934 File Offset: 0x002C7B34
		// (set) Token: 0x0600C827 RID: 51239 RVA: 0x002C9961 File Offset: 0x002C7B61
		[Description("The client side event which will be fired when the input control is disabled.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnDisable
		{
			get
			{
				object obj = this.ViewState["OnDisable"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["OnDisable"] = value;
			}
		}

		// Token: 0x170040AB RID: 16555
		// (get) Token: 0x0600C828 RID: 51240 RVA: 0x002C9974 File Offset: 0x002C7B74
		// (set) Token: 0x0600C829 RID: 51241 RVA: 0x002C99A1 File Offset: 0x002C7BA1
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The client side event which will be fired when the input control is enabled.")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnEnable
		{
			get
			{
				object obj = this.ViewState["OnEnable"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["OnEnable"] = value;
			}
		}

		// Token: 0x170040AC RID: 16556
		// (get) Token: 0x0600C82A RID: 51242 RVA: 0x002C99B4 File Offset: 0x002C7BB4
		// (set) Token: 0x0600C82B RID: 51243 RVA: 0x002C99E1 File Offset: 0x002C7BE1
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The client side event which will be fired before the input control value is changed. The event could be canceled.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnValueChanging
		{
			get
			{
				object obj = this.ViewState["OnValueChanging"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["OnValueChanging"] = value;
			}
		}

		// Token: 0x170040AD RID: 16557
		// (get) Token: 0x0600C82C RID: 51244 RVA: 0x002C99F4 File Offset: 0x002C7BF4
		// (set) Token: 0x0600C82D RID: 51245 RVA: 0x002C9A21 File Offset: 0x002C7C21
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The client side event which will be fired after the input control value is changed.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnValueChanged
		{
			get
			{
				object obj = this.ViewState["OnValueChanged"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["OnValueChanged"] = value;
			}
		}

		// Token: 0x170040AE RID: 16558
		// (get) Token: 0x0600C82E RID: 51246 RVA: 0x002C9A34 File Offset: 0x002C7C34
		// (set) Token: 0x0600C82F RID: 51247 RVA: 0x002C9A61 File Offset: 0x002C7C61
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The client side event which will be fired on every key press when the input control is focused.")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnKeyPress
		{
			get
			{
				object obj = this.ViewState["OnKeyPress"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["OnKeyPress"] = value;
			}
		}

		// Token: 0x170040AF RID: 16559
		// (get) Token: 0x0600C830 RID: 51248 RVA: 0x002C9A74 File Offset: 0x002C7C74
		// (set) Token: 0x0600C831 RID: 51249 RVA: 0x002C9AA1 File Offset: 0x002C7CA1
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Fired whenever the value of any enumeration mask part has changed.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnEnumerationChanged
		{
			get
			{
				object obj = this.ViewState["OnEnumerationChanged"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["OnEnumerationChanged"] = value;
			}
		}

		// Token: 0x170040B0 RID: 16560
		// (get) Token: 0x0600C832 RID: 51250 RVA: 0x002C9AB4 File Offset: 0x002C7CB4
		// (set) Token: 0x0600C833 RID: 51251 RVA: 0x002C9AE1 File Offset: 0x002C7CE1
		[Category("Client-side events")]
		[Description("Fired whenever the user increases the value of any enumeration or numeric range mask part of RadMaskedTextBox.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string OnMoveUp
		{
			get
			{
				object obj = this.ViewState["OnMoveUp"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["OnMoveUp"] = value;
			}
		}

		// Token: 0x170040B1 RID: 16561
		// (get) Token: 0x0600C834 RID: 51252 RVA: 0x002C9AF4 File Offset: 0x002C7CF4
		// (set) Token: 0x0600C835 RID: 51253 RVA: 0x002C9B21 File Offset: 0x002C7D21
		[Description("Fired whenever the user decreases the value of any enumeration or numeric range mask part of RadMaskedTextBox.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnMoveDown
		{
			get
			{
				object obj = this.ViewState["OnMoveDown"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["OnMoveDown"] = value;
			}
		}

		// Token: 0x040034B8 RID: 13496
		private InputStateBag _viewStateOwner;
	}
}
