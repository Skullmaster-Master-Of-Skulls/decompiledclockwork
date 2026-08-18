using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using MailBee;

namespace a
{
	// Token: 0x020004A7 RID: 1191
	internal class ad : NetworkStream
	{
		// Token: 0x060028AD RID: 10413 RVA: 0x000BD2CF File Offset: 0x000BC2CF
		public ad(Socket A_0, ah A_1) : base(A_0)
		{
			this.a = A_1;
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x000BD2E0 File Offset: 0x000BC2E0
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.a.h();
			if (this.a.h0() != null && !this.a.h0()())
			{
				throw new MailBeeUserAbortException(5);
			}
			int num = 0;
			try
			{
				num = base.Read(buffer, offset, count);
			}
			catch (IOException a_)
			{
				this.a.g(a_);
			}
			catch (ObjectDisposedException a_2)
			{
				this.a.g(a_2);
			}
			if (this.d() != null && this.a.f.c() != null)
			{
				this.d()(buffer, offset, num, this.a.f.c());
			}
			return num;
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x000BD3A0 File Offset: 0x000BC3A0
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.a.h();
			if (this.a.h0() != null && !this.a.h0()())
			{
				throw new MailBeeUserAbortException(5);
			}
			try
			{
				base.Write(buffer, offset, count);
			}
			catch (IOException a_)
			{
				this.a.g(a_);
			}
			catch (ObjectDisposedException a_2)
			{
				this.a.g(a_2);
			}
			if (this.c() != null && this.a.f.c() != null)
			{
				this.c()(buffer, offset, count, this.a.f.c());
			}
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x000BD45C File Offset: 0x000BC45C
		public override void WriteByte(byte value)
		{
			this.a.h();
			if (this.a.h0() != null && !this.a.h0()())
			{
				throw new MailBeeUserAbortException(5);
			}
			try
			{
				base.WriteByte(value);
			}
			catch (IOException a_)
			{
				this.a.g(a_);
			}
			catch (ObjectDisposedException a_2)
			{
				this.a.g(a_2);
			}
			if (this.c() != null && this.a.f.c() != null)
			{
				this.c()(new byte[]
				{
					value
				}, 0, 1, this.a.f.c());
			}
		}

		// Token: 0x060028B1 RID: 10417 RVA: 0x000BD520 File Offset: 0x000BC520
		private a1 d()
		{
			return this.a.a8();
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x000BD52D File Offset: 0x000BC52D
		private bd c()
		{
			return this.a.ba();
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x000BD53A File Offset: 0x000BC53A
		private ak b()
		{
			return this.a.h4();
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x000BD547 File Offset: 0x000BC547
		private bl a()
		{
			return this.a.h6();
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x000BD554 File Offset: 0x000BC554
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.a.h();
			if (this.a.h0() != null && !this.a.h0()())
			{
				throw new MailBeeUserAbortException(5);
			}
			this.b = buffer;
			this.c = offset;
			this.d = size;
			this.e = this.a.g(TimeSpan.FromMilliseconds((double)this.a.hy()));
			try
			{
				return base.BeginRead(buffer, offset, size, callback, state);
			}
			catch (IOException a_)
			{
				this.e.Dispose();
				this.e = null;
				this.a.g(a_);
			}
			catch (ObjectDisposedException a_2)
			{
				this.e.Dispose();
				this.e = null;
				this.a.g(a_2);
			}
			return null;
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x000BD63C File Offset: 0x000BC63C
		public override int EndRead(IAsyncResult asyncResult)
		{
			int num = 0;
			byte[] a_ = this.b;
			int a_2 = this.c;
			int num2 = this.d;
			this.b = null;
			try
			{
				num = base.EndRead(asyncResult);
			}
			catch (IOException a_3)
			{
				this.a.g(a_3);
			}
			catch (ObjectDisposedException a_4)
			{
				this.a.g(a_4);
			}
			finally
			{
				this.e.Dispose();
				this.e = null;
			}
			if (this.d() != null && this.a.f.c() != null)
			{
				this.d()(a_, a_2, num, this.a.f.c());
			}
			return num;
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x000BD708 File Offset: 0x000BC708
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.a.h();
			if (this.a.h0() != null && !this.a.h0()())
			{
				throw new MailBeeUserAbortException(5);
			}
			this.b = buffer;
			this.c = offset;
			this.d = size;
			this.e = this.a.g(TimeSpan.FromMilliseconds((double)this.a.hy()));
			try
			{
				return base.BeginWrite(buffer, offset, size, callback, state);
			}
			catch (IOException a_)
			{
				this.e.Dispose();
				this.e = null;
				this.a.g(a_);
			}
			catch (ObjectDisposedException a_2)
			{
				this.e.Dispose();
				this.e = null;
				this.a.g(a_2);
			}
			return null;
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x000BD7F0 File Offset: 0x000BC7F0
		public override void EndWrite(IAsyncResult asyncResult)
		{
			byte[] a_ = this.b;
			int a_2 = this.c;
			int a_3 = this.d;
			this.b = null;
			try
			{
				base.EndWrite(asyncResult);
			}
			catch (IOException a_4)
			{
				this.a.g(a_4);
			}
			catch (ObjectDisposedException a_5)
			{
				this.a.g(a_5);
			}
			finally
			{
				this.e.Dispose();
				this.e = null;
			}
			if (this.c() != null && this.a.f.c() != null)
			{
				this.c()(a_, a_2, a_3, this.a.f.c());
			}
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x000BD8B8 File Offset: 0x000BC8B8
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			Func<IAsyncResult, int> endMethod = new Func<IAsyncResult, int>(this.EndRead);
			return Task.Factory.FromAsync<int>(this.BeginRead(buffer, offset, count, null, null), endMethod);
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x000BD8EC File Offset: 0x000BC8EC
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			Action<IAsyncResult> endMethod = new Action<IAsyncResult>(this.EndWrite);
			return Task.Factory.FromAsync(this.BeginWrite(buffer, offset, count, null, null), endMethod);
		}

		// Token: 0x04001BB7 RID: 7095
		private ah a;

		// Token: 0x04001BB8 RID: 7096
		private byte[] b;

		// Token: 0x04001BB9 RID: 7097
		private int c;

		// Token: 0x04001BBA RID: 7098
		private int d;

		// Token: 0x04001BBB RID: 7099
		private @as e;
	}
}
