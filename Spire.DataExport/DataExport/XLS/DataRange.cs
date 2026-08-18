using System;
using System.ComponentModel;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Utils;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001B3 RID: 435
	public class DataRange : ICloneable
	{
		// Token: 0x06000C40 RID: 3136 RVA: 0x00080A6C File Offset: 0x0007FA6C
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
			return new DataRange
			{
				ColX = this.ColX,
				ColY = this.ColY,
				RowX = this.RowX,
				RowY = this.RowY
			};
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00080AE0 File Offset: 0x0007FAE0
		public void SaveToXmlFile(XMLFile File, string Section)
		{
			int a_ = 16;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			File.WriteValue(Section, HyperlinksCollectionEditor.b("缫娭儯䀱䀳电圷嘹", a_), this.ᜀ.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("缫娭儯䀱䀳搵圷䴹", a_), this.ᜂ.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("椫䀭启焱嬳娵", a_), this.ᜁ.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("椫䀭启怱嬳䄵", a_), this.ᜃ.ToString());
			File.SaveToFile();
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00080BAC File Offset: 0x0007FBAC
		public void LoadFromXmlFile(XMLFile File, string Section)
		{
			int a_ = 17;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.ᜀ = Convert.ToByte(File.ReadValue(Section, HyperlinksCollectionEditor.b("縬嬮倰䄲䄴琶嘸场", a_), 0.ToString()));
			this.ᜂ = (int)Convert.ToUInt16(File.ReadValue(Section, HyperlinksCollectionEditor.b("縬嬮倰䄲䄴收嘸䰺", a_), 0.ToString()));
			this.ᜁ = Convert.ToByte(File.ReadValue(Section, HyperlinksCollectionEditor.b("栬䄮唰瀲娴嬶", a_), 0.ToString()));
			this.ᜃ = (int)Convert.ToUInt16(File.ReadValue(Section, HyperlinksCollectionEditor.b("栬䄮唰愲娴䀶", a_), 0.ToString()));
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x00080C94 File Offset: 0x0007FC94
		// (set) Token: 0x06000C44 RID: 3140 RVA: 0x00080CD8 File Offset: 0x0007FCD8
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets the left side of the data range.")]
		public byte ColX
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
				int num = 2;
				for (;;)
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
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							this.ᜀ = value;
							num = 1;
							continue;
						case 1:
							return;
						}
						break;
					}
					IL_38:
					if (value != this.ᜀ)
					{
						num = 0;
						continue;
					}
					break;
					goto IL_38;
				}
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x00080D54 File Offset: 0x0007FD54
		// (set) Token: 0x06000C46 RID: 3142 RVA: 0x00080D98 File Offset: 0x0007FD98
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(0)]
		[Description("Gets or sets the right side of the data range.")]
		public byte ColY
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
				int num = 1;
				for (;;)
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
						switch (num)
						{
						case 0:
							this.ᜁ = value;
							num = 2;
							continue;
						case 2:
							return;
						}
						break;
					}
					IL_40:
					if (value != this.ᜁ)
					{
						num = 0;
						continue;
					}
					break;
					goto IL_40;
				}
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x00080E14 File Offset: 0x0007FE14
		// (set) Token: 0x06000C48 RID: 3144 RVA: 0x00080E58 File Offset: 0x0007FE58
		[Description("Gets the top of the data range.")]
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public int RowX
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
				int num = 1;
				for (;;)
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
						switch (num)
						{
						case 0:
							this.ᜂ = value;
							num = 2;
							continue;
						case 2:
							return;
						}
						break;
					}
					IL_40:
					if (value != this.ᜂ)
					{
						num = 0;
						continue;
					}
					break;
					goto IL_40;
				}
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x00080ED4 File Offset: 0x0007FED4
		// (set) Token: 0x06000C4A RID: 3146 RVA: 0x00080F18 File Offset: 0x0007FF18
		[DefaultValue(0)]
		[Description("Gets or sets the bottom of the data range.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public int RowY
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
						break;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 1:
							this.ᜃ = value;
							num = 2;
							continue;
						case 2:
							return;
						}
						break;
					}
					IL_40:
					if (value != this.ᜃ)
					{
						num = 1;
						continue;
					}
					break;
					goto IL_40;
				}
			}
		}

		// Token: 0x0400094B RID: 2379
		private bool \u25D8ª\u00A1\u0094;

		// Token: 0x0400094C RID: 2380
		private string \u2593\u00AF\u0092\u009D;

		// Token: 0x0400094D RID: 2381
		private byte ᜀ;

		// Token: 0x0400094E RID: 2382
		private byte ᜁ;

		// Token: 0x0400094F RID: 2383
		private int ᜂ;

		// Token: 0x04000950 RID: 2384
		private int ᜃ;

		// Token: 0x04000951 RID: 2385
		internal bool ᜄ;

		// Token: 0x04000952 RID: 2386
		internal string ᜅ = string.Empty;
	}
}
