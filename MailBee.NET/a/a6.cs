using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using a.j;
using MailBee;

namespace a
{
	// Token: 0x0200048B RID: 1163
	internal class a6 : SaslMethod, IDisposable
	{
		// Token: 0x0600280B RID: 10251 RVA: 0x000BA3E2 File Offset: 0x000B93E2
		protected virtual string k3()
		{
			return "NTLM";
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x000BA3E9 File Offset: 0x000B93E9
		public a6()
		{
			base.ExpectBase64Challenge = false;
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x000BA418 File Offset: 0x000B9418
		public override bool IsSecure()
		{
			return true;
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x000BA41B File Offset: 0x000B941B
		public override bool RequiresCredentials()
		{
			return false;
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x000BA41E File Offset: 0x000B941E
		public override string GetSaslID()
		{
			return "NTLM";
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x000BA425 File Offset: 0x000B9425
		internal override AuthenticationMethods GetMethodEnumMember()
		{
			return AuthenticationMethods.SaslNtlm;
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x000BA42C File Offset: 0x000B942C
		public override void CreateNextClientAnswer()
		{
			switch (base.Stage)
			{
			case 0:
				base.ExpectBase64Challenge = true;
				base.ClientAnswer = this.a();
				return;
			case 1:
				base.ClientAnswer = this.b(base.ServerChallenge);
				return;
			case 2:
				base.ClientAnswer = this.a(base.ServerChallenge);
				return;
			default:
				return;
			}
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x000BA48C File Offset: 0x000B948C
		[SecuritySafeCritical]
		private new byte[] a()
		{
			global::a.j.b b = new global::a.j.b(0U);
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			intPtr2 = this.b();
			int num;
			try
			{
				num = ac.AcquireCredentialsHandle(base.AccountName, this.k3(), 2, IntPtr.Zero, intPtr2, IntPtr.Zero, IntPtr.Zero, ref this.a, ref b);
			}
			finally
			{
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
			}
			if (num != 0)
			{
				throw new MailBeeLoginWin32Exception(num);
			}
			string targetName = base.TargetName;
			s[] array = new s[1];
			s[] array2 = new s[1];
			uint num2 = 0U;
			int num3 = -1;
			byte[] array3 = new byte[12288];
			int a_ = this.k4();
			GCHandle gchandle = GCHandle.Alloc(array3, GCHandleType.Pinned);
			array[0].c = gchandle.AddrOfPinnedObject();
			array[0].b = 2;
			array[0].a = array3.Length;
			an an;
			an.b = 1;
			GCHandle gchandle2 = GCHandle.Alloc(array, GCHandleType.Pinned);
			an.c = gchandle2.AddrOfPinnedObject();
			an.a = 0;
			array2[0].c = IntPtr.Zero;
			array2[0].b = 2;
			array2[0].a = 0;
			an an2;
			an2.b = 1;
			GCHandle gchandle3 = GCHandle.Alloc(array2, GCHandleType.Pinned);
			an2.c = gchandle3.AddrOfPinnedObject();
			an2.a = 0;
			IntPtr intPtr3 = Marshal.AllocHGlobal(Marshal.SizeOf(an2));
			Marshal.StructureToPtr(an2, intPtr3, false);
			try
			{
				if (targetName != null)
				{
					byte[] bytes = Encoding.Unicode.GetBytes(targetName + "\0");
					intPtr = Marshal.AllocHGlobal(bytes.Length);
					Marshal.Copy(bytes, 0, intPtr, bytes.Length);
				}
				global::a.j.b b2;
				num3 = ac.InitializeSecurityContext(ref this.a, IntPtr.Zero, intPtr, a_, 0, 16, IntPtr.Zero, 0, out this.b, out an, out num2, out b2);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				Marshal.FreeHGlobal(intPtr3);
				gchandle.Free();
				gchandle3.Free();
			}
			if (num3 < 0)
			{
				gchandle2.Free();
				throw new MailBeeLoginWin32Exception(num3);
			}
			byte[] array4 = new byte[array[0].a];
			Marshal.Copy(array[0].c, array4, 0, array4.Length);
			gchandle2.Free();
			int stage = base.Stage;
			base.Stage = stage + 1;
			return array4;
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x000BA710 File Offset: 0x000B9710
		[SecuritySafeCritical]
		private new byte[] b(byte[] A_0)
		{
			an an = default(an);
			s[] array = new s[1];
			an an2 = default(an);
			s[] array2 = new s[1];
			global::a.j.b b = default(global::a.j.b);
			IntPtr intPtr = IntPtr.Zero;
			string targetName = base.TargetName;
			if (targetName != null)
			{
				byte[] bytes = Encoding.Unicode.GetBytes(targetName + "\0");
				intPtr = Marshal.AllocHGlobal(bytes.Length);
				Marshal.Copy(bytes, 0, intPtr, bytes.Length);
			}
			int a_ = this.k4();
			GCHandle gchandle = GCHandle.Alloc(A_0, GCHandleType.Pinned);
			array[0].c = gchandle.AddrOfPinnedObject();
			array[0].a = A_0.Length;
			array[0].b = 2;
			an.b = 1;
			GCHandle gchandle2 = GCHandle.Alloc(array, GCHandleType.Pinned);
			an.c = gchandle2.AddrOfPinnedObject();
			an.a = 0;
			byte[] array3 = new byte[12288];
			GCHandle gchandle3 = GCHandle.Alloc(array3, GCHandleType.Pinned);
			array2[0].c = gchandle3.AddrOfPinnedObject();
			array2[0].b = 2;
			array2[0].a = array3.Length;
			an2.b = 1;
			GCHandle gchandle4 = GCHandle.Alloc(array2, GCHandleType.Pinned);
			an2.c = gchandle4.AddrOfPinnedObject();
			an2.a = 0;
			IntPtr intPtr2 = Marshal.AllocHGlobal(Marshal.SizeOf(this.b));
			Marshal.StructureToPtr(this.b, intPtr2, false);
			IntPtr intPtr3 = Marshal.AllocHGlobal(Marshal.SizeOf(an));
			Marshal.StructureToPtr(an, intPtr3, false);
			int num;
			try
			{
				uint num2;
				num = ac.InitializeSecurityContext(ref this.a, intPtr2, intPtr, a_, 0, 16, intPtr3, 0, out this.b, out an2, out num2, out b);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				Marshal.FreeHGlobal(intPtr2);
				Marshal.FreeHGlobal(intPtr3);
				gchandle3.Free();
				gchandle.Free();
				gchandle2.Free();
			}
			if (num == 0 || num == 590610)
			{
				byte[] array4 = new byte[array2[0].a];
				Marshal.Copy(array2[0].c, array4, 0, array2[0].a);
				gchandle4.Free();
				if (num == 0)
				{
					int stage = base.Stage;
					base.Stage = stage + 1;
				}
				return array4;
			}
			gchandle4.Free();
			throw new MailBeeLoginWin32Exception(num);
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x000BA980 File Offset: 0x000B9980
		[SecuritySafeCritical]
		private new byte[] a(byte[] A_0)
		{
			an an = default(an);
			s[] array = new s[2];
			GCHandle gchandle = GCHandle.Alloc(A_0, GCHandleType.Pinned);
			array[0].c = gchandle.AddrOfPinnedObject();
			array[0].a = A_0.Length;
			array[0].b = 10;
			an.b = 2;
			array[1].b = 1;
			array[1].a = 0;
			array[1].c = IntPtr.Zero;
			GCHandle gchandle2 = GCHandle.Alloc(array, GCHandleType.Pinned);
			an.c = gchandle2.AddrOfPinnedObject();
			an.a = 0;
			int num;
			try
			{
				num = ac.DecryptMessage(ref this.b, ref an, 0U, IntPtr.Zero);
			}
			finally
			{
				gchandle.Free();
			}
			if (num != 0)
			{
				gchandle2.Free();
				throw new MailBeeLoginWin32Exception(num);
			}
			a6.a a = new a6.a();
			Marshal.PtrToStructure((IntPtr)(an.c.ToInt64() + (long)Marshal.SizeOf(array[0])), a);
			byte[] array2 = new byte[a.a];
			Marshal.Copy(a.c, array2, 0, a.a);
			gchandle2.Free();
			p p = default(p);
			num = ac.QueryContextAttributes(ref this.b, 0U, out p);
			if (num != 0)
			{
				throw new MailBeeLoginWin32Exception(num);
			}
			array = new s[3];
			an.b = 3;
			GCHandle gchandle3 = GCHandle.Alloc(new byte[p.d], GCHandleType.Pinned);
			array[0].c = gchandle3.AddrOfPinnedObject();
			array[0].a = (int)p.d;
			array[0].b = 2;
			GCHandle gchandle4 = GCHandle.Alloc(array2, GCHandleType.Pinned);
			array[1].c = gchandle4.AddrOfPinnedObject();
			array[1].a = array2.Length;
			array[1].b = 1;
			GCHandle gchandle5 = GCHandle.Alloc(new byte[p.c], GCHandleType.Pinned);
			array[2].c = gchandle5.AddrOfPinnedObject();
			array[2].a = (int)p.c;
			array[2].b = 9;
			gchandle2 = GCHandle.Alloc(array, GCHandleType.Pinned);
			an.c = gchandle2.AddrOfPinnedObject();
			an.a = 0;
			num = ac.EncryptMessage(ref this.b, 2147483649U, ref an, 0U);
			if (num != 0)
			{
				gchandle3.Free();
				gchandle4.Free();
				gchandle5.Free();
				gchandle2.Free();
				throw new MailBeeLoginWin32Exception(num);
			}
			byte[] array3 = new byte[array[0].a + array[1].a + array[2].a];
			int num2 = 0;
			Marshal.Copy(array[0].c, array3, num2, array[0].a);
			num2 += array[0].a;
			Marshal.Copy(array[1].c, array3, num2, array[1].a);
			num2 += array[1].a;
			Marshal.Copy(array[2].c, array3, num2, array[2].a);
			gchandle3.Free();
			gchandle4.Free();
			gchandle5.Free();
			gchandle2.Free();
			int stage = base.Stage;
			base.Stage = stage + 1;
			return array3;
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x000BACFC File Offset: 0x000B9CFC
		internal override void set_TargetNameInternal(string value)
		{
			base.a(null);
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x000BAD08 File Offset: 0x000B9D08
		[SecuritySafeCritical]
		protected new IntPtr b()
		{
			if (base.AccountName == null || base.AccountName == string.Empty)
			{
				return IntPtr.Zero;
			}
			base.h();
			string a_ = base.e();
			IntPtr intPtr = IntPtr.Zero;
			ap ap = new ap(base.AccountName, base.Password, a_);
			intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(ap));
			Marshal.StructureToPtr(ap, intPtr, false);
			return intPtr;
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x000BAD78 File Offset: 0x000B9D78
		protected virtual int k4()
		{
			return 2048;
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x000BAD7F File Offset: 0x000B9D7F
		protected override void Dispose(bool disposing)
		{
			if (!this.c)
			{
				this.b(ref this.b);
				this.a(ref this.a);
				this.c = true;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x000BADB4 File Offset: 0x000B9DB4
		~a6()
		{
			this.Dispose(false);
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x000BADE4 File Offset: 0x000B9DE4
		[SecuritySafeCritical]
		private new void b(ref v A_0)
		{
			if (A_0.b != IntPtr.Zero && A_0.b != A_0.a)
			{
				ac.DeleteSecurityContext(ref A_0);
				A_0 = new v(IntPtr.Zero);
			}
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x000BAE22 File Offset: 0x000B9E22
		[SecuritySafeCritical]
		private new void a(ref v A_0)
		{
			if (A_0.b != IntPtr.Zero && A_0.b != A_0.a)
			{
				ac.FreeCredentialsHandle(ref A_0);
				A_0 = new v(IntPtr.Zero);
			}
		}

		// Token: 0x04001B6D RID: 7021
		protected new v a = new v(IntPtr.Zero);

		// Token: 0x04001B6E RID: 7022
		protected new v b = new v(IntPtr.Zero);

		// Token: 0x04001B6F RID: 7023
		protected new bool c;

		// Token: 0x0200048C RID: 1164
		[StructLayout(LayoutKind.Sequential)]
		internal new class a
		{
			// Token: 0x04001B70 RID: 7024
			public int a;

			// Token: 0x04001B71 RID: 7025
			public int b;

			// Token: 0x04001B72 RID: 7026
			public IntPtr c;
		}
	}
}
