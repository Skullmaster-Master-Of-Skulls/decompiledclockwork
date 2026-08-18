using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Net.Mail;
using System.Security.Principal;
using System.Web.Security;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000465 RID: 1125
	internal static class LoginUtil
	{
		// Token: 0x060036A8 RID: 13992 RVA: 0x000B0E10 File Offset: 0x000AF010
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

		// Token: 0x060036A9 RID: 13993 RVA: 0x000B0E4C File Offset: 0x000AF04C
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

		// Token: 0x060036AA RID: 13994 RVA: 0x000B0EA1 File Offset: 0x000AF0A1
		internal static void CopyStyleToInnerControl(WebControl control, Style style)
		{
			if (style == null || style.IsEmpty)
			{
				return;
			}
			control.ForeColor = style.ForeColor;
			control.Font.CopyFrom(style.Font);
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x000B0ECC File Offset: 0x000AF0CC
		internal static Table CreateChildTable(bool convertingToTemplate)
		{
			if (convertingToTemplate)
			{
				return new Table();
			}
			return new ChildTable(2);
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x000B0EE0 File Offset: 0x000AF0E0
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

		// Token: 0x060036AD RID: 13997 RVA: 0x000B0F4C File Offset: 0x000AF14C
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

		// Token: 0x060036AE RID: 13998 RVA: 0x000B0F8C File Offset: 0x000AF18C
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

		// Token: 0x060036AF RID: 13999 RVA: 0x000B0FC0 File Offset: 0x000AF1C0
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

		// Token: 0x060036B0 RID: 14000 RVA: 0x000B0FEC File Offset: 0x000AF1EC
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

		// Token: 0x060036B1 RID: 14001 RVA: 0x000B10C0 File Offset: 0x000AF2C0
		internal static void SetTableCellStyle(Control control, Style style)
		{
			Control parent = control.Parent;
			if (parent != null)
			{
				((TableCell)parent).ApplyStyle(style);
			}
		}

		// Token: 0x060036B2 RID: 14002 RVA: 0x000B10E4 File Offset: 0x000AF2E4
		internal static void SetTableCellVisible(Control control, bool visible)
		{
			Control parent = control.Parent;
			if (parent != null)
			{
				parent.Visible = visible;
			}
		}

		// Token: 0x060036B3 RID: 14003 RVA: 0x000B1104 File Offset: 0x000AF304
		internal static string ModifiedOuterTableBasicStylePropertyName(WebControl control)
		{
			if (control.BackColor != Color.Empty)
			{
				return "BackColor";
			}
			if (control.BorderColor != Color.Empty)
			{
				return "BorderColor";
			}
			if (control.BorderWidth != Unit.Empty)
			{
				return "BorderWidth";
			}
			if (control.BorderStyle != BorderStyle.NotSet)
			{
				return "BorderStyle";
			}
			if (!string.IsNullOrEmpty(control.CssClass))
			{
				return "CssClass";
			}
			if (control.ForeColor != Color.Empty)
			{
				return "ForeColor";
			}
			if (control.Height != Unit.Empty)
			{
				return "Height";
			}
			if (control.Width != Unit.Empty)
			{
				return "Width";
			}
			return string.Empty;
		}

		// Token: 0x04002216 RID: 8726
		private const string _userNameReplacementKey = "<%\\s*UserName\\s*%>";

		// Token: 0x04002217 RID: 8727
		private const string _passwordReplacementKey = "<%\\s*Password\\s*%>";

		// Token: 0x04002218 RID: 8728
		private const string _templateDesignerRegion = "0";

		// Token: 0x020009A6 RID: 2470
		// (Invoke) Token: 0x06006B96 RID: 27542
		internal delegate void OnSendingMailDelegate(MailMessageEventArgs e);

		// Token: 0x020009A7 RID: 2471
		// (Invoke) Token: 0x06006B9A RID: 27546
		internal delegate void OnSendMailErrorDelegate(SendMailErrorEventArgs e);

		// Token: 0x020009A8 RID: 2472
		internal sealed class DisappearingTableRow : TableRow
		{
			// Token: 0x06006B9D RID: 27549 RVA: 0x0017FC04 File Offset: 0x0017DE04
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

		// Token: 0x020009A9 RID: 2473
		internal abstract class GenericContainer<ControlType> : WebControl where ControlType : WebControl, IBorderPaddingControl, IRenderOuterTableControl
		{
			// Token: 0x06006B9F RID: 27551 RVA: 0x0017FC70 File Offset: 0x0017DE70
			public GenericContainer(ControlType owner)
			{
				this._owner = owner;
			}

			// Token: 0x17001DB2 RID: 7602
			// (get) Token: 0x06006BA0 RID: 27552 RVA: 0x0017FC7F File Offset: 0x0017DE7F
			internal int BorderPadding
			{
				get
				{
					return this._owner.BorderPadding;
				}
			}

			// Token: 0x17001DB3 RID: 7603
			// (get) Token: 0x06006BA1 RID: 27553 RVA: 0x0017FC91 File Offset: 0x0017DE91
			// (set) Token: 0x06006BA2 RID: 27554 RVA: 0x0017FC99 File Offset: 0x0017DE99
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

			// Token: 0x17001DB4 RID: 7604
			// (get) Token: 0x06006BA3 RID: 27555
			protected abstract bool ConvertingToTemplate { get; }

			// Token: 0x17001DB5 RID: 7605
			// (get) Token: 0x06006BA4 RID: 27556 RVA: 0x0017FCA2 File Offset: 0x0017DEA2
			// (set) Token: 0x06006BA5 RID: 27557 RVA: 0x0017FCAA File Offset: 0x0017DEAA
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

			// Token: 0x17001DB6 RID: 7606
			// (get) Token: 0x06006BA6 RID: 27558 RVA: 0x0017FCB3 File Offset: 0x0017DEB3
			internal ControlType Owner
			{
				get
				{
					return this._owner;
				}
			}

			// Token: 0x17001DB7 RID: 7607
			// (get) Token: 0x06006BA7 RID: 27559 RVA: 0x0017FCBB File Offset: 0x0017DEBB
			// (set) Token: 0x06006BA8 RID: 27560 RVA: 0x0017FCCD File Offset: 0x0017DECD
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

			// Token: 0x17001DB8 RID: 7608
			// (get) Token: 0x06006BA9 RID: 27561 RVA: 0x0017FCD6 File Offset: 0x0017DED6
			private bool RenderOuterTable
			{
				get
				{
					return this._owner.RenderOuterTable;
				}
			}

			// Token: 0x17001DB9 RID: 7609
			// (get) Token: 0x06006BAA RID: 27562 RVA: 0x0017FCE8 File Offset: 0x0017DEE8
			private bool UsingDefaultTemplate
			{
				get
				{
					return this.BorderTable != null;
				}
			}

			// Token: 0x06006BAB RID: 27563 RVA: 0x00061169 File Offset: 0x0005F369
			public sealed override void Focus()
			{
				throw new NotSupportedException(SR.GetString("NoFocusSupport", new object[]
				{
					base.GetType().Name
				}));
			}

			// Token: 0x06006BAC RID: 27564 RVA: 0x0017FCF4 File Offset: 0x0017DEF4
			private Control FindControl<RequiredType>(string id, bool required, string errorResourceKey)
			{
				Control control = this.FindControl(id);
				if (control is RequiredType)
				{
					return control;
				}
				if (required && !this.Owner.DesignMode)
				{
					throw new HttpException(SR.GetString(errorResourceKey, new object[]
					{
						this.Owner.ID,
						id
					}));
				}
				return null;
			}

			// Token: 0x06006BAD RID: 27565 RVA: 0x0017FD52 File Offset: 0x0017DF52
			protected Control FindOptionalControl<RequiredType>(string id)
			{
				return this.FindControl<RequiredType>(id, false, null);
			}

			// Token: 0x06006BAE RID: 27566 RVA: 0x0017FD5D File Offset: 0x0017DF5D
			protected Control FindRequiredControl<RequiredType>(string id, string errorResourceKey)
			{
				return this.FindControl<RequiredType>(id, true, errorResourceKey);
			}

			// Token: 0x06006BAF RID: 27567 RVA: 0x0017FD68 File Offset: 0x0017DF68
			protected internal virtual string ModifiedOuterTableStylePropertyName()
			{
				if (this.BorderPadding != 1)
				{
					return "BorderPadding";
				}
				return LoginUtil.ModifiedOuterTableBasicStylePropertyName(this.Owner);
			}

			// Token: 0x06006BB0 RID: 27568 RVA: 0x0017FD8C File Offset: 0x0017DF8C
			protected internal sealed override void Render(HtmlTextWriter writer)
			{
				if (!this.RenderOuterTable)
				{
					string text = this.ModifiedOuterTableStylePropertyName();
					if (!string.IsNullOrEmpty(text))
					{
						throw new InvalidOperationException(SR.GetString("IRenderOuterTableControl_CannotSetStyleWhenDisableRenderOuterTable", new object[]
						{
							text,
							this._owner.GetType().Name,
							this._owner.ID
						}));
					}
				}
				if (!this.UsingDefaultTemplate)
				{
					this.RenderContentsInUnitTable(writer);
					return;
				}
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
				if (this.RenderOuterTable)
				{
					this.RenderContents(writer);
					return;
				}
				ControlCollection controls = this.BorderTable.Rows[0].Cells[0].Controls;
				LoginUtil.GenericContainer<ControlType>.RenderControls(writer, controls);
			}

			// Token: 0x06006BB1 RID: 27569 RVA: 0x0017FEA4 File Offset: 0x0017E0A4
			private void RenderContentsInUnitTable(HtmlTextWriter writer)
			{
				if (!this.RenderOuterTable && !this.RenderDesignerRegion)
				{
					LoginUtil.GenericContainer<ControlType>.RenderControls(writer, this.Controls);
					return;
				}
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
				if (this.RenderOuterTable)
				{
					string id = this.Parent.ID;
					if (id != null && id.Length != 0)
					{
						layoutTable.ID = this.Parent.ClientID;
					}
					layoutTable.CopyBaseAttributes(this);
					layoutTable.ApplyStyle(base.ControlStyle);
					layoutTable.CellPadding = 0;
					layoutTable.CellSpacing = 0;
				}
				layoutTable.RenderControl(writer);
			}

			// Token: 0x06006BB2 RID: 27570 RVA: 0x0017FFB8 File Offset: 0x0017E1B8
			private static void RenderControls(HtmlTextWriter writer, ControlCollection controls)
			{
				foreach (object obj in controls)
				{
					Control control = (Control)obj;
					control.RenderControl(writer);
				}
			}

			// Token: 0x06006BB3 RID: 27571 RVA: 0x0018000C File Offset: 0x0017E20C
			protected void VerifyControlNotPresent<RequiredType>(string id, string errorResourceKey)
			{
				Control control = this.FindOptionalControl<RequiredType>(id);
				if (control != null && !this.Owner.DesignMode)
				{
					throw new HttpException(SR.GetString(errorResourceKey, new object[]
					{
						this.Owner.ID,
						id
					}));
				}
			}

			// Token: 0x0400394E RID: 14670
			private bool _renderDesignerRegion;

			// Token: 0x0400394F RID: 14671
			private ControlType _owner;

			// Token: 0x04003950 RID: 14672
			private Table _layoutTable;

			// Token: 0x04003951 RID: 14673
			private Table _borderTable;
		}
	}
}
