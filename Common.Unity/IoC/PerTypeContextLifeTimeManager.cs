using System;
using System.Runtime.Remoting.Messaging;
using System.Web;
using Microsoft.Practices.Unity;

namespace TechnoPro.Common.Unity.IoC
{
	// Token: 0x0200000C RID: 12
	public class PerTypeContextLifeTimeManager<T> : LifetimeManager, IDisposable
	{
		// Token: 0x06000048 RID: 72 RVA: 0x000034A9 File Offset: 0x000016A9
		public PerTypeContextLifeTimeManager()
		{
			this._key = typeof(T).AssemblyQualifiedName;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000034C8 File Offset: 0x000016C8
		public override object GetValue()
		{
			return (HttpContext.Current != null && HttpContext.Current.Session != null) ? this.GetFromHttpContext() : this.GetFromCallContext();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000034FC File Offset: 0x000016FC
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

		// Token: 0x0600004B RID: 75 RVA: 0x00003538 File Offset: 0x00001738
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

		// Token: 0x0600004C RID: 76 RVA: 0x00003571 File Offset: 0x00001771
		private void RemoveFromHttpContext()
		{
			HttpContext.Current.Session.Remove(this._key);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000358A File Offset: 0x0000178A
		private void RemoveFromCallContext()
		{
			CallContext.FreeNamedDataSlot(this._key);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003599 File Offset: 0x00001799
		private void SetInHttpContext(object newValue)
		{
			HttpContext.Current.Session[this._key] = newValue;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000035B3 File Offset: 0x000017B3
		private void SetInCallContext(object newValue)
		{
			CallContext.SetData(this._key, newValue);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000035C4 File Offset: 0x000017C4
		private object GetFromCallContext()
		{
			return CallContext.GetData(this._key);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000035E4 File Offset: 0x000017E4
		private object GetFromHttpContext()
		{
			return HttpContext.Current.Session[this._key];
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000349F File Offset: 0x0000169F
		public void Dispose()
		{
			this.RemoveValue();
		}

		// Token: 0x0400000D RID: 13
		private readonly string _key;
	}
}
