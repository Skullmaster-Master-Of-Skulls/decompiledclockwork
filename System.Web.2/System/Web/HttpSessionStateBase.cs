using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Web.SessionState;

namespace System.Web
{
	// Token: 0x02000034 RID: 52
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class HttpSessionStateBase : ICollection, IEnumerable
	{
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int CodePage
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

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpSessionStateBase Contents
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpCookieMode CookieMode
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool IsCookieless
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool IsNewSession
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool IsReadOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int LCID
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

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual SessionStateMode Mode
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string SessionID
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpStaticObjectsCollectionBase StaticObjects
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int Timeout
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

		// Token: 0x17000214 RID: 532
		public virtual object this[int index]
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

		// Token: 0x17000215 RID: 533
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

		// Token: 0x0600048B RID: 1163 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Abandon()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Add(string name, object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Clear()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Remove(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void RemoveAll()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int Count
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool IsSynchronized
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual object SyncRoot
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
