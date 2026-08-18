using System;
using System.Collections.Generic;
using System.Drawing;

namespace iTextSharp.text.pdf.codec.wmf
{
	// Token: 0x020003A4 RID: 932
	public class MetaState
	{
		// Token: 0x0600204A RID: 8266 RVA: 0x000BFC94 File Offset: 0x000BEC94
		public MetaState()
		{
			this.savedStates = new Stack<MetaState>();
			this.MetaObjects = new List<MetaObject>();
			this.currentPoint = new Point(0, 0);
			this.currentPen = new MetaPen();
			this.currentBrush = new MetaBrush();
			this.currentFont = new MetaFont();
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x000BFD20 File Offset: 0x000BED20
		public MetaState(MetaState state)
		{
			this.metaState = state;
		}

		// Token: 0x17000587 RID: 1415
		// (set) Token: 0x0600204C RID: 8268 RVA: 0x000BFD70 File Offset: 0x000BED70
		public MetaState metaState
		{
			set
			{
				this.savedStates = value.savedStates;
				this.MetaObjects = value.MetaObjects;
				this.currentPoint = value.currentPoint;
				this.currentPen = value.currentPen;
				this.currentBrush = value.currentBrush;
				this.currentFont = value.currentFont;
				this.currentBackgroundColor = value.currentBackgroundColor;
				this.currentTextColor = value.currentTextColor;
				this.backgroundMode = value.backgroundMode;
				this.polyFillMode = value.polyFillMode;
				this.textAlign = value.textAlign;
				this.lineJoin = value.lineJoin;
				this.offsetWx = value.offsetWx;
				this.offsetWy = value.offsetWy;
				this.extentWx = value.extentWx;
				this.extentWy = value.extentWy;
				this.scalingX = value.scalingX;
				this.scalingY = value.scalingY;
			}
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x000BFE58 File Offset: 0x000BEE58
		public void AddMetaObject(MetaObject obj)
		{
			for (int i = 0; i < this.MetaObjects.Count; i++)
			{
				if (this.MetaObjects[i] == null)
				{
					this.MetaObjects[i] = obj;
					return;
				}
			}
			this.MetaObjects.Add(obj);
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x000BFEA4 File Offset: 0x000BEEA4
		public void SelectMetaObject(int index, PdfContentByte cb)
		{
			MetaObject metaObject = this.MetaObjects[index];
			if (metaObject == null)
			{
				return;
			}
			switch (metaObject.Type)
			{
			case 1:
			{
				this.currentPen = (MetaPen)metaObject;
				int style = this.currentPen.Style;
				if (style != 5)
				{
					BaseColor color = this.currentPen.Color;
					cb.SetColorStroke(color);
					cb.SetLineWidth(Math.Abs((float)this.currentPen.PenWidth * this.scalingX / (float)this.extentWx));
					switch (style)
					{
					case 1:
						cb.SetLineDash(18f, 6f, 0f);
						return;
					case 2:
						cb.SetLineDash(3f, 0f);
						return;
					case 3:
						cb.SetLiteral("[9 6 3 6]0 d\n");
						return;
					case 4:
						cb.SetLiteral("[9 3 3 3 3 3]0 d\n");
						return;
					default:
						cb.SetLineDash(0f);
						return;
					}
				}
				break;
			}
			case 2:
			{
				this.currentBrush = (MetaBrush)metaObject;
				int style = this.currentBrush.Style;
				if (style == 0)
				{
					BaseColor color2 = this.currentBrush.Color;
					cb.SetColorFill(color2);
					return;
				}
				if (style == 2)
				{
					BaseColor colorFill = this.currentBackgroundColor;
					cb.SetColorFill(colorFill);
					return;
				}
				break;
			}
			case 3:
				this.currentFont = (MetaFont)metaObject;
				break;
			default:
				return;
			}
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x000BFFF4 File Offset: 0x000BEFF4
		public void DeleteMetaObject(int index)
		{
			this.MetaObjects[index] = null;
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x000C0004 File Offset: 0x000BF004
		public void SaveState(PdfContentByte cb)
		{
			cb.SaveState();
			MetaState item = new MetaState(this);
			this.savedStates.Push(item);
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x000C002C File Offset: 0x000BF02C
		public void RestoreState(int index, PdfContentByte cb)
		{
			int num;
			if (index < 0)
			{
				num = Math.Min(-index, this.savedStates.Count);
			}
			else
			{
				num = Math.Max(this.savedStates.Count - index, 0);
			}
			if (num == 0)
			{
				return;
			}
			MetaState metaState = null;
			while (num-- != 0)
			{
				cb.RestoreState();
				metaState = this.savedStates.Pop();
			}
			this.metaState = metaState;
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x000C0090 File Offset: 0x000BF090
		public void Cleanup(PdfContentByte cb)
		{
			int count = this.savedStates.Count;
			while (count-- > 0)
			{
				cb.RestoreState();
			}
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x000C00B9 File Offset: 0x000BF0B9
		public float TransformX(int x)
		{
			return ((float)x - (float)this.offsetWx) * this.scalingX / (float)this.extentWx;
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x000C00D4 File Offset: 0x000BF0D4
		public float TransformY(int y)
		{
			return (1f - ((float)y - (float)this.offsetWy) / (float)this.extentWy) * this.scalingY;
		}

		// Token: 0x17000588 RID: 1416
		// (set) Token: 0x06002055 RID: 8277 RVA: 0x000C00F5 File Offset: 0x000BF0F5
		public float ScalingX
		{
			set
			{
				this.scalingX = value;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (set) Token: 0x06002056 RID: 8278 RVA: 0x000C00FE File Offset: 0x000BF0FE
		public float ScalingY
		{
			set
			{
				this.scalingY = value;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (set) Token: 0x06002057 RID: 8279 RVA: 0x000C0107 File Offset: 0x000BF107
		public int OffsetWx
		{
			set
			{
				this.offsetWx = value;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (set) Token: 0x06002058 RID: 8280 RVA: 0x000C0110 File Offset: 0x000BF110
		public int OffsetWy
		{
			set
			{
				this.offsetWy = value;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (set) Token: 0x06002059 RID: 8281 RVA: 0x000C0119 File Offset: 0x000BF119
		public int ExtentWx
		{
			set
			{
				this.extentWx = value;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (set) Token: 0x0600205A RID: 8282 RVA: 0x000C0122 File Offset: 0x000BF122
		public int ExtentWy
		{
			set
			{
				this.extentWy = value;
			}
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x000C012C File Offset: 0x000BF12C
		public float TransformAngle(float angle)
		{
			float num = (this.scalingY < 0f) ? (-angle) : angle;
			return (float)((this.scalingX < 0f) ? (3.141592653589793 - (double)num) : ((double)num));
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600205C RID: 8284 RVA: 0x000C016A File Offset: 0x000BF16A
		// (set) Token: 0x0600205D RID: 8285 RVA: 0x000C0172 File Offset: 0x000BF172
		public Point CurrentPoint
		{
			get
			{
				return this.currentPoint;
			}
			set
			{
				this.currentPoint = value;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x0600205E RID: 8286 RVA: 0x000C017B File Offset: 0x000BF17B
		public MetaBrush CurrentBrush
		{
			get
			{
				return this.currentBrush;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x000C0183 File Offset: 0x000BF183
		public MetaPen CurrentPen
		{
			get
			{
				return this.currentPen;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06002060 RID: 8288 RVA: 0x000C018B File Offset: 0x000BF18B
		public MetaFont CurrentFont
		{
			get
			{
				return this.currentFont;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06002061 RID: 8289 RVA: 0x000C0193 File Offset: 0x000BF193
		// (set) Token: 0x06002062 RID: 8290 RVA: 0x000C019B File Offset: 0x000BF19B
		public BaseColor CurrentBackgroundColor
		{
			get
			{
				return this.currentBackgroundColor;
			}
			set
			{
				this.currentBackgroundColor = value;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06002063 RID: 8291 RVA: 0x000C01A4 File Offset: 0x000BF1A4
		// (set) Token: 0x06002064 RID: 8292 RVA: 0x000C01AC File Offset: 0x000BF1AC
		public BaseColor CurrentTextColor
		{
			get
			{
				return this.currentTextColor;
			}
			set
			{
				this.currentTextColor = value;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06002065 RID: 8293 RVA: 0x000C01B5 File Offset: 0x000BF1B5
		// (set) Token: 0x06002066 RID: 8294 RVA: 0x000C01BD File Offset: 0x000BF1BD
		public int BackgroundMode
		{
			get
			{
				return this.backgroundMode;
			}
			set
			{
				this.backgroundMode = value;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06002067 RID: 8295 RVA: 0x000C01C6 File Offset: 0x000BF1C6
		// (set) Token: 0x06002068 RID: 8296 RVA: 0x000C01CE File Offset: 0x000BF1CE
		public int TextAlign
		{
			get
			{
				return this.textAlign;
			}
			set
			{
				this.textAlign = value;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06002069 RID: 8297 RVA: 0x000C01D7 File Offset: 0x000BF1D7
		// (set) Token: 0x0600206A RID: 8298 RVA: 0x000C01DF File Offset: 0x000BF1DF
		public int PolyFillMode
		{
			get
			{
				return this.polyFillMode;
			}
			set
			{
				this.polyFillMode = value;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (set) Token: 0x0600206B RID: 8299 RVA: 0x000C01E8 File Offset: 0x000BF1E8
		public PdfContentByte LineJoinRectangle
		{
			set
			{
				if (this.lineJoin != 0)
				{
					this.lineJoin = 0;
					value.SetLineJoin(0);
				}
			}
		}

		// Token: 0x17000598 RID: 1432
		// (set) Token: 0x0600206C RID: 8300 RVA: 0x000C0200 File Offset: 0x000BF200
		public PdfContentByte LineJoinPolygon
		{
			set
			{
				if (this.lineJoin == 0)
				{
					this.lineJoin = 1;
					value.SetLineJoin(1);
				}
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x0600206D RID: 8301 RVA: 0x000C0218 File Offset: 0x000BF218
		public bool LineNeutral
		{
			get
			{
				return this.lineJoin == 0;
			}
		}

		// Token: 0x0400162D RID: 5677
		public static int TA_NOUPDATECP = 0;

		// Token: 0x0400162E RID: 5678
		public static int TA_UPDATECP = 1;

		// Token: 0x0400162F RID: 5679
		public static int TA_LEFT = 0;

		// Token: 0x04001630 RID: 5680
		public static int TA_RIGHT = 2;

		// Token: 0x04001631 RID: 5681
		public static int TA_CENTER = 6;

		// Token: 0x04001632 RID: 5682
		public static int TA_TOP = 0;

		// Token: 0x04001633 RID: 5683
		public static int TA_BOTTOM = 8;

		// Token: 0x04001634 RID: 5684
		public static int TA_BASELINE = 24;

		// Token: 0x04001635 RID: 5685
		public static int TRANSPARENT = 1;

		// Token: 0x04001636 RID: 5686
		public static int OPAQUE = 2;

		// Token: 0x04001637 RID: 5687
		public static int ALTERNATE = 1;

		// Token: 0x04001638 RID: 5688
		public static int WINDING = 2;

		// Token: 0x04001639 RID: 5689
		public Stack<MetaState> savedStates;

		// Token: 0x0400163A RID: 5690
		public List<MetaObject> MetaObjects;

		// Token: 0x0400163B RID: 5691
		public Point currentPoint;

		// Token: 0x0400163C RID: 5692
		public MetaPen currentPen;

		// Token: 0x0400163D RID: 5693
		public MetaBrush currentBrush;

		// Token: 0x0400163E RID: 5694
		public MetaFont currentFont;

		// Token: 0x0400163F RID: 5695
		public BaseColor currentBackgroundColor = BaseColor.WHITE;

		// Token: 0x04001640 RID: 5696
		public BaseColor currentTextColor = BaseColor.BLACK;

		// Token: 0x04001641 RID: 5697
		public int backgroundMode = MetaState.OPAQUE;

		// Token: 0x04001642 RID: 5698
		public int polyFillMode = MetaState.ALTERNATE;

		// Token: 0x04001643 RID: 5699
		public int lineJoin = 1;

		// Token: 0x04001644 RID: 5700
		public int textAlign;

		// Token: 0x04001645 RID: 5701
		public int offsetWx;

		// Token: 0x04001646 RID: 5702
		public int offsetWy;

		// Token: 0x04001647 RID: 5703
		public int extentWx;

		// Token: 0x04001648 RID: 5704
		public int extentWy;

		// Token: 0x04001649 RID: 5705
		public float scalingX;

		// Token: 0x0400164A RID: 5706
		public float scalingY;
	}
}
