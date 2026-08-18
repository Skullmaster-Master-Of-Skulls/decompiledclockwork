using System;
using System.Collections;
using System.Diagnostics;
using System.Text;

namespace System.Xml.XPath
{
	// Token: 0x020002EF RID: 751
	[DebuggerDisplay("Position={CurrentPosition}, Current={debuggerDisplayProxy}")]
	public abstract class XPathNodeIterator : ICloneable, IEnumerable
	{
		// Token: 0x06002D42 RID: 11586 RVA: 0x000EC0BC File Offset: 0x000EA2BC
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06002D43 RID: 11587
		public abstract XPathNodeIterator Clone();

		// Token: 0x06002D44 RID: 11588
		public abstract bool MoveNext();

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06002D45 RID: 11589
		public abstract XPathNavigator Current { get; }

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06002D46 RID: 11590
		public abstract int CurrentPosition { get; }

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06002D47 RID: 11591 RVA: 0x000EC0C4 File Offset: 0x000EA2C4
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

		// Token: 0x06002D48 RID: 11592 RVA: 0x000EC0FB File Offset: 0x000EA2FB
		public virtual IEnumerator GetEnumerator()
		{
			return new XPathNodeIterator.Enumerator(this);
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06002D49 RID: 11593 RVA: 0x000EC103 File Offset: 0x000EA303
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

		// Token: 0x0400137A RID: 4986
		internal int count = -1;

		// Token: 0x020004BF RID: 1215
		private class Enumerator : IEnumerator
		{
			// Token: 0x060031AA RID: 12714 RVA: 0x00120EBC File Offset: 0x0011F0BC
			public Enumerator(XPathNodeIterator original)
			{
				this.original = original.Clone();
			}

			// Token: 0x17000A79 RID: 2681
			// (get) Token: 0x060031AB RID: 12715 RVA: 0x00120ED0 File Offset: 0x0011F0D0
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

			// Token: 0x060031AC RID: 12716 RVA: 0x00120F3C File Offset: 0x0011F13C
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

			// Token: 0x060031AD RID: 12717 RVA: 0x00120F88 File Offset: 0x0011F188
			public virtual void Reset()
			{
				this.iterationStarted = false;
			}

			// Token: 0x04001F99 RID: 8089
			private XPathNodeIterator original;

			// Token: 0x04001F9A RID: 8090
			private XPathNodeIterator current;

			// Token: 0x04001F9B RID: 8091
			private bool iterationStarted;
		}

		// Token: 0x020004C0 RID: 1216
		private struct DebuggerDisplayProxy
		{
			// Token: 0x060031AE RID: 12718 RVA: 0x00120F91 File Offset: 0x0011F191
			public DebuggerDisplayProxy(XPathNodeIterator nodeIterator)
			{
				this.nodeIterator = nodeIterator;
			}

			// Token: 0x060031AF RID: 12719 RVA: 0x00120F9C File Offset: 0x0011F19C
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("Position=");
				stringBuilder.Append(this.nodeIterator.CurrentPosition);
				stringBuilder.Append(", Current=");
				if (this.nodeIterator.Current == null)
				{
					stringBuilder.Append("null");
				}
				else
				{
					stringBuilder.Append('{');
					stringBuilder.Append(new XPathNavigator.DebuggerDisplayProxy(this.nodeIterator.Current).ToString());
					stringBuilder.Append('}');
				}
				return stringBuilder.ToString();
			}

			// Token: 0x04001F9C RID: 8092
			private XPathNodeIterator nodeIterator;
		}
	}
}
