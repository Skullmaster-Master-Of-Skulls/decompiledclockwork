using System;
using System.Collections;
using System.IO;
using a.b;

namespace MailBee.Outlook
{
	// Token: 0x020005A6 RID: 1446
	[Serializable]
	internal class POIFSFileSystem : gj
	{
		// Token: 0x060030A8 RID: 12456 RVA: 0x000E397A File Offset: 0x000E297A
		public static Stream b(Stream A_0)
		{
			return new gm(A_0);
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000E3984 File Offset: 0x000E2984
		public POIFSFileSystem()
		{
			c3 a_ = new c3(this.bigBlockSize);
			this._property_table = new gz(a_);
			this._documents = new ArrayList();
			this._root = null;
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x000E39CC File Offset: 0x000E29CC
		public POIFSFileSystem(Stream A_0) : this()
		{
			bool a_ = false;
			c3 c;
			d3 d;
			try
			{
				c = new c3(A_0);
				this.bigBlockSize = c.b();
				d = new d3(A_0, this.bigBlockSize);
				a_ = true;
			}
			finally
			{
				this.a(A_0, a_);
			}
			new e7(c.b(), c.f(), c.d(), c.a(), c.h(), d);
			gz gz = new gz(c, d);
			this.a(fx.a(this.bigBlockSize, d, gz.b(), c.e()), d, gz.b().om(), null, c.g());
			this.Root.StorageClsid = gz.b().d();
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000E3A94 File Offset: 0x000E2A94
		private void a(Stream A_0, bool A_1)
		{
			if (A_0 is MemoryStream)
			{
				"POIFS is closing the supplied input stream of type (" + A_0.GetType().Name + ") which supports mark/reset.  This will be a problem for the caller if the stream will still be used.  If that is the case the caller should wrap the input stream to avoid this Close logic.  This warning is only temporary and will not be present in future versions of POI.";
			}
			try
			{
				A_0.Close();
			}
			catch (IOException)
			{
				if (A_1)
				{
					throw;
				}
			}
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x000E3AE8 File Offset: 0x000E2AE8
		public static bool a(Stream A_0)
		{
			byte[] a_ = new byte[8];
			g9.a(A_0, a_);
			return new r(0, a_).a() == -2226271756974174256L;
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x000E3B1B File Offset: 0x000E2B1B
		public h4 a(Stream A_0, string A_1)
		{
			return this.Root.em(A_1, A_0);
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x000E3B2A File Offset: 0x000E2B2A
		public h4 a(string A_0, int A_1, dn A_2)
		{
			return this.Root.en(A_0, A_1, A_2);
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x000E3B3A File Offset: 0x000E2B3A
		public ig a(string A_0)
		{
			return this.Root.eo(A_0);
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x000E3B48 File Offset: 0x000E2B48
		public az b(string A_0)
		{
			return this.Root.a(A_0);
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x000E3B58 File Offset: 0x000E2B58
		public void c(Stream A_0)
		{
			this._property_table.a();
			il il = new il(this.bigBlockSize, this._documents, this._property_table.b());
			ib ib = new ib(this.bigBlockSize);
			ArrayList arrayList = new ArrayList();
			arrayList.AddRange(this._documents);
			arrayList.Add(this._property_table);
			arrayList.Add(il);
			arrayList.Add(il.b());
			foreach (object obj in arrayList)
			{
				cr cr = (cr)obj;
				int num = cr.ap();
				if (num != 0)
				{
					cr.jm(ib.a(num));
				}
			}
			int a_ = ib.a();
			ik ik = new ik(this.bigBlockSize);
			gx[] array = ik.a(ib.ap(), a_);
			ik.b(this._property_table.c());
			ik.c(il.b().b());
			ik.a(il.a());
			ArrayList arrayList2 = new ArrayList();
			arrayList2.Add(ik);
			arrayList2.AddRange(this._documents);
			arrayList2.Add(this._property_table);
			arrayList2.Add(il);
			arrayList2.Add(il.b());
			arrayList2.Add(ib);
			for (int i = 0; i < array.Length; i++)
			{
				arrayList2.Add(array[i]);
			}
			foreach (object obj2 in arrayList2)
			{
				((af)obj2).a3(A_0);
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060030B2 RID: 12466 RVA: 0x000E3CEE File Offset: 0x000E2CEE
		public DirectoryNode Root
		{
			get
			{
				if (this._root == null)
				{
					this._root = new DirectoryNode(this._property_table.b(), this, null);
				}
				return this._root;
			}
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x000E3D16 File Offset: 0x000E2D16
		public void a(eg A_0)
		{
			this._documents.Add(A_0);
			this._property_table.b(A_0.c());
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x000E3D36 File Offset: 0x000E2D36
		public void a(g8 A_0)
		{
			this._property_table.b(A_0);
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x000E3D44 File Offset: 0x000E2D44
		public void a(EntryNode A_0)
		{
			this._property_table.a(A_0.Property);
			if (A_0.IsDocumentEntry)
			{
				this._documents.Remove(((hz)A_0).a());
			}
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x000E3D78 File Offset: 0x000E2D78
		private void a(dc A_0, dc A_1, IEnumerator A_2, DirectoryNode A_3, int A_4)
		{
			while (A_2.MoveNext())
			{
				object obj = A_2.Current;
				ed ed = (ed)obj;
				string a_ = ed.f();
				DirectoryNode directoryNode = (A_3 == null) ? this.Root : A_3;
				if (ed.lj())
				{
					DirectoryNode directoryNode2 = (DirectoryNode)directoryNode.eo(a_);
					directoryNode2.StorageClsid = ed.d();
					this.a(A_0, A_1, ((g8)ed).om(), directoryNode2, A_4);
				}
				else
				{
					int a_2 = ed.i();
					int a_3 = ed.h();
					eg a_4;
					if (ed.g())
					{
						a_4 = new eg(a_, A_0.fc(a_2, A_4), a_3);
					}
					else
					{
						a_4 = new eg(a_, A_1.fc(a_2, A_4), a_3);
					}
					directoryNode.a(a_4);
				}
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060030B7 RID: 12471 RVA: 0x000E3E3E File Offset: 0x000E2E3E
		public Array ViewableArray
		{
			get
			{
				if (this.PreferArray)
				{
					return ((gj)this.Root).ji();
				}
				return new object[0];
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060030B8 RID: 12472 RVA: 0x000E3E5A File Offset: 0x000E2E5A
		public IEnumerator ViewableIterator
		{
			get
			{
				if (!this.PreferArray)
				{
					return ((gj)this.Root).jj();
				}
				return ArrayList.ReadOnly(new ArrayList()).GetEnumerator();
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060030B9 RID: 12473 RVA: 0x000E3E7F File Offset: 0x000E2E7F
		public bool PreferArray
		{
			get
			{
				return ((gj)this.Root).jk();
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060030BA RID: 12474 RVA: 0x000E3E8C File Offset: 0x000E2E8C
		public string ShortDescription
		{
			get
			{
				return "POIFS FileSystem";
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060030BB RID: 12475 RVA: 0x000E3E93 File Offset: 0x000E2E93
		public int BigBlockSize
		{
			get
			{
				return this.bigBlockSize.f();
			}
		}

		// Token: 0x04002030 RID: 8240
		private gz _property_table;

		// Token: 0x04002031 RID: 8241
		private IList _documents;

		// Token: 0x04002032 RID: 8242
		private DirectoryNode _root;

		// Token: 0x04002033 RID: 8243
		private y bigBlockSize = c5.b;
	}
}
