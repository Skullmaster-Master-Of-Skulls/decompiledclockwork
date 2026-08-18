using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace System.Web
{
	// Token: 0x02000022 RID: 34
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class HttpApplicationStateBase : NameObjectCollectionBase, ICollection, IEnumerable
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string[] AllKeys
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpApplicationStateBase Contents
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00003ABB File Offset: 0x00001CBB
		public override int Count
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool IsSynchronized
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual object SyncRoot
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000029 RID: 41
		public virtual object this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700002A RID: 42
		public virtual object this[string name]
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

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpStaticObjectsCollectionBase StaticObjects
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Add(string name, object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Clear()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual object Get(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual object Get(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00003ABB File Offset: 0x00001CBB
		public override IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string GetKey(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Lock()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Remove(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void RemoveAll()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Set(string name, object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void UnLock()
		{
			throw new NotImplementedException();
		}
	}
}
