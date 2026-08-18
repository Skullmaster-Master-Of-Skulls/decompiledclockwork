using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using a.b;

namespace MailBee.Outlook
{
	// Token: 0x020005A3 RID: 1443
	[Serializable]
	internal class DirectoryNode : EntryNode, ig, gj
	{
		// Token: 0x0600307C RID: 12412 RVA: 0x000E33E6 File Offset: 0x000E23E6
		public DirectoryNode(g8 A_0, POIFSFileSystem A_1, DirectoryNode A_2) : this(A_0, A_2, A_1, null)
		{
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x000E33F2 File Offset: 0x000E23F2
		public DirectoryNode(g8 A_0, h0 A_1, DirectoryNode A_2) : this(A_0, A_2, null, A_1)
		{
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x000E3400 File Offset: 0x000E2400
		private DirectoryNode(g8 A_0, DirectoryNode A_1, POIFSFileSystem A_2, h0 A_3) : base(A_0, A_1)
		{
			this._oFilesSystem = A_2;
			this._nFilesSystem = A_3;
			if (A_1 == null)
			{
				this._path = new db();
			}
			else
			{
				this._path = new db(A_1._path, new string[]
				{
					A_0.f()
				});
			}
			this._byname = new Dictionary<string, e1>();
			this._entries = new List<e1>();
			IEnumerator<ed> enumerator = A_0.om();
			while (enumerator.MoveNext())
			{
				ed ed = enumerator.Current;
				e1 e;
				if (ed.lj())
				{
					g8 a_ = (g8)ed;
					if (this._oFilesSystem != null)
					{
						e = new DirectoryNode(a_, this._oFilesSystem, this);
					}
					else
					{
						e = new DirectoryNode(a_, this._nFilesSystem, this);
					}
				}
				else
				{
					e = new hz((gg)ed, this);
				}
				this._entries.Add(e);
				this._byname.Add(e.r(), e);
			}
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x000E34E6 File Offset: 0x000E24E6
		public az b(string A_0)
		{
			e1 e = this.el(A_0);
			if (!e.s())
			{
				throw new IOException("Entry '" + A_0 + "' Is not a DocumentEntry");
			}
			return new az((h4)e);
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x000E3518 File Offset: 0x000E2518
		public h4 a(eg A_0)
		{
			gg gg = A_0.c();
			hz hz = new hz(gg, this);
			((g8)base.Property).on(gg);
			this._oFilesSystem.a(A_0);
			this._entries.Add(hz);
			this._byname.Add(gg.f(), hz);
			return hz;
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x000E3570 File Offset: 0x000E2570
		public bool a(string A_0, string A_1)
		{
			bool flag = false;
			EntryNode entryNode = (EntryNode)this._byname[A_0];
			if (entryNode != null)
			{
				flag = ((g8)base.Property).a(entryNode.Property, A_1);
				if (flag)
				{
					this._byname.Remove(A_0);
					this._byname[entryNode.Property.f()] = entryNode;
				}
			}
			return flag;
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x000E35D4 File Offset: 0x000E25D4
		public bool a(EntryNode A_0)
		{
			bool flag = ((g8)base.Property).a(A_0.Property);
			if (flag)
			{
				this._entries.Remove(A_0);
				this._byname.Remove(A_0.Name);
				if (this._oFilesSystem != null)
				{
					this._oFilesSystem.a(A_0);
					return flag;
				}
				this._nFilesSystem.a(A_0);
			}
			return flag;
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06003083 RID: 12419 RVA: 0x000E363A File Offset: 0x000E263A
		public db Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x000E3642 File Offset: 0x000E2642
		public POIFSFileSystem FileSystem
		{
			get
			{
				return this._oFilesSystem;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06003085 RID: 12421 RVA: 0x000E364A File Offset: 0x000E264A
		public h0 NFileSystem
		{
			get
			{
				return this._nFilesSystem;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06003086 RID: 12422 RVA: 0x000E3652 File Offset: 0x000E2652
		public IEnumerator<e1> Entries
		{
			get
			{
				return this._entries.GetEnumerator();
			}
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x000E3664 File Offset: 0x000E2664
		internal e1 a(int A_0)
		{
			return this._entries[A_0];
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06003088 RID: 12424 RVA: 0x000E3672 File Offset: 0x000E2672
		public bool IsEmpty
		{
			get
			{
				return this._entries.Count == 0;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06003089 RID: 12425 RVA: 0x000E3682 File Offset: 0x000E2682
		public int EntryCount
		{
			get
			{
				return this._entries.Count;
			}
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x000E368F File Offset: 0x000E268F
		public bool ej(string A_0)
		{
			return A_0 != null && this._byname.ContainsKey(A_0);
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x000E36A4 File Offset: 0x000E26A4
		public e1 el(string A_0)
		{
			e1 e = null;
			if (A_0 != null)
			{
				try
				{
					e = this._byname[A_0];
				}
				catch (KeyNotFoundException)
				{
					throw new FileNotFoundException("no such entry: \"" + A_0 + "\"");
				}
			}
			if (e == null)
			{
				throw new FileNotFoundException("no such entry: \"" + A_0 + "\"");
			}
			return e;
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x000E3708 File Offset: 0x000E2708
		public az a(e1 A_0)
		{
			if (!A_0.s())
			{
				throw new MailBeeOutlookMsgBuildingException(string.Format(Resources.Instance.ErrorDesc_OleDocEntry0IsNotDocumentEntry, A_0.r()), 1201);
			}
			return new az((h4)A_0);
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x000E373D File Offset: 0x000E273D
		public az a(string A_0)
		{
			return this.a(this.el(A_0));
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x000E374C File Offset: 0x000E274C
		public h4 a(hw A_0)
		{
			h4 result;
			try
			{
				gg gg = A_0.a();
				hz hz = new hz(gg, this);
				((g8)base.Property).on(gg);
				this._nFilesSystem.a(A_0);
				this._entries.Add(hz);
				this._byname[gg.f()] = hz;
				result = hz;
			}
			catch (IOException ex)
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x000E37BC File Offset: 0x000E27BC
		public ig eo(string A_0)
		{
			g8 a_ = new g8(A_0);
			DirectoryNode directoryNode;
			if (this._oFilesSystem != null)
			{
				directoryNode = new DirectoryNode(a_, this._oFilesSystem, this);
				this._oFilesSystem.a(a_);
			}
			else
			{
				directoryNode = new DirectoryNode(a_, this._nFilesSystem, this);
				this._nFilesSystem.a(a_);
			}
			((g8)base.Property).on(a_);
			this._entries.Add(directoryNode);
			this._byname[A_0] = directoryNode;
			return directoryNode;
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06003091 RID: 12433 RVA: 0x000E3847 File Offset: 0x000E2847
		// (set) Token: 0x06003090 RID: 12432 RVA: 0x000E3839 File Offset: 0x000E2839
		public ar StorageClsid
		{
			get
			{
				return base.Property.d();
			}
			set
			{
				base.Property.a(value);
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06003092 RID: 12434 RVA: 0x000E3854 File Offset: 0x000E2854
		public override bool IsDirectoryEntry
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06003093 RID: 12435 RVA: 0x000E3857 File Offset: 0x000E2857
		protected override bool IsDeleteOK
		{
			get
			{
				return this.IsEmpty;
			}
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x000E3860 File Offset: 0x000E2860
		public h4 em(string A_0, Stream A_1)
		{
			h4 result;
			try
			{
				if (this._nFilesSystem != null)
				{
					result = this.a(new hw(A_0, this._nFilesSystem, A_1));
				}
				else
				{
					result = this.a(new eg(A_0, A_1));
				}
			}
			catch (IOException ex)
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x000E38B0 File Offset: 0x000E28B0
		public h4 en(string A_0, int A_1, dn A_2)
		{
			return this.a(new eg(A_0, A_1, this._path, A_2));
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06003096 RID: 12438 RVA: 0x000E38C6 File Offset: 0x000E28C6
		public Array ViewableArray
		{
			get
			{
				return new object[0];
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06003097 RID: 12439 RVA: 0x000E38CE File Offset: 0x000E28CE
		public IEnumerator ViewableIterator
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(base.Property);
				arrayList.AddRange(this._entries);
				return arrayList.GetEnumerator();
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06003098 RID: 12440 RVA: 0x000E38F3 File Offset: 0x000E28F3
		public bool PreferArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06003099 RID: 12441 RVA: 0x000E38F6 File Offset: 0x000E28F6
		public string ShortDescription
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x000E38FE File Offset: 0x000E28FE
		public IEnumerator<e1> GetEnumerator()
		{
			return this._entries.GetEnumerator();
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x000E3910 File Offset: 0x000E2910
		IEnumerator IEnumerable.b()
		{
			return this._entries.GetEnumerator();
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x0600309C RID: 12444 RVA: 0x000E3922 File Offset: 0x000E2922
		public bool CanRead
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x0600309D RID: 12445 RVA: 0x000E3929 File Offset: 0x000E2929
		public bool CanSeek
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x000E3930 File Offset: 0x000E2930
		public bool CanWrite
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x000E3937 File Offset: 0x000E2937
		public void g()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060030A0 RID: 12448 RVA: 0x000E393E File Offset: 0x000E293E
		public long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060030A1 RID: 12449 RVA: 0x000E3945 File Offset: 0x000E2945
		// (set) Token: 0x060030A2 RID: 12450 RVA: 0x000E394C File Offset: 0x000E294C
		public long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x000E3953 File Offset: 0x000E2953
		public int a(byte[] A_0, int A_1, int A_2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x000E395A File Offset: 0x000E295A
		public long a(long A_0, SeekOrigin A_1)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x000E3961 File Offset: 0x000E2961
		public void a(long A_0)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400202B RID: 8235
		private Dictionary<string, e1> _byname;

		// Token: 0x0400202C RID: 8236
		private List<e1> _entries;

		// Token: 0x0400202D RID: 8237
		private POIFSFileSystem _oFilesSystem;

		// Token: 0x0400202E RID: 8238
		private h0 _nFilesSystem;

		// Token: 0x0400202F RID: 8239
		private db _path;
	}
}
