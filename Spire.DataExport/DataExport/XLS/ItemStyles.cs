using System;
using Spire.DataExport.Collections;
using Spire.DataExport.Utils;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001DA RID: 474
	public class ItemStyles : Collection
	{
		// Token: 0x06000E57 RID: 3671 RVA: 0x0009F910 File Offset: 0x0009E910
		public ItemStyles(object Holder)
		{
			this.m_holder = Holder;
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0009F92C File Offset: 0x0009E92C
		public StripStyle Add(StripStyle Item)
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
			base.Add(Item);
			return Item;
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x0009F970 File Offset: 0x0009E970
		public bool IsEqual(ItemStyles Styles)
		{
			bool flag;
			for (;;)
			{
				flag = (base.Count == Styles.Count);
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return flag;
					case 1:
						IL_D0:
						goto IL_A2;
					case 2:
					{
						int num2;
						if (num2 >= base.Count)
						{
							num = 3;
							continue;
						}
						flag &= this[num2].IsEqual(Styles[num2]);
						num = 4;
						continue;
					}
					case 3:
						goto IL_D2;
					case 4:
						if (flag)
						{
							num = 5;
							continue;
						}
						goto IL_D2;
					case 5:
					{
						int num2;
						num2++;
						if (true)
						{
						}
						num = 6;
						continue;
					}
					case 6:
						goto IL_A2;
					case 7:
					{
						if (!flag)
						{
							num = 0;
							continue;
						}
						int num2 = 0;
						num = 1;
						continue;
					}
					}
					break;
					IL_A2:
					num = 2;
					continue;
					IL_D2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D0;
					default:
						goto IL_E8;
					}
				}
			}
			return flag;
			IL_E8:
			if (false)
			{
			}
			return flag;
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0009FA6C File Offset: 0x0009EA6C
		public void SaveToXmlFile(XMLFile File, string SectionPrefix)
		{
			for (;;)
			{
				int num = 0;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num >= base.Count)
						{
							num2 = 1;
							continue;
						}
						for (;;)
						{
							this[num].SaveToXmlFile(File, SectionPrefix + num.ToString());
							num++;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_73;
							}
						}
						IL_73:
						if (true)
						{
						}
						if (false)
						{
						}
						num2 = 2;
						continue;
					case 1:
						return;
					case 2:
						goto IL_24;
					case 3:
						goto IL_24;
					}
					break;
					IL_24:
					num2 = 0;
				}
			}
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0009FB14 File Offset: 0x0009EB14
		public void LoadFromXmlFile(XMLFile File, string SectionPrefix)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					Array array = null;
					base.Clear();
					File.ReadSections(ref array);
					int num = 6;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
							goto IL_F2;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_16E;
							default:
							{
								if (false)
								{
								}
								string text;
								string strA = text.Substring(0, SectionPrefix.Length);
								num = 3;
								continue;
							}
							}
							break;
						case 2:
						{
							string[] array2 = array as string[];
							string[] array3 = array2;
							num2 = 0;
							num = 5;
							continue;
						}
						case 3:
						{
							string strA;
							if (string.Compare(strA, SectionPrefix) == 0)
							{
								num = 9;
								continue;
							}
							goto IL_6F;
						}
						case 4:
							goto IL_16E;
						case 5:
							goto IL_F2;
						case 6:
							if (array != null)
							{
								num = 2;
								continue;
							}
							return;
						case 7:
						{
							string text;
							if (text.Length >= SectionPrefix.Length)
							{
								if (true)
								{
								}
								num = 1;
								continue;
							}
							goto IL_6F;
						}
						case 8:
							return;
						case 9:
						{
							string text;
							this.Add(new StripStyle()).LoadFromXmlFile(File, text);
							num = 4;
							continue;
						}
						case 10:
						{
							string[] array3;
							if (num2 >= array3.Length)
							{
								num = 8;
								continue;
							}
							string text = array3[num2];
							num = 7;
							continue;
						}
						}
						break;
						IL_6F:
						num2++;
						num = 0;
						continue;
						IL_16E:
						goto IL_6F;
						IL_F2:
						num = 10;
					}
				}
				return;
			}
		}

		// Token: 0x170001CE RID: 462
		public StripStyle this[int Index]
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
				return base[Index] as StripStyle;
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
				base[Index] = value;
			}
		}
	}
}
