using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Spire.Doc.Formatting
{
	// Token: 0x02000477 RID: 1143
	public class XmlTableFormat
	{
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06003FE0 RID: 16352 RVA: 0x003AF360 File Offset: 0x003AE360
		// (set) Token: 0x06003FE1 RID: 16353 RVA: 0x003AF3E4 File Offset: 0x003AE3E4
		internal List<Stream> NodeArray2010
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							this.ᜀ = new List<Stream>();
							num = 0;
							continue;
						}
						break;
					}
					if (true)
					{
					}
					if (this.ᜀ != null)
					{
						break;
					}
					num = 1;
				}
				IL_6F:
				return this.ᜀ;
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
				this.ᜀ = value;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06003FE2 RID: 16354 RVA: 0x003AF428 File Offset: 0x003AE428
		// (set) Token: 0x06003FE3 RID: 16355 RVA: 0x003AF4AC File Offset: 0x003AE4AC
		internal List<XmlNode> NodeArray
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_6F;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							this.ᜁ = new List<XmlNode>();
							if (true)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					if (this.ᜁ != null)
					{
						break;
					}
					num = 2;
				}
				IL_6F:
				return this.ᜁ;
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
				this.ᜁ = value;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06003FE4 RID: 16356 RVA: 0x003AF4F0 File Offset: 0x003AE4F0
		// (set) Token: 0x06003FE5 RID: 16357 RVA: 0x003AF534 File Offset: 0x003AE534
		internal string StyleName
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

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06003FE6 RID: 16358 RVA: 0x003AF578 File Offset: 0x003AE578
		internal RowFormat Format
		{
			get
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
							continue;
						default:
							if (false)
							{
							}
							this.ᜃ = new RowFormat();
							this.ᜃ.ᜀ(this.ᜄ);
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_80;
					}
					if (true)
					{
					}
					if (this.ᜃ != null)
					{
						break;
					}
					num = 0;
				}
				IL_80:
				return this.ᜃ;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06003FE7 RID: 16359 RVA: 0x003AF610 File Offset: 0x003AE610
		internal bool HasFormat
		{
			get
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_88;
					case 1:
						if (this.ᜁ != null)
						{
							num = 6;
							continue;
						}
						return false;
					case 2:
						if (this.ᜁ.Count > 0)
						{
							num = 7;
							continue;
						}
						return false;
					case 3:
						if (this.ᜃ.IsDefault)
						{
							num = 0;
							continue;
						}
						return true;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_88;
						}
						if (false)
						{
						}
						break;
					case 5:
						num = 3;
						continue;
					case 6:
						num = 2;
						continue;
					case 7:
						return true;
					}
					if (this.ᜃ != null)
					{
						num = 5;
						continue;
					}
					IL_88:
					if (true)
					{
					}
					num = 1;
				}
				return true;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06003FE8 RID: 16360 RVA: 0x003AF6F0 File Offset: 0x003AE6F0
		internal Table Owner
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
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x003AF734 File Offset: 0x003AE734
		internal XmlTableFormat(Table A_0)
		{
			this.ᜄ = A_0;
		}

		// Token: 0x06003FEA RID: 16362 RVA: 0x003AF750 File Offset: 0x003AE750
		internal XmlTableFormat ᜀ(Table A_0)
		{
			XmlTableFormat xmlTableFormat;
			for (;;)
			{
				xmlTableFormat = new XmlTableFormat(A_0);
				xmlTableFormat.Format.ImportContainer(this.ᜃ);
				xmlTableFormat.Format.LayoutType = this.ᜃ.LayoutType;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						xmlTableFormat.Format.Scaling = this.ᜃ.Scaling;
						goto IL_A5;
					case 1:
						if (this.ᜃ.Scaling != 100f)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_A5;
							}
							if (false)
							{
							}
							num = 0;
							continue;
						}
						goto IL_BA;
					case 2:
						goto IL_B8;
					}
					break;
					IL_A5:
					if (true)
					{
					}
					num = 2;
				}
			}
			IL_B8:
			IL_BA:
			xmlTableFormat.StyleName = this.ᜂ;
			xmlTableFormat.Owner.ᜀ(A_0.OwnerTextBody);
			xmlTableFormat.NodeArray = this.ᜁ;
			xmlTableFormat.NodeArray2010 = this.ᜀ;
			return xmlTableFormat;
		}

		// Token: 0x06003FEB RID: 16363 RVA: 0x003AF850 File Offset: 0x003AE850
		internal void ᜂ()
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_5E;
				case 1:
					this.ᜃ.Close();
					this.ᜃ = null;
					num = 0;
					continue;
				case 2:
					return;
				case 3:
					if (true)
					{
					}
					break;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (this.ᜁ != null)
						{
							num = 5;
							continue;
						}
						return;
					}
					break;
				case 5:
					this.ᜁ.Clear();
					this.ᜁ = null;
					num = 2;
					continue;
				}
				if (this.ᜃ != null)
				{
					num = 1;
					continue;
				}
				IL_5E:
				num = 4;
			}
		}

		// Token: 0x04002E44 RID: 11844
		private List<Stream> ᜀ;

		// Token: 0x04002E45 RID: 11845
		private List<XmlNode> ᜁ;

		// Token: 0x04002E46 RID: 11846
		private string ᜂ;

		// Token: 0x04002E47 RID: 11847
		private RowFormat ᜃ;

		// Token: 0x04002E48 RID: 11848
		private long \u2609\u009F\u00A0\u008A;

		// Token: 0x04002E49 RID: 11849
		private Table ᜄ;
	}
}
