using System;
using System.Collections;
using Spire.DataExport.Utils;

namespace Spire.DataExport.RTF
{
	// Token: 0x02000171 RID: 369
	public class RTFStyles : CollectionBase
	{
		// Token: 0x060009B5 RID: 2485 RVA: 0x000628D0 File Offset: 0x000618D0
		public RTFStyles(object Holder)
		{
			this.ᜀ = Holder;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x000628EC File Offset: 0x000618EC
		public RtfItemStyle Add(RtfItemStyle Item)
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
			base.InnerList.Add(Item);
			return Item;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00062938 File Offset: 0x00061938
		public void SaveToXmlFile(XMLFile File, string SectionPrefix)
		{
			for (;;)
			{
				int num = 0;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num < base.Count)
						{
							this[num].SaveToXmlFile(File, SectionPrefix + num.ToString());
							num++;
							num2 = 2;
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
							num2 = 3;
							continue;
						}
						break;
					case 1:
						if (true)
						{
						}
						goto IL_2C;
					case 2:
						goto IL_2C;
					case 3:
						return;
					}
					break;
					IL_2C:
					num2 = 0;
				}
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x000629E0 File Offset: 0x000619E0
		public void LoadFromXmlFile(XMLFile File, string SectionPrefix)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					base.Clear();
					Array array = null;
					File.ReadSections(ref array);
					int num = 10;
					for (;;)
					{
						int num2;
						string[] array2;
						switch (num)
						{
						case 0:
						{
							string text;
							string strA = text.Substring(0, SectionPrefix.Length);
							num = 1;
							continue;
						}
						case 1:
						{
							string strA;
							if (string.Compare(strA, SectionPrefix, true) == 0)
							{
								num = 3;
								continue;
							}
							goto IL_6F;
						}
						case 2:
							goto IL_6D;
						case 3:
						{
							string text;
							this.Add(new RtfItemStyle()).LoadFromXmlFile(File, text);
							num = 5;
							continue;
						}
						case 4:
							goto IL_D0;
						case 5:
							goto IL_6F;
						case 6:
							return;
						case 7:
						{
							string text;
							if (text.Length < SectionPrefix.Length)
							{
								goto IL_6F;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_6D;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								num = 0;
								continue;
							}
							break;
						}
						case 8:
							goto IL_D0;
						case 9:
						{
							if (num2 >= array2.Length)
							{
								num = 6;
								continue;
							}
							string text = array2[num2];
							num = 7;
							continue;
						}
						case 10:
							if (array != null)
							{
								num = 2;
								continue;
							}
							return;
						}
						break;
						IL_6D:
						string[] array3 = array as string[];
						array2 = array3;
						num2 = 0;
						num = 8;
						continue;
						IL_6F:
						num2++;
						num = 4;
						continue;
						IL_D0:
						num = 9;
					}
				}
				return;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x00062B64 File Offset: 0x00061B64
		public object Holder
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
		}

		// Token: 0x1700006B RID: 107
		public RtfItemStyle this[int Index]
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
				return base.InnerList[Index] as RtfItemStyle;
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
				base.InnerList[Index] = value;
			}
		}

		// Token: 0x0400075C RID: 1884
		private float \u25D9\u00B0\u0087\u008D;

		// Token: 0x0400075D RID: 1885
		private byte \u25D8\u0091\u009C\u00A5;

		// Token: 0x0400075E RID: 1886
		private float[] \u25D8\u0085\u00AE\u0097;

		// Token: 0x0400075F RID: 1887
		private byte[] \u25D9\u00AE\u0097\u00A9;

		// Token: 0x04000760 RID: 1888
		private object ᜀ;
	}
}
