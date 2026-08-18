using System;
using System.Collections;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000040 RID: 64
	internal sealed class EnumDefinitionIdentity : IEnumerator
	{
		// Token: 0x06000137 RID: 311 RVA: 0x000070DA File Offset: 0x000052DA
		internal EnumDefinitionIdentity(IEnumDefinitionIdentity e)
		{
			if (e == null)
			{
				throw new ArgumentNullException();
			}
			this._enum = e;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000070FE File Offset: 0x000052FE
		private DefinitionIdentity GetCurrent()
		{
			if (this._current == null)
			{
				throw new InvalidOperationException();
			}
			return new DefinitionIdentity(this._current);
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00007119 File Offset: 0x00005319
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00007119 File Offset: 0x00005319
		public DefinitionIdentity Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006C59 File Offset: 0x00004E59
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007121 File Offset: 0x00005321
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

		// Token: 0x0600013D RID: 317 RVA: 0x00007150 File Offset: 0x00005350
		public void Reset()
		{
			this._current = null;
			this._enum.Reset();
		}

		// Token: 0x0400013E RID: 318
		private IEnumDefinitionIdentity _enum;

		// Token: 0x0400013F RID: 319
		private IDefinitionIdentity _current;

		// Token: 0x04000140 RID: 320
		private IDefinitionIdentity[] _fetchList = new IDefinitionIdentity[1];
	}
}
