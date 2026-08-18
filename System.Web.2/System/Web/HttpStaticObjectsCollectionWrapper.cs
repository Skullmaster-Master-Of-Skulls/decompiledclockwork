using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Web
{
	// Token: 0x02000037 RID: 55
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class HttpStaticObjectsCollectionWrapper : HttpStaticObjectsCollectionBase
	{
		// Token: 0x060004C1 RID: 1217 RVA: 0x00005E45 File Offset: 0x00004045
		public HttpStaticObjectsCollectionWrapper(HttpStaticObjectsCollection httpStaticObjectsCollection)
		{
			if (httpStaticObjectsCollection == null)
			{
				throw new ArgumentNullException("httpStaticObjectsCollection");
			}
			this._collection = httpStaticObjectsCollection;
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00005E62 File Offset: 0x00004062
		public override int Count
		{
			get
			{
				return this._collection.Count;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00005E6F File Offset: 0x0000406F
		public override bool IsReadOnly
		{
			get
			{
				return this._collection.IsReadOnly;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00005E7C File Offset: 0x0000407C
		public override bool IsSynchronized
		{
			get
			{
				return this._collection.IsSynchronized;
			}
		}

		// Token: 0x17000233 RID: 563
		public override object this[string name]
		{
			get
			{
				return this._collection[name];
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00005E97 File Offset: 0x00004097
		public override bool NeverAccessed
		{
			get
			{
				return this._collection.NeverAccessed;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00005EA4 File Offset: 0x000040A4
		public override object SyncRoot
		{
			get
			{
				return this._collection.SyncRoot;
			}
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00005EB1 File Offset: 0x000040B1
		public override void CopyTo(Array array, int index)
		{
			this._collection.CopyTo(array, index);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00005EC0 File Offset: 0x000040C0
		public override IEnumerator GetEnumerator()
		{
			return this._collection.GetEnumerator();
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00005ECD File Offset: 0x000040CD
		public override object GetObject(string name)
		{
			return this._collection.GetObject(name);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00005EDB File Offset: 0x000040DB
		public override void Serialize(BinaryWriter writer)
		{
			this._collection.Serialize(writer);
		}

		// Token: 0x04000111 RID: 273
		private HttpStaticObjectsCollection _collection;
	}
}
