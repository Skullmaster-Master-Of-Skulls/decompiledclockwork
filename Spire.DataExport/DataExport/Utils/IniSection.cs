using System;
using System.ComponentModel;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.Utils
{
	// Token: 0x0200023C RID: 572
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class IniSection
	{
		// Token: 0x06001179 RID: 4473 RVA: 0x000BD718 File Offset: 0x000BC718
		[Category("Section")]
		public IniSection(string secName, XMLSetting topObject)
		{
			this.Name = secName;
			this.ᜄ = topObject;
			this.ᜀ = new IniSettings(this.ᜄ);
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x000BD754 File Offset: 0x000BC754
		// (set) Token: 0x0600117B RID: 4475 RVA: 0x000BD798 File Offset: 0x000BC798
		[Description("Collection of settings")]
		public IniSettings Settings
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

		// Token: 0x1700027A RID: 634
		public IniSetting this[int itemNumber]
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
				return this.Settings[itemNumber];
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
				this.Settings[itemNumber] = value;
			}
		}

		// Token: 0x1700027B RID: 635
		public IniSetting this[string itemName]
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
				return this.Settings[itemName];
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
				this.Settings[itemName] = value;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x000BD8FC File Offset: 0x000BC8FC
		// (set) Token: 0x06001181 RID: 4481 RVA: 0x000BD940 File Offset: 0x000BC940
		[Description("Section name")]
		public string Name
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

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x000BD984 File Offset: 0x000BC984
		public object Parent
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
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x000BD9C8 File Offset: 0x000BC9C8
		public override string ToString()
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
			return this.Settings.Count + HyperlinksCollectionEditor.b("ബ簮吰䜲䄴帶圸尺丼", a_);
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x000BDA30 File Offset: 0x000BCA30
		// (set) Token: 0x06001185 RID: 4485 RVA: 0x000BDA74 File Offset: 0x000BCA74
		public bool DisplayInPG
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

		// Token: 0x04000C48 RID: 3144
		private IniSettings ᜀ;

		// Token: 0x04000C49 RID: 3145
		private string ᜁ;

		// Token: 0x04000C4A RID: 3146
		private bool ᜂ = true;

		// Token: 0x04000C4B RID: 3147
		internal object ᜃ;

		// Token: 0x04000C4C RID: 3148
		private float \u25D9\u00A0\u00A6\u00A4;

		// Token: 0x04000C4D RID: 3149
		private long[] \u2460\u0080\u00A4\u00AF;

		// Token: 0x04000C4E RID: 3150
		internal XMLSetting ᜄ;
	}
}
