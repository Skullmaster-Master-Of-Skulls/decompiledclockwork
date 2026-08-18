using System;
using System.Security.Permissions;
using System.Windows.Forms;

namespace System.Web.UI.Design.Util
{
	// Token: 0x0200015F RID: 351
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed class ColorComboBox : ComboBox
	{
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x00050F88 File Offset: 0x0004F188
		// (set) Token: 0x06000C5D RID: 3165 RVA: 0x00050FB4 File Offset: 0x0004F1B4
		public string Color
		{
			get
			{
				int selectedIndex = this.SelectedIndex;
				if (selectedIndex != -1)
				{
					return ColorComboBox.COLOR_VALUES[selectedIndex];
				}
				return this.Text.Trim();
			}
			set
			{
				this.SelectedIndex = -1;
				this.Text = string.Empty;
				if (value == null)
				{
					return;
				}
				string text = value.Trim();
				if (text.Length != 0)
				{
					for (int i = 0; i < ColorComboBox.COLOR_VALUES.Length; i++)
					{
						if (string.Compare(ColorComboBox.COLOR_VALUES[i], text, StringComparison.OrdinalIgnoreCase) == 0)
						{
							text = ColorComboBox.COLOR_VALUES[i];
							break;
						}
					}
					this.Text = text;
				}
			}
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0005101C File Offset: 0x0004F21C
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (!base.DesignMode && !base.RecreatingHandle)
			{
				base.Items.Clear();
				ComboBox.ObjectCollection items = base.Items;
				object[] color_VALUES = ColorComboBox.COLOR_VALUES;
				items.AddRange(color_VALUES);
			}
		}

		// Token: 0x040007A0 RID: 1952
		private static readonly string[] COLOR_VALUES = new string[]
		{
			"Aqua",
			"Black",
			"Blue",
			"Fuchsia",
			"Gray",
			"Green",
			"Lime",
			"Maroon",
			"Navy",
			"Olive",
			"Purple",
			"Red",
			"Silver",
			"Teal",
			"White",
			"Yellow"
		};
	}
}
