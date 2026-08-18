using System;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.Utils
{
	// Token: 0x02000237 RID: 567
	public class XMLFile
	{
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x000BA684 File Offset: 0x000B9684
		// (set) Token: 0x06001143 RID: 4419 RVA: 0x000BA6C8 File Offset: 0x000B96C8
		public string FileName
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

		// Token: 0x06001144 RID: 4420 RVA: 0x000BA70C File Offset: 0x000B970C
		public XMLFile()
		{
			this.ᜁ = new XMLSetting(this.ᜀ);
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x000BA73C File Offset: 0x000B973C
		public XMLFile(string fileName) : this()
		{
			this.ᜀ = fileName;
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x000BA758 File Offset: 0x000B9758
		protected override void Finalize()
		{
			try
			{
				int num = 3;
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
							this.ᜁ = null;
							num = 1;
							continue;
						case 1:
							goto IL_5F;
						case 2:
							goto IL_67;
						}
						if (this.ᜁ != null)
						{
							num = 0;
							continue;
						}
						break;
					}
					IL_5F:
					num = 2;
				}
				IL_67:;
			}
			finally
			{
				base.Finalize();
			}
			if (true)
			{
			}
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x000BA7F8 File Offset: 0x000B97F8
		public bool LoadFromFile(string fileName)
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
			return this.ᜁ.Load(fileName);
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x000BA840 File Offset: 0x000B9840
		public bool SaveToFile(string fileName)
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
			return this.ᜁ.Save(fileName);
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x000BA888 File Offset: 0x000B9888
		public bool SaveToFile()
		{
			int a_ = 17;
			while (this.ᜀ.Trim().Length == 0)
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
					throw new Exception(HyperlinksCollectionEditor.b("礬䜮吰ጲ匴帶唸帺ᴼ儾⁀⹂⁄杆⩈⩊⍌潎㽐㱒⅔睖㭘㹚絜㩞ౠ።ᅤṦ䡨", a_));
				}
			}
			return this.SaveToFile(this.ᜀ);
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x000BA900 File Offset: 0x000B9900
		public string ReadValue(string SectionName, string Key, string DefaultValue)
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
			return this.ᜁ.GetVal(SectionName, Key, DefaultValue);
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x000BA950 File Offset: 0x000B9950
		public string ReadValue(string SectionName, string Key)
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
			return this.ᜁ.GetVal(SectionName, Key);
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x000BA9A0 File Offset: 0x000B99A0
		public void WriteValue(string SectionName, string Key, string Value)
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
			this.ᜁ.SetVal(SectionName, Key, Value);
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x000BA9EC File Offset: 0x000B99EC
		public void RemoveValue(string SectionName, string Key)
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
			this.ᜁ.Remove(SectionName, Key);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x000BAA34 File Offset: 0x000B9A34
		public void RemoveSection(string SectionName)
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
			this.ᜁ.Remove(SectionName);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x000BAA7C File Offset: 0x000B9A7C
		public void ReadValues(string SectionName, ref Array values)
		{
			int a_ = 10;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(HyperlinksCollectionEditor.b("爥䀧伩ఫ簭唯匱倳怵夷嘹䤻嬽㌿扁⥃⍅㱇≉⍋⩍灏㭑❓癕㥗㡙⽛⩝቟͡ݣብ䥧", a_));
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x000BAAD4 File Offset: 0x000B9AD4
		public void ReadSections(ref Array Sections)
		{
			int a_ = 3;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(HyperlinksCollectionEditor.b("䬞䤠䘢Ԥ甦䰨䨪䤬簮吰倲䄴帶嘸唺丼Ἶⱀ♂ㅄ⽆♈⽊浌♎≐獒㑔㕖⩘⽚⽜㹞ɠᝢ䑤", a_));
		}

		// Token: 0x04000C2E RID: 3118
		private int \u2609\u0094\u0080\u00A4;

		// Token: 0x04000C2F RID: 3119
		private byte[] \u2593\u00AC\u0091\u00AE;

		// Token: 0x04000C30 RID: 3120
		private string ᜀ = string.Empty;

		// Token: 0x04000C31 RID: 3121
		private XMLSetting ᜁ;
	}
}
