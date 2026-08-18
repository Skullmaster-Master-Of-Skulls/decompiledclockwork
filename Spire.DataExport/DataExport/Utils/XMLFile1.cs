using System;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.Utils
{
	// Token: 0x02000235 RID: 565
	public class XMLFile1
	{
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x000B8250 File Offset: 0x000B7250
		// (set) Token: 0x06001122 RID: 4386 RVA: 0x000B8294 File Offset: 0x000B7294
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

		// Token: 0x06001123 RID: 4387 RVA: 0x000B82D8 File Offset: 0x000B72D8
		public XMLFile1()
		{
			this.ᜁ = new XMLSetting(this.ᜀ);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x000B8308 File Offset: 0x000B7308
		public XMLFile1(string fileName) : this()
		{
			this.ᜀ = fileName;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x000B8324 File Offset: 0x000B7324
		protected override void Finalize()
		{
			try
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
			}
			finally
			{
				base.Finalize();
			}
			if (true)
			{
			}
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x000B8378 File Offset: 0x000B7378
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

		// Token: 0x06001127 RID: 4391 RVA: 0x000B83C0 File Offset: 0x000B73C0
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

		// Token: 0x06001128 RID: 4392 RVA: 0x000B8408 File Offset: 0x000B7408
		public bool SaveToFile()
		{
			int a_ = 19;
			if (this.ᜀ.Trim().Length == 0)
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
					throw new Exception(HyperlinksCollectionEditor.b("笮夰嘲ᔴ儶倸场堼Ἶ⽀≂⡄≆楈⡊ⱌⅎ煐㵒㩔⍖祘㥚㡜罞Ѡ๢ᕤ፦ၨ䩪", a_));
				}
			}
			return this.SaveToFile(this.ᜀ);
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x000B8480 File Offset: 0x000B7480
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

		// Token: 0x0600112A RID: 4394 RVA: 0x000B84D0 File Offset: 0x000B74D0
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

		// Token: 0x0600112B RID: 4395 RVA: 0x000B8520 File Offset: 0x000B7520
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

		// Token: 0x0600112C RID: 4396 RVA: 0x000B856C File Offset: 0x000B756C
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

		// Token: 0x0600112D RID: 4397 RVA: 0x000B85B4 File Offset: 0x000B75B4
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

		// Token: 0x0600112E RID: 4398 RVA: 0x000B85FC File Offset: 0x000B75FC
		public void ReadValues(string SectionName, ref Array values)
		{
			int a_ = 1;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(HyperlinksCollectionEditor.b("䤜眞䐠̢眤䈦䠨伪第丮崰䘲倴䐶ᤸ嘺堼䬾⥀ⱂ⅄杆⁈㡊浌⹎㍐⁒⅔╖㡘㡚⥜繞", a_));
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x000B8654 File Offset: 0x000B7654
		public void ReadSections(ref Array Sections)
		{
			int a_ = 4;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(HyperlinksCollectionEditor.b("琟䨡䄣إ稧伩䴫䨭振圱圳䈵儷唹刻䴽怿⽁⅃㉅⁇╉⡋湍㥏⅑瑓㝕㩗⥙⡛ⱝşšၣ䝥", a_));
		}

		// Token: 0x04000C2A RID: 3114
		private float \u25D9\u0081\u008C\u0097;

		// Token: 0x04000C2B RID: 3115
		private int \u25D8\u008B\u0085\u0095;

		// Token: 0x04000C2C RID: 3116
		private string ᜀ = string.Empty;

		// Token: 0x04000C2D RID: 3117
		private XMLSetting ᜁ;
	}
}
