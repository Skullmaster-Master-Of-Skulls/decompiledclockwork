using System;
using System.Runtime.Remoting.Messaging;
using System.Web;
using Microsoft.Practices.Unity;

namespace TechnoPro.Common.Unity.IoC
{
	// Token: 0x0200000B RID: 11
	public class PerRegistrationContextLifeTimeManager : LifetimeManager, IDisposable
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00003338 File Offset: 0x00001538
		public PerRegistrationContextLifeTimeManager()
		{
			this._key = string.Format("PerCallContextOrRequestLifeTimeManager_{0}", Guid.NewGuid());
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000335C File Offset: 0x0000155C
		public override object GetValue()
		{
			return (HttpContext.Current != null && HttpContext.Current.Session != null) ? this.GetFromHttpContext() : this.GetFromCallContext();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003390 File Offset: 0x00001590
		public override void SetValue(object newValue)
		{
			bool flag = HttpContext.Current != null && HttpContext.Current.Session != null;
			if (flag)
			{
				this.SetInHttpContext(newValue);
			}
			else
			{
				this.SetInCallContext(newValue);
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000033CC File Offset: 0x000015CC
		public override void RemoveValue()
		{
			bool flag = HttpContext.Current != null && HttpContext.Current.Session != null;
			if (flag)
			{
				this.RemoveFromHttpContext();
			}
			else
			{
				this.RemoveFromCallContext();
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003405 File Offset: 0x00001605
		private void RemoveFromHttpContext()
		{
			HttpContext.Current.Session.Remove(this._key);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000341E File Offset: 0x0000161E
		private void RemoveFromCallContext()
		{
			CallContext.FreeNamedDataSlot(this._key);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000342D File Offset: 0x0000162D
		private void SetInHttpContext(object newValue)
		{
			HttpContext.Current.Session[this._key] = newValue;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003447 File Offset: 0x00001647
		private void SetInCallContext(object newValue)
		{
			CallContext.SetData(this._key, newValue);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003458 File Offset: 0x00001658
		private object GetFromCallContext()
		{
			return CallContext.GetData(this._key);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003478 File Offset: 0x00001678
		private object GetFromHttpContext()
		{
			return HttpContext.Current.Session[this._key];
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000349F File Offset: 0x0000169F
		public void Dispose()
		{
			this.RemoveValue();
		}

		// Token: 0x0400000C RID: 12
		private readonly string _key;
	}
}
