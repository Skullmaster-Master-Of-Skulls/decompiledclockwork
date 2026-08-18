using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using Spire.CompoundFile.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Interface;

namespace Spire.Doc.Reporting
{
	// Token: 0x02000102 RID: 258
	public class MailMerge
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00049CB0 File Offset: 0x00048CB0
		// (set) Token: 0x060006D4 RID: 1748 RVA: 0x00049CF4 File Offset: 0x00048CF4
		public bool ClearFields
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
				return this.ᜅ;
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
				this.ᜅ = value;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00049D38 File Offset: 0x00048D38
		protected Document Document
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

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x00049D7C File Offset: 0x00048D7C
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x00049DC0 File Offset: 0x00048DC0
		public bool HideEmptyParagraphs
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
				return this.ᜈ;
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
				this.ᜈ = value;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00049E04 File Offset: 0x00048E04
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x00049E48 File Offset: 0x00048E48
		public bool HideEmptyGroup
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
				return this.ᜉ;
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
				this.ᜉ = value;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x00049E8C File Offset: 0x00048E8C
		private Dictionary<string, IRowsEnumerator> NestedEnums
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜌ = new Dictionary<string, IRowsEnumerator>();
						num = 2;
						continue;
					case 2:
						goto IL_6F;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					if (false)
					{
					}
					if (this.ᜌ != null)
					{
						break;
					}
					if (true)
					{
					}
					num = 0;
				}
				IL_6F:
				return this.ᜌ;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x00049F10 File Offset: 0x00048F10
		private DataSet CurrentDataSet
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
						this.ᜋ = new DataSet();
						num = 0;
						continue;
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
						if (this.ᜋ != null)
						{
							goto IL_71;
						}
						num = 1;
						break;
					}
				}
				IL_6F:
				IL_71:
				return this.ᜋ;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x00049F94 File Offset: 0x00048F94
		private Regex VariableCommandRegex
		{
			get
			{
				int a_ = 13;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.\u170D = new Regex(ClipboardData.b("噲嵴ⱶ❸奺塼≾ꪀꪂꂄ", a_));
						if (true)
						{
						}
						num = 2;
						continue;
					case 2:
						goto IL_86;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					if (false)
					{
					}
					if (this.\u170D != null)
					{
						break;
					}
					num = 0;
				}
				IL_86:
				return this.\u170D;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0004A030 File Offset: 0x00049030
		private Stack<MailMerge.ᜁ> GroupSelectors
		{
			get
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
						this.ᜎ = new Stack<MailMerge.ᜁ>();
						num = 2;
						continue;
					case 2:
						goto IL_6F;
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
						if (this.ᜎ != null)
						{
							goto IL_71;
						}
						num = 1;
						break;
					}
				}
				IL_6F:
				IL_71:
				return this.ᜎ;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0004A0B4 File Offset: 0x000490B4
		public Dictionary<string, string> MappedFields
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
						this.ᜑ = new Dictionary<string, string>();
						num = 0;
						continue;
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
						if (this.ᜑ != null)
						{
							goto IL_71;
						}
						num = 1;
						break;
					}
				}
				IL_6F:
				IL_71:
				return this.ᜑ;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0004A138 File Offset: 0x00049138
		private Dictionary<string, bool> ClearFieldsState
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
						this.\u1718 = new Dictionary<string, bool>();
						num = 1;
						continue;
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
						if (this.\u1718 != null)
						{
							goto IL_71;
						}
						num = 2;
						break;
					}
				}
				IL_6F:
				IL_71:
				return this.\u1718;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0004A1BC File Offset: 0x000491BC
		private MailMergeDataSet CurrentDataSetDocIO
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 2:
						this.\u1716 = new MailMergeDataSet();
						num = 0;
						continue;
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
						if (this.\u1716 != null)
						{
							goto IL_71;
						}
						num = 2;
						break;
					}
				}
				IL_6F:
				IL_71:
				return this.\u1716;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060006E1 RID: 1761 RVA: 0x0004A240 File Offset: 0x00049240
		// (remove) Token: 0x060006E2 RID: 1762 RVA: 0x0004A2D8 File Offset: 0x000492D8
		public event MergeFieldEventHandler MergeField
		{
			add
			{
				for (;;)
				{
					MergeFieldEventHandler mergeFieldEventHandler = this.\u1719;
					int num = 0;
					for (;;)
					{
						MergeFieldEventHandler mergeFieldEventHandler2;
						switch (num)
						{
						case 0:
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
								goto IL_49;
							}
							break;
						case 1:
							return;
						case 2:
							if (mergeFieldEventHandler == mergeFieldEventHandler2)
							{
								num = 1;
								continue;
							}
							goto IL_49;
						}
						break;
						IL_49:
						mergeFieldEventHandler2 = mergeFieldEventHandler;
						MergeFieldEventHandler value2 = (MergeFieldEventHandler)Delegate.Combine(mergeFieldEventHandler2, value);
						mergeFieldEventHandler = Interlocked.CompareExchange<MergeFieldEventHandler>(ref this.\u1719, value2, mergeFieldEventHandler2);
						num = 2;
					}
				}
			}
			remove
			{
				if (true)
				{
				}
				for (;;)
				{
					MergeFieldEventHandler mergeFieldEventHandler = this.\u1719;
					int num = 1;
					for (;;)
					{
						MergeFieldEventHandler mergeFieldEventHandler2;
						switch (num)
						{
						case 0:
							if (mergeFieldEventHandler == mergeFieldEventHandler2)
							{
								num = 2;
								continue;
							}
							goto IL_49;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								goto IL_49;
							}
							break;
						case 2:
							return;
						}
						break;
						IL_49:
						mergeFieldEventHandler2 = mergeFieldEventHandler;
						MergeFieldEventHandler value2 = (MergeFieldEventHandler)Delegate.Remove(mergeFieldEventHandler2, value);
						mergeFieldEventHandler = Interlocked.CompareExchange<MergeFieldEventHandler>(ref this.\u1719, value2, mergeFieldEventHandler2);
						num = 0;
					}
				}
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060006E3 RID: 1763 RVA: 0x0004A36C File Offset: 0x0004936C
		// (remove) Token: 0x060006E4 RID: 1764 RVA: 0x0004A404 File Offset: 0x00049404
		public event MergeImageFieldEventHandler MergeImageField
		{
			add
			{
				for (;;)
				{
					MergeImageFieldEventHandler mergeImageFieldEventHandler = this.\u171A;
					int num = 2;
					for (;;)
					{
						MergeImageFieldEventHandler mergeImageFieldEventHandler2;
						switch (num)
						{
						case 0:
							return;
						case 1:
							if (mergeImageFieldEventHandler == mergeImageFieldEventHandler2)
							{
								if (true)
								{
								}
								num = 0;
								continue;
							}
							goto IL_41;
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
								goto IL_41;
							}
							break;
						}
						break;
						IL_41:
						mergeImageFieldEventHandler2 = mergeImageFieldEventHandler;
						MergeImageFieldEventHandler value2 = (MergeImageFieldEventHandler)Delegate.Combine(mergeImageFieldEventHandler2, value);
						mergeImageFieldEventHandler = Interlocked.CompareExchange<MergeImageFieldEventHandler>(ref this.\u171A, value2, mergeImageFieldEventHandler2);
						num = 1;
					}
				}
			}
			remove
			{
				for (;;)
				{
					MergeImageFieldEventHandler mergeImageFieldEventHandler = this.\u171A;
					int num = 2;
					for (;;)
					{
						MergeImageFieldEventHandler mergeImageFieldEventHandler2;
						switch (num)
						{
						case 0:
							if (mergeImageFieldEventHandler == mergeImageFieldEventHandler2)
							{
								num = 1;
								continue;
							}
							goto IL_49;
						case 1:
							return;
						case 2:
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
								goto IL_49;
							}
							break;
						}
						break;
						IL_49:
						mergeImageFieldEventHandler2 = mergeImageFieldEventHandler;
						MergeImageFieldEventHandler value2 = (MergeImageFieldEventHandler)Delegate.Remove(mergeImageFieldEventHandler2, value);
						mergeImageFieldEventHandler = Interlocked.CompareExchange<MergeImageFieldEventHandler>(ref this.\u171A, value2, mergeImageFieldEventHandler2);
						num = 0;
					}
				}
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060006E5 RID: 1765 RVA: 0x0004A49C File Offset: 0x0004949C
		// (remove) Token: 0x060006E6 RID: 1766 RVA: 0x0004A530 File Offset: 0x00049530
		public event MergeGroupEventHandler MergeGroup
		{
			add
			{
				for (;;)
				{
					MergeGroupEventHandler mergeGroupEventHandler = this.\u171B;
					int num = 0;
					for (;;)
					{
						MergeGroupEventHandler mergeGroupEventHandler2;
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								goto IL_41;
							}
							break;
						case 1:
							goto IL_72;
						case 2:
							if (mergeGroupEventHandler == mergeGroupEventHandler2)
							{
								num = 1;
								continue;
							}
							goto IL_41;
						}
						break;
						IL_41:
						mergeGroupEventHandler2 = mergeGroupEventHandler;
						MergeGroupEventHandler value2 = (MergeGroupEventHandler)Delegate.Combine(mergeGroupEventHandler2, value);
						mergeGroupEventHandler = Interlocked.CompareExchange<MergeGroupEventHandler>(ref this.\u171B, value2, mergeGroupEventHandler2);
						num = 2;
					}
				}
				IL_72:
				if (true)
				{
				}
			}
			remove
			{
				for (;;)
				{
					if (true)
					{
					}
					MergeGroupEventHandler mergeGroupEventHandler = this.\u171B;
					int num = 2;
					for (;;)
					{
						MergeGroupEventHandler mergeGroupEventHandler2;
						switch (num)
						{
						case 0:
							return;
						case 1:
							if (mergeGroupEventHandler == mergeGroupEventHandler2)
							{
								num = 0;
								continue;
							}
							goto IL_49;
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
								goto IL_49;
							}
							break;
						}
						break;
						IL_49:
						mergeGroupEventHandler2 = mergeGroupEventHandler;
						MergeGroupEventHandler value2 = (MergeGroupEventHandler)Delegate.Remove(mergeGroupEventHandler2, value);
						mergeGroupEventHandler = Interlocked.CompareExchange<MergeGroupEventHandler>(ref this.\u171B, value2, mergeGroupEventHandler2);
						num = 1;
					}
				}
			}
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0004A5C8 File Offset: 0x000495C8
		internal MailMerge(Document A_0)
		{
			this.ᜀ = A_0;
			this.ᜂ = new SectionCollection();
			this.ᜁ = new MailMerge.ᜁ(new MailMerge.ᜁ.ᜀ(this.ᜅ));
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0004A60C File Offset: 0x0004960C
		public void Execute(string[] fieldNames, string[] fieldValues)
		{
			int a_ = 12;
			for (;;)
			{
				this.Document.ᜈ = true;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_EF;
					case 1:
						if (fieldNames == null)
						{
							num = 8;
							continue;
						}
						num = 5;
						continue;
					case 2:
						goto IL_ED;
					case 3:
						if (this.ᜃ.Length > 0)
						{
							num = 4;
							continue;
						}
						goto IL_15E;
					case 4:
					{
						int num2 = 0;
						int count = this.Document.Sections.Count;
						num = 7;
						continue;
					}
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_EF;
						default:
							if (false)
							{
							}
							if (fieldValues == null)
							{
								if (true)
								{
								}
								num = 2;
								continue;
							}
							this.ᜃ = fieldNames;
							this.ᜄ = fieldValues;
							num = 3;
							continue;
						}
						break;
					case 6:
					{
						int num2;
						int count;
						if (num2 >= count)
						{
							num = 9;
							continue;
						}
						ISection a_2 = this.Document.Sections[num2];
						this.ᜀ(a_2, null);
						num2++;
						num = 0;
						continue;
					}
					case 7:
						goto IL_EF;
					case 8:
						goto IL_58;
					case 9:
						goto IL_109;
					}
					break;
					IL_EF:
					num = 6;
				}
			}
			IL_58:
			throw new ArgumentNullException(ClipboardData.b("ᑱᵳ፵ᑷṹ㉻ώ", a_));
			IL_ED:
			throw new ArgumentNullException(ClipboardData.b("ᑱᵳ፵ᑷṹ⩻ώ", a_));
			IL_109:
			IL_15E:
			this.Document.ᜈ = false;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0004A790 File Offset: 0x00049790
		public void Execute(DataRow row)
		{
			int a_ = 15;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_50;
			}
			if (false)
			{
			}
			if (row == null)
			{
				if (true)
				{
				}
				throw new ArgumentNullException(ClipboardData.b("ݴᡶ๸", a_));
			}
			IL_50:
			this.ᜁ(new spr\u1977(row));
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0004A7FC File Offset: 0x000497FC
		public void Execute(IEnumerable dataSource)
		{
			int a_ = 8;
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
				if (dataSource == null)
				{
					throw new ArgumentNullException(ClipboardData.b("੭ᅯٱᕳյ᝷ཹ๻ᵽ", a_));
				}
				break;
			}
			MailMergeDataTable dataSource2 = new MailMergeDataTable(string.Empty, dataSource);
			this.ExecuteGroup(dataSource2);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0004A86C File Offset: 0x0004986C
		public void Execute(DataTable table)
		{
			int a_ = 15;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_50;
			}
			if (false)
			{
			}
			if (table == null)
			{
				if (true)
				{
				}
				throw new ArgumentNullException(ClipboardData.b("Ŵᙶ᭸᝺᡼", a_));
			}
			IL_50:
			this.ᜁ(new spr\u1977(table));
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0004A8D8 File Offset: 0x000498D8
		public void Execute(DataView dataView)
		{
			int a_ = 9;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_48;
			}
			if (false)
			{
			}
			if (dataView == null)
			{
				throw new ArgumentNullException(ClipboardData.b("୮ၰݲᑴⅶၸṺ੼", a_));
			}
			IL_48:
			if (true)
			{
			}
			this.ᜁ(new sprᲯ(dataView));
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0004A944 File Offset: 0x00049944
		public void Execute(OleDbDataReader dataReader)
		{
			int a_ = 5;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_48;
			}
			if (false)
			{
			}
			if (dataReader == null)
			{
				throw new ArgumentNullException(ClipboardData.b("ཪ౬᭮ၰⅲၴᙶᵸṺོ", a_));
			}
			IL_48:
			if (true)
			{
			}
			this.ᜁ(new spr\u22E0(dataReader));
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0004A9B0 File Offset: 0x000499B0
		public void Execute(IDataReader dataReader)
		{
			int a_ = 17;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_50;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			if (dataReader == null)
			{
				throw new ArgumentNullException(ClipboardData.b("፶ᡸེᱼ⵾ﮈ", a_));
			}
			IL_50:
			this.ᜁ(new spr\u22E0(dataReader));
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0004AA1C File Offset: 0x00049A1C
		public void ExecuteWidthRegion(DataTable table)
		{
			int a_ = 4;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_50;
			}
			if (false)
			{
			}
			if (table == null)
			{
				if (true)
				{
				}
				throw new ArgumentNullException(ClipboardData.b("ṩ൫౭ᱯ᝱", a_));
			}
			IL_50:
			this.ᜂ(new spr\u1977(table));
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0004AA88 File Offset: 0x00049A88
		public void ExecuteWidthRegion(DataView dataView)
		{
			int a_ = 7;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_50;
			}
			if (false)
			{
			}
			if (dataView == null)
			{
				if (true)
				{
				}
				throw new ArgumentNullException(ClipboardData.b("६๮հቲ⍴Ṷᱸ౺", a_));
			}
			IL_50:
			this.ᜂ(new sprᲯ(dataView));
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0004AAF4 File Offset: 0x00049AF4
		public void ExecuteWidthRegion(IDataReader dataReader)
		{
			int a_ = 1;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_50;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			if (dataReader == null)
			{
				throw new ArgumentNullException(ClipboardData.b("ͦࡨὪ౬㵮ᑰቲᅴቶ୸", a_));
			}
			IL_50:
			this.ᜂ(new spr\u22E0(dataReader));
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0004AB60 File Offset: 0x00049B60
		public void ExecuteWidthNestedRegion(MailMergeDataSet dataSource, List<DictionaryEntry> filters)
		{
			int a_ = 11;
			int num = 5;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					this.ᜌ.Clear();
					this.ᜌ = null;
					num = 6;
					continue;
				case 1:
					this.\u1713.Clear();
					this.\u1713 = null;
					num = 4;
					continue;
				case 2:
					goto IL_100;
				case 3:
					if (this.\u1713 != null)
					{
						num = 1;
						continue;
					}
					goto IL_1E8;
				case 4:
					goto IL_1A2;
				case 6:
					goto IL_B4;
				case 7:
					num = 9;
					continue;
				case 8:
					goto IL_1D5;
				case 9:
					if (dataSource.DataSet.Count == 0)
					{
						num = 2;
						continue;
					}
					num = 8;
					continue;
				case 10:
					if (this.ᜌ != null)
					{
						num = 0;
						continue;
					}
					goto IL_B4;
				case 11:
				{
					if (filters.Count == 0)
					{
						num = 12;
						continue;
					}
					this.ᜀ();
					this.\u1714 = dataSource;
					this.\u1715 = filters;
					DictionaryEntry dictionaryEntry = filters[0];
					this.Document.ᜈ = true;
					this.ᜐ = true;
					this.ᜇ((string)dictionaryEntry.Key);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1D5;
					default:
						if (false)
						{
						}
						num = 10;
						continue;
					}
					break;
				}
				case 12:
					goto IL_1C5;
				case 13:
					num = 11;
					continue;
				}
				if (dataSource != null)
				{
					num = 7;
					continue;
				}
				goto IL_96;
				IL_B4:
				num = 3;
				continue;
				IL_1D5:
				if (filters == null)
				{
					break;
				}
				num = 13;
			}
			IL_82:
			throw new ArgumentException(ClipboardData.b("ተᱲᡴ᩶ᡸᕺ᥼౾ꆀﶈꮊﲎ놐", a_));
			IL_96:
			throw new ArgumentException(ClipboardData.b("ᕰቲŴᙶ⩸Ṻॼ彾ꖄﮊ歷", a_));
			IL_100:
			goto IL_96;
			IL_1A2:
			goto IL_1E8;
			IL_1C5:
			goto IL_82;
			IL_1E8:
			this.Document.ᜈ = false;
			this.ᜐ = false;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0004AD68 File Offset: 0x00049D68
		public void ExecuteWidthNestedRegion(DbConnection conn, List<DictionaryEntry> commands)
		{
			int a_ = 10;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_DC;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DC;
					default:
						if (false)
						{
						}
						if (this.ᜋ != null)
						{
							num = 9;
							continue;
						}
						goto IL_184;
					}
					break;
				case 3:
					goto IL_59;
				case 4:
					goto IL_F7;
				case 5:
					goto IL_134;
				case 6:
					goto IL_11E;
				case 7:
				{
					if (commands == null)
					{
						num = 4;
						continue;
					}
					this.ᜀ();
					this.ᜊ = conn;
					this.ᜏ = commands;
					DictionaryEntry dictionaryEntry = commands[0];
					this.Document.ᜈ = true;
					this.ᜐ = true;
					this.ᜇ((string)dictionaryEntry.Key);
					num = 8;
					continue;
				}
				case 8:
					if (this.ᜌ != null)
					{
						num = 0;
						continue;
					}
					goto IL_134;
				case 9:
					this.ᜋ.Clear();
					this.ᜋ = null;
					if (true)
					{
					}
					num = 6;
					continue;
				}
				if (conn == null)
				{
					num = 3;
					continue;
				}
				num = 7;
				continue;
				IL_DC:
				this.ᜌ.Clear();
				this.ᜌ = null;
				num = 5;
				continue;
				IL_134:
				num = 2;
			}
			IL_59:
			throw new ArgumentException(ClipboardData.b("፯ᵱᩳᡵ", a_));
			IL_F7:
			throw new ArgumentException(ClipboardData.b("፯ᵱᥳ᭵᥷ᑹ᡻ൽ", a_));
			IL_11E:
			IL_184:
			this.Document.ᜈ = false;
			this.ᜐ = false;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0004AF0C File Offset: 0x00049F0C
		public void ExecuteWidthNestedRegion(DbConnection conn, List<DictionaryEntry> commands, bool isSqlConnection)
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
			this.\u1712 = isSqlConnection;
			this.ExecuteWidthNestedRegion(conn, commands);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0004AF58 File Offset: 0x00049F58
		public void ExecuteWidthNestedRegion(DataSet dataSet, List<DictionaryEntry> commands)
		{
			int a_ = 11;
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_13C;
				case 1:
					goto IL_126;
				case 2:
					if (this.ᜌ != null)
					{
						num = 6;
						continue;
					}
					goto IL_13C;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E1;
					default:
						if (false)
						{
						}
						if (this.\u1713 != null)
						{
							num = 5;
							continue;
						}
						goto IL_18C;
					}
					break;
				case 4:
					goto IL_59;
				case 5:
					this.\u1713.Clear();
					this.\u1713 = null;
					num = 1;
					continue;
				case 6:
					goto IL_E1;
				case 7:
					goto IL_107;
				case 8:
				{
					if (true)
					{
					}
					if (commands == null)
					{
						num = 7;
						continue;
					}
					this.ᜀ();
					this.\u1713 = dataSet.Copy();
					this.ᜏ = commands;
					DictionaryEntry dictionaryEntry = commands[0];
					this.Document.ᜈ = true;
					this.ᜐ = true;
					this.ᜇ((string)dictionaryEntry.Key);
					num = 2;
					continue;
				}
				}
				if (dataSet == null)
				{
					num = 4;
					continue;
				}
				num = 8;
				continue;
				IL_E1:
				this.ᜌ.Clear();
				this.ᜌ = null;
				num = 0;
				continue;
				IL_13C:
				num = 3;
			}
			IL_59:
			throw new ArgumentException(ClipboardData.b("ᕰቲŴᙶ⩸Ṻॼ", a_));
			IL_107:
			throw new ArgumentException(ClipboardData.b("ተᱲᡴ᩶ᡸᕺ᥼౾", a_));
			IL_126:
			IL_18C:
			this.Document.ᜈ = false;
			this.ᜐ = false;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0004B104 File Offset: 0x0004A104
		public string[] GetMergeFieldNames()
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
			List<string> list = new List<string>();
			this.ᜀ(list, null);
			return list.ToArray();
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0004B154 File Offset: 0x0004A154
		public string[] GetMergeFieldNames(string groupName)
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
			List<string> list = new List<string>();
			this.ᜀ(list, groupName);
			return list.ToArray();
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0004B1A4 File Offset: 0x0004A1A4
		public string[] GetMergeGroupNames()
		{
			switch (0)
			{
			default:
			{
				List<string> list;
				for (;;)
				{
					list = new List<string>();
					Stack<EntityEntry> stack = new Stack<EntityEntry>();
					stack.Push(new EntityEntry(this.Document));
					int num = 4;
					for (;;)
					{
						EntityEntry entityEntry;
						MergeField mergeField;
						switch (num)
						{
						case 0:
							goto IL_15F;
						case 1:
							goto IL_1F5;
						case 2:
							if (entityEntry.Current != null)
							{
								num = 16;
								continue;
							}
							goto IL_15F;
						case 3:
							if (stack.Count != 0)
							{
								num = 21;
								continue;
							}
							goto IL_1D1;
						case 4:
							goto IL_21F;
						case 5:
							if (entityEntry.Current.IsComposite)
							{
								num = 13;
								continue;
							}
							goto IL_1FA;
						case 6:
							if (true)
							{
							}
							num = 5;
							continue;
						case 7:
							if (MailMerge.ᜂ(mergeField))
							{
								num = 11;
								continue;
							}
							goto IL_15F;
						case 8:
						{
							ICompositeObject compositeObject;
							if (compositeObject.ChildObjects.Count > 0)
							{
								num = 9;
								continue;
							}
							goto IL_1FA;
						}
						case 9:
						{
							ICompositeObject compositeObject;
							stack.Push(new EntityEntry(compositeObject.ChildObjects[0]));
							num = 15;
							continue;
						}
						case 10:
							goto IL_1D1;
						case 11:
							list.Add(mergeField.FieldName);
							num = 17;
							continue;
						case 12:
							if (entityEntry.Current.DocumentObjectType == DocumentObjectType.MergeField)
							{
								num = 18;
								continue;
							}
							goto IL_15F;
						case 13:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_28D;
							default:
							{
								if (false)
								{
								}
								ICompositeObject compositeObject = entityEntry.Current as ICompositeObject;
								num = 8;
								continue;
							}
							}
							break;
						case 14:
							if (entityEntry.Fetch())
							{
								num = 10;
								continue;
							}
							stack.Pop();
							num = 3;
							continue;
						case 15:
							goto IL_1D1;
						case 16:
							num = 12;
							continue;
						case 17:
							goto IL_15F;
						case 18:
							goto IL_28D;
						case 19:
							if (stack.Count <= 0)
							{
								num = 1;
								continue;
							}
							goto IL_21F;
						case 20:
							if (entityEntry.Current != null)
							{
								num = 6;
								continue;
							}
							goto IL_1FA;
						case 21:
							entityEntry = stack.Peek();
							num = 0;
							continue;
						}
						break;
						IL_15F:
						num = 14;
						continue;
						IL_1D1:
						num = 19;
						continue;
						IL_1FA:
						num = 2;
						continue;
						IL_21F:
						entityEntry = stack.Peek();
						num = 20;
						continue;
						IL_28D:
						mergeField = (entityEntry.Current as MergeField);
						num = 7;
					}
				}
				IL_1F5:
				return list.ToArray();
			}
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0004B474 File Offset: 0x0004A474
		private string[] ᜂ()
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				List<string> list;
				for (;;)
				{
					list = new List<string>();
					Stack<IEnumerator> stack = new Stack<IEnumerator>();
					stack.Push(this.Document.ChildObjects.GetEnumerator());
					int num = 21;
					for (;;)
					{
						IEnumerator enumerator;
						switch (num)
						{
						case 0:
							enumerator = stack.Peek();
							num = 3;
							continue;
						case 1:
							if (enumerator.MoveNext())
							{
								num = 17;
								continue;
							}
							goto IL_2BE;
						case 2:
							goto IL_137;
						case 3:
							goto IL_137;
						case 4:
							if (enumerator.MoveNext())
							{
								num = 14;
								continue;
							}
							stack.Pop();
							num = 9;
							continue;
						case 5:
						{
							DocumentObject documentObject;
							if (documentObject != null)
							{
								num = 19;
								continue;
							}
							goto IL_137;
						}
						case 6:
						{
							MergeField mergeField;
							if (MailMerge.ᜂ(mergeField))
							{
								num = 22;
								continue;
							}
							goto IL_137;
						}
						case 7:
						{
							DocumentObject documentObject;
							MergeField mergeField = documentObject as MergeField;
							num = 6;
							continue;
						}
						case 8:
							if (stack.Count <= 0)
							{
								num = 23;
								continue;
							}
							goto IL_292;
						case 9:
							if (stack.Count != 0)
							{
								goto IL_D0;
							}
							goto IL_1CF;
						case 10:
						{
							DocumentObject documentObject;
							if (documentObject.DocumentObjectType == DocumentObjectType.MergeField)
							{
								num = 7;
								continue;
							}
							goto IL_137;
						}
						case 11:
						{
							ICompositeObject compositeObject;
							stack.Push(compositeObject.ChildObjects.GetEnumerator());
							num = 20;
							continue;
						}
						case 12:
						{
							ICompositeObject compositeObject;
							if (compositeObject != null)
							{
								num = 15;
								continue;
							}
							goto IL_2BE;
						}
						case 13:
						{
							ICompositeObject compositeObject;
							if (compositeObject.ChildObjects.Count > 0)
							{
								num = 11;
								continue;
							}
							goto IL_2BE;
						}
						case 14:
							goto IL_1CF;
						case 15:
							num = 13;
							continue;
						case 16:
						{
							DocumentObject documentObject = enumerator.Current as DocumentObject;
							num = 5;
							continue;
						}
						case 17:
						{
							ICompositeObject compositeObject = enumerator.Current as ICompositeObject;
							num = 12;
							continue;
						}
						case 18:
							if (enumerator.Current != null)
							{
								num = 16;
								continue;
							}
							goto IL_137;
						case 19:
							num = 10;
							continue;
						case 20:
							goto IL_1CF;
						case 21:
							goto IL_292;
						case 22:
						{
							MergeField mergeField;
							list.Add(mergeField.FieldName);
							num = 2;
							continue;
						}
						case 23:
							goto IL_1F3;
						}
						break;
						IL_D0:
						num = 0;
						continue;
						IL_137:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D0;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						IL_1CF:
						num = 8;
						continue;
						IL_292:
						enumerator = stack.Peek();
						num = 1;
						continue;
						IL_2BE:
						num = 18;
					}
				}
				IL_1F3:
				return list.ToArray();
			}
			}
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0004B770 File Offset: 0x0004A770
		private void ᜀ(List<string> A_0, string A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					Section section = null;
					int num = 0;
					int count = this.Document.Sections.Count;
					int num2;
					int num3;
					int count2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_199:
						if (num >= count)
						{
							num2 = 11;
						}
						else
						{
							section = this.Document.Sections[num];
							num3 = 0;
							count2 = section.Body.Items.Count;
							num2 = 13;
						}
						break;
					default:
						if (false)
						{
						}
						num2 = 12;
						break;
					}
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_1FE;
						case 1:
						{
							int num4 = 0;
							num2 = 15;
							continue;
						}
						case 2:
						{
							int num4;
							num4++;
							num2 = 3;
							continue;
						}
						case 3:
							goto IL_16E;
						case 4:
							goto IL_18D;
						case 5:
						{
							if (num3 >= count2)
							{
								num2 = 1;
								continue;
							}
							if (true)
							{
							}
							BodyRegion a_ = section.Body.Items[num3];
							this.ᜀ(A_0, a_, A_1);
							num3++;
							num2 = 8;
							continue;
						}
						case 6:
						{
							int num4;
							if (num4 >= 6)
							{
								num2 = 7;
								continue;
							}
							int num5 = 0;
							int count3 = section.HeadersFooters[num4].Items.Count;
							num2 = 0;
							continue;
						}
						case 7:
							num++;
							num2 = 4;
							continue;
						case 8:
							goto IL_1DB;
						case 9:
							goto IL_199;
						case 10:
						{
							int num5;
							int count3;
							if (num5 >= count3)
							{
								num2 = 2;
								continue;
							}
							int num4;
							BodyRegion a_ = section.HeadersFooters[num4].Items[num5];
							this.ᜀ(A_0, a_, A_1);
							num5++;
							num2 = 14;
							continue;
						}
						case 11:
							return;
						case 12:
							goto IL_18D;
						case 13:
							goto IL_1DB;
						case 14:
							goto IL_1FE;
						case 15:
							goto IL_16E;
						}
						break;
						IL_16E:
						num2 = 6;
						continue;
						IL_18D:
						num2 = 9;
						continue;
						IL_1DB:
						num2 = 5;
						continue;
						IL_1FE:
						num2 = 10;
					}
				}
				return;
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0004B9C0 File Offset: 0x0004A9C0
		private void ᜅ(IRowsEnumerator A_0)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_11A:
				num = 8;
				break;
			default:
				if (false)
				{
				}
				switch (0)
				{
				default:
					goto IL_EE;
				}
				break;
			}
			MailMerge.ᜁ ᜁ;
			bool flag;
			for (;;)
			{
				IL_2F:
				int num2;
				switch (num)
				{
				case 0:
					if (!this.ᜀ(A_0))
					{
						num = 44;
						continue;
					}
					goto IL_251;
				case 1:
					this.ᜃ(ᜁ);
					num = 45;
					continue;
				case 2:
					goto IL_289;
				case 3:
				{
					Body body;
					if (body.Items[num2] is Paragraph)
					{
						num = 43;
						continue;
					}
					num = 34;
					continue;
				}
				case 4:
					this.ᜁ.ᜂ().FieldName = string.Empty;
					this.ᜁ.ᜆ().FieldName = string.Empty;
					num = 13;
					continue;
				case 5:
				{
					int num3;
					if (num2 > num3)
					{
						num = 19;
						continue;
					}
					num = 3;
					continue;
				}
				case 6:
					if (this.HideEmptyGroup)
					{
						num = 16;
						continue;
					}
					goto IL_1D4;
				case 7:
				{
					int num4;
					int count;
					if (num4 >= count)
					{
						num = 12;
						continue;
					}
					Table table;
					this.ᜁ(table.Rows[num4]);
					num4++;
					num = 39;
					continue;
				}
				case 8:
					if (ᜁ.ᜄ() != null)
					{
						num = 30;
						continue;
					}
					goto IL_151;
				case 9:
					goto IL_3F5;
				case 10:
					if (this.HideEmptyGroup)
					{
						num = 1;
						continue;
					}
					goto IL_129;
				case 11:
				{
					Body body;
					Table table = body.Items[num2] as Table;
					int num4 = 0;
					int count = table.Rows.Count;
					num = 9;
					continue;
				}
				case 12:
					goto IL_28E;
				case 13:
					goto IL_4B1;
				case 14:
					goto IL_1D4;
				case 15:
					if (this.ᜅ)
					{
						num = 24;
						continue;
					}
					goto IL_151;
				case 16:
					this.ᜃ(ᜁ);
					num = 14;
					continue;
				case 17:
					num = 10;
					continue;
				case 18:
					if (A_0.RowsCount == 0)
					{
						num = 17;
						continue;
					}
					goto IL_129;
				case 19:
					flag = true;
					num = 18;
					continue;
				case 20:
					if (ᜁ.ᜄ().TextBody.Owner != null)
					{
						num = 25;
						continue;
					}
					goto IL_151;
				case 21:
					goto IL_129;
				case 22:
				{
					int num5 = ᜁ.ᜂ().Owner.ឯ();
					int num3 = ᜁ.ᜆ().Owner.ឯ();
					Body body = ᜁ.ᜂ().Owner.Owner as Body;
					num2 = num5;
					num = 42;
					continue;
				}
				case 23:
					this.ᜃ(A_0);
					num = 2;
					continue;
				case 24:
					goto IL_11A;
				case 25:
					num = 36;
					continue;
				case 26:
					if (A_0.RowsCount == 0)
					{
						num = 28;
						continue;
					}
					goto IL_1D4;
				case 27:
					goto IL_28E;
				case 28:
					num = 6;
					continue;
				case 29:
					goto IL_274;
				case 30:
					if (true)
					{
					}
					num = 20;
					continue;
				case 31:
					if (A_0.RowsCount == 0)
					{
						num = 22;
						continue;
					}
					goto IL_151;
				case 32:
					if (ᜁ.ᜄ() != null)
					{
						num = 29;
						continue;
					}
					num = 37;
					continue;
				case 33:
					if (this.ᜐ)
					{
						num = 4;
						continue;
					}
					goto IL_4B1;
				case 34:
				{
					Body body;
					if (body.Items[num2] is Table)
					{
						num = 11;
						continue;
					}
					goto IL_28E;
				}
				case 35:
					goto IL_3D0;
				case 36:
					if (ᜁ.ᜄ().TextBody.Owner is Section)
					{
						num = 38;
						continue;
					}
					goto IL_151;
				case 37:
					if (ᜁ.ᜅ() != null)
					{
						num = 23;
						continue;
					}
					return;
				case 38:
					num = 31;
					continue;
				case 39:
					goto IL_3F5;
				case 40:
					num = 0;
					continue;
				case 41:
					if (!flag)
					{
						num = 40;
						continue;
					}
					goto IL_251;
				case 42:
					goto IL_3D0;
				case 43:
				{
					Body body;
					this.ᜂ(body.Items[num2] as Paragraph);
					num = 27;
					continue;
				}
				case 44:
					return;
				case 45:
					goto IL_129;
				}
				goto IL_EE;
				IL_129:
				num = 33;
				continue;
				IL_151:
				num = 26;
				continue;
				IL_1D4:
				this.ᜀ(ᜁ.ᜂ(), true);
				this.ᜀ(ᜁ.ᜆ(), true);
				num = 21;
				continue;
				IL_251:
				num = 32;
				continue;
				IL_28E:
				num2++;
				num = 35;
				continue;
				IL_3D0:
				num = 5;
				continue;
				IL_3F5:
				num = 7;
				continue;
				IL_4B1:
				num = 41;
			}
			IL_274:
			this.ᜄ(A_0);
			return;
			IL_289:
			return;
			IL_EE:
			flag = false;
			ᜁ = this.ᜁ;
			num = 15;
			goto IL_2F;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0004BF68 File Offset: 0x0004AF68
		private void ᜃ(MailMerge.ᜁ A_0)
		{
			int a_ = 18;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_1A7;
				case 1:
					num = 9;
					continue;
				case 3:
					return;
				case 4:
					goto IL_A7;
				case 5:
					if (A_0.ᜅ() != null)
					{
						num = 11;
						continue;
					}
					return;
				case 6:
					this.ᜁ(A_0);
					num = 4;
					continue;
				case 7:
					A_0.ᜄ().ItemEndIndex = A_0.ᜄ().ItemStartIndex;
					num = 8;
					continue;
				case 8:
					goto IL_15B;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1A7;
					default:
						if (false)
						{
						}
						if (A_0.ᜂ().Prefix == ClipboardData.b("ⱷ᭹ṻች톁慎ﺉ", a_))
						{
							num = 6;
							continue;
						}
						this.ᜀ(A_0);
						num = 0;
						continue;
					}
					break;
				case 10:
					if (A_0.ᜄ() != null)
					{
						num = 7;
						continue;
					}
					goto IL_15B;
				case 11:
					A_0.ᜅ().ᜂ = A_0.ᜅ().ᜁ;
					num = 3;
					continue;
				case 12:
					goto IL_A7;
				}
				if (A_0.ᜂ().OwnerParagraph.IsInCell)
				{
					if (true)
					{
					}
					num = 1;
					continue;
				}
				this.ᜂ(A_0);
				num = 12;
				continue;
				IL_A7:
				num = 10;
				continue;
				IL_1A7:
				goto IL_A7;
				IL_15B:
				num = 5;
			}
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0004C124 File Offset: 0x0004B124
		private void ᜂ(MailMerge.ᜁ A_0)
		{
			switch (0)
			{
			default:
			{
				int num;
				int num2;
				int num3;
				Body body;
				for (;;)
				{
					num = A_0.ᜂ().ឯ();
					num2 = A_0.ᜂ().OwnerParagraph.ឯ();
					num3 = A_0.ᜆ().ឯ();
					int num4 = A_0.ᜆ().OwnerParagraph.ឯ();
					body = (A_0.ᜂ().OwnerParagraph.Owner as Body);
					int num5 = 3;
					for (;;)
					{
						switch (num5)
						{
						case 0:
							goto IL_DF;
						case 1:
							this.ᜀ(body.Items[num2 + 1] as Paragraph, 0, num3 + 1);
							num5 = 11;
							continue;
						case 2:
							goto IL_12C;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1B1;
							default:
								if (false)
								{
								}
								if (num2 == num4)
								{
									num5 = 0;
									continue;
								}
								num5 = 6;
								continue;
							}
							break;
						case 4:
							this.ᜀ(body.Items[num2] as Paragraph, num, (body.Items[num2] as Paragraph).Items.Count);
							this.ᜀ(body, num2 + 1, num4);
							num5 = 9;
							continue;
						case 5:
							if (num > 0)
							{
								num5 = 4;
								continue;
							}
							goto IL_E4;
						case 6:
							if ((body.Items[num2] as Paragraph).Items.Count > 1)
							{
								num5 = 12;
								continue;
							}
							goto IL_E4;
						case 7:
							goto IL_32A;
						case 8:
							if (num3 == (body.Items[num2 + 1] as Paragraph).Items.Count - 1)
							{
								num5 = 2;
								continue;
							}
							goto IL_1B1;
						case 9:
							if (num3 == (body.Items[num2 + 1] as Paragraph).Items.Count - 1)
							{
								num5 = 7;
								continue;
							}
							num5 = 14;
							continue;
						case 10:
							if ((body.Items[num2 + 1] as Paragraph).Items.Count > 0)
							{
								num5 = 1;
								continue;
							}
							return;
						case 11:
							goto IL_24C;
						case 12:
							num5 = 5;
							continue;
						case 13:
							goto IL_16D;
						case 14:
							if ((body.Items[num2 + 1] as Paragraph).Items.Count > 0)
							{
								num5 = 13;
								continue;
							}
							return;
						}
						break;
						IL_E4:
						this.ᜀ(body, num2, num4);
						num5 = 8;
						continue;
						IL_1B1:
						if (true)
						{
						}
						num5 = 10;
					}
				}
				IL_DF:
				this.ᜀ(body.Items[num2] as Paragraph, num, num3);
				return;
				IL_12C:
				body.Items.RemoveAt(num2 + 1);
				return;
				IL_16D:
				this.ᜀ(body.Items[num2 + 1] as Paragraph, 0, num3 + 1);
				return;
				IL_24C:
				return;
				IL_32A:
				body.Items.RemoveAt(num2 + 1);
				return;
			}
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0004C460 File Offset: 0x0004B460
		private void ᜁ(MailMerge.ᜁ A_0)
		{
			switch (0)
			{
			default:
			{
				int num2;
				int cellIndex;
				int cellIndex2;
				int rowIndex;
				int rowIndex2;
				int num4;
				Table table;
				for (;;)
				{
					for (;;)
					{
						int num = A_0.ᜂ().ឯ();
						num2 = A_0.ᜆ().ឯ();
						int num3 = A_0.ᜂ().OwnerParagraph.ឯ();
						cellIndex = (A_0.ᜂ().Owner.Owner as TableCell).GetCellIndex();
						cellIndex2 = (A_0.ᜆ().Owner.Owner as TableCell).GetCellIndex();
						rowIndex = (A_0.ᜂ().Owner.Owner.Owner as TableRow).GetRowIndex();
						rowIndex2 = (A_0.ᜆ().Owner.Owner.Owner as TableRow).GetRowIndex();
						num4 = A_0.ᜆ().OwnerParagraph.ឯ();
						table = (A_0.ᜂ().Owner.Owner.Owner.Owner as Table);
						this.ᜁ(A_0, table, num, num3, cellIndex, cellIndex2, rowIndex, rowIndex2);
						int num5 = 3;
						for (;;)
						{
							switch (num5)
							{
							case 0:
								num5 = 4;
								continue;
							case 1:
								goto IL_14B;
							case 2:
								num5 = 9;
								continue;
							case 3:
								if (rowIndex != rowIndex2)
								{
									num5 = 1;
									continue;
								}
								num5 = 7;
								continue;
							case 4:
								if (num2 == (table.Rows[rowIndex].Cells[cellIndex].Items[num4] as Paragraph).Items.Count - 1)
								{
									num5 = 6;
									continue;
								}
								goto IL_224;
							case 5:
								return;
							case 6:
								goto IL_1A4;
							case 7:
								if (num == 0)
								{
									if (true)
									{
									}
									num5 = 2;
									continue;
								}
								return;
							case 8:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									num5 = 10;
									continue;
								}
								break;
							case 9:
								if (num3 == 0)
								{
									num5 = 8;
									continue;
								}
								return;
							case 10:
								if (num4 == table.Rows[rowIndex].Cells[cellIndex].Items.Count - 1)
								{
									num5 = 0;
									continue;
								}
								goto IL_224;
							}
							break;
							IL_224:
							this.ᜀ(table.Rows[rowIndex].Cells[cellIndex].Items[num4], 0, num2);
							this.ᜀ(table.Rows[rowIndex].Cells[cellIndex], 0, num4);
							num5 = 5;
						}
					}
				}
				IL_14B:
				this.ᜀ(A_0, table, num2, num4, cellIndex, cellIndex2, rowIndex, rowIndex2);
				return;
				IL_1A4:
				table.Rows.RemoveAt(rowIndex);
				return;
			}
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0004C750 File Offset: 0x0004B750
		private void ᜁ(MailMerge.ᜁ A_0, Table A_1, int A_2, int A_3, int A_4, int A_5, int A_6, int A_7)
		{
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_3FD;
				case 1:
					goto IL_2D1;
				case 2:
					goto IL_2B9;
				case 3:
					if (A_6 == A_7)
					{
						num = 24;
						continue;
					}
					this.ᜀ(A_1.Rows[A_6], A_4 + 1, A_1.Rows[A_6].Cells.Count);
					num = 2;
					continue;
				case 4:
					if (A_6 != A_7)
					{
						if (true)
						{
						}
						num = 13;
						continue;
					}
					num = 11;
					continue;
				case 5:
					goto IL_E6;
				case 6:
					if (A_6 != A_7)
					{
						num = 10;
						continue;
					}
					num = 15;
					continue;
				case 7:
					goto IL_2B9;
				case 8:
					if (A_4 == A_1.Rows[A_6].Cells.Count - 1)
					{
						num = 19;
						continue;
					}
					this.ᜀ(A_1.Rows[A_6], A_4, A_1.Rows[A_6].Cells.Count - 1);
					num = 12;
					continue;
				case 10:
					goto IL_1E0;
				case 11:
					if (A_4 == A_5)
					{
						num = 0;
						continue;
					}
					goto IL_1E5;
				case 12:
					goto IL_A1;
				case 13:
					num = 8;
					continue;
				case 14:
					if (A_4 == 0)
					{
						num = 22;
						continue;
					}
					num = 4;
					continue;
				case 15:
					if (A_4 == A_5)
					{
						num = 5;
						continue;
					}
					goto IL_349;
				case 16:
					goto IL_A1;
				case 17:
					num = 14;
					continue;
				case 18:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_EB;
					default:
						if (false)
						{
						}
						if (A_3 == 0)
						{
							num = 17;
							continue;
						}
						goto IL_399;
					}
					break;
				case 19:
					A_1.Rows[A_6].Cells.RemoveAt(A_4);
					num = 16;
					continue;
				case 20:
					num = 18;
					continue;
				case 21:
					if (A_6 + 1 < A_7)
					{
						num = 23;
						continue;
					}
					return;
				case 22:
					num = 6;
					continue;
				case 23:
					goto IL_C2;
				case 24:
					this.ᜀ(A_1.Rows[A_6], A_4 + 1, A_5);
					num = 7;
					continue;
				}
				if (A_2 == 0)
				{
					num = 20;
					continue;
				}
				goto IL_EB;
				IL_A1:
				num = 21;
				continue;
				IL_EB:
				this.ᜀ(A_1.Rows[A_6].Cells[A_4].Items[A_3] as Paragraph, A_2, (A_1.Rows[A_6].Cells[A_4].Items[A_3] as Paragraph).Items.Count);
				this.ᜀ(A_1.Rows[A_6].Cells[A_4], A_3 + 1, A_1.Rows[A_6].Cells[A_4].Items.Count);
				num = 3;
				continue;
				IL_2B9:
				this.ᜀ(A_1, A_6 + 1, A_7);
				num = 1;
			}
			IL_C2:
			this.ᜀ(A_1, A_6 + 1, A_7);
			return;
			IL_E6:
			A_1.Rows[A_6].Cells.RemoveAt(A_4);
			return;
			IL_1E0:
			this.ᜀ(A_1, A_6, A_7);
			return;
			IL_1E5:
			this.ᜀ(A_1.Rows[A_6], A_4, A_5);
			return;
			IL_2D1:
			return;
			IL_349:
			this.ᜀ(A_1.Rows[A_6], A_4, A_5);
			return;
			IL_399:
			this.ᜀ(A_1.Rows[A_6].Cells[A_4], A_3, A_1.Rows[A_6].Cells[A_4].Items.Count);
			return;
			IL_3FD:
			A_1.Rows[A_6].Cells.RemoveAt(A_4);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0004CBB8 File Offset: 0x0004BBB8
		private void ᜀ(MailMerge.ᜁ A_0, Table A_1, int A_2, int A_3, int A_4, int A_5, int A_6, int A_7)
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_5 == A_1.Rows[A_6 + 1].Cells.Count - 1)
					{
						num = 1;
						continue;
					}
					goto IL_4D;
				case 1:
					goto IL_9A;
				case 2:
					if (A_3 == A_1.Rows[A_6 + 1].Cells[A_5].Items.Count - 1)
					{
						num = 7;
						continue;
					}
					goto IL_119;
				case 3:
					if (true)
					{
					}
					num = 6;
					continue;
				case 4:
					num = 2;
					continue;
				case 6:
					if (A_2 != (A_1.Rows[A_6 + 1].Cells[A_5].Items[A_3] as Paragraph).Items.Count - 1)
					{
						goto IL_1AE;
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
						num = 4;
						continue;
					}
					break;
				case 7:
					num = 0;
					continue;
				}
				if (A_6 == A_7)
				{
					goto IL_1AE;
				}
				num = 3;
			}
			IL_4D:
			this.ᜀ(A_1.Rows[A_6 + 1], 0, A_5 + 1);
			return;
			IL_9A:
			A_1.Rows.RemoveAt(A_6 + 1);
			return;
			IL_119:
			this.ᜀ(A_1.Rows[A_6 + 1].Cells[A_5], 0, A_3 + 1);
			this.ᜀ(A_1.Rows[A_6 + 1], 0, A_5);
			return;
			IL_1AE:
			this.ᜀ(A_1.Rows[A_6 + 1].Cells[A_5].Items[A_3] as Paragraph, 0, A_2 + 1);
			this.ᜀ(A_1.Rows[A_6 + 1].Cells[A_5], 0, A_3);
			this.ᜀ(A_1.Rows[A_6 + 1], 0, A_5);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0004CDE8 File Offset: 0x0004BDE8
		private void ᜀ(MailMerge.ᜁ A_0)
		{
			switch (0)
			{
			default:
			{
				int num2;
				int num3;
				TableCell tableCell;
				for (;;)
				{
					int num = A_0.ᜂ().ឯ();
					num2 = A_0.ᜂ().OwnerParagraph.ឯ();
					num3 = A_0.ᜆ().ឯ();
					int num4 = A_0.ᜆ().OwnerParagraph.ឯ();
					tableCell = (A_0.ᜂ().OwnerParagraph.Owner as TableCell);
					int num5 = 8;
					for (;;)
					{
						switch (num5)
						{
						case 0:
							if (num2 != num4)
							{
								num5 = 17;
								continue;
							}
							num5 = 4;
							continue;
						case 1:
							goto IL_26E;
						case 2:
							goto IL_2F2;
						case 3:
							if (num3 == (tableCell.Items[num2 + 1] as Paragraph).Items.Count - 1)
							{
								num5 = 10;
								continue;
							}
							num5 = 14;
							continue;
						case 4:
							if (true)
							{
							}
							if (num3 == (tableCell.Items[num2] as Paragraph).Items.Count - 1)
							{
								num5 = 7;
								continue;
							}
							num5 = 13;
							continue;
						case 5:
							if (num3 == (tableCell.Items[num2 + 1] as Paragraph).Items.Count - 1)
							{
								num5 = 9;
								continue;
							}
							num5 = 11;
							continue;
						case 6:
							num5 = 12;
							continue;
						case 7:
							goto IL_241;
						case 8:
							if ((tableCell.Items[num2] as Paragraph).Items.Count > 1)
							{
								num5 = 6;
								continue;
							}
							goto IL_3B8;
						case 9:
							goto IL_1F8;
						case 10:
							goto IL_2B1;
						case 11:
							if ((tableCell.Items[num2 + 1] as Paragraph).Items.Count > 0)
							{
								num5 = 2;
								continue;
							}
							return;
						case 12:
							if (num > 0)
							{
								num5 = 18;
								continue;
							}
							goto IL_3B8;
						case 13:
							if ((tableCell.Items[num2] as Paragraph).Items.Count > 0)
							{
								num5 = 16;
								continue;
							}
							return;
						case 14:
							if ((tableCell.Items[num2 + 1] as Paragraph).Items.Count > 0)
							{
								num5 = 15;
								continue;
							}
							return;
						case 15:
							goto IL_372;
						case 16:
							this.ᜀ(tableCell.Items[num2] as Paragraph, 0, num3 + 1);
							num5 = 1;
							continue;
						case 17:
							num5 = 3;
							continue;
						case 18:
							this.ᜀ(tableCell.Items[num2] as Paragraph, num, (tableCell.Items[num2] as Paragraph).Items.Count);
							this.ᜀ(tableCell, num2 + 1, num4);
							num5 = 5;
							continue;
						}
						break;
						IL_3B8:
						this.ᜀ(tableCell, num2, num4);
						num5 = 0;
					}
				}
				IL_13F:
				tableCell.Items.RemoveAt(num2 + 1);
				return;
				IL_1F8:
				goto IL_13F;
				IL_241:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_13F;
				default:
					if (false)
					{
					}
					tableCell.Items.RemoveAt(num2);
					return;
				}
				IL_26E:
				return;
				IL_2B1:
				tableCell.Items.RemoveAt(num2 + 1);
				return;
				IL_2F2:
				this.ᜀ(tableCell.Items[num2 + 1] as Paragraph, 0, num3);
				return;
				IL_372:
				this.ᜀ(tableCell.Items[num2 + 1] as Paragraph, 0, (tableCell.Items[num2 + 1] as Paragraph).Items.Count + 1);
				return;
			}
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0004D1E8 File Offset: 0x0004C1E8
		private void ᜀ(DocumentObject A_0, int A_1, int A_2)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					DocumentObjectType documentObjectType = A_0.DocumentObjectType;
					int num = 15;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_F4;
						case 1:
							num = 9;
							continue;
						case 2:
						{
							int num2;
							if (num2 >= A_2)
							{
								num = 19;
								continue;
							}
							(A_0 as TableRow).Cells.RemoveAt(A_1);
							num2++;
							num = 18;
							continue;
						}
						case 3:
							goto IL_19E;
						case 4:
							goto IL_26B;
						case 5:
							goto IL_28C;
						case 6:
							goto IL_28C;
						case 7:
							if (true)
							{
							}
							goto IL_19E;
						case 8:
							goto IL_1BB;
						case 9:
							switch (documentObjectType)
							{
							case DocumentObjectType.Table:
							{
								int num3 = A_1;
								num = 6;
								continue;
							}
							case DocumentObjectType.TableRow:
							{
								int num2 = A_1;
								num = 14;
								continue;
							}
							case DocumentObjectType.TableCell:
							{
								int num4 = A_1;
								num = 4;
								continue;
							}
							default:
								num = 12;
								continue;
							}
							break;
						case 10:
						{
							int num4;
							if (num4 >= A_2)
							{
								num = 17;
								continue;
							}
							(A_0 as TableCell).Items.RemoveAt(A_1);
							num4++;
							num = 23;
							continue;
						}
						case 11:
							return;
						case 12:
							return;
						case 13:
						{
							int num3;
							if (num3 >= A_2)
							{
								num = 16;
								continue;
							}
							(A_0 as Table).Rows.RemoveAt(A_1);
							num3++;
							num = 5;
							continue;
						}
						case 14:
							goto IL_2B3;
						case 15:
							switch (documentObjectType)
							{
							case DocumentObjectType.Body:
							{
								int num5 = A_1;
								num = 3;
								continue;
							}
							case DocumentObjectType.HeaderFooter:
								return;
							case DocumentObjectType.Paragraph:
							{
								int num6 = A_1;
								num = 20;
								continue;
							}
							default:
								num = 1;
								continue;
							}
							break;
						case 16:
							return;
						case 17:
							return;
						case 18:
							goto IL_2B3;
						case 19:
							return;
						case 20:
							goto IL_F4;
						case 21:
						{
							int num6;
							if (num6 < A_2)
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
									(A_0 as Paragraph).Items.RemoveAt(A_1);
									num6++;
									num = 0;
									continue;
								}
							}
							num = 11;
							continue;
						}
						case 22:
						{
							int num5;
							if (num5 >= A_2)
							{
								num = 8;
								continue;
							}
							(A_0 as Body).Items.RemoveAt(A_1);
							num5++;
							num = 7;
							continue;
						}
						case 23:
							goto IL_26B;
						}
						break;
						IL_F4:
						num = 21;
						continue;
						IL_19E:
						num = 22;
						continue;
						IL_26B:
						num = 10;
						continue;
						IL_28C:
						num = 13;
						continue;
						IL_2B3:
						num = 2;
					}
				}
				return;
				IL_1BB:
				return;
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0004D4CC File Offset: 0x0004C4CC
		private void ᜄ(IRowsEnumerator A_0)
		{
			switch (0)
			{
			default:
			{
				MailMerge.ᜁ ᜁ;
				for (;;)
				{
					ᜁ = this.ᜁ;
					TextBodyPart textBodyPart = new TextBodyPart();
					TextBodySelection textBodySelection = ᜁ.ᜄ();
					textBodyPart.Copy(textBodySelection);
					A_0.Reset();
					int num = 2;
					for (;;)
					{
						int a_;
						switch (num)
						{
						case 0:
							goto IL_238;
						case 1:
							if (textBodyPart.BodyItems[textBodyPart.BodyItems.Count - 1] is Paragraph)
							{
								num = 6;
								continue;
							}
							goto IL_238;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_12E;
							default:
								if (false)
								{
								}
								goto IL_1E2;
							}
							break;
						case 3:
							goto IL_1E2;
						case 4:
							goto IL_205;
						case 5:
							if (this.\u1713 == null)
							{
								num = 15;
								continue;
							}
							goto IL_21D;
						case 6:
							a_ = (textBodyPart.BodyItems[textBodyPart.BodyItems.Count - 1] as Paragraph).Items.Count - 1;
							num = 0;
							continue;
						case 7:
							if (true)
							{
							}
							if (textBodyPart.BodyItems.Count > 0)
							{
								num = 10;
								continue;
							}
							goto IL_238;
						case 8:
							goto IL_21D;
						case 9:
							num = 5;
							continue;
						case 10:
							num = 1;
							continue;
						case 11:
							num = 17;
							continue;
						case 12:
							if (this.\u1714 != null)
							{
								num = 8;
								continue;
							}
							goto IL_298;
						case 13:
							if (textBodySelection.ItemStartIndex == textBodySelection.ItemEndIndex)
							{
								num = 19;
								continue;
							}
							textBodyPart.PasteAt(textBodySelection.TextBody, textBodySelection.ItemEndIndex, textBodySelection.ParagraphItemEndIndex);
							num = 14;
							continue;
						case 14:
							goto IL_CF;
						case 15:
							num = 12;
							continue;
						case 16:
							goto IL_12E;
						case 17:
							if (this.ᜐ)
							{
								num = 21;
								continue;
							}
							return;
						case 18:
							if (this.ᜊ == null)
							{
								num = 9;
								continue;
							}
							goto IL_21D;
						case 19:
							textBodyPart.PasteAt(textBodySelection.TextBody, textBodySelection.ItemEndIndex, textBodySelection.ParagraphItemEndIndex + 1);
							num = 16;
							continue;
						case 20:
							if (A_0.IsLast)
							{
								num = 11;
								continue;
							}
							num = 13;
							continue;
						case 21:
							goto IL_368;
						case 22:
							goto IL_298;
						case 23:
							if (!A_0.NextRow())
							{
								num = 4;
								continue;
							}
							num = 18;
							continue;
						}
						break;
						IL_CF:
						a_ = 0;
						num = 7;
						continue;
						IL_12E:
						goto IL_CF;
						IL_1E2:
						num = 23;
						continue;
						IL_21D:
						this.ᜀ(ᜁ.ᜈ(), A_0);
						num = 22;
						continue;
						IL_238:
						textBodySelection.ᜀ(textBodyPart.BodyItems.Count - 1, a_);
						num = 3;
						continue;
						IL_298:
						int count = textBodySelection.TextBody.Items.Count;
						this.ᜀ(textBodySelection.TextBody, textBodySelection.ItemStartIndex, textBodySelection.ItemEndIndex, textBodySelection.ParagraphItemStartIndex, textBodySelection.ParagraphItemEndIndex, A_0);
						this.\u1717++;
						textBodySelection.ItemEndIndex += textBodySelection.TextBody.Items.Count - count;
						num = 20;
					}
				}
				IL_205:
				return;
				IL_368:
				this.NestedEnums.Remove(ᜁ.ᜈ());
				return;
			}
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0004D874 File Offset: 0x0004C874
		private void ᜃ(IRowsEnumerator A_0)
		{
			switch (0)
			{
			default:
			{
				MailMerge.ᜁ ᜁ;
				int num3;
				for (;;)
				{
					ᜁ = this.ᜁ;
					Table table = ᜁ.ᜅ().ᜀ;
					int num = ᜁ.ᜅ().ᜁ;
					int num2 = ᜁ.ᜅ().ᜂ;
					int count = table.Rows.Count;
					num3 = num;
					int num4 = 0;
					int num5 = 5;
					for (;;)
					{
						int num7;
						TableRow[] array;
						int num8;
						int num9;
						switch (num5)
						{
						case 0:
							goto IL_272;
						case 1:
							goto IL_197;
						case 2:
							goto IL_272;
						case 3:
							A_0.Reset();
							num5 = 21;
							continue;
						case 4:
						{
							int num6;
							if (num6 >= num7)
							{
								num5 = 11;
								continue;
							}
							table.Rows.Insert(num3 + num6, array[num6].Clone());
							num6++;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_354;
							default:
								if (false)
								{
								}
								num5 = 0;
								continue;
							}
							break;
						}
						case 5:
							if (this.ᜐ)
							{
								num5 = 15;
								continue;
							}
							goto IL_197;
						case 6:
							this.NestedEnums.Remove(ᜁ.ᜈ());
							num5 = 9;
							continue;
						case 7:
							goto IL_107;
						case 8:
							num5 = 22;
							continue;
						case 9:
							goto IL_102;
						case 10:
							if (true)
							{
							}
							goto IL_125;
						case 11:
							goto IL_1EE;
						case 12:
							num5 = 17;
							continue;
						case 13:
							if (this.ᜐ)
							{
								num5 = 6;
								continue;
							}
							goto IL_37C;
						case 14:
							goto IL_211;
						case 15:
							this.ᜁ(num, num2, table);
							num5 = 1;
							continue;
						case 16:
							if (this.ᜊ == null)
							{
								num5 = 8;
								continue;
							}
							goto IL_107;
						case 17:
							if (this.\u1714 != null)
							{
								num5 = 7;
								continue;
							}
							goto IL_338;
						case 18:
							goto IL_338;
						case 19:
							if (!A_0.NextRow())
							{
								num5 = 14;
								continue;
							}
							num5 = 16;
							continue;
						case 20:
							if (num8 > num2)
							{
								num5 = 3;
								continue;
							}
							array[num9] = table.Rows[num8].Clone();
							num9++;
							num8++;
							num5 = 24;
							continue;
						case 21:
							goto IL_1EE;
						case 22:
							if (this.\u1713 == null)
							{
								num5 = 12;
								continue;
							}
							goto IL_107;
						case 23:
						{
							if (A_0.IsLast)
							{
								num5 = 25;
								continue;
							}
							num3 += num4;
							int num6 = 0;
							num5 = 2;
							continue;
						}
						case 24:
							goto IL_125;
						case 25:
							num5 = 13;
							continue;
						}
						break;
						IL_107:
						this.ᜀ(ᜁ.ᜈ(), A_0);
						num5 = 18;
						continue;
						IL_125:
						num5 = 20;
						continue;
						IL_197:
						num7 = num2 - num + 1;
						array = new TableRow[num7];
						num9 = 0;
						num8 = num;
						num5 = 10;
						continue;
						IL_1EE:
						num5 = 19;
						continue;
						IL_272:
						num5 = 4;
						continue;
						IL_354:
						num5 = 23;
						continue;
						IL_338:
						num4 = this.ᜀ(table, num3, num7, A_0);
						this.\u1717++;
						goto IL_354;
					}
				}
				IL_102:
				IL_211:
				IL_37C:
				ᜁ.ᜅ().ᜁ = num3;
				return;
			}
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0004DC0C File Offset: 0x0004CC0C
		private void ᜂ(IRowsEnumerator A_0)
		{
			for (;;)
			{
				this.Document.ᜈ = true;
				this.ᜀ();
				int num = 0;
				int count = this.Document.Sections.Count;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_5D;
					case 1:
						goto IL_49;
					case 2:
						goto IL_49;
					case 3:
						if (num >= count)
						{
							num2 = 0;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
						{
							if (false)
							{
							}
							if (true)
							{
							}
							Section a_ = this.Document.Sections[num];
							this.ᜀ(a_, A_0);
							num++;
							num2 = 2;
							continue;
						}
						}
						break;
					}
					break;
					IL_49:
					num2 = 3;
				}
			}
			IL_5D:
			this.ᜁ();
			this.Document.ᜈ = false;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0004DCE4 File Offset: 0x0004CCE4
		private void ᜀ(Section A_0, IRowsEnumerator A_1)
		{
			for (;;)
			{
				this.ᜁ.ᜀ(A_0.Body, A_1);
				int num = 0;
				int num2 = 6;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						Body body;
						if (body.Items.Count > 0)
						{
							num2 = 5;
							continue;
						}
						goto IL_42;
					}
					case 1:
					{
						if (num >= 6)
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
								num2 = 3;
								continue;
							}
						}
						Body body = A_0.HeadersFooters[num];
						num2 = 0;
						continue;
					}
					case 2:
						goto IL_42;
					case 3:
						return;
					case 4:
						goto IL_A4;
					case 5:
					{
						Body body;
						this.ᜁ.ᜀ(body, A_1);
						num2 = 2;
						continue;
					}
					case 6:
						goto IL_A4;
					}
					break;
					IL_42:
					num++;
					num2 = 4;
					continue;
					IL_A4:
					num2 = 1;
				}
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0004DDD8 File Offset: 0x0004CDD8
		private void ᜀ(Body A_0, int A_1, int A_2, int A_3, int A_4, IRowsEnumerator A_5)
		{
			int a_ = 10;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_E57:
				goto IL_D3F;
			default:
				if (false)
				{
				}
				switch (0)
				{
				default:
					num = 66;
					break;
				}
				break;
			}
			string fieldName;
			for (;;)
			{
				IL_41:
				Paragraph paragraph;
				int num3;
				int num4;
				Field field;
				Field field2;
				int num6;
				int num7;
				int num8;
				int num9;
				int num10;
				switch (num)
				{
				case 0:
					num = 85;
					continue;
				case 1:
				{
					int num2;
					if (num2 > 0)
					{
						num = 130;
						continue;
					}
					MergeField mergeField;
					this.ᜀ(mergeField, false);
					num = 5;
					continue;
				}
				case 2:
					if (this.ᜈ)
					{
						num = 36;
						continue;
					}
					goto IL_397;
				case 3:
					if (!A_5.IsEnd)
					{
						num = 22;
						continue;
					}
					goto IL_72F;
				case 4:
					num = 91;
					continue;
				case 5:
					goto IL_4D3;
				case 6:
					if (A_5 != null)
					{
						num = 34;
						continue;
					}
					goto IL_BB1;
				case 7:
				{
					int count = A_0.Items.Count;
					this.GroupSelectors.Push(this.ᜁ);
					this.ᜁ = new MailMerge.ᜁ(new MailMerge.ᜁ.ᜀ(this.ᜅ));
					IRowsEnumerator rowsEnumerator;
					this.ᜁ.ᜀ(A_0, rowsEnumerator);
					int num2 = this.ᜁ.ᜁ();
					num = 77;
					continue;
				}
				case 8:
					A_5.NextRow();
					num = 51;
					continue;
				case 9:
					goto IL_97A;
				case 10:
				{
					MergeField mergeField;
					if (!MailMerge.ᜁ(mergeField))
					{
						num = 109;
						continue;
					}
					goto IL_929;
				}
				case 11:
					goto IL_A37;
				case 12:
					goto IL_97A;
				case 13:
					if (paragraph.Items.Count <= num3)
					{
						num = 55;
						continue;
					}
					num = 103;
					continue;
				case 14:
					this.ClearFieldsState.Add(fieldName, this.ClearFields);
					num = 128;
					continue;
				case 15:
					num4 = A_4;
					goto IL_B9A;
				case 16:
					if (A_5 != null)
					{
						num = 47;
						continue;
					}
					goto IL_72F;
				case 17:
					num4 = paragraph.Items.Count - 1;
					goto IL_B9A;
				case 18:
					if (!A_5.IsEnd)
					{
						num = 8;
						continue;
					}
					goto IL_BB1;
				case 19:
					if (field is IfField)
					{
						num = 94;
						continue;
					}
					num = 70;
					continue;
				case 20:
					field2 = null;
					goto IL_A9C;
				case 21:
					this.CurrentDataSetDocIO.ᜀ(fieldName);
					num = 48;
					continue;
				case 22:
					A_5.NextRow();
					num = 82;
					continue;
				case 23:
					goto IL_84F;
				case 24:
				{
					MergeField mergeField;
					if (mergeField.Prefix.StartsWith(ClipboardData.b("㥯άᕳᅵᵷ", a_)))
					{
						num = 40;
						continue;
					}
					this.ᜀ(mergeField, A_5);
					num = 56;
					continue;
				}
				case 25:
				{
					int num5;
					int count2;
					if (num5 >= count2)
					{
						num = 0;
						continue;
					}
					Table table;
					this.ᜀ(table, num5, 1, A_5);
					num5++;
					num = 63;
					continue;
				}
				case 26:
					goto IL_E47;
				case 27:
					this.SendMergeGroup(GroupEventType.GroupEnd, A_5);
					num = 90;
					continue;
				case 28:
					num = 45;
					continue;
				case 29:
				{
					IRowsEnumerator rowsEnumerator;
					if (rowsEnumerator != null)
					{
						num = 7;
						continue;
					}
					goto IL_97A;
				}
				case 30:
					num = 86;
					continue;
				case 31:
					if (this.ᜅ)
					{
						num = 93;
						continue;
					}
					goto IL_97A;
				case 32:
					goto IL_97A;
				case 33:
					if (field.Type != FieldType.FieldMergeRec)
					{
						num = 50;
						continue;
					}
					goto IL_E07;
				case 34:
					num = 18;
					continue;
				case 35:
					num = 95;
					continue;
				case 36:
					this.ᜁ(paragraph);
					num = 111;
					continue;
				case 37:
					if (paragraph.Items.Count > num3)
					{
						num = 28;
						continue;
					}
					goto IL_A07;
				case 38:
					num = 64;
					continue;
				case 39:
					goto IL_97A;
				case 40:
				{
					MergeField mergeField;
					this.ᜀ(mergeField, paragraph, A_5);
					num = 44;
					continue;
				}
				case 41:
				{
					if (num6 > num7)
					{
						num = 129;
						continue;
					}
					BodyRegion bodyRegion = A_0.Items[num6];
					DocumentObjectType documentObjectType = bodyRegion.DocumentObjectType;
					num = 124;
					continue;
				}
				case 42:
					if (A_4 <= -1)
					{
						num = 108;
						continue;
					}
					num = 15;
					continue;
				case 43:
					if (num6 != 0)
					{
						num = 115;
						continue;
					}
					num = 105;
					continue;
				case 44:
					goto IL_97A;
				case 45:
					if (paragraph.Items[num3].DocumentObjectType == DocumentObjectType.TextBox)
					{
						num = 76;
						continue;
					}
					goto IL_A07;
				case 46:
				{
					BodyRegion bodyRegion;
					Table table = bodyRegion as Table;
					int num5 = 0;
					int count2 = table.Rows.Count;
					num = 80;
					continue;
				}
				case 47:
					num = 3;
					continue;
				case 48:
					goto IL_42B;
				case 49:
					if (field.Type == FieldType.FieldMergeSeq)
					{
						num = 97;
						continue;
					}
					goto IL_97A;
				case 50:
					num = 49;
					continue;
				case 51:
					goto IL_BB1;
				case 52:
					num8 = 0;
					goto IL_DA6;
				case 53:
					goto IL_97A;
				case 54:
					this.SendMergeGroup(GroupEventType.GroupStart, A_5);
					num = 11;
					continue;
				case 55:
					num = 20;
					continue;
				case 56:
					goto IL_97A;
				case 57:
				{
					DocumentObjectType documentObjectType;
					if (documentObjectType == DocumentObjectType.Table)
					{
						num = 46;
						continue;
					}
					goto IL_397;
				}
				case 58:
					num = 69;
					continue;
				case 59:
					if (field.Type == FieldType.FieldNextIf)
					{
						num = 101;
						continue;
					}
					num = 33;
					continue;
				case 60:
					goto IL_4D3;
				case 61:
					goto IL_60C;
				case 62:
				{
					MergeField mergeField;
					fieldName = mergeField.FieldName;
					num = 106;
					continue;
				}
				case 63:
					goto IL_82C;
				case 64:
				{
					MergeField mergeField;
					if (mergeField.Domain != A_5.TableName)
					{
						num = 4;
						continue;
					}
					goto IL_60C;
				}
				case 65:
					goto IL_B31;
				case 67:
					if (!this.ClearFieldsState.ContainsKey(fieldName))
					{
						num = 14;
						continue;
					}
					this.ClearFieldsState[fieldName] = this.ClearFields;
					num = 23;
					continue;
				case 68:
					goto IL_97A;
				case 69:
				{
					MergeField mergeField;
					if (!this.NestedEnums.ContainsKey(mergeField.FieldName))
					{
						num = 62;
						continue;
					}
					goto IL_97A;
				}
				case 70:
					if (field.Type == FieldType.FieldNext)
					{
						num = 84;
						continue;
					}
					num = 59;
					continue;
				case 71:
					if (field != null)
					{
						num = 99;
						continue;
					}
					goto IL_97A;
				case 72:
					num = 10;
					continue;
				case 73:
				{
					MergeField mergeField;
					if (mergeField.Prefix == ClipboardData.b("㝯q᭳͵ࡷ⥹ࡻώ", a_))
					{
						num = 54;
						continue;
					}
					goto IL_A37;
				}
				case 74:
					num9 += A_5.CurrentRowIndex;
					num = 96;
					continue;
				case 75:
				{
					MergeField mergeField;
					if (mergeField.Prefix == ClipboardData.b("㝯q᭳͵ࡷ㽹ቻ᩽", a_))
					{
						num = 27;
						continue;
					}
					goto IL_D7D;
				}
				case 76:
				{
					TextBox textBox = paragraph.Items[num3] as TextBox;
					this.ᜀ(textBox.Body, 0, -1, 0, -1, A_5);
					num = 68;
					continue;
				}
				case 77:
				{
					int num2;
					if (num2 == -1)
					{
						num = 98;
						continue;
					}
					num = 1;
					continue;
				}
				case 78:
					this.CurrentDataSet.Tables.Remove(fieldName);
					num = 123;
					continue;
				case 79:
					num = 67;
					continue;
				case 80:
					goto IL_82C;
				case 81:
					if (num6 == num7)
					{
						num = 88;
						continue;
					}
					goto IL_9A4;
				case 82:
					goto IL_72F;
				case 83:
					goto IL_97A;
				case 84:
					num = 16;
					continue;
				case 85:
					goto IL_397;
				case 86:
				{
					MergeField mergeField;
					if (MailMerge.ᜁ(mergeField))
					{
						num = 117;
						continue;
					}
					goto IL_97A;
				}
				case 87:
				{
					MergeField mergeField = field as MergeField;
					fieldName = mergeField.FieldName;
					num = 131;
					continue;
				}
				case 88:
					num = 42;
					continue;
				case 89:
					goto IL_B0E;
				case 90:
					if (true)
					{
					}
					goto IL_D7D;
				case 91:
					if (this.ᜐ)
					{
						num = 61;
						continue;
					}
					goto IL_97A;
				case 92:
				{
					MergeField mergeField;
					if (!MailMerge.ᜂ(mergeField))
					{
						num = 72;
						continue;
					}
					goto IL_929;
				}
				case 93:
				{
					MergeField mergeField;
					this.ᜀ(mergeField, true);
					num = 53;
					continue;
				}
				case 94:
					this.ᜀ(field as IfField, A_5);
					num = 39;
					continue;
				case 95:
				{
					MergeField mergeField;
					if (MailMerge.ᜂ(mergeField))
					{
						num = 58;
						continue;
					}
					goto IL_97A;
				}
				case 96:
					goto IL_867;
				case 97:
					goto IL_E07;
				case 98:
					goto IL_6B8;
				case 99:
					num = 120;
					continue;
				case 100:
					goto IL_B0E;
				case 101:
					num = 116;
					continue;
				case 102:
				{
					MergeField mergeField;
					if (!MailMerge.ᜂ(mergeField))
					{
						num = 30;
						continue;
					}
					goto IL_901;
				}
				case 103:
					field2 = (paragraph.Items[num3] as Field);
					goto IL_A9C;
				case 104:
					A_2 = A_0.Items.Count - 1;
					num = 26;
					continue;
				case 105:
					num8 = A_3;
					goto IL_DA6;
				case 106:
					if (!(fieldName == string.Empty))
					{
						num = 121;
						continue;
					}
					goto IL_97A;
				case 107:
					if (A_5 != null)
					{
						num = 74;
						continue;
					}
					goto IL_867;
				case 108:
					goto IL_9A4;
				case 109:
					num = 24;
					continue;
				case 110:
					if (this.ᜐ)
					{
						num = 35;
						continue;
					}
					num = 102;
					continue;
				case 111:
					goto IL_397;
				case 112:
					goto IL_3A7;
				case 113:
					goto IL_E57;
				case 114:
				{
					MergeField mergeField;
					if (mergeField.Prefix == ClipboardData.b("⑯፱ᙳ᩵ᵷ⥹ࡻώ", a_))
					{
						num = 79;
						continue;
					}
					IRowsEnumerator rowsEnumerator = this.ᜆ(fieldName);
					num = 29;
					continue;
				}
				case 115:
					num = 52;
					continue;
				case 116:
					if (field.ᜌ())
					{
						num = 122;
						continue;
					}
					goto IL_BB1;
				case 117:
					goto IL_901;
				case 118:
					num = 57;
					continue;
				case 119:
					if (this.ᜋ != null)
					{
						num = 78;
						continue;
					}
					num = 126;
					continue;
				case 120:
					if (field is MergeField)
					{
						num = 87;
						continue;
					}
					num = 19;
					continue;
				case 121:
					num = 114;
					continue;
				case 122:
					num = 6;
					continue;
				case 123:
					goto IL_42B;
				case 124:
				{
					DocumentObjectType documentObjectType;
					if (documentObjectType != DocumentObjectType.Paragraph)
					{
						num = 118;
						continue;
					}
					BodyRegion bodyRegion;
					paragraph = (bodyRegion as Paragraph);
					num = 43;
					continue;
				}
				case 125:
					goto IL_B31;
				case 126:
					if (this.\u1716 != null)
					{
						num = 21;
						continue;
					}
					goto IL_42B;
				case 127:
					if (num3 > num10)
					{
						num = 65;
						continue;
					}
					num = 37;
					continue;
				case 128:
					goto IL_84F;
				case 129:
					return;
				case 130:
				{
					int count;
					int num11 = A_0.Items.Count - count;
					int num2;
					num6 += num11 + num2 - 1;
					num7 += num11;
					A_2 = num7;
					num = 60;
					continue;
				}
				case 131:
				{
					MergeField mergeField;
					if (mergeField.Domain != null)
					{
						num = 38;
						continue;
					}
					goto IL_60C;
				}
				}
				if (A_2 < 0)
				{
					num = 104;
					continue;
				}
				goto IL_E47;
				IL_397:
				num6++;
				num = 112;
				continue;
				IL_42B:
				this.ᜁ = this.GroupSelectors.Pop();
				num = 125;
				continue;
				IL_4D3:
				num = 119;
				continue;
				IL_60C:
				num = 73;
				continue;
				IL_72F:
				this.ᜀ(field, true);
				num = 32;
				continue;
				IL_82C:
				num = 25;
				continue;
				IL_84F:
				this.ClearFields = false;
				num = 83;
				continue;
				IL_867:
				this.ᜀ(field, num9.ToString());
				num = 12;
				continue;
				IL_901:
				num = 31;
				continue;
				IL_929:
				num = 110;
				continue;
				IL_97A:
				num3++;
				num = 100;
				continue;
				IL_9A4:
				num = 17;
				continue;
				IL_A07:
				num = 13;
				continue;
				IL_A37:
				num = 75;
				continue;
				IL_A9C:
				field = field2;
				num = 71;
				continue;
				IL_B0E:
				num = 127;
				continue;
				IL_B31:
				num = 2;
				continue;
				IL_B9A:
				num10 = num4;
				int num12;
				num3 = num12;
				num = 89;
				continue;
				IL_BB1:
				paragraph.Items.Remove(field);
				num10 = paragraph.Items.Count - 1;
				num3--;
				num = 9;
				continue;
				IL_D7D:
				num = 92;
				continue;
				IL_DA6:
				num12 = num8;
				num = 81;
				continue;
				IL_E07:
				num9 = 1;
				num = 107;
				continue;
				IL_E47:
				num6 = A_1;
				num7 = A_2;
				num = 113;
			}
			IL_3A7:
			goto IL_D3F;
			IL_6B8:
			throw new ApplicationException(ClipboardData.b("ɯ᝱፳ή᝷ᑹ屻屽", a_) + fieldName + ClipboardData.b("副剱ᵳյ塷᝹ᕻൽꢇ꺍晴뚕쎟잡蒣슥잧즩\ud9ab쎭햯\udcb1삳颵", a_));
			IL_D3F:
			num = 41;
			goto IL_41;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0004ED0C File Offset: 0x0004DD0C
		private int ᜀ(Table A_0, int A_1, int A_2, IRowsEnumerator A_3)
		{
			switch (0)
			{
			default:
			{
				int count;
				int num5;
				for (;;)
				{
					count = A_0.Rows.Count;
					int num = A_1 + A_2 - 1;
					TableRow tableRow = null;
					int num2 = A_1;
					int num3 = 0;
					for (;;)
					{
						string text;
						string a;
						switch (num3)
						{
						case 0:
							goto IL_24B;
						case 1:
							goto IL_300;
						case 2:
							if (this.ClearFieldsState.ContainsKey(text))
							{
								num3 = 17;
								continue;
							}
							goto IL_288;
						case 3:
						{
							int num4;
							int count2;
							if (num4 >= count2)
							{
								num3 = 4;
								continue;
							}
							TableCell a_ = tableRow.Cells[num4];
							this.ᜀ(a_, 0, -1, 0, -1, A_3);
							num4++;
							num3 = 8;
							continue;
						}
						case 4:
							goto IL_321;
						case 5:
							if (!(a == text))
							{
								num3 = 10;
								continue;
							}
							goto IL_3F4;
						case 6:
							goto IL_2A7;
						case 7:
						{
							IRowsEnumerator rowsEnumerator;
							if (rowsEnumerator != null)
							{
								num3 = 13;
								continue;
							}
							goto IL_1C9;
						}
						case 8:
							goto IL_300;
						case 9:
							goto IL_153;
						case 10:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_321;
							default:
								if (false)
								{
								}
								goto IL_288;
							}
							break;
						case 11:
							num3 = 18;
							continue;
						case 12:
							if (this.\u1716 != null)
							{
								num3 = 21;
								continue;
							}
							goto IL_153;
						case 13:
						{
							if (true)
							{
							}
							this.GroupSelectors.Push(this.ᜁ);
							this.ᜁ = new MailMerge.ᜁ(new MailMerge.ᜁ.ᜀ(this.ᜅ));
							IRowsEnumerator rowsEnumerator;
							this.ᜁ.ᜀ(A_0, A_1, num, rowsEnumerator);
							num3 = 27;
							continue;
						}
						case 14:
						{
							if (num2 > num)
							{
								num3 = 11;
								continue;
							}
							tableRow = A_0.Rows[num2];
							int num4 = 0;
							int count2 = tableRow.Cells.Count;
							num3 = 1;
							continue;
						}
						case 15:
							goto IL_153;
						case 16:
						{
							if (text == null)
							{
								num3 = 6;
								continue;
							}
							int count3;
							num5 = A_0.Rows.Count - count3;
							num += num5;
							A_1 += num5;
							count3 = A_0.Rows.Count;
							IRowsEnumerator rowsEnumerator = this.ᜆ(text);
							num3 = 7;
							continue;
						}
						case 17:
							this.ClearFields = this.ClearFieldsState[text];
							this.ClearFieldsState.Remove(text);
							num3 = 24;
							continue;
						case 18:
							if (this.ᜐ)
							{
								num3 = 19;
								continue;
							}
							goto IL_3F4;
						case 19:
						{
							text = this.ᜀ(A_1, num, A_0);
							int count3 = A_0.Rows.Count;
							num3 = 20;
							continue;
						}
						case 20:
							if (text != null)
							{
								num3 = 25;
								continue;
							}
							goto IL_288;
						case 21:
							this.CurrentDataSetDocIO.ᜀ(text);
							num3 = 9;
							continue;
						case 22:
							goto IL_1C9;
						case 23:
							this.CurrentDataSet.Tables.Remove(text);
							num3 = 15;
							continue;
						case 24:
							goto IL_288;
						case 25:
							num3 = 2;
							continue;
						case 26:
							goto IL_24B;
						case 27:
							if (this.ᜋ != null)
							{
								num3 = 23;
								continue;
							}
							num3 = 12;
							continue;
						}
						break;
						IL_153:
						this.ᜁ = this.GroupSelectors.Pop();
						num3 = 22;
						continue;
						IL_1C9:
						a = text;
						text = this.ᜀ(A_1, num, A_0);
						num3 = 5;
						continue;
						IL_24B:
						num3 = 14;
						continue;
						IL_288:
						num3 = 16;
						continue;
						IL_300:
						num3 = 3;
						continue;
						IL_321:
						num2++;
						num3 = 26;
					}
				}
				IL_2A7:
				IL_3F4:
				num5 = A_0.Rows.Count - count;
				return A_2 + num5;
			}
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0004F120 File Offset: 0x0004E120
		private void ᜇ(string A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					for (;;)
					{
						IRowsEnumerator rowsEnumerator = this.ᜆ(A_0);
						int num = 8;
						for (;;)
						{
							switch (num)
							{
							case 0:
								this.CurrentDataSetDocIO.ᜀ(A_0);
								num = 7;
								continue;
							case 1:
								this.ᜁ();
								num = 2;
								continue;
							case 2:
								if (this.ᜋ != null)
								{
									num = 4;
									continue;
								}
								num = 5;
								continue;
							case 3:
								return;
							case 4:
								goto IL_97;
							case 5:
								if (this.\u1716 != null)
								{
									num = 0;
									continue;
								}
								goto IL_172;
							case 6:
								goto IL_FF;
							case 7:
								goto IL_D4;
							case 8:
							{
								if (rowsEnumerator == null)
								{
									num = 3;
									continue;
								}
								int num2 = 0;
								int count = this.Document.Sections.Count;
								num = 6;
								continue;
							}
							case 9:
								goto IL_FF;
							case 10:
							{
								int num2;
								int count;
								if (num2 >= count)
								{
									num = 1;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
								{
									if (false)
									{
									}
									Section a_ = this.Document.Sections[num2];
									this.ᜀ(a_, rowsEnumerator);
									num2++;
									num = 9;
									continue;
								}
								}
								break;
							}
							}
							break;
							IL_FF:
							num = 10;
						}
					}
				}
				return;
				IL_97:
				this.CurrentDataSet.Tables.Remove(A_0);
				return;
				IL_D4:
				IL_172:
				if (true)
				{
				}
				return;
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0004F2A8 File Offset: 0x0004E2A8
		private IRowsEnumerator ᜆ(string A_0)
		{
			switch (0)
			{
			default:
			{
				IRowsEnumerator rowsEnumerator;
				for (;;)
				{
					rowsEnumerator = null;
					object obj = this.ᜅ(A_0);
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_53;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_53;
							default:
							{
								if (false)
								{
								}
								MailMergeDataTable mailMergeDataTable = obj as MailMergeDataTable;
								this.CurrentDataSetDocIO.Add(mailMergeDataTable);
								rowsEnumerator = new spr\u1977(mailMergeDataTable);
								rowsEnumerator.Reset();
								num = 4;
								continue;
							}
							}
							break;
						case 2:
							return rowsEnumerator;
						case 3:
							if (obj is MailMergeDataTable)
							{
								if (true)
								{
								}
								num = 1;
								continue;
							}
							return rowsEnumerator;
						case 4:
							return rowsEnumerator;
						case 5:
							if (obj is DataTable)
							{
								num = 0;
								continue;
							}
							num = 3;
							continue;
						}
						break;
						IL_53:
						DataTable dataTable = obj as DataTable;
						this.CurrentDataSet.Tables.Add(dataTable);
						rowsEnumerator = new spr\u1977(dataTable);
						rowsEnumerator.Reset();
						num = 2;
					}
				}
				return rowsEnumerator;
			}
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0004F3BC File Offset: 0x0004E3BC
		private void ᜀ(string A_0, IRowsEnumerator A_1)
		{
			if (!this.NestedEnums.ContainsKey(A_0))
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
					break;
				}
				this.NestedEnums.Add(A_0, A_1);
				return;
			}
			this.NestedEnums[A_0] = A_1;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0004F424 File Offset: 0x0004E424
		private object ᜅ(string A_0)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_86;
				case 1:
					goto IL_38;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_86;
					default:
						if (false)
						{
						}
						if (this.\u1714 != null)
						{
							num = 0;
							continue;
						}
						goto IL_90;
					}
					break;
				}
				if (true)
				{
				}
				if (this.ᜊ != null)
				{
					num = 1;
				}
				else
				{
					num = 2;
				}
			}
			IL_38:
			return this.ᜂ(A_0);
			IL_86:
			return this.ᜀ(A_0, this.\u1714);
			IL_90:
			return this.ᜁ(A_0);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0004F4C8 File Offset: 0x0004E4C8
		private MailMergeDataTable ᜀ(string A_0, MailMergeDataSet A_1)
		{
			if (true)
			{
			}
			MailMergeDataTable mailMergeDataTable;
			string text;
			for (;;)
			{
				mailMergeDataTable = A_1.ᜁ(A_0);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return mailMergeDataTable;
					case 1:
						if (mailMergeDataTable == null)
						{
							num = 3;
							continue;
						}
						text = this.ᜄ(A_0);
						num = 2;
						continue;
					case 2:
						while (!(text == string.Empty))
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_88;
							}
						}
						num = 0;
						continue;
					case 3:
						goto IL_3B;
					}
					break;
				}
			}
			IL_3B:
			return null;
			IL_88:
			if (false)
			{
			}
			return mailMergeDataTable.ᜀ(text);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0004F56C File Offset: 0x0004E56C
		private string ᜄ(string A_0)
		{
			int a_ = 14;
			switch (0)
			{
			default:
			{
				string text;
				for (;;)
				{
					DictionaryEntry dictionaryEntry = new DictionaryEntry(string.Empty, string.Empty);
					bool flag = false;
					int num = 9;
					for (;;)
					{
						int num3;
						int count2;
						switch (num)
						{
						case 0:
							goto IL_B4;
						case 1:
							flag = true;
							num = 18;
							continue;
						case 2:
							goto IL_276;
						case 3:
							flag = true;
							num = 7;
							continue;
						case 4:
							if (text.IndexOf(ClipboardData.b("关", a_)) == -1)
							{
								num = 20;
								continue;
							}
							goto IL_1E2;
						case 5:
							if (flag)
							{
								num = 17;
								continue;
							}
							goto IL_2DA;
						case 6:
						{
							int num2;
							int count;
							if (num2 >= count)
							{
								num = 16;
								continue;
							}
							dictionaryEntry = this.\u1715[num2];
							num = 13;
							continue;
						}
						case 7:
							goto IL_21F;
						case 8:
							if (A_0 == (string)dictionaryEntry.Key)
							{
								num = 3;
								continue;
							}
							num3++;
							num = 10;
							continue;
						case 9:
							if (this.ᜏ != null)
							{
								num = 21;
								continue;
							}
							num = 12;
							continue;
						case 10:
							goto IL_276;
						case 11:
							goto IL_B4;
						case 12:
							if (this.\u1715 != null)
							{
								num = 19;
								continue;
							}
							goto IL_21F;
						case 13:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_AF;
							default:
							{
								if (false)
								{
								}
								if (A_0 == (string)dictionaryEntry.Key)
								{
									num = 1;
									continue;
								}
								int num2;
								num2++;
								num = 11;
								continue;
							}
							}
							break;
						case 14:
							if (num3 >= count2)
							{
								num = 22;
								continue;
							}
							dictionaryEntry = this.ᜏ[num3];
							num = 8;
							continue;
						case 15:
							goto IL_21F;
						case 16:
							goto IL_21F;
						case 17:
							text = (string)dictionaryEntry.Value;
							num = 4;
							continue;
						case 18:
							goto IL_21F;
						case 19:
						{
							int num2 = 0;
							int count = this.\u1715.Count;
							num = 0;
							continue;
						}
						case 20:
							return text;
						case 21:
							goto IL_AF;
						case 22:
							if (true)
							{
							}
							num = 15;
							continue;
						}
						break;
						IL_AF:
						num3 = 0;
						count2 = this.ᜏ.Count;
						num = 2;
						continue;
						IL_B4:
						num = 6;
						continue;
						IL_21F:
						num = 5;
						continue;
						IL_276:
						num = 14;
					}
				}
				return text;
				IL_1E2:
				return this.ᜃ(text);
				IL_2DA:
				return null;
			}
			}
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0004F854 File Offset: 0x0004E854
		private string ᜃ(string A_0)
		{
			int a_ = 0;
			switch (0)
			{
			default:
				for (;;)
				{
					IL_94:
					MatchCollection matchCollection = this.VariableCommandRegex.Matches(A_0);
					int num = 18;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
						{
							if (false)
							{
							}
							string text;
							int num2;
							string text2;
							switch (num)
							{
							case 0:
								goto IL_28A;
							case 1:
							{
								string[] array;
								IRowsEnumerator rowsEnumerator = this.NestedEnums[array[0]];
								num = 4;
								continue;
							}
							case 2:
							{
								string[] array;
								DataTable dataTable;
								if (dataTable.Columns.Contains(array[1]))
								{
									num = 6;
									continue;
								}
								goto IL_2A3;
							}
							case 3:
							{
								string[] array;
								if (array.Length != 2)
								{
									num = 17;
									continue;
								}
								IRowsEnumerator rowsEnumerator = null;
								num = 5;
								continue;
							}
							case 4:
								goto IL_26B;
							case 5:
							{
								string[] array;
								if (this.NestedEnums.ContainsKey(array[0]))
								{
									num = 1;
									continue;
								}
								goto IL_26B;
							}
							case 6:
								num = 19;
								continue;
							case 7:
								if (true)
								{
								}
								text = ClipboardData.b("䅥", a_) + text + ClipboardData.b("䅥", a_);
								num = 11;
								continue;
							case 8:
							{
								string[] array;
								if (this.\u1713.Tables.Contains(array[0]))
								{
									num = 14;
									continue;
								}
								goto IL_2A3;
							}
							case 9:
							{
								IRowsEnumerator rowsEnumerator;
								if (rowsEnumerator == null)
								{
									num = 0;
									continue;
								}
								string[] array;
								text = rowsEnumerator.GetCellValue(array[1]).ToString();
								num = 20;
								continue;
							}
							case 10:
								return A_0;
							case 11:
								goto IL_2A3;
							case 12:
								goto IL_C4;
							case 13:
								goto IL_23B;
							case 14:
							{
								string[] array;
								DataTable dataTable = this.\u1713.Tables[array[0]];
								num = 2;
								continue;
							}
							case 15:
								num = 8;
								continue;
							case 16:
								goto IL_23B;
							case 17:
								goto IL_19B;
							case 18:
								goto IL_AD;
							case 19:
							{
								string[] array;
								DataTable dataTable;
								if (dataTable.Columns[array[1]].DataType.Name == ClipboardData.b("㕥ᱧᡩիmᝯ", a_))
								{
									num = 7;
									continue;
								}
								goto IL_2A3;
							}
							case 20:
								if (this.\u1713 != null)
								{
									num = 15;
									continue;
								}
								goto IL_2A3;
							case 21:
							{
								int count;
								if (num2 >= count)
								{
									num = 10;
									continue;
								}
								text2 = matchCollection[num2].Value.Replace(ClipboardData.b("䍥", a_), string.Empty);
								char[] separator;
								string[] array = text2.Split(separator);
								num = 3;
								continue;
							}
							}
							goto IL_94;
							IL_23B:
							num = 21;
							continue;
							IL_26B:
							num = 9;
							continue;
							IL_2A3:
							A_0 = A_0.Replace(ClipboardData.b("䍥", a_) + text2 + ClipboardData.b("䍥", a_), text);
							num2++;
							num = 13;
							continue;
						}
						}
						IL_AD:
						if (matchCollection.Count == 0)
						{
							num = 12;
						}
						else
						{
							char[] separator = new char[]
							{
								'.'
							};
							string text = null;
							string text2 = null;
							string[] array = null;
							int num2 = 0;
							int count = matchCollection.Count;
							num = 16;
						}
					}
				}
				IL_C4:
				return null;
				IL_19B:
				throw new ArgumentException(ClipboardData.b("㕥ᱧᡩիmᝯ剱ɳ᝵ᑷཹ᥻幽꺍랏랑뎓뚕ﲝ쾟캡힣蚥肧\udca9춫\udcad\ud9af펱횳\udab5\uddb7骹\udfbb톽궿꿁ꗃꣅ곇ꟍꏏ뫓맕곗龎ꫛ뿝賟诡胣죥", a_));
				IL_28A:
				return string.Empty;
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0004FBF8 File Offset: 0x0004EBF8
		private void ᜁ(int A_0, int A_1, Table A_2)
		{
			int a_ = 17;
			switch (0)
			{
			default:
			{
				Dictionary<string, MergeField> dictionary;
				Dictionary<string, MergeField> dictionary2;
				for (;;)
				{
					dictionary = new Dictionary<string, MergeField>();
					dictionary2 = new Dictionary<string, MergeField>();
					MergeField mergeField = null;
					int num = A_0;
					int num2 = 7;
					for (;;)
					{
						if (true)
						{
						}
						Dictionary<string, MergeField>.KeyCollection.Enumerator enumerator2;
						switch (num2)
						{
						case 0:
							if (num > A_1)
							{
								num2 = 6;
								continue;
							}
							goto IL_2D4;
						case 1:
						{
							Dictionary<string, MergeField>.KeyCollection.Enumerator enumerator = dictionary2.Keys.GetEnumerator();
							num2 = 16;
							continue;
						}
						case 2:
							if (dictionary.Count == 0)
							{
								num2 = 4;
								continue;
							}
							goto IL_1AE;
						case 3:
							if (dictionary.Count > 0)
							{
								num2 = 12;
								continue;
							}
							goto IL_95;
						case 4:
							num2 = 17;
							continue;
						case 5:
							if (dictionary2.Count == 0)
							{
								num2 = 18;
								continue;
							}
							goto IL_95;
						case 6:
							num2 = 2;
							continue;
						case 7:
							goto IL_D1;
						case 8:
							try
							{
								num2 = 4;
								string text;
								for (;;)
								{
									switch (num2)
									{
									case 0:
										goto IL_41E;
									case 2:
										num2 = 0;
										continue;
									case 3:
										if (!dictionary2.ContainsKey(text))
										{
											num2 = 6;
											continue;
										}
										dictionary2.Remove(text);
										num2 = 1;
										continue;
									case 5:
										if (!enumerator2.MoveNext())
										{
											num2 = 2;
											continue;
										}
										text = enumerator2.Current;
										num2 = 3;
										continue;
									case 6:
										goto IL_3B0;
									}
									IL_3EF:
									num2 = 5;
									continue;
									goto IL_3EF;
								}
								IL_3B0:
								throw new ApplicationException(ClipboardData.b("⍶ᡸ᥺ᅼ᩾튀ﶈꮊﾒ랖뮘", a_) + text + ClipboardData.b("啶奸ὺቼ᩾ꊄꦈ年뎒솔ﮘ\uda9e쾠잢薤솦삨캪솬쮮醰횲쒴슶킸춺\udcbc펾꓀귂뇄", a_));
								IL_41E:
								goto IL_F6;
							}
							finally
							{
								((IDisposable)enumerator2).Dispose();
							}
							goto IL_431;
							IL_F6:
							num2 = 15;
							continue;
						case 9:
							try
							{
								num2 = 1;
								Dictionary<string, MergeField>.KeyCollection.Enumerator enumerator3;
								for (;;)
								{
									switch (num2)
									{
									case 0:
										goto IL_4D6;
									case 2:
										num2 = 0;
										continue;
									case 3:
										if (!enumerator3.MoveNext())
										{
											num2 = 2;
											continue;
										}
										goto IL_47E;
									}
									num2 = 3;
								}
								IL_47E:
								string str = enumerator3.Current;
								throw new ApplicationException(ClipboardData.b("⍶ᡸ᥺ᅼ᩾튀ﶈꮊﾒ랖뮘", a_) + str + ClipboardData.b("啶奸ὺቼ᩾ꊄꦈ年뎒솔ﮘ\uda9e쾠잢薤솦삨캪솬쮮醰횲쒴슶킸춺\udcbc펾꓀귂뇄", a_));
								IL_4D6:
								goto IL_95;
							}
							finally
							{
								Dictionary<string, MergeField>.KeyCollection.Enumerator enumerator3;
								((IDisposable)enumerator3).Dispose();
							}
							goto Block_11;
						case 10:
							goto IL_4E9;
						case 11:
						{
							Dictionary<string, MergeField>.KeyCollection.Enumerator enumerator4 = dictionary2.Keys.GetEnumerator();
							num2 = 13;
							continue;
						}
						case 12:
						{
							Dictionary<string, MergeField>.KeyCollection.Enumerator enumerator3 = dictionary.Keys.GetEnumerator();
							num2 = 9;
							continue;
						}
						case 13:
							try
							{
								num2 = 2;
								Dictionary<string, MergeField>.KeyCollection.Enumerator enumerator4;
								for (;;)
								{
									switch (num2)
									{
									case 0:
										num2 = 3;
										continue;
									case 1:
										if (!enumerator4.MoveNext())
										{
											num2 = 0;
											continue;
										}
										goto IL_143;
									case 3:
										goto IL_19B;
									}
									num2 = 1;
								}
								IL_143:
								string str2 = enumerator4.Current;
								throw new ApplicationException(ClipboardData.b("⍶ᡸ᥺ᅼ᩾쒀Ꞇ뎒랔", a_) + str2 + ClipboardData.b("啶奸ὺቼ᩾ꊄꦈ年뎒솔ﮘ첞햠슢힤펦覨춪쒬쪮\uddb0ힲ閴튶좸캺풼즾ꃀ꿂ꃄ꧆뷈", a_));
								IL_19B:
								goto IL_861;
							}
							finally
							{
								Dictionary<string, MergeField>.KeyCollection.Enumerator enumerator4;
								((IDisposable)enumerator4).Dispose();
							}
							goto IL_1AE;
						case 14:
							goto IL_D1;
						case 15:
							if (dictionary2.Count > 0)
							{
								num2 = 11;
								continue;
							}
							goto IL_861;
						case 16:
							try
							{
								num2 = 0;
								Dictionary<string, MergeField>.KeyCollection.Enumerator enumerator;
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
										switch (num2)
										{
										case 1:
											goto IL_2B3;
										case 2:
											goto IL_2C1;
										case 3:
											if (!enumerator.MoveNext())
											{
												num2 = 1;
												continue;
											}
											goto IL_263;
										}
										num2 = 3;
										continue;
									}
									IL_2B3:
									num2 = 2;
								}
								IL_263:
								string str3 = enumerator.Current;
								throw new ApplicationException(ClipboardData.b("⍶ᡸ᥺ᅼ᩾쒀Ꞇ뎒랔", a_) + str3 + ClipboardData.b("啶奸ὺቼ᩾ꊄꦈ年뎒솔ﮘ첞햠슢힤펦覨춪쒬쪮\uddb0ힲ閴튶좸캺풼즾ꃀ꿂ꃄ꧆뷈", a_));
								IL_2C1:
								goto IL_95;
							}
							finally
							{
								Dictionary<string, MergeField>.KeyCollection.Enumerator enumerator;
								((IDisposable)enumerator).Dispose();
							}
							goto IL_2D4;
						case 17:
							if (dictionary2.Count > 0)
							{
								num2 = 1;
								continue;
							}
							goto IL_95;
						case 18:
							goto IL_431;
						}
						break;
						IL_95:
						enumerator2 = dictionary.Keys.GetEnumerator();
						num2 = 8;
						continue;
						IL_D1:
						num2 = 0;
						continue;
						IL_1AE:
						num2 = 5;
						continue;
						IL_2D4:
						TableRow tableRow = A_2.Rows[num];
						IEnumerator enumerator5 = tableRow.Cells.GetEnumerator();
						num2 = 10;
						continue;
						Block_11:
						try
						{
							IL_4E9:
							num2 = 2;
							for (;;)
							{
								switch (num2)
								{
								case 0:
									try
									{
										num2 = 0;
										for (;;)
										{
											switch (num2)
											{
											case 1:
												goto IL_75D;
											case 2:
												goto IL_769;
											case 3:
											{
												IEnumerator enumerator6;
												if (!enumerator6.MoveNext())
												{
													num2 = 1;
													continue;
												}
												Paragraph paragraph = (Paragraph)enumerator6.Current;
												IEnumerator enumerator7 = paragraph.Items.GetEnumerator();
												num2 = 4;
												continue;
											}
											case 4:
												try
												{
													num2 = 6;
													for (;;)
													{
														switch (num2)
														{
														case 0:
															if (!mergeField.ConvertedToText)
															{
																num2 = 10;
																continue;
															}
															goto IL_6B1;
														case 2:
															num2 = 8;
															continue;
														case 4:
															goto IL_70F;
														case 5:
														{
															IEnumerator enumerator7;
															if (!enumerator7.MoveNext())
															{
																num2 = 15;
																continue;
															}
															ParagraphBase paragraphBase = (ParagraphBase)enumerator7.Current;
															num2 = 13;
															continue;
														}
														case 7:
															if (MailMerge.ᜂ(mergeField))
															{
																num2 = 12;
																continue;
															}
															goto IL_6B1;
														case 8:
															if (!mergeField.ConvertedToText)
															{
																num2 = 9;
																continue;
															}
															break;
														case 9:
															dictionary2.Add(mergeField.FieldName, mergeField);
															num2 = 3;
															continue;
														case 10:
															dictionary.Add(mergeField.FieldName, mergeField);
															num2 = 1;
															continue;
														case 11:
														{
															ParagraphBase paragraphBase;
															mergeField = (paragraphBase as MergeField);
															num2 = 7;
															continue;
														}
														case 12:
															num2 = 0;
															continue;
														case 13:
														{
															ParagraphBase paragraphBase;
															if (paragraphBase is MergeField)
															{
																num2 = 11;
																continue;
															}
															break;
														}
														case 14:
															if (MailMerge.ᜁ(mergeField))
															{
																num2 = 2;
																continue;
															}
															break;
														case 15:
															num2 = 4;
															continue;
														}
														IL_670:
														num2 = 5;
														continue;
														goto IL_670;
														IL_6B1:
														num2 = 14;
													}
													IL_70F:
													break;
												}
												finally
												{
													for (;;)
													{
														IEnumerator enumerator7;
														IDisposable disposable = enumerator7 as IDisposable;
														num2 = 0;
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
																goto IL_75C;
															case 1:
																disposable.Dispose();
																num2 = 2;
																continue;
															case 2:
																goto IL_75A;
															}
															break;
														}
													}
													IL_75A:
													IL_75C:;
												}
												goto IL_75D;
											}
											IL_53F:
											num2 = 3;
											continue;
											goto IL_53F;
											IL_75D:
											num2 = 2;
										}
										IL_769:;
									}
									finally
									{
										for (;;)
										{
											IEnumerator enumerator6;
											IDisposable disposable2 = enumerator6 as IDisposable;
											num2 = 0;
											for (;;)
											{
												switch (num2)
												{
												case 0:
													if (disposable2 != null)
													{
														num2 = 2;
														continue;
													}
													goto IL_7B3;
												case 1:
													goto IL_7B1;
												case 2:
													disposable2.Dispose();
													num2 = 1;
													continue;
												}
												break;
											}
										}
										IL_7B1:
										IL_7B3:;
									}
									break;
								case 1:
									num2 = 4;
									continue;
								case 3:
								{
									if (!enumerator5.MoveNext())
									{
										num2 = 1;
										continue;
									}
									TableCell tableCell = (TableCell)enumerator5.Current;
									IEnumerator enumerator6 = tableCell.Paragraphs.GetEnumerator();
									num2 = 0;
									continue;
								}
								case 4:
									goto IL_813;
								}
								IL_7B4:
								num2 = 3;
								continue;
								goto IL_7B4;
							}
							IL_813:
							goto IL_300;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable3 = enumerator5 as IDisposable;
								num2 = 2;
								for (;;)
								{
									switch (num2)
									{
									case 0:
										disposable3.Dispose();
										num2 = 1;
										continue;
									case 1:
										goto IL_85E;
									case 2:
										if (disposable3 != null)
										{
											num2 = 0;
											continue;
										}
										goto IL_860;
									}
									break;
								}
							}
							IL_85E:
							IL_860:;
						}
						goto IL_861;
						IL_300:
						num++;
						num2 = 14;
						continue;
						IL_431:
						num2 = 3;
					}
				}
				IL_861:
				dictionary.Clear();
				dictionary2.Clear();
				return;
			}
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00050520 File Offset: 0x0004F520
		private void ᜃ(MergeField A_0)
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
			this.ᜀ(A_0, true);
			A_0.FieldName = string.Empty;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00050570 File Offset: 0x0004F570
		private string ᜀ(int A_0, int A_1, Table A_2)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int num = A_0;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							string result;
							try
							{
								num2 = 2;
								for (;;)
								{
									switch (num2)
									{
									case 0:
									{
										IEnumerator enumerator;
										if (!enumerator.MoveNext())
										{
											num2 = 1;
											continue;
										}
										TableCell tableCell = (TableCell)enumerator.Current;
										IEnumerator enumerator2 = tableCell.Paragraphs.GetEnumerator();
										num2 = 3;
										continue;
									}
									case 1:
										goto IL_3FD;
									case 3:
										try
										{
											num2 = 1;
											for (;;)
											{
												IEnumerator enumerator2;
												IEnumerator enumerator3;
												switch (num2)
												{
												case 0:
													goto IL_393;
												case 2:
													if (!enumerator2.MoveNext())
													{
														num2 = 4;
														continue;
													}
													goto IL_336;
												case 3:
													try
													{
														num2 = 5;
														for (;;)
														{
															string text;
															switch (num2)
															{
															case 0:
															{
																MergeField mergeField;
																if (mergeField.FieldName != string.Empty)
																{
																	num2 = 13;
																	continue;
																}
																break;
															}
															case 1:
																num2 = 0;
																continue;
															case 2:
																num2 = 3;
																continue;
															case 3:
																goto IL_2EB;
															case 4:
																num2 = 14;
																continue;
															case 6:
																text = null;
																goto IL_2CC;
															case 7:
																goto IL_2DA;
															case 8:
															{
																ParagraphBase paragraphBase;
																if (paragraphBase is MergeField)
																{
																	num2 = 12;
																	continue;
																}
																break;
															}
															case 9:
															{
																if (!enumerator3.MoveNext())
																{
																	num2 = 2;
																	continue;
																}
																ParagraphBase paragraphBase = (ParagraphBase)enumerator3.Current;
																num2 = 8;
																continue;
															}
															case 10:
															{
																MergeField mergeField;
																if (MailMerge.ᜂ(mergeField))
																{
																	num2 = 1;
																	continue;
																}
																break;
															}
															case 11:
															{
																string fieldName;
																if (!(fieldName == string.Empty))
																{
																	num2 = 4;
																	continue;
																}
																num2 = 6;
																continue;
															}
															case 12:
															{
																ParagraphBase paragraphBase;
																MergeField mergeField = paragraphBase as MergeField;
																num2 = 10;
																continue;
															}
															case 13:
															{
																ParagraphBase paragraphBase;
																string fieldName = (paragraphBase as MergeField).FieldName;
																num2 = 11;
																continue;
															}
															case 14:
															{
																string fieldName;
																text = fieldName;
																goto IL_2CC;
															}
															}
															IL_1EA:
															num2 = 9;
															continue;
															goto IL_1EA;
															IL_2CC:
															result = text;
															num2 = 7;
														}
														IL_2DA:
														return result;
														IL_2EB:
														break;
													}
													finally
													{
														for (;;)
														{
															IDisposable disposable = enumerator3 as IDisposable;
															num2 = 0;
															for (;;)
															{
																switch (num2)
																{
																case 0:
																	if (disposable != null)
																	{
																		num2 = 2;
																		continue;
																	}
																	goto IL_335;
																case 1:
																	goto IL_333;
																case 2:
																	disposable.Dispose();
																	num2 = 1;
																	continue;
																}
																break;
															}
														}
														IL_333:
														IL_335:;
													}
													goto IL_336;
												case 4:
													num2 = 0;
													continue;
												}
												goto IL_16F;
												IL_336:
												Paragraph paragraph = (Paragraph)enumerator2.Current;
												enumerator3 = paragraph.Items.GetEnumerator();
												num2 = 3;
												continue;
												IL_361:
												num2 = 2;
												continue;
												IL_16F:
												goto IL_361;
											}
											IL_393:
											break;
										}
										finally
										{
											for (;;)
											{
												IL_3AD:
												IDisposable disposable2;
												switch ((1 == 1) ? 1 : 0)
												{
												case 0:
												case 2:
													IL_3F1:
													num2 = 2;
													break;
												default:
												{
													if (false)
													{
													}
													IEnumerator enumerator2;
													disposable2 = (enumerator2 as IDisposable);
													num2 = 1;
													break;
												}
												}
												for (;;)
												{
													switch (num2)
													{
													case 0:
														goto IL_3E8;
													case 1:
														if (disposable2 != null)
														{
															num2 = 0;
															continue;
														}
														goto IL_3FC;
													case 2:
														goto IL_3FA;
													}
													goto IL_3AD;
												}
												IL_3E8:
												disposable2.Dispose();
												goto IL_3F1;
											}
											IL_3FA:
											IL_3FC:;
										}
										goto IL_3FD;
									case 4:
										goto IL_409;
									}
									IL_126:
									num2 = 0;
									continue;
									goto IL_126;
									IL_3FD:
									num2 = 4;
								}
								IL_409:
								goto IL_4F;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator;
									IDisposable disposable3 = enumerator as IDisposable;
									num2 = 2;
									for (;;)
									{
										switch (num2)
										{
										case 0:
											disposable3.Dispose();
											num2 = 1;
											continue;
										case 1:
											goto IL_454;
										case 2:
											if (disposable3 != null)
											{
												num2 = 0;
												continue;
											}
											goto IL_456;
										}
										break;
									}
								}
								IL_454:
								IL_456:;
							}
							return result;
						}
						case 1:
							goto IL_AD;
						case 2:
						{
							TableRow tableRow = A_2.Rows[num];
							IEnumerator enumerator = tableRow.Cells.GetEnumerator();
							num2 = 0;
							continue;
						}
						case 3:
							goto IL_AD;
						case 4:
							if (num < A_2.Rows.Count)
							{
								num2 = 2;
								continue;
							}
							goto IL_4F;
						case 5:
							goto IL_D4;
						case 6:
							if (true)
							{
							}
							if (num > A_1)
							{
								num2 = 5;
								continue;
							}
							num2 = 4;
							continue;
						}
						break;
						IL_4F:
						num++;
						num2 = 1;
						continue;
						IL_AD:
						num2 = 6;
					}
				}
				IL_D4:
				return null;
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00050A24 File Offset: 0x0004FA24
		private DataTable ᜂ(string A_0)
		{
			int a_ = 12;
			switch (0)
			{
			default:
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_D3:
					goto IL_93;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					goto IL_72;
				}
				int num;
				string text;
				DbCommand dbCommand;
				DbDataAdapter dbDataAdapter;
				DataTable dataTable;
				for (;;)
				{
					IL_47:
					switch (num)
					{
					case 0:
						if (text == null)
						{
							num = 8;
							continue;
						}
						num = 5;
						continue;
					case 1:
						goto IL_FA;
					case 2:
						goto IL_D3;
					case 3:
						goto IL_17E;
					case 4:
						if (this.\u1712)
						{
							num = 3;
							continue;
						}
						dbCommand = new OleDbCommand(text, this.ᜊ as OleDbConnection);
						dbDataAdapter = new OleDbDataAdapter(dbCommand as OleDbCommand);
						num = 7;
						continue;
					case 5:
						if (text == string.Empty)
						{
							num = 1;
							continue;
						}
						goto IL_153;
					case 6:
						goto IL_153;
					case 7:
						goto IL_14C;
					case 8:
						text = ClipboardData.b("ⅱᅳ᩵ᵷ᥹ࡻ幽ꩿꊁ겋", a_) + A_0;
						num = 6;
						continue;
					}
					goto IL_72;
					IL_153:
					dataTable = new DataTable(A_0);
					dbDataAdapter = null;
					num = 4;
				}
				IL_FA:
				return null;
				IL_14C:
				goto IL_93;
				IL_17E:
				goto IL_A9;
				IL_72:
				dataTable = null;
				text = this.ᜄ(A_0);
				num = 0;
				goto IL_47;
				try
				{
					IL_93:
					dbDataAdapter.Fill(dataTable);
					return dataTable;
				}
				catch
				{
					return dataTable;
				}
				IL_A9:
				dbCommand = new SqlCommand(text, this.ᜊ as SqlConnection);
				dbDataAdapter = new SqlDataAdapter(dbCommand as SqlCommand);
				num = 2;
				goto IL_47;
			}
			}
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00050BC8 File Offset: 0x0004FBC8
		private DataTable ᜁ(string A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					DataTable dataTable = this.\u1713.Tables[A_0];
					int num = 9;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_F2;
						case 1:
						{
							int num2;
							DataRow[] array;
							if (num2 >= array.Length)
							{
								num = 7;
								continue;
							}
							DataRow dataRow = array[num2];
							DataTable dataTable2;
							DataRow dataRow2 = dataTable2.NewRow();
							dataRow2.ItemArray = dataRow.ItemArray;
							dataRow2.RowError = dataRow.RowError;
							dataTable2.Rows.Add(dataRow2);
							num2++;
							num = 6;
							continue;
						}
						case 2:
						{
							try
							{
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 0:
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_1FC;
										default:
											if (false)
											{
											}
											break;
										}
										break;
									case 1:
										goto IL_1FC;
									case 3:
										goto IL_205;
									case 4:
									{
										IEnumerator enumerator;
										if (!enumerator.MoveNext())
										{
											num = 1;
											continue;
										}
										DataColumn dataColumn = (DataColumn)enumerator.Current;
										DataTable dataTable2;
										DataColumn dataColumn2 = dataTable2.Columns.Add(dataColumn.ColumnName);
										dataColumn2.DataType = dataColumn.DataType;
										num = 2;
										continue;
									}
									}
									IL_1DF:
									num = 4;
									continue;
									goto IL_1DF;
									IL_1FC:
									num = 3;
								}
								IL_205:
								goto IL_75;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator;
									IDisposable disposable = enumerator as IDisposable;
									num = 0;
									for (;;)
									{
										switch (num)
										{
										case 0:
											if (disposable != null)
											{
												num = 1;
												continue;
											}
											goto IL_252;
										case 1:
											disposable.Dispose();
											num = 2;
											continue;
										case 2:
											goto IL_250;
										}
										break;
									}
								}
								IL_250:
								IL_252:;
							}
							goto IL_253;
							IL_75:
							DataRow[] array2;
							DataRow[] array = array2;
							int num2 = 0;
							num = 4;
							continue;
						}
						case 3:
							goto IL_70;
						case 4:
							goto IL_AD;
						case 5:
						{
							string text;
							if (text == null)
							{
								num = 0;
								continue;
							}
							DataRow[] array2 = null;
							num = 8;
							continue;
						}
						case 6:
							goto IL_AD;
						case 7:
						{
							DataTable dataTable2;
							return dataTable2;
						}
						case 8:
						{
							if (true)
							{
							}
							DataTable result;
							try
							{
								string text;
								DataRow[] array2 = dataTable.Select(text);
								goto IL_86;
							}
							catch
							{
								result = null;
							}
							return result;
							IL_86:
							DataTable dataTable2 = new DataTable(A_0);
							IEnumerator enumerator = dataTable.Columns.GetEnumerator();
							num = 2;
							continue;
						}
						case 9:
						{
							if (dataTable == null)
							{
								num = 3;
								continue;
							}
							string text = this.ᜄ(A_0);
							num = 5;
							continue;
						}
						}
						break;
						IL_AD:
						num = 1;
					}
				}
				IL_70:
				goto IL_253;
				IL_F2:
				return null;
				IL_253:
				return null;
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00050E60 File Offset: 0x0004FE60
		protected MergeFieldEventArgs SendMergeField(IMergeField field, object value, IRowsEnumerator rowsEnum)
		{
			MergeFieldEventArgs mergeFieldEventArgs;
			for (;;)
			{
				mergeFieldEventArgs = new MergeFieldEventArgs(this.Document, rowsEnum.TableName, rowsEnum.CurrentRowIndex, field, value);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_50;
					case 1:
						if (this.\u1719 != null)
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						return mergeFieldEventArgs;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_50;
						default:
							goto IL_7B;
						}
						break;
					}
					break;
					IL_50:
					this.\u1719(this, mergeFieldEventArgs);
					num = 2;
				}
			}
			IL_7B:
			if (false)
			{
			}
			return mergeFieldEventArgs;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00050EFC File Offset: 0x0004FEFC
		protected MergeGroupEventArgs SendMergeGroup(GroupEventType eventType, IRowsEnumerator rowsEnum)
		{
			if (true)
			{
			}
			MergeGroupEventArgs mergeGroupEventArgs;
			for (;;)
			{
				mergeGroupEventArgs = new MergeGroupEventArgs(this.Document, rowsEnum.TableName, rowsEnum.CurrentRowIndex, eventType);
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
							goto IL_4F;
						default:
							goto IL_7A;
						}
						break;
					case 1:
						goto IL_4F;
					case 2:
						if (this.\u171B != null)
						{
							num = 1;
							continue;
						}
						return mergeGroupEventArgs;
					}
					break;
					IL_4F:
					this.\u171B(this, mergeGroupEventArgs);
					num = 0;
				}
			}
			IL_7A:
			if (false)
			{
			}
			return mergeGroupEventArgs;
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00050F98 File Offset: 0x0004FF98
		protected MergeImageFieldEventArgs SendMergeImageField(IMergeField field, object bmp, IRowsEnumerator rowsEnum)
		{
			MergeImageFieldEventArgs mergeImageFieldEventArgs;
			for (;;)
			{
				mergeImageFieldEventArgs = null;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return mergeImageFieldEventArgs;
					case 1:
						this.\u171A(this, mergeImageFieldEventArgs);
						num = 0;
						continue;
					case 2:
						if (this.\u171A != null)
						{
							num = 1;
							continue;
						}
						return mergeImageFieldEventArgs;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_76;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							goto IL_3B;
						}
						break;
					case 4:
						goto IL_76;
					case 5:
						if (rowsEnum != null)
						{
							num = 4;
							continue;
						}
						mergeImageFieldEventArgs = new MergeImageFieldEventArgs(this.Document, null, int.MaxValue, field, bmp);
						num = 6;
						continue;
					case 6:
						goto IL_3B;
					}
					break;
					IL_3B:
					num = 2;
					continue;
					IL_76:
					mergeImageFieldEventArgs = new MergeImageFieldEventArgs(this.Document, rowsEnum.TableName, rowsEnum.CurrentRowIndex, field, bmp);
					num = 3;
				}
			}
			return mergeImageFieldEventArgs;
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00051090 File Offset: 0x00050090
		private void ᜁ(IRowsEnumerator A_0)
		{
			int a_ = 5;
			switch (0)
			{
			default:
				for (;;)
				{
					this.Document.ᜈ = true;
					this.ᜀ();
					int num = 19;
					for (;;)
					{
						IWSectionCollection sections;
						switch (num)
						{
						case 0:
							if (this.ᜅ)
							{
								num = 5;
								continue;
							}
							goto IL_19D;
						case 1:
							goto IL_2B5;
						case 2:
							num = 0;
							continue;
						case 3:
						{
							int num2 = sections.Count;
							num = 14;
							continue;
						}
						case 4:
							goto IL_B6;
						case 5:
						{
							int num3 = 0;
							int count = sections.Count;
							num = 1;
							continue;
						}
						case 6:
							goto IL_ED;
						case 7:
							goto IL_19D;
						case 8:
						{
							int rowsCount;
							if (rowsCount > 1)
							{
								num = 12;
								continue;
							}
							goto IL_1E5;
						}
						case 9:
							goto IL_236;
						case 10:
						{
							int num4;
							int count2;
							if (num4 >= count2)
							{
								if (true)
								{
								}
								num = 3;
								continue;
							}
							this.ᜀ(sections[num4], A_0);
							num4++;
							num = 18;
							continue;
						}
						case 11:
							if (A_0.RowsCount == 0)
							{
								num = 2;
								continue;
							}
							goto IL_19D;
						case 12:
							this.ᜁ(this.Document);
							num = 21;
							continue;
						case 13:
							goto IL_2B5;
						case 14:
							if (!A_0.IsLast)
							{
								num = 22;
								continue;
							}
							goto IL_19D;
						case 15:
							num = 6;
							continue;
						case 16:
						{
							int num3;
							int count;
							if (num3 >= count)
							{
								num = 15;
								continue;
							}
							this.ᜀ(sections[num3], null);
							num3++;
							num = 13;
							continue;
						}
						case 17:
							if (!A_0.NextRow())
							{
								num = 20;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
							{
								if (false)
								{
								}
								int num2;
								int num4 = num2;
								int count2 = sections.Count;
								break;
							}
							}
							num = 9;
							continue;
						case 18:
							goto IL_236;
						case 19:
						{
							if (A_0 == null)
							{
								num = 4;
								continue;
							}
							int rowsCount = A_0.RowsCount;
							int num2 = 0;
							num = 8;
							continue;
						}
						case 20:
							goto IL_1BD;
						case 21:
							goto IL_1E5;
						case 22:
							this.ᜀ(this.Document);
							num = 7;
							continue;
						}
						break;
						IL_19D:
						num = 17;
						continue;
						IL_1E5:
						sections = this.Document.Sections;
						A_0.Reset();
						num = 11;
						continue;
						IL_236:
						num = 10;
						continue;
						IL_2B5:
						num = 16;
					}
				}
				IL_B6:
				throw new ArgumentNullException(ClipboardData.b("ᥪɬᡮɰ㙲᭴ɶᑸ", a_));
				IL_ED:
				IL_1BD:
				this.Document.ᜈ = false;
				return;
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00051384 File Offset: 0x00050384
		private void ᜀ(ISection A_0, IRowsEnumerator A_1)
		{
			for (;;)
			{
				this.ᜀ(A_0.Body.Items, A_1);
				int num = 0;
				int num2 = 6;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						BodyRegionCollection bodyRegionCollection;
						this.ᜀ(bodyRegionCollection, A_1);
						num2 = 5;
						continue;
					}
					case 1:
					{
						BodyRegionCollection bodyRegionCollection;
						if (bodyRegionCollection.Count > 0)
						{
							goto IL_95;
						}
						goto IL_42;
					}
					case 2:
					{
						if (num >= 6)
						{
							num2 = 3;
							continue;
						}
						BodyRegionCollection bodyRegionCollection = (BodyRegionCollection)A_0.HeadersFooters[num].ChildObjects;
						num2 = 1;
						continue;
					}
					case 3:
						return;
					case 4:
						goto IL_AC;
					case 5:
						goto IL_42;
					case 6:
						goto IL_AC;
					}
					break;
					IL_42:
					num++;
					if (true)
					{
					}
					num2 = 4;
					continue;
					IL_95:
					num2 = 0;
					continue;
					IL_AC:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_95;
					default:
						if (false)
						{
						}
						num2 = 2;
						break;
					}
				}
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00051478 File Offset: 0x00050478
		private void ᜀ(BodyRegionCollection A_0, IRowsEnumerator A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IBodyRegion bodyRegion = null;
					int num = 0;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							ITable table;
							if (table != null)
							{
								num2 = 2;
								continue;
							}
							goto IL_12B;
						}
						case 1:
							goto IL_D6;
						case 2:
						{
							ITable table;
							this.ᜀ(table, A_1);
							goto IL_84;
						}
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_84;
							default:
								if (false)
								{
								}
								goto IL_D6;
							}
							break;
						case 4:
							return;
						case 5:
							goto IL_12B;
						case 6:
							goto IL_12B;
						case 7:
							if (bodyRegion is ITable)
							{
								num2 = 10;
								continue;
							}
							goto IL_12B;
						case 8:
							if (bodyRegion is IParagraph)
							{
								num2 = 11;
								continue;
							}
							num2 = 7;
							continue;
						case 9:
							if (num >= A_0.Count)
							{
								num2 = 4;
								continue;
							}
							bodyRegion = A_0[num];
							num2 = 8;
							continue;
						case 10:
						{
							ITable table = bodyRegion as ITable;
							num2 = 0;
							continue;
						}
						case 11:
						{
							Paragraph a_ = bodyRegion as Paragraph;
							this.ᜀ(a_, A_1);
							num2 = 6;
							continue;
						}
						}
						break;
						IL_84:
						num2 = 5;
						continue;
						IL_D6:
						if (true)
						{
						}
						num2 = 9;
						continue;
						IL_12B:
						num++;
						num2 = 1;
					}
				}
				return;
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x000515E8 File Offset: 0x000505E8
		private void ᜀ(Paragraph A_0, IRowsEnumerator A_1)
		{
			int a_ = 19;
			switch (0)
			{
			default:
				for (;;)
				{
					int num = 0;
					int count = A_0.Items.Count;
					int num2 = 19;
					for (;;)
					{
						Field field;
						int num3;
						switch (num2)
						{
						case 0:
							if (field.Type == FieldType.FieldNext)
							{
								num2 = 45;
								continue;
							}
							num2 = 35;
							continue;
						case 1:
							if (A_1 != null)
							{
								num2 = 52;
								continue;
							}
							goto IL_1D7;
						case 2:
						{
							MergeField mergeField;
							if (mergeField.Prefix.StartsWith(ClipboardData.b("へᙺᱼ᡾", a_)))
							{
								num2 = 7;
								continue;
							}
							num2 = 53;
							continue;
						}
						case 3:
							goto IL_34D;
						case 4:
							goto IL_34D;
						case 5:
							goto IL_334;
						case 6:
							A_1.NextRow();
							num2 = 5;
							continue;
						case 7:
						{
							MergeField mergeField;
							this.ᜀ(mergeField, A_0, A_1);
							num2 = 4;
							continue;
						}
						case 8:
							this.ᜀ(field as IfField, A_1);
							num2 = 49;
							continue;
						case 9:
							goto IL_34D;
						case 10:
							if (field.Type == FieldType.FieldIf)
							{
								num2 = 8;
								continue;
							}
							num2 = 38;
							continue;
						case 11:
							num2 = 2;
							continue;
						case 12:
							if (A_0[num] is spr\u248F)
							{
								num2 = 30;
								continue;
							}
							goto IL_34D;
						case 13:
							if (A_0[num] is Field)
							{
								num2 = 25;
								continue;
							}
							num2 = 17;
							continue;
						case 14:
							try
							{
								num2 = 4;
								for (;;)
								{
									switch (num2)
									{
									case 1:
										goto IL_620;
									case 2:
									{
										IEnumerator enumerator;
										if (!enumerator.MoveNext())
										{
											num2 = 1;
											continue;
										}
										TextBox textBox = (TextBox)enumerator.Current;
										this.ᜀ((BodyRegionCollection)textBox.Body.ChildObjects, A_1);
										num2 = 0;
										continue;
									}
									case 3:
										goto IL_629;
									case 4:
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_620;
										default:
											if (false)
											{
											}
											break;
										}
										break;
									}
									IL_603:
									num2 = 2;
									continue;
									goto IL_603;
									IL_620:
									num2 = 3;
								}
								IL_629:
								goto IL_34D;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator;
									IDisposable disposable = enumerator as IDisposable;
									num2 = 1;
									for (;;)
									{
										switch (num2)
										{
										case 0:
											disposable.Dispose();
											num2 = 2;
											continue;
										case 1:
											if (disposable != null)
											{
												num2 = 0;
												continue;
											}
											goto IL_676;
										case 2:
											goto IL_674;
										}
										break;
									}
								}
								IL_674:
								IL_676:;
							}
							goto IL_677;
						case 15:
							goto IL_34D;
						case 16:
							this.ᜁ(A_0);
							num2 = 48;
							continue;
						case 17:
							if (A_0[num] is TextBox)
							{
								num2 = 51;
								continue;
							}
							num2 = 12;
							continue;
						case 18:
							if (true)
							{
							}
							goto IL_1D7;
						case 19:
							goto IL_1F6;
						case 20:
							num2 = 31;
							continue;
						case 21:
							goto IL_34D;
						case 22:
							num2 = 29;
							continue;
						case 23:
							if (A_1 != null)
							{
								num2 = 42;
								continue;
							}
							goto IL_334;
						case 24:
							goto IL_34D;
						case 25:
							field = (A_0[num] as Field);
							num2 = 0;
							continue;
						case 26:
							num2 = 40;
							continue;
						case 27:
							if (field.Type == FieldType.FieldMergeSeq)
							{
								num2 = 34;
								continue;
							}
							goto IL_34D;
						case 28:
							if ((A_0[num] as spr\u248F).ᜎ() != null)
							{
								num2 = 46;
								continue;
							}
							goto IL_34D;
						case 29:
							if (A_1 != null)
							{
								num2 = 26;
								continue;
							}
							goto IL_4AF;
						case 30:
							num2 = 28;
							continue;
						case 31:
							if (this.ᜈ)
							{
								num2 = 16;
								continue;
							}
							return;
						case 32:
							if (!A_1.IsEnd)
							{
								num2 = 6;
								continue;
							}
							goto IL_334;
						case 33:
						{
							if (num >= count)
							{
								num2 = 20;
								continue;
							}
							MergeField mergeField = A_0[num] as MergeField;
							num2 = 39;
							continue;
						}
						case 34:
							goto IL_3CA;
						case 35:
							if (field.Type == FieldType.FieldNextIf)
							{
								num2 = 50;
								continue;
							}
							num2 = 10;
							continue;
						case 36:
							goto IL_4AF;
						case 37:
							num2 = 27;
							continue;
						case 38:
							if (field.Type != FieldType.FieldMergeRec)
							{
								num2 = 37;
								continue;
							}
							goto IL_3CA;
						case 39:
						{
							MergeField mergeField;
							if (mergeField != null)
							{
								num2 = 11;
								continue;
							}
							num2 = 13;
							continue;
						}
						case 40:
							if (!A_1.IsEnd)
							{
								num2 = 47;
								continue;
							}
							goto IL_4AF;
						case 41:
						{
							MergeField mergeField;
							this.ᜀ(mergeField, A_1);
							num2 = 24;
							continue;
						}
						case 42:
							num2 = 32;
							continue;
						case 43:
							if (field.ᜌ())
							{
								num2 = 22;
								continue;
							}
							goto IL_4AF;
						case 44:
							goto IL_1F6;
						case 45:
							num2 = 23;
							continue;
						case 46:
						{
							IEnumerator enumerator = (A_0[num] as spr\u248F).ᜎ().GetEnumerator();
							num2 = 14;
							continue;
						}
						case 47:
							A_1.NextRow();
							num2 = 36;
							continue;
						case 48:
							return;
						case 49:
							goto IL_34D;
						case 50:
							num2 = 43;
							continue;
						case 51:
							goto IL_677;
						case 52:
							num3 += A_1.CurrentRowIndex;
							num2 = 18;
							continue;
						case 53:
						{
							MergeField mergeField;
							if (!mergeField.ConvertedToText)
							{
								num2 = 41;
								continue;
							}
							goto IL_34D;
						}
						}
						break;
						IL_1D7:
						this.ᜀ(field, num3.ToString());
						num2 = 3;
						continue;
						IL_1F6:
						num2 = 33;
						continue;
						IL_334:
						this.ᜀ(field, true);
						num2 = 21;
						continue;
						IL_34D:
						num++;
						num2 = 44;
						continue;
						IL_3CA:
						num3 = 1;
						num2 = 1;
						continue;
						IL_4AF:
						A_0.Items.Remove(field);
						count = A_0.Items.Count;
						num--;
						num2 = 15;
						continue;
						IL_677:
						TextBox textBox2 = A_0[num] as TextBox;
						this.ᜀ((BodyRegionCollection)textBox2.Body.ChildObjects, A_1);
						num2 = 9;
					}
				}
				return;
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00051CF4 File Offset: 0x00050CF4
		private void ᜀ(ITable A_0, IRowsEnumerator A_1)
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
				for (;;)
				{
					TableRow tableRow = null;
					int num = 0;
					int count = A_0.Rows.Count;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_B0;
						case 1:
						{
							int num3;
							int count2;
							if (num3 >= count2)
							{
								num2 = 7;
								continue;
							}
							TableCell tableCell = tableRow.Cells[num3];
							this.ᜀ((BodyRegionCollection)tableCell.ChildObjects, A_1);
							num3++;
							num2 = 4;
							continue;
						}
						case 2:
							goto IL_B0;
						case 3:
							return;
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
								goto IL_6B;
							}
							break;
						case 5:
						{
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							tableRow = A_0.Rows[num];
							int num3 = 0;
							int count2 = tableRow.Cells.Count;
							num2 = 6;
							continue;
						}
						case 6:
							goto IL_6B;
						case 7:
							num++;
							num2 = 0;
							continue;
						}
						break;
						IL_6B:
						num2 = 1;
						continue;
						IL_B0:
						num2 = 5;
					}
				}
				return;
			}
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00051E2C File Offset: 0x00050E2C
		private void ᜀ(Field A_0, string A_1)
		{
			TextRange textRange;
			for (;;)
			{
				textRange = new TextRange(this.Document);
				Paragraph ownerParagraph = A_0.OwnerParagraph;
				int index = A_0.ឯ();
				ownerParagraph.Items.Remove(A_0);
				ownerParagraph.Items.Insert(index, textRange);
				textRange.CharacterFormat.ImportContainer(A_0.CharacterFormat);
				textRange.CharacterFormat.ᜃ(A_0.CharacterFormat);
				int num = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						if (textRange.CharacterFormat.Sprms.ᜄ(2133))
						{
							num = 2;
							continue;
						}
						goto IL_E5;
					case 1:
						goto IL_E3;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E3;
						default:
							if (false)
							{
							}
							textRange.CharacterFormat.Sprms.ᜆ(2133);
							num = 1;
							continue;
						}
						break;
					}
					break;
				}
			}
			IL_E3:
			IL_E5:
			textRange.Text = A_1;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00051F28 File Offset: 0x00050F28
		private void ᜁ()
		{
			for (;;)
			{
				int num = 0;
				int num2 = 6;
				for (;;)
				{
					Field field;
					switch (num2)
					{
					case 0:
						goto IL_38;
					case 1:
						goto IL_A4;
					case 2:
						if (num >= this.Document.Fields.Count)
						{
							num2 = 4;
							continue;
						}
						field = this.Document.Fields.ᜀ(num);
						num2 = 5;
						continue;
					case 3:
						goto IL_50;
					case 4:
						goto IL_CD;
					case 5:
						if (field.Type != FieldType.FieldMergeRec)
						{
							goto IL_97;
						}
						goto IL_50;
					case 6:
						goto IL_A4;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_97;
						default:
							if (false)
							{
							}
							if (field.Type == FieldType.FieldMergeSeq)
							{
								num2 = 3;
								continue;
							}
							goto IL_38;
						}
						break;
					case 8:
						num2 = 7;
						continue;
					}
					break;
					IL_38:
					num++;
					num2 = 1;
					continue;
					IL_50:
					this.ᜀ(field, this.\u1717.ToString());
					num--;
					num2 = 0;
					continue;
					IL_97:
					num2 = 8;
					continue;
					IL_A4:
					num2 = 2;
				}
			}
			IL_CD:
			if (true)
			{
			}
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00052050 File Offset: 0x00051050
		private void ᜀ(IMergeField A_0, IRowsEnumerator A_1)
		{
			int a_ = 14;
			switch (0)
			{
			default:
			{
				int num = 10;
				for (;;)
				{
					string text;
					object obj;
					bool flag;
					switch (num)
					{
					case 0:
						obj = A_1.GetCellValue(text);
						num = 12;
						continue;
					case 1:
					{
						int num2;
						int num3;
						if (num2 >= num3)
						{
							num = 8;
							continue;
						}
						text = A_1.ColumnNames[num2];
						string a = A_0.FieldName.ToUpper();
						string text2 = text.ToUpper();
						num = 20;
						continue;
					}
					case 2:
						num = 23;
						continue;
					case 3:
						goto IL_A0;
					case 4:
						goto IL_F0;
					case 5:
						if (!flag)
						{
							num = 22;
							continue;
						}
						goto IL_168;
					case 6:
					{
						MergeFieldEventArgs mergeFieldEventArgs = this.SendMergeField(A_0, obj, A_1);
						A_0.Text = mergeFieldEventArgs.Text;
						(A_0 as MergeField).ConvertedToText = true;
						flag = true;
						num = 4;
						continue;
					}
					case 7:
						return;
					case 8:
						goto IL_A5;
					case 9:
					{
						int num2 = 0;
						int num3 = A_1.ColumnNames.Length;
						num = 11;
						continue;
					}
					case 11:
						goto IL_143;
					case 12:
						goto IL_1CD;
					case 13:
						goto IL_2C1;
					case 14:
						if (obj == null)
						{
							num = 9;
							continue;
						}
						goto IL_A5;
					case 15:
						if (this.ᜅ)
						{
							num = 21;
							continue;
						}
						return;
					case 16:
						if (text != null)
						{
							num = 0;
							continue;
						}
						goto IL_1CD;
					case 17:
						goto IL_143;
					case 18:
						if (obj != null)
						{
							num = 6;
							continue;
						}
						goto IL_F0;
					case 19:
						goto IL_A5;
					case 20:
					{
						string a;
						string text2;
						if (!(a == text2))
						{
							num = 2;
							continue;
						}
						goto IL_2C1;
					}
					case 21:
						IL_210:
						goto IL_168;
					case 22:
						num = 15;
						continue;
					case 23:
					{
						string a;
						string text2;
						if (a == ClipboardData.b("噳", a_) + text2 + ClipboardData.b("噳", a_))
						{
							num = 13;
							continue;
						}
						int num2;
						num2++;
						num = 17;
						continue;
					}
					}
					if (A_1 == null)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					text = null;
					flag = false;
					obj = null;
					text = this.ᜀ(A_0.FieldName);
					num = 16;
					continue;
					IL_A5:
					num = 18;
					continue;
					IL_F0:
					num = 5;
					continue;
					IL_143:
					num = 1;
					continue;
					IL_168:
					this.ᜀ(A_0, true);
					num = 7;
					continue;
					IL_2C1:
					obj = A_1.GetCellValue(text);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_210;
					default:
						if (false)
						{
						}
						num = 19;
						continue;
					}
					IL_1CD:
					num = 14;
				}
				IL_A0:
				this.ᜀ(A_0);
				return;
			}
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00052354 File Offset: 0x00051354
		private void ᜀ(IfField A_0, IRowsEnumerator A_1)
		{
			switch (0)
			{
			default:
			{
				int num = 3;
				for (;;)
				{
					int num4;
					switch (num)
					{
					case 0:
						goto IL_128;
					case 1:
						goto IL_18F;
					case 2:
					{
						int num2;
						int num3;
						if (num2 >= num3)
						{
							num = 4;
							continue;
						}
						string text = A_1.ColumnNames[num2];
						string b = text.ToUpper();
						string a = string.Empty;
						spr\u23E3 spr_u23E = null;
						num4 = 0;
						int count = A_0.MergeFields.Count;
						num = 1;
						continue;
					}
					case 4:
						return;
					case 5:
						goto IL_18F;
					case 6:
						goto IL_128;
					case 7:
					{
						string text;
						object cellValue = A_1.GetCellValue(text);
						spr\u23E3 spr_u23E;
						spr_u23E.ᜀ(cellValue.ToString());
						num = 13;
						continue;
					}
					case 8:
					{
						string b;
						string a;
						if (a == b)
						{
							num = 7;
							continue;
						}
						goto IL_7F;
					}
					case 9:
					{
						int count;
						if (num4 < count)
						{
							spr\u23E3 spr_u23E = A_0.MergeFields[num4];
							num = 11;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_123;
						default:
							if (false)
							{
							}
							num = 14;
							continue;
						}
						break;
					}
					case 10:
						return;
					case 11:
					{
						spr\u23E3 spr_u23E;
						if (spr_u23E.ᜂ() != null)
						{
							num = 15;
							continue;
						}
						goto IL_7F;
					}
					case 12:
					{
						if (A_1 == null)
						{
							num = 10;
							continue;
						}
						string text = null;
						int num2 = 0;
						int num3 = A_1.ColumnNames.Length;
						num = 0;
						continue;
					}
					case 13:
						goto IL_123;
					case 14:
					{
						int num2;
						num2++;
						num = 6;
						continue;
					}
					case 15:
					{
						spr\u23E3 spr_u23E;
						string a = spr_u23E.ᜂ().ToUpper();
						if (true)
						{
						}
						num = 8;
						continue;
					}
					case 16:
						return;
					}
					if (A_0.MergeFields.Count == 0)
					{
						num = 16;
						continue;
					}
					num = 12;
					continue;
					IL_7F:
					num4++;
					num = 5;
					continue;
					IL_123:
					goto IL_7F;
					IL_128:
					num = 2;
					continue;
					IL_18F:
					num = 9;
				}
				return;
			}
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00052588 File Offset: 0x00051588
		private void ᜀ(IMergeField A_0, IParagraph A_1, IRowsEnumerator A_2)
		{
			switch (0)
			{
			default:
			{
				int num = 20;
				MergeImageFieldEventArgs a_;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_213;
					default:
					{
						if (false)
						{
						}
						string text;
						object obj;
						bool flag;
						string fieldName;
						switch (num)
						{
						case 0:
							obj = A_2.GetCellValue(text);
							num = 16;
							continue;
						case 1:
							goto IL_13C;
						case 2:
							if (!flag)
							{
								num = 3;
								continue;
							}
							goto IL_247;
						case 3:
							num = 6;
							continue;
						case 4:
						{
							Image image;
							if (image != null)
							{
								num = 23;
								continue;
							}
							goto IL_13C;
						}
						case 5:
							goto IL_31B;
						case 6:
							if (this.ᜅ)
							{
								num = 29;
								continue;
							}
							return;
						case 7:
							if (this.\u171A == null)
							{
								num = 9;
								continue;
							}
							goto IL_163;
						case 8:
						{
							int num2;
							int num3;
							if (num2 >= num3)
							{
								num = 27;
								continue;
							}
							text = A_2.ColumnNames[num2];
							num = 14;
							continue;
						}
						case 9:
							goto IL_2A4;
						case 10:
							goto IL_227;
						case 11:
						{
							Image image = this.ᜀ(obj);
							num = 4;
							continue;
						}
						case 12:
							obj = A_2.GetCellValue(text);
							num = 24;
							continue;
						case 13:
							goto IL_137;
						case 14:
						{
							if (text.ToUpper() == fieldName.ToUpper())
							{
								num = 0;
								continue;
							}
							int num2;
							num2++;
							num = 5;
							continue;
						}
						case 15:
							if (this.\u171A != null)
							{
								num = 13;
								continue;
							}
							goto IL_2A6;
						case 16:
							goto IL_185;
						case 17:
							num = 7;
							continue;
						case 18:
							if (A_2 == null)
							{
								num = 22;
								continue;
							}
							goto IL_2A6;
						case 19:
							if (obj == null)
							{
								num = 26;
								continue;
							}
							goto IL_185;
						case 21:
							if (text != null)
							{
								num = 12;
								continue;
							}
							goto IL_260;
						case 22:
							num = 15;
							continue;
						case 23:
						{
							Image image;
							obj = image;
							num = 1;
							continue;
						}
						case 24:
							goto IL_260;
						case 25:
							if (obj != null)
							{
								num = 11;
								continue;
							}
							goto IL_227;
						case 26:
						{
							int num2 = 0;
							int num3 = A_2.ColumnNames.Length;
							num = 30;
							continue;
						}
						case 27:
							goto IL_185;
						case 28:
							goto IL_25B;
						case 29:
							if (true)
							{
							}
							goto IL_247;
						case 30:
							goto IL_31B;
						}
						if (A_2 == null)
						{
							num = 17;
							break;
						}
						goto IL_163;
						IL_13C:
						a_ = this.SendMergeImageField(A_0, obj, A_2);
						this.ᜀ(A_0, A_1, a_);
						flag = true;
						num = 10;
						break;
						IL_163:
						num = 18;
						break;
						IL_185:
						num = 25;
						break;
						IL_227:
						num = 2;
						break;
						IL_247:
						this.ᜀ(A_0, true);
						num = 28;
						break;
						IL_260:
						num = 19;
						break;
						IL_2A6:
						flag = false;
						fieldName = A_0.FieldName;
						text = null;
						obj = null;
						text = this.ᜀ(fieldName);
						num = 21;
						break;
						IL_31B:
						num = 8;
						break;
					}
					}
				}
				IL_137:
				IL_213:
				a_ = this.SendMergeImageField(A_0, null, A_2);
				this.ᜀ(A_0, A_1, a_);
				return;
				IL_25B:
				return;
				IL_2A4:
				this.ᜀ(A_0);
				return;
			}
			}
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00052904 File Offset: 0x00051904
		private void ᜀ(IMergeField A_0, IParagraph A_1, MergeImageFieldEventArgs A_2)
		{
			int num = 1;
			for (;;)
			{
				IPicture picture;
				switch (num)
				{
				case 0:
					goto IL_3F;
				case 2:
				{
					int index = A_1.Items.IndexOf(A_0);
					A_1.Items.RemoveAt(index);
					picture = (IPicture)this.Document.CreateParagraphItem(ParagraphItemType.Picture);
					A_1.Items.Insert(index, picture);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BE;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				}
				case 3:
					if (!A_2.Skip)
					{
						num = 2;
						continue;
					}
					return;
				case 4:
					if (A_2.Image != null)
					{
						num = 5;
						continue;
					}
					return;
				case 5:
					goto IL_BE;
				case 6:
					return;
				}
				if (A_2.UseText)
				{
					num = 0;
					continue;
				}
				num = 3;
				continue;
				IL_BE:
				picture.LoadImage(A_2.Image);
				num = 6;
			}
			IL_3F:
			if (true)
			{
			}
			A_0.Text = A_2.Text;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00052A24 File Offset: 0x00051A24
		private void ᜀ(IMergeField A_0)
		{
			switch (0)
			{
			default:
			{
				int num = 13;
				MergeFieldEventArgs mergeFieldEventArgs;
				for (;;)
				{
					int num2;
					string text;
					switch (num)
					{
					case 0:
					{
						int num3;
						num2 = num3;
						num = 6;
						continue;
					}
					case 1:
						mergeFieldEventArgs = new MergeFieldEventArgs(this.Document, "", num2, A_0, this.ᜄ[num2]);
						goto IL_32E;
					case 2:
						if (this.\u1719 != null)
						{
							num = 17;
							continue;
						}
						goto IL_246;
					case 3:
						if (text != null)
						{
							num = 25;
							continue;
						}
						goto IL_2EE;
					case 4:
						goto IL_2EE;
					case 5:
						goto IL_274;
					case 6:
						goto IL_B7;
					case 7:
						goto IL_2EE;
					case 8:
					{
						int num3 = 0;
						num = 29;
						continue;
					}
					case 9:
						goto IL_17B;
					case 10:
						goto IL_279;
					case 11:
					{
						int num3;
						if (num3 >= this.ᜃ.Length)
						{
							num = 14;
							continue;
						}
						num = 19;
						continue;
					}
					case 12:
						if (this.ᜄ == null)
						{
							num = 21;
							continue;
						}
						goto IL_14C;
					case 14:
						goto IL_B7;
					case 15:
						if (num2 != -1)
						{
							num = 1;
							continue;
						}
						num = 23;
						continue;
					case 16:
					{
						int num4;
						if (num4 >= this.ᜃ.Length)
						{
							num = 4;
							continue;
						}
						num = 22;
						continue;
					}
					case 17:
						this.\u1719(this, mergeFieldEventArgs);
						num = 27;
						continue;
					case 18:
					{
						if (true)
						{
						}
						int num4;
						num2 = num4;
						num = 7;
						continue;
					}
					case 19:
					{
						int num3;
						if (this.ᜃ[num3].ToUpper() == A_0.FieldName.ToUpper())
						{
							num = 0;
							continue;
						}
						num3++;
						num = 9;
						continue;
					}
					case 20:
						goto IL_279;
					case 21:
						goto IL_2C4;
					case 22:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_32E;
						default:
						{
							if (false)
							{
							}
							int num4;
							if (this.ᜃ[num4].ToUpper() == text.ToUpper())
							{
								num = 18;
								continue;
							}
							num4++;
							num = 10;
							continue;
						}
						}
						break;
					case 23:
						if (this.ᜅ)
						{
							num = 24;
							continue;
						}
						return;
					case 24:
						this.ᜀ(A_0, true);
						num = 5;
						continue;
					case 25:
					{
						int num4 = 0;
						num = 20;
						continue;
					}
					case 26:
						num = 12;
						continue;
					case 27:
						goto IL_147;
					case 28:
						if (num2 == -1)
						{
							num = 8;
							continue;
						}
						goto IL_B7;
					case 29:
						goto IL_17B;
					}
					if (this.ᜅ)
					{
						num = 26;
						continue;
					}
					goto IL_14C;
					IL_B7:
					num = 15;
					continue;
					IL_14C:
					num2 = -1;
					text = this.ᜀ(A_0.FieldName);
					num = 3;
					continue;
					IL_17B:
					num = 11;
					continue;
					IL_279:
					num = 16;
					continue;
					IL_2EE:
					num = 28;
					continue;
					IL_32E:
					num = 2;
				}
				IL_147:
				IL_246:
				A_0.Text = mergeFieldEventArgs.Text;
				(A_0 as MergeField).ConvertedToText = true;
				return;
				IL_274:
				return;
				IL_2C4:
				A_0.Text = "";
				(A_0 as MergeField).ConvertedToText = true;
				return;
			}
			}
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00052DC4 File Offset: 0x00051DC4
		private void ᜁ(Document A_0)
		{
			int a_ = 17;
			if (A_0 != null)
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
					this.ᜂ.Clear();
					A_0.Sections.ᜀ(this.ᜂ);
					return;
				}
			}
			throw new ArgumentNullException(ClipboardData.b("፶ᙸ᡺ࡼቾ", a_));
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00052E40 File Offset: 0x00051E40
		private void ᜀ(Document A_0)
		{
			int a_ = 4;
			int num = 1;
			for (;;)
			{
				if (true)
				{
				}
				int num2;
				int count;
				switch (num)
				{
				case 0:
					goto IL_44;
				case 2:
					return;
				case 3:
					goto IL_C0;
				case 4:
					goto IL_C0;
				case 5:
				{
					if (num2 >= count)
					{
						num = 2;
						continue;
					}
					ISection section = this.ᜂ[num2];
					A_0.Sections.Add(section.Clone());
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				}
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				num2 = 0;
				count = this.ᜂ.Count;
				num = 4;
				continue;
				IL_C0:
				num = 5;
			}
			IL_44:
			throw new ArgumentNullException(ClipboardData.b("๩ͫ൭կάᅳᡵ౷", a_));
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00052F38 File Offset: 0x00051F38
		private Bitmap ᜀ(object A_0)
		{
			int num = 2;
			for (;;)
			{
				MemoryStream stream;
				switch (num)
				{
				case 0:
					try
					{
						return new Bitmap(stream);
					}
					catch
					{
						return null;
					}
					goto IL_74;
				case 1:
					goto IL_74;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
				{
					Bitmap result;
					return result;
				}
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					if (A_0.GetType() == typeof(byte[]))
					{
						num = 1;
						continue;
					}
					goto IL_8A;
				}
				IL_74:
				stream = new MemoryStream((byte[])A_0);
				num = 0;
			}
			IL_8A:
			return null;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00052FE4 File Offset: 0x00051FE4
		private void ᜀ(List<string> A_0, BodyRegion A_1, string A_2)
		{
			switch (0)
			{
			default:
			{
				int num = 2;
				for (;;)
				{
					int num4;
					int count3;
					MergeField mergeField;
					int num6;
					int count5;
					TextBox textBox;
					switch (num)
					{
					case 0:
						num = 15;
						continue;
					case 1:
					{
						int num2;
						int count;
						if (num2 >= count)
						{
							num = 22;
							continue;
						}
						TableRow tableRow;
						TableCell tableCell = tableRow.Cells[num2];
						int num3 = 0;
						int count2 = tableCell.Paragraphs.Count;
						num = 14;
						continue;
					}
					case 3:
						num = 6;
						continue;
					case 4:
						num = 23;
						continue;
					case 5:
					{
						int num2;
						num2++;
						num = 21;
						continue;
					}
					case 6:
						if (!this.ᜆ)
						{
							num = 4;
							continue;
						}
						goto IL_123;
					case 7:
					{
						if (num4 >= count3)
						{
							num = 17;
							continue;
						}
						ParagraphBase paragraphBase = (A_1 as Paragraph)[num4];
						num = 8;
						continue;
					}
					case 8:
					{
						ParagraphBase paragraphBase;
						if (paragraphBase is MergeField)
						{
							num = 37;
							continue;
						}
						num = 16;
						continue;
					}
					case 9:
						return;
					case 10:
						goto IL_292;
					case 11:
						if (mergeField.FieldName == A_2)
						{
							num = 3;
							continue;
						}
						num = 20;
						continue;
					case 12:
					{
						int num3;
						int count2;
						if (num3 >= count2)
						{
							num = 5;
							continue;
						}
						TableCell tableCell;
						BodyRegion a_ = tableCell.Items[num3];
						this.ᜀ(A_0, a_, A_2);
						num3++;
						num = 42;
						continue;
					}
					case 13:
						goto IL_498;
					case 14:
						goto IL_344;
					case 15:
						if (MailMerge.ᜁ(mergeField))
						{
							num = 32;
							continue;
						}
						goto IL_530;
					case 16:
					{
						ParagraphBase paragraphBase;
						if (paragraphBase is TextBox)
						{
							num = 44;
							continue;
						}
						goto IL_530;
					}
					case 17:
						return;
					case 18:
						goto IL_292;
					case 19:
					{
						int num5;
						int count4;
						if (num5 >= count4)
						{
							num = 9;
							continue;
						}
						Table table;
						TableRow tableRow = table.Rows[num5];
						int num2 = 0;
						int count = tableRow.Cells.Count;
						num = 29;
						continue;
					}
					case 20:
						if (A_2 != null)
						{
							num = 35;
							continue;
						}
						goto IL_498;
					case 21:
						goto IL_31E;
					case 22:
					{
						int num5;
						num5++;
						num = 31;
						continue;
					}
					case 23:
						if (MailMerge.ᜂ(mergeField))
						{
							num = 36;
							continue;
						}
						goto IL_123;
					case 24:
						if (num6 >= count5)
						{
							num = 43;
							continue;
						}
						this.ᜀ(A_0, textBox.Body.Items[num6], A_2);
						num6++;
						num = 38;
						continue;
					case 25:
						num = 40;
						continue;
					case 26:
						goto IL_530;
					case 27:
						if (this.ᜇ)
						{
							goto IL_530;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_38F;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 28:
					{
						Table table = A_1 as Table;
						TableRow tableRow = null;
						TableCell tableCell = null;
						int num5 = 0;
						int count4 = table.Rows.Count;
						num = 30;
						continue;
					}
					case 29:
						goto IL_31E;
					case 30:
						goto IL_443;
					case 31:
						goto IL_443;
					case 32:
						this.ᜇ = true;
						this.ᜆ = false;
						num = 26;
						continue;
					case 33:
						goto IL_530;
					case 34:
						goto IL_123;
					case 35:
						num = 41;
						continue;
					case 36:
						this.ᜆ = true;
						this.ᜇ = false;
						num = 34;
						continue;
					case 37:
					{
						ParagraphBase paragraphBase;
						mergeField = (paragraphBase as MergeField);
						num = 11;
						continue;
					}
					case 38:
						goto IL_207;
					case 39:
						goto IL_207;
					case 40:
						if (!this.ᜇ)
						{
							num = 13;
							continue;
						}
						goto IL_530;
					case 41:
						if (this.ᜆ)
						{
							num = 25;
							continue;
						}
						goto IL_530;
					case 42:
						goto IL_344;
					case 43:
						goto IL_530;
					case 44:
						goto IL_38F;
					}
					if (true)
					{
					}
					if (A_1 is ITable)
					{
						num = 28;
						continue;
					}
					num4 = 0;
					count3 = (A_1 as Paragraph).Items.Count;
					num = 18;
					continue;
					IL_123:
					num = 27;
					continue;
					IL_207:
					num = 24;
					continue;
					IL_292:
					num = 7;
					continue;
					IL_31E:
					num = 1;
					continue;
					IL_344:
					num = 12;
					continue;
					IL_38F:
					textBox = (TextBox)(A_1 as Paragraph)[num4];
					count5 = textBox.Body.Items.Count;
					num6 = 0;
					num = 39;
					continue;
					IL_443:
					num = 19;
					continue;
					IL_498:
					A_0.Add(mergeField.FieldName);
					num = 33;
					continue;
					IL_530:
					num4++;
					num = 10;
				}
				return;
			}
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00053560 File Offset: 0x00052560
		private static bool ᜂ(MergeField A_0)
		{
			int a_ = 12;
			string prefix = A_0.Prefix;
			if (prefix == ClipboardData.b("♱ᕳᑵᑷό⽻੽", a_))
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
					return true;
				}
			}
			if (true)
			{
			}
			return prefix == ClipboardData.b("㕱ٳ᥵൷੹⽻੽", a_);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x000535DC File Offset: 0x000525DC
		private static bool ᜁ(MergeField A_0)
		{
			int a_ = 1;
			string prefix = A_0.Prefix;
			if (prefix == ClipboardData.b("㍦ࡨ४Ŭ੮㑰ᵲᅴ", a_))
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
					return true;
				}
			}
			return prefix == ClipboardData.b("⁦᭨ѪᡬὮ㑰ᵲᅴ", a_);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00053658 File Offset: 0x00052658
		private static bool ᜀ(MergeField A_0)
		{
			int a_ = 18;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			string prefix = A_0.Prefix;
			return prefix == ClipboardData.b("ⱷ᭹ṻች잁", a_);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x000536B8 File Offset: 0x000526B8
		private bool ᜀ(IRowsEnumerator A_0)
		{
			switch (0)
			{
			default:
			{
				int num = 14;
				for (;;)
				{
					int num2;
					MailMerge.ᜁ ᜁ;
					switch (num)
					{
					case 0:
						num = 10;
						continue;
					case 1:
						if (this.ᜈ)
						{
							num = 26;
							continue;
						}
						return false;
					case 2:
						goto IL_212;
					case 3:
					{
						int num3;
						if (num2 > num3)
						{
							num = 12;
							continue;
						}
						num = 8;
						continue;
					}
					case 4:
						goto IL_237;
					case 5:
						this.ᜁ(ᜁ.ᜄ().TextBody.Items);
						num = 21;
						continue;
					case 6:
						if (this.ᜅ)
						{
							num = 5;
							continue;
						}
						goto IL_2D5;
					case 7:
						this.ᜁ(ᜁ.ᜅ().ᜀ.Rows[num2]);
						num = 2;
						continue;
					case 8:
					{
						int num4;
						if (ᜁ.ᜅ().ᜀ.Rows.Count > num4)
						{
							num = 15;
							continue;
						}
						goto IL_1AA;
					}
					case 9:
						if (ᜁ.ᜅ() != null)
						{
							num = 23;
							continue;
						}
						return false;
					case 10:
						if (!this.ᜈ)
						{
							num = 22;
							continue;
						}
						goto IL_1E3;
					case 11:
						if (this.ᜈ)
						{
							num = 13;
							continue;
						}
						goto IL_1AA;
					case 12:
						goto IL_256;
					case 13:
						if (true)
						{
						}
						this.ᜀ(ᜁ.ᜅ().ᜀ.Rows[num2]);
						num = 24;
						continue;
					case 15:
						num = 19;
						continue;
					case 16:
						num = 6;
						continue;
					case 17:
						goto IL_1DE;
					case 18:
						if (!this.ᜅ)
						{
							goto IL_164;
						}
						goto IL_1E3;
					case 19:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_164;
						default:
							if (false)
							{
							}
							if (this.ᜅ)
							{
								num = 7;
								continue;
							}
							goto IL_212;
						}
						break;
					case 20:
						goto IL_237;
					case 21:
						goto IL_2D5;
					case 22:
						return true;
					case 23:
					{
						int num4 = ᜁ.ᜅ().ᜁ;
						int num3 = ᜁ.ᜅ().ᜂ;
						num2 = num4;
						num = 20;
						continue;
					}
					case 24:
						goto IL_1AA;
					case 25:
						return true;
					case 26:
						this.ᜀ(ᜁ.ᜄ().TextBody.Items);
						num = 17;
						continue;
					case 27:
						if (ᜁ.ᜄ() != null)
						{
							num = 16;
							continue;
						}
						num = 9;
						continue;
					}
					if (A_0.RowsCount > 0)
					{
						num = 25;
						continue;
					}
					num = 18;
					continue;
					IL_164:
					num = 0;
					continue;
					IL_1AA:
					num2++;
					num = 4;
					continue;
					IL_1E3:
					ᜁ = this.ᜁ;
					num = 27;
					continue;
					IL_212:
					num = 11;
					continue;
					IL_237:
					num = 3;
					continue;
					IL_2D5:
					num = 1;
				}
				return true;
				IL_1DE:
				IL_256:
				return false;
			}
			}
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00053A1C File Offset: 0x00052A1C
		private void ᜀ(IWSectionCollection A_0)
		{
			for (;;)
			{
				int num = 0;
				int count = A_0.Count;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						this.ᜀ(A_0[num], null);
						num++;
						if (true)
						{
						}
						num2 = 3;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_47;
						default:
							if (false)
							{
							}
							goto IL_47;
						}
						break;
					case 2:
						return;
					case 3:
						goto IL_47;
					}
					break;
					IL_47:
					num2 = 0;
				}
			}
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00053AB8 File Offset: 0x00052AB8
		private void ᜁ(TableRow A_0)
		{
			for (;;)
			{
				int num = 0;
				int count = A_0.Cells.Count;
				int num2 = 3;
				for (;;)
				{
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						return;
					case 1:
					{
						if (num >= count)
						{
							num2 = 0;
							continue;
						}
						TableCell tableCell = A_0.Cells[num];
						this.ᜁ(tableCell.Items);
						num++;
						num2 = 2;
						continue;
					}
					case 2:
						goto IL_56;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_56;
						default:
							if (false)
							{
							}
							goto IL_56;
						}
						break;
					}
					break;
					IL_56:
					num2 = 1;
				}
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00053B68 File Offset: 0x00052B68
		private void ᜁ(BodyRegionCollection A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					BodyRegion bodyRegion = null;
					int num = 0;
					int count = A_0.Count;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_E8;
						case 1:
							goto IL_E8;
						case 2:
							goto IL_133;
						case 3:
							this.ᜂ(bodyRegion as Paragraph);
							num2 = 2;
							continue;
						case 4:
						{
							Table table = bodyRegion as Table;
							int num3 = 0;
							int count2 = table.Rows.Count;
							num2 = 12;
							continue;
						}
						case 5:
							goto IL_64;
						case 6:
							return;
						case 7:
							if (num >= count)
							{
								num2 = 6;
								continue;
							}
							bodyRegion = A_0[num];
							num2 = 9;
							continue;
						case 8:
							if (bodyRegion is Table)
							{
								if (true)
								{
								}
								num2 = 4;
								continue;
							}
							goto IL_133;
						case 9:
							if (bodyRegion is Paragraph)
							{
								num2 = 3;
								continue;
							}
							num2 = 8;
							continue;
						case 10:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_C0;
							default:
								if (false)
								{
								}
								goto IL_133;
							}
							break;
						case 11:
						{
							int num3;
							int count2;
							if (num3 >= count2)
							{
								num2 = 10;
								continue;
							}
							Table table;
							this.ᜁ(table.Rows[num3]);
							num3++;
							goto IL_C0;
						}
						case 12:
							goto IL_64;
						}
						break;
						IL_64:
						num2 = 11;
						continue;
						IL_C0:
						num2 = 5;
						continue;
						IL_E8:
						num2 = 7;
						continue;
						IL_133:
						num++;
						num2 = 0;
					}
				}
				return;
			}
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00053D0C File Offset: 0x00052D0C
		private void ᜂ(Paragraph A_0)
		{
			for (;;)
			{
				Field field = null;
				int num = 0;
				int count = A_0.Items.Count;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_121;
					case 1:
						if (true)
						{
						}
						if (field.Type != FieldType.FieldMergeField)
						{
							num2 = 2;
							continue;
						}
						goto IL_17F;
					case 2:
						num2 = 8;
						continue;
					case 3:
						if (A_0.Items[num] is Field)
						{
							num2 = 11;
							continue;
						}
						num2 = 5;
						continue;
					case 4:
						if (num >= count)
						{
							num2 = 9;
							continue;
						}
						num2 = 3;
						continue;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E0;
						default:
							if (false)
							{
							}
							if (A_0.Items[num] is TextBox)
							{
								num2 = 6;
								continue;
							}
							goto IL_16E;
						}
						break;
					case 6:
						goto IL_E0;
					case 7:
						goto IL_17F;
					case 8:
						if (field.Type == FieldType.FieldNext)
						{
							num2 = 7;
							continue;
						}
						goto IL_16E;
					case 9:
						return;
					case 10:
						goto IL_16E;
					case 11:
						field = (A_0.Items[num] as Field);
						num2 = 1;
						continue;
					case 12:
						goto IL_16E;
					case 13:
						goto IL_121;
					}
					break;
					IL_E0:
					this.ᜁ((A_0.Items[num] as TextBox).Body.Items);
					num2 = 10;
					continue;
					IL_121:
					num2 = 4;
					continue;
					IL_16E:
					num++;
					num2 = 13;
					continue;
					IL_17F:
					this.ᜀ(field, true);
					num2 = 12;
				}
			}
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00053ED0 File Offset: 0x00052ED0
		private void ᜀ(IField A_0, bool A_1)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					num = 7;
					continue;
				case 2:
					num = 9;
					continue;
				case 3:
					goto IL_14D;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B4;
					default:
						if (false)
						{
						}
						if (A_0.Type == FieldType.FieldNext)
						{
							num = 2;
							continue;
						}
						goto IL_93;
					}
					break;
				case 5:
					goto IL_18F;
				case 6:
					if (!MailMerge.ᜂ(A_0 as MergeField))
					{
						num = 1;
						continue;
					}
					goto IL_14D;
				case 7:
					if (!MailMerge.ᜁ(A_0 as MergeField))
					{
						num = 13;
						continue;
					}
					goto IL_14D;
				case 8:
					if (A_0 is MergeField)
					{
						num = 11;
						continue;
					}
					return;
				case 9:
					if (A_1)
					{
						num = 5;
						continue;
					}
					goto IL_93;
				case 10:
					goto IL_164;
				case 11:
					goto IL_B4;
				case 12:
					if (A_1)
					{
						num = 3;
						continue;
					}
					return;
				case 13:
					num = 12;
					continue;
				case 14:
					return;
				}
				if ((A_0 as Field).ConvertedToText)
				{
					if (true)
					{
					}
					num = 14;
					continue;
				}
				A_0.Text = string.Empty;
				num = 4;
				continue;
				IL_93:
				num = 8;
				continue;
				IL_B4:
				num = 6;
				continue;
				IL_14D:
				(A_0 as MergeField).ConvertedToText = true;
				num = 10;
			}
			return;
			IL_164:
			return;
			IL_18F:
			(A_0 as Field).ConvertedToText = true;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00054070 File Offset: 0x00053070
		private void ᜁ(Paragraph A_0)
		{
			int num = 10;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_82;
				case 1:
					num = 3;
					continue;
				case 2:
					num = 6;
					continue;
				case 3:
					if (A_0.Items[0] is MergeField)
					{
						num = 2;
						continue;
					}
					return;
				case 4:
				{
					TableCell tableCell = A_0.Owner as TableCell;
					num = 5;
					continue;
				}
				case 5:
				{
					TableCell tableCell;
					if (tableCell != null)
					{
						num = 0;
						continue;
					}
					goto IL_B2;
				}
				case 6:
					if (this.ᜀ(A_0))
					{
						num = 4;
						continue;
					}
					return;
				case 7:
					goto IL_13C;
				case 8:
				{
					TableCell tableCell;
					if (tableCell.ChildObjects.Count != 1)
					{
						goto IL_B2;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_82;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				}
				case 9:
					goto IL_D6;
				}
				if (A_0.Items.Count > 0)
				{
					num = 1;
					continue;
				}
				break;
				IL_82:
				num = 8;
				continue;
				IL_B2:
				if (true)
				{
				}
				A_0.RemoveEmpty = true;
				num = 9;
			}
			IL_D6:
			return;
			IL_13C:
			A_0.ChildObjects.Clear();
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x000541C0 File Offset: 0x000531C0
		private bool ᜀ(Paragraph A_0)
		{
			switch (0)
			{
			default:
			{
				bool result;
				for (;;)
				{
					result = true;
					int num = 0;
					int num2 = 10;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							DocumentObjectType documentObjectType;
							if (documentObjectType != DocumentObjectType.Picture)
							{
								num2 = 3;
								continue;
							}
							result = false;
							num2 = 1;
							continue;
						}
						case 1:
							goto IL_9F;
						case 2:
							num2 = 13;
							continue;
						case 3:
							num2 = 8;
							continue;
						case 4:
							goto IL_9F;
						case 5:
							return result;
						case 6:
							if (true)
							{
							}
							goto IL_72;
						case 7:
							goto IL_11F;
						case 8:
						{
							DocumentObjectType documentObjectType;
							if (documentObjectType == DocumentObjectType.MergeField)
							{
								num2 = 9;
								continue;
							}
							result = false;
							num2 = 4;
							continue;
						}
						case 9:
						{
							MergeField mergeField = A_0.Items[num] as MergeField;
							num2 = 14;
							continue;
						}
						case 10:
							goto IL_11F;
						case 11:
							goto IL_9F;
						case 12:
						{
							if (num >= A_0.Items.Count)
							{
								num2 = 5;
								continue;
							}
							ParagraphBase paragraphBase = A_0.Items[num];
							DocumentObjectType documentObjectType = paragraphBase.DocumentObjectType;
							num2 = 0;
							continue;
						}
						case 13:
						{
							MergeField mergeField;
							if (!(mergeField.Text == string.Empty))
							{
								goto IL_168;
							}
							goto IL_9F;
						}
						case 14:
						{
							MergeField mergeField;
							if (mergeField.ConvertedToText)
							{
								num2 = 2;
								continue;
							}
							goto IL_72;
						}
						}
						break;
						IL_72:
						result = false;
						num2 = 11;
						continue;
						IL_9F:
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_168:
							num2 = 6;
							continue;
						default:
							if (false)
							{
							}
							num2 = 7;
							continue;
						}
						IL_11F:
						num2 = 12;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00054388 File Offset: 0x00053388
		private void ᜀ(TableRow A_0)
		{
			for (;;)
			{
				int num = 0;
				int count = A_0.Cells.Count;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9E;
						default:
						{
							if (false)
							{
							}
							if (true)
							{
							}
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							TableCell tableCell = A_0.Cells[num];
							this.ᜀ(tableCell.Items);
							num++;
							num2 = 2;
							continue;
						}
						}
						break;
					case 1:
						goto IL_32;
					case 2:
						goto IL_9E;
					case 3:
						return;
					}
					break;
					IL_32:
					num2 = 0;
					continue;
					IL_9E:
					goto IL_32;
				}
			}
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00054438 File Offset: 0x00053438
		private void ᜀ(BodyRegionCollection A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					BodyRegion bodyRegion = null;
					int num = 0;
					int num2 = A_0.Count;
					int num3 = 9;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_101;
						case 1:
							IL_162:
							goto IL_6C;
						case 2:
							return;
						case 3:
							goto IL_101;
						case 4:
							if (true)
							{
							}
							if (num >= num2)
							{
								num3 = 2;
								continue;
							}
							bodyRegion = A_0[num];
							num3 = 13;
							continue;
						case 5:
							num--;
							num2--;
							num3 = 0;
							continue;
						case 6:
							goto IL_6C;
						case 7:
						{
							Table table = bodyRegion as Table;
							int num4 = 0;
							int count = table.Rows.Count;
							num3 = 6;
							continue;
						}
						case 8:
						{
							int num4;
							int count;
							if (num4 >= count)
							{
								num3 = 3;
								continue;
							}
							Table table;
							this.ᜀ(table.Rows[num4]);
							num4++;
							num3 = 1;
							continue;
						}
						case 9:
							goto IL_167;
						case 10:
							goto IL_167;
						case 11:
							num3 = 14;
							continue;
						case 12:
							if (bodyRegion is Table)
							{
								num3 = 7;
								continue;
							}
							goto IL_101;
						case 13:
							if (bodyRegion is Paragraph)
							{
								num3 = 11;
								continue;
							}
							num3 = 12;
							continue;
						case 14:
							if (this.ᜀ(A_0, num))
							{
								num3 = 5;
								continue;
							}
							goto IL_101;
						}
						break;
						IL_6C:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_162;
						default:
							if (false)
							{
							}
							num3 = 8;
							continue;
						}
						IL_101:
						num++;
						num3 = 10;
						continue;
						IL_167:
						num3 = 4;
					}
				}
				return;
			}
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00054614 File Offset: 0x00053614
		private bool ᜀ(BodyRegionCollection A_0, int A_1)
		{
			Paragraph paragraph;
			for (;;)
			{
				paragraph = (A_0[A_1] as Paragraph);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (paragraph.Items.Count > 0)
						{
							num = 3;
							continue;
						}
						return false;
					case 1:
						num = 2;
						continue;
					case 2:
						if (true)
						{
						}
						if (!(paragraph.Items[0] is MergeField))
						{
							return false;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return false;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 3:
						num = 5;
						continue;
					case 4:
						goto IL_AD;
					case 5:
						if (paragraph.Text == string.Empty)
						{
							num = 1;
							continue;
						}
						return false;
					}
					break;
				}
			}
			IL_AD:
			A_0.Remove(paragraph);
			return true;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x000546FC File Offset: 0x000536FC
		private string ᜀ(string A_0)
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
						goto IL_73;
					}
					break;
				case 2:
					num = 3;
					continue;
				case 3:
					if (this.ᜑ.ContainsKey(A_0))
					{
						num = 1;
						continue;
					}
					goto IL_83;
				}
				if (this.ᜑ == null)
				{
					goto IL_83;
				}
				num = 2;
			}
			IL_73:
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ᜑ[A_0];
			IL_83:
			return null;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00054798 File Offset: 0x00053798
		private void ᜀ()
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
						goto IL_3B;
					default:
						goto IL_85;
					}
					break;
				case 1:
					this.Document.GrammarSpellingData.ᜀ(null);
					this.Document.GrammarSpellingData.ᜁ(null);
					num = 0;
					continue;
				case 2:
					if (true)
					{
					}
					break;
				}
				goto IL_24;
				IL_3B:
				num = 1;
				continue;
				IL_24:
				if (this.Document.GrammarSpellingData != null)
				{
					goto IL_3B;
				}
				return;
			}
			IL_85:
			if (false)
			{
			}
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00054834 File Offset: 0x00053834
		public void ExecuteGroup(MailMergeDataTable dataSource)
		{
			int a_ = 7;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (dataSource.GroupName == string.Empty)
					{
						num = 3;
						continue;
					}
					goto IL_A9;
				case 2:
					goto IL_3C;
				case 3:
					goto IL_93;
				}
				if (dataSource == null)
				{
					if (true)
					{
					}
					num = 2;
				}
				else
				{
					num = 0;
				}
			}
			IL_3C:
			goto IL_95;
			IL_93:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_95:
				throw new ArgumentNullException(ClipboardData.b("६๮հቲٴᡶ౸ॺṼ᩾", a_));
			default:
				if (false)
				{
				}
				this.ᜁ(new spr\u1977(dataSource));
				return;
			}
			IL_A9:
			this.ᜂ(new spr\u1977(dataSource));
		}

		// Token: 0x04000DE5 RID: 3557
		private Document ᜀ;

		// Token: 0x04000DE6 RID: 3558
		private MailMerge.ᜁ ᜁ;

		// Token: 0x04000DE7 RID: 3559
		private SectionCollection ᜂ;

		// Token: 0x04000DE8 RID: 3560
		private string[] ᜃ;

		// Token: 0x04000DE9 RID: 3561
		private string[] ᜄ;

		// Token: 0x04000DEA RID: 3562
		private bool ᜅ = true;

		// Token: 0x04000DEB RID: 3563
		private bool ᜆ;

		// Token: 0x04000DEC RID: 3564
		private bool ᜇ;

		// Token: 0x04000DED RID: 3565
		private bool ᜈ;

		// Token: 0x04000DEE RID: 3566
		private bool ᜉ;

		// Token: 0x04000DEF RID: 3567
		private DbConnection ᜊ;

		// Token: 0x04000DF0 RID: 3568
		private DataSet ᜋ;

		// Token: 0x04000DF1 RID: 3569
		private Dictionary<string, IRowsEnumerator> ᜌ;

		// Token: 0x04000DF2 RID: 3570
		private Regex \u170D;

		// Token: 0x04000DF3 RID: 3571
		private Stack<MailMerge.ᜁ> ᜎ;

		// Token: 0x04000DF4 RID: 3572
		private List<DictionaryEntry> ᜏ;

		// Token: 0x04000DF5 RID: 3573
		private bool ᜐ;

		// Token: 0x04000DF6 RID: 3574
		private Dictionary<string, string> ᜑ;

		// Token: 0x04000DF7 RID: 3575
		private bool \u1712;

		// Token: 0x04000DF8 RID: 3576
		private DataSet \u1713;

		// Token: 0x04000DF9 RID: 3577
		private MailMergeDataSet \u1714;

		// Token: 0x04000DFA RID: 3578
		private List<DictionaryEntry> \u1715;

		// Token: 0x04000DFB RID: 3579
		private MailMergeDataSet \u1716;

		// Token: 0x04000DFC RID: 3580
		private int \u1717;

		// Token: 0x04000DFD RID: 3581
		private Dictionary<string, bool> \u1718;

		// Token: 0x04000DFE RID: 3582
		private MergeFieldEventHandler \u1719;

		// Token: 0x04000DFF RID: 3583
		private MergeImageFieldEventHandler \u171A;

		// Token: 0x04000E00 RID: 3584
		private MergeGroupEventHandler \u171B;

		// Token: 0x02000103 RID: 259
		internal class ᜁ
		{
			// Token: 0x06000739 RID: 1849 RVA: 0x000548F8 File Offset: 0x000538F8
			internal TextBodySelection ᜄ()
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

			// Token: 0x0600073A RID: 1850 RVA: 0x0005493C File Offset: 0x0005393C
			internal MailMerge.ᜀ ᜅ()
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

			// Token: 0x0600073B RID: 1851 RVA: 0x00054980 File Offset: 0x00053980
			internal MergeField ᜂ()
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
				return this.ᜄ;
			}

			// Token: 0x0600073C RID: 1852 RVA: 0x000549C4 File Offset: 0x000539C4
			internal MergeField ᜆ()
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
				return this.ᜅ;
			}

			// Token: 0x0600073D RID: 1853 RVA: 0x00054A08 File Offset: 0x00053A08
			internal void ᜂ(MergeField A_0)
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
				this.ᜅ = A_0;
			}

			// Token: 0x0600073E RID: 1854 RVA: 0x00054A4C File Offset: 0x00053A4C
			internal int ᜃ()
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
				return this.ᜆ;
			}

			// Token: 0x0600073F RID: 1855 RVA: 0x00054A90 File Offset: 0x00053A90
			internal void ᜀ(int A_0)
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
				this.ᜆ = A_0;
			}

			// Token: 0x06000740 RID: 1856 RVA: 0x00054AD4 File Offset: 0x00053AD4
			internal bool ᜇ()
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
				return this.ᜅ != null;
			}

			// Token: 0x06000741 RID: 1857 RVA: 0x00054B1C File Offset: 0x00053B1C
			internal string ᜈ()
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
				return this.ᜌ;
			}

			// Token: 0x06000742 RID: 1858 RVA: 0x00054B60 File Offset: 0x00053B60
			internal int ᜁ()
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
				return this.ᜏ;
			}

			// Token: 0x06000743 RID: 1859 RVA: 0x00054BA4 File Offset: 0x00053BA4
			internal ᜁ(MailMerge.ᜁ.ᜀ A_0)
			{
				this.\u170D = (MailMerge.ᜁ.ᜀ)Delegate.Combine(this.\u170D, A_0);
			}

			// Token: 0x06000744 RID: 1860 RVA: 0x00054BF8 File Offset: 0x00053BF8
			private void ᜀ(IRowsEnumerator A_0)
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
				this.ᜀ = null;
				this.ᜁ = null;
				this.ᜄ = null;
				this.ᜅ = null;
				this.ᜆ = 0;
				this.ᜇ = -1;
				this.ᜈ = -1;
				this.ᜉ = -1;
				this.ᜊ = -1;
				this.ᜏ = -1;
				this.ᜎ = A_0;
				this.ᜌ = this.ᜎ.TableName;
			}

			// Token: 0x06000745 RID: 1861 RVA: 0x00054C94 File Offset: 0x00053C94
			private void ᜀ(ICompositeObject A_0)
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
				Stack<string> a_ = new Stack<string>();
				this.ᜀ(A_0, a_);
			}

			// Token: 0x06000746 RID: 1862 RVA: 0x00054CE0 File Offset: 0x00053CE0
			private void ᜀ(ICompositeObject A_0, Stack<string> A_1)
			{
				switch (0)
				{
				default:
				{
					if (true)
					{
					}
					IEnumerator enumerator = A_0.ChildObjects.GetEnumerator();
					try
					{
						int num = 6;
						for (;;)
						{
							switch (num)
							{
							case 0:
								A_1.Pop();
								num = 8;
								continue;
							case 1:
							{
								MergeField mergeField;
								mergeField.Domain = A_1.Peek();
								num = 10;
								continue;
							}
							case 2:
							{
								IDocumentObject documentObject;
								if (documentObject is ICompositeObject)
								{
									num = 15;
									continue;
								}
								num = 17;
								continue;
							}
							case 3:
							{
								if (!enumerator.MoveNext())
								{
									num = 9;
									continue;
								}
								IDocumentObject documentObject = (IDocumentObject)enumerator.Current;
								num = 2;
								continue;
							}
							case 4:
							{
								string fieldName;
								if (fieldName == A_1.Peek())
								{
									num = 0;
									continue;
								}
								break;
							}
							case 5:
							{
								MergeField mergeField;
								if (MailMerge.ᜂ(mergeField))
								{
									num = 7;
									continue;
								}
								num = 16;
								continue;
							}
							case 7:
							{
								MergeField mergeField;
								A_1.Push(mergeField.FieldName);
								mergeField.Domain = mergeField.FieldName;
								num = 14;
								continue;
							}
							case 9:
								num = 18;
								continue;
							case 11:
							{
								IDocumentObject documentObject;
								MergeField mergeField = documentObject as MergeField;
								num = 5;
								continue;
							}
							case 13:
							{
								MergeField mergeField;
								string fieldName = mergeField.FieldName;
								mergeField.Domain = fieldName;
								num = 4;
								continue;
							}
							case 15:
							{
								IDocumentObject documentObject;
								this.ᜀ(documentObject as ICompositeObject, A_1);
								num = 12;
								continue;
							}
							case 16:
							{
								MergeField mergeField;
								if (MailMerge.ᜁ(mergeField))
								{
									num = 13;
									continue;
								}
								num = 19;
								continue;
							}
							case 17:
							{
								IDocumentObject documentObject;
								if (documentObject is MergeField)
								{
									num = 11;
									continue;
								}
								break;
							}
							case 18:
								goto IL_238;
							case 19:
								if (A_1.Count > 0)
								{
									num = 1;
									continue;
								}
								break;
							}
							IL_F4:
							num = 3;
							continue;
							goto IL_F4;
						}
						IL_238:;
					}
					finally
					{
						for (;;)
						{
							IDisposable disposable = enumerator as IDisposable;
							int num = 0;
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
										if (false)
										{
										}
										if (disposable == null)
										{
											goto IL_29D;
										}
										break;
									}
									num = 2;
									continue;
								case 1:
									goto IL_29B;
								case 2:
									disposable.Dispose();
									num = 1;
									continue;
								}
								break;
							}
						}
						IL_29B:
						IL_29D:;
					}
					return;
				}
				}
			}

			// Token: 0x06000747 RID: 1863 RVA: 0x00054FB4 File Offset: 0x00053FB4
			internal void ᜀ(Body A_0, IRowsEnumerator A_1)
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
				this.ᜀ(A_0);
				this.ᜀ(A_1);
				this.ᜃ = A_0;
				this.ᜂ = A_0;
				this.ᜀ(this.ᜃ.Items);
			}

			// Token: 0x06000748 RID: 1864 RVA: 0x00055020 File Offset: 0x00054020
			internal void ᜀ(Table A_0, int A_1, int A_2, IRowsEnumerator A_3)
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
				this.ᜀ(A_3);
				this.ᜀ(A_0, A_1, A_2);
			}

			// Token: 0x06000749 RID: 1865 RVA: 0x0005506C File Offset: 0x0005406C
			private void ᜀ(BodyRegionCollection A_0)
			{
				switch (0)
				{
				default:
					for (;;)
					{
						int num = 0;
						int count = A_0.Count;
						int num2 = 36;
						for (;;)
						{
							int num3;
							Paragraph paragraph;
							switch (num2)
							{
							case 0:
							{
								ParagraphBase paragraphBase;
								if (paragraphBase is BookmarkEnd)
								{
									num2 = 4;
									continue;
								}
								this.ᜈ = num3;
								num2 = 5;
								continue;
							}
							case 1:
								goto IL_24B;
							case 2:
								goto IL_466;
							case 3:
								goto IL_4A7;
							case 4:
								goto IL_1FB;
							case 5:
							{
								ParagraphBase paragraphBase;
								if (paragraphBase is TextBox)
								{
									num2 = 14;
									continue;
								}
								this.ᜀ(paragraphBase);
								num2 = 39;
								continue;
							}
							case 6:
								num2 = 35;
								continue;
							case 7:
								goto IL_1DD;
							case 8:
								if (this.ᜁ.ᜁ == this.ᜁ.ᜂ)
								{
									num2 = 9;
									continue;
								}
								goto IL_4A7;
							case 9:
								count = A_0.Count;
								num2 = 20;
								continue;
							case 10:
								return;
							case 11:
							{
								BodyRegion bodyRegion;
								if ((bodyRegion as spr\u1AE7).ᜆ() != null)
								{
									num2 = 6;
									continue;
								}
								goto IL_4A7;
							}
							case 12:
								goto IL_4A7;
							case 13:
								if (this.ᜇ())
								{
									num2 = 38;
									continue;
								}
								goto IL_466;
							case 14:
							{
								this.ᜆ = 0;
								ParagraphBase paragraphBase;
								this.ᜀ((paragraphBase as TextBox).Body.Items);
								this.ᜆ = num;
								num2 = 28;
								continue;
							}
							case 15:
							{
								BodyRegion bodyRegion;
								this.ᜀ((bodyRegion as spr\u1AE7).ᜆ().ᜂ().Items);
								num2 = 12;
								continue;
							}
							case 16:
								num2 = 17;
								continue;
							case 17:
							{
								BodyRegion bodyRegion;
								if ((bodyRegion as spr\u1AE7).ᜆ().ᜂ().Items != null)
								{
									num2 = 15;
									continue;
								}
								goto IL_4A7;
							}
							case 18:
							{
								DocumentObjectType documentObjectType;
								if (documentObjectType != DocumentObjectType.Table)
								{
									num2 = 24;
									continue;
								}
								BodyRegion bodyRegion;
								Table table = (Table)bodyRegion;
								this.ᜀ(table, 0, table.Rows.Count - 1);
								num2 = 19;
								continue;
							}
							case 19:
								goto IL_4A7;
							case 20:
								goto IL_4A7;
							case 21:
								num = this.ᜀ.ItemEndIndex;
								num2 = 40;
								continue;
							case 22:
								num3 = this.ᜀ.ParagraphItemEndIndex;
								num2 = 7;
								continue;
							case 23:
								num2 = 0;
								continue;
							case 24:
								num2 = 33;
								continue;
							case 25:
							{
								if (num >= count)
								{
									num2 = 10;
									continue;
								}
								BodyRegion bodyRegion = A_0[num];
								this.ᜆ = num;
								DocumentObjectType documentObjectType = bodyRegion.DocumentObjectType;
								num2 = 37;
								continue;
							}
							case 26:
								goto IL_1A8;
							case 27:
								num2 = 3;
								continue;
							case 28:
								goto IL_26F;
							case 29:
								goto IL_478;
							case 30:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_478;
								default:
									if (false)
									{
									}
									num2 = 18;
									continue;
								}
								break;
							case 31:
								goto IL_466;
							case 32:
							{
								if (num3 >= paragraph.Items.Count)
								{
									num2 = 27;
									continue;
								}
								ParagraphBase paragraphBase = paragraph.Items[num3];
								num2 = 34;
								continue;
							}
							case 33:
								goto IL_4A2;
							case 34:
							{
								ParagraphBase paragraphBase;
								if (!(paragraphBase is BookmarkStart))
								{
									num2 = 23;
									continue;
								}
								goto IL_1FB;
							}
							case 35:
							{
								BodyRegion bodyRegion;
								if ((bodyRegion as spr\u1AE7).ᜆ().ᜂ() != null)
								{
									num2 = 16;
									continue;
								}
								goto IL_4A7;
							}
							case 36:
								if (true)
								{
								}
								goto IL_24B;
							case 37:
							{
								DocumentObjectType documentObjectType;
								switch (documentObjectType)
								{
								case DocumentObjectType.Paragraph:
								{
									BodyRegion bodyRegion;
									paragraph = (Paragraph)bodyRegion;
									num3 = 0;
									num2 = 26;
									continue;
								}
								case DocumentObjectType.StructureDocumentTag:
									num2 = 11;
									continue;
								default:
									num2 = 30;
									continue;
								}
								break;
							}
							case 38:
								num2 = 41;
								continue;
							case 39:
								goto IL_26F;
							case 40:
								if (this.ᜀ.ItemStartIndex == this.ᜀ.ItemEndIndex)
								{
									num2 = 22;
									continue;
								}
								goto IL_1DD;
							case 41:
								if (this.ᜀ != null)
								{
									num2 = 21;
									continue;
								}
								num2 = 8;
								continue;
							}
							break;
							IL_1A8:
							num2 = 32;
							continue;
							IL_478:
							goto IL_1A8;
							IL_1DD:
							count = A_0.Count;
							this.ᜀ();
							num2 = 31;
							continue;
							IL_1FB:
							paragraph.Items.RemoveAt(num3);
							num3--;
							num2 = 2;
							continue;
							IL_24B:
							num2 = 25;
							continue;
							IL_26F:
							num2 = 13;
							continue;
							IL_466:
							num3++;
							num2 = 29;
							continue;
							IL_4A7:
							num++;
							num2 = 1;
						}
					}
					return;
					IL_4A2:
					throw new Exception();
				}
			}

			// Token: 0x0600074A RID: 1866 RVA: 0x000555BC File Offset: 0x000545BC
			private void ᜀ(Table A_0, int A_1, int A_2)
			{
				switch (0)
				{
				default:
				{
					int num;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
					{
						IL_81:
						int count;
						if (num >= count)
						{
							num2 = 4;
						}
						else
						{
							TableRow tableRow;
							TableCell tableCell = tableRow.Cells[num];
							this.ᜀ(tableCell.Items);
							num2 = 3;
						}
						break;
					}
					default:
						if (false)
						{
						}
						goto IL_5F;
					}
					int count2;
					int num3;
					for (;;)
					{
						IL_2C:
						switch (num2)
						{
						case 0:
							goto IL_78;
						case 1:
							A_2 += A_0.Rows.Count - count2;
							num3 = this.ᜁ.ᜁ;
							this.ᜀ();
							num2 = 6;
							continue;
						case 2:
							return;
						case 3:
							if (this.ᜇ())
							{
								num2 = 1;
								continue;
							}
							num++;
							num2 = 0;
							continue;
						case 4:
							goto IL_13F;
						case 5:
						{
							if (num3 > A_2)
							{
								num2 = 2;
								continue;
							}
							TableRow tableRow = A_0.Rows[num3];
							this.ᜊ = num3;
							num = 0;
							int count = tableRow.Cells.Count;
							num2 = 9;
							continue;
						}
						case 6:
							goto IL_13F;
						case 7:
							goto IL_81;
						case 8:
							goto IL_E0;
						case 9:
							goto IL_78;
						case 10:
							goto IL_E0;
						}
						goto IL_5F;
						IL_78:
						num2 = 7;
						continue;
						IL_E0:
						num2 = 5;
						continue;
						IL_13F:
						if (true)
						{
						}
						num3++;
						num2 = 8;
					}
					return;
					IL_5F:
					count2 = A_0.Rows.Count;
					num3 = A_1;
					num2 = 10;
					goto IL_2C;
				}
				}
			}

			// Token: 0x0600074B RID: 1867 RVA: 0x00055754 File Offset: 0x00054754
			private void ᜀ()
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
				this.ᜀ = null;
				this.ᜁ = null;
				this.ᜄ = null;
				this.ᜅ = null;
			}

			// Token: 0x0600074C RID: 1868 RVA: 0x000557AC File Offset: 0x000547AC
			private void ᜀ(ParagraphBase A_0)
			{
				int num = 4;
				MergeField mergeField;
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
							if (MailMerge.ᜂ(mergeField))
							{
								num = 8;
								continue;
							}
							goto IL_166;
						case 1:
							goto IL_BD;
						case 2:
							if (mergeField.FieldName == this.ᜌ)
							{
								num = 3;
								continue;
							}
							goto IL_166;
						case 3:
							num = 9;
							continue;
						case 5:
							this.ᜀ(mergeField);
							num = 6;
							continue;
						case 6:
							if (this.\u170D != null)
							{
								num = 11;
								continue;
							}
							goto IL_166;
						case 7:
							if (MailMerge.ᜁ(mergeField))
							{
								num = 5;
								continue;
							}
							goto IL_166;
						case 8:
							goto IL_161;
						case 9:
							if (this.ᜄ == null)
							{
								num = 1;
								continue;
							}
							num = 7;
							continue;
						case 10:
							goto IL_DB;
						case 11:
							this.\u170D(this.ᜎ);
							num = 10;
							continue;
						case 12:
							mergeField = (A_0 as MergeField);
							num = 2;
							continue;
						}
						if (A_0.DocumentObjectType == DocumentObjectType.MergeField)
						{
							num = 12;
							continue;
						}
						goto IL_166;
					}
					IL_BD:
					num = 0;
				}
				IL_DB:
				goto IL_166;
				IL_161:
				this.ᜁ(mergeField);
				return;
				IL_166:
				if (true)
				{
				}
			}

			// Token: 0x0600074D RID: 1869 RVA: 0x00055928 File Offset: 0x00054928
			private void ᜁ(MergeField A_0)
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
				this.ᜄ = A_0;
				this.ᜂ = A_0.OwnerParagraph.OwnerTextBody;
				this.ᜇ = this.ᜆ;
				this.ᜉ = this.ᜈ;
				this.ᜋ = this.ᜊ;
			}

			// Token: 0x0600074E RID: 1870 RVA: 0x000559A0 File Offset: 0x000549A0
			private void ᜀ(MergeField A_0)
			{
				Body ownerTextBody;
				for (;;)
				{
					this.ᜅ = A_0;
					ownerTextBody = A_0.OwnerParagraph.OwnerTextBody;
					this.ᜏ = this.ᜆ - this.ᜇ + 1;
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 2;
							continue;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_58;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								num = 6;
								continue;
							}
							break;
						case 2:
							if (this.ᜂ.DocumentObjectType == DocumentObjectType.TableCell)
							{
								num = 1;
								continue;
							}
							goto IL_189;
						case 3:
							if (ownerTextBody.DocumentObjectType == DocumentObjectType.TableCell)
							{
								num = 0;
								continue;
							}
							goto IL_189;
						case 4:
							goto IL_AE;
						case 5:
							goto IL_58;
						case 6:
							if ((this.ᜂ.Owner as TableRow).OwnerTable == (ownerTextBody.Owner as TableRow).OwnerTable)
							{
								num = 4;
								continue;
							}
							goto IL_189;
						case 7:
							goto IL_6C;
						}
						break;
						IL_58:
						if (ownerTextBody == this.ᜂ)
						{
							num = 7;
						}
						else
						{
							num = 3;
						}
					}
				}
				IL_6C:
				this.ᜀ = new TextBodySelection(ownerTextBody, this.ᜇ, this.ᜆ, this.ᜉ, this.ᜈ);
				return;
				IL_AE:
				this.ᜀ(ownerTextBody as TableCell);
				this.ᜁ = new MailMerge.ᜀ(ownerTextBody.Owner.Owner as Table, this.ᜋ, this.ᜊ);
				return;
				IL_189:
				throw new MailMergeException();
			}

			// Token: 0x0600074F RID: 1871 RVA: 0x00055B3C File Offset: 0x00054B3C
			private void ᜀ(TableCell A_0)
			{
				switch (0)
				{
				default:
					for (;;)
					{
						TableRow tableRow = A_0.OwnerRow;
						bool flag = false;
						IEnumerator enumerator = tableRow.Cells.GetEnumerator();
						int num = 5;
						for (;;)
						{
							switch (num)
							{
							case 0:
								return;
							case 1:
								goto IL_167;
							case 2:
								goto IL_281;
							case 3:
								if (!flag)
								{
									if (true)
									{
									}
									num = 0;
									continue;
								}
								goto IL_281;
							case 4:
								try
								{
									num = 0;
									for (;;)
									{
										switch (num)
										{
										case 1:
										{
											IEnumerator enumerator2;
											if (!enumerator2.MoveNext())
											{
												num = 3;
												continue;
											}
											TableCell tableCell = (TableCell)enumerator2.Current;
											num = 6;
											continue;
										}
										case 2:
											flag = true;
											num = 5;
											continue;
										case 3:
											goto IL_10D;
										case 4:
											goto IL_119;
										case 5:
											goto IL_10D;
										case 6:
											if (A_0.CellFormat.VerticalMerge != CellMerge.None)
											{
												num = 2;
												continue;
											}
											break;
										}
										IL_B5:
										num = 1;
										continue;
										goto IL_B5;
										IL_10D:
										num = 4;
									}
									IL_119:
									goto IL_5E;
								}
								finally
								{
									for (;;)
									{
										IEnumerator enumerator2;
										IDisposable disposable = enumerator2 as IDisposable;
										num = 0;
										for (;;)
										{
											switch (num)
											{
											case 0:
												if (disposable != null)
												{
													num = 2;
													continue;
												}
												goto IL_166;
											case 1:
												goto IL_164;
											case 2:
												disposable.Dispose();
												num = 1;
												continue;
											}
											break;
										}
									}
									IL_164:
									IL_166:;
								}
								goto IL_167;
								IL_5E:
								num = 8;
								continue;
							case 5:
								try
								{
									num = 5;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_23A;
										case 1:
											if (A_0.CellFormat.VerticalMerge != CellMerge.None)
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
													num = 6;
													continue;
												}
											}
											break;
										case 2:
											goto IL_22E;
										case 3:
										{
											if (!enumerator.MoveNext())
											{
												num = 4;
												continue;
											}
											TableCell tableCell2 = (TableCell)enumerator.Current;
											num = 1;
											continue;
										}
										case 4:
											goto IL_22E;
										case 6:
											flag = true;
											num = 2;
											continue;
										}
										IL_20C:
										num = 3;
										continue;
										goto IL_20C;
										IL_22E:
										num = 0;
									}
									IL_23A:
									goto IL_2A4;
								}
								finally
								{
									for (;;)
									{
										IDisposable disposable2 = enumerator as IDisposable;
										num = 0;
										for (;;)
										{
											switch (num)
											{
											case 0:
												if (disposable2 != null)
												{
													num = 1;
													continue;
												}
												goto IL_280;
											case 1:
												disposable2.Dispose();
												num = 2;
												continue;
											case 2:
												goto IL_27E;
											}
											break;
										}
									}
									IL_27E:
									IL_280:;
								}
								goto IL_281;
								IL_2A4:
								num = 3;
								continue;
							case 6:
							{
								if (tableRow.NextSibling == null)
								{
									num = 7;
									continue;
								}
								tableRow = (tableRow.NextSibling as TableRow);
								flag = false;
								IEnumerator enumerator2 = tableRow.Cells.GetEnumerator();
								num = 4;
								continue;
							}
							case 7:
								goto IL_2A1;
							case 8:
								if (flag)
								{
									num = 1;
									continue;
								}
								return;
							}
							break;
							IL_167:
							this.ᜊ++;
							num = 2;
							continue;
							IL_281:
							num = 6;
						}
					}
					IL_2A1:
					return;
				}
			}

			// Token: 0x04000E01 RID: 3585
			private TextBodySelection ᜀ;

			// Token: 0x04000E02 RID: 3586
			private MailMerge.ᜀ ᜁ;

			// Token: 0x04000E03 RID: 3587
			private Body ᜂ;

			// Token: 0x04000E04 RID: 3588
			private Body ᜃ;

			// Token: 0x04000E05 RID: 3589
			private MergeField ᜄ;

			// Token: 0x04000E06 RID: 3590
			private MergeField ᜅ;

			// Token: 0x04000E07 RID: 3591
			private int ᜆ;

			// Token: 0x04000E08 RID: 3592
			private int ᜇ = -1;

			// Token: 0x04000E09 RID: 3593
			private int ᜈ = -1;

			// Token: 0x04000E0A RID: 3594
			private int ᜉ = -1;

			// Token: 0x04000E0B RID: 3595
			private int ᜊ = -1;

			// Token: 0x04000E0C RID: 3596
			private int ᜋ = -1;

			// Token: 0x04000E0D RID: 3597
			private string ᜌ;

			// Token: 0x04000E0E RID: 3598
			private MailMerge.ᜁ.ᜀ \u170D;

			// Token: 0x04000E0F RID: 3599
			private IRowsEnumerator ᜎ;

			// Token: 0x04000E10 RID: 3600
			private int ᜏ = -1;

			// Token: 0x02000104 RID: 260
			// (Invoke) Token: 0x06000751 RID: 1873
			internal delegate void ᜀ(IRowsEnumerator A_0);
		}

		// Token: 0x02000105 RID: 261
		internal class ᜀ
		{
			// Token: 0x06000754 RID: 1876 RVA: 0x00055E5C File Offset: 0x00054E5C
			internal ᜀ(Table A_0, int A_1, int A_2)
			{
				this.ᜀ = A_0;
				this.ᜁ = A_1;
				this.ᜂ = A_2;
				this.ᜀ();
			}

			// Token: 0x06000755 RID: 1877 RVA: 0x00055E8C File Offset: 0x00054E8C
			private void ᜀ()
			{
				int a_ = 11;
				int num = 3;
				for (;;)
				{
					IL_13:
					switch (num)
					{
					case 0:
						goto IL_A1;
					case 1:
						num = 5;
						continue;
					case 2:
						goto IL_F2;
					case 4:
						if (this.ᜂ >= 0)
						{
							num = 7;
							continue;
						}
						goto IL_F4;
					case 5:
						if (this.ᜁ >= this.ᜀ.Rows.Count)
						{
							num = 2;
							continue;
						}
						num = 4;
						continue;
					case 6:
						while (this.ᜂ >= this.ᜀ.Rows.Count)
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
								num = 0;
								goto IL_13;
							}
						}
						return;
					case 7:
						if (true)
						{
						}
						num = 6;
						continue;
					}
					if (this.ᜁ < 0)
					{
						goto IL_108;
					}
					num = 1;
				}
				IL_A1:
				goto IL_F4;
				IL_F2:
				goto IL_108;
				IL_F4:
				throw new ArgumentOutOfRangeException(ClipboardData.b("㑰ᵲᅴ╶ᙸ౺㑼ᅾﶄ", a_));
				IL_108:
				throw new ArgumentOutOfRangeException(ClipboardData.b("≰ݲᑴն൸⥺ቼࡾ좀", a_));
			}

			// Token: 0x04000E11 RID: 3601
			internal Table ᜀ;

			// Token: 0x04000E12 RID: 3602
			internal int ᜁ;

			// Token: 0x04000E13 RID: 3603
			internal int ᜂ;
		}
	}
}
