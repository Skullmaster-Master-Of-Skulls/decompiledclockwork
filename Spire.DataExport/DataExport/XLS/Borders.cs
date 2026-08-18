using System;
using System.ComponentModel;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001AF RID: 431
	public class Borders : ICloneable
	{
		// Token: 0x06000C13 RID: 3091 RVA: 0x0007F5E0 File Offset: 0x0007E5E0
		public object Clone()
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
			return new Borders
			{
				Bottom = this.Bottom,
				DiagDown = this.DiagDown,
				DiagUp = this.DiagUp,
				Left = this.Left,
				Right = this.Right,
				Top = this.Top
			};
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0007F66C File Offset: 0x0007E66C
		public bool IsEqual(Borders Borders)
		{
			int num = 11;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_130;
					default:
						if (false)
						{
						}
						num = 10;
						continue;
					}
					break;
				case 1:
					goto IL_13B;
				case 2:
					return false;
				case 3:
					if (this.ᜁ.IsEqual(Borders.Right))
					{
						num = 0;
						continue;
					}
					return false;
				case 4:
					if (this.ᜄ.IsEqual(Borders.DiagDown))
					{
						goto IL_130;
					}
					return false;
				case 5:
					num = 3;
					continue;
				case 6:
					if (this.ᜀ.IsEqual(Borders.Left))
					{
						num = 5;
						continue;
					}
					return false;
				case 7:
					if (this.ᜃ.IsEqual(Borders.Bottom))
					{
						num = 8;
						continue;
					}
					return false;
				case 8:
					if (true)
					{
					}
					num = 4;
					continue;
				case 9:
					num = 7;
					continue;
				case 10:
					if (this.ᜂ.IsEqual(Borders.Top))
					{
						num = 9;
						continue;
					}
					return false;
				}
				if (Borders == null)
				{
					num = 2;
					continue;
				}
				num = 6;
				continue;
				IL_130:
				num = 1;
			}
			return false;
			IL_13B:
			return this.ᜅ.IsEqual(Borders.DiagUp);
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0007F7F0 File Offset: 0x0007E7F0
		public void SetDefault()
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
			this.ᜀ.SetDefault();
			this.ᜁ.SetDefault();
			this.ᜂ.SetDefault();
			this.ᜃ.SetDefault();
			this.ᜄ.SetDefault();
			this.ᜅ.SetDefault();
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0007F870 File Offset: 0x0007E870
		// (set) Token: 0x06000C17 RID: 3095 RVA: 0x0007F8B4 File Offset: 0x0007E8B4
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CellBorder Left
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
				return this.ᜀ;
			}
			set
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜀ = value;
						num = 3;
						continue;
					case 1:
						goto IL_6E;
					case 2:
						num = 1;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6E;
						default:
							goto IL_56;
						}
						break;
					}
					if (value != null)
					{
						num = 2;
						continue;
					}
					return;
					IL_6E:
					if (value == this.ᜀ)
					{
						return;
					}
					num = 0;
				}
				IL_56:
				if (false)
				{
				}
				if (true)
				{
				}
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x0007F94C File Offset: 0x0007E94C
		// (set) Token: 0x06000C19 RID: 3097 RVA: 0x0007F990 File Offset: 0x0007E990
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public CellBorder Right
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
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6E;
					case 1:
						this.ᜁ = value;
						num = 3;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6E;
						default:
							goto IL_5E;
						}
						break;
					case 4:
						num = 0;
						continue;
					}
					if (value != null)
					{
						if (true)
						{
						}
						num = 4;
						continue;
					}
					return;
					IL_6E:
					if (value == this.ᜁ)
					{
						return;
					}
					num = 1;
				}
				IL_5E:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x0007FA28 File Offset: 0x0007EA28
		// (set) Token: 0x06000C1B RID: 3099 RVA: 0x0007FA6C File Offset: 0x0007EA6C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public CellBorder Top
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
				return this.ᜂ;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						this.ᜂ = value;
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
						break;
					case 3:
						num = 4;
						continue;
					case 4:
						if (value != this.ᜂ)
						{
							num = 1;
							continue;
						}
						return;
					}
					if (true)
					{
					}
					if (value == null)
					{
						break;
					}
					num = 3;
				}
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0007FB04 File Offset: 0x0007EB04
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x0007FB48 File Offset: 0x0007EB48
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public CellBorder Bottom
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
				return this.ᜃ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						break;
					case 1:
						num = 2;
						continue;
					case 2:
						if (value != this.ᜃ)
						{
							num = 3;
							continue;
						}
						return;
					case 3:
						this.ᜃ = value;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 4:
						return;
					}
					if (value == null)
					{
						break;
					}
					num = 1;
				}
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x0007FBE0 File Offset: 0x0007EBE0
		// (set) Token: 0x06000C1F RID: 3103 RVA: 0x0007FC24 File Offset: 0x0007EC24
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CellBorder DiagDown
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
				return this.ᜄ;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜄ = value;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 1:
						num = 4;
						continue;
					case 3:
						return;
					case 4:
						if (value != this.ᜄ)
						{
							num = 0;
							continue;
						}
						return;
					}
					if (value == null)
					{
						break;
					}
					if (true)
					{
					}
					num = 1;
				}
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x0007FCBC File Offset: 0x0007ECBC
		// (set) Token: 0x06000C21 RID: 3105 RVA: 0x0007FD00 File Offset: 0x0007ED00
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public CellBorder DiagUp
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
				return this.ᜅ;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value != this.ᜅ)
						{
							num = 4;
							continue;
						}
						return;
					case 1:
						num = 0;
						continue;
					case 3:
						return;
					case 4:
						this.ᜅ = value;
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
							num = 3;
							continue;
						}
						break;
					}
					if (value == null)
					{
						break;
					}
					num = 1;
				}
			}
		}

		// Token: 0x04000932 RID: 2354
		private CellBorder ᜀ = new CellBorder();

		// Token: 0x04000933 RID: 2355
		private CellBorder ᜁ = new CellBorder();

		// Token: 0x04000934 RID: 2356
		private float \u2460\u009E\u00AB\u009C;

		// Token: 0x04000935 RID: 2357
		private CellBorder ᜂ = new CellBorder();

		// Token: 0x04000936 RID: 2358
		private long \u2593\u008B\u0084\u0095;

		// Token: 0x04000937 RID: 2359
		private CellBorder ᜃ = new CellBorder();

		// Token: 0x04000938 RID: 2360
		private CellBorder ᜄ = new CellBorder();

		// Token: 0x04000939 RID: 2361
		private CellBorder ᜅ = new CellBorder();
	}
}
