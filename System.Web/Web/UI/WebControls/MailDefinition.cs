using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Net.Configuration;
using System.Net.Mail;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005DC RID: 1500
	[Bindable(false)]
	[ParseChildren(true, "")]
	[TypeConverter(typeof(EmptyStringExpandableObjectConverter))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class MailDefinition : IStateManager
	{
		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x06004978 RID: 18808 RVA: 0x0012B341 File Offset: 0x0012A341
		// (set) Token: 0x06004979 RID: 18809 RVA: 0x0012B357 File Offset: 0x0012A357
		[WebCategory("Behavior")]
		[UrlProperty("*.*")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[WebSysDescription("MailDefinition_BodyFileName")]
		[Editor("System.Web.UI.Design.WebControls.MailDefinitionBodyFileNameEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string BodyFileName
		{
			get
			{
				if (this._bodyFileName != null)
				{
					return this._bodyFileName;
				}
				return string.Empty;
			}
			set
			{
				this._bodyFileName = value;
			}
		}

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x0600497A RID: 18810 RVA: 0x0012B360 File Offset: 0x0012A360
		// (set) Token: 0x0600497B RID: 18811 RVA: 0x0012B38D File Offset: 0x0012A38D
		[WebSysDescription("MailDefinition_CC")]
		[WebCategory("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string CC
		{
			get
			{
				object obj = this.ViewState["CC"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["CC"] = value;
			}
		}

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x0600497C RID: 18812 RVA: 0x0012B3A0 File Offset: 0x0012A3A0
		// (set) Token: 0x0600497D RID: 18813 RVA: 0x0012B3CD File Offset: 0x0012A3CD
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[WebSysDescription("MailDefinition_From")]
		[WebCategory("Behavior")]
		public string From
		{
			get
			{
				object obj = this.ViewState["From"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["From"] = value;
			}
		}

		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x0600497E RID: 18814 RVA: 0x0012B3E0 File Offset: 0x0012A3E0
		[WebSysDescription("MailDefinition_EmbeddedObjects")]
		[WebCategory("Behavior")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		public EmbeddedMailObjectsCollection EmbeddedObjects
		{
			get
			{
				if (this._embeddedObjects == null)
				{
					this._embeddedObjects = new EmbeddedMailObjectsCollection();
				}
				return this._embeddedObjects;
			}
		}

		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x0600497F RID: 18815 RVA: 0x0012B3FC File Offset: 0x0012A3FC
		// (set) Token: 0x06004980 RID: 18816 RVA: 0x0012B425 File Offset: 0x0012A425
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		[WebSysDescription("MailDefinition_IsBodyHtml")]
		[NotifyParentProperty(true)]
		public bool IsBodyHtml
		{
			get
			{
				object obj = this.ViewState["IsBodyHtml"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["IsBodyHtml"] = value;
			}
		}

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x06004981 RID: 18817 RVA: 0x0012B440 File Offset: 0x0012A440
		// (set) Token: 0x06004982 RID: 18818 RVA: 0x0012B469 File Offset: 0x0012A469
		[DefaultValue(MailPriority.Normal)]
		[NotifyParentProperty(true)]
		[WebCategory("Behavior")]
		[WebSysDescription("MailDefinition_Priority")]
		public MailPriority Priority
		{
			get
			{
				object obj = this.ViewState["Priority"];
				if (obj != null)
				{
					return (MailPriority)obj;
				}
				return MailPriority.Normal;
			}
			set
			{
				if (value < MailPriority.Normal || value > MailPriority.High)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["Priority"] = value;
			}
		}

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x06004983 RID: 18819 RVA: 0x0012B494 File Offset: 0x0012A494
		// (set) Token: 0x06004984 RID: 18820 RVA: 0x0012B4C1 File Offset: 0x0012A4C1
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("MailDefinition_Subject")]
		[NotifyParentProperty(true)]
		public string Subject
		{
			get
			{
				object obj = this.ViewState["Subject"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Subject"] = value;
			}
		}

		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x06004985 RID: 18821 RVA: 0x0012B4D4 File Offset: 0x0012A4D4
		internal string SubjectInternal
		{
			get
			{
				return (string)this.ViewState["Subject"];
			}
		}

		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x06004986 RID: 18822 RVA: 0x0012B4EB File Offset: 0x0012A4EB
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		private StateBag ViewState
		{
			get
			{
				if (this._viewState == null)
				{
					this._viewState = new StateBag(false);
					if (this._isTrackingViewState)
					{
						((IStateManager)this._viewState).TrackViewState();
					}
				}
				return this._viewState;
			}
		}

		// Token: 0x06004987 RID: 18823 RVA: 0x0012B51C File Offset: 0x0012A51C
		public MailMessage CreateMailMessage(string recipients, IDictionary replacements, Control owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			string body = string.Empty;
			string bodyFileName = this.BodyFileName;
			if (!string.IsNullOrEmpty(bodyFileName))
			{
				string text = bodyFileName;
				if (!UrlPath.IsAbsolutePhysicalPath(text))
				{
					text = UrlPath.Combine(owner.AppRelativeTemplateSourceDirectory, text);
				}
				TextReader textReader = new StreamReader(owner.OpenFile(text));
				try
				{
					body = textReader.ReadToEnd();
				}
				finally
				{
					textReader.Close();
				}
			}
			return this.CreateMailMessage(recipients, replacements, body, owner);
		}

		// Token: 0x06004988 RID: 18824 RVA: 0x0012B59C File Offset: 0x0012A59C
		public MailMessage CreateMailMessage(string recipients, IDictionary replacements, string body, Control owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			string from = this.From;
			if (string.IsNullOrEmpty(from))
			{
				SmtpSection smtp = RuntimeConfig.GetConfig().Smtp;
				if (smtp == null || smtp.Network == null || string.IsNullOrEmpty(smtp.From))
				{
					throw new HttpException(SR.GetString("MailDefinition_NoFromAddressSpecified"));
				}
				from = smtp.From;
			}
			MailMessage mailMessage = null;
			MailMessage result;
			try
			{
				mailMessage = new MailMessage(from, recipients);
				if (!string.IsNullOrEmpty(this.CC))
				{
					mailMessage.CC.Add(this.CC);
				}
				if (!string.IsNullOrEmpty(this.Subject))
				{
					mailMessage.Subject = this.Subject;
				}
				mailMessage.Priority = this.Priority;
				if (replacements != null && !string.IsNullOrEmpty(body))
				{
					foreach (object obj in replacements.Keys)
					{
						string text = obj as string;
						string text2 = replacements[obj] as string;
						if (text == null || text2 == null)
						{
							throw new ArgumentException(SR.GetString("MailDefinition_InvalidReplacements"));
						}
						text2 = text2.Replace("$", "$$");
						body = Regex.Replace(body, text, text2, RegexOptions.IgnoreCase);
					}
				}
				if (this.EmbeddedObjects.Count > 0)
				{
					string mediaType = this.IsBodyHtml ? "text/html" : "text/plain";
					AlternateView alternateView = AlternateView.CreateAlternateViewFromString(body, null, mediaType);
					foreach (object obj2 in this.EmbeddedObjects)
					{
						EmbeddedMailObject embeddedMailObject = (EmbeddedMailObject)obj2;
						string text3 = embeddedMailObject.Path;
						if (string.IsNullOrEmpty(text3))
						{
							throw ExceptionUtil.PropertyNullOrEmpty("EmbeddedMailObject.Path");
						}
						if (!UrlPath.IsAbsolutePhysicalPath(text3))
						{
							VirtualPath virtualPath = VirtualPath.Combine(owner.TemplateControlVirtualDirectory, VirtualPath.Create(text3));
							text3 = virtualPath.AppRelativeVirtualPathString;
						}
						LinkedResource linkedResource = null;
						try
						{
							Stream stream = null;
							try
							{
								stream = owner.OpenFile(text3);
								linkedResource = new LinkedResource(stream);
							}
							catch
							{
								if (stream != null)
								{
									((IDisposable)stream).Dispose();
								}
								throw;
							}
							linkedResource.ContentId = embeddedMailObject.Name;
							linkedResource.ContentType.Name = UrlPath.GetFileName(text3);
							alternateView.LinkedResources.Add(linkedResource);
						}
						catch
						{
							if (linkedResource != null)
							{
								linkedResource.Dispose();
							}
							throw;
						}
					}
					mailMessage.AlternateViews.Add(alternateView);
				}
				else if (!string.IsNullOrEmpty(body))
				{
					mailMessage.Body = body;
				}
				mailMessage.IsBodyHtml = this.IsBodyHtml;
				result = mailMessage;
			}
			catch
			{
				if (mailMessage != null)
				{
					mailMessage.Dispose();
				}
				throw;
			}
			return result;
		}

		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x06004989 RID: 18825 RVA: 0x0012B8C0 File Offset: 0x0012A8C0
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x0600498A RID: 18826 RVA: 0x0012B8C8 File Offset: 0x0012A8C8
		void IStateManager.LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				((IStateManager)this.ViewState).LoadViewState(savedState);
			}
		}

		// Token: 0x0600498B RID: 18827 RVA: 0x0012B8D9 File Offset: 0x0012A8D9
		object IStateManager.SaveViewState()
		{
			if (this._viewState != null)
			{
				return ((IStateManager)this._viewState).SaveViewState();
			}
			return null;
		}

		// Token: 0x0600498C RID: 18828 RVA: 0x0012B8F0 File Offset: 0x0012A8F0
		void IStateManager.TrackViewState()
		{
			this._isTrackingViewState = true;
			if (this._viewState != null)
			{
				((IStateManager)this._viewState).TrackViewState();
			}
		}

		// Token: 0x04002B35 RID: 11061
		private bool _isTrackingViewState;

		// Token: 0x04002B36 RID: 11062
		private StateBag _viewState;

		// Token: 0x04002B37 RID: 11063
		private EmbeddedMailObjectsCollection _embeddedObjects;

		// Token: 0x04002B38 RID: 11064
		private string _bodyFileName;
	}
}
