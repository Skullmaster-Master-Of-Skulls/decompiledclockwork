using System;
using System.Collections;
using System.Diagnostics;

namespace System.Xml.XPath
{
	// Token: 0x020000BE RID: 190
	[DebuggerDisplay("Position={CurrentPosition}, Current={debuggerDisplayProxy}")]
	public abstract class XPathNodeIterator : ICloneable, IEnumerable
	{
		// Token: 0x06000B58 RID: 2904 RVA: 0x00034CAC File Offset: 0x00033CAC
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06000B59 RID: 2905
		public abstract XPathNodeIterator Clone();

		// Token: 0x06000B5A RID: 2906
		public abstract bool MoveNext();

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000B5B RID: 2907
		public abstract XPathNavigator Current { get; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000B5C RID: 2908
		public abstract int CurrentPosition { get; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x00034CB4 File Offset: 0x00033CB4
		public virtual int Count
		{
			get
			{
				if (this.count == -1)
				{
					XPathNodeIterator xpathNodeIterator = this.Clone();
					while (xpathNodeIterator.MoveNext())
					{
					}
					this.count = xpathNodeIterator.CurrentPosition;
				}
				return this.count;
			}
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00034CEB File Offset: 0x00033CEB
		public virtual IEnumerator GetEnumerator()
		{
			return new XPathNodeIterator.Enumerator(this);
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x00034CF3 File Offset: 0x00033CF3
		private object debuggerDisplayProxy
		{
			get
			{
				if (this.Current != null)
				{
					return new XPathNavigator.DebuggerDisplayProxy(this.Current);
				}
				return null;
			}
		}

		// Token: 0x040008DB RID: 2267
		internal int count = -1;

		// Token: 0x020000BF RID: 191
		private class Enumerator : IEnumerator
		{
			// Token: 0x06000B61 RID: 2913 RVA: 0x00034D1E File Offset: 0x00033D1E
			public Enumerator(XPathNodeIterator original)
			{
				this.original = original.Clone();
			}

			// Token: 0x17000277 RID: 631
			// (get) Token: 0x06000B62 RID: 2914 RVA: 0x00034D34 File Offset: 0x00033D34
			public virtual object Current
			{
				get
				{
					if (!this.iterationStarted)
					{
						throw new InvalidOperationException(Res.GetString("Sch_EnumNotStarted", new object[]
						{
							string.Empty
						}));
					}
					if (this.current == null)
					{
						throw new InvalidOperationException(Res.GetString("Sch_EnumFinished", new object[]
						{
							string.Empty
						}));
					}
					return this.current.Current.Clone();
				}
			}

			// Token: 0x06000B63 RID: 2915 RVA: 0x00034DA4 File Offset: 0x00033DA4
			public virtual bool MoveNext()
			{
				if (!this.iterationStarted)
				{
					this.current = this.original.Clone();
					this.iterationStarted = true;
				}
				if (this.current == null || !this.current.MoveNext())
				{
					this.current = null;
					return false;
				}
				return true;
			}

			// Token: 0x06000B64 RID: 2916 RVA: 0x00034DF0 File Offset: 0x00033DF0
			public virtual void Reset()
			{
				this.iterationStarted = false;
			}

			// Token: 0x040008DC RID: 2268
			private XPathNodeIterator original;

			// Token: 0x040008DD RID: 2269
			private XPathNodeIterator current;

			// Token: 0x040008DE RID: 2270
			private bool iterationStarted;
		}
	}
}
