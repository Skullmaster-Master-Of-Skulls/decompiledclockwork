using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Licensing;
using Telerik.Web.UI.Chat;

namespace Telerik.Web.UI
{
	// Token: 0x0200008A RID: 138
	[RequiredScript(typeof(Html5Chat))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadChat))]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Classic, typeof(RadChat))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Mobile, typeof(RadChat))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Mobile, typeof(RadChat))]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ClientScriptResource("Telerik.Web.UI.RadChat", "Telerik.Web.UI.Chat.Scripts.RadChat.js")]
	[EmbeddedSkin("Chat", typeof(RadChat))]
	[ParseChildren(ChildrenAsProperties = true)]
	[EmbeddedSkin("Chat", "Default", typeof(RadChat))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Classic, typeof(RadChat))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Lightweight, typeof(RadChat))]
	public class RadChat : RadWebControl
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x0000D50E File Offset: 0x0000B70E
		public RadChat()
		{
			this.RegisterJSConverters();
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0000D527 File Offset: 0x0000B727
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Messages MessagesSettings
		{
			get
			{
				if (this._messages == null)
				{
					this._messages = new Messages();
				}
				return this._messages;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x0000D542 File Offset: 0x0000B742
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public User UserSettings
		{
			get
			{
				if (this._user == null)
				{
					this._user = new User();
				}
				return this._user;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0000D55D File Offset: 0x0000B75D
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ChatToolbar ToolbarSettings
		{
			get
			{
				if (this._toolbar == null)
				{
					this._toolbar = new ChatToolbar();
				}
				return this._toolbar;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x0000D578 File Offset: 0x0000B778
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ChatClientEvents ClientEvents
		{
			get
			{
				if (this._clientEvents == null)
				{
					this._clientEvents = new ChatClientEvents();
				}
				return this._clientEvents;
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0000D593 File Offset: 0x0000B793
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddProperty("skin", base.RuntimeSkin);
			descriptor.AddScriptProperty("_options", this.serializer.Serialize(this));
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			ScriptObjectBuilder.RegisterCssReferences(this);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000D5D4 File Offset: 0x0000B7D4
		private void RegisterJSConverters()
		{
			List<JavaScriptConverter> converters = new List<JavaScriptConverter>
			{
				new RadChatConverter(),
				new MessagesConverter(),
				new UserConverter(),
				new CollapseConverter(),
				new ExpandConverter(),
				new AnimationConverter(),
				new ChatToolbarButtonConverter(),
				new ChatToolbarConverter()
			};
			this.serializer.RegisterConverters(converters);
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x0000D64D File Offset: 0x0000B84D
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0000D651 File Offset: 0x0000B851
		protected override string CssClassFormatString
		{
			get
			{
				return "RadChat RadChat_{0}";
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0000D658 File Offset: 0x0000B858
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.ClientEvents).LoadViewState(array[num++]);
			((IStateManager)this.MessagesSettings).LoadViewState(array[num++]);
			((IStateManager)this.ToolbarSettings).LoadViewState(array[num++]);
			((IStateManager)this.UserSettings).LoadViewState(array[num++]);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0000D6C4 File Offset: 0x0000B8C4
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.ClientEvents).SaveViewState(),
				((IStateManager)this.MessagesSettings).SaveViewState(),
				((IStateManager)this.ToolbarSettings).SaveViewState(),
				((IStateManager)this.UserSettings).SaveViewState()
			};
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0000D71C File Offset: 0x0000B91C
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.ClientEvents).TrackViewState();
			((IStateManager)this.MessagesSettings).TrackViewState();
			((IStateManager)this.ToolbarSettings).TrackViewState();
			((IStateManager)this.UserSettings).TrackViewState();
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0000D750 File Offset: 0x0000B950
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0000D75C File Offset: 0x0000B95C
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "actionClick", this.ClientEvents.OnActionClick);
			RadWebControl.DescribeEvent(descriptor, "initialize", this.ClientEvents.OnInitialize);
			RadWebControl.DescribeEvent(descriptor, "load", this.ClientEvents.OnLoad);
			RadWebControl.DescribeEvent(descriptor, "post", this.ClientEvents.OnPost);
			RadWebControl.DescribeEvent(descriptor, "sendMessage", this.ClientEvents.OnSendMessage);
			RadWebControl.DescribeEvent(descriptor, "typingEnd", this.ClientEvents.OnTypingEnd);
			RadWebControl.DescribeEvent(descriptor, "typingStart", this.ClientEvents.OnTypingStart);
			RadWebControl.DescribeEvent(descriptor, "toolClick", this.ClientEvents.OnToolClick);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x040000B6 RID: 182
		private Messages _messages;

		// Token: 0x040000B7 RID: 183
		private User _user;

		// Token: 0x040000B8 RID: 184
		private ChatToolbar _toolbar;

		// Token: 0x040000B9 RID: 185
		private ChatClientEvents _clientEvents;

		// Token: 0x040000BA RID: 186
		private readonly AdvancedJavaScriptSerializer serializer = new AdvancedJavaScriptSerializer();
	}
}
