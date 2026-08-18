using System;
using System.ComponentModel;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Utils;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001C6 RID: 454
	public class ChartAutoPosition : ICloneable
	{
		// Token: 0x06000D64 RID: 3428 RVA: 0x00094738 File Offset: 0x00093738
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
			return new ChartAutoPosition
			{
				Placement = this.Placement,
				Height = this.Height,
				Left = this.Left,
				Top = this.Top,
				Width = this.Width
			};
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x000947B8 File Offset: 0x000937B8
		public void SaveToXmlFile(XMLFile File, string Section)
		{
			int a_ = 6;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			string key = HyperlinksCollectionEditor.b("愡䰣䜥娧帩猫縭尯匱圳匵唷弹刻䨽", a_);
			int num = (int)this.ᜀ;
			File.WriteValue(Section, key, num.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("愡䰣䜥娧帩猫昭唯嬱匳帵䰷", a_), this.ᜁ.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("愡䰣䜥娧帩猫戭唯吱䀳", a_), this.ᜂ.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("愡䰣䜥娧帩猫稭弯䈱", a_), this.ᜃ.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("愡䰣䜥娧帩猫礭夯嘱䀳帵", a_), this.ᜄ.ToString());
			File.SaveToFile();
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x000948A8 File Offset: 0x000938A8
		public void LoadFromXmlFile(XMLFile File, string Section)
		{
			int a_ = 3;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.ᜀ = (ChartPlacement)Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("尞䤠䈢圤匦瘨笪䄬丮到嘲場制圸伺", a_), 0.ToString()));
			this.ᜁ = Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("尞䤠䈢圤匦瘨挪䠬䘮嘰嬲䄴", a_), 10.ToString()));
			this.ᜂ = Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("尞䤠䈢圤匦瘨未䠬䤮䔰", a_), 0.ToString()));
			this.ᜃ = Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("尞䤠䈢圤匦瘨缪䈬弮", a_), 0.ToString()));
			this.ᜄ = Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("尞䤠䈢圤匦瘨簪䐬䬮䔰嬲", a_), 5.ToString()));
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x000949BC File Offset: 0x000939BC
		// (set) Token: 0x06000D68 RID: 3432 RVA: 0x00094A00 File Offset: 0x00093A00
		[DefaultValue(ChartPlacement.Bottom)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public ChartPlacement Placement
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

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x00094A44 File Offset: 0x00093A44
		// (set) Token: 0x06000D6A RID: 3434 RVA: 0x00094A88 File Offset: 0x00093A88
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(10)]
		public int Height
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

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x00094ACC File Offset: 0x00093ACC
		// (set) Token: 0x06000D6C RID: 3436 RVA: 0x00094B10 File Offset: 0x00093B10
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(0)]
		public int Left
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

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x00094B54 File Offset: 0x00093B54
		// (set) Token: 0x06000D6E RID: 3438 RVA: 0x00094B98 File Offset: 0x00093B98
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(0)]
		public int Top
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
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜃ = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x00094BDC File Offset: 0x00093BDC
		// (set) Token: 0x06000D70 RID: 3440 RVA: 0x00094C20 File Offset: 0x00093C20
		[DefaultValue(5)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public int Width
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
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜄ = value;
			}
		}

		// Token: 0x040009FF RID: 2559
		private ChartPlacement ᜀ;

		// Token: 0x04000A00 RID: 2560
		private float[] \u25D9\u00A9\u00A8\u00AD;

		// Token: 0x04000A01 RID: 2561
		private bool \u25D9\u00A1\u0093ª;

		// Token: 0x04000A02 RID: 2562
		private int ᜁ = 10;

		// Token: 0x04000A03 RID: 2563
		private int ᜂ;

		// Token: 0x04000A04 RID: 2564
		private int \u2609\u009A\u0097\u0090;

		// Token: 0x04000A05 RID: 2565
		private int ᜃ;

		// Token: 0x04000A06 RID: 2566
		private int ᜄ = 5;
	}
}
