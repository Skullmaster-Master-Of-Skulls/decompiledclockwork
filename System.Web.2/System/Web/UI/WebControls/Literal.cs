using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200045B RID: 1115
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Text")]
	[Designer("System.Web.UI.Design.WebControls.LiteralDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ControlBuilder(typeof(LiteralControlBuilder))]
	public class Literal : Control, ITextControl
	{
		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x060035FC RID: 13820 RVA: 0x000AEB14 File Offset: 0x000ACD14
		// (set) Token: 0x060035FD RID: 13821 RVA: 0x000AEB3D File Offset: 0x000ACD3D
		[DefaultValue(LiteralMode.Transform)]
		[WebCategory("Behavior")]
		[WebSysDescription("Literal_Mode")]
		public LiteralMode Mode
		{
			get
			{
				object obj = this.ViewState["Mode"];
				if (obj != null)
				{
					return (LiteralMode)obj;
				}
				return LiteralMode.Transform;
			}
			set
			{
				if (value < LiteralMode.Transform || value > LiteralMode.Encode)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["Mode"] = value;
			}
		}

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x060035FE RID: 13822 RVA: 0x000AEB68 File Offset: 0x000ACD68
		// (set) Token: 0x060035FF RID: 13823 RVA: 0x00087E45 File Offset: 0x00086045
		[Localizable(true)]
		[Bindable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("Literal_Text")]
		public string Text
		{
			get
			{
				string text = (string)this.ViewState["Text"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x06003600 RID: 13824 RVA: 0x000AEB98 File Offset: 0x000ACD98
		protected override void AddParsedSubObject(object obj)
		{
			if (obj is LiteralControl)
			{
				this.Text = ((LiteralControl)obj).Text;
				return;
			}
			throw new HttpException(SR.GetString("Cannot_Have_Children_Of_Type", new object[]
			{
				"Literal",
				obj.GetType().Name.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x00060B2F File Offset: 0x0005ED2F
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x00061169 File Offset: 0x0005F369
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException(SR.GetString("NoFocusSupport", new object[]
			{
				base.GetType().Name
			}));
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x000AEBF4 File Offset: 0x000ACDF4
		protected internal override void Render(HtmlTextWriter writer)
		{
			string text = this.Text;
			if (text.Length != 0)
			{
				if (this.Mode != LiteralMode.Encode)
				{
					writer.Write(text);
					return;
				}
				HttpUtility.HtmlEncode(text, writer);
			}
		}
	}
}
