using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000179 RID: 377
	internal class EnumerableWrapperWeakToStrong : IEnumerable<object>, IEnumerable
	{
		// Token: 0x06000DDF RID: 3551 RVA: 0x00031202 File Offset: 0x0002F402
		internal EnumerableWrapperWeakToStrong(IEnumerable wrappedEnumerable)
		{
			this.m_wrappedEnumerable = wrappedEnumerable;
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00031211 File Offset: 0x0002F411
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<object>)this).GetEnumerator();
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00031219 File Offset: 0x0002F419
		public IEnumerator<object> GetEnumerator()
		{
			return new EnumerableWrapperWeakToStrong.WrapperEnumeratorWeakToStrong(this.m_wrappedEnumerable.GetEnumerator());
		}

		// Token: 0x04000814 RID: 2068
		private readonly IEnumerable m_wrappedEnumerable;

		// Token: 0x020003AC RID: 940
		private class WrapperEnumeratorWeakToStrong : IEnumerator<object>, IDisposable, IEnumerator
		{
			// Token: 0x06001D3A RID: 7482 RVA: 0x00067F83 File Offset: 0x00066183
			internal WrapperEnumeratorWeakToStrong(IEnumerator wrappedEnumerator)
			{
				this.m_wrappedEnumerator = wrappedEnumerator;
			}

			// Token: 0x1700055F RID: 1375
			// (get) Token: 0x06001D3B RID: 7483 RVA: 0x00067F92 File Offset: 0x00066192
			object IEnumerator.Current
			{
				get
				{
					return this.m_wrappedEnumerator.Current;
				}
			}

			// Token: 0x17000560 RID: 1376
			// (get) Token: 0x06001D3C RID: 7484 RVA: 0x00067F9F File Offset: 0x0006619F
			object IEnumerator<object>.Current
			{
				get
				{
					return this.m_wrappedEnumerator.Current;
				}
			}

			// Token: 0x06001D3D RID: 7485 RVA: 0x00067FAC File Offset: 0x000661AC
			void IDisposable.Dispose()
			{
				IDisposable disposable = this.m_wrappedEnumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}

			// Token: 0x06001D3E RID: 7486 RVA: 0x00067FCE File Offset: 0x000661CE
			bool IEnumerator.MoveNext()
			{
				return this.m_wrappedEnumerator.MoveNext();
			}

			// Token: 0x06001D3F RID: 7487 RVA: 0x00067FDB File Offset: 0x000661DB
			void IEnumerator.Reset()
			{
				this.m_wrappedEnumerator.Reset();
			}

			// Token: 0x040010FC RID: 4348
			private IEnumerator m_wrappedEnumerator;
		}
	}
}
