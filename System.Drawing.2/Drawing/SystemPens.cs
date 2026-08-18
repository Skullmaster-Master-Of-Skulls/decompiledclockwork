using System;

namespace System.Drawing
{
	// Token: 0x02000037 RID: 55
	public sealed class SystemPens
	{
		// Token: 0x06000563 RID: 1379 RVA: 0x00003800 File Offset: 0x00001A00
		private SystemPens()
		{
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00018074 File Offset: 0x00016274
		public static Pen ActiveBorder
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.ActiveBorder);
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00018080 File Offset: 0x00016280
		public static Pen ActiveCaption
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.ActiveCaption);
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x0001808C File Offset: 0x0001628C
		public static Pen ActiveCaptionText
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.ActiveCaptionText);
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00018098 File Offset: 0x00016298
		public static Pen AppWorkspace
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.AppWorkspace);
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x000180A4 File Offset: 0x000162A4
		public static Pen ButtonFace
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.ButtonFace);
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x000180B0 File Offset: 0x000162B0
		public static Pen ButtonHighlight
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.ButtonHighlight);
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x000180BC File Offset: 0x000162BC
		public static Pen ButtonShadow
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.ButtonShadow);
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x000180C8 File Offset: 0x000162C8
		public static Pen Control
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.Control);
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x000180D4 File Offset: 0x000162D4
		public static Pen ControlText
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.ControlText);
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x000180E0 File Offset: 0x000162E0
		public static Pen ControlDark
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.ControlDark);
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x000180EC File Offset: 0x000162EC
		public static Pen ControlDarkDark
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.ControlDarkDark);
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x000180F8 File Offset: 0x000162F8
		public static Pen ControlLight
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.ControlLight);
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00018104 File Offset: 0x00016304
		public static Pen ControlLightLight
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.ControlLightLight);
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00018110 File Offset: 0x00016310
		public static Pen Desktop
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.Desktop);
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0001811C File Offset: 0x0001631C
		public static Pen GradientActiveCaption
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.GradientActiveCaption);
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00018128 File Offset: 0x00016328
		public static Pen GradientInactiveCaption
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.GradientInactiveCaption);
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00018134 File Offset: 0x00016334
		public static Pen GrayText
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.GrayText);
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00018140 File Offset: 0x00016340
		public static Pen Highlight
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.Highlight);
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0001814C File Offset: 0x0001634C
		public static Pen HighlightText
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.HighlightText);
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00018158 File Offset: 0x00016358
		public static Pen HotTrack
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.HotTrack);
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00018164 File Offset: 0x00016364
		public static Pen InactiveBorder
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.InactiveBorder);
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00018170 File Offset: 0x00016370
		public static Pen InactiveCaption
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.InactiveCaption);
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0001817C File Offset: 0x0001637C
		public static Pen InactiveCaptionText
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.InactiveCaptionText);
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x00018188 File Offset: 0x00016388
		public static Pen Info
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.Info);
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00018194 File Offset: 0x00016394
		public static Pen InfoText
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.InfoText);
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x000181A0 File Offset: 0x000163A0
		public static Pen Menu
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.Menu);
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x000181AC File Offset: 0x000163AC
		public static Pen MenuBar
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.MenuBar);
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x000181B8 File Offset: 0x000163B8
		public static Pen MenuHighlight
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.MenuHighlight);
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x000181C4 File Offset: 0x000163C4
		public static Pen MenuText
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.MenuText);
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x000181D0 File Offset: 0x000163D0
		public static Pen ScrollBar
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.ScrollBar);
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x000181DC File Offset: 0x000163DC
		public static Pen Window
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.Window);
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x000181E8 File Offset: 0x000163E8
		public static Pen WindowFrame
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.WindowFrame);
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x000181F4 File Offset: 0x000163F4
		public static Pen WindowText
		{
			get
			{
				return SystemPens.FromSystemColor(SystemColors.WindowText);
			}
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00018200 File Offset: 0x00016400
		public static Pen FromSystemColor(Color c)
		{
			if (!c.IsSystemColor)
			{
				throw new ArgumentException(SR.GetString("ColorNotSystemColor", new object[]
				{
					c.ToString()
				}));
			}
			Pen[] array = (Pen[])SafeNativeMethods.Gdip.ThreadData[SystemPens.SystemPensKey];
			if (array == null)
			{
				array = new Pen[33];
				SafeNativeMethods.Gdip.ThreadData[SystemPens.SystemPensKey] = array;
			}
			int num = (int)c.ToKnownColor();
			if (num > 167)
			{
				num -= 141;
			}
			num--;
			if (array[num] == null)
			{
				array[num] = new Pen(c, true);
			}
			return array[num];
		}

		// Token: 0x04000328 RID: 808
		private static readonly object SystemPensKey = new object();
	}
}
