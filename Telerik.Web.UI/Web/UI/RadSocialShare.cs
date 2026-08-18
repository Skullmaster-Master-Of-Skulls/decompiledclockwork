using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.SocialShare;

namespace Telerik.Web.UI
{
	// Token: 0x02000F01 RID: 3841
	[EmbeddedSkin("SocialShare")]
	[EmbeddedSkin("SocialShare", "Default")]
	[ToolboxBitmap(typeof(RadSocialShare), "Telerik.Web.UI.SocialShare.png")]
	[RequiredScript(typeof(PopupBehavior))]
	[ClientScriptResource("Telerik.Web.UI.RadSocialShare", "Telerik.Web.UI.SocialShare.RadSocialShare.js")]
	[Description("Telerik SocialShare component")]
	[ToolboxData("<{0}:RadSocialShare runat=\"server\"></{0}:RadSocialShare>")]
	[TelerikToolboxCategory("Miscellaneous")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[Designer("Telerik.Web.Design.RadSocialShareDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ParseChildren(ChildrenAsProperties = true)]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadSocialShare))]
	[LightweightRendering]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Classic, typeof(RadSocialShare))]
	public class RadSocialShare : RadWebControl, ICallbackEventHandler
	{
		// Token: 0x060091DA RID: 37338 RVA: 0x0020D03C File Offset: 0x0020B23C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.EnsureChildControls();
			if (this._emailPopup != null)
			{
				RadCaptcha radCaptcha = (RadCaptcha)this._emailPopup.ContentContainer.FindControl(this.ClientID + "_captcha");
				radCaptcha.CaptchaValidate += this.emailCaptcha_CaptchaValidate;
			}
		}

		// Token: 0x060091DB RID: 37339 RVA: 0x0020D098 File Offset: 0x0020B298
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (base.IsSkinSet || this.ViewState["EnableEmbeddedSkins"] != null || this.ViewState["EnableEmbeddedBaseStylesheet"] != null)
			{
				this.ApplySkin(this, base.RuntimeSkin);
			}
			this.SetChildControlsSkinRelatedSizes();
		}

		// Token: 0x060091DC RID: 37340 RVA: 0x0020D0EB File Offset: 0x0020B2EB
		protected void emailCaptcha_CaptchaValidate(object sender, CaptchaValidateEventArgs e)
		{
			e.CancelDefaultValidation = true;
			e.IsValid = true;
		}

		// Token: 0x060091DD RID: 37341 RVA: 0x0020D0FC File Offset: 0x0020B2FC
		private void ApplySkin(Control target, string skin)
		{
			if (!target.Visible)
			{
				return;
			}
			foreach (object obj in target.Controls)
			{
				Control control = (Control)obj;
				ISkinnableControl skinnableControl = control as ISkinnableControl;
				if (skinnableControl != null)
				{
					skinnableControl.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
					skinnableControl.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
					skinnableControl.EnableAjaxSkinRendering = this.EnableAjaxSkinRendering;
					skinnableControl.Skin = skin;
				}
				this.ApplySkin(control, skin);
			}
		}

		// Token: 0x060091DE RID: 37342 RVA: 0x0020D194 File Offset: 0x0020B394
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			if (this._emailPopup != null)
			{
				this.Page.ClientScript.GetCallbackEventReference(this, null, null, null);
			}
			if (this.MainButtons.Count > 0)
			{
				descriptor.AddScriptProperty("mainButtons", "\"[" + this.GetSerializedCollection(true) + "]\"");
			}
			if (this.CompactButtons.Count > 0)
			{
				descriptor.AddScriptProperty("compactButtons", "\"[" + this.GetSerializedCollection(false) + "]\"");
			}
			descriptor.AddScriptProperty("_addFbScript", this._addFbScript.ToString().ToLower());
			descriptor.AddScriptProperty("_addGoogleScript", this._addGoogleScript.ToString().ToLower());
			descriptor.AddScriptProperty("_addTwitterScript", this._addTwitterScript.ToString().ToLower());
			descriptor.AddScriptProperty("_addLinkedInScript", this._addLinkedInScript.ToString().ToLower());
			descriptor.AddScriptProperty("_addYammerScript", this._addYammerScript.ToString().ToLower());
			descriptor.AddScriptProperty("_addPinterestScript", this._addPinterestScript.ToString().ToLower());
			descriptor.AddProperty("_uniqueId", this.UniqueID);
			descriptor.AddProperty("_locale", this.GetFacebookSupportedLocale(CultureInfo.CreateSpecificCulture(CultureInfo.CurrentUICulture.Name).Name.Replace("-", "_")));
		}

		// Token: 0x060091DF RID: 37343 RVA: 0x0020D30C File Offset: 0x0020B50C
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			base.CreateChildControls();
			RadSocialButton compactButton = this.MainButtons._compactButton;
			if (compactButton != null)
			{
				if (this.CompactButtons.Count == 0)
				{
					this.AddDefaultCompactButtons();
				}
				if (this._compactPopup == null && this.CompactButtons.Count > 0)
				{
					this.ConfigureAddPopup("_compactPopup", compactButton.DialogTop, compactButton.DialogLeft, (compactButton is RadCompactButton) ? ((RadCompactButton)compactButton).DialogTitle : "Share on");
				}
				if (this._comboBox == null && this._compactPopup != null)
				{
					this._comboBox = new RadComboBox();
					this.SetRenderModeToChildControl(this._comboBox);
					this._comboBox.ID = "comboBox";
					this._comboBox.Filter = RadComboBoxFilter.StartsWith;
					this._comboBox.CloseDropDownOnBlur = false;
					this._comboBox.ExpandAnimation.Duration = 0;
					this._comboBox.EnableViewState = false;
					this._comboBox.EnableScreenBoundaryDetection = false;
					this._comboBox.ShowToggleImage = false;
					this._comboBox.ToolTip = "Type to filter";
					this.AddButtonItems();
					this._comboBox.EmptyMessage = " ";
					this._compactPopup.ContentContainer.Controls.Add(this._comboBox);
				}
			}
			if ((this.MainButtons._emailButton != null || this.CompactButtons._emailButton != null) && this._emailPopup == null)
			{
				RadSocialButton radSocialButton = (this.MainButtons._emailButton != null) ? this.MainButtons._emailButton : this.CompactButtons._emailButton;
				this.ConfigureAddPopup("_emailPopup", radSocialButton.DialogTop, radSocialButton.DialogLeft, "Share Link");
				Control child = this.LoadUserControl("SendEmail");
				this._emailPopup.ContentContainer.Controls.Add(child);
			}
		}

		// Token: 0x060091E0 RID: 37344 RVA: 0x0020D4E8 File Offset: 0x0020B6E8
		private void ConfigureAddPopup(string ID, Unit top, Unit left, string title)
		{
			bool flag = ID == "_compactPopup";
			RadWindow radWindow = new RadWindow();
			this.SetRenderModeToChildControl(radWindow);
			if (flag)
			{
				this._compactPopup = radWindow;
			}
			else
			{
				this._emailPopup = radWindow;
			}
			radWindow.ID = this.ID + ID;
			radWindow.Behaviors = (WindowBehaviors.Close | WindowBehaviors.Pin | WindowBehaviors.Move);
			radWindow.Top = ((top != Unit.Empty) ? top : this.DialogTop);
			radWindow.Left = ((left != Unit.Empty) ? left : this.DialogLeft);
			radWindow.Title = title;
			radWindow.VisibleStatusbar = false;
			radWindow.EnableShadow = true;
			radWindow.Shortcuts.Add(new WindowShortcut("close", "Esc"));
			this.Controls.Add(radWindow);
		}

		// Token: 0x060091E1 RID: 37345 RVA: 0x0020D5B0 File Offset: 0x0020B7B0
		private void SetChildControlsSkinRelatedSizes()
		{
			RadSocialButton compactButton = this.MainButtons._compactButton;
			if (compactButton != null)
			{
				Unit dropDownWidth = Unit.Empty;
				if (this.CompactButtons.Count > 0)
				{
					Unit unit = compactButton.DialogWidth;
					if (this.minimumPopupWidthPerSkin.ContainsKey(base.RuntimeSkin) && unit.Value < (double)this.minimumPopupWidthPerSkin[base.RuntimeSkin])
					{
						unit = Unit.Pixel(this.minimumPopupWidthPerSkin[base.RuntimeSkin]);
					}
					if (unit.Value > 0.0)
					{
						dropDownWidth = unit;
						unit = Unit.Pixel((int)unit.Value + 70);
					}
					this.SetPopupSize("_compactPopup", unit, compactButton.DialogHeight);
				}
				if (this._comboBox != null && this._compactPopup != null)
				{
					if (dropDownWidth.Value > 0.0)
					{
						this._comboBox.Width = (this._comboBox.DropDownWidth = dropDownWidth);
					}
					int num = int.Parse(this._compactPopup.Height.Value.ToString());
					this._comboBox.Height = ((num > 60) ? (num - 60) : num);
				}
			}
			if ((this.MainButtons._emailButton != null || this.CompactButtons._emailButton != null) && this._emailPopup == null)
			{
				RadSocialButton radSocialButton = (this.MainButtons._emailButton != null) ? this.MainButtons._emailButton : this.CompactButtons._emailButton;
				this.SetPopupSize("_emailPopup", radSocialButton.DialogWidth, radSocialButton.DialogHeight);
			}
		}

		// Token: 0x060091E2 RID: 37346 RVA: 0x0020D748 File Offset: 0x0020B948
		private void SetPopupSize(string ID, Unit width, Unit height)
		{
			RadWindow radWindow = (ID == "_compactPopup") ? this._compactPopup : this._emailPopup;
			if (radWindow != null)
			{
				radWindow.Width = width;
				radWindow.Height = height;
			}
		}

		// Token: 0x060091E3 RID: 37347 RVA: 0x0020D782 File Offset: 0x0020B982
		private void SetRenderModeToChildControl(ISkinnableControl control)
		{
			if (control != null)
			{
				control.RenderMode = this.RenderMode;
			}
		}

		// Token: 0x060091E4 RID: 37348 RVA: 0x0020D794 File Offset: 0x0020B994
		private Control LoadUserControl(string controlName)
		{
			Control result = null;
			string name = string.Format("Telerik.Web.UI.SocialShare.UserControls.{0}.ascx", controlName);
			Encoding utf = Encoding.UTF8;
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = string.Empty;
			using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(name))
			{
				byte[] array = new byte[manifestResourceStream.Length];
				manifestResourceStream.Read(array, 0, (int)manifestResourceStream.Length);
				text = utf.GetString(array);
			}
			if (text != string.Empty)
			{
				string content = this.FormUniqueAttributesRegex(text, "id");
				string content2 = this.FormUniqueAttributesRegex(content, "name");
				text = this.FormUniqueAttributesRegex(content2, "for");
				result = this.Page.ParseControl(text, true);
			}
			return result;
		}

		// Token: 0x060091E5 RID: 37349 RVA: 0x0020D8AC File Offset: 0x0020BAAC
		private string FormUniqueAttributesRegex(string content, string attribute)
		{
			string pattern = string.Format("[\\s]{0}\\=(?<br>[\\\"\\'])(?<{1}>[^\\\"\\']+)\\k<br>", attribute, attribute);
			string str = Regex.Replace(Regex.Escape(content), pattern, delegate(Match match)
			{
				string value = match.Groups[attribute].Value;
				return string.Format(" {0}=\"{1}_{2}\"", attribute, this.ClientID, value);
			}, RegexOptions.IgnoreCase);
			return Regex.Unescape(str);
		}

		// Token: 0x060091E6 RID: 37350 RVA: 0x0020D904 File Offset: 0x0020BB04
		protected void AddButtonItems()
		{
			this._comboBox.Items.Clear();
			StringWriter stringWriter = new StringWriter();
			using (HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter))
			{
				foreach (object obj in this.CompactButtons)
				{
					RadSocialButtonBase btn = (RadSocialButtonBase)obj;
					this.RenderSocialButton(false, htmlTextWriter, btn, true);
					RadComboBoxItem radComboBoxItem = new RadComboBoxItem(stringWriter.ToString());
					stringWriter.GetStringBuilder().Remove(0, stringWriter.GetStringBuilder().Length);
					this._comboBox.Items.Add(radComboBoxItem);
					radComboBoxItem.SetRenderMethodDelegate(new RenderMethod(this.DecodeItemHTML));
				}
			}
			stringWriter.Dispose();
		}

		// Token: 0x060091E7 RID: 37351 RVA: 0x0020D9EC File Offset: 0x0020BBEC
		protected override Style CreateControlStyle()
		{
			Style style = base.CreateControlStyle();
			style.Width = this.Width;
			style.Height = this.Height;
			return style;
		}

		// Token: 0x060091E8 RID: 37352 RVA: 0x0020DA1C File Offset: 0x0020BC1C
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.EnsureChildControls();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "sshContent");
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			foreach (object obj in this.MainButtons)
			{
				RadSocialButtonBase btn = (RadSocialButtonBase)obj;
				this.RenderSocialButton(true, writer, btn, false);
			}
			writer.RenderEndTag();
			base.RenderContents(writer);
		}

		// Token: 0x060091E9 RID: 37353 RVA: 0x0020DAA0 File Offset: 0x0020BCA0
		protected void RenderSocialButton(bool addLi, HtmlTextWriter writer, RadSocialButtonBase btn, bool isCompactButton)
		{
			if (addLi)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "sshListItem");
				writer.RenderBeginTag(HtmlTextWriterTag.Li);
			}
			string str = btn.SocialNetType.ToString();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "sshLinkItem");
			writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:void(0)");
			RadSocialButton radSocialButton = btn as RadSocialButton;
			if (radSocialButton != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, radSocialButton.ToolTip);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			if (radSocialButton != null)
			{
				bool flag = radSocialButton.CustomIconUrl != string.Empty && radSocialButton.CustomIconUrl.ToLower() != "none";
				string value = flag ? ("sshCustomIcon " + radSocialButton.CssClass) : ("sshIcon ssh" + str + " " + radSocialButton.CssClass);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
				if (flag)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Width, radSocialButton.CustomIconWidth.Value.ToString() + "px");
					writer.AddStyleAttribute(HtmlTextWriterStyle.Height, radSocialButton.CustomIconHeight.Value.ToString() + "px");
					writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundImage, base.ResolveClientUrl(radSocialButton.CustomIconUrl));
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.RenderEndTag();
				string a = radSocialButton.LabelText.ToLower();
				bool flag2 = a != string.Empty && a != "none";
				if (isCompactButton && !flag2)
				{
					radSocialButton.LabelText = radSocialButton.ToolTip.Replace("Share on ", "");
					flag2 = true;
				}
				if (flag2)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "sshText");
					writer.AddAttribute(HtmlTextWriterAttribute.Title, radSocialButton.ToolTip);
					writer.RenderBeginTag(HtmlTextWriterTag.Span);
					writer.Write(radSocialButton.LabelText);
					writer.RenderEndTag();
				}
			}
			if (btn.SocialNetType.ToString().Contains("Facebook") && btn.SocialNetType != SocialNetType.ShareOnFacebook)
			{
				this._addFbScript = true;
			}
			if (btn.SocialNetType.ToString().Contains("Twitter") && btn.SocialNetType != SocialNetType.ShareOnTwitter)
			{
				this._addTwitterScript = true;
			}
			if (btn.SocialNetType.ToString().Contains("Google") && btn.SocialNetType != SocialNetType.GoogleBookmarks && btn.SocialNetType != SocialNetType.ShareOnGooglePlus)
			{
				this._addGoogleScript = true;
			}
			if (btn.SocialNetType.ToString().Contains("LinkedIn") && btn.SocialNetType != SocialNetType.LinkedIn)
			{
				this._addLinkedInScript = true;
			}
			this._addYammerScript = (this._addYammerScript || btn.SocialNetType.ToString().Equals("Yammer"));
			if (btn.SocialNetType.ToString() == SocialNetType.Pinterest.ToString())
			{
				this._addPinterestScript = true;
			}
			writer.RenderEndTag();
			if (addLi)
			{
				writer.RenderEndTag();
			}
		}

		// Token: 0x060091EA RID: 37354 RVA: 0x0020DD97 File Offset: 0x0020BF97
		protected void DecodeItemHTML(HtmlTextWriter writer, Control container)
		{
			writer.Write(HttpUtility.HtmlDecode((container as RadComboBoxItem).Text));
		}

		// Token: 0x060091EB RID: 37355 RVA: 0x0020DDB0 File Offset: 0x0020BFB0
		protected void AddButtonSerialization(StringBuilder str, RadSocialButtonBase btn)
		{
			str.AppendFormat("['{0}','{1}','{2}'", btn.SocialNetType, this.GetResolvedUrl(btn.UrlToShare), this.EscapeString(btn.TitleToShare));
			RadSocialButton radSocialButton = btn as RadSocialButton;
			if (radSocialButton != null)
			{
				str.AppendFormat(", '{0}', '{1}', '{2}', '{3}'", new object[]
				{
					(radSocialButton.DialogWidth != Unit.Empty) ? radSocialButton.DialogWidth.Value : this.DialogWidth.Value,
					(radSocialButton.DialogHeight != Unit.Empty) ? radSocialButton.DialogHeight.Value : this.DialogHeight.Value,
					(radSocialButton.DialogTop != Unit.Empty) ? radSocialButton.DialogTop : this.DialogTop,
					(radSocialButton.DialogLeft != Unit.Empty) ? radSocialButton.DialogLeft : this.DialogLeft
				});
			}
			RadFacebookButton radFacebookButton = btn as RadFacebookButton;
			if (radFacebookButton != null)
			{
				str.AppendFormat(", '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'", new object[]
				{
					radFacebookButton.ShowFaces,
					radFacebookButton.ButtonLayout,
					radFacebookButton.ColorScheme,
					radFacebookButton.Width,
					radFacebookButton.Font,
					radFacebookButton.ReferralsLabel
				});
				this._addFbScript = true;
			}
			RadTwitterButton radTwitterButton = btn as RadTwitterButton;
			if (radTwitterButton != null)
			{
				this._addTwitterScript = true;
				str.AppendFormat(", '{0}'", radTwitterButton.CounterMode);
			}
			RadGoogleButton radGoogleButton = btn as RadGoogleButton;
			if (radGoogleButton != null)
			{
				this._addGoogleScript = true;
				str.AppendFormat(", '{0}', '{1}', '{2}'", radGoogleButton.ButtonSize, radGoogleButton.AnnotationType, radGoogleButton.Width);
			}
			RadLinkedInButton radLinkedInButton = btn as RadLinkedInButton;
			if (radLinkedInButton != null)
			{
				this._addLinkedInScript = true;
				str.AppendFormat(", '{0}', '{1}'", radLinkedInButton.CounterMode, radLinkedInButton.ShowZeroCount.ToString().ToLower());
			}
			RadYammerButton radYammerButton = btn as RadYammerButton;
			if (radYammerButton != null)
			{
				this._addYammerScript = true;
				str.AppendFormat(", '{0}', '{1}'", radYammerButton.ButtonType.ToString().ToLower(), radYammerButton.YammerNetwork);
			}
			RadPinterestButton radPinterestButton = btn as RadPinterestButton;
			if (radPinterestButton != null)
			{
				this._addPinterestScript = true;
				str.AppendFormat(", '{0}', '{1}', '{2}'", radPinterestButton.FromUrl, radPinterestButton.CounterMode.ToString(), radPinterestButton.ButtonType.ToString());
			}
			str.Append("],");
		}

		// Token: 0x060091EC RID: 37356 RVA: 0x0020E08C File Offset: 0x0020C28C
		protected string GetSerializedCollection(bool mainButtons)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (mainButtons)
			{
				using (IEnumerator enumerator = this.MainButtons.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						RadSocialButtonBase btn = (RadSocialButtonBase)obj;
						this.AddButtonSerialization(stringBuilder, btn);
					}
					goto IL_8D;
				}
			}
			foreach (object obj2 in this.CompactButtons)
			{
				RadSocialButtonBase btn2 = (RadSocialButtonBase)obj2;
				this.AddButtonSerialization(stringBuilder, btn2);
			}
			IL_8D:
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060091ED RID: 37357 RVA: 0x0020E164 File Offset: 0x0020C364
		private void AddDefaultCompactButtons()
		{
			foreach (object obj in Enum.GetValues(typeof(SocialNetType)))
			{
				int num = (int)obj;
				string name = Enum.GetName(typeof(SocialNetType), num);
				if ((!name.Contains("Facebook") || !(name != "ShareOnFacebook")) && (!name.Contains("Twitter") || !(name != "ShareOnTwitter")) && (!name.Contains("Google") || !(name != "GoogleBookmarks") || !(name != "ShareOnGooglePlus")) && !(name == "CompactButton") && (!name.Contains("LinkedIn") || !(name != "LinkedIn")) && !(name == "Yammer") && !(name == "Pinterest"))
				{
					RadSocialButton radSocialButton = new RadSocialButton();
					radSocialButton.SocialNetType = (SocialNetType)num;
					if (!this.MainButtons.Contains(radSocialButton))
					{
						this.CompactButtons.Add(radSocialButton);
					}
				}
			}
		}

		// Token: 0x060091EE RID: 37358 RVA: 0x0020E2A8 File Offset: 0x0020C4A8
		private void SendMail(string from, string to, string subject, string body)
		{
			MailMessage mailMessage = new MailMessage();
			mailMessage.IsBodyHtml = true;
			mailMessage.From = new MailAddress(from);
			mailMessage.Sender = new MailAddress(this._emailSettings.FromEmail);
			string[] array = to.Split(new char[]
			{
				',',
				';'
			});
			foreach (string text in array)
			{
				if (text != string.Empty)
				{
					mailMessage.To.Add(new MailAddress(text));
				}
			}
			mailMessage.Subject = subject;
			mailMessage.Body = body;
			string smtpserver = this.EmailSettings.SMTPServer;
			SmtpClient smtpClient = (smtpserver != string.Empty) ? new SmtpClient(smtpserver) : new SmtpClient();
			if (this.EmailSettings.UserName != string.Empty && this.EmailSettings.Password != string.Empty)
			{
				smtpClient.Credentials = new NetworkCredential(this.EmailSettings.UserName, this.EmailSettings.Password);
			}
			else
			{
				smtpClient.UseDefaultCredentials = true;
			}
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtpClient.Send(mailMessage);
		}

		// Token: 0x060091EF RID: 37359 RVA: 0x0020E3DC File Offset: 0x0020C5DC
		string ICallbackEventHandler.GetCallbackResult()
		{
			return "";
		}

		// Token: 0x060091F0 RID: 37360 RVA: 0x0020E3E4 File Offset: 0x0020C5E4
		void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
		{
			string text = HttpContext.Current.Request["__CALLBACKPARAM"];
			if (!text.Contains("sendMail"))
			{
				return;
			}
			string[] array = text.Split(new char[]
			{
				'&'
			});
			RadCaptcha radCaptcha = this._emailPopup.ContentContainer.FindControl(this.ClientID + "_captcha") as RadCaptcha;
			if (radCaptcha.CaptchaImage.PreviousText == array[5])
			{
				this.SendMail(array[1], array[2], HttpUtility.UrlDecode(array[3]), HttpUtility.UrlDecode(array[4]));
				radCaptcha.CaptchaImage.RenderImage();
				return;
			}
			throw new Exception("email captha failed!");
		}

		// Token: 0x060091F1 RID: 37361 RVA: 0x0020E49C File Offset: 0x0020C69C
		private string GetResolvedUrl(string url)
		{
			if (url.Contains("~"))
			{
				url = base.ResolveUrl(url);
			}
			if (!string.IsNullOrEmpty(url) && !url.Contains("http://") && !url.Contains("https://") && !url.Contains("www.") && HttpContext.Current != null)
			{
				Uri url2 = HttpContext.Current.Request.Url;
				url = url2.AbsoluteUri.Replace(url2.PathAndQuery, url);
			}
			return url;
		}

		// Token: 0x060091F2 RID: 37362 RVA: 0x0020E51A File Offset: 0x0020C71A
		private string EscapeString(string str)
		{
			return str.Replace("'", "\\\\'").Replace("\"", "\\\"");
		}

		// Token: 0x060091F3 RID: 37363 RVA: 0x0020E53C File Offset: 0x0020C73C
		private string GetFacebookSupportedLocale(string locale)
		{
			string[] array = new string[]
			{
				"sq_AL",
				"ar_AR",
				"hy_AM",
				"ay_BO",
				"az_AZ",
				"eu_ES",
				"be_BY",
				"bn_IN",
				"bs_BA",
				"bg_BG",
				"ca_ES",
				"ck_US",
				"hr_HR",
				"cs_CZ",
				"da_DK",
				"nl_NL",
				"nl_BE",
				"en_PI",
				"en_GB",
				"en_UD",
				"en_US",
				"eo_EO",
				"et_EE",
				"fo_FO",
				"tl_PH",
				"fi_FI",
				"fb_FI",
				"fr_CA",
				"fr_FR",
				"gl_ES",
				"ka_GE",
				"de_DE",
				"el_GR",
				"gn_PY",
				"gu_IN",
				"he_IL",
				"hi_IN",
				"hu_HU",
				"is_IS",
				"id_ID",
				"ga_IE",
				"it_IT",
				"ja_JP",
				"jv_ID",
				"kn_IN",
				"kk_KZ",
				"km_KH",
				"tl_ST",
				"ko_KR",
				"ku_TR",
				"la_VA",
				"lv_LV",
				"fb_LT",
				"li_NL",
				"lt_LT",
				"mk_MK",
				"mg_MG",
				"ms_MY",
				"ml_IN",
				"mt_MT",
				"mr_IN",
				"mn_MN",
				"ne_NP",
				"se_NO",
				"nb_NO",
				"nn_NO",
				"ps_AF",
				"fa_IR",
				"pl_PL",
				"pt_BR",
				"pt_PT",
				"pa_IN",
				"qu_PE",
				"ro_RO",
				"rm_CH",
				"ru_RU",
				"sa_IN",
				"sr_RS",
				"zh_CN",
				"sk_SK",
				"sl_SI",
				"so_SO",
				"es_LA",
				"es_CL",
				"es_CO",
				"es_MX",
				"es_ES",
				"es_VE",
				"sw_KE",
				"sv_SE",
				"sy_SY",
				"tg_TJ",
				"ta_IN",
				"tt_RU",
				"te_IN",
				"th_TH",
				"zh_HK",
				"zh_TW",
				"tr_TR",
				"uk_UA",
				"ur_PK",
				"uz_UZ",
				"vi_VN",
				"cy_GB",
				"xh_ZA",
				"yi_DE",
				"zu_ZA"
			};
			if (Array.IndexOf<string>(array, locale) <= -1)
			{
				return "en_US";
			}
			return locale;
		}

		// Token: 0x17002E10 RID: 11792
		// (get) Token: 0x060091F4 RID: 37364 RVA: 0x0020E91E File Offset: 0x0020CB1E
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17002E11 RID: 11793
		// (get) Token: 0x060091F5 RID: 37365 RVA: 0x0020E924 File Offset: 0x0020CB24
		protected override string CssClassFormatString
		{
			get
			{
				string text = "RadSocialShare";
				if (!string.IsNullOrEmpty(base.RuntimeSkin))
				{
					text += " RadSocialShare_{0}";
				}
				if (this.Orientation == Orientation.Vertical)
				{
					text += " sshVerticalMode";
				}
				return text;
			}
		}

		// Token: 0x17002E12 RID: 11794
		// (get) Token: 0x060091F6 RID: 37366 RVA: 0x0020E966 File Offset: 0x0020CB66
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002E13 RID: 11795
		// (get) Token: 0x060091F7 RID: 37367 RVA: 0x0020E969 File Offset: 0x0020CB69
		// (set) Token: 0x060091F8 RID: 37368 RVA: 0x0020E98A File Offset: 0x0020CB8A
		[Category("Layout")]
		[Description("Specifies the orientation of the buttons. Horizontal by default.")]
		[DefaultValue(Orientation.Horizontal)]
		public Orientation Orientation
		{
			get
			{
				return (Orientation)(this.ViewState["Orientation"] ?? Orientation.Horizontal);
			}
			set
			{
				this.ViewState["Orientation"] = value;
			}
		}

		// Token: 0x17002E14 RID: 11796
		// (get) Token: 0x060091F9 RID: 37369 RVA: 0x0020E9A2 File Offset: 0x0020CBA2
		// (set) Token: 0x060091FA RID: 37370 RVA: 0x0020E9CC File Offset: 0x0020CBCC
		[DefaultValue(typeof(Unit), "470")]
		[TypeConverter(typeof(UnitConverter))]
		[Description("Specifies the width of the social dialog popup.")]
		[Category("Layout")]
		public Unit DialogWidth
		{
			get
			{
				return (Unit)(this.ViewState["DialogWidth"] ?? new Unit(470));
			}
			set
			{
				this.ViewState["DialogWidth"] = value;
			}
		}

		// Token: 0x17002E15 RID: 11797
		// (get) Token: 0x060091FB RID: 37371 RVA: 0x0020E9E4 File Offset: 0x0020CBE4
		// (set) Token: 0x060091FC RID: 37372 RVA: 0x0020EA0E File Offset: 0x0020CC0E
		[TypeConverter(typeof(UnitConverter))]
		[Category("Layout")]
		[Description("Specifies the height of the social dialog popup.")]
		[DefaultValue(typeof(Unit), "470")]
		public Unit DialogHeight
		{
			get
			{
				return (Unit)(this.ViewState["DialogHeight"] ?? new Unit(470));
			}
			set
			{
				this.ViewState["DialogHeight"] = value;
			}
		}

		// Token: 0x17002E16 RID: 11798
		// (get) Token: 0x060091FD RID: 37373 RVA: 0x0020EA26 File Offset: 0x0020CC26
		// (set) Token: 0x060091FE RID: 37374 RVA: 0x0020EA4B File Offset: 0x0020CC4B
		[Description("Specifies the top of the social dialog. It is centered by default.")]
		[Category("Layout")]
		[DefaultValue(typeof(Unit), "")]
		[TypeConverter(typeof(UnitConverter))]
		public Unit DialogTop
		{
			get
			{
				return (Unit)(this.ViewState["DialogTop"] ?? Unit.Empty);
			}
			set
			{
				this.ViewState["DialogTop"] = value;
			}
		}

		// Token: 0x17002E17 RID: 11799
		// (get) Token: 0x060091FF RID: 37375 RVA: 0x0020EA63 File Offset: 0x0020CC63
		// (set) Token: 0x06009200 RID: 37376 RVA: 0x0020EA88 File Offset: 0x0020CC88
		[TypeConverter(typeof(UnitConverter))]
		[Category("Layout")]
		[Description("Specifies the left of the social dialog popup. It is centered by default.")]
		[DefaultValue(typeof(Unit), "")]
		public Unit DialogLeft
		{
			get
			{
				return (Unit)(this.ViewState["DialogLeft"] ?? Unit.Empty);
			}
			set
			{
				this.ViewState["DialogLeft"] = value;
			}
		}

		// Token: 0x17002E18 RID: 11800
		// (get) Token: 0x06009201 RID: 37377 RVA: 0x0020EAA0 File Offset: 0x0020CCA0
		// (set) Token: 0x06009202 RID: 37378 RVA: 0x0020EAC5 File Offset: 0x0020CCC5
		[TypeConverter(typeof(UnitConverter))]
		[Category("Layout")]
		[Description("Specifies the width of the social share control.")]
		[DefaultValue(typeof(Unit), "")]
		public override Unit Width
		{
			get
			{
				return (Unit)(this.ViewState["Width"] ?? Unit.Empty);
			}
			set
			{
				this.ViewState["Width"] = value;
			}
		}

		// Token: 0x17002E19 RID: 11801
		// (get) Token: 0x06009203 RID: 37379 RVA: 0x0020EADD File Offset: 0x0020CCDD
		// (set) Token: 0x06009204 RID: 37380 RVA: 0x0020EB02 File Offset: 0x0020CD02
		[Category("Layout")]
		[Description("Specifies the height of the social share control.")]
		[TypeConverter(typeof(UnitConverter))]
		[DefaultValue(typeof(Unit), "")]
		public override Unit Height
		{
			get
			{
				return (Unit)(this.ViewState["Height"] ?? Unit.Empty);
			}
			set
			{
				this.ViewState["Height"] = value;
			}
		}

		// Token: 0x17002E1A RID: 11802
		// (get) Token: 0x06009205 RID: 37381 RVA: 0x0020EB1A File Offset: 0x0020CD1A
		// (set) Token: 0x06009206 RID: 37382 RVA: 0x0020EB40 File Offset: 0x0020CD40
		[Category("Behavior")]
		[ClientControlProperty]
		[Description("Specifies the URL to share. The page's URL by default.")]
		[DefaultValue("")]
		public string UrlToShare
		{
			get
			{
				return this.GetResolvedUrl((string)(this.ViewState["UrlToShare"] ?? string.Empty));
			}
			set
			{
				this.ViewState["UrlToShare"] = value;
			}
		}

		// Token: 0x17002E1B RID: 11803
		// (get) Token: 0x06009207 RID: 37383 RVA: 0x0020EB53 File Offset: 0x0020CD53
		// (set) Token: 0x06009208 RID: 37384 RVA: 0x0020EB73 File Offset: 0x0020CD73
		[Description("Specifies the title of the shared message.")]
		[DefaultValue("")]
		[Category("Behavior")]
		[ClientControlProperty]
		public virtual string TitleToShare
		{
			get
			{
				return (string)(this.ViewState["TitleToShare"] ?? string.Empty);
			}
			set
			{
				this.ViewState["TitleToShare"] = value;
			}
		}

		// Token: 0x17002E1C RID: 11804
		// (get) Token: 0x06009209 RID: 37385 RVA: 0x0020EB86 File Offset: 0x0020CD86
		// (set) Token: 0x0600920A RID: 37386 RVA: 0x0020EBA7 File Offset: 0x0020CDA7
		[Category("Behavior")]
		[DefaultValue(true)]
		[ClientControlProperty]
		[Description("Specifies whether IFRAMEs should be hidden while the compact ot send email dialog is moved.")]
		public bool HideIframesOnDialogMove
		{
			get
			{
				return (bool)(this.ViewState["HideIframesOnDialogMove"] ?? true);
			}
			set
			{
				this.ViewState["HideIframesOnDialogMove"] = value;
			}
		}

		// Token: 0x17002E1D RID: 11805
		// (get) Token: 0x0600920B RID: 37387 RVA: 0x0020EBBF File Offset: 0x0020CDBF
		[Description("The Compact Buttons collection.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public GenericSocialButtonsCollection<RadSocialButton> CompactButtons
		{
			get
			{
				if (this._compactButtons == null)
				{
					this._compactButtons = new GenericSocialButtonsCollection<RadSocialButton>();
				}
				return this._compactButtons;
			}
		}

		// Token: 0x17002E1E RID: 11806
		// (get) Token: 0x0600920C RID: 37388 RVA: 0x0020EBDA File Offset: 0x0020CDDA
		[Description("The Main Buttons collection.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public GenericSocialButtonsCollection<RadSocialButtonBase> MainButtons
		{
			get
			{
				if (this._mainButtons == null)
				{
					this._mainButtons = new GenericSocialButtonsCollection<RadSocialButtonBase>();
				}
				return this._mainButtons;
			}
		}

		// Token: 0x17002E1F RID: 11807
		// (get) Token: 0x0600920D RID: 37389 RVA: 0x0020EBF5 File Offset: 0x0020CDF5
		// (set) Token: 0x0600920E RID: 37390 RVA: 0x0020EC15 File Offset: 0x0020CE15
		[ClientPropertyName("gaID")]
		[Browsable(true)]
		[DefaultValue("")]
		[Description("Specifies the web property ID for your Analytics account.")]
		[ClientControlProperty]
		[Category("Behavior")]
		[Bindable(true)]
		public string GoogleAnalyticsUA
		{
			get
			{
				return (string)(this.ViewState["GoogleAnalyticsUA"] ?? string.Empty);
			}
			set
			{
				this.ViewState["GoogleAnalyticsUA"] = value;
			}
		}

		// Token: 0x17002E20 RID: 11808
		// (get) Token: 0x0600920F RID: 37391 RVA: 0x0020EC28 File Offset: 0x0020CE28
		// (set) Token: 0x06009210 RID: 37392 RVA: 0x0020EC48 File Offset: 0x0020CE48
		[Description("Specifies the FacebookAppId of your Facebook application.")]
		[ClientPropertyName("fbAppId")]
		[DefaultValue("")]
		[ClientControlProperty]
		public string FacebookAppId
		{
			get
			{
				return ((string)this.ViewState["FacebookAppId"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["FacebookAppId"] = value;
			}
		}

		// Token: 0x17002E21 RID: 11809
		// (get) Token: 0x06009211 RID: 37393 RVA: 0x0020EC5B File Offset: 0x0020CE5B
		// (set) Token: 0x06009212 RID: 37394 RVA: 0x0020EC7B File Offset: 0x0020CE7B
		[DefaultValue("")]
		[ClientControlProperty]
		[ClientPropertyName("yammerAppId")]
		[Description("Specifies the YammerAppId of your Yammer application.")]
		public string YammerAppId
		{
			get
			{
				return ((string)this.ViewState["YammerAppId"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["YammerAppId"] = value;
			}
		}

		// Token: 0x17002E22 RID: 11810
		// (get) Token: 0x06009213 RID: 37395 RVA: 0x0020EC8E File Offset: 0x0020CE8E
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("The E-mail Settings collection. Must be set in the markup or web.config.")]
		public RadSocialShareEmailSettings EmailSettings
		{
			get
			{
				if (this._emailSettings == null)
				{
					this._emailSettings = new RadSocialShareEmailSettings();
				}
				return this._emailSettings;
			}
		}

		// Token: 0x17002E23 RID: 11811
		// (get) Token: 0x06009214 RID: 37396 RVA: 0x0020ECA9 File Offset: 0x0020CEA9
		// (set) Token: 0x06009215 RID: 37397 RVA: 0x0020ECC9 File Offset: 0x0020CEC9
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("clicking")]
		[Category("Client-side events")]
		[Description("Specifies the name of the JavaScript function that is called when a Styled button is clicked. Can be cancelled.")]
		public string OnSocialButtonClicking
		{
			get
			{
				return ((string)this.ViewState["OnSocialButtonClicking"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnSocialButtonClicking"] = value;
			}
		}

		// Token: 0x17002E24 RID: 11812
		// (get) Token: 0x06009216 RID: 37398 RVA: 0x0020ECDC File Offset: 0x0020CEDC
		// (set) Token: 0x06009217 RID: 37399 RVA: 0x0020ECFC File Offset: 0x0020CEFC
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientPropertyName("clicked")]
		[Description("Specifies the name of the JavaScript function that is called after a Styled button is clicked. Cannot be cancelled.")]
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnSocialButtonClicked
		{
			get
			{
				return ((string)this.ViewState["OnSocialButtonClicked"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnSocialButtonClicked"] = value;
			}
		}

		// Token: 0x17002E25 RID: 11813
		// (get) Token: 0x06009218 RID: 37400 RVA: 0x0020ED0F File Offset: 0x0020CF0F
		// (set) Token: 0x06009219 RID: 37401 RVA: 0x0020ED2F File Offset: 0x0020CF2F
		[Description("Specifies the name of the JavaScript function that is called when the Facebook Like standard button is clicked.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("fbLike")]
		[Category("Client-side events")]
		public string OnFacebookLike
		{
			get
			{
				return ((string)this.ViewState["OnFacebookLike"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnFacebookLike"] = value;
			}
		}

		// Token: 0x17002E26 RID: 11814
		// (get) Token: 0x0600921A RID: 37402 RVA: 0x0020ED42 File Offset: 0x0020CF42
		// (set) Token: 0x0600921B RID: 37403 RVA: 0x0020ED62 File Offset: 0x0020CF62
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Description("Specifies the name of the JavaScript function that is called when the Facebook UnLike standard button is clicked.")]
		[ClientControlEvent]
		[ClientPropertyName("fbUnLike")]
		public string OnFacebookUnLike
		{
			get
			{
				return ((string)this.ViewState["OnFacebookUnLike"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnFacebookUnLike"] = value;
			}
		}

		// Token: 0x17002E27 RID: 11815
		// (get) Token: 0x0600921C RID: 37404 RVA: 0x0020ED75 File Offset: 0x0020CF75
		// (set) Token: 0x0600921D RID: 37405 RVA: 0x0020ED95 File Offset: 0x0020CF95
		[Description("Specifies the name of the JavaScript function that is called when the Facebook Send standard button is clicked.")]
		[ClientPropertyName("fbSend")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[ClientControlEvent]
		[DefaultValue("")]
		public string OnFacebookSend
		{
			get
			{
				return ((string)this.ViewState["OnFacebookSend"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnFacebookSend"] = value;
			}
		}

		// Token: 0x17002E28 RID: 11816
		// (get) Token: 0x0600921E RID: 37406 RVA: 0x0020EDA8 File Offset: 0x0020CFA8
		// (set) Token: 0x0600921F RID: 37407 RVA: 0x0020EDC8 File Offset: 0x0020CFC8
		[DefaultValue("")]
		[Description("Specifies the name of the JavaScript function that is called when the Tweet standard button is clicked.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("tweet")]
		[Category("Client-side events")]
		public string OnTweet
		{
			get
			{
				return ((string)this.ViewState["OnTweet"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnTweet"] = value;
			}
		}

		// Token: 0x17002E29 RID: 11817
		// (get) Token: 0x06009220 RID: 37408 RVA: 0x0020EDDB File Offset: 0x0020CFDB
		// (set) Token: 0x06009221 RID: 37409 RVA: 0x0020EDFB File Offset: 0x0020CFFB
		[Category("Client-side events")]
		[Description("Specifies the name of the JavaScript function that is called when the LinkedIn standard button is clicked.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("linkedInShare")]
		public string OnLinkedInShare
		{
			get
			{
				return ((string)this.ViewState["OnLinkedInShare"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnLinkedInShare"] = value;
			}
		}

		// Token: 0x17002E2A RID: 11818
		// (get) Token: 0x06009222 RID: 37410 RVA: 0x0020EE0E File Offset: 0x0020D00E
		// (set) Token: 0x06009223 RID: 37411 RVA: 0x0020EE2E File Offset: 0x0020D02E
		[DefaultValue("")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("onPlusone")]
		[Description("Specifies the name of the JavaScript function that is called when the GooglePlus standard button is clicked for approval.")]
		[ClientControlEvent]
		public string OnGooglePlusOneOn
		{
			get
			{
				return ((string)this.ViewState["OnGooglePlusOneOn"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnGooglePlusOneOn"] = value;
			}
		}

		// Token: 0x17002E2B RID: 11819
		// (get) Token: 0x06009224 RID: 37412 RVA: 0x0020EE41 File Offset: 0x0020D041
		// (set) Token: 0x06009225 RID: 37413 RVA: 0x0020EE61 File Offset: 0x0020D061
		[ClientControlEvent]
		[Description("Specifies the name of the JavaScript function that is called when the GooglePlus standard button is clicked for disapproval.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("offPlusone")]
		[Category("Client-side events")]
		public string OnGooglePlusOneOff
		{
			get
			{
				return ((string)this.ViewState["OnGooglePlusOneOff"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnGooglePlusOneOff"] = value;
			}
		}

		// Token: 0x06009226 RID: 37414 RVA: 0x0020EE74 File Offset: 0x0020D074
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "fbAppId", this.FacebookAppId, "");
			base.DescribeProperty<string>(descriptor, "gaID", this.GoogleAnalyticsUA, "");
			base.DescribeProperty<bool>(descriptor, "hideIframesOnDialogMove", this.HideIframesOnDialogMove, true);
			base.DescribeProperty<string>(descriptor, "titleToShare", this.TitleToShare, "");
			base.DescribeProperty<string>(descriptor, "urlToShare", this.UrlToShare, "");
			base.DescribeProperty<string>(descriptor, "yammerAppId", this.YammerAppId, "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06009227 RID: 37415 RVA: 0x0020EF10 File Offset: 0x0020D110
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "fbLike", this.OnFacebookLike);
			RadWebControl.DescribeEvent(descriptor, "fbSend", this.OnFacebookSend);
			RadWebControl.DescribeEvent(descriptor, "fbUnLike", this.OnFacebookUnLike);
			RadWebControl.DescribeEvent(descriptor, "offPlusone", this.OnGooglePlusOneOff);
			RadWebControl.DescribeEvent(descriptor, "onPlusone", this.OnGooglePlusOneOn);
			RadWebControl.DescribeEvent(descriptor, "linkedInShare", this.OnLinkedInShare);
			RadWebControl.DescribeEvent(descriptor, "clicked", this.OnSocialButtonClicked);
			RadWebControl.DescribeEvent(descriptor, "clicking", this.OnSocialButtonClicking);
			RadWebControl.DescribeEvent(descriptor, "tweet", this.OnTweet);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x040029D7 RID: 10711
		private const int AdditionalSpaceInCompactPopup = 70;

		// Token: 0x040029D8 RID: 10712
		private bool _addFbScript;

		// Token: 0x040029D9 RID: 10713
		private bool _addGoogleScript;

		// Token: 0x040029DA RID: 10714
		private bool _addTwitterScript;

		// Token: 0x040029DB RID: 10715
		private bool _addLinkedInScript;

		// Token: 0x040029DC RID: 10716
		private bool _addYammerScript;

		// Token: 0x040029DD RID: 10717
		private bool _addPinterestScript;

		// Token: 0x040029DE RID: 10718
		private RadWindow _compactPopup;

		// Token: 0x040029DF RID: 10719
		private RadComboBox _comboBox;

		// Token: 0x040029E0 RID: 10720
		private RadWindow _emailPopup;

		// Token: 0x040029E1 RID: 10721
		private Dictionary<string, int> minimumPopupWidthPerSkin = new Dictionary<string, int>
		{
			{
				"Bootstrap",
				202
			},
			{
				"BlackMetroTouch",
				202
			},
			{
				"Material",
				202
			},
			{
				"MetroTouch",
				202
			},
			{
				"Glow",
				168
			},
			{
				"Silk",
				168
			}
		};

		// Token: 0x040029E2 RID: 10722
		private GenericSocialButtonsCollection<RadSocialButton> _compactButtons;

		// Token: 0x040029E3 RID: 10723
		private GenericSocialButtonsCollection<RadSocialButtonBase> _mainButtons;

		// Token: 0x040029E4 RID: 10724
		private RadSocialShareEmailSettings _emailSettings;
	}
}
