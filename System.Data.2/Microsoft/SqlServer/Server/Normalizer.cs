using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Security.Permissions;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200005F RID: 95
	internal abstract class Normalizer
	{
		// Token: 0x060004F4 RID: 1268 RVA: 0x00046A4C File Offset: 0x00045E4C
		internal static Normalizer GetNormalizer(Type t)
		{
			Normalizer normalizer = null;
			if (t.IsPrimitive)
			{
				if (t == typeof(byte))
				{
					normalizer = new ByteNormalizer();
				}
				else if (t == typeof(sbyte))
				{
					normalizer = new SByteNormalizer();
				}
				else if (t == typeof(bool))
				{
					normalizer = new BooleanNormalizer();
				}
				else if (t == typeof(short))
				{
					normalizer = new ShortNormalizer();
				}
				else if (t == typeof(ushort))
				{
					normalizer = new UShortNormalizer();
				}
				else if (t == typeof(int))
				{
					normalizer = new IntNormalizer();
				}
				else if (t == typeof(uint))
				{
					normalizer = new UIntNormalizer();
				}
				else if (t == typeof(float))
				{
					normalizer = new FloatNormalizer();
				}
				else if (t == typeof(double))
				{
					normalizer = new DoubleNormalizer();
				}
				else if (t == typeof(long))
				{
					normalizer = new LongNormalizer();
				}
				else if (t == typeof(ulong))
				{
					normalizer = new ULongNormalizer();
				}
			}
			else if (t.IsValueType)
			{
				normalizer = new BinaryOrderedUdtNormalizer(t, false);
			}
			if (normalizer == null)
			{
				throw new Exception(Res.GetString("Sql_CanotCreateNormalizer", new object[]
				{
					t.FullName
				}));
			}
			normalizer.m_skipNormalize = false;
			return normalizer;
		}

		// Token: 0x060004F5 RID: 1269
		internal abstract void Normalize(FieldInfo fi, object recvr, Stream s);

		// Token: 0x060004F6 RID: 1270
		internal abstract void DeNormalize(FieldInfo fi, object recvr, Stream s);

		// Token: 0x060004F7 RID: 1271 RVA: 0x00046BD4 File Offset: 0x00045FD4
		protected void FlipAllBits(byte[] b)
		{
			for (int i = 0; i < b.Length; i++)
			{
				b[i] = ~b[i];
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00046BF8 File Offset: 0x00045FF8
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		protected object GetValue(FieldInfo fi, object obj)
		{
			return fi.GetValue(obj);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00046C0C File Offset: 0x0004600C
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		protected void SetValue(FieldInfo fi, object recvr, object value)
		{
			fi.SetValue(recvr, value);
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060004FA RID: 1274
		internal abstract int Size { get; }

		// Token: 0x040001E6 RID: 486
		protected bool m_skipNormalize;
	}
}
