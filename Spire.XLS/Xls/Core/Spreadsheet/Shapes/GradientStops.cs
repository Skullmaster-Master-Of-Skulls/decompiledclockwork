using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Shapes
{
	// Token: 0x0200021E RID: 542
	public class GradientStops : List<XlsGradientStop>
	{
		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x0600208A RID: 8330 RVA: 0x00124D4C File Offset: 0x00123D4C
		// (set) Token: 0x0600208B RID: 8331 RVA: 0x00124D90 File Offset: 0x00123D90
		public int Angle
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
				return this.ᜁ;
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
				this.ᜁ = value;
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x0600208C RID: 8332 RVA: 0x00124DD4 File Offset: 0x00123DD4
		// (set) Token: 0x0600208D RID: 8333 RVA: 0x00124E18 File Offset: 0x00123E18
		public GradientType GradientType
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜂ = value;
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x0600208E RID: 8334 RVA: 0x00124E5C File Offset: 0x00123E5C
		// (set) Token: 0x0600208F RID: 8335 RVA: 0x00124EA0 File Offset: 0x00123EA0
		public Rectangle FillToRect
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
				return this.ᜃ;
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
				this.ᜃ = value;
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06002090 RID: 8336 RVA: 0x00124EE4 File Offset: 0x00123EE4
		public bool IsDoubled
		{
			get
			{
				switch (0)
				{
				default:
				{
					bool result;
					for (;;)
					{
						int count = base.Count;
						result = true;
						int num = 5;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								int num2;
								int num3;
								if (num2 > num3)
								{
									num = 3;
									continue;
								}
								XlsGradientStop xlsGradientStop = base[num2];
								XlsGradientStop xlsGradientStop2 = base[num3];
								num = 4;
								continue;
							}
							case 1:
							{
								XlsGradientStop xlsGradientStop;
								XlsGradientStop xlsGradientStop2;
								if (xlsGradientStop.Position != 100000 - xlsGradientStop2.Position)
								{
									num = 7;
									continue;
								}
								int num2;
								num2++;
								int num3;
								num3--;
								num = 8;
								continue;
							}
							case 2:
								result = false;
								num = 9;
								continue;
							case 3:
								return result;
							case 4:
							{
								XlsGradientStop xlsGradientStop;
								XlsGradientStop xlsGradientStop2;
								if (!(xlsGradientStop.OColor != xlsGradientStop2.OColor))
								{
									num = 6;
									continue;
								}
								goto IL_68;
							}
							case 5:
							{
								if (count <= 2)
								{
									num = 2;
									continue;
								}
								int num2 = 0;
								int num3 = count - 1;
								num = 11;
								continue;
							}
							case 6:
								num = 1;
								continue;
							case 7:
								goto IL_68;
							case 8:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_BF;
								default:
									if (false)
									{
									}
									goto IL_C1;
								}
								break;
							case 9:
								return result;
							case 10:
								goto IL_73;
							case 11:
								goto IL_BF;
							}
							break;
							IL_68:
							result = false;
							num = 10;
							continue;
							IL_C1:
							num = 0;
							continue;
							IL_BF:
							goto IL_C1;
						}
					}
					IL_73:
					if (true)
					{
					}
					return result;
				}
				}
			}
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x0012506C File Offset: 0x0012406C
		public GradientStops()
		{
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x00125080 File Offset: 0x00124080
		public GradientStops(byte[] data)
		{
			this.ᜀ(data);
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x0012509C File Offset: 0x0012409C
		public void Serialize(Stream stream)
		{
			int a_ = 0;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_162;
				case 2:
					return;
				case 3:
					goto IL_3C;
				case 4:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 2;
						continue;
					}
					base[num2].ᜀ(stream);
					num2++;
					num = 5;
					continue;
				}
				case 5:
					goto IL_162;
				}
				if (stream == null)
				{
					num = 3;
					continue;
				}
				for (;;)
				{
					byte[] bytes = BitConverter.GetBytes(base.Count);
					stream.Write(bytes, 0, bytes.Length);
					bytes = BitConverter.GetBytes(this.ᜁ);
					stream.Write(bytes, 0, bytes.Length);
					stream.WriteByte((byte)this.ᜂ);
					bytes = BitConverter.GetBytes(this.ᜃ.Left);
					stream.Write(bytes, 0, bytes.Length);
					bytes = BitConverter.GetBytes(this.ᜃ.Top);
					stream.Write(bytes, 0, bytes.Length);
					bytes = BitConverter.GetBytes(this.ᜃ.Right);
					stream.Write(bytes, 0, bytes.Length);
					bytes = BitConverter.GetBytes(this.ᜃ.Bottom);
					stream.Write(bytes, 0, bytes.Length);
					int num2 = 0;
					int count = base.Count;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_133;
					}
				}
				IL_133:
				if (true)
				{
				}
				if (false)
				{
				}
				num = 1;
				continue;
				IL_162:
				num = 4;
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔵䰷䠹夻弽ⴿ", a_));
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x0012522C File Offset: 0x0012422C
		private void ᜀ(byte[] A_0)
		{
			int a_ = 12;
			switch (0)
			{
			default:
			{
				int num = 4;
				for (;;)
				{
					int num2;
					int num3;
					int num4;
					switch (num)
					{
					case 0:
						goto IL_71;
					case 1:
					{
						if (num2 >= num3)
						{
							num = 2;
							continue;
						}
						XlsGradientStop item = new XlsGradientStop(A_0, num4);
						num4 += 12;
						base.Add(item);
						num2++;
						num = 5;
						continue;
					}
					case 2:
						return;
					case 3:
						goto IL_13E;
					case 5:
						goto IL_13E;
					}
					if (A_0 == null)
					{
						if (true)
						{
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
							num = 0;
							continue;
						}
					}
					num4 = 0;
					num3 = BitConverter.ToInt32(A_0, num4);
					num4 += 4;
					this.ᜁ = BitConverter.ToInt32(A_0, num4);
					num4 += 4;
					this.ᜂ = (GradientType)A_0[num4];
					num4++;
					int left = BitConverter.ToInt32(A_0, num4);
					num4 += 4;
					int top = BitConverter.ToInt32(A_0, num4);
					num4 += 4;
					int right = BitConverter.ToInt32(A_0, num4);
					num4 += 4;
					int bottom = BitConverter.ToInt32(A_0, num4);
					num4 += 4;
					this.ᜃ = Rectangle.FromLTRB(left, top, right, bottom);
					num2 = 0;
					num = 3;
					continue;
					IL_13E:
					num = 1;
				}
				IL_71:
				throw new ArgumentNullException(RecordTableEnumerator.b("♁╃㉅⥇", a_));
			}
			}
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x0012539C File Offset: 0x0012439C
		public void DoubleGradientStops()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int count = base.Count;
					int num = 0;
					for (;;)
					{
						int num4;
						switch (num)
						{
						case 0:
						{
							if (count == 0)
							{
								num = 7;
								continue;
							}
							XlsGradientStop xlsGradientStop = base[count - 1];
							int num2 = xlsGradientStop.Position;
							int num3 = num2 >> 1;
							xlsGradientStop.Position = num3;
							goto IL_120;
						}
						case 1:
							goto IL_169;
						case 2:
							goto IL_E2;
						case 3:
						{
							if (num4 < 0)
							{
								num = 6;
								continue;
							}
							XlsGradientStop xlsGradientStop2 = base[num4];
							int num2 = xlsGradientStop2.Position >> 1;
							xlsGradientStop2.Position = num2;
							xlsGradientStop2 = xlsGradientStop2.ᜀ();
							xlsGradientStop2.Position = 100000 - num2;
							base.Add(xlsGradientStop2);
							num4--;
							num = 4;
							continue;
						}
						case 4:
							goto IL_E2;
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_120;
							default:
							{
								if (false)
								{
								}
								if (true)
								{
								}
								int num2;
								if (num2 != 100000)
								{
									num = 8;
									continue;
								}
								goto IL_169;
							}
							}
							break;
						case 6:
							return;
						case 7:
							return;
						case 8:
						{
							XlsGradientStop xlsGradientStop = xlsGradientStop.ᜀ();
							int num3;
							xlsGradientStop.Position = 100000 - num3;
							base.Add(xlsGradientStop);
							num = 1;
							continue;
						}
						}
						break;
						IL_E2:
						num = 3;
						continue;
						IL_120:
						num = 5;
						continue;
						IL_169:
						num4 = count - 2;
						num = 2;
					}
				}
				return;
			}
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x00125528 File Offset: 0x00124528
		public void InvertGradientStops()
		{
			switch (0)
			{
			default:
				if (true)
				{
				}
				for (;;)
				{
					int count = base.Count;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return;
						case 1:
							goto IL_BD;
						case 2:
							if (count == 0)
							{
								num = 0;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return;
							default:
							{
								if (false)
								{
								}
								base.Reverse();
								int num2 = 0;
								num = 1;
								continue;
							}
							}
							break;
						case 3:
							goto IL_BD;
						case 4:
							return;
						case 5:
						{
							int num2;
							if (num2 >= count)
							{
								num = 4;
								continue;
							}
							XlsGradientStop xlsGradientStop = base[num2];
							int position = xlsGradientStop.Position;
							xlsGradientStop.Position = 100000 - position;
							num2++;
							num = 3;
							continue;
						}
						}
						break;
						IL_BD:
						num = 5;
					}
				}
				return;
			}
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x00125610 File Offset: 0x00124610
		public GradientStops ShrinkGradientStops()
		{
			switch (0)
			{
			default:
			{
				GradientStops gradientStops;
				for (;;)
				{
					gradientStops = new GradientStops();
					gradientStops.ᜁ = this.ᜁ;
					gradientStops.ᜂ = this.ᜂ;
					gradientStops.ᜃ = this.ᜃ;
					int num = 0;
					int count = base.Count;
					int num2 = 2;
					for (;;)
					{
						XlsGradientStop xlsGradientStop;
						switch (num2)
						{
						case 0:
							return gradientStops;
						case 1:
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							xlsGradientStop = base[num];
							num2 = 4;
							continue;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_AF;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								goto IL_91;
							}
							break;
						case 3:
							goto IL_AF;
						case 4:
							if (xlsGradientStop.Position <= 50000)
							{
								num2 = 3;
								continue;
							}
							return gradientStops;
						case 5:
							goto IL_91;
						}
						break;
						IL_91:
						num2 = 1;
						continue;
						IL_AF:
						xlsGradientStop = xlsGradientStop.ᜀ();
						xlsGradientStop.Position <<= 1;
						gradientStops.Add(xlsGradientStop);
						num++;
						num2 = 5;
					}
				}
				return gradientStops;
			}
			}
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x00125734 File Offset: 0x00124734
		public GradientStops Clone()
		{
			GradientStops gradientStops;
			for (;;)
			{
				gradientStops = new GradientStops();
				gradientStops.ᜁ = this.ᜁ;
				gradientStops.ᜂ = this.ᜂ;
				gradientStops.ᜃ = this.ᜃ;
				int num = 0;
				int count = base.Count;
				if (true)
				{
				}
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_65;
					case 1:
						goto IL_5D;
					case 2:
						goto IL_5D;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_65;
						default:
							goto IL_B6;
						}
						break;
					}
					break;
					IL_5D:
					num2 = 0;
					continue;
					IL_65:
					if (num >= count)
					{
						num2 = 3;
					}
					else
					{
						gradientStops.Add(base[num].ᜀ());
						num++;
						num2 = 2;
					}
				}
			}
			IL_B6:
			if (false)
			{
			}
			return gradientStops;
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x00125800 File Offset: 0x00124800
		internal bool ᜀ(GradientStops A_0)
		{
			switch (0)
			{
			default:
			{
				int num = 7;
				for (;;)
				{
					bool result;
					int count;
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F1;
						default:
							if (false)
							{
							}
							result = false;
							num = 8;
							continue;
						}
						break;
					case 1:
						if (A_0.Count == count)
						{
							goto IL_F1;
						}
						return result;
					case 2:
					{
						int num2;
						if (num2 >= count)
						{
							num = 5;
							continue;
						}
						if (true)
						{
						}
						XlsGradientStop xlsGradientStop = base[num2];
						XlsGradientStop a_ = A_0[num2];
						num = 9;
						continue;
					}
					case 3:
					{
						result = true;
						int num2 = 0;
						num = 4;
						continue;
					}
					case 4:
						goto IL_FF;
					case 5:
						return result;
					case 6:
						goto IL_FF;
					case 8:
						return result;
					case 9:
					{
						XlsGradientStop xlsGradientStop;
						XlsGradientStop a_;
						if (!xlsGradientStop.ᜀ(a_))
						{
							num = 0;
							continue;
						}
						int num2;
						num2++;
						num = 6;
						continue;
					}
					case 10:
						return false;
					}
					if (A_0 == null)
					{
						num = 10;
						continue;
					}
					result = false;
					count = base.Count;
					num = 1;
					continue;
					IL_F1:
					num = 3;
					continue;
					IL_FF:
					num = 2;
				}
				return false;
			}
			}
		}

		// Token: 0x04001137 RID: 4407
		private bool \u2460\u0090\u00AD\u008F;

		// Token: 0x04001138 RID: 4408
		private long[] \u25D8\u00A2\u0080\u009B;

		// Token: 0x04001139 RID: 4409
		internal const int ᜀ = 100000;

		// Token: 0x0400113A RID: 4410
		private int ᜁ;

		// Token: 0x0400113B RID: 4411
		private int[] \u25D8\u00AD\u009A\u007F;

		// Token: 0x0400113C RID: 4412
		private int[] \u2460\u0096\u0085\u009C;

		// Token: 0x0400113D RID: 4413
		private GradientType ᜂ;

		// Token: 0x0400113E RID: 4414
		private long[] \u25D8\u00A4\u008E\u0095;

		// Token: 0x0400113F RID: 4415
		private Rectangle ᜃ;
	}
}
