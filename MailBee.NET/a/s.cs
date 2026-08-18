using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace a
{
	// Token: 0x020004A8 RID: 1192
	internal class s : Stream
	{
		// Token: 0x060028BB RID: 10427 RVA: 0x000BD91D File Offset: 0x000BC91D
		public s(Stream A_0, r A_1)
		{
			this.a = A_0;
			this.b = A_1;
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x000BD933 File Offset: 0x000BC933
		public override void Flush()
		{
			this.a.Flush();
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x000BD940 File Offset: 0x000BC940
		public override void SetLength(long value)
		{
			this.a.SetLength(value);
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x000BD94E File Offset: 0x000BC94E
		public override long get_Length()
		{
			return this.a.Length;
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x000BD95B File Offset: 0x000BC95B
		public override long get_Position()
		{
			return this.a.Position;
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x000BD968 File Offset: 0x000BC968
		public override void set_Position(long value)
		{
			value = this.a.Position;
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x000BD977 File Offset: 0x000BC977
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.a.Seek(offset, origin);
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x000BD986 File Offset: 0x000BC986
		public override bool get_CanRead()
		{
			return this.a.CanRead;
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x000BD993 File Offset: 0x000BC993
		public override bool get_CanSeek()
		{
			return this.a.CanSeek;
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x000BD9A0 File Offset: 0x000BC9A0
		public override bool get_CanWrite()
		{
			return this.a.CanWrite;
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x000BD9B0 File Offset: 0x000BC9B0
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this.b.g(buffer, offset, count);
			if (num > 0)
			{
				return num;
			}
			return this.a.Read(buffer, offset, count);
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x000BD9E0 File Offset: 0x000BC9E0
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.a.Write(buffer, offset, count);
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x000BD9F0 File Offset: 0x000BC9F0
		public override void WriteByte(byte value)
		{
			this.a.WriteByte(value);
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x000BD9FE File Offset: 0x000BC9FE
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return base.ReadAsync(buffer, offset, count).a(callback, state);
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x000BDA12 File Offset: 0x000BCA12
		public override int EndRead(IAsyncResult asyncResult)
		{
			return asyncResult.b<int>();
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x000BDA1C File Offset: 0x000BCA1C
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			int num = this.b.g(buffer, offset, count);
			if (num > 0)
			{
				return Task.FromResult<int>(num);
			}
			return this.a.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x000BDA53 File Offset: 0x000BCA53
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return base.WriteAsync(buffer, offset, count).b(callback, state);
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x000BDA67 File Offset: 0x000BCA67
		public override void EndWrite(IAsyncResult asyncResult)
		{
			asyncResult.a();
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x000BDA6F File Offset: 0x000BCA6F
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this.a.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x04001BBC RID: 7100
		private Stream a;

		// Token: 0x04001BBD RID: 7101
		private r b;
	}
}
