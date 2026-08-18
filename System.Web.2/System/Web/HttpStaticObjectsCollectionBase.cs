using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Web
{
	// Token: 0x02000036 RID: 54
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class HttpStaticObjectsCollectionBase : ICollection, IEnumerable
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int Count
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool IsReadOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool IsSynchronized
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700022D RID: 557
		public virtual object this[string name]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool NeverAccessed
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual object SyncRoot
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual object GetObject(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Serialize(BinaryWriter writer)
		{
			throw new NotImplementedException();
		}
	}
}
