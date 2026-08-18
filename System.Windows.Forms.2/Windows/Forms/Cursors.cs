using System;

namespace System.Windows.Forms
{
	// Token: 0x02000179 RID: 377
	public sealed class Cursors
	{
		// Token: 0x0600140D RID: 5133 RVA: 0x00002843 File Offset: 0x00000A43
		private Cursors()
		{
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x00043922 File Offset: 0x00041B22
		internal static Cursor KnownCursorFromHCursor(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				return null;
			}
			return new Cursor(handle);
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x00043939 File Offset: 0x00041B39
		public static Cursor AppStarting
		{
			get
			{
				if (Cursors.appStarting == null)
				{
					Cursors.appStarting = new Cursor(32650, 0);
				}
				return Cursors.appStarting;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x0004395D File Offset: 0x00041B5D
		public static Cursor Arrow
		{
			get
			{
				if (Cursors.arrow == null)
				{
					Cursors.arrow = new Cursor(32512, 0);
				}
				return Cursors.arrow;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x00043981 File Offset: 0x00041B81
		public static Cursor Cross
		{
			get
			{
				if (Cursors.cross == null)
				{
					Cursors.cross = new Cursor(32515, 0);
				}
				return Cursors.cross;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x000439A5 File Offset: 0x00041BA5
		public static Cursor Default
		{
			get
			{
				if (Cursors.defaultCursor == null)
				{
					Cursors.defaultCursor = new Cursor(32512, 0);
				}
				return Cursors.defaultCursor;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x000439C9 File Offset: 0x00041BC9
		public static Cursor IBeam
		{
			get
			{
				if (Cursors.iBeam == null)
				{
					Cursors.iBeam = new Cursor(32513, 0);
				}
				return Cursors.iBeam;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x000439ED File Offset: 0x00041BED
		public static Cursor No
		{
			get
			{
				if (Cursors.no == null)
				{
					Cursors.no = new Cursor(32648, 0);
				}
				return Cursors.no;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x00043A11 File Offset: 0x00041C11
		public static Cursor SizeAll
		{
			get
			{
				if (Cursors.sizeAll == null)
				{
					Cursors.sizeAll = new Cursor(32646, 0);
				}
				return Cursors.sizeAll;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x00043A35 File Offset: 0x00041C35
		public static Cursor SizeNESW
		{
			get
			{
				if (Cursors.sizeNESW == null)
				{
					Cursors.sizeNESW = new Cursor(32643, 0);
				}
				return Cursors.sizeNESW;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x00043A59 File Offset: 0x00041C59
		public static Cursor SizeNS
		{
			get
			{
				if (Cursors.sizeNS == null)
				{
					Cursors.sizeNS = new Cursor(32645, 0);
				}
				return Cursors.sizeNS;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x00043A7D File Offset: 0x00041C7D
		public static Cursor SizeNWSE
		{
			get
			{
				if (Cursors.sizeNWSE == null)
				{
					Cursors.sizeNWSE = new Cursor(32642, 0);
				}
				return Cursors.sizeNWSE;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x00043AA1 File Offset: 0x00041CA1
		public static Cursor SizeWE
		{
			get
			{
				if (Cursors.sizeWE == null)
				{
					Cursors.sizeWE = new Cursor(32644, 0);
				}
				return Cursors.sizeWE;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x00043AC5 File Offset: 0x00041CC5
		public static Cursor UpArrow
		{
			get
			{
				if (Cursors.upArrow == null)
				{
					Cursors.upArrow = new Cursor(32516, 0);
				}
				return Cursors.upArrow;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x00043AE9 File Offset: 0x00041CE9
		public static Cursor WaitCursor
		{
			get
			{
				if (Cursors.wait == null)
				{
					Cursors.wait = new Cursor(32514, 0);
				}
				return Cursors.wait;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x00043B0D File Offset: 0x00041D0D
		public static Cursor Help
		{
			get
			{
				if (Cursors.help == null)
				{
					Cursors.help = new Cursor(32651, 0);
				}
				return Cursors.help;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x00043B31 File Offset: 0x00041D31
		public static Cursor HSplit
		{
			get
			{
				if (Cursors.hSplit == null)
				{
					Cursors.hSplit = new Cursor("hsplit.cur", 0);
				}
				return Cursors.hSplit;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x00043B55 File Offset: 0x00041D55
		public static Cursor VSplit
		{
			get
			{
				if (Cursors.vSplit == null)
				{
					Cursors.vSplit = new Cursor("vsplit.cur", 0);
				}
				return Cursors.vSplit;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x00043B79 File Offset: 0x00041D79
		public static Cursor NoMove2D
		{
			get
			{
				if (Cursors.noMove2D == null)
				{
					Cursors.noMove2D = new Cursor("nomove2d.cur", 0);
				}
				return Cursors.noMove2D;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x00043B9D File Offset: 0x00041D9D
		public static Cursor NoMoveHoriz
		{
			get
			{
				if (Cursors.noMoveHoriz == null)
				{
					Cursors.noMoveHoriz = new Cursor("nomoveh.cur", 0);
				}
				return Cursors.noMoveHoriz;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x00043BC1 File Offset: 0x00041DC1
		public static Cursor NoMoveVert
		{
			get
			{
				if (Cursors.noMoveVert == null)
				{
					Cursors.noMoveVert = new Cursor("nomovev.cur", 0);
				}
				return Cursors.noMoveVert;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x00043BE5 File Offset: 0x00041DE5
		public static Cursor PanEast
		{
			get
			{
				if (Cursors.panEast == null)
				{
					Cursors.panEast = new Cursor("east.cur", 0);
				}
				return Cursors.panEast;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x00043C09 File Offset: 0x00041E09
		public static Cursor PanNE
		{
			get
			{
				if (Cursors.panNE == null)
				{
					Cursors.panNE = new Cursor("ne.cur", 0);
				}
				return Cursors.panNE;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x00043C2D File Offset: 0x00041E2D
		public static Cursor PanNorth
		{
			get
			{
				if (Cursors.panNorth == null)
				{
					Cursors.panNorth = new Cursor("north.cur", 0);
				}
				return Cursors.panNorth;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x00043C51 File Offset: 0x00041E51
		public static Cursor PanNW
		{
			get
			{
				if (Cursors.panNW == null)
				{
					Cursors.panNW = new Cursor("nw.cur", 0);
				}
				return Cursors.panNW;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x00043C75 File Offset: 0x00041E75
		public static Cursor PanSE
		{
			get
			{
				if (Cursors.panSE == null)
				{
					Cursors.panSE = new Cursor("se.cur", 0);
				}
				return Cursors.panSE;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x00043C99 File Offset: 0x00041E99
		public static Cursor PanSouth
		{
			get
			{
				if (Cursors.panSouth == null)
				{
					Cursors.panSouth = new Cursor("south.cur", 0);
				}
				return Cursors.panSouth;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x00043CBD File Offset: 0x00041EBD
		public static Cursor PanSW
		{
			get
			{
				if (Cursors.panSW == null)
				{
					Cursors.panSW = new Cursor("sw.cur", 0);
				}
				return Cursors.panSW;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x00043CE1 File Offset: 0x00041EE1
		public static Cursor PanWest
		{
			get
			{
				if (Cursors.panWest == null)
				{
					Cursors.panWest = new Cursor("west.cur", 0);
				}
				return Cursors.panWest;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x00043D05 File Offset: 0x00041F05
		public static Cursor Hand
		{
			get
			{
				if (Cursors.hand == null)
				{
					Cursors.hand = new Cursor("hand.cur", 0);
				}
				return Cursors.hand;
			}
		}

		// Token: 0x0400095D RID: 2397
		private static Cursor appStarting;

		// Token: 0x0400095E RID: 2398
		private static Cursor arrow;

		// Token: 0x0400095F RID: 2399
		private static Cursor cross;

		// Token: 0x04000960 RID: 2400
		private static Cursor defaultCursor;

		// Token: 0x04000961 RID: 2401
		private static Cursor iBeam;

		// Token: 0x04000962 RID: 2402
		private static Cursor no;

		// Token: 0x04000963 RID: 2403
		private static Cursor sizeAll;

		// Token: 0x04000964 RID: 2404
		private static Cursor sizeNESW;

		// Token: 0x04000965 RID: 2405
		private static Cursor sizeNS;

		// Token: 0x04000966 RID: 2406
		private static Cursor sizeNWSE;

		// Token: 0x04000967 RID: 2407
		private static Cursor sizeWE;

		// Token: 0x04000968 RID: 2408
		private static Cursor upArrow;

		// Token: 0x04000969 RID: 2409
		private static Cursor wait;

		// Token: 0x0400096A RID: 2410
		private static Cursor help;

		// Token: 0x0400096B RID: 2411
		private static Cursor hSplit;

		// Token: 0x0400096C RID: 2412
		private static Cursor vSplit;

		// Token: 0x0400096D RID: 2413
		private static Cursor noMove2D;

		// Token: 0x0400096E RID: 2414
		private static Cursor noMoveHoriz;

		// Token: 0x0400096F RID: 2415
		private static Cursor noMoveVert;

		// Token: 0x04000970 RID: 2416
		private static Cursor panEast;

		// Token: 0x04000971 RID: 2417
		private static Cursor panNE;

		// Token: 0x04000972 RID: 2418
		private static Cursor panNorth;

		// Token: 0x04000973 RID: 2419
		private static Cursor panNW;

		// Token: 0x04000974 RID: 2420
		private static Cursor panSE;

		// Token: 0x04000975 RID: 2421
		private static Cursor panSouth;

		// Token: 0x04000976 RID: 2422
		private static Cursor panSW;

		// Token: 0x04000977 RID: 2423
		private static Cursor panWest;

		// Token: 0x04000978 RID: 2424
		private static Cursor hand;
	}
}
