using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000153 RID: 339
	[DefaultProperty("Color")]
	[SRDescription("DescriptionColorDialog")]
	public class ColorDialog : CommonDialog
	{
		// Token: 0x06000D83 RID: 3459 RVA: 0x0002709F File Offset: 0x0002529F
		public ColorDialog()
		{
			this.customColors = new int[16];
			this.Reset();
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x000270BA File Offset: 0x000252BA
		// (set) Token: 0x06000D85 RID: 3461 RVA: 0x000270C6 File Offset: 0x000252C6
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("CDallowFullOpenDescr")]
		public virtual bool AllowFullOpen
		{
			get
			{
				return !this.GetOption(4);
			}
			set
			{
				this.SetOption(4, !value);
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x000270D3 File Offset: 0x000252D3
		// (set) Token: 0x06000D87 RID: 3463 RVA: 0x000270E0 File Offset: 0x000252E0
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("CDanyColorDescr")]
		public virtual bool AnyColor
		{
			get
			{
				return this.GetOption(256);
			}
			set
			{
				this.SetOption(256, value);
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x000270EE File Offset: 0x000252EE
		// (set) Token: 0x06000D89 RID: 3465 RVA: 0x000270F6 File Offset: 0x000252F6
		[SRCategory("CatData")]
		[SRDescription("CDcolorDescr")]
		public Color Color
		{
			get
			{
				return this.color;
			}
			set
			{
				if (!value.IsEmpty)
				{
					this.color = value;
					return;
				}
				this.color = Color.Black;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x00027114 File Offset: 0x00025314
		// (set) Token: 0x06000D8B RID: 3467 RVA: 0x00027128 File Offset: 0x00025328
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("CDcustomColorsDescr")]
		public int[] CustomColors
		{
			get
			{
				return (int[])this.customColors.Clone();
			}
			set
			{
				int num = (value == null) ? 0 : Math.Min(value.Length, 16);
				if (num > 0)
				{
					Array.Copy(value, 0, this.customColors, 0, num);
				}
				for (int i = num; i < 16; i++)
				{
					this.customColors[i] = 16777215;
				}
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x00027173 File Offset: 0x00025373
		// (set) Token: 0x06000D8D RID: 3469 RVA: 0x0002717C File Offset: 0x0002537C
		[SRCategory("CatAppearance")]
		[DefaultValue(false)]
		[SRDescription("CDfullOpenDescr")]
		public virtual bool FullOpen
		{
			get
			{
				return this.GetOption(2);
			}
			set
			{
				this.SetOption(2, value);
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x00027186 File Offset: 0x00025386
		protected virtual IntPtr Instance
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return UnsafeNativeMethods.GetModuleHandle(null);
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x0002718E File Offset: 0x0002538E
		protected virtual int Options
		{
			get
			{
				return this.options;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x00027196 File Offset: 0x00025396
		// (set) Token: 0x06000D91 RID: 3473 RVA: 0x0002719F File Offset: 0x0002539F
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("CDshowHelpDescr")]
		public virtual bool ShowHelp
		{
			get
			{
				return this.GetOption(8);
			}
			set
			{
				this.SetOption(8, value);
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x000271A9 File Offset: 0x000253A9
		// (set) Token: 0x06000D93 RID: 3475 RVA: 0x000271B6 File Offset: 0x000253B6
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("CDsolidColorOnlyDescr")]
		public virtual bool SolidColorOnly
		{
			get
			{
				return this.GetOption(128);
			}
			set
			{
				this.SetOption(128, value);
			}
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x000271C4 File Offset: 0x000253C4
		private bool GetOption(int option)
		{
			return (this.options & option) != 0;
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x000271D1 File Offset: 0x000253D1
		public override void Reset()
		{
			this.options = 0;
			this.color = Color.Black;
			this.CustomColors = null;
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x000271EC File Offset: 0x000253EC
		private void ResetColor()
		{
			this.Color = Color.Black;
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x000271FC File Offset: 0x000253FC
		protected override bool RunDialog(IntPtr hwndOwner)
		{
			NativeMethods.WndProc lpfnHook = new NativeMethods.WndProc(this.HookProc);
			NativeMethods.CHOOSECOLOR choosecolor = new NativeMethods.CHOOSECOLOR();
			IntPtr intPtr = Marshal.AllocCoTaskMem(64);
			bool result;
			try
			{
				Marshal.Copy(this.customColors, 0, intPtr, 16);
				choosecolor.hwndOwner = hwndOwner;
				choosecolor.hInstance = this.Instance;
				choosecolor.rgbResult = ColorTranslator.ToWin32(this.color);
				choosecolor.lpCustColors = intPtr;
				int num = this.Options | 17;
				if (!this.AllowFullOpen)
				{
					num &= -3;
				}
				choosecolor.Flags = num;
				choosecolor.lpfnHook = lpfnHook;
				if (!SafeNativeMethods.ChooseColor(choosecolor))
				{
					result = false;
				}
				else
				{
					if (choosecolor.rgbResult != ColorTranslator.ToWin32(this.color))
					{
						this.color = ColorTranslator.FromOle(choosecolor.rgbResult);
					}
					Marshal.Copy(intPtr, this.customColors, 0, 16);
					result = true;
				}
			}
			finally
			{
				Marshal.FreeCoTaskMem(intPtr);
			}
			return result;
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x000272E4 File Offset: 0x000254E4
		private void SetOption(int option, bool value)
		{
			if (value)
			{
				this.options |= option;
				return;
			}
			this.options &= ~option;
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00027308 File Offset: 0x00025508
		private bool ShouldSerializeColor()
		{
			return !this.Color.Equals(Color.Black);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00027338 File Offset: 0x00025538
		public override string ToString()
		{
			string str = base.ToString();
			return str + ",  Color: " + this.Color.ToString();
		}

		// Token: 0x04000797 RID: 1943
		private int options;

		// Token: 0x04000798 RID: 1944
		private int[] customColors;

		// Token: 0x04000799 RID: 1945
		private Color color;
	}
}
