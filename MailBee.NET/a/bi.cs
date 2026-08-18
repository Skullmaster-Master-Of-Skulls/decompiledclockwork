using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using MailBee;

namespace a
{
	// Token: 0x02000484 RID: 1156
	internal class bi : SaslMethod, IDisposable
	{
		// Token: 0x060027D9 RID: 10201 RVA: 0x000B8F49 File Offset: 0x000B7F49
		public bi()
		{
			base.ExpectBase64Challenge = false;
			this.d = false;
			this.c = false;
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x000B8F66 File Offset: 0x000B7F66
		private new bool a()
		{
			return this.c;
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x000B8F6E File Offset: 0x000B7F6E
		private new void a(bool A_0)
		{
			this.c = A_0;
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x000B8F77 File Offset: 0x000B7F77
		internal override void set_TargetNameInternal(string value)
		{
			base.a((value == null) ? (base.ServiceName + "/" + base.ServerName) : value);
		}

		// Token: 0x060027DD RID: 10205 RVA: 0x000B8F9B File Offset: 0x000B7F9B
		protected virtual ProtectionLevel cd()
		{
			return ProtectionLevel.EncryptAndSign;
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x000B8F9E File Offset: 0x000B7F9E
		public override bool IsSecure()
		{
			return true;
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x000B8FA1 File Offset: 0x000B7FA1
		public override bool RequiresCredentials()
		{
			return false;
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x000B8FA4 File Offset: 0x000B7FA4
		public override string GetSaslID()
		{
			return "GSSAPI";
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x000B8FAB File Offset: 0x000B7FAB
		internal override AuthenticationMethods GetMethodEnumMember()
		{
			return AuthenticationMethods.SaslGssApi;
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x000B8FB2 File Offset: 0x000B7FB2
		public override void CreateNextClientAnswer()
		{
			throw new NotSupportedException("NegotiateSaslMethod.CreateNextClientAnswer");
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x000B8FC0 File Offset: 0x000B7FC0
		public new void c(bool A_0)
		{
			byte[] array = new byte[64];
			NetworkCredential credential;
			if (base.AccountName == null || base.AccountName == string.Empty)
			{
				credential = (NetworkCredential)CredentialCache.DefaultCredentials;
			}
			else
			{
				base.h();
				string domain = base.e();
				credential = new NetworkCredential(base.AccountName, base.Password, domain);
			}
			try
			{
				this.b = new bi.b(this, A_0);
				this.a = new NegotiateStream(this.b);
				this.a.AuthenticateAsClient(credential, base.TargetName, this.cd(), TokenImpersonationLevel.Impersonation);
				if (this.a())
				{
					this.b.a(this.b.b() == 22);
					try
					{
						int count = this.a.Read(array, 0, array.Length);
						this.a.Write(array, 0, count);
					}
					catch (IOException ex)
					{
						Exception ex2 = ex.InnerException as MailBeeException;
						if (ex2 != null)
						{
							throw ex2;
						}
						base.ClientAnswer = new byte[0];
						this.b()(this, false);
					}
				}
			}
			finally
			{
				base.Dispose();
			}
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x000B90F0 File Offset: 0x000B80F0
		protected override void Dispose(bool disposing)
		{
			if (!this.d)
			{
				if (disposing && this.a != null)
				{
					this.a.Dispose();
					this.a = null;
					this.b = null;
				}
				this.d = true;
			}
			base.Dispose(disposing);
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x000B912C File Offset: 0x000B812C
		~bi()
		{
			this.Dispose(false);
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x000B915C File Offset: 0x000B815C
		internal new u.d b()
		{
			return this.e;
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x000B9164 File Offset: 0x000B8164
		internal new void a(u.d A_0)
		{
			this.e = A_0;
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x000B9170 File Offset: 0x000B8170
		public new Task b(bool A_0)
		{
			bi.a a;
			a.c = this;
			a.d = A_0;
			a.b = AsyncTaskMethodBuilder.Create();
			a.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<bi.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x000B91BD File Offset: 0x000B81BD
		internal new u.f c()
		{
			return this.f;
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x000B91C5 File Offset: 0x000B81C5
		internal new void a(u.f A_0)
		{
			this.f = A_0;
		}

		// Token: 0x04001B43 RID: 6979
		private new NegotiateStream a;

		// Token: 0x04001B44 RID: 6980
		private new bi.b b;

		// Token: 0x04001B45 RID: 6981
		private new bool c;

		// Token: 0x04001B46 RID: 6982
		private new bool d;

		// Token: 0x04001B47 RID: 6983
		private new u.d e;

		// Token: 0x04001B48 RID: 6984
		private new u.f f;

		// Token: 0x02000485 RID: 1157
		private new class b : Stream
		{
			// Token: 0x060027EB RID: 10219 RVA: 0x000B91CE File Offset: 0x000B81CE
			public b(bi A_0, bool A_1)
			{
				this.k = A_0;
				this.a = null;
				this.g = false;
				this.h = false;
				this.e = 0;
				this.f = 0;
				this.j = 0;
				this.l = A_1;
			}

			// Token: 0x060027EC RID: 10220 RVA: 0x000B920E File Offset: 0x000B820E
			private bool a()
			{
				if (this.l)
				{
					this.l = false;
					return true;
				}
				return false;
			}

			// Token: 0x060027ED RID: 10221 RVA: 0x000B9222 File Offset: 0x000B8222
			public bool c()
			{
				return this.h;
			}

			// Token: 0x060027EE RID: 10222 RVA: 0x000B922A File Offset: 0x000B822A
			public void a(bool A_0)
			{
				this.h = A_0;
			}

			// Token: 0x060027EF RID: 10223 RVA: 0x000B9233 File Offset: 0x000B8233
			public byte b()
			{
				return this.b;
			}

			// Token: 0x060027F0 RID: 10224 RVA: 0x000B923C File Offset: 0x000B823C
			public override void Write(byte[] buffer, int offset, int count)
			{
				int num = 0;
				if (this.k.Stage == 0)
				{
					this.k.ExpectBase64Challenge = true;
				}
				if (count > 0)
				{
					if (this.j == 0)
					{
						this.i = false;
						int stage = this.k.Stage;
						if (stage == 0 || stage == 1)
						{
							if (count >= 5)
							{
								this.b = buffer[offset];
								this.i = (buffer[offset] == 21);
								num = 5;
								this.c = buffer[offset + 1];
								this.d = buffer[offset + 2];
								this.j = (int)buffer[offset + 3] * 256 + (int)buffer[offset + 4];
							}
						}
						else if (count >= 4)
						{
							num = 4;
							this.j = (int)buffer[offset] + (int)buffer[offset + 1] * 256;
						}
						this.a = new byte[this.j];
					}
					if (count == num)
					{
						return;
					}
					this.a = w.b(this.a, this.f, this.f + count - num, true);
					Buffer.BlockCopy(buffer, offset + num, this.a, this.f, count - num);
					this.f += count - num;
					if (this.i && this.f == 8)
					{
						int a_ = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(this.a, 4));
						this.k.ClientAnswer = null;
						this.k.b()(this.k, this.a());
						throw new MailBeeLoginWin32Exception(a_);
					}
					if (this.f == this.j)
					{
						this.k.ClientAnswer = this.a;
						this.a = null;
						this.f = 0;
						this.j = 0;
						this.k.a(this.k.b()(this.k, this.a()));
						this.h = false;
					}
				}
			}

			// Token: 0x060027F1 RID: 10225 RVA: 0x000B940C File Offset: 0x000B840C
			public override int Read(byte[] buffer, int offset, int count)
			{
				int num = 0;
				if (!this.g)
				{
					if (this.h)
					{
						this.k.ClientAnswer = new byte[0];
						this.k.b()(this.k, this.a());
						this.h = false;
					}
					int stage = this.k.Stage;
					if (stage == 0 || stage == 1)
					{
						num = 5;
						if (this.k.Stage == 0)
						{
							buffer[offset] = 22;
							buffer[offset + 3] = (byte)(this.k.ServerChallenge.Length / 256);
							buffer[offset + 4] = (byte)(this.k.ServerChallenge.Length & 255);
						}
						else if (this.k.Stage == 1)
						{
							buffer[offset] = 20;
							if (this.k.a())
							{
								buffer[offset + 3] = (byte)(this.k.ServerChallenge.Length / 256);
								buffer[offset + 4] = (byte)(this.k.ServerChallenge.Length & 255);
							}
							else
							{
								buffer[offset + 3] = 0;
								buffer[offset + 4] = 0;
							}
						}
						buffer[offset + 1] = this.c;
						buffer[offset + 2] = this.d;
						this.g = true;
					}
					else if (count >= 4)
					{
						num = 4;
						buffer[offset] = (byte)(this.k.ServerChallenge.Length & 255);
						buffer[offset + 1] = (byte)(this.k.ServerChallenge.Length / 256);
						buffer[offset + 2] = 0;
						buffer[offset + 3] = 0;
						this.g = true;
					}
				}
				int num2 = 0;
				if (count > num && this.k.a())
				{
					num2 = ((this.k.ServerChallenge.Length - this.e < count - num) ? (this.k.ServerChallenge.Length - this.e) : (count - num));
					Buffer.BlockCopy(this.k.ServerChallenge, this.e, buffer, offset + num, num2);
					this.e += num2;
				}
				if (this.e == this.k.ServerChallenge.Length)
				{
					bi bi = this.k;
					int stage = bi.Stage;
					bi.Stage = stage + 1;
					this.e = 0;
					this.g = false;
				}
				return num + num2;
			}

			// Token: 0x060027F2 RID: 10226 RVA: 0x000B9633 File Offset: 0x000B8633
			public override bool get_CanRead()
			{
				return true;
			}

			// Token: 0x060027F3 RID: 10227 RVA: 0x000B9636 File Offset: 0x000B8636
			public override bool get_CanWrite()
			{
				return true;
			}

			// Token: 0x060027F4 RID: 10228 RVA: 0x000B9639 File Offset: 0x000B8639
			public override bool get_CanSeek()
			{
				return false;
			}

			// Token: 0x060027F5 RID: 10229 RVA: 0x000B963C File Offset: 0x000B863C
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060027F6 RID: 10230 RVA: 0x000B9643 File Offset: 0x000B8643
			public override long get_Position()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060027F7 RID: 10231 RVA: 0x000B964A File Offset: 0x000B864A
			public override void set_Position(long value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060027F8 RID: 10232 RVA: 0x000B9651 File Offset: 0x000B8651
			public override long get_Length()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060027F9 RID: 10233 RVA: 0x000B9658 File Offset: 0x000B8658
			public override void SetLength(long value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060027FA RID: 10234 RVA: 0x000B965F File Offset: 0x000B865F
			public override void Flush()
			{
			}

			// Token: 0x060027FB RID: 10235 RVA: 0x000B9664 File Offset: 0x000B8664
			public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				bi.b.a a;
				a.c = this;
				a.e = buffer;
				a.f = offset;
				a.d = count;
				a.b = AsyncTaskMethodBuilder.Create();
				a.a = -1;
				AsyncTaskMethodBuilder asyncTaskMethodBuilder = a.b;
				asyncTaskMethodBuilder.Start<bi.b.a>(ref a);
				return a.b.Task;
			}

			// Token: 0x060027FC RID: 10236 RVA: 0x000B96C4 File Offset: 0x000B86C4
			public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				bi.b.b b;
				b.c = this;
				b.d = buffer;
				b.e = offset;
				b.f = count;
				b.b = AsyncTaskMethodBuilder<int>.Create();
				b.a = -1;
				AsyncTaskMethodBuilder<int> asyncTaskMethodBuilder = b.b;
				asyncTaskMethodBuilder.Start<bi.b.b>(ref b);
				return b.b.Task;
			}

			// Token: 0x04001B49 RID: 6985
			private byte[] a;

			// Token: 0x04001B4A RID: 6986
			private byte b;

			// Token: 0x04001B4B RID: 6987
			private byte c;

			// Token: 0x04001B4C RID: 6988
			private byte d;

			// Token: 0x04001B4D RID: 6989
			private int e;

			// Token: 0x04001B4E RID: 6990
			private int f;

			// Token: 0x04001B4F RID: 6991
			private bool g;

			// Token: 0x04001B50 RID: 6992
			private bool h;

			// Token: 0x04001B51 RID: 6993
			private bool i;

			// Token: 0x04001B52 RID: 6994
			private int j;

			// Token: 0x04001B53 RID: 6995
			private bi k;

			// Token: 0x04001B54 RID: 6996
			private bool l;
		}
	}
}
