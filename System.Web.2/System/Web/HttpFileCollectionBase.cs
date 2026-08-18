using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace System.Web
{
	// Token: 0x0200002A RID: 42
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class HttpFileCollectionBase : NameObjectCollectionBase, ICollection, IEnumerable
	{
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string[] AllKeys
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00003ABB File Offset: 0x00001CBB
		public override int Count
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool IsSynchronized
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual object SyncRoot
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700015D RID: 349
		public virtual HttpPostedFileBase this[string name]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700015E RID: 350
		public virtual HttpPostedFileBase this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void CopyTo(Array dest, int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpPostedFileBase Get(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpPostedFileBase Get(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual IList<HttpPostedFileBase> GetMultiple(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00003ABB File Offset: 0x00001CBB
		public override IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string GetKey(int index)
		{
			throw new NotImplementedException();
		}
	}
}
