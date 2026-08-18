using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000542 RID: 1346
	public class ImageGalleryKeyboardNavigationSettings : ImageGallerySettings
	{
		// Token: 0x06002F89 RID: 12169 RVA: 0x0009BA45 File Offset: 0x00099C45
		public ImageGalleryKeyboardNavigationSettings(RadImageGallery gallery) : base(gallery)
		{
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06002F8A RID: 12170 RVA: 0x0009BA50 File Offset: 0x00099C50
		// (set) Token: 0x06002F8B RID: 12171 RVA: 0x0009BA79 File Offset: 0x00099C79
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public bool AllowCycle
		{
			get
			{
				object obj = base.ViewState["AllowCycle"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["AllowCycle"] = value;
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06002F8C RID: 12172 RVA: 0x0009BA94 File Offset: 0x00099C94
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Description("")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public KeyboardNavigationShortcuts<ImageGalleryShortcut> Shortcuts
		{
			get
			{
				if (this.shortcuts == null)
				{
					this.shortcuts = new KeyboardNavigationShortcuts<ImageGalleryShortcut>();
					this.shortcuts.AddDefaultShortcuts(new ImageGalleryShortcut[]
					{
						new ImageGalleryShortcut
						{
							Command = ImageGalleryShortcutCommand.Focus,
							Key = KeyboardNavigationKey.Y,
							Modifiers = KeyboardNavigationModifier.Ctrl
						},
						new ImageGalleryShortcut
						{
							Command = ImageGalleryShortcutCommand.Prev,
							Key = KeyboardNavigationKey.LeftArrow
						},
						new ImageGalleryShortcut
						{
							Command = ImageGalleryShortcutCommand.Next,
							Key = KeyboardNavigationKey.RightArrow
						},
						new ImageGalleryShortcut
						{
							Command = ImageGalleryShortcutCommand.Up,
							Key = KeyboardNavigationKey.UpArrow
						},
						new ImageGalleryShortcut
						{
							Command = ImageGalleryShortcutCommand.Down,
							Key = KeyboardNavigationKey.DownArrow
						},
						new ImageGalleryShortcut
						{
							Command = ImageGalleryShortcutCommand.MoveToFirst,
							Key = KeyboardNavigationKey.Home
						},
						new ImageGalleryShortcut
						{
							Command = ImageGalleryShortcutCommand.MoveToLast,
							Key = KeyboardNavigationKey.End
						},
						new ImageGalleryShortcut
						{
							Command = ImageGalleryShortcutCommand.PrevView,
							Key = KeyboardNavigationKey.LeftArrow,
							Modifiers = KeyboardNavigationModifier.Alt
						},
						new ImageGalleryShortcut
						{
							Command = ImageGalleryShortcutCommand.NextView,
							Key = KeyboardNavigationKey.RightArrow,
							Modifiers = KeyboardNavigationModifier.Alt
						},
						new ImageGalleryShortcut
						{
							Command = ImageGalleryShortcutCommand.PrevView,
							Key = KeyboardNavigationKey.UpArrow,
							Modifiers = KeyboardNavigationModifier.Alt
						},
						new ImageGalleryShortcut
						{
							Command = ImageGalleryShortcutCommand.NextView,
							Key = KeyboardNavigationKey.DownArrow,
							Modifiers = KeyboardNavigationModifier.Alt
						},
						new ImageGalleryShortcut
						{
							Command = ImageGalleryShortcutCommand.Select,
							Key = KeyboardNavigationKey.Enter
						},
						new ImageGalleryShortcut
						{
							Command = ImageGalleryShortcutCommand.Close,
							Key = KeyboardNavigationKey.Escape
						},
						new ImageGalleryShortcut
						{
							Command = ImageGalleryShortcutCommand.ToggleSlideshow,
							Key = KeyboardNavigationKey.Space
						},
						new ImageGalleryShortcut
						{
							Command = ImageGalleryShortcutCommand.ToggleFullScreen,
							Key = KeyboardNavigationKey.F
						},
						new ImageGalleryShortcut
						{
							Command = ImageGalleryShortcutCommand.ToggleThumbnails,
							Key = KeyboardNavigationKey.T
						},
						new ImageGalleryShortcut
						{
							Command = ImageGalleryShortcutCommand.PrevPage,
							Key = KeyboardNavigationKey.PageDown
						},
						new ImageGalleryShortcut
						{
							Command = ImageGalleryShortcutCommand.NextPage,
							Key = KeyboardNavigationKey.PageUp
						}
					});
				}
				if (this.IsTrackingViewState)
				{
					((IStateManager)this.shortcuts).TrackViewState();
				}
				return this.shortcuts;
			}
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x0009BD2C File Offset: 0x00099F2C
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			object value = base.SaveViewState();
			arrayList.Add(value);
			arrayList.Add(((IStateManager)this.Shortcuts).SaveViewState());
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x0009BD70 File Offset: 0x00099F70
		protected override void LoadViewState(object state)
		{
			if (state != null)
			{
				object[] array = (object[])state;
				int num = 0;
				base.LoadViewState(array[num++]);
				((IStateManager)this.Shortcuts).LoadViewState(array[num++]);
			}
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x0009BDA8 File Offset: 0x00099FA8
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.IsTrackingViewState)
			{
				return;
			}
			((IStateManager)this.Shortcuts).TrackViewState();
		}

		// Token: 0x04000CB8 RID: 3256
		private KeyboardNavigationShortcuts<ImageGalleryShortcut> shortcuts;
	}
}
