using System;
using Spire.Xls.Core;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Parser.Biff_Records.Charts;

namespace Spire.Xls
{
	// Token: 0x02000054 RID: 84
	public class Format3D : XlsObject, IFormat3D, ICloneParent
	{
		// Token: 0x06000827 RID: 2087 RVA: 0x00055C7C File Offset: 0x00054C7C
		internal Format3D(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜀ();
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00055CA4 File Offset: 0x00054CA4
		private void ᜀ()
		{
			int a_ = 19;
			this.ᜁ = (XlsWorkbook)base.FindParent(typeof(XlsWorkbook));
			if (this.ᜁ == null)
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
					throw new ApplicationException(RecordTableEnumerator.b("㥈⩊㽌⩎㽐❒畔㡖㭘ㅚ㡜㱞ᕠၢ䕤Ѧࡨժ䵬ŮṰݲ啴ᕶᱸ孺᭼ၾꦆ", a_));
				}
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x00055D24 File Offset: 0x00054D24
		// (set) Token: 0x0600082A RID: 2090 RVA: 0x00055D6C File Offset: 0x00054D6C
		public XLSXChartBevelType BevelTopType
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
				return this.ᜀ.BevelTopType;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_59;
						}
						break;
					case 2:
						this.ᜀ.BevelTopType = value;
						num = 1;
						continue;
					}
					IL_1C:
					if (value != this.BevelTopType)
					{
						num = 2;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_59:
				if (true)
				{
				}
				if (false)
				{
				}
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x00055DEC File Offset: 0x00054DEC
		// (set) Token: 0x0600082C RID: 2092 RVA: 0x00055E34 File Offset: 0x00054E34
		public XLSXChartBevelType BevelBottomType
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
				return this.ᜀ.BevelBottomType;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.ᜀ.BevelBottomType = value;
						num = 2;
						continue;
					case 2:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_61;
						}
						break;
					}
					IL_1C:
					if (value != this.BevelBottomType)
					{
						num = 1;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_61:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x00055EB4 File Offset: 0x00054EB4
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x00055EFC File Offset: 0x00054EFC
		public XLSXChartMaterialType MaterialType
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
				return this.ᜀ.MaterialType;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_61;
						}
						break;
					case 2:
						this.ᜀ.MaterialType = value;
						num = 0;
						continue;
					}
					IL_1C:
					if (true)
					{
					}
					if (value != this.MaterialType)
					{
						num = 2;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_61:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x00055F7C File Offset: 0x00054F7C
		// (set) Token: 0x06000830 RID: 2096 RVA: 0x00055FC4 File Offset: 0x00054FC4
		public XLSXChartLightingType LightingType
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
				return this.ᜀ.LightingType;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜀ.LightingType = value;
						num = 2;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_61;
						}
						break;
					}
					IL_1C:
					if (true)
					{
					}
					if (value != this.LightingType)
					{
						num = 0;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_61:
				if (false)
				{
				}
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00056044 File Offset: 0x00055044
		object ICloneParent.Clone(object parent)
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
			return this.Clone(parent);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00056088 File Offset: 0x00055088
		public Format3D Clone(object parent)
		{
			int a_ = 15;
			if (true)
			{
			}
			if (parent == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					throw new ArgumentNullException(RecordTableEnumerator.b("㕄♆㭈⹊⍌㭎", a_));
				}
			}
			Format3D format3D = (Format3D)base.MemberwiseClone();
			format3D.ᜀ = (ShadowOptions)spr\u1CD3.ᜀ(this.ᜀ);
			format3D.SetParent(parent);
			format3D.ᜀ();
			return format3D;
		}

		// Token: 0x0400016F RID: 367
		private float \u2593\u0080\u0088\u00A9;

		// Token: 0x04000170 RID: 368
		private bool[] \u2593\u0096\u0081\u0098;

		// Token: 0x04000171 RID: 369
		private bool \u2593\u009E\u00A5\u008B;

		// Token: 0x04000172 RID: 370
		private ShadowOptions ᜀ = new ShadowOptions();

		// Token: 0x04000173 RID: 371
		private string[] \u25D9\u0096\u0085\u0083;

		// Token: 0x04000174 RID: 372
		private XlsWorkbook ᜁ;
	}
}
