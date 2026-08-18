using System;
using System.Drawing;
using System.Threading;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls
{
	// Token: 0x02000104 RID: 260
	public class OColor : IDisposable
	{
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000BC0 RID: 3008 RVA: 0x00074170 File Offset: 0x00073170
		// (remove) Token: 0x06000BC1 RID: 3009 RVA: 0x00074204 File Offset: 0x00073204
		internal event OColor.ᜀ AfterChange
		{
			add
			{
				for (;;)
				{
					OColor.ᜀ ᜀ = this.ᜃ;
					if (true)
					{
					}
					int num = 0;
					for (;;)
					{
						OColor.ᜀ ᜀ2;
						switch (num)
						{
						case 0:
							goto IL_37;
						case 1:
							goto IL_68;
						case 2:
							if (ᜀ == ᜀ2)
							{
								num = 1;
								continue;
							}
							goto IL_37;
						}
						break;
						IL_37:
						ᜀ2 = ᜀ;
						OColor.ᜀ value2 = (OColor.ᜀ)Delegate.Combine(ᜀ2, value);
						ᜀ = Interlocked.CompareExchange<OColor.ᜀ>(ref this.ᜃ, value2, ᜀ2);
						num = 2;
					}
				}
				IL_68:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					break;
				}
			}
			remove
			{
				for (;;)
				{
					OColor.ᜀ ᜀ = this.ᜃ;
					if (true)
					{
					}
					int num = 0;
					for (;;)
					{
						OColor.ᜀ ᜀ2;
						switch (num)
						{
						case 0:
							goto IL_37;
						case 1:
							if (ᜀ == ᜀ2)
							{
								num = 2;
								continue;
							}
							goto IL_37;
						case 2:
							goto IL_68;
						}
						break;
						IL_37:
						ᜀ2 = ᜀ;
						OColor.ᜀ value2 = (OColor.ᜀ)Delegate.Remove(ᜀ2, value);
						ᜀ = Interlocked.CompareExchange<OColor.ᜀ>(ref this.ᜃ, value2, ᜀ2);
						num = 1;
					}
				}
				IL_68:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					break;
				}
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x00074298 File Offset: 0x00073298
		public int Value
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜁ;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x000742DC File Offset: 0x000732DC
		// (set) Token: 0x06000BC4 RID: 3012 RVA: 0x00074320 File Offset: 0x00073320
		public double Tint
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜂ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜂ = value;
			}
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00074364 File Offset: 0x00073364
		public OColor(Color color) : this(ColorType.RGB, color.ToArgb())
		{
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00074380 File Offset: 0x00073380
		public OColor(ExcelColors color) : this(ColorType.Known, (int)color)
		{
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00074398 File Offset: 0x00073398
		public OColor(ColorType colorType, int colorValue) : this(colorType, colorValue, 0.0)
		{
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x000743B8 File Offset: 0x000733B8
		public OColor(ColorType colorType, int colorValue, double tint)
		{
			this.ᜀ = colorType;
			this.ᜁ = colorValue;
			this.ᜂ = tint;
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x000743E0 File Offset: 0x000733E0
		public ColorType ColorType
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ;
			}
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00074424 File Offset: 0x00073424
		internal ExcelColors ᜂ(IWorkbook A_0)
		{
			ExcelColors nearestColor;
			for (;;)
			{
				ColorType colorType = this.ᜀ;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						for (;;)
						{
							if (true)
							{
							}
							if (colorType != ColorType.Known)
							{
								goto IL_59;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_49;
							}
						}
						IL_49:
						if (false)
						{
						}
						num = 1;
						continue;
						IL_59:
						nearestColor = (A_0 as XlsWorkbook).GetNearestColor(this.ᜁ(A_0), 8);
						num = 3;
						continue;
					case 1:
						nearestColor = (ExcelColors)this.ᜁ;
						num = 2;
						continue;
					case 2:
						return nearestColor;
					case 3:
						return nearestColor;
					}
					break;
				}
			}
			return nearestColor;
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x000744C8 File Offset: 0x000734C8
		public ExcelColors GetKnownColor(Workbook book)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ᜂ(book.excelWorkbook);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00074510 File Offset: 0x00073510
		public void SetKnownColor(ExcelColors value)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜀ(value, true);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00074554 File Offset: 0x00073554
		internal void ᜀ(ExcelColors A_0, bool A_1)
		{
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜁ != (int)A_0)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					return;
				case 1:
					goto IL_7F;
				case 2:
					if (A_1)
					{
						goto IL_B1;
					}
					return;
				case 3:
					num = 8;
					continue;
				case 4:
					num = 0;
					continue;
				case 5:
					return;
				case 6:
					this.ᜃ();
					num = 5;
					continue;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B1;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 8:
					if (this.ᜃ != null)
					{
						num = 6;
						continue;
					}
					return;
				}
				if (this.ᜀ == ColorType.Known)
				{
					num = 4;
					continue;
				}
				IL_7F:
				this.ᜀ = ColorType.Known;
				this.ᜁ = (int)A_0;
				this.ᜀ(false);
				this.ᜂ = 0.0;
				num = 2;
				continue;
				IL_B1:
				num = 3;
			}
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0007466C File Offset: 0x0007366C
		internal void ᜀ(ExcelColors A_0, bool A_1, XlsWorkbook A_2)
		{
			int num = 7;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					num = 5;
					continue;
				case 1:
					if (this.ᜃ != null)
					{
						num = 10;
						continue;
					}
					return;
				case 2:
					goto IL_D5;
				case 3:
					goto IL_106;
				case 4:
					return;
				case 5:
					if (this.ᜁ != (int)A_0)
					{
						num = 2;
						continue;
					}
					return;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						if (A_1)
						{
							num = 11;
							continue;
						}
						return;
					}
					break;
				case 8:
					if (!A_2.IsEqualColor)
					{
						num = 9;
						continue;
					}
					goto IL_106;
				case 9:
					this.ᜀ(false, A_2);
					num = 3;
					continue;
				case 10:
					this.ᜃ();
					num = 4;
					continue;
				case 11:
					num = 1;
					continue;
				}
				if (this.ᜀ == ColorType.Known)
				{
					num = 0;
					continue;
				}
				IL_D5:
				this.ᜀ = ColorType.Known;
				this.ᜁ = (int)A_0;
				num = 8;
				continue;
				IL_106:
				this.ᜂ = 0.0;
				num = 6;
			}
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x000747C8 File Offset: 0x000737C8
		internal Color ᜁ(IWorkbook A_0)
		{
			switch (0)
			{
			default:
			{
				Color color;
				for (;;)
				{
					ColorType colorType = this.ᜀ;
					int num = 14;
					for (;;)
					{
						int num5;
						Color[] ᜱ;
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_A3;
							default:
							{
								if (false)
								{
								}
								int num2;
								int num3;
								color = XlsWorkbook.ᜪ[num2][num3];
								num = 22;
								continue;
							}
							}
							break;
						case 1:
							num = 15;
							continue;
						case 2:
							goto IL_29F;
						case 3:
							goto IL_CF;
						case 4:
							goto IL_29F;
						case 5:
						{
							int num3;
							int num4;
							if (num3 < num4)
							{
								num = 0;
								continue;
							}
							goto IL_225;
						}
						case 6:
							goto IL_327;
						case 7:
							goto IL_2FF;
						case 8:
						{
							Color color2;
							if (!color2.Equals(color))
							{
								num = 9;
								continue;
							}
							goto IL_204;
						}
						case 9:
						{
							int num2;
							num2++;
							num5++;
							num = 7;
							continue;
						}
						case 10:
							num = 5;
							continue;
						case 11:
							goto IL_204;
						case 12:
						{
							if (num5 >= ᜱ.Length)
							{
								num = 11;
								continue;
							}
							Color color2 = ᜱ[num5];
							num = 8;
							continue;
						}
						case 13:
						{
							int num6;
							double[] ᜩ;
							if (num6 >= ᜩ.Length)
							{
								num = 6;
								continue;
							}
							double num7 = ᜩ[num6];
							num = 19;
							continue;
						}
						case 14:
							switch (colorType)
							{
							case ColorType.Known:
								color = A_0.GetPaletteColor((ExcelColors)this.ᜁ);
								num = 4;
								continue;
							case ColorType.RGB:
								color = spr\u1D39.ᜀ(this.ᜁ);
								if (true)
								{
								}
								num = 2;
								continue;
							case ColorType.Theme:
								color = (A_0 as XlsWorkbook).ᜂ(this.ᜁ);
								num = 24;
								continue;
							}
							goto IL_A3;
						case 15:
							goto IL_27F;
						case 16:
							if (this.ᜂ != 0.0)
							{
								num = 20;
								continue;
							}
							return color;
						case 17:
							goto IL_CF;
						case 18:
							goto IL_2FF;
						case 19:
						{
							double num7;
							if (num7 != this.ᜂ)
							{
								num = 21;
								continue;
							}
							goto IL_327;
						}
						case 20:
						{
							int num3 = 0;
							int num2 = 0;
							int num8 = XlsWorkbook.ᜪ.Length;
							int num4 = XlsWorkbook.ᜩ.Length;
							double[] ᜩ = XlsWorkbook.ᜩ;
							int num6 = 0;
							num = 17;
							continue;
						}
						case 21:
						{
							int num3;
							num3++;
							int num6;
							num6++;
							num = 3;
							continue;
						}
						case 22:
							goto IL_1DE;
						case 23:
						{
							int num2;
							int num8;
							if (num2 < num8)
							{
								num = 10;
								continue;
							}
							goto IL_225;
						}
						case 24:
							goto IL_29F;
						case 25:
							goto IL_23E;
						}
						break;
						IL_A3:
						num = 1;
						continue;
						IL_CF:
						num = 13;
						continue;
						IL_204:
						num = 23;
						continue;
						IL_225:
						color = spr\u2306.ᜀ(color, this.ᜂ);
						num = 25;
						continue;
						IL_29F:
						num = 16;
						continue;
						IL_2FF:
						num = 12;
						continue;
						IL_327:
						ᜱ = XlsWorkbook.ᜱ;
						num5 = 0;
						num = 18;
					}
				}
				IL_1DE:
				IL_23E:
				return color;
				IL_27F:
				throw new InvalidOperationException();
			}
			}
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x00074B20 File Offset: 0x00073B20
		internal void ᜀ(Color A_0, IWorkbook A_1)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.SetRGB(A_0, A_1, 0.0);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00074B6C File Offset: 0x00073B6C
		internal void ᜀ(Color A_0)
		{
			for (;;)
			{
				IL_2C:
				int num = A_0.ToArgb();
				int num2 = Color.Black.ToArgb();
				int num3 = 7;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						if (this.ᜁ == num)
						{
							num3 = 8;
							continue;
						}
						goto IL_5D;
					case 1:
						this.ᜃ();
						num3 = 6;
						continue;
					case 2:
						num3 = 0;
						continue;
					case 3:
						if (this.ᜁ == num2)
						{
							num3 = 4;
							continue;
						}
						return;
					case 4:
						goto IL_5D;
					case 5:
						if (this.ᜃ != null)
						{
							num3 = 1;
							continue;
						}
						return;
					case 6:
						goto IL_107;
					case 7:
						if (this.ᜀ == ColorType.RGB)
						{
							num3 = 2;
							continue;
						}
						goto IL_5D;
					case 8:
						num3 = 3;
						continue;
					}
					goto IL_2C;
					IL_5D:
					this.ᜀ = ColorType.RGB;
					this.ᜁ = num;
					this.ᜂ = 0.0;
					if (true)
					{
					}
					num3 = 5;
				}
				IL_107:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_11D;
				}
			}
			IL_11D:
			if (false)
			{
			}
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00074CA0 File Offset: 0x00073CA0
		public static implicit operator OColor(Color color)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return new OColor(color);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00074CE4 File Offset: 0x00073CE4
		public static bool operator ==(OColor first, OColor second)
		{
			for (;;)
			{
				if (true)
				{
				}
				int num = 12;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (second == null)
						{
							num = 7;
							continue;
						}
						goto IL_6F;
					case 1:
						if (first.ᜀ == second.ᜀ)
						{
							num = 14;
							continue;
						}
						return false;
					case 2:
						goto IL_16E;
					case 3:
						if (second == null)
						{
							num = 8;
							continue;
						}
						goto IL_148;
					case 4:
						num = 3;
						continue;
					case 5:
						if (second == null)
						{
							num = 6;
							continue;
						}
						return false;
					case 6:
						goto IL_163;
					case 7:
						goto IL_CA;
					case 8:
						return true;
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_16E;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 10:
						if (first == null)
						{
							num = 11;
							continue;
						}
						goto IL_163;
					case 11:
						num = 5;
						continue;
					case 12:
						if (first == null)
						{
							num = 4;
							continue;
						}
						goto IL_148;
					case 13:
						if (first.ᜁ == second.ᜁ)
						{
							num = 15;
							continue;
						}
						return false;
					case 14:
						num = 13;
						continue;
					case 15:
						goto IL_F5;
					}
					break;
					IL_6F:
					num = 1;
					continue;
					IL_16E:
					if (first != null)
					{
						num = 9;
						continue;
					}
					goto IL_6F;
					IL_148:
					num = 10;
					continue;
					IL_163:
					num = 2;
				}
			}
			IL_CA:
			return false;
			IL_F5:
			return first.ᜂ == second.ᜂ;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00074E78 File Offset: 0x00073E78
		public static bool operator !=(OColor first, OColor second)
		{
			for (;;)
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (first == null)
						{
							num = 2;
							continue;
						}
						goto IL_143;
					case 1:
						if (first.ᜁ == second.ᜁ)
						{
							num = 15;
							continue;
						}
						return true;
					case 2:
						num = 11;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_171;
						default:
							if (false)
							{
							}
							num = 9;
							continue;
						}
						break;
					case 4:
						num = 6;
						continue;
					case 5:
						goto IL_166;
					case 6:
						if (second == null)
						{
							num = 5;
							continue;
						}
						return true;
					case 7:
						goto IL_171;
					case 8:
						if (true)
						{
						}
						if (first == null)
						{
							num = 4;
							continue;
						}
						goto IL_166;
					case 9:
						if (second == null)
						{
							num = 14;
							continue;
						}
						goto IL_67;
					case 10:
						return false;
					case 11:
						if (second == null)
						{
							num = 10;
							continue;
						}
						goto IL_143;
					case 12:
						if (first.ᜀ == second.ᜀ)
						{
							num = 13;
							continue;
						}
						return true;
					case 13:
						num = 1;
						continue;
					case 14:
						goto IL_C2;
					case 15:
						goto IL_ED;
					}
					break;
					IL_67:
					num = 12;
					continue;
					IL_171:
					if (first != null)
					{
						num = 3;
						continue;
					}
					goto IL_67;
					IL_143:
					num = 8;
					continue;
					IL_166:
					num = 7;
				}
			}
			IL_C2:
			return true;
			IL_ED:
			return first.ᜂ != second.ᜂ;
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00075010 File Offset: 0x00074010
		internal void ᜀ(OColor A_0, bool A_1)
		{
			int a_ = 6;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_46;
				case 1:
					if (true)
					{
					}
					if (this.ᜃ != null)
					{
						num = 4;
						continue;
					}
					return;
				case 2:
					return;
				case 3:
					num = 1;
					continue;
				case 4:
					this.ᜃ();
					num = 2;
					continue;
				case 6:
					if (A_1)
					{
						goto IL_BE;
					}
					return;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					this.ᜁ = A_0.ᜁ;
					this.ᜀ = A_0.ᜀ;
					this.ᜂ = A_0.ᜂ;
					num = 6;
					continue;
				}
				IL_BE:
				num = 3;
			}
			IL_46:
			throw new ArgumentNullException(RecordTableEnumerator.b("猻紽⼿⹁⭃㑅", a_));
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00075120 File Offset: 0x00074120
		internal void ᜀ(IWorkbook A_0)
		{
			int num = 0;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6B;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 1:
						this.SetKnownColor(this.ᜂ(A_0));
						num = 2;
						continue;
					case 2:
						goto IL_69;
					}
					if (this.ᜀ == ColorType.Known)
					{
						goto IL_6B;
					}
					num = 1;
					break;
				}
			}
			IL_69:
			IL_6B:
			if (true)
			{
			}
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x000751A0 File Offset: 0x000741A0
		public override int GetHashCode()
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return this.ᜁ.GetHashCode() ^ this.ᜀ.GetHashCode();
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x000751F8 File Offset: 0x000741F8
		internal void ᜀ(ExcelColors A_0)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜀ = ColorType.Known;
			this.ᜁ = (int)A_0;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00075244 File Offset: 0x00074244
		internal OColor ᜀ()
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return (OColor)base.MemberwiseClone();
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0007528C File Offset: 0x0007428C
		internal void ᜁ()
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜀ(true);
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x000752D0 File Offset: 0x000742D0
		internal void ᜀ(bool A_0)
		{
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜁ += 64;
					num = 4;
					continue;
				case 1:
					if (this.ᜁ == 0)
					{
						num = 0;
						continue;
					}
					num = 9;
					continue;
				case 2:
					goto IL_51;
				case 3:
					this.ᜀ((ExcelColors)this.ᜁ, A_0);
					num = 8;
					continue;
				case 4:
					goto IL_51;
				case 5:
				{
					int num2;
					if (num2 != this.ᜁ)
					{
						num = 3;
						continue;
					}
					return;
				}
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_77;
					default:
						if (false)
						{
						}
						this.ᜁ += 8;
						num = 2;
						continue;
					}
					break;
				case 8:
					goto IL_E5;
				case 9:
					goto IL_77;
				case 10:
				{
					int num2 = this.ᜁ;
					num = 1;
					continue;
				}
				}
				if (this.ᜀ == ColorType.Known)
				{
					num = 10;
					continue;
				}
				return;
				IL_51:
				num = 5;
				continue;
				IL_77:
				if (this.ᜁ >= 8)
				{
					goto IL_51;
				}
				num = 6;
			}
			IL_E5:
			if (true)
			{
			}
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00075414 File Offset: 0x00074414
		internal void ᜀ(bool A_0, XlsWorkbook A_1)
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2 = this.ᜁ;
					num = 6;
					continue;
				}
				case 1:
					this.ᜁ += 64;
					num = 3;
					continue;
				case 2:
					goto IL_F8;
				case 3:
					goto IL_F8;
				case 4:
					return;
				case 6:
					if (this.ᜁ == 0)
					{
						num = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 10;
						continue;
					}
					break;
				case 7:
				{
					int num2;
					if (num2 != this.ᜁ)
					{
						num = 8;
						continue;
					}
					return;
				}
				case 8:
					this.ᜀ((ExcelColors)this.ᜁ, A_0);
					num = 4;
					continue;
				case 9:
					if (!A_1.IsLoaded)
					{
						num = 12;
						continue;
					}
					goto IL_F8;
				case 10:
					if (this.ᜁ < 8)
					{
						num = 11;
						continue;
					}
					goto IL_F8;
				case 11:
					if (true)
					{
					}
					num = 9;
					continue;
				case 12:
					this.ᜁ += 8;
					num = 2;
					continue;
				}
				if (this.ᜀ == ColorType.Known)
				{
					num = 0;
					continue;
				}
				break;
				IL_F8:
				num = 7;
			}
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00075584 File Offset: 0x00074584
		public override bool Equals(object obj)
		{
			OColor first;
			for (;;)
			{
				first = (obj as OColor);
				if (obj != null)
				{
					goto IL_2A;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_20;
				}
			}
			IL_20:
			if (false)
			{
			}
			return false;
			IL_2A:
			if (true)
			{
			}
			return first == this;
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x000755D4 File Offset: 0x000745D4
		public void SetTheme(int themeIndex, IWorkbook book)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.SetTheme(themeIndex, book, 0.0);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00075620 File Offset: 0x00074620
		public void SetTheme(int themeIndex, IWorkbook book, double dTintValue)
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜁ != themeIndex)
					{
						num = 4;
						continue;
					}
					return;
				case 1:
					if (this.ᜃ != null)
					{
						num = 6;
						continue;
					}
					return;
				case 2:
					goto IL_A0;
				case 3:
					num = 0;
					continue;
				case 4:
					goto IL_A2;
				case 6:
					this.ᜃ();
					num = 2;
					continue;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						goto IL_100;
					}
					break;
				case 8:
					if (book.Version == ExcelVersion.Version97to2003)
					{
						num = 7;
						continue;
					}
					num = 1;
					continue;
				}
				if (this.ᜀ == ColorType.Theme)
				{
					num = 3;
					continue;
				}
				IL_A2:
				this.ᜀ = ColorType.Theme;
				this.ᜁ = themeIndex;
				this.ᜂ = dTintValue;
				if (true)
				{
				}
				num = 8;
			}
			IL_A0:
			return;
			IL_100:
			if (false)
			{
			}
			this.ᜀ(book);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00075738 File Offset: 0x00074738
		public void SetRGB(Color rgb, IWorkbook book, double dTintValue)
		{
			for (;;)
			{
				int num = rgb.ToArgb();
				int num2 = Color.Black.ToArgb();
				int num3 = 5;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						if (book.Version == ExcelVersion.Version97to2003)
						{
							num3 = 9;
							continue;
						}
						goto IL_102;
					case 1:
						if (!(book as XlsWorkbook).Loading)
						{
							num3 = 11;
							continue;
						}
						goto IL_102;
					case 2:
						if (this.ᜁ == num2)
						{
							num3 = 8;
							continue;
						}
						return;
					case 3:
						this.ᜃ();
						num3 = 6;
						continue;
					case 4:
						num3 = 12;
						continue;
					case 5:
						if (this.ᜀ == ColorType.RGB)
						{
							num3 = 4;
							continue;
						}
						goto IL_122;
					case 6:
						return;
					case 7:
						num3 = 2;
						continue;
					case 8:
						goto IL_122;
					case 9:
						goto IL_A0;
					case 10:
						if (this.ᜃ != null)
						{
							num3 = 3;
							continue;
						}
						return;
					case 11:
						num3 = 0;
						continue;
					case 12:
						if (this.ᜁ == num)
						{
							num3 = 7;
							continue;
						}
						goto IL_122;
					}
					break;
					IL_102:
					num3 = 10;
					continue;
					IL_122:
					this.ᜀ = ColorType.RGB;
					this.ᜁ = num;
					this.ᜂ = 0.0;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num3 = 1;
						break;
					}
				}
			}
			IL_A0:
			this.ᜀ(book);
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x000758D0 File Offset: 0x000748D0
		public void Dispose()
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜃ = null;
			GC.SuppressFinalize(this);
		}

		// Token: 0x040009F9 RID: 2553
		private ColorType ᜀ;

		// Token: 0x040009FA RID: 2554
		private int ᜁ;

		// Token: 0x040009FB RID: 2555
		private double ᜂ;

		// Token: 0x040009FC RID: 2556
		private OColor.ᜀ ᜃ;

		// Token: 0x02000105 RID: 261
		// (Invoke) Token: 0x06000BE3 RID: 3043
		internal delegate void ᜀ();
	}
}
