using System;
using System.Drawing;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	// Token: 0x020002CA RID: 714
	internal class LinkUtilities
	{
		// Token: 0x06002BB5 RID: 11189 RVA: 0x000C4A48 File Offset: 0x000C2C48
		private static Color GetIEColor(string name)
		{
			new RegistryPermission(PermissionState.Unrestricted).Assert();
			Color result;
			try
			{
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Settings");
				if (registryKey != null)
				{
					string text = (string)registryKey.GetValue(name);
					registryKey.Close();
					if (text != null)
					{
						string[] array = text.Split(new char[]
						{
							','
						});
						int[] array2 = new int[3];
						int num = Math.Min(array2.Length, array.Length);
						for (int i = 0; i < num; i++)
						{
							int.TryParse(array[i], out array2[i]);
						}
						return Color.FromArgb(array2[0], array2[1], array2[2]);
					}
				}
				if (string.Equals(name, "Anchor Color", StringComparison.OrdinalIgnoreCase))
				{
					result = Color.Blue;
				}
				else if (string.Equals(name, "Anchor Color Visited", StringComparison.OrdinalIgnoreCase))
				{
					result = Color.Purple;
				}
				else
				{
					string.Equals(name, "Anchor Color Hover", StringComparison.OrdinalIgnoreCase);
					result = Color.Red;
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06002BB6 RID: 11190 RVA: 0x000C4B40 File Offset: 0x000C2D40
		public static Color IELinkColor
		{
			get
			{
				if (LinkUtilities.ielinkColor.IsEmpty)
				{
					LinkUtilities.ielinkColor = LinkUtilities.GetIEColor("Anchor Color");
				}
				return LinkUtilities.ielinkColor;
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06002BB7 RID: 11191 RVA: 0x000C4B62 File Offset: 0x000C2D62
		public static Color IEActiveLinkColor
		{
			get
			{
				if (LinkUtilities.ieactiveLinkColor.IsEmpty)
				{
					LinkUtilities.ieactiveLinkColor = LinkUtilities.GetIEColor("Anchor Color Hover");
				}
				return LinkUtilities.ieactiveLinkColor;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06002BB8 RID: 11192 RVA: 0x000C4B84 File Offset: 0x000C2D84
		public static Color IEVisitedLinkColor
		{
			get
			{
				if (LinkUtilities.ievisitedLinkColor.IsEmpty)
				{
					LinkUtilities.ievisitedLinkColor = LinkUtilities.GetIEColor("Anchor Color Visited");
				}
				return LinkUtilities.ievisitedLinkColor;
			}
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x000C4BA8 File Offset: 0x000C2DA8
		public static Color GetVisitedLinkColor()
		{
			int red = (int)((SystemColors.Window.R + SystemColors.WindowText.R + 1) / 2);
			int g = (int)SystemColors.WindowText.G;
			int blue = (int)((SystemColors.Window.B + SystemColors.WindowText.B + 1) / 2);
			return Color.FromArgb(red, g, blue);
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x000C4C0C File Offset: 0x000C2E0C
		public static LinkBehavior GetIELinkBehavior()
		{
			new RegistryPermission(PermissionState.Unrestricted).Assert();
			try
			{
				RegistryKey registryKey = null;
				try
				{
					registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main");
				}
				catch (SecurityException)
				{
				}
				if (registryKey != null)
				{
					string text = (string)registryKey.GetValue("Anchor Underline");
					registryKey.Close();
					if (text != null && string.Compare(text, "no", true, CultureInfo.InvariantCulture) == 0)
					{
						return LinkBehavior.NeverUnderline;
					}
					if (text != null && string.Compare(text, "hover", true, CultureInfo.InvariantCulture) == 0)
					{
						return LinkBehavior.HoverUnderline;
					}
					return LinkBehavior.AlwaysUnderline;
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return LinkBehavior.AlwaysUnderline;
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x000C4CB4 File Offset: 0x000C2EB4
		public static void EnsureLinkFonts(Font baseFont, LinkBehavior link, ref Font linkFont, ref Font hoverLinkFont)
		{
			LinkUtilities.EnsureLinkFontsInternal(baseFont, link, ref linkFont, ref hoverLinkFont, false);
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x000C4CC0 File Offset: 0x000C2EC0
		internal static void EnsureLinkFontsInternal(Font baseFont, LinkBehavior link, ref Font linkFont, ref Font hoverLinkFont, bool isActive)
		{
			if (linkFont != null && hoverLinkFont != null)
			{
				return;
			}
			bool flag = true;
			bool flag2 = true;
			if (link == LinkBehavior.SystemDefault)
			{
				link = LinkUtilities.GetIELinkBehavior();
			}
			switch (link)
			{
			case LinkBehavior.AlwaysUnderline:
				flag = true;
				flag2 = true;
				break;
			case LinkBehavior.HoverUnderline:
				flag = false;
				flag2 = true;
				break;
			case LinkBehavior.NeverUnderline:
				flag = false;
				flag2 = false;
				break;
			}
			if (flag2 == flag)
			{
				FontStyle fontStyle = baseFont.Style;
				if (flag2)
				{
					fontStyle |= FontStyle.Underline;
				}
				else
				{
					fontStyle &= ~FontStyle.Underline;
				}
				if (AccessibilityImprovements.Level5)
				{
					if (isActive)
					{
						fontStyle |= FontStyle.Bold;
					}
					else
					{
						fontStyle &= ~FontStyle.Bold;
					}
				}
				hoverLinkFont = new Font(baseFont, fontStyle);
				linkFont = hoverLinkFont;
				return;
			}
			FontStyle fontStyle2 = baseFont.Style;
			if (flag2)
			{
				fontStyle2 |= FontStyle.Underline;
			}
			else
			{
				fontStyle2 &= ~FontStyle.Underline;
			}
			hoverLinkFont = new Font(baseFont, fontStyle2);
			FontStyle fontStyle3 = baseFont.Style;
			if (flag)
			{
				fontStyle3 |= FontStyle.Underline;
			}
			else
			{
				fontStyle3 &= ~FontStyle.Underline;
			}
			linkFont = new Font(baseFont, fontStyle3);
		}

		// Token: 0x04001252 RID: 4690
		private static Color ielinkColor = Color.Empty;

		// Token: 0x04001253 RID: 4691
		private static Color ieactiveLinkColor = Color.Empty;

		// Token: 0x04001254 RID: 4692
		private static Color ievisitedLinkColor = Color.Empty;

		// Token: 0x04001255 RID: 4693
		private const string IESettingsRegPath = "Software\\Microsoft\\Internet Explorer\\Settings";

		// Token: 0x04001256 RID: 4694
		public const string IEMainRegPath = "Software\\Microsoft\\Internet Explorer\\Main";

		// Token: 0x04001257 RID: 4695
		private const string IEAnchorColor = "Anchor Color";

		// Token: 0x04001258 RID: 4696
		private const string IEAnchorColorVisited = "Anchor Color Visited";

		// Token: 0x04001259 RID: 4697
		private const string IEAnchorColorHover = "Anchor Color Hover";
	}
}
