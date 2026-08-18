using System;
using System.Collections;
using System.Text;

namespace a.b
{
	// Token: 0x020002A8 RID: 680
	internal class @as : Hashtable
	{
		// Token: 0x060017C7 RID: 6087 RVA: 0x0006CF37 File Offset: 0x0006BF37
		public static @as a()
		{
			if (@as.e == null)
			{
				@as @as = new @as();
				@as.a(@as.a, ch.b());
				@as.a(@as.b, ch.a());
				@as.e = @as;
			}
			return @as.e;
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x0006CF74 File Offset: 0x0006BF74
		public static string a(byte[] A_0, long A_1)
		{
			ch ch = @as.a().a(A_0);
			if (ch == null)
			{
				return "[undefined]";
			}
			string text = (string)ch.a(A_1);
			if (text == null)
			{
				return "[undefined]";
			}
			return text;
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x0006CFAD File Offset: 0x0006BFAD
		public ch a(byte[] A_0)
		{
			return (ch)this[Encoding.UTF8.GetString(A_0)];
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x0006CFC5 File Offset: 0x0006BFC5
		public object a(object A_0)
		{
			return this.a((byte[])A_0);
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x0006CFD4 File Offset: 0x0006BFD4
		public object a(byte[] A_0, ch A_1)
		{
			this[A_0] = A_1;
			return A_1;
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x0006CFEC File Offset: 0x0006BFEC
		public object a(object A_0, object A_1)
		{
			return this.a((byte[])A_0, (ch)A_1);
		}

		// Token: 0x040011DA RID: 4570
		public static readonly byte[] a = new byte[]
		{
			242,
			159,
			133,
			224,
			79,
			249,
			16,
			104,
			171,
			145,
			8,
			0,
			43,
			39,
			179,
			217
		};

		// Token: 0x040011DB RID: 4571
		public static readonly byte[] b = new byte[]
		{
			213,
			205,
			213,
			2,
			46,
			156,
			16,
			27,
			147,
			151,
			8,
			0,
			43,
			44,
			249,
			174
		};

		// Token: 0x040011DC RID: 4572
		public static readonly byte[] c = new byte[]
		{
			213,
			205,
			213,
			5,
			46,
			156,
			16,
			27,
			147,
			151,
			8,
			0,
			43,
			44,
			249,
			174
		};

		// Token: 0x040011DD RID: 4573
		public const string d = "[undefined]";

		// Token: 0x040011DE RID: 4574
		private static @as e;
	}
}
