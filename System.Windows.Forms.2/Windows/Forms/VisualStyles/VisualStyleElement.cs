using System;

namespace System.Windows.Forms.VisualStyles
{
	// Token: 0x02000454 RID: 1108
	public class VisualStyleElement
	{
		// Token: 0x06004D6C RID: 19820 RVA: 0x0013FE75 File Offset: 0x0013E075
		private VisualStyleElement(string className, int part, int state)
		{
			this.className = className;
			this.part = part;
			this.state = state;
		}

		// Token: 0x06004D6D RID: 19821 RVA: 0x0013FE92 File Offset: 0x0013E092
		public static VisualStyleElement CreateElement(string className, int part, int state)
		{
			return new VisualStyleElement(className, part, state);
		}

		// Token: 0x170012FC RID: 4860
		// (get) Token: 0x06004D6E RID: 19822 RVA: 0x0013FE9C File Offset: 0x0013E09C
		public string ClassName
		{
			get
			{
				return this.className;
			}
		}

		// Token: 0x170012FD RID: 4861
		// (get) Token: 0x06004D6F RID: 19823 RVA: 0x0013FEA4 File Offset: 0x0013E0A4
		public int Part
		{
			get
			{
				return this.part;
			}
		}

		// Token: 0x170012FE RID: 4862
		// (get) Token: 0x06004D70 RID: 19824 RVA: 0x0013FEAC File Offset: 0x0013E0AC
		public int State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x0400325A RID: 12890
		internal static readonly int Count = 25;

		// Token: 0x0400325B RID: 12891
		private string className;

		// Token: 0x0400325C RID: 12892
		private int part;

		// Token: 0x0400325D RID: 12893
		private int state;

		// Token: 0x02000834 RID: 2100
		public static class Button
		{
			// Token: 0x04004365 RID: 17253
			private static readonly string className = "BUTTON";

			// Token: 0x020008D2 RID: 2258
			public static class PushButton
			{
				// Token: 0x1700193F RID: 6463
				// (get) Token: 0x0600732A RID: 29482 RVA: 0x001A565B File Offset: 0x001A385B
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Button.PushButton.normal == null)
						{
							VisualStyleElement.Button.PushButton.normal = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.PushButton.part, 1);
						}
						return VisualStyleElement.Button.PushButton.normal;
					}
				}

				// Token: 0x17001940 RID: 6464
				// (get) Token: 0x0600732B RID: 29483 RVA: 0x001A567E File Offset: 0x001A387E
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Button.PushButton.hot == null)
						{
							VisualStyleElement.Button.PushButton.hot = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.PushButton.part, 2);
						}
						return VisualStyleElement.Button.PushButton.hot;
					}
				}

				// Token: 0x17001941 RID: 6465
				// (get) Token: 0x0600732C RID: 29484 RVA: 0x001A56A1 File Offset: 0x001A38A1
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Button.PushButton.pressed == null)
						{
							VisualStyleElement.Button.PushButton.pressed = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.PushButton.part, 3);
						}
						return VisualStyleElement.Button.PushButton.pressed;
					}
				}

				// Token: 0x17001942 RID: 6466
				// (get) Token: 0x0600732D RID: 29485 RVA: 0x001A56C4 File Offset: 0x001A38C4
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Button.PushButton.disabled == null)
						{
							VisualStyleElement.Button.PushButton.disabled = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.PushButton.part, 4);
						}
						return VisualStyleElement.Button.PushButton.disabled;
					}
				}

				// Token: 0x17001943 RID: 6467
				// (get) Token: 0x0600732E RID: 29486 RVA: 0x001A56E7 File Offset: 0x001A38E7
				public static VisualStyleElement Default
				{
					get
					{
						if (VisualStyleElement.Button.PushButton._default == null)
						{
							VisualStyleElement.Button.PushButton._default = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.PushButton.part, 5);
						}
						return VisualStyleElement.Button.PushButton._default;
					}
				}

				// Token: 0x04004566 RID: 17766
				private static readonly int part = 1;

				// Token: 0x04004567 RID: 17767
				private static VisualStyleElement normal;

				// Token: 0x04004568 RID: 17768
				private static VisualStyleElement hot;

				// Token: 0x04004569 RID: 17769
				private static VisualStyleElement pressed;

				// Token: 0x0400456A RID: 17770
				private static VisualStyleElement disabled;

				// Token: 0x0400456B RID: 17771
				private static VisualStyleElement _default;
			}

			// Token: 0x020008D3 RID: 2259
			public static class RadioButton
			{
				// Token: 0x17001944 RID: 6468
				// (get) Token: 0x06007330 RID: 29488 RVA: 0x001A5712 File Offset: 0x001A3912
				public static VisualStyleElement UncheckedNormal
				{
					get
					{
						if (VisualStyleElement.Button.RadioButton.uncheckednormal == null)
						{
							VisualStyleElement.Button.RadioButton.uncheckednormal = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.RadioButton.part, 1);
						}
						return VisualStyleElement.Button.RadioButton.uncheckednormal;
					}
				}

				// Token: 0x17001945 RID: 6469
				// (get) Token: 0x06007331 RID: 29489 RVA: 0x001A5735 File Offset: 0x001A3935
				public static VisualStyleElement UncheckedHot
				{
					get
					{
						if (VisualStyleElement.Button.RadioButton.uncheckedhot == null)
						{
							VisualStyleElement.Button.RadioButton.uncheckedhot = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.RadioButton.part, 2);
						}
						return VisualStyleElement.Button.RadioButton.uncheckedhot;
					}
				}

				// Token: 0x17001946 RID: 6470
				// (get) Token: 0x06007332 RID: 29490 RVA: 0x001A5758 File Offset: 0x001A3958
				public static VisualStyleElement UncheckedPressed
				{
					get
					{
						if (VisualStyleElement.Button.RadioButton.uncheckedpressed == null)
						{
							VisualStyleElement.Button.RadioButton.uncheckedpressed = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.RadioButton.part, 3);
						}
						return VisualStyleElement.Button.RadioButton.uncheckedpressed;
					}
				}

				// Token: 0x17001947 RID: 6471
				// (get) Token: 0x06007333 RID: 29491 RVA: 0x001A577B File Offset: 0x001A397B
				public static VisualStyleElement UncheckedDisabled
				{
					get
					{
						if (VisualStyleElement.Button.RadioButton.uncheckeddisabled == null)
						{
							VisualStyleElement.Button.RadioButton.uncheckeddisabled = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.RadioButton.part, 4);
						}
						return VisualStyleElement.Button.RadioButton.uncheckeddisabled;
					}
				}

				// Token: 0x17001948 RID: 6472
				// (get) Token: 0x06007334 RID: 29492 RVA: 0x001A579E File Offset: 0x001A399E
				public static VisualStyleElement CheckedNormal
				{
					get
					{
						if (VisualStyleElement.Button.RadioButton.checkednormal == null)
						{
							VisualStyleElement.Button.RadioButton.checkednormal = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.RadioButton.part, 5);
						}
						return VisualStyleElement.Button.RadioButton.checkednormal;
					}
				}

				// Token: 0x17001949 RID: 6473
				// (get) Token: 0x06007335 RID: 29493 RVA: 0x001A57C1 File Offset: 0x001A39C1
				public static VisualStyleElement CheckedHot
				{
					get
					{
						if (VisualStyleElement.Button.RadioButton.checkedhot == null)
						{
							VisualStyleElement.Button.RadioButton.checkedhot = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.RadioButton.part, 6);
						}
						return VisualStyleElement.Button.RadioButton.checkedhot;
					}
				}

				// Token: 0x1700194A RID: 6474
				// (get) Token: 0x06007336 RID: 29494 RVA: 0x001A57E4 File Offset: 0x001A39E4
				public static VisualStyleElement CheckedPressed
				{
					get
					{
						if (VisualStyleElement.Button.RadioButton.checkedpressed == null)
						{
							VisualStyleElement.Button.RadioButton.checkedpressed = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.RadioButton.part, 7);
						}
						return VisualStyleElement.Button.RadioButton.checkedpressed;
					}
				}

				// Token: 0x1700194B RID: 6475
				// (get) Token: 0x06007337 RID: 29495 RVA: 0x001A5807 File Offset: 0x001A3A07
				public static VisualStyleElement CheckedDisabled
				{
					get
					{
						if (VisualStyleElement.Button.RadioButton.checkeddisabled == null)
						{
							VisualStyleElement.Button.RadioButton.checkeddisabled = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.RadioButton.part, 8);
						}
						return VisualStyleElement.Button.RadioButton.checkeddisabled;
					}
				}

				// Token: 0x0400456C RID: 17772
				private static readonly int part = 2;

				// Token: 0x0400456D RID: 17773
				internal static readonly int HighContrastDisabledPart = 8;

				// Token: 0x0400456E RID: 17774
				private static VisualStyleElement uncheckednormal;

				// Token: 0x0400456F RID: 17775
				private static VisualStyleElement uncheckedhot;

				// Token: 0x04004570 RID: 17776
				private static VisualStyleElement uncheckedpressed;

				// Token: 0x04004571 RID: 17777
				private static VisualStyleElement uncheckeddisabled;

				// Token: 0x04004572 RID: 17778
				private static VisualStyleElement checkednormal;

				// Token: 0x04004573 RID: 17779
				private static VisualStyleElement checkedhot;

				// Token: 0x04004574 RID: 17780
				private static VisualStyleElement checkedpressed;

				// Token: 0x04004575 RID: 17781
				private static VisualStyleElement checkeddisabled;
			}

			// Token: 0x020008D4 RID: 2260
			public static class CheckBox
			{
				// Token: 0x1700194C RID: 6476
				// (get) Token: 0x06007339 RID: 29497 RVA: 0x001A5838 File Offset: 0x001A3A38
				public static VisualStyleElement UncheckedNormal
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.uncheckednormal == null)
						{
							VisualStyleElement.Button.CheckBox.uncheckednormal = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 1);
						}
						return VisualStyleElement.Button.CheckBox.uncheckednormal;
					}
				}

				// Token: 0x1700194D RID: 6477
				// (get) Token: 0x0600733A RID: 29498 RVA: 0x001A585B File Offset: 0x001A3A5B
				public static VisualStyleElement UncheckedHot
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.uncheckedhot == null)
						{
							VisualStyleElement.Button.CheckBox.uncheckedhot = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 2);
						}
						return VisualStyleElement.Button.CheckBox.uncheckedhot;
					}
				}

				// Token: 0x1700194E RID: 6478
				// (get) Token: 0x0600733B RID: 29499 RVA: 0x001A587E File Offset: 0x001A3A7E
				public static VisualStyleElement UncheckedPressed
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.uncheckedpressed == null)
						{
							VisualStyleElement.Button.CheckBox.uncheckedpressed = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 3);
						}
						return VisualStyleElement.Button.CheckBox.uncheckedpressed;
					}
				}

				// Token: 0x1700194F RID: 6479
				// (get) Token: 0x0600733C RID: 29500 RVA: 0x001A58A1 File Offset: 0x001A3AA1
				public static VisualStyleElement UncheckedDisabled
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.uncheckeddisabled == null)
						{
							VisualStyleElement.Button.CheckBox.uncheckeddisabled = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 4);
						}
						return VisualStyleElement.Button.CheckBox.uncheckeddisabled;
					}
				}

				// Token: 0x17001950 RID: 6480
				// (get) Token: 0x0600733D RID: 29501 RVA: 0x001A58C4 File Offset: 0x001A3AC4
				public static VisualStyleElement CheckedNormal
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.checkednormal == null)
						{
							VisualStyleElement.Button.CheckBox.checkednormal = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 5);
						}
						return VisualStyleElement.Button.CheckBox.checkednormal;
					}
				}

				// Token: 0x17001951 RID: 6481
				// (get) Token: 0x0600733E RID: 29502 RVA: 0x001A58E7 File Offset: 0x001A3AE7
				public static VisualStyleElement CheckedHot
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.checkedhot == null)
						{
							VisualStyleElement.Button.CheckBox.checkedhot = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 6);
						}
						return VisualStyleElement.Button.CheckBox.checkedhot;
					}
				}

				// Token: 0x17001952 RID: 6482
				// (get) Token: 0x0600733F RID: 29503 RVA: 0x001A590A File Offset: 0x001A3B0A
				public static VisualStyleElement CheckedPressed
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.checkedpressed == null)
						{
							VisualStyleElement.Button.CheckBox.checkedpressed = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 7);
						}
						return VisualStyleElement.Button.CheckBox.checkedpressed;
					}
				}

				// Token: 0x17001953 RID: 6483
				// (get) Token: 0x06007340 RID: 29504 RVA: 0x001A592D File Offset: 0x001A3B2D
				public static VisualStyleElement CheckedDisabled
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.checkeddisabled == null)
						{
							VisualStyleElement.Button.CheckBox.checkeddisabled = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 8);
						}
						return VisualStyleElement.Button.CheckBox.checkeddisabled;
					}
				}

				// Token: 0x17001954 RID: 6484
				// (get) Token: 0x06007341 RID: 29505 RVA: 0x001A5950 File Offset: 0x001A3B50
				public static VisualStyleElement MixedNormal
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.mixednormal == null)
						{
							VisualStyleElement.Button.CheckBox.mixednormal = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 9);
						}
						return VisualStyleElement.Button.CheckBox.mixednormal;
					}
				}

				// Token: 0x17001955 RID: 6485
				// (get) Token: 0x06007342 RID: 29506 RVA: 0x001A5974 File Offset: 0x001A3B74
				public static VisualStyleElement MixedHot
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.mixedhot == null)
						{
							VisualStyleElement.Button.CheckBox.mixedhot = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 10);
						}
						return VisualStyleElement.Button.CheckBox.mixedhot;
					}
				}

				// Token: 0x17001956 RID: 6486
				// (get) Token: 0x06007343 RID: 29507 RVA: 0x001A5998 File Offset: 0x001A3B98
				public static VisualStyleElement MixedPressed
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.mixedpressed == null)
						{
							VisualStyleElement.Button.CheckBox.mixedpressed = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 11);
						}
						return VisualStyleElement.Button.CheckBox.mixedpressed;
					}
				}

				// Token: 0x17001957 RID: 6487
				// (get) Token: 0x06007344 RID: 29508 RVA: 0x001A59BC File Offset: 0x001A3BBC
				public static VisualStyleElement MixedDisabled
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.mixeddisabled == null)
						{
							VisualStyleElement.Button.CheckBox.mixeddisabled = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 12);
						}
						return VisualStyleElement.Button.CheckBox.mixeddisabled;
					}
				}

				// Token: 0x04004576 RID: 17782
				private static readonly int part = 3;

				// Token: 0x04004577 RID: 17783
				internal static readonly int HighContrastDisabledPart = 9;

				// Token: 0x04004578 RID: 17784
				private static VisualStyleElement uncheckednormal;

				// Token: 0x04004579 RID: 17785
				private static VisualStyleElement uncheckedhot;

				// Token: 0x0400457A RID: 17786
				private static VisualStyleElement uncheckedpressed;

				// Token: 0x0400457B RID: 17787
				private static VisualStyleElement uncheckeddisabled;

				// Token: 0x0400457C RID: 17788
				private static VisualStyleElement checkednormal;

				// Token: 0x0400457D RID: 17789
				private static VisualStyleElement checkedhot;

				// Token: 0x0400457E RID: 17790
				private static VisualStyleElement checkedpressed;

				// Token: 0x0400457F RID: 17791
				private static VisualStyleElement checkeddisabled;

				// Token: 0x04004580 RID: 17792
				private static VisualStyleElement mixednormal;

				// Token: 0x04004581 RID: 17793
				private static VisualStyleElement mixedhot;

				// Token: 0x04004582 RID: 17794
				private static VisualStyleElement mixedpressed;

				// Token: 0x04004583 RID: 17795
				private static VisualStyleElement mixeddisabled;
			}

			// Token: 0x020008D5 RID: 2261
			public static class GroupBox
			{
				// Token: 0x17001958 RID: 6488
				// (get) Token: 0x06007346 RID: 29510 RVA: 0x001A59EF File Offset: 0x001A3BEF
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Button.GroupBox.normal == null)
						{
							VisualStyleElement.Button.GroupBox.normal = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.GroupBox.part, 1);
						}
						return VisualStyleElement.Button.GroupBox.normal;
					}
				}

				// Token: 0x17001959 RID: 6489
				// (get) Token: 0x06007347 RID: 29511 RVA: 0x001A5A12 File Offset: 0x001A3C12
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Button.GroupBox.disabled == null)
						{
							VisualStyleElement.Button.GroupBox.disabled = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.GroupBox.part, 2);
						}
						return VisualStyleElement.Button.GroupBox.disabled;
					}
				}

				// Token: 0x04004584 RID: 17796
				private static readonly int part = 4;

				// Token: 0x04004585 RID: 17797
				internal static readonly int HighContrastDisabledPart = 10;

				// Token: 0x04004586 RID: 17798
				private static VisualStyleElement normal;

				// Token: 0x04004587 RID: 17799
				private static VisualStyleElement disabled;
			}

			// Token: 0x020008D6 RID: 2262
			public static class UserButton
			{
				// Token: 0x1700195A RID: 6490
				// (get) Token: 0x06007349 RID: 29513 RVA: 0x001A5A44 File Offset: 0x001A3C44
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Button.UserButton.normal == null)
						{
							VisualStyleElement.Button.UserButton.normal = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.UserButton.part, 0);
						}
						return VisualStyleElement.Button.UserButton.normal;
					}
				}

				// Token: 0x04004588 RID: 17800
				private static readonly int part = 5;

				// Token: 0x04004589 RID: 17801
				private static VisualStyleElement normal;
			}
		}

		// Token: 0x02000835 RID: 2101
		public static class ComboBox
		{
			// Token: 0x04004366 RID: 17254
			private static readonly string className = "COMBOBOX";

			// Token: 0x020008D7 RID: 2263
			public static class DropDownButton
			{
				// Token: 0x1700195B RID: 6491
				// (get) Token: 0x0600734B RID: 29515 RVA: 0x001A5A6F File Offset: 0x001A3C6F
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ComboBox.DropDownButton.normal == null)
						{
							VisualStyleElement.ComboBox.DropDownButton.normal = new VisualStyleElement(VisualStyleElement.ComboBox.className, VisualStyleElement.ComboBox.DropDownButton.part, 1);
						}
						return VisualStyleElement.ComboBox.DropDownButton.normal;
					}
				}

				// Token: 0x1700195C RID: 6492
				// (get) Token: 0x0600734C RID: 29516 RVA: 0x001A5A92 File Offset: 0x001A3C92
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ComboBox.DropDownButton.hot == null)
						{
							VisualStyleElement.ComboBox.DropDownButton.hot = new VisualStyleElement(VisualStyleElement.ComboBox.className, VisualStyleElement.ComboBox.DropDownButton.part, 2);
						}
						return VisualStyleElement.ComboBox.DropDownButton.hot;
					}
				}

				// Token: 0x1700195D RID: 6493
				// (get) Token: 0x0600734D RID: 29517 RVA: 0x001A5AB5 File Offset: 0x001A3CB5
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ComboBox.DropDownButton.pressed == null)
						{
							VisualStyleElement.ComboBox.DropDownButton.pressed = new VisualStyleElement(VisualStyleElement.ComboBox.className, VisualStyleElement.ComboBox.DropDownButton.part, 3);
						}
						return VisualStyleElement.ComboBox.DropDownButton.pressed;
					}
				}

				// Token: 0x1700195E RID: 6494
				// (get) Token: 0x0600734E RID: 29518 RVA: 0x001A5AD8 File Offset: 0x001A3CD8
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ComboBox.DropDownButton.disabled == null)
						{
							VisualStyleElement.ComboBox.DropDownButton.disabled = new VisualStyleElement(VisualStyleElement.ComboBox.className, VisualStyleElement.ComboBox.DropDownButton.part, 4);
						}
						return VisualStyleElement.ComboBox.DropDownButton.disabled;
					}
				}

				// Token: 0x0400458A RID: 17802
				private static readonly int part = 1;

				// Token: 0x0400458B RID: 17803
				private static VisualStyleElement normal;

				// Token: 0x0400458C RID: 17804
				private static VisualStyleElement hot;

				// Token: 0x0400458D RID: 17805
				private static VisualStyleElement pressed;

				// Token: 0x0400458E RID: 17806
				private static VisualStyleElement disabled;
			}

			// Token: 0x020008D8 RID: 2264
			internal static class Border
			{
				// Token: 0x1700195F RID: 6495
				// (get) Token: 0x06007350 RID: 29520 RVA: 0x001A5B03 File Offset: 0x001A3D03
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ComboBox.Border.normal == null)
						{
							VisualStyleElement.ComboBox.Border.normal = new VisualStyleElement(VisualStyleElement.ComboBox.className, 4, 3);
						}
						return VisualStyleElement.ComboBox.Border.normal;
					}
				}

				// Token: 0x0400458F RID: 17807
				private const int part = 4;

				// Token: 0x04004590 RID: 17808
				private static VisualStyleElement normal;
			}

			// Token: 0x020008D9 RID: 2265
			internal static class ReadOnlyButton
			{
				// Token: 0x17001960 RID: 6496
				// (get) Token: 0x06007351 RID: 29521 RVA: 0x001A5B22 File Offset: 0x001A3D22
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ComboBox.ReadOnlyButton.normal == null)
						{
							VisualStyleElement.ComboBox.ReadOnlyButton.normal = new VisualStyleElement(VisualStyleElement.ComboBox.className, 5, 2);
						}
						return VisualStyleElement.ComboBox.ReadOnlyButton.normal;
					}
				}

				// Token: 0x04004591 RID: 17809
				private const int part = 5;

				// Token: 0x04004592 RID: 17810
				private static VisualStyleElement normal;
			}

			// Token: 0x020008DA RID: 2266
			internal static class DropDownButtonRight
			{
				// Token: 0x17001961 RID: 6497
				// (get) Token: 0x06007352 RID: 29522 RVA: 0x001A5B41 File Offset: 0x001A3D41
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ComboBox.DropDownButtonRight.normal == null)
						{
							VisualStyleElement.ComboBox.DropDownButtonRight.normal = new VisualStyleElement(VisualStyleElement.ComboBox.className, 6, 1);
						}
						return VisualStyleElement.ComboBox.DropDownButtonRight.normal;
					}
				}

				// Token: 0x04004593 RID: 17811
				private const int part = 6;

				// Token: 0x04004594 RID: 17812
				private static VisualStyleElement normal;
			}

			// Token: 0x020008DB RID: 2267
			internal static class DropDownButtonLeft
			{
				// Token: 0x17001962 RID: 6498
				// (get) Token: 0x06007353 RID: 29523 RVA: 0x001A5B60 File Offset: 0x001A3D60
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ComboBox.DropDownButtonLeft.normal == null)
						{
							VisualStyleElement.ComboBox.DropDownButtonLeft.normal = new VisualStyleElement(VisualStyleElement.ComboBox.className, 7, 2);
						}
						return VisualStyleElement.ComboBox.DropDownButtonLeft.normal;
					}
				}

				// Token: 0x04004595 RID: 17813
				private const int part = 7;

				// Token: 0x04004596 RID: 17814
				private static VisualStyleElement normal;
			}
		}

		// Token: 0x02000836 RID: 2102
		public static class Page
		{
			// Token: 0x04004367 RID: 17255
			private static readonly string className = "PAGE";

			// Token: 0x020008DC RID: 2268
			public static class Up
			{
				// Token: 0x17001963 RID: 6499
				// (get) Token: 0x06007354 RID: 29524 RVA: 0x001A5B7F File Offset: 0x001A3D7F
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Page.Up.normal == null)
						{
							VisualStyleElement.Page.Up.normal = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.Up.part, 1);
						}
						return VisualStyleElement.Page.Up.normal;
					}
				}

				// Token: 0x17001964 RID: 6500
				// (get) Token: 0x06007355 RID: 29525 RVA: 0x001A5BA2 File Offset: 0x001A3DA2
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Page.Up.hot == null)
						{
							VisualStyleElement.Page.Up.hot = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.Up.part, 2);
						}
						return VisualStyleElement.Page.Up.hot;
					}
				}

				// Token: 0x17001965 RID: 6501
				// (get) Token: 0x06007356 RID: 29526 RVA: 0x001A5BC5 File Offset: 0x001A3DC5
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Page.Up.pressed == null)
						{
							VisualStyleElement.Page.Up.pressed = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.Up.part, 3);
						}
						return VisualStyleElement.Page.Up.pressed;
					}
				}

				// Token: 0x17001966 RID: 6502
				// (get) Token: 0x06007357 RID: 29527 RVA: 0x001A5BE8 File Offset: 0x001A3DE8
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Page.Up.disabled == null)
						{
							VisualStyleElement.Page.Up.disabled = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.Up.part, 4);
						}
						return VisualStyleElement.Page.Up.disabled;
					}
				}

				// Token: 0x04004597 RID: 17815
				private static readonly int part = 1;

				// Token: 0x04004598 RID: 17816
				private static VisualStyleElement normal;

				// Token: 0x04004599 RID: 17817
				private static VisualStyleElement hot;

				// Token: 0x0400459A RID: 17818
				private static VisualStyleElement pressed;

				// Token: 0x0400459B RID: 17819
				private static VisualStyleElement disabled;
			}

			// Token: 0x020008DD RID: 2269
			public static class Down
			{
				// Token: 0x17001967 RID: 6503
				// (get) Token: 0x06007359 RID: 29529 RVA: 0x001A5C13 File Offset: 0x001A3E13
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Page.Down.normal == null)
						{
							VisualStyleElement.Page.Down.normal = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.Down.part, 1);
						}
						return VisualStyleElement.Page.Down.normal;
					}
				}

				// Token: 0x17001968 RID: 6504
				// (get) Token: 0x0600735A RID: 29530 RVA: 0x001A5C36 File Offset: 0x001A3E36
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Page.Down.hot == null)
						{
							VisualStyleElement.Page.Down.hot = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.Down.part, 2);
						}
						return VisualStyleElement.Page.Down.hot;
					}
				}

				// Token: 0x17001969 RID: 6505
				// (get) Token: 0x0600735B RID: 29531 RVA: 0x001A5C59 File Offset: 0x001A3E59
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Page.Down.pressed == null)
						{
							VisualStyleElement.Page.Down.pressed = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.Down.part, 3);
						}
						return VisualStyleElement.Page.Down.pressed;
					}
				}

				// Token: 0x1700196A RID: 6506
				// (get) Token: 0x0600735C RID: 29532 RVA: 0x001A5C7C File Offset: 0x001A3E7C
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Page.Down.disabled == null)
						{
							VisualStyleElement.Page.Down.disabled = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.Down.part, 4);
						}
						return VisualStyleElement.Page.Down.disabled;
					}
				}

				// Token: 0x0400459C RID: 17820
				private static readonly int part = 2;

				// Token: 0x0400459D RID: 17821
				private static VisualStyleElement normal;

				// Token: 0x0400459E RID: 17822
				private static VisualStyleElement hot;

				// Token: 0x0400459F RID: 17823
				private static VisualStyleElement pressed;

				// Token: 0x040045A0 RID: 17824
				private static VisualStyleElement disabled;
			}

			// Token: 0x020008DE RID: 2270
			public static class UpHorizontal
			{
				// Token: 0x1700196B RID: 6507
				// (get) Token: 0x0600735E RID: 29534 RVA: 0x001A5CA7 File Offset: 0x001A3EA7
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Page.UpHorizontal.normal == null)
						{
							VisualStyleElement.Page.UpHorizontal.normal = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.UpHorizontal.part, 1);
						}
						return VisualStyleElement.Page.UpHorizontal.normal;
					}
				}

				// Token: 0x1700196C RID: 6508
				// (get) Token: 0x0600735F RID: 29535 RVA: 0x001A5CCA File Offset: 0x001A3ECA
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Page.UpHorizontal.hot == null)
						{
							VisualStyleElement.Page.UpHorizontal.hot = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.UpHorizontal.part, 2);
						}
						return VisualStyleElement.Page.UpHorizontal.hot;
					}
				}

				// Token: 0x1700196D RID: 6509
				// (get) Token: 0x06007360 RID: 29536 RVA: 0x001A5CED File Offset: 0x001A3EED
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Page.UpHorizontal.pressed == null)
						{
							VisualStyleElement.Page.UpHorizontal.pressed = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.UpHorizontal.part, 3);
						}
						return VisualStyleElement.Page.UpHorizontal.pressed;
					}
				}

				// Token: 0x1700196E RID: 6510
				// (get) Token: 0x06007361 RID: 29537 RVA: 0x001A5D10 File Offset: 0x001A3F10
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Page.UpHorizontal.disabled == null)
						{
							VisualStyleElement.Page.UpHorizontal.disabled = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.UpHorizontal.part, 4);
						}
						return VisualStyleElement.Page.UpHorizontal.disabled;
					}
				}

				// Token: 0x040045A1 RID: 17825
				private static readonly int part = 3;

				// Token: 0x040045A2 RID: 17826
				private static VisualStyleElement normal;

				// Token: 0x040045A3 RID: 17827
				private static VisualStyleElement hot;

				// Token: 0x040045A4 RID: 17828
				private static VisualStyleElement pressed;

				// Token: 0x040045A5 RID: 17829
				private static VisualStyleElement disabled;
			}

			// Token: 0x020008DF RID: 2271
			public static class DownHorizontal
			{
				// Token: 0x1700196F RID: 6511
				// (get) Token: 0x06007363 RID: 29539 RVA: 0x001A5D3B File Offset: 0x001A3F3B
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Page.DownHorizontal.normal == null)
						{
							VisualStyleElement.Page.DownHorizontal.normal = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.DownHorizontal.part, 1);
						}
						return VisualStyleElement.Page.DownHorizontal.normal;
					}
				}

				// Token: 0x17001970 RID: 6512
				// (get) Token: 0x06007364 RID: 29540 RVA: 0x001A5D5E File Offset: 0x001A3F5E
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Page.DownHorizontal.hot == null)
						{
							VisualStyleElement.Page.DownHorizontal.hot = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.DownHorizontal.part, 2);
						}
						return VisualStyleElement.Page.DownHorizontal.hot;
					}
				}

				// Token: 0x17001971 RID: 6513
				// (get) Token: 0x06007365 RID: 29541 RVA: 0x001A5D81 File Offset: 0x001A3F81
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Page.DownHorizontal.pressed == null)
						{
							VisualStyleElement.Page.DownHorizontal.pressed = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.DownHorizontal.part, 3);
						}
						return VisualStyleElement.Page.DownHorizontal.pressed;
					}
				}

				// Token: 0x17001972 RID: 6514
				// (get) Token: 0x06007366 RID: 29542 RVA: 0x001A5DA4 File Offset: 0x001A3FA4
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Page.DownHorizontal.disabled == null)
						{
							VisualStyleElement.Page.DownHorizontal.disabled = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.DownHorizontal.part, 4);
						}
						return VisualStyleElement.Page.DownHorizontal.disabled;
					}
				}

				// Token: 0x040045A6 RID: 17830
				private static readonly int part = 4;

				// Token: 0x040045A7 RID: 17831
				private static VisualStyleElement normal;

				// Token: 0x040045A8 RID: 17832
				private static VisualStyleElement hot;

				// Token: 0x040045A9 RID: 17833
				private static VisualStyleElement pressed;

				// Token: 0x040045AA RID: 17834
				private static VisualStyleElement disabled;
			}
		}

		// Token: 0x02000837 RID: 2103
		public static class Spin
		{
			// Token: 0x04004368 RID: 17256
			private static readonly string className = "SPIN";

			// Token: 0x020008E0 RID: 2272
			public static class Up
			{
				// Token: 0x17001973 RID: 6515
				// (get) Token: 0x06007368 RID: 29544 RVA: 0x001A5DCF File Offset: 0x001A3FCF
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Spin.Up.normal == null)
						{
							VisualStyleElement.Spin.Up.normal = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.Up.part, 1);
						}
						return VisualStyleElement.Spin.Up.normal;
					}
				}

				// Token: 0x17001974 RID: 6516
				// (get) Token: 0x06007369 RID: 29545 RVA: 0x001A5DF2 File Offset: 0x001A3FF2
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Spin.Up.hot == null)
						{
							VisualStyleElement.Spin.Up.hot = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.Up.part, 2);
						}
						return VisualStyleElement.Spin.Up.hot;
					}
				}

				// Token: 0x17001975 RID: 6517
				// (get) Token: 0x0600736A RID: 29546 RVA: 0x001A5E15 File Offset: 0x001A4015
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Spin.Up.pressed == null)
						{
							VisualStyleElement.Spin.Up.pressed = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.Up.part, 3);
						}
						return VisualStyleElement.Spin.Up.pressed;
					}
				}

				// Token: 0x17001976 RID: 6518
				// (get) Token: 0x0600736B RID: 29547 RVA: 0x001A5E38 File Offset: 0x001A4038
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Spin.Up.disabled == null)
						{
							VisualStyleElement.Spin.Up.disabled = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.Up.part, 4);
						}
						return VisualStyleElement.Spin.Up.disabled;
					}
				}

				// Token: 0x040045AB RID: 17835
				private static readonly int part = 1;

				// Token: 0x040045AC RID: 17836
				private static VisualStyleElement normal;

				// Token: 0x040045AD RID: 17837
				private static VisualStyleElement hot;

				// Token: 0x040045AE RID: 17838
				private static VisualStyleElement pressed;

				// Token: 0x040045AF RID: 17839
				private static VisualStyleElement disabled;
			}

			// Token: 0x020008E1 RID: 2273
			public static class Down
			{
				// Token: 0x17001977 RID: 6519
				// (get) Token: 0x0600736D RID: 29549 RVA: 0x001A5E63 File Offset: 0x001A4063
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Spin.Down.normal == null)
						{
							VisualStyleElement.Spin.Down.normal = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.Down.part, 1);
						}
						return VisualStyleElement.Spin.Down.normal;
					}
				}

				// Token: 0x17001978 RID: 6520
				// (get) Token: 0x0600736E RID: 29550 RVA: 0x001A5E86 File Offset: 0x001A4086
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Spin.Down.hot == null)
						{
							VisualStyleElement.Spin.Down.hot = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.Down.part, 2);
						}
						return VisualStyleElement.Spin.Down.hot;
					}
				}

				// Token: 0x17001979 RID: 6521
				// (get) Token: 0x0600736F RID: 29551 RVA: 0x001A5EA9 File Offset: 0x001A40A9
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Spin.Down.pressed == null)
						{
							VisualStyleElement.Spin.Down.pressed = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.Down.part, 3);
						}
						return VisualStyleElement.Spin.Down.pressed;
					}
				}

				// Token: 0x1700197A RID: 6522
				// (get) Token: 0x06007370 RID: 29552 RVA: 0x001A5ECC File Offset: 0x001A40CC
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Spin.Down.disabled == null)
						{
							VisualStyleElement.Spin.Down.disabled = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.Down.part, 4);
						}
						return VisualStyleElement.Spin.Down.disabled;
					}
				}

				// Token: 0x040045B0 RID: 17840
				private static readonly int part = 2;

				// Token: 0x040045B1 RID: 17841
				private static VisualStyleElement normal;

				// Token: 0x040045B2 RID: 17842
				private static VisualStyleElement hot;

				// Token: 0x040045B3 RID: 17843
				private static VisualStyleElement pressed;

				// Token: 0x040045B4 RID: 17844
				private static VisualStyleElement disabled;
			}

			// Token: 0x020008E2 RID: 2274
			public static class UpHorizontal
			{
				// Token: 0x1700197B RID: 6523
				// (get) Token: 0x06007372 RID: 29554 RVA: 0x001A5EF7 File Offset: 0x001A40F7
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Spin.UpHorizontal.normal == null)
						{
							VisualStyleElement.Spin.UpHorizontal.normal = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.UpHorizontal.part, 1);
						}
						return VisualStyleElement.Spin.UpHorizontal.normal;
					}
				}

				// Token: 0x1700197C RID: 6524
				// (get) Token: 0x06007373 RID: 29555 RVA: 0x001A5F1A File Offset: 0x001A411A
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Spin.UpHorizontal.hot == null)
						{
							VisualStyleElement.Spin.UpHorizontal.hot = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.UpHorizontal.part, 2);
						}
						return VisualStyleElement.Spin.UpHorizontal.hot;
					}
				}

				// Token: 0x1700197D RID: 6525
				// (get) Token: 0x06007374 RID: 29556 RVA: 0x001A5F3D File Offset: 0x001A413D
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Spin.UpHorizontal.pressed == null)
						{
							VisualStyleElement.Spin.UpHorizontal.pressed = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.UpHorizontal.part, 3);
						}
						return VisualStyleElement.Spin.UpHorizontal.pressed;
					}
				}

				// Token: 0x1700197E RID: 6526
				// (get) Token: 0x06007375 RID: 29557 RVA: 0x001A5F60 File Offset: 0x001A4160
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Spin.UpHorizontal.disabled == null)
						{
							VisualStyleElement.Spin.UpHorizontal.disabled = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.UpHorizontal.part, 4);
						}
						return VisualStyleElement.Spin.UpHorizontal.disabled;
					}
				}

				// Token: 0x040045B5 RID: 17845
				private static readonly int part = 3;

				// Token: 0x040045B6 RID: 17846
				private static VisualStyleElement normal;

				// Token: 0x040045B7 RID: 17847
				private static VisualStyleElement hot;

				// Token: 0x040045B8 RID: 17848
				private static VisualStyleElement pressed;

				// Token: 0x040045B9 RID: 17849
				private static VisualStyleElement disabled;
			}

			// Token: 0x020008E3 RID: 2275
			public static class DownHorizontal
			{
				// Token: 0x1700197F RID: 6527
				// (get) Token: 0x06007377 RID: 29559 RVA: 0x001A5F8B File Offset: 0x001A418B
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Spin.DownHorizontal.normal == null)
						{
							VisualStyleElement.Spin.DownHorizontal.normal = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.DownHorizontal.part, 1);
						}
						return VisualStyleElement.Spin.DownHorizontal.normal;
					}
				}

				// Token: 0x17001980 RID: 6528
				// (get) Token: 0x06007378 RID: 29560 RVA: 0x001A5FAE File Offset: 0x001A41AE
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Spin.DownHorizontal.hot == null)
						{
							VisualStyleElement.Spin.DownHorizontal.hot = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.DownHorizontal.part, 2);
						}
						return VisualStyleElement.Spin.DownHorizontal.hot;
					}
				}

				// Token: 0x17001981 RID: 6529
				// (get) Token: 0x06007379 RID: 29561 RVA: 0x001A5FD1 File Offset: 0x001A41D1
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Spin.DownHorizontal.pressed == null)
						{
							VisualStyleElement.Spin.DownHorizontal.pressed = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.DownHorizontal.part, 3);
						}
						return VisualStyleElement.Spin.DownHorizontal.pressed;
					}
				}

				// Token: 0x17001982 RID: 6530
				// (get) Token: 0x0600737A RID: 29562 RVA: 0x001A5FF4 File Offset: 0x001A41F4
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Spin.DownHorizontal.disabled == null)
						{
							VisualStyleElement.Spin.DownHorizontal.disabled = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.DownHorizontal.part, 4);
						}
						return VisualStyleElement.Spin.DownHorizontal.disabled;
					}
				}

				// Token: 0x040045BA RID: 17850
				private static readonly int part = 4;

				// Token: 0x040045BB RID: 17851
				private static VisualStyleElement normal;

				// Token: 0x040045BC RID: 17852
				private static VisualStyleElement hot;

				// Token: 0x040045BD RID: 17853
				private static VisualStyleElement pressed;

				// Token: 0x040045BE RID: 17854
				private static VisualStyleElement disabled;
			}
		}

		// Token: 0x02000838 RID: 2104
		public static class ScrollBar
		{
			// Token: 0x04004369 RID: 17257
			private static readonly string className = "SCROLLBAR";

			// Token: 0x020008E4 RID: 2276
			public static class ArrowButton
			{
				// Token: 0x17001983 RID: 6531
				// (get) Token: 0x0600737C RID: 29564 RVA: 0x001A601F File Offset: 0x001A421F
				public static VisualStyleElement UpNormal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.upnormal == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.upnormal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 1);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.upnormal;
					}
				}

				// Token: 0x17001984 RID: 6532
				// (get) Token: 0x0600737D RID: 29565 RVA: 0x001A6042 File Offset: 0x001A4242
				public static VisualStyleElement UpHot
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.uphot == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.uphot = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 2);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.uphot;
					}
				}

				// Token: 0x17001985 RID: 6533
				// (get) Token: 0x0600737E RID: 29566 RVA: 0x001A6065 File Offset: 0x001A4265
				public static VisualStyleElement UpPressed
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.uppressed == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.uppressed = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 3);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.uppressed;
					}
				}

				// Token: 0x17001986 RID: 6534
				// (get) Token: 0x0600737F RID: 29567 RVA: 0x001A6088 File Offset: 0x001A4288
				public static VisualStyleElement UpDisabled
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.updisabled == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.updisabled = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 4);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.updisabled;
					}
				}

				// Token: 0x17001987 RID: 6535
				// (get) Token: 0x06007380 RID: 29568 RVA: 0x001A60AB File Offset: 0x001A42AB
				public static VisualStyleElement DownNormal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.downnormal == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.downnormal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 5);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.downnormal;
					}
				}

				// Token: 0x17001988 RID: 6536
				// (get) Token: 0x06007381 RID: 29569 RVA: 0x001A60CE File Offset: 0x001A42CE
				public static VisualStyleElement DownHot
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.downhot == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.downhot = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 6);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.downhot;
					}
				}

				// Token: 0x17001989 RID: 6537
				// (get) Token: 0x06007382 RID: 29570 RVA: 0x001A60F1 File Offset: 0x001A42F1
				public static VisualStyleElement DownPressed
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.downpressed == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.downpressed = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 7);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.downpressed;
					}
				}

				// Token: 0x1700198A RID: 6538
				// (get) Token: 0x06007383 RID: 29571 RVA: 0x001A6114 File Offset: 0x001A4314
				public static VisualStyleElement DownDisabled
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.downdisabled == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.downdisabled = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 8);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.downdisabled;
					}
				}

				// Token: 0x1700198B RID: 6539
				// (get) Token: 0x06007384 RID: 29572 RVA: 0x001A6137 File Offset: 0x001A4337
				public static VisualStyleElement LeftNormal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.leftnormal == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.leftnormal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 9);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.leftnormal;
					}
				}

				// Token: 0x1700198C RID: 6540
				// (get) Token: 0x06007385 RID: 29573 RVA: 0x001A615B File Offset: 0x001A435B
				public static VisualStyleElement LeftHot
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.lefthot == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.lefthot = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 10);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.lefthot;
					}
				}

				// Token: 0x1700198D RID: 6541
				// (get) Token: 0x06007386 RID: 29574 RVA: 0x001A617F File Offset: 0x001A437F
				public static VisualStyleElement LeftPressed
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.leftpressed == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.leftpressed = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 11);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.leftpressed;
					}
				}

				// Token: 0x1700198E RID: 6542
				// (get) Token: 0x06007387 RID: 29575 RVA: 0x001A61A3 File Offset: 0x001A43A3
				public static VisualStyleElement LeftDisabled
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.leftdisabled == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.leftdisabled = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 12);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.leftdisabled;
					}
				}

				// Token: 0x1700198F RID: 6543
				// (get) Token: 0x06007388 RID: 29576 RVA: 0x001A61C7 File Offset: 0x001A43C7
				public static VisualStyleElement RightNormal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.rightnormal == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.rightnormal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 13);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.rightnormal;
					}
				}

				// Token: 0x17001990 RID: 6544
				// (get) Token: 0x06007389 RID: 29577 RVA: 0x001A61EB File Offset: 0x001A43EB
				public static VisualStyleElement RightHot
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.righthot == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.righthot = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 14);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.righthot;
					}
				}

				// Token: 0x17001991 RID: 6545
				// (get) Token: 0x0600738A RID: 29578 RVA: 0x001A620F File Offset: 0x001A440F
				public static VisualStyleElement RightPressed
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.rightpressed == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.rightpressed = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 15);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.rightpressed;
					}
				}

				// Token: 0x17001992 RID: 6546
				// (get) Token: 0x0600738B RID: 29579 RVA: 0x001A6233 File Offset: 0x001A4433
				public static VisualStyleElement RightDisabled
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.rightdisabled == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.rightdisabled = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 16);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.rightdisabled;
					}
				}

				// Token: 0x040045BF RID: 17855
				private static readonly int part = 1;

				// Token: 0x040045C0 RID: 17856
				private static VisualStyleElement upnormal;

				// Token: 0x040045C1 RID: 17857
				private static VisualStyleElement uphot;

				// Token: 0x040045C2 RID: 17858
				private static VisualStyleElement uppressed;

				// Token: 0x040045C3 RID: 17859
				private static VisualStyleElement updisabled;

				// Token: 0x040045C4 RID: 17860
				private static VisualStyleElement downnormal;

				// Token: 0x040045C5 RID: 17861
				private static VisualStyleElement downhot;

				// Token: 0x040045C6 RID: 17862
				private static VisualStyleElement downpressed;

				// Token: 0x040045C7 RID: 17863
				private static VisualStyleElement downdisabled;

				// Token: 0x040045C8 RID: 17864
				private static VisualStyleElement leftnormal;

				// Token: 0x040045C9 RID: 17865
				private static VisualStyleElement lefthot;

				// Token: 0x040045CA RID: 17866
				private static VisualStyleElement leftpressed;

				// Token: 0x040045CB RID: 17867
				private static VisualStyleElement leftdisabled;

				// Token: 0x040045CC RID: 17868
				private static VisualStyleElement rightnormal;

				// Token: 0x040045CD RID: 17869
				private static VisualStyleElement righthot;

				// Token: 0x040045CE RID: 17870
				private static VisualStyleElement rightpressed;

				// Token: 0x040045CF RID: 17871
				private static VisualStyleElement rightdisabled;
			}

			// Token: 0x020008E5 RID: 2277
			public static class ThumbButtonHorizontal
			{
				// Token: 0x17001993 RID: 6547
				// (get) Token: 0x0600738D RID: 29581 RVA: 0x001A625F File Offset: 0x001A445F
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ThumbButtonHorizontal.normal == null)
						{
							VisualStyleElement.ScrollBar.ThumbButtonHorizontal.normal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ThumbButtonHorizontal.part, 1);
						}
						return VisualStyleElement.ScrollBar.ThumbButtonHorizontal.normal;
					}
				}

				// Token: 0x17001994 RID: 6548
				// (get) Token: 0x0600738E RID: 29582 RVA: 0x001A6282 File Offset: 0x001A4482
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ThumbButtonHorizontal.hot == null)
						{
							VisualStyleElement.ScrollBar.ThumbButtonHorizontal.hot = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ThumbButtonHorizontal.part, 2);
						}
						return VisualStyleElement.ScrollBar.ThumbButtonHorizontal.hot;
					}
				}

				// Token: 0x17001995 RID: 6549
				// (get) Token: 0x0600738F RID: 29583 RVA: 0x001A62A5 File Offset: 0x001A44A5
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ThumbButtonHorizontal.pressed == null)
						{
							VisualStyleElement.ScrollBar.ThumbButtonHorizontal.pressed = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ThumbButtonHorizontal.part, 3);
						}
						return VisualStyleElement.ScrollBar.ThumbButtonHorizontal.pressed;
					}
				}

				// Token: 0x17001996 RID: 6550
				// (get) Token: 0x06007390 RID: 29584 RVA: 0x001A62C8 File Offset: 0x001A44C8
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ThumbButtonHorizontal.disabled == null)
						{
							VisualStyleElement.ScrollBar.ThumbButtonHorizontal.disabled = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ThumbButtonHorizontal.part, 4);
						}
						return VisualStyleElement.ScrollBar.ThumbButtonHorizontal.disabled;
					}
				}

				// Token: 0x040045D0 RID: 17872
				private static readonly int part = 2;

				// Token: 0x040045D1 RID: 17873
				private static VisualStyleElement normal;

				// Token: 0x040045D2 RID: 17874
				private static VisualStyleElement hot;

				// Token: 0x040045D3 RID: 17875
				private static VisualStyleElement pressed;

				// Token: 0x040045D4 RID: 17876
				private static VisualStyleElement disabled;
			}

			// Token: 0x020008E6 RID: 2278
			public static class ThumbButtonVertical
			{
				// Token: 0x17001997 RID: 6551
				// (get) Token: 0x06007392 RID: 29586 RVA: 0x001A62F3 File Offset: 0x001A44F3
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ThumbButtonVertical.normal == null)
						{
							VisualStyleElement.ScrollBar.ThumbButtonVertical.normal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ThumbButtonVertical.part, 1);
						}
						return VisualStyleElement.ScrollBar.ThumbButtonVertical.normal;
					}
				}

				// Token: 0x17001998 RID: 6552
				// (get) Token: 0x06007393 RID: 29587 RVA: 0x001A6316 File Offset: 0x001A4516
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ThumbButtonVertical.hot == null)
						{
							VisualStyleElement.ScrollBar.ThumbButtonVertical.hot = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ThumbButtonVertical.part, 2);
						}
						return VisualStyleElement.ScrollBar.ThumbButtonVertical.hot;
					}
				}

				// Token: 0x17001999 RID: 6553
				// (get) Token: 0x06007394 RID: 29588 RVA: 0x001A6339 File Offset: 0x001A4539
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ThumbButtonVertical.pressed == null)
						{
							VisualStyleElement.ScrollBar.ThumbButtonVertical.pressed = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ThumbButtonVertical.part, 3);
						}
						return VisualStyleElement.ScrollBar.ThumbButtonVertical.pressed;
					}
				}

				// Token: 0x1700199A RID: 6554
				// (get) Token: 0x06007395 RID: 29589 RVA: 0x001A635C File Offset: 0x001A455C
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ThumbButtonVertical.disabled == null)
						{
							VisualStyleElement.ScrollBar.ThumbButtonVertical.disabled = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ThumbButtonVertical.part, 4);
						}
						return VisualStyleElement.ScrollBar.ThumbButtonVertical.disabled;
					}
				}

				// Token: 0x040045D5 RID: 17877
				private static readonly int part = 3;

				// Token: 0x040045D6 RID: 17878
				private static VisualStyleElement normal;

				// Token: 0x040045D7 RID: 17879
				private static VisualStyleElement hot;

				// Token: 0x040045D8 RID: 17880
				private static VisualStyleElement pressed;

				// Token: 0x040045D9 RID: 17881
				private static VisualStyleElement disabled;
			}

			// Token: 0x020008E7 RID: 2279
			public static class RightTrackHorizontal
			{
				// Token: 0x1700199B RID: 6555
				// (get) Token: 0x06007397 RID: 29591 RVA: 0x001A6387 File Offset: 0x001A4587
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.RightTrackHorizontal.normal == null)
						{
							VisualStyleElement.ScrollBar.RightTrackHorizontal.normal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.RightTrackHorizontal.part, 1);
						}
						return VisualStyleElement.ScrollBar.RightTrackHorizontal.normal;
					}
				}

				// Token: 0x1700199C RID: 6556
				// (get) Token: 0x06007398 RID: 29592 RVA: 0x001A63AA File Offset: 0x001A45AA
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ScrollBar.RightTrackHorizontal.hot == null)
						{
							VisualStyleElement.ScrollBar.RightTrackHorizontal.hot = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.RightTrackHorizontal.part, 2);
						}
						return VisualStyleElement.ScrollBar.RightTrackHorizontal.hot;
					}
				}

				// Token: 0x1700199D RID: 6557
				// (get) Token: 0x06007399 RID: 29593 RVA: 0x001A63CD File Offset: 0x001A45CD
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ScrollBar.RightTrackHorizontal.pressed == null)
						{
							VisualStyleElement.ScrollBar.RightTrackHorizontal.pressed = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.RightTrackHorizontal.part, 3);
						}
						return VisualStyleElement.ScrollBar.RightTrackHorizontal.pressed;
					}
				}

				// Token: 0x1700199E RID: 6558
				// (get) Token: 0x0600739A RID: 29594 RVA: 0x001A63F0 File Offset: 0x001A45F0
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ScrollBar.RightTrackHorizontal.disabled == null)
						{
							VisualStyleElement.ScrollBar.RightTrackHorizontal.disabled = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.RightTrackHorizontal.part, 4);
						}
						return VisualStyleElement.ScrollBar.RightTrackHorizontal.disabled;
					}
				}

				// Token: 0x040045DA RID: 17882
				private static readonly int part = 4;

				// Token: 0x040045DB RID: 17883
				private static VisualStyleElement normal;

				// Token: 0x040045DC RID: 17884
				private static VisualStyleElement hot;

				// Token: 0x040045DD RID: 17885
				private static VisualStyleElement pressed;

				// Token: 0x040045DE RID: 17886
				private static VisualStyleElement disabled;
			}

			// Token: 0x020008E8 RID: 2280
			public static class LeftTrackHorizontal
			{
				// Token: 0x1700199F RID: 6559
				// (get) Token: 0x0600739C RID: 29596 RVA: 0x001A641B File Offset: 0x001A461B
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.LeftTrackHorizontal.normal == null)
						{
							VisualStyleElement.ScrollBar.LeftTrackHorizontal.normal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.LeftTrackHorizontal.part, 1);
						}
						return VisualStyleElement.ScrollBar.LeftTrackHorizontal.normal;
					}
				}

				// Token: 0x170019A0 RID: 6560
				// (get) Token: 0x0600739D RID: 29597 RVA: 0x001A643E File Offset: 0x001A463E
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ScrollBar.LeftTrackHorizontal.hot == null)
						{
							VisualStyleElement.ScrollBar.LeftTrackHorizontal.hot = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.LeftTrackHorizontal.part, 2);
						}
						return VisualStyleElement.ScrollBar.LeftTrackHorizontal.hot;
					}
				}

				// Token: 0x170019A1 RID: 6561
				// (get) Token: 0x0600739E RID: 29598 RVA: 0x001A6461 File Offset: 0x001A4661
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ScrollBar.LeftTrackHorizontal.pressed == null)
						{
							VisualStyleElement.ScrollBar.LeftTrackHorizontal.pressed = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.LeftTrackHorizontal.part, 3);
						}
						return VisualStyleElement.ScrollBar.LeftTrackHorizontal.pressed;
					}
				}

				// Token: 0x170019A2 RID: 6562
				// (get) Token: 0x0600739F RID: 29599 RVA: 0x001A6484 File Offset: 0x001A4684
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ScrollBar.LeftTrackHorizontal.disabled == null)
						{
							VisualStyleElement.ScrollBar.LeftTrackHorizontal.disabled = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.LeftTrackHorizontal.part, 4);
						}
						return VisualStyleElement.ScrollBar.LeftTrackHorizontal.disabled;
					}
				}

				// Token: 0x040045DF RID: 17887
				private static readonly int part = 5;

				// Token: 0x040045E0 RID: 17888
				private static VisualStyleElement normal;

				// Token: 0x040045E1 RID: 17889
				private static VisualStyleElement hot;

				// Token: 0x040045E2 RID: 17890
				private static VisualStyleElement pressed;

				// Token: 0x040045E3 RID: 17891
				private static VisualStyleElement disabled;
			}

			// Token: 0x020008E9 RID: 2281
			public static class LowerTrackVertical
			{
				// Token: 0x170019A3 RID: 6563
				// (get) Token: 0x060073A1 RID: 29601 RVA: 0x001A64AF File Offset: 0x001A46AF
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.LowerTrackVertical.normal == null)
						{
							VisualStyleElement.ScrollBar.LowerTrackVertical.normal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.LowerTrackVertical.part, 1);
						}
						return VisualStyleElement.ScrollBar.LowerTrackVertical.normal;
					}
				}

				// Token: 0x170019A4 RID: 6564
				// (get) Token: 0x060073A2 RID: 29602 RVA: 0x001A64D2 File Offset: 0x001A46D2
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ScrollBar.LowerTrackVertical.hot == null)
						{
							VisualStyleElement.ScrollBar.LowerTrackVertical.hot = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.LowerTrackVertical.part, 2);
						}
						return VisualStyleElement.ScrollBar.LowerTrackVertical.hot;
					}
				}

				// Token: 0x170019A5 RID: 6565
				// (get) Token: 0x060073A3 RID: 29603 RVA: 0x001A64F5 File Offset: 0x001A46F5
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ScrollBar.LowerTrackVertical.pressed == null)
						{
							VisualStyleElement.ScrollBar.LowerTrackVertical.pressed = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.LowerTrackVertical.part, 3);
						}
						return VisualStyleElement.ScrollBar.LowerTrackVertical.pressed;
					}
				}

				// Token: 0x170019A6 RID: 6566
				// (get) Token: 0x060073A4 RID: 29604 RVA: 0x001A6518 File Offset: 0x001A4718
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ScrollBar.LowerTrackVertical.disabled == null)
						{
							VisualStyleElement.ScrollBar.LowerTrackVertical.disabled = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.LowerTrackVertical.part, 4);
						}
						return VisualStyleElement.ScrollBar.LowerTrackVertical.disabled;
					}
				}

				// Token: 0x040045E4 RID: 17892
				private static readonly int part = 6;

				// Token: 0x040045E5 RID: 17893
				private static VisualStyleElement normal;

				// Token: 0x040045E6 RID: 17894
				private static VisualStyleElement hot;

				// Token: 0x040045E7 RID: 17895
				private static VisualStyleElement pressed;

				// Token: 0x040045E8 RID: 17896
				private static VisualStyleElement disabled;
			}

			// Token: 0x020008EA RID: 2282
			public static class UpperTrackVertical
			{
				// Token: 0x170019A7 RID: 6567
				// (get) Token: 0x060073A6 RID: 29606 RVA: 0x001A6543 File Offset: 0x001A4743
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.UpperTrackVertical.normal == null)
						{
							VisualStyleElement.ScrollBar.UpperTrackVertical.normal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.UpperTrackVertical.part, 1);
						}
						return VisualStyleElement.ScrollBar.UpperTrackVertical.normal;
					}
				}

				// Token: 0x170019A8 RID: 6568
				// (get) Token: 0x060073A7 RID: 29607 RVA: 0x001A6566 File Offset: 0x001A4766
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ScrollBar.UpperTrackVertical.hot == null)
						{
							VisualStyleElement.ScrollBar.UpperTrackVertical.hot = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.UpperTrackVertical.part, 2);
						}
						return VisualStyleElement.ScrollBar.UpperTrackVertical.hot;
					}
				}

				// Token: 0x170019A9 RID: 6569
				// (get) Token: 0x060073A8 RID: 29608 RVA: 0x001A6589 File Offset: 0x001A4789
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ScrollBar.UpperTrackVertical.pressed == null)
						{
							VisualStyleElement.ScrollBar.UpperTrackVertical.pressed = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.UpperTrackVertical.part, 3);
						}
						return VisualStyleElement.ScrollBar.UpperTrackVertical.pressed;
					}
				}

				// Token: 0x170019AA RID: 6570
				// (get) Token: 0x060073A9 RID: 29609 RVA: 0x001A65AC File Offset: 0x001A47AC
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ScrollBar.UpperTrackVertical.disabled == null)
						{
							VisualStyleElement.ScrollBar.UpperTrackVertical.disabled = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.UpperTrackVertical.part, 4);
						}
						return VisualStyleElement.ScrollBar.UpperTrackVertical.disabled;
					}
				}

				// Token: 0x040045E9 RID: 17897
				private static readonly int part = 7;

				// Token: 0x040045EA RID: 17898
				private static VisualStyleElement normal;

				// Token: 0x040045EB RID: 17899
				private static VisualStyleElement hot;

				// Token: 0x040045EC RID: 17900
				private static VisualStyleElement pressed;

				// Token: 0x040045ED RID: 17901
				private static VisualStyleElement disabled;
			}

			// Token: 0x020008EB RID: 2283
			public static class GripperHorizontal
			{
				// Token: 0x170019AB RID: 6571
				// (get) Token: 0x060073AB RID: 29611 RVA: 0x001A65D7 File Offset: 0x001A47D7
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.GripperHorizontal.normal == null)
						{
							VisualStyleElement.ScrollBar.GripperHorizontal.normal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.GripperHorizontal.part, 0);
						}
						return VisualStyleElement.ScrollBar.GripperHorizontal.normal;
					}
				}

				// Token: 0x040045EE RID: 17902
				private static readonly int part = 8;

				// Token: 0x040045EF RID: 17903
				private static VisualStyleElement normal;
			}

			// Token: 0x020008EC RID: 2284
			public static class GripperVertical
			{
				// Token: 0x170019AC RID: 6572
				// (get) Token: 0x060073AD RID: 29613 RVA: 0x001A6602 File Offset: 0x001A4802
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.GripperVertical.normal == null)
						{
							VisualStyleElement.ScrollBar.GripperVertical.normal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.GripperVertical.part, 0);
						}
						return VisualStyleElement.ScrollBar.GripperVertical.normal;
					}
				}

				// Token: 0x040045F0 RID: 17904
				private static readonly int part = 9;

				// Token: 0x040045F1 RID: 17905
				private static VisualStyleElement normal;
			}

			// Token: 0x020008ED RID: 2285
			public static class SizeBox
			{
				// Token: 0x170019AD RID: 6573
				// (get) Token: 0x060073AF RID: 29615 RVA: 0x001A662E File Offset: 0x001A482E
				public static VisualStyleElement RightAlign
				{
					get
					{
						if (VisualStyleElement.ScrollBar.SizeBox.rightalign == null)
						{
							VisualStyleElement.ScrollBar.SizeBox.rightalign = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.SizeBox.part, 1);
						}
						return VisualStyleElement.ScrollBar.SizeBox.rightalign;
					}
				}

				// Token: 0x170019AE RID: 6574
				// (get) Token: 0x060073B0 RID: 29616 RVA: 0x001A6651 File Offset: 0x001A4851
				public static VisualStyleElement LeftAlign
				{
					get
					{
						if (VisualStyleElement.ScrollBar.SizeBox.leftalign == null)
						{
							VisualStyleElement.ScrollBar.SizeBox.leftalign = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.SizeBox.part, 2);
						}
						return VisualStyleElement.ScrollBar.SizeBox.leftalign;
					}
				}

				// Token: 0x040045F2 RID: 17906
				private static readonly int part = 10;

				// Token: 0x040045F3 RID: 17907
				private static VisualStyleElement rightalign;

				// Token: 0x040045F4 RID: 17908
				private static VisualStyleElement leftalign;
			}
		}

		// Token: 0x02000839 RID: 2105
		public static class Tab
		{
			// Token: 0x0400436A RID: 17258
			private static readonly string className = "TAB";

			// Token: 0x020008EE RID: 2286
			public static class TabItem
			{
				// Token: 0x170019AF RID: 6575
				// (get) Token: 0x060073B2 RID: 29618 RVA: 0x001A667D File Offset: 0x001A487D
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Tab.TabItem.normal == null)
						{
							VisualStyleElement.Tab.TabItem.normal = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItem.part, 1);
						}
						return VisualStyleElement.Tab.TabItem.normal;
					}
				}

				// Token: 0x170019B0 RID: 6576
				// (get) Token: 0x060073B3 RID: 29619 RVA: 0x001A66A0 File Offset: 0x001A48A0
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Tab.TabItem.hot == null)
						{
							VisualStyleElement.Tab.TabItem.hot = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItem.part, 2);
						}
						return VisualStyleElement.Tab.TabItem.hot;
					}
				}

				// Token: 0x170019B1 RID: 6577
				// (get) Token: 0x060073B4 RID: 29620 RVA: 0x001A66C3 File Offset: 0x001A48C3
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Tab.TabItem.pressed == null)
						{
							VisualStyleElement.Tab.TabItem.pressed = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItem.part, 3);
						}
						return VisualStyleElement.Tab.TabItem.pressed;
					}
				}

				// Token: 0x170019B2 RID: 6578
				// (get) Token: 0x060073B5 RID: 29621 RVA: 0x001A66E6 File Offset: 0x001A48E6
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Tab.TabItem.disabled == null)
						{
							VisualStyleElement.Tab.TabItem.disabled = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItem.part, 4);
						}
						return VisualStyleElement.Tab.TabItem.disabled;
					}
				}

				// Token: 0x040045F5 RID: 17909
				private static readonly int part = 1;

				// Token: 0x040045F6 RID: 17910
				private static VisualStyleElement normal;

				// Token: 0x040045F7 RID: 17911
				private static VisualStyleElement hot;

				// Token: 0x040045F8 RID: 17912
				private static VisualStyleElement pressed;

				// Token: 0x040045F9 RID: 17913
				private static VisualStyleElement disabled;
			}

			// Token: 0x020008EF RID: 2287
			public static class TabItemLeftEdge
			{
				// Token: 0x170019B3 RID: 6579
				// (get) Token: 0x060073B7 RID: 29623 RVA: 0x001A6711 File Offset: 0x001A4911
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Tab.TabItemLeftEdge.normal == null)
						{
							VisualStyleElement.Tab.TabItemLeftEdge.normal = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItemLeftEdge.part, 1);
						}
						return VisualStyleElement.Tab.TabItemLeftEdge.normal;
					}
				}

				// Token: 0x170019B4 RID: 6580
				// (get) Token: 0x060073B8 RID: 29624 RVA: 0x001A6734 File Offset: 0x001A4934
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Tab.TabItemLeftEdge.hot == null)
						{
							VisualStyleElement.Tab.TabItemLeftEdge.hot = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItemLeftEdge.part, 2);
						}
						return VisualStyleElement.Tab.TabItemLeftEdge.hot;
					}
				}

				// Token: 0x170019B5 RID: 6581
				// (get) Token: 0x060073B9 RID: 29625 RVA: 0x001A6757 File Offset: 0x001A4957
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Tab.TabItemLeftEdge.pressed == null)
						{
							VisualStyleElement.Tab.TabItemLeftEdge.pressed = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItemLeftEdge.part, 3);
						}
						return VisualStyleElement.Tab.TabItemLeftEdge.pressed;
					}
				}

				// Token: 0x170019B6 RID: 6582
				// (get) Token: 0x060073BA RID: 29626 RVA: 0x001A677A File Offset: 0x001A497A
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Tab.TabItemLeftEdge.disabled == null)
						{
							VisualStyleElement.Tab.TabItemLeftEdge.disabled = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItemLeftEdge.part, 4);
						}
						return VisualStyleElement.Tab.TabItemLeftEdge.disabled;
					}
				}

				// Token: 0x040045FA RID: 17914
				private static readonly int part = 2;

				// Token: 0x040045FB RID: 17915
				private static VisualStyleElement normal;

				// Token: 0x040045FC RID: 17916
				private static VisualStyleElement hot;

				// Token: 0x040045FD RID: 17917
				private static VisualStyleElement pressed;

				// Token: 0x040045FE RID: 17918
				private static VisualStyleElement disabled;
			}

			// Token: 0x020008F0 RID: 2288
			public static class TabItemRightEdge
			{
				// Token: 0x170019B7 RID: 6583
				// (get) Token: 0x060073BC RID: 29628 RVA: 0x001A67A5 File Offset: 0x001A49A5
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Tab.TabItemRightEdge.normal == null)
						{
							VisualStyleElement.Tab.TabItemRightEdge.normal = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItemRightEdge.part, 1);
						}
						return VisualStyleElement.Tab.TabItemRightEdge.normal;
					}
				}

				// Token: 0x170019B8 RID: 6584
				// (get) Token: 0x060073BD RID: 29629 RVA: 0x001A67C8 File Offset: 0x001A49C8
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Tab.TabItemRightEdge.hot == null)
						{
							VisualStyleElement.Tab.TabItemRightEdge.hot = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItemRightEdge.part, 2);
						}
						return VisualStyleElement.Tab.TabItemRightEdge.hot;
					}
				}

				// Token: 0x170019B9 RID: 6585
				// (get) Token: 0x060073BE RID: 29630 RVA: 0x001A67EB File Offset: 0x001A49EB
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Tab.TabItemRightEdge.pressed == null)
						{
							VisualStyleElement.Tab.TabItemRightEdge.pressed = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItemRightEdge.part, 3);
						}
						return VisualStyleElement.Tab.TabItemRightEdge.pressed;
					}
				}

				// Token: 0x170019BA RID: 6586
				// (get) Token: 0x060073BF RID: 29631 RVA: 0x001A680E File Offset: 0x001A4A0E
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Tab.TabItemRightEdge.disabled == null)
						{
							VisualStyleElement.Tab.TabItemRightEdge.disabled = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItemRightEdge.part, 4);
						}
						return VisualStyleElement.Tab.TabItemRightEdge.disabled;
					}
				}

				// Token: 0x040045FF RID: 17919
				private static readonly int part = 3;

				// Token: 0x04004600 RID: 17920
				private static VisualStyleElement normal;

				// Token: 0x04004601 RID: 17921
				private static VisualStyleElement hot;

				// Token: 0x04004602 RID: 17922
				private static VisualStyleElement pressed;

				// Token: 0x04004603 RID: 17923
				private static VisualStyleElement disabled;
			}

			// Token: 0x020008F1 RID: 2289
			public static class TabItemBothEdges
			{
				// Token: 0x170019BB RID: 6587
				// (get) Token: 0x060073C1 RID: 29633 RVA: 0x001A6839 File Offset: 0x001A4A39
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Tab.TabItemBothEdges.normal == null)
						{
							VisualStyleElement.Tab.TabItemBothEdges.normal = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItemBothEdges.part, 0);
						}
						return VisualStyleElement.Tab.TabItemBothEdges.normal;
					}
				}

				// Token: 0x04004604 RID: 17924
				private static readonly int part = 4;

				// Token: 0x04004605 RID: 17925
				private static VisualStyleElement normal;
			}

			// Token: 0x020008F2 RID: 2290
			public static class TopTabItem
			{
				// Token: 0x170019BC RID: 6588
				// (get) Token: 0x060073C3 RID: 29635 RVA: 0x001A6864 File Offset: 0x001A4A64
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItem.normal == null)
						{
							VisualStyleElement.Tab.TopTabItem.normal = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItem.part, 1);
						}
						return VisualStyleElement.Tab.TopTabItem.normal;
					}
				}

				// Token: 0x170019BD RID: 6589
				// (get) Token: 0x060073C4 RID: 29636 RVA: 0x001A6887 File Offset: 0x001A4A87
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItem.hot == null)
						{
							VisualStyleElement.Tab.TopTabItem.hot = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItem.part, 2);
						}
						return VisualStyleElement.Tab.TopTabItem.hot;
					}
				}

				// Token: 0x170019BE RID: 6590
				// (get) Token: 0x060073C5 RID: 29637 RVA: 0x001A68AA File Offset: 0x001A4AAA
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItem.pressed == null)
						{
							VisualStyleElement.Tab.TopTabItem.pressed = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItem.part, 3);
						}
						return VisualStyleElement.Tab.TopTabItem.pressed;
					}
				}

				// Token: 0x170019BF RID: 6591
				// (get) Token: 0x060073C6 RID: 29638 RVA: 0x001A68CD File Offset: 0x001A4ACD
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItem.disabled == null)
						{
							VisualStyleElement.Tab.TopTabItem.disabled = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItem.part, 4);
						}
						return VisualStyleElement.Tab.TopTabItem.disabled;
					}
				}

				// Token: 0x04004606 RID: 17926
				private static readonly int part = 5;

				// Token: 0x04004607 RID: 17927
				private static VisualStyleElement normal;

				// Token: 0x04004608 RID: 17928
				private static VisualStyleElement hot;

				// Token: 0x04004609 RID: 17929
				private static VisualStyleElement pressed;

				// Token: 0x0400460A RID: 17930
				private static VisualStyleElement disabled;
			}

			// Token: 0x020008F3 RID: 2291
			public static class TopTabItemLeftEdge
			{
				// Token: 0x170019C0 RID: 6592
				// (get) Token: 0x060073C8 RID: 29640 RVA: 0x001A68F8 File Offset: 0x001A4AF8
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItemLeftEdge.normal == null)
						{
							VisualStyleElement.Tab.TopTabItemLeftEdge.normal = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItemLeftEdge.part, 1);
						}
						return VisualStyleElement.Tab.TopTabItemLeftEdge.normal;
					}
				}

				// Token: 0x170019C1 RID: 6593
				// (get) Token: 0x060073C9 RID: 29641 RVA: 0x001A691B File Offset: 0x001A4B1B
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItemLeftEdge.hot == null)
						{
							VisualStyleElement.Tab.TopTabItemLeftEdge.hot = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItemLeftEdge.part, 2);
						}
						return VisualStyleElement.Tab.TopTabItemLeftEdge.hot;
					}
				}

				// Token: 0x170019C2 RID: 6594
				// (get) Token: 0x060073CA RID: 29642 RVA: 0x001A693E File Offset: 0x001A4B3E
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItemLeftEdge.pressed == null)
						{
							VisualStyleElement.Tab.TopTabItemLeftEdge.pressed = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItemLeftEdge.part, 3);
						}
						return VisualStyleElement.Tab.TopTabItemLeftEdge.pressed;
					}
				}

				// Token: 0x170019C3 RID: 6595
				// (get) Token: 0x060073CB RID: 29643 RVA: 0x001A6961 File Offset: 0x001A4B61
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItemLeftEdge.disabled == null)
						{
							VisualStyleElement.Tab.TopTabItemLeftEdge.disabled = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItemLeftEdge.part, 4);
						}
						return VisualStyleElement.Tab.TopTabItemLeftEdge.disabled;
					}
				}

				// Token: 0x0400460B RID: 17931
				private static readonly int part = 6;

				// Token: 0x0400460C RID: 17932
				private static VisualStyleElement normal;

				// Token: 0x0400460D RID: 17933
				private static VisualStyleElement hot;

				// Token: 0x0400460E RID: 17934
				private static VisualStyleElement pressed;

				// Token: 0x0400460F RID: 17935
				private static VisualStyleElement disabled;
			}

			// Token: 0x020008F4 RID: 2292
			public static class TopTabItemRightEdge
			{
				// Token: 0x170019C4 RID: 6596
				// (get) Token: 0x060073CD RID: 29645 RVA: 0x001A698C File Offset: 0x001A4B8C
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItemRightEdge.normal == null)
						{
							VisualStyleElement.Tab.TopTabItemRightEdge.normal = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItemRightEdge.part, 1);
						}
						return VisualStyleElement.Tab.TopTabItemRightEdge.normal;
					}
				}

				// Token: 0x170019C5 RID: 6597
				// (get) Token: 0x060073CE RID: 29646 RVA: 0x001A69AF File Offset: 0x001A4BAF
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItemRightEdge.hot == null)
						{
							VisualStyleElement.Tab.TopTabItemRightEdge.hot = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItemRightEdge.part, 2);
						}
						return VisualStyleElement.Tab.TopTabItemRightEdge.hot;
					}
				}

				// Token: 0x170019C6 RID: 6598
				// (get) Token: 0x060073CF RID: 29647 RVA: 0x001A69D2 File Offset: 0x001A4BD2
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItemRightEdge.pressed == null)
						{
							VisualStyleElement.Tab.TopTabItemRightEdge.pressed = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItemRightEdge.part, 3);
						}
						return VisualStyleElement.Tab.TopTabItemRightEdge.pressed;
					}
				}

				// Token: 0x170019C7 RID: 6599
				// (get) Token: 0x060073D0 RID: 29648 RVA: 0x001A69F5 File Offset: 0x001A4BF5
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItemRightEdge.disabled == null)
						{
							VisualStyleElement.Tab.TopTabItemRightEdge.disabled = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItemRightEdge.part, 4);
						}
						return VisualStyleElement.Tab.TopTabItemRightEdge.disabled;
					}
				}

				// Token: 0x04004610 RID: 17936
				private static readonly int part = 7;

				// Token: 0x04004611 RID: 17937
				private static VisualStyleElement normal;

				// Token: 0x04004612 RID: 17938
				private static VisualStyleElement hot;

				// Token: 0x04004613 RID: 17939
				private static VisualStyleElement pressed;

				// Token: 0x04004614 RID: 17940
				private static VisualStyleElement disabled;
			}

			// Token: 0x020008F5 RID: 2293
			public static class TopTabItemBothEdges
			{
				// Token: 0x170019C8 RID: 6600
				// (get) Token: 0x060073D2 RID: 29650 RVA: 0x001A6A20 File Offset: 0x001A4C20
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItemBothEdges.normal == null)
						{
							VisualStyleElement.Tab.TopTabItemBothEdges.normal = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItemBothEdges.part, 0);
						}
						return VisualStyleElement.Tab.TopTabItemBothEdges.normal;
					}
				}

				// Token: 0x04004615 RID: 17941
				private static readonly int part = 8;

				// Token: 0x04004616 RID: 17942
				private static VisualStyleElement normal;
			}

			// Token: 0x020008F6 RID: 2294
			public static class Pane
			{
				// Token: 0x170019C9 RID: 6601
				// (get) Token: 0x060073D4 RID: 29652 RVA: 0x001A6A4B File Offset: 0x001A4C4B
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Tab.Pane.normal == null)
						{
							VisualStyleElement.Tab.Pane.normal = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.Pane.part, 0);
						}
						return VisualStyleElement.Tab.Pane.normal;
					}
				}

				// Token: 0x04004617 RID: 17943
				private static readonly int part = 9;

				// Token: 0x04004618 RID: 17944
				private static VisualStyleElement normal;
			}

			// Token: 0x020008F7 RID: 2295
			public static class Body
			{
				// Token: 0x170019CA RID: 6602
				// (get) Token: 0x060073D6 RID: 29654 RVA: 0x001A6A77 File Offset: 0x001A4C77
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Tab.Body.normal == null)
						{
							VisualStyleElement.Tab.Body.normal = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.Body.part, 0);
						}
						return VisualStyleElement.Tab.Body.normal;
					}
				}

				// Token: 0x04004619 RID: 17945
				private static readonly int part = 10;

				// Token: 0x0400461A RID: 17946
				private static VisualStyleElement normal;
			}
		}

		// Token: 0x0200083A RID: 2106
		public static class ExplorerBar
		{
			// Token: 0x0400436B RID: 17259
			private static readonly string className = "EXPLORERBAR";

			// Token: 0x020008F8 RID: 2296
			public static class HeaderBackground
			{
				// Token: 0x170019CB RID: 6603
				// (get) Token: 0x060073D8 RID: 29656 RVA: 0x001A6AA3 File Offset: 0x001A4CA3
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.HeaderBackground.normal == null)
						{
							VisualStyleElement.ExplorerBar.HeaderBackground.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.HeaderBackground.part, 0);
						}
						return VisualStyleElement.ExplorerBar.HeaderBackground.normal;
					}
				}

				// Token: 0x0400461B RID: 17947
				private static readonly int part = 1;

				// Token: 0x0400461C RID: 17948
				private static VisualStyleElement normal;
			}

			// Token: 0x020008F9 RID: 2297
			public static class HeaderClose
			{
				// Token: 0x170019CC RID: 6604
				// (get) Token: 0x060073DA RID: 29658 RVA: 0x001A6ACE File Offset: 0x001A4CCE
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.HeaderClose.normal == null)
						{
							VisualStyleElement.ExplorerBar.HeaderClose.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.HeaderClose.part, 1);
						}
						return VisualStyleElement.ExplorerBar.HeaderClose.normal;
					}
				}

				// Token: 0x170019CD RID: 6605
				// (get) Token: 0x060073DB RID: 29659 RVA: 0x001A6AF1 File Offset: 0x001A4CF1
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.HeaderClose.hot == null)
						{
							VisualStyleElement.ExplorerBar.HeaderClose.hot = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.HeaderClose.part, 2);
						}
						return VisualStyleElement.ExplorerBar.HeaderClose.hot;
					}
				}

				// Token: 0x170019CE RID: 6606
				// (get) Token: 0x060073DC RID: 29660 RVA: 0x001A6B14 File Offset: 0x001A4D14
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.HeaderClose.pressed == null)
						{
							VisualStyleElement.ExplorerBar.HeaderClose.pressed = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.HeaderClose.part, 3);
						}
						return VisualStyleElement.ExplorerBar.HeaderClose.pressed;
					}
				}

				// Token: 0x0400461D RID: 17949
				private static readonly int part = 2;

				// Token: 0x0400461E RID: 17950
				private static VisualStyleElement normal;

				// Token: 0x0400461F RID: 17951
				private static VisualStyleElement hot;

				// Token: 0x04004620 RID: 17952
				private static VisualStyleElement pressed;
			}

			// Token: 0x020008FA RID: 2298
			public static class HeaderPin
			{
				// Token: 0x170019CF RID: 6607
				// (get) Token: 0x060073DE RID: 29662 RVA: 0x001A6B3F File Offset: 0x001A4D3F
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.HeaderPin.normal == null)
						{
							VisualStyleElement.ExplorerBar.HeaderPin.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.HeaderPin.part, 1);
						}
						return VisualStyleElement.ExplorerBar.HeaderPin.normal;
					}
				}

				// Token: 0x170019D0 RID: 6608
				// (get) Token: 0x060073DF RID: 29663 RVA: 0x001A6B62 File Offset: 0x001A4D62
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.HeaderPin.hot == null)
						{
							VisualStyleElement.ExplorerBar.HeaderPin.hot = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.HeaderPin.part, 2);
						}
						return VisualStyleElement.ExplorerBar.HeaderPin.hot;
					}
				}

				// Token: 0x170019D1 RID: 6609
				// (get) Token: 0x060073E0 RID: 29664 RVA: 0x001A6B85 File Offset: 0x001A4D85
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.HeaderPin.pressed == null)
						{
							VisualStyleElement.ExplorerBar.HeaderPin.pressed = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.HeaderPin.part, 3);
						}
						return VisualStyleElement.ExplorerBar.HeaderPin.pressed;
					}
				}

				// Token: 0x170019D2 RID: 6610
				// (get) Token: 0x060073E1 RID: 29665 RVA: 0x001A6BA8 File Offset: 0x001A4DA8
				public static VisualStyleElement SelectedNormal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.HeaderPin.selectednormal == null)
						{
							VisualStyleElement.ExplorerBar.HeaderPin.selectednormal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.HeaderPin.part, 4);
						}
						return VisualStyleElement.ExplorerBar.HeaderPin.selectednormal;
					}
				}

				// Token: 0x170019D3 RID: 6611
				// (get) Token: 0x060073E2 RID: 29666 RVA: 0x001A6BCB File Offset: 0x001A4DCB
				public static VisualStyleElement SelectedHot
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.HeaderPin.selectedhot == null)
						{
							VisualStyleElement.ExplorerBar.HeaderPin.selectedhot = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.HeaderPin.part, 5);
						}
						return VisualStyleElement.ExplorerBar.HeaderPin.selectedhot;
					}
				}

				// Token: 0x170019D4 RID: 6612
				// (get) Token: 0x060073E3 RID: 29667 RVA: 0x001A6BEE File Offset: 0x001A4DEE
				public static VisualStyleElement SelectedPressed
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.HeaderPin.selectedpressed == null)
						{
							VisualStyleElement.ExplorerBar.HeaderPin.selectedpressed = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.HeaderPin.part, 6);
						}
						return VisualStyleElement.ExplorerBar.HeaderPin.selectedpressed;
					}
				}

				// Token: 0x04004621 RID: 17953
				private static readonly int part = 3;

				// Token: 0x04004622 RID: 17954
				private static VisualStyleElement normal;

				// Token: 0x04004623 RID: 17955
				private static VisualStyleElement hot;

				// Token: 0x04004624 RID: 17956
				private static VisualStyleElement pressed;

				// Token: 0x04004625 RID: 17957
				private static VisualStyleElement selectednormal;

				// Token: 0x04004626 RID: 17958
				private static VisualStyleElement selectedhot;

				// Token: 0x04004627 RID: 17959
				private static VisualStyleElement selectedpressed;
			}

			// Token: 0x020008FB RID: 2299
			public static class IEBarMenu
			{
				// Token: 0x170019D5 RID: 6613
				// (get) Token: 0x060073E5 RID: 29669 RVA: 0x001A6C19 File Offset: 0x001A4E19
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.IEBarMenu.normal == null)
						{
							VisualStyleElement.ExplorerBar.IEBarMenu.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.IEBarMenu.part, 1);
						}
						return VisualStyleElement.ExplorerBar.IEBarMenu.normal;
					}
				}

				// Token: 0x170019D6 RID: 6614
				// (get) Token: 0x060073E6 RID: 29670 RVA: 0x001A6C3C File Offset: 0x001A4E3C
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.IEBarMenu.hot == null)
						{
							VisualStyleElement.ExplorerBar.IEBarMenu.hot = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.IEBarMenu.part, 2);
						}
						return VisualStyleElement.ExplorerBar.IEBarMenu.hot;
					}
				}

				// Token: 0x170019D7 RID: 6615
				// (get) Token: 0x060073E7 RID: 29671 RVA: 0x001A6C5F File Offset: 0x001A4E5F
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.IEBarMenu.pressed == null)
						{
							VisualStyleElement.ExplorerBar.IEBarMenu.pressed = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.IEBarMenu.part, 3);
						}
						return VisualStyleElement.ExplorerBar.IEBarMenu.pressed;
					}
				}

				// Token: 0x04004628 RID: 17960
				private static readonly int part = 4;

				// Token: 0x04004629 RID: 17961
				private static VisualStyleElement normal;

				// Token: 0x0400462A RID: 17962
				private static VisualStyleElement hot;

				// Token: 0x0400462B RID: 17963
				private static VisualStyleElement pressed;
			}

			// Token: 0x020008FC RID: 2300
			public static class NormalGroupBackground
			{
				// Token: 0x170019D8 RID: 6616
				// (get) Token: 0x060073E9 RID: 29673 RVA: 0x001A6C8A File Offset: 0x001A4E8A
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.NormalGroupBackground.normal == null)
						{
							VisualStyleElement.ExplorerBar.NormalGroupBackground.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.NormalGroupBackground.part, 0);
						}
						return VisualStyleElement.ExplorerBar.NormalGroupBackground.normal;
					}
				}

				// Token: 0x0400462C RID: 17964
				private static readonly int part = 5;

				// Token: 0x0400462D RID: 17965
				private static VisualStyleElement normal;
			}

			// Token: 0x020008FD RID: 2301
			public static class NormalGroupCollapse
			{
				// Token: 0x170019D9 RID: 6617
				// (get) Token: 0x060073EB RID: 29675 RVA: 0x001A6CB5 File Offset: 0x001A4EB5
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.NormalGroupCollapse.normal == null)
						{
							VisualStyleElement.ExplorerBar.NormalGroupCollapse.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.NormalGroupCollapse.part, 1);
						}
						return VisualStyleElement.ExplorerBar.NormalGroupCollapse.normal;
					}
				}

				// Token: 0x170019DA RID: 6618
				// (get) Token: 0x060073EC RID: 29676 RVA: 0x001A6CD8 File Offset: 0x001A4ED8
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.NormalGroupCollapse.hot == null)
						{
							VisualStyleElement.ExplorerBar.NormalGroupCollapse.hot = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.NormalGroupCollapse.part, 2);
						}
						return VisualStyleElement.ExplorerBar.NormalGroupCollapse.hot;
					}
				}

				// Token: 0x170019DB RID: 6619
				// (get) Token: 0x060073ED RID: 29677 RVA: 0x001A6CFB File Offset: 0x001A4EFB
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.NormalGroupCollapse.pressed == null)
						{
							VisualStyleElement.ExplorerBar.NormalGroupCollapse.pressed = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.NormalGroupCollapse.part, 3);
						}
						return VisualStyleElement.ExplorerBar.NormalGroupCollapse.pressed;
					}
				}

				// Token: 0x0400462E RID: 17966
				private static readonly int part = 6;

				// Token: 0x0400462F RID: 17967
				private static VisualStyleElement normal;

				// Token: 0x04004630 RID: 17968
				private static VisualStyleElement hot;

				// Token: 0x04004631 RID: 17969
				private static VisualStyleElement pressed;
			}

			// Token: 0x020008FE RID: 2302
			public static class NormalGroupExpand
			{
				// Token: 0x170019DC RID: 6620
				// (get) Token: 0x060073EF RID: 29679 RVA: 0x001A6D26 File Offset: 0x001A4F26
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.NormalGroupExpand.normal == null)
						{
							VisualStyleElement.ExplorerBar.NormalGroupExpand.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.NormalGroupExpand.part, 1);
						}
						return VisualStyleElement.ExplorerBar.NormalGroupExpand.normal;
					}
				}

				// Token: 0x170019DD RID: 6621
				// (get) Token: 0x060073F0 RID: 29680 RVA: 0x001A6D49 File Offset: 0x001A4F49
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.NormalGroupExpand.hot == null)
						{
							VisualStyleElement.ExplorerBar.NormalGroupExpand.hot = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.NormalGroupExpand.part, 2);
						}
						return VisualStyleElement.ExplorerBar.NormalGroupExpand.hot;
					}
				}

				// Token: 0x170019DE RID: 6622
				// (get) Token: 0x060073F1 RID: 29681 RVA: 0x001A6D6C File Offset: 0x001A4F6C
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.NormalGroupExpand.pressed == null)
						{
							VisualStyleElement.ExplorerBar.NormalGroupExpand.pressed = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.NormalGroupExpand.part, 3);
						}
						return VisualStyleElement.ExplorerBar.NormalGroupExpand.pressed;
					}
				}

				// Token: 0x04004632 RID: 17970
				private static readonly int part = 7;

				// Token: 0x04004633 RID: 17971
				private static VisualStyleElement normal;

				// Token: 0x04004634 RID: 17972
				private static VisualStyleElement hot;

				// Token: 0x04004635 RID: 17973
				private static VisualStyleElement pressed;
			}

			// Token: 0x020008FF RID: 2303
			public static class NormalGroupHead
			{
				// Token: 0x170019DF RID: 6623
				// (get) Token: 0x060073F3 RID: 29683 RVA: 0x001A6D97 File Offset: 0x001A4F97
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.NormalGroupHead.normal == null)
						{
							VisualStyleElement.ExplorerBar.NormalGroupHead.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.NormalGroupHead.part, 0);
						}
						return VisualStyleElement.ExplorerBar.NormalGroupHead.normal;
					}
				}

				// Token: 0x04004636 RID: 17974
				private static readonly int part = 8;

				// Token: 0x04004637 RID: 17975
				private static VisualStyleElement normal;
			}

			// Token: 0x02000900 RID: 2304
			public static class SpecialGroupBackground
			{
				// Token: 0x170019E0 RID: 6624
				// (get) Token: 0x060073F5 RID: 29685 RVA: 0x001A6DC2 File Offset: 0x001A4FC2
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.SpecialGroupBackground.normal == null)
						{
							VisualStyleElement.ExplorerBar.SpecialGroupBackground.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.SpecialGroupBackground.part, 0);
						}
						return VisualStyleElement.ExplorerBar.SpecialGroupBackground.normal;
					}
				}

				// Token: 0x04004638 RID: 17976
				private static readonly int part = 9;

				// Token: 0x04004639 RID: 17977
				private static VisualStyleElement normal;
			}

			// Token: 0x02000901 RID: 2305
			public static class SpecialGroupCollapse
			{
				// Token: 0x170019E1 RID: 6625
				// (get) Token: 0x060073F7 RID: 29687 RVA: 0x001A6DEE File Offset: 0x001A4FEE
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.SpecialGroupCollapse.normal == null)
						{
							VisualStyleElement.ExplorerBar.SpecialGroupCollapse.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.SpecialGroupCollapse.part, 1);
						}
						return VisualStyleElement.ExplorerBar.SpecialGroupCollapse.normal;
					}
				}

				// Token: 0x170019E2 RID: 6626
				// (get) Token: 0x060073F8 RID: 29688 RVA: 0x001A6E11 File Offset: 0x001A5011
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.SpecialGroupCollapse.hot == null)
						{
							VisualStyleElement.ExplorerBar.SpecialGroupCollapse.hot = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.SpecialGroupCollapse.part, 2);
						}
						return VisualStyleElement.ExplorerBar.SpecialGroupCollapse.hot;
					}
				}

				// Token: 0x170019E3 RID: 6627
				// (get) Token: 0x060073F9 RID: 29689 RVA: 0x001A6E34 File Offset: 0x001A5034
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.SpecialGroupCollapse.pressed == null)
						{
							VisualStyleElement.ExplorerBar.SpecialGroupCollapse.pressed = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.SpecialGroupCollapse.part, 3);
						}
						return VisualStyleElement.ExplorerBar.SpecialGroupCollapse.pressed;
					}
				}

				// Token: 0x0400463A RID: 17978
				private static readonly int part = 10;

				// Token: 0x0400463B RID: 17979
				private static VisualStyleElement normal;

				// Token: 0x0400463C RID: 17980
				private static VisualStyleElement hot;

				// Token: 0x0400463D RID: 17981
				private static VisualStyleElement pressed;
			}

			// Token: 0x02000902 RID: 2306
			public static class SpecialGroupExpand
			{
				// Token: 0x170019E4 RID: 6628
				// (get) Token: 0x060073FB RID: 29691 RVA: 0x001A6E60 File Offset: 0x001A5060
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.SpecialGroupExpand.normal == null)
						{
							VisualStyleElement.ExplorerBar.SpecialGroupExpand.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.SpecialGroupExpand.part, 1);
						}
						return VisualStyleElement.ExplorerBar.SpecialGroupExpand.normal;
					}
				}

				// Token: 0x170019E5 RID: 6629
				// (get) Token: 0x060073FC RID: 29692 RVA: 0x001A6E83 File Offset: 0x001A5083
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.SpecialGroupExpand.hot == null)
						{
							VisualStyleElement.ExplorerBar.SpecialGroupExpand.hot = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.SpecialGroupExpand.part, 2);
						}
						return VisualStyleElement.ExplorerBar.SpecialGroupExpand.hot;
					}
				}

				// Token: 0x170019E6 RID: 6630
				// (get) Token: 0x060073FD RID: 29693 RVA: 0x001A6EA6 File Offset: 0x001A50A6
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.SpecialGroupExpand.pressed == null)
						{
							VisualStyleElement.ExplorerBar.SpecialGroupExpand.pressed = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.SpecialGroupExpand.part, 3);
						}
						return VisualStyleElement.ExplorerBar.SpecialGroupExpand.pressed;
					}
				}

				// Token: 0x0400463E RID: 17982
				private static readonly int part = 11;

				// Token: 0x0400463F RID: 17983
				private static VisualStyleElement normal;

				// Token: 0x04004640 RID: 17984
				private static VisualStyleElement hot;

				// Token: 0x04004641 RID: 17985
				private static VisualStyleElement pressed;
			}

			// Token: 0x02000903 RID: 2307
			public static class SpecialGroupHead
			{
				// Token: 0x170019E7 RID: 6631
				// (get) Token: 0x060073FF RID: 29695 RVA: 0x001A6ED2 File Offset: 0x001A50D2
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.SpecialGroupHead.normal == null)
						{
							VisualStyleElement.ExplorerBar.SpecialGroupHead.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.SpecialGroupHead.part, 0);
						}
						return VisualStyleElement.ExplorerBar.SpecialGroupHead.normal;
					}
				}

				// Token: 0x04004642 RID: 17986
				private static readonly int part = 12;

				// Token: 0x04004643 RID: 17987
				private static VisualStyleElement normal;
			}
		}

		// Token: 0x0200083B RID: 2107
		public static class Header
		{
			// Token: 0x0400436C RID: 17260
			private static readonly string className = "HEADER";

			// Token: 0x02000904 RID: 2308
			public static class Item
			{
				// Token: 0x170019E8 RID: 6632
				// (get) Token: 0x06007401 RID: 29697 RVA: 0x001A6EFE File Offset: 0x001A50FE
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Header.Item.normal == null)
						{
							VisualStyleElement.Header.Item.normal = new VisualStyleElement(VisualStyleElement.Header.className, VisualStyleElement.Header.Item.part, 1);
						}
						return VisualStyleElement.Header.Item.normal;
					}
				}

				// Token: 0x170019E9 RID: 6633
				// (get) Token: 0x06007402 RID: 29698 RVA: 0x001A6F21 File Offset: 0x001A5121
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Header.Item.hot == null)
						{
							VisualStyleElement.Header.Item.hot = new VisualStyleElement(VisualStyleElement.Header.className, VisualStyleElement.Header.Item.part, 2);
						}
						return VisualStyleElement.Header.Item.hot;
					}
				}

				// Token: 0x170019EA RID: 6634
				// (get) Token: 0x06007403 RID: 29699 RVA: 0x001A6F44 File Offset: 0x001A5144
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Header.Item.pressed == null)
						{
							VisualStyleElement.Header.Item.pressed = new VisualStyleElement(VisualStyleElement.Header.className, VisualStyleElement.Header.Item.part, 3);
						}
						return VisualStyleElement.Header.Item.pressed;
					}
				}

				// Token: 0x04004644 RID: 17988
				private static readonly int part = 1;

				// Token: 0x04004645 RID: 17989
				private static VisualStyleElement normal;

				// Token: 0x04004646 RID: 17990
				private static VisualStyleElement hot;

				// Token: 0x04004647 RID: 17991
				private static VisualStyleElement pressed;
			}

			// Token: 0x02000905 RID: 2309
			public static class ItemLeft
			{
				// Token: 0x170019EB RID: 6635
				// (get) Token: 0x06007405 RID: 29701 RVA: 0x001A6F6F File Offset: 0x001A516F
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Header.ItemLeft.normal == null)
						{
							VisualStyleElement.Header.ItemLeft.normal = new VisualStyleElement(VisualStyleElement.Header.className, VisualStyleElement.Header.ItemLeft.part, 1);
						}
						return VisualStyleElement.Header.ItemLeft.normal;
					}
				}

				// Token: 0x170019EC RID: 6636
				// (get) Token: 0x06007406 RID: 29702 RVA: 0x001A6F92 File Offset: 0x001A5192
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Header.ItemLeft.hot == null)
						{
							VisualStyleElement.Header.ItemLeft.hot = new VisualStyleElement(VisualStyleElement.Header.className, VisualStyleElement.Header.ItemLeft.part, 2);
						}
						return VisualStyleElement.Header.ItemLeft.hot;
					}
				}

				// Token: 0x170019ED RID: 6637
				// (get) Token: 0x06007407 RID: 29703 RVA: 0x001A6FB5 File Offset: 0x001A51B5
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Header.ItemLeft.pressed == null)
						{
							VisualStyleElement.Header.ItemLeft.pressed = new VisualStyleElement(VisualStyleElement.Header.className, VisualStyleElement.Header.ItemLeft.part, 3);
						}
						return VisualStyleElement.Header.ItemLeft.pressed;
					}
				}

				// Token: 0x04004648 RID: 17992
				private static readonly int part = 2;

				// Token: 0x04004649 RID: 17993
				private static VisualStyleElement normal;

				// Token: 0x0400464A RID: 17994
				private static VisualStyleElement hot;

				// Token: 0x0400464B RID: 17995
				private static VisualStyleElement pressed;
			}

			// Token: 0x02000906 RID: 2310
			public static class ItemRight
			{
				// Token: 0x170019EE RID: 6638
				// (get) Token: 0x06007409 RID: 29705 RVA: 0x001A6FE0 File Offset: 0x001A51E0
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Header.ItemRight.normal == null)
						{
							VisualStyleElement.Header.ItemRight.normal = new VisualStyleElement(VisualStyleElement.Header.className, VisualStyleElement.Header.ItemRight.part, 1);
						}
						return VisualStyleElement.Header.ItemRight.normal;
					}
				}

				// Token: 0x170019EF RID: 6639
				// (get) Token: 0x0600740A RID: 29706 RVA: 0x001A7003 File Offset: 0x001A5203
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Header.ItemRight.hot == null)
						{
							VisualStyleElement.Header.ItemRight.hot = new VisualStyleElement(VisualStyleElement.Header.className, VisualStyleElement.Header.ItemRight.part, 2);
						}
						return VisualStyleElement.Header.ItemRight.hot;
					}
				}

				// Token: 0x170019F0 RID: 6640
				// (get) Token: 0x0600740B RID: 29707 RVA: 0x001A7026 File Offset: 0x001A5226
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Header.ItemRight.pressed == null)
						{
							VisualStyleElement.Header.ItemRight.pressed = new VisualStyleElement(VisualStyleElement.Header.className, VisualStyleElement.Header.ItemRight.part, 3);
						}
						return VisualStyleElement.Header.ItemRight.pressed;
					}
				}

				// Token: 0x0400464C RID: 17996
				private static readonly int part = 3;

				// Token: 0x0400464D RID: 17997
				private static VisualStyleElement normal;

				// Token: 0x0400464E RID: 17998
				private static VisualStyleElement hot;

				// Token: 0x0400464F RID: 17999
				private static VisualStyleElement pressed;
			}

			// Token: 0x02000907 RID: 2311
			public static class SortArrow
			{
				// Token: 0x170019F1 RID: 6641
				// (get) Token: 0x0600740D RID: 29709 RVA: 0x001A7051 File Offset: 0x001A5251
				public static VisualStyleElement SortedUp
				{
					get
					{
						if (VisualStyleElement.Header.SortArrow.sortedup == null)
						{
							VisualStyleElement.Header.SortArrow.sortedup = new VisualStyleElement(VisualStyleElement.Header.className, VisualStyleElement.Header.SortArrow.part, 1);
						}
						return VisualStyleElement.Header.SortArrow.sortedup;
					}
				}

				// Token: 0x170019F2 RID: 6642
				// (get) Token: 0x0600740E RID: 29710 RVA: 0x001A7074 File Offset: 0x001A5274
				public static VisualStyleElement SortedDown
				{
					get
					{
						if (VisualStyleElement.Header.SortArrow.sorteddown == null)
						{
							VisualStyleElement.Header.SortArrow.sorteddown = new VisualStyleElement(VisualStyleElement.Header.className, VisualStyleElement.Header.SortArrow.part, 2);
						}
						return VisualStyleElement.Header.SortArrow.sorteddown;
					}
				}

				// Token: 0x04004650 RID: 18000
				private static readonly int part = 4;

				// Token: 0x04004651 RID: 18001
				private static VisualStyleElement sortedup;

				// Token: 0x04004652 RID: 18002
				private static VisualStyleElement sorteddown;
			}
		}

		// Token: 0x0200083C RID: 2108
		public static class ListView
		{
			// Token: 0x0400436D RID: 17261
			private static readonly string className = "LISTVIEW";

			// Token: 0x02000908 RID: 2312
			public static class Item
			{
				// Token: 0x170019F3 RID: 6643
				// (get) Token: 0x06007410 RID: 29712 RVA: 0x001A709F File Offset: 0x001A529F
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ListView.Item.normal == null)
						{
							VisualStyleElement.ListView.Item.normal = new VisualStyleElement(VisualStyleElement.ListView.className, VisualStyleElement.ListView.Item.part, 1);
						}
						return VisualStyleElement.ListView.Item.normal;
					}
				}

				// Token: 0x170019F4 RID: 6644
				// (get) Token: 0x06007411 RID: 29713 RVA: 0x001A70C2 File Offset: 0x001A52C2
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ListView.Item.hot == null)
						{
							VisualStyleElement.ListView.Item.hot = new VisualStyleElement(VisualStyleElement.ListView.className, VisualStyleElement.ListView.Item.part, 2);
						}
						return VisualStyleElement.ListView.Item.hot;
					}
				}

				// Token: 0x170019F5 RID: 6645
				// (get) Token: 0x06007412 RID: 29714 RVA: 0x001A70E5 File Offset: 0x001A52E5
				public static VisualStyleElement Selected
				{
					get
					{
						if (VisualStyleElement.ListView.Item.selected == null)
						{
							VisualStyleElement.ListView.Item.selected = new VisualStyleElement(VisualStyleElement.ListView.className, VisualStyleElement.ListView.Item.part, 3);
						}
						return VisualStyleElement.ListView.Item.selected;
					}
				}

				// Token: 0x170019F6 RID: 6646
				// (get) Token: 0x06007413 RID: 29715 RVA: 0x001A7108 File Offset: 0x001A5308
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ListView.Item.disabled == null)
						{
							VisualStyleElement.ListView.Item.disabled = new VisualStyleElement(VisualStyleElement.ListView.className, VisualStyleElement.ListView.Item.part, 4);
						}
						return VisualStyleElement.ListView.Item.disabled;
					}
				}

				// Token: 0x170019F7 RID: 6647
				// (get) Token: 0x06007414 RID: 29716 RVA: 0x001A712B File Offset: 0x001A532B
				public static VisualStyleElement SelectedNotFocus
				{
					get
					{
						if (VisualStyleElement.ListView.Item.selectednotfocus == null)
						{
							VisualStyleElement.ListView.Item.selectednotfocus = new VisualStyleElement(VisualStyleElement.ListView.className, VisualStyleElement.ListView.Item.part, 5);
						}
						return VisualStyleElement.ListView.Item.selectednotfocus;
					}
				}

				// Token: 0x04004653 RID: 18003
				private static readonly int part = 1;

				// Token: 0x04004654 RID: 18004
				private static VisualStyleElement normal;

				// Token: 0x04004655 RID: 18005
				private static VisualStyleElement hot;

				// Token: 0x04004656 RID: 18006
				private static VisualStyleElement selected;

				// Token: 0x04004657 RID: 18007
				private static VisualStyleElement disabled;

				// Token: 0x04004658 RID: 18008
				private static VisualStyleElement selectednotfocus;
			}

			// Token: 0x02000909 RID: 2313
			public static class Group
			{
				// Token: 0x170019F8 RID: 6648
				// (get) Token: 0x06007416 RID: 29718 RVA: 0x001A7156 File Offset: 0x001A5356
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ListView.Group.normal == null)
						{
							VisualStyleElement.ListView.Group.normal = new VisualStyleElement(VisualStyleElement.ListView.className, VisualStyleElement.ListView.Group.part, 0);
						}
						return VisualStyleElement.ListView.Group.normal;
					}
				}

				// Token: 0x04004659 RID: 18009
				private static readonly int part = 2;

				// Token: 0x0400465A RID: 18010
				private static VisualStyleElement normal;
			}

			// Token: 0x0200090A RID: 2314
			public static class Detail
			{
				// Token: 0x170019F9 RID: 6649
				// (get) Token: 0x06007418 RID: 29720 RVA: 0x001A7181 File Offset: 0x001A5381
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ListView.Detail.normal == null)
						{
							VisualStyleElement.ListView.Detail.normal = new VisualStyleElement(VisualStyleElement.ListView.className, VisualStyleElement.ListView.Detail.part, 0);
						}
						return VisualStyleElement.ListView.Detail.normal;
					}
				}

				// Token: 0x0400465B RID: 18011
				private static readonly int part = 3;

				// Token: 0x0400465C RID: 18012
				private static VisualStyleElement normal;
			}

			// Token: 0x0200090B RID: 2315
			public static class SortedDetail
			{
				// Token: 0x170019FA RID: 6650
				// (get) Token: 0x0600741A RID: 29722 RVA: 0x001A71AC File Offset: 0x001A53AC
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ListView.SortedDetail.normal == null)
						{
							VisualStyleElement.ListView.SortedDetail.normal = new VisualStyleElement(VisualStyleElement.ListView.className, VisualStyleElement.ListView.SortedDetail.part, 0);
						}
						return VisualStyleElement.ListView.SortedDetail.normal;
					}
				}

				// Token: 0x0400465D RID: 18013
				private static readonly int part = 4;

				// Token: 0x0400465E RID: 18014
				private static VisualStyleElement normal;
			}

			// Token: 0x0200090C RID: 2316
			public static class EmptyText
			{
				// Token: 0x170019FB RID: 6651
				// (get) Token: 0x0600741C RID: 29724 RVA: 0x001A71D7 File Offset: 0x001A53D7
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ListView.EmptyText.normal == null)
						{
							VisualStyleElement.ListView.EmptyText.normal = new VisualStyleElement(VisualStyleElement.ListView.className, VisualStyleElement.ListView.EmptyText.part, 0);
						}
						return VisualStyleElement.ListView.EmptyText.normal;
					}
				}

				// Token: 0x0400465F RID: 18015
				private static readonly int part = 5;

				// Token: 0x04004660 RID: 18016
				private static VisualStyleElement normal;
			}
		}

		// Token: 0x0200083D RID: 2109
		public static class MenuBand
		{
			// Token: 0x0400436E RID: 17262
			private static readonly string className = "MENUBAND";

			// Token: 0x0200090D RID: 2317
			public static class NewApplicationButton
			{
				// Token: 0x170019FC RID: 6652
				// (get) Token: 0x0600741E RID: 29726 RVA: 0x001A7202 File Offset: 0x001A5402
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.MenuBand.NewApplicationButton.normal == null)
						{
							VisualStyleElement.MenuBand.NewApplicationButton.normal = new VisualStyleElement(VisualStyleElement.MenuBand.className, VisualStyleElement.MenuBand.NewApplicationButton.part, 1);
						}
						return VisualStyleElement.MenuBand.NewApplicationButton.normal;
					}
				}

				// Token: 0x170019FD RID: 6653
				// (get) Token: 0x0600741F RID: 29727 RVA: 0x001A7225 File Offset: 0x001A5425
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.MenuBand.NewApplicationButton.hot == null)
						{
							VisualStyleElement.MenuBand.NewApplicationButton.hot = new VisualStyleElement(VisualStyleElement.MenuBand.className, VisualStyleElement.MenuBand.NewApplicationButton.part, 2);
						}
						return VisualStyleElement.MenuBand.NewApplicationButton.hot;
					}
				}

				// Token: 0x170019FE RID: 6654
				// (get) Token: 0x06007420 RID: 29728 RVA: 0x001A7248 File Offset: 0x001A5448
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.MenuBand.NewApplicationButton.pressed == null)
						{
							VisualStyleElement.MenuBand.NewApplicationButton.pressed = new VisualStyleElement(VisualStyleElement.MenuBand.className, VisualStyleElement.MenuBand.NewApplicationButton.part, 3);
						}
						return VisualStyleElement.MenuBand.NewApplicationButton.pressed;
					}
				}

				// Token: 0x170019FF RID: 6655
				// (get) Token: 0x06007421 RID: 29729 RVA: 0x001A726B File Offset: 0x001A546B
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.MenuBand.NewApplicationButton.disabled == null)
						{
							VisualStyleElement.MenuBand.NewApplicationButton.disabled = new VisualStyleElement(VisualStyleElement.MenuBand.className, VisualStyleElement.MenuBand.NewApplicationButton.part, 4);
						}
						return VisualStyleElement.MenuBand.NewApplicationButton.disabled;
					}
				}

				// Token: 0x17001A00 RID: 6656
				// (get) Token: 0x06007422 RID: 29730 RVA: 0x001A728E File Offset: 0x001A548E
				public static VisualStyleElement Checked
				{
					get
					{
						if (VisualStyleElement.MenuBand.NewApplicationButton._checked == null)
						{
							VisualStyleElement.MenuBand.NewApplicationButton._checked = new VisualStyleElement(VisualStyleElement.MenuBand.className, VisualStyleElement.MenuBand.NewApplicationButton.part, 5);
						}
						return VisualStyleElement.MenuBand.NewApplicationButton._checked;
					}
				}

				// Token: 0x17001A01 RID: 6657
				// (get) Token: 0x06007423 RID: 29731 RVA: 0x001A72B1 File Offset: 0x001A54B1
				public static VisualStyleElement HotChecked
				{
					get
					{
						if (VisualStyleElement.MenuBand.NewApplicationButton.hotchecked == null)
						{
							VisualStyleElement.MenuBand.NewApplicationButton.hotchecked = new VisualStyleElement(VisualStyleElement.MenuBand.className, VisualStyleElement.MenuBand.NewApplicationButton.part, 6);
						}
						return VisualStyleElement.MenuBand.NewApplicationButton.hotchecked;
					}
				}

				// Token: 0x04004661 RID: 18017
				private static readonly int part = 1;

				// Token: 0x04004662 RID: 18018
				private static VisualStyleElement normal;

				// Token: 0x04004663 RID: 18019
				private static VisualStyleElement hot;

				// Token: 0x04004664 RID: 18020
				private static VisualStyleElement pressed;

				// Token: 0x04004665 RID: 18021
				private static VisualStyleElement disabled;

				// Token: 0x04004666 RID: 18022
				private static VisualStyleElement _checked;

				// Token: 0x04004667 RID: 18023
				private static VisualStyleElement hotchecked;
			}

			// Token: 0x0200090E RID: 2318
			public static class Separator
			{
				// Token: 0x17001A02 RID: 6658
				// (get) Token: 0x06007425 RID: 29733 RVA: 0x001A72DC File Offset: 0x001A54DC
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.MenuBand.Separator.normal == null)
						{
							VisualStyleElement.MenuBand.Separator.normal = new VisualStyleElement(VisualStyleElement.MenuBand.className, VisualStyleElement.MenuBand.Separator.part, 0);
						}
						return VisualStyleElement.MenuBand.Separator.normal;
					}
				}

				// Token: 0x04004668 RID: 18024
				private static readonly int part = 2;

				// Token: 0x04004669 RID: 18025
				private static VisualStyleElement normal;
			}
		}

		// Token: 0x0200083E RID: 2110
		public static class Menu
		{
			// Token: 0x0400436F RID: 17263
			private static readonly string className = "MENU";

			// Token: 0x0200090F RID: 2319
			public static class Item
			{
				// Token: 0x17001A03 RID: 6659
				// (get) Token: 0x06007427 RID: 29735 RVA: 0x001A7307 File Offset: 0x001A5507
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Menu.Item.normal == null)
						{
							VisualStyleElement.Menu.Item.normal = new VisualStyleElement(VisualStyleElement.Menu.className, VisualStyleElement.Menu.Item.part, 1);
						}
						return VisualStyleElement.Menu.Item.normal;
					}
				}

				// Token: 0x17001A04 RID: 6660
				// (get) Token: 0x06007428 RID: 29736 RVA: 0x001A732A File Offset: 0x001A552A
				public static VisualStyleElement Selected
				{
					get
					{
						if (VisualStyleElement.Menu.Item.selected == null)
						{
							VisualStyleElement.Menu.Item.selected = new VisualStyleElement(VisualStyleElement.Menu.className, VisualStyleElement.Menu.Item.part, 2);
						}
						return VisualStyleElement.Menu.Item.selected;
					}
				}

				// Token: 0x17001A05 RID: 6661
				// (get) Token: 0x06007429 RID: 29737 RVA: 0x001A734D File Offset: 0x001A554D
				public static VisualStyleElement Demoted
				{
					get
					{
						if (VisualStyleElement.Menu.Item.demoted == null)
						{
							VisualStyleElement.Menu.Item.demoted = new VisualStyleElement(VisualStyleElement.Menu.className, VisualStyleElement.Menu.Item.part, 3);
						}
						return VisualStyleElement.Menu.Item.demoted;
					}
				}

				// Token: 0x0400466A RID: 18026
				private static readonly int part = 1;

				// Token: 0x0400466B RID: 18027
				private static VisualStyleElement normal;

				// Token: 0x0400466C RID: 18028
				private static VisualStyleElement selected;

				// Token: 0x0400466D RID: 18029
				private static VisualStyleElement demoted;
			}

			// Token: 0x02000910 RID: 2320
			public static class DropDown
			{
				// Token: 0x17001A06 RID: 6662
				// (get) Token: 0x0600742B RID: 29739 RVA: 0x001A7378 File Offset: 0x001A5578
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Menu.DropDown.normal == null)
						{
							VisualStyleElement.Menu.DropDown.normal = new VisualStyleElement(VisualStyleElement.Menu.className, VisualStyleElement.Menu.DropDown.part, 0);
						}
						return VisualStyleElement.Menu.DropDown.normal;
					}
				}

				// Token: 0x0400466E RID: 18030
				private static readonly int part = 2;

				// Token: 0x0400466F RID: 18031
				private static VisualStyleElement normal;
			}

			// Token: 0x02000911 RID: 2321
			public static class BarItem
			{
				// Token: 0x17001A07 RID: 6663
				// (get) Token: 0x0600742D RID: 29741 RVA: 0x001A73A3 File Offset: 0x001A55A3
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Menu.BarItem.normal == null)
						{
							VisualStyleElement.Menu.BarItem.normal = new VisualStyleElement(VisualStyleElement.Menu.className, VisualStyleElement.Menu.BarItem.part, 0);
						}
						return VisualStyleElement.Menu.BarItem.normal;
					}
				}

				// Token: 0x04004670 RID: 18032
				private static readonly int part = 3;

				// Token: 0x04004671 RID: 18033
				private static VisualStyleElement normal;
			}

			// Token: 0x02000912 RID: 2322
			public static class BarDropDown
			{
				// Token: 0x17001A08 RID: 6664
				// (get) Token: 0x0600742F RID: 29743 RVA: 0x001A73CE File Offset: 0x001A55CE
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Menu.BarDropDown.normal == null)
						{
							VisualStyleElement.Menu.BarDropDown.normal = new VisualStyleElement(VisualStyleElement.Menu.className, VisualStyleElement.Menu.BarDropDown.part, 0);
						}
						return VisualStyleElement.Menu.BarDropDown.normal;
					}
				}

				// Token: 0x04004672 RID: 18034
				private static readonly int part = 4;

				// Token: 0x04004673 RID: 18035
				private static VisualStyleElement normal;
			}

			// Token: 0x02000913 RID: 2323
			public static class Chevron
			{
				// Token: 0x17001A09 RID: 6665
				// (get) Token: 0x06007431 RID: 29745 RVA: 0x001A73F9 File Offset: 0x001A55F9
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Menu.Chevron.normal == null)
						{
							VisualStyleElement.Menu.Chevron.normal = new VisualStyleElement(VisualStyleElement.Menu.className, VisualStyleElement.Menu.Chevron.part, 0);
						}
						return VisualStyleElement.Menu.Chevron.normal;
					}
				}

				// Token: 0x04004674 RID: 18036
				private static readonly int part = 5;

				// Token: 0x04004675 RID: 18037
				private static VisualStyleElement normal;
			}

			// Token: 0x02000914 RID: 2324
			public static class Separator
			{
				// Token: 0x17001A0A RID: 6666
				// (get) Token: 0x06007433 RID: 29747 RVA: 0x001A7424 File Offset: 0x001A5624
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Menu.Separator.normal == null)
						{
							VisualStyleElement.Menu.Separator.normal = new VisualStyleElement(VisualStyleElement.Menu.className, VisualStyleElement.Menu.Separator.part, 0);
						}
						return VisualStyleElement.Menu.Separator.normal;
					}
				}

				// Token: 0x04004676 RID: 18038
				private static readonly int part = 6;

				// Token: 0x04004677 RID: 18039
				private static VisualStyleElement normal;
			}
		}

		// Token: 0x0200083F RID: 2111
		public static class ProgressBar
		{
			// Token: 0x04004370 RID: 17264
			private static readonly string className = "PROGRESS";

			// Token: 0x02000915 RID: 2325
			public static class Bar
			{
				// Token: 0x17001A0B RID: 6667
				// (get) Token: 0x06007435 RID: 29749 RVA: 0x001A744F File Offset: 0x001A564F
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ProgressBar.Bar.normal == null)
						{
							VisualStyleElement.ProgressBar.Bar.normal = new VisualStyleElement(VisualStyleElement.ProgressBar.className, VisualStyleElement.ProgressBar.Bar.part, 0);
						}
						return VisualStyleElement.ProgressBar.Bar.normal;
					}
				}

				// Token: 0x04004678 RID: 18040
				private static readonly int part = 1;

				// Token: 0x04004679 RID: 18041
				private static VisualStyleElement normal;
			}

			// Token: 0x02000916 RID: 2326
			public static class BarVertical
			{
				// Token: 0x17001A0C RID: 6668
				// (get) Token: 0x06007437 RID: 29751 RVA: 0x001A747A File Offset: 0x001A567A
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ProgressBar.BarVertical.normal == null)
						{
							VisualStyleElement.ProgressBar.BarVertical.normal = new VisualStyleElement(VisualStyleElement.ProgressBar.className, VisualStyleElement.ProgressBar.BarVertical.part, 0);
						}
						return VisualStyleElement.ProgressBar.BarVertical.normal;
					}
				}

				// Token: 0x0400467A RID: 18042
				private static readonly int part = 2;

				// Token: 0x0400467B RID: 18043
				private static VisualStyleElement normal;
			}

			// Token: 0x02000917 RID: 2327
			public static class Chunk
			{
				// Token: 0x17001A0D RID: 6669
				// (get) Token: 0x06007439 RID: 29753 RVA: 0x001A74A5 File Offset: 0x001A56A5
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ProgressBar.Chunk.normal == null)
						{
							VisualStyleElement.ProgressBar.Chunk.normal = new VisualStyleElement(VisualStyleElement.ProgressBar.className, VisualStyleElement.ProgressBar.Chunk.part, 0);
						}
						return VisualStyleElement.ProgressBar.Chunk.normal;
					}
				}

				// Token: 0x0400467C RID: 18044
				private static readonly int part = 3;

				// Token: 0x0400467D RID: 18045
				private static VisualStyleElement normal;
			}

			// Token: 0x02000918 RID: 2328
			public static class ChunkVertical
			{
				// Token: 0x17001A0E RID: 6670
				// (get) Token: 0x0600743B RID: 29755 RVA: 0x001A74D0 File Offset: 0x001A56D0
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ProgressBar.ChunkVertical.normal == null)
						{
							VisualStyleElement.ProgressBar.ChunkVertical.normal = new VisualStyleElement(VisualStyleElement.ProgressBar.className, VisualStyleElement.ProgressBar.ChunkVertical.part, 0);
						}
						return VisualStyleElement.ProgressBar.ChunkVertical.normal;
					}
				}

				// Token: 0x0400467E RID: 18046
				private static readonly int part = 4;

				// Token: 0x0400467F RID: 18047
				private static VisualStyleElement normal;
			}
		}

		// Token: 0x02000840 RID: 2112
		public static class Rebar
		{
			// Token: 0x04004371 RID: 17265
			private static readonly string className = "REBAR";

			// Token: 0x02000919 RID: 2329
			public static class Gripper
			{
				// Token: 0x17001A0F RID: 6671
				// (get) Token: 0x0600743D RID: 29757 RVA: 0x001A74FB File Offset: 0x001A56FB
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Rebar.Gripper.normal == null)
						{
							VisualStyleElement.Rebar.Gripper.normal = new VisualStyleElement(VisualStyleElement.Rebar.className, VisualStyleElement.Rebar.Gripper.part, 0);
						}
						return VisualStyleElement.Rebar.Gripper.normal;
					}
				}

				// Token: 0x04004680 RID: 18048
				private static readonly int part = 1;

				// Token: 0x04004681 RID: 18049
				private static VisualStyleElement normal;
			}

			// Token: 0x0200091A RID: 2330
			public static class GripperVertical
			{
				// Token: 0x17001A10 RID: 6672
				// (get) Token: 0x0600743F RID: 29759 RVA: 0x001A7526 File Offset: 0x001A5726
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Rebar.GripperVertical.normal == null)
						{
							VisualStyleElement.Rebar.GripperVertical.normal = new VisualStyleElement(VisualStyleElement.Rebar.className, VisualStyleElement.Rebar.GripperVertical.part, 0);
						}
						return VisualStyleElement.Rebar.GripperVertical.normal;
					}
				}

				// Token: 0x04004682 RID: 18050
				private static readonly int part = 2;

				// Token: 0x04004683 RID: 18051
				private static VisualStyleElement normal;
			}

			// Token: 0x0200091B RID: 2331
			public static class Band
			{
				// Token: 0x17001A11 RID: 6673
				// (get) Token: 0x06007441 RID: 29761 RVA: 0x001A7551 File Offset: 0x001A5751
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Rebar.Band.normal == null)
						{
							VisualStyleElement.Rebar.Band.normal = new VisualStyleElement(VisualStyleElement.Rebar.className, VisualStyleElement.Rebar.Band.part, 0);
						}
						return VisualStyleElement.Rebar.Band.normal;
					}
				}

				// Token: 0x04004684 RID: 18052
				private static readonly int part = 3;

				// Token: 0x04004685 RID: 18053
				private static VisualStyleElement normal;
			}

			// Token: 0x0200091C RID: 2332
			public static class Chevron
			{
				// Token: 0x17001A12 RID: 6674
				// (get) Token: 0x06007443 RID: 29763 RVA: 0x001A757C File Offset: 0x001A577C
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Rebar.Chevron.normal == null)
						{
							VisualStyleElement.Rebar.Chevron.normal = new VisualStyleElement(VisualStyleElement.Rebar.className, VisualStyleElement.Rebar.Chevron.part, 1);
						}
						return VisualStyleElement.Rebar.Chevron.normal;
					}
				}

				// Token: 0x17001A13 RID: 6675
				// (get) Token: 0x06007444 RID: 29764 RVA: 0x001A759F File Offset: 0x001A579F
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Rebar.Chevron.hot == null)
						{
							VisualStyleElement.Rebar.Chevron.hot = new VisualStyleElement(VisualStyleElement.Rebar.className, VisualStyleElement.Rebar.Chevron.part, 2);
						}
						return VisualStyleElement.Rebar.Chevron.hot;
					}
				}

				// Token: 0x17001A14 RID: 6676
				// (get) Token: 0x06007445 RID: 29765 RVA: 0x001A75C2 File Offset: 0x001A57C2
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Rebar.Chevron.pressed == null)
						{
							VisualStyleElement.Rebar.Chevron.pressed = new VisualStyleElement(VisualStyleElement.Rebar.className, VisualStyleElement.Rebar.Chevron.part, 3);
						}
						return VisualStyleElement.Rebar.Chevron.pressed;
					}
				}

				// Token: 0x04004686 RID: 18054
				private static readonly int part = 4;

				// Token: 0x04004687 RID: 18055
				private static VisualStyleElement normal;

				// Token: 0x04004688 RID: 18056
				private static VisualStyleElement hot;

				// Token: 0x04004689 RID: 18057
				private static VisualStyleElement pressed;
			}

			// Token: 0x0200091D RID: 2333
			public static class ChevronVertical
			{
				// Token: 0x17001A15 RID: 6677
				// (get) Token: 0x06007447 RID: 29767 RVA: 0x001A75ED File Offset: 0x001A57ED
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Rebar.ChevronVertical.normal == null)
						{
							VisualStyleElement.Rebar.ChevronVertical.normal = new VisualStyleElement(VisualStyleElement.Rebar.className, VisualStyleElement.Rebar.ChevronVertical.part, 1);
						}
						return VisualStyleElement.Rebar.ChevronVertical.normal;
					}
				}

				// Token: 0x17001A16 RID: 6678
				// (get) Token: 0x06007448 RID: 29768 RVA: 0x001A7610 File Offset: 0x001A5810
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Rebar.ChevronVertical.hot == null)
						{
							VisualStyleElement.Rebar.ChevronVertical.hot = new VisualStyleElement(VisualStyleElement.Rebar.className, VisualStyleElement.Rebar.ChevronVertical.part, 2);
						}
						return VisualStyleElement.Rebar.ChevronVertical.hot;
					}
				}

				// Token: 0x17001A17 RID: 6679
				// (get) Token: 0x06007449 RID: 29769 RVA: 0x001A7633 File Offset: 0x001A5833
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Rebar.ChevronVertical.pressed == null)
						{
							VisualStyleElement.Rebar.ChevronVertical.pressed = new VisualStyleElement(VisualStyleElement.Rebar.className, VisualStyleElement.Rebar.ChevronVertical.part, 3);
						}
						return VisualStyleElement.Rebar.ChevronVertical.pressed;
					}
				}

				// Token: 0x0400468A RID: 18058
				private static readonly int part = 5;

				// Token: 0x0400468B RID: 18059
				private static VisualStyleElement normal;

				// Token: 0x0400468C RID: 18060
				private static VisualStyleElement hot;

				// Token: 0x0400468D RID: 18061
				private static VisualStyleElement pressed;
			}
		}

		// Token: 0x02000841 RID: 2113
		public static class StartPanel
		{
			// Token: 0x04004372 RID: 17266
			private static readonly string className = "STARTPANEL";

			// Token: 0x0200091E RID: 2334
			public static class UserPane
			{
				// Token: 0x17001A18 RID: 6680
				// (get) Token: 0x0600744B RID: 29771 RVA: 0x001A765E File Offset: 0x001A585E
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.StartPanel.UserPane.normal == null)
						{
							VisualStyleElement.StartPanel.UserPane.normal = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.UserPane.part, 0);
						}
						return VisualStyleElement.StartPanel.UserPane.normal;
					}
				}

				// Token: 0x0400468E RID: 18062
				private static readonly int part = 1;

				// Token: 0x0400468F RID: 18063
				private static VisualStyleElement normal;
			}

			// Token: 0x0200091F RID: 2335
			public static class MorePrograms
			{
				// Token: 0x17001A19 RID: 6681
				// (get) Token: 0x0600744D RID: 29773 RVA: 0x001A7689 File Offset: 0x001A5889
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.StartPanel.MorePrograms.normal == null)
						{
							VisualStyleElement.StartPanel.MorePrograms.normal = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.MorePrograms.part, 0);
						}
						return VisualStyleElement.StartPanel.MorePrograms.normal;
					}
				}

				// Token: 0x04004690 RID: 18064
				private static readonly int part = 2;

				// Token: 0x04004691 RID: 18065
				private static VisualStyleElement normal;
			}

			// Token: 0x02000920 RID: 2336
			public static class MoreProgramsArrow
			{
				// Token: 0x17001A1A RID: 6682
				// (get) Token: 0x0600744F RID: 29775 RVA: 0x001A76B4 File Offset: 0x001A58B4
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.StartPanel.MoreProgramsArrow.normal == null)
						{
							VisualStyleElement.StartPanel.MoreProgramsArrow.normal = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.MoreProgramsArrow.part, 1);
						}
						return VisualStyleElement.StartPanel.MoreProgramsArrow.normal;
					}
				}

				// Token: 0x17001A1B RID: 6683
				// (get) Token: 0x06007450 RID: 29776 RVA: 0x001A76D7 File Offset: 0x001A58D7
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.StartPanel.MoreProgramsArrow.hot == null)
						{
							VisualStyleElement.StartPanel.MoreProgramsArrow.hot = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.MoreProgramsArrow.part, 2);
						}
						return VisualStyleElement.StartPanel.MoreProgramsArrow.hot;
					}
				}

				// Token: 0x17001A1C RID: 6684
				// (get) Token: 0x06007451 RID: 29777 RVA: 0x001A76FA File Offset: 0x001A58FA
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.StartPanel.MoreProgramsArrow.pressed == null)
						{
							VisualStyleElement.StartPanel.MoreProgramsArrow.pressed = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.MoreProgramsArrow.part, 3);
						}
						return VisualStyleElement.StartPanel.MoreProgramsArrow.pressed;
					}
				}

				// Token: 0x04004692 RID: 18066
				private static readonly int part = 3;

				// Token: 0x04004693 RID: 18067
				private static VisualStyleElement normal;

				// Token: 0x04004694 RID: 18068
				private static VisualStyleElement hot;

				// Token: 0x04004695 RID: 18069
				private static VisualStyleElement pressed;
			}

			// Token: 0x02000921 RID: 2337
			public static class ProgList
			{
				// Token: 0x17001A1D RID: 6685
				// (get) Token: 0x06007453 RID: 29779 RVA: 0x001A7725 File Offset: 0x001A5925
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.StartPanel.ProgList.normal == null)
						{
							VisualStyleElement.StartPanel.ProgList.normal = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.ProgList.part, 0);
						}
						return VisualStyleElement.StartPanel.ProgList.normal;
					}
				}

				// Token: 0x04004696 RID: 18070
				private static readonly int part = 4;

				// Token: 0x04004697 RID: 18071
				private static VisualStyleElement normal;
			}

			// Token: 0x02000922 RID: 2338
			public static class ProgListSeparator
			{
				// Token: 0x17001A1E RID: 6686
				// (get) Token: 0x06007455 RID: 29781 RVA: 0x001A7750 File Offset: 0x001A5950
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.StartPanel.ProgListSeparator.normal == null)
						{
							VisualStyleElement.StartPanel.ProgListSeparator.normal = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.ProgListSeparator.part, 0);
						}
						return VisualStyleElement.StartPanel.ProgListSeparator.normal;
					}
				}

				// Token: 0x04004698 RID: 18072
				private static readonly int part = 5;

				// Token: 0x04004699 RID: 18073
				private static VisualStyleElement normal;
			}

			// Token: 0x02000923 RID: 2339
			public static class PlaceList
			{
				// Token: 0x17001A1F RID: 6687
				// (get) Token: 0x06007457 RID: 29783 RVA: 0x001A777B File Offset: 0x001A597B
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.StartPanel.PlaceList.normal == null)
						{
							VisualStyleElement.StartPanel.PlaceList.normal = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.PlaceList.part, 0);
						}
						return VisualStyleElement.StartPanel.PlaceList.normal;
					}
				}

				// Token: 0x0400469A RID: 18074
				private static readonly int part = 6;

				// Token: 0x0400469B RID: 18075
				private static VisualStyleElement normal;
			}

			// Token: 0x02000924 RID: 2340
			public static class PlaceListSeparator
			{
				// Token: 0x17001A20 RID: 6688
				// (get) Token: 0x06007459 RID: 29785 RVA: 0x001A77A6 File Offset: 0x001A59A6
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.StartPanel.PlaceListSeparator.normal == null)
						{
							VisualStyleElement.StartPanel.PlaceListSeparator.normal = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.PlaceListSeparator.part, 0);
						}
						return VisualStyleElement.StartPanel.PlaceListSeparator.normal;
					}
				}

				// Token: 0x0400469C RID: 18076
				private static readonly int part = 7;

				// Token: 0x0400469D RID: 18077
				private static VisualStyleElement normal;
			}

			// Token: 0x02000925 RID: 2341
			public static class LogOff
			{
				// Token: 0x17001A21 RID: 6689
				// (get) Token: 0x0600745B RID: 29787 RVA: 0x001A77D1 File Offset: 0x001A59D1
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.StartPanel.LogOff.normal == null)
						{
							VisualStyleElement.StartPanel.LogOff.normal = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.LogOff.part, 0);
						}
						return VisualStyleElement.StartPanel.LogOff.normal;
					}
				}

				// Token: 0x0400469E RID: 18078
				private static readonly int part = 8;

				// Token: 0x0400469F RID: 18079
				private static VisualStyleElement normal;
			}

			// Token: 0x02000926 RID: 2342
			public static class LogOffButtons
			{
				// Token: 0x17001A22 RID: 6690
				// (get) Token: 0x0600745D RID: 29789 RVA: 0x001A77FC File Offset: 0x001A59FC
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.StartPanel.LogOffButtons.normal == null)
						{
							VisualStyleElement.StartPanel.LogOffButtons.normal = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.LogOffButtons.part, 1);
						}
						return VisualStyleElement.StartPanel.LogOffButtons.normal;
					}
				}

				// Token: 0x17001A23 RID: 6691
				// (get) Token: 0x0600745E RID: 29790 RVA: 0x001A781F File Offset: 0x001A5A1F
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.StartPanel.LogOffButtons.hot == null)
						{
							VisualStyleElement.StartPanel.LogOffButtons.hot = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.LogOffButtons.part, 2);
						}
						return VisualStyleElement.StartPanel.LogOffButtons.hot;
					}
				}

				// Token: 0x17001A24 RID: 6692
				// (get) Token: 0x0600745F RID: 29791 RVA: 0x001A7842 File Offset: 0x001A5A42
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.StartPanel.LogOffButtons.pressed == null)
						{
							VisualStyleElement.StartPanel.LogOffButtons.pressed = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.LogOffButtons.part, 3);
						}
						return VisualStyleElement.StartPanel.LogOffButtons.pressed;
					}
				}

				// Token: 0x040046A0 RID: 18080
				private static readonly int part = 9;

				// Token: 0x040046A1 RID: 18081
				private static VisualStyleElement normal;

				// Token: 0x040046A2 RID: 18082
				private static VisualStyleElement hot;

				// Token: 0x040046A3 RID: 18083
				private static VisualStyleElement pressed;
			}

			// Token: 0x02000927 RID: 2343
			public static class UserPicture
			{
				// Token: 0x17001A25 RID: 6693
				// (get) Token: 0x06007461 RID: 29793 RVA: 0x001A786E File Offset: 0x001A5A6E
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.StartPanel.UserPicture.normal == null)
						{
							VisualStyleElement.StartPanel.UserPicture.normal = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.UserPicture.part, 0);
						}
						return VisualStyleElement.StartPanel.UserPicture.normal;
					}
				}

				// Token: 0x040046A4 RID: 18084
				private static readonly int part = 10;

				// Token: 0x040046A5 RID: 18085
				private static VisualStyleElement normal;
			}

			// Token: 0x02000928 RID: 2344
			public static class Preview
			{
				// Token: 0x17001A26 RID: 6694
				// (get) Token: 0x06007463 RID: 29795 RVA: 0x001A789A File Offset: 0x001A5A9A
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.StartPanel.Preview.normal == null)
						{
							VisualStyleElement.StartPanel.Preview.normal = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.Preview.part, 0);
						}
						return VisualStyleElement.StartPanel.Preview.normal;
					}
				}

				// Token: 0x040046A6 RID: 18086
				private static readonly int part = 11;

				// Token: 0x040046A7 RID: 18087
				private static VisualStyleElement normal;
			}
		}

		// Token: 0x02000842 RID: 2114
		public static class Status
		{
			// Token: 0x04004373 RID: 17267
			private static readonly string className = "STATUS";

			// Token: 0x02000929 RID: 2345
			public static class Bar
			{
				// Token: 0x17001A27 RID: 6695
				// (get) Token: 0x06007465 RID: 29797 RVA: 0x001A78C6 File Offset: 0x001A5AC6
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Status.Bar.normal == null)
						{
							VisualStyleElement.Status.Bar.normal = new VisualStyleElement(VisualStyleElement.Status.className, VisualStyleElement.Status.Bar.part, 0);
						}
						return VisualStyleElement.Status.Bar.normal;
					}
				}

				// Token: 0x040046A8 RID: 18088
				private static readonly int part;

				// Token: 0x040046A9 RID: 18089
				private static VisualStyleElement normal;
			}

			// Token: 0x0200092A RID: 2346
			public static class Pane
			{
				// Token: 0x17001A28 RID: 6696
				// (get) Token: 0x06007466 RID: 29798 RVA: 0x001A78E9 File Offset: 0x001A5AE9
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Status.Pane.normal == null)
						{
							VisualStyleElement.Status.Pane.normal = new VisualStyleElement(VisualStyleElement.Status.className, VisualStyleElement.Status.Pane.part, 0);
						}
						return VisualStyleElement.Status.Pane.normal;
					}
				}

				// Token: 0x040046AA RID: 18090
				private static readonly int part = 1;

				// Token: 0x040046AB RID: 18091
				private static VisualStyleElement normal;
			}

			// Token: 0x0200092B RID: 2347
			public static class GripperPane
			{
				// Token: 0x17001A29 RID: 6697
				// (get) Token: 0x06007468 RID: 29800 RVA: 0x001A7914 File Offset: 0x001A5B14
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Status.GripperPane.normal == null)
						{
							VisualStyleElement.Status.GripperPane.normal = new VisualStyleElement(VisualStyleElement.Status.className, VisualStyleElement.Status.GripperPane.part, 0);
						}
						return VisualStyleElement.Status.GripperPane.normal;
					}
				}

				// Token: 0x040046AC RID: 18092
				private static readonly int part = 2;

				// Token: 0x040046AD RID: 18093
				private static VisualStyleElement normal;
			}

			// Token: 0x0200092C RID: 2348
			public static class Gripper
			{
				// Token: 0x17001A2A RID: 6698
				// (get) Token: 0x0600746A RID: 29802 RVA: 0x001A793F File Offset: 0x001A5B3F
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Status.Gripper.normal == null)
						{
							VisualStyleElement.Status.Gripper.normal = new VisualStyleElement(VisualStyleElement.Status.className, VisualStyleElement.Status.Gripper.part, 0);
						}
						return VisualStyleElement.Status.Gripper.normal;
					}
				}

				// Token: 0x040046AE RID: 18094
				private static readonly int part = 3;

				// Token: 0x040046AF RID: 18095
				private static VisualStyleElement normal;
			}
		}

		// Token: 0x02000843 RID: 2115
		public static class TaskBand
		{
			// Token: 0x04004374 RID: 17268
			private static readonly string className = "TASKBAND";

			// Token: 0x0200092D RID: 2349
			public static class GroupCount
			{
				// Token: 0x17001A2B RID: 6699
				// (get) Token: 0x0600746C RID: 29804 RVA: 0x001A796A File Offset: 0x001A5B6A
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TaskBand.GroupCount.normal == null)
						{
							VisualStyleElement.TaskBand.GroupCount.normal = new VisualStyleElement(VisualStyleElement.TaskBand.className, VisualStyleElement.TaskBand.GroupCount.part, 0);
						}
						return VisualStyleElement.TaskBand.GroupCount.normal;
					}
				}

				// Token: 0x040046B0 RID: 18096
				private static readonly int part = 1;

				// Token: 0x040046B1 RID: 18097
				private static VisualStyleElement normal;
			}

			// Token: 0x0200092E RID: 2350
			public static class FlashButton
			{
				// Token: 0x17001A2C RID: 6700
				// (get) Token: 0x0600746E RID: 29806 RVA: 0x001A7995 File Offset: 0x001A5B95
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TaskBand.FlashButton.normal == null)
						{
							VisualStyleElement.TaskBand.FlashButton.normal = new VisualStyleElement(VisualStyleElement.TaskBand.className, VisualStyleElement.TaskBand.FlashButton.part, 0);
						}
						return VisualStyleElement.TaskBand.FlashButton.normal;
					}
				}

				// Token: 0x040046B2 RID: 18098
				private static readonly int part = 2;

				// Token: 0x040046B3 RID: 18099
				private static VisualStyleElement normal;
			}

			// Token: 0x0200092F RID: 2351
			public static class FlashButtonGroupMenu
			{
				// Token: 0x17001A2D RID: 6701
				// (get) Token: 0x06007470 RID: 29808 RVA: 0x001A79C0 File Offset: 0x001A5BC0
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TaskBand.FlashButtonGroupMenu.normal == null)
						{
							VisualStyleElement.TaskBand.FlashButtonGroupMenu.normal = new VisualStyleElement(VisualStyleElement.TaskBand.className, VisualStyleElement.TaskBand.FlashButtonGroupMenu.part, 0);
						}
						return VisualStyleElement.TaskBand.FlashButtonGroupMenu.normal;
					}
				}

				// Token: 0x040046B4 RID: 18100
				private static readonly int part = 3;

				// Token: 0x040046B5 RID: 18101
				private static VisualStyleElement normal;
			}
		}

		// Token: 0x02000844 RID: 2116
		public static class TaskbarClock
		{
			// Token: 0x04004375 RID: 17269
			private static readonly string className = "CLOCK";

			// Token: 0x02000930 RID: 2352
			public static class Time
			{
				// Token: 0x17001A2E RID: 6702
				// (get) Token: 0x06007472 RID: 29810 RVA: 0x001A79EB File Offset: 0x001A5BEB
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TaskbarClock.Time.normal == null)
						{
							VisualStyleElement.TaskbarClock.Time.normal = new VisualStyleElement(VisualStyleElement.TaskbarClock.className, VisualStyleElement.TaskbarClock.Time.part, 1);
						}
						return VisualStyleElement.TaskbarClock.Time.normal;
					}
				}

				// Token: 0x040046B6 RID: 18102
				private static readonly int part = 1;

				// Token: 0x040046B7 RID: 18103
				private static VisualStyleElement normal;
			}
		}

		// Token: 0x02000845 RID: 2117
		public static class Taskbar
		{
			// Token: 0x04004376 RID: 17270
			private static readonly string className = "TASKBAR";

			// Token: 0x02000931 RID: 2353
			public static class BackgroundBottom
			{
				// Token: 0x17001A2F RID: 6703
				// (get) Token: 0x06007474 RID: 29812 RVA: 0x001A7A16 File Offset: 0x001A5C16
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Taskbar.BackgroundBottom.normal == null)
						{
							VisualStyleElement.Taskbar.BackgroundBottom.normal = new VisualStyleElement(VisualStyleElement.Taskbar.className, VisualStyleElement.Taskbar.BackgroundBottom.part, 0);
						}
						return VisualStyleElement.Taskbar.BackgroundBottom.normal;
					}
				}

				// Token: 0x040046B8 RID: 18104
				private static readonly int part = 1;

				// Token: 0x040046B9 RID: 18105
				private static VisualStyleElement normal;
			}

			// Token: 0x02000932 RID: 2354
			public static class BackgroundRight
			{
				// Token: 0x17001A30 RID: 6704
				// (get) Token: 0x06007476 RID: 29814 RVA: 0x001A7A41 File Offset: 0x001A5C41
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Taskbar.BackgroundRight.normal == null)
						{
							VisualStyleElement.Taskbar.BackgroundRight.normal = new VisualStyleElement(VisualStyleElement.Taskbar.className, VisualStyleElement.Taskbar.BackgroundRight.part, 0);
						}
						return VisualStyleElement.Taskbar.BackgroundRight.normal;
					}
				}

				// Token: 0x040046BA RID: 18106
				private static readonly int part = 2;

				// Token: 0x040046BB RID: 18107
				private static VisualStyleElement normal;
			}

			// Token: 0x02000933 RID: 2355
			public static class BackgroundTop
			{
				// Token: 0x17001A31 RID: 6705
				// (get) Token: 0x06007478 RID: 29816 RVA: 0x001A7A6C File Offset: 0x001A5C6C
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Taskbar.BackgroundTop.normal == null)
						{
							VisualStyleElement.Taskbar.BackgroundTop.normal = new VisualStyleElement(VisualStyleElement.Taskbar.className, VisualStyleElement.Taskbar.BackgroundTop.part, 0);
						}
						return VisualStyleElement.Taskbar.BackgroundTop.normal;
					}
				}

				// Token: 0x040046BC RID: 18108
				private static readonly int part = 3;

				// Token: 0x040046BD RID: 18109
				private static VisualStyleElement normal;
			}

			// Token: 0x02000934 RID: 2356
			public static class BackgroundLeft
			{
				// Token: 0x17001A32 RID: 6706
				// (get) Token: 0x0600747A RID: 29818 RVA: 0x001A7A97 File Offset: 0x001A5C97
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Taskbar.BackgroundLeft.normal == null)
						{
							VisualStyleElement.Taskbar.BackgroundLeft.normal = new VisualStyleElement(VisualStyleElement.Taskbar.className, VisualStyleElement.Taskbar.BackgroundLeft.part, 0);
						}
						return VisualStyleElement.Taskbar.BackgroundLeft.normal;
					}
				}

				// Token: 0x040046BE RID: 18110
				private static readonly int part = 4;

				// Token: 0x040046BF RID: 18111
				private static VisualStyleElement normal;
			}

			// Token: 0x02000935 RID: 2357
			public static class SizingBarBottom
			{
				// Token: 0x17001A33 RID: 6707
				// (get) Token: 0x0600747C RID: 29820 RVA: 0x001A7AC2 File Offset: 0x001A5CC2
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Taskbar.SizingBarBottom.normal == null)
						{
							VisualStyleElement.Taskbar.SizingBarBottom.normal = new VisualStyleElement(VisualStyleElement.Taskbar.className, VisualStyleElement.Taskbar.SizingBarBottom.part, 0);
						}
						return VisualStyleElement.Taskbar.SizingBarBottom.normal;
					}
				}

				// Token: 0x040046C0 RID: 18112
				private static readonly int part = 5;

				// Token: 0x040046C1 RID: 18113
				private static VisualStyleElement normal;
			}

			// Token: 0x02000936 RID: 2358
			public static class SizingBarRight
			{
				// Token: 0x17001A34 RID: 6708
				// (get) Token: 0x0600747E RID: 29822 RVA: 0x001A7AED File Offset: 0x001A5CED
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Taskbar.SizingBarRight.normal == null)
						{
							VisualStyleElement.Taskbar.SizingBarRight.normal = new VisualStyleElement(VisualStyleElement.Taskbar.className, VisualStyleElement.Taskbar.SizingBarRight.part, 0);
						}
						return VisualStyleElement.Taskbar.SizingBarRight.normal;
					}
				}

				// Token: 0x040046C2 RID: 18114
				private static readonly int part = 6;

				// Token: 0x040046C3 RID: 18115
				private static VisualStyleElement normal;
			}

			// Token: 0x02000937 RID: 2359
			public static class SizingBarTop
			{
				// Token: 0x17001A35 RID: 6709
				// (get) Token: 0x06007480 RID: 29824 RVA: 0x001A7B18 File Offset: 0x001A5D18
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Taskbar.SizingBarTop.normal == null)
						{
							VisualStyleElement.Taskbar.SizingBarTop.normal = new VisualStyleElement(VisualStyleElement.Taskbar.className, VisualStyleElement.Taskbar.SizingBarTop.part, 0);
						}
						return VisualStyleElement.Taskbar.SizingBarTop.normal;
					}
				}

				// Token: 0x040046C4 RID: 18116
				private static readonly int part = 7;

				// Token: 0x040046C5 RID: 18117
				private static VisualStyleElement normal;
			}

			// Token: 0x02000938 RID: 2360
			public static class SizingBarLeft
			{
				// Token: 0x17001A36 RID: 6710
				// (get) Token: 0x06007482 RID: 29826 RVA: 0x001A7B43 File Offset: 0x001A5D43
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Taskbar.SizingBarLeft.normal == null)
						{
							VisualStyleElement.Taskbar.SizingBarLeft.normal = new VisualStyleElement(VisualStyleElement.Taskbar.className, VisualStyleElement.Taskbar.SizingBarLeft.part, 0);
						}
						return VisualStyleElement.Taskbar.SizingBarLeft.normal;
					}
				}

				// Token: 0x040046C6 RID: 18118
				private static readonly int part = 8;

				// Token: 0x040046C7 RID: 18119
				private static VisualStyleElement normal;
			}
		}

		// Token: 0x02000846 RID: 2118
		public static class ToolBar
		{
			// Token: 0x04004377 RID: 17271
			private static readonly string className = "TOOLBAR";

			// Token: 0x02000939 RID: 2361
			internal static class Bar
			{
				// Token: 0x17001A37 RID: 6711
				// (get) Token: 0x06007484 RID: 29828 RVA: 0x001A7B6E File Offset: 0x001A5D6E
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolBar.Bar.normal == null)
						{
							VisualStyleElement.ToolBar.Bar.normal = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.Bar.part, 0);
						}
						return VisualStyleElement.ToolBar.Bar.normal;
					}
				}

				// Token: 0x040046C8 RID: 18120
				private static readonly int part;

				// Token: 0x040046C9 RID: 18121
				private static VisualStyleElement normal;
			}

			// Token: 0x0200093A RID: 2362
			public static class Button
			{
				// Token: 0x17001A38 RID: 6712
				// (get) Token: 0x06007485 RID: 29829 RVA: 0x001A7B91 File Offset: 0x001A5D91
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolBar.Button.normal == null)
						{
							VisualStyleElement.ToolBar.Button.normal = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.Button.part, 1);
						}
						return VisualStyleElement.ToolBar.Button.normal;
					}
				}

				// Token: 0x17001A39 RID: 6713
				// (get) Token: 0x06007486 RID: 29830 RVA: 0x001A7BB4 File Offset: 0x001A5DB4
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ToolBar.Button.hot == null)
						{
							VisualStyleElement.ToolBar.Button.hot = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.Button.part, 2);
						}
						return VisualStyleElement.ToolBar.Button.hot;
					}
				}

				// Token: 0x17001A3A RID: 6714
				// (get) Token: 0x06007487 RID: 29831 RVA: 0x001A7BD7 File Offset: 0x001A5DD7
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ToolBar.Button.pressed == null)
						{
							VisualStyleElement.ToolBar.Button.pressed = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.Button.part, 3);
						}
						return VisualStyleElement.ToolBar.Button.pressed;
					}
				}

				// Token: 0x17001A3B RID: 6715
				// (get) Token: 0x06007488 RID: 29832 RVA: 0x001A7BFA File Offset: 0x001A5DFA
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ToolBar.Button.disabled == null)
						{
							VisualStyleElement.ToolBar.Button.disabled = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.Button.part, 4);
						}
						return VisualStyleElement.ToolBar.Button.disabled;
					}
				}

				// Token: 0x17001A3C RID: 6716
				// (get) Token: 0x06007489 RID: 29833 RVA: 0x001A7C1D File Offset: 0x001A5E1D
				public static VisualStyleElement Checked
				{
					get
					{
						if (VisualStyleElement.ToolBar.Button._checked == null)
						{
							VisualStyleElement.ToolBar.Button._checked = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.Button.part, 5);
						}
						return VisualStyleElement.ToolBar.Button._checked;
					}
				}

				// Token: 0x17001A3D RID: 6717
				// (get) Token: 0x0600748A RID: 29834 RVA: 0x001A7C40 File Offset: 0x001A5E40
				public static VisualStyleElement HotChecked
				{
					get
					{
						if (VisualStyleElement.ToolBar.Button.hotchecked == null)
						{
							VisualStyleElement.ToolBar.Button.hotchecked = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.Button.part, 6);
						}
						return VisualStyleElement.ToolBar.Button.hotchecked;
					}
				}

				// Token: 0x040046CA RID: 18122
				private static readonly int part = 1;

				// Token: 0x040046CB RID: 18123
				private static VisualStyleElement normal;

				// Token: 0x040046CC RID: 18124
				private static VisualStyleElement hot;

				// Token: 0x040046CD RID: 18125
				private static VisualStyleElement pressed;

				// Token: 0x040046CE RID: 18126
				private static VisualStyleElement disabled;

				// Token: 0x040046CF RID: 18127
				private static VisualStyleElement _checked;

				// Token: 0x040046D0 RID: 18128
				private static VisualStyleElement hotchecked;
			}

			// Token: 0x0200093B RID: 2363
			public static class DropDownButton
			{
				// Token: 0x17001A3E RID: 6718
				// (get) Token: 0x0600748C RID: 29836 RVA: 0x001A7C6B File Offset: 0x001A5E6B
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolBar.DropDownButton.normal == null)
						{
							VisualStyleElement.ToolBar.DropDownButton.normal = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.DropDownButton.part, 1);
						}
						return VisualStyleElement.ToolBar.DropDownButton.normal;
					}
				}

				// Token: 0x17001A3F RID: 6719
				// (get) Token: 0x0600748D RID: 29837 RVA: 0x001A7C8E File Offset: 0x001A5E8E
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ToolBar.DropDownButton.hot == null)
						{
							VisualStyleElement.ToolBar.DropDownButton.hot = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.DropDownButton.part, 2);
						}
						return VisualStyleElement.ToolBar.DropDownButton.hot;
					}
				}

				// Token: 0x17001A40 RID: 6720
				// (get) Token: 0x0600748E RID: 29838 RVA: 0x001A7CB1 File Offset: 0x001A5EB1
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ToolBar.DropDownButton.pressed == null)
						{
							VisualStyleElement.ToolBar.DropDownButton.pressed = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.DropDownButton.part, 3);
						}
						return VisualStyleElement.ToolBar.DropDownButton.pressed;
					}
				}

				// Token: 0x17001A41 RID: 6721
				// (get) Token: 0x0600748F RID: 29839 RVA: 0x001A7CD4 File Offset: 0x001A5ED4
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ToolBar.DropDownButton.disabled == null)
						{
							VisualStyleElement.ToolBar.DropDownButton.disabled = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.DropDownButton.part, 4);
						}
						return VisualStyleElement.ToolBar.DropDownButton.disabled;
					}
				}

				// Token: 0x17001A42 RID: 6722
				// (get) Token: 0x06007490 RID: 29840 RVA: 0x001A7CF7 File Offset: 0x001A5EF7
				public static VisualStyleElement Checked
				{
					get
					{
						if (VisualStyleElement.ToolBar.DropDownButton._checked == null)
						{
							VisualStyleElement.ToolBar.DropDownButton._checked = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.DropDownButton.part, 5);
						}
						return VisualStyleElement.ToolBar.DropDownButton._checked;
					}
				}

				// Token: 0x17001A43 RID: 6723
				// (get) Token: 0x06007491 RID: 29841 RVA: 0x001A7D1A File Offset: 0x001A5F1A
				public static VisualStyleElement HotChecked
				{
					get
					{
						if (VisualStyleElement.ToolBar.DropDownButton.hotchecked == null)
						{
							VisualStyleElement.ToolBar.DropDownButton.hotchecked = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.DropDownButton.part, 6);
						}
						return VisualStyleElement.ToolBar.DropDownButton.hotchecked;
					}
				}

				// Token: 0x040046D1 RID: 18129
				private static readonly int part = 2;

				// Token: 0x040046D2 RID: 18130
				private static VisualStyleElement normal;

				// Token: 0x040046D3 RID: 18131
				private static VisualStyleElement hot;

				// Token: 0x040046D4 RID: 18132
				private static VisualStyleElement pressed;

				// Token: 0x040046D5 RID: 18133
				private static VisualStyleElement disabled;

				// Token: 0x040046D6 RID: 18134
				private static VisualStyleElement _checked;

				// Token: 0x040046D7 RID: 18135
				private static VisualStyleElement hotchecked;
			}

			// Token: 0x0200093C RID: 2364
			public static class SplitButton
			{
				// Token: 0x17001A44 RID: 6724
				// (get) Token: 0x06007493 RID: 29843 RVA: 0x001A7D45 File Offset: 0x001A5F45
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButton.normal == null)
						{
							VisualStyleElement.ToolBar.SplitButton.normal = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButton.part, 1);
						}
						return VisualStyleElement.ToolBar.SplitButton.normal;
					}
				}

				// Token: 0x17001A45 RID: 6725
				// (get) Token: 0x06007494 RID: 29844 RVA: 0x001A7D68 File Offset: 0x001A5F68
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButton.hot == null)
						{
							VisualStyleElement.ToolBar.SplitButton.hot = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButton.part, 2);
						}
						return VisualStyleElement.ToolBar.SplitButton.hot;
					}
				}

				// Token: 0x17001A46 RID: 6726
				// (get) Token: 0x06007495 RID: 29845 RVA: 0x001A7D8B File Offset: 0x001A5F8B
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButton.pressed == null)
						{
							VisualStyleElement.ToolBar.SplitButton.pressed = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButton.part, 3);
						}
						return VisualStyleElement.ToolBar.SplitButton.pressed;
					}
				}

				// Token: 0x17001A47 RID: 6727
				// (get) Token: 0x06007496 RID: 29846 RVA: 0x001A7DAE File Offset: 0x001A5FAE
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButton.disabled == null)
						{
							VisualStyleElement.ToolBar.SplitButton.disabled = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButton.part, 4);
						}
						return VisualStyleElement.ToolBar.SplitButton.disabled;
					}
				}

				// Token: 0x17001A48 RID: 6728
				// (get) Token: 0x06007497 RID: 29847 RVA: 0x001A7DD1 File Offset: 0x001A5FD1
				public static VisualStyleElement Checked
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButton._checked == null)
						{
							VisualStyleElement.ToolBar.SplitButton._checked = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButton.part, 5);
						}
						return VisualStyleElement.ToolBar.SplitButton._checked;
					}
				}

				// Token: 0x17001A49 RID: 6729
				// (get) Token: 0x06007498 RID: 29848 RVA: 0x001A7DF4 File Offset: 0x001A5FF4
				public static VisualStyleElement HotChecked
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButton.hotchecked == null)
						{
							VisualStyleElement.ToolBar.SplitButton.hotchecked = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButton.part, 6);
						}
						return VisualStyleElement.ToolBar.SplitButton.hotchecked;
					}
				}

				// Token: 0x040046D8 RID: 18136
				private static readonly int part = 3;

				// Token: 0x040046D9 RID: 18137
				private static VisualStyleElement normal;

				// Token: 0x040046DA RID: 18138
				private static VisualStyleElement hot;

				// Token: 0x040046DB RID: 18139
				private static VisualStyleElement pressed;

				// Token: 0x040046DC RID: 18140
				private static VisualStyleElement disabled;

				// Token: 0x040046DD RID: 18141
				private static VisualStyleElement _checked;

				// Token: 0x040046DE RID: 18142
				private static VisualStyleElement hotchecked;
			}

			// Token: 0x0200093D RID: 2365
			public static class SplitButtonDropDown
			{
				// Token: 0x17001A4A RID: 6730
				// (get) Token: 0x0600749A RID: 29850 RVA: 0x001A7E1F File Offset: 0x001A601F
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButtonDropDown.normal == null)
						{
							VisualStyleElement.ToolBar.SplitButtonDropDown.normal = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButtonDropDown.part, 1);
						}
						return VisualStyleElement.ToolBar.SplitButtonDropDown.normal;
					}
				}

				// Token: 0x17001A4B RID: 6731
				// (get) Token: 0x0600749B RID: 29851 RVA: 0x001A7E42 File Offset: 0x001A6042
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButtonDropDown.hot == null)
						{
							VisualStyleElement.ToolBar.SplitButtonDropDown.hot = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButtonDropDown.part, 2);
						}
						return VisualStyleElement.ToolBar.SplitButtonDropDown.hot;
					}
				}

				// Token: 0x17001A4C RID: 6732
				// (get) Token: 0x0600749C RID: 29852 RVA: 0x001A7E65 File Offset: 0x001A6065
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButtonDropDown.pressed == null)
						{
							VisualStyleElement.ToolBar.SplitButtonDropDown.pressed = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButtonDropDown.part, 3);
						}
						return VisualStyleElement.ToolBar.SplitButtonDropDown.pressed;
					}
				}

				// Token: 0x17001A4D RID: 6733
				// (get) Token: 0x0600749D RID: 29853 RVA: 0x001A7E88 File Offset: 0x001A6088
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButtonDropDown.disabled == null)
						{
							VisualStyleElement.ToolBar.SplitButtonDropDown.disabled = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButtonDropDown.part, 4);
						}
						return VisualStyleElement.ToolBar.SplitButtonDropDown.disabled;
					}
				}

				// Token: 0x17001A4E RID: 6734
				// (get) Token: 0x0600749E RID: 29854 RVA: 0x001A7EAB File Offset: 0x001A60AB
				public static VisualStyleElement Checked
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButtonDropDown._checked == null)
						{
							VisualStyleElement.ToolBar.SplitButtonDropDown._checked = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButtonDropDown.part, 5);
						}
						return VisualStyleElement.ToolBar.SplitButtonDropDown._checked;
					}
				}

				// Token: 0x17001A4F RID: 6735
				// (get) Token: 0x0600749F RID: 29855 RVA: 0x001A7ECE File Offset: 0x001A60CE
				public static VisualStyleElement HotChecked
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButtonDropDown.hotchecked == null)
						{
							VisualStyleElement.ToolBar.SplitButtonDropDown.hotchecked = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButtonDropDown.part, 6);
						}
						return VisualStyleElement.ToolBar.SplitButtonDropDown.hotchecked;
					}
				}

				// Token: 0x040046DF RID: 18143
				private static readonly int part = 4;

				// Token: 0x040046E0 RID: 18144
				private static VisualStyleElement normal;

				// Token: 0x040046E1 RID: 18145
				private static VisualStyleElement hot;

				// Token: 0x040046E2 RID: 18146
				private static VisualStyleElement pressed;

				// Token: 0x040046E3 RID: 18147
				private static VisualStyleElement disabled;

				// Token: 0x040046E4 RID: 18148
				private static VisualStyleElement _checked;

				// Token: 0x040046E5 RID: 18149
				private static VisualStyleElement hotchecked;
			}

			// Token: 0x0200093E RID: 2366
			public static class SeparatorHorizontal
			{
				// Token: 0x17001A50 RID: 6736
				// (get) Token: 0x060074A1 RID: 29857 RVA: 0x001A7EF9 File Offset: 0x001A60F9
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolBar.SeparatorHorizontal.normal == null)
						{
							VisualStyleElement.ToolBar.SeparatorHorizontal.normal = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SeparatorHorizontal.part, 0);
						}
						return VisualStyleElement.ToolBar.SeparatorHorizontal.normal;
					}
				}

				// Token: 0x040046E6 RID: 18150
				private static readonly int part = 5;

				// Token: 0x040046E7 RID: 18151
				private static VisualStyleElement normal;
			}

			// Token: 0x0200093F RID: 2367
			public static class SeparatorVertical
			{
				// Token: 0x17001A51 RID: 6737
				// (get) Token: 0x060074A3 RID: 29859 RVA: 0x001A7F24 File Offset: 0x001A6124
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolBar.SeparatorVertical.normal == null)
						{
							VisualStyleElement.ToolBar.SeparatorVertical.normal = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SeparatorVertical.part, 0);
						}
						return VisualStyleElement.ToolBar.SeparatorVertical.normal;
					}
				}

				// Token: 0x040046E8 RID: 18152
				private static readonly int part = 6;

				// Token: 0x040046E9 RID: 18153
				private static VisualStyleElement normal;
			}
		}

		// Token: 0x02000847 RID: 2119
		public static class ToolTip
		{
			// Token: 0x04004378 RID: 17272
			private static readonly string className = "TOOLTIP";

			// Token: 0x02000940 RID: 2368
			public static class Standard
			{
				// Token: 0x17001A52 RID: 6738
				// (get) Token: 0x060074A5 RID: 29861 RVA: 0x001A7F4F File Offset: 0x001A614F
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolTip.Standard.normal == null)
						{
							VisualStyleElement.ToolTip.Standard.normal = new VisualStyleElement(VisualStyleElement.ToolTip.className, VisualStyleElement.ToolTip.Standard.part, 1);
						}
						return VisualStyleElement.ToolTip.Standard.normal;
					}
				}

				// Token: 0x17001A53 RID: 6739
				// (get) Token: 0x060074A6 RID: 29862 RVA: 0x001A7F72 File Offset: 0x001A6172
				public static VisualStyleElement Link
				{
					get
					{
						if (VisualStyleElement.ToolTip.Standard.link == null)
						{
							VisualStyleElement.ToolTip.Standard.link = new VisualStyleElement(VisualStyleElement.ToolTip.className, VisualStyleElement.ToolTip.Standard.part, 2);
						}
						return VisualStyleElement.ToolTip.Standard.link;
					}
				}

				// Token: 0x040046EA RID: 18154
				private static readonly int part = 1;

				// Token: 0x040046EB RID: 18155
				private static VisualStyleElement normal;

				// Token: 0x040046EC RID: 18156
				private static VisualStyleElement link;
			}

			// Token: 0x02000941 RID: 2369
			public static class StandardTitle
			{
				// Token: 0x17001A54 RID: 6740
				// (get) Token: 0x060074A8 RID: 29864 RVA: 0x001A7F9D File Offset: 0x001A619D
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolTip.StandardTitle.normal == null)
						{
							VisualStyleElement.ToolTip.StandardTitle.normal = new VisualStyleElement(VisualStyleElement.ToolTip.className, VisualStyleElement.ToolTip.StandardTitle.part, 0);
						}
						return VisualStyleElement.ToolTip.StandardTitle.normal;
					}
				}

				// Token: 0x040046ED RID: 18157
				private static readonly int part = 2;

				// Token: 0x040046EE RID: 18158
				private static VisualStyleElement normal;
			}

			// Token: 0x02000942 RID: 2370
			public static class Balloon
			{
				// Token: 0x17001A55 RID: 6741
				// (get) Token: 0x060074AA RID: 29866 RVA: 0x001A7FC8 File Offset: 0x001A61C8
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolTip.Balloon.normal == null)
						{
							VisualStyleElement.ToolTip.Balloon.normal = new VisualStyleElement(VisualStyleElement.ToolTip.className, VisualStyleElement.ToolTip.Balloon.part, 1);
						}
						return VisualStyleElement.ToolTip.Balloon.normal;
					}
				}

				// Token: 0x17001A56 RID: 6742
				// (get) Token: 0x060074AB RID: 29867 RVA: 0x001A7FEB File Offset: 0x001A61EB
				public static VisualStyleElement Link
				{
					get
					{
						if (VisualStyleElement.ToolTip.Balloon.link == null)
						{
							VisualStyleElement.ToolTip.Balloon.link = new VisualStyleElement(VisualStyleElement.ToolTip.className, VisualStyleElement.ToolTip.Balloon.part, 2);
						}
						return VisualStyleElement.ToolTip.Balloon.link;
					}
				}

				// Token: 0x040046EF RID: 18159
				private static readonly int part = 3;

				// Token: 0x040046F0 RID: 18160
				private static VisualStyleElement normal;

				// Token: 0x040046F1 RID: 18161
				private static VisualStyleElement link;
			}

			// Token: 0x02000943 RID: 2371
			public static class BalloonTitle
			{
				// Token: 0x17001A57 RID: 6743
				// (get) Token: 0x060074AD RID: 29869 RVA: 0x001A8016 File Offset: 0x001A6216
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolTip.BalloonTitle.normal == null)
						{
							VisualStyleElement.ToolTip.BalloonTitle.normal = new VisualStyleElement(VisualStyleElement.ToolTip.className, VisualStyleElement.ToolTip.BalloonTitle.part, 0);
						}
						return VisualStyleElement.ToolTip.BalloonTitle.normal;
					}
				}

				// Token: 0x040046F2 RID: 18162
				private static readonly int part = 4;

				// Token: 0x040046F3 RID: 18163
				private static VisualStyleElement normal;
			}

			// Token: 0x02000944 RID: 2372
			public static class Close
			{
				// Token: 0x17001A58 RID: 6744
				// (get) Token: 0x060074AF RID: 29871 RVA: 0x001A8041 File Offset: 0x001A6241
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolTip.Close.normal == null)
						{
							VisualStyleElement.ToolTip.Close.normal = new VisualStyleElement(VisualStyleElement.ToolTip.className, VisualStyleElement.ToolTip.Close.part, 1);
						}
						return VisualStyleElement.ToolTip.Close.normal;
					}
				}

				// Token: 0x17001A59 RID: 6745
				// (get) Token: 0x060074B0 RID: 29872 RVA: 0x001A8064 File Offset: 0x001A6264
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ToolTip.Close.hot == null)
						{
							VisualStyleElement.ToolTip.Close.hot = new VisualStyleElement(VisualStyleElement.ToolTip.className, VisualStyleElement.ToolTip.Close.part, 2);
						}
						return VisualStyleElement.ToolTip.Close.hot;
					}
				}

				// Token: 0x17001A5A RID: 6746
				// (get) Token: 0x060074B1 RID: 29873 RVA: 0x001A8087 File Offset: 0x001A6287
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ToolTip.Close.pressed == null)
						{
							VisualStyleElement.ToolTip.Close.pressed = new VisualStyleElement(VisualStyleElement.ToolTip.className, VisualStyleElement.ToolTip.Close.part, 3);
						}
						return VisualStyleElement.ToolTip.Close.pressed;
					}
				}

				// Token: 0x040046F4 RID: 18164
				private static readonly int part = 5;

				// Token: 0x040046F5 RID: 18165
				private static VisualStyleElement normal;

				// Token: 0x040046F6 RID: 18166
				private static VisualStyleElement hot;

				// Token: 0x040046F7 RID: 18167
				private static VisualStyleElement pressed;
			}
		}

		// Token: 0x02000848 RID: 2120
		public static class TrackBar
		{
			// Token: 0x04004379 RID: 17273
			private static readonly string className = "TRACKBAR";

			// Token: 0x02000945 RID: 2373
			public static class Track
			{
				// Token: 0x17001A5B RID: 6747
				// (get) Token: 0x060074B3 RID: 29875 RVA: 0x001A80B2 File Offset: 0x001A62B2
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrackBar.Track.normal == null)
						{
							VisualStyleElement.TrackBar.Track.normal = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.Track.part, 1);
						}
						return VisualStyleElement.TrackBar.Track.normal;
					}
				}

				// Token: 0x040046F8 RID: 18168
				private static readonly int part = 1;

				// Token: 0x040046F9 RID: 18169
				private static VisualStyleElement normal;
			}

			// Token: 0x02000946 RID: 2374
			public static class TrackVertical
			{
				// Token: 0x17001A5C RID: 6748
				// (get) Token: 0x060074B5 RID: 29877 RVA: 0x001A80DD File Offset: 0x001A62DD
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrackBar.TrackVertical.normal == null)
						{
							VisualStyleElement.TrackBar.TrackVertical.normal = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.TrackVertical.part, 1);
						}
						return VisualStyleElement.TrackBar.TrackVertical.normal;
					}
				}

				// Token: 0x040046FA RID: 18170
				private static readonly int part = 2;

				// Token: 0x040046FB RID: 18171
				private static VisualStyleElement normal;
			}

			// Token: 0x02000947 RID: 2375
			public static class Thumb
			{
				// Token: 0x17001A5D RID: 6749
				// (get) Token: 0x060074B7 RID: 29879 RVA: 0x001A8108 File Offset: 0x001A6308
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrackBar.Thumb.normal == null)
						{
							VisualStyleElement.TrackBar.Thumb.normal = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.Thumb.part, 1);
						}
						return VisualStyleElement.TrackBar.Thumb.normal;
					}
				}

				// Token: 0x17001A5E RID: 6750
				// (get) Token: 0x060074B8 RID: 29880 RVA: 0x001A812B File Offset: 0x001A632B
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.TrackBar.Thumb.hot == null)
						{
							VisualStyleElement.TrackBar.Thumb.hot = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.Thumb.part, 2);
						}
						return VisualStyleElement.TrackBar.Thumb.hot;
					}
				}

				// Token: 0x17001A5F RID: 6751
				// (get) Token: 0x060074B9 RID: 29881 RVA: 0x001A814E File Offset: 0x001A634E
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.TrackBar.Thumb.pressed == null)
						{
							VisualStyleElement.TrackBar.Thumb.pressed = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.Thumb.part, 3);
						}
						return VisualStyleElement.TrackBar.Thumb.pressed;
					}
				}

				// Token: 0x17001A60 RID: 6752
				// (get) Token: 0x060074BA RID: 29882 RVA: 0x001A8171 File Offset: 0x001A6371
				public static VisualStyleElement Focused
				{
					get
					{
						if (VisualStyleElement.TrackBar.Thumb.focused == null)
						{
							VisualStyleElement.TrackBar.Thumb.focused = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.Thumb.part, 4);
						}
						return VisualStyleElement.TrackBar.Thumb.focused;
					}
				}

				// Token: 0x17001A61 RID: 6753
				// (get) Token: 0x060074BB RID: 29883 RVA: 0x001A8194 File Offset: 0x001A6394
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.TrackBar.Thumb.disabled == null)
						{
							VisualStyleElement.TrackBar.Thumb.disabled = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.Thumb.part, 5);
						}
						return VisualStyleElement.TrackBar.Thumb.disabled;
					}
				}

				// Token: 0x040046FC RID: 18172
				private static readonly int part = 3;

				// Token: 0x040046FD RID: 18173
				private static VisualStyleElement normal;

				// Token: 0x040046FE RID: 18174
				private static VisualStyleElement hot;

				// Token: 0x040046FF RID: 18175
				private static VisualStyleElement pressed;

				// Token: 0x04004700 RID: 18176
				private static VisualStyleElement focused;

				// Token: 0x04004701 RID: 18177
				private static VisualStyleElement disabled;
			}

			// Token: 0x02000948 RID: 2376
			public static class ThumbBottom
			{
				// Token: 0x17001A62 RID: 6754
				// (get) Token: 0x060074BD RID: 29885 RVA: 0x001A81BF File Offset: 0x001A63BF
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbBottom.normal == null)
						{
							VisualStyleElement.TrackBar.ThumbBottom.normal = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbBottom.part, 1);
						}
						return VisualStyleElement.TrackBar.ThumbBottom.normal;
					}
				}

				// Token: 0x17001A63 RID: 6755
				// (get) Token: 0x060074BE RID: 29886 RVA: 0x001A81E2 File Offset: 0x001A63E2
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbBottom.hot == null)
						{
							VisualStyleElement.TrackBar.ThumbBottom.hot = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbBottom.part, 2);
						}
						return VisualStyleElement.TrackBar.ThumbBottom.hot;
					}
				}

				// Token: 0x17001A64 RID: 6756
				// (get) Token: 0x060074BF RID: 29887 RVA: 0x001A8205 File Offset: 0x001A6405
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbBottom.pressed == null)
						{
							VisualStyleElement.TrackBar.ThumbBottom.pressed = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbBottom.part, 3);
						}
						return VisualStyleElement.TrackBar.ThumbBottom.pressed;
					}
				}

				// Token: 0x17001A65 RID: 6757
				// (get) Token: 0x060074C0 RID: 29888 RVA: 0x001A8228 File Offset: 0x001A6428
				public static VisualStyleElement Focused
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbBottom.focused == null)
						{
							VisualStyleElement.TrackBar.ThumbBottom.focused = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbBottom.part, 4);
						}
						return VisualStyleElement.TrackBar.ThumbBottom.focused;
					}
				}

				// Token: 0x17001A66 RID: 6758
				// (get) Token: 0x060074C1 RID: 29889 RVA: 0x001A824B File Offset: 0x001A644B
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbBottom.disabled == null)
						{
							VisualStyleElement.TrackBar.ThumbBottom.disabled = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbBottom.part, 5);
						}
						return VisualStyleElement.TrackBar.ThumbBottom.disabled;
					}
				}

				// Token: 0x04004702 RID: 18178
				private static readonly int part = 4;

				// Token: 0x04004703 RID: 18179
				private static VisualStyleElement normal;

				// Token: 0x04004704 RID: 18180
				private static VisualStyleElement hot;

				// Token: 0x04004705 RID: 18181
				private static VisualStyleElement pressed;

				// Token: 0x04004706 RID: 18182
				private static VisualStyleElement focused;

				// Token: 0x04004707 RID: 18183
				private static VisualStyleElement disabled;
			}

			// Token: 0x02000949 RID: 2377
			public static class ThumbTop
			{
				// Token: 0x17001A67 RID: 6759
				// (get) Token: 0x060074C3 RID: 29891 RVA: 0x001A8276 File Offset: 0x001A6476
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbTop.normal == null)
						{
							VisualStyleElement.TrackBar.ThumbTop.normal = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbTop.part, 1);
						}
						return VisualStyleElement.TrackBar.ThumbTop.normal;
					}
				}

				// Token: 0x17001A68 RID: 6760
				// (get) Token: 0x060074C4 RID: 29892 RVA: 0x001A8299 File Offset: 0x001A6499
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbTop.hot == null)
						{
							VisualStyleElement.TrackBar.ThumbTop.hot = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbTop.part, 2);
						}
						return VisualStyleElement.TrackBar.ThumbTop.hot;
					}
				}

				// Token: 0x17001A69 RID: 6761
				// (get) Token: 0x060074C5 RID: 29893 RVA: 0x001A82BC File Offset: 0x001A64BC
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbTop.pressed == null)
						{
							VisualStyleElement.TrackBar.ThumbTop.pressed = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbTop.part, 3);
						}
						return VisualStyleElement.TrackBar.ThumbTop.pressed;
					}
				}

				// Token: 0x17001A6A RID: 6762
				// (get) Token: 0x060074C6 RID: 29894 RVA: 0x001A82DF File Offset: 0x001A64DF
				public static VisualStyleElement Focused
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbTop.focused == null)
						{
							VisualStyleElement.TrackBar.ThumbTop.focused = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbTop.part, 4);
						}
						return VisualStyleElement.TrackBar.ThumbTop.focused;
					}
				}

				// Token: 0x17001A6B RID: 6763
				// (get) Token: 0x060074C7 RID: 29895 RVA: 0x001A8302 File Offset: 0x001A6502
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbTop.disabled == null)
						{
							VisualStyleElement.TrackBar.ThumbTop.disabled = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbTop.part, 5);
						}
						return VisualStyleElement.TrackBar.ThumbTop.disabled;
					}
				}

				// Token: 0x04004708 RID: 18184
				private static readonly int part = 5;

				// Token: 0x04004709 RID: 18185
				private static VisualStyleElement normal;

				// Token: 0x0400470A RID: 18186
				private static VisualStyleElement hot;

				// Token: 0x0400470B RID: 18187
				private static VisualStyleElement pressed;

				// Token: 0x0400470C RID: 18188
				private static VisualStyleElement focused;

				// Token: 0x0400470D RID: 18189
				private static VisualStyleElement disabled;
			}

			// Token: 0x0200094A RID: 2378
			public static class ThumbVertical
			{
				// Token: 0x17001A6C RID: 6764
				// (get) Token: 0x060074C9 RID: 29897 RVA: 0x001A832D File Offset: 0x001A652D
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbVertical.normal == null)
						{
							VisualStyleElement.TrackBar.ThumbVertical.normal = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbVertical.part, 1);
						}
						return VisualStyleElement.TrackBar.ThumbVertical.normal;
					}
				}

				// Token: 0x17001A6D RID: 6765
				// (get) Token: 0x060074CA RID: 29898 RVA: 0x001A8350 File Offset: 0x001A6550
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbVertical.hot == null)
						{
							VisualStyleElement.TrackBar.ThumbVertical.hot = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbVertical.part, 2);
						}
						return VisualStyleElement.TrackBar.ThumbVertical.hot;
					}
				}

				// Token: 0x17001A6E RID: 6766
				// (get) Token: 0x060074CB RID: 29899 RVA: 0x001A8373 File Offset: 0x001A6573
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbVertical.pressed == null)
						{
							VisualStyleElement.TrackBar.ThumbVertical.pressed = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbVertical.part, 3);
						}
						return VisualStyleElement.TrackBar.ThumbVertical.pressed;
					}
				}

				// Token: 0x17001A6F RID: 6767
				// (get) Token: 0x060074CC RID: 29900 RVA: 0x001A8396 File Offset: 0x001A6596
				public static VisualStyleElement Focused
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbVertical.focused == null)
						{
							VisualStyleElement.TrackBar.ThumbVertical.focused = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbVertical.part, 4);
						}
						return VisualStyleElement.TrackBar.ThumbVertical.focused;
					}
				}

				// Token: 0x17001A70 RID: 6768
				// (get) Token: 0x060074CD RID: 29901 RVA: 0x001A83B9 File Offset: 0x001A65B9
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbVertical.disabled == null)
						{
							VisualStyleElement.TrackBar.ThumbVertical.disabled = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbVertical.part, 5);
						}
						return VisualStyleElement.TrackBar.ThumbVertical.disabled;
					}
				}

				// Token: 0x0400470E RID: 18190
				private static readonly int part = 6;

				// Token: 0x0400470F RID: 18191
				private static VisualStyleElement normal;

				// Token: 0x04004710 RID: 18192
				private static VisualStyleElement hot;

				// Token: 0x04004711 RID: 18193
				private static VisualStyleElement pressed;

				// Token: 0x04004712 RID: 18194
				private static VisualStyleElement focused;

				// Token: 0x04004713 RID: 18195
				private static VisualStyleElement disabled;
			}

			// Token: 0x0200094B RID: 2379
			public static class ThumbLeft
			{
				// Token: 0x17001A71 RID: 6769
				// (get) Token: 0x060074CF RID: 29903 RVA: 0x001A83E4 File Offset: 0x001A65E4
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbLeft.normal == null)
						{
							VisualStyleElement.TrackBar.ThumbLeft.normal = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbLeft.part, 1);
						}
						return VisualStyleElement.TrackBar.ThumbLeft.normal;
					}
				}

				// Token: 0x17001A72 RID: 6770
				// (get) Token: 0x060074D0 RID: 29904 RVA: 0x001A8407 File Offset: 0x001A6607
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbLeft.hot == null)
						{
							VisualStyleElement.TrackBar.ThumbLeft.hot = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbLeft.part, 2);
						}
						return VisualStyleElement.TrackBar.ThumbLeft.hot;
					}
				}

				// Token: 0x17001A73 RID: 6771
				// (get) Token: 0x060074D1 RID: 29905 RVA: 0x001A842A File Offset: 0x001A662A
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbLeft.pressed == null)
						{
							VisualStyleElement.TrackBar.ThumbLeft.pressed = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbLeft.part, 3);
						}
						return VisualStyleElement.TrackBar.ThumbLeft.pressed;
					}
				}

				// Token: 0x17001A74 RID: 6772
				// (get) Token: 0x060074D2 RID: 29906 RVA: 0x001A844D File Offset: 0x001A664D
				public static VisualStyleElement Focused
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbLeft.focused == null)
						{
							VisualStyleElement.TrackBar.ThumbLeft.focused = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbLeft.part, 4);
						}
						return VisualStyleElement.TrackBar.ThumbLeft.focused;
					}
				}

				// Token: 0x17001A75 RID: 6773
				// (get) Token: 0x060074D3 RID: 29907 RVA: 0x001A8470 File Offset: 0x001A6670
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbLeft.disabled == null)
						{
							VisualStyleElement.TrackBar.ThumbLeft.disabled = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbLeft.part, 5);
						}
						return VisualStyleElement.TrackBar.ThumbLeft.disabled;
					}
				}

				// Token: 0x04004714 RID: 18196
				private static readonly int part = 7;

				// Token: 0x04004715 RID: 18197
				private static VisualStyleElement normal;

				// Token: 0x04004716 RID: 18198
				private static VisualStyleElement hot;

				// Token: 0x04004717 RID: 18199
				private static VisualStyleElement pressed;

				// Token: 0x04004718 RID: 18200
				private static VisualStyleElement focused;

				// Token: 0x04004719 RID: 18201
				private static VisualStyleElement disabled;
			}

			// Token: 0x0200094C RID: 2380
			public static class ThumbRight
			{
				// Token: 0x17001A76 RID: 6774
				// (get) Token: 0x060074D5 RID: 29909 RVA: 0x001A849B File Offset: 0x001A669B
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbRight.normal == null)
						{
							VisualStyleElement.TrackBar.ThumbRight.normal = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbRight.part, 1);
						}
						return VisualStyleElement.TrackBar.ThumbRight.normal;
					}
				}

				// Token: 0x17001A77 RID: 6775
				// (get) Token: 0x060074D6 RID: 29910 RVA: 0x001A84BE File Offset: 0x001A66BE
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbRight.hot == null)
						{
							VisualStyleElement.TrackBar.ThumbRight.hot = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbRight.part, 2);
						}
						return VisualStyleElement.TrackBar.ThumbRight.hot;
					}
				}

				// Token: 0x17001A78 RID: 6776
				// (get) Token: 0x060074D7 RID: 29911 RVA: 0x001A84E1 File Offset: 0x001A66E1
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbRight.pressed == null)
						{
							VisualStyleElement.TrackBar.ThumbRight.pressed = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbRight.part, 3);
						}
						return VisualStyleElement.TrackBar.ThumbRight.pressed;
					}
				}

				// Token: 0x17001A79 RID: 6777
				// (get) Token: 0x060074D8 RID: 29912 RVA: 0x001A8504 File Offset: 0x001A6704
				public static VisualStyleElement Focused
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbRight.focused == null)
						{
							VisualStyleElement.TrackBar.ThumbRight.focused = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbRight.part, 4);
						}
						return VisualStyleElement.TrackBar.ThumbRight.focused;
					}
				}

				// Token: 0x17001A7A RID: 6778
				// (get) Token: 0x060074D9 RID: 29913 RVA: 0x001A8527 File Offset: 0x001A6727
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbRight.disabled == null)
						{
							VisualStyleElement.TrackBar.ThumbRight.disabled = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbRight.part, 5);
						}
						return VisualStyleElement.TrackBar.ThumbRight.disabled;
					}
				}

				// Token: 0x0400471A RID: 18202
				private static readonly int part = 8;

				// Token: 0x0400471B RID: 18203
				private static VisualStyleElement normal;

				// Token: 0x0400471C RID: 18204
				private static VisualStyleElement hot;

				// Token: 0x0400471D RID: 18205
				private static VisualStyleElement pressed;

				// Token: 0x0400471E RID: 18206
				private static VisualStyleElement focused;

				// Token: 0x0400471F RID: 18207
				private static VisualStyleElement disabled;
			}

			// Token: 0x0200094D RID: 2381
			public static class Ticks
			{
				// Token: 0x17001A7B RID: 6779
				// (get) Token: 0x060074DB RID: 29915 RVA: 0x001A8552 File Offset: 0x001A6752
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrackBar.Ticks.normal == null)
						{
							VisualStyleElement.TrackBar.Ticks.normal = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.Ticks.part, 1);
						}
						return VisualStyleElement.TrackBar.Ticks.normal;
					}
				}

				// Token: 0x04004720 RID: 18208
				private static readonly int part = 9;

				// Token: 0x04004721 RID: 18209
				private static VisualStyleElement normal;
			}

			// Token: 0x0200094E RID: 2382
			public static class TicksVertical
			{
				// Token: 0x17001A7C RID: 6780
				// (get) Token: 0x060074DD RID: 29917 RVA: 0x001A857E File Offset: 0x001A677E
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrackBar.TicksVertical.normal == null)
						{
							VisualStyleElement.TrackBar.TicksVertical.normal = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.TicksVertical.part, 1);
						}
						return VisualStyleElement.TrackBar.TicksVertical.normal;
					}
				}

				// Token: 0x04004722 RID: 18210
				private static readonly int part = 10;

				// Token: 0x04004723 RID: 18211
				private static VisualStyleElement normal;
			}
		}

		// Token: 0x02000849 RID: 2121
		public static class TreeView
		{
			// Token: 0x0400437A RID: 17274
			private static readonly string className = "TREEVIEW";

			// Token: 0x0200094F RID: 2383
			public static class Item
			{
				// Token: 0x17001A7D RID: 6781
				// (get) Token: 0x060074DF RID: 29919 RVA: 0x001A85AA File Offset: 0x001A67AA
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TreeView.Item.normal == null)
						{
							VisualStyleElement.TreeView.Item.normal = new VisualStyleElement(VisualStyleElement.TreeView.className, VisualStyleElement.TreeView.Item.part, 1);
						}
						return VisualStyleElement.TreeView.Item.normal;
					}
				}

				// Token: 0x17001A7E RID: 6782
				// (get) Token: 0x060074E0 RID: 29920 RVA: 0x001A85CD File Offset: 0x001A67CD
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.TreeView.Item.hot == null)
						{
							VisualStyleElement.TreeView.Item.hot = new VisualStyleElement(VisualStyleElement.TreeView.className, VisualStyleElement.TreeView.Item.part, 2);
						}
						return VisualStyleElement.TreeView.Item.hot;
					}
				}

				// Token: 0x17001A7F RID: 6783
				// (get) Token: 0x060074E1 RID: 29921 RVA: 0x001A85F0 File Offset: 0x001A67F0
				public static VisualStyleElement Selected
				{
					get
					{
						if (VisualStyleElement.TreeView.Item.selected == null)
						{
							VisualStyleElement.TreeView.Item.selected = new VisualStyleElement(VisualStyleElement.TreeView.className, VisualStyleElement.TreeView.Item.part, 3);
						}
						return VisualStyleElement.TreeView.Item.selected;
					}
				}

				// Token: 0x17001A80 RID: 6784
				// (get) Token: 0x060074E2 RID: 29922 RVA: 0x001A8613 File Offset: 0x001A6813
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.TreeView.Item.disabled == null)
						{
							VisualStyleElement.TreeView.Item.disabled = new VisualStyleElement(VisualStyleElement.TreeView.className, VisualStyleElement.TreeView.Item.part, 4);
						}
						return VisualStyleElement.TreeView.Item.disabled;
					}
				}

				// Token: 0x17001A81 RID: 6785
				// (get) Token: 0x060074E3 RID: 29923 RVA: 0x001A8636 File Offset: 0x001A6836
				public static VisualStyleElement SelectedNotFocus
				{
					get
					{
						if (VisualStyleElement.TreeView.Item.selectednotfocus == null)
						{
							VisualStyleElement.TreeView.Item.selectednotfocus = new VisualStyleElement(VisualStyleElement.TreeView.className, VisualStyleElement.TreeView.Item.part, 5);
						}
						return VisualStyleElement.TreeView.Item.selectednotfocus;
					}
				}

				// Token: 0x04004724 RID: 18212
				private static readonly int part = 1;

				// Token: 0x04004725 RID: 18213
				private static VisualStyleElement normal;

				// Token: 0x04004726 RID: 18214
				private static VisualStyleElement hot;

				// Token: 0x04004727 RID: 18215
				private static VisualStyleElement selected;

				// Token: 0x04004728 RID: 18216
				private static VisualStyleElement disabled;

				// Token: 0x04004729 RID: 18217
				private static VisualStyleElement selectednotfocus;
			}

			// Token: 0x02000950 RID: 2384
			public static class Glyph
			{
				// Token: 0x17001A82 RID: 6786
				// (get) Token: 0x060074E5 RID: 29925 RVA: 0x001A8661 File Offset: 0x001A6861
				public static VisualStyleElement Closed
				{
					get
					{
						if (VisualStyleElement.TreeView.Glyph.closed == null)
						{
							VisualStyleElement.TreeView.Glyph.closed = new VisualStyleElement(VisualStyleElement.TreeView.className, VisualStyleElement.TreeView.Glyph.part, 1);
						}
						return VisualStyleElement.TreeView.Glyph.closed;
					}
				}

				// Token: 0x17001A83 RID: 6787
				// (get) Token: 0x060074E6 RID: 29926 RVA: 0x001A8684 File Offset: 0x001A6884
				public static VisualStyleElement Opened
				{
					get
					{
						if (VisualStyleElement.TreeView.Glyph.opened == null)
						{
							VisualStyleElement.TreeView.Glyph.opened = new VisualStyleElement(VisualStyleElement.TreeView.className, VisualStyleElement.TreeView.Glyph.part, 2);
						}
						return VisualStyleElement.TreeView.Glyph.opened;
					}
				}

				// Token: 0x0400472A RID: 18218
				private static readonly int part = 2;

				// Token: 0x0400472B RID: 18219
				private static VisualStyleElement closed;

				// Token: 0x0400472C RID: 18220
				private static VisualStyleElement opened;
			}

			// Token: 0x02000951 RID: 2385
			public static class Branch
			{
				// Token: 0x17001A84 RID: 6788
				// (get) Token: 0x060074E8 RID: 29928 RVA: 0x001A86AF File Offset: 0x001A68AF
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TreeView.Branch.normal == null)
						{
							VisualStyleElement.TreeView.Branch.normal = new VisualStyleElement(VisualStyleElement.TreeView.className, VisualStyleElement.TreeView.Branch.part, 0);
						}
						return VisualStyleElement.TreeView.Branch.normal;
					}
				}

				// Token: 0x0400472D RID: 18221
				private static readonly int part = 3;

				// Token: 0x0400472E RID: 18222
				private static VisualStyleElement normal;
			}
		}

		// Token: 0x0200084A RID: 2122
		internal static class ExplorerTreeView
		{
			// Token: 0x0400437B RID: 17275
			private static readonly string className = "Explorer::TreeView";

			// Token: 0x02000952 RID: 2386
			public static class Glyph
			{
				// Token: 0x17001A85 RID: 6789
				// (get) Token: 0x060074EA RID: 29930 RVA: 0x001A86DA File Offset: 0x001A68DA
				public static VisualStyleElement Closed
				{
					get
					{
						if (VisualStyleElement.ExplorerTreeView.Glyph.closed == null)
						{
							VisualStyleElement.ExplorerTreeView.Glyph.closed = new VisualStyleElement(VisualStyleElement.ExplorerTreeView.className, VisualStyleElement.ExplorerTreeView.Glyph.part, 1);
						}
						return VisualStyleElement.ExplorerTreeView.Glyph.closed;
					}
				}

				// Token: 0x17001A86 RID: 6790
				// (get) Token: 0x060074EB RID: 29931 RVA: 0x001A86FD File Offset: 0x001A68FD
				public static VisualStyleElement Opened
				{
					get
					{
						if (VisualStyleElement.ExplorerTreeView.Glyph.opened == null)
						{
							VisualStyleElement.ExplorerTreeView.Glyph.opened = new VisualStyleElement(VisualStyleElement.ExplorerTreeView.className, VisualStyleElement.ExplorerTreeView.Glyph.part, 2);
						}
						return VisualStyleElement.ExplorerTreeView.Glyph.opened;
					}
				}

				// Token: 0x0400472F RID: 18223
				private static readonly int part = 2;

				// Token: 0x04004730 RID: 18224
				private static VisualStyleElement closed;

				// Token: 0x04004731 RID: 18225
				private static VisualStyleElement opened;
			}
		}

		// Token: 0x0200084B RID: 2123
		public static class TextBox
		{
			// Token: 0x0400437C RID: 17276
			private static readonly string className = "EDIT";

			// Token: 0x02000953 RID: 2387
			public static class TextEdit
			{
				// Token: 0x17001A87 RID: 6791
				// (get) Token: 0x060074ED RID: 29933 RVA: 0x001A8728 File Offset: 0x001A6928
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TextBox.TextEdit.normal == null)
						{
							VisualStyleElement.TextBox.TextEdit.normal = new VisualStyleElement(VisualStyleElement.TextBox.className, VisualStyleElement.TextBox.TextEdit.part, 1);
						}
						return VisualStyleElement.TextBox.TextEdit.normal;
					}
				}

				// Token: 0x17001A88 RID: 6792
				// (get) Token: 0x060074EE RID: 29934 RVA: 0x001A874B File Offset: 0x001A694B
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.TextBox.TextEdit.hot == null)
						{
							VisualStyleElement.TextBox.TextEdit.hot = new VisualStyleElement(VisualStyleElement.TextBox.className, VisualStyleElement.TextBox.TextEdit.part, 2);
						}
						return VisualStyleElement.TextBox.TextEdit.hot;
					}
				}

				// Token: 0x17001A89 RID: 6793
				// (get) Token: 0x060074EF RID: 29935 RVA: 0x001A876E File Offset: 0x001A696E
				public static VisualStyleElement Selected
				{
					get
					{
						if (VisualStyleElement.TextBox.TextEdit.selected == null)
						{
							VisualStyleElement.TextBox.TextEdit.selected = new VisualStyleElement(VisualStyleElement.TextBox.className, VisualStyleElement.TextBox.TextEdit.part, 3);
						}
						return VisualStyleElement.TextBox.TextEdit.selected;
					}
				}

				// Token: 0x17001A8A RID: 6794
				// (get) Token: 0x060074F0 RID: 29936 RVA: 0x001A8791 File Offset: 0x001A6991
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.TextBox.TextEdit.disabled == null)
						{
							VisualStyleElement.TextBox.TextEdit.disabled = new VisualStyleElement(VisualStyleElement.TextBox.className, VisualStyleElement.TextBox.TextEdit.part, 4);
						}
						return VisualStyleElement.TextBox.TextEdit.disabled;
					}
				}

				// Token: 0x17001A8B RID: 6795
				// (get) Token: 0x060074F1 RID: 29937 RVA: 0x001A87B4 File Offset: 0x001A69B4
				public static VisualStyleElement Focused
				{
					get
					{
						if (VisualStyleElement.TextBox.TextEdit.focused == null)
						{
							VisualStyleElement.TextBox.TextEdit.focused = new VisualStyleElement(VisualStyleElement.TextBox.className, VisualStyleElement.TextBox.TextEdit.part, 5);
						}
						return VisualStyleElement.TextBox.TextEdit.focused;
					}
				}

				// Token: 0x17001A8C RID: 6796
				// (get) Token: 0x060074F2 RID: 29938 RVA: 0x001A87D7 File Offset: 0x001A69D7
				public static VisualStyleElement ReadOnly
				{
					get
					{
						if (VisualStyleElement.TextBox.TextEdit._readonly == null)
						{
							VisualStyleElement.TextBox.TextEdit._readonly = new VisualStyleElement(VisualStyleElement.TextBox.className, VisualStyleElement.TextBox.TextEdit.part, 6);
						}
						return VisualStyleElement.TextBox.TextEdit._readonly;
					}
				}

				// Token: 0x17001A8D RID: 6797
				// (get) Token: 0x060074F3 RID: 29939 RVA: 0x001A87FA File Offset: 0x001A69FA
				public static VisualStyleElement Assist
				{
					get
					{
						if (VisualStyleElement.TextBox.TextEdit.assist == null)
						{
							VisualStyleElement.TextBox.TextEdit.assist = new VisualStyleElement(VisualStyleElement.TextBox.className, VisualStyleElement.TextBox.TextEdit.part, 7);
						}
						return VisualStyleElement.TextBox.TextEdit.assist;
					}
				}

				// Token: 0x04004732 RID: 18226
				private static readonly int part = 1;

				// Token: 0x04004733 RID: 18227
				private static VisualStyleElement normal;

				// Token: 0x04004734 RID: 18228
				private static VisualStyleElement hot;

				// Token: 0x04004735 RID: 18229
				private static VisualStyleElement selected;

				// Token: 0x04004736 RID: 18230
				private static VisualStyleElement disabled;

				// Token: 0x04004737 RID: 18231
				private static VisualStyleElement focused;

				// Token: 0x04004738 RID: 18232
				private static VisualStyleElement _readonly;

				// Token: 0x04004739 RID: 18233
				private static VisualStyleElement assist;
			}

			// Token: 0x02000954 RID: 2388
			public static class Caret
			{
				// Token: 0x17001A8E RID: 6798
				// (get) Token: 0x060074F5 RID: 29941 RVA: 0x001A8825 File Offset: 0x001A6A25
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TextBox.Caret.normal == null)
						{
							VisualStyleElement.TextBox.Caret.normal = new VisualStyleElement(VisualStyleElement.TextBox.className, VisualStyleElement.TextBox.Caret.part, 0);
						}
						return VisualStyleElement.TextBox.Caret.normal;
					}
				}

				// Token: 0x0400473A RID: 18234
				private static readonly int part = 2;

				// Token: 0x0400473B RID: 18235
				private static VisualStyleElement normal;
			}
		}

		// Token: 0x0200084C RID: 2124
		public static class TrayNotify
		{
			// Token: 0x0400437D RID: 17277
			private static readonly string className = "TRAYNOTIFY";

			// Token: 0x02000955 RID: 2389
			public static class Background
			{
				// Token: 0x17001A8F RID: 6799
				// (get) Token: 0x060074F7 RID: 29943 RVA: 0x001A8850 File Offset: 0x001A6A50
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrayNotify.Background.normal == null)
						{
							VisualStyleElement.TrayNotify.Background.normal = new VisualStyleElement(VisualStyleElement.TrayNotify.className, VisualStyleElement.TrayNotify.Background.part, 0);
						}
						return VisualStyleElement.TrayNotify.Background.normal;
					}
				}

				// Token: 0x0400473C RID: 18236
				private static readonly int part = 1;

				// Token: 0x0400473D RID: 18237
				private static VisualStyleElement normal;
			}

			// Token: 0x02000956 RID: 2390
			public static class AnimateBackground
			{
				// Token: 0x17001A90 RID: 6800
				// (get) Token: 0x060074F9 RID: 29945 RVA: 0x001A887B File Offset: 0x001A6A7B
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrayNotify.AnimateBackground.normal == null)
						{
							VisualStyleElement.TrayNotify.AnimateBackground.normal = new VisualStyleElement(VisualStyleElement.TrayNotify.className, VisualStyleElement.TrayNotify.AnimateBackground.part, 0);
						}
						return VisualStyleElement.TrayNotify.AnimateBackground.normal;
					}
				}

				// Token: 0x0400473E RID: 18238
				private static readonly int part = 2;

				// Token: 0x0400473F RID: 18239
				private static VisualStyleElement normal;
			}
		}

		// Token: 0x0200084D RID: 2125
		public static class Window
		{
			// Token: 0x0400437E RID: 17278
			private static readonly string className = "WINDOW";

			// Token: 0x02000957 RID: 2391
			public static class Caption
			{
				// Token: 0x17001A91 RID: 6801
				// (get) Token: 0x060074FB RID: 29947 RVA: 0x001A88A6 File Offset: 0x001A6AA6
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.Caption.active == null)
						{
							VisualStyleElement.Window.Caption.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.Caption.part, 1);
						}
						return VisualStyleElement.Window.Caption.active;
					}
				}

				// Token: 0x17001A92 RID: 6802
				// (get) Token: 0x060074FC RID: 29948 RVA: 0x001A88C9 File Offset: 0x001A6AC9
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.Caption.inactive == null)
						{
							VisualStyleElement.Window.Caption.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.Caption.part, 2);
						}
						return VisualStyleElement.Window.Caption.inactive;
					}
				}

				// Token: 0x17001A93 RID: 6803
				// (get) Token: 0x060074FD RID: 29949 RVA: 0x001A88EC File Offset: 0x001A6AEC
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.Caption.disabled == null)
						{
							VisualStyleElement.Window.Caption.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.Caption.part, 3);
						}
						return VisualStyleElement.Window.Caption.disabled;
					}
				}

				// Token: 0x04004740 RID: 18240
				private static readonly int part = 1;

				// Token: 0x04004741 RID: 18241
				private static VisualStyleElement active;

				// Token: 0x04004742 RID: 18242
				private static VisualStyleElement inactive;

				// Token: 0x04004743 RID: 18243
				private static VisualStyleElement disabled;
			}

			// Token: 0x02000958 RID: 2392
			public static class SmallCaption
			{
				// Token: 0x17001A94 RID: 6804
				// (get) Token: 0x060074FF RID: 29951 RVA: 0x001A8917 File Offset: 0x001A6B17
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.SmallCaption.active == null)
						{
							VisualStyleElement.Window.SmallCaption.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallCaption.part, 1);
						}
						return VisualStyleElement.Window.SmallCaption.active;
					}
				}

				// Token: 0x17001A95 RID: 6805
				// (get) Token: 0x06007500 RID: 29952 RVA: 0x001A893A File Offset: 0x001A6B3A
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.SmallCaption.inactive == null)
						{
							VisualStyleElement.Window.SmallCaption.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallCaption.part, 2);
						}
						return VisualStyleElement.Window.SmallCaption.inactive;
					}
				}

				// Token: 0x17001A96 RID: 6806
				// (get) Token: 0x06007501 RID: 29953 RVA: 0x001A895D File Offset: 0x001A6B5D
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.SmallCaption.disabled == null)
						{
							VisualStyleElement.Window.SmallCaption.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallCaption.part, 3);
						}
						return VisualStyleElement.Window.SmallCaption.disabled;
					}
				}

				// Token: 0x04004744 RID: 18244
				private static readonly int part = 2;

				// Token: 0x04004745 RID: 18245
				private static VisualStyleElement active;

				// Token: 0x04004746 RID: 18246
				private static VisualStyleElement inactive;

				// Token: 0x04004747 RID: 18247
				private static VisualStyleElement disabled;
			}

			// Token: 0x02000959 RID: 2393
			public static class MinCaption
			{
				// Token: 0x17001A97 RID: 6807
				// (get) Token: 0x06007503 RID: 29955 RVA: 0x001A8988 File Offset: 0x001A6B88
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.MinCaption.active == null)
						{
							VisualStyleElement.Window.MinCaption.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MinCaption.part, 1);
						}
						return VisualStyleElement.Window.MinCaption.active;
					}
				}

				// Token: 0x17001A98 RID: 6808
				// (get) Token: 0x06007504 RID: 29956 RVA: 0x001A89AB File Offset: 0x001A6BAB
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.MinCaption.inactive == null)
						{
							VisualStyleElement.Window.MinCaption.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MinCaption.part, 2);
						}
						return VisualStyleElement.Window.MinCaption.inactive;
					}
				}

				// Token: 0x17001A99 RID: 6809
				// (get) Token: 0x06007505 RID: 29957 RVA: 0x001A89CE File Offset: 0x001A6BCE
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.MinCaption.disabled == null)
						{
							VisualStyleElement.Window.MinCaption.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MinCaption.part, 3);
						}
						return VisualStyleElement.Window.MinCaption.disabled;
					}
				}

				// Token: 0x04004748 RID: 18248
				private static readonly int part = 3;

				// Token: 0x04004749 RID: 18249
				private static VisualStyleElement active;

				// Token: 0x0400474A RID: 18250
				private static VisualStyleElement inactive;

				// Token: 0x0400474B RID: 18251
				private static VisualStyleElement disabled;
			}

			// Token: 0x0200095A RID: 2394
			public static class SmallMinCaption
			{
				// Token: 0x17001A9A RID: 6810
				// (get) Token: 0x06007507 RID: 29959 RVA: 0x001A89F9 File Offset: 0x001A6BF9
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.SmallMinCaption.active == null)
						{
							VisualStyleElement.Window.SmallMinCaption.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallMinCaption.part, 1);
						}
						return VisualStyleElement.Window.SmallMinCaption.active;
					}
				}

				// Token: 0x17001A9B RID: 6811
				// (get) Token: 0x06007508 RID: 29960 RVA: 0x001A8A1C File Offset: 0x001A6C1C
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.SmallMinCaption.inactive == null)
						{
							VisualStyleElement.Window.SmallMinCaption.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallMinCaption.part, 2);
						}
						return VisualStyleElement.Window.SmallMinCaption.inactive;
					}
				}

				// Token: 0x17001A9C RID: 6812
				// (get) Token: 0x06007509 RID: 29961 RVA: 0x001A8A3F File Offset: 0x001A6C3F
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.SmallMinCaption.disabled == null)
						{
							VisualStyleElement.Window.SmallMinCaption.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallMinCaption.part, 3);
						}
						return VisualStyleElement.Window.SmallMinCaption.disabled;
					}
				}

				// Token: 0x0400474C RID: 18252
				private static readonly int part = 4;

				// Token: 0x0400474D RID: 18253
				private static VisualStyleElement active;

				// Token: 0x0400474E RID: 18254
				private static VisualStyleElement inactive;

				// Token: 0x0400474F RID: 18255
				private static VisualStyleElement disabled;
			}

			// Token: 0x0200095B RID: 2395
			public static class MaxCaption
			{
				// Token: 0x17001A9D RID: 6813
				// (get) Token: 0x0600750B RID: 29963 RVA: 0x001A8A6A File Offset: 0x001A6C6A
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.MaxCaption.active == null)
						{
							VisualStyleElement.Window.MaxCaption.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MaxCaption.part, 1);
						}
						return VisualStyleElement.Window.MaxCaption.active;
					}
				}

				// Token: 0x17001A9E RID: 6814
				// (get) Token: 0x0600750C RID: 29964 RVA: 0x001A8A8D File Offset: 0x001A6C8D
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.MaxCaption.inactive == null)
						{
							VisualStyleElement.Window.MaxCaption.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MaxCaption.part, 2);
						}
						return VisualStyleElement.Window.MaxCaption.inactive;
					}
				}

				// Token: 0x17001A9F RID: 6815
				// (get) Token: 0x0600750D RID: 29965 RVA: 0x001A8AB0 File Offset: 0x001A6CB0
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.MaxCaption.disabled == null)
						{
							VisualStyleElement.Window.MaxCaption.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MaxCaption.part, 3);
						}
						return VisualStyleElement.Window.MaxCaption.disabled;
					}
				}

				// Token: 0x04004750 RID: 18256
				private static readonly int part = 5;

				// Token: 0x04004751 RID: 18257
				private static VisualStyleElement active;

				// Token: 0x04004752 RID: 18258
				private static VisualStyleElement inactive;

				// Token: 0x04004753 RID: 18259
				private static VisualStyleElement disabled;
			}

			// Token: 0x0200095C RID: 2396
			public static class SmallMaxCaption
			{
				// Token: 0x17001AA0 RID: 6816
				// (get) Token: 0x0600750F RID: 29967 RVA: 0x001A8ADB File Offset: 0x001A6CDB
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.SmallMaxCaption.active == null)
						{
							VisualStyleElement.Window.SmallMaxCaption.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallMaxCaption.part, 1);
						}
						return VisualStyleElement.Window.SmallMaxCaption.active;
					}
				}

				// Token: 0x17001AA1 RID: 6817
				// (get) Token: 0x06007510 RID: 29968 RVA: 0x001A8AFE File Offset: 0x001A6CFE
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.SmallMaxCaption.inactive == null)
						{
							VisualStyleElement.Window.SmallMaxCaption.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallMaxCaption.part, 2);
						}
						return VisualStyleElement.Window.SmallMaxCaption.inactive;
					}
				}

				// Token: 0x17001AA2 RID: 6818
				// (get) Token: 0x06007511 RID: 29969 RVA: 0x001A8B21 File Offset: 0x001A6D21
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.SmallMaxCaption.disabled == null)
						{
							VisualStyleElement.Window.SmallMaxCaption.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallMaxCaption.part, 3);
						}
						return VisualStyleElement.Window.SmallMaxCaption.disabled;
					}
				}

				// Token: 0x04004754 RID: 18260
				private static readonly int part = 6;

				// Token: 0x04004755 RID: 18261
				private static VisualStyleElement active;

				// Token: 0x04004756 RID: 18262
				private static VisualStyleElement inactive;

				// Token: 0x04004757 RID: 18263
				private static VisualStyleElement disabled;
			}

			// Token: 0x0200095D RID: 2397
			public static class FrameLeft
			{
				// Token: 0x17001AA3 RID: 6819
				// (get) Token: 0x06007513 RID: 29971 RVA: 0x001A8B4C File Offset: 0x001A6D4C
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.FrameLeft.active == null)
						{
							VisualStyleElement.Window.FrameLeft.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.FrameLeft.part, 1);
						}
						return VisualStyleElement.Window.FrameLeft.active;
					}
				}

				// Token: 0x17001AA4 RID: 6820
				// (get) Token: 0x06007514 RID: 29972 RVA: 0x001A8B6F File Offset: 0x001A6D6F
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.FrameLeft.inactive == null)
						{
							VisualStyleElement.Window.FrameLeft.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.FrameLeft.part, 2);
						}
						return VisualStyleElement.Window.FrameLeft.inactive;
					}
				}

				// Token: 0x04004758 RID: 18264
				private static readonly int part = 7;

				// Token: 0x04004759 RID: 18265
				private static VisualStyleElement active;

				// Token: 0x0400475A RID: 18266
				private static VisualStyleElement inactive;
			}

			// Token: 0x0200095E RID: 2398
			public static class FrameRight
			{
				// Token: 0x17001AA5 RID: 6821
				// (get) Token: 0x06007516 RID: 29974 RVA: 0x001A8B9A File Offset: 0x001A6D9A
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.FrameRight.active == null)
						{
							VisualStyleElement.Window.FrameRight.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.FrameRight.part, 1);
						}
						return VisualStyleElement.Window.FrameRight.active;
					}
				}

				// Token: 0x17001AA6 RID: 6822
				// (get) Token: 0x06007517 RID: 29975 RVA: 0x001A8BBD File Offset: 0x001A6DBD
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.FrameRight.inactive == null)
						{
							VisualStyleElement.Window.FrameRight.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.FrameRight.part, 2);
						}
						return VisualStyleElement.Window.FrameRight.inactive;
					}
				}

				// Token: 0x0400475B RID: 18267
				private static readonly int part = 8;

				// Token: 0x0400475C RID: 18268
				private static VisualStyleElement active;

				// Token: 0x0400475D RID: 18269
				private static VisualStyleElement inactive;
			}

			// Token: 0x0200095F RID: 2399
			public static class FrameBottom
			{
				// Token: 0x17001AA7 RID: 6823
				// (get) Token: 0x06007519 RID: 29977 RVA: 0x001A8BE8 File Offset: 0x001A6DE8
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.FrameBottom.active == null)
						{
							VisualStyleElement.Window.FrameBottom.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.FrameBottom.part, 1);
						}
						return VisualStyleElement.Window.FrameBottom.active;
					}
				}

				// Token: 0x17001AA8 RID: 6824
				// (get) Token: 0x0600751A RID: 29978 RVA: 0x001A8C0B File Offset: 0x001A6E0B
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.FrameBottom.inactive == null)
						{
							VisualStyleElement.Window.FrameBottom.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.FrameBottom.part, 2);
						}
						return VisualStyleElement.Window.FrameBottom.inactive;
					}
				}

				// Token: 0x0400475E RID: 18270
				private static readonly int part = 9;

				// Token: 0x0400475F RID: 18271
				private static VisualStyleElement active;

				// Token: 0x04004760 RID: 18272
				private static VisualStyleElement inactive;
			}

			// Token: 0x02000960 RID: 2400
			public static class SmallFrameLeft
			{
				// Token: 0x17001AA9 RID: 6825
				// (get) Token: 0x0600751C RID: 29980 RVA: 0x001A8C37 File Offset: 0x001A6E37
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.SmallFrameLeft.active == null)
						{
							VisualStyleElement.Window.SmallFrameLeft.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallFrameLeft.part, 1);
						}
						return VisualStyleElement.Window.SmallFrameLeft.active;
					}
				}

				// Token: 0x17001AAA RID: 6826
				// (get) Token: 0x0600751D RID: 29981 RVA: 0x001A8C5A File Offset: 0x001A6E5A
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.SmallFrameLeft.inactive == null)
						{
							VisualStyleElement.Window.SmallFrameLeft.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallFrameLeft.part, 2);
						}
						return VisualStyleElement.Window.SmallFrameLeft.inactive;
					}
				}

				// Token: 0x04004761 RID: 18273
				private static readonly int part = 10;

				// Token: 0x04004762 RID: 18274
				private static VisualStyleElement active;

				// Token: 0x04004763 RID: 18275
				private static VisualStyleElement inactive;
			}

			// Token: 0x02000961 RID: 2401
			public static class SmallFrameRight
			{
				// Token: 0x17001AAB RID: 6827
				// (get) Token: 0x0600751F RID: 29983 RVA: 0x001A8C86 File Offset: 0x001A6E86
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.SmallFrameRight.active == null)
						{
							VisualStyleElement.Window.SmallFrameRight.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallFrameRight.part, 1);
						}
						return VisualStyleElement.Window.SmallFrameRight.active;
					}
				}

				// Token: 0x17001AAC RID: 6828
				// (get) Token: 0x06007520 RID: 29984 RVA: 0x001A8CA9 File Offset: 0x001A6EA9
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.SmallFrameRight.inactive == null)
						{
							VisualStyleElement.Window.SmallFrameRight.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallFrameRight.part, 2);
						}
						return VisualStyleElement.Window.SmallFrameRight.inactive;
					}
				}

				// Token: 0x04004764 RID: 18276
				private static readonly int part = 11;

				// Token: 0x04004765 RID: 18277
				private static VisualStyleElement active;

				// Token: 0x04004766 RID: 18278
				private static VisualStyleElement inactive;
			}

			// Token: 0x02000962 RID: 2402
			public static class SmallFrameBottom
			{
				// Token: 0x17001AAD RID: 6829
				// (get) Token: 0x06007522 RID: 29986 RVA: 0x001A8CD5 File Offset: 0x001A6ED5
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.SmallFrameBottom.active == null)
						{
							VisualStyleElement.Window.SmallFrameBottom.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallFrameBottom.part, 1);
						}
						return VisualStyleElement.Window.SmallFrameBottom.active;
					}
				}

				// Token: 0x17001AAE RID: 6830
				// (get) Token: 0x06007523 RID: 29987 RVA: 0x001A8CF8 File Offset: 0x001A6EF8
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.SmallFrameBottom.inactive == null)
						{
							VisualStyleElement.Window.SmallFrameBottom.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallFrameBottom.part, 2);
						}
						return VisualStyleElement.Window.SmallFrameBottom.inactive;
					}
				}

				// Token: 0x04004767 RID: 18279
				private static readonly int part = 12;

				// Token: 0x04004768 RID: 18280
				private static VisualStyleElement active;

				// Token: 0x04004769 RID: 18281
				private static VisualStyleElement inactive;
			}

			// Token: 0x02000963 RID: 2403
			public static class SysButton
			{
				// Token: 0x17001AAF RID: 6831
				// (get) Token: 0x06007525 RID: 29989 RVA: 0x001A8D24 File Offset: 0x001A6F24
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.SysButton.normal == null)
						{
							VisualStyleElement.Window.SysButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SysButton.part, 1);
						}
						return VisualStyleElement.Window.SysButton.normal;
					}
				}

				// Token: 0x17001AB0 RID: 6832
				// (get) Token: 0x06007526 RID: 29990 RVA: 0x001A8D47 File Offset: 0x001A6F47
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.SysButton.hot == null)
						{
							VisualStyleElement.Window.SysButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SysButton.part, 2);
						}
						return VisualStyleElement.Window.SysButton.hot;
					}
				}

				// Token: 0x17001AB1 RID: 6833
				// (get) Token: 0x06007527 RID: 29991 RVA: 0x001A8D6A File Offset: 0x001A6F6A
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.SysButton.pressed == null)
						{
							VisualStyleElement.Window.SysButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SysButton.part, 3);
						}
						return VisualStyleElement.Window.SysButton.pressed;
					}
				}

				// Token: 0x17001AB2 RID: 6834
				// (get) Token: 0x06007528 RID: 29992 RVA: 0x001A8D8D File Offset: 0x001A6F8D
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.SysButton.disabled == null)
						{
							VisualStyleElement.Window.SysButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SysButton.part, 4);
						}
						return VisualStyleElement.Window.SysButton.disabled;
					}
				}

				// Token: 0x0400476A RID: 18282
				private static readonly int part = 13;

				// Token: 0x0400476B RID: 18283
				private static VisualStyleElement normal;

				// Token: 0x0400476C RID: 18284
				private static VisualStyleElement hot;

				// Token: 0x0400476D RID: 18285
				private static VisualStyleElement pressed;

				// Token: 0x0400476E RID: 18286
				private static VisualStyleElement disabled;
			}

			// Token: 0x02000964 RID: 2404
			public static class MdiSysButton
			{
				// Token: 0x17001AB3 RID: 6835
				// (get) Token: 0x0600752A RID: 29994 RVA: 0x001A8DB9 File Offset: 0x001A6FB9
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.MdiSysButton.normal == null)
						{
							VisualStyleElement.Window.MdiSysButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiSysButton.part, 1);
						}
						return VisualStyleElement.Window.MdiSysButton.normal;
					}
				}

				// Token: 0x17001AB4 RID: 6836
				// (get) Token: 0x0600752B RID: 29995 RVA: 0x001A8DDC File Offset: 0x001A6FDC
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.MdiSysButton.hot == null)
						{
							VisualStyleElement.Window.MdiSysButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiSysButton.part, 2);
						}
						return VisualStyleElement.Window.MdiSysButton.hot;
					}
				}

				// Token: 0x17001AB5 RID: 6837
				// (get) Token: 0x0600752C RID: 29996 RVA: 0x001A8DFF File Offset: 0x001A6FFF
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.MdiSysButton.pressed == null)
						{
							VisualStyleElement.Window.MdiSysButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiSysButton.part, 3);
						}
						return VisualStyleElement.Window.MdiSysButton.pressed;
					}
				}

				// Token: 0x17001AB6 RID: 6838
				// (get) Token: 0x0600752D RID: 29997 RVA: 0x001A8E22 File Offset: 0x001A7022
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.MdiSysButton.disabled == null)
						{
							VisualStyleElement.Window.MdiSysButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiSysButton.part, 4);
						}
						return VisualStyleElement.Window.MdiSysButton.disabled;
					}
				}

				// Token: 0x0400476F RID: 18287
				private static readonly int part = 14;

				// Token: 0x04004770 RID: 18288
				private static VisualStyleElement normal;

				// Token: 0x04004771 RID: 18289
				private static VisualStyleElement hot;

				// Token: 0x04004772 RID: 18290
				private static VisualStyleElement pressed;

				// Token: 0x04004773 RID: 18291
				private static VisualStyleElement disabled;
			}

			// Token: 0x02000965 RID: 2405
			public static class MinButton
			{
				// Token: 0x17001AB7 RID: 6839
				// (get) Token: 0x0600752F RID: 29999 RVA: 0x001A8E4E File Offset: 0x001A704E
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.MinButton.normal == null)
						{
							VisualStyleElement.Window.MinButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MinButton.part, 1);
						}
						return VisualStyleElement.Window.MinButton.normal;
					}
				}

				// Token: 0x17001AB8 RID: 6840
				// (get) Token: 0x06007530 RID: 30000 RVA: 0x001A8E71 File Offset: 0x001A7071
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.MinButton.hot == null)
						{
							VisualStyleElement.Window.MinButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MinButton.part, 2);
						}
						return VisualStyleElement.Window.MinButton.hot;
					}
				}

				// Token: 0x17001AB9 RID: 6841
				// (get) Token: 0x06007531 RID: 30001 RVA: 0x001A8E94 File Offset: 0x001A7094
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.MinButton.pressed == null)
						{
							VisualStyleElement.Window.MinButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MinButton.part, 3);
						}
						return VisualStyleElement.Window.MinButton.pressed;
					}
				}

				// Token: 0x17001ABA RID: 6842
				// (get) Token: 0x06007532 RID: 30002 RVA: 0x001A8EB7 File Offset: 0x001A70B7
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.MinButton.disabled == null)
						{
							VisualStyleElement.Window.MinButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MinButton.part, 4);
						}
						return VisualStyleElement.Window.MinButton.disabled;
					}
				}

				// Token: 0x04004774 RID: 18292
				private static readonly int part = 15;

				// Token: 0x04004775 RID: 18293
				private static VisualStyleElement normal;

				// Token: 0x04004776 RID: 18294
				private static VisualStyleElement hot;

				// Token: 0x04004777 RID: 18295
				private static VisualStyleElement pressed;

				// Token: 0x04004778 RID: 18296
				private static VisualStyleElement disabled;
			}

			// Token: 0x02000966 RID: 2406
			public static class MdiMinButton
			{
				// Token: 0x17001ABB RID: 6843
				// (get) Token: 0x06007534 RID: 30004 RVA: 0x001A8EE3 File Offset: 0x001A70E3
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.MdiMinButton.normal == null)
						{
							VisualStyleElement.Window.MdiMinButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiMinButton.part, 1);
						}
						return VisualStyleElement.Window.MdiMinButton.normal;
					}
				}

				// Token: 0x17001ABC RID: 6844
				// (get) Token: 0x06007535 RID: 30005 RVA: 0x001A8F06 File Offset: 0x001A7106
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.MdiMinButton.hot == null)
						{
							VisualStyleElement.Window.MdiMinButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiMinButton.part, 2);
						}
						return VisualStyleElement.Window.MdiMinButton.hot;
					}
				}

				// Token: 0x17001ABD RID: 6845
				// (get) Token: 0x06007536 RID: 30006 RVA: 0x001A8F29 File Offset: 0x001A7129
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.MdiMinButton.pressed == null)
						{
							VisualStyleElement.Window.MdiMinButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiMinButton.part, 3);
						}
						return VisualStyleElement.Window.MdiMinButton.pressed;
					}
				}

				// Token: 0x17001ABE RID: 6846
				// (get) Token: 0x06007537 RID: 30007 RVA: 0x001A8F4C File Offset: 0x001A714C
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.MdiMinButton.disabled == null)
						{
							VisualStyleElement.Window.MdiMinButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiMinButton.part, 4);
						}
						return VisualStyleElement.Window.MdiMinButton.disabled;
					}
				}

				// Token: 0x04004779 RID: 18297
				private static readonly int part = 16;

				// Token: 0x0400477A RID: 18298
				private static VisualStyleElement normal;

				// Token: 0x0400477B RID: 18299
				private static VisualStyleElement hot;

				// Token: 0x0400477C RID: 18300
				private static VisualStyleElement pressed;

				// Token: 0x0400477D RID: 18301
				private static VisualStyleElement disabled;
			}

			// Token: 0x02000967 RID: 2407
			public static class MaxButton
			{
				// Token: 0x17001ABF RID: 6847
				// (get) Token: 0x06007539 RID: 30009 RVA: 0x001A8F78 File Offset: 0x001A7178
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.MaxButton.normal == null)
						{
							VisualStyleElement.Window.MaxButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MaxButton.part, 1);
						}
						return VisualStyleElement.Window.MaxButton.normal;
					}
				}

				// Token: 0x17001AC0 RID: 6848
				// (get) Token: 0x0600753A RID: 30010 RVA: 0x001A8F9B File Offset: 0x001A719B
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.MaxButton.hot == null)
						{
							VisualStyleElement.Window.MaxButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MaxButton.part, 2);
						}
						return VisualStyleElement.Window.MaxButton.hot;
					}
				}

				// Token: 0x17001AC1 RID: 6849
				// (get) Token: 0x0600753B RID: 30011 RVA: 0x001A8FBE File Offset: 0x001A71BE
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.MaxButton.pressed == null)
						{
							VisualStyleElement.Window.MaxButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MaxButton.part, 3);
						}
						return VisualStyleElement.Window.MaxButton.pressed;
					}
				}

				// Token: 0x17001AC2 RID: 6850
				// (get) Token: 0x0600753C RID: 30012 RVA: 0x001A8FE1 File Offset: 0x001A71E1
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.MaxButton.disabled == null)
						{
							VisualStyleElement.Window.MaxButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MaxButton.part, 4);
						}
						return VisualStyleElement.Window.MaxButton.disabled;
					}
				}

				// Token: 0x0400477E RID: 18302
				private static readonly int part = 17;

				// Token: 0x0400477F RID: 18303
				private static VisualStyleElement normal;

				// Token: 0x04004780 RID: 18304
				private static VisualStyleElement hot;

				// Token: 0x04004781 RID: 18305
				private static VisualStyleElement pressed;

				// Token: 0x04004782 RID: 18306
				private static VisualStyleElement disabled;
			}

			// Token: 0x02000968 RID: 2408
			public static class CloseButton
			{
				// Token: 0x17001AC3 RID: 6851
				// (get) Token: 0x0600753E RID: 30014 RVA: 0x001A900D File Offset: 0x001A720D
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.CloseButton.normal == null)
						{
							VisualStyleElement.Window.CloseButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.CloseButton.part, 1);
						}
						return VisualStyleElement.Window.CloseButton.normal;
					}
				}

				// Token: 0x17001AC4 RID: 6852
				// (get) Token: 0x0600753F RID: 30015 RVA: 0x001A9030 File Offset: 0x001A7230
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.CloseButton.hot == null)
						{
							VisualStyleElement.Window.CloseButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.CloseButton.part, 2);
						}
						return VisualStyleElement.Window.CloseButton.hot;
					}
				}

				// Token: 0x17001AC5 RID: 6853
				// (get) Token: 0x06007540 RID: 30016 RVA: 0x001A9053 File Offset: 0x001A7253
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.CloseButton.pressed == null)
						{
							VisualStyleElement.Window.CloseButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.CloseButton.part, 3);
						}
						return VisualStyleElement.Window.CloseButton.pressed;
					}
				}

				// Token: 0x17001AC6 RID: 6854
				// (get) Token: 0x06007541 RID: 30017 RVA: 0x001A9076 File Offset: 0x001A7276
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.CloseButton.disabled == null)
						{
							VisualStyleElement.Window.CloseButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.CloseButton.part, 4);
						}
						return VisualStyleElement.Window.CloseButton.disabled;
					}
				}

				// Token: 0x04004783 RID: 18307
				private static readonly int part = 18;

				// Token: 0x04004784 RID: 18308
				private static VisualStyleElement normal;

				// Token: 0x04004785 RID: 18309
				private static VisualStyleElement hot;

				// Token: 0x04004786 RID: 18310
				private static VisualStyleElement pressed;

				// Token: 0x04004787 RID: 18311
				private static VisualStyleElement disabled;
			}

			// Token: 0x02000969 RID: 2409
			public static class SmallCloseButton
			{
				// Token: 0x17001AC7 RID: 6855
				// (get) Token: 0x06007543 RID: 30019 RVA: 0x001A90A2 File Offset: 0x001A72A2
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.SmallCloseButton.normal == null)
						{
							VisualStyleElement.Window.SmallCloseButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallCloseButton.part, 1);
						}
						return VisualStyleElement.Window.SmallCloseButton.normal;
					}
				}

				// Token: 0x17001AC8 RID: 6856
				// (get) Token: 0x06007544 RID: 30020 RVA: 0x001A90C5 File Offset: 0x001A72C5
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.SmallCloseButton.hot == null)
						{
							VisualStyleElement.Window.SmallCloseButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallCloseButton.part, 2);
						}
						return VisualStyleElement.Window.SmallCloseButton.hot;
					}
				}

				// Token: 0x17001AC9 RID: 6857
				// (get) Token: 0x06007545 RID: 30021 RVA: 0x001A90E8 File Offset: 0x001A72E8
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.SmallCloseButton.pressed == null)
						{
							VisualStyleElement.Window.SmallCloseButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallCloseButton.part, 3);
						}
						return VisualStyleElement.Window.SmallCloseButton.pressed;
					}
				}

				// Token: 0x17001ACA RID: 6858
				// (get) Token: 0x06007546 RID: 30022 RVA: 0x001A910B File Offset: 0x001A730B
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.SmallCloseButton.disabled == null)
						{
							VisualStyleElement.Window.SmallCloseButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallCloseButton.part, 4);
						}
						return VisualStyleElement.Window.SmallCloseButton.disabled;
					}
				}

				// Token: 0x04004788 RID: 18312
				private static readonly int part = 19;

				// Token: 0x04004789 RID: 18313
				private static VisualStyleElement normal;

				// Token: 0x0400478A RID: 18314
				private static VisualStyleElement hot;

				// Token: 0x0400478B RID: 18315
				private static VisualStyleElement pressed;

				// Token: 0x0400478C RID: 18316
				private static VisualStyleElement disabled;
			}

			// Token: 0x0200096A RID: 2410
			public static class MdiCloseButton
			{
				// Token: 0x17001ACB RID: 6859
				// (get) Token: 0x06007548 RID: 30024 RVA: 0x001A9137 File Offset: 0x001A7337
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.MdiCloseButton.normal == null)
						{
							VisualStyleElement.Window.MdiCloseButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiCloseButton.part, 1);
						}
						return VisualStyleElement.Window.MdiCloseButton.normal;
					}
				}

				// Token: 0x17001ACC RID: 6860
				// (get) Token: 0x06007549 RID: 30025 RVA: 0x001A915A File Offset: 0x001A735A
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.MdiCloseButton.hot == null)
						{
							VisualStyleElement.Window.MdiCloseButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiCloseButton.part, 2);
						}
						return VisualStyleElement.Window.MdiCloseButton.hot;
					}
				}

				// Token: 0x17001ACD RID: 6861
				// (get) Token: 0x0600754A RID: 30026 RVA: 0x001A917D File Offset: 0x001A737D
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.MdiCloseButton.pressed == null)
						{
							VisualStyleElement.Window.MdiCloseButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiCloseButton.part, 3);
						}
						return VisualStyleElement.Window.MdiCloseButton.pressed;
					}
				}

				// Token: 0x17001ACE RID: 6862
				// (get) Token: 0x0600754B RID: 30027 RVA: 0x001A91A0 File Offset: 0x001A73A0
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.MdiCloseButton.disabled == null)
						{
							VisualStyleElement.Window.MdiCloseButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiCloseButton.part, 4);
						}
						return VisualStyleElement.Window.MdiCloseButton.disabled;
					}
				}

				// Token: 0x0400478D RID: 18317
				private static readonly int part = 20;

				// Token: 0x0400478E RID: 18318
				private static VisualStyleElement normal;

				// Token: 0x0400478F RID: 18319
				private static VisualStyleElement hot;

				// Token: 0x04004790 RID: 18320
				private static VisualStyleElement pressed;

				// Token: 0x04004791 RID: 18321
				private static VisualStyleElement disabled;
			}

			// Token: 0x0200096B RID: 2411
			public static class RestoreButton
			{
				// Token: 0x17001ACF RID: 6863
				// (get) Token: 0x0600754D RID: 30029 RVA: 0x001A91CC File Offset: 0x001A73CC
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.RestoreButton.normal == null)
						{
							VisualStyleElement.Window.RestoreButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.RestoreButton.part, 1);
						}
						return VisualStyleElement.Window.RestoreButton.normal;
					}
				}

				// Token: 0x17001AD0 RID: 6864
				// (get) Token: 0x0600754E RID: 30030 RVA: 0x001A91EF File Offset: 0x001A73EF
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.RestoreButton.hot == null)
						{
							VisualStyleElement.Window.RestoreButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.RestoreButton.part, 2);
						}
						return VisualStyleElement.Window.RestoreButton.hot;
					}
				}

				// Token: 0x17001AD1 RID: 6865
				// (get) Token: 0x0600754F RID: 30031 RVA: 0x001A9212 File Offset: 0x001A7412
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.RestoreButton.pressed == null)
						{
							VisualStyleElement.Window.RestoreButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.RestoreButton.part, 3);
						}
						return VisualStyleElement.Window.RestoreButton.pressed;
					}
				}

				// Token: 0x17001AD2 RID: 6866
				// (get) Token: 0x06007550 RID: 30032 RVA: 0x001A9235 File Offset: 0x001A7435
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.RestoreButton.disabled == null)
						{
							VisualStyleElement.Window.RestoreButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.RestoreButton.part, 4);
						}
						return VisualStyleElement.Window.RestoreButton.disabled;
					}
				}

				// Token: 0x04004792 RID: 18322
				private static readonly int part = 21;

				// Token: 0x04004793 RID: 18323
				private static VisualStyleElement normal;

				// Token: 0x04004794 RID: 18324
				private static VisualStyleElement hot;

				// Token: 0x04004795 RID: 18325
				private static VisualStyleElement pressed;

				// Token: 0x04004796 RID: 18326
				private static VisualStyleElement disabled;
			}

			// Token: 0x0200096C RID: 2412
			public static class MdiRestoreButton
			{
				// Token: 0x17001AD3 RID: 6867
				// (get) Token: 0x06007552 RID: 30034 RVA: 0x001A9261 File Offset: 0x001A7461
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.MdiRestoreButton.normal == null)
						{
							VisualStyleElement.Window.MdiRestoreButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiRestoreButton.part, 1);
						}
						return VisualStyleElement.Window.MdiRestoreButton.normal;
					}
				}

				// Token: 0x17001AD4 RID: 6868
				// (get) Token: 0x06007553 RID: 30035 RVA: 0x001A9284 File Offset: 0x001A7484
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.MdiRestoreButton.hot == null)
						{
							VisualStyleElement.Window.MdiRestoreButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiRestoreButton.part, 2);
						}
						return VisualStyleElement.Window.MdiRestoreButton.hot;
					}
				}

				// Token: 0x17001AD5 RID: 6869
				// (get) Token: 0x06007554 RID: 30036 RVA: 0x001A92A7 File Offset: 0x001A74A7
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.MdiRestoreButton.pressed == null)
						{
							VisualStyleElement.Window.MdiRestoreButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiRestoreButton.part, 3);
						}
						return VisualStyleElement.Window.MdiRestoreButton.pressed;
					}
				}

				// Token: 0x17001AD6 RID: 6870
				// (get) Token: 0x06007555 RID: 30037 RVA: 0x001A92CA File Offset: 0x001A74CA
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.MdiRestoreButton.disabled == null)
						{
							VisualStyleElement.Window.MdiRestoreButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiRestoreButton.part, 4);
						}
						return VisualStyleElement.Window.MdiRestoreButton.disabled;
					}
				}

				// Token: 0x04004797 RID: 18327
				private static readonly int part = 22;

				// Token: 0x04004798 RID: 18328
				private static VisualStyleElement normal;

				// Token: 0x04004799 RID: 18329
				private static VisualStyleElement hot;

				// Token: 0x0400479A RID: 18330
				private static VisualStyleElement pressed;

				// Token: 0x0400479B RID: 18331
				private static VisualStyleElement disabled;
			}

			// Token: 0x0200096D RID: 2413
			public static class HelpButton
			{
				// Token: 0x17001AD7 RID: 6871
				// (get) Token: 0x06007557 RID: 30039 RVA: 0x001A92F6 File Offset: 0x001A74F6
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.HelpButton.normal == null)
						{
							VisualStyleElement.Window.HelpButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HelpButton.part, 1);
						}
						return VisualStyleElement.Window.HelpButton.normal;
					}
				}

				// Token: 0x17001AD8 RID: 6872
				// (get) Token: 0x06007558 RID: 30040 RVA: 0x001A9319 File Offset: 0x001A7519
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.HelpButton.hot == null)
						{
							VisualStyleElement.Window.HelpButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HelpButton.part, 2);
						}
						return VisualStyleElement.Window.HelpButton.hot;
					}
				}

				// Token: 0x17001AD9 RID: 6873
				// (get) Token: 0x06007559 RID: 30041 RVA: 0x001A933C File Offset: 0x001A753C
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.HelpButton.pressed == null)
						{
							VisualStyleElement.Window.HelpButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HelpButton.part, 3);
						}
						return VisualStyleElement.Window.HelpButton.pressed;
					}
				}

				// Token: 0x17001ADA RID: 6874
				// (get) Token: 0x0600755A RID: 30042 RVA: 0x001A935F File Offset: 0x001A755F
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.HelpButton.disabled == null)
						{
							VisualStyleElement.Window.HelpButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HelpButton.part, 4);
						}
						return VisualStyleElement.Window.HelpButton.disabled;
					}
				}

				// Token: 0x0400479C RID: 18332
				private static readonly int part = 23;

				// Token: 0x0400479D RID: 18333
				private static VisualStyleElement normal;

				// Token: 0x0400479E RID: 18334
				private static VisualStyleElement hot;

				// Token: 0x0400479F RID: 18335
				private static VisualStyleElement pressed;

				// Token: 0x040047A0 RID: 18336
				private static VisualStyleElement disabled;
			}

			// Token: 0x0200096E RID: 2414
			public static class MdiHelpButton
			{
				// Token: 0x17001ADB RID: 6875
				// (get) Token: 0x0600755C RID: 30044 RVA: 0x001A938B File Offset: 0x001A758B
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.MdiHelpButton.normal == null)
						{
							VisualStyleElement.Window.MdiHelpButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiHelpButton.part, 1);
						}
						return VisualStyleElement.Window.MdiHelpButton.normal;
					}
				}

				// Token: 0x17001ADC RID: 6876
				// (get) Token: 0x0600755D RID: 30045 RVA: 0x001A93AE File Offset: 0x001A75AE
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.MdiHelpButton.hot == null)
						{
							VisualStyleElement.Window.MdiHelpButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiHelpButton.part, 2);
						}
						return VisualStyleElement.Window.MdiHelpButton.hot;
					}
				}

				// Token: 0x17001ADD RID: 6877
				// (get) Token: 0x0600755E RID: 30046 RVA: 0x001A93D1 File Offset: 0x001A75D1
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.MdiHelpButton.pressed == null)
						{
							VisualStyleElement.Window.MdiHelpButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiHelpButton.part, 3);
						}
						return VisualStyleElement.Window.MdiHelpButton.pressed;
					}
				}

				// Token: 0x17001ADE RID: 6878
				// (get) Token: 0x0600755F RID: 30047 RVA: 0x001A93F4 File Offset: 0x001A75F4
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.MdiHelpButton.disabled == null)
						{
							VisualStyleElement.Window.MdiHelpButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiHelpButton.part, 4);
						}
						return VisualStyleElement.Window.MdiHelpButton.disabled;
					}
				}

				// Token: 0x040047A1 RID: 18337
				private static readonly int part = 24;

				// Token: 0x040047A2 RID: 18338
				private static VisualStyleElement normal;

				// Token: 0x040047A3 RID: 18339
				private static VisualStyleElement hot;

				// Token: 0x040047A4 RID: 18340
				private static VisualStyleElement pressed;

				// Token: 0x040047A5 RID: 18341
				private static VisualStyleElement disabled;
			}

			// Token: 0x0200096F RID: 2415
			public static class HorizontalScroll
			{
				// Token: 0x17001ADF RID: 6879
				// (get) Token: 0x06007561 RID: 30049 RVA: 0x001A9420 File Offset: 0x001A7620
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.HorizontalScroll.normal == null)
						{
							VisualStyleElement.Window.HorizontalScroll.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HorizontalScroll.part, 1);
						}
						return VisualStyleElement.Window.HorizontalScroll.normal;
					}
				}

				// Token: 0x17001AE0 RID: 6880
				// (get) Token: 0x06007562 RID: 30050 RVA: 0x001A9443 File Offset: 0x001A7643
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.HorizontalScroll.hot == null)
						{
							VisualStyleElement.Window.HorizontalScroll.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HorizontalScroll.part, 2);
						}
						return VisualStyleElement.Window.HorizontalScroll.hot;
					}
				}

				// Token: 0x17001AE1 RID: 6881
				// (get) Token: 0x06007563 RID: 30051 RVA: 0x001A9466 File Offset: 0x001A7666
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.HorizontalScroll.pressed == null)
						{
							VisualStyleElement.Window.HorizontalScroll.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HorizontalScroll.part, 3);
						}
						return VisualStyleElement.Window.HorizontalScroll.pressed;
					}
				}

				// Token: 0x17001AE2 RID: 6882
				// (get) Token: 0x06007564 RID: 30052 RVA: 0x001A9489 File Offset: 0x001A7689
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.HorizontalScroll.disabled == null)
						{
							VisualStyleElement.Window.HorizontalScroll.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HorizontalScroll.part, 4);
						}
						return VisualStyleElement.Window.HorizontalScroll.disabled;
					}
				}

				// Token: 0x040047A6 RID: 18342
				private static readonly int part = 25;

				// Token: 0x040047A7 RID: 18343
				private static VisualStyleElement normal;

				// Token: 0x040047A8 RID: 18344
				private static VisualStyleElement hot;

				// Token: 0x040047A9 RID: 18345
				private static VisualStyleElement pressed;

				// Token: 0x040047AA RID: 18346
				private static VisualStyleElement disabled;
			}

			// Token: 0x02000970 RID: 2416
			public static class HorizontalThumb
			{
				// Token: 0x17001AE3 RID: 6883
				// (get) Token: 0x06007566 RID: 30054 RVA: 0x001A94B5 File Offset: 0x001A76B5
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.HorizontalThumb.normal == null)
						{
							VisualStyleElement.Window.HorizontalThumb.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HorizontalThumb.part, 1);
						}
						return VisualStyleElement.Window.HorizontalThumb.normal;
					}
				}

				// Token: 0x17001AE4 RID: 6884
				// (get) Token: 0x06007567 RID: 30055 RVA: 0x001A94D8 File Offset: 0x001A76D8
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.HorizontalThumb.hot == null)
						{
							VisualStyleElement.Window.HorizontalThumb.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HorizontalThumb.part, 2);
						}
						return VisualStyleElement.Window.HorizontalThumb.hot;
					}
				}

				// Token: 0x17001AE5 RID: 6885
				// (get) Token: 0x06007568 RID: 30056 RVA: 0x001A94FB File Offset: 0x001A76FB
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.HorizontalThumb.pressed == null)
						{
							VisualStyleElement.Window.HorizontalThumb.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HorizontalThumb.part, 3);
						}
						return VisualStyleElement.Window.HorizontalThumb.pressed;
					}
				}

				// Token: 0x17001AE6 RID: 6886
				// (get) Token: 0x06007569 RID: 30057 RVA: 0x001A951E File Offset: 0x001A771E
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.HorizontalThumb.disabled == null)
						{
							VisualStyleElement.Window.HorizontalThumb.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HorizontalThumb.part, 4);
						}
						return VisualStyleElement.Window.HorizontalThumb.disabled;
					}
				}

				// Token: 0x040047AB RID: 18347
				private static readonly int part = 26;

				// Token: 0x040047AC RID: 18348
				private static VisualStyleElement normal;

				// Token: 0x040047AD RID: 18349
				private static VisualStyleElement hot;

				// Token: 0x040047AE RID: 18350
				private static VisualStyleElement pressed;

				// Token: 0x040047AF RID: 18351
				private static VisualStyleElement disabled;
			}

			// Token: 0x02000971 RID: 2417
			public static class VerticalScroll
			{
				// Token: 0x17001AE7 RID: 6887
				// (get) Token: 0x0600756B RID: 30059 RVA: 0x001A954A File Offset: 0x001A774A
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.VerticalScroll.normal == null)
						{
							VisualStyleElement.Window.VerticalScroll.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.VerticalScroll.part, 1);
						}
						return VisualStyleElement.Window.VerticalScroll.normal;
					}
				}

				// Token: 0x17001AE8 RID: 6888
				// (get) Token: 0x0600756C RID: 30060 RVA: 0x001A956D File Offset: 0x001A776D
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.VerticalScroll.hot == null)
						{
							VisualStyleElement.Window.VerticalScroll.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.VerticalScroll.part, 2);
						}
						return VisualStyleElement.Window.VerticalScroll.hot;
					}
				}

				// Token: 0x17001AE9 RID: 6889
				// (get) Token: 0x0600756D RID: 30061 RVA: 0x001A9590 File Offset: 0x001A7790
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.VerticalScroll.pressed == null)
						{
							VisualStyleElement.Window.VerticalScroll.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.VerticalScroll.part, 3);
						}
						return VisualStyleElement.Window.VerticalScroll.pressed;
					}
				}

				// Token: 0x17001AEA RID: 6890
				// (get) Token: 0x0600756E RID: 30062 RVA: 0x001A95B3 File Offset: 0x001A77B3
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.VerticalScroll.disabled == null)
						{
							VisualStyleElement.Window.VerticalScroll.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.VerticalScroll.part, 4);
						}
						return VisualStyleElement.Window.VerticalScroll.disabled;
					}
				}

				// Token: 0x040047B0 RID: 18352
				private static readonly int part = 27;

				// Token: 0x040047B1 RID: 18353
				private static VisualStyleElement normal;

				// Token: 0x040047B2 RID: 18354
				private static VisualStyleElement hot;

				// Token: 0x040047B3 RID: 18355
				private static VisualStyleElement pressed;

				// Token: 0x040047B4 RID: 18356
				private static VisualStyleElement disabled;
			}

			// Token: 0x02000972 RID: 2418
			public static class VerticalThumb
			{
				// Token: 0x17001AEB RID: 6891
				// (get) Token: 0x06007570 RID: 30064 RVA: 0x001A95DF File Offset: 0x001A77DF
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.VerticalThumb.normal == null)
						{
							VisualStyleElement.Window.VerticalThumb.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.VerticalThumb.part, 1);
						}
						return VisualStyleElement.Window.VerticalThumb.normal;
					}
				}

				// Token: 0x17001AEC RID: 6892
				// (get) Token: 0x06007571 RID: 30065 RVA: 0x001A9602 File Offset: 0x001A7802
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.VerticalThumb.hot == null)
						{
							VisualStyleElement.Window.VerticalThumb.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.VerticalThumb.part, 2);
						}
						return VisualStyleElement.Window.VerticalThumb.hot;
					}
				}

				// Token: 0x17001AED RID: 6893
				// (get) Token: 0x06007572 RID: 30066 RVA: 0x001A9625 File Offset: 0x001A7825
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.VerticalThumb.pressed == null)
						{
							VisualStyleElement.Window.VerticalThumb.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.VerticalThumb.part, 3);
						}
						return VisualStyleElement.Window.VerticalThumb.pressed;
					}
				}

				// Token: 0x17001AEE RID: 6894
				// (get) Token: 0x06007573 RID: 30067 RVA: 0x001A9648 File Offset: 0x001A7848
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.VerticalThumb.disabled == null)
						{
							VisualStyleElement.Window.VerticalThumb.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.VerticalThumb.part, 4);
						}
						return VisualStyleElement.Window.VerticalThumb.disabled;
					}
				}

				// Token: 0x040047B5 RID: 18357
				private static readonly int part = 28;

				// Token: 0x040047B6 RID: 18358
				private static VisualStyleElement normal;

				// Token: 0x040047B7 RID: 18359
				private static VisualStyleElement hot;

				// Token: 0x040047B8 RID: 18360
				private static VisualStyleElement pressed;

				// Token: 0x040047B9 RID: 18361
				private static VisualStyleElement disabled;
			}

			// Token: 0x02000973 RID: 2419
			public static class Dialog
			{
				// Token: 0x17001AEF RID: 6895
				// (get) Token: 0x06007575 RID: 30069 RVA: 0x001A9674 File Offset: 0x001A7874
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.Dialog.normal == null)
						{
							VisualStyleElement.Window.Dialog.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.Dialog.part, 0);
						}
						return VisualStyleElement.Window.Dialog.normal;
					}
				}

				// Token: 0x040047BA RID: 18362
				private static readonly int part = 29;

				// Token: 0x040047BB RID: 18363
				private static VisualStyleElement normal;
			}

			// Token: 0x02000974 RID: 2420
			public static class CaptionSizingTemplate
			{
				// Token: 0x17001AF0 RID: 6896
				// (get) Token: 0x06007577 RID: 30071 RVA: 0x001A96A0 File Offset: 0x001A78A0
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.CaptionSizingTemplate.normal == null)
						{
							VisualStyleElement.Window.CaptionSizingTemplate.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.CaptionSizingTemplate.part, 0);
						}
						return VisualStyleElement.Window.CaptionSizingTemplate.normal;
					}
				}

				// Token: 0x040047BC RID: 18364
				private static readonly int part = 30;

				// Token: 0x040047BD RID: 18365
				private static VisualStyleElement normal;
			}

			// Token: 0x02000975 RID: 2421
			public static class SmallCaptionSizingTemplate
			{
				// Token: 0x17001AF1 RID: 6897
				// (get) Token: 0x06007579 RID: 30073 RVA: 0x001A96CC File Offset: 0x001A78CC
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.SmallCaptionSizingTemplate.normal == null)
						{
							VisualStyleElement.Window.SmallCaptionSizingTemplate.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallCaptionSizingTemplate.part, 0);
						}
						return VisualStyleElement.Window.SmallCaptionSizingTemplate.normal;
					}
				}

				// Token: 0x040047BE RID: 18366
				private static readonly int part = 31;

				// Token: 0x040047BF RID: 18367
				private static VisualStyleElement normal;
			}

			// Token: 0x02000976 RID: 2422
			public static class FrameLeftSizingTemplate
			{
				// Token: 0x17001AF2 RID: 6898
				// (get) Token: 0x0600757B RID: 30075 RVA: 0x001A96F8 File Offset: 0x001A78F8
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.FrameLeftSizingTemplate.normal == null)
						{
							VisualStyleElement.Window.FrameLeftSizingTemplate.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.FrameLeftSizingTemplate.part, 0);
						}
						return VisualStyleElement.Window.FrameLeftSizingTemplate.normal;
					}
				}

				// Token: 0x040047C0 RID: 18368
				private static readonly int part = 32;

				// Token: 0x040047C1 RID: 18369
				private static VisualStyleElement normal;
			}

			// Token: 0x02000977 RID: 2423
			public static class SmallFrameLeftSizingTemplate
			{
				// Token: 0x17001AF3 RID: 6899
				// (get) Token: 0x0600757D RID: 30077 RVA: 0x001A9724 File Offset: 0x001A7924
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.SmallFrameLeftSizingTemplate.normal == null)
						{
							VisualStyleElement.Window.SmallFrameLeftSizingTemplate.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallFrameLeftSizingTemplate.part, 0);
						}
						return VisualStyleElement.Window.SmallFrameLeftSizingTemplate.normal;
					}
				}

				// Token: 0x040047C2 RID: 18370
				private static readonly int part = 33;

				// Token: 0x040047C3 RID: 18371
				private static VisualStyleElement normal;
			}

			// Token: 0x02000978 RID: 2424
			public static class FrameRightSizingTemplate
			{
				// Token: 0x17001AF4 RID: 6900
				// (get) Token: 0x0600757F RID: 30079 RVA: 0x001A9750 File Offset: 0x001A7950
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.FrameRightSizingTemplate.normal == null)
						{
							VisualStyleElement.Window.FrameRightSizingTemplate.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.FrameRightSizingTemplate.part, 0);
						}
						return VisualStyleElement.Window.FrameRightSizingTemplate.normal;
					}
				}

				// Token: 0x040047C4 RID: 18372
				private static readonly int part = 34;

				// Token: 0x040047C5 RID: 18373
				private static VisualStyleElement normal;
			}

			// Token: 0x02000979 RID: 2425
			public static class SmallFrameRightSizingTemplate
			{
				// Token: 0x17001AF5 RID: 6901
				// (get) Token: 0x06007581 RID: 30081 RVA: 0x001A977C File Offset: 0x001A797C
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.SmallFrameRightSizingTemplate.normal == null)
						{
							VisualStyleElement.Window.SmallFrameRightSizingTemplate.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallFrameRightSizingTemplate.part, 0);
						}
						return VisualStyleElement.Window.SmallFrameRightSizingTemplate.normal;
					}
				}

				// Token: 0x040047C6 RID: 18374
				private static readonly int part = 35;

				// Token: 0x040047C7 RID: 18375
				private static VisualStyleElement normal;
			}

			// Token: 0x0200097A RID: 2426
			public static class FrameBottomSizingTemplate
			{
				// Token: 0x17001AF6 RID: 6902
				// (get) Token: 0x06007583 RID: 30083 RVA: 0x001A97A8 File Offset: 0x001A79A8
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.FrameBottomSizingTemplate.normal == null)
						{
							VisualStyleElement.Window.FrameBottomSizingTemplate.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.FrameBottomSizingTemplate.part, 0);
						}
						return VisualStyleElement.Window.FrameBottomSizingTemplate.normal;
					}
				}

				// Token: 0x040047C8 RID: 18376
				private static readonly int part = 36;

				// Token: 0x040047C9 RID: 18377
				private static VisualStyleElement normal;
			}

			// Token: 0x0200097B RID: 2427
			public static class SmallFrameBottomSizingTemplate
			{
				// Token: 0x17001AF7 RID: 6903
				// (get) Token: 0x06007585 RID: 30085 RVA: 0x001A97D4 File Offset: 0x001A79D4
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.SmallFrameBottomSizingTemplate.normal == null)
						{
							VisualStyleElement.Window.SmallFrameBottomSizingTemplate.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallFrameBottomSizingTemplate.part, 0);
						}
						return VisualStyleElement.Window.SmallFrameBottomSizingTemplate.normal;
					}
				}

				// Token: 0x040047CA RID: 18378
				private static readonly int part = 37;

				// Token: 0x040047CB RID: 18379
				private static VisualStyleElement normal;
			}
		}
	}
}
