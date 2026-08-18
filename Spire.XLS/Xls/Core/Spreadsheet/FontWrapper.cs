using System;
using System.Collections;
using System.Drawing;
using System.Threading;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000058 RID: 88
	public class FontWrapper : CommonWrapper, IInternalFont
	{
		// Token: 0x0600084A RID: 2122 RVA: 0x00056B04 File Offset: 0x00055B04
		public FontWrapper()
		{
			this.ᜂ = true;
			base..ctor();
			this.ᜄ = new OColor(spr\u1D39.ᜀ);
			this.ᜄ.AfterChange += this.ColorObjectUpdate;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00056B48 File Offset: 0x00055B48
		public FontWrapper(XlsFont font)
		{
			int a_ = 18;
			this..ctor();
			if (font == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("⹇╉≋㩍", a_));
			}
			this.ᜀ = font;
			this.ᜄ.ᜀ(font.OColor, false);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00056B98 File Offset: 0x00055B98
		public FontWrapper(XlsFont font, bool bReadOnly, bool bRaiseEvents) : this(font)
		{
			this.ᜁ = bReadOnly;
			this.ᜂ = bRaiseEvents;
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x00056BBC File Offset: 0x00055BBC
		// (set) Token: 0x0600084E RID: 2126 RVA: 0x00056C04 File Offset: 0x00055C04
		public bool IsBold
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
				return this.ᜀ.IsBold;
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
							goto IL_6D;
						}
						break;
					case 2:
						this.BeginUpdate();
						this.ᜀ.IsBold = value;
						this.EndUpdate();
						num = 0;
						continue;
					}
					IL_1C:
					if (true)
					{
					}
					if (value != this.IsBold)
					{
						num = 2;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_6D:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x00056C90 File Offset: 0x00055C90
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x00056CD8 File Offset: 0x00055CD8
		public ExcelColors KnownColor
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
				return this.ᜀ.KnownColor;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.BeginUpdate();
						this.ᜀ.KnownColor = value;
						this.EndUpdate();
						num = 2;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_65;
						}
						break;
					}
					IL_1C:
					if (value != this.KnownColor)
					{
						num = 0;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_65:
				if (true)
				{
				}
				if (false)
				{
				}
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x00056D64 File Offset: 0x00055D64
		// (set) Token: 0x06000852 RID: 2130 RVA: 0x00056DAC File Offset: 0x00055DAC
		public Color Color
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
				return this.ᜀ.Color;
			}
			set
			{
				int num = 2;
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
							goto IL_72;
						}
						break;
					case 1:
						this.BeginUpdate();
						this.ᜀ.Color = value;
						this.EndUpdate();
						num = 0;
						continue;
					}
					IL_1C:
					if (true)
					{
					}
					if (value != this.Color)
					{
						num = 1;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_72:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x00056E40 File Offset: 0x00055E40
		// (set) Token: 0x06000854 RID: 2132 RVA: 0x00056E88 File Offset: 0x00055E88
		public bool IsItalic
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
				return this.ᜀ.IsItalic;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.BeginUpdate();
						this.ᜀ.IsItalic = value;
						this.EndUpdate();
						if (true)
						{
						}
						num = 2;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_6D;
						}
						break;
					}
					IL_1C:
					if (value != this.IsItalic)
					{
						num = 1;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_6D:
				if (false)
				{
				}
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x00056F14 File Offset: 0x00055F14
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x00056F5C File Offset: 0x00055F5C
		public bool MacOSOutlineFont
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
				return this.ᜀ.MacOSOutlineFont;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.BeginUpdate();
						this.ᜀ.MacOSOutlineFont = value;
						this.EndUpdate();
						num = 2;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_65;
						}
						break;
					}
					IL_1C:
					if (value != this.MacOSOutlineFont)
					{
						num = 0;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_65:
				if (true)
				{
				}
				if (false)
				{
				}
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x00056FE8 File Offset: 0x00055FE8
		// (set) Token: 0x06000858 RID: 2136 RVA: 0x00057030 File Offset: 0x00056030
		public bool MacOSShadow
		{
			get
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
				return this.ᜀ.MacOSShadow;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.BeginUpdate();
						this.ᜀ.MacOSShadow = value;
						this.EndUpdate();
						num = 1;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_6D;
						}
						break;
					}
					IL_1C:
					if (true)
					{
					}
					if (value != this.MacOSShadow)
					{
						num = 0;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_6D:
				if (false)
				{
				}
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x000570BC File Offset: 0x000560BC
		// (set) Token: 0x0600085A RID: 2138 RVA: 0x00057104 File Offset: 0x00056104
		public double Size
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
				return this.ᜀ.Size;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.BeginUpdate();
						this.ᜀ.Size = value;
						this.EndUpdate();
						num = 1;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_6D;
						}
						break;
					}
					IL_1C:
					if (true)
					{
					}
					if (value != this.Size)
					{
						num = 0;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_6D:
				if (false)
				{
				}
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x00057190 File Offset: 0x00056190
		// (set) Token: 0x0600085C RID: 2140 RVA: 0x000571D8 File Offset: 0x000561D8
		public bool IsStrikethrough
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
				return this.ᜀ.IsStrikethrough;
			}
			set
			{
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.BeginUpdate();
						this.ᜀ.IsStrikethrough = value;
						this.EndUpdate();
						num = 2;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_6D;
						}
						break;
					}
					IL_24:
					if (value != this.IsStrikethrough)
					{
						num = 1;
						continue;
					}
					return;
					goto IL_24;
				}
				IL_6D:
				if (false)
				{
				}
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x00057264 File Offset: 0x00056264
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x000572AC File Offset: 0x000562AC
		public bool IsSubscript
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
				return this.ᜀ.IsSubscript;
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
							goto IL_6D;
						}
						break;
					case 2:
						this.BeginUpdate();
						this.ᜀ.IsSubscript = value;
						this.EndUpdate();
						num = 1;
						continue;
					}
					IL_1C:
					if (true)
					{
					}
					if (value != this.IsSubscript)
					{
						num = 2;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_6D:
				if (false)
				{
				}
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x00057338 File Offset: 0x00056338
		// (set) Token: 0x06000860 RID: 2144 RVA: 0x00057380 File Offset: 0x00056380
		public bool IsSuperscript
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
				return this.ᜀ.IsSuperscript;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_6D;
						}
						break;
					case 1:
						this.BeginUpdate();
						this.ᜀ.IsSuperscript = value;
						this.EndUpdate();
						num = 0;
						continue;
					}
					IL_24:
					if (value != this.IsSuperscript)
					{
						num = 1;
						continue;
					}
					return;
					goto IL_24;
				}
				IL_6D:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x0005740C File Offset: 0x0005640C
		// (set) Token: 0x06000862 RID: 2146 RVA: 0x00057454 File Offset: 0x00056454
		public FontUnderlineType Underline
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
				return this.ᜀ.Underline;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.BeginUpdate();
						this.ᜀ.Underline = value;
						this.EndUpdate();
						num = 2;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_65;
						}
						break;
					}
					IL_1C:
					if (value != this.Underline)
					{
						num = 1;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_65:
				if (true)
				{
				}
				if (false)
				{
				}
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x000574E0 File Offset: 0x000564E0
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x00057528 File Offset: 0x00056528
		public string FontName
		{
			get
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
				return this.ᜀ.FontName;
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
							goto IL_6A;
						}
						break;
					case 2:
						this.BeginUpdate();
						this.ᜀ.FontName = value;
						this.EndUpdate();
						num = 0;
						continue;
					}
					IL_1C:
					if (value != this.FontName)
					{
						num = 2;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_6A:
				if (true)
				{
				}
				if (false)
				{
				}
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x000575BC File Offset: 0x000565BC
		// (set) Token: 0x06000866 RID: 2150 RVA: 0x00057604 File Offset: 0x00056604
		public FontVertialAlignmentType VerticalAlignment
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
				return this.ᜀ.VerticalAlignment;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.BeginUpdate();
						this.ᜀ.VerticalAlignment = value;
						this.EndUpdate();
						num = 2;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_65;
						}
						break;
					}
					IL_1C:
					if (value != this.VerticalAlignment)
					{
						num = 0;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_65:
				if (false)
				{
				}
				if (true)
				{
				}
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00057690 File Offset: 0x00056690
		public Font GenerateNativeFont()
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
			return this.ᜀ.GenerateNativeFont();
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x000576D8 File Offset: 0x000566D8
		public bool IsAutoColor
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
				return false;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x00057714 File Offset: 0x00056714
		internal spr\u1DF5 ReservedHandle
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
				return this.ᜀ.ReservedHandle;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x0005775C File Offset: 0x0005675C
		public object Parent
		{
			get
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
				return this.ᜀ.Parent;
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x000577A4 File Offset: 0x000567A4
		public void ColorObjectUpdate()
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
			this.BeginUpdate();
			this.ᜀ.OColor.ᜀ(this.ᜄ, true);
			this.EndUpdate();
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00057804 File Offset: 0x00056804
		public FontWrapper Clone(XlsWorkbook book, object parent, IDictionary dicFontIndexes)
		{
			FontWrapper fontWrapper;
			int num;
			for (;;)
			{
				IL_3A:
				fontWrapper = new FontWrapper();
				num = this.ᜀ.Index;
				for (;;)
				{
					IL_4C:
					int num2 = 2;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4C;
						default:
							if (false)
							{
							}
							switch (num2)
							{
							case 0:
								goto IL_86;
							case 1:
								num = (int)dicFontIndexes[num];
								num2 = 0;
								continue;
							case 2:
								if (dicFontIndexes != null)
								{
									if (true)
									{
									}
									num2 = 1;
									continue;
								}
								goto IL_88;
							}
							goto IL_3A;
						}
					}
				}
			}
			IL_86:
			IL_88:
			fontWrapper.ᜁ = this.ᜁ;
			fontWrapper.ᜀ = (XlsFont)book.InnerFonts[num];
			return fontWrapper;
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600086D RID: 2157 RVA: 0x000578C0 File Offset: 0x000568C0
		// (remove) Token: 0x0600086E RID: 2158 RVA: 0x00057958 File Offset: 0x00056958
		public event EventHandler AfterChangeEvent
		{
			add
			{
				for (;;)
				{
					IL_42:
					EventHandler eventHandler = this.ᜅ;
					for (;;)
					{
						IL_49:
						int num = 2;
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_49;
							default:
							{
								if (false)
								{
								}
								if (true)
								{
								}
								EventHandler eventHandler2;
								switch (num)
								{
								case 0:
									if (eventHandler == eventHandler2)
									{
										num = 1;
										continue;
									}
									goto IL_53;
								case 1:
									return;
								case 2:
									goto IL_53;
								}
								goto IL_42;
								IL_53:
								eventHandler2 = eventHandler;
								EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
								eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ᜅ, value2, eventHandler2);
								num = 0;
								break;
							}
							}
						}
					}
				}
			}
			remove
			{
				for (;;)
				{
					IL_3A:
					EventHandler eventHandler = this.ᜅ;
					for (;;)
					{
						IL_41:
						if (true)
						{
						}
						int num = 0;
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_41;
							default:
							{
								if (false)
								{
								}
								EventHandler eventHandler2;
								switch (num)
								{
								case 0:
									goto IL_53;
								case 1:
									if (eventHandler == eventHandler2)
									{
										num = 2;
										continue;
									}
									goto IL_53;
								case 2:
									return;
								}
								goto IL_3A;
								IL_53:
								eventHandler2 = eventHandler;
								EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
								eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ᜅ, value2, eventHandler2);
								num = 1;
								break;
							}
							}
						}
					}
				}
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x000579F0 File Offset: 0x000569F0
		public int FontIndex
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
				return this.ᜀ.Index;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x00057A38 File Offset: 0x00056A38
		// (set) Token: 0x06000871 RID: 2161 RVA: 0x00057A7C File Offset: 0x00056A7C
		internal XlsFont Wrapped
		{
			get
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
				return this.ᜀ;
			}
			set
			{
				int a_ = 16;
				while (value == null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						throw new ArgumentNullException(RecordTableEnumerator.b("ぅ⥇♉㥋⭍", a_));
					}
				}
				this.ᜀ = value;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x00057AE0 File Offset: 0x00056AE0
		// (set) Token: 0x06000873 RID: 2163 RVA: 0x00057B24 File Offset: 0x00056B24
		public bool IsReadOnly
		{
			get
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
				return this.ᜁ;
			}
			set
			{
				int a_ = 17;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						num = 3;
						continue;
					case 2:
						goto IL_90;
					case 3:
						if (this.ᜁ)
						{
							num = 2;
							continue;
						}
						goto IL_92;
					}
					if (value)
					{
						goto IL_92;
					}
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
						num = 1;
						break;
					}
				}
				IL_90:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ņ♈╊㥌潎㡐⁒畔╖㱘㩚㥜ぞའརᱤ䭦䥨ࡪ౬ŮὰᱲŴ坶᭸Ṻ嵼᱾ꎌ", a_));
				IL_92:
				this.ᜁ = value;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x00057BCC File Offset: 0x00056BCC
		public XlsWorkbook Workbook
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
				return this.ᜀ.ParentWorkbook;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x00057C14 File Offset: 0x00056C14
		// (set) Token: 0x06000876 RID: 2166 RVA: 0x00057C58 File Offset: 0x00056C58
		internal bool IsDirectly
		{
			get
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

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x00057C9C File Offset: 0x00056C9C
		public OColor OColor
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
				return this.ᜀ.Color;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x00057CE8 File Offset: 0x00056CE8
		public int Index
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
				return this.ᜀ.Index;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x00057D30 File Offset: 0x00056D30
		public XlsFont Font
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

		// Token: 0x0600087A RID: 2170 RVA: 0x00057D74 File Offset: 0x00056D74
		public override void BeginUpdate()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 7;
					continue;
				case 2:
					return;
				case 3:
					if (true)
					{
					}
					if (!this.ᜃ)
					{
						num = 4;
						continue;
					}
					goto IL_105;
				case 4:
					this.ᜀ = (XlsFont)this.Workbook.CreateFont(this.ᜀ, false);
					num = 5;
					continue;
				case 5:
					goto IL_FC;
				case 6:
					goto IL_D2;
				case 7:
					if (this.ᜁ)
					{
						num = 6;
						continue;
					}
					num = 8;
					continue;
				case 8:
					if (!this.ᜂ)
					{
						num = 2;
						continue;
					}
					num = 3;
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
					if (base.BeginCallsCount != 0)
					{
						goto IL_105;
					}
					num = 0;
					break;
				}
			}
			return;
			IL_D2:
			throw new spr\u23DE();
			IL_FC:
			IL_105:
			base.BeginUpdate();
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00057E8C File Offset: 0x00056E8C
		public override void EndUpdate()
		{
			for (;;)
			{
				base.EndUpdate();
				int num = 9;
				for (;;)
				{
					XlsWorkbook workbook;
					switch (num)
					{
					case 0:
						if (this.ᜅ != null)
						{
							num = 7;
							continue;
						}
						return;
					case 1:
						return;
					case 2:
						num = 5;
						continue;
					case 3:
						return;
					case 4:
						goto IL_53;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_90;
						default:
							if (false)
							{
							}
							if (!this.ᜂ)
							{
								num = 1;
								continue;
							}
							workbook = this.Workbook;
							num = 6;
							continue;
						}
						break;
					case 6:
						goto IL_90;
					case 7:
						if (true)
						{
						}
						this.ᜅ(this, EventArgs.Empty);
						num = 3;
						continue;
					case 8:
						this.ᜀ = (XlsFont)workbook.AddFont(this.ᜀ);
						num = 4;
						continue;
					case 9:
						if (base.BeginCallsCount == 0)
						{
							num = 2;
							continue;
						}
						return;
					}
					break;
					IL_53:
					workbook.SetChanged();
					num = 0;
					continue;
					IL_90:
					if (this.ᜃ)
					{
						goto IL_53;
					}
					num = 8;
				}
			}
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00057FCC File Offset: 0x00056FCC
		internal void ᜁ()
		{
			int num = 1;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					this.ᜅ(this, EventArgs.Empty);
					num = 2;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_65;
					}
					break;
				}
				IL_24:
				if (this.ᜅ != null)
				{
					num = 0;
					continue;
				}
				return;
				goto IL_24;
			}
			IL_65:
			if (false)
			{
			}
		}

		// Token: 0x0400017F RID: 383
		private float \u25D9\u008D\u00A4\u0095;

		// Token: 0x04000180 RID: 384
		private XlsFont ᜀ;

		// Token: 0x04000181 RID: 385
		private bool ᜁ;

		// Token: 0x04000182 RID: 386
		private string[] \u2609\u00A0\u008A\u009E;

		// Token: 0x04000183 RID: 387
		private bool ᜂ;

		// Token: 0x04000184 RID: 388
		private long \u2460\u00A6\u009F\u0086;

		// Token: 0x04000185 RID: 389
		private byte \u25D8\u00A2\u00A8\u0099;

		// Token: 0x04000186 RID: 390
		private int[] \u2460\u00A4\u00A7\u009F;

		// Token: 0x04000187 RID: 391
		private bool ᜃ;

		// Token: 0x04000188 RID: 392
		private OColor ᜄ;

		// Token: 0x04000189 RID: 393
		private byte \u25D8\u0081\u0082\u008D;

		// Token: 0x0400018A RID: 394
		private EventHandler ᜅ;
	}
}
