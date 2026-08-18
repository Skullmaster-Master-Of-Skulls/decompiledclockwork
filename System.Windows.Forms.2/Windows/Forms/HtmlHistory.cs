using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000282 RID: 642
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class HtmlHistory : IDisposable
	{
		// Token: 0x06002912 RID: 10514 RVA: 0x000BCCEE File Offset: 0x000BAEEE
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		internal HtmlHistory(UnsafeNativeMethods.IOmHistory history)
		{
			this.htmlHistory = history;
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06002913 RID: 10515 RVA: 0x000BCCFD File Offset: 0x000BAEFD
		private UnsafeNativeMethods.IOmHistory NativeOmHistory
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				return this.htmlHistory;
			}
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x000BCD1E File Offset: 0x000BAF1E
		public void Dispose()
		{
			this.htmlHistory = null;
			this.disposed = true;
			GC.SuppressFinalize(this);
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06002915 RID: 10517 RVA: 0x000BCD34 File Offset: 0x000BAF34
		public int Length
		{
			get
			{
				return (int)this.NativeOmHistory.GetLength();
			}
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x000BCD44 File Offset: 0x000BAF44
		public void Back(int numberBack)
		{
			if (numberBack < 0)
			{
				throw new ArgumentOutOfRangeException("numberBack", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"numberBack",
					numberBack.ToString(CultureInfo.CurrentCulture),
					0.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (numberBack > 0)
			{
				object obj = -numberBack;
				this.NativeOmHistory.Go(ref obj);
			}
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x000BCDB4 File Offset: 0x000BAFB4
		public void Forward(int numberForward)
		{
			if (numberForward < 0)
			{
				throw new ArgumentOutOfRangeException("numberForward", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"numberForward",
					numberForward.ToString(CultureInfo.CurrentCulture),
					0.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (numberForward > 0)
			{
				object obj = numberForward;
				this.NativeOmHistory.Go(ref obj);
			}
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x000BCE20 File Offset: 0x000BB020
		public void Go(Uri url)
		{
			this.Go(url.ToString());
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x000BCE30 File Offset: 0x000BB030
		public void Go(string urlString)
		{
			object obj = urlString;
			this.NativeOmHistory.Go(ref obj);
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x000BCE4C File Offset: 0x000BB04C
		public void Go(int relativePosition)
		{
			object obj = relativePosition;
			this.NativeOmHistory.Go(ref obj);
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x0600291B RID: 10523 RVA: 0x000BCE6D File Offset: 0x000BB06D
		public object DomHistory
		{
			get
			{
				return this.NativeOmHistory;
			}
		}

		// Token: 0x040010D9 RID: 4313
		private UnsafeNativeMethods.IOmHistory htmlHistory;

		// Token: 0x040010DA RID: 4314
		private bool disposed;
	}
}
