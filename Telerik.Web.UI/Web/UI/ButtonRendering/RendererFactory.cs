using System;
using System.Web.UI;
using Telerik.Web.UI.ButtonBase;
using Telerik.Web.UI.ButtonNS;
using Telerik.Web.UI.ButtonRendering.Classic;
using Telerik.Web.UI.ButtonRendering.Lightweight;

namespace Telerik.Web.UI.ButtonRendering
{
	// Token: 0x020000ED RID: 237
	internal class RendererFactory
	{
		// Token: 0x060009BD RID: 2493 RVA: 0x0002345C File Offset: 0x0002165C
		public static IRenderer GetRenderer(RadButton button)
		{
			bool flag = button.ButtonType == RadButtonType.ToggleButton;
			ButtonToggleType toggleType = button.ToggleType;
			bool flag2 = flag && toggleType == ButtonToggleType.CheckBox;
			bool flag3 = flag && toggleType == ButtonToggleType.Radio;
			bool flag4 = flag && toggleType != ButtonToggleType.None;
			RendererFactory.GetImageOptions(button);
			IRenderer result;
			if (button.ResolvedRenderMode == RenderMode.Lightweight)
			{
				if (button.IsTemplateInitialized)
				{
					ImageRenderingOptions imgOptions = button.HasImage ? RendererFactory.GetImageOptions(button) : new ImageRenderingOptions();
					result = new TemplateButtonRenderer(delegate(HtmlTextWriter writer)
					{
						button.RenderContentsBase(writer);
					}, RendererFactory.GetOptions(button), imgOptions);
				}
				else if (button.EnableSplitButton)
				{
					result = new SplitButtonRenderer(RendererFactory.GetOptions(button), RendererFactory.GetIconOptions(button));
				}
				else if (flag4 && (button.HasIconInState || button.HasImageInState))
				{
					result = new ToggleButtonRenderer(RendererFactory.GetOptions(button), RendererFactory.GetIconOptions(button));
				}
				else if (flag2)
				{
					result = new ToggleCheckBoxRenderer(RendererFactory.GetOptions(button));
				}
				else if (flag3)
				{
					result = new ToggleRadioButtonRenderer(RendererFactory.GetOptions(button));
				}
				else if (button.HasImage || button.HasIcon)
				{
					result = new ImageButtonRenderer(RendererFactory.GetOptions(button), RendererFactory.GetIconOptions(button), RendererFactory.GetImageOptions(button));
				}
				else
				{
					result = new StandardButtonRenderer(RendererFactory.GetOptions(button));
				}
			}
			else
			{
				result = new ClassicRenderer(button);
			}
			return result;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0002364E File Offset: 0x0002184E
		public static IRenderer GetRenderer(RadButtonBase btn)
		{
			return new StandardButtonRenderer(new ButtonRenderingOptions());
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00023670 File Offset: 0x00021870
		public static IRenderer GetRenderer(RadPushButton btn)
		{
			ButtonRenderingOptions options = RendererFactory.GetOptions(btn);
			if (btn.IsTemplateInitialized)
			{
				return new TemplateButtonRenderer(delegate(HtmlTextWriter writer)
				{
					btn.RenderContentsBase(writer);
				}, options);
			}
			if (btn.HasIcon)
			{
				return new ImageButtonRenderer(options, RendererFactory.GetIconOptions(btn), new ImageRenderingOptions());
			}
			return new StandardButtonRenderer(RendererFactory.GetOptions(btn));
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00023708 File Offset: 0x00021908
		public static IRenderer GetRenderer(RadLinkButton btn)
		{
			ButtonRenderingOptions options = RendererFactory.GetOptions(btn);
			if (btn.IsTemplateInitialized)
			{
				return new TemplateButtonRenderer(delegate(HtmlTextWriter writer)
				{
					btn.RenderContentsBase(writer);
				}, options);
			}
			if (btn.HasIcon)
			{
				return new ImageButtonRenderer(options, RendererFactory.GetIconOptions(btn), new ImageRenderingOptions());
			}
			return new StandardButtonRenderer(RendererFactory.GetOptions(btn));
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x000237A0 File Offset: 0x000219A0
		public static IRenderer GetRenderer(RadImageButton btn)
		{
			ButtonRenderingOptions options = RendererFactory.GetOptions(btn);
			ImageRenderingOptions imgOptions = btn.HasImage ? RendererFactory.GetImageOptions(btn) : new ImageRenderingOptions();
			if (btn.IsTemplateInitialized)
			{
				return new TemplateButtonRenderer(delegate(HtmlTextWriter writer)
				{
					btn.RenderContentsBase(writer);
				}, options, imgOptions);
			}
			if (btn.HasImage)
			{
				return new ImageButtonRenderer(options, new IconRenderingOptions(), imgOptions);
			}
			return new StandardButtonRenderer(RendererFactory.GetOptions(btn));
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00023838 File Offset: 0x00021A38
		public static IRenderer GetRenderer(RadToggleButton btn)
		{
			ButtonRenderingOptions options = RendererFactory.GetOptions(btn);
			if (btn.HasImageInState)
			{
				return new ToggleButtonRenderer(options, RendererFactory.GetIconOptions(btn));
			}
			if (btn.HasIconInState)
			{
				return new ImageButtonRenderer(options, RendererFactory.GetIconOptions(btn), new ImageRenderingOptions());
			}
			return new StandardButtonRenderer(options);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00023881 File Offset: 0x00021A81
		public static IRenderer GetRenderer(RadCheckBox button)
		{
			return new CheckBoxRenderer(RendererFactory.GetOptions(button));
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0002388E File Offset: 0x00021A8E
		public static IRenderer GetRenderer(RadSwitch button)
		{
			return new SwitchRenderer(RendererFactory.GetOptions(button), button.ToggleStates);
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x000238A1 File Offset: 0x00021AA1
		public static IRenderer GetRenderer(RadRadioButton button)
		{
			return new RadioButtonRenderer(RendererFactory.GetOptions(button));
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x000238B0 File Offset: 0x00021AB0
		private static ButtonRenderingOptions GetOptions(RadButton btn)
		{
			return new ButtonRenderingOptions
			{
				ButtonType = btn.ButtonType,
				Checked = new bool?(btn.Checked),
				DesignTimeStyleSheet = (btn.InDesignMode ? SkinRegistrar.GetDesignTimeStyleSheet(btn) : string.Empty),
				DisabledButtonCssClass = btn.DisabledButtonCssClass,
				EnableBrowserButtonStyle = btn.EnableBrowserButtonStyle,
				HasBackgroundImage = btn.HasBackgroundImage,
				HasImage = btn.HasImage,
				InDesignMode = btn.InDesignMode,
				IsButtonEnabled = btn.IsButtonEnabled,
				IsClientSubmit = btn.IsClientSubmit,
				IsTemplateInitialized = btn.IsTemplateInitialized,
				OriginalEnabled = btn.OriginalEnabled,
				ReadOnly = btn.ReadOnly,
				ReadOnlyCssClass = btn.ReadOnlyCssClass,
				Skin = btn.RuntimeSkin,
				SplitButtonCssClass = btn.SplitButtonCssClass,
				SplitButtonPosition = btn.SplitButtonPosition,
				UniqueID = btn.UniqueID,
				Text = btn.Text,
				ToggleStatesCount = btn.ToggleStates.Count,
				ToggleType = btn.ToggleType,
				Primary = btn.Primary,
				HasStateWithPrimaryIcon = btn.HasStateWithPrimaryIcon,
				HasStateWithSecondaryIcon = btn.HasStateWithSecondaryIcon
			};
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00023A00 File Offset: 0x00021C00
		private static ButtonRenderingOptions GetOptions(RadPushButton btn)
		{
			return new ButtonRenderingOptions
			{
				ButtonType = RadButtonType.StandardButton,
				DesignTimeStyleSheet = (btn.InDesignMode ? SkinRegistrar.GetDesignTimeStyleSheet(btn) : string.Empty),
				DisabledButtonCssClass = btn.DisabledCssClass,
				EnableBrowserButtonStyle = (btn.ResolvedRenderMode == RenderMode.Native),
				InDesignMode = btn.InDesignMode,
				IsButtonEnabled = btn.IsButtonEnabled,
				IsTemplateInitialized = btn.IsTemplateInitialized,
				OriginalEnabled = btn.OriginalEnabled,
				IsClientSubmit = btn.IsClientSubmit,
				Skin = btn.RuntimeSkin,
				UniqueID = btn.UniqueID,
				Text = btn.Text,
				Primary = btn.Primary
			};
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00023AC0 File Offset: 0x00021CC0
		private static ButtonRenderingOptions GetOptions(RadLinkButton btn)
		{
			return new ButtonRenderingOptions
			{
				ButtonType = RadButtonType.LinkButton,
				IsClientSubmit = true,
				DesignTimeStyleSheet = (btn.InDesignMode ? SkinRegistrar.GetDesignTimeStyleSheet(btn) : string.Empty),
				DisabledButtonCssClass = btn.DisabledCssClass,
				EnableBrowserButtonStyle = (btn.ResolvedRenderMode == RenderMode.Native),
				InDesignMode = btn.InDesignMode,
				IsButtonEnabled = btn.IsButtonEnabled,
				IsTemplateInitialized = btn.IsTemplateInitialized,
				OriginalEnabled = btn.OriginalEnabled,
				Skin = btn.RuntimeSkin,
				UniqueID = btn.UniqueID,
				Text = btn.Text,
				Primary = btn.Primary
			};
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00023B78 File Offset: 0x00021D78
		private static ButtonRenderingOptions GetOptions(RadImageButton btn)
		{
			return new ButtonRenderingOptions
			{
				ButtonType = RadButtonType.StandardButton,
				DesignTimeStyleSheet = (btn.InDesignMode ? SkinRegistrar.GetDesignTimeStyleSheet(btn) : string.Empty),
				DisabledButtonCssClass = btn.DisabledCssClass,
				EnableBrowserButtonStyle = (btn.ResolvedRenderMode == RenderMode.Native),
				IsClientSubmit = btn.IsClientSubmit,
				InDesignMode = btn.InDesignMode,
				IsButtonEnabled = btn.IsButtonEnabled,
				IsTemplateInitialized = btn.IsTemplateInitialized,
				OriginalEnabled = btn.OriginalEnabled,
				Skin = btn.RuntimeSkin,
				UniqueID = btn.UniqueID,
				Text = btn.Text,
				HasBackgroundImage = true,
				HasImage = btn.HasImage
			};
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00023C3C File Offset: 0x00021E3C
		private static ButtonRenderingOptions GetOptions(RadToggleButton btn)
		{
			ButtonRenderingOptions buttonRenderingOptions = new ButtonRenderingOptions
			{
				ButtonType = RadButtonType.ToggleButton,
				DesignTimeStyleSheet = (btn.InDesignMode ? SkinRegistrar.GetDesignTimeStyleSheet(btn) : string.Empty),
				DisabledButtonCssClass = btn.DisabledCssClass,
				IsClientSubmit = btn.IsClientSubmit,
				InDesignMode = btn.InDesignMode,
				IsButtonEnabled = btn.IsButtonEnabled,
				EnableBrowserButtonStyle = (btn.ResolvedRenderMode == RenderMode.Native),
				OriginalEnabled = btn.OriginalEnabled,
				Skin = btn.RuntimeSkin,
				UniqueID = btn.UniqueID,
				HasBackgroundImage = true,
				HasImage = btn.HasImageInState,
				ToggleType = ButtonToggleType.CustomToggle,
				ToggleStatesCount = btn.ToggleStates.Count,
				HasStateWithPrimaryIcon = btn.HasStateWithPrimaryIcon,
				HasStateWithSecondaryIcon = btn.HasStateWithSecondaryIcon,
				Text = btn.Text,
				Value = (string.IsNullOrEmpty(btn.Value) ? btn.Text : btn.Value)
			};
			ButtonToggleState selectedToggleState = btn.SelectedToggleState;
			if (selectedToggleState != null)
			{
				buttonRenderingOptions.Text = selectedToggleState.Text;
				buttonRenderingOptions.Value = (string.IsNullOrEmpty(selectedToggleState.Value) ? buttonRenderingOptions.Text : selectedToggleState.Value);
			}
			return buttonRenderingOptions;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00023D80 File Offset: 0x00021F80
		private static ButtonRenderingOptions GetOptions(RadCheckBox button)
		{
			return new ButtonRenderingOptions
			{
				ButtonType = RadButtonType.StandardButton,
				DesignTimeStyleSheet = (button.InDesignMode ? SkinRegistrar.GetDesignTimeStyleSheet(button) : string.Empty),
				DisabledButtonCssClass = button.DisabledCssClass,
				IsClientSubmit = button.IsClientSubmit,
				InDesignMode = button.InDesignMode,
				IsButtonEnabled = button.IsButtonEnabled,
				EnableBrowserButtonStyle = (button.ResolvedRenderMode == RenderMode.Native),
				OriginalEnabled = button.OriginalEnabled,
				Skin = button.RuntimeSkin,
				UniqueID = button.UniqueID,
				Text = button.Text,
				Checked = button.Checked
			};
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00023E34 File Offset: 0x00022034
		private static ButtonRenderingOptions GetOptions(RadSwitch button)
		{
			return new ButtonRenderingOptions
			{
				ButtonType = RadButtonType.StandardButton,
				DesignTimeStyleSheet = (button.InDesignMode ? SkinRegistrar.GetDesignTimeStyleSheet(button) : string.Empty),
				DisabledButtonCssClass = button.DisabledCssClass,
				IsClientSubmit = button.IsClientSubmit,
				InDesignMode = button.InDesignMode,
				IsButtonEnabled = button.IsButtonEnabled,
				EnableBrowserButtonStyle = true,
				OriginalEnabled = button.OriginalEnabled,
				Skin = button.RuntimeSkin,
				UniqueID = button.UniqueID,
				Text = button.Text,
				Checked = button.Checked
			};
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00023EE0 File Offset: 0x000220E0
		private static ButtonRenderingOptions GetOptions(RadRadioButton button)
		{
			return new ButtonRenderingOptions
			{
				ButtonType = RadButtonType.StandardButton,
				DesignTimeStyleSheet = (button.InDesignMode ? SkinRegistrar.GetDesignTimeStyleSheet(button) : string.Empty),
				DisabledButtonCssClass = button.DisabledCssClass,
				IsClientSubmit = button.IsClientSubmit,
				InDesignMode = button.InDesignMode,
				IsButtonEnabled = button.IsButtonEnabled,
				EnableBrowserButtonStyle = (button.ResolvedRenderMode == RenderMode.Native),
				OriginalEnabled = button.OriginalEnabled,
				Skin = button.RuntimeSkin,
				UniqueID = button.UniqueID,
				Text = button.Text,
				Checked = button.Checked
			};
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00023F94 File Offset: 0x00022194
		private static IconRenderingOptions GetIconOptions(RadButton btn)
		{
			RadButtonIcon icon = btn.Icon;
			return new IconRenderingOptions
			{
				PrimaryIconBottom = icon.PrimaryIconBottom,
				PrimaryIconCssClass = icon.PrimaryIconCssClass,
				PrimaryIconHeight = icon.PrimaryIconHeight,
				PrimaryIconLeft = icon.PrimaryIconLeft,
				PrimaryIconRight = icon.PrimaryIconRight,
				PrimaryIconTop = icon.PrimaryIconTop,
				PrimaryIconUrl = btn.ResolveUrl(icon.PrimaryIconUrl),
				PrimaryIconWidth = icon.PrimaryIconWidth,
				SecondaryIconBottom = icon.SecondaryIconBottom,
				SecondaryIconCssClass = icon.SecondaryIconCssClass,
				SecondaryIconHeight = icon.SecondaryIconHeight,
				SecondaryIconLeft = icon.SecondaryIconLeft,
				SecondaryIconRight = icon.SecondaryIconRight,
				SecondaryIconTop = icon.SecondaryIconTop,
				SecondaryIconUrl = btn.ResolveUrl(icon.SecondaryIconUrl),
				SecondaryIconWidth = icon.SecondaryIconWidth,
				ShowPrimaryIcon = icon.ShowPrimaryIcon,
				ShowSecondaryIcon = icon.ShowSecondaryIcon
			};
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00024094 File Offset: 0x00022294
		private static IconRenderingOptions GetIconOptions(RadPushButton btn)
		{
			ButtonIcon icon = btn.Icon;
			return new IconRenderingOptions
			{
				PrimaryIconCssClass = icon.CssClass,
				PrimaryIconHeight = icon.Height,
				PrimaryIconLeft = icon.Left,
				PrimaryIconTop = icon.Top,
				PrimaryIconUrl = btn.ResolveUrl(icon.Url),
				PrimaryIconWidth = icon.Width,
				ShowPrimaryIcon = icon.ShowIcon,
				ShowSecondaryIcon = false
			};
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00024110 File Offset: 0x00022310
		private static IconRenderingOptions GetIconOptions(RadLinkButton btn)
		{
			ButtonIcon icon = btn.Icon;
			return new IconRenderingOptions
			{
				PrimaryIconCssClass = icon.CssClass,
				PrimaryIconHeight = icon.Height,
				PrimaryIconLeft = icon.Left,
				PrimaryIconTop = icon.Top,
				PrimaryIconUrl = btn.ResolveUrl(icon.Url),
				PrimaryIconWidth = icon.Width,
				ShowPrimaryIcon = icon.ShowIcon,
				ShowSecondaryIcon = false
			};
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0002418C File Offset: 0x0002238C
		private static IconRenderingOptions GetIconOptions(RadToggleButton btn)
		{
			ButtonToggleState selectedToggleState = btn.SelectedToggleState;
			if (selectedToggleState != null)
			{
				return new IconRenderingOptions
				{
					PrimaryIconCssClass = selectedToggleState.Icon.CssClass,
					PrimaryIconHeight = selectedToggleState.Icon.Height,
					PrimaryIconLeft = selectedToggleState.Icon.Left,
					PrimaryIconTop = selectedToggleState.Icon.Top,
					PrimaryIconUrl = btn.ResolveUrl(selectedToggleState.Icon.Url),
					PrimaryIconWidth = selectedToggleState.Icon.Width,
					ShowPrimaryIcon = selectedToggleState.Icon.ShowIcon,
					ShowSecondaryIcon = false
				};
			}
			return new IconRenderingOptions();
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00024238 File Offset: 0x00022438
		private static ImageRenderingOptions GetImageOptions(RadImageButton btn)
		{
			ButtonImage image = btn.Image;
			return new ImageRenderingOptions
			{
				DisabledImageUrl = btn.ResolveUrl(image.DisabledUrl),
				ImageUrl = btn.ResolveUrl(image.Url),
				Sizing = image.Sizing
			};
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00024284 File Offset: 0x00022484
		private static ImageRenderingOptions GetImageOptions(RadButton btn)
		{
			RadButtonImage image = btn.Image;
			return new ImageRenderingOptions
			{
				DisabledImageUrl = btn.ResolveUrl(image.DisabledImageUrl),
				ImageUrl = btn.ResolveUrl(image.ImageUrl)
			};
		}
	}
}
