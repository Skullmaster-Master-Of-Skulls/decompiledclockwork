using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000E51 RID: 3665
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class RibbonBarKeyboardNavigationSettings : ObjectWithState
	{
		// Token: 0x06008B08 RID: 35592 RVA: 0x001FAA5D File Offset: 0x001F8C5D
		public RibbonBarKeyboardNavigationSettings(StateBag OwnerStateBag) : base("KeyboradNavigationSettings", OwnerStateBag)
		{
		}

		// Token: 0x17002BEF RID: 11247
		// (get) Token: 0x06008B09 RID: 35593 RVA: 0x001FAA7A File Offset: 0x001F8C7A
		// (set) Token: 0x06008B0A RID: 35594 RVA: 0x001FAA82 File Offset: 0x001F8C82
		[Category("Client")]
		[Description("his property auto enables key hints on page load.")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public virtual bool Activated { get; set; }

		// Token: 0x17002BF0 RID: 11248
		// (get) Token: 0x06008B0B RID: 35595 RVA: 0x001FAA8B File Offset: 0x001F8C8B
		// (set) Token: 0x06008B0C RID: 35596 RVA: 0x001FAA93 File Offset: 0x001F8C93
		[DefaultValue(typeof(RibbonBarKeyboardNavigationSettings.RibbonBarCommandKey), "AltKey")]
		[Category("Client")]
		[Description("This property sets the key that is used to focus RadRibbonBar. It is always used in combination with FocusKey.")]
		[NotifyParentProperty(true)]
		public virtual RibbonBarKeyboardNavigationSettings.RibbonBarCommandKey CommandKey
		{
			get
			{
				return this.commandKey;
			}
			set
			{
				this.commandKey = value;
			}
		}

		// Token: 0x17002BF1 RID: 11249
		// (get) Token: 0x06008B0D RID: 35597 RVA: 0x001FAA9C File Offset: 0x001F8C9C
		// (set) Token: 0x06008B0E RID: 35598 RVA: 0x001FAAA4 File Offset: 0x001F8CA4
		[Description("This property sets the key that is used to focus RadRibbonBar. It is always used in combination with CommandKey.")]
		[Category("Client")]
		[DefaultValue(typeof(RibbonBarKeyboardNavigationSettings.RibbonBarFocusKey), "R")]
		[NotifyParentProperty(true)]
		public virtual RibbonBarKeyboardNavigationSettings.RibbonBarFocusKey FocusKey
		{
			get
			{
				return this.focusKey;
			}
			set
			{
				this.focusKey = value;
			}
		}

		// Token: 0x040026DF RID: 9951
		private RibbonBarKeyboardNavigationSettings.RibbonBarCommandKey commandKey = RibbonBarKeyboardNavigationSettings.RibbonBarCommandKey.Alt;

		// Token: 0x040026E0 RID: 9952
		private RibbonBarKeyboardNavigationSettings.RibbonBarFocusKey focusKey = RibbonBarKeyboardNavigationSettings.RibbonBarFocusKey.R;

		// Token: 0x02000E52 RID: 3666
		[Flags]
		public enum RibbonBarFocusKey
		{
			// Token: 0x040026E3 RID: 9955
			A = 65,
			// Token: 0x040026E4 RID: 9956
			B = 66,
			// Token: 0x040026E5 RID: 9957
			C = 67,
			// Token: 0x040026E6 RID: 9958
			D = 68,
			// Token: 0x040026E7 RID: 9959
			D0 = 48,
			// Token: 0x040026E8 RID: 9960
			D1 = 49,
			// Token: 0x040026E9 RID: 9961
			D2 = 50,
			// Token: 0x040026EA RID: 9962
			D3 = 51,
			// Token: 0x040026EB RID: 9963
			D4 = 52,
			// Token: 0x040026EC RID: 9964
			D5 = 53,
			// Token: 0x040026ED RID: 9965
			D6 = 54,
			// Token: 0x040026EE RID: 9966
			D7 = 55,
			// Token: 0x040026EF RID: 9967
			D8 = 56,
			// Token: 0x040026F0 RID: 9968
			D9 = 57,
			// Token: 0x040026F1 RID: 9969
			E = 69,
			// Token: 0x040026F2 RID: 9970
			F = 70,
			// Token: 0x040026F3 RID: 9971
			G = 71,
			// Token: 0x040026F4 RID: 9972
			H = 72,
			// Token: 0x040026F5 RID: 9973
			I = 73,
			// Token: 0x040026F6 RID: 9974
			J = 74,
			// Token: 0x040026F7 RID: 9975
			K = 75,
			// Token: 0x040026F8 RID: 9976
			L = 76,
			// Token: 0x040026F9 RID: 9977
			M = 77,
			// Token: 0x040026FA RID: 9978
			N = 78,
			// Token: 0x040026FB RID: 9979
			O = 79,
			// Token: 0x040026FC RID: 9980
			P = 80,
			// Token: 0x040026FD RID: 9981
			Q = 81,
			// Token: 0x040026FE RID: 9982
			R = 82,
			// Token: 0x040026FF RID: 9983
			S = 83,
			// Token: 0x04002700 RID: 9984
			T = 84,
			// Token: 0x04002701 RID: 9985
			U = 85,
			// Token: 0x04002702 RID: 9986
			V = 86,
			// Token: 0x04002703 RID: 9987
			W = 87,
			// Token: 0x04002704 RID: 9988
			X = 88,
			// Token: 0x04002705 RID: 9989
			Y = 89,
			// Token: 0x04002706 RID: 9990
			Z = 90
		}

		// Token: 0x02000E53 RID: 3667
		[Flags]
		public enum RibbonBarCommandKey
		{
			// Token: 0x04002708 RID: 9992
			Alt = 1,
			// Token: 0x04002709 RID: 9993
			Ctrl = 2,
			// Token: 0x0400270A RID: 9994
			Shift = 3,
			// Token: 0x0400270B RID: 9995
			AltShift = 4,
			// Token: 0x0400270C RID: 9996
			AltCtrl = 5,
			// Token: 0x0400270D RID: 9997
			CtrlShift = 6
		}
	}
}
