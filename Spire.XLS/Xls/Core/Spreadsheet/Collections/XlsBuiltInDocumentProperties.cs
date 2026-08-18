using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Spire.CompoundFile.XLS;
using Spire.CompoundFile.XLS.Native;
using Spire.Xls.Core.Interface;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x0200002F RID: 47
	public class XlsBuiltInDocumentProperties : CollectionExtended<DocumentProperty>, IBuiltInDocumentProperties
	{
		// Token: 0x06000344 RID: 836 RVA: 0x0001DE04 File Offset: 0x0001CE04
		internal XlsBuiltInDocumentProperties(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x1700011C RID: 284
		public IDocumentProperty this[BuiltInPropertyType index]
		{
			get
			{
				IDictionary dictionary;
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
					dictionary = this.GetDictionary(index);
					if (!dictionary.Contains((int)index))
					{
						DocumentProperty documentProperty = new DocumentProperty(index, null);
						this.Add(documentProperty);
						return documentProperty;
					}
					break;
				}
				return (IDocumentProperty)dictionary[(int)index];
			}
		}

		// Token: 0x1700011D RID: 285
		public IDocumentProperty this[int iIndex]
		{
			get
			{
				int a_ = 14;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 1:
						goto IL_A5;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							if (iIndex <= base.Count - 1)
							{
								goto IL_A7;
							}
							break;
						}
						num = 1;
						continue;
					}
					if (iIndex < 0)
					{
						break;
					}
					if (true)
					{
					}
					num = 0;
				}
				IL_3F:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⵃཅ♇⹉⥋㙍", a_), RecordTableEnumerator.b("ቃ❅⑇㽉⥋湍㍏㍑㩓㡕㝗⹙籛㱝՟䉡ࡣͥ᭧ᥩ䱫ᩭᡯ፱ᩳ噵䡷婹ᵻၽꊁ늑ﺕ聯벛좟쎡쪣蚥얩\ud9ab삭쒯銱馳隵覷钹", a_));
				IL_A5:
				goto IL_3F;
				IL_A7:
				return base.List[iIndex];
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0001DF68 File Offset: 0x0001CF68
		public bool Contains(BuiltInPropertyType index)
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
			IDictionary dictionary = this.GetDictionary(index);
			return dictionary.Contains((int)index);
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0001DFBC File Offset: 0x0001CFBC
		// (set) Token: 0x06000349 RID: 841 RVA: 0x0001E010 File Offset: 0x0001D010
		public string Title
		{
			get
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
					if (this.Contains(BuiltInPropertyType.Title))
					{
						return this[BuiltInPropertyType.Title].Text;
					}
					break;
				}
				if (true)
				{
				}
				return null;
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
				this[BuiltInPropertyType.Title].Text = value;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0001E058 File Offset: 0x0001D058
		// (set) Token: 0x0600034B RID: 843 RVA: 0x0001E0AC File Offset: 0x0001D0AC
		public string Subject
		{
			get
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
					if (this.Contains(BuiltInPropertyType.Subject))
					{
						return this[BuiltInPropertyType.Subject].Text;
					}
					break;
				}
				return null;
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
				this[BuiltInPropertyType.Subject].Text = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0001E0F4 File Offset: 0x0001D0F4
		// (set) Token: 0x0600034D RID: 845 RVA: 0x0001E148 File Offset: 0x0001D148
		public string Author
		{
			get
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
					if (this.Contains(BuiltInPropertyType.Author))
					{
						return this[BuiltInPropertyType.Author].Text;
					}
					break;
				}
				if (true)
				{
				}
				return null;
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
				this[BuiltInPropertyType.Author].Text = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0001E190 File Offset: 0x0001D190
		// (set) Token: 0x0600034F RID: 847 RVA: 0x0001E1E4 File Offset: 0x0001D1E4
		public string Keywords
		{
			get
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
					if (this.Contains(BuiltInPropertyType.Keywords))
					{
						return this[BuiltInPropertyType.Keywords].Text;
					}
					break;
				}
				if (true)
				{
				}
				return null;
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
				this[BuiltInPropertyType.Keywords].Text = value;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0001E22C File Offset: 0x0001D22C
		// (set) Token: 0x06000351 RID: 849 RVA: 0x0001E280 File Offset: 0x0001D280
		public string Comments
		{
			get
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
					if (this.Contains(BuiltInPropertyType.Comments))
					{
						return this[BuiltInPropertyType.Comments].Text;
					}
					break;
				}
				return null;
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
				this[BuiltInPropertyType.Comments].Text = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0001E2C8 File Offset: 0x0001D2C8
		// (set) Token: 0x06000353 RID: 851 RVA: 0x0001E31C File Offset: 0x0001D31C
		public string Template
		{
			get
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
					if (this.Contains(BuiltInPropertyType.Template))
					{
						return this[BuiltInPropertyType.Template].Text;
					}
					break;
				}
				return null;
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
				this[BuiltInPropertyType.Template].Text = value;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0001E364 File Offset: 0x0001D364
		// (set) Token: 0x06000355 RID: 853 RVA: 0x0001E3B8 File Offset: 0x0001D3B8
		public string LastAuthor
		{
			get
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
					if (this.Contains(BuiltInPropertyType.LastAuthor))
					{
						return this[BuiltInPropertyType.LastAuthor].Text;
					}
					break;
				}
				return null;
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
				this[BuiltInPropertyType.LastAuthor].Text = value;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0001E400 File Offset: 0x0001D400
		// (set) Token: 0x06000357 RID: 855 RVA: 0x0001E458 File Offset: 0x0001D458
		public string RevisionNumber
		{
			get
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
					if (this.Contains(BuiltInPropertyType.RevisionNumber))
					{
						return this[BuiltInPropertyType.RevisionNumber].Text;
					}
					break;
				}
				return null;
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
				this[BuiltInPropertyType.RevisionNumber].Text = value;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0001E4A4 File Offset: 0x0001D4A4
		// (set) Token: 0x06000359 RID: 857 RVA: 0x0001E500 File Offset: 0x0001D500
		public TimeSpan EditTime
		{
			get
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
					if (this.Contains(BuiltInPropertyType.EditTime))
					{
						return this[BuiltInPropertyType.EditTime].TimeSpan;
					}
					break;
				}
				return TimeSpan.MinValue;
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
				this[BuiltInPropertyType.EditTime].TimeSpan = value;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0001E54C File Offset: 0x0001D54C
		// (set) Token: 0x0600035B RID: 859 RVA: 0x0001E5A8 File Offset: 0x0001D5A8
		public DateTime LastPrinted
		{
			get
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
					if (this.Contains(BuiltInPropertyType.LastPrinted))
					{
						return this[BuiltInPropertyType.LastPrinted].DateTime;
					}
					break;
				}
				if (true)
				{
				}
				return DateTime.MinValue;
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
				this[BuiltInPropertyType.LastPrinted].DateTime = value;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0001E5F4 File Offset: 0x0001D5F4
		// (set) Token: 0x0600035D RID: 861 RVA: 0x0001E650 File Offset: 0x0001D650
		public DateTime CreatedTime
		{
			get
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
					if (this.Contains(BuiltInPropertyType.CreationDate))
					{
						return this[BuiltInPropertyType.CreationDate].DateTime;
					}
					break;
				}
				if (true)
				{
				}
				return DateTime.MinValue;
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
				this[BuiltInPropertyType.CreationDate].DateTime = value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0001E69C File Offset: 0x0001D69C
		// (set) Token: 0x0600035F RID: 863 RVA: 0x0001E6F8 File Offset: 0x0001D6F8
		public DateTime LastSaveTime
		{
			get
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
					if (this.Contains(BuiltInPropertyType.LastSaveDate))
					{
						if (true)
						{
						}
						return this[BuiltInPropertyType.LastSaveDate].DateTime;
					}
					break;
				}
				return DateTime.MinValue;
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
				this[BuiltInPropertyType.LastSaveDate].DateTime = value;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0001E744 File Offset: 0x0001D744
		// (set) Token: 0x06000361 RID: 865 RVA: 0x0001E7A0 File Offset: 0x0001D7A0
		public int PageCount
		{
			get
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
					if (this.Contains(BuiltInPropertyType.PageCount))
					{
						return this[BuiltInPropertyType.PageCount].Int32;
					}
					break;
				}
				if (true)
				{
				}
				return int.MinValue;
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
				this[BuiltInPropertyType.PageCount].Int32 = value;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0001E7EC File Offset: 0x0001D7EC
		// (set) Token: 0x06000363 RID: 867 RVA: 0x0001E848 File Offset: 0x0001D848
		public int WordCount
		{
			get
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
					if (this.Contains(BuiltInPropertyType.WordCount))
					{
						return this[BuiltInPropertyType.WordCount].Int32;
					}
					break;
				}
				return int.MinValue;
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
				this[BuiltInPropertyType.WordCount].Int32 = value;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0001E894 File Offset: 0x0001D894
		// (set) Token: 0x06000365 RID: 869 RVA: 0x0001E8F0 File Offset: 0x0001D8F0
		public int Characters
		{
			get
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
					if (this.Contains(BuiltInPropertyType.CharCount))
					{
						return this[BuiltInPropertyType.CharCount].Int32;
					}
					break;
				}
				if (true)
				{
				}
				return int.MinValue;
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
				this[BuiltInPropertyType.CharCount].Int32 = value;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0001E93C File Offset: 0x0001D93C
		// (set) Token: 0x06000367 RID: 871 RVA: 0x0001E994 File Offset: 0x0001D994
		public string ApplicationName
		{
			get
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
					if (true)
					{
					}
					if (this.Contains(BuiltInPropertyType.ApplicationName))
					{
						return this[BuiltInPropertyType.ApplicationName].Text;
					}
					break;
				}
				return null;
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
				this[BuiltInPropertyType.ApplicationName].Text = value;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0001E9E0 File Offset: 0x0001D9E0
		// (set) Token: 0x06000369 RID: 873 RVA: 0x0001EA3C File Offset: 0x0001DA3C
		public int Security
		{
			get
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
					if (this.Contains(BuiltInPropertyType.Security))
					{
						return this[BuiltInPropertyType.Security].Int32;
					}
					break;
				}
				return int.MinValue;
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
				this[BuiltInPropertyType.Security].Int32 = value;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0001EA88 File Offset: 0x0001DA88
		// (set) Token: 0x0600036B RID: 875 RVA: 0x0001EAE4 File Offset: 0x0001DAE4
		public string Category
		{
			get
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
					if (this.Contains(BuiltInPropertyType.Category))
					{
						return this[BuiltInPropertyType.Category].Text;
					}
					break;
				}
				return null;
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
				this[BuiltInPropertyType.Category].Text = value;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0001EB30 File Offset: 0x0001DB30
		// (set) Token: 0x0600036D RID: 877 RVA: 0x0001EB8C File Offset: 0x0001DB8C
		public string PresentationTarget
		{
			get
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
					if (this.Contains(BuiltInPropertyType.PresentationTarget))
					{
						if (true)
						{
						}
						return this[BuiltInPropertyType.PresentationTarget].Text;
					}
					break;
				}
				return null;
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
				this[BuiltInPropertyType.PresentationTarget].Text = value;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0001EBD8 File Offset: 0x0001DBD8
		// (set) Token: 0x0600036F RID: 879 RVA: 0x0001EC38 File Offset: 0x0001DC38
		public int Bytes
		{
			get
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
					if (true)
					{
					}
					if (this.Contains(BuiltInPropertyType.ByteCount))
					{
						return this[BuiltInPropertyType.ByteCount].Int32;
					}
					break;
				}
				return int.MinValue;
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
				this[BuiltInPropertyType.ByteCount].Int32 = value;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0001EC84 File Offset: 0x0001DC84
		// (set) Token: 0x06000371 RID: 881 RVA: 0x0001ECE4 File Offset: 0x0001DCE4
		public int LineCount
		{
			get
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
					if (this.Contains(BuiltInPropertyType.LineCount))
					{
						return this[BuiltInPropertyType.LineCount].Int32;
					}
					break;
				}
				if (true)
				{
				}
				return int.MinValue;
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
				this[BuiltInPropertyType.LineCount].Int32 = value;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0001ED30 File Offset: 0x0001DD30
		// (set) Token: 0x06000373 RID: 883 RVA: 0x0001ED90 File Offset: 0x0001DD90
		public int ParagraphCount
		{
			get
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
					if (this.Contains(BuiltInPropertyType.PageCount))
					{
						return this[BuiltInPropertyType.ParagraphCount].Int32;
					}
					break;
				}
				return int.MinValue;
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
				this[BuiltInPropertyType.ParagraphCount].Int32 = value;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0001EDDC File Offset: 0x0001DDDC
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0001EE3C File Offset: 0x0001DE3C
		public int SlideCount
		{
			get
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
					if (this.Contains(BuiltInPropertyType.SlideCount))
					{
						return this[BuiltInPropertyType.SlideCount].Int32;
					}
					break;
				}
				return int.MinValue;
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
				this[BuiltInPropertyType.SlideCount].Int32 = value;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0001EE88 File Offset: 0x0001DE88
		// (set) Token: 0x06000377 RID: 887 RVA: 0x0001EEE8 File Offset: 0x0001DEE8
		public int NoteCount
		{
			get
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
					if (this.Contains(BuiltInPropertyType.NoteCount))
					{
						return this[BuiltInPropertyType.NoteCount].Int32;
					}
					break;
				}
				if (true)
				{
				}
				return int.MinValue;
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
				this[BuiltInPropertyType.NoteCount].Int32 = value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0001EF34 File Offset: 0x0001DF34
		// (set) Token: 0x06000379 RID: 889 RVA: 0x0001EF94 File Offset: 0x0001DF94
		public int HiddenCount
		{
			get
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
					if (true)
					{
					}
					if (this.Contains(BuiltInPropertyType.HiddenCount))
					{
						return this[BuiltInPropertyType.HiddenCount].Int32;
					}
					break;
				}
				return int.MinValue;
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
				this[BuiltInPropertyType.HiddenCount].Int32 = value;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0001EFE0 File Offset: 0x0001DFE0
		// (set) Token: 0x0600037B RID: 891 RVA: 0x0001F040 File Offset: 0x0001E040
		public int MultimediaClipCount
		{
			get
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
					if (this.Contains(BuiltInPropertyType.MultimediaClipCount))
					{
						if (true)
						{
						}
						return this[BuiltInPropertyType.MultimediaClipCount].Int32;
					}
					break;
				}
				return int.MinValue;
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
				this[BuiltInPropertyType.MultimediaClipCount].Int32 = value;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0001F08C File Offset: 0x0001E08C
		// (set) Token: 0x0600037D RID: 893 RVA: 0x0001F0E8 File Offset: 0x0001E0E8
		public bool ScaleCrop
		{
			get
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
					if (true)
					{
					}
					if (this.Contains(BuiltInPropertyType.ScaleCrop))
					{
						return this[BuiltInPropertyType.ScaleCrop].Boolean;
					}
					break;
				}
				return false;
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
				this[BuiltInPropertyType.ScaleCrop].Boolean = value;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0001F134 File Offset: 0x0001E134
		// (set) Token: 0x0600037F RID: 895 RVA: 0x0001F190 File Offset: 0x0001E190
		public string Manager
		{
			get
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
					if (this.Contains(BuiltInPropertyType.Manager))
					{
						return this[BuiltInPropertyType.Manager].Text;
					}
					break;
				}
				return null;
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
				this[BuiltInPropertyType.Manager].Text = value;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0001F1DC File Offset: 0x0001E1DC
		// (set) Token: 0x06000381 RID: 897 RVA: 0x0001F238 File Offset: 0x0001E238
		public string Company
		{
			get
			{
				if (!this.Contains(BuiltInPropertyType.Company))
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3F;
					}
					if (false)
					{
					}
					if (true)
					{
					}
					return null;
				}
				IL_3F:
				return this[BuiltInPropertyType.Company].Text;
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
				this[BuiltInPropertyType.Company].Text = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0001F284 File Offset: 0x0001E284
		// (set) Token: 0x06000383 RID: 899 RVA: 0x0001F2E0 File Offset: 0x0001E2E0
		public bool LinksDirty
		{
			get
			{
				if (!this.Contains(BuiltInPropertyType.LinksDirty))
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3F;
					}
					if (true)
					{
					}
					if (false)
					{
					}
					return false;
				}
				IL_3F:
				return this[BuiltInPropertyType.LinksDirty].Boolean;
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
				this[BuiltInPropertyType.LinksDirty].Boolean = value;
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001F32C File Offset: 0x0001E32C
		protected IDictionary GetDictionary(BuiltInPropertyType propertyId)
		{
			if (true)
			{
			}
			bool flag;
			XlsDocumentProperty.CorrectIndex(propertyId, out flag);
			if (!flag)
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
					return this.ᜃ;
				}
			}
			return this.ᜄ;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001F384 File Offset: 0x0001E384
		protected override void OnClearComplete()
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
			this.ᜃ.Clear();
			this.ᜄ.Clear();
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0001F3D8 File Offset: 0x0001E3D8
		internal new void ᜀ(sprណ A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					List<spr\u22A9> list = A_0.ᜀ();
					int num = 0;
					int count = list.Count;
					int num2 = 9;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							spr\u22A9 spr_u22A;
							XlsBuiltInDocumentProperties.ᜀ(spr_u22A, this.ᜄ, base.InnerList, true, true);
							num2 = 8;
							continue;
						}
						case 1:
							return;
						case 2:
						{
							spr\u22A9 spr_u22A;
							if (spr_u22A.ᜃ() == XlsBuiltInDocumentProperties.ᜁ)
							{
								num2 = 0;
								continue;
							}
							num2 = 4;
							continue;
						}
						case 3:
							goto IL_FD;
						case 4:
						{
							spr\u22A9 spr_u22A;
							if (spr_u22A.ᜃ() == XlsBuiltInDocumentProperties.ᜂ)
							{
								num2 = 7;
								continue;
							}
							goto IL_5D;
						}
						case 5:
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return;
							default:
							{
								if (false)
								{
								}
								spr\u22A9 spr_u22A = list[num];
								num2 = 2;
								continue;
							}
							}
							break;
						case 6:
							goto IL_5D;
						case 7:
						{
							if (true)
							{
							}
							spr\u22A9 spr_u22A;
							XlsBuiltInDocumentProperties.ᜀ(spr_u22A, this.ᜃ, base.InnerList, false, true);
							num2 = 6;
							continue;
						}
						case 8:
							goto IL_5D;
						case 9:
							goto IL_FD;
						}
						break;
						IL_5D:
						num++;
						num2 = 3;
						continue;
						IL_FD:
						num2 = 5;
					}
				}
				return;
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0001F54C File Offset: 0x0001E54C
		internal new static void ᜀ(spr\u22A9 A_0, IDictionary A_1, List<DocumentProperty> A_2, bool A_3, bool A_4)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_219:
				if (A_4)
				{
					goto IL_B5;
				}
				num = 9;
				break;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				switch (0)
				{
				default:
					goto IL_95;
				}
				break;
			}
			Dictionary<int, DocumentProperty> dictionary;
			DocumentProperty documentProperty;
			bool flag;
			object key2;
			for (;;)
			{
				IL_3E:
				int num2;
				int count;
				List<spr\u2129> list;
				object obj;
				switch (num)
				{
				case 0:
					dictionary = new Dictionary<int, DocumentProperty>();
					num = 1;
					continue;
				case 1:
					goto IL_14B;
				case 2:
				{
					if (num2 >= count)
					{
						num = 12;
						continue;
					}
					spr\u2129 spr_u = list[num2];
					num = 3;
					continue;
				}
				case 3:
				{
					spr\u2129 spr_u;
					if (spr_u.ᜂ())
					{
						num = 15;
						continue;
					}
					documentProperty = new DocumentProperty(spr_u, A_3);
					num = 14;
					continue;
				}
				case 4:
					goto IL_18C;
				case 5:
					goto IL_219;
				case 6:
					goto IL_187;
				case 7:
					if (!flag)
					{
						num = 16;
						continue;
					}
					goto IL_139;
				case 8:
					num = 10;
					continue;
				case 9:
				{
					spr\u2129 spr_u;
					dictionary.Add(spr_u.ᜅ(), documentProperty);
					num = 6;
					continue;
				}
				case 10:
					obj = documentProperty.Name;
					goto IL_20B;
				case 11:
					goto IL_139;
				case 12:
					return;
				case 13:
					goto IL_18C;
				case 14:
					if (!A_4)
					{
						num = 8;
						continue;
					}
					num = 17;
					continue;
				case 15:
				{
					spr\u2129 spr_u;
					int key = spr_u.ᜁ();
					DocumentProperty documentProperty2 = dictionary[key];
					documentProperty2.SetLinkSource(spr_u);
					num = 18;
					continue;
				}
				case 16:
					A_2.Add(documentProperty);
					num = 11;
					continue;
				case 17:
					obj = (int)documentProperty.PropertyId;
					goto IL_20B;
				case 18:
					goto IL_139;
				case 19:
					if (!A_4)
					{
						num = 0;
						continue;
					}
					goto IL_14B;
				}
				goto IL_95;
				IL_139:
				num2++;
				num = 13;
				continue;
				IL_14B:
				list = A_0.ᜄ();
				num2 = 0;
				count = list.Count;
				num = 4;
				continue;
				IL_18C:
				num = 2;
				continue;
				IL_20B:
				key2 = obj;
				num = 5;
			}
			IL_187:
			goto IL_B5;
			IL_95:
			dictionary = null;
			num = 19;
			goto IL_3E;
			IL_B5:
			flag = A_1.Contains(key2);
			A_1[key2] = documentProperty;
			num = 7;
			goto IL_3E;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0001F7D0 File Offset: 0x0001E7D0
		internal new static void ᜀ(spr\u22A9 A_0, ICollection A_1)
		{
			switch (0)
			{
			default:
			{
				int num = 2;
				IEnumerator enumerator = A_1.GetEnumerator();
				try
				{
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 1:
							goto IL_95;
						case 3:
						{
							if (!enumerator.MoveNext())
							{
								num2 = 4;
								continue;
							}
							XlsDocumentProperty a_ = (XlsDocumentProperty)enumerator.Current;
							spr\u2129 item = XlsBuiltInDocumentProperties.ᜀ(a_, num);
							A_0.ᜄ().Add(item);
							num++;
							num2 = 0;
							continue;
						}
						case 4:
							num2 = 1;
							continue;
						}
						IL_70:
						num2 = 3;
						continue;
						goto IL_70;
					}
					IL_95:;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_100;
						default:
						{
							if (false)
							{
							}
							int num2 = 0;
							for (;;)
							{
								switch (num2)
								{
								case 0:
									if (disposable != null)
									{
										num2 = 1;
										continue;
									}
									goto IL_102;
								case 1:
									if (true)
									{
									}
									disposable.Dispose();
									num2 = 2;
									continue;
								case 2:
									goto IL_100;
								}
								break;
							}
							break;
						}
						}
					}
					IL_100:
					IL_102:;
				}
				return;
			}
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001F8FC File Offset: 0x0001E8FC
		private new static spr\u2129 ᜀ(XlsDocumentProperty A_0, int A_1)
		{
			spr\u2129 spr_u;
			for (;;)
			{
				spr_u = new spr\u2129();
				A_0.FillPropVariant(spr_u, A_1);
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return spr_u;
				default:
				{
					if (false)
					{
					}
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (A_0.InternalName != null)
							{
								num = 2;
								continue;
							}
							return spr_u;
						case 1:
							return spr_u;
						case 2:
							spr_u.ᜀ(A_0.InternalName);
							num = 1;
							continue;
						}
						break;
					}
					break;
				}
				}
			}
			return spr_u;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0001F98C File Offset: 0x0001E98C
		internal new void ᜀ(sprᮓ A_0)
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
			XlsBuiltInDocumentProperties.ᜀ(A_0, XlsBuiltInDocumentProperties.ᜁ, this.ᜄ.Values);
			XlsBuiltInDocumentProperties.ᜀ(A_0, XlsBuiltInDocumentProperties.ᜂ, this.ᜃ.Values);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001F9F4 File Offset: 0x0001E9F4
		internal new void ᜁ(sprᮓ A_0)
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
			XlsBuiltInDocumentProperties.ᜀ(A_0, XlsBuiltInDocumentProperties.ᜁ, this.ᜄ, base.InnerList, true, true);
			XlsBuiltInDocumentProperties.ᜀ(A_0, XlsBuiltInDocumentProperties.ᜂ, this.ᜃ, base.InnerList, false, true);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001FA64 File Offset: 0x0001EA64
		internal new static void ᜀ(sprᮓ A_0, Guid A_1, ICollection A_2)
		{
			int a_ = 13;
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (A_2 == null)
						{
							num = 1;
							continue;
						}
						spr\u17B9 spr_u17B = null;
						Guid empty = Guid.Empty;
						short num2 = (short)Encoding.Default.CodePage;
						num = 3;
						continue;
					}
					case 1:
						goto IL_258;
					case 2:
						goto IL_5E;
					case 3:
						goto IL_27A;
					}
					if (A_0 == null)
					{
						num = 2;
					}
					else
					{
						num = 0;
					}
				}
				IL_5E:
				throw new ArgumentNullException(RecordTableEnumerator.b("あ⁄㍆᥈㥊≌㽎", a_));
				IL_215:
				throw new ArgumentNullException(RecordTableEnumerator.b("⁂⩄⭆᥈㥊≌㽎㑐⅒⅔㹖㱘⡚", a_));
				IL_258:
				goto IL_215;
				IL_27A:
				goto IL_63;
				try
				{
					try
					{
						for (;;)
						{
							IL_63:
							spr\u17B9 spr_u17B;
							Guid empty;
							A_0.ᜀ(ref A_1, ref empty, 0U, STGM.STGM_READWRITE | STGM.STGM_SHARE_EXCLUSIVE | STGM.STGM_CREATE, out spr_u17B);
							spr\u1D49 spr_u1D = new spr\u1D49();
							try
							{
								spr_u1D.ᜁ(1);
								short num2;
								spr_u1D.ᜀ(num2, PropertyType.Int16);
								spr_u1D.ᜀ(spr_u17B);
								int num3 = 2;
								IEnumerator enumerator = A_2.GetEnumerator();
								try
								{
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_11E;
										case 3:
											num = 0;
											continue;
										case 4:
										{
											if (!enumerator.MoveNext())
											{
												num = 3;
												continue;
											}
											DocumentProperty documentProperty = (DocumentProperty)enumerator.Current;
											documentProperty.ᜀ(spr_u17B, spr_u1D, num3);
											num3++;
											num = 1;
											continue;
										}
										}
										IL_CE:
										num = 4;
										continue;
										goto IL_CE;
									}
									IL_11E:;
								}
								finally
								{
									for (;;)
									{
										IDisposable disposable = enumerator as IDisposable;
										num = 1;
										for (;;)
										{
											switch (num)
											{
											case 0:
												disposable.Dispose();
												num = 2;
												continue;
											case 1:
												if (disposable != null)
												{
													num = 0;
													continue;
												}
												goto IL_168;
											case 2:
												goto IL_166;
											}
											break;
										}
									}
									IL_166:
									IL_168:;
								}
							}
							finally
							{
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 1:
										((IDisposable)spr_u1D).Dispose();
										num = 2;
										continue;
									case 2:
										goto IL_1A6;
									}
									if (spr_u1D == null)
									{
										break;
									}
									num = 1;
								}
								IL_1A6:;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_1BF;
							}
						}
						IL_1BF:
						if (false)
						{
						}
					}
					catch (Exception)
					{
					}
					return;
				}
				finally
				{
					num = 0;
					for (;;)
					{
						spr\u17B9 spr_u17B;
						switch (num)
						{
						case 1:
							spr_u17B.ᜀ(STGC.STGC_DEFAULT);
							Marshal.FinalReleaseComObject(spr_u17B);
							num = 2;
							continue;
						case 2:
							goto IL_212;
						}
						if (spr_u17B == null)
						{
							break;
						}
						num = 1;
					}
					IL_212:;
				}
				goto IL_215;
			}
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001FD54 File Offset: 0x0001ED54
		internal new static void ᜀ(sprᮓ A_0, Guid A_1, IDictionary A_2, IList<DocumentProperty> A_3, bool A_4, bool A_5)
		{
			int a_ = 15;
			switch (0)
			{
			default:
			{
				int num = 4;
				for (;;)
				{
					int num2;
					spr\u17B9 spr_u17B;
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_61;
					case 2:
						goto IL_9D;
					case 3:
						if (num2 != 0)
						{
							num = 0;
							continue;
						}
						goto IL_A2;
					case 4:
						if (true)
						{
						}
						break;
					case 5:
					{
						Dictionary<int, DocumentProperty> dictionary = new Dictionary<int, DocumentProperty>();
						num = 8;
						continue;
					}
					case 6:
					{
						if (A_2 == null)
						{
							num = 2;
							continue;
						}
						spr_u17B = null;
						spr\u1B81 spr_u1B = null;
						Dictionary<int, DocumentProperty> dictionary = null;
						num = 7;
						continue;
					}
					case 7:
						if (!A_5)
						{
							num = 5;
							continue;
						}
						goto IL_396;
					case 8:
						goto IL_396;
					}
					if (A_0 == null)
					{
						num = 1;
						continue;
					}
					num = 6;
					continue;
					IL_396:
					num2 = A_0.ᜀ(ref A_1, STGM.STGM_SHARE_EXCLUSIVE, out spr_u17B);
					num = 3;
				}
				IL_61:
				goto IL_382;
				IL_9D:
				throw new ArgumentNullException(RecordTableEnumerator.b("⅄⹆⩈ᭊ㽌⁎⅐㙒❔⍖じ㹚⹜", a_));
				try
				{
					spr\u17B9 spr_u17B;
					spr\u1B81 spr_u1B;
					try
					{
						IL_A2:
						spr_u17B.ᜀ(out spr_u1B);
						goto IL_2D2;
					}
					catch (COMException)
					{
						if (spr_u17B == null)
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
								goto IL_2D2;
							}
						}
						Marshal.ReleaseComObject(spr_u17B);
						return;
					}
					goto Block_7;
					int num3;
					for (;;)
					{
						IL_2A1:
						switch (num)
						{
						case 0:
							goto IL_309;
						case 1:
							goto IL_2CD;
						case 2:
							if (num3 == 0)
							{
								num = 3;
								continue;
							}
							break;
						case 3:
							num = 0;
							continue;
						}
						goto IL_2B8;
					}
					IL_2CD:
					goto IL_E3;
					IL_309:
					return;
					Block_7:
					spr\u24F0 a_2;
					spr\u1D49 spr_u1D;
					try
					{
						IL_E3:
						num = 3;
						for (;;)
						{
							DocumentProperty documentProperty;
							object obj;
							bool flag;
							switch (num)
							{
							case 0:
								if (!A_5)
								{
									num = 5;
									continue;
								}
								goto IL_195;
							case 1:
								obj = documentProperty.Name;
								goto IL_1E4;
							case 2:
								obj = (int)documentProperty.PropertyId;
								goto IL_1E4;
							case 4:
								goto IL_24E;
							case 5:
							{
								Dictionary<int, DocumentProperty> dictionary;
								dictionary.Add((int)a_2.ᜁ, documentProperty);
								num = 8;
								continue;
							}
							case 6:
								if (!A_5)
								{
									num = 9;
									continue;
								}
								num = 2;
								continue;
							case 7:
							{
								int key = spr_u1D.\u1718();
								Dictionary<int, DocumentProperty> dictionary;
								DocumentProperty documentProperty2 = dictionary[key];
								documentProperty2.SetLinkSource(spr_u1D);
								num = 12;
								continue;
							}
							case 8:
								goto IL_195;
							case 9:
								num = 1;
								continue;
							case 10:
								if (!flag)
								{
									num = 11;
									continue;
								}
								goto IL_24E;
							case 11:
								A_3.Add(documentProperty);
								num = 4;
								continue;
							case 12:
								goto IL_24E;
							case 13:
								goto IL_25A;
							}
							if (spr_u1D.ᜑ())
							{
								num = 7;
								continue;
							}
							documentProperty = new DocumentProperty(spr_u1D, A_4);
							num = 6;
							continue;
							IL_195:
							object key2;
							flag = A_2.Contains(key2);
							A_2[key2] = documentProperty;
							num = 10;
							continue;
							IL_1E4:
							key2 = obj;
							num = 0;
							continue;
							IL_24E:
							num = 13;
						}
						IL_25A:
						goto IL_2D2;
					}
					finally
					{
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								goto IL_29C;
							case 2:
								((IDisposable)spr_u1D).Dispose();
								num = 1;
								continue;
							}
							if (spr_u1D == null)
							{
								break;
							}
							num = 2;
						}
						IL_29C:;
					}
					IL_2B8:
					spr_u1D = new spr\u1D49(a_2, spr_u17B, A_5);
					num = 1;
					goto IL_2A1;
					IL_2D2:
					num3 = 0;
					a_2 = default(spr\u24F0);
					spr_u1B.ᜀ(1, ref a_2, out num3);
					num = 2;
					goto IL_2A1;
				}
				finally
				{
					num = 2;
					for (;;)
					{
						spr\u17B9 spr_u17B;
						switch (num)
						{
						case 0:
							goto IL_368;
						case 1:
						{
							spr\u1B81 spr_u1B;
							Marshal.FinalReleaseComObject(spr_u1B);
							num = 0;
							continue;
						}
						case 3:
						{
							spr\u1B81 spr_u1B;
							if (spr_u1B != null)
							{
								num = 1;
								continue;
							}
							goto IL_381;
						}
						case 4:
							Marshal.FinalReleaseComObject(spr_u17B);
							num = 5;
							continue;
						case 5:
							goto IL_36A;
						}
						if (spr_u17B != null)
						{
							num = 4;
							continue;
						}
						IL_36A:
						num = 3;
					}
					IL_368:
					IL_381:;
				}
				IL_382:
				throw new ArgumentNullException(RecordTableEnumerator.b("㙄≆㵈ᭊ㽌⁎⅐", a_));
			}
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x000201B0 File Offset: 0x0001F1B0
		protected override void OnInsertComplete(int index, DocumentProperty value)
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
			base.OnInsertComplete(index, value);
			BuiltInPropertyType propertyId = value.PropertyId;
			IDictionary dictionary = this.GetDictionary(propertyId);
			int num = (int)propertyId;
			dictionary.Add(num, value);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00020214 File Offset: 0x0001F214
		internal new void ᜀ(spr\u22A9 A_0, spr\u22A9 A_1)
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
			XlsBuiltInDocumentProperties.ᜀ(A_0, this.ᜄ.Values);
			XlsBuiltInDocumentProperties.ᜀ(A_1, this.ᜃ.Values);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00020274 File Offset: 0x0001F274
		// Note: this type is marked as 'beforefieldinit'.
		static XlsBuiltInDocumentProperties()
		{
			int a_ = 16;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			XlsBuiltInDocumentProperties.ᜁ = new Guid(RecordTableEnumerator.b("E穇獉ੋ癍敏ᝑ摓筕汗᱙ᩛ杝䵟卡呣健偧䝩⵫Ɑ䥯䍱女䙵䁷䩹䱻䱽쉿낁뎃쒅뮇캉떋", a_));
			XlsBuiltInDocumentProperties.ᜂ = new Guid(RecordTableEnumerator.b("Ʌ絇ॉࡋ੍敏扑晓筕橗Ὑ敛ᵝ䵟卡呣坥⩧䝩啫嵭䥯䕱女䙵䁷䩹䱻䱽쉿낁잃삅놇쮉즋", a_));
		}

		// Token: 0x04000094 RID: 148
		private new const STGM ᜀ = STGM.STGM_READWRITE | STGM.STGM_SHARE_EXCLUSIVE | STGM.STGM_CREATE;

		// Token: 0x04000095 RID: 149
		internal new static readonly Guid ᜁ;

		// Token: 0x04000096 RID: 150
		private bool \u2593\u009B\u0089\u008B;

		// Token: 0x04000097 RID: 151
		private float[] \u25D9\u0097\u008E\u00A1;

		// Token: 0x04000098 RID: 152
		internal new static readonly Guid ᜂ;

		// Token: 0x04000099 RID: 153
		private Dictionary<int, XlsDocumentProperty> ᜃ = new Dictionary<int, XlsDocumentProperty>();

		// Token: 0x0400009A RID: 154
		private bool \u25D8\u00A3\u008A\u009D;

		// Token: 0x0400009B RID: 155
		private Dictionary<int, XlsDocumentProperty> ᜄ = new Dictionary<int, XlsDocumentProperty>();
	}
}
