using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000468 RID: 1128
	[Bindable(false)]
	[TypeConverter(typeof(EmptyStringExpandableObjectConverter))]
	[ParseChildren(true, "")]
	public sealed class MailDefinition : IStateManager
	{
		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x060036D2 RID: 14034 RVA: 0x000B150D File Offset: 0x000AF70D
		// (set) Token: 0x060036D3 RID: 14035 RVA: 0x000B1523 File Offset: 0x000AF723
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("MailDefinition_BodyFileName")]
		[Editor("System.Web.UI.Design.WebControls.MailDefinitionBodyFileNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty("*.*")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x060036D4 RID: 14036 RVA: 0x000B152C File Offset: 0x000AF72C
		// (set) Token: 0x060036D5 RID: 14037 RVA: 0x000B1559 File Offset: 0x000AF759
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("MailDefinition_CC")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x060036D6 RID: 14038 RVA: 0x000B156C File Offset: 0x000AF76C
		// (set) Token: 0x060036D7 RID: 14039 RVA: 0x000B1599 File Offset: 0x000AF799
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("MailDefinition_From")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x060036D8 RID: 14040 RVA: 0x000B15AC File Offset: 0x000AF7AC
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Behavior")]
		[WebSysDescription("MailDefinition_EmbeddedObjects")]
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

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x060036D9 RID: 14041 RVA: 0x000B15C8 File Offset: 0x000AF7C8
		// (set) Token: 0x060036DA RID: 14042 RVA: 0x000B15F1 File Offset: 0x000AF7F1
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

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x060036DB RID: 14043 RVA: 0x000B160C File Offset: 0x000AF80C
		// (set) Token: 0x060036DC RID: 14044 RVA: 0x000B1635 File Offset: 0x000AF835
		[WebCategory("Behavior")]
		[DefaultValue(MailPriority.Normal)]
		[WebSysDescription("MailDefinition_Priority")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x060036DD RID: 14045 RVA: 0x000B1660 File Offset: 0x000AF860
		// (set) Token: 0x060036DE RID: 14046 RVA: 0x000B168D File Offset: 0x000AF88D
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

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x060036DF RID: 14047 RVA: 0x000B16A0 File Offset: 0x000AF8A0
		internal string SubjectInternal
		{
			get
			{
				return (string)this.ViewState["Subject"];
			}
		}

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x060036E0 RID: 14048 RVA: 0x000B16B7 File Offset: 0x000AF8B7
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x060036E1 RID: 14049 RVA: 0x000B16E8 File Offset: 0x000AF8E8
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

		// Token: 0x060036E2 RID: 14050 RVA: 0x000B1768 File Offset: 0x000AF968
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

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x060036E3 RID: 14051 RVA: 0x000B1A8C File Offset: 0x000AFC8C
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x060036E4 RID: 14052 RVA: 0x000B1A94 File Offset: 0x000AFC94
		void IStateManager.LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				((IStateManager)this.ViewState).LoadViewState(savedState);
			}
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x000B1AA5 File Offset: 0x000AFCA5
		object IStateManager.SaveViewState()
		{
			if (this._viewState != null)
			{
				return ((IStateManager)this._viewState).SaveViewState();
			}
			return null;
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x000B1ABC File Offset: 0x000AFCBC
		void IStateManager.TrackViewState()
		{
			this._isTrackingViewState = true;
			if (this._viewState != null)
			{
				((IStateManager)this._viewState).TrackViewState();
			}
		}

		// Token: 0x04002226 RID: 8742
		private bool _isTrackingViewState;

		// Token: 0x04002227 RID: 8743
		private StateBag _viewState;

		// Token: 0x04002228 RID: 8744
		private EmbeddedMailObjectsCollection _embeddedObjects;

		// Token: 0x04002229 RID: 8745
		private string _bodyFileName;
	}
}
