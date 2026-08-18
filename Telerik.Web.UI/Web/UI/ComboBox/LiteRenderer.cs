using System;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.Common.Helpers;

namespace Telerik.Web.UI.ComboBox
{
	// Token: 0x02000A16 RID: 2582
	public class LiteRenderer : DecoratedRendererBase
	{
		// Token: 0x060061E5 RID: 25061 RVA: 0x00171AC7 File Offset: 0x0016FCC7
		public LiteRenderer(RadComboBox owner) : base(owner)
		{
		}

		// Token: 0x060061E6 RID: 25062 RVA: 0x00171AD0 File Offset: 0x0016FCD0
		protected string GetFullID(string id)
		{
			return string.Format("{0}_{1}", base.Owner.ClientID, id);
		}

		// Token: 0x060061E7 RID: 25063 RVA: 0x00171AE8 File Offset: 0x0016FCE8
		protected string GetInnerWrapCssClass()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("rcbInner");
			if (base.Owner.ReadOnly)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append("rcbReadOnly");
			}
			if (!base.Owner.IsControlEnabled)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append("rcbDisabled");
			}
			if (!base.Owner.ShowToggleImage)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append("rcbToggleButtonHidden");
			}
			if (base.Owner.RadComboBoxImagePosition == RadComboBoxImagePosition.Left)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append("rcbLeftImage");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060061E8 RID: 25064 RVA: 0x00171BA4 File Offset: 0x0016FDA4
		protected override void RenderWrapper(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.GetInnerWrapCssClass());
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			Control control = base.CreateInput();
			control.RenderControl(writer);
			if (base.Owner.ShowToggleImage)
			{
				this.RenderButton(writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x060061E9 RID: 25065 RVA: 0x00171BF0 File Offset: 0x0016FDF0
		protected void RenderButton(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcbActionButton");
			writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, "-1");
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
			if (!base.Owner.IsControlEnabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Button);
			this.RenderIconDown(writer);
			this.RenderButtonText(writer);
			writer.RenderEndTag();
		}

		// Token: 0x060061EA RID: 25066 RVA: 0x00171C5A File Offset: 0x0016FE5A
		protected void RenderIconDown(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.GetFullID("Arrow"));
			IconHelper.RenderIcon(writer, "arrow-60-down");
		}

		// Token: 0x060061EB RID: 25067 RVA: 0x00171C7A File Offset: 0x0016FE7A
		protected void RenderButtonText(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcbButtonText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("select");
			writer.RenderEndTag();
		}

		// Token: 0x040017F6 RID: 6134
		public const string WhiteSpaceEncoded = "<!-- &nbsp; -->";

		// Token: 0x040017F7 RID: 6135
		private const string ButtonTabIndex = "-1";
	}
}
