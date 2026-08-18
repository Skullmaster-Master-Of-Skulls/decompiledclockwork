using System;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000023 RID: 35
	internal class OpoUdtCtx
	{
		// Token: 0x06000165 RID: 357 RVA: 0x00014358 File Offset: 0x00013358
		internal OpoUdtCtx(IntPtr opsConCtx, IntPtr pUDT, IntPtr pOCIRef, IntPtr pObjInd)
		{
			try
			{
				int num = OpsCon.AddRef(opsConCtx);
				if (num <= 1)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			this.m_opsConCtx = opsConCtx;
			OpsUdt.SetSig(opsConCtx, out this.m_SessionBegin);
			this.m_pUDT = pUDT;
			this.m_pOCIRef = pOCIRef;
			this.m_pObjInd = pObjInd;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000143DC File Offset: 0x000133DC
		internal void AddRefCount()
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			lock (this)
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				this.m_refCount++;
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00014454 File Offset: 0x00013454
		internal void RelRefCount()
		{
			if (!this.m_disposed)
			{
				lock (this)
				{
					if (!this.m_disposed)
					{
						this.m_refCount--;
						if (this.m_refCount <= 0)
						{
							this.Dispose();
							GC.SuppressFinalize(this);
						}
					}
				}
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000144BC File Offset: 0x000134BC
		private void Dispose()
		{
			if (!this.m_disposed)
			{
				try
				{
					if (this.m_pUDT != IntPtr.Zero || this.m_pOCIRef != IntPtr.Zero || this.m_pAttrRefTdo != IntPtr.Zero || this.m_pAttrTdo != IntPtr.Zero)
					{
						OpsUdt.Dispose(this.m_opsConCtx, this.m_SessionBegin, ref this.m_pUDT, ref this.m_pOCIRef, ref this.m_pAttrRefTdo, ref this.m_pAttrTdo);
					}
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
				}
				try
				{
					OpsCon.RelRef(ref this.m_opsConCtx);
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00014598 File Offset: 0x00013598
		~OpoUdtCtx()
		{
			this.Dispose();
		}

		// Token: 0x040000F5 RID: 245
		public IntPtr m_opsConCtx;

		// Token: 0x040000F6 RID: 246
		public IntPtr m_pUDT;

		// Token: 0x040000F7 RID: 247
		public IntPtr m_pOCIRef;

		// Token: 0x040000F8 RID: 248
		public IntPtr m_pObjInd;

		// Token: 0x040000F9 RID: 249
		public IntPtr m_pAttrRefTdo;

		// Token: 0x040000FA RID: 250
		public IntPtr m_pAttrTdo;

		// Token: 0x040000FB RID: 251
		public bool m_disposed;

		// Token: 0x040000FC RID: 252
		internal int m_refCount;

		// Token: 0x040000FD RID: 253
		internal int m_IsPinned;

		// Token: 0x040000FE RID: 254
		internal int m_pinLatest;

		// Token: 0x040000FF RID: 255
		internal int m_SessionBegin;
	}
}
