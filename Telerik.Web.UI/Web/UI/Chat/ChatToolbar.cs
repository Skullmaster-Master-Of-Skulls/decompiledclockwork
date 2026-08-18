using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Chat
{
	// Token: 0x0200007F RID: 127
	public class ChatToolbar : StateManager, IDefaultCheck
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0000CE45 File Offset: 0x0000B045
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x0000CE66 File Offset: 0x0000B066
		[DefaultValue(false)]
		public bool Animation
		{
			get
			{
				return (bool)(base.ViewState["Animation"] ?? false);
			}
			set
			{
				base.ViewState["Animation"] = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0000CE7E File Offset: 0x0000B07E
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Animation AnimationSettings
		{
			get
			{
				if (this._animation == null)
				{
					this._animation = new Animation();
				}
				return this._animation;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0000CE99 File Offset: 0x0000B099
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ChatToolbarButtonsCollection ButtonsCollection
		{
			get
			{
				if (this._buttons == null)
				{
					this._buttons = new ChatToolbarButtonsCollection();
				}
				return this._buttons;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x0000CEB4 File Offset: 0x0000B0B4
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x0000CED5 File Offset: 0x0000B0D5
		[DefaultValue(false)]
		public bool Scrollable
		{
			get
			{
				return (bool)(base.ViewState["Scrollable"] ?? false);
			}
			set
			{
				base.ViewState["Scrollable"] = value;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0000CEED File Offset: 0x0000B0ED
		// (set) Token: 0x0600052F RID: 1327 RVA: 0x0000CF0E File Offset: 0x0000B10E
		[DefaultValue(false)]
		public bool Toggleable
		{
			get
			{
				return (bool)(base.ViewState["Toggleable"] ?? false);
			}
			set
			{
				base.ViewState["Toggleable"] = value;
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000CF26 File Offset: 0x0000B126
		internal override void SetDirty()
		{
			base.SetDirty();
			this.AnimationSettings.SetDirty();
			this.ButtonsCollection.SetDirty();
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000CF44 File Offset: 0x0000B144
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.AnimationSettings).LoadViewState(array[num++]);
			((IStateManager)this.ButtonsCollection).LoadViewState(array[num++]);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000CF8C File Offset: 0x0000B18C
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.AnimationSettings).SaveViewState(),
				((IStateManager)this.ButtonsCollection).SaveViewState()
			};
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000CFC8 File Offset: 0x0000B1C8
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.AnimationSettings).TrackViewState();
			((IStateManager)this.ButtonsCollection).TrackViewState();
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0000CFE6 File Offset: 0x0000B1E6
		public bool IsDefault
		{
			get
			{
				return !this.Animation && this.AnimationSettings.IsDefault && this.ButtonsCollection.ItemsList.Count == 0 && !this.Scrollable && !this.Toggleable;
			}
		}

		// Token: 0x040000B4 RID: 180
		private Animation _animation;

		// Token: 0x040000B5 RID: 181
		private ChatToolbarButtonsCollection _buttons;
	}
}
