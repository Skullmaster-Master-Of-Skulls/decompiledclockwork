using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web
{
	// Token: 0x0200002B RID: 43
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class HttpFileCollectionWrapper : HttpFileCollectionBase
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x00004F84 File Offset: 0x00003184
		public HttpFileCollectionWrapper(HttpFileCollection httpFileCollection)
		{
			if (httpFileCollection == null)
			{
				throw new ArgumentNullException("httpFileCollection");
			}
			this._collection = httpFileCollection;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00004FA1 File Offset: 0x000031A1
		public override string[] AllKeys
		{
			get
			{
				return this._collection.AllKeys;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00004FAE File Offset: 0x000031AE
		public override int Count
		{
			get
			{
				return ((ICollection)this._collection).Count;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00004FBB File Offset: 0x000031BB
		public override bool IsSynchronized
		{
			get
			{
				return ((ICollection)this._collection).IsSynchronized;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00004FC8 File Offset: 0x000031C8
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return this._collection.Keys;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00004FD5 File Offset: 0x000031D5
		public override object SyncRoot
		{
			get
			{
				return ((ICollection)this._collection).SyncRoot;
			}
		}

		// Token: 0x17000164 RID: 356
		public override HttpPostedFileBase this[string name]
		{
			get
			{
				HttpPostedFile httpPostedFile = this._collection[name];
				if (httpPostedFile == null)
				{
					return null;
				}
				return new HttpPostedFileWrapper(httpPostedFile);
			}
		}

		// Token: 0x17000165 RID: 357
		public override HttpPostedFileBase this[int index]
		{
			get
			{
				HttpPostedFile httpPostedFile = this._collection[index];
				if (httpPostedFile == null)
				{
					return null;
				}
				return new HttpPostedFileWrapper(httpPostedFile);
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00005031 File Offset: 0x00003231
		public override void CopyTo(Array dest, int index)
		{
			this._collection.CopyTo(dest, index);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00005040 File Offset: 0x00003240
		public override HttpPostedFileBase Get(int index)
		{
			HttpPostedFile httpPostedFile = this._collection.Get(index);
			if (httpPostedFile == null)
			{
				return null;
			}
			return new HttpPostedFileWrapper(httpPostedFile);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00005068 File Offset: 0x00003268
		public override HttpPostedFileBase Get(string name)
		{
			HttpPostedFile httpPostedFile = this._collection.Get(name);
			if (httpPostedFile == null)
			{
				return null;
			}
			return new HttpPostedFileWrapper(httpPostedFile);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00005090 File Offset: 0x00003290
		public override IList<HttpPostedFileBase> GetMultiple(string name)
		{
			ICollection<HttpPostedFile> multiple = this._collection.GetMultiple(name);
			return (from f in multiple
			select new HttpPostedFileWrapper(f)).ToList<HttpPostedFileBase>().AsReadOnly();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000050D9 File Offset: 0x000032D9
		public override IEnumerator GetEnumerator()
		{
			return ((IEnumerable)this._collection).GetEnumerator();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000050E6 File Offset: 0x000032E6
		public override string GetKey(int index)
		{
			return this._collection.GetKey(index);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000050F4 File Offset: 0x000032F4
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this._collection.GetObjectData(info, context);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00005103 File Offset: 0x00003303
		public override void OnDeserialization(object sender)
		{
			this._collection.OnDeserialization(sender);
		}

		// Token: 0x0400010B RID: 267
		private HttpFileCollection _collection;
	}
}
