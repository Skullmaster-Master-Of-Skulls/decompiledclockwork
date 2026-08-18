using System;
using System.Runtime.CompilerServices;

namespace System.Windows.Forms
{
	// Token: 0x020002E5 RID: 741
	internal static class LocalAppContextSwitches
	{
		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06002EB2 RID: 11954 RVA: 0x000D3422 File Offset: 0x000D1622
		public static bool DontSupportReentrantFilterMessage
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.DontSupportReentrantFilterMessage", ref LocalAppContextSwitches._dontSupportReentrantFilterMessage);
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06002EB3 RID: 11955 RVA: 0x000D3433 File Offset: 0x000D1633
		public static bool DoNotSupportSelectAllShortcutInMultilineTextBox
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.DoNotSupportSelectAllShortcutInMultilineTextBox", ref LocalAppContextSwitches._doNotSupportSelectAllShortcutInMultilineTextBox);
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06002EB4 RID: 11956 RVA: 0x000D3444 File Offset: 0x000D1644
		public static bool DoNotLoadLatestRichEditControl
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.DoNotLoadLatestRichEditControl", ref LocalAppContextSwitches._doNotLoadLatestRichEditControl);
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06002EB5 RID: 11957 RVA: 0x000D3455 File Offset: 0x000D1655
		public static bool UseLegacyContextMenuStripSourceControlValue
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.UseLegacyContextMenuStripSourceControlValue", ref LocalAppContextSwitches._useLegacyContextMenuStripSourceControlValue);
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06002EB6 RID: 11958 RVA: 0x000D3466 File Offset: 0x000D1666
		public static bool UseLegacyDomainUpDownControlScrolling
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.DomainUpDown.UseLegacyScrolling", ref LocalAppContextSwitches._useLegacyDomainUpDownScrolling);
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06002EB7 RID: 11959 RVA: 0x000D3477 File Offset: 0x000D1677
		public static bool AllowUpdateChildControlIndexForTabControls
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.AllowUpdateChildControlIndexForTabControls", ref LocalAppContextSwitches._allowUpdateChildControlIndexForTabControls);
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06002EB8 RID: 11960 RVA: 0x000D3488 File Offset: 0x000D1688
		public static bool UseLegacyImages
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.UseLegacyImages", ref LocalAppContextSwitches._useLegacyImages);
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06002EB9 RID: 11961 RVA: 0x000D3499 File Offset: 0x000D1699
		public static bool EnableVisualStyleValidation
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.EnableVisualStyleValidation", ref LocalAppContextSwitches._enableVisualStyleValidation);
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06002EBA RID: 11962 RVA: 0x000D34AA File Offset: 0x000D16AA
		public static bool EnableLegacyDangerousClipboardDeserializationMode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (LocalAppContextSwitches._enableLegacyDangerousClipboardDeserializationMode < 0)
				{
					return false;
				}
				if (LocalAppContextSwitches._enableLegacyDangerousClipboardDeserializationMode > 0)
				{
					return true;
				}
				if (UnsafeNativeMethods.IsDynamicCodePolicyEnabled())
				{
					LocalAppContextSwitches._enableLegacyDangerousClipboardDeserializationMode = -1;
				}
				else
				{
					LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.EnableLegacyDangerousClipboardDeserializationMode", ref LocalAppContextSwitches._enableLegacyDangerousClipboardDeserializationMode);
				}
				return LocalAppContextSwitches._enableLegacyDangerousClipboardDeserializationMode > 0;
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06002EBB RID: 11963 RVA: 0x000D34E7 File Offset: 0x000D16E7
		public static bool EnableLegacyChineseIMEIndicator
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.EnableLegacyChineseIMEIndicator", ref LocalAppContextSwitches._enableLegacyChineseIMEIndicator);
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06002EBC RID: 11964 RVA: 0x000D34F8 File Offset: 0x000D16F8
		public static bool EnableLegacyIMEFocusInComboBox
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.EnableLegacyIMEFocusInComboBox", ref LocalAppContextSwitches._enableLegacyIMEFocusInComboBox);
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06002EBD RID: 11965 RVA: 0x000D3509 File Offset: 0x000D1709
		public static bool DisconnectUiaProvidersOnWmDestroy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.DisconnectUiaProvidersOnWmDestroy", ref LocalAppContextSwitches._disconnectUiaProvidersOnWmDestroy);
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06002EBE RID: 11966 RVA: 0x000D351A File Offset: 0x000D171A
		public static bool NoClientNotifications
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.AccessibleObject.NoClientNotifications", ref LocalAppContextSwitches._noClientNotifications);
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06002EBF RID: 11967 RVA: 0x000D352B File Offset: 0x000D172B
		public static bool FreeControlsForRefCountedAccessibleObjectsInLevel5
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.FreeControlsForRefCountedAccessibleObjectsInLevel5", ref LocalAppContextSwitches._freeControlsForRefCountedAccessibleObjectsInLevel5);
			}
		}

		// Token: 0x04001351 RID: 4945
		internal const string DontSupportReentrantFilterMessageSwitchName = "Switch.System.Windows.Forms.DontSupportReentrantFilterMessage";

		// Token: 0x04001352 RID: 4946
		internal const string DoNotSupportSelectAllShortcutInMultilineTextBoxSwitchName = "Switch.System.Windows.Forms.DoNotSupportSelectAllShortcutInMultilineTextBox";

		// Token: 0x04001353 RID: 4947
		internal const string DoNotLoadLatestRichEditControlSwitchName = "Switch.System.Windows.Forms.DoNotLoadLatestRichEditControl";

		// Token: 0x04001354 RID: 4948
		internal const string UseLegacyContextMenuStripSourceControlValueSwitchName = "Switch.System.Windows.Forms.UseLegacyContextMenuStripSourceControlValue";

		// Token: 0x04001355 RID: 4949
		internal const string DomainUpDownUseLegacyScrollingSwitchName = "Switch.System.Windows.Forms.DomainUpDown.UseLegacyScrolling";

		// Token: 0x04001356 RID: 4950
		internal const string AllowUpdateChildControlIndexForTabControlsSwitchName = "Switch.System.Windows.Forms.AllowUpdateChildControlIndexForTabControls";

		// Token: 0x04001357 RID: 4951
		internal const string UseLegacyImagesSwitchName = "Switch.System.Windows.Forms.UseLegacyImages";

		// Token: 0x04001358 RID: 4952
		internal const string EnableVisualStyleValidationSwitchName = "Switch.System.Windows.Forms.EnableVisualStyleValidation";

		// Token: 0x04001359 RID: 4953
		internal const string EnableLegacyDangerousClipboardDeserializationModeSwitchName = "Switch.System.Windows.Forms.EnableLegacyDangerousClipboardDeserializationMode";

		// Token: 0x0400135A RID: 4954
		internal const string EnableLegacyChineseIMEIndicatorSwitchName = "Switch.System.Windows.Forms.EnableLegacyChineseIMEIndicator";

		// Token: 0x0400135B RID: 4955
		internal const string EnableLegacyIMEFocusInComboBoxSwitchName = "Switch.System.Windows.Forms.EnableLegacyIMEFocusInComboBox";

		// Token: 0x0400135C RID: 4956
		internal const string DisconnectUiaProvidersOnWmDestroySwitchName = "Switch.System.Windows.Forms.DisconnectUiaProvidersOnWmDestroy";

		// Token: 0x0400135D RID: 4957
		internal const string NoClientNotificationsSwitchName = "Switch.System.Windows.Forms.AccessibleObject.NoClientNotifications";

		// Token: 0x0400135E RID: 4958
		internal const string FreeControlsForRefCountedAccessibleObjectsInLevel5SwitchName = "Switch.System.Windows.Forms.FreeControlsForRefCountedAccessibleObjectsInLevel5";

		// Token: 0x0400135F RID: 4959
		private static int _dontSupportReentrantFilterMessage;

		// Token: 0x04001360 RID: 4960
		private static int _doNotSupportSelectAllShortcutInMultilineTextBox;

		// Token: 0x04001361 RID: 4961
		private static int _doNotLoadLatestRichEditControl;

		// Token: 0x04001362 RID: 4962
		private static int _useLegacyContextMenuStripSourceControlValue;

		// Token: 0x04001363 RID: 4963
		private static int _useLegacyDomainUpDownScrolling;

		// Token: 0x04001364 RID: 4964
		private static int _allowUpdateChildControlIndexForTabControls;

		// Token: 0x04001365 RID: 4965
		private static int _useLegacyImages;

		// Token: 0x04001366 RID: 4966
		private static int _enableVisualStyleValidation;

		// Token: 0x04001367 RID: 4967
		private static int _enableLegacyDangerousClipboardDeserializationMode;

		// Token: 0x04001368 RID: 4968
		private static int _enableLegacyChineseIMEIndicator;

		// Token: 0x04001369 RID: 4969
		private static int _enableLegacyIMEFocusInComboBox;

		// Token: 0x0400136A RID: 4970
		private static int _disconnectUiaProvidersOnWmDestroy;

		// Token: 0x0400136B RID: 4971
		private static int _noClientNotifications;

		// Token: 0x0400136C RID: 4972
		private static int _freeControlsForRefCountedAccessibleObjectsInLevel5;
	}
}
