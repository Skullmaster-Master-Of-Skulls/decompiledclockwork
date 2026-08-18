using System;
using System.Collections;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000042 RID: 66
	internal sealed class EnumReferenceIdentity : IEnumerator
	{
		// Token: 0x06000142 RID: 322 RVA: 0x00007164 File Offset: 0x00005364
		internal EnumReferenceIdentity(IEnumReferenceIdentity e)
		{
			this._enum = e;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000717F File Offset: 0x0000537F
		private ReferenceIdentity GetCurrent()
		{
			if (this._current == null)
			{
				throw new InvalidOperationException();
			}
			return new ReferenceIdentity(this._current);
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000719A File Offset: 0x0000539A
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000719A File Offset: 0x0000539A
		public ReferenceIdentity Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006C59 File Offset: 0x00004E59
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000071A2 File Offset: 0x000053A2
		public bool MoveNext()
		{
			if (this._enum.Next(1U, this._fetchList) == 1U)
			{
				this._current = this._fetchList[0];
				return true;
			}
			this._current = null;
			return false;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000071D1 File Offset: 0x000053D1
		public void Reset()
		{
			this._current = null;
			this._enum.Reset();
		}

		// Token: 0x04000141 RID: 321
		private IEnumReferenceIdentity _enum;

		// Token: 0x04000142 RID: 322
		private IReferenceIdentity _current;

		// Token: 0x04000143 RID: 323
		private IReferenceIdentity[] _fetchList = new IReferenceIdentity[1];
	}
}
