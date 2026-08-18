using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200190A RID: 6410
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class InputManagerClientEvents
	{
		// Token: 0x0600F8BB RID: 63675 RVA: 0x00382E5E File Offset: 0x0038105E
		internal static string ToLower(Match m)
		{
			return m.ToString().ToLower();
		}

		// Token: 0x0600F8BC RID: 63676 RVA: 0x00382E6B File Offset: 0x0038106B
		public InputManagerClientEvents(StateBag viewStateOwner)
		{
			this._viewStateOwner = new InputStateBag("inputM_events_", viewStateOwner);
		}

		// Token: 0x17004B29 RID: 19241
		// (get) Token: 0x0600F8BD RID: 63677 RVA: 0x00382E84 File Offset: 0x00381084
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public InputStateBag ViewState
		{
			get
			{
				return this._viewStateOwner;
			}
		}

		// Token: 0x0600F8BE RID: 63678 RVA: 0x00382E8C File Offset: 0x0038108C
		public override string ToString()
		{
			return "";
		}

		// Token: 0x0600F8BF RID: 63679 RVA: 0x00382E94 File Offset: 0x00381094
		internal void DescribeEvents(IScriptDescriptor descriptor)
		{
			string[] array = new string[]
			{
				"blur",
				"focus",
				"keyPress",
				"error",
				"validating",
				"valueChanged"
			};
			string[] array2 = new string[]
			{
				"OnBlur",
				"OnFocus",
				"OnKeyPress",
				"OnError",
				"OnValidating",
				"OnValueChanged"
			};
			for (int i = 0; i < array.Length; i++)
			{
				string text = (string)DataBinder.GetPropertyValue(this, array2[i]);
				if (!string.IsNullOrEmpty(text))
				{
					descriptor.AddEvent(array[i], text);
				}
			}
		}

		// Token: 0x17004B2A RID: 19242
		// (get) Token: 0x0600F8C0 RID: 63680 RVA: 0x00382F54 File Offset: 0x00381154
		// (set) Token: 0x0600F8C1 RID: 63681 RVA: 0x00382F81 File Offset: 0x00381181
		[Description("The client side event which will be fired when the input contol loses focus.")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
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

		// Token: 0x17004B2B RID: 19243
		// (get) Token: 0x0600F8C2 RID: 63682 RVA: 0x00382F94 File Offset: 0x00381194
		// (set) Token: 0x0600F8C3 RID: 63683 RVA: 0x00382FC1 File Offset: 0x003811C1
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("The client event will be fired when incorrect value is entered in the input and the validation fails.")]
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

		// Token: 0x17004B2C RID: 19244
		// (get) Token: 0x0600F8C4 RID: 63684 RVA: 0x00382FD4 File Offset: 0x003811D4
		// (set) Token: 0x0600F8C5 RID: 63685 RVA: 0x00383001 File Offset: 0x00381201
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("The client side event which will be fired on every key press when the input control is focused.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
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

		// Token: 0x17004B2D RID: 19245
		// (get) Token: 0x0600F8C6 RID: 63686 RVA: 0x00383014 File Offset: 0x00381214
		// (set) Token: 0x0600F8C7 RID: 63687 RVA: 0x00383041 File Offset: 0x00381241
		[NotifyParentProperty(true)]
		[Description("The client side event which will be fired on changing the value of the input control.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
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

		// Token: 0x17004B2E RID: 19246
		// (get) Token: 0x0600F8C8 RID: 63688 RVA: 0x00383054 File Offset: 0x00381254
		// (set) Token: 0x0600F8C9 RID: 63689 RVA: 0x00383081 File Offset: 0x00381281
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("The client side event which will be fired when a focus to the input control is given.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
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

		// Token: 0x17004B2F RID: 19247
		// (get) Token: 0x0600F8CA RID: 63690 RVA: 0x00383094 File Offset: 0x00381294
		// (set) Token: 0x0600F8CB RID: 63691 RVA: 0x003830C1 File Offset: 0x003812C1
		[Description("The client side event which will be fired before a input control is validated.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public virtual string OnValidating
		{
			get
			{
				object obj = this.ViewState["OnValidating"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["OnValidating"] = value;
			}
		}

		// Token: 0x040046CB RID: 18123
		private InputStateBag _viewStateOwner;
	}
}
