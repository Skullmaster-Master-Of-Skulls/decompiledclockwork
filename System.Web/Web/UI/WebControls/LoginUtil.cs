using System;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Security.Principal;
using System.Web.Security;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004E3 RID: 1251
	internal static class LoginUtil
	{
		// Token: 0x06003CD8 RID: 15576 RVA: 0x001000A8 File Offset: 0x000FF0A8
		internal static void ApplyStyleToLiteral(Literal literal, string text, Style style, bool setTableCellVisible)
		{
			bool visible = false;
			if (!string.IsNullOrEmpty(text))
			{
				literal.Text = text;
				if (style != null)
				{
					LoginUtil.SetTableCellStyle(literal, style);
				}
				visible = true;
			}
			if (setTableCellVisible)
			{
				LoginUtil.SetTableCellVisible(literal, visible);
				return;
			}
			literal.Visible = visible;
		}

		// Token: 0x06003CD9 RID: 15577 RVA: 0x001000E4 File Offset: 0x000FF0E4
		internal static void CopyBorderStyles(WebControl control, Style style)
		{
			if (style == null || style.IsEmpty)
			{
				return;
			}
			control.BorderStyle = style.BorderStyle;
			control.BorderColor = style.BorderColor;
			control.BorderWidth = style.BorderWidth;
			control.BackColor = style.BackColor;
			control.CssClass = style.CssClass;
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x00100139 File Offset: 0x000FF139
		internal static void CopyStyleToInnerControl(WebControl control, Style style)
		{
			if (style == null || style.IsEmpty)
			{
				return;
			}
			control.ForeColor = style.ForeColor;
			control.Font.CopyFrom(style.Font);
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x00100164 File Offset: 0x000FF164
		internal static Table CreateChildTable(bool convertingToTemplate)
		{
			if (convertingToTemplate)
			{
				return new Table();
			}
			return new ChildTable(2);
		}

		// Token: 0x06003CDC RID: 15580 RVA: 0x00100178 File Offset: 0x000FF178
		private static MailMessage CreateMailMessage(string email, string userName, string password, MailDefinition mailDefinition, string defaultBody, Control owner)
		{
			ListDictionary listDictionary = new ListDictionary();
			if (mailDefinition.IsBodyHtml)
			{
				userName = HttpUtility.HtmlEncode(userName);
				password = HttpUtility.HtmlEncode(password);
			}
			listDictionary.Add("<%\\s*UserName\\s*%>", userName);
			listDictionary.Add("<%\\s*Password\\s*%>", password);
			if (string.IsNullOrEmpty(mailDefinition.BodyFileName) && defaultBody != null)
			{
				return mailDefinition.CreateMailMessage(email, listDictionary, defaultBody, owner);
			}
			return mailDefinition.CreateMailMessage(email, listDictionary, owner);
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x001001E4 File Offset: 0x000FF1E4
		internal static MembershipProvider GetProvider(string providerName)
		{
			MembershipProvider membershipProvider;
			if (string.IsNullOrEmpty(providerName))
			{
				membershipProvider = Membership.Provider;
			}
			else
			{
				membershipProvider = Membership.Providers[providerName];
				if (membershipProvider == null)
				{
					throw new HttpException(SR.GetString("WebControl_CantFindProvider"));
				}
			}
			return membershipProvider;
		}

		// Token: 0x06003CDE RID: 15582 RVA: 0x00100224 File Offset: 0x000FF224
		internal static IPrincipal GetUser(Control c)
		{
			IPrincipal result = null;
			Page page = c.Page;
			if (page != null)
			{
				result = page.User;
			}
			else
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext != null)
				{
					result = httpContext.User;
				}
			}
			return result;
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x00100258 File Offset: 0x000FF258
		internal static string GetUserName(Control c)
		{
			string result = null;
			IPrincipal user = LoginUtil.GetUser(c);
			if (user != null)
			{
				IIdentity identity = user.Identity;
				if (identity != null)
				{
					result = identity.Name;
				}
			}
			return result;
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x00100284 File Offset: 0x000FF284
		internal static void SendPasswordMail(string email, string userName, string password, MailDefinition mailDefinition, string defaultSubject, string defaultBody, LoginUtil.OnSendingMailDelegate onSendingMailDelegate, LoginUtil.OnSendMailErrorDelegate onSendMailErrorDelegate, Control owner)
		{
			try
			{
				new MailAddress(email);
			}
			catch (Exception e)
			{
				onSendMailErrorDelegate(new SendMailErrorEventArgs(e)
				{
					Handled = true
				});
				return;
			}
			try
			{
				using (MailMessage mailMessage = LoginUtil.CreateMailMessage(email, userName, password, mailDefinition, defaultBody, owner))
				{
					if (mailDefinition.SubjectInternal == null && defaultSubject != null)
					{
						mailMessage.Subject = defaultSubject;
					}
					MailMessageEventArgs mailMessageEventArgs = new MailMessageEventArgs(mailMessage);
					onSendingMailDelegate(mailMessageEventArgs);
					if (!mailMessageEventArgs.Cancel)
					{
						SmtpClient smtpClient = new SmtpClient();
						smtpClient.Send(mailMessage);
					}
				}
			}
			catch (Exception e2)
			{
				SendMailErrorEventArgs sendMailErrorEventArgs = new SendMailErrorEventArgs(e2);
				onSendMailErrorDelegate(sendMailErrorEventArgs);
				if (!sendMailErrorEventArgs.Handled)
				{
					throw;
				}
			}
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x00100354 File Offset: 0x000FF354
		internal static void SetTableCellStyle(Control control, Style style)
		{
			Control parent = control.Parent;
			if (parent != null)
			{
				((TableCell)parent).ApplyStyle(style);
			}
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x00100378 File Offset: 0x000FF378
		internal static void SetTableCellVisible(Control control, bool visible)
		{
			Control parent = control.Parent;
			if (parent != null)
			{
				parent.Visible = visible;
			}
		}

		// Token: 0x04002748 RID: 10056
		private const string _userNameReplacementKey = "<%\\s*UserName\\s*%>";

		// Token: 0x04002749 RID: 10057
		private const string _passwordReplacementKey = "<%\\s*Password\\s*%>";

		// Token: 0x0400274A RID: 10058
		private const string _templateDesignerRegion = "0";

		// Token: 0x020004E4 RID: 1252
		// (Invoke) Token: 0x06003CE4 RID: 15588
		internal delegate void OnSendingMailDelegate(MailMessageEventArgs e);

		// Token: 0x020004E5 RID: 1253
		// (Invoke) Token: 0x06003CE8 RID: 15592
		internal delegate void OnSendMailErrorDelegate(SendMailErrorEventArgs e);

		// Token: 0x020004E8 RID: 1256
		internal sealed class DisappearingTableRow : TableRow
		{
			// Token: 0x06003CF8 RID: 15608 RVA: 0x00100570 File Offset: 0x000FF570
			protected internal override void Render(HtmlTextWriter writer)
			{
				bool flag = false;
				foreach (object obj in this.Cells)
				{
					TableCell tableCell = (TableCell)obj;
					if (tableCell.Visible)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					base.Render(writer);
				}
			}
		}

		// Token: 0x020004E9 RID: 1257
		internal abstract class GenericContainer<ControlType> : WebControl where ControlType : WebControl
		{
			// Token: 0x06003CFA RID: 15610 RVA: 0x001005E4 File Offset: 0x000FF5E4
			public GenericContainer(ControlType owner)
			{
				this._owner = owner;
			}

			// Token: 0x17000E2F RID: 3631
			// (get) Token: 0x06003CFB RID: 15611 RVA: 0x001005F3 File Offset: 0x000FF5F3
			// (set) Token: 0x06003CFC RID: 15612 RVA: 0x001005FB File Offset: 0x000FF5FB
			internal Table BorderTable
			{
				get
				{
					return this._borderTable;
				}
				set
				{
					this._borderTable = value;
				}
			}

			// Token: 0x17000E30 RID: 3632
			// (get) Token: 0x06003CFD RID: 15613
			protected abstract bool ConvertingToTemplate { get; }

			// Token: 0x17000E31 RID: 3633
			// (get) Token: 0x06003CFE RID: 15614 RVA: 0x00100604 File Offset: 0x000FF604
			// (set) Token: 0x06003CFF RID: 15615 RVA: 0x0010060C File Offset: 0x000FF60C
			internal Table LayoutTable
			{
				get
				{
					return this._layoutTable;
				}
				set
				{
					this._layoutTable = value;
				}
			}

			// Token: 0x17000E32 RID: 3634
			// (get) Token: 0x06003D00 RID: 15616 RVA: 0x00100615 File Offset: 0x000FF615
			internal ControlType Owner
			{
				get
				{
					return this._owner;
				}
			}

			// Token: 0x17000E33 RID: 3635
			// (get) Token: 0x06003D01 RID: 15617 RVA: 0x0010061D File Offset: 0x000FF61D
			// (set) Token: 0x06003D02 RID: 15618 RVA: 0x0010062F File Offset: 0x000FF62F
			internal bool RenderDesignerRegion
			{
				get
				{
					return base.DesignMode && this._renderDesignerRegion;
				}
				set
				{
					this._renderDesignerRegion = value;
				}
			}

			// Token: 0x17000E34 RID: 3636
			// (get) Token: 0x06003D03 RID: 15619 RVA: 0x00100638 File Offset: 0x000FF638
			private bool UsingDefaultTemplate
			{
				get
				{
					return this.BorderTable != null;
				}
			}

			// Token: 0x06003D04 RID: 15620 RVA: 0x00100648 File Offset: 0x000FF648
			public sealed override void Focus()
			{
				throw new NotSupportedException(SR.GetString("NoFocusSupport", new object[]
				{
					base.GetType().Name
				}));
			}

			// Token: 0x06003D05 RID: 15621 RVA: 0x0010067C File Offset: 0x000FF67C
			private Control FindControl<RequiredType>(string id, bool required, string errorResourceKey)
			{
				Control control = this.FindControl(id);
				if (control is RequiredType)
				{
					return control;
				}
				if (required)
				{
					ControlType owner = this.Owner;
					if (!owner.DesignMode)
					{
						object[] array = new object[2];
						object[] array2 = array;
						int num = 0;
						ControlType owner2 = this.Owner;
						array2[num] = owner2.ID;
						array[1] = id;
						throw new HttpException(SR.GetString(errorResourceKey, array));
					}
				}
				return null;
			}

			// Token: 0x06003D06 RID: 15622 RVA: 0x001006E4 File Offset: 0x000FF6E4
			protected Control FindOptionalControl<RequiredType>(string id)
			{
				return this.FindControl<RequiredType>(id, false, null);
			}

			// Token: 0x06003D07 RID: 15623 RVA: 0x001006EF File Offset: 0x000FF6EF
			protected Control FindRequiredControl<RequiredType>(string id, string errorResourceKey)
			{
				return this.FindControl<RequiredType>(id, true, errorResourceKey);
			}

			// Token: 0x06003D08 RID: 15624 RVA: 0x001006FC File Offset: 0x000FF6FC
			protected internal sealed override void Render(HtmlTextWriter writer)
			{
				if (this.UsingDefaultTemplate)
				{
					if (!this.ConvertingToTemplate)
					{
						this.BorderTable.CopyBaseAttributes(this);
						if (base.ControlStyleCreated)
						{
							LoginUtil.CopyBorderStyles(this.BorderTable, base.ControlStyle);
							LoginUtil.CopyStyleToInnerControl(this.LayoutTable, base.ControlStyle);
						}
					}
					this.LayoutTable.Height = this.Height;
					this.LayoutTable.Width = this.Width;
					this.RenderContents(writer);
					return;
				}
				this.RenderContentsInUnitTable(writer);
			}

			// Token: 0x06003D09 RID: 15625 RVA: 0x00100780 File Offset: 0x000FF780
			private void RenderContentsInUnitTable(HtmlTextWriter writer)
			{
				LayoutTable layoutTable = new LayoutTable(1, 1, this.Page);
				if (this.RenderDesignerRegion)
				{
					layoutTable[0, 0].Attributes["_designerRegion"] = "0";
				}
				else
				{
					foreach (object obj in this.Controls)
					{
						Control child = (Control)obj;
						layoutTable[0, 0].Controls.Add(child);
					}
				}
				string id = this.Parent.ID;
				if (id != null && id.Length != 0)
				{
					layoutTable.ID = this.Parent.ClientID;
				}
				layoutTable.CopyBaseAttributes(this);
				layoutTable.ApplyStyle(base.ControlStyle);
				layoutTable.CellPadding = 0;
				layoutTable.CellSpacing = 0;
				layoutTable.RenderControl(writer);
			}

			// Token: 0x06003D0A RID: 15626 RVA: 0x0010086C File Offset: 0x000FF86C
			protected void VerifyControlNotPresent<RequiredType>(string id, string errorResourceKey)
			{
				Control control = this.FindOptionalControl<RequiredType>(id);
				if (control != null)
				{
					ControlType owner = this.Owner;
					if (!owner.DesignMode)
					{
						object[] array = new object[2];
						object[] array2 = array;
						int num = 0;
						ControlType owner2 = this.Owner;
						array2[num] = owner2.ID;
						array[1] = id;
						throw new HttpException(SR.GetString(errorResourceKey, array));
					}
				}
			}

			// Token: 0x0400274C RID: 10060
			private bool _renderDesignerRegion;

			// Token: 0x0400274D RID: 10061
			private ControlType _owner;

			// Token: 0x0400274E RID: 10062
			private Table _layoutTable;

			// Token: 0x0400274F RID: 10063
			private Table _borderTable;
		}
	}
}
